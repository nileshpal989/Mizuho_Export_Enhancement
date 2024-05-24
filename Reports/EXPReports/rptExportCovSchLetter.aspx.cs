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

public partial class Reports_EXPORTReports_rptExportCovSchLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDocList.Attributes.Add("onclick", "return dochelp()");
            btnToDocList.Attributes.Add("onclick", "return dochelp1()");
            txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Attributes.Add("onclick", "return validateGenerate()");
            btnCustList.Attributes.Add("onclick", "return Custhelp()");


            fillddlBranch();
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
            ddlBranch.DataValueField = "BranchName";
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
        txtToDocumentNo.Text = "";
    }
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = false;
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = true;
        txtCustomerID.Text = "";
    }
}