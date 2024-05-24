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

public partial class Reports_XOSReports_XOS_Report : System.Web.UI.Page
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
                //ddlBranch.Focus();
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;


            }
          txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'As On Date'" + " );");

          //  btnCreate.Attributes.Add("onclick", "return validateSave();");
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
        //ddlBranch.Focus();
        labelMessage.Text = "";
        txtFromDate.Focus();
        //      txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }


    protected void btnCreate_Click(object sender, EventArgs e)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter p = new SqlParameter("@startdate", SqlDbType.VarChar);
        p.Value = txtFromDate.Text.ToString().Trim();
        string _qry1 = "tf_EXPORT_Report_XOS_getcurrencyCardMaster";
        DataTable dt1 = objData1.getData(_qry1, p);
        if (dt1.Rows.Count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", " validateSave();", true);
                    
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Add Cross Currency Rate Master for a " + txtFromDate.Text.ToString() + "' );", true);
        }
    }
}