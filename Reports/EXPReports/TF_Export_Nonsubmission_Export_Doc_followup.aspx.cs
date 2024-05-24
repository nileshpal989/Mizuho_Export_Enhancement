using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPORTReports_TF_Export_Nonsubmission_Export_Doc_followup : System.Web.UI.Page
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
            if (!IsPostBack)
            {

                fillBranch();
                ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                ddlBranch.Enabled = false;
                txtFromDate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btnCreate.Attributes.Add("onclick", "return validateSave();");
                rdbAllCust.Attributes.Add("onClick", "return validateDisplay();");
                rdbSelectedCust.Attributes.Add("onClick", "return validateDisplay();");
                txtFromDate.Attributes.Add("onblur", "isValidDate(" + txtFromDate.ClientID + "," + "' From Date.'" + ");");
                txtToDate.Attributes.Add("onblur", "isValidDate(" + txtToDate.ClientID + "," + "' To Date.'" + ");");

            }
        }
    }

    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    public void fillCustomerIdDescription()
    {
        lblCustName.Text = "";
        string custid = txtCustAccountNo.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtCustAccountNo.Text = "";
            lblCustName.Text = "";
        }
    }
    protected void txtCustAccountNo_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        trCustAcNo.Attributes.Add("style", "block");
    }
}