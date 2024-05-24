using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using OfficeOpenXml;
using ClosedXML.Excel;

public partial class IMP_Transactions_TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Checker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TabContainerMain.ActiveTab = tbDocumentDetails;
            if (Request.QueryString["DocNo"] == null)
            {
                Response.Redirect("TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Maker_View.aspx", true);
            }
            else
            {
                hdnUserName.Value = Session["userName"].ToString();
                hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                hdnDocType.Value = Request.QueryString["DocType"].Trim();
                hdnDocScrutiny.Value = Request.QueryString["DocScrutiny"].Trim();
                txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                txtIBDDocNo.Text = Request.QueryString["IBD_DocNo"].Trim();

                hdnDocNo.Value = Request.QueryString["DocNo"].Trim();

                if (Request.QueryString["Status"] == "Approved By Checker")
                {
                    ddlApproveReject.Enabled = false;
                }

                Fill_Logd_Accept_Details();
                Fill_LCDiscountingDetails();

            }
        }
        ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
    }
    protected void Fill_Logd_Accept_Details()
    {
        Get_Acceptance_Details();
        Get_IBD_Lodgment_Details();
    }
    protected void Fill_LCDiscountingDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_IBD_Settlement_Get_Details", PDocNo, PIBD_DocNo);
        if (dt.Rows.Count > 0)
        {
            txtDocNo.Text = dt.Rows[0]["Document_No"].ToString();
            txtIBDDocNo.Text = dt.Rows[0]["IBD_Document_No"].ToString();
            txtValueDate.Text = dt.Rows[0]["Settlement_Date"].ToString();

            if (dt.Rows[0]["AccDocDetails_Flag"].ToString() == "Y")
            {
                chk_AccDocDetails.Checked = true;
                Panel_AccDocDetails.Visible = true;

                txtCustName.Text = dt.Rows[0]["CustomerName"].ToString();
                txtCommentCode.Text = dt.Rows[0]["Comment_Code"].ToString();
                txtMaturityDate.Text = dt.Rows[0]["Maturity_Date"].ToString();
                txtsettlCodeForCust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
                txtSettl_CodeForBank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();
                txtInterest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();
                txtDiscount.Text = dt.Rows[0]["Discount_Flag"].ToString();
                txtInterest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();
                txt_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();
                txt_INT_Rate.Text = dt.Rows[0]["Interest_Rate"].ToString();
                txtInterestAmt.Text = dt.Rows[0]["Interest_Amount"].ToString();
                txtOverinterestRate.Text = dt.Rows[0]["Overdue_Interest_Rate"].ToString();
                txtOverNoOfDays.Text = dt.Rows[0]["Overdue_No_Of_Days"].ToString();
                txtOverAmount.Text = dt.Rows[0]["Overdue_Interest_Amount"].ToString();
                txtAttn.Text = dt.Rows[0]["Attn"].ToString();
            }
            else
            {
                chk_AccDocDetails.Checked = false;
                Panel_AccDocDetails.Visible = false;
            }
            ////-------Import Accounting Acc------------
            if (dt.Rows[0]["AccImpAccounting_Flag"].ToString() == "Y")
            {
                chk_AccImpAccounting.Checked = true;
                Panel_AccImpAccounting.Visible = true;

                txt_DiscAmt.Text = dt.Rows[0]["IMP_ACC_Amount"].ToString();
                txt_IMP_ACC_ExchRate.Text = dt.Rows[0]["IMP_ACC_ExchRate"].ToString();
                txtPrinc_matu.Text = dt.Rows[0]["Principal_MATU"].ToString();
                txtPrinc_lump.Text = dt.Rows[0]["Principal_LUMP"].ToString();
                txtprinc_Contract_no.Text = dt.Rows[0]["Principal_Contract_No"].ToString();
                txt_Princ_Ex_Curr.Text = dt.Rows[0]["Principal_Ex_Curr"].ToString();
                txtprinc_Ex_rate.Text = dt.Rows[0]["Principal_Exch_Rate"].ToString();
                txtprinc_Intnl_Ex_rate.Text = dt.Rows[0]["Principal_Intnl_Exch_Rate"].ToString();

                txtInterest_matu.Text = dt.Rows[0]["Interest_MATU"].ToString();
                txtInterest_lump.Text = dt.Rows[0]["Interest_LUMP"].ToString();
                txtInterest_Contract_no.Text = dt.Rows[0]["Interest_Contract_No"].ToString();
                txt_interest_Ex_Curr.Text = dt.Rows[0]["Interest_Ex_Curr"].ToString();
                txtInterest_Ex_rate.Text = dt.Rows[0]["Interest_Exch_Rate"].ToString();
                txtInterest_Intnl_Ex_rate.Text = dt.Rows[0]["Interest_Intnl_Exch_Rate"].ToString();

                txtCommission_matu.Text = dt.Rows[0]["Commission_MATU"].ToString();
                txtCommission_lump.Text = dt.Rows[0]["Commission_LUMP"].ToString();
                txtCommission_Contract_no.Text = dt.Rows[0]["Commission_Contract_No"].ToString();
                txt_Commission_Ex_Curr.Text = dt.Rows[0]["Commission_Ex_Curr"].ToString();
                txtCommission_Ex_rate.Text = dt.Rows[0]["Commission_Exch_Rate"].ToString();
                txtCommission_Intnl_Ex_rate.Text = dt.Rows[0]["Commission_Intnl_Exch_Rate"].ToString();

                txtTheir_Commission_matu.Text = dt.Rows[0]["Their_Commission_MATU"].ToString();
                txtTheir_Commission_lump.Text = dt.Rows[0]["Their_Commission_LUMP"].ToString();
                txtTheir_Commission_Contract_no.Text = dt.Rows[0]["Their_Commission_Contract_No"].ToString();
                txt_Their_Commission_Ex_Curr.Text = dt.Rows[0]["Their_Commission_Ex_Curr"].ToString();
                txtTheir_Commission_Ex_rate.Text = dt.Rows[0]["Their_Commission_Exch_Rate"].ToString();
                txtTheir_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["Their_Commission_Intnl_Exch_Rate"].ToString();

                txt_CR_Code.Text = dt.Rows[0]["CR_Code"].ToString();
                txt_CR_AC_ShortName.Text = dt.Rows[0]["CR_Cust_ACC_Name"].ToString();
                txt_CR_Cust_abbr.Text = dt.Rows[0]["CR_Cust_Abbr"].ToString();
                txt_CR_Cust_Acc.Text = dt.Rows[0]["CR_Cust_Acc_No"].ToString();
                txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Acceptance_Curr"].ToString();
                txt_CR_Acceptance_amt.Text = dt.Rows[0]["CR_Acceptance_Amount"].ToString();
                txt_CR_Acceptance_payer.Text = dt.Rows[0]["CR_Acceptance_Payer"].ToString();

                txt_CR_Interest_Curr.Text = dt.Rows[0]["CR_Interest_Curr"].ToString();
                txt_CR_Interest_amt.Text = dt.Rows[0]["CR_Interest_Amount"].ToString();
                txt_CR_Interest_payer.Text = dt.Rows[0]["CR_Interest_Payer"].ToString();

                txt_CR_Accept_Commission_Curr.Text = dt.Rows[0]["CR_Acceptance_Comm_Curr"].ToString();
                txt_CR_Accept_Commission_amt.Text = dt.Rows[0]["CR_Acceptance_Comm_Amount"].ToString();
                txt_CR_Accept_Commission_Payer.Text = dt.Rows[0]["CR_Acceptance_Comm_Payer"].ToString();

                txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Curr"].ToString();
                txt_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Amount"].ToString();
                txt_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Payer"].ToString();

                txt_CR_Others_Curr.Text = dt.Rows[0]["CR_Others_Curr"].ToString();
                txt_CR_Others_amt.Text = dt.Rows[0]["CR_Others_Amount"].ToString();
                txt_CR_Others_Payer.Text = dt.Rows[0]["CR_Others_Payer"].ToString();

                txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["CR_Their_Comm_Curr"].ToString();
                txt_CR_Their_Commission_amt.Text = dt.Rows[0]["CR_Their_Comm_Amount"].ToString();
                txt_CR_Their_Commission_Payer.Text = dt.Rows[0]["CR_Their_Comm_Payer"].ToString();

                txt_DR_Code.Text = dt.Rows[0]["DR_CODE"].ToString();
                txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_abbr"].ToString();
                txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc"].ToString();
                txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
                txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["DR_Current_Acc_Amount"].ToString();
                txt_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_Current_Acc_Payer"].ToString();

                txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["DR_Current_Acc_Curr2"].ToString();
                txt_DR_Cur_Acc_amt2.Text = dt.Rows[0]["DR_Current_Acc_Amount2"].ToString();
                txt_DR_Cur_Acc_payer2.Text = dt.Rows[0]["DR_Current_Acc_Payer2"].ToString();

                txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["DR_Current_Acc_Curr3"].ToString();
                txt_DR_Cur_Acc_amt3.Text = dt.Rows[0]["DR_Current_Acc_Amount3"].ToString();
                txt_DR_Cur_Acc_payer3.Text = dt.Rows[0]["DR_Current_Acc_Payer3"].ToString();
            }
            else
            {
                chk_AccImpAccounting.Checked = false;
                Panel_AccImpAccounting.Visible = false;
            }

            //------------------------GENRAL OPERATION I-----------------------------------------------
            if (dt.Rows[0]["GO_Bill_Handling_Flag"].ToString() == "Y")
            {
                chk_GO1_Flag.Checked = true;
                Panel_GO1.Visible = true;
                txt_GO1_Comment.Text = dt.Rows[0]["GO_Bill_Handling_Comment"].ToString();
                txt_GO1_SectionNo.Text = dt.Rows[0]["GO_Bill_Handling_Section"].ToString();
                txt_GO1_Remarks.Text = dt.Rows[0]["GO_Bill_Handling_Remark"].ToString();
                txt_GO1_Memo.Text = dt.Rows[0]["GO_Bill_Handling_Memo"].ToString();
                txt_GO1_Scheme_no.Text = dt.Rows[0]["GO_Bill_Handling_SchemeNo"].ToString();
                txt_GO1_Debit_Code.SelectedValue = dt.Rows[0]["GO_Bill_Handling_Debit_Code"].ToString();
                txt_GO1_Debit_Curr.Text = dt.Rows[0]["GO_Bill_Handling_Debit_CCY"].ToString();
                txt_GO1_Debit_Amt.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Amt"].ToString();
                txt_GO1_Debit_Cust.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_abbr"].ToString();
                txt_GO1_Debit_Cust_Name.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_Name"].ToString();
                txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_GO1_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccNo"].ToString();
                txt_GO1_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccCode_Disc"].ToString();
                txt_GO1_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Debit_ExchRate"].ToString();
                txt_GO1_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Bill_Handling_Debit_ExchCCY"].ToString();
                txt_GO1_Debit_FUND.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Fund"].ToString();
                txt_GO1_Debit_Check_No.Text = dt.Rows[0]["GO_Bill_Handling_Debit_CheckNo"].ToString();
                txt_GO1_Debit_Available.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Available"].ToString();
                txt_GO1_Debit_AdPrint.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Advice_Print"].ToString();
                txt_GO1_Debit_Details.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Details"].ToString();
                txt_GO1_Debit_Entity.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Entity"].ToString();
                txt_GO1_Debit_Division.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Division"].ToString();
                txt_GO1_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Bill_Handling_Debit_InterAmt"].ToString();
                txt_GO1_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Debit_InterRate"].ToString();
                txt_GO1_Credit_Code.SelectedValue = dt.Rows[0]["GO_Bill_Handling_Credit_Code"].ToString();
                txt_GO1_Credit_Curr.Text = dt.Rows[0]["GO_Bill_Handling_Credit_CCY"].ToString();
                txt_GO1_Credit_Amt.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Amt"].ToString();
                txt_GO1_Credit_Cust.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_abbr"].ToString();
                txt_GO1_Credit_Cust_Name.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_Name"].ToString();
                txt_GO1_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_AccCode"].ToString();
                txt_GO1_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_AccCode_Disc"].ToString();
                txt_GO1_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_AccNo"].ToString();
                txt_GO1_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Credit_ExchRate"].ToString();
                txt_GO1_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Bill_Handling_Credit_ExchCCY"].ToString();
                txt_GO1_Credit_FUND.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Fund"].ToString();
                txt_GO1_Credit_Check_No.Text = dt.Rows[0]["GO_Bill_Handling_Credit_CheckNo"].ToString();
                txt_GO1_Credit_Available.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Available"].ToString();
                txt_GO1_Credit_AdPrint.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Advice_Print"].ToString();
                txt_GO1_Credit_Details.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Details"].ToString();
                txt_GO1_Credit_Entity.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Entity"].ToString();
                txt_GO1_Credit_Division.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Division"].ToString();
                txt_GO1_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Bill_Handling_Credit_InterAmt"].ToString();
                txt_GO1_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Credit_InterRate"].ToString();
            }
            //------------------------GENRAL OPERATION II-----------------------------------------------
            if (dt.Rows[0]["GO2_Bill_Handling_Flag"].ToString() == "Y")
            {
                chk_GO2_Flag.Checked = true;
                Panel_GO2.Visible = true;

                txt_GO2_Comment.Text = dt.Rows[0]["GO2_Bill_Handling_Comment"].ToString();
                txt_GO2_SectionNo.Text = dt.Rows[0]["GO2_Bill_Handling_Section"].ToString();
                txt_GO2_Remarks.Text = dt.Rows[0]["GO2_Bill_Handling_Remark"].ToString();
                txt_GO2_Memo.Text = dt.Rows[0]["GO2_Bill_Handling_Memo"].ToString();
                txt_GO2_Scheme_no.Text = dt.Rows[0]["GO2_Bill_Handling_SchemeNo"].ToString();
                txt_GO2_Debit_Code.SelectedValue = dt.Rows[0]["GO2_Bill_Handling_Debit_Code"].ToString();
                txt_GO2_Debit_Curr.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_CCY"].ToString();
                txt_GO2_Debit_Amt.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Amt"].ToString();
                txt_GO2_Debit_Cust.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_abbr"].ToString();
                txt_GO2_Debit_Cust_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_Name"].ToString();
                txt_GO2_Debit_Cust_AcCode.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_GO2_Debit_Cust_AccNo.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccNo"].ToString();
                txt_GO2_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccCode_Disc"].ToString();
                txt_GO2_Debit_Exch_Rate.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_ExchRate"].ToString();
                txt_GO2_Debit_Exch_CCY.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_ExchCCY"].ToString();
                txt_GO2_Debit_FUND.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Fund"].ToString();
                txt_GO2_Debit_Check_No.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_CheckNo"].ToString();
                txt_GO2_Debit_Available.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Available"].ToString();
                txt_GO2_Debit_AdPrint.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Advice_Print"].ToString();
                txt_GO2_Debit_Details.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Details"].ToString();
                txt_GO2_Debit_Entity.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Entity"].ToString();
                txt_GO2_Debit_Division.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Division"].ToString();
                txt_GO2_Debit_Inter_Amount.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_InterAmt"].ToString();
                txt_GO2_Debit_Inter_Rate.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_InterRate"].ToString();
                txt_GO2_Credit_Code.SelectedValue = dt.Rows[0]["GO2_Bill_Handling_Credit_Code"].ToString();
                txt_GO2_Credit_Curr.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_CCY"].ToString();
                txt_GO2_Credit_Amt.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Amt"].ToString();
                txt_GO2_Credit_Cust.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_abbr"].ToString();
                txt_GO2_Credit_Cust_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_Name"].ToString();
                txt_GO2_Credit_Cust_AcCode.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_AccCode"].ToString();
                txt_GO2_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_AccCode_Disc"].ToString();
                txt_GO2_Credit_Cust_AccNo.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_AccNo"].ToString();
                txt_GO2_Credit_Exch_Rate.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_ExchRate"].ToString();
                txt_GO2_Credit_Exch_Curr.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_ExchCCY"].ToString();
                txt_GO2_Credit_FUND.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Fund"].ToString();
                txt_GO2_Credit_Check_No.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_CheckNo"].ToString();
                txt_GO2_Credit_Available.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Available"].ToString();
                txt_GO2_Credit_AdPrint.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Advice_Print"].ToString();
                txt_GO2_Credit_Details.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Details"].ToString();
                txt_GO2_Credit_Entity.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Entity"].ToString();
                txt_GO2_Credit_Division.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Division"].ToString();
                txt_GO2_Credit_Inter_Amount.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_InterAmt"].ToString();
                txt_GO2_Credit_Inter_Rate.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_InterRate"].ToString();
            }
            ///////---------IBD Doc Details-----------
            txt_IBD_CustName.Text = dt.Rows[0]["IBD_CustomerName"].ToString();
            txt_IBD_CommentCode.Text = dt.Rows[0]["IBD_Comment_Code"].ToString();
            txt_IBD_MaturityDate.Text = dt.Rows[0]["IBD_Maturity_Date"].ToString();
            txt_IBD_settlCodeForCust.Text = dt.Rows[0]["IBD_Settlement_For_Cust_Code"].ToString();
            txt_IBD_Settl_CodeForBank.Text = dt.Rows[0]["IBD_Settlement_For_Bank_Code"].ToString();
            txt_IBD_Interest_From.Text = dt.Rows[0]["IBD_Interest_From_Date"].ToString();
            txt_IBD_Discount.Text = dt.Rows[0]["IBD_Discount_Flag"].ToString();
            txt_IBD_Interest_To.Text = dt.Rows[0]["IBD_Interest_To_Date"].ToString();
            txt_IBD__No_Of_Days.Text = dt.Rows[0]["IBD_No_Of_Days"].ToString();
            txt_IBD__INT_Rate.Text = dt.Rows[0]["IBD_Interest_Rate"].ToString();
            txt_IBD_InterestAmt.Text = dt.Rows[0]["IBD_Interest_Amount"].ToString();
            txt_IBD_OverinterestRate.Text = dt.Rows[0]["IBD_Overdue_Interest_Rate"].ToString();
            txt_IBD_OverNoOfDays.Text = dt.Rows[0]["IBD_Overdue_No_Of_Days"].ToString();
            txt_IBD_OverAmount.Text = dt.Rows[0]["IBD_Overdue_Interest_Amount"].ToString();
            txt_IBD_Attn.Text = dt.Rows[0]["IBD_Attn"].ToString();

            ////-------IBD Import Accounting Acc------------
            txt_IBD_DiscAmt.Text = dt.Rows[0]["IBD_IMP_ACC_Amount"].ToString();
            txt_IBD_IMP_ACC_ExchRate.Text = dt.Rows[0]["IBD_IMP_ACC_ExchRate"].ToString();
            txt_IBDPrinc_matu.Text = dt.Rows[0]["IBD_Principal_MATU"].ToString();
            txt_IBDPrinc_lump.Text = dt.Rows[0]["IBD_Principal_LUMP"].ToString();
            txt_IBDprinc_Contract_no.Text = dt.Rows[0]["IBD_Principal_Contract_No"].ToString();
            txt_IBD_Princ_Ex_Curr.Text = dt.Rows[0]["IBD_Principal_Ex_Curr"].ToString();
            txt_IBDprinc_Ex_rate.Text = dt.Rows[0]["IBD_Principal_Exch_Rate"].ToString();
            txt_IBDprinc_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Principal_Intnl_Exch_Rate"].ToString();

            txt_IBDInterest_matu.Text = dt.Rows[0]["IBD_Interest_MATU"].ToString();
            txt_IBDInterest_lump.Text = dt.Rows[0]["IBD_Interest_LUMP"].ToString();
            txt_IBDInterest_Contract_no.Text = dt.Rows[0]["IBD_Interest_Contract_No"].ToString();
            txt_IBD_interest_Ex_Curr.Text = dt.Rows[0]["IBD_Interest_Ex_Curr"].ToString();
            txt_IBDInterest_Ex_rate.Text = dt.Rows[0]["IBD_Interest_Exch_Rate"].ToString();
            txt_IBDInterest_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Interest_Intnl_Exch_Rate"].ToString();

            txt_IBDCommission_matu.Text = dt.Rows[0]["IBD_Commission_MATU"].ToString();
            txt_IBDCommission_lump.Text = dt.Rows[0]["IBD_Commission_LUMP"].ToString();
            txt_IBDCommission_Contract_no.Text = dt.Rows[0]["IBD_Commission_Contract_No"].ToString();
            txt_IBD_Commission_Ex_Curr.Text = dt.Rows[0]["IBD_Commission_Ex_Curr"].ToString();
            txt_IBDCommission_Ex_rate.Text = dt.Rows[0]["IBD_Commission_Exch_Rate"].ToString();
            txt_IBDCommission_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Commission_Intnl_Exch_Rate"].ToString();

            txt_IBDTheir_Commission_matu.Text = dt.Rows[0]["IBD_Their_Commission_MATU"].ToString();
            txt_IBDTheir_Commission_lump.Text = dt.Rows[0]["IBD_Their_Commission_LUMP"].ToString();
            txt_IBDTheir_Commission_Contract_no.Text = dt.Rows[0]["IBD_Their_Commission_Contract_No"].ToString();
            txt_IBD_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IBD_Their_Commission_Ex_Curr"].ToString();
            txt_IBDTheir_Commission_Ex_rate.Text = dt.Rows[0]["IBD_Their_Commission_Exch_Rate"].ToString();
            txt_IBDTheir_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Their_Commission_Intnl_Exch_Rate"].ToString();

            txt_IBD_CR_Code.Text = dt.Rows[0]["IBD_CR_Code"].ToString();
            txt_IBD_CR_AC_ShortName.Text = dt.Rows[0]["IBD_CR_Cust_ACC_Name"].ToString();
            txt_IBD_CR_Cust_abbr.Text = dt.Rows[0]["IBD_CR_Cust_Abbr"].ToString();
            txt_IBD_CR_Cust_Acc.Text = dt.Rows[0]["IBD_CR_Cust_Acc_No"].ToString();
            txt_IBD_CR_Acceptance_Curr.Text = dt.Rows[0]["IBD_CR_Acceptance_Curr"].ToString();
            txt_IBD_CR_Acceptance_amt.Text = dt.Rows[0]["IBD_CR_Acceptance_Amount"].ToString();
            txt_IBD_CR_Acceptance_payer.Text = dt.Rows[0]["IBD_CR_Acceptance_Payer"].ToString();

            txt_IBD_CR_Interest_Curr.Text = dt.Rows[0]["IBD_CR_Interest_Curr"].ToString();
            txt_IBD_CR_Interest_amt.Text = dt.Rows[0]["IBD_CR_Interest_Amount"].ToString();
            txt_IBD_CR_Interest_payer.Text = dt.Rows[0]["IBD_CR_Interest_Payer"].ToString();

            txt_IBD_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IBD_CR_Acceptance_Comm_Curr"].ToString();
            txt_IBD_CR_Accept_Commission_amt.Text = dt.Rows[0]["IBD_CR_Acceptance_Comm_Amount"].ToString();
            txt_IBD_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IBD_CR_Acceptance_Comm_Payer"].ToString();

            txt_IBD_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IBD_CR_Pay_Handle_Comm_Curr"].ToString();
            txt_IBD_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["IBD_CR_Pay_Handle_Comm_Amount"].ToString();
            txt_IBD_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IBD_CR_Pay_Handle_Comm_Payer"].ToString();

            txt_IBD_CR_Others_Curr.Text = dt.Rows[0]["IBD_CR_Others_Curr"].ToString();
            txt_IBD_CR_Others_amt.Text = dt.Rows[0]["IBD_CR_Others_Amount"].ToString();
            txt_IBD_CR_Others_Payer.Text = dt.Rows[0]["IBD_CR_Others_Payer"].ToString();

            txt_IBD_CR_Their_Commission_Curr.Text = dt.Rows[0]["IBD_CR_Their_Comm_Curr"].ToString();
            txt_IBD_CR_Their_Commission_amt.Text = dt.Rows[0]["IBD_CR_Their_Comm_Amount"].ToString();
            txt_IBD_CR_Their_Commission_Payer.Text = dt.Rows[0]["IBD_CR_Their_Comm_Payer"].ToString();

            txt_IBD_IBD_DR_Code.Text = dt.Rows[0]["IBD_DR_IBDCode"].ToString();
            txt_IBD_IBD_DR_Cust_abbr.Text = dt.Rows[0]["IBD_DR_IBDCust_abbr"].ToString();
            txt_IBD_IBD_DR_Cust_Acc.Text = dt.Rows[0]["IBD_DR_IBDCust_Acc"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IBD_DR_IBDCur_Acc_Curr"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_amt.Text = dt.Rows[0]["IBD_DR_IBDCur_Acc_amt"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_payer.Text = dt.Rows[0]["IBD_DR_IBDCur_Acc_Payr"].ToString();

            if (hdnIBDInterest_MATU.Value == "2")
            {
                panal_IBD_DRdetails.Visible = true;

                txt_IBD_DR_Code.Text = dt.Rows[0]["IBD_DR_CODE"].ToString();
                txt_IBD_DR_Cust_abbr.Text = dt.Rows[0]["IBD_DR_Cust_abbr"].ToString();
                txt_IBD_DR_Cust_Acc.Text = dt.Rows[0]["IBD_DR_Cust_Acc"].ToString();
                txt_IBD_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IBD_DR_Current_Acc_Curr"].ToString();
                txt_IBD_DR_Cur_Acc_amt.Text = dt.Rows[0]["IBD_DR_Current_Acc_Amount"].ToString();
                txt_IBD_DR_Cur_Acc_payer.Text = dt.Rows[0]["IBD_DR_Current_Acc_Payer"].ToString();
            }
            txt_IBD_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IBD_DR_Current_Acc_Curr2"].ToString();
            txt_IBD_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IBD_DR_Current_Acc_Amount2"].ToString();
            txt_IBD_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IBD_DR_Current_Acc_Payer2"].ToString();

            txt_IBD_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IBD_DR_Current_Acc_Curr3"].ToString();
            txt_IBD_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IBD_DR_Current_Acc_Amount3"].ToString();
            txt_IBD_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IBD_DR_Current_Acc_Payer3"].ToString();

            if (dt.Rows[0]["IBD_Extn_Flag"].ToString() == "Y")
            {
                chk_IBDExtn_Flag.Checked = true;
            }

            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
        }
    }
    private void ToggleDocType(string DocType, string DocFLC_ILC_Type)
    {
        switch (DocType)
        {
            case "ACC": //LodgmentUnderLC_Usance
                string Foreign_Local = "";
                if (DocFLC_ILC_Type == "FLC")
                {
                    Foreign_Local = "Foreign";
                    txtsettlCodeForCust.Text = "29";
                    txt_IBD_settlCodeForCust.Text = "29";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "Local";
                    txtsettlCodeForCust.Text = "23";
                    txt_IBD_settlCodeForCust.Text = "23";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblLCDescount_Lodgment_UnderLC.Text = "LCDiscount_Under_LC";
                lblSight_Usance.Text = "Usance";
                txtSettl_CodeForBank.Text = "09";
                txt_IBD_Settl_CodeForBank.Text = "09";

                break;
        }
    }
    protected void btnIBD_EXTENSION_Click(object sender, EventArgs e)
    {
        if (hdnDocScrutiny.Value == "Clean")
        {
            Response.Redirect("TF_IMP_LC_DESCOUNTING_ACC_IBD_Checker.aspx?DocNo=" + txtDocNo.Text + "&DocType=" + hdnDocType.Value.Trim() + "&BranchName=" + hdnBranchName.Value.Trim() +
                "&DocScrutiny=" + hdnDocScrutiny.Value.Trim() + "&IBDDocument_No= &IBDExtnDocNo=" + txtIBDDocNo.Text + "&IBDExtn=Y");
        }
        else
        {
            Response.Redirect("TF_IMP_LC_DESCOUNTING_DISCREPANT_ACC_IBD_Checker.aspx?DocNo=" + txtDocNo.Text + "&DocType=" + hdnDocType.Value.Trim() + "&BranchName=" + hdnBranchName.Value.Trim() +
                 "&DocScrutiny=" + hdnDocScrutiny.Value.Trim() + "&IBDDocument_No= &IBDExtnDocNo=" + txtIBDDocNo.Text + "&IBDExtn=Y");
        }
    }
    public void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            if (chk_AccDocDetails.Checked == true && chk_AccImpAccounting.Checked == true)
            {
                GBaseFileCreation();
            }
            if (chk_GO1_Flag.Checked == true)
            {
                GBaseFileCreationGeneralOperation1();
            }
            if (chk_GO2_Flag.Checked == true)
            {
                GBaseFileCreationGeneralOperation2();
            }
            GBaseFileCreation_IBD();

        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        SqlParameter PIBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDDocNo.Text.ToString());

        string Result = obj.SaveDeleteData("TF_IMP_IBD_settlement_Checker_ApproveReject", P_DocNo, P_Status, P_RejectReason, PIBDDocument_No_Extn);

        Response.Redirect("TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());

        DataTable dt = objData1.getData("TF_IMP_IBD_Settlement_GbaseFileCreation", PDocNo, PIBD_DocNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_GBase" + ".xlsx";
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
    public void GBaseFileCreation_IBD()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
        DataTable dt = objData1.getData("TF_IMP_IBD_Settlement_GbaseFileCreation_IBD", PDocNo, PIBD_DocNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_GBase_IBD" + ".xlsx";
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
    public void GBaseFileCreationGeneralOperation1()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
        DataTable dt = objData1.getData("TF_IMP_IBD_Settlement_GbaseFileCreation_GO1", PDocNo, PIBD_DocNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_GBase_GO1" + ".xlsx";
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
                //string link = "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
                //lblFolderLink.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
        }
        else
        {
        }
    }
    public void GBaseFileCreationGeneralOperation2()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
        DataTable dt = objData1.getData("TF_IMP_IBD_Settlement_GbaseFileCreation_GO2", PDocNo, PIBD_DocNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_GBase_GO2" + ".xlsx";
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
                //string link = "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
                //lblFolderLink.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
        }
        else
        {
        }
    }
    public void Get_Acceptance_Details() {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_IBD_Settlement_Get_Acceptance_Details", PDocNo);
        if (dt.Rows.Count > 0)
        {
            ToggleDocType(dt.Rows[0]["Document_Type"].ToString(), dt.Rows[0]["Document_FLC_ILC"].ToString());
            //txtIBDDocNo.Text = Request.QueryString["IBDDocNo"].Trim();

            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            chk_AccDocDetails.Checked = true;
            Panel_AccDocDetails.Visible = true;

            txtCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtMaturityDate.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            txtInterest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();
            txtInterest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            txt_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();


            chk_AccImpAccounting.Checked = true;
            Panel_AccImpAccounting.Visible = true;
            txt_DiscAmt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txt_IMP_ACC_ExchRate.Text = dt.Rows[0]["IMP_ACC_ExchRate"].ToString();

            txt_CR_Code.Text = dt.Rows[0]["CR_Code"].ToString();
            txt_CR_Cust_abbr.Text = dt.Rows[0]["CR_Cust_Abbr"].ToString();
            txt_CR_Cust_Acc.Text = dt.Rows[0]["CR_Cust_Acc_No"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Acceptance_Curr"].ToString();
            //txt_CR_Acceptance_payer.Text = dt.Rows[0]["CR_Acceptance_Payer"].ToString();

            //txt_CR_Interest_amt.Text = dt.Rows[0]["CR_Interest_Amount"].ToString();
            //txt_CR_Interest_Curr.Text = dt.Rows[0]["CR_Interest_Curr"].ToString();
            //txt_CR_Interest_payer.Text = dt.Rows[0]["CR_Interest_Payer"].ToString();

            //txt_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Amount"].ToString();
            //txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Curr"].ToString();
            //txt_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Payer"].ToString();

            //txt_CR_Accept_Commission_amt.Text = dt.Rows[0]["CR_Acceptance_Comm_Amount"].ToString();
            //txt_CR_Others_amt.Text = dt.Rows[0]["CR_Others_Amount"].ToString();
            //txt_CR_Their_Commission_amt.Text = dt.Rows[0]["CR_Their_Comm_Amount"].ToString();

            txt_DR_Code.Text = dt.Rows[0]["DR_Code"].ToString();
            txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_Abbr"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc_No"].ToString();
            txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["Draft_Amt"].ToString();

            //txt_CR_Accept_Commission_Curr.Text = dt.Rows[0]["CR_Acceptance_Comm_Curr"].ToString();
            //txt_CR_Others_Curr.Text = dt.Rows[0]["CR_Others_Curr"].ToString();
            //txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["CR_Their_Comm_Curr"].ToString();

            //txt_CR_Accept_Commission_Payer.Text = dt.Rows[0]["CR_Acceptance_Comm_Payer"].ToString();
            //txt_CR_Others_Payer.Text = dt.Rows[0]["CR_Others_Payer"].ToString();
            //txt_CR_Their_Commission_Payer.Text = dt.Rows[0]["CR_Their_Comm_Payer"].ToString();
            //txt_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_Current_Acc_Payer"].ToString();
        }
    }
    public void Get_IBD_Lodgment_Details()
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt_IBD = new DataTable();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
        dt_IBD = obj.getData("TF_IMP_IBD_Settlement_Get_IBD_Lodgment_Details", PDocNo, PIBD_DocNo);
        if (dt_IBD.Rows.Count > 0)
        {
            //////------------------IBD Doc Details----------------
            //txtIBDDocNo.Text = dt_IBD.Rows[0]["IBDDocument_No"].ToString();
            txt_IBD_CustName.Text = dt_IBD.Rows[0]["CustomerName"].ToString();
            txt_IBD_MaturityDate.Text = dt_IBD.Rows[0]["Interest_To_Date"].ToString();
            txt_IBD_Interest_From.Text = dt_IBD.Rows[0]["Interest_From_Date"].ToString();
            txt_IBD_Interest_To.Text = dt_IBD.Rows[0]["Interest_To_Date"].ToString();
            txt_IBD__No_Of_Days.Text = dt_IBD.Rows[0]["No_Of_Days"].ToString();

            txt_IBD_DiscAmt.Text = dt_IBD.Rows[0]["IMP_ACC_Amount"].ToString();
            txt_IBD_IMP_ACC_ExchRate.Text = dt_IBD.Rows[0]["IMP_ACC_ExchRate"].ToString();

            txt_IBD_CR_Code.Text = dt_IBD.Rows[0]["DR_IBDCode"].ToString();
            txt_IBD_CR_AC_ShortName.Text = "I.B.D";
            txt_IBD_CR_Cust_abbr.Text = dt_IBD.Rows[0]["DR_IBDCust_abbr"].ToString();
            txt_IBD_CR_Cust_Acc.Text = dt_IBD.Rows[0]["DR_IBDCust_Acc"].ToString();
            txt_IBD_CR_Acceptance_Curr.Text = dt_IBD.Rows[0]["DR_IBDCur_Acc_Curr"].ToString();
            txt_IBD_CR_Acceptance_amt.Text = dt_IBD.Rows[0]["DR_IBDCur_Acc_amt"].ToString();
            //txt_IBD_CR_Acceptance_payer.Text = dt_IBD.Rows[0]["DR_IBDCur_Acc_Payr"].ToString();

            //txt_IBD_CR_Interest_amt.Text = dt_IBD.Rows[0]["CR_Interest_Amount"].ToString();
            //txt_IBD_CR_Interest_Curr.Text = dt_IBD.Rows[0]["CR_Interest_Curr"].ToString();
            //txt_IBD_CR_Interest_payer.Text = dt_IBD.Rows[0]["CR_Interest_Payer"].ToString();

            txt_IBD_CR_Accept_Commission_amt.Text = dt_IBD.Rows[0]["CR_Acceptance_Comm_Amount"].ToString();
            txt_IBD_CR_Accept_Commission_Curr.Text = dt_IBD.Rows[0]["CR_Acceptance_Comm_Curr"].ToString();
            //txt_IBD_CR_Accept_Commission_Payer.Text = dt_IBD.Rows[0]["CR_Acceptance_Comm_Payer"].ToString();

            txt_IBD_CR_Pay_Handle_Commission_amt.Text = dt_IBD.Rows[0]["CR_Pay_Handle_Comm_Amount"].ToString();
            txt_IBD_CR_Pay_Handle_Commission_Curr.Text = dt_IBD.Rows[0]["CR_Pay_Handle_Comm_Curr"].ToString();
            //txt_IBD_CR_Pay_Handle_Commission_Payer.Text = dt_IBD.Rows[0]["CR_Pay_Handle_Comm_Payer"].ToString();

            txt_IBD_CR_Others_amt.Text = dt_IBD.Rows[0]["CR_Others_Amount"].ToString();
            txt_IBD_CR_Others_Curr.Text = dt_IBD.Rows[0]["CR_Others_Curr"].ToString();
            //txt_IBD_CR_Others_Payer.Text = dt_IBD.Rows[0]["CR_Others_Payer"].ToString();

            txt_IBD_CR_Their_Commission_amt.Text = dt_IBD.Rows[0]["CR_Their_Comm_Amount"].ToString();
            txt_IBD_CR_Their_Commission_Curr.Text = dt_IBD.Rows[0]["CR_Their_Comm_Curr"].ToString();
            //txt_IBD_CR_Their_Commission_Payer.Text = dt_IBD.Rows[0]["CR_Their_Comm_Payer"].ToString();

            txt_IBD_IBD_DR_Code.Text = dt_IBD.Rows[0]["CR_Code"].ToString();
            txt_IBD_IBD_DR_Cust_abbr.Text = dt_IBD.Rows[0]["CR_Cust_Abbr"].ToString();
            txt_IBD_IBD_DR_Cust_Acc.Text = dt_IBD.Rows[0]["CR_Cust_Acc_No"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_Curr.Text = dt_IBD.Rows[0]["CR_Acceptance_Curr"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_amt.Text = dt_IBD.Rows[0]["CR_Acceptance_Amount"].ToString();
            //txt_IBD_IBD_DR_Cur_Acc_payer.Text = dt_IBD.Rows[0]["CR_Acceptance_Payer"].ToString();

            if (dt_IBD.Rows[0]["Interest_MATU"].ToString() == "2")
            {
                hdnIBDInterest_MATU.Value = dt_IBD.Rows[0]["Interest_MATU"].ToString();
                panal_IBD_DRdetails.Visible = true;

                txt_IBD_DR_Code.Text = dt_IBD.Rows[0]["CR_Code"].ToString();
                txt_IBD_DR_Cust_abbr.Text = dt_IBD.Rows[0]["CR_Cust_Abbr"].ToString();
                txt_IBD_DR_Cust_Acc.Text = dt_IBD.Rows[0]["CR_Cust_Acc_No"].ToString();
                txt_IBD_DR_Cur_Acc_Curr.Text = dt_IBD.Rows[0]["CR_Interest_Curr"].ToString();
                txt_IBD_DR_Cur_Acc_amt.Text = dt_IBD.Rows[0]["CR_Interest_Amount"].ToString();
                //txt_IBD_DR_Cur_Acc_payer.Text = dt_IBD.Rows[0]["CR_Acceptance_Payer"].ToString();
            }
        }
    }
}