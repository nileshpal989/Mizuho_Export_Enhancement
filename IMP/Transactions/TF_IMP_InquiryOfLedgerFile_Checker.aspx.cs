using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.IO;
using ClosedXML.Excel;

public partial class IMP_Transactions_TF_IMP_InquiryOfLedgerFile_Checker : System.Web.UI.Page
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
                    FillMMRODetails();
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                    }
            }
            
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string AR = "";
            if (ddlApproveReject.SelectedIndex == 1)
            {
                AR = "A";
                    GBaseFileCreationMM();
                    GBaseFileCreationRO();
            }
            if (ddlApproveReject.SelectedIndex == 2)
            {
                AR = "R";
            }
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            TF_DATA obj = new TF_DATA();
            SqlParameter P_DocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter P_DueDate = new SqlParameter("@DueDate", hdnDueDate.Value.Trim());
            string Result = obj.SaveDeleteData("TF_IMP_ApproveRejectMMRO", P_DocNo, P_RejectReason, P_Status, P_DueDate);
            Response.Redirect("TF_IMP_InquiryOfLedgerFile_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_InquiryOfLedgerFile_Checker_View.aspx");
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbRollOver;
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbMoneyMarket;
    }
    protected void FillMMRODetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
        DataTable dt = obj.getData("TF_IMP_FillMMRODetails_Checker", P_DocNo);
        if (dt.Rows.Count > 0)
        {
            // Money Market
            //txtCustomerNameMK.Text = dt.Rows[0]["CustomerNameMK"].ToString();
            //txtAccountCodeMK.Text = dt.Rows[0]["AccountCodeMK"].ToString();
            //txtCurrencyMK.Text = dt.Rows[0]["CurrencyMK"].ToString();
            txtReferenceNumberMK.Text = dt.Rows[0]["Document_No"].ToString();
            //txtSystemCodeMK.Text = dt.Rows[0]["SystemCodeMK"].ToString();
            //txtFrontNoMK.Text = dt.Rows[0]["FrontNoMK"].ToString();
            //txtNosOfDaysMK.Text = dt.Rows[0]["NosOfDaysMK"].ToString();
            //txtInterestMK.Text = dt.Rows[0]["InterestMK"].ToString();
            txtFinalDueDateMK.Text = dt.Rows[0]["FinalDueDateMK"].ToString();
            hdnDueDate.Value = dt.Rows[0]["FinalDueDateMK"].ToString();
            //txtSpreadMK.Text = dt.Rows[0]["SpreadMK"].ToString();
            //txtBaseRateMK.Text = dt.Rows[0]["BaseRateMK"].ToString();
            //txtFundsMK.Text = dt.Rows[0]["FundsMK"].ToString();
            //txtSettlementMethodMK.Text = dt.Rows[0]["SettlementMethodMK"].ToString();
            //txtOurDipositoryMK.Text = dt.Rows[0]["OurDipositoryMK"].ToString();
            //txtCustomerAbbrMK.Text = dt.Rows[0]["CustomerAbbrMK"].ToString();
            //txtContraAccountMK.Text = dt.Rows[0]["ContraAccountMK"].ToString();
            //txtAccountNoMK.Text = dt.Rows[0]["AccountNoMK"].ToString();
            //txtTheirDipository1MK.Text = dt.Rows[0]["TheirDipository1MK"].ToString();
            //txtTheirDipository2MK.Text = dt.Rows[0]["TheirDipository2MK"].ToString();
            //txtTheirDipository3MK.Text = dt.Rows[0]["TheirDipository3MK"].ToString();
            //txtTheirDipository4MK.Text = dt.Rows[0]["TheirDipository4MK"].ToString();
            //txtTheirAccountMK.Text = dt.Rows[0]["TheirAccountMK"].ToString();
            //txtATTNMK.Text = dt.Rows[0]["ATTNMK"].ToString();
            //txtCurrentBalanceMK.Text = dt.Rows[0]["CurrentBalanceMK"].ToString();
            //txtValueDateMK.Text = dt.Rows[0]["ValueDateMK"].ToString();
            //txtOperationDateMK.Text = dt.Rows[0]["OperationDateMK"].ToString();
            //txtSettlement1MK.Text = dt.Rows[0]["Settlement1MK"].ToString();
            //txtSettlement2MK.Text = dt.Rows[0]["Settlement2MK"].ToString();
            //txtLastModificationMK.Text = dt.Rows[0]["LastModificationMK"].ToString();
            //txtRollOverNoMK.Text = dt.Rows[0]["RollOverNoMK"].ToString();
            //txtLastTRNSNoMK.Text = dt.Rows[0]["LastTRNSNoMK"].ToString();
            //txtLastOperationMK.Text = dt.Rows[0]["LastOperationMK"].ToString();
            //txtRemEUCMK.Text = dt.Rows[0]["RemEUCMK"].ToString();
            //txtStatusMK.Text = dt.Rows[0]["StatusMK"].ToString();
            //txtOverDueAccrueMK.Text = dt.Rows[0]["OverDueAccrueMK"].ToString();
            //txtEntityMK.Text = dt.Rows[0]["EntityMK"].ToString();

            //hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
            //// Roll Over
            //txtCustomerNameRO.Text = dt.Rows[0]["CustomerNameRO"].ToString();
            //txtAccountCodeRO.Text = dt.Rows[0]["AccountCodeRO"].ToString();
            //txtCurrencyRO.Text = dt.Rows[0]["CurrencyRO"].ToString();
            txtReferenceNoRO.Text = dt.Rows[0]["Document_No"].ToString();
            //txtDivisionCodeRO.Text = dt.Rows[0]["DivisionCodeRO"].ToString();
            //txtFrontNoRO.Text = dt.Rows[0]["FrontNoRO"].ToString();
            //txtPledgedRO.Text = dt.Rows[0]["PledgedRO"].ToString();
            //txtRollOverNoRO.Text = dt.Rows[0]["RollOverNoRO"].ToString();
            //txtOperationDateRO.Text = dt.Rows[0]["OperationDateRO"].ToString();
            //txtValueDateRO.Text = dt.Rows[0]["ValueDateRO"].ToString();
            txtDueDateRO.Text = dt.Rows[0]["DueDateRO"].ToString();
            //txtDaysRO.Text = dt.Rows[0]["DaysRO"].ToString();
            //txtAmountRO.Text = dt.Rows[0]["AmountRO"].ToString();
            //txtPrimeCCYRO.Text = dt.Rows[0]["PrimeCCYRO"].ToString();
            //txtInterestRate1RO.Text = dt.Rows[0]["InterestRate1RO"].ToString();
            //txtInterestRate2RO.Text = dt.Rows[0]["InterestRate2RO"].ToString();
            //txtInterestAmountRO.Text = dt.Rows[0]["InterestAmountRO"].ToString();
            //txtDaysAYearRO.Text = dt.Rows[0]["DaysAYearRO"].ToString();
            //txtBaseRateRO.Text = dt.Rows[0]["BaseRateRO"].ToString();
            //txtSpreadRO.Text = dt.Rows[0]["SpreadRO"].ToString();
            //txtOverdueRO.Text = dt.Rows[0]["OverdueRO"].ToString();
            //txtInterestPayerRO.Text = dt.Rows[0]["InterestPayerRO"].ToString();
            //txtNonAccrueRO.Text = dt.Rows[0]["NonAccrueRO"].ToString();
            //txtInterestOperationDateRO.Text = dt.Rows[0]["InterestOperationDateRO"].ToString();
            //txtInterestValueDateRO.Text = dt.Rows[0]["InterestValueDateRO"].ToString();
            //txtSettlementDateRO.Text = dt.Rows[0]["SettlementDateRO"].ToString();
            //txtSettlementValueDateRO.Text = dt.Rows[0]["SettlementValueDateRO"].ToString();
            //txtLastModificationDateRO.Text = dt.Rows[0]["LastModificationDateRO"].ToString();
            //txtLastOperationDateRO.Text = dt.Rows[0]["LastOperationDateRO"].ToString();
            //txtTransactionNoRO.Text = dt.Rows[0]["TransactionNoRO"].ToString();
            //txtRealizedInterestTotalRO.Text = dt.Rows[0]["RealizedInterestTotalRO"].ToString();
            //txtFundTypeRO.Text = dt.Rows[0]["FundTypeRO"].ToString();
            //txtInternalRateRO.Text = dt.Rows[0]["InternalRateRO"].ToString();
            //txtInterestRateChangeNoRO.Text = dt.Rows[0]["InterestRateChangeNoRO"].ToString();
            //txtStatusCodeRO.Text = dt.Rows[0]["StatusCodeRO"].ToString();
            hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
        }
    }
    public void GBaseFileCreationMM()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@DocNo", SqlDbType.VarChar);
        PRefNo.Value = txtReferenceNumberMK.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_MoneyMarket", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/MoneyMarket/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtReferenceNumberMK.Text.Trim() + "_GBase" + ".xlsx";
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

            }
        }
        else
        {
        }
    }
    public void GBaseFileCreationRO()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@DocNo", SqlDbType.VarChar);
        PRefNo.Value = txtReferenceNoRO.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_RollOver", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/RollOver/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtReferenceNoRO.Text.Trim() +"_GBase" + ".xlsx";
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

            }
        }
        else
        {
        }
    }
}