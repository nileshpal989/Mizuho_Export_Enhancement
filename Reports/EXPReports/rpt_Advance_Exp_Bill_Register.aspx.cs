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

public partial class Reports_EXPReports_rpt_Advance_Exp_Bill_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlBranch.SelectedValue = Session["userLBCode"].ToString();
            ddlBranch.Enabled = false;
            //txtFromDate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnDocList.Attributes.Add("onclick", "return dochelp();");
            //  txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");                      
            btnSave.Attributes.Add("onclick", "return validateGenerate()");
            fillddlBranch();
            txtFromDate.Focus();
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
        ListItem li = new ListItem();
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void rdbAllDocNo_CheckedChanged(object sender, EventArgs e)
    {
        tblDocNo.Visible = false;
    }
    protected void rdbSelectedDocNo_CheckedChanged(object sender, EventArgs e)
    {
        tblDocNo.Visible = true;
        txtDocumentNo.Text = "";
    }
}