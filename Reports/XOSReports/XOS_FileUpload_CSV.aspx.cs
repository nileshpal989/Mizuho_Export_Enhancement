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

public partial class XOS_XOS_FileUpload_CSV : System.Web.UI.Page
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
                //fillBranch();
                //txtUploadDate.Attributes.Add("onblur", "return checkSysDate(" + txtUploadDate.ClientID + ");");
                //txtUploadDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                //ddlBranch.Focus();

            }

            btnupload.Attributes.Add("onclick", "return validateSave();");

        }

    }

    //protected void fillBranch()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetBranchDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlBranch.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "---Select---";
    //        ddlBranch.DataSource = dt.DefaultView;
    //        ddlBranch.DataTextField = "BranchName";
    //        ddlBranch.DataValueField = "BranchName";
    //        ddlBranch.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";

    //    ddlBranch.Items.Insert(0, li);

    //}

    private string Upload_Data_From_Excel(string _filepath)
    {
        FileStream currentFileStream = null;//EDIT

        string path = Server.MapPath("../GeneratedFiles/Uploaded_Files");

        //string tempFilePath = path + "\\TEMP.txt";

        norecinexcel = 0;
        string uploadresult = "";

        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string result = "";

        string _Remark = "";
        string _RealizationDt = "";
        string _RealizedBill = "";
        string _BuyerPinCode = "";
        string _BuyerCountry = "";

        string _BuyerAddress2 = "";
        string _BuyerAddress1 = "";
        string _BuyerName = "";
        string _OSINRAmt = "";
        string _RealizedINRAmt = "";
        string _RealizedCur = "";
        string _InvoiceINRAmt = "";
        string _InvoiceFCAmt = "";

        string _InvoiceCur = "";
        string _Commodity = "";
        string _ExpCountry = "";
        string _ExtnGrantedUpTo = "";
        string _ExtnGrantingAuthority = "";
        string _ExtnGranted = "";
        string _RealizationDueDt = "";
        string _ExpDate = "";
        string _GRPPSDFno = "";
        string _ShippingBillDt = "";
        string _ShippingBillNo = "";
        string _PortCode = "";
        string _ShipmentPort = "";
        string _PinCode = "";
        string _City = "";
        string _ExpoterAddress2 = "";
        string _ExpoterAddress1 = "";
        string _ExpoterName = "";
        string _StatusHolder = "";
        string _ExpBillDate = "";
        string _ExpBillNo = "";
        string _Export = "";
        string _IECcode = "";
        string _XOSperiod = "";
        string _ADcode = "";        
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
            string[] value1=new string[36]  ;
            DataTable dt = new DataTable();
            DataRow row;
            int columnno = 0,n=0;
            foreach (string dc in value) 
            {
                if (columnno < 36)
                dt.Columns.Add(new DataColumn(columnno.ToString()));
                columnno++;
            }
            
            while (!sr.EndOfStream)
            {
                if(n!=0)
                value = sr.ReadLine().Split(',');
                for (int u=0; u <= 35; u++)
                {
                     value1[u] = value[u];
                }
                    row = dt.NewRow();
                    row.ItemArray = value1;
                    dt.Rows.Add(row);
               
                norecinexcel = norecinexcel + 1;
                n = 1;
            }

            TF_DATA objDelete = new TF_DATA();
            string _query1 = "Delete_XOS_CSV_Invalid_Data";
            DataTable dt1 = objDelete.getData(_query1);

            SqlParameter pADcode = new SqlParameter("@ADcode", SqlDbType.VarChar);
            SqlParameter pXOSperiod = new SqlParameter("@XOSperiod", SqlDbType.VarChar);
            SqlParameter pIECcode = new SqlParameter("@IECcode", SqlDbType.VarChar);
            SqlParameter pExport = new SqlParameter("@Export", SqlDbType.VarChar);
            SqlParameter pExpBillNo = new SqlParameter("@ExpBillNo", SqlDbType.VarChar);
            SqlParameter pExpBillDate = new SqlParameter("@ExpBillDate", SqlDbType.VarChar);
            SqlParameter pStatusHolder = new SqlParameter("@StatusHolder", SqlDbType.VarChar);
            SqlParameter pExpoterName = new SqlParameter("@ExpoterName", SqlDbType.VarChar);
            SqlParameter pExpoterAddress1 = new SqlParameter("@ExpoterAddress1", SqlDbType.VarChar);
            SqlParameter pExpoterAddress2 = new SqlParameter("@ExpoterAddress2", SqlDbType.VarChar);
            SqlParameter pCity = new SqlParameter("@City", SqlDbType.VarChar);
            SqlParameter pPinCode = new SqlParameter("@PinCode", SqlDbType.VarChar);
            SqlParameter pShipmentPort = new SqlParameter("@ShipmentPort", SqlDbType.VarChar);
            SqlParameter pPortCode = new SqlParameter("@PortCode", SqlDbType.VarChar);
            SqlParameter pShippingBillNo = new SqlParameter("@ShippingBillNo", SqlDbType.VarChar);
            SqlParameter pShippingBillDt = new SqlParameter("@ShippingBillDt", SqlDbType.VarChar);
            SqlParameter pGRPPSDFno = new SqlParameter("@GRPPSDFno", SqlDbType.VarChar);
            SqlParameter pExpDate = new SqlParameter("@ExpDate", SqlDbType.VarChar);
            SqlParameter pRealizationDueDt = new SqlParameter("@RealizationDueDt", SqlDbType.VarChar);
            SqlParameter pExtnGranted = new SqlParameter("@ExtnGranted", SqlDbType.VarChar);
            SqlParameter pExtnGrantingAuthority = new SqlParameter("@ExtnGrantingAuthority", SqlDbType.VarChar);
            SqlParameter pExtnGrantedUpTo = new SqlParameter("@ExtnGrantedUpTo", SqlDbType.VarChar);
            SqlParameter pExpCountry = new SqlParameter("@ExpCountry", SqlDbType.VarChar);
            SqlParameter pCommodity = new SqlParameter("@Commodity", SqlDbType.VarChar);

            SqlParameter pInvoiceCur = new SqlParameter("@InvoiceCur", SqlDbType.VarChar);
            SqlParameter pInvoiceFCAmt = new SqlParameter("@InvoiceFCAmt", SqlDbType.VarChar);
            SqlParameter pInvoiceINRAmt = new SqlParameter("@InvoiceINRAmt", SqlDbType.VarChar);
            SqlParameter pRealizedCur = new SqlParameter("@RealizedCur", SqlDbType.VarChar);
            SqlParameter pRealizedINRAmt = new SqlParameter("@RealizedINRAmt", SqlDbType.VarChar);
            SqlParameter pOSINRAmt = new SqlParameter("@OSINRAmt", SqlDbType.VarChar);
            SqlParameter pBuyerName = new SqlParameter("@BuyerName", SqlDbType.VarChar);
            SqlParameter pBuyerAddress1 = new SqlParameter("@BuyerAddress1", SqlDbType.VarChar);
            SqlParameter pBuyerAddress2 = new SqlParameter("@BuyerAddress2", SqlDbType.VarChar);
            SqlParameter pBuyerCountry = new SqlParameter("@BuyerCountry", SqlDbType.VarChar);
            SqlParameter pBuyerPinCode = new SqlParameter("@BuyerPinCode", SqlDbType.VarChar);
            SqlParameter pRealizedBill = new SqlParameter("@RealizedBill", SqlDbType.VarChar);
            SqlParameter pRealizationDt = new SqlParameter("@RealizationDt", SqlDbType.VarChar);
            SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.VarChar);


            SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
            pUser.Value = _userName;
            SqlParameter pUploadingDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
            pUploadingDate.Value = _uploadingDate;
            
           

            try
            {

                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _ADcode = dt.Rows[i][0].ToString();
                        _XOSperiod = dt.Rows[i][1].ToString();
                        _IECcode = dt.Rows[i][2].ToString();
                        _Export = dt.Rows[i][3].ToString();
                        _ExpBillNo = dt.Rows[i][4].ToString();
                        _ExpBillDate = dt.Rows[i][5].ToString();
                        _StatusHolder = dt.Rows[i][6].ToString();
                        _ExpoterName = dt.Rows[i][7].ToString();
                        _ExpoterAddress1 = dt.Rows[i][8].ToString();
                        _ExpoterAddress2 = dt.Rows[i][9].ToString();
                        _City = dt.Rows[i][10].ToString();
                        _PinCode = dt.Rows[i][11].ToString();
                        _ShipmentPort = dt.Rows[i][12].ToString();
                        _PortCode = dt.Rows[i][13].ToString();
                        _ShippingBillNo = dt.Rows[i][14].ToString();
                        _ShippingBillDt = dt.Rows[i][15].ToString();
                        _GRPPSDFno = dt.Rows[i][16].ToString();
                        _ExpDate = dt.Rows[i][17].ToString();
                        _RealizationDueDt = dt.Rows[i][18].ToString();
                        _ExtnGranted = dt.Rows[i][19].ToString();
                        _ExtnGrantingAuthority = dt.Rows[i][20].ToString();
                        _ExtnGrantedUpTo = dt.Rows[i][21].ToString();
                        _ExpCountry = dt.Rows[i][22].ToString();
                        _Commodity = dt.Rows[i][23].ToString();
                        _InvoiceCur = dt.Rows[i][24].ToString();

                        _InvoiceFCAmt = dt.Rows[i][25].ToString();
                        _InvoiceINRAmt = dt.Rows[i][26].ToString();
                        _RealizedCur = dt.Rows[i][27].ToString();
                        _RealizedINRAmt = dt.Rows[i][28].ToString();
                        _OSINRAmt = dt.Rows[i][29].ToString();
                        _BuyerName = dt.Rows[i][30].ToString();
                        _BuyerAddress1 = dt.Rows[i][31].ToString();
                        _BuyerAddress2 = dt.Rows[i][32].ToString();
                        _BuyerCountry = dt.Rows[i][33].ToString();
                        _BuyerPinCode = dt.Rows[i][34].ToString();
                        _RealizedBill = dt.Rows[i][35].ToString();

                        //_RealizationDt = dt.Rows[i][36].ToString();
                        //_Remark = dt.Rows[i][37].ToString();
                        

                        //if (_dateOfUpload != txtUploadDate.Text)
                        //{
                        //    uploadresult = "Check Upload Date in Excel File";
                        //    return uploadresult;
                        //}
                        //if (_company != txtCoID.Text)
                        //{
                        //    uploadresult = "Check Co ID in Excel File";
                        //    return uploadresult;
                        //}

                        pADcode.Value = _ADcode;
                        pXOSperiod.Value = _XOSperiod;
                        pIECcode.Value = _IECcode;
                        pExport.Value = _Export;
                        pExpBillNo.Value = _ExpBillNo;
                        pExpBillDate.Value = _ExpBillDate;
                        pStatusHolder.Value = _StatusHolder;
                        pExpoterName.Value = _ExpoterName;
                        pExpoterAddress1.Value = _ExpoterAddress1;
                        pExpoterAddress2.Value = _ExpoterAddress2;
                        pCity.Value = _City;
                        pPinCode.Value = _PinCode;
                        pShipmentPort.Value = _ShipmentPort;
                        pPortCode.Value = _PortCode;
                        pShippingBillNo.Value = _ShippingBillNo;
                        pShippingBillDt.Value = _ShippingBillDt;
                        pGRPPSDFno.Value = _GRPPSDFno;
                        pExpDate.Value = _ExpDate;
                        pRealizationDueDt.Value = _RealizationDueDt;
                        pExtnGranted.Value = _ExtnGranted;
                        pExtnGrantingAuthority.Value = _ExtnGrantingAuthority;
                        pExtnGrantedUpTo.Value = _ExtnGrantedUpTo;
                        pExpCountry.Value = _ExpCountry;
                        pCommodity.Value = _Commodity;

                        pInvoiceCur.Value = _InvoiceCur;
                        pInvoiceFCAmt.Value = _InvoiceFCAmt;
                        pInvoiceINRAmt.Value = _InvoiceINRAmt;
                        pRealizedCur.Value = _RealizedCur;
                        pRealizedINRAmt.Value = _RealizedINRAmt;
                        pOSINRAmt.Value = _OSINRAmt;
                        pBuyerName.Value = _BuyerName;
                        pBuyerAddress1.Value = _BuyerAddress1;
                        pBuyerAddress2.Value = _BuyerAddress2;
                        pBuyerCountry.Value = _BuyerCountry;
                        pBuyerPinCode.Value = _BuyerPinCode;
                        pRealizedBill.Value = _RealizedBill;
                        //pRealizationDt.Value = _RealizationDt;
                        //pRemark.Value = _Remark;

                        string _query = "Exp_XOS_CSV_Invalid_Data";
                        TF_DATA objUpload = new TF_DATA();
                        //DataTable dt2 = objUpload.getData(_query, pADcode, pXOSperiod, pIECcode, pExport, pExpBillNo, pExpBillDate, pStatusHolder
                        //    , pExpoterName, pExpoterAddress1, pExpoterAddress2, pCity, pPinCode, pShipmentPort, pPortCode, pShippingBillNo, pShippingBillDt
                        //    , pGRPPSDFno, pExpDate, pRealizationDueDt, pExtnGranted, pExtnGrantingAuthority, pExtnGrantedUpTo
                        //    , pExpCountry, pCommodity, pInvoiceCur, pInvoiceFCAmt, pInvoiceINRAmt, pRealizedCur, pRealizedINRAmt, pOSINRAmt
                        //    , pBuyerName, pBuyerAddress1, pBuyerAddress2, pBuyerCountry, pBuyerPinCode, pRealizedBill, pRealizationDt, pRemark, pUser, pUploadingDate);
                        DataTable dt2 = objUpload.getData(_query, pADcode, pXOSperiod, pIECcode, pExport, pExpBillNo, pExpBillDate, pStatusHolder
                            , pExpoterName, pExpoterAddress1, pExpoterAddress2, pCity, pPinCode, pShipmentPort, pPortCode, pShippingBillNo, pShippingBillDt
                            , pGRPPSDFno, pExpDate, pRealizationDueDt, pExtnGranted, pExtnGrantingAuthority, pExtnGrantedUpTo
                            , pExpCountry, pCommodity, pInvoiceCur, pInvoiceFCAmt, pInvoiceINRAmt, pRealizedCur, pRealizedINRAmt, pOSINRAmt
                            , pBuyerName, pBuyerAddress1, pBuyerAddress2, pBuyerCountry, pBuyerPinCode, pRealizedBill, pUser, pUploadingDate);

                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0][0].ToString() == "Success")
                            {
                                uploadresult = "uploaded";
                            }
                        }
                        
                    }

                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    labelMessage.Text = errorMsg;
                }

//                uploadresult = "uploaded";

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
            Response.Redirect("~/Reports/XOSReports/ViewXOScsvoutputdatavalidation.aspx", true);
        else
            labelMessage.Text = "No Error Records";
    }
    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    GC.Collect();
    //    ddlBranch.SelectedIndex = 0;
    //}

    //protected void btnNo_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Exp_Main.aspx", true);
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/XOS/XOS_Main.aspx", true);
    }
}