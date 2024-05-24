using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;

public partial class Reports_EXPReports_Export_Report_XOSValid : System.Web.UI.Page
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
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
                txtFromDate.Focus();

            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'As On Date'" + " );");
            btnCreate.Attributes.Add("onclick", "return validateSave();");
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
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
        txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }

}