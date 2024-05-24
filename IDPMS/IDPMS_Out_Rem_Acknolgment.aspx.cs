using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Threading;

public partial class IDPMS_IDPMS_Out_Rem_Acknolgment : System.Web.UI.Page
{
    int cntrec, norecinxml;
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
                ddlRefNo.SelectedIndex = 1;
                ddlRefNo.SelectedValue = Session["userADCode"].ToString();
                ddlRefNo.Enabled = false;
            }
        }
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails1";
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
        string uploadresult = "", _FileNo = "";
        string fileName = "";
        string outwardReferenceNumber = "", ADCode = "", Amount = "", currencyCode = "", paymentDate = "", IECode = "", IEName = "",
            IEAddress = "", IEPANNumber = "", isCapitalGoods = "", beneficiaryName = "", beneficiaryAccountNumber = "", beneficiaryCountry = "",
            SWIFT = "", purposeCode = "", recordIndicator = "", paymentTerms = "", errorCode = "", UploadedDate = "", remcurr = "", AdjustedAmount = "", AdjustedDate = "",
            AdjustInd = "", AdjustedSeqNo = "", approvedBy = "", LetterNo = "", LetterDate = "", DocumentNumber = "", DocumentDate = "", PortOfDischarge = "", Remarks = "";


        string path = Server.MapPath("~/GeneratedFiles/Uploaded_Files");
        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileuploaded";
        TF_DATA objSave = new TF_DATA();
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
            var sr = _filepath;
            _FileNo = sr.Split('\\').Last();


            string[] values;
            values = _FileNo.Split('.');
            string Vales1 = values[1].ToString();
            fileName = Vales1.Substring(4, 3);

            if (fileName == "orm")
            {
                SqlParameter P22 = new SqlParameter("@FileName", _FileNo);
                string QueryCheckFile = "TF_CheckOutwardFileName";
                DataTable dt = objSave.getData(QueryCheckFile, P22);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('File  already Upload.');", true);

                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(sr);

                    XmlNode Bank =
                        doc.SelectSingleNode("/bank/checkSum");
                    string noOfShippingBills =
                        Bank.SelectSingleNode("noOfORM").InnerText;


                    XmlNode ShippingBills =
                        doc.SelectSingleNode("/bank/outwardReferences");


                    XmlNodeList shippingBill =
                       ShippingBills.SelectNodes("outwardReference");

                    string[] values_p;
                    string _errorcode;
                    foreach (XmlNode node in shippingBill)
                    {

                        outwardReferenceNumber = node["outwardReferenceNumber"].InnerText;
                        norecinxml = norecinxml + 1;
                        ADCode = node["ADCode"].InnerText;
                        Amount = node["Amount"].InnerText;
                        currencyCode = node["currencyCode"].InnerText;
                        paymentDate = node["paymentDate"].InnerText;
                        IECode = node["IECode"].InnerText;
                        IEName = node["IEName"].InnerText;
                        IEAddress = node["IEAddress"].InnerText;
                        IEPANNumber = node["IEPANNumber"].InnerText;
                        isCapitalGoods = node["isCapitalGoods"].InnerText;
                        beneficiaryName = node["beneficiaryName"].InnerText;
                        beneficiaryAccountNumber = node["beneficiaryAccountNumber"].InnerText;
                        beneficiaryCountry = node["beneficiaryCountry"].InnerText;
                        SWIFT = node["SWIFT"].InnerText;
                        purposeCode = node["purposeCode"].InnerText;
                        recordIndicator = node["recordIndicator"].InnerText;

                        paymentTerms = node["paymentTerms"].InnerText;

                        errorCode = node["errorCode"].InnerText;
                        char[] splitchar = { ',' };
                        values_p = errorCode.Split(splitchar);
                        _errorcode = values_p[0].ToString();




                        SqlParameter p1 = new SqlParameter("@FileName", SqlDbType.VarChar);
                        p1.Value = _FileNo;
                        SqlParameter p2 = new SqlParameter("@outwardReferenceNumber", SqlDbType.VarChar);
                        p2.Value = outwardReferenceNumber;
                        SqlParameter p3 = new SqlParameter("@ADCode", SqlDbType.VarChar);
                        p3.Value = ADCode;
                        SqlParameter p4 = new SqlParameter("@Amount", SqlDbType.VarChar);
                        p4.Value = Amount;
                        SqlParameter p5 = new SqlParameter("@currencyCode", SqlDbType.VarChar);
                        p5.Value = currencyCode;
                        SqlParameter p6 = new SqlParameter("@paymentDate", SqlDbType.VarChar);
                        p6.Value = paymentDate;
                        SqlParameter p7 = new SqlParameter("@IECode", SqlDbType.VarChar);
                        p7.Value = IECode;
                        SqlParameter p8 = new SqlParameter("@IEName", SqlDbType.VarChar);
                        p8.Value = IEName;
                        SqlParameter p9 = new SqlParameter("@IEAddress", SqlDbType.VarChar);
                        p9.Value = IEAddress;
                        SqlParameter p10 = new SqlParameter("@IEPANNumbe", SqlDbType.VarChar);
                        p10.Value = IEPANNumber;
                        SqlParameter p11 = new SqlParameter("@isCapitalGoods", SqlDbType.VarChar);
                        p11.Value = isCapitalGoods;
                        SqlParameter p12 = new SqlParameter("@beneficiaryName", SqlDbType.VarChar);
                        p12.Value = beneficiaryName;
                        SqlParameter p13 = new SqlParameter("@beneficiaryAccountNumber", SqlDbType.VarChar);
                        p13.Value = beneficiaryAccountNumber;
                        SqlParameter p14 = new SqlParameter("@beneficiaryCountry", SqlDbType.VarChar);
                        p14.Value = beneficiaryCountry;
                        SqlParameter p15 = new SqlParameter("@SWIFT", SqlDbType.VarChar);
                        p15.Value = SWIFT;
                        SqlParameter p16 = new SqlParameter("@purposeCode", SqlDbType.VarChar);
                        p16.Value = purposeCode;
                        SqlParameter p17 = new SqlParameter("@recordIndicator", SqlDbType.VarChar);
                        p17.Value = recordIndicator;
                        SqlParameter p18 = new SqlParameter("@paymentTerms", SqlDbType.VarChar);
                        p18.Value = paymentTerms;
                        SqlParameter p19 = new SqlParameter("@Error_Code", SqlDbType.VarChar);
                        p19.Value = _errorcode;
                        SqlParameter p20 = new SqlParameter("@AddDate", SqlDbType.VarChar);
                        p20.Value = System.DateTime.Now.ToString();

                        string _query = "TF_IDPMS_FileUpload_AcknowledgmentOutward";

                        result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);
                        if (result == "Uploaded")
                        {
                            cntrec = cntrec + 1;
                        }
                        labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinxml + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                    }
                }
            }
            else if (fileName == "ora")
            {
                SqlParameter P22 = new SqlParameter("@FileName", _FileNo);
                string QueryCheckFile = "TF_Check_ORM_Closure_FileName";
                DataTable dt = objSave.getData(QueryCheckFile, P22);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('File  already Upload.');", true);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(sr);

                    XmlNode Bank =
                        doc.SelectSingleNode("/bank/checkSum");
                    string noOfShippingBills =
                        Bank.SelectSingleNode("noOfORM").InnerText;


                    XmlNode ShippingBills =
                        doc.SelectSingleNode("/bank/outwardReferences");


                    XmlNodeList shippingBill =
                       ShippingBills.SelectNodes("outwardReference");

                    string[] values_p;
                    string _errorcode;
                    foreach (XmlNode node in shippingBill)
                    {

                        outwardReferenceNumber = node["outwardReferenceNumber"].InnerText;
                        norecinxml = norecinxml + 1;
                        ADCode = node["ADCode"].InnerText;
                        remcurr = node["remittanceCurrency"].InnerText;
                        AdjustedAmount = node["adjustedAmount"].InnerText;
                        AdjustedDate = node["adjustedDate"].InnerText;
                        IECode = node["IECode"].InnerText;
                        SWIFT = node["SWIFT"].InnerText;
                        AdjustInd = node["adjustmentIndicator"].InnerText;
                        AdjustedSeqNo = node["adjustmentSeqNumber"].InnerText;
                        approvedBy = node["approvedBy"].InnerText;
                        LetterNo = node["letterNumber"].InnerText;
                        LetterDate = node["letterDate"].InnerText;
                        DocumentNumber = node["documentNumber"].InnerText;
                        DocumentDate = node["documentDate"].InnerText;
                        PortOfDischarge = node["portofDischarge"].InnerText;
                        recordIndicator = node["recordIndicator"].InnerText;

                        Remarks = node["remark"].InnerText;

                        errorCode = node["errorCode"].InnerText;
                        char[] splitchar = { ',' };
                        values_p = errorCode.Split(splitchar);
                        _errorcode = values_p[0].ToString();




                        SqlParameter p1 = new SqlParameter("@FileName", SqlDbType.VarChar);
                        p1.Value = _FileNo;
                        SqlParameter p2 = new SqlParameter("@outwardReferenceNumber", SqlDbType.VarChar);
                        p2.Value = outwardReferenceNumber;
                        SqlParameter p3 = new SqlParameter("@ADCode", SqlDbType.VarChar);
                        p3.Value = ADCode;
                        SqlParameter p4 = new SqlParameter("@Cur", SqlDbType.VarChar);
                        p4.Value = remcurr;
                        SqlParameter p5 = new SqlParameter("@AdjAmt", SqlDbType.VarChar);
                        p5.Value = AdjustedAmount;
                        SqlParameter p6 = new SqlParameter("@AdjDate", SqlDbType.VarChar);
                        p6.Value = AdjustedDate;
                        SqlParameter p7 = new SqlParameter("@IECode", SqlDbType.VarChar);
                        p7.Value = IECode;
                        SqlParameter p8 = new SqlParameter("@SWIFT", SqlDbType.VarChar);
                        p8.Value = SWIFT;
                        SqlParameter p9 = new SqlParameter("@AdjInd", SqlDbType.VarChar);
                        p9.Value = AdjustInd;
                        SqlParameter p10 = new SqlParameter("@AdjSeqNo", SqlDbType.VarChar);
                        p10.Value = AdjustedSeqNo;
                        SqlParameter p11 = new SqlParameter("@ApprBy", SqlDbType.VarChar);
                        p11.Value = approvedBy;
                        SqlParameter p12 = new SqlParameter("@LetNo", SqlDbType.VarChar);
                        p12.Value = LetterNo;
                        SqlParameter p13 = new SqlParameter("@LetDt", SqlDbType.VarChar);
                        p13.Value = LetterDate;
                        SqlParameter p14 = new SqlParameter("@DocNo", SqlDbType.VarChar);
                        p14.Value = DocumentNumber;
                        SqlParameter p15 = new SqlParameter("@DocDt", SqlDbType.VarChar);
                        p15.Value = DocumentDate;
                        SqlParameter p16 = new SqlParameter("@PortOfDis", SqlDbType.VarChar);
                        p16.Value = PortOfDischarge;
                        SqlParameter p17 = new SqlParameter("@RecInd", SqlDbType.VarChar);
                        p17.Value = recordIndicator;
                        SqlParameter p18 = new SqlParameter("@Remark", SqlDbType.VarChar);
                        p18.Value = Remarks;
                        SqlParameter p19 = new SqlParameter("@ErrorCode", SqlDbType.VarChar);
                        p19.Value = _errorcode;
                        SqlParameter p20 = new SqlParameter("@AddDate", SqlDbType.VarChar);
                        p20.Value = System.DateTime.Now.ToString("dd/MM/yyyy");

                        string _query = "TF_IDPMS_FileUpld_ORMClsrAck";

                        result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);
                        if (result == "Uploaded")
                        {
                            cntrec = cntrec + 1;
                        }
                        labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinxml + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Select  Vaild File');", true);

            }
            return uploadresult;

        }
        else
            return uploadresult;
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "";

        string path = Server.MapPath("~/GeneratedFiles/Uploaded_Files");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        result = upload_file(path + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName));
    }
}