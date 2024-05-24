using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

public partial class IDPMS_IDPMS_ORM_Closure_Upload : System.Web.UI.Page
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
        SqlParameter AD_Code = new SqlParameter("@adcode", ddlBranch.SelectedValue.ToString());
        _query = "TF_IDPMS_ORM_CLOSURE_UPLOAD_Delete";
        result = objdata.SaveDeleteData(_query, AD_Code);



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

        string _Ref_no = "", _AD_Code = "", _IE_Code = "", _Curr = "", _adjustedAmount = "", _adjustedDate = "", _adjustmentIndicator = "", _adjustmentSeqNumber = "",
            _ApproveBy = "", _letterNumber = "", _letterDate = "", _documentNumber = "", _documentDate = "", _portofDischarge = "", _RecordInd = "", _Remarks = "";

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

        cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        string _query = "TF_IDPMS_ORM_CLOSURE_UPLOAD";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i][0].ToString().Trim() + dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim() +
                     dt.Rows[i][3].ToString().Trim() + dt.Rows[i][4].ToString().Trim() + dt.Rows[i][5].ToString().Trim() +
                     dt.Rows[i][6].ToString().Trim() + dt.Rows[i][7].ToString().Trim() + dt.Rows[i][7].ToString().Trim() +
                     dt.Rows[i][8].ToString().Trim() + dt.Rows[i][10].ToString().Trim() + dt.Rows[i][11].ToString().Trim() +
                     dt.Rows[i][12].ToString().Trim()) != "")
                {
                    norecinexcel = norecinexcel + 1;

                    _IE_Code = dt.Rows[i][0].ToString();
                    _Ref_no = dt.Rows[i][1].ToString();
                    _Curr = dt.Rows[i][2].ToString();
                    _adjustedAmount = dt.Rows[i][3].ToString().Replace(",", "");
                    _adjustedDate = dt.Rows[i][4].ToString();
                    _adjustmentIndicator = dt.Rows[i][5].ToString();
                    _ApproveBy = dt.Rows[i][6].ToString();
                    _letterNumber = dt.Rows[i][7].ToString();
                    _letterDate = dt.Rows[i][8].ToString();
                    _documentNumber = dt.Rows[i][9].ToString();
                    _documentDate = dt.Rows[i][10].ToString();
                    _portofDischarge = dt.Rows[i][11].ToString();
                    _Remarks = dt.Rows[i][12].ToString();

                    SqlParameter Ref_no = new SqlParameter("@Ref_no", _Ref_no);
                    SqlParameter AD_Code = new SqlParameter("@AD_Code", ddlBranch.SelectedValue.ToString());
                    SqlParameter IE_Code = new SqlParameter("@IE_Code", _IE_Code);
                    SqlParameter Curr = new SqlParameter("@Curr", _Curr);
                    SqlParameter adjustedAmount = new SqlParameter("@adjustedAmount", _adjustedAmount.Replace(",", ""));
                    SqlParameter adjustedDate = new SqlParameter("@adjustedDate", _adjustedDate);
                    SqlParameter adjustmentIndicator = new SqlParameter("@adjustmentIndicator", _adjustmentIndicator);
                    SqlParameter adjustmentSeqNumber = new SqlParameter("@adjustmentSeqNumber", _adjustmentSeqNumber);
                    SqlParameter ApproveBy = new SqlParameter("@ApproveBy", _ApproveBy);
                    SqlParameter letterNumber = new SqlParameter("@letterNumber", _letterNumber);
                    SqlParameter letterDate = new SqlParameter("@letterDate", _letterDate);
                    SqlParameter documentNumber = new SqlParameter("@documentNumber", _documentNumber);
                    SqlParameter documentDate = new SqlParameter("@documentDate", _documentDate);
                    SqlParameter portofDischarge = new SqlParameter("@portofDischarge", _portofDischarge);
                    SqlParameter RecordInd = new SqlParameter("@RecordInd", _RecordInd);
                    SqlParameter Remarks = new SqlParameter("@Remarks", _Remarks);
                    SqlParameter Addedby = new SqlParameter("@Addedby", Session["userName"].ToString());

                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, Ref_no, AD_Code, IE_Code, Curr, adjustedAmount, adjustedDate, adjustmentIndicator, adjustmentSeqNumber,
                            ApproveBy, letterNumber, letterDate, documentNumber, documentDate, portofDischarge, RecordInd, Remarks, Addedby);

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
        string script = "", ADcode = "";
        ADcode = ddlBranch.SelectedValue.ToString();

        SqlParameter AD_Code = new SqlParameter("@adcode", ADcode);
        DataTable dt = objdata.getData("TF_IDPMS_ORM_CLOSURE_UPLOAD_VALIDATE", AD_Code);
        if (dt.Rows.Count > 0)
        {
            lblValMsg.Text = "<b><font color='red'>" + dt.Rows.Count.ToString() + "</font> Error Records. </b>";
            script = "window.open('IDPM_rpt_DataValidation_ORM_Closure.aspx?adcode=" + ADcode + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
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
        SqlParameter AD_Code = new SqlParameter("@adcode", ddlBranch.SelectedValue.ToString());
        DataTable dt = objdata.getData("TF_IDPMS_ORM_CLOSURE_UPLOAD_VALIDATE", AD_Code);
        if (dt.Rows.Count > 0)
        {
            lbltest.Text = "<b>No Records Processed due to error records present in Uploaded file.</b>";
        }
        else
        {
            string result = objdata.SaveDeleteData("TF_IDPMS_ORM_CLOSURE_UPLOAD_Process", AD_Code);
            if (result.Substring(0, 8) == "Uploaded")
            {
                lbltest.Text = "<b><font color='red'>" + result.Substring(8) + "</font> Records processed successfully.</b>";
            }
            else
                lbltest.Text = "No Records Processed.";
        }
    }
}