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

public partial class TF_IDPMS_BOE_Closure_FILE_UPLOAD : System.Web.UI.Page
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
        string query = "TF_IDPMS_BOE_Closure_Upload_Delete";
        SqlParameter ADCODE = new SqlParameter("@AddedADCODE", Session["userADCode"].ToString());
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

        cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        SqlParameter P1 = new SqlParameter("@IE_Code", SqlDbType.VarChar);
        SqlParameter P2 = new SqlParameter("@BOE_No", SqlDbType.VarChar);
        SqlParameter P3 = new SqlParameter("@Port_Code", SqlDbType.VarChar);
        SqlParameter P4 = new SqlParameter("@BOE_Date", SqlDbType.VarChar);
        //SqlParameter P5 = new SqlParameter("@Inv_Sr_No", SqlDbType.VarChar);
        SqlParameter P6 = new SqlParameter("@Inv_No", SqlDbType.VarChar);
        SqlParameter P7 = new SqlParameter("@Closure_Amt", SqlDbType.VarChar);
        SqlParameter P8 = new SqlParameter("@Reason", SqlDbType.VarChar);
        SqlParameter P9 = new SqlParameter("@Closure_Date", SqlDbType.VarChar);
        SqlParameter P10 = new SqlParameter("@ApprovedBy", SqlDbType.VarChar);
        SqlParameter P11 = new SqlParameter("@LetterNo", SqlDbType.VarChar);
        SqlParameter P12 = new SqlParameter("@LetterDate", SqlDbType.VarChar);
        SqlParameter P13 = new SqlParameter("@CloseBillIndicator", SqlDbType.VarChar);
        SqlParameter P14 = new SqlParameter("@Remark", SqlDbType.VarChar);
        SqlParameter P15 = new SqlParameter("@AddedADCode", SqlDbType.VarChar);
        SqlParameter P16 = new SqlParameter("@AddedBy", SqlDbType.VarChar);

        string _query = "TF_IDPMS_BOE_Closure_Upload";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if((dt.Rows[i][0].ToString().Trim() + dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim() +
                     dt.Rows[i][3].ToString().Trim() + dt.Rows[i][4].ToString().Trim() + dt.Rows[i][5].ToString().Trim() +
                     dt.Rows[i][6].ToString().Trim() + dt.Rows[i][7].ToString().Trim() + dt.Rows[i][7].ToString().Trim() +
                     dt.Rows[i][8].ToString().Trim() + dt.Rows[i][10].ToString().Trim() + dt.Rows[i][11].ToString().Trim() +
                     dt.Rows[i][12].ToString().Trim()) != "")
                {
                    norecinexcel = norecinexcel + 1;

                    P1.Value = dt.Rows[i][0].ToString();
                    P2.Value = dt.Rows[i][1].ToString();
                    P3.Value = dt.Rows[i][2].ToString();
                    P4.Value = dt.Rows[i][3].ToString();
                    //P5.Value = dt.Rows[i][4].ToString();
                    P6.Value = dt.Rows[i][4].ToString();
                    P7.Value = dt.Rows[i][5].ToString().Replace(",", "");
                    P8.Value = dt.Rows[i][6].ToString();
                    P9.Value = dt.Rows[i][7].ToString().ToUpper().Trim();
                    P10.Value = dt.Rows[i][8].ToString().Trim();
                    P11.Value = dt.Rows[i][9].ToString().Trim();
                    P12.Value = dt.Rows[i][10].ToString().Trim();
                    P13.Value = dt.Rows[i][11].ToString().Trim();
                    P14.Value = dt.Rows[i][12].ToString().Trim();
                    P15.Value = Session["userADCode"].ToString();
                    P16.Value = Session["userName"].ToString();

                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, P1, P2, P3, P4, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16);

                    if (result == "Uploaded")
                    {
                        cntrec = cntrec + 1;

                        lblUploadMsg.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                    }
                }
            }
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";
        string _adcode = Session["userADCode"].ToString();
        SqlParameter ADCODE = new SqlParameter("@AddedADCODE", _adcode);
        
        DataTable dt = objdata.getData("TF_IDPMS_BOE_Closure_Upload_Validate", ADCODE);
        if (dt.Rows.Count > 0)
        {
            lblValMsg.Text = "<b><font color='red'>" + dt.Rows.Count.ToString() + "</font> Error Records. </b>";
            script = "window.open('TF_IDPMS_Rpt_DataValidation_BOE_Closure.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        else
        {
            script = "alert('No Error Records.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            lblValMsg.Text = "<b> No Error Records </b>";
        }
        lblProcessMsg.Text = "";
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string _adcode = Session["userADCode"].ToString();
        SqlParameter ADCODE = new SqlParameter("@AddedADCODE", _adcode);
        
        DataTable dt = objdata.getData("TF_IDPMS_BOE_Closure_Upload_Validate", ADCODE);
        if (dt.Rows.Count > 0)
        {
            lblProcessMsg.Text = "<b> No Records Processed. Please Correct All the Error records first and then Reupload file. </b>";
        }
        else
        {
            //string result1 = objdata.SaveDeleteData("TF_IDPMS_GET_DOCNO_SETTLEMENT_TEMP", ADCODE);

            string result3 = objdata.SaveDeleteData("TF_IDPMS_BOE_Closure_Upload_Process", ADCODE);
            if (result3.Substring(0, 8) == "Uploaded")
            {
                lblProcessMsg.Text = "<b><font color='red'>" + result3.Substring(8) + "</font> Records processed successfully. </b>";
            }
            else
            {
                lblProcessMsg.Text = "<b> No Records Processed. </b>";
            }
        }
    }

}