using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_EXPORTReports_TF_EXP_Realisation_Excel_Report : System.Web.UI.Page
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
                //ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btngenerate.Attributes.Add("onclick", "return ShowProgress()");
                fillddlBranch();
                txtFromDate.Focus();
                txtFromDate.Attributes.Add("onblur", "isValidDate(" + txtFromDate.ClientID + "," + "' From Date.'" + ");");
                txtToDate.Attributes.Add("onblur", "isValidDate(" + txtToDate.ClientID + "," + "' To Date.'" + ");");

            }
        }
    }
    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        System.Data.DataTable dt = objData.getData(_query, p1);
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
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string _result = "";

        TF_DATA objdata = new TF_DATA();
        SqlParameter branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        branch.Value = ddlBranch.SelectedItem.Text;

        SqlParameter fromshippingdate = new SqlParameter("@startdate", SqlDbType.VarChar);
        fromshippingdate.Value = txtFromDate.Text.Trim();

        SqlParameter Toshippingdate = new SqlParameter("@enddate", SqlDbType.VarChar);
        Toshippingdate.Value = txtToDate.Text.Trim();

        string _query = "TF_rptExpRealisationReport_Excel";
        TF_DATA objSave = new TF_DATA();
        System.Data.DataTable dt = objdata.getData(_query, branch, fromshippingdate, Toshippingdate);
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);
        }
        else {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('No Records.')", true);
        }
    }
    protected void btnGeneratereport_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "TF_rptExpRealisationReport_Excel";
        SqlParameter branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        branch.Value = ddlBranch.Text.Trim();

        SqlParameter fromshippingdate = new SqlParameter("@startdate", SqlDbType.VarChar);
        fromshippingdate.Value = txtFromDate.Text.Trim();

        SqlParameter Toshippingdate = new SqlParameter("@enddate", SqlDbType.VarChar);
        Toshippingdate.Value = txtToDate.Text.Trim();
        System.Data.DataTable dt = objdata.getData(script, branch, fromshippingdate, Toshippingdate);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Export Realisation Report");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Export Realisation Report.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}