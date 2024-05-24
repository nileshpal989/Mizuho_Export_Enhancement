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

public partial class EDPMS_EDPMS_File_Upload : System.Web.UI.Page
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

        string SrNo = "", AD_Code = "", Document_Date = "", Negotiation_Date = "", Document_No = "", AD_Bill_No = "", Beneficiary_Name = "",
                Beneficiary_Address = "", IE_Code = "", Account_No = "", Applicant_Name = "", Applicant_Address = "", Applicant_Country_Code = "",
                Maturity_Date = "", Realized_Date = "", Bill_Amount = "", Invoice_Sr_No = "", Invoice_No = "", Invoice_Date = "", Invoice_Amount = "", Currency = "",
                Realized_Amount = "", Realization_Status = "", Form_No = "", Shipping_Bill_No = "", Shipping_Bill_Date = "", @Shipping_Bill_Amount = "", PortCode = "",
                TypeOfExport = "", RecordIndicator = "", EBRC_No = "", Closed_Bill_Indication = "", Export_Agency = "", Direct_Dispatch_Indicator = "",
                Bank_Charges = "", FOB_Amount = "", FOB_Amount_IC = "", Freight_Amount = "", Freight_Amount_IC = "", Insurance_Amount = "", Insurance_Amount_IC = "",
                Commission_Amount = "", Commission_Amount_IC = "", Packaging_Charges_Amount = "", Packaging_Charges_Amount_IC = "", Deduction_Amount = "",
                Deduction_Amount_IC = "", Discount_Amount = "", Discount_Amount_IC = "", IsProcessed = "", UploadedBy = "", UploadedDate = "";
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

            SqlParameter p1 = new SqlParameter("@SrNo", SqlDbType.VarChar);
            SqlParameter p2 = new SqlParameter("@AD_Code", SqlDbType.VarChar);
            SqlParameter p3 = new SqlParameter("@Document_Date", SqlDbType.VarChar);
            SqlParameter p4 = new SqlParameter("@Negotiation_Date", SqlDbType.VarChar);
            SqlParameter p5 = new SqlParameter("@Document_No", SqlDbType.VarChar);
            SqlParameter p6 = new SqlParameter("@AD_Bill_No", SqlDbType.VarChar);
            SqlParameter p7 = new SqlParameter("@Beneficiary_Name", SqlDbType.VarChar);
            SqlParameter p8 = new SqlParameter("@Beneficiary_Address", SqlDbType.VarChar);
            SqlParameter p9 = new SqlParameter("@IE_Code", SqlDbType.VarChar);
            SqlParameter p10 = new SqlParameter("@Account_No", SqlDbType.VarChar);
            SqlParameter p11 = new SqlParameter("@Applicant_Name", SqlDbType.VarChar);
            SqlParameter p12 = new SqlParameter("@Applicant_Address", SqlDbType.VarChar);
            SqlParameter p13 = new SqlParameter("@Applicant_Country_Code", SqlDbType.VarChar);
            SqlParameter p14 = new SqlParameter("@Maturity_Date", SqlDbType.VarChar);
            SqlParameter p15 = new SqlParameter("@Realized_Date", SqlDbType.VarChar);
            SqlParameter p16 = new SqlParameter("@Invoice_Sr_No", SqlDbType.VarChar);
            SqlParameter p17 = new SqlParameter("@Invoice_No", SqlDbType.VarChar);
            SqlParameter p18 = new SqlParameter("@Invoice_Date", SqlDbType.VarChar);
            SqlParameter p19 = new SqlParameter("@Invoice_Amount", SqlDbType.VarChar);
            SqlParameter p20 = new SqlParameter("@Currency", SqlDbType.VarChar);
            SqlParameter p21 = new SqlParameter("@Realized_Amount", SqlDbType.VarChar);
            SqlParameter p22 = new SqlParameter("@Realization_Status", SqlDbType.VarChar);
            SqlParameter p23 = new SqlParameter("@Form_No", SqlDbType.VarChar);
            SqlParameter p24 = new SqlParameter("@Shipping_Bill_No", SqlDbType.VarChar);
            SqlParameter p25 = new SqlParameter("@Shipping_Bill_Date", SqlDbType.VarChar);
            SqlParameter p26 = new SqlParameter("@PortCode", SqlDbType.VarChar);
            SqlParameter p27 = new SqlParameter("@TypeOfExport", SqlDbType.VarChar);
            SqlParameter p28 = new SqlParameter("@RecordIndicator", SqlDbType.VarChar);
            SqlParameter p29 = new SqlParameter("@EBRC_No", SqlDbType.VarChar);
            SqlParameter p30 = new SqlParameter("@Closed_Bill_Indication", SqlDbType.VarChar);
            SqlParameter p31 = new SqlParameter("@Export_Agency", SqlDbType.VarChar);
            SqlParameter p32 = new SqlParameter("@Direct_Dispatch_Indicator", SqlDbType.VarChar);
            SqlParameter p33 = new SqlParameter("@Bank_Charges", SqlDbType.VarChar);
            SqlParameter p34 = new SqlParameter("@FOB_Amount", SqlDbType.VarChar);
            SqlParameter p35 = new SqlParameter("@FOB_Amount_IC", SqlDbType.VarChar);
            SqlParameter p36 = new SqlParameter("@Freight_Amount", SqlDbType.VarChar);
            SqlParameter p37 = new SqlParameter("@Freight_Amount_IC", SqlDbType.VarChar);
            SqlParameter p38 = new SqlParameter("@Insurance_Amount", SqlDbType.VarChar);
            SqlParameter p39 = new SqlParameter("@Insurance_Amount_IC", SqlDbType.VarChar);
            SqlParameter p40 = new SqlParameter("@Commission_Amount", SqlDbType.VarChar);
            SqlParameter p41 = new SqlParameter("@Commission_Amount_IC", SqlDbType.VarChar);
            SqlParameter p42 = new SqlParameter("@Packaging_Charges_Amount", SqlDbType.VarChar);
            SqlParameter p43 = new SqlParameter("@Packaging_Charges_Amount_IC", SqlDbType.VarChar);
            SqlParameter p44 = new SqlParameter("@Deduction_Amount", SqlDbType.VarChar);
            SqlParameter p45 = new SqlParameter("@Deduction_Amount_IC", SqlDbType.VarChar);
            SqlParameter p46 = new SqlParameter("@Discount_Amount", SqlDbType.VarChar);
            SqlParameter p47 = new SqlParameter("@Discount_Amount_IC", SqlDbType.VarChar);
            SqlParameter p48 = new SqlParameter("@IsProcessed ", SqlDbType.VarChar);
            SqlParameter p49 = new SqlParameter("@UploadedBy", SqlDbType.VarChar);
            SqlParameter p50 = new SqlParameter("@UploadedDate", SqlDbType.VarChar);
            SqlParameter p51 = new SqlParameter("@Shipping_Bill_Amount", SqlDbType.VarChar);
            SqlParameter p52 = new SqlParameter("@Bill_Amount", SqlDbType.VarChar);
            SqlParameter p53 = new SqlParameter("@IsValid", "");

            SqlParameter p54 = new SqlParameter("@FIRCNumber", SqlDbType.VarChar);
            SqlParameter p55 = new SqlParameter("@FIRCADCode", SqlDbType.VarChar);
            string _query = "TF_EDPMS_FileUpload";

            try
            {
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim() != "")
                        {
                            //SrNo = dt.Rows[i][0].ToString();
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
                            Maturity_Date = dt.Rows[i][12].ToString();
                            Realized_Date = dt.Rows[i][13].ToString();
                            Bill_Amount = dt.Rows[i][14].ToString();
                            Invoice_Sr_No = dt.Rows[i][15].ToString().Replace("'", "");
                            Invoice_No = dt.Rows[i][16].ToString().Replace("'", "");
                            Invoice_Date = dt.Rows[i][17].ToString();
                            Invoice_Amount = dt.Rows[i][18].ToString();
                            Currency = dt.Rows[i][19].ToString();
                            Realized_Amount = dt.Rows[i][20].ToString();
                            Realization_Status = dt.Rows[i][21].ToString();
                            Form_No = dt.Rows[i][22].ToString().Replace("'", "");
                            Shipping_Bill_No = dt.Rows[i][23].ToString().Replace("'", "");
                            Shipping_Bill_Date = dt.Rows[i][24].ToString();
                            Shipping_Bill_Amount = dt.Rows[i][25].ToString();
                            PortCode = dt.Rows[i][26].ToString();
                            TypeOfExport = dt.Rows[i][27].ToString();
                            RecordIndicator = dt.Rows[i][28].ToString();
                            EBRC_No = dt.Rows[i][29].ToString();
                            Closed_Bill_Indication = dt.Rows[i][30].ToString();
                            Export_Agency = dt.Rows[i][31].ToString();
                            Direct_Dispatch_Indicator = dt.Rows[i][32].ToString();
                            Bank_Charges = dt.Rows[i][33].ToString();
                            FOB_Amount = dt.Rows[i][34].ToString();
                            FOB_Amount_IC = dt.Rows[i][35].ToString();
                            Freight_Amount = dt.Rows[i][36].ToString();
                            Freight_Amount_IC = dt.Rows[i][37].ToString();
                            Insurance_Amount = dt.Rows[i][38].ToString();
                            Insurance_Amount_IC = dt.Rows[i][39].ToString();
                            Commission_Amount = dt.Rows[i][40].ToString();
                            Commission_Amount_IC = dt.Rows[i][41].ToString();
                            Packaging_Charges_Amount = dt.Rows[i][42].ToString();
                            Packaging_Charges_Amount_IC = dt.Rows[i][43].ToString();
                            Deduction_Amount = dt.Rows[i][44].ToString();
                            Deduction_Amount_IC = dt.Rows[i][45].ToString();
                            Discount_Amount = dt.Rows[i][46].ToString();
                            Discount_Amount_IC = dt.Rows[i][47].ToString();

                            _FIRCNumber = dt.Rows[i][48].ToString();
                            _FIRCADCode = dt.Rows[i][49].ToString();

                            IsProcessed = "";
                            UploadedBy = Session["userName"].ToString();
                            UploadedDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            p1.Value = "";
                            p2.Value = AD_Code;
                            p3.Value = Document_Date;
                            p4.Value = Negotiation_Date;
                            p5.Value = Document_No;
                            p6.Value = AD_Bill_No;
                            p7.Value = Beneficiary_Name;
                            p8.Value = Beneficiary_Address;
                            p9.Value = IE_Code;
                            p10.Value = Account_No;
                            p11.Value = Applicant_Name;
                            p12.Value = Applicant_Address;
                            p13.Value = Applicant_Country_Code;
                            p14.Value = Maturity_Date;
                            p15.Value = Realized_Date;
                            p16.Value = Invoice_Sr_No;
                            p17.Value = Invoice_No;
                            p18.Value = Invoice_Date;
                            p19.Value = Invoice_Amount;
                            p20.Value = Currency;
                            p21.Value = Realized_Amount;
                            p22.Value = Realization_Status;
                            p23.Value = Form_No;
                            p24.Value = Shipping_Bill_No;
                            p25.Value = Shipping_Bill_Date;
                            p26.Value = PortCode;
                            p27.Value = TypeOfExport;
                            p28.Value = RecordIndicator;
                            p29.Value = EBRC_No;
                            p30.Value = Closed_Bill_Indication;
                            p31.Value = Export_Agency;
                            p32.Value = Direct_Dispatch_Indicator;
                            p33.Value = Bank_Charges;
                            p34.Value = FOB_Amount;
                            p35.Value = FOB_Amount_IC;
                            p36.Value = Freight_Amount;
                            p37.Value = Freight_Amount_IC;
                            p38.Value = Insurance_Amount;
                            p39.Value = Insurance_Amount_IC;
                            p40.Value = Commission_Amount;
                            p41.Value = Commission_Amount_IC;
                            p42.Value = Packaging_Charges_Amount;
                            p43.Value = Packaging_Charges_Amount_IC;
                            p44.Value = Deduction_Amount;
                            p45.Value = Deduction_Amount_IC;
                            p46.Value = Discount_Amount;
                            p47.Value = Discount_Amount_IC;
                            p48.Value = IsProcessed;
                            p49.Value = UploadedBy;
                            p50.Value = UploadedDate;

                            p51.Value = Shipping_Bill_Amount;
                            p52.Value = Bill_Amount;

                            p54.Value = _FIRCNumber;
                            p55.Value = _FIRCADCode;

                            //if (AD_Code == ddlRefNo.SelectedValue)
                            //{
                            //    //  pREF_NO.Value= = Ref_No;
                            TF_DATA objSave = new TF_DATA();
                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23,
                                     p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55);
                            if (result.Substring(0, 8) == "Uploaded")
                            {
                                cntrec = cntrec + 1;
                                _rowData = "File Sr No :" + _FileSrNo.ToString() + " Uploaded successfully with Sr No.: " + result.Substring(8).ToString();
                            }
                            else
                            {
                                _rowData = "File Sr No :" + _FileSrNo.ToString() + " Not uploaded. [ " + result.ToString() + " ]";
                            }
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(),"","alert('Invalid AD Code')",true);
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
        _query = "TF_EDPMS_FileUploadDelete";
        SqlParameter p1 = new SqlParameter("@DocType", "Realized");

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
        DataTable dt = objdata.getData("TF_EDPMS_FileUpload_Validate");
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
        string result = objdata.SaveDeleteData("TF_EDPMS_FileUpload_ProcessRelizationData");
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