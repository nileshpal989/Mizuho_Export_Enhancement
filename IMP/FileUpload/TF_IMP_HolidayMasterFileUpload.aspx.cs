using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

public partial class IMP_FileUpload_TF_IMP_HolidayMasterFileUpload : System.Web.UI.Page
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

            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(10000);
        label2.Text = "";
        lblHint.Text = "";
        TF_DATA objData = new TF_DATA();
        DataTable dt = new DataTable();

        if (fileinhouse.HasFile)
        {
            string fname;
            fname = fileinhouse.FileName;
            //string path = Server.MapPath(fileinhouse.PostedFile.FileName);
            txtInputFile.Text = fileinhouse.PostedFile.FileName;
            if (fname.Contains(".xls") == true || fname.Contains(".xlsx") == true || fname.Contains(".XLS") == true || fname.Contains(".XLSX") == true)
            {
                string FileName = Path.GetFileName(fileinhouse.PostedFile.FileName);
                string Extension = Path.GetExtension(fileinhouse.PostedFile.FileName);
                string FolderPath = Server.MapPath("../Uploaded_Files");

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                FileName = FileName.Replace(" ", "");

                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                fileinhouse.SaveAs(FilePath);
                GetExcelSheets(FilePath, Extension, "No");
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Excel File First.')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "Alert('Please Select Excel File First.')", true);
        }
    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            string conStr = "";
            int norecinexcel = 0;
            int cnt = 0;
            int cntTot = 0;
            int errorcount = 0;

            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
                case ".XLS": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".XLSX": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            connExcel.Open();

            cmdExcel.CommandText = "SELECT F1,F2,FORMAT(F3,'dd/MM/yyyy') as F3,F4 FROM [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            int RowCount = dt.Rows.Count;
            if (dt.Rows.Count > 1)
            {
                for (int i = 1; i < RowCount; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() != "")
                    {
                        norecinexcel = norecinexcel + 1;
                        SqlParameter PCurrency = new SqlParameter("@Currency",dt.Rows[i][0]);
                        SqlParameter PADCode= new SqlParameter("@ADCode", dt.Rows[i][1]);
                        SqlParameter PDate = new SqlParameter("@Date", dt.Rows[i][2]);
                        SqlParameter PDesc = new SqlParameter("@Desc", dt.Rows[i][3]);
                        SqlParameter PAddedBy = new SqlParameter("@AddedBy", Session["userName"].ToString());
                        string Result = objData.SaveDeleteData("TF_IMP_UploadHolidayMaster", PCurrency, PADCode, PDate, PDesc, PAddedBy);
                        if (Result == "Inserted")
                        {
                            cnt++;
                        }
                        else
                        {
                            
                        }
                        cntTot++;
                    }
                }
                labelMessage.Text = "<font color='red'>" + cnt + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records Uploaded out of " + "<font color='red'>" + cntTot + "</font>" + " from file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                string record = cnt + " records uploaded out of " + cntTot + " records from the file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "uploadfile", "VAlert('" + record + "')", true);
            }
        }
        catch (Exception Ex)
        {
            labelMessage.Text = Ex.Message;
        }
    }
}