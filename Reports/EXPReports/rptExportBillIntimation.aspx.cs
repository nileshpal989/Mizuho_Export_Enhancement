using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPReports_rptExportBillIntimation : System.Web.UI.Page
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
            txtToDocumentNo.Attributes.Add("onkeydown", "return ToDocId(event)");
            btnDocList.Attributes.Add("onclick", "return dochelp()");
            btnToDocList.Attributes.Add("onclick", "return Todochelp()");

            if (!IsPostBack)
            {
                clearControls();
                rdbAllDocNo.Checked = true;
                ddlBranch.Focus();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillProcessingDate();

                txtFromDate.Attributes.Add("onkeypress", "return false;");
                txtFromDate.Attributes.Add("oncut", "return false;");
                txtFromDate.Attributes.Add("oncopy", "return false;");
                txtFromDate.Attributes.Add("onpaste", "return false;");
                txtFromDate.Attributes.Add("oncontextmenu", "return false;");
                btnSave.Attributes.Add("onclick", "return validateGenerate();");
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                rdbAllDocNo.Visible = true;
                rdbSelectedDocNo.Visible = true;

                Doccode.Visible = false;
                ToDoccode.Visible = false;
            }
        }
    }


    protected void btnDocId_Click(object sender, EventArgs e)
    {
        if (hdnDocId.Value != "")
        {
            txtDocumentNo.Text = hdnDocId.Value;

            txtDocumentNo.Focus();
        }
    }

    protected void btnToDocId_Click(object sender, EventArgs e)
    {
        if (hdnToDocId.Value != "")
        {
            txtToDocumentNo.Text = hdnToDocId.Value;

            txtToDocumentNo.Focus();
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
        //li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            li.Text = "All Branches";

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
        //txtToDate.Text = "";
        txtFromDate.Focus();
        rdbAllDocNo.Checked = true;
    }

    protected void btnChangeDate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime newDate = new DateTime();

        string frmDate = "";
        string toDate = "";

        if (txtFromDate.Text != "")
        {
            newDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            frmDate = newDate.ToString("dd/MM/yyyy");
            //hdnFromDate.Value = frmDate.Trim();
        }

        if (txtFromDate.Text == "")
        {
            //txtToDate.Text = "";
        }
        else
        {
            DateTime nextDate = new DateTime();

            nextDate = System.DateTime.Now;

            toDate = nextDate.ToString("dd/MM/yyyy");

        }
    }


    protected void fillProcessingDate()
    {
        if (Session["startdate"] != null)
        {
            txtFromDate.Text = Session["startdate"].ToString();
            hdnFromDate.Value = Session["startdateMM"].ToString();
        }


    }

    protected void rdbAllDocNo_CheckedChanged(object sender, EventArgs e)
    {


        rdbAllDocNo.Checked = true;
        Doclist.Visible = false;
        rdbAllDocNo.Focus();
    }
    protected void rdbSelectedDocNo_CheckedChanged(object sender, EventArgs e)
    {

        Doclist.Visible = true;
        txtDocumentNo.Text = "";
        txtToDocumentNo.Text = "";

        rdbSelectedDocNo.Focus();
        txtDocumentNo.Visible = true;
        btnDocList.Visible = true;
        txtToDocumentNo.Visible = true;
        btnToDocList.Visible = true;
        rdbSelectedDocNo.Focus();
        Doccode.Visible = true;
        ToDoccode.Visible = true;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {

        txtDocumentNo.Focus();
    }

    protected void txtToDocumentNo_TextChanged(object sender, EventArgs e)
    {
        txtToDocumentNo.Focus();
    }
}