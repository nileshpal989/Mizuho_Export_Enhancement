using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;


public partial class Reports_EXPReports_Export_Report_BillOutStanding : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            txtCustomer.Attributes.Add("onkeydown", "return CustId(event)");
            BtnCustList.Attributes.Add("onclick", "return Custhelp()");
            txtOVPID.Attributes.Add("onkeydown", "return OVPartyId(event)");
            btnOVPList.Attributes.Add("onclick", "return OVpartyhelp()");
            txtOVBank.Attributes.Add("onkeydown", "return OVBankId(event)");
            btnOVBankList.Attributes.Add("onclick", "return OVBankhelp()");
            txtCountry.Attributes.Add("onkeydown", "return CountryId(event)");
            btnCountryList.Attributes.Add("onclick", "return Countryhelp()");
            if (!IsPostBack)
            {
                clearControls();
                rdbDocumnetwise.Checked = true;
                ddlBranch.Focus();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;

                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                rdbDocumnetwise.Visible = true;
                rdbAllCustomer.Visible = false;
                rdbSelectedCustomer.Visible = false;
                rdbAllOverseasBank.Visible = false;
                rdbSelectedOverseasBank.Visible = false;
                rdbAllOverseasParty.Visible = false;
                rdbSelectedOverseasParty.Visible = false;
                rdbAllCountry.Visible = false;
                rdbSelectedCountry.Visible = false;
                //rdbADV.Visible = false;
            }
        }
    }

    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        ListItem li01 = new ListItem();
        li.Value = "1";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.Items.Insert(1, li01);
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtFromDate.Focus();
        rdbDocumnetwise.Checked = true;
    }
    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomer.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtCustomer.Text = "";
            lblCustomerName.Text = "";
        }
    }
    public void fillOVPartyIdDescription()
    {
        lblCustomerName.Text = "";
        string partyid = txtOVPID.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = partyid;
        SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        p2.Value = "EXPORT";

        string _query = "TF_GetOverseasPartyMasterDetailsLOD";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblOVPartyName.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
        }
        else
        {
            txtOVPID.Text = "";
            lblOVPartyName.Text = "";
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
        txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }
    protected void rdbDocumnetwise_CheckedChanged(object sender, EventArgs e)
    {
        {
            if (rdbDocumnetwise.Checked == true)
            {
                btnCreate.Visible = true;
                rdbAllCustomer.Visible = false;
                rdbSelectedCustomer.Visible = false;
                rdbAllOverseasParty.Visible = false;
                rdbSelectedOverseasParty.Visible = false;
                rdbAllOverseasBank.Visible = false;
                rdbSelectedOverseasBank.Visible = false;
                rdbAllCountry.Visible = false;
                rdbSelectedCountry.Visible = false;
                CustList.Visible = false;
                OVPartylist.Visible = false;
                OVBanklist.Visible = false;
                CountryList.Visible = false;
            }
            rdbDocumnetwise.Focus();
        }
    }

    protected void rdbCustomerwise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCustomerwise.Checked == true)
        {
            btnCreate.Visible = true;
            rdbSelectedCustomer.Checked = false;
            rdbAllCustomer.Checked = true;

            rdbAllCustomer.Visible = true;
            rdbSelectedCustomer.Visible = true;
            rdbSelectedOverseasParty.Visible = false;
            rdbAllOverseasParty.Visible = false;
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
            OVPartylist.Visible = false;
            OVBanklist.Visible = false;
            CountryList.Visible = false;
        }
        else
        {
            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
        }
        rdbCustomerwise.Focus();
    }
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = true;
        CustList.Visible = false;
        OVPartylist.Visible = false;
        OVBanklist.Visible = false;
        CountryList.Visible = false;
        rdbAllCustomer.Focus();
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        CustList.Visible = true;
        txtCustomer.Text = "";
        lblCustomerName.Text = "Customer Name";
        txtCustomer.Visible = true;
        BtnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus();
    }

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomer.Focus();
    }
    protected void rdbOVPartywise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbOVPartywise.Checked == true)
        {
            rdbAllOverseasParty.Checked = true;
            rdbSelectedOverseasParty.Checked = false;

            rdbAllOverseasParty.Visible = true;
            rdbSelectedOverseasParty.Visible = true;
            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
            CustList.Visible = false;
            OVBanklist.Visible = false;
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
            CountryList.Visible = false;
            btnCreate.Visible = false;

        }
        else
        {
            rdbAllOverseasParty.Visible = false;
            rdbSelectedOverseasParty.Visible = false;
        }
        rdbOVPartywise.Focus();
    }
    protected void rdbOVBankwise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbOVBankwise.Checked == true)
        {
            rdbAllOverseasBank.Checked = true;
            rdbSelectedOverseasBank.Checked = false;
            rdbAllOverseasBank.Visible = true;
            rdbSelectedOverseasBank.Visible = true;
            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
            rdbAllOverseasParty.Visible = false;
            rdbSelectedOverseasParty.Visible = false;
            CustList.Visible = false;
            OVPartylist.Visible = false;
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
            CountryList.Visible = false;
            btnCreate.Visible = false;
        }
        else
        {
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
        }
        rdbCustomerwise.Focus();
    }
    protected void rdbAllOverseasParty_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllOverseasParty.Checked = true;
        CustList.Visible = false;
        OVBanklist.Visible = false;
        OVPartylist.Visible = false;
        rdbAllCountry.Visible = false;
        CountryList.Visible = false;
        rdbAllOverseasParty.Focus();
    }
    protected void rdbSelectedOverseasParty_CheckedChanged(object sender, EventArgs e)
    {
        OVPartylist.Visible = true;
        txtOVPID.Text = "";
        lblOVPartyName.Text = "Overseas Party Name";
        txtOVPID.Visible = true;
        btnOVPList.Visible = true;
        lblOVPartyName.Visible = true;
        rdbSelectedOverseasParty.Focus();
    }
    protected void rdbAllOverseasBank_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllOverseasBank.Checked = true;
        OVBanklist.Visible = false;
        OVPartylist.Visible = false;
        CustList.Visible = false;
        CountryList.Visible = false;
        rdbAllOverseasBank.Focus();
    }
    protected void rdbSelectedOverseasBank_CheckedChanged(object sender, EventArgs e)
    {
        OVBanklist.Visible = true;
        txtOVBank.Text = "";
        lblOVbankName.Text = "Overseas Bank Name";
        txtOVBank.Visible = true;
        btnOVBankList.Visible = true;
        lblOVbankName.Visible = true;
        rdbSelectedOverseasBank.Focus();
    }

    protected void txtOVPID_TextChanged(object sender, EventArgs e)
    {
        fillOVPartyIdDescription();
        txtOVPID.Focus();
    }
    private void fillOverseasBankDescription()
    {
        lblOVbankName.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtOVBank.Text;
        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOVbankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblOVbankName.Text.Length > 20)
            {
                lblOVbankName.ToolTip = lblOVbankName.Text;
                lblOVbankName.Text = lblOVbankName.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOVBank.Text = "";
            lblOVbankName.Text = "";
        }

    }
    protected void txtOVBank_TextChanged(object sender, EventArgs e)
    {
        fillOverseasBankDescription();
        txtOVBank.Focus();
    }
    protected void rdbCountrywise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCountrywise.Checked == true)
        {

            rdbAllCountry.Checked = true;
            rdbSelectedCountry.Checked = false;
            rdbAllCountry.Visible = true;
            rdbSelectedCountry.Visible = true;
            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
            rdbAllOverseasParty.Visible = false;
            rdbSelectedOverseasParty.Visible = false;
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
            CustList.Visible = false;
            OVPartylist.Visible = false;
            CountryList.Visible = false;
            OVBanklist.Visible = false;
            btnCreate.Visible = false;
        }
        else
        {
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
        }
        rdbCountrywise.Focus();
    }
    protected void rdbAllCountry_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCountry.Checked = true;
        OVBanklist.Visible = false;
        OVPartylist.Visible = false;
        CustList.Visible = false;
        CountryList.Visible = false;
        rdbAllCountry.Focus();
    }
    public void fillCountryDescription()
    {
        lblCountyName.Text = "";

        string Countryid = txtCountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@C_Code", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "HelpCurMstr1";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountyName.Text = dt.Rows[0]["C_Description"].ToString().Trim();
        }
        else
        {
            txtCountry.Text = "";
            lblCountyName.Text = "";
        }
    }
    protected void rdbSelectedCountry_CheckedChanged(object sender, EventArgs e)
    {
        CountryList.Visible = true;
        txtCountry.Text = "";
        lblCountyName.Text = "Currency Name";
        txtCountry.Visible = true;
        btnCountryList.Visible = true;
        lblCountyName.Visible = true;
        rdbSelectedCountry.Focus();
    }
    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtCountry.Focus();
    }
    protected void rdbBLBD_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdbSLBD_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        bool flag = true;
        if (ddlBranch.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Branch Name.');", true);
            flag = false;
        }
        if (txtFromDate.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter As on Date');", true);
            flag = false;
        }
        if (flag == true)
        {
            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            //string _directoryPath = ddlBranch.Text.ToString() + "-" + documentDate.ToString("ddMMyyyy");
            //_directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + _directoryPath);

            string a = Session["userADCode"].ToString();
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + a);

            string DocType = "";
            string Loan = "";
            string LC = "";
            string BillType = "All";
            string Unaccepted = "";
            string custAcNO = "";
            if (rdbAll.Checked == true)
            {
                DocType = "All";
            }
            if (rbtbla.Checked == true)
                DocType = "BLA";
            if (rbtblu.Checked == true)
                DocType = "BLU";
            if (rbtbba.Checked == true)
                DocType = "BBA";
            if (rbtbbu.Checked == true)
                DocType = "BBU";
            if (rbtbca.Checked == true)
                DocType = "BCA";
            if (rbtbcu.Checked == true)
                DocType = "BCU";
            if (rbtIBD.Checked == true)
                DocType = "IBD";
            //if (rbtLBC.Checked == true)
            //    DocType = "LBC";
            if (rbtBEB.Checked == true)
                DocType = "EB";

            if (rdbLoanAdv.Checked == true)
            {
                Loan = "Y";
            }
            else if (rdbLoanNotAdv.Checked == true)
            {
                Loan = "N";
            }
            else if (rdbLoanAll.Checked == true)
            {
                Loan = "All";
            }

            if (rbdLCwise.Checked == true)
            {
                LC = "2";
            }
            else if (rbdNonLcwise.Checked == true)
            {
                LC = "1";
            }
            else if (rbdLCAll.Checked == true)
            {
                LC = "All";
            }

            //if (rdbSight.Checked == true)
            //{
            //    BillType = "S";
            //}
            //else if (rdbUsance.Checked == true)
            //{
            //    BillType = "U";
            //}
            //else if (rbdBillType.Checked == true)
            //{
            //    BillType = "All";
            //}
            if (rbdAccepted.Checked == true)
            {
                Unaccepted = "2";
            }
            else if (rbdUnaccepted.Checked == true)
            {
                Unaccepted = "1";
            }
            else if (rdbAcceptedAll.Checked == true)
            {
                Unaccepted = "All";
            }

            if (rdbCustomerwise.Checked == true)
            {
                if (rdbAllCustomer.Checked == true)
                {
                    custAcNO = "All";
                }
                else if (rdbSelectedCustomer.Checked == true)
                {
                    string txt = txtCustomer.Text.ToString();
                    if (txt == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Customer A/c No.');", true);
                    }
                    custAcNO = txtCustomer.Text.ToString();
                }
            }
            else
                custAcNO = "All";

            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p1.Value = documentDate.ToString("MM/dd/yyyy");
            SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p2.Value = ddlBranch.Text.ToString().Trim();
            SqlParameter p3 = new SqlParameter("@doctype", SqlDbType.VarChar);
            p3.Value = DocType;
            SqlParameter p4 = new SqlParameter("@Loan", SqlDbType.VarChar);
            p4.Value = Loan;
            SqlParameter p5 = new SqlParameter("@LC", SqlDbType.VarChar);
            p5.Value = LC;
            SqlParameter p6 = new SqlParameter("@billtype", SqlDbType.VarChar);
            p6.Value = BillType;
            SqlParameter p7 = new SqlParameter("@Type", SqlDbType.VarChar);
            p7.Value = "";
            SqlParameter p8 = new SqlParameter("@unaccepted", SqlDbType.VarChar);
            p8.Value = Unaccepted;
            SqlParameter p9 = new SqlParameter("@CustAccNo", SqlDbType.VarChar);
            p9.Value = custAcNO;
            int cnt = 0;
            string _qry = "TF_Export_Report_ExportBillOutStanding_CSV";
            DataTable dt = objData.getData(_qry, p1, p2, p3, p4, p5, p6, p7, p8,p9);
            if (dt.Rows.Count > 0)
            {
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                
                //string _filePath = _directoryPath + "/" + ddlBranch.Text.ToString() + documentDate.ToString("dd-MM-yyyy") + ".csv";

                string _FromDate = txtFromDate.Text.Substring(0, 2) + " " + txtFromDate.Text.Substring(3, 2) + " " + txtFromDate.Text.Substring(6, 4);
                string _filePath = _directoryPath + "/Exp_OS-" + _FromDate + ".csv";

                StreamWriter sw;
                sw = File.CreateText(_filePath);
                sw.WriteLine("Branch,Doc_Date,Document_No,LC_Date,LC_Number,Customer_Name,Customer_Invoice No.,Overseas_Bank_Name,Drawee,Curr,Loan_Advance,Net_Amount,Bill_Amount,Amount_Realised,Balance,Bill_Due_Date,Accepted_Due_Date,Payment_Tenor,Goods,Country," +
                 "Shipping_Bill_No, Shipping_Bill_Date, InvoiceNo, InvoiceDate,GR_PP CUST NO , PortCode, Shipping Bill Currency, Shipping Bill Amount,Customer_AccountNo,Swift,IFSC,Bank_Country,Dispatch_Indicator,Notes");
                

                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    string _strLC_Date = dt.Rows[j]["LC_Date"].ToString().Trim();
                    string _strLC_Date1 = _strLC_Date.Replace(",", " -");
                    string _strLC_Number = dt.Rows[j]["LC_Number"].ToString().Trim();
                    string _strLC_Number1 = _strLC_Number.Replace(",", " -");
                    string _strNet_Amount = dt.Rows[j]["Net_Amount"].ToString().Trim();
                    string _strNet_Amount1 = _strNet_Amount.Replace(",", " -");

                    string _strBankName = dt.Rows[j]["BankName"].ToString().Trim();
                    string _strBankName1 = _strBankName.Replace(",", " -");

                    string _strBranchName = dt.Rows[j]["BranchName"].ToString().Trim();
                    string _strBranchName1 = _strBranchName.Replace(",", " -");
                    string _strDate_Negotiated = dt.Rows[j]["Date_Negotiated"].ToString().Trim();
                    string _strDate_Negotiated1 = _strDate_Negotiated.Replace(",", " -");
                    string _strDocument_No = dt.Rows[j]["Document_No"].ToString().Trim();
                    string _stDocument_No = _strDocument_No.Replace(",", " -");
                    string _strCUST_NAME = dt.Rows[j]["CUST_NAME"].ToString().Trim();
                    string _strCUST_NAME1 = _strCUST_NAME.Replace(",", " -");
                    string _strInvoice_No = dt.Rows[j]["Invoice_No"].ToString().Trim();
                    string _strInvoice_No1 = _strInvoice_No.Replace(",", " -");

                    string _strParty_Name = dt.Rows[j]["Party_Name"].ToString().Trim();
                    string _strParty_Name1 = _strParty_Name.Replace(",", " -");

                    string _strOther_Currency_For_INR = dt.Rows[j]["Other_Currency_For_INR"].ToString().Trim();
                    string _strOther_Currency_For_INR1 = _strOther_Currency_For_INR.Replace(",", " -");

                    string _strBill_Amount = dt.Rows[j]["Bill_Amount"].ToString().Trim();
                    string _strBill_Amount1 = _strBill_Amount.Replace(",", " -");

                    string _strRealised_Amount = dt.Rows[j]["Realised_Amount"].ToString().Trim();
                    string _strRealised_Amount1 = _strRealised_Amount.Replace(",", " -");
                    string _strBalance = dt.Rows[j]["Balance"].ToString().Trim();
                    string _strBalance1 = _strBalance.Replace(",", " -");

                    string _strDue_Date = dt.Rows[j]["Due_Date"].ToString().Trim();
                    string _strDue_Date1 = _strDue_Date.Replace(",", " -");

                    string _strAccepted_Due_Date = dt.Rows[j]["Accepted_Due_Date"].ToString().Trim();
                    string _strAccepted_Due_Date1 = _strAccepted_Due_Date.Replace(",", " -");

                    string _strDocument_Type = dt.Rows[j]["Document_Type"].ToString().Trim();
                    string _strLoan = dt.Rows[j]["Loan"].ToString().Trim();
                    string _strLoan1 = _strLoan.Replace(",", " -");
                    string _strLC = dt.Rows[j]["LC_NO"].ToString().Trim();
                    string _strBillType = dt.Rows[j]["Bill_Type"].ToString().Trim();
                    string _strUnaccepted = dt.Rows[j]["Unaccepted"].ToString().Trim();
                    string _strcustAcNO = dt.Rows[j]["CustAcNo"].ToString().Trim();
                    string _strPaymentTenor = dt.Rows[j]["PaymentTenor"].ToString().Trim().Replace(",","-");
                    string _strGoods = dt.Rows[j]["GoodsDescription"].ToString().Trim().Replace(",", "-") ;
                    string _strcountry = dt.Rows[j]["Country"].ToString().Trim().Replace(",", "-") ;

                    string _Shipping_Bill_No = dt.Rows[j]["Shipping_Bill_No"].ToString().Trim().Replace(",", "-");
                    string _Shipping_Bill_Date = dt.Rows[j]["Shipping_Bill_Date"].ToString().Trim().Replace(",", "-");
                    string _InvoiceNo = dt.Rows[j]["InvoiceNo"].ToString().Trim().Replace(",", "-");
                    string _InvoiceDate = dt.Rows[j]["InvoiceDate"].ToString().Trim().Replace(",", "-");
                    string _GRcustomsNo = dt.Rows[j]["GR"].ToString().Trim().Replace(",", "-");
                    string _PortCode = dt.Rows[j]["PortCode"].ToString().Trim().Replace(",", "-");
                    string _ShippingCurrency = dt.Rows[j]["GRCurrency"].ToString().Trim().Replace(",", "-");
                    string _ShippingAmount = dt.Rows[j]["Amount"].ToString().Trim().Replace(",", "-");
                    
                    string _strCustAccno = dt.Rows[j]["CustAcNo"].ToString().Trim();
                    string _strswift = dt.Rows[j]["Swift"].ToString().Trim().Replace(",", "-");
                    string _strifsc = dt.Rows[j]["IFSC"].ToString().Trim().Replace(",", "-");
                    string _strbankcountry = dt.Rows[j]["Bank_Country"].ToString().Trim().Replace(",", "-");
                    string _DispatchIndi = dt.Rows[j]["DispatchIndicator"].ToString().Trim();
                    string _strnotes = dt.Rows[j]["Document_Remarks"].ToString().Trim().Replace(",", "-"); 
                    

                    if ((_strBranchName1 == ddlBranch.Text.ToString().Trim() || ddlBranch.Text.ToString().Trim() == "All Branches") && (_strDocument_Type == DocType || DocType == "All") && (_strLoan == Loan || Loan == "All")
                         && (_strLC == LC || LC == "All") && (_strBillType == BillType || BillType == "All") && (_strUnaccepted == Unaccepted || Unaccepted == "All")
                        && (_strcustAcNO == custAcNO || custAcNO == "All"))
                    {
                        sw.Write(_strBranchName1 + ",");

                        sw.Write(_strDate_Negotiated1 + ",");
                                               
                        sw.Write(_strDocument_No + ",");

                        sw.Write(_strLC_Date1 + ",");
                        sw.Write(_strLC_Number1 + ",");

                        sw.Write(_strCUST_NAME1 + ",");

                        sw.Write(_strInvoice_No1 + ",");


                        sw.Write(_strBankName1 + ",");

                        sw.Write(_strParty_Name1 + ",");

                        sw.Write(_strOther_Currency_For_INR1 + ",");

                        sw.Write(_strLoan1 + ",");
                       
                        sw.Write(_strNet_Amount1 + ",");
                       
                        sw.Write(_strBill_Amount1 + ",");

                        sw.Write(_strRealised_Amount1 + ",");

                        sw.Write(_strBalance1 + ",");

                        sw.Write(_strDue_Date1 + ",");

                        sw.Write(_strAccepted_Due_Date1 + ",");

                        sw.Write(_strPaymentTenor + ",");

                        sw.Write(_strGoods + ",");

                        sw.Write(_strcountry + ",");
                        

                        sw.Write(_Shipping_Bill_No + ",");

                        sw.Write(_Shipping_Bill_Date+ ",");

                        sw.Write(_InvoiceNo + ",");

                        sw.Write(_InvoiceDate+ ",");

                        sw.Write(_GRcustomsNo+ ",");

                        sw.Write(_PortCode + ",");

                        sw.Write(_ShippingCurrency + ",");

                        sw.Write(_ShippingAmount + ",");

                        sw.Write(_strCustAccno + ",");

                        sw.Write(_strswift + ",");

                        sw.Write(_strifsc + ",");

                        sw.Write(_strbankcountry + ",");

                        sw.Write(_DispatchIndi + ",");

                        sw.WriteLine(_strnotes + ",");

                        cnt++;
                    }

                }
                sw.Flush();
                sw.Close();
                sw.Dispose();

                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();

                // labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;
                // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);

                if (cnt == 0)
                {
                    labelMessage.Text = "There is No Record for This Dates " + documentDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    string path = "file://" + _serverName + "/TF_GeneratedFiles/EXPORT/" + ddlBranch.SelectedItem.Text;
                    string link = "/TF_GeneratedFiles/EXPORT/" + ddlBranch.SelectedItem.Text;
                    labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                }


                ddlBranch.Focus();

            }
            else
            {
                //labelMessage.Text = "No Reocrds ";
                labelMessage.Text = "There is No Record for This Dates " + documentDate.ToString("dd/MM/yyyy");
                //ddlBranch.Focus();

                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record for This Dates');", true);
            }
        }
    }
}