using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Math;
using Org.BouncyCastle.Utilities.Encoders;
using DocumentFormat.OpenXml;

public partial class Reports_EXPORTReports_TF_EXP_BILL_Realisation_Report : System.Web.UI.Page
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
            btnConsigneeList.Attributes.Add("onclick", "return Consigneehelp()");
            txtConsignee.Attributes.Add("onkeydown", "return ConsigneeId(event)");
            if (!IsPostBack)
            {
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillddlBranch();
                btnSave.Attributes.Add("onclick", "return ShowProgress();");
                //btnSave.Attributes.Add("onclick", "return validateSave();");
                rdbforeign.Checked = true;
                rdbDocumnetwise.Visible = true;
                rdbAllCustomer.Visible = false;
                rdbSelectedCustomer.Visible = false;
                rdbAllOverseasBank.Visible = false;
                rdbSelectedOverseasBank.Visible = false;
                rdbAllOverseasParty.Visible = false;
                rdbSelectedOverseasParty.Visible = false;
                rdbAllCountry.Visible = false;
                rdbSelectedCountry.Visible = false;
                rdbAllConsignee.Visible = false;
                rdbSelectedConsignee.Visible = false;
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
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
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
        rdbDocumnetwise.Checked = true;
    }
    public void fillCustomerIdDescription()
    {
        try
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
        catch (Exception ex)
        {
            ViewState["Method"] = "FillCustomerIdDescription";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('" + ex.Message + "');", true);
            this.LogError(ex);
        }
    }
    public void fillOVPartyIdDescription()
    {
        try
        {
            lblCustomerName.Text = "";
            string partyid = txtOVPID.Text.Trim();
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
            p1.Value = partyid;
            SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
            p2.Value = "EXPORT";

            string _query = "TF_GetOverseasPartyMasterDetails_LOD";
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
        catch (Exception ex)
        {
            ViewState["Method"] = "fillOVPartyIdDescription";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('" + ex.Message + "');", true);
            this.LogError(ex);
        }
    }
    private void fillOverseasBankDescription()
    {
        try
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
        catch (Exception ex)
        {
            ViewState["Method"] = "fillOverseasBankDescription";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('" + ex.Message + "');", true);
            this.LogError(ex);
        }
    }
    public void fillCountryDescription()
    {
        try
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
        catch (Exception ex)
        {
            ViewState["Method"] = "fillCountryDescription";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('" + ex.Message + "');", true);
            this.LogError(ex);
        }
    }
    public void fillConsigneeIdDescription()
    {
        try
        {
            lblConsigneePartyName.Text = "";
            string consigneeid = txtConsignee.Text.Trim();
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@consigneeid", SqlDbType.VarChar);
            p1.Value = consigneeid;
            string _query = "TF_rptGetConsigneeMasterDetails";
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                lblConsigneePartyName.Text = dt.Rows[0]["Party_NAME"].ToString().Trim();
            }
            else
            {
                txtConsignee.Text = "";
                lblConsigneePartyName.Text = "";
            }
        }
        catch (Exception ex)
        {
            ViewState["Method"] = "fillConsigneeIdDescription";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('" + ex.Message + "');", true);
            this.LogError(ex);
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
                rdbAllConsignee.Visible = false;
                rdbSelectedConsignee.Visible = false;
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
                consigneeparty.Visible = false;
            }
            rdbDocumnetwise.Focus();
        }
    }
    protected void rdbCustomerwise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCustomerwise.Checked == true)
        {
            rdbAllCustomer.Visible = true;
            rdbAllCustomer.Checked = true;
            rdbSelectedCustomer.Visible = true;
            rdbSelectedCustomer.Checked = false;

            CustList.Visible = false;
            txtCustomer.Text = "";
            lblCustomerName.Text = "Customer Name";
            txtCustomer.Visible = true;
            BtnCustList.Visible = true;
            lblCustomerName.Visible = true;
            rdbSelectedCustomer.Focus();

            rdbSelectedOverseasParty.Visible = false;
            rdbAllOverseasParty.Visible = false;
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
            rdbAllConsignee.Visible = false;
            rdbSelectedConsignee.Visible = false;
            OVPartylist.Visible = false;
            OVBanklist.Visible = false;
            CountryList.Visible = false;
            consigneeparty.Visible = false;
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
            rdbAllOverseasParty.Visible = true;
            rdbAllOverseasParty.Checked = true;
            rdbSelectedOverseasParty.Visible = true;
            rdbSelectedOverseasParty.Checked = false;
            OVPartylist.Visible = false;
            txtOVPID.Text = "";
            lblOVPartyName.Text = "Overseas Party Name";
            txtOVPID.Visible = true;
            btnOVPList.Visible = true;
            lblOVPartyName.Visible = true;
            rdbSelectedOverseasParty.Focus();


            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
            CustList.Visible = false;
            OVBanklist.Visible = false;
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
            rdbAllConsignee.Visible = false;
            rdbSelectedConsignee.Visible = false;
            CountryList.Visible = false;
            consigneeparty.Visible = false;


        }
        else
        {
            rdbAllOverseasParty.Visible = false;
            rdbSelectedOverseasParty.Visible = false;
        }
        rdbOVPartywise.Focus();
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
    protected void txtOVPID_TextChanged(object sender, EventArgs e)
    {
        fillOVPartyIdDescription();
        txtOVPID.Focus();
    }
    protected void rdbOVBankwise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbOVBankwise.Checked == true)
        {
            rdbAllOverseasBank.Visible = true;
            rdbAllOverseasBank.Checked = true;
            rdbSelectedOverseasBank.Visible = true;
            rdbSelectedOverseasBank.Checked = false;
            OVBanklist.Visible = false;
            txtOVBank.Text = "";
            lblOVbankName.Text = "Overseas Bank Name";
            txtOVBank.Visible = true;
            btnOVBankList.Visible = true;
            lblOVbankName.Visible = true;
            rdbSelectedOverseasBank.Focus();

            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
            rdbAllOverseasParty.Visible = false;
            rdbSelectedOverseasParty.Visible = false;
            rdbAllConsignee.Visible = false;
            rdbSelectedConsignee.Visible = false;
            CustList.Visible = false;
            OVPartylist.Visible = false;
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
            CountryList.Visible = false;
            consigneeparty.Visible = false;

        }
        else
        {
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
        }
        rdbCustomerwise.Focus();
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
    protected void txtOVBank_TextChanged(object sender, EventArgs e)
    {
        fillOverseasBankDescription();
        txtOVBank.Focus();
    }
    protected void rdbCountrywise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCountrywise.Checked == true)
        {
            rdbAllCountry.Visible = true;
            rdbAllCountry.Checked = true;
            rdbSelectedCountry.Visible = true;
            rdbSelectedCountry.Checked = false;
            CountryList.Visible = false;
            txtCountry.Text = "";
            lblCountyName.Text = "Currency Name";
            txtCountry.Visible = true;
            btnCountryList.Visible = true;
            lblCountyName.Visible = true;
            rdbSelectedCountry.Focus();

            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
            rdbAllOverseasParty.Visible = false;
            rdbSelectedOverseasParty.Visible = false;
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
            rdbAllConsignee.Visible = false;
            rdbSelectedConsignee.Visible = false;
            CustList.Visible = false;
            OVPartylist.Visible = false;
            OVBanklist.Visible = false;
            consigneeparty.Visible = false;


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
    protected void rdbConsignee_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbConsignee.Checked == true)
        {
            rdbAllConsignee.Visible = true;
            rdbAllConsignee.Checked = true;
            rdbSelectedConsignee.Visible = true;
            rdbSelectedConsignee.Checked = false;
            consigneeparty.Visible = false;
            txtConsignee.Text = "";
            lblConsigneePartyName.Text = "Consignee ID";
            txtConsignee.Visible = true;
            btnConsigneeList.Visible = true;
            lblConsigneePartyName.Visible = true;
            rdbSelectedConsignee.Focus();


            rdbSelectedCustomer.Checked = false;
            rdbAllCustomer.Checked = false;
            rdbAllCustomer.Visible = false;
            rdbSelectedCustomer.Visible = false;
            rdbSelectedOverseasParty.Visible = false;
            rdbAllOverseasParty.Visible = false;
            rdbAllOverseasBank.Visible = false;
            rdbSelectedOverseasBank.Visible = false;
            rdbAllCountry.Visible = false;
            rdbSelectedCountry.Visible = false;
            OVPartylist.Visible = false;
            OVBanklist.Visible = false;
            CountryList.Visible = false;
            CustList.Visible = false;
        }
        else
        {
            rdbAllConsignee.Visible = false;
            rdbSelectedConsignee.Visible = false;
        }
        rdbConsignee.Focus();
    }
    protected void rdbAllConsignee_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllConsignee.Checked = true;
        consigneeparty.Visible = false;
        OVPartylist.Visible = false;
        OVBanklist.Visible = false;
        CountryList.Visible = false;
        rdbAllConsignee.Focus();
    }
    protected void rdbSelectedConsignee_CheckedChanged(object sender, EventArgs e)
    {
        consigneeparty.Visible = true;
        txtConsignee.Text = "";
        lblConsigneePartyName.Text = "Consignee ID";
        txtConsignee.Visible = true;
        btnConsigneeList.Visible = true;
        lblConsigneePartyName.Visible = true;
        rdbSelectedConsignee.Focus();
    }
    protected void txtConsignee_TextChanged(object sender, EventArgs e)
    {
        fillConsigneeIdDescription();
        txtConsignee.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            TF_DATA objdata = new TF_DATA();
            string script = "",ID="", reporttype="";

            if (rdbforeign.Checked == true)
            {
                //script = "TF_rptExpRealisationReport_Excel_Foreign";
                if (rdbDocumnetwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Docwise_foreign";
                    reporttype = "Document_Wise";
                }
                if (rdbCustomerwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Custwise_foreign";
                    reporttype = "Customer_Wise";
                    if (rdbAllCustomer.Checked == true)
                    {
                        ID = "All";
                    }
                    else if (rdbSelectedCustomer.Checked == true)
                    {
                        ID = txtCustomer.Text.Trim();
                    }
                }
                if (rdbOVPartywise.Checked == true)
                {
                    reporttype = "OverseasParty_Wise";
                    script = "TF_Export_Report_Realisation_Ovrsesprtywise_foreign";
                    if (rdbAllOverseasParty.Checked == true)
                    {
                        ID = "All";
                    }
                    else if (rdbSelectedOverseasParty.Checked == true)
                    {
                        ID = txtOVPID.Text.Trim();
                    }
                }
                if (rdbOVBankwise.Checked == true)
                {
                    reporttype = "OverseasBank_Wise";
                    script = "TF_Export_Report_Realisation_Foreign_OverseasBankWise";
                    if (rdbAllOverseasBank.Checked == true)
                    {
                        ID = "All";
                    }
                    else if (rdbSelectedOverseasBank.Checked == true)
                    {
                        ID = txtOVBank.Text.Trim();
                    }
                }
                if (rdbCountrywise.Checked == true)
                {
                    reporttype = "Currency_Wise";
                    script = "TF_Export_Report_Realisation_Currencywise_foreign";
                    if (rdbAllCountry.Checked == true)
                    {
                        ID = "All";
                    }
                    else if (rdbSelectedCountry.Checked == true)
                    {
                        ID = txtCountry.Text.Trim();
                    }
                }
                if (rdbConsignee.Checked == true)
                {
                    reporttype = "Consignee_Wise";
                    script = "TF_Export_Report_Realisation_Consigneewise_foreign";
                    if (rdbAllConsignee.Checked == true)
                    {
                        ID = "All";
                    }
                    else if (rdbSelectedConsignee.Checked == true)
                    {
                        ID = txtConsignee.Text.Trim();
                    }
                }
            }
            if (rdblocal.Checked == true)
            {
                //script = "TF_rptExpRealisationReport_Excel_Local";
               
                if (rdbDocumnetwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Local_Docwise";
                }
                if (rdbCustomerwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Local_Custwise";
                }
                if (rdbOVPartywise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Local_Ovrsesprtywise";
                }
                if (rdbOVBankwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Ovrsesbankwise_Local";
                }
                if (rdbCountrywise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Local_Currencywise";
                }
                if (rdbConsignee.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Local_Consigneewise";
                }
            }
            if (rdbboth.Checked == true)
            {
                //script = "TF_rptExpRealisationReport_Excel";
                if (rdbDocumnetwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Both_Docwise";
                }
                if (rdbCustomerwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Both_Custwise";
                }
                if (rdbOVPartywise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Both_Ovrsesprtywise";
                }
                if (rdbOVBankwise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Ovrsesbankwise_Local";
                }
                if (rdbCountrywise.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Both_Currencywise";
                }
                if (rdbConsignee.Checked == true)
                {
                    script = "TF_Export_Report_Realisation_Both_Consigneewise";
                }
            }

            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text.Trim());
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text.Trim());
            SqlParameter p4 = new SqlParameter("@ID", ID);
            DataTable dt = objdata.getData(script, p1, p2, p3, p4);

            string adcode = ddlBranch.SelectedValue.ToString();
            string dateInfo = DateTime.Now.ToString("ddMMyyyy").Trim();
            string date = DateTime.Now.ToString("_ddMMyyyy_HHmmss").Trim();
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Realisation/" + adcode + "/" + dateInfo + "/" + reporttype + "/");
            string FileName = "TF_Export_Bill_Realisation" + date + ".xlsx";
            ViewState["_directoryPath"] = ("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Realisation/" + adcode + "/" + dateInfo + "/" + reporttype + "/");
            ViewState["FileName"] = FileName;

            if (dt.Rows.Count > 0)
            {
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                ViewState["MyDataTable"] = dt;
                string _filePath = _directoryPath + FileName;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    int red = 31;
                    int green = 112;
                    int blue = 166;
                    //var ws = wb.Worksheets.Add(dt, "Export Bill Realisation");
                    var ws = wb.Worksheets.Add("Export Bill Realisation");
                    
                    var firstRow = ws.Row(1);                   
                    List<string> columnsToAlign = new List<string> { "Bill_Amount", "OSBillAmount", "InstructedAmount", "RealizedAmount", "FbankChanges", "PCFC_Amt", "EEFC_Amt","Cross Currency Amt","ShippinBillAmount","ShippingBillSettlement_Amt" };
                    List<string> exchrate = new List<string> { "ExchangeRate","InternalExchRate", "Cross Currency Rate" };
                    int startRow = 1;
                    int startColumn = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        // Fetch header name from DataTable and write to Excel
                        ws.Cell(startRow, startColumn + j).Value = dt.Columns[j].ColumnName;

                        // Check if the current column name exists in columnsToAlign or columnsToAlign1
                        string columnName = dt.Columns[j].ColumnName;
                        if ((!columnsToAlign.Contains(columnName) && !exchrate.Contains(columnName)) || columnsToAlign.Contains(columnName) || exchrate.Contains(columnName))
                        {
                            // Set background color and bold property for non-blank fields
                            ws.Cell(startRow, startColumn + j).Style.Fill.BackgroundColor = XLColor.FromArgb(red, green, blue);
                            ws.Cell(startRow, startColumn + j).Style.Font.FontColor = XLColor.White;
                            ws.Cell(startRow, startColumn + j).Style.Font.Bold = true;
                        }
                    }
                    



                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        // Fetch header name from DataTable and write to Excel
                        ws.Cell(startRow, startColumn + j).Value = dt.Columns[j].ColumnName;
                    }
                    // Write data from DataTable starting from the second row
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            // Write each cell value to the worksheet
                            ws.Cell(startRow + i + 1, startColumn + j).Value = dt.Rows[i][j];
                            string cellData = dt.Rows[i][j].ToString();

                            if (j == 48) // Zero-based index for column 43
                            {
                                cellData = "'" + cellData; // Format as text
                            }

                            ws.Cell(startRow + i + 1, startColumn + j).Value = cellData;

                        }

                    }

                    foreach (string columnName in columnsToAlign)
                    {
                        int columnIndex = dt.Columns.IndexOf(columnName) + 1; // Adding 1 because ClosedXML is 1-based index                                                
                        ws.Column(columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        ws.Column(columnIndex).Style.NumberFormat.Format = "#,##0.00"; // Set the number format as desired                        
                    }
                    foreach (string columnName1 in exchrate)
                    {
                        int columnIndex1 = dt.Columns.IndexOf(columnName1) + 1; // Adding 1 because ClosedXML is 1-based index                     
                        ws.Column(columnIndex1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;                       
                        ws.Column(columnIndex1).Style.NumberFormat.Format = "#,##0.00000"; // Set the number format as desired
                    }

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                    lblpath.Text = "<b>" + ViewState["_directoryPath"].ToString().Trim() + "</b>";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "alert('Report Generated Successfully..'); ClickAnotherButton();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "setTimeout(function() { alert('Report Generated Successfully..');ClickAnotherButton();  setTimeout(ClickAnotherButton, 10); }, 10);", true);
                    lblmsg.Text = "<b> Report Generated Successfully.. </b>";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('No Record Found.');", true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "There is some issue to generate the report.";
            string _Addedby = Session["UserName"].ToString();
            TF_DATA objSave = new TF_DATA();
            SqlParameter Report_Name = new SqlParameter("@Report_Name", "TF_Export_Bill_Realisation-Excel");
            SqlParameter Module = new SqlParameter("@Module", "Export".ToUpper());
            SqlParameter Menu = new SqlParameter("@Menu", "Report");
            SqlParameter Error_msg = new SqlParameter("@Error_msg", ex.Message);
            SqlParameter Error_code = new SqlParameter("@Error_code", ex.StackTrace);
            SqlParameter Error_time = new SqlParameter("@Error_time", System.DateTime.Now);
            SqlParameter Error_Part = new SqlParameter("@Error_Part", "During generating the report (Button Generate Click)");
            SqlParameter Addedby = new SqlParameter("@Addedby", _Addedby.ToUpper());
            string _query = "TF_EXP_Report_Log";
            string result = objSave.SaveDeleteData(_query, Report_Name, Module, Menu, Error_msg, Error_code, Error_time, Addedby);
        }
    }
    protected void btnexcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["MyDataTable"];
            if (dt != null)
            {
                string _directoryPath = ViewState["_directoryPath"].ToString();
                string FileName = ViewState["FileName"].ToString();
                Response.ContentType = "image/jpg";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                Response.TransmitFile(Server.MapPath(_directoryPath + "/" + FileName));
                Response.End();
                lblmsg.Text = "Report Generated Successfully..";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('No Record Found For Download.');", true);
            }
        }
        catch(Exception ex)
        {
            lblmsg.Text = "There is some issue to download the report.";            
            string _Addedby = Session["UserName"].ToString();
            TF_DATA objSave = new TF_DATA();
            SqlParameter Report_Name = new SqlParameter("@Report_Name", "TF_Export_Bill_Realisation-Excel");
            SqlParameter Module = new SqlParameter("@Module", "Export".ToUpper());
            SqlParameter Menu = new SqlParameter("@Menu", "Report");
            SqlParameter Error_msg = new SqlParameter("@Error_msg", ex.Message);            
            SqlParameter Error_Place = new SqlParameter("@Error_Place", ex.StackTrace);
            SqlParameter Error_time = new SqlParameter("@Error_time", System.DateTime.Now);
            SqlParameter Error_Part = new SqlParameter("@Error_Part", "During Downloading the report");
            SqlParameter Addedby = new SqlParameter("@Addedby", _Addedby.ToUpper());            
            string _query = "TF_EXP_Report_Log";
            string result = objSave.SaveDeleteData(_query, Report_Name, Module, Menu,Error_msg, Error_Place, Error_time, Error_Part,Addedby);
        }
    }
    private void LogError(Exception ex)
    {
        string method = ViewState["Method"].ToString();
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", ex.Message);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", ex.StackTrace);
        message += Environment.NewLine;
        message += string.Format("Source: {0}", ex.Source);
        message += Environment.NewLine;
        message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
        message += Environment.NewLine;
        message += "Method " + method;
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;

        string FileName = "ErrorLog.txt";
        string dateInfo = DateTime.Now.ToString("ddMMyyyy").Trim();
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Outstanding More Than 270/" + dateInfo + "/");
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        //string path = Server.MapPath("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Outstanding/ErrorLog/" + dateInfo);
        string path = _directoryPath + "/" + FileName;
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }
    }
}