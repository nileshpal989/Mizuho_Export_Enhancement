using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Threading;

public partial class EDPMS_EDPMS_Upload_INW_CSV_FILE : System.Web.UI.Page
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
                ddlRefNo.SelectedValue = Session["userADCOde"].ToString();
                ddlRefNo.Enabled = false;
            }
        }
    }
    private string upload_file(string _filepath)
    {
        string uploadresult = "", _FileSrNo = "";
        TF_DATA objSave = new TF_DATA();

        string BranchCode, Document_No, Document_Date, CustAcNo, Purpose_Code, Currency
               , Amount, Balance, Exchange_Rate, Amount_In_INR, FIRC_No, FIRC_Date, FIRC_AD_Code,Value_Date, Remitter_Name, Remitter_CountryID,
               Remitter_Address, Remitter_Bank, Remitter_Bank_Country, Remitter_Bank_Address, Purpose_Of_Remittance;
        string _FIRCNumber, _FIRCADCode;
        //FileStream currentFileStream = null;//EDIT

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

            SqlParameter p1 = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            SqlParameter p2 = new SqlParameter("@Document_No", SqlDbType.VarChar);
            SqlParameter p3 = new SqlParameter("@Document_Date", SqlDbType.VarChar);
            SqlParameter p4 = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
            SqlParameter p5 = new SqlParameter("@Purpose_Code", SqlDbType.VarChar);
            SqlParameter p6 = new SqlParameter("@Currency", SqlDbType.VarChar);
            SqlParameter p7 = new SqlParameter("@Amount", SqlDbType.VarChar);
            SqlParameter p9 = new SqlParameter("@Exchange_Rate", SqlDbType.VarChar);
            SqlParameter p10 = new SqlParameter("@Amount_In_INR", SqlDbType.VarChar);
            SqlParameter p11 = new SqlParameter("@FIRC_No", SqlDbType.VarChar);
            SqlParameter p12 = new SqlParameter("@FIRC_Date", SqlDbType.VarChar);
            SqlParameter p13 = new SqlParameter("@FIRC_AD_Code", SqlDbType.VarChar);
            SqlParameter p14 = new SqlParameter("@Value_Date", SqlDbType.VarChar);
            SqlParameter p15 = new SqlParameter("@Remitter_Name", SqlDbType.VarChar);
            SqlParameter p16 = new SqlParameter("@Remitter_CountryID", SqlDbType.VarChar);
            SqlParameter p17 = new SqlParameter("@Remitter_Address", SqlDbType.VarChar);
            SqlParameter p18 = new SqlParameter("@Remitter_Bank", SqlDbType.VarChar);
            SqlParameter p21 = new SqlParameter("@Remitter_Bank_Country", SqlDbType.VarChar);
            SqlParameter p19 = new SqlParameter("@Remitter_Bank_Address", SqlDbType.VarChar);
            SqlParameter p20 = new SqlParameter("@Purpose_Of_Remittance", SqlDbType.VarChar);
            
            string _qry = "TF_EDPMS_Delete_Temp_INW";
            string Result = objSave.SaveDeleteData(_qry);         
            
            string _query = "TF_EDPMS_INW_FILE_UPLOAD";
            try
            {
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim() != "")
                        {
                            //SrNo = dt.Rows[i][0].ToString();
                            BranchCode = dt.Rows[i][0].ToString();
                            Document_No = dt.Rows[i][1].ToString();
                            Document_Date = dt.Rows[i][2].ToString();
                            CustAcNo = dt.Rows[i][3].ToString();
                            Purpose_Code = dt.Rows[i][4].ToString();
                            Currency = dt.Rows[i][5].ToString();
                            Amount = dt.Rows[i][6].ToString();
                            Exchange_Rate = dt.Rows[i][7].ToString();
                            Amount_In_INR = dt.Rows[i][8].ToString();
                            FIRC_No = dt.Rows[i][9].ToString();
                            FIRC_Date = dt.Rows[i][10].ToString();
                            FIRC_AD_Code = dt.Rows[i][11].ToString();
                            Value_Date = dt.Rows[i][12].ToString();
                            Remitter_Name = dt.Rows[i][13].ToString();
                            Remitter_CountryID = dt.Rows[i][14].ToString();
                            Remitter_Address= dt.Rows[i][15].ToString();
                            Remitter_Bank= dt.Rows[i][16].ToString();
                            Remitter_Bank_Country = dt.Rows[i][17].ToString();
                            Remitter_Bank_Address= dt.Rows[i][18].ToString();
                            Purpose_Of_Remittance= dt.Rows[i][19].ToString();

                            p1.Value = BranchCode;
                            p2.Value = Document_No;
                            p3.Value = Document_Date;
                            p4.Value = CustAcNo;
                            p5.Value = Purpose_Code;
                            p6.Value = Currency;
                            p7.Value = Amount;
                            p9.Value = Exchange_Rate;
                            p10.Value = Amount_In_INR;
                            p11.Value = FIRC_No;
                            p12.Value = FIRC_Date;
                            p13.Value = FIRC_AD_Code;
                            p14.Value = Value_Date;
                            p15.Value = Remitter_Name;
                            p16.Value = Remitter_CountryID;
                            p17.Value = Remitter_Address;
                            p18.Value = Remitter_Bank;
                            p19.Value = Remitter_Bank_Address;
                            p20.Value = Purpose_Of_Remittance;
                            p21.Value = Remitter_Bank_Country;

                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p9, p10, p11, p12,p13,p14,p15,p16,p17,p18,p19,p20,p21);
                            if (result.Substring(0, 8) == "Uploaded")
                            {
                                cntrec = cntrec + 1;
                                _rowData = "File Sr No :" + _FileSrNo.ToString() + " Uploaded successfully with Sr No.: " + result.Substring(8).ToString();
                            }
                            else
                            {
                                _rowData = "File Sr No :" + _FileSrNo.ToString() + " Not uploaded. [ " + result.ToString() + " ]";
                            }
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
        DataTable dt = objdata.getData("TF_EDPMS_INW_FileUpload_Validate" );
        if (dt.Rows.Count > 0)
        {
            script = "window.open('EDPMS_rpt_INW_Data_Validation.aspx?Branch=" + ddlRefNo.SelectedItem.Text + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
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
        string result = objdata.SaveDeleteData("TF_EDPMS_INW_FileUpload_ProcessData");
        if (result == "Uploaded")
        {
            labelMessage.Text = "File Processed Successfully.";
        }
        else
            labelMessage.Text = "No Records Processed.";
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
}