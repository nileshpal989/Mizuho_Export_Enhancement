using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class Reports_EXPORTReports_rpt_Inward_Remittance_outstanding_statement : System.Web.UI.Page
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
                txtAsOnDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;

                rdbAllCustomer.Checked = true;
                Customer_CheckedChanged();
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
        if (dt.Rows.Count > 0)
        {
            li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddlBranch.Items.Insert(0, li);
    }
    public void fillCustomerIdDescription()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", txtCustomerID.Text.Trim());
        DataTable dt = objData.getData("TF_rptGetCustomerMasterDetails", p1);
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
    protected void txtCustomerID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }
    protected void rdbCustomer_CheckedChanged(object sender, EventArgs e)
    {
        Customer_CheckedChanged();
    }
    private void Customer_CheckedChanged()
    {
        if (rdbAllCustomer.Checked == true)
        {
            tr_Customer.Visible = false;
            btnSave.Focus();
        }
        else
        {
            tr_Customer.Visible = true;
            txtCustomerID.Focus();
        }
        txtCustomerID.Text = "";
        lblCustomerName.Text = "";
    }
}