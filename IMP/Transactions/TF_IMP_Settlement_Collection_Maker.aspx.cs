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

public partial class IMP_Transactions_TF_IMP_Settlement_Collection_Maker : System.Web.UI.Page
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
            Get_DiscrepencyCharges();
            if (!IsPostBack)
            {
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_Settlement_Maker_View.aspx", true);
                }
                else
                {
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    hdnflcIlcType.Value = Request.QueryString["FLC_ILC"].Trim();
                    hdnDoc_Scrutiny.Value = Request.QueryString["Doc_Scrutiny"].Trim();

                    Toggel_DocType(hdnDocType.Value, hdnBranchName.Value, hdnflcIlcType.Value, hdnDoc_Scrutiny.Value);

                    TF_DATA obj = new TF_DATA();
                    SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
                    string result = "";
                    result = obj.SaveDeleteData("TF_IMP_Check_Ref_In_Settlement", PDocNo);
                    if (result == "exists")
                    {
                        hdnMode.Value = "Edit";
                    }
                    else
                    {
                        hdnMode.Value = "Add";
                    }
                    txt200Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txt103Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    if (hdnMode.Value == "Edit")
                    {
                        Fill_Logd_Details();
                        FillSettlementDetails();
                        Check_LEINO_ExchRateDetails();
                    }
                    else
                    {
                        Fill_Logd_Details();
                        Check_LEINO_ExchRateDetails();
                    }
                    txtPrinAmtPaidAccNegoDate_754.Attributes.Add("onblur", "return isValidDate(" + txtPrinAmtPaidAccNegoDate_754.ClientID + "," + "'[33A] Date'" + " );");
                    txt_754_TotalAmtClmd_Date.Attributes.Add("onblur", "return isValidDate(" + txt_754_TotalAmtClmd_Date.ClientID + "," + "'[34A] Date'" + " );");
                    //Swift change
                    txt200Date.Attributes.Add("onblur", "return isValidDate(" + txt200Date.ClientID + "," + "'[32A] value Date'" + " );");
                    txt103Date.Attributes.Add("onblur", "return isValidDate(" + txt103Date.ClientID + "," + "'[32A] value Date'" + " );");
                }
            }
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
    protected void MakeReadOnlyFor()
    {
        //-----------For All the Other Tansaction-------------------------------------
        txt_Collection_Settlement_for_Cust.Enabled = false;
        txt_Collection_Settlement_For_Bank.Enabled = false;
        txt_Collection_Attn.Enabled = false;
        chk_IMPACC1Flag.Checked = false;
        chk_IMPACC2Flag.Checked = false;
        chk_IMPACC3Flag.Checked = false;
        chk_IMPACC4Flag.Checked = false;
        chk_IMPACC5Flag.Checked = false;

        chk_IMPACC1Flag.Enabled = false;
        chk_IMPACC2Flag.Enabled = false;
        chk_IMPACC3Flag.Enabled = false;
        chk_IMPACC4Flag.Enabled = false;
        chk_IMPACC5Flag.Enabled = false;
        //------------------------For Acc Transaction---------------------------------------
        txt_UnderLC_Comment_Code.Enabled = false;
        txt_UnderLC_Maturity.Enabled = false;
        txt_UnderLC_Settlement_for_Cust.Enabled = false;
        txt_UnderLC_Settlement_For_Bank.Enabled = false;
        txt_UnderLC_Interest_From.Enabled = false;
        txt_UnderLC_Interest_To.Enabled = false;
        txt_UnderLC_No_Of_Days.Enabled = false;
        txt_UnderLC_Discount.Enabled = false;
        txt_UnderLC_Rate.Enabled = false;
        txt_UnderLC_Amount.Enabled = false;
        txt_UnderLC_Overdue_Interestrate.Enabled = false;
        txt_UnderLC_Oveduenoofdays.Enabled = false;
        txt_UnderLC_Overdueamount.Enabled = false;
        txt_UnderLC_Attn.Enabled = false;
        btncal_Maturity_Date.Enabled = false;
        btncal_Interest_From_Date.Enabled = false;
        btncal_Interest_To_Date.Enabled = false;

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
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString();
            hdnLedgerStatusCode.Value = dt.Rows[0]["Status_Code"].ToString();
            if (hdnLedgerStatusCode.Value == "S")
            {
                MakeReadOnlyFor();
            }
            ////////////////////////////// SETTLEMENT(IBD,ACC)//////////////////////////
            Settlement_For_Cust_AccNo.Value = dt.Rows[0]["Settlement_For_Cust_AccNo"].ToString();
            Settlement_For_Cust_Abbr.Value = dt.Rows[0]["Settlement_For_Cust_Abbr"].ToString();
            Settlement_For_Cust_AccCode.Value = dt.Rows[0]["Settlement_For_Cust_AccCode"].ToString();
            Settl_For_Bank_Abbr.Value = dt.Rows[0]["Settl_For_Bank_Abbr"].ToString();
            Settl_ForBank_AccNo.Value = dt.Rows[0]["Settl_ForBank_AccNo"].ToString();
            Settl_ForBank_AccCode.Value = dt.Rows[0]["Settl_ForBank_AccCode"].ToString();
            txt_MT103Receiver.Text = dt.Rows[0]["SwiftCode"].ToString();
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt103Currency.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt103Amount.Text = dt.Rows[0]["Bill_Amount"].ToString();
            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();   //Added by bhupen on 01122022 for LEI
            
            Get_DiscrepencyCharges();
            if (lblDoc_Curr.Text == "INR")
            {
                CalculateR42Amount();
            }
            else
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

            ACC_Their_Comm_Amount.Value = dt.Rows[0]["CR_Their_Comm_Amount"].ToString();
            ACC_Their_Comm_Curr.Value = dt.Rows[0]["CR_Their_Comm_Curr"].ToString();
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

            ddlOrderingCustomer.SelectedValue = "K";
            txtOrderingCustomer_Acc.Text=dt.Rows[0]["CustAcNo"].ToString();
            txtOrderingCustomer_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtOrderingCustomer_Addr1.Text = dt.Rows[0]["CUST_ADDRESS1"].ToString();
            txtOrderingCustomer_Addr2.Text = dt.Rows[0]["CUST_ADDRESS2"].ToString();
            txtOrderingCustomer_Addr3.Text = dt.Rows[0]["CUST_ADDRESS3"].ToString();
            ddlOrderingCustomer_TextChanged(null,null);

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

            if (dt.Rows[0]["BeneficiaryCustomerHeader"].ToString() == "A")
            {
                ddlBeneficiaryCustomer.SelectedValue = "A";
                txtBeneficiaryCustomerAccountNumber.Text = dt.Rows[0]["BeneficiaryCustomerAccno"].ToString();
                txtBeneficiaryCustomerSwiftCode.Text = dt.Rows[0]["BeneficiaryCustomerSwiftCode"].ToString();
                ddlBeneficiaryCustomer_TextChanged(null,null);
            }
            if (dt.Rows[0]["BeneficiaryCustomerHeader"].ToString() == "D")
            {
                ddlBeneficiaryCustomer.SelectedValue = "F";
                txtBeneficiaryCustomerAccountNumber.Text = dt.Rows[0]["BeneficiaryCustomerAccno"].ToString();
                txtBeneficiaryCustomerName.Text = dt.Rows[0]["BeneficiaryCustomerName"].ToString();
                txtBeneficiaryCustomerAddr1.Text = dt.Rows[0]["BeneficiaryCustomerAddress1"].ToString();
                txtBeneficiaryCustomerAddr2.Text = dt.Rows[0]["BeneficiaryCustomerAddress2"].ToString();
                txtBeneficiaryCustomerAddr3.Text = dt.Rows[0]["BeneficiaryCustomerAddress3"].ToString();
                ddlBeneficiaryCustomer_TextChanged(null, null);
            }

            txtRemittanceInformation1.Text = dt.Rows[0]["RemittanceInformation1"].ToString();
            txtRemittanceInformation2.Text = dt.Rows[0]["RemittanceInformation2"].ToString();
            txtRemittanceInformation3.Text = dt.Rows[0]["RemittanceInformation3"].ToString();
            txtRemittanceInformation4.Text = dt.Rows[0]["RemittanceInformation4"].ToString();
            ddlBankOperationCode.SelectedValue = "CRED";

            calculateSwift52aAmountField();

            // MT 202 & 103 changes 02122019
            txtTransactionRefNoR42.Text = "" + dt.Rows[0]["Document_No"].ToString();
            txtRelatedReferenceR42.Text = dt.Rows[0]["Their_Ref_No"].ToString();
            txtValueDateR42.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtCureencyR42.Text = "INR";
            txtOrderingInstitutionIFSCR42.Text = dt.Rows[0]["RTGSCode"].ToString();
            txtBeneficiaryInstitutionIFSCR42.Text = dt.Rows[0]["IFSC_Code"].ToString();
            txtCodeWordR42.Text = "TRF";
            txtAdditionalInformationR42.Text = "";

            string R42_CUST_NAME, R42_Their_Ref_No, R42_Doc_LC_No, R42_Drawer_NAME, R42_Document_No;
            if (("//" + dt.Rows[0]["CUST_NAME"].ToString()).Length > 35)
                R42_CUST_NAME = ("//" + dt.Rows[0]["CUST_NAME"].ToString()).Substring(0, 35);
            else
                R42_CUST_NAME = "//" + dt.Rows[0]["CUST_NAME"].ToString();

            if (("//YOUR REF NO:" + dt.Rows[0]["Their_Ref_No"].ToString()).Length > 35)
                R42_Their_Ref_No = ("//YOUR REF NO:" + dt.Rows[0]["Their_Ref_No"].ToString()).Substring(0, 35);
            else
                R42_Their_Ref_No = "//YOUR REF NO:" + dt.Rows[0]["Their_Ref_No"].ToString();

            if (("//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString()).Length > 35)
                R42_Doc_LC_No = ("//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString()).Substring(0, 35);
            else
                R42_Doc_LC_No = "//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString();

            if (("//" + dt.Rows[0]["Drawer_NAME"].ToString()).Length > 35)
                R42_Drawer_NAME = ("//" + dt.Rows[0]["Drawer_NAME"].ToString()).Substring(0, 35);
            else
                R42_Drawer_NAME = "//" + dt.Rows[0]["Drawer_NAME"].ToString();

            if (("//OUR BILL REF NO:" + dt.Rows[0]["Document_No"].ToString()).Length > 35)
                R42_Document_No = ("//OUR BILL REF NO:" + dt.Rows[0]["Document_No"].ToString()).Substring(0, 35);
            else
                R42_Document_No = "//OUR BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();

            txtMoreInfo1R42.Text = R42_CUST_NAME;//"//" + dt.Rows[0]["CUST_NAME"].ToString();
            txtMoreInfo2R42.Text = R42_Their_Ref_No;//"//YOUR REF NO:" + dt.Rows[0]["Their_Ref_No"].ToString();

            if (dt.Rows[0]["Doc_LC_No"].ToString() != "")
            {
                txtMoreInfo3R42.Text = R42_Doc_LC_No;//"//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString();
                txtMoreInfo4R42.Text = R42_Drawer_NAME;//"//" + dt.Rows[0]["Drawer_NAME"].ToString();
                txtMoreInfo5R42.Text = R42_Document_No;//"//OUR BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();
            }
            else
            {
                txtMoreInfo3R42.Text = R42_Drawer_NAME;//"//" + dt.Rows[0]["Drawer_NAME"].ToString();
                txtMoreInfo4R42.Text = R42_Document_No;//"//OUR BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();
            }

            txtSenderToReceiverInformation2021.Text = "/BNF/IMPORT BILL SETTLEMENT";
            txtSenderToReceiverInformation2022.Text = "//OUR REF NO " + dt.Rows[0]["Document_No"].ToString();
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
            //txtValueDate.Text = dt.Rows[0]["Settlement_Date"].ToString();
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
                //IMP_ACC2_FCRefNo
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
                txt_GO1_Left_Debit_Code.SelectedValue = dt.Rows[0]["GO1_DebitCredit1_Code"].ToString();
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
                txt_GO1_Left_Credit_Code.SelectedValue = dt.Rows[0]["GO1_DebitCredit2_Code"].ToString();
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
                txt_GO1_Right_Debit_Code.SelectedValue = dt.Rows[0]["GO1_DebitCredit3_Code"].ToString();
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
                txt_GO1_Right_Credit_Code.SelectedValue = dt.Rows[0]["GO1_DebitCredit4_Code"].ToString();
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
                txt_GO2_Left_Debit_Code.SelectedValue = dt.Rows[0]["GO2_DebitCredit1_Code"].ToString();
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
                txt_GO2_Left_Credit_Code.SelectedValue = dt.Rows[0]["GO2_DebitCredit2_Code"].ToString();
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
                txt_GO2_Right_Debit_Code.SelectedValue = dt.Rows[0]["GO2_DebitCredit3_Code"].ToString();
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
                txt_GO2_Right_Credit_Code.SelectedValue = dt.Rows[0]["GO2_DebitCredit4_Code"].ToString();
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
                txt_GO3_Left_Debit_Code.SelectedValue = dt.Rows[0]["GO3_DebitCredit1_Code"].ToString();
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
                txt_GO3_Left_Credit_Code.SelectedValue = dt.Rows[0]["GO3_DebitCredit2_Code"].ToString();
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
                txt_GO3_Right_Debit_Code.SelectedValue = dt.Rows[0]["GO3_DebitCredit3_Code"].ToString();
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
                txt_GO3_Right_Credit_Code.SelectedValue = dt.Rows[0]["GO3_DebitCredit4_Code"].ToString();
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
            //txt103Currency.Text = dt.Rows[0]["Currency103"].ToString();
            //txt103Amount.Text = dt.Rows[0]["Amount103"].ToString();
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
            ddlOrderingCustomer_TextChanged(null,null);
            ddlBeneficiaryCustomer.SelectedValue = dt.Rows[0]["BeneficiaryCustomer_header"].ToString();
            txtBeneficiaryCustomerAccountNumber.Text = dt.Rows[0]["BeneficiaryCustomerAccountNumber"].ToString();
            txtBeneficiaryCustomerSwiftCode.Text = dt.Rows[0]["BeneficiaryCustomerSwiftCode"].ToString();
            txtBeneficiaryCustomerName.Text = dt.Rows[0]["BeneficiaryCustomer_Name"].ToString();
            txtBeneficiaryCustomerAddr1.Text = dt.Rows[0]["BeneficiaryCustomer_Addr1"].ToString();
            txtBeneficiaryCustomerAddr2.Text = dt.Rows[0]["BeneficiaryCustomer_Addr2"].ToString();
            txtBeneficiaryCustomerAddr3.Text = dt.Rows[0]["BeneficiaryCustomer_Addr3"].ToString();
            ddlBeneficiaryCustomer_TextChanged(null,null);
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
            //txt200Date.Text = dt.Rows[0]["Date200"].ToString();
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
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
            txtSenderToReceiverInformation2021.Text = dt.Rows[0]["SenderToReceiverInformation2021"].ToString();
            txtSenderToReceiverInformation2022.Text = dt.Rows[0]["SenderToReceiverInformation2022"].ToString();
            txtSenderToReceiverInformation2023.Text = dt.Rows[0]["SenderToReceiverInformation2023"].ToString();
            txtSenderToReceiverInformation2024.Text = dt.Rows[0]["SenderToReceiverInformation2024"].ToString();
            txtSenderToReceiverInformation2025.Text = dt.Rows[0]["SenderToReceiverInformation2025"].ToString();
            txtSenderToReceiverInformation2026.Text = dt.Rows[0]["SenderToReceiverInformation2026"].ToString();
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

        //-----------------------------------------------------------------------Nilesh END--------------------------------------------------------------------------//
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
    [WebMethod]
    public static string AddUpdateSettlement(string hdnUserName, string _BranchName, string _txtDocNo, string _txt_Doc_Value_Date,
        ////////// Bill Document //////////////////////////
        string _txt_Doc_Comment_Code, string _txt_Doc_Maturity, string _txt_Doc_Settlement_for_Cust, string _txt_Doc_Settlement_For_Bank,
        string _txt_Doc_Interest_From, string _txt_Doc_Interest_To, string _txt_Doc_No_Of_Days, string _txt_Doc_Discount,
        string _txt_Doc_InterestRate, string _txt_Doc_InterestAmount,
        string _txt_Doc_Overdue_Interestrate, string _txt_Doc_Oveduenoofdays, string _txt_Doc_Overdueamount, string _txt_Doc_Attn,
        //////////// IMPORT ACCOUNTING 1 //////////////////////////
        string _chk_IMPACC1Flag, string _txt_IMPACC1_FCRefNo,
        string _txt_IMPACC1_DiscAmt, string _txt_IMPACC1_DiscExchRate,
        string _txt_IMPACC1_Princ_matu, string _txt_IMPACC1_Princ_lump, string _txt_IMPACC1_Princ_Contract_no, string _txt_IMPACC1_Princ_Ex_Curr, string _txt_IMPACC1_Princ_Ex_rate, string _txt_IMPACC1_Princ_Intnl_Ex_rate,
        string _txt_IMPACC1_Interest_matu, string _txt_IMPACC1_Interest_lump, string _txt_IMPACC1_Interest_Contract_no, string _txt_IMPACC1_Interest_Ex_Curr, string _txt_IMPACC1_Interest_Ex_rate, string _txt_IMPACC1_Interest_Intnl_Ex_rate,
        string _txt_IMPACC1_Commission_matu, string _txt_IMPACC1_Commission_lump, string _txt_IMPACC1_Commission_Contract_no, string _txt_IMPACC1_Commission_Ex_Curr, string _txt_IMPACC1_Commission_Ex_rate, string _txt_IMPACC1_Commission_Intnl_Ex_rate,
        string _txt_IMPACC1_Their_Commission_matu, string _txt_IMPACC1_Their_Commission_lump, string _txt_IMPACC1_Their_Commission_Contract_no, string _txt_IMPACC1_Their_Commission_Ex_Curr, string _txt_IMPACC1_Their_Commission_Ex_rate, string _txt_IMPACC1_Their_Commission_Intnl_Ex_rate,
        string _txt_IMPACC1_CR_Code, string _txt_IMPACC1_CR_AC_Short_Name, string _txt_IMPACC1_CR_Cust_abbr, string _txt_IMPACC1_CR_Cust_Acc, string _txt_IMPACC1_CR_Acceptance_Curr, string _txt_IMPACC1_CR_Acceptance_amt, string _txt_IMPACC1_CR_Acceptance_payer,
        string _txt_IMPACC1_CR_Interest_Curr, string _txt_IMPACC1_CR_Interest_amt, string _txt_IMPACC1_CR_Interest_payer,
        string _txt_IMPACC1_CR_Accept_Commission_Curr, string _txt_IMPACC1_CR_Accept_Commission_amt, string _txt_IMPACC1_CR_Accept_Commission_Payer,
        string _txt_IMPACC1_CR_Pay_Handle_Commission_Curr, string _txt_IMPACC1_CR_Pay_Handle_Commission_amt, string _txt_IMPACC1_CR_Pay_Handle_Commission_Payer,
        string _txt_IMPACC1_CR_Others_Curr, string _txt_IMPACC1_CR_Others_amt, string _txt_IMPACC1_CR_Others_Payer,
        string _txt_IMPACC1_CR_Their_Commission_Curr, string _txt_IMPACC1_CR_Their_Commission_amt, string _txt_IMPACC1_CR_Their_Commission_Payer,
        string _txt_IMPACC1_DR_Code, string _txt_IMPACC1_DR_AC_Short_Name, string _txt_IMPACC1_DR_Cust_abbr, string _txt_IMPACC1_DR_Cust_Acc,
        string _txt_IMPACC1_DR_Cur_Acc_Curr, string _txt_IMPACC1_DR_Cur_Acc_amt, string _txt_IMPACC1_DR_Cur_Acc_payer,
        string _txt_IMPACC1_DR_Cur_Acc_Curr2, string _txt_IMPACC1_DR_Cur_Acc_amt2, string _txt_IMPACC1_DR_Cur_Acc_payer2,
        string _txt_IMPACC1_DR_Cur_Acc_Curr3, string _txt_IMPACC1_DR_Cur_Acc_amt3, string _txt_IMPACC1_DR_Cur_Acc_payer3,
        string _txt_IMPACC1_DR_Cur_Acc_Curr4, string _txt_IMPACC1_DR_Cur_Acc_amt4, string _txt_IMPACC1_DR_Cur_Acc_payer4,
        string _txt_IMPACC1_DR_Cur_Acc_Curr5, string _txt_IMPACC1_DR_Cur_Acc_amt5, string _txt_IMPACC1_DR_Cur_Acc_payer5,
        string _txt_IMPACC1_DR_Cur_Acc_Curr6, string _txt_IMPACC1_DR_Cur_Acc_amt6, string _txt_IMPACC1_DR_Cur_Acc_payer6,

        string _txt_IMPACC1_DR_Code2, string _txt_IMPACC1_DR_AC_Short_Name2, string _txt_IMPACC1_DR_Cust_abbr2, string _txt_IMPACC1_DR_Cust_Acc2,
        string _txt_IMPACC1_DR_Code3, string _txt_IMPACC1_DR_AC_Short_Name3, string _txt_IMPACC1_DR_Cust_abbr3, string _txt_IMPACC1_DR_Cust_Acc3,
        string _txt_IMPACC1_DR_Code4, string _txt_IMPACC1_DR_AC_Short_Name4, string _txt_IMPACC1_DR_Cust_abbr4, string _txt_IMPACC1_DR_Cust_Acc4,
        string _txt_IMPACC1_DR_Code5, string _txt_IMPACC1_DR_AC_Short_Name5, string _txt_IMPACC1_DR_Cust_abbr5, string _txt_IMPACC1_DR_Cust_Acc5,
        string _txt_IMPACC1_DR_Code6, string _txt_IMPACC1_DR_AC_Short_Name6, string _txt_IMPACC1_DR_Cust_abbr6, string _txt_IMPACC1_DR_Cust_Acc6,

        //////////// IMPORT ACCOUNTING 2 ////////////
        string _chk_IMPACC2Flag, string _txt_IMPACC2_FCRefNo,
        string _txt_IMPACC2_DiscAmt, string _txt_IMPACC2_DiscExchRate,
        string _txt_IMPACC2_Princ_matu, string _txt_IMPACC2_Princ_lump, string _txt_IMPACC2_Princ_Contract_no, string _txt_IMPACC2_Princ_Ex_Curr, string _txt_IMPACC2_Princ_Ex_rate, string _txt_IMPACC2_Princ_Intnl_Ex_rate,
        string _txt_IMPACC2_Interest_matu, string _txt_IMPACC2_Interest_lump, string _txt_IMPACC2_Interest_Contract_no, string _txt_IMPACC2_Interest_Ex_Curr, string _txt_IMPACC2_Interest_Ex_rate, string _txt_IMPACC2_Interest_Intnl_Ex_rate,
        string _txt_IMPACC2_Commission_matu, string _txt_IMPACC2_Commission_lump, string _txt_IMPACC2_Commission_Contract_no, string _txt_IMPACC2_Commission_Ex_Curr, string _txt_IMPACC2_Commission_Ex_rate, string _txt_IMPACC2_Commission_Intnl_Ex_rate,
        string _txt_IMPACC2_Their_Commission_matu, string _txt_IMPACC2_Their_Commission_lump, string _txt_IMPACC2_Their_Commission_Contract_no, string _txt_IMPACC2_Their_Commission_Ex_Curr, string _txt_IMPACC2_Their_Commission_Ex_rate, string _txt_IMPACC2_Their_Commission_Intnl_Ex_rate,
        string _txt_IMPACC2_CR_Code, string _txt_IMPACC2_CR_AC_Short_Name, string _txt_IMPACC2_CR_Cust_abbr, string _txt_IMPACC2_CR_Cust_Acc, string _txt_IMPACC2_CR_Acceptance_Curr, string _txt_IMPACC2_CR_Acceptance_amt, string _txt_IMPACC2_CR_Acceptance_payer,
        string _txt_IMPACC2_CR_Interest_Curr, string _txt_IMPACC2_CR_Interest_amt, string _txt_IMPACC2_CR_Interest_payer,
        string _txt_IMPACC2_CR_Accept_Commission_Curr, string _txt_IMPACC2_CR_Accept_Commission_amt, string _txt_IMPACC2_CR_Accept_Commission_Payer,
        string _txt_IMPACC2_CR_Pay_Handle_Commission_Curr, string _txt_IMPACC2_CR_Pay_Handle_Commission_amt, string _txt_IMPACC2_CR_Pay_Handle_Commission_Payer,
        string _txt_IMPACC2_CR_Others_Curr, string _txt_IMPACC2_CR_Others_amt, string _txt_IMPACC2_CR_Others_Payer,
        string _txt_IMPACC2_CR_Their_Commission_Curr, string _txt_IMPACC2_CR_Their_Commission_amt, string _txt_IMPACC2_CR_Their_Commission_Payer,
        string _txt_IMPACC2_DR_Code, string _txt_IMPACC2_DR_AC_Short_Name, string _txt_IMPACC2_DR_Cust_abbr, string _txt_IMPACC2_DR_Cust_Acc,
        string _txt_IMPACC2_DR_Cur_Acc_Curr, string _txt_IMPACC2_DR_Cur_Acc_amt, string _txt_IMPACC2_DR_Cur_Acc_payer,
        string _txt_IMPACC2_DR_Cur_Acc_Curr2, string _txt_IMPACC2_DR_Cur_Acc_amt2, string _txt_IMPACC2_DR_Cur_Acc_payer2,
        string _txt_IMPACC2_DR_Cur_Acc_Curr3, string _txt_IMPACC2_DR_Cur_Acc_amt3, string _txt_IMPACC2_DR_Cur_Acc_payer3,
        string _txt_IMPACC2_DR_Cur_Acc_Curr4, string _txt_IMPACC2_DR_Cur_Acc_amt4, string _txt_IMPACC2_DR_Cur_Acc_payer4,
        string _txt_IMPACC2_DR_Cur_Acc_Curr5, string _txt_IMPACC2_DR_Cur_Acc_amt5, string _txt_IMPACC2_DR_Cur_Acc_payer5,
        string _txt_IMPACC2_DR_Cur_Acc_Curr6, string _txt_IMPACC2_DR_Cur_Acc_amt6, string _txt_IMPACC2_DR_Cur_Acc_payer6,

        string _txt_IMPACC2_DR_Code2, string _txt_IMPACC2_DR_AC_Short_Name2, string _txt_IMPACC2_DR_Cust_abbr2, string _txt_IMPACC2_DR_Cust_Acc2,
        string _txt_IMPACC2_DR_Code3, string _txt_IMPACC2_DR_AC_Short_Name3, string _txt_IMPACC2_DR_Cust_abbr3, string _txt_IMPACC2_DR_Cust_Acc3,
        string _txt_IMPACC2_DR_Code4, string _txt_IMPACC2_DR_AC_Short_Name4, string _txt_IMPACC2_DR_Cust_abbr4, string _txt_IMPACC2_DR_Cust_Acc4,
        string _txt_IMPACC2_DR_Code5, string _txt_IMPACC2_DR_AC_Short_Name5, string _txt_IMPACC2_DR_Cust_abbr5, string _txt_IMPACC2_DR_Cust_Acc5,
        string _txt_IMPACC2_DR_Code6, string _txt_IMPACC2_DR_AC_Short_Name6, string _txt_IMPACC2_DR_Cust_abbr6, string _txt_IMPACC2_DR_Cust_Acc6,

        //////////// IMPORT ACCOUNTING 3 ////////////
        string _chk_IMPACC3Flag, string _txt_IMPACC3_FCRefNo,
        string _txt_IMPACC3_DiscAmt, string _txt_IMPACC3_DiscExchRate,
        string _txt_IMPACC3_Princ_matu, string _txt_IMPACC3_Princ_lump, string _txt_IMPACC3_Princ_Contract_no, string _txt_IMPACC3_Princ_Ex_Curr, string _txt_IMPACC3_Princ_Ex_rate, string _txt_IMPACC3_Princ_Intnl_Ex_rate,
        string _txt_IMPACC3_Interest_matu, string _txt_IMPACC3_Interest_lump, string _txt_IMPACC3_Interest_Contract_no, string _txt_IMPACC3_Interest_Ex_Curr, string _txt_IMPACC3_Interest_Ex_rate, string _txt_IMPACC3_Interest_Intnl_Ex_rate,
        string _txt_IMPACC3_Commission_matu, string _txt_IMPACC3_Commission_lump, string _txt_IMPACC3_Commission_Contract_no, string _txt_IMPACC3_Commission_Ex_Curr, string _txt_IMPACC3_Commission_Ex_rate, string _txt_IMPACC3_Commission_Intnl_Ex_rate,
        string _txt_IMPACC3_Their_Commission_matu, string _txt_IMPACC3_Their_Commission_lump, string _txt_IMPACC3_Their_Commission_Contract_no, string _txt_IMPACC3_Their_Commission_Ex_Curr, string _txt_IMPACC3_Their_Commission_Ex_rate, string _txt_IMPACC3_Their_Commission_Intnl_Ex_rate,
        string _txt_IMPACC3_CR_Code, string _txt_IMPACC3_CR_AC_Short_Name, string _txt_IMPACC3_CR_Cust_abbr, string _txt_IMPACC3_CR_Cust_Acc, string _txt_IMPACC3_CR_Acceptance_Curr, string _txt_IMPACC3_CR_Acceptance_amt, string _txt_IMPACC3_CR_Acceptance_payer,
        string _txt_IMPACC3_CR_Interest_Curr, string _txt_IMPACC3_CR_Interest_amt, string _txt_IMPACC3_CR_Interest_payer,
        string _txt_IMPACC3_CR_Accept_Commission_Curr, string _txt_IMPACC3_CR_Accept_Commission_amt, string _txt_IMPACC3_CR_Accept_Commission_Payer,
        string _txt_IMPACC3_CR_Pay_Handle_Commission_Curr, string _txt_IMPACC3_CR_Pay_Handle_Commission_amt, string _txt_IMPACC3_CR_Pay_Handle_Commission_Payer,
        string _txt_IMPACC3_CR_Others_Curr, string _txt_IMPACC3_CR_Others_amt, string _txt_IMPACC3_CR_Others_Payer,
        string _txt_IMPACC3_CR_Their_Commission_Curr, string _txt_IMPACC3_CR_Their_Commission_amt, string _txt_IMPACC3_CR_Their_Commission_Payer,
        string _txt_IMPACC3_DR_Code, string _txt_IMPACC3_DR_AC_Short_Name, string _txt_IMPACC3_DR_Cust_abbr, string _txt_IMPACC3_DR_Cust_Acc,
        string _txt_IMPACC3_DR_Cur_Acc_Curr, string _txt_IMPACC3_DR_Cur_Acc_amt, string _txt_IMPACC3_DR_Cur_Acc_payer,
        string _txt_IMPACC3_DR_Cur_Acc_Curr2, string _txt_IMPACC3_DR_Cur_Acc_amt2, string _txt_IMPACC3_DR_Cur_Acc_payer2,
        string _txt_IMPACC3_DR_Cur_Acc_Curr3, string _txt_IMPACC3_DR_Cur_Acc_amt3, string _txt_IMPACC3_DR_Cur_Acc_payer3,
        string _txt_IMPACC3_DR_Cur_Acc_Curr4, string _txt_IMPACC3_DR_Cur_Acc_amt4, string _txt_IMPACC3_DR_Cur_Acc_payer4,
        string _txt_IMPACC3_DR_Cur_Acc_Curr5, string _txt_IMPACC3_DR_Cur_Acc_amt5, string _txt_IMPACC3_DR_Cur_Acc_payer5,
        string _txt_IMPACC3_DR_Cur_Acc_Curr6, string _txt_IMPACC3_DR_Cur_Acc_amt6, string _txt_IMPACC3_DR_Cur_Acc_payer6,

        string _txt_IMPACC3_DR_Code2, string _txt_IMPACC3_DR_AC_Short_Name2, string _txt_IMPACC3_DR_Cust_abbr2, string _txt_IMPACC3_DR_Cust_Acc2,
        string _txt_IMPACC3_DR_Code3, string _txt_IMPACC3_DR_AC_Short_Name3, string _txt_IMPACC3_DR_Cust_abbr3, string _txt_IMPACC3_DR_Cust_Acc3,
        string _txt_IMPACC3_DR_Code4, string _txt_IMPACC3_DR_AC_Short_Name4, string _txt_IMPACC3_DR_Cust_abbr4, string _txt_IMPACC3_DR_Cust_Acc4,
        string _txt_IMPACC3_DR_Code5, string _txt_IMPACC3_DR_AC_Short_Name5, string _txt_IMPACC3_DR_Cust_abbr5, string _txt_IMPACC3_DR_Cust_Acc5,
        string _txt_IMPACC3_DR_Code6, string _txt_IMPACC3_DR_AC_Short_Name6, string _txt_IMPACC3_DR_Cust_abbr6, string _txt_IMPACC3_DR_Cust_Acc6,

        /////////// IMPORT ACCOUNTING 4 ////////////
        string _chk_IMPACC4Flag, string _txt_IMPACC4_FCRefNo,
        string _txt_IMPACC4_DiscAmt, string _txt_IMPACC4_DiscExchRate,
        string _txt_IMPACC4_Princ_matu, string _txt_IMPACC4_Princ_lump, string _txt_IMPACC4_Princ_Contract_no, string _txt_IMPACC4_Princ_Ex_Curr, string _txt_IMPACC4_Princ_Ex_rate, string _txt_IMPACC4_Princ_Intnl_Ex_rate,
        string _txt_IMPACC4_Interest_matu, string _txt_IMPACC4_Interest_lump, string _txt_IMPACC4_Interest_Contract_no, string _txt_IMPACC4_Interest_Ex_Curr, string _txt_IMPACC4_Interest_Ex_rate, string _txt_IMPACC4_Interest_Intnl_Ex_rate,
        string _txt_IMPACC4_Commission_matu, string _txt_IMPACC4_Commission_lump, string _txt_IMPACC4_Commission_Contract_no, string _txt_IMPACC4_Commission_Ex_Curr, string _txt_IMPACC4_Commission_Ex_rate, string _txt_IMPACC4_Commission_Intnl_Ex_rate,
        string _txt_IMPACC4_Their_Commission_matu, string _txt_IMPACC4_Their_Commission_lump, string _txt_IMPACC4_Their_Commission_Contract_no, string _txt_IMPACC4_Their_Commission_Ex_Curr, string _txt_IMPACC4_Their_Commission_Ex_rate, string _txt_IMPACC4_Their_Commission_Intnl_Ex_rate,
        string _txt_IMPACC4_CR_Code, string _txt_IMPACC4_CR_AC_Short_Name, string _txt_IMPACC4_CR_Cust_abbr, string _txt_IMPACC4_CR_Cust_Acc, string _txt_IMPACC4_CR_Acceptance_Curr, string _txt_IMPACC4_CR_Acceptance_amt, string _txt_IMPACC4_CR_Acceptance_payer,
        string _txt_IMPACC4_CR_Interest_Curr, string _txt_IMPACC4_CR_Interest_amt, string _txt_IMPACC4_CR_Interest_payer,
        string _txt_IMPACC4_CR_Accept_Commission_Curr, string _txt_IMPACC4_CR_Accept_Commission_amt, string _txt_IMPACC4_CR_Accept_Commission_Payer,
        string _txt_IMPACC4_CR_Pay_Handle_Commission_Curr, string _txt_IMPACC4_CR_Pay_Handle_Commission_amt, string _txt_IMPACC4_CR_Pay_Handle_Commission_Payer,
        string _txt_IMPACC4_CR_Others_Curr, string _txt_IMPACC4_CR_Others_amt, string _txt_IMPACC4_CR_Others_Payer,
        string _txt_IMPACC4_CR_Their_Commission_Curr, string _txt_IMPACC4_CR_Their_Commission_amt, string _txt_IMPACC4_CR_Their_Commission_Payer,
        string _txt_IMPACC4_DR_Code, string _txt_IMPACC4_DR_AC_Short_Name, string _txt_IMPACC4_DR_Cust_abbr, string _txt_IMPACC4_DR_Cust_Acc,
        string _txt_IMPACC4_DR_Cur_Acc_Curr, string _txt_IMPACC4_DR_Cur_Acc_amt, string _txt_IMPACC4_DR_Cur_Acc_payer,
        string _txt_IMPACC4_DR_Cur_Acc_Curr2, string _txt_IMPACC4_DR_Cur_Acc_amt2, string _txt_IMPACC4_DR_Cur_Acc_payer2,
        string _txt_IMPACC4_DR_Cur_Acc_Curr3, string _txt_IMPACC4_DR_Cur_Acc_amt3, string _txt_IMPACC4_DR_Cur_Acc_payer3,
        string _txt_IMPACC4_DR_Cur_Acc_Curr4, string _txt_IMPACC4_DR_Cur_Acc_amt4, string _txt_IMPACC4_DR_Cur_Acc_payer4,
        string _txt_IMPACC4_DR_Cur_Acc_Curr5, string _txt_IMPACC4_DR_Cur_Acc_amt5, string _txt_IMPACC4_DR_Cur_Acc_payer5,
        string _txt_IMPACC4_DR_Cur_Acc_Curr6, string _txt_IMPACC4_DR_Cur_Acc_amt6, string _txt_IMPACC4_DR_Cur_Acc_payer6,

        string _txt_IMPACC4_DR_Code2, string _txt_IMPACC4_DR_AC_Short_Name2, string _txt_IMPACC4_DR_Cust_abbr2, string _txt_IMPACC4_DR_Cust_Acc2,
        string _txt_IMPACC4_DR_Code3, string _txt_IMPACC4_DR_AC_Short_Name3, string _txt_IMPACC4_DR_Cust_abbr3, string _txt_IMPACC4_DR_Cust_Acc3,
        string _txt_IMPACC4_DR_Code4, string _txt_IMPACC4_DR_AC_Short_Name4, string _txt_IMPACC4_DR_Cust_abbr4, string _txt_IMPACC4_DR_Cust_Acc4,
        string _txt_IMPACC4_DR_Code5, string _txt_IMPACC4_DR_AC_Short_Name5, string _txt_IMPACC4_DR_Cust_abbr5, string _txt_IMPACC4_DR_Cust_Acc5,
        string _txt_IMPACC4_DR_Code6, string _txt_IMPACC4_DR_AC_Short_Name6, string _txt_IMPACC4_DR_Cust_abbr6, string _txt_IMPACC4_DR_Cust_Acc6,

        ////////////// IMPORT ACCounting 5 ///////////////
        string _chk_IMPACC5Flag, string _txt_IMPACC5_FCRefNo,
        string _txt_IMPACC5_DiscAmt, string _txt_IMPACC5_DiscExchRate,
        string _txt_IMPACC5_Princ_matu, string _txt_IMPACC5_Princ_lump, string _txt_IMPACC5_Princ_Contract_no, string _txt_IMPACC5_Princ_Ex_Curr, string _txt_IMPACC5_Princ_Ex_rate, string _txt_IMPACC5_Princ_Intnl_Ex_rate,
        string _txt_IMPACC5_Interest_matu, string _txt_IMPACC5_Interest_lump, string _txt_IMPACC5_Interest_Contract_no, string _txt_IMPACC5_Interest_Ex_Curr, string _txt_IMPACC5_Interest_Ex_rate, string _txt_IMPACC5_Interest_Intnl_Ex_rate,
        string _txt_IMPACC5_Commission_matu, string _txt_IMPACC5_Commission_lump, string _txt_IMPACC5_Commission_Contract_no, string _txt_IMPACC5_Commission_Ex_Curr, string _txt_IMPACC5_Commission_Ex_rate, string _txt_IMPACC5_Commission_Intnl_Ex_rate,
        string _txt_IMPACC5_Their_Commission_matu, string _txt_IMPACC5_Their_Commission_lump, string _txt_IMPACC5_Their_Commission_Contract_no, string _txt_IMPACC5_Their_Commission_Ex_Curr, string _txt_IMPACC5_Their_Commission_Ex_rate, string _txt_IMPACC5_Their_Commission_Intnl_Ex_rate,
        string _txt_IMPACC5_CR_Code, string _txt_IMPACC5_CR_AC_Short_Name, string _txt_IMPACC5_CR_Cust_abbr, string _txt_IMPACC5_CR_Cust_Acc, string _txt_IMPACC5_CR_Acceptance_Curr, string _txt_IMPACC5_CR_Acceptance_amt, string _txt_IMPACC5_CR_Acceptance_payer,
        string _txt_IMPACC5_CR_Interest_Curr, string _txt_IMPACC5_CR_Interest_amt, string _txt_IMPACC5_CR_Interest_payer,
        string _txt_IMPACC5_CR_Accept_Commission_Curr, string _txt_IMPACC5_CR_Accept_Commission_amt, string _txt_IMPACC5_CR_Accept_Commission_Payer,
        string _txt_IMPACC5_CR_Pay_Handle_Commission_Curr, string _txt_IMPACC5_CR_Pay_Handle_Commission_amt, string _txt_IMPACC5_CR_Pay_Handle_Commission_Payer,
        string _txt_IMPACC5_CR_Others_Curr, string _txt_IMPACC5_CR_Others_amt, string _txt_IMPACC5_CR_Others_Payer,
        string _txt_IMPACC5_CR_Their_Commission_Curr, string _txt_IMPACC5_CR_Their_Commission_amt, string _txt_IMPACC5_CR_Their_Commission_Payer,
        string _txt_IMPACC5_DR_Code, string _txt_IMPACC5_DR_AC_Short_Name, string _txt_IMPACC5_DR_Cust_abbr, string _txt_IMPACC5_DR_Cust_Acc,
        string _txt_IMPACC5_DR_Cur_Acc_Curr, string _txt_IMPACC5_DR_Cur_Acc_amt, string _txt_IMPACC5_DR_Cur_Acc_payer,
        string _txt_IMPACC5_DR_Cur_Acc_Curr2, string _txt_IMPACC5_DR_Cur_Acc_amt2, string _txt_IMPACC5_DR_Cur_Acc_payer2,
        string _txt_IMPACC5_DR_Cur_Acc_Curr3, string _txt_IMPACC5_DR_Cur_Acc_amt3, string _txt_IMPACC5_DR_Cur_Acc_payer3,
        string _txt_IMPACC5_DR_Cur_Acc_Curr4, string _txt_IMPACC5_DR_Cur_Acc_amt4, string _txt_IMPACC5_DR_Cur_Acc_payer4,
        string _txt_IMPACC5_DR_Cur_Acc_Curr5, string _txt_IMPACC5_DR_Cur_Acc_amt5, string _txt_IMPACC5_DR_Cur_Acc_payer5,
        string _txt_IMPACC5_DR_Cur_Acc_Curr6, string _txt_IMPACC5_DR_Cur_Acc_amt6, string _txt_IMPACC5_DR_Cur_Acc_payer6,

        string _txt_IMPACC5_DR_Code2, string _txt_IMPACC5_DR_AC_Short_Name2, string _txt_IMPACC5_DR_Cust_abbr2, string _txt_IMPACC5_DR_Cust_Acc2,
        string _txt_IMPACC5_DR_Code3, string _txt_IMPACC5_DR_AC_Short_Name3, string _txt_IMPACC5_DR_Cust_abbr3, string _txt_IMPACC5_DR_Cust_Acc3,
        string _txt_IMPACC5_DR_Code4, string _txt_IMPACC5_DR_AC_Short_Name4, string _txt_IMPACC5_DR_Cust_abbr4, string _txt_IMPACC5_DR_Cust_Acc4,
        string _txt_IMPACC5_DR_Code5, string _txt_IMPACC5_DR_AC_Short_Name5, string _txt_IMPACC5_DR_Cust_abbr5, string _txt_IMPACC5_DR_Cust_Acc5,
        string _txt_IMPACC5_DR_Code6, string _txt_IMPACC5_DR_AC_Short_Name6, string _txt_IMPACC5_DR_Cust_abbr6, string _txt_IMPACC5_DR_Cust_Acc6,

        ///////////////// GENERAL OPRATOIN 1 /////////////////
        string _chk_GO1Flag,
        string _txt_GO1_Left_Comment,
        string _txt_GO1_Left_SectionNo, string _txt_GO1_Left_Remarks, string _txt_GO1_Left_Memo,
        string _txt_GO1_Left_Scheme_no,
        string _txt_GO1_Left_Debit_Code, string _txt_GO1_Left_Debit_Curr, string _txt_GO1_Left_Debit_Amt,
        string _txt_GO1_Left_Debit_Cust, string _txt_GO1_Left_Debit_Cust_Name,
        string _txt_GO1_Left_Debit_Cust_AcCode, string _txt_GO1_Left_Debit_Cust_AcCode_Name, string _txt_GO1_Left_Debit_Cust_AccNo,
        string _txt_GO1_Left_Debit_Exch_Rate, string _txt_GO1_Left_Debit_Exch_CCY,
        string _txt_GO1_Left_Debit_FUND, string _txt_GO1_Left_Debit_Check_No, string _txt_GO1_Left_Debit_Available,
        string _txt_GO1_Left_Debit_AdPrint, string _txt_GO1_Left_Debit_Details, string _txt_GO1_Left_Debit_Entity,
        string _txt_GO1_Left_Debit_Division, string _txt_GO1_Left_Debit_Inter_Amount, string _txt_GO1_Left_Debit_Inter_Rate,
        string _txt_GO1_Left_Credit_Code, string _txt_GO1_Left_Credit_Curr, string _txt_GO1_Left_Credit_Amt,
        string _txt_GO1_Left_Credit_Cust, string _txt_GO1_Left_Credit_Cust_Name,
        string _txt_GO1_Left_Credit_Cust_AcCode, string _txt_GO1_Left_Credit_Cust_AcCode_Name, string _txt_GO1_Left_Credit_Cust_AccNo,
        string _txt_GO1_Left_Credit_Exch_Rate, string _txt_GO1_Left_Credit_Exch_Curr,
        string _txt_GO1_Left_Credit_FUND, string _txt_GO1_Left_Credit_Check_No, string _txt_GO1_Left_Credit_Available,
        string _txt_GO1_Left_Credit_AdPrint, string _txt_GO1_Left_Credit_Details, string _txt_GO1_Left_Credit_Entity,
        string _txt_GO1_Left_Credit_Division, string _txt_GO1_Left_Credit_Inter_Amount, string _txt_GO1_Left_Credit_Inter_Rate,
        string _txt_GO1_Right_Comment,
        string _txt_GO1_Right_SectionNo, string _txt_GO1_Right_Remarks, string _txt_GO1_Right_Memo,
        string _txt_GO1_Right_Scheme_no,
        string _txt_GO1_Right_Debit_Code, string _txt_GO1_Right_Debit_Curr, string _txt_GO1_Right_Debit_Amt,
        string _txt_GO1_Right_Debit_Cust, string _txt_GO1_Right_Debit_Cust_Name,
        string _txt_GO1_Right_Debit_Cust_AcCode, string _txt_GO1_Right_Debit_Cust_AcCode_Name,
        string _txt_GO1_Right_Debit_Cust_AccNo,
        string _txt_GO1_Right_Debit_Exch_Rate, string _txt_GO1_Right_Debit_Exch_CCY,
        string _txt_GO1_Right_Debit_FUND, string _txt_GO1_Right_Debit_Check_No, string _txt_GO1_Right_Debit_Available,
        string _txt_GO1_Right_Debit_AdPrint, string _txt_GO1_Right_Debit_Details, string _txt_GO1_Right_Debit_Entity,
        string _txt_GO1_Right_Debit_Division, string _txt_GO1_Right_Debit_Inter_Amount, string _txt_GO1_Right_Debit_Inter_Rate,
        string _txt_GO1_Right_Credit_Code, string _txt_GO1_Right_Credit_Curr, string _txt_GO1_Right_Credit_Amt,
        string _txt_GO1_Right_Credit_Cust, string _txt_GO1_Right_Credit_Cust_Name,
        string _txt_GO1_Right_Credit_Cust_AcCode, string _txt_GO1_Right_Credit_Cust_AcCode_Name, string _txt_GO1_Right_Credit_Cust_AccNo,
        string _txt_GO1_Right_Credit_Exch_Rate, string _txt_GO1_Right_Credit_Exch_Curr,
        string _txt_GO1_Right_Credit_FUND, string _txt_GO1_Right_Credit_Check_No, string _txt_GO1_Right_Credit_Available,
        string _txt_GO1_Right_Credit_AdPrint, string _txt_GO1_Right_Credit_Details, string _txt_GO1_Right_Credit_Entity,
        string _txt_GO1_Right_Credit_Division, string _txt_GO1_Right_Credit_Inter_Amount, string _txt_GO1_Right_Credit_Inter_Rate,

        ///////////////// GENERAL OPRATOIN 2 ////////////////////
        string _chk_GO2Flag,
        string _txt_GO2_Left_Comment,
        string _txt_GO2_Left_SectionNo, string _txt_GO2_Left_Remarks, string _txt_GO2_Left_Memo,
        string _txt_GO2_Left_Scheme_no,
        string _txt_GO2_Left_Debit_Code, string _txt_GO2_Left_Debit_Curr, string _txt_GO2_Left_Debit_Amt,
        string _txt_GO2_Left_Debit_Cust, string _txt_GO2_Left_Debit_Cust_Name,
        string _txt_GO2_Left_Debit_Cust_AcCode, string _txt_GO2_Left_Debit_Cust_AcCode_Name, string _txt_GO2_Left_Debit_Cust_AccNo,
        string _txt_GO2_Left_Debit_Exch_Rate, string _txt_GO2_Left_Debit_Exch_CCY,
        string _txt_GO2_Left_Debit_FUND, string _txt_GO2_Left_Debit_Check_No, string _txt_GO2_Left_Debit_Available,
        string _txt_GO2_Left_Debit_AdPrint, string _txt_GO2_Left_Debit_Details, string _txt_GO2_Left_Debit_Entity,
        string _txt_GO2_Left_Debit_Division, string _txt_GO2_Left_Debit_Inter_Amount, string _txt_GO2_Left_Debit_Inter_Rate,
        string _txt_GO2_Left_Credit_Code, string _txt_GO2_Left_Credit_Curr, string _txt_GO2_Left_Credit_Amt,
        string _txt_GO2_Left_Credit_Cust, string _txt_GO2_Left_Credit_Cust_Name,
        string _txt_GO2_Left_Credit_Cust_AcCode, string _txt_GO2_Left_Credit_Cust_AcCode_Name, string _txt_GO2_Left_Credit_Cust_AccNo,
        string _txt_GO2_Left_Credit_Exch_Rate, string _txt_GO2_Left_Credit_Exch_Curr,
        string _txt_GO2_Left_Credit_FUND, string _txt_GO2_Left_Credit_Check_No, string _txt_GO2_Left_Credit_Available,
        string _txt_GO2_Left_Credit_AdPrint, string _txt_GO2_Left_Credit_Details, string _txt_GO2_Left_Credit_Entity,
        string _txt_GO2_Left_Credit_Division, string _txt_GO2_Left_Credit_Inter_Amount, string _txt_GO2_Left_Credit_Inter_Rate,
        string _txt_GO2_Right_Comment,
        string _txt_GO2_Right_SectionNo, string _txt_GO2_Right_Remarks, string _txt_GO2_Right_Memo,
        string _txt_GO2_Right_Scheme_no,
        string _txt_GO2_Right_Debit_Code, string _txt_GO2_Right_Debit_Curr, string _txt_GO2_Right_Debit_Amt,
        string _txt_GO2_Right_Debit_Cust, string _txt_GO2_Right_Debit_Cust_Name,
        string _txt_GO2_Right_Debit_Cust_AcCode, string _txt_GO2_Right_Debit_Cust_AcCode_Name,
        string _txt_GO2_Right_Debit_Cust_AccNo,
        string _txt_GO2_Right_Debit_Exch_Rate, string _txt_GO2_Right_Debit_Exch_CCY,
        string _txt_GO2_Right_Debit_FUND, string _txt_GO2_Right_Debit_Check_No, string _txt_GO2_Right_Debit_Available,
        string _txt_GO2_Right_Debit_AdPrint, string _txt_GO2_Right_Debit_Details, string _txt_GO2_Right_Debit_Entity,
        string _txt_GO2_Right_Debit_Division, string _txt_GO2_Right_Debit_Inter_Amount, string _txt_GO2_Right_Debit_Inter_Rate,
        string _txt_GO2_Right_Credit_Code, string _txt_GO2_Right_Credit_Curr, string _txt_GO2_Right_Credit_Amt,
        string _txt_GO2_Right_Credit_Cust, string _txt_GO2_Right_Credit_Cust_Name,
        string _txt_GO2_Right_Credit_Cust_AcCode, string _txt_GO2_Right_Credit_Cust_AcCode_Name, string _txt_GO2_Right_Credit_Cust_AccNo,
        string _txt_GO2_Right_Credit_Exch_Rate, string _txt_GO2_Right_Credit_Exch_Curr,
        string _txt_GO2_Right_Credit_FUND, string _txt_GO2_Right_Credit_Check_No, string _txt_GO2_Right_Credit_Available,
        string _txt_GO2_Right_Credit_AdPrint, string _txt_GO2_Right_Credit_Details, string _txt_GO2_Right_Credit_Entity,
        string _txt_GO2_Right_Credit_Division, string _txt_GO2_Right_Credit_Inter_Amount, string _txt_GO2_Right_Credit_Inter_Rate,
        ///////////////// GENERAL OPRATOIN 3 ////////////////////
        string _chk_GO3Flag,
        string _txt_GO3_Left_Comment,
        string _txt_GO3_Left_SectionNo, string _txt_GO3_Left_Remarks, string _txt_GO3_Left_Memo,
        string _txt_GO3_Left_Scheme_no,
        string _txt_GO3_Left_Debit_Code, string _txt_GO3_Left_Debit_Curr, string _txt_GO3_Left_Debit_Amt,
        string _txt_GO3_Left_Debit_Cust, string _txt_GO3_Left_Debit_Cust_Name,
        string _txt_GO3_Left_Debit_Cust_AcCode, string _txt_GO3_Left_Debit_Cust_AcCode_Name, string _txt_GO3_Left_Debit_Cust_AccNo,
        string _txt_GO3_Left_Debit_Exch_Rate, string _txt_GO3_Left_Debit_Exch_CCY,
        string _txt_GO3_Left_Debit_FUND, string _txt_GO3_Left_Debit_Check_No, string _txt_GO3_Left_Debit_Available,
        string _txt_GO3_Left_Debit_AdPrint, string _txt_GO3_Left_Debit_Details, string _txt_GO3_Left_Debit_Entity,
        string _txt_GO3_Left_Debit_Division, string _txt_GO3_Left_Debit_Inter_Amount, string _txt_GO3_Left_Debit_Inter_Rate,
        string _txt_GO3_Left_Credit_Code, string _txt_GO3_Left_Credit_Curr, string _txt_GO3_Left_Credit_Amt,
        string _txt_GO3_Left_Credit_Cust, string _txt_GO3_Left_Credit_Cust_Name,
        string _txt_GO3_Left_Credit_Cust_AcCode, string _txt_GO3_Left_Credit_Cust_AcCode_Name, string _txt_GO3_Left_Credit_Cust_AccNo,
        string _txt_GO3_Left_Credit_Exch_Rate, string _txt_GO3_Left_Credit_Exch_Curr,
        string _txt_GO3_Left_Credit_FUND, string _txt_GO3_Left_Credit_Check_No, string _txt_GO3_Left_Credit_Available,
        string _txt_GO3_Left_Credit_AdPrint, string _txt_GO3_Left_Credit_Details, string _txt_GO3_Left_Credit_Entity,
        string _txt_GO3_Left_Credit_Division, string _txt_GO3_Left_Credit_Inter_Amount, string _txt_GO3_Left_Credit_Inter_Rate,
        string _txt_GO3_Right_Comment,
        string _txt_GO3_Right_SectionNo, string _txt_GO3_Right_Remarks, string _txt_GO3_Right_Memo,
        string _txt_GO3_Right_Scheme_no,
        string _txt_GO3_Right_Debit_Code, string _txt_GO3_Right_Debit_Curr, string _txt_GO3_Right_Debit_Amt,
        string _txt_GO3_Right_Debit_Cust, string _txt_GO3_Right_Debit_Cust_Name,
        string _txt_GO3_Right_Debit_Cust_AcCode, string _txt_GO3_Right_Debit_Cust_AcCode_Name,
        string _txt_GO3_Right_Debit_Cust_AccNo,
        string _txt_GO3_Right_Debit_Exch_Rate, string _txt_GO3_Right_Debit_Exch_CCY,
        string _txt_GO3_Right_Debit_FUND, string _txt_GO3_Right_Debit_Check_No, string _txt_GO3_Right_Debit_Available,
        string _txt_GO3_Right_Debit_AdPrint, string _txt_GO3_Right_Debit_Details, string _txt_GO3_Right_Debit_Entity,
        string _txt_GO3_Right_Debit_Division, string _txt_GO3_Right_Debit_Inter_Amount, string _txt_GO3_Right_Debit_Inter_Rate,
        string _txt_GO3_Right_Credit_Code, string _txt_GO3_Right_Credit_Curr, string _txt_GO3_Right_Credit_Amt,
        string _txt_GO3_Right_Credit_Cust, string _txt_GO3_Right_Credit_Cust_Name,
        string _txt_GO3_Right_Credit_Cust_AcCode, string _txt_GO3_Right_Credit_Cust_AcCode_Name, string _txt_GO3_Right_Credit_Cust_AccNo,
        string _txt_GO3_Right_Credit_Exch_Rate, string _txt_GO3_Right_Credit_Exch_Curr,
        string _txt_GO3_Right_Credit_FUND, string _txt_GO3_Right_Credit_Check_No, string _txt_GO3_Right_Credit_Available,
        string _txt_GO3_Right_Credit_AdPrint, string _txt_GO3_Right_Credit_Details, string _txt_GO3_Right_Credit_Entity,
        string _txt_GO3_Right_Credit_Division, string _txt_GO3_Right_Credit_Inter_Amount, string _txt_GO3_Right_Credit_Inter_Rate,
        ///////////////// GOAcccChange ////////////////////
        string _chk_GOAcccChangeFlag,
        string _txt_GOAccChange_Ref_No, string _txt_GOAccChange_Comment,
        string _txt_GOAccChange_SectionNo, string _txt_GOAccChange_Remarks, string _txt_GOAccChange_Memo,
        string _txt_GOAccChange_Scheme_no,
        string _txt_GOAccChange_Debit_Code, string _txt_GOAccChange_Debit_Curr, string _txt_GOAccChange_Debit_Amt,
        string _txt_GOAccChange_Debit_Cust, string _txt_GOAccChange_Debit_Cust_Name,
        string _txt_GOAccChange_Debit_Cust_AcCode, string _txt_GOAccChange_Debit_Cust_AccNo, string _txt_GOAccChange_Debit_Cust_AcCode_Name,
        string _txt_GOAccChange_Debit_Exch_Rate, string _txt_GOAccChange_Debit_Exch_CCY,
        string _txt_GOAccChange_Debit_FUND, string _txt_GOAccChange_Debit_Check_No, string _txt_GOAccChange_Debit_Available,
        string _txt_GOAccChange_Debit_AdPrint, string _txt_GOAccChange_Debit_Details, string _txt_GOAccChange_Debit_Entity,
        string _txt_GOAccChange_Debit_Division, string _txt_GOAccChange_Debit_Inter_Amount, string _txt_GOAccChange_Debit_Inter_Rate,
        string _txt_GOAccChange_Credit_Code, string _txt_GOAccChange_Credit_Curr, string _txt_GOAccChange_Credit_Amt,
        string _txt_GOAccChange_Credit_Cust, string _txt_GOAccChange_Credit_Cust_Name,
        string _txt_GOAccChange_Credit_Cust_AcCode, string _txt_GOAccChange_Credit_Cust_AcCode_Name, string _txt_GOAccChange_Credit_Cust_AccNo,
        string _txt_GOAccChange_Credit_Exch_Rate, string _txt_GOAccChange_Credit_Exch_Curr,
        string _txt_GOAccChange_Credit_FUND, string _txt_GOAccChange_Credit_Check_No, string _txt_GOAccChange_Credit_Available,
        string _txt_GOAccChange_Credit_AdPrint, string _txt_GOAccChange_Credit_Details, string _txt_GOAccChange_Credit_Entity,
        string _txt_GOAccChange_Credit_Division, string _txt_GOAccChange_Credit_Inter_Amount, string _txt_GOAccChange_Credit_Inter_Rate,
        //// Swift
        string _txt_MT103Receiver,
        string _txtInstructionCode, string _txtTransactionTypeCode, string _txtVDate32, 
        //string _txtCurrency32, string _txtAmount32,
        string _txtCurrency, string _txtInstructedAmount, string _txtExchangeRate, string _txtSendingInstitutionAccountNumber,
        string _txtSendingInstitutionSwiftCode, string _ddlOrderingInstitution, string _txtOrderingInstitutionAccountNumber, string _txtOrderingInstitutionSwiftCode,
        string _txtOrderingInstitutionName, string _txtOrderingInstitutionAddress1, string _txtOrderingInstitutionAddress2, string _txtOrderingInstitutionAddress3,
        string _ddlSendersCorrespondent, string _txtSendersCorrespondentAccountNumber, string _txtSendersCorrespondentSwiftCode, string _txtSendersCorrespondentName,
        string _txtSendersCorrespondentLocation, string _txtSendersCorrespondentAddress1, string _txtSendersCorrespondentAddress2, string _txtSendersCorrespondentAddress3,
        string _ddlReceiversCorrespondent, string _txtReceiversCorrespondentAccountNumber, string _txtReceiversCorrespondentSwiftCode, string _txtReceiversCorrespondentName,
        string _txtReceiversCorrespondentLocation, string _txtReceiversCorrespondentAddress1, string _txtReceiversCorrespondentAddress2, string _txtReceiversCorrespondentAddress3,
        string _ddlThirdReimbursementInstitution, string _txtThirdReimbursementInstitutionAccountNumber, string _txtThirdReimbursementInstitutionSwiftCode, string _txtThirdReimbursementInstitutionName,
        string _txtThirdReimbursementInstitutionLocation, string _txtThirdReimbursementInstitutionAddress1, string _txtThirdReimbursementInstitutionAddress2, string _txtThirdReimbursementInstitutionAddress3,
        string _txtDetailsOfCharges, string _txtSenderCharges, string _txtSenderCharges2, string _txtReceiverCharges, string _txtReceiverCharges2, string _txtSendertoReceiverInformation,
        string _txtSendertoReceiverInformation2, string _txtSendertoReceiverInformation3, string _txtSendertoReceiverInformation4, string _txtSendertoReceiverInformation5,
        string _txtSendertoReceiverInformation6, string _txtRegulatoryReporting, string _txtRegulatoryReporting2, string _txtRegulatoryReporting3,

        string _txtTimeIndication, string _ddlBankOperationCode,
        string _ddlOrderingCustomer, string _txtOrderingCustomer_Acc, string _txtOrderingCustomer_SwiftCode,
        string _txtOrderingCustomer_Name,
        string _txtOrderingCustomer_Addr1, string _txtOrderingCustomer_Addr2, string _txtOrderingCustomer_Addr3,
        string _ddlBeneficiaryCustomer,
        string _txtBeneficiaryCustomerAccountNumber, string _txtBeneficiaryCustomerSwiftCode,
        string _txtBeneficiaryCustomerName,
        string _txtBeneficiaryCustomerAddr1, string _txtBeneficiaryCustomerAddr2, string _txtBeneficiaryCustomerAddr3,
        string _txtRemittanceInformation1, string _txtRemittanceInformation2, string _txtRemittanceInformation3, string _txtRemittanceInformation4,
                
        string _txt202Amount, string _ddlOrderingInstitution202, string _txtOrderingInstitution202AccountNumber, string _txtOrderingInstitution202SwiftCode, string _txtOrderingInstitution202Name,
        string _txtOrderingInstitution202Address1, string _txtOrderingInstitution202Address2, string _txtOrderingInstitution202Address3, string _ddlSendersCorrespondent202,
        string _txtSendersCorrespondent202AccountNumber, string _txtSendersCorrespondent202SwiftCode, string _txtSendersCorrespondent202Name, string _txtSendersCorrespondent202Location,
        string _txtSendersCorrespondent202Address1, string _txtSendersCorrespondent202Address2, string _txtSendersCorrespondent202Address3, string _ddlReceiversCorrespondent202,
        string _txtReceiversCorrespondent202AccountNumber, string _txtReceiversCorrespondent202SwiftCode, string _txtReceiversCorrespondent202Name, string _txtReceiversCorrespondent202Location,
        string _txtReceiversCorrespondent202Address1, string _txtReceiversCorrespondent202Address2, string _txtReceiversCorrespondent202Address3,

        string _rdb_swift_None, string _rdb_swift_103, string _rdb_swift_202, string _rdb_swift_200,
        string _txt200BicCode, string _txt200TransactionRefNO, string _txt200Date, string _txt200Currency, string _txt200Amount, string _txt200SenderCorreCode, string _txt200SenderCorreLocation,
        string _ddl200Intermediary, string _txt200IntermediaryAccountNumber, string _txt200IntermediarySwiftCode, string _txt200IntermediaryName,
        string _txt200IntermediaryAddress1, string _txt200IntermediaryAddress2, string _txt200IntermediaryAddress3, string _ddl200AccWithInstitution,
        string _txt200AccWithInstitutionAccountNumber, string _txt200AccWithInstitutionSwiftCode, string _txt200AccWithInstitutionLocation, string _txt200AccWithInstitutionName,
        string _txt200AccWithInstitutionAddress1, string _txt200AccWithInstitutionAddress2, string _txt200AccWithInstitutionAddress3,
        string _txt200SendertoReceiverInformation1, string _txt200SendertoReceiverInformation2, string _txt200SendertoReceiverInformation3, string _txt200SendertoReceiverInformation4,
        string _txt200SendertoReceiverInformation5, string _txt200SendertoReceiverInformation6,
        string _txtTransactionRefNoR42, string _txtRelatedReferenceR42, string _txtValueDateR42, string _txtCureencyR42, string _txtAmountR42, string _txtOrderingInstitutionIFSCR42,
        string _txtBeneficiaryInstitutionIFSCR42, string _txtCodeWordR42, string _txtAdditionalInformationR42, string _txtMoreInfo1R42,
        string _txtMoreInfo2R42, string _txtMoreInfo3R42, string _txtMoreInfo4R42, string _txtMoreInfo5R42, string _rdb_swift_R42,

        //MT 202 Changes 02122019
        string _ddlIntermediary202, string _txtIntermediary202AccountNumber, string _txtIntermediary202SwiftCode, string _txtIntermediary202Name,
        string _txtIntermediary202Address1, string _txtIntermediary202Address2, string _txtIntermediary202Address3, string _ddlAccountWithInstitution202,
        string _txtAccountWithInstitution202AccountNumber, string _txtAccountWithInstitution202SwiftCode, string _txtAccountWithInstitution202Name,
        string _txtAccountWithInstitution202Location, string _txtAccountWithInstitution202Address1, string _txtAccountWithInstitution202Address2,
        string _txtAccountWithInstitution202Address3, string _ddlBeneficiaryInstitution202, string _txtBeneficiaryInstitution202AccountNumber,
        string _txtBeneficiaryInstitution202SwiftCode, string _txtBeneficiaryInstitution202Name, string _txtBeneficiaryInstitution202Address1,
        string _txtBeneficiaryInstitution202Address2, string _txtBeneficiaryInstitution202Address3,
        string _txtSenderToReceiverInformation2021, string _txtSenderToReceiverInformation2022, string _txtSenderToReceiverInformation2023, string _txtSenderToReceiverInformation2024,
        string _txtSenderToReceiverInformation2025, string _txtSenderToReceiverInformation2026, string _ddlIntermediary103, string _txtIntermediary103AccountNumber,
        string _txtIntermediary103SwiftCode, string _txtIntermediary103Name, string _txtIntermediary103Address1, string _txtIntermediary103Address2,
        string _txtIntermediary103Address3, string _ddlAccountWithInstitution103, string _txtAccountWithInstitution103AccountNumber,
        string _txtAccountWithInstitution103SwiftCode, string _txtAccountWithInstitution103Name, string _txtAccountWithInstitution103Location,
        string _txtAccountWithInstitution103Address1, string _txtAccountWithInstitution103Address2, string _txtAccountWithInstitution103Address3,
        
         //------------------------------------------------Nilesh---------------------------------------------------------------------------------------------------/
        string _txt_754_SenRef, string _txt_754_RelRef, string MT754_Flag,
        string _ddlPrinAmtPaidAccNego_754, string _txtPrinAmtPaidAccNegoDate_754, string _txtPrinAmtPaidAccNegoCurr_754, string _txtPrinAmtPaidAccNegoAmt_754,
        string _txt_754_AddAmtClamd_Ccy, string _txt_754_AddAmtClamd_Amt,
        string _txt_MT754_Charges_Deducted, string _txt_MT754_Charges_Deducted2, string _txt_MT754_Charges_Deducted3, string _txt_MT754_Charges_Deducted4, string _txt_MT754_Charges_Deducted5, string _txt_MT754_Charges_Deducted6,
        string _txt_MT754_Charges_Added, string _txt_MT754_Charges_Added2, string _txt_MT754_Charges_Added3, string _txt_MT754_Charges_Added4, string _txt_MT754_Charges_Added5, string _txt_MT754_Charges_Added6,
        string _ddlTotalAmtclamd_754, string _txt_754_TotalAmtClmd_Date, string _txt_754_TotalAmtClmd_Ccy, string _txt_754_TotalAmtClmd_Amt,
        string _ddlReimbursingbank_754, string _txtReimbursingBankAccountnumber_754, string _txtReimbursingBankpartyidentifier_754, string _txtReimbursingBankIdentifiercode_754, string _txtReimbursingBankLocation_754,
        string _txtReimbursingBankName_754, string _txtReimbursingBankAddress1_754, string _txtReimbursingBankAddress2_754, string _txtReimbursingBankAddress3_754,
        string _ddlAccountwithbank_754, string _txtAccountwithBankAccountnumber_754, string _txtAccountwithBankpartyidentifier_754, string _txtAccountwithBankIdentifiercode_754,
        string _txtAccountwithBankLocation_754, string _txtAccountwithBankName_754, string _txtAccountwithBankAddress1_754, string _txtAccountwithBankAddress2_754, string _txtAccountwithBankAddress3_754,
        string _ddlBeneficiarybank_754, string _txtBeneficiaryBankAccountnumber_754, string _txtBeneficiarypartyidentifire, string _txtBeneficiaryBankIdentifiercode_754,
        string _txtBeneficiaryBankName_754, string _txtBeneficiaryBankAddress1_754, string _txtBeneficiaryBankAddress2_754, string _txtBeneficiaryBankAddress3_754,
        string _txt_MT754_Sender_to_Receiver_Information, string _txt_MT754_Sender_to_Receiver_Information2, string _txt_MT754_Sender_to_Receiver_Information3, string _txt_MT754_Sender_to_Receiver_Information4,
        string _txt_MT754_Sender_to_Receiver_Information5, string _txt_MT754_Sender_to_Receiver_Information6,

        string _txt_Narrative_754_1,
        string _txt_Narrative_754_2, string _txt_Narrative_754_3, string _txt_Narrative_754_4, string _txt_Narrative_754_5,
        string _txt_Narrative_754_6, string _txt_Narrative_754_7, string _txt_Narrative_754_8, string _txt_Narrative_754_9, string _txt_Narrative_754_10,
        string _txt_Narrative_754_11, string _txt_Narrative_754_12, string _txt_Narrative_754_13, string _txt_Narrative_754_14, string _txt_Narrative_754_15,
        string _txt_Narrative_754_16, string _txt_Narrative_754_17, string _txt_Narrative_754_18, string _txt_Narrative_754_19, string _txt_Narrative_754_20,
        string _txt_Narrative_754_21, string _txt_Narrative_754_22, string _txt_Narrative_754_23, string _txt_Narrative_754_24, string _txt_Narrative_754_25,
        string _txt_Narrative_754_26, string _txt_Narrative_754_27, string _txt_Narrative_754_28, string _txt_Narrative_754_29, string _txt_Narrative_754_30,
        string _txt_Narrative_754_31, string _txt_Narrative_754_32, string _txt_Narrative_754_33, string _txt_Narrative_754_34, string _txt_Narrative_754_35


      //--------------------------------------------------------------Nilesh END-----------------------------------------------------------------------------------------------------------
        )
    {
        TF_DATA obj = new TF_DATA();
        ///////Document Details
        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName.ToUpper());
        SqlParameter P_txtDocNo = new SqlParameter("@Document_No", _txtDocNo.ToUpper());
        SqlParameter P_txt_Doc_Value_Date = new SqlParameter("@Settlement_Date", _txt_Doc_Value_Date.ToUpper());
        SqlParameter P_txt_Doc_Comment_Code = new SqlParameter("@Comment_Code", _txt_Doc_Comment_Code.ToUpper());
        SqlParameter P_txt_Doc_Maturity = new SqlParameter("@Maturity_Date", _txt_Doc_Maturity.ToUpper());
        SqlParameter P_txt_Doc_Settlement_for_Cust = new SqlParameter("@Settlement_For_Cust_Code", _txt_Doc_Settlement_for_Cust.ToUpper());
        SqlParameter P_txt_Doc_Settlement_For_Bank = new SqlParameter("@Settlement_For_Bank_Code", _txt_Doc_Settlement_For_Bank.ToUpper());
        SqlParameter P_txt_Doc_Interest_From = new SqlParameter("@Interest_From_Date", _txt_Doc_Interest_From.ToUpper());
        SqlParameter P_txt_Doc_Interest_To = new SqlParameter("@Interest_To_Date", _txt_Doc_Interest_To.ToUpper());
        SqlParameter P_txt_Doc_No_Of_Days = new SqlParameter("@No_Of_Days", _txt_Doc_No_Of_Days.ToUpper());
        SqlParameter P_txt_Doc_Discount = new SqlParameter("@Discount_Flag", _txt_Doc_Discount.ToUpper());
        SqlParameter P_txt_Doc_InterestRate = new SqlParameter("@Interest_Rate", _txt_Doc_InterestRate.ToUpper());
        SqlParameter P_txt_Doc_InterestAmount = new SqlParameter("@Interest_Amount", _txt_Doc_InterestAmount.ToUpper());
        SqlParameter P_txt_Doc_Overdue_Interestrate = new SqlParameter("@Overdue_Interest_Rate", _txt_Doc_Overdue_Interestrate.ToUpper());
        SqlParameter P_txt_Doc_Oveduenoofdays = new SqlParameter("@Overdue_No_Of_Days", _txt_Doc_Oveduenoofdays.ToUpper());
        SqlParameter P_txt_Doc_Overdueamount = new SqlParameter("@Overdue_Interest_Amount", _txt_Doc_Overdueamount.ToUpper());
        SqlParameter P_txt_Doc_Attn = new SqlParameter("@Attn", _txt_Doc_Attn.ToUpper());

        /////IMPORT ACCOUNTING 1
        SqlParameter P_chk_IMPACC1Flag = new SqlParameter("@IMP_ACC1_Flag", _chk_IMPACC1Flag.ToUpper());
        SqlParameter P_txt_IMPACC1_FCRefNo = new SqlParameter("@IMP_ACC1_FCRefNo", _txt_IMPACC1_FCRefNo.ToUpper());
        SqlParameter P_txt_IMPACC1_DiscAmt = new SqlParameter("@IMP_ACC1_Amount", _txt_IMPACC1_DiscAmt.ToUpper());
        SqlParameter P_txt_IMPACC1_DiscExchRate = new SqlParameter("@IMP_ACC1_ExchRate", _txt_IMPACC1_DiscExchRate.ToUpper());
        SqlParameter P_txt_IMPACC1_Princ_matu = new SqlParameter("@IMP_ACC1_Principal_MATU", _txt_IMPACC1_Princ_matu.ToUpper());
        SqlParameter P_txt_IMPACC1_Princ_lump = new SqlParameter("@IMP_ACC1_Principal_LUMP", _txt_IMPACC1_Princ_lump.ToUpper());
        SqlParameter P_txt_IMPACC1_Princ_Contract_no = new SqlParameter("@IMP_ACC1_Principal_Contract_No", _txt_IMPACC1_Princ_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC1_Princ_Ex_Curr = new SqlParameter("@IMP_ACC1_Principal_Ex_Curr", _txt_IMPACC1_Princ_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_Princ_Ex_rate = new SqlParameter("@IMP_ACC1_Principal_Exch_Rate", _txt_IMPACC1_Princ_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Principal_Intnl_Exch_Rate", _txt_IMPACC1_Princ_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_Interest_matu = new SqlParameter("@IMP_ACC1_Interest_MATU", _txt_IMPACC1_Interest_matu.ToUpper());
        SqlParameter P_txt_IMPACC1_Interest_lump = new SqlParameter("@IMP_ACC1_Interest_LUMP", _txt_IMPACC1_Interest_lump.ToUpper());
        SqlParameter P_txt_IMPACC1_Interest_Contract_no = new SqlParameter("@IMP_ACC1_Interest_Contract_No", _txt_IMPACC1_Interest_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC1_Interest_Ex_Curr = new SqlParameter("@IMP_ACC1_Interest_Ex_Curr", _txt_IMPACC1_Interest_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_Interest_Ex_rate = new SqlParameter("@IMP_ACC1_Interest_Exch_Rate", _txt_IMPACC1_Interest_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Interest_Intnl_Exch_Rate", _txt_IMPACC1_Interest_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_Commission_matu = new SqlParameter("@IMP_ACC1_Commission_MATU", _txt_IMPACC1_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC1_Commission_lump = new SqlParameter("@IMP_ACC1_Commission_LUMP", _txt_IMPACC1_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC1_Commission_Contract_no = new SqlParameter("@IMP_ACC1_Commission_Contract_No", _txt_IMPACC1_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC1_Commission_Ex_Curr = new SqlParameter("@IMP_ACC1_Commission_Ex_Curr", _txt_IMPACC1_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_Commission_Ex_rate = new SqlParameter("@IMP_ACC1_Commission_Exch_Rate", _txt_IMPACC1_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Commission_Intnl_Exch_Rate", _txt_IMPACC1_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_Their_Commission_matu = new SqlParameter("@IMP_ACC1_Their_Commission_MATU", _txt_IMPACC1_Their_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC1_Their_Commission_lump = new SqlParameter("@IMP_ACC1_Their_Commission_LUMP", _txt_IMPACC1_Their_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC1_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC1_Their_Commission_Contract_No", _txt_IMPACC1_Their_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC1_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC1_Their_Commission_Ex_Curr", _txt_IMPACC1_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC1_Their_Commission_Exch_Rate", _txt_IMPACC1_Their_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Their_Commission_Intnl_Exch_Rate", _txt_IMPACC1_Their_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Code = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Code", _txt_IMPACC1_CR_Code.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_AC_Short_Name = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Short_Name", _txt_IMPACC1_CR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Cust_abbr = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Cust_Abbr", _txt_IMPACC1_CR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Cust_Acc = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Cust_Acc_No", _txt_IMPACC1_CR_Cust_Acc.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Curr", _txt_IMPACC1_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Acceptance_amt = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Amt", _txt_IMPACC1_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Acceptance_payer = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Payer", _txt_IMPACC1_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Interest_Curr = new SqlParameter("@IMP_ACC1_CR_Interest_Curr", _txt_IMPACC1_CR_Interest_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Interest_amt = new SqlParameter("@IMP_ACC1_CR_Interest_Amount", _txt_IMPACC1_CR_Interest_amt.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Interest_payer = new SqlParameter("@IMP_ACC1_CR_Interest_Payer", _txt_IMPACC1_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Curr", _txt_IMPACC1_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Amount", _txt_IMPACC1_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Payer", _txt_IMPACC1_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Curr", _txt_IMPACC1_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Amount", _txt_IMPACC1_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Payer", _txt_IMPACC1_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Others_Curr = new SqlParameter("@IMP_ACC1_CR_Others_Curr", _txt_IMPACC1_CR_Others_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Others_amt = new SqlParameter("@IMP_ACC1_CR_Others_Amount", _txt_IMPACC1_CR_Others_amt.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Others_Payer = new SqlParameter("@IMP_ACC1_CR_Others_Payer", _txt_IMPACC1_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Curr", _txt_IMPACC1_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Amount", _txt_IMPACC1_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC1_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Payer", _txt_IMPACC1_CR_Their_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Code = new SqlParameter("@IMP_ACC1_DR_Code", _txt_IMPACC1_DR_Code.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_AC_Short_Name = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name", _txt_IMPACC1_DR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_abbr = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr", _txt_IMPACC1_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_Acc = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No", _txt_IMPACC1_DR_Cust_Acc.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr", _txt_IMPACC1_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount", _txt_IMPACC1_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer", _txt_IMPACC1_DR_Cur_Acc_payer.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr2", _txt_IMPACC1_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount2", _txt_IMPACC1_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer2", _txt_IMPACC1_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr3", _txt_IMPACC1_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount3", _txt_IMPACC1_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer3", _txt_IMPACC1_DR_Cur_Acc_payer3.ToUpper());

        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr4", _txt_IMPACC1_DR_Cur_Acc_Curr4.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount4", _txt_IMPACC1_DR_Cur_Acc_amt4.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer4", _txt_IMPACC1_DR_Cur_Acc_payer4.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr5", _txt_IMPACC1_DR_Cur_Acc_Curr5.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount5", _txt_IMPACC1_DR_Cur_Acc_amt5.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer5", _txt_IMPACC1_DR_Cur_Acc_payer5.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr6", _txt_IMPACC1_DR_Cur_Acc_Curr6.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount6", _txt_IMPACC1_DR_Cur_Acc_amt6.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer6", _txt_IMPACC1_DR_Cur_Acc_payer6.ToUpper());

        SqlParameter P_txt_IMPACC1_DR_Code2 = new SqlParameter("@IMP_ACC1_DR_Code2", _txt_IMPACC1_DR_Code2.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name2", _txt_IMPACC1_DR_AC_Short_Name2.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr2", _txt_IMPACC1_DR_Cust_abbr2.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No2", _txt_IMPACC1_DR_Cust_Acc2.ToUpper());

        SqlParameter P_txt_IMPACC1_DR_Code3 = new SqlParameter("@IMP_ACC1_DR_Code3", _txt_IMPACC1_DR_Code3.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name3", _txt_IMPACC1_DR_AC_Short_Name3.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr3", _txt_IMPACC1_DR_Cust_abbr3.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No3", _txt_IMPACC1_DR_Cust_Acc3.ToUpper());

        SqlParameter P_txt_IMPACC1_DR_Code4 = new SqlParameter("@IMP_ACC1_DR_Code4", _txt_IMPACC1_DR_Code4.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name4", _txt_IMPACC1_DR_AC_Short_Name4.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr4", _txt_IMPACC1_DR_Cust_abbr4.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No4", _txt_IMPACC1_DR_Cust_Acc4.ToUpper());

        SqlParameter P_txt_IMPACC1_DR_Code5 = new SqlParameter("@IMP_ACC1_DR_Code5", _txt_IMPACC1_DR_Code5.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name5", _txt_IMPACC1_DR_AC_Short_Name5.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr5", _txt_IMPACC1_DR_Cust_abbr5.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No5", _txt_IMPACC1_DR_Cust_Acc5.ToUpper());

        SqlParameter P_txt_IMPACC1_DR_Code6 = new SqlParameter("@IMP_ACC1_DR_Code6", _txt_IMPACC1_DR_Code6.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name6", _txt_IMPACC1_DR_AC_Short_Name6.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr6", _txt_IMPACC1_DR_Cust_abbr6.ToUpper());
        SqlParameter P_txt_IMPACC1_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No6", _txt_IMPACC1_DR_Cust_Acc6.ToUpper());


        ////IMPORT ACCOUNTING 2
        SqlParameter P_chk_IMPACC2Flag = new SqlParameter("@IMP_ACC2_Flag", _chk_IMPACC2Flag.ToUpper());
        SqlParameter P_txt_IMPACC2_FCRefNo = new SqlParameter("@IMP_ACC2_FCRefNo", _txt_IMPACC2_FCRefNo.ToUpper());
        SqlParameter P_txt_IMPACC2_DiscAmt = new SqlParameter("@IMP_ACC2_Amount", _txt_IMPACC2_DiscAmt.ToUpper());
        SqlParameter P_txt_IMPACC2_DiscExchRate = new SqlParameter("@IMP_ACC2_ExchRate", _txt_IMPACC2_DiscExchRate.ToUpper());
        SqlParameter P_txt_IMPACC2_Princ_matu = new SqlParameter("@IMP_ACC2_Principal_MATU", _txt_IMPACC2_Princ_matu.ToUpper());
        SqlParameter P_txt_IMPACC2_Princ_lump = new SqlParameter("@IMP_ACC2_Principal_LUMP", _txt_IMPACC2_Princ_lump.ToUpper());
        SqlParameter P_txt_IMPACC2_Princ_Contract_no = new SqlParameter("@IMP_ACC2_Principal_Contract_No", _txt_IMPACC2_Princ_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC2_Princ_Ex_Curr = new SqlParameter("@IMP_ACC2_Principal_Ex_Curr", _txt_IMPACC2_Princ_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_Princ_Ex_rate = new SqlParameter("@IMP_ACC2_Principal_Exch_Rate", _txt_IMPACC2_Princ_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC2_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Principal_Intnl_Exch_Rate", _txt_IMPACC2_Princ_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC2_Interest_matu = new SqlParameter("@IMP_ACC2_Interest_MATU", _txt_IMPACC2_Interest_matu.ToUpper());
        SqlParameter P_txt_IMPACC2_Interest_lump = new SqlParameter("@IMP_ACC2_Interest_LUMP", _txt_IMPACC2_Interest_lump.ToUpper());
        SqlParameter P_txt_IMPACC2_Interest_Contract_no = new SqlParameter("@IMP_ACC2_Interest_Contract_No", _txt_IMPACC2_Interest_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC2_Interest_Ex_Curr = new SqlParameter("@IMP_ACC2_Interest_Ex_Curr", _txt_IMPACC2_Interest_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_Interest_Ex_rate = new SqlParameter("@IMP_ACC2_Interest_Exch_Rate", _txt_IMPACC2_Interest_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC2_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Interest_Intnl_Exch_Rate", _txt_IMPACC2_Interest_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC2_Commission_matu = new SqlParameter("@IMP_ACC2_Commission_MATU", _txt_IMPACC2_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC2_Commission_lump = new SqlParameter("@IMP_ACC2_Commission_LUMP", _txt_IMPACC2_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC2_Commission_Contract_no = new SqlParameter("@IMP_ACC2_Commission_Contract_No", _txt_IMPACC2_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC2_Commission_Ex_Curr = new SqlParameter("@IMP_ACC2_Commission_Ex_Curr", _txt_IMPACC2_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_Commission_Ex_rate = new SqlParameter("@IMP_ACC2_Commission_Exch_Rate", _txt_IMPACC2_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC2_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Commission_Intnl_Exch_Rate", _txt_IMPACC2_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC2_Their_Commission_matu = new SqlParameter("@IMP_ACC2_Their_Commission_MATU", _txt_IMPACC2_Their_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC2_Their_Commission_lump = new SqlParameter("@IMP_ACC2_Their_Commission_LUMP", _txt_IMPACC2_Their_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC2_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC2_Their_Commission_Contract_No", _txt_IMPACC2_Their_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC2_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC2_Their_Commission_Ex_Curr", _txt_IMPACC2_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC2_Their_Commission_Exch_Rate", _txt_IMPACC2_Their_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC2_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Their_Commission_Intnl_Exch_Rate", _txt_IMPACC2_Their_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Code = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Code", _txt_IMPACC2_CR_Code.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_AC_Short_Name = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Short_Name", _txt_IMPACC2_CR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Cust_abbr = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Cust_Abbr", _txt_IMPACC2_CR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Cust_Acc = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Cust_Acc_No", _txt_IMPACC2_CR_Cust_Acc.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Curr", _txt_IMPACC2_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Acceptance_amt = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Amt", _txt_IMPACC2_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Acceptance_payer = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Payer", _txt_IMPACC2_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Interest_Curr = new SqlParameter("@IMP_ACC2_CR_Interest_Curr", _txt_IMPACC2_CR_Interest_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Interest_amt = new SqlParameter("@IMP_ACC2_CR_Interest_Amount", _txt_IMPACC2_CR_Interest_amt.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Interest_payer = new SqlParameter("@IMP_ACC2_CR_Interest_Payer", _txt_IMPACC2_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Curr", _txt_IMPACC2_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Amount", _txt_IMPACC2_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Payer", _txt_IMPACC2_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Curr", _txt_IMPACC2_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Amount", _txt_IMPACC2_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Payer", _txt_IMPACC2_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Others_Curr = new SqlParameter("@IMP_ACC2_CR_Others_Curr", _txt_IMPACC2_CR_Others_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Others_amt = new SqlParameter("@IMP_ACC2_CR_Others_Amount", _txt_IMPACC2_CR_Others_amt.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Others_Payer = new SqlParameter("@IMP_ACC2_CR_Others_Payer", _txt_IMPACC2_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Curr", _txt_IMPACC2_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Amount", _txt_IMPACC2_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC2_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Payer", _txt_IMPACC2_CR_Their_Commission_Payer.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Code = new SqlParameter("@IMP_ACC2_DR_Code", _txt_IMPACC2_DR_Code.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_AC_Short_Name = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name", _txt_IMPACC2_DR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_abbr = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr", _txt_IMPACC2_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_Acc = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No", _txt_IMPACC2_DR_Cust_Acc.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr", _txt_IMPACC2_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount", _txt_IMPACC2_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer", _txt_IMPACC2_DR_Cur_Acc_payer.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr2", _txt_IMPACC2_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount2", _txt_IMPACC2_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer2", _txt_IMPACC2_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr3", _txt_IMPACC2_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount3", _txt_IMPACC2_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer3", _txt_IMPACC2_DR_Cur_Acc_payer3.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr4", _txt_IMPACC2_DR_Cur_Acc_Curr4.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount4", _txt_IMPACC2_DR_Cur_Acc_amt4.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer4", _txt_IMPACC2_DR_Cur_Acc_payer4.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr5", _txt_IMPACC2_DR_Cur_Acc_Curr5.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount5", _txt_IMPACC2_DR_Cur_Acc_amt5.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer5", _txt_IMPACC2_DR_Cur_Acc_payer5.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr6", _txt_IMPACC2_DR_Cur_Acc_Curr6.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount6", _txt_IMPACC2_DR_Cur_Acc_amt6.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer6", _txt_IMPACC2_DR_Cur_Acc_payer6.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Code2 = new SqlParameter("@IMP_ACC2_DR_Code2", _txt_IMPACC2_DR_Code2.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name2", _txt_IMPACC2_DR_AC_Short_Name2.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr2", _txt_IMPACC2_DR_Cust_abbr2.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No2", _txt_IMPACC2_DR_Cust_Acc2.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Code3 = new SqlParameter("@IMP_ACC2_DR_Code3", _txt_IMPACC2_DR_Code3.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name3", _txt_IMPACC2_DR_AC_Short_Name3.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr3", _txt_IMPACC2_DR_Cust_abbr3.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No3", _txt_IMPACC2_DR_Cust_Acc3.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Code4 = new SqlParameter("@IMP_ACC2_DR_Code4", _txt_IMPACC2_DR_Code4.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name4", _txt_IMPACC2_DR_AC_Short_Name4.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr4", _txt_IMPACC2_DR_Cust_abbr4.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No4", _txt_IMPACC2_DR_Cust_Acc4.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Code5 = new SqlParameter("@IMP_ACC2_DR_Code5", _txt_IMPACC2_DR_Code5.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name5", _txt_IMPACC2_DR_AC_Short_Name5.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr5", _txt_IMPACC2_DR_Cust_abbr5.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No5", _txt_IMPACC2_DR_Cust_Acc5.ToUpper());

        SqlParameter P_txt_IMPACC2_DR_Code6 = new SqlParameter("@IMP_ACC2_DR_Code6", _txt_IMPACC2_DR_Code6.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name6", _txt_IMPACC2_DR_AC_Short_Name6.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr6", _txt_IMPACC2_DR_Cust_abbr6.ToUpper());
        SqlParameter P_txt_IMPACC2_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No6", _txt_IMPACC2_DR_Cust_Acc6.ToUpper());

        //////IMPORT ACCOUNTING  3
        SqlParameter P_chk_IMPACC3Flag = new SqlParameter("@IMP_ACC3_Flag", _chk_IMPACC3Flag.ToUpper());
        SqlParameter P_txt_IMPACC3_FCRefNo = new SqlParameter("@IMP_ACC3_FCRefNo", _txt_IMPACC3_FCRefNo.ToUpper());
        SqlParameter P_txt_IMPACC3_DiscAmt = new SqlParameter("@IMP_ACC3_Amount", _txt_IMPACC3_DiscAmt.ToUpper());
        SqlParameter P_txt_IMPACC3_DiscExchRate = new SqlParameter("@IMP_ACC3_ExchRate", _txt_IMPACC3_DiscExchRate.ToUpper());
        SqlParameter P_txt_IMPACC3_Princ_matu = new SqlParameter("@IMP_ACC3_Principal_MATU", _txt_IMPACC3_Princ_matu.ToUpper());
        SqlParameter P_txt_IMPACC3_Princ_lump = new SqlParameter("@IMP_ACC3_Principal_LUMP", _txt_IMPACC3_Princ_lump.ToUpper());
        SqlParameter P_txt_IMPACC3_Princ_Contract_no = new SqlParameter("@IMP_ACC3_Principal_Contract_No", _txt_IMPACC3_Princ_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC3_Princ_Ex_Curr = new SqlParameter("@IMP_ACC3_Principal_Ex_Curr", _txt_IMPACC3_Princ_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_Princ_Ex_rate = new SqlParameter("@IMP_ACC3_Principal_Exch_Rate", _txt_IMPACC3_Princ_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC3_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Principal_Intnl_Exch_Rate", _txt_IMPACC3_Princ_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC3_Interest_matu = new SqlParameter("@IMP_ACC3_Interest_MATU", _txt_IMPACC3_Interest_matu.ToUpper());
        SqlParameter P_txt_IMPACC3_Interest_lump = new SqlParameter("@IMP_ACC3_Interest_LUMP", _txt_IMPACC3_Interest_lump.ToUpper());
        SqlParameter P_txt_IMPACC3_Interest_Contract_no = new SqlParameter("@IMP_ACC3_Interest_Contract_No", _txt_IMPACC3_Interest_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC3_Interest_Ex_Curr = new SqlParameter("@IMP_ACC3_Interest_Ex_Curr", _txt_IMPACC3_Interest_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_Interest_Ex_rate = new SqlParameter("@IMP_ACC3_Interest_Exch_Rate", _txt_IMPACC3_Interest_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC3_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Interest_Intnl_Exch_Rate", _txt_IMPACC3_Interest_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC3_Commission_matu = new SqlParameter("@IMP_ACC3_Commission_MATU", _txt_IMPACC3_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC3_Commission_lump = new SqlParameter("@IMP_ACC3_Commission_LUMP", _txt_IMPACC3_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC3_Commission_Contract_no = new SqlParameter("@IMP_ACC3_Commission_Contract_No", _txt_IMPACC3_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC3_Commission_Ex_Curr = new SqlParameter("@IMP_ACC3_Commission_Ex_Curr", _txt_IMPACC3_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_Commission_Ex_rate = new SqlParameter("@IMP_ACC3_Commission_Exch_Rate", _txt_IMPACC3_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC3_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Commission_Intnl_Exch_Rate", _txt_IMPACC3_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC3_Their_Commission_matu = new SqlParameter("@IMP_ACC3_Their_Commission_MATU", _txt_IMPACC3_Their_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC3_Their_Commission_lump = new SqlParameter("@IMP_ACC3_Their_Commission_LUMP", _txt_IMPACC3_Their_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC3_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC3_Their_Commission_Contract_No", _txt_IMPACC3_Their_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC3_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC3_Their_Commission_Ex_Curr", _txt_IMPACC3_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC3_Their_Commission_Exch_Rate", _txt_IMPACC3_Their_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC3_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Their_Commission_Intnl_Exch_Rate", _txt_IMPACC3_Their_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Code = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Code", _txt_IMPACC3_CR_Code.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_AC_Short_Name = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Short_Name", _txt_IMPACC3_CR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Cust_abbr = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Cust_Abbr", _txt_IMPACC3_CR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Cust_Acc = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Cust_Acc_No", _txt_IMPACC3_CR_Cust_Acc.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Curr", _txt_IMPACC3_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Acceptance_amt = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Amt", _txt_IMPACC3_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Acceptance_payer = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Payer", _txt_IMPACC3_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Interest_Curr = new SqlParameter("@IMP_ACC3_CR_Interest_Curr", _txt_IMPACC3_CR_Interest_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Interest_amt = new SqlParameter("@IMP_ACC3_CR_Interest_Amount", _txt_IMPACC3_CR_Interest_amt.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Interest_payer = new SqlParameter("@IMP_ACC3_CR_Interest_Payer", _txt_IMPACC3_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Curr", _txt_IMPACC3_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Amount", _txt_IMPACC3_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Payer", _txt_IMPACC3_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Curr", _txt_IMPACC3_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Amount", _txt_IMPACC3_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Payer", _txt_IMPACC3_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Others_Curr = new SqlParameter("@IMP_ACC3_CR_Others_Curr", _txt_IMPACC3_CR_Others_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Others_amt = new SqlParameter("@IMP_ACC3_CR_Others_Amount", _txt_IMPACC3_CR_Others_amt.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Others_Payer = new SqlParameter("@IMP_ACC3_CR_Others_Payer", _txt_IMPACC3_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Curr", _txt_IMPACC3_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Amount", _txt_IMPACC3_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC3_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Payer", _txt_IMPACC3_CR_Their_Commission_Payer.ToUpper());

        SqlParameter P_txt_IMPACC3_DR_Code = new SqlParameter("@IMP_ACC3_DR_Code", _txt_IMPACC3_DR_Code.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_AC_Short_Name = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name", _txt_IMPACC3_DR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_abbr = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr", _txt_IMPACC3_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_Acc = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No", _txt_IMPACC3_DR_Cust_Acc.ToUpper());

        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr", _txt_IMPACC3_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount", _txt_IMPACC3_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer", _txt_IMPACC3_DR_Cur_Acc_payer.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr2", _txt_IMPACC3_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount2", _txt_IMPACC3_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer2", _txt_IMPACC3_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr3", _txt_IMPACC3_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount3", _txt_IMPACC3_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer3", _txt_IMPACC3_DR_Cur_Acc_payer3.ToUpper());

        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr4", _txt_IMPACC3_DR_Cur_Acc_Curr4.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount4", _txt_IMPACC3_DR_Cur_Acc_amt4.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer4", _txt_IMPACC3_DR_Cur_Acc_payer4.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr5", _txt_IMPACC3_DR_Cur_Acc_Curr5.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount5", _txt_IMPACC3_DR_Cur_Acc_amt5.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer5", _txt_IMPACC3_DR_Cur_Acc_payer5.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr6", _txt_IMPACC3_DR_Cur_Acc_Curr6.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount6", _txt_IMPACC3_DR_Cur_Acc_amt6.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer6", _txt_IMPACC3_DR_Cur_Acc_payer6.ToUpper());


        SqlParameter P_txt_IMPACC3_DR_Code2 = new SqlParameter("@IMP_ACC3_DR_Code2", _txt_IMPACC3_DR_Code2.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name2", _txt_IMPACC3_DR_AC_Short_Name2.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr2", _txt_IMPACC3_DR_Cust_abbr2.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No2", _txt_IMPACC3_DR_Cust_Acc2.ToUpper());

        SqlParameter P_txt_IMPACC3_DR_Code3 = new SqlParameter("@IMP_ACC3_DR_Code3", _txt_IMPACC3_DR_Code3.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name3", _txt_IMPACC3_DR_AC_Short_Name3.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr3", _txt_IMPACC3_DR_Cust_abbr3.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No3", _txt_IMPACC3_DR_Cust_Acc3.ToUpper());

        SqlParameter P_txt_IMPACC3_DR_Code4 = new SqlParameter("@IMP_ACC3_DR_Code4", _txt_IMPACC3_DR_Code4.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name4", _txt_IMPACC3_DR_AC_Short_Name4.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr4", _txt_IMPACC3_DR_Cust_abbr4.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No4", _txt_IMPACC3_DR_Cust_Acc4.ToUpper());

        SqlParameter P_txt_IMPACC3_DR_Code5 = new SqlParameter("@IMP_ACC3_DR_Code5", _txt_IMPACC3_DR_Code5.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name5", _txt_IMPACC3_DR_AC_Short_Name5.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr5", _txt_IMPACC3_DR_Cust_abbr5.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No5", _txt_IMPACC3_DR_Cust_Acc5.ToUpper());

        SqlParameter P_txt_IMPACC3_DR_Code6 = new SqlParameter("@IMP_ACC3_DR_Code6", _txt_IMPACC3_DR_Code6.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name6", _txt_IMPACC3_DR_AC_Short_Name6.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr6", _txt_IMPACC3_DR_Cust_abbr6.ToUpper());
        SqlParameter P_txt_IMPACC3_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No6", _txt_IMPACC3_DR_Cust_Acc6.ToUpper());

        //////IMPORT ACCOUNTING 4
        SqlParameter P_chk_IMPACC4Flag = new SqlParameter("@IMP_ACC4_Flag", _chk_IMPACC4Flag.ToUpper());
        SqlParameter P_txt_IMPACC4_FCRefNo = new SqlParameter("@IMP_ACC4_FCRefNo", _txt_IMPACC4_FCRefNo.ToUpper());
        SqlParameter P_txt_IMPACC4_DiscAmt = new SqlParameter("@IMP_ACC4_Amount", _txt_IMPACC4_DiscAmt.ToUpper());
        SqlParameter P_txt_IMPACC4_DiscExchRate = new SqlParameter("@IMP_ACC4_ExchRate", _txt_IMPACC4_DiscExchRate.ToUpper());
        SqlParameter P_txt_IMPACC4_Princ_matu = new SqlParameter("@IMP_ACC4_Principal_MATU", _txt_IMPACC4_Princ_matu.ToUpper());
        SqlParameter P_txt_IMPACC4_Princ_lump = new SqlParameter("@IMP_ACC4_Principal_LUMP", _txt_IMPACC4_Princ_lump.ToUpper());
        SqlParameter P_txt_IMPACC4_Princ_Contract_no = new SqlParameter("@IMP_ACC4_Principal_Contract_No", _txt_IMPACC4_Princ_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC4_Princ_Ex_Curr = new SqlParameter("@IMP_ACC4_Principal_Ex_Curr", _txt_IMPACC4_Princ_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_Princ_Ex_rate = new SqlParameter("@IMP_ACC4_Principal_Exch_Rate", _txt_IMPACC4_Princ_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC4_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Principal_Intnl_Exch_Rate", _txt_IMPACC4_Princ_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC4_Interest_matu = new SqlParameter("@IMP_ACC4_Interest_MATU", _txt_IMPACC4_Interest_matu.ToUpper());
        SqlParameter P_txt_IMPACC4_Interest_lump = new SqlParameter("@IMP_ACC4_Interest_LUMP", _txt_IMPACC4_Interest_lump.ToUpper());
        SqlParameter P_txt_IMPACC4_Interest_Contract_no = new SqlParameter("@IMP_ACC4_Interest_Contract_No", _txt_IMPACC4_Interest_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC4_Interest_Ex_Curr = new SqlParameter("@IMP_ACC4_Interest_Ex_Curr", _txt_IMPACC4_Interest_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_Interest_Ex_rate = new SqlParameter("@IMP_ACC4_Interest_Exch_Rate", _txt_IMPACC4_Interest_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC4_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Interest_Intnl_Exch_Rate", _txt_IMPACC4_Interest_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC4_Commission_matu = new SqlParameter("@IMP_ACC4_Commission_MATU", _txt_IMPACC4_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC4_Commission_lump = new SqlParameter("@IMP_ACC4_Commission_LUMP", _txt_IMPACC4_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC4_Commission_Contract_no = new SqlParameter("@IMP_ACC4_Commission_Contract_No", _txt_IMPACC4_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC4_Commission_Ex_Curr = new SqlParameter("@IMP_ACC4_Commission_Ex_Curr", _txt_IMPACC4_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_Commission_Ex_rate = new SqlParameter("@IMP_ACC4_Commission_Exch_Rate", _txt_IMPACC4_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC4_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Commission_Intnl_Exch_Rate", _txt_IMPACC4_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC4_Their_Commission_matu = new SqlParameter("@IMP_ACC4_Their_Commission_MATU", _txt_IMPACC4_Their_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC4_Their_Commission_lump = new SqlParameter("@IMP_ACC4_Their_Commission_LUMP", _txt_IMPACC4_Their_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC4_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC4_Their_Commission_Contract_No", _txt_IMPACC4_Their_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC4_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC4_Their_Commission_Ex_Curr", _txt_IMPACC4_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC4_Their_Commission_Exch_Rate", _txt_IMPACC4_Their_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC4_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Their_Commission_Intnl_Exch_Rate", _txt_IMPACC4_Their_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Code = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Code", _txt_IMPACC4_CR_Code.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_AC_Short_Name = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Short_Name", _txt_IMPACC4_CR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Cust_abbr = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Cust_Abbr", _txt_IMPACC4_CR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Cust_Acc = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Cust_Acc_No", _txt_IMPACC4_CR_Cust_Acc.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Curr", _txt_IMPACC4_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Acceptance_amt = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Amt", _txt_IMPACC4_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Acceptance_payer = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Payer", _txt_IMPACC4_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Interest_Curr = new SqlParameter("@IMP_ACC4_CR_Interest_Curr", _txt_IMPACC4_CR_Interest_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Interest_amt = new SqlParameter("@IMP_ACC4_CR_Interest_Amount", _txt_IMPACC4_CR_Interest_amt.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Interest_payer = new SqlParameter("@IMP_ACC4_CR_Interest_Payer", _txt_IMPACC4_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Curr", _txt_IMPACC4_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Amount", _txt_IMPACC4_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Payer", _txt_IMPACC4_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Curr", _txt_IMPACC4_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Amount", _txt_IMPACC4_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Payer", _txt_IMPACC4_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Others_Curr = new SqlParameter("@IMP_ACC4_CR_Others_Curr", _txt_IMPACC4_CR_Others_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Others_amt = new SqlParameter("@IMP_ACC4_CR_Others_Amount", _txt_IMPACC4_CR_Others_amt.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Others_Payer = new SqlParameter("@IMP_ACC4_CR_Others_Payer", _txt_IMPACC4_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Curr", _txt_IMPACC4_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Amount", _txt_IMPACC4_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC4_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Payer", _txt_IMPACC4_CR_Their_Commission_Payer.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Code = new SqlParameter("@IMP_ACC4_DR_Code", _txt_IMPACC4_DR_Code.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_AC_Short_Name = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name", _txt_IMPACC4_DR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_abbr = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr", _txt_IMPACC4_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_Acc = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No", _txt_IMPACC4_DR_Cust_Acc.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr", _txt_IMPACC4_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount", _txt_IMPACC4_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer", _txt_IMPACC4_DR_Cur_Acc_payer.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr2", _txt_IMPACC4_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount2", _txt_IMPACC4_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer2", _txt_IMPACC4_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr3", _txt_IMPACC4_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount3", _txt_IMPACC4_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer3", _txt_IMPACC4_DR_Cur_Acc_payer3.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr4", _txt_IMPACC4_DR_Cur_Acc_Curr4.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount4", _txt_IMPACC4_DR_Cur_Acc_amt4.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer4", _txt_IMPACC4_DR_Cur_Acc_payer4.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr5", _txt_IMPACC4_DR_Cur_Acc_Curr5.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount5", _txt_IMPACC4_DR_Cur_Acc_amt5.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer5", _txt_IMPACC4_DR_Cur_Acc_payer5.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr6", _txt_IMPACC4_DR_Cur_Acc_Curr6.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount6", _txt_IMPACC4_DR_Cur_Acc_amt6.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer6", _txt_IMPACC4_DR_Cur_Acc_payer6.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Code2 = new SqlParameter("@IMP_ACC4_DR_Code2", _txt_IMPACC4_DR_Code2.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name2", _txt_IMPACC4_DR_AC_Short_Name2.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr2", _txt_IMPACC4_DR_Cust_abbr2.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No2", _txt_IMPACC4_DR_Cust_Acc2.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Code3 = new SqlParameter("@IMP_ACC4_DR_Code3", _txt_IMPACC4_DR_Code3.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name3", _txt_IMPACC4_DR_AC_Short_Name3.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr3", _txt_IMPACC4_DR_Cust_abbr3.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No3", _txt_IMPACC4_DR_Cust_Acc3.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Code4 = new SqlParameter("@IMP_ACC4_DR_Code4", _txt_IMPACC4_DR_Code4.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name4", _txt_IMPACC4_DR_AC_Short_Name4.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr4", _txt_IMPACC4_DR_Cust_abbr4.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No4", _txt_IMPACC4_DR_Cust_Acc4.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Code5 = new SqlParameter("@IMP_ACC4_DR_Code5", _txt_IMPACC4_DR_Code5.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name5", _txt_IMPACC4_DR_AC_Short_Name5.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr5", _txt_IMPACC4_DR_Cust_abbr5.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No5", _txt_IMPACC4_DR_Cust_Acc5.ToUpper());

        SqlParameter P_txt_IMPACC4_DR_Code6 = new SqlParameter("@IMP_ACC4_DR_Code6", _txt_IMPACC4_DR_Code6.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name6", _txt_IMPACC4_DR_AC_Short_Name6.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr6", _txt_IMPACC4_DR_Cust_abbr6.ToUpper());
        SqlParameter P_txt_IMPACC4_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No6", _txt_IMPACC4_DR_Cust_Acc6.ToUpper());

        ///////IMPORT ACCOUNTING 5
        SqlParameter P_chk_IMPACC5Flag = new SqlParameter("@IMP_ACC5_Flag", _chk_IMPACC5Flag.ToUpper());
        SqlParameter P_txt_IMPACC5_FCRefNo = new SqlParameter("@IMP_ACC5_FCRefNo", _txt_IMPACC5_FCRefNo.ToUpper());
        SqlParameter P_txt_IMPACC5_DiscAmt = new SqlParameter("@IMP_ACC5_Amount", _txt_IMPACC5_DiscAmt.ToUpper());
        SqlParameter P_txt_IMPACC5_DiscExchRate = new SqlParameter("@IMP_ACC5_ExchRate", _txt_IMPACC5_DiscExchRate.ToUpper());
        SqlParameter P_txt_IMPACC5_Princ_matu = new SqlParameter("@IMP_ACC5_Principal_MATU", _txt_IMPACC5_Princ_matu.ToUpper());
        SqlParameter P_txt_IMPACC5_Princ_lump = new SqlParameter("@IMP_ACC5_Principal_LUMP", _txt_IMPACC5_Princ_lump.ToUpper());
        SqlParameter P_txt_IMPACC5_Princ_Contract_no = new SqlParameter("@IMP_ACC5_Principal_Contract_No", _txt_IMPACC5_Princ_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC5_Princ_Ex_Curr = new SqlParameter("@IMP_ACC5_Principal_Ex_Curr", _txt_IMPACC5_Princ_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_Princ_Ex_rate = new SqlParameter("@IMP_ACC5_Principal_Exch_Rate", _txt_IMPACC5_Princ_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC5_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Principal_Intnl_Exch_Rate", _txt_IMPACC5_Princ_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC5_Interest_matu = new SqlParameter("@IMP_ACC5_Interest_MATU", _txt_IMPACC5_Interest_matu.ToUpper());
        SqlParameter P_txt_IMPACC5_Interest_lump = new SqlParameter("@IMP_ACC5_Interest_LUMP", _txt_IMPACC5_Interest_lump.ToUpper());
        SqlParameter P_txt_IMPACC5_Interest_Contract_no = new SqlParameter("@IMP_ACC5_Interest_Contract_No", _txt_IMPACC5_Interest_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC5_Interest_Ex_Curr = new SqlParameter("@IMP_ACC5_Interest_Ex_Curr", _txt_IMPACC5_Interest_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_Interest_Ex_rate = new SqlParameter("@IMP_ACC5_Interest_Exch_Rate", _txt_IMPACC5_Interest_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC5_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Interest_Intnl_Exch_Rate", _txt_IMPACC5_Interest_Intnl_Ex_rate.ToUpper());

        SqlParameter P_txt_IMPACC5_Commission_matu = new SqlParameter("@IMP_ACC5_Commission_MATU", _txt_IMPACC5_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC5_Commission_lump = new SqlParameter("@IMP_ACC5_Commission_LUMP", _txt_IMPACC5_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC5_Commission_Contract_no = new SqlParameter("@IMP_ACC5_Commission_Contract_No", _txt_IMPACC5_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC5_Commission_Ex_Curr = new SqlParameter("@IMP_ACC5_Commission_Ex_Curr", _txt_IMPACC5_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_Commission_Ex_rate = new SqlParameter("@IMP_ACC5_Commission_Exch_Rate", _txt_IMPACC5_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC5_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Commission_Intnl_Exch_Rate", _txt_IMPACC5_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC5_Their_Commission_matu = new SqlParameter("@IMP_ACC5_Their_Commission_MATU", _txt_IMPACC5_Their_Commission_matu.ToUpper());
        SqlParameter P_txt_IMPACC5_Their_Commission_lump = new SqlParameter("@IMP_ACC5_Their_Commission_LUMP", _txt_IMPACC5_Their_Commission_lump.ToUpper());
        SqlParameter P_txt_IMPACC5_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC5_Their_Commission_Contract_No", _txt_IMPACC5_Their_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_IMPACC5_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC5_Their_Commission_Ex_Curr", _txt_IMPACC5_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC5_Their_Commission_Exch_Rate", _txt_IMPACC5_Their_Commission_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC5_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Their_Commission_Intnl_Exch_Rate", _txt_IMPACC5_Their_Commission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Code = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Code", _txt_IMPACC5_CR_Code.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_AC_Short_Name = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Short_Name", _txt_IMPACC5_CR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Cust_abbr = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Cust_Abbr", _txt_IMPACC5_CR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Cust_Acc = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Cust_Acc_No", _txt_IMPACC5_CR_Cust_Acc.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Curr", _txt_IMPACC5_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Acceptance_amt = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Amt", _txt_IMPACC5_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Acceptance_payer = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Payer", _txt_IMPACC5_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Interest_Curr = new SqlParameter("@IMP_ACC5_CR_Interest_Curr", _txt_IMPACC5_CR_Interest_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Interest_amt = new SqlParameter("@IMP_ACC5_CR_Interest_Amount", _txt_IMPACC5_CR_Interest_amt.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Interest_payer = new SqlParameter("@IMP_ACC5_CR_Interest_Payer", _txt_IMPACC5_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Curr", _txt_IMPACC5_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Amount", _txt_IMPACC5_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Payer", _txt_IMPACC5_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Curr", _txt_IMPACC5_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Amount", _txt_IMPACC5_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Payer", _txt_IMPACC5_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Others_Curr = new SqlParameter("@IMP_ACC5_CR_Others_Curr", _txt_IMPACC5_CR_Others_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Others_amt = new SqlParameter("@IMP_ACC5_CR_Others_Amount", _txt_IMPACC5_CR_Others_amt.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Others_Payer = new SqlParameter("@IMP_ACC5_CR_Others_Payer", _txt_IMPACC5_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Curr", _txt_IMPACC5_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Amount", _txt_IMPACC5_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_txt_IMPACC5_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Payer", _txt_IMPACC5_CR_Their_Commission_Payer.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Code = new SqlParameter("@IMP_ACC5_DR_Code", _txt_IMPACC5_DR_Code.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_AC_Short_Name = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name", _txt_IMPACC5_DR_AC_Short_Name.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_abbr = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr", _txt_IMPACC5_DR_Cust_abbr.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_Acc = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No", _txt_IMPACC5_DR_Cust_Acc.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr", _txt_IMPACC5_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount", _txt_IMPACC5_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer", _txt_IMPACC5_DR_Cur_Acc_payer.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr2", _txt_IMPACC5_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount2", _txt_IMPACC5_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer2", _txt_IMPACC5_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr3", _txt_IMPACC5_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount3", _txt_IMPACC5_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer3", _txt_IMPACC5_DR_Cur_Acc_payer3.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr4", _txt_IMPACC5_DR_Cur_Acc_Curr4.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount4", _txt_IMPACC5_DR_Cur_Acc_amt4.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer4", _txt_IMPACC5_DR_Cur_Acc_payer4.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr5", _txt_IMPACC5_DR_Cur_Acc_Curr5.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount5", _txt_IMPACC5_DR_Cur_Acc_amt5.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer5", _txt_IMPACC5_DR_Cur_Acc_payer5.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr6", _txt_IMPACC5_DR_Cur_Acc_Curr6.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount6", _txt_IMPACC5_DR_Cur_Acc_amt6.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer6", _txt_IMPACC5_DR_Cur_Acc_payer6.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Code2 = new SqlParameter("@IMP_ACC5_DR_Code2", _txt_IMPACC5_DR_Code2.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name2", _txt_IMPACC5_DR_AC_Short_Name2.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr2", _txt_IMPACC5_DR_Cust_abbr2.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No2", _txt_IMPACC5_DR_Cust_Acc2.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Code3 = new SqlParameter("@IMP_ACC5_DR_Code3", _txt_IMPACC5_DR_Code3.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name3", _txt_IMPACC5_DR_AC_Short_Name3.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr3", _txt_IMPACC5_DR_Cust_abbr3.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No3", _txt_IMPACC5_DR_Cust_Acc3.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Code4 = new SqlParameter("@IMP_ACC5_DR_Code4", _txt_IMPACC5_DR_Code4.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name4", _txt_IMPACC5_DR_AC_Short_Name4.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr4", _txt_IMPACC5_DR_Cust_abbr4.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No4", _txt_IMPACC5_DR_Cust_Acc4.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Code5 = new SqlParameter("@IMP_ACC5_DR_Code5", _txt_IMPACC5_DR_Code5.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name5", _txt_IMPACC5_DR_AC_Short_Name5.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr5", _txt_IMPACC5_DR_Cust_abbr5.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No5", _txt_IMPACC5_DR_Cust_Acc5.ToUpper());

        SqlParameter P_txt_IMPACC5_DR_Code6 = new SqlParameter("@IMP_ACC5_DR_Code6", _txt_IMPACC5_DR_Code6.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name6", _txt_IMPACC5_DR_AC_Short_Name6.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr6", _txt_IMPACC5_DR_Cust_abbr6.ToUpper());
        SqlParameter P_txt_IMPACC5_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No6", _txt_IMPACC5_DR_Cust_Acc6.ToUpper());


        /////GENERAL OPRATION 1
        SqlParameter P_chk_GO1Flag = new SqlParameter("@GO1_Flag", _chk_GO1Flag);
        SqlParameter P_txt_GO1_Left_Comment = new SqlParameter("@GO1_Comment1", _txt_GO1_Left_Comment.ToUpper());
        SqlParameter P_txt_GO1_Left_SectionNo = new SqlParameter("@GO1_Section1", _txt_GO1_Left_SectionNo.ToUpper());
        SqlParameter P_txt_GO1_Left_Remarks = new SqlParameter("@GO1_Remark1", _txt_GO1_Left_Remarks.ToUpper());
        SqlParameter P_txt_GO1_Left_Memo = new SqlParameter("@GO1_Memo1", _txt_GO1_Left_Memo.ToUpper());
        SqlParameter P_txt_GO1_Left_Scheme_no = new SqlParameter("@GO1_SchemeNo1", _txt_GO1_Left_Scheme_no.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Code = new SqlParameter("@GO1_DebitCredit1_Code", _txt_GO1_Left_Debit_Code.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Curr = new SqlParameter("@GO1_DebitCredit1_CCY", _txt_GO1_Left_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Amt = new SqlParameter("@GO1_DebitCredit1_Amt", _txt_GO1_Left_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Cust = new SqlParameter("@GO1_DebitCredit1_Cust_abbr", _txt_GO1_Left_Debit_Cust.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Cust_Name = new SqlParameter("@GO1_DebitCredit1_Cust_Name", _txt_GO1_Left_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Cust_AcCode = new SqlParameter("@GO1_DebitCredit1_Cust_AccCode", _txt_GO1_Left_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Cust_AcCode_Name = new SqlParameter("@GO1_DebitCredit1_Cust_AccCode_Disc", _txt_GO1_Left_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Cust_AccNo = new SqlParameter("@GO1_DebitCredit1_Cust_AccNo", _txt_GO1_Left_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Exch_Rate = new SqlParameter("@GO1_DebitCredit1_ExchRate", _txt_GO1_Left_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Exch_CCY = new SqlParameter("@GO1_DebitCredit1_ExchCCY", _txt_GO1_Left_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_FUND = new SqlParameter("@GO1_DebitCredit1_Fund", _txt_GO1_Left_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Check_No = new SqlParameter("@GO1_DebitCredit1_CheckNo", _txt_GO1_Left_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Available = new SqlParameter("@GO1_DebitCredit1_Available", _txt_GO1_Left_Debit_Available.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_AdPrint = new SqlParameter("@GO1_DebitCredit1_Advice_Print", _txt_GO1_Left_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Details = new SqlParameter("@GO1_DebitCredit1_Details", _txt_GO1_Left_Debit_Details.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Entity = new SqlParameter("@GO1_DebitCredit1_Entity", _txt_GO1_Left_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Division = new SqlParameter("@GO1_DebitCredit1_Division", _txt_GO1_Left_Debit_Division.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Inter_Amount = new SqlParameter("@GO1_DebitCredit1_InterAmt", _txt_GO1_Left_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO1_Left_Debit_Inter_Rate = new SqlParameter("@GO1_DebitCredit1_InterRate", _txt_GO1_Left_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Code = new SqlParameter("@GO1_DebitCredit2_Code", _txt_GO1_Left_Credit_Code.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Curr = new SqlParameter("@GO1_DebitCredit2_CCY", _txt_GO1_Left_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Amt = new SqlParameter("@GO1_DebitCredit2_Amt", _txt_GO1_Left_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Cust = new SqlParameter("@GO1_DebitCredit2_Cust_abbr", _txt_GO1_Left_Credit_Cust.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Cust_Name = new SqlParameter("@GO1_DebitCredit2_Cust_Name", _txt_GO1_Left_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Cust_AcCode = new SqlParameter("@GO1_DebitCredit2_Cust_AccCode", _txt_GO1_Left_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Cust_AcCode_Name = new SqlParameter("@GO1_DebitCredit2_Cust_AccCode_Disc", _txt_GO1_Left_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Cust_AccNo = new SqlParameter("@GO1_DebitCredit2_Cust_AccNo", _txt_GO1_Left_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Exch_Rate = new SqlParameter("@GO1_DebitCredit2_ExchRate", _txt_GO1_Left_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Exch_CCY = new SqlParameter("@GO1_DebitCredit2_ExchCCY", _txt_GO1_Left_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_FUND = new SqlParameter("@GO1_DebitCredit2_Fund", _txt_GO1_Left_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Check_No = new SqlParameter("@GO1_DebitCredit2_CheckNo", _txt_GO1_Left_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Available = new SqlParameter("@GO1_DebitCredit2_Available", _txt_GO1_Left_Credit_Available.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_AdPrint = new SqlParameter("@GO1_DebitCredit2_Advice_Print", _txt_GO1_Left_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Details = new SqlParameter("@GO1_DebitCredit2_Details", _txt_GO1_Left_Credit_Details.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Entity = new SqlParameter("@GO1_DebitCredit2_Entity", _txt_GO1_Left_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Division = new SqlParameter("@GO1_DebitCredit2_Division", _txt_GO1_Left_Credit_Division.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Inter_Amount = new SqlParameter("@GO1_DebitCredit2_InterAmt", _txt_GO1_Left_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO1_Left_Credit_Inter_Rate = new SqlParameter("@GO1_DebitCredit2_InterRate", _txt_GO1_Left_Credit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO1_Right_Comment = new SqlParameter("@GO1_Comment2", _txt_GO1_Right_Comment.ToUpper());
        SqlParameter P_txt_GO1_Right_SectionNo = new SqlParameter("@GO1_Section2", _txt_GO1_Right_SectionNo.ToUpper());
        SqlParameter P_txt_GO1_Right_Remarks = new SqlParameter("@GO1_Remark2", _txt_GO1_Right_Remarks.ToUpper());
        SqlParameter P_txt_GO1_Right_Memo = new SqlParameter("@GO1_Memo2", _txt_GO1_Right_Memo.ToUpper());
        SqlParameter P_txt_GO1_Right_Scheme_no = new SqlParameter("@GO1_SchemeNo2", _txt_GO1_Right_Scheme_no.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Code = new SqlParameter("@GO1_DebitCredit3_Code", _txt_GO1_Right_Debit_Code.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Curr = new SqlParameter("@GO1_DebitCredit3_CCY", _txt_GO1_Right_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Amt = new SqlParameter("@GO1_DebitCredit3_Amt", _txt_GO1_Right_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Cust = new SqlParameter("@GO1_DebitCredit3_Cust_abbr", _txt_GO1_Right_Debit_Cust.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Cust_Name = new SqlParameter("@GO1_DebitCredit3_Cust_Name", _txt_GO1_Right_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Cust_AcCode = new SqlParameter("@GO1_DebitCredit3_Cust_AccCode", _txt_GO1_Right_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Cust_AcCode_Name = new SqlParameter("@GO1_DebitCredit3_Cust_AccCode_Disc", _txt_GO1_Right_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Cust_AccNo = new SqlParameter("@GO1_DebitCredit3_Cust_AccNo", _txt_GO1_Right_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Exch_Rate = new SqlParameter("@GO1_DebitCredit3_ExchRate", _txt_GO1_Right_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Exch_CCY = new SqlParameter("@GO1_DebitCredit3_ExchCCY", _txt_GO1_Right_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_FUND = new SqlParameter("@GO1_DebitCredit3_Fund", _txt_GO1_Right_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Check_No = new SqlParameter("@GO1_DebitCredit3_CheckNo", _txt_GO1_Right_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Available = new SqlParameter("@GO1_DebitCredit3_Available", _txt_GO1_Right_Debit_Available.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_AdPrint = new SqlParameter("@GO1_DebitCredit3_Advice_Print", _txt_GO1_Right_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Details = new SqlParameter("@GO1_DebitCredit3_Details", _txt_GO1_Right_Debit_Details.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Entity = new SqlParameter("@GO1_DebitCredit3_Entity", _txt_GO1_Right_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Division = new SqlParameter("@GO1_DebitCredit3_Division", _txt_GO1_Right_Debit_Division.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Inter_Amount = new SqlParameter("@GO1_DebitCredit3_InterAmt", _txt_GO1_Right_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO1_Right_Debit_Inter_Rate = new SqlParameter("@GO1_DebitCredit3_InterRate", _txt_GO1_Right_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Code = new SqlParameter("@GO1_DebitCredit4_Code", _txt_GO1_Right_Credit_Code.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Curr = new SqlParameter("@GO1_DebitCredit4_CCY", _txt_GO1_Right_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Amt = new SqlParameter("@GO1_DebitCredit4_Amt", _txt_GO1_Right_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Cust = new SqlParameter("@GO1_DebitCredit4_Cust_abbr", _txt_GO1_Right_Credit_Cust.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Cust_Name = new SqlParameter("@GO1_DebitCredit4_Cust_Name", _txt_GO1_Right_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Cust_AcCode = new SqlParameter("@GO1_DebitCredit4_Cust_AccCode", _txt_GO1_Right_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Cust_AcCode_Name = new SqlParameter("@GO1_DebitCredit4_Cust_AccCode_Disc", _txt_GO1_Right_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Cust_AccNo = new SqlParameter("@GO1_DebitCredit4_Cust_AccNo", _txt_GO1_Right_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Exch_Rate = new SqlParameter("@GO1_DebitCredit4_ExchRate", _txt_GO1_Right_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Exch_CCY = new SqlParameter("@GO1_DebitCredit4_ExchCCY", _txt_GO1_Right_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_FUND = new SqlParameter("@GO1_DebitCredit4_Fund", _txt_GO1_Right_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Check_No = new SqlParameter("@GO1_DebitCredit4_CheckNo", _txt_GO1_Right_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Available = new SqlParameter("@GO1_DebitCredit4_Available", _txt_GO1_Right_Credit_Available.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_AdPrint = new SqlParameter("@GO1_DebitCredit4_Advice_Print", _txt_GO1_Right_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Details = new SqlParameter("@GO1_DebitCredit4_Details", _txt_GO1_Right_Credit_Details.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Entity = new SqlParameter("@GO1_DebitCredit4_Entity", _txt_GO1_Right_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Division = new SqlParameter("@GO1_DebitCredit4_Division", _txt_GO1_Right_Credit_Division.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Inter_Amount = new SqlParameter("@GO1_DebitCredit4_InterAmt", _txt_GO1_Right_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO1_Right_Credit_Inter_Rate = new SqlParameter("@GO1_DebitCredit4_InterRate", _txt_GO1_Right_Credit_Inter_Rate.ToUpper());

        /////GENERAL OPRATION 2
        SqlParameter P_chk_GO2Flag = new SqlParameter("@GO2_Flag", _chk_GO2Flag);
        SqlParameter P_txt_GO2_Left_Comment = new SqlParameter("@GO2_Comment1", _txt_GO2_Left_Comment.ToUpper());
        SqlParameter P_txt_GO2_Left_SectionNo = new SqlParameter("@GO2_Section1", _txt_GO2_Left_SectionNo.ToUpper());
        SqlParameter P_txt_GO2_Left_Remarks = new SqlParameter("@GO2_Remark1", _txt_GO2_Left_Remarks.ToUpper());
        SqlParameter P_txt_GO2_Left_Memo = new SqlParameter("@GO2_Memo1", _txt_GO2_Left_Memo.ToUpper());
        SqlParameter P_txt_GO2_Left_Scheme_no = new SqlParameter("@GO2_SchemeNo1", _txt_GO2_Left_Scheme_no.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Code = new SqlParameter("@GO2_DebitCredit1_Code", _txt_GO2_Left_Debit_Code.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Curr = new SqlParameter("@GO2_DebitCredit1_CCY", _txt_GO2_Left_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Amt = new SqlParameter("@GO2_DebitCredit1_Amt", _txt_GO2_Left_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Cust = new SqlParameter("@GO2_DebitCredit1_Cust_abbr", _txt_GO2_Left_Debit_Cust.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Cust_Name = new SqlParameter("@GO2_DebitCredit1_Cust_Name", _txt_GO2_Left_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Cust_AcCode = new SqlParameter("@GO2_DebitCredit1_Cust_AccCode", _txt_GO2_Left_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Cust_AcCode_Name = new SqlParameter("@GO2_DebitCredit1_Cust_AccCode_Disc", _txt_GO2_Left_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Cust_AccNo = new SqlParameter("@GO2_DebitCredit1_Cust_AccNo", _txt_GO2_Left_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Exch_Rate = new SqlParameter("@GO2_DebitCredit1_ExchRate", _txt_GO2_Left_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Exch_CCY = new SqlParameter("@GO2_DebitCredit1_ExchCCY", _txt_GO2_Left_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_FUND = new SqlParameter("@GO2_DebitCredit1_Fund", _txt_GO2_Left_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Check_No = new SqlParameter("@GO2_DebitCredit1_CheckNo", _txt_GO2_Left_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Available = new SqlParameter("@GO2_DebitCredit1_Available", _txt_GO2_Left_Debit_Available.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_AdPrint = new SqlParameter("@GO2_DebitCredit1_Advice_Print", _txt_GO2_Left_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Details = new SqlParameter("@GO2_DebitCredit1_Details", _txt_GO2_Left_Debit_Details.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Entity = new SqlParameter("@GO2_DebitCredit1_Entity", _txt_GO2_Left_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Division = new SqlParameter("@GO2_DebitCredit1_Division", _txt_GO2_Left_Debit_Division.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Inter_Amount = new SqlParameter("@GO2_DebitCredit1_InterAmt", _txt_GO2_Left_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO2_Left_Debit_Inter_Rate = new SqlParameter("@GO2_DebitCredit1_InterRate", _txt_GO2_Left_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Code = new SqlParameter("@GO2_DebitCredit2_Code", _txt_GO2_Left_Credit_Code.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Curr = new SqlParameter("@GO2_DebitCredit2_CCY", _txt_GO2_Left_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Amt = new SqlParameter("@GO2_DebitCredit2_Amt", _txt_GO2_Left_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Cust = new SqlParameter("@GO2_DebitCredit2_Cust_abbr", _txt_GO2_Left_Credit_Cust.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Cust_Name = new SqlParameter("@GO2_DebitCredit2_Cust_Name", _txt_GO2_Left_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Cust_AcCode = new SqlParameter("@GO2_DebitCredit2_Cust_AccCode", _txt_GO2_Left_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Cust_AcCode_Name = new SqlParameter("@GO2_DebitCredit2_Cust_AccCode_Disc", _txt_GO2_Left_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Cust_AccNo = new SqlParameter("@GO2_DebitCredit2_Cust_AccNo", _txt_GO2_Left_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Exch_Rate = new SqlParameter("@GO2_DebitCredit2_ExchRate", _txt_GO2_Left_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Exch_CCY = new SqlParameter("@GO2_DebitCredit2_ExchCCY", _txt_GO2_Left_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_FUND = new SqlParameter("@GO2_DebitCredit2_Fund", _txt_GO2_Left_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Check_No = new SqlParameter("@GO2_DebitCredit2_CheckNo", _txt_GO2_Left_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Available = new SqlParameter("@GO2_DebitCredit2_Available", _txt_GO2_Left_Credit_Available.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_AdPrint = new SqlParameter("@GO2_DebitCredit2_Advice_Print", _txt_GO2_Left_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Details = new SqlParameter("@GO2_DebitCredit2_Details", _txt_GO2_Left_Credit_Details.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Entity = new SqlParameter("@GO2_DebitCredit2_Entity", _txt_GO2_Left_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Division = new SqlParameter("@GO2_DebitCredit2_Division", _txt_GO2_Left_Credit_Division.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Inter_Amount = new SqlParameter("@GO2_DebitCredit2_InterAmt", _txt_GO2_Left_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO2_Left_Credit_Inter_Rate = new SqlParameter("@GO2_DebitCredit2_InterRate", _txt_GO2_Left_Credit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO2_Right_Comment = new SqlParameter("@GO2_Comment2", _txt_GO2_Right_Comment.ToUpper());
        SqlParameter P_txt_GO2_Right_SectionNo = new SqlParameter("@GO2_Section2", _txt_GO2_Right_SectionNo.ToUpper());
        SqlParameter P_txt_GO2_Right_Remarks = new SqlParameter("@GO2_Remark2", _txt_GO2_Right_Remarks.ToUpper());
        SqlParameter P_txt_GO2_Right_Memo = new SqlParameter("@GO2_Memo2", _txt_GO2_Right_Memo.ToUpper());
        SqlParameter P_txt_GO2_Right_Scheme_no = new SqlParameter("@GO2_SchemeNo2", _txt_GO2_Right_Scheme_no.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Code = new SqlParameter("@GO2_DebitCredit3_Code", _txt_GO2_Right_Debit_Code.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Curr = new SqlParameter("@GO2_DebitCredit3_CCY", _txt_GO2_Right_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Amt = new SqlParameter("@GO2_DebitCredit3_Amt", _txt_GO2_Right_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Cust = new SqlParameter("@GO2_DebitCredit3_Cust_abbr", _txt_GO2_Right_Debit_Cust.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Cust_Name = new SqlParameter("@GO2_DebitCredit3_Cust_Name", _txt_GO2_Right_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Cust_AcCode = new SqlParameter("@GO2_DebitCredit3_Cust_AccCode", _txt_GO2_Right_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Cust_AcCode_Name = new SqlParameter("@GO2_DebitCredit3_Cust_AccCode_Disc", _txt_GO2_Right_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Cust_AccNo = new SqlParameter("@GO2_DebitCredit3_Cust_AccNo", _txt_GO2_Right_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Exch_Rate = new SqlParameter("@GO2_DebitCredit3_ExchRate", _txt_GO2_Right_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Exch_CCY = new SqlParameter("@GO2_DebitCredit3_ExchCCY", _txt_GO2_Right_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_FUND = new SqlParameter("@GO2_DebitCredit3_Fund", _txt_GO2_Right_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Check_No = new SqlParameter("@GO2_DebitCredit3_CheckNo", _txt_GO2_Right_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Available = new SqlParameter("@GO2_DebitCredit3_Available", _txt_GO2_Right_Debit_Available.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_AdPrint = new SqlParameter("@GO2_DebitCredit3_Advice_Print", _txt_GO2_Right_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Details = new SqlParameter("@GO2_DebitCredit3_Details", _txt_GO2_Right_Debit_Details.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Entity = new SqlParameter("@GO2_DebitCredit3_Entity", _txt_GO2_Right_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Division = new SqlParameter("@GO2_DebitCredit3_Division", _txt_GO2_Right_Debit_Division.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Inter_Amount = new SqlParameter("@GO2_DebitCredit3_InterAmt", _txt_GO2_Right_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO2_Right_Debit_Inter_Rate = new SqlParameter("@GO2_DebitCredit3_InterRate", _txt_GO2_Right_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Code = new SqlParameter("@GO2_DebitCredit4_Code", _txt_GO2_Right_Credit_Code.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Curr = new SqlParameter("@GO2_DebitCredit4_CCY", _txt_GO2_Right_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Amt = new SqlParameter("@GO2_DebitCredit4_Amt", _txt_GO2_Right_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Cust = new SqlParameter("@GO2_DebitCredit4_Cust_abbr", _txt_GO2_Right_Credit_Cust.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Cust_Name = new SqlParameter("@GO2_DebitCredit4_Cust_Name", _txt_GO2_Right_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Cust_AcCode = new SqlParameter("@GO2_DebitCredit4_Cust_AccCode", _txt_GO2_Right_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Cust_AcCode_Name = new SqlParameter("@GO2_DebitCredit4_Cust_AccCode_Disc", _txt_GO2_Right_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Cust_AccNo = new SqlParameter("@GO2_DebitCredit4_Cust_AccNo", _txt_GO2_Right_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Exch_Rate = new SqlParameter("@GO2_DebitCredit4_ExchRate", _txt_GO2_Right_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Exch_CCY = new SqlParameter("@GO2_DebitCredit4_ExchCCY", _txt_GO2_Right_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_FUND = new SqlParameter("@GO2_DebitCredit4_Fund", _txt_GO2_Right_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Check_No = new SqlParameter("@GO2_DebitCredit4_CheckNo", _txt_GO2_Right_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Available = new SqlParameter("@GO2_DebitCredit4_Available", _txt_GO2_Right_Credit_Available.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_AdPrint = new SqlParameter("@GO2_DebitCredit4_Advice_Print", _txt_GO2_Right_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Details = new SqlParameter("@GO2_DebitCredit4_Details", _txt_GO2_Right_Credit_Details.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Entity = new SqlParameter("@GO2_DebitCredit4_Entity", _txt_GO2_Right_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Division = new SqlParameter("@GO2_DebitCredit4_Division", _txt_GO2_Right_Credit_Division.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Inter_Amount = new SqlParameter("@GO2_DebitCredit4_InterAmt", _txt_GO2_Right_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO2_Right_Credit_Inter_Rate = new SqlParameter("@GO2_DebitCredit4_InterRate", _txt_GO2_Right_Credit_Inter_Rate.ToUpper());
        /////GENERAL OPRATION 3
        SqlParameter P_chk_GO3Flag = new SqlParameter("@GO3_Flag", _chk_GO3Flag);
        SqlParameter P_txt_GO3_Left_Comment = new SqlParameter("@GO3_Comment1", _txt_GO3_Left_Comment.ToUpper());
        SqlParameter P_txt_GO3_Left_SectionNo = new SqlParameter("@GO3_Section1", _txt_GO3_Left_SectionNo.ToUpper());
        SqlParameter P_txt_GO3_Left_Remarks = new SqlParameter("@GO3_Remark1", _txt_GO3_Left_Remarks.ToUpper());
        SqlParameter P_txt_GO3_Left_Memo = new SqlParameter("@GO3_Memo1", _txt_GO3_Left_Memo.ToUpper());
        SqlParameter P_txt_GO3_Left_Scheme_no = new SqlParameter("@GO3_SchemeNo1", _txt_GO3_Left_Scheme_no.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Code = new SqlParameter("@GO3_DebitCredit1_Code", _txt_GO3_Left_Debit_Code.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Curr = new SqlParameter("@GO3_DebitCredit1_CCY", _txt_GO3_Left_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Amt = new SqlParameter("@GO3_DebitCredit1_Amt", _txt_GO3_Left_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Cust = new SqlParameter("@GO3_DebitCredit1_Cust_abbr", _txt_GO3_Left_Debit_Cust.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Cust_Name = new SqlParameter("@GO3_DebitCredit1_Cust_Name", _txt_GO3_Left_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Cust_AcCode = new SqlParameter("@GO3_DebitCredit1_Cust_AccCode", _txt_GO3_Left_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Cust_AcCode_Name = new SqlParameter("@GO3_DebitCredit1_Cust_AccCode_Disc", _txt_GO3_Left_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Cust_AccNo = new SqlParameter("@GO3_DebitCredit1_Cust_AccNo", _txt_GO3_Left_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Exch_Rate = new SqlParameter("@GO3_DebitCredit1_ExchRate", _txt_GO3_Left_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Exch_CCY = new SqlParameter("@GO3_DebitCredit1_ExchCCY", _txt_GO3_Left_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_FUND = new SqlParameter("@GO3_DebitCredit1_Fund", _txt_GO3_Left_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Check_No = new SqlParameter("@GO3_DebitCredit1_CheckNo", _txt_GO3_Left_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Available = new SqlParameter("@GO3_DebitCredit1_Available", _txt_GO3_Left_Debit_Available.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_AdPrint = new SqlParameter("@GO3_DebitCredit1_Advice_Print", _txt_GO3_Left_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Details = new SqlParameter("@GO3_DebitCredit1_Details", _txt_GO3_Left_Debit_Details.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Entity = new SqlParameter("@GO3_DebitCredit1_Entity", _txt_GO3_Left_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Division = new SqlParameter("@GO3_DebitCredit1_Division", _txt_GO3_Left_Debit_Division.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Inter_Amount = new SqlParameter("@GO3_DebitCredit1_InterAmt", _txt_GO3_Left_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO3_Left_Debit_Inter_Rate = new SqlParameter("@GO3_DebitCredit1_InterRate", _txt_GO3_Left_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Code = new SqlParameter("@GO3_DebitCredit2_Code", _txt_GO3_Left_Credit_Code.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Curr = new SqlParameter("@GO3_DebitCredit2_CCY", _txt_GO3_Left_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Amt = new SqlParameter("@GO3_DebitCredit2_Amt", _txt_GO3_Left_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Cust = new SqlParameter("@GO3_DebitCredit2_Cust_abbr", _txt_GO3_Left_Credit_Cust.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Cust_Name = new SqlParameter("@GO3_DebitCredit2_Cust_Name", _txt_GO3_Left_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Cust_AcCode = new SqlParameter("@GO3_DebitCredit2_Cust_AccCode", _txt_GO3_Left_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Cust_AcCode_Name = new SqlParameter("@GO3_DebitCredit2_Cust_AccCode_Disc", _txt_GO3_Left_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Cust_AccNo = new SqlParameter("@GO3_DebitCredit2_Cust_AccNo", _txt_GO3_Left_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Exch_Rate = new SqlParameter("@GO3_DebitCredit2_ExchRate", _txt_GO3_Left_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Exch_CCY = new SqlParameter("@GO3_DebitCredit2_ExchCCY", _txt_GO3_Left_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_FUND = new SqlParameter("@GO3_DebitCredit2_Fund", _txt_GO3_Left_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Check_No = new SqlParameter("@GO3_DebitCredit2_CheckNo", _txt_GO3_Left_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Available = new SqlParameter("@GO3_DebitCredit2_Available", _txt_GO3_Left_Credit_Available.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_AdPrint = new SqlParameter("@GO3_DebitCredit2_Advice_Print", _txt_GO3_Left_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Details = new SqlParameter("@GO3_DebitCredit2_Details", _txt_GO3_Left_Credit_Details.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Entity = new SqlParameter("@GO3_DebitCredit2_Entity", _txt_GO3_Left_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Division = new SqlParameter("@GO3_DebitCredit2_Division", _txt_GO3_Left_Credit_Division.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Inter_Amount = new SqlParameter("@GO3_DebitCredit2_InterAmt", _txt_GO3_Left_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO3_Left_Credit_Inter_Rate = new SqlParameter("@GO3_DebitCredit2_InterRate", _txt_GO3_Left_Credit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO3_Right_Comment = new SqlParameter("@GO3_Comment2", _txt_GO3_Right_Comment.ToUpper());
        SqlParameter P_txt_GO3_Right_SectionNo = new SqlParameter("@GO3_Section2", _txt_GO3_Right_SectionNo.ToUpper());
        SqlParameter P_txt_GO3_Right_Remarks = new SqlParameter("@GO3_Remark2", _txt_GO3_Right_Remarks.ToUpper());
        SqlParameter P_txt_GO3_Right_Memo = new SqlParameter("@GO3_Memo2", _txt_GO3_Right_Memo.ToUpper());
        SqlParameter P_txt_GO3_Right_Scheme_no = new SqlParameter("@GO3_SchemeNo2", _txt_GO3_Right_Scheme_no.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Code = new SqlParameter("@GO3_DebitCredit3_Code", _txt_GO3_Right_Debit_Code.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Curr = new SqlParameter("@GO3_DebitCredit3_CCY", _txt_GO3_Right_Debit_Curr.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Amt = new SqlParameter("@GO3_DebitCredit3_Amt", _txt_GO3_Right_Debit_Amt.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Cust = new SqlParameter("@GO3_DebitCredit3_Cust_abbr", _txt_GO3_Right_Debit_Cust.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Cust_Name = new SqlParameter("@GO3_DebitCredit3_Cust_Name", _txt_GO3_Right_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Cust_AcCode = new SqlParameter("@GO3_DebitCredit3_Cust_AccCode", _txt_GO3_Right_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Cust_AcCode_Name = new SqlParameter("@GO3_DebitCredit3_Cust_AccCode_Disc", _txt_GO3_Right_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Cust_AccNo = new SqlParameter("@GO3_DebitCredit3_Cust_AccNo", _txt_GO3_Right_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Exch_Rate = new SqlParameter("@GO3_DebitCredit3_ExchRate", _txt_GO3_Right_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Exch_CCY = new SqlParameter("@GO3_DebitCredit3_ExchCCY", _txt_GO3_Right_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_FUND = new SqlParameter("@GO3_DebitCredit3_Fund", _txt_GO3_Right_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Check_No = new SqlParameter("@GO3_DebitCredit3_CheckNo", _txt_GO3_Right_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Available = new SqlParameter("@GO3_DebitCredit3_Available", _txt_GO3_Right_Debit_Available.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_AdPrint = new SqlParameter("@GO3_DebitCredit3_Advice_Print", _txt_GO3_Right_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Details = new SqlParameter("@GO3_DebitCredit3_Details", _txt_GO3_Right_Debit_Details.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Entity = new SqlParameter("@GO3_DebitCredit3_Entity", _txt_GO3_Right_Debit_Entity.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Division = new SqlParameter("@GO3_DebitCredit3_Division", _txt_GO3_Right_Debit_Division.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Inter_Amount = new SqlParameter("@GO3_DebitCredit3_InterAmt", _txt_GO3_Right_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO3_Right_Debit_Inter_Rate = new SqlParameter("@GO3_DebitCredit3_InterRate", _txt_GO3_Right_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Code = new SqlParameter("@GO3_DebitCredit4_Code", _txt_GO3_Right_Credit_Code.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Curr = new SqlParameter("@GO3_DebitCredit4_CCY", _txt_GO3_Right_Credit_Curr.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Amt = new SqlParameter("@GO3_DebitCredit4_Amt", _txt_GO3_Right_Credit_Amt.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Cust = new SqlParameter("@GO3_DebitCredit4_Cust_abbr", _txt_GO3_Right_Credit_Cust.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Cust_Name = new SqlParameter("@GO3_DebitCredit4_Cust_Name", _txt_GO3_Right_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Cust_AcCode = new SqlParameter("@GO3_DebitCredit4_Cust_AccCode", _txt_GO3_Right_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Cust_AcCode_Name = new SqlParameter("@GO3_DebitCredit4_Cust_AccCode_Disc", _txt_GO3_Right_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Cust_AccNo = new SqlParameter("@GO3_DebitCredit4_Cust_AccNo", _txt_GO3_Right_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Exch_Rate = new SqlParameter("@GO3_DebitCredit4_ExchRate", _txt_GO3_Right_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Exch_CCY = new SqlParameter("@GO3_DebitCredit4_ExchCCY", _txt_GO3_Right_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_FUND = new SqlParameter("@GO3_DebitCredit4_Fund", _txt_GO3_Right_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Check_No = new SqlParameter("@GO3_DebitCredit4_CheckNo", _txt_GO3_Right_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Available = new SqlParameter("@GO3_DebitCredit4_Available", _txt_GO3_Right_Credit_Available.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_AdPrint = new SqlParameter("@GO3_DebitCredit4_Advice_Print", _txt_GO3_Right_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Details = new SqlParameter("@GO3_DebitCredit4_Details", _txt_GO3_Right_Credit_Details.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Entity = new SqlParameter("@GO3_DebitCredit4_Entity", _txt_GO3_Right_Credit_Entity.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Division = new SqlParameter("@GO3_DebitCredit4_Division", _txt_GO3_Right_Credit_Division.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Inter_Amount = new SqlParameter("@GO3_DebitCredit4_InterAmt", _txt_GO3_Right_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO3_Right_Credit_Inter_Rate = new SqlParameter("@GO3_DebitCredit4_InterRate", _txt_GO3_Right_Credit_Inter_Rate.ToUpper());

        ///////GENERAL OPRATION AccChange
        SqlParameter P_chk_GOAcccChangeFlag = new SqlParameter("@GO_Acc_Change_Flag", _chk_GOAcccChangeFlag);
        SqlParameter P_txt_GOAccChange_Ref_No = new SqlParameter("@GO_Acc_Change_TransRef_No", _txt_GOAccChange_Ref_No.ToUpper());
        SqlParameter P_txt_GOAccChange_Comment = new SqlParameter("@GO_Acc_Change_Comment", _txt_GOAccChange_Comment.ToUpper());
        SqlParameter P_txt_GOAccChange_SectionNo = new SqlParameter("@GO_Acc_Change_Section", _txt_GOAccChange_SectionNo.ToUpper());
        SqlParameter P_txt_GOAccChange_Remarks = new SqlParameter("@GO_Acc_Change_Remark", _txt_GOAccChange_Remarks.ToUpper());
        SqlParameter P_txt_GOAccChange_Memo = new SqlParameter("@GO_Acc_Change_Memo", _txt_GOAccChange_Memo.ToUpper());
        SqlParameter P_txt_GOAccChange_Scheme_no = new SqlParameter("@GO_Acc_Change_SchemeNo", _txt_GOAccChange_Scheme_no.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Code = new SqlParameter("@GO_Acc_Change_Debit_Code", _txt_GOAccChange_Debit_Code.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Curr = new SqlParameter("@GO_Acc_Change_Debit_CCY", _txt_GOAccChange_Debit_Curr.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Amt = new SqlParameter("@GO_Acc_Change_Debit_Amt", _txt_GOAccChange_Debit_Amt.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Cust = new SqlParameter("@GO_Acc_Change_Debit_Cust_abbr", _txt_GOAccChange_Debit_Cust.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Cust_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_Name", _txt_GOAccChange_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode", _txt_GOAccChange_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode_Disc", _txt_GOAccChange_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccNo", _txt_GOAccChange_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Debit_ExchRate", _txt_GOAccChange_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Exch_CCY = new SqlParameter("@GO_Acc_Change_Debit_ExchCCY", _txt_GOAccChange_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_FUND = new SqlParameter("@GO_Acc_Change_Debit_Fund", _txt_GOAccChange_Debit_FUND.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Check_No = new SqlParameter("@GO_Acc_Change_Debit_CheckNo", _txt_GOAccChange_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Available = new SqlParameter("@GO_Acc_Change_Debit_Available", _txt_GOAccChange_Debit_Available.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_AdPrint = new SqlParameter("@GO_Acc_Change_Debit_Advice_Print", _txt_GOAccChange_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Details = new SqlParameter("@GO_Acc_Change_Debit_Details", _txt_GOAccChange_Debit_Details.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Entity = new SqlParameter("@GO_Acc_Change_Debit_Entity", _txt_GOAccChange_Debit_Entity.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Division = new SqlParameter("@GO_Acc_Change_Debit_Division", _txt_GOAccChange_Debit_Division.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Debit_InterAmt", _txt_GOAccChange_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GOAccChange_Debit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Debit_InterRate", _txt_GOAccChange_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Code = new SqlParameter("@GO_Acc_Change_Credit_Code", _txt_GOAccChange_Credit_Code.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Curr = new SqlParameter("@GO_Acc_Change_Credit_CCY", _txt_GOAccChange_Credit_Curr.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Amt = new SqlParameter("@GO_Acc_Change_Credit_Amt", _txt_GOAccChange_Credit_Amt.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Cust = new SqlParameter("@GO_Acc_Change_Credit_Cust_abbr", _txt_GOAccChange_Credit_Cust.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Cust_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_Name", _txt_GOAccChange_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode", _txt_GOAccChange_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode_Disc", _txt_GOAccChange_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccNo", _txt_GOAccChange_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Credit_ExchRate", _txt_GOAccChange_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Exch_CCY = new SqlParameter("@GO_Acc_Change_Credit_ExchCCY", _txt_GOAccChange_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_FUND = new SqlParameter("@GO_Acc_Change_Credit_Fund", _txt_GOAccChange_Credit_FUND.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Check_No = new SqlParameter("@GO_Acc_Change_Credit_CheckNo", _txt_GOAccChange_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Available = new SqlParameter("@GO_Acc_Change_Credit_Available", _txt_GOAccChange_Credit_Available.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_AdPrint = new SqlParameter("@GO_Acc_Change_Credit_Advice_Print", _txt_GOAccChange_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Details = new SqlParameter("@GO_Acc_Change_Credit_Details", _txt_GOAccChange_Credit_Details.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Entity = new SqlParameter("@GO_Acc_Change_Credit_Entity", _txt_GOAccChange_Credit_Entity.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Division = new SqlParameter("@GO_Acc_Change_Credit_Division", _txt_GOAccChange_Credit_Division.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Credit_InterAmt", _txt_GOAccChange_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GOAccChange_Credit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Credit_InterRate", _txt_GOAccChange_Credit_Inter_Rate.ToUpper());

        ///////swift file

        SqlParameter P_txt_MT103Receiver = new SqlParameter("@_txt_MT103Receiver", _txt_MT103Receiver.ToUpper());
        SqlParameter P_txtInstructionCode = new SqlParameter("@_txtInstructionCode", _txtInstructionCode.ToUpper());
        SqlParameter P_txtTransactionTypeCode = new SqlParameter("@_txtTransactionTypeCode", _txtTransactionTypeCode.ToUpper());
        SqlParameter P_txtVDate32 = new SqlParameter("@_txtVDate32", _txtVDate32.ToUpper());
        //SqlParameter P_txtCurrency32 = new SqlParameter("@_txtCurrency32", _txtCurrency32.ToUpper());
        //SqlParameter P_txtAmount32 = new SqlParameter("@_txtAmount32", _txtAmount32);
        SqlParameter P_txtCurrency = new SqlParameter("@_txtCurrency", _txtCurrency.ToUpper());
        SqlParameter P_txtInstructedAmount = new SqlParameter("@_txtInstructedAmount", _txtInstructedAmount.ToUpper());
        SqlParameter P_txtExchangeRate = new SqlParameter("@_txtExchangeRate", _txtExchangeRate.ToUpper());
        SqlParameter P_txtSendingInstitutionAccountNumber = new SqlParameter("@_txtSendingInstitutionAccountNumber", _txtSendingInstitutionAccountNumber.ToUpper());
        SqlParameter P_txtSendingInstitutionSwiftCode = new SqlParameter("@_txtSendingInstitutionSwiftCode", _txtSendingInstitutionSwiftCode.ToUpper());
        SqlParameter P_ddlOrderingInstitution = new SqlParameter("@_ddlOrderingInstitution", _ddlOrderingInstitution.ToUpper());
        SqlParameter P_txtOrderingInstitutionAccountNumber = new SqlParameter("@_txtOrderingInstitutionAccountNumber", _txtOrderingInstitutionAccountNumber.ToUpper());
        SqlParameter P_txtOrderingInstitutionSwiftCode = new SqlParameter("@_txtOrderingInstitutionSwiftCode", _txtOrderingInstitutionSwiftCode.ToUpper());
        SqlParameter P_txtOrderingInstitutionName = new SqlParameter("@_txtOrderingInstitutionName", _txtOrderingInstitutionName.ToUpper());
        SqlParameter P_txtOrderingInstitutionAddress1 = new SqlParameter("@_txtOrderingInstitutionAddress1", _txtOrderingInstitutionAddress1.ToUpper());
        SqlParameter P_txtOrderingInstitutionAddress2 = new SqlParameter("@_txtOrderingInstitutionAddress2", _txtOrderingInstitutionAddress2.ToUpper());
        SqlParameter P_txtOrderingInstitutionAddress3 = new SqlParameter("@_txtOrderingInstitutionAddress3", _txtOrderingInstitutionAddress3.ToUpper());
        SqlParameter P_ddlSendersCorrespondent = new SqlParameter("@_ddlSendersCorrespondent", _ddlSendersCorrespondent.ToUpper());
        SqlParameter P_txtSendersCorrespondentAccountNumber = new SqlParameter("@_txtSendersCorrespondentAccountNumber", _txtSendersCorrespondentAccountNumber.ToUpper());
        SqlParameter P_txtSendersCorrespondentSwiftCode = new SqlParameter("@_txtSendersCorrespondentSwiftCode", _txtSendersCorrespondentSwiftCode.ToUpper());
        SqlParameter P_txtSendersCorrespondentName = new SqlParameter("@_txtSendersCorrespondentName", _txtSendersCorrespondentName.ToUpper());
        SqlParameter P_txtSendersCorrespondentLocation = new SqlParameter("@_txtSendersCorrespondentLocation", _txtSendersCorrespondentLocation.ToUpper());
        SqlParameter P_txtSendersCorrespondentAddress1 = new SqlParameter("@_txtSendersCorrespondentAddress1", _txtSendersCorrespondentAddress1.ToUpper());
        SqlParameter P_txtSendersCorrespondentAddress2 = new SqlParameter("@_txtSendersCorrespondentAddress2", _txtSendersCorrespondentAddress2.ToUpper());
        SqlParameter P_txtSendersCorrespondentAddress3 = new SqlParameter("@_txtSendersCorrespondentAddress3", _txtSendersCorrespondentAddress3.ToUpper());
        SqlParameter P_ddlReceiversCorrespondent = new SqlParameter("@_ddlReceiversCorrespondent", _ddlReceiversCorrespondent.ToUpper());
        SqlParameter P_txtReceiversCorrespondentAccountNumber = new SqlParameter("@_txtReceiversCorrespondentAccountNumber", _txtReceiversCorrespondentAccountNumber.ToUpper());
        SqlParameter P_txtReceiversCorrespondentSwiftCode = new SqlParameter("@_txtReceiversCorrespondentSwiftCode", _txtReceiversCorrespondentSwiftCode.ToUpper());
        SqlParameter P_txtReceiversCorrespondentName = new SqlParameter("@_txtReceiversCorrespondentName", _txtReceiversCorrespondentName.ToUpper());
        SqlParameter P_txtReceiversCorrespondentLocation = new SqlParameter("@_txtReceiversCorrespondentLocation", _txtReceiversCorrespondentLocation.ToUpper());
        SqlParameter P_txtReceiversCorrespondentAddress1 = new SqlParameter("@_txtReceiversCorrespondentAddress1", _txtReceiversCorrespondentAddress1.ToUpper());
        SqlParameter P_txtReceiversCorrespondentAddress2 = new SqlParameter("@_txtReceiversCorrespondentAddress2", _txtReceiversCorrespondentAddress2.ToUpper());
        SqlParameter P_txtReceiversCorrespondentAddress3 = new SqlParameter("@_txtReceiversCorrespondentAddress3", _txtReceiversCorrespondentAddress3.ToUpper());
        SqlParameter P_ddlThirdReimbursementInstitution = new SqlParameter("@_ddlThirdReimbursementInstitution", _ddlThirdReimbursementInstitution.ToUpper());
        SqlParameter P_txtThirdReimbursementInstitutionAccountNumber = new SqlParameter("@_txtThirdReimbursementInstitutionAccountNumber", _txtThirdReimbursementInstitutionAccountNumber.ToUpper());
        SqlParameter P_txtThirdReimbursementInstitutionSwiftCode = new SqlParameter("@_txtThirdReimbursementInstitutionSwiftCode", _txtThirdReimbursementInstitutionSwiftCode.ToUpper());
        SqlParameter P_txtThirdReimbursementInstitutionName = new SqlParameter("@_txtThirdReimbursementInstitutionName", _txtThirdReimbursementInstitutionName.ToUpper());
        SqlParameter P_txtThirdReimbursementInstitutionLocation = new SqlParameter("@_txtThirdReimbursementInstitutionLocation", _txtThirdReimbursementInstitutionLocation.ToUpper());
        SqlParameter P_txtThirdReimbursementInstitutionAddress1 = new SqlParameter("@_txtThirdReimbursementInstitutionAddress1", _txtThirdReimbursementInstitutionAddress1.ToUpper());
        SqlParameter P_txtThirdReimbursementInstitutionAddress2 = new SqlParameter("@_txtThirdReimbursementInstitutionAddress2", _txtThirdReimbursementInstitutionAddress2.ToUpper());
        SqlParameter P_txtThirdReimbursementInstitutionAddress3 = new SqlParameter("@_txtThirdReimbursementInstitutionAddress3", _txtThirdReimbursementInstitutionAddress3.ToUpper());
        SqlParameter P_txtDetailsOfCharges = new SqlParameter("@_txtDetailsOfCharges", _txtDetailsOfCharges.ToUpper());
        SqlParameter P_txtSenderCharges = new SqlParameter("@_txtSenderCharges", _txtSenderCharges.ToUpper());
        SqlParameter P_txtSenderCharges2 = new SqlParameter("@_txtSenderCharges2", _txtSenderCharges2.ToUpper());
        SqlParameter P_txtReceiverCharges = new SqlParameter("@_txtReceiverCharges", _txtReceiverCharges.ToUpper());
        SqlParameter P_txtReceiverCharges2 = new SqlParameter("@_txtReceiverCharges2", _txtReceiverCharges2.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation = new SqlParameter("@_txtSendertoReceiverInformation", _txtSendertoReceiverInformation.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation2 = new SqlParameter("@_txtSendertoReceiverInformation2", _txtSendertoReceiverInformation2.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation3 = new SqlParameter("@_txtSendertoReceiverInformation3", _txtSendertoReceiverInformation3.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation4 = new SqlParameter("@_txtSendertoReceiverInformation4", _txtSendertoReceiverInformation4.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation5 = new SqlParameter("@_txtSendertoReceiverInformation5", _txtSendertoReceiverInformation5.ToUpper());
        SqlParameter P_txtSendertoReceiverInformation6 = new SqlParameter("@_txtSendertoReceiverInformation6", _txtSendertoReceiverInformation6.ToUpper());
        SqlParameter P_txtRegulatoryReporting = new SqlParameter("@_txtRegulatoryReporting", _txtRegulatoryReporting.ToUpper());
        SqlParameter P_txtRegulatoryReporting2 = new SqlParameter("@_txtRegulatoryReporting2", _txtRegulatoryReporting2.ToUpper());
        SqlParameter P_txtRegulatoryReporting3 = new SqlParameter("@_txtRegulatoryReporting3", _txtRegulatoryReporting3.ToUpper());

        SqlParameter P_txtTimeIndication = new SqlParameter("@_txtTimeIndication", _txtTimeIndication.ToUpper());
        SqlParameter P_ddlBankOperationCode = new SqlParameter("@_ddlBankOperationCode", _ddlBankOperationCode.ToUpper());
        SqlParameter P_ddlOrderingCustomer = new SqlParameter("@_ddlOrderingCustomer", _ddlOrderingCustomer.ToUpper());
        SqlParameter P_txtOrderingCustomer_Acc = new SqlParameter("@_txtOrderingCustomer_Acc", _txtOrderingCustomer_Acc.ToUpper());
        SqlParameter P_txtOrderingCustomer_SwiftCode = new SqlParameter("@_txtOrderingCustomer_SwiftCode", _txtOrderingCustomer_SwiftCode.ToUpper());
        SqlParameter P_txtOrderingCustomer_Name = new SqlParameter("@_txtOrderingCustomer_Name", _txtOrderingCustomer_Name.ToUpper());
        SqlParameter P_txtOrderingCustomer_Addr1 = new SqlParameter("@_txtOrderingCustomer_Addr1", _txtOrderingCustomer_Addr1.ToUpper());
        SqlParameter P_txtOrderingCustomer_Addr2 = new SqlParameter("@_txtOrderingCustomer_Addr2", _txtOrderingCustomer_Addr2.ToUpper());
        SqlParameter P_txtOrderingCustomer_Addr3 = new SqlParameter("@_txtOrderingCustomer_Addr3", _txtOrderingCustomer_Addr3.ToUpper());
        SqlParameter P_ddlBeneficiaryCustomer = new SqlParameter("@_ddlBeneficiaryCustomer", _ddlBeneficiaryCustomer.ToUpper());
        SqlParameter P_txtBeneficiaryCustomerAccountNumber = new SqlParameter("@_txtBeneficiaryCustomerAccountNumber", _txtBeneficiaryCustomerAccountNumber.ToUpper());
        SqlParameter P_txtBeneficiaryCustomerSwiftCode = new SqlParameter("@_txtBeneficiaryCustomerSwiftCode", _txtBeneficiaryCustomerSwiftCode.ToUpper());
        SqlParameter P_txtBeneficiaryCustomerName = new SqlParameter("@_txtBeneficiaryCustomerName", _txtBeneficiaryCustomerName.ToUpper());
        SqlParameter P_txtBeneficiaryCustomerAddr1 = new SqlParameter("@_txtBeneficiaryCustomerAddr1", _txtBeneficiaryCustomerAddr1.ToUpper());
        SqlParameter P_txtBeneficiaryCustomerAddr2 = new SqlParameter("@_txtBeneficiaryCustomerAddr2", _txtBeneficiaryCustomerAddr2.ToUpper());
        SqlParameter P_txtBeneficiaryCustomerAddr3 = new SqlParameter("@_txtBeneficiaryCustomerAddr3", _txtBeneficiaryCustomerAddr3.ToUpper());
        SqlParameter P_txtRemittanceInformation1 = new SqlParameter("@_txtRemittanceInformation1", _txtRemittanceInformation1.ToUpper());
        SqlParameter P_txtRemittanceInformation2 = new SqlParameter("@_txtRemittanceInformation2", _txtRemittanceInformation2.ToUpper());
        SqlParameter P_txtRemittanceInformation3 = new SqlParameter("@_txtRemittanceInformation3", _txtRemittanceInformation3.ToUpper());
        SqlParameter P_txtRemittanceInformation4 = new SqlParameter("@_txtRemittanceInformation4", _txtRemittanceInformation4.ToUpper());

        SqlParameter P_txt202Amount = new SqlParameter("@_txt202Amount", _txt202Amount.ToUpper());
        SqlParameter P_ddlOrderingInstitution202 = new SqlParameter("@_ddlOrderingInstitution202", _ddlOrderingInstitution202.ToUpper());
        SqlParameter P_txtOrderingInstitution202AccountNumber = new SqlParameter("@_txtOrderingInstitution202AccountNumber", _txtOrderingInstitution202AccountNumber.ToUpper());
        SqlParameter P_txtOrderingInstitution202SwiftCode = new SqlParameter("@_txtOrderingInstitution202SwiftCode", _txtOrderingInstitution202SwiftCode.ToUpper());
        SqlParameter P_txtOrderingInstitution202Name = new SqlParameter("@_txtOrderingInstitution202Name", _txtOrderingInstitution202Name.ToUpper());
        SqlParameter P_txtOrderingInstitution202Address1 = new SqlParameter("@_txtOrderingInstitution202Address1", _txtOrderingInstitution202Address1.ToUpper());
        SqlParameter P_txtOrderingInstitution202Address2 = new SqlParameter("@_txtOrderingInstitution202Address2", _txtOrderingInstitution202Address2.ToUpper());
        SqlParameter P_txtOrderingInstitution202Address3 = new SqlParameter("@_txtOrderingInstitution202Address3", _txtOrderingInstitution202Address3.ToUpper());
        SqlParameter P_ddlSendersCorrespondent202 = new SqlParameter("@_ddlSendersCorrespondent202", _ddlSendersCorrespondent202.ToUpper());
        SqlParameter P_txtSendersCorrespondent202AccountNumber = new SqlParameter("@_txtSendersCorrespondent202AccountNumber", _txtSendersCorrespondent202AccountNumber.ToUpper());
        SqlParameter P_txtSendersCorrespondent202SwiftCode = new SqlParameter("@_txtSendersCorrespondent202SwiftCode", _txtSendersCorrespondent202SwiftCode.ToUpper());
        SqlParameter P_txtSendersCorrespondent202Name = new SqlParameter("@_txtSendersCorrespondent202Name", _txtSendersCorrespondent202Name.ToUpper());
        SqlParameter P_txtSendersCorrespondent202Location = new SqlParameter("@_txtSendersCorrespondent202Location", _txtSendersCorrespondent202Location.ToUpper());
        SqlParameter P_txtSendersCorrespondent202Address1 = new SqlParameter("@_txtSendersCorrespondent202Address1", _txtSendersCorrespondent202Address1.ToUpper());
        SqlParameter P_txtSendersCorrespondent202Address2 = new SqlParameter("@_txtSendersCorrespondent202Address2", _txtSendersCorrespondent202Address2.ToUpper());
        SqlParameter P_txtSendersCorrespondent202Address3 = new SqlParameter("@_txtSendersCorrespondent202Address3", _txtSendersCorrespondent202Address3.ToUpper());
        SqlParameter P_ddlReceiversCorrespondent202 = new SqlParameter("@_ddlReceiversCorrespondent202", _ddlReceiversCorrespondent202.ToUpper());
        SqlParameter P_txtReceiversCorrespondent202AccountNumber = new SqlParameter("@_txtReceiversCorrespondent202AccountNumber", _txtReceiversCorrespondent202AccountNumber.ToUpper());
        SqlParameter P_txtReceiversCorrespondent202SwiftCode = new SqlParameter("@_txtReceiversCorrespondent202SwiftCode", _txtReceiversCorrespondent202SwiftCode.ToUpper());
        SqlParameter P_txtReceiversCorrespondent202Name = new SqlParameter("@_txtReceiversCorrespondent202Name", _txtReceiversCorrespondent202Name.ToUpper());
        SqlParameter P_txtReceiversCorrespondent202Location = new SqlParameter("@_txtReceiversCorrespondent202Location", _txtReceiversCorrespondent202Location.ToUpper());
        SqlParameter P_txtReceiversCorrespondent202Address1 = new SqlParameter("@_txtReceiversCorrespondent202Address1", _txtReceiversCorrespondent202Address1.ToUpper());
        SqlParameter P_txtReceiversCorrespondent202Address2 = new SqlParameter("@_txtReceiversCorrespondent202Address2", _txtReceiversCorrespondent202Address2.ToUpper());
        SqlParameter P_txtReceiversCorrespondent202Address3 = new SqlParameter("@_txtReceiversCorrespondent202Address3", _txtReceiversCorrespondent202Address3.ToUpper());

        // MT202 & 103 Changes 02122019
        SqlParameter P_ddlIntermediary202 = new SqlParameter("@_ddlIntermediary202", _ddlIntermediary202.ToUpper());
        SqlParameter P_txtIntermediary202AccountNumber = new SqlParameter("@_txtIntermediary202AccountNumber", _txtIntermediary202AccountNumber.ToUpper());
        SqlParameter P_txtIntermediary202SwiftCode = new SqlParameter("@_txtIntermediary202SwiftCode", _txtIntermediary202SwiftCode.ToUpper());
        SqlParameter P_txtIntermediary202Name = new SqlParameter("@_txtIntermediary202Name", _txtIntermediary202Name.ToUpper());
        SqlParameter P_txtIntermediary202Address1 = new SqlParameter("@_txtIntermediary202Address1", _txtIntermediary202Address1.ToUpper());
        SqlParameter P_txtIntermediary202Address2 = new SqlParameter("@_txtIntermediary202Address2", _txtIntermediary202Address2.ToUpper());
        SqlParameter P_txtIntermediary202Address3 = new SqlParameter("@_txtIntermediary202Address3", _txtIntermediary202Address3.ToUpper());
        SqlParameter P_ddlAccountWithInstitution202 = new SqlParameter("@_ddlAccountWithInstitution202", _ddlAccountWithInstitution202.ToUpper());
        SqlParameter P_txtAccountWithInstitution202AccountNumber = new SqlParameter("@_txtAccountWithInstitution202AccountNumber", _txtAccountWithInstitution202AccountNumber.ToUpper());
        SqlParameter P_txtAccountWithInstitution202SwiftCode = new SqlParameter("@_txtAccountWithInstitution202SwiftCode", _txtAccountWithInstitution202SwiftCode.ToUpper());
        SqlParameter P_txtAccountWithInstitution202Name = new SqlParameter("@_txtAccountWithInstitution202Name", _txtAccountWithInstitution202Name.ToUpper());
        SqlParameter P_txtAccountWithInstitution202Location = new SqlParameter("@_txtAccountWithInstitution202Location", _txtAccountWithInstitution202Location.ToUpper());
        SqlParameter P_txtAccountWithInstitution202Address1 = new SqlParameter("@_txtAccountWithInstitution202Address1", _txtAccountWithInstitution202Address1.ToUpper());
        SqlParameter P_txtAccountWithInstitution202Address2 = new SqlParameter("@_txtAccountWithInstitution202Address2", _txtAccountWithInstitution202Address2.ToUpper());
        SqlParameter P_txtAccountWithInstitution202Address3 = new SqlParameter("@_txtAccountWithInstitution202Address3", _txtAccountWithInstitution202Address3.ToUpper());
        SqlParameter P_ddlBeneficiaryInstitution202 = new SqlParameter("@_ddlBeneficiaryInstitution202", _ddlBeneficiaryInstitution202.ToUpper());
        SqlParameter P_txtBeneficiaryInstitution202AccountNumber = new SqlParameter("@_txtBeneficiaryInstitution202AccountNumber", _txtBeneficiaryInstitution202AccountNumber.ToUpper());
        SqlParameter P_txtBeneficiaryInstitution202SwiftCode = new SqlParameter("@_txtBeneficiaryInstitution202SwiftCode", _txtBeneficiaryInstitution202SwiftCode.ToUpper());
        SqlParameter P_txtBeneficiaryInstitution202Name = new SqlParameter("@_txtBeneficiaryInstitution202Name", _txtBeneficiaryInstitution202Name.ToUpper());
        SqlParameter P_txtBeneficiaryInstitution202Address1 = new SqlParameter("@_txtBeneficiaryInstitution202Address1", _txtBeneficiaryInstitution202Address1.ToUpper());
        SqlParameter P_txtBeneficiaryInstitution202Address2 = new SqlParameter("@_txtBeneficiaryInstitution202Address2", _txtBeneficiaryInstitution202Address2.ToUpper());
        SqlParameter P_txtBeneficiaryInstitution202Address3 = new SqlParameter("@_txtBeneficiaryInstitution202Address3", _txtBeneficiaryInstitution202Address3.ToUpper());
        SqlParameter P_txtSenderToReceiverInformation2021 = new SqlParameter("@_txtSenderToReceiverInformation2021", _txtSenderToReceiverInformation2021.ToUpper());
        SqlParameter P_txtSenderToReceiverInformation2022 = new SqlParameter("@_txtSenderToReceiverInformation2022", _txtSenderToReceiverInformation2022.ToUpper());
        SqlParameter P_txtSenderToReceiverInformation2023 = new SqlParameter("@_txtSenderToReceiverInformation2023", _txtSenderToReceiverInformation2023.ToUpper());
        SqlParameter P_txtSenderToReceiverInformation2024 = new SqlParameter("@_txtSenderToReceiverInformation2024", _txtSenderToReceiverInformation2024.ToUpper());
        SqlParameter P_txtSenderToReceiverInformation2025 = new SqlParameter("@_txtSenderToReceiverInformation2025", _txtSenderToReceiverInformation2025.ToUpper());
        SqlParameter P_txtSenderToReceiverInformation2026 = new SqlParameter("@_txtSenderToReceiverInformation2026", _txtSenderToReceiverInformation2026.ToUpper());

        SqlParameter P_ddlIntermediary103 = new SqlParameter("@_ddlIntermediary103", _ddlIntermediary103.ToUpper());
        SqlParameter P_txtIntermediary103AccountNumber = new SqlParameter("@_txtIntermediary103AccountNumber", _txtIntermediary103AccountNumber.ToUpper());
        SqlParameter P_txtIntermediary103SwiftCode = new SqlParameter("@_txtIntermediary103SwiftCode", _txtIntermediary103SwiftCode.ToUpper());
        SqlParameter P_txtIntermediary103Name = new SqlParameter("@_txtIntermediary103Name", _txtIntermediary103Name.ToUpper());
        SqlParameter P_txtIntermediary103Address1 = new SqlParameter("@_txtIntermediary103Address1", _txtIntermediary103Address1.ToUpper());
        SqlParameter P_txtIntermediary103Address2 = new SqlParameter("@_txtIntermediary103Address2", _txtIntermediary103Address2.ToUpper());
        SqlParameter P_txtIntermediary103Address3 = new SqlParameter("@_txtIntermediary103Address3", _txtIntermediary103Address3.ToUpper());
        SqlParameter P_ddlAccountWithInstitution103 = new SqlParameter("@_ddlAccountWithInstitution103", _ddlAccountWithInstitution103.ToUpper());
        SqlParameter P_txtAccountWithInstitution103AccountNumber = new SqlParameter("@_txtAccountWithInstitution103AccountNumber", _txtAccountWithInstitution103AccountNumber.ToUpper());
        SqlParameter P_txtAccountWithInstitution103SwiftCode = new SqlParameter("@_txtAccountWithInstitution103SwiftCode", _txtAccountWithInstitution103SwiftCode.ToUpper());
        SqlParameter P_txtAccountWithInstitution103Name = new SqlParameter("@_txtAccountWithInstitution103Name", _txtAccountWithInstitution103Name.ToUpper());
        SqlParameter P_txtAccountWithInstitution103Location = new SqlParameter("@_txtAccountWithInstitution103Location", _txtAccountWithInstitution103Location.ToUpper());
        SqlParameter P_txtAccountWithInstitution103Address1 = new SqlParameter("@_txtAccountWithInstitution103Address1", _txtAccountWithInstitution103Address1.ToUpper());
        SqlParameter P_txtAccountWithInstitution103Address2 = new SqlParameter("@_txtAccountWithInstitution103Address2", _txtAccountWithInstitution103Address2.ToUpper());
        SqlParameter P_txtAccountWithInstitution103Address3 = new SqlParameter("@_txtAccountWithInstitution103Address3", _txtAccountWithInstitution103Address3.ToUpper());


        SqlParameter P_rdb_swift_None = new SqlParameter("@_rdb_swift_None", _rdb_swift_None.ToUpper());
        SqlParameter P_rdb_swift_103 = new SqlParameter("@_rdb_swift_103", _rdb_swift_103.ToUpper());
        SqlParameter P_rdb_swift_200 = new SqlParameter("@_rdb_swift_200", _rdb_swift_200.ToUpper());
        SqlParameter P_rdb_swift_202 = new SqlParameter("@_rdb_swift_202", _rdb_swift_202.ToUpper());

        SqlParameter P_txt200BicCode = new SqlParameter("@_txt200BicCode", _txt200BicCode.ToUpper());
        SqlParameter P_txt200TransactionRefNO = new SqlParameter("@_txt200TransactionRefNO", _txt200TransactionRefNO.ToUpper());
        SqlParameter P_txt200Date = new SqlParameter("@_txt200Date", _txt200Date.ToUpper());
        SqlParameter P_txt200Currency = new SqlParameter("@_txt200Currency", _txt200Currency.ToUpper());
        SqlParameter P_txt200Amount = new SqlParameter("@_txt200Amount", _txt200Amount.ToUpper());
        SqlParameter P_txt200SenderCorreCode = new SqlParameter("@_txt200SenderCorreCode", _txt200SenderCorreCode.ToUpper());
        SqlParameter P_txt200SenderCorreLocation = new SqlParameter("@_txt200SenderCorreLocation", _txt200SenderCorreLocation.ToUpper());
        SqlParameter P_ddl200Intermediary = new SqlParameter("@_ddl200Intermediary", _ddl200Intermediary.ToUpper());
        SqlParameter P_txt200IntermediaryAccountNumber = new SqlParameter("@_txt200IntermediaryAccountNumber", _txt200IntermediaryAccountNumber.ToUpper());
        SqlParameter P_txt200IntermediarySwiftCode = new SqlParameter("@_txt200IntermediarySwiftCode", _txt200IntermediarySwiftCode.ToUpper());
        SqlParameter P_txt200IntermediaryName = new SqlParameter("@_txt200IntermediaryName", _txt200IntermediaryName.ToUpper());
        SqlParameter P_txt200IntermediaryAddress1 = new SqlParameter("@_txt200IntermediaryAddress1", _txt200IntermediaryAddress1.ToUpper());
        SqlParameter P_txt200IntermediaryAddress2 = new SqlParameter("@_txt200IntermediaryAddress2", _txt200IntermediaryAddress2.ToUpper());
        SqlParameter P_txt200IntermediaryAddress3 = new SqlParameter("@_txt200IntermediaryAddress3", _txt200IntermediaryAddress3.ToUpper());
        SqlParameter P_ddl200AccWithInstitution = new SqlParameter("@_ddl200AccWithInstitution", _ddl200AccWithInstitution.ToUpper());
        SqlParameter P_txt200AccWithInstitutionAccountNumber = new SqlParameter("@_txt200AccWithInstitutionAccountNumber", _txt200AccWithInstitutionAccountNumber.ToUpper());
        SqlParameter P_txt200AccWithInstitutionSwiftCode = new SqlParameter("@_txt200AccWithInstitutionSwiftCode", _txt200AccWithInstitutionSwiftCode.ToUpper());
        SqlParameter P_txt200AccWithInstitutionLocation = new SqlParameter("@_txt200AccWithInstitutionLocation", _txt200AccWithInstitutionLocation.ToUpper());
        SqlParameter P_txt200AccWithInstitutionName = new SqlParameter("@_txt200AccWithInstitutionName", _txt200AccWithInstitutionName.ToUpper());
        SqlParameter P_txt200AccWithInstitutionAddress1 = new SqlParameter("@_txt200AccWithInstitutionAddress1", _txt200AccWithInstitutionAddress1.ToUpper());
        SqlParameter P_txt200AccWithInstitutionAddress2 = new SqlParameter("@_txt200AccWithInstitutionAddress2", _txt200AccWithInstitutionAddress2.ToUpper());
        SqlParameter P_txt200AccWithInstitutionAddress3 = new SqlParameter("@_txt200AccWithInstitutionAddress3", _txt200AccWithInstitutionAddress3.ToUpper());
        SqlParameter P_txt200SendertoReceiverInformation1 = new SqlParameter("@_txt200SendertoReceiverInformation1", _txt200SendertoReceiverInformation1.ToUpper());
        SqlParameter P_txt200SendertoReceiverInformation2 = new SqlParameter("@_txt200SendertoReceiverInformation2", _txt200SendertoReceiverInformation2.ToUpper());
        SqlParameter P_txt200SendertoReceiverInformation3 = new SqlParameter("@_txt200SendertoReceiverInformation3", _txt200SendertoReceiverInformation3.ToUpper());
        SqlParameter P_txt200SendertoReceiverInformation4 = new SqlParameter("@_txt200SendertoReceiverInformation4", _txt200SendertoReceiverInformation4.ToUpper());
        SqlParameter P_txt200SendertoReceiverInformation5 = new SqlParameter("@_txt200SendertoReceiverInformation5", _txt200SendertoReceiverInformation5.ToUpper());
        SqlParameter P_txt200SendertoReceiverInformation6 = new SqlParameter("@_txt200SendertoReceiverInformation6", _txt200SendertoReceiverInformation6.ToUpper());

        SqlParameter P_txtTransactionRefNoR42 = new SqlParameter("@_txtTransactionRefNoR42", _txtTransactionRefNoR42.ToUpper());
        SqlParameter P_txtRelatedReferenceR42 = new SqlParameter("@_txtRelatedReferenceR42", _txtRelatedReferenceR42.ToUpper());
        SqlParameter P_txtValueDateR42 = new SqlParameter("@_txtValueDateR42", _txtValueDateR42.ToUpper());
        SqlParameter P_txtCureencyR42 = new SqlParameter("@_txtCureencyR42", _txtCureencyR42.ToUpper());
        SqlParameter P_txtAmountR42 = new SqlParameter("@_txtAmountR42", _txtAmountR42.ToUpper());
        SqlParameter P_txtOrderingInstitutionIFSCR42 = new SqlParameter("@_txtOrderingInstitutionIFSCR42", _txtOrderingInstitutionIFSCR42.ToUpper());
        SqlParameter P_txtBeneficiaryInstitutionIFSCR42 = new SqlParameter("@_txtBeneficiaryInstitutionIFSCR42", _txtBeneficiaryInstitutionIFSCR42.ToUpper());
        SqlParameter P_txtCodeWordR42 = new SqlParameter("@_txtCodeWordR42", _txtCodeWordR42.ToUpper());
        SqlParameter P_txtAdditionalInformationR42 = new SqlParameter("@_txtAdditionalInformationR42", _txtAdditionalInformationR42.ToUpper());
        SqlParameter P_txtMoreInfo1R42 = new SqlParameter("@_txtMoreInfo1R42", _txtMoreInfo1R42.ToUpper());
        SqlParameter P_txtMoreInfo2R42 = new SqlParameter("@_txtMoreInfo2R42", _txtMoreInfo2R42.ToUpper());
        SqlParameter P_txtMoreInfo3R42 = new SqlParameter("@_txtMoreInfo3R42", _txtMoreInfo3R42.ToUpper());
        SqlParameter P_txtMoreInfo4R42 = new SqlParameter("@_txtMoreInfo4R42", _txtMoreInfo4R42.ToUpper());
        SqlParameter P_txtMoreInfo5R42 = new SqlParameter("@_txtMoreInfo5R42", _txtMoreInfo5R42.ToUpper());
        SqlParameter P_rdb_swift_R42 = new SqlParameter("@_rdb_swift_R42", _rdb_swift_R42.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));



        //--------------------------------------------------------------Nilesh-----------------------------------------------------------------------/
        SqlParameter P_MT754_Flag = new SqlParameter("@MT754_Flag", MT754_Flag.ToUpper());
        SqlParameter P_txt_754_SenRef = new SqlParameter("@Sender_Ref_754", _txt_754_SenRef.ToUpper());
        SqlParameter P_txt_754_RelRef = new SqlParameter("@Related_Ref_754", _txt_754_RelRef.ToUpper());
        SqlParameter P_ddlPrinAmtPaidAccNego_754 = new SqlParameter("@Pri_Amt_Paid_754", _ddlPrinAmtPaidAccNego_754.ToUpper());
        SqlParameter P_txtPrinAmtPaidAccNegoDate_754 = new SqlParameter("@Pri_AMT_Date_754", _txtPrinAmtPaidAccNegoDate_754.ToUpper());
        SqlParameter P_txtPrinAmtPaidAccNegoCurr_754 = new SqlParameter("@Pri_AMT_Curr_754", _txtPrinAmtPaidAccNegoCurr_754.ToUpper());
        SqlParameter P_txtPrinAmtPaidAccNegoAmt_754 = new SqlParameter("@Pri_AMT_Amt_754", _txtPrinAmtPaidAccNegoAmt_754.ToUpper());
        SqlParameter P_txt_754_AddAmtClamd_Ccy = new SqlParameter("@Additional_AMT_Curr_754", _txt_754_AddAmtClamd_Ccy.ToUpper());
        SqlParameter P_txt_754_AddAmtClamd_Amt = new SqlParameter("@Additional_AMT_Amt_754", _txt_754_AddAmtClamd_Amt.ToUpper());
        SqlParameter P_txt_MT754_Charges_Deducted = new SqlParameter("@Charges_deducted1_754", _txt_MT754_Charges_Deducted.ToUpper());
        SqlParameter P_txt_MT754_Charges_Deducted2 = new SqlParameter("@Charges_deducted2_754", _txt_MT754_Charges_Deducted2.ToUpper());
        SqlParameter P_txt_MT754_Charges_Deducted3 = new SqlParameter("@Charges_deducted3_754", _txt_MT754_Charges_Deducted3.ToUpper());
        SqlParameter P_txt_MT754_Charges_Deducted4 = new SqlParameter("@Charges_deducted4_754", _txt_MT754_Charges_Deducted4.ToUpper());
        SqlParameter P_txt_MT754_Charges_Deducted5 = new SqlParameter("@Charges_deducted5_754", _txt_MT754_Charges_Deducted5.ToUpper());
        SqlParameter P_txt_MT754_Charges_Deducted6 = new SqlParameter("@Charges_deducted6_754", _txt_MT754_Charges_Deducted6.ToUpper());
        SqlParameter P_txt_MT754_Charges_Added = new SqlParameter("@Charges_Added1_754", _txt_MT754_Charges_Added.ToUpper());
        SqlParameter P_txt_MT754_Charges_Added2 = new SqlParameter("@Charges_Added2_754", _txt_MT754_Charges_Added2.ToUpper());
        SqlParameter P_txt_MT754_Charges_Added3 = new SqlParameter("@Charges_Added3_754", _txt_MT754_Charges_Added3.ToUpper());
        SqlParameter P_txt_MT754_Charges_Added4 = new SqlParameter("@Charges_Added4_754", _txt_MT754_Charges_Added4.ToUpper());
        SqlParameter P_txt_MT754_Charges_Added5 = new SqlParameter("@Charges_Added5_754", _txt_MT754_Charges_Added5.ToUpper());
        SqlParameter P_txt_MT754_Charges_Added6 = new SqlParameter("@Charges_Added6_754", _txt_MT754_Charges_Added6.ToUpper());
        SqlParameter P_ddlTotalAmtclamd_754 = new SqlParameter("@Ttl_Amt_Clmd_754", _ddlTotalAmtclamd_754.ToUpper());
        SqlParameter P_txt_754_TotalAmtClmd_Date = new SqlParameter("@Ttl_Amt_Clmd_Date_754", _txt_754_TotalAmtClmd_Date.ToUpper());
        SqlParameter P_txt_754_TotalAmtClmd_Ccy = new SqlParameter("@Ttl_Amt_Clmd_Curr_754", _txt_754_TotalAmtClmd_Ccy.ToUpper());
        SqlParameter P_txt_754_TotalAmtClmd_Amt = new SqlParameter("@Ttl_Amt_Clmd_AMT_754", _txt_754_TotalAmtClmd_Amt.ToUpper());
        SqlParameter P_ddlReimbursingbank_754 = new SqlParameter("@Reimbursing_Bank_754", _ddlReimbursingbank_754.ToUpper());
        SqlParameter P_txtReimbursingBankAccountnumber_754 = new SqlParameter("@Reimbursing_Bank_Accno_754", _txtReimbursingBankAccountnumber_754.ToUpper());
        SqlParameter P_txtReimbursingBankpartyidentifier_754 = new SqlParameter("@Reimbur_Bank_PartyIdent_754", _txtReimbursingBankpartyidentifier_754.ToUpper());
        SqlParameter P_txtReimbursingBankIdentifiercode_754 = new SqlParameter("@Reimbur_Bank_IdentCode_754", _txtReimbursingBankIdentifiercode_754.ToUpper());
        SqlParameter P_txtReimbursingBankLocation_754 = new SqlParameter("@Reimbur_Bank_Lctn_754", _txtReimbursingBankLocation_754.ToUpper());
        SqlParameter P_txtReimbursingBankName_754 = new SqlParameter("@Reimbursing_Bank_Name_754", _txtReimbursingBankName_754.ToUpper());
        SqlParameter P_txtReimbursingBankAddress1_754 = new SqlParameter("@Reimbur_Bank_Addrs1_754", _txtReimbursingBankAddress1_754.ToUpper());
        SqlParameter P_txtReimbursingBankAddress2_754 = new SqlParameter("@Reimbur_Bank_Addrs2_754", _txtReimbursingBankAddress2_754.ToUpper());
        SqlParameter P_txtReimbursingBankAddress3_754 = new SqlParameter("@Reimbur_Bank_Addrs3_754", _txtReimbursingBankAddress3_754.ToUpper());
        SqlParameter P_ddlAccountwithbank_754 = new SqlParameter("@Account_With_Bank", _ddlAccountwithbank_754.ToUpper());
        SqlParameter P_txtAccountwithBankAccountnumber_754 = new SqlParameter("@Acnt_With_Bank_Acc_754", _txtAccountwithBankAccountnumber_754.ToUpper());
        SqlParameter P_txtAccountwithBankpartyidentifier_754 = new SqlParameter("@Acnt_with_Bank_PartyIdent_754", _txtAccountwithBankpartyidentifier_754.ToUpper());
        SqlParameter P_txtAccountwithBankIdentifiercode_754 = new SqlParameter("@Acnt_With_Bank_IdentCode_754", _txtAccountwithBankIdentifiercode_754.ToUpper());
        SqlParameter P_txtAccountwithBankLocation_754 = new SqlParameter("@Acnt_With_Bank_Loctn_754", _txtAccountwithBankLocation_754.ToUpper());
        SqlParameter P_txtAccountwithBankName_754 = new SqlParameter("@Acnt_With_Bank_Name", _txtAccountwithBankName_754.ToUpper());
        SqlParameter P_txtAccountwithBankAddress1_754 = new SqlParameter("@Acnt_With_Bank_Addr1_754", _txtAccountwithBankAddress1_754.ToUpper());
        SqlParameter P_txtAccountwithBankAddress2_754 = new SqlParameter("@Acnt_With_Bank_Addr2_754", _txtAccountwithBankAddress2_754.ToUpper());
        SqlParameter P_txtAccountwithBankAddress3_754 = new SqlParameter("@Acnt_With_Bank_Addr3_754", _txtAccountwithBankAddress3_754.ToUpper());
        SqlParameter P_ddlBeneficiarybank_754 = new SqlParameter("@Banificiary_Bank_754", _ddlBeneficiarybank_754.ToUpper());
        SqlParameter P_txtBeneficiaryBankAccountnumber_754 = new SqlParameter("@Banificiary_AccNo_754", _txtBeneficiaryBankAccountnumber_754.ToUpper());
        SqlParameter P_txtBeneficiarypartyidentifire = new SqlParameter("@Banificiary_PartyIdent_754", _txtBeneficiarypartyidentifire.ToUpper());
        SqlParameter P_txtBeneficiaryBankIdentifiercode_754 = new SqlParameter("@Banificiary_IdentCode_754", _txtBeneficiaryBankIdentifiercode_754.ToUpper());
        SqlParameter P_txtBeneficiaryBankName_754 = new SqlParameter("@Banificiary_Bank_Name_754", _txtBeneficiaryBankName_754.ToUpper());
        SqlParameter P_txtBeneficiaryBankAddress1_754 = new SqlParameter("@Banificiary_Addr1_754", _txtBeneficiaryBankAddress1_754.ToUpper());
        SqlParameter P_txtBeneficiaryBankAddress2_754 = new SqlParameter("@Banificiary_Addr2_754", _txtBeneficiaryBankAddress2_754.ToUpper());
        SqlParameter P_txtBeneficiaryBankAddress3_754 = new SqlParameter("@Banificiary_Addr3_754", _txtBeneficiaryBankAddress3_754.ToUpper());
        SqlParameter P_txt_MT754_Sender_to_Receiver_Information = new SqlParameter("@SendertoReceiverInformation_754", _txt_MT754_Sender_to_Receiver_Information.ToUpper());
        SqlParameter P_txt_MT754_Sender_to_Receiver_Information2 = new SqlParameter("@SendertoReceiverInformation2_754", _txt_MT754_Sender_to_Receiver_Information2.ToUpper());
        SqlParameter P_txt_MT754_Sender_to_Receiver_Information3 = new SqlParameter("@SendertoReceiverInformation3_754", _txt_MT754_Sender_to_Receiver_Information3.ToUpper());
        SqlParameter P_txt_MT754_Sender_to_Receiver_Information4 = new SqlParameter("@SendertoReceiverInformation4_754", _txt_MT754_Sender_to_Receiver_Information4.ToUpper());
        SqlParameter P_txt_MT754_Sender_to_Receiver_Information5 = new SqlParameter("@SendertoReceiverInformation5_754", _txt_MT754_Sender_to_Receiver_Information5.ToUpper());
        SqlParameter P_txt_MT754_Sender_to_Receiver_Information6 = new SqlParameter("@SendertoReceiverInformation6_754", _txt_MT754_Sender_to_Receiver_Information6.ToUpper());




        SqlParameter P_txt_Narrative_754_1 = new SqlParameter("@_txt_Narrative_754_1", _txt_Narrative_754_1.ToUpper());
        SqlParameter P_txt_Narrative_754_2 = new SqlParameter("@_txt_Narrative_754_2", _txt_Narrative_754_2.ToUpper());
        SqlParameter P_txt_Narrative_754_3 = new SqlParameter("@_txt_Narrative_754_3", _txt_Narrative_754_3.ToUpper());
        SqlParameter P_txt_Narrative_754_4 = new SqlParameter("@_txt_Narrative_754_4", _txt_Narrative_754_4.ToUpper());
        SqlParameter P_txt_Narrative_754_5 = new SqlParameter("@_txt_Narrative_754_5", _txt_Narrative_754_5.ToUpper());
        SqlParameter P_txt_Narrative_754_6 = new SqlParameter("@_txt_Narrative_754_6", _txt_Narrative_754_6.ToUpper());
        SqlParameter P_txt_Narrative_754_7 = new SqlParameter("@_txt_Narrative_754_7", _txt_Narrative_754_7.ToUpper());
        SqlParameter P_txt_Narrative_754_8 = new SqlParameter("@_txt_Narrative_754_8", _txt_Narrative_754_8.ToUpper());
        SqlParameter P_txt_Narrative_754_9 = new SqlParameter("@_txt_Narrative_754_9", _txt_Narrative_754_9.ToUpper());
        SqlParameter P_txt_Narrative_754_10 = new SqlParameter("@_txt_Narrative_754_10", _txt_Narrative_754_10.ToUpper());
        SqlParameter P_txt_Narrative_754_11 = new SqlParameter("@_txt_Narrative_754_11", _txt_Narrative_754_11.ToUpper());
        SqlParameter P_txt_Narrative_754_12 = new SqlParameter("@_txt_Narrative_754_12", _txt_Narrative_754_12.ToUpper());
        SqlParameter P_txt_Narrative_754_13 = new SqlParameter("@_txt_Narrative_754_13", _txt_Narrative_754_13.ToUpper());
        SqlParameter P_txt_Narrative_754_14 = new SqlParameter("@_txt_Narrative_754_14", _txt_Narrative_754_14.ToUpper());
        SqlParameter P_txt_Narrative_754_15 = new SqlParameter("@_txt_Narrative_754_15", _txt_Narrative_754_15.ToUpper());
        SqlParameter P_txt_Narrative_754_16 = new SqlParameter("@_txt_Narrative_754_16", _txt_Narrative_754_16.ToUpper());
        SqlParameter P_txt_Narrative_754_17 = new SqlParameter("@_txt_Narrative_754_17", _txt_Narrative_754_17.ToUpper());
        SqlParameter P_txt_Narrative_754_18 = new SqlParameter("@_txt_Narrative_754_18", _txt_Narrative_754_18.ToUpper());
        SqlParameter P_txt_Narrative_754_19 = new SqlParameter("@_txt_Narrative_754_19", _txt_Narrative_754_19.ToUpper());
        SqlParameter P_txt_Narrative_754_20 = new SqlParameter("@_txt_Narrative_754_20", _txt_Narrative_754_20.ToUpper());
        SqlParameter P_txt_Narrative_754_21 = new SqlParameter("@_txt_Narrative_754_21", _txt_Narrative_754_21.ToUpper());
        SqlParameter P_txt_Narrative_754_22 = new SqlParameter("@_txt_Narrative_754_22", _txt_Narrative_754_22.ToUpper());
        SqlParameter P_txt_Narrative_754_23 = new SqlParameter("@_txt_Narrative_754_23", _txt_Narrative_754_23.ToUpper());
        SqlParameter P_txt_Narrative_754_24 = new SqlParameter("@_txt_Narrative_754_24", _txt_Narrative_754_24.ToUpper());
        SqlParameter P_txt_Narrative_754_25 = new SqlParameter("@_txt_Narrative_754_25", _txt_Narrative_754_25.ToUpper());
        SqlParameter P_txt_Narrative_754_26 = new SqlParameter("@_txt_Narrative_754_26", _txt_Narrative_754_26.ToUpper());
        SqlParameter P_txt_Narrative_754_27 = new SqlParameter("@_txt_Narrative_754_27", _txt_Narrative_754_27.ToUpper());
        SqlParameter P_txt_Narrative_754_28 = new SqlParameter("@_txt_Narrative_754_28", _txt_Narrative_754_28.ToUpper());
        SqlParameter P_txt_Narrative_754_29 = new SqlParameter("@_txt_Narrative_754_29", _txt_Narrative_754_29.ToUpper());
        SqlParameter P_txt_Narrative_754_30 = new SqlParameter("@_txt_Narrative_754_30", _txt_Narrative_754_30.ToUpper());
        SqlParameter P_txt_Narrative_754_31 = new SqlParameter("@_txt_Narrative_754_31", _txt_Narrative_754_31.ToUpper());
        SqlParameter P_txt_Narrative_754_32 = new SqlParameter("@_txt_Narrative_754_32", _txt_Narrative_754_32.ToUpper());
        SqlParameter P_txt_Narrative_754_33 = new SqlParameter("@_txt_Narrative_754_33", _txt_Narrative_754_33.ToUpper());
        SqlParameter P_txt_Narrative_754_34 = new SqlParameter("@_txt_Narrative_754_34", _txt_Narrative_754_34.ToUpper());
        SqlParameter P_txt_Narrative_754_35 = new SqlParameter("@_txt_Narrative_754_35", _txt_Narrative_754_35.ToUpper());
        //----------------------------------------------------------Nilesh END---------------------------------------------------------------------------------------------------

        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdate_Settlement_Collection",
        P_BranchName, P_txtDocNo, P_txt_Doc_Value_Date,
        P_txt_Doc_Comment_Code, P_txt_Doc_Maturity,
        P_txt_Doc_Settlement_for_Cust, P_txt_Doc_Settlement_For_Bank,
        P_txt_Doc_Interest_From, P_txt_Doc_Interest_To, P_txt_Doc_No_Of_Days, P_txt_Doc_Discount,
        P_txt_Doc_InterestRate, P_txt_Doc_InterestAmount,
        P_txt_Doc_Overdue_Interestrate, P_txt_Doc_Oveduenoofdays, P_txt_Doc_Overdueamount,
        P_txt_Doc_Attn,
            ////////////Import Accounting 1
        P_chk_IMPACC1Flag, P_txt_IMPACC1_FCRefNo,
        P_txt_IMPACC1_DiscAmt, P_txt_IMPACC1_DiscExchRate,
        P_txt_IMPACC1_Princ_matu, P_txt_IMPACC1_Princ_lump, P_txt_IMPACC1_Princ_Contract_no, P_txt_IMPACC1_Princ_Ex_Curr, P_txt_IMPACC1_Princ_Ex_rate, P_txt_IMPACC1_Princ_Intnl_Ex_rate,
        P_txt_IMPACC1_Interest_matu, P_txt_IMPACC1_Interest_lump, P_txt_IMPACC1_Interest_Contract_no, P_txt_IMPACC1_Interest_Ex_Curr, P_txt_IMPACC1_Interest_Ex_rate, P_txt_IMPACC1_Interest_Intnl_Ex_rate,
        P_txt_IMPACC1_Commission_matu, P_txt_IMPACC1_Commission_lump, P_txt_IMPACC1_Commission_Contract_no, P_txt_IMPACC1_Commission_Ex_Curr, P_txt_IMPACC1_Commission_Ex_rate, P_txt_IMPACC1_Commission_Intnl_Ex_rate,
        P_txt_IMPACC1_Their_Commission_matu, P_txt_IMPACC1_Their_Commission_lump, P_txt_IMPACC1_Their_Commission_Contract_no, P_txt_IMPACC1_Their_Commission_Ex_Curr, P_txt_IMPACC1_Their_Commission_Ex_rate, P_txt_IMPACC1_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC1_CR_Code, P_txt_IMPACC1_CR_AC_Short_Name, P_txt_IMPACC1_CR_Cust_abbr, P_txt_IMPACC1_CR_Cust_Acc, P_txt_IMPACC1_CR_Acceptance_Curr, P_txt_IMPACC1_CR_Acceptance_amt, P_txt_IMPACC1_CR_Acceptance_payer,
        P_txt_IMPACC1_CR_Interest_Curr, P_txt_IMPACC1_CR_Interest_amt, P_txt_IMPACC1_CR_Interest_payer,
        P_txt_IMPACC1_CR_Accept_Commission_Curr, P_txt_IMPACC1_CR_Accept_Commission_amt, P_txt_IMPACC1_CR_Accept_Commission_Payer,
        P_txt_IMPACC1_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC1_CR_Pay_Handle_Commission_amt, P_txt_IMPACC1_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC1_CR_Others_Curr, P_txt_IMPACC1_CR_Others_amt, P_txt_IMPACC1_CR_Others_Payer,
        P_txt_IMPACC1_CR_Their_Commission_Curr, P_txt_IMPACC1_CR_Their_Commission_amt, P_txt_IMPACC1_CR_Their_Commission_Payer,
        P_txt_IMPACC1_DR_Code, P_txt_IMPACC1_DR_AC_Short_Name, P_txt_IMPACC1_DR_Cust_abbr, P_txt_IMPACC1_DR_Cust_Acc,
        P_txt_IMPACC1_DR_Cur_Acc_Curr, P_txt_IMPACC1_DR_Cur_Acc_amt, P_txt_IMPACC1_DR_Cur_Acc_payer,
        P_txt_IMPACC1_DR_Cur_Acc_Curr2, P_txt_IMPACC1_DR_Cur_Acc_amt2, P_txt_IMPACC1_DR_Cur_Acc_payer2,
        P_txt_IMPACC1_DR_Cur_Acc_Curr3, P_txt_IMPACC1_DR_Cur_Acc_amt3, P_txt_IMPACC1_DR_Cur_Acc_payer3,
        P_txt_IMPACC1_DR_Cur_Acc_Curr4, P_txt_IMPACC1_DR_Cur_Acc_amt4, P_txt_IMPACC1_DR_Cur_Acc_payer4,
        P_txt_IMPACC1_DR_Cur_Acc_Curr5, P_txt_IMPACC1_DR_Cur_Acc_amt5, P_txt_IMPACC1_DR_Cur_Acc_payer5,
        P_txt_IMPACC1_DR_Cur_Acc_Curr6, P_txt_IMPACC1_DR_Cur_Acc_amt6, P_txt_IMPACC1_DR_Cur_Acc_payer6,

        P_txt_IMPACC1_DR_Code2, P_txt_IMPACC1_DR_AC_Short_Name2, P_txt_IMPACC1_DR_Cust_abbr2, P_txt_IMPACC1_DR_Cust_Acc2,
        P_txt_IMPACC1_DR_Code3, P_txt_IMPACC1_DR_AC_Short_Name3, P_txt_IMPACC1_DR_Cust_abbr3, P_txt_IMPACC1_DR_Cust_Acc3,
        P_txt_IMPACC1_DR_Code4, P_txt_IMPACC1_DR_AC_Short_Name4, P_txt_IMPACC1_DR_Cust_abbr4, P_txt_IMPACC1_DR_Cust_Acc4,
        P_txt_IMPACC1_DR_Code5, P_txt_IMPACC1_DR_AC_Short_Name5, P_txt_IMPACC1_DR_Cust_abbr5, P_txt_IMPACC1_DR_Cust_Acc5,
        P_txt_IMPACC1_DR_Code6, P_txt_IMPACC1_DR_AC_Short_Name6, P_txt_IMPACC1_DR_Cust_abbr6, P_txt_IMPACC1_DR_Cust_Acc6,

            ////////////Import Accounting 2
        P_chk_IMPACC2Flag, P_txt_IMPACC2_FCRefNo,
        P_txt_IMPACC2_DiscAmt, P_txt_IMPACC2_DiscExchRate,
        P_txt_IMPACC2_Princ_matu, P_txt_IMPACC2_Princ_lump, P_txt_IMPACC2_Princ_Contract_no, P_txt_IMPACC2_Princ_Ex_Curr, P_txt_IMPACC2_Princ_Ex_rate, P_txt_IMPACC2_Princ_Intnl_Ex_rate,
        P_txt_IMPACC2_Interest_matu, P_txt_IMPACC2_Interest_lump, P_txt_IMPACC2_Interest_Contract_no, P_txt_IMPACC2_Interest_Ex_Curr, P_txt_IMPACC2_Interest_Ex_rate, P_txt_IMPACC2_Interest_Intnl_Ex_rate,
        P_txt_IMPACC2_Commission_matu, P_txt_IMPACC2_Commission_lump, P_txt_IMPACC2_Commission_Contract_no, P_txt_IMPACC2_Commission_Ex_Curr, P_txt_IMPACC2_Commission_Ex_rate, P_txt_IMPACC2_Commission_Intnl_Ex_rate,
        P_txt_IMPACC2_Their_Commission_matu, P_txt_IMPACC2_Their_Commission_lump, P_txt_IMPACC2_Their_Commission_Contract_no, P_txt_IMPACC2_Their_Commission_Ex_Curr, P_txt_IMPACC2_Their_Commission_Ex_rate, P_txt_IMPACC2_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC2_CR_Code, P_txt_IMPACC2_CR_AC_Short_Name, P_txt_IMPACC2_CR_Cust_abbr, P_txt_IMPACC2_CR_Cust_Acc, P_txt_IMPACC2_CR_Acceptance_Curr, P_txt_IMPACC2_CR_Acceptance_amt, P_txt_IMPACC2_CR_Acceptance_payer,
        P_txt_IMPACC2_CR_Interest_Curr, P_txt_IMPACC2_CR_Interest_amt, P_txt_IMPACC2_CR_Interest_payer,
        P_txt_IMPACC2_CR_Accept_Commission_Curr, P_txt_IMPACC2_CR_Accept_Commission_amt, P_txt_IMPACC2_CR_Accept_Commission_Payer,
        P_txt_IMPACC2_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC2_CR_Pay_Handle_Commission_amt, P_txt_IMPACC2_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC2_CR_Others_Curr, P_txt_IMPACC2_CR_Others_amt, P_txt_IMPACC2_CR_Others_Payer,
        P_txt_IMPACC2_CR_Their_Commission_Curr, P_txt_IMPACC2_CR_Their_Commission_amt, P_txt_IMPACC2_CR_Their_Commission_Payer,
        P_txt_IMPACC2_DR_Code, P_txt_IMPACC2_DR_AC_Short_Name, P_txt_IMPACC2_DR_Cust_abbr, P_txt_IMPACC2_DR_Cust_Acc,
        P_txt_IMPACC2_DR_Cur_Acc_Curr, P_txt_IMPACC2_DR_Cur_Acc_amt, P_txt_IMPACC2_DR_Cur_Acc_payer,
        P_txt_IMPACC2_DR_Cur_Acc_Curr2, P_txt_IMPACC2_DR_Cur_Acc_amt2, P_txt_IMPACC2_DR_Cur_Acc_payer2,
        P_txt_IMPACC2_DR_Cur_Acc_Curr3, P_txt_IMPACC2_DR_Cur_Acc_amt3, P_txt_IMPACC2_DR_Cur_Acc_payer3,
        P_txt_IMPACC2_DR_Cur_Acc_Curr4, P_txt_IMPACC2_DR_Cur_Acc_amt4, P_txt_IMPACC2_DR_Cur_Acc_payer4,
        P_txt_IMPACC2_DR_Cur_Acc_Curr5, P_txt_IMPACC2_DR_Cur_Acc_amt5, P_txt_IMPACC2_DR_Cur_Acc_payer5,
        P_txt_IMPACC2_DR_Cur_Acc_Curr6, P_txt_IMPACC2_DR_Cur_Acc_amt6, P_txt_IMPACC2_DR_Cur_Acc_payer6,

        P_txt_IMPACC2_DR_Code2, P_txt_IMPACC2_DR_AC_Short_Name2, P_txt_IMPACC2_DR_Cust_abbr2, P_txt_IMPACC2_DR_Cust_Acc2,
        P_txt_IMPACC2_DR_Code3, P_txt_IMPACC2_DR_AC_Short_Name3, P_txt_IMPACC2_DR_Cust_abbr3, P_txt_IMPACC2_DR_Cust_Acc3,
        P_txt_IMPACC2_DR_Code4, P_txt_IMPACC2_DR_AC_Short_Name4, P_txt_IMPACC2_DR_Cust_abbr4, P_txt_IMPACC2_DR_Cust_Acc4,
        P_txt_IMPACC2_DR_Code5, P_txt_IMPACC2_DR_AC_Short_Name5, P_txt_IMPACC2_DR_Cust_abbr5, P_txt_IMPACC2_DR_Cust_Acc5,
        P_txt_IMPACC2_DR_Code6, P_txt_IMPACC2_DR_AC_Short_Name6, P_txt_IMPACC2_DR_Cust_abbr6, P_txt_IMPACC2_DR_Cust_Acc6,

            ////////////Import Accounting 3
        P_chk_IMPACC3Flag, P_txt_IMPACC3_FCRefNo,
        P_txt_IMPACC3_DiscAmt, P_txt_IMPACC3_DiscExchRate,
        P_txt_IMPACC3_Princ_matu, P_txt_IMPACC3_Princ_lump, P_txt_IMPACC3_Princ_Contract_no, P_txt_IMPACC3_Princ_Ex_Curr, P_txt_IMPACC3_Princ_Ex_rate, P_txt_IMPACC3_Princ_Intnl_Ex_rate,
        P_txt_IMPACC3_Interest_matu, P_txt_IMPACC3_Interest_lump, P_txt_IMPACC3_Interest_Contract_no, P_txt_IMPACC3_Interest_Ex_Curr, P_txt_IMPACC3_Interest_Ex_rate, P_txt_IMPACC3_Interest_Intnl_Ex_rate,
        P_txt_IMPACC3_Commission_matu, P_txt_IMPACC3_Commission_lump, P_txt_IMPACC3_Commission_Contract_no, P_txt_IMPACC3_Commission_Ex_Curr, P_txt_IMPACC3_Commission_Ex_rate, P_txt_IMPACC3_Commission_Intnl_Ex_rate,
        P_txt_IMPACC3_Their_Commission_matu, P_txt_IMPACC3_Their_Commission_lump, P_txt_IMPACC3_Their_Commission_Contract_no, P_txt_IMPACC3_Their_Commission_Ex_Curr, P_txt_IMPACC3_Their_Commission_Ex_rate, P_txt_IMPACC3_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC3_CR_Code, P_txt_IMPACC3_CR_AC_Short_Name, P_txt_IMPACC3_CR_Cust_abbr, P_txt_IMPACC3_CR_Cust_Acc, P_txt_IMPACC3_CR_Acceptance_Curr, P_txt_IMPACC3_CR_Acceptance_amt, P_txt_IMPACC3_CR_Acceptance_payer,
        P_txt_IMPACC3_CR_Interest_Curr, P_txt_IMPACC3_CR_Interest_amt, P_txt_IMPACC3_CR_Interest_payer,
        P_txt_IMPACC3_CR_Accept_Commission_Curr, P_txt_IMPACC3_CR_Accept_Commission_amt, P_txt_IMPACC3_CR_Accept_Commission_Payer,
        P_txt_IMPACC3_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC3_CR_Pay_Handle_Commission_amt, P_txt_IMPACC3_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC3_CR_Others_Curr, P_txt_IMPACC3_CR_Others_amt, P_txt_IMPACC3_CR_Others_Payer,
        P_txt_IMPACC3_CR_Their_Commission_Curr, P_txt_IMPACC3_CR_Their_Commission_amt, P_txt_IMPACC3_CR_Their_Commission_Payer,
        P_txt_IMPACC3_DR_Code, P_txt_IMPACC3_DR_AC_Short_Name, P_txt_IMPACC3_DR_Cust_abbr, P_txt_IMPACC3_DR_Cust_Acc,
        P_txt_IMPACC3_DR_Cur_Acc_Curr, P_txt_IMPACC3_DR_Cur_Acc_amt, P_txt_IMPACC3_DR_Cur_Acc_payer,
        P_txt_IMPACC3_DR_Cur_Acc_Curr2, P_txt_IMPACC3_DR_Cur_Acc_amt2, P_txt_IMPACC3_DR_Cur_Acc_payer2,
        P_txt_IMPACC3_DR_Cur_Acc_Curr3, P_txt_IMPACC3_DR_Cur_Acc_amt3, P_txt_IMPACC3_DR_Cur_Acc_payer3,
        P_txt_IMPACC3_DR_Cur_Acc_Curr4, P_txt_IMPACC3_DR_Cur_Acc_amt4, P_txt_IMPACC3_DR_Cur_Acc_payer4,
        P_txt_IMPACC3_DR_Cur_Acc_Curr5, P_txt_IMPACC3_DR_Cur_Acc_amt5, P_txt_IMPACC3_DR_Cur_Acc_payer5,
        P_txt_IMPACC3_DR_Cur_Acc_Curr6, P_txt_IMPACC3_DR_Cur_Acc_amt6, P_txt_IMPACC3_DR_Cur_Acc_payer6,

        P_txt_IMPACC3_DR_Code2, P_txt_IMPACC3_DR_AC_Short_Name2, P_txt_IMPACC3_DR_Cust_abbr2, P_txt_IMPACC3_DR_Cust_Acc2,
        P_txt_IMPACC3_DR_Code3, P_txt_IMPACC3_DR_AC_Short_Name3, P_txt_IMPACC3_DR_Cust_abbr3, P_txt_IMPACC3_DR_Cust_Acc3,
        P_txt_IMPACC3_DR_Code4, P_txt_IMPACC3_DR_AC_Short_Name4, P_txt_IMPACC3_DR_Cust_abbr4, P_txt_IMPACC3_DR_Cust_Acc4,
        P_txt_IMPACC3_DR_Code5, P_txt_IMPACC3_DR_AC_Short_Name5, P_txt_IMPACC3_DR_Cust_abbr5, P_txt_IMPACC3_DR_Cust_Acc5,
        P_txt_IMPACC3_DR_Code6, P_txt_IMPACC3_DR_AC_Short_Name6, P_txt_IMPACC3_DR_Cust_abbr6, P_txt_IMPACC3_DR_Cust_Acc6,

            ////////Import accounting 4
        P_chk_IMPACC4Flag, P_txt_IMPACC4_FCRefNo,
        P_txt_IMPACC4_DiscAmt, P_txt_IMPACC4_DiscExchRate,
        P_txt_IMPACC4_Princ_matu, P_txt_IMPACC4_Princ_lump, P_txt_IMPACC4_Princ_Contract_no, P_txt_IMPACC4_Princ_Ex_Curr, P_txt_IMPACC4_Princ_Ex_rate, P_txt_IMPACC4_Princ_Intnl_Ex_rate,
        P_txt_IMPACC4_Interest_matu, P_txt_IMPACC4_Interest_lump, P_txt_IMPACC4_Interest_Contract_no, P_txt_IMPACC4_Interest_Ex_Curr, P_txt_IMPACC4_Interest_Ex_rate, P_txt_IMPACC4_Interest_Intnl_Ex_rate,
        P_txt_IMPACC4_Commission_matu, P_txt_IMPACC4_Commission_lump, P_txt_IMPACC4_Commission_Contract_no, P_txt_IMPACC4_Commission_Ex_Curr, P_txt_IMPACC4_Commission_Ex_rate, P_txt_IMPACC4_Commission_Intnl_Ex_rate,
        P_txt_IMPACC4_Their_Commission_matu, P_txt_IMPACC4_Their_Commission_lump, P_txt_IMPACC4_Their_Commission_Contract_no, P_txt_IMPACC4_Their_Commission_Ex_Curr, P_txt_IMPACC4_Their_Commission_Ex_rate, P_txt_IMPACC4_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC4_CR_Code, P_txt_IMPACC4_CR_AC_Short_Name, P_txt_IMPACC4_CR_Cust_abbr, P_txt_IMPACC4_CR_Cust_Acc, P_txt_IMPACC4_CR_Acceptance_Curr, P_txt_IMPACC4_CR_Acceptance_amt, P_txt_IMPACC4_CR_Acceptance_payer,
        P_txt_IMPACC4_CR_Interest_Curr, P_txt_IMPACC4_CR_Interest_amt, P_txt_IMPACC4_CR_Interest_payer,
        P_txt_IMPACC4_CR_Accept_Commission_Curr, P_txt_IMPACC4_CR_Accept_Commission_amt, P_txt_IMPACC4_CR_Accept_Commission_Payer,
        P_txt_IMPACC4_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC4_CR_Pay_Handle_Commission_amt, P_txt_IMPACC4_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC4_CR_Others_Curr, P_txt_IMPACC4_CR_Others_amt, P_txt_IMPACC4_CR_Others_Payer,
        P_txt_IMPACC4_CR_Their_Commission_Curr, P_txt_IMPACC4_CR_Their_Commission_amt, P_txt_IMPACC4_CR_Their_Commission_Payer,
        P_txt_IMPACC4_DR_Code, P_txt_IMPACC4_DR_AC_Short_Name, P_txt_IMPACC4_DR_Cust_abbr, P_txt_IMPACC4_DR_Cust_Acc,
        P_txt_IMPACC4_DR_Cur_Acc_Curr, P_txt_IMPACC4_DR_Cur_Acc_amt, P_txt_IMPACC4_DR_Cur_Acc_payer,
        P_txt_IMPACC4_DR_Cur_Acc_Curr2, P_txt_IMPACC4_DR_Cur_Acc_amt2, P_txt_IMPACC4_DR_Cur_Acc_payer2,
        P_txt_IMPACC4_DR_Cur_Acc_Curr3, P_txt_IMPACC4_DR_Cur_Acc_amt3, P_txt_IMPACC4_DR_Cur_Acc_payer3,
        P_txt_IMPACC4_DR_Cur_Acc_Curr4, P_txt_IMPACC4_DR_Cur_Acc_amt4, P_txt_IMPACC4_DR_Cur_Acc_payer4,
        P_txt_IMPACC4_DR_Cur_Acc_Curr5, P_txt_IMPACC4_DR_Cur_Acc_amt5, P_txt_IMPACC4_DR_Cur_Acc_payer5,
        P_txt_IMPACC4_DR_Cur_Acc_Curr6, P_txt_IMPACC4_DR_Cur_Acc_amt6, P_txt_IMPACC4_DR_Cur_Acc_payer6,

        P_txt_IMPACC4_DR_Code2, P_txt_IMPACC4_DR_AC_Short_Name2, P_txt_IMPACC4_DR_Cust_abbr2, P_txt_IMPACC4_DR_Cust_Acc2,
        P_txt_IMPACC4_DR_Code3, P_txt_IMPACC4_DR_AC_Short_Name3, P_txt_IMPACC4_DR_Cust_abbr3, P_txt_IMPACC4_DR_Cust_Acc3,
        P_txt_IMPACC4_DR_Code4, P_txt_IMPACC4_DR_AC_Short_Name4, P_txt_IMPACC4_DR_Cust_abbr4, P_txt_IMPACC4_DR_Cust_Acc4,
        P_txt_IMPACC4_DR_Code5, P_txt_IMPACC4_DR_AC_Short_Name5, P_txt_IMPACC4_DR_Cust_abbr5, P_txt_IMPACC4_DR_Cust_Acc5,
        P_txt_IMPACC4_DR_Code6, P_txt_IMPACC4_DR_AC_Short_Name6, P_txt_IMPACC4_DR_Cust_abbr6, P_txt_IMPACC4_DR_Cust_Acc6,

            /////////import accounting 5
        P_chk_IMPACC5Flag, P_txt_IMPACC5_FCRefNo,
        P_txt_IMPACC5_DiscAmt, P_txt_IMPACC5_DiscExchRate,
        P_txt_IMPACC5_Princ_matu, P_txt_IMPACC5_Princ_lump, P_txt_IMPACC5_Princ_Contract_no, P_txt_IMPACC5_Princ_Ex_Curr, P_txt_IMPACC5_Princ_Ex_rate, P_txt_IMPACC5_Princ_Intnl_Ex_rate,
        P_txt_IMPACC5_Interest_matu, P_txt_IMPACC5_Interest_lump, P_txt_IMPACC5_Interest_Contract_no, P_txt_IMPACC5_Interest_Ex_Curr, P_txt_IMPACC5_Interest_Ex_rate, P_txt_IMPACC5_Interest_Intnl_Ex_rate,
        P_txt_IMPACC5_Commission_matu, P_txt_IMPACC5_Commission_lump, P_txt_IMPACC5_Commission_Contract_no, P_txt_IMPACC5_Commission_Ex_Curr, P_txt_IMPACC5_Commission_Ex_rate, P_txt_IMPACC5_Commission_Intnl_Ex_rate,
        P_txt_IMPACC5_Their_Commission_matu, P_txt_IMPACC5_Their_Commission_lump, P_txt_IMPACC5_Their_Commission_Contract_no, P_txt_IMPACC5_Their_Commission_Ex_Curr, P_txt_IMPACC5_Their_Commission_Ex_rate, P_txt_IMPACC5_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC5_CR_Code, P_txt_IMPACC5_CR_AC_Short_Name, P_txt_IMPACC5_CR_Cust_abbr, P_txt_IMPACC5_CR_Cust_Acc, P_txt_IMPACC5_CR_Acceptance_Curr, P_txt_IMPACC5_CR_Acceptance_amt, P_txt_IMPACC5_CR_Acceptance_payer,
        P_txt_IMPACC5_CR_Interest_Curr, P_txt_IMPACC5_CR_Interest_amt, P_txt_IMPACC5_CR_Interest_payer,
        P_txt_IMPACC5_CR_Accept_Commission_Curr, P_txt_IMPACC5_CR_Accept_Commission_amt, P_txt_IMPACC5_CR_Accept_Commission_Payer,
        P_txt_IMPACC5_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC5_CR_Pay_Handle_Commission_amt, P_txt_IMPACC5_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC5_CR_Others_Curr, P_txt_IMPACC5_CR_Others_amt, P_txt_IMPACC5_CR_Others_Payer,
        P_txt_IMPACC5_CR_Their_Commission_Curr, P_txt_IMPACC5_CR_Their_Commission_amt, P_txt_IMPACC5_CR_Their_Commission_Payer,
        P_txt_IMPACC5_DR_Code, P_txt_IMPACC5_DR_AC_Short_Name, P_txt_IMPACC5_DR_Cust_abbr, P_txt_IMPACC5_DR_Cust_Acc,
        P_txt_IMPACC5_DR_Cur_Acc_Curr, P_txt_IMPACC5_DR_Cur_Acc_amt, P_txt_IMPACC5_DR_Cur_Acc_payer,
        P_txt_IMPACC5_DR_Cur_Acc_Curr2, P_txt_IMPACC5_DR_Cur_Acc_amt2, P_txt_IMPACC5_DR_Cur_Acc_payer2,
        P_txt_IMPACC5_DR_Cur_Acc_Curr3, P_txt_IMPACC5_DR_Cur_Acc_amt3, P_txt_IMPACC5_DR_Cur_Acc_payer3,
        P_txt_IMPACC5_DR_Cur_Acc_Curr4, P_txt_IMPACC5_DR_Cur_Acc_amt4, P_txt_IMPACC5_DR_Cur_Acc_payer4,
        P_txt_IMPACC5_DR_Cur_Acc_Curr5, P_txt_IMPACC5_DR_Cur_Acc_amt5, P_txt_IMPACC5_DR_Cur_Acc_payer5,
        P_txt_IMPACC5_DR_Cur_Acc_Curr6, P_txt_IMPACC5_DR_Cur_Acc_amt6, P_txt_IMPACC5_DR_Cur_Acc_payer6,

        P_txt_IMPACC5_DR_Code2, P_txt_IMPACC5_DR_AC_Short_Name2, P_txt_IMPACC5_DR_Cust_abbr2, P_txt_IMPACC5_DR_Cust_Acc2,
        P_txt_IMPACC5_DR_Code3, P_txt_IMPACC5_DR_AC_Short_Name3, P_txt_IMPACC5_DR_Cust_abbr3, P_txt_IMPACC5_DR_Cust_Acc3,
        P_txt_IMPACC5_DR_Code4, P_txt_IMPACC5_DR_AC_Short_Name4, P_txt_IMPACC5_DR_Cust_abbr4, P_txt_IMPACC5_DR_Cust_Acc4,
        P_txt_IMPACC5_DR_Code5, P_txt_IMPACC5_DR_AC_Short_Name5, P_txt_IMPACC5_DR_Cust_abbr5, P_txt_IMPACC5_DR_Cust_Acc5,
        P_txt_IMPACC5_DR_Code6, P_txt_IMPACC5_DR_AC_Short_Name6, P_txt_IMPACC5_DR_Cust_abbr6, P_txt_IMPACC5_DR_Cust_Acc6,

            /////GO1
        P_chk_GO1Flag,
        P_txt_GO1_Left_Comment,
        P_txt_GO1_Left_SectionNo, P_txt_GO1_Left_Remarks, P_txt_GO1_Left_Memo,
        P_txt_GO1_Left_Scheme_no,
        P_txt_GO1_Left_Debit_Code, P_txt_GO1_Left_Debit_Curr, P_txt_GO1_Left_Debit_Amt,
        P_txt_GO1_Left_Debit_Cust, P_txt_GO1_Left_Debit_Cust_Name,
        P_txt_GO1_Left_Debit_Cust_AcCode, P_txt_GO1_Left_Debit_Cust_AcCode_Name, P_txt_GO1_Left_Debit_Cust_AccNo,
        P_txt_GO1_Left_Debit_Exch_Rate, P_txt_GO1_Left_Debit_Exch_CCY,
        P_txt_GO1_Left_Debit_FUND, P_txt_GO1_Left_Debit_Check_No, P_txt_GO1_Left_Debit_Available,
        P_txt_GO1_Left_Debit_AdPrint, P_txt_GO1_Left_Debit_Details, P_txt_GO1_Left_Debit_Entity,
        P_txt_GO1_Left_Debit_Division, P_txt_GO1_Left_Debit_Inter_Amount, P_txt_GO1_Left_Debit_Inter_Rate,
        P_txt_GO1_Left_Credit_Code, P_txt_GO1_Left_Credit_Curr, P_txt_GO1_Left_Credit_Amt,
        P_txt_GO1_Left_Credit_Cust, P_txt_GO1_Left_Credit_Cust_Name,
        P_txt_GO1_Left_Credit_Cust_AcCode, P_txt_GO1_Left_Credit_Cust_AcCode_Name, P_txt_GO1_Left_Credit_Cust_AccNo,
        P_txt_GO1_Left_Credit_Exch_Rate, P_txt_GO1_Left_Credit_Exch_CCY,
        P_txt_GO1_Left_Credit_FUND, P_txt_GO1_Left_Credit_Check_No, P_txt_GO1_Left_Credit_Available,
        P_txt_GO1_Left_Credit_AdPrint, P_txt_GO1_Left_Credit_Details, P_txt_GO1_Left_Credit_Entity,
        P_txt_GO1_Left_Credit_Division, P_txt_GO1_Left_Credit_Inter_Amount, P_txt_GO1_Left_Credit_Inter_Rate,
        P_txt_GO1_Right_Comment,
        P_txt_GO1_Right_SectionNo, P_txt_GO1_Right_Remarks, P_txt_GO1_Right_Memo,
        P_txt_GO1_Right_Scheme_no,
        P_txt_GO1_Right_Debit_Code, P_txt_GO1_Right_Debit_Curr, P_txt_GO1_Right_Debit_Amt,
        P_txt_GO1_Right_Debit_Cust, P_txt_GO1_Right_Debit_Cust_Name,
        P_txt_GO1_Right_Debit_Cust_AcCode, P_txt_GO1_Right_Debit_Cust_AcCode_Name, P_txt_GO1_Right_Debit_Cust_AccNo,
        P_txt_GO1_Right_Debit_Exch_Rate, P_txt_GO1_Right_Debit_Exch_CCY,
        P_txt_GO1_Right_Debit_FUND, P_txt_GO1_Right_Debit_Check_No, P_txt_GO1_Right_Debit_Available,
        P_txt_GO1_Right_Debit_AdPrint, P_txt_GO1_Right_Debit_Details, P_txt_GO1_Right_Debit_Entity,
        P_txt_GO1_Right_Debit_Division, P_txt_GO1_Right_Debit_Inter_Amount, P_txt_GO1_Right_Debit_Inter_Rate,
        P_txt_GO1_Right_Credit_Code, P_txt_GO1_Right_Credit_Curr, P_txt_GO1_Right_Credit_Amt,
        P_txt_GO1_Right_Credit_Cust, P_txt_GO1_Right_Credit_Cust_Name,
        P_txt_GO1_Right_Credit_Cust_AcCode, P_txt_GO1_Right_Credit_Cust_AcCode_Name, P_txt_GO1_Right_Credit_Cust_AccNo,
        P_txt_GO1_Right_Credit_Exch_Rate, P_txt_GO1_Right_Credit_Exch_CCY,
        P_txt_GO1_Right_Credit_FUND, P_txt_GO1_Right_Credit_Check_No, P_txt_GO1_Right_Credit_Available,
        P_txt_GO1_Right_Credit_AdPrint, P_txt_GO1_Right_Credit_Details, P_txt_GO1_Right_Credit_Entity,
        P_txt_GO1_Right_Credit_Division, P_txt_GO1_Right_Credit_Inter_Amount, P_txt_GO1_Right_Credit_Inter_Rate,
            /////GO2
        P_chk_GO2Flag,
        P_txt_GO2_Left_Comment,
        P_txt_GO2_Left_SectionNo, P_txt_GO2_Left_Remarks, P_txt_GO2_Left_Memo,
        P_txt_GO2_Left_Scheme_no,
        P_txt_GO2_Left_Debit_Code, P_txt_GO2_Left_Debit_Curr, P_txt_GO2_Left_Debit_Amt,
        P_txt_GO2_Left_Debit_Cust, P_txt_GO2_Left_Debit_Cust_Name,
        P_txt_GO2_Left_Debit_Cust_AcCode, P_txt_GO2_Left_Debit_Cust_AcCode_Name, P_txt_GO2_Left_Debit_Cust_AccNo,
        P_txt_GO2_Left_Debit_Exch_Rate, P_txt_GO2_Left_Debit_Exch_CCY,
        P_txt_GO2_Left_Debit_FUND, P_txt_GO2_Left_Debit_Check_No, P_txt_GO2_Left_Debit_Available,
        P_txt_GO2_Left_Debit_AdPrint, P_txt_GO2_Left_Debit_Details, P_txt_GO2_Left_Debit_Entity,
        P_txt_GO2_Left_Debit_Division, P_txt_GO2_Left_Debit_Inter_Amount, P_txt_GO2_Left_Debit_Inter_Rate,
        P_txt_GO2_Left_Credit_Code, P_txt_GO2_Left_Credit_Curr, P_txt_GO2_Left_Credit_Amt,
        P_txt_GO2_Left_Credit_Cust, P_txt_GO2_Left_Credit_Cust_Name,
        P_txt_GO2_Left_Credit_Cust_AcCode, P_txt_GO2_Left_Credit_Cust_AcCode_Name, P_txt_GO2_Left_Credit_Cust_AccNo,
        P_txt_GO2_Left_Credit_Exch_Rate, P_txt_GO2_Left_Credit_Exch_CCY,
        P_txt_GO2_Left_Credit_FUND, P_txt_GO2_Left_Credit_Check_No, P_txt_GO2_Left_Credit_Available,
        P_txt_GO2_Left_Credit_AdPrint, P_txt_GO2_Left_Credit_Details, P_txt_GO2_Left_Credit_Entity,
        P_txt_GO2_Left_Credit_Division, P_txt_GO2_Left_Credit_Inter_Amount, P_txt_GO2_Left_Credit_Inter_Rate,
        P_txt_GO2_Right_Comment,
        P_txt_GO2_Right_SectionNo, P_txt_GO2_Right_Remarks, P_txt_GO2_Right_Memo,
        P_txt_GO2_Right_Scheme_no,
        P_txt_GO2_Right_Debit_Code, P_txt_GO2_Right_Debit_Curr, P_txt_GO2_Right_Debit_Amt,
        P_txt_GO2_Right_Debit_Cust, P_txt_GO2_Right_Debit_Cust_Name,
        P_txt_GO2_Right_Debit_Cust_AcCode, P_txt_GO2_Right_Debit_Cust_AcCode_Name, P_txt_GO2_Right_Debit_Cust_AccNo,
        P_txt_GO2_Right_Debit_Exch_Rate, P_txt_GO2_Right_Debit_Exch_CCY,
        P_txt_GO2_Right_Debit_FUND, P_txt_GO2_Right_Debit_Check_No, P_txt_GO2_Right_Debit_Available,
        P_txt_GO2_Right_Debit_AdPrint, P_txt_GO2_Right_Debit_Details, P_txt_GO2_Right_Debit_Entity,
        P_txt_GO2_Right_Debit_Division, P_txt_GO2_Right_Debit_Inter_Amount, P_txt_GO2_Right_Debit_Inter_Rate,
        P_txt_GO2_Right_Credit_Code, P_txt_GO2_Right_Credit_Curr, P_txt_GO2_Right_Credit_Amt,
        P_txt_GO2_Right_Credit_Cust, P_txt_GO2_Right_Credit_Cust_Name,
        P_txt_GO2_Right_Credit_Cust_AcCode, P_txt_GO2_Right_Credit_Cust_AcCode_Name, P_txt_GO2_Right_Credit_Cust_AccNo,
        P_txt_GO2_Right_Credit_Exch_Rate, P_txt_GO2_Right_Credit_Exch_CCY,
        P_txt_GO2_Right_Credit_FUND, P_txt_GO2_Right_Credit_Check_No, P_txt_GO2_Right_Credit_Available,
        P_txt_GO2_Right_Credit_AdPrint, P_txt_GO2_Right_Credit_Details, P_txt_GO2_Right_Credit_Entity,
        P_txt_GO2_Right_Credit_Division, P_txt_GO2_Right_Credit_Inter_Amount, P_txt_GO2_Right_Credit_Inter_Rate,
            /////GO3
        P_chk_GO3Flag,
        P_txt_GO3_Left_Comment,
        P_txt_GO3_Left_SectionNo, P_txt_GO3_Left_Remarks, P_txt_GO3_Left_Memo,
        P_txt_GO3_Left_Scheme_no,
        P_txt_GO3_Left_Debit_Code, P_txt_GO3_Left_Debit_Curr, P_txt_GO3_Left_Debit_Amt,
        P_txt_GO3_Left_Debit_Cust, P_txt_GO3_Left_Debit_Cust_Name,
        P_txt_GO3_Left_Debit_Cust_AcCode, P_txt_GO3_Left_Debit_Cust_AcCode_Name, P_txt_GO3_Left_Debit_Cust_AccNo,
        P_txt_GO3_Left_Debit_Exch_Rate, P_txt_GO3_Left_Debit_Exch_CCY,
        P_txt_GO3_Left_Debit_FUND, P_txt_GO3_Left_Debit_Check_No, P_txt_GO3_Left_Debit_Available,
        P_txt_GO3_Left_Debit_AdPrint, P_txt_GO3_Left_Debit_Details, P_txt_GO3_Left_Debit_Entity,
        P_txt_GO3_Left_Debit_Division, P_txt_GO3_Left_Debit_Inter_Amount, P_txt_GO3_Left_Debit_Inter_Rate,
        P_txt_GO3_Left_Credit_Code, P_txt_GO3_Left_Credit_Curr, P_txt_GO3_Left_Credit_Amt,
        P_txt_GO3_Left_Credit_Cust, P_txt_GO3_Left_Credit_Cust_Name,
        P_txt_GO3_Left_Credit_Cust_AcCode, P_txt_GO3_Left_Credit_Cust_AcCode_Name, P_txt_GO3_Left_Credit_Cust_AccNo,
        P_txt_GO3_Left_Credit_Exch_Rate, P_txt_GO3_Left_Credit_Exch_CCY,
        P_txt_GO3_Left_Credit_FUND, P_txt_GO3_Left_Credit_Check_No, P_txt_GO3_Left_Credit_Available,
        P_txt_GO3_Left_Credit_AdPrint, P_txt_GO3_Left_Credit_Details, P_txt_GO3_Left_Credit_Entity,
        P_txt_GO3_Left_Credit_Division, P_txt_GO3_Left_Credit_Inter_Amount, P_txt_GO3_Left_Credit_Inter_Rate,
        P_txt_GO3_Right_Comment,
        P_txt_GO3_Right_SectionNo, P_txt_GO3_Right_Remarks, P_txt_GO3_Right_Memo,
        P_txt_GO3_Right_Scheme_no,
        P_txt_GO3_Right_Debit_Code, P_txt_GO3_Right_Debit_Curr, P_txt_GO3_Right_Debit_Amt,
        P_txt_GO3_Right_Debit_Cust, P_txt_GO3_Right_Debit_Cust_Name,
        P_txt_GO3_Right_Debit_Cust_AcCode, P_txt_GO3_Right_Debit_Cust_AcCode_Name, P_txt_GO3_Right_Debit_Cust_AccNo,
        P_txt_GO3_Right_Debit_Exch_Rate, P_txt_GO3_Right_Debit_Exch_CCY,
        P_txt_GO3_Right_Debit_FUND, P_txt_GO3_Right_Debit_Check_No, P_txt_GO3_Right_Debit_Available,
        P_txt_GO3_Right_Debit_AdPrint, P_txt_GO3_Right_Debit_Details, P_txt_GO3_Right_Debit_Entity,
        P_txt_GO3_Right_Debit_Division, P_txt_GO3_Right_Debit_Inter_Amount, P_txt_GO3_Right_Debit_Inter_Rate,
        P_txt_GO3_Right_Credit_Code, P_txt_GO3_Right_Credit_Curr, P_txt_GO3_Right_Credit_Amt,
        P_txt_GO3_Right_Credit_Cust, P_txt_GO3_Right_Credit_Cust_Name,
        P_txt_GO3_Right_Credit_Cust_AcCode, P_txt_GO3_Right_Credit_Cust_AcCode_Name, P_txt_GO3_Right_Credit_Cust_AccNo,
        P_txt_GO3_Right_Credit_Exch_Rate, P_txt_GO3_Right_Credit_Exch_CCY,
        P_txt_GO3_Right_Credit_FUND, P_txt_GO3_Right_Credit_Check_No, P_txt_GO3_Right_Credit_Available,
        P_txt_GO3_Right_Credit_AdPrint, P_txt_GO3_Right_Credit_Details, P_txt_GO3_Right_Credit_Entity,
        P_txt_GO3_Right_Credit_Division, P_txt_GO3_Right_Credit_Inter_Amount, P_txt_GO3_Right_Credit_Inter_Rate,
            ////GOAcccChange
        P_chk_GOAcccChangeFlag,
        P_txt_GOAccChange_Ref_No,
        P_txt_GOAccChange_Comment,
        P_txt_GOAccChange_SectionNo, P_txt_GOAccChange_Remarks, P_txt_GOAccChange_Memo,
        P_txt_GOAccChange_Scheme_no,
        P_txt_GOAccChange_Debit_Code, P_txt_GOAccChange_Debit_Curr, P_txt_GOAccChange_Debit_Amt,
        P_txt_GOAccChange_Debit_Cust, P_txt_GOAccChange_Debit_Cust_Name,
        P_txt_GOAccChange_Debit_Cust_AcCode, P_txt_GOAccChange_Debit_Cust_AcCode_Name, P_txt_GOAccChange_Debit_Cust_AccNo,
        P_txt_GOAccChange_Debit_Exch_Rate, P_txt_GOAccChange_Debit_Exch_CCY,
        P_txt_GOAccChange_Debit_FUND, P_txt_GOAccChange_Debit_Check_No, P_txt_GOAccChange_Debit_Available,
        P_txt_GOAccChange_Debit_AdPrint, P_txt_GOAccChange_Debit_Details, P_txt_GOAccChange_Debit_Entity,
        P_txt_GOAccChange_Debit_Division, P_txt_GOAccChange_Debit_Inter_Amount, P_txt_GOAccChange_Debit_Inter_Rate,
        P_txt_GOAccChange_Credit_Code, P_txt_GOAccChange_Credit_Curr, P_txt_GOAccChange_Credit_Amt,
        P_txt_GOAccChange_Credit_Cust, P_txt_GOAccChange_Credit_Cust_Name,
        P_txt_GOAccChange_Credit_Cust_AcCode, P_txt_GOAccChange_Credit_Cust_AcCode_Name, P_txt_GOAccChange_Credit_Cust_AccNo,
        P_txt_GOAccChange_Credit_Exch_Rate, P_txt_GOAccChange_Credit_Exch_CCY,
        P_txt_GOAccChange_Credit_FUND, P_txt_GOAccChange_Credit_Check_No, P_txt_GOAccChange_Credit_Available,
        P_txt_GOAccChange_Credit_AdPrint, P_txt_GOAccChange_Credit_Details, P_txt_GOAccChange_Credit_Entity,
        P_txt_GOAccChange_Credit_Division, P_txt_GOAccChange_Credit_Inter_Amount, P_txt_GOAccChange_Credit_Inter_Rate,

        // Swift File
        P_txt_MT103Receiver,
        P_txtInstructionCode, P_txtTransactionTypeCode, P_txtVDate32,
        //P_txtCurrency32, P_txtAmount32,
        P_txtCurrency, P_txtInstructedAmount, P_txtExchangeRate, P_txtSendingInstitutionAccountNumber,
        P_txtSendingInstitutionSwiftCode, P_ddlOrderingInstitution, P_txtOrderingInstitutionAccountNumber, P_txtOrderingInstitutionSwiftCode,
        P_txtOrderingInstitutionName, P_txtOrderingInstitutionAddress1, P_txtOrderingInstitutionAddress2, P_txtOrderingInstitutionAddress3,
        P_ddlSendersCorrespondent, P_txtSendersCorrespondentAccountNumber, P_txtSendersCorrespondentSwiftCode, P_txtSendersCorrespondentName,
        P_txtSendersCorrespondentLocation, P_txtSendersCorrespondentAddress1, P_txtSendersCorrespondentAddress2, P_txtSendersCorrespondentAddress3,
        P_ddlReceiversCorrespondent, P_txtReceiversCorrespondentAccountNumber, P_txtReceiversCorrespondentSwiftCode, P_txtReceiversCorrespondentName,
        P_txtReceiversCorrespondentLocation, P_txtReceiversCorrespondentAddress1, P_txtReceiversCorrespondentAddress2, P_txtReceiversCorrespondentAddress3,
        P_ddlThirdReimbursementInstitution, P_txtThirdReimbursementInstitutionAccountNumber, P_txtThirdReimbursementInstitutionSwiftCode, P_txtThirdReimbursementInstitutionName,
        P_txtThirdReimbursementInstitutionLocation, P_txtThirdReimbursementInstitutionAddress1, P_txtThirdReimbursementInstitutionAddress2, P_txtThirdReimbursementInstitutionAddress3,
        P_txtDetailsOfCharges, P_txtSenderCharges, P_txtSenderCharges2, P_txtReceiverCharges, P_txtReceiverCharges2, P_txtSendertoReceiverInformation,
        P_txtSendertoReceiverInformation2, P_txtSendertoReceiverInformation3, P_txtSendertoReceiverInformation4, P_txtSendertoReceiverInformation5,
        P_txtSendertoReceiverInformation6, P_txtRegulatoryReporting, P_txtRegulatoryReporting2, P_txtRegulatoryReporting3,

        P_txtTimeIndication, P_ddlBankOperationCode,
        P_ddlOrderingCustomer, P_txtOrderingCustomer_Acc, P_txtOrderingCustomer_SwiftCode,
        P_txtOrderingCustomer_Name, P_txtOrderingCustomer_Addr1, P_txtOrderingCustomer_Addr2, P_txtOrderingCustomer_Addr3,
        P_ddlBeneficiaryCustomer, P_txtBeneficiaryCustomerAccountNumber, P_txtBeneficiaryCustomerSwiftCode,
        P_txtBeneficiaryCustomerName, P_txtBeneficiaryCustomerAddr1, P_txtBeneficiaryCustomerAddr2, P_txtBeneficiaryCustomerAddr3,
        P_txtRemittanceInformation1, P_txtRemittanceInformation2, P_txtRemittanceInformation3, P_txtRemittanceInformation4,

        P_txt202Amount, P_ddlOrderingInstitution202, P_txtOrderingInstitution202AccountNumber, P_txtOrderingInstitution202SwiftCode, P_txtOrderingInstitution202Name,
        P_txtOrderingInstitution202Address1, P_txtOrderingInstitution202Address2, P_txtOrderingInstitution202Address3, P_ddlSendersCorrespondent202,
        P_txtSendersCorrespondent202AccountNumber, P_txtSendersCorrespondent202SwiftCode, P_txtSendersCorrespondent202Name, P_txtSendersCorrespondent202Location,
        P_txtSendersCorrespondent202Address1, P_txtSendersCorrespondent202Address2, P_txtSendersCorrespondent202Address3, P_ddlReceiversCorrespondent202,
        P_txtReceiversCorrespondent202AccountNumber, P_txtReceiversCorrespondent202SwiftCode, P_txtReceiversCorrespondent202Name, P_txtReceiversCorrespondent202Location,
        P_txtReceiversCorrespondent202Address1, P_txtReceiversCorrespondent202Address2, P_txtReceiversCorrespondent202Address3,

        P_rdb_swift_None, P_rdb_swift_103, P_rdb_swift_202,


        // MT 200
        P_rdb_swift_200, P_txt200BicCode, P_txt200TransactionRefNO, P_txt200Date, P_txt200Currency, P_txt200Amount, P_txt200SenderCorreCode, P_txt200SenderCorreLocation,
        P_ddl200Intermediary, P_txt200IntermediaryAccountNumber, P_txt200IntermediarySwiftCode, P_txt200IntermediaryName, P_txt200IntermediaryAddress1,
        P_txt200IntermediaryAddress2, P_txt200IntermediaryAddress3, P_ddl200AccWithInstitution, P_txt200AccWithInstitutionAccountNumber, P_txt200AccWithInstitutionSwiftCode,
        P_txt200AccWithInstitutionLocation, P_txt200AccWithInstitutionName, P_txt200AccWithInstitutionAddress1, P_txt200AccWithInstitutionAddress2, P_txt200AccWithInstitutionAddress3,
        P_txt200SendertoReceiverInformation1, P_txt200SendertoReceiverInformation2, P_txt200SendertoReceiverInformation3,
        P_txt200SendertoReceiverInformation4, P_txt200SendertoReceiverInformation5, P_txt200SendertoReceiverInformation6,
        P_txtTransactionRefNoR42, P_txtRelatedReferenceR42, P_txtValueDateR42, P_txtCureencyR42, P_txtAmountR42, P_txtOrderingInstitutionIFSCR42, P_txtBeneficiaryInstitutionIFSCR42,
        P_txtCodeWordR42, P_txtAdditionalInformationR42, P_txtMoreInfo1R42, P_txtMoreInfo2R42, P_txtMoreInfo3R42, P_txtMoreInfo4R42,
        P_txtMoreInfo5R42, P_rdb_swift_R42,

        //MT 103 & 202 Changes on 02122019
        P_ddlIntermediary202, P_txtIntermediary202AccountNumber, P_txtIntermediary202SwiftCode, P_txtIntermediary202Name,
        P_txtIntermediary202Address1, P_txtIntermediary202Address2, P_txtIntermediary202Address3, P_ddlAccountWithInstitution202,
        P_txtAccountWithInstitution202AccountNumber, P_txtAccountWithInstitution202SwiftCode, P_txtAccountWithInstitution202Name, P_txtAccountWithInstitution202Location,
        P_txtAccountWithInstitution202Address1, P_txtAccountWithInstitution202Address2, P_txtAccountWithInstitution202Address3, P_ddlBeneficiaryInstitution202,
        P_txtBeneficiaryInstitution202AccountNumber, P_txtBeneficiaryInstitution202SwiftCode, P_txtBeneficiaryInstitution202Name,
        P_txtBeneficiaryInstitution202Address1, P_txtBeneficiaryInstitution202Address2, P_txtBeneficiaryInstitution202Address3,

        P_ddlIntermediary103, P_txtIntermediary103AccountNumber, P_txtIntermediary103SwiftCode, P_txtIntermediary103Name,
        P_txtIntermediary103Address1, P_txtIntermediary103Address2, P_txtIntermediary103Address3, P_ddlAccountWithInstitution103,
        P_txtAccountWithInstitution103AccountNumber, P_txtAccountWithInstitution103SwiftCode, P_txtAccountWithInstitution103Name, P_txtAccountWithInstitution103Location,
        P_txtAccountWithInstitution103Address1, P_txtAccountWithInstitution103Address2, P_txtAccountWithInstitution103Address3,
        P_txtSenderToReceiverInformation2021, P_txtSenderToReceiverInformation2022, P_txtSenderToReceiverInformation2023, P_txtSenderToReceiverInformation2024, P_txtSenderToReceiverInformation2025,
        P_txtSenderToReceiverInformation2026,
        P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate,
        //----------------------------------------------------------Nilesh---------------------------------------------------------------------------------------------------
        P_MT754_Flag,
        P_txt_754_SenRef, P_txt_754_RelRef,
        P_ddlPrinAmtPaidAccNego_754, P_txtPrinAmtPaidAccNegoDate_754, P_txtPrinAmtPaidAccNegoCurr_754, P_txtPrinAmtPaidAccNegoAmt_754,
        P_txt_754_AddAmtClamd_Ccy, P_txt_754_AddAmtClamd_Amt,
        P_txt_MT754_Charges_Deducted, P_txt_MT754_Charges_Deducted2, P_txt_MT754_Charges_Deducted3, P_txt_MT754_Charges_Deducted4,
        P_txt_MT754_Charges_Deducted5, P_txt_MT754_Charges_Deducted6,
        P_txt_MT754_Charges_Added, P_txt_MT754_Charges_Added2, P_txt_MT754_Charges_Added3, P_txt_MT754_Charges_Added4,
        P_txt_MT754_Charges_Added5, P_txt_MT754_Charges_Added6,
        P_ddlTotalAmtclamd_754, P_txt_754_TotalAmtClmd_Date, P_txt_754_TotalAmtClmd_Ccy, P_txt_754_TotalAmtClmd_Amt,
        P_ddlReimbursingbank_754, P_txtReimbursingBankAccountnumber_754, P_txtReimbursingBankpartyidentifier_754,
        P_txtReimbursingBankIdentifiercode_754, P_txtReimbursingBankLocation_754, P_txtReimbursingBankName_754,
        P_txtReimbursingBankAddress1_754, P_txtReimbursingBankAddress2_754, P_txtReimbursingBankAddress3_754,
        P_ddlAccountwithbank_754, P_txtAccountwithBankAccountnumber_754, P_txtAccountwithBankpartyidentifier_754,
        P_txtAccountwithBankIdentifiercode_754, P_txtAccountwithBankLocation_754, P_txtAccountwithBankName_754,
        P_txtAccountwithBankAddress1_754, P_txtAccountwithBankAddress2_754, P_txtAccountwithBankAddress3_754,
        P_ddlBeneficiarybank_754, P_txtBeneficiaryBankAccountnumber_754, P_txtBeneficiarypartyidentifire, P_txtBeneficiaryBankIdentifiercode_754,
        P_txtBeneficiaryBankName_754, P_txtBeneficiaryBankAddress1_754, P_txtBeneficiaryBankAddress2_754, P_txtBeneficiaryBankAddress3_754,
        P_txt_MT754_Sender_to_Receiver_Information, P_txt_MT754_Sender_to_Receiver_Information2, P_txt_MT754_Sender_to_Receiver_Information3,
        P_txt_MT754_Sender_to_Receiver_Information4, P_txt_MT754_Sender_to_Receiver_Information5, P_txt_MT754_Sender_to_Receiver_Information6,

        P_txt_Narrative_754_1,
        P_txt_Narrative_754_2, P_txt_Narrative_754_3, P_txt_Narrative_754_4, P_txt_Narrative_754_5,
        P_txt_Narrative_754_6, P_txt_Narrative_754_7, P_txt_Narrative_754_8, P_txt_Narrative_754_9, P_txt_Narrative_754_10,
        P_txt_Narrative_754_11, P_txt_Narrative_754_12, P_txt_Narrative_754_13, P_txt_Narrative_754_14, P_txt_Narrative_754_15,
        P_txt_Narrative_754_16, P_txt_Narrative_754_17, P_txt_Narrative_754_18, P_txt_Narrative_754_19, P_txt_Narrative_754_20,
        P_txt_Narrative_754_21, P_txt_Narrative_754_22, P_txt_Narrative_754_23, P_txt_Narrative_754_24, P_txt_Narrative_754_25,
        P_txt_Narrative_754_26, P_txt_Narrative_754_27, P_txt_Narrative_754_28, P_txt_Narrative_754_29, P_txt_Narrative_754_30,
        P_txt_Narrative_754_31, P_txt_Narrative_754_32, P_txt_Narrative_754_33, P_txt_Narrative_754_34, P_txt_Narrative_754_35
        //----------------------------------------------------------Nilesh END---------------------------------------------------------------------------------------------------

        );
        return _Result;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_SubmitSettlementForChecker", P_DocNo);
        if (Result == "Submit")
        {
            if (lblCollection_Lodgment_UnderLC.Text.Trim() == "Settlment_Collection")
            {
                Lei_Audit_Collection();
            }
            else
            {
                Lei_Audit_LC();
            }
            string _script = "";
            _script = "window.location='TF_IMP_Settlement_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
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
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
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
            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
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
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
        }
    }
    protected void txtSenderCharges_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PCurr = new SqlParameter("@Bill_Curr", txtSenderCharges.Text.ToString());
        DataTable DM = new DataTable();
        DM = obj.getData("TF_IMP_Currency_List_Check", PCurr);
        if (DM.Rows.Count > 0)
        {
            txtSenderCharges2.Focus();
        }
        else
        {
            if (txtSenderCharges.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter valid currency');", true);
                txtSenderCharges.Text = "";
                txtSenderCharges.Focus();
            }
        }
    }
    protected void txtReceiverCharges_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PCurr = new SqlParameter("@Bill_Curr", txtReceiverCharges.Text.ToString());
        DataTable DM = new DataTable();
        DM = obj.getData("TF_IMP_Currency_List_Check", PCurr);
        if (DM.Rows.Count > 0)
        {
            txtReceiverCharges2.Focus();
        }
        else
        {
            if (txtReceiverCharges.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter valid currency');", true);
                txtReceiverCharges.Text = "";
                txtReceiverCharges.Focus();
            }
        }
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PCurr = new SqlParameter("@Bill_Curr", txtCurrency.Text.ToString());
        DataTable DM = new DataTable();
        DM = obj.getData("TF_IMP_Currency_List_Check", PCurr);
        if (DM.Rows.Count > 0)
        {
            txtInstructedAmount.Focus();
        }
        else
        {
            if (txtCurrency.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter valid currency');", true);
                txtCurrency.Text = "";
                txtCurrency.Focus();
            }
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
            txt_IMPACC1_CR_Code.Text = Settl_ForBank_AccCode.Value;
            txt_IMPACC1_CR_Cust_abbr.Text = Settl_For_Bank_Abbr.Value;
            txt_IMPACC1_CR_Cust_Acc.Text = Settl_ForBank_AccNo.Value;
            txt_IMPACC1_CR_Their_Commission_amt.Text = ACC_Their_Comm_Amount.Value;
            txt_IMPACC1_CR_Their_Commission_Curr.Text = ACC_Their_Comm_Curr.Value;
            txt_IMPACC1_DR_Cur_Acc_amt6.Text = ACC_Their_Comm_Amount.Value;
            txt_IMPACC1_DR_Cur_Acc_Curr6.Text = ACC_Their_Comm_Curr.Value;


            txt_IMPACC1_DR_Code.Text = Settlement_For_Cust_AccCode.Value;
            txt_IMPACC1_DR_AC_Short_Name.Text = "CURRENT ACCOUNT";
            txt_IMPACC1_DR_Cust_abbr.Text = Settlement_For_Cust_Abbr.Value;
            txt_IMPACC1_DR_Cust_Acc.Text = Settlement_For_Cust_AccNo.Value;

            txt_IMPACC1_DiscAmt.Text = lblBillAmt.Text;
            txt_IMPACC1_CR_Acceptance_amt.Text = lblBillAmt.Text;
            txt_IMPACC1_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            txt_IMPACC1_DR_Cur_Acc_amt.Text = lblBillAmt.Text;
            txt_IMPACC1_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
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
            txt_IMPACC2_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            txt_IMPACC2_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
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
            txt_IMPACC3_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            txt_IMPACC3_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
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
            txt_IMPACC4_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            txt_IMPACC4_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
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
            txt_IMPACC5_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            txt_IMPACC5_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
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
            txt_GO1_Left_Debit_Code.SelectedValue = "D";
            txt_GO1_Left_Debit_AdPrint.Text = "Y";

            txt_GO1_Left_Debit_Curr.Text = "INR";
            txt_GO1_Left_Debit_Cust_AcCode.Text = Settlement_For_Cust_AccCode.Value;
            txt_GO1_Left_Debit_Cust_AcCode_Name.Text = "CURRENT ACCOUNT";
            txt_GO1_Left_Debit_Cust.Text = Settlement_For_Cust_Abbr.Value;
            txt_GO1_Left_Debit_Cust_Name.Text = lbl_Collection_Customer_Name.Text;
            txt_GO1_Left_Debit_Cust_AccNo.Text = Settlement_For_Cust_AccNo.Value;
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

            txt_GOAccChange_Ref_No.Text = "TRN" + txtDocNo.Text.Substring(3, 11);

            txt_GOAccChange_SectionNo.Text = "05";
            txt_GOAccChange_Debit_Code.SelectedValue = "D";
            txt_GOAccChange_Credit_Code.SelectedValue = "C";

            txt_GOAccChange_Debit_Entity.Text = "010";
            txt_GOAccChange_Credit_Entity.Text = "010";
            txt_GOAccChange_Debit_Division.Text = "21";
            txt_GOAccChange_Credit_Division.Text = "31";
            txt_GOAccChange_Debit_Curr.Text = lblDoc_Curr.Text;
            txt_GOAccChange_Credit_Curr.Text = lblDoc_Curr.Text;

            if (lblForeign_Local.Text == "Local")
            {
                txt_GOAccChange_Remarks.Text = "MHCB I/O INLAND BILL SETT";
                txt_GOAccChange_Debit_Details.Text = "MHCB I/O INLAND BILL SETT";
                txt_GOAccChange_Credit_Details.Text = "MHCB I/O INLAND BILL SETT";
            }
            if (lblForeign_Local.Text == "Foreign")
            {
                txt_GOAccChange_Remarks.Text = "MHCB I/O IMPORT BILL SETT";
                txt_GOAccChange_Debit_Details.Text = "MHCB I/O IMPORT BILL SETT";
                txt_GOAccChange_Credit_Details.Text = "MHCB I/O IMPORT BILL SETT";
            }

            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "GO_AccChange_DR_Amt_Calculation();", true);
        }
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
    protected void CalculateR42Amount()
    {
        double amount = 0;
        if (lblScrutiny.Text == "Discrepant")
        {
            amount = Convert.ToDouble(lblBillAmt.Text) - (Convert.ToDouble(hdnDiscrepancyChrg.Value) + (Convert.ToDouble(hdnDiscrepancyChrg.Value) * Convert.ToDouble(hdnTaxChrg.Value) / 100));
        }
        else
        {
            amount = Convert.ToDouble(lblBillAmt.Text);
        }
        txtAmountR42.Text = amount.ToString();
    }
    protected void calculateSwift52aAmountField()
    {
        double amount = 0;
        TF_DATA obj = new TF_DATA();
        SqlParameter DocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].Trim());
        DataTable DM = new DataTable();
        DM = obj.getData("TF_IMP_Get_Settelment52aAmount", DocNo);
        if (DM.Rows.Count > 0)
        {
            string amt = DM.Rows[0]["Amount"].ToString();
            if (amt != "")
            {
                amount = Convert.ToDouble(amt);
                txt202Amount.Text = amount.ToString();
            }
        }

    }
    protected void txt_IMPACC1_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC1_FCRefNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchName.Value.Trim());
        SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
        dt = obj.getData("TF_IMP_FillFXDetails", P1, P2, P4);

        if (dt.Rows.Count > 0)
        {
            string result = obj.SaveDeleteData("TF_IMP_CheckFX", P1, P3);
            string _script = "";
            if (result == "Exists")
            {
                _script = "alert('This FC Ref No(" + txt_IMPACC1_FCRefNo.Text + ") already used.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC1_FCRefNo.Text = "";
                txt_IMPACC1_Princ_Ex_Curr.Text = "";
                txt_IMPACC1_Princ_Ex_rate.Text = "";
                txt_IMPACC1_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC1_FCRefNo.Focus();
                return;
            }
            else
            {
                _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC1_FCRefNo.Focus();
            }
            txt_IMPACC1_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC1_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC1_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            txt_IMPACC1_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC1_DR_Amt_Calculation();", true);
            txt_IMPACC1_FCRefNo.Focus();

        }
        else
        {
            txt_IMPACC1_FCRefNo.Text = "";
            txt_IMPACC1_Princ_Ex_Curr.Text = "";
            txt_IMPACC1_DR_Cur_Acc_Curr.Text = "";
            txt_IMPACC1_Princ_Ex_rate.Text = "";
            txt_IMPACC1_Princ_Intnl_Ex_rate.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
            txt_IMPACC1_FCRefNo.Focus();
        }
    }
    protected void txt_IMPACC2_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC2_FCRefNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchName.Value.Trim());
        SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
        dt = obj.getData("TF_IMP_FillFXDetails", P1, P2, P4);
        if (dt.Rows.Count > 0)
        {
            string result = obj.SaveDeleteData("TF_IMP_CheckFX", P1, P3);
            string _script = "";
            if (result == "Exists")
            {
                _script = "alert('This FC Ref No(" + txt_IMPACC2_FCRefNo.Text + ") already used.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC2_FCRefNo.Text = "";
                txt_IMPACC2_Princ_Ex_Curr.Text = "";
                txt_IMPACC2_Princ_Ex_rate.Text = "";
                txt_IMPACC2_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC2_FCRefNo.Focus();
                return;
            }
            else
            {
                _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC2_FCRefNo.Focus();
            }
            txt_IMPACC2_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC2_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC2_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            txt_IMPACC2_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC2_DR_Amt_Calculation();", true);
            txt_IMPACC2_FCRefNo.Focus();
        }
        else
        {
            txt_IMPACC2_FCRefNo.Text = "";
            txt_IMPACC2_Princ_Ex_Curr.Text = "";
            txt_IMPACC2_DR_Cur_Acc_Curr.Text = "";
            txt_IMPACC2_Princ_Ex_rate.Text = "";
            txt_IMPACC2_Princ_Intnl_Ex_rate.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
            txt_IMPACC2_FCRefNo.Focus();
        }
    }
    protected void txt_IMPACC3_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC3_FCRefNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchName.Value.Trim());
        SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
        dt = obj.getData("TF_IMP_FillFXDetails", P1, P2, P4);
        if (dt.Rows.Count > 0)
        {
            string result = obj.SaveDeleteData("TF_IMP_CheckFX", P1, P3);
            string _script = "";
            if (result == "Exists")
            {
                _script = "alert('This FC Ref No(" + txt_IMPACC3_FCRefNo.Text + ") already used.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC3_FCRefNo.Text = "";
                txt_IMPACC3_Princ_Ex_Curr.Text = "";
                txt_IMPACC3_Princ_Ex_rate.Text = "";
                txt_IMPACC3_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC3_FCRefNo.Focus();
                return;
            }
            else
            {
                _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC3_FCRefNo.Focus();
            }
            txt_IMPACC3_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC3_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC3_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            txt_IMPACC3_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC3_DR_Amt_Calculation();", true);
            txt_IMPACC3_FCRefNo.Focus();
        }
        else
        {
            txt_IMPACC3_FCRefNo.Text = "";
            txt_IMPACC3_Princ_Ex_Curr.Text = "";
            txt_IMPACC3_DR_Cur_Acc_Curr.Text = "";
            txt_IMPACC3_Princ_Ex_rate.Text = "";
            txt_IMPACC3_Princ_Intnl_Ex_rate.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
            txt_IMPACC3_FCRefNo.Focus();
        }
    }
    protected void txt_IMPACC4_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC4_FCRefNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchName.Value.Trim());
        SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
        dt = obj.getData("TF_IMP_FillFXDetails", P1, P2, P4);
        if (dt.Rows.Count > 0)
        {
            string result = obj.SaveDeleteData("TF_IMP_CheckFX", P1, P3);
            string _script = "";
            if (result == "Exists")
            {
                _script = "alert('This FC Ref No(" + txt_IMPACC4_FCRefNo.Text + ") already used.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC4_FCRefNo.Text = "";
                txt_IMPACC4_Princ_Ex_Curr.Text = "";
                txt_IMPACC4_Princ_Ex_rate.Text = "";
                txt_IMPACC4_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC4_FCRefNo.Focus();
                return;
            }
            else
            {
                _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC4_FCRefNo.Focus();
            }
            txt_IMPACC4_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC4_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC4_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            txt_IMPACC4_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC4_DR_Amt_Calculation();", true);
            txt_IMPACC4_FCRefNo.Focus();
        }
        else
        {
            txt_IMPACC4_FCRefNo.Text = "";
            txt_IMPACC4_Princ_Ex_Curr.Text = "";
            txt_IMPACC4_DR_Cur_Acc_Curr.Text = "";
            txt_IMPACC4_Princ_Ex_rate.Text = "";
            txt_IMPACC4_Princ_Intnl_Ex_rate.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
            txt_IMPACC4_FCRefNo.Focus();
        }
    }
    protected void txt_IMPACC5_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC5_FCRefNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchName.Value.Trim());
        SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.Trim());
        dt = obj.getData("TF_IMP_FillFXDetails", P1, P2, P4);
        if (dt.Rows.Count > 0)
        {
            string result = obj.SaveDeleteData("TF_IMP_CheckFX", P1, P3);
            string _script = "";
            if (result == "Exists")
            {
                _script = "alert('This FC Ref No(" + txt_IMPACC5_FCRefNo.Text + ") already used.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC5_FCRefNo.Text = "";
                txt_IMPACC5_Princ_Ex_Curr.Text = "";
                txt_IMPACC5_Princ_Ex_rate.Text = "";
                txt_IMPACC5_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC5_FCRefNo.Focus();
                return;
            }
            else
            {
                _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                txt_IMPACC5_FCRefNo.Focus();
            }
            txt_IMPACC5_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC5_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
            txt_IMPACC5_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            txt_IMPACC5_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC5_DR_Amt_Calculation();", true);
            txt_IMPACC5_FCRefNo.Focus();
        }
        else
        {
            txt_IMPACC5_FCRefNo.Text = "";
            txt_IMPACC5_Princ_Ex_Curr.Text = "";
            txt_IMPACC5_DR_Cur_Acc_Curr.Text = "";
            txt_IMPACC5_Princ_Ex_rate.Text = "";
            txt_IMPACC5_Princ_Intnl_Ex_rate.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
            txt_IMPACC5_FCRefNo.Focus();
        }
    }
    [WebMethod]
    public static Fields CheckFXREFno(string REFNo, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@REFNO", REFNo);
        SqlParameter p3 = new SqlParameter("@DocNo", DocNo);
        string result = obj.SaveDeleteData("TF_IMP_CheckFX", p1, p3);
        fields.FXnoStatus = result;
        return fields;
    }
    public class Fields
    {
        public string FXnoStatus { get; set; }
    }
    //---Nilesh---//
    protected void ddlPrinAmtPaidAccNego_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlPrinAmtPaidAccNego_754.SelectedValue == "A")
        {
            btncalendar_PrinAmtPaidAccNegoDate_754.Visible = true;
            txtPrinAmtPaidAccNegoDate_754.Visible = true;
            lbl_rinAmtPaidAccNegoDate_754.Visible = true;
        }
        else
        {
            btncalendar_PrinAmtPaidAccNegoDate_754.Visible = false;
            txtPrinAmtPaidAccNegoDate_754.Visible = false;
            lbl_rinAmtPaidAccNegoDate_754.Visible = false;
        }
    }
    protected void ddlTotalAmtclamd_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlTotalAmtclamd_754.SelectedValue == "A")
        {
            btncalendar_754_TotalAmtClmd_Date.Visible = true;
            txt_754_TotalAmtClmd_Date.Visible = true;
            lbl_754_TotalAmtClmd_Date.Visible = true;
        }
        else
        {
            btncalendar_754_TotalAmtClmd_Date.Visible = false;
            txt_754_TotalAmtClmd_Date.Visible = false;
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
    //-----------------------------------Nilesh END----------------------------------//

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
                        hdnDrawercountry.Value = dt1.Rows[0]["Drawer_COUNTRY"].ToString();
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
                        hdnDrawercountry.Value = dt1.Rows[0]["Drawer_COUNTRY"].ToString();
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
                            btnSubmit.Enabled = false;
                           // if (lblCollection_Lodgment_UnderLC.Text.Trim() == "Settlment_Collection")
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
                            btnSubmit.Enabled = true;
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
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
                }
                else
                {
                    lblLEI_Remark.Text = "...Verified.";
                    hdnleino.Value = dt.Rows[0]["LEI_No"].ToString();
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                }
            }
            else
            {
                lblLEI_Remark.Text = "...Not Verified.";
                lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleino.Value = "";
                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
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
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark.Text = "...Not Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "Y";
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                lblLEIExpiry_Remark.Text = "...Not Verified.";
                lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleiexpiry.Value = "";
                hdnleiexpiry1.Value = "";
                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
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
                    if(hdnDrawercountry.Value == "IN")
                    {
                        btnSubmit.Enabled = false;
                        btnSubmit.Visible = false;
                    }
                }
                else
                {
                    lblLEI_Remark_Drawee.Text = "...Verified.";
                    hdnDraweeleino.Value = dt.Rows[0]["Drawer_LEI_NO"].ToString();
                    if (lblLEI_Remark.Text.Trim() == "...Verified." && lblLEIExpiry_Remark.Text.Trim() == "...Verified.")
                    {
                        btnSubmit.Enabled = true;
                        btnSubmit.Visible = true;
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
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
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
                        btnSubmit.Enabled = true;
                        btnSubmit.Visible = true;
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
                        btnSubmit.Enabled = false;
                        btnSubmit.Visible = false;
                    }
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleiexpiry.Value = "";
                hdnDraweeleiexpiry1.Value = "";
                if (hdnDrawercountry.Value == "IN")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
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
                btnSubmit.Enabled = false;
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
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
                }
                else
                {
                    lblLEI_Remark.Text = "...Verified.";
                    hdnleino.Value = dt.Rows[0]["LEI_No"].ToString();
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                }
            }
            else
            {
                lblLEI_Remark.Text = "...Not Verified.";
                lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleino.Value = "";
                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
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
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark.Text = "...Not Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "Y";
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                lblLEIExpiry_Remark.Text = "...Not Verified.";
                lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleiexpiry.Value = "";
                hdnleiexpiry1.Value = "";
                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
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
                        btnSubmit.Enabled = false;
                        btnSubmit.Visible = false;
                    }
                }
                else
                {
                    lblLEI_Remark_Drawee.Text = "...Verified.";
                    hdnDraweeleino.Value = dt.Rows[0]["Drawer_LEI_NO"].ToString();
                    if (lblLEI_Remark.Text.Trim() == "...Verified." && lblLEIExpiry_Remark.Text.Trim() == "...Verified.")
                    {
                        btnSubmit.Enabled = true;
                        btnSubmit.Visible = true;
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
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
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
                        btnSubmit.Enabled = true;
                        btnSubmit.Visible = true;
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
                        btnSubmit.Enabled = false;
                        btnSubmit.Visible = false;
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
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;
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
                btnSubmit.Enabled = false;
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
                SqlParameter P_Status = new SqlParameter("@status", "C");
                SqlParameter P_Module = new SqlParameter("@Module", "S");
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
                //SqlParameter P_DueDate = new SqlParameter("@DueDate", txt_UnderLC_Interest_To.Text.Trim());
                SqlParameter P_DueDate = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim());
                SqlParameter P_LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value.Trim());
                SqlParameter P_CustLEI_Flag = new SqlParameter("@Cust_LEIFlag", hdncustleiflag.Value.Trim());
                SqlParameter P_Status = new SqlParameter("@status", "C");
                SqlParameter P_Module = new SqlParameter("@Module", "S");
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
                        hdncustleiflag.Value = "R";
                        btnSubmit.Enabled = false;
                        btn_Verify_UnderLC.Visible = true;
                        btn_Verify_Collection.Visible = false;
                        ReccuringLEI.Visible = true;
                        ReccuringLEI.Text = "This is Recurring LEI Customer.";
                        ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                        ReccuringLEI.Font.Size = 10;
                    }
                    else
                    {
                        hdncustleiflag.Value = "R";
                        btnSubmit.Enabled = false;
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
    //------------------------------------End------------------------------------------//

}