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

public static class StringExtensions
{
    public static string Right(this string str, int length)
    {
        return str.Substring(str.Length - length, length);
    }
}

public partial class IDPMS_TF_IDPMS_ManualFileUpload : System.Web.UI.Page
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


            }
        }
    }


    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
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
            li.Text = "No Record(s) Found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "", _query = "";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@branch", ddlBranch.SelectedItem.Text);
        _query = "TF_IDPMS_Manual_FileUploadDelete";
        result = objdata.SaveDeleteData(_query, p1);

        if (FileUpload1.HasFile)
        {
            txtInputFile.Text = FileUpload1.PostedFile.FileName;

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
                //txtInputFile.Text = Server.MapPath(FileUpload1.PostedFile.FileName);

                GetExcelSheets(FilePath, Extension, "No");
            }
        }
    }

    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";

        string portOfDisharg = "", importAgency = "", billEntryNo = "", billEntryDate = "", ADCode = "", IE_Code = "", portShipmnt = "", Record_indicator = "", OPartnm = "", OPartAddrs = "", OPartyCountry = "",
            Seq_No = "", invoiceNo = "", termsOfinvoive = "", invoiceAmt = "", invoiceCurr = "",
             UploadedBy = "", UploadedDate = "", invoiceSrNo = "";

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

        cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        SqlParameter p1 = new SqlParameter("@portOfDisharg", SqlDbType.VarChar);
        SqlParameter p2 = new SqlParameter("@importAgency", SqlDbType.VarChar);
        SqlParameter p3 = new SqlParameter("@billEntryNo", SqlDbType.VarChar);
        SqlParameter p4 = new SqlParameter("@billEntryDate", SqlDbType.VarChar);
        SqlParameter p5 = new SqlParameter("@ADCode", SqlDbType.VarChar);
        SqlParameter p6 = new SqlParameter("@IE_Code", SqlDbType.VarChar);
        SqlParameter p7 = new SqlParameter("@portShipmnt", SqlDbType.VarChar);
        SqlParameter p8 = new SqlParameter("@Record_indicator", SqlDbType.VarChar);
        SqlParameter p9 = new SqlParameter("@OPartnm", SqlDbType.VarChar);
        SqlParameter p10 = new SqlParameter("@OPartAddrs", SqlDbType.VarChar);
        SqlParameter p11 = new SqlParameter("@OPartyCountry", SqlDbType.VarChar);
        SqlParameter p12 = new SqlParameter("@invoiceSrNo", SqlDbType.VarChar);
        SqlParameter p13 = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
        SqlParameter p14 = new SqlParameter("@termsOfinvoive", SqlDbType.VarChar);
        SqlParameter p15 = new SqlParameter("@invoiceAmt", SqlDbType.VarChar);
        SqlParameter p16 = new SqlParameter("@invoiceCurr", SqlDbType.VarChar);
        SqlParameter p17 = new SqlParameter("@user", Session["userName"].ToString());
        SqlParameter p18 = new SqlParameter("@datetime", System.DateTime.Now.ToString());

        string _query = "TF_IDPMS_Manual_FileUpload";

        if (dt.Rows.Count > 1)
        {
            TF_DATA objSave = new TF_DATA();
            //string DocNoQuery = "TF_GetDocNoManualBOEProcess";
            //DataTable dtDoc = objSave.getData(DocNoQuery);

            //string AudDocNoM = "";
            //AudDocNoM = dtDoc.Rows[0]["LastDocNo"].ToString();

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() != "")
                {
                    norecinexcel = norecinexcel + 1;

                    portOfDisharg = dt.Rows[i][0].ToString();
                    importAgency = dt.Rows[i][1].ToString();
                    billEntryNo = dt.Rows[i][2].ToString();
                    billEntryDate = dt.Rows[i][3].ToString();
                    ADCode = dt.Rows[i][4].ToString();
                    IE_Code = dt.Rows[i][5].ToString();
                    portShipmnt = dt.Rows[i][6].ToString();
                    Record_indicator = dt.Rows[i][7].ToString();
                    OPartnm = dt.Rows[i][8].ToString();
                    OPartAddrs = dt.Rows[i][9].ToString();
                    OPartyCountry = dt.Rows[i][10].ToString();
                    invoiceSrNo = dt.Rows[i][11].ToString();
                    invoiceNo = dt.Rows[i][12].ToString();
                    termsOfinvoive = dt.Rows[i][13].ToString();
                    invoiceAmt = dt.Rows[i][14].ToString();
                    invoiceCurr = dt.Rows[i][15].ToString();

                    p1.Value = portOfDisharg;
                    p2.Value = importAgency;
                    p3.Value = billEntryNo;
                    p4.Value = billEntryDate;
                    p5.Value = ADCode;
                    p6.Value = IE_Code;
                    p7.Value = portShipmnt;
                    p8.Value = Record_indicator;
                    p9.Value = OPartnm;
                    p10.Value = OPartAddrs;
                    p11.Value = OPartyCountry;
                    p12.Value = invoiceSrNo;
                    p13.Value = invoiceNo;
                    p14.Value = termsOfinvoive;
                    p15.Value = invoiceAmt;
                    p16.Value = invoiceCurr;

                    if (importAgency == "Customs")
                    {
                        p2.Value = "1";
                    }
                    else
                    {
                        p2.Value = "2";
                    }

                    //if (dtDoc.Rows.Count > 0)
                    //{
                    //    SqlParameter IncDocNo = new SqlParameter("@Doc_No", AudDocNoM);
                      
                    //    String year = AudDocNoM.Substring(10, 4);
                    //    AudDocNoM = (Convert.ToInt32(AudDocNoM.Substring(4, 6)) + 1).ToString();
                    //    AudDocNoM = "00000" + AudDocNoM;
                    //    AudDocNoM = StringExtensions.Right(AudDocNoM, 6);
                    //    AudDocNoM = "MBOE" + AudDocNoM + year;

                        result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18);

                        if (result == "Uploaded")
                        {
                           

                            cntrec = cntrec + 1;

                            labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Records Uploaded')", true);
                        }                    
                }
            }
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";
        SqlParameter p1 = new SqlParameter("@ADCode", Session["userADCode"].ToString());
        DataTable dt = objdata.getData("IDPMS_Manual_FileUpload_Validate",p1);
        if (dt.Rows.Count > 0)
        {
            string count = dt.Rows.Count.ToString();
            script = "window.open('IDPM_rpt_DataValidation_Manual.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            labelMessage1.Text = "<b><font color='red'>" + count + " </font>Error record(s) ";
        }
        else
        {
            script = "alert('No Error Records')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            labelMessage1.Text = "<b><font color='red'>" + cntrec + " </font>Error record(s) ";
        }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        string _script = "";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@ADCode", Session["userADCode"].ToString());
        DataTable dt = objdata.getData("IDPMS_Manual_FileUpload_Validate",p1);
        if (dt.Rows.Count > 0)
        {
            //_script = "alert('There are still Error Records in the File. Please Correct and Reupload and Revalidate')";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", _script, true);
            labelMessage2.Text = "<b>There are still Error Records in the File. Please Correct and Reupload and Revalidate";
        }
        else
        {
            //string _Query_Insert = objdata.SaveDeleteData("IDPMS_ManulaData_FileUpload_Process");
            string result = "";
            SqlParameter p2 = new SqlParameter("@ADCode", Session["userADCode"].ToString());
            result = objdata.SaveDeleteData("IDPMS_ManulaData_FileUpload_Process",p2);
            if (result.Substring(0, 8) == "Uploaded")
            {
                labelMessage2.Text = "<b><font color='red'>" + result.Substring(8) + " </font>Records processed successfully.";
            }
            else
                labelMessage2.Text = "No Records Processed.";
        }
    }
}
