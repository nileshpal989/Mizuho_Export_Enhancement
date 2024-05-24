using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

public partial class IDPMS_IDPMS_OTT_FILE_UPLOAD : System.Web.UI.Page
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
                //fillBranch();
                //ddlBranch.SelectedIndex = 1;
                //ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;
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
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "", _query = "";

        TF_DATA objdata = new TF_DATA();
        //SqlParameter p1 = new SqlParameter("@IsValidate", "N");
        _query = "TF_IDPMS_Out_FileUploadDelete";
        result = objdata.SaveDeleteData(_query);



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
            }
        }





        //norecinexcel = 0;
        //string path = Server.MapPath("~/GeneratedFiles/Uploaded_Files");
        //if (!Directory.Exists(path))
        //{
        //    Directory.CreateDirectory(path);
        //}

        //result = upload_file(path + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName));
        //labelMessage.Font.Size = 10;

        //if (result == "fileuploaded")
        //{
        //    TF_DATA objServerName = new TF_DATA();
        //    string _serverName = objServerName.GetServerName();
        //    labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + " (Size :" + FileUpload1.PostedFile.ContentLength + "kb)</b>";
        //    //lbltest.Text = "File name: " +fileinhouse.PostedFile.FileName + "<br>" + fileinhouse.PostedFile.ContentLength + " kb<br>" + "Content type: " +fileinhouse.PostedFile.ContentType;
        //}
        //else
        //{
        //    labelMessage.Text = result;
        //}

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

        //    //Get the Sheets in Excel WorkBoo
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

        cmdExcel.CommandText = "SELECT F1,F2,F3,F4,FORMAT(F5,'dd/MM/yyyy') as F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        SqlParameter p1 = new SqlParameter("@Ref_no", SqlDbType.VarChar);
        SqlParameter p2 = new SqlParameter("@AD_Code", SqlDbType.VarChar);
        SqlParameter p3 = new SqlParameter("@Amount", SqlDbType.VarChar);
        SqlParameter p4 = new SqlParameter("@Curr", SqlDbType.VarChar);
        SqlParameter p5 = new SqlParameter("@Payment_Date", SqlDbType.VarChar);
        SqlParameter p6 = new SqlParameter("@IE_Code", SqlDbType.VarChar);
        SqlParameter p7 = new SqlParameter("@IE_Name", SqlDbType.VarChar);
        SqlParameter p8 = new SqlParameter("@IE_Address", SqlDbType.VarChar);
        SqlParameter p9 = new SqlParameter("@IE_Pan_Number", SqlDbType.VarChar);
        SqlParameter p10 = new SqlParameter("@CG", SqlDbType.VarChar);
        SqlParameter p11 = new SqlParameter("@Ben_Name", SqlDbType.VarChar);
        SqlParameter p12 = new SqlParameter("@Ben_accno", SqlDbType.VarChar);
        SqlParameter p13 = new SqlParameter("@Ben_Coutry", SqlDbType.VarChar);
        SqlParameter p14 = new SqlParameter("@Swift", SqlDbType.VarChar);
        SqlParameter p15 = new SqlParameter("@Pupose_Code", SqlDbType.VarChar);
        SqlParameter p16 = new SqlParameter("@Record_indicator", SqlDbType.VarChar);
        SqlParameter p17 = new SqlParameter("@Remarks", SqlDbType.VarChar);
        SqlParameter p18 = new SqlParameter("@Payment_Terms", SqlDbType.VarChar);
        SqlParameter p19 = new SqlParameter("@Seq_No", SqlDbType.VarChar);


        string _query = "TF_IDPMS_Out_FileUpload";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() != "")
                {
                    norecinexcel = norecinexcel + 1;

                    Ref_no = dt.Rows[i][0].ToString();
                    AD_Code = dt.Rows[i][1].ToString();
                    Amount = dt.Rows[i][2].ToString();
                    Currency = dt.Rows[i][3].ToString();
                    payment_date = dt.Rows[i][4].ToString();
                    IE_Code = dt.Rows[i][5].ToString();
                    IE_Name = dt.Rows[i][6].ToString();
                    IE_Address = dt.Rows[i][7].ToString();
                    IE_Pan_Number = dt.Rows[i][8].ToString();
                    CG = dt.Rows[i][9].ToString();
                    Ben_name = dt.Rows[i][10].ToString();
                    Ben_accno = dt.Rows[i][11].ToString();
                    Ben_country = dt.Rows[i][12].ToString();
                    Swift_code = dt.Rows[i][13].ToString();
                    Purpose_code = dt.Rows[i][14].ToString();
                    Record_indicator = dt.Rows[i][15].ToString();
                    Remarks = dt.Rows[i][16].ToString();
                    Payment_Terms = dt.Rows[i][17].ToString();
                    Seq_No = "";


                    p1.Value = Ref_no;
                    p2.Value = AD_Code;
                    p3.Value = Amount.Replace(",", ""); ;
                    p4.Value = Currency;
                    p5.Value = payment_date;
                    p6.Value = IE_Code;
                    p7.Value = IE_Name;
                    p8.Value = IE_Address;
                    p9.Value = IE_Pan_Number;
                    p10.Value = CG;
                    p11.Value = Ben_name;
                    p12.Value = Ben_accno;
                    p13.Value = Ben_country;
                    p14.Value = Swift_code;
                    p15.Value = Purpose_code;
                    p16.Value = Record_indicator;
                    p17.Value = Remarks;
                    p18.Value = Payment_Terms;
                    p19.Value = Seq_No;

                    ////if (RecordIndicator == "N")
                    ////{
                    ////    p19.Value = "1";
                    ////}
                    ////else if (RecordIndicator == "A")
                    ////{
                    ////    p19.Value = "2";
                    ////}
                    ////else
                    ////{
                    ////    p19.Value = "3";
                    ////}

                    //p19.Value = RecordIndicator;
                    //p20.Value = IsValidate;
                    //p21.Value = UploadedBy;
                    //p22.Value = UploadedDate;

                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19);



                    if (result == "Uploaded")
                    {
                        cntrec = cntrec + 1;

                        labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                        //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Records Uploaded')", true);
                    }
                }
            }
        }

    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";
        DataTable dt = objdata.getData("IDPMS_OutwardRemittance_FileUpload_Validate");
        if (dt.Rows.Count > 0)
        {
            script = "window.open('IDPM_rpt_DataValidation_OutwardRemittance.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        else
        {
            script = "alert('No Error Records')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string result = objdata.SaveDeleteData("IDPMS_OutWardRemittance_FileUpload_Process");
        if (result.Substring(0, 8) == "Uploaded")
        {
            labelMessage.Text = result.Substring(8) + " Records processed successfully.";
        }
        else
            labelMessage.Text = "No Records Processed.";
    }
}