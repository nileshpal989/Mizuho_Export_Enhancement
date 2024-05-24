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

public partial class IMP_Transactions_TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker : System.Web.UI.Page
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
            hdnUserName.Value = Session["userName"].ToString();
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
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();

                    TF_DATA obj = new TF_DATA();
                    SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
                    string result = "";
                    result = obj.SaveDeleteData("TF_IMP_Check_Ref_In_Acceptance", PDocNo);
                    if (result == "exists")
                    {
                        hdnMode.Value = "Edit";
                    }
                    else
                    {
                        hdnMode.Value = "Add";
                    }

                    txtIBD_ACC_kind.Text = "1";
                    txtAutoSettlement.Text = "0";
                    txtCountryRisk.Text = "IN";
                    txtGradeCode.Text = "99";
                    txtPurpose.Text = "C";

                    MakeReadOnly();

                    txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtInterest_From.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    if (hdnMode.Value == "Edit")
                    {
                        Fill_AcceptanceLCDetails747();
                        Fill_Logd_Details();
                        Fill_AcceptanceLCDetails();
                        Fill_AcceptanceDetails();
                        Check_LEINO_ExchRateDetails();
                        Check_cust_Leiverify();
                        Check_MT740();
                        Get_Acceptance_Get_Date_Diff(null, null);
                    }
                    else
                    {
                        //txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        //txtInterest_From.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        Fill_AcceptanceLCDetails747();
                        Fill_Logd_Details();
                        Fill_AcceptanceLCDetails();
                        Check_LEINO_ExchRateDetails();
                        Check_cust_Leiverify();
                        Check_MT740();
                        Get_Acceptance_Get_Date_Diff(null, null);
                    }


                }

                txtExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCreditAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtPercentageCreditAmountTolerance.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtPercentageCreditAmountTolerance1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtApplBR.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_INT_Rate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtBaseRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSpread.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterestAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtFundType.Attributes.Add("onkeydown", "return validate_Number(event);");
                //vinay swift changes
                txt_747_DateofogauthriztnRmburse.Attributes.Add("onblur", "return isValidDate(" + txt_747_DateofogauthriztnRmburse.ClientID + "," + "'[30] Date of the Original Authorisation to Reimburse'" + " );");
                txt_747_NewDateofExpiry.Attributes.Add("onblur", "return isValidDate(" + txt_747_NewDateofExpiry.ClientID + "," + "'[31E] New Date of Expiry'" + " );");
                txt_740_Date.Attributes.Add("onblur", "return isValidDate(" + txt_740_Date.ClientID + "," + "'[31D] Date and Place of Expiry'" + " );");

            }
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
            ToggleDocType(dt.Rows[0]["Document_Type"].ToString(), dt.Rows[0]["Document_FLC_ILC"].ToString());
            hdnDocScrutiny.Value = dt.Rows[0]["Document_Scrutiny"].ToString();
            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();  //Added by bhupen on 09112022
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

            if (dt.Rows[0]["Document_FLC_ILC"].ToString() == "ILC")
            {
                txtsettlCodeForCust.Text = "23";
                txtsettlforCust_Abbr.Text = dt.Rows[0]["Cust_Abbr_Acceptance"].ToString();
                txtsettlforCust_AccCode.Text = dt.Rows[0]["AC_Code"].ToString();
                txtsettlforCust_AccNo.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();
            }
            else if (dt.Rows[0]["Document_FLC_ILC"].ToString() == "FLC")
            {
                txtsettlCodeForCust.Text = "29";
                txtsettlforCust_Abbr.Text = "900";
                txtsettlforCust_AccCode.Text = "14017";
                txtsettlforCust_AccNo.Text = "";
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
                rdb_swift_999.Checked = true;
                rdb_swift_None.Checked = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('RMA has disable status for this Nego/Remit Bank SWIFT code.')", true);
            }

            if (dt.Rows[0]["Nego_Remit_Bank_Type"].ToString() == "LOCAL")
            {
                rdb_swift_740.Enabled = false;
                rdb_swift_999.Enabled = false;
            }

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

            txtCreditCurrency.Text = dt.Rows[0]["CreditCurrency"].ToString();
            txtCreditAmount.Text = dt.Rows[0]["CreditAmount"].ToString();
            txtPercentageCreditAmountTolerance.Text = dt.Rows[0]["PercentageCreditAmountTolerance"].ToString();
            txtPercentageCreditAmountTolerance1.Text = dt.Rows[0]["PercentageCreditAmountTolerance1"].ToString();
            txtMixedPaymentDetails.Text = dt.Rows[0]["MixedPaymentDetails"].ToString();
            txtMixedPaymentDetails2.Text = dt.Rows[0]["MixedPaymentDetails2"].ToString();
            txtMixedPaymentDetails3.Text = dt.Rows[0]["MixedPaymentDetails3"].ToString();
            txtMixedPaymentDetails4.Text = dt.Rows[0]["MixedPaymentDetails4"].ToString();
            txtDeferredPaymentDetails.Text = dt.Rows[0]["DeferredPaymentDetails"].ToString();
            txtDeferredPaymentDetails2.Text = dt.Rows[0]["DeferredPaymentDetails2"].ToString();
            txtDeferredPaymentDetails3.Text = dt.Rows[0]["DeferredPaymentDetails3"].ToString();
            txtDeferredPaymentDetails4.Text = dt.Rows[0]["DeferredPaymentDetails4"].ToString();

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
    protected void Get_Acceptance_Get_Date_Diff(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PInterest_From = new SqlParameter("@Interest_From", txtInterest_From.Text.ToString());
        SqlParameter PInterest_To = new SqlParameter("@Interest_To", txtInterest_To.Text.ToString());
        DataTable Date_dt = new DataTable();
        Date_dt = obj.getData("TF_IMP_Acceptance_Get_Date_Diff", PInterest_From, PInterest_To);
        if (Date_dt.Rows.Count > 0)
        {
            txt_No_Of_Days.Text = Date_dt.Rows[0]["NoOfDays"].ToString();
            txt_INT_Rate.Focus();
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
            //txtValueDate.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
            txtDraftAmt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txtContractNo.Text = dt.Rows[0]["Contract_No"].ToString();
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

            if (dt.Rows[0]["Commission_MATU"].ToString() == "1")
            {
                txt_CR_Accept_Commission_Payer.Enabled = true;
                txt_CR_Accept_Commission_amt.Enabled = true;
                txt_CR_Accept_Commission_Curr.Enabled = true;
                txt_CR_Others_amt.Enabled = true;
                txt_CR_Others_Payer.Enabled = true;
                txt_CR_Others_Curr.Enabled = true;
                txt_CR_Their_Commission_amt.Enabled = true;
                txt_CR_Their_Commission_Payer.Enabled = true;
                txt_CR_Their_Commission_Curr.Enabled = true;
            }

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

            //if (txt_740_documentaryCreditno.Text == "") 
            //{
            //txt_740_documentaryCreditno.Text = dt.Rows[0]["DocumentCreditNumber_740"].ToString();
            //}
            //if (txt_740_AccountIdentification.Text == "")
            //{
            //    txt_740_AccountIdentification.Text = dt.Rows[0]["AccountIdentification_740"].ToString();
            //}
            //if (txt_740_Applicablerules.Text == "")
            //{
            //    txt_740_Applicablerules.Text = dt.Rows[0]["ApplicableRules_740"].ToString();
            //}
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
                disableonstatus();
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
            //txtNegoPartyIdentifier.Text = dt.Rows[0]["NegoPartyIdentifier"].ToString();
            txtReceiverPartyIdentifier.Text = dt.Rows[0]["ReceiverPartyIdentifier"].ToString();
            txtSenderPartyIdentifierSFMS.Text = dt.Rows[0]["SenderPartyIdentifierSFMS"].ToString();
            txtReceiverPartyIdentifierSFMS.Text = dt.Rows[0]["ReceiverPartyIdentifierSFMS"].ToString();
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }

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
            //mt754


        }
    }
    protected void Fill_AcceptanceLCDetails747()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get747LCDetails_Acceptance", PDocNo);
        if (dt.Rows.Count > 0)
        {
            txt_747_documentaryCredtno.Text = dt.Rows[0]["txnfmb20"].ToString();
            txt_747_reimbursingbankRef.Text = dt.Rows[0]["txnfmb21"].ToString();
            txt_747_IncreaseofDocumentaryCreditCurr.Text = dt.Rows[0]["txnfmb32bccy"].ToString();
            txt_747_IncreaseofDocumentaryCreditAmt.Text = dt.Rows[0]["txnfmb32bamount"].ToString();
            txt_747_DecreaseofDocumentaryCreditCurr.Text = dt.Rows[0]["txnfmb33bccy"].ToString();
            txt_747_DecreaseofDocumentaryCreditAmt.Text = dt.Rows[0]["txnfmb33bamount"].ToString();
            txt_747_NewDocumentaryCreditAmtAfterAmendmentCurr.Text = dt.Rows[0]["txnfmb34bccy"].ToString();
            txt_747_NewDocumentaryCreditAmtAfterAmendment.Text = dt.Rows[0]["txnfmb34bamount"].ToString();
            txt_747_PercentageCreditAmtTolerance1.Text = dt.Rows[0]["txnfmb39a1"].ToString();
            txt_747_PercentageCreditAmtTolerance2.Text = dt.Rows[0]["txnfmb39a2"].ToString();
            Label1.Text = "LC Amendment : " + dt.Rows[0]["txnfmb26e"].ToString() + " LC Amendment time : " + dt.Rows[0]["txnmakerdate"].ToString();
        }
        else
        {
            Label1.Text = "No LC details for MT747";
        }

    }  // Added by bhupen on 12092022
    protected void Fill_AcceptanceLCDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Fill_AcceptanceLCDetails_740", PDocNo);
        if (dt.Rows.Count > 0)
       { 
            txt_740_documentaryCreditno.Text = dt.Rows[0]["DocumentCreditNumber"].ToString();
            txt_740_AccountIdentification.Text = dt.Rows[0]["AccountIdentification"].ToString();
            txt_740_Applicablerules.Text = dt.Rows[0]["ApplicableRules"].ToString();
            txt_740_Date.Text = dt.Rows[0]["DateandPlaceofExpiry"].ToString();
            txt_740_Placeofexpiry.Text = dt.Rows[0]["DateandPlaceofExpiryPlace"].ToString();
            txtCreditCurrency.Text = dt.Rows[0]["CreditAmtccy"].ToString();
            txtCreditAmount.Text = dt.Rows[0]["CreditAmtAmount"].ToString();
            txtPercentageCreditAmountTolerance.Text = dt.Rows[0]["PercentageCreditAmtTolerance1"].ToString();
            txtPercentageCreditAmountTolerance1.Text = dt.Rows[0]["PercentageCreditAmtTolerance2"].ToString();
            txt_Acceptance_Beneficiary.Text = dt.Rows[0]["Beneficiary_acc"].ToString();
            txt_Acceptance_Beneficiary2.Text = dt.Rows[0]["Beneficiary_add"].ToString();
            txt_Acceptance_Beneficiary3.Text = dt.Rows[0]["Beneficiary_add2"].ToString();
            txt_Acceptance_Beneficiary4.Text = dt.Rows[0]["Beneficiary_add3"].ToString();
            txt_Acceptance_Beneficiary5.Text = dt.Rows[0]["Beneficiary_add4"].ToString();
            txt_740_Draftsat1.Text = dt.Rows[0]["Draftsat1"].ToString();
            if (dt.Rows[0]["NegotiatingBankHeader"].ToString() != "")
            {
                txtNegoAccountNumber.Text = dt.Rows[0]["NegotiatingBankpartyIdentifier"].ToString();
                if (dt.Rows[0]["NegotiatingBankHeader"].ToString() == "A")
                {
                    ddlNegotiatingBankSwift.SelectedValue = "A";
                    ddlNegotiatingBankSwift_SelectedIndexChanged(null, null);
                    txtNegoSwiftCode.Text = dt.Rows[0]["NegotiatingBankCode"].ToString();
                }
                else
                {
                    ddlNegotiatingBankSwift.SelectedValue = "D";
                    ddlNegotiatingBankSwift_SelectedIndexChanged(null, null);
                    txtNegoName.Text = dt.Rows[0]["NegotiatingBankName"].ToString();
                    //txtNegoAddress1.Text = dt.Rows[0]["AvailableWithBy3"].ToString();
                    //txtNegoAddress2.Text = dt.Rows[0]["AvailableWithBy4"].ToString();
                    //txtNegoAddress3.Text = dt.Rows[0]["AvailableWithBy5"].ToString();
                }
            }
            else
            {
                ddlNegotiatingBankSwift.SelectedValue = "A";
                ddlNegotiatingBankSwift_SelectedIndexChanged(null, null);
            }
            if (dt.Rows[0]["AvailableWithByHeader"].ToString() != "")
            {
                txtAvailablewithbyCode.Text = dt.Rows[0]["AvailableWithByCode"].ToString();
                if (dt.Rows[0]["AvailableWithByHeader"].ToString() == "A")
                {
                    ddlAvailablewithby_740.SelectedValue = "A";
                    ddlAvailablewithby_740_TextChanged(null, null);
                    txtAvailablewithbySwiftCode.Text = dt.Rows[0]["AvailableWithByIdentifier"].ToString();
                }
                else
                {
                    ddlAvailablewithby_740.SelectedValue = "D";
                    ddlAvailablewithby_740_TextChanged(null, null);
                    txtAvailablewithbyName.Text = dt.Rows[0]["AvailableWithByName"].ToString();
                    //txtAvailablewithbyAddress1.Text = dt.Rows[0]["AvailableWithBy3"].ToString();
                    //txtAvailablewithbyAddress2.Text = dt.Rows[0]["AvailableWithBy4"].ToString();
                    //txtAvailablewithbyAddress3.Text = dt.Rows[0]["AvailableWithBy5"].ToString();
                }
            }
            else
            {
                ddlAvailablewithby_740.SelectedValue = "A";
                ddlAvailablewithby_740_TextChanged(null, null);
            }
            if (dt.Rows[0]["DraweeHeader"].ToString() != "")
            {
                txtDraweeAccountNumber.Text = dt.Rows[0]["DraweePartyIdentifier"].ToString();
                if (dt.Rows[0]["DraweeHeader"].ToString() == "A")
                {
                    ddlDrawee_740.SelectedValue = "A";
                    ddlDrawee_740_TextChanged(null, null);
                    txtDraweeSwiftCode.Text = dt.Rows[0]["DraweeIdentifiercode"].ToString();
                }
                else
                {
                    ddlDrawee_740.SelectedValue = "D";
                    ddlDrawee_740_TextChanged(null, null);
                    txtDraweeName.Text = dt.Rows[0]["DraweeName"].ToString();
                    //txtDraweeAddress1.Text = dt.Rows[0]["Drawee3"].ToString();
                    //txtDraweeAddress2.Text = dt.Rows[0]["Drawee4"].ToString();
                    //txtDraweeAddress3.Text = dt.Rows[0]["Drawee5"].ToString();
                }
            }
            else
            {
                ddlDrawee_740.SelectedValue = "A";
                ddlDrawee_740_TextChanged(null, null);
            }
        }
    }     //Added by bhupen for MT740 on 20092022
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
                    ddl_PurposeCode.Enabled = false;
                }
                lblForeign_Local.Text = Foreign_Local;
                lblCollection_Lodgment_UnderLC.Text = "Acceptance_Under_LC";
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
        txt_Doc_Curr.Enabled = false;
        //txtLogdmentDate.Enabled = false;
        txtCustomer_ID.Enabled = false;
        txtHO_Apl.Enabled = false;
        txtIBD_ACC_kind.Enabled = false;
        txtValueDate.Enabled = false;
        txtCommentCode.Enabled = false;
        txtAutoSettlement.Enabled = false;
        txtDraftAmt.Enabled = false;
        txtLCNo.Enabled = false;
        txtIBDAmt.Enabled = false;
        txtCountryRisk.Enabled = false;
        txtGradeCode.Enabled = false;
        txtPurpose.Enabled = false;
        //txtRiskCust.Enabled = false;
        //txtsettlCodeForCust.Enabled = false;
        //txtsettlforCust_Abbr.Enabled = false;
        //txtsettlforCust_AccCode.Enabled = false;
        //txtsettlforCust_AccNo.Enabled = false;
        //txtInterest_From.Enabled = false;
        //txtInterest_To.Enabled = false;
        txt_No_Of_Days.Enabled = false;
        txtSettl_CodeForBank.Enabled = false;
        //ddl_Settl_ForBank_Abbr.Enabled = false;
        //txtSettl_ForBank_AccCode.Enabled = false;
        //txtSettl_ForBank_AccNo.Enabled = false;
        txtNego_Remit_Bank_Type.Enabled = false;
        txtNego_Remit_Bank.Enabled = false;
        txtAcwithInstitution.Enabled = false;
        txtReimbursingbank.Enabled = false;

        txtDrawer.Enabled = false;
        ddlTenor.Enabled = false;
        txtTenor.Enabled = false;
        ddlTenor_Days_From.Enabled = false;
        txtTenor_Description.Enabled = false;

        txtDateArrival.Enabled = false;
        btncal_Arrival_date.Enabled = false;


        txt_Their_Ref_no.Enabled = false;
        //txtNego_Date.Enabled = false;
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

        //Import Accounting
        txtPrinc_lump.Enabled = false;
        txtPrinc_matu.Enabled = false;
        txtprinc_Contract_no.Enabled = false;
        txtprinc_Ex_rate.Enabled = false;
        txtprinc_Intnl_Ex_rate.Enabled = false;

        txtInterest_Contract_no.Enabled = false;
        txtInterest_Ex_rate.Enabled = false;
        txtInterest_Intnl_Ex_rate.Enabled = false;

        txtCommission_Contract_no.Enabled = false;
        txtCommission_Ex_rate.Enabled = false;
        txtCommission_Intnl_Ex_rate.Enabled = false;

        txtTheir_Commission_Contract_no.Enabled = false;
        txtTheir_Commission_Ex_rate.Enabled = false;
        txtTheir_Commission_Intnl_Ex_rate.Enabled = false;
        txtTheir_Commission_lump.Enabled = false;
        txtTheir_Commission_matu.Enabled = false;
        //-------------
        txt_DiscAmt.Enabled = false;
        txt_CR_Acceptance_amt.Enabled = false;

        txt_CR_Interest_amt.Enabled = false;
        txt_CR_Accept_Commission_amt.Enabled = false;
        //txt_CR_Others_amt.Enabled = false;
        txt_CR_Pay_Handle_Commission_amt.Enabled = false;
        //txt_CR_Their_Commission_amt.Enabled = false;

        txt_CR_Interest_payer.Enabled = false;
        txt_CR_Accept_Commission_Payer.Enabled = false;
        txt_CR_Pay_Handle_Commission_Payer.Enabled = false;
        //txt_CR_Others_Payer.Enabled = false;
        //txt_CR_Their_Commission_Payer.Enabled = false;

        txt_Princ_Ex_Curr.Enabled = false;
        txt_interest_Ex_Curr.Enabled = false;
        txt_Commission_Ex_Curr.Enabled = false;
        txt_Their_Commission_Ex_Curr.Enabled = false;
        txt_CR_Acceptance_Curr.Enabled = false;
        txt_CR_Interest_Curr.Enabled = false;
        txt_CR_Accept_Commission_Curr.Enabled = false;
        //txt_CR_Others_Curr.Enabled = false;
        txt_CR_Pay_Handle_Commission_Curr.Enabled = false;
        //txt_CR_Their_Commission_Curr.Enabled = false;
        txt_DR_Cur_Acc_Curr.Enabled = false;
        txt_DR_Cur_Acc_Curr2.Enabled = false;
        txt_DR_Cur_Acc_Curr3.Enabled = false;

        txt_DR_Cur_Acc_payer2.Enabled = false;
        txt_DR_Cur_Acc_payer3.Enabled = false;

        txt_DR_Cur_Acc_amt2.Enabled = false;
        txt_DR_Cur_Acc_amt3.Enabled = false;

        //txt_DR_Cust_Acc.Enabled = false;

        txtApplNo.Enabled = false;
        txtApplBR.Enabled = false;

        //General Operation
        // //GO_Swift_SFMS
        txt_GO_Swift_SFMS_Ref_No.Enabled = false;
        txt_GO_Swift_SFMS_SectionNo.Enabled = false;
        //txt_GO_Swift_SFMS_Remarks.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Debit_AdPrint.Enabled = false;
        //txt_GO_Swift_SFMS_Debit_Details.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Curr.Enabled = false;
        //txt_GO_Swift_SFMS_Credit_Details.Enabled = false;

        // //GO_LC_Commitement
        txt_GO_LC_Commitement_Ref_No.Enabled = false;
        txt_GO_LC_Commitement_SectionNo.Enabled = false;
        //txt_GO_LC_Commitement_Remarks.Enabled = false;
        txt_GO_LC_Commitement_Debit_Code.Enabled = false;
        txt_GO_LC_Commitement_Debit_Curr.Enabled = false;
        //txt_GO_LC_Commitement_Debit_Amt.Enabled = false;
        txt_GO_LC_Commitement_Debit_Advice_Print.Enabled = false;
        //txt_GO_LC_Commitement_Debit_Details.Enabled = false;
        txt_GO_LC_Commitement_Credit_Code.Enabled = false;
        txt_GO_LC_Commitement_Credit_Curr.Enabled = false;
        //txt_GO_LC_Commitement_Credit_Details.Enabled = false;

        rdb_SP_Instr_Other.Enabled = false;
        rdb_SP_Instr_Annexure.Enabled = false;
        rdb_SP_Instr_On_Sight.Enabled = false;
        rdb_SP_Instr_On_Date.Enabled = false;

        txt_SP_Instructions1.Enabled = false;
        txt_SP_Instructions2.Enabled = false;
        txt_SP_Instructions3.Enabled = false;
        txt_SP_Instructions4.Enabled = false;
        txt_SP_Instructions5.Enabled = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        /*
        obj.SaveDeleteData("TF_IMP_ACCEPTANCE_IBD_ACC_LOGDetails", P_DocNo);
        obj.SaveDeleteData("TF_IMP_ACCEPTANCE_IBD_ACC_AuditRecords", P_DocNo);
        */
        Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx", true);
    }
    protected void txtLedger_Modify_amt_TextChanged(object sender, EventArgs e)
    {
        Check_LEINO_ExchRateDetails();
        Check_cust_Leiverify();
    } //Added by bhupen for LEI on 21012023
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
    private void fillCustomerMasterDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtCustomer_ID.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString().Trim();  // Added by Bhupen for Lei changes 10112022
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

            txtsettlCodeForCust.Focus();
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
    //protected void fillNostroBank()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    ListItem li = new ListItem();

    //    SqlParameter pBranchName = new SqlParameter("@BranchName", hdnBranchName.Value.ToString());
    //    SqlParameter pCurr = new SqlParameter("@Curr", lblDoc_Curr.Text.ToString());

    //    switch (lblForeign_Local.Text)
    //    {
    //        case "Foreign":

    //            DataTable dt_Nostro = objData.getData("TF_IMP_Nostro_Bank_List_For_Acceptance", pBranchName, pCurr);
    //            ddl_Settl_ForBank_Abbr.Items.Clear();
    //            li.Value = "";
    //            if (dt_Nostro.Rows.Count > 0)
    //            {
    //                li.Text = "Select";
    //                ddl_Settl_ForBank_Abbr.DataSource = dt_Nostro.DefaultView;
    //                ddl_Settl_ForBank_Abbr.DataTextField = "CUST_ABBR";
    //                ddl_Settl_ForBank_Abbr.DataValueField = "CUST_ABBR";
    //                ddl_Settl_ForBank_Abbr.DataBind();
    //            }
    //            else
    //            {
    //                li.Text = "No record(s) found";
    //            }
    //            ddl_Settl_ForBank_Abbr.Items.Insert(0, li);

    //            if (hdnBranchName.Value != "Mumbai")
    //            {
    //                ddl_Settl_ForBank_Abbr.SelectedValue = "MHCB MB";
    //                ddl_Settl_ForBank_Abbr.Enabled = false;
    //            }
    //            else
    //            {
    //                ddl_Settl_ForBank_Abbr.Enabled = true;
    //            }
    //            break;

    //        case "Local":

    //            //hdnBranchName.Value  //lblDoc_Curr.Text  //hdnDocScrutiny.Value
    //            ddl_Settl_ForBank_Abbr.Items.Clear();
    //            if (hdnDocScrutiny.Value == "1") // Clean Local
    //            {
    //                if (hdnBranchName.Value == "Mumbai")
    //                {
    //                    ddl_Settl_ForBank_Abbr.Items.Insert(0,"RBI");
    //                    //ddl_Settl_ForBank_Abbr.SelectedItem.Value = "RBI";
    //                    //ddl_Settl_ForBank_Abbr.SelectedItem.Text = "RBI";
    //                }
    //                if (hdnBranchName.Value != "Mumbai")
    //                {
    //                    ddl_Settl_ForBank_Abbr.Items.Insert(0, "MHCB MB");
    //                    //ddl_Settl_ForBank_Abbr.SelectedItem.Value = "MHCB MB";
    //                    //ddl_Settl_ForBank_Abbr.SelectedItem.Text = "MHCB MB";
    //                }
    //            }

    //            if (hdnDocScrutiny.Value == "2") // Discrepant Local
    //            {
    //                ddl_Settl_ForBank_Abbr.Items.Insert(0, "900");
    //                //ddl_Settl_ForBank_Abbr.SelectedItem.Value = "900";
    //                //ddl_Settl_ForBank_Abbr.SelectedItem.Text = "900";
    //            }

    //            break;

    //    }
    //}
    //protected void fillNostroBankDetails()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter pCUST_ABBR = new SqlParameter("@CUST_ABBR", ddl_Settl_ForBank_Abbr.SelectedValue.ToString());
    //    DataTable dt = objData.getData("TF_IMP_Nostro_Bank_Details_For_Acceptance", pCUST_ABBR);
    //    switch (lblForeign_Local.Text)
    //    {
    //        case "Foreign":

    //            if (dt.Rows.Count > 0)
    //            {
    //                txtSettl_ForBank_AccCode.Text = dt.Rows[0]["GL_CODE"].ToString();
    //                txtSettl_ForBank_AccNo.Text = dt.Rows[0]["AC_No"].ToString();
    //                txtREM_EUC.Focus();
    //            }
    //            else
    //            {
    //                txtSettl_ForBank_AccCode.Text = "";
    //                txtSettl_ForBank_AccNo.Text = "";
    //                ddl_Settl_ForBank_Abbr.Focus();
    //            }
    //            break;

    //        case "Local":

    //            if (hdnDocScrutiny.Value == "1") // Clean Local
    //            {
    //                if (hdnBranchName.Value == "Mumbai")
    //                {
    //                    txtSettl_ForBank_AccCode.Text = "01991";
    //                    txtSettl_ForBank_AccNo.Text = "H60-000028";
    //                    txtREM_EUC.Focus();
    //                }
    //                if (hdnBranchName.Value != "Mumbai")
    //                {
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        txtSettl_ForBank_AccCode.Text = dt.Rows[0]["GL_CODE"].ToString();
    //                        txtSettl_ForBank_AccNo.Text = dt.Rows[0]["AC_No"].ToString();
    //                        txtREM_EUC.Focus();
    //                    }
    //                    else
    //                    {
    //                        txtSettl_ForBank_AccCode.Text = "";
    //                        txtSettl_ForBank_AccNo.Text = "";
    //                    }
    //                }

    //            }

    //            if (hdnDocScrutiny.Value == "2") // Discrepant Local
    //            {
    //                SqlParameter pBranchName = new SqlParameter("@BranchName", hdnBranchName.Value.ToString());
    //                SqlParameter pCUSTABB = new SqlParameter("@CUSTABB", ddl_Settl_ForBank_Abbr.SelectedValue.ToString());
    //                DataTable dt_Sundry = objData.getData("TF_IMP_Get_Sundry_Acc_For_Sett_For_Bank", pBranchName, pCUSTABB);
    //                if (dt_Sundry.Rows.Count > 0)
    //                {
    //                    txtSettl_ForBank_AccCode.Text = dt_Sundry.Rows[0]["ACCODE"].ToString();
    //                    txtSettl_ForBank_AccNo.Text = dt_Sundry.Rows[0]["ACCOUNTNO"].ToString();
    //                }
    //                else
    //                {
    //                    txtSettl_ForBank_AccCode.Text = "";
    //                    txtSettl_ForBank_AccNo.Text = "";
    //                }
    //            }
    //            break;
    //    }

    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_SubmitAcceptaneForChecker", P_DocNo);
        if (Result == "Submit")
        {
            Lei_Audit();
            string _script = "";
            _script = "window.location='TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
    }
    //protected void ddl_Settl_ForBank_Abbr_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillNostroBankDetails();
    //}
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
    [WebMethod]
    public static string AddUpdateAceptance(string hdnUserName, string _BranchName, string _txtDocNo, string _Document_Curr, string _txtHO_Apl, string _txtIBD_ACC_kind, string _txtValueDate,
        string _txtCommentCode, string _txtAutoSettlement,
        string _txtDraftAmt, string _txtContractNo, string _txtExchRate,
        string _txtCountryRisk, string _txtRiskCust,
        string _txtGradeCode, string _txtApplNo, string _txtApplBR, string _txtPurpose, string _ddl_PurposeCode,
        string _txtsettlCodeForCust, string _txtsettlforCust_Abbr, string _txtsettlforCust_AccCode, string _txtsettlforCust_AccNo,
        string _txtInterest_From, string _txtInterest_To, string _txt_No_Of_Days,
        string _txt_INT_Rate,

        string _txtBaseRate, string _txtSpread, string _txtInterestAmt, string _txtFundType,
        string _txtInternalRate, string _txtSettl_CodeForBank, string _ddl_Settl_ForBank_Abbr,
        string _txtSettl_ForBank_AccCode, string _txtSettl_ForBank_AccNo,
        string _txtAttn, string _txtREM_EUC,

        ////Instruction
        string _txt_INST_Code,

        ////  Import Accounting
        string _chk_IMP_ACC_Commission,
        string _txt_IMP_ACC_ExchRate,
        string _txtPrinc_matu, string _txtInterest_matu, string _txtCommission_matu, string _txtTheir_Commission_matu,
        string _txtPrinc_lump, string _txtInterest_lump, string _txtCommission_lump, string _txtTheir_Commission_lump,
        string _txtprinc_Contract_no, string _txtInterest_Contract_no, string _txtCommission_Contract_no, string _txtTheir_Commission_Contract_no,
        string _txtprinc_Ex_rate, string _txtInterest_Ex_rate, string _txtCommission_Ex_rate, string _txtTheir_Commission_Ex_rate,
        string _txtprinc_Intnl_Ex_rate, string _txtInterest_Intnl_Ex_rate, string _txtCommission_Intnl_Ex_rate, string _txtTheir_Commission_Intnl_Ex_rate,
        string _txt_CR_Code, string _txt_CR_Cust_abbr, string _txt_CR_Cust_Acc,
        string _txt_CR_Acceptance_amt, string _txt_CR_Interest_amt, string _txt_CR_Accept_Commission_amt,
        string _txt_CR_Pay_Handle_Commission_amt, string _txt_CR_Others_amt, string _txt_CR_Their_Commission_amt,
        string _txt_CR_Acceptance_payer, string _txt_CR_Interest_payer, string _txt_CR_Accept_Commission_Payer,
        string _txt_CR_Pay_Handle_Commission_Payer, string _txt_CR_Others_Payer, string _txt_CR_Their_Commission_Payer,
        string _txt_DR_Code, string _txt_DR_Cust_abbr, string _txt_DR_Cust_Acc,
        string _txt_DR_Cur_Acc_amt, string _txt_DR_Cur_Acc_amt2, string _txt_DR_Cur_Acc_amt3,
        string _txt_DR_Cur_Acc_payer, string _txt_DR_Cur_Acc_payer2, string _txt_DR_Cur_Acc_payer3,

        string _txt_Princ_Ex_Curr, string _txt_interest_Ex_Curr, string _txt_Commission_Ex_Curr, string _txt_Their_Commission_Ex_Curr,
        string _txt_CR_Acceptance_Curr, string _txt_CR_Interest_Curr, string _txt_CR_Accept_Commission_Curr,
        string _txt_CR_Pay_Handle_Commission_Curr, string _txt_CR_Others_Curr, string _txt_CR_Their_Commission_Curr,
        string _txt_DR_Cur_Acc_Curr, string _txt_DR_Cur_Acc_Curr2, string _txt_DR_Cur_Acc_Curr3,

        ////  General_Operations Swift_SFMS_CHRG
        string _chkGO_Swift_SFMS, string _txt_GO_Swift_SFMS_Comment, string _txt_GO_Swift_SFMS_Ref_No,
        string _txt_GO_Swift_SFMS_SectionNo, string _txt_GO_Swift_SFMS_Remarks, string _txt_GO_Swift_SFMS_Memo,

        string _txt_GO_Swift_SFMS_Debit_Code, string _txt_GO_Swift_SFMS_Debit_Curr, string _txt_GO_Swift_SFMS_Debit_Amt,
        string _txt_GO_Swift_SFMS_Debit_Cust, string _txt_GO_Swift_SFMS_Debit_Cust_AcCode, string _txt_GO_Swift_SFMS_Debit_Cust_AccNo,
        string _txt_GO_Swift_SFMS_Debit_Exch_Curr, string _txt_GO_Swift_SFMS_Debit_Exch_Rate,
        string _txt_GO_Swift_SFMS_Debit_AdPrint, string _txt_GO_Swift_SFMS_Debit_Details, string _txt_GO_Swift_SFMS_Debit_Entity,

        string _txt_GO_Swift_SFMS_Credit_Code, string _txt_GO_Swift_SFMS_Credit_Curr, string _txt_GO_Swift_SFMS_Credit_Amt,
        string _txt_GO_Swift_SFMS_Credit_Cust, string _txt_GO_Swift_SFMS_Credit_Cust_AcCode, string _txt_GO_Swift_SFMS_Credit_Cust_AccNo,
        string _txt_GO_Swift_SFMS_Credit_Exch_Curr, string _txt_GO_Swift_SFMS_Credit_Exch_Rate,
        string _txt_GO_Swift_SFMS_Credit_AdPrint, string _txt_GO_Swift_SFMS_Credit_Details, string _txt_GO_Swift_SFMS_Credit_Entity,

        string _txt_GO_Swift_SFMS_Scheme_no,
        string _txt_GO_Swift_SFMS_Debit_FUND, string _txt_GO_Swift_SFMS_Debit_Check_No, string _txt_GO_Swift_SFMS_Debit_Available,
        string _txt_GO_Swift_SFMS_Debit_Division, string _txt_GO_Swift_SFMS_Debit_Inter_Amount, string _txt_GO_Swift_SFMS_Debit_Inter_Rate,
        string _txt_GO_Swift_SFMS_Credit_FUND, string _txt_GO_Swift_SFMS_Credit_Check_No, string _txt_GO_Swift_SFMS_Credit_Available,
        string _txt_GO_Swift_SFMS_Credit_Division, string _txt_GO_Swift_SFMS_Credit_Inter_Amount, string _txt_GO_Swift_SFMS_Credit_Inter_Rate,

        ////  General_Operations LC_Commitment_CHRG
        string _chkGO_LC_Commitement, string _txt_GO_LC_Commitement_Comment, string _txt_GO_LC_Commitement_Ref_No,
        string _txt_GO_LC_Commitement_SectionNo, string _txt_GO_LC_Commitement_Remarks, string _txt_GO_LC_Commitement_MEMO,

        string _txt_GO_LC_Commitement_Debit_Code, string _txt_GO_LC_Commitement_Debit_Curr, string _txt_GO_LC_Commitement_Debit_Amt,
        string _txt_GO_LC_Commitement_Debit_Cust, string _txt_GO_LC_Commitement_Debit_Cust_AcCode, string _txt_GO_LC_Commitement_Debit_Cust_AccNo,
        string _txt_GO_LC_Commitement_Debit_Exch_Curr, string _txt_GO_LC_Commitement_Debit_Exch_Rate,
        string _txt_GO_LC_Commitement_Debit_Advice_Print, string _txt_GO_LC_Commitement_Debit_Details, string _txt_GO_LC_Commitement_Debit_Entity,

        string _txt_GO_LC_Commitement_Credit_Code, string _txt_GO_LC_Commitement_Credit_Curr, string _txt_GO_LC_Commitement_Credit_Amt,
        string _txt_GO_LC_Commitement_Credit_Cust, string _txt_GO_LC_Commitement_Credit_Cust_AcCode, string _txt_GO_LC_Commitement_Credit_Cust_AccNo,
        string _txt_GO_LC_Commitement_Credit_Exch_Curr, string _txt_GO_LC_Commitement_Credit_Exch_Rate,
        string _txt_GO_LC_Commitement_Credit_Advice_Print, string _txt_GO_LC_Commitement_Credit_Details, string _txt_GO_LC_Commitement_Credit_Entity,

        string _txt_GO_LC_Commitement_Scheme_no,
        string _txt_GO_LC_Commitement_Debit_FUND, string _txt_GO_LC_Commitement_Debit_Check_No, string _txt_GO_LC_Commitement_Debit_Available,
        string _txt_GO_LC_Commitement_Debit_Division, string _txt_GO_LC_Commitement_Debit_Inter_Amount, string _txt_GO_LC_Commitement_Debit_Inter_Rate,
        string _txt_GO_LC_Commitement_Credit_FUND, string _txt_GO_LC_Commitement_Credit_Check_No, string _txt_GO_LC_Commitement_Credit_Available,
        string _txt_GO_LC_Commitement_Credit_Division, string _txt_GO_LC_Commitement_Credit_Inter_Amount, string _txt_GO_LC_Commitement_Credit_Inter_Rate,

        //// Swift Files////////////////////////////////////////////////////////////////////////////////////
        string None_Flag, string MT740_Flag, string MT756_Flag, string MT999_Flag, string MT799_Flag,

        string _txt_Narrative1, string _txt_Narrative2, string _txt_Narrative3, string _txt_Narrative4, string _txt_Narrative5,
        string _txt_Narrative6, string _txt_Narrative7, string _txt_Narrative8, string _txt_Narrative9, string _txt_Narrative10,
        string _txt_Narrative11, string _txt_Narrative12, string _txt_Narrative13, string _txt_Narrative14, string _txt_Narrative15,
        string _txt_Narrative16, string _txt_Narrative17, string _txt_Narrative18, string _txt_Narrative19, string _txt_Narrative20,
        string _txt_Narrative21, string _txt_Narrative22, string _txt_Narrative23, string _txt_Narrative24, string _txt_Narrative25,
        string _txt_Narrative26, string _txt_Narrative27, string _txt_Narrative28, string _txt_Narrative29, string _txt_Narrative30,
        string _txt_Narrative31, string _txt_Narrative32, string _txt_Narrative33, string _txt_Narrative34, string _txt_Narrative35,
        string _ddlNegotiatingBankSwift, string _txtNegoAccountNumber, string _txtNegoSwiftCode, string _txtNegoName, string _txtNegoAddress1, string _txtNegoAddress2, string _txtNegoAddress3,
        //Added by bhupen as new fields for MT740 on 23082022
        string _txt_740_documentaryCreditno, string _txt_740_AccountIdentification, string _txt_740_Applicablerules, string _txt_740_Date, string _txt_740_Placeofexpiry, string _txt_740_Draftsat1,
        string _txt_740_Draftsat2, string _txt_740_Draftsat3, string _ddlAvailablewithby_740, string _txtAvailablewithbyCode, string _txtAvailablewithbySwiftCode, string _txtAvailablewithbyName,
        string _txtAvailablewithbyAddress1, string _txtAvailablewithbyAddress2, string _txtAvailablewithbyAddress3, string _ddlDrawee_740, string _txtDraweeAccountNumber,
        string _txtDraweeSwiftCode, string _txtDraweeName, string _txtDraweeAddress1, string _txtDraweeAddress2, string _txtDraweeAddress3, string _txt_Acceptance_Beneficiary5,
        //End
        string _txt_Acceptance_Beneficiary, string _txt_Acceptance_Beneficiary2, string _txt_Acceptance_Beneficiary3, string _txt_Acceptance_Beneficiary4
        , string _txtCreditCurrency, string _txtCreditAmount, string _txtPercentageCreditAmountTolerance, string _txtPercentageCreditAmountTolerance1, string _txt_Acceptance_Max_Credit_Amt, string _txt_Acceptance_Additional_Amt_Covered
        , string _txt_Acceptance_Additional_Amt_Covered2, string _txt_Acceptance_Additional_Amt_Covered3, string _txt_Acceptance_Additional_Amt_Covered4, string _txtMixedPaymentDetails
        , string _txtMixedPaymentDetails2, string _txtMixedPaymentDetails3, string _txtMixedPaymentDetails4, string _txtDeferredPaymentDetails, string _txtDeferredPaymentDetails2
        , string _txtDeferredPaymentDetails3, string _txtDeferredPaymentDetails4, string _txt_Acceptance_Reimbur_Bank_Charges, string _txt_Acceptance_Other_Charges
        , string _txt_Acceptance_Other_Charges2, string _txt_Acceptance_Other_Charges3, string _txt_Acceptance_Other_Charges4, string _txt_Acceptance_Other_Charges5
        , string _txt_Acceptance_Other_Charges6, string _txt_Acceptance_Sender_to_Receiver_Information, string _txt_Acceptance_Sender_to_Receiver_Information2
        , string _txt_Acceptance_Sender_to_Receiver_Information3, string _txt_Acceptance_Sender_to_Receiver_Information4, string _txt_Acceptance_Sender_to_Receiver_Information5
        , string _txt_Acceptance_Sender_to_Receiver_Information6,
        string _ddlReceiverCorrespondentMT, string _txtReceiverAccountNumberMT, string _txtReceiverSwiftCodeMT, string _txtReceiverNameMT,
        string _txtReceiverLocationMT, string _txtReceiverAddress1MT, string _txtReceiverAddress2MT, string _txtReceiverAddress3MT,
        string _txt_Narrative_756_1, string _txt_Narrative_756_2, string _txt_Narrative_756_3, string _txt_Narrative_756_4, string _txt_Narrative_756_5,
        string _txt_Narrative_756_6, string _txt_Narrative_756_7, string _txt_Narrative_756_8, string _txt_Narrative_756_9, string _txt_Narrative_756_10,
        string _txt_Narrative_756_11, string _txt_Narrative_756_12, string _txt_Narrative_756_13, string _txt_Narrative_756_14, string _txt_Narrative_756_15,
        string _txt_Narrative_756_16, string _txt_Narrative_756_17, string _txt_Narrative_756_18, string _txt_Narrative_756_19, string _txt_Narrative_756_20,
        string _txt_Narrative_756_21, string _txt_Narrative_756_22, string _txt_Narrative_756_23, string _txt_Narrative_756_24, string _txt_Narrative_756_25,
        string _txt_Narrative_756_26, string _txt_Narrative_756_27, string _txt_Narrative_756_28, string _txt_Narrative_756_29, string _txt_Narrative_756_30,
        string _txt_Narrative_756_31, string _txt_Narrative_756_32, string _txt_Narrative_756_33, string _txt_Narrative_756_34, string _txt_Narrative_756_35,
        string _txt_Discrepancy_Charges_Swift,
        string _ddlReceiverCorrespondentSFMS, string _txtReceiverAccountNumberSFMS, string _txtReceiverSwiftCodeSFMS, string _txtReceiverNameSFMS, string _txtReceiverLocationSFMS,
        string _txtReceiverAddress1SFMS, string _txtReceiverAddress2SFMS, string _txtReceiverAddress3SFMS, string _ddlSenderCorrespondentSFMS, string _txtSenderAccountNumberSFMS,
        string _txtSenderSwiftCodeSFMS, string _txtSenderNameSFMS, string _txtSenderLocationSFMS, string _txtSenderAddress1SFMS, string _txtSenderAddress2SFMS, string _txtSenderAddress3SFMS,
        string _txt_Discrepancy_Charges_SFMS,

        ////LEDGER MODIFICATION
        string _chk_Ledger_Modify,
        string _txtLedgerRemark,
        string _txtLedgerCustName, string _txtLedgerAccode, string _txtLedgerCURR,
        string _txtLedgerRefNo, string _txtLedger_Modify_amt, string _txtLedgerOperationDate, string _txtLedgerBalanceAmt,
        string _txtLedgerAcceptDate, string _txtLedgerMaturity, string _txtLedgerSettlememtDate, string _txtLedgerSettlValue,
        string _txtLedgerLastModDate, string _txtLedgerREM_EUC, string _txtLedgerLastOPEDate, string _txtLedgerTransNo,
        string _txtLedgerContraCountry, string _txtLedgerStatusCode, string _txtLedgerCollectOfComm, string _txtLedgerCommodity,
        string _txtLedgerhandlingCommRate, string _txtLedgerhandlingCommCurr, string _txtLedgerhandlingCommAmt, string _txtLedgerhandlingCommPayer,
        string _txtLedgerPostageRate, string _txtLedgerPostageCurr, string _txtLedgerPostageAmt, string _txtLedgerPostagePayer,
        string _txtLedgerOthersRate, string _txtLedgerOthersCurr, string _txtLedgerOthersAmt, string _txtLedgerOthersPayer,
        string _txtLedgerTheirCommRate, string _txtLedgerTheirCommCurr, string _txtLedgerTheirCommAmt, string _txtLedgerTheirCommPayer,
        string _txtLedgerNegoBank,
        string _txtLedgerReimbursingBank,
        string _txtLedgerDrawer, string _txtLedgerTenor, string _txtLedgerAttn, string _txtNegoPartyIdentifier, string _txtReceiverPartyIdentifier,
        string _txtSenderPartyIdentifierSFMS, string _txtReceiverPartyIdentifierSFMS,

        ////////////////////////////////////////////////////////// Bhupen //////////////////////////////////////////////////////////////////////////////////////

        string _txtdoccreditNo_747, string _txtReimbursingbanRef_747,string MT747_Flag,
        string _txtDateoforiginalAutoOfReimburse_747, string _txtNewdateofExpiry_747,
        string _txtIncreaseofDocumentryCreditAmt_Curr_747, string _txtIncreaseofDocumentryCreditAmt_747,
        string _txtdecreaseofDocumentryCreditAmt_Curr_747, string _txtdecreaseofDocumentryCreditAmt_747,
        string _NewDocumentryCreditAmtAfterAmendment_Curr_747, string _NewDocumentryCreditAmtAfterAmendment_747,
        string _txtPercentageCreditAmtTolerance_747_1, string _txtPercentageCreditAmtTolerance_747_2,
        string _txtAddAmtCovered_747_1, string _txtAddAmtCovered_747_2, string _txtAddAmtCovered_747_3,
        string _txtAddAmtCovered_747_4,
        string _txtSenToRecInfo_747_1, string _txtSenToRecInfo_747_2, string _txtSenToRecInfo_747_3,
        string _txtSenToRecInfo_747_4, string _txtSenToRecInfo_747_5, string _txtSenToRecInfo_747_6,

        string _txt_Narrative_747_1, string _txt_Narrative_747_2, string _txt_Narrative_747_3, string _txt_Narrative_747_4, string _txt_Narrative_747_5,
        string _txt_Narrative_747_6, string _txt_Narrative_747_7, string _txt_Narrative_747_8, string _txt_Narrative_747_9, string _txt_Narrative_747_10,
        string _txt_Narrative_747_11, string _txt_Narrative_747_12, string _txt_Narrative_747_13, string _txt_Narrative_747_14, string _txt_Narrative_747_15,
        string _txt_Narrative_747_16, string _txt_Narrative_747_17, string _txt_Narrative_747_18, string _txt_Narrative_747_19, string _txt_Narrative_747_20

        ///////////////////////////////////////////////// 747////////////////////////////////////////////////////////////////////////////////////////////////////////////
        )
    {
        TF_DATA obj = new TF_DATA();

        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName.ToUpper());
        SqlParameter P_txtDocNo = new SqlParameter("@Document_No", _txtDocNo.ToUpper());
        SqlParameter P_Document_Curr = new SqlParameter("@Document_Curr", _Document_Curr.ToUpper());
        SqlParameter P_txtHO_Apl = new SqlParameter("@HO_Appl", _txtHO_Apl.ToUpper());
        SqlParameter P_txtIBD_ACC_kind = new SqlParameter("@IBD_ACC_Kind", _txtIBD_ACC_kind.ToUpper());
        SqlParameter P_txtValueDate = new SqlParameter("@Acceptance_Value_Date", _txtValueDate.ToUpper());
        SqlParameter P_txtCommentCode = new SqlParameter("@Comment_Code", _txtCommentCode.ToUpper());
        SqlParameter P_txtAutoSettlement = new SqlParameter("@Auto_Settl", _txtAutoSettlement.ToUpper());

        SqlParameter P_txtDraftAmt = new SqlParameter("@Draft_Amt", _txtDraftAmt.ToUpper());
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
        SqlParameter P_ddl_Settl_ForBank_Abbr = new SqlParameter("@Settl_For_Bank_Abbr", _ddl_Settl_ForBank_Abbr.ToUpper());
        SqlParameter P_txtSettl_ForBank_AccCode = new SqlParameter("@Settl_ForBank_AccCode", _txtSettl_ForBank_AccCode.ToUpper());
        SqlParameter P_txtSettl_ForBank_AccNo = new SqlParameter("@Settl_ForBank_AccNo", _txtSettl_ForBank_AccNo.ToUpper());
        SqlParameter P_txtAttn = new SqlParameter("@Attn", _txtAttn.ToUpper());
        SqlParameter P_txtREM_EUC = new SqlParameter("@REM_EUC", _txtREM_EUC.ToUpper());

        //Instruction
        SqlParameter P_txt_INST_Code = new SqlParameter("@INST_Code", _txt_INST_Code.ToUpper());

        //Import accounting
        SqlParameter P_chk_IMP_ACC_Commission = new SqlParameter("@IMP_ACC_Commission_Flag", _chk_IMP_ACC_Commission.ToUpper());
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
        SqlParameter P_txt_CR_Cust_abbr = new SqlParameter("@CR_Cust_Abbr", _txt_CR_Cust_abbr.ToUpper());
        SqlParameter P_txt_CR_Cust_Acc = new SqlParameter("@CR_Cust_Acc_No", _txt_CR_Cust_Acc.ToUpper());
        SqlParameter P_txt_CR_Acceptance_amt = new SqlParameter("@CR_Acceptance_Amount", _txt_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_CR_Interest_amt = new SqlParameter("@CR_Interest_Amount", _txt_CR_Interest_amt.ToUpper());

        SqlParameter P_txt_CR_Accept_Commission_amt = new SqlParameter("@CR_Acceptance_Comm_Amount", _txt_CR_Accept_Commission_amt.ToUpper());

        SqlParameter P_txt_CR_Pay_Handle_Commission_amt = new SqlParameter("@CR_Pay_Handle_Comm_Amount", _txt_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_CR_Others_amt = new SqlParameter("@CR_Others_Amount", _txt_CR_Others_amt.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_amt = new SqlParameter("@CR_Their_Comm_Amount", _txt_CR_Their_Commission_amt.ToUpper());

        SqlParameter P_txt_DR_Code = new SqlParameter("@DR_Code", _txt_DR_Code.ToUpper());
        SqlParameter P_txt_DR_Cust_abbr = new SqlParameter("@DR_Cust_Abbr", _txt_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_DR_Cust_Acc = new SqlParameter("@DR_Cust_Acc_No", _txt_DR_Cust_Acc.ToUpper());

        SqlParameter P_txt_DR_Cur_Acc_amt = new SqlParameter("@DR_Current_Acc_Amount", _txt_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_amt2 = new SqlParameter("@DR_Current_Acc_Amount2", _txt_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_amt3 = new SqlParameter("@DR_Current_Acc_Amount3", _txt_DR_Cur_Acc_amt3.ToUpper());

        SqlParameter P_txt_CR_Acceptance_payer = new SqlParameter("@CR_Acceptance_Payer", _txt_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_CR_Interest_payer = new SqlParameter("@CR_Interest_Payer", _txt_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_CR_Accept_Commission_Payer = new SqlParameter("@CR_Acceptance_Comm_Payer", _txt_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_CR_Pay_Handle_Commission_Payer = new SqlParameter("@CR_Pay_Handle_Comm_Payer", _txt_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_CR_Others_Payer = new SqlParameter("@CR_Others_Payer", _txt_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_Payer = new SqlParameter("@CR_Their_Comm_Payer", _txt_CR_Their_Commission_Payer.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_payer = new SqlParameter("@DR_Current_Acc_Payer", _txt_DR_Cur_Acc_payer.ToUpper());
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
        SqlParameter P_txt_DR_Cur_Acc_Curr = new SqlParameter("@DR_Current_Acc_Curr", _txt_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_Curr2 = new SqlParameter("@DR_Current_Acc_Curr2", _txt_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_Curr3 = new SqlParameter("@DR_Current_Acc_Curr3", _txt_DR_Cur_Acc_Curr3.ToUpper());


        //// General Operation SWIFT_SFMS
        SqlParameter P_GO_SWIFT_SFMS_Flag = new SqlParameter("@GO_SWIFT_SFMS_Flag", _chkGO_Swift_SFMS.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Ref_No = new SqlParameter("@GO_SWIFT_SFMS_Ref_No", _txt_GO_Swift_SFMS_Ref_No.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Remark = new SqlParameter("@GO_SWIFT_SFMS_Remark", _txt_GO_Swift_SFMS_Remarks.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Section = new SqlParameter("@GO_SWIFT_SFMS_Section", _txt_GO_Swift_SFMS_SectionNo.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Comment = new SqlParameter("@GO_SWIFT_SFMS_Comment", _txt_GO_Swift_SFMS_Comment.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Memo = new SqlParameter("@GO_SWIFT_SFMS_Memo", _txt_GO_Swift_SFMS_Memo.ToUpper());

        SqlParameter P_GO_SWIFT_SFMS_Debit = new SqlParameter("@GO_SWIFT_SFMS_Debit", _txt_GO_Swift_SFMS_Debit_Code.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Debit_Amt = new SqlParameter("@GO_SWIFT_SFMS_Debit_Amt", _txt_GO_Swift_SFMS_Debit_Amt.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Debit_CCY = new SqlParameter("@GO_SWIFT_SFMS_Debit_CCY", _txt_GO_Swift_SFMS_Debit_Curr.ToUpper());

        SqlParameter P_GO_SWIFT_SFMS_Debit_Cust = new SqlParameter("@GO_SWIFT_SFMS_Debit_Cust", _txt_GO_Swift_SFMS_Debit_Cust.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Debit_Cust_AcCode = new SqlParameter("@GO_SWIFT_SFMS_Debit_Cust_AcCode", _txt_GO_Swift_SFMS_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Debit_Cust_AccNo = new SqlParameter("@GO_SWIFT_SFMS_Debit_Cust_AccNo", _txt_GO_Swift_SFMS_Debit_Cust_AccNo.ToUpper());

        SqlParameter P_GO_SWIFT_SFMS_Debit_ExchRate = new SqlParameter("@GO_SWIFT_SFMS_Debit_ExchRate", _txt_GO_Swift_SFMS_Debit_Exch_Rate.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Debit_ExchCCY = new SqlParameter("@GO_SWIFT_SFMS_Debit_ExchCCY", _txt_GO_Swift_SFMS_Debit_Exch_Curr.ToUpper());

        SqlParameter P_GO_SWIFT_SFMS_Debit_Advice_Print = new SqlParameter("@GO_SWIFT_SFMS_Debit_Advice_Print", _txt_GO_Swift_SFMS_Debit_AdPrint.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Debit_Details = new SqlParameter("@GO_SWIFT_SFMS_Debit_Details", _txt_GO_Swift_SFMS_Debit_Details.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Debit_Entity = new SqlParameter("@GO_SWIFT_SFMS_Debit_Entity", _txt_GO_Swift_SFMS_Debit_Entity.ToUpper());

        SqlParameter P_GO_SWIFT_SFMS_Credit = new SqlParameter("@GO_SWIFT_SFMS_Credit", _txt_GO_Swift_SFMS_Credit_Code.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_Amt = new SqlParameter("@GO_SWIFT_SFMS_Credit_Amt", _txt_GO_Swift_SFMS_Credit_Amt.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_CCY = new SqlParameter("@GO_SWIFT_SFMS_Credit_CCY", _txt_GO_Swift_SFMS_Credit_Curr.ToUpper());

        SqlParameter P_GO_SWIFT_SFMS_Credit_Cust = new SqlParameter("@GO_SWIFT_SFMS_Credit_Cust", _txt_GO_Swift_SFMS_Credit_Cust.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_Cust_AccNo = new SqlParameter("@GO_SWIFT_SFMS_Credit_Cust_AccNo", _txt_GO_Swift_SFMS_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_Cust_AcCode = new SqlParameter("@GO_SWIFT_SFMS_Credit_Cust_AcCode", _txt_GO_Swift_SFMS_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_ExchCCY = new SqlParameter("@GO_SWIFT_SFMS_Credit_ExchCCY", _txt_GO_Swift_SFMS_Credit_Exch_Curr.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_ExchRate = new SqlParameter("@GO_SWIFT_SFMS_Credit_ExchRate", _txt_GO_Swift_SFMS_Credit_Exch_Rate.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_Advice_Print = new SqlParameter("@GO_SWIFT_SFMS_Credit_Advice_Print", _txt_GO_Swift_SFMS_Credit_AdPrint.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_Details = new SqlParameter("@GO_SWIFT_SFMS_Credit_Details", _txt_GO_Swift_SFMS_Credit_Details.ToUpper());
        SqlParameter P_GO_SWIFT_SFMS_Credit_Entity = new SqlParameter("@GO_SWIFT_SFMS_Credit_Entity", _txt_GO_Swift_SFMS_Credit_Entity.ToUpper());


        SqlParameter P_txt_GO_Swift_SFMS_Scheme_no = new SqlParameter("@GO_SWIFT_SFMS_SchemeNo", _txt_GO_Swift_SFMS_Scheme_no.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_FUND = new SqlParameter("@GO_SWIFT_SFMS_Debit_Fund", _txt_GO_Swift_SFMS_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Check_No = new SqlParameter("@GO_SWIFT_SFMS_Debit_CheckNo", _txt_GO_Swift_SFMS_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Available = new SqlParameter("@GO_SWIFT_SFMS_Debit_Available", _txt_GO_Swift_SFMS_Debit_Available.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Division = new SqlParameter("@GO_SWIFT_SFMS_Debit_Division", _txt_GO_Swift_SFMS_Debit_Division.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Inter_Amount = new SqlParameter("@GO_SWIFT_SFMS_Debit_InterAmt", _txt_GO_Swift_SFMS_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Inter_Rate = new SqlParameter("@GO_SWIFT_SFMS_Debit_InterRate", _txt_GO_Swift_SFMS_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_FUND = new SqlParameter("@GO_SWIFT_SFMS_Credit_Fund", _txt_GO_Swift_SFMS_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Check_No = new SqlParameter("@GO_SWIFT_SFMS_Credit_CheckNo", _txt_GO_Swift_SFMS_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Available = new SqlParameter("@GO_SWIFT_SFMS_Credit_Available", _txt_GO_Swift_SFMS_Credit_Available.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Division = new SqlParameter("@GO_SWIFT_SFMS_Credit_Division", _txt_GO_Swift_SFMS_Credit_Division.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Inter_Amount = new SqlParameter("@GO_SWIFT_SFMS_Credit_InterAmt", _txt_GO_Swift_SFMS_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Inter_Rate = new SqlParameter("@GO_SWIFT_SFMS_Credit_InterRate", _txt_GO_Swift_SFMS_Credit_Inter_Rate.ToUpper());

        ////  General_Operations LC_Commitment_CHRG

        SqlParameter P_GO_LC_Commitment_Flag = new SqlParameter("@GO_LC_Commitment_Flag", _chkGO_LC_Commitement.ToUpper());
        SqlParameter P_GO_LC_Commitment_Ref_No = new SqlParameter("@GO_LC_Commitment_Ref_No", _txt_GO_LC_Commitement_Ref_No.ToUpper());
        SqlParameter P_GO_LC_Commitment_Remark = new SqlParameter("@GO_LC_Commitment_Remark", _txt_GO_LC_Commitement_Remarks.ToUpper());
        SqlParameter P_GO_LC_Commitment_Section = new SqlParameter("@GO_LC_Commitment_Section", _txt_GO_LC_Commitement_SectionNo.ToUpper());
        SqlParameter P_GO_LC_Commitment_Comment = new SqlParameter("@GO_LC_Commitment_Comment", _txt_GO_LC_Commitement_Comment.ToUpper());
        SqlParameter P_GO_LC_Commitment_Memo = new SqlParameter("@GO_LC_Commitment_Memo", _txt_GO_LC_Commitement_MEMO.ToUpper());

        SqlParameter P_GO_LC_Commitment_Debit = new SqlParameter("@GO_LC_Commitment_Debit", _txt_GO_LC_Commitement_Debit_Code.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_Amt = new SqlParameter("@GO_LC_Commitment_Debit_Amt", _txt_GO_LC_Commitement_Debit_Amt.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_CCY = new SqlParameter("@GO_LC_Commitment_Debit_CCY", _txt_GO_LC_Commitement_Debit_Curr.ToUpper());

        SqlParameter P_GO_LC_Commitment_Debit_Cust = new SqlParameter("@GO_LC_Commitment_Debit_Cust", _txt_GO_LC_Commitement_Debit_Cust.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_Cust_AcCode = new SqlParameter("@GO_LC_Commitment_Debit_Cust_AcCode", _txt_GO_LC_Commitement_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_Cust_AccNo = new SqlParameter("@GO_LC_Commitment_Debit_Cust_AccNo", _txt_GO_LC_Commitement_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_ExchRate = new SqlParameter("@GO_LC_Commitment_Debit_ExchRate", _txt_GO_LC_Commitement_Debit_Exch_Rate.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_ExchCCY = new SqlParameter("@GO_LC_Commitment_Debit_ExchCCY", _txt_GO_LC_Commitement_Debit_Exch_Curr.ToUpper());

        SqlParameter P_GO_LC_Commitment_Debit_Advice_Print = new SqlParameter("@GO_LC_Commitment_Debit_Advice_Print", _txt_GO_LC_Commitement_Debit_Advice_Print.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_Details = new SqlParameter("@GO_LC_Commitment_Debit_Details", _txt_GO_LC_Commitement_Debit_Details.ToUpper());
        SqlParameter P_GO_LC_Commitment_Debit_Entity = new SqlParameter("@GO_LC_Commitment_Debit_Entity", _txt_GO_LC_Commitement_Debit_Entity.ToUpper());

        SqlParameter P_GO_LC_Commitment_Credit = new SqlParameter("@GO_LC_Commitment_Credit", _txt_GO_LC_Commitement_Credit_Code.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_Amt = new SqlParameter("@GO_LC_Commitment_Credit_Amt", _txt_GO_LC_Commitement_Credit_Amt.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_CCY = new SqlParameter("@GO_LC_Commitment_Credit_CCY", _txt_GO_LC_Commitement_Credit_Curr.ToUpper());

        SqlParameter P_GO_LC_Commitment_Credit_Cust = new SqlParameter("@GO_LC_Commitment_Credit_Cust", _txt_GO_LC_Commitement_Credit_Cust.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_Cust_AccNo = new SqlParameter("@GO_LC_Commitment_Credit_Cust_AccNo", _txt_GO_LC_Commitement_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_Cust_AcCode = new SqlParameter("@GO_LC_Commitment_Credit_Cust_AcCode", _txt_GO_LC_Commitement_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_ExchCCY = new SqlParameter("@GO_LC_Commitment_Credit_ExchCCY", _txt_GO_LC_Commitement_Credit_Exch_Curr.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_ExchRate = new SqlParameter("@GO_LC_Commitment_Credit_ExchRate", _txt_GO_LC_Commitement_Credit_Exch_Rate.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_Advice_Print = new SqlParameter("@GO_LC_Commitment_Credit_Advice_Print", _txt_GO_LC_Commitement_Credit_Advice_Print.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_Details = new SqlParameter("@GO_LC_Commitment_Credit_Details", _txt_GO_LC_Commitement_Credit_Details.ToUpper());
        SqlParameter P_GO_LC_Commitment_Credit_Entity = new SqlParameter("@GO_LC_Commitment_Credit_Entity", _txt_GO_LC_Commitement_Credit_Entity.ToUpper());

        SqlParameter P_txt_GO_LC_Commitement_Scheme_no = new SqlParameter("@GO_LC_Commitment_SchemeNo", _txt_GO_LC_Commitement_Scheme_no.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Debit_FUND = new SqlParameter("@GO_LC_Commitment_Debit_Fund", _txt_GO_LC_Commitement_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Debit_Check_No = new SqlParameter("@GO_LC_Commitment_Debit_CheckNo", _txt_GO_LC_Commitement_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Debit_Available = new SqlParameter("@GO_LC_Commitment_Debit_Available", _txt_GO_LC_Commitement_Debit_Available.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Debit_Division = new SqlParameter("@GO_LC_Commitment_Debit_Division", _txt_GO_LC_Commitement_Debit_Division.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Debit_Inter_Amount = new SqlParameter("@GO_LC_Commitment_Debit_InterAmt", _txt_GO_LC_Commitement_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Debit_Inter_Rate = new SqlParameter("@GO_LC_Commitment_Debit_InterRate", _txt_GO_LC_Commitement_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Credit_FUND = new SqlParameter("@GO_LC_Commitment_Credit_Fund", _txt_GO_LC_Commitement_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Credit_Check_No = new SqlParameter("@GO_LC_Commitment_Credit_CheckNo", _txt_GO_LC_Commitement_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Credit_Available = new SqlParameter("@GO_LC_Commitment_Credit_Available", _txt_GO_LC_Commitement_Credit_Available.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Credit_Division = new SqlParameter("@GO_LC_Commitment_Credit_Division", _txt_GO_LC_Commitement_Credit_Division.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Credit_Inter_Amount = new SqlParameter("@GO_LC_Commitment_Credit_InterAmt", _txt_GO_LC_Commitement_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO_LC_Commitement_Credit_Inter_Rate = new SqlParameter("@GO_LC_Commitment_Credit_InterRate", _txt_GO_LC_Commitement_Credit_Inter_Rate.ToUpper());

        SqlParameter P_None_Flag = new SqlParameter("@None_Flag", None_Flag.ToUpper());
        SqlParameter P_MT740_Flag = new SqlParameter("@MT740_Flag", MT740_Flag.ToUpper());
        SqlParameter P_MT756_Flag = new SqlParameter("@MT756_Flag", MT756_Flag.ToUpper());
        SqlParameter P_MT999_Flag = new SqlParameter("@MT999_Flag", MT999_Flag.ToUpper());
        SqlParameter P_MT799_Flag = new SqlParameter("@MT799_Flag", MT799_Flag.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        SqlParameter P_txt_Narrative1 = new SqlParameter("@_txt_Narrative1", _txt_Narrative1.ToUpper());
        SqlParameter P_txt_Narrative2 = new SqlParameter("@_txt_Narrative2", _txt_Narrative2.ToUpper());
        SqlParameter P_txt_Narrative3 = new SqlParameter("@_txt_Narrative3", _txt_Narrative3.ToUpper());
        SqlParameter P_txt_Narrative4 = new SqlParameter("@_txt_Narrative4", _txt_Narrative4.ToUpper());
        SqlParameter P_txt_Narrative5 = new SqlParameter("@_txt_Narrative5", _txt_Narrative5.ToUpper());
        SqlParameter P_txt_Narrative6 = new SqlParameter("@_txt_Narrative6", _txt_Narrative6.ToUpper());
        SqlParameter P_txt_Narrative7 = new SqlParameter("@_txt_Narrative7", _txt_Narrative7.ToUpper());
        SqlParameter P_txt_Narrative8 = new SqlParameter("@_txt_Narrative8", _txt_Narrative8.ToUpper());
        SqlParameter P_txt_Narrative9 = new SqlParameter("@_txt_Narrative9", _txt_Narrative9.ToUpper());
        SqlParameter P_txt_Narrative10 = new SqlParameter("@_txt_Narrative10", _txt_Narrative10.ToUpper());
        SqlParameter P_txt_Narrative11 = new SqlParameter("@_txt_Narrative11", _txt_Narrative11.ToUpper());
        SqlParameter P_txt_Narrative12 = new SqlParameter("@_txt_Narrative12", _txt_Narrative12.ToUpper());
        SqlParameter P_txt_Narrative13 = new SqlParameter("@_txt_Narrative13", _txt_Narrative13.ToUpper());
        SqlParameter P_txt_Narrative14 = new SqlParameter("@_txt_Narrative14", _txt_Narrative14.ToUpper());
        SqlParameter P_txt_Narrative15 = new SqlParameter("@_txt_Narrative15", _txt_Narrative15.ToUpper());
        SqlParameter P_txt_Narrative16 = new SqlParameter("@_txt_Narrative16", _txt_Narrative16.ToUpper());
        SqlParameter P_txt_Narrative17 = new SqlParameter("@_txt_Narrative17", _txt_Narrative17.ToUpper());
        SqlParameter P_txt_Narrative18 = new SqlParameter("@_txt_Narrative18", _txt_Narrative18.ToUpper());
        SqlParameter P_txt_Narrative19 = new SqlParameter("@_txt_Narrative19", _txt_Narrative19.ToUpper());
        SqlParameter P_txt_Narrative20 = new SqlParameter("@_txt_Narrative20", _txt_Narrative20.ToUpper());
        SqlParameter P_txt_Narrative21 = new SqlParameter("@_txt_Narrative21", _txt_Narrative21.ToUpper());
        SqlParameter P_txt_Narrative22 = new SqlParameter("@_txt_Narrative22", _txt_Narrative22.ToUpper());
        SqlParameter P_txt_Narrative23 = new SqlParameter("@_txt_Narrative23", _txt_Narrative23.ToUpper());
        SqlParameter P_txt_Narrative24 = new SqlParameter("@_txt_Narrative24", _txt_Narrative24.ToUpper());
        SqlParameter P_txt_Narrative25 = new SqlParameter("@_txt_Narrative25", _txt_Narrative25.ToUpper());
        SqlParameter P_txt_Narrative26 = new SqlParameter("@_txt_Narrative26", _txt_Narrative26.ToUpper());
        SqlParameter P_txt_Narrative27 = new SqlParameter("@_txt_Narrative27", _txt_Narrative27.ToUpper());
        SqlParameter P_txt_Narrative28 = new SqlParameter("@_txt_Narrative28", _txt_Narrative28.ToUpper());
        SqlParameter P_txt_Narrative29 = new SqlParameter("@_txt_Narrative29", _txt_Narrative29.ToUpper());
        SqlParameter P_txt_Narrative30 = new SqlParameter("@_txt_Narrative30", _txt_Narrative30.ToUpper());
        SqlParameter P_txt_Narrative31 = new SqlParameter("@_txt_Narrative31", _txt_Narrative31.ToUpper());
        SqlParameter P_txt_Narrative32 = new SqlParameter("@_txt_Narrative32", _txt_Narrative32.ToUpper());
        SqlParameter P_txt_Narrative33 = new SqlParameter("@_txt_Narrative33", _txt_Narrative33.ToUpper());
        SqlParameter P_txt_Narrative34 = new SqlParameter("@_txt_Narrative34", _txt_Narrative34.ToUpper());
        SqlParameter P_txt_Narrative35 = new SqlParameter("@_txt_Narrative35", _txt_Narrative35.ToUpper());

        SqlParameter P_ddlNegotiatingBankSwift = new SqlParameter("@_ddlNegotiatingBankSwift", _ddlNegotiatingBankSwift.ToUpper());
        SqlParameter P_txtNegoAccountNumber = new SqlParameter("@_txtNegoAccountNumber", _txtNegoAccountNumber.ToUpper());
        SqlParameter P_txtNegoSwiftCode = new SqlParameter("@_txtNegoSwiftCode", _txtNegoSwiftCode.ToUpper());
        SqlParameter P_txtNegoName = new SqlParameter("@_txtNegoName", _txtNegoName.ToUpper());
        SqlParameter P_txtNegoAddress1 = new SqlParameter("@_txtNegoAddress1", _txtNegoAddress1.ToUpper());
        SqlParameter P_txtNegoAddress2 = new SqlParameter("@_txtNegoAddress2", _txtNegoAddress2.ToUpper());
        SqlParameter P_txtNegoAddress3 = new SqlParameter("@_txtNegoAddress3", _txtNegoAddress3.ToUpper());
        //Added by bhupen for MT740 on 23082022
        SqlParameter P_txt_740_documentaryCreditno = new SqlParameter("@_txt_740_documentaryCreditno", _txt_740_documentaryCreditno.ToUpper());
        SqlParameter P_txt_740_AccountIdentification = new SqlParameter("@_txt_740_AccountIdentification", _txt_740_AccountIdentification.ToUpper());
        SqlParameter P_txt_740_Applicablerules = new SqlParameter("@_txt_740_Applicablerules", _txt_740_Applicablerules.ToUpper());
        SqlParameter P_txt_740_Date = new SqlParameter("@_txt_740_Date", _txt_740_Date.ToUpper());
        SqlParameter P_txt_740_Placeofexpiry = new SqlParameter("@_txt_740_Placeofexpiry", _txt_740_Placeofexpiry.ToUpper());
        SqlParameter P_txt_740_Draftsat1 = new SqlParameter("@_txt_740_Draftsat1", _txt_740_Draftsat1.ToUpper());
        SqlParameter P_txt_740_Draftsat2 = new SqlParameter("@_txt_740_Draftsat2", _txt_740_Draftsat2.ToUpper());
        SqlParameter P_txt_740_Draftsat3 = new SqlParameter("@_txt_740_Draftsat3", _txt_740_Draftsat3.ToUpper());
        SqlParameter P_ddlAvailablewithby_740 = new SqlParameter("@_ddlAvailablewithby_740", _ddlAvailablewithby_740.ToUpper());
        SqlParameter P_txtAvailablewithbyCode = new SqlParameter("@_txtAvailablewithbyCode", _txtAvailablewithbyCode.ToUpper());
        SqlParameter P_txtAvailablewithbySwiftCode = new SqlParameter("@_txtAvailablewithbySwiftCode", _txtAvailablewithbySwiftCode.ToUpper());
        SqlParameter P_txtAvailablewithbyName = new SqlParameter("@_txtAvailablewithbyName", _txtAvailablewithbyName.ToUpper());
        SqlParameter P_txtAvailablewithbyAddress1 = new SqlParameter("@_txtAvailablewithbyAddress1", _txtAvailablewithbyAddress1.ToUpper());
        SqlParameter P_txtAvailablewithbyAddress2 = new SqlParameter("@_txtAvailablewithbyAddress2", _txtAvailablewithbyAddress2.ToUpper());
        SqlParameter P_txtAvailablewithbyAddress3 = new SqlParameter("@_txtAvailablewithbyAddress3", _txtAvailablewithbyAddress3.ToUpper());
        SqlParameter P_ddlDrawee_740 = new SqlParameter("@_ddlDrawee_740", _ddlDrawee_740.ToUpper());
        SqlParameter P_txtDraweeAccountNumber = new SqlParameter("@_txtDraweeAccountNumber", _txtDraweeAccountNumber.ToUpper());
        SqlParameter P_txtDraweeSwiftCode = new SqlParameter("@_txtDraweeSwiftCode", _txtDraweeSwiftCode.ToUpper());
        SqlParameter P_txtDraweeName = new SqlParameter("@_txtDraweeName", _txtDraweeName.ToUpper());
        SqlParameter P_txtDraweeAddress1 = new SqlParameter("@_txtDraweeAddress1", _txtDraweeAddress1.ToUpper());
        SqlParameter P_txtDraweeAddress2 = new SqlParameter("@_txtDraweeAddress2", _txtDraweeAddress2.ToUpper());
        SqlParameter P_txtDraweeAddress3 = new SqlParameter("@_txtDraweeAddress3", _txtDraweeAddress3.ToUpper());
        SqlParameter P_txt_Acceptance_Beneficiary5 = new SqlParameter("@_txt_Acceptance_Beneficiary5", _txt_Acceptance_Beneficiary5.ToUpper());
        //End
        SqlParameter P_txt_Acceptance_Beneficiary = new SqlParameter("@_txt_Acceptance_Beneficiary", _txt_Acceptance_Beneficiary.ToUpper());
        SqlParameter P_txt_Acceptance_Beneficiary2 = new SqlParameter("@_txt_Acceptance_Beneficiary2", _txt_Acceptance_Beneficiary2.ToUpper());
        SqlParameter P_txt_Acceptance_Beneficiary3 = new SqlParameter("@_txt_Acceptance_Beneficiary3", _txt_Acceptance_Beneficiary3.ToUpper());
        SqlParameter P_txt_Acceptance_Beneficiary4 = new SqlParameter("@_txt_Acceptance_Beneficiary4", _txt_Acceptance_Beneficiary4.ToUpper());
        SqlParameter P_txtCreditCurrency = new SqlParameter("@_txtCreditCurrency", _txtCreditCurrency.ToUpper());
        SqlParameter P_txtCreditAmount = new SqlParameter("@_txtCreditAmount", _txtCreditAmount.ToUpper());
        SqlParameter P_txtPercentageCreditAmountTolerance = new SqlParameter("@_txtPercentageCreditAmountTolerance", _txtPercentageCreditAmountTolerance.ToUpper());
        SqlParameter P_txtPercentageCreditAmountTolerance1 = new SqlParameter("@_txtPercentageCreditAmountTolerance1", _txtPercentageCreditAmountTolerance1.ToUpper());
        SqlParameter P_txt_Acceptance_Max_Credit_Amt = new SqlParameter("@_txt_Acceptance_Max_Credit_Amt", _txt_Acceptance_Max_Credit_Amt.ToUpper());
        SqlParameter P_txt_Acceptance_Additional_Amt_Covered = new SqlParameter("@_txt_Acceptance_Additional_Amt_Covered", _txt_Acceptance_Additional_Amt_Covered.ToUpper());
        SqlParameter P_txt_Acceptance_Additional_Amt_Covered2 = new SqlParameter("@_txt_Acceptance_Additional_Amt_Covered2", _txt_Acceptance_Additional_Amt_Covered2.ToUpper());
        SqlParameter P_txt_Acceptance_Additional_Amt_Covered3 = new SqlParameter("@_txt_Acceptance_Additional_Amt_Covered3", _txt_Acceptance_Additional_Amt_Covered3.ToUpper());
        SqlParameter P_txt_Acceptance_Additional_Amt_Covered4 = new SqlParameter("@_txt_Acceptance_Additional_Amt_Covered4", _txt_Acceptance_Additional_Amt_Covered4.ToUpper());
        SqlParameter P_txtMixedPaymentDetails = new SqlParameter("@_txtMixedPaymentDetails", _txtMixedPaymentDetails.ToUpper());
        SqlParameter P_txtMixedPaymentDetails2 = new SqlParameter("@_txtMixedPaymentDetails2", _txtMixedPaymentDetails2.ToUpper());
        SqlParameter P_txtMixedPaymentDetails3 = new SqlParameter("@_txtMixedPaymentDetails3", _txtMixedPaymentDetails3.ToUpper());
        SqlParameter P_txtMixedPaymentDetails4 = new SqlParameter("@_txtMixedPaymentDetails4", _txtMixedPaymentDetails4.ToUpper());
        SqlParameter P_txtDeferredPaymentDetails = new SqlParameter("@_txtDeferredPaymentDetails", _txtDeferredPaymentDetails.ToUpper());
        SqlParameter P_txtDeferredPaymentDetails2 = new SqlParameter("@_txtDeferredPaymentDetails2", _txtDeferredPaymentDetails2.ToUpper());
        SqlParameter P_txtDeferredPaymentDetails3 = new SqlParameter("@_txtDeferredPaymentDetails3", _txtDeferredPaymentDetails3.ToUpper());
        SqlParameter P_txtDeferredPaymentDetails4 = new SqlParameter("@_txtDeferredPaymentDetails4", _txtDeferredPaymentDetails4.ToUpper());
        SqlParameter P_txt_Acceptance_Reimbur_Bank_Charges = new SqlParameter("@_txt_Acceptance_Reimbur_Bank_Charges", _txt_Acceptance_Reimbur_Bank_Charges.ToUpper());
        SqlParameter P_txt_Acceptance_Other_Charges = new SqlParameter("@_txt_Acceptance_Other_Charges", _txt_Acceptance_Other_Charges.ToUpper());
        SqlParameter P_txt_Acceptance_Other_Charges2 = new SqlParameter("@_txt_Acceptance_Other_Charges2", _txt_Acceptance_Other_Charges2.ToUpper());
        SqlParameter P_txt_Acceptance_Other_Charges3 = new SqlParameter("@_txt_Acceptance_Other_Charges3", _txt_Acceptance_Other_Charges3.ToUpper());
        SqlParameter P_txt_Acceptance_Other_Charges4 = new SqlParameter("@_txt_Acceptance_Other_Charges4", _txt_Acceptance_Other_Charges4.ToUpper());
        SqlParameter P_txt_Acceptance_Other_Charges5 = new SqlParameter("@_txt_Acceptance_Other_Charges5", _txt_Acceptance_Other_Charges5.ToUpper());
        SqlParameter P_txt_Acceptance_Other_Charges6 = new SqlParameter("@_txt_Acceptance_Other_Charges6", _txt_Acceptance_Other_Charges6.ToUpper());
        SqlParameter P_txt_Acceptance_Sender_to_Receiver_Information = new SqlParameter("@_txt_Acceptance_Sender_to_Receiver_Information", _txt_Acceptance_Sender_to_Receiver_Information.ToUpper());
        SqlParameter P_txt_Acceptance_Sender_to_Receiver_Information2 = new SqlParameter("@_txt_Acceptance_Sender_to_Receiver_Information2", _txt_Acceptance_Sender_to_Receiver_Information2.ToUpper());
        SqlParameter P_txt_Acceptance_Sender_to_Receiver_Information3 = new SqlParameter("@_txt_Acceptance_Sender_to_Receiver_Information3", _txt_Acceptance_Sender_to_Receiver_Information3.ToUpper());
        SqlParameter P_txt_Acceptance_Sender_to_Receiver_Information4 = new SqlParameter("@_txt_Acceptance_Sender_to_Receiver_Information4", _txt_Acceptance_Sender_to_Receiver_Information4.ToUpper());
        SqlParameter P_txt_Acceptance_Sender_to_Receiver_Information5 = new SqlParameter("@_txt_Acceptance_Sender_to_Receiver_Information5", _txt_Acceptance_Sender_to_Receiver_Information5.ToUpper());
        SqlParameter P_txt_Acceptance_Sender_to_Receiver_Information6 = new SqlParameter("@_txt_Acceptance_Sender_to_Receiver_Information6", _txt_Acceptance_Sender_to_Receiver_Information6.ToUpper());

        SqlParameter P_ddlReceiverCorrespondentMT = new SqlParameter("@_ddlReceiverCorrespondentMT", _ddlReceiverCorrespondentMT.ToUpper());
        SqlParameter P_txtReceiverAccountNumberMT = new SqlParameter("@_txtReceiverAccountNumberMT", _txtReceiverAccountNumberMT.ToUpper());
        SqlParameter P_txtReceiverSwiftCodeMT = new SqlParameter("@_txtReceiverSwiftCodeMT", _txtReceiverSwiftCodeMT.ToUpper());
        SqlParameter P_txtReceiverNameMT = new SqlParameter("@_txtReceiverNameMT", _txtReceiverNameMT.ToUpper());
        SqlParameter P_txtReceiverLocationMT = new SqlParameter("@_txtReceiverLocationMT", _txtReceiverLocationMT.ToUpper());
        SqlParameter P_txtReceiverAddress1MT = new SqlParameter("@_txtReceiverAddress1MT", _txtReceiverAddress1MT.ToUpper());
        SqlParameter P_txtReceiverAddress2MT = new SqlParameter("@_txtReceiverAddress2MT", _txtReceiverAddress2MT.ToUpper());
        SqlParameter P_txtReceiverAddress3MT = new SqlParameter("@_txtReceiverAddress3MT", _txtReceiverAddress3MT.ToUpper());
        SqlParameter P_ddlReceiverCorrespondentSFMS = new SqlParameter("@_ddlReceiverCorrespondentSFMS", _ddlReceiverCorrespondentSFMS.ToUpper());
        SqlParameter P_txtReceiverAccountNumberSFMS = new SqlParameter("@_txtReceiverAccountNumberSFMS", _txtReceiverAccountNumberSFMS.ToUpper());
        SqlParameter P_txtReceiverSwiftCodeSFMS = new SqlParameter("@_txtReceiverSwiftCodeSFMS", _txtReceiverSwiftCodeSFMS.ToUpper());
        SqlParameter P_txtReceiverNameSFMS = new SqlParameter("@_txtReceiverNameSFMS", _txtReceiverNameSFMS.ToUpper());
        SqlParameter P_txtReceiverLocationSFMS = new SqlParameter("@_txtReceiverLocationSFMS", _txtReceiverLocationSFMS.ToUpper());
        SqlParameter P_txtReceiverAddress1SFMS = new SqlParameter("@_txtReceiverAddress1SFMS", _txtReceiverAddress1SFMS.ToUpper());
        SqlParameter P_txtReceiverAddress2SFMS = new SqlParameter("@_txtReceiverAddress2SFMS", _txtReceiverAddress2SFMS.ToUpper());
        SqlParameter P_txtReceiverAddress3SFMS = new SqlParameter("@_txtReceiverAddress3SFMS", _txtReceiverAddress3SFMS.ToUpper());
        SqlParameter P_ddlSenderCorrespondentSFMS = new SqlParameter("@_ddlSenderCorrespondentSFMS", _ddlSenderCorrespondentSFMS.ToUpper());
        SqlParameter P_txtSenderAccountNumberSFMS = new SqlParameter("@_txtSenderAccountNumberSFMS", _txtSenderAccountNumberSFMS.ToUpper());
        SqlParameter P_txtSenderSwiftCodeSFMS = new SqlParameter("@_txtSenderSwiftCodeSFMS", _txtSenderSwiftCodeSFMS.ToUpper());
        SqlParameter P_txtSenderNameSFMS = new SqlParameter("@_txtSenderNameSFMS", _txtSenderNameSFMS.ToUpper());
        SqlParameter P_txtSenderLocationSFMS = new SqlParameter("@_txtSenderLocationSFMS", _txtSenderLocationSFMS.ToUpper());
        SqlParameter P_txtSenderAddress1SFMS = new SqlParameter("@_txtSenderAddress1SFMS", _txtSenderAddress1SFMS.ToUpper());
        SqlParameter P_txtSenderAddress2SFMS = new SqlParameter("@_txtSenderAddress2SFMS", _txtSenderAddress2SFMS.ToUpper());
        SqlParameter P_txtSenderAddress3SFMS = new SqlParameter("@_txtSenderAddress3SFMS", _txtSenderAddress3SFMS.ToUpper());

        SqlParameter P_txt_Narrative_756_1 = new SqlParameter("@_txt_Narrative_756_1", _txt_Narrative_756_1.ToUpper());
        SqlParameter P_txt_Narrative_756_2 = new SqlParameter("@_txt_Narrative_756_2", _txt_Narrative_756_2.ToUpper());
        SqlParameter P_txt_Narrative_756_3 = new SqlParameter("@_txt_Narrative_756_3", _txt_Narrative_756_3.ToUpper());
        SqlParameter P_txt_Narrative_756_4 = new SqlParameter("@_txt_Narrative_756_4", _txt_Narrative_756_4.ToUpper());
        SqlParameter P_txt_Narrative_756_5 = new SqlParameter("@_txt_Narrative_756_5", _txt_Narrative_756_5.ToUpper());
        SqlParameter P_txt_Narrative_756_6 = new SqlParameter("@_txt_Narrative_756_6", _txt_Narrative_756_6.ToUpper());
        SqlParameter P_txt_Narrative_756_7 = new SqlParameter("@_txt_Narrative_756_7", _txt_Narrative_756_7.ToUpper());
        SqlParameter P_txt_Narrative_756_8 = new SqlParameter("@_txt_Narrative_756_8", _txt_Narrative_756_8.ToUpper());
        SqlParameter P_txt_Narrative_756_9 = new SqlParameter("@_txt_Narrative_756_9", _txt_Narrative_756_9.ToUpper());
        SqlParameter P_txt_Narrative_756_10 = new SqlParameter("@_txt_Narrative_756_10", _txt_Narrative_756_10.ToUpper());
        SqlParameter P_txt_Narrative_756_11 = new SqlParameter("@_txt_Narrative_756_11", _txt_Narrative_756_11.ToUpper());
        SqlParameter P_txt_Narrative_756_12 = new SqlParameter("@_txt_Narrative_756_12", _txt_Narrative_756_12.ToUpper());
        SqlParameter P_txt_Narrative_756_13 = new SqlParameter("@_txt_Narrative_756_13", _txt_Narrative_756_13.ToUpper());
        SqlParameter P_txt_Narrative_756_14 = new SqlParameter("@_txt_Narrative_756_14", _txt_Narrative_756_14.ToUpper());
        SqlParameter P_txt_Narrative_756_15 = new SqlParameter("@_txt_Narrative_756_15", _txt_Narrative_756_15.ToUpper());
        SqlParameter P_txt_Narrative_756_16 = new SqlParameter("@_txt_Narrative_756_16", _txt_Narrative_756_16.ToUpper());
        SqlParameter P_txt_Narrative_756_17 = new SqlParameter("@_txt_Narrative_756_17", _txt_Narrative_756_17.ToUpper());
        SqlParameter P_txt_Narrative_756_18 = new SqlParameter("@_txt_Narrative_756_18", _txt_Narrative_756_18.ToUpper());
        SqlParameter P_txt_Narrative_756_19 = new SqlParameter("@_txt_Narrative_756_19", _txt_Narrative_756_19.ToUpper());
        SqlParameter P_txt_Narrative_756_20 = new SqlParameter("@_txt_Narrative_756_20", _txt_Narrative_756_20.ToUpper());
        SqlParameter P_txt_Narrative_756_21 = new SqlParameter("@_txt_Narrative_756_21", _txt_Narrative_756_21.ToUpper());
        SqlParameter P_txt_Narrative_756_22 = new SqlParameter("@_txt_Narrative_756_22", _txt_Narrative_756_22.ToUpper());
        SqlParameter P_txt_Narrative_756_23 = new SqlParameter("@_txt_Narrative_756_23", _txt_Narrative_756_23.ToUpper());
        SqlParameter P_txt_Narrative_756_24 = new SqlParameter("@_txt_Narrative_756_24", _txt_Narrative_756_24.ToUpper());
        SqlParameter P_txt_Narrative_756_25 = new SqlParameter("@_txt_Narrative_756_25", _txt_Narrative_756_25.ToUpper());
        SqlParameter P_txt_Narrative_756_26 = new SqlParameter("@_txt_Narrative_756_26", _txt_Narrative_756_26.ToUpper());
        SqlParameter P_txt_Narrative_756_27 = new SqlParameter("@_txt_Narrative_756_27", _txt_Narrative_756_27.ToUpper());
        SqlParameter P_txt_Narrative_756_28 = new SqlParameter("@_txt_Narrative_756_28", _txt_Narrative_756_28.ToUpper());
        SqlParameter P_txt_Narrative_756_29 = new SqlParameter("@_txt_Narrative_756_29", _txt_Narrative_756_29.ToUpper());
        SqlParameter P_txt_Narrative_756_30 = new SqlParameter("@_txt_Narrative_756_30", _txt_Narrative_756_30.ToUpper());
        SqlParameter P_txt_Narrative_756_31 = new SqlParameter("@_txt_Narrative_756_31", _txt_Narrative_756_31.ToUpper());
        SqlParameter P_txt_Narrative_756_32 = new SqlParameter("@_txt_Narrative_756_32", _txt_Narrative_756_32.ToUpper());
        SqlParameter P_txt_Narrative_756_33 = new SqlParameter("@_txt_Narrative_756_33", _txt_Narrative_756_33.ToUpper());
        SqlParameter P_txt_Narrative_756_34 = new SqlParameter("@_txt_Narrative_756_34", _txt_Narrative_756_34.ToUpper());
        SqlParameter P_txt_Narrative_756_35 = new SqlParameter("@_txt_Narrative_756_35", _txt_Narrative_756_35.ToUpper());
        SqlParameter P_txt_Discrepancy_Charges_Swift = new SqlParameter("@_txt_Discrepancy_Charges_Swift", _txt_Discrepancy_Charges_Swift.ToUpper());
        SqlParameter P_txt_Discrepancy_Charges_SFMS = new SqlParameter("@_txt_Discrepancy_Charges_SFMS", _txt_Discrepancy_Charges_SFMS.ToUpper());

        SqlParameter P_Ledger_File_Flag = new SqlParameter("@Ledger_File_Flag", _chk_Ledger_Modify.ToUpper());
        SqlParameter P_Ledger_Remark = new SqlParameter("@Ledger_Remark", _txtLedgerRemark.ToUpper());
        SqlParameter P_Ledger_Cust_AccNo = new SqlParameter("@Ledger_Cust_AccNo", _txtLedgerCustName.ToUpper());
        SqlParameter P_Ledger_Curr = new SqlParameter("@Ledger_Curr", _txtLedgerCURR.ToUpper());
        SqlParameter P_Ledger_AccCode = new SqlParameter("@Ledger_AccCode", _txtLedgerAccode.ToUpper());
        SqlParameter P_Ledger_Ref_No = new SqlParameter("@Ledger_Ref_No", _txtLedgerRefNo.ToUpper());
        SqlParameter P_Ledger_Amount = new SqlParameter("@Ledger_Amount", _txtLedger_Modify_amt.ToUpper());
        SqlParameter P_Ledger_Operation_Date = new SqlParameter("@Ledger_Operation_Date", _txtLedgerOperationDate.ToUpper());
        SqlParameter p_Ledger_Balance_Amt = new SqlParameter("@Ledger_Balance_Amt", _txtLedgerBalanceAmt.ToUpper());
        SqlParameter P_Ledger_Accept_Date = new SqlParameter("@Ledger_Accept_Date", _txtLedgerAcceptDate.ToUpper());
        SqlParameter P_Ledger_Maturity = new SqlParameter("@Ledger_Maturity", _txtLedgerMaturity.ToUpper());
        SqlParameter P_Ledger_Settlement_Date = new SqlParameter("@Ledger_Settlement_Date", _txtLedgerSettlememtDate.ToUpper());
        SqlParameter P_Ledger_Settlement_Value = new SqlParameter("@Ledger_Settlement_Value", _txtLedgerSettlValue.ToUpper());
        SqlParameter P_Ledger_Last_Mod_Date = new SqlParameter("@Ledger_Last_Mod_Date", _txtLedgerLastModDate.ToUpper());
        SqlParameter P_Ledger_Rem_EUC = new SqlParameter("@Ledger_Rem_EUC", _txtLedgerREM_EUC.ToUpper());
        SqlParameter P_Ledger_Last_OPE_Date = new SqlParameter("@Ledger_Last_OPE_Date", _txtLedgerLastOPEDate.ToUpper());
        SqlParameter P_Ledger_Trans_No = new SqlParameter("@Ledger_Trans_No", _txtLedgerTransNo.ToUpper());
        SqlParameter P_Ledger_Contra_Country = new SqlParameter("@Ledger_Contra_Country", _txtLedgerContraCountry.ToUpper());
        SqlParameter P_Ledger_Status_Code = new SqlParameter("@Ledger_Status_Code", _txtLedgerStatusCode.ToUpper());
        SqlParameter P_Ledger_Collect_Of_Cumm = new SqlParameter("@Ledger_Collect_Of_Cumm", _txtLedgerCollectOfComm.ToUpper());
        SqlParameter P_Ledger_Commodity = new SqlParameter("@Ledger_Commodity", _txtLedgerCommodity.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Rate = new SqlParameter("@Ledger_Handling_Comm_Rate", _txtLedgerhandlingCommRate.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Curr = new SqlParameter("@Ledger_Handling_Comm_Curr", _txtLedgerhandlingCommCurr.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Amt = new SqlParameter("@Ledger_Handling_Comm_Amt", _txtLedgerhandlingCommAmt.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Payer = new SqlParameter("@Ledger_Handling_Comm_Payer", _txtLedgerhandlingCommPayer.ToUpper());
        SqlParameter P_Ledger_Postage_Rate = new SqlParameter("@Ledger_Postage_Rate", _txtLedgerPostageRate.ToUpper());
        SqlParameter P_Ledger_Postage_Curr = new SqlParameter("@Ledger_Postage_Curr", _txtLedgerPostageCurr.ToUpper());
        SqlParameter P_Ledger_Postage_Amt = new SqlParameter("@Ledger_Postage_Amt", _txtLedgerPostageAmt.ToUpper());
        SqlParameter P_Ledger_Postage_Payer = new SqlParameter("@Ledger_Postage_Payer", _txtLedgerPostagePayer.ToUpper());
        SqlParameter P_Ledger_Others_Rate = new SqlParameter("@Ledger_Others_Rate", _txtLedgerOthersRate.ToUpper());
        SqlParameter P_Ledger_Others_Curr = new SqlParameter("@Ledger_Others_Curr", _txtLedgerOthersCurr.ToUpper());
        SqlParameter P_Ledger_Others_Amt = new SqlParameter("@Ledger_Others_Amt", _txtLedgerOthersAmt.ToUpper());
        SqlParameter P_Ledger_Others_Payer = new SqlParameter("@Ledger_Others_Payer", _txtLedgerOthersPayer.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Rate = new SqlParameter("@Ledger_Their_Comm_Rate", _txtLedgerTheirCommRate.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Curr = new SqlParameter("@Ledger_Their_Comm_Curr", _txtLedgerTheirCommCurr.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Amt = new SqlParameter("@Ledger_Their_Comm_Amt", _txtLedgerTheirCommAmt.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Payer = new SqlParameter("@Ledger_Their_Comm_Payer", _txtLedgerTheirCommPayer.ToUpper());
        SqlParameter P_Ledger_Collect_Nego_Bank = new SqlParameter("@Ledger_Collect_Nego_Bank", _txtLedgerNegoBank.ToUpper());
        SqlParameter P_Ledger_Reimbursing_Bank = new SqlParameter("@Ledger_Reimbursing_Bank", _txtLedgerReimbursingBank.ToUpper());
        SqlParameter P_Ledger_Drawer_Drawee = new SqlParameter("@Ledger_Drawer_Drawee", _txtLedgerDrawer.ToUpper());
        SqlParameter P_Ledger_Tenor = new SqlParameter("@Ledger_Tenor", _txtLedgerTenor.ToUpper());
        SqlParameter P_Ledger_Attn = new SqlParameter("@Ledger_Attn", _txtLedgerAttn.ToUpper());

        SqlParameter P_txtNegoPartyIdentifier = new SqlParameter("@_txtNegoPartyIdentifier", _txtNegoPartyIdentifier.ToUpper());
        SqlParameter P_txtReceiverPartyIdentifier = new SqlParameter("@_txtReceiverPartyIdentifier", _txtReceiverPartyIdentifier.ToUpper());
        SqlParameter P_txtSenderPartyIdentifierSFMS = new SqlParameter("@_txtSenderPartyIdentifierSFMS", _txtSenderPartyIdentifierSFMS.ToUpper());
        SqlParameter P_txtReceiverPartyIdentifierSFMS = new SqlParameter("@_txtReceiverPartyIdentifierSFMS", _txtReceiverPartyIdentifierSFMS.ToUpper());



        /////////////////////////////////////////// Bhupen 747 //////////////////////////////////////////////////////////////////////////////

        SqlParameter P_txtdoccreditNo_747 = new SqlParameter("@DocCreditNo_747", _txtdoccreditNo_747.ToUpper());
        SqlParameter P_txtReimbursingbanRef_747 = new SqlParameter("@ReimbursingBankRef_747", _txtReimbursingbanRef_747.ToUpper());
        SqlParameter P_txtDateoforiginalAutoOfReimburse_747 = new SqlParameter("@DateoforiginalAuthoOfReimburse_747", _txtDateoforiginalAutoOfReimburse_747.ToUpper());
        SqlParameter P_txtNewdateofExpiry_747 = new SqlParameter("@NewDateOfExpiry_747", _txtNewdateofExpiry_747.ToUpper());
        SqlParameter P_txtIncreaseofDocumentryCreditAmt_Curr_747 = new SqlParameter("@IncreaseofDocumentryCreditAmt_Curr_747", _txtIncreaseofDocumentryCreditAmt_Curr_747.ToUpper());
        SqlParameter P_txtIncreaseofDocumentryCreditAmt_747 = new SqlParameter("@IncreaseofDocumentryCreditAmt_747", _txtIncreaseofDocumentryCreditAmt_747.ToUpper());
        SqlParameter P_txtdecreaseofDocumentryCreditAmt_Curr_747 = new SqlParameter("@decreaseofDocumentryCreditAmt_Curr_747", _txtdecreaseofDocumentryCreditAmt_Curr_747.ToUpper());
        SqlParameter P_txtdecreaseofDocumentryCreditAmt_747 = new SqlParameter("@decreaseofDocumentryCreditAmt_747", _txtdecreaseofDocumentryCreditAmt_747.ToUpper());
        SqlParameter P_NewDocumentryCreditAmtAfterAmendment_Curr_747 = new SqlParameter("@NewDocumentryCreditAmtAfterAmendment_Curr_747", _NewDocumentryCreditAmtAfterAmendment_Curr_747.ToUpper());
        SqlParameter P_NewDocumentryCreditAmtAfterAmendment_747 = new SqlParameter("@NewDocumentryCreditAmtAfterAmendment_747", _NewDocumentryCreditAmtAfterAmendment_747.ToUpper());
        SqlParameter P_txtPercentageCreditAmtTolerance_747_1 = new SqlParameter("@PercentageCreditAmtTolerance1_747", _txtPercentageCreditAmtTolerance_747_1.ToUpper());
        SqlParameter P_txtPercentageCreditAmtTolerance_747_2 = new SqlParameter("@PercentageCreditAmtTolerance2_747", _txtPercentageCreditAmtTolerance_747_2.ToUpper());
        SqlParameter P_txtAddAmtCovered_747_1 = new SqlParameter("@AddAmtCovered1_747", _txtAddAmtCovered_747_1.ToUpper());
        SqlParameter P_txtAddAmtCovered_747_2 = new SqlParameter("@AddAmtCovered2_747", _txtAddAmtCovered_747_2.ToUpper());
        SqlParameter P_txtAddAmtCovered_747_3 = new SqlParameter("@AddAmtCovered3_747", _txtAddAmtCovered_747_3.ToUpper());
        SqlParameter P_txtAddAmtCovered_747_4 = new SqlParameter("@AddAmtCovered4_747", _txtAddAmtCovered_747_4.ToUpper());
        SqlParameter P_txtSenToRecInfo_747_1 = new SqlParameter("@SenToRecInfo1_747", _txtSenToRecInfo_747_1.ToUpper());
        SqlParameter P_txtSenToRecInfo_747_2 = new SqlParameter("@SenToRecInfo2_747", _txtSenToRecInfo_747_2.ToUpper());
        SqlParameter P_txtSenToRecInfo_747_3 = new SqlParameter("@SenToRecInfo3_747", _txtSenToRecInfo_747_3.ToUpper());
        SqlParameter P_txtSenToRecInfo_747_4 = new SqlParameter("@SenToRecInfo4_747", _txtSenToRecInfo_747_4.ToUpper());
        SqlParameter P_txtSenToRecInfo_747_5 = new SqlParameter("@SenToRecInfo5_747", _txtSenToRecInfo_747_5.ToUpper());
        SqlParameter P_txtSenToRecInfo_747_6 = new SqlParameter("@SenToRecInfo6_747", _txtSenToRecInfo_747_6.ToUpper());

        SqlParameter P_MT747_Flag = new SqlParameter("@MT747_Flag", MT747_Flag.ToUpper());
        SqlParameter P_txt_Narrative_747_1 = new SqlParameter("@Narrative1_747", _txt_Narrative_747_1.ToUpper());
        SqlParameter P_txt_Narrative_747_2 = new SqlParameter("@Narrative2_747", _txt_Narrative_747_2.ToUpper());
        SqlParameter P_txt_Narrative_747_3 = new SqlParameter("@Narrative3_747", _txt_Narrative_747_3.ToUpper());
        SqlParameter P_txt_Narrative_747_4 = new SqlParameter("@Narrative4_747", _txt_Narrative_747_4.ToUpper());
        SqlParameter P_txt_Narrative_747_5 = new SqlParameter("@Narrative5_747", _txt_Narrative_747_5.ToUpper());
        SqlParameter P_txt_Narrative_747_6 = new SqlParameter("@Narrative6_747", _txt_Narrative_747_6.ToUpper());
        SqlParameter P_txt_Narrative_747_7 = new SqlParameter("@Narrative7_747", _txt_Narrative_747_7.ToUpper());
        SqlParameter P_txt_Narrative_747_8 = new SqlParameter("@Narrative8_747", _txt_Narrative_747_8.ToUpper());
        SqlParameter P_txt_Narrative_747_9 = new SqlParameter("@Narrative9_747", _txt_Narrative_747_9.ToUpper());
        SqlParameter P_txt_Narrative_747_10 = new SqlParameter("@Narrative10_747", _txt_Narrative_747_10.ToUpper());
        SqlParameter P_txt_Narrative_747_11 = new SqlParameter("@Narrative11_747", _txt_Narrative_747_11.ToUpper());
        SqlParameter P_txt_Narrative_747_12 = new SqlParameter("@Narrative12_747", _txt_Narrative_747_12.ToUpper());
        SqlParameter P_txt_Narrative_747_13 = new SqlParameter("@Narrative13_747", _txt_Narrative_747_13.ToUpper());
        SqlParameter P_txt_Narrative_747_14 = new SqlParameter("@Narrative14_747", _txt_Narrative_747_14.ToUpper());
        SqlParameter P_txt_Narrative_747_15 = new SqlParameter("@Narrative15_747", _txt_Narrative_747_15.ToUpper());
        SqlParameter P_txt_Narrative_747_16 = new SqlParameter("@Narrative16_747", _txt_Narrative_747_16.ToUpper());
        SqlParameter P_txt_Narrative_747_17 = new SqlParameter("@Narrative17_747", _txt_Narrative_747_17.ToUpper());
        SqlParameter P_txt_Narrative_747_18 = new SqlParameter("@Narrative18_747", _txt_Narrative_747_18.ToUpper());
        SqlParameter P_txt_Narrative_747_19 = new SqlParameter("@Narrative19_747", _txt_Narrative_747_19.ToUpper());
        SqlParameter P_txt_Narrative_747_20 = new SqlParameter("@Narrative20_747", _txt_Narrative_747_20.ToUpper());


        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdate_Acceptance_ACC_IBD", P_BranchName, P_txtDocNo,
            P_Document_Curr, P_txtHO_Apl, P_txtIBD_ACC_kind, P_txtValueDate,
            P_txtCommentCode, P_txtAutoSettlement,
            P_txtDraftAmt, P_txtContractNo, P_txtExchRate, P_txtCountryRisk,
            P_txtRiskCust, P_txtGradeCode,
            P_txtApplNo, P_txtApplBR, P_txtPurpose, P_ddl_PurposeCode,
            P_txtsettlCodeForCust, P_txtsettlforCust_Abbr, P_txtsettlforCust_AccCode, P_txtsettlforCust_AccNo,
            P_txtInterest_From, P_txtInterest_To, P_txt_No_Of_Days,
            P_txt_INT_Rate,

            P_txtBaseRate, P_txtSpread, P_txtInterestAmt, P_txtFundType,
            P_txtInternalRate, P_txtSettl_CodeForBank, P_ddl_Settl_ForBank_Abbr,
            P_txtSettl_ForBank_AccCode, P_txtSettl_ForBank_AccNo,
            P_txtAttn, P_txtREM_EUC,

            //Instruction
            P_txt_INST_Code,

            //Import Accounting
            P_chk_IMP_ACC_Commission,
            P_txt_IMP_ACC_ExchRate,
            P_txtPrinc_matu, P_txtInterest_matu, P_txtCommission_matu, P_txtTheir_Commission_matu,
            P_txtPrinc_lump, P_txtInterest_lump, P_txtCommission_lump, P_txtTheir_Commission_lump,
            P_txtprinc_Contract_no, P_txtInterest_Contract_no, P_txtCommission_Contract_no, P_txtTheir_Commission_Contract_no, P_txtprinc_Ex_rate, P_txtInterest_Ex_rate, P_txtCommission_Ex_rate, P_txtTheir_Commission_Ex_rate,
            P_txtprinc_Intnl_Ex_rate, P_txtInterest_Intnl_Ex_rate, P_txtCommission_Intnl_Ex_rate, P_txtTheir_Commission_Intnl_Ex_rate, P_txt_CR_Code, P_txt_CR_Cust_abbr,
            P_txt_CR_Cust_Acc, P_txt_CR_Acceptance_amt, P_txt_CR_Interest_amt, P_txt_CR_Accept_Commission_amt,
            P_txt_CR_Pay_Handle_Commission_amt, P_txt_CR_Their_Commission_amt, P_txt_CR_Others_amt,

            P_txt_DR_Code, P_txt_DR_Cust_abbr, P_txt_DR_Cust_Acc,
            P_txt_DR_Cur_Acc_amt, P_txt_DR_Cur_Acc_amt2, P_txt_DR_Cur_Acc_amt3,

            P_txt_CR_Acceptance_payer, P_txt_CR_Interest_payer, P_txt_CR_Accept_Commission_Payer, P_txt_CR_Pay_Handle_Commission_Payer, P_txt_CR_Others_Payer, P_txt_CR_Their_Commission_Payer,
            P_txt_DR_Cur_Acc_payer, P_txt_DR_Cur_Acc_payer2, P_txt_DR_Cur_Acc_payer3,

            P_txt_Princ_Ex_Curr, P_txt_interest_Ex_Curr, P_txt_Commission_Ex_Curr, P_txt_Their_Commission_Ex_Curr,
            P_txt_CR_Acceptance_Curr, P_txt_CR_Interest_Curr, P_txt_CR_Accept_Commission_Curr,
            P_txt_CR_Pay_Handle_Commission_Curr, P_txt_CR_Others_Curr, P_txt_CR_Their_Commission_Curr,
            P_txt_DR_Cur_Acc_Curr, P_txt_DR_Cur_Acc_Curr2, P_txt_DR_Cur_Acc_Curr3,

            //// General Operation SWIFT_SFMS
            P_GO_SWIFT_SFMS_Flag, P_GO_SWIFT_SFMS_Ref_No, P_GO_SWIFT_SFMS_Remark,
            P_GO_SWIFT_SFMS_Section, P_GO_SWIFT_SFMS_Comment, P_GO_SWIFT_SFMS_Memo,

            P_GO_SWIFT_SFMS_Debit, P_GO_SWIFT_SFMS_Debit_Amt, P_GO_SWIFT_SFMS_Debit_CCY,
            P_GO_SWIFT_SFMS_Debit_Cust, P_GO_SWIFT_SFMS_Debit_Cust_AcCode, P_GO_SWIFT_SFMS_Debit_Cust_AccNo,
            P_GO_SWIFT_SFMS_Debit_ExchCCY, P_GO_SWIFT_SFMS_Debit_ExchRate,
            P_GO_SWIFT_SFMS_Debit_Advice_Print, P_GO_SWIFT_SFMS_Debit_Details, P_GO_SWIFT_SFMS_Debit_Entity,

            P_GO_SWIFT_SFMS_Credit, P_GO_SWIFT_SFMS_Credit_Amt, P_GO_SWIFT_SFMS_Credit_CCY,
            P_GO_SWIFT_SFMS_Credit_Cust, P_GO_SWIFT_SFMS_Credit_Cust_AccNo, P_GO_SWIFT_SFMS_Credit_Cust_AcCode,
            P_GO_SWIFT_SFMS_Credit_ExchCCY, P_GO_SWIFT_SFMS_Credit_ExchRate,
            P_GO_SWIFT_SFMS_Credit_Advice_Print, P_GO_SWIFT_SFMS_Credit_Details, P_GO_SWIFT_SFMS_Credit_Entity,

            P_txt_GO_Swift_SFMS_Scheme_no,
            P_txt_GO_Swift_SFMS_Debit_FUND, P_txt_GO_Swift_SFMS_Debit_Check_No, P_txt_GO_Swift_SFMS_Debit_Available,
            P_txt_GO_Swift_SFMS_Debit_Division, P_txt_GO_Swift_SFMS_Debit_Inter_Amount, P_txt_GO_Swift_SFMS_Debit_Inter_Rate,
            P_txt_GO_Swift_SFMS_Credit_FUND, P_txt_GO_Swift_SFMS_Credit_Check_No, P_txt_GO_Swift_SFMS_Credit_Available,
            P_txt_GO_Swift_SFMS_Credit_Division, P_txt_GO_Swift_SFMS_Credit_Inter_Amount, P_txt_GO_Swift_SFMS_Credit_Inter_Rate,

             //// General Operation LC_Commitment
             P_GO_LC_Commitment_Flag, P_GO_LC_Commitment_Comment, P_GO_LC_Commitment_Ref_No,
             P_GO_LC_Commitment_Section, P_GO_LC_Commitment_Remark, P_GO_LC_Commitment_Memo,

             P_GO_LC_Commitment_Debit, P_GO_LC_Commitment_Debit_CCY, P_GO_LC_Commitment_Debit_Amt,
             P_GO_LC_Commitment_Debit_Cust, P_GO_LC_Commitment_Debit_Cust_AcCode, P_GO_LC_Commitment_Debit_Cust_AccNo,
             P_GO_LC_Commitment_Debit_ExchCCY, P_GO_LC_Commitment_Debit_ExchRate,
             P_GO_LC_Commitment_Debit_Advice_Print, P_GO_LC_Commitment_Debit_Details, P_GO_LC_Commitment_Debit_Entity,

             P_GO_LC_Commitment_Credit, P_GO_LC_Commitment_Credit_CCY, P_GO_LC_Commitment_Credit_Amt,
             P_GO_LC_Commitment_Credit_Cust, P_GO_LC_Commitment_Credit_Cust_AccNo, P_GO_LC_Commitment_Credit_Cust_AcCode,
             P_GO_LC_Commitment_Credit_ExchCCY, P_GO_LC_Commitment_Credit_ExchRate,
             P_GO_LC_Commitment_Credit_Advice_Print, P_GO_LC_Commitment_Credit_Details, P_GO_LC_Commitment_Credit_Entity,

            P_txt_GO_LC_Commitement_Scheme_no,
            P_txt_GO_LC_Commitement_Debit_FUND, P_txt_GO_LC_Commitement_Debit_Check_No, P_txt_GO_LC_Commitement_Debit_Available,
            P_txt_GO_LC_Commitement_Debit_Division, P_txt_GO_LC_Commitement_Debit_Inter_Amount, P_txt_GO_LC_Commitement_Debit_Inter_Rate,
            P_txt_GO_LC_Commitement_Credit_FUND, P_txt_GO_LC_Commitement_Credit_Check_No, P_txt_GO_LC_Commitement_Credit_Available,
            P_txt_GO_LC_Commitement_Credit_Division, P_txt_GO_LC_Commitement_Credit_Inter_Amount, P_txt_GO_LC_Commitement_Credit_Inter_Rate,
            //////////////
            P_None_Flag, P_MT740_Flag, P_MT756_Flag, P_MT999_Flag, P_MT799_Flag,

            P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate,

            P_txt_Narrative1, P_txt_Narrative2, P_txt_Narrative3, P_txt_Narrative4, P_txt_Narrative5, P_txt_Narrative6, P_txt_Narrative7, P_txt_Narrative8, P_txt_Narrative9, P_txt_Narrative10,
            P_txt_Narrative11, P_txt_Narrative12, P_txt_Narrative13, P_txt_Narrative14, P_txt_Narrative15, P_txt_Narrative16, P_txt_Narrative17, P_txt_Narrative18, P_txt_Narrative19, P_txt_Narrative20,
            P_txt_Narrative21, P_txt_Narrative22, P_txt_Narrative23, P_txt_Narrative24, P_txt_Narrative25, P_txt_Narrative26, P_txt_Narrative27, P_txt_Narrative28, P_txt_Narrative29, P_txt_Narrative30,
            P_txt_Narrative31, P_txt_Narrative32, P_txt_Narrative33, P_txt_Narrative34, P_txt_Narrative35,
            P_ddlNegotiatingBankSwift,
            P_txtNegoAccountNumber, P_txtNegoSwiftCode, P_txtNegoName, P_txtNegoAddress1, P_txtNegoAddress2, P_txtNegoAddress3,
            //Added by bhupen for MT740 on 23082022
            P_txt_740_documentaryCreditno, P_txt_740_AccountIdentification, P_txt_740_Applicablerules, P_txt_740_Date, P_txt_740_Placeofexpiry, P_txt_740_Draftsat1, P_txt_740_Draftsat2,
            P_txt_740_Draftsat3, P_ddlAvailablewithby_740, P_txtAvailablewithbyCode, P_txtAvailablewithbySwiftCode, P_txtAvailablewithbyName, P_txtAvailablewithbyAddress1, P_txtAvailablewithbyAddress2,
            P_txtAvailablewithbyAddress3, P_ddlDrawee_740, P_txtDraweeAccountNumber, P_txtDraweeSwiftCode, P_txtDraweeName, P_txtDraweeAddress1, 
            P_txtDraweeAddress2, P_txtDraweeAddress3,P_txt_Acceptance_Beneficiary5,

            P_txt_Acceptance_Beneficiary, P_txt_Acceptance_Beneficiary2, P_txt_Acceptance_Beneficiary3, P_txt_Acceptance_Beneficiary4, P_txtCreditCurrency, P_txtCreditAmount,
            P_txtPercentageCreditAmountTolerance, P_txtPercentageCreditAmountTolerance1, P_txt_Acceptance_Max_Credit_Amt, P_txt_Acceptance_Additional_Amt_Covered, P_txt_Acceptance_Additional_Amt_Covered2,
            P_txt_Acceptance_Additional_Amt_Covered3, P_txt_Acceptance_Additional_Amt_Covered4, P_txtMixedPaymentDetails, P_txtMixedPaymentDetails2, P_txtMixedPaymentDetails3,
            P_txtMixedPaymentDetails4, P_txtDeferredPaymentDetails, P_txtDeferredPaymentDetails2, P_txtDeferredPaymentDetails3, P_txtDeferredPaymentDetails4, P_txt_Acceptance_Reimbur_Bank_Charges,
            P_txt_Acceptance_Other_Charges, P_txt_Acceptance_Other_Charges2, P_txt_Acceptance_Other_Charges3, P_txt_Acceptance_Other_Charges4, P_txt_Acceptance_Other_Charges5,
            P_txt_Acceptance_Other_Charges6, P_txt_Acceptance_Sender_to_Receiver_Information, P_txt_Acceptance_Sender_to_Receiver_Information2, P_txt_Acceptance_Sender_to_Receiver_Information3,
            P_txt_Acceptance_Sender_to_Receiver_Information4, P_txt_Acceptance_Sender_to_Receiver_Information5, P_txt_Acceptance_Sender_to_Receiver_Information6,

            P_ddlReceiverCorrespondentMT, P_txtReceiverAccountNumberMT, P_txtReceiverSwiftCodeMT, P_txtReceiverNameMT,
            P_txtReceiverLocationMT, P_txtReceiverAddress1MT, P_txtReceiverAddress2MT, P_txtReceiverAddress3MT,
            P_txt_Narrative_756_1, P_txt_Narrative_756_2, P_txt_Narrative_756_3, P_txt_Narrative_756_4, P_txt_Narrative_756_5,
            P_txt_Narrative_756_6, P_txt_Narrative_756_7, P_txt_Narrative_756_8, P_txt_Narrative_756_9, P_txt_Narrative_756_10,
            P_txt_Narrative_756_11, P_txt_Narrative_756_12, P_txt_Narrative_756_13, P_txt_Narrative_756_14, P_txt_Narrative_756_15,
            P_txt_Narrative_756_16, P_txt_Narrative_756_17, P_txt_Narrative_756_18, P_txt_Narrative_756_19, P_txt_Narrative_756_20,
            P_txt_Narrative_756_21, P_txt_Narrative_756_22, P_txt_Narrative_756_23, P_txt_Narrative_756_24, P_txt_Narrative_756_25,
            P_txt_Narrative_756_26, P_txt_Narrative_756_27, P_txt_Narrative_756_28, P_txt_Narrative_756_29, P_txt_Narrative_756_30,
            P_txt_Narrative_756_31, P_txt_Narrative_756_32, P_txt_Narrative_756_33, P_txt_Narrative_756_34, P_txt_Narrative_756_35,
            P_ddlReceiverCorrespondentSFMS, P_txtReceiverAccountNumberSFMS, P_txtReceiverSwiftCodeSFMS, P_txtReceiverNameSFMS, P_txtReceiverLocationSFMS,
            P_txtReceiverAddress1SFMS, P_txtReceiverAddress2SFMS, P_txtReceiverAddress3SFMS, P_ddlSenderCorrespondentSFMS, P_txtSenderAccountNumberSFMS,
            P_txtSenderSwiftCodeSFMS, P_txtSenderNameSFMS, P_txtSenderLocationSFMS, P_txtSenderAddress1SFMS, P_txtSenderAddress2SFMS, P_txtSenderAddress3SFMS,

            P_txt_Discrepancy_Charges_Swift, P_txt_Discrepancy_Charges_SFMS,
            ////Ledger_File
            P_Ledger_File_Flag, P_Ledger_Remark, P_Ledger_Cust_AccNo, P_Ledger_Curr, P_Ledger_AccCode, P_Ledger_Ref_No, P_Ledger_Amount, p_Ledger_Balance_Amt,
            P_Ledger_Operation_Date, P_Ledger_Maturity, P_Ledger_Accept_Date, P_Ledger_Settlement_Date, P_Ledger_Settlement_Value,
            P_Ledger_Last_Mod_Date, P_Ledger_Rem_EUC, P_Ledger_Last_OPE_Date, P_Ledger_Trans_No, P_Ledger_Contra_Country, P_Ledger_Status_Code,
            P_Ledger_Collect_Of_Cumm, P_Ledger_Commodity,
            P_Ledger_Handling_Comm_Rate, P_Ledger_Handling_Comm_Curr, P_Ledger_Handling_Comm_Amt, P_Ledger_Handling_Comm_Payer,
            P_Ledger_Postage_Rate, P_Ledger_Postage_Curr, P_Ledger_Postage_Amt, P_Ledger_Postage_Payer,
            P_Ledger_Others_Rate, P_Ledger_Others_Curr, P_Ledger_Others_Amt, P_Ledger_Others_Payer,
            P_Ledger_Their_Comm_Rate, P_Ledger_Their_Comm_Curr, P_Ledger_Their_Comm_Amt, P_Ledger_Their_Comm_Payer,
            P_Ledger_Collect_Nego_Bank, P_Ledger_Reimbursing_Bank, P_Ledger_Drawer_Drawee, P_Ledger_Tenor, P_Ledger_Attn,
            P_txtNegoPartyIdentifier, P_txtReceiverPartyIdentifier, P_txtSenderPartyIdentifierSFMS, P_txtReceiverPartyIdentifierSFMS,

            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////  Bhupen /////////////////////////////////////////////////////////////////////
            P_txtdoccreditNo_747, P_txtReimbursingbanRef_747, P_MT747_Flag,
            P_txtDateoforiginalAutoOfReimburse_747, P_txtNewdateofExpiry_747,
            P_txtIncreaseofDocumentryCreditAmt_Curr_747, P_txtIncreaseofDocumentryCreditAmt_747,
            P_txtdecreaseofDocumentryCreditAmt_Curr_747, P_txtdecreaseofDocumentryCreditAmt_747,
            P_NewDocumentryCreditAmtAfterAmendment_Curr_747, P_NewDocumentryCreditAmtAfterAmendment_747,
            P_txtPercentageCreditAmtTolerance_747_1, P_txtPercentageCreditAmtTolerance_747_2, P_txtAddAmtCovered_747_1, P_txtAddAmtCovered_747_2,
            P_txtAddAmtCovered_747_3, P_txtAddAmtCovered_747_4,
            P_txtSenToRecInfo_747_1, P_txtSenToRecInfo_747_2, P_txtSenToRecInfo_747_3,
            P_txtSenToRecInfo_747_4, P_txtSenToRecInfo_747_5, P_txtSenToRecInfo_747_6,
            P_txt_Narrative_747_1, P_txt_Narrative_747_2, P_txt_Narrative_747_3, P_txt_Narrative_747_4, P_txt_Narrative_747_5,
            P_txt_Narrative_747_6, P_txt_Narrative_747_7, P_txt_Narrative_747_8, P_txt_Narrative_747_9, P_txt_Narrative_747_10,
            P_txt_Narrative_747_11, P_txt_Narrative_747_12, P_txt_Narrative_747_13, P_txt_Narrative_747_14, P_txt_Narrative_747_15,
            P_txt_Narrative_747_16, P_txt_Narrative_747_17, P_txt_Narrative_747_18, P_txt_Narrative_747_19, P_txt_Narrative_747_20

            ////////////////////////////////////////////// END  ///////////////////////////////////////////////////////////////////////

            );
        return _Result;
    }
    //[WebMethod]
    //public static List<string> Get_Nostro_Bank_List( string BranchName,string Curr)
    //{
    //    List<string> Nostro_Bank_Code = new List<string>();
    //    int i = 0;
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter pBranchName = new SqlParameter("@BranchName", BranchName);
    //    SqlParameter pCurr = new SqlParameter("@Curr", Curr);
    //    DataTable dt = objData.getData("TF_IMP_Nostro_Bank_List_For_Acceptance", pBranchName, pCurr);
    //    if (dt.Rows.Count > 0)
    //    {

    //        while (i != dt.Rows.Count)
    //        {
    //            Nostro_Bank_Code.Add(dt.Rows[i]["CUST_ABBR"].ToString());
    //            i++;
    //        }
    //    }
    //    return Nostro_Bank_Code;
    //}

    protected void btnLedgerNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentDetails;
        txtContractNo.Focus();
    }
    protected void btnDocPrev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentLedger;
        txtLedger_Modify_amt.Focus();
    }
    protected void btnDocNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentInstructions;
        txt_INST_Code.Focus();
    }
    protected void btn_Instr_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentDetails;
        txtContractNo.Focus();
    }
    protected void btn_Instr_Next_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentAccounting;
        txt_IMP_ACC_ExchRate.Focus();
    }
    protected void btnDocAccPrev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentInstructions;
        txt_INST_Code.Focus();
    }
    protected void btnDocAccNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentGO;
    }
    protected void btnGO_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentAccounting;
        txtPrinc_matu.Focus();
    }
    protected void btnGO_Next_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentSwiftFile;

    }
    protected void btn_SWIFT_File_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentGO;
    }
    protected void chkGO_Swift_SFMS_OnCheckedChanged(object sender, EventArgs e)
    {
        GO_Swift_SFMS_Toggel();
    }
    protected void chkGO_LC_Commitement_OnCheckedChanged(object sender, EventArgs e)
    {
        GO_LC_Commitement_Toggel();
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

            txt_GO_Swift_SFMS_Debit_Cust.Text = txt_DR_Cust_abbr.Text;
            txt_GO_Swift_SFMS_Credit_Cust.Text = txt_DR_Cust_abbr.Text;
            txt_GO_Swift_SFMS_Debit_Cust_AcCode.Text = txt_DR_Code.Text;
            txt_GO_Swift_SFMS_Debit_Cust_AccNo.Text = txt_DR_Cust_Acc.Text;


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

            txt_GO_LC_Commitement_Debit_Cust.Text = txt_DR_Cust_abbr.Text;
            txt_GO_LC_Commitement_Credit_Cust.Text = txt_DR_Cust_abbr.Text;
            txt_GO_LC_Commitement_Debit_Cust_AcCode.Text = txt_DR_Code.Text;
            txt_GO_LC_Commitement_Debit_Cust_AccNo.Text = txt_DR_Cust_Acc.Text;

            txt_GO_LC_Commitement_Debit_Details.Text = "LC COMMITMENT  CHGS";

            txt_GO_LC_Commitement_Credit_Code.Text = "C";
            txt_GO_LC_Commitement_Credit_Curr.Text = "INR";

            txt_GO_LC_Commitement_Credit_Cust_AcCode.Text = "66802";
            txt_GO_LC_Commitement_Credit_Details.Text = "LC COMMITMENT  CHGS";

            fillLCDetails(txtLCNo.Text.Trim(), txtDocNo.Text.ToString().Trim(), txtRiskCust.Text.Trim(), lblForeign_Local.Text.Trim());
        }
    }
    protected void chk_Ledger_Modify_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_Ledger_Modify.Checked == true)
        {
            PanelLedgerFile.Visible = true;
            txtLedgerRefNo.Text = txtDocNo.Text;
            txtLedger_Modify_amt.Text = "";
            txtLedgerBalanceAmt.Text = "";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Commission_Toggel();", true);
        }
        if (chk_Ledger_Modify.Checked == false)
        {
            PanelLedgerFile.Visible = false;
            txtDraftAmt.Text = lblBillAmt.Text;
            txtDraftAmt.Text = lblBillAmt.Text;
            txt_DiscAmt.Text = lblBillAmt.Text;
            txt_CR_Acceptance_amt.Text = lblBillAmt.Text;
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Commission_Toggel();", true);
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        disableonstatus();
    }
    public void disableonstatus()
    {
        if (chk_Ledger_Modify.Checked == true)
        {
            if (txtLedgerStatusCode.Text.ToString().ToUpper() == "S")
            {
                txtContractNo.Enabled = false;
                txtExchRate.Enabled = false;
                txtRiskCust.Enabled = false;
                txtsettlCodeForCust.Enabled = false;
                txtsettlforCust_Abbr.Enabled = false;
                txtsettlforCust_AccCode.Enabled = false;
                txtsettlforCust_AccNo.Enabled = false;
                txtInterest_From.Enabled = false;
                txtInterest_To.Enabled = false;
                txtInternalRate.Enabled = false;
                txtBaseRate.Enabled = false;
                txtSpread.Enabled = false;
                txtInterestAmt.Enabled = false;
                txtFundType.Enabled = false;
                txtInternalRate.Enabled = false;
                txt_INT_Rate.Enabled = false;
                txtsettlforCust_Abbr.Enabled = false;
                txtsettlforCust_AccCode.Enabled = false;
                txtsettlforCust_AccNo.Enabled = false;
                txtSettl_ForBank_Abbr.Enabled = false;
                txtSettl_ForBank_AccCode.Enabled = false;
                txtSettl_ForBank_AccNo.Enabled = false;
                txtREM_EUC.Enabled = false;
                txtAttn.Enabled = false;
                btnSettl_CodeForBank_help.Enabled = false;
                txt_INST_Code.Enabled = false;
                //Import accounting
                txt_IMP_ACC_ExchRate.Enabled = false;
                txtInterest_matu.Enabled = false;
                txtInterest_lump.Enabled = false;
                txtCommission_matu.Enabled = false;
                txtCommission_lump.Enabled = false;
                txt_CR_Code.Enabled = false;
                txt_CR_Cust_abbr.Enabled = false;
                txt_CR_Cust_Acc.Enabled = false;
                txt_CR_Acceptance_payer.Enabled = false;
                txt_CR_Accept_Commission_Curr.Enabled = false;
                txt_CR_Accept_Commission_amt.Enabled = false;
                txt_CR_Accept_Commission_Payer.Enabled = false;
                txt_CR_Others_Curr.Enabled = false;
                txt_CR_Others_amt.Enabled = false;
                txt_CR_Others_Payer.Enabled = false;
                txt_CR_Their_Commission_Curr.Enabled = false;
                txt_CR_Their_Commission_amt.Enabled = false;
                txt_CR_Their_Commission_Payer.Enabled = false;
                txt_DR_Code.Enabled = false;
                txt_DR_Cust_abbr.Enabled = false;
                txt_DR_Cust_Acc.Enabled = false;
                txt_DR_Cur_Acc_amt.Enabled = false;
                txt_DR_Cur_Acc_payer.Enabled = false;

                chkGO_LC_Commitement.Checked = false;
                chkGO_Swift_SFMS.Checked = false;
                chkGO_LC_Commitement.Enabled = false;
                chkGO_Swift_SFMS.Enabled = false;

                //swift disable
                txt_Discrepancy_Charges_SFMS.Enabled = false;
                ddlSenderCorrespondentSFMS.Enabled = false;
                txtSenderPartyIdentifierSFMS.Enabled = false;
                txtSenderAccountNumberSFMS.Enabled = false;
                txtSenderSwiftCodeSFMS.Enabled = false;
                txtSenderNameSFMS.Enabled = false;
                txtSenderLocationSFMS.Enabled = false;
                txtSenderAddress1SFMS.Enabled = false;
                panel_AddNarrative_756.Enabled = false;

                panel_AddNarrative.Enabled = false;
                txtSenderAddress2SFMS.Enabled = false;

                txtSenderAddress3SFMS.Enabled = false;
                ddlReceiverCorrespondentSFMS.Enabled = false;
                txtReceiverPartyIdentifierSFMS.Enabled = false;
                txtReceiverAccountNumberSFMS.Enabled = false;
                txtReceiverSwiftCodeSFMS.Enabled = false;
                txtReceiverNameSFMS.Enabled = false;
                txtReceiverLocationSFMS.Enabled = false;
                txtReceiverAddress1SFMS.Enabled = false;
                txtReceiverAddress2SFMS.Enabled = false;
                txtReceiverAddress3SFMS.Enabled = false;
                txt_Discrepancy_Charges_Swift.Enabled = false;
                ddlReceiverCorrespondentMT.Enabled = false;
                txtReceiverPartyIdentifier.Enabled = false;
                txtReceiverAccountNumberMT.Enabled = false;
                txtReceiverSwiftCodeMT.Enabled = false;

                rdb_swift_756.Checked = false;
                rdb_swift_799.Checked = false;
                rdb_swift_999.Checked = false;
                rdb_swift_756.Enabled = false;
                rdb_swift_799.Enabled = false;
                rdb_swift_999.Enabled = false;
                chk_IMP_ACC_Commission.Enabled = false;
                if (lblDoc_Curr.Text.ToString() == "INR")
                {
                    rdb_swift_740.Enabled = false;
                }
                else
                {
                    rdb_swift_740.Enabled = true;
                }
            }
            else
            {
                txtContractNo.Enabled = true;
                txtExchRate.Enabled = true;
                txtRiskCust.Enabled = true;
                txtsettlCodeForCust.Enabled = true;
                txtsettlforCust_Abbr.Enabled = true;
                txtsettlforCust_AccCode.Enabled = true;
                txtsettlforCust_AccNo.Enabled = true;
                txtInterest_From.Enabled = true;
                txtInterest_To.Enabled = true;
                txtInternalRate.Enabled = true;
                txtBaseRate.Enabled = true;
                txtSpread.Enabled = true;
                txtInterestAmt.Enabled = true;
                txt_INT_Rate.Enabled = true;
                txtFundType.Enabled = true;
                txtInternalRate.Enabled = true;
                txtsettlforCust_Abbr.Enabled = true;
                txtsettlforCust_AccCode.Enabled = true;
                txtsettlforCust_AccNo.Enabled = true;
                txtSettl_ForBank_Abbr.Enabled = true;
                txtSettl_ForBank_AccCode.Enabled = true;
                txtSettl_ForBank_AccNo.Enabled = true;
                txtREM_EUC.Enabled = true;
                txtAttn.Enabled = true;
                txt_INST_Code.Enabled = true;
                //Import accounting
                txt_IMP_ACC_ExchRate.Enabled = true;
                txtInterest_matu.Enabled = true;
                txtInterest_lump.Enabled = true;
                txtCommission_matu.Enabled = true;
                txtCommission_lump.Enabled = true;
                txt_CR_Code.Enabled = true;
                txt_CR_Cust_abbr.Enabled = true;
                txt_CR_Cust_Acc.Enabled = true;
                txt_CR_Acceptance_payer.Enabled = true;
                txt_CR_Accept_Commission_Curr.Enabled = true;
                txt_CR_Accept_Commission_amt.Enabled = true;
                txt_CR_Accept_Commission_Payer.Enabled = true;
                txt_CR_Others_Curr.Enabled = true;
                txt_CR_Others_amt.Enabled = true;
                txt_CR_Others_Payer.Enabled = true;
                txt_CR_Their_Commission_Curr.Enabled = true;
                txt_CR_Their_Commission_amt.Enabled = true;
                txt_CR_Their_Commission_Payer.Enabled = true;
                txt_DR_Code.Enabled = true;
                txt_DR_Cust_abbr.Enabled = true;
                txt_DR_Cust_Acc.Enabled = true;
                txt_DR_Cur_Acc_amt.Enabled = true;

                txt_DR_Cur_Acc_payer.Enabled = true;

                chkGO_LC_Commitement.Enabled = true;
                chkGO_Swift_SFMS.Enabled = true;

                //swift disable
                txt_Discrepancy_Charges_SFMS.Enabled = true;
                ddlSenderCorrespondentSFMS.Enabled = true;
                txtSenderPartyIdentifierSFMS.Enabled = true;
                txtSenderAccountNumberSFMS.Enabled = true;
                txtSenderSwiftCodeSFMS.Enabled = true;
                txtSenderNameSFMS.Enabled = true;
                txtSenderLocationSFMS.Enabled = true;
                txtSenderAddress1SFMS.Enabled = true;
                panel_AddNarrative_756.Enabled = true;

                panel_AddNarrative.Enabled = true;
                txtSenderAddress2SFMS.Enabled = true;

                txtSenderAddress3SFMS.Enabled = true;
                ddlReceiverCorrespondentSFMS.Enabled = true;
                txtReceiverPartyIdentifierSFMS.Enabled = true;
                txtReceiverAccountNumberSFMS.Enabled = true;
                txtReceiverSwiftCodeSFMS.Enabled = true;
                txtReceiverNameSFMS.Enabled = true;
                txtReceiverLocationSFMS.Enabled = true;
                txtReceiverAddress1SFMS.Enabled = true;
                txtReceiverAddress2SFMS.Enabled = true;
                txtReceiverAddress3SFMS.Enabled = true;
                txt_Discrepancy_Charges_Swift.Enabled = true;
                ddlReceiverCorrespondentMT.Enabled = true;
                txtReceiverPartyIdentifier.Enabled = true;
                txtReceiverAccountNumberMT.Enabled = true;
                txtReceiverSwiftCodeMT.Enabled = true;

                rdb_swift_756.Enabled = true;
                rdb_swift_799.Enabled = true;
                rdb_swift_999.Enabled = true;
                chk_IMP_ACC_Commission.Enabled = true;
            }
        }
    }

    //----------------Added by bhupen on 15112022-----------------------------------//
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
                lbl_Exch_rate.Visible = true;
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
                            btnSubmit.Enabled = false;
                            btn_Verify.Visible = true;
                        }
                        else
                        {
                            lbl_LEIAmt_Check.Text = "Transaction Bill Amt is less than LEI Thresold Limit.";
                            lbl_LEIAmt_Check.ForeColor = System.Drawing.Color.Green;
                            lbl_LEIAmt_Check.Font.Size = 10;
                            hdnLeiFlag.Value = "N";
                            btnSubmit.Enabled = true;
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
                SqlParameter P_Status = new SqlParameter("@status", "C");
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
                btnSubmit.Enabled = true;

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
                    hdncustleiflag.Value = "R";
                    btnSubmit.Enabled = false;
                    btn_Verify.Visible = true;
                    ReccuringLEI.Visible = true;
                    ReccuringLEI.Text = "This is Recurring LEI Customer.";
                    ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                    ReccuringLEI.Font.Size = 10;
                //}
            }
        }
    }
    protected void Check_MT740()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Doc_Lc_No", txtLCNo.Text.Trim());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_Check740_LC", PDocNo);
        if (dt.Rows.Count > 0)
        {
            Label2.Text = "LC No: " + dt.Rows[0]["Doc_LC_No"].ToString();
            Label3.Text = "Doc No: " + dt.Rows[0]["Document_No"].ToString();
            txt_747_DateofogauthriztnRmburse.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
        }
        else
        {
            Label2.Text = "";
            Label3.Text = "";
            txt_747_DateofogauthriztnRmburse.Text = "";
        }
    }
    //---------------------------------END-----------------------------------//
}