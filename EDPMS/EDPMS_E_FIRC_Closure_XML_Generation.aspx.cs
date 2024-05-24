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


public partial class EDPMS_EDPMS_E_FIRC_Closure_XML_Generation : System.Web.UI.Page
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
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p1.Value = txtfromDate.Text.ToString();

        SqlParameter p2 = new SqlParameter("@todate", SqlDbType.VarChar);
        p2.Value = txtToDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        p3.Value = ddlBranch.SelectedValue.ToString();

        string _query = "TF_EDPMS_E_FIRC_Closure_Xml_Generation";
        string a = ddlBranch.Text;
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string queryfilname = "TF_EDPMS_E_FIRC_Closure_Xml_FileName";
            DataTable dtfile = objData.getData(queryfilname);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string _filePath = _directoryPath + "/" + filename1 + ".frccls.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);


            sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfFIRC>" + dt.Rows[0]["noOfFIRC"].ToString() + "</noOfFIRC>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<fircNumbers>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // string FIRC_No = "", FIRC_Date = "", fircAmount = "", fundTransferFlag = "", remarks = "", remitterAddress = "", recordIndicator = "";

                sw.WriteLine("<fircClosure>");
                    sw.WriteLine("<fircNumber>" + dt.Rows[i]["fircNumber"].ToString() + "</fircNumber>");
                    sw.WriteLine("<adCode>" + dt.Rows[i]["adCode"].ToString() + "</adCode>");
                    sw.WriteLine("<closureCurrency>" + dt.Rows[i]["closureCurrency"].ToString() + "</closureCurrency>");
                    sw.WriteLine("<closureAmount>" + Convert.ToDecimal(dt.Rows[i]["closureAmount"].ToString()).ToString("0.00") + "</closureAmount>");
                    sw.WriteLine("<adjustmentDate>" + dt.Rows[i]["adjustmentDate"].ToString() + "</adjustmentDate>");
                    sw.WriteLine("<approvalBy>" + dt.Rows[i]["approvalBy"].ToString() + "</approvalBy>");
                    sw.WriteLine("<reasonForClosure>" + dt.Rows[i]["reasonForClosure"].ToString() + "</reasonForClosure>");
                    sw.WriteLine("<closureSequenceNo>" + dt.Rows[i]["closureSequenceNo"].ToString() + "</closureSequenceNo>");
                    sw.WriteLine("<recordIndicator>" + dt.Rows[i]["recordIndicator"].ToString() + "</recordIndicator>");
                    sw.WriteLine("<remarks>" + dt.Rows[i]["remarks"].ToString() + "</remarks>");
                sw.WriteLine("</fircClosure>");

                string _query1 = "TF_EDPMS_E_FIRC_Closure_Xml_Generate_Insert";
                SqlParameter filename = new SqlParameter("@FileName", SqlDbType.VarChar);
                filename.Value = filename1;
                SqlParameter fircNumber = new SqlParameter("@fircNumber", SqlDbType.VarChar);
                fircNumber.Value = dt.Rows[i]["fircNumber"].ToString();
                SqlParameter adCode = new SqlParameter("@adCode", SqlDbType.VarChar);
                adCode.Value = dt.Rows[i]["adCode"].ToString();
                SqlParameter closureCurrency = new SqlParameter("@closureCurrency", SqlDbType.VarChar);
                closureCurrency.Value = dt.Rows[i]["closureCurrency"].ToString();
                SqlParameter closureAmount = new SqlParameter("@closureAmount", SqlDbType.VarChar);
                closureAmount.Value = dt.Rows[i]["closureAmount"].ToString();
                SqlParameter adjustmentDate = new SqlParameter("@adjustmentDate", SqlDbType.VarChar);
                adjustmentDate.Value = dt.Rows[i]["adjustmentDate"].ToString();
                SqlParameter closureSequenceNo = new SqlParameter("@closureSequenceNo", SqlDbType.VarChar);
                closureSequenceNo.Value = dt.Rows[i]["closureSequenceNo"].ToString();
                SqlParameter recordIndicator = new SqlParameter("@recordIndicator", SqlDbType.VarChar);
                recordIndicator.Value = dt.Rows[i]["recordIndicator"].ToString();
                SqlParameter AddedBy = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                AddedBy.Value = Session["userName"].ToString();
                SqlParameter ieCode = new SqlParameter("@ieCode", dt.Rows[i]["IECode"].ToString());
                objData.SaveDeleteData(_query1, filename, fircNumber,ieCode, adCode, closureCurrency, closureAmount, adjustmentDate, closureSequenceNo, recordIndicator, AddedBy);

            }
            sw.WriteLine("</fircNumbers>");
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
