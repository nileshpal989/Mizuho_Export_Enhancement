using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class IDPMS_TF_IDPMS_Out_Remittance_FileCreation : System.Web.UI.Page
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
        p1.Value = ddlBranch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        SqlParameter p4 = new SqlParameter("@Type", SqlDbType.VarChar);

        if(rdbOtt.Checked==true)
        {
            p4.Value = "OTT";
        }
        if (rdbOther.Checked == true)
        {
            p4.Value = "OTHER";
        }


            //string query1 = "TF_EDPMS_Count_Shipping_bill";
            //DataTable dt2 = objData.getData(query1, p1, p2, p3);

            string Date = System.DateTime.Now.ToString("ddMMyyyy");
            string a = ddlBranch.Text;
            string _qurey = "TF_IDPMS_Generate_XML_File_Out_Rem";
            DataTable dt = objData.getData(_qurey, p1,p2, p3,p4);
            if (dt.Rows.Count > 0)
            {
                //string _directoryPath = "EDPMS";
                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                string query = "TF_IDPMS_GenerateFileName_Out_Rem";
                DataTable dtfile = objData.getData(query);

                string Adcode = ddlBranch.SelectedValue.ToString();

                string filename1 = dtfile.Rows[0]["FileName"].ToString();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    noorms = noorms + 1;
                }


                string _filePath = _directoryPath + "/" + Adcode + "_" +filename1 + ".orm.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfORM>" + noorms + "</noOfORM>");
                sw.WriteLine("</checkSum>");
                sw.WriteLine("<outwardReferences>");



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine("<outwardReference>");
                    sw.WriteLine("<outwardReferenceNumber>" + dt.Rows[i]["ORMNo"].ToString() + "</outwardReferenceNumber>");
                    sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
                    sw.WriteLine("<Amount>" + dt.Rows[i]["Amount"].ToString() + "</Amount>");
                    sw.WriteLine("<currencyCode>" + dt.Rows[i]["Currency"].ToString() + "</currencyCode>");
                    sw.WriteLine("<paymentDate>" + dt.Rows[i]["Payment_Date"].ToString() + "</paymentDate>");
                    sw.WriteLine("<IECode>" + dt.Rows[i]["CUST_IE_CODE"].ToString() + "</IECode>");

                    sw.WriteLine("<IEName>" + dt.Rows[i]["CUST_NAME"].ToString() + "</IEName>");
                    sw.WriteLine("<IEAddress>" + dt.Rows[i]["Address"].ToString() + "</IEAddress>");
                    sw.WriteLine("<IEPANNumber>" + dt.Rows[i]["CUST_PAN_NO"].ToString() + "</IEPANNumber>");
                    sw.WriteLine("<isCapitalGoods>" + dt.Rows[i]["CG"].ToString() + "</isCapitalGoods>");
                    sw.WriteLine("<beneficiaryName>" + dt.Rows[i]["Ben_Name"].ToString() + "</beneficiaryName>");
                    sw.WriteLine("<beneficiaryAccountNumber>" + dt.Rows[i]["Ben_accno"].ToString() + "</beneficiaryAccountNumber>");
                    sw.WriteLine("<beneficiaryCountry>" + dt.Rows[i]["Ben_country"].ToString() + "</beneficiaryCountry>");

                    sw.WriteLine("<SWIFT>" + dt.Rows[i]["Swift_code"].ToString() + "</SWIFT>");
                    sw.WriteLine("<purposeCode>" + dt.Rows[i]["Purpose_code"].ToString() + "</purposeCode>");
                    sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Record_indicator"].ToString() + "</recordIndicator>");
                    sw.WriteLine("<remarks>" + dt.Rows[i]["Remarks"].ToString() + "</remarks>");
                    sw.WriteLine("<paymentTerms>" + dt.Rows[i]["Payment_terms"].ToString() + "</paymentTerms>");





                    sw.WriteLine("</outwardReference>");

                    string query5 = "TF_IDPMS_Insert_Out_Rem_Data_Created";

                  


                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                    adcode.Value = dt.Rows[i]["ADCODE"].ToString();
                    
                    SqlParameter Out_Rem_No = new SqlParameter("@Out_Rem_No", SqlDbType.VarChar);
                    Out_Rem_No.Value = dt.Rows[i]["ORMNo"].ToString();
                    SqlParameter Payment_Date = new SqlParameter("@Payment_Date", SqlDbType.VarChar);
                    Payment_Date.Value = dt.Rows[i]["Payment_Date"].ToString();
                    SqlParameter filename = new SqlParameter("@filename", SqlDbType.VarChar);
                    filename.Value = filename1;
                    SqlParameter addedby = new SqlParameter("@addedby", SqlDbType.VarChar);
                    addedby.Value = Session["userName"].ToString();
                    SqlParameter addedtime = new SqlParameter("@addedtime", SqlDbType.VarChar);
                    addedtime.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");



                    SqlParameter Purpose_Code = new SqlParameter("@Purpose_Code", SqlDbType.VarChar);
                    Purpose_Code.Value = dt.Rows[i]["Purpose_Code"].ToString();
                    SqlParameter IECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                    IECode.Value = dt.Rows[i]["CUST_IE_CODE"].ToString();


                    objData.SaveDeleteData(query5, adcode, Out_Rem_No, Payment_Date, filename, addedby, addedtime, Purpose_Code, IECode);

                }
                sw.WriteLine("</outwardReferences>");
                sw.WriteLine("</bank>");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();
                lblmessage.Font.Bold = true;
                string path = "file://" + _serverName + "/TF_GeneratedFiles/IDPMS";
                string link = "/TF_GeneratedFiles/IDPMS/";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
            else
            {
                lblmessage.Text = "No Records";
            }
        
    }
}