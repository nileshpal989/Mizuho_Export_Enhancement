using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;

public partial class EDPMS_EDPMS_XMLgeneration_Realized : System.Web.UI.Page
{
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
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtfromDate.Focus();

                //txtsrno.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtfileName.Text = "RBI" + System.DateTime.Now.ToString("ddMMyyyy");
                //txtsrno.Attributes.Add("onblur", "return countsrno();");
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
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";

        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        DataTable dtInvoice = new DataTable();
        string a = ddlBranch.Text;
        string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        string Date = System.DateTime.Now.ToString("ddMMyyyy");

        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();
        SqlParameter p2 = new SqlParameter("@FromDate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();
        SqlParameter p3 = new SqlParameter("@ToDate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        string _qurey = "TF_EDPMS_GenerateXML_Data_Realization";
        dt = obj.getData(_qurey, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            string query = "TF_EDPMS_GenerateFileName_Realized";
            DataTable dtfile = obj.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();

            string _filePath = _directoryPath + "/" + filename1 + ".prn.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

            sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfInvoices>" + dt.Rows[0]["InvoiceNoCount"].ToString() + "</noOfInvoices>");
            sw.WriteLine("<noOfShippingBills>" + dt.Rows[0]["ShippBillCount"].ToString() + "</noOfShippingBills>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<shippingBills>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("<shippingBill>");
                sw.WriteLine("<portCode>" + dt.Rows[i]["PortCode"].ToString() + "</portCode>");
                sw.WriteLine("<exportType>" + dt.Rows[i]["TypeofExport"].ToString() + "</exportType>");
                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["RecordIndicator"].ToString() + "</recordIndicator>");
                sw.WriteLine("<shippingBillNo>" + dt.Rows[i]["ShippingBillNo"].ToString() + "</shippingBillNo>");
                sw.WriteLine("<shippingBillDate>" + dt.Rows[i]["Shipping_Bill_Date"].ToString() + "</shippingBillDate>");
                sw.WriteLine("<formNo>" + dt.Rows[i]["FormNo"].ToString() + "</formNo>");
                sw.WriteLine("<LEODate>" + dt.Rows[i]["leodate"].ToString() + "</LEODate>");
                sw.WriteLine("<adCode>" + dt.Rows[i]["ADCode"].ToString() + "</adCode>");
                sw.WriteLine("<paymentSequence>" + dt.Rows[i]["EBRCNO"].ToString() + "</paymentSequence>");
                sw.WriteLine("<IRMNumber>" + dt.Rows[i]["IRMNo"].ToString() + "</IRMNumber>");
                sw.WriteLine("<FIRCNumber>" + dt.Rows[i]["Firc_No"].ToString() + "</FIRCNumber>");
                sw.WriteLine("<remittanceAdCode>" + dt.Rows[i]["RemAdCode"].ToString() + "</remittanceAdCode>");
                sw.WriteLine("<thirdParty>" + "N" + "</thirdParty>");
                sw.WriteLine("<realizedCurrencyCode>" + dt.Rows[i]["Currency"].ToString() + "</realizedCurrencyCode>");
                sw.WriteLine("<realizationDate>" + dt.Rows[i]["Document_Date"].ToString() + "</realizationDate>");
                sw.WriteLine("<invoices>");
                //sw.WriteLine("<ebrcNumber>" + dt.Rows[i]["EBRCNO"].ToString() + "</ebrcNumber>");
                //sw.WriteLine("<invoices>");
                _qurey = "TF_EDPMS_GetNoOfInvoices";
                string shippingbills = "";
                if (dt.Rows[i]["ShippingBillNo"].ToString() == "")
                {
                    shippingbills = dt.Rows[i]["FormNo"].ToString();
                }
                else
                {
                    shippingbills = dt.Rows[i]["ShippingBillNo"].ToString();
                }

                SqlParameter pSNo = new SqlParameter("@shippingBillNo", shippingbills);
                SqlParameter shippingDt = new SqlParameter("@shippingDt", dt.Rows[i]["Shipping_Bill_Date"].ToString());
                SqlParameter adcode1 = new SqlParameter("@Adcode", dt.Rows[i]["ADCode"].ToString());
                SqlParameter portcode1 = new SqlParameter("@PortCode", dt.Rows[i]["PortCode"].ToString());
                SqlParameter IecodE = new SqlParameter("@iecODE", dt.Rows[i]["IECode"].ToString());
                SqlParameter EBRCNO = new SqlParameter("@EBRCNO", dt.Rows[i]["EBRCNO"].ToString());
                SqlParameter docdate = new SqlParameter("@DocDate", dt.Rows[i]["Document_Date"].ToString());
                SqlParameter IRMNO = new SqlParameter("@irmno", dt.Rows[i]["IRMFIRC"].ToString());
                dtInvoice = obj.getData(_qurey, pSNo, shippingDt, adcode1, portcode1, IecodE, EBRCNO, docdate, IRMNO);

                //dtInvoice.DefaultView.Sort = "InvoiceNo, CloseOfBillIndication ASC";
                //DataTable sortedDtInvoice = dtInvoice.DefaultView.ToTable();

                for (int j = 0; j < dtInvoice.Rows.Count; j++)
                {
                    sw.WriteLine("<invoice>");
                    sw.WriteLine("<invoiceSerialNo>" + dtInvoice.Rows[j]["InvoiceSrNo"].ToString() + "</invoiceSerialNo>");
                    sw.WriteLine("<invoiceNo>" + dtInvoice.Rows[j]["InvoiceNo"].ToString() + "</invoiceNo>");
                    sw.WriteLine("<invoiceDate>" + dtInvoice.Rows[j]["Invoice_Date"].ToString() + "</invoiceDate>");
                    //sw.WriteLine("<realizedCurrencyCode>" + dtInvoice.Rows[j]["Currency"].ToString() + "</realizedCurrencyCode>");
                    sw.WriteLine("<accountNumber>" + dtInvoice.Rows[j]["AccountNo"].ToString() + "</accountNumber>");
                    //sw.WriteLine("<realizationStatus>" + dt.Rows[i]["RealizationStatus"].ToString() + "</realizationStatus>");
                    //sw.WriteLine("<FIRCNumber>" + dt.Rows[i]["Firc_No"].ToString() + "</FIRCNumber>");
                    //sw.WriteLine("<FIRCADCode>" + dt.Rows[i]["Firc_AD_Code"].ToString() + "</FIRCADCode>");
                    //sw.WriteLine("<realizedCurrencyCode>" + dt.Rows[i]["Currency"].ToString() + "</realizedCurrencyCode>");
                    //sw.WriteLine("<realizationDate>" + dt.Rows[i]["Document_Date"].ToString() + "</realizationDate>");
                    sw.WriteLine("<bankingChargesAmt>" + dtInvoice.Rows[j]["BankCharges"].ToString() + "</bankingChargesAmt>");
                    sw.WriteLine("<FOBAmt>" + dtInvoice.Rows[j]["FOB_Amount"].ToString() + "</FOBAmt>");
                    sw.WriteLine("<FOBAmtIC>" + dtInvoice.Rows[j]["FOB_Amount_IC"].ToString() + "</FOBAmtIC>");
                    sw.WriteLine("<freightAmt>" + dtInvoice.Rows[j]["Freight_Amount"].ToString() + "</freightAmt>");
                    sw.WriteLine("<freightAmtIC>" + dtInvoice.Rows[j]["Freight_Amount_IC"].ToString() + "</freightAmtIC>");
                    sw.WriteLine("<insuranceAmt>" + dtInvoice.Rows[j]["Insurance_Amount"].ToString() + "</insuranceAmt>");
                    sw.WriteLine("<insuranceAmtIC>" + dtInvoice.Rows[j]["Insurance_Amount_IC"].ToString() + "</insuranceAmtIC>");
                    //sw.WriteLine("<commissionAmt>" + dt.Rows[i]["Commission_Amount"].ToString() + "</commissionAmt>");
                    //sw.WriteLine("<commissionAmtIC>" + dt.Rows[i]["Commission_Amount_IC"].ToString() + "</commissionAmtIC>");
                    //sw.WriteLine("<packagingChargesAmt>" + dt.Rows[i]["Packaging_Charges_Amount"].ToString() + "</packagingChargesAmt>");
                    //sw.WriteLine("<packagingChargesAmtIC>" + dt.Rows[i]["Packaging_Charges_Amount_IC"].ToString() + "</packagingChargesAmtIC>");
                    //sw.WriteLine("<deductionAmt>" + dt.Rows[i]["Deduction_Amount"].ToString() + "</deductionAmt>");
                    //sw.WriteLine("<deductionAmtIC>" + dt.Rows[i]["Deduction_Amount_IC"].ToString() + "</deductionAmtIC>");
                    //sw.WriteLine("<discountAmt>" + dt.Rows[i]["Discount_Amount"].ToString() + "</discountAmt>");
                    //sw.WriteLine("<discountAmtIC>" + dt.Rows[i]["Discount_Amount_IC"].ToString() + "</discountAmtIC>");
                    sw.WriteLine("<remitterName>" + dtInvoice.Rows[j]["Buyer_Name"].ToString() + "</remitterName>");
                    sw.WriteLine("<remitterCountry>" + dtInvoice.Rows[j]["Buyer_Country"].ToString() + "</remitterCountry>");
                    sw.WriteLine("<closeOfBillIndicator>" + dtInvoice.Rows[j]["CloseOfBillIndication"].ToString() + "</closeOfBillIndicator>");
                    sw.WriteLine("</invoice>");

                    string query5 = "TF_EDPMS_Insert_Realized_Data_Created";

                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                    adcode.Value = dt.Rows[i]["ADCode"].ToString();
                    SqlParameter shipbillno = new SqlParameter("@shipbill", SqlDbType.VarChar);
                    shipbillno.Value = shippingbills;
                    SqlParameter shipbilldate = new SqlParameter("@shipbilldate", SqlDbType.VarChar);
                    shipbilldate.Value = dt.Rows[i]["Shipping_Bill_Date"].ToString();
                    SqlParameter invoiceno = new SqlParameter("@invoiceno", SqlDbType.VarChar);
                    invoiceno.Value = dtInvoice.Rows[j]["InvoiceNo"].ToString();
                    SqlParameter filename = new SqlParameter("@filename", SqlDbType.VarChar);
                    filename.Value = filename1;
                    SqlParameter addedby = new SqlParameter("@addedby", SqlDbType.VarChar);
                    addedby.Value = Session["userName"].ToString();
                    SqlParameter addedtime = new SqlParameter("@addedtime", SqlDbType.VarChar);
                    addedtime.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    /////added by shailesh 
                    SqlParameter closeBillInd = new SqlParameter("@closeBillInd", SqlDbType.VarChar);
                    closeBillInd.Value = dtInvoice.Rows[j]["CloseOfBillIndication"].ToString();

                    SqlParameter portcode = new SqlParameter("@PortCode", dt.Rows[i]["PortCode"].ToString());

                    obj.SaveDeleteData(query5, adcode, shipbillno, shipbilldate, invoiceno, filename, addedby, addedtime, portcode, IRMNO, closeBillInd);

                    //i++;


                }

                sw.WriteLine("</invoices>");
                sw.WriteLine("</shippingBill>");
            }
            sw.WriteLine("</shippingBills>");
            sw.Write("</bank>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            lblmessage.Font.Bold = true;
            string path = "file://" + _serverName + "/TF_GeneratedFiles/EDPMS/" + a + "/";
            string link = "/TF_GeneratedFiles/EDPMS/" + a + "/" + filename1 + ".prn.xml";
            lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

        }
        else
        {
            lblmessage.Text = "No Records";
        }
    }
}