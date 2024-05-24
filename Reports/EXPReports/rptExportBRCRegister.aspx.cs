using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class Reports_EXPReports_rptExportBRCRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnFDocHelp.Attributes.Add("onclick", "return OpenDocList()");
            btnTDocHelp.Attributes.Add("onclick", "return OpenDocList1()");
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Attributes.Add("onclick", "return validateSave()");
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

    protected void rdbAllDocumentNo_CheckedChanged(object sender, EventArgs e)
    {
        tblDocNo.Visible = false;
    }
    protected void rdbSelectedDocumentNo_CheckedChanged(object sender, EventArgs e)
    {
        tblDocNo.Visible = true;
        txtFDocNo.Text = "";
        txtTDocNo.Text = "";
    }
}