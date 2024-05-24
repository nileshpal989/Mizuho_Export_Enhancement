using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_XML_Fileupload_Paymant_Ack : System.Web.UI.Page
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
        string uploadresult = "", _FileNo = "";
        string fileName="";
        string Port_Code="",Export_Type="",Record_Ind="",ShippingBill_No="",ShippingBill_Date="",Form_No="",
            AdCode = "", Ebrc_No = "", Error_Code = "", Invoice_No = "", Invoice_Date = "",InvoiceErrorCode="", Currency = "", UploadedBy = "", UploadedDate = "";

        string path = Server.MapPath("~/TF_GeneratedFiles/UploadedFiles");
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
            SqlParameter P22 = new SqlParameter("@FileName", _FileNo);
            string QueryCheckFile = "TF_Check_FileName_Realized_Ack";
            DataTable dt = objSave.getData(QueryCheckFile, P22);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('File  already Upload.');", true);

            }
            else
            {
                string[] values;
                values = _FileNo.Split('.');
                string Vales1 = values[1].ToString();
                //int Length1 = _FileNo.Length;
                //string S = Convert.ToString(Length1);
                //if (S == "41")
                //{
                fileName = Vales1.Substring(4, 3);

                //string fileName = _FileNo.Substring(18, 3);
                if (fileName == "prn")
                {

                    XmlDocument doc = new XmlDocument();
                    doc.Load(sr);

                    XmlNode Bank =
                        doc.SelectSingleNode("/bank/checkSum");
                    string noOfShippingBills =
                        Bank.SelectSingleNode("noOfShippingBills").InnerText;
                    string noOfInvoices = Bank.SelectSingleNode("noOfInvoices").InnerText;

                    XmlNode shippingBills =
                        doc.SelectSingleNode("/bank/shippingBills");

                    XmlNodeList shippingBill =
                       shippingBills.SelectNodes("shippingBill");

                    foreach (XmlNode node in shippingBill)
                    {
                        Port_Code = node["portCode"].InnerText;
                        Export_Type = node["exportType"].InnerText;
                        Record_Ind = node["recordIndicator"].InnerText;
                        ShippingBill_No = node["shippingBillNo"].InnerText;
                        ShippingBill_Date = node["shippingBillDate"].InnerText;
                        Form_No = node["formNo"].InnerText;
                        AdCode = node["adCode"].InnerText;
                        Ebrc_No = node["paymentSequence"].InnerText;
                        Error_Code = node["errorCodes"].InnerText;
                        Currency = node["realizedCurrencyCode"].InnerText;
                        //XmlNode Invoices =
                        //shippingBills.("/bank/invoices/invoice");

                        //XmlNode parent = node.ParentNode;
                        XmlNodeList invoice = ((XmlElement)node).GetElementsByTagName("invoice");


                        //XmlNodeList invoice =
                        //doc.SelectNodes("/bank/invoices/invoice/invoiceNo");

                        foreach (XmlNode childNode in invoice)
                        {

                            norecinxml = norecinxml + 1;

                            Invoice_No = childNode["invoiceNo"].InnerText;
                            Invoice_Date = childNode["invoiceDate"].InnerText;
                          
                            InvoiceErrorCode = childNode["invoiceErrorCodes"].InnerText;
                            UploadedBy = Session["UserName"].ToString();
                            UploadedDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            string shippingbillnos = "";
                            if (ShippingBill_No=="")
                            {
                                shippingbillnos = Form_No;
                            }
                            else
                            {
                                shippingbillnos = ShippingBill_No;
                            }



                            SqlParameter p1 = new SqlParameter("@FileName", SqlDbType.VarChar);
                            p1.Value = _FileNo;
                            SqlParameter p2 = new SqlParameter("@Port_Code", SqlDbType.VarChar);
                            p2.Value = Port_Code;
                            SqlParameter p3 = new SqlParameter("@Export_Type", SqlDbType.VarChar);
                            p3.Value = Export_Type;
                            SqlParameter p4 = new SqlParameter("@Record_Indicator", SqlDbType.VarChar);
                            p4.Value = Record_Ind;
                            SqlParameter p5 = new SqlParameter("@ShippingBill_no", SqlDbType.VarChar);
                            p5.Value = shippingbillnos;
                            SqlParameter p6 = new SqlParameter("@ShippingBill_Date", SqlDbType.VarChar);
                            p6.Value = ShippingBill_Date;
                            SqlParameter p7 = new SqlParameter("@Form_No", SqlDbType.VarChar);
                            p7.Value = Form_No;
                            SqlParameter p8 = new SqlParameter("@Ad_Code", SqlDbType.VarChar);
                            p8.Value = AdCode;
                            SqlParameter p9 = new SqlParameter("@Ebrc_No", SqlDbType.VarChar);
                            p9.Value = Ebrc_No;
                            SqlParameter p10 = new SqlParameter("@Error_Code", SqlDbType.VarChar);
                            p10.Value = Error_Code;
                            SqlParameter p11 = new SqlParameter("@Invoice_no", SqlDbType.VarChar);
                            p11.Value = Invoice_No;
                            SqlParameter p12 = new SqlParameter("@Invoice_Date", SqlDbType.VarChar);
                            p12.Value = Invoice_Date;
                            SqlParameter p13 = new SqlParameter("@Currency", SqlDbType.VarChar);
                            p13.Value = Currency;

                            SqlParameter p16 = new SqlParameter("@InvoiceErrorCode", SqlDbType.VarChar);
                            p16.Value = InvoiceErrorCode;


                            SqlParameter p14 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                            p14.Value = UploadedBy;
                            SqlParameter p15 = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                            p15.Value = UploadedDate;
                            string _query = "TF_EDPMS_FileUpload_AcknowledgmentRealizationOfDocument";

                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
                            if (result == "Uploaded")
                            {
                                cntrec = cntrec + 1;
                            }
                            labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinxml + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                        }
                        GC.Collect();
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Select  Vaild File');", true);

                }
            }
                   
            return uploadresult;
        }
        else
            return uploadresult;
        
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "";

        string path = Server.MapPath("~/TF_GeneratedFiles/UploadedFiles");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        result = upload_file(path + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName));
    }
    }
