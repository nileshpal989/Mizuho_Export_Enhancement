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

public partial class IMP_Transactions_TF_IMP_LC_DESCOUNTING_ACC_IBD_Checker : System.Web.UI.Page
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
                    Response.Redirect("TF_IMP_LC_DESCOUNTING_ACC_IBD_Checker.aspx", true);
                }
                else
                {
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    hdnDocNo.Value = Request.QueryString["DocNo"].Trim();
                    txtIBDExtnNo.Text = "";
                    hdnIBDExtnFlag.Value = "";
                    if (Request.QueryString["IBDExtn"] != null)
                    {
                        if (Request.QueryString["IBDExtn"] == "Y")
                        {
                            hdnIBDExtnFlag.Value = "Y";
                            PanelIBDExtn.Visible = true;
                            txtIBDExtnNo.Text = Request.QueryString["IBDExtnDocNo"].ToString();
                            tblR42format.Visible = false;
                        }
                    }
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
            txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["Bill_Currency"].ToString();

            //R42 file
            txt_R42_tansactionRefNO.Text = dt.Rows[0]["Transaction Reference Number"].ToString();
            txt_R42_RelatedRef.Text = dt.Rows[0]["Related Reference"].ToString();
            txt_R42_Curr_4488.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_R42_Benificiary_IFSC_6521.Text = dt.Rows[0]["IFSC_Code"].ToString();
            txt_R42_CodeWord_7495.Text = "TRF";
            txt_R42_AddInfo_7495.Text = "";
            txt_R42_MoreInfo_7495.Text = "//" + dt.Rows[0]["Drawer_NAME"].ToString();
            txt_R42_MoreInfo2_7495.Text = "//THEIR REF NO:" + dt.Rows[0]["Related Reference"].ToString();
            txt_R42_MoreInfo3_7495.Text = "//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString();
            txt_R42_MoreInfo4_7495.Text = "//" + dt.Rows[0]["Drawer_NAME"].ToString();
            txt_R42_MoreInfo5_7495.Text = "//BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();
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
            txtValueDate.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();

            if (dt.Rows[0]["IBD_Extn_Flag"].ToString() == "Y")
            {
                PanelIBDExtn.Visible = true;
                txtIBDExtnNo.Text = dt.Rows[0]["IBDDocument_No_Extn"].ToString();
            }

            if (dt.Rows[0]["IntapplicantBeniStatus"].ToString() == "A")
            {
                Intapp.Checked = true;
                //TabContainerMain.Tabs[4].Visible = false;
            }
            else if (dt.Rows[0]["IntapplicantBeniStatus"].ToString() == "B")
            {
                Intbeni.Checked = true;
                //TabContainerMain.Tabs[4].Visible = true;
            }
            txtDraftAmt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txtIBDAmt.Text = dt.Rows[0]["IBD_Amt"].ToString();

            txtContractNo.Text = dt.Rows[0]["Contract_No"].ToString();
            txtExchRate.Text = dt.Rows[0]["Exch_Rate"].ToString();
            ddl_PurposeCode.SelectedValue = dt.Rows[0]["Purpose_Code"].ToString();
            //fill_PurposeCode_Description();
            txtvalueDate1.Text = dt.Rows[0]["ValueDate1"].ToString();
            txtApplNo.Text = dt.Rows[0]["Appl_No"].ToString();

            txtRiskCust.Text = dt.Rows[0]["Risk_Cust"].ToString();
            txtApplBR.Text = dt.Rows[0]["Appl_BR"].ToString();


            txtsettlCodeForCust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
            txtsettlforCust_Abbr.Text = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            txtsettlforCust_AccCode.Text = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();
            txtsettlforCust_AccNo.Text = dt.Rows[0]["Settlement_For_Cust_AccNo"].ToString();

            txtInterest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();
            txtInterest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            txt_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();

            txt_INT_Rate.Text = dt.Rows[0]["INT_Rate"].ToString();
            txtBaseRate.Text = dt.Rows[0]["Base_Rate"].ToString();
            txtSpread.Text = dt.Rows[0]["Spread"].ToString();
            txtInterestAmt.Text = dt.Rows[0]["Int_Amount"].ToString();
            txtFundType.Text = dt.Rows[0]["Fund_type"].ToString();
            txtInternalRate.Text = dt.Rows[0]["Internal_Rate"].ToString();
            txtIBD_ACC_kind.Text = dt.Rows[0]["IBD_ACC_Kind"].ToString();

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
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Acceptance_Curr"].ToString();
            txt_CR_Interest_Curr.Text = dt.Rows[0]["CR_Interest_Curr"].ToString();
            txt_CR_Others_Curr.Text = dt.Rows[0]["CR_Others_Curr"].ToString();
            txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Curr"].ToString();
            txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["CR_Their_Comm_Curr"].ToString();
            ////txt_DR_Code_Curr.Text = "INR"; // dt.Rows[0]["Bill_Currency"].ToString();

            txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
            txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["DR_Current_Acc_Curr2"].ToString();
            txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["DR_Current_Acc_Curr3"].ToString();
            //------------------------------------------------------------------------------------

            txt_CR_Acceptance_payer.Text = dt.Rows[0]["CR_Acceptance_Payer"].ToString();
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
                txt_GOBR_Debit_Code.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Code"].ToString();
                txt_GOBR_Debit_Curr.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_CCY"].ToString();
                txt_GOBR_Debit_Amt.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Amt"].ToString();
                txt_GOBR_Debit_Cust.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_abbr"].ToString();
                txt_GOBR_Debit_Cust_Name.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_Name"].ToString();
                txt_GOBR_Debit_Cust_AcCode.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_GOBR_Debit_Cust_AccNo.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_AccNo"].ToString();
                txt_GOBR_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GOBR_Bill_Handling_Debit_Cust_AccCode_Disc"].ToString();
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
                txt_GOBR_Credit_Code.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Code"].ToString();
                txt_GOBR_Credit_Curr.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_CCY"].ToString();
                txt_GOBR_Credit_Amt.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Amt"].ToString();
                txt_GOBR_Credit_Cust.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Cust_abbr"].ToString();
                txt_GOBR_Credit_Cust_Name.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Cust_Name"].ToString();
                txt_GOBR_Credit_Cust_AcCode.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Cust_AccCode"].ToString();
                txt_GOBR_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GOBR_Bill_Handling_Credit_Cust_AccCode_Disc"].ToString();
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

            //------------------------GENRAL OPERATION-----------------------------------------------
            if ((dt.Rows[0]["GO_Bill_Handling_Flag"].ToString() == "Y"))
            {
                chk_GO1_Flag.Checked = true;
                Panel_GO1.Visible = true;

                txt_GO1_Comment.Text = dt.Rows[0]["GO_Bill_Handling_Comment"].ToString();
                txt_GO1_SectionNo.Text = dt.Rows[0]["GO_Bill_Handling_Section"].ToString();
                txt_GO1_Remarks.Text = dt.Rows[0]["GO_Bill_Handling_Remark"].ToString();
                txt_GO1_Memo.Text = dt.Rows[0]["GO_Bill_Handling_Memo"].ToString();
                txt_GO1_Scheme_no.Text = dt.Rows[0]["GO_Bill_Handling_SchemeNo"].ToString();
                txt_GO1_Debit_Code.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Code"].ToString();
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
                txt_GO1_Credit_Code.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Code"].ToString();
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


            //---------------R42 format for IBD-------------------------------------------
            if (dt.Rows[0]["IBD_Extn_Flag"].ToString() != "Y")
            {
                tblR42format.Visible = true;
                txt_R42_tansactionRefNO.Text = dt.Rows[0]["TranRefNo_2020"].ToString();
                txt_R42_RelatedRef.Text = dt.Rows[0]["RelatedRefno_2006"].ToString();
                txt_R42_ValueDate_4488.Text = dt.Rows[0]["ValueDate_4488"].ToString();
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
            hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
        }
    }
    private void fill_PurposeCode_Description()
    {

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@purposeID", ddl_PurposeCode.SelectedValue.ToString());
        DataTable dt = objData.getData("TF_IMP_PurposeCodeDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lbl_PurposeCodeDesc.Text = dt.Rows[0]["description"].ToString().Trim();
            lbl_PurposeCodeDesc.ToolTip = lbl_PurposeCodeDesc.Text;

            txtsettlCodeForCust.Focus();
        }
        else
        {
            lbl_PurposeCodeDesc.Text = "";
            ddl_PurposeCode.ClearSelection();
            ddl_PurposeCode.Focus();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";

            IBDGbaseFileCreation(); //Shrihari Fiile Creation
            //vinay--------------
            if (cb_GOBranch_Bill_Handling_Flag.Checked == true)
            {
                GOBRFileCreation();
            }
            //GOBRFileCreation();
            if (chk_GO1_Flag.Checked == true)
            {
                GO1FileCreation();
            }

            CreateSwiftR42();
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        SqlParameter PIBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
        string _script = "", Result = "";
        Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectLCDescounting", P_DocNo, P_Status, P_RejectReason, PIBDDocument_No_Extn);

        _script = "TF_IMP_LC_DESCOUNTING_ACC_IBD_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR;
        Response.Redirect(_script, true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_LC_DESCOUNTING_ACC_IBD_Checker_View.aspx");
    }

    public void IBDGbaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
        DataTable dt = objData1.getData("TF_IMP_LCDiscounting_IBD_ACC_GbaseFileCreation", PDocNo, PIBDDocument_No_Extn);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Booking/Gbase/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_Gbase" + ".xlsx";
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
    public void GOBRFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
        DataTable dt = objData1.getData("TF_IMP_LCDiscounting_IBD_ACC_GbaseFileCreation_GOBR", PDocNo, PIBDDocument_No_Extn);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Booking/Gbase/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_GOBR" + ".xlsx";
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
    public void GO1FileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
        DataTable dt = objData1.getData("TF_IMP_LCDiscounting_IBD_ACC_GbaseFileCreation_GO1", PDocNo, PIBDDocument_No_Extn);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Booking/Gbase/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_GO1" + ".xlsx";
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
    public void CreateSwiftR42()
    {
        string FileType = "SWIFT";
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBDDocument_No_Extn = new SqlParameter("@IBDDocument_No_Extn", txtIBDExtnNo.Text.ToString());
        DataTable dt = objData1.getData("TF_IMP_LCDiscounting_IBD_ACC_R42_GbaseFileCreation", PDocNo, PIBDDocument_No_Extn);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Own LC Discounting/Booking/R42/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtIBDDocNo.Text.Trim() + "_R42" + ".xlsx";
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    if (FileType == "SWIFT")
                    {
                        ws.Cells["B1"].Value = "Reciver:";
                    }
                    else
                    {
                        ws.Cells["B1"].Value = "Reciver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();

                    ws.Cells["A2"].Value = "[2020]";
                    ws.Cells["B2"].Value = "Transation Reference Number";
                    ws.Cells["C2"].Value = dt.Rows[0]["TransactionRefNo"].ToString();

                    ws.Cells["A3"].Value = "[2006]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["C3"].Value = dt.Rows[0]["RelatedReference"].ToString();

                    ws.Cells["A4"].Value = "[4488]";
                    ws.Cells["B4"].Value = "Value Date, Currency Code, Amount";
                    ws.Cells["C4"].Value = dt.Rows[0]["ValueDate"].ToString();
                    ws.Cells["D4"].Value = dt.Rows[0]["Currency"].ToString();
                    ws.Cells["E4"].Value = dt.Rows[0]["Amount"].ToString();

                    ws.Cells["A5"].Value = "[5517]";
                    ws.Cells["B5"].Value = "Ordering Institution IFSC";
                    ws.Cells["C5"].Value = dt.Rows[0]["OrderingInstitutionIFSC"].ToString();

                    ws.Cells["A6"].Value = "[6521]";
                    ws.Cells["B6"].Value = "Beneficiary Institution IFSC";
                    ws.Cells["C6"].Value = dt.Rows[0]["BeneficiaryInstitutionIFSC"].ToString();

                    ws.Cells["A7"].Value = "[7495]";
                    ws.Cells["B7"].Value = "Sender to Receiver Info";
                    ws.Cells["C7"].Value = dt.Rows[0]["CodeWord"].ToString();
                    int _Ecol = 8;
                    if (dt.Rows[0]["AdditionalInfo"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AdditionalInfo"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MoreInfo1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MoreInfo1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MoreInfo2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MoreInfo2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MoreInfo3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MoreInfo3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MoreInfo4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MoreInfo4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MoreInfo5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MoreInfo5"].ToString();
                        _Ecol++;
                    }

                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    using (MemoryStream MS = new MemoryStream())
                    {
                        pck.SaveAs(MS);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MS.WriteTo(file);
                        file.Close();
                        MS.Close();
                    }
                }
            }
        }
        else
        {
        }
    }
}