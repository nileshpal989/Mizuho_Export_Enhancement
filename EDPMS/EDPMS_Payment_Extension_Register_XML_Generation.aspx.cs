using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class EDPMS_EDPMS_Payment_Extension_Register_XML_Generation : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return Generate();");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtfromDate.Focus();

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
    protected void btnSave_Click(object sender, EventArgs e)                
    {
        int NoofshippingBill = 0;
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();


        string query1 = "EDPMS_Payment_Ext_count_Ship";
        DataTable dt2 = objData.getData(query1, p1, p2, p3);
       
        string Date = System.DateTime.Now.ToString("ddMMyyyy");
        string a = ddlBranch.Text + "XML";
        string _qurey = "EDPMS_Payment_Ext_Reg_XML_file_Gen";
        DataTable dt = objData.getData(_qurey, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {

             for (int j = 0; j < dt.Rows.Count; j++)
               {

                NoofshippingBill = NoofshippingBill + 1;
                }



            string _directoryPath = Server.MapPath("../GeneratedFiles/EDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string query = "EDPMS_Generate_file_Name_Payment_EXT";
            DataTable dtfile = objData.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();

            string _filePath = _directoryPath + "/" + filename1 + ".pen.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

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
                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Recordind"].ToString() + "</recordIndicator>");
                sw.WriteLine("<shippingBillNo>" + dt.Rows[i]["ShippingbillNo"].ToString() + "</shippingBillNo>");
                sw.WriteLine("<shippingBillDate>" + dt.Rows[i]["Shipping_Bill_Date"].ToString() + "</shippingBillDate>");
                sw.WriteLine("<formNo>" + dt.Rows[i]["FormNo"].ToString() + "</formNo>");
                sw.WriteLine("<LEODate>" + dt.Rows[i]["LEODate"].ToString() + "</LEODate>");
                sw.WriteLine("<adCode>" + dt.Rows[i]["ADcode"].ToString() + "</adCode>");
                sw.WriteLine("<IECode>" + dt.Rows[i]["CUST_IE_CODE"].ToString() + "</IECode>");
           
                sw.WriteLine("<realizationExtensionInd>" + dt.Rows[i]["ADBank"].ToString() + "</realizationExtensionInd>"); 
                sw.WriteLine("<letterNo>" + dt.Rows[i]["LetterNo"].ToString() + "</letterNo>");
                sw.WriteLine("<letterDate>" + dt.Rows[i]["LetterDate"].ToString() + "</letterDate>");
                sw.WriteLine("<extendedRealizationDate>" + dt.Rows[i]["ExtensionDate"].ToString() + "</extendedRealizationDate>");

                //sw.WriteLine("<buyerName>" + dt.Rows[i]["Buyer_Name"].ToString() + "</buyerName>");
                //sw.WriteLine("<buyerCountry>" + dt.Rows[i]["Buyer_Country"].ToString() + "</buyerCountry>");
                sw.WriteLine("</shippingBill>");

                string query5 = "TF_Edpms_insert_Paymentexp_Data_Created";

                string _shippingbill="";
                if (dt.Rows[i]["ShippingbillNo"].ToString() == "")
                {
                    _shippingbill = dt.Rows[i]["FormNo"].ToString();
                }
                else
                {
                    _shippingbill = dt.Rows[i]["ShippingbillNo"].ToString();
                }

                SqlParameter shipbillNo = new SqlParameter("@ShipbillNo", SqlDbType.VarChar);
                shipbillNo.Value = _shippingbill;

                SqlParameter shipbilldte = new SqlParameter("@shipBillDate", SqlDbType.VarChar);
                shipbilldte.Value = dt.Rows[i]["Shipping_Bill_Date"].ToString();

                SqlParameter letterno = new SqlParameter("@letterno", SqlDbType.VarChar);
                letterno.Value = dt.Rows[i]["LetterNo"].ToString();

                SqlParameter lettedte = new SqlParameter("@LetterDate", SqlDbType.VarChar);
                lettedte.Value = dt.Rows[i]["LetterDate"].ToString();

                SqlParameter extendate = new SqlParameter("@extensionDate", SqlDbType.VarChar);
                extendate.Value = dt.Rows[i]["ExtensionDate"].ToString();

                SqlParameter adbank = new SqlParameter("@adbank", SqlDbType.VarChar);
                adbank.Value = dt.Rows[i]["ADBank"].ToString();

                SqlParameter xmlstarttag = new SqlParameter("@Xmlstarttag", SqlDbType.VarChar);
                xmlstarttag.Value = dt.Rows[0]["XMLStartTag"].ToString();

                SqlParameter adcode = new SqlParameter("@adCode", SqlDbType.VarChar);
                adcode.Value = dt.Rows[i]["ADcode"].ToString();

                SqlParameter IECode = new SqlParameter("@IEcode", SqlDbType.VarChar);
                IECode.Value = dt.Rows[i]["CUST_IE_CODE"].ToString();

                SqlParameter portcode = new SqlParameter("@portCode", SqlDbType.VarChar);
                portcode.Value = dt.Rows[i]["PortCode"].ToString();

                SqlParameter recind = new SqlParameter("@Recordin", SqlDbType.VarChar);
                recind.Value = dt.Rows[i]["Recordind"].ToString();

                SqlParameter typexp = new SqlParameter("@TypeExp", SqlDbType.VarChar);
                typexp.Value = dt.Rows[i]["TypeofExport"].ToString();

                SqlParameter formno = new SqlParameter("@formno", SqlDbType.VarChar);
                formno.Value = dt.Rows[i]["FormNo"].ToString();

                SqlParameter filename = new SqlParameter("@Filename", SqlDbType.VarChar);
                filename.Value = filename1;

                SqlParameter addedby = new SqlParameter("@addedby", SqlDbType.VarChar);
                addedby.Value = Session["userName"].ToString();

                SqlParameter addedtime = new SqlParameter("@Addeddate", SqlDbType.VarChar);
                addedtime.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");


                objData.SaveDeleteData(query5, shipbillNo, shipbilldte, letterno, lettedte, extendate, adbank, xmlstarttag, adcode, IECode, portcode, recind, typexp, formno, filename, addedby, addedtime);

            }
            sw.WriteLine("</shippingBills>");
            sw.WriteLine("</bank>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            lblmessage.Font.Bold = true;
            string path = "file://" + _serverName + "/GeneratedFiles/EDPMS";
            string link = "/GeneratedFiles/EDPMS/";
            lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            
        }

        else
        {
            lblmessage.Text = "No Records";
            txtfromDate.Focus();
        }
    }
}