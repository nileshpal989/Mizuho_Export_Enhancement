using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;
using System.IO;


public partial class EDPMS_EDPMS_E_FIRC_Old_Closure_Upload : System.Web.UI.Page
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
        SqlParameter Addedby = new SqlParameter("@Addedby", Session["userName"].ToString());
        _query = "TF_EDPMS_E_FIRC_Old_CLOSURE_UPLOAD_Delete";
        result = objdata.SaveDeleteData(_query, AD_Code, Addedby);

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
        string script = "", ADcode = "";
        ADcode = ddlBranch.SelectedValue.ToString();

        SqlParameter AD_Code = new SqlParameter("@adcode", ADcode);
        SqlParameter Addedby = new SqlParameter("@addedBy", Session["userName"].ToString());
        DataTable dt = objdata.getData("TF_EDPMS_E_FIRC_Old_CLOSURE_UPLOAD_VALIDATE", AD_Code, Addedby);
        if (dt.Rows.Count > 0)
        {
            lblValMsg.Text = "<b><font color='red'>" + dt.Rows.Count.ToString() + "</font> Error Records. </b>";
            script = "window.open('EDPMS_E_FIRC_Old_Closure_Upload_Validation.aspx?adcode=" + ADcode + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        else
        {
            lblValMsg.Text = "<b>No Error Records</b>";
            script = "alert('No Error Records')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        SqlParameter AD_Code = new SqlParameter("@adcode", ddlBranch.SelectedValue.ToString());
        SqlParameter Addedby = new SqlParameter("@addedBy", Session["userName"].ToString());
        DataTable dtv = objdata.getData("TF_EDPMS_E_FIRC_Old_CLOSURE_UPLOAD_VALIDATE", AD_Code, Addedby);
        if (dtv.Rows.Count > 0)
        {
            lbltest.Text = "<b>No Records generated due to error records present in Uploaded file.</b>";
        }
        else
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

            TF_DATA objData = new TF_DATA();
            SqlParameter p3 = new SqlParameter("@Adcode", SqlDbType.VarChar);
            p3.Value = ddlBranch.SelectedValue.ToString();

            string _query = "TF_EDPMS_E_FIRC_Old_Closure_Xml_Generation";
            string a = ddlBranch.Text;
            DataTable dt = objData.getData(_query, p3);
            if (dt.Rows.Count > 0)
            {
                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                string queryfilname = "TF_EDPMS_E_FIRC_Old_Closure_Xml_FileName";
                DataTable dtfile = objData.getData(queryfilname);
                string filename1 = dtfile.Rows[0]["FileName"].ToString();
                string _filePath = _directoryPath + "/" + filename1 + ".frccls.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);


                sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfFIRC>" + dt.Rows[0]["noOfFIRC"].ToString() + "</noOfFIRC>");
                sw.WriteLine("</checkSum>");
                sw.WriteLine("<fircNumbers>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // string FIRC_No = "", FIRC_Date = "", fircAmount = "", fundTransferFlag = "", remarks = "", remitterAddress = "", recordIndicator = "";

                    sw.WriteLine("<fircClosure>");
                    sw.WriteLine("<fircNumber>" + dt.Rows[i]["fircNumber"].ToString() + "</fircNumber>");
                    sw.WriteLine("<adCode>" + dt.Rows[i]["adCode"].ToString() + "</adCode>");
                    sw.WriteLine("<closureCurrency>" + dt.Rows[i]["closureCurrency"].ToString() + "</closureCurrency>");
                    sw.WriteLine("<closureAmount>" + Convert.ToDecimal(dt.Rows[i]["closureAmount"].ToString()).ToString("0.00") + "</closureAmount>");
                    sw.WriteLine("<adjustmentDate>" + dt.Rows[i]["adjustmentDate"].ToString() + "</adjustmentDate>");
                    sw.WriteLine("<approvalBy>" + dt.Rows[i]["approvalBy"].ToString() + "</approvalBy>");
                    sw.WriteLine("<reasonForClosure>" + dt.Rows[i]["reasonForClosure"].ToString() + "</reasonForClosure>");
                    sw.WriteLine("<closureSequenceNo>" + dt.Rows[i]["closureSequenceNo"].ToString() + "</closureSequenceNo>");
                    sw.WriteLine("<recordIndicator>" + dt.Rows[i]["recordIndicator"].ToString() + "</recordIndicator>");
                    sw.WriteLine("<remarks>" + dt.Rows[i]["remarks"].ToString() + "</remarks>");
                    sw.WriteLine("</fircClosure>");
                                       
                }
                sw.WriteLine("</fircNumbers>");
                sw.WriteLine("</bank>");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();
                lblmessage.Font.Bold = true;
                string path = "file://" + _serverName + "/TF_GeneratedFiles/EDPMS";
                string link = "/TF_GeneratedFiles/EDPMS/";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
            else
            {
                lblmessage.Text = "No Records";
            }
        }
    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";

        string _fircNumber = "", _ieCode = "", _adCode = "", _closureCurrency = "", _closureAmount = "", _adjustmentDate = "", _approvalBy = "", _reasonForClosure = "", _closureSequenceNo = "",
            _recordIndicator = "", _remarks = "", _Added_adCode = "", _Addedby;

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

        cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        string _query = "TF_EDPMS_E_FIRC_Old_CLOSURE_UPLOAD";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i][0].ToString().Trim() + dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim() +
                     dt.Rows[i][3].ToString().Trim() + dt.Rows[i][4].ToString().Trim() + dt.Rows[i][5].ToString().Trim() +
                     dt.Rows[i][6].ToString().Trim() + dt.Rows[i][7].ToString().Trim() + dt.Rows[i][8].ToString().Trim() +
                     dt.Rows[i][9].ToString().Trim() + dt.Rows[i][10].ToString().Trim()) != "")
                {
                    norecinexcel = norecinexcel + 1;

                    _fircNumber = dt.Rows[i][0].ToString();
                    _ieCode = dt.Rows[i][1].ToString();
                    _adCode = dt.Rows[i][2].ToString();
                    _closureCurrency = dt.Rows[i][3].ToString();
                    _closureAmount = dt.Rows[i][4].ToString().Replace(",", "");
                    _adjustmentDate = dt.Rows[i][5].ToString();
                    _approvalBy = dt.Rows[i][6].ToString();
                    _reasonForClosure = dt.Rows[i][7].ToString();
                    _closureSequenceNo = dt.Rows[i][8].ToString();
                    _recordIndicator = dt.Rows[i][9].ToString();
                    _remarks = dt.Rows[i][10].ToString();
                    _Added_adCode = ddlBranch.SelectedValue.ToString();
                    _Addedby = Session["userName"].ToString();


                    SqlParameter fircNumber = new SqlParameter("@fircNumber", _fircNumber);
                    SqlParameter ieCode = new SqlParameter("@ieCode", _ieCode);
                    SqlParameter adCode = new SqlParameter("@adCode", _adCode);
                    SqlParameter closureCurrency = new SqlParameter("@closureCurrency", _closureCurrency);
                    SqlParameter closureAmount = new SqlParameter("@closureAmount", _closureAmount);
                    SqlParameter adjustmentDate = new SqlParameter("@adjustmentDate", _adjustmentDate);
                    SqlParameter approvalBy = new SqlParameter("@approvalBy", _approvalBy);
                    SqlParameter reasonForClosure = new SqlParameter("@reasonForClosure", _reasonForClosure);
                    SqlParameter closureSequenceNo = new SqlParameter("@closureSequenceNo", _closureSequenceNo);
                    SqlParameter recordIndicator = new SqlParameter("@recordIndicator", _recordIndicator);
                    SqlParameter remarks = new SqlParameter("@remarks", _remarks);
                    SqlParameter Added_adCode = new SqlParameter("@Added_adCode", _Added_adCode);
                    SqlParameter Addedby = new SqlParameter("@Addedby", _Addedby);


                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, fircNumber, ieCode, adCode, closureCurrency, closureAmount, adjustmentDate, approvalBy, reasonForClosure, closureSequenceNo,
                            recordIndicator, remarks, Added_adCode, Addedby);

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
    
}