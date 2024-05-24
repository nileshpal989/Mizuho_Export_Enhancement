using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using OfficeOpenXml;
using ClosedXML.Excel;

public partial class IMP_Transactions_TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker : System.Web.UI.Page
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
                TabContainerMain.ActiveTab = tbDocumentLedger;
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx", true);
                }
                else
                {
                    fill_PurposeCode();
                    hdnUserName.Value = Session["userName"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();

                    txtIBD_ACC_kind.Text = "1";
                    txtAutoSettlement.Text = "0";
                    txtCountryRisk.Text = "IN";
                    txtGradeCode.Text = "99";
                    txtPurpose.Text = "C";
                    txt_CR_Accept_Commission_Curr.Text = "INR";
                    MakeReadOnly();

                    Fill_Logd_Details();
                    Fill_AcceptanceDetails();
                    Get_Acceptance_Get_Date_Diff();
                    Check_LEINO_ExchRateDetails();
                    Check_cust_Leiverify();

                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                        btn_Verify.Enabled = false;
                        Check_Lei_Verified();
                        Check_Lei_RecurringStatus();
                    }
                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    protected void Fill_Logd_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_BOE_Details_For_Acceptance", PDocNo);
        if (dt.Rows.Count > 0)
        {
            hdnDocNo.Value = dt.Rows[0]["Document_No"].ToString();
            ToggleDocType(dt.Rows[0]["Document_Type"].ToString(), dt.Rows[0]["Document_FLC_ILC"].ToString());
            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();             //Added by bhupen on 09112022
            hdnDocScrutiny.Value = dt.Rows[0]["Document_Scrutiny"].ToString();
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_Doc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();

            hdnNegoRemiBankType.Value = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            if (dt.Rows[0]["Nego_Remit_Bank_Type"].ToString() == "FOREIGN")
            {
                btn_Generate_SFMS.Visible = false;
                btn_Generate_Swift.Visible = true;
            }
            else
            {
                if (dt.Rows[0]["Nego_Remit_Bank_Type"].ToString() == "LOCAL")
                {
                    btn_Generate_SFMS.Visible = true;
                    btn_Generate_Swift.Visible = false;
                }
                else
                {
                    btn_Generate_SFMS.Visible = false;
                    btn_Generate_Swift.Visible = false;
                }
            }

            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txtDraftAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            //txtIBDAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt_DiscAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txtLCNo.Text = dt.Rows[0]["Doc_LC_No"].ToString();


            txtCustomer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
            fillCustomerMasterDetails();
            txtRiskCust.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();

            txtApplNo.Text = dt.Rows[0]["txnappno"].ToString();
            //txtApplBR.Text = dt.Rows[0]["Branch_Code"].ToString();

            if (dt.Rows[0]["Document_Scrutiny"].ToString() == "1")
            {
                txtsettlCodeForCust.Text = "23";
                txtsettlforCust_Abbr.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();
                txtsettlforCust_AccCode.Text = dt.Rows[0]["AC_Code"].ToString();
                txtsettlforCust_AccNo.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();
            }
            else if (dt.Rows[0]["Document_Scrutiny"].ToString() == "2")
            {
                txtsettlCodeForCust.Text = "29";
                txtsettlforCust_Abbr.Text = "900";
                txtsettlforCust_AccCode.Text = "22002";
                txtsettlforCust_AccNo.Text = "H30792000056";
            }

            txtInterest_To.Text = dt.Rows[0]["Maturity_Date"].ToString();
            txtNego_Remit_Bank_Type.Text = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            txtNego_Remit_Bank.Text = dt.Rows[0]["Nego_Remit_Bank"].ToString();
            lbl_Nego_Remit_Bank.Text = dt.Rows[0]["Nego_Remit_Bank_Desc"].ToString();
            txtAcwithInstitution.Text = dt.Rows[0]["Acc_With_Bank"].ToString();
            lblAcWithInstiBankDesc.Text = dt.Rows[0]["Acc_With_Bank_Desc"].ToString();
            txtReimbursingbank.Text = dt.Rows[0]["Reimbursing_Bank"].ToString();
            lbl_Reimbursingbank.Text = dt.Rows[0]["Reimbursing_Bank_Desc"].ToString();

            txtDrawer.Text = dt.Rows[0]["Drawer_NAME"].ToString();

            ddlTenor.SelectedValue = dt.Rows[0]["Tenor_Type"].ToString();
            txtTenor.Text = dt.Rows[0]["Tenor"].ToString();
            ddlTenor_Days_From.SelectedValue = dt.Rows[0]["Tenor_Days_From"].ToString();
            txtTenor_Description.Text = dt.Rows[0]["Tenor_Desc"].ToString();

            txtDateArrival.Text = dt.Rows[0]["Received_Date"].ToString();
            txt_Their_Ref_no.Text = dt.Rows[0]["Their_Ref_No"].ToString();

            txtCommodity.Text = dt.Rows[0]["Commodity_Code"].ToString();
            fill_GBaseCommodity_Description();
            txtCommodityDesc.Text = dt.Rows[0]["Commodity_Desc"].ToString();

            txtShippingDate.Text = dt.Rows[0]["Shipping_Date"].ToString();
            txtVesselName.Text = dt.Rows[0]["Vessel_Name"].ToString();
            txtFromPort.Text = dt.Rows[0]["From_Port"].ToString();
            txtToPort.Text = dt.Rows[0]["To_Port"].ToString();
            txtDocFirst1.Text = dt.Rows[0]["DOC_First_INS"].ToString();
            txtDocFirst2.Text = dt.Rows[0]["DOC_First_BL"].ToString();
            txtDocFirst3.Text = dt.Rows[0]["DOC_First_AWB"].ToString();
            txtDocSecond1.Text = dt.Rows[0]["DOC_Second_INS"].ToString();
            txtDocSecond2.Text = dt.Rows[0]["DOC_Second_BL"].ToString();
            txtDocSecond3.Text = dt.Rows[0]["DOC_Second_AWB"].ToString();

            if (dt.Rows[0]["Special_Instruction_Type"].ToString() == "OTHERS")
            {
                rdb_SP_Instr_Other.Checked = true;
            }
            if (dt.Rows[0]["Special_Instruction_Type"].ToString() == "ANNEXURE")
            {
                rdb_SP_Instr_Annexure.Checked = true;
            }
            if (dt.Rows[0]["Special_Instruction_Type1"].ToString() == "ON_SIGHT")
            {
                rdb_SP_Instr_On_Sight.Checked = true;
            }
            if (dt.Rows[0]["Special_Instruction_Type1"].ToString() == "ON_DATE")
            {
                rdb_SP_Instr_On_Date.Checked = true;
            }
            txt_SP_Instructions1.Text = dt.Rows[0]["Special_Instruction1"].ToString();
            txt_SP_Instructions2.Text = dt.Rows[0]["Special_Instruction2"].ToString();
            txt_SP_Instructions3.Text = dt.Rows[0]["Special_Instruction3"].ToString();
            txt_SP_Instructions4.Text = dt.Rows[0]["Special_Instruction4"].ToString();
            txt_SP_Instructions5.Text = dt.Rows[0]["Special_Instruction5"].ToString();

            //            Import Accounting
            txt_Princ_Ex_Curr.Text = "";
            txt_interest_Ex_Curr.Text = "";
            txt_Commission_Ex_Curr.Text = "";
            txt_Their_Commission_Ex_Curr.Text = "";
            txt_CR_Accept_Commission_Curr.Text = "INR";
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Interest_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Others_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();

            txt_DR_Cur_Acc_Curr.Text = "INR";
            txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["Bill_Currency"].ToString();

            txt_DR_Code.Text = dt.Rows[0]["AC_Code"].ToString();
            txt_DR_Cust_abbr.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();

            txt_CR_Their_Commission_amt.Text = dt.Rows[0]["Their_Cumm_Amount"].ToString();

            // General Operation
            // //GO_Swift_SFMS
            txt_GO_Swift_SFMS_Ref_No.Text = dt.Rows[0]["Document_No"].ToString();
            txt_GO_Swift_SFMS_Debit_Cust_AcCode.Text = dt.Rows[0]["AC_Code"].ToString();
            txt_GO_Swift_SFMS_Debit_Cust.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();
            txt_GO_Swift_SFMS_Debit_Cust_AccNo.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();

            txt_GO_Swift_SFMS_Credit_Cust.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();
            // //GO_LC_Commitement
            txt_GO_LC_Commitement_Ref_No.Text = dt.Rows[0]["Doc_LC_No"].ToString();
            txt_GO_LC_Commitement_Debit_Cust_AcCode.Text = dt.Rows[0]["AC_Code"].ToString();
            txt_GO_LC_Commitement_Debit_Cust.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();
            txt_GO_LC_Commitement_Debit_Cust_AccNo.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();

            txt_GO_LC_Commitement_Credit_Cust.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();
            if (dt.Rows[0]["RMA_Status"].ToString() == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('RMA has disable status for this Nego/Remit Bank SWIFT code.')", true);
            }

            SqlParameter PCurr = new SqlParameter("@Bill_Curr", lblDoc_Curr.Text.ToString());
            DataTable DM = new DataTable();
            DM = obj.getData("TF_IMP_Get_DiscrepencyCharges", PCurr);
            if (DM.Rows.Count > 0)
            {
                txt_Discrepancy_Charges_SFMS.Text = DM.Rows[0]["Amount"].ToString();
                txt_Discrepancy_Charges_Swift.Text = DM.Rows[0]["Amount"].ToString();
            }
            else
            {
                txt_Discrepancy_Charges_SFMS.Text = "";
                txt_Discrepancy_Charges_Swift.Text = "";
            }

            fillLCDetails(dt.Rows[0]["Doc_LC_No"].ToString().Trim(), txtDocNo.Text.ToString().Trim(), dt.Rows[0]["Cust_Abbr"].ToString().Trim(), lblForeign_Local.Text.Trim());
        }


    }
    protected void Get_Acceptance_Get_Date_Diff()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PInterest_From = new SqlParameter("@Interest_From", txtInterest_From.Text.ToString());
        SqlParameter PInterest_To = new SqlParameter("@Interest_To", txtInterest_To.Text.ToString());
        DataTable Date_dt = new DataTable();
        Date_dt = obj.getData("TF_IMP_Acceptance_Get_Date_Diff", PInterest_From, PInterest_To);
        if (Date_dt.Rows.Count > 0)
        {
            txt_No_Of_Days.Text = Date_dt.Rows[0]["NoOfDays"].ToString();
        }
    }
    protected void Fill_AcceptanceDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_ACC_IBD_Acceptance_Details", PDocNo);
        if (dt.Rows.Count > 0)
        {

            txtValueDate.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
            txtContractNo.Text = dt.Rows[0]["Contract_No"].ToString();
            txtDraftAmt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txtExchRate.Text = dt.Rows[0]["Exch_Rate"].ToString();

            ddl_PurposeCode.ClearSelection();
            ddl_PurposeCode.SelectedValue = dt.Rows[0]["Purpose_Code"].ToString();
            fill_PurposeCode_Description();

            txtRiskCust.Text = dt.Rows[0]["Risk_Cust"].ToString();
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
            //txtSettl_CodeForBank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();

            txtSettl_ForBank_Abbr.Text = dt.Rows[0]["Settl_For_Bank_Abbr"].ToString();
            txtSettl_ForBank_AccCode.Text = dt.Rows[0]["Settl_ForBank_AccCode"].ToString();
            txtSettl_ForBank_AccNo.Text = dt.Rows[0]["Settl_ForBank_AccNo"].ToString();

            txtAttn.Text = dt.Rows[0]["Attn"].ToString();
            txtREM_EUC.Text = dt.Rows[0]["REM_EUC"].ToString();

            ////Instructions
            txt_INST_Code.Text = dt.Rows[0]["INST_Code"].ToString();

            //// Import Accounting
            if (dt.Rows[0]["IMP_ACC_Commission_Flag"].ToString() == "Y")
            {
                chk_IMP_ACC_Commission.Checked = true;
            }
            else
            {
                chk_IMP_ACC_Commission.Checked = false;
            }
            txt_DiscAmt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txt_IMP_ACC_ExchRate.Text = dt.Rows[0]["IMP_ACC_ExchRate"].ToString();
            //txtPrinc_lump.Text = dt.Rows[0]["Principal_LUMP"].ToString();
            //txtPrinc_matu.Text = dt.Rows[0]["Principal_MATU"].ToString();
            //txtprinc_Contract_no.Text = dt.Rows[0]["Principal_Contract_No"].ToString();
            //txtprinc_Ex_rate.Text = dt.Rows[0]["Principal_Exch_Rate"].ToString();
            //txtprinc_Intnl_Ex_rate.Text = dt.Rows[0]["Principal_Intnl_Exch_Rate"].ToString();

            txtInterest_lump.Text = dt.Rows[0]["Interest_LUMP"].ToString();
            txtInterest_matu.Text = dt.Rows[0]["Interest_MATU"].ToString();
            //txtInterest_Contract_no.Text = dt.Rows[0]["Interest_Contract_No"].ToString();
            //txtInterest_Ex_rate.Text = dt.Rows[0]["Interest_Exch_Rate"].ToString();
            //txtInterest_Intnl_Ex_rate.Text = dt.Rows[0]["Interest_Intnl_Exch_Rate"].ToString();


            txtCommission_lump.Text = dt.Rows[0]["Commission_LUMP"].ToString();
            txtCommission_matu.Text = dt.Rows[0]["Commission_MATU"].ToString();
            //txtCommission_Contract_no.Text = dt.Rows[0]["Commission_Contract_No"].ToString();
            //txtCommission_Ex_rate.Text = dt.Rows[0]["Commission_Exch_Rate"].ToString();
            //txtCommission_Intnl_Ex_rate.Text = dt.Rows[0]["Commission_Intnl_Exch_Rate"].ToString();

            //txtTheir_Commission_Contract_no.Text = dt.Rows[0]["Their_Commission_Contract_No"].ToString();
            //txtTheir_Commission_Ex_rate.Text = dt.Rows[0]["Their_Commission_Exch_Rate"].ToString();
            //txtTheir_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["Their_Commission_Intnl_Exch_Rate"].ToString();
            //txtTheir_Commission_lump.Text = dt.Rows[0]["Their_Commission_LUMP"].ToString();
            //txtTheir_Commission_matu.Text = dt.Rows[0]["Their_Commission_MATU"].ToString();

            txt_CR_Code.Text = dt.Rows[0]["CR_Code"].ToString();
            txt_CR_Cust_abbr.Text = dt.Rows[0]["CR_Cust_Abbr"].ToString();
            txt_CR_Cust_Acc.Text = dt.Rows[0]["CR_Cust_Acc_No"].ToString();

            txt_CR_Acceptance_amt.Text = dt.Rows[0]["CR_Acceptance_Amount"].ToString();
            //txt_CR_Interest_amt.Text = dt.Rows[0]["CR_Interest_Amount"].ToString();
            //txt_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Amount"].ToString();
            txt_CR_Accept_Commission_amt.Text = dt.Rows[0]["CR_Acceptance_Comm_Amount"].ToString();
            txt_CR_Others_amt.Text = dt.Rows[0]["CR_Others_Amount"].ToString();
            txt_CR_Their_Commission_amt.Text = dt.Rows[0]["CR_Their_Comm_Amount"].ToString();


            txt_DR_Code.Text = dt.Rows[0]["DR_Code"].ToString();
            txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_Abbr"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc_No"].ToString();

            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["DR_Current_Acc_Amount"].ToString();
            //txt_DR_Cur_Acc_amt2.Text = dt.Rows[0]["DR_Current_Acc_Amount2"].ToString();
            //txt_DR_Cur_Acc_amt3.Text = dt.Rows[0]["DR_Current_Acc_Amount3"].ToString();

            //txt_Princ_Ex_Curr.Text = dt.Rows[0]["Principal_Ex_Curr"].ToString();
            //txt_interest_Ex_Curr.Text = dt.Rows[0]["Interest_Ex_Curr"].ToString();
            //txt_Commission_Ex_Curr.Text = dt.Rows[0]["Commission_Ex_Curr"].ToString();
            //txt_Their_Commission_Ex_Curr.Text = dt.Rows[0]["Their_Commission_Ex_Curr"].ToString();
            txt_CR_Accept_Commission_Curr.Text = dt.Rows[0]["CR_Acceptance_Comm_Curr"].ToString();
            //txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Acceptance_Curr"].ToString();
            //txt_CR_Interest_Curr.Text = dt.Rows[0]["CR_Interest_Curr"].ToString();
            txt_CR_Others_Curr.Text = dt.Rows[0]["CR_Others_Curr"].ToString();
            //txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Curr"].ToString();
            txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["CR_Their_Comm_Curr"].ToString();
            //txt_DR_Code_Curr.Text = "INR"; // dt.Rows[0]["Bill_Currency"].ToString();

            //txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
            //txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["DR_Current_Acc_Curr2"].ToString();
            //txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["DR_Current_Acc_Curr3"].ToString();

            txt_CR_Acceptance_payer.Text = dt.Rows[0]["CR_Acceptance_Payer"].ToString();
            txt_CR_Accept_Commission_Payer.Text = dt.Rows[0]["CR_Acceptance_Comm_Payer"].ToString();
            txt_CR_Others_Payer.Text = dt.Rows[0]["CR_Others_Payer"].ToString();
            txt_CR_Their_Commission_Payer.Text = dt.Rows[0]["CR_Their_Comm_Payer"].ToString();
            txt_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_Current_Acc_Payer"].ToString();
            //txt_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["CR_Pay_Handle_Comm_Payer"].ToString();
            //txt_CR_Interest_payer.Text = dt.Rows[0]["CR_Interest_Payer"].ToString();
            //txt_DR_Cur_Acc_payer2.Text = dt.Rows[0]["DR_Current_Acc_Payer2"].ToString();
            //txt_DR_Cur_Acc_payer3.Text = dt.Rows[0]["DR_Current_Acc_Payer3"].ToString();

            //// General Operation swift Sfms GO_SWIFT_SFMS_Flag

            if (dt.Rows[0]["GO_SWIFT_SFMS_Flag"].ToString() == "Y")
            {
                chkGO_Swift_SFMS.Checked = true;

                GO_Swift_SFMS_Toggel();

                txt_GO_Swift_SFMS_Comment.Text = dt.Rows[0]["GO_SWIFT_SFMS_Comment"].ToString();
                txt_GO_Swift_SFMS_Memo.Text = dt.Rows[0]["GO_SWIFT_SFMS_Memo"].ToString();

                txt_GO_Swift_SFMS_Debit_Amt.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Amt"].ToString();

                txt_GO_Swift_SFMS_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Cust_AcCode"].ToString();
                txt_GO_Swift_SFMS_Debit_Cust.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Cust"].ToString();
                txt_GO_Swift_SFMS_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Cust_AccNo"].ToString();

                txt_GO_Swift_SFMS_Debit_Exch_Curr.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_ExchCCY"].ToString();
                txt_GO_Swift_SFMS_Debit_Exch_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_ExchRate"].ToString();
                txt_GO_Swift_SFMS_Debit_Entity.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Entity"].ToString();
                txt_GO_Swift_SFMS_Debit_AdPrint.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Advice_Print"].ToString();

                txt_GO_Swift_SFMS_Credit_Amt.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Amt"].ToString();

                txt_GO_Swift_SFMS_Credit_Cust.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Cust"].ToString();
                txt_GO_Swift_SFMS_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Cust_AcCode"].ToString();
                txt_GO_Swift_SFMS_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Cust_AccNo"].ToString();

                txt_GO_Swift_SFMS_Credit_Exch_Curr.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_ExchCCY"].ToString();
                txt_GO_Swift_SFMS_Credit_Exch_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_ExchRate"].ToString();
                txt_GO_Swift_SFMS_Credit_Entity.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Entity"].ToString();
                txt_GO_Swift_SFMS_Credit_AdPrint.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Advice_Print"].ToString();

                txt_GO_Swift_SFMS_Remarks.Text = dt.Rows[0]["GO_SWIFT_SFMS_Remark"].ToString();
                txt_GO_Swift_SFMS_Debit_Details.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Details"].ToString();
                txt_GO_Swift_SFMS_Credit_Details.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Details"].ToString();

                txt_GO_Swift_SFMS_Scheme_no.Text = dt.Rows[0]["GO_SWIFT_SFMS_SchemeNo"].ToString();
                txt_GO_Swift_SFMS_Debit_FUND.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Fund"].ToString();
                txt_GO_Swift_SFMS_Debit_Check_No.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_CheckNo"].ToString();
                txt_GO_Swift_SFMS_Debit_Available.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Available"].ToString();
                txt_GO_Swift_SFMS_Debit_Division.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Division"].ToString();
                txt_GO_Swift_SFMS_Debit_Inter_Amount.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_InterAmt"].ToString();
                txt_GO_Swift_SFMS_Debit_Inter_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_InterRate"].ToString();

                txt_GO_Swift_SFMS_Credit_FUND.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Fund"].ToString();
                txt_GO_Swift_SFMS_Credit_Check_No.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_CheckNo"].ToString();
                txt_GO_Swift_SFMS_Credit_Available.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Available"].ToString();
                txt_GO_Swift_SFMS_Credit_Division.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Division"].ToString();
                txt_GO_Swift_SFMS_Credit_Inter_Amount.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_InterAmt"].ToString();
                txt_GO_Swift_SFMS_Credit_Inter_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_InterRate"].ToString();
            }
            else
            {
                panalGO_Swift_SFMS.Visible = false;
            }

            if (dt.Rows[0]["GO_LC_Commitment_Flag"].ToString() == "Y")
            {
                chkGO_LC_Commitement.Checked = true;
                GO_LC_Commitement_Toggel();

                txt_GO_LC_Commitement_Comment.Text = dt.Rows[0]["GO_LC_Commitment_Comment"].ToString();
                txt_GO_LC_Commitement_MEMO.Text = dt.Rows[0]["GO_LC_Commitment_Memo"].ToString();

                txt_GO_LC_Commitement_Debit_Amt.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Amt"].ToString();

                txt_GO_LC_Commitement_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Cust_AcCode"].ToString();
                txt_GO_LC_Commitement_Debit_Cust.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Cust"].ToString();
                txt_GO_LC_Commitement_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Cust_AccNo"].ToString();

                txt_GO_LC_Commitement_Debit_Exch_Curr.Text = dt.Rows[0]["GO_LC_Commitment_Debit_ExchCCY"].ToString();
                txt_GO_LC_Commitement_Debit_Exch_Rate.Text = dt.Rows[0]["GO_LC_Commitment_Debit_ExchRate"].ToString();
                txt_GO_LC_Commitement_Debit_Entity.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Entity"].ToString();
                txt_GO_LC_Commitement_Debit_Advice_Print.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Advice_Print"].ToString();

                txt_GO_LC_Commitement_Credit_Amt.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Amt"].ToString();

                txt_GO_LC_Commitement_Credit_Cust.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Cust"].ToString();
                txt_GO_LC_Commitement_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Cust_AcCode"].ToString();
                txt_GO_LC_Commitement_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Cust_AccNo"].ToString();

                txt_GO_LC_Commitement_Credit_Exch_Curr.Text = dt.Rows[0]["GO_LC_Commitment_Credit_ExchCCY"].ToString();
                txt_GO_LC_Commitement_Credit_Exch_Rate.Text = dt.Rows[0]["GO_LC_Commitment_Credit_ExchRate"].ToString();
                txt_GO_LC_Commitement_Credit_Entity.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Entity"].ToString();
                txt_GO_LC_Commitement_Credit_Advice_Print.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Advice_Print"].ToString();

                txt_GO_LC_Commitement_Remarks.Text = dt.Rows[0]["GO_LC_Commitment_Remark"].ToString();
                txt_GO_LC_Commitement_Debit_Details.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Details"].ToString();
                txt_GO_LC_Commitement_Credit_Details.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Details"].ToString();

                txt_GO_LC_Commitement_Scheme_no.Text = dt.Rows[0]["GO_LC_Commitment_SchemeNo"].ToString();
                txt_GO_LC_Commitement_Debit_FUND.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Fund"].ToString();
                txt_GO_LC_Commitement_Debit_Check_No.Text = dt.Rows[0]["GO_LC_Commitment_Debit_CheckNo"].ToString();
                txt_GO_LC_Commitement_Debit_Available.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Available"].ToString();
                txt_GO_LC_Commitement_Debit_Division.Text = dt.Rows[0]["GO_LC_Commitment_Debit_Division"].ToString();
                txt_GO_LC_Commitement_Debit_Inter_Amount.Text = dt.Rows[0]["GO_LC_Commitment_Debit_InterAmt"].ToString();
                txt_GO_LC_Commitement_Debit_Inter_Rate.Text = dt.Rows[0]["GO_LC_Commitment_Debit_InterRate"].ToString();

                txt_GO_LC_Commitement_Credit_FUND.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Fund"].ToString();
                txt_GO_LC_Commitement_Credit_Check_No.Text = dt.Rows[0]["GO_LC_Commitment_Credit_CheckNo"].ToString();
                txt_GO_LC_Commitement_Credit_Available.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Available"].ToString();
                txt_GO_LC_Commitement_Credit_Division.Text = dt.Rows[0]["GO_LC_Commitment_Credit_Division"].ToString();
                txt_GO_LC_Commitement_Credit_Inter_Amount.Text = dt.Rows[0]["GO_LC_Commitment_Credit_InterAmt"].ToString();
                txt_GO_LC_Commitement_Credit_Inter_Rate.Text = dt.Rows[0]["GO_LC_Commitment_Credit_InterRate"].ToString();
            }
            else
            {
                panalGO_LC_Commitement.Visible = false;
            }

            //// swift files
            if (dt.Rows[0]["MTNone_Flag"].ToString() == "Y")
            {
                rdb_swift_None.Checked = true;
            }
            else
            {
                rdb_swift_None.Checked = false;
            }

            if (dt.Rows[0]["MT740_Flag"].ToString() == "Y")
            {
                rdb_swift_740.Checked = true;
            }
            else
            {
                rdb_swift_740.Checked = false;
            }

            if (dt.Rows[0]["MT756_Flag"].ToString() == "Y")
            {
                rdb_swift_756.Checked = true;
            }
            else
            {
                rdb_swift_756.Checked = false;
            }

            if (dt.Rows[0]["MT999_Flag"].ToString() == "Y")
            {
                rdb_swift_999.Checked = true;
            }
            else
            {
                rdb_swift_999.Checked = false;
            }

            if (dt.Rows[0]["MT799_Flag"].ToString() == "Y")
            {
                rdb_swift_799.Checked = true;
            }
            else
            {
                rdb_swift_799.Checked = false;
            }

            txt_Narrative_1.Text = dt.Rows[0]["Narrative1"].ToString();
            txt_Narrative_2.Text = dt.Rows[0]["Narrative2"].ToString();
            txt_Narrative_3.Text = dt.Rows[0]["Narrative3"].ToString();
            txt_Narrative_4.Text = dt.Rows[0]["Narrative4"].ToString();
            txt_Narrative_5.Text = dt.Rows[0]["Narrative5"].ToString();
            txt_Narrative_6.Text = dt.Rows[0]["Narrative6"].ToString();
            txt_Narrative_7.Text = dt.Rows[0]["Narrative7"].ToString();
            txt_Narrative_8.Text = dt.Rows[0]["Narrative8"].ToString();
            txt_Narrative_9.Text = dt.Rows[0]["Narrative9"].ToString();
            txt_Narrative_10.Text = dt.Rows[0]["Narrative10"].ToString();
            txt_Narrative_11.Text = dt.Rows[0]["Narrative11"].ToString();
            txt_Narrative_12.Text = dt.Rows[0]["Narrative12"].ToString();
            txt_Narrative_13.Text = dt.Rows[0]["Narrative13"].ToString();
            txt_Narrative_14.Text = dt.Rows[0]["Narrative14"].ToString();
            txt_Narrative_15.Text = dt.Rows[0]["Narrative15"].ToString();
            txt_Narrative_16.Text = dt.Rows[0]["Narrative16"].ToString();
            txt_Narrative_17.Text = dt.Rows[0]["Narrative17"].ToString();
            txt_Narrative_18.Text = dt.Rows[0]["Narrative18"].ToString();
            txt_Narrative_19.Text = dt.Rows[0]["Narrative19"].ToString();
            txt_Narrative_20.Text = dt.Rows[0]["Narrative20"].ToString();
            txt_Narrative_21.Text = dt.Rows[0]["Narrative21"].ToString();
            txt_Narrative_22.Text = dt.Rows[0]["Narrative22"].ToString();
            txt_Narrative_23.Text = dt.Rows[0]["Narrative23"].ToString();
            txt_Narrative_24.Text = dt.Rows[0]["Narrative24"].ToString();
            txt_Narrative_25.Text = dt.Rows[0]["Narrative25"].ToString();
            txt_Narrative_26.Text = dt.Rows[0]["Narrative26"].ToString();
            txt_Narrative_27.Text = dt.Rows[0]["Narrative27"].ToString();
            txt_Narrative_28.Text = dt.Rows[0]["Narrative28"].ToString();
            txt_Narrative_29.Text = dt.Rows[0]["Narrative29"].ToString();
            txt_Narrative_30.Text = dt.Rows[0]["Narrative30"].ToString();
            txt_Narrative_31.Text = dt.Rows[0]["Narrative31"].ToString();
            txt_Narrative_32.Text = dt.Rows[0]["Narrative32"].ToString();
            txt_Narrative_33.Text = dt.Rows[0]["Narrative33"].ToString();
            txt_Narrative_34.Text = dt.Rows[0]["Narrative34"].ToString();
            txt_Narrative_35.Text = dt.Rows[0]["Narrative35"].ToString();

            if (dt.Rows[0]["NegotiatingBankSwiftType"].ToString() != "")
            {
                txtNegoAccountNumber.Text = dt.Rows[0]["NegoAccountNumber_ACC"].ToString();
                if (dt.Rows[0]["NegotiatingBankSwiftType"].ToString() == "A")
                {
                    ddlNegotiatingBankSwift.SelectedValue = "A";
                    ddlNegotiatingBankSwift_SelectedIndexChanged(null, null);
                    txtNegoSwiftCode.Text = dt.Rows[0]["NegoSwiftCode_ACC"].ToString();
                }
                else
                {
                    ddlNegotiatingBankSwift.SelectedValue = "D";
                    ddlNegotiatingBankSwift_SelectedIndexChanged(null, null);
                    txtNegoName.Text = dt.Rows[0]["NegoName_ACC"].ToString();
                    txtNegoAddress1.Text = dt.Rows[0]["NegoAddress1_ACC"].ToString();
                    txtNegoAddress2.Text = dt.Rows[0]["NegoAddress2_ACC"].ToString();
                    txtNegoAddress3.Text = dt.Rows[0]["NegoAddress3_ACC"].ToString();
                }
            }
            else
            {
                ddlNegotiatingBankSwift.SelectedValue = "A";
                ddlNegotiatingBankSwift_SelectedIndexChanged(null, null);
            }
            //Added by bhupen on 23082022
            txt_740_documentaryCreditno.Text = dt.Rows[0]["DocumentCreditNumber_740"].ToString();
            txt_740_AccountIdentification.Text = dt.Rows[0]["AccountIdentification_740"].ToString();
            txt_740_Applicablerules.Text = dt.Rows[0]["ApplicableRules_740"].ToString();
            txt_740_Date.Text = dt.Rows[0]["DateandPlaceofExpiry_740"].ToString();
            txt_740_Placeofexpiry.Text = dt.Rows[0]["DateandPlaceofExpiryPlace_740"].ToString();
            txt_740_Draftsat1.Text = dt.Rows[0]["Draftsat1_740"].ToString();
            txt_740_Draftsat2.Text = dt.Rows[0]["Draftsat2_740"].ToString();
            txt_740_Draftsat3.Text = dt.Rows[0]["Draftsat3_740"].ToString();
            if (dt.Rows[0]["AvailableWithByHeader_740"].ToString() != "")
            {
                txtAvailablewithbyCode.Text = dt.Rows[0]["AvailableWithByCode_740"].ToString();
                if (dt.Rows[0]["AvailableWithByHeader_740"].ToString() == "A")
                {
                    ddlAvailablewithby_740.SelectedValue = "A";
                    ddlAvailablewithby_740_TextChanged(null, null);
                    txtAvailablewithbySwiftCode.Text = dt.Rows[0]["AvailableWithBySwiftCode_740"].ToString();
                }
                else
                {
                    ddlAvailablewithby_740.SelectedValue = "D";
                    ddlAvailablewithby_740_TextChanged(null, null);
                    txtAvailablewithbyName.Text = dt.Rows[0]["AvailableWithByName_740"].ToString();
                    txtAvailablewithbyAddress1.Text = dt.Rows[0]["AvailableWithByAddress1_740"].ToString();
                    txtAvailablewithbyAddress2.Text = dt.Rows[0]["AvailableWithByAddress2_740"].ToString();
                    txtAvailablewithbyAddress3.Text = dt.Rows[0]["AvailableWithByAddress3_740"].ToString();
                }
            }
            else
            {
                ddlAvailablewithby_740.SelectedValue = "A";
                ddlAvailablewithby_740_TextChanged(null, null);
            }
            if (dt.Rows[0]["DraweeHeader_740"].ToString() != "")
            {
                txtDraweeAccountNumber.Text = dt.Rows[0]["DraweeAccountNumber_740"].ToString();
                if (dt.Rows[0]["DraweeHeader_740"].ToString() == "A")
                {
                    ddlDrawee_740.SelectedValue = "A";
                    ddlDrawee_740_TextChanged(null, null);
                    txtDraweeSwiftCode.Text = dt.Rows[0]["DraweeSwiftCode_740"].ToString();
                }
                else
                {
                    ddlDrawee_740.SelectedValue = "D";
                    ddlDrawee_740_TextChanged(null, null);
                    txtDraweeName.Text = dt.Rows[0]["DraweeName_740"].ToString();
                    txtDraweeAddress1.Text = dt.Rows[0]["DraweeAddress1_740"].ToString();
                    txtDraweeAddress2.Text = dt.Rows[0]["DraweeAddress2_740"].ToString();
                    txtDraweeAddress3.Text = dt.Rows[0]["DraweeAddress3_740"].ToString();
                }
            }
            else
            {
                ddlDrawee_740.SelectedValue = "A";
                ddlDrawee_740_TextChanged(null, null);
            }
            txt_Acceptance_Beneficiary5.Text = dt.Rows[0]["Beneficiary_ACC5"].ToString();

            txt_Acceptance_Beneficiary.Text = dt.Rows[0]["Beneficiary_ACC1"].ToString();
            txt_Acceptance_Beneficiary2.Text = dt.Rows[0]["Beneficiary_ACC2"].ToString();
            txt_Acceptance_Beneficiary3.Text = dt.Rows[0]["Beneficiary_ACC3"].ToString();
            txt_Acceptance_Beneficiary4.Text = dt.Rows[0]["Beneficiary_ACC4"].ToString();
            txtCreditCurrency.Text = dt.Rows[0]["CreditCurrency_ACC"].ToString();
            txtCreditAmount.Text = dt.Rows[0]["CreditAmount_ACC"].ToString();
            txtPercentageCreditAmountTolerance.Text = dt.Rows[0]["PercentageCreditAmountTolerance"].ToString();
            txtPercentageCreditAmountTolerance1.Text = dt.Rows[0]["PercentageCreditAmountTolerance1"].ToString();
            txt_Acceptance_Max_Credit_Amt.Text = dt.Rows[0]["Max_Credit_Amt_ACC"].ToString();
            txt_Acceptance_Additional_Amt_Covered.Text = dt.Rows[0]["Additional_Amt_Covered_ACC"].ToString();
            txt_Acceptance_Additional_Amt_Covered2.Text = dt.Rows[0]["Additional_Amt_Covered_ACC2"].ToString();
            txt_Acceptance_Additional_Amt_Covered3.Text = dt.Rows[0]["Additional_Amt_Covered_ACC3"].ToString();
            txt_Acceptance_Additional_Amt_Covered4.Text = dt.Rows[0]["Additional_Amt_Covered_ACC4"].ToString();
            txtMixedPaymentDetails.Text = dt.Rows[0]["MixedPaymentDetails"].ToString();
            txtMixedPaymentDetails2.Text = dt.Rows[0]["MixedPaymentDetails2"].ToString();
            txtMixedPaymentDetails3.Text = dt.Rows[0]["MixedPaymentDetails3"].ToString();
            txtMixedPaymentDetails4.Text = dt.Rows[0]["MixedPaymentDetails4"].ToString();
            txtDeferredPaymentDetails.Text = dt.Rows[0]["DeferredPaymentDetails"].ToString();
            txtDeferredPaymentDetails2.Text = dt.Rows[0]["DeferredPaymentDetails2"].ToString();
            txtDeferredPaymentDetails3.Text = dt.Rows[0]["DeferredPaymentDetails3"].ToString();
            txtDeferredPaymentDetails4.Text = dt.Rows[0]["DeferredPaymentDetails4"].ToString();
            txt_Acceptance_Reimbur_Bank_Charges.Text = dt.Rows[0]["Reimbur_Bank_Charges_ACC"].ToString();
            txt_Acceptance_Other_Charges.Text = dt.Rows[0]["Other_Charges_ACC"].ToString();
            txt_Acceptance_Other_Charges2.Text = dt.Rows[0]["Other_Charges_ACC2"].ToString();
            txt_Acceptance_Other_Charges3.Text = dt.Rows[0]["Other_Charges_ACC3"].ToString();
            txt_Acceptance_Other_Charges4.Text = dt.Rows[0]["Other_Charges_ACC4"].ToString();
            txt_Acceptance_Other_Charges5.Text = dt.Rows[0]["Other_Charges_ACC5"].ToString();
            txt_Acceptance_Other_Charges6.Text = dt.Rows[0]["Other_Charges_ACC6"].ToString();
            txt_Acceptance_Sender_to_Receiver_Information.Text = dt.Rows[0]["Sender_to_Receiver_Information"].ToString();
            txt_Acceptance_Sender_to_Receiver_Information2.Text = dt.Rows[0]["Sender_to_Receiver_Information2"].ToString();
            txt_Acceptance_Sender_to_Receiver_Information3.Text = dt.Rows[0]["Sender_to_Receiver_Information3"].ToString();
            txt_Acceptance_Sender_to_Receiver_Information4.Text = dt.Rows[0]["Sender_to_Receiver_Information4"].ToString();
            txt_Acceptance_Sender_to_Receiver_Information5.Text = dt.Rows[0]["Sender_to_Receiver_Information5"].ToString();
            txt_Acceptance_Sender_to_Receiver_Information6.Text = dt.Rows[0]["Sender_to_Receiver_Information6"].ToString();

            //MT 756
            if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() != "")
            {
                txtReceiverAccountNumberMT.Text = dt.Rows[0]["ReceiverAccountNumberMT"].ToString();
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "A")
                {
                    ddlReceiverCorrespondentMT.SelectedValue = "A";
                    ddlReceiverCorrespondentMT_TextChanged(null, null);
                    txtReceiverSwiftCodeMT.Text = dt.Rows[0]["ReceiverSwiftCodeMT"].ToString();
                }
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "B")
                {
                    ddlReceiverCorrespondentMT.SelectedValue = "B";
                    ddlReceiverCorrespondentMT_TextChanged(null, null);
                    txtReceiverLocationMT.Text = dt.Rows[0]["ReceiverLocationMT"].ToString();
                }
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "D")
                {
                    ddlReceiverCorrespondentMT.SelectedValue = "D";
                    ddlReceiverCorrespondentMT_TextChanged(null, null);
                    txtReceiverNameMT.Text = dt.Rows[0]["ReceiverNameMT"].ToString();
                    txtReceiverAddress1MT.Text = dt.Rows[0]["ReceiverAddress1MT"].ToString();
                    txtReceiverAddress2MT.Text = dt.Rows[0]["ReceiverAddress2MT"].ToString();
                    txtReceiverAddress3MT.Text = dt.Rows[0]["ReceiverAddress3MT"].ToString();
                }
            }
            else
            {
                ddlReceiverCorrespondentMT.SelectedValue = "A";
                ddlReceiverCorrespondentMT_TextChanged(null, null);
            }

            txt_Narrative_756_1.Text = dt.Rows[0]["Narrative7561"].ToString();
            txt_Narrative_756_2.Text = dt.Rows[0]["Narrative7562"].ToString();
            txt_Narrative_756_3.Text = dt.Rows[0]["Narrative7563"].ToString();
            txt_Narrative_756_4.Text = dt.Rows[0]["Narrative7564"].ToString();
            txt_Narrative_756_5.Text = dt.Rows[0]["Narrative7565"].ToString();
            txt_Narrative_756_6.Text = dt.Rows[0]["Narrative7566"].ToString();
            txt_Narrative_756_7.Text = dt.Rows[0]["Narrative7567"].ToString();
            txt_Narrative_756_8.Text = dt.Rows[0]["Narrative7568"].ToString();
            txt_Narrative_756_9.Text = dt.Rows[0]["Narrative7569"].ToString();
            txt_Narrative_756_10.Text = dt.Rows[0]["Narrative75610"].ToString();
            txt_Narrative_756_11.Text = dt.Rows[0]["Narrative75611"].ToString();
            txt_Narrative_756_12.Text = dt.Rows[0]["Narrative75612"].ToString();
            txt_Narrative_756_13.Text = dt.Rows[0]["Narrative75613"].ToString();
            txt_Narrative_756_14.Text = dt.Rows[0]["Narrative75614"].ToString();
            txt_Narrative_756_15.Text = dt.Rows[0]["Narrative75615"].ToString();
            txt_Narrative_756_16.Text = dt.Rows[0]["Narrative75616"].ToString();
            txt_Narrative_756_17.Text = dt.Rows[0]["Narrative75617"].ToString();
            txt_Narrative_756_18.Text = dt.Rows[0]["Narrative75618"].ToString();
            txt_Narrative_756_19.Text = dt.Rows[0]["Narrative75619"].ToString();
            txt_Narrative_756_20.Text = dt.Rows[0]["Narrative75620"].ToString();
            txt_Narrative_756_21.Text = dt.Rows[0]["Narrative75621"].ToString();
            txt_Narrative_756_22.Text = dt.Rows[0]["Narrative75622"].ToString();
            txt_Narrative_756_23.Text = dt.Rows[0]["Narrative75623"].ToString();
            txt_Narrative_756_24.Text = dt.Rows[0]["Narrative75624"].ToString();
            txt_Narrative_756_25.Text = dt.Rows[0]["Narrative75625"].ToString();
            txt_Narrative_756_26.Text = dt.Rows[0]["Narrative75626"].ToString();
            txt_Narrative_756_27.Text = dt.Rows[0]["Narrative75627"].ToString();
            txt_Narrative_756_28.Text = dt.Rows[0]["Narrative75628"].ToString();
            txt_Narrative_756_29.Text = dt.Rows[0]["Narrative75629"].ToString();
            txt_Narrative_756_30.Text = dt.Rows[0]["Narrative75630"].ToString();
            txt_Narrative_756_31.Text = dt.Rows[0]["Narrative75631"].ToString();
            txt_Narrative_756_32.Text = dt.Rows[0]["Narrative75632"].ToString();
            txt_Narrative_756_33.Text = dt.Rows[0]["Narrative75633"].ToString();
            txt_Narrative_756_34.Text = dt.Rows[0]["Narrative75634"].ToString();
            txt_Narrative_756_35.Text = dt.Rows[0]["Narrative75635"].ToString();

            //SFMS 756
            if (dt.Rows[0]["SenderCorrespondentSFMS"].ToString() != "")
            {
                txtSenderAccountNumberSFMS.Text = dt.Rows[0]["SenderAccountNumberSFMS"].ToString();
                if (dt.Rows[0]["SenderCorrespondentSFMS"].ToString() == "A")
                {
                    ddlSenderCorrespondentSFMS.SelectedValue = "A";
                    ddlSenderCorrespondentSFMS_TextChanged(null, null);
                    txtSenderSwiftCodeSFMS.Text = dt.Rows[0]["SenderSwiftCodeSFMS"].ToString();
                }
                if (dt.Rows[0]["SenderCorrespondentSFMS"].ToString() == "B")
                {
                    ddlSenderCorrespondentSFMS.SelectedValue = "B";
                    ddlSenderCorrespondentSFMS_TextChanged(null, null);
                    txtSenderLocationSFMS.Text = dt.Rows[0]["SenderLocationSFMS"].ToString();
                }
                if (dt.Rows[0]["SenderCorrespondentSFMS"].ToString() == "D")
                {
                    ddlSenderCorrespondentSFMS.SelectedValue = "D";
                    ddlSenderCorrespondentSFMS_TextChanged(null, null);
                    txtSenderNameSFMS.Text = dt.Rows[0]["SenderNameSFMS"].ToString();
                    txtSenderAddress1SFMS.Text = dt.Rows[0]["SenderAddress1SFMS"].ToString();
                    txtSenderAddress2SFMS.Text = dt.Rows[0]["SenderAddress2SFMS"].ToString();
                    txtSenderAddress3SFMS.Text = dt.Rows[0]["SenderAddress3SFMS"].ToString();
                }
            }
            else
            {
                ddlSenderCorrespondentSFMS.SelectedValue = "A";
                ddlSenderCorrespondentSFMS_TextChanged(null, null);
            }

            if (dt.Rows[0]["ReceiverCorrespondentSFMS"].ToString() != "")
            {
                txtReceiverAccountNumberSFMS.Text = dt.Rows[0]["ReceiverAccountNumberSFMS"].ToString();
                if (dt.Rows[0]["ReceiverCorrespondentSFMS"].ToString() == "A")
                {
                    ddlReceiverCorrespondentSFMS.SelectedValue = "A";
                    ddlReceiverCorrespondentSFMS_TextChanged(null, null);
                    txtReceiverSwiftCodeSFMS.Text = dt.Rows[0]["ReceiverSwiftCodeSFMS"].ToString();
                }
                if (dt.Rows[0]["ReceiverCorrespondentSFMS"].ToString() == "B")
                {
                    ddlReceiverCorrespondentSFMS.SelectedValue = "B";
                    ddlReceiverCorrespondentSFMS_TextChanged(null, null);
                    txtReceiverLocationSFMS.Text = dt.Rows[0]["ReceiverLocationSFMS"].ToString();
                }
                if (dt.Rows[0]["ReceiverCorrespondentSFMS"].ToString() == "D")
                {
                    ddlReceiverCorrespondentSFMS.SelectedValue = "D";
                    ddlReceiverCorrespondentSFMS_TextChanged(null, null);
                    txtReceiverNameSFMS.Text = dt.Rows[0]["ReceiverNameSFMS"].ToString();
                    txtReceiverAddress1SFMS.Text = dt.Rows[0]["ReceiverAddress1SFMS"].ToString();
                    txtReceiverAddress2SFMS.Text = dt.Rows[0]["ReceiverAddress2SFMS"].ToString();
                    txtReceiverAddress3SFMS.Text = dt.Rows[0]["ReceiverAddress3SFMS"].ToString();
                }
            }
            else
            {
                ddlReceiverCorrespondentSFMS.SelectedValue = "A";
                ddlReceiverCorrespondentSFMS_TextChanged(null, null);
            }

            txt_Discrepancy_Charges_SFMS.Text = dt.Rows[0]["Discrepancy_Charges_SFMS"].ToString();
            txt_Discrepancy_Charges_Swift.Text = dt.Rows[0]["Discrepancy_Charges_Swift"].ToString();

            if (dt.Rows[0]["Ledger_File_Flag"].ToString() == "Y")
            {
                chk_Ledger_Modify.Checked = true;
                chk_Ledger_Modify_OnCheckedChanged(null, null);

                txtLedgerRemark.Text = dt.Rows[0]["Ledger_Remark"].ToString();
                txtLedgerCustName.Text = dt.Rows[0]["Ledger_Cust_AccNo"].ToString();
                txtLedgerCURR.Text = dt.Rows[0]["Ledger_Curr"].ToString();
                txtLedgerAccode.Text = dt.Rows[0]["Ledger_AccCode"].ToString();
                txtLedgerRefNo.Text = dt.Rows[0]["Ledger_Ref_No"].ToString();
                txtLedger_Modify_amt.Text = dt.Rows[0]["Ledger_Amount"].ToString();
                txtLedgerOperationDate.Text = dt.Rows[0]["Ledger_Operation_Date"].ToString();
                txtLedgerBalanceAmt.Text = dt.Rows[0]["Ledger_Balance_Amt"].ToString();
                txtLedgerAcceptDate.Text = dt.Rows[0]["Ledger_Accept_Date"].ToString();
                txtLedgerMaturity.Text = dt.Rows[0]["Ledger_Maturity"].ToString();
                txtLedgerSettlememtDate.Text = dt.Rows[0]["Ledger_Settlement_Date"].ToString();
                txtLedgerSettlValue.Text = dt.Rows[0]["Ledger_Settlement_Value"].ToString();
                txtLedgerLastModDate.Text = dt.Rows[0]["Ledger_Last_Mod_Date"].ToString();
                txtLedgerREM_EUC.Text = dt.Rows[0]["Ledger_Rem_EUC"].ToString();
                txtLedgerLastOPEDate.Text = dt.Rows[0]["Ledger_Last_OPE_Date"].ToString();
                txtLedgerTransNo.Text = dt.Rows[0]["Ledger_Trans_No"].ToString();
                txtLedgerContraCountry.Text = dt.Rows[0]["Ledger_Contra_Country"].ToString();
                txtLedgerStatusCode.Text = dt.Rows[0]["Ledger_Status_Code"].ToString();
                txtLedgerCollectOfComm.Text = dt.Rows[0]["Ledger_Collect_Of_Cumm"].ToString();
                txtLedgerCommodity.Text = dt.Rows[0]["Ledger_Commodity"].ToString();
                txtLedgerhandlingCommRate.Text = dt.Rows[0]["Ledger_Handling_Comm_Rate"].ToString();
                txtLedgerhandlingCommCurr.Text = dt.Rows[0]["Ledger_Handling_Comm_Curr"].ToString();
                txtLedgerhandlingCommAmt.Text = dt.Rows[0]["Ledger_Handling_Comm_Amt"].ToString();
                txtLedgerPostageRate.Text = dt.Rows[0]["Ledger_Postage_Rate"].ToString();
                txtLedgerPostageCurr.Text = dt.Rows[0]["Ledger_Postage_Curr"].ToString();
                txtLedgerPostageAmt.Text = dt.Rows[0]["Ledger_Postage_Amt"].ToString();
                txtLedgerPostagePayer.Text = dt.Rows[0]["Ledger_Postage_Payer"].ToString();
                txtLedgerOthersRate.Text = dt.Rows[0]["Ledger_Others_Rate"].ToString();
                txtLedgerOthersCurr.Text = dt.Rows[0]["Ledger_Others_Curr"].ToString();
                txtLedgerOthersAmt.Text = dt.Rows[0]["Ledger_Others_Amt"].ToString();
                txtLedgerOthersPayer.Text = dt.Rows[0]["Ledger_Others_Payer"].ToString();
                txtLedgerTheirCommRate.Text = dt.Rows[0]["Ledger_Their_Comm_Rate"].ToString();
                txtLedgerTheirCommCurr.Text = dt.Rows[0]["Ledger_Their_Comm_Curr"].ToString();
                txtLedgerTheirCommAmt.Text = dt.Rows[0]["Ledger_Their_Comm_Amt"].ToString();
                txtLedgerTheirCommPayer.Text = dt.Rows[0]["Ledger_Their_Comm_Payer"].ToString();
                txtLedgerNegoBank.Text = dt.Rows[0]["Ledger_Collect_Nego_Bank"].ToString();
                txtLedgerReimbursingBank.Text = dt.Rows[0]["Ledger_Reimbursing_Bank"].ToString();
                txtLedgerDrawer.Text = dt.Rows[0]["Ledger_Drawer_Drawee"].ToString();
                txtLedgerTenor.Text = dt.Rows[0]["Ledger_Tenor"].ToString();
                txtLedgerAttn.Text = dt.Rows[0]["Ledger_Attn"].ToString();
            }
            else
            {
                chk_Ledger_Modify.Checked = false;
                chk_Ledger_Modify_OnCheckedChanged(null, null);
            }
           // txtNegoPartyIdentifier.Text = dt.Rows[0]["NegoPartyIdentifier"].ToString();
            txtReceiverPartyIdentifier.Text = dt.Rows[0]["ReceiverPartyIdentifier"].ToString();
            txtSenderPartyIdentifier.Text = dt.Rows[0]["SenderPartyIdentifierSFMS"].ToString();
            txtReceiverPartyIdentifierSFMS.Text = dt.Rows[0]["ReceiverPartyIdentifierSFMS"].ToString();

            // -------------- 747 Bhupen------------------------------------------//
            if (dt.Rows[0]["MT747_Flag"].ToString() == "Y")
            {
                rdb_swift_747.Checked = true;
            }
            else
            {
                rdb_swift_747.Checked = false;
            }

            txt_747_documentaryCredtno.Text = dt.Rows[0]["DocCreditNo_747"].ToString();
            txt_747_reimbursingbankRef.Text = dt.Rows[0]["ReimbursingBankRef_747"].ToString();
            txt_747_DateofogauthriztnRmburse.Text = dt.Rows[0]["DateoforiginalAuthoOfReimburse_747"].ToString();
            txt_747_NewDateofExpiry.Text = dt.Rows[0]["NewDateOfExpiry_747"].ToString();
            txt_747_IncreaseofDocumentaryCreditCurr.Text = dt.Rows[0]["IncreaseofDocumentryCreditAmt_Curr_747"].ToString();
            txt_747_IncreaseofDocumentaryCreditAmt.Text = dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString();
            txt_747_DecreaseofDocumentaryCreditCurr.Text = dt.Rows[0]["decreaseofDocumentryCreditAmt_Curr_747"].ToString();
            txt_747_DecreaseofDocumentaryCreditAmt.Text = dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString();
            txt_747_NewDocumentaryCreditAmtAfterAmendmentCurr.Text = dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_Curr_747"].ToString();
            txt_747_NewDocumentaryCreditAmtAfterAmendment.Text = dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString();
            txt_747_PercentageCreditAmtTolerance1.Text = dt.Rows[0]["PercentageCreditAmtTolerance1_747"].ToString();
            txt_747_PercentageCreditAmtTolerance2.Text = dt.Rows[0]["PercentageCreditAmtTolerance2_747"].ToString();
            txt_747_AdditionalAmtsCovered1.Text = dt.Rows[0]["AddAmtCovered1_747"].ToString();
            txt_747_AdditionalAmtsCovered2.Text = dt.Rows[0]["AddAmtCovered2_747"].ToString();
            txt_747_AdditionalAmtsCovered3.Text = dt.Rows[0]["AddAmtCovered3_747"].ToString();
            txt_747_AdditionalAmtsCovered4.Text = dt.Rows[0]["AddAmtCovered4_747"].ToString();
            txt_747_SentoReceivInfo1.Text = dt.Rows[0]["SenToRecInfo1_747"].ToString();
            txt_747_SentoReceivInfo2.Text = dt.Rows[0]["SenToRecInfo2_747"].ToString();
            txt_747_SentoReceivInfo3.Text = dt.Rows[0]["SenToRecInfo3_747"].ToString();
            txt_747_SentoReceivInfo4.Text = dt.Rows[0]["SenToRecInfo4_747"].ToString();
            txt_747_SentoReceivInfo5.Text = dt.Rows[0]["SenToRecInfo5_747"].ToString();
            txt_747_SentoReceivInfo6.Text = dt.Rows[0]["SenToRecInfo6_747"].ToString();
            txt_Narrative_747_1.Text = dt.Rows[0]["Narrative1_747"].ToString();
            txt_Narrative_747_2.Text = dt.Rows[0]["Narrative2_747"].ToString();
            txt_Narrative_747_3.Text = dt.Rows[0]["Narrative3_747"].ToString();
            txt_Narrative_747_4.Text = dt.Rows[0]["Narrative4_747"].ToString();
            txt_Narrative_747_5.Text = dt.Rows[0]["Narrative5_747"].ToString();
            txt_Narrative_747_6.Text = dt.Rows[0]["Narrative6_747"].ToString();
            txt_Narrative_747_7.Text = dt.Rows[0]["Narrative7_747"].ToString();
            txt_Narrative_747_8.Text = dt.Rows[0]["Narrative8_747"].ToString();
            txt_Narrative_747_9.Text = dt.Rows[0]["Narrative9_747"].ToString();
            txt_Narrative_747_10.Text = dt.Rows[0]["Narrative10_747"].ToString();
            txt_Narrative_747_11.Text = dt.Rows[0]["Narrative11_747"].ToString();
            txt_Narrative_747_12.Text = dt.Rows[0]["Narrative12_747"].ToString();
            txt_Narrative_747_13.Text = dt.Rows[0]["Narrative13_747"].ToString();
            txt_Narrative_747_14.Text = dt.Rows[0]["Narrative14_747"].ToString();
            txt_Narrative_747_15.Text = dt.Rows[0]["Narrative15_747"].ToString();
            txt_Narrative_747_16.Text = dt.Rows[0]["Narrative16_747"].ToString();
            txt_Narrative_747_17.Text = dt.Rows[0]["Narrative17_747"].ToString();
            txt_Narrative_747_18.Text = dt.Rows[0]["Narrative18_747"].ToString();
            txt_Narrative_747_19.Text = dt.Rows[0]["Narrative19_747"].ToString();
            txt_Narrative_747_20.Text = dt.Rows[0]["Narrative20_747"].ToString();

            hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
        }
    }
    protected void fillLCDetails(string LCNo, string DocNo, string CustAbbr, string Foreign_Local)
    {
        SqlParameter p1 = new SqlParameter("@LCNo", LCNo);
        SqlParameter p2 = new SqlParameter("@RefNo", DocNo);
        SqlParameter p3 = new SqlParameter("@CustAbbr", CustAbbr);
        SqlParameter p4 = new SqlParameter("@LocalForeign", Foreign_Local);
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_FillLCDetails", p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            lblLC_Balance.Text = dt.Rows[0]["BalAmount"].ToString();
            if (float.Parse(lblBillAmt.Text) > float.Parse(lblLC_Balance.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Bill Amount is greater than LC Balance Amount.')", true);
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker_View.aspx", true);
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
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "Local";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblCollection_Lodgment_UnderLC.Text = "Lodgment_Under_LC";
                lblSight_Usance.Text = "Usance";
                txtSettl_CodeForBank.Text = "09";

                lbl_Instructions1.Text = "KINDLY RETURN ATTACHED TRUST RECEIPT OFFICIALLY SIGNED.";
                lbl_Instructions2.Text = "DAYS) AS PER ARTICLE 16(D) OF UCP 600";
                lbl_Instructions3.Text = "2)WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                lbl_Instructions4.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                break;
        }
    }
    protected void MakeReadOnly()
    {
        txtDocNo.Enabled = false;
        txtCustomer_ID.Enabled = false;
        txtHO_Apl.Enabled = false;
        txtIBD_ACC_kind.Enabled = false;
        txtValueDate.Enabled = false;
        txtCommentCode.Enabled = false;
        txtAutoSettlement.Enabled = false;
        txtDraftAmt.Enabled = false;
        txt_Doc_Curr.Enabled = false;
        txtLCNo.Enabled = false;
        txtIBDAmt.Enabled = false;
        txtCountryRisk.Enabled = false;
        txtGradeCode.Enabled = false;
        txtRiskCust.Enabled = false;
        txtsettlCodeForCust.Enabled = false;
        txtsettlforCust_Abbr.Enabled = false;
        txtsettlforCust_AccCode.Enabled = false;
        txtsettlforCust_AccNo.Enabled = false;
        txtInterest_From.Enabled = false;
        txtInterest_To.Enabled = false;
        txt_No_Of_Days.Enabled = false;
        txtSettl_CodeForBank.Enabled = false;
        txtSettl_ForBank_AccCode.Enabled = false;
        txtSettl_ForBank_AccNo.Enabled = false;
        txtNego_Remit_Bank.Enabled = false;
        txtAcwithInstitution.Enabled = false;
        txtReimbursingbank.Enabled = false;
        txtNego_Remit_Bank_Type.Enabled = false;
        ////Instructions
        txtDrawer.Enabled = false;
        txtTenor.Enabled = false;
        txtTenor_Description.Enabled = false;
        txtDateArrival.Enabled = false;
        btncal_Arrival_date.Enabled = false;
        txtNego_Remit_Bank.Enabled = false;
        txt_Their_Ref_no.Enabled = false;
        txtAcwithInstitution.Enabled = false;
        txtReimbursingbank.Enabled = false;
        txtCommodity.Enabled = false;
        txtCommodityDesc.Enabled = false;
        txtShippingDate.Enabled = false;
        btnCal_Ship_Date.Enabled = false;
        txtDocFirst1.Enabled = false;
        txtDocFirst2.Enabled = false;
        txtDocFirst3.Enabled = false;
        txtVesselName.Enabled = false;
        txtDocSecond1.Enabled = false;
        txtDocSecond2.Enabled = false;
        txtDocSecond3.Enabled = false;
        txtFromPort.Enabled = false;
        txtToPort.Enabled = false;

        rdb_SP_Instr_Other.Enabled = false;
        rdb_SP_Instr_Annexure.Enabled = false;
        rdb_SP_Instr_On_Sight.Enabled = false;
        rdb_SP_Instr_On_Date.Enabled = false;

        txt_SP_Instructions1.Enabled = false;
        txt_SP_Instructions2.Enabled = false;
        txt_SP_Instructions3.Enabled = false;
        txt_SP_Instructions4.Enabled = false;
        txt_SP_Instructions5.Enabled = false;

        txtExchRate.Enabled = false;
        txtPurpose.Enabled = false;
        ddl_PurposeCode.Enabled = false;
        txt_INT_Rate.Enabled = false;
        txtBaseRate.Enabled = false;
        txtSpread.Enabled = false;
        txtInterestAmt.Enabled = false;
        txtFundType.Enabled = false;
        txtInternalRate.Enabled = false;
        txtSettl_ForBank_Abbr.Enabled = false;
        txtAttn.Enabled = false;
        txtREM_EUC.Enabled = false;
        txt_INST_Code.Enabled = false;
        txtContractNo.Enabled = false;
        txtApplNo.Enabled = false;
        txtApplBR.Enabled = false;
        ddlTenor.Enabled = false;
        ddlTenor_Days_From.Enabled = false;

        //import accounting
        chk_IMP_ACC_Commission.Enabled = false;
        txt_IMP_ACC_ExchRate.Enabled = false;
        txtPrinc_matu.Enabled = false;
        txtPrinc_lump.Enabled = false;
        txtprinc_Contract_no.Enabled = false;
        txt_Princ_Ex_Curr.Enabled = false;
        txtprinc_Ex_rate.Enabled = false;
        txtprinc_Intnl_Ex_rate.Enabled = false;
        txtInterest_matu.Enabled = false;
        txtInterest_lump.Enabled = false;
        txtInterest_Contract_no.Enabled = false;
        txt_interest_Ex_Curr.Enabled = false;
        txtInterest_Ex_rate.Enabled = false;
        txtInterest_Intnl_Ex_rate.Enabled = false;
        txtCommission_matu.Enabled = false;
        txtCommission_lump.Enabled = false;
        txtCommission_Contract_no.Enabled = false;
        txt_Commission_Ex_Curr.Enabled = false;
        txtCommission_Ex_rate.Enabled = false;
        txtCommission_Intnl_Ex_rate.Enabled = false;
        txtTheir_Commission_matu.Enabled = false;
        txtTheir_Commission_lump.Enabled = false;
        txtTheir_Commission_Contract_no.Enabled = false;
        txt_Their_Commission_Ex_Curr.Enabled = false;
        txtTheir_Commission_Ex_rate.Enabled = false;
        txtTheir_Commission_Intnl_Ex_rate.Enabled = false;
        txt_CR_Code.Enabled = false;
        txt_CR_Cust_abbr.Enabled = false;
        txt_CR_Cust_Acc.Enabled = false;
        txt_CR_Acceptance_Curr.Enabled = false;
        txt_CR_Acceptance_amt.Enabled = false;
        txt_CR_Acceptance_payer.Enabled = false;
        txt_CR_Interest_Curr.Enabled = false;
        txt_CR_Interest_amt.Enabled = false;
        txt_CR_Interest_payer.Enabled = false;
        txt_CR_Accept_Commission_Curr.Enabled = false;
        txt_CR_Accept_Commission_amt.Enabled = false;
        txt_CR_Accept_Commission_Payer.Enabled = false;
        txt_CR_Pay_Handle_Commission_Curr.Enabled = false;
        txt_CR_Pay_Handle_Commission_amt.Enabled = false;
        txt_CR_Pay_Handle_Commission_Payer.Enabled = false;
        txt_CR_Others_Curr.Enabled = false;
        txt_CR_Others_amt.Enabled = false;
        txt_CR_Others_Payer.Enabled = false;
        txt_CR_Their_Commission_Curr.Enabled = false;
        txt_CR_Their_Commission_amt.Enabled = false;
        txt_CR_Their_Commission_Payer.Enabled = false;
        //txt_DR_Code_Curr.Enabled = false;
        txt_DR_Code.Enabled = false;
        txt_DR_Cust_abbr.Enabled = false;
        txt_DR_Cust_Acc.Enabled = false;
        txt_DR_Cur_Acc_Curr.Enabled = false;
        txt_DR_Cur_Acc_amt.Enabled = false;
        txt_DR_Cur_Acc_payer.Enabled = false;
        txt_DR_Cur_Acc_Curr2.Enabled = false;
        txt_DR_Cur_Acc_amt2.Enabled = false;
        txt_DR_Cur_Acc_payer2.Enabled = false;
        txt_DR_Cur_Acc_Curr3.Enabled = false;
        txt_DR_Cur_Acc_amt3.Enabled = false;
        txt_DR_Cur_Acc_payer3.Enabled = false;
        txt_DiscAmt.Enabled = false;

        // General Operation
        chkGO_Swift_SFMS.Enabled = false;
        chkGO_LC_Commitement.Enabled = false;
        txt_GO_Swift_SFMS_Ref_No.Enabled = false;
        txt_GO_Swift_SFMS_Comment.Enabled = false;
        txt_GO_Swift_SFMS_SectionNo.Enabled = false;
        txt_GO_Swift_SFMS_Remarks.Enabled = false;
        txt_GO_Swift_SFMS_Memo.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Amt.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Cust.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Cust_AcCode.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Cust_AccNo.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Exch_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Exch_Rate.Enabled = false;
        txt_GO_Swift_SFMS_Debit_AdPrint.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Details.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Entity.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Amt.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Cust.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Cust_AcCode.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Cust_AccNo.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Exch_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Exch_Rate.Enabled = false;
        txt_GO_Swift_SFMS_Credit_AdPrint.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Details.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Entity.Enabled = false;
        txt_GO_Swift_SFMS_Scheme_no.Enabled = false;
        txt_GO_Swift_SFMS_Debit_FUND.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Check_No.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Available.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Division.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Inter_Amount.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Inter_Rate.Enabled = false;

        txt_GO_Swift_SFMS_Credit_FUND.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Check_No.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Available.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Division.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Inter_Amount.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Inter_Rate.Enabled = false;


        txt_GO_LC_Commitement_Ref_No.Enabled = false;
        txt_GO_LC_Commitement_Comment.Enabled = false;
        txt_GO_LC_Commitement_SectionNo.Enabled = false;
        txt_GO_LC_Commitement_Remarks.Enabled = false;
        txt_GO_LC_Commitement_MEMO.Enabled = false;
        txt_GO_LC_Commitement_Debit_Code.Enabled = false;
        txt_GO_LC_Commitement_Debit_Curr.Enabled = false;
        txt_GO_LC_Commitement_Debit_Amt.Enabled = false;
        txt_GO_LC_Commitement_Debit_Cust.Enabled = false;
        txt_GO_LC_Commitement_Debit_Cust_AcCode.Enabled = false;
        txt_GO_LC_Commitement_Debit_Cust_AccNo.Enabled = false;
        txt_GO_LC_Commitement_Debit_Exch_Curr.Enabled = false;
        txt_GO_LC_Commitement_Debit_Exch_Rate.Enabled = false;
        txt_GO_LC_Commitement_Debit_Advice_Print.Enabled = false;
        txt_GO_LC_Commitement_Debit_Details.Enabled = false;
        txt_GO_LC_Commitement_Debit_Entity.Enabled = false;
        txt_GO_LC_Commitement_Credit_Code.Enabled = false;
        txt_GO_LC_Commitement_Credit_Curr.Enabled = false;
        txt_GO_LC_Commitement_Credit_Amt.Enabled = false;
        txt_GO_LC_Commitement_Credit_Cust.Enabled = false;
        txt_GO_LC_Commitement_Credit_Cust_AcCode.Enabled = false;
        txt_GO_LC_Commitement_Credit_Cust_AccNo.Enabled = false;
        txt_GO_LC_Commitement_Credit_Exch_Curr.Enabled = false;
        txt_GO_LC_Commitement_Credit_Exch_Rate.Enabled = false;
        txt_GO_LC_Commitement_Credit_Advice_Print.Enabled = false;
        txt_GO_LC_Commitement_Credit_Details.Enabled = false;
        txt_GO_LC_Commitement_Credit_Entity.Enabled = false;

        txt_GO_LC_Commitement_Scheme_no.Enabled = false;
        txt_GO_LC_Commitement_Debit_FUND.Enabled = false;
        txt_GO_LC_Commitement_Debit_Check_No.Enabled = false;
        txt_GO_LC_Commitement_Debit_Available.Enabled = false;
        txt_GO_LC_Commitement_Debit_Division.Enabled = false;
        txt_GO_LC_Commitement_Debit_Inter_Amount.Enabled = false;
        txt_GO_LC_Commitement_Debit_Inter_Rate.Enabled = false;

        txt_GO_LC_Commitement_Credit_FUND.Enabled = false;
        txt_GO_LC_Commitement_Credit_Check_No.Enabled = false;
        txt_GO_LC_Commitement_Credit_Available.Enabled = false;
        txt_GO_LC_Commitement_Credit_Division.Enabled = false;
        txt_GO_LC_Commitement_Credit_Inter_Amount.Enabled = false;
        txt_GO_LC_Commitement_Credit_Inter_Rate.Enabled = false;

        //// swift Files
        rdb_swift_None.Enabled = false;
        rdb_swift_740.Enabled = false;
        rdb_swift_756.Enabled = false;
        rdb_swift_999.Enabled = false;
        rdb_swift_799.Enabled = false;
        rdb_swift_747.Enabled = false;

        txt_Narrative_1.Enabled = false;
        txt_Narrative_2.Enabled = false;
        txt_Narrative_3.Enabled = false;
        txt_Narrative_4.Enabled = false;
        txt_Narrative_5.Enabled = false;
        txt_Narrative_6.Enabled = false;
        txt_Narrative_7.Enabled = false;
        txt_Narrative_8.Enabled = false;
        txt_Narrative_9.Enabled = false;
        txt_Narrative_10.Enabled = false;
        txt_Narrative_11.Enabled = false;
        txt_Narrative_12.Enabled = false;
        txt_Narrative_13.Enabled = false;
        txt_Narrative_14.Enabled = false;
        txt_Narrative_15.Enabled = false;
        txt_Narrative_16.Enabled = false;
        txt_Narrative_17.Enabled = false;
        txt_Narrative_18.Enabled = false;
        txt_Narrative_19.Enabled = false;
        txt_Narrative_20.Enabled = false;
        txt_Narrative_21.Enabled = false;
        txt_Narrative_22.Enabled = false;
        txt_Narrative_23.Enabled = false;
        txt_Narrative_24.Enabled = false;
        txt_Narrative_25.Enabled = false;
        txt_Narrative_26.Enabled = false;
        txt_Narrative_27.Enabled = false;
        txt_Narrative_28.Enabled = false;
        txt_Narrative_29.Enabled = false;
        txt_Narrative_30.Enabled = false;
        txt_Narrative_31.Enabled = false;
        txt_Narrative_32.Enabled = false;
        txt_Narrative_33.Enabled = false;
        txt_Narrative_34.Enabled = false;
        txt_Narrative_35.Enabled = false;

        // swift 740
        txt_Acceptance_Beneficiary.Enabled = false;
        txt_Acceptance_Beneficiary2.Enabled = false;
        ddlNegotiatingBankSwift.Enabled = false;
        txtNegoAccountNumber.Enabled = false;
        txt_Acceptance_Beneficiary3.Enabled = false;
        txt_Acceptance_Beneficiary4.Enabled = false;
        txtNegoSwiftCode.Enabled = false;
        txtNegoName.Enabled = false;
        txtCreditCurrency.Enabled = false;
        txtCreditAmount.Enabled = false;
        lblNegoAddress1.Enabled = false;
        txtNegoAddress1.Enabled = false;
        txtPercentageCreditAmountTolerance.Enabled = false;
        txtNegoAddress2.Enabled = false;
        txt_Acceptance_Max_Credit_Amt.Enabled = false;
        txtNegoAddress3.Enabled = false;
        txt_Acceptance_Additional_Amt_Covered.Enabled = false;
        txt_Acceptance_Additional_Amt_Covered2.Enabled = false;
        txt_Acceptance_Additional_Amt_Covered3.Enabled = false;
        txt_Acceptance_Additional_Amt_Covered4.Enabled = false;
        txtMixedPaymentDetails.Enabled = false;
        txtMixedPaymentDetails2.Enabled = false;
        txtMixedPaymentDetails3.Enabled = false;
        txtMixedPaymentDetails4.Enabled = false;
        txtDeferredPaymentDetails.Enabled = false;
        txtDeferredPaymentDetails2.Enabled = false;
        txtDeferredPaymentDetails3.Enabled = false;
        txtDeferredPaymentDetails4.Enabled = false;
        txt_Acceptance_Reimbur_Bank_Charges.Enabled = false;
        txt_Acceptance_Other_Charges.Enabled = false;
        txt_Acceptance_Other_Charges2.Enabled = false;
        txt_Acceptance_Other_Charges3.Enabled = false;
        txt_Acceptance_Other_Charges4.Enabled = false;
        txt_Acceptance_Other_Charges5.Enabled = false;
        txt_Acceptance_Other_Charges6.Enabled = false;
        txt_Acceptance_Sender_to_Receiver_Information.Enabled = false;
        txt_Acceptance_Sender_to_Receiver_Information2.Enabled = false;
        txt_Acceptance_Sender_to_Receiver_Information3.Enabled = false;
        txt_Acceptance_Sender_to_Receiver_Information4.Enabled = false;
        txt_Acceptance_Sender_to_Receiver_Information5.Enabled = false;
        txt_Acceptance_Sender_to_Receiver_Information6.Enabled = false;

        //swift 756 MT

        ddlReceiverCorrespondentMT.Enabled = false;
        txtReceiverAccountNumberMT.Enabled = false;
        txtReceiverSwiftCodeMT.Enabled = false;
        txtReceiverNameMT.Enabled = false;
        txtReceiverLocationMT.Enabled = false;
        txtReceiverAddress1MT.Enabled = false;
        txtReceiverAddress2MT.Enabled = false;
        txtReceiverAddress3MT.Enabled = false;
        txt_Narrative_756_1.Enabled = false;
        txt_Narrative_756_2.Enabled = false;
        txt_Narrative_756_3.Enabled = false;
        txt_Narrative_756_4.Enabled = false;
        txt_Narrative_756_5.Enabled = false;
        txt_Narrative_756_6.Enabled = false;
        txt_Narrative_756_7.Enabled = false;
        txt_Narrative_756_8.Enabled = false;
        txt_Narrative_756_9.Enabled = false;
        txt_Narrative_756_10.Enabled = false;
        txt_Narrative_756_11.Enabled = false;
        txt_Narrative_756_12.Enabled = false;
        txt_Narrative_756_13.Enabled = false;
        txt_Narrative_756_14.Enabled = false;
        txt_Narrative_756_15.Enabled = false;
        txt_Narrative_756_16.Enabled = false;
        txt_Narrative_756_17.Enabled = false;
        txt_Narrative_756_18.Enabled = false;
        txt_Narrative_756_19.Enabled = false;
        txt_Narrative_756_20.Enabled = false;
        txt_Narrative_756_21.Enabled = false;
        txt_Narrative_756_22.Enabled = false;
        txt_Narrative_756_23.Enabled = false;
        txt_Narrative_756_24.Enabled = false;
        txt_Narrative_756_25.Enabled = false;
        txt_Narrative_756_26.Enabled = false;
        txt_Narrative_756_27.Enabled = false;
        txt_Narrative_756_28.Enabled = false;
        txt_Narrative_756_29.Enabled = false;
        txt_Narrative_756_30.Enabled = false;
        txt_Narrative_756_31.Enabled = false;
        txt_Narrative_756_32.Enabled = false;
        txt_Narrative_756_33.Enabled = false;
        txt_Narrative_756_34.Enabled = false;
        txt_Narrative_756_35.Enabled = false;

        txt_Discrepancy_Charges_SFMS.Enabled = false;
        txt_Discrepancy_Charges_Swift.Enabled = false;
        // Swift 756 SFMS

        ddlSenderCorrespondentSFMS.Enabled = false;
        txtSenderAccountNumberSFMS.Enabled = false;
        txtSenderSwiftCodeSFMS.Enabled = false;
        txtSenderNameSFMS.Enabled = false;
        txtSenderLocationSFMS.Enabled = false;
        txtSenderAddress1SFMS.Enabled = false;
        txtSenderAddress2SFMS.Enabled = false;
        txtSenderAddress3SFMS.Enabled = false;
        ddlReceiverCorrespondentSFMS.Enabled = false;
        txtReceiverAccountNumberSFMS.Enabled = false;
        txtReceiverSwiftCodeSFMS.Enabled = false;
        txtReceiverNameSFMS.Enabled = false;
        txtReceiverLocationSFMS.Enabled = false;
        txtReceiverAddress1SFMS.Enabled = false;
        txtReceiverAddress2SFMS.Enabled = false;
        txtReceiverAddress3SFMS.Enabled = false;
        //////////ledger file

        chk_Ledger_Modify.Enabled = false;
        txtLedgerRemark.Enabled = false;
        txtLedgerCustName.Enabled = false;
        txtLedgerCURR.Enabled = false;
        txtLedgerAccode.Enabled = false;
        txtLedgerRefNo.Enabled = false;
        txtLedger_Modify_amt.Enabled = false;
        txtLedgerOperationDate.Enabled = false;
        txtLedgerBalanceAmt.Enabled = false;
        txtLedgerAcceptDate.Enabled = false;
        txtLedgerMaturity.Enabled = false;
        txtLedgerSettlememtDate.Enabled = false;
        txtLedgerSettlValue.Enabled = false;
        txtLedgerLastModDate.Enabled = false;
        txtLedgerREM_EUC.Enabled = false;
        txtLedgerLastOPEDate.Enabled = false;
        txtLedgerTransNo.Enabled = false;
        txtLedgerContraCountry.Enabled = false;
        txtLedgerStatusCode.Enabled = false;
        txtLedgerCollectOfComm.Enabled = false;
        txtLedgerCommodity.Enabled = false;
        txtLedgerhandlingCommRate.Enabled = false;
        txtLedgerhandlingCommCurr.Enabled = false;
        txtLedgerhandlingCommAmt.Enabled = false;
        txtLedgerhandlingCommPayer.Enabled = false;
        txtLedgerPostageRate.Enabled = false;
        txtLedgerPostageCurr.Enabled = false;
        txtLedgerPostageAmt.Enabled = false;
        txtLedgerPostagePayer.Enabled = false;
        txtLedgerOthersRate.Enabled = false;
        txtLedgerOthersCurr.Enabled = false;
        txtLedgerOthersAmt.Enabled = false;
        txtLedgerOthersPayer.Enabled = false;
        txtLedgerTheirCommRate.Enabled = false;
        txtLedgerTheirCommCurr.Enabled = false;
        txtLedgerTheirCommAmt.Enabled = false;
        txtLedgerTheirCommPayer.Enabled = false;
        txtLedgerNegoBank.Enabled = false;
        txtLedgerReimbursingBank.Enabled = false;
        txtLedgerDrawer.Enabled = false;
        txtLedgerTenor.Enabled = false;
        txtLedgerAttn.Enabled = false;
        //-------------- bhupen 747----------------
        txt_747_documentaryCredtno.Enabled = false;
        txt_747_reimbursingbankRef.Enabled = false;
        txt_747_DateofogauthriztnRmburse.Enabled = false;
        txt_747_NewDateofExpiry.Enabled = false;
        txt_747_IncreaseofDocumentaryCreditCurr.Enabled = false;
        txt_747_IncreaseofDocumentaryCreditAmt.Enabled = false;
        txt_747_DecreaseofDocumentaryCreditCurr.Enabled = false;
        txt_747_DecreaseofDocumentaryCreditAmt.Enabled = false;
        txt_747_NewDocumentaryCreditAmtAfterAmendmentCurr.Enabled = false;
        txt_747_NewDocumentaryCreditAmtAfterAmendment.Enabled = false;
        txt_747_PercentageCreditAmtTolerance1.Enabled = false;
        txt_747_PercentageCreditAmtTolerance2.Enabled = false;
        txt_747_AdditionalAmtsCovered1.Enabled = false;
        txt_747_AdditionalAmtsCovered2.Enabled = false;
        txt_747_AdditionalAmtsCovered3.Enabled = false;
        txt_747_AdditionalAmtsCovered4.Enabled = false;
        txt_747_SentoReceivInfo1.Enabled = false;
        txt_747_SentoReceivInfo2.Enabled = false;
        txt_747_SentoReceivInfo3.Enabled = false;
        txt_747_SentoReceivInfo4.Enabled = false;
        txt_747_SentoReceivInfo5.Enabled = false;
        txt_747_SentoReceivInfo6.Enabled = false;
        txt_Narrative_747_1.Enabled = false;
        txt_Narrative_747_2.Enabled = false;
        txt_Narrative_747_3.Enabled = false;
        txt_Narrative_747_4.Enabled = false;
        txt_Narrative_747_5.Enabled = false;
        txt_Narrative_747_6.Enabled = false;
        txt_Narrative_747_7.Enabled = false;
        txt_Narrative_747_8.Enabled = false;
        txt_Narrative_747_9.Enabled = false;
        txt_Narrative_747_10.Enabled = false;
        txt_Narrative_747_11.Enabled = false;
        txt_Narrative_747_12.Enabled = false;
        txt_Narrative_747_13.Enabled = false;
        txt_Narrative_747_14.Enabled = false;
        txt_Narrative_747_15.Enabled = false;
        txt_Narrative_747_16.Enabled = false;
        txt_Narrative_747_17.Enabled = false;
        txt_Narrative_747_18.Enabled = false;
        txt_Narrative_747_19.Enabled = false;
        txt_Narrative_747_20.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", hdnDocNo.Value.Trim());
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            CreateSwiftSFMSFile();
            if (txtLedgerStatusCode.Text.ToString().ToUpper() != "S")
            {
                GBaseFileCreation();
            }
            if (chkGO_Swift_SFMS.Checked)
            {
                GBaseFileCreationGOSwiftSFMS();
            }
            if (chkGO_LC_Commitement.Checked)
            {
                GBaseFileCreationGOLCComm();
            }
            if (chk_Ledger_Modify.Checked)
            {
                GBaseFileCreationLedger();
            }
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        Lei_Audit();
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        SqlParameter P_checkby = new SqlParameter("@checkby", hdnUserName.Value.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectAcceptance", P_DocNo, P_Status, P_RejectReason, P_checkby);
        Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
    private void fillCustomerMasterDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtCustomer_ID.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString().Trim();          // Added by Bhupen for Lei changes 10112022
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            lblCustomerDesc.ToolTip = lblCustomerDesc.Text;
            if (lblCustomerDesc.Text.Length > 30)
            {
                lblCustomerDesc.Text = lblCustomerDesc.Text.Substring(0, 30) + "...";
            }
        }
        else
        {
            txtCustomer_ID.Text = "";
            lblCustomerDesc.Text = "";
        }

    }
    protected void fill_PurposeCode()
    {
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_PurposeCode_List");
        ddl_PurposeCode.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddl_PurposeCode.DataSource = dt.DefaultView;
            ddl_PurposeCode.DataTextField = "PurposeID_Descr";
            ddl_PurposeCode.DataValueField = "purposeID";
            ddl_PurposeCode.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddl_PurposeCode.Items.Insert(0, li);


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

            txt_INT_Rate.Focus();
        }
        else
        {
            lbl_PurposeCodeDesc.Text = "";
            ddl_PurposeCode.ClearSelection();
            ddl_PurposeCode.Focus();
        }
    }
    protected void fill_GBaseCommodity_Description()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CommodityID", txtCommodity.Text.ToString());
        DataTable dt = objData.getData("TF_IMP_GBaseCommodityDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCommodityDesc.Text = dt.Rows[0]["GBase_Commodity_Description"].ToString().Trim();
            lblCommodityDesc.ToolTip = lblCommodityDesc.Text;
            if (lblCommodityDesc.Text.Length > 20)
            {
                lblCommodityDesc.Text = lblCommodityDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            lblCommodityDesc.Text = "";
        }
    }

    protected void btnLedgerNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentDetails;
    }
    protected void btnDocPrev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentLedger;
    }
    protected void btnDocNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentInstructions;
    }
    protected void btn_Instr_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentDetails;
    }
    protected void btn_Instr_Next_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentAccounting;
    }
    protected void btnDocAccPrev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentInstructions;
    }
    protected void btnDocAccNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentGO;
    }
    protected void btnGO_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentAccounting;
    }
    protected void btnGO_Next_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentSwiftFile;
    }
    protected void btn_SWIFT_File_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentGO;
    }
    protected void txtCustomer_ID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerMasterDetails();
    }
    protected void txtCommodity_TextChanged(object sender, EventArgs e)
    {
        fill_GBaseCommodity_Description();
    }
    protected void ddl_PurposeCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_PurposeCode_Description();
    }
    protected void chkGO_Swift_SFMS_OnCheckedChanged(object sender, EventArgs e)
    {
        GO_Swift_SFMS_Toggel();
    }
    protected void chkGO_LC_Commitement_OnCheckedChanged(object sender, EventArgs e)
    {
        GO_LC_Commitement_Toggel();
    }
    protected void chk_Ledger_Modify_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_Ledger_Modify.Checked == true)
        {
            PanelLedgerFile.Visible = true;
        }
        if (chk_Ledger_Modify.Checked == false)
        {
            PanelLedgerFile.Visible = false;
        }
    }
    protected void GO_Swift_SFMS_Toggel()
    {
        if (chkGO_Swift_SFMS.Checked == false)
        {
            panalGO_Swift_SFMS.Visible = false;
        }
        else if (chkGO_Swift_SFMS.Checked == true)
        {
            panalGO_Swift_SFMS.Visible = true;

            txt_GO_Swift_SFMS_SectionNo.Text = "05";
            txt_GO_Swift_SFMS_Remarks.Text = "SWIFT/SFMS CHARGES";

            txt_GO_Swift_SFMS_Debit_Code.Text = "D";
            txt_GO_Swift_SFMS_Debit_Curr.Text = "INR";

            txt_GO_Swift_SFMS_Debit_AdPrint.Text = "Y";
            txt_GO_Swift_SFMS_Debit_Details.Text = "SWIFT/SFMS CHARGES";

            txt_GO_Swift_SFMS_Credit_Code.Text = "C";
            txt_GO_Swift_SFMS_Credit_Curr.Text = "INR";

            txt_GO_Swift_SFMS_Credit_Cust_AcCode.Text = "67109";
            txt_GO_Swift_SFMS_Credit_Details.Text = "SWIFT/SFMS CHARGES";

        }
    }
    protected void GO_LC_Commitement_Toggel()
    {
        if (chkGO_LC_Commitement.Checked == false)
        {
            panalGO_LC_Commitement.Visible = false;
        }
        else if (chkGO_LC_Commitement.Checked == true)
        {
            panalGO_LC_Commitement.Visible = true;

            txt_GO_LC_Commitement_SectionNo.Text = "04";
            txt_GO_LC_Commitement_Remarks.Text = "LC COMMITMENT CHGS";

            txt_GO_LC_Commitement_Debit_Code.Text = "D";
            txt_GO_LC_Commitement_Debit_Curr.Text = "INR";

            txt_GO_LC_Commitement_Debit_Details.Text = "LC COMMITMENT CHGS";

            txt_GO_LC_Commitement_Credit_Code.Text = "C";
            txt_GO_LC_Commitement_Credit_Curr.Text = "INR";

            txt_GO_LC_Commitement_Credit_Cust_AcCode.Text = "66802";
            txt_GO_LC_Commitement_Credit_Details.Text = "LC COMMITMENT CHGS";

            fillLCDetails(txtLCNo.Text.Trim(), txtDocNo.Text.ToString().Trim(), txtRiskCust.Text.Trim(), lblForeign_Local.Text.Trim());
        }
    }

    protected void ddlNegotiatingBankSwift_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNegotiatingBankSwift.SelectedValue == "A")
        {
            lblSwiftOrName.Text = "Swift Code :";
            lblNegoAddress1.Text = "";
            lblNegoAddress2.Text = "";
            lblNegoAddress3.Text = "";
            txtNegoAccountNumber.Visible = true;
            txtNegoSwiftCode.Visible = true;
            txtNegoName.Visible = false;
            txtNegoAddress1.Visible = false;
            txtNegoAddress2.Visible = false;
            txtNegoAddress3.Visible = false;
        }
        else
        {
            lblSwiftOrName.Text = "Name :";
            lblNegoAddress1.Text = "Address 1 :";
            lblNegoAddress2.Text = "Address 2 :";
            lblNegoAddress3.Text = "Address 3 :";
            txtNegoAccountNumber.Visible = true;
            txtNegoSwiftCode.Visible = false;
            txtNegoName.Visible = true;
            txtNegoAddress1.Visible = true;
            txtNegoAddress2.Visible = true;
            txtNegoAddress3.Visible = true;
        }
    }
    protected void ddlReceiverCorrespondentMT_TextChanged(object sender, EventArgs e)
    {
        if (ddlReceiverCorrespondentMT.SelectedValue == "A")
        {
            lblReceiverSNLMT.Text = "Swift Code :";
            lblReceiverAddress1MT.Text = "";
            lblReceiverAddress2MT.Text = "";
            lblReceiverAddress3MT.Text = "";
            txtReceiverSwiftCodeMT.Visible = true;
            txtReceiverNameMT.Visible = false;
            txtReceiverLocationMT.Visible = false;
            txtReceiverAddress1MT.Visible = false;
            txtReceiverAddress2MT.Visible = false;
            txtReceiverAddress3MT.Visible = false;
        }
        if (ddlReceiverCorrespondentMT.SelectedValue == "B")
        {
            lblReceiverSNLMT.Text = "Location :";
            lblReceiverAddress1MT.Text = "";
            lblReceiverAddress2MT.Text = "";
            lblReceiverAddress3MT.Text = "";
            txtReceiverSwiftCodeMT.Visible = false;
            txtReceiverNameMT.Visible = false;
            txtReceiverLocationMT.Visible = true;
            txtReceiverAddress1MT.Visible = false;
            txtReceiverAddress2MT.Visible = false;
            txtReceiverAddress3MT.Visible = false;
        }
        if (ddlReceiverCorrespondentMT.SelectedValue == "D")
        {
            lblReceiverSNLMT.Text = "Name :";
            lblReceiverAddress1MT.Text = "Address1 :";
            lblReceiverAddress2MT.Text = "Address2 :";
            lblReceiverAddress3MT.Text = "Address3 :";
            txtReceiverSwiftCodeMT.Visible = false;
            txtReceiverNameMT.Visible = true;
            txtReceiverLocationMT.Visible = false;
            txtReceiverAddress1MT.Visible = true;
            txtReceiverAddress2MT.Visible = true;
            txtReceiverAddress3MT.Visible = true;
        }
    }
    protected void ddlSenderCorrespondentSFMS_TextChanged(object sender, EventArgs e)
    {
        if (ddlSenderCorrespondentSFMS.SelectedValue == "A")
        {
            lblSenderSNLSFMS.Text = "Swift Code :";
            lblSenderAddress1SFMS.Text = "";
            lblSenderAddress2SFMS.Text = "";
            lblSenderAddress3SFMS.Text = "";
            txtSenderSwiftCodeSFMS.Visible = true;
            txtSenderNameSFMS.Visible = false;
            txtSenderLocationSFMS.Visible = false;
            txtSenderAddress1SFMS.Visible = false;
            txtSenderAddress2SFMS.Visible = false;
            txtSenderAddress3SFMS.Visible = false;
        }
        if (ddlSenderCorrespondentSFMS.SelectedValue == "B")
        {
            lblSenderSNLSFMS.Text = "Location :";
            lblSenderAddress1SFMS.Text = "";
            lblSenderAddress2SFMS.Text = "";
            lblSenderAddress3SFMS.Text = "";
            txtSenderSwiftCodeSFMS.Visible = false;
            txtSenderNameSFMS.Visible = false;
            txtSenderLocationSFMS.Visible = true;
            txtSenderAddress1SFMS.Visible = false;
            txtSenderAddress2SFMS.Visible = false;
            txtSenderAddress3SFMS.Visible = false;
        }
        if (ddlSenderCorrespondentSFMS.SelectedValue == "D")
        {
            lblSenderSNLSFMS.Text = "Name :";
            lblSenderAddress1SFMS.Text = "Address1 :";
            lblSenderAddress2SFMS.Text = "Address2 :";
            lblSenderAddress3SFMS.Text = "Address3 :";
            txtSenderSwiftCodeSFMS.Visible = false;
            txtSenderNameSFMS.Visible = true;
            txtSenderLocationSFMS.Visible = false;
            txtSenderAddress1SFMS.Visible = true;
            txtSenderAddress2SFMS.Visible = true;
            txtSenderAddress3SFMS.Visible = true;
        }
    }
    protected void ddlReceiverCorrespondentSFMS_TextChanged(object sender, EventArgs e)
    {
        if (ddlReceiverCorrespondentSFMS.SelectedValue == "A")
        {
            lblReceiverSNLSFMS.Text = "Swift Code :";
            lblReceiverAddress1SFMS.Text = "";
            lblReceiverAddress2SFMS.Text = "";
            lblReceiverAddress3SFMS.Text = "";
            txtReceiverSwiftCodeSFMS.Visible = true;
            txtReceiverNameSFMS.Visible = false;
            txtReceiverLocationSFMS.Visible = false;
            txtReceiverAddress1SFMS.Visible = false;
            txtReceiverAddress2SFMS.Visible = false;
            txtReceiverAddress3SFMS.Visible = false;
        }
        if (ddlReceiverCorrespondentSFMS.SelectedValue == "B")
        {
            lblReceiverSNLSFMS.Text = "Location :";
            lblReceiverAddress1SFMS.Text = "";
            lblReceiverAddress2SFMS.Text = "";
            lblReceiverAddress3SFMS.Text = "";
            txtReceiverSwiftCodeSFMS.Visible = false;
            txtReceiverNameSFMS.Visible = false;
            txtReceiverLocationSFMS.Visible = true;
            txtReceiverAddress1SFMS.Visible = false;
            txtReceiverAddress2SFMS.Visible = false;
            txtReceiverAddress3SFMS.Visible = false;
        }
        if (ddlReceiverCorrespondentSFMS.SelectedValue == "D")
        {
            lblReceiverSNLSFMS.Text = "Name :";
            lblReceiverAddress1SFMS.Text = "Address1 :";
            lblReceiverAddress2SFMS.Text = "Address2 :";
            lblReceiverAddress3SFMS.Text = "Address3 :";
            txtReceiverSwiftCodeSFMS.Visible = false;
            txtReceiverNameSFMS.Visible = true;
            txtReceiverLocationSFMS.Visible = false;
            txtReceiverAddress1SFMS.Visible = true;
            txtReceiverAddress2SFMS.Visible = true;
            txtReceiverAddress3SFMS.Visible = true;
        }
    }
    protected void ddlAvailablewithby_740_TextChanged(object sender, EventArgs e)
    {
        if (ddlAvailablewithby_740.SelectedValue == "A")
        {
            lblAvailablewithbySwiftCode.Text = "Swift Code: ";
            txtAvailablewithbySwiftCode.Visible = true;
            txtAvailablewithbyName.Visible = false;
            lblAvailablewithbyAddress1.Visible = false;
            lblAvailablewithbyAddress2.Visible = false;
            lblAvailablewithbyAddress3.Visible = false;
            txtAvailablewithbyAddress1.Visible = false;
            txtAvailablewithbyAddress2.Visible = false;
            txtAvailablewithbyAddress3.Visible = false;
        }
        else
        {
            lblAvailablewithbySwiftCode.Text = "Name: ";
            txtAvailablewithbySwiftCode.Visible = false;
            txtAvailablewithbyName.Visible = true;
            lblAvailablewithbyAddress1.Visible = true;
            lblAvailablewithbyAddress2.Visible = true;
            lblAvailablewithbyAddress3.Visible = true;
            txtAvailablewithbyAddress1.Visible = true;
            txtAvailablewithbyAddress2.Visible = true;
            txtAvailablewithbyAddress3.Visible = true;
        }
    }
    protected void ddlDrawee_740_TextChanged(object sender, EventArgs e)
    {
        if (ddlDrawee_740.SelectedValue == "A")
        {
            lblDraweeSwiftCode.Text = "Swift Code: ";
            txtDraweeSwiftCode.Visible = true;
            txtDraweeName.Visible = false;
            lblDraweeAddress1.Visible = false;
            lblDraweeAddress2.Visible = false;
            lblDraweeAddress3.Visible = false;
            txtDraweeAddress1.Visible = false;
            txtDraweeAddress2.Visible = false;
            txtDraweeAddress3.Visible = false;
        }
        else
        {
            lblDraweeSwiftCode.Text = "Name: ";
            txtDraweeSwiftCode.Visible = false;
            txtDraweeName.Visible = true;
            lblDraweeAddress1.Visible = true;
            lblDraweeAddress2.Visible = true;
            lblDraweeAddress3.Visible = true;
            txtDraweeAddress1.Visible = true;
            txtDraweeAddress2.Visible = true;
            txtDraweeAddress3.Visible = true;
        }
    }
    public void CreateSwiftSFMSFile()
    {
        DeleteDataForSwiftSTP();
        if (hdnNegoRemiBankType.Value == "FOREIGN")
        {
            if (rdb_swift_740.Checked)
            {
                //CreateSwift740("SWIFT");
                CreateSwift740_XML("SWIFT");
                InsertDataForSwiftSTP("MT740", txtDocNo.Text.Trim());
            }
            if (rdb_swift_756.Checked)
            {
                //CreateSwift756("SWIFT");
                CreateSwift756_XML("SWIFT");
                InsertDataForSwiftSTP("MT756", txtDocNo.Text.Trim());
            }
            if (rdb_swift_747.Checked)
            {
                //CreateSwift747("SWIFT");
                CreateSwift747_XML("SWIFT");
                InsertDataForSwiftSTP("MT747", txtDocNo.Text.Trim());
            }
            if (rdb_swift_799.Checked)
            {
                //CreateSwift799("SWIFT");
                CreateSwift799_XML("SWIFT");
                InsertDataForSwiftSTP("MT799", txtDocNo.Text.Trim());
            }
            if (rdb_swift_999.Checked)
            {
                //CreateSwift999("SWIFT");
                CreateSwift999_XML("SWIFT");
                InsertDataForSwiftSTP("MT999", txtDocNo.Text.Trim());
            }
        }
        else if (hdnNegoRemiBankType.Value == "LOCAL")
        {
            if (rdb_swift_740.Checked)
            {
                CreateSwift740("SFMS");
            }
            if (rdb_swift_756.Checked)
            {
                CreateSFMS756("SFMS");
            }
            if (rdb_swift_747.Checked)
            {
                CreateSwift747("SFMS");
            }
            if (rdb_swift_799.Checked)
            {
                CreateSwift799("SFMS");
            }
            if (rdb_swift_999.Checked)
            {
                CreateSwift999("SFMS");
            }
        }
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Acceptance", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase" + ".xlsx";
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
    public void CreateSwift756(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT756_FileCreation", P_DocNo);
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
            string _directoryPath = "";
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT756/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT756.xlsx";
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    ws.Cells["B1"].Value = "To";
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();

                    ws.Cells["B2"].Value = "Senders Reference";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["C2"].Value = dt.Rows[0]["SendersReference"].ToString();

                    ws.Cells["B3"].Value = "Presenting Bank’s Reference";
                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["C3"].Value = dt.Rows[0]["PresentingBanksReference"].ToString();

                    ws.Cells["B4"].Value = "Total Amount Claimed";
                    ws.Cells["A4"].Value = "[32B]";
                    ws.Cells["C4"].Value = dt.Rows[0]["TotalAmountClaimedCurr"].ToString();
                    ws.Cells["D4"].Value = dt.Rows[0]["TotalAmountClaimedAmount"].ToString();

                    ws.Cells["B5"].Value = "Amount Reimbursed or Paid";
                    ws.Cells["A5"].Value = "[33A]";
                    ws.Cells["C5"].Value = dt.Rows[0]["AmountReimbursedorPaidDate"].ToString();
                    ws.Cells["D5"].Value = dt.Rows[0]["AmountReimbursedorPaidCurr"].ToString();
                    ws.Cells["E5"].Value = dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString();

                    ws.Cells["B6"].Value = "Sender’s Correspondent";
                    ws.Cells["A6"].Value = "[53A]";
                    ws.Cells["C6"].Value = dt.Rows[0]["SendersCorrespondent1"].ToString();
                    ws.Cells["C7"].Value = dt.Rows[0]["SendersCorrespondent2"].ToString();
                    int _Ecol = 8;
                    if (dt.Rows[0]["SendersCorrespondent3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendersCorrespondent3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendersCorrespondent4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendersCorrespondent4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendersCorrespondent5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendersCorrespondent5"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["B" + _Ecol].Value = "Receiver’s Correspondent";
                    ws.Cells["A" + _Ecol].Value = "[54" + dt.Rows[0]["ReceiverCorrespondentMT"].ToString() + "]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverPartyIdentifier"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["ReceiverAccountNumberMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAccountNumberMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverSwiftCodeMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverSwiftCodeMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverLocationMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverLocationMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverNameMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverNameMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress1MT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress1MT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress2MT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress2MT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress3MT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress3MT"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["B" + _Ecol].Value = "Sender to Receiver Information";
                    ws.Cells["A" + _Ecol].Value = "[72Z]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation1"].ToString();
                    _Ecol++;
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation2"].ToString();
                    _Ecol++;
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation3"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["SendertoReceiverInformation4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation6"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative7561"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Narrative";
                    ws.Cells["A" + _Ecol].Value = "[79Z]";
                    _Ecol++;

                    int _Tcol = 2;
                    for (int i = 0; i < 35; i++)
                    {
                        if (dt.Rows[0]["Narrative756" + _Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative756" + _Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
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
    public void CreateSFMS756(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_FIN756_FileCreation", P_DocNo);
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
            string _directoryPath = "";
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS756/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN756.xlsx";
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");

                    ws.Cells["B1"].Value = "Receiver Address";
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();

                    ws.Cells["B2"].Value = "Senders Reference";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["C2"].Value = dt.Rows[0]["SendersReference"].ToString();

                    ws.Cells["B3"].Value = "Presenting Bank’s Reference";
                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["C3"].Value = dt.Rows[0]["PresentingBanksReference"].ToString();

                    ws.Cells["B4"].Value = "Total Amount Claimed";
                    ws.Cells["A4"].Value = "[32B]";
                    ws.Cells["C4"].Value = dt.Rows[0]["TotalAmountClaimedCurr"].ToString();
                    ws.Cells["D4"].Value = dt.Rows[0]["TotalAmountClaimedAmount"].ToString();

                    ws.Cells["B5"].Value = "Amount Reimbursed or Paid";
                    ws.Cells["A5"].Value = "[33A]";
                    ws.Cells["C5"].Value = dt.Rows[0]["AmountReimbursedorPaidDate"].ToString();
                    ws.Cells["D5"].Value = dt.Rows[0]["AmountReimbursedorPaidCurr"].ToString();
                    ws.Cells["E5"].Value = dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString();

                    ws.Cells["B6"].Value = "Sender’s Correspondent";
                    ws.Cells["A6"].Value = "[53" + dt.Rows[0]["SenderCorrespondentSFMS"].ToString() + "]";
                    ws.Cells["C6"].Value = dt.Rows[0]["SenderPartyIdentifierSFMS"].ToString();
                    int _Ecol = 7;
                    if (dt.Rows[0]["SenderAccountNumberSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderAccountNumberSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderSwiftCodeSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderSwiftCodeSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderLocationSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderLocationSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderNameSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderNameSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderAddress1SFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderAddress1SFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderAddress2SFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderAddress2SFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderAddress3SFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderAddress3SFMS"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Receiver’s Correspondent";
                    ws.Cells["A" + _Ecol].Value = "[54" + dt.Rows[0]["ReceiverCorrespondentSFMS"].ToString() + "]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverPartyIdentifierSFMS"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["ReceiverAccountNumberSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAccountNumberSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverSwiftCodeSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverSwiftCodeSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverLocationSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverLocationSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverNameSFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverNameSFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress1SFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress1SFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress2SFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress2SFMS"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress3SFMS"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress3SFMS"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Sender to Receiver Information";
                    ws.Cells["A" + _Ecol].Value = "[72]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["SendertoReceiverInformation2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation6"].ToString();
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
    public void CreateSwift740(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT740_FileCreation", P_DocNo);
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
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT740/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS740/" + MTodayDate + "/");
            }
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT740.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN740.xlsx";
            }
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    ws.Cells["B1"].Value = "To";
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();

                    ws.Cells["B2"].Value = "Document Credit Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["C2"].Value = dt.Rows[0]["DocumentCreditNumber"].ToString();

                    ws.Cells["B3"].Value = "Account Identification";
                    ws.Cells["A3"].Value = "[25]";
                    ws.Cells["C3"].Value = dt.Rows[0]["AccountIdentification"].ToString();

                    ws.Cells["B4"].Value = "Applicable Rules";
                    ws.Cells["A4"].Value = "[40F]";
                    ws.Cells["C4"].Value = dt.Rows[0]["ApplicableRules"].ToString();

                    ws.Cells["B5"].Value = "Date and Place of Expiry";
                    ws.Cells["A5"].Value = "[31D]";
                    ws.Cells["C5"].Value = dt.Rows[0]["DateandPlaceofExpiry"].ToString();
                    ws.Cells["D5"].Value = dt.Rows[0]["DateandPlaceofExpiryPlace"].ToString();

                    ws.Cells["B6"].Value = "Negotiating Bank";
                    ws.Cells["A6"].Value = "[58" + dt.Rows[0]["NegoRemiBank"].ToString() + "]";
                    ws.Cells["C6"].Value = dt.Rows[0]["NegoPartyIdentifier"].ToString();
                    int _Ecol = 7;
                    if (dt.Rows[0]["NegoAccountNumber"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["NegoAccountNumber"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["NegoSwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["NegoSwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["NegoName"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["NegoName"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["NegoAddress1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["NegoAddress1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["NegoAddress2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["NegoAddress2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["NegoAddress3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["NegoAddress3"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["B" + _Ecol].Value = "Beneficiary [/34x]";
                    ws.Cells["A" + _Ecol].Value = "[59]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Beneficiary341"].ToString();
                    _Ecol = _Ecol + 1;
                    if (dt.Rows[0]["Beneficiary342"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Beneficiary342"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Beneficiary343"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Beneficiary343"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Beneficiary344"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Beneficiary344"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Credit Amount";
                    ws.Cells["A" + _Ecol].Value = "[32B]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["CreditCurrency"].ToString();
                    ws.Cells["D" + _Ecol].Value = dt.Rows[0]["CreditAmount"].ToString();
                    _Ecol = _Ecol + 1;
                    if (dt.Rows[0]["PercentageCreditAmountTolerance"].ToString() != "")
                    {
                        ws.Cells["B" + _Ecol].Value = "Percentage Credit Amount Tolerance";
                        ws.Cells["A" + _Ecol].Value = "[39A]";
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["PercentageCreditAmountTolerance"].ToString();
                        ws.Cells["D" + _Ecol].Value = dt.Rows[0]["PercentageCreditAmountTolerance1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MaximumCreditAmount"].ToString() != "")
                    {
                        ws.Cells["B" + _Ecol].Value = "Maximum Credit Amount";
                        ws.Cells["A" + _Ecol].Value = "[39B]";
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MaximumCreditAmount"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Additional_Amt_Covered_ACC"].ToString() != "")
                    {
                        ws.Cells["B" + _Ecol].Value = "Additional Amounts Covered";
                        ws.Cells["A" + _Ecol].Value = "[39C]";
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Additional_Amt_Covered_ACC"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Additional_Amt_Covered_ACC2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Additional_Amt_Covered_ACC2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Additional_Amt_Covered_ACC3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Additional_Amt_Covered_ACC3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Additional_Amt_Covered_ACC4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Additional_Amt_Covered_ACC4"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["B" + _Ecol].Value = "Available With .. By..";
                    ws.Cells["A" + _Ecol].Value = "[41" + dt.Rows[0]["AvailableWithByHeader"].ToString() + "]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AvailableWithBy"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["AvailableWithBy1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AvailableWithBy1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AvailableWithBy2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AvailableWithBy2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AvailableWithBy3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AvailableWithBy3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AvailableWithBy4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AvailableWithBy4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AvailableWithBy5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AvailableWithBy5"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Drafts at..";
                    ws.Cells["A" + _Ecol].Value = "[42C]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Draftsat"].ToString();
                    _Ecol++;
                    ws.Cells["B" + _Ecol].Value = "Drawee";
                    ws.Cells["A" + _Ecol].Value = "[42" + dt.Rows[0]["DraweeHeader"].ToString() + "]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Drawee"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["Drawee1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Drawee1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Drawee2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Drawee2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Drawee3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Drawee3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Drawee4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Drawee4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Drawee5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Drawee5"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Mixed Payment Details";
                    ws.Cells["A" + _Ecol].Value = "[42M]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MixedPaymentDetails"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["MixedPaymentDetails2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MixedPaymentDetails2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MixedPaymentDetails3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MixedPaymentDetails3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["MixedPaymentDetails4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["MixedPaymentDetails4"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["B" + _Ecol].Value = "Deferred Payment Details";
                    ws.Cells["A" + _Ecol].Value = "[42P]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DeferredPaymentDetails"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["DeferredPaymentDetails2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DeferredPaymentDetails2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["DeferredPaymentDetails3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DeferredPaymentDetails3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["DeferredPaymentDetails4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DeferredPaymentDetails4"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Reimbursing Bank Charges";
                    ws.Cells["A" + _Ecol].Value = "[71A]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReimbursingBankCharges"].ToString();
                    _Ecol++;

                    ws.Cells["B" + _Ecol].Value = "Other Charges";
                    ws.Cells["A" + _Ecol].Value = "[71D]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Other_Charges_ACC"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["Other_Charges_ACC2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Other_Charges_ACC2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Other_Charges_ACC3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Other_Charges_ACC3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Other_Charges_ACC4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Other_Charges_ACC4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Other_Charges_ACC5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Other_Charges_ACC5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Other_Charges_ACC6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Other_Charges_ACC6"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Sender to Receiver Information";
                    ws.Cells["A" + _Ecol].Value = "[72Z]";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Sender_to_Receiver_Information"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["Sender_to_Receiver_Information2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Sender_to_Receiver_Information2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Sender_to_Receiver_Information3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Sender_to_Receiver_Information3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Sender_to_Receiver_Information4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Sender_to_Receiver_Information4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Sender_to_Receiver_Information5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Sender_to_Receiver_Information5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Sender_to_Receiver_Information6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Sender_to_Receiver_Information6"].ToString();
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
    public void CreateSwift999(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT756_FileCreation", P_DocNo);
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
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS999/" + MTodayDate + "/");
            }
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT999.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN999.xlsx";
            }
            if (dt.Rows.Count > 0)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");

                    ws.Cells["B1"].Value = "To";
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();

                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B2"].Value = "Transaction Reference Number";
                    ws.Cells["C2"].Value = dt.Rows[0]["SendersReference"].ToString();

                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["C3"].Value = dt.Rows[0]["PresentingBanksReference"].ToString();

                    ws.Cells["A4"].Value = "[79]";
                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["C4"].Value = "KINDLY TREAT THIS MESSAGE AS MT-756 AS WE DO NOT";
                    ws.Cells["C5"].Value = "HAVE RMA WITH YOUR GOOD BANK.";
                    ws.Cells["C6"].Value = ".";
                    ws.Cells["C7"].Value = "20: " + dt.Rows[0]["SendersReference"].ToString();

                    int _Ecol = 8;
                    if (dt.Rows[0]["PresentingBanksReference"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "21: " + dt.Rows[0]["PresentingBanksReference"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["TotalAmountClaimedCurr"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "32B: " + dt.Rows[0]["TotalAmountClaimedCurr"].ToString() + " " + dt.Rows[0]["TotalAmountClaimedAmount"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AmountReimbursedorPaidCurr"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "33A: " + dt.Rows[0]["AmountReimbursedorPaidDate"].ToString() + " " + dt.Rows[0]["AmountReimbursedorPaidCurr"].ToString() + " " + dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendersCorrespondent1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "53A: " + dt.Rows[0]["SendersCorrespondent1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendersCorrespondent2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "     " + dt.Rows[0]["SendersCorrespondent2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendersCorrespondent3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendersCorrespondent3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendersCorrespondent4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendersCorrespondent4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendersCorrespondent5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendersCorrespondent5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverPartyIdentifier"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "54A: " + dt.Rows[0]["ReceiverPartyIdentifier"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAccountNumberMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAccountNumberMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverSwiftCodeMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverSwiftCodeMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverLocationMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverLocationMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverNameMT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverNameMT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress1MT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress1MT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress2MT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress2MT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverAddress3MT"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverAddress3MT"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "72Z: " + dt.Rows[0]["SendertoReceiverInformation1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "     " + dt.Rows[0]["SendertoReceiverInformation2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "     " + dt.Rows[0]["SendertoReceiverInformation3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "     " + dt.Rows[0]["SendertoReceiverInformation4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "     " + dt.Rows[0]["SendertoReceiverInformation5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "     " + dt.Rows[0]["SendertoReceiverInformation6"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative7561"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = "79Z: " + dt.Rows[0]["Narrative7561"].ToString();
                        _Ecol++;
                        int _Tcol = 2;
                        for (int i = 0; i < 35; i++)
                        {
                            if (dt.Rows[0]["Narrative756" + _Tcol].ToString() != "")
                            {
                                ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative756" + _Tcol].ToString();
                                _Ecol++;
                            }
                            _Tcol++;
                        }
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
    public void CreateSwift799(string FileType)
    {
        string _directoryPath = ""; string Query = "";
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
        if (FileType == "SWIFT")
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT799/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT799ACC_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS799/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_FIN799ACC_FileCreation";
        }
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData(Query, P_DocNo);

        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT799.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN799.xlsx";
            }
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    if (FileType == "SWIFT")
                    {
                        ws.Cells["B1"].Value = "TO:";
                    }
                    else
                    {
                        ws.Cells["B1"].Value = "Reciver Address";
                    }
                    ws.Cells["B2"].Value = "Transation Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";

                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0]["Transaction Reference Number"].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0]["Related Reference"].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";
                    int _Ecol = 4;

                    int _Tcol = 1;
                    for (int i = 0; i < 36; i++)
                    {
                        if (dt.Rows[0]["Narrative" + _Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative" + _Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
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
    public void CreateSwift747(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT747_FileCreation", P_DocNo);
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
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT747/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS747/" + MTodayDate + "/");
            }
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT747.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN747.xlsx";
            }
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    ws.Cells["B1"].Value = "To";
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();

                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B2"].Value = "Documentary Credit Number";
                    ws.Cells["C2"].Value = dt.Rows[0]["Documentary Credit Number"].ToString();

                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["B3"].Value = "Reimbursing Bank's Reference";
                    ws.Cells["C3"].Value = dt.Rows[0]["ReimbursingBankRef_747"].ToString();

                    ws.Cells["A4"].Value = "[30]";
                    ws.Cells["B4"].Value = "Date of the Original Authorisation to Reimburse";
                    ws.Cells["C4"].Value = dt.Rows[0]["DateoforiginalAuthoOfReimburse_747"].ToString();

                    ws.Cells["A5"].Value = "[31E]";
                    ws.Cells["B5"].Value = "New Date of Expiry";
                    ws.Cells["C5"].Value = dt.Rows[0]["NewDateOfExpiry_747"].ToString();

                    ws.Cells["A6"].Value = "[32B]";
                    ws.Cells["B6"].Value = "Increase of Documentary Credit Amount";
                    ws.Cells["C6"].Value = dt.Rows[0]["IncreaseofDocumentryCreditAmt_Curr_747"].ToString();
                    ws.Cells["D6"].Value = dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString();

                    ws.Cells["A7"].Value = "[33B]";
                    ws.Cells["B7"].Value = "Decrease of Documentary Credit Amount";
                    ws.Cells["C7"].Value = dt.Rows[0]["decreaseofDocumentryCreditAmt_Curr_747"].ToString();
                    ws.Cells["D7"].Value = dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString();

                    ws.Cells["A8"].Value = "[34B]";
                    ws.Cells["B8"].Value = "New Documentary Credit Amount After Amendment";
                    ws.Cells["C8"].Value = dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_Curr_747"].ToString();
                    ws.Cells["D8"].Value = dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString();

                    ws.Cells["A9"].Value = "[39A]";
                    ws.Cells["B9"].Value = "Percentage Credit Amount Tolerance";
                    ws.Cells["C9"].Value = dt.Rows[0]["PercentageCreditAmtTolerance1_747"].ToString();
                    ws.Cells["D9"].Value = dt.Rows[0]["PercentageCreditAmtTolerance2_747"].ToString();

                    ws.Cells["A10"].Value = "[39C]";
                    ws.Cells["B10"].Value = "Additional Amounts Covered";
                    int _Ecol = 10;
                    if (dt.Rows[0]["AddAmtCovered1_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AddAmtCovered1_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AddAmtCovered2_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AddAmtCovered2_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AddAmtCovered3_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AddAmtCovered3_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenToRecInfo4_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenToRecInfo4_747"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[72Z]";
                    ws.Cells["B" + _Ecol].Value = "Sender to Receiver Information";

                    if (dt.Rows[0]["SenToRecInfo1_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenToRecInfo1_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenToRecInfo2_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenToRecInfo2_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenToRecInfo3_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenToRecInfo3_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenToRecInfo4_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenToRecInfo4_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenToRecInfo5_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenToRecInfo5_747"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenToRecInfo6_747"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenToRecInfo6_747"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[77]";
                    ws.Cells["B" + _Ecol].Value = "Narrative";

                    int _Tcol = 1;
                    for (int i = 0; i < 20; i++)
                    {
                        if (dt.Rows[0]["Narrative" + _Tcol + "_747"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative" + _Tcol + "_747"].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
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
    }

    public void CreateSwift756_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT756_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
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
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT756/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS756/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT756_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN756_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = ""; string comma = "";
                #region Narrative
                if (dt.Rows[0]["Narrative7561"].ToString() != "")
                {
                    Narrative = dt.Rows[0]["Narrative7561"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7562"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7562"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7563"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7563"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7564"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7564"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7565"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7565"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7566"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7566"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7567"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7567"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7568"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7568"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7569"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7569"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75610"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75610"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75611"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75611"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75612"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75612"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75613"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75613"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75614"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75614"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75615"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75615"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75616"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75616"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75617"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75617"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75618"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75618"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75619"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75619"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75620"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75620"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75621"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75621"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75622"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75622"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75623"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75623"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75624"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75624"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75625"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75625"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75626"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75626"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75627"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75627"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75628"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75628"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75629"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75629"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75630"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75630"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75631"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75631"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75632"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75632"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75633"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75633"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75634"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75634"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative75635"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75635"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region SendertoReceiverInformation
                string SendertoReceiverInformation = "";
                if (dt.Rows[0]["SendertoReceiverInformation1"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SendertoReceiverInformation1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation2"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation2"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation3"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation3"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation4"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation4"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation5"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation5"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation6"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation6"].ToString().ToString().Replace("/", "");
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == ",") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Total Amount Claimed
                string TotalAmountClam = "";
                TotalAmountClam = dt.Rows[0]["TotalAmountClaimedCurr"].ToString();
                if (dt.Rows[0]["TotalAmountClaimedAmount"].ToString() != "")
                {
                    if (dt.Rows[0]["TotalAmountClaimedAmount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["TotalAmountClaimedAmount"].ToString().Contains(".00"))
                        {
                            TotalAmountClam = TotalAmountClam + dt.Rows[0]["TotalAmountClaimedAmount"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            TotalAmountClam = TotalAmountClam + dt.Rows[0]["TotalAmountClaimedAmount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        TotalAmountClam = TotalAmountClam + dt.Rows[0]["TotalAmountClaimedAmount"].ToString() + ",";
                    }
                }
                #endregion
                #region AmountReimbursedorPaid
                string AmountReimbursedorPaid = dt.Rows[0]["AmountReimbursedorPaidDate"].ToString().Replace("-", "") + dt.Rows[0]["AmountReimbursedorPaidCurr"].ToString().Replace("-", "");
                if (dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString() != "")
                {
                    if (dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString().Contains(".00"))
                        {
                            AmountReimbursedorPaid = AmountReimbursedorPaid + dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            AmountReimbursedorPaid = AmountReimbursedorPaid + dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        AmountReimbursedorPaid = AmountReimbursedorPaid + dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString() + ",";
                    }
                }
                #endregion
                #region Sender's Correspondent
                string SenderCorrespondent = "";
                if (dt.Rows[0]["SendersCorrespondent1"].ToString() != "")
                {
                    SenderCorrespondent = dt.Rows[0]["SendersCorrespondent1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendersCorrespondent2"].ToString() != "")
                {
                    SenderCorrespondent = SenderCorrespondent + "#" + dt.Rows[0]["SendersCorrespondent2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendersCorrespondent3"].ToString() != "")
                {
                    SenderCorrespondent = SenderCorrespondent + "," + dt.Rows[0]["SendersCorrespondent3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendersCorrespondent4"].ToString() != "")
                {
                    SenderCorrespondent = SenderCorrespondent + "," + dt.Rows[0]["SendersCorrespondent4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendersCorrespondent5"].ToString() != "")
                {
                    SenderCorrespondent = SenderCorrespondent + "," + dt.Rows[0]["SendersCorrespondent5"].ToString().Replace("/", "");
                }
                if (SenderCorrespondent.Length != 0)
                {
                    comma = SenderCorrespondent.Trim().Substring(0, 1);
                    if (comma == ",") { SenderCorrespondent = SenderCorrespondent.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region ReciverCorrespondance
                string ReciverCorrespondance = "";
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "A")
                {
                    if (dt.Rows[0]["ReceiverPartyIdentifier"].ToString() != "" || dt.Rows[0]["ReceiverAccountNumberMT"].ToString() != "")
                    {
                        ReciverCorrespondance = dt.Rows[0]["ReceiverPartyIdentifier"].ToString() + dt.Rows[0]["ReceiverAccountNumberMT"].ToString();
                    }
                    if (ReciverCorrespondance != "" && dt.Rows[0]["ReceiverSwiftCodeMT"].ToString() != "")
                    {
                        ReciverCorrespondance = ReciverCorrespondance + "#" + dt.Rows[0]["ReceiverSwiftCodeMT"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReceiverSwiftCodeMT"].ToString() != "")
                        {
                            ReciverCorrespondance = dt.Rows[0]["ReceiverSwiftCodeMT"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "B")
                {
                    if (dt.Rows[0]["ReceiverPartyIdentifier"].ToString() != "" || dt.Rows[0]["ReceiverAccountNumberMT"].ToString() != "")
                    {
                        ReciverCorrespondance = dt.Rows[0]["ReceiverPartyIdentifier"].ToString() + dt.Rows[0]["ReceiverAccountNumberMT"].ToString();
                    }
                    if (ReciverCorrespondance != "" && dt.Rows[0]["ReceiverLocationMT"].ToString() != "")
                    {
                        ReciverCorrespondance = ReciverCorrespondance + "#" + dt.Rows[0]["ReceiverLocationMT"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReceiverLocationMT"].ToString() != "")
                        {
                            ReciverCorrespondance = dt.Rows[0]["ReceiverLocationMT"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "D")
                {
                    ReciverCorrespondance = dt.Rows[0]["ReceiverPartyIdentifier"].ToString() + dt.Rows[0]["ReceiverAccountNumberMT"].ToString();
                    if (dt.Rows[0]["ReceiverNameMT"].ToString() != "")
                    {
                        ReciverCorrespondance = ReciverCorrespondance + '#' + dt.Rows[0]["ReceiverNameMT"].ToString();
                    }
                    if (dt.Rows[0]["ReceiverAddress1MT"].ToString() != "")
                    {
                        ReciverCorrespondance = ReciverCorrespondance + ',' + dt.Rows[0]["ReceiverAddress1MT"].ToString();
                    }
                    if (dt.Rows[0]["ReceiverAddress2MT"].ToString() != "")
                    {
                        ReciverCorrespondance = ReciverCorrespondance + ',' + dt.Rows[0]["ReceiverAddress2MT"].ToString();
                    }
                    if (dt.Rows[0]["ReceiverAddress3MT"].ToString() != "")
                    {
                        ReciverCorrespondance = ReciverCorrespondance + ',' + dt.Rows[0]["ReceiverAddress3MT"].ToString();
                    }
                }
                if (ReciverCorrespondance.Length != 0)
                {
                    comma = ReciverCorrespondance.Trim().Substring(0, 1);
                    if (comma == ",") { ReciverCorrespondance = ReciverCorrespondance.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<SendersReference>" + dt.Rows[0]["SendersReference"].ToString() + "</SendersReference>");
                sw.WriteLine("<PresentingBanksReference>" + dt.Rows[0]["PresentingBanksReference"].ToString() + "</PresentingBanksReference>");
                sw.WriteLine("<TotalAmountClaimed>" + TotalAmountClam + "</TotalAmountClaimed>");
                sw.WriteLine("<AmountReimbursedorPaid>" + AmountReimbursedorPaid + "</AmountReimbursedorPaid>");
                if (dt.Rows[0]["SendersCorrespondent"].ToString() == "A")
                {
                    sw.WriteLine("<SendersCorrespondentA>" + SenderCorrespondent + "</SendersCorrespondentA>");
                }
                if (dt.Rows[0]["SendersCorrespondent"].ToString() == "B")
                {
                    sw.WriteLine("<SendersCorrespondentB>" + SenderCorrespondent + "</SendersCorrespondentB>");
                }
                if (dt.Rows[0]["SendersCorrespondent"].ToString() == "D")
                {
                    sw.WriteLine("<SendersCorrespondentD>" + SenderCorrespondent + "</SendersCorrespondentD>");
                }
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "A")
                {
                    sw.WriteLine("<ReceiversCorrespondentA>" + ReciverCorrespondance + "</ReceiversCorrespondentA>");
                }
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "B")
                {
                    sw.WriteLine("<ReceiversCorrespondentB>" + ReciverCorrespondance + "</ReceiversCorrespondentB>");
                }
                if (dt.Rows[0]["ReceiverCorrespondentMT"].ToString() == "D")
                {
                    sw.WriteLine("<ReceiversCorrespondentD>" + ReciverCorrespondance + "</ReceiversCorrespondentD>");
                }
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }// Swift Changes
    public void CreateSwift740_XML(string FileType)
    {
        string _directoryPath = ""; string Query = "";
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string MTodayDate = "";
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (FileType == "SWIFT")
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT740/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT740_FileCreation_XML";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS740/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT740_FileCreation_XML";
        }
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData(Query, P_DocNo);

        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT740_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN740_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                // string DateAmtofUtilisation = dt.Rows[0]["Date"].ToString().Replace("-", "") + dt.Rows[0]["Currency"].ToString() + dt.Rows[0]["Amount"].ToString();
                string XMLStartTag = dt.Rows[0]["XMLStartTag"].ToString();
                string Sender = dt.Rows[0]["Sender"].ToString().Replace("-", "");
                string MessageType = dt.Rows[0]["MessageType"].ToString();
                string DocumentCreditNumber = dt.Rows[0]["DocumentCreditNumber"].ToString();
                string AccountIdentification = dt.Rows[0]["AccountIdentification"].ToString();
                string ApplicableRules = dt.Rows[0]["ApplicableRules"].ToString().Replace("-", "");
                string DateandPlaceofExpiry = "";
                if (dt.Rows[0]["DateandPlaceofExpiry"].ToString() != "")
                {
                    DateandPlaceofExpiry = dt.Rows[0]["DateandPlaceofExpiry"].ToString().Replace("-", "").Trim();
                }
                if (dt.Rows[0]["DateandPlaceofExpiryPlace"].ToString() != "")
                {
                    DateandPlaceofExpiry = DateandPlaceofExpiry + "#" + dt.Rows[0]["DateandPlaceofExpiryPlace"].ToString();
                }
                string comma = "";
                string PercentageCreditAmountTolerance = "";
                string MaximumCreditAmount = "";
                string Draftsat = "";
                //string Draftsat = dt.Rows[0]["Draftsat"].ToString().Replace("-", "");
                string ReimbursingBankCharges = dt.Rows[0]["ReimbursingBankCharges"].ToString().Replace("-", "");
                string Negotiating_Bank = "";
                string Beneficiary = "";
                string CreditAmount = "";
                string Additional_Amt_Covered_ACC = "";
                string AvailableWithBy = "";
                string Drawee = "";
                string MixedPaymentDetails = "";
                string DeferredPaymentDetails = "";
                string Other_Charges_ACC = "";
                string SendertoReceiverInformation = "";
                #region Draftsat
                if (dt.Rows[0]["Draftsat"].ToString() != "")
                {
                    Draftsat = dt.Rows[0]["Draftsat"].ToString();
                }
                if (dt.Rows[0]["Draftsat2"].ToString() != "")
                {
                    Draftsat = Draftsat + "#" + dt.Rows[0]["Draftsat2"].ToString();
                }
                if (dt.Rows[0]["Draftsat3"].ToString() != "")
                {
                    Draftsat = Draftsat + "#" + dt.Rows[0]["Draftsat3"].ToString();
                }
                if (Draftsat.Length != 0)
                {
                    comma = Draftsat.Trim().Substring(0, 1);
                    if (comma == "#") { Draftsat = Draftsat.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Negotiting bank
                if (dt.Rows[0]["NegoRemiBank"].ToString() == "A")
                {
                    if (dt.Rows[0]["NegoAccountNumber"].ToString() != "")
                    {
                        Negotiating_Bank =  dt.Rows[0]["NegoAccountNumber"].ToString();
                    }
                    if (Negotiating_Bank != "" && dt.Rows[0]["NegoSwiftCode"].ToString() != "")
                    {
                        Negotiating_Bank = Negotiating_Bank + "#" + dt.Rows[0]["NegoSwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["NegoSwiftCode"].ToString() != "")
                        {
                            Negotiating_Bank = dt.Rows[0]["NegoSwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["NegoRemiBank"].ToString() == "D")
                {
                    Negotiating_Bank = dt.Rows[0]["NegoAccountNumber"].ToString();
                    if (dt.Rows[0]["NegoName"].ToString() != "")
                    {
                        Negotiating_Bank = Negotiating_Bank + "#" + dt.Rows[0]["NegoName"].ToString();
                    }
                    if (dt.Rows[0]["NegoAddress1"].ToString() != "")
                    {
                        Negotiating_Bank = Negotiating_Bank + "#" + dt.Rows[0]["NegoAddress1"].ToString();
                    }
                    if (dt.Rows[0]["NegoAddress2"].ToString() != "")
                    {
                        Negotiating_Bank = Negotiating_Bank + "#" + dt.Rows[0]["NegoAddress2"].ToString();
                    }
                    if (dt.Rows[0]["NegoAddress3"].ToString() != "")
                    {
                        Negotiating_Bank = Negotiating_Bank + "#" + dt.Rows[0]["NegoAddress3"].ToString();
                    }
                }
                if (Negotiating_Bank.Length != 0)
                {
                    comma = Negotiating_Bank.Trim().Substring(0, 1);
                    if (comma == "#") { Negotiating_Bank = Negotiating_Bank.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region PercentageCreditAmountTolerance
                if (dt.Rows[0]["PercentageCreditAmountTolerance"].ToString() != "")
                {
                    PercentageCreditAmountTolerance = dt.Rows[0]["PercentageCreditAmountTolerance"].ToString() + "/" + dt.Rows[0]["PercentageCreditAmountTolerance1"].ToString();
                }
                #endregion
                #region Beneficiary
                if (dt.Rows[0]["Beneficiary341"].ToString() != "")
                {
                    Beneficiary = dt.Rows[0]["Beneficiary341"].ToString();
                }
                if (dt.Rows[0]["Beneficiary342"].ToString() != "")
                {
                    Beneficiary = Beneficiary + "#" + dt.Rows[0]["Beneficiary342"].ToString();
                }
                if (dt.Rows[0]["Beneficiary343"].ToString() != "")
                {
                    Beneficiary = Beneficiary + "#" + dt.Rows[0]["Beneficiary343"].ToString();
                }
                if (dt.Rows[0]["Beneficiary344"].ToString() != "")
                {
                    Beneficiary = Beneficiary + "#" + dt.Rows[0]["Beneficiary344"].ToString();
                }
                //Added by bhupen on 17012023(For LC)
                if (dt.Rows[0]["Beneficiary345"].ToString() != "")
                {
                    Beneficiary = Beneficiary + "#" + dt.Rows[0]["Beneficiary345"].ToString();
                }
                if (Beneficiary.Length != 0)
                {
                    comma = Beneficiary.Trim().Substring(0, 1);
                    if (comma == "#") { Beneficiary = Beneficiary.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region CreditAmount
                CreditAmount = dt.Rows[0]["CreditCurrency"].ToString();
                if (dt.Rows[0]["CreditAmount"].ToString() != "")
                {
                    if (dt.Rows[0]["CreditAmount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["CreditAmount"].ToString().Contains(".00"))
                        {
                            CreditAmount = CreditAmount + dt.Rows[0]["CreditAmount"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            CreditAmount = CreditAmount + dt.Rows[0]["CreditAmount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        CreditAmount = CreditAmount + dt.Rows[0]["CreditAmount"].ToString() + ",";
                    }
                }
                #endregion
                #region Additional Amounts Covered
                if (dt.Rows[0]["Additional_Amt_Covered_ACC"].ToString() != "")
                {
                    Additional_Amt_Covered_ACC = dt.Rows[0]["Additional_Amt_Covered_ACC"].ToString();
                }
                if (dt.Rows[0]["Additional_Amt_Covered_ACC2"].ToString() != "")
                {
                    Additional_Amt_Covered_ACC = Additional_Amt_Covered_ACC + "#" + dt.Rows[0]["Additional_Amt_Covered_ACC2"].ToString();
                }
                if (dt.Rows[0]["Additional_Amt_Covered_ACC3"].ToString() != "")
                {
                    Additional_Amt_Covered_ACC = Additional_Amt_Covered_ACC + "#" + dt.Rows[0]["Additional_Amt_Covered_ACC3"].ToString();
                }
                if (dt.Rows[0]["Additional_Amt_Covered_ACC4"].ToString() != "")
                {
                    Additional_Amt_Covered_ACC = Additional_Amt_Covered_ACC + "#" + dt.Rows[0]["Additional_Amt_Covered_ACC4"].ToString();
                }
                if (Additional_Amt_Covered_ACC.Length != 0)
                {
                    comma = Additional_Amt_Covered_ACC.Trim().Substring(0, 1);
                    if (comma == "#") { Additional_Amt_Covered_ACC = Additional_Amt_Covered_ACC.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region AvailableWithBy
                if (dt.Rows[0]["AvailableWithByHeader"].ToString() == "A")
                {
                    if (dt.Rows[0]["AvailableWithBySwiftCode"].ToString() != "")
                    {
                        AvailableWithBy = dt.Rows[0]["AvailableWithByCode"].ToString();
                    }
                    if (AvailableWithBy != "" && dt.Rows[0]["AvailableWithBySwiftCode"].ToString() != "")
                    {
                        AvailableWithBy = AvailableWithBy + "#" + dt.Rows[0]["AvailableWithBySwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AvailableWithByCode"].ToString() != "")
                        {
                            AvailableWithBy = dt.Rows[0]["AvailableWithByCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AvailableWithByHeader"].ToString() == "D")
                {
                    AvailableWithBy = dt.Rows[0]["AvailableWithByName"].ToString();
                    if (dt.Rows[0]["AvailableWithByAddress1"].ToString() != "")
                    {
                        AvailableWithBy = AvailableWithBy + "#" + dt.Rows[0]["AvailableWithByAddress1"].ToString();
                    }
                    if (dt.Rows[0]["AvailableWithByAddress2"].ToString() != "")
                    {
                        AvailableWithBy = AvailableWithBy + "#" + dt.Rows[0]["AvailableWithByAddress2"].ToString();
                    }
                    if (dt.Rows[0]["AvailableWithByAddress3"].ToString() != "")
                    {
                        AvailableWithBy = AvailableWithBy + "#" + dt.Rows[0]["AvailableWithByAddress3"].ToString();
                    }
                    if (dt.Rows[0]["AvailableWithByCode"].ToString() != "")
                    {
                        AvailableWithBy = AvailableWithBy + "#" + dt.Rows[0]["AvailableWithByCode"].ToString();
                    }
                }
                if (AvailableWithBy.Length != 0)
                {
                    comma = AvailableWithBy.Trim().Substring(0, 1);
                    if (comma == "#") { AvailableWithBy = AvailableWithBy.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Drawee
                if (dt.Rows[0]["DraweeHeader"].ToString() == "A")
                {
                    if (dt.Rows[0]["Drawee_ACNo"].ToString() != "")
                    {
                        Drawee = dt.Rows[0]["Drawee_ACNo"].ToString();
                    }
                    if (Drawee != "" && dt.Rows[0]["Drawee_Swiftcode"].ToString() != "")
                    {
                        Drawee = Drawee + "#" + dt.Rows[0]["Drawee_Swiftcode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Drawee_Swiftcode"].ToString() != "")
                        {
                            Drawee = dt.Rows[0]["Drawee_Swiftcode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["DraweeHeader"].ToString() == "D")
                {
                    Drawee = dt.Rows[0]["Drawee_ACNo"].ToString();
                    if (dt.Rows[0]["DraweeName"].ToString() != "")
                    {
                        Drawee = Drawee + "#" + dt.Rows[0]["DraweeName"].ToString();
                    }
                    if (dt.Rows[0]["DraweeAddress1"].ToString() != "")
                    {
                        Drawee = Drawee + "#" + dt.Rows[0]["DraweeAddress1"].ToString();
                    }
                    if (dt.Rows[0]["DraweeAddress2"].ToString() != "")
                    {
                        Drawee = Drawee + "#" + dt.Rows[0]["DraweeAddress2"].ToString();
                    }
                    if (dt.Rows[0]["DraweeAddress3"].ToString() != "")
                    {
                        Drawee = Drawee + "#" + dt.Rows[0]["DraweeAddress3"].ToString();
                    }
                }
                if (Drawee.Length != 0)
                {
                    comma = Drawee.Trim().Substring(0, 1);
                    if (comma == "#") { Drawee = Drawee.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region MixedPaymentDetails
                if (dt.Rows[0]["MixedPaymentDetails"].ToString() != "")
                {
                    MixedPaymentDetails = dt.Rows[0]["MixedPaymentDetails"].ToString();
                }
                if (dt.Rows[0]["MixedPaymentDetails2"].ToString() != "")
                {
                    MixedPaymentDetails = MixedPaymentDetails + "#" + dt.Rows[0]["MixedPaymentDetails2"].ToString();
                }
                if (dt.Rows[0]["MixedPaymentDetails3"].ToString() != "")
                {
                    MixedPaymentDetails = MixedPaymentDetails + "#" + dt.Rows[0]["MixedPaymentDetails3"].ToString();
                }
                if (dt.Rows[0]["MixedPaymentDetails4"].ToString() != "")
                {
                    MixedPaymentDetails = MixedPaymentDetails + "#" + dt.Rows[0]["MixedPaymentDetails4"].ToString();
                }
                if (MixedPaymentDetails.Length != 0)
                {
                    comma = MixedPaymentDetails.Trim().Substring(0, 1);
                    if (comma == "#") { MixedPaymentDetails = MixedPaymentDetails.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region DeferredPaymentDetails
                if (dt.Rows[0]["DeferredPaymentDetails"].ToString() != "")
                {
                    DeferredPaymentDetails = dt.Rows[0]["DeferredPaymentDetails"].ToString();
                }
                if (dt.Rows[0]["DeferredPaymentDetails2"].ToString() != "")
                {
                    DeferredPaymentDetails = DeferredPaymentDetails + "#" + dt.Rows[0]["DeferredPaymentDetails2"].ToString();
                }
                if (dt.Rows[0]["DeferredPaymentDetails3"].ToString() != "")
                {
                    DeferredPaymentDetails = DeferredPaymentDetails + "#" + dt.Rows[0]["DeferredPaymentDetails3"].ToString();
                }
                if (dt.Rows[0]["DeferredPaymentDetails4"].ToString() != "")
                {
                    DeferredPaymentDetails = DeferredPaymentDetails + "#" + dt.Rows[0]["DeferredPaymentDetails4"].ToString();
                }
                if (DeferredPaymentDetails.Length != 0)
                {
                    comma = DeferredPaymentDetails.Trim().Substring(0, 1);
                    if (comma == "#") { DeferredPaymentDetails = DeferredPaymentDetails.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Other_Charges_ACC
                if (dt.Rows[0]["Other_Charges_ACC"].ToString() != "")
                {
                    Other_Charges_ACC = dt.Rows[0]["Other_Charges_ACC"].ToString();
                }
                if (dt.Rows[0]["Other_Charges_ACC2"].ToString() != "")
                {
                    Other_Charges_ACC = Other_Charges_ACC + "#" + dt.Rows[0]["Other_Charges_ACC2"].ToString();
                }
                if (dt.Rows[0]["Other_Charges_ACC3"].ToString() != "")
                {
                    Other_Charges_ACC = Other_Charges_ACC + "#" + dt.Rows[0]["Other_Charges_ACC3"].ToString();
                }
                if (dt.Rows[0]["Other_Charges_ACC4"].ToString() != "")
                {
                    Other_Charges_ACC = Other_Charges_ACC + "#" + dt.Rows[0]["Other_Charges_ACC4"].ToString();
                }
                if (dt.Rows[0]["Other_Charges_ACC5"].ToString() != "")
                {
                    Other_Charges_ACC = Other_Charges_ACC + "#" + dt.Rows[0]["Other_Charges_ACC5"].ToString();
                }
                if (dt.Rows[0]["Other_Charges_ACC6"].ToString() != "")
                {
                    Other_Charges_ACC = Other_Charges_ACC + "#" + dt.Rows[0]["Other_Charges_ACC6"].ToString();
                }
                if (Other_Charges_ACC.Length != 0)
                {
                    comma = Other_Charges_ACC.Trim().Substring(0, 1);
                    if (comma == "#") { Other_Charges_ACC = Other_Charges_ACC.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region SendertoReceiverInformation
                if (dt.Rows[0]["Sender_to_Receiver_Information"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + dt.Rows[0]["Sender_to_Receiver_Information"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Sender_to_Receiver_Information2"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["Sender_to_Receiver_Information2"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Sender_to_Receiver_Information3"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["Sender_to_Receiver_Information3"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Sender_to_Receiver_Information4"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["Sender_to_Receiver_Information4"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Sender_to_Receiver_Information5"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["Sender_to_Receiver_Information5"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Sender_to_Receiver_Information6"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["Sender_to_Receiver_Information6"].ToString().ToString().Replace("/", "");
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == "#") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Maximum Credit Amount
                if (dt.Rows[0]["MaximumCreditAmount"].ToString() != "") //vinay swift change
                {
                    if (dt.Rows[0]["MaximumCreditAmount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["MaximumCreditAmount"].ToString().Contains(".00"))
                        {
                            MaximumCreditAmount = MaximumCreditAmount + dt.Rows[0]["MaximumCreditAmount"].ToString().Replace(".00", ",");
                        }
                        else
                        {

                            MaximumCreditAmount = MaximumCreditAmount + dt.Rows[0]["MaximumCreditAmount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        MaximumCreditAmount = MaximumCreditAmount + dt.Rows[0]["MaximumCreditAmount"].ToString() + ",";
                    }
                }
                #endregion
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<DocumentaryCreditNumber>" + DocumentCreditNumber + "</DocumentaryCreditNumber>");
                sw.WriteLine("<AccountIdentification>" + dt.Rows[0]["AccountIdentification"].ToString() + "</AccountIdentification>");
                sw.WriteLine("<ApplicableRules>" + ApplicableRules + "</ApplicableRules>");
                sw.WriteLine("<DateandPlaceofExpiry>" + DateandPlaceofExpiry + "</DateandPlaceofExpiry>");
                if (dt.Rows[0]["NegoRemiBank"].ToString() == "A")
                {
                    sw.WriteLine("<NegotiatingBankA>" + Negotiating_Bank + "</NegotiatingBankA>");
                }
                if (dt.Rows[0]["NegoRemiBank"].ToString() == "D")
                {
                    sw.WriteLine("<NegotiatingBankD>" + Negotiating_Bank + "</NegotiatingBankD>");
                }
                sw.WriteLine("<Beneficiary>" + Beneficiary + "</Beneficiary>");
                sw.WriteLine("<CreditAmount>" + CreditAmount + "</CreditAmount>");
                sw.WriteLine("<PercentageCreditAmountTolerance>" + PercentageCreditAmountTolerance + "</PercentageCreditAmountTolerance>");
                sw.WriteLine("<MaximumCreditAmount>" + MaximumCreditAmount + "</MaximumCreditAmount>");
                sw.WriteLine("<AdditionalAmountsCovered>" + Additional_Amt_Covered_ACC + "</AdditionalAmountsCovered>"); //vinay Swift Changes Tagname

                if (dt.Rows[0]["AvailableWithByHeader"].ToString() == "A")
                {
                    sw.WriteLine("<AvailableWithByA>" + AvailableWithBy + "</AvailableWithByA>");
                }
                if (dt.Rows[0]["AvailableWithByHeader"].ToString() == "D")
                {
                    sw.WriteLine("<AvailableWithByD>" + AvailableWithBy + "</AvailableWithByD>");
                }

                sw.WriteLine("<Draftsat>" + Draftsat + "</Draftsat>");
                if (dt.Rows[0]["DraweeHeader"].ToString() == "A")
                {
                    sw.WriteLine("<DraweeA>" + Drawee + "</DraweeA>");
                }
                if (dt.Rows[0]["DraweeHeader"].ToString() == "D")
                {
                    sw.WriteLine("<DraweeD>" + Drawee + "</DraweeD>");
                }

                sw.WriteLine("<MixedPaymentDetails>" + MixedPaymentDetails + "</MixedPaymentDetails>");
                sw.WriteLine("<DeferredPaymentDetails>" + DeferredPaymentDetails + "</DeferredPaymentDetails>");
                sw.WriteLine("<ReimbursingBankCharges>" + ReimbursingBankCharges + "</ReimbursingBankCharges>");
                sw.WriteLine("<OtherCharges>" + Other_Charges_ACC + "</OtherCharges>");
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }  // Swift Changes
    public void CreateSwift999_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999_ACC_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
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
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS999/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT999_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN999_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                Narrative = Narrative + "KINDLY TREAT THIS MESSAGE AS MT-756 AS WE DO NOT";
                Narrative = Narrative + " HAVE RMA WITH YOUR GOOD BANK.";
                Narrative = Narrative + ".";
                Narrative = Narrative + " 20: " + dt.Rows[0]["SendersReference"].ToString();

                if (dt.Rows[0]["PresentingBanksReference"].ToString() != "")
                {
                    Narrative = Narrative + " " + "21: " + dt.Rows[0]["PresentingBanksReference"].ToString(); ;
                }
                if (dt.Rows[0]["TotalAmountClaimedCurr"].ToString() != "")
                {
                    Narrative = Narrative + " " + "32B: " + dt.Rows[0]["TotalAmountClaimedCurr"].ToString() + " " + dt.Rows[0]["TotalAmountClaimedAmount"].ToString();
                }
                if (dt.Rows[0]["AmountReimbursedorPaidCurr"].ToString() != "")
                {
                    Narrative = Narrative + " " + "33A: " + dt.Rows[0]["AmountReimbursedorPaidDate"].ToString() + " " + dt.Rows[0]["AmountReimbursedorPaidCurr"].ToString() + " " + dt.Rows[0]["AmountReimbursedorPaidAmount"].ToString();
                }
                if (dt.Rows[0]["SendersCorrespondent1"].ToString() != "")
                {
                    Narrative = Narrative + " " + "53A: " + dt.Rows[0]["SendersCorrespondent1"].ToString();
                }
                if (dt.Rows[0]["SendersCorrespondent2"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["SendersCorrespondent2"].ToString();
                }
                if (dt.Rows[0]["SendersCorrespondent3"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["SendersCorrespondent3"].ToString();
                }
                if (dt.Rows[0]["SendersCorrespondent4"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["SendersCorrespondent4"].ToString();
                }
                if (dt.Rows[0]["SendersCorrespondent5"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["SendersCorrespondent5"].ToString();
                }
                if (dt.Rows[0]["ReceiverPartyIdentifier"].ToString() != "")
                {
                    Narrative = Narrative + " " + "54A: " + dt.Rows[0]["ReceiverPartyIdentifier"].ToString();
                }
                if (dt.Rows[0]["ReceiverAccountNumberMT"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["ReceiverAccountNumberMT"].ToString();
                }
                if (dt.Rows[0]["ReceiverSwiftCodeMT"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["ReceiverSwiftCodeMT"].ToString();
                }
                if (dt.Rows[0]["ReceiverLocationMT"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["ReceiverLocationMT"].ToString();
                }
                if (dt.Rows[0]["ReceiverNameMT"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["ReceiverNameMT"].ToString();
                }
                if (dt.Rows[0]["ReceiverAddress1MT"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["ReceiverAddress1MT"].ToString();
                }
                if (dt.Rows[0]["ReceiverAddress2MT"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["ReceiverAddress2MT"].ToString();
                }
                if (dt.Rows[0]["ReceiverAddress3MT"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["ReceiverAddress3MT"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation1"].ToString() != "")
                {
                    Narrative = Narrative + " " + "72Z: " + dt.Rows[0]["SendertoReceiverInformation1"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["SendertoReceiverInformation2"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["SendertoReceiverInformation3"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["SendertoReceiverInformation4"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["SendertoReceiverInformation5"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["SendertoReceiverInformation6"].ToString();
                }
                if (dt.Rows[0]["Narrative7561"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7561"].ToString();
                }
                if (dt.Rows[0]["Narrative7562"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7562"].ToString();
                }
                if (dt.Rows[0]["Narrative7563"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7563"].ToString();
                }
                if (dt.Rows[0]["Narrative7564"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7564"].ToString();
                }
                if (dt.Rows[0]["Narrative7565"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7565"].ToString();
                }
                if (dt.Rows[0]["Narrative7566"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7566"].ToString();
                }
                if (dt.Rows[0]["Narrative7567"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7567"].ToString();
                }
                if (dt.Rows[0]["Narrative7568"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7568"].ToString();
                }
                if (dt.Rows[0]["Narrative7569"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7569"].ToString();
                }
                if (dt.Rows[0]["Narrative75610"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75610"].ToString();
                }
                if (dt.Rows[0]["Narrative75611"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75611"].ToString();
                }
                if (dt.Rows[0]["Narrative75612"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75612"].ToString();
                }
                if (dt.Rows[0]["Narrative75613"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75613"].ToString();
                }
                if (dt.Rows[0]["Narrative75614"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75614"].ToString();
                }
                if (dt.Rows[0]["Narrative75615"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75615"].ToString();
                }
                if (dt.Rows[0]["Narrative75616"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75616"].ToString();
                }
                if (dt.Rows[0]["Narrative75617"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75617"].ToString();
                }
                if (dt.Rows[0]["Narrative75618"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75618"].ToString();
                }
                if (dt.Rows[0]["Narrative75619"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75619"].ToString();
                }
                if (dt.Rows[0]["Narrative75620"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75620"].ToString();
                }
                if (dt.Rows[0]["Narrative75621"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75621"].ToString();
                }
                if (dt.Rows[0]["Narrative75622"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75622"].ToString();
                }
                if (dt.Rows[0]["Narrative75623"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75623"].ToString();
                }
                if (dt.Rows[0]["Narrative75624"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75624"].ToString();
                }
                if (dt.Rows[0]["Narrative75625"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75625"].ToString();
                }
                if (dt.Rows[0]["Narrative75626"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75626"].ToString();
                }
                if (dt.Rows[0]["Narrative75627"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75627"].ToString();
                }
                if (dt.Rows[0]["Narrative75628"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75628"].ToString();
                }
                if (dt.Rows[0]["Narrative75629"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75629"].ToString();
                }
                if (dt.Rows[0]["Narrative75630"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75630"].ToString();
                }
                if (dt.Rows[0]["Narrative75631"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75631"].ToString();
                }
                if (dt.Rows[0]["Narrative75632"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75632"].ToString();
                }
                if (dt.Rows[0]["Narrative75633"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75633"].ToString();
                }
                if (dt.Rows[0]["Narrative75634"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75634"].ToString();
                }
                if (dt.Rows[0]["Narrative75635"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative75635"].ToString();
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["SendersReference"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["PresentingBanksReference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

    }
    public void CreateSwift799_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT799ACC_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
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
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT799/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS799/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                //_filePath = _directoryPath + "/" + txtDocNo.Text + "_MT799.XML";
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT799_" + TodayDate1 + ".xml";
            }
            else
            {
                //_filePath = _directoryPath + "/" + txtDocNo.Text + "_MT799.XML";
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN799_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative36"].ToString() != "")
                {
                    Narrative = Narrative + "\n" + "<Narrative>" + dt.Rows[0]["Narrative35"].ToString().Replace("/", "") + "</Narrative>";
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    } // Swift Changes
    public void CreateSwift747_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT747_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
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
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT747/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS747/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT747_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN747_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = ""; string comma = "";
                #region Narrative
                if (dt.Rows[0]["Narrative1_747"].ToString() != "")
                {
                    Narrative = dt.Rows[0]["Narrative1_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative2_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative3_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative4_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative5_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative6_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative8_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative9_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative10_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative11_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative12_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative13_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative14_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative15_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative16_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative17_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative18_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative19_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative20_747"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20_747"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region SendertoReceiverInformation
                string SendertoReceiverInformation = "";
                if (dt.Rows[0]["SenToRecInfo1_747"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SenToRecInfo1_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecInfo2_747"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecInfo2_747"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecInfo3_747"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecInfo3_747"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecInfo4_747"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecInfo4_747"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecInfo5_747"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecInfo5_747"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecInfo6_747"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecInfo6_747"].ToString().ToString().Replace("/", "");
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == ",") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region AdditionalAmountsCovered
                string AdditionalAmountsCovered = "";
                if (dt.Rows[0]["AddAmtCovered1_747"].ToString() != "")
                {
                    AdditionalAmountsCovered = dt.Rows[0]["AddAmtCovered1_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["AddAmtCovered2_747"].ToString() != "")
                {
                    AdditionalAmountsCovered = AdditionalAmountsCovered + "," + dt.Rows[0]["AddAmtCovered2_747"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["AddAmtCovered3_747"].ToString() != "")
                {
                    AdditionalAmountsCovered = AdditionalAmountsCovered + "," + dt.Rows[0]["AddAmtCovered3_747"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["AddAmtCovered4_747"].ToString() != "")
                {
                    AdditionalAmountsCovered = AdditionalAmountsCovered + "," + dt.Rows[0]["AddAmtCovered4_747"].ToString().ToString().Replace("/", "");
                }
                if (AdditionalAmountsCovered.Length != 0)
                {
                    comma = AdditionalAmountsCovered.Trim().Substring(0, 1);
                    if (comma == ",") { AdditionalAmountsCovered = AdditionalAmountsCovered.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region IncreaseofDocumentaryCreditAmount
                string IncreaseofDocumentaryCreditAmount = "";
                if (dt.Rows[0]["IncreaseofDocumentryCreditAmt_Curr_747"].ToString() != "")
                {
                    IncreaseofDocumentaryCreditAmount = dt.Rows[0]["IncreaseofDocumentryCreditAmt_Curr_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString() != "")
                {
                    if (dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString().Contains(".00"))
                        {
                            IncreaseofDocumentaryCreditAmount = IncreaseofDocumentaryCreditAmount + dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            IncreaseofDocumentaryCreditAmount = IncreaseofDocumentaryCreditAmount + dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        IncreaseofDocumentaryCreditAmount = IncreaseofDocumentaryCreditAmount + dt.Rows[0]["IncreaseofDocumentryCreditAmt_747"].ToString() + ",";
                    }
                }
                #endregion
                #region DecreaseofDocumentaryCreditAmount
                string DecreaseofDocumentaryCreditAmount = "";
                if (dt.Rows[0]["decreaseofDocumentryCreditAmt_Curr_747"].ToString() != "")
                {
                    DecreaseofDocumentaryCreditAmount = dt.Rows[0]["decreaseofDocumentryCreditAmt_Curr_747"].ToString().Replace("/", "");

                }
                if (dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString() != "")
                {
                    if (dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString().Contains(".00"))
                        {
                            DecreaseofDocumentaryCreditAmount = DecreaseofDocumentaryCreditAmount + dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            DecreaseofDocumentaryCreditAmount = DecreaseofDocumentaryCreditAmount + dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        DecreaseofDocumentaryCreditAmount = DecreaseofDocumentaryCreditAmount + dt.Rows[0]["decreaseofDocumentryCreditAmt_747"].ToString() + ",";
                    }
                }
                #endregion
                #region NewDocumentaryCreditAmountAfterAmendment
                string NewDocumentaryCreditAmountAfterAmendment = "";
                if (dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_Curr_747"].ToString() != "")
                {
                    NewDocumentaryCreditAmountAfterAmendment = dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_Curr_747"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString() != "")
                {
                    if (dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString().Contains(".00"))
                        {
                            NewDocumentaryCreditAmountAfterAmendment = NewDocumentaryCreditAmountAfterAmendment + dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            NewDocumentaryCreditAmountAfterAmendment = NewDocumentaryCreditAmountAfterAmendment + dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        NewDocumentaryCreditAmountAfterAmendment = NewDocumentaryCreditAmountAfterAmendment + dt.Rows[0]["NewDocumentryCreditAmtAfterAmendment_747"].ToString() + ",";
                    }
                }
                #endregion
                #region PercentageCreditAmountTolerance
                string PercentageCreditAmountTolerance = "";
                if (dt.Rows[0]["PercentageCreditAmtTolerance1_747"].ToString() != "")
                {
                    PercentageCreditAmountTolerance = dt.Rows[0]["PercentageCreditAmtTolerance1_747"].ToString() + "/" +
                                                    dt.Rows[0]["PercentageCreditAmtTolerance2_747"].ToString();
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<DocumentaryCreditNumber>" + dt.Rows[0]["DocCreditNo_747"].ToString() + "</DocumentaryCreditNumber>");
                sw.WriteLine("<ReimbursingBanksReference>" + dt.Rows[0]["ReimbursingBankRef_747"].ToString() + "</ReimbursingBanksReference>");
                sw.WriteLine("<Dateof>" + dt.Rows[0]["DateoforiginalAuthoOfReimburse_747"].ToString() + "</Dateof>");
                sw.WriteLine("<NewDateofExpiry>" + dt.Rows[0]["NewDateOfExpiry_747"].ToString() + "</NewDateofExpiry>");
                sw.WriteLine("<Increaseof>" + IncreaseofDocumentaryCreditAmount + "</Increaseof>");
                sw.WriteLine("<Decreaseof>" + DecreaseofDocumentaryCreditAmount + "</Decreaseof>");
                sw.WriteLine("<NewDocumentary>" + NewDocumentaryCreditAmountAfterAmendment + "</NewDocumentary>");
                sw.WriteLine("<Percentage>" + PercentageCreditAmountTolerance + "</Percentage>");
                sw.WriteLine("<Additional>" + AdditionalAmountsCovered + "</Additional>");
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    } // swift Changes
    public void GBaseFileCreationGOSwiftSFMS()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Acceptance_GOSF", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_GOSwiftSFMS" + ".xlsx";
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
    public void GBaseFileCreationGOLCComm()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Acceptance_GOLC", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_GOLCComm" + ".xlsx";
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
    public void GBaseFileCreationLedger()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Acceptance_Ledger", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/LEDGER/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_Ledger" + ".xlsx";
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
    protected void InsertDataForSwiftSTP(string SwiftType, string DocNo)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", DocNo);
        SqlParameter P2 = new SqlParameter("@SwiftType", SwiftType);
        string result = obj.SaveDeleteData("TF_SWIFT_STP_InsertAcceptanceData", P1, P2);
    }
    protected void DeleteDataForSwiftSTP()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string result = obj.SaveDeleteData("TF_SWIFT_STP_DeleteAcceptanceData", P1);
    }

    //----------------Added by bhupen on 17112022 for LEI-----------------------------------//
    protected void GetDrawee_Details_LEI()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_GetLodgementLEIdetails_acc", PDocNo);
        if (dt.Rows.Count > 0)
        {
            hdnDrawerno.Value = dt.Rows[0]["Drawer"].ToString();
            if (hdnDrawerno.Value != "")
            {
                SqlParameter CustAcno = new SqlParameter("@CustAcno", txtCustomer_ID.Text);
                SqlParameter Drawerno = new SqlParameter("@Drawee", hdnDrawerno.Value);
                DataTable dt1 = new DataTable();
                dt1 = obj.getData("TF_IMP_GetDraweedetails_acceptance", CustAcno, Drawerno);
                if (dt1.Rows.Count > 0)
                {
                    hdnDrawer.Value = dt1.Rows[0]["Drawer_NAME"].ToString();
                }
            }
        }
    }
    private void Check_LEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.ToString().Trim());
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString().Trim());
            string _query = "TF_IMP_GetLEIDetails_Customer";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LEI_No1"].ToString().Trim() == "")
                {
                    lblLEI_Remark.Text = "...Not Verified.";
                    lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                    hdnleino.Value = "";
                }
                else
                {
                    lblLEI_Remark.Text = "...Verified.";
                    hdnleino.Value = dt.Rows[0]["LEI_No"].ToString();
                }
            }
            else
            {
                lblLEI_Remark.Text = "...Not Verified.";
                lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleino.Value = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_LEINO_ExpirydateDetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.ToString().Trim());
            SqlParameter p2 = new SqlParameter("@DueDate", txtInterest_To.Text.ToString().Trim());
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString().Trim());

            string _query = "TF_IMP_GetLEIDetails_ExpiryDate";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Statuschk"].ToString() == "Grater")
                {
                    lblLEIExpiry_Remark.Text = "...Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "N";
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark.Text = "...Not Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "Y";
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_Remark.Text = "...Not Verified.";
                lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleiexpiry.Value = "";
                hdnleiexpiry1.Value = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_LEINO_ExchRateDetails()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            try
            {
                TF_DATA objData = new TF_DATA();
                string result2 = "";
                SqlParameter p1 = new SqlParameter("@CurrCode", SqlDbType.VarChar);
                p1.Value = lblDoc_Curr.Text.Trim();
                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.VarChar);
                p2.Value = txtValueDate.Text.ToString();
                string _query = "TF_IMP_GetLEI_RateCardDetails";
                DataTable dt = objData.getData(_query, p1, p2);
                if (dt.Rows.Count > 0)
                {
                    string Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    string Amount = txtDraftAmt.Text.Trim().Replace(",", "");
                    if (Amount != "" && Exch_rate != "")
                    {
                        SqlParameter billamt = new SqlParameter("@billamt", Amount);
                        SqlParameter exch_Rate = new SqlParameter("@exch_Rate", Exch_rate);
                        string _queryC = "TF_IMP_GetThresholdLimitCheck";
                        result2 = objData.SaveDeleteData(_queryC, billamt, exch_Rate);

                        if (result2 == "Grater")
                        {
                            lbl_LEIAmt_Check.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit.Please Verify LEI details.";
                            lbl_LEIAmt_Check.ForeColor = System.Drawing.Color.Red;
                            lbl_LEIAmt_Check.Font.Size = 10;
                            //lbl_LEIAmt_Check.Font.Bold = true;
                            hdnLeiFlag.Value = "Y";
                            ddlApproveReject.Enabled = false;
                            btn_Verify.Visible = true;
                        }
                        else
                        {
                            lbl_LEIAmt_Check.Text = "Transaction Bill Amt is less than LEI Thresold Limit.";
                            lbl_LEIAmt_Check.ForeColor = System.Drawing.Color.Green;
                            lbl_LEIAmt_Check.Font.Size = 10;
                            hdnLeiFlag.Value = "N";
                            ddlApproveReject.Enabled = true;
                            btn_Verify.Visible = false;
                        }
                        Check_cust_Leiverify();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('No Exch Rate is available for currency.')", true);
                }
            }
            catch (Exception ex)
            {
                //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
            }
        }
    }
    private void Check_DraweeLEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", txtCustomer_ID.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Draweename", hdnDrawer.Value.Trim());
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());

            string _query = "TF_IMP_GetLEIDetails_Drawee";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Drawer_LEI_NO"].ToString() == "")
                {
                    lblLEI_Remark_Drawee.Text = "...Not Verified.";
                    lblLEI_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                    hdnDraweeleino.Value = "";
                }
                else
                {
                    lblLEI_Remark_Drawee.Text = "...Verified.";
                    hdnDraweeleino.Value = dt.Rows[0]["Drawer_LEI_NO"].ToString();
                }
            }
            else
            {
                lblLEI_Remark_Drawee.Text = "...Not Verified.";
                lblLEI_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleino.Value = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_DraweeLEINO_ExpirydateDetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", txtCustomer_ID.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Draweename", hdnDrawer.Value.Trim());
            SqlParameter p3 = new SqlParameter("@DueDate", txtInterest_To.Text.ToString().Trim());
            SqlParameter p4 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());

            string _query = "TF_IMP_GetLEIDetails_ExpiryDate_Drawee";
            DataTable dt = objData.getData(_query, p1, p2, p3, p4);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Statuschk"].ToString() == "Grater")
                {
                    lblLEIExpiry_Remark_Drawee.Text = "...Verified.";
                    lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Green;
                    hdnDraweeleiexpiry.Value = dt.Rows[0]["Drawer_LEIExpiryDate"].ToString();
                    hdnDraweeleiexpiry1.Value = "N";
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                    lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Green;
                    hdnDraweeleiexpiry.Value = dt.Rows[0]["Drawer_LEIExpiryDate"].ToString();
                    hdnDraweeleiexpiry1.Value = "Y";
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleiexpiry.Value = "";
                hdnDraweeleiexpiry1.Value = "";
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void Lei_Audit()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            try
            {
                TF_DATA obj = new TF_DATA();
                SqlParameter P_BranchCode = new SqlParameter("@BranchCode", hdnBranchCode.Value.Trim());
                SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
                SqlParameter P_DocType = new SqlParameter("@DocType", "ACC");
                SqlParameter P_Sight_Usance = new SqlParameter("@billType", lblSight_Usance.Text.Trim());
                SqlParameter P_CustAcno = new SqlParameter("@CustAcNo", txtCustomer_ID.Text.Trim());
                SqlParameter P_Cust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
                SqlParameter P_Cust_Name = new SqlParameter("@Cust_Name", lblCustomerDesc.Text.Trim());
                SqlParameter P_Cust_LEI = new SqlParameter("@Cust_LEI", hdnleino.Value.Trim());
                SqlParameter P_Cust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdnleiexpiry.Value.Trim());
                SqlParameter P_Cust_LEI_Expired = new SqlParameter("@Cust_LEI_Expired", hdnleiexpiry1.Value.Trim());
                SqlParameter P_Drawee_Name = new SqlParameter("@Drawee_Name", hdnDrawer.Value.Trim());
                SqlParameter P_Drawee_LEI = new SqlParameter("@Drawee_LEI", hdnDraweeleino.Value.Trim());
                SqlParameter P_Drawee_LEI_Expiry = new SqlParameter("@Drawee_LEI_Expiry", hdnDraweeleiexpiry.Value.Trim());
                SqlParameter P_Drawee_LEI_Expired = new SqlParameter("@Drawee_LEI_Expired", hdnDraweeleiexpiry1.Value.Trim());
                SqlParameter P_BillAmount = new SqlParameter("@BillAmt", txtDraftAmt.Text.Trim().Replace(",", ""));
                SqlParameter P_txtCurr = new SqlParameter("@Curr", lblDoc_Curr.Text.Trim());
                SqlParameter P_Exchrate = new SqlParameter("@ExchRt", lbl_Exch_rate.Text.Trim());
                SqlParameter P_LodgementDate = new SqlParameter("@LodgDate", txtValueDate.Text.Trim());
                SqlParameter P_DueDate = new SqlParameter("@DueDate", txtInterest_To.Text.Trim());
                SqlParameter P_LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value.Trim());
                SqlParameter P_CustLEI_Flag = new SqlParameter("@Cust_LEIFlag", hdncustleiflag.Value.Trim());
                SqlParameter P_Status = new SqlParameter("@status", ddlApproveReject.SelectedItem.Text.Trim());
                SqlParameter P_Module = new SqlParameter("@Module", "A");
                SqlParameter P_AddedBy = new SqlParameter("@user", hdnUserName.Value.Trim());
                SqlParameter P_AddedDate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                string _queryLEI = "TF_IMP_AddUpdate_LEITransaction";

                string _result = obj.SaveDeleteData(_queryLEI, P_BranchCode, P_DocNo, P_DocType, P_Sight_Usance, P_CustAcno, P_Cust_Abbr, P_Cust_Name, P_Cust_LEI,
                   P_Cust_LEI_Expiry, P_Cust_LEI_Expired, P_Drawee_Name, P_Drawee_LEI, P_Drawee_LEI_Expiry, P_Drawee_LEI_Expired, P_BillAmount, P_txtCurr, P_Exchrate,
                   P_LodgementDate, P_DueDate, P_LEI_Flag,P_CustLEI_Flag, P_Status, P_Module, P_AddedBy, P_AddedDate);
            }
            catch (Exception ex)
            {
                //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
            }
        }
    }
    protected void btn_Verify_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCustomer_ID.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please Select Customer for Verifying LEI details.')", true);
                txtCustomer_ID.Focus();
            }
            else if (lblBillAmt.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Amount for Verifying LEI details.')", true);
                lblBillAmt.Focus();
            }
            else if (txtInterest_To.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Due date for Verifying LEI details.')", true);
                txtInterest_To.Focus();
            }
            else
            {
                GetDrawee_Details_LEI();
                Check_LEINODetails();
                Check_LEINO_ExpirydateDetails();
                Check_DraweeLEINODetails();
                Check_DraweeLEINO_ExpirydateDetails();
                ddlApproveReject.Enabled = true;

                String CustLEINo = hdnleino.Value + " " + lblLEI_Remark.Text + "\\n";
                String CustLEIExpiry = hdnleiexpiry.Value + " " + lblLEIExpiry_Remark.Text + "\\n";
                String DrawerLEINo = hdnDraweeleino.Value + " " + lblLEI_Remark_Drawee.Text + "\\n";
                String DrawerLEIExpiry = hdnDraweeleiexpiry.Value + " " + lblLEIExpiry_Remark_Drawee.Text + "\\n";

                String LEIMSG = @"Customer LEI No : " + CustLEINo + "Customer LEI Expiry : " + CustLEIExpiry + "Drawer LEI : " + DrawerLEINo + "Drawer LEI Expiry : " + DrawerLEIExpiry;
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "messege", "MyConfirm('" + LEIMSG + "')", true);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('" + LEIMSG + "')", true);
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_cust_Leiverify()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //SqlParameter p3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            string _query = "TF_IMP_CheckLEIflaggedCust_Count_Checker";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                //hdncustleiflag.Value = dt.Rows[0]["Cust_Lei_Flag"].ToString();
                //if (hdncustleiflag.Value == "R")
                //{
                    ddlApproveReject.Enabled = false;
                    btn_Verify.Visible = true;
                    ReccuringLEI.Visible = true;
                    ReccuringLEI.Text = "This is Recurring LEI Customer.";
                    ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                    ReccuringLEI.Font.Size = 10;
                //}
            }
        }
    }
    private void Check_Lei_Verified()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Module", "A");
            string _query = "TF_IMP_Check_LeiVerified";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                LeiVerified.Visible = true;
                if (lbl_LEIAmt_Check.Text == "Transaction Bill Amt is Greater than LEI Thresold Limit.Please Verify LEI details.")
                {
                    lbl_LEIAmt_Check.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit.";
                }
                LeiVerified.Text = "LEI Already Verified.";
                LeiVerified.ForeColor = System.Drawing.Color.Red;
                LeiVerified.Font.Size = 10;
            }
            else
            {
                LeiVerified.Visible = false;
            }
        }
    }
    private void Check_Lei_RecurringStatus()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Module", "A");
            string _query = "TF_IMP_Check_LeiRecurring";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                string cust_lei_flag = dt.Rows[0]["Cust_Lei_Flag"].ToString();
                string Tstatus = dt.Rows[0]["TStatus"].ToString();
                if (cust_lei_flag.ToString().Trim() == "" && Tstatus.ToString().Trim() == "A")
                {
                    ReccuringLEI.Visible = false;
                }
            }
        }
    }
    //------------------------------------End------------------------------------------//
}
