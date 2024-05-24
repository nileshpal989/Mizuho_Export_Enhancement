using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EBR_TF_EBRC_ITTEUC_APISTATUSUPLOAD : System.Web.UI.Page
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
                ddlBranch.SelectedIndex = 1;
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                btnupload.Attributes.Add("onclick", "return  ShowProgress();");                
            }
        }
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        string result = "", _query = ""; lblHint.Text = "";
        TF_DATA objdata = new TF_DATA();
        _query = "TF_EBRC_ITT_Delete_Tempdata";// NOT WORKING ONLY DUMMY
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
            string BANKREFNUMBER = "", IRMISSUEDATE = "", IRMNUMBER = "", IRMSTATUS = "", IFSCCODE = "", REMITTANCEADCODE = "", REMITTANCEDATE = "",
           REMITTANCEFCC = "", REMITTANCEFCAMOUNT = "", INRCREDITAMOUNT = "", IECCODE = "", PANNUMBER = "", REMITTERNAME = "", REMITTERCOUNTRY = "",
           PURPOSEOFREMITTANCE = "", BANKACCOUNTNO = "", _Addedby, branchcode = "", BankUniqueTransactionId = "" ,
           Push_Status = "", Push_Error = "", Get_Status = "", RecvdRecordCount = "",
           processedRecordCount = "", irmStatusLst = "", EX_Exception = "", actualdatd = "";
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
                            SqlParameter PUSH_STATUS = new SqlParameter("@Push_Status", SqlDbType.VarChar);
                            SqlParameter PUSH_ERROR = new SqlParameter("@Push_Error", SqlDbType.VarChar);
                            SqlParameter RECVDRECORDCOUNT = new SqlParameter("@RecvdRecordCount", SqlDbType.VarChar);
                            SqlParameter GET_STATUS = new SqlParameter("@Get_Status", SqlDbType.VarChar);
                            SqlParameter PROCESSEDRECORDCOUNT = new SqlParameter("@processedRecordCount", SqlDbType.VarChar);
                            SqlParameter IRMSTATUSLST = new SqlParameter("@irmStatusLst", SqlDbType.VarChar);
                            SqlParameter ACTUALDATA = new SqlParameter("@actualdata", SqlDbType.VarChar);
                            SqlParameter EXEXCEPTION = new SqlParameter("@EX_Exception", SqlDbType.VarChar);
                            SqlParameter ADD_DATE = new SqlParameter("@ADD_DATE", SqlDbType.VarChar);
                            SqlParameter ADD_USER = new SqlParameter("@AddedBy", SqlDbType.VarChar);

                            string _query = "TF_EBRC_ITTEUC_APISTATUS_Upload";
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
                            Push_Status = worksheet.Cells[row, 18].Text;
                            Push_Error = worksheet.Cells[row, 19].Text;
                            Get_Status = worksheet.Cells[row, 20].Text;
                            RecvdRecordCount = worksheet.Cells[row, 21].Text;
                            processedRecordCount = worksheet.Cells[row, 22].Text;
                            irmStatusLst = worksheet.Cells[row, 23].Text;
                            actualdatd = worksheet.Cells[row, 24].Text;
                            EX_Exception = worksheet.Cells[row, 25].Text;

                            string qry;
                            TF_DATA objdata1 = new TF_DATA();
                            qry = "TF_EBRC_FileUpload_Get_Valid_Branch";

                            SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                            DataTable dt2 = objdata1.getData(qry, p2);
                            string queryupload = "";
                            objdata1.SaveDeleteData(queryupload);
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
                                PUSH_STATUS.Value = Push_Status;
                                PUSH_ERROR.Value = Push_Error;
                                GET_STATUS.Value = Get_Status;
                                RECVDRECORDCOUNT.Value = RecvdRecordCount;
                                PROCESSEDRECORDCOUNT.Value = processedRecordCount;
                                IRMSTATUSLST.Value = irmStatusLst;
                                ACTUALDATA.Value = actualdatd;
                                EXEXCEPTION.Value = EX_Exception;

                                ADD_USER.Value = Session["UserName"].ToString();
                                ADD_DATE.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                                TF_DATA objSave = new TF_DATA();
                                result = objSave.SaveDeleteData(_query,BRANCHCODE, BankTransUniqNo, _BANKREFNUMBER, _IRMISSUEDATE, _IRMNUMBER, _IRMSTATUS, _IFSCCODE,
                                         _REMITTANCEADCODE, _REMITTANCEDATE, Curr, FcAmt, _INRCREDITAMOUNT, IECode, _PANNUMBER, Reminame, CntryACholder, Pcode,
                                         _BANKACCOUNTNO, PUSH_STATUS, PUSH_ERROR, GET_STATUS, RECVDRECORDCOUNT, PROCESSEDRECORDCOUNT,
                                    IRMSTATUSLST, ACTUALDATA, EXEXCEPTION, ADD_USER);


                                if (result == "Uploaded")
                                {
                                    string RES=objSave.SaveDeleteData("TF_EBRC_ITTEUC_UpdateAPIStatus", BankTransUniqNo, PUSH_STATUS, GET_STATUS);
                                    dtcount = dtcount + 1;
                                }
                                else
                                {
                                    //string result1 = objSave.SaveDeleteData("TF_EBRC_ORM_FileUploadLog", BranchCode, SRNO, BANK_UNIQUE_TXID, ORM_ISSUE_DATE, ORMNO, ORMSTATUS, IFSCCODE, ADCODE,
                                    //PAYMENTDATE, ORNFCC, ORNFCCAMT, INRPAYABLEAMT, EXCHANGERATE, IECCODE, PANNO, BENEFNAME, BENEFCOUNTRY, PURPOSECODE, MODEOFPAYMENT, REF_IRM, ADD_USER, ADD_DATE);
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
                        //string record = dt1count + " records not uploaded out of " + noofrecinexcel + " records. ";
                        ////string record = dtcount + " records uploaded out of " + noofrecinexcel + " records from the file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "VAlert('" + record + "');", true);
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
            
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
}