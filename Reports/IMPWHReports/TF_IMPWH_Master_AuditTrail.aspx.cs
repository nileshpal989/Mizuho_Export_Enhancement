using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_IMPWHReports_TF_IMPWH_Master_AuditTrail : System.Web.UI.Page
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
                fillUserList();

                btnSave.Attributes.Add("onclick", "return Generate();");
                btnUserList.Attributes.Add("onclick", "return OpenUserList();");
                txtUserName.Attributes.Add("onblur", "return checkUser();");
                //PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                //hdnModule.Value = Request.QueryString["ModuleType"].ToString();

                //rdbAllCustomer.Attributes.Add("onclick", "return toogleDisplay();");
                //rdbSelectedCustomer.Attributes.Add("onclick", "return toogleDisplay();");
            }
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddlBranch.Focus();
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
        }
    }
    protected void fillUserList()
    {
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_UserList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddlUserList.DataSource = dt.DefaultView;
            ddlUserList.DataTextField = "userName";
            ddlUserList.DataValueField = "userName";
            ddlUserList.DataBind();
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
        //ListItem li = new ListItem();
        //li.Value = "0";
        //ListItem li01 = new ListItem();
        //li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            //li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else { }
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);
        //ddlBranch.Items.Insert(1, li01);

    }
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = false;
        rdbAllCustomer.Checked = true;
        divUser.Visible = false;
        txtUserName.Text = "";

    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = true;
        rdbAllCustomer.Checked = false;
        divUser.Visible = true;
    }
}