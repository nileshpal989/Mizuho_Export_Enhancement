using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Reports_EXPORTReports_EXP_rptPrinting : System.Web.UI.Page
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
            labelMessage.Text = "";
            txtDocumentNo.Attributes.Add("onkeydown", "return DocId(event)");
            txtToDocumentNo.Attributes.Add("onkeydown", "return ToDocId(event)");
            btnDocList.Attributes.Add("onclick", "return dochelp()");
            btnToDocList.Attributes.Add("onclick", "return Todochelp()");
            if (!IsPostBack)
            {
                clearControls();
                rdbAllDocNo.Checked = true;
                txtFromDate.Focus();
                fillProcessingDate();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                //txtFromDate.Attributes.Add("onkeypress", "return false;");
                //txtFromDate.Attributes.Add("oncut", "return false;");
                //txtFromDate.Attributes.Add("oncopy", "return false;");
                //txtFromDate.Attributes.Add("onpaste", "return false;");
                //txtFromDate.Attributes.Add("oncontextmenu", "return false;");
                //txtFromDate.Attributes.Add("onblur", "toDate();");
                btnSave.Attributes.Add("onclick", "return validateGenerate();");
                Doccode.Visible = false;
            }
        }
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";
        labelMessage.Text = "";
        rdbAllDocNo.Focus();
        rdbSelectedDocNo.Checked = false;
        txtDocumentNo.Visible = false;
        btnDocList.Visible = false;
        txtToDocumentNo.Visible = false;
        btnToDocList.Visible = false;
        Doccode.Visible = false;
        ToDoccode.Visible = false;
        txtDocumentNo.Text = "";
        txtToDocumentNo.Text = "";
    }

    protected void fillProcessingDate()
    {
        if (Session["startdate"] != null)
        {
            txtFromDate.Text = Session["startdate"].ToString();
            hdnFromDate.Value = Session["startdateMM"].ToString();
        }


    }

    //public void fillDocIdDescription()
    //{
    //    lblDocName.Text = "";
    //    System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
    //    dateInfo1.ShortDatePattern = "dd/MM/yyyy";
    //    DateTime documentDate1 = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo1);
    //    // string DocDate = txtFromDate.Text.Trim();


    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@DocDate", SqlDbType.VarChar);
    //    string _DocDT = documentDate1.ToString("MM/dd/yyyy");
    //    p1.Value = _DocDT;
    //    string _query = "TF_INW_getDocNoDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblDocName.Text = dt.Rows[0]["BankName"].ToString().Trim();
    //    }
    //    else
    //    {
    //        txtDocumentNo.Text = "";
    //        lblDocName.Text = "";
    //    }
    //}

    protected void btnDocId_Click(object sender, EventArgs e)
    {
        if (hdnDocId.Value != "")
        {
            txtDocumentNo.Text = hdnDocId.Value;
            //fillDocIdDescription();
            txtDocumentNo.Focus();
        }
    }

    protected void btnToDocId_Click(object sender, EventArgs e)
    {
        if (hdnToDocId.Value != "")
        {
            txtToDocumentNo.Text = hdnToDocId.Value;
            //fillToDocIdDescription();
            txtToDocumentNo.Focus();
        }
    }

    protected void rdbAllDocNo_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllDocNo.Checked = true;
        rdbAllDocNo.Focus();
        rdbSelectedDocNo.Checked = false;
        txtDocumentNo.Visible = false;
        btnDocList.Visible = false;
        Doccode.Visible = false;
        txtToDocumentNo.Visible = false;
        btnToDocList.Visible = false;
        ToDoccode.Visible = false;
        txtDocumentNo.Text = "";
        txtToDocumentNo.Text = "";
    }
    protected void rdbSelectedDocNo_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedDocNo.Checked = true;
        rdbSelectedDocNo.Focus();
        txtDocumentNo.Visible = true;
        btnDocList.Visible = true;
        Doccode.Visible = true;
        txtToDocumentNo.Visible = true;
        btnToDocList.Visible = true;
        ToDoccode.Visible = true;
        rdbAllDocNo.Checked = false;

    }

    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {
        //fillDocIdDescription();
        txtDocumentNo.Focus();
        //lblDocName.Visible = true;
    }

    protected void txtToDocumentNo_TextChanged(object sender, EventArgs e)
    {
        //fillDocIdDescription();
        txtToDocumentNo.Focus();
        //lblDocName.Visible = true;
    }
}