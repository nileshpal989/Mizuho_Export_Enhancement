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

public partial class IMP_Transactions_TF_IMP_InquiryOfLedgerFile_Maker : System.Web.UI.Page
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
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
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
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbRollOver;
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbMoneyMarket;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            TF_DATA obj = new TF_DATA();
            SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            string Result = obj.SaveDeleteData("TF_IMP_SubmitToCheckerMMRO", P_DocNo);
            if (Result == "Submit")
            {
                Clear();
                txtDocNo.Text = "";
                txtDocNo.Focus();
                TabContainerMain.ActiveTab = tbMoneyMarket;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Document sent to checker.')", true);
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void FillDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = obj.getData("TF_IMP_FillMMRODetails", P_DocNo);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblRejectReason.Text = "Reject Reason:- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
            else
            {
                lblRejectReason.Text = "";
            }

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
        }
        else
        {
            Clear();
            lblRejectReason.Text = "";
        }
    }
    [WebMethod]
    public static void SaveUpdateMoneyMarket(string _DueDate,string _MoneyMarket, string _txtCustomerNameMK, string _txtAccountCodeMK, string _txtCurrencyMK, string _txtReferenceNumberMK, string _txtSystemCodeMK,
    string _txtFrontNoMK, string _txtNosOfDaysMK, string _txtInterestMK, string _txtFinalDueDateMK, string _txtSpreadMK, string _txtBaseRateMK, string _txtFundsMK, string _txtSettlementMethodMK,
    string _txtOurDipositoryMK, string _txtCustomerAbbrMK, string _txtContraAccountMK, string _txtAccountNoMK, string _txtTheirDipository1MK, string _txtTheirDipository2MK, string _txtTheirDipository3MK,
    string _txtTheirDipository4MK, string _txtTheirAccountMK, string _txtATTNMK, string _txtCurrentBalanceMK, string _txtValueDateMK, string _txtOperationDateMK, string _txtSettlement1MK, string _txtSettlement2MK,
    string _txtLastModificationMK, string _txtRollOverNoMK, string _txtLastTRNSNoMK, string _txtLastOperationMK, string _txtRemEUCMK, string _txtStatusMK, string _txtOverDueAccrueMK, string _txtEntityMK,
        string _RollOver, string _txtCustomerNameRO, string _txtAccountCodeRO, string _txtCurrencyRO, string _txtReferenceNoRO, string _txtDivisionCodeRO,
    string _txtFrontNoRO, string _txtPledgedRO, string _txtRollOverNoRO, string _txtOperationDateRO, string _txtValueDateRO, string _txtDueDateRO, string _txtDaysRO, string _txtAmountRO, string _txtPrimeCCYRO,
    string _txtInterestRate1RO, string _txtInterestRate2RO, string _txtInterestAmountRO, string _txtDaysAYearRO, string _txtBaseRateRO, string _txtSpreadRO, string _txtOverdueRO, string _txtInterestPayerRO,
    string _txtNonAccrueRO, string _txtInterestOperationDateRO, string _txtInterestValueDateRO, string _txtSettlementDateRO, string _txtSettlementValueDateRO, string _txtLastModificationDateRO,
    string _txtLastOperationDateRO, string _txtTransactionNoRO, string _txtRealizedInterestTotalRO, string _txtFundTypeRO, string _txtInternalRateRO, string _txtInterestRateChangeNoRO, string _txtStatusCodeRO)
    {
        SqlParameter P_DueDate = new SqlParameter("@_DueDate", _DueDate.ToUpper());
        SqlParameter P_MoneyMarket = new SqlParameter("@_MoneyMarket", _MoneyMarket.ToUpper());
        SqlParameter P_txtCustomerNameMK = new SqlParameter("@_txtCustomerNameMK", _txtCustomerNameMK.ToUpper());
        SqlParameter P_txtAccountCodeMK = new SqlParameter("@_txtAccountCodeMK", _txtAccountCodeMK.ToUpper());
        SqlParameter P_txtCurrencyMK = new SqlParameter("@_txtCurrencyMK", _txtCurrencyMK.ToUpper());
        SqlParameter P_txtReferenceNumberMK = new SqlParameter("@_txtDocument_No", _txtReferenceNumberMK.ToUpper());
        SqlParameter P_txtSystemCodeMK = new SqlParameter("@_txtSystemCodeMK", _txtSystemCodeMK.ToUpper());
        SqlParameter P_txtFrontNoMK = new SqlParameter("@_txtFrontNoMK", _txtFrontNoMK.ToUpper());
        SqlParameter P_txtNosOfDaysMK = new SqlParameter("@_txtNosOfDaysMK", _txtNosOfDaysMK.ToUpper());
        SqlParameter P_txtInterestMK = new SqlParameter("@_txtInterestMK", _txtInterestMK.ToUpper());
        SqlParameter P_txtFinalDueDateMK = new SqlParameter("@_txtFinalDueDateMK", _txtFinalDueDateMK.ToUpper());
        SqlParameter P_txtSpreadMK = new SqlParameter("@_txtSpreadMK", _txtSpreadMK.ToUpper());
        SqlParameter P_txtBaseRateMK = new SqlParameter("@_txtBaseRateMK", _txtBaseRateMK.ToUpper());
        SqlParameter P_txtFundsMK = new SqlParameter("@_txtFundsMK", _txtFundsMK.ToUpper());
        SqlParameter P_txtSettlementMethodMK = new SqlParameter("@_txtSettlementMethodMK", _txtSettlementMethodMK.ToUpper());
        SqlParameter P_txtOurDipositoryMK = new SqlParameter("@_txtOurDipositoryMK", _txtOurDipositoryMK.ToUpper());
        SqlParameter P_txtCustomerAbbrMK = new SqlParameter("@_txtCustomerAbbrMK", _txtCustomerAbbrMK.ToUpper());
        SqlParameter P_txtContraAccountMK = new SqlParameter("@_txtContraAccountMK", _txtContraAccountMK.ToUpper());
        SqlParameter P_txtAccountNoMK = new SqlParameter("@_txtAccountNoMK", _txtAccountNoMK.ToUpper());
        SqlParameter P_txtTheirDipository1MK = new SqlParameter("@_txtTheirDipository1MK", _txtTheirDipository1MK.ToUpper());
        SqlParameter P_txtTheirDipository2MK = new SqlParameter("@_txtTheirDipository2MK", _txtTheirDipository2MK.ToUpper());
        SqlParameter P_txtTheirDipository3MK = new SqlParameter("@_txtTheirDipository3MK", _txtTheirDipository3MK.ToUpper());
        SqlParameter P_txtTheirDipository4MK = new SqlParameter("@_txtTheirDipository4MK", _txtTheirDipository4MK.ToUpper());
        SqlParameter P_txtTheirAccountMK = new SqlParameter("@_txtTheirAccountMK", _txtTheirAccountMK.ToUpper());
        SqlParameter P_txtATTNMK = new SqlParameter("@_txtATTNMK", _txtATTNMK.ToUpper());
        SqlParameter P_txtCurrentBalanceMK = new SqlParameter("@_txtCurrentBalanceMK", _txtCurrentBalanceMK.ToUpper());
        SqlParameter P_txtValueDateMK = new SqlParameter("@_txtValueDateMK", _txtValueDateMK.ToUpper());
        SqlParameter P_txtOperationDateMK = new SqlParameter("@_txtOperationDateMK", _txtOperationDateMK.ToUpper());
        SqlParameter P_txtSettlement1MK = new SqlParameter("@_txtSettlement1MK", _txtSettlement1MK.ToUpper());
        SqlParameter P_txtSettlement2MK = new SqlParameter("@_txtSettlement2MK", _txtSettlement2MK.ToUpper());
        SqlParameter P_txtLastModificationMK = new SqlParameter("@_txtLastModificationMK", _txtLastModificationMK.ToUpper());
        SqlParameter P_txtRollOverNoMK = new SqlParameter("@_txtRollOverNoMK", _txtRollOverNoMK.ToUpper());
        SqlParameter P_txtLastTRNSNoMK = new SqlParameter("@_txtLastTRNSNoMK", _txtLastTRNSNoMK.ToUpper());
        SqlParameter P_txtLastOperationMK = new SqlParameter("@_txtLastOperationMK", _txtLastOperationMK.ToUpper());
        SqlParameter P_txtRemEUCMK = new SqlParameter("@_txtRemEUCMK", _txtRemEUCMK.ToUpper());
        SqlParameter P_txtStatusMK = new SqlParameter("@_txtStatusMK", _txtStatusMK.ToUpper());
        SqlParameter P_txtOverDueAccrueMK = new SqlParameter("@_txtOverDueAccrueMK", _txtOverDueAccrueMK.ToUpper());
        SqlParameter P_txtEntityMK = new SqlParameter("@_txtEntityMK", _txtEntityMK.ToUpper());

        //Roll Over
        SqlParameter P_RollOver = new SqlParameter("@_RollOver", _RollOver.ToUpper());
        SqlParameter P_txtCustomerNameRO = new SqlParameter("@_txtCustomerNameRO", _txtCustomerNameRO.ToUpper());
        SqlParameter P_txtAccountCodeRO = new SqlParameter("@_txtAccountCodeRO", _txtAccountCodeRO.ToUpper());
        SqlParameter P_txtCurrencyRO = new SqlParameter("@_txtCurrencyRO", _txtCurrencyRO.ToUpper());
        SqlParameter P_txtDivisionCodeRO = new SqlParameter("@_txtDivisionCodeRO", _txtDivisionCodeRO.ToUpper());
        SqlParameter P_txtFrontNoRO = new SqlParameter("@_txtFrontNoRO", _txtFrontNoRO.ToUpper());
        SqlParameter P_txtPledgedRO = new SqlParameter("@_txtPledgedRO", _txtPledgedRO.ToUpper());
        SqlParameter P_txtRollOverNoRO = new SqlParameter("@_txtRollOverNoRO", _txtRollOverNoRO.ToUpper());
        SqlParameter P_txtOperationDateRO = new SqlParameter("@_txtOperationDateRO", _txtOperationDateRO.ToUpper());
        SqlParameter P_txtValueDateRO = new SqlParameter("@_txtValueDateRO", _txtValueDateRO.ToUpper());
        SqlParameter P_txtDueDateRO = new SqlParameter("@_txtDueDateRO", _txtDueDateRO.ToUpper());
        SqlParameter P_txtDaysRO = new SqlParameter("@_txtDaysRO", _txtDaysRO.ToUpper());
        SqlParameter P_txtAmountRO = new SqlParameter("@_txtAmountRO", _txtAmountRO.ToUpper());
        SqlParameter P_txtPrimeCCYRO = new SqlParameter("@_txtPrimeCCYRO", _txtPrimeCCYRO.ToUpper());
        SqlParameter P_txtInterestRate1RO = new SqlParameter("@_txtInterestRate1RO", _txtInterestRate1RO.ToUpper());
        SqlParameter P_txtInterestRate2RO = new SqlParameter("@_txtInterestRate2RO", _txtInterestRate2RO.ToUpper());
        SqlParameter P_txtInterestAmountRO = new SqlParameter("@_txtInterestAmountRO", _txtInterestAmountRO.ToUpper());
        SqlParameter P_txtDaysAYearRO = new SqlParameter("@_txtDaysAYearRO", _txtDaysAYearRO.ToUpper());
        SqlParameter P_txtBaseRateRO = new SqlParameter("@_txtBaseRateRO", _txtBaseRateRO.ToUpper());
        SqlParameter P_txtSpreadRO = new SqlParameter("@_txtSpreadRO", _txtSpreadRO.ToUpper());
        SqlParameter P_txtOverdueRO = new SqlParameter("@_txtOverdueRO", _txtOverdueRO.ToUpper());
        SqlParameter P_txtInterestPayerRO = new SqlParameter("@_txtInterestPayerRO", _txtInterestPayerRO.ToUpper());
        SqlParameter P_txtNonAccrueRO = new SqlParameter("@_txtNonAccrueRO", _txtNonAccrueRO.ToUpper());
        SqlParameter P_txtInterestOperationDateRO = new SqlParameter("@_txtInterestOperationDateRO", _txtInterestOperationDateRO.ToUpper());
        SqlParameter P_txtInterestValueDateRO = new SqlParameter("@_txtInterestValueDateRO", _txtInterestValueDateRO.ToUpper());
        SqlParameter P_txtSettlementDateRO = new SqlParameter("@_txtSettlementDateRO", _txtSettlementDateRO.ToUpper());
        SqlParameter P_txtSettlementValueDateRO = new SqlParameter("@_txtSettlementValueDateRO", _txtSettlementValueDateRO.ToUpper());
        SqlParameter P_txtLastModificationDateRO = new SqlParameter("@_txtLastModificationDateRO", _txtLastModificationDateRO.ToUpper());
        SqlParameter P_txtLastOperationDateRO = new SqlParameter("@_txtLastOperationDateRO", _txtLastOperationDateRO.ToUpper());
        SqlParameter P_txtTransactionNoRO = new SqlParameter("@_txtTransactionNoRO", _txtTransactionNoRO.ToUpper());
        SqlParameter P_txtRealizedInterestTotalRO = new SqlParameter("@_txtRealizedInterestTotalRO", _txtRealizedInterestTotalRO.ToUpper());
        SqlParameter P_txtFundTypeRO = new SqlParameter("@_txtFundTypeRO", _txtFundTypeRO.ToUpper());
        SqlParameter P_txtInternalRateRO = new SqlParameter("@_txtInternalRateRO", _txtInternalRateRO.ToUpper());
        SqlParameter P_txtInterestRateChangeNoRO = new SqlParameter("@_txtInterestRateChangeNoRO", _txtInterestRateChangeNoRO.ToUpper());
        SqlParameter P_txtStatusCodeRO = new SqlParameter("@_txtStatusCodeRO", _txtStatusCodeRO.ToUpper());

        TF_DATA obj = new TF_DATA();
        string _result = obj.SaveDeleteData("TF_IMP_AddUpdateMMRO",P_DueDate, P_txtCustomerNameMK, P_txtAccountCodeMK, P_txtCurrencyMK, P_txtReferenceNumberMK, P_txtSystemCodeMK, P_txtFrontNoMK, P_txtNosOfDaysMK,
P_txtInterestMK, P_txtFinalDueDateMK, P_txtSpreadMK, P_txtBaseRateMK, P_txtFundsMK, P_txtSettlementMethodMK, P_txtOurDipositoryMK, P_txtCustomerAbbrMK, P_txtContraAccountMK, P_txtAccountNoMK,
P_txtTheirDipository1MK, P_txtTheirDipository2MK, P_txtTheirDipository3MK, P_txtTheirDipository4MK, P_txtTheirAccountMK, P_txtATTNMK, P_txtCurrentBalanceMK, P_txtValueDateMK, P_txtOperationDateMK,
P_txtSettlement1MK, P_txtSettlement2MK, P_txtLastModificationMK, P_txtRollOverNoMK, P_txtLastTRNSNoMK, P_txtLastOperationMK, P_txtRemEUCMK, P_txtStatusMK, P_txtOverDueAccrueMK, P_txtEntityMK,
P_txtCustomerNameRO, P_txtAccountCodeRO, P_txtCurrencyRO, P_txtDivisionCodeRO, P_txtFrontNoRO,
P_txtPledgedRO, P_txtRollOverNoRO, P_txtOperationDateRO, P_txtValueDateRO, P_txtDueDateRO, P_txtDaysRO, P_txtAmountRO, P_txtPrimeCCYRO, P_txtInterestRate1RO, P_txtInterestRate2RO, P_txtInterestAmountRO,
P_txtDaysAYearRO, P_txtBaseRateRO, P_txtSpreadRO, P_txtOverdueRO, P_txtInterestPayerRO, P_txtNonAccrueRO, P_txtInterestOperationDateRO, P_txtInterestValueDateRO, P_txtSettlementDateRO,
P_txtSettlementValueDateRO, P_txtLastModificationDateRO, P_txtLastOperationDateRO, P_txtTransactionNoRO, P_txtRealizedInterestTotalRO, P_txtFundTypeRO, P_txtInternalRateRO, P_txtInterestRateChangeNoRO,
P_txtStatusCodeRO);
        string a = _result;
    }
    
    protected void txtDocNo_OnTextChanged(object sender, EventArgs e)
    {
        FillDetails();
        txtReferenceNoRO.Text = txtDocNo.Text;
        txtReferenceNumberMK.Text = txtDocNo.Text;
    }
    protected void Clear()
    {
        txtCustomerNameMK.Text = "";
        txtAccountCodeMK.Text = "";
        txtCurrencyMK.Text = "";
        txtReferenceNumberMK.Text = "";
        txtSystemCodeMK.Text = "";
        txtFrontNoMK.Text = "";
        txtNosOfDaysMK.Text = "";
        txtInterestMK.Text = "";
        txtFinalDueDateMK.Text = "";
        txtSpreadMK.Text = "";
        txtBaseRateMK.Text = "";
        txtFundsMK.Text = "";
        txtSettlementMethodMK.Text = "";
        txtOurDipositoryMK.Text = "";
        txtCustomerAbbrMK.Text = "";
        txtContraAccountMK.Text = "";
        txtAccountNoMK.Text = "";
        txtTheirDipository1MK.Text = "";
        txtTheirDipository2MK.Text = "";
        txtTheirDipository3MK.Text = "";
        txtTheirDipository4MK.Text = "";
        txtTheirAccountMK.Text = "";
        txtATTNMK.Text = "";
        txtCurrentBalanceMK.Text = "";
        txtValueDateMK.Text = "";
        txtOperationDateMK.Text = "";
        txtSettlement1MK.Text = "";
        txtSettlement2MK.Text = "";
        txtLastModificationMK.Text = "";
        txtRollOverNoMK.Text = "";
        txtLastTRNSNoMK.Text = "";
        txtLastOperationMK.Text = "";
        txtRemEUCMK.Text = "";
        txtStatusMK.Text = "";
        txtOverDueAccrueMK.Text = "";
        txtEntityMK.Text = "";

        txtCustomerNameRO.Text = "";
        txtAccountCodeRO.Text = "";
        txtCurrencyRO.Text = "";
        txtReferenceNoRO.Text = "";
        txtDivisionCodeRO.Text = "";
        txtFrontNoRO.Text = "";
        txtPledgedRO.Text = "";
        txtRollOverNoRO.Text = "";
        txtOperationDateRO.Text = "";
        txtValueDateRO.Text = "";
        txtDueDateRO.Text = "";
        txtDaysRO.Text = "";
        txtAmountRO.Text = "";
        txtPrimeCCYRO.Text = "";
        txtInterestRate1RO.Text = "";
        txtInterestRate2RO.Text = "";
        txtInterestAmountRO.Text = "";
        txtDaysAYearRO.Text = "";
        txtBaseRateRO.Text = "";
        txtSpreadRO.Text = "";
        txtOverdueRO.Text = "";
        txtInterestPayerRO.Text = "";
        txtNonAccrueRO.Text = "";
        txtInterestOperationDateRO.Text = "";
        txtInterestValueDateRO.Text = "";
        txtSettlementDateRO.Text = "";
        txtSettlementValueDateRO.Text = "";
        txtLastModificationDateRO.Text = "";
        txtLastOperationDateRO.Text = "";
        txtTransactionNoRO.Text = "";
        txtRealizedInterestTotalRO.Text = "";
        txtFundTypeRO.Text = "";
        txtInternalRateRO.Text = "";
        txtInterestRateChangeNoRO.Text = "";
        txtStatusCodeRO.Text = "";
        txtAccountCodeRO.Text = "";
        txtCurrencyRO.Text = "";
        txtReferenceNoRO.Text = "";
        txtDivisionCodeRO.Text = "";
        txtFrontNoRO.Text = "";
        txtPledgedRO.Text = "";
        txtRollOverNoRO.Text = "";
        txtOperationDateRO.Text = "";
        txtValueDateRO.Text = "";
        txtDueDateRO.Text = "";
        txtDaysRO.Text = "";
        txtAmountRO.Text = "";
        txtPrimeCCYRO.Text = "";
        txtInterestRate1RO.Text = "";
        txtInterestRate2RO.Text = "";
        txtInterestAmountRO.Text = "";
        txtDaysAYearRO.Text = "";
        txtBaseRateRO.Text = "";
        txtSpreadRO.Text = "";
        txtOverdueRO.Text = "";
        txtInterestPayerRO.Text = "";
        txtNonAccrueRO.Text = "";
        txtInterestOperationDateRO.Text = "";
        txtInterestValueDateRO.Text = "";
        txtSettlementDateRO.Text = "";
        txtSettlementValueDateRO.Text = "";
        txtLastModificationDateRO.Text = "";
        txtLastOperationDateRO.Text = "";
        txtTransactionNoRO.Text = "";
        txtRealizedInterestTotalRO.Text = "";
        txtFundTypeRO.Text = "";
        txtInternalRateRO.Text = "";
        txtInterestRateChangeNoRO.Text = "";
        txtStatusCodeRO.Text = "";        
    }
}