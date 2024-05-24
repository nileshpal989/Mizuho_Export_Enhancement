using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using OfficeOpenXml;

public partial class EBR_TF_EBRC_ITTEUCFileUpload : System.Web.UI.Page
{
    int norecinexcel, cntrec, newsrno, newsrno1, srcnt;
    string result;    
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
                btnProcess.Enabled = false;
                btnValidate.Enabled = false;
                fillBranch();
                ddlBranch.SelectedIndex = 1;
                ddlBranch.SelectedValue = Session["userADCode"].ToString();                                
                btnupload.Attributes.Add("onclick", "return  ShowProgress();");
                btnValidate.Attributes.Add("onclick", "return  ShowProgressValidate();");
                btnProcess.Attributes.Add("onclick", "return  ShowProgressProcess();");
            }
        }
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        LBLCOUNT.Text = "";
        lblValMsg.Text = "";
        string result = "", _query = "";

        TF_DATA objdata = new TF_DATA();
        SqlParameter Branch = new SqlParameter("@Branch", ddlBranch.SelectedValue.ToString());
        SqlParameter Addedby = new SqlParameter("@addedBy", Session["userName"].ToString());
        _query = "TF_EBRC_ITTEUC_UPLOAD_Delete";
        result = objdata.SaveDeleteData(_query, Branch, Addedby);
        if (FileUpload1.HasFile)
        {
            string fname;
            fname = FileUpload1.FileName;
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
                txtInputFile.Text = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";

        SqlParameter Branch = new SqlParameter("@BranchName", ddlBranch.SelectedValue);
        SqlParameter user = new SqlParameter("@user", Session["userName"].ToString());
        DataTable dt = objdata.getData("TF_EBRC_ITTEUC_UPLOAD_VALIDATE", user, Branch);
        if (dt.Rows.Count > 0)
        {
            System.Threading.Thread.Sleep(2000);
            btnProcess.Enabled = false;
            lblValMsg.Text = "<b><font color='red'>" + dt.Rows.Count.ToString() + "</font> Error Records. </b>";
            string record = dt.Rows.Count.ToString() + " Errors Found In Input File.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + record + " Please click on OK to download error report file.');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);
            btnProcess.Enabled = false;
        }
        else
        {
            btnProcess.Enabled = true;
            script = "alert('No Error Records')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TF_DATA objdata = new TF_DATA();
        SqlParameter Branch = new SqlParameter("@Branch", ddlBranch.SelectedValue.ToString());
        SqlParameter Addedby = new SqlParameter("@addedBy", Session["userName"].ToString());       
        string result = objdata.SaveDeleteData("TF_EBRC_ITTEUC_UPLOAD_PROCESS", Addedby);
        if (result.Substring(0, 8) == "Uploaded")
        {
            lbltest.Text = "<b><font color='red'>" + result.Substring(8) + "</font> Records processed successfully.</b>";
        }
        else
        {
            lbltest.Text = "No Records Processed.";
        }
        ddlBranch.Enabled = true;
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
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }      
    protected void getankUniqueTransactionID()
    {
        TF_DATA objData = new TF_DATA();
        String _query = "TF_EBRC_ITTEUC_GetBankUniqueTransactionID";

        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.SelectedValue.Trim();

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            newsrno = Convert.ToInt32(dt.Rows[0]["BankUniqueTransactionID"].ToString().Trim());
            newsrno1 = newsrno + 1;
        }
        else
        {
            newsrno = '1';
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {       
        TF_DATA objdata = new TF_DATA();
        string script = "TF_EBRC_ITTEUC_UPLOAD_VALIDATE";
        SqlParameter Branch = new SqlParameter("@BranchName", ddlBranch.SelectedValue);
        SqlParameter user = new SqlParameter("@user", Session["userName"].ToString());
        DataTable dt = objdata.getData(script, user, Branch);
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
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string conStr = "";
            HiddenField hdnbranchcode = new HiddenField();
            labelMessage.Text = "";
            int dt1count = 0; int dtcount = 0; int noofrecinexcel = 0;
            string BANKREFNUMBER = "", IRMISSUEDATE = "", IRMNUMBER = "", IRMSTATUS = "", IFSCCODE = "", REMITTANCEADCODE = "", REMITTANCEDATE = "",
            REMITTANCEFCC = "", REMITTANCEFCAMOUNT = "", INRCREDITAMOUNT = "", IECCODE = "", PANNUMBER = "", REMITTERNAME = "", REMITTERCOUNTRY = "",
            PURPOSEOFREMITTANCE = "", BANKACCOUNTNO = "", _Addedby, branchcode = "", BankUniqueTransactionId = "";
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
                            _Addedby = Session["userName"].ToString();
                            SqlParameter BankTransUniqNo = new SqlParameter("@BankUniqueTransactionId", BankUniqueTransactionId);
                            SqlParameter _BANKREFNUMBER = new SqlParameter("@bankrefno", BANKREFNUMBER);
                            SqlParameter _IRMISSUEDATE = new SqlParameter("@DocDate", IRMISSUEDATE);
                            SqlParameter _IRMNUMBER = new SqlParameter("@DocNo", IRMNUMBER);
                            SqlParameter _IRMSTATUS = new SqlParameter("@irmstatus", IRMSTATUS);
                            SqlParameter _IFSCCODE = new SqlParameter("@IFSC", IFSCCODE);
                            SqlParameter _REMITTANCEADCODE = new SqlParameter("@RemittanceADCode", REMITTANCEADCODE);
                            SqlParameter _REMITTANCEDATE = new SqlParameter("@Remittancedate", REMITTANCEDATE);
                            SqlParameter Curr = new SqlParameter("@Curr", REMITTANCEFCC.ToUpper());
                            SqlParameter FcAmt = new SqlParameter("@FcAmt", REMITTANCEFCAMOUNT);
                            SqlParameter _INRCREDITAMOUNT = new SqlParameter("@INRCreditAmount", INRCREDITAMOUNT);
                            SqlParameter IECode = new SqlParameter("@IECode", IECCODE);
                            SqlParameter _PANNUMBER = new SqlParameter("@PanNumber", PANNUMBER);
                            SqlParameter Reminame = new SqlParameter("@Reminame", REMITTERNAME.ToUpper());
                            SqlParameter CntryACholder = new SqlParameter("@CntryACholder", REMITTERCOUNTRY);
                            SqlParameter Pcode = new SqlParameter("@Pcode", PURPOSEOFREMITTANCE);
                            SqlParameter _BANKACCOUNTNO = new SqlParameter("@BANKACCOUNTNO", BANKACCOUNTNO);
                            SqlParameter Addedby = new SqlParameter("@Addedby", _Addedby.ToUpper());
                            SqlParameter BRANCHCODE = new SqlParameter("@branchCode", hdnbranchcode.Value);
                            //SqlParameter Addeddate = new SqlParameter("@Addedby", _Addedby.ToUpper());
                            string _query = "TF_EBRC_ITTEUC_UPLOAD";
                            noofrecinexcel++;
                            //SRNO.Value = "";
                            BankUniqueTransactionId = worksheet.Cells[row, 1].Text;
                            BANKREFNUMBER = worksheet.Cells[row, 2].Text;
                            IRMISSUEDATE = worksheet.Cells[row, 3].Text;
                            IRMNUMBER = worksheet.Cells[row, 4].Text;
                            IRMSTATUS = worksheet.Cells[row, 5].Text;
                            IFSCCODE = worksheet.Cells[row, 6].Text;
                            REMITTANCEADCODE = worksheet.Cells[row, 7].Text;
                            REMITTANCEDATE = worksheet.Cells[row, 8].Text;
                            REMITTANCEFCC = worksheet.Cells[row, 9].Text;
                            REMITTANCEFCAMOUNT = worksheet.Cells[row, 10].Text;
                            INRCREDITAMOUNT = worksheet.Cells[row, 11].Text;
                            IECCODE = worksheet.Cells[row, 12].Text;
                            PANNUMBER = worksheet.Cells[row, 13].Text;
                            REMITTERNAME = worksheet.Cells[row, 14].Text;
                            REMITTERCOUNTRY = worksheet.Cells[row, 15].Text;
                            PURPOSEOFREMITTANCE = worksheet.Cells[row, 16].Text;
                            BANKACCOUNTNO = worksheet.Cells[row, 17].Text;

                            string qry;
                            TF_DATA objdata1 = new TF_DATA();
                            qry = "TF_EBRC_FileUpload_Get_Valid_Branch";

                            SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                            DataTable dt2 = objdata1.getData(qry, p2);

                            if (dt2.Rows.Count > 0)
                            {
                                hdnbranchcode.Value = dt2.Rows[0]["BranchCode"].ToString();
                                BRANCHCODE.Value = hdnbranchcode.Value;
                                BankTransUniqNo.Value = BankUniqueTransactionId;
                                _BANKREFNUMBER.Value = BANKREFNUMBER;
                                _IRMISSUEDATE.Value = IRMISSUEDATE;
                                _IRMNUMBER.Value = IRMNUMBER;
                                _IRMSTATUS.Value = IRMSTATUS;
                                _IFSCCODE.Value = IFSCCODE;
                                _REMITTANCEADCODE.Value = REMITTANCEADCODE;
                                _REMITTANCEDATE.Value = REMITTANCEDATE;
                                Curr.Value = REMITTANCEFCC;
                                FcAmt.Value = REMITTANCEFCAMOUNT;
                                _INRCREDITAMOUNT.Value = INRCREDITAMOUNT;
                                IECode.Value = IECCODE;
                                _PANNUMBER.Value = PANNUMBER;
                                Reminame.Value = REMITTERNAME;
                                CntryACholder.Value = REMITTERCOUNTRY;
                                Pcode.Value = PURPOSEOFREMITTANCE;
                                _BANKACCOUNTNO.Value = BANKACCOUNTNO;

                                Addedby.Value = Session["UserName"].ToString();
                                //ADD_DATE.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                                TF_DATA objSave = new TF_DATA();
                                result = objSave.SaveDeleteData(_query, BankTransUniqNo, _BANKREFNUMBER, _IRMISSUEDATE, _IRMNUMBER, _IRMSTATUS, _IFSCCODE,
                                         _REMITTANCEADCODE, _REMITTANCEDATE, Curr, FcAmt, _INRCREDITAMOUNT, IECode, _PANNUMBER, Reminame, CntryACholder, Pcode,
                                         _BANKACCOUNTNO, Addedby, BRANCHCODE);

                                if (result == "Uploaded")
                                {
                                    dtcount = dtcount + 1;
                                }
                                else
                                {
                                    string result1 = objSave.SaveDeleteData("TF_EBRC_ITT_FileUploadLog", BankTransUniqNo, _BANKREFNUMBER, _IRMISSUEDATE, _IRMNUMBER, _IRMSTATUS, _IFSCCODE,
                                         _REMITTANCEADCODE, _REMITTANCEDATE, Curr, FcAmt, _INRCREDITAMOUNT, IECode, _PANNUMBER, Reminame, CntryACholder, Pcode,
                                         _BANKACCOUNTNO, Addedby, BRANCHCODE);
                                    dt1count = dt1count + 1;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('Branch Not Find.');", true);
                            }
                        }
                    }
                    if (dtcount == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('File Aborted.');", true);
                    }
                    if ((dtcount != noofrecinexcel) &&(dtcount != 0))
                    {
                        btnProcess.Enabled = false;
                        btnValidate.Enabled = false;
                        string record = dt1count + " records not uploaded out of " + noofrecinexcel + " records. ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "VAlert('" + record + "');", true);
                    }
                    else
                    {
                        btnProcess.Enabled = true;
                        btnValidate.Enabled = true;
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
        }        
    }
    protected void btnrecordnotuploaded_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "TF_EBRC_ITT_EUC_FilenotuploadedAudit";
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
        else
        { 
        
        }
    }
}