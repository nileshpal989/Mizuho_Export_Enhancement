using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class EDPMS_EDPMS_Generate_XMLFile_Receipt_Document : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return Generate();");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
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
        //    ddlBranch.Items.Insert(0, li);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int NoofshippingBill = 0;
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

        if (PageHeader.Text == "EDPMS XML (Receipt) File Generation")
        {

            string query1 = "TF_EDPMS_Count_Shipping_bill";
            DataTable dt2 = objData.getData(query1, p1, p2, p3);
            if (dt2.Rows.Count > 0)
            {
                // NoofshippingBill = dt2.Rows[0]["noofShippingBill"].ToString();
            }
            string Date = System.DateTime.Now.ToString("ddMMyyyy");
            string a = ddlBranch.Text;
            string _qurey = "TF_EDPMS_Generate_XML_File_Receipt_Document";
            DataTable dt = objData.getData(_qurey, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                //string _directoryPath = "EDPMS";
                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                string query = "TF_EDPMS_GenerateFileName_Receipt";
                DataTable dtfile = objData.getData(query);
                string filename1 = dtfile.Rows[0]["FileName"].ToString();

                string _filePath = _directoryPath + "/" + filename1 + ".rod.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    NoofshippingBill = NoofshippingBill + 1;

                }



                sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfShippingBills>" + NoofshippingBill + "</noOfShippingBills>");
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
                    sw.WriteLine("<LEODate>" + dt.Rows[i]["LEODate"].ToString() + "</LEODate>");
                    sw.WriteLine("<IECode>" + dt.Rows[i]["IECode"].ToString() + "</IECode>");
                    sw.WriteLine("<changedIECode/>");
                    sw.WriteLine("<adCode>" + dt.Rows[i]["ADCode"].ToString() + "</adCode>");
                    sw.WriteLine("<adExportAgency>" + dt.Rows[i]["ExportAgency"].ToString() + "</adExportAgency>");
                    sw.WriteLine("<directDispatchIndicator>" + dt.Rows[i]["Direct_Dispatch_Indicator"].ToString() + "</directDispatchIndicator>");
                    sw.WriteLine("<adBillNo>" + dt.Rows[i]["ADBillNo"].ToString() + "</adBillNo>");
                    sw.WriteLine("<dateOfNegotiation>" + dt.Rows[i]["Negotiation_Date"].ToString() + "</dateOfNegotiation>");
                    sw.WriteLine("<buyerName>" + dt.Rows[i]["Buyer_Name"].ToString() + "</buyerName>");
                    sw.WriteLine("<buyerCountry>" + dt.Rows[i]["Buyer_Country"].ToString() + "</buyerCountry>");
                    sw.WriteLine("</shippingBill>");

                    string shippingbill = "";
                    if (dt.Rows[i]["ShippingBillNo"].ToString() == "")
                    {
                        shippingbill = dt.Rows[i]["FormNo"].ToString();
                    }
                    else
                    {
                        shippingbill = dt.Rows[i]["ShippingBillNo"].ToString();
                    }

                    string query5 = "TF_EDPMS_Insert_Receipt_Data_Created";

                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                    adcode.Value = dt.Rows[i]["ADCode"].ToString();
                    SqlParameter shipbillno = new SqlParameter("@shipbill", SqlDbType.VarChar);
                    shipbillno.Value = shippingbill;
                    SqlParameter shipbilldate = new SqlParameter("@shipbilldate", SqlDbType.VarChar);
                    shipbilldate.Value = dt.Rows[i]["Shipping_Bill_Date"].ToString();
                    SqlParameter filename = new SqlParameter("@filename", SqlDbType.VarChar);
                    filename.Value = filename1;
                    SqlParameter addedby = new SqlParameter("@addedby", SqlDbType.VarChar);
                    addedby.Value = Session["userName"].ToString();
                    SqlParameter addedtime = new SqlParameter("@addedtime", SqlDbType.VarChar);
                    addedtime.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    objData.SaveDeleteData(query5, adcode, shipbillno, shipbilldate, filename, addedby, addedtime);

                }
                sw.WriteLine("</shippingBills>");
                sw.WriteLine("</bank>");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();
                lblmessage.Font.Bold = true;
                string path = "file://" + _serverName + "/TF_GeneratedFiles/EDPMS/" + a + "/";
                string link = "/TF_GeneratedFiles/EDPMS/" + a + "/" + filename1 + ".rod.xml";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
            else
            {
                lblmessage.Text = "No Records";
            }
        }
        else if (PageHeader.Text == "EDPMS XML (AD Transfer) File Generation")
        {
            string Date = System.DateTime.Now.ToString("ddMMyyyy");
            string a = ddlBranch.Text;
            int NoofshippingBill1 = 0;
            string _qurey = "TF_EDPMS_Generate_XML_File_AD_Transfer";
            DataTable dt = objData.getData(_qurey, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                string query = "TF_EDPMS_GenerateFileName_ADTransfer";
                DataTable dtfile = objData.getData(query);
                string filename1 = dtfile.Rows[0]["FileName"].ToString();

                string _filePath = _directoryPath + "/" + filename1 + ".trr.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    NoofshippingBill1 = NoofshippingBill1 + 1;

                }

                sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfShippingBills>" + NoofshippingBill1 + "</noOfShippingBills>");
                sw.WriteLine("</checkSum>");
                sw.WriteLine("<shippingBills>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine("<shippingBill>");
                    sw.WriteLine("<portCode>" + dt.Rows[i]["PortCode"].ToString() + "</portCode>");
                    sw.WriteLine("<exportAgency>" + dt.Rows[i]["Export_Agency"].ToString() + "</exportAgency>");
                    sw.WriteLine("<exportType>" + dt.Rows[i]["TypeofExport"].ToString() + "</exportType>");
                    sw.WriteLine("<shippingBillNo>" + dt.Rows[i]["Shipping_Bill_No"].ToString() + "</shippingBillNo>");
                    sw.WriteLine("<shippingBillDate>" + dt.Rows[i]["Shipping_Bill_Date"].ToString() + "</shippingBillDate>");
                    sw.WriteLine("<formNo>" + dt.Rows[i]["FormNo"].ToString() + "</formNo>");
                    sw.WriteLine("<IECode>" + dt.Rows[i]["IECode"].ToString() + "</IECode>");
                    sw.WriteLine("<existingAdCode>" + dt.Rows[i]["ExitingCode"].ToString() + "</existingAdCode>");
                    sw.WriteLine("<newAdCode>" + dt.Rows[i]["NewCode"].ToString() + "</newAdCode>");
                    sw.WriteLine("</shippingBill>");


                    string shippingbill = "";
                    if (dt.Rows[i]["Shipping_Bill_No"].ToString() == "")
                    {
                        shippingbill = dt.Rows[i]["FormNo"].ToString();
                    }
                    else
                    {
                        shippingbill = dt.Rows[i]["Shipping_Bill_No"].ToString();
                    }



                    string query5 = "TF_EDPMS_Insert_AD_Transfer_Data_Created";




                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                    adcode.Value = ddlBranch.Text;
                    SqlParameter shipbillno = new SqlParameter("@shipbill", SqlDbType.VarChar);
                    shipbillno.Value = shippingbill;
                    SqlParameter shipbilldate = new SqlParameter("@shipbilldate", SqlDbType.VarChar);
                    shipbilldate.Value = dt.Rows[i]["Shipping_Bill_Date"].ToString();
                    SqlParameter filename = new SqlParameter("@filename", SqlDbType.VarChar);
                    filename.Value = filename1;
                    SqlParameter addedby = new SqlParameter("@addedby", SqlDbType.VarChar);
                    addedby.Value = Session["userName"].ToString();
                    SqlParameter addedtime = new SqlParameter("@addedtime", SqlDbType.VarChar);
                    addedtime.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                  //  objData.SaveDeleteData(query5, adcode, shipbillno, shipbilldate, filename, addedby, addedtime);
                }
                sw.WriteLine("</shippingBills>");
                sw.WriteLine("</bank>");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();
                lblmessage.Font.Bold = true;
                string path = "file://" + _serverName + "/TF_GeneratedFiles/EDPMS";
                string link = "/TF_GeneratedFiles/EDPMS/";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }

            else
            {
                lblmessage.Text = "No Records";
            }

        }
    }
}