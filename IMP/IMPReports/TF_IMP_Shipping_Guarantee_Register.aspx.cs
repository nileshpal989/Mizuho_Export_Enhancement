using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using System.Web.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.DataValidation;
using System.Drawing;

public partial class IMP_IMPReports_TF_IMP_Shipping_Guarantee_Register : System.Web.UI.Page
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
                //ddlBranch.Enabled = false;
                fillBranch();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
            btnCreate.Attributes.Add("onclick", "return validateSave();");

        }
    }
    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
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
        {
            li.Text = "No record(s) found";
        }
        ddlBranch.Items.Insert(0, li);
    }
    protected void btndownload_Click(object sender, EventArgs e)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@FROMDATE", SqlDbType.VarChar);
        P1.Value = txtFromDate.Text.Trim();

        SqlParameter P2 = new SqlParameter("@TODATE", SqlDbType.VarChar);
        P2.Value = txtToDate.Text.Trim();

        SqlParameter P3 = new SqlParameter("@BRANCHCODE", SqlDbType.VarChar);
        P3.Value = ddlBranch.SelectedValue.ToString();


        DataTable dt = objData1.getData("TF_IMP_Shipping_Guarantee_Register", P1, P2, P3);
        string Todaytime = DateTime.Now.ToString("dd-MM-yyyy hhmmss");
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Reports");

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string filename = "";
            filename = "TF_IMP_SG_Register-" + Todaytime + ".xlsx";

            string _filePath = _directoryPath + "/" + filename;

            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt, "Worksheet");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                }
                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();// use to get pc name of the server
                string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/Reports/";
                string link = "/TF_GeneratedFiles/IMPORT/Reports/" + filename;
                labelMessage.Text = "Files Created in " + "<a href=" + path + "></a>";
                string filePath = "~/TF_GeneratedFiles/IMPORT/Reports/" + filename;
                // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('File Created Successfully on " + _serverName + " in " + path + "')", true);
                exceldownload(filename, filePath);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There are No records for this period.')", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Message", "VAlert('There are No records for this period','#fromDate');", true);
            txtFromDate.Focus();
        }
    }
    public void exceldownload(string filename, string filePath)
    {
        Response.ContentType = ".xlsx";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
}