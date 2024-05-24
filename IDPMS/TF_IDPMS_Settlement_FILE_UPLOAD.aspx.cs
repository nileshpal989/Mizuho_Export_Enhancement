using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System.Activities.Statements;
using Microsoft.Office.Core;
using DocumentFormat.OpenXml.Math;
using Org.BouncyCastle.Crypto.Generators;

public partial class IDPMS_TF_IDPMS_Settlement_FILE_UPLOAD : System.Web.UI.Page
{
    int norecinexcel, cntrec;
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
                fillBranch();
                ddlBranch.SelectedIndex = 1;
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;

                lblUploadMsg.Text = "";
                lblValMsg.Text = "";
                lblProcessMsg.Text = "";
                btnSynce.Enabled = false;
                btnWarning.Enabled = false;
                btnValidate.Enabled = false;
                btnProcess.Enabled = false;

            }
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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddlBranch.Items.Insert(0, li);
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "";
        lblUploadMsg.Text = "";
        lblValMsg.Text = "";
        lblProcessMsg.Text = "";

        TF_DATA objdata = new TF_DATA();
        string query = "TF_IDPMS_BOE_Settlement_FileUploadDelete";
        SqlParameter ADCODE = new SqlParameter("@ADCODE", Session["userADCode"].ToString());
        result = objdata.SaveDeleteData(query, ADCODE);

        string Ext = System.IO.Path.GetExtension(FileUpload1.FileName);

        string c = FileUpload1.FileName;

        if (FileUpload1.HasFile)
        {
            string fname;
            fname = FileUpload1.FileName;
            //if (fname.Contains(".xls") == false && fname.Contains(".xlsx") == false)
            if (Ext != ".xls" && Ext != ".xlsx")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please Upload Excel File Only !!')", true);
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
                //query = "TF_IDPMS_GET_DOCNO_SETTLEMENT_TEMP";
                //result = objdata.SaveDeleteData(query, ADCODE);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please Select Excel File First !!')", true);
            FileUpload1.Focus();
        }

        lblValMsg.Text = "";
        lblProcessMsg.Text = "";
    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";

        string Ref_no = "", AD_Code = "", Amount = "", Currency = "", payment_date = "", IE_Code = "", IE_Name = "", IE_Address = "", IE_Pan_Number = "", CG = "", Ben_name = "", Ben_accno = "", Ben_country = "", Swift_code = "", Purpose_code = "", Record_indicator = "", Remarks = "", Payment_Terms = "",
            Seq_No = "",
             UploadedBy = "", UploadedDate = "";

        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                         .ConnectionString;
                break;
        }

        //    //Get the Sheets in Excel WorkBook
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

        //cmdExcel.CommandText = "SELECT F1,F2,FORMAT(F3,'dd/MM/yyyy') as F3,F4,F5,F6,F7,F8,F9,F10 From [" + SheetName + "]";
        cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        SqlParameter PortCode = new SqlParameter("@PortCode", SqlDbType.VarChar);
        SqlParameter Bill_No = new SqlParameter("@Bill_No", SqlDbType.VarChar);
        SqlParameter Bill_Date = new SqlParameter("Bill_Date", SqlDbType.VarChar);
        SqlParameter IECODE = new SqlParameter("IECODE", SqlDbType.VarChar);
        SqlParameter outwardReferenceNumber = new SqlParameter("@outwardReferenceNumber", SqlDbType.VarChar);
        //SqlParameter invoiceSerialNo = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
        SqlParameter invoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
        SqlParameter invoiceAmt = new SqlParameter("@invoiceAmt", SqlDbType.VarChar);
        SqlParameter OttAmt = new SqlParameter("@OttAmt", SqlDbType.VarChar);
        SqlParameter Addedby = new SqlParameter("@Addedby", SqlDbType.VarChar);
        SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
        SqlParameter Doc_Date = new SqlParameter("@Doc_Date", SqlDbType.VarChar);
        SqlParameter AdddedDateTime = new SqlParameter("@AdddedDateTime", SqlDbType.VarChar);
        SqlParameter Party_Code = new SqlParameter("@Party_Code", SqlDbType.VarChar);
        SqlParameter Third_Party_Ind = new SqlParameter("@Third_Party_Ind", SqlDbType.VarChar);
        SqlParameter ticketIDD = new SqlParameter("@ticketid", SqlDbType.VarChar);
        string _query = "TF_IDPMS_BOE_Settlement_FileUpload";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() != "")
                {
                    norecinexcel = norecinexcel + 1;
                    ticketIDD.Value = norecinexcel;
                    PortCode.Value = dt.Rows[i][0].ToString();
                    Bill_No.Value = dt.Rows[i][1].ToString();
                    Bill_Date.Value = dt.Rows[i][2].ToString();
                    IECODE.Value = dt.Rows[i][3].ToString().Trim();
                    outwardReferenceNumber.Value = dt.Rows[i][4].ToString();
                    //invoiceSerialNo.Value = dt.Rows[i][5].ToString();
                    invoiceNo.Value = dt.Rows[i][5].ToString();
                    invoiceAmt.Value = dt.Rows[i][6].ToString();
                    OttAmt.Value = dt.Rows[i][7].ToString();

                    Third_Party_Ind.Value = dt.Rows[i][8].ToString().ToUpper().Trim();
                    Party_Code.Value = dt.Rows[i][9].ToString().Trim();
                    Addedby.Value = Session["userName"].ToString();
                    ADCODE.Value = Session["userADCode"].ToString();
                    Doc_Date.Value = System.DateTime.Now.ToString("dd/MM/yyyy");
                    AdddedDateTime.Value = System.DateTime.Now.ToString();

                    TF_DATA objSave = new TF_DATA();
                                        
                    result = objSave.SaveDeleteData(_query, PortCode, Bill_No, Bill_Date, IECODE, outwardReferenceNumber, invoiceNo, invoiceAmt, OttAmt, Addedby, AdddedDateTime, ADCODE, Doc_Date, Third_Party_Ind, Party_Code,ticketIDD);

                    if (result == "Uploaded")
                    {
                        btnSynce.Enabled = true;
                        cntrec = cntrec + 1;
                        
                    }                    
                }
            }
        }
        
            string script = cntrec + " record(s) Uploaded out of " + norecinexcel + " from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", "alert('" + script + "')", true);
            lblUploadMsg.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
            btnupload.BackColor = System.Drawing.Color.GreenYellow;
            
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";
        string _adcode = Session["userADCode"].ToString();
        SqlParameter ADCODE = new SqlParameter("@ADCODE", _adcode);

        string _AddedBy = Session["userName"].ToString();
        SqlParameter AddedBy = new SqlParameter("@Addedby", _AddedBy);

        string Query = "TF_IDPMS_BOE_Settlement_FileUpload_Validate";

        DataTable dt = objdata.getData(Query, ADCODE, AddedBy);
        if (dt.Rows.Count > 0)
        {
            
            lblValMsg.Text = "<b><font color='red'>" + dt.Rows.Count.ToString() + "</font> Error Records. </b>";
            script = "window.open('TF_IDPMS_Rpt_DataValidation_BOE_Settlement.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            btnSynce.BackColor = System.Drawing.Color.GreenYellow;

        }
        else
        {
            btnSynce.BackColor = System.Drawing.Color.GreenYellow;
            btnValidate.BackColor = System.Drawing.Color.GreenYellow;
            btnProcess.Enabled = true;
            script = "alert('No Error Records.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            lblValMsg.Text = "<b> No Error Records </b>";
        }
        lblProcessMsg.Text = "";
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        SqlParameter ADCODE = new SqlParameter("@ADCODE", Session["userADCode"].ToString());

        string _AddedBy = Session["userName"].ToString();
        SqlParameter AddedBy = new SqlParameter("@Addedby", _AddedBy);


        DataTable dt = objdata.getData("TF_IDPMS_BOE_Settlement_FileUpload_Valid1ate", ADCODE, AddedBy);
        if (dt.Rows.Count > 0)
        {
            lblProcessMsg.Text = "<b> No Records Processed. Please Correct All the Error records first and then Reupload file. </b>";
        }
        else
        {
            string result1 = objdata.SaveDeleteData("TF_IDPMS_GET_DOCNO_SETTLEMENT_TEMP", ADCODE);
            string result2 = objdata.SaveDeleteData("TF_IDPMS_SET_PayRefNo_SETTLEMENT_TEMP", ADCODE, AddedBy);
            string result3 = objdata.SaveDeleteData("TF_IDPMS_BOE_Settlement_FileUpload_Process", ADCODE, AddedBy);
            if (result3.Substring(0, 8) == "Uploaded")
            {
                string script = "alert('Records processed successfully.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                lblProcessMsg.Text = "<b><font color='red'>" + result3.Substring(8) + "</font> Records processed successfully. </b>";
            }
            else
            {
                string script = "alert('No Records Processed.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                lblProcessMsg.Text = "<b> No Records Processed. </b>";
            }
        }
    }
    protected void btnWarning_Click(object sender, EventArgs e)
    {

        TF_DATA objdata = new TF_DATA();
        string script = "";
        string _adcode = Session["userADCode"].ToString();
        SqlParameter ADCODE = new SqlParameter("@ADCODE", _adcode);

        string _AddedBy = Session["userName"].ToString();
        SqlParameter AddedBy = new SqlParameter("@Addedby", _AddedBy);

        string Query = "TF_IDPMS_BOE_Settlement_FileUpload_Warning";

        DataTable dt = objdata.getData(Query, ADCODE, AddedBy);
        if (dt.Rows.Count > 0)
        {
           
            //lblValMsg.Text = "<b><font color='red'>" + dt.Rows.Count.ToString() + "</font> Error Records. </b>";
            script = "window.open('TF_IDPMS_Settlement_FILE_Upload_Warning.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            btnSynce.BackColor = System.Drawing.Color.GreenYellow;
            btnValidate.Enabled = true;
        }
        else
        {
            btnValidate.Enabled = true;
            btnSynce.BackColor = System.Drawing.Color.GreenYellow;
            btnWarning.BackColor = System.Drawing.Color.GreenYellow;
            script = "alert('No Error Records.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }

        lblValMsg.Text = "";
        lblProcessMsg.Text = "";
    }
    protected void btnSync_Click(object sender, EventArgs e)
    {
        try
        {
            if (confirmValue.Value == "true")
            {
                string BillNo = "", IE_Code = "", InvoiceNo = "", INVSerialNo = "", INVAmount = "", ticketID = "", updateinvsrnoflag = "";
                int CountUpdatedRecord = 0;
                TF_DATA objdata = new TF_DATA();
                string script = "";
                string _adcode = Session["userADCode"].ToString();
                SqlParameter ADCODE = new SqlParameter("@ADCODE", _adcode);

                string _AddedBy = Session["userName"].ToString();
                SqlParameter AddedBy = new SqlParameter("@Addedby", _AddedBy);

                string Query = "TF_IDPMS_BOE_Settlement_FileUpload_SYNC_Validate";
                TF_DATA objSave = new TF_DATA();
                DataTable dt = objdata.getData(Query, ADCODE, AddedBy);
                int numberOfRecords = dt.Rows.Count;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        foreach (DataRow getduplicate in dt.Rows)
                        {

                            SqlParameter Bill_No1 = new SqlParameter("@Bill_No", SqlDbType.VarChar);
                            SqlParameter IECODE1 = new SqlParameter("IECODE", SqlDbType.VarChar);
                            SqlParameter invoiceSerialNo1 = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                            SqlParameter invoiceNo1 = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                            BillNo = getduplicate["Bill_No"].ToString().Trim();
                            IE_Code = getduplicate["IECODE"].ToString().Trim();
                            InvoiceNo = getduplicate["invoiceNo"].ToString().Trim();
                            Bill_No1.Value = BillNo;
                            IECODE1.Value = IE_Code;
                            invoiceNo1.Value = InvoiceNo;
                            DataTable getallbillno = objSave.getData("TF_IDPMS_BOE_Settlement_FileUpload_DuplicateBillNO", Bill_No1, IECODE1, invoiceNo1);

                            foreach (DataRow row in getallbillno.Rows)
                            {
                                SqlParameter Bill_No = new SqlParameter("@Bill_No", SqlDbType.VarChar);
                                SqlParameter IECODE = new SqlParameter("IECODE", SqlDbType.VarChar);
                                SqlParameter invoiceSerialNo = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                                SqlParameter invoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                                SqlParameter invoiceAmt = new SqlParameter("@invoiceAmt", SqlDbType.VarChar);
                                SqlParameter ticketidd = new SqlParameter("@ticketID", SqlDbType.VarChar);

                                BillNo = row["Bill_No"].ToString().Trim();
                                IE_Code = row["IECODE"].ToString().Trim();
                                InvoiceNo = row["invoiceNo"].ToString().Trim();
                                INVAmount = row["invoiceAmt"].ToString().Trim();
                                ticketID = row["TicketID"].ToString().Trim();
                                Bill_No.Value = BillNo;
                                IECODE.Value = IE_Code;
                                invoiceNo.Value = InvoiceNo;
                                invoiceAmt.Value = INVAmount;
                                ticketidd.Value = ticketID;

                                DataTable getinvsrno = objSave.getData("TF_IDPMS_BOE_Settlement_FileUpload_GetInvSRNo", Bill_No, IECODE, invoiceNo, invoiceAmt);
                                INVSerialNo = getinvsrno.Rows[0]["InvoiceSerialNo"].ToString().Trim();
                                if (INVSerialNo != "Invoice Serial No. Not Find")
                                {
                                    CountUpdatedRecord = CountUpdatedRecord + 1;
                                    invoiceSerialNo.Value = INVSerialNo;
                                    updateinvsrnoflag = objSave.SaveDeleteData("TF_IDPMS_BOE_Settlement_FileUpload_UpdateInvSRNoFlag", Bill_No, IECODE, invoiceNo, invoiceAmt, invoiceSerialNo, ticketidd);
                                }
                            }
                        }
                    }
                }
                if (CountUpdatedRecord > 0)
                {
                    btnWarning.Enabled = true;
                    btnValidate.Enabled = true;
                    string bill, invoice;
                    if (numberOfRecords != 1)
                        bill = "Bills";
                    else
                        bill = "Bill";
                    if (CountUpdatedRecord != 1)
                        invoice = "Invoices";
                    else
                        invoice = "Invoice";
                    script = CountUpdatedRecord + " " + invoice + " synced of "+ numberOfRecords + " " + bill;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "alert('" + script + "');", true);
                    lblsync.Text = "<b><font color='red'>" + CountUpdatedRecord + "</font>" +" "+ invoice + " synced of <font color = 'red' >" + numberOfRecords + "</font>" + " " + bill;
                    btnSynce.ForeColor = System.Drawing.Color.GreenYellow;                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "alert('There is no any record to synced');", true);
                    lblsync.Text = "<b> There is no any record to synced </b>";
                    btnSynce.ForeColor = System.Drawing.Color.GreenYellow;
                }
            }
            else
            {
                btnWarning.Enabled = true;
                btnValidate.Enabled = true;

            }
        }
        catch (Exception ex)
        {
            lblUploadMsg.Text = ex.Message;
        }        
    }  
    protected void GetCountedValue(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string _adcode = Session["userADCode"].ToString();
        SqlParameter ADCODE = new SqlParameter("@ADCODE", _adcode);
        string _AddedBy = Session["userName"].ToString();
        SqlParameter AddedBy = new SqlParameter("@Addedby", _AddedBy);
        string Query = "TF_IDPMS_BOE_Settlement_FileUpload_SYNC_Validate";
        TF_DATA objSave = new TF_DATA();
        DataTable dt = objdata.getData(Query, ADCODE, AddedBy);
        int numberOfRecords = dt.Rows.Count;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "confirmAction('" + numberOfRecords + "');", true);
    }

}