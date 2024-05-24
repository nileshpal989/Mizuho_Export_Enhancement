using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Configuration;
using System.Data;

public partial class Reports_EXPReports_rptExportDataStatusReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddlBranch.SelectedValue = Session["userLBCode"].ToString();            
            txtFromDate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Attributes.Add("onclick", "return validateGenerate()");
            fillddlBranch();
            txtFromDate.Attributes.Add("onblur", "isValidDate(" + txtFromDate.ClientID + "," + "' From Date.'" + ");");
            txtToDate.Attributes.Add("onblur", "isValidDate(" + txtToDate.ClientID + "," + "' To Date.'" + ");");
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
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            ddlBranch.Items.Insert(0, li);
        }
    }

    protected void rdbAllBranch_CheckedChanged(object sender, EventArgs e)
    {
        tblBranch.Visible = false;
    }
    protected void rdbSelectedBranch_CheckedChanged(object sender, EventArgs e)
    {
        tblBranch.Visible = true;
    }
}