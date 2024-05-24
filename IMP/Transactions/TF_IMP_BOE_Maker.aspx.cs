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
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

public partial class IMP_Transactions_TF_IMP_BOE_Maker : System.Web.UI.Page
{
    IMPClass objIMP = new IMPClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            try
            {
                hdnUserName.Value = Session["userName"].ToString();
                if (!IsPostBack)
                {
                    TabContainerMain.ActiveTab = tbDocumentDetails;
                    if (Request.QueryString["mode"] == null)
                    {
                        Response.Redirect("TF_IMP_BOE_Maker_View.aspx", true);
                    }
                    else
                    {
                        hdnBranchCode.Value = Request.QueryString["BranchCode"].Trim();
                        hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                        hdnDocType.Value = Request.QueryString["DocType"].Trim();
                        txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                        lblForeign_Local.Text = Request.QueryString["flcIlcType"].ToString();
                        lblCollection_Lodgment_UnderLC.Text = Request.QueryString["Collection_Lodgment"].ToString();
                        lblSight_Usance.Text = Request.QueryString["Sight_Usance"].ToString();
                        fillCurrency();
                        fill_GBaseCommodity();
                        fill_Country();
                        fill_CountryOfOrigin();
                        fill_DisposalOfDocument();

                        txtLogdmentDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        if (Request.QueryString["Mode"].ToString() == "Edit")
                        {
                            FillDetails();
                            ToggleDocTypeEdit(Request.QueryString["DocType"].Trim());
                            Check_LEINO_ExchRateDetails();
                            ////objIMP.CreateUserLog(hdnUserName.Value, "Opened document(" + txtDocNo.Text + ") for edit.");
                        }
                        else
                        {
                            txtDateReceived.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                            //txtLogdmentDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                            hdnDocumentScrutiny.Value = Request.QueryString["Doc_Scrutiny"].ToString();
                            ddl_Doc_Scrutiny.SelectedValue = Request.QueryString["Doc_Scrutiny"].ToString();
                            rbd_None.Checked = true;
                            ToggleDocTypeAdd(Request.QueryString["DocType"].Trim());
                            ////objIMP.CreateUserLog(hdnUserName.Value, "Selected document(" + txtDocNo.Text + ") to add.");
                        }
                        //intensionally ToggleDocType() kept after txtDateReceived set to call Exp_AcceptanceDate(), Duedate()
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "PageLoadFunctions();", true);
                    }
                    //btn_Swift_Create.Attributes.Add("OnClick", "return ValidateSwiftCreate();");
                    //btn_SFMS_create.Attributes.Add("OnClick", "return ValidateSFMSCreate();");
                    btnCustomerList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
                    btnLC_Help.Attributes.Add("onclick", "return OpenLCList('mouseClick');");
                    btn_Nego_Remit_Bank.Attributes.Add("onclick", "return OpenNego_Remit_BankList('mouseClick');");
                    btnAcwithInstitution.Attributes.Add("onclick", "return Open_Acwith_Institution_List('mouseClick');");
                    btn_Reimbursingbank.Attributes.Add("onclick", "return OpenReimbursingBankList('mouseClick');");
                    //txtBillAmount.Attributes.Add("onchange", "return Total_Bill_Amount('b');");
                    txtBillAmount.Attributes.Add("onblur", "return Total_Bill_Amount('b');");
                    txt_Interest_Amount.Attributes.Add("onblur", "return Total_Bill_Amount('a');");
                    txtComissionAmount.Attributes.Add("onblur", "return Total_Bill_Amount('a');");
                    txtOther_amount.Attributes.Add("onblur", "return Total_Bill_Amount('a');");
                    txtTheirCommission_Amount.Attributes.Add("onblur", "return Total_Bill_Amount('a');");
                    txtDateReceived.Attributes.Add("onblur", "return isValidDate(" + txtDateReceived.ClientID + "," + "'Received Date'" + " );");
                    txt_AcceptanceDate.Attributes.Add("onblur", "return isValidDate(" + txt_AcceptanceDate.ClientID + "," + "'Expected Acceptance Date'" + " );");
                    txtBOExchange.Attributes.Add("onblur", "return isValidDate(" + txtBOExchange.ClientID + "," + "'Bill of Exchange Date'" + " );");
                    txtNego_Date.Attributes.Add("onblur", "return isValidDate(" + txtNego_Date.ClientID + "," + "'Negotiation Date'" + " );");
                    txtDate734.Attributes.Add("onblur", "return isValidDate(" + txtDate734.ClientID + "," + "'[33A] Date'" + " );");
                    //ddlTenor.Attributes.Add("onchange", "return Check_Tenor_Type();");
                    txt_Their_Ref_no.Attributes.Add("onblur", "return validate_Their_Ref_No();");
                    txtBillAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtAmount734.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txt_Interest_Amount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtComissionAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtOther_amount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtTheirCommission_Amount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtTenor.Attributes.Add("onkeydown", "return validate_Number(event);");
                    ddl_Doc_Currency.Attributes.Add("onblur", "return validate_Currency();");

                    txtTenor.Attributes.Add("onblur", "return Total_Bill_Amount('a');");
                    txt_Stamp_Duty_Charges_ExRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txt_Stamp_Duty_Charges_ExRate.Attributes.Add("onblur", "return Total_Bill_Amount('a');");
                }
                txtDateReceived.Focus();
            }
            catch (Exception ex)
            {
                ////objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
            }
        }
    }
    protected void FillDetails()
    {
        try
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
                txtDateReceived.Text = dt.Rows[0]["Received_Date"].ToString();
                //txtLogdmentDate.Text = dt.Rows[0]["Lodgment_Date"].ToString();
                txtCustomer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
                fillCustomerMasterDetails();
                txt_AcceptanceDate.Text = dt.Rows[0]["Expt_Accept_Date"].ToString();
                txtBillAmount.Text = dt.Rows[0]["Bill_Amount"].ToString().Replace(",", "");
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
                if (dt.Rows[0]["Tenor_Days_From"].ToString() == "OTHERS/BLANK")
                {
                    txtTenor_Description.Enabled = true;
                }
                txtTenor_Description.Text = dt.Rows[0]["Tenor_Desc"].ToString();

                txtBOExchange.Text = dt.Rows[0]["BOExchange_Date"].ToString();
                txtDueDate.Text = dt.Rows[0]["Maturity_Date"].ToString();
                GetStampDutyCharges();
                ddl_Nego_Remit_Bank.SelectedValue = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
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

                //txt_CountryOfOrigin.Text = dt.Rows[0]["Country_Of_Origin"].ToString();

                ddlCountryCode.SelectedValue = dt.Rows[0]["Country_Code"].ToString();
                fill_Country_Description();
                ddlCountryOfOrigin.SelectedValue = dt.Rows[0]["Country_Of_Origin"].ToString();
                fill_CountryOfOrigin_Description();

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
                txt_Interest_Amount.Text = dt.Rows[0]["Interest_Amount"].ToString().Replace(",", "");
                ddl_Comission_Currency.SelectedValue = dt.Rows[0]["Our_Comm_Curr"].ToString();
                txtComissionAmount.Text = dt.Rows[0]["Our_Cumm_Amount"].ToString().Replace(",", "");
                ddl_Other_Currency.SelectedValue = dt.Rows[0]["Other_Comm_Curr"].ToString();
                txtOther_amount.Text = dt.Rows[0]["Other_Cumm_Amount"].ToString().Replace(",", "");
                ddl_Their_Commission_Currency.SelectedValue = dt.Rows[0]["Their_Comm_Curr"].ToString();
                txtTheirCommission_Amount.Text = dt.Rows[0]["Their_Cumm_Amount"].ToString().Replace(",", "");
                txt_Total_Bill_Amt.Text = dt.Rows[0]["Total_Bill_Amount"].ToString().Replace(",", "");

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

                if (lblCollection_Lodgment_UnderLC.Text == "Lodgment_Under_LC")
                {
                    hdnDocumentScrutiny.Value = dt.Rows[0]["Document_Scrutiny"].ToString();
                    ddl_Doc_Scrutiny.SelectedValue = dt.Rows[0]["Document_Scrutiny"].ToString();
                }

                if (dt.Rows[0]["Special_Instruction_Type"].ToString() == "NONE")
                {
                    rdb_SP_Instr_None.Checked = true;
                    rdb_SP_Instr_None_CheckedChanged(null, null);
                }
                if (dt.Rows[0]["Special_Instruction_Type"].ToString() == "OTHERS")
                {
                    rdb_SP_Instr_Other.Checked = true;
                    rdb_SP_Instr_Other_CheckedChanged(null, null);
                }
                if (dt.Rows[0]["Special_Instruction_Type"].ToString() == "ANNEXURE")
                {
                    rdb_SP_Instr_Annexure.Checked = true;
                    rdb_SP_Instr_Annexure_CheckedChanged(null, null);
                }
                if (dt.Rows[0]["Special_Instruction_Type1"].ToString() == "ON_SIGHT")
                {
                    rdb_SP_Instr_On_Sight.Checked = true;
                    rdb_SP_Instr_On_Sight_CheckedChanged(null, null);
                }
                if (dt.Rows[0]["Special_Instruction_Type1"].ToString() == "ON_DATE")
                {
                    rdb_SP_Instr_On_Date.Checked = true;
                    rdb_SP_Instr_On_Date_CheckedChanged(null, null);
                }

                txt_SP_Instructions1.Text = dt.Rows[0]["Special_Instruction1"].ToString();
                txt_SP_Instructions2.Text = dt.Rows[0]["Special_Instruction2"].ToString();
                txt_SP_Instructions3.Text = dt.Rows[0]["Special_Instruction3"].ToString();
                txt_SP_Instructions4.Text = dt.Rows[0]["Special_Instruction4"].ToString();
                txt_SP_Instructions5.Text = dt.Rows[0]["Special_Instruction5"].ToString();

                //txt_Instructions1 are hardcoded in ToggleDocType()
                //txt_Instructions1.Text = dt.Rows[0]["Instructions1"].ToString();
                //txt_Instructions2.Text = dt.Rows[0]["Instructions2"].ToString();
                //txt_Instructions3.Text = dt.Rows[0]["Instructions3"].ToString();
                //txt_Instructions4.Text = dt.Rows[0]["Instructions4"].ToString();
                //txt_Instructions5.Text = dt.Rows[0]["Instructions5"].ToString();

                //if (dt.Rows[0]["Pay_swift_Type_56"].ToString() == "56A_D")
                //{
                //   // rdb56A_D.Checked = true;
                //}
                //if (dt.Rows[0]["Pay_swift_Type_57"].ToString() == "57A_D")
                //{
                //    //rdb57A_D.Checked = true;
                //}
                //if (dt.Rows[0]["Pay_swift_Type_58"].ToString() == "58A_D")
                //{
                //  //  rdb58A_D.Checked = true;
                //}
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

                if (dt.Rows[0]["Checker_Remark"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
                }
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
        catch (Exception ex)
        {
            ////objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    [WebMethod]
    public static string AddUpdateBoe(string hdnUserName, string _BranchCode, string _DocType, string _flcIlcType, string _Sight_Usance,
        string _txtDocNo, string _txtDateReceived, string _txtLogdmentDate, string _txtCustomer_ID,
        string _txt_AcceptanceDate, string _txtBillAmount, string _ddl_Doc_Currency, string _txt_LC_No, string _txt_BRO_Ref_No, string _SGDocNo, string _DP_DA,
        string _ddlTenor, string _txtTenor, string _ddlTenor_Days_From, string _txtTenor_Description, string _txtBOExchange, string _txtDueDate,
        string _ddl_Nego_Remit_Bank_Type, string _txtNego_Remit_Bank, string _txt_Their_Ref_no, string _txtNego_Date,
        string _txtAcwithInstitution, string _txt_Swift_Code, string _txtReimbursingbank, string _txt_Inv_No,
        string _ddlDrawer, string _txt_Inv_Date,
        string _ddl_Commodity, string _txtCommodityDesc, string _ddlCountryCode, string _txt_CountryOfOrigin,
        string _txtShippingDate, string _txtDocFirst1, string _txtDocFirst2, string _txtDocFirst3,
        string _txtVesselName, string _txtDocSecond1, string _txtDocSecond2, string _txtDocSecond3,
        string _txtFromPort, string _txtToPort,
        string _ddl_Interest_Currency, string _txt_Interest_Amount, string _ddl_Comission_Currency, string _txtComissionAmount,
        string _ddl_Other_Currency, string _txtOther_amount, string _ddl_Their_Commission_Currency, string _txtTheirCommission_Amount,
        string _txt_Total_Bill_Amt, string _ddl_Doc_Scrutiny,
        string _txt_Discrepancy1, string _txt_Discrepancy2, string _txt_Discrepancy3, string _txt_Discrepancy4, string _txt_Discrepancy5,
        string _txt_Discrepancy6, string _txt_Discrepancy7, string _txt_Discrepancy8, string _txt_Discrepancy9, string _txt_Discrepancy10,
        string _txt_Discrepancy11, string _txt_Discrepancy12, string _txt_Discrepancy13, string _txt_Discrepancy14, string _txt_Discrepancy15,
        string _txt_Discrepancy16, string _txt_Discrepancy17, string _txt_Discrepancy18, string _txt_Discrepancy19, string _txt_Discrepancy20,
        string _txt_Discrepancy21, string _txt_Discrepancy22, string _txt_Discrepancy23, string _txt_Discrepancy24, string _txt_Discrepancy25,
        string _txt_Discrepancy26, string _txt_Discrepancy27, string _txt_Discrepancy28, string _txt_Discrepancy29, string _txt_Discrepancy30,
        string _txt_Discrepancy31, string _txt_Discrepancy32, string _txt_Discrepancy33, string _txt_Discrepancy34, string _txt_Discrepancy35,
        string _SWIFT_File_Type, string _ProtestFlag,
        string _txt_Narrative1, string _txt_Narrative2, string _txt_Narrative3, string _txt_Narrative4, string _txt_Narrative5,
        string _txt_Narrative6, string _txt_Narrative7, string _txt_Narrative8, string _txt_Narrative9, string _txt_Narrative10,
        string _txt_Narrative11, string _txt_Narrative12, string _txt_Narrative13, string _txt_Narrative14, string _txt_Narrative15,
        string _txt_Narrative16, string _txt_Narrative17, string _txt_Narrative18, string _txt_Narrative19, string _txt_Narrative20,
        string _txt_Narrative21, string _txt_Narrative22, string _txt_Narrative23, string _txt_Narrative24, string _txt_Narrative25,
        string _txt_Narrative26, string _txt_Narrative27, string _txt_Narrative28, string _txt_Narrative29, string _txt_Narrative30,
        string _txt_Narrative31, string _txt_Narrative32, string _txt_Narrative33, string _txt_Narrative34, string _txt_Narrative35,
        string _SP_Instructions_Type, string _SP_Instructions_Type1, string _txt_SP_Instructions1, string _txt_SP_Instructions2, string _txt_SP_Instructions3, string _txt_SP_Instructions4, string _txt_SP_Instructions5,
        string _txt_Instructions1, string _txt_Instructions2, string _txt_Instructions3, string _txt_Instructions4, string _txt_Instructions5,
        string _Instructiondate,
        string _Pay_swift_Type_56, string _txt_PaymentSwift56ACC_No, string _txt_PaymentSwift56A, string _txt_PaymentSwift56D_name, string _txt_PaymentSwift56D_Address,
        string _Pay_swift_Type_57, string _txt_PaymentSwift57ACC_No, string _txt_PaymentSwift57A, string _txt_PaymentSwift57D_name, string _txt_PaymentSwift57D_Address,
        string _Pay_swift_Type_58, string _txt_PaymentSwift58ACC_No, string _txt_PaymentSwift58A, string _txt_PaymentSwift58D_name, string _txt_PaymentSwift58D_Address,
        string _txt_PaymentSwift58D_Address2, string _txt_PaymentSwift58D_Address3, string _txt_PaymentSwift58D_Address4,
        string Stamp_Duty_Charges_Curr, string Stamp_Duty_Charges_ExRate, string Stamp_Duty_Charges_Amount, string _OwnLCDiscount_Type,
        string _txtAMLDrawee, string _txtAMLDrawer, string _txtAMLNagoRemiBank, string _txtAMLCommodity, string _txtAMLVessel, string _txtAMLFromPort, string _txtAMLCountry, string _txtCountryOfOrigin,
        string _txtAML1, string _txtAML2, string _txtAML3, string _txtAML4, string _txtAML5, string _txtAML6, string _txtAML7, string _txtAML8,
        string _txtAML9, string _txtAML10, string _txtAML11, string _txtAML12, string _txtAML13, string _txtAML14, string _txtAML15, string _txtAML16, string _txtAML17,
        string _txtAML18, string _txtAML19, string _txtAML20, string _txtAML21, string _txtAML22, string _txtAML23, string _txtAML24, string _txtAML25, string _txtAML26,
        string _txtAML27, string _txtAML28, string _txtAML29, string _txtAML30, string _chkSpecialCase, string _txt_txtRiskCust, string _txt_txtSettelementAcNo, string _txt_txtRiskCustAbbr, string LodgCumAcc
        // Swift Details
        , string _txtChargesClaimed7341, string _txtChargesClaimed7342, string _txtChargesClaimed7343, string _txtChargesClaimed7344, string _txtChargesClaimed7345, string _txtChargesClaimed7346,
        string _ddlTotalAmountClaimed734, string _txtDate734, string _txtCurrency734, string _txtAmount734,
        string _ddlAccountWithBank734, string _txtAccountWithBank734AccountNo, string _txtAccountWithBank734SwiftCode, string _txtAccountWithBank734Location, string _txtAccountWithBank734Name,
        string _txtAccountWithBank734Address1, string _txtAccountWithBank734Address2, string _txtAccountWithBank734Address3,
        string _txtSendertoReceiverInformation7341, string _txtSendertoReceiverInformation7342, string _txtSendertoReceiverInformation7343,
        string _txtSendertoReceiverInformation7344, string _txtSendertoReceiverInformation7345, string _txtSendertoReceiverInformation7346,
        string _txtAccountWithBank734PartyIdentifier,
        string _ddl_DisposalOfDoc
        )
    {
        TF_DATA obj = new TF_DATA();
        if (_Sight_Usance == "Sight")
        {
            _Sight_Usance = "S";
        }
        else
        {
            _Sight_Usance = "U";
        }
        if (_flcIlcType == "FOREIGN")
        {
            _flcIlcType = "FLC";
        }
        else
        {
            _flcIlcType = "ILC";
        }

        SqlParameter P_BranchCode = new SqlParameter("@_BranchCode", _BranchCode.ToUpper());
        SqlParameter P_DocType = new SqlParameter("@_DocType", _DocType.ToUpper());
        SqlParameter P_flcIlcType = new SqlParameter("@_flcIlcType", _flcIlcType.ToUpper());
        SqlParameter P_Sight_Usance = new SqlParameter("@_Sight_Usance", _Sight_Usance.ToUpper());
        SqlParameter P_txtDocNo = new SqlParameter("@_txtDocNo", _txtDocNo.ToUpper());
        SqlParameter P_txtDateReceived = new SqlParameter("@_txtDateReceived", _txtDateReceived.ToUpper());
        SqlParameter P_txtLogdmentDate = new SqlParameter("@_txtLogdmentDate", _txtLogdmentDate.ToUpper());
        SqlParameter P_txtCustomer_ID = new SqlParameter("@_txtCustomer_ID", _txtCustomer_ID.ToUpper());
        SqlParameter P_txt_AcceptanceDate = new SqlParameter("@_txt_AcceptanceDate", _txt_AcceptanceDate.ToUpper());
        SqlParameter P_txtBillAmount = new SqlParameter("@_txtBillAmount", _txtBillAmount.ToUpper());
        SqlParameter P_ddl_Doc_Currency = new SqlParameter("@_ddl_Doc_Currency", _ddl_Doc_Currency.ToUpper());
        SqlParameter P_txt_LC_No = new SqlParameter("@_txt_LC_No", _txt_LC_No.ToUpper());
        SqlParameter P_txt_BRO_Ref_No = new SqlParameter("@_txt_BRO_Ref_No", _txt_BRO_Ref_No.ToUpper());
        SqlParameter P_SGDocNo = new SqlParameter("@_SGDocNo", _SGDocNo.ToUpper());

        SqlParameter P_DP_DA = new SqlParameter("@_DP_DA", _DP_DA.ToUpper());
        SqlParameter P_ddlTenor = new SqlParameter("@_ddlTenor", _ddlTenor.ToUpper());
        SqlParameter P_txtTenor = new SqlParameter("@_txtTenor", _txtTenor.ToUpper());
        SqlParameter P_ddlTenor_Days_From = new SqlParameter("@_ddlTenor_Days_From", _ddlTenor_Days_From.ToUpper());
        SqlParameter P_txtTenor_Description = new SqlParameter("@_txtTenor_Description", _txtTenor_Description.ToUpper());
        SqlParameter P_txtBOExchange = new SqlParameter("@_txtBOExchange", _txtBOExchange.ToUpper());
        SqlParameter P_txtDueDate = new SqlParameter("@_txtDueDate", _txtDueDate.ToUpper());
        SqlParameter P_ddl_Nego_Remit_Bank_Type = new SqlParameter("@ddl_Nego_Remit_Bank_Type", _ddl_Nego_Remit_Bank_Type.ToUpper());
        SqlParameter P_txtNego_Remit_Bank = new SqlParameter("@_txtNego_Remit_Bank", _txtNego_Remit_Bank.ToUpper());
        SqlParameter P_txt_Their_Ref_no = new SqlParameter("@_txt_Their_Ref_no", _txt_Their_Ref_no.ToUpper());
        SqlParameter P_txtNego_Date = new SqlParameter("@_txtNego_Date", _txtNego_Date.ToUpper());
        SqlParameter P_txtAcwithInstitution = new SqlParameter("@_txtAcwithInstitution", _txtAcwithInstitution.ToUpper());
        SqlParameter P_txt_Swift_Code = new SqlParameter("@_txt_Swift_Code", _txt_Swift_Code.ToUpper());
        SqlParameter P_txtReimbursingbank = new SqlParameter("@_txtReimbursingbank", _txtReimbursingbank.ToUpper());
        SqlParameter P_txt_Inv_No = new SqlParameter("@_txt_Inv_No", _txt_Inv_No.ToUpper());
        SqlParameter P_ddlDrawer = new SqlParameter("@_txtDrawer", _ddlDrawer.ToUpper());
        SqlParameter P_txt_Inv_Date = new SqlParameter("@_txt_Inv_Date", _txt_Inv_Date.ToUpper());
        SqlParameter P_ddl_Commodity = new SqlParameter("@_ddl_Commodity", _ddl_Commodity.ToUpper());
        SqlParameter P_txtCommodityDesc = new SqlParameter("@_txtCommodityDesc", _txtCommodityDesc.ToUpper());
        SqlParameter P_ddlCountryCode = new SqlParameter("@_ddlCountryCode", _ddlCountryCode.ToUpper());
        SqlParameter P_txt_CountryOfOrigin = new SqlParameter("@_txt_CountryOfOrigin", _txt_CountryOfOrigin.ToUpper());
        SqlParameter P_txtShippingDate = new SqlParameter("@_txtShippingDate", _txtShippingDate.ToUpper());
        SqlParameter P_txtDocFirst1 = new SqlParameter("@_txtDocFirst1", _txtDocFirst1.ToUpper());
        SqlParameter P_txtDocFirst2 = new SqlParameter("@_txtDocFirst2", _txtDocFirst2.ToUpper());
        SqlParameter P_txtDocFirst3 = new SqlParameter("@_txtDocFirst3", _txtDocFirst3.ToUpper());
        SqlParameter P_txtVesselName = new SqlParameter("@_txtVesselName", _txtVesselName.ToUpper());
        SqlParameter P_txtDocSecond1 = new SqlParameter("@_txtDocSecond1", _txtDocSecond1.ToUpper());
        SqlParameter P_txtDocSecond2 = new SqlParameter("@_txtDocSecond2", _txtDocSecond2.ToUpper());
        SqlParameter P_txtDocSecond3 = new SqlParameter("@_txtDocSecond3", _txtDocSecond3.ToUpper());
        SqlParameter P_txtFromPort = new SqlParameter("@_txtFromPort", _txtFromPort.ToUpper());
        SqlParameter P_txtToPort = new SqlParameter("@_txtToPort", _txtToPort.ToUpper());
        SqlParameter P_ddl_Interest_Currency = new SqlParameter("@_ddl_Interest_Currency", _ddl_Interest_Currency.ToUpper());
        SqlParameter P_txt_Interest_Amount = new SqlParameter("@_txt_Interest_Amount", _txt_Interest_Amount.ToUpper());
        SqlParameter P_ddl_Comission_Currency = new SqlParameter("@_ddl_Comission_Currency", _ddl_Comission_Currency.ToUpper());
        SqlParameter P_txtComissionAmount = new SqlParameter("@_txtComissionAmount", _txtComissionAmount.ToUpper());
        SqlParameter P_ddl_Other_Currency = new SqlParameter("@_ddl_Other_Currency", _ddl_Other_Currency.ToUpper());
        SqlParameter P_txtOther_amount = new SqlParameter("@_txtOther_amount", _txtOther_amount.ToUpper());
        SqlParameter P_ddl_Their_Commission_Currency = new SqlParameter("@_ddl_Their_Commission_Currency", _ddl_Their_Commission_Currency.ToUpper());
        SqlParameter P_txtTheirCommission_Amount = new SqlParameter("@_txtTheirCommission_Amount", _txtTheirCommission_Amount.ToUpper());
        SqlParameter P_txt_Total_Bill_Amt = new SqlParameter("@_txt_Total_Bill_Amt", _txt_Total_Bill_Amt.ToUpper());

        SqlParameter P_ddl_Doc_Scrutiny = new SqlParameter("@_ddl_Doc_Scrutiny", _ddl_Doc_Scrutiny.ToUpper());

        SqlParameter P_txt_Discrepancy1 = new SqlParameter("@_txt_Discrepancy1", _txt_Discrepancy1.ToUpper());
        SqlParameter P_txt_Discrepancy2 = new SqlParameter("@_txt_Discrepancy2", _txt_Discrepancy2.ToUpper());
        SqlParameter P_txt_Discrepancy3 = new SqlParameter("@_txt_Discrepancy3", _txt_Discrepancy3.ToUpper());
        SqlParameter P_txt_Discrepancy4 = new SqlParameter("@_txt_Discrepancy4", _txt_Discrepancy4.ToUpper());
        SqlParameter P_txt_Discrepancy5 = new SqlParameter("@_txt_Discrepancy5", _txt_Discrepancy5.ToUpper());
        SqlParameter P_txt_Discrepancy6 = new SqlParameter("@_txt_Discrepancy6", _txt_Discrepancy6.ToUpper());
        SqlParameter P_txt_Discrepancy7 = new SqlParameter("@_txt_Discrepancy7", _txt_Discrepancy7.ToUpper());
        SqlParameter P_txt_Discrepancy8 = new SqlParameter("@_txt_Discrepancy8", _txt_Discrepancy8.ToUpper());
        SqlParameter P_txt_Discrepancy9 = new SqlParameter("@_txt_Discrepancy9", _txt_Discrepancy9.ToUpper());
        SqlParameter P_txt_Discrepancy10 = new SqlParameter("@_txt_Discrepancy10", _txt_Discrepancy10.ToUpper());
        SqlParameter P_txt_Discrepancy11 = new SqlParameter("@_txt_Discrepancy11", _txt_Discrepancy11.ToUpper());
        SqlParameter P_txt_Discrepancy12 = new SqlParameter("@_txt_Discrepancy12", _txt_Discrepancy12.ToUpper());
        SqlParameter P_txt_Discrepancy13 = new SqlParameter("@_txt_Discrepancy13", _txt_Discrepancy13.ToUpper());
        SqlParameter P_txt_Discrepancy14 = new SqlParameter("@_txt_Discrepancy14", _txt_Discrepancy14.ToUpper());
        SqlParameter P_txt_Discrepancy15 = new SqlParameter("@_txt_Discrepancy15", _txt_Discrepancy15.ToUpper());
        SqlParameter P_txt_Discrepancy16 = new SqlParameter("@_txt_Discrepancy16", _txt_Discrepancy16.ToUpper());
        SqlParameter P_txt_Discrepancy17 = new SqlParameter("@_txt_Discrepancy17", _txt_Discrepancy17.ToUpper());
        SqlParameter P_txt_Discrepancy18 = new SqlParameter("@_txt_Discrepancy18", _txt_Discrepancy18.ToUpper());
        SqlParameter P_txt_Discrepancy19 = new SqlParameter("@_txt_Discrepancy19", _txt_Discrepancy19.ToUpper());
        SqlParameter P_txt_Discrepancy20 = new SqlParameter("@_txt_Discrepancy20", _txt_Discrepancy20.ToUpper());
        SqlParameter P_txt_Discrepancy21 = new SqlParameter("@_txt_Discrepancy21", _txt_Discrepancy21.ToUpper());
        SqlParameter P_txt_Discrepancy22 = new SqlParameter("@_txt_Discrepancy22", _txt_Discrepancy22.ToUpper());
        SqlParameter P_txt_Discrepancy23 = new SqlParameter("@_txt_Discrepancy23", _txt_Discrepancy23.ToUpper());
        SqlParameter P_txt_Discrepancy24 = new SqlParameter("@_txt_Discrepancy24", _txt_Discrepancy24.ToUpper());
        SqlParameter P_txt_Discrepancy25 = new SqlParameter("@_txt_Discrepancy25", _txt_Discrepancy25.ToUpper());
        SqlParameter P_txt_Discrepancy26 = new SqlParameter("@_txt_Discrepancy26", _txt_Discrepancy26.ToUpper());
        SqlParameter P_txt_Discrepancy27 = new SqlParameter("@_txt_Discrepancy27", _txt_Discrepancy27.ToUpper());
        SqlParameter P_txt_Discrepancy28 = new SqlParameter("@_txt_Discrepancy28", _txt_Discrepancy28.ToUpper());
        SqlParameter P_txt_Discrepancy29 = new SqlParameter("@_txt_Discrepancy29", _txt_Discrepancy29.ToUpper());
        SqlParameter P_txt_Discrepancy30 = new SqlParameter("@_txt_Discrepancy30", _txt_Discrepancy30.ToUpper());
        SqlParameter P_txt_Discrepancy31 = new SqlParameter("@_txt_Discrepancy31", _txt_Discrepancy31.ToUpper());
        SqlParameter P_txt_Discrepancy32 = new SqlParameter("@_txt_Discrepancy32", _txt_Discrepancy32.ToUpper());
        SqlParameter P_txt_Discrepancy33 = new SqlParameter("@_txt_Discrepancy33", _txt_Discrepancy33.ToUpper());
        SqlParameter P_txt_Discrepancy34 = new SqlParameter("@_txt_Discrepancy34", _txt_Discrepancy34.ToUpper());
        SqlParameter P_txt_Discrepancy35 = new SqlParameter("@_txt_Discrepancy35", _txt_Discrepancy35.ToUpper());

        SqlParameter P_SwiftType = new SqlParameter("@SwiftType", _SWIFT_File_Type.ToUpper());
        SqlParameter P_ProtestFlag = new SqlParameter("@ProtestFlag", _ProtestFlag.ToUpper());

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

        SqlParameter P_SP_Instructions_Type = new SqlParameter("@_SP_Instructions_Type", _SP_Instructions_Type.ToUpper());
        SqlParameter P_SP_Instructions_Type1 = new SqlParameter("@_SP_Instructions_Type1", _SP_Instructions_Type1.ToUpper());

        SqlParameter P_txt_SP_Instructions1 = new SqlParameter("@_txt_SP_Instructions1", _txt_SP_Instructions1.ToUpper());
        SqlParameter P_txt_SP_Instructions2 = new SqlParameter("@_txt_SP_Instructions2", _txt_SP_Instructions2.ToUpper());
        SqlParameter P_txt_SP_Instructions3 = new SqlParameter("@_txt_SP_Instructions3", _txt_SP_Instructions3.ToUpper());
        SqlParameter P_txt_SP_Instructions4 = new SqlParameter("@_txt_SP_Instructions4", _txt_SP_Instructions4.ToUpper());
        SqlParameter P_txt_SP_Instructions5 = new SqlParameter("@_txt_SP_Instructions5", _txt_SP_Instructions5.ToUpper());

        SqlParameter P_txt_Instructions1 = new SqlParameter("@_txt_Instructions1", _txt_Instructions1.ToUpper());
        SqlParameter P_txt_Instructions2 = new SqlParameter("@_txt_Instructions2", _txt_Instructions2.ToUpper());
        SqlParameter P_txt_Instructions3 = new SqlParameter("@_txt_Instructions3", _txt_Instructions3.ToUpper());
        SqlParameter P_txt_Instructions4 = new SqlParameter("@_txt_Instructions4", _txt_Instructions4.ToUpper());
        SqlParameter P_txt_Instructions5 = new SqlParameter("@_txt_Instructions5", _txt_Instructions5.ToUpper());
        SqlParameter P_Instructiondate = new SqlParameter("@_Instructiondate", _Instructiondate.ToUpper());

        SqlParameter P_Pay_swift_Type_56 = new SqlParameter("@_Pay_swift_Type_56", _Pay_swift_Type_56.ToUpper());
        SqlParameter P_txt_PaymentSwift56ACC_No = new SqlParameter("@_txt_PaymentSwift56ACC_No", _txt_PaymentSwift56ACC_No.ToUpper());
        SqlParameter P_txt_PaymentSwift56A = new SqlParameter("@_txt_PaymentSwift56A", _txt_PaymentSwift56A.ToUpper());
        SqlParameter P_txt_PaymentSwift56D_name = new SqlParameter("@_txt_PaymentSwift56D_name", _txt_PaymentSwift56D_name.ToUpper());
        SqlParameter P_txt_PaymentSwift56D_Address = new SqlParameter("@_txt_PaymentSwift56D_Address", _txt_PaymentSwift56D_Address.ToUpper());

        SqlParameter P_Pay_swift_Type_57 = new SqlParameter("@_Pay_swift_Type_57", _Pay_swift_Type_57.ToUpper());
        SqlParameter P_txt_PaymentSwift57ACC_No = new SqlParameter("@_txt_PaymentSwift57ACC_No", _txt_PaymentSwift57ACC_No.ToUpper());
        SqlParameter P_txt_PaymentSwift57A = new SqlParameter("@_txt_PaymentSwift57A", _txt_PaymentSwift57A.ToUpper());
        SqlParameter P_txt_PaymentSwift57D_name = new SqlParameter("@_txt_PaymentSwift57D_name", _txt_PaymentSwift57D_name.ToUpper());
        SqlParameter P_txt_PaymentSwift57D_Address = new SqlParameter("@_txt_PaymentSwift57D_Address", _txt_PaymentSwift57D_Address.ToUpper());

        SqlParameter P_Pay_swift_Type_58 = new SqlParameter("@_Pay_swift_Type_58", _Pay_swift_Type_58.ToUpper());
        SqlParameter P_txt_PaymentSwift58ACC_No = new SqlParameter("@_txt_PaymentSwift58ACC_No", _txt_PaymentSwift58ACC_No.ToUpper());
        SqlParameter P_txt_PaymentSwift58A = new SqlParameter("@_txt_PaymentSwift58A", _txt_PaymentSwift58A.ToUpper());
        SqlParameter P_txt_PaymentSwift58D_name = new SqlParameter("@_txt_PaymentSwift58D_name", _txt_PaymentSwift58D_name.ToUpper());
        SqlParameter P_txt_PaymentSwift58D_Address = new SqlParameter("@_txt_PaymentSwift58D_Address", _txt_PaymentSwift58D_Address.ToUpper());
        SqlParameter P_txt_PaymentSwift58D_Address2 = new SqlParameter("@_txt_PaymentSwift58D_Address2", _txt_PaymentSwift58D_Address2.ToUpper());
        SqlParameter P_txt_PaymentSwift58D_Address3 = new SqlParameter("@_txt_PaymentSwift58D_Address3", _txt_PaymentSwift58D_Address3.ToUpper());
        SqlParameter P_txt_PaymentSwift58D_Address4 = new SqlParameter("@_txt_PaymentSwift58D_Address4", _txt_PaymentSwift58D_Address4.ToUpper());
        SqlParameter P_OwnLCDiscount_Type = new SqlParameter("@_OwnLCDiscount_Type", _OwnLCDiscount_Type.ToUpper());

        SqlParameter P_Stamp_Duty_Charges_Curr = new SqlParameter("@Stamp_Duty_Charges_Curr", Stamp_Duty_Charges_Curr.ToUpper());
        SqlParameter P_Stamp_Duty_Charges_ExRate = new SqlParameter("@Stamp_Duty_Charges_ExRate", Stamp_Duty_Charges_ExRate.ToUpper());
        SqlParameter P_Stamp_Duty_Charges_Amount = new SqlParameter("@Stamp_Duty_Charges_Amount", Stamp_Duty_Charges_Amount.ToUpper());

        SqlParameter P_txtAMLDrawee = new SqlParameter("@txtAMLDrawee", _txtAMLDrawee.ToUpper());
        SqlParameter P_txtAMLDrawer = new SqlParameter("@txtAMLDrawer", _txtAMLDrawer.ToUpper());
        SqlParameter P_txtAMLNagoRemiBank = new SqlParameter("@txtAMLNagoRemiBank", _txtAMLNagoRemiBank.ToUpper());
        SqlParameter P_txtAMLSwiftCode = new SqlParameter("@txtAMLSwiftCode", "");
        SqlParameter P_txtAMLCommodity = new SqlParameter("@txtAMLCommodity", _txtAMLCommodity.ToUpper());
        SqlParameter P_txtAMLVessel = new SqlParameter("@txtAMLVessel", _txtAMLVessel.ToUpper());
        SqlParameter P_txtAMLFromPort = new SqlParameter("@txtAMLFromPort", _txtAMLFromPort.ToUpper());
        SqlParameter P_txtAMLCountry = new SqlParameter("@txtAMLCountry", _txtAMLCountry.ToUpper());
        SqlParameter P_txtCountryOfOrigin = new SqlParameter("@txtCountryOfOrigin", _txtCountryOfOrigin.ToUpper());

        SqlParameter P_txtAML1 = new SqlParameter("@txtAML1", _txtAML1.ToUpper());
        SqlParameter P_txtAML2 = new SqlParameter("@txtAML2", _txtAML2.ToUpper());
        SqlParameter P_txtAML3 = new SqlParameter("@txtAML3", _txtAML3.ToUpper());
        SqlParameter P_txtAML4 = new SqlParameter("@txtAML4", _txtAML4.ToUpper());
        SqlParameter P_txtAML5 = new SqlParameter("@txtAML5", _txtAML5.ToUpper());
        SqlParameter P_txtAML6 = new SqlParameter("@txtAML6", _txtAML6.ToUpper());
        SqlParameter P_txtAML7 = new SqlParameter("@txtAML7", _txtAML7.ToUpper());
        SqlParameter P_txtAML8 = new SqlParameter("@txtAML8", _txtAML8.ToUpper());
        SqlParameter P_txtAML9 = new SqlParameter("@txtAML9", _txtAML9.ToUpper());
        SqlParameter P_txtAML10 = new SqlParameter("@txtAML10", _txtAML10.ToUpper());
        SqlParameter P_txtAML11 = new SqlParameter("@txtAML11", _txtAML11.ToUpper());
        SqlParameter P_txtAML12 = new SqlParameter("@txtAML12", _txtAML12.ToUpper());
        SqlParameter P_txtAML13 = new SqlParameter("@txtAML13", _txtAML13.ToUpper());
        SqlParameter P_txtAML14 = new SqlParameter("@txtAML14", _txtAML14.ToUpper());
        SqlParameter P_txtAML15 = new SqlParameter("@txtAML15", _txtAML15.ToUpper());
        SqlParameter P_txtAML16 = new SqlParameter("@txtAML16", _txtAML16.ToUpper());
        SqlParameter P_txtAML17 = new SqlParameter("@txtAML17", _txtAML17.ToUpper());
        SqlParameter P_txtAML18 = new SqlParameter("@txtAML18", _txtAML18.ToUpper());
        SqlParameter P_txtAML19 = new SqlParameter("@txtAML19", _txtAML19.ToUpper());
        SqlParameter P_txtAML20 = new SqlParameter("@txtAML20", _txtAML20.ToUpper());
        SqlParameter P_txtAML21 = new SqlParameter("@txtAML21", _txtAML21.ToUpper());
        SqlParameter P_txtAML22 = new SqlParameter("@txtAML22", _txtAML22.ToUpper());
        SqlParameter P_txtAML23 = new SqlParameter("@txtAML23", _txtAML23.ToUpper());
        SqlParameter P_txtAML24 = new SqlParameter("@txtAML24", _txtAML24.ToUpper());
        SqlParameter P_txtAML25 = new SqlParameter("@txtAML25", _txtAML25.ToUpper());
        SqlParameter P_txtAML26 = new SqlParameter("@txtAML26", _txtAML26.ToUpper());
        SqlParameter P_txtAML27 = new SqlParameter("@txtAML27", _txtAML27.ToUpper());
        SqlParameter P_txtAML28 = new SqlParameter("@txtAML28", _txtAML28.ToUpper());
        SqlParameter P_txtAML29 = new SqlParameter("@txtAML29", _txtAML29.ToUpper());
        SqlParameter P_txtAML30 = new SqlParameter("@txtAML30", _txtAML30.ToUpper());

        SqlParameter P_chkSpecialCase = new SqlParameter("@chkSpecialCase", _chkSpecialCase.ToUpper());
        SqlParameter P_txt_txtRiskCust = new SqlParameter("@txt_txtRiskCust", _txt_txtRiskCust.ToUpper());
        SqlParameter P_txt_txtSettelementAcNo = new SqlParameter("@txt_txtSettelementAcNo", _txt_txtSettelementAcNo.ToUpper());
        SqlParameter P_txt_txtRiskCustAbbr = new SqlParameter("@txt_txtRiskCustAbbr", _txt_txtRiskCustAbbr.ToUpper());
        SqlParameter P_LodgCumAcc = new SqlParameter("@LodgCumAcc", LodgCumAcc.ToUpper());

        //Swift Details

        SqlParameter P_txtChargesClaimed7341 = new SqlParameter("@_txtChargesClaimed7341", _txtChargesClaimed7341.ToUpper());
        SqlParameter P_txtChargesClaimed7342 = new SqlParameter("@_txtChargesClaimed7342", _txtChargesClaimed7342.ToUpper());
        SqlParameter P_txtChargesClaimed7343 = new SqlParameter("@_txtChargesClaimed7343", _txtChargesClaimed7343.ToUpper());
        SqlParameter P_txtChargesClaimed7344 = new SqlParameter("@_txtChargesClaimed7344", _txtChargesClaimed7344.ToUpper());
        SqlParameter P_txtChargesClaimed7345 = new SqlParameter("@_txtChargesClaimed7345", _txtChargesClaimed7345.ToUpper());
        SqlParameter P_txtChargesClaimed7346 = new SqlParameter("@_txtChargesClaimed7346", _txtChargesClaimed7346.ToUpper());
        SqlParameter P_ddlTotalAmountClaimed734 = new SqlParameter("@_ddlTotalAmountClaimed734", _ddlTotalAmountClaimed734.ToUpper());
        SqlParameter P_txtDate734 = new SqlParameter("@_txtDate734", _txtDate734.ToUpper());
        SqlParameter P_txtCurrency734 = new SqlParameter("@_txtCurrency734", _txtCurrency734.ToUpper());
        SqlParameter P_txtAmount734 = new SqlParameter("@_txtAmount734", _txtAmount734.ToUpper());
        SqlParameter P_ddlAccountWithBank734 = new SqlParameter("@_ddlAccountWithBank734", _ddlAccountWithBank734.ToUpper());
        SqlParameter P_txtAccountWithBank734AccountNo = new SqlParameter("@_txtAccountWithBank734AccountNo", _txtAccountWithBank734AccountNo.ToUpper());
        SqlParameter P_txtAccountWithBank734SwiftCode = new SqlParameter("@_txtAccountWithBank734SwiftCode", _txtAccountWithBank734SwiftCode.ToUpper());
        SqlParameter P_txtAccountWithBank734Location = new SqlParameter("@_txtAccountWithBank734Location", _txtAccountWithBank734Location.ToUpper());
        SqlParameter P_txtAccountWithBank734Name = new SqlParameter("@_txtAccountWithBank734Name", _txtAccountWithBank734Name.ToUpper());
        SqlParameter P_txtAccountWithBank734Address1 = new SqlParameter("@_txtAccountWithBank734Address1", _txtAccountWithBank734Address1.ToUpper());
        SqlParameter P_txtAccountWithBank734Address2 = new SqlParameter("@_txtAccountWithBank734Address2", _txtAccountWithBank734Address2.ToUpper());
        SqlParameter P_txtAccountWithBank734Address3 = new SqlParameter("@_txtAccountWithBank734Address3", _txtAccountWithBank734Address3.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation7341 = new SqlParameter("@_txtSendertoReceiverInformation7341", _txtSendertoReceiverInformation7341.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation7342 = new SqlParameter("@_txtSendertoReceiverInformation7342", _txtSendertoReceiverInformation7342.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation7343 = new SqlParameter("@_txtSendertoReceiverInformation7343", _txtSendertoReceiverInformation7343.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation7344 = new SqlParameter("@_txtSendertoReceiverInformation7344", _txtSendertoReceiverInformation7344.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation7345 = new SqlParameter("@_txtSendertoReceiverInformation7345", _txtSendertoReceiverInformation7345.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation7346 = new SqlParameter("@_txtSendertoReceiverInformation7346", _txtSendertoReceiverInformation7346.ToUpper());
        SqlParameter P_txtAccountWithBank734PartyIdentifier = new SqlParameter("@_txtAccountWithBank734PartyIdentifier", _txtAccountWithBank734PartyIdentifier.ToUpper());

        SqlParameter P_ddl_DisposalOfDoc = new SqlParameter("@_ddl_DisposalOfDoc", _ddl_DisposalOfDoc.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));


        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdateBOEData", P_BranchCode, P_DocType, P_flcIlcType, P_Sight_Usance, P_txtDocNo, P_txtDateReceived, P_txtLogdmentDate,
            P_txtCustomer_ID, P_txt_AcceptanceDate, P_txtBillAmount, P_ddl_Doc_Currency, P_txt_LC_No, P_txt_BRO_Ref_No, P_SGDocNo, P_DP_DA,
            P_ddlTenor, P_txtTenor, P_ddlTenor_Days_From, P_txtTenor_Description, P_txtBOExchange, P_txtDueDate,
            P_ddl_Nego_Remit_Bank_Type, P_txtNego_Remit_Bank, P_txt_Their_Ref_no, P_txtNego_Date, P_txtAcwithInstitution, P_txt_Swift_Code, P_txtReimbursingbank,
            P_txt_Inv_No, P_ddlDrawer, P_txt_Inv_Date, P_ddl_Commodity, P_txtCommodityDesc, P_ddlCountryCode,
            P_txt_CountryOfOrigin, P_txtShippingDate,
            P_txtDocFirst1, P_txtDocFirst2, P_txtDocFirst3, P_txtVesselName, P_txtDocSecond1, P_txtDocSecond2, P_txtDocSecond3,
            P_txtFromPort, P_txtToPort, P_ddl_Interest_Currency, P_txt_Interest_Amount, P_ddl_Comission_Currency, P_txtComissionAmount,
            P_ddl_Other_Currency, P_txtOther_amount, P_ddl_Their_Commission_Currency, P_txtTheirCommission_Amount, P_txt_Total_Bill_Amt,

            P_txt_Discrepancy1, P_txt_Discrepancy2, P_txt_Discrepancy3, P_txt_Discrepancy4, P_txt_Discrepancy5, P_txt_Discrepancy6, P_txt_Discrepancy7, P_txt_Discrepancy8, P_txt_Discrepancy9, P_txt_Discrepancy10,
            P_txt_Discrepancy11, P_txt_Discrepancy12, P_txt_Discrepancy13, P_txt_Discrepancy14, P_txt_Discrepancy15, P_txt_Discrepancy16, P_txt_Discrepancy17, P_txt_Discrepancy18, P_txt_Discrepancy19, P_txt_Discrepancy20,
            P_txt_Discrepancy21, P_txt_Discrepancy22, P_txt_Discrepancy23, P_txt_Discrepancy24, P_txt_Discrepancy25, P_txt_Discrepancy26, P_txt_Discrepancy27, P_txt_Discrepancy28, P_txt_Discrepancy29, P_txt_Discrepancy30,
            P_txt_Discrepancy31, P_txt_Discrepancy32, P_txt_Discrepancy33, P_txt_Discrepancy34, P_txt_Discrepancy35,

            P_txt_Narrative1, P_txt_Narrative2, P_txt_Narrative3, P_txt_Narrative4, P_txt_Narrative5, P_txt_Narrative6, P_txt_Narrative7, P_txt_Narrative8, P_txt_Narrative9, P_txt_Narrative10,
            P_txt_Narrative11, P_txt_Narrative12, P_txt_Narrative13, P_txt_Narrative14, P_txt_Narrative15, P_txt_Narrative16, P_txt_Narrative17, P_txt_Narrative18, P_txt_Narrative19, P_txt_Narrative20,
            P_txt_Narrative21, P_txt_Narrative22, P_txt_Narrative23, P_txt_Narrative24, P_txt_Narrative25, P_txt_Narrative26, P_txt_Narrative27, P_txt_Narrative28, P_txt_Narrative29, P_txt_Narrative30,
            P_txt_Narrative31, P_txt_Narrative32, P_txt_Narrative33, P_txt_Narrative34, P_txt_Narrative35,

            P_ddl_Doc_Scrutiny, P_txt_Instructions1, P_txt_Instructions2, P_txt_Instructions3, P_txt_Instructions4, P_txt_Instructions5, P_Instructiondate,
            P_SP_Instructions_Type, P_SP_Instructions_Type1, P_txt_SP_Instructions1, P_txt_SP_Instructions2, P_txt_SP_Instructions3, P_txt_SP_Instructions4, P_txt_SP_Instructions5,
            P_SwiftType, P_ProtestFlag,
            P_Pay_swift_Type_56, P_txt_PaymentSwift56ACC_No, P_txt_PaymentSwift56A, P_txt_PaymentSwift56D_name, P_txt_PaymentSwift56D_Address,
            P_Pay_swift_Type_57, P_txt_PaymentSwift57ACC_No, P_txt_PaymentSwift57A, P_txt_PaymentSwift57D_name, P_txt_PaymentSwift57D_Address,
            P_Pay_swift_Type_58, P_txt_PaymentSwift58ACC_No, P_txt_PaymentSwift58A, P_txt_PaymentSwift58D_name, P_txt_PaymentSwift58D_Address,
            P_txt_PaymentSwift58D_Address2, P_txt_PaymentSwift58D_Address3, P_txt_PaymentSwift58D_Address4, P_OwnLCDiscount_Type,
            P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate, P_Stamp_Duty_Charges_Curr, P_Stamp_Duty_Charges_ExRate, P_Stamp_Duty_Charges_Amount,
            P_txtAMLDrawee, P_txtAMLDrawer, P_txtAMLNagoRemiBank, P_txtAMLSwiftCode, P_txtAMLCommodity, P_txtAMLVessel, P_txtAMLFromPort, P_txtAMLCountry, P_txtCountryOfOrigin, P_txtAML1, P_txtAML2, P_txtAML3,
            P_txtAML4, P_txtAML5, P_txtAML6, P_txtAML7, P_txtAML8, P_txtAML9, P_txtAML10, P_txtAML11, P_txtAML12, P_txtAML13, P_txtAML14, P_txtAML15, P_txtAML16, P_txtAML17,
            P_txtAML18, P_txtAML19, P_txtAML20, P_txtAML21, P_txtAML22, P_txtAML23, P_txtAML24, P_txtAML25, P_txtAML26, P_txtAML27, P_txtAML28, P_txtAML29, P_txtAML30,
            P_chkSpecialCase, P_txt_txtRiskCust, P_txt_txtSettelementAcNo, P_txt_txtRiskCustAbbr, P_LodgCumAcc,
            P_txtChargesClaimed7341, P_txtChargesClaimed7342, P_txtChargesClaimed7343, P_txtChargesClaimed7344, P_txtChargesClaimed7345, P_txtChargesClaimed7346,
            P_ddlTotalAmountClaimed734, P_txtDate734, P_txtCurrency734, P_txtAmount734, P_ddlAccountWithBank734, P_txtAccountWithBank734AccountNo,
            P_txtAccountWithBank734SwiftCode, P_txtAccountWithBank734Location, P_txtAccountWithBank734Name, P_txtAccountWithBank734Address1, P_txtAccountWithBank734Address2,
            P_txtAccountWithBank734Address3, P_txtSendertoReceiverInformation7341, P_txtSendertoReceiverInformation7342, P_txtSendertoReceiverInformation7343,
            P_txtSendertoReceiverInformation7344, P_txtSendertoReceiverInformation7345, P_txtSendertoReceiverInformation7346, P_txtAccountWithBank734PartyIdentifier,
            P_ddl_DisposalOfDoc);
        return _Result;
    }
    [WebMethod]
    public static string AddUpdateLCData(string hdnCustAbbr, string hdnLCREF1, string hdnLCREF2, string hdnLCCurrency, string hdnLCAppNo, string hdnLCCountry, string hdnLCREF3, string txtCustomer_ID, string txtDocNo, string hdnUserName, string txtBillAmount, string hdnLCCommCode)
    {
        TF_DATA obj = new TF_DATA();
        string query = "TF_IMP_AddUpdateLCData";
        SqlParameter p1 = new SqlParameter("@hdnCustAbbr", hdnCustAbbr);
        SqlParameter p2 = new SqlParameter("@hdnLCREF1", hdnLCREF1);
        SqlParameter p3 = new SqlParameter("@hdnLCREF2", hdnLCREF2);
        SqlParameter p4 = new SqlParameter("@hdnLCCurrency", hdnLCCurrency);
        SqlParameter p5 = new SqlParameter("@hdnLCAppNo", hdnLCAppNo);
        SqlParameter p6 = new SqlParameter("@hdnLCCountry", hdnLCCountry);
        SqlParameter p7 = new SqlParameter("@hdnLCCommCode", hdnLCCommCode);
        SqlParameter p8 = new SqlParameter("@hdnLCREF3", hdnLCREF3);
        SqlParameter p9 = new SqlParameter("@txtCustomer_ID", txtCustomer_ID);
        SqlParameter p10 = new SqlParameter("@txtDocNo", txtDocNo);
        SqlParameter p11 = new SqlParameter("@LodgeAmount", txtBillAmount);
        SqlParameter p12 = new SqlParameter("@UserName", hdnUserName);

        string Result = obj.SaveDeleteData(query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
        return Result;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Lei_Audit();
        //try
        //{
        //    TF_DATA obj = new TF_DATA();
        //    SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        //    string Result = obj.SaveDeleteData("TF_IMP_SubmitForChecker", P_DocNo);
        //    if (Result == "Submit")
        //    {
        //        ////objIMP.CreateUserLog(hdnUserName.Value, "Sent document(" + txtDocNo.Text + ") to checker.");
        //        string _script = "";
        //        _script = "window.location='TF_IMP_BOE_Maker_View.aspx?result=Submit'";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        //}
    }
    //------------------Added by bhupen for LEI--------------------------//
    protected void btn_Verify_Click(object sender, EventArgs e)
    {
        try
        { 
            if(txtCustomer_ID.Text.Trim() == "")
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
            else if (ddlDrawer.SelectedItem.Text == "Select")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Select Drawer for Verifying LEI details.')", true);
                ddlDrawer.Focus();
            }
            else
            {
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
    private void ToggleDocTypeAdd(string DocType)
    {
        try
        {
            if (Request.QueryString["flcIlcType"].ToString() == "LOCAL")
            {
                ddlCountryOfOrigin.SelectedItem.Value = "IN";
                ddlCountryOfOrigin.SelectedItem.Text = "IN-INDIA";
                lblCountryOfOriginDesc.Text = "INDIA";
                txtCountryOfOrigin.Text = "INDIA";   //aml Counrty of origin.
                ddl_Doc_Currency.SelectedItem.Value = "INR";
                ddl_Doc_Currency.SelectedItem.Text = "INR";
                //ddl_Doc_Currency.Enabled = false;
            }
            if (ddl_Doc_Scrutiny.SelectedValue == "2")
            {
                //panel_AddDiscrepancy.Style.Add("display", "block");
                divDisc.Style.Add("display", "block");
            }
            if (lblCollection_Lodgment_UnderLC.Text == "Collection")
            {
                rowRiskCust.Visible = false;
            }
            switch (DocType)
            {
                case "ICA": //Collection_Sight
                    tbSwift.Visible = false;
                    panal_LC_No.Visible = false;
                    panal_Scrutiny.Visible = false;
                    panal_Stamp_Duty_Charges.Visible = true;
                    panelDP_DA.Visible = true;
                    cb_Protest.Visible = true;

                    rbtDP.Checked = true;
                    //-------Tenor
                    ddlTenor.SelectedValue = "1";
                    ddlTenor.Enabled = false;
                    txtTenor.Enabled = false;
                    ddlTenor_Days_From.Enabled = false;
                    txtTenor_Description.Enabled = false;
                    //-------expected acceptance Date/Payment Date
                    lbl_AcceptanceDate.Text = "Payment Date :";
                    //txt_AcceptanceDate.Enabled = false;
                    btncal_Accept_Date.Enabled = false;
                    //-------BOExchange Date
                    //txtBOExchange.Enabled = false;
                    //btnCal_BOExDate.Enabled = false;
                    //-------Nego date
                    //txtNego_Date.Enabled = false;
                    //btnCal_Nego_Date.Enabled = false;
                    //-------Reimbursing Bank
                    txtReimbursingbank.Enabled = false;
                    btn_Reimbursingbank.Enabled = false;
                    //------Discrepancy
                    CPE_Discrepancy.Collapsed = true;
                    btn_DiscrepancyList.Enabled = false;
                    //------Swift type
                    rdb_MT734.Visible = false;
                    rdb_MT799.Visible = false;
                    PanelPaymentSwiftDetail.Visible = true;

                    //Sp instructions as per sumana suggested
                    rdb_SP_Instr_None.Enabled = false;
                    rdb_SP_Instr_Annexure.Enabled = false;
                    rdb_SP_Instr_On_Date.Enabled = false;
                    rdb_SP_Instr_On_Sight.Enabled = false;
                    rdb_SP_Instr_Other.Enabled = false;

                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN")
                    {
                        //Foreign ICA
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE";
                        txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE";
                        txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                        txt_Instructions4.Text = "KINDLY PROVIDE YOUR INSTRUCTIONS FOR SETTLEMENT ON";
                        txt_Instructions5.Text = "OR BEFORE BY " + txt_AcceptanceDate.Text;
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL")
                    {
                        //Local ICA
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS";
                        txt_Instructions3.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions4.Text = "";
                        txt_Instructions5.Text = "";
                    }
                    break;

                case "ICU": //Collection_Usance
                    //Scrutiny
                    tbSwift.Visible = false;
                    panal_LC_No.Visible = false;
                    panal_Scrutiny.Visible = false;
                    panal_Stamp_Duty_Charges.Visible = true;
                    panelDP_DA.Visible = true;
                    cb_Protest.Visible = true;
                    rbtDA.Checked = true;
                    //Acceptance Date
                    lbl_AcceptanceDate.Text = "Exp Acceptance Date :";
                    //txt_AcceptanceDate.Enabled = false;
                    btncal_Accept_Date.Enabled = false;
                    //------Discrepancy
                    CPE_Discrepancy.Collapsed = true;
                    btn_DiscrepancyList.Enabled = false;
                    //swift type
                    rdb_MT734.Visible = false;
                    rdb_MT799.Visible = false;
                    //-------Nego date
                    //txtNego_Date.Enabled = false;
                    //btnCal_Nego_Date.Enabled = false;
                    //-------Reimbursing Bank
                    txtReimbursingbank.Enabled = false;
                    btn_Reimbursingbank.Enabled = false;
                    PanelPaymentSwiftDetail.Visible = true;
                    //Sp instructions as per sumana suggested
                    rdb_SP_Instr_None.Enabled = false;
                    rdb_SP_Instr_Annexure.Enabled = false;
                    rdb_SP_Instr_On_Date.Enabled = false;
                    rdb_SP_Instr_On_Sight.Enabled = false;
                    rdb_SP_Instr_Other.Enabled = false;
                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN")
                    {//Foreign ICU

                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE";
                        txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE";
                        txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions4.Text = "KINDLY PROVIDE YOUR ACCEPTANCE BY " + txt_AcceptanceDate.Text;
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL")
                    {//Local ICU
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR ACCEPTANCE BY " + txt_AcceptanceDate.Text;
                        txt_Instructions3.Text = "";
                        txt_Instructions4.Text = "";
                        txt_Instructions5.Text = "";
                    }
                    break;

                case "IBA": //LodgmentUnderLC_Sight
                    tbSwift.Visible = true;
                    panelDP_DA.Visible = false;
                    cb_Protest.Visible = false;
                    //-------Tenor
                    ddlTenor.SelectedValue = "1";
                    ddlTenor.Enabled = false;
                    txtTenor.Enabled = false;
                    ddlTenor_Days_From.Enabled = false;
                    txtTenor_Description.Enabled = false;
                    //-------expected acceptance Date/Payment Date
                    lbl_AcceptanceDate.Text = "Payment Date :";
                    //txt_AcceptanceDate.Enabled = false;
                    btncal_Accept_Date.Enabled = false;
                    //-------BOExchange Date
                    //txtBOExchange.Enabled = false;
                    //btnCal_BOExDate.Enabled = false;
                    //-------Swift type
                    rdb_MT499.Visible = false;
                    //cb_Protest.Visible = false;
                    //PanelPaymentSwiftDetail.Visible = false;
                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Foreign IBA Without DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST PAYMENT. KINDLY PROVIDE YOUR";
                        txt_Instructions4.Text = "SETTLEMENT INSTRUCTIONS AS PER UCP 600";
                        txt_Instructions5.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Foreign IBA With DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST PAYMENT. IF THE DISCREPANCIES ARE";
                        txt_Instructions4.Text = "ACCEPTABLE, KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS AS";
                        txt_Instructions5.Text = "PER ARTICLE 16(D) OF UCP 600 BY(5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Local IBA Without DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS ";
                        txt_Instructions3.Text = "AS PER UCP 600 BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions4.Text = "";
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Local IBA With DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT. IF THE";
                        txt_Instructions2.Text = "DISCREPANCIES ARE ACCEPTABLE,KINDLY PROVIDE YOUR";
                        txt_Instructions3.Text = "SETTLEMENT INSTRUCTIONS AS PER ARTICLE 16(D) OF UCP 600.";
                        txt_Instructions4.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions5.Text = "";
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
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
                    panelDP_DA.Visible = false;
                    cb_Protest.Visible = false;
                    rdb_MT499.Visible = false;
                    //cb_Protest.Visible = false;
                    lbl_AcceptanceDate.Text = "Exp Acceptance Date :";
                    //PanelPaymentSwiftDetail.Visible = false;

                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Foreign ACC Without DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST ACCEPTANCE. KINDLY PROVIDE";
                        txt_Instructions4.Text = "YOUR ACCEPTANCE AS PER UCP 600 BY(5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Foreign ACC With DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST ACCEPTANCE. IF THE DISCREPANCIES";
                        txt_Instructions4.Text = "ARE ACCEPTED BY YOU KINDLY PROVIDE YOUR ACCEPTANCE";
                        txt_Instructions5.Text = "AS PER ARTICLE 16(D) OF UCP 600, BY " + txt_AcceptanceDate.Text;
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Local ACC Without DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "KINDLY PROVIDE YOUR ACCEPTANCE AS PER UCP 600";
                        txt_Instructions2.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions4.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Local ACC With DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "IF THE DISCREPANCIES ARE ACCEPTABLE,";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR ACCEPTANCE AS PER";
                        txt_Instructions3.Text = "ARTICLE 16(D) OF UCP 600 BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions4.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions5.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void ToggleDocTypeEdit(string DocType)
    {
        try
        {
            if (ddl_Doc_Scrutiny.SelectedValue == "2")
            {
                //panel_AddDiscrepancy.Style.Add("display", "block");
                divDisc.Style.Add("display", "block");
            }
            if (lblCollection_Lodgment_UnderLC.Text == "Collection")
            {
                rowRiskCust.Visible = false;
            }
            switch (DocType)
            {
                case "ICA": //Collection_Sight
                    tbSwift.Visible = false;
                    panal_LC_No.Visible = false;
                    panal_Scrutiny.Visible = false;
                    panal_Stamp_Duty_Charges.Visible = true;
                    panelDP_DA.Visible = true;
                    cb_Protest.Visible = true;

                    rbtDP.Checked = true;
                    //-------Tenor
                    ddlTenor.SelectedValue = "1";
                    ddlTenor.Enabled = false;
                    txtTenor.Enabled = false;
                    ddlTenor_Days_From.Enabled = false;
                    txtTenor_Description.Enabled = false;
                    //-------expected acceptance Date/Payment Date
                    lbl_AcceptanceDate.Text = "Payment Date :";
                    //txt_AcceptanceDate.Enabled = false;
                    btncal_Accept_Date.Enabled = false;
                    //-------BOExchange Date
                    //txtBOExchange.Enabled = false;
                    //btnCal_BOExDate.Enabled = false;
                    //-------Nego date
                    //txtNego_Date.Enabled = false;
                    //btnCal_Nego_Date.Enabled = false;
                    //-------Reimbursing Bank
                    txtReimbursingbank.Enabled = false;
                    btn_Reimbursingbank.Enabled = false;
                    //------Discrepancy
                    CPE_Discrepancy.Collapsed = true;
                    btn_DiscrepancyList.Enabled = false;
                    //------Swift type
                    rdb_MT734.Visible = false;
                    rdb_MT799.Visible = false;
                    PanelPaymentSwiftDetail.Visible = true;

                    //Sp instructions as per sumana suggested
                    rdb_SP_Instr_None.Enabled = false;
                    rdb_SP_Instr_Annexure.Enabled = false;
                    rdb_SP_Instr_On_Date.Enabled = false;
                    rdb_SP_Instr_On_Sight.Enabled = false;
                    rdb_SP_Instr_Other.Enabled = false;

                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN")
                    {
                        //Foreign ICA
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE";
                        txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE";
                        txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                        txt_Instructions4.Text = "KINDLY PROVIDE YOUR INSTRUCTIONS FOR SETTLEMENT ON";
                        txt_Instructions5.Text = "OR BEFORE BY " + txt_AcceptanceDate.Text;
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL")
                    {
                        //Local ICA
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS";
                        txt_Instructions3.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions4.Text = "";
                        txt_Instructions5.Text = "";
                    }
                    break;

                case "ICU": //Collection_Usance
                    //Scrutiny
                    tbSwift.Visible = false;
                    panal_LC_No.Visible = false;
                    panal_Scrutiny.Visible = false;
                    panal_Stamp_Duty_Charges.Visible = true;
                    panelDP_DA.Visible = true;
                    cb_Protest.Visible = true;
                    rbtDA.Checked = true;
                    //Acceptance Date
                    lbl_AcceptanceDate.Text = "Exp Acceptance Date :";
                    //txt_AcceptanceDate.Enabled = false;
                    btncal_Accept_Date.Enabled = false;
                    //------Discrepancy
                    CPE_Discrepancy.Collapsed = true;
                    btn_DiscrepancyList.Enabled = false;
                    //swift type
                    rdb_MT734.Visible = false;
                    rdb_MT799.Visible = false;
                    //-------Nego date
                    //txtNego_Date.Enabled = false;
                    //btnCal_Nego_Date.Enabled = false;
                    //-------Reimbursing Bank
                    txtReimbursingbank.Enabled = false;
                    btn_Reimbursingbank.Enabled = false;
                    PanelPaymentSwiftDetail.Visible = true;
                    //Sp instructions as per sumana suggested
                    rdb_SP_Instr_None.Enabled = false;
                    rdb_SP_Instr_Annexure.Enabled = false;
                    rdb_SP_Instr_On_Date.Enabled = false;
                    rdb_SP_Instr_On_Sight.Enabled = false;
                    rdb_SP_Instr_Other.Enabled = false;
                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN")
                    {//Foreign ICU

                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE";
                        txt_Instructions2.Text = "SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE";
                        txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions4.Text = "KINDLY PROVIDE YOUR ACCEPTANCE BY " + txt_AcceptanceDate.Text;
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL")
                    {//Local ICU
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR ACCEPTANCE BY " + txt_AcceptanceDate.Text;
                        txt_Instructions3.Text = "";
                        txt_Instructions4.Text = "";
                        txt_Instructions5.Text = "";
                    }
                    break;

                case "IBA": //LodgmentUnderLC_Sight
                    tbSwift.Visible = true;
                    panelDP_DA.Visible = false;
                    cb_Protest.Visible = false;
                    //-------Tenor
                    ddlTenor.SelectedValue = "1";
                    ddlTenor.Enabled = false;
                    txtTenor.Enabled = false;
                    ddlTenor_Days_From.Enabled = false;
                    txtTenor_Description.Enabled = false;
                    //-------expected acceptance Date/Payment Date
                    lbl_AcceptanceDate.Text = "Payment Date :";
                    //txt_AcceptanceDate.Enabled = false;
                    btncal_Accept_Date.Enabled = false;
                    //-------BOExchange Date
                    //txtBOExchange.Enabled = false;
                    //btnCal_BOExDate.Enabled = false;
                    //-------Swift type
                    rdb_MT499.Visible = false;
                    //cb_Protest.Visible = false;
                    //PanelPaymentSwiftDetail.Visible = false;
                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Foreign IBA Without DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST PAYMENT. KINDLY PROVIDE YOUR";
                        txt_Instructions4.Text = "SETTLEMENT INSTRUCTIONS AS PER UCP 600";
                        txt_Instructions5.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Foreign IBA With DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST PAYMENT. IF THE DISCREPANCIES ARE";
                        txt_Instructions4.Text = "ACCEPTABLE, KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS AS";
                        txt_Instructions5.Text = "PER ARTICLE 16(D) OF UCP 600 BY(5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Local IBA Without DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT.";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS ";
                        txt_Instructions3.Text = "AS PER UCP 600 BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions4.Text = "";
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Local IBA With DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "WE DELIVER THE DOCUMENTS AGAINST PAYMENT. IF THE";
                        txt_Instructions2.Text = "DISCREPANCIES ARE ACCEPTABLE,KINDLY PROVIDE YOUR";
                        txt_Instructions3.Text = "SETTLEMENT INSTRUCTIONS AS PER ARTICLE 16(D) OF UCP 600.";
                        txt_Instructions4.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions5.Text = "";
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
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
                    panelDP_DA.Visible = false;
                    cb_Protest.Visible = false;
                    rdb_MT499.Visible = false;
                    //cb_Protest.Visible = false;
                    lbl_AcceptanceDate.Text = "Exp Acceptance Date :";
                    //PanelPaymentSwiftDetail.Visible = false;

                    if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Foreign ACC Without DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST ACCEPTANCE. KINDLY PROVIDE";
                        txt_Instructions4.Text = "YOUR ACCEPTANCE AS PER UCP 600 BY(5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "FOREIGN" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Foreign ACC With DISCREPANCY
                        txt_Instructions1.Text = "AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED";
                        txt_Instructions2.Text = "WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER";
                        txt_Instructions3.Text = "THE DOCUMENTS AGAINST ACCEPTANCE. IF THE DISCREPANCIES";
                        txt_Instructions4.Text = "ARE ACCEPTED BY YOU KINDLY PROVIDE YOUR ACCEPTANCE";
                        txt_Instructions5.Text = "AS PER ARTICLE 16(D) OF UCP 600, BY " + txt_AcceptanceDate.Text;
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "1")
                    {
                        //Local ACC Without DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "KINDLY PROVIDE YOUR ACCEPTANCE AS PER UCP 600";
                        txt_Instructions2.Text = "BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions3.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions4.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                        txt_Instructions5.Text = "";
                    }
                    else if (Request.QueryString["flcIlcType"].ToString() == "LOCAL" && ddl_Doc_Scrutiny.SelectedValue == "2")
                    {
                        //Local ACC With DISCREPANCY
                        Exp_AcceptanceDate();
                        Duedate();
                        txt_Instructions1.Text = "IF THE DISCREPANCIES ARE ACCEPTABLE,";
                        txt_Instructions2.Text = "KINDLY PROVIDE YOUR ACCEPTANCE AS PER";
                        txt_Instructions3.Text = "ARTICLE 16(D) OF UCP 600 BY (5 BANKING DAYS) " + txt_AcceptanceDate.Text;
                        txt_Instructions4.Text = "WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.";
                        txt_Instructions5.Text = "KINDLY MAKE YOUR PAYMENT ON DUE DATE.";
                        rdb_SP_Instr_Annexure.Checked = true;
                        rdb_SP_Instr_Annexure_CheckedChanged(null, null);
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
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
            DataTable dt = objData.getData(_query, p1,p2);
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
            string _query = "TF_IMP_GetLEIDetails_ExpiryDate";
            DataTable dt = objData.getData(_query, p1,p2,p3);
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
            SqlParameter p1 = new SqlParameter("@CurrCode", SqlDbType.VarChar);
            p1.Value = ddl_Doc_Currency.SelectedValue.ToString();
            SqlParameter p2 = new SqlParameter("@Date", SqlDbType.VarChar);
            p2.Value = txtLogdmentDate.Text.ToString();
            string _query = "TF_IMP_GetLEI_RateCardDetails";
            DataTable dt = objData.getData(_query, p1 , p2);
            if (dt.Rows.Count > 0)
            {
                string Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                string Amount = txtBillAmount.Text.Trim().Replace(",", "");
                if (Amount != "" && Exch_rate != "")
                {
                    SqlParameter billamt = new SqlParameter("@billamt", Amount);
                    SqlParameter exch_Rate = new SqlParameter("@exch_Rate", Exch_rate);
                    string _queryC = "TF_IMP_GetThresholdLimitCheck";
                    result2 = objData.SaveDeleteData(_queryC, billamt, exch_Rate);

                    if (result2 == "Grater")
                    {
                        label1.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit. Please Verify LEI details.";
                        label1.ForeColor = System.Drawing.Color.Red;
                        label1.Font.Size = 10;
                        //label1.Font.Bold = true;
                        hdnLeiFlag.Value = "Y";
                        btnSubmit.Enabled = false;
                        btn_Verify.Visible = true;
                        hdncustleiflag.Value = "Y";
                    }
                    else
                    {
                        label1.Text = "Transaction Bill Amt is less than LEI Thresold Limit.";
                        label1.ForeColor = System.Drawing.Color.Green;
                        label1.Font.Size = 10;
                        hdnLeiFlag.Value = "N";
                        btnSubmit.Enabled = true;
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
            DataTable dt = objData.getData(_query, p1, p2,p3);
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
                SqlParameter P_Status = new SqlParameter("@status", "C");
                SqlParameter P_Module = new SqlParameter("@Module", "L");
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
    protected void txtBillAmount_TextChanged(object sender, EventArgs e)
    {
        if (lblForeign_Local.Text == "FOREIGN")
        {
            Check_LEINO_ExchRateDetails();
            if (hdncustnamelei.Value != "")
            {
                txtCustomer_ID.Text = hdncustnamelei.Value;
                fillCustomerMasterDetails();
            }
            if (hdnCustLCNo.Value != "")
            {
                txt_LC_No.Text = hdnCustLCNo.Value;
                fillLCDetails();
            }
            Check_LC();
        }
        if (lblForeign_Local.Text == "LOCAL")
        {
            //Check_LEINO_ExchRateDetails();
            if (hdncustnamelei.Value != "")
            {
                txtCustomer_ID.Text = hdncustnamelei.Value;
                fillCustomerMasterDetails();
            }
            if (hdnCustLCNo.Value != "")
            {
                txt_LC_No.Text = hdnCustLCNo.Value;
                fillLCDetails();
            }
            Check_LC();
        }
    }
    protected void Check_LC()
    {
        try
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
                ddlDrawer.SelectedValue = dt.Rows[0]["Drawer"].ToString();
                //txtDocNo.Text = dt.Rows[0]["Document_No"].ToString();
                //txtCustomer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
                //fillCustomerMasterDetails();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void Check_cust_Leiverify()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            string _query = "TF_IMP_CheckLEIflaggedCust_Count";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                hdncustleiflag.Value = "R";
                btnSubmit.Enabled = false;
                btn_Verify.Visible = true;
                ReccuringLEI.Visible = true;
                ReccuringLEI.Text = "This is Recurring LEI Customer.";
                ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                ReccuringLEI.Font.Size = 10;
            }
            else
            {
                ReccuringLEI.Visible = false;
            }
        }
    }   
    //----------------------------------END------------------------------------//

    private void fillCustomerMasterDetails()
    {
        try
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
                //fillLEIMasterDetails();
                txtCustomer_ID.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Select Customer of Selected Branch.')", true);
                txtCustomer_ID.Text = "";
                lblCustomerDesc.Text = "";
                txtCustomer_ID.Focus();
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void fillLCDetails()
    {
        try
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
                    ddl_Doc_Currency.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('LC Currency Code " + dt.Rows[0]["Currency"].ToString() + " does not exist in LMCC Master Please update.')", true);
                }
                else
                {
                    ddl_Doc_Currency.SelectedValue = _currency;
                    ddl_Comission_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
                    ddl_Interest_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
                    ddl_Other_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
                    ddl_Their_Commission_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
                    ddl_Doc_Currency.Enabled = false;
                }
                if (_commodity == "false")
                {
                    ddl_Commodity.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('LC Commodity Code " + dt.Rows[0]["CommCode"].ToString() + " does not exist in LMCC Master Please update.')", true);
                }
                else
                {
                    ddl_Commodity.SelectedValue = _commodity;
                    ddl_Commodity.Enabled = false;
                }
                if (_country == "false")
                {
                    ddlCountryCode.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('LC Country Code " + dt.Rows[0]["Country"].ToString() + " does not exist in LMCC Master Please update.')", true);
                }
                else
                {
                    ddlCountryCode.SelectedValue = _country;
                    ddlCountryCode.Enabled = false;
                }
            }
            else
            {
                if (lblCollection_Lodgment_UnderLC.Text == "Lodgment_Under_LC")
                {
                    lblLCDesc.Text = "";
                    txt_LC_No.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('Invalid LC No for this customer.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void fillNego_Remit_BankDetails()
    {
        try
        {
            if (hdnnego_remittype.Value != "")
            {
                txtNego_Remit_Bank.Text = hdnnego_remittype.Value;  //Added by bhupen
            }
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
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This swift code [" + hdnNegoRemiSwiftCode.Value + "] is not Enabled.')", true);
                    }
                }
                else
                {
                    hdnMT999LC.Value = "MT999LC";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This swift code [" + hdnNegoRemiSwiftCode.Value + "] does not exist in RMA Master.')", true);
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
        try
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void fillReimbursingbankDetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
            p1.Value = txtReimbursingbank.Text;

            DataTable dt = objData.getData("TF_IMP_Reimbursing_BankMasterDetails", p1);
            if (dt.Rows.Count > 0)
            {
                lbl_Reimbursingbank.Text = dt.Rows[0]["BankName"].ToString().Trim() + ":" + dt.Rows[0]["BranchName"].ToString().Trim();
                lbl_Reimbursingbank.ToolTip = lbl_Reimbursingbank.Text;
                if (lbl_Reimbursingbank.Text.Length > 50)
                {
                    lbl_Reimbursingbank.Text = lbl_Reimbursingbank.Text.Substring(0, 50) + "...";
                }
            }
            else
            {
                txtReimbursingbank.Text = "";
                lbl_Reimbursingbank.Text = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fillCurrency()
    {
        try
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fillCurrency_Description()
    {
        try
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fill_GBaseCommodity()
    {
        try
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }

    }
    protected void fill_GBaseCommodity_Description()
    {
        try
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
                    txtAMLCommodity.Text = dt.Rows[0]["GBase_Commodity_Description"].ToString().Trim();
                }
            }
            else
            {
                lblCommodityDesc.Text = "";
                txtAMLCommodity.Text = "";
                ddl_Commodity.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fill_Country()
    {
        try
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fill_CountryOfOrigin()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData("TF_IMP_Country_List");
            ddlCountryOfOrigin.Items.Clear();
            ListItem li = new ListItem();
            li.Value = "";
            if (dt.Rows.Count > 0)
            {
                li.Text = "Select";
                ddlCountryOfOrigin.DataSource = dt.DefaultView;
                ddlCountryOfOrigin.DataTextField = "Country_Desc";
                ddlCountryOfOrigin.DataValueField = "CountryID";
                ddlCountryOfOrigin.DataBind();
            }
            else
            {
                li.Text = "No record(s) found";
            }
            ddlCountryOfOrigin.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
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
    protected void fill_Country_Description()
    {
        try
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fill_CountryOfOrigin_Description()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CountryID", ddlCountryOfOrigin.SelectedValue.ToString());
            DataTable dt = objData.getData("TF_IMP_Country_Details", p1);
            if (dt.Rows.Count > 0)
            {
                lblCountryOfOriginDesc.Text = dt.Rows[0]["CountryName"].ToString().Trim();
                lblCountryOfOriginDesc.ToolTip = lblCountryOfOriginDesc.Text.ToString();

                DataTable dtCheck = objData.getData("TF_IMP_SanctionedCountry_details", p1);
                if (dtCheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Selected Country Of Origin is in Sanctioned Country list.')", true);
                    ddlCountryOfOrigin.Focus();
                }
            }
            else
            {
                lblCountryOfOriginDesc.Text = "";
                ddlCountryOfOrigin.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void fill_Drawer()
    {
        try
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
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("TF_IMP_BOE_Maker_View.aspx", true);
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txtCustomer_ID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            fillCustomerMasterDetails();
            txt_LC_No.Text = "";
            lblLCDesc.Text = "";
            txtCustomer_ID.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txt_LC_No_TextChanged(object sender, EventArgs e)
    {
        fillLCDetails();
    }
    protected void txtAcwithInstitution_TextChanged(object sender, EventArgs e)
    {
        fillAcwithInstitutionDetails();
    }
    protected void rdb_SP_Instr_None_CheckedChanged(object sender, EventArgs e)
    {
        Instrutionselection();
        txt_SP_Instructions1.Text = ""; txt_SP_Instructions1.Enabled = true;
        txt_SP_Instructions2.Text = ""; txt_SP_Instructions2.Enabled = true;
        txt_SP_Instructions3.Text = ""; txt_SP_Instructions3.Enabled = true;
        txt_SP_Instructions4.Text = ""; txt_SP_Instructions4.Enabled = true;
        txt_SP_Instructions5.Text = ""; txt_SP_Instructions5.Enabled = true;
    }
    protected void rdb_SP_Instr_Other_CheckedChanged(object sender, EventArgs e)
    {
        rdb_SP_Instr_None_CheckedChanged(null, null);
    }
    protected void rdb_SP_Instr_Annexure_CheckedChanged(object sender, EventArgs e)
    {
        rdb_SP_Instr_None_CheckedChanged(null, null);
        if (rdb_SP_Instr_Annexure.Checked)
        {
            txt_SP_Instructions1.Text = "As Per Annexure";
        }
        if (rdb_SP_Instr_On_Sight.Checked && rdb_SP_Instr_Annexure.Checked)
        {
            txt_SP_Instructions1.Text = "As Per Annexure";
            txt_SP_Instructions2.Text = "Beneficiary to be paid at sight as per LC terms.";
        }
        else if (rdb_SP_Instr_On_Date.Checked && rdb_SP_Instr_Annexure.Checked)
        {
            txt_SP_Instructions1.Text = "As Per Annexure";
            txt_SP_Instructions2.Text = "Beneficiary to be paid on dated";
        }
    }
    protected void rdb_SP_Instr_On_Sight_CheckedChanged(object sender, EventArgs e)
    {
        rdb_SP_Instr_Other.Checked = false;
        rdb_SP_Instr_None_CheckedChanged(null, null);
        if (rdb_SP_Instr_Annexure.Checked == false && rdb_SP_Instr_On_Sight.Checked)
        {
            txt_SP_Instructions1.Text = "Beneficiary to be paid at sight as per LC terms.";
        }
        else if (rdb_SP_Instr_On_Sight.Checked && rdb_SP_Instr_Annexure.Checked)
        {
            txt_SP_Instructions1.Text = "As Per Annexure";
            txt_SP_Instructions2.Text = "Beneficiary to be paid at sight as per LC terms.";
        }
    }
    protected void rdb_SP_Instr_On_Date_CheckedChanged(object sender, EventArgs e)
    {
        rdb_SP_Instr_Other.Checked = false;
        rdb_SP_Instr_None_CheckedChanged(null, null);

        if (rdb_SP_Instr_Annexure.Checked == false && rdb_SP_Instr_On_Date.Checked)
        {
            txt_SP_Instructions1.Text = "Beneficiary to be paid on dated";
        }
        else if (rdb_SP_Instr_On_Date.Checked && rdb_SP_Instr_Annexure.Checked)
        {
            txt_SP_Instructions1.Text = "As Per Annexure";
            txt_SP_Instructions2.Text = "Beneficiary to be paid on dated";
        }

    }
    protected void ddl_Doc_Currency_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillCurrency_Description();
            if (ddl_Doc_Currency.SelectedValue.ToString() != "0")
            {
                ddl_Comission_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
                ddl_Interest_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
                ddl_Other_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
                ddl_Their_Commission_Currency.Text = ddl_Doc_Currency.SelectedValue.ToString();
            }
            if (ddl_Doc_Currency.SelectedItem.ToString() == "INR")
            {
                ddlCountryOfOrigin.SelectedItem.Value = "IN";
                txtCountryOfOrigin.Text = "IN";
            }
            else
            {
                ddlCountryOfOrigin.SelectedItem.Value = "";
                txtCountryOfOrigin.Text = "";
            }
            Exp_AcceptanceDate();
            Duedate();
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "Total_Bill_Amount('a');", true);
            ddl_Doc_Currency.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void ddl_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_GBaseCommodity_Description();
        ddl_Commodity.Focus();
    }
    protected void ddlCountryCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_Country_Description();
        ddlCountryCode.Focus();
    }
    protected void ddlCountryOfOrigin_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_CountryOfOrigin_Description();
        ddlCountryOfOrigin.Focus();
    }   
    protected void txtNego_Remit_Bank_TextChanged(object sender, EventArgs e)
    {
        fillNego_Remit_BankDetails();
        txtNego_Remit_Bank.Focus();
    }
    protected void txtReimbursingbank_TextChanged(object sender, EventArgs e)
    {
        fillReimbursingbankDetails();
    }
    protected void btnupload_Disc_Click(object sender, EventArgs e)
    {
        //if (FileUpload_Discre.HasFile)
        //{
        //    string FolderPath = Server.MapPath("~/Uploaded_Files/Discrepancy");

        //    if (!Directory.Exists(FolderPath))
        //    {
        //        Directory.CreateDirectory(FolderPath);
        //    }
        //    string FilePath = FolderPath + "\\" + txtDocNo.Text .ToString();
        //    FileUpload_Discre.SaveAs(FilePath);
        //}
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
    protected void btnDocAccPrev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentInstructions;
    }
    protected void btn_Instr_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentDetails;
    }
    protected void btn_Instr_Next_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbAmlDetails;
    }
    protected void btnDocNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentInstructions;
    }
    protected void ddl_Nego_Remit_Bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNego_Remit_Bank.Text = "";
        fillNego_Remit_BankDetails();
    }
    //siddhesh
    public void Duedate()
    {
        try
        {
            string Sight_Usance = lblSight_Usance.Text.ToString(), Doc_Curr = ddl_Doc_Currency.SelectedItem.Value, Tenor_Days = "", Start_Date = "", Tenor_Days_From = "";
            switch (Sight_Usance)
            {
                case "Sight":
                    Start_Date = txtDateReceived.Text.ToString();
                    Tenor_Days = "5";
                    break;

                case "Usance":
                    if (ddlTenor.SelectedItem.Value == "4")
                    {
                        Tenor_Days_From = ddlTenor_Days_From.SelectedItem.Value.ToString();
                        Tenor_Days = txtTenor.Text.ToString();
                        //Tenor_Days = Int32.Parse(txtTenor.Text);
                        switch (Tenor_Days_From)
                        {
                            case "SHIPMENT DATE":
                                Start_Date = txtShippingDate.Text.ToString();
                                break;
                            case "INVOICE DATE":
                                Start_Date = txt_Inv_Date.Text.ToString();
                                break;
                            case "BOEXCHANGE DATE":
                                Start_Date = txtBOExchange.Text.ToString();
                                break;
                            case "OTHERS/BLANK":
                                Start_Date = "";
                                break;
                        }
                    }
                    break;
            }
            TF_DATA objData = new TF_DATA();
            SqlParameter p_Start_Date = new SqlParameter("@_DueDate", Start_Date);
            SqlParameter p_BillCurr = new SqlParameter("@BillCurr", Doc_Curr);
            SqlParameter p_Tenor_Days = new SqlParameter("@count", Tenor_Days);
            SqlParameter p_Branch_Code = new SqlParameter("@Branch_Code", Request.QueryString["BranchCode"].Trim());

            string _query;
            if (Sight_Usance == "Usance")
            { _query = "TF_IMP_CheckHolidayForDueDateTanor"; }
            else
            {
                _query = "TF_IMP_CheckHolidayForDueDate";
            }

            if (Start_Date != "" && Tenor_Days != "" && Doc_Curr != "")
            {
                DataTable dt = objData.getData(_query, p_Start_Date, p_BillCurr, p_Tenor_Days, p_Branch_Code);
                if (dt.Rows.Count > 0)
                {
                    txtDueDate.Text = dt.Rows[0]["Due_date"].ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    public void Exp_AcceptanceDate()
    {
        try
        {
            string Doc_Curr = ddl_Doc_Currency.SelectedItem.Value;
            TF_DATA objData = new TF_DATA();
            SqlParameter p_Start_Date = new SqlParameter("@_DueDate", txtDateReceived.Text.ToString());
            SqlParameter p_BillCurr = new SqlParameter("@BillCurr", Doc_Curr);
            SqlParameter p_Tenor_Days = new SqlParameter("@count", 5);
            SqlParameter p_Branch_Code = new SqlParameter("@Branch_Code", Request.QueryString["BranchCode"].Trim());

            string _query = "";
            switch (hdnDocType.Value)
            {
                case "ICA":
                    _query = "TF_IMP_CheckHolidayForPaymentDate";
                    break;
                case "ICU":
                    _query = "TF_IMP_CheckHolidayForExceptenceDate";
                    break;
                case "IBA":
                    _query = "TF_IMP_CheckHolidayForPaymentDate";
                    break;
                case "ACC":
                    _query = "TF_IMP_CheckHolidayForExceptenceDate";
                    break;
            }
            if (txtDateReceived.Text != "" && Doc_Curr != "")
            {
                DataTable dt = objData.getData(_query, p_Start_Date, p_BillCurr, p_Tenor_Days, p_Branch_Code);
                if (dt.Rows.Count > 0)
                {
                    txt_AcceptanceDate.Text = dt.Rows[0]["Due_date"].ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txtDateReceived_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Exp_AcceptanceDate();
            Duedate();
            txtDateReceived.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void ddlTenor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var _lblSight_Usance = lblSight_Usance.Text;
            var _ddlTenor = ddlTenor.SelectedValue;
            if (_lblSight_Usance == "Usance" && _ddlTenor == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('For Usance type Tenor cannot be Sight.')", true);
                ddlTenor.SelectedIndex = 0;
                ddlTenor.Focus();
            }
            Duedate();
            ddlTenor.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txtTenor_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Duedate();
            GetStampDutyCharges();
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "Total_Bill_Amount('a');", true);
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void ddlTenor_Days_From_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Duedate();
            ddlTenor_Days_From.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txtBOExchange_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (Request.QueryString["Sight_Usance"].ToString() != "Sight")
            //{
            Duedate();
            //}
            txtBOExchange.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txt_Inv_Date_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Duedate();
            txt_Inv_Date.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txtShippingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Duedate();
            txtShippingDate.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void ddlPaymentSwift56_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PaymentSwiftselection();
            ddlPaymentSwift56.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void ddlPaymentSwift57_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PaymentSwiftselection();
            ddlPaymentSwift57.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void ddlPaymentSwift58_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PaymentSwiftselection();
            ddlPaymentSwift58.Focus();
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    public void PaymentSwiftselection()
    {
        try
        {
            if (ddlPaymentSwift56.SelectedValue == "56A")
            {
                panel56ACC_No.Style.Add("display", "block");
                panel56A.Style.Add("display", "block");
                panel56DName.Style.Add("display", "none");
                panel56DAddress.Style.Add("display", "none");
                txt_PaymentSwift56D_name.Text = "";
                txt_PaymentSwift56D_Address.Text = "";
            }
            else if (ddlPaymentSwift56.SelectedValue == "56D")
            {
                panel56ACC_No.Style.Add("display", "block");
                panel56DName.Style.Add("display", "block");
                panel56DAddress.Style.Add("display", "block");
                panel56A.Style.Add("display", "none");
                txt_PaymentSwift56A.Text = "";
            }
            else
            {
                panel56ACC_No.Style.Add("display", "none");
                panel56A.Style.Add("display", "none");
                panel56DName.Style.Add("display", "none");
                panel56DAddress.Style.Add("display", "none");
            }
            if (ddlPaymentSwift57.SelectedValue == "57A")
            {
                panel57ACC_No.Style.Add("display", "block");
                panel57A.Style.Add("display", "block");
                panel57DName.Style.Add("display", "none");
                panel57DAddress.Style.Add("display", "none");
                txt_PaymentSwift57D_name.Text = "";
                txt_PaymentSwift57D_Address.Text = "";
            }
            else if (ddlPaymentSwift57.SelectedValue == "57D")
            {
                panel57ACC_No.Style.Add("display", "block");
                panel57DName.Style.Add("display", "block");
                panel57DAddress.Style.Add("display", "block");
                panel57A.Style.Add("display", "none");
                txt_PaymentSwift57A.Text = "";
            }
            else
            {
                panel57ACC_No.Style.Add("display", "none");
                panel57A.Style.Add("display", "none");
                panel57DName.Style.Add("display", "none");
                panel57DAddress.Style.Add("display", "none");
            }
            if (ddlPaymentSwift58.SelectedValue == "58A")
            {
                panel58ACC_No.Style.Add("display", "block");
                panel58A.Style.Add("display", "block");
                panel58DName.Style.Add("display", "none");
                panel58Address.Style.Add("display", "none");
                panel58Address2.Style.Add("display", "none");
                panel58Address3.Style.Add("display", "none");
                panel58Address4.Style.Add("display", "none");
                txt_PaymentSwift58D_name.Text = "";
                txt_PaymentSwift58D_Address.Text = "";
                txt_PaymentSwift58D_Address2.Text = "";
                txt_PaymentSwift58D_Address3.Text = "";
                txt_PaymentSwift58D_Address4.Text = "";
            }
            else if (ddlPaymentSwift58.SelectedValue == "58D")
            {
                panel58ACC_No.Style.Add("display", "block");
                panel58DName.Style.Add("display", "block");
                panel58Address.Style.Add("display", "block");
                panel58Address2.Style.Add("display", "block");
                panel58Address3.Style.Add("display", "block");
                panel58Address4.Style.Add("display", "block");
                panel58A.Style.Add("display", "none");
                txt_PaymentSwift58A.Text = "";
            }
            else
            {
                panel58ACC_No.Style.Add("display", "none");
                panel58A.Style.Add("display", "none");
                panel58DName.Style.Add("display", "none");
                panel58Address.Style.Add("display", "none");
                panel58Address2.Style.Add("display", "none");
                panel58Address3.Style.Add("display", "none");
                panel58Address4.Style.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    public void Instrutionselection()
    {
        try
        {
            if (rdb_SP_Instr_None.Checked)
            {
                rdb_SP_Instr_Other.Checked = false;
                rdb_SP_Instr_Annexure.Checked = false;
                rdb_SP_Instr_On_Sight.Checked = false;
                rdb_SP_Instr_On_Date.Checked = false;
            }
            if (rdb_SP_Instr_Other.Checked)
            {
                rdb_SP_Instr_None.Checked = false;
                rdb_SP_Instr_Annexure.Checked = false;
                rdb_SP_Instr_On_Sight.Checked = false;
                rdb_SP_Instr_On_Date.Checked = false;
            }
            if (rdb_SP_Instr_On_Sight.Checked)
            {
                rdb_SP_Instr_None.Checked = false;
                rdb_SP_Instr_Other.Checked = false;
            }
            if (rdb_SP_Instr_On_Date.Checked)
            {
                rdb_SP_Instr_None.Checked = false;
                rdb_SP_Instr_Other.Checked = false;
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    public void GetStampDutyCharges()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p_Tenor_Days = new SqlParameter("@Tenor_Days", txtTenor.Text);
            DataTable dt = objData.getData("TF_IMP_Get_Stamp_Duty_Charges", p_Tenor_Days);
            if (dt.Rows.Count > 0)
            {
                hdnStamp_Duty_Per_Thousand.Value = dt.Rows[0]["Rates"].ToString();
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    public string AMLTextFileCreation()
    {
        TF_DATA objData = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        dt = objData.getData("TF_IMP_AMLFileCreation", P_DocNo);
        string _DirectoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/AML");
        SqlParameter PUserName = new SqlParameter("@UserName", Session["userName"].ToString());
        string _FileName = objData.SaveDeleteData("TF_IMP_GetAMLFileName", PUserName);
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
        return _FileName + ".txt";
    }
    public void TotalAmountClaimedChange()
    {
        string TotalAmountClaimed = ddlTotalAmountClaimed734.SelectedValue;
        if (TotalAmountClaimed == "A")
        {
            txtDate734.Style.Add("display", "inline-block");
            lblDate734.Style.Add("display", "inline-block");
        }
        if (TotalAmountClaimed == "B")
        {
            txtDate734.Style.Add("display", "none");
            lblDate734.Style.Add("display", "none");
        }
    }
    public void AccountWithBankChange()
    {
        string AccountWithBank = ddlAccountWithBank734.SelectedValue;
        if (AccountWithBank == "A")
        {

            txtAccountWithBank734SwiftCode.Style.Add("display", "block");
            lblAccountWithBank734SwiftCode.Text = "Swift/IFSC Code :";
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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string FileName = AMLTextFileCreation();
        string filePath = "~/TF_GeneratedFiles/IMPORT/AML/" + FileName;
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
    protected void ddlDrawer_TextChanged(object sender, EventArgs e)
    {
        txtAMLDrawer.Text = ddlDrawer.SelectedItem.Text;
        ddlDrawer.Focus();
    }
    [WebMethod]
    public static Fields fillCustomerMasterDetails(string CustomerID, string BranchName)
    {
        Fields fields = new Fields();
        fields.CustStatus = "No";
        List<ListItem> drawer = new List<ListItem>();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        SqlParameter p2 = new SqlParameter("@BranchName", BranchName);
        p1.Value = CustomerID;
        SqlParameter p3 = new SqlParameter("@CUST_ACCOUNT_NO", CustomerID);
        string _query = "TF_IMP_GetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1, p2);
        DataTable dtDrawer = objData.getData("TF_IMP_Drawer_List", p3);
        if (dt.Rows.Count > 0)
        {
            fields.CustName = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            fields.CustAbbr = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            for (int i = 0; i < dtDrawer.Rows.Count; i++)
            {
                drawer.Add(new ListItem() { Text = dtDrawer.Rows[i]["Drawer_NAME"].ToString().Trim(), Value = dtDrawer.Rows[i]["Drawer_ID"].ToString().Trim() });
            }
            fields.DrawerList = drawer;
        }
        else
        {
            fields.CustStatus = "Select Customer of Selected Branch.";
        }
        return fields;
    }
    [WebMethod]
    public static Fields fillLCDetails(string DocNo, string CustAbbr, string LCNo, string CollectionORLC, string ForeignLocal)
    {
        Fields fields = new Fields();
        string _QLC = "TF_IMP_FillLCDetails";
        string _Qcurrency = "TF_IMP_CheckCurrencyInMaster";
        string _Qcommodity = "TF_IMP_CheckCommodityInMaster";
        string _Qcountry = "TF_IMP_CheckCountryInMaster";


        SqlParameter p1 = new SqlParameter("@LCNo", LCNo);
        SqlParameter p2 = new SqlParameter("@RefNo", DocNo);
        SqlParameter p3 = new SqlParameter("@CustAbbr", CustAbbr);
        SqlParameter p4 = new SqlParameter("@LocalForeign", ForeignLocal);
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_QLC, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            fields.LCStatus = "true";
            fields.LCAmount = dt.Rows[0]["BalAmount"].ToString();
            fields.LCRef1 = dt.Rows[0]["Ref1"].ToString();
            fields.LCRef2 = dt.Rows[0]["Ref2"].ToString();
            fields.LCRef3 = dt.Rows[0]["Ref3"].ToString();
            fields.LCCurrency = dt.Rows[0]["Currency"].ToString();
            fields.LCAppNo = dt.Rows[0]["ApplNo"].ToString();
            fields.LCCountry = dt.Rows[0]["Country"].ToString();
            fields.LCCommodityCode = dt.Rows[0]["CommCode"].ToString();
            fields.LCExpiryDate = dt.Rows[0]["ExpiryDate"].ToString();
            fields.LCRiskCustomer = dt.Rows[0]["RiskCustomer"].ToString();
            fields.LCSettlementAcNo = dt.Rows[0]["SettlementAcNo"].ToString();
            fields.LCCustomerABBR1 = dt.Rows[0]["CustAbbr1"].ToString();

            SqlParameter pcurrency = new SqlParameter("@CurrencyCode", dt.Rows[0]["Currency"].ToString());
            DataTable dtCurrency = objData.getData(_Qcurrency, pcurrency);
            fields.Currency = dtCurrency.Rows[0]["C_CODE"].ToString();
            fields.CurrencyName = dtCurrency.Rows[0]["C_DESCRIPTION"].ToString();

            SqlParameter pcommodity = new SqlParameter("@CommodityCode", dt.Rows[0]["CommCode"].ToString());
            DataTable dtCommodity = objData.getData(_Qcommodity, pcommodity);
            fields.Commodity = dtCommodity.Rows[0]["Gbase_Commodity_ID"].ToString();
            fields.CommodityName = dtCommodity.Rows[0]["GBase_Commodity_Description"].ToString();

            SqlParameter pcountry = new SqlParameter("@CountryCode", dt.Rows[0]["Country"].ToString());
            DataTable dtCountry = objData.getData(_Qcountry, pcountry);
            fields.Country = dtCountry.Rows[0]["CountryID"].ToString();
            fields.CountryName = dtCountry.Rows[0]["CountryName"].ToString();
            return fields;
        }
        else
        {
            if (CollectionORLC == "Lodgment_Under_LC")
            {
                fields.LCStatus = "false";
            }
            return fields;
        }
    }
    [WebMethod]
    public static Fields fillNegoRemitBankDetails(string NegoRemiID, string SwiftCode, string NegoRemiType)
    {
        Fields fields = new Fields();
        fields.RMAStatus = "true";
        fields.RMAExistStatus = "true";
        fields.SStatus = "true";
        string _query = "";
        string _RMAQuery = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter P_BankCode = new SqlParameter("@bankcode", SqlDbType.VarChar);
        P_BankCode.Value = NegoRemiID;
        SqlParameter P_SwiftCode = new SqlParameter("@SwiftCode", SqlDbType.VarChar);
        P_SwiftCode.Value = SwiftCode;
        if (NegoRemiType == "FOREIGN")
        {
            _query = "TF_IMP_OverseasBankMasterDetails";
            _RMAQuery = "TF_IMP_CheckSwiftCodeInRMAMAster";
        }
        else if (NegoRemiType == "LOCAL")
        {
            _query = "TF_IMP_LocalBankMasterDetails";
        }
        else
        {

        }
        DataTable dt = objData.getData(_query, P_BankCode);
        if (dt.Rows.Count > 0)
        {
            fields.SStatus = "true";
            fields.NegoRemiDesc = dt.Rows[0]["BankName"].ToString().Trim();
            fields.NegoRemiBankCode = dt.Rows[0]["BankCode"].ToString().Trim();

            fields.shortName = dt.Rows[0]["shortName"].ToString().Trim();
            fields.BankAddress = dt.Rows[0]["BankAddress"].ToString().Trim();
            fields.shortAddress = dt.Rows[0]["shortAddress"].ToString().Trim();
            fields.SwiftCode = dt.Rows[0]["SwiftCode"].ToString().Trim();
            fields.IFSC_Code = dt.Rows[0]["IFSC_Code"].ToString().Trim();

        }
        else
        {
            fields.SStatus = "false";
        }
        if (NegoRemiType == "FOREIGN" && NegoRemiID != "")
        {
            _RMAQuery = "TF_IMP_CheckSwiftCodeInRMAMaster";
            DataTable dtRMA = objData.getData(_RMAQuery, P_SwiftCode);
            if (dtRMA.Rows.Count > 0)
            {
                if (dtRMA.Rows[0]["SStatus"].ToString().Trim() == "Enabled")
                {
                    fields.RMAStatus = "true";
                }
                else
                {
                    fields.RMAStatus = "false";
                }
            }
            else
            {
                fields.RMAExistStatus = "false";
            }
        }
        return fields;
    }
    [WebMethod]
    public static Fields fillCurrencyDetails(string CurrID)
    {
        Fields fields = new Fields();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@currCode", CurrID);
        DataTable dt = objData.getData("TF_IMP_GetCurrDesc", p1);
        if (dt.Rows.Count > 0)
        {
            fields.CurrDesc = dt.Rows[0]["C_DESCRIPTION"].ToString();
        }
        else
        {
            fields.CurrDesc = "false";
        }
        return fields;
    }
    [WebMethod]
    public static Fields Duedate(string CurrID, string dateRecieved, string branchcode, string SightOrUsance, string Tenor, string TenorDaysFrom, string ShippingDate, string InvDate, string BoeExchangeDate, string TenorDays)
    {
        Fields fields = new Fields();
        string Sight_Usance = SightOrUsance, Doc_Curr = CurrID, Tenor_Days = "", Start_Date = "", Tenor_Days_From = "";
        switch (Sight_Usance)
        {
            case "Sight":
                Start_Date = dateRecieved;
                Tenor_Days = "5";
                break;

            case "Usance":
                if (Tenor == "4")
                {
                    Tenor_Days_From = TenorDaysFrom;
                    Tenor_Days = TenorDays;
                    //Tenor_Days = Int32.Parse(txtTenor.Text);
                    switch (Tenor_Days_From)
                    {
                        case "SHIPMENT DATE":
                            Start_Date = ShippingDate;
                            break;
                        case "INVOICE DATE":
                            Start_Date = InvDate;
                            break;
                        case "BOEXCHANGE DATE":
                            Start_Date = BoeExchangeDate;
                            break;
                        case "OTHERS/BLANK":
                            Start_Date = "";
                            break;
                    }
                }
                break;
        }
        TF_DATA objData = new TF_DATA();
        SqlParameter p_Start_Date = new SqlParameter("@_DueDate", Start_Date);
        SqlParameter p_BillCurr = new SqlParameter("@BillCurr", Doc_Curr);
        SqlParameter p_Tenor_Days = new SqlParameter("@count", Tenor_Days);
        SqlParameter p_Branch_Code = new SqlParameter("@Branch_Code", branchcode);

        string _query;
        if (Sight_Usance == "Usance")
        { _query = "TF_IMP_CheckHolidayForDueDateTanor"; }
        else
        {
            _query = "TF_IMP_CheckHolidayForDueDate";
        }

        if (Start_Date != "" && Tenor_Days != "" && Doc_Curr != "")
        {
            DataTable dt = objData.getData(_query, p_Start_Date, p_BillCurr, p_Tenor_Days, p_Branch_Code);
            if (dt.Rows.Count > 0)
            {
                fields.DueDate = dt.Rows[0]["Due_date"].ToString().Trim();
            }
        }
        else { fields.DueDate = ""; }
        return fields;
    }
    [WebMethod]
    public static Fields Exp_AcceptanceDate(string CurrID, string dateRecieved, string branchcode, string DocType)
    {
        Fields fields = new Fields();
        string Doc_Curr = CurrID;
        TF_DATA objData = new TF_DATA();
        SqlParameter p_Start_Date = new SqlParameter("@_DueDate", dateRecieved);
        SqlParameter p_BillCurr = new SqlParameter("@BillCurr", Doc_Curr);
        SqlParameter p_Tenor_Days = new SqlParameter("@count", 5);
        SqlParameter p_Branch_Code = new SqlParameter("@Branch_Code", branchcode);
        string _query = "";
        switch (DocType)
        {
            case "ICA":
                _query = "TF_IMP_CheckHolidayForPaymentDate";
                break;
            case "ICU":
                _query = "TF_IMP_CheckHolidayForExceptenceDate";
                break;
            case "IBA":
                _query = "TF_IMP_CheckHolidayForPaymentDate";
                break;
            case "ACC":
                _query = "TF_IMP_CheckHolidayForExceptenceDate";
                break;
        }

        if (dateRecieved != "" && Doc_Curr != "")
        {
            DataTable dt = objData.getData(_query, p_Start_Date, p_BillCurr, p_Tenor_Days, p_Branch_Code);
            if (dt.Rows.Count > 0)
            {
                fields.ExDate = dt.Rows[0]["Due_date"].ToString().Trim();
            }
        }
        else { fields.ExDate = ""; }
        return fields;
    }
    [WebMethod]
    public static Fields fillAcwithInstitutionDetails(string AccWithID)
    {
        Fields fields = new Fields();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = AccWithID;

        DataTable dt = objData.getData("TF_IMP_AccWithInstitutionDetails", p1);
        if (dt.Rows.Count > 0)
        {
            fields.AccWithInstiDesc = dt.Rows[0]["BankName"].ToString().Trim();
        }
        else
        {
            fields.AccWithInstiDesc = "";
        }
        return fields;
    }
    [WebMethod]
    public static Fields fillReimbursingbankDetails(string ReimbBankID)
    {
        Fields fields = new Fields();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = ReimbBankID;

        DataTable dt = objData.getData("TF_IMP_Reimbursing_BankMasterDetails", p1);
        if (dt.Rows.Count > 0)
        {
            fields.ReimbBankDesc = dt.Rows[0]["BankName"].ToString().Trim() + ":" + dt.Rows[0]["BranchName"].ToString().Trim();
        }
        else
        {
            fields.ReimbBankDesc = "";
        }
        return fields;
    }
    [WebMethod]
    public static Fields fill_GBaseCommodity_Description(string CommodityID)
    {
        Fields fields = new Fields();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CommodityID", CommodityID);
        DataTable dt = objData.getData("TF_IMP_GBaseCommodityDetails", p1);
        if (dt.Rows.Count > 0)
        {
            fields.CommodityDesc = dt.Rows[0]["GBase_Commodity_Description"].ToString().Trim();
        }
        else
        {
            fields.CommodityDesc = "";
        }
        return fields;
    }
    [WebMethod]
    public static Fields fill_Country_Description(string CountryID)
    {
        Fields fields = new Fields();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CountryID", CountryID);
        DataTable dt = objData.getData("TF_IMP_Country_Details", p1);
        if (dt.Rows.Count > 0)
        {
            fields.CountryDesc = dt.Rows[0]["CountryName"].ToString().Trim();
            DataTable dtCheck = objData.getData("TF_IMP_SanctionedCountry_details", p1);
            if (dtCheck.Rows.Count > 0)
            {
                fields.CountrySStatus = "false";
            }
        }
        else
        {
            fields.CountryDesc = "";
        }
        return fields;
    }
    [WebMethod]
    public static Fields SubmitToChecker(string UserName, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", DocNo);
        fields.CheckerStatus = obj.SaveDeleteData("TF_IMP_SubmitForChecker", P_DocNo);
        ////IMPClass.CreateUserLogA(UserName, "Sent document(" + DocNo + ") to checker.");
        return fields;
        
    }
    [WebMethod]
    public static Fields AMLTextFileCreation(string DocNo, string Path, string UserName)
    {
        Fields fields = new Fields();
        TF_DATA objData = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", DocNo);
        dt = objData.getData("TF_IMP_AMLFileCreation", P_DocNo);
        string _DirectoryPath = Path.Replace("-", "\\");
        SqlParameter PUserName = new SqlParameter("@UserName", UserName);
        string _FileName = objData.SaveDeleteData("TF_IMP_GetAMLFileName", PUserName);
        if (!Directory.Exists(_DirectoryPath))
        {
            Directory.CreateDirectory(_DirectoryPath);
        }
        StreamWriter sw;
        string FilePath = _DirectoryPath + "/" + _FileName + ".txt";
        fields.AMLFileName = _FileName + ".txt";
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
        return fields;
    }
    [WebMethod]
    public static Fields GetStampDutyCharges1(string TenorDays)
    {
        Fields fields = new Fields();
        fields.StampDutyPerThousand = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p_Tenor_Days = new SqlParameter("@Tenor_Days", TenorDays);
        DataTable dt = objData.getData("TF_IMP_Get_Stamp_Duty_Charges", p_Tenor_Days);
        if (dt.Rows.Count > 0)
        {
            fields.StampDutyPerThousand = dt.Rows[0]["Rates"].ToString();
            //hdnStamp_Duty_Per_Thousand.Value = dt.Rows[0]["Rates"].ToString();
        }
        return fields;
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
    [WebMethod]
    public static Fields CheckLCinBRO(string LCNo, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter PLCNo = new SqlParameter("@LCNo", LCNo);
        SqlParameter PDocNo = new SqlParameter("@DocNo", DocNo);
        string Result = obj.SaveDeleteData("TF_IMP_CheckLCNoInBRO", PLCNo, PDocNo);
        fields.LCInBROStatus = Result;
        return fields;
    }
    [WebMethod]
    public static Fields CheckLCinSG(string LCNo, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter PLCNo = new SqlParameter("@LCNo", LCNo);
        SqlParameter PDocNo = new SqlParameter("@DocNo", DocNo);
        string Result = obj.SaveDeleteData("TF_IMP_CheckLCNoInSG", PLCNo, PDocNo);
        fields.LCInSGStatus = Result;
        return fields;
    }

    /*
    [WebMethod]
    public static string CreateUserLog(string UserName, string DocNo)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = DocNo;
        string _query = "TF_IMP_BOE_AuditRecords";
        objData.SaveDeleteData(_query, p1);
        //--------------------------------------------------------
        SqlParameter p2 = new SqlParameter("@DocNo", SqlDbType.VarChar);
        p2.Value = DocNo;
        string _query1 = "TF_IMP_BOE_LOGDetails";
        objData.SaveDeleteData(_query1, p2);
        IMPClass.CreateUserLogA(UserName, "Closed document(" + DocNo + ") without sending to checker.");
        return "Success";
    }
     */
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
        public string shortName { get; set; }
        public string BankAddress { get; set; }
        public string shortAddress { get; set; }
        public string SwiftCode { get; set; }
        public string IFSC_Code { get; set; }
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
        // Check LC No In BRO
        public string LCInBROStatus { get; set; }
        // Check LC No In BRO
        public string LCInSGStatus { get; set; }
    }
}