using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

public partial class EDPMS_EDPMS_File_Upload_ReceiptOfDoc : System.Web.UI.Page
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
               ddlRefNo.SelectedValue=Session["userADCode"].ToString();
               ddlRefNo.Enabled = false;
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
        ddlRefNo.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlRefNo.DataSource = dt.DefaultView;
            ddlRefNo.DataTextField = "BranchName";
            ddlRefNo.DataValueField = "AuthorizedDealerCode";
            ddlRefNo.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlRefNo.Items.Insert(0, li);

    }

    private string upload_file(string _filepath)
    {
        string uploadresult = "", _FileSrNo = "";

        string AD_Code = "", Document_Date = "", Negotiation_Date = "", Document_No = "", AD_Bill_No = "", Beneficiary_Name = "",
                Beneficiary_Address = "", IE_Code = "", Account_No = "", Applicant_Name = "", Applicant_Address = "", Applicant_Country_Code = "",
                Bill_Amount = "", Form_No = "", Shipping_Bill_No = "", Shipping_Bill_Date = "", @Shipping_Bill_Amount = "", PortCode = "",
                TypeOfExport = "", RecordIndicator = "", Export_Agency = "", Direct_Dispatch_Indicator = "",
                UploadedBy = "", UploadedDate = "";


        string path = Server.MapPath("~/TF_GeneratedFiles/UploadedFiles");
        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileuploaded";

        try
        {
            System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);
            if (System.IO.File.Exists(_filepath))
            {
                try
                {
                    File.Delete(_filepath);
                }
                catch (Exception ex)  //or maybe in finally
                {
                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                    Thread.Sleep(10000); //Wait for object to be killed. 
                    labelMessage.Text = ex.Message.ToString();
                    File.Delete(_filepath); //File can be now deleted
                }
            }
        }
        catch { }
        try
        {
            FileUpload1.PostedFile.SaveAs(_filepath);
            uploadresult = "fileuploaded";
        }
        catch
        {
            uploadresult = "ioerror";
        }
        if (uploadresult == "fileuploaded")
        {
            StreamReader sr = new StreamReader(_filepath);
            string line = sr.ReadLine();
            string[] value = line.Split(',');
            DataTable dt = new DataTable();
            DataRow row;
            foreach (string dc in value)
            {
                dt.Columns.Add(new DataColumn(dc));
            }
            while (!sr.EndOfStream)
            {
                value = sr.ReadLine().Split(',');
                if (value.Length == dt.Columns.Count)
                {
                    row = dt.NewRow();
                    row.ItemArray = value;
                    dt.Rows.Add(row);
                }
                else
                {
                    _rowData = "File Sr No :" + value[0] + " Not uploaded. [ Due to the presence of comma (,) in the row ]";
                    labelMessage.Text = "File Sr No :" + value[0] + " Not uploaded. [ Due to the presence of comma (,) in the row ]";
                }
                norecinexcel = norecinexcel + 1;
            }

            SqlParameter p1 = new SqlParameter("@AD_Code", SqlDbType.VarChar);
            SqlParameter p2 = new SqlParameter("@Document_Date", SqlDbType.VarChar);
            SqlParameter p3 = new SqlParameter("@Negotiation_Date", SqlDbType.VarChar);
            SqlParameter p4 = new SqlParameter("@Document_No", SqlDbType.VarChar);
            SqlParameter p5 = new SqlParameter("@AD_Bill_No", SqlDbType.VarChar);
            SqlParameter p6 = new SqlParameter("@Beneficiary_Name", SqlDbType.VarChar);
            SqlParameter p7 = new SqlParameter("@Beneficiary_Address", SqlDbType.VarChar);
            SqlParameter p8 = new SqlParameter("@IE_Code", SqlDbType.VarChar);
            SqlParameter p9 = new SqlParameter("@Account_No", SqlDbType.VarChar);
            SqlParameter p10 = new SqlParameter("@Applicant_Name", SqlDbType.VarChar);
            SqlParameter p11 = new SqlParameter("@Applicant_Address", SqlDbType.VarChar);
            SqlParameter p12 = new SqlParameter("@Applicant_Country_Code", SqlDbType.VarChar);
            SqlParameter p13 = new SqlParameter("@Bill_Amount", SqlDbType.VarChar);
            SqlParameter p14 = new SqlParameter("@Form_No", SqlDbType.VarChar);
            SqlParameter p15 = new SqlParameter("@Shipping_Bill_No", SqlDbType.VarChar);
            SqlParameter p16 = new SqlParameter("@Shipping_Bill_Date", SqlDbType.VarChar);
            SqlParameter p17 = new SqlParameter("@Shipping_Bill_Amount", SqlDbType.VarChar);
            SqlParameter p18 = new SqlParameter("@PortCode", SqlDbType.VarChar);
            SqlParameter p19 = new SqlParameter("@TypeOfExport", SqlDbType.VarChar);
            SqlParameter p20 = new SqlParameter("@RecordIndicator", SqlDbType.VarChar);
            SqlParameter p21 = new SqlParameter("@Export_Agency", SqlDbType.VarChar);
            SqlParameter p22 = new SqlParameter("@Direct_Dispatch_Indicator", SqlDbType.VarChar);
            SqlParameter p23 = new SqlParameter("@UploadedBy", SqlDbType.VarChar);
            SqlParameter p24 = new SqlParameter("@UploadedDate", SqlDbType.VarChar);

            string _query = "TF_EDPMS_FileUpload_ReceiptOfDoc";

            try
            {
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim() != "")
                        {
                            AD_Code = dt.Rows[i][0].ToString();
                            Document_Date = dt.Rows[i][1].ToString();
                            Negotiation_Date = dt.Rows[i][2].ToString();
                            Document_No = dt.Rows[i][3].ToString();
                            AD_Bill_No = dt.Rows[i][4].ToString();
                            Beneficiary_Name = dt.Rows[i][5].ToString();
                            Beneficiary_Address = dt.Rows[i][6].ToString();
                            IE_Code = dt.Rows[i][7].ToString().Replace("'", "");
                            Account_No = dt.Rows[i][8].ToString().Replace("'", "");
                            Applicant_Name = dt.Rows[i][9].ToString();
                            Applicant_Address = dt.Rows[i][10].ToString();
                            Applicant_Country_Code = dt.Rows[i][11].ToString();
                            Bill_Amount = dt.Rows[i][12].ToString();
                            Form_No = dt.Rows[i][13].ToString().Replace("'", "");
                            Shipping_Bill_No = dt.Rows[i][14].ToString().Replace("'", "");
                            Shipping_Bill_Date = dt.Rows[i][15].ToString();
                            Shipping_Bill_Amount = dt.Rows[i][16].ToString();
                            PortCode = dt.Rows[i][17].ToString();
                            TypeOfExport = dt.Rows[i][18].ToString();
                            RecordIndicator = dt.Rows[i][19].ToString();
                            Export_Agency = dt.Rows[i][20].ToString();
                            Direct_Dispatch_Indicator = dt.Rows[i][21].ToString();

                            UploadedBy = Session["UserName"].ToString();
                            UploadedDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            p1.Value = AD_Code;
                            p2.Value = Document_Date;
                            p3.Value = Negotiation_Date;
                            p4.Value = Document_No;
                            p5.Value = AD_Bill_No;
                            p6.Value = Beneficiary_Name;
                            p7.Value = Beneficiary_Address;
                            p8.Value = IE_Code;
                            p9.Value = Account_No;
                            p10.Value = Applicant_Name;
                            p11.Value = Applicant_Address;
                            p12.Value = Applicant_Country_Code;
                            p13.Value = Bill_Amount;
                            p14.Value = Form_No;
                            p15.Value = Shipping_Bill_No;
                            p16.Value = Shipping_Bill_Date;
                            p17.Value = Shipping_Bill_Amount;
                            p18.Value = PortCode;
                            p19.Value = TypeOfExport;
                            p20.Value = RecordIndicator;
                            p21.Value = Export_Agency;
                            p22.Value = Direct_Dispatch_Indicator;
                            p23.Value = UploadedBy;
                            p24.Value = UploadedDate;


                            //if (Ref_No == ddlRefNo.SelectedValue)
                            //{
                            //  pREF_NO.Value= = Ref_No;
                            TF_DATA objSave = new TF_DATA();
                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23,
                                     p24);
                            if (result.Substring(0, 8) == "Uploaded")
                            {
                                cntrec = cntrec + 1;
                                _rowData = "File Sr No :" + _FileSrNo.ToString() + " Uploaded successfully with Sr No.: " + result.Substring(8).ToString();
                            }
                            else
                            {
                                _rowData = "File Sr No :" + _FileSrNo.ToString() + " Not uploaded. [ " + result.ToString() + " ]";
                            }
                            //if (_rowData != "")
                            //File.AppendAllText(tempFilePath, Environment.NewLine + _rowData);//appends all text in temp file
                            //}                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    labelMessage.Text = errorMsg;
                }
                // lblUploading.Text = "";
            }
            catch (Exception ex)
            {
                uploadresult = ex.Message;
                GC.Collect();
            }
            return uploadresult;
        }
        else
            return uploadresult;
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "", _query = "";

        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@DocType", "DocBill");
        _query = "TF_EDPMS_FileUploadDelete";
        objdata.SaveDeleteData(_query, p1);

        norecinexcel = 0;
        string path = Server.MapPath("~/TF_GeneratedFiles/UploadedFiles");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        result = upload_file(path + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName));
        labelMessage.Font.Size = 10;

        if (result == "fileuploaded")
        {
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + " (Size :" + FileUpload1.PostedFile.ContentLength + "kb)</b>";
            //lbltest.Text = "File name: " +fileinhouse.PostedFile.FileName + "<br>" + fileinhouse.PostedFile.ContentLength + " kb<br>" + "Content type: " +fileinhouse.PostedFile.ContentType;
        }
        else
        {
            labelMessage.Text = result;
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";
        DataTable dt = objdata.getData("TF_EDPMS_FileUpload_Validate_ReceiptOfDoc");
        if (dt.Rows.Count > 0)
        {
            script = "window.open('EDPM_rpt_DataValidation_Realized.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
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
        string result = objdata.SaveDeleteData("TF_EDPMS_FileUpload_ProcessReceiptOfDocument");
        if (result == "Uploaded")
        {
            labelMessage.Text = "Records processed successfully.";
        }
        else
            labelMessage.Text = "No Records Processed.";
    }
}