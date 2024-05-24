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

public partial class EDPMS_EDPMS_OUTSTANDING_BILLS_FILE_UPLOAD : System.Web.UI.Page
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

        string Document_No, Document_Type, Bill_Type="", CustAcNo, Overseas_Party_Name, Commodity, Steamer,
               AWB_Date, DueDate ,Country_Code, Currency, Days, Bill_Amount, ActBillAmt, GRCurrency, Amount, Shipping_Bill_No, Shipping_Bill_Date,
               FormType, GR, InvoiceNo, InvoiceDate, InsuranceAmount, FreightAmount, DiscountAmt, Commission, OtherDeductionAmt
               , PackingCharges, ExportAgency, DispInd,Invoice_Amt,Date_Received,PortCode
    ;
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

            SqlParameter p1 = new SqlParameter("@Document_Type", SqlDbType.VarChar);
            SqlParameter p2 = new SqlParameter("@Bill_Type", SqlDbType.VarChar);
            SqlParameter p3 = new SqlParameter("@Document_No", SqlDbType.VarChar);
            SqlParameter p4 = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
            SqlParameter p5 = new SqlParameter("@Overseas_Party_Name", SqlDbType.VarChar);
            SqlParameter p6 = new SqlParameter("@Commodity", SqlDbType.VarChar);
            SqlParameter p8 = new SqlParameter("@AWB_Date", SqlDbType.VarChar);
            SqlParameter p9 = new SqlParameter("@Country_Code", SqlDbType.VarChar);
            SqlParameter p10 = new SqlParameter("@Currency", SqlDbType.VarChar);
            SqlParameter p11 = new SqlParameter("@Days", SqlDbType.VarChar);
            SqlParameter p12 = new SqlParameter("@From_AWB_Date", SqlDbType.VarChar);
            SqlParameter p13 = new SqlParameter("@Bill_Amount", SqlDbType.VarChar);
            SqlParameter p14 = new SqlParameter("@ActBillAmt", SqlDbType.VarChar);
            SqlParameter p15 = new SqlParameter("@GRCurrency", SqlDbType.VarChar);
            SqlParameter p16 = new SqlParameter("@Amount", SqlDbType.VarChar);
            SqlParameter p17 = new SqlParameter("@Shipping_Bill_No", SqlDbType.VarChar);
            SqlParameter p18 = new SqlParameter("@Shipping_Bill_Date", SqlDbType.VarChar);
            SqlParameter p19 = new SqlParameter("@FormType", SqlDbType.VarChar);
            SqlParameter p20 = new SqlParameter("@GR", SqlDbType.VarChar);

            SqlParameter p21 = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
            SqlParameter p22 = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
            SqlParameter p23 = new SqlParameter("@InsuranceAmount", SqlDbType.VarChar);
            SqlParameter p24 = new SqlParameter("@FreightAmount", SqlDbType.VarChar);

            SqlParameter p25 = new SqlParameter("@DiscountAmt", SqlDbType.VarChar);
            SqlParameter p26 = new SqlParameter("@Commission", SqlDbType.VarChar);
            SqlParameter p27 = new SqlParameter("@OtherDeductionAmt", SqlDbType.VarChar);
            SqlParameter p28 = new SqlParameter("@PackingCharges", SqlDbType.VarChar);
            SqlParameter p29 = new SqlParameter("@ExportAgency", SqlDbType.VarChar);
            SqlParameter p30 = new SqlParameter("@DispInd", SqlDbType.VarChar);
            SqlParameter p31 = new SqlParameter("@InvoiceAmt", SqlDbType.VarChar);
            SqlParameter p32 = new SqlParameter("@Date_Received", SqlDbType.VarChar);
            SqlParameter p33 = new SqlParameter("@DueDate", SqlDbType.VarChar);
            SqlParameter p34 = new SqlParameter("@User", SqlDbType.VarChar);
            SqlParameter p35 = new SqlParameter("@UploadDate", SqlDbType.VarChar);
            SqlParameter p36 = new SqlParameter("@PortCode", SqlDbType.VarChar);

            string _userName = Session["userName"].ToString().Trim();
            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            string _qry = "TF_EDPMS_Delete_Temp_Outstanding";
            string Result = objSave.SaveDeleteData(_qry);

            string _query = "TF_EDPMS_Outstanding_FILE_UPLOAD";
            try
            {
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim() != "")
                        {
                            //SrNo = dt.Rows[i][0].ToString();
                            Document_Type = dt.Rows[i][1].ToString().Substring(0,3);

                            if (dt.Rows[i][1].ToString().Substring(0, 3) == "BLA" || dt.Rows[i][1].ToString().Substring(0, 3) == "BCA" || dt.Rows[i][1].ToString().Substring(0, 3) == "BBA")
                            {
                                Bill_Type = "S";
                            }
                            else if (dt.Rows[i][1].ToString().Substring(0, 3) == "BLU" || dt.Rows[i][1].ToString().Substring(0, 3) == "BCU" || dt.Rows[i][1].ToString().Substring(0, 3) == "BBU")
                            {
                                Bill_Type = "U";
                            }
                            
                            Document_No = dt.Rows[i][1].ToString();
                            CustAcNo = dt.Rows[i][2].ToString();
                            Overseas_Party_Name = dt.Rows[i][5].ToString();
                            Commodity = dt.Rows[i][6].ToString();
                            AWB_Date = dt.Rows[i][7].ToString();
                            Country_Code = dt.Rows[i][8].ToString();
                            Currency = dt.Rows[i][9].ToString();
                            Days = dt.Rows[i][10].ToString();
                            DueDate = dt.Rows[i][12].ToString();
                            Bill_Amount = dt.Rows[i][13].ToString();
                            ActBillAmt = dt.Rows[i][14].ToString();


                            GRCurrency = dt.Rows[i][15].ToString();
                            Amount = dt.Rows[i][16].ToString();
                            Shipping_Bill_No = dt.Rows[i][17].ToString();
                            Shipping_Bill_Date = dt.Rows[i][18].ToString();
                            FormType = dt.Rows[i][19].ToString();
                            GR= dt.Rows[i][20].ToString();
                            InvoiceNo = dt.Rows[i][21].ToString();
                            InvoiceDate = dt.Rows[i][22].ToString();
                            InsuranceAmount = dt.Rows[i][23].ToString();
                            FreightAmount= dt.Rows[i][24].ToString();
                            DiscountAmt = dt.Rows[i][25].ToString();
                            Commission = dt.Rows[i][26].ToString();
                            OtherDeductionAmt= dt.Rows[i][27].ToString();
                            PackingCharges= dt.Rows[i][28].ToString();
                            ExportAgency= dt.Rows[i][29].ToString();
                            DispInd= dt.Rows[i][30].ToString();
                            Invoice_Amt = dt.Rows[i][32].ToString();
                            Date_Received = dt.Rows[i][33].ToString();
                            PortCode = dt.Rows[i][34].ToString();


                            p1.Value=Document_Type;
                            p2.Value = Bill_Type; 
                            p3.Value=Document_No;
                            p4.Value=CustAcNo;
                            p5.Value = Overseas_Party_Name; 
                            p6.Value=Commodity; 
                            p8.Value=AWB_Date; 
                            p9.Value=Country_Code; 
                            p10.Value=Currency; 
                            p11.Value=Days;
                            p12.Value = AWB_Date;
                            p13.Value=Bill_Amount; 
                            p14.Value=ActBillAmt; 
                            p15.Value=GRCurrency; 
                            p16.Value=Amount; 
                            p17.Value=Shipping_Bill_No; 
                            p18.Value=Shipping_Bill_Date;
                            p19.Value=FormType; 
                            p20.Value=GR; 
                            p21.Value=InvoiceNo; 
                            p22.Value=InvoiceDate; 
                            p23.Value=InsuranceAmount; 
                            p24.Value=FreightAmount; 
                            p25.Value=DiscountAmt; 
                            p26.Value=Commission; 
                            p27.Value=OtherDeductionAmt; 
                            p28.Value=PackingCharges; 
                            p29.Value=ExportAgency;
                            p30.Value = DispInd;
                            p31.Value = Invoice_Amt;
                            p32.Value = Date_Received;
                            p33.Value = DueDate;
                            p34.Value = _userName;
                            p35.Value = _uploadingDate;
                            p36.Value = PortCode;

                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6,p8 ,p9, p10, p11, p12, p13, p14, p15, p16,
                                                                    p17,p18,p19,p20,p21,p22,p23,p24,p25,p26,p27,p28,p29,p30,p31,p32,p33,p34,p35,p36);
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
        DataTable dt = objdata.getData("TF_EDPMS_OUTSTANDING_FileUpload_Validate");
        if (dt.Rows.Count > 0)
        {
            script = "window.open('EDPMS_rpt_OUTSTANDING__Data_Validation.aspx?Branch=" + ddlRefNo.SelectedItem.Text + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
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
        string result = objdata.SaveDeleteData("TF_EDPMS_OUTPUT_File_Upload_ProcessData");
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