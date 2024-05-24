using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
using OfficeOpenXml;
using ClosedXML.Excel;

public partial class IMP_Transactions_TF_IMP_ReversalOfLiability_Checker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_ReversalofGO_Checker_View.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() == "edit")
                    {
                        txtRefNo.Text = Request.QueryString["DocNo"].Trim();
                        FillDetails();
                    }
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {

                        ddlApproveReject.Enabled = false;
                    }
                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    protected void FillDetails()
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtRefNo.Text);
        dt = obj.getData("TF_IMP_FillRevOfLibilityDetails", P_DocNo);
        if (dt.Rows.Count > 0)
        {
            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            txtCustomerAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            txtCCode.Text = dt.Rows[0]["CCode"].ToString();
            txtValueDate.Text = dt.Rows[0]["ValueDate"].ToString();
            txtExpiryDate.Text = dt.Rows[0]["ExpiryDate"].ToString();
            ddlAmendmentOption.Text = dt.Rows[0]["AmendmentOption"].ToString();
            txtAAmount.Text = dt.Rows[0]["AmenAmount"].ToString();
            txtACurrency.Text = dt.Rows[0]["AmenCurrency"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            txtApplicationNo.Text = dt.Rows[0]["ApplicationNo"].ToString();
            txtApplyingBranch.Text = dt.Rows[0]["ApplyingBranch"].ToString();
            txtAdvisingBank.Text = dt.Rows[0]["AdvisingBank"].ToString();
            ddlAttentionCode.Text = dt.Rows[0]["AttentionCode"].ToString();
            txtCDCCY1.Text = dt.Rows[0]["CDCCY1"].ToString();
            txtCDAmount1.Text = dt.Rows[0]["CDAmount1"].ToString();
            txtCDCCY2.Text = dt.Rows[0]["CDCCY2"].ToString();
            txtCDAmount2.Text = dt.Rows[0]["CDAmount2"].ToString();
            txtCDCCY3.Text = dt.Rows[0]["CDCCY3"].ToString();
            txtCDAmount3.Text = dt.Rows[0]["CDAmount3"].ToString();
            txtDCACShortName1.Text = dt.Rows[0]["DCACShortName1"].ToString();
            txtDCCustAbbr1.Text = dt.Rows[0]["DCCustAbbr1"].ToString();
            txtDCAccountNo1.Text = dt.Rows[0]["DCAccountNumber1"].ToString();
            txtDCCCY1.Text = dt.Rows[0]["DCCCY1"].ToString();
            txtDCAmount1.Text = dt.Rows[0]["DCAmount1"].ToString();
            txtDCACShortName2.Text = dt.Rows[0]["DCACShortName2"].ToString();
            txtDCCustAbbr2.Text = dt.Rows[0]["DCCustAbbr2"].ToString();
            txtDCAccountNo2.Text = dt.Rows[0]["DCAccountNumber2"].ToString();
            txtDCCCY2.Text = dt.Rows[0]["DCCCY2"].ToString();
            txtDCAmount2.Text = dt.Rows[0]["DCAmount2"].ToString();
            txtDCACShortName3.Text = dt.Rows[0]["DCACShortName3"].ToString();
            txtDCCustAbbr3.Text = dt.Rows[0]["DCCustAbbr3"].ToString();
            txtDCAccountNo3.Text = dt.Rows[0]["DCAccountNumber3"].ToString();
            txtDCCCY3.Text = dt.Rows[0]["DCCCY3"].ToString();
            txtDCAmount3.Text = dt.Rows[0]["DCAmount3"].ToString();
        }
    }
    public void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            GBaseFileCreation();
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveReject_RevOfLib", PDocNo, P_Status, P_RejectReason);

        Response.Redirect("TF_IMP_ReversalOfLiability_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@DocNo", SqlDbType.VarChar);
        PRefNo.Value = txtRefNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_RevOfLib", PRefNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/ReversalOfLiability/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtRefNo.Text.Trim() + "_GBase" + ".xlsx";
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt, "Worksheet");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                }
                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();
                string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
                string link = "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
                //lblFolderLink.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
        }
        else
        {
        }
    }
}