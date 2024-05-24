using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPORTReport_EXPORT_rptLCAdvised : System.Web.UI.Page
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

            txtBenfID.Attributes.Add("onkeydown", "return BenfId(event)");
            btnBenfList.Attributes.Add("onclick", "return Benfhelp()");
            txtIssuingBank.Attributes.Add("onkeydown", "return IssuingBankId(event)");
            BtnIssuingBankList.Attributes.Add("onclick", "return IssuingBankhelp()");

            if (!IsPostBack)
            {
                clearControls();
                rdbDocDate.Checked = true;
                ddlBranch.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                
                txtFromDate.Attributes.Add("onblur", "toDate();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                //btnCustList.Attributes.Add("onclick", "return OpenCustList('mouseClick');");

                fillddlBranch();

                rdbDocDate.Visible = true;
                rdbAllBeneficiary .Visible = false;
                rdbSelectedBeneficiary.Visible = false;
                rdbAllIssuingBank.Visible = false;
                rdbSelectedIssuingBank.Visible = false;


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
        //txtToDate.Text = "";
        txtFromDate.Focus();
        rdbDocDate.Checked = true;
    }

    //public void fillBenfIdDescription()
    //{
    //    lblBeneficiaryName.Text = "";
    //    string Benfid = txtBenfID.Text.Trim();
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BenfID", SqlDbType.VarChar);
    //    p1.Value = Benfid;
    //    string _query = "TF_GetBeneficaryMasterDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblBeneficiaryName.Text = dt.Rows[0]["Benf_Name"].ToString().Trim();
    //    }
    //    else
    //    {
    //        txtBenfID.Text = "";
    //        lblBeneficiaryName.Text = "";
    //    }
    //}

    public void fillIssuingBankIdDescription()
    {
        lblIssuingBank.Text = "";
        string IssuingBankid = txtIssuingBank.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = IssuingBankid;
        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblIssuingBank.Text = dt.Rows[0]["BankName"].ToString().Trim();
        }
        else
        {
            txtIssuingBank.Text = "";
            lblIssuingBank.Text = "";
        }
    }


    //protected void btnCustId_Click(object sender, EventArgs e)
    //{
    //    if (hdnCustId.Value != "")
    //    {
    //        txtCustomerID.Text = hdnCustId.Value;
    //        fillCustomerIdDescription();
    //        txtCustomerID.Focus();
    //    }
    //}



    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }

  
    protected void rdbDocDate_CheckedChanged(object sender, EventArgs e)
    {
        {
            if (rdbDocDate.Checked == true)
            {
                rdbAllBeneficiary.Visible = false;
                rdbSelectedBeneficiary.Visible = false;
                rdbAllIssuingBank.Visible = false;
                rdbSelectedIssuingBank.Visible = false;
                rdbSelectedBeneficiary.Checked = false;
                Benflist.Visible = false;
                IssuingBankList.Visible = false;



            }
            rdbDocDate.Focus();
        }
    }
    protected void rdbBenfWise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbBenfWise.Checked == true)
        {
            rdbAllBeneficiary.Visible = true;
            rdbSelectedBeneficiary.Checked = false;
            rdbAllBeneficiary.Checked = true;
            rdbSelectedBeneficiary.Visible = true;
            Benflist.Visible = false;
            IssuingBankList.Visible = false;
            rdbAllIssuingBank.Visible = false;
            rdbSelectedIssuingBank.Visible = false;



        }
        rdbBenfWise.Focus();
    }
    protected void rdbIssueingBank_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbIssueingBank.Checked == true)
        {
            rdbSelectedIssuingBank.Checked = false;
            rdbAllIssuingBank.Checked = true;
            rdbAllIssuingBank.Visible = false;
            rdbSelectedBeneficiary.Visible = false;
            rdbAllBeneficiary.Visible = false;
            Benflist.Visible = false;
            
            rdbAllIssuingBank.Visible = true;
            rdbSelectedIssuingBank.Visible = true;

        }
        else
        {
            rdbAllIssuingBank.Visible = false;
            rdbSelectedIssuingBank.Visible = false;
        }
        rdbIssueingBank.Focus();
    }
    protected void rdbLCno_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbLCno.Checked == true)
        {
            rdbAllBeneficiary.Visible = false;
            rdbSelectedBeneficiary.Visible = false;
            rdbAllIssuingBank.Visible = false;
            rdbSelectedBeneficiary.Visible = false;
            rdbSelectedIssuingBank.Visible = false;
            Benflist.Visible = false;
            IssuingBankList.Visible = false;
        }
        rdbLCno.Focus();
    }
    protected void rdbAllBeneficiary_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllBeneficiary.Checked = true;
        Benflist.Visible = false;
        rdbAllBeneficiary.Focus();
    }
    protected void rdbSelectedBeneficiary_CheckedChanged(object sender, EventArgs e)
    {
        Benflist.Visible = true;
        txtBenfID.Text = "";
       // lblBeneficiaryName.Text = "Beneficiary Name";
        rdbSelectedBeneficiary.Focus();
        txtBenfID.Visible = true;
        btnBenfList.Visible = true;
       // lblBeneficiaryName.Visible = true;
        rdbSelectedBeneficiary.Focus();
    }
    protected void txtBenfID_TextChanged1(object sender, EventArgs e)
    {
        //fillBenfIdDescription();
        txtBenfID.Focus();
    }
    protected void rdbAllIssuingBank_CheckedChanged(object sender, EventArgs e)
    {

        rdbAllIssuingBank.Checked = true;
        IssuingBankList.Visible = false;
        rdbAllIssuingBank.Focus();
    }
    protected void rdbSelectedIssuingBank_CheckedChanged(object sender, EventArgs e)
    {
        IssuingBankList.Visible = true;
        txtIssuingBank.Text = "";
        lblIssuingBank.Text = "Issuing Bank Name";
        txtIssuingBank.Visible = true;
        BtnIssuingBankList.Visible = true;
        lblIssuingBank.Visible = true;
        rdbSelectedIssuingBank.Focus();
    }
    protected void txtIssuingBank_TextChanged1(object sender, EventArgs e)
    {
        fillIssuingBankIdDescription();
        txtIssuingBank.Focus();
    }
}
