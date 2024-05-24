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

public partial class IMP_FileCreation_BankDataDump : System.Web.UI.Page
{
    string s = "";
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

            }
            btnCreate.Attributes.Add("onclick", "return validateSave();");

        }
    }
    protected void btndownload_Click(object sender, EventArgs e)
    {
        string IMPCheck = "";
        if (rdbIMPBank.Checked == true)
        {
            IMPCheck = "Y";
        }
        else
        {
            IMPCheck = "N";
        }

        TF_DATA objData1 = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@IMPCheck", SqlDbType.VarChar);
        P1.Value = IMPCheck;


        DataTable dt = objData1.getData("TF_BankDataDump", P1);
        string Date = DateTime.Now.ToString("ddMMyyyy");
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/FileCreation");

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string filename = "";
            if (rdbIMPBank.Checked == true)
            {
                filename = "Import overseas bank_" + Date + ".xlsx";
            }
            else if (rdbotherBank.Checked == true)
            {
                filename = "Other than import overseas bank_" + Date + ".xlsx";
            }
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
                string filePath = "~/TF_GeneratedFiles/IMPORT/FileCreation/" + filename;
                // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('File Created Successfully on " + _serverName + " in " + path + "')", true);
                exceldownload(filename, filePath);
            }
        }
        else
        {
            // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There are No records for this period.')", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Message", "VAlert('There are No records for this period','#rdbAllCustomer');", true);
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