using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;

public partial class EXP_EXP_FileUpload_CSV : System.Web.UI.Page
{
    int cntrec = 0;
    int norecinexcel = 0;
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
                cntrec = 0;
                norecinexcel = 0;
                fillBranch();
                txtUploadDate.Attributes.Add("onblur", "return checkSysDate(" + txtUploadDate.ClientID + ");");
                txtUploadDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();

            }
             btnupload.Attributes.Add("onclick", "return validateSave();");

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
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);

    }

    private string Upload_Data_From_Excel(string _filepath)
    {
        FileStream currentFileStream = null;//EDIT

        string path = Server.MapPath("../GeneratedFiles/Uploaded_Files");

        string tempFilePath = path + "\\TEMP.txt";

        norecinexcel = 0;
        string uploadresult = "";

        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
      //  string _DateRecvd = System.DateTime.Now.ToString("dd/MM/yyyy");
        string result = "";
        string _company = "";
        string _docType = "";
        string _custAcNo = "";
        string _overseasPartyID = "";
        string _invoiceNo = "";
        string _invoiceDate = "";
        string _invoiceNoOfDocs = "";
        string _shippmentMode = "";
        string _dateAWB = "";
        string _coveringFrom = "";
        string _coveringTo = "";
        string _countryCode = "";
        string _billType = "";
        string _currency = "";
        string _noOfDays = "";
        string _afterFrom = "";
        string _billAmount = "";
        string _negoAmount = "";
        string _GRcurrency = "";
        string _GRamount = "";
        string _GRshippingBillNo = "";
        string _GRshippingBilldate = "";
        string _commodity = "";
        string _dateOfUpload = "";
        string _srNo = "";
        string _GRType = "";
        string _GRno = "";
        string _GRExchRate = "";


        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileposted";
        try
        {
            System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);

            if (System.IO.File.Exists(_filepath))
            {
                try
                {
                    File.Delete(_filepath);
                    //System.IO.File.Delete(_filepath);
                }

                catch  //or maybe in finally
                {
                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.

                    Thread.Sleep(10000); //Wait for object to be killed. 
                    File.Delete(_filepath); //File can be now deleted

                }
            }

        }
        catch { }

        try
        {
            fileinhouse.PostedFile.SaveAs(_filepath);
            uploadresult = "fileposted";
        }
        catch
        {
            uploadresult = "ioerror";
        }

        if (uploadresult == "fileposted")
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
                norecinexcel = norecinexcel + 1;
            }



            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
            else
            {
                currentFileStream = File.Create(tempFilePath);//creates temp text file
                currentFileStream.Close();//frees the file for editing/reading
            }//if file does not already exist
            File.AppendAllText(tempFilePath, _rowData);

            SqlParameter pBranchName = new SqlParameter("@bName", SqlDbType.VarChar);
            pBranchName.Value = ddlBranch.Text;
            SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
            SqlParameter pCustAcNo = new SqlParameter("@custAcNo", SqlDbType.VarChar);
            SqlParameter pOverseasPartyID = new SqlParameter("@OverseasPartyID", SqlDbType.VarChar);
            SqlParameter pInvoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
            SqlParameter pInvoiceDate = new SqlParameter("@invoiceDate", SqlDbType.VarChar);
            SqlParameter pInvoiceDocs = new SqlParameter("@InvoiceDoc", SqlDbType.VarChar);
            SqlParameter pShippmentMode = new SqlParameter("@Steamer", SqlDbType.VarChar);
            SqlParameter pDateAWB = new SqlParameter("@AWBdate", SqlDbType.VarChar);
            SqlParameter pCoveringFrom = new SqlParameter("@CoveringFrom", SqlDbType.VarChar);
            SqlParameter pCoveringTo = new SqlParameter("@CoveringTo", SqlDbType.VarChar);
            SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);
            SqlParameter pBillType = new SqlParameter("@billType", SqlDbType.VarChar);
            SqlParameter pCurrency = new SqlParameter("@Curr", SqlDbType.VarChar);
            SqlParameter pNoOfDays = new SqlParameter("@noOfDays", SqlDbType.VarChar);
            SqlParameter pAfterFrom = new SqlParameter("@afterFrom", SqlDbType.VarChar);
            SqlParameter pBillAmount = new SqlParameter("@ActbillAmt", SqlDbType.VarChar);
            SqlParameter pNegoAmount = new SqlParameter("@NegotiatedAmt", SqlDbType.VarChar);
            SqlParameter pGRcurrency = new SqlParameter("@GRcurrency", SqlDbType.VarChar);
            SqlParameter pGRamount = new SqlParameter("@GRamount", SqlDbType.VarChar);
            SqlParameter pGRshippingBillNo = new SqlParameter("@GRshippingBillNo", SqlDbType.VarChar);
            SqlParameter pGRshippingBillDate = new SqlParameter("@GRshippingBillDate", SqlDbType.VarChar);
            SqlParameter pCommodity = new SqlParameter("@Commodity", SqlDbType.VarChar);
            SqlParameter pDateRcvd = new SqlParameter("@dateRcvd", SqlDbType.VarChar);

            SqlParameter pGRno = new SqlParameter("@Grno", SqlDbType.VarChar);
            SqlParameter pGRType = new SqlParameter("@GrType", SqlDbType.VarChar);
            SqlParameter pGRExchRate = new SqlParameter("@GrExchRate", SqlDbType.VarChar);

            SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
            pUser.Value = _userName;
            SqlParameter pUploadingDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
            pUploadingDate.Value = _uploadingDate;

            string _query = "TF_EXP_UploadExportEntryDetails";

            try
            {
               
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim() != "")
                        {
                            _srNo = dt.Rows[i][0].ToString();
                            _company = dt.Rows[i][1].ToString();
                            _docType = dt.Rows[i][2].ToString();
                            _custAcNo = dt.Rows[i][3].ToString();
                            _overseasPartyID = dt.Rows[i][5].ToString();
                            _invoiceNo = dt.Rows[i][7].ToString();
                            _invoiceDate = dt.Rows[i][8].ToString();
                            _invoiceNoOfDocs = dt.Rows[i][9].ToString();
                            _shippmentMode = dt.Rows[i][10].ToString();
                            _dateAWB = dt.Rows[i][11].ToString();
                            _coveringFrom = dt.Rows[i][12].ToString();
                            _coveringTo = dt.Rows[i][14].ToString();
                            _countryCode = dt.Rows[i][15].ToString();
                            _billType = dt.Rows[i][16].ToString();
                            _currency = dt.Rows[i][17].ToString();
                            _noOfDays = dt.Rows[i][18].ToString();
                            _afterFrom = dt.Rows[i][19].ToString();
                            _billAmount = dt.Rows[i][20].ToString();
                            _negoAmount = dt.Rows[i][21].ToString();
                            _GRcurrency = dt.Rows[i][22].ToString();
                            _GRamount = dt.Rows[i][23].ToString();
                            _GRshippingBillNo = dt.Rows[i][24].ToString();
                            _GRshippingBilldate = dt.Rows[i][25].ToString();
                            _commodity = dt.Rows[i][26].ToString();
                            _GRType = dt.Rows[i][27].ToString();
                            _GRno = dt.Rows[i][28].ToString();
                            _GRExchRate = dt.Rows[i][29].ToString();
                            _dateOfUpload = dt.Rows[i][30].ToString();

                            if (_dateOfUpload != txtUploadDate.Text)
                            {
                                uploadresult = "Check Upload Date in Excel File";
                                return uploadresult;
                            }
                            if (_company != txtCoID.Text)
                            {
                                uploadresult = "Check Co ID in Excel File";
                                return uploadresult;
                            }

                            pBranchName.Value = ddlBranch.Text;
                            pDocType.Value = _docType;
                            pCustAcNo.Value = _custAcNo;
                            pOverseasPartyID.Value = _overseasPartyID;
                            pInvoiceNo.Value = _invoiceNo;
                            pInvoiceDate.Value = _invoiceDate;
                            pInvoiceDocs.Value = _invoiceNoOfDocs;
                            pShippmentMode.Value = _shippmentMode;
                            pDateAWB.Value = _dateAWB;
                            pCoveringFrom.Value = _coveringFrom;
                            pCoveringTo.Value = _coveringTo;
                            pCountryCode.Value = _countryCode;
                            pBillType.Value = _billType;
                            pCurrency.Value = _currency;
                            pNoOfDays.Value = _noOfDays;
                            pAfterFrom.Value = _afterFrom;
                            pBillAmount.Value = _billAmount;
                            pNegoAmount.Value = _negoAmount;
                            pCommodity.Value = _commodity;
                            pDateRcvd.Value = _dateOfUpload; //_DateRecvd;
                            pGRamount.Value = _GRamount;
                            pGRcurrency.Value = _GRcurrency;
                            pGRshippingBillNo.Value = _GRshippingBillNo;
                            pGRshippingBillDate.Value = _GRshippingBilldate;
                            pGRno.Value = _GRno;
                            pGRType.Value = _GRType;
                            pGRExchRate.Value = _GRExchRate;


                            TF_DATA objSave = new TF_DATA();
                            result = objSave.SaveDeleteData(_query, pBranchName, pDocType, pCustAcNo, pOverseasPartyID,
                                                pInvoiceNo, pInvoiceDate, pInvoiceDocs, pShippmentMode,
                                                pDateAWB, pCoveringFrom, pCoveringTo, pCountryCode,
                                                pBillType, pCurrency, pNoOfDays, pAfterFrom, pBillAmount, pNegoAmount,
                                                pCommodity, pDateRcvd, pGRamount, pGRcurrency, pGRshippingBillNo, pGRshippingBillDate, pUser, pUploadingDate
                                                ,pGRno,pGRExchRate,pGRType
                                                );


                            if (result.Substring(0, 8) == "Uploaded")
                            {
                                cntrec = cntrec + 1;
                                _rowData = "Sr No :" + _srNo + " uploaded successfully with Document No.: " + result.Substring(8);
                            }
                            else
                            {
                                _rowData = "Sr No :" + _srNo + "  " + result;
                            }

                            if (_rowData != "")
                                File.AppendAllText(tempFilePath, Environment.NewLine + _rowData);//appends all text in temp file
                        }
                    }

                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    labelMessage.Text = errorMsg;
                }

                uploadresult = "uploaded";

                //if (File.Exists(tempFilePath))
                //    File.Delete(tempFilePath);//delete temp file

            }
            catch (System.Exception ex)
            {
                // uploadresult = "error";
                uploadresult = ex.Message;
                // oconn.Close();
                GC.Collect();
            }
            return uploadresult;
        }
        else
            return uploadresult;

    }


    protected void btnupload_Click(object sender, EventArgs e)
    {
        norecinexcel = 0;
        string path = Server.MapPath("../GeneratedFiles/Uploaded_Files");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        //string result = Upload_Data_From_Excel(fileinhouse.PostedFile.FileName);
        string result = Upload_Data_From_Excel(path + "\\" + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName));
        labelMessage.Font.Size = 11;
      
        if (result == "uploaded")
        {
            
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

            string link = "Click here";
            string tempfilepath = "file://" + _serverName + "\\GeneratedFiles\\Uploaded_Files\\temp.txt";

            labelMessage.Text = cntrec + " record(s) " + "Uploaded out of " + norecinexcel + " from file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName) + ", to view Document Reference no " + "<a href=" + tempfilepath + " target='_blank'> " + link + " </a>";
        }
        else
        {
            labelMessage.Text = result;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        GC.Collect();
        ddlBranch.SelectedIndex = 0;
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Exp_Main.aspx", true);
    }
}
