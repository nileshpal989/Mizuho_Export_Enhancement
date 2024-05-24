using ClosedXML.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IMP_Transactions_TF_IMP_BOE_Checker : System.Web.UI.Page
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
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_BOE_Maker_View.aspx", true);
                }
                else
                {
                    hdnUserName.Value = Session["userName"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    hdnDocumentScrutiny.Value = Request.QueryString["lblDocument_Scrutiny"].ToString();
                    ToggleDocType(Request.QueryString["DocType"].Trim(), Request.QueryString["flcIlcType"].ToString(), Request.QueryString["lblDocument_Scrutiny"].ToString());
                    fillCurrency();
                    fill_GBaseCommodity();
                    fill_Country();
                    fill_DisposalOfDocument();
                    PaymentSwiftselection();
                    if (Request.QueryString["Mode"].ToString() == "Edit")
                    {
                        FillDetails();
                        MakeReadOnly();
                        fillBranch();
                        Check_LEINO_ExchRateDetails();
                        Check_Lei_RecurringStatus();
                    }
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                        btn_Verify.Enabled = false;
                        Check_Lei_Verified();
                        Check_Lei_RecurringStatus();
                    }
                }
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "PageLoadFunctions();", true);
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    protected void FillDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_BOE_Details", PDocNo);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["BRO_REF_NO"].ToString() != "")
            {
                lblBRONo.Text = "BRO No- " + dt.Rows[0]["BRO_REF_NO"].ToString();
                hdnBRONo.Value = dt.Rows[0]["BRO_REF_NO"].ToString();
            }
            if (dt.Rows[0]["BRO_REF_NO"].ToString() != "")
            {
                lblBRODate.Text = "Date- " + dt.Rows[0]["BRO_Date"].ToString();
            }
            if (dt.Rows[0]["Bill_Amt"].ToString() != "")
            {
                lblBROAmount.Text = "Amount- " + dt.Rows[0]["Bill_Amt"].ToString();
            }

            if (dt.Rows[0]["SG_Doc_No"].ToString() != "")
            {
                lblSGDocNo.Text = "SG DocNo- " + dt.Rows[0]["SG_Doc_No"].ToString();
                hdnSGDocNo.Value = dt.Rows[0]["SG_Doc_No"].ToString();
                lblSGValDate.Text = "Date- " + dt.Rows[0]["SG_Value_Date"].ToString();
                lblSGAmount.Text = "Amount- " + dt.Rows[0]["SG_Bill_Amt"].ToString();
            }
            txtDocNo.Text = dt.Rows[0]["Document_No"].ToString();
            hdnDocNo.Value = dt.Rows[0]["Document_No"].ToString();
            txtDateReceived.Text = dt.Rows[0]["Received_Date"].ToString();
            txtLogdmentDate.Text = dt.Rows[0]["Lodgment_Date"].ToString();
            txtCustomer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
            fillCustomerMasterDetails();
            txt_AcceptanceDate.Text = dt.Rows[0]["Expt_Accept_Date"].ToString();

            txtBillAmount.Text = dt.Rows[0]["Bill_Amount"].ToString();
            ddl_Doc_Currency.SelectedValue = dt.Rows[0]["Bill_Currency"].ToString();
            fillCurrency_Description();
            txt_LC_No.Text = dt.Rows[0]["Doc_LC_No"].ToString();
            fillLCDetails();
            if (dt.Rows[0]["DP_DA"].ToString() == "Y")
            {
                rbtDP.Checked = true;
            }
            else
            {
                rbtDA.Checked = true;
            }
            ddlTenor.SelectedValue = dt.Rows[0]["Tenor_Type"].ToString();
            txtTenor.Text = dt.Rows[0]["Tenor"].ToString();
            ddlTenor_Days_From.SelectedValue = dt.Rows[0]["Tenor_Days_From"].ToString();
            txtTenor_Description.Text = dt.Rows[0]["Tenor_Desc"].ToString();
            txtBOExchange.Text = dt.Rows[0]["BOExchange_Date"].ToString();
            txtDueDate.Text = dt.Rows[0]["Maturity_Date"].ToString();

            ddl_Nego_Remit_Bank.SelectedValue = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            if (dt.Rows[0]["Nego_Remit_Bank_Type"].ToString() == "FOREIGN")
            {
                btn_Swift_Create.Visible = true;
                btn_SFMS_create.Visible = false;
            }
            else
            {
                if (dt.Rows[0]["Nego_Remit_Bank_Type"].ToString() == "LOCAL")
                {
                    btn_Swift_Create.Visible = false;
                    btn_SFMS_create.Visible = true;
                }
                else
                {
                    btn_Swift_Create.Visible = false;
                    btn_SFMS_create.Visible = false;
                }
            }

            txtNego_Remit_Bank.Text = dt.Rows[0]["Nego_Remit_Bank"].ToString();
            hdnNegoRemiSwiftCode.Value = dt.Rows[0]["SwiftCode"].ToString();
            fillNego_Remit_BankDetails();

            txt_Their_Ref_no.Text = dt.Rows[0]["Their_Ref_No"].ToString();
            txtNego_Date.Text = dt.Rows[0]["Nego_Date"].ToString();

            txtAcwithInstitution.Text = dt.Rows[0]["Acc_With_Bank"].ToString();
            fillAcwithInstitutionDetails();

            txtReimbursingbank.Text = dt.Rows[0]["Reimbursing_Bank"].ToString();
            fillReimbursingbankDetails();

            txt_Inv_No.Text = dt.Rows[0]["Invoice_No"].ToString();
            ddlDrawer.SelectedValue = dt.Rows[0]["Drawer"].ToString();
            txt_Inv_Date.Text = dt.Rows[0]["Invoice_Date"].ToString();

            ddl_Commodity.SelectedValue = dt.Rows[0]["Commodity_Code"].ToString();
            txtCommodityDesc.Text = dt.Rows[0]["Commodity_Desc"].ToString();
            fill_GBaseCommodity_Description();

            txt_CountryOfOrigin.Text = dt.Rows[0]["Country_Of_Origin"].ToString();
            ddlCountryCode.SelectedValue = dt.Rows[0]["Country_Code"].ToString();
            fill_Country_Description();

            txtShippingDate.Text = dt.Rows[0]["Shipping_Date"].ToString();
            txtDocFirst1.Text = dt.Rows[0]["DOC_First_INS"].ToString();
            txtDocFirst2.Text = dt.Rows[0]["DOC_First_BL"].ToString();
            txtDocFirst3.Text = dt.Rows[0]["DOC_First_AWB"].ToString();
            txtVesselName.Text = dt.Rows[0]["Vessel_Name"].ToString();
            txtDocSecond1.Text = dt.Rows[0]["DOC_Second_INS"].ToString();
            txtDocSecond2.Text = dt.Rows[0]["DOC_Second_BL"].ToString();
            txtDocSecond3.Text = dt.Rows[0]["DOC_Second_AWB"].ToString();
            txtFromPort.Text = dt.Rows[0]["From_Port"].ToString();
            txtToPort.Text = dt.Rows[0]["To_Port"].ToString();
            ddl_Interest_Currency.SelectedValue = dt.Rows[0]["Interest_Curr"].ToString();
            txt_Interest_Amount.Text = dt.Rows[0]["Interest_Amount"].ToString();
            ddl_Comission_Currency.SelectedValue = dt.Rows[0]["Our_Comm_Curr"].ToString();
            txtComissionAmount.Text = dt.Rows[0]["Our_Cumm_Amount"].ToString();
            ddl_Other_Currency.SelectedValue = dt.Rows[0]["Other_Comm_Curr"].ToString();
            txtOther_amount.Text = dt.Rows[0]["Other_Cumm_Amount"].ToString();
            ddl_Their_Commission_Currency.SelectedValue = dt.Rows[0]["Their_Comm_Curr"].ToString();
            txtTheirCommission_Amount.Text = dt.Rows[0]["Their_Cumm_Amount"].ToString();
            txt_Total_Bill_Amt.Text = dt.Rows[0]["Total_Bill_Amount"].ToString();

            txt_Discrepancy_1.Text = dt.Rows[0]["Discrepancy1"].ToString();
            txt_Discrepancy_2.Text = dt.Rows[0]["Discrepancy2"].ToString();
            txt_Discrepancy_3.Text = dt.Rows[0]["Discrepancy3"].ToString();
            txt_Discrepancy_4.Text = dt.Rows[0]["Discrepancy4"].ToString();
            txt_Discrepancy_5.Text = dt.Rows[0]["Discrepancy5"].ToString();
            txt_Discrepancy_6.Text = dt.Rows[0]["Discrepancy6"].ToString();
            txt_Discrepancy_7.Text = dt.Rows[0]["Discrepancy7"].ToString();
            txt_Discrepancy_8.Text = dt.Rows[0]["Discrepancy8"].ToString();
            txt_Discrepancy_9.Text = dt.Rows[0]["Discrepancy9"].ToString();
            txt_Discrepancy_10.Text = dt.Rows[0]["Discrepancy10"].ToString();
            txt_Discrepancy_11.Text = dt.Rows[0]["Discrepancy11"].ToString();
            txt_Discrepancy_12.Text = dt.Rows[0]["Discrepancy12"].ToString();
            txt_Discrepancy_13.Text = dt.Rows[0]["Discrepancy13"].ToString();
            txt_Discrepancy_14.Text = dt.Rows[0]["Discrepancy14"].ToString();
            txt_Discrepancy_15.Text = dt.Rows[0]["Discrepancy15"].ToString();
            txt_Discrepancy_16.Text = dt.Rows[0]["Discrepancy16"].ToString();
            txt_Discrepancy_17.Text = dt.Rows[0]["Discrepancy17"].ToString();
            txt_Discrepancy_18.Text = dt.Rows[0]["Discrepancy18"].ToString();
            txt_Discrepancy_19.Text = dt.Rows[0]["Discrepancy19"].ToString();
            txt_Discrepancy_20.Text = dt.Rows[0]["Discrepancy20"].ToString();
            txt_Discrepancy_21.Text = dt.Rows[0]["Discrepancy21"].ToString();
            txt_Discrepancy_22.Text = dt.Rows[0]["Discrepancy22"].ToString();
            txt_Discrepancy_23.Text = dt.Rows[0]["Discrepancy23"].ToString();
            txt_Discrepancy_24.Text = dt.Rows[0]["Discrepancy24"].ToString();
            txt_Discrepancy_25.Text = dt.Rows[0]["Discrepancy25"].ToString();
            txt_Discrepancy_26.Text = dt.Rows[0]["Discrepancy26"].ToString();
            txt_Discrepancy_27.Text = dt.Rows[0]["Discrepancy27"].ToString();
            txt_Discrepancy_28.Text = dt.Rows[0]["Discrepancy28"].ToString();
            txt_Discrepancy_29.Text = dt.Rows[0]["Discrepancy29"].ToString();
            txt_Discrepancy_30.Text = dt.Rows[0]["Discrepancy30"].ToString();
            txt_Discrepancy_31.Text = dt.Rows[0]["Discrepancy31"].ToString();
            txt_Discrepancy_32.Text = dt.Rows[0]["Discrepancy32"].ToString();
            txt_Discrepancy_33.Text = dt.Rows[0]["Discrepancy33"].ToString();
            txt_Discrepancy_34.Text = dt.Rows[0]["Discrepancy34"].ToString();
            txt_Discrepancy_35.Text = dt.Rows[0]["Discrepancy35"].ToString();

            if (dt.Rows[0]["SwiftType"].ToString() == "MT499")
            {
                rdb_MT499.Checked = true;
                if (dt.Rows[0]["ProtestFlag"].ToString() == "Y")
                {
                    cb_Protest.Checked = true;
                }
            }
            else if (dt.Rows[0]["SwiftType"].ToString() == "MT734")
            {
                rdb_MT734.Checked = true;
            }
            else if (dt.Rows[0]["SwiftType"].ToString() == "MT799")
            {
                rdb_MT799.Checked = true;
            }
            else if (dt.Rows[0]["SwiftType"].ToString() == "MT999")
            {
                rdb_MT999.Checked = true;
                if (dt.Rows[0]["ProtestFlag"].ToString() == "Y")
                {
                    cb_Protest.Checked = true;
                }
            }
            else
            {
                rbd_None.Checked = true;
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


            ddl_Doc_Scrutiny.SelectedValue = dt.Rows[0]["Document_Scrutiny"].ToString();
            if (ddl_Doc_Scrutiny.SelectedValue == "2")
            {
                panel_AddDiscrepancy.Visible = true;
            }
            else
            {
                panel_AddDiscrepancy.Visible = false;
            }
            if (dt.Rows[0]["Special_Instruction_Type"].ToString() == "NONE")
            {
                rdb_SP_Instr_None.Checked = true;
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

            // instructions are hard coded as per doctype in ToggleDocType()

            txt_Instructions1.Text = dt.Rows[0]["Instructions1"].ToString();
            txt_Instructions2.Text = dt.Rows[0]["Instructions2"].ToString();
            txt_Instructions3.Text = dt.Rows[0]["Instructions3"].ToString();
            txt_Instructions4.Text = dt.Rows[0]["Instructions4"].ToString();
            txt_Instructions5.Text = dt.Rows[0]["Instructions5"].ToString();


            ddlPaymentSwift56.SelectedValue = dt.Rows[0]["Pay_swift_Type_56"].ToString();
            ddlPaymentSwift57.SelectedValue = dt.Rows[0]["Pay_swift_Type_57"].ToString();
            ddlPaymentSwift58.SelectedValue = dt.Rows[0]["Pay_swift_Type_58"].ToString();

            PaymentSwiftselection();
            txt_PaymentSwift56ACC_No.Text = dt.Rows[0]["Pay_swift_56ACC_No"].ToString();
            txt_PaymentSwift56A.Text = dt.Rows[0]["Pay_swift_56A"].ToString();
            txt_PaymentSwift56D_name.Text = dt.Rows[0]["Pay_swift_56D_name"].ToString();
            txt_PaymentSwift56D_Address.Text = dt.Rows[0]["Pay_swift_56D_Address"].ToString();

            txt_PaymentSwift57ACC_No.Text = dt.Rows[0]["Pay_swift_57ACC_No"].ToString();
            txt_PaymentSwift57A.Text = dt.Rows[0]["Pay_swift_57A"].ToString();
            txt_PaymentSwift57D_name.Text = dt.Rows[0]["Pay_swift_57D_name"].ToString();
            txt_PaymentSwift57D_Address.Text = dt.Rows[0]["Pay_swift_57D_Address"].ToString();

            txt_PaymentSwift58ACC_No.Text = dt.Rows[0]["Pay_swift_58ACC_No"].ToString();
            txt_PaymentSwift58A.Text = dt.Rows[0]["Pay_swift_58A"].ToString();
            txt_PaymentSwift58D_name.Text = dt.Rows[0]["Pay_swift_58D_name"].ToString();
            txt_PaymentSwift58D_Address.Text = dt.Rows[0]["Pay_swift_58D_Address"].ToString();
            txt_PaymentSwift58D_Address2.Text = dt.Rows[0]["Pay_swift_58D_Address2"].ToString();
            txt_PaymentSwift58D_Address3.Text = dt.Rows[0]["Pay_swift_58D_Address3"].ToString();
            txt_PaymentSwift58D_Address4.Text = dt.Rows[0]["Pay_swift_58D_Address4"].ToString();

            if (dt.Rows[0]["OwnLCDiscount_Type"].ToString() == "Y")
            {
                rdb_ownLCDiscount_No.Checked = false;
                rdb_ownLCDiscount_Yes.Checked = true;
            }
            if (dt.Rows[0]["OwnLCDiscount_Type"].ToString() == "N")
            {
                rdb_ownLCDiscount_Yes.Checked = false;
                rdb_ownLCDiscount_No.Checked = true;
            }
            hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
            ddl_Stamp_Duty_Charges_Curr.SelectedValue = dt.Rows[0]["Stamp_Duty_Charges_Curr"].ToString();
            txt_Stamp_Duty_Charges_ExRate.Text = dt.Rows[0]["Stamp_Duty_Charges_ExRate"].ToString();
            txt_Stamp_Duty_Charges_Amount.Text = dt.Rows[0]["Stamp_Duty_Charges_Amount"].ToString();

            //AML Tab
            txtAMLDrawee.Text = dt.Rows[0]["AML_Drawee"].ToString();
            txtAMLDrawer.Text = dt.Rows[0]["AML_Drawer"].ToString();
            txtAMLNagoRemiBank.Text = dt.Rows[0]["AML_NagoRemiBank"].ToString();
            //txtAMLSwiftCode.Text = dt.Rows[0]["AML_SwiftCode"].ToString();
            txtAMLCommodity.Text = dt.Rows[0]["AML_Commodity"].ToString();
            txtAMLVessel.Text = dt.Rows[0]["AML_Vessel"].ToString();
            txtAMLFromPort.Text = dt.Rows[0]["AML_FromPort"].ToString();
            txtAMLCountry.Text = dt.Rows[0]["AML_Country"].ToString();
            txtCountryOfOrigin.Text = dt.Rows[0]["AML_Country_Of_Origin"].ToString();
            txtAML1.Text = dt.Rows[0]["AML_1"].ToString();
            txtAML2.Text = dt.Rows[0]["AML_2"].ToString();
            txtAML3.Text = dt.Rows[0]["AML_3"].ToString();
            txtAML4.Text = dt.Rows[0]["AML_4"].ToString();
            txtAML5.Text = dt.Rows[0]["AML_5"].ToString();
            txtAML6.Text = dt.Rows[0]["AML_6"].ToString();
            txtAML7.Text = dt.Rows[0]["AML_7"].ToString();
            txtAML8.Text = dt.Rows[0]["AML_8"].ToString();
            txtAML9.Text = dt.Rows[0]["AML_9"].ToString();
            txtAML10.Text = dt.Rows[0]["AML_10"].ToString();
            txtAML11.Text = dt.Rows[0]["AML_11"].ToString();
            txtAML12.Text = dt.Rows[0]["AML_12"].ToString();
            txtAML13.Text = dt.Rows[0]["AML_13"].ToString();
            txtAML14.Text = dt.Rows[0]["AML_14"].ToString();
            txtAML15.Text = dt.Rows[0]["AML_15"].ToString();
            txtAML16.Text = dt.Rows[0]["AML_16"].ToString();
            txtAML17.Text = dt.Rows[0]["AML_17"].ToString();
            txtAML18.Text = dt.Rows[0]["AML_18"].ToString();
            txtAML19.Text = dt.Rows[0]["AML_19"].ToString();
            txtAML20.Text = dt.Rows[0]["AML_20"].ToString();
            txtAML21.Text = dt.Rows[0]["AML_21"].ToString();
            txtAML22.Text = dt.Rows[0]["AML_22"].ToString();
            txtAML23.Text = dt.Rows[0]["AML_23"].ToString();
            txtAML24.Text = dt.Rows[0]["AML_24"].ToString();
            txtAML25.Text = dt.Rows[0]["AML_25"].ToString();
            txtAML26.Text = dt.Rows[0]["AML_26"].ToString();
            txtAML27.Text = dt.Rows[0]["AML_27"].ToString();
            txtAML28.Text = dt.Rows[0]["AML_28"].ToString();
            txtAML29.Text = dt.Rows[0]["AML_29"].ToString();
            txtAML30.Text = dt.Rows[0]["AML_30"].ToString();

            if (dt.Rows[0]["Special_Case"].ToString() == "Y")
            {
                rowRiskCust.Visible = true;
                chkSpecialCase.Checked = true;
                txtRiskCust.Text = dt.Rows[0]["Risk_Cust"].ToString();
                txtSettelementAcNo.Text = dt.Rows[0]["Settelement_AC_No"].ToString();
                txtRiskCustAbbr.Text = dt.Rows[0]["Risk_Cust_Abbr"].ToString();

                lblRiskCust.Style.Add("visibility", "visible");
                txtRiskCust.Style.Add("visibility", "visible");
                lblSettelementAcNo.Style.Add("visibility", "visible");
                txtSettelementAcNo.Style.Add("visibility", "visible");
                lblRiskCustAbbr.Style.Add("visibility", "visible");
                txtRiskCustAbbr.Style.Add("visibility", "visible");
            }
            if (dt.Rows[0]["LodgCumAcc"].ToString() == "Y")
            {
                chkLodgCumAcc.Checked = true;
            }
            else
            {
                chkLodgCumAcc.Checked = false;
            }

            txtChargesClaimed7341.Text = dt.Rows[0]["ChargesClaimed7341"].ToString();
            txtChargesClaimed7342.Text = dt.Rows[0]["ChargesClaimed7342"].ToString();
            txtChargesClaimed7343.Text = dt.Rows[0]["ChargesClaimed7343"].ToString();
            txtChargesClaimed7344.Text = dt.Rows[0]["ChargesClaimed7344"].ToString();
            txtChargesClaimed7345.Text = dt.Rows[0]["ChargesClaimed7345"].ToString();
            txtChargesClaimed7346.Text = dt.Rows[0]["ChargesClaimed7346"].ToString();

            ddlTotalAmountClaimed734.SelectedValue = dt.Rows[0]["TotalAmountClaimed734"].ToString();
            txtDate734.Text = dt.Rows[0]["Date734"].ToString();
            txtCurrency734.Text = dt.Rows[0]["Currency734"].ToString();
            txtAmount734.Text = dt.Rows[0]["Amount734"].ToString();
            TotalAmountClaimedChange();

            ddlAccountWithBank734.SelectedValue = dt.Rows[0]["AccountWithBank734"].ToString();
            txtAccountWithBank734AccountNo.Text = dt.Rows[0]["AccountWithBank734AccountNo"].ToString();
            txtAccountWithBank734PartyIdentifier.Text = dt.Rows[0]["AccountWithBankPartyIdentifier"].ToString();
            txtAccountWithBank734SwiftCode.Text = dt.Rows[0]["AccountWithBank734SwiftCode"].ToString();
            txtAccountWithBank734Location.Text = dt.Rows[0]["AccountWithBank734Location"].ToString();
            txtAccountWithBank734Name.Text = dt.Rows[0]["AccountWithBank734Name"].ToString();
            txtAccountWithBank734Address1.Text = dt.Rows[0]["AccountWithBank734Address1"].ToString();
            txtAccountWithBank734Address2.Text = dt.Rows[0]["AccountWithBank734Address2"].ToString();
            txtAccountWithBank734Address3.Text = dt.Rows[0]["AccountWithBank734Address3"].ToString();
            AccountWithBankChange();

            txtSendertoReceiverInformation7341.Text = dt.Rows[0]["SendertoReceiverInformation7341"].ToString();
            txtSendertoReceiverInformation7342.Text = dt.Rows[0]["SendertoReceiverInformation7342"].ToString();
            txtSendertoReceiverInformation7343.Text = dt.Rows[0]["SendertoReceiverInformation7343"].ToString();
            txtSendertoReceiverInformation7344.Text = dt.Rows[0]["SendertoReceiverInformation7344"].ToString();
            txtSendertoReceiverInformation7345.Text = dt.Rows[0]["SendertoReceiverInformation7345"].ToString();
            txtSendertoReceiverInformation7346.Text = dt.Rows[0]["SendertoReceiverInformation7346"].ToString();

            ddl_DisposalOfDoc.SelectedValue = dt.Rows[0]["Disposal_of_documents_Code"].ToString();

        }
        else
        {

        }
    }
    private void fillLCDetails()
    {
        string _query = "TF_IMP_FillLCDetails";
        string _currency = "";
        string _commodity = "";
        string _country = "";

        string _Qcurrency = "TF_IMP_CheckCurrencyInMaster";
        string _Qcommodity = "TF_IMP_CheckCommodityInMaster";
        string _Qcountry = "TF_IMP_CheckCountryInMaster";

        SqlParameter p1 = new SqlParameter("@LCNo", txt_LC_No.Text.Trim());
        SqlParameter p2 = new SqlParameter("@RefNo", txtDocNo.Text.Trim());
        SqlParameter p3 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
        SqlParameter p4 = new SqlParameter("@LocalForeign", lblForeign_Local.Text.ToUpper());
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            lblLCDesc.Text = dt.Rows[0]["BalAmount"].ToString();
            hdnLCREF1.Value = dt.Rows[0]["Ref1"].ToString();
            hdnLCREF2.Value = dt.Rows[0]["Ref2"].ToString();
            hdnLCREF3.Value = dt.Rows[0]["Ref3"].ToString();
            hdnLCCurrency.Value = dt.Rows[0]["Currency"].ToString();
            hdnLCAppNo.Value = dt.Rows[0]["ApplNo"].ToString();
            hdnLCCountry.Value = dt.Rows[0]["Country"].ToString();
            hdnLCCommCode.Value = dt.Rows[0]["CommCode"].ToString();
            hdnExpiryDate.Value = dt.Rows[0]["ExpiryDate"].ToString();
            hdnLCRiskCust.Value = dt.Rows[0]["RiskCustomer"].ToString();
            hdnLCSettlementAccNo.Value = dt.Rows[0]["SettlementAcNo"].ToString();
            hdnLCSpCustAbbr.Value = dt.Rows[0]["CustAbbr1"].ToString();

            txtRiskCust.Text = dt.Rows[0]["RiskCustomer"].ToString();
            txtSettelementAcNo.Text = dt.Rows[0]["SettlementAcNo"].ToString();
            txtRiskCustAbbr.Text = dt.Rows[0]["CustAbbr1"].ToString();

            SqlParameter pcurrency = new SqlParameter("@CurrencyCode", dt.Rows[0]["Currency"].ToString());
            _currency = objData.SaveDeleteData(_Qcurrency, pcurrency);
            SqlParameter pcommodity = new SqlParameter("@CommodityCode", dt.Rows[0]["CommCode"].ToString());
            _commodity = objData.SaveDeleteData(_Qcommodity, pcommodity);
            SqlParameter pcountry = new SqlParameter("@CountryCode", dt.Rows[0]["Country"].ToString());
            _country = objData.SaveDeleteData(_Qcountry, pcountry);
            if (hdnExpiryDate.Value == "Yes")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('This LC has expired.')", true);
            }
            if (_currency == "false")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('LC Currency Code " + dt.Rows[0]["Currency"].ToString() + " does not exist in LMCC Master Please update.')", true);
            }
            if (_commodity == "false")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('LC Commodity Code " + dt.Rows[0]["CommCode"].ToString() + " does not exist in LMCC Master Please update.')", true);
            }
            if (_country == "false")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('LC Country Code " + dt.Rows[0]["Country"].ToString() + " does not exist in LMCC Master Please update.')", true);
            }
        }
        else
        {
            if (lblCollection_Lodgment_UnderLC.Text == "Lodgment_Under_LC")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('Invalid LC No for this customer.')", true);
            }
        }
    }
    protected void MakeReadOnly()
    {
        txtDocNo.Enabled = false;
        txtDateReceived.Enabled = false;
        txtLogdmentDate.Enabled = false;
        txtCustomer_ID.Enabled = false;
        txt_AcceptanceDate.Enabled = false;
        txtBillAmount.Enabled = false;
        ddl_Doc_Currency.Enabled = false;
        txt_LC_No.Enabled = false;
        rbtDP.Enabled = false;
        rbtDA.Enabled = false;
        ddlTenor.Enabled = false;
        txtTenor.Enabled = false;
        ddlTenor_Days_From.Enabled = false;
        txtTenor_Description.Enabled = false;
        txtBOExchange.Enabled = false;
        txtDueDate.Enabled = false;
        ddl_Nego_Remit_Bank.Enabled = false;
        txtNego_Remit_Bank.Enabled = false;
        txt_Their_Ref_no.Enabled = false;
        txtNego_Date.Enabled = false;
        txtAcwithInstitution.Enabled = false;
        txtReimbursingbank.Enabled = false;
        ddlDrawer.Enabled = false;
        txt_Inv_No.Enabled = false;
        txt_Inv_Date.Enabled = false;
        ddl_Commodity.Enabled = false;
        txtCommodityDesc.Enabled = false;
        txt_CountryOfOrigin.Enabled = false;
        ddlCountryCode.Enabled = false;
        txtShippingDate.Enabled = false;
        txtDocFirst1.Enabled = false;
        txtDocFirst2.Enabled = false;
        txtDocFirst3.Enabled = false;
        txtVesselName.Enabled = false;
        txtDocSecond1.Enabled = false;
        txtDocSecond2.Enabled = false;
        txtDocSecond3.Enabled = false;
        txtFromPort.Enabled = false;
        txtToPort.Enabled = false;
        ddl_Interest_Currency.Enabled = false;
        txt_Interest_Amount.Enabled = false;
        ddl_Comission_Currency.Enabled = false;
        txtComissionAmount.Enabled = false;
        ddl_Other_Currency.Enabled = false;
        txtOther_amount.Enabled = false;
        ddl_Their_Commission_Currency.Enabled = false;
        ddlDrawer.Enabled = false;
        txtTheirCommission_Amount.Enabled = false;
        txt_Total_Bill_Amt.Enabled = false;

        txt_Discrepancy_1.Enabled = false;
        txt_Discrepancy_2.Enabled = false;
        txt_Discrepancy_3.Enabled = false;
        txt_Discrepancy_4.Enabled = false;
        txt_Discrepancy_5.Enabled = false;
        txt_Discrepancy_6.Enabled = false;
        txt_Discrepancy_7.Enabled = false;
        txt_Discrepancy_8.Enabled = false;
        txt_Discrepancy_9.Enabled = false;
        txt_Discrepancy_10.Enabled = false;
        txt_Discrepancy_11.Enabled = false;
        txt_Discrepancy_12.Enabled = false;
        txt_Discrepancy_13.Enabled = false;
        txt_Discrepancy_14.Enabled = false;
        txt_Discrepancy_15.Enabled = false;
        txt_Discrepancy_16.Enabled = false;
        txt_Discrepancy_17.Enabled = false;
        txt_Discrepancy_18.Enabled = false;
        txt_Discrepancy_19.Enabled = false;
        txt_Discrepancy_20.Enabled = false;
        txt_Discrepancy_21.Enabled = false;
        txt_Discrepancy_22.Enabled = false;
        txt_Discrepancy_23.Enabled = false;
        txt_Discrepancy_24.Enabled = false;
        txt_Discrepancy_25.Enabled = false;
        txt_Discrepancy_26.Enabled = false;
        txt_Discrepancy_27.Enabled = false;
        txt_Discrepancy_28.Enabled = false;
        txt_Discrepancy_29.Enabled = false;
        txt_Discrepancy_30.Enabled = false;
        txt_Discrepancy_31.Enabled = false;
        txt_Discrepancy_32.Enabled = false;
        txt_Discrepancy_33.Enabled = false;
        txt_Discrepancy_34.Enabled = false;
        txt_Discrepancy_35.Enabled = false;

        rbd_None.Enabled = false;
        rdb_MT499.Enabled = false;
        rdb_MT734.Enabled = false;
        rdb_MT799.Enabled = false;
        rdb_MT999.Enabled = false;
        cb_Protest.Enabled = false;

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

        ddl_Doc_Scrutiny.Enabled = false;
        rdb_SP_Instr_None.Enabled = false;
        rdb_SP_Instr_Other.Enabled = false;
        rdb_SP_Instr_Annexure.Enabled = false;
        rdb_SP_Instr_On_Sight.Enabled = false;
        rdb_SP_Instr_On_Date.Enabled = false;

        txt_SP_Instructions1.Enabled = false;
        txt_SP_Instructions2.Enabled = false;
        txt_SP_Instructions3.Enabled = false;
        txt_SP_Instructions4.Enabled = false;
        txt_SP_Instructions5.Enabled = false;

        rdb_ownLCDiscount_Yes.Enabled = false;
        rdb_ownLCDiscount_No.Enabled = false;

        ddlPaymentSwift56.Enabled = false;
        ddlPaymentSwift57.Enabled = false;
        ddlPaymentSwift58.Enabled = false;

        txt_PaymentSwift56ACC_No.Enabled = false;
        txt_PaymentSwift56A.Enabled = false;
        txt_PaymentSwift56D_name.Enabled = false;
        txt_PaymentSwift56D_Address.Enabled = false;

        txt_PaymentSwift57ACC_No.Enabled = false;
        txt_PaymentSwift57A.Enabled = false;
        txt_PaymentSwift57D_name.Enabled = false;
        txt_PaymentSwift57D_Address.Enabled = false;

        txt_PaymentSwift58ACC_No.Enabled = false;
        txt_PaymentSwift58A.Enabled = false;
        txt_PaymentSwift58D_name.Enabled = false;
        txt_PaymentSwift58D_Address.Enabled = false;
        txt_PaymentSwift58D_Address2.Enabled = false;
        txt_PaymentSwift58D_Address3.Enabled = false;
        txt_PaymentSwift58D_Address4.Enabled = false;

        /////Button Make Disabled////////////

        txt_Stamp_Duty_Charges_ExRate.Enabled = false;
        btn_NarrativeList.Enabled = false;

        //AML Tab
        txtAMLDrawee.Enabled = false;
        txtAMLDrawer.Enabled = false;
        txtAMLNagoRemiBank.Enabled = false;
        //txtAMLSwiftCode.Enabled = false;
        txtAMLCommodity.Enabled = false;
        txtAMLVessel.Enabled = false;
        txtAMLFromPort.Enabled = false;
        txtAMLCountry.Enabled = false;
        txtCountryOfOrigin.Enabled = false;
        txtAML1.Enabled = false;
        txtAML2.Enabled = false;
        txtAML3.Enabled = false;
        txtAML4.Enabled = false;
        txtAML5.Enabled = false;
        txtAML6.Enabled = false;
        txtAML7.Enabled = false;
        txtAML8.Enabled = false;
        txtAML9.Enabled = false;
        txtAML10.Enabled = false;
        txtAML11.Enabled = false;
        txtAML12.Enabled = false;
        txtAML13.Enabled = false;
        txtAML14.Enabled = false;
        txtAML15.Enabled = false;
        txtAML16.Enabled = false;
        txtAML17.Enabled = false;
        txtAML18.Enabled = false;
        txtAML19.Enabled = false;
        txtAML20.Enabled = false;
        txtAML21.Enabled = false;
        txtAML22.Enabled = false;
        txtAML23.Enabled = false;
        txtAML24.Enabled = false;
        txtAML25.Enabled = false;
        txtAML26.Enabled = false;
        txtAML27.Enabled = false;
        txtAML28.Enabled = false;
        txtAML29.Enabled = false;
        txtAML30.Enabled = false;

        chkSpecialCase.Enabled = false;
        txtRiskCust.Enabled = false;
        txtSettelementAcNo.Enabled = false;
        txtRiskCustAbbr.Enabled = false;
        chkLodgCumAcc.Enabled = false;
    }
    private void ToggleDocType(string DocType, string DocFLC_ILC_Type, string lblDocument_Scrutiny)
    {
        switch (DocType)
        {
            case "ICA": //Collection_Sight
                tbSwift.Visible = false;
                cb_Protest.Visible = true;
                lblCollection_Lodgment_UnderLC.Text = "Collection";
                lblSight_Usance.Text = "Sight";
                panal_LC_No.Visible = false;
                panelDP_DA.Visible = true;
                panal_Scrutiny.Visible = false;
                panal_Stamp_Duty_Charges.Visible = true;
                rbtDP.Checked = true;
                ddlTenor.SelectedValue = "1";
                ddlTenor.Enabled = false;
                txtTenor.Enabled = false;
                txtTenor_Description.Enabled = false;
                lbl_AcceptanceDate.Text = "Payment Date :";
                txt_AcceptanceDate.Enabled = false;
                CPE_Discrepancy.Collapsed = true;
                btn_DiscrepancyList.Enabled = false;
                rdb_MT734.Visible = false;
                rdb_MT799.Visible = false;
                PanelPaymentSwiftDetail.Visible = true;
                if (DocFLC_ILC_Type == "FLC")
                {//Foreign ICA
                    lblForeign_Local.Text = "FOREIGN";

                    txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE";
                    txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE";
                    txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                    txt_Instructions4.Text = "KINDLY PROVIDE YOUR INSTRUCTIONS FOR SETTLEMENT ON";
                    txt_Instructions5.Text = "OR BEFORE BY " + txt_AcceptanceDate.Text;
                }
                else if (DocFLC_ILC_Type == "ILC")
                {//Local ICA
                    lblForeign_Local.Text = "LOCAL";

                    txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                    txt_Instructions2.Text = "KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS";
                    txt_Instructions3.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    txt_Instructions4.Text = "";
                    txt_Instructions5.Text = "";
                }
                break;

            case "ICU": //Collection_Usance
                tbSwift.Visible = false;
                cb_Protest.Visible = true;
                lblCollection_Lodgment_UnderLC.Text = "Collection";
                lblSight_Usance.Text = "Usance";
                panal_LC_No.Visible = false;
                panelDP_DA.Visible = true;
                panal_Scrutiny.Visible = false;
                panal_Stamp_Duty_Charges.Visible = true;
                lbl_AcceptanceDate.Text = "Exp Acceptance Date :";
                rbtDA.Checked = true;
                txt_AcceptanceDate.Enabled = false;
                btn_DiscrepancyList.Enabled = false;
                rdb_MT734.Visible = false;
                rdb_MT799.Visible = false;
                PanelPaymentSwiftDetail.Visible = true;
                if (DocFLC_ILC_Type == "FLC")
                {//Foreign ICU
                    lblForeign_Local.Text = "FOREIGN";

                    txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE";
                    txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE";
                    txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                    txt_Instructions4.Text = "KINDLY PROVIDE YOUR ACCEPTANCE BY " + txt_AcceptanceDate.Text;
                    txt_Instructions5.Text = "";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {//Local ICU
                    lblForeign_Local.Text = "LOCAL";

                    txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                    txt_Instructions2.Text = "KINDLY PROVIDE YOUR ACCEPTANCE BY " + txt_AcceptanceDate.Text;
                    txt_Instructions3.Text = "";
                    txt_Instructions4.Text = "";
                    txt_Instructions5.Text = "";
                }
                break;

            case "IBA": //LodgmentUnderLC_Sight
                tbSwift.Visible = true;
                cb_Protest.Visible = false;
                lblCollection_Lodgment_UnderLC.Text = "LodgmentUnderLC";
                lblSight_Usance.Text = "Sight";
                panelDP_DA.Visible = false;
                panal_LC_No.Visible = true;
                panal_Stamp_Duty_Charges.Visible = false;
                //rbtDP.Checked = true;
                ddlTenor.SelectedValue = "1";
                ddlTenor.Enabled = false;
                txtTenor.Enabled = false;
                txtTenor_Description.Enabled = false;
                lbl_AcceptanceDate.Text = "Payment Date :";
                txt_AcceptanceDate.Enabled = false;
                rdb_MT499.Visible = false;
                //PanelPaymentSwiftDetail.Visible = false;
                //cb_Protest.Visible = false;
                if (DocFLC_ILC_Type == "FLC" && lblDocument_Scrutiny == "1")
                {
                    //Foreign IBA Without DISCREPANCY
                    lblForeign_Local.Text = "FOREIGN";

                    txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                    txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                    txt_Instructions3.Text = "THE DOCUMENTS AGAINST PAYMENT. KINDLY PROVIDE YOUR";
                    txt_Instructions4.Text = "SETTLEMENT INSTRUCTIONS AS PER UCP 600";
                    txt_Instructions5.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                }
                if (DocFLC_ILC_Type == "FLC" && lblDocument_Scrutiny == "2")
                {
                    //Foreign IBA With DISCREPANCY
                    lblForeign_Local.Text = "FOREIGN";

                    txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                    txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE WE DELIVER";
                    txt_Instructions3.Text = "THE DOCUMENTS AGAINST PAYMENT. IF THE DISCREPANCIES ARE";
                    txt_Instructions4.Text = "ACCEPTABLE,KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS AS";
                    txt_Instructions5.Text = "PER ARTICLE 16(D) OF UCP 600 BY(5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                }
                else if (DocFLC_ILC_Type == "ILC" && lblDocument_Scrutiny == "1")
                {//Local IBA Without DISCREPANCY
                    lblForeign_Local.Text = "LOCAL";

                    txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                    txt_Instructions2.Text = "KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS ";
                    txt_Instructions3.Text = "AS PER UCP 600 BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    txt_Instructions4.Text = "";
                    txt_Instructions5.Text = "";
                }
                else if (DocFLC_ILC_Type == "ILC" && lblDocument_Scrutiny == "2")
                {//Local IBA With DISCREPANCY
                    lblForeign_Local.Text = "LOCAL";

                    txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT. IF THE";
                    txt_Instructions2.Text = "DISCREPANCIES ARE ACCEPTABLE,KINDLY PROVIDE YOUR";
                    txt_Instructions3.Text = "SETTLEMENT INSTRUCTIONS AS PER ARTICLE 16(D) OF UCP 600.";
                    txt_Instructions4.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    txt_Instructions5.Text = "";
                }
                if (hdnBRONo.Value != "" || hdnSGDocNo.Value != "")
                {
                    txt_Instructions1.Text = "AS PER RBI GUIDELINES BILL OF ENTRIES TO BE";
                    txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE.";
                    txt_Instructions3.Text = "WE DELIVER DOCUMENTS AGAINST PAYMENT.";
                    txt_Instructions4.Text = "";
                    txt_Instructions5.Text = "";
                }
                break;

            case "ACC": //LodgmentUnderLC_Usance
                tbSwift.Visible = true;
                cb_Protest.Visible = false;
                lblCollection_Lodgment_UnderLC.Text = "LodgmentUnderLC";
                lblSight_Usance.Text = "Usance";
                //rbtDA.Checked = true;
                panal_Stamp_Duty_Charges.Visible = false;
                panelDP_DA.Visible = false;
                panal_LC_No.Visible = true;
                rdb_MT499.Visible = false;
                //cb_Protest.Visible = false;
                lbl_AcceptanceDate.Text = "Exp Acceptance Date :";
                //PanelPaymentSwiftDetail.Visible = false;
                if (DocFLC_ILC_Type == "ILC" && lblDocument_Scrutiny == "1")
                {//Local ACC Without DISCREPANCY
                    lblForeign_Local.Text = "LOCAL";

                    txt_Instructions1.Text = "KINDLY PROVIDE YOUR ACCEPTANCE AS PER UCP 600";
                    txt_Instructions2.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                    txt_Instructions4.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                    txt_Instructions5.Text = "";
                }
                else if (DocFLC_ILC_Type == "ILC" && lblDocument_Scrutiny == "2")
                {//Local ACC With DISCREPANCY
                    lblForeign_Local.Text = "LOCAL";

                    txt_Instructions1.Text = "IF THE DISCREPANCIES ARE ACCEPTABLE,";
                    txt_Instructions2.Text = "KINDLY PROVIDE YOUR ACCEPTANCE AS PER";
                    txt_Instructions3.Text = "ARTICLE 16(D) OF UCP 600 BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    txt_Instructions4.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                    txt_Instructions5.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                }
                else if (DocFLC_ILC_Type == "FLC" && lblDocument_Scrutiny == "1")
                {//Foreign ACC Without DISCREPANCY
                    lblForeign_Local.Text = "FOREIGN";

                    txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                    txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                    txt_Instructions3.Text = "THE DOCUMENTS AGAINST ACCEPTANCE. KINDLY PROVIDE";
                    txt_Instructions4.Text = "YOUR ACCEPTANCE AS PER UCP 600 BY(5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    txt_Instructions5.Text = "";
                }
                else if (DocFLC_ILC_Type == "FLC" && lblDocument_Scrutiny == "2")
                {//Foreign ACC With DISCREPANCY
                    lblForeign_Local.Text = "FOREIGN";

                    txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                    txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                    txt_Instructions3.Text = "THE DOCUMENTS AGAINST ACCEPTANCE. IF THE DISCREPANCIES";
                    txt_Instructions4.Text = "ARE ACCEPTED BY YOU KINDLY PROVIDE YOUR ACCEPTANCE";
                    txt_Instructions5.Text = "AS PER ARTICLE 16(D) OF UCP 600, BY" + txt_AcceptanceDate.Text;
                }
                if (hdnBRONo.Value != "" || hdnSGDocNo.Value != "")
                {
                    txt_Instructions1.Text = "AS PER RBI GUIDELINES BILL OF ENTRIES TO BE";
                    txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE.";
                    txt_Instructions3.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                    txt_Instructions4.Text = "";
                    txt_Instructions5.Text = "";
                }
                break;
        }
    }
    private void fillCustomerMasterDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        SqlParameter p2 = new SqlParameter("@BranchName", hdnBranchName.Value.ToString());
        p1.Value = txtCustomer_ID.Text;
        string _query = "TF_IMP_GetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            lblCustomerDesc.ToolTip = lblCustomerDesc.Text;
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            if (lblCustomerDesc.Text.Length > 30)
            {
                lblCustomerDesc.Text = lblCustomerDesc.Text.Substring(0, 30) + "...";
            }
            fill_Drawer();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Select Customer of Selected Branch.')", true);
            txtCustomer_ID.Text = "";
            lblCustomerDesc.Text = "";
        }

    }
    private void fillNego_Remit_BankDetails()
    {
        try
        {
            string _query = "";
            string _RMAQuery = "";
            TF_DATA objData = new TF_DATA();
            SqlParameter P_BankCode = new SqlParameter("@bankcode", SqlDbType.VarChar);
            P_BankCode.Value = txtNego_Remit_Bank.Text;
            SqlParameter P_SwiftCode = new SqlParameter("@SwiftCode", SqlDbType.VarChar);
            P_SwiftCode.Value = hdnNegoRemiSwiftCode.Value;
            if (ddl_Nego_Remit_Bank.SelectedValue == "FOREIGN")
            {
                _query = "TF_IMP_OverseasBankMasterDetails";
            }
            else if (ddl_Nego_Remit_Bank.SelectedValue == "LOCAL")
            {
                _query = "TF_IMP_LocalBankMasterDetails";

                ddlPaymentSwift56.Enabled = false;
                ddlPaymentSwift57.Enabled = false;
                ddlPaymentSwift58.Enabled = false;
            }
            DataTable dt = objData.getData(_query, P_BankCode);
            if (dt.Rows.Count > 0)
            {
                //lbl_Nego_Remit_Bank.Text = dt.Rows[0]["shortName"].ToString().Trim();
                lbl_Nego_Remit_Bank.Text = "NAME";
                txtNego_Remit_Bank.Text = dt.Rows[0]["BankCode"].ToString().Trim();
                lbl_Nego_Remit_Bank.ToolTip = dt.Rows[0]["BankName"].ToString().Trim();

                if (ddl_Nego_Remit_Bank.SelectedValue == "FOREIGN")
                {
                    lbl_Nego_RemitSwift_IFSC.Text = "SWIFT";
                    lbl_Nego_RemitSwift_IFSC.ToolTip = dt.Rows[0]["SwiftCode"].ToString().Trim();
                }
                else if (ddl_Nego_Remit_Bank.SelectedValue == "LOCAL")
                {
                    lbl_Nego_RemitSwift_IFSC.Text = "IFSC";
                    lbl_Nego_RemitSwift_IFSC.ToolTip = dt.Rows[0]["IFSC_Code"].ToString().Trim();
                }
                lbl_Nego_Remit_Bank_Addr.Text = "ADDRESS";
                lbl_Nego_Remit_Bank_Addr.ToolTip = dt.Rows[0]["BankAddress"].ToString().Trim();
            }
            else
            {
                if (txtNego_Remit_Bank.Text != "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This Bank code is not part of Bank Master.')", true);
                }
                txtNego_Remit_Bank.Text = "";
                lbl_Nego_Remit_Bank.Text = "";
                lbl_Nego_Remit_Bank.ToolTip = "";

                lbl_Nego_RemitSwift_IFSC.Text = "";
                lbl_Nego_RemitSwift_IFSC.ToolTip = "";

                lbl_Nego_Remit_Bank_Addr.Text = "";
                lbl_Nego_Remit_Bank_Addr.ToolTip = "";

                txtAMLNagoRemiBank.Text = "";
                txtNego_Remit_Bank.Focus();
            }
            //if (dt.Rows.Count > 0)
            //{
            //    lbl_Nego_Remit_Bank.Text = dt.Rows[0]["BankName"].ToString().Trim();
            //    txtNego_Remit_Bank.Text = dt.Rows[0]["BankCode"].ToString().Trim();
            //    lbl_Nego_Remit_Bank.ToolTip = lbl_Nego_Remit_Bank.Text;
            //    if (lbl_Nego_Remit_Bank.Text.Length > 30)
            //    {
            //        lbl_Nego_Remit_Bank.Text = lbl_Nego_Remit_Bank.Text.Substring(0, 30) + "...";
            //        txtAMLNagoRemiBank.Text = dt.Rows[0]["BankName"].ToString().Trim();
            //    }
            //}
            //else
            //{
            //    if (txtNego_Remit_Bank.Text != "")
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This Bank code is not part of Bank Master.')", true);
            //    }
            //    txtNego_Remit_Bank.Text = "";
            //    lbl_Nego_Remit_Bank.Text = "";
            //    txtAMLNagoRemiBank.Text = "";
            //    txtNego_Remit_Bank.Focus();
            //}
            if (ddl_Nego_Remit_Bank.SelectedValue == "FOREIGN" && txtNego_Remit_Bank.Text.Trim() != "")
            {
                _RMAQuery = "TF_IMP_CheckSwiftCodeInRMAMaster";
                DataTable dtRMA = objData.getData(_RMAQuery, P_SwiftCode);
                if (dtRMA.Rows.Count > 0)
                {
                    if (dtRMA.Rows[0]["SStatus"].ToString().Trim() == "Enabled")
                    {
                        hdnMT999LC.Value = "MT999";
                    }
                    else
                    {
                        hdnMT999LC.Value = "MT999LC";
                        ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('This swift code [" + hdnNegoRemiSwiftCode.Value + "] is not Enabled.')", true);
                    }
                }
                else
                {
                    hdnMT999LC.Value = "MT999LC";
                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('This swift code [" + hdnNegoRemiSwiftCode.Value + "] does not exist in RMA Master.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void fillAcwithInstitutionDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtAcwithInstitution.Text;

        DataTable dt = objData.getData("TF_IMP_AccWithInstitutionDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblAcWithInstiBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
            lblAcWithInstiBankDesc.ToolTip = lblAcWithInstiBankDesc.Text;
            if (lblAcWithInstiBankDesc.Text.Length > 30)
            {
                lblAcWithInstiBankDesc.Text = lblAcWithInstiBankDesc.Text.Substring(0, 30) + "...";
            }
        }
        else
        {
            txtAcwithInstitution.Text = "";
            lblAcWithInstiBankDesc.Text = "";
        }

    }
    private void fillReimbursingbankDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtReimbursingbank.Text;

        DataTable dt = objData.getData("TF_IMP_Reimbursing_BankMasterDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lbl_Reimbursingbank.Text = dt.Rows[0]["BankName"].ToString().Trim();
            lbl_Reimbursingbank.ToolTip = lbl_Reimbursingbank.Text;
            if (lbl_Reimbursingbank.Text.Length > 30)
            {
                lbl_Reimbursingbank.Text = lbl_Reimbursingbank.Text.Substring(0, 30) + "...";
            }
        }
        else
        {
            txtReimbursingbank.Text = "";
            lbl_Reimbursingbank.Text = "";
        }

    }
    protected void fillCurrency()
    {

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_Currency_List");
        ddl_Doc_Currency.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddl_Doc_Currency.DataSource = dt.DefaultView;
            ddl_Doc_Currency.DataTextField = "C_Code";
            ddl_Doc_Currency.DataValueField = "C_Code";
            ddl_Doc_Currency.DataBind();

            ddl_Interest_Currency.DataSource = dt.DefaultView;
            ddl_Interest_Currency.DataTextField = "C_Code";
            ddl_Interest_Currency.DataValueField = "C_Code";
            ddl_Interest_Currency.DataBind();

            ddl_Other_Currency.DataSource = dt.DefaultView;
            ddl_Other_Currency.DataTextField = "C_Code";
            ddl_Other_Currency.DataValueField = "C_Code";
            ddl_Other_Currency.DataBind();

            ddl_Their_Commission_Currency.DataSource = dt.DefaultView;
            ddl_Their_Commission_Currency.DataTextField = "C_Code";
            ddl_Their_Commission_Currency.DataValueField = "C_Code";
            ddl_Their_Commission_Currency.DataBind();

            ddl_Comission_Currency.DataSource = dt.DefaultView;
            ddl_Comission_Currency.DataTextField = "C_Code";
            ddl_Comission_Currency.DataValueField = "C_Code";
            ddl_Comission_Currency.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddl_Doc_Currency.Items.Insert(0, li);
        ddl_Interest_Currency.Items.Insert(0, li);
        ddl_Other_Currency.Items.Insert(0, li);
        ddl_Their_Commission_Currency.Items.Insert(0, li);
        ddl_Comission_Currency.Items.Insert(0, li);

    }
    protected void fillCurrency_Description()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@currCode", ddl_Doc_Currency.SelectedValue.ToString());
        DataTable dt = objData.getData("TF_IMP_GetCurrDesc", p1);
        if (dt.Rows.Count > 0)
        {
            lbl_Doc_Currency.ToolTip = dt.Rows[0]["C_DESCRIPTION"].ToString();
            lbl_Doc_Currency.Text = dt.Rows[0]["C_DESCRIPTION"].ToString();
        }
        else
        {
            lbl_Doc_Currency.Text = "";
            ddl_Doc_Currency.ClearSelection();
        }
    }
    protected void fill_GBaseCommodity()
    {
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_GBaseCommodity_List");
        ddl_Commodity.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddl_Commodity.DataSource = dt.DefaultView;
            ddl_Commodity.DataTextField = "GBase_Commodity_Descr";
            ddl_Commodity.DataValueField = "Gbase_Commodity_ID";
            ddl_Commodity.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddl_Commodity.Items.Insert(0, li);


    }
    protected void fill_GBaseCommodity_Description()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CommodityID", ddl_Commodity.SelectedValue.ToString());
        DataTable dt = objData.getData("TF_IMP_GBaseCommodityDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCommodityDesc.Text = dt.Rows[0]["GBase_Commodity_Description"].ToString().Trim();
            lblCommodityDesc.ToolTip = lblCommodityDesc.Text;
            if (lblCommodityDesc.Text.Length > 10)
            {
                lblCommodityDesc.Text = lblCommodityDesc.Text.Substring(0, 10) + "...";
            }
        }
        else
        {
            lblCommodityDesc.Text = "";
            ddl_Commodity.ClearSelection();
        }
    }
    protected void fill_Country()
    {
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_Country_List");
        ddlCountryCode.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddlCountryCode.DataSource = dt.DefaultView;
            ddlCountryCode.DataTextField = "Country_Desc";
            ddlCountryCode.DataValueField = "CountryID";
            ddlCountryCode.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddlCountryCode.Items.Insert(0, li);


    }
    protected void fill_Country_Description()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CountryID", ddlCountryCode.SelectedValue.ToString());
        DataTable dt = objData.getData("TF_IMP_Country_Details", p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString().Trim();
            lblCountryDesc.ToolTip = lblCountryDesc.Text.ToString();

            DataTable dtCheck = objData.getData("TF_IMP_SanctionedCountry_details", p1);
            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Selected Country is in Sanctioned Country list.')", true);
                ddlCountryCode.Focus();
            }
        }
        else
        {
            lblCountryDesc.Text = "";
            ddlCountryCode.ClearSelection();
        }
    }
    protected void fill_DisposalOfDocument()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData("TF_IMP_DisposalOfDocument_List");
            ddl_DisposalOfDoc.Items.Clear();
            ListItem li = new ListItem();
            li.Value = "";
            if (dt.Rows.Count > 0)
            {
                li.Text = "Select";
                ddl_DisposalOfDoc.DataSource = dt.DefaultView;
                ddl_DisposalOfDoc.DataTextField = "Disposal_of_documents_Desc";
                ddl_DisposalOfDoc.DataValueField = "Disposal_of_documents_Code";
                ddl_DisposalOfDoc.DataBind();
            }
            else
            {
                li.Text = "No record(s) found";
            }
            ddl_DisposalOfDoc.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fill_Drawer()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CUST_ACCOUNT_NO", txtCustomer_ID.Text.ToString());
        DataTable dt = objData.getData("TF_IMP_Drawer_List", p1);
        ddlDrawer.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddlDrawer.DataSource = dt.DefaultView;
            ddlDrawer.DataTextField = "Drawer_NAME";
            ddlDrawer.DataValueField = "Drawer_ID";
            ddlDrawer.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddlDrawer.Items.Insert(0, li);


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_BOE_Checker_View.aspx", true);
    }
    protected void txtCustomer_ID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerMasterDetails();
    }
    protected void txtAcwithInstitution_TextChanged(object sender, EventArgs e)
    {
        fillAcwithInstitutionDetails();
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
            if (!chkLodgCumAcc.Checked)
            {
                GBaseFileCreation();
            }
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        Lei_Audit();
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        SqlParameter P_BRONo = new SqlParameter("@BRONo", hdnBRONo.Value.Trim());
        SqlParameter P_SGNo = new SqlParameter("@SGNo", hdnSGDocNo.Value.Trim());
        SqlParameter P_CheckedBy = new SqlParameter("@CheckBy", hdnUserName.Value);
        string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectBOEData", P_DocNo, P_Status, P_RejectReason, P_BRONo, P_SGNo, P_CheckedBy);

        Response.Redirect("TF_IMP_BOE_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        
        //ClearAllFields();
        //FillDetailsAfterSave(Result.Substring(7),_AR);

    }
    protected void rdb_SP_Instr_Other_CheckedChanged(object sender, EventArgs e)
    {
        txt_SP_Instructions1.Text = "";
        txt_SP_Instructions1.Focus();
    }
    protected void rdb_SP_Instr_Annexure_CheckedChanged(object sender, EventArgs e)
    {
        txt_SP_Instructions1.Text = "As Per Annexure";
    }
    protected void rdb_SP_Instr_On_Sight_CheckedChanged(object sender, EventArgs e)
    {
        txt_SP_Instructions1.Text = "Beneficiary to be paid on Sight basis.";
    }
    protected void rdb_SP_Instr_On_Date_CheckedChanged(object sender, EventArgs e)
    {
        txt_SP_Instructions1.Text = "Beneficiary to be paid on dated";
        txt_SP_Instructions1.Focus();
    }
    protected void ddl_Doc_Currency_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCurrency_Description();
        if (ddl_Doc_Currency.SelectedValue.ToString() != "0")
        {
            ddl_Comission_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
            ddl_Interest_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
            ddl_Other_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
            ddl_Their_Commission_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
        }
    }
    protected void ddl_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_GBaseCommodity_Description();
    }
    protected void ddlCountryCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_Country_Description();
    }
    protected void txtNego_Remit_Bank_TextChanged(object sender, EventArgs e)
    {
        fillNego_Remit_BankDetails();
    }
    protected void txtReimbursingbank_TextChanged(object sender, EventArgs e)
    {
        fillReimbursingbankDetails();
    }
    protected void ddl_Nego_Remit_Bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNego_Remit_Bank.Text = "";
        fillNego_Remit_BankDetails();
    }
    protected void btnDocAccPrev_Click(object sender, EventArgs e)
    {
        if (hdnDocType.Value == "ICA" || hdnDocType.Value == "ICU")
        {
            TabContainerMain.ActiveTabIndex = 1;
        }
        if (hdnDocType.Value == "IBA" || hdnDocType.Value == "ACC")
        {
            TabContainerMain.ActiveTabIndex = 2;
        }
    }
    protected void btnSwiftPrev_Click(object sender, EventArgs e)
    {

        if (hdnDocType.Value == "ICA" || hdnDocType.Value == "ICU")
        {
            TabContainerMain.ActiveTabIndex = 0;
        }
        if (hdnDocType.Value == "IBA" || hdnDocType.Value == "ACC")
        {
            TabContainerMain.ActiveTabIndex = 0;
        }
    }
    protected void btnSwiftNext_Click(object sender, EventArgs e)
    {

        if (hdnDocType.Value == "ICA" || hdnDocType.Value == "ICU")
        {
            TabContainerMain.ActiveTabIndex = 2;
        }
        if (hdnDocType.Value == "IBA" || hdnDocType.Value == "ACC")
        {
            TabContainerMain.ActiveTabIndex = 2;
        }
    }
    protected void btn_Instr_Prev_Click(object sender, EventArgs e)
    {

        if (hdnDocType.Value == "ICA" || hdnDocType.Value == "ICU")
        {
            TabContainerMain.ActiveTabIndex = 0;
        }
        if (hdnDocType.Value == "IBA" || hdnDocType.Value == "ACC")
        {
            TabContainerMain.ActiveTabIndex = 1;
        }
    }
    protected void btn_Instr_Next_Click(object sender, EventArgs e)
    {
        if (hdnDocType.Value == "ICA" || hdnDocType.Value == "ICU")
        {
            TabContainerMain.ActiveTabIndex = 2;
        }
        if (hdnDocType.Value == "IBA" || hdnDocType.Value == "ACC")
        {
            TabContainerMain.ActiveTabIndex = 3;
        }
    }
    protected void btnDocNext_Click(object sender, EventArgs e)
    {
        if (hdnDocType.Value == "ICA" || hdnDocType.Value == "ICU")
        {
            TabContainerMain.ActiveTabIndex = 1;
        }
        if (hdnDocType.Value == "IBA" || hdnDocType.Value == "ACC")
        {
            TabContainerMain.ActiveTabIndex = 1;
        }
    }
    //protected void btnupload_AML_Click(object sender, EventArgs e)
    //{
    //    if (FileUpload_AML.HasFile)
    //    {
    //        string FolderPath = Server.MapPath("~/Uploaded_Files/AML");

    //        if (!Directory.Exists(FolderPath))
    //        {
    //            Directory.CreateDirectory(FolderPath);
    //        }
    //        string FilePath = FolderPath + "\\" + txtDocNo.Text.ToString();
    //        FileUpload_AML.SaveAs(FilePath);
    //    }
    //}
    //protected void lnk_Swift_Download_Click(object sender, EventArgs e)
    //{
    //    string FileName = txtDocNo+"_"+;
    //    string FileType = ViewState["FileType"].ToString();
    //    string filePath = "~/TF_GeneratedFiles/IMPWH/" + FileType + "/" + FileName;
    //    Response.ContentType = "image/jpg";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
    //    Response.TransmitFile(Server.MapPath(filePath));
    //    Response.End();
    //}
    //protected void lnk_SFMS_Download_Click(object sender, EventArgs e)
    //{
    //    string FileName = ViewState["FileName"].ToString();
    //    string FileType = ViewState["FileType"].ToString();
    //    string filePath = "~/TF_GeneratedFiles/IMPWH/" + FileType + "/" + FileName;
    //    Response.ContentType = "image/jpg";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
    //    Response.TransmitFile(Server.MapPath(filePath));
    //    Response.End();
    //}

    //protected void ddlApproveReject_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    TF_DATA obj = new TF_DATA();
    //    SqlParameter P_DocNo = new SqlParameter("@DocNo", hdnDocNo.Value.Trim());
    //    string AR = "";
    //    if (ddlApproveReject.SelectedIndex == 1)
    //    {
    //        AR = "A";
    //    }
    //    if (ddlApproveReject.SelectedIndex == 2)
    //    {
    //        AR = "R";
    //    }
    //    SqlParameter P_Status = new SqlParameter("@Status", AR);
    //    SqlParameter P_RejectReason = new SqlParameter("@RejectReason", txtRejectReason.Text.Trim());
    //    string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectBOEData", P_DocNo, P_Status, P_RejectReason);
    //    if (Result.Substring(0, 7) == "Updated")
    //    {
    //        string script1 = "";
    //        if (ddlApproveReject.SelectedIndex == 1)
    //        {
    //            script1 = "alert('Record Approved with doc no. " + Result.Substring(7) + "')";
    //        }
    //        if (ddlApproveReject.SelectedIndex == 2)
    //        {
    //            script1 = "alert('Record Rejected with doc no. " + Result.Substring(7) + "')";
    //        }
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
    //        ClearAllFields();
    //        FillDetailsAfterSave();
    //    }
    //}
    protected void ddlPaymentSwift56_SelectedIndexChanged(object sender, EventArgs e)
    {
        PaymentSwiftselection();
    }
    protected void ddlPaymentSwift57_SelectedIndexChanged(object sender, EventArgs e)
    {
        PaymentSwiftselection();
    }
    protected void ddlPaymentSwift58_SelectedIndexChanged(object sender, EventArgs e)
    {
        PaymentSwiftselection();
    }
    public void PaymentSwiftselection()
    {
        if (ddlPaymentSwift56.SelectedValue == "56A")
        {
            panel56ACC_No.Visible = true;
            panel56A.Visible = true;
            panel56DName.Visible = false;
            panel56DAddress.Visible = false;
            txt_PaymentSwift56D_name.Text = "";
            txt_PaymentSwift56D_Address.Text = "";
        }
        else if (ddlPaymentSwift56.SelectedValue == "56D")
        {
            panel56ACC_No.Visible = true;
            panel56DName.Visible = true;
            panel56DAddress.Visible = true; panel56A.Visible = false;
            txt_PaymentSwift56A.Text = "";
        }
        else
        {
            panel56ACC_No.Visible = false;
            panel56A.Visible = false;
            panel56DName.Visible = false;
            panel56DAddress.Visible = false;
        }
        if (ddlPaymentSwift57.SelectedValue == "57A")
        {
            panel57ACC_No.Visible = true;
            panel57A.Visible = true;
            panel57DName.Visible = false;
            panel57DAddress.Visible = false;
            txt_PaymentSwift57D_name.Text = "";
            txt_PaymentSwift57D_Address.Text = "";
        }
        else if (ddlPaymentSwift57.SelectedValue == "57D")
        {
            panel57ACC_No.Visible = true;
            panel57DName.Visible = true;
            panel57DAddress.Visible = true;
            panel57A.Visible = false;
            txt_PaymentSwift57A.Text = "";
        }
        else
        {
            panel57ACC_No.Visible = false;
            panel57A.Visible = false;
            panel57DName.Visible = false;
            panel57DAddress.Visible = false;
        }
        if (ddlPaymentSwift58.SelectedValue == "58A")
        {
            panel58ACC_No.Visible = true;
            panel58A.Visible = true;
            panel58DName.Visible = false;
            panel58Address.Visible = false;
            panel58Address2.Visible = false;
            panel58Address3.Visible = false;
            panel58Address4.Visible = false;
            txt_PaymentSwift58D_name.Text = "";
            txt_PaymentSwift58D_Address.Text = "";
            txt_PaymentSwift58D_Address2.Text = "";
            txt_PaymentSwift58D_Address3.Text = "";
            txt_PaymentSwift58D_Address4.Text = "";
        }
        else if (ddlPaymentSwift58.SelectedValue == "58D")
        {
            panel58ACC_No.Visible = true;
            panel58DName.Visible = true;
            panel58Address.Visible = true;
            panel58Address2.Visible = true;
            panel58Address3.Visible = true;
            panel58Address4.Visible = true;
            panel58A.Visible = false;
            txt_PaymentSwift58A.Text = "";
        }
        else
        {
            panel58ACC_No.Visible = false;
            panel58A.Visible = false;
            panel58DName.Visible = false;
            panel58Address.Visible = false;
            panel58Address2.Visible = false;
            panel58Address3.Visible = false;
            panel58Address4.Visible = false;
        }
    }
    public void CreateSwiftSFMSFile()
    {
        string DocScrutiny = Request.QueryString["lblDocument_Scrutiny"].ToString();
        string FileType = "";
        if (ddl_Nego_Remit_Bank.SelectedIndex == 1)
        {
            FileType = "SWIFT";
            if (rdb_MT734.Checked)
            {
                //CreateSwift734(FileType);
                CreateSwift734_XML(FileType);
                InsertDataForSwiftSTP("MT734");
            }
            if (rdb_MT799.Checked)
            {
                //CreateSwift799(FileType);
                CreateSwift799_XML(FileType);
                InsertDataForSwiftSTP("MT799");
            }
            if (rdb_MT499.Checked && cb_Protest.Checked)
            {
                //CreateProtest499(FileType);
                CreateProtest499_XML(FileType);
                InsertDataForSwiftSTP("MT499PT");
            }
            else if (rdb_MT499.Checked)
            {
                //CreateSwift499(FileType);
                CreateSwift499_XML(FileType);
                InsertDataForSwiftSTP("MT499");
            }
            if (rdb_MT999.Checked && cb_Protest.Checked)
            {
                //CreateProtest999(FileType);
                CreateProtest999_XML(FileType);
                InsertDataForSwiftSTP("MT999PT");
            }
            else if (rdb_MT999.Checked && lblCollection_Lodgment_UnderLC.Text == "LodgmentUnderLC" && DocScrutiny == "2")
            {
                //CreateSwift999LC(FileType);
                CreateSwift999LC_XML(FileType);
                InsertDataForSwiftSTP("MT999LC");
            }
            else if (rdb_MT999.Checked)
            {
                //CreateSwift999(FileType);
                CreateSwift999_XML(FileType);
                InsertDataForSwiftSTP("MT999");
            }

        }
        if (ddl_Nego_Remit_Bank.SelectedIndex == 2)
        {
            FileType = "SFMS";
            if (rdb_MT734.Checked)
            {
                CreateSwift734(FileType);
            }
            if (rdb_MT799.Checked)
            {
                CreateSwift799(FileType);
            }
            if (rdb_MT499.Checked && cb_Protest.Checked)
            {
                CreateProtest499(FileType);
            }
            else if (rdb_MT499.Checked)
            {
                CreateSwift499(FileType);
            }
            if (rdb_MT999.Checked && cb_Protest.Checked)
            {
                CreateProtest999(FileType);
            }
            else if (rdb_MT999.Checked && lblCollection_Lodgment_UnderLC.Text == "LodgmentUnderLC" && DocScrutiny == "2")
            {
                CreateSwift999LC(FileType);
            }
            else if (rdb_MT999.Checked)
            {
                CreateSwift999(FileType);
            }

        }
    }
    public void CreateSwift734(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT734_FileCreation", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SWIFT/MT734/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SFMS/SFMS734/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            string _senderToRecInfo = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT734.xlsx";
                _senderToRecInfo = "[72Z]";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN734.xlsx";
                _senderToRecInfo = "[72]";
            }
            if (dt.Rows.Count > 0)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    ws.Cells["A1"].Value = "";
                    if (FileType == "SWIFT")
                    {
                        ws.Cells["B1"].Value = "TO:";
                        ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();
                    }
                    else
                    {
                        ws.Cells["B1"].Value = "Reciver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();

                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B2"].Value = "Sender's TRN";
                    ws.Cells["C2"].Value = dt.Rows[0]["SendersTRN"].ToString();

                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["B3"].Value = "Presenting Bank's Reference";
                    ws.Cells["C3"].Value = dt.Rows[0]["PresentingBanksReference"].ToString();

                    ws.Cells["A4"].Value = "[32A]";
                    ws.Cells["B4"].Value = "Date and Amount of Utilisation";
                    ws.Cells["C4"].Value = dt.Rows[0]["Date"].ToString();
                    ws.Cells["D4"].Value = dt.Rows[0]["Currency"].ToString();
                    ws.Cells["E4"].Value = dt.Rows[0]["Amount"].ToString();

                    int _Ecol = 5;
                    ws.Cells["A" + _Ecol].Value = "[73A]";
                    ws.Cells["B" + _Ecol].Value = "Charges Claimed";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ChargesClaimed7341"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["ChargesClaimed7342"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ChargesClaimed7342"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ChargesClaimed7343"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ChargesClaimed7343"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ChargesClaimed7344"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ChargesClaimed7344"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ChargesClaimed7345"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ChargesClaimed7345"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ChargesClaimed7346"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ChargesClaimed7346"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[33" + dt.Rows[0]["TotalAmountClaimed734"].ToString() + "]";
                    ws.Cells["B" + _Ecol].Value = "Total Amount Claimed";
                    if (dt.Rows[0]["Date734"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Date734"].ToString();
                        ws.Cells["D" + _Ecol].Value = dt.Rows[0]["Currency734"].ToString();
                        ws.Cells["E" + _Ecol].Value = dt.Rows[0]["Amount734"].ToString();
                    }
                    else
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Currency734"].ToString();
                        ws.Cells["D" + _Ecol].Value = dt.Rows[0]["Amount734"].ToString();
                    }
                    _Ecol++;

                    ws.Cells["A" + _Ecol].Value = "[57" + dt.Rows[0]["AccountWithBank734"].ToString() + "]";
                    ws.Cells["B" + _Ecol].Value = "Account With Bank";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBankPartyIdentifier"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["AccountWithBank734AccountNo"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBank734AccountNo"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithBank734SwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBank734SwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithBank734Location"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBank734Location"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithBank734Name"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBank734Name"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithBank734Address1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBank734Address1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithBank734Address2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBank734Address2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithBank734Address3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithBank734Address3"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = _senderToRecInfo;
                    ws.Cells["B" + _Ecol].Value = "Sender To Receiver Information";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation7341"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["SendertoReceiverInformation7342"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation7342"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation7343"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation7343"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation7344"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation7344"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation7345"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation7345"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation7346"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation7346"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[77J]";
                    ws.Cells["B" + _Ecol].Value = "Discrepancies";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Discrepancy1"].ToString();
                    _Ecol++;
                    int _Tcol = 2;

                    for (int i = 1; i < 43; i++)
                    {
                        if (dt.Rows[0]["Discrepancy" + _Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Discrepancy" + _Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[77B]";
                    ws.Cells["B" + _Ecol].Value = "Disposal of Documents";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DisposalOfDocuments"].ToString();
                    _Ecol++;
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DisposalOfDocuments1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["DisposalOfDocuments2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DisposalOfDocuments2"].ToString();
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
    public void CreateSwift499(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT499_FileCreation", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SWIFT/MT499/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SFMS/SFMS499/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT499.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN499.xlsx";
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

                    ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";

                    int _Tcol = 2;
                    int _Ecol = 4;

                    for (int i = 0; i < 35; i++)
                    {
                        if (dt.Rows[0][_Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0][_Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
                    }
                    //ws.Cells["C" + _Ecol].Value =_Tcol;

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
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT799_FileCreation", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SWIFT/MT799/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SFMS/SFMS799/" + MTodayDate + "/");
            }

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
                    ws.Cells["C2"].Value = dt.Rows[0]["TransactionReferenceNumber"].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0]["RelatedReference"].ToString();

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
                    //ws.Cells["C" + _Ecol].Value = _Tcol;


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
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999_FileCreation", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SWIFT/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SFMS/SFMS999/" + MTodayDate + "/");
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

                    ws.Cells["C1"].Value = dt.Rows[0][37].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";

                    int _Tcol = 2;
                    int _Ecol = 4;

                    for (int i = 0; i < 35; i++)
                    {
                        if (dt.Rows[0][_Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0][_Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
                    }
                    //ws.Cells["C" + _Ecol].Value = _Tcol;


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
    public void CreateProtest499(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT499_FileCreation", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SWIFT/MT499/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SFMS/SFMS499/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT499.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN499.xlsx";
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
                        ws.Cells["B1"].Value = "TO:";
                    }
                    ws.Cells["B2"].Value = "Transation Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";

                    ws.Cells["C1"].Value = dt.Rows[0][37].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";
                    ws.Cells["C4"].Value = "WE REFER TO YOUR COLLECTION INSTRUCTION,";
                    ws.Cells["C5"].Value = "KINDLY NOTE THAT WE ARE NOT RESPONSIBLE";
                    ws.Cells["C6"].Value = "TO PROTEST IN CASE OF NON-PAYMENT OR";
                    ws.Cells["C7"].Value = "NON-ACCEPTANCE.";
                    ws.Cells["C8"].Value = ".";
                    ws.Cells["C9"].Value = "PLEASE NOTE AND UPDATE YOUR RECORDS ACCORDINGLY.";
                    ws.Cells["C10"].Value = "REGARDS,";
                    ws.Cells["C11"].Value = dt.Rows[0]["BankName"].ToString();

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
    public void CreateProtest999(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999_FileCreation", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SWIFT/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SFMS/SFMS999/" + MTodayDate + "/");
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
                    if (FileType == "SWIFT")
                    {
                        ws.Cells["B1"].Value = "TO:";
                    }
                    else
                    {
                        ws.Cells["B1"].Value = "TO:";
                    }
                    ws.Cells["B2"].Value = "Transation Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";

                    ws.Cells["C1"].Value = dt.Rows[0][37].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";
                    ws.Cells["C4"].Value = "KIND ATTN:TRADE FINANCE DEPT,";
                    ws.Cells["C5"].Value = "KINDLY TREAT THIS MESSAGE AS MT 499,AS WE DON'T";
                    ws.Cells["C6"].Value = "HAVE RMA WITH YOUR GOOD BANK.";
                    ws.Cells["C7"].Value = "WE REFER TO YOUR COLLECTION INSTRUCTION, KINDLY";
                    ws.Cells["C8"].Value = "NOTE THAT WE ARE NOT RESPONSIBLE TO PROTEST IN";
                    ws.Cells["C9"].Value = "CASE OF NON-PAYMENT OR NON-ACCEPTANCE.";
                    ws.Cells["C10"].Value = "PLEASE NOTE AND UPDATE YOUR RECORDS ACCORDINGLY.";
                    ws.Cells["C11"].Value = "REGARDS,";
                    ws.Cells["C12"].Value = dt.Rows[0]["BankName"].ToString();

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
    public void CreateSwift999LC(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999LC_FileCreation", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SWIFT/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/SFMS/SFMS999/" + MTodayDate + "/");
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

                    ws.Cells["C1"].Value = dt.Rows[0][49].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";
                    ws.Cells["C4"].Value = "KINDLY TREAT THIS MESSAGE AS MT-734 SINCE WE DO";
                    ws.Cells["C5"].Value = "NOT HAVE DIRECT RMA WITH YOUR GOOD BANK.";
                    ws.Cells["C6"].Value = ".";
                    ws.Cells["C7"].Value = "20 : " + dt.Rows[0][0].ToString();
                    ws.Cells["C8"].Value = "21 : " + dt.Rows[0][52].ToString();
                    ws.Cells["C9"].Value = "32A : " + dt.Rows[0][2].ToString() + " " + dt.Rows[0][3].ToString() + " " + dt.Rows[0][4].ToString();
                    ws.Cells["C10"].Value = "77J :";

                    int _Tcol = 5;
                    int _Ecol = 11;

                    for (int i = 0; i < 43; i++)
                    {
                        if (dt.Rows[0][_Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0][_Tcol].ToString();
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
    ///////////////////////////////Nilesh////////////////////////////////////////

    //------------------IBA/ACC----------------------------------------------------

    public void CreateSwift734_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT734_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Lodgement/MT734/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Lodgement/SFMS734/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT734_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN734_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string DateAmtofUtilisation = "";
                string ChargesClaimed = "";
                string TotalAmountClaimed = "";
                string AccountWithBank = "";
                string SendertoReceiverInformation = "";
                string Discrepancy = "";
                string DisposalofDocument = "";
                string comma = "";

                #region Date and Amount of Utilisation
                if (dt.Rows[0]["Date"].ToString() != "")
                {
                    DateAmtofUtilisation = dt.Rows[0]["Date"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["Currency"].ToString() != "")
                {
                    DateAmtofUtilisation = DateAmtofUtilisation + dt.Rows[0]["Currency"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["Amount"].ToString() != "")
                {
                    if (dt.Rows[0]["Amount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["Amount"].ToString().Contains(".00"))
                        {
                            DateAmtofUtilisation = DateAmtofUtilisation + dt.Rows[0]["Amount"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            DateAmtofUtilisation = DateAmtofUtilisation + dt.Rows[0]["Amount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        DateAmtofUtilisation = DateAmtofUtilisation + dt.Rows[0]["Amount"].ToString() + ",";
                    }
                }
                #endregion
                #region Charges Claimed
                if (dt.Rows[0]["ChargesClaimed7341"].ToString() != "")
                {
                    ChargesClaimed = dt.Rows[0]["ChargesClaimed7341"].ToString();
                }
                if (dt.Rows[0]["ChargesClaimed7342"].ToString() != "")
                {
                    ChargesClaimed = ChargesClaimed + "," + dt.Rows[0]["ChargesClaimed7342"].ToString();
                }
                if (dt.Rows[0]["ChargesClaimed7343"].ToString() != "")
                {
                    ChargesClaimed = ChargesClaimed + "," + dt.Rows[0]["ChargesClaimed7343"].ToString();
                }
                if (dt.Rows[0]["ChargesClaimed7344"].ToString() != "")
                {
                    ChargesClaimed = ChargesClaimed + "," + dt.Rows[0]["ChargesClaimed7344"].ToString();
                }
                if (dt.Rows[0]["ChargesClaimed7345"].ToString() != "")
                {
                    ChargesClaimed = ChargesClaimed + "," + dt.Rows[0]["ChargesClaimed7345"].ToString();
                }
                if (dt.Rows[0]["ChargesClaimed7346"].ToString() != "")
                {
                    ChargesClaimed = ChargesClaimed + "," + dt.Rows[0]["ChargesClaimed7346"].ToString();
                }
                if (ChargesClaimed.Length != 0)
                {
                    comma = ChargesClaimed.Trim().Substring(0, 1);
                    if (comma == ",") { ChargesClaimed = ChargesClaimed.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Total Amount Claimed
                if (dt.Rows[0]["TotalAmountClaimed734"].ToString() == "A")
                {
                    if (dt.Rows[0]["Date734"].ToString() != "")
                    {
                        TotalAmountClaimed = dt.Rows[0]["Date734"].ToString().Replace("-", "");
                    }
                    if (dt.Rows[0]["Currency734"].ToString() != "")
                    {
                        TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Currency734"].ToString().Replace("-", "");
                    }
                    if (dt.Rows[0]["Amount734"].ToString() != "")
                    {

                        if (dt.Rows[0]["Amount734"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["Amount734"].ToString().Contains(".00"))
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Amount734"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Amount734"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Amount734"].ToString() + ",";
                        }
                    }
                }
                if (dt.Rows[0]["TotalAmountClaimed734"].ToString() == "B")
                {
                    if (dt.Rows[0]["Currency734"].ToString() != "")
                    {
                        TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Currency734"].ToString().Replace("-", "");
                    }
                    if (dt.Rows[0]["Amount734"].ToString() != "")
                    {
                        if (dt.Rows[0]["Amount734"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["Amount734"].ToString().Contains(".00"))
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Amount734"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Amount734"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["Amount734"].ToString() + ",";
                        }
                    }
                }
                #endregion
                #region AccountWithBank
                if (dt.Rows[0]["AccountWithBank734"].ToString() == "A")
                {
                    if (dt.Rows[0]["AccountWithBankPartyIdentifier"].ToString() != "" || dt.Rows[0]["AccountWithBank734AccountNo"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["AccountWithBankPartyIdentifier"].ToString() + dt.Rows[0]["AccountWithBank734AccountNo"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["AccountWithBank734SwiftCode"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["AccountWithBank734SwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccountWithBank734SwiftCode"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["AccountWithBank734SwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccountWithBank734"].ToString() == "B")
                {
                    if (dt.Rows[0]["AccountWithBankPartyIdentifier"].ToString() != "" || dt.Rows[0]["AccountWithBank734AccountNo"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["AccountWithBankPartyIdentifier"].ToString() + dt.Rows[0]["AccountWithBank734AccountNo"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["AccountWithBank734Location"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["AccountWithBank734Location"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccountWithBank734Location"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["AccountWithBank734Location"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccountWithBank734"].ToString() == "D")
                {
                    AccountWithBank = dt.Rows[0]["AccountWithBankPartyIdentifier"].ToString() + dt.Rows[0]["AccountWithBank734AccountNo"].ToString();
                    if (dt.Rows[0]["AccountWithBank734Name"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["AccountWithBank734Name"].ToString();
                    }
                    if (dt.Rows[0]["AccountWithBank734Address1"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "," + dt.Rows[0]["AccountWithBank734Address1"].ToString() + ",";
                    }
                    if (dt.Rows[0]["AccountWithBank734Address2"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "," + dt.Rows[0]["AccountWithBank734Address2"].ToString() + ",";
                    }
                    if (dt.Rows[0]["AccountWithBank734Address3"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "," + dt.Rows[0]["AccountWithBank734Address3"].ToString();
                    }
                }
                #endregion
                #region SendertoReceiverInformation
                if (dt.Rows[0]["SendertoReceiverInformation7341"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + dt.Rows[0]["SendertoReceiverInformation7341"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation7342"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation7342"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation7343"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation7343"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation7344"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation7344"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation7345"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation7345"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation7346"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation7346"].ToString().ToString().Replace("/", "");
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == ",") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Discrepancies
                if (dt.Rows[0]["Discrepancy1"].ToString() != "")
                {
                    Discrepancy = Discrepancy + dt.Rows[0]["Discrepancy1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy2"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy3"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy4"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy5"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy6"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy7"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy8"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy9"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy10"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy11"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy12"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy13"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy14"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy15"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy16"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy17"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy18"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy19"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy20"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy21"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy22"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy23"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy24"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy25"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy26"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy27"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy27"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy28"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy29"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy30"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy31"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy32"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy33"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy34"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy35"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy35"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy36"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy36"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy37"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy37"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy38"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy38"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy39"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy39"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy40"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy40"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy41"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy41"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy42"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy42"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Discrepancy43"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy43"].ToString().Replace("/", "");
                }
                if (Discrepancy.Length != 0)
                {
                    comma = Discrepancy.Trim().Substring(0, 1);
                    if (comma == ",") { Discrepancy = Discrepancy.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region DisposalOfDocument
                if (dt.Rows[0]["DisposalOfDocuments"].ToString() != "")
                {
                    DisposalofDocument = DisposalofDocument + dt.Rows[0]["DisposalOfDocuments"].ToString();
                }
                if (dt.Rows[0]["DisposalOfDocuments1"].ToString() != "")
                {
                    DisposalofDocument = DisposalofDocument + "," + dt.Rows[0]["DisposalOfDocuments1"].ToString();
                }
                if (dt.Rows[0]["DisposalOfDocuments2"].ToString() != "")
                {
                    DisposalofDocument = DisposalofDocument + "," + dt.Rows[0]["DisposalOfDocuments2"].ToString();
                }
                if (DisposalofDocument.Length != 0)
                {
                    comma = DisposalofDocument.Trim().Substring(0, 1);
                    if (comma == ",") { DisposalofDocument = DisposalofDocument.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<SendersTRN>" + dt.Rows[0]["SendersTRN"].ToString() + "</SendersTRN>");
                sw.WriteLine("<PresentingBanksReference>" + dt.Rows[0]["PresentingBanksReference"].ToString() + "</PresentingBanksReference>");
                sw.WriteLine("<DateAmtofUtilisation>" + DateAmtofUtilisation + "</DateAmtofUtilisation>");
                sw.WriteLine("<ChargesClaimed>" + ChargesClaimed + "</ChargesClaimed>");
                if (dt.Rows[0]["TotalAmountClaimed734"].ToString() == "A")
                {
                    sw.WriteLine("<TotalAmountClaimedA>" + TotalAmountClaimed + "</TotalAmountClaimedA>");
                }
                if (dt.Rows[0]["TotalAmountClaimed734"].ToString() == "B")
                {
                    sw.WriteLine("<TotalAmountClaimedB>" + TotalAmountClaimed + "</TotalAmountClaimedB>");
                }
                if (dt.Rows[0]["AccountWithBank734"].ToString() == "A")
                {
                    sw.WriteLine("<AccountWithBankA>" + AccountWithBank + "</AccountWithBankA>");
                }
                if (dt.Rows[0]["AccountWithBank734"].ToString() == "B")
                {
                    sw.WriteLine("<AccountWithBankB>" + AccountWithBank + "</AccountWithBankB>");
                }
                if (dt.Rows[0]["AccountWithBank734"].ToString() == "D")
                {
                    sw.WriteLine("<AccountWithBankD>" + AccountWithBank + "</AccountWithBankD>");
                }
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("<Discrepancies>" + Discrepancy + "</Discrepancies>");
                sw.WriteLine("<DisposalofDocuments>" + DisposalofDocument + "</DisposalofDocuments>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }  // Swift Changes
    public void CreateSwift799_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT799_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Lodgement/MT799/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Lodgement/SFMS799/" + MTodayDate + "/");
            }
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT799_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN799_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                string comma = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
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
                if (Narrative.Length != 0)
                {
                    comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
    public void CreateSwift499_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT499_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Lodgement/MT499/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Lodgement/SFMS499/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT499_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN499_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
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
    }
    public void CreateProtest499_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT499_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Lodgement/MT499/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Lodgement/SFMS499/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_PROTEST_MT499_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_PROTEST_FIN499_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";

                Narrative = "WE REFER TO YOUR COLLECTION INSTRUCTION,";
                Narrative = Narrative + " KINDLY NOTE THAT WE ARE NOT RESPONSIBLE";
                Narrative = Narrative + " TO PROTEST IN CASE OF NON-PAYMENT OR";
                Narrative = Narrative + " NON-ACCEPTANCE.";
                Narrative = Narrative + ".";
                Narrative = Narrative + " PLEASE NOTE AND UPDATE YOUR RECORDS ACCORDINGLY.";
                Narrative = Narrative + " REGARDS,";

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "," + dt.Rows[0]["BankName"].ToString() + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
    public void CreateSwift999_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Lodgement/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Lodgement/SFMS999/" + MTodayDate + "/");
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
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
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
    }
    public void CreateProtest999_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Lodgement/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Lodgement/SFMS999/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_PROTEST_MT999_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_PROTEST_FIN999_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";

                Narrative = "KIND ATTN:TRADE FINANCE DEPT,";
                Narrative = Narrative + " KINDLY TREAT THIS MESSAGE AS MT 499,AS WE DO NOT";
                Narrative = Narrative + " HAVE RMA WITH YOUR GOOD BANK.";
                Narrative = Narrative + " WE REFER TO YOUR COLLECTION INSTRUCTION, KINDLY";
                Narrative = Narrative + " NOTE THAT WE ARE NOT RESPONSIBLE TO PROTEST IN";
                Narrative = Narrative + " CASE OF NON-PAYMENT OR NON-ACCEPTANCE.";
                Narrative = Narrative + " PLEASE NOTE AND UPDATE YOUR RECORDS ACCORDINGLY.";
                Narrative = Narrative + " REGARDS,";

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "," + dt.Rows[0]["BankName"].ToString() + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
    public void CreateSwift999LC_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999LC_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Lodgement/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Lodgement/SFMS999/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT999LC_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN999LC_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Discrepancy = "";
                Discrepancy = Discrepancy + "KINDLY TREAT THIS MESSAGE AS MT-734 SINCE WE DO";
                Discrepancy = Discrepancy + " NOT HAVE DIRECT RMA WITH YOUR GOOD BANK.";
                Discrepancy = Discrepancy + ".";
                Discrepancy = Discrepancy +" 20:" + dt.Rows[0]["Senders TRN"].ToString();
                Discrepancy = Discrepancy + "#" +" 21:" + dt.Rows[0]["Presenting Banks Reference"].ToString();
                Discrepancy = Discrepancy + "#" + "32A:" + dt.Rows[0]["Date"].ToString() + " " + dt.Rows[0]["Currency"].ToString() + " " + dt.Rows[0]["Amount"].ToString();
                Discrepancy = Discrepancy + "#" + "77J:";
                if (dt.Rows[0]["Discrepancy1"].ToString() != "")
                {
                    Discrepancy = Discrepancy + dt.Rows[0]["Discrepancy1"].ToString();
                }
                if (dt.Rows[0]["Discrepancy2"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy2"].ToString();
                }
                if (dt.Rows[0]["Discrepancy3"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy3"].ToString();
                }
                if (dt.Rows[0]["Discrepancy4"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy4"].ToString();
                }
                if (dt.Rows[0]["Discrepancy5"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy5"].ToString();
                }
                if (dt.Rows[0]["Discrepancy6"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy6"].ToString();
                }
                if (dt.Rows[0]["Discrepancy7"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy7"].ToString();
                }
                if (dt.Rows[0]["Discrepancy8"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy8"].ToString();
                }
                if (dt.Rows[0]["Discrepancy9"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy9"].ToString();
                }
                if (dt.Rows[0]["Discrepancy10"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy10"].ToString();
                }
                if (dt.Rows[0]["Discrepancy11"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy11"].ToString();
                }
                if (dt.Rows[0]["Discrepancy12"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy12"].ToString();
                }
                if (dt.Rows[0]["Discrepancy13"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy13"].ToString();
                }
                if (dt.Rows[0]["Discrepancy14"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy14"].ToString();
                }
                if (dt.Rows[0]["Discrepancy15"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy15"].ToString();
                }
                if (dt.Rows[0]["Discrepancy16"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy16"].ToString();
                }
                if (dt.Rows[0]["Discrepancy17"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy17"].ToString();
                }
                if (dt.Rows[0]["Discrepancy18"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy18"].ToString();
                }
                if (dt.Rows[0]["Discrepancy19"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy19"].ToString();
                }
                if (dt.Rows[0]["Discrepancy20"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy20"].ToString();
                }
                if (dt.Rows[0]["Discrepancy21"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy21"].ToString();
                }
                if (dt.Rows[0]["Discrepancy22"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy22"].ToString();
                }
                if (dt.Rows[0]["Discrepancy23"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy23"].ToString();
                }
                if (dt.Rows[0]["Discrepancy24"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy24"].ToString();
                }
                if (dt.Rows[0]["Discrepancy25"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy25"].ToString();
                }
                if (dt.Rows[0]["Discrepancy26"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy26"].ToString();
                }
                if (dt.Rows[0]["Discrepancy27"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy27"].ToString();
                }
                if (dt.Rows[0]["Discrepancy28"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy28"].ToString();
                }
                if (dt.Rows[0]["Discrepancy29"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy29"].ToString();
                }
                if (dt.Rows[0]["Discrepancy30"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy30"].ToString();
                }
                if (dt.Rows[0]["Discrepancy31"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy31"].ToString();
                }
                if (dt.Rows[0]["Discrepancy32"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy32"].ToString();
                }
                if (dt.Rows[0]["Discrepancy33"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy33"].ToString();
                }
                if (dt.Rows[0]["Discrepancy34"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy34"].ToString();
                }
                if (dt.Rows[0]["Discrepancy35"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy35"].ToString();
                }
                if (dt.Rows[0]["Discrepancy36"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy36"].ToString();
                }
                if (dt.Rows[0]["Discrepancy37"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy37"].ToString();
                }
                if (dt.Rows[0]["Discrepancy38"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy38"].ToString();
                }
                if (dt.Rows[0]["Discrepancy39"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy39"].ToString();
                }
                if (dt.Rows[0]["Discrepancy40"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy40"].ToString();
                }
                if (dt.Rows[0]["Discrepancy41"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy41"].ToString();
                }
                if (dt.Rows[0]["Discrepancy42"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy42"].ToString();
                }
                if (dt.Rows[0]["Discrepancy43"].ToString() != "")
                {
                    Discrepancy = Discrepancy + "," + dt.Rows[0]["Discrepancy43"].ToString();
                }
                if (Discrepancy.Length != 0)
                {
                    string comma = Discrepancy.Trim().Substring(0, 1);
                    if (comma == ",") { Discrepancy = Discrepancy.Remove(0, 1); }
                    comma = "";
                }
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["TO"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Senders TRN"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Presenting Banks Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Discrepancy + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }

    public void TotalAmountClaimedChange()
    {
        string TotalAmountClaimed = ddlTotalAmountClaimed734.SelectedValue;
        if (TotalAmountClaimed == "A")
        {
            txtDate734.Style.Add("display", "inline-block");
        }
        if (TotalAmountClaimed == "B")
        {
            txtDate734.Style.Add("display", "none");
        }
    }
    public void AccountWithBankChange()
    {
        string AccountWithBank = ddlAccountWithBank734.SelectedValue;
        if (AccountWithBank == "A")
        {

            txtAccountWithBank734SwiftCode.Style.Add("display", "block");
            lblAccountWithBank734SwiftCode.Text = "Swift Code :";
            txtAccountWithBank734Location.Style.Add("display", "none");
            txtAccountWithBank734Name.Style.Add("display", "none");
            lblAccountWithBank734Address1.Style.Add("display", "none");
            lblAccountWithBank734Address2.Style.Add("display", "none");
            lblAccountWithBank734Address3.Style.Add("display", "none");
            txtAccountWithBank734Address1.Style.Add("display", "none");
            txtAccountWithBank734Address2.Style.Add("display", "none");
            txtAccountWithBank734Address3.Style.Add("display", "none");
        }
        if (AccountWithBank == "B")
        {
            txtAccountWithBank734SwiftCode.Style.Add("display", "none");
            lblAccountWithBank734SwiftCode.Text = "Location :";
            txtAccountWithBank734Location.Style.Add("display", "block");
            txtAccountWithBank734Name.Style.Add("display", "none");
            lblAccountWithBank734Address1.Style.Add("display", "none");
            lblAccountWithBank734Address2.Style.Add("display", "none");
            lblAccountWithBank734Address3.Style.Add("display", "none");
            txtAccountWithBank734Address1.Style.Add("display", "none");
            txtAccountWithBank734Address2.Style.Add("display", "none");
            txtAccountWithBank734Address3.Style.Add("display", "none");
        }
        if (AccountWithBank == "D")
        {
            txtAccountWithBank734SwiftCode.Style.Add("display", "none");
            lblAccountWithBank734SwiftCode.Text = "Name :";
            txtAccountWithBank734Location.Style.Add("display", "none");
            txtAccountWithBank734Name.Style.Add("display", "block");
            lblAccountWithBank734Address1.Style.Add("display", "block");
            lblAccountWithBank734Address2.Style.Add("display", "block");
            lblAccountWithBank734Address3.Style.Add("display", "block");
            txtAccountWithBank734Address1.Style.Add("display", "block");
            txtAccountWithBank734Address2.Style.Add("display", "block");
            txtAccountWithBank734Address3.Style.Add("display", "block");
        }
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/GBASE/" + MTodayDate + "/");
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
                string link = "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
                lblFolderLink.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
        }
        else
        {
        }
    }
    public void AMLTextFileCreation()
    {
        TF_DATA objData = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        dt = objData.getData("TF_IMP_AMLFileCreation", P_DocNo);
        string _DirectoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Lodgement/AML");
        SqlParameter PUserName = new SqlParameter("@UserName", Session["userName"].ToString());
        string _FileName = objData.SaveDeleteData("TF_IMP_GetAMLFileName", PUserName);
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
        if (!Directory.Exists(_DirectoryPath))
        {
            Directory.CreateDirectory(_DirectoryPath);
        }
        StreamWriter sw;
        string FilePath = _DirectoryPath + "/" + _FileName + ".txt";
        sw = File.CreateText(FilePath);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Rows[0][i].ToString() != "")
                {
                    sw.WriteLine(dt.Rows[0][i].ToString() + "~~~~~DOB~PASSPORT~PAN~ACCTNO~CUSTID~");
                }
            }
        }
        sw.Flush();
        sw.Close();
        sw.Dispose();
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
            else if (txtBillAmount.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Amount for Verifying LEI details.')", true);
                txtBillAmount.Focus();
            }
            else if (txtDueDate.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Due date for Verifying LEI details.')", true);
                txtDueDate.Focus();
            }
            else if (ddlDrawer.SelectedItem.Text.Trim() == "Select")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Select Drawer for Verifying LEI details.')", true);
                ddlDrawer.Focus();
            }
            else
            {
                Check_Lei_Status();
                Check_LEINODetails();
                Check_LEINO_ExpirydateDetails();
                // Check_LEINO_ExchRateDetails();
                Check_DraweeLEINODetails();
                Check_DraweeLEINO_ExpirydateDetails();
                ddlApproveReject.Enabled = true;

                String CustLEINo = hdnleino.Value + " " + lblLEI_Remark.Text + "\\n";
                String CustLEIExpiry = hdnleiexpiry.Value + " " + lblLEIExpiry_Remark.Text + "\\n";
                String DrawerLEINo = hdnDraweeleino.Value + " " + lblLEI_Remark_Drawee.Text + "\\n";
                String DrawerLEIExpiry = hdnDraweeleiexpiry.Value + " " + lblLEIExpiry_Remark_Drawee.Text + "\\n";

                String LEIMSG = @"Customer LEI No : " + CustLEINo + "Customer LEI Expiry : " + CustLEIExpiry + "Drawer LEI : " + DrawerLEINo + "Drawer LEI Expiry : " + DrawerLEIExpiry;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('" + LEIMSG + "')", true);
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", hdnBranchName.Value.ToString());
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            hdnBranchCode.Value = dt.Rows[0]["BranchCode"].ToString();
        }
    }
    private void Check_LEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //p1.Value = txtCustomer_ID.Text;
            string _query = "TF_IMP_GetLEIDetails_Customer";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LEI_No1"].ToString().Trim() == "")
                {
                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI Number for this customer is not available.')", true);
                    lblLEI_Remark.Text = "...Not Verified.";
                    lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                    hdnleino.Value = "";
                }
                else
                {
                    lblLEI_Remark.Text = "...Verified.";
                    hdnleino.Value = dt.Rows[0]["LEI_No"].ToString();
                    //lblLEI_Remark.Visible = true;
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
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
            SqlParameter p2 = new SqlParameter("@DueDate", txtDueDate.Text.ToString().Trim());
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //p1.Value = txtCustomer_ID.Text;
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
        if (lblForeign_Local.Text == "FOREIGN")
        {
            try
            {
                TF_DATA objData = new TF_DATA();
                string result2 = "";
                SqlParameter p1 = new SqlParameter("@CurrCode", ddl_Doc_Currency.SelectedValue.ToString());
                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.VarChar);
                p2.Value = txtLogdmentDate.Text.ToString();
                string _query = "TF_IMP_GetLEI_RateCardDetails";
                DataTable dt = objData.getData(_query, p1, p2);
                if (dt.Rows.Count > 0)
                {
                    string Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    string Amount = txtBillAmount.Text.Trim().Replace(",","");
                    if (Amount != "" && Exch_rate != "")
                    {
                        SqlParameter billamt = new SqlParameter("@billamt", Amount);
                        SqlParameter exch_Rate = new SqlParameter("@exch_Rate", Exch_rate);
                        string _queryC = "TF_IMP_GetThresholdLimitCheck";
                        result2 = objData.SaveDeleteData(_queryC, billamt, exch_Rate);

                        if (result2 == "Grater")
                        {
                            label1.Text = "Transaction Bill Amount is Greater than LEI Thresold Limit.Please Verify LEI details.";
                            label1.ForeColor = System.Drawing.Color.Red;
                            label1.Font.Size = 10;
                            label1.Font.Bold = true;
                            hdnLeiFlag.Value = "Y";
                            ddlApproveReject.Enabled = false;
                            btn_Verify.Visible = true;
                        }
                        else
                        {
                            label1.Text = "Transaction Bill Amount is less than LEI Thresold Limit.";
                            label1.ForeColor = System.Drawing.Color.Green;
                            label1.Font.Size = 10;
                            hdnLeiFlag.Value = "N";
                            ddlApproveReject.Enabled = true;
                            btn_Verify.Visible = false;
                            hdncustleiflag.Value = "";
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
            SqlParameter p2 = new SqlParameter("@Draweename", ddlDrawer.SelectedItem.Text.Trim());
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());


            string _query = "TF_IMP_GetLEIDetails_Drawee";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Drawer_LEI_NO"].ToString() == "")
                {
                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI Number for this Drawer master is not available.')", true);
                    lblLEI_Remark_Drawee.Text = "...Not Verified.";
                    lblLEI_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                    hdnDraweeleino.Value = "";
                }
                else
                {
                    lblLEI_Remark_Drawee.Text = "...Verified.";
                    hdnDraweeleino.Value = dt.Rows[0]["Drawer_LEI_NO"].ToString();
                    //lblLEI_Remark.Visible = true;
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
            SqlParameter p2 = new SqlParameter("@Draweename", ddlDrawer.SelectedItem.Text.Trim());
            SqlParameter p3 = new SqlParameter("@DueDate", txtDueDate.Text.ToString().Trim());
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
                SqlParameter P_DocType = new SqlParameter("@DocType", hdnDocType.Value.Trim());
                SqlParameter P_Sight_Usance = new SqlParameter("@billType", lblSight_Usance.Text.Trim());
                SqlParameter P_CustAcno = new SqlParameter("@CustAcNo", txtCustomer_ID.Text.Trim());
                SqlParameter P_Cust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
                SqlParameter P_Cust_Name = new SqlParameter("@Cust_Name", lblCustomerDesc.Text.Trim());
                SqlParameter P_Cust_LEI = new SqlParameter("@Cust_LEI", hdnleino.Value.Trim());
                SqlParameter P_Cust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdnleiexpiry.Value.Trim());
                SqlParameter P_Cust_LEI_Expired = new SqlParameter("@Cust_LEI_Expired", hdnleiexpiry1.Value.Trim());
                SqlParameter P_Drawee_Name = new SqlParameter("@Drawee_Name", ddlDrawer.SelectedItem.Text.Trim());
                SqlParameter P_Drawee_LEI = new SqlParameter("@Drawee_LEI", hdnDraweeleino.Value.Trim());
                SqlParameter P_Drawee_LEI_Expiry = new SqlParameter("@Drawee_LEI_Expiry", hdnDraweeleiexpiry.Value.Trim());
                SqlParameter P_Drawee_LEI_Expired = new SqlParameter("@Drawee_LEI_Expired", hdnDraweeleiexpiry1.Value.Trim());
                SqlParameter P_BillAmount = new SqlParameter("@BillAmt", txtBillAmount.Text.Trim().Replace(",", ""));
                SqlParameter P_txtCurr = new SqlParameter("@Curr", ddl_Doc_Currency.SelectedItem.Text.Trim());
                SqlParameter P_Exchrate = new SqlParameter("@ExchRt", lbl_Exch_rate.Text.Trim());
                SqlParameter P_LodgementDate = new SqlParameter("@LodgDate", txtLogdmentDate.Text.Trim());
                SqlParameter P_DueDate = new SqlParameter("@DueDate", txtDueDate.Text.Trim());
                SqlParameter P_LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value.Trim());
                SqlParameter P_CustLEI_Flag = new SqlParameter("@Cust_LEIFlag", hdncustleiflag.Value.Trim());
                SqlParameter P_Status = new SqlParameter("@status", ddlApproveReject.SelectedItem.Text.Trim());
                SqlParameter P_Module = new SqlParameter("@Module", "L");
                SqlParameter P_AddedBy = new SqlParameter("@user", hdnUserName.Value.Trim());
                SqlParameter P_AddedDate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                string _queryLEI = "TF_IMP_AddUpdate_LEITransaction_Checker";

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
                ddlApproveReject.Enabled = false;
                btn_Verify.Visible = true;
                ReccuringLEI.Visible = true;
                ReccuringLEI.Text = "This is Recurring LEI Customer.";
                ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                ReccuringLEI.Font.Size = 10;
            }
        }
    }
    private void Check_Lei_Verified()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Module", "L");
            string _query = "TF_IMP_Check_LeiVerified";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {

                LeiVerified.Visible = true;
                if (label1.Text == "Transaction Bill Amount is Greater than LEI Thresold Limit.Please Verify LEI details.")
                {
                    label1.Text = "Transaction Bill Amount is Greater than LEI Thresold Limit.";
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
    private void Check_Lei_Status()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Module", "L");
            string _query = "TF_IMP_CheckLei_Status";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                string lei_status = dt.Rows[0]["Cust_Lei_Flag"].ToString();
                if (lei_status.ToString().Trim() == "")
                {
                    hdncustleiflag.Value = "R";
                }
                else
                {
                    hdncustleiflag.Value = dt.Rows[0]["Cust_Lei_Flag"].ToString();
                }
            }
        }
    }

    private void Check_Lei_RecurringStatus()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Module", "L");
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
                else if (cust_lei_flag.ToString().Trim() == "Y")
                {
                    ReccuringLEI.Visible = false;
                }
            }
        }
    }

    protected void InsertDataForSwiftSTP(string SwiftType)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@SwiftType", SwiftType);
        string result = obj.SaveDeleteData("TF_SWIFT_STP_InsertLodgmentData", P1, P2);
    }
    [WebMethod]
    public static String Check_CurrVoucherBalance(string Curr)
    {
        string Result = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter PDocument_Curr = new SqlParameter("@Document_Curr", Curr.ToString());
        try
        {
            Result = objData.SaveDeleteData("TF_IMP_Voucher_Balance_CurrCheck", PDocument_Curr);
        }
        catch (Exception ex)
        {
            Result = ex.Message.ToString();
        }
        return Result;
    }
    [WebMethod]
    public static Fields TheirRefNoChange(string TheirRefNo, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter PTheirRefNo = new SqlParameter("@TheirRefNo", TheirRefNo);
        SqlParameter PDocNo = new SqlParameter("@DocNo", DocNo);
        string Result = obj.SaveDeleteData("TF_IMP_CheckDuplicateTheirRefNo", PTheirRefNo, PDocNo);
        fields.TheirRefNoStatus = Result;
        return fields;
    }
    [WebMethod]
    public static Fields InvoiceNoChange(string InvoiceNo, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter PTheirRefNo = new SqlParameter("@InvoiceNo", InvoiceNo);
        SqlParameter PDocNo = new SqlParameter("@DocNo", DocNo);
        string Result = obj.SaveDeleteData("TF_IMP_CheckDuplicateInvoiceNo", PTheirRefNo, PDocNo);
        fields.InvoiceNoStatus = Result;
        return fields;
    }
    public class Fields
    {
        //Customer Help
        public string CustName { get; set; }
        public string CustAbbr { get; set; }
        public List<ListItem> DrawerList { get; set; }
        public string CustStatus { get; set; }
        //LC Help
        public string LCAmount { get; set; }
        public string LCRef1 { get; set; }
        public string LCRef2 { get; set; }
        public string LCRef3 { get; set; }
        public string LCCurrency { get; set; }
        public string LCAppNo { get; set; }
        public string LCCountry { get; set; }
        public string LCCommodityCode { get; set; }
        public string LCExpiryDate { get; set; }
        public string LCRiskCustomer { get; set; }
        public string LCSettlementAcNo { get; set; }
        public string LCCustomerABBR1 { get; set; }
        public string Currency { get; set; }
        public string CurrencyName { get; set; }
        public string Commodity { get; set; }
        public string CommodityName { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string LCStatus { get; set; }
        //Nego Remi Bank Help
        public string NegoRemiBankCode { get; set; }
        public string NegoRemiDesc { get; set; }
        public string SStatus { get; set; }
        public string RMAExistStatus { get; set; }
        public string RMAStatus { get; set; }
        // On Currency Change
        public string CurrDesc { get; set; }
        // Due Date
        public string DueDate { get; set; }
        //Exceptence Date
        public string ExDate { get; set; }
        // AccountWith Institution Help
        public string AccWithInstiDesc { get; set; }
        // Reimbursing Bank Help
        public string ReimbBankDesc { get; set; }
        // Commodity Code change
        public string CommodityDesc { get; set; }
        // Country Change
        public string CountryDesc { get; set; }
        public string CountrySStatus { get; set; }

        // Submit to Checker
        public string CheckerStatus { get; set; }
        // Stamp Duty Charges
        public string StampDutyPerThousand { get; set; }
        // AML Text File Creation
        public string AMLFileName { get; set; }
        // Their Ref No Change
        public string TheirRefNoStatus { get; set; }
        // Invoice No Change
        public string InvoiceNoStatus { get; set; }
    }
}