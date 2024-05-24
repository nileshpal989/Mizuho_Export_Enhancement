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


public partial class IMP_Transactions_TF_IMP_LC_DESCOUNTING_DISCREPANT_ACC_IBD_Maker : System.Web.UI.Page
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
                TabContainerMain.ActiveTab = tbDocumentDetails;
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_LC_DESCOUNTING_ACC_IBD_Maker_View.aspx", true);
                }
                else
                {
                    MakeReadOnly();
                    txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtvalueDate1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtInterest_From.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txt_R42_ValueDate_4488.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    hdnUserName.Value = Session["userName"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    hdnIBDExtnFlag.Value = "";
                    txtIBDExtnNo.Text = "";
                    if (Request.QueryString["IBDExtn"] != null)
                    {
                        if (Request.QueryString["IBDExtn"].ToString().Trim() == "Y")
                        {
                            hdnIBDExtnFlag.Value = "Y";
                            PanelIBDExtn.Visible = true;
                            txtIBDExtnNo.Text = Request.QueryString["IBDExtnDocNo"].ToString();
                            tblR42format.Visible = false;
                        }
                    }
                    txtIBD_ACC_kind.Text = "2";
                    txtAutoSettlement.Text = "0";
                    txtCountryRisk.Text = "IN";
                    txtGradeCode.Text = "99";
                    txtPurpose.Text = "C";
                    txtFundType.Text = "1";
                    txtBaseRate.Text = "11";
                    txt_IBD_DR_Code.Text = "10607";

                    TF_DATA obj = new TF_DATA();
                    SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
                    SqlParameter PIBD_DocNo = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
                    string result = "";
                    result = obj.SaveDeleteData("TF_IMP_Check_IBDRef_ONLCDiscounting", PDocNo, PIBD_DocNo);
                    if (result == "exists")
                    {
                        hdnMode.Value = "Edit";
                    }
                    else
                    {
                        hdnMode.Value = "Add";
                    }

                    if (hdnMode.Value == "Edit")
                    {
                        Fill_Logd_Accept_Details();
                        Fill_LCDiscountingDetails();
                    }
                    else
                    {
                        Fill_Logd_Accept_Details();
                    }
                    Get_LCDescounting_Get_Date_Diff(null, null);
                }
                txtExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtApplBR.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_INT_Rate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtBaseRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSpread.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterestAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFundType.Attributes.Add("onkeydown", "return validate_Number(event);");
                Togglerdbonappbeni1();

            }
        }
    }
    [WebMethod]
    public static string AddUpdateONLCDiscount(string _hdnUserName, string _BranchName, string _txtDocNo, string _txtIBDDocNo, string _Document_Curr,
        string _hdnIBDExtnFlag, string _txtIBDExtnNo,
    string _txtDraftAmt, string _txtIBDAmt, string _txt_DiscAmt,
        //------------------------Document Details------------------------------------------------------------------------------------
    string _txtCustName, string _txtHO_Apl, string _txtIBD_ACC_kind, string _txtvalueDate1, string _txtValueDate, String _applibeniStatus,
    string _txtCommentCode, string _txtAutoSettlement, string _txtContractNo, string _txtExchRate,
    string _txtCountryRisk, string _txtRiskCust,
    string _txtGradeCode, string _txtApplNo, string _txtApplBR, string _txtPurpose, string _ddl_PurposeCode,
    string _txtsettlCodeForCust, string _txtsettlforCust_Abbr, string _txtsettlforCust_AccCode, string _txtsettlforCust_AccNo,
    string _txtInterest_From, string _txtInterest_To, string _txt_No_Of_Days,
    string _txt_INT_Rate,
    string _txtBaseRate, string _txtSpread, string _txtInterestAmt, string _txtFundType,
    string _txtInternalRate, string _txtSettl_CodeForBank, string _txtSettl_ForBank_Abbr,
    string _txtSettl_ForBank_AccCode, string _txtSettl_ForBank_AccNo,
    string _txtAttn, string _txtREM_EUC,

    ////Instruction
    string _txt_INST_Code, string _lbl_Instructions1,
        //        ////  Import Accounting
    string _txt_IMP_ACC_ExchRate, string _txtPrinc_matu, string _txtInterest_matu, string _txtCommission_matu, string _txtTheir_Commission_matu,
    string _txtPrinc_lump, string _txtInterest_lump, string _txtCommission_lump, string _txtTheir_Commission_lump,
    string _txtprinc_Contract_no, string _txtInterest_Contract_no, string _txtCommission_Contract_no, string _txtTheir_Commission_Contract_no,
    string _txtprinc_Ex_rate, string _txtInterest_Ex_rate, string _txtCommission_Ex_rate, string _txtTheir_Commission_Ex_rate,
    string _txtprinc_Intnl_Ex_rate, string _txtInterest_Intnl_Ex_rate, string _txtCommission_Intnl_Ex_rate, string _txtTheir_Commission_Intnl_Ex_rate,
    string _txt_CR_Code,
    string _txt_CR_AC_ShortName,
    string _txt_CR_Cust_abbr, string _txt_CR_Cust_Acc,
    string _txt_CR_Acceptance_amt, string _txt_CR_Interest_amt, string _txt_CR_Accept_Commission_amt,
    string _txt_CR_Pay_Handle_Commission_amt, string _txt_CR_Others_amt, string _txt_CR_Their_Commission_amt,
    string _txt_Princ_Ex_Curr, string _txt_interest_Ex_Curr, string _txt_Commission_Ex_Curr, string _txt_Their_Commission_Ex_Curr,
    string _txt_CR_Acceptance_Curr, string _txt_CR_Interest_Curr, string _txt_CR_Accept_Commission_Curr, string _txt_CR_Pay_Handle_Commission_Curr,
    string _txt_CR_Others_Curr, string _txt_CR_Their_Commission_Curr, string _txt_DR_Cur_Acc_Curr2,
    string _txt_DR_Cur_Acc_Curr3,
    string _txt_CR_Acceptance_payer, string _txt_CR_Interest_payer, string _txt_CR_Accept_Commission_Payer,
    string _txt_CR_Pay_Handle_Commission_Payer,
    string _txt_CR_Others_Payer, string _txt_CR_Their_Commission_Payer,
    string _txt_IBD_DR_Code,
        //string _txt_IBD_DR_AC_ShortName,
    string _txt_IBD_DR_Cust_abbr, string _txt_IBD_DR_Cust_Acc,
    string _txt_IBD_DR_Cur_Acc_Curr, string _txt_IBD_DR_Cur_Acc_amt,
    string _txt_IBD_DR_Cur_Acc_payer,
    string _txt_DR_Code, string _txt_DR_Cust_abbr, string _txt_DR_Cust_Acc, string _txt_DR_Cur_Acc_Curr,
    string _txt_DR_Cur_Acc_amt, string _txt_DR_Cur_Acc_payer,
    string _txt_DR_Cur_Acc_amt2, string _txt_DR_Cur_Acc_amt3,
    string _txt_DR_Cur_Acc_payer2, string _txt_DR_Cur_Acc_payer3,
        //----------------------Genral  Operation Branch-----------------------------------------------------------------
    string _cb_GOBranch_Bill_Handling_Flag,
    string _txt_GOBR_Ref_No,
    string _txt_GOBR_Comment, string _txt_GOBR_SectionNo, string _txt_GOBR_Remarks, string _txt_GOBR_Memo,
    string _txt_GOBR_Scheme_no, string _txt_GOBR_Debit_Code, string _txt_GOBR_Debit_Curr, string _txt_GOBR_Debit_Amt, string _txt_GOBR_Debit_Cust, string _txt_GOBR_Debit_Cust_AcCode,
    string _txt_GOBR_Debit_Cust_AccNo, string _txt_GOBR_Debit_Exch_Rate, string _txt_GOBR_Debit_Exch_CCY, string _txt_GOBR_Debit_FUND, string _txt_GOBR_Debit_Check_No,
    string _txt_GOBR_Debit_Available, string _txt_GOBR_Debit_AdPrint, string _txt_GOBR_Debit_Details, string _txt_GOBR_Debit_Entity, string _txt_GOBR_Debit_Division,
    string _txt_GOBR_Debit_Inter_Amount, string _txt_GOBR_Debit_Inter_Rate, string _txt_GOBR_Credit_Code, string _txt_GOBR_Credit_Curr, string _txt_GOBR_Credit_Amt,
    string _txt_GOBR_Credit_Cust, string _txt_GOBR_Credit_Cust_AcCode, string _txt_GOBR_Credit_Cust_AccNo, string _txt_GOBR_Credit_Exch_Rate, string _txt_GOBR_Credit_Exch_Curr,
    string _txt_GOBR_Credit_FUND, string _txt_GOBR_Credit_Check_No, string _txt_GOBR_Credit_Available, string _txt_GOBR_Credit_AdPrint, string _txt_GOBR_Credit_Details,
    string _txt_GOBR_Credit_Entity, string _txt_GOBR_Credit_Division, string _txt_GOBR_Credit_Inter_Amount, string _txt_GOBR_Credit_Inter_Rate,
        //----------------------Genral  Operation I-------------------------------------------------------------
    string _chk_GO1_Flag,
    string _txt_GO1_Comment, string _txt_GO1_SectionNo, string _txt_GO1_Remarks, string _txt_GO1_Memo,
    string _txt_GO1_Scheme_no, string _txt_GO1_Debit_Code, string _txt_GO1_Debit_Curr, string _txt_GO1_Debit_Amt, string _txt_GO1_Debit_Cust, string _txt_GO1_Debit_Cust_AcCode,
    string _txt_GO1_Debit_Cust_AccNo, string _txt_GO1_Debit_Exch_Rate, string _txt_GO1_Debit_Exch_CCY, string _txt_GO1_Debit_FUND, string _txt_GO1_Debit_Check_No,
    string _txt_GO1_Debit_Available, string _txt_GO1_Debit_AdPrint, string _txt_GO1_Debit_Details, string _txt_GO1_Debit_Entity, string _txt_GO1_Debit_Division,
    string _txt_GO1_Debit_Inter_Amount, string _txt_GO1_Debit_Inter_Rate, string _txt_GO1_Credit_Code, string _txt_GO1_Credit_Curr, string _txt_GO1_Credit_Amt,
    string _txt_GO1_Credit_Cust, string _txt_GO1_Credit_Cust_AcCode, string _txt_GO1_Credit_Cust_AccNo, string _txt_GO1_Credit_Exch_Rate, string _txt_GO1_Credit_Exch_Curr,
    string _txt_GO1_Credit_FUND, string _txt_GO1_Credit_Check_No, string _txt_GO1_Credit_Available, string _txt_GO1_Credit_AdPrint, string _txt_GO1_Credit_Details,
    string _txt_GO1_Credit_Entity, string _txt_GO1_Credit_Division, string _txt_GO1_Credit_Inter_Amount, string _txt_GO1_Credit_Inter_Rate,
        //--------------------Genral operationII------------------------------------------------------------------------------------------------------
    string _chk_GO2_Flag,
    string _txt_GO2_Comment, string _txt_GO2_SectionNo, string _txt_GO2_Remarks, string _txt_GO2_Memo,
    string _txt_GO2_Scheme_no, string _txt_GO2_Debit_Code, string _txt_GO2_Debit_Curr, string _txt_GO2_Debit_Amt, string _txt_GO2_Debit_Cust, string _txt_GO2_Debit_Cust_AcCode,
    string _txt_GO2_Debit_Cust_AccNo, string _txt_GO2_Debit_Exch_Rate, string _txt_GO2_Debit_Exch_CCY, string _txt_GO2_Debit_FUND, string _txt_GO2_Debit_Check_No,
    string _txt_GO2_Debit_Available, string _txt_GO2_Debit_AdPrint, string _txt_GO2_Debit_Details, string _txt_GO2_Debit_Entity, string _txt_GO2_Debit_Division,
    string _txt_GO2_Debit_Inter_Amount, string _txt_GO2_Debit_Inter_Rate, string _txt_GO2_Credit_Code, string _txt_GO2_Credit_Curr, string _txt_GO2_Credit_Amt,
    string _txt_GO2_Credit_Cust, string _txt_GO2_Credit_Cust_AcCode, string _txt_GO2_Credit_Cust_AccNo, string _txt_GO2_Credit_Exch_Rate, string _txt_GO2_Credit_Exch_Curr,
    string _txt_GO2_Credit_FUND, string _txt_GO2_Credit_Check_No, string _txt_GO2_Credit_Available, string _txt_GO2_Credit_AdPrint, string _txt_GO2_Credit_Details,
    string _txt_GO2_Credit_Entity, string _txt_GO2_Credit_Division, string _txt_GO2_Credit_Inter_Amount, string _txt_GO2_Credit_Inter_Rate,

    //-----------------------R42Format For IBD-------------------------------------------------
    string _txt_R42_tansactionRefNO, string _txt_R42_RelatedRef, string _txt_R42_ValueDate_4488, string _txt_R42_Curr_4488, string _txt_R42_Amt_4488,
    string _txt_R42_Orderingins_IFSC_5517, string _txt_R42_Benificiary_IFSC_6521,
    string _txt_R42_CodeWord_7495, string _txt_R42_AddInfo_7495, string _txt_R42_MoreInfo_7495, string _txt_R42_MoreInfo2_7495, string _txt_R42_MoreInfo3_7495,
    string _txt_R42_MoreInfo4_7495, string _txt_R42_MoreInfo5_7495
    )
    {
        TF_DATA obj = new TF_DATA();

        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName.ToUpper());
        SqlParameter P_txtDocNo = new SqlParameter("@Document_No", _txtDocNo.ToUpper());
        SqlParameter P_txtIBDDocNo = new SqlParameter("@IBDDocNo", _txtIBDDocNo.ToUpper());
        SqlParameter P_Document_Curr = new SqlParameter("@Document_Curr", _Document_Curr.ToUpper());

        SqlParameter P_IBD_Extn_Flag = new SqlParameter("@IBD_Extn_Flag", _hdnIBDExtnFlag.ToUpper());
        SqlParameter P_IBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", _txtIBDExtnNo.ToUpper());

        SqlParameter P_txtDraftAmt = new SqlParameter("@Draft_Amt", _txtDraftAmt.ToUpper());
        SqlParameter P_txtIBDAmt = new SqlParameter("@IBD_Amt", _txtIBDAmt.ToUpper());
        SqlParameter P_txt_DiscAmt = new SqlParameter("@IMP_ACC_Amount", _txt_DiscAmt.ToUpper());

        SqlParameter p_txtCustName = new SqlParameter("@CustomerName", _txtCustName.ToUpper());
        SqlParameter P_txtHO_Apl = new SqlParameter("@HO_Appl", _txtHO_Apl.ToUpper());
        SqlParameter P_txtIBD_ACC_kind = new SqlParameter("@IBD_ACC_Kind", _txtIBD_ACC_kind.ToUpper());
        SqlParameter P_txtvalueDate1 = new SqlParameter("@Value_Date1", _txtvalueDate1.ToUpper());
        SqlParameter P_txtValueDate = new SqlParameter("@Acceptance_Value_Date", _txtValueDate.ToUpper());
        SqlParameter P_applibeniStatus = new SqlParameter("@IntapplicantBeniStatus", _applibeniStatus.ToUpper());
        SqlParameter P_txtCommentCode = new SqlParameter("@Comment_Code", _txtCommentCode.ToUpper());
        SqlParameter P_txtAutoSettlement = new SqlParameter("@Auto_Settl", _txtAutoSettlement.ToUpper());

        SqlParameter P_txtContractNo = new SqlParameter("@Contract_No", _txtContractNo.ToUpper());
        SqlParameter P_txtExchRate = new SqlParameter("@Exch_Rate", _txtExchRate.ToUpper());
        SqlParameter P_txtCountryRisk = new SqlParameter("@Country_Risk", _txtCountryRisk.ToUpper());
        SqlParameter P_txtRiskCust = new SqlParameter("@Risk_Cust", _txtRiskCust.ToUpper());
        SqlParameter P_txtGradeCode = new SqlParameter("@Grade_Code", _txtGradeCode.ToUpper());
        SqlParameter P_txtApplNo = new SqlParameter("@Appl_No", _txtApplNo.ToUpper());
        SqlParameter P_txtApplBR = new SqlParameter("@Appl_BR", _txtApplBR.ToUpper());
        SqlParameter P_txtPurpose = new SqlParameter("@Purpose_Type", _txtPurpose.ToUpper());
        SqlParameter P_ddl_PurposeCode = new SqlParameter("@Purpose_Code", _ddl_PurposeCode.ToUpper());

        SqlParameter P_txtsettlCodeForCust = new SqlParameter("@Settlement_For_Cust_Code", _txtsettlCodeForCust.ToUpper());
        SqlParameter P_txtsettlforCust_Abbr = new SqlParameter("@Settlement_For_Cust_Abbr", _txtsettlforCust_Abbr.ToUpper());
        SqlParameter P_txtsettlforCust_AccCode = new SqlParameter("@Settlement_For_Cust_AccCode", _txtsettlforCust_AccCode.ToUpper());
        SqlParameter P_txtsettlforCust_AccNo = new SqlParameter("@Settlement_For_Cust_AccNo", _txtsettlforCust_AccNo.ToUpper());

        SqlParameter P_txtInterest_From = new SqlParameter("@Interest_From_Date", _txtInterest_From.ToUpper());
        SqlParameter P_txtInterest_To = new SqlParameter("@Interest_To_Date", _txtInterest_To.ToUpper());
        SqlParameter P_txt_No_Of_Days = new SqlParameter("@No_Of_Days", _txt_No_Of_Days.ToUpper());

        SqlParameter P_txt_INT_Rate = new SqlParameter("@INT_Rate", _txt_INT_Rate.ToUpper());

        SqlParameter P_txtBaseRate = new SqlParameter("@Base_Rate", _txtBaseRate.ToUpper());
        SqlParameter P_txtSpread = new SqlParameter("@Spread", _txtSpread.ToUpper());
        SqlParameter P_txtInterestAmt = new SqlParameter("@Int_Amount", _txtInterestAmt.ToUpper());
        SqlParameter P_txtFundType = new SqlParameter("@Fund_type", _txtFundType.ToUpper());
        SqlParameter P_txtInternalRate = new SqlParameter("@Internal_Rate", _txtInternalRate.ToUpper());
        SqlParameter P_txtSettl_CodeForBank = new SqlParameter("@Settlement_For_Bank_Code", _txtSettl_CodeForBank.ToUpper());
        SqlParameter P_txtSettl_ForBank_Abbr = new SqlParameter("@Settl_For_Bank_Abbr", _txtSettl_ForBank_Abbr.ToUpper());
        SqlParameter P_txtSettl_ForBank_AccCode = new SqlParameter("@Settl_ForBank_AccCode", _txtSettl_ForBank_AccCode.ToUpper());
        SqlParameter P_txtSettl_ForBank_AccNo = new SqlParameter("@Settl_ForBank_AccNo", _txtSettl_ForBank_AccNo.ToUpper());
        SqlParameter P_txtAttn = new SqlParameter("@Attn", _txtAttn.ToUpper());
        SqlParameter P_txtREM_EUC = new SqlParameter("@REM_EUC", _txtREM_EUC.ToUpper());

        //Instruction
        SqlParameter P_txt_INST_Code = new SqlParameter("@INST_Code", _txt_INST_Code.ToUpper());
        SqlParameter P_lbl_Instructions1 = new SqlParameter("@Instructions1", _lbl_Instructions1.ToUpper());

        //Import accounting
        SqlParameter P_txt_IMP_ACC_ExchRate = new SqlParameter("@IMP_ACC_ExchRate", _txt_IMP_ACC_ExchRate.ToUpper());
        SqlParameter P_txtPrinc_matu = new SqlParameter("@Principal_MATU", _txtPrinc_matu.ToUpper());
        SqlParameter P_txtInterest_matu = new SqlParameter("@Interest_MATU", _txtInterest_matu.ToUpper());
        SqlParameter P_txtCommission_matu = new SqlParameter("@Commission_MATU", _txtCommission_matu.ToUpper());
        SqlParameter P_txtTheir_Commission_matu = new SqlParameter("@Their_Commission_MATU", _txtTheir_Commission_matu.ToUpper());

        SqlParameter P_txtPrinc_lump = new SqlParameter("@Principal_LUMP", _txtPrinc_lump.ToUpper());
        SqlParameter P_txtInterest_lump = new SqlParameter("@Interest_LUMP", _txtInterest_lump.ToUpper());
        SqlParameter P_txtCommission_lump = new SqlParameter("@Commission_LUMP", _txtCommission_lump.ToUpper());
        SqlParameter P_txtTheir_Commission_lump = new SqlParameter("@Their_Commission_LUMP", _txtTheir_Commission_lump.ToUpper());

        SqlParameter P_txtprinc_Contract_no = new SqlParameter("@Principal_Contract_No", _txtprinc_Contract_no.ToUpper());
        SqlParameter P_txtInterest_Contract_no = new SqlParameter("@Interest_Contract_No", _txtInterest_Contract_no.ToUpper());
        SqlParameter P_txtCommission_Contract_no = new SqlParameter("@Commission_Contract_No", _txtCommission_Contract_no.ToUpper());
        SqlParameter P_txtTheir_Commission_Contract_no = new SqlParameter("@Their_Commission_Contract_No", _txtTheir_Commission_Contract_no.ToUpper());

        SqlParameter P_txtprinc_Ex_rate = new SqlParameter("@Principal_Exch_Rate", _txtprinc_Ex_rate.ToUpper());
        SqlParameter P_txtInterest_Ex_rate = new SqlParameter("@Interest_Exch_Rate", _txtInterest_Ex_rate.ToUpper());
        SqlParameter P_txtCommission_Ex_rate = new SqlParameter("@Commission_Exch_Rate", _txtCommission_Ex_rate.ToUpper());
        SqlParameter P_txtTheir_Commission_Ex_rate = new SqlParameter("@Their_Commission_Exch_Rate", _txtTheir_Commission_Ex_rate.ToUpper());

        SqlParameter P_txtprinc_Intnl_Ex_rate = new SqlParameter("@Principal_Intnl_Exch_Rate", _txtprinc_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txtInterest_Intnl_Ex_rate = new SqlParameter("@Interest_Intnl_Exch_Rate", _txtInterest_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txtCommission_Intnl_Ex_rate = new SqlParameter("@Commission_Intnl_Exch_Rate", _txtCommission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txtTheir_Commission_Intnl_Ex_rate = new SqlParameter("@Their_Commission_Intnl_Exch_Rate", _txtTheir_Commission_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_CR_Code = new SqlParameter("@CR_Code", _txt_CR_Code.ToUpper());
        SqlParameter P_txt_CR_AC_ShortName = new SqlParameter("@CR_AC_ShortName", _txt_CR_AC_ShortName.ToUpper());
        SqlParameter P_txt_CR_Cust_abbr = new SqlParameter("@CR_Cust_Abbr", _txt_CR_Cust_abbr.ToUpper());
        SqlParameter P_txt_CR_Cust_Acc = new SqlParameter("@CR_Cust_Acc_No", _txt_CR_Cust_Acc.ToUpper());
        SqlParameter P_txt_CR_Acceptance_amt = new SqlParameter("@CR_Acceptance_Amount", _txt_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_CR_Interest_amt = new SqlParameter("@CR_Interest_Amount", _txt_CR_Interest_amt.ToUpper());
        SqlParameter P_txt_CR_Accept_Commission_amt = new SqlParameter("@CR_Acceptance_Comm_Amount", _txt_CR_Accept_Commission_amt.ToUpper());

        SqlParameter P_txt_CR_Pay_Handle_Commission_amt = new SqlParameter("@CR_Pay_Handle_Comm_Amount", _txt_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_CR_Others_amt = new SqlParameter("@CR_Others_Amount", _txt_CR_Others_amt.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_amt = new SqlParameter("@CR_Their_Comm_Amount", _txt_CR_Their_Commission_amt.ToUpper());


        SqlParameter P_txt_IBD_DR_Code = new SqlParameter("@IBD_DRCode", _txt_IBD_DR_Code.ToUpper());
        ////SqlParameter P_txt_IBD_DR_AC_ShortName = new SqlParameter("@IBD_DR_ACShortName", _txt_IBD_DR_AC_ShortName.ToUpper());
        SqlParameter P_txt_IBD_DR_Cust_abbr = new SqlParameter("@IBD_DRCust_abbr", _txt_IBD_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IBD_DR_Cust_Acc = new SqlParameter("@IBD_DRCust_Acc", _txt_IBD_DR_Cust_Acc.ToUpper());
        SqlParameter P_txt_IBD_DR_Cur_Acc_Curr = new SqlParameter("@IBD_DRCur_Acc_Curr", _txt_IBD_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_IBD_DR_Cur_Acc_amt = new SqlParameter("@IBD_DRCur_Acc_amt", _txt_IBD_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_IBD_DR_Cur_Acc_payer = new SqlParameter("@IBD_DR_CurAcc_payer", _txt_IBD_DR_Cur_Acc_payer.ToUpper());


        SqlParameter P_txt_DR_Code = new SqlParameter("@DR_CODE", _txt_DR_Code.ToUpper());
        SqlParameter P_txt_DR_Cust_abbr = new SqlParameter("@DR_Cust_abbr", _txt_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_DR_Cust_Acc = new SqlParameter("@DR_Cust_Acc", _txt_DR_Cust_Acc.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_amt = new SqlParameter("@DR_Current_Acc_Amount", _txt_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_Curr = new SqlParameter("@DR_Current_Acc_Curr", _txt_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_payer = new SqlParameter("@DR_Current_Acc_Payer", _txt_DR_Cur_Acc_payer.ToUpper());

        SqlParameter P_txt_DR_Cur_Acc_amt2 = new SqlParameter("@DR_Current_Acc_Amount2", _txt_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_amt3 = new SqlParameter("@DR_Current_Acc_Amount3", _txt_DR_Cur_Acc_amt3.ToUpper());

        SqlParameter P_txt_CR_Acceptance_payer = new SqlParameter("@CR_Acceptance_Payer", _txt_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_CR_Interest_payer = new SqlParameter("@CR_Interest_Payer", _txt_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_CR_Accept_Commission_Payer = new SqlParameter("@CR_Acceptance_Comm_Payer", _txt_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_CR_Pay_Handle_Commission_Payer = new SqlParameter("@CR_Pay_Handle_Comm_Payer", _txt_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_CR_Others_Payer = new SqlParameter("@CR_Others_Payer", _txt_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_Payer = new SqlParameter("@CR_Their_Comm_Payer", _txt_CR_Their_Commission_Payer.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_payer2 = new SqlParameter("@DR_Current_Acc_Payer2", _txt_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_payer3 = new SqlParameter("@DR_Current_Acc_Payer3", _txt_DR_Cur_Acc_payer3.ToUpper());

        SqlParameter P_txt_Princ_Ex_Curr = new SqlParameter("@Principal_Ex_Curr", _txt_Princ_Ex_Curr.ToUpper());
        SqlParameter P_txt_interest_Ex_Curr = new SqlParameter("@Interest_Ex_Curr", _txt_interest_Ex_Curr.ToUpper());
        SqlParameter P_txt_Commission_Ex_Curr = new SqlParameter("@Commission_Ex_Curr", _txt_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_Their_Commission_Ex_Curr = new SqlParameter("@Their_Commission_Ex_Curr", _txt_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_CR_Acceptance_Curr = new SqlParameter("@CR_Acceptance_Curr", _txt_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_txt_CR_Interest_Curr = new SqlParameter("@CR_Interest_Curr", _txt_CR_Interest_Curr.ToUpper());
        SqlParameter P_txt_CR_Accept_Commission_Curr = new SqlParameter("@CR_Acceptance_Comm_Curr", _txt_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_txt_CR_Pay_Handle_Commission_Curr = new SqlParameter("@CR_Pay_Handle_Comm_Curr", _txt_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_txt_CR_Others_Curr = new SqlParameter("@CR_Others_Curr", _txt_CR_Others_Curr.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_Curr = new SqlParameter("@CR_Their_Comm_Curr", _txt_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_Curr2 = new SqlParameter("@DR_Current_Acc_Curr2", _txt_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_Curr3 = new SqlParameter("@DR_Current_Acc_Curr3", _txt_DR_Cur_Acc_Curr3.ToUpper());


        /////GENERAL OPRATION Branch _txt_GOBR_Ref_No
        SqlParameter p_cb_GOBranch_Bill_Handling_Flag = new SqlParameter("@GOBR_Bill_Handling_Flag", _cb_GOBranch_Bill_Handling_Flag);
        SqlParameter P_txt_GOBR_Ref_No = new SqlParameter("@GOBR_Bill_Handling_TransRef_No", _txt_GOBR_Ref_No.ToUpper());
        SqlParameter P_txt_GOBR_Comment = new SqlParameter("@GOBR_Bill_Handling_Comment", _txt_GOBR_Comment.ToUpper());
        SqlParameter P_txt_GOBR_SectionNo = new SqlParameter("@GOBR_Bill_Handling_Section", _txt_GOBR_SectionNo.ToUpper());
        SqlParameter P_txt_GOBR_Remarks = new SqlParameter("@GOBR_Bill_Handling_Remark", _txt_GOBR_Remarks.ToUpper());
        SqlParameter P_txt_GOBR_Memo = new SqlParameter("@GOBR_Bill_Handling_Memo", _txt_GOBR_Memo.ToUpper());
        SqlParameter P_txt_GOBR_Scheme_no = new SqlParameter("@GOBR_Bill_Handling_SchemeNo", _txt_GOBR_Scheme_no.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Code = new SqlParameter("@GOBR_Bill_Handling_Debit_Code", _txt_GOBR_Debit_Code.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Curr = new SqlParameter("@GOBR_Bill_Handling_Debit_CCY", _txt_GOBR_Debit_Curr.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Amt = new SqlParameter("@GOBR_Bill_Handling_Debit_Amt", _txt_GOBR_Debit_Amt.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Cust = new SqlParameter("@GOBR_Bill_Handling_Debit_Cust_abbr", _txt_GOBR_Debit_Cust.ToUpper());

        SqlParameter P_txt_GOBR_Debit_Cust_AcCode = new SqlParameter("@GOBR_Bill_Handling_Debit_Cust_AccCode", _txt_GOBR_Debit_Cust_AcCode.ToUpper());

        SqlParameter P_txt_GOBR_Debit_Cust_AccNo = new SqlParameter("@GOBR_Bill_Handling_Debit_Cust_AccNo", _txt_GOBR_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Exch_Rate = new SqlParameter("@GOBR_Bill_Handling_Debit_ExchRate", _txt_GOBR_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Exch_CCY = new SqlParameter("@GOBR_Bill_Handling_Debit_ExchCCY", _txt_GOBR_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GOBR_Debit_FUND = new SqlParameter("@GOBR_Bill_Handling_Debit_Fund", _txt_GOBR_Debit_FUND.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Check_No = new SqlParameter("@GOBR_Bill_Handling_Debit_CheckNo", _txt_GOBR_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Available = new SqlParameter("@GOBR_Bill_Handling_Debit_Available", _txt_GOBR_Debit_Available.ToUpper());
        SqlParameter P_txt_GOBR_Debit_AdPrint = new SqlParameter("@GOBR_Bill_Handling_Debit_Advice_Print", _txt_GOBR_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Details = new SqlParameter("@GOBR_Bill_Handling_Debit_Details", _txt_GOBR_Debit_Details.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Entity = new SqlParameter("@GOBR_Bill_Handling_Debit_Entity", _txt_GOBR_Debit_Entity.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Division = new SqlParameter("@GOBR_Bill_Handling_Debit_Division", _txt_GOBR_Debit_Division.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Inter_Amount = new SqlParameter("@GOBR_Bill_Handling_Debit_InterAmt", _txt_GOBR_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GOBR_Debit_Inter_Rate = new SqlParameter("@GOBR_Bill_Handling_Debit_InterRate", _txt_GOBR_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Code = new SqlParameter("@GOBR_Bill_Handling_Credit_Code", _txt_GOBR_Credit_Code.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Curr = new SqlParameter("@GOBR_Bill_Handling_Credit_CCY", _txt_GOBR_Credit_Curr.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Amt = new SqlParameter("@GOBR_Bill_Handling_Credit_Amt", _txt_GOBR_Credit_Amt.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Cust = new SqlParameter("@GOBR_Bill_Handling_Credit_Cust_abbr", _txt_GOBR_Credit_Cust.ToUpper());

        SqlParameter P_txt_GOBR_Credit_Cust_AcCode = new SqlParameter("@GOBR_Bill_Handling_Credit_Cust_AccCode", _txt_GOBR_Credit_Cust_AcCode.ToUpper());

        SqlParameter P_txt_GOBR_Credit_Cust_AccNo = new SqlParameter("@GOBR_Bill_Handling_Credit_Cust_AccNo", _txt_GOBR_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Exch_Rate = new SqlParameter("@GOBR_Bill_Handling_Credit_ExchRate", _txt_GOBR_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Exch_Curr = new SqlParameter("@GOBR_Bill_Handling_Credit_ExchCCY", _txt_GOBR_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GOBR_Credit_FUND = new SqlParameter("@GOBR_Bill_Handling_Credit_Fund", _txt_GOBR_Credit_FUND.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Check_No = new SqlParameter("@GOBR_Bill_Handling_Credit_CheckNo", _txt_GOBR_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Available = new SqlParameter("@GOBR_Bill_Handling_Credit_Available", _txt_GOBR_Credit_Available.ToUpper());
        SqlParameter P_txt_GOBR_Credit_AdPrint = new SqlParameter("@GOBR_Bill_Handling_Credit_Advice_Print", _txt_GOBR_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Details = new SqlParameter("@GOBR_Bill_Handling_Credit_Details", _txt_GOBR_Credit_Details.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Entity = new SqlParameter("@GOBR_Bill_Handling_Credit_Entity", _txt_GOBR_Credit_Entity.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Division = new SqlParameter("@GOBR_Bill_Handling_Credit_Division", _txt_GOBR_Credit_Division.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Inter_Amount = new SqlParameter("@GOBR_Bill_Handling_Credit_InterAmt", _txt_GOBR_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GOBR_Credit_Inter_Rate = new SqlParameter("@GOBR_Bill_Handling_Credit_InterRate", _txt_GOBR_Credit_Inter_Rate.ToUpper());

        /////--------------------------------------------GENERAL OPRATION 1------------------------------------------------------------------------------------------------
        SqlParameter P_chk_GO1_Flag = new SqlParameter("@GO_Bill_Handling_Flag", _chk_GO1_Flag.ToUpper());
        SqlParameter P_txt_GO1_Comment = new SqlParameter("@GO_Bill_Handling_Comment", _txt_GO1_Comment.ToUpper());
        SqlParameter P_txt_GO1_SectionNo = new SqlParameter("@GO_Bill_Handling_Section", _txt_GO1_SectionNo.ToUpper());
        SqlParameter P_txt_GO1_Remarks = new SqlParameter("@GO_Bill_Handling_Remark", _txt_GO1_Remarks.ToUpper());
        SqlParameter P_txt_GO1_Memo = new SqlParameter("@GO_Bill_Handling_Memo", _txt_GO1_Memo.ToUpper());
        SqlParameter P_txt_GO1_Scheme_no = new SqlParameter("@GO_Bill_Handling_SchemeNo", _txt_GO1_Scheme_no.ToUpper());
        SqlParameter P_txt_GO1_Debit_Code = new SqlParameter("@GO_Bill_Handling_Debit_Code", _txt_GO1_Debit_Code.ToUpper());
        SqlParameter P_txt_GO1_Debit_Curr = new SqlParameter("@GO_Bill_Handling_Debit_CCY", _txt_GO1_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO1_Debit_Amt = new SqlParameter("@GO_Bill_Handling_Debit_Amt", _txt_GO1_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO1_Debit_Cust = new SqlParameter("@GO_Bill_Handling_Debit_Cust_abbr", _txt_GO1_Debit_Cust.ToUpper());

        SqlParameter P_txt_GO1_Debit_Cust_AcCode = new SqlParameter("@GO_Bill_Handling_Debit_Cust_AccCode", _txt_GO1_Debit_Cust_AcCode.ToUpper());

        SqlParameter P_txt_GO1_Debit_Cust_AccNo = new SqlParameter("@GO_Bill_Handling_Debit_Cust_AccNo", _txt_GO1_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO1_Debit_Exch_Rate = new SqlParameter("@GO_Bill_Handling_Debit_ExchRate", _txt_GO1_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO1_Debit_Exch_CCY = new SqlParameter("@GO_Bill_Handling_Debit_ExchCCY", _txt_GO1_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO1_Debit_FUND = new SqlParameter("@GO_Bill_Handling_Debit_Fund", _txt_GO1_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO1_Debit_Check_No = new SqlParameter("@GO_Bill_Handling_Debit_CheckNo", _txt_GO1_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO1_Debit_Available = new SqlParameter("@GO_Bill_Handling_Debit_Available", _txt_GO1_Debit_Available.ToUpper());
        SqlParameter P_txt_GO1_Debit_AdPrint = new SqlParameter("@GO_Bill_Handling_Debit_Advice_Print", _txt_GO1_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO1_Debit_Details = new SqlParameter("@GO_Bill_Handling_Debit_Details", _txt_GO1_Debit_Details.ToUpper());
        SqlParameter P_txt_GO1_Debit_Entity = new SqlParameter("@GO_Bill_Handling_Debit_Entity", _txt_GO1_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO1_Debit_Division = new SqlParameter("@GO_Bill_Handling_Debit_Division", _txt_GO1_Debit_Division.ToUpper());
        SqlParameter P_txt_GO1_Debit_Inter_Amount = new SqlParameter("@GO_Bill_Handling_Debit_InterAmt", _txt_GO1_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO1_Debit_Inter_Rate = new SqlParameter("@GO_Bill_Handling_Debit_InterRate", _txt_GO1_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO1_Credit_Code = new SqlParameter("@GO_Bill_Handling_Credit_Code", _txt_GO1_Credit_Code.ToUpper());
        SqlParameter P_txt_GO1_Credit_Curr = new SqlParameter("@GO_Bill_Handling_Credit_CCY", _txt_GO1_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO1_Credit_Amt = new SqlParameter("@GO_Bill_Handling_Credit_Amt", _txt_GO1_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO1_Credit_Cust = new SqlParameter("@GO_Bill_Handling_Credit_Cust_abbr", _txt_GO1_Credit_Cust.ToUpper());

        SqlParameter P_txt_GO1_Credit_Cust_AcCode = new SqlParameter("@GO_Bill_Handling_Credit_Cust_AccCode", _txt_GO1_Credit_Cust_AcCode.ToUpper());

        SqlParameter P_txt_GO1_Credit_Cust_AccNo = new SqlParameter("@GO_Bill_Handling_Credit_Cust_AccNo", _txt_GO1_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO1_Credit_Exch_Rate = new SqlParameter("@GO_Bill_Handling_Credit_ExchRate", _txt_GO1_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO1_Credit_Exch_Curr = new SqlParameter("@GO_Bill_Handling_Credit_ExchCCY", _txt_GO1_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO1_Credit_FUND = new SqlParameter("@GO_Bill_Handling_Credit_Fund", _txt_GO1_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO1_Credit_Check_No = new SqlParameter("@GO_Bill_Handling_Credit_CheckNo", _txt_GO1_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO1_Credit_Available = new SqlParameter("@GO_Bill_Handling_Credit_Available", _txt_GO1_Credit_Available.ToUpper());
        SqlParameter P_txt_GO1_Credit_AdPrint = new SqlParameter("@GO_Bill_Handling_Credit_Advice_Print", _txt_GO1_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO1_Credit_Details = new SqlParameter("@GO_Bill_Handling_Credit_Details", _txt_GO1_Credit_Details.ToUpper());
        SqlParameter P_txt_GO1_Credit_Entity = new SqlParameter("@GO_Bill_Handling_Credit_Entity", _txt_GO1_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO1_Credit_Division = new SqlParameter("@GO_Bill_Handling_Credit_Division", _txt_GO1_Credit_Division.ToUpper());
        SqlParameter P_txt_GO1_Credit_Inter_Amount = new SqlParameter("@GO_Bill_Handling_Credit_InterAmt", _txt_GO1_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO1_Credit_Inter_Rate = new SqlParameter("@GO_Bill_Handling_Credit_InterRate", _txt_GO1_Credit_Inter_Rate.ToUpper());

        //------------------------------------Genral operation II---------------------------------------------------------------------------------
        SqlParameter P_chk_GO2_Flag = new SqlParameter("@GO2_Bill_Handling_Flag", _chk_GO2_Flag.ToUpper());
        SqlParameter P_txt_GO2_Comment = new SqlParameter("@GO2_Bill_Handling_Comment", _txt_GO2_Comment.ToUpper());
        SqlParameter P_txt_GO2_SectionNo = new SqlParameter("@GO2_Bill_Handling_Section", _txt_GO2_SectionNo.ToUpper());
        SqlParameter P_txt_GO2_Remarks = new SqlParameter("@GO2_Bill_Handling_Remark", _txt_GO2_Remarks.ToUpper());
        SqlParameter P_txt_GO2_Memo = new SqlParameter("@GO2_Bill_Handling_Memo", _txt_GO2_Memo.ToUpper());
        SqlParameter P_txt_GO2_Scheme_no = new SqlParameter("@GO2_Bill_Handling_SchemeNo", _txt_GO2_Scheme_no.ToUpper());
        SqlParameter P_txt_GO2_Debit_Code = new SqlParameter("@GO2_Bill_Handling_Debit_Code", _txt_GO2_Debit_Code.ToUpper());
        SqlParameter P_txt_GO2_Debit_Curr = new SqlParameter("@GO2_Bill_Handling_Debit_CCY", _txt_GO2_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO2_Debit_Amt = new SqlParameter("@GO2_Bill_Handling_Debit_Amt", _txt_GO2_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO2_Debit_Cust = new SqlParameter("@GO2_Bill_Handling_Debit_Cust_abbr", _txt_GO2_Debit_Cust.ToUpper());

        SqlParameter P_txt_GO2_Debit_Cust_AcCode = new SqlParameter("@GO2_Bill_Handling_Debit_Cust_AccCode", _txt_GO2_Debit_Cust_AcCode.ToUpper());

        SqlParameter P_txt_GO2_Debit_Cust_AccNo = new SqlParameter("@GO2_Bill_Handling_Debit_Cust_AccNo", _txt_GO2_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO2_Debit_Exch_Rate = new SqlParameter("@GO2_Bill_Handling_Debit_ExchRate", _txt_GO2_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO2_Debit_Exch_CCY = new SqlParameter("@GO2_Bill_Handling_Debit_ExchCCY", _txt_GO2_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO2_Debit_FUND = new SqlParameter("@GO2_Bill_Handling_Debit_Fund", _txt_GO2_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO2_Debit_Check_No = new SqlParameter("@GO2_Bill_Handling_Debit_CheckNo", _txt_GO2_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO2_Debit_Available = new SqlParameter("@GO2_Bill_Handling_Debit_Available", _txt_GO2_Debit_Available.ToUpper());
        SqlParameter P_txt_GO2_Debit_AdPrint = new SqlParameter("@GO2_Bill_Handling_Debit_Advice_Print", _txt_GO2_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO2_Debit_Details = new SqlParameter("@GO2_Bill_Handling_Debit_Details", _txt_GO2_Debit_Details.ToUpper());
        SqlParameter P_txt_GO2_Debit_Entity = new SqlParameter("@GO2_Bill_Handling_Debit_Entity", _txt_GO2_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO2_Debit_Division = new SqlParameter("@GO2_Bill_Handling_Debit_Division", _txt_GO2_Debit_Division.ToUpper());
        SqlParameter P_txt_GO2_Debit_Inter_Amount = new SqlParameter("@GO2_Bill_Handling_Debit_InterAmt", _txt_GO2_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO2_Debit_Inter_Rate = new SqlParameter("@GO2_Bill_Handling_Debit_InterRate", _txt_GO2_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO2_Credit_Code = new SqlParameter("@GO2_Bill_Handling_Credit_Code", _txt_GO2_Credit_Code.ToUpper());
        SqlParameter P_txt_GO2_Credit_Curr = new SqlParameter("@GO2_Bill_Handling_Credit_CCY", _txt_GO2_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO2_Credit_Amt = new SqlParameter("@GO2_Bill_Handling_Credit_Amt", _txt_GO2_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO2_Credit_Cust = new SqlParameter("@GO2_Bill_Handling_Credit_Cust_abbr", _txt_GO2_Credit_Cust.ToUpper());

        SqlParameter P_txt_GO2_Credit_Cust_AcCode = new SqlParameter("@GO2_Bill_Handling_Credit_Cust_AccCode", _txt_GO2_Credit_Cust_AcCode.ToUpper());

        SqlParameter P_txt_GO2_Credit_Cust_AccNo = new SqlParameter("@GO2_Bill_Handling_Credit_Cust_AccNo", _txt_GO2_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO2_Credit_Exch_Rate = new SqlParameter("@GO2_Bill_Handling_Credit_ExchRate", _txt_GO2_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO2_Credit_Exch_Curr = new SqlParameter("@GO2_Bill_Handling_Credit_ExchCCY", _txt_GO2_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO2_Credit_FUND = new SqlParameter("@GO2_Bill_Handling_Credit_Fund", _txt_GO2_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO2_Credit_Check_No = new SqlParameter("@GO2_Bill_Handling_Credit_CheckNo", _txt_GO2_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO2_Credit_Available = new SqlParameter("@GO2_Bill_Handling_Credit_Available", _txt_GO2_Credit_Available.ToUpper());
        SqlParameter P_txt_GO2_Credit_AdPrint = new SqlParameter("@GO2_Bill_Handling_Credit_Advice_Print", _txt_GO2_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO2_Credit_Details = new SqlParameter("@GO2_Bill_Handling_Credit_Details", _txt_GO2_Credit_Details.ToUpper());
        SqlParameter P_txt_GO2_Credit_Entity = new SqlParameter("@GO2_Bill_Handling_Credit_Entity", _txt_GO2_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO2_Credit_Division = new SqlParameter("@GO2_Bill_Handling_Credit_Division", _txt_GO2_Credit_Division.ToUpper());
        SqlParameter P_txt_GO2_Credit_Inter_Amount = new SqlParameter("@GO2_Bill_Handling_Credit_InterAmt", _txt_GO2_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO2_Credit_Inter_Rate = new SqlParameter("@GO2_Bill_Handling_Credit_InterRate", _txt_GO2_Credit_Inter_Rate.ToUpper());

        //---------------------------------R42 Format for IBD-------------------------------------------------------------------------------------
        SqlParameter p_txt_R42_tansactionRefNO = new SqlParameter("@TranRefNo_2020", _txt_R42_tansactionRefNO.ToUpper());
        SqlParameter p_txt_R42_RelatedRef = new SqlParameter("@RelatedRefno_2006", _txt_R42_RelatedRef.ToUpper());
        SqlParameter p_txt_R42_ValueDate_4488 = new SqlParameter("@ValueDate_4488", _txt_R42_ValueDate_4488.ToUpper());
        SqlParameter p_txt_R42_Curr_4488 = new SqlParameter("@Curr_4488", _txt_R42_Curr_4488.ToUpper());
        SqlParameter p_txt_R42_Amt_4488 = new SqlParameter("@Amt_4488", _txt_R42_Amt_4488.ToUpper());
        SqlParameter p_txt_R42_Orderingins_IFSC_5517 = new SqlParameter("@OrderingInstitution_IFSC_5517", _txt_R42_Orderingins_IFSC_5517.ToUpper());
        SqlParameter p_txt_R42_Benificiary_IFSC_6521 = new SqlParameter("@BeneficiaryInstitution_IFSC_5517", _txt_R42_Benificiary_IFSC_6521.ToUpper());
        SqlParameter p_txt_R42_CodeWord_7495 = new SqlParameter("@CodeWord_7495", _txt_R42_CodeWord_7495.ToUpper());
        SqlParameter p_txt_R42_AddInfo_7495 = new SqlParameter("@AdditionalInfo_7495", _txt_R42_AddInfo_7495.ToUpper());
        SqlParameter p_txt_R42_MoreInfo_7495 = new SqlParameter("@MoreInfo_7495", _txt_R42_MoreInfo_7495.ToUpper());
        SqlParameter p_txt_R42_MoreInfo2_7495 = new SqlParameter("@MoreInfo2_7495", _txt_R42_MoreInfo2_7495.ToUpper());
        SqlParameter p_txt_R42_MoreInfo3_7495 = new SqlParameter("@MoreInfo3_7495", _txt_R42_MoreInfo3_7495.ToUpper());
        SqlParameter p_txt_R42_MoreInfo4_7495 = new SqlParameter("@MoreInfo4_7495", _txt_R42_MoreInfo4_7495.ToUpper());
        SqlParameter p_txt_R42_MoreInfo5_7495 = new SqlParameter("@MoreInfo5_7495", _txt_R42_MoreInfo5_7495.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", _hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", _hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdate_Discripant_LCDiscounting_IBD", P_BranchName, P_txtDocNo, P_txtIBDDocNo, P_Document_Curr,
            P_IBD_Extn_Flag, P_IBDDocument_No_Extn,
            P_txtDraftAmt, P_txtIBDAmt, P_txt_DiscAmt,
        p_txtCustName,
        P_txtHO_Apl, P_txtIBD_ACC_kind, P_txtvalueDate1, P_txtValueDate, P_applibeniStatus,
        P_txtCommentCode, P_txtAutoSettlement,
            //P_chk_Ledger_Modify, P_txtLedger_Modify_amt,
        P_txtContractNo, P_txtExchRate, P_txtCountryRisk,
        P_txtRiskCust, P_txtGradeCode,
        P_txtApplNo, P_txtApplBR, P_txtPurpose, P_ddl_PurposeCode,
        P_txtsettlCodeForCust, P_txtsettlforCust_Abbr, P_txtsettlforCust_AccCode, P_txtsettlforCust_AccNo,
        P_txtInterest_From, P_txtInterest_To, P_txt_No_Of_Days,
        P_txt_INT_Rate,

        P_txtBaseRate, P_txtSpread, P_txtInterestAmt, P_txtFundType,
        P_txtInternalRate, P_txtSettl_CodeForBank, P_txtSettl_ForBank_Abbr,
        P_txtSettl_ForBank_AccCode, P_txtSettl_ForBank_AccNo,
        P_txtAttn, P_txtREM_EUC,
            //instruction
        P_txt_INST_Code, P_lbl_Instructions1,

        //     //import accounting
        P_txt_IMP_ACC_ExchRate, P_txtPrinc_matu, P_txtInterest_matu, P_txtCommission_matu, P_txtTheir_Commission_matu,
        P_txtPrinc_lump, P_txtInterest_lump, P_txtCommission_lump, P_txtTheir_Commission_lump,
        P_txtprinc_Contract_no, P_txtInterest_Contract_no, P_txtCommission_Contract_no, P_txtTheir_Commission_Contract_no,
        P_txtprinc_Ex_rate, P_txtInterest_Ex_rate, P_txtCommission_Ex_rate, P_txtTheir_Commission_Ex_rate,

        P_txtprinc_Intnl_Ex_rate, P_txtInterest_Intnl_Ex_rate, P_txtCommission_Intnl_Ex_rate, P_txtTheir_Commission_Intnl_Ex_rate,
        P_txt_CR_Code,
        P_txt_CR_AC_ShortName,
        P_txt_CR_Cust_abbr, P_txt_CR_Cust_Acc, P_txt_CR_Acceptance_amt, P_txt_CR_Interest_amt,
        P_txt_CR_Accept_Commission_amt, P_txt_CR_Pay_Handle_Commission_amt, P_txt_CR_Others_amt, P_txt_CR_Their_Commission_amt,
        P_txt_IBD_DR_Code,
            //P_txt_IBD_DR_AC_ShortName,
        P_txt_IBD_DR_Cust_abbr, P_txt_IBD_DR_Cust_Acc, P_txt_IBD_DR_Cur_Acc_Curr,
        P_txt_IBD_DR_Cur_Acc_amt, P_txt_IBD_DR_Cur_Acc_payer,

        P_txt_DR_Code, P_txt_DR_Cust_abbr, P_txt_DR_Cust_Acc,
        P_txt_DR_Cur_Acc_amt, P_txt_DR_Cur_Acc_Curr, P_txt_DR_Cur_Acc_payer,

        P_txt_DR_Cur_Acc_amt2, P_txt_DR_Cur_Acc_amt3,

        P_txt_CR_Acceptance_payer, P_txt_CR_Interest_payer, P_txt_CR_Accept_Commission_Payer, P_txt_CR_Pay_Handle_Commission_Payer,
        P_txt_CR_Others_Payer, P_txt_CR_Their_Commission_Payer, P_txt_DR_Cur_Acc_payer2, P_txt_DR_Cur_Acc_payer3,

        P_txt_Princ_Ex_Curr, P_txt_interest_Ex_Curr, P_txt_Commission_Ex_Curr, P_txt_Their_Commission_Ex_Curr, P_txt_CR_Acceptance_Curr,
        P_txt_CR_Interest_Curr, P_txt_CR_Accept_Commission_Curr, P_txt_CR_Pay_Handle_Commission_Curr, P_txt_CR_Others_Curr, P_txt_CR_Their_Commission_Curr,
        P_txt_DR_Cur_Acc_Curr2, P_txt_DR_Cur_Acc_Curr3,

        // Genral operation Branch
        p_cb_GOBranch_Bill_Handling_Flag, P_txt_GOBR_Ref_No, P_txt_GOBR_Comment, P_txt_GOBR_SectionNo, P_txt_GOBR_Remarks, P_txt_GOBR_Memo, P_txt_GOBR_Scheme_no, P_txt_GOBR_Debit_Code, P_txt_GOBR_Debit_Curr,
        P_txt_GOBR_Debit_Amt, P_txt_GOBR_Debit_Cust, P_txt_GOBR_Debit_Cust_AcCode,
        P_txt_GOBR_Debit_Cust_AccNo, P_txt_GOBR_Debit_Exch_Rate, P_txt_GOBR_Debit_Exch_CCY, P_txt_GOBR_Debit_FUND, P_txt_GOBR_Debit_Check_No,
        P_txt_GOBR_Debit_Available, P_txt_GOBR_Debit_AdPrint, P_txt_GOBR_Debit_Details, P_txt_GOBR_Debit_Entity, P_txt_GOBR_Debit_Division, P_txt_GOBR_Debit_Inter_Amount,
        P_txt_GOBR_Debit_Inter_Rate, P_txt_GOBR_Credit_Code, P_txt_GOBR_Credit_Curr, P_txt_GOBR_Credit_Amt, P_txt_GOBR_Credit_Cust,
        P_txt_GOBR_Credit_Cust_AcCode, P_txt_GOBR_Credit_Cust_AccNo, P_txt_GOBR_Credit_Exch_Rate, P_txt_GOBR_Credit_Exch_Curr,
        P_txt_GOBR_Credit_FUND, P_txt_GOBR_Credit_Check_No, P_txt_GOBR_Credit_Available, P_txt_GOBR_Credit_AdPrint, P_txt_GOBR_Credit_Details, P_txt_GOBR_Credit_Entity,
        P_txt_GOBR_Credit_Division, P_txt_GOBR_Credit_Inter_Amount, P_txt_GOBR_Credit_Inter_Rate,


        //----------------Genral operations I----------------------------------------
        P_chk_GO1_Flag,
        P_txt_GO1_Comment, P_txt_GO1_SectionNo, P_txt_GO1_Remarks, P_txt_GO1_Memo, P_txt_GO1_Scheme_no, P_txt_GO1_Debit_Code, P_txt_GO1_Debit_Curr,
        P_txt_GO1_Debit_Amt, P_txt_GO1_Debit_Cust, P_txt_GO1_Debit_Cust_AcCode,
        P_txt_GO1_Debit_Cust_AccNo, P_txt_GO1_Debit_Exch_Rate, P_txt_GO1_Debit_Exch_CCY, P_txt_GO1_Debit_FUND, P_txt_GO1_Debit_Check_No,
        P_txt_GO1_Debit_Available, P_txt_GO1_Debit_AdPrint, P_txt_GO1_Debit_Details, P_txt_GO1_Debit_Entity, P_txt_GO1_Debit_Division, P_txt_GO1_Debit_Inter_Amount,
        P_txt_GO1_Debit_Inter_Rate, P_txt_GO1_Credit_Code, P_txt_GO1_Credit_Curr, P_txt_GO1_Credit_Amt, P_txt_GO1_Credit_Cust,
        P_txt_GO1_Credit_Cust_AcCode, P_txt_GO1_Credit_Cust_AccNo, P_txt_GO1_Credit_Exch_Rate, P_txt_GO1_Credit_Exch_Curr,
        P_txt_GO1_Credit_FUND, P_txt_GO1_Credit_Check_No, P_txt_GO1_Credit_Available, P_txt_GO1_Credit_AdPrint, P_txt_GO1_Credit_Details, P_txt_GO1_Credit_Entity,
        P_txt_GO1_Credit_Division, P_txt_GO1_Credit_Inter_Amount, P_txt_GO1_Credit_Inter_Rate,

        //----------------Genral operations II----------------------------------------
        P_chk_GO2_Flag,
        P_txt_GO2_Comment, P_txt_GO2_SectionNo, P_txt_GO2_Remarks, P_txt_GO2_Memo, P_txt_GO2_Scheme_no, P_txt_GO2_Debit_Code, P_txt_GO2_Debit_Curr,
        P_txt_GO2_Debit_Amt, P_txt_GO2_Debit_Cust, P_txt_GO2_Debit_Cust_AcCode,
        P_txt_GO2_Debit_Cust_AccNo, P_txt_GO2_Debit_Exch_Rate, P_txt_GO2_Debit_Exch_CCY, P_txt_GO2_Debit_FUND, P_txt_GO2_Debit_Check_No,
        P_txt_GO2_Debit_Available, P_txt_GO2_Debit_AdPrint, P_txt_GO2_Debit_Details, P_txt_GO2_Debit_Entity, P_txt_GO2_Debit_Division, P_txt_GO2_Debit_Inter_Amount,
        P_txt_GO2_Debit_Inter_Rate, P_txt_GO2_Credit_Code, P_txt_GO2_Credit_Curr, P_txt_GO2_Credit_Amt, P_txt_GO2_Credit_Cust,
        P_txt_GO2_Credit_Cust_AcCode, P_txt_GO2_Credit_Cust_AccNo, P_txt_GO2_Credit_Exch_Rate, P_txt_GO2_Credit_Exch_Curr,
        P_txt_GO2_Credit_FUND, P_txt_GO2_Credit_Check_No, P_txt_GO2_Credit_Available, P_txt_GO2_Credit_AdPrint, P_txt_GO2_Credit_Details, P_txt_GO2_Credit_Entity,
        P_txt_GO2_Credit_Division, P_txt_GO2_Credit_Inter_Amount, P_txt_GO2_Credit_Inter_Rate,

        //------------------------R42 Format For IBD-----------------------------------------------------------------
        p_txt_R42_tansactionRefNO, p_txt_R42_RelatedRef, p_txt_R42_ValueDate_4488, p_txt_R42_Curr_4488, p_txt_R42_Amt_4488,
        p_txt_R42_Orderingins_IFSC_5517, p_txt_R42_Benificiary_IFSC_6521,
        p_txt_R42_CodeWord_7495, p_txt_R42_AddInfo_7495,
        p_txt_R42_MoreInfo_7495, p_txt_R42_MoreInfo2_7495, p_txt_R42_MoreInfo3_7495, p_txt_R42_MoreInfo4_7495, p_txt_R42_MoreInfo5_7495,
        P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate
        );
        return _Result;
    }
    protected void txtInterest_matu_TextChanged(object sender, EventArgs e)
    {
        string TextChange = txtInterest_matu.Text;
        if (TextChange == "1")
        {
            panal_DRdetails.Visible = true;
            txt_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
        }
        else
        {
            panal_DRdetails.Visible = false;
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "script", "DebitCredit_For_IMP_ACC();", true);
        //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "DebitCredit_For_IMP_ACC();", true);
    }
    protected void Fill_Logd_Accept_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_BOE_Details_For_LCDescounting", PDocNo);
        if (dt.Rows.Count > 0)
        {
            ToggleDocType(hdnBranchName.Value, hdnDocType.Value, dt.Rows[0]["Document_FLC_ILC"].ToString());
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            Get_DiscrepencyCharges();
            hdnNegoRemiBankType.Value = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            txtsettlCodeForCust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
            txtsettlforCust_Abbr.Text = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            txtsettlforCust_AccCode.Text = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();
            txtsettlforCust_AccNo.Text = dt.Rows[0]["Settlement_For_Cust_AccNo"].ToString();

            txtSettl_CodeForBank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();
            txtSettl_ForBank_Abbr.Text = dt.Rows[0]["Settl_For_Bank_Abbr"].ToString();
            txtSettl_ForBank_AccCode.Text = dt.Rows[0]["Settl_ForBank_AccCode"].ToString();
            txtSettl_ForBank_AccNo.Text = dt.Rows[0]["Settl_ForBank_AccNo"].ToString();

            txtInterest_To.Text = dt.Rows[0]["Maturity_Date"].ToString();
            //            Import Accounting
            txt_CR_Accept_Commission_Curr.Text = "INR";
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Interest_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Others_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();

            txt_CR_Code.Text = dt.Rows[0]["Settl_ForBank_AccCode"].ToString();
            txt_CR_Cust_abbr.Text = dt.Rows[0]["Settl_For_Bank_Abbr"].ToString();
            txt_CR_Cust_Acc.Text = dt.Rows[0]["Settl_ForBank_AccNo"].ToString();

            txt_IBD_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_DR_Cur_Acc_Curr.Text = "INR";

            //---------------General Operation Brach----------------------------------------
            txt_GOBR_Debit_Cust.Text = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            txt_GOBR_Debit_Cust_AcCode.Text = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();

            //--------------General Operation Branch1---------------------------------------
            txt_GO1_Debit_Cust.Text = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();
            //--------------General Operation Branch2----------------------------------------
            txt_GO2_Debit_Cust.Text = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            txt_GO2_Debit_Cust_AcCode.Text = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();

            //R42 file
            //txt_R42_tansactionRefNO.Text = dt.Rows[0]["Transaction Reference Number"].ToString();
            txt_R42_RelatedRef.Text = dt.Rows[0]["Related Reference"].ToString();
            txt_R42_Curr_4488.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_R42_Benificiary_IFSC_6521.Text = dt.Rows[0]["IFSC_Code"].ToString();
            txt_R42_CodeWord_7495.Text = "TRF";
            txt_R42_AddInfo_7495.Text = "";
            txt_R42_MoreInfo_7495.Text = "//" + dt.Rows[0]["Drawer_NAME"].ToString();
            txt_R42_MoreInfo2_7495.Text = "//YOUR REF NO:" + dt.Rows[0]["Related Reference"].ToString();
            txt_R42_MoreInfo3_7495.Text = "//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString();
            txt_R42_MoreInfo4_7495.Text = "//" + dt.Rows[0]["Drawer_NAME"].ToString();
            txt_R42_MoreInfo5_7495.Text = "//OUR BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();
            txt_R42_Orderingins_IFSC_5517.Text = dt.Rows[0]["RTGSCode"].ToString();
        }
    }
    protected void Fill_LCDiscountingDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_IBD_ACC_ONLCDiscount_Details", PDocNo, PIBDDocument_No_Extn);
        if (dt.Rows.Count > 0)
        {
            txtCustName.Text = dt.Rows[0]["CustomerName"].ToString();
            txtIBDDocNo.Text = dt.Rows[0]["IBDDocument_No"].ToString();

            if (dt.Rows[0]["IBD_Extn_Flag"].ToString() == "Y")
            {
                PanelIBDExtn.Visible = true;
                txtIBDExtnNo.Text = dt.Rows[0]["IBDDocument_No_Extn"].ToString();
            }

            if (dt.Rows[0]["IntapplicantBeniStatus"].ToString() == "IDA")
            {
                rdbIDOA.Checked = true;
                //TabContainerMain.Tabs[4].Visible = false;
            }
            else if (dt.Rows[0]["IntapplicantBeniStatus"].ToString() == "IDB")
            {
                rdbIDOB.Checked = true;
                //TabContainerMain.Tabs[4].Visible = true;
            }
            else if (dt.Rows[0]["IntapplicantBeniStatus"].ToString() == "IBDA")
            {
                rdbIOBDOA.Checked = true;
                //TabContainerMain.Tabs[4].Visible = true;
            }
            else if (dt.Rows[0]["IntapplicantBeniStatus"].ToString() == "IADB")
            {
                rdbIOADOB.Checked = true;
                //TabContainerMain.Tabs[4].Visible = true;
            }
            txtDraftAmt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txtIBDAmt.Text = dt.Rows[0]["IBD_Amt"].ToString();

            txtContractNo.Text = dt.Rows[0]["Contract_No"].ToString();
            txtExchRate.Text = dt.Rows[0]["Exch_Rate"].ToString();
            ddl_PurposeCode.SelectedValue = dt.Rows[0]["Purpose_Code"].ToString();
            //fill_PurposeCode_Description();
            txtApplNo.Text = dt.Rows[0]["Appl_No"].ToString();
            txtRiskCust.Text = dt.Rows[0]["Risk_Cust"].ToString();
            txtApplBR.Text = dt.Rows[0]["Appl_BR"].ToString();

            txtsettlCodeForCust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
            txtsettlforCust_Abbr.Text = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            txtsettlforCust_AccCode.Text = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();
            txtsettlforCust_AccNo.Text = dt.Rows[0]["Settlement_For_Cust_AccNo"].ToString();

            txtInterest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();

            txt_INT_Rate.Text = dt.Rows[0]["INT_Rate"].ToString();
            txtBaseRate.Text = dt.Rows[0]["Base_Rate"].ToString();
            txtSpread.Text = dt.Rows[0]["Spread"].ToString();
            txtInterestAmt.Text = dt.Rows[0]["Int_Amount"].ToString();
            txtFundType.Text = dt.Rows[0]["Fund_type"].ToString();
            txtInternalRate.Text = dt.Rows[0]["Internal_Rate"].ToString();

            txtSettl_CodeForBank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();
            txtSettl_ForBank_Abbr.Text = dt.Rows[0]["Settl_For_Bank_Abbr"].ToString();
            txtSettl_ForBank_AccCode.Text = dt.Rows[0]["Settl_ForBank_AccCode"].ToString();
            txtSettl_ForBank_AccNo.Text = dt.Rows[0]["Settl_ForBank_AccNo"].ToString();

            txtAttn.Text = dt.Rows[0]["Attn"].ToString();
            txtREM_EUC.Text = dt.Rows[0]["REM_EUC"].ToString();

            ////Instructions
            txt_INST_Code.Text = dt.Rows[0]["INST_Code"].ToString();

            //// Import Accounting
            txt_DiscAmt.Text = dt.Rows[0]["IMP_ACC_Amount"].ToString();
            txt_IMP_ACC_ExchRate.Text = dt.Rows[0]["IMP_ACC_ExchRate"].ToString();
            txtHO_Apl.Text = dt.Rows[0]["HO_Appl"].ToString();
            txtCommentCode.Text = dt.Rows[0]["Comment_Code"].ToString();
            //-----------------------------------------------------------------------------------------------------------
            txtPrinc_lump.Text = dt.Rows[0]["Principal_LUMP"].ToString();
            txtPrinc_matu.Text = dt.Rows[0]["Principal_MATU"].ToString();
            txtprinc_Contract_no.Text = dt.Rows[0]["Principal_Contract_No"].ToString();
            txtprinc_Ex_rate.Text = dt.Rows[0]["Principal_Exch_Rate"].ToString();
            txtprinc_Intnl_Ex_rate.Text = dt.Rows[0]["Principal_Intnl_Exch_Rate"].ToString();
            //------------------------------------------------------------------------------------------------------------------
            txtInterest_lump.Text = dt.Rows[0]["Interest_LUMP"].ToString();
            txtInterest_matu.Text = dt.Rows[0]["Interest_MATU"].ToString();

            txt_IBD_DR_Code.Text = dt.Rows[0]["DR_IBDCode"].ToString();
            //txt_IBD_DR_AC_ShortName.Text = dt.Rows[0]["DR_IBDAC_ShortName"].ToString();
            txt_IBD_DR_Cust_abbr.Text = dt.Rows[0]["DR_IBDCust_abbr"].ToString();
            txt_IBD_DR_Cust_Acc.Text = dt.Rows[0]["DR_IBDCust_Acc"].ToString();
            txt_IBD_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_IBDCur_Acc_Curr"].ToString();
            txt_IBD_DR_Cur_Acc_amt.Text = dt.Rows[0]["DR_IBDCur_Acc_amt"].ToString();
            txt_IBD_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_IBDCur_Acc_Payr"].ToString();

            if (dt.Rows[0]["Interest_MATU"].ToString() == "1")
            {
                panal_DRdetails.Visible = true;

                txt_DR_Code.Text = dt.Rows[0]["DR_CODE"].ToString();
                txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_abbr"].ToString();
                txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc"].ToString();
                txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
                txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["DR_Current_Acc_Amount"].ToString();
                txt_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_Current_Acc_Payer"].ToString();

            }


            //--------------------------------------------------------------------------------------------------
            txtInterest_Contract_no.Text = dt.Rows[0]["Interest_Contract_No"].ToString();
            txtInterest_Ex_rate.Text = dt.Rows[0]["Interest_Exch_Rate"].ToString();
            txtInterest_Intnl_Ex_rate.Text = dt.Rows[0]["Interest_Intnl_Exch_Rate"].ToString();
            //----------------------------------------------------------------------------------------------------------
            txtCommission_lump.Text = dt.Rows[0]["Commission_LUMP"].ToString();
            txtCommission_matu.Text = dt.Rows[0]["Commission_MATU"].ToString();

            //------------------------------------------------------------------------------------------------------------
            txtCommission_Contract_no.Text = dt.Rows[0]["Commission_Contract_No"].ToString();
            txtCommission_Ex_rate.Text = dt.Rows[0]["Commission_Exch_Rate"].ToString();
            txtCommission_Intnl_Ex_rate.Text = dt.Rows[0]["Commission_Intnl_Exch_Rate"].ToString();


            txtTheir_Commission_Contract_no.Text = dt.Rows[0]["Their_Commission_Contract_No"].ToString();
            txtTheir_Commission_Ex_rate.Text = dt.Rows[0]["Their_Commission_Exch_Rate"].ToString();
            txtTheir_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["Their_Commission_Intnl_Exch_Rate"].ToString();
            txtTheir_Commission_lump.Text = dt.Rows[0]["Their_Commission_LUMP"].ToString();
            txtTheir_Commission_matu.Text = dt.Rows[0]["Their_Commission_MATU"].ToString();
            //-------------------------------------------------------------------------------------------------------------------
            txt_CR_Code.Text = dt.Rows[0]["CR_Code"].ToString();
            txt_CR_AC_ShortName.Text = dt.Rows[0]["CR_Cust_ACC_Name"].ToString();
            txt_CR_Cust_abbr.Text = dt.Rows[0]["CR_Cust_Abbr"].ToString();
            txt_CR_Cust_Acc.Text = dt.Rows[0]["CR_Cust_Acc_No"].ToString();
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Acceptance_Curr"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["CR_Acceptance_Amount"].ToString();
            txt_CR_Acceptance_payer.Text = dt.Rows[0]["CR_Acceptance_Payer"].ToString();

            //----------------------------------------------------------------------------------------------------
            txt_CR_Interest_amt.Text = dt.Rows[0]["CR_Interest_Amount"].ToString();
            txt_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Amount"].ToString();
            txt_CR_Accept_Commission_amt.Text = dt.Rows[0]["CR_Acceptance_Comm_Amount"].ToString();
            txt_CR_Others_amt.Text = dt.Rows[0]["CR_Others_Amount"].ToString();
            txt_CR_Their_Commission_amt.Text = dt.Rows[0]["CR_Their_Comm_Amount"].ToString();

            txt_DR_Code.Text = dt.Rows[0]["DR_CODE"].ToString();
            txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_abbr"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc"].ToString();
            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["DR_Current_Acc_Amount"].ToString();

            //------------------------------------------------------------------------------------------------------
            txt_DR_Cur_Acc_amt2.Text = dt.Rows[0]["DR_Current_Acc_Amount2"].ToString();
            txt_DR_Cur_Acc_amt3.Text = dt.Rows[0]["DR_Current_Acc_Amount3"].ToString();

            txt_Princ_Ex_Curr.Text = dt.Rows[0]["Principal_Ex_Curr"].ToString();
            txt_interest_Ex_Curr.Text = dt.Rows[0]["Interest_Ex_Curr"].ToString();
            txt_Commission_Ex_Curr.Text = dt.Rows[0]["Commission_Ex_Curr"].ToString();
            txt_Their_Commission_Ex_Curr.Text = dt.Rows[0]["Their_Commission_Ex_Curr"].ToString();

            txt_CR_Accept_Commission_Curr.Text = dt.Rows[0]["CR_Acceptance_Comm_Curr"].ToString();
            txt_CR_Interest_Curr.Text = dt.Rows[0]["CR_Interest_Curr"].ToString();
            txt_CR_Others_Curr.Text = dt.Rows[0]["CR_Others_Curr"].ToString();
            txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Curr"].ToString();
            txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["CR_Their_Comm_Curr"].ToString();
            ////txt_DR_Code_Curr.Text = "INR"; // dt.Rows[0]["Bill_Currency"].ToString();

            txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
            txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["DR_Current_Acc_Curr2"].ToString();
            txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["DR_Current_Acc_Curr3"].ToString();
            //------------------------------------------------------------------------------------

            txt_CR_Accept_Commission_Payer.Text = dt.Rows[0]["CR_Acceptance_Comm_Payer"].ToString();
            txt_CR_Others_Payer.Text = dt.Rows[0]["CR_Others_Payer"].ToString();
            txt_CR_Their_Commission_Payer.Text = dt.Rows[0]["CR_Their_Comm_Payer"].ToString();
            txt_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_Current_Acc_Payer"].ToString();

            //----------------------------------------
            txt_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Payer"].ToString();
            txt_CR_Interest_payer.Text = dt.Rows[0]["CR_Interest_Payer"].ToString();
            txt_DR_Cur_Acc_payer2.Text = dt.Rows[0]["DR_Current_Acc_Payer2"].ToString();
            txt_DR_Cur_Acc_payer3.Text = dt.Rows[0]["DR_Current_Acc_Payer3"].ToString();

            //---------------------------Genral Operation Branch---------------------------------------------

            if ((dt.Rows[0]["GOBR_Bill_Handling_Flag"].ToString() == "Y"))
            {
                cb_GOBranch_Bill_Handling_Flag.Checked = true;
                PanelGOBR_Bill_Handling.Visible = true;
                txt_GOBR_Ref_No.Text = dt.Rows[0]["GOBR_Bill_Handling_TransRef_No"].ToString();
                txt_GOBR_Comment.Text = dt.Rows[0]["GOBR_Bill_Handling_Comment"].ToString();
                txt_GOBR_SectionNo.Text = dt.Rows[0]["GOBR_Bill_Handling_Section"].ToString();
                txt_GOBR_Remarks.Text = dt.Rows[0]["GOBR_Bill_Handling_Remark"].ToString();
                txt_GOBR_Memo.Text = dt.Rows[0]["GOBR_Bill_Handling_Memo"].ToString();
                txt_GOBR_Scheme_no.Text = dt.Rows[0]["GOBR_Bill_Handling_SchemeNo"].ToString();
                txt_GOBR_Debit_Code.SelectedValue = dt.Rows[0]["GOBR_Bill_Handling_Debit_Code"].ToString();
                txt_GOBR_Debit_Curr.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_CCY"].ToString();
                txt_GOBR_Debit_Amt.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Amt"].ToString();
                txt_GOBR_Debit_Cust.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_abbr"].ToString();

                txt_GOBR_Debit_Cust_AcCode.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_GOBR_Debit_Cust_AccNo.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_AccNo"].ToString();

                txt_GOBR_Debit_Exch_Rate.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_ExchRate"].ToString();
                txt_GOBR_Debit_Exch_CCY.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_ExchCCY"].ToString();
                txt_GOBR_Debit_FUND.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Fund"].ToString();
                txt_GOBR_Debit_Check_No.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_CheckNo"].ToString();
                txt_GOBR_Debit_Available.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Available"].ToString();
                txt_GOBR_Debit_AdPrint.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Advice_Print"].ToString();
                txt_GOBR_Debit_Details.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Details"].ToString();
                txt_GOBR_Debit_Entity.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Entity"].ToString();
                txt_GOBR_Debit_Division.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Division"].ToString();
                txt_GOBR_Debit_Inter_Amount.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_InterAmt"].ToString();
                txt_GOBR_Debit_Inter_Rate.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_InterRate"].ToString();
                txt_GOBR_Credit_Code.SelectedValue = dt.Rows[0]["GOBR_Bill_Handling_Credit_Code"].ToString();
                txt_GOBR_Credit_Curr.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_CCY"].ToString();
                txt_GOBR_Credit_Amt.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Amt"].ToString();
                txt_GOBR_Credit_Cust.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Cust_abbr"].ToString();

                txt_GOBR_Credit_Cust_AcCode.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Cust_AccCode"].ToString();

                txt_GOBR_Credit_Cust_AccNo.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Cust_AccNo"].ToString();
                txt_GOBR_Credit_Exch_Rate.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_ExchRate"].ToString();
                txt_GOBR_Credit_Exch_Curr.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_ExchCCY"].ToString();
                txt_GOBR_Credit_FUND.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Fund"].ToString();
                txt_GOBR_Credit_Check_No.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_CheckNo"].ToString();
                txt_GOBR_Credit_Available.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Available"].ToString();
                txt_GOBR_Credit_AdPrint.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Advice_Print"].ToString();
                txt_GOBR_Credit_Details.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Details"].ToString();
                txt_GOBR_Credit_Entity.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Entity"].ToString();
                txt_GOBR_Credit_Division.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Division"].ToString();
                txt_GOBR_Credit_Inter_Amount.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_InterAmt"].ToString();
                txt_GOBR_Credit_Inter_Rate.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_InterRate"].ToString();

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

                txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_GO1_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccNo"].ToString();

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

                txt_GO1_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_AccCode"].ToString();

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
            //--------------------------GO2----------------------------------------------------------
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

                txt_GO2_Debit_Cust_AcCode.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_GO2_Debit_Cust_AccNo.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccNo"].ToString();

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

                txt_GO2_Credit_Cust_AcCode.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_AccCode"].ToString();

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

            //---------------R42 format for IBD-------------------------------------------
            if (dt.Rows[0]["IBD_Extn_Flag"].ToString() != "Y")
            {
                tblR42format.Visible = true;
                txt_R42_tansactionRefNO.Text = dt.Rows[0]["TranRefNo_2020"].ToString();
                txt_R42_RelatedRef.Text = dt.Rows[0]["RelatedRefno_2006"].ToString();
                txt_R42_Curr_4488.Text = dt.Rows[0]["Curr_4488"].ToString();
                txt_R42_Amt_4488.Text = dt.Rows[0]["Amt_4488"].ToString();
                txt_R42_Orderingins_IFSC_5517.Text = dt.Rows[0]["OrderingInstitution_IFSC_5517"].ToString();
                txt_R42_Benificiary_IFSC_6521.Text = dt.Rows[0]["BeneficiaryInstitution_IFSC_5517"].ToString();
                txt_R42_CodeWord_7495.Text = dt.Rows[0]["CodeWord_7495"].ToString();
                txt_R42_AddInfo_7495.Text = dt.Rows[0]["AdditionalInfo_7495"].ToString();
                txt_R42_MoreInfo_7495.Text = dt.Rows[0]["MoreInfo_7495"].ToString();
                txt_R42_MoreInfo2_7495.Text = dt.Rows[0]["MoreInfo2_7495"].ToString();
                txt_R42_MoreInfo3_7495.Text = dt.Rows[0]["MoreInfo3_7495"].ToString();
                txt_R42_MoreInfo4_7495.Text = dt.Rows[0]["MoreInfo4_7495"].ToString();
                txt_R42_MoreInfo5_7495.Text = dt.Rows[0]["MoreInfo5_7495"].ToString();
            }
            else
            {
                tblR42format.Visible = false;
            }
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
        }
    }
    private void ToggleDocType(string Branch, string DocType, string DocFLC_ILC_Type)
    {
        if (Branch == "Mumbai")
        {
            Branch = "Mumbai";
            tbDocumentGOBranch.Visible = false;
        }
        else
        {
            Branch = "Branch";
            tbDocumentGOBranch.Visible = true;
        }
        switch (DocType)
        {
            case "ACC": //LodgmentUnderLC_Usance
                string Foreign_Local = "";
                if (DocFLC_ILC_Type == "FLC")
                {
                    Foreign_Local = "Foreign";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "Local";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblLCDescount_Lodgment_UnderLC.Text = "LCDiscount_Under_LC";
                lblSight_Usance.Text = "Usance";

                lbl_Instructions1.Text = "KINDLY RETURN ATTACHED TRUST RECEIPT OFFICIALLY SIGNED.";

                break;
        }
    }
    protected void Get_LCDescounting_Get_Date_Diff(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PInterest_From = new SqlParameter("@Interest_From", txtInterest_From.Text.ToString());
        SqlParameter PInterest_To = new SqlParameter("@Interest_To", txtInterest_To.Text.ToString());
        DataTable Date_dt = new DataTable();
        Date_dt = obj.getData("TF_IMP_ONLCDescounting_Get_Date_Diff", PInterest_From, PInterest_To);
        if (Date_dt.Rows.Count > 0)
        {
            txt_No_Of_Days.Text = Date_dt.Rows[0]["NoOfDays"].ToString();
            txt_INT_Rate.Focus();
        }
    }
    protected void rdbIDOA_CheckedChanged(object sender, EventArgs e)
    {
        Togglerdbonappbeni();
        txt_CR_Code.Text = "01991";
    }
    protected void rdbIDOB_CheckedChanged(object sender, EventArgs e)
    {
        Togglerdbonappbeni();
    }
    protected void rdbIOBDOA_CheckedChanged(object sender, EventArgs e)
    {
        Togglerdbonappbeni();
    }
    protected void rdbIOADOB_CheckedChanged(object sender, EventArgs e)
    {
        Togglerdbonappbeni();
    }
    protected void Togglerdbonappbeni()
    {
        if (rdbIDOA.Checked == true)
        {
            txt_CR_AC_ShortName.Text = "RSV DEPO TO RBI";
            btnGO1_Next.Enabled = true;
            btnGOBR_Next.Visible = false;
            //TabContainerMain.Tabs[4].Visible = false;
        }
        else if (rdbIDOB.Checked == true)
        {
            txt_CR_AC_ShortName.Text = "SUNDRY DEPOSITS";
            btnGO1_Next.Visible = true;
            btnGOBR_Next.Visible = true;
            txtInterest_matu.Text = "1";
            //panal_DRdetails.Visible = true;
            //TabContainerMain.Tabs[4].Visible = true;
        }
        else if (rdbIOBDOA.Checked == true)
        {
            txt_CR_AC_ShortName.Text = "SUNDRY DEPOSITS";
            btnGO1_Next.Visible = true;
            btnGOBR_Next.Visible = true;
            txtInterest_matu.Text = "1";
            //panal_DRdetails.Visible = true;
            //TabContainerMain.Tabs[4].Visible = true;
        }
        else if (rdbIOADOB.Checked == true)
        {
            txt_CR_AC_ShortName.Text = "SUNDRY DEPOSITS";
            btnGO1_Next.Visible = true;
            btnGOBR_Next.Visible = true;
            //TabContainerMain.Tabs[4].Visible = true;
        }
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "R42_Amt_Calculation();", true);

        txtInterest_matu_TextChanged(null, null);

    }
    protected void Togglerdbonappbeni1()
    {
        if (rdbIDOA.Checked == true)
        {
            btnGOBR_Next.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P_IBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
        string Result = obj.SaveDeleteData("TF_IMP_SubmitONLCDiscountForChecker", P_DocNo, P_IBDDocument_No_Extn);
        if (Result == "Submit")
        {
            string _script = "window.location='TF_IMP_LC_DESCOUNTING_ACC_IBD_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }

    }
    protected void MakeReadOnly()
    {
        txtDocNo.Enabled = false;
        txtValueDate.Enabled = false;

        txtPurpose.Enabled = false;
        ddl_PurposeCode.Enabled = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_LC_DESCOUNTING_ACC_IBD_Maker_View.aspx");
    }
    protected void cb_GOBranch_Bill_Handling_Flag_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_GOBranch_Bill_Handling_Flag.Checked == false)
        {
            PanelGOBR_Bill_Handling.Visible = false;
        }
        else if (cb_GOBranch_Bill_Handling_Flag.Checked == true)
        {
            PanelGOBR_Bill_Handling.Visible = true;
            txt_GOBR_Debit_Code.SelectedValue = "D";
            txt_GOBR_Debit_Curr.Text = "INR";
            txt_GOBR_Credit_Code.SelectedValue = "C";
            txt_GOBR_Credit_Curr.Text = "INR";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "R42_Amt_Calculation();", true);
        }
    }
    protected void chk_GO1_Flag_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_GO1_Flag.Checked == false)
        {
            Panel_GO1.Visible = false;
        }
        else if (chk_GO1_Flag.Checked == true)
        {
            Panel_GO1.Visible = true;
            txt_GO1_Debit_Code.SelectedValue = "D";
            txt_GO1_Debit_Curr.Text = "INR";
            txt_GO1_Credit_Code.SelectedValue = "C";
            txt_GO1_Credit_Curr.Text = "INR";
        }
    }
    protected void chk_GO2_Flag_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_GO2_Flag.Checked == false)
        {
            Panel_GO2.Visible = false;
        }
        else if (chk_GO2_Flag.Checked == true)
        {
            Panel_GO2.Visible = true;
            txt_GO2_Debit_Code.SelectedValue = "D";
            txt_GO2_Debit_Curr.Text = "INR";
            txt_GO2_Credit_Code.SelectedValue = "C";
            txt_GO2_Credit_Curr.Text = "INR";
        }
    }
    protected void txtIBDDocNo_CheckedChanged(object sender, EventArgs e)
    {
        //ToggleapplicantBeni();
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter p1 = new SqlParameter("@REFNO", txtIBDDocNo.Text.Trim());
        string branch = txtDocNo.Text.Substring(4, 3);
        SqlParameter p2 = new SqlParameter("@BranchCode", branch);
        SqlParameter p3 = new SqlParameter("@DocNo", txtDocNo.Text);
        dt = obj.getData("TF_IMP_FillMintDetails", p1, p2);
        if (dt.Rows.Count > 0)
        {
            string result = obj.SaveDeleteData("TF_IMP_CheckMint", p1, p3);
            string _script = "";
            if (result == "Exists")
            {
                _script = "alert('This IBD REF Number(" + txtIBDDocNo.Text + ") already used.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txtIBDDocNo.Text = txt_R42_tansactionRefNO.Text = "";
                txtCustName.Text = "";
                txtDraftAmt.Text = "";
                txtRiskCust.Text = "";
                txtInterest_To.Text = "";
                txt_INT_Rate.Text = "";
                txtSpread.Text = "";
                txtInterestAmt.Text = "";
                txtInternalRate.Text = "";
                txtIBDDocNo.Focus();
                return;
            }
            txtCustName.Text = dt.Rows[0]["Customer_Abbreviation"].ToString();
            txtDraftAmt.Text = dt.Rows[0]["Principal_Amount"].ToString();
            txtRiskCust.Text = dt.Rows[0]["Customer_Abbreviation"].ToString();
            txtInterest_To.Text = dt.Rows[0]["Final_Due_Date"].ToString();
            txt_INT_Rate.Text = dt.Rows[0]["Interest_Rate"].ToString();
            txtSpread.Text = dt.Rows[0]["Trans_Spread"].ToString();
            txtInterestAmt.Text = dt.Rows[0]["Interest_Amount"].ToString();
            txtInternalRate.Text = dt.Rows[0]["Internal_Rate"].ToString();
            /////DraftAmt=CR_amt , IBD_DR_amt, DiscAmt, R42_Amt_4488
            txtDraftAmt.Text = dt.Rows[0]["Principal_Amount"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["Principal_Amount"].ToString();
            txt_IBD_DR_Cur_Acc_amt.Text = dt.Rows[0]["Principal_Amount"].ToString();
            txt_DiscAmt.Text = dt.Rows[0]["Principal_Amount"].ToString();
            ///// INT Amt=CR_Interest_amt and DR Current acc Amt
            txtInterestAmt.Text = dt.Rows[0]["Interest_Amount"].ToString();
            txt_CR_Interest_amt.Text = dt.Rows[0]["Interest_Amount"].ToString();
            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["Interest_Amount"].ToString();
            txt_CR_Interest_payer.Text = "B";

            txt_R42_tansactionRefNO.Text = txtIBDDocNo.Text;
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "R42_Amt_Calculation();", true);
            txtIBDDocNo.Focus();
        }
        else
        {
            txtIBDDocNo.Text = txt_R42_tansactionRefNO.Text = "";
            txtCustName.Text = "";
            txtDraftAmt.Text = "";
            txtRiskCust.Text = "";
            txtInterest_To.Text = "";
            txt_INT_Rate.Text = "";
            txtSpread.Text = "";
            txtInternalRate.Text = "";

            txt_CR_Acceptance_amt.Text = "";
            txt_IBD_DR_Cur_Acc_amt.Text = "";
            txt_DiscAmt.Text = "";

            txtInterestAmt.Text = "";
            txt_CR_Interest_amt.Text = "";
            txt_DR_Cur_Acc_amt.Text = "";
            txt_CR_Interest_payer.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid IBD REF Number.')", true);
            txtIBDDocNo.Focus();
        }
    }
    [WebMethod]
    public static Fields CheckIBDREFno(string REFNo, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@REFNO", REFNo);
        SqlParameter p3 = new SqlParameter("@DocNo", DocNo);
        string result = obj.SaveDeleteData("TF_IMP_CheckMint", p1, p3);
        fields.IBDREFnoStatus = result;
        return fields;
    }
    public class Fields
    {
        public string IBDREFnoStatus { get; set; }
    }
    protected void Get_DiscrepencyCharges()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PCurr = new SqlParameter("@Bill_Curr", lblDoc_Curr.Text.ToString());
        DataTable DM = new DataTable();
        DM = obj.getData("TF_IMP_Get_DiscrepencyCharges", PCurr);
        if (DM.Rows.Count > 0)
        {
            hdnDiscrepancyChrg.Value = DM.Rows[0]["Amount"].ToString();
        }
        DataTable St = new DataTable();
        St = obj.getData("TF_IMP_Get_taxCharges");
        if (St.Rows.Count > 0)
        {
            hdnTaxChrg.Value = St.Rows[0]["TOTAL_SERVICE_TAX"].ToString();
        }
    }
}