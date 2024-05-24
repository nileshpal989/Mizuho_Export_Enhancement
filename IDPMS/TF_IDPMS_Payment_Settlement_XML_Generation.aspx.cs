using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class IDPMS_TF_IDPMS_Payment_Settlement_XML_Generation : System.Web.UI.Page
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
                ddlBranch.SelectedIndex = 1;
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                btnSave.Attributes.Add("onclick", "return Generate();");
                btnSaveCan.Attributes.Add("onclick", "return GenerateCancel();");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
            }
        }
        txtToDate.Attributes.Add("onblur", "return ValidDates();");
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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string shippingbill = "";
        int noorms = 0;
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        SqlParameter p4 = new SqlParameter("@Type", SqlDbType.VarChar);

        if (rdbOtt.Checked == true)
        {
            p4.Value = "OTT";
        }
        if (rdbOther.Checked == true)
        {
            p4.Value = "OTHER";
        }

        //string query1 = "IDPMS_PaymentSettlement";
        //DataTable dt2 = objData.getData(query1, p1, p2, p3);



        //// check entry in TF_IDPMS_Payment_Created table
        //DataTable chk = objData.getData("TF_IDPMS_Payment_Settlement_XML_Check", p1, p2, p3, p4);
        //if (chk.Rows.Count > 0)
        //{

        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "confirmation()", true);
        //    // on ok btnok_Click  
        //}
        //else
        //{
            string _qurey;
            _qurey = "TF_IDPMS_Payment_Settlement_XML_Generation";

            string Date = System.DateTime.Now.ToString("ddMMyyyy");
            string a = ddlBranch.Text;
            DataTable dt = objData.getData(_qurey, p1, p2, p3, p4);
            if (dt.Rows.Count > 0)
            {
                //string _directoryPath = "EDPMS";
                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                string query = "TF_IDPMS_GenerateFileName_Payment_Settlment";
                DataTable dtfile = objData.getData(query);
                string filename1 = dtfile.Rows[0]["FileName"].ToString();
                string Adcode = ddlBranch.SelectedValue.ToString();

                //for (int i = 0; i < dt2.Rows.Count; i++)
                //{

                //    noorms = noorms + 1;
                //}


                string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".bes.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfbillOfEntry>" + dt.Rows[0]["NoOfBill"].ToString() + "</noOfbillOfEntry>");
                sw.WriteLine("<noOfInvoices>" + dt.Rows[0]["noofinvoices"].ToString() + "</noOfInvoices>");
                sw.WriteLine("</checkSum>");
                sw.WriteLine("<billOfEntrys>");



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine("<billOfEntry>");
                    sw.WriteLine("<portOfDischarge>" + dt.Rows[i]["PortCode"].ToString() + "</portOfDischarge>");
                    sw.WriteLine("<billOfEntryNumber>" + dt.Rows[i]["Bill_No"].ToString() + "</billOfEntryNumber>");
                    sw.WriteLine("<billOfEntryDate>" + dt.Rows[i]["Bill_Date"].ToString() + "</billOfEntryDate>");
                    sw.WriteLine("<IECode>" + dt.Rows[i]["IECODE"].ToString() + "</IECode>");
                    //sw.WriteLine("<changeIECode>" + "" + "</changeIECode>");
                    sw.WriteLine("<changeIECode>" + dt.Rows[i]["changeIECode"].ToString() + "</changeIECode>");
                    sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
                    sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Record_Inc"].ToString() + "</recordIndicator>");
                    sw.WriteLine("<paymentParty>" + dt.Rows[i]["paymentParty"].ToString() + "</paymentParty>");
                    sw.WriteLine("<paymentReferenceNumber>" + dt.Rows[i]["paymentReferenceNumber"].ToString() + "</paymentReferenceNumber>");
                    sw.WriteLine("<outwardReferenceNumber>" + dt.Rows[i]["outwardReferenceNumber"].ToString() + "</outwardReferenceNumber>");
                    sw.WriteLine("<outwardReferenceADCode>" + dt.Rows[i]["outwardReferenceADCode"].ToString() + "</outwardReferenceADCode>");
                    sw.WriteLine("<remittanceCurrency>" + dt.Rows[i]["remittanceCurrency"].ToString() + "</remittanceCurrency>");
                    sw.WriteLine("<billClosureIndicator>" + dt.Rows[i]["billClosureIndicator"].ToString() + "</billClosureIndicator>");


                    DataTable dt3 = new DataTable();
                    string GetInvoiceDeatils = "TF_IDPMS_Payment_Settlement_get_Invoice_Dateils";
                    SqlParameter BillNo = new SqlParameter("@BillNo", dt.Rows[i]["Bill_No"].ToString());
                    SqlParameter BillDate = new SqlParameter("@BillDate", dt.Rows[i]["Bill_Date"].ToString());
                    SqlParameter PortCode = new SqlParameter("@PortCode", dt.Rows[i]["PortCode"].ToString());
                    SqlParameter Doc_No = new SqlParameter("@Doc_No", dt.Rows[i]["outwardReferenceNumber"].ToString());
                    SqlParameter Payment_Ref = new SqlParameter("@Payment_Ref", dt.Rows[i]["paymentReferenceNumber"].ToString());
                    SqlParameter billClosureIndicator = new SqlParameter("@billClosureIndicator", dt.Rows[i]["billClosureIndicator"].ToString());
                    dt3 = objData.getData(GetInvoiceDeatils, BillNo, BillDate, PortCode, Doc_No, Payment_Ref, p4, billClosureIndicator);


                    if (dt3.Rows.Count > 0)
                    {
                        sw.WriteLine("<invoices>");
                        for (int j = 0; j < dt3.Rows.Count; j++)
                        {


                            sw.WriteLine("<invoice>");
                            sw.WriteLine("<invoiceSerialNo>" + dt3.Rows[j]["invoiceSerialNo"].ToString() + "</invoiceSerialNo>");
                            sw.WriteLine("<invoiceNo>" + dt3.Rows[j]["invoiceNo"].ToString() + "</invoiceNo>");
                            sw.WriteLine("<invoiceAmt>" + dt3.Rows[j]["invoiceAmt"].ToString() + "</invoiceAmt>");
                            sw.WriteLine("<invoiceAmtIc>" + dt3.Rows[j]["invoiceAmtIc"].ToString() + "</invoiceAmtIc>");
                            sw.WriteLine("</invoice>");


                            // sw.WriteLine("</invoices>");
                            //  sw.WriteLine("</billOfEntry>");


                            string query5 = "TF_Payment_Settlement_Created";


                            SqlParameter portOfDischarge = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
                            portOfDischarge.Value = dt.Rows[i]["PortCode"].ToString();
                            SqlParameter billOfEntryNumber = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
                            billOfEntryNumber.Value = dt.Rows[i]["Bill_No"].ToString();
                            SqlParameter billOfEntryDate = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
                            billOfEntryDate.Value = dt.Rows[i]["Bill_Date"].ToString();
                            SqlParameter IECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                            IECode.Value = dt.Rows[i]["IECODE"].ToString();
                            SqlParameter paymentReferenceNumber = new SqlParameter("@paymentReferenceNumber", SqlDbType.VarChar);
                            paymentReferenceNumber.Value = dt.Rows[i]["paymentReferenceNumber"].ToString();
                            SqlParameter invoiceSerialNo = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                            invoiceSerialNo.Value = dt3.Rows[j]["invoiceSerialNo"].ToString();

                            SqlParameter invoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                            invoiceNo.Value = dt3.Rows[j]["invoiceNo"].ToString();
                            SqlParameter FileName = new SqlParameter("@FileName", SqlDbType.VarChar);
                            FileName.Value = filename1;


                            //DataTable check = objData.getData("TF_Payment_Settlement_Created_check", portOfDischarge, billOfEntryNumber, billOfEntryDate, IECode, paymentReferenceNumber, invoiceSerialNo, invoiceNo, FileName, p2, p3);
                            ////p2 fromdate, p3 todate 
                            //string res = check.Rows.Count.ToString();
                            //if (check.Rows.Count == 0)
                            //{
                            string result = objData.SaveDeleteData(query5, portOfDischarge, billOfEntryNumber, billOfEntryDate, IECode, paymentReferenceNumber, invoiceSerialNo, invoiceNo, FileName, p2, p3);
                            // }
                        }
                    }
                    sw.WriteLine("</invoices>");
                    sw.WriteLine("</billOfEntry>");

                }
                //sw.WriteLine("</invoices>");
                // sw.WriteLine("</billOfEntry>");
                sw.WriteLine("</billOfEntrys>");
                sw.WriteLine("</bank>");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();
                lblmessage.Font.Bold = true;
                string path = "file://" + _serverName + "../TF_GeneratedFiles/IDPMS";
                string link = "../TF_GeneratedFiles/IDPMS";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
            else
            {
                lblmessage.Text = "No Records";
            }
        //}

    }

    protected void btnSaveCan_Click(object sender, EventArgs e)
    {

        string shippingbill = "";
        int noorms = 0;
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        SqlParameter p4 = new SqlParameter("@Type", SqlDbType.VarChar);

        if (rdbOtt.Checked == true)
        {
            p4.Value = "OTT";
        }
        if (rdbOther.Checked == true)
        {
            p4.Value = "OTHER";
        }

        //string query1 = "IDPMS_PaymentSettlement";
        //DataTable dt2 = objData.getData(query1, p1, p2, p3);


        string Date = System.DateTime.Now.ToString("ddMMyyyy");
        string a = ddlBranch.Text;
        string _qurey = "TF_IDPMS_Payment_Settlement_Cancel_XML_Generation";
        DataTable dt = objData.getData(_qurey, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            //string _directoryPath = "EDPMS";
            string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string query = "TF_IDPMS_GenerateFileName_Payment_Settlment_Cancel";
            DataTable dtfile = objData.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string Adcode = ddlBranch.SelectedValue.ToString();

            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{

            //    noorms = noorms + 1;
            //}


            string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".bes.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

            sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfbillOfEntry>" + dt.Rows[0]["NoOfBill"].ToString() + "</noOfbillOfEntry>");
            sw.WriteLine("<noOfInvoices>" + dt.Rows[0]["noofinvoices"].ToString() + "</noOfInvoices>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<billOfEntrys>");



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("<billOfEntry>");
                sw.WriteLine("<portOfDischarge>" + dt.Rows[i]["PortCode"].ToString() + "</portOfDischarge>");
                sw.WriteLine("<billOfEntryNumber>" + dt.Rows[i]["Bill_No"].ToString() + "</billOfEntryNumber>");
                sw.WriteLine("<billOfEntryDate>" + dt.Rows[i]["Bill_Date"].ToString() + "</billOfEntryDate>");
                sw.WriteLine("<IECode>" + dt.Rows[i]["IECODE"].ToString() + "</IECode>");
                //sw.WriteLine("<changeIECode>" + "" + "</changeIECode>");
                sw.WriteLine("<changeIECode>" + dt.Rows[i]["changeIECode"].ToString() + "</changeIECode>");
                sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Record_Inc"].ToString() + "</recordIndicator>");
                sw.WriteLine("<paymentParty>" + dt.Rows[i]["paymentParty"].ToString() + "</paymentParty>");
                sw.WriteLine("<paymentReferenceNumber>" + dt.Rows[i]["paymentReferenceNumber"].ToString() + "</paymentReferenceNumber>");
                sw.WriteLine("<outwardReferenceNumber>" + dt.Rows[i]["outwardReferenceNumber"].ToString() + "</outwardReferenceNumber>");
                sw.WriteLine("<outwardReferenceADCode>" + dt.Rows[i]["outwardReferenceADCode"].ToString() + "</outwardReferenceADCode>");
                sw.WriteLine("<remittanceCurrency>" + dt.Rows[i]["remittanceCurrency"].ToString() + "</remittanceCurrency>");
                sw.WriteLine("<billClosureIndicator>" + dt.Rows[i]["billClosureIndicator"].ToString() + "</billClosureIndicator>");


                DataTable dt3 = new DataTable();
                string GetInvoiceDeatils = "TF_IDPMS_Payment_Settlement_Cancel_get_Invoice_Dateils";
                SqlParameter BillNo = new SqlParameter("@BillNo", dt.Rows[i]["Bill_No"].ToString());
                SqlParameter BillDate = new SqlParameter("@BillDate", dt.Rows[i]["Bill_Date"].ToString());
                SqlParameter PortCode = new SqlParameter("@PortCode", dt.Rows[i]["PortCode"].ToString());
                SqlParameter Doc_No = new SqlParameter("@Doc_No", dt.Rows[i]["outwardReferenceNumber"].ToString());
                SqlParameter Payment_Ref = new SqlParameter("@Payment_Ref", dt.Rows[i]["paymentReferenceNumber"].ToString());
                SqlParameter billClosureIndicator = new SqlParameter("@billClosureIndicator", dt.Rows[i]["billClosureIndicator"].ToString());
                dt3 = objData.getData(GetInvoiceDeatils, BillNo, BillDate, PortCode, Doc_No, Payment_Ref, p4, billClosureIndicator);


                if (dt3.Rows.Count > 0)
                {
                    sw.WriteLine("<invoices>");
                    for (int j = 0; j < dt3.Rows.Count; j++)
                    {


                        sw.WriteLine("<invoice>");
                        sw.WriteLine("<invoiceSerialNo>" + dt3.Rows[j]["invoiceSerialNo"].ToString() + "</invoiceSerialNo>");
                        sw.WriteLine("<invoiceNo>" + dt3.Rows[j]["invoiceNo"].ToString() + "</invoiceNo>");
                        sw.WriteLine("<invoiceAmt>" + dt3.Rows[j]["invoiceAmt"].ToString() + "</invoiceAmt>");
                        sw.WriteLine("<invoiceAmtIc>" + dt3.Rows[j]["invoiceAmtIc"].ToString() + "</invoiceAmtIc>");
                        sw.WriteLine("</invoice>");


                        // sw.WriteLine("</invoices>");
                        //  sw.WriteLine("</billOfEntry>");

                        string query5 = "TF_Payment_Settlement_Cancel_Created";




                        SqlParameter portOfDischarge = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
                        portOfDischarge.Value = dt.Rows[i]["PortCode"].ToString();
                        SqlParameter billOfEntryNumber = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
                        billOfEntryNumber.Value = dt.Rows[i]["Bill_No"].ToString();
                        SqlParameter billOfEntryDate = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
                        billOfEntryDate.Value = dt.Rows[i]["Bill_Date"].ToString();
                        SqlParameter IECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                        IECode.Value = dt.Rows[i]["IECODE"].ToString();
                        SqlParameter paymentReferenceNumber = new SqlParameter("@paymentReferenceNumber", SqlDbType.VarChar);
                        paymentReferenceNumber.Value = dt.Rows[i]["paymentReferenceNumber"].ToString();
                        SqlParameter invoiceSerialNo = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                        invoiceSerialNo.Value = dt3.Rows[j]["invoiceSerialNo"].ToString();



                        SqlParameter invoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                        invoiceNo.Value = dt3.Rows[j]["invoiceNo"].ToString();
                        SqlParameter FileName = new SqlParameter("@FileName", SqlDbType.VarChar);
                        FileName.Value = filename1;

                        string result = objData.SaveDeleteData(query5, portOfDischarge, billOfEntryNumber, billOfEntryDate, IECode, paymentReferenceNumber, invoiceSerialNo, invoiceNo, FileName);
                    }
                }
                sw.WriteLine("</invoices>");
                sw.WriteLine("</billOfEntry>");

            }
            //sw.WriteLine("</invoices>");
            // sw.WriteLine("</billOfEntry>");
            sw.WriteLine("</billOfEntrys>");
            sw.WriteLine("</bank>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            lblmessage.Font.Bold = true;
            string path = "file://" + _serverName + "../TF_GeneratedFiles/IDPMS";
            string link = "../TF_GeneratedFiles/IDPMS";
            lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
        {
            lblmessage.Text = "No Records";
        }

    }
    //protected void reGeneration_Click(object sender, EventArgs e)
    //{
    //    string shippingbill = "";
    //    int noorms = 0;
    //    System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
    //    dateInfo.ShortDatePattern = "dd/MM/yyyy";

    //    DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
    //    DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
    //    p1.Value = ddlBranch.Text.Trim();

    //    SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
    //    p2.Value = txtfromDate.Text.ToString();

    //    SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
    //    p3.Value = txtToDate.Text.ToString();

    //    SqlParameter p4 = new SqlParameter("@Type", SqlDbType.VarChar);

    //    if (rdbOtt.Checked == true)
    //    {
    //        p4.Value = "OTT";
    //    }
    //    if (rdbOther.Checked == true)
    //    {
    //        p4.Value = "OTHER";
    //    }

    //    //string query1 = "IDPMS_PaymentSettlement";
    //    //DataTable dt2 = objData.getData(query1, p1, p2, p3);

    //    string _qurey;
    //    _qurey = "TF_IDPMS_Payment_Settlement_XML_ReGeneration";

    //    string Date = System.DateTime.Now.ToString("ddMMyyyy");
    //    string a = ddlBranch.Text;
    //    DataTable dt = objData.getData(_qurey, p1, p2, p3, p4);
    //    if (dt.Rows.Count > 0)
    //    {
    //        //string _directoryPath = "EDPMS";
    //        string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
    //        if (!Directory.Exists(_directoryPath))
    //        {
    //            Directory.CreateDirectory(_directoryPath);
    //        }

    //        string query = "TF_IDPMS_GenerateFileName_Payment_Settlment";
    //        DataTable dtfile = objData.getData(query);
    //        string filename1 = dtfile.Rows[0]["FileName"].ToString();
    //        string Adcode = ddlBranch.SelectedValue.ToString();

    //        //for (int i = 0; i < dt2.Rows.Count; i++)
    //        //{

    //        //    noorms = noorms + 1;
    //        //}


    //        string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".bes.xml";
    //        StreamWriter sw;
    //        sw = File.CreateText(_filePath);

    //        sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
    //        sw.WriteLine("<bank>");
    //        sw.WriteLine("<checkSum>");
    //        sw.WriteLine("<noOfbillOfEntry>" + dt.Rows[0]["NoOfBill"].ToString() + "</noOfbillOfEntry>");
    //        sw.WriteLine("<noOfInvoices>" + dt.Rows[0]["noofinvoices"].ToString() + "</noOfInvoices>");
    //        sw.WriteLine("</checkSum>");
    //        sw.WriteLine("<billOfEntrys>");



    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            sw.WriteLine("<billOfEntry>");
    //            sw.WriteLine("<portOfDischarge>" + dt.Rows[i]["PortCode"].ToString() + "</portOfDischarge>");
    //            sw.WriteLine("<billOfEntryNumber>" + dt.Rows[i]["Bill_No"].ToString() + "</billOfEntryNumber>");
    //            sw.WriteLine("<billOfEntryDate>" + dt.Rows[i]["Bill_Date"].ToString() + "</billOfEntryDate>");
    //            sw.WriteLine("<IECode>" + dt.Rows[i]["IECODE"].ToString() + "</IECode>");
    //            //sw.WriteLine("<changeIECode>" + "" + "</changeIECode>");
    //            sw.WriteLine("<changeIECode>" + dt.Rows[i]["changeIECode"].ToString() + "</changeIECode>");
    //            sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
    //            sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Record_Inc"].ToString() + "</recordIndicator>");
    //            sw.WriteLine("<paymentParty>" + dt.Rows[i]["paymentParty"].ToString() + "</paymentParty>");
    //            sw.WriteLine("<paymentReferenceNumber>" + dt.Rows[i]["paymentReferenceNumber"].ToString() + "</paymentReferenceNumber>");
    //            sw.WriteLine("<outwardReferenceNumber>" + dt.Rows[i]["outwardReferenceNumber"].ToString() + "</outwardReferenceNumber>");
    //            sw.WriteLine("<outwardReferenceADCode>" + dt.Rows[i]["outwardReferenceADCode"].ToString() + "</outwardReferenceADCode>");
    //            sw.WriteLine("<remittanceCurrency>" + dt.Rows[i]["remittanceCurrency"].ToString() + "</remittanceCurrency>");
    //            sw.WriteLine("<billClosureIndicator>" + dt.Rows[i]["billClosureIndicator"].ToString() + "</billClosureIndicator>");


    //            DataTable dt3 = new DataTable();
    //            string GetInvoiceDeatils = "TF_IDPMS_Payment_Settlement_get_Invoice_Dateils";
    //            SqlParameter BillNo = new SqlParameter("@BillNo", dt.Rows[i]["Bill_No"].ToString());
    //            SqlParameter BillDate = new SqlParameter("@BillDate", dt.Rows[i]["Bill_Date"].ToString());
    //            SqlParameter PortCode = new SqlParameter("@PortCode", dt.Rows[i]["PortCode"].ToString());
    //            SqlParameter Doc_No = new SqlParameter("@Doc_No", dt.Rows[i]["outwardReferenceNumber"].ToString());
    //            SqlParameter Payment_Ref = new SqlParameter("@Payment_Ref", dt.Rows[i]["paymentReferenceNumber"].ToString());
    //            SqlParameter billClosureIndicator = new SqlParameter("@billClosureIndicator", dt.Rows[i]["billClosureIndicator"].ToString());
    //            dt3 = objData.getData(GetInvoiceDeatils, BillNo, BillDate, PortCode, Doc_No, Payment_Ref, p4, billClosureIndicator);


    //            if (dt3.Rows.Count > 0)
    //            {
    //                sw.WriteLine("<invoices>");
    //                for (int j = 0; j < dt3.Rows.Count; j++)
    //                {


    //                    sw.WriteLine("<invoice>");
    //                    sw.WriteLine("<invoiceSerialNo>" + dt3.Rows[j]["invoiceSerialNo"].ToString() + "</invoiceSerialNo>");
    //                    sw.WriteLine("<invoiceNo>" + dt3.Rows[j]["invoiceNo"].ToString() + "</invoiceNo>");
    //                    sw.WriteLine("<invoiceAmt>" + dt3.Rows[j]["invoiceAmt"].ToString() + "</invoiceAmt>");
    //                    sw.WriteLine("<invoiceAmtIc>" + dt3.Rows[j]["invoiceAmtIc"].ToString() + "</invoiceAmtIc>");
    //                    sw.WriteLine("</invoice>");


    //                    // sw.WriteLine("</invoices>");
    //                    //  sw.WriteLine("</billOfEntry>");


    //                    string query5 = "TF_Payment_Settlement_Created";


    //                    SqlParameter portOfDischarge = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
    //                    portOfDischarge.Value = dt.Rows[i]["PortCode"].ToString();
    //                    SqlParameter billOfEntryNumber = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
    //                    billOfEntryNumber.Value = dt.Rows[i]["Bill_No"].ToString();
    //                    SqlParameter billOfEntryDate = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
    //                    billOfEntryDate.Value = dt.Rows[i]["Bill_Date"].ToString();
    //                    SqlParameter IECode = new SqlParameter("@IECode", SqlDbType.VarChar);
    //                    IECode.Value = dt.Rows[i]["IECODE"].ToString();
    //                    SqlParameter paymentReferenceNumber = new SqlParameter("@paymentReferenceNumber", SqlDbType.VarChar);
    //                    paymentReferenceNumber.Value = dt.Rows[i]["paymentReferenceNumber"].ToString();
    //                    SqlParameter invoiceSerialNo = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
    //                    invoiceSerialNo.Value = dt3.Rows[j]["invoiceSerialNo"].ToString();

    //                    SqlParameter invoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
    //                    invoiceNo.Value = dt3.Rows[j]["invoiceNo"].ToString();
    //                    SqlParameter FileName = new SqlParameter("@FileName", SqlDbType.VarChar);
    //                    FileName.Value = filename1;


    //                    DataTable check = objData.getData("TF_Payment_Settlement_Created_check", portOfDischarge, billOfEntryNumber, billOfEntryDate, IECode, paymentReferenceNumber, invoiceSerialNo, invoiceNo, FileName, p2, p3);
    //                    //p2 fromdate, p3 todate 
    //                    string res = check.Rows.Count.ToString();
    //                    if (check.Rows.Count == 0)
    //                    {
    //                        string result = objData.SaveDeleteData(query5, portOfDischarge, billOfEntryNumber, billOfEntryDate, IECode, paymentReferenceNumber, invoiceSerialNo, invoiceNo, FileName, p2, p3);
    //                    }
    //                }
    //            }
    //            sw.WriteLine("</invoices>");
    //            sw.WriteLine("</billOfEntry>");

    //        }
    //        //sw.WriteLine("</invoices>");
    //        // sw.WriteLine("</billOfEntry>");
    //        sw.WriteLine("</billOfEntrys>");
    //        sw.WriteLine("</bank>");
    //        sw.Flush();
    //        sw.Close();
    //        sw.Dispose();
    //        TF_DATA objServerName = new TF_DATA();
    //        string _serverName = objServerName.GetServerName();
    //        lblmessage.Font.Bold = true;
    //        string path = "file://" + _serverName + "../TF_GeneratedFiles/IDPMS";
    //        string link = "../TF_GeneratedFiles/IDPMS";
    //        lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
    //    }
    //    else
    //    {
    //        lblmessage.Text = "No Records";
    //    }
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }
}