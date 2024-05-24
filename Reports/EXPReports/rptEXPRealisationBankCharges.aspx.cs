using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPReports_rptEXPRealisationBankCharges : System.Web.UI.Page
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

            txtCustomerID.Attributes.Add("onkeydown", "return CustId(event)");
            btnCustList.Attributes.Add("onclick", "return custhelp()");

            txtCurrency.Attributes.Add("onkeydown", "return CurId(event)");
            btnCurList.Attributes.Add("onclick", "return curhelp()");

            if (!IsPostBack)
            {
                clearControls();

                rdbAllcur.Checked = true;

                fillProcessingDate();

                //     btnSave.Attributes.Add("onclick", "validateSave();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                fillddlBranch();

                txtCurrency.Visible = false;
                txtCustomerID.Visible = false;
               
                btnCurList.Visible = false;
                ddlBranch.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
    }
    public void fillCurrencyIdDescription()
    {
        lblCurrency.Text = "";
        string cur = txtCurrency.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p1.Value = cur;
        string _query = "TF_GetCurrencyMasterDetails";
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
            ddlBranch.DataValueField = "BranchName";
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
        rdbAllcur.Checked = true;
    }

    protected void btnChangeDate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime newDate = new DateTime();

        string frmDate = "";
        string toDate = "";

        if (txtFromDate.Text != "")
        {
            newDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            frmDate = newDate.ToString("dd/MM/yyyy");
        }

        if (txtFromDate.Text == "")
        {

        }
        else
        {
            DateTime nextDate = new DateTime();

            nextDate = System.DateTime.Now;

            toDate = nextDate.ToString("dd/MM/yyyy");

        }
    }


    protected void fillProcessingDate()
    {
        if (Session["startdate"] != null)
        {
            txtFromDate.Text = Session["startdate"].ToString();
            hdnFromDate.Value = Session["startdateMM"].ToString();
        }


    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();

    }
   
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {

        txtFromDate.Focus();

    }
    
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Focus();
        rdbAllCustomer.Checked = true;
        Custlist.Visible = false;

    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = true;
        txtCustomerID.Text = "";
        lblCustomerName.Text = "Customer Name";
        rdbSelectedCustomer.Focus();
        txtCustomerID.Visible = true;
        btnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus();
    }
    protected void txtCustomerID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        fillCurrencyIdDescription();
    }
    protected void rdbAllcur_CheckedChanged(object sender, EventArgs e)
    {

        rdbAllcur.Checked = true;
        rdbAllcur.Focus();
        rdbSingleCur.Checked = false;
        txtCurrency.Visible = false;
        rdbSelectedCustomer.Checked = false;
        rdbAllCustomer.Checked = true;
        btnCurList.Visible = false;
        txtCustomerID.Visible = false;
        btnCustList.Visible = false;
        Curlist.Visible = false;
        Custlist.Visible = false;


        txtCurrency.Text = "";
    }
    protected void rdbSingleCur_CheckedChanged(object sender, EventArgs e)
    {
        
        rdbSingleCur.Checked = true;
        lblCurrency.Text = "Currency Name";
        lblCurrency.Visible = true;
        rdbSingleCur.Focus();
        Curlist.Visible = true;
        txtCurrency.Visible = true;
        btnCurList.Visible = true;
        rdbAllcur.Checked = false;

    }
}