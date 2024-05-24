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

public partial class IMP_Transactions_TF_IMP_Settlement_Collection_Checker : System.Web.UI.Page
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
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_Settlement_Checker_View.aspx", true);
                }
                else
                {
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    hdnDocNo.Value = Request.QueryString["DocNo"].Trim();
                    hdnflcIlcType.Value = Request.QueryString["FLC_ILC"].Trim();
                    hdnDoc_Scrutiny.Value = Request.QueryString["Doc_Scrutiny"].Trim();
                    hdnUserName.Value = Session["userName"].ToString();

                    Toggel_DocType(hdnDocType.Value, hdnBranchName.Value, hdnflcIlcType.Value, hdnDoc_Scrutiny.Value);
                    Fill_Logd_Details();
                    FillSettlementDetails();
                    Check_LEINO_ExchRateDetails();
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                        btn_Verify_Collection.Enabled = false;
                        btn_Verify_UnderLC.Enabled = false;
                        Check_Lei_Verified();
                        Check_Lei_RecurringStatus();
                    }
                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    protected void Toggel_DocType(string DocType, string Branch, string FLC_ILC, string Scrutiny)
    {
        if (Branch == "Mumbai")
        {
            Branch = "Mumbai";
            tbDocumentGOAccChange.Enabled = false;
        }
        else
        {
            Branch = "Branch";
            tbDocumentGOAccChange.Enabled = true;
        }
        switch (Scrutiny)
        {
            case "1":
                lblScrutiny.Text = "Clean";
                break;
            case "2":
                lblScrutiny.Text = "Discrepant";
                break;
        }
        if (DocType == "ACC")
        {
            lblCollection_Lodgment_UnderLC.Text = "Settlment_UnderLC";
            lblSight_Usance.Text = "Usance";
            tbDocumentDetailsUnderLC.Enabled = true;
            TabContainerMain.ActiveTab = tbDocumentDetailsUnderLC;
            txt_UnderLC_Settlement_For_Bank.Text = "09";
        }
        else
        {
            tbDocumentDetailsCollection.Enabled = true;
            TabContainerMain.ActiveTab = tbDocumentDetailsCollection;
            txt_Collection_Settlement_For_Bank.Text = "09";
            switch (DocType)
            {
                case "IBA":
                    lblCollection_Lodgment_UnderLC.Text = "Settlment_UnderLC";
                    lblSight_Usance.Text = "Sight";

                    break;
                case "ICA":
                    lblCollection_Lodgment_UnderLC.Text = "Settlment_Collection";
                    lblSight_Usance.Text = "Sight";
                    break;
                case "ICU":
                    lblCollection_Lodgment_UnderLC.Text = "Settlment_Collection";
                    lblSight_Usance.Text = "Usance";
                    break;
            }
        }


        switch (FLC_ILC)
        {
            case "ILC":  //local
                lblForeign_Local.Text = "Local";
                txt_Collection_Settlement_for_Cust.Text = "23";
                txt_UnderLC_Settlement_for_Cust.Text = "23";
                break;
            case "FLC":  //Foreign
                lblForeign_Local.Text = "Foreign";
                txt_Collection_Settlement_for_Cust.Text = "29";
                txt_UnderLC_Settlement_for_Cust.Text = "29";
                break;
        }
        switch (Branch)
        {
            case "Mumbai":// Mumbai
                break;
            case "Branch":
                break;
        }

        //txt_CR_Code.Text = "22002";
        //txt_CR_AC_Short_Name.Text = "SUNDRY DEPOSITS";
        //txt_CR_Cust_abbr.Text = "900";
        //txt_CR_Cust_Acc.Text = "F30007244";
        //txt_DR_Code.Text = "2000";
        //txt_DR_AC_Short_Name.Text = "CURRENT ACCOUNT";
    }
    protected void Fill_Logd_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PDocType = new SqlParameter("@Document_Type", hdnDocType.Value.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_BOE_Acceptance_Detalis_For_Settlement", PDocNo, PDocType);
        if (dt.Rows.Count > 0)
        {
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString(); //Added by bhupen for lei on 05122022
            hdnLedgerStatusCode.Value = dt.Rows[0]["Status_Code"].ToString();// use to checked status code of ledger modification
            hdnNegoRemiBankType.Value = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            //////////////// SETTLEMENT(IBD,ACC)//////////////////////////
            Settlement_For_Cust_AccNo.Value = dt.Rows[0]["Settlement_For_Cust_AccNo"].ToString();
            Settlement_For_Cust_Abbr.Value = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            Settlement_For_Cust_AccCode.Value = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();
            Settl_For_Bank_Abbr.Value = dt.Rows[0]["Settl_For_Bank_Abbr"].ToString();
            Settl_ForBank_AccNo.Value = dt.Rows[0]["Settl_ForBank_AccNo"].ToString();
            Settl_ForBank_AccCode.Value = dt.Rows[0]["Settl_ForBank_AccCode"].ToString();

            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();   //Added by bhupen on 01122022 for LEI
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt103Currency.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt103Amount.Text = dt.Rows[0]["Bill_Amount"].ToString();
            if (lblDoc_Curr.Text != "INR")
            {
                tbSwiftR42.Enabled = false;
                rdb_swift_R42.Visible = false;
            }
            txt_Collection_Customer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
            txt_UnderLC_Customer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
            lbl_Collection_Customer_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();
            lbl_UnderLC_Customer_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();

            txt_UnderLC_Maturity.Text = dt.Rows[0]["Maturity_Date"].ToString();
            txt_UnderLC_Interest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();
            txt_UnderLC_Interest_To.Text = dt.Rows[0]["Maturity_Date"].ToString();
            txt_UnderLC_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();

            //---------------------Added by bhupen for Lei_Audit---------------------------------//
            hdnDuedate_Collection.Value = dt.Rows[0]["Maturity_Date"].ToString();

            txtTransactionRefNoR42.Text = "" + dt.Rows[0]["Document_No"].ToString();
            txtRelatedReferenceR42.Text = dt.Rows[0]["Their_Ref_No"].ToString();
            txtValueDateR42.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtCureencyR42.Text = "INR";
            txtOrderingInstitutionIFSCR42.Text = dt.Rows[0]["IFSCCODE"].ToString();
            txtBeneficiaryInstitutionIFSCR42.Text = dt.Rows[0]["IFSC_Code"].ToString();
            txtCodeWordR42.Text = "TRF";
            txtAdditionalInformationR42.Text = "";
            txtMoreInfo1R42.Text = "//" + dt.Rows[0]["CUST_NAME"].ToString();
            txtMoreInfo2R42.Text = "//YOUR REF NO:" + dt.Rows[0]["Their_Ref_No"].ToString();

            // MT 202 & 103 changes 02122019
            ddlIntermediary202.SelectedValue = dt.Rows[0]["IntermediaryInstitutionHeader"].ToString();
            txtIntermediary202AccountNumber.Text = dt.Rows[0]["IntermediaryInstitutionAccno"].ToString();
            txtIntermediary202SwiftCode.Text = dt.Rows[0]["IntermediaryInstitutionSwiftCode"].ToString();
            txtIntermediary202Name.Text = dt.Rows[0]["IntermediaryInstitutionName"].ToString();
            txtIntermediary202Address1.Text = dt.Rows[0]["IntermediaryInstitutionAddress1"].ToString();
            txtIntermediary202Address2.Text = dt.Rows[0]["IntermediaryInstitutionAddress2"].ToString();
            txtIntermediary202Address3.Text = dt.Rows[0]["IntermediaryInstitutionAddress3"].ToString();
            ddlIntermediary202_TextChanged(null, null);

            ddlAccountWithInstitution202.SelectedValue = dt.Rows[0]["AccountWithInstitutionHeader"].ToString();
            txtAccountWithInstitution202AccountNumber.Text = dt.Rows[0]["AccountWithInstitutionAccno"].ToString();
            txtAccountWithInstitution202SwiftCode.Text = dt.Rows[0]["AccountWithInstitutionSwiftCode"].ToString();
            txtAccountWithInstitution202Name.Text = dt.Rows[0]["AccountWithInstitutionName"].ToString();
            txtAccountWithInstitution202Location.Text = "";
            txtAccountWithInstitution202Address1.Text = dt.Rows[0]["AccountWithInstitutionAddress1"].ToString();
            txtAccountWithInstitution202Address2.Text = dt.Rows[0]["AccountWithInstitutionAddress2"].ToString();
            txtAccountWithInstitution202Address3.Text = dt.Rows[0]["AccountWithInstitutionAddress3"].ToString();
            ddlAccountWithInstitution202_TextChanged(null, null);

            ddlBeneficiaryInstitution202.SelectedValue = dt.Rows[0]["BeneficiaryCustomerHeader"].ToString();
            txtBeneficiaryInstitution202AccountNumber.Text = dt.Rows[0]["BeneficiaryCustomerAccno"].ToString();
            txtBeneficiaryInstitution202SwiftCode.Text = dt.Rows[0]["BeneficiaryCustomerSwiftCode"].ToString();
            txtBeneficiaryInstitution202Name.Text = dt.Rows[0]["BeneficiaryCustomerName"].ToString();
            txtBeneficiaryInstitution202Address1.Text = dt.Rows[0]["BeneficiaryCustomerAddress1"].ToString();
            txtBeneficiaryInstitution202Address2.Text = dt.Rows[0]["BeneficiaryCustomerAddress2"].ToString();
            txtBeneficiaryInstitution202Address3.Text = dt.Rows[0]["BeneficiaryCustomerAddress3"].ToString();
            ddlBeneficiaryInstitution202_TextChanged(null, null);

            ddlIntermediary103.SelectedValue = dt.Rows[0]["IntermediaryInstitutionHeader"].ToString();
            txtIntermediary103AccountNumber.Text = dt.Rows[0]["IntermediaryInstitutionAccno"].ToString();
            txtIntermediary103SwiftCode.Text = dt.Rows[0]["IntermediaryInstitutionSwiftCode"].ToString();
            txtIntermediary103Name.Text = dt.Rows[0]["IntermediaryInstitutionName"].ToString();
            txtIntermediary103Address1.Text = dt.Rows[0]["IntermediaryInstitutionAddress1"].ToString();
            txtIntermediary103Address2.Text = dt.Rows[0]["IntermediaryInstitutionAddress2"].ToString();
            txtIntermediary103Address3.Text = dt.Rows[0]["IntermediaryInstitutionAddress3"].ToString();
            ddlIntermediary103_TextChanged(null, null);

            ddlAccountWithInstitution103.SelectedValue = dt.Rows[0]["AccountWithInstitutionHeader"].ToString();
            txtAccountWithInstitution103AccountNumber.Text = dt.Rows[0]["AccountWithInstitutionAccno"].ToString();
            txtAccountWithInstitution103SwiftCode.Text = dt.Rows[0]["AccountWithInstitutionSwiftCode"].ToString();
            txtAccountWithInstitution103Name.Text = dt.Rows[0]["AccountWithInstitutionName"].ToString();
            txtAccountWithInstitution103Location.Text = "";
            txtAccountWithInstitution103Address1.Text = dt.Rows[0]["AccountWithInstitutionAddress1"].ToString();
            txtAccountWithInstitution103Address2.Text = dt.Rows[0]["AccountWithInstitutionAddress2"].ToString();
            txtAccountWithInstitution103Address3.Text = dt.Rows[0]["AccountWithInstitutionAddress3"].ToString();
            ddlAccountWithInstitution103_TextChanged(null, null);

            txtBeneficiaryCustomerAccountNumber.Text = dt.Rows[0]["BeneficiaryCustomerAccno"].ToString();
            txtBeneficiaryCustomerSwiftCode.Text = dt.Rows[0]["BeneficiaryCustomerSwiftCode"].ToString();
            // MT 202 & 103 changes 02122019
            if (dt.Rows[0]["Doc_LC_No"].ToString() != "")
            {
                txtMoreInfo3R42.Text = "//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString();
                txtMoreInfo4R42.Text = "//" + dt.Rows[0]["Drawer_NAME"].ToString();
                txtMoreInfo5R42.Text = "//BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();
            }
            else
            {
                txtMoreInfo3R42.Text = "//" + dt.Rows[0]["Drawer_NAME"].ToString();
                txtMoreInfo4R42.Text = "//BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();
            }
        }
    }
    protected void FillSettlementDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_Settlement_Details", PDocNo);
        if (dt.Rows.Count > 0)
        {
            //////////////(Collection)//////////////////////////
            txtValueDate.Text = dt.Rows[0]["Settlement_Date"].ToString();
            //txt_Doc_Value_Date.Text = dt.Rows[0]["Settlement_Date"].ToString();
            txt_Collection_Settlement_for_Cust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
            txt_Collection_Settlement_For_Bank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();
            txt_Collection_Attn.Text = dt.Rows[0]["Attn"].ToString();

            //////////////(Under LC)//////////////////////////
            txt_UnderLC_Interest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();
            txt_UnderLC_Comment_Code.Text = dt.Rows[0]["Comment_Code"].ToString();
            txt_UnderLC_Settlement_for_Cust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
            txt_UnderLC_Settlement_For_Bank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();
            txt_UnderLC_Discount.Text = dt.Rows[0]["Discount_Flag"].ToString();
            txt_UnderLC_Interest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            txt_UnderLC_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();
            txt_UnderLC_Rate.Text = dt.Rows[0]["Interest_Rate"].ToString();
            txt_UnderLC_Amount.Text = dt.Rows[0]["Interest_Amount"].ToString();
            txt_UnderLC_Overdue_Interestrate.Text = dt.Rows[0]["Overdue_Interest_Rate"].ToString();
            txt_UnderLC_Oveduenoofdays.Text = dt.Rows[0]["Overdue_No_Of_Days"].ToString();
            txt_UnderLC_Overdueamount.Text = dt.Rows[0]["Overdue_Interest_Amount"].ToString();
            txt_UnderLC_Attn.Text = dt.Rows[0]["Attn"].ToString();

            // Import Accounting
            if (dt.Rows[0]["IMP_ACC1_Flag"].ToString() == "Y")
            {
                chk_IMPACC1Flag.Checked = true;
                PanelIMPACC1.Visible = true;
                txt_IMPACC1_FCRefNo.Text = dt.Rows[0]["IMP_ACC1_FCRefNo"].ToString();
                txt_IMPACC1_DiscAmt.Text = dt.Rows[0]["IMP_ACC1_Amount"].ToString();
                txt_IMPACC1_Princ_matu.Text = dt.Rows[0]["IMP_ACC1_Principal_MATU"].ToString();
                txt_IMPACC1_Princ_lump.Text = dt.Rows[0]["IMP_ACC1_Principal_LUMP"].ToString();
                txt_IMPACC1_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Principal_Contract_No"].ToString();
                txt_IMPACC1_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Principal_Ex_Curr"].ToString();
                txt_IMPACC1_Princ_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Principal_Exch_Rate"].ToString();
                txt_IMPACC1_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Principal_Intnl_Exch_Rate"].ToString();
                txt_IMPACC1_Interest_matu.Text = dt.Rows[0]["IMP_ACC1_Interest_MATU"].ToString();
                txt_IMPACC1_Interest_lump.Text = dt.Rows[0]["IMP_ACC1_Interest_LUMP"].ToString();
                txt_IMPACC1_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Interest_Contract_No"].ToString();
                txt_IMPACC1_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Interest_Ex_Curr"].ToString();
                txt_IMPACC1_Interest_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Interest_Exch_Rate"].ToString();
                txt_IMPACC1_Interest_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Interest_Intnl_Exch_Rate"].ToString();
                txt_IMPACC1_Commission_matu.Text = dt.Rows[0]["IMP_ACC1_Commission_MATU"].ToString();
                txt_IMPACC1_Commission_lump.Text = dt.Rows[0]["IMP_ACC1_Commission_LUMP"].ToString();
                txt_IMPACC1_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Commission_Contract_No"].ToString();
                txt_IMPACC1_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Commission_Ex_Curr"].ToString();
                txt_IMPACC1_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Commission_Exch_Rate"].ToString();
                txt_IMPACC1_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC1_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_MATU"].ToString();
                txt_IMPACC1_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_LUMP"].ToString();
                txt_IMPACC1_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_Contract_No"].ToString();
                txt_IMPACC1_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC1_Their_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_Exch_Rate"].ToString();
                txt_IMPACC1_Their_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC1_CR_Code.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC1_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC1_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC1_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC1_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC1_CR_Acceptance_amt.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Amt"].ToString();
                txt_IMPACC1_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC1_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Interest_Curr"].ToString();
                txt_IMPACC1_CR_Interest_amt.Text = dt.Rows[0]["IMP_ACC1_CR_Interest_Amount"].ToString();
                txt_IMPACC1_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC1_CR_Interest_Payer"].ToString();
                txt_IMPACC1_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC1_CR_Accept_Commission_amt.Text = dt.Rows[0]["IMP_ACC1_CR_Acceptance_Comm_Amount"].ToString();
                txt_IMPACC1_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC1_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC1_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["IMP_ACC1_CR_Pay_Handle_Comm_Amount"].ToString();
                txt_IMPACC1_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC1_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Others_Curr"].ToString();
                txt_IMPACC1_CR_Others_amt.Text = dt.Rows[0]["IMP_ACC1_CR_Others_Amount"].ToString();
                txt_IMPACC1_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Others_Payer"].ToString();
                txt_IMPACC1_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC1_CR_Their_Commission_amt.Text = dt.Rows[0]["IMP_ACC1_CR_Their_Comm_Amount"].ToString();
                txt_IMPACC1_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Their_Comm_Payer"].ToString();
                txt_IMPACC1_DR_Code.Text = dt.Rows[0]["IMP_ACC1_DR_Code"].ToString();
                txt_IMPACC1_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name"].ToString();
                txt_IMPACC1_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr"].ToString();
                txt_IMPACC1_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC1_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC1_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC1_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC1_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC1_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC1_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC1_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer6"].ToString();


                txt_IMPACC1_DR_Code2.Text = dt.Rows[0]["IMP_ACC1_DR_Code2"].ToString();
                txt_IMPACC1_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC1_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr2"].ToString();
                txt_IMPACC1_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC1_DR_Code3.Text = dt.Rows[0]["IMP_ACC1_DR_Code3"].ToString();
                txt_IMPACC1_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC1_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr3"].ToString();
                txt_IMPACC1_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC1_DR_Code4.Text = dt.Rows[0]["IMP_ACC1_DR_Code4"].ToString();
                txt_IMPACC1_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC1_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr4"].ToString();
                txt_IMPACC1_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC1_DR_Code5.Text = dt.Rows[0]["IMP_ACC1_DR_Code5"].ToString();
                txt_IMPACC1_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC1_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr5"].ToString();
                txt_IMPACC1_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC1_DR_Code6.Text = dt.Rows[0]["IMP_ACC1_DR_Code6"].ToString();
                txt_IMPACC1_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC1_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr6"].ToString();
                txt_IMPACC1_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No6"].ToString();
            }
            if (dt.Rows[0]["IMP_ACC2_Flag"].ToString() == "Y")
            {
                chk_IMPACC2Flag.Checked = true;
                PanelIMPACC2.Visible = true;
                txt_IMPACC2_FCRefNo.Text = dt.Rows[0]["IMP_ACC2_FCRefNo"].ToString();
                txt_IMPACC2_DiscAmt.Text = dt.Rows[0]["IMP_ACC2_Amount"].ToString();
                txt_IMPACC2_Princ_matu.Text = dt.Rows[0]["IMP_ACC2_Principal_MATU"].ToString();
                txt_IMPACC2_Princ_lump.Text = dt.Rows[0]["IMP_ACC2_Principal_LUMP"].ToString();
                txt_IMPACC2_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Principal_Contract_No"].ToString();
                txt_IMPACC2_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Principal_Ex_Curr"].ToString();
                txt_IMPACC2_Princ_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Principal_Exch_Rate"].ToString();
                txt_IMPACC2_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Principal_Intnl_Exch_Rate"].ToString();
                txt_IMPACC2_Interest_matu.Text = dt.Rows[0]["IMP_ACC2_Interest_MATU"].ToString();
                txt_IMPACC2_Interest_lump.Text = dt.Rows[0]["IMP_ACC2_Interest_LUMP"].ToString();
                txt_IMPACC2_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Interest_Contract_No"].ToString();
                txt_IMPACC2_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Interest_Ex_Curr"].ToString();
                txt_IMPACC2_Interest_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Interest_Exch_Rate"].ToString();
                txt_IMPACC2_Interest_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Interest_Intnl_Exch_Rate"].ToString();
                txt_IMPACC2_Commission_matu.Text = dt.Rows[0]["IMP_ACC2_Commission_MATU"].ToString();
                txt_IMPACC2_Commission_lump.Text = dt.Rows[0]["IMP_ACC2_Commission_LUMP"].ToString();
                txt_IMPACC2_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Commission_Contract_No"].ToString();
                txt_IMPACC2_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Commission_Ex_Curr"].ToString();
                txt_IMPACC2_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Commission_Exch_Rate"].ToString();
                txt_IMPACC2_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC2_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_MATU"].ToString();
                txt_IMPACC2_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_LUMP"].ToString();
                txt_IMPACC2_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_Contract_No"].ToString();
                txt_IMPACC2_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC2_Their_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_Exch_Rate"].ToString();
                txt_IMPACC2_Their_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC2_CR_Code.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC2_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC2_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC2_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC2_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC2_CR_Acceptance_amt.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Amt"].ToString();
                txt_IMPACC2_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC2_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Interest_Curr"].ToString();
                txt_IMPACC2_CR_Interest_amt.Text = dt.Rows[0]["IMP_ACC2_CR_Interest_Amount"].ToString();
                txt_IMPACC2_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC2_CR_Interest_Payer"].ToString();
                txt_IMPACC2_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC2_CR_Accept_Commission_amt.Text = dt.Rows[0]["IMP_ACC2_CR_Acceptance_Comm_Amount"].ToString();
                txt_IMPACC2_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC2_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC2_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["IMP_ACC2_CR_Pay_Handle_Comm_Amount"].ToString();
                txt_IMPACC2_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC2_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Others_Curr"].ToString();
                txt_IMPACC2_CR_Others_amt.Text = dt.Rows[0]["IMP_ACC2_CR_Others_Amount"].ToString();
                txt_IMPACC2_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Others_Payer"].ToString();
                txt_IMPACC2_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC2_CR_Their_Commission_amt.Text = dt.Rows[0]["IMP_ACC2_CR_Their_Comm_Amount"].ToString();
                txt_IMPACC2_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Their_Comm_Payer"].ToString();
                txt_IMPACC2_DR_Code.Text = dt.Rows[0]["IMP_ACC2_DR_Code"].ToString();
                txt_IMPACC2_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name"].ToString();
                txt_IMPACC2_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr"].ToString();
                txt_IMPACC2_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC2_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC2_DR_Code2.Text = dt.Rows[0]["IMP_ACC2_DR_Code2"].ToString();
                txt_IMPACC2_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC2_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr2"].ToString();
                txt_IMPACC2_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC2_DR_Code3.Text = dt.Rows[0]["IMP_ACC2_DR_Code3"].ToString();
                txt_IMPACC2_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC2_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr3"].ToString();
                txt_IMPACC2_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC2_DR_Code4.Text = dt.Rows[0]["IMP_ACC2_DR_Code4"].ToString();
                txt_IMPACC2_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC2_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr4"].ToString();
                txt_IMPACC2_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC2_DR_Code5.Text = dt.Rows[0]["IMP_ACC2_DR_Code5"].ToString();
                txt_IMPACC2_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC2_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr5"].ToString();
                txt_IMPACC2_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC2_DR_Code6.Text = dt.Rows[0]["IMP_ACC2_DR_Code6"].ToString();
                txt_IMPACC2_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC2_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr6"].ToString();
                txt_IMPACC2_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No6"].ToString();
            }
            if (dt.Rows[0]["IMP_ACC3_Flag"].ToString() == "Y")
            {
                chk_IMPACC3Flag.Checked = true;
                PanelIMPACC3.Visible = true;
                txt_IMPACC3_FCRefNo.Text = dt.Rows[0]["IMP_ACC3_FCRefNo"].ToString();
                txt_IMPACC3_DiscAmt.Text = dt.Rows[0]["IMP_ACC3_Amount"].ToString();
                txt_IMPACC3_Princ_matu.Text = dt.Rows[0]["IMP_ACC3_Principal_MATU"].ToString();
                txt_IMPACC3_Princ_lump.Text = dt.Rows[0]["IMP_ACC3_Principal_LUMP"].ToString();
                txt_IMPACC3_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Principal_Contract_No"].ToString();
                txt_IMPACC3_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Principal_Ex_Curr"].ToString();
                txt_IMPACC3_Princ_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Principal_Exch_Rate"].ToString();
                txt_IMPACC3_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Principal_Intnl_Exch_Rate"].ToString();
                txt_IMPACC3_Interest_matu.Text = dt.Rows[0]["IMP_ACC3_Interest_MATU"].ToString();
                txt_IMPACC3_Interest_lump.Text = dt.Rows[0]["IMP_ACC3_Interest_LUMP"].ToString();
                txt_IMPACC3_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Interest_Contract_No"].ToString();
                txt_IMPACC3_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Interest_Ex_Curr"].ToString();
                txt_IMPACC3_Interest_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Interest_Exch_Rate"].ToString();
                txt_IMPACC3_Interest_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Interest_Intnl_Exch_Rate"].ToString();
                txt_IMPACC3_Commission_matu.Text = dt.Rows[0]["IMP_ACC3_Commission_MATU"].ToString();
                txt_IMPACC3_Commission_lump.Text = dt.Rows[0]["IMP_ACC3_Commission_LUMP"].ToString();
                txt_IMPACC3_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Commission_Contract_No"].ToString();
                txt_IMPACC3_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Commission_Ex_Curr"].ToString();
                txt_IMPACC3_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Commission_Exch_Rate"].ToString();
                txt_IMPACC3_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC3_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_MATU"].ToString();
                txt_IMPACC3_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_LUMP"].ToString();
                txt_IMPACC3_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_Contract_No"].ToString();
                txt_IMPACC3_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC3_Their_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_Exch_Rate"].ToString();
                txt_IMPACC3_Their_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC3_CR_Code.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC3_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC3_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC3_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC3_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC3_CR_Acceptance_amt.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Amt"].ToString();
                txt_IMPACC3_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC3_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Interest_Curr"].ToString();
                txt_IMPACC3_CR_Interest_amt.Text = dt.Rows[0]["IMP_ACC3_CR_Interest_Amount"].ToString();
                txt_IMPACC3_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC3_CR_Interest_Payer"].ToString();
                txt_IMPACC3_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC3_CR_Accept_Commission_amt.Text = dt.Rows[0]["IMP_ACC3_CR_Acceptance_Comm_Amount"].ToString();
                txt_IMPACC3_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC3_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC3_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["IMP_ACC3_CR_Pay_Handle_Comm_Amount"].ToString();
                txt_IMPACC3_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC3_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Others_Curr"].ToString();
                txt_IMPACC3_CR_Others_amt.Text = dt.Rows[0]["IMP_ACC3_CR_Others_Amount"].ToString();
                txt_IMPACC3_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Others_Payer"].ToString();
                txt_IMPACC3_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC3_CR_Their_Commission_amt.Text = dt.Rows[0]["IMP_ACC3_CR_Their_Comm_Amount"].ToString();
                txt_IMPACC3_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Their_Comm_Payer"].ToString();
                txt_IMPACC3_DR_Code.Text = dt.Rows[0]["IMP_ACC3_DR_Code"].ToString();
                txt_IMPACC3_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name"].ToString();
                txt_IMPACC3_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr"].ToString();
                txt_IMPACC3_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC3_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC3_DR_Code2.Text = dt.Rows[0]["IMP_ACC3_DR_Code2"].ToString();
                txt_IMPACC3_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC3_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr2"].ToString();
                txt_IMPACC3_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC3_DR_Code3.Text = dt.Rows[0]["IMP_ACC3_DR_Code3"].ToString();
                txt_IMPACC3_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC3_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr3"].ToString();
                txt_IMPACC3_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC3_DR_Code4.Text = dt.Rows[0]["IMP_ACC3_DR_Code4"].ToString();
                txt_IMPACC3_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC3_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr4"].ToString();
                txt_IMPACC3_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC3_DR_Code5.Text = dt.Rows[0]["IMP_ACC3_DR_Code5"].ToString();
                txt_IMPACC3_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC3_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr5"].ToString();
                txt_IMPACC3_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC3_DR_Code6.Text = dt.Rows[0]["IMP_ACC3_DR_Code6"].ToString();
                txt_IMPACC3_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC3_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr6"].ToString();
                txt_IMPACC3_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No6"].ToString();

            }

            if (dt.Rows[0]["IMP_ACC4_Flag"].ToString() == "Y")
            {
                chk_IMPACC4Flag.Checked = true;
                PanelIMPACC4.Visible = true;
                txt_IMPACC4_FCRefNo.Text = dt.Rows[0]["IMP_ACC4_FCRefNo"].ToString();
                txt_IMPACC4_DiscAmt.Text = dt.Rows[0]["IMP_ACC4_Amount"].ToString();
                txt_IMPACC4_Princ_matu.Text = dt.Rows[0]["IMP_ACC4_Principal_MATU"].ToString();
                txt_IMPACC4_Princ_lump.Text = dt.Rows[0]["IMP_ACC4_Principal_LUMP"].ToString();
                txt_IMPACC4_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Principal_Contract_No"].ToString();
                txt_IMPACC4_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Principal_Ex_Curr"].ToString();
                txt_IMPACC4_Princ_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Principal_Exch_Rate"].ToString();
                txt_IMPACC4_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Principal_Intnl_Exch_Rate"].ToString();
                txt_IMPACC4_Interest_matu.Text = dt.Rows[0]["IMP_ACC4_Interest_MATU"].ToString();
                txt_IMPACC4_Interest_lump.Text = dt.Rows[0]["IMP_ACC4_Interest_LUMP"].ToString();
                txt_IMPACC4_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Interest_Contract_No"].ToString();
                txt_IMPACC4_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Interest_Ex_Curr"].ToString();
                txt_IMPACC4_Interest_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Interest_Exch_Rate"].ToString();
                txt_IMPACC4_Interest_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Interest_Intnl_Exch_Rate"].ToString();
                txt_IMPACC4_Commission_matu.Text = dt.Rows[0]["IMP_ACC4_Commission_MATU"].ToString();
                txt_IMPACC4_Commission_lump.Text = dt.Rows[0]["IMP_ACC4_Commission_LUMP"].ToString();
                txt_IMPACC4_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Commission_Contract_No"].ToString();
                txt_IMPACC4_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Commission_Ex_Curr"].ToString();
                txt_IMPACC4_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Commission_Exch_Rate"].ToString();
                txt_IMPACC4_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC4_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_MATU"].ToString();
                txt_IMPACC4_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_LUMP"].ToString();
                txt_IMPACC4_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_Contract_No"].ToString();
                txt_IMPACC4_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC4_Their_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_Exch_Rate"].ToString();
                txt_IMPACC4_Their_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC4_CR_Code.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC4_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC4_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC4_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC4_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC4_CR_Acceptance_amt.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Amt"].ToString();
                txt_IMPACC4_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC4_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Interest_Curr"].ToString();
                txt_IMPACC4_CR_Interest_amt.Text = dt.Rows[0]["IMP_ACC4_CR_Interest_Amount"].ToString();
                txt_IMPACC4_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC4_CR_Interest_Payer"].ToString();
                txt_IMPACC4_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC4_CR_Accept_Commission_amt.Text = dt.Rows[0]["IMP_ACC4_CR_Acceptance_Comm_Amount"].ToString();
                txt_IMPACC4_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC4_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC4_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["IMP_ACC4_CR_Pay_Handle_Comm_Amount"].ToString();
                txt_IMPACC4_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC4_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Others_Curr"].ToString();
                txt_IMPACC4_CR_Others_amt.Text = dt.Rows[0]["IMP_ACC4_CR_Others_Amount"].ToString();
                txt_IMPACC4_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Others_Payer"].ToString();
                txt_IMPACC4_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC4_CR_Their_Commission_amt.Text = dt.Rows[0]["IMP_ACC4_CR_Their_Comm_Amount"].ToString();
                txt_IMPACC4_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Their_Comm_Payer"].ToString();
                txt_IMPACC4_DR_Code.Text = dt.Rows[0]["IMP_ACC4_DR_Code"].ToString();
                txt_IMPACC4_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name"].ToString();
                txt_IMPACC4_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr"].ToString();
                txt_IMPACC4_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC4_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC4_DR_Code2.Text = dt.Rows[0]["IMP_ACC4_DR_Code2"].ToString();
                txt_IMPACC4_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC4_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr2"].ToString();
                txt_IMPACC4_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC4_DR_Code3.Text = dt.Rows[0]["IMP_ACC4_DR_Code3"].ToString();
                txt_IMPACC4_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC4_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr3"].ToString();
                txt_IMPACC4_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC4_DR_Code4.Text = dt.Rows[0]["IMP_ACC4_DR_Code4"].ToString();
                txt_IMPACC4_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC4_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr4"].ToString();
                txt_IMPACC4_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC4_DR_Code5.Text = dt.Rows[0]["IMP_ACC4_DR_Code5"].ToString();
                txt_IMPACC4_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC4_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr5"].ToString();
                txt_IMPACC4_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC4_DR_Code6.Text = dt.Rows[0]["IMP_ACC4_DR_Code6"].ToString();
                txt_IMPACC4_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC4_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr6"].ToString();
                txt_IMPACC4_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No6"].ToString();

            }
            if (dt.Rows[0]["IMP_ACC5_Flag"].ToString() == "Y")
            {
                chk_IMPACC5Flag.Checked = true;
                PanelIMPACC5.Visible = true;
                txt_IMPACC5_FCRefNo.Text = dt.Rows[0]["IMP_ACC5_FCRefNo"].ToString();
                txt_IMPACC5_DiscAmt.Text = dt.Rows[0]["IMP_ACC5_Amount"].ToString();
                txt_IMPACC5_Princ_matu.Text = dt.Rows[0]["IMP_ACC5_Principal_MATU"].ToString();
                txt_IMPACC5_Princ_lump.Text = dt.Rows[0]["IMP_ACC5_Principal_LUMP"].ToString();
                txt_IMPACC5_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Principal_Contract_No"].ToString();
                txt_IMPACC5_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Principal_Ex_Curr"].ToString();
                txt_IMPACC5_Princ_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Principal_Exch_Rate"].ToString();
                txt_IMPACC5_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Principal_Intnl_Exch_Rate"].ToString();
                txt_IMPACC5_Interest_matu.Text = dt.Rows[0]["IMP_ACC5_Interest_MATU"].ToString();
                txt_IMPACC5_Interest_lump.Text = dt.Rows[0]["IMP_ACC5_Interest_LUMP"].ToString();
                txt_IMPACC5_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Interest_Contract_No"].ToString();
                txt_IMPACC5_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Interest_Ex_Curr"].ToString();
                txt_IMPACC5_Interest_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Interest_Exch_Rate"].ToString();
                txt_IMPACC5_Interest_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Interest_Intnl_Exch_Rate"].ToString();
                txt_IMPACC5_Commission_matu.Text = dt.Rows[0]["IMP_ACC5_Commission_MATU"].ToString();
                txt_IMPACC5_Commission_lump.Text = dt.Rows[0]["IMP_ACC5_Commission_LUMP"].ToString();
                txt_IMPACC5_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Commission_Contract_No"].ToString();
                txt_IMPACC5_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Commission_Ex_Curr"].ToString();
                txt_IMPACC5_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Commission_Exch_Rate"].ToString();
                txt_IMPACC5_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC5_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_MATU"].ToString();
                txt_IMPACC5_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_LUMP"].ToString();
                txt_IMPACC5_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_Contract_No"].ToString();
                txt_IMPACC5_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC5_Their_Commission_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_Exch_Rate"].ToString();
                txt_IMPACC5_Their_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_Intnl_Exch_Rate"].ToString();
                txt_IMPACC5_CR_Code.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC5_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC5_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC5_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC5_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC5_CR_Acceptance_amt.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Amt"].ToString();
                txt_IMPACC5_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC5_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Interest_Curr"].ToString();
                txt_IMPACC5_CR_Interest_amt.Text = dt.Rows[0]["IMP_ACC5_CR_Interest_Amount"].ToString();
                txt_IMPACC5_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC5_CR_Interest_Payer"].ToString();
                txt_IMPACC5_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC5_CR_Accept_Commission_amt.Text = dt.Rows[0]["IMP_ACC5_CR_Acceptance_Comm_Amount"].ToString();
                txt_IMPACC5_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC5_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC5_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["IMP_ACC5_CR_Pay_Handle_Comm_Amount"].ToString();
                txt_IMPACC5_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC5_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Others_Curr"].ToString();
                txt_IMPACC5_CR_Others_amt.Text = dt.Rows[0]["IMP_ACC5_CR_Others_Amount"].ToString();
                txt_IMPACC5_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Others_Payer"].ToString();
                txt_IMPACC5_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC5_CR_Their_Commission_amt.Text = dt.Rows[0]["IMP_ACC5_CR_Their_Comm_Amount"].ToString();
                txt_IMPACC5_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Their_Comm_Payer"].ToString();
                txt_IMPACC5_DR_Code.Text = dt.Rows[0]["IMP_ACC5_DR_Code"].ToString();
                txt_IMPACC5_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name"].ToString();
                txt_IMPACC5_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr"].ToString();
                txt_IMPACC5_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC5_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC5_DR_Code2.Text = dt.Rows[0]["IMP_ACC5_DR_Code2"].ToString();
                txt_IMPACC5_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC5_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr2"].ToString();
                txt_IMPACC5_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC5_DR_Code3.Text = dt.Rows[0]["IMP_ACC5_DR_Code3"].ToString();
                txt_IMPACC5_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC5_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr3"].ToString();
                txt_IMPACC5_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC5_DR_Code4.Text = dt.Rows[0]["IMP_ACC5_DR_Code4"].ToString();
                txt_IMPACC5_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC5_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr4"].ToString();
                txt_IMPACC5_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC5_DR_Code5.Text = dt.Rows[0]["IMP_ACC5_DR_Code5"].ToString();
                txt_IMPACC5_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC5_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr5"].ToString();
                txt_IMPACC5_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC5_DR_Code6.Text = dt.Rows[0]["IMP_ACC5_DR_Code6"].ToString();
                txt_IMPACC5_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC5_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr6"].ToString();
                txt_IMPACC5_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No6"].ToString();

            }
            // General Operation
            if (dt.Rows[0]["GO1_Flag"].ToString() == "Y")
            {
                chk_GO1Flag.Checked = true;
                Panel_GO1Left.Visible = true;
                txt_GO1_Left_Comment.Text = dt.Rows[0]["GO1_Comment1"].ToString();
                txt_GO1_Left_SectionNo.Text = dt.Rows[0]["GO1_Section1"].ToString();
                txt_GO1_Left_Remarks.Text = dt.Rows[0]["GO1_Remark1"].ToString();
                txt_GO1_Left_Memo.Text = dt.Rows[0]["GO1_Memo1"].ToString();
                txt_GO1_Left_Scheme_no.Text = dt.Rows[0]["GO1_SchemeNo1"].ToString();
                txt_GO1_Left_Debit_Code.Text = dt.Rows[0]["GO1_DebitCredit1_Code"].ToString();
                txt_GO1_Left_Debit_Curr.Text = dt.Rows[0]["GO1_DebitCredit1_CCY"].ToString();
                txt_GO1_Left_Debit_Amt.Text = dt.Rows[0]["GO1_DebitCredit1_Amt"].ToString();
                txt_GO1_Left_Debit_Cust.Text = dt.Rows[0]["GO1_DebitCredit1_Cust_abbr"].ToString();
                txt_GO1_Left_Debit_Cust_Name.Text = dt.Rows[0]["GO1_DebitCredit1_Cust_Name"].ToString();
                txt_GO1_Left_Debit_Cust_AcCode.Text = dt.Rows[0]["GO1_DebitCredit1_Cust_AccCode"].ToString();
                txt_GO1_Left_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO1_DebitCredit1_Cust_AccCode_Disc"].ToString();
                txt_GO1_Left_Debit_Cust_AccNo.Text = dt.Rows[0]["GO1_DebitCredit1_Cust_AccNo"].ToString();
                txt_GO1_Left_Debit_Exch_Rate.Text = dt.Rows[0]["GO1_DebitCredit1_ExchRate"].ToString();
                txt_GO1_Left_Debit_Exch_CCY.Text = dt.Rows[0]["GO1_DebitCredit1_ExchCCY"].ToString();
                txt_GO1_Left_Debit_FUND.Text = dt.Rows[0]["GO1_DebitCredit1_Fund"].ToString();
                txt_GO1_Left_Debit_Check_No.Text = dt.Rows[0]["GO1_DebitCredit1_CheckNo"].ToString();
                txt_GO1_Left_Debit_Available.Text = dt.Rows[0]["GO1_DebitCredit1_Available"].ToString();
                txt_GO1_Left_Debit_AdPrint.Text = dt.Rows[0]["GO1_DebitCredit1_Advice_Print"].ToString();
                txt_GO1_Left_Debit_Details.Text = dt.Rows[0]["GO1_DebitCredit1_Details"].ToString();
                txt_GO1_Left_Debit_Entity.Text = dt.Rows[0]["GO1_DebitCredit1_Entity"].ToString();
                txt_GO1_Left_Debit_Division.Text = dt.Rows[0]["GO1_DebitCredit1_Division"].ToString();
                txt_GO1_Left_Debit_Inter_Amount.Text = dt.Rows[0]["GO1_DebitCredit1_InterAmt"].ToString();
                txt_GO1_Left_Debit_Inter_Rate.Text = dt.Rows[0]["GO1_DebitCredit1_InterRate"].ToString();
                txt_GO1_Left_Credit_Code.Text = dt.Rows[0]["GO1_DebitCredit2_Code"].ToString();
                txt_GO1_Left_Credit_Curr.Text = dt.Rows[0]["GO1_DebitCredit2_CCY"].ToString();
                txt_GO1_Left_Credit_Amt.Text = dt.Rows[0]["GO1_DebitCredit2_Amt"].ToString();
                txt_GO1_Left_Credit_Cust.Text = dt.Rows[0]["GO1_DebitCredit2_Cust_abbr"].ToString();
                txt_GO1_Left_Credit_Cust_Name.Text = dt.Rows[0]["GO1_DebitCredit2_Cust_Name"].ToString();
                txt_GO1_Left_Credit_Cust_AcCode.Text = dt.Rows[0]["GO1_DebitCredit2_Cust_AccCode"].ToString();
                txt_GO1_Left_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO1_DebitCredit2_Cust_AccCode_Disc"].ToString();
                txt_GO1_Left_Credit_Cust_AccNo.Text = dt.Rows[0]["GO1_DebitCredit2_Cust_AccNo"].ToString();
                txt_GO1_Left_Credit_Exch_Rate.Text = dt.Rows[0]["GO1_DebitCredit2_ExchRate"].ToString();
                txt_GO1_Left_Credit_Exch_Curr.Text = dt.Rows[0]["GO1_DebitCredit2_ExchCCY"].ToString();
                txt_GO1_Left_Credit_FUND.Text = dt.Rows[0]["GO1_DebitCredit2_Fund"].ToString();
                txt_GO1_Left_Credit_Check_No.Text = dt.Rows[0]["GO1_DebitCredit2_CheckNo"].ToString();
                txt_GO1_Left_Credit_Available.Text = dt.Rows[0]["GO1_DebitCredit2_Available"].ToString();
                txt_GO1_Left_Credit_AdPrint.Text = dt.Rows[0]["GO1_DebitCredit2_Advice_Print"].ToString();
                txt_GO1_Left_Credit_Details.Text = dt.Rows[0]["GO1_DebitCredit2_Details"].ToString();
                txt_GO1_Left_Credit_Entity.Text = dt.Rows[0]["GO1_DebitCredit2_Entity"].ToString();
                txt_GO1_Left_Credit_Division.Text = dt.Rows[0]["GO1_DebitCredit2_Division"].ToString();
                txt_GO1_Left_Credit_Inter_Amount.Text = dt.Rows[0]["GO1_DebitCredit2_InterAmt"].ToString();
                txt_GO1_Left_Credit_Inter_Rate.Text = dt.Rows[0]["GO1_DebitCredit2_InterRate"].ToString();

                Panel_GO1Right.Visible = true;
                txt_GO1_Right_Comment.Text = dt.Rows[0]["GO1_Comment2"].ToString();
                txt_GO1_Right_SectionNo.Text = dt.Rows[0]["GO1_Section2"].ToString();
                txt_GO1_Right_Remarks.Text = dt.Rows[0]["GO1_Remark2"].ToString();
                txt_GO1_Right_Memo.Text = dt.Rows[0]["GO1_Memo2"].ToString();
                txt_GO1_Right_Scheme_no.Text = dt.Rows[0]["GO1_SchemeNo2"].ToString();
                txt_GO1_Right_Debit_Code.Text = dt.Rows[0]["GO1_DebitCredit3_Code"].ToString();
                txt_GO1_Right_Debit_Curr.Text = dt.Rows[0]["GO1_DebitCredit3_CCY"].ToString();
                txt_GO1_Right_Debit_Amt.Text = dt.Rows[0]["GO1_DebitCredit3_Amt"].ToString();
                txt_GO1_Right_Debit_Cust.Text = dt.Rows[0]["GO1_DebitCredit3_Cust_abbr"].ToString();
                txt_GO1_Right_Debit_Cust_Name.Text = dt.Rows[0]["GO1_DebitCredit3_Cust_Name"].ToString();
                txt_GO1_Right_Debit_Cust_AcCode.Text = dt.Rows[0]["GO1_DebitCredit3_Cust_AccCode"].ToString();
                txt_GO1_Right_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO1_DebitCredit3_Cust_AccCode_Disc"].ToString();
                txt_GO1_Right_Debit_Cust_AccNo.Text = dt.Rows[0]["GO1_DebitCredit3_Cust_AccNo"].ToString();
                txt_GO1_Right_Debit_Exch_Rate.Text = dt.Rows[0]["GO1_DebitCredit3_ExchRate"].ToString();
                txt_GO1_Right_Debit_Exch_CCY.Text = dt.Rows[0]["GO1_DebitCredit3_ExchCCY"].ToString();
                txt_GO1_Right_Debit_FUND.Text = dt.Rows[0]["GO1_DebitCredit3_Fund"].ToString();
                txt_GO1_Right_Debit_Check_No.Text = dt.Rows[0]["GO1_DebitCredit3_CheckNo"].ToString();
                txt_GO1_Right_Debit_Available.Text = dt.Rows[0]["GO1_DebitCredit3_Available"].ToString();
                txt_GO1_Right_Debit_AdPrint.Text = dt.Rows[0]["GO1_DebitCredit3_Advice_Print"].ToString();
                txt_GO1_Right_Debit_Details.Text = dt.Rows[0]["GO1_DebitCredit3_Details"].ToString();
                txt_GO1_Right_Debit_Entity.Text = dt.Rows[0]["GO1_DebitCredit3_Entity"].ToString();
                txt_GO1_Right_Debit_Division.Text = dt.Rows[0]["GO1_DebitCredit3_Division"].ToString();
                txt_GO1_Right_Debit_Inter_Amount.Text = dt.Rows[0]["GO1_DebitCredit3_InterAmt"].ToString();
                txt_GO1_Right_Debit_Inter_Rate.Text = dt.Rows[0]["GO1_DebitCredit3_InterRate"].ToString();
                txt_GO1_Right_Credit_Code.Text = dt.Rows[0]["GO1_DebitCredit4_Code"].ToString();
                txt_GO1_Right_Credit_Curr.Text = dt.Rows[0]["GO1_DebitCredit4_CCY"].ToString();
                txt_GO1_Right_Credit_Amt.Text = dt.Rows[0]["GO1_DebitCredit4_Amt"].ToString();
                txt_GO1_Right_Credit_Cust.Text = dt.Rows[0]["GO1_DebitCredit4_Cust_abbr"].ToString();
                txt_GO1_Right_Credit_Cust_Name.Text = dt.Rows[0]["GO1_DebitCredit4_Cust_Name"].ToString();
                txt_GO1_Right_Credit_Cust_AcCode.Text = dt.Rows[0]["GO1_DebitCredit4_Cust_AccCode"].ToString();
                txt_GO1_Right_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO1_DebitCredit4_Cust_AccCode_Disc"].ToString();
                txt_GO1_Right_Credit_Cust_AccNo.Text = dt.Rows[0]["GO1_DebitCredit4_Cust_AccNo"].ToString();
                txt_GO1_Right_Credit_Exch_Rate.Text = dt.Rows[0]["GO1_DebitCredit4_ExchRate"].ToString();
                txt_GO1_Right_Credit_Exch_Curr.Text = dt.Rows[0]["GO1_DebitCredit4_ExchCCY"].ToString();
                txt_GO1_Right_Credit_FUND.Text = dt.Rows[0]["GO1_DebitCredit4_Fund"].ToString();
                txt_GO1_Right_Credit_Check_No.Text = dt.Rows[0]["GO1_DebitCredit4_CheckNo"].ToString();
                txt_GO1_Right_Credit_Available.Text = dt.Rows[0]["GO1_DebitCredit4_Available"].ToString();
                txt_GO1_Right_Credit_AdPrint.Text = dt.Rows[0]["GO1_DebitCredit4_Advice_Print"].ToString();
                txt_GO1_Right_Credit_Details.Text = dt.Rows[0]["GO1_DebitCredit4_Details"].ToString();
                txt_GO1_Right_Credit_Entity.Text = dt.Rows[0]["GO1_DebitCredit4_Entity"].ToString();
                txt_GO1_Right_Credit_Division.Text = dt.Rows[0]["GO1_DebitCredit4_Division"].ToString();
                txt_GO1_Right_Credit_Inter_Amount.Text = dt.Rows[0]["GO1_DebitCredit4_InterAmt"].ToString();
                txt_GO1_Right_Credit_Inter_Rate.Text = dt.Rows[0]["GO1_DebitCredit4_InterRate"].ToString();
            }
            if (dt.Rows[0]["GO2_Flag"].ToString() == "Y")
            {
                chk_GO2Flag.Checked = true;
                Panel_GO2Left.Visible = true;
                txt_GO2_Left_Comment.Text = dt.Rows[0]["GO2_Comment1"].ToString();
                txt_GO2_Left_SectionNo.Text = dt.Rows[0]["GO2_Section1"].ToString();
                txt_GO2_Left_Remarks.Text = dt.Rows[0]["GO2_Remark1"].ToString();
                txt_GO2_Left_Memo.Text = dt.Rows[0]["GO2_Memo1"].ToString();
                txt_GO2_Left_Scheme_no.Text = dt.Rows[0]["GO2_SchemeNo1"].ToString();
                txt_GO2_Left_Debit_Code.Text = dt.Rows[0]["GO2_DebitCredit1_Code"].ToString();
                txt_GO2_Left_Debit_Curr.Text = dt.Rows[0]["GO2_DebitCredit1_CCY"].ToString();
                txt_GO2_Left_Debit_Amt.Text = dt.Rows[0]["GO2_DebitCredit1_Amt"].ToString();
                txt_GO2_Left_Debit_Cust.Text = dt.Rows[0]["GO2_DebitCredit1_Cust_abbr"].ToString();
                txt_GO2_Left_Debit_Cust_Name.Text = dt.Rows[0]["GO2_DebitCredit1_Cust_Name"].ToString();
                txt_GO2_Left_Debit_Cust_AcCode.Text = dt.Rows[0]["GO2_DebitCredit1_Cust_AccCode"].ToString();
                txt_GO2_Left_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_DebitCredit1_Cust_AccCode_Disc"].ToString();
                txt_GO2_Left_Debit_Cust_AccNo.Text = dt.Rows[0]["GO2_DebitCredit1_Cust_AccNo"].ToString();
                txt_GO2_Left_Debit_Exch_Rate.Text = dt.Rows[0]["GO2_DebitCredit1_ExchRate"].ToString();
                txt_GO2_Left_Debit_Exch_CCY.Text = dt.Rows[0]["GO2_DebitCredit1_ExchCCY"].ToString();
                txt_GO2_Left_Debit_FUND.Text = dt.Rows[0]["GO2_DebitCredit1_Fund"].ToString();
                txt_GO2_Left_Debit_Check_No.Text = dt.Rows[0]["GO2_DebitCredit1_CheckNo"].ToString();
                txt_GO2_Left_Debit_Available.Text = dt.Rows[0]["GO2_DebitCredit1_Available"].ToString();
                txt_GO2_Left_Debit_AdPrint.Text = dt.Rows[0]["GO2_DebitCredit1_Advice_Print"].ToString();
                txt_GO2_Left_Debit_Details.Text = dt.Rows[0]["GO2_DebitCredit1_Details"].ToString();
                txt_GO2_Left_Debit_Entity.Text = dt.Rows[0]["GO2_DebitCredit1_Entity"].ToString();
                txt_GO2_Left_Debit_Division.Text = dt.Rows[0]["GO2_DebitCredit1_Division"].ToString();
                txt_GO2_Left_Debit_Inter_Amount.Text = dt.Rows[0]["GO2_DebitCredit1_InterAmt"].ToString();
                txt_GO2_Left_Debit_Inter_Rate.Text = dt.Rows[0]["GO2_DebitCredit1_InterRate"].ToString();
                txt_GO2_Left_Credit_Code.Text = dt.Rows[0]["GO2_DebitCredit2_Code"].ToString();
                txt_GO2_Left_Credit_Curr.Text = dt.Rows[0]["GO2_DebitCredit2_CCY"].ToString();
                txt_GO2_Left_Credit_Amt.Text = dt.Rows[0]["GO2_DebitCredit2_Amt"].ToString();
                txt_GO2_Left_Credit_Cust.Text = dt.Rows[0]["GO2_DebitCredit2_Cust_abbr"].ToString();
                txt_GO2_Left_Credit_Cust_Name.Text = dt.Rows[0]["GO2_DebitCredit2_Cust_Name"].ToString();
                txt_GO2_Left_Credit_Cust_AcCode.Text = dt.Rows[0]["GO2_DebitCredit2_Cust_AccCode"].ToString();
                txt_GO2_Left_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_DebitCredit2_Cust_AccCode_Disc"].ToString();
                txt_GO2_Left_Credit_Cust_AccNo.Text = dt.Rows[0]["GO2_DebitCredit2_Cust_AccNo"].ToString();
                txt_GO2_Left_Credit_Exch_Rate.Text = dt.Rows[0]["GO2_DebitCredit2_ExchRate"].ToString();
                txt_GO2_Left_Credit_Exch_Curr.Text = dt.Rows[0]["GO2_DebitCredit2_ExchCCY"].ToString();
                txt_GO2_Left_Credit_FUND.Text = dt.Rows[0]["GO2_DebitCredit2_Fund"].ToString();
                txt_GO2_Left_Credit_Check_No.Text = dt.Rows[0]["GO2_DebitCredit2_CheckNo"].ToString();
                txt_GO2_Left_Credit_Available.Text = dt.Rows[0]["GO2_DebitCredit2_Available"].ToString();
                txt_GO2_Left_Credit_AdPrint.Text = dt.Rows[0]["GO2_DebitCredit2_Advice_Print"].ToString();
                txt_GO2_Left_Credit_Details.Text = dt.Rows[0]["GO2_DebitCredit2_Details"].ToString();
                txt_GO2_Left_Credit_Entity.Text = dt.Rows[0]["GO2_DebitCredit2_Entity"].ToString();
                txt_GO2_Left_Credit_Division.Text = dt.Rows[0]["GO2_DebitCredit2_Division"].ToString();
                txt_GO2_Left_Credit_Inter_Amount.Text = dt.Rows[0]["GO2_DebitCredit2_InterAmt"].ToString();
                txt_GO2_Left_Credit_Inter_Rate.Text = dt.Rows[0]["GO2_DebitCredit2_InterRate"].ToString();

                Panel_GO2Right.Visible = true;
                txt_GO2_Right_Comment.Text = dt.Rows[0]["GO2_Comment2"].ToString();
                txt_GO2_Right_SectionNo.Text = dt.Rows[0]["GO2_Section2"].ToString();
                txt_GO2_Right_Remarks.Text = dt.Rows[0]["GO2_Remark2"].ToString();
                txt_GO2_Right_Memo.Text = dt.Rows[0]["GO2_Memo2"].ToString();
                txt_GO2_Right_Scheme_no.Text = dt.Rows[0]["GO2_SchemeNo2"].ToString();
                txt_GO2_Right_Debit_Code.Text = dt.Rows[0]["GO2_DebitCredit3_Code"].ToString();
                txt_GO2_Right_Debit_Curr.Text = dt.Rows[0]["GO2_DebitCredit3_CCY"].ToString();
                txt_GO2_Right_Debit_Amt.Text = dt.Rows[0]["GO2_DebitCredit3_Amt"].ToString();
                txt_GO2_Right_Debit_Cust.Text = dt.Rows[0]["GO2_DebitCredit3_Cust_abbr"].ToString();
                txt_GO2_Right_Debit_Cust_Name.Text = dt.Rows[0]["GO2_DebitCredit3_Cust_Name"].ToString();
                txt_GO2_Right_Debit_Cust_AcCode.Text = dt.Rows[0]["GO2_DebitCredit3_Cust_AccCode"].ToString();
                txt_GO2_Right_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_DebitCredit3_Cust_AccCode_Disc"].ToString();
                txt_GO2_Right_Debit_Cust_AccNo.Text = dt.Rows[0]["GO2_DebitCredit3_Cust_AccNo"].ToString();
                txt_GO2_Right_Debit_Exch_Rate.Text = dt.Rows[0]["GO2_DebitCredit3_ExchRate"].ToString();
                txt_GO2_Right_Debit_Exch_CCY.Text = dt.Rows[0]["GO2_DebitCredit3_ExchCCY"].ToString();
                txt_GO2_Right_Debit_FUND.Text = dt.Rows[0]["GO2_DebitCredit3_Fund"].ToString();
                txt_GO2_Right_Debit_Check_No.Text = dt.Rows[0]["GO2_DebitCredit3_CheckNo"].ToString();
                txt_GO2_Right_Debit_Available.Text = dt.Rows[0]["GO2_DebitCredit3_Available"].ToString();
                txt_GO2_Right_Debit_AdPrint.Text = dt.Rows[0]["GO2_DebitCredit3_Advice_Print"].ToString();
                txt_GO2_Right_Debit_Details.Text = dt.Rows[0]["GO2_DebitCredit3_Details"].ToString();
                txt_GO2_Right_Debit_Entity.Text = dt.Rows[0]["GO2_DebitCredit3_Entity"].ToString();
                txt_GO2_Right_Debit_Division.Text = dt.Rows[0]["GO2_DebitCredit3_Division"].ToString();
                txt_GO2_Right_Debit_Inter_Amount.Text = dt.Rows[0]["GO2_DebitCredit3_InterAmt"].ToString();
                txt_GO2_Right_Debit_Inter_Rate.Text = dt.Rows[0]["GO2_DebitCredit3_InterRate"].ToString();
                txt_GO2_Right_Credit_Code.Text = dt.Rows[0]["GO2_DebitCredit4_Code"].ToString();
                txt_GO2_Right_Credit_Curr.Text = dt.Rows[0]["GO2_DebitCredit4_CCY"].ToString();
                txt_GO2_Right_Credit_Amt.Text = dt.Rows[0]["GO2_DebitCredit4_Amt"].ToString();
                txt_GO2_Right_Credit_Cust.Text = dt.Rows[0]["GO2_DebitCredit4_Cust_abbr"].ToString();
                txt_GO2_Right_Credit_Cust_Name.Text = dt.Rows[0]["GO2_DebitCredit4_Cust_Name"].ToString();
                txt_GO2_Right_Credit_Cust_AcCode.Text = dt.Rows[0]["GO2_DebitCredit4_Cust_AccCode"].ToString();
                txt_GO2_Right_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_DebitCredit4_Cust_AccCode_Disc"].ToString();
                txt_GO2_Right_Credit_Cust_AccNo.Text = dt.Rows[0]["GO2_DebitCredit4_Cust_AccNo"].ToString();
                txt_GO2_Right_Credit_Exch_Rate.Text = dt.Rows[0]["GO2_DebitCredit4_ExchRate"].ToString();
                txt_GO2_Right_Credit_Exch_Curr.Text = dt.Rows[0]["GO2_DebitCredit4_ExchCCY"].ToString();
                txt_GO2_Right_Credit_FUND.Text = dt.Rows[0]["GO2_DebitCredit4_Fund"].ToString();
                txt_GO2_Right_Credit_Check_No.Text = dt.Rows[0]["GO2_DebitCredit4_CheckNo"].ToString();
                txt_GO2_Right_Credit_Available.Text = dt.Rows[0]["GO2_DebitCredit4_Available"].ToString();
                txt_GO2_Right_Credit_AdPrint.Text = dt.Rows[0]["GO2_DebitCredit4_Advice_Print"].ToString();
                txt_GO2_Right_Credit_Details.Text = dt.Rows[0]["GO2_DebitCredit4_Details"].ToString();
                txt_GO2_Right_Credit_Entity.Text = dt.Rows[0]["GO2_DebitCredit4_Entity"].ToString();
                txt_GO2_Right_Credit_Division.Text = dt.Rows[0]["GO2_DebitCredit4_Division"].ToString();
                txt_GO2_Right_Credit_Inter_Amount.Text = dt.Rows[0]["GO2_DebitCredit4_InterAmt"].ToString();
                txt_GO2_Right_Credit_Inter_Rate.Text = dt.Rows[0]["GO2_DebitCredit4_InterRate"].ToString();
            }
            if (dt.Rows[0]["GO3_Flag"].ToString() == "Y")
            {
                chk_GO3Flag.Checked = true;
                Panel_GO3Left.Visible = true;
                txt_GO3_Left_Comment.Text = dt.Rows[0]["GO3_Comment1"].ToString();
                txt_GO3_Left_SectionNo.Text = dt.Rows[0]["GO3_Section1"].ToString();
                txt_GO3_Left_Remarks.Text = dt.Rows[0]["GO3_Remark1"].ToString();
                txt_GO3_Left_Memo.Text = dt.Rows[0]["GO3_Memo1"].ToString();
                txt_GO3_Left_Scheme_no.Text = dt.Rows[0]["GO3_SchemeNo1"].ToString();
                txt_GO3_Left_Debit_Code.Text = dt.Rows[0]["GO3_DebitCredit1_Code"].ToString();
                txt_GO3_Left_Debit_Curr.Text = dt.Rows[0]["GO3_DebitCredit1_CCY"].ToString();
                txt_GO3_Left_Debit_Amt.Text = dt.Rows[0]["GO3_DebitCredit1_Amt"].ToString();
                txt_GO3_Left_Debit_Cust.Text = dt.Rows[0]["GO3_DebitCredit1_Cust_abbr"].ToString();
                txt_GO3_Left_Debit_Cust_Name.Text = dt.Rows[0]["GO3_DebitCredit1_Cust_Name"].ToString();
                txt_GO3_Left_Debit_Cust_AcCode.Text = dt.Rows[0]["GO3_DebitCredit1_Cust_AccCode"].ToString();
                txt_GO3_Left_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO3_DebitCredit1_Cust_AccCode_Disc"].ToString();
                txt_GO3_Left_Debit_Cust_AccNo.Text = dt.Rows[0]["GO3_DebitCredit1_Cust_AccNo"].ToString();
                txt_GO3_Left_Debit_Exch_Rate.Text = dt.Rows[0]["GO3_DebitCredit1_ExchRate"].ToString();
                txt_GO3_Left_Debit_Exch_CCY.Text = dt.Rows[0]["GO3_DebitCredit1_ExchCCY"].ToString();
                txt_GO3_Left_Debit_FUND.Text = dt.Rows[0]["GO3_DebitCredit1_Fund"].ToString();
                txt_GO3_Left_Debit_Check_No.Text = dt.Rows[0]["GO3_DebitCredit1_CheckNo"].ToString();
                txt_GO3_Left_Debit_Available.Text = dt.Rows[0]["GO3_DebitCredit1_Available"].ToString();
                txt_GO3_Left_Debit_AdPrint.Text = dt.Rows[0]["GO3_DebitCredit1_Advice_Print"].ToString();
                txt_GO3_Left_Debit_Details.Text = dt.Rows[0]["GO3_DebitCredit1_Details"].ToString();
                txt_GO3_Left_Debit_Entity.Text = dt.Rows[0]["GO3_DebitCredit1_Entity"].ToString();
                txt_GO3_Left_Debit_Division.Text = dt.Rows[0]["GO3_DebitCredit1_Division"].ToString();
                txt_GO3_Left_Debit_Inter_Amount.Text = dt.Rows[0]["GO3_DebitCredit1_InterAmt"].ToString();
                txt_GO3_Left_Debit_Inter_Rate.Text = dt.Rows[0]["GO3_DebitCredit1_InterRate"].ToString();
                txt_GO3_Left_Credit_Code.Text = dt.Rows[0]["GO3_DebitCredit2_Code"].ToString();
                txt_GO3_Left_Credit_Curr.Text = dt.Rows[0]["GO3_DebitCredit2_CCY"].ToString();
                txt_GO3_Left_Credit_Amt.Text = dt.Rows[0]["GO3_DebitCredit2_Amt"].ToString();
                txt_GO3_Left_Credit_Cust.Text = dt.Rows[0]["GO3_DebitCredit2_Cust_abbr"].ToString();
                txt_GO3_Left_Credit_Cust_Name.Text = dt.Rows[0]["GO3_DebitCredit2_Cust_Name"].ToString();
                txt_GO3_Left_Credit_Cust_AcCode.Text = dt.Rows[0]["GO3_DebitCredit2_Cust_AccCode"].ToString();
                txt_GO3_Left_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO3_DebitCredit2_Cust_AccCode_Disc"].ToString();
                txt_GO3_Left_Credit_Cust_AccNo.Text = dt.Rows[0]["GO3_DebitCredit2_Cust_AccNo"].ToString();
                txt_GO3_Left_Credit_Exch_Rate.Text = dt.Rows[0]["GO3_DebitCredit2_ExchRate"].ToString();
                txt_GO3_Left_Credit_Exch_Curr.Text = dt.Rows[0]["GO3_DebitCredit2_ExchCCY"].ToString();
                txt_GO3_Left_Credit_FUND.Text = dt.Rows[0]["GO3_DebitCredit2_Fund"].ToString();
                txt_GO3_Left_Credit_Check_No.Text = dt.Rows[0]["GO3_DebitCredit2_CheckNo"].ToString();
                txt_GO3_Left_Credit_Available.Text = dt.Rows[0]["GO3_DebitCredit2_Available"].ToString();
                txt_GO3_Left_Credit_AdPrint.Text = dt.Rows[0]["GO3_DebitCredit2_Advice_Print"].ToString();
                txt_GO3_Left_Credit_Details.Text = dt.Rows[0]["GO3_DebitCredit2_Details"].ToString();
                txt_GO3_Left_Credit_Entity.Text = dt.Rows[0]["GO3_DebitCredit2_Entity"].ToString();
                txt_GO3_Left_Credit_Division.Text = dt.Rows[0]["GO3_DebitCredit2_Division"].ToString();
                txt_GO3_Left_Credit_Inter_Amount.Text = dt.Rows[0]["GO3_DebitCredit2_InterAmt"].ToString();
                txt_GO3_Left_Credit_Inter_Rate.Text = dt.Rows[0]["GO3_DebitCredit2_InterRate"].ToString();

                Panel_GO3Right.Visible = true;
                txt_GO3_Right_Comment.Text = dt.Rows[0]["GO3_Comment2"].ToString();
                txt_GO3_Right_SectionNo.Text = dt.Rows[0]["GO3_Section2"].ToString();
                txt_GO3_Right_Remarks.Text = dt.Rows[0]["GO3_Remark2"].ToString();
                txt_GO3_Right_Memo.Text = dt.Rows[0]["GO3_Memo2"].ToString();
                txt_GO3_Right_Scheme_no.Text = dt.Rows[0]["GO3_SchemeNo2"].ToString();
                txt_GO3_Right_Debit_Code.Text = dt.Rows[0]["GO3_DebitCredit3_Code"].ToString();
                txt_GO3_Right_Debit_Curr.Text = dt.Rows[0]["GO3_DebitCredit3_CCY"].ToString();
                txt_GO3_Right_Debit_Amt.Text = dt.Rows[0]["GO3_DebitCredit3_Amt"].ToString();
                txt_GO3_Right_Debit_Cust.Text = dt.Rows[0]["GO3_DebitCredit3_Cust_abbr"].ToString();
                txt_GO3_Right_Debit_Cust_Name.Text = dt.Rows[0]["GO3_DebitCredit3_Cust_Name"].ToString();
                txt_GO3_Right_Debit_Cust_AcCode.Text = dt.Rows[0]["GO3_DebitCredit3_Cust_AccCode"].ToString();
                txt_GO3_Right_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO3_DebitCredit3_Cust_AccCode_Disc"].ToString();
                txt_GO3_Right_Debit_Cust_AccNo.Text = dt.Rows[0]["GO3_DebitCredit3_Cust_AccNo"].ToString();
                txt_GO3_Right_Debit_Exch_Rate.Text = dt.Rows[0]["GO3_DebitCredit3_ExchRate"].ToString();
                txt_GO3_Right_Debit_Exch_CCY.Text = dt.Rows[0]["GO3_DebitCredit3_ExchCCY"].ToString();
                txt_GO3_Right_Debit_FUND.Text = dt.Rows[0]["GO3_DebitCredit3_Fund"].ToString();
                txt_GO3_Right_Debit_Check_No.Text = dt.Rows[0]["GO3_DebitCredit3_CheckNo"].ToString();
                txt_GO3_Right_Debit_Available.Text = dt.Rows[0]["GO3_DebitCredit3_Available"].ToString();
                txt_GO3_Right_Debit_AdPrint.Text = dt.Rows[0]["GO3_DebitCredit3_Advice_Print"].ToString();
                txt_GO3_Right_Debit_Details.Text = dt.Rows[0]["GO3_DebitCredit3_Details"].ToString();
                txt_GO3_Right_Debit_Entity.Text = dt.Rows[0]["GO3_DebitCredit3_Entity"].ToString();
                txt_GO3_Right_Debit_Division.Text = dt.Rows[0]["GO3_DebitCredit3_Division"].ToString();
                txt_GO3_Right_Debit_Inter_Amount.Text = dt.Rows[0]["GO3_DebitCredit3_InterAmt"].ToString();
                txt_GO3_Right_Debit_Inter_Rate.Text = dt.Rows[0]["GO3_DebitCredit3_InterRate"].ToString();
                txt_GO3_Right_Credit_Code.Text = dt.Rows[0]["GO3_DebitCredit4_Code"].ToString();
                txt_GO3_Right_Credit_Curr.Text = dt.Rows[0]["GO3_DebitCredit4_CCY"].ToString();
                txt_GO3_Right_Credit_Amt.Text = dt.Rows[0]["GO3_DebitCredit4_Amt"].ToString();
                txt_GO3_Right_Credit_Cust.Text = dt.Rows[0]["GO3_DebitCredit4_Cust_abbr"].ToString();
                txt_GO3_Right_Credit_Cust_Name.Text = dt.Rows[0]["GO3_DebitCredit4_Cust_Name"].ToString();
                txt_GO3_Right_Credit_Cust_AcCode.Text = dt.Rows[0]["GO3_DebitCredit4_Cust_AccCode"].ToString();
                txt_GO3_Right_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO3_DebitCredit4_Cust_AccCode_Disc"].ToString();
                txt_GO3_Right_Credit_Cust_AccNo.Text = dt.Rows[0]["GO3_DebitCredit4_Cust_AccNo"].ToString();
                txt_GO3_Right_Credit_Exch_Rate.Text = dt.Rows[0]["GO3_DebitCredit4_ExchRate"].ToString();
                txt_GO3_Right_Credit_Exch_Curr.Text = dt.Rows[0]["GO3_DebitCredit4_ExchCCY"].ToString();
                txt_GO3_Right_Credit_FUND.Text = dt.Rows[0]["GO3_DebitCredit4_Fund"].ToString();
                txt_GO3_Right_Credit_Check_No.Text = dt.Rows[0]["GO3_DebitCredit4_CheckNo"].ToString();
                txt_GO3_Right_Credit_Available.Text = dt.Rows[0]["GO3_DebitCredit4_Available"].ToString();
                txt_GO3_Right_Credit_AdPrint.Text = dt.Rows[0]["GO3_DebitCredit4_Advice_Print"].ToString();
                txt_GO3_Right_Credit_Details.Text = dt.Rows[0]["GO3_DebitCredit4_Details"].ToString();
                txt_GO3_Right_Credit_Entity.Text = dt.Rows[0]["GO3_DebitCredit4_Entity"].ToString();
                txt_GO3_Right_Credit_Division.Text = dt.Rows[0]["GO3_DebitCredit4_Division"].ToString();
                txt_GO3_Right_Credit_Inter_Amount.Text = dt.Rows[0]["GO3_DebitCredit4_InterAmt"].ToString();
                txt_GO3_Right_Credit_Inter_Rate.Text = dt.Rows[0]["GO3_DebitCredit4_InterRate"].ToString();
            }
            if (dt.Rows[0]["GO_Acc_Change_Flag"].ToString() == "Y")
            {
                chk_GOAcccChangeFlag.Checked = true;
                panal_GOAccChange.Visible = true;
                txt_GOAccChange_Ref_No.Text = dt.Rows[0]["GO_Acc_Change_TransRef_No"].ToString();
                txt_GOAccChange_Comment.Text = dt.Rows[0]["GO_Acc_Change_Comment"].ToString();
                txt_GOAccChange_SectionNo.Text = dt.Rows[0]["GO_Acc_Change_Section"].ToString();
                txt_GOAccChange_Remarks.Text = dt.Rows[0]["GO_Acc_Change_Remark"].ToString();
                txt_GOAccChange_Memo.Text = dt.Rows[0]["GO_Acc_Change_Memo"].ToString();
                txt_GOAccChange_Scheme_no.Text = dt.Rows[0]["GO_Acc_Change_SchemeNo"].ToString();
                txt_GOAccChange_Debit_Code.Text = dt.Rows[0]["GO_Acc_Change_Debit_Code"].ToString();
                txt_GOAccChange_Debit_Curr.Text = dt.Rows[0]["GO_Acc_Change_Debit_CCY"].ToString();
                txt_GOAccChange_Debit_Amt.Text = dt.Rows[0]["GO_Acc_Change_Debit_Amt"].ToString();
                txt_GOAccChange_Debit_Cust.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_abbr"].ToString();
                txt_GOAccChange_Debit_Cust_Name.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_Name"].ToString();
                txt_GOAccChange_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccCode"].ToString();
                txt_GOAccChange_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccCode_Disc"].ToString();
                txt_GOAccChange_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccNo"].ToString();
                txt_GOAccChange_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Acc_Change_Debit_ExchRate"].ToString();
                txt_GOAccChange_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Acc_Change_Debit_ExchCCY"].ToString();
                txt_GOAccChange_Debit_FUND.Text = dt.Rows[0]["GO_Acc_Change_Debit_Fund"].ToString();
                txt_GOAccChange_Debit_Check_No.Text = dt.Rows[0]["GO_Acc_Change_Debit_CheckNo"].ToString();
                txt_GOAccChange_Debit_Available.Text = dt.Rows[0]["GO_Acc_Change_Debit_Available"].ToString();
                txt_GOAccChange_Debit_AdPrint.Text = dt.Rows[0]["GO_Acc_Change_Debit_Advice_Print"].ToString();
                txt_GOAccChange_Debit_Details.Text = dt.Rows[0]["GO_Acc_Change_Debit_Details"].ToString();
                txt_GOAccChange_Debit_Entity.Text = dt.Rows[0]["GO_Acc_Change_Debit_Entity"].ToString();
                txt_GOAccChange_Debit_Division.Text = dt.Rows[0]["GO_Acc_Change_Debit_Division"].ToString();
                txt_GOAccChange_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Acc_Change_Debit_InterAmt"].ToString();
                txt_GOAccChange_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Acc_Change_Debit_InterRate"].ToString();
                txt_GOAccChange_Credit_Code.Text = dt.Rows[0]["GO_Acc_Change_Credit_Code"].ToString();
                txt_GOAccChange_Credit_Curr.Text = dt.Rows[0]["GO_Acc_Change_Credit_CCY"].ToString();
                txt_GOAccChange_Credit_Amt.Text = dt.Rows[0]["GO_Acc_Change_Credit_Amt"].ToString();
                txt_GOAccChange_Credit_Cust.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_abbr"].ToString();
                txt_GOAccChange_Credit_Cust_Name.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_Name"].ToString();
                txt_GOAccChange_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccCode"].ToString();
                txt_GOAccChange_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccCode_Disc"].ToString();
                txt_GOAccChange_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccNo"].ToString();
                txt_GOAccChange_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Acc_Change_Credit_ExchRate"].ToString();
                txt_GOAccChange_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Acc_Change_Credit_ExchCCY"].ToString();
                txt_GOAccChange_Credit_FUND.Text = dt.Rows[0]["GO_Acc_Change_Credit_Fund"].ToString();
                txt_GOAccChange_Credit_Check_No.Text = dt.Rows[0]["GO_Acc_Change_Credit_CheckNo"].ToString();
                txt_GOAccChange_Credit_Available.Text = dt.Rows[0]["GO_Acc_Change_Credit_Available"].ToString();
                txt_GOAccChange_Credit_AdPrint.Text = dt.Rows[0]["GO_Acc_Change_Credit_Advice_Print"].ToString();
                txt_GOAccChange_Credit_Details.Text = dt.Rows[0]["GO_Acc_Change_Credit_Details"].ToString();
                txt_GOAccChange_Credit_Entity.Text = dt.Rows[0]["GO_Acc_Change_Credit_Entity"].ToString();
                txt_GOAccChange_Credit_Division.Text = dt.Rows[0]["GO_Acc_Change_Credit_Division"].ToString();
                txt_GOAccChange_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Acc_Change_Credit_InterAmt"].ToString();
                txt_GOAccChange_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Acc_Change_Credit_InterRate"].ToString();
            }
            ////// Swift
            txt_MT103Receiver.Text = dt.Rows[0]["Receiver_MT103"].ToString();
            txtInstructionCode.Text = dt.Rows[0]["InstructionCode"].ToString();
            txtTransactionTypeCode.Text = dt.Rows[0]["TransactionTypeCode"].ToString();
            txt103Date.Text = dt.Rows[0]["Valuedate103"].ToString();
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            txtInstructedAmount.Text = dt.Rows[0]["InstructedAmount"].ToString();
            txtExchangeRate.Text = dt.Rows[0]["ExchangeRate"].ToString();
            txtSendingInstitutionAccountNumber.Text = dt.Rows[0]["SendingInstitutionAccountNumber"].ToString();
            txtSendingInstitutionSwiftCode.Text = dt.Rows[0]["SendingInstitutionSwiftCode"].ToString();

            ddlOrderingInstitution.SelectedValue = dt.Rows[0]["OrderingInstitution"].ToString();
            txtOrderingInstitutionAccountNumber.Text = dt.Rows[0]["OrderingInstitutionAccountNumber"].ToString();
            txtOrderingInstitutionSwiftCode.Text = dt.Rows[0]["OrderingInstitutionSwiftCode"].ToString();
            txtOrderingInstitutionName.Text = dt.Rows[0]["OrderingInstitutionName"].ToString();
            txtOrderingInstitutionAddress1.Text = dt.Rows[0]["OrderingInstitutionAddress1"].ToString();
            txtOrderingInstitutionAddress2.Text = dt.Rows[0]["OrderingInstitutionAddress2"].ToString();
            txtOrderingInstitutionAddress3.Text = dt.Rows[0]["OrderingInstitutionAddress3"].ToString();
            ddlOrderingInstitution_TextChanged(null, null);

            ddlSendersCorrespondent.SelectedValue = dt.Rows[0]["SendersCorrespondent"].ToString();
            txtSendersCorrespondentAccountNumber.Text = dt.Rows[0]["SendersCorrespondentAccountNumber"].ToString();
            txtSendersCorrespondentSwiftCode.Text = dt.Rows[0]["SendersCorrespondentSwiftCode"].ToString();
            txtSendersCorrespondentName.Text = dt.Rows[0]["SendersCorrespondentName"].ToString();
            txtSendersCorrespondentLocation.Text = dt.Rows[0]["SendersCorrespondentLocation"].ToString();
            txtSendersCorrespondentAddress1.Text = dt.Rows[0]["SendersCorrespondentAddress1"].ToString();
            txtSendersCorrespondentAddress2.Text = dt.Rows[0]["SendersCorrespondentAddress2"].ToString();
            txtSendersCorrespondentAddress3.Text = dt.Rows[0]["SendersCorrespondentAddress3"].ToString();
            ddlSendersCorrespondent_TextChanged(null, null);

            ddlReceiversCorrespondent.SelectedValue = dt.Rows[0]["ReceiversCorrespondent"].ToString();
            txtReceiversCorrespondentAccountNumber.Text = dt.Rows[0]["ReceiversCorrespondentAccountNumber"].ToString();
            txtReceiversCorrespondentSwiftCode.Text = dt.Rows[0]["ReceiversCorrespondentSwiftCode"].ToString();
            txtReceiversCorrespondentName.Text = dt.Rows[0]["ReceiversCorrespondentName"].ToString();
            txtReceiversCorrespondentLocation.Text = dt.Rows[0]["ReceiversCorrespondentLocation"].ToString();
            txtReceiversCorrespondentAddress1.Text = dt.Rows[0]["ReceiversCorrespondentAddress1"].ToString();
            txtReceiversCorrespondentAddress2.Text = dt.Rows[0]["ReceiversCorrespondentAddress2"].ToString();
            txtReceiversCorrespondentAddress3.Text = dt.Rows[0]["ReceiversCorrespondentAddress3"].ToString();
            ddlReceiversCorrespondent_TextChanged(null, null);

            ddlThirdReimbursementInstitution.SelectedValue = dt.Rows[0]["ThirdReimbursementInstitution"].ToString();
            txtThirdReimbursementInstitutionAccountNumber.Text = dt.Rows[0]["ThirdReimbursementInstitutionAccountNumber"].ToString();
            txtThirdReimbursementInstitutionSwiftCode.Text = dt.Rows[0]["ThirdReimbursementInstitutionSwiftCode"].ToString();
            txtThirdReimbursementInstitutionName.Text = dt.Rows[0]["ThirdReimbursementInstitutionName"].ToString();
            txtThirdReimbursementInstitutionLocation.Text = dt.Rows[0]["ThirdReimbursementInstitutionLocation"].ToString();
            txtThirdReimbursementInstitutionAddress1.Text = dt.Rows[0]["ThirdReimbursementInstitutionAddress1"].ToString();
            txtThirdReimbursementInstitutionAddress2.Text = dt.Rows[0]["ThirdReimbursementInstitutionAddress2"].ToString();
            txtThirdReimbursementInstitutionAddress3.Text = dt.Rows[0]["ThirdReimbursementInstitutionAddress3"].ToString();
            ddlThirdReimbursementInstitution_TextChanged(null, null);

            txtDetailsOfCharges.Text = dt.Rows[0]["DetailsOfCharges"].ToString();
            txtSenderCharges.Text = dt.Rows[0]["SenderCharges"].ToString();
            txtSenderCharges2.Text = dt.Rows[0]["SenderCharges2"].ToString();
            txtReceiverCharges.Text = dt.Rows[0]["ReceiverCharges"].ToString();
            txtReceiverCharges2.Text = dt.Rows[0]["ReceiverCharges2"].ToString();
            txtSendertoReceiverInformation.Text = dt.Rows[0]["SendertoReceiverInformation"].ToString();
            txtSendertoReceiverInformation2.Text = dt.Rows[0]["SendertoReceiverInformation2"].ToString();
            txtSendertoReceiverInformation3.Text = dt.Rows[0]["SendertoReceiverInformation3"].ToString();
            txtSendertoReceiverInformation4.Text = dt.Rows[0]["SendertoReceiverInformation4"].ToString();
            txtSendertoReceiverInformation5.Text = dt.Rows[0]["SendertoReceiverInformation5"].ToString();
            txtSendertoReceiverInformation6.Text = dt.Rows[0]["SendertoReceiverInformation6"].ToString();
            txtRegulatoryReporting.Text = dt.Rows[0]["RegulatoryReporting"].ToString();
            txtRegulatoryReporting2.Text = dt.Rows[0]["RegulatoryReporting2"].ToString();
            txtRegulatoryReporting3.Text = dt.Rows[0]["RegulatoryReporting3"].ToString();

            txtTimeIndication.Text = dt.Rows[0]["Time_Indication"].ToString();
            ddlBankOperationCode.SelectedValue = dt.Rows[0]["Bank_Operation_Code"].ToString();
            ddlOrderingCustomer.SelectedValue = dt.Rows[0]["OrderingCustomer_header"].ToString();
            txtOrderingCustomer_Acc.Text = dt.Rows[0]["OrderingCustomer_AcNo"].ToString();
            txtOrderingCustomer_SwiftCode.Text = dt.Rows[0]["OrderingCustomer_SwiftCode"].ToString();
            txtOrderingCustomer_Name.Text = dt.Rows[0]["OrderingCustomer_Name"].ToString();
            txtOrderingCustomer_Addr1.Text = dt.Rows[0]["OrderingCustomer_Addr1"].ToString();
            txtOrderingCustomer_Addr2.Text = dt.Rows[0]["OrderingCustomer_Addr2"].ToString();
            txtOrderingCustomer_Addr3.Text = dt.Rows[0]["OrderingCustomer_Addr3"].ToString();
            ddlOrderingCustomer_TextChanged(null, null);
            ddlBeneficiaryCustomer.SelectedValue = dt.Rows[0]["BeneficiaryCustomer_header"].ToString();
            txtBeneficiaryCustomerAccountNumber.Text = dt.Rows[0]["BeneficiaryCustomerAccountNumber"].ToString();
            txtBeneficiaryCustomerSwiftCode.Text = dt.Rows[0]["BeneficiaryCustomerSwiftCode"].ToString();
            txtBeneficiaryCustomerName.Text = dt.Rows[0]["BeneficiaryCustomer_Name"].ToString();
            txtBeneficiaryCustomerAddr1.Text = dt.Rows[0]["BeneficiaryCustomer_Addr1"].ToString();
            txtBeneficiaryCustomerAddr2.Text = dt.Rows[0]["BeneficiaryCustomer_Addr2"].ToString();
            txtBeneficiaryCustomerAddr3.Text = dt.Rows[0]["BeneficiaryCustomer_Addr3"].ToString();
            ddlBeneficiaryCustomer_TextChanged(null, null);
            txtRemittanceInformation1.Text = dt.Rows[0]["Remittance_Information1"].ToString();
            txtRemittanceInformation2.Text = dt.Rows[0]["Remittance_Information2"].ToString();
            txtRemittanceInformation3.Text = dt.Rows[0]["Remittance_Information3"].ToString();
            txtRemittanceInformation4.Text = dt.Rows[0]["Remittance_Information4"].ToString();

            ddlOrderingInstitution202.SelectedValue = dt.Rows[0]["OrderingInstitution202"].ToString();
            txtOrderingInstitution202AccountNumber.Text = dt.Rows[0]["OrderingInstitution202AccountNumber"].ToString();
            txtOrderingInstitution202SwiftCode.Text = dt.Rows[0]["OrderingInstitution202SwiftCode"].ToString();
            txtOrderingInstitution202Name.Text = dt.Rows[0]["OrderingInstitution202Name"].ToString();
            txtOrderingInstitution202Address1.Text = dt.Rows[0]["OrderingInstitution202Address1"].ToString();
            txtOrderingInstitution202Address2.Text = dt.Rows[0]["OrderingInstitution202Address2"].ToString();
            txtOrderingInstitution202Address3.Text = dt.Rows[0]["OrderingInstitution202Address3"].ToString();
            ddlOrderingInstitution202_TextChanged(null, null);

            ddlSendersCorrespondent202.SelectedValue = dt.Rows[0]["SendersCorrespondent202"].ToString();
            txtSendersCorrespondent202AccountNumber.Text = dt.Rows[0]["SendersCorrespondent202AccountNumber"].ToString();
            txtSendersCorrespondent202SwiftCode.Text = dt.Rows[0]["SendersCorrespondent202SwiftCode"].ToString();
            txtSendersCorrespondent202Name.Text = dt.Rows[0]["SendersCorrespondent202Name"].ToString();
            txtSendersCorrespondent202Location.Text = dt.Rows[0]["SendersCorrespondent202Location"].ToString();
            txtSendersCorrespondent202Address1.Text = dt.Rows[0]["SendersCorrespondent202Address1"].ToString();
            txtSendersCorrespondent202Address2.Text = dt.Rows[0]["SendersCorrespondent202Address2"].ToString();
            txtSendersCorrespondent202Address3.Text = dt.Rows[0]["SendersCorrespondent202Address3"].ToString();
            ddlSendersCorrespondent202_TextChanged(null, null);

            ddlReceiversCorrespondent202.SelectedValue = dt.Rows[0]["ReceiversCorrespondent202"].ToString();
            txtReceiversCorrespondent202AccountNumber.Text = dt.Rows[0]["ReceiversCorrespondent202AccountNumber"].ToString();
            txtReceiversCorrespondent202SwiftCode.Text = dt.Rows[0]["ReceiversCorrespondent202SwiftCode"].ToString();
            txtReceiversCorrespondent202Name.Text = dt.Rows[0]["ReceiversCorrespondent202Name"].ToString();
            txtReceiversCorrespondent202Location.Text = dt.Rows[0]["ReceiversCorrespondent202Location"].ToString();
            txtReceiversCorrespondent202Address1.Text = dt.Rows[0]["ReceiversCorrespondent202Address1"].ToString();
            txtReceiversCorrespondent202Address2.Text = dt.Rows[0]["ReceiversCorrespondent202Address2"].ToString();
            txtReceiversCorrespondent202Address3.Text = dt.Rows[0]["ReceiversCorrespondent202Address3"].ToString();
            ddlReceiversCorrespondent202_TextChanged(null, null);

            //MT 200
            txt200BicCode.Text = dt.Rows[0]["BicCode200"].ToString();
            txt200TransactionRefNO.Text = dt.Rows[0]["TransactionRefNO200"].ToString();
            txt200Date.Text = dt.Rows[0]["Date200"].ToString();
            txt200Currency.Text = dt.Rows[0]["Currency200"].ToString();
            txt200Amount.Text = dt.Rows[0]["Amount200"].ToString();
            txt200SenderCorreCode.Text = dt.Rows[0]["SenderCorreCode200"].ToString();
            txt200SenderCorreLocation.Text = dt.Rows[0]["SenderCorreLocation200"].ToString();

            ddl200Intermediary.SelectedValue = dt.Rows[0]["Intermediary200"].ToString();
            txt200IntermediaryAccountNumber.Text = dt.Rows[0]["IntermediaryAccountNumber200"].ToString();
            txt200IntermediarySwiftCode.Text = dt.Rows[0]["IntermediarySwiftCode200"].ToString();
            txt200IntermediaryName.Text = dt.Rows[0]["IntermediaryName200"].ToString();
            txt200IntermediaryAddress1.Text = dt.Rows[0]["IntermediaryAddress1200"].ToString();
            txt200IntermediaryAddress2.Text = dt.Rows[0]["IntermediaryAddress2200"].ToString();
            txt200IntermediaryAddress3.Text = dt.Rows[0]["IntermediaryAddress3200"].ToString();
            ddl200Intermediary_TextChanged(null, null);

            ddl200AccWithInstitution.SelectedValue = dt.Rows[0]["AccWithInstitution200"].ToString();
            txt200AccWithInstitutionAccountNumber.Text = dt.Rows[0]["AccWithInstitutionAccountNumber200"].ToString();
            txt200AccWithInstitutionSwiftCode.Text = dt.Rows[0]["AccWithInstitutionSwiftCode200"].ToString();
            txt200AccWithInstitutionLocation.Text = dt.Rows[0]["AccWithInstitutionLocation200"].ToString();
            txt200AccWithInstitutionName.Text = dt.Rows[0]["AccWithInstitutionName200"].ToString();
            txt200AccWithInstitutionAddress1.Text = dt.Rows[0]["AccWithInstitutionAddress1200"].ToString();
            txt200AccWithInstitutionAddress2.Text = dt.Rows[0]["AccWithInstitutionAddress2200"].ToString();
            txt200AccWithInstitutionAddress3.Text = dt.Rows[0]["AccWithInstitutionAddress3200"].ToString();
            ddl200AccWithInstitution_TextChanged(null, null);

            txt200SendertoReceiverInformation1.Text = dt.Rows[0]["SendertoReceiverInformation1200"].ToString();
            txt200SendertoReceiverInformation2.Text = dt.Rows[0]["SendertoReceiverInformation2200"].ToString();
            txt200SendertoReceiverInformation3.Text = dt.Rows[0]["SendertoReceiverInformation3200"].ToString();
            txt200SendertoReceiverInformation4.Text = dt.Rows[0]["SendertoReceiverInformation4200"].ToString();
            txt200SendertoReceiverInformation5.Text = dt.Rows[0]["SendertoReceiverInformation5200"].ToString();
            txt200SendertoReceiverInformation6.Text = dt.Rows[0]["SendertoReceiverInformation6200"].ToString();

            txtTransactionRefNoR42.Text = dt.Rows[0]["TransactionRefNoR42"].ToString();
            txtRelatedReferenceR42.Text = dt.Rows[0]["RelatedReferenceR42"].ToString();
            txtValueDateR42.Text = dt.Rows[0]["ValueDateR42"].ToString();
            txtCureencyR42.Text = dt.Rows[0]["CureencyR42"].ToString();
            txtAmountR42.Text = dt.Rows[0]["AmountR42"].ToString();
            txtOrderingInstitutionIFSCR42.Text = dt.Rows[0]["OrderingInstitutionIFSCR42"].ToString();
            txtBeneficiaryInstitutionIFSCR42.Text = dt.Rows[0]["BeneficiaryInstitutionIFSCR42"].ToString();
            txtCodeWordR42.Text = dt.Rows[0]["CodeWordR42"].ToString();
            txtAdditionalInformationR42.Text = dt.Rows[0]["AdditionalInformationR42"].ToString();
            txtMoreInfo1R42.Text = dt.Rows[0]["MoreInfo1R42"].ToString();
            txtMoreInfo2R42.Text = dt.Rows[0]["MoreInfo2R42"].ToString();
            txtMoreInfo3R42.Text = dt.Rows[0]["MoreInfo3R42"].ToString();
            txtMoreInfo4R42.Text = dt.Rows[0]["MoreInfo4R42"].ToString();
            txtMoreInfo5R42.Text = dt.Rows[0]["MoreInfo5R42"].ToString();

            if (dt.Rows[0]["swift_None"].ToString() == "Y")
            {
                rdb_swift_None.Checked = true;
            }
            if (dt.Rows[0]["swift_103"].ToString() == "Y")
            {
                rdb_swift_103.Checked = true;
            }
            if (dt.Rows[0]["swift_202"].ToString() == "Y")
            {
                rdb_swift_202.Checked = true;
            }
            if (dt.Rows[0]["swift_200"].ToString() == "Y")
            {
                rdb_swift_200.Checked = true;
            }
            if (dt.Rows[0]["swift_R42"].ToString() == "Y")
            {
                rdb_swift_R42.Checked = true;
            }
            //----------------------------------------Nilesh start----------------------------------------
            if (dt.Rows[0]["MT754_Flag"].ToString() == "Y")
            {
                rdb_swift_754.Checked = true;
            }


            txt202Amount.Text = dt.Rows[0]["Amount202"].ToString();
            // MT 202 & 103 changes 02122019
            ddlIntermediary202.SelectedValue = dt.Rows[0]["Intermediary202"].ToString();
            txtIntermediary202AccountNumber.Text = dt.Rows[0]["Intermediary202AccountNumber"].ToString();
            txtIntermediary202SwiftCode.Text = dt.Rows[0]["Intermediary202SwiftCode"].ToString();
            txtIntermediary202Name.Text = dt.Rows[0]["Intermediary202Name"].ToString();
            txtIntermediary202Address1.Text = dt.Rows[0]["Intermediary202Address1"].ToString();
            txtIntermediary202Address2.Text = dt.Rows[0]["Intermediary202Address2"].ToString();
            txtIntermediary202Address3.Text = dt.Rows[0]["Intermediary202Address3"].ToString();
            ddlIntermediary202_TextChanged(null, null);

            ddlAccountWithInstitution202.SelectedValue = dt.Rows[0]["AccountWithInstitution202"].ToString();
            txtAccountWithInstitution202AccountNumber.Text = dt.Rows[0]["AccountWithInstitution202AccountNumber"].ToString();
            txtAccountWithInstitution202SwiftCode.Text = dt.Rows[0]["AccountWithInstitution202SwiftCode"].ToString();
            txtAccountWithInstitution202Name.Text = dt.Rows[0]["AccountWithInstitution202Name"].ToString();
            txtAccountWithInstitution202Location.Text = dt.Rows[0]["AccountWithInstitution202Location"].ToString();
            txtAccountWithInstitution202Address1.Text = dt.Rows[0]["AccountWithInstitution202Address1"].ToString();
            txtAccountWithInstitution202Address2.Text = dt.Rows[0]["AccountWithInstitution202Address2"].ToString();
            txtAccountWithInstitution202Address3.Text = dt.Rows[0]["AccountWithInstitution202Address3"].ToString();
            ddlAccountWithInstitution202_TextChanged(null, null);

            ddlBeneficiaryInstitution202.SelectedValue = dt.Rows[0]["BeneficiaryInstitution202"].ToString();
            txtBeneficiaryInstitution202AccountNumber.Text = dt.Rows[0]["BeneficiaryInstitution202AccountNumber"].ToString();
            txtBeneficiaryInstitution202SwiftCode.Text = dt.Rows[0]["BeneficiaryInstitution202SwiftCode"].ToString();
            txtBeneficiaryInstitution202Name.Text = dt.Rows[0]["BeneficiaryInstitution202Name"].ToString();
            txtBeneficiaryInstitution202Address1.Text = dt.Rows[0]["BeneficiaryInstitution202Address1"].ToString();
            txtBeneficiaryInstitution202Address2.Text = dt.Rows[0]["BeneficiaryInstitution202Address2"].ToString();
            txtBeneficiaryInstitution202Address3.Text = dt.Rows[0]["BeneficiaryInstitution202Address3"].ToString();
            ddlBeneficiaryInstitution202_TextChanged(null, null);

            ddlIntermediary103.SelectedValue = dt.Rows[0]["Intermediary103"].ToString();
            txtIntermediary103AccountNumber.Text = dt.Rows[0]["Intermediary103AccountNumber"].ToString();
            txtIntermediary103SwiftCode.Text = dt.Rows[0]["Intermediary103SwiftCode"].ToString();
            txtIntermediary103Name.Text = dt.Rows[0]["Intermediary103Name"].ToString();
            txtIntermediary103Address1.Text = dt.Rows[0]["Intermediary103Address1"].ToString();
            txtIntermediary103Address2.Text = dt.Rows[0]["Intermediary103Address2"].ToString();
            txtIntermediary103Address3.Text = dt.Rows[0]["Intermediary103Address3"].ToString();
            ddlIntermediary103_TextChanged(null, null);

            ddlAccountWithInstitution103.SelectedValue = dt.Rows[0]["AccountWithInstitution103"].ToString();
            txtAccountWithInstitution103AccountNumber.Text = dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString();
            txtAccountWithInstitution103SwiftCode.Text = dt.Rows[0]["AccountWithInstitution103SwiftCode"].ToString();
            txtAccountWithInstitution103Name.Text = dt.Rows[0]["AccountWithInstitution103Name"].ToString();
            txtAccountWithInstitution103Location.Text = dt.Rows[0]["AccountWithInstitution103Location"].ToString();
            txtAccountWithInstitution103Address1.Text = dt.Rows[0]["AccountWithInstitution103Address1"].ToString();
            txtAccountWithInstitution103Address2.Text = dt.Rows[0]["AccountWithInstitution103Address2"].ToString();
            txtAccountWithInstitution103Address3.Text = dt.Rows[0]["AccountWithInstitution103Address3"].ToString();
            ddlAccountWithInstitution103_TextChanged(null, null);
            // MT 202 & 103 changes 02122019

            txtSenderToReceiverInformation2021.Text = dt.Rows[0]["SenderToReceiverInformation2021"].ToString();
            txtSenderToReceiverInformation2022.Text = dt.Rows[0]["SenderToReceiverInformation2022"].ToString();
            txtSenderToReceiverInformation2023.Text = dt.Rows[0]["SenderToReceiverInformation2023"].ToString();
            txtSenderToReceiverInformation2024.Text = dt.Rows[0]["SenderToReceiverInformation2024"].ToString();
            txtSenderToReceiverInformation2025.Text = dt.Rows[0]["SenderToReceiverInformation2025"].ToString();
            txtSenderToReceiverInformation2026.Text = dt.Rows[0]["SenderToReceiverInformation2026"].ToString();



            //----------------------------------------------------Nilesh Start------------------------------------------------------------------------------------------//
            if (dt.Rows[0]["MT754_Flag"].ToString() == "Y")
            {
                rdb_swift_754.Checked = true;
            }
            else
            {
                rdb_swift_754.Checked = false;
            }
            txt_754_SenRef.Text = dt.Rows[0]["Sender_Ref_754"].ToString();
            txt_754_RelRef.Text = dt.Rows[0]["Related_Ref_754"].ToString();
            ddlPrinAmtPaidAccNego_754.SelectedValue = dt.Rows[0]["Pri_Amt_Paid_754"].ToString();
            ddlPrinAmtPaidAccNego_754_TextChanged(null, null);
            txtPrinAmtPaidAccNegoDate_754.Text = dt.Rows[0]["Pri_AMT_Date_754"].ToString();
            txtPrinAmtPaidAccNegoCurr_754.Text = dt.Rows[0]["Pri_AMT_Curr_754"].ToString();
            txtPrinAmtPaidAccNegoAmt_754.Text = dt.Rows[0]["Pri_AMT_Amt_754"].ToString();
            txt_754_AddAmtClamd_Ccy.Text = dt.Rows[0]["Additional_AMT_Curr_754"].ToString();
            txt_754_AddAmtClamd_Amt.Text = dt.Rows[0]["Additional_AMT_Amt_754"].ToString();
            txt_754_ChargesDeduct1.Text = dt.Rows[0]["Charges_deducted1_754"].ToString();
            txt_754_ChargesDeduct2.Text = dt.Rows[0]["Charges_deducted2_754"].ToString();
            txt_754_ChargesDeduct3.Text = dt.Rows[0]["Charges_deducted3_754"].ToString();
            txt_754_ChargesDeduct4.Text = dt.Rows[0]["Charges_deducted4_754"].ToString();
            txt_754_ChargesDeduct5.Text = dt.Rows[0]["Charges_deducted5_754"].ToString();
            txt_754_ChargesDeduct6.Text = dt.Rows[0]["Charges_deducted6_754"].ToString();
            txt_754_ChargesAdded1.Text = dt.Rows[0]["Charges_Added1_754"].ToString();
            txt_754_ChargesAdded2.Text = dt.Rows[0]["Charges_Added2_754"].ToString();
            txt_754_ChargesAdded3.Text = dt.Rows[0]["Charges_Added3_754"].ToString();
            txt_754_ChargesAdded4.Text = dt.Rows[0]["Charges_Added4_754"].ToString();
            txt_754_ChargesAdded5.Text = dt.Rows[0]["Charges_Added5_754"].ToString();
            txt_754_ChargesAdded6.Text = dt.Rows[0]["Charges_Added6_754"].ToString();
            ddlTotalAmtclamd_754.SelectedValue = dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString();
            ddlTotalAmtclamd_754_TextChanged(null, null);
            txt_754_TotalAmtClmd_Date.Text = dt.Rows[0]["Ttl_Amt_Clmd_Date_754"].ToString();
            txt_754_TotalAmtClmd_Ccy.Text = dt.Rows[0]["Ttl_Amt_Clmd_Curr_754"].ToString();
            txt_754_TotalAmtClmd_Amt.Text = dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString();
            ddlReimbursingbank_754.SelectedValue = dt.Rows[0]["Reimbursing_Bank_754"].ToString();
            ddlReimbursingbank_754_TextChanged(null, null);
            txtReimbursingBankAccountnumber_754.Text = dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString();
            txtReimbursingBankIdentifiercode_754.Text = dt.Rows[0]["Reimbur_Bank_IdentCode_754"].ToString();
            txtReimbursingBankpartyidentifier_754.Text = dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString();
            txtReimbursingBankLocation_754.Text = dt.Rows[0]["Reimbur_Bank_Lctn_754"].ToString();
            txtReimbursingBankName_754.Text = dt.Rows[0]["Reimbursing_Bank_Name_754"].ToString();
            txtReimbursingBankAddress1_754.Text = dt.Rows[0]["Reimbur_Bank_Addrs1_754"].ToString();
            txtReimbursingBankAddress2_754.Text = dt.Rows[0]["Reimbur_Bank_Addrs2_754"].ToString();
            txtReimbursingBankAddress3_754.Text = dt.Rows[0]["Reimbur_Bank_Addrs3_754"].ToString();
            ddlAccountwithbank_754.SelectedValue = dt.Rows[0]["Account_With_Bank"].ToString();
            ddlAccountwithbank_754_TextChanged(null, null);
            txtAccountwithBankAccountnumber_754.Text = dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString();
            txtAccountwithBankIdentifiercode_754.Text = dt.Rows[0]["Acnt_With_Bank_IdentCode_754"].ToString();
            txtAccountwithBankpartyidentifier_754.Text = dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString();
            txtAccountwithBankLocation_754.Text = dt.Rows[0]["Acnt_With_Bank_Loctn_754"].ToString();
            txtAccountwithBankName_754.Text = dt.Rows[0]["Acnt_With_Bank_Name"].ToString();
            txtAccountwithBankAddress1_754.Text = dt.Rows[0]["Acnt_With_Bank_Addr1_754"].ToString();
            txtAccountwithBankAddress2_754.Text = dt.Rows[0]["Acnt_With_Bank_Addr2_754"].ToString();
            txtAccountwithBankAddress3_754.Text = dt.Rows[0]["Acnt_With_Bank_Addr3_754"].ToString();

            ddlBeneficiarybank_754.SelectedValue = dt.Rows[0]["Banificiary_Bank_754"].ToString();
            ddlBeneficiarybank_754_TextChanged(null, null);
            txtBeneficiaryBankAccountnumber_754.Text = dt.Rows[0]["Banificiary_AccNo_754"].ToString();
            txtBeneficiarypartyidentifire.Text = dt.Rows[0]["Banificiary_PartyIdent_754"].ToString();
            txtBeneficiaryBankIdentifiercode_754.Text = dt.Rows[0]["Banificiary_IdentCode_754"].ToString();
            txtBeneficiaryBankName_754.Text = dt.Rows[0]["Banificiary_Bank_Name_754"].ToString();
            txtBeneficiaryBankAddress1_754.Text = dt.Rows[0]["Banificiary_Addr1_754"].ToString();
            txtBeneficiaryBankAddress2_754.Text = dt.Rows[0]["Banificiary_Addr2_754"].ToString();
            txtBeneficiaryBankAddress3_754.Text = dt.Rows[0]["Banificiary_Addr3_754"].ToString();

            txt_754_SenRecInfo1.Text = dt.Rows[0]["SendertoReceiverInformation_754"].ToString();
            txt_754_SenRecInfo2.Text = dt.Rows[0]["SendertoReceiverInformation2_754"].ToString();
            txt_754_SenRecInfo3.Text = dt.Rows[0]["SendertoReceiverInformation3_754"].ToString();
            txt_754_SenRecInfo4.Text = dt.Rows[0]["SendertoReceiverInformation4_754"].ToString();
            txt_754_SenRecInfo5.Text = dt.Rows[0]["SendertoReceiverInformation5_754"].ToString();
            txt_754_SenRecInfo6.Text = dt.Rows[0]["SendertoReceiverInformation6_754"].ToString();


            txt_Narrative_754_1.Text = dt.Rows[0]["Narrative754_1"].ToString();
            txt_Narrative_754_2.Text = dt.Rows[0]["Narrative754_2"].ToString();
            txt_Narrative_754_3.Text = dt.Rows[0]["Narrative754_3"].ToString();
            txt_Narrative_754_4.Text = dt.Rows[0]["Narrative754_4"].ToString();
            txt_Narrative_754_5.Text = dt.Rows[0]["Narrative754_5"].ToString();
            txt_Narrative_754_6.Text = dt.Rows[0]["Narrative754_6"].ToString();
            txt_Narrative_754_7.Text = dt.Rows[0]["Narrative754_7"].ToString();
            txt_Narrative_754_8.Text = dt.Rows[0]["Narrative754_8"].ToString();
            txt_Narrative_754_9.Text = dt.Rows[0]["Narrative754_9"].ToString();
            txt_Narrative_754_10.Text = dt.Rows[0]["Narrative754_10"].ToString();
            txt_Narrative_754_11.Text = dt.Rows[0]["Narrative754_11"].ToString();
            txt_Narrative_754_12.Text = dt.Rows[0]["Narrative754_12"].ToString();
            txt_Narrative_754_13.Text = dt.Rows[0]["Narrative754_13"].ToString();
            txt_Narrative_754_14.Text = dt.Rows[0]["Narrative754_14"].ToString();
            txt_Narrative_754_15.Text = dt.Rows[0]["Narrative754_15"].ToString();
            txt_Narrative_754_16.Text = dt.Rows[0]["Narrative754_16"].ToString();
            txt_Narrative_754_17.Text = dt.Rows[0]["Narrative754_17"].ToString();
            txt_Narrative_754_18.Text = dt.Rows[0]["Narrative754_18"].ToString();
            txt_Narrative_754_19.Text = dt.Rows[0]["Narrative754_19"].ToString();
            txt_Narrative_754_20.Text = dt.Rows[0]["Narrative754_20"].ToString();
            txt_Narrative_754_21.Text = dt.Rows[0]["Narrative754_21"].ToString();
            txt_Narrative_754_22.Text = dt.Rows[0]["Narrative754_22"].ToString();
            txt_Narrative_754_23.Text = dt.Rows[0]["Narrative754_23"].ToString();
            txt_Narrative_754_24.Text = dt.Rows[0]["Narrative754_24"].ToString();
            txt_Narrative_754_25.Text = dt.Rows[0]["Narrative754_25"].ToString();
            txt_Narrative_754_26.Text = dt.Rows[0]["Narrative754_26"].ToString();
            txt_Narrative_754_27.Text = dt.Rows[0]["Narrative754_27"].ToString();
            txt_Narrative_754_28.Text = dt.Rows[0]["Narrative754_28"].ToString();
            txt_Narrative_754_29.Text = dt.Rows[0]["Narrative754_29"].ToString();
            txt_Narrative_754_30.Text = dt.Rows[0]["Narrative754_30"].ToString();
            txt_Narrative_754_31.Text = dt.Rows[0]["Narrative754_31"].ToString();
            txt_Narrative_754_32.Text = dt.Rows[0]["Narrative754_32"].ToString();
            txt_Narrative_754_33.Text = dt.Rows[0]["Narrative754_33"].ToString();
            txt_Narrative_754_34.Text = dt.Rows[0]["Narrative754_34"].ToString();
            txt_Narrative_754_35.Text = dt.Rows[0]["Narrative754_35"].ToString();
            //----------------------------------------------------Nilesh END-----------------------------------------------------------------------------------------//

        }
    }
    protected void Get_Acceptance_Get_Date_Diff(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PInterest_From = new SqlParameter("@Interest_From", txt_UnderLC_Interest_From.Text.ToString());
        SqlParameter PInterest_To = new SqlParameter("@Interest_To", txt_UnderLC_Interest_To.Text.ToString());
        DataTable Date_dt = new DataTable();
        Date_dt = obj.getData("TF_IMP_Acceptance_Get_Date_Diff", PInterest_From, PInterest_To);
        if (Date_dt.Rows.Count > 0)
        {
            txt_UnderLC_No_Of_Days.Text = Date_dt.Rows[0]["NoOfDays"].ToString();
            txt_UnderLC_Rate.Focus();
        }
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
            GBaseFile();
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        if (lblCollection_Lodgment_UnderLC.Text.Trim() == "Settlment_Collection")
        {
            Lei_Audit_Collection();
        }
        else
        {
            Lei_Audit_LC();
        }
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        SqlParameter P_CheckedBy = new SqlParameter("@CheckBy", Session["userName"].ToString());
        string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectSettlement", P_DocNo, P_Status, P_RejectReason, P_CheckedBy);
        Response.Redirect("TF_IMP_Settlement_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_Settlement_Checker_View.aspx", true);
    }
    protected void ddlOrderingCustomer_TextChanged(object sender, EventArgs e)
    {
        if (ddlOrderingCustomer.SelectedValue == "A")
        {
            lblOrderingCustomer_SwiftCode.Text = "Swift Code: ";
            txtOrderingCustomer_SwiftCode.Visible = true;

            txtOrderingCustomer_Name.Visible = false;
            lblOrderingCustomer_Addr1.Visible = false;
            lblOrderingCustomer_Addr2.Visible = false;
            lblOrderingCustomer_Addr3.Visible = false;
            txtOrderingCustomer_Addr1.Visible = false;
            txtOrderingCustomer_Addr2.Visible = false;
            txtOrderingCustomer_Addr3.Visible = false;
        }
        else if (ddlOrderingCustomer.SelectedValue == "F")
        {
            lblOrderingCustomer_SwiftCode.Text = "Name: ";
            txtOrderingCustomer_SwiftCode.Visible = false;

            txtOrderingCustomer_Name.Visible = true;
            lblOrderingCustomer_Addr1.Visible = true;
            lblOrderingCustomer_Addr2.Visible = true;
            lblOrderingCustomer_Addr3.Visible = true;
            txtOrderingCustomer_Addr1.Visible = true;
            txtOrderingCustomer_Addr2.Visible = true;
            txtOrderingCustomer_Addr3.Visible = true;
        }
        else if (ddlOrderingCustomer.SelectedValue == "K")
        {
            lblOrderingCustomer_SwiftCode.Text = "Name: ";
            txtOrderingCustomer_SwiftCode.Visible = false;

            txtOrderingCustomer_Name.Visible = true;
            lblOrderingCustomer_Addr1.Visible = true;
            lblOrderingCustomer_Addr2.Visible = true;
            lblOrderingCustomer_Addr3.Visible = true;
            txtOrderingCustomer_Addr1.Visible = true;
            txtOrderingCustomer_Addr2.Visible = true;
            txtOrderingCustomer_Addr3.Visible = true;
        }
    }
    protected void ddlBeneficiaryCustomer_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiaryCustomer.SelectedValue == "A")
        {
            lblBeneficiaryCustomerSwiftCode.Text = "Swift Code: ";
            txtBeneficiaryCustomerSwiftCode.Visible = true;

            txtBeneficiaryCustomerName.Visible = false;
            lblBeneficiaryCustomerAddr1.Visible = false;
            lblBeneficiaryCustomerAddr2.Visible = false;
            lblBeneficiaryCustomerAddr3.Visible = false;
            txtBeneficiaryCustomerAddr1.Visible = false;
            txtBeneficiaryCustomerAddr2.Visible = false;
            txtBeneficiaryCustomerAddr3.Visible = false;
        }
        else if (ddlBeneficiaryCustomer.SelectedValue == "F")
        {
            lblBeneficiaryCustomerSwiftCode.Text = "Name: ";
            txtBeneficiaryCustomerSwiftCode.Visible = false;

            txtBeneficiaryCustomerName.Visible = true;
            lblBeneficiaryCustomerAddr1.Visible = true;
            lblBeneficiaryCustomerAddr2.Visible = true;
            lblBeneficiaryCustomerAddr3.Visible = true;
            txtBeneficiaryCustomerAddr1.Visible = true;
            txtBeneficiaryCustomerAddr2.Visible = true;
            txtBeneficiaryCustomerAddr3.Visible = true;
        }
        else if (ddlBeneficiaryCustomer.SelectedValue == "N")
        {
            lblBeneficiaryCustomerSwiftCode.Text = "Name: ";
            txtBeneficiaryCustomerSwiftCode.Visible = false;

            txtBeneficiaryCustomerName.Visible = true;
            lblBeneficiaryCustomerAddr1.Visible = true;
            lblBeneficiaryCustomerAddr2.Visible = true;
            lblBeneficiaryCustomerAddr3.Visible = true;
            txtBeneficiaryCustomerAddr1.Visible = true;
            txtBeneficiaryCustomerAddr2.Visible = true;
            txtBeneficiaryCustomerAddr3.Visible = true;
        }
    }

    protected void ddlOrderingInstitution_TextChanged(object sender, EventArgs e)
    {
        if (ddlOrderingInstitution.SelectedValue == "A")
        {
            lblOrderingInstitutionSwiftCode.Text = "Swift Code: ";
            txtOrderingInstitutionSwiftCode.Visible = true;
            txtOrderingInstitutionName.Visible = false;
            lblOrderingInstitutionAddress1.Visible = false;
            lblOrderingInstitutionAddress2.Visible = false;
            lblOrderingInstitutionAddress3.Visible = false;
            txtOrderingInstitutionAddress1.Visible = false;
            txtOrderingInstitutionAddress2.Visible = false;
            txtOrderingInstitutionAddress3.Visible = false;
        }
        else
        {
            if (ddlOrderingInstitution.SelectedValue == "B")
            {
                lblOrderingInstitutionSwiftCode.Text = "Location: ";
                txtOrderingInstitutionSwiftCode.Visible = false;
                txtOrderingInstitutionName.Visible = false;
                lblOrderingInstitutionAddress1.Visible = false;
                lblOrderingInstitutionAddress2.Visible = false;
                lblOrderingInstitutionAddress3.Visible = false;
                txtOrderingInstitutionAddress1.Visible = false;
                txtOrderingInstitutionAddress2.Visible = false;
                txtOrderingInstitutionAddress3.Visible = false;
            }
            else
            {
                lblOrderingInstitutionSwiftCode.Text = "Name: ";
                txtOrderingInstitutionSwiftCode.Visible = false;
                txtOrderingInstitutionName.Visible = true;
                lblOrderingInstitutionAddress1.Visible = true;
                lblOrderingInstitutionAddress2.Visible = true;
                lblOrderingInstitutionAddress3.Visible = true;
                txtOrderingInstitutionAddress1.Visible = true;
                txtOrderingInstitutionAddress2.Visible = true;
                txtOrderingInstitutionAddress3.Visible = true;
            }
        }
    }
    protected void ddlSendersCorrespondent_TextChanged(object sender, EventArgs e)
    {
        if (ddlSendersCorrespondent.SelectedValue == "A")
        {
            lblSendersCorrespondentSwiftCode.Text = "Swift Code: ";
            txtSendersCorrespondentSwiftCode.Visible = true;
            txtSendersCorrespondentName.Visible = false;
            txtSendersCorrespondentLocation.Visible = false;
            lblSendersCorrespondentAddress1.Visible = false;
            lblSendersCorrespondentAddress2.Visible = false;
            lblSendersCorrespondentAddress3.Visible = false;
            txtSendersCorrespondentAddress1.Visible = false;
            txtSendersCorrespondentAddress2.Visible = false;
            txtSendersCorrespondentAddress3.Visible = false;
        }
        else
        {
            if (ddlSendersCorrespondent.SelectedValue == "B")
            {
                lblSendersCorrespondentSwiftCode.Text = "Location: ";
                txtSendersCorrespondentSwiftCode.Visible = false;
                txtSendersCorrespondentName.Visible = false;
                txtSendersCorrespondentLocation.Visible = true;
                lblSendersCorrespondentAddress1.Visible = false;
                lblSendersCorrespondentAddress2.Visible = false;
                lblSendersCorrespondentAddress3.Visible = false;
                txtSendersCorrespondentAddress1.Visible = false;
                txtSendersCorrespondentAddress2.Visible = false;
                txtSendersCorrespondentAddress3.Visible = false;
            }
            else
            {
                lblSendersCorrespondentSwiftCode.Text = "Name: ";
                txtSendersCorrespondentSwiftCode.Visible = false;
                txtSendersCorrespondentName.Visible = true;
                txtSendersCorrespondentLocation.Visible = false;
                lblSendersCorrespondentAddress1.Visible = true;
                lblSendersCorrespondentAddress2.Visible = true;
                lblSendersCorrespondentAddress3.Visible = true;
                txtSendersCorrespondentAddress1.Visible = true;
                txtSendersCorrespondentAddress2.Visible = true;
                txtSendersCorrespondentAddress3.Visible = true;
            }
        }
    }
    protected void ddlReceiversCorrespondent_TextChanged(object sender, EventArgs e)
    {
        if (ddlReceiversCorrespondent.SelectedValue == "A")
        {
            lblReceiversCorrespondentSwiftCode.Text = "Swift Code: ";
            txtReceiversCorrespondentSwiftCode.Visible = true;
            txtReceiversCorrespondentName.Visible = false;
            txtReceiversCorrespondentLocation.Visible = false;
            lblReceiversCorrespondentAddress1.Visible = false;
            lblReceiversCorrespondentAddress2.Visible = false;
            lblReceiversCorrespondentAddress3.Visible = false;
            txtReceiversCorrespondentAddress1.Visible = false;
            txtReceiversCorrespondentAddress2.Visible = false;
            txtReceiversCorrespondentAddress3.Visible = false;
        }
        else
        {
            if (ddlReceiversCorrespondent.SelectedValue == "B")
            {
                lblReceiversCorrespondentSwiftCode.Text = "Location: ";
                txtReceiversCorrespondentSwiftCode.Visible = false;
                txtReceiversCorrespondentName.Visible = false;
                txtReceiversCorrespondentLocation.Visible = true;
                lblReceiversCorrespondentAddress1.Visible = false;
                lblReceiversCorrespondentAddress2.Visible = false;
                lblReceiversCorrespondentAddress3.Visible = false;
                txtReceiversCorrespondentAddress1.Visible = false;
                txtReceiversCorrespondentAddress2.Visible = false;
                txtReceiversCorrespondentAddress3.Visible = false;
            }
            else
            {
                lblReceiversCorrespondentSwiftCode.Text = "Name: ";
                txtReceiversCorrespondentSwiftCode.Visible = false;
                txtReceiversCorrespondentName.Visible = true;
                txtReceiversCorrespondentLocation.Visible = false;
                lblReceiversCorrespondentAddress1.Visible = true;
                lblReceiversCorrespondentAddress2.Visible = true;
                lblReceiversCorrespondentAddress3.Visible = true;
                txtReceiversCorrespondentAddress1.Visible = true;
                txtReceiversCorrespondentAddress2.Visible = true;
                txtReceiversCorrespondentAddress3.Visible = true;
            }
        }
    }
    protected void ddlThirdReimbursementInstitution_TextChanged(object sender, EventArgs e)
    {
        if (ddlThirdReimbursementInstitution.SelectedValue == "A")
        {
            lblThirdReimbursementInstitutionSwiftCode.Text = "Swift Code: ";
            txtThirdReimbursementInstitutionSwiftCode.Visible = true;
            txtThirdReimbursementInstitutionName.Visible = false;
            txtThirdReimbursementInstitutionLocation.Visible = false;
            lblThirdReimbursementInstitutionAddress1.Visible = false;
            lblThirdReimbursementInstitutionAddress2.Visible = false;
            lblThirdReimbursementInstitutionAddress3.Visible = false;
            txtThirdReimbursementInstitutionAddress1.Visible = false;
            txtThirdReimbursementInstitutionAddress2.Visible = false;
            txtThirdReimbursementInstitutionAddress3.Visible = false;
        }
        else
        {
            if (ddlThirdReimbursementInstitution.SelectedValue == "B")
            {
                lblThirdReimbursementInstitutionSwiftCode.Text = "Location: ";
                txtThirdReimbursementInstitutionSwiftCode.Visible = false;
                txtThirdReimbursementInstitutionName.Visible = false;
                txtThirdReimbursementInstitutionLocation.Visible = true;
                lblThirdReimbursementInstitutionAddress1.Visible = false;
                lblThirdReimbursementInstitutionAddress2.Visible = false;
                lblThirdReimbursementInstitutionAddress3.Visible = false;
                txtThirdReimbursementInstitutionAddress1.Visible = false;
                txtThirdReimbursementInstitutionAddress2.Visible = false;
                txtThirdReimbursementInstitutionAddress3.Visible = false;
            }
            else
            {
                lblThirdReimbursementInstitutionSwiftCode.Text = "Name: ";
                txtThirdReimbursementInstitutionSwiftCode.Visible = false;
                txtThirdReimbursementInstitutionName.Visible = true;
                txtThirdReimbursementInstitutionLocation.Visible = false;
                lblThirdReimbursementInstitutionAddress1.Visible = true;
                lblThirdReimbursementInstitutionAddress2.Visible = true;
                lblThirdReimbursementInstitutionAddress3.Visible = true;
                txtThirdReimbursementInstitutionAddress1.Visible = true;
                txtThirdReimbursementInstitutionAddress2.Visible = true;
                txtThirdReimbursementInstitutionAddress3.Visible = true;
            }
        }
    }
    protected void ddlOrderingInstitution202_TextChanged(object sender, EventArgs e)
    {
        if (ddlOrderingInstitution202.SelectedValue == "A")
        {
            lblOrderingInstitution202SwiftCode.Text = "Swift Code: ";
            txtOrderingInstitution202SwiftCode.Visible = true;
            txtOrderingInstitution202Name.Visible = false;
            lblOrderingInstitution202Address1.Visible = false;
            lblOrderingInstitution202Address2.Visible = false;
            lblOrderingInstitution202Address3.Visible = false;
            txtOrderingInstitution202Address1.Visible = false;
            txtOrderingInstitution202Address2.Visible = false;
            txtOrderingInstitution202Address3.Visible = false;
        }
        else
        {
            lblOrderingInstitution202SwiftCode.Text = "Name: ";
            txtOrderingInstitution202SwiftCode.Visible = false;
            txtOrderingInstitution202Name.Visible = true;
            lblOrderingInstitution202Address1.Visible = true;
            lblOrderingInstitution202Address2.Visible = true;
            lblOrderingInstitution202Address3.Visible = true;
            txtOrderingInstitution202Address1.Visible = true;
            txtOrderingInstitution202Address2.Visible = true;
            txtOrderingInstitution202Address3.Visible = true;
        }
    }
    protected void ddlSendersCorrespondent202_TextChanged(object sender, EventArgs e)
    {
        if (ddlSendersCorrespondent202.SelectedValue == "A")
        {
            lblSendersCorrespondent202SwiftCode.Text = "Swift Code: ";
            txtSendersCorrespondent202SwiftCode.Visible = true;
            txtSendersCorrespondent202Name.Visible = false;
            txtSendersCorrespondent202Location.Visible = false;
            lblSendersCorrespondent202Address1.Visible = false;
            lblSendersCorrespondent202Address2.Visible = false;
            lblSendersCorrespondent202Address3.Visible = false;
            txtSendersCorrespondent202Address1.Visible = false;
            txtSendersCorrespondent202Address2.Visible = false;
            txtSendersCorrespondent202Address3.Visible = false;
        }
        else
        {
            if (ddlSendersCorrespondent202.SelectedValue == "B")
            {
                lblSendersCorrespondent202SwiftCode.Text = "Location: ";
                txtSendersCorrespondent202SwiftCode.Visible = false;
                txtSendersCorrespondent202Name.Visible = false;
                txtSendersCorrespondent202Location.Visible = true;
                lblSendersCorrespondent202Address1.Visible = false;
                lblSendersCorrespondent202Address2.Visible = false;
                lblSendersCorrespondent202Address3.Visible = false;
                txtSendersCorrespondent202Address1.Visible = false;
                txtSendersCorrespondent202Address2.Visible = false;
                txtSendersCorrespondent202Address3.Visible = false;
            }
            else
            {
                lblSendersCorrespondent202SwiftCode.Text = "Name: ";
                txtSendersCorrespondent202SwiftCode.Visible = false;
                txtSendersCorrespondent202Name.Visible = true;
                txtSendersCorrespondent202Location.Visible = false;
                lblSendersCorrespondent202Address1.Visible = true;
                lblSendersCorrespondent202Address2.Visible = true;
                lblSendersCorrespondent202Address3.Visible = true;
                txtSendersCorrespondent202Address1.Visible = true;
                txtSendersCorrespondent202Address2.Visible = true;
                txtSendersCorrespondent202Address3.Visible = true;
            }
        }
    }
    protected void ddlReceiversCorrespondent202_TextChanged(object sender, EventArgs e)
    {
        if (ddlReceiversCorrespondent202.SelectedValue == "A")
        {
            lblReceiversCorrespondent202SwiftCode.Text = "Swift Code: ";
            txtReceiversCorrespondent202SwiftCode.Visible = true;
            txtReceiversCorrespondent202Name.Visible = false;
            txtReceiversCorrespondent202Location.Visible = false;
            lblReceiversCorrespondent202Address1.Visible = false;
            lblReceiversCorrespondent202Address2.Visible = false;
            lblReceiversCorrespondent202Address3.Visible = false;
            txtReceiversCorrespondent202Address1.Visible = false;
            txtReceiversCorrespondent202Address2.Visible = false;
            txtReceiversCorrespondent202Address3.Visible = false;
        }
        else
        {
            if (ddlReceiversCorrespondent202.SelectedValue == "B")
            {
                lblReceiversCorrespondent202SwiftCode.Text = "Location: ";
                txtReceiversCorrespondent202SwiftCode.Visible = false;
                txtReceiversCorrespondent202Name.Visible = false;
                txtReceiversCorrespondent202Location.Visible = true;
                lblReceiversCorrespondent202Address1.Visible = false;
                lblReceiversCorrespondent202Address2.Visible = false;
                lblReceiversCorrespondent202Address3.Visible = false;
                txtReceiversCorrespondent202Address1.Visible = false;
                txtReceiversCorrespondent202Address2.Visible = false;
                txtReceiversCorrespondent202Address3.Visible = false;
            }
            else
            {
                lblReceiversCorrespondent202SwiftCode.Text = "Name: ";
                txtReceiversCorrespondent202SwiftCode.Visible = false;
                txtReceiversCorrespondent202Name.Visible = true;
                txtReceiversCorrespondent202Location.Visible = false;
                lblReceiversCorrespondent202Address1.Visible = true;
                lblReceiversCorrespondent202Address2.Visible = true;
                lblReceiversCorrespondent202Address3.Visible = true;
                txtReceiversCorrespondent202Address1.Visible = true;
                txtReceiversCorrespondent202Address2.Visible = true;
                txtReceiversCorrespondent202Address3.Visible = true;
            }
        }
    }
    protected void ddl200Intermediary_TextChanged(object sender, EventArgs e)
    {
        if (ddl200Intermediary.SelectedValue == "A")
        {
            lbl200IntermediarySwiftCode.Text = "Swift Code: ";
            txt200IntermediarySwiftCode.Visible = true;
            txt200IntermediaryName.Visible = false;
            lbl200IntermediaryAddress1.Visible = false;
            lbl200IntermediaryAddress2.Visible = false;
            lbl200IntermediaryAddress3.Visible = false;
            txt200IntermediaryAddress1.Visible = false;
            txt200IntermediaryAddress2.Visible = false;
            txt200IntermediaryAddress3.Visible = false;
        }
        else
        {
            lbl200IntermediarySwiftCode.Text = "Name: ";
            txt200IntermediarySwiftCode.Visible = false;
            txt200IntermediaryName.Visible = true;
            lbl200IntermediaryAddress1.Visible = true;
            lbl200IntermediaryAddress2.Visible = true;
            lbl200IntermediaryAddress3.Visible = true;
            txt200IntermediaryAddress1.Visible = true;
            txt200IntermediaryAddress2.Visible = true;
            txt200IntermediaryAddress3.Visible = true;
        }
    }
    protected void ddl200AccWithInstitution_TextChanged(object sender, EventArgs e)
    {
        if (ddl200AccWithInstitution.SelectedValue == "A")
        {
            lbl200AccWithInstitutionSwiftCode.Text = "Swift Code: ";
            txt200AccWithInstitutionSwiftCode.Visible = true;
            txt200AccWithInstitutionLocation.Visible = false;
            txt200AccWithInstitutionName.Visible = false;
            lbl200AccWithInstitutionAddress1.Visible = false;
            lbl200AccWithInstitutionAddress2.Visible = false;
            lbl200AccWithInstitutionAddress3.Visible = false;
            txt200AccWithInstitutionAddress1.Visible = false;
            txt200AccWithInstitutionAddress2.Visible = false;
            txt200AccWithInstitutionAddress3.Visible = false;
        }
        else
        {
            if (ddl200AccWithInstitution.SelectedValue == "B")
            {
                lbl200AccWithInstitutionSwiftCode.Text = "Location: ";
                txt200AccWithInstitutionSwiftCode.Visible = false;
                txt200AccWithInstitutionLocation.Visible = true;
                txt200AccWithInstitutionName.Visible = false;
                lbl200AccWithInstitutionAddress1.Visible = false;
                lbl200AccWithInstitutionAddress2.Visible = false;
                lbl200AccWithInstitutionAddress3.Visible = false;
                txt200AccWithInstitutionAddress1.Visible = false;
                txt200AccWithInstitutionAddress2.Visible = false;
                txt200AccWithInstitutionAddress3.Visible = false;
            }
            else
            {
                lbl200AccWithInstitutionSwiftCode.Text = "Name: ";
                txt200AccWithInstitutionSwiftCode.Visible = false;
                txt200AccWithInstitutionLocation.Visible = false;
                txt200AccWithInstitutionName.Visible = true;
                lbl200AccWithInstitutionAddress1.Visible = true;
                lbl200AccWithInstitutionAddress2.Visible = true;
                lbl200AccWithInstitutionAddress3.Visible = true;
                txt200AccWithInstitutionAddress1.Visible = true;
                txt200AccWithInstitutionAddress2.Visible = true;
                txt200AccWithInstitutionAddress3.Visible = true;
            }
        }
    }
    //MT 202 changes 02122019
    protected void ddlIntermediary202_TextChanged(object sender, EventArgs e)
    {
        if (ddlIntermediary202.SelectedValue == "A")
        {
            lblIntermediary202SwiftCode.Text = "Swift Code: ";
            txtIntermediary202SwiftCode.Visible = true;
            txtIntermediary202Name.Visible = false;
            lblIntermediary202Address1.Visible = false;
            lblIntermediary202Address2.Visible = false;
            lblIntermediary202Address3.Visible = false;
            txtIntermediary202Address1.Visible = false;
            txtIntermediary202Address2.Visible = false;
            txtIntermediary202Address3.Visible = false;
        }
        else
        {
            lblIntermediary202SwiftCode.Text = "Name: ";
            txtIntermediary202SwiftCode.Visible = false;
            txtIntermediary202Name.Visible = true;
            lblIntermediary202Address1.Visible = true;
            lblIntermediary202Address2.Visible = true;
            lblIntermediary202Address3.Visible = true;
            txtIntermediary202Address1.Visible = true;
            txtIntermediary202Address2.Visible = true;
            txtIntermediary202Address3.Visible = true;
        }
    }
    protected void ddlAccountWithInstitution202_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountWithInstitution202.SelectedValue == "A")
        {
            lblAccountWithInstitution202SwiftCode.Text = "Swift Code: ";
            txtAccountWithInstitution202SwiftCode.Visible = true;
            txtAccountWithInstitution202Name.Visible = false;
            txtAccountWithInstitution202Location.Visible = false;
            lblAccountWithInstitution202Address1.Visible = false;
            lblAccountWithInstitution202Address2.Visible = false;
            lblAccountWithInstitution202Address3.Visible = false;
            txtAccountWithInstitution202Address1.Visible = false;
            txtAccountWithInstitution202Address2.Visible = false;
            txtAccountWithInstitution202Address3.Visible = false;
        }
        else
        {
            if (ddlAccountWithInstitution202.SelectedValue == "B")
            {
                lblAccountWithInstitution202SwiftCode.Text = "Location: ";
                txtAccountWithInstitution202SwiftCode.Visible = false;
                txtAccountWithInstitution202Name.Visible = false;
                txtAccountWithInstitution202Location.Visible = true;
                lblAccountWithInstitution202Address1.Visible = false;
                lblAccountWithInstitution202Address2.Visible = false;
                lblAccountWithInstitution202Address3.Visible = false;
                txtAccountWithInstitution202Address1.Visible = false;
                txtAccountWithInstitution202Address2.Visible = false;
                txtAccountWithInstitution202Address3.Visible = false;
            }
            else
            {
                lblAccountWithInstitution202SwiftCode.Text = "Name: ";
                txtAccountWithInstitution202SwiftCode.Visible = false;
                txtAccountWithInstitution202Name.Visible = true;
                txtAccountWithInstitution202Location.Visible = false;
                lblAccountWithInstitution202Address1.Visible = true;
                lblAccountWithInstitution202Address2.Visible = true;
                lblAccountWithInstitution202Address3.Visible = true;
                txtAccountWithInstitution202Address1.Visible = true;
                txtAccountWithInstitution202Address2.Visible = true;
                txtAccountWithInstitution202Address3.Visible = true;
            }
        }
    }
    protected void ddlBeneficiaryInstitution202_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiaryInstitution202.SelectedValue == "A")
        {
            lblBeneficiaryInstitution202SwiftCode.Text = "Swift Code: ";
            txtBeneficiaryInstitution202SwiftCode.Visible = true;
            txtBeneficiaryInstitution202Name.Visible = false;
            lblBeneficiaryInstitution202Address1.Visible = false;
            lblBeneficiaryInstitution202Address2.Visible = false;
            lblBeneficiaryInstitution202Address3.Visible = false;
            txtBeneficiaryInstitution202Address1.Visible = false;
            txtBeneficiaryInstitution202Address2.Visible = false;
            txtBeneficiaryInstitution202Address3.Visible = false;
        }
        else
        {
            lblBeneficiaryInstitution202SwiftCode.Text = "Name: ";
            txtBeneficiaryInstitution202SwiftCode.Visible = false;
            txtBeneficiaryInstitution202Name.Visible = true;
            lblBeneficiaryInstitution202Address1.Visible = true;
            lblBeneficiaryInstitution202Address2.Visible = true;
            lblBeneficiaryInstitution202Address3.Visible = true;
            txtBeneficiaryInstitution202Address1.Visible = true;
            txtBeneficiaryInstitution202Address2.Visible = true;
            txtBeneficiaryInstitution202Address3.Visible = true;
        }
    }
    //MT 103 changes 02122019
    protected void ddlIntermediary103_TextChanged(object sender, EventArgs e)
    {
        if (ddlIntermediary103.SelectedValue == "A")
        {
            lblIntermediary103SwiftCode.Text = "Swift Code: ";
            lblIntermediary103SwiftCode.Visible = true;
            txtIntermediary103SwiftCode.Visible = true;
            txtIntermediary103Name.Visible = false;
            lblIntermediary103Address1.Visible = false;
            lblIntermediary103Address2.Visible = false;
            lblIntermediary103Address3.Visible = false;
            txtIntermediary103Address1.Visible = false;
            txtIntermediary103Address2.Visible = false;
            txtIntermediary103Address3.Visible = false;
        }
        else
        {
            if (ddlIntermediary103.SelectedValue == "C")
            {
                lblIntermediary103SwiftCode.Text = "";
                lblIntermediary103SwiftCode.Visible = false;
                txtIntermediary103SwiftCode.Visible = false;
                txtIntermediary103Name.Visible = false;
                lblIntermediary103Address1.Visible = false;
                lblIntermediary103Address2.Visible = false;
                lblIntermediary103Address3.Visible = false;
                txtIntermediary103Address1.Visible = false;
                txtIntermediary103Address2.Visible = false;
                txtIntermediary103Address3.Visible = false;
            }
            else
            {
                lblIntermediary103SwiftCode.Text = "Name: ";
                lblIntermediary103SwiftCode.Visible = true;
                txtIntermediary103SwiftCode.Visible = false;
                txtIntermediary103Name.Visible = true;
                lblIntermediary103Address1.Visible = true;
                lblIntermediary103Address2.Visible = true;
                lblIntermediary103Address3.Visible = true;
                txtIntermediary103Address1.Visible = true;
                txtIntermediary103Address2.Visible = true;
                txtIntermediary103Address3.Visible = true;
            }
        }
    }
    protected void ddlAccountWithInstitution103_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountWithInstitution103.SelectedValue == "A")
        {
            lblAccountWithInstitution103SwiftCode.Text = "Swift Code: ";
            lblAccountWithInstitution103SwiftCode.Visible = true;
            txtAccountWithInstitution103SwiftCode.Visible = true;
            txtAccountWithInstitution103Name.Visible = false;
            txtAccountWithInstitution103Location.Visible = false;
            lblAccountWithInstitution103Address1.Visible = false;
            lblAccountWithInstitution103Address2.Visible = false;
            lblAccountWithInstitution103Address3.Visible = false;
            txtAccountWithInstitution103Address1.Visible = false;
            txtAccountWithInstitution103Address2.Visible = false;
            txtAccountWithInstitution103Address3.Visible = false;
        }
        else
        {
            if (ddlAccountWithInstitution103.SelectedValue == "B")
            {
                lblAccountWithInstitution103SwiftCode.Text = "Location: ";
                lblAccountWithInstitution103SwiftCode.Visible = true;
                txtAccountWithInstitution103SwiftCode.Visible = false;
                txtAccountWithInstitution103Name.Visible = false;
                txtAccountWithInstitution103Location.Visible = true;
                lblAccountWithInstitution103Address1.Visible = false;
                lblAccountWithInstitution103Address2.Visible = false;
                lblAccountWithInstitution103Address3.Visible = false;
                txtAccountWithInstitution103Address1.Visible = false;
                txtAccountWithInstitution103Address2.Visible = false;
                txtAccountWithInstitution103Address3.Visible = false;
            }
            else
            {
                if (ddlAccountWithInstitution103.SelectedValue == "C")
                {
                    lblAccountWithInstitution103SwiftCode.Text = "";
                    lblAccountWithInstitution103SwiftCode.Visible = false;
                    txtAccountWithInstitution103SwiftCode.Visible = false;
                    txtAccountWithInstitution103Name.Visible = false;
                    txtAccountWithInstitution103Location.Visible = false;
                    lblAccountWithInstitution103Address1.Visible = false;
                    lblAccountWithInstitution103Address2.Visible = false;
                    lblAccountWithInstitution103Address3.Visible = false;
                    txtAccountWithInstitution103Address1.Visible = false;
                    txtAccountWithInstitution103Address2.Visible = false;
                    txtAccountWithInstitution103Address3.Visible = false;
                }
                else
                {
                    lblAccountWithInstitution103SwiftCode.Text = "Name: ";
                    lblAccountWithInstitution103SwiftCode.Visible = true;
                    txtAccountWithInstitution103SwiftCode.Visible = false;
                    txtAccountWithInstitution103Name.Visible = true;
                    txtAccountWithInstitution103Location.Visible = false;
                    lblAccountWithInstitution103Address1.Visible = true;
                    lblAccountWithInstitution103Address2.Visible = true;
                    lblAccountWithInstitution103Address3.Visible = true;
                    txtAccountWithInstitution103Address1.Visible = true;
                    txtAccountWithInstitution103Address2.Visible = true;
                    txtAccountWithInstitution103Address3.Visible = true;
                }
            }
        }
    }
    ///////GO and import accounting 12/12/2019
    protected void chk_IMPACC1Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC1Flag.Checked == false)
        {
            PanelIMPACC1.Visible = false;
        }
        else if (chk_IMPACC1Flag.Checked == true)
        {
            PanelIMPACC1.Visible = true;
        }
    }
    protected void chk_IMPACC2Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC2Flag.Checked == false)
        {
            PanelIMPACC2.Visible = false;
        }
        else if (chk_IMPACC2Flag.Checked == true)
        {
            PanelIMPACC2.Visible = true;
        }
    }
    protected void chk_IMPACC3Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC3Flag.Checked == false)
        {
            PanelIMPACC3.Visible = false;
        }
        else if (chk_IMPACC3Flag.Checked == true)
        {
            PanelIMPACC3.Visible = true;
        }
    }
    protected void chk_IMPACC4Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC4Flag.Checked == false)
        {
            PanelIMPACC4.Visible = false;
        }
        else if (chk_IMPACC4Flag.Checked == true)
        {
            PanelIMPACC4.Visible = true;
        }
    }
    protected void chk_IMPACC5Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC5Flag.Checked == false)
        {
            PanelIMPACC5.Visible = false;
        }
        else if (chk_IMPACC5Flag.Checked == true)
        {
            PanelIMPACC5.Visible = true;
        }
    }
    protected void chk_GO1Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_GO1Flag.Checked == false)
        {
            Panel_GO1Left.Visible = false;
            Panel_GO1Right.Visible = false;
        }
        else if (chk_GO1Flag.Checked == true)
        {
            Panel_GO1Left.Visible = true;
            Panel_GO1Right.Visible = true;
        }
    }
    protected void chk_GO2Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_GO2Flag.Checked == false)
        {
            Panel_GO2Left.Visible = false;
            Panel_GO2Right.Visible = false;
        }
        else if (chk_GO2Flag.Checked == true)
        {
            Panel_GO2Left.Visible = true;
            Panel_GO2Right.Visible = true;
        }
    }
    protected void chk_GO3Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_GO3Flag.Checked == false)
        {
            Panel_GO3Left.Visible = false;
            Panel_GO3Right.Visible = false;
        }
        else if (chk_GO3Flag.Checked == true)
        {
            Panel_GO3Left.Visible = true;
            Panel_GO3Right.Visible = true;
        }
    }
    protected void chk_GOAcccChangeFlag_Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_GOAcccChangeFlag.Checked == false)
        {
            panal_GOAccChange.Visible = false;
        }
        else if (chk_GOAcccChangeFlag.Checked == true)
        {
            panal_GOAccChange.Visible = true;
        }
    }
    public void CreateSwiftSFMSFile()
    {
        string FileType;
        DeleteDataForSwiftSTP();
        if (hdnNegoRemiBankType.Value == "FOREIGN")
        {
            FileType = "SWIFT";

            if (rdb_swift_103.Checked)
            {
                //CreateSwift103(FileType);
                CreateSwift103_XML(FileType);
                InsertDataForSwiftSTP("MT103");
            }
            if (rdb_swift_202.Checked)
            {
                //CreateSwift202(FileType);
                CreateSwift202_XML(FileType);
                InsertDataForSwiftSTP("MT202");
            }
            if (rdb_swift_200.Checked)
            {
                //CreateSwift200(FileType);
                CreateSwift200_XML(FileType);
                InsertDataForSwiftSTP("MT200");
            }
            if (rdb_swift_R42.Checked)
            {
                CreateSwiftR42("SWIFT");
            }
            if (rdb_swift_754.Checked)
            {
                //CreateSwift754(FileType);
                CreateSwift754_XML(FileType);
                InsertDataForSwiftSTP("MT754");
            }
        }
        else if (hdnNegoRemiBankType.Value == "LOCAL")
        {
            FileType = "SFMS";

            if (rdb_swift_103.Checked)
            {
                CreateSwift103(FileType);
            }
            if (rdb_swift_202.Checked)
            {
                CreateSwift202(FileType);
            }
            if (rdb_swift_200.Checked)
            {
                CreateSwift200(FileType);
            }
            if (rdb_swift_R42.Checked)
            {
                CreateSwiftR42("SWIFT");
            }
            if (rdb_swift_754.Checked)
            {
                CreateSwift754(FileType);
            }
        }
    }
    public void GBaseFile()
    {
        if (hdnLedgerStatusCode.Value.ToUpper() != "S")
        {
            GBaseFileCreation();
        }
        if (chk_GO1Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation1();
        }
        if (chk_GO2Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation2();
        }
        if (chk_GO3Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation3();
        }
        if (chk_GOAcccChangeFlag.Checked == true)
        {
            GBaseFileCreationGeneralOperation4();
        }
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        string Query = "TF_IMP_GBaseFileCreation_Settlement";
        if (hdnDocType.Value != "ACC")
        {
            Query = "TF_IMP_GbaseFileCreation_Settlement_Collection";
        }
        DataTable dt = objData1.getData(Query, PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/GBASE/" + MTodayDate + "/");
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

            }
        }
        else
        {
        }
    }
    public void GBaseFileCreationGeneralOperation1()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Settlement_GO1", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_GO1" + ".xlsx";
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
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Settlement_GO2", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_GO2" + ".xlsx";
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
    public void GBaseFileCreationGeneralOperation3()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Settlement_GO3", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_GO3" + ".xlsx";
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
    public void GBaseFileCreationGeneralOperation4()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Settlement_GO4", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_GOBR" + ".xlsx";
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
    public void CreateSwift103(string FileType)
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SWIFT/MT103/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT103_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SFMS/SFMS103/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT103_FileCreation";
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
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT103.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN103.xlsx";
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
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();

                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B2"].Value = "Sender's Reference";
                    ws.Cells["C2"].Value = dt.Rows[0]["SendersReference"].ToString();

                    ws.Cells["A3"].Value = "[13C]";
                    ws.Cells["B3"].Value = "Time Indication";
                    ws.Cells["C3"].Value = dt.Rows[0]["TimeIndication"].ToString();

                    ws.Cells["A4"].Value = "[23B]";
                    ws.Cells["B4"].Value = "Bank Operation Code";
                    ws.Cells["C4"].Value = dt.Rows[0]["BankOperationCode"].ToString();

                    ws.Cells["A5"].Value = "[23E]";
                    ws.Cells["B5"].Value = "Instruction Code";
                    ws.Cells["C5"].Value = dt.Rows[0]["InstructionCode"].ToString();

                    ws.Cells["A6"].Value = "[26T]";
                    ws.Cells["B6"].Value = "Transaction Type Code";
                    ws.Cells["C6"].Value = dt.Rows[0]["TransactionTypeCode"].ToString();

                    ws.Cells["A7"].Value = "[32A]";
                    ws.Cells["B7"].Value = "Value Date/Currency/Interbank Settled Amount";
                    ws.Cells["C7"].Value = dt.Rows[0]["ValueDate"].ToString();
                    ws.Cells["D7"].Value = dt.Rows[0]["Currency"].ToString();
                    ws.Cells["E7"].Value = dt.Rows[0]["InterbankSettledAmount"].ToString();

                    ws.Cells["A8"].Value = "[33B]";
                    ws.Cells["B8"].Value = "Currency/Instructed Amount";
                    ws.Cells["C8"].Value = dt.Rows[0]["Currency33B"].ToString();
                    ws.Cells["D8"].Value = dt.Rows[0]["InstructedAmount"].ToString();

                    ws.Cells["A9"].Value = "[36]";
                    ws.Cells["B9"].Value = "Exchange Rate";
                    ws.Cells["C9"].Value = dt.Rows[0]["ExchangeRate"].ToString();

                    ws.Cells["A10"].Value = dt.Rows[0]["OrderingCustomer_header"].ToString();
                    ws.Cells["B10"].Value = "Ordering Customer";
                    ws.Cells["C10"].Value = dt.Rows[0]["OrderingCustomerACno"].ToString();
                    int _Ecol = 11;
                    if (dt.Rows[0]["OrderingCustomer_SwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingCustomer_SwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingCustomerName"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingCustomerName"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingCustomerAddress"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingCustomerAddress"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingCustomerAddress2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingCustomerAddress2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingCustomerAddress3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingCustomerAddress3"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[51A]";
                    ws.Cells["B" + _Ecol].Value = "Sending Institution";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendingInstitution1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["SendingInstitution2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendingInstitution2"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["OrderingInstitutionHeader"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Ordering Institution";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["OrderingInstitution2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution6"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["SendersCorrespondentHeader"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Sender's Correspondent";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["SenderCorrespondent2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent6"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent7"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent7"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["ReceiversCorrespondentHeader"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Receiver's Correspondent";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["ReceiverCorrespondent2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent6"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent7"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent7"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitutionHeader"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Third Reimbursement Institution";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitution1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["ThirdReimbursementInstitution2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitution2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitution3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitution4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitution5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitution6"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution7"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ThirdReimbursementInstitution7"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["IntermediaryInstitutionHeader"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Intermediary Institution";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary103AccountNumber"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["Intermediary103SwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary103SwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary103Name"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary103Name"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary103Address1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary103Address1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary103Address2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary103Address2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary103Address3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary103Address3"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["AccountWithInstitutionHeader"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Account With Institution";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["AccountWithInstitution103SwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution103SwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Name"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution103Name"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Location"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution103Location"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Address1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution103Address1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Address2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution103Address2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Address3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution103Address3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["BeneficiaryCustomer_header"].ToString() == "N")
                    {
                        ws.Cells["A" + _Ecol].Value = "[59]";
                    }
                    else
                    {
                        ws.Cells["A" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomer_header"].ToString();
                    }
                    ws.Cells["B" + _Ecol].Value = "Beneficiary Customer";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomerAcc"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["BeneficiaryCustomerSwift"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomerSwift"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["BeneficiaryCustomer_Name"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomer_Name"].ToString();
                        _Ecol++;
                    } if (dt.Rows[0]["BeneficiaryCustomer_Addr1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomer_Addr1"].ToString();
                        _Ecol++;
                    } if (dt.Rows[0]["BeneficiaryCustomer_Addr2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomer_Addr2"].ToString();
                        _Ecol++;
                    } if (dt.Rows[0]["BeneficiaryCustomer_Addr3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomer_Addr3"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[70]";
                    ws.Cells["B" + _Ecol].Value = "Remittance Information";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["RemittanceInformation1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["RemittanceInformation2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["RemittanceInformation2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["RemittanceInformation3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["RemittanceInformation3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["RemittanceInformation4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["RemittanceInformation4"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[71A]";
                    ws.Cells["B" + _Ecol].Value = "Details of Charges";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["DetailsofCharges"].ToString();
                    _Ecol++;
                    ws.Cells["A" + _Ecol].Value = "[71F]";
                    ws.Cells["B" + _Ecol].Value = "Sender's Charges";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCharges1"].ToString();
                    ws.Cells["D" + _Ecol].Value = dt.Rows[0]["SenderCharges2"].ToString();
                    _Ecol++;
                    ws.Cells["A" + _Ecol].Value = "[71G]";
                    ws.Cells["B" + _Ecol].Value = "Receiver's Charges";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCharges1"].ToString();
                    ws.Cells["D" + _Ecol].Value = dt.Rows[0]["ReceiverCharges2"].ToString();
                    _Ecol++;
                    ws.Cells["A" + _Ecol].Value = "[71G]";
                    ws.Cells["B" + _Ecol].Value = "Receiver's Charges";
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
                    ws.Cells["A" + _Ecol].Value = "[77B]";
                    ws.Cells["B" + _Ecol].Value = "Regulatory Reporting";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["RegulatoryReporting1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["RegulatoryReporting2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["RegulatoryReporting2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["RegulatoryReporting3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["RegulatoryReporting3"].ToString();
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
    public void CreateSwift202(string FileType)
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SWIFT/MT202/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT202_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SFMS/SFMS202/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT202_FileCreation";
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
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT202.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN202.xlsx";
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
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();

                    ws.Cells["B2"].Value = "Transaction Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["C2"].Value = dt.Rows[0]["SendersReference"].ToString();

                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["C3"].Value = dt.Rows[0]["RelatedReference"].ToString();

                    ws.Cells["B4"].Value = "Time Indication";
                    ws.Cells["A4"].Value = "[13C]";
                    ws.Cells["C4"].Value = dt.Rows[0]["TimeIndication"].ToString();

                    ws.Cells["B5"].Value = "Value Date, Currency Code, Amount";
                    ws.Cells["A5"].Value = "[32A]";
                    ws.Cells["C5"].Value = dt.Rows[0]["ValueDate"].ToString();
                    ws.Cells["D5"].Value = dt.Rows[0]["Currency"].ToString();
                    ws.Cells["E5"].Value = dt.Rows[0]["Amount"].ToString();

                    ws.Cells["B6"].Value = "Ordering Institution";
                    ws.Cells["A6"].Value = dt.Rows[0]["OrderingInstitutionHeader"].ToString();
                    ws.Cells["C6"].Value = dt.Rows[0]["OrderingInstitution1"].ToString();
                    int _Ecol = 7;
                    if (dt.Rows[0]["OrderingInstitution2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["OrderingInstitution6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["OrderingInstitution6"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Sender's Correspondent";
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["SenderCorrespondentHeader"].ToString();
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["SenderCorrespondent2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent6"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SenderCorrespondent7"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorrespondent7"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Receiver's Correspondent";
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondentHeader"].ToString();
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["ReceiverCorrespondent2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent6"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent7"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["ReceiverCorrespondent7"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Intermediary";
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["IntermediaryInstitutionHeader"].ToString();
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary202AccountNumber"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["Intermediary202SwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary202SwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary202Name"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary202Name"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary202Address1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary202Address1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary202Address2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary202Address2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Intermediary202Address3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Intermediary202Address3"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Account With Institution";
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["AccountWithInstitutionHeader"].ToString();
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution202AccountNumber"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["AccountWithInstitution202SwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution202SwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Name"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution202Name"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Location"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution202Location"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Address1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution202Address1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Address2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution202Address2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Address3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccountWithInstitution202Address3"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["B" + _Ecol].Value = "Beneficiary Institution";
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["BeneficiaryCustomerHeader"].ToString();
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryInstitution202AccountNumber"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["BeneficiaryInstitution202SwiftCode"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryInstitution202SwiftCode"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["BeneficiaryInstitution202Name"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryInstitution202Name"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["BeneficiaryInstitution202Address1"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryInstitution202Address1"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["BeneficiaryInstitution202Address2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryInstitution202Address2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["BeneficiaryInstitution202Address3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["BeneficiaryInstitution202Address3"].ToString();
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
                    if (dt.Rows[0]["SendertoReceiverInformation7"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation7"].ToString();
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
    public void CreateSwift200(string FileType)
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SWIFT/MT200/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT200_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SFMS/SFMS200/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT200_FileCreation";
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
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT200.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN200.xlsx";
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
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();

                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B2"].Value = "Transaction Reference Number";
                    ws.Cells["C2"].Value = dt.Rows[0]["TransactionRefNO200"].ToString();

                    ws.Cells["A3"].Value = "[32A]";
                    ws.Cells["B3"].Value = "Value Date, Currency Code, Amount";
                    ws.Cells["C3"].Value = dt.Rows[0]["Date200"].ToString();
                    ws.Cells["D3"].Value = dt.Rows[0]["Currency200"].ToString();
                    ws.Cells["E3"].Value = dt.Rows[0]["Amount200"].ToString();

                    ws.Cells["A4"].Value = "[53B]";
                    ws.Cells["B4"].Value = "Sender's Correspondent";
                    ws.Cells["C4"].Value = dt.Rows[0]["SenderCorreCode200"].ToString();
                    int _Ecol = 5;
                    if (dt.Rows[0]["SenderCorreLocation200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SenderCorreLocation200"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["Intermediary200Header"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Intermediary";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["IntermediaryAccountNumber200"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["IntermediarySwiftCode200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["IntermediarySwiftCode200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["IntermediaryName200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["IntermediaryName200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["IntermediaryAddress1200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["IntermediaryAddress1200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["IntermediaryAddress2200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["IntermediaryAddress2200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["IntermediaryAddress3200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["IntermediaryAddress3200"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = dt.Rows[0]["AccWithInstitution200Header"].ToString();
                    ws.Cells["B" + _Ecol].Value = "Account With Institution";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccWithInstitutionAccountNumber200"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["AccWithInstitutionSwiftCode200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccWithInstitutionSwiftCode200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccWithInstitutionLocation200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccWithInstitutionLocation200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccWithInstitutionName200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccWithInstitutionName200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccWithInstitutionAddress1200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccWithInstitutionAddress1200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccWithInstitutionAddress2200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccWithInstitutionAddress2200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["AccWithInstitutionAddress3200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["AccWithInstitutionAddress3200"].ToString();
                        _Ecol++;
                    }
                    ws.Cells["A" + _Ecol].Value = "[72]";
                    ws.Cells["B" + _Ecol].Value = "Sender to Receiver Information";
                    ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation1200"].ToString();
                    _Ecol++;
                    if (dt.Rows[0]["SendertoReceiverInformation2200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation2200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation3200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation3200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation4200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation4200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation5200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation5200"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation6200"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation6200"].ToString();
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
    public void CreateSwiftR42(string FileType)
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SWIFT/R42/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_R42FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SFMS/R42/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_R42FileCreation";
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
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MTR42.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FINR42.xlsx";
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
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();

                    ws.Cells["A2"].Value = "[2020]";
                    ws.Cells["B2"].Value = "Transaction Reference Number";
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
    public void CreateSwift754(string FileType)
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SWIFT/MT754/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT754_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Settlement/SFMS/SFMS754/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT754_FileCreation";
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
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT754.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN754.xlsx";
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
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["Sender"].ToString();

                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B2"].Value = "Sender's Reference";
                    ws.Cells["C2"].Value = dt.Rows[0]["Sender_Ref_754"].ToString();

                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["C3"].Value = dt.Rows[0]["Related_Ref_754"].ToString();

                    ws.Cells["A4"].Value = "[32" + dt.Rows[0]["Pri_Amt_Paid_754"].ToString() + "]";
                    ws.Cells["B4"].Value = "Principal Amount Paid/Accepted/Negotiated";

                    if (dt.Rows[0]["Pri_Amt_Paid_754"].ToString() == "A")
                    {
                        ws.Cells["C4"].Value = dt.Rows[0]["Pri_AMT_Date_754"].ToString();
                        ws.Cells["D4"].Value = dt.Rows[0]["Pri_AMT_Curr_754"].ToString();
                        ws.Cells["E4"].Value = dt.Rows[0]["Pri_AMT_Amt_754"].ToString();
                    }
                    if (dt.Rows[0]["Pri_Amt_Paid_754"].ToString() == "B")
                    {
                        ws.Cells["C4"].Value = dt.Rows[0]["Pri_AMT_Curr_754"].ToString();
                        ws.Cells["D4"].Value = dt.Rows[0]["Pri_AMT_Amt_754"].ToString();
                    }

                    ws.Cells["A5"].Value = "[33B]";
                    ws.Cells["B5"].Value = "Additional Amounts";
                    ws.Cells["C5"].Value = dt.Rows[0]["Additional_AMT_Curr_754"].ToString();
                    ws.Cells["D5"].Value = dt.Rows[0]["Additional_AMT_Amt_754"].ToString();

                    ws.Cells["A6"].Value = "[71D]";
                    ws.Cells["B6"].Value = "Charges Deducted";
                    int _Ecol = 6;
                    if (dt.Rows[0]["Charges_deducted1_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_deducted1_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_deducted2_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_deducted2_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_deducted3_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_deducted3_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_deducted4_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_deducted4_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_deducted5_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_deducted5_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_deducted6_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_deducted6_754"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[73A]";
                    ws.Cells["B" + _Ecol].Value = "Charges Added";

                    if (dt.Rows[0]["Charges_Added1_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_Added1_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_Added2_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_Added2_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_Added3_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_Added3_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_Added4_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_Added4_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_Added5_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_Added5_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Charges_Added6_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Charges_Added6_754"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[34" + dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString() + "]";
                    ws.Cells["B" + _Ecol].Value = "Total Amount Claimed";

                    if (dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString() == "A")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Ttl_Amt_Clmd_Date_754"].ToString();
                        ws.Cells["D" + _Ecol].Value = dt.Rows[0]["Ttl_Amt_Clmd_Curr_754"].ToString();
                        ws.Cells["E" + _Ecol].Value = dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString() == "B")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Ttl_Amt_Clmd_Curr_754"].ToString();
                        ws.Cells["D" + _Ecol].Value = dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString();
                        _Ecol++;
                    }

                    ws.Cells["A" + _Ecol].Value = "[53" + dt.Rows[0]["Reimbursing_Bank_754"].ToString() + "]";
                    ws.Cells["B" + _Ecol].Value = "Reimbursing Bank";

                    if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "A")
                    {
                        if (dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_IdentCode_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_IdentCode_754"].ToString();
                            _Ecol++;
                        }
                    }
                    if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "B")
                    {
                        if (dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_Lctn_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_Lctn_754"].ToString();
                            _Ecol++;
                        }
                    }
                    if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "D")
                    {
                        if (dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbursing_Bank_Name_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbursing_Bank_Name_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_Addrs1_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_Addrs1_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_Addrs2_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_Addrs2_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Reimbur_Bank_Addrs3_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Reimbur_Bank_Addrs3_754"].ToString();
                            _Ecol++;
                        }
                    }

                    ws.Cells["A" + _Ecol].Value = "[57" + dt.Rows[0]["Account_With_Bank"].ToString() + "]";
                    ws.Cells["B" + _Ecol].Value = "Account With Bank";

                    if (dt.Rows[0]["Account_With_Bank"].ToString() == "A")
                    {
                        if (dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_With_Bank_IdentCode_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_IdentCode_754"].ToString();
                            _Ecol++;
                        }
                    }
                    if (dt.Rows[0]["Account_With_Bank"].ToString() == "B")
                    {
                        if (dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_With_Bank_Loctn_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Loctn_754"].ToString();
                            _Ecol++;
                        }
                    }
                    if (dt.Rows[0]["Account_With_Bank"].ToString() == "D")
                    {
                        if (dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_With_Bank_Name"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Name"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_With_Bank_Addr1_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Addr1_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_With_Bank_Addr2_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Addr2_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Acnt_With_Bank_Addr3_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Acnt_With_Bank_Addr3_754"].ToString();
                            _Ecol++;
                        }
                    }

                    ws.Cells["A" + _Ecol].Value = "[58" + dt.Rows[0]["Banificiary_Bank_754"].ToString() + "]";
                    ws.Cells["B" + _Ecol].Value = "Beneficiary Bank";

                    if (dt.Rows[0]["Banificiary_Bank_754"].ToString() == "A")
                    {
                        if (dt.Rows[0]["Banificiary_AccNo_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_AccNo_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Banificiary_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Banificiary_IdentCode_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_IdentCode_754"].ToString();
                            _Ecol++;
                        }
                    }
                    if (dt.Rows[0]["Banificiary_Bank_754"].ToString() == "D")
                    {
                        if (dt.Rows[0]["Banificiary_AccNo_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_AccNo_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Banificiary_PartyIdent_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_PartyIdent_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Banificiary_Bank_Name_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_Bank_Name_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Banificiary_Addr1_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_Addr1_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Banificiary_Addr2_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_Addr2_754"].ToString();
                            _Ecol++;
                        }
                        if (dt.Rows[0]["Banificiary_Addr3_754"].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Banificiary_Addr3_754"].ToString();
                            _Ecol++;
                        }
                    }

                    ws.Cells["A" + _Ecol].Value = "[72Z]";
                    ws.Cells["B" + _Ecol].Value = "Sender to Receiver Information";
                    if (dt.Rows[0]["SendertoReceiverInformation_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation2_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation2_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation3_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation3_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation4_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation4_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation5_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation5_754"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["SendertoReceiverInformation6_754"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["SendertoReceiverInformation6_754"].ToString();
                        _Ecol++;
                    }

                    int _Tcol = 1;
                    for (int i = 0; i < 35; i++)
                    {
                        if (dt.Rows[0]["Narrative754_" + _Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative754_" + _Tcol].ToString();
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
    //-------------------------------------CREATING XML FILE----------------------------------------------------
    public void CreateSwift103_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT103_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Settlement/MT103/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Settlement/SFMS103/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT103_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN103_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                #region ValueDateCurrencyInterbankSettledAmount
                string ValueDateCurrencyInterbankSettledAmount = dt.Rows[0]["ValueDate"].ToString().Replace("-", "") + dt.Rows[0]["Currency"].ToString();
                if (dt.Rows[0]["InterbankSettledAmount"].ToString() != "")
                {
                    if (dt.Rows[0]["InterbankSettledAmount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["InterbankSettledAmount"].ToString().Contains(".00"))
                        {
                            ValueDateCurrencyInterbankSettledAmount = ValueDateCurrencyInterbankSettledAmount + dt.Rows[0]["InterbankSettledAmount"].ToString().Replace(".00", ",");
                        }
                        else
                        {

                            ValueDateCurrencyInterbankSettledAmount = ValueDateCurrencyInterbankSettledAmount + dt.Rows[0]["InterbankSettledAmount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        ValueDateCurrencyInterbankSettledAmount = ValueDateCurrencyInterbankSettledAmount + dt.Rows[0]["InterbankSettledAmount"].ToString() + ",";
                    }
                }
                #endregion
                #region CurrencyInstructedAmount
                string CurrencyInstructedAmount = dt.Rows[0]["Currency33B"].ToString();
                if (dt.Rows[0]["InstructedAmount"].ToString() != "")
                {
                    if (dt.Rows[0]["InstructedAmount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["InstructedAmount"].ToString().Contains(".00"))
                        {
                            CurrencyInstructedAmount = CurrencyInstructedAmount + dt.Rows[0]["InstructedAmount"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            CurrencyInstructedAmount = CurrencyInstructedAmount + dt.Rows[0]["InstructedAmount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        CurrencyInstructedAmount = CurrencyInstructedAmount + dt.Rows[0]["InstructedAmount"].ToString() + ",";
                    }
                }
                #endregion
                #region exchangerate
                string exchangerate = "";
                if (dt.Rows[0]["ExchangeRate"].ToString() != "")
                {
                    if (dt.Rows[0]["ExchangeRate"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["ExchangeRate"].ToString().Contains(".00"))
                        {
                            exchangerate = exchangerate + dt.Rows[0]["ExchangeRate"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            exchangerate = exchangerate + dt.Rows[0]["ExchangeRate"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        exchangerate = exchangerate + dt.Rows[0]["ExchangeRate"].ToString() + ",";
                    }
                }
                #endregion
                string OrderingCustomer = "";
                string comma = "";
                #region Ordering Customer
                if (dt.Rows[0]["OrderingCustomer_header"].ToString() == "50A")
                {
                    if (dt.Rows[0]["OrderingCustomerACno"].ToString() != "")
                    {
                        OrderingCustomer = dt.Rows[0]["OrderingCustomerACno"].ToString();
                    }
                    if (OrderingCustomer != "" && dt.Rows[0]["OrderingCustomer_SwiftCode"].ToString() != "")
                    {
                        OrderingCustomer = OrderingCustomer + "#" + dt.Rows[0]["OrderingCustomer_SwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["OrderingCustomer_SwiftCode"].ToString() != "")
                        {
                            OrderingCustomer = dt.Rows[0]["OrderingCustomer_SwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["OrderingCustomer_header"].ToString() == "50K" || dt.Rows[0]["OrderingCustomer_header"].ToString() == "50F")
                {
                    OrderingCustomer = dt.Rows[0]["OrderingCustomerACno"].ToString();
                    if (dt.Rows[0]["OrderingCustomerName"].ToString() != "")
                    {
                        OrderingCustomer = OrderingCustomer + "#" + dt.Rows[0]["OrderingCustomerName"].ToString();
                    }
                    if (dt.Rows[0]["OrderingCustomerAddress"].ToString() != "")
                    {
                        OrderingCustomer = OrderingCustomer + "#" + dt.Rows[0]["OrderingCustomerAddress"].ToString();
                    }
                    if (dt.Rows[0]["OrderingCustomerAddress2"].ToString() != "")
                    {
                        OrderingCustomer = OrderingCustomer + "#" + dt.Rows[0]["OrderingCustomerAddress2"].ToString();
                    }
                    if (dt.Rows[0]["OrderingCustomerAddress3"].ToString() != "")
                    {
                        OrderingCustomer = OrderingCustomer + "#" + dt.Rows[0]["OrderingCustomerAddress3"].ToString();
                    }
                }
                if (OrderingCustomer.Length != 0)
                {
                    comma = OrderingCustomer.Trim().Substring(0, 1);
                    if (comma == "#") { OrderingCustomer = OrderingCustomer.Remove(0, 1); }
                    comma = "";
                }

                #endregion

                string SendingInstitution = "";
                if (dt.Rows[0]["SendingInstitution1"].ToString() != "" || dt.Rows[0]["SendingInstitution2"].ToString() != "")
                {
                    SendingInstitution = dt.Rows[0]["SendingInstitution1"].ToString() + "#" + dt.Rows[0]["SendingInstitution2"].ToString();
                }
                string OrderingInstitution = "";
                #region Ordering Institution
                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52A")
                {
                    if (dt.Rows[0]["OrderingInstitution1"].ToString() != "")
                    {
                        OrderingInstitution = dt.Rows[0]["OrderingInstitution1"].ToString();
                    }
                    if (OrderingInstitution != "" && dt.Rows[0]["OrderingInstitution2"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution2"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["OrderingInstitution2"].ToString() != "")
                        {
                            OrderingInstitution = dt.Rows[0]["OrderingInstitution2"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52D")
                {
                    OrderingInstitution = dt.Rows[0]["OrderingInstitution1"].ToString();
                    if (dt.Rows[0]["OrderingInstitution3"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution3"].ToString();
                    }
                    if (dt.Rows[0]["OrderingInstitution4"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution4"].ToString();
                    }
                    if (dt.Rows[0]["OrderingInstitution5"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution5"].ToString();
                    }
                    if (dt.Rows[0]["OrderingInstitution6"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution6"].ToString();
                    }
                }
                if (OrderingInstitution.Length != 0)
                {
                    comma = OrderingInstitution.Trim().Substring(0, 1);
                    if (comma == "#") { OrderingInstitution = OrderingInstitution.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                string SendersCorrespondent = "";
                #region Sender Correspondent
                if (dt.Rows[0]["SendersCorrespondentHeader"].ToString() == "53A")
                {
                    if (dt.Rows[0]["SenderCorrespondent1"].ToString() != "")
                    {
                        SendersCorrespondent = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    }
                    if (SendersCorrespondent != "" && dt.Rows[0]["SenderCorrespondent2"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent2"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["SenderCorrespondent2"].ToString() != "")
                        {
                            SendersCorrespondent = dt.Rows[0]["SenderCorrespondent2"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["SendersCorrespondentHeader"].ToString() == "53B")
                {
                    if (dt.Rows[0]["SenderCorrespondent1"].ToString() != "")
                    {
                        SendersCorrespondent = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    }
                    if (SendersCorrespondent != "" && dt.Rows[0]["SenderCorrespondent3"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent3"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["SenderCorrespondent3"].ToString() != "")
                        {
                            SendersCorrespondent = dt.Rows[0]["SenderCorrespondent3"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["SendersCorrespondentHeader"].ToString() == "53D")
                {
                    SendersCorrespondent = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    if (dt.Rows[0]["SenderCorrespondent4"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent4"].ToString();
                    }
                    if (dt.Rows[0]["SenderCorrespondent5"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent5"].ToString();
                    }
                    if (dt.Rows[0]["SenderCorrespondent6"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent6"].ToString();
                    }
                    if (dt.Rows[0]["SenderCorrespondent7"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent7"].ToString();
                    }
                }
                if (SendersCorrespondent.Length != 0)
                {
                    comma = SendersCorrespondent.Trim().Substring(0, 1);
                    if (comma == "#") { SendersCorrespondent = SendersCorrespondent.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                string ReceiversCorrespondent = "";
                #region Receivers Correspondent
                if (dt.Rows[0]["ReceiversCorrespondentHeader"].ToString() == "54A")
                {
                    if (dt.Rows[0]["ReceiverCorrespondent1"].ToString() != "")
                    {
                        ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    }
                    if (ReceiversCorrespondent != "" && dt.Rows[0]["ReceiverCorrespondent2"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent2"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReceiverCorrespondent2"].ToString() != "")
                        {
                            ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent2"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ReceiversCorrespondentHeader"].ToString() == "54B")
                {
                    if (dt.Rows[0]["ReceiverCorrespondent1"].ToString() != "")
                    {
                        ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    }
                    if (ReceiversCorrespondent != "" && dt.Rows[0]["ReceiverCorrespondent3"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent3"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReceiverCorrespondent3"].ToString() != "")
                        {
                            ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent3"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ReceiversCorrespondentHeader"].ToString() == "54D")
                {
                    ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    if (dt.Rows[0]["ReceiverCorrespondent4"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent4"].ToString();
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent5"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent5"].ToString();
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent6"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent6"].ToString();
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent7"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent7"].ToString();
                    }
                }
                if (ReceiversCorrespondent.Length != 0)
                {
                    comma = ReceiversCorrespondent.Trim().Substring(0, 1);
                    if (comma == "#") { ReceiversCorrespondent = ReceiversCorrespondent.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                string ThirdReimbursementInstitution = "";
                #region Third Reimbursement Institution
                if (dt.Rows[0]["ThirdReimbursementInstitutionHeader"].ToString() == "55A")
                {
                    if (dt.Rows[0]["ThirdReimbursementInstitution1"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = dt.Rows[0]["ThirdReimbursementInstitution1"].ToString();
                    }
                    if (ThirdReimbursementInstitution != "" && dt.Rows[0]["ThirdReimbursementInstitution2"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = ThirdReimbursementInstitution + "#" + dt.Rows[0]["ThirdReimbursementInstitution2"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ThirdReimbursementInstitution2"].ToString() != "")
                        {
                            ThirdReimbursementInstitution = dt.Rows[0]["ThirdReimbursementInstitution2"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ThirdReimbursementInstitutionHeader"].ToString() == "55B")
                {
                    if (dt.Rows[0]["ThirdReimbursementInstitution1"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = dt.Rows[0]["ThirdReimbursementInstitution1"].ToString();
                    }
                    if (ThirdReimbursementInstitution != "" && dt.Rows[0]["ThirdReimbursementInstitution3"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = ThirdReimbursementInstitution + "#" + dt.Rows[0]["ThirdReimbursementInstitution3"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ThirdReimbursementInstitution3"].ToString() != "")
                        {
                            ThirdReimbursementInstitution = dt.Rows[0]["ThirdReimbursementInstitution3"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ThirdReimbursementInstitutionHeader"].ToString() == "55D")
                {
                    ThirdReimbursementInstitution = dt.Rows[0]["ThirdReimbursementInstitution1"].ToString();
                    if (dt.Rows[0]["ThirdReimbursementInstitution4"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = ThirdReimbursementInstitution + "#" + dt.Rows[0]["ThirdReimbursementInstitution4"].ToString();
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution5"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = ThirdReimbursementInstitution + "#" + dt.Rows[0]["ThirdReimbursementInstitution5"].ToString();
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution6"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = ThirdReimbursementInstitution + "#" + dt.Rows[0]["ThirdReimbursementInstitution6"].ToString();
                    }
                    if (dt.Rows[0]["ThirdReimbursementInstitution7"].ToString() != "")
                    {
                        ThirdReimbursementInstitution = ThirdReimbursementInstitution + "#" + dt.Rows[0]["ThirdReimbursementInstitution7"].ToString();
                    }
                }
                if (ThirdReimbursementInstitution.Length != 0)
                {
                    comma = ThirdReimbursementInstitution.Trim().Substring(0, 1);
                    if (comma == "#") { ThirdReimbursementInstitution = ThirdReimbursementInstitution.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                string IntermediaryInstitution = "";
                #region Intermediary Institution
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56A")
                {
                    if (dt.Rows[0]["Intermediary103AccountNumber"].ToString() != "")
                    {
                        IntermediaryInstitution = dt.Rows[0]["Intermediary103AccountNumber"].ToString();
                    }
                    if (IntermediaryInstitution != "" && dt.Rows[0]["Intermediary103SwiftCode"].ToString() != "")
                    {
                        IntermediaryInstitution = IntermediaryInstitution + "#" + dt.Rows[0]["Intermediary103SwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Intermediary103SwiftCode"].ToString() != "")
                        {
                            IntermediaryInstitution = dt.Rows[0]["Intermediary103SwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56C")
                {
                    IntermediaryInstitution = dt.Rows[0]["Intermediary103AccountNumber"].ToString();
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56D")
                {
                    IntermediaryInstitution = dt.Rows[0]["Intermediary103AccountNumber"].ToString();
                    if (dt.Rows[0]["Intermediary103Name"].ToString() != "")
                    {
                        IntermediaryInstitution = IntermediaryInstitution + "#" + dt.Rows[0]["Intermediary103Name"].ToString();
                    }
                    if (dt.Rows[0]["Intermediary103Address1"].ToString() != "")
                    {
                        IntermediaryInstitution = IntermediaryInstitution + "#" + dt.Rows[0]["Intermediary103Address1"].ToString();
                    }
                    if (dt.Rows[0]["Intermediary103Address2"].ToString() != "")
                    {
                        IntermediaryInstitution = IntermediaryInstitution + "#" + dt.Rows[0]["Intermediary103Address2"].ToString();
                    }
                    if (dt.Rows[0]["Intermediary103Address3"].ToString() != "")
                    {
                        IntermediaryInstitution = IntermediaryInstitution + "#" + dt.Rows[0]["Intermediary103Address3"].ToString();
                    }
                }
                if (IntermediaryInstitution.Length != 0)
                {
                    comma = IntermediaryInstitution.Trim().Substring(0, 1);
                    if (comma == "#") { IntermediaryInstitution = IntermediaryInstitution.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                string AccountWithInstitution = "";
                #region Account With Institution
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57A")
                {
                    if (dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString() != "")
                    {
                        AccountWithInstitution = dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString();
                    }
                    if (AccountWithInstitution != "" && dt.Rows[0]["AccountWithInstitution103SwiftCode"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution103SwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccountWithInstitution103SwiftCode"].ToString() != "")
                        {
                            AccountWithInstitution = dt.Rows[0]["AccountWithInstitution103SwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57B")
                {
                    if (dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString() != "")
                    {
                        AccountWithInstitution = dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString();
                    }
                    if (AccountWithInstitution != "" && dt.Rows[0]["AccountWithInstitution103Location"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution103Location"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccountWithInstitution103Location"].ToString() != "")
                        {
                            AccountWithInstitution = dt.Rows[0]["AccountWithInstitution103Location"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57C")
                {
                    AccountWithInstitution = dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString();
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57D")
                {
                    AccountWithInstitution = dt.Rows[0]["AccountWithInstitution103AccountNumber"].ToString();
                    if (dt.Rows[0]["AccountWithInstitution103Name"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution103Name"].ToString();
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Address1"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution103Address1"].ToString();
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Address2"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution103Address2"].ToString();
                    }
                    if (dt.Rows[0]["AccountWithInstitution103Address3"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution103Address3"].ToString();
                    }
                }
                if (AccountWithInstitution.Length != 0)
                {
                    comma = AccountWithInstitution.Trim().Substring(0, 1);
                    if (comma == "#") { AccountWithInstitution = AccountWithInstitution.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                string BeneficiaryCustomer = "";
                #region Beneficiary_Customer
                if (dt.Rows[0]["BeneficiaryCustomer_header"].ToString() == "59A")
                {
                    if (dt.Rows[0]["BeneficiaryCustomerAcc"].ToString() != "")
                    {
                        BeneficiaryCustomer = dt.Rows[0]["BeneficiaryCustomerAcc"].ToString();
                    }
                    if (BeneficiaryCustomer != "" && dt.Rows[0]["BeneficiaryCustomerSwift"].ToString() != "")
                    {
                        BeneficiaryCustomer = BeneficiaryCustomer + "#" + dt.Rows[0]["BeneficiaryCustomerSwift"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["BeneficiaryCustomerSwift"].ToString() != "")
                        {
                            BeneficiaryCustomer = dt.Rows[0]["BeneficiaryCustomerSwift"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["BeneficiaryCustomer_header"].ToString() == "59F" || dt.Rows[0]["BeneficiaryCustomer_header"].ToString() == "59N")
                {
                    BeneficiaryCustomer = dt.Rows[0]["BeneficiaryCustomerAcc"].ToString();
                    if (dt.Rows[0]["BeneficiaryCustomer_Name"].ToString() != "")
                    {
                        BeneficiaryCustomer = BeneficiaryCustomer + "#" + dt.Rows[0]["BeneficiaryCustomer_Name"].ToString();
                    }
                    if (dt.Rows[0]["BeneficiaryCustomer_Addr1"].ToString() != "")
                    {
                        BeneficiaryCustomer = BeneficiaryCustomer + "#" + dt.Rows[0]["BeneficiaryCustomer_Addr1"].ToString();
                    }
                    if (dt.Rows[0]["BeneficiaryCustomer_Addr2"].ToString() != "")
                    {
                        BeneficiaryCustomer = BeneficiaryCustomer + "#" + dt.Rows[0]["BeneficiaryCustomer_Addr2"].ToString();
                    }
                    if (dt.Rows[0]["BeneficiaryCustomer_Addr3"].ToString() != "")
                    {
                        BeneficiaryCustomer = BeneficiaryCustomer + "#" + dt.Rows[0]["BeneficiaryCustomer_Addr3"].ToString();
                    }
                }
                if (BeneficiaryCustomer.Length != 0)
                {
                    comma = BeneficiaryCustomer.Trim().Substring(0, 1);
                    if (comma == "#") { BeneficiaryCustomer = BeneficiaryCustomer.Remove(0, 1); }
                    comma = "";
                }

                #endregion
                string RemittanceInformation = "";
                #region Remittance_Information
                if (dt.Rows[0]["RemittanceInformation1"].ToString() != "")
                {
                    RemittanceInformation = dt.Rows[0]["RemittanceInformation1"].ToString();
                }
                if (dt.Rows[0]["RemittanceInformation2"].ToString() != "")
                {
                    RemittanceInformation = RemittanceInformation + '#' + dt.Rows[0]["RemittanceInformation2"].ToString();
                }
                if (dt.Rows[0]["RemittanceInformation3"].ToString() != "")
                {
                    RemittanceInformation = RemittanceInformation + '#' + dt.Rows[0]["RemittanceInformation3"].ToString();
                }
                if (dt.Rows[0]["RemittanceInformation4"].ToString() != "")
                {
                    RemittanceInformation = RemittanceInformation + '#' + dt.Rows[0]["RemittanceInformation4"].ToString();
                }
                if (RemittanceInformation.Length != 0)
                {
                    comma = RemittanceInformation.Trim().Substring(0, 1);
                    if (comma == "#") { RemittanceInformation = RemittanceInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                #region SendersCharges
                string SendersCharges = dt.Rows[0]["SenderCharges1"].ToString();
                if (dt.Rows[0]["SenderCharges2"].ToString() != "")
                {
                    if (dt.Rows[0]["SenderCharges2"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["SenderCharges2"].ToString().Contains(".00"))
                        {
                            SendersCharges = SendersCharges + dt.Rows[0]["SenderCharges2"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            SendersCharges = SendersCharges + dt.Rows[0]["SenderCharges2"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        SendersCharges = SendersCharges + dt.Rows[0]["SenderCharges2"].ToString() + ",";
                    }
                }
                #endregion
                #region ReceiversCharges
                string ReceiversCharges = dt.Rows[0]["ReceiverCharges1"].ToString();
                if (dt.Rows[0]["ReceiverCharges2"].ToString() != "")
                {
                    if (dt.Rows[0]["ReceiverCharges2"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["ReceiverCharges2"].ToString().Contains(".00"))
                        {
                            ReceiversCharges = ReceiversCharges + dt.Rows[0]["ReceiverCharges2"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            ReceiversCharges = ReceiversCharges + dt.Rows[0]["ReceiverCharges2"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        ReceiversCharges = ReceiversCharges + dt.Rows[0]["ReceiverCharges2"].ToString() + ",";
                    }
                }
                #endregion
                #region SendertoReceiverInformation
                string SendertoReceiverInformation = "";
                if (dt.Rows[0]["SendertoReceiverInformation1"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SendertoReceiverInformation1"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation2"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + '#' + dt.Rows[0]["SendertoReceiverInformation2"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation3"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + '#' + dt.Rows[0]["SendertoReceiverInformation3"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation4"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + '#' + dt.Rows[0]["SendertoReceiverInformation4"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation5"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + '#' + dt.Rows[0]["SendertoReceiverInformation5"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation6"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + '#' + dt.Rows[0]["SendertoReceiverInformation6"].ToString().Replace("-", "");
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == ",") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Regulatory Reporting
                string RegulatoryReporting = "";
                if (dt.Rows[0]["RegulatoryReporting1"].ToString() != "")
                {
                    RegulatoryReporting = dt.Rows[0]["RegulatoryReporting1"].ToString();
                }
                if (dt.Rows[0]["RegulatoryReporting2"].ToString() != "")
                {
                    RegulatoryReporting = RegulatoryReporting + '#' + dt.Rows[0]["RegulatoryReporting2"].ToString();
                }
                if (dt.Rows[0]["RegulatoryReporting3"].ToString() != "")
                {
                    RegulatoryReporting = RegulatoryReporting + '#' + dt.Rows[0]["RegulatoryReporting3"].ToString();
                }
                if (RegulatoryReporting.Length != 0)
                {
                    comma = RegulatoryReporting.Trim().Substring(0, 1);
                    if (comma == ",") { RegulatoryReporting = RegulatoryReporting.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<SendersReference>" + dt.Rows[0]["SendersReference"].ToString() + "</SendersReference>");
                sw.WriteLine("<TimeIndication>" + dt.Rows[0]["TimeIndication"].ToString() + "</TimeIndication>");
                sw.WriteLine("<BankOperationCode>" + dt.Rows[0]["BankOperationCode"].ToString() + "</BankOperationCode>");
                sw.WriteLine("<InstructionCode>" + dt.Rows[0]["InstructionCode"].ToString() + "</InstructionCode>");
                sw.WriteLine("<TransactionTypeCode>" + dt.Rows[0]["TransactionTypeCode"].ToString() + "</TransactionTypeCode>");
                sw.WriteLine("<ValueDate>" + ValueDateCurrencyInterbankSettledAmount + "</ValueDate>");
                sw.WriteLine("<CurrencyInstructedAmount>" + CurrencyInstructedAmount + "</CurrencyInstructedAmount>");
                sw.WriteLine("<ExchangeRate>" + exchangerate + "</ExchangeRate>");

                if (dt.Rows[0]["OrderingCustomer_header"].ToString() == "50A")
                {
                    sw.WriteLine("<OrderingCustomerA>" + OrderingCustomer + "</OrderingCustomerA>");
                }
                if (dt.Rows[0]["OrderingCustomer_header"].ToString() == "50F")
                {
                    sw.WriteLine("<OrderingCustomerF>" + OrderingCustomer + "</OrderingCustomerF>");
                }
                if (dt.Rows[0]["OrderingCustomer_header"].ToString() == "50K")
                {
                    sw.WriteLine("<OrderingCustomerK>" + OrderingCustomer + "</OrderingCustomerK>");
                }

                sw.WriteLine("<SendingInstitution>" + SendingInstitution + "</SendingInstitution>");

                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52A")
                {
                    sw.WriteLine("<OrderingInstitutionA>" + OrderingInstitution + "</OrderingInstitutionA>");
                }
                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52D")
                {
                    sw.WriteLine("<OrderingInstitutionD>" + OrderingInstitution + "</OrderingInstitutionD>");
                }
                if (dt.Rows[0]["SendersCorrespondentHeader"].ToString() == "53A")
                {
                    sw.WriteLine("<SendersCorrespondentA>" + SendersCorrespondent + "</SendersCorrespondentA>");
                }
                if (dt.Rows[0]["SendersCorrespondentHeader"].ToString() == "53B")
                {
                    sw.WriteLine("<SendersCorrespondentB>" + SendersCorrespondent + "</SendersCorrespondentB>");
                }
                if (dt.Rows[0]["SendersCorrespondentHeader"].ToString() == "53D")
                {
                    sw.WriteLine("<SendersCorrespondentD>" + SendersCorrespondent + "</SendersCorrespondentD>");
                }
                if (dt.Rows[0]["ReceiversCorrespondentHeader"].ToString() == "54A")
                {
                    sw.WriteLine("<ReceiversCorrespondentA>" + ReceiversCorrespondent + "</ReceiversCorrespondentA>");
                }
                if (dt.Rows[0]["ReceiversCorrespondentHeader"].ToString() == "54B")
                {
                    sw.WriteLine("<ReceiversCorrespondentB>" + ReceiversCorrespondent + "</ReceiversCorrespondentB>");
                }
                if (dt.Rows[0]["ReceiversCorrespondentHeader"].ToString() == "54D")
                {
                    sw.WriteLine("<ReceiversCorrespondentD>" + ReceiversCorrespondent + "</ReceiversCorrespondentD>");
                }
                if (dt.Rows[0]["ThirdReimbursementInstitutionHeader"].ToString() == "55A")
                {
                    sw.WriteLine("<ThirdReimbursementA>" + ThirdReimbursementInstitution + "</ThirdReimbursementA>");
                }
                if (dt.Rows[0]["ThirdReimbursementInstitutionHeader"].ToString() == "55B")
                {
                    sw.WriteLine("<ThirdReimbursementB>" + ThirdReimbursementInstitution + "</ThirdReimbursementB>");
                }
                if (dt.Rows[0]["ThirdReimbursementInstitutionHeader"].ToString() == "55D")
                {
                    sw.WriteLine("<ThirdReimbursementD>" + ThirdReimbursementInstitution + "</ThirdReimbursementD>");
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56A")
                {
                    sw.WriteLine("<IntermediaryInstitutionA>" + IntermediaryInstitution + "</IntermediaryInstitutionA>");
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56C")
                {
                    sw.WriteLine("<IntermediaryInstitutionC>" + IntermediaryInstitution + "</IntermediaryInstitutionC>");
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56D")
                {
                    sw.WriteLine("<IntermediaryInstitutionD>" + IntermediaryInstitution + "</IntermediaryInstitutionD>");
                }
                //----------------------------------------AccountWithInstitution---------------------------------------------------------------------
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57A")
                {
                    sw.WriteLine("<AccountWithInstitutionA>" + AccountWithInstitution + "</AccountWithInstitutionA>");
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57B")
                {
                    sw.WriteLine("<AccountWithInstitutionB>" + AccountWithInstitution + "</AccountWithInstitutionB>");
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57C")
                {
                    sw.WriteLine("<AccountWithInstitutionC>" + AccountWithInstitution + "</AccountWithInstitutionC>");
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57D")
                {
                    sw.WriteLine("<AccountWithInstitutionD>" + AccountWithInstitution + "</AccountWithInstitutionD>");
                }

                if (dt.Rows[0]["BeneficiaryCustomer_header"].ToString() == "59A")
                {
                    sw.WriteLine("<BeneficiaryCustomerA>" + BeneficiaryCustomer + "</BeneficiaryCustomerA>");
                }
                if (dt.Rows[0]["BeneficiaryCustomer_header"].ToString() == "59F")
                {
                    sw.WriteLine("<BeneficiaryCustomerF>" + BeneficiaryCustomer + "</BeneficiaryCustomerF>");
                }
                if (dt.Rows[0]["BeneficiaryCustomer_header"].ToString() == "59N")
                {
                    sw.WriteLine("<BeneficiaryCustomer>" + BeneficiaryCustomer + "</BeneficiaryCustomer>");
                }

                sw.WriteLine("<RemittanceInformation>" + RemittanceInformation + "</RemittanceInformation>");
                sw.WriteLine("<DetailsofCharges>" + dt.Rows[0]["DetailsofCharges"].ToString() + "</DetailsofCharges>");
                sw.WriteLine("<SendersCharges>" + SendersCharges + "</SendersCharges>");
                sw.WriteLine("<ReceiversCharges>" + ReceiversCharges + "</ReceiversCharges>");
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("<RegulatoryReporting>" + RegulatoryReporting + "</RegulatoryReporting>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

    }  // swift change bhupen
    public void CreateSwift202_XML(string FileType)
    {
        TF_DATA obj_Data1 = new TF_DATA();
        SqlParameter P_Docno = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = obj_Data1.getData("TF_IMP_Swift_MT202_FileCreation_XML", P_Docno);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Settlement/MT202/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Settlement/SFMS202/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT202_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN202_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string TransactionReferenceNumber = dt.Rows[0]["SendersReference"].ToString();
                string RelatedReference = dt.Rows[0]["RelatedReference"].ToString();
                string TimeIndication = dt.Rows[0]["TimeIndication"].ToString();
                string ValueDateCurrencyAmount = dt.Rows[0]["ValueDate"].ToString().Replace("/", "") + dt.Rows[0]["Currency"].ToString().Replace("/", "");
                #region Amount
                if (dt.Rows[0]["Amount"].ToString() != "")
                {
                    if (dt.Rows[0]["Amount"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["Amount"].ToString().Contains(".00"))
                        {
                            ValueDateCurrencyAmount = ValueDateCurrencyAmount + dt.Rows[0]["Amount"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            ValueDateCurrencyAmount = ValueDateCurrencyAmount + dt.Rows[0]["Amount"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        ValueDateCurrencyAmount = ValueDateCurrencyAmount + dt.Rows[0]["Amount"].ToString() + ",";
                    }
                }
                #endregion
                string OrderingInstitution = "";
                string SendersCorrespondent = "";
                string ReceiversCorrespondent = "";
                string Intermediary = "";
                string AccountWithInstitution = "";
                string BeneficiaryInstitution = "";
                string SendertoReceiverInformation = "";
                string comma = "";
                #region Ordering Institution
                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52A")
                {
                    if (dt.Rows[0]["OrderingInstitution1"].ToString() != "")
                    {
                        OrderingInstitution = dt.Rows[0]["OrderingInstitution1"].ToString();
                    }
                    if (OrderingInstitution != "" && dt.Rows[0]["OrderingInstitution2"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution2"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["OrderingInstitution2"].ToString() != "")
                        {
                            OrderingInstitution = dt.Rows[0]["OrderingInstitution2"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52D")
                {
                    OrderingInstitution = dt.Rows[0]["OrderingInstitution1"].ToString();
                    if (dt.Rows[0]["OrderingInstitution3"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution3"].ToString().Replace("-", "");
                    }
                    if (dt.Rows[0]["OrderingInstitution4"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution4"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["OrderingInstitution5"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution5"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["OrderingInstitution6"].ToString() != "")
                    {
                        OrderingInstitution = OrderingInstitution + "#" + dt.Rows[0]["OrderingInstitution6"].ToString().Replace("/", "");
                    }
                }
                if (OrderingInstitution.Length != 0)
                {
                    comma = OrderingInstitution.Trim().Substring(0, 1);
                    if (comma == "#") { OrderingInstitution = OrderingInstitution.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region SendersCorrespondent
                if (dt.Rows[0]["SenderCorrespondentHeader"].ToString() == "53A")
                {
                    if (dt.Rows[0]["SenderCorrespondent1"].ToString() != "")
                    {
                        SendersCorrespondent = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    }
                    if (SendersCorrespondent != "" && dt.Rows[0]["SenderCorrespondent2"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent2"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["SenderCorrespondent2"].ToString() != "")
                        {
                            SendersCorrespondent = dt.Rows[0]["SenderCorrespondent2"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["SenderCorrespondentHeader"].ToString() == "53B")
                {
                    if (dt.Rows[0]["SenderCorrespondent1"].ToString() != "")
                    {
                        SendersCorrespondent = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    }
                    if (SendersCorrespondent != "" && dt.Rows[0]["SenderCorrespondent3"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent3"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["SenderCorrespondent3"].ToString() != "")
                        {
                            SendersCorrespondent = dt.Rows[0]["SenderCorrespondent3"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["SenderCorrespondentHeader"].ToString() == "53D")
                {
                    SendersCorrespondent = dt.Rows[0]["SenderCorrespondent1"].ToString();
                    if (dt.Rows[0]["SenderCorrespondent4"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent4"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["SenderCorrespondent5"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent5"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["SenderCorrespondent6"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent6"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["SenderCorrespondent7"].ToString() != "")
                    {
                        SendersCorrespondent = SendersCorrespondent + "#" + dt.Rows[0]["SenderCorrespondent7"].ToString().Replace("/", "");
                    }
                }
                if (SendersCorrespondent.Length != 0)
                {
                    comma = SendersCorrespondent.Trim().Substring(0, 1);
                    if (comma == "#") { SendersCorrespondent = SendersCorrespondent.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Receiver Correspondent
                if (dt.Rows[0]["ReceiverCorrespondentHeader"].ToString() == "54A")
                {
                    if (dt.Rows[0]["ReceiverCorrespondent1"].ToString() != "")
                    {
                        ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    }
                    if (ReceiversCorrespondent != "" && dt.Rows[0]["ReceiverCorrespondent2"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent2"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReceiverCorrespondent2"].ToString() != "")
                        {
                            ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent2"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ReceiverCorrespondentHeader"].ToString() == "54B")
                {
                    if (dt.Rows[0]["ReceiverCorrespondent1"].ToString() != "")
                    {
                        ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    }
                    if (ReceiversCorrespondent != "" && dt.Rows[0]["ReceiverCorrespondent3"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent3"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReceiverCorrespondent3"].ToString() != "")
                        {
                            ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent3"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ReceiverCorrespondentHeader"].ToString() == "54D")
                {
                    ReceiversCorrespondent = dt.Rows[0]["ReceiverCorrespondent1"].ToString();
                    if (dt.Rows[0]["ReceiverCorrespondent4"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent4"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent5"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent5"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent6"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent6"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["ReceiverCorrespondent7"].ToString() != "")
                    {
                        ReceiversCorrespondent = ReceiversCorrespondent + "#" + dt.Rows[0]["ReceiverCorrespondent7"].ToString().Replace("/", "");
                    }
                }
                if (ReceiversCorrespondent.Length != 0)
                {
                    comma = ReceiversCorrespondent.Trim().Substring(0, 1);
                    if (comma == "#") { ReceiversCorrespondent = ReceiversCorrespondent.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Intermediary
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56A")
                {
                    if (dt.Rows[0]["Intermediary202AccountNumber"].ToString() != "")
                    {
                        Intermediary = dt.Rows[0]["Intermediary202AccountNumber"].ToString();
                    }
                    if (Intermediary != "" && dt.Rows[0]["Intermediary202SwiftCode"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["Intermediary202SwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Intermediary202SwiftCode"].ToString() != "")
                        {
                            Intermediary = dt.Rows[0]["Intermediary202SwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56D")
                {
                    Intermediary = dt.Rows[0]["Intermediary202AccountNumber"].ToString();
                    if (dt.Rows[0]["Intermediary202Name"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["Intermediary202Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["Intermediary202Address1"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["Intermediary202Address1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["Intermediary202Address2"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["Intermediary202Address2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["Intermediary202Address3"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["Intermediary202Address3"].ToString().Replace("/", "");
                    }
                }
                if (Intermediary.Length != 0)
                {
                    comma = Intermediary.Trim().Substring(0, 1);
                    if (comma == "#") { Intermediary = Intermediary.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Account With Institution
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57A")
                {
                    if (dt.Rows[0]["AccountWithInstitution202AccountNumber"].ToString() != "")
                    {
                        AccountWithInstitution = dt.Rows[0]["AccountWithInstitution202AccountNumber"].ToString();
                    }
                    if (AccountWithInstitution != "" && dt.Rows[0]["AccountWithInstitution202SwiftCode"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution202SwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccountWithInstitution202SwiftCode"].ToString() != "")
                        {
                            AccountWithInstitution = dt.Rows[0]["AccountWithInstitution202SwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57B")
                {
                    if (dt.Rows[0]["AccountWithInstitution202AccountNumber"].ToString() != "")
                    {
                        AccountWithInstitution = dt.Rows[0]["AccountWithInstitution202AccountNumber"].ToString();
                    }
                    if (AccountWithInstitution != "" && dt.Rows[0]["AccountWithInstitution202Location"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution202Location"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccountWithInstitution202Location"].ToString() != "")
                        {
                            AccountWithInstitution = dt.Rows[0]["AccountWithInstitution202Location"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57D")
                {
                    AccountWithInstitution = dt.Rows[0]["AccountWithInstitution202AccountNumber"].ToString();
                    if (dt.Rows[0]["AccountWithInstitution202Name"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution202Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Address1"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution202Address1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Address2"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution202Address2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccountWithInstitution202Address3"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccountWithInstitution202Address3"].ToString().Replace("/", "");
                    }
                }
                if (AccountWithInstitution.Length != 0)
                {
                    comma = AccountWithInstitution.Trim().Substring(0, 1);
                    if (comma == ",") { AccountWithInstitution = AccountWithInstitution.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Beneficiary
                if (dt.Rows[0]["BeneficiaryCustomerHeader"].ToString() == "58A")
                {
                    if (dt.Rows[0]["BeneficiaryInstitution202AccountNumber"].ToString() != "")
                    {
                        BeneficiaryInstitution = dt.Rows[0]["BeneficiaryInstitution202AccountNumber"].ToString();
                    }
                    if (BeneficiaryInstitution != "" && dt.Rows[0]["BeneficiaryInstitution202SwiftCode"].ToString() != "")
                    {
                        BeneficiaryInstitution = BeneficiaryInstitution + "#" + dt.Rows[0]["BeneficiaryInstitution202SwiftCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["BeneficiaryInstitution202SwiftCode"].ToString() != "")
                        {
                            BeneficiaryInstitution = dt.Rows[0]["BeneficiaryInstitution202SwiftCode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["BeneficiaryCustomerHeader"].ToString() == "58D")
                {
                    BeneficiaryInstitution = dt.Rows[0]["BeneficiaryInstitution202AccountNumber"].ToString();
                    if (dt.Rows[0]["BeneficiaryInstitution202Name"].ToString() != "")
                    {
                        BeneficiaryInstitution = BeneficiaryInstitution + "#" + dt.Rows[0]["BeneficiaryInstitution202Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryInstitution202Address1"].ToString() != "")
                    {
                        BeneficiaryInstitution = BeneficiaryInstitution + "#" + dt.Rows[0]["BeneficiaryInstitution202Address1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryInstitution202Address2"].ToString() != "")
                    {
                        BeneficiaryInstitution = BeneficiaryInstitution + "#" + dt.Rows[0]["BeneficiaryInstitution202Address2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryInstitution202Address3"].ToString() != "")
                    {
                        BeneficiaryInstitution = BeneficiaryInstitution + "#" + dt.Rows[0]["BeneficiaryInstitution202Address3"].ToString().Replace("/", "");
                    }
                }
                #endregion
                #region Sender TO Receiver
                if (dt.Rows[0]["SendertoReceiverInformation1"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SendertoReceiverInformation1"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation2"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation2"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation3"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation3"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation4"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation4"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation5"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation5"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation6"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation6"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation7"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation7"].ToString();
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == "#") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["TO"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + TransactionReferenceNumber + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + RelatedReference + "</RelatedReference>");
                sw.WriteLine("<TimeIndication>" + TimeIndication + "</TimeIndication>");
                sw.WriteLine("<ValueDate>" + ValueDateCurrencyAmount + "</ValueDate>");
                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52A")
                {
                    sw.WriteLine("<OrderingInstitutionA>" + OrderingInstitution + "</OrderingInstitutionA>");
                }
                if (dt.Rows[0]["OrderingInstitutionHeader"].ToString() == "52D")
                {
                    sw.WriteLine("<OrderingInstitutionD>" + OrderingInstitution + "</OrderingInstitutionD>");
                }
                if (dt.Rows[0]["SenderCorrespondentHeader"].ToString() == "53A")
                {
                    sw.WriteLine("<SendersCorrespondentA>" + SendersCorrespondent + "</SendersCorrespondentA>");
                }
                if (dt.Rows[0]["SenderCorrespondentHeader"].ToString() == "53B")
                {
                    sw.WriteLine("<SendersCorrespondentB>" + SendersCorrespondent + "</SendersCorrespondentB>");
                }
                if (dt.Rows[0]["SenderCorrespondentHeader"].ToString() == "53D")
                {
                    sw.WriteLine("<SendersCorrespondentD>" + SendersCorrespondent + "</SendersCorrespondentD>");
                }
                if (dt.Rows[0]["ReceiverCorrespondentHeader"].ToString() == "54A")
                {
                    sw.WriteLine("<ReceiversCorrespondentA>" + ReceiversCorrespondent + "</ReceiversCorrespondentA>");
                }
                if (dt.Rows[0]["ReceiverCorrespondentHeader"].ToString() == "54B")
                {
                    sw.WriteLine("<ReceiversCorrespondentB>" + ReceiversCorrespondent + "</ReceiversCorrespondentB>");
                }
                if (dt.Rows[0]["ReceiverCorrespondentHeader"].ToString() == "54D")
                {
                    sw.WriteLine("<ReceiversCorrespondentD>" + ReceiversCorrespondent + "</ReceiversCorrespondentD>");
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56A")
                {
                    sw.WriteLine("<IntermediaryA>" + Intermediary + "</IntermediaryA>");
                }
                if (dt.Rows[0]["IntermediaryInstitutionHeader"].ToString() == "56D")
                {
                    sw.WriteLine("<IntermediaryD>" + Intermediary + "</IntermediaryD>");
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57A")
                {
                    sw.WriteLine("<AccountWithInstitutionA>" + AccountWithInstitution + "</AccountWithInstitutionA>");
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57B")
                {
                    sw.WriteLine("<AccountWithInstitutionB>" + AccountWithInstitution + "</AccountWithInstitutionB>");
                }
                if (dt.Rows[0]["AccountWithInstitutionHeader"].ToString() == "57D")
                {
                    sw.WriteLine("<AccountWithInstitutionD>" + AccountWithInstitution + "</AccountWithInstitutionD>");
                }
                if (dt.Rows[0]["BeneficiaryCustomerHeader"].ToString() == "58A")
                {
                    sw.WriteLine("<BeneficiaryInstitutionA>" + BeneficiaryInstitution + "</BeneficiaryInstitutionA>");
                }
                if (dt.Rows[0]["BeneficiaryCustomerHeader"].ToString() == "58D")
                {
                    sw.WriteLine("<BeneficiaryInstitutionD>" + BeneficiaryInstitution + "</BeneficiaryInstitutionD>");
                }
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    } // swift change
    public void CreateSwift200_XML(string FileType)
    {
        TF_DATA obj_data1 = new TF_DATA();
        SqlParameter p_Docno = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = obj_data1.getData("TF_IMP_Swift_MT200_FileCreation_XML", p_Docno);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Settlement/MT200/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Settlement/SFMS200/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT200_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN200_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string SendersCorrespondent = "";
                string Intermediary = "";
                string AccountWithInstitution = "";
                string SendertoReceiverInformation = "";
                string comma = "";
                string ValueDateCurrencyAmount = dt.Rows[0]["Date200"].ToString().Replace("/", "") + dt.Rows[0]["Currency200"].ToString().Replace("/", "");
                #region Amount
                if (dt.Rows[0]["Amount200"].ToString() != "")
                {
                    if (dt.Rows[0]["Amount200"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["Amount200"].ToString().Contains(".00"))
                        {
                            ValueDateCurrencyAmount = ValueDateCurrencyAmount + dt.Rows[0]["Amount200"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            ValueDateCurrencyAmount = ValueDateCurrencyAmount + dt.Rows[0]["Amount200"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        ValueDateCurrencyAmount = ValueDateCurrencyAmount + dt.Rows[0]["Amount200"].ToString() + ",";
                    }
                }
                #endregion
                if (dt.Rows[0]["SenderCorreCode200"].ToString() != "" || dt.Rows[0]["SenderCorreLocation200"].ToString() != "")
                {
                    SendersCorrespondent = dt.Rows[0]["SenderCorreCode200"].ToString() + "#" + dt.Rows[0]["SenderCorreLocation200"].ToString();
                }
                #region Intermediary
                if (dt.Rows[0]["Intermediary200Header"].ToString() == "56A")
                {
                    if (dt.Rows[0]["IntermediaryAccountNumber200"].ToString() != "")
                    {
                        Intermediary = dt.Rows[0]["IntermediaryAccountNumber200"].ToString();
                    }
                    if (Intermediary != "" && dt.Rows[0]["IntermediarySwiftCode200"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["IntermediarySwiftCode200"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["IntermediarySwiftCode200"].ToString() != "")
                        {
                            Intermediary = dt.Rows[0]["IntermediarySwiftCode200"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["Intermediary200Header"].ToString() == "56D")
                {
                    Intermediary = dt.Rows[0]["IntermediaryAccountNumber200"].ToString();
                    if (dt.Rows[0]["IntermediaryName200"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["IntermediaryName200"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["IntermediaryAddress1200"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["IntermediaryAddress1200"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["IntermediaryAddress2200"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["IntermediaryAddress2200"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["IntermediaryAddress3200"].ToString() != "")
                    {
                        Intermediary = Intermediary + "#" + dt.Rows[0]["IntermediaryAddress3200"].ToString().Replace("/", "");
                    }
                }
                if (Intermediary.Length != 0)
                {
                    comma = Intermediary.Trim().Substring(0, 1);
                    if (comma == "#") { Intermediary = Intermediary.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Account With Institution
                if (dt.Rows[0]["AccWithInstitution200Header"].ToString() == "57A")
                {
                    if (dt.Rows[0]["AccWithInstitutionAccountNumber200"].ToString() != "")
                    {
                        AccountWithInstitution = dt.Rows[0]["AccWithInstitutionAccountNumber200"].ToString();
                    }
                    if (AccountWithInstitution != "" && dt.Rows[0]["AccWithInstitutionSwiftCode200"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccWithInstitutionSwiftCode200"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccWithInstitutionSwiftCode200"].ToString() != "")
                        {
                            AccountWithInstitution = dt.Rows[0]["AccWithInstitutionSwiftCode200"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccWithInstitution200Header"].ToString() == "57B")
                {
                    if (dt.Rows[0]["AccWithInstitutionAccountNumber200"].ToString() != "")
                    {
                        AccountWithInstitution = dt.Rows[0]["AccWithInstitutionAccountNumber200"].ToString();
                    }
                    if (AccountWithInstitution != "" && dt.Rows[0]["AccWithInstitutionLocation200"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccWithInstitutionLocation200"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccWithInstitutionLocation200"].ToString() != "")
                        {
                            AccountWithInstitution = dt.Rows[0]["AccWithInstitutionLocation200"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["AccWithInstitution200Header"].ToString() == "57D")
                {
                    AccountWithInstitution = dt.Rows[0]["AccWithInstitutionAccountNumber200"].ToString();
                    if (dt.Rows[0]["AccWithInstitutionName200"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccWithInstitutionName200"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithInstitutionAddress1200"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccWithInstitutionAddress1200"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithInstitutionAddress2200"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccWithInstitutionAddress2200"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithInstitutionAddress3200"].ToString() != "")
                    {
                        AccountWithInstitution = AccountWithInstitution + "#" + dt.Rows[0]["AccWithInstitutionAddress3200"].ToString().Replace("/", "");
                    }
                }
                if (AccountWithInstitution.Length != 0)
                {
                    comma = AccountWithInstitution.Trim().Substring(0, 1);
                    if (comma == "#") { AccountWithInstitution = AccountWithInstitution.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Sender To Receiver Information
                if (dt.Rows[0]["SendertoReceiverInformation1200"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SendertoReceiverInformation1200"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation2200"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation2200"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation3200"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation3200"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation4200"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation4200"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation5200"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation5200"].ToString();
                }
                if (dt.Rows[0]["SendertoReceiverInformation6200"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "#" + dt.Rows[0]["SendertoReceiverInformation6200"].ToString();
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == "#") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["TO"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["TransactionReferenceNo"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<ValueDate>" + ValueDateCurrencyAmount + "</ValueDate>");
                sw.WriteLine("<SendersCorrespondent>" + SendersCorrespondent + "</SendersCorrespondent>");
                if (dt.Rows[0]["Intermediary200Header"].ToString() == "56A")
                {
                    sw.WriteLine("<IntermediaryA>" + Intermediary + "</IntermediaryA>");
                }
                if (dt.Rows[0]["Intermediary200Header"].ToString() == "56D")
                {
                    sw.WriteLine("<IntermediaryD>" + Intermediary + "</IntermediaryD>");
                }

                if (dt.Rows[0]["AccWithInstitution200Header"].ToString() == "57A")
                {
                    sw.WriteLine("<AccountWithInstitutionA>" + AccountWithInstitution + "</AccountWithInstitutionA>");
                }
                if (dt.Rows[0]["AccWithInstitution200Header"].ToString() == "57B")
                {
                    sw.WriteLine("<AccountWithInstitutionB>" + AccountWithInstitution + "</AccountWithInstitutionB>");
                }
                if (dt.Rows[0]["AccWithInstitution200Header"].ToString() == "57D")
                {
                    sw.WriteLine("<AccountWithInstitutionD>" + AccountWithInstitution + "</AccountWithInstitutionD>");
                }
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }  // swift change
    public void CreateSwift754_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT754_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Settlement/MT754/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Settlement/SFMS754/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT754_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN754_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                #region Principal amount Paid
                string PrincipalAmountPaid = "";
                if (dt.Rows[0]["Pri_Amt_Paid_754"].ToString() == "A")
                {
                    if (dt.Rows[0]["Pri_AMT_Date_754"].ToString() != "")
                    {
                        PrincipalAmountPaid = dt.Rows[0]["Pri_AMT_Date_754"].ToString();
                    }
                    if (dt.Rows[0]["Pri_AMT_Curr_754"].ToString() != "")
                    {
                        PrincipalAmountPaid = PrincipalAmountPaid + dt.Rows[0]["Pri_AMT_Curr_754"].ToString();
                    }
                    if (dt.Rows[0]["Pri_AMT_Amt_754"].ToString() != "")
                    {
                        if (dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Contains(".00"))
                            {
                                PrincipalAmountPaid = PrincipalAmountPaid + dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                PrincipalAmountPaid = PrincipalAmountPaid + dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            PrincipalAmountPaid = PrincipalAmountPaid + dt.Rows[0]["Pri_AMT_Amt_754"].ToString() + ",";
                        }
                    }
                }
                if (dt.Rows[0]["Pri_Amt_Paid_754"].ToString() == "B")
                {
                    if (dt.Rows[0]["Pri_AMT_Curr_754"].ToString() != "")
                    {
                        PrincipalAmountPaid = dt.Rows[0]["Pri_AMT_Curr_754"].ToString();
                    }
                    if (dt.Rows[0]["Pri_AMT_Amt_754"].ToString() != "")
                    {
                        if (dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Contains(".00"))
                            {
                                PrincipalAmountPaid = PrincipalAmountPaid + dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                PrincipalAmountPaid = PrincipalAmountPaid + dt.Rows[0]["Pri_AMT_Amt_754"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            PrincipalAmountPaid = PrincipalAmountPaid + dt.Rows[0]["Pri_AMT_Amt_754"].ToString() + ",";
                        }
                    }
                }
                #endregion
                #region Additional Amounts
                string AdditionalAmounts = "";
                AdditionalAmounts = dt.Rows[0]["Additional_AMT_Curr_754"].ToString().Replace("-", "");
                if (dt.Rows[0]["Additional_AMT_Amt_754"].ToString() != "")
                {
                    if (dt.Rows[0]["Additional_AMT_Amt_754"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["Additional_AMT_Amt_754"].ToString().Contains(".00"))
                        {
                            AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["Additional_AMT_Amt_754"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["Additional_AMT_Amt_754"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["Additional_AMT_Amt_754"].ToString() + ",";
                    }
                }
                #endregion
                #region Charges deducted
                string Chargesdeducted = ""; string comma = "";
                if (dt.Rows[0]["Charges_deducted1_754"].ToString() != "")
                {
                    Chargesdeducted = dt.Rows[0]["Charges_deducted1_754"].ToString();
                }
                if (dt.Rows[0]["Charges_deducted2_754"].ToString() != "")
                {
                    Chargesdeducted = Chargesdeducted + "," + dt.Rows[0]["Charges_deducted2_754"].ToString();
                }
                if (dt.Rows[0]["Charges_deducted3_754"].ToString() != "")
                {
                    Chargesdeducted = Chargesdeducted + "," + dt.Rows[0]["Charges_deducted3_754"].ToString();
                }
                if (dt.Rows[0]["Charges_deducted4_754"].ToString() != "")
                {
                    Chargesdeducted = Chargesdeducted + "," + dt.Rows[0]["Charges_deducted4_754"].ToString();
                }
                if (dt.Rows[0]["Charges_deducted5_754"].ToString() != "")
                {
                    Chargesdeducted = Chargesdeducted + "," + dt.Rows[0]["Charges_deducted5_754"].ToString();
                }
                if (dt.Rows[0]["Charges_deducted6_754"].ToString() != "")
                {
                    Chargesdeducted = Chargesdeducted + "," + dt.Rows[0]["Charges_deducted6_754"].ToString();
                }
                if (Chargesdeducted.Length != 0)
                {
                    comma = Chargesdeducted.Trim().Substring(0, 1);
                    if (comma == ",") { Chargesdeducted = Chargesdeducted.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Charges Added
                string ChargesAdded = "";
                if (dt.Rows[0]["Charges_Added1_754"].ToString() != "")
                {
                    ChargesAdded = dt.Rows[0]["Charges_Added1_754"].ToString();
                }
                if (dt.Rows[0]["Charges_Added2_754"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["Charges_Added2_754"].ToString();
                }
                if (dt.Rows[0]["Charges_Added3_754"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["Charges_Added3_754"].ToString();
                }
                if (dt.Rows[0]["Charges_Added4_754"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["Charges_Added4_754"].ToString();
                }
                if (dt.Rows[0]["Charges_Added5_754"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["Charges_Added5_754"].ToString();
                }
                if (dt.Rows[0]["Charges_Added6_754"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["Charges_Added6_754"].ToString();
                }
                if (ChargesAdded.Length != 0)
                {
                    comma = ChargesAdded.Trim().Substring(0, 1);
                    if (comma == ",") { ChargesAdded = ChargesAdded.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Total Amount Claimed
                string TotalAmountClam = "";
                if (dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString() == "A")
                {
                    TotalAmountClam = dt.Rows[0]["Ttl_Amt_Clmd_Date_754"].ToString() + dt.Rows[0]["Ttl_Amt_Clmd_Curr_754"].ToString();
                    if (dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString() != "")
                    {
                        if (dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Contains(".00"))
                            {
                                TotalAmountClam = TotalAmountClam + dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClam = TotalAmountClam + dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClam = TotalAmountClam + dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString() + ",";
                        }
                    }
                }
                if (dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString() == "B")
                {
                    TotalAmountClam = dt.Rows[0]["Ttl_Amt_Clmd_Curr_754"].ToString();
                    if (dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString() != "")
                    {
                        if (dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Contains(".00"))
                            {
                                TotalAmountClam = TotalAmountClam + dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClam = TotalAmountClam + dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClam = TotalAmountClam + dt.Rows[0]["Ttl_Amt_Clmd_AMT_754"].ToString() + ",";
                        }
                    }
                }
                #endregion
                #region Reimbursing Bank
                string ReimbursingBank = "";
                if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "A")
                {
                    if (dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() != "" || dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString() != "")
                    {
                        ReimbursingBank = dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() + dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString();
                    }
                    if (ReimbursingBank != "" && dt.Rows[0]["Reimbur_Bank_IdentCode_754"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + "#" + dt.Rows[0]["Reimbur_Bank_IdentCode_754"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Reimbur_Bank_IdentCode_754"].ToString() != "")
                        {
                            ReimbursingBank = dt.Rows[0]["Reimbur_Bank_IdentCode_754"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "B")
                {
                    if (dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() != "" || dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString() != "")
                    {
                        ReimbursingBank = dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() + dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString();
                    }
                    if (ReimbursingBank != "" && dt.Rows[0]["Reimbur_Bank_Lctn_754"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + "#" + dt.Rows[0]["Reimbur_Bank_Lctn_754"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Reimbur_Bank_Lctn_754"].ToString() != "")
                        {
                            ReimbursingBank = dt.Rows[0]["Reimbur_Bank_Lctn_754"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "D")
                {
                    ReimbursingBank = dt.Rows[0]["Reimbursing_Bank_Accno_754"].ToString() + dt.Rows[0]["Reimbur_Bank_PartyIdent_754"].ToString();
                    if (dt.Rows[0]["Reimbursing_Bank_Name_754"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + '#' + dt.Rows[0]["Reimbursing_Bank_Name_754"].ToString();
                    }
                    if (dt.Rows[0]["Reimbur_Bank_Addrs1_754"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + ',' + dt.Rows[0]["Reimbur_Bank_Addrs1_754"].ToString();
                    }
                    if (dt.Rows[0]["Reimbur_Bank_Addrs2_754"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + ',' + dt.Rows[0]["Reimbur_Bank_Addrs2_754"].ToString();
                    }
                    if (dt.Rows[0]["Reimbur_Bank_Addrs3_754"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + ',' + dt.Rows[0]["Reimbur_Bank_Addrs3_754"].ToString();
                    }
                }
                if (ReimbursingBank.Length != 0)
                {
                    comma = ReimbursingBank.Trim().Substring(0, 1);
                    if (comma == ",") { ReimbursingBank = ReimbursingBank.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Account With Bank
                string AccountWithBank = "";
                if (dt.Rows[0]["Account_With_Bank"].ToString() == "A")
                {
                    if (dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() != "" || dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() + dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["Acnt_With_Bank_IdentCode_754"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["Acnt_With_Bank_IdentCode_754"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Acnt_With_Bank_IdentCode_754"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["Acnt_With_Bank_IdentCode_754"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["Account_With_Bank"].ToString() == "B")
                {
                    if (dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() != "" || dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() + dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["Acnt_With_Bank_Loctn_754"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["Acnt_With_Bank_Loctn_754"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Acnt_With_Bank_Loctn_754"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["Acnt_With_Bank_Loctn_754"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["Account_With_Bank"].ToString() == "D")
                {
                    AccountWithBank = dt.Rows[0]["Acnt_With_Bank_Acc_754"].ToString() + dt.Rows[0]["Acnt_with_Bank_PartyIdent_754"].ToString();
                    if (dt.Rows[0]["Acnt_With_Bank_Name"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + '#' + dt.Rows[0]["Acnt_With_Bank_Name"].ToString();
                    }
                    if (dt.Rows[0]["Acnt_With_Bank_Addr1_754"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["Acnt_With_Bank_Addr1_754"].ToString();
                    }
                    if (dt.Rows[0]["Acnt_With_Bank_Addr2_754"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["Acnt_With_Bank_Addr2_754"].ToString();
                    }
                    if (dt.Rows[0]["Acnt_With_Bank_Addr3_754"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["Acnt_With_Bank_Addr3_754"].ToString();
                    }
                }
                if (AccountWithBank.Length != 0)
                {
                    comma = AccountWithBank.Trim().Substring(0, 1);
                    if (comma == ",") { AccountWithBank = AccountWithBank.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Benificiary Bank
                string BanificiaryBank = "";
                if (dt.Rows[0]["Banificiary_Bank_754"].ToString() == "A")
                {
                    if (dt.Rows[0]["Banificiary_AccNo_754"].ToString() != "" || dt.Rows[0]["Banificiary_PartyIdent_754"].ToString() != "")
                    {
                        BanificiaryBank = dt.Rows[0]["Banificiary_AccNo_754"].ToString() + dt.Rows[0]["Banificiary_PartyIdent_754"].ToString();
                    }
                    if (BanificiaryBank != "" && dt.Rows[0]["Banificiary_IdentCode_754"].ToString() != "")
                    {
                        BanificiaryBank = BanificiaryBank + "#" + dt.Rows[0]["Banificiary_IdentCode_754"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["Banificiary_IdentCode_754"].ToString() != "")
                        {
                            BanificiaryBank = dt.Rows[0]["Banificiary_IdentCode_754"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["Banificiary_Bank_754"].ToString() == "D")
                {
                    BanificiaryBank = dt.Rows[0]["Banificiary_AccNo_754"].ToString() + dt.Rows[0]["Banificiary_PartyIdent_754"].ToString();
                    if (dt.Rows[0]["Banificiary_Bank_Name_754"].ToString() != "")
                    {
                        BanificiaryBank = BanificiaryBank + '#' + dt.Rows[0]["Banificiary_Bank_Name_754"].ToString();
                    }
                    if (dt.Rows[0]["Banificiary_Addr1_754"].ToString() != "")
                    {
                        BanificiaryBank = BanificiaryBank + ',' + dt.Rows[0]["Banificiary_Addr1_754"].ToString();
                    }
                    if (dt.Rows[0]["Banificiary_Addr2_754"].ToString() != "")
                    {
                        BanificiaryBank = BanificiaryBank + ',' + dt.Rows[0]["Banificiary_Addr2_754"].ToString();
                    }
                    if (dt.Rows[0]["Banificiary_Addr3_754"].ToString() != "")
                    {
                        BanificiaryBank = BanificiaryBank + ',' + dt.Rows[0]["Banificiary_Addr3_754"].ToString();
                    }
                }
                if (BanificiaryBank.Length != 0)
                {
                    comma = BanificiaryBank.Trim().Substring(0, 1);
                    if (comma == ",") { BanificiaryBank = BanificiaryBank.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Sender To Receiver Information
                string SendertoReceiverInformation = "";
                if (dt.Rows[0]["SendertoReceiverInformation_754"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SendertoReceiverInformation_754"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation2_754"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation2_754"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation3_754"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation3_754"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation4_754"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation4_754"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation5_754"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation5_754"].ToString().ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SendertoReceiverInformation6_754"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SendertoReceiverInformation6_754"].ToString().ToString().Replace("/", "");
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == ",") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Narrative
                string Narrative = "";
                if (dt.Rows[0]["Narrative754_1"].ToString() != "")
                {
                    Narrative = dt.Rows[0]["Narrative754_1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_27"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative754_35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative754_35"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<SendersReference>" + dt.Rows[0]["Sender_Ref_754"].ToString() + "</SendersReference>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related_Ref_754"].ToString() + "</RelatedReference>");
                if (dt.Rows[0]["Pri_Amt_Paid_754"].ToString() == "A")
                {
                    sw.WriteLine("<PrincipalAmountA>" + PrincipalAmountPaid + "</PrincipalAmountA>");  //vinay swift changes
                }
                if (dt.Rows[0]["Pri_Amt_Paid_754"].ToString() == "B")
                {
                    sw.WriteLine("<PrincipalAmountB>" + PrincipalAmountPaid + "</PrincipalAmountB>"); //vinay swift changes
                }
                sw.WriteLine("<AdditionalAmounts>" + AdditionalAmounts + "</AdditionalAmounts>");
                sw.WriteLine("<ChargesDeducted>" + Chargesdeducted + "</ChargesDeducted>");
                sw.WriteLine("<ChargesAdded>" + ChargesAdded + "</ChargesAdded>");
                if (dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString() == "A")
                {
                    sw.WriteLine("<TotalAmountClaimedA>" + TotalAmountClam + "</TotalAmountClaimedA>");
                }
                if (dt.Rows[0]["Ttl_Amt_Clmd_754"].ToString() == "B")
                {
                    sw.WriteLine("<TotalAmountClaimedB>" + TotalAmountClam + "</TotalAmountClaimedB>");
                }
                if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "A")
                {
                    sw.WriteLine("<ReimbursingBankA>" + ReimbursingBank + "</ReimbursingBankA>");
                }
                if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "B")
                {
                    sw.WriteLine("<ReimbursingBankB>" + ReimbursingBank + "</ReimbursingBankB>");
                }

                if (dt.Rows[0]["Reimbursing_Bank_754"].ToString() == "D")
                {
                    sw.WriteLine("<ReimbursingBankD>" + ReimbursingBank + "</ReimbursingBankD>");
                }
                if (dt.Rows[0]["Account_With_Bank"].ToString() == "A")
                {
                    sw.WriteLine("<AccountWithBankA>" + AccountWithBank + "</AccountWithBankA>");
                }
                if (dt.Rows[0]["Account_With_Bank"].ToString() == "B")
                {
                    sw.WriteLine("<AccountWithBankB>" + AccountWithBank + "</AccountWithBankB>");
                }

                if (dt.Rows[0]["Account_With_Bank"].ToString() == "D")
                {
                    sw.WriteLine("<AccountWithBankD>" + AccountWithBank + "</AccountWithBankD>");
                }
                if (dt.Rows[0]["Banificiary_Bank_754"].ToString() == "A")
                {
                    sw.WriteLine("<BeneficiaryBankA>" + BanificiaryBank + "</BeneficiaryBankA>");
                }
                if (dt.Rows[0]["Banificiary_Bank_754"].ToString() == "D")
                {
                    sw.WriteLine("<BeneficiaryBankD>" + BanificiaryBank + "</BeneficiaryBankD>");
                }
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }   //swift change
    //------------------------------------------end-------------------------------------------------------------
    //---Nilesh---//
    protected void ddlPrinAmtPaidAccNego_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlPrinAmtPaidAccNego_754.SelectedValue == "A")
        {
            txtPrinAmtPaidAccNegoDate_754.Visible = true;
            lbl_rinAmtPaidAccNegoDate_754.Visible = true;
        }
        else
        {
            txtPrinAmtPaidAccNegoDate_754.Visible = false;
            txtPrinAmtPaidAccNegoDate_754.Text = "";
            lbl_rinAmtPaidAccNegoDate_754.Visible = false;
        }
    }
    protected void ddlTotalAmtclamd_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlTotalAmtclamd_754.SelectedValue == "A")
        {
            txt_754_TotalAmtClmd_Date.Visible = true;
            lbl_754_TotalAmtClmd_Date.Visible = true;
        }
        else
        {
            txt_754_TotalAmtClmd_Date.Visible = false;
            txt_754_TotalAmtClmd_Date.Text = "";
            lbl_754_TotalAmtClmd_Date.Visible = false;
        }
    }
    protected void ddlReimbursingbank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlReimbursingbank_754.SelectedValue == "A")
        {
            lblReimbursingBankIdentifier_754.Text = "Identifier Code: ";
            txtReimbursingBankIdentifiercode_754.Visible = true;
            txtReimbursingBankIdentifiercode_754.Focus();
            txtReimbursingBankLocation_754.Visible = false;
            txtReimbursingBankLocation_754.Text = "";
            txtReimbursingBankName_754.Visible = false;
            txtReimbursingBankName_754.Text = "";

            lblReimbursingBankAddress1_754.Visible = false;
            lblReimbursingBankAddress2_754.Visible = false;
            lblReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress1_754.Visible = false;
            txtReimbursingBankAddress1_754.Text = "";
            txtReimbursingBankAddress2_754.Visible = false;
            txtReimbursingBankAddress2_754.Text = "";
            txtReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress3_754.Text = "";
        }
        else if (ddlReimbursingbank_754.SelectedValue == "B")
        {
            lblReimbursingBankIdentifier_754.Text = "Location :";
            txtReimbursingBankIdentifiercode_754.Visible = false;
            txtReimbursingBankIdentifiercode_754.Text = "";
            txtReimbursingBankLocation_754.Visible = true;
            txtReimbursingBankLocation_754.Focus();
            txtReimbursingBankName_754.Visible = false;
            txtReimbursingBankName_754.Text = "";
            lblReimbursingBankAddress1_754.Visible = false;
            lblReimbursingBankAddress2_754.Visible = false;
            lblReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress1_754.Visible = false;
            txtReimbursingBankAddress1_754.Text = "";
            txtReimbursingBankAddress2_754.Visible = false;
            txtReimbursingBankAddress2_754.Text = "";
            txtReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress3_754.Text = "";
        }
        else //if (ddlReimbursingbank_754.SelectedValue == "B")
        {
            lblReimbursingBankIdentifier_754.Text = "Name : ";
            txtReimbursingBankIdentifiercode_754.Visible = false;
            txtReimbursingBankIdentifiercode_754.Text = "";
            txtReimbursingBankLocation_754.Visible = false;
            txtReimbursingBankLocation_754.Text = "";
            txtReimbursingBankName_754.Visible = true;
            txtReimbursingBankName_754.Focus();

            lblReimbursingBankAddress1_754.Visible = true;
            lblReimbursingBankAddress2_754.Visible = true;
            lblReimbursingBankAddress3_754.Visible = true;
            txtReimbursingBankAddress1_754.Visible = true;
            txtReimbursingBankAddress2_754.Visible = true;
            txtReimbursingBankAddress3_754.Visible = true;
        }
    }
    protected void ddlBeneficiarybank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiarybank_754.SelectedValue == "A")
        {
            lblBeneficiaryBankIdentifier_754.Text = "Identifier Code: ";
            txtBeneficiaryBankIdentifiercode_754.Visible = true;
            txtBeneficiaryBankIdentifiercode_754.Focus();
            txtBeneficiaryBankName_754.Visible = false;
            txtBeneficiaryBankName_754.Text = "";
            lblBeneficiaryBankAddress1_754.Visible = false;
            lblBeneficiaryBankAddress2_754.Visible = false;
            lblBeneficiaryBankAddress3_754.Visible = false;
            txtBeneficiaryBankAddress1_754.Visible = false;
            txtBeneficiaryBankAddress1_754.Text = "";
            txtBeneficiaryBankAddress2_754.Visible = false;
            txtBeneficiaryBankAddress2_754.Text = "";
            txtBeneficiaryBankAddress3_754.Visible = false;
            txtBeneficiaryBankAddress3_754.Text = "";
        }
        else
        {
            lblBeneficiaryBankIdentifier_754.Text = "Name: ";
            txtBeneficiaryBankIdentifiercode_754.Visible = false;
            txtBeneficiaryBankIdentifiercode_754.Text = "";
            txtBeneficiaryBankName_754.Visible = true;
            txtBeneficiaryBankName_754.Focus();
            lblBeneficiaryBankAddress1_754.Visible = true;
            lblBeneficiaryBankAddress2_754.Visible = true;
            lblBeneficiaryBankAddress3_754.Visible = true;
            txtBeneficiaryBankAddress1_754.Visible = true;
            txtBeneficiaryBankAddress2_754.Visible = true;
            txtBeneficiaryBankAddress3_754.Visible = true;
        }
    }
    protected void ddlAccountwithbank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithbank_754.SelectedValue == "A")
        {
            lblAccountwithBankIdentifier_754.Text = "Identifier Code: ";
            txtAccountwithBankIdentifiercode_754.Visible = true;
            txtAccountwithBankIdentifiercode_754.Focus();
            txtAccountwithBankLocation_754.Visible = false;
            txtAccountwithBankLocation_754.Text = "";
            txtAccountwithBankName_754.Visible = false;
            txtAccountwithBankName_754.Text = "";

            lblAccountwithBankAddress1_754.Visible = false;
            lblAccountwithBankAddress2_754.Visible = false;
            lblAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress1_754.Visible = false;
            txtAccountwithBankAddress1_754.Text = "";
            txtAccountwithBankAddress2_754.Visible = false;
            txtAccountwithBankAddress2_754.Text = "";
            txtAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress3_754.Text = "";
        }
        else if (ddlAccountwithbank_754.SelectedValue == "B")
        {
            lblAccountwithBankIdentifier_754.Text = "Location :";
            txtAccountwithBankIdentifiercode_754.Visible = false;
            txtAccountwithBankIdentifiercode_754.Text = "";
            txtAccountwithBankLocation_754.Visible = true;
            txtAccountwithBankLocation_754.Focus();
            txtAccountwithBankName_754.Visible = false;
            txtAccountwithBankName_754.Text = "";
            lblAccountwithBankAddress1_754.Visible = false;
            lblAccountwithBankAddress2_754.Visible = false;
            lblAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress1_754.Visible = false;
            txtAccountwithBankAddress1_754.Text = "";
            txtAccountwithBankAddress2_754.Visible = false;
            txtAccountwithBankAddress2_754.Text = "";
            txtAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress3_754.Text = "";
        }
        else //if (ddlAccountwithbank_754.SelectedValue == "D")
        {
            lblAccountwithBankIdentifier_754.Text = "Name : ";
            txtAccountwithBankIdentifiercode_754.Visible = false;
            txtAccountwithBankIdentifiercode_754.Text = "";
            txtAccountwithBankLocation_754.Visible = false;
            txtAccountwithBankLocation_754.Text = "";
            txtAccountwithBankName_754.Visible = true;

            lblAccountwithBankAddress1_754.Visible = true;
            lblAccountwithBankAddress2_754.Visible = true;
            lblAccountwithBankAddress3_754.Visible = true;
            txtAccountwithBankAddress1_754.Visible = true;
            txtAccountwithBankAddress2_754.Visible = true;
            txtAccountwithBankAddress3_754.Visible = true;
        }
    }
    //---Nilesh END----//
    protected void InsertDataForSwiftSTP(string SwiftType)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@SwiftType", SwiftType);
        string result = obj.SaveDeleteData("TF_SWIFT_STP_InsertSettlementData", P1, P2);
    }
    protected void DeleteDataForSwiftSTP()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string result = obj.SaveDeleteData("TF_SWIFT_STP_DeleteSettlementData", P1);
    }

    //----------------Added by bhupen on 30112022-----------------------------------//
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
                //if (lblCollection_Lodgment_UnderLC.Text.Trim() == "Settlment_Collection")
                if (hdnDocType.Value.Trim() == "ACC")
                {
                    SqlParameter CustAcno = new SqlParameter("@CustAcno", txt_UnderLC_Customer_ID.Text.Trim());
                    SqlParameter Drawerno = new SqlParameter("@Drawee", hdnDrawerno.Value);
                    DataTable dt1 = new DataTable();
                    dt1 = obj.getData("TF_IMP_GetDraweedetails_acceptance", CustAcno, Drawerno);
                    if (dt1.Rows.Count > 0)
                    {
                        hdnDrawer.Value = dt1.Rows[0]["Drawer_NAME"].ToString();
                    }
                }
                else
                {
                    SqlParameter CustAcno1 = new SqlParameter("@CustAcno", txt_Collection_Customer_ID.Text.Trim());
                    SqlParameter Drawerno1 = new SqlParameter("@Drawee", hdnDrawerno.Value);
                    DataTable dt1 = new DataTable();
                    dt1 = obj.getData("TF_IMP_GetDraweedetails_acceptance", CustAcno1, Drawerno1);
                    if (dt1.Rows.Count > 0)
                    {
                        hdnDrawer.Value = dt1.Rows[0]["Drawer_NAME"].ToString();
                    }
                }
            }
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
                    string Amount = lblBillAmt.Text.Trim().Replace(",", "");
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
                            //if (lblCollection_Lodgment_UnderLC.Text.Trim() == "Settlment_Collection")
                            if (hdnDocType.Value.Trim() == "ACC")
                            {
                                btn_Verify_UnderLC.Visible = true;
                                btn_Verify_Collection.Visible = false;
                            }
                            else
                            {
                                btn_Verify_UnderLC.Visible = false;
                                btn_Verify_Collection.Visible = true;
                            }
                        }
                        else
                        {
                            lbl_LEIAmt_Check.Text = "Transaction Bill Amt is less than LEI Thresold Limit.";
                            lbl_LEIAmt_Check.ForeColor = System.Drawing.Color.Green;
                            lbl_LEIAmt_Check.Font.Size = 10;
                            hdnLeiFlag.Value = "N";
                            ddlApproveReject.Enabled = true;
                            btn_Verify_UnderLC.Visible = false;
                            btn_Verify_Collection.Visible = false;
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

    private void Check_LEINODetails_Collection()
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
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
                else
                {
                    lblLEI_Remark.Text = "...Verified.";
                    hdnleino.Value = dt.Rows[0]["LEI_No"].ToString();
                    ddlApproveReject.Enabled = true;
                    ddlApproveReject.Visible = true;
                }
            }
            else
            {
                lblLEI_Remark.Text = "...Not Verified.";
                lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleino.Value = "";
                ddlApproveReject.Enabled = false;
                ddlApproveReject.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_LEINO_ExpirydateDetails_Collection()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.ToString().Trim());
            //SqlParameter p2 = new SqlParameter("@DueDate", hdnDuedate_Collection.Value.Trim());
            SqlParameter p2 = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim()); 
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.Trim());

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
                    ddlApproveReject.Enabled = true;
                    ddlApproveReject.Visible = true;
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark.Text = "...Not Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "Y";
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
            }
            else
            {
                lblLEIExpiry_Remark.Text = "...Not Verified.";
                lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleiexpiry.Value = "";
                hdnleiexpiry1.Value = "";
                ddlApproveReject.Enabled = false;
                ddlApproveReject.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_DraweeLEINODetails_Collection()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", txt_Collection_Customer_ID.Text.Trim());
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
                    if (hdnDrawercountry.Value == "IN")
                    {
                        ddlApproveReject.Enabled = false;
                        ddlApproveReject.Visible = false;
                    }
                }
                else
                {
                    lblLEI_Remark_Drawee.Text = "...Verified.";
                    hdnDraweeleino.Value = dt.Rows[0]["Drawer_LEI_NO"].ToString();
                    if (lblLEI_Remark.Text.Trim() == "...Verified." && lblLEIExpiry_Remark.Text.Trim() == "...Verified.")
                    {
                        ddlApproveReject.Enabled = true;
                        ddlApproveReject.Visible = true;
                    }
                }
            }
            else
            {
                lblLEI_Remark_Drawee.Text = "...Not Verified.";
                lblLEI_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleino.Value = "";
                if (hdnDrawercountry.Value == "IN")
                {
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_DraweeLEINO_ExpirydateDetails_Collection()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", txt_Collection_Customer_ID.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Draweename", hdnDrawer.Value.ToString().Trim());
            //SqlParameter p3 = new SqlParameter("@DueDate", hdnDuedate_Collection.Value.Trim());
            SqlParameter p3 = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim()); 
            SqlParameter p4 = new SqlParameter("@BranchCode", hdnBranchCode.Value.Trim());

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
                    if (lblLEI_Remark.Text.Trim() == "...Verified." && lblLEIExpiry_Remark.Text.Trim() == "...Verified.")
                    {
                        ddlApproveReject.Enabled = true;
                        ddlApproveReject.Visible = true;
                    }
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                    lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Green;
                    hdnDraweeleiexpiry.Value = dt.Rows[0]["Drawer_LEIExpiryDate"].ToString();
                    hdnDraweeleiexpiry1.Value = "Y";
                    if (hdnDrawercountry.Value == "IN")
                    {
                        ddlApproveReject.Enabled = false;
                        ddlApproveReject.Visible = false;
                    }
                }
            }
            else
            {
                lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleiexpiry.Value = "";
                hdnDraweeleiexpiry1.Value = "";
                if (hdnDrawercountry.Value == "IN")
                {
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btn_Verify_Collection_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_Collection_Customer_ID.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please Select Customer for Verifying LEI details.')", true);
                txt_Collection_Customer_ID.Focus();
            }
            else if (lblBillAmt.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Amount for Verifying LEI details.')", true);
                lblBillAmt.Focus();
            }
            else if (hdnDuedate_Collection.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('There is no Due date for Verifying LEI details.')", true);
            }
            else
            {
                ddlApproveReject.Enabled = false;
                GetDrawee_Details_LEI();
                Check_LEINODetails_Collection();
                Check_LEINO_ExpirydateDetails_Collection();
                Check_DraweeLEINODetails_Collection();
                Check_DraweeLEINO_ExpirydateDetails_Collection();

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

    private void Check_LEINODetails_LC()
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
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
                else
                {
                    lblLEI_Remark.Text = "...Verified.";
                    hdnleino.Value = dt.Rows[0]["LEI_No"].ToString();
                    ddlApproveReject.Enabled = true;
                    ddlApproveReject.Visible = true;
                }
            }
            else
            {
                lblLEI_Remark.Text = "...Not Verified.";
                lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleino.Value = "";
                ddlApproveReject.Enabled = false;
                ddlApproveReject.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_LEINO_ExpirydateDetails_LC()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.ToString().Trim());
            //SqlParameter p2 = new SqlParameter("@DueDate", txt_UnderLC_Interest_To.Text.ToString().Trim());
            SqlParameter p2 = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim()); 
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
                    ddlApproveReject.Enabled = true;
                    ddlApproveReject.Visible = true;
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark.Text = "...Not Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "Y";
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
            }
            else
            {
                lblLEIExpiry_Remark.Text = "...Not Verified.";
                lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleiexpiry.Value = "";
                hdnleiexpiry1.Value = "";
                ddlApproveReject.Enabled = false;
                ddlApproveReject.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_DraweeLEINODetails_LC()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", txt_UnderLC_Customer_ID.Text.Trim());
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
                    if (hdnDrawercountry.Value == "IN")
                    {
                        ddlApproveReject.Enabled = false;
                        ddlApproveReject.Visible = false;
                    }
                }
                else
                {
                    lblLEI_Remark_Drawee.Text = "...Verified.";
                    hdnDraweeleino.Value = dt.Rows[0]["Drawer_LEI_NO"].ToString();
                    if (lblLEI_Remark.Text.Trim() == "...Verified." && lblLEIExpiry_Remark.Text.Trim() == "...Verified.")
                    {
                        ddlApproveReject.Enabled = true;
                        ddlApproveReject.Visible = true;
                    }
                }
            }
            else
            {
                lblLEI_Remark_Drawee.Text = "...Not Verified.";
                lblLEI_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleino.Value = "";
                if (hdnDrawercountry.Value == "IN")
                {
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_DraweeLEINO_ExpirydateDetails_LC()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", txt_UnderLC_Customer_ID.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Draweename", hdnDrawer.Value.Trim());
            //SqlParameter p3 = new SqlParameter("@DueDate", txt_UnderLC_Interest_To.Text.ToString().Trim());
            SqlParameter p3 = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim()); 
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
                    if (lblLEI_Remark.Text.Trim() == "...Verified." && lblLEIExpiry_Remark.Text.Trim() == "...Verified.")
                    {
                        ddlApproveReject.Enabled = true;
                        ddlApproveReject.Visible = true;
                    }
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                    lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Green;
                    hdnDraweeleiexpiry.Value = dt.Rows[0]["Drawer_LEIExpiryDate"].ToString();
                    hdnDraweeleiexpiry1.Value = "Y";
                    if (hdnDrawercountry.Value == "IN")
                    {
                        ddlApproveReject.Enabled = false;
                        ddlApproveReject.Visible = false;
                    }
                }
            }
            else
            {
                lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleiexpiry.Value = "";
                hdnDraweeleiexpiry1.Value = "";
                if (hdnDrawercountry.Value == "IN")
                {
                    ddlApproveReject.Enabled = false;
                    ddlApproveReject.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btn_Verify_UnderLC_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_UnderLC_Customer_ID.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please Select Customer for Verifying LEI details.')", true);
                txt_UnderLC_Customer_ID.Focus();
            }
            else if (lblBillAmt.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Amount for Verifying LEI details.')", true);
                lblBillAmt.Focus();
            }
            else if (txt_UnderLC_Interest_To.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('There is no Due date for Verifying LEI details.')", true);
                txt_UnderLC_Interest_To.Focus();
            }
            else
            {
                ddlApproveReject.Enabled = false;
                GetDrawee_Details_LEI();
                Check_LEINODetails_LC();
                Check_LEINO_ExpirydateDetails_LC();
                Check_DraweeLEINODetails_LC();
                Check_DraweeLEINO_ExpirydateDetails_LC();

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

    private void Lei_Audit_Collection()
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
                SqlParameter P_CustAcno = new SqlParameter("@CustAcNo", txt_Collection_Customer_ID.Text.Trim());
                SqlParameter P_Cust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
                SqlParameter P_Cust_Name = new SqlParameter("@Cust_Name", lbl_Collection_Customer_Name.Text.Trim());
                SqlParameter P_Cust_LEI = new SqlParameter("@Cust_LEI", hdnleino.Value.Trim());
                SqlParameter P_Cust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdnleiexpiry.Value.Trim());
                SqlParameter P_Cust_LEI_Expired = new SqlParameter("@Cust_LEI_Expired", hdnleiexpiry1.Value.Trim());
                SqlParameter P_Drawee_Name = new SqlParameter("@Drawee_Name", hdnDrawer.Value.Trim());
                SqlParameter P_Drawee_LEI = new SqlParameter("@Drawee_LEI", hdnDraweeleino.Value.Trim());
                SqlParameter P_Drawee_LEI_Expiry = new SqlParameter("@Drawee_LEI_Expiry", hdnDraweeleiexpiry.Value.Trim());
                SqlParameter P_Drawee_LEI_Expired = new SqlParameter("@Drawee_LEI_Expired", hdnDraweeleiexpiry1.Value.Trim());
                SqlParameter P_BillAmount = new SqlParameter("@BillAmt", lblBillAmt.Text.Trim().Replace(",", ""));
                SqlParameter P_txtCurr = new SqlParameter("@Curr", lblDoc_Curr.Text.Trim());
                SqlParameter P_Exchrate = new SqlParameter("@ExchRt", lbl_Exch_rate.Text.Trim());
                SqlParameter P_LodgementDate = new SqlParameter("@LodgDate", txtValueDate.Text.Trim());
                //SqlParameter P_DueDate = new SqlParameter("@DueDate", hdnDuedate_Collection.Value.Trim());
                SqlParameter P_DueDate = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim());
                SqlParameter P_LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value.Trim());
                SqlParameter P_CustLEI_Flag = new SqlParameter("@Cust_LEIFlag", hdncustleiflag.Value.Trim());
                SqlParameter P_Status = new SqlParameter("@status", ddlApproveReject.SelectedItem.Text.Trim());
                SqlParameter P_Module = new SqlParameter("@Module", "S");
                SqlParameter P_AddedBy = new SqlParameter("@user", hdnUserName.Value.Trim());
                SqlParameter P_AddedDate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                string _queryLEI = "TF_IMP_AddUpdate_LEITransaction";

                string _result = obj.SaveDeleteData(_queryLEI, P_BranchCode, P_DocNo, P_DocType, P_Sight_Usance, P_CustAcno, P_Cust_Abbr, P_Cust_Name, P_Cust_LEI,
                   P_Cust_LEI_Expiry, P_Cust_LEI_Expired, P_Drawee_Name, P_Drawee_LEI, P_Drawee_LEI_Expiry, P_Drawee_LEI_Expired, P_BillAmount, P_txtCurr, P_Exchrate,
                   P_LodgementDate, P_DueDate, P_LEI_Flag, P_CustLEI_Flag, P_Status, P_Module, P_AddedBy, P_AddedDate);
            }
            catch (Exception ex)
            {
                //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
            }
        }
    }
    private void Lei_Audit_LC()
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
                SqlParameter P_CustAcno = new SqlParameter("@CustAcNo", txt_UnderLC_Customer_ID.Text.Trim());
                SqlParameter P_Cust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
                SqlParameter P_Cust_Name = new SqlParameter("@Cust_Name", lbl_UnderLC_Customer_Name.Text.Trim());
                SqlParameter P_Cust_LEI = new SqlParameter("@Cust_LEI", hdnleino.Value.Trim());
                SqlParameter P_Cust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdnleiexpiry.Value.Trim());
                SqlParameter P_Cust_LEI_Expired = new SqlParameter("@Cust_LEI_Expired", hdnleiexpiry1.Value.Trim());
                SqlParameter P_Drawee_Name = new SqlParameter("@Drawee_Name", hdnDrawer.Value.Trim());
                SqlParameter P_Drawee_LEI = new SqlParameter("@Drawee_LEI", hdnDraweeleino.Value.Trim());
                SqlParameter P_Drawee_LEI_Expiry = new SqlParameter("@Drawee_LEI_Expiry", hdnDraweeleiexpiry.Value.Trim());
                SqlParameter P_Drawee_LEI_Expired = new SqlParameter("@Drawee_LEI_Expired", hdnDraweeleiexpiry1.Value.Trim());
                SqlParameter P_BillAmount = new SqlParameter("@BillAmt", lblBillAmt.Text.Trim().Replace(",", ""));
                SqlParameter P_txtCurr = new SqlParameter("@Curr", lblDoc_Curr.Text.Trim());
                SqlParameter P_Exchrate = new SqlParameter("@ExchRt", lbl_Exch_rate.Text.Trim());
                SqlParameter P_LodgementDate = new SqlParameter("@LodgDate", txtValueDate.Text.Trim());
                //SqlParameter P_DueDate = new SqlParameter("@DueDate", txt_UnderLC_Interest_To.Text.Trim());txtValueDate.Text.ToString().Trim()
                SqlParameter P_DueDate = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim());
                SqlParameter P_LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value.Trim());
                SqlParameter P_CustLEI_Flag = new SqlParameter("@Cust_LEIFlag", hdncustleiflag.Value.Trim());
                SqlParameter P_Status = new SqlParameter("@status", ddlApproveReject.SelectedItem.Text.Trim());
                SqlParameter P_Module = new SqlParameter("@Module", "S");
                SqlParameter P_AddedBy = new SqlParameter("@user", hdnUserName.Value.Trim());
                SqlParameter P_AddedDate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                string _queryLEI = "TF_IMP_AddUpdate_LEITransaction";

                string _result = obj.SaveDeleteData(_queryLEI, P_BranchCode, P_DocNo, P_DocType, P_Sight_Usance, P_CustAcno, P_Cust_Abbr, P_Cust_Name, P_Cust_LEI,
                   P_Cust_LEI_Expiry, P_Cust_LEI_Expired, P_Drawee_Name, P_Drawee_LEI, P_Drawee_LEI_Expiry, P_Drawee_LEI_Expired, P_BillAmount, P_txtCurr, P_Exchrate,
                   P_LodgementDate, P_DueDate, P_LEI_Flag, P_CustLEI_Flag, P_Status, P_Module, P_AddedBy, P_AddedDate);
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
                //hdncustleiflag.Value = dt.Rows[0]["Cust_Lei_Flag"].ToString();
                //if (hdncustleiflag.Value == "R")
                //{
                    if (hdnDocType.Value.Trim() == "ACC")
                    {
                        ddlApproveReject.Enabled = false;
                        btn_Verify_UnderLC.Visible = true;
                        btn_Verify_Collection.Visible = false;
                        ReccuringLEI.Visible = true;
                        ReccuringLEI.Text = "This is Recurring LEI Customer.";
                        ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                        ReccuringLEI.Font.Size = 10;
                    }
                    else
                    {
                        ddlApproveReject.Enabled = false;
                        btn_Verify_UnderLC.Visible = false;
                        btn_Verify_Collection.Visible = true;
                        ReccuringLEI.Visible = true;
                        ReccuringLEI.Text = "This is Recurring LEI Customer.";
                        ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                        ReccuringLEI.Font.Size = 10;
                    }
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
            SqlParameter p2 = new SqlParameter("@Module", "S");
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
            SqlParameter p2 = new SqlParameter("@Module", "S");
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
  //-------------------------------------End------------------------------------------//
}

   
