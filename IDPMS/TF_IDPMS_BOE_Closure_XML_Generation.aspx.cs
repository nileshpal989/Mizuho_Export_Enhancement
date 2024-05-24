using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
public partial class IDPMS_TF_IDPMS_BOE_Closure_XML_Generation : System.Web.UI.Page
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

                btnSave.Attributes.Add("onclick", "return Generate();");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
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
        p1.Value = ddlBranch.SelectedValue.Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        //string query1 = "IDPMS_PaymentSettlement";
        //DataTable dt2 = objData.getData(query1, p1, p2, p3);


        string Date = System.DateTime.Now.ToString("ddMMyyyy");
        string a = ddlBranch.Text;
        string _qurey = "TF_IDPMS_BOE_Closure_XML_Generation";
        DataTable dt = objData.getData(_qurey, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            //string _directoryPath = "EDPMS";
            string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string query = "TF_IDPMS_GenerateFileName_BOE_Closure";
            DataTable dtfile = objData.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string Adcode = ddlBranch.SelectedValue.ToString();
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{

            //    noorms = noorms + 1;
            //}


            string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".bea.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

            sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfBOE>" + dt.Rows[0]["NoOfBill"].ToString() + "</noOfBOE>");
            sw.WriteLine("<noOfInvoices>" + dt.Rows[0]["noofinvoices"].ToString() + "</noOfInvoices>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<billOfEntries>");



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("<billOfEntry>");
                sw.WriteLine("<portOfDischarge>" + dt.Rows[i]["portOfDischarge"].ToString() + "</portOfDischarge>");
                sw.WriteLine("<billOfEntryNumber>" + dt.Rows[i]["billOfEntryNumber"].ToString() + "</billOfEntryNumber>");
                sw.WriteLine("<billOfEntryDate>" + dt.Rows[i]["billOfEntryDate"].ToString() + "</billOfEntryDate>");
                sw.WriteLine("<IECode>" + dt.Rows[i]["IECODE"].ToString() + "</IECode>");
                sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Record_Inc"].ToString() + "</recordIndicator>");
                sw.WriteLine("<adjustmentReferenceNumber>" + dt.Rows[i]["adjustmentReferenceNumber"].ToString() + "</adjustmentReferenceNumber>");
                sw.WriteLine("<closeofBillIndicator>" + dt.Rows[i]["closeofBillIndicator"].ToString() + "</closeofBillIndicator>");
                sw.WriteLine("<adjustmentDate>" + dt.Rows[i]["adjustmentDate"].ToString() + "</adjustmentDate>");
                sw.WriteLine("<adjustmentIndicator>" + dt.Rows[i]["adjustmentIndicator"].ToString() + "</adjustmentIndicator>");
                sw.WriteLine("<documentNumber>" + dt.Rows[i]["documentNumber"].ToString() + "</documentNumber>");
                sw.WriteLine("<documentDate>" + dt.Rows[i]["documentDate"].ToString() + "</documentDate>");
                sw.WriteLine("<documentPort>" + dt.Rows[i]["documentPort"].ToString() + "</documentPort>");
                sw.WriteLine("<approvedBy>" + dt.Rows[i]["approvedBy"].ToString() + "</approvedBy>");
                sw.WriteLine("<letterNumber >" + dt.Rows[i]["letterNumber"].ToString() + "</letterNumber >");
                sw.WriteLine("<letterDate >" + dt.Rows[i]["letterDate"].ToString() + "</letterDate >");
                sw.WriteLine("<Remark >" + dt.Rows[i]["Remark"].ToString() + "</Remark >");



                DataTable dt3 = new DataTable();
                string GetInvoiceDeatils = "TF_IDPMS_get_Invoice_Dateils_BOE_Closure";
                SqlParameter q1 = new SqlParameter("@adjustmentReferenceNumber", dt.Rows[i]["adjustmentReferenceNumber"].ToString());
                SqlParameter q2 = new SqlParameter("@billOfEntryNumber", dt.Rows[i]["billOfEntryNumber"].ToString());
                SqlParameter q3 = new SqlParameter("@portOfDischarge", dt.Rows[i]["portOfDischarge"].ToString());
                SqlParameter q4 = new SqlParameter("@documentNumber", dt.Rows[i]["documentNumber"].ToString());
                dt3 = objData.getData(GetInvoiceDeatils, q1, q2, q3, q4);

                if (dt3.Rows.Count > 0)
                {
                    sw.WriteLine("<invoices>");
                    for (int j = 0; j < dt3.Rows.Count; j++)
                    {
                        sw.WriteLine("<invoice>");
                        sw.WriteLine("<invoiceSerialNo>" + dt3.Rows[j]["invoiceSerialNo"].ToString() + "</invoiceSerialNo>");
                        sw.WriteLine("<invoiceNo>" + dt3.Rows[j]["invoiceNo"].ToString() + "</invoiceNo>");
                        sw.WriteLine("<adjustedInvoiceValueIC>" + dt3.Rows[j]["adjustedValue"].ToString() + "</adjustedInvoiceValueIC>");
                        sw.WriteLine("</invoice>");

                        string query5 = "TF_Payment_BOE_Closure_Created";

                        SqlParameter t1 = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
                        t1.Value = dt.Rows[i]["portOfDischarge"].ToString();
                        SqlParameter t2 = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
                        t2.Value = dt.Rows[i]["billOfEntryNumber"].ToString();
                        SqlParameter t3 = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
                        t3.Value = dt.Rows[i]["billOfEntryDate"].ToString();
                        SqlParameter t4 = new SqlParameter("@IECode", SqlDbType.VarChar);
                        t4.Value = dt.Rows[i]["IECODE"].ToString();
                        SqlParameter t5 = new SqlParameter("@ADCode", SqlDbType.VarChar);
                        t5.Value = dt.Rows[i]["ADCODE"].ToString();
                        SqlParameter t6 = new SqlParameter("@adjustmentReferenceNumber", SqlDbType.VarChar);
                        t6.Value = dt.Rows[i]["adjustmentReferenceNumber"].ToString();
                        SqlParameter t7 = new SqlParameter("@closeofBillIndicator", SqlDbType.VarChar);
                        t7.Value = dt.Rows[i]["closeofBillIndicator"].ToString();
                        SqlParameter t8 = new SqlParameter("@adjustmentDate", SqlDbType.VarChar);
                        t8.Value = dt.Rows[i]["adjustmentDate"].ToString();
                        SqlParameter t9 = new SqlParameter("@adjustmentIndicator", SqlDbType.VarChar);
                        t9.Value = dt.Rows[i]["adjustmentIndicator"].ToString();
                        SqlParameter t10 = new SqlParameter("@documentNumber", SqlDbType.VarChar);
                        t10.Value = dt.Rows[i]["documentNumber"].ToString();
                        SqlParameter t11 = new SqlParameter("@documentDate", SqlDbType.VarChar);
                        t11.Value = dt.Rows[i]["documentDate"].ToString();
                        SqlParameter t12 = new SqlParameter("@documentPort", SqlDbType.VarChar);
                        t12.Value = dt.Rows[i]["documentPort"].ToString();
                        SqlParameter t13 = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                        t13.Value = dt3.Rows[j]["invoiceSerialNo"].ToString();
                        SqlParameter t14 = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                        t14.Value = dt3.Rows[j]["invoiceNo"].ToString();
                        SqlParameter t15 = new SqlParameter("@adjustedValue", SqlDbType.VarChar);
                        t15.Value = dt3.Rows[j]["adjustedValue"].ToString();
                        SqlParameter t16 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                        t16.Value = Session["userName"].ToString().Trim();
                        SqlParameter t17 = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                        t17.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        SqlParameter t18 = new SqlParameter("@FileName", SqlDbType.VarChar);
                        t18.Value = filename1;
                        SqlParameter t19 = new SqlParameter("@AddedAdCode", SqlDbType.VarChar);
                        t19.Value = Session["userADCode"].ToString().Trim();

                        string result = objData.SaveDeleteData(query5, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19);
                    }
                }
                sw.WriteLine("</invoices>");
                sw.WriteLine("</billOfEntry>");

            }
            sw.WriteLine("</billOfEntries>");
            sw.WriteLine("</bank>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            lblmessage.Font.Bold = true;
            string path = "file://" + _serverName + "/TF_GeneratedFiles/IDPMS";
            string link = "/TF_GeneratedFiles/IDPMS";
            lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
        {
            lblmessage.Text = "No Records";
        }

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
        p1.Value = ddlBranch.SelectedValue.ToString().Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        //string query1 = "IDPMS_PaymentSettlement";
        //DataTable dt2 = objData.getData(query1, p1, p2, p3);


        string Date = System.DateTime.Now.ToString("ddMMyyyy");
        string a = ddlBranch.Text;
        string _qurey = "TF_IDPMS_BOE_Closure_Cancel_XML_Generation";
        DataTable dt = objData.getData(_qurey, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            //string _directoryPath = "EDPMS";
            string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string query = "TF_IDPMS_GenerateFileName_BOE_Closure_Cancel";
            DataTable dtfile = objData.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string Adcode = ddlBranch.SelectedValue.ToString();

            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{

            //    noorms = noorms + 1;
            //}


            string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".bea.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

            sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfBOE>" + dt.Rows[0]["NoOfBill"].ToString() + "</noOfBOE>");
            sw.WriteLine("<noOfInvoices>" + dt.Rows[0]["noofinvoices"].ToString() + "</noOfInvoices>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<billOfEntries>");



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("<billOfEntry>");
                sw.WriteLine("<portOfDischarge>" + dt.Rows[i]["portOfDischarge"].ToString() + "</portOfDischarge>");
                sw.WriteLine("<billOfEntryNumber>" + dt.Rows[i]["billOfEntryNumber"].ToString() + "</billOfEntryNumber>");
                sw.WriteLine("<billOfEntryDate>" + dt.Rows[i]["billOfEntryDate"].ToString() + "</billOfEntryDate>");
                sw.WriteLine("<IECode>" + dt.Rows[i]["IECODE"].ToString() + "</IECode>");
                sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Record_Inc"].ToString() + "</recordIndicator>");
                sw.WriteLine("<adjustmentReferenceNumber>" + dt.Rows[i]["adjustmentReferenceNumber"].ToString() + "</adjustmentReferenceNumber>");
                sw.WriteLine("<closeofBillIndicator>" + dt.Rows[i]["closeofBillIndicator"].ToString() + "</closeofBillIndicator>");
                sw.WriteLine("<adjustmentDate>" + dt.Rows[i]["adjustmentDate"].ToString() + "</adjustmentDate>");
                sw.WriteLine("<adjustmentIndicator>" + dt.Rows[i]["adjustmentIndicator"].ToString() + "</adjustmentIndicator>");
                sw.WriteLine("<documentNumber>" + dt.Rows[i]["documentNumber"].ToString() + "</documentNumber>");
                sw.WriteLine("<documentDate>" + dt.Rows[i]["documentDate"].ToString() + "</documentDate>");
                sw.WriteLine("<documentPort>" + dt.Rows[i]["documentPort"].ToString() + "</documentPort>");
                sw.WriteLine("<approvedBy>" + dt.Rows[i]["approvedBy"].ToString() + "</approvedBy>");
                sw.WriteLine("<letterNumber >" + dt.Rows[i]["letterNumber"].ToString() + "</letterNumber >");
                sw.WriteLine("<letterDate >" + dt.Rows[i]["letterDate"].ToString() + "</letterDate >");
                sw.WriteLine("<Remark >" + dt.Rows[i]["Remark"].ToString() + "</Remark >");



                DataTable dt3 = new DataTable();
                string GetInvoiceDeatils = "TF_IDPMS_get_Invoice_Dateils_BOE_Closure_Cancel";
                SqlParameter q1 = new SqlParameter("@adjustmentReferenceNumber", dt.Rows[i]["adjustmentReferenceNumber"].ToString());
                SqlParameter q2 = new SqlParameter("@billOfEntryNumber", dt.Rows[i]["billOfEntryNumber"].ToString());
                SqlParameter q3 = new SqlParameter("@portOfDischarge", dt.Rows[i]["portOfDischarge"].ToString());
                SqlParameter q4 = new SqlParameter("@documentNumber", dt.Rows[i]["documentNumber"].ToString());
                dt3 = objData.getData(GetInvoiceDeatils, q1, q2, q3, q4);

                if (dt3.Rows.Count > 0)
                {
                    sw.WriteLine("<invoices>");
                    for (int j = 0; j < dt3.Rows.Count; j++)
                    {
                        sw.WriteLine("<invoice>");
                        sw.WriteLine("<invoiceSerialNo>" + dt3.Rows[j]["invoiceSerialNo"].ToString() + "</invoiceSerialNo>");
                        sw.WriteLine("<invoiceNo>" + dt3.Rows[j]["invoiceNo"].ToString() + "</invoiceNo>");
                        sw.WriteLine("<adjustedInvoiceValueIC>" + dt3.Rows[j]["adjustedInvoiceValueIC"].ToString() + "</adjustedInvoiceValueIC>");
                        sw.WriteLine("</invoice>");

                        string query5 = "TF_Payment_BOE_Closure_Cancel_Created";

                        SqlParameter t1 = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
                        t1.Value = dt.Rows[i]["portOfDischarge"].ToString();
                        SqlParameter t2 = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
                        t2.Value = dt.Rows[i]["billOfEntryNumber"].ToString();
                        SqlParameter t3 = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
                        t3.Value = dt.Rows[i]["billOfEntryDate"].ToString();
                        SqlParameter t4 = new SqlParameter("@IECode", SqlDbType.VarChar);
                        t4.Value = dt.Rows[i]["IECODE"].ToString();
                        SqlParameter t5 = new SqlParameter("@ADCode", SqlDbType.VarChar);
                        t5.Value = dt.Rows[i]["ADCODE"].ToString();
                        SqlParameter t6 = new SqlParameter("@adjustmentReferenceNumber", SqlDbType.VarChar);
                        t6.Value = dt.Rows[i]["adjustmentReferenceNumber"].ToString();
                        SqlParameter t7 = new SqlParameter("@closeofBillIndicator", SqlDbType.VarChar);
                        t7.Value = dt.Rows[i]["closeofBillIndicator"].ToString();
                        SqlParameter t8 = new SqlParameter("@adjustmentDate", SqlDbType.VarChar);
                        t8.Value = dt.Rows[i]["adjustmentDate"].ToString();
                        SqlParameter t9 = new SqlParameter("@adjustmentIndicator", SqlDbType.VarChar);
                        t9.Value = dt.Rows[i]["adjustmentIndicator"].ToString();
                        SqlParameter t10 = new SqlParameter("@documentNumber", SqlDbType.VarChar);
                        t10.Value = dt.Rows[i]["documentNumber"].ToString();
                        SqlParameter t11 = new SqlParameter("@documentDate", SqlDbType.VarChar);
                        t11.Value = dt.Rows[i]["documentDate"].ToString();
                        SqlParameter t12 = new SqlParameter("@documentPort", SqlDbType.VarChar);
                        t12.Value = dt.Rows[i]["documentPort"].ToString();
                        SqlParameter t13 = new SqlParameter("@invoiceSerialNo", SqlDbType.VarChar);
                        t13.Value = dt3.Rows[j]["invoiceSerialNo"].ToString();
                        SqlParameter t14 = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                        t14.Value = dt3.Rows[j]["invoiceNo"].ToString();
                        SqlParameter t15 = new SqlParameter("@adjustedValue", SqlDbType.VarChar);
                        t15.Value = dt3.Rows[j]["adjustedInvoiceValueIC"].ToString();
                        SqlParameter t16 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                        t16.Value = Session["userName"].ToString().Trim();
                        SqlParameter t17 = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                        t17.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        SqlParameter t18 = new SqlParameter("@FileName", SqlDbType.VarChar);
                        t18.Value = filename1;
                        SqlParameter t19 = new SqlParameter("@AddedAdCode", SqlDbType.VarChar);
                        t19.Value = Session["userADCode"].ToString().Trim();

                        string result = objData.SaveDeleteData(query5, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19);
                    }
                }
                sw.WriteLine("</invoices>");
                sw.WriteLine("</billOfEntry>");

            }
            sw.WriteLine("</billOfEntries>");
            sw.WriteLine("</bank>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            lblmessage.Font.Bold = true;
            string path = "file://" + _serverName + "/TF_GeneratedFiles/IDPMS";
            string link = "/TF_GeneratedFiles/IDPMS";
            lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
        {
            lblmessage.Text = "No Records";
        }
    }
}