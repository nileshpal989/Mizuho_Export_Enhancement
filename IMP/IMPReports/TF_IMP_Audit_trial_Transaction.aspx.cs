using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class IMP_IMPReports_TF_IMP_Audit_trial_Transaction : System.Web.UI.Page
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
                //btnUserList.Attributes.Add("onclick", "return OpenUserList();");
                btnSave.Attributes.Add("onclick", "return Generate();");
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();

                ddlBranch.Enabled = true;



                rdbAlluser.Checked = true;
                Table1.Visible = false;



                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
            }





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
        li.Text = "All Branch";
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            //ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataValueField = "BranchName";

            ddlBranch.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    //protected void txtusername_TextChanged(object sender, EventArgs e)
    //{
    //    lbluserrole.Text = "";
    //    //string ingr = txtUserName.Text.Trim();
    //    string ingr = txtusername.Text.Trim();
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("userName", SqlDbType.VarChar);
    //    p1.Value = ingr;
    //    String query = "TF_cust_liability_GetAuditTrailFilldetails";
    //    DataTable dt = objData.getData(query, p1);

    //    if (dt.Rows.Count > 0)
    //    {
    //        //lbluserrole.Text = dt.Rows[0]["userRole"].ToString().Trim();

    //    }
    //    else
    //    {
    //        //lbluserrole.Text = "<font style='color:red;'>Invalid Customer id</font>";
    //        txtusername.Text = "";
    //        txtusername.Focus();
    //        //txtUserName.Text = "";
    //        //txtUserName.Focus();
    //    }
    //}
    protected void rdbAlluser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAlluser.Checked == true)
        {
            Table1.Visible = false;

            txtusername.Text = "";
            //txtUserName.Text = "";



        }
    }
    protected void rdbSelecteduser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSelecteduser.Checked == true)
        {
            Table1.Visible = true;
            rdbAlluser.Checked = false;



        }
    }
}