using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPORTReports_EXP_LCGiventoFirstFlight : System.Web.UI.Page
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
                clearControls();
                fillddlBranch();
                txtFromDate.Attributes.Add("onblur", "toDate();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
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
        li01.Value = "1";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            li01.Text = "ALL BRANCHES";

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
        ddlBranch.Focus();
        txtFromDate.Text = "";
        txtToDate.Text = "";

        //rdbAllBranch.Checked = true;
    }

    //protected void rdbSelectedBranch_CheckedChanged(object sender, EventArgs e)
    //{
    //    SelectBranch.Visible = true;
    //    fillddlBranch();
    //}
    //protected void rdbAllBranch_CheckedChanged(object sender, EventArgs e)
    //{
    //    SelectBranch.Visible = false;
    //    ddlBranch.Items.Clear();
    //}
}