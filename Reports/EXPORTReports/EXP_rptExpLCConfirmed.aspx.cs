using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPORTReports_EXP_rptExpLCConfirmed : System.Web.UI.Page
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

            txtDocumentNo.Attributes.Add("onkeydown", "return DocId(event)");
            btnDocList.Attributes.Add("onclick", "return dochelp()");
            if (!IsPostBack)
            {
                clearControls();
                rdbAllDocNo.Checked = true;
                ddlBranch.Focus();
                //txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                //txtFromDate.Attributes.Add("onkeypress", "return false;");
                //txtFromDate.Attributes.Add("oncut", "return false;");
                //txtFromDate.Attributes.Add("oncopy", "return false;");
                //txtFromDate.Attributes.Add("onpaste", "return false;");
                //txtFromDate.Attributes.Add("oncontextmenu", "return false;");
                btnSave.Attributes.Add("onclick", "validateSave();");
                //txtFromDate.Attributes.Add("onblur", "toDate();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                fillddlBranch();
                Doccode.Visible = false;
            }
        }
    }
    //public void fillDocIdDescription()
    //{
    //    lblDocName.Text = "";
    //    string DocDate = txtFromDate.Text.Trim();
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@DocDate", SqlDbType.DateTime);
    //    p1.Value = Convert.ToDateTime(txtFromDate.Text);
    //    string _query = "TF_PC_getDocNoDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblDocName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
    //    }
    //    else
    //    {
    //        txtDocumentNo.Text = "";
    //        lblDocName.Text = "";
    //    }
    //}

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
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }

    //protected void btnDocId_Click(object sender, EventArgs e)
    //{
    //    if (hdnDocId.Value != "")
    //    {
    //        txtDocumentNo.Text = hdnDocId.Value;
    //        //fillDocIdDescription();
    //        txtDocumentNo.Focus();
    //    }
    //}

    protected void clearControls()
    {
        //txtFromDate.Text = "";
        //txtToDate.Text = "";
        //txtFromDate.Focus();
        rdbAllDocNo.Checked = true;
    }

    protected void rdbAllDocNo_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllDocNo.Checked = true;
        rdbAllDocNo.Focus();
        rdbSelectedDocNo.Checked = false;
        txtDocumentNo.Visible = false;
        btnDocList.Visible = false;
        //lblDocName.Visible = false;

        txtDocumentNo.Text = "";
        //lblDocName.Text = "";
        Doccode.Visible = false;
    }
    protected void rdbSelectedDocNo_CheckedChanged(object sender, EventArgs e)
    {
        Doccode.Visible = true;
        rdbSelectedDocNo.Checked = true;
        rdbSelectedDocNo.Focus();
        txtDocumentNo.Visible = true;
        btnDocList.Visible = true;
        //lblDocName.Visible = true;
        rdbAllDocNo.Checked = false;

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {
        txtDocumentNo.Focus();
    }
}