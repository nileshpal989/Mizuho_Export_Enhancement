using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class IDPMS_TF_IDPMS_Manual_Bill_File_Created_ : System.Web.UI.Page
{
    string cust;
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    clearControls();
                    fillddlBranch();
                    ddlBranch.Focus();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                }
                btnCreate.Attributes.Add("onclick", "return validateSave();");


            }

        }
    }

    protected void clearControls()
    {

        labelMessage.Text = "";

        ddlBranch.Focus();



    }
    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";

        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            //li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        // ddlBranch.Items.Insert(1, li01);
    }





    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";


        DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);


        string _directoryPath = ddlBranch.Text.ToString() + "-" + documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy");
        _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/");



        //if (!Directory.Exists(_directoryPath))
        //{
        //    Directory.CreateDirectory(_directoryPath);
        //}
        //For csv file creation
        TF_DATA objData = new TF_DATA();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtFromDate.Text;

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text;



        string _qry = "TF_IDPMS_Manual_Bill_CVS_File_Generation";
        DataTable dt = objData.getData(_qry, p2, p3);
        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string query = "TF_IDPMS_GenerateFileName_Manual";
            DataTable dtfile = objData.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string Adcode = ddlBranch.SelectedValue.ToString();

            string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".mbe.csv";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            sw.WriteLine("portOfDischarge|importAgency|billOfEntryNumber|billOfEntryDate|ADCode|G-P|IECode|IEName|IEAddress|IEPANNumber|portOfShipment|recordIndicator|IGMNumber|IGMDate|MAWB-MBLNumber|MAWB-MBLDate|HAWB-HBLNumber|HAWB-HBLDate|invoiceSerialNo|invoiceNo|termsOfInvoice|supplierName|supplierAddress|supplierCountry|sellerName|sellerAddress|sellerCountry|invoiceAmount|invoiceCurrency|freightAmount|freightCurrencyCode|insuranceAmount|insuranceCurrencyCode|agencyCommission|agencyCurrency|discountCharges|discountCurrency|miscellaneousCharges|miscellaneousCurrency|thirdPartyName|thirdPartyAddress|thirdPartyCountry");
            for (int j = 0; j < dt.Rows.Count; j++)      
            {

                string _strPortCode = dt.Rows[j]["PortCode"].ToString().Trim();
                string _strPortCode1 = _strPortCode.Replace(",", " -");
                sw.Write(_strPortCode1 + "|");


                /*1*/
                string _Import_Agency = dt.Rows[j]["Import_Agency"].ToString().Trim();
                string _Import_Agency1 = _Import_Agency.Replace(",", " -");
                sw.Write(_Import_Agency1 + "|");

                /*2*/
                string _Bill_No = dt.Rows[j]["Bill_No"].ToString().Trim();
                string _Bill_No1 = _Bill_No.Replace(",", " -");
                sw.Write(_Bill_No1 + "|");


                string _Bill_Date = dt.Rows[j]["Bill_Date"].ToString().Trim();
                string _Bill_Date1 = _Bill_Date.Replace(",", " -");
                sw.Write(_Bill_Date1 + "|");
                /*3*/
                string _AdCode = dt.Rows[j]["AdCode"].ToString().Trim();
                string _AdCode1 = _AdCode.Replace(",", " -");
                sw.Write(_AdCode1 + "|");

                /*4*/

                string _GP = dt.Rows[j]["GP"].ToString().Trim();
                string _GP1 = _GP.Replace(",", " -");
                sw.Write(_GP1 + "|");


                /*5*/
                string IECode = dt.Rows[j]["CUST_IE_CODE"].ToString().Trim();
                string IECode1 = IECode.Replace(",", " -");
                sw.Write(IECode1 + "|");

                /*6*/

                string IEName = dt.Rows[j]["CUST_NAME"].ToString().Trim();
                string IEName1 = IEName.Replace(",", " -");
                sw.Write(IEName1 + "|");


                string IEAddress = dt.Rows[j]["CUST_ADDRESS"].ToString().Trim();
                string IEAddress1 = IEAddress.Replace(",", " -");
                sw.Write(IEAddress1 + "|");


                string IEPANNumber = dt.Rows[j]["CUST_PAN_NO"].ToString().Trim();
                string IEPANNumber1 = IEPANNumber.Replace(",", " -");
                sw.Write(IEPANNumber1 + "|");


                string PortShipment = dt.Rows[j]["Port_Of_Ship"].ToString().Trim();
                string PortShipment1 = PortShipment.Replace(",", " -");
                sw.Write(PortShipment1 + "|");



                string Rec_Inc = dt.Rows[j]["Rec_Inc"].ToString().Trim();
                string Rec_Inc1 = Rec_Inc.Replace(",", " -");
                sw.Write(Rec_Inc1 + "|");


                /*7*/
                string IGMNumber = "";
                sw.Write(IGMNumber + "|");
                /*8*/
                string IGMDate = "";
                sw.Write(IGMDate + "|");
                /*9*/
                string MBLNumber = "";
                sw.Write(MBLNumber + "|");
                /*10*/
                string MBLDate = "";
                sw.Write(MBLDate + "|");
                /*11*/
                string HBLNumber = "";
                sw.Write(HBLNumber + "|");


                /*12*/
                string HBLDate = "";
                sw.Write(HBLDate + "|");
                /*13*/
                string invoiceSerialNo = dt.Rows[j]["InvoiceSerialNo"].ToString().Trim();
                string invoiceSerialNo1 = invoiceSerialNo.Replace(",", " -");
                sw.Write(invoiceSerialNo1 + "|");

                /*14*/
                string invoiceNo = dt.Rows[j]["InvoiceNo"].ToString().Trim();
                string invoiceNo1 = invoiceNo.Replace(",", " -");
                sw.Write(invoiceNo1 + "|");

                /*15*/
                string termsOfInvoice = dt.Rows[j]["TermsofInvoice"].ToString().Trim();
                string termsOfInvoice1 = termsOfInvoice.Replace(",", " -");
                sw.Write(termsOfInvoice1 + "|");

                /*16*/
                string supplierName = dt.Rows[j]["Party_Code"].ToString().Trim();
                string supplierName1 = supplierName.Replace(",", " -");
                sw.Write(supplierName1 + "|");

                /*17*/
                string supplierAddress = dt.Rows[j]["Party_Address"].ToString().Trim();
                string supplierAddress1 = supplierAddress.Replace(",", " -");
                sw.Write(supplierAddress1 + "|");

                /*18*/
                string supplierCountry = dt.Rows[j]["Party_Country"].ToString().Trim();
                string supplierCountry1 = supplierCountry.Replace(",", " -");
                sw.Write(supplierCountry1 + "|");

                /*19*/

                string sellerName = "";
                sw.Write(sellerName + "|");

                /*20*/

                string sellerAddress = "";
                sw.Write(sellerAddress + "|");

                /*21*/

                string sellerCountry = "";
                sw.Write(sellerCountry + "|");

                /*22*/
                string invoiceAmount = dt.Rows[j]["invoiceAmt"].ToString().Trim();
                string invoiceAmount1 = invoiceAmount.Replace(",", " -");
                sw.Write(invoiceAmount1 + "|");

                /*23*/
                string invoiceCurrency = dt.Rows[j]["remittanceCurrency"].ToString().Trim();
                //string invoiceCurrency1 = invoiceCurrency.Replace(",", " -");
                sw.Write(invoiceCurrency + "|");

                /*24*/

                string freightAmount = "";
                sw.Write(freightAmount + "|");

                /*25*/
                string freightCurrencyCode = "";
                sw.Write(freightCurrencyCode + "|");

                /*26*/

                string insuranceAmount = "";
                sw.Write(insuranceAmount + "|");

                /*27*/

                string insuranceCurrencyCode = "";
                sw.Write(insuranceCurrencyCode + "|");

                /*28**/
                string agencyCommission = "";
                sw.Write(agencyCommission + "|");

                /*29*/
                string agencyCurrency = "";
                sw.Write(agencyCurrency + "|");

                /*30*/

                string discountCharges = "";
                sw.Write(discountCharges + "|");

                /*31*/
                string discountCurrency = "";
                sw.Write(discountCurrency + "|");

                /*32*/

                string miscellaneousCharges = "";
                sw.Write(miscellaneousCharges + "|");

                /*33*/
                string miscellaneousCurrency = "";
                sw.Write(miscellaneousCurrency + "|");

                /*34*/
                string thirdPartyName = "";
                //string thirdPartyName1 = thirdPartyName.Replace(",", " -");
                sw.Write(thirdPartyName + "|");

                /*35*/
                string thirdPartyAddress = "";
                //string thirdPartyAddress1 = thirdPartyAddress.Replace(",", " -");
                sw.Write(thirdPartyAddress + "|");

                /*36*/
                string thirdPartyCountry = "";
                //string thirdPartyCountry1 = thirdPartyCountry.Replace(",", " -");
                sw.WriteLine(thirdPartyCountry);


                string query2 = "TF_IDPMS_Manual_Bill_Inserted";

                SqlParameter p8 = new SqlParameter("@Bill_No", dt.Rows[j]["Bill_No"].ToString().Trim());
                SqlParameter p9 = new SqlParameter("@Bill_Entry_Date", dt.Rows[j]["Bill_Date"].ToString().Trim());
                SqlParameter p10 = new SqlParameter("@Port_Code", dt.Rows[j]["PortCode"].ToString().Trim());
                SqlParameter p11 = new SqlParameter("@Invoice_No", dt.Rows[j]["InvoiceNo"].ToString().Trim());
                SqlParameter p12 = new SqlParameter("@FileName", filename1);


                string result = objData.SaveDeleteData(query2, p8, p9, p10, p11, p12);

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

            string path = "file://" + _serverName + "../TF_GeneratedFiles/IDPMS";
            string link = "../TF_GeneratedFiles/IDPMS/";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            //labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);
            ddlBranch.Focus();
            //string path = "file://" + _serverName + "/GeneratedFiles/EXPORT";
            //string link = "/GeneratedFiles/EXPORT";
            //labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
        {
            //labelMessage.Text = "No Reocrds ";
            labelMessage.Text = "There is No Record Between This Dates " + documentDate.ToString("dd/MM/yyyy") + "-" + documentDate1.ToString("dd/MM/yyyy");
            //ddlBranch.Focus();

            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);
        }

    }


}