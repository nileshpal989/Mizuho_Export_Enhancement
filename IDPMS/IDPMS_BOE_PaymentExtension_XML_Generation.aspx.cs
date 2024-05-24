using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class IDPMS_IDPMS_BOE_PaymentExtension_XML_Generation : System.Web.UI.Page
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
        p1.Value = ddlBranch.SelectedValue.ToString().Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();



        //string query1 = "TF_EDPMS_Count_Shipping_bill";
        //DataTable dt2 = objData.getData(query1, p1, p2, p3);

        string Date = System.DateTime.Now.ToString("ddMMyyyy");
        string a = ddlBranch.Text;
        string _qurey = "TF_IDPMS_Generate_XML_File_Pay_ext";
        DataTable dt = objData.getData(_qurey, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            //string _directoryPath = "EDPMS";
            string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string query = "TF_IDPMS_GenerateFileName_Pay_Ext";
            DataTable dtfile = objData.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string Adcode = ddlBranch.SelectedValue.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                noorms = noorms + 1;
            }


            string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".bee.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

            sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfBOE>" + noorms + "</noOfBOE>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<billOfEntries>");



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("<billOfEntry>");
                sw.WriteLine("<portOfDischarge>" + dt.Rows[i]["portOfDischarge"].ToString() + "</portOfDischarge>");
                sw.WriteLine("<billOfEntryNumber>" + dt.Rows[i]["billOfEntryNumber"].ToString() + "</billOfEntryNumber>");
                sw.WriteLine("<billOfEntryDate>" + dt.Rows[i]["billOfEntryDate"].ToString() + "</billOfEntryDate>");
                //sw.WriteLine("<IECode>>" + dt.Rows[i]["IECode"].ToString() + "</IECode>>");
                ////sw.WriteLine("<paymentDate>" + dt.Rows[i]["Payment_Date"].ToString() + "</paymentDate>");
                sw.WriteLine("<IECode>" + dt.Rows[i]["IECode"].ToString() + "</IECode>");

                sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCode"].ToString() + "</ADCode>");
                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["recordIndicator"].ToString() + "</recordIndicator>");
                sw.WriteLine("<extenstionBy>" + dt.Rows[i]["extenstionBy"].ToString() + "</extenstionBy>");
                sw.WriteLine("<letterNumber>" + dt.Rows[i]["letterNumber"].ToString() + "</letterNumber>");
                sw.WriteLine("<letterDate>" + dt.Rows[i]["letterDate"].ToString() + "</letterDate>");
                sw.WriteLine("<extensionDate>" + dt.Rows[i]["extensionDate"].ToString() + "</extensionDate>");
                sw.WriteLine("<remarks>" + dt.Rows[i]["remarks"].ToString() + "</remarks>");

                //sw.WriteLine("<SWIFT>" + dt.Rows[i]["Swift_code"].ToString() + "</SWIFT>");
                //sw.WriteLine("<purposeCode>" + dt.Rows[i]["Purpose_code"].ToString() + "</purposeCode>");
                //sw.WriteLine("<recordIndicator>" + dt.Rows[i]["Record_indicator"].ToString() + "</recordIndicator>");
                //sw.WriteLine("<remarks>" + dt.Rows[i]["Remarks"].ToString() + "</remarks>");
                //sw.WriteLine("<paymentTerms>" + dt.Rows[i]["Payment_terms"].ToString() + "</paymentTerms>");
                
                sw.WriteLine("</billOfEntry>");

                string query5 = "TF_IDPMS_Insert_Pay_Ext_Data_Created";

                SqlParameter adcode = new SqlParameter("@ADCode", SqlDbType.VarChar);
                adcode.Value = dt.Rows[i]["ADCode"].ToString();

                SqlParameter portOfDischarge = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
                portOfDischarge.Value = dt.Rows[i]["portOfDischarge"].ToString();

                SqlParameter billOfEntryNumber = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
                billOfEntryNumber.Value = dt.Rows[i]["billOfEntryNumber"].ToString();

                SqlParameter billOfEntryDate = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
                billOfEntryDate.Value = dt.Rows[i]["billOfEntryDate"].ToString();

                SqlParameter IECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                IECode.Value = dt.Rows[i]["IECode"].ToString();

                SqlParameter extenstionBy = new SqlParameter("@extenstionBy", SqlDbType.VarChar);
                extenstionBy.Value = dt.Rows[i]["extenstionBy"].ToString();

                SqlParameter letterNumber = new SqlParameter("@letterNumber", SqlDbType.VarChar);
                letterNumber.Value = dt.Rows[i]["letterNumber"].ToString();

                SqlParameter letterDate = new SqlParameter("@letterDate", SqlDbType.VarChar);
                letterDate.Value = dt.Rows[i]["letterDate"].ToString();

                SqlParameter extensionDate = new SqlParameter("@extensionDate", SqlDbType.VarChar);
                extensionDate.Value = dt.Rows[i]["extensionDate"].ToString();

                SqlParameter remarks = new SqlParameter("@remarks", SqlDbType.VarChar);
                remarks.Value = dt.Rows[i]["remarks"].ToString();

                SqlParameter filename = new SqlParameter("@filename", SqlDbType.VarChar);
                filename.Value = filename1;

                SqlParameter AddedBy = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                AddedBy.Value = Session["userName"].ToString();

                SqlParameter AddedDate = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                AddedDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                SqlParameter AddedAdCode = new SqlParameter("@AddedAdCode", SqlDbType.VarChar);
                AddedAdCode.Value = dt.Rows[i]["AddedAdCode"].ToString();

                objData.SaveDeleteData(query5, adcode, portOfDischarge, billOfEntryNumber, billOfEntryDate, IECode, extenstionBy, letterNumber, letterDate, extensionDate, remarks, filename, AddedBy, AddedDate, AddedAdCode);

            }
            sw.WriteLine("</billOfEntries>");
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
}