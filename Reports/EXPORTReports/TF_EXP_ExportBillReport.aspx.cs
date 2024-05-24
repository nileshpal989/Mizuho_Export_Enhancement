using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
public partial class Reports_EXPORTReports_TF_EXP_ExportBillReport : System.Web.UI.Page
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
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (!IsPostBack)
            {
                clearControls();
                rdbDocumentWise.Checked = true;
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                txtFromDate.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
            txtCustomerID.Attributes.Add("onkeydown", "return CustId(event)");
            btnCustList.Attributes.Add("onclick", "return custhelp()");
            txtOverseasPartyID.Attributes.Add("onkeydown", "return OVPartyId(event)");
            btnOverseasPartyList.Attributes.Add("onclick", "return OVpartyhelp()");
            txtOverseasBankID.Attributes.Add("onkeydown", "return OVBankId(event)");
            btnOverseasBankList.Attributes.Add("onclick", "return OVBankhelp()");
            txtCurrency.Attributes.Add("onkeydown", "return CurrencyId(event)");
            btnCurrencyList.Attributes.Add("onclick", "return Currencyhelp()");
            txtConsigneePartyID.Attributes.Add("onkeydown", "return ConsigneeId(event)");
            btnConsigneePartyList.Attributes.Add("onclick", "return Consigneehelp()");
               
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
        //li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);

    }
    protected void clearControls()
    {
        txtFromDate.Text = "";

        txtFromDate.Focus();
        rdbDocumentWise.Checked = true;
    }
    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomerID.Text.Trim();
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
            txtCustomerID.Text = "";
            lblCustomerName.Text = "";
        }
    }
    public void fillOVPartyIdDescription()
    {
        lblCustomerName.Text = "";
        string partyid = txtOverseasPartyID.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = partyid;
        SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        p2.Value = "EXPORT";

        string _query = "TF_GetOverseasPartyMasterDetailsLOD";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblOverseasPartyName.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
        }
        else
        {
            txtOverseasPartyID.Text = "";
            lblOverseasPartyName.Text = "";
        }
    }
    private void fillOverseasBankDescription()
    {
        lblOverseasBankName.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtOverseasBankID.Text;
        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblOverseasBankName.Text.Length > 20)
            {
                lblOverseasBankName.ToolTip = lblOverseasBankName.Text;
                lblOverseasBankName.Text = lblOverseasBankName.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOverseasBankID.Text = "";
            lblOverseasBankName.Text = "";
        }

    }
    public void fillCountryDescription()
    {
        lblCurrency.Text = "";

        string Countryid = txtCurrency.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@C_Code", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "HelpCurMstr1";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCurrency.Text = dt.Rows[0]["C_Description"].ToString().Trim();
        }
        else
        {
            txtCurrency.Text = "";
            lblCurrency.Text = "";
        }
    }
    public void fillConsigneeIdDescription()
    {
        string partyid = txtConsigneePartyID.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = partyid;

        string _query = "TF_GetConsigneePartyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblConsigneePartyName.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
        }
        else
        {
            txtConsigneePartyID.Text = "";
            lblConsigneePartyName.Text = "";
        }
    }
    protected void btnCustId_Click(object sender, EventArgs e)
    {
        if (hdnCustId.Value != "")
        {
            txtCustomerID.Text = hdnCustId.Value;
            fillCustomerIdDescription();
            txtCustomerID.Focus();
        }
    }
    protected void rdbDocumentWise_CheckedChanged(object sender, EventArgs e)
    {
        rdbDocumentWise.Checked = true;
        OverseasPartylist.Visible = false;
        OverseasBanklist.Visible = false;
        ConsigneePartylist.Visible = false;
        Custlist.Visible = false;
        Currencylist.Visible = false;
        rdbDocumentWise.Focus();
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = true;
        OverseasBanklist.Visible = false;
        OverseasPartylist.Visible = false;
        Currencylist.Visible = false;
        txtCustomerID.Text = "";
        lblCustomerName.Text = "Customer Name";
        rdbSelectedCustomer.Focus();
        txtCustomerID.Visible = true;
        btnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus();
    }

    protected void rdbSelectedOverseasParty_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = false;
        OverseasBanklist.Visible = false;
        ConsigneePartylist.Visible = false;
        Currencylist.Visible = false;
        OverseasPartylist.Visible = true;
        txtOverseasPartyID.Text = "";
        lblOverseasPartyName.Text = "Overseas Party Name";
        rdbSelectedOverseasParty.Focus();
        txtOverseasPartyID.Visible = true;
        btnOverseasPartyList.Visible = true;
        lblOverseasPartyName.Visible = true;
    }

    protected void txtCustomerID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }
    protected void txtCustomerID_TextChanged1(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomerID.Focus();
    }
    protected void txtOverseasPartyID_TextChanged(object sender, EventArgs e)
    {
        fillOVPartyIdDescription();
        txtOverseasPartyID.Focus();
    }
    protected void rdbOverseasBankWise_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = false;
        OverseasPartylist.Visible = false;
        ConsigneePartylist.Visible = false;
        Currencylist.Visible = false;
        OverseasBanklist.Visible = true;
        txtOverseasBankID.Text = "";
        lblOverseasBankName.Text = "Overseas Bank Name";
        rdbSelectedOverseasParty.Focus();
        txtOverseasBankID.Visible = true;
        btnOverseasBankList.Visible = true;
        lblOverseasBankName.Visible = true;
    }
    protected void txtOverseasBankID_TextChanged(object sender, EventArgs e)
    {
        
        fillOverseasBankDescription();
        txtOverseasBankID.Focus();
    }

    protected void rdbSelectedConsigneeParty_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = false;
        OverseasPartylist.Visible = false;
        OverseasBanklist.Visible = false;
        Currencylist.Visible = false;
        ConsigneePartylist.Visible = true;
        txtConsigneePartyID.Text = "";
        lblConsigneePartyName.Text = "Consignee Party Name";
        rdbSelectedConsigneeParty.Focus();
        txtConsigneePartyID.Visible = true;
        btnConsigneePartyList.Visible = true;
        lblConsigneePartyName.Visible = true;
    }
    protected void txtConsigneePartyID_TextChanged(object sender, EventArgs e)
    {
        fillConsigneeIdDescription();
        txtConsigneePartyID.Focus();
    }

    protected void rdbSelectedCurrency_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = false;
        OverseasPartylist.Visible = false;
        OverseasBanklist.Visible = false;
        ConsigneePartylist.Visible = false;
        Currencylist.Visible = true;
        txtCurrency.Text = "";
        lblCurrency.Text = "Currency";
        rdbSelectedCurrency.Focus();
        txtCurrency.Visible = true;
        btnCurrencyList.Visible = true;
        lblCurrency.Visible = true;
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtCurrency.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string CurrentDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (ddlBranch.SelectedValue == "---Select---")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Select Branch.');", true);
        }
        else if (txtFromDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please select From Date.');", true);
        }

        else if (rdbSelectedCustomer.Checked == true && txtCustomerID.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Customer A/c No.');", true);
        }
        else if (rdbSelectedOverseasParty.Checked == true && txtOverseasPartyID.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Overseas Party ID.');", true);
        }
        else if (rdbOverseasBankWise.Checked == true && txtOverseasBankID.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Overseas Bank ID.');", true);
        }
        else if (rdbSelectedConsigneeParty.Checked == true && txtConsigneePartyID.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Consignee Party ID.');", true);
        }
        else if (rdbSelectedCurrency.Checked == true && txtCurrency.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Currency.');", true);
        }
        else
        {
            string Wise = "";

            if (rdbDocumentWise.Checked == true)
            {
                Wise = "All";
            }
            if (rdbSelectedCustomer.Checked == true)
            {
                Wise = txtCustomerID.Text.Trim();
            }
            if (rdbSelectedOverseasParty.Checked == true)
            {
                Wise = txtOverseasPartyID.Text.Trim();
            }
            if (rdbOverseasBankWise.Checked == true)
            {
                Wise = txtOverseasBankID.Text.Trim();
            }
            if (rdbSelectedConsigneeParty.Checked == true)
            {
                Wise = txtConsigneePartyID.Text.Trim();
            }
            if (rdbSelectedCurrency.Checked == true)
            {
                Wise = txtCurrency.Text.Trim();
            }
            TF_DATA objdata = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text.ToString());
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text.ToString());
            SqlParameter p4 = new SqlParameter("@Wise", Wise);
            string script = "TF_IRMExportBillReceipt";
            DataTable dt = objdata.getData(script, p1, p2, p3, p4);
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data_CheckLsit");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=TF_ExportBillReceiptReport" + Convert.ToDateTime(CurrentDate) + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

            else
            {
                // txtFromDate.Text = "";
                //txtFromDate.Focus();
                //Response.Write("<script>alert('No Records')</script>");
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('No Records.');", true);

            }
        }
    }  
    
}