using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using OfficeOpenXml;
using Org.BouncyCastle.Crypto.Generators;

public partial class EBR_TF_EBRC_ORM_FileUpload : System.Web.UI.Page
{
    int norecinexcel, cntrec;
    string fname;
    string result;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedUserId"] == null)
        {
            Response.Redirect("~/TF_Log_out.aspx?sessionout=yes&sessionid=" + "", true);
        }
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                fillBranch();
            }
            btnupload.Attributes.Add("onclick", "return  ShowProgress();");
            btnValidate.Attributes.Add("onclick", "return  ShowProgressValidate();");
            btnProcess.Attributes.Add("onclick", "return  ShowProgressProcess();");
        }
    }
    protected void fillBranch()
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
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
            ddlBranch.Items.Insert(0, li);
            ddlBranch.SelectedIndex = 1;
            ddlBranch_SelectedIndexChanged(null, null);
            ddlBranch.Focus();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        string result = "", _query = ""; lblHint.Text = "";
        TF_DATA objdata = new TF_DATA();
        _query = "TF_EBRC_ORM_Delete_Tempdata";
        DataTable res = objdata.getData(_query);
        DataTable Delenotupload = objdata.getData(_query);

        if (FileUpload1.HasFile)
        {
            string fname;
            fname = FileUpload1.FileName;
            txtInputFile.Text = FileUpload1.PostedFile.FileName;

            if (fname.Contains(".xls") == false && fname.Contains(".xlsx") == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please upload only excel file.')", true);
                FileUpload1.Focus();
            }
            else
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = Server.MapPath("~/Uploaded_Files");

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                FileName = FileName.Replace(" ", "");
                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(FilePath);
                GetExcelSheets(FilePath, Extension, "No");
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please upload File First.')", true);
        }
    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string conStr = "";
            labelMessage.Text = "";
            int dt1count = 0; int dtcount = 0; int noofrecinexcel = 0;
            string Branch = "", bankuniquetxid = "", ORMIssuedate = "", ORMNo = "", ORMStatus = "", ifsccode = "", adcode = "",
                paymentdate = "", ORNFcc = "", ORNFCCAmt = "", INRpayableAMT = "", exchangerate = "", ieccode = "", panno = "", benefname = "", benefcontry = "",
                purposecode = "", paymentmode = "", refIRM = "";
            int errorcount = 0;
            conStr = String.Format(FilePath);
            using (var package = new ExcelPackage(new FileInfo(conStr)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet != null)
                {
                    // Get the dimensions of the worksheet
                    var startCell = worksheet.Dimension.Start;
                    var endCell = worksheet.Dimension.End;

                    // Loop through rows and columns to find non-empty data

                    for (int row = 2; row <= endCell.Row; row++)
                    {
                        bool isEmptyRow = true;

                        for (int col = startCell.Column; col <= endCell.Column; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Text;

                            // Check if the cell is not empty or null
                            if (!string.IsNullOrWhiteSpace(cellValue))
                            {
                                isEmptyRow = false;
                                break;
                            }
                        }
                        // If the row is not empty, you can process it here
                        if (!isEmptyRow)
                        {
                            SqlParameter BranchCode = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                            SqlParameter SRNO = new SqlParameter("@SRNO", SqlDbType.VarChar);
                            SqlParameter BANK_UNIQUE_TXID = new SqlParameter("@BkUniqueTxId", SqlDbType.VarChar);
                            SqlParameter ORM_ISSUE_DATE = new SqlParameter("@ORMIssueDate", SqlDbType.VarChar);
                            SqlParameter ORMNO = new SqlParameter("@ORMNo", SqlDbType.VarChar);
                            SqlParameter ORMSTATUS = new SqlParameter("@ORMStatus", SqlDbType.VarChar);
                            SqlParameter IFSCCODE = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                            SqlParameter ADCODE = new SqlParameter("@ADCode", SqlDbType.VarChar);
                            SqlParameter PAYMENTDATE = new SqlParameter("@PaymentDate", SqlDbType.VarChar);
                            SqlParameter ORNFCC = new SqlParameter("@ORNFCC", SqlDbType.VarChar);
                            SqlParameter ORNFCCAMT = new SqlParameter("@ORNFCCAmt", SqlDbType.VarChar);
                            SqlParameter INRPAYABLEAMT = new SqlParameter("@INRPayableAmt", SqlDbType.VarChar);
                            SqlParameter EXCHANGERATE = new SqlParameter("@ExchangeRate", SqlDbType.VarChar);
                            SqlParameter IECCODE = new SqlParameter("@IECCode", SqlDbType.VarChar);
                            SqlParameter PANNO = new SqlParameter("@PanNo", SqlDbType.VarChar);
                            SqlParameter BENEFNAME = new SqlParameter("@BenefName", SqlDbType.VarChar);
                            SqlParameter BENEFCOUNTRY = new SqlParameter("@BenefCountry", SqlDbType.VarChar);
                            SqlParameter PURPOSECODE = new SqlParameter("@PurposeCode", SqlDbType.VarChar);
                            SqlParameter MODEOFPAYMENT = new SqlParameter("@ModeOfPayment", SqlDbType.VarChar);
                            SqlParameter REF_IRM = new SqlParameter("@RefIRM", SqlDbType.VarChar);
                            SqlParameter ADD_DATE = new SqlParameter("@ADD_DATE", SqlDbType.VarChar);
                            SqlParameter ADD_USER = new SqlParameter("@ADD_USER", SqlDbType.VarChar);

                            string _query = "TF_EBRC_ORM_TempData_Upload";
                            noofrecinexcel++;
                            SRNO.Value = "";
                            bankuniquetxid = worksheet.Cells[row, 1].Text;
                            ORMIssuedate = worksheet.Cells[row, 2].Text;
                            ORMNo = worksheet.Cells[row, 3].Text;
                            ORMStatus = worksheet.Cells[row, 4].Text;
                            ifsccode = worksheet.Cells[row, 5].Text;
                            adcode = worksheet.Cells[row, 6].Text;
                            paymentdate = worksheet.Cells[row, 7].Text;
                            ORNFcc = worksheet.Cells[row, 8].Text;
                            ORNFCCAmt = worksheet.Cells[row, 9].Text;
                            INRpayableAMT = worksheet.Cells[row, 10].Text;
                            exchangerate = worksheet.Cells[row, 11].Text;
                            ieccode = worksheet.Cells[row, 12].Text;
                            panno = worksheet.Cells[row, 13].Text;
                            benefname = worksheet.Cells[row, 14].Text;
                            benefcontry = worksheet.Cells[row, 15].Text;
                            purposecode = worksheet.Cells[row, 16].Text;
                            paymentmode = worksheet.Cells[row, 17].Text;
                            refIRM = worksheet.Cells[row, 18].Text;

                            string qry;
                            TF_DATA objdata1 = new TF_DATA();
                            qry = "TF_EBRC_FileUpload_Get_Valid_Branch";

                            SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                            DataTable dt2 = objdata1.getData(qry, p2);

                            if (dt2.Rows.Count > 0)
                            {
                                hdnbranchcode.Value = dt2.Rows[0]["BranchCode"].ToString();
                                BranchCode.Value = hdnbranchcode.Value;
                                BANK_UNIQUE_TXID.Value = bankuniquetxid;
                                ORM_ISSUE_DATE.Value = ORMIssuedate;
                                ORMNO.Value = ORMNo;
                                ORMSTATUS.Value = ORMStatus;
                                IFSCCODE.Value = ifsccode;
                                ADCODE.Value = adcode;
                                PAYMENTDATE.Value = paymentdate;
                                ORNFCC.Value = ORNFcc;
                                ORNFCCAMT.Value = ORNFCCAmt;
                                INRPAYABLEAMT.Value = INRpayableAMT;
                                EXCHANGERATE.Value = exchangerate;
                                IECCODE.Value = ieccode;
                                PANNO.Value = panno;
                                BENEFNAME.Value = benefname;
                                BENEFCOUNTRY.Value = benefcontry;
                                PURPOSECODE.Value = purposecode;
                                MODEOFPAYMENT.Value = paymentmode;
                                REF_IRM.Value = refIRM;

                                ADD_USER.Value = Session["UserName"].ToString();
                                ADD_DATE.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                                TF_DATA objSave = new TF_DATA();
                                result = objSave.SaveDeleteData(_query, BranchCode, SRNO, BANK_UNIQUE_TXID, ORM_ISSUE_DATE, ORMNO, ORMSTATUS, IFSCCODE, ADCODE,
                                    PAYMENTDATE, ORNFCC, ORNFCCAMT, INRPAYABLEAMT, EXCHANGERATE, IECCODE, PANNO, BENEFNAME, BENEFCOUNTRY, PURPOSECODE, MODEOFPAYMENT, REF_IRM, ADD_USER, ADD_DATE);
                                

                                if (result == "Uploaded")
                                {
                                    dtcount = dtcount + 1;
                                }
                                else
                                {
                                    string result1 = objSave.SaveDeleteData("TF_EBRC_ORM_FileUploadLog", BranchCode, SRNO, BANK_UNIQUE_TXID, ORM_ISSUE_DATE, ORMNO, ORMSTATUS, IFSCCODE, ADCODE,
                                    PAYMENTDATE, ORNFCC, ORNFCCAMT, INRPAYABLEAMT, EXCHANGERATE, IECCODE, PANNO, BENEFNAME, BENEFCOUNTRY, PURPOSECODE, MODEOFPAYMENT, REF_IRM, ADD_USER, ADD_DATE);
                                    dt1count = dt1count + 1;
                                }
                                if (lblHint.Text == "")
                                {
                                    if (errorcount > 0)
                                    {
                                        lblHint.Text = "<font color='red'>" + "Please Correct All Errors." + "</font>";
                                        string script = "window.open('View_EBRC_NotUploaded.aspx?','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "popup", script, true);
                                    }
                                }
                            }
                        }
                    }
                    if (dtcount == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('File Aborted.');", true);
                    }
                    if (dtcount != noofrecinexcel)
                    {
                        string record = dt1count + " records not uploaded out of " + noofrecinexcel + " records. ";
                        //string record = dtcount + " records uploaded out of " + noofrecinexcel + " records from the file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "VAlert('" + record+ "');", true);                        
                    }
                    else
                    {
                        string record = dtcount + " records uploaded out of " + noofrecinexcel + " records from the file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + record + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            labelMessage.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "alert('Upload Correct File Format.');", true);
            this.LogError(ex);
        }        
    }
    private void LogError(Exception ex)
    {
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", ex.Message);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", ex.StackTrace);
        message += Environment.NewLine;
        message += string.Format("Source: {0}", ex.Source);
        message += Environment.NewLine;
        message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        string path = Server.MapPath("~/TF_GeneratedFiles/ErrorLog.txt");
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (txtInputFile.Text != "")
        {
            if (lblHint.Text == "")
            {
                hdnhiide.Value = "1";
                TF_DATA objdata = new TF_DATA();
                string script = "";
                SqlParameter p1 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text.ToString());
                DataTable dt = objdata.getData("TF_EBRC_ORM_Validate", p1);

                if (dt.Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(5000);
                    btnProcess.Enabled = false;
                    lblHint.Text = "<font color='red'>" + "Please Correct All Errors Then You Can Process Data.." + "</font>";
                    string result = objdata.SaveDeleteData("TF_EBRC_ORM_Temp_Invalid_RowCount", p1);
                    string[] splitresult = result.Split('/');
                    string record = splitresult[0] + " Errors Found In Input File.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + record + " Please click on OK to download error report file.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);
                }
                else
                {
                    script = "No Error Records.";                 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + script + "');", true);
                    lblHint.Text = "";
                    btnProcess.Enabled = true;
                }
            }
            else
            {
                lblHint.Text = lblHint.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('Please Correct All Errors.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('Please Upload File First.');", true);
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (txtInputFile.Text != "")
        {
            if (lblHint.Text == "")
            {
                if (hdnhiide.Value == "1")
                {
                    System.Threading.Thread.Sleep(5000);
                    SqlParameter BName = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                    SqlParameter FileName = new SqlParameter("@FileName", txtInputFile.Text.Trim());
                    SqlParameter UserName = new SqlParameter("@UserName", Session["userName"].ToString().Trim());
                    TF_DATA objdata = new TF_DATA();
                    string result = objdata.SaveDeleteData("TF_EBRC_ORM_Process", BName, UserName, FileName);

                    if (result.Substring(0, 8) == "Uploaded")
                    {
                        string record = result.Substring(8) + " Valid Records Processed Successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + record + "');", true);
                    }
                    else if (result == "Records Alredy Exists")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + result + "');", true);
                    }
                    else
                    {
                        labelMessage.Text = " <font color='red'>" + "0 " + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records processed ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('No Records Processed.');", true);
                    }
                    hdnhiide.Value = "0";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('First Validate The File.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('Please Correct All Errors Then You Can Process Data..');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('Please Upload File First.');", true);
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "TF_EBRC_ORM_Validate";
        SqlParameter p1 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
        DataTable dt = objdata.getData(script, p1);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Error_Records");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Data_Validation.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btnrecordnotuploaded_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "TF_EBRC_ORM_FilenotuploadedAudit";
        SqlParameter p1 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
        DataTable dt = objdata.getData(script, p1);
        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Error_Records");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Data_Validation.xlsx");
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
}