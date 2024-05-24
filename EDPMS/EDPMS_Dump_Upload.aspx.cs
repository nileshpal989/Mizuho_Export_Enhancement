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

public partial class EDPMS_EDPMS_Dump_Upload : System.Web.UI.Page
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
        string ExportAgency = "", 
                ExportType = "",
                RecordIndicator = "",
                PortCode = "",
                ShippingBillNo = "",
                ShippingBillDate = "",
                LEODate = "",
                CustNo = "",
                FormNo = "",
                IECode = "",
                ADCode = "",
                CountryDes = "",
                InvoiceSerialNo = "",
                InvoiceNo = "",
                Invoicedate = "",
                FOBCurrency = "",
                FOBAmt = "",
                FreightCurrency = "",
                FreightAmt = "",
                InsuranceCurrency = "",
                InsuranceAmt = "",
                CommissionCurrency = "",
                CommissionAmt = "",
                DiscountCurrency = "",
                DiscountAmt = "",
                DeductionCurrency = "",
                DeductionAmt = "",
                PackagingCurrency = "",
                PackagingAmt = "",
                FileName = "",
                UpdatdBy="",
                UpdatdDate="";

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
            string QueryCheckFile = "TF_Check_EDPMS_DUMP";
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
                fileName = Vales1.Substring(0, 3);

                //string fileName = _FileNo.Substring(18, 3);
                if (fileName == "mdf")
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
                        ExportAgency = node["exportAgency"].InnerText;
                        ExportType = node["exportType"].InnerText;
                        RecordIndicator = node["recordIndicator"].InnerText;
                        PortCode = node["portCode"].InnerText;
                        ShippingBillNo = node["shippingBillNo"].InnerText;
                        ShippingBillDate = node["shippingBillDate"].InnerText;
                        LEODate = node["LEODate"].InnerText;
                        CustNo = node["custNo"].InnerText;
                        FormNo = node["formNo"].InnerText;
                        IECode = node["IECode"].InnerText;
                        ADCode = node["adCode"].InnerText;
                        CountryDes = node["countryOfDestination"].InnerText;
                    
                        //XmlNode Invoices =
                        //shippingBills.("/bank/invoices/invoice");

                        //XmlNode parent = node.ParentNode;
                        XmlNodeList invoice = ((XmlElement)node).GetElementsByTagName("invoice");


                        //XmlNodeList invoice =
                        //doc.SelectNodes("/bank/invoices/invoice/invoiceNo");

                        foreach (XmlNode childNode in invoice)
                        {

                            norecinxml = norecinxml + 1;

                            InvoiceSerialNo = childNode["invoiceSerialNo"].InnerText;
                            InvoiceNo = childNode["invoiceNo"].InnerText;
                            Invoicedate = childNode["invoiceDate"].InnerText;
                            FOBCurrency = childNode["FOBCurrencyCode"].InnerText;
                            FOBAmt = childNode["FOBAmt"].InnerText;
                            FreightCurrency = childNode["freightCurrencyCode"].InnerText;
                            FreightAmt = childNode["freightAmt"].InnerText;
                            InsuranceCurrency = childNode["insuranceCurrencyCode"].InnerText;
                            InsuranceAmt = childNode["insuranceAmt"].InnerText;
                            CommissionCurrency = childNode["commissionCurrencyCode"].InnerText;
                            CommissionAmt = childNode["commissionAmt"].InnerText;
                            DiscountCurrency = childNode["discountCurrencyCode"].InnerText;
                            DiscountAmt = childNode["discountAmt"].InnerText;
                            DeductionCurrency = childNode["deductionsCurrencyCode"].InnerText;
                            DeductionAmt = childNode["deductionsAmt"].InnerText;
                            PackagingCurrency = childNode["packagingCurrencyCode"].InnerText;
                            PackagingAmt = childNode["packagingChargesAmt"].InnerText;


                            UpdatdBy = Session["UserName"].ToString();
                            UpdatdDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            SqlParameter p1 = new SqlParameter("@ExportAgency", SqlDbType.VarChar);
                            p1.Value = ExportAgency;
                            SqlParameter p2 = new SqlParameter("@ExportType", SqlDbType.VarChar);
                            p2.Value = ExportType;
                            SqlParameter p3 = new SqlParameter("@RecordIndicator", SqlDbType.VarChar);
                            p3.Value = RecordIndicator;
                            SqlParameter p4 = new SqlParameter("@Port_Code", SqlDbType.VarChar);
                            p4.Value = PortCode;
                            SqlParameter p5 = new SqlParameter("@ShippingBill_no", SqlDbType.VarChar);
                            p5.Value = ShippingBillNo;
                            SqlParameter p6 = new SqlParameter("@ShippingBill_Date", SqlDbType.VarChar);
                            p6.Value = ShippingBillDate;
                            SqlParameter p7 = new SqlParameter("@LEODate", SqlDbType.VarChar);
                            p7.Value = LEODate;
                            SqlParameter p8 = new SqlParameter("@CustNo", SqlDbType.VarChar);
                            p8.Value = CustNo;
                            SqlParameter p9 = new SqlParameter("@Form_No", SqlDbType.VarChar);
                            p9.Value = FormNo;
                            SqlParameter p10 = new SqlParameter("@IECode", SqlDbType.VarChar);
                            p10.Value = IECode;
                            SqlParameter p11 = new SqlParameter("@Ad_Code", SqlDbType.VarChar);
                            p11.Value = ADCode;
                            SqlParameter p12 = new SqlParameter("@CountryDes", SqlDbType.VarChar);
                            p12.Value = CountryDes;
                            SqlParameter p13 = new SqlParameter("@InvoiceSerialNo", SqlDbType.VarChar);
                            p13.Value = InvoiceSerialNo;
                            SqlParameter p14 = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                            p14.Value = InvoiceNo;
                            SqlParameter p15 = new SqlParameter("@Invoicedate", SqlDbType.VarChar);
                            p15.Value = Invoicedate;
                          
                            SqlParameter p16 = new SqlParameter("@FOBCurrency", SqlDbType.VarChar);
                            p16.Value = FOBCurrency;
                            SqlParameter p17 = new SqlParameter("@FOBAmt", SqlDbType.VarChar);
                            p17.Value = FOBAmt;
                            SqlParameter p18 = new SqlParameter("@FreightCurrency", SqlDbType.VarChar);
                            p18.Value = FreightCurrency;
                            SqlParameter p19 = new SqlParameter("@FreightAmt", SqlDbType.VarChar);
                            p19.Value = FreightAmt;

                            SqlParameter p20 = new SqlParameter("@InsuranceCurrency", SqlDbType.VarChar);
                            p20.Value = InsuranceCurrency;

                            SqlParameter p21 = new SqlParameter("@InsuranceAmt", SqlDbType.VarChar);
                            p21.Value = InsuranceAmt;

                            SqlParameter p22 = new SqlParameter("@CommissionCurrency", SqlDbType.VarChar);
                            p22.Value = CommissionCurrency;
                            SqlParameter p23 = new SqlParameter("@CommissionAmt", SqlDbType.VarChar);
                            p23.Value = CommissionAmt;
                            SqlParameter p24 = new SqlParameter("@DiscountCurrency", SqlDbType.VarChar);
                            p24.Value = DiscountCurrency;
                            SqlParameter p25 = new SqlParameter("@DiscountAmt", SqlDbType.VarChar);
                            p25.Value = DiscountAmt;
                            SqlParameter p26 = new SqlParameter("@DeductionCurrency", SqlDbType.VarChar);
                            p26.Value = DeductionCurrency;

                            SqlParameter p27 = new SqlParameter("@DeductionAmt", SqlDbType.VarChar);
                            p27.Value = DeductionAmt;
                            SqlParameter p28 = new SqlParameter("@PackagingCurrency", SqlDbType.VarChar);
                            p28.Value = PackagingCurrency;
                            SqlParameter p29 = new SqlParameter("@PackagingAmt", SqlDbType.VarChar);
                            p29.Value = PackagingAmt;

                            SqlParameter p30 = new SqlParameter("@FileName", SqlDbType.VarChar);
                            p30.Value = _FileNo;
                            

                            SqlParameter p31 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                            p31.Value = UpdatdBy;
                            SqlParameter p32 = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                            p32.Value = UpdatdDate;

                            string _query = "TF_EDPMS_DUMP_FileUpload";

                            result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16,p17,p18,p19,p20,
                                p21,p22,p23,p24,p25,p26,p27,p28,p29,p30,p31,p32);

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