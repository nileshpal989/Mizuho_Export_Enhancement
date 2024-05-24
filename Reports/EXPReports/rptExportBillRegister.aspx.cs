using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPReports_rptExportBillRegister : System.Web.UI.Page
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
                txtFromDate.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                txtFromDate.Attributes.Add("onblur", "toDate();");
                btnSave.Attributes.Add("onclick", "return validateSave();");


                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                rdbDocumnetwise.Visible = true;
                rdbAllCustomer.Visible = false;
                rdbSelectedCustomer.Visible = false;
                rdbAllOverseasBank.Visible = false;
                rdbSelectedOverseasBank.Visible = false;
                rdbAllOverseasParty.Visible = false;
                rdbSelectedOverseasParty.Visible = false;
                rdbAllCountry.Visible = false;
                rdbSelectedCountry.Visible = false;
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
        txtToDate.Text = "";
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
        //SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        //p2.Value = "EXPORT";

        string _query = "TF_GetOverseasPartyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
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
    }
    protected void rdbDocumnetwise_CheckedChanged(object sender, EventArgs e)
    {
        {
            if (rdbDocumnetwise.Checked == true)
            {
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
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountyName.Text = dt.Rows[0]["CountryName"].ToString().Trim();
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
        lblCountyName.Text = "Country Name";
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
}
