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
using System.Xml;

public partial class IDPMS_TF_IDPMS_Dump_Upload : System.Web.UI.Page
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
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlRefNo.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";

        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";

            ddlRefNo.DataSource = dt.DefaultView;
            ddlRefNo.DataTextField = "BranchName";
            ddlRefNo.DataValueField = "AuthorizedDealerCode";
            ddlRefNo.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);

    }

    private string upload_file(string _filepath)
    {
        string uploadresult = "", _FileNo = "";
        string fileName = "";
        string portOfDischarge = "",
                importAgency = "",
                billOfEntryNumber = "",
                billOfEntryDate = "",
                ADCode = "",
                G_P = "",
                IECode = "",
                IEName = "",
                IEAddress = "",
                IEPANNumber = "",
                portOfShipment = "",
                IGMNumber = "",
                IGMDate = "",
                MAWB_MBLNumber = "",
                MAWB_MBLDate = "",
                HAWB_HBLNumber = "",
                HAWB_HBLDate = "",
                recordIndicator = "",
                invoiceSerialNo = "",
                invoiceNo = "",
                termsOfInvoice = "",
                supplierName = "",
                supplierAddress = "",
                supplierCountry = "",
                sellerName = "",
                sellerAddress = "",
                sellerCountry = "",
                invoiceAmount = "",
                invoiceCurrency = "",
                freightAmount = "",
                freightCurrencyCode = "",
                insuranceAmount = "",
                insuranceCurrencyCode = "",
                agencyCommission = "",
                agencyCurrency = "",
                discountCharges = "",
                discountCurrency = "",
                miscellaneousCharges = "",
                miscellaneousCurrency = "",
                thirdPartyName = "",
                thirdPartyAddress = "",
                thirdPartyCountry = "",
            //new Column 21/08/2018
                UtilizedAmt = "",
                FileName = "",
                UpdatdBy = "",
                UpdatdDate = "";

        string path = Server.MapPath("~/GeneratedFiles/UploadedFiles");
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
            string QueryCheckFile = "TF_Check_IDPMS_DUMP";
            DataTable dt = objSave.getData(QueryCheckFile, P22);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('File already Upload.');", true);

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
                if (Vales1.Substring(0, 3) == "obe")
                { fileName = Vales1.Substring(0, 3); }
                else
                { fileName = Vales1.Substring(0, 6); }


                //string fileName = _FileNo.Substring(18, 3);
                if (fileName == "obe" || fileName == "obback")
                {

                    XmlDocument doc = new XmlDocument();
                    doc.Load(sr);

                    XmlNode Bank =
                        doc.SelectSingleNode("/bank/checkSum");
                    string noOfbillEntry =
                        Bank.SelectSingleNode("noOfbillOfEntry").InnerText;
                    string noOfInvoices = Bank.SelectSingleNode("noOfInvoices").InnerText;

                    XmlNode billOfEntrys =
                        doc.SelectSingleNode("/bank/billOfEntrys");

                    XmlNodeList billOfEntry =
                       billOfEntrys.SelectNodes("billOfEntry");

                    foreach (XmlNode node in billOfEntry)
                    {
                        portOfDischarge = node["portOfDischarge"].InnerText;
                        importAgency = node["importAgency"].InnerText;
                        billOfEntryNumber = node["billOfEntryNumber"].InnerText;
                        billOfEntryDate = node["billOfEntryDate"].InnerText;
                        ADCode = node["ADCode"].InnerText;
                        G_P = node["G-P"].InnerText;
                        IECode = node["IECode"].InnerText;
                        IEName = node["IEName"].InnerText;
                        IEAddress = node["IEAddress"].InnerText;
                        IEPANNumber = node["IEPANNumber"].InnerText;
                        portOfShipment = node["portOfShipment"].InnerText;
                        IGMNumber = node["IGMNumber"].InnerText;


                        IGMDate = node["IGMDate"].InnerText;
                        MAWB_MBLNumber = node["MAWB-MBLNumber"].InnerText;
                        MAWB_MBLDate = node["MAWB-MBLDate"].InnerText;
                        HAWB_HBLNumber = node["HAWB-HBLNumber"].InnerText;
                        HAWB_HBLDate = node["HAWB-HBLDate"].InnerText;
                        recordIndicator = node["recordIndicator"].InnerText;


                        //XmlNode Invoices =
                        //shippingBills.("/bank/invoices/invoice");

                        //XmlNode parent = node.ParentNode;
                        XmlNodeList invoice = ((XmlElement)node).GetElementsByTagName("invoice");


                        //XmlNodeList invoice =
                        //doc.SelectNodes("/bank/invoices/invoice/invoiceNo");

                        foreach (XmlNode childNode in invoice)
                        {
                            norecinxml = norecinxml + 1;

                            invoiceSerialNo = childNode["invoiceSerialNo"].InnerText;
                            invoiceNo = childNode["invoiceNo"].InnerText;
                            termsOfInvoice = childNode["termsOfInvoice"].InnerText;
                            supplierName = childNode["supplierName"].InnerText;
                            supplierAddress = childNode["supplierAddress"].InnerText;
                            supplierCountry = childNode["supplierCountry"].InnerText;
                            sellerName = childNode["sellerName"].InnerText;
                            sellerAddress = childNode["sellerAddress"].InnerText;
                            sellerCountry = childNode["sellerCountry"].InnerText;
                            invoiceAmount = childNode["invoiceAmount"].InnerText;
                            invoiceCurrency = childNode["invoiceCurrency"].InnerText;
                            freightAmount = childNode["freightAmount"].InnerText;
                            freightCurrencyCode = childNode["freightCurrencyCode"].InnerText;
                            insuranceAmount = childNode["insuranceAmount"].InnerText;
                            insuranceCurrencyCode = childNode["insuranceCurrencyCode"].InnerText;
                            agencyCommission = childNode["agencyCommission"].InnerText;
                            agencyCurrency = childNode["agencyCurrency"].InnerText;

                            discountCharges = childNode["discountCharges"].InnerText;
                            discountCurrency = childNode["discountCurrency"].InnerText;
                            miscellaneousCharges = childNode["miscellaneousCharges"].InnerText;
                            miscellaneousCurrency = childNode["miscellaneousCurrency"].InnerText;
                            thirdPartyName = childNode["thirdPartyName"].InnerText;

                            thirdPartyAddress = childNode["thirdPartyAddress"].InnerText;
                            thirdPartyCountry = childNode["thirdPartyCountry"].InnerText;
                            UtilizedAmt = childNode["utilizedAmount"].InnerText;
                            FileName = _FileNo;

                            UpdatdBy = Session["UserName"].ToString();
                            UpdatdDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            SqlParameter p1 = new SqlParameter("@PortDischarge", SqlDbType.VarChar);
                            p1.Value = portOfDischarge;
                            SqlParameter p2 = new SqlParameter("@IMPORT_Agency", SqlDbType.VarChar);
                            p2.Value = importAgency;
                            SqlParameter p3 = new SqlParameter("@Bill_Entry_No", SqlDbType.VarChar);
                            p3.Value = billOfEntryNumber;
                            SqlParameter p4 = new SqlParameter("@Bill_Entry_Date", SqlDbType.VarChar);
                            p4.Value = billOfEntryDate;
                            SqlParameter p5 = new SqlParameter("@ADCode", SqlDbType.VarChar);
                            p5.Value = ADCode;
                            SqlParameter p6 = new SqlParameter("@G_P", SqlDbType.VarChar);
                            p6.Value = G_P;
                            SqlParameter p7 = new SqlParameter("@IECode", SqlDbType.VarChar);
                            p7.Value = IECode;
                            SqlParameter p8 = new SqlParameter("@IEName", SqlDbType.VarChar);
                            p8.Value = IEName;
                            SqlParameter p9 = new SqlParameter("@IEAddress", SqlDbType.VarChar);
                            p9.Value = IEAddress;
                            SqlParameter p10 = new SqlParameter("@IEPanNo", SqlDbType.VarChar);
                            p10.Value = IEPANNumber;
                            SqlParameter p11 = new SqlParameter("@Port_Shipment", SqlDbType.VarChar);
                            p11.Value = portOfShipment;
                            SqlParameter p12 = new SqlParameter("@Record_Inc", SqlDbType.VarChar);
                            p12.Value = recordIndicator;
                            SqlParameter p13 = new SqlParameter("@IGMN_No", SqlDbType.VarChar);
                            p13.Value = IGMNumber;
                            SqlParameter p14 = new SqlParameter("@IGM_Date", SqlDbType.VarChar);
                            p14.Value = IGMDate;
                            SqlParameter p15 = new SqlParameter("@MAWB_MBLNo", SqlDbType.VarChar);
                            p15.Value = MAWB_MBLNumber;

                            SqlParameter p16 = new SqlParameter("@MAWB_MBL_Date", SqlDbType.VarChar);
                            p16.Value = MAWB_MBLDate;
                            SqlParameter p17 = new SqlParameter("@HAWB_HBL_No", SqlDbType.VarChar);
                            p17.Value = HAWB_HBLNumber;
                            SqlParameter p18 = new SqlParameter("@HAWB_HBL_Date", SqlDbType.VarChar);
                            p18.Value = HAWB_HBLDate;
                            SqlParameter p19 = new SqlParameter("@Invoce_Serial_No", SqlDbType.VarChar);
                            p19.Value = invoiceSerialNo;

                            SqlParameter p20 = new SqlParameter("@Invoce_No", SqlDbType.VarChar);
                            p20.Value = invoiceNo;

                            SqlParameter p21 = new SqlParameter("@Terms_of_Invoice", SqlDbType.VarChar);
                            p21.Value = termsOfInvoice;

                            SqlParameter p22 = new SqlParameter("@Invoice_Amt", SqlDbType.VarChar);
                            p22.Value = invoiceAmount;
                            SqlParameter p23 = new SqlParameter("@Invoice_Currency", SqlDbType.VarChar);
                            p23.Value = invoiceCurrency;
                            SqlParameter p24 = new SqlParameter("@Freight_Amt", SqlDbType.VarChar);
                            p24.Value = freightAmount;
                            SqlParameter p25 = new SqlParameter("@Freight_Currency", SqlDbType.VarChar);
                            p25.Value = freightCurrencyCode;
                            SqlParameter p26 = new SqlParameter("@Insurance_Amt", SqlDbType.VarChar);
                            p26.Value = insuranceAmount;

                            SqlParameter p27 = new SqlParameter("@Insurance_Currency", SqlDbType.VarChar);
                            p27.Value = insuranceCurrencyCode;
                            SqlParameter p28 = new SqlParameter("@Agency_Commision", SqlDbType.VarChar);
                            p28.Value = agencyCommission;
                            SqlParameter p29 = new SqlParameter("@Agency_Currency", SqlDbType.VarChar);
                            p29.Value = agencyCurrency;

                            SqlParameter p30 = new SqlParameter("@Discount_Charges", SqlDbType.VarChar);
                            p30.Value = discountCharges;
                            SqlParameter p31 = new SqlParameter("@Discount_Currency", SqlDbType.VarChar);
                            p31.Value = discountCurrency;
                            SqlParameter p32 = new SqlParameter("@Miscellaneous_Charges", SqlDbType.VarChar);
                            p32.Value = miscellaneousCharges;
                            SqlParameter p33 = new SqlParameter("@Miscellaneous_Currency", SqlDbType.VarChar);
                            p33.Value = miscellaneousCurrency;
                            SqlParameter p34 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
                            p34.Value = supplierName;

                            SqlParameter p35 = new SqlParameter("@Supplier_Address", SqlDbType.VarChar);
                            p35.Value = supplierAddress;
                            SqlParameter p36 = new SqlParameter("@Supplier_Country", SqlDbType.VarChar);
                            p36.Value = supplierCountry;
                            SqlParameter p37 = new SqlParameter("@Seller_Name", SqlDbType.VarChar);
                            p37.Value = sellerName;

                            SqlParameter p38 = new SqlParameter("@Seller_Address", SqlDbType.VarChar);
                            p38.Value = sellerAddress;
                            SqlParameter p39 = new SqlParameter("@Seller_Country", SqlDbType.VarChar);
                            p39.Value = sellerCountry;
                            SqlParameter p40 = new SqlParameter("@Third_Party_Name", SqlDbType.VarChar);
                            p40.Value = thirdPartyName;

                            SqlParameter p41 = new SqlParameter("@Third_Party_Address", SqlDbType.VarChar);
                            p41.Value = thirdPartyAddress;
                            SqlParameter p42 = new SqlParameter("@Third_Party_Country", SqlDbType.VarChar);
                            p42.Value = thirdPartyCountry;

                            SqlParameter p43 = new SqlParameter("@FileName", SqlDbType.VarChar);
                            p43.Value = fileName;
                            SqlParameter p44 = new SqlParameter("@Addusername", SqlDbType.VarChar);
                            p44.Value = Session["userName"].ToString();
                            SqlParameter p45 = new SqlParameter("@Adddatetime", SqlDbType.VarChar);
                            p45.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                            SqlParameter p46 = new SqlParameter("@UtilizedAmt", SqlDbType.VarChar);
                            p46.Value = UtilizedAmt;

                            string _query = "TF_IDPMS_ADD_EDIT";

                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46);

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

        string path = Server.MapPath("~/GeneratedFiles/UploadedFiles");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        result = upload_file(path + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName));
    }

}