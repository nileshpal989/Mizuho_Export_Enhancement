using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

public partial class IDPMS_TF_IDPMS_Payment_Settlement_RBI_Ack : System.Web.UI.Page
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

            if (fileName == "bes")
            {
                SqlParameter P22 = new SqlParameter("@FileName", _FileNo);
                string QueryCheckFile = "TF_Check_FileName_PaymentSettlement_Ack";
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
                    string noOfbillOfEntry =
                        Bank.SelectSingleNode("noOfbillOfEntry").InnerText;
                    string noOfInvoices = Bank.SelectSingleNode("noOfInvoices").InnerText;

                    XmlNode billOfEntrys =
                        doc.SelectSingleNode("/bank/billOfEntrys");

                    XmlNodeList billOfEntryNumber =
                       billOfEntrys.SelectNodes("billOfEntry");
                    string portOfDischarge = "", billOfEntryNumber1 = "", billOfEntryDate = "", IECode = "", ADCode = "", paymentReferenceNumber = "",
    outwardReferenceNumber = "", remittanceCurrency = "", billClosureIndicator = "", errorCode = "", invoiceSerialNo = "", invoiceNo = "", invoiceAmt = "", InvoiceErrorCode = "", Currency = "", UploadedBy = "", UploadedDate = "",
inrnumber = "", realizationDate = "";
                    ;



                    foreach (XmlNode node in billOfEntryNumber)
                    {
                        portOfDischarge = node["portOfDischarge"].InnerText;
                        billOfEntryNumber1 = node["billOfEntryNumber"].InnerText;
                        billOfEntryDate = node["billOfEntryDate"].InnerText;
                        IECode = node["IECode"].InnerText;
                        ADCode = node["ADCode"].InnerText;
                        paymentReferenceNumber = node["paymentReferenceNumber"].InnerText;
                        outwardReferenceNumber = node["outwardReferenceNumber"].InnerText;
                        remittanceCurrency = node["remittanceCurrency"].InnerText;
                        billClosureIndicator = node["billClosureIndicator"].InnerText;
                        errorCode = node["errorCode"].InnerText;

                        //XmlNode Invoices =
                        //shippingBills.("/bank/invoices/invoice");

                        //XmlNode parent = node.ParentNode;


                        //XmlNodeList invoice =
                        //doc.SelectNodes("/bank/invoices/invoice/invoiceNo");
                        XmlNodeList invoice = ((XmlElement)node).GetElementsByTagName("invoice");
                        foreach (XmlNode childNode in invoice)
                        {

                            norecinxml = norecinxml + 1;

                            invoiceSerialNo = childNode["invoiceSerialNo"].InnerText;
                            invoiceNo = childNode["invoiceNo"].InnerText;
                            invoiceAmt = childNode["invoiceAmt"].InnerText;
                            InvoiceErrorCode = childNode["errorCode"].InnerText;
                            UploadedBy = Session["UserName"].ToString();
                            UploadedDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            SqlParameter p1 = new SqlParameter("@FileName", SqlDbType.VarChar);
                            p1.Value = _FileNo;
                            SqlParameter p2 = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
                            p2.Value = portOfDischarge;
                            SqlParameter p3 = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
                            p3.Value = billOfEntryNumber1;
                            SqlParameter p4 = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
                            p4.Value = billOfEntryDate;
                            SqlParameter p5 = new SqlParameter("@IECode", SqlDbType.VarChar);
                            p5.Value = IECode;
                            SqlParameter p6 = new SqlParameter("@ADCode", SqlDbType.VarChar);
                            p6.Value = ADCode;
                            SqlParameter p7 = new SqlParameter("@paymentReferenceNumber", SqlDbType.VarChar);
                            p7.Value = paymentReferenceNumber;
                            SqlParameter p8 = new SqlParameter("@outwardReferenceNumber", SqlDbType.VarChar);
                            p8.Value = outwardReferenceNumber;
                            SqlParameter p9 = new SqlParameter("@remittanceCurrency", SqlDbType.VarChar);
                            p9.Value = remittanceCurrency;
                            SqlParameter p10 = new SqlParameter("@billClosureIndicator", SqlDbType.VarChar);
                            p10.Value = billClosureIndicator;
                            SqlParameter p11 = new SqlParameter("@errorCode", SqlDbType.VarChar);
                            p11.Value = errorCode;
                            SqlParameter p12 = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                            p12.Value = invoiceSerialNo;
                            SqlParameter p13 = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                            p13.Value = invoiceNo;

                            SqlParameter p16 = new SqlParameter("@invoiceAmt", SqlDbType.VarChar);
                            p16.Value = invoiceAmt;


                            SqlParameter p14 = new SqlParameter("@Inv_Error_Code", SqlDbType.VarChar);
                            p14.Value = InvoiceErrorCode;

                            string _query = "TF_IDPMS_Payment_Settlment_RBI_Ack";

                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p16);
                            if (result == "1")
                            {
                                cntrec = cntrec + 1;
                            }
                            labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinxml + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                        }
                        GC.Collect();
                    }

                }

            }
            else if (Vales1 == "failbeaAck" || Vales1 == "passbeaAck")
            {
                SqlParameter P22 = new SqlParameter("@FileName", _FileNo);
                string QueryCheckFile = "TF_Check_FileName_BOE_Closure_Ack";
                DataTable dt = objSave.getData(QueryCheckFile, P22);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('File  already Uploaded.');", true);
                }
                else
                {
                    ////////////////////////////////////////////////////////////

                    XmlDocument doc = new XmlDocument();
                    doc.Load(sr);

                    XmlNode Bank =
                        doc.SelectSingleNode("/bank/checkSum");
                    string noOfbillOfEntry =
                        Bank.SelectSingleNode("noOfBOE").InnerText;
                    string noOfInvoices = Bank.SelectSingleNode("noOfInvoices").InnerText;

                    XmlNode billOfEntrys =
                        doc.SelectSingleNode("/bank/billOfEntries");

                    XmlNodeList billOfEntryNumber =
                       billOfEntrys.SelectNodes("billOfEntry");
                    string portOfDischarge = "", billOfEntryNumber1 = "", billOfEntryDate = "", IECode = "", ADCode = "", recordIndicator = "",
    adjustmentReferenceNumber = "", closeofBillIndicator = "", adjustmentDate = "", adjustmentIndicator = "",
    documentNumber = "", documentDate = "", documentPort = "", approvedBy = "", letterNumber = "", letterDate = "", Remark = "", errorCode = "",
    invoiceSerialNo = "", invoiceNo = "", adjustedInvoiceValueIC = "", InvoiceErrorCode = "", UploadedBy = "", UploadedDate = "";

                    foreach (XmlNode node in billOfEntryNumber)
                    {
                        portOfDischarge = node["portOfDischarge"].InnerText;
                        billOfEntryNumber1 = node["billOfEntryNumber"].InnerText;
                        billOfEntryDate = node["billOfEntryDate"].InnerText;
                        IECode = node["IECode"].InnerText;
                        ADCode = node["ADCode"].InnerText;
                        recordIndicator = node["recordIndicator"].InnerText;
                        adjustmentReferenceNumber = node["adjustmentReferenceNumber"].InnerText;
                        closeofBillIndicator = node["closeofBillIndicator"].InnerText;
                        adjustmentDate = node["adjustmentDate"].InnerText;
                        adjustmentIndicator = node["adjustmentIndicator"].InnerText;
                        documentNumber = node["documentNumber"].InnerText;
                        documentDate = node["documentDate"].InnerText;
                        documentPort = node["documentPort"].InnerText;
                        approvedBy = node["approvedBy"].InnerText;
                        letterNumber = node["letterNumber"].InnerText;
                        letterDate = node["letterDate"].InnerText;
                        Remark = node["Remark"].InnerText;
                        errorCode = node["errorCode"].InnerText;
                        XmlNodeList invoice = ((XmlElement)node).GetElementsByTagName("invoice");
                        foreach (XmlNode childNode in invoice)
                        {
                            norecinxml = norecinxml + 1;

                            invoiceSerialNo = childNode["invoiceSerialNo"].InnerText;
                            invoiceNo = childNode["invoiceNo"].InnerText;
                            adjustedInvoiceValueIC = childNode["adjustedInvoiceValueIC"].InnerText;
                            InvoiceErrorCode = childNode["errorCode"].InnerText;
                            UploadedBy = Session["UserName"].ToString();
                            //UploadedDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            SqlParameter p1 = new SqlParameter("@FileName", SqlDbType.VarChar);
                            p1.Value = _FileNo;
                            SqlParameter p2 = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
                            p2.Value = portOfDischarge;
                            SqlParameter p3 = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
                            p3.Value = billOfEntryNumber1;
                            SqlParameter p4 = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
                            p4.Value = billOfEntryDate;
                            SqlParameter p5 = new SqlParameter("@IECode", SqlDbType.VarChar);
                            p5.Value = IECode;
                            SqlParameter p6 = new SqlParameter("@ADCode", SqlDbType.VarChar);
                            p6.Value = ADCode;
                            SqlParameter p7 = new SqlParameter("@recordIndicator", SqlDbType.VarChar);
                            p7.Value = recordIndicator;
                            SqlParameter p8 = new SqlParameter("@adjustmentReferenceNumber", SqlDbType.VarChar);
                            p8.Value = adjustmentReferenceNumber;
                            SqlParameter p9 = new SqlParameter("@closeofBillIndicator", SqlDbType.VarChar);
                            p9.Value = closeofBillIndicator;
                            SqlParameter p10 = new SqlParameter("@adjustmentDate", SqlDbType.VarChar);
                            p10.Value = adjustmentDate;
                            SqlParameter p11 = new SqlParameter("@adjustmentIndicator", SqlDbType.VarChar);
                            p11.Value = adjustmentIndicator;

                            SqlParameter p12 = new SqlParameter("@documentNumber", SqlDbType.VarChar);
                            p12.Value = documentNumber;
                            SqlParameter p13 = new SqlParameter("@documentDate", SqlDbType.VarChar);
                            p13.Value = documentDate;
                            SqlParameter p14 = new SqlParameter("@documentPort", SqlDbType.VarChar);
                            p14.Value = documentPort;
                            SqlParameter p15 = new SqlParameter("@approvedBy", SqlDbType.VarChar);
                            p15.Value = approvedBy;
                            SqlParameter p16 = new SqlParameter("@letterNumber", SqlDbType.VarChar);
                            p16.Value = letterNumber;
                            SqlParameter p17 = new SqlParameter("@letterDate", SqlDbType.VarChar);
                            p17.Value = letterDate;
                            SqlParameter p18 = new SqlParameter("@Remark", SqlDbType.VarChar);
                            p18.Value = Remark;
                            SqlParameter p19 = new SqlParameter("@errorCode", SqlDbType.VarChar);
                            p19.Value = errorCode;

                            SqlParameter p20 = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                            p20.Value = invoiceSerialNo;
                            SqlParameter p21 = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                            p21.Value = invoiceNo;
                            SqlParameter p22 = new SqlParameter("@adjustedInvoiceValueIC", SqlDbType.VarChar);
                            p22.Value = adjustedInvoiceValueIC;
                            SqlParameter p23 = new SqlParameter("@Inv_Error_Code", SqlDbType.VarChar);
                            p23.Value = InvoiceErrorCode;
                            SqlParameter p24 = new SqlParameter("@addedby", SqlDbType.VarChar);
                            p24.Value = UploadedBy;

                            SqlParameter p25 = new SqlParameter("@addeddate", SqlDbType.VarChar);
                            p25.Value = System.DateTime.Now.ToString("dd/MM/yyyy");

                            string _query = "TF_IDPMS_BOE_Closure_RBI_Ack_Add";

                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p16, p15, p17, p18, p19, p20, p21, p22, p23, p24, p25);
                            if (result == "1")
                            {
                                cntrec = cntrec + 1;
                            }

                            labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinxml + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                        }
                        GC.Collect();
                    }

                    ///////////////////////////////////////////////////////////
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