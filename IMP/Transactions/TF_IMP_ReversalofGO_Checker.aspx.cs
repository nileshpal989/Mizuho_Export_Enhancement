using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML.Excel;

public partial class IMP_Transactions_TF_IMP_ReversalofGO_Checker : System.Web.UI.Page
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
                        txtDocNo.Text = Request.QueryString["Delivery_Order_No"].Trim();
                        txtAppl_Name.Text = Request.QueryString["Applicant_Name"].Trim();
                        txtBrodate1.Text = Request.QueryString["BRO_Date"].Trim();
                        txtBill_Amt.Text = Request.QueryString["Bill_Amt"].Trim();
                        txtccy.Text = Request.QueryString["Currency"].Trim();
                        fillDetails();
                        MakeReadOnly();
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
    public void RevOfGOFileCreation()
    {
        string brono = Request.QueryString["Delivery_Order_No"].ToString();
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", Request.QueryString["Delivery_Order_No"].ToString());
        DataTable dt = objData1.getData("TF_IMP_ReversalOfGO1_ExcelFile_ForAcceptance", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/BRO/ReversalOfGO/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + brono + "_RevGO" + ".xlsx";
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
            }
        }
        else
        {
        }
    }
    public void fillDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Deliverorderno", Request.QueryString["Delivery_Order_No"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_RevOfGO_ViewDetails", PDocNo);
        if (dt.Rows.Count > 0)
        {
            txt_GO1_Comment.Text = dt.Rows[0]["GO_Comment"].ToString();
            txt_GO1_SectionNo.Text = dt.Rows[0]["GO_SectionNo"].ToString();
            txt_GO1_Remarks.Text = dt.Rows[0]["GO_Remark"].ToString();
            txt_GO1_Memo.Text = dt.Rows[0]["GO_Memo"].ToString();
            txt_GO1_Scheme_no.Text = dt.Rows[0]["GO_SchemeNo"].ToString();

            txt_GO1_Debit_Code.Text = dt.Rows[0]["GO_Debit/Credit_Code1"].ToString();
            txt_GO1_Debit_Curr.Text = dt.Rows[0]["GO_Debit/Credit_CCY2"].ToString();
            txt_GO1_Debit_Amt.Text = dt.Rows[0]["GO_Debit/Credit_Amt2"].ToString();
            txt_GO1_Debit_Cust.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Code2"].ToString();
            txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode2"].ToString();
            txt_GO1_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccNo2"].ToString();
            txt_GO1_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Debit/Credit_ExchRate2"].ToString();
            txt_GO1_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Debit/Credit_ExchCCY2"].ToString();
            txt_GO1_Debit_FUND.Text = dt.Rows[0]["GO_Debit/Credit_Fund2"].ToString();
            txt_GO1_Debit_Check_No.Text = dt.Rows[0]["GO_Debit/Credit_CheckNo2"].ToString();
            txt_GO1_Debit_Available.Text = dt.Rows[0]["GO_Debit/Credit_Available2"].ToString();
            txt_GO1_Debit_AdPrint.Text = dt.Rows[0]["GO_Debit/Credit_Advice_Print2"].ToString();
            txt_GO1_Debit_Details.Text = dt.Rows[0]["GO_Debit/Credit_Details2"].ToString();
            txt_GO1_Debit_Entity.Text = dt.Rows[0]["GO_Debit/Credit_Entity2"].ToString();
            txt_GO1_Debit_Division.Text = dt.Rows[0]["GO_Debit/Credit_Division2"].ToString();
            txt_GO1_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Debit/Credit_InterAmt2"].ToString();
            txt_GO1_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Debit/Credit_InterRate2"].ToString();

            txt_GO1_Credit_Code.Text = dt.Rows[0]["GO_Debit/Credit_Code2"].ToString();
            txt_GO1_Credit_Curr.Text = dt.Rows[0]["GO_Debit/Credit_CCY1"].ToString();
            txt_GO1_Credit_Amt.Text = dt.Rows[0]["GO_Debit/Credit_Amt1"].ToString();
            txt_GO1_Credit_Cust.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Code1"].ToString();
            txt_GO1_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode1"].ToString();
            txt_GO1_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccNo1"].ToString();
            txt_GO1_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Debit/Credit_ExchRate1"].ToString();
            txt_GO1_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Debit/Credit_ExchCCY1"].ToString();
            txt_GO1_Credit_FUND.Text = dt.Rows[0]["GO_Debit/Credit_Fund1"].ToString();
            txt_GO1_Credit_Check_No.Text = dt.Rows[0]["GO_Debit/Credit_CheckNo1"].ToString();
            txt_GO1_Credit_Available.Text = dt.Rows[0]["GO_Debit/Credit_Available1"].ToString();
            txt_GO1_Credit_AdPrint.Text = dt.Rows[0]["GO_Debit/Credit_Advice_Print1"].ToString();
            txt_GO1_Credit_Details.Text = dt.Rows[0]["GO_Debit/Credit_Details1"].ToString();
            txt_GO1_Credit_Entity.Text = dt.Rows[0]["GO_Debit/Credit_Entity1"].ToString();
            txt_GO1_Credit_Division.Text = dt.Rows[0]["GO_Debit/Credit_Division1"].ToString();
            txt_GO1_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Debit/Credit_InterAmt1"].ToString();
            txt_GO1_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Debit/Credit_InterRate1"].ToString();
        }
        else { }
    }
    public void MakeReadOnly()
    {
       
        txt_GO1_Comment.Enabled = false;
        txt_GO1_SectionNo.Enabled = false;
        txt_GO1_Remarks.Enabled = false;
        txt_GO1_Memo.Enabled = false;
        txt_GO1_Scheme_no.Enabled = false;
       
        txt_GO1_Credit_AdPrint.Enabled = false;
        txt_GO1_Credit_Amt.Enabled = false;
        txt_GO1_Credit_Available.Enabled = false;
        txt_GO1_Credit_Check_No.Enabled = false;
        txt_GO1_Credit_Code.Enabled = false;
        txt_GO1_Credit_Curr.Enabled = false;
        txt_GO1_Credit_Cust.Enabled = false;
        txt_GO1_Credit_Cust_AccNo.Enabled = false;
        txt_GO1_Credit_Cust_AcCode.Enabled = false;
        txt_GO1_Credit_Details.Enabled = false;
        txt_GO1_Credit_Division.Enabled = false;
        txt_GO1_Credit_Entity.Enabled = false;
        txt_GO1_Credit_Exch_Curr.Enabled = false;
        txt_GO1_Credit_Exch_Rate.Enabled = false;
        txt_GO1_Credit_FUND.Enabled = false;
        txt_GO1_Credit_Inter_Amount.Enabled = false;
        txt_GO1_Credit_Inter_Rate.Enabled = false;
        txt_GO1_Debit_AdPrint.Enabled = false;
        txt_GO1_Debit_Amt.Enabled = false;
        txt_GO1_Debit_Available.Enabled = false;
        txt_GO1_Debit_Check_No.Enabled = false;
        txt_GO1_Debit_Curr.Enabled = false;
        txt_GO1_Debit_Code.Enabled = false;
        txt_GO1_Debit_Cust.Enabled = false;
        txt_GO1_Debit_Cust_AccNo.Enabled = false;
        txt_GO1_Debit_Cust_AcCode.Enabled = false;
        txt_GO1_Debit_Details.Enabled = false;
        txt_GO1_Debit_Division.Enabled = false;
        txt_GO1_Debit_Entity.Enabled = false;
        txt_GO1_Debit_Exch_CCY.Enabled = false;
        txt_GO1_Debit_Exch_Rate.Enabled = false;
        txt_GO1_Debit_FUND.Enabled = false;
        txt_GO1_Debit_Inter_Amount.Enabled = false;
        txt_GO1_Debit_Inter_Rate.Enabled = false;
    }
    public void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Deliverorderno", Request.QueryString["Delivery_Order_No"].ToString());
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            RevOfGOFileCreation();
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveReject_RevOfGO", PDocNo, P_Status, P_RejectReason);

        Response.Redirect("TF_IMP_ReversalofGO_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
}