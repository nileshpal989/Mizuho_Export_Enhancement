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

public partial class EXP_EXP_FileUpload : System.Web.UI.Page
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
        //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        //dateInfo.ShortDatePattern = "dd/MM/yyyy";

        string _userName = Session["userName"].ToString().Trim();
         string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
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
            OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _filepath + ";Extended Properties=Excel 8.0");
            //OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _filepath + ";Extended Properties=Excel 12.0 Xml");
            DataTable dtS;
            using (OleDbConnection c = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _filepath + ";Extended Properties=Excel 8.0"))
            {
                c.Open();
                dtS = c.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                  new object[] { null, null, null, "TABLE" });
                c.Close();
            }
            string sheetname = dtS.Rows[0]["TABLE_NAME"].ToString().Trim();
            string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
           

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

            SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
            pUser.Value = _userName;
            SqlParameter pUploadingDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
            pUploadingDate.Value = _uploadingDate;

            string _query = "TF_EXP_UploadExportEntryDetails";

            try
            {
                norecinexcel = 0;

                //OleDbCommand ocmd = new OleDbCommand("select * from [Sheet1$]", oconn);
                OleDbCommand ocmd = new OleDbCommand("select * from [" + sheetname + "]", oconn);
                oconn.Open();

                OleDbDataReader dr = ocmd.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    norecinexcel = 0;
                    do
                    {
                        if (dr[0] != null)
                        {
                            if (dr[0].ToString() != "") //------ Ignore records without SrNo
                            {
                                norecinexcel = norecinexcel + 1;

                                if (dr[1] != null)
                                {
                                    if (dr[1].ToString() != "")
                                    {
                                        _company = dr[1].ToString().Trim();
                                    }
                                    else
                                        _company = "";
                                }
                                else
                                    _company = "";


                                if (dr[2] != null)
                                {
                                    if (dr[2].ToString() != "")
                                        _docType = dr[2].ToString().Trim();
                                    else
                                        _docType = "";
                                }
                                else
                                    _docType = "";


                                if (dr[3] != null)
                                {
                                    if (dr[3].ToString() != "")
                                        _custAcNo = dr[3].ToString().Trim();
                                    else
                                        _custAcNo = "";
                                }
                                else
                                    _custAcNo = "";


                                if (dr[5] != null)
                                {
                                    if (dr[5].ToString() != "")
                                        _overseasPartyID = dr[5].ToString().Trim();
                                    else
                                        _overseasPartyID = "";
                                }
                                else
                                    _overseasPartyID = "";

                                if (dr[7] != null)
                                {
                                    if (dr[7].ToString() != "")
                                        _invoiceNo = dr[7].ToString().Trim();
                                    else
                                        _invoiceNo = "";
                                }
                                else
                                    _invoiceNo = "";

                                if (dr[8] != null)
                                {
                                    if (dr[8].ToString() != "")
                                    {
                                        _invoiceDate = dr[8].ToString().Trim();
                                    }
                                    else
                                        _invoiceDate = "";
                                }
                                else
                                    _invoiceDate = "";

                                if (dr[9] != null)
                                {
                                    if (dr[9].ToString() != "")
                                    {
                                        _invoiceNoOfDocs = dr[9].ToString().Trim();
                                    }
                                    else
                                        _invoiceNoOfDocs = "";
                                }
                                else
                                    _invoiceNoOfDocs = "";

                                if (dr[10] != null)
                                {
                                    if (dr[10].ToString() != "")
                                    {
                                        _shippmentMode = dr[10].ToString().Trim();
                                    }
                                    else
                                        _shippmentMode = "";
                                }
                                else
                                    _shippmentMode = "";

                                if (dr[11] != null)
                                {
                                    if (dr[11].ToString() != "")
                                    {
                                        _dateAWB = dr[11].ToString().Trim();
                                    }
                                    else
                                        _dateAWB = "";
                                }
                                else
                                    _dateAWB = "";


                                if (dr[12] != null)
                                {
                                    if (dr[12].ToString() != "")
                                    {
                                        _coveringFrom = dr[12].ToString().Trim();
                                    }
                                    else
                                        _coveringFrom = "";
                                }
                                else
                                    _coveringFrom = "";


                                if (dr[14] != null)
                                {
                                    if (dr[14].ToString() != "")
                                        _coveringTo = dr[14].ToString().Trim();
                                    else
                                        _coveringTo = "";
                                }
                                else
                                    _coveringTo = "";


                                if (dr[15] != null)
                                {
                                    if (dr[15].ToString() != "")
                                        _countryCode = dr[15].ToString();
                                    else
                                        _countryCode = "";
                                }
                                else
                                    _countryCode = "";

                                if (dr[16] != null)
                                {
                                    if (dr[16].ToString() != "")
                                        _billType = dr[16].ToString().Trim();
                                    else
                                        _billType = "";
                                }
                                else
                                    _billType = "";

                                if (dr[17] != null)
                                {
                                    if (dr[17].ToString() != "")
                                        _currency = dr[17].ToString().Trim();
                                    else
                                        _currency = "";
                                }
                                else
                                    _currency = "";

                                if (dr[18] != null)
                                {
                                    if (dr[18].ToString() != "")
                                        _noOfDays = dr[18].ToString().Trim();
                                    else
                                        _noOfDays = "";
                                }
                                else
                                    _noOfDays = "";

                                if (dr[19] != null)
                                {
                                    if (dr[19].ToString() != "")
                                    {
                                        _afterFrom = dr[19].ToString().Trim();
                                        _afterFrom = _afterFrom.Substring(0, 1);
                                    }
                                    else
                                        _afterFrom = "";
                                }
                                else
                                    _afterFrom = "";


                                if (dr[20] != null)
                                {
                                    if (dr[20].ToString() != "")
                                        _billAmount = dr[20].ToString().Trim();
                                    else
                                        _billAmount = "";
                                }
                                else
                                    _billAmount = "";

                                if (dr[21] != null)
                                {
                                    if (dr[21].ToString() != "")
                                        _negoAmount = dr[21].ToString().Trim();
                                    else
                                        _negoAmount = "";
                                }
                                else
                                    _negoAmount = "";

                                if (dr[22] != null)
                                {
                                    if (dr[22].ToString() != "")
                                        _GRcurrency = dr[22].ToString().Trim();
                                    else
                                        _GRcurrency = "";
                                }
                                else
                                    _GRcurrency = "";

                                if (dr[23] != null)
                                {
                                    if (dr[23].ToString() != "")
                                        _GRamount = dr[23].ToString().Trim();
                                    else
                                        _GRamount = "";
                                }
                                else
                                    _GRamount = "";

                                if (dr[24] != null)
                                {
                                    if (dr[24].ToString() != "")
                                        _GRshippingBillNo = dr[24].ToString().Trim();
                                    else
                                        _GRshippingBillNo = "";
                                }
                                else
                                    _GRshippingBillNo = "";

                                if (dr[25] != null)
                                {
                                    if (dr[25].ToString() != "")
                                        _GRshippingBilldate = dr[25].ToString().Trim();
                                    else
                                        _GRshippingBilldate = "";
                                }
                                else
                                    _GRshippingBilldate = "";

                                if (dr[26] != null)
                                {
                                    if (dr[26].ToString() != "")
                                        _commodity = dr[26].ToString().Trim();
                                    else
                                        _commodity = "";
                                }
                                else
                                    _commodity = "";

                                if (dr[27] != null)
                                {
                                    if (dr[27].ToString() != "")
                                        _dateOfUpload = dr[27].ToString().Trim();
                                    else
                                        _dateOfUpload = "";
                                }
                                else
                                    _dateOfUpload = "";

                                try
                                {
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
                                        pDateRcvd.Value = _dateOfUpload;
                                        pGRamount.Value = _GRamount;
                                        pGRcurrency.Value = _GRcurrency;
                                        pGRshippingBillNo.Value = _GRshippingBillNo;
                                        pGRshippingBillDate.Value = _GRshippingBilldate;

                                        TF_DATA objSave = new TF_DATA();
                                        result = objSave.SaveDeleteData(_query, pBranchName, pDocType, pCustAcNo, pOverseasPartyID,
                                                            pInvoiceNo, pInvoiceDate, pInvoiceDocs, pShippmentMode,
                                                            pDateAWB, pCoveringFrom, pCoveringTo, pCountryCode,
                                                            pBillType, pCurrency, pNoOfDays, pAfterFrom, pBillAmount, pNegoAmount,
                                                            pCommodity, pDateRcvd, pGRamount, pGRcurrency, pGRshippingBillNo, pGRshippingBillDate, pUser, pUploadingDate);
                                
                                    
                                   
                                    if (result.Substring(0, 8) == "Uploaded")
                                    {
                                        cntrec = cntrec + 1;
                                        _rowData = "Sr No :" + dr[0].ToString() + " uploaded successfully with Document No.: " + result.Substring(8);
                                    }
                                    else
                                    {
                                        _rowData = "Sr No :" + dr[0].ToString() + "  " + result;
                                    }

                                    if (_rowData != "")
                                        File.AppendAllText(tempFilePath, Environment.NewLine + _rowData);//appends all text in temp file


                                }
                                catch (Exception ex)
                                {
                                    //dt.Clear();
                                    string errorMsg = ex.Message;
                                    labelMessage.Text = errorMsg;
                                }

                            }
                        }
                    } while (dr.Read());

                }

                try
                {
                    System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);
                    if (System.IO.File.Exists(_filepath))
                    {
                        try
                        {
                            File.Delete(_filepath);
                        }

                        catch  //or maybe in finally
                        {
                            GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                            Thread.Sleep(500); //Wait for object to be killed. 
                            File.Delete(_filepath); //File can be now deleted
                        }
                    }

                }
                catch { }

                uploadresult = "uploaded";
                oconn.Close();
                GC.Collect();

                //if (File.Exists(tempFilePath))
                //    File.Delete(tempFilePath);//delete temp file

            }
            catch (System.Exception ex)
            {
                // uploadresult = "error";
                uploadresult = ex.Message;
                oconn.Close();
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
          //labelMessage.Text = cntrec + " record(s) " + "Uploaded out of " + norecinexcel + " from file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

            string link = "Click here";
            string tempfilepath = "file://" + _serverName + "\\GeneratedFiles\\Uploaded_Files\\temp.txt";
            
            labelMessage.Text = "File uploaded successfully, to view Document Reference no " + "<a href=" + tempfilepath + " target='_blank'> " + link + " </a>";
        }
        else
        {
            labelMessage.Text = result;
        }

    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Exp_Main.aspx", true);
    }
}
