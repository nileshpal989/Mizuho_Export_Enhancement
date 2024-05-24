using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using System.Net;

public partial class EDPMS_EDPMS_GenerateXMLFile_INWRemittance : System.Web.UI.Page
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

                //ddlBranch.SelectedIndex = 1;

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
        int noOfIrm = 0;
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p1.Value = txtfromDate.Text.ToString();

        SqlParameter p2 = new SqlParameter("@todate", SqlDbType.VarChar);
        p2.Value = txtToDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@branchcode", SqlDbType.VarChar);
        p3.Value = ddlBranch.Text.ToString();

        string _query = "TF_IRMFileCreation";
        string a = ddlBranch.Text;
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string queryfilname = "EDPMS_INW_FileCreation_SrNo";
            DataTable dtfile = objData.getData(queryfilname);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string _filePath = _directoryPath + "/" + filename1 + ".irm.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                noOfIrm = noOfIrm + 1;
            }

            sw.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfIrm>" + noOfIrm + "</noOfIrm>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<remittances>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string FIRC_No = "", FIRC_Date = "", fircAmount = "", fundTransferFlag = "", remarks = "", remitterAddress = "", recordIndicator = "";

                sw.WriteLine("<remittance>");
                sw.WriteLine("<IRMNumber>" + dt.Rows[i]["IRMNo"].ToString() + "</IRMNumber>");
                sw.WriteLine("<remittanceAmount>" + Convert.ToDecimal(dt.Rows[i]["RemitterAmount"].ToString()).ToString("0.0000") + "</remittanceAmount>");
                sw.WriteLine("<remittanceDate>" + dt.Rows[i]["remitterdate"].ToString() + "</remittanceDate>");
                sw.WriteLine("<adCode>" + dt.Rows[i]["ADCode"].ToString() + "</adCode>");
                sw.WriteLine("<fircFlag>" + dt.Rows[i]["fircflag"].ToString() + "</fircFlag>");
                sw.WriteLine("<fircNumber>" + FIRC_No + "</fircNumber>");
                sw.WriteLine("<fircIssueDate>" + FIRC_Date + "</fircIssueDate>");
                sw.WriteLine("<fircAmount>" + fircAmount + "</fircAmount>");
                // This removed from this date  20/07/2016
                //sw.WriteLine("<fundTransferFlag>" + fundTransferFlag + "</fundTransferFlag>");
                //sw.WriteLine("<IFSCCode>" + dt.Rows[i]["IFSCCode"].ToString() + "</IFSCCode>");
                sw.WriteLine("<currency>" + dt.Rows[i]["Currency"].ToString() + "</currency>");
                sw.WriteLine("<ieCode>" + dt.Rows[i]["iecode"].ToString() + "</ieCode>");
                sw.WriteLine("<ieName>" + dt.Rows[i]["iename"].ToString() + "</ieName>");
                sw.WriteLine("<remitterName>" + dt.Rows[i]["Remittername"].ToString() + "</remitterName>");
                sw.WriteLine("<remitterAddress>" + remitterAddress + "</remitterAddress>");
                sw.WriteLine("<remitterCountry>" + dt.Rows[i]["country"].ToString() + "</remitterCountry>");
                sw.WriteLine("<remitterBankName>" + dt.Rows[i]["BankName"].ToString() + "</remitterBankName>");
                sw.WriteLine("<remitterBankCountry>" + dt.Rows[i]["country"].ToString() + "</remitterBankCountry>");
                sw.WriteLine("<swiftOtherBankRefNumber>" + dt.Rows[i]["swiftrefno"].ToString() + "</swiftOtherBankRefNumber>");
                sw.WriteLine("<purposeOfRemittance>" + dt.Rows[i]["Purpose_Code"].ToString() + "</purposeOfRemittance>");

                //if (dt.Rows[i]["RecordIndicator"].ToString() == "N")
                //{
                //    recordIndicator = "1";
                //}
                //if (dt.Rows[i]["RecordIndicator"].ToString() == "A")
                //{
                //    recordIndicator = "2";
                //}
                //else
                //{
                //    recordIndicator = "3";
                //}

                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["recordind"].ToString() + "</recordIndicator>");
                sw.WriteLine("<remarks>" + remarks + "</remarks>");
                sw.WriteLine("</remittance>");

                string _query1 = "EDPMS_Insert_InwardRemittance_FileCreation_XML";

                SqlParameter filename = new SqlParameter("@FileName", SqlDbType.VarChar);
                filename.Value = filename1;
                SqlParameter IRMNumber = new SqlParameter("@IRMNumber", SqlDbType.VarChar);
                IRMNumber.Value = dt.Rows[i]["IRMNo"].ToString();
                SqlParameter RemittanceAmount = new SqlParameter("@RemittanceAmount", SqlDbType.VarChar);
                RemittanceAmount.Value = dt.Rows[i]["RemitterAmount"].ToString();
                SqlParameter RemittanceDate = new SqlParameter("@RemittanceDate", SqlDbType.VarChar);
                RemittanceDate.Value = dt.Rows[i]["remitterdate"].ToString();
                SqlParameter ADCode = new SqlParameter("@ADCode", SqlDbType.VarChar);
                ADCode.Value = dt.Rows[i]["ADCode"].ToString();
                SqlParameter FircFlag = new SqlParameter("@FircFlag", SqlDbType.VarChar);
                FircFlag.Value = dt.Rows[i]["fircflag"].ToString();
                SqlParameter IFSCCode = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                IFSCCode.Value = dt.Rows[i]["ifsccode"].ToString();
                SqlParameter Currency = new SqlParameter("@Currency", SqlDbType.VarChar);
                Currency.Value = dt.Rows[i]["Currency"].ToString();
                SqlParameter IECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                IECode.Value = dt.Rows[i]["iecode"].ToString();
                SqlParameter IEName = new SqlParameter("@IEName", SqlDbType.VarChar);
                IEName.Value = dt.Rows[i]["iename"].ToString();
                SqlParameter RemitterName = new SqlParameter("@RemitterName", SqlDbType.VarChar);
                RemitterName.Value = dt.Rows[i]["Remittername"].ToString();
                SqlParameter RemitterCountry = new SqlParameter("@RemitterCountry", SqlDbType.VarChar);
                RemitterCountry.Value = dt.Rows[i]["country"].ToString();
                SqlParameter RemitterBankName = new SqlParameter("@RemitterBankName", SqlDbType.VarChar);
                RemitterBankName.Value = dt.Rows[i]["BankName"].ToString();
                SqlParameter RemitterBankCountry = new SqlParameter("@RemitterBankCountry", SqlDbType.VarChar);
                RemitterBankCountry.Value = dt.Rows[i]["country"].ToString();
                SqlParameter SwiftOtherBankRefNumber = new SqlParameter("@SwiftOtherBankRefNumber", SqlDbType.VarChar);
                SwiftOtherBankRefNumber.Value = dt.Rows[i]["swiftrefno"].ToString();
                SqlParameter PurposeOfRemittance = new SqlParameter("@PurposeOfRemittance", SqlDbType.VarChar);
                PurposeOfRemittance.Value = dt.Rows[i]["Purpose_Code"].ToString();
                SqlParameter RecordIndicator = new SqlParameter("@RecordIndicator", SqlDbType.VarChar);
                RecordIndicator.Value = dt.Rows[i]["recordind"].ToString();
                SqlParameter AddedBy = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                AddedBy.Value = Session["userName"].ToString();
                SqlParameter AddedDate = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                AddedDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                objData.SaveDeleteData(_query1, filename, IRMNumber, RemittanceAmount, RemittanceDate, ADCode, FircFlag, IFSCCode, Currency, IECode, IEName,
                    RemitterName, RemitterCountry, RemitterBankName, RemitterBankCountry, SwiftOtherBankRefNumber, PurposeOfRemittance, RecordIndicator, AddedBy, AddedDate);

            }
            sw.WriteLine("</remittances>");
            sw.WriteLine("</bank>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            lblmessage.Font.Bold = true;
            string path = "file://" + _serverName + "/TF_GeneratedFiles/EDPMS/" + a + "/";
            string link = "/TF_GeneratedFiles/EDPMS/" + a + "/" + filename1 + ".irm.xml";
            lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
        {
            lblmessage.Text = "No Records";
        }
    }
}