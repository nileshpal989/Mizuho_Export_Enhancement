using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Reports_XOSReports_XOS_rptEBWRegister : System.Web.UI.Page
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
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
                
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();

                txtCustomer.Attributes.Add("onkeydown", "return CustId(event);");
                btCustList.Attributes.Add("onclick", "return Custhelp();");
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                rdbAllCustomer.Attributes.Add("onclick", "return toogleDisplay();");
                rdbSelectedCustomer.Attributes.Add("onclick", "return toogleDisplay();");
                btnSave.Attributes.Add("onclick", "return Generate();");

                rdbAllCustomer.Visible = true;
                rdbAllCustomer.Checked = true;
                rdbSelectedCustomer.Visible = true;
 
            }
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtfromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
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
    public void CustomerIdDescription()
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

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        CustomerIdDescription();
        txtCustomer.Focus();
    }
}