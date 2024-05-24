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

public partial class IMP_Transactions_TF_IMP_Settlement_Maker : System.Web.UI.Page
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
                TabContainerMain.ActiveTab = tbDocumentDetails;
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

                    txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txt200Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txt_Doc_Value_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    if (hdnMode.Value == "Edit")
                    {
                        Fill_Logd_Details();
                        Fill_Settlement_Details();
                    }
                    else
                    {
                        //txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        //txt200Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        //txt_Doc_Value_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        Fill_Logd_Details();
                    }
                }
            }
        }
    }
    protected void Toggel_DocType(string DocType, string Branch, string FLC_ILC, string Scrutiny)
    {
        if (Branch != "Mumbai")
        {
            Branch = "Branch";
        }
        switch (DocType)
        {
            case "ACC":
                lblCollection_Lodgment_UnderLC.Text = "Settlment_UnderLC";
                lblSight_Usance.Text = "Usance";
                txt_Doc_Settlement_For_Bank.Text = "09";

                //// GO_Bill_Handling
                txt_Bill_Handling_SectionNo.Text = "05";
                txt_Bill_Handling_Remarks.Text = "BILL HANDLING CHARGES";
                txt_Bill_Handling_Debit_Code.Text = "D";
                txt_Bill_Handling_Debit_Cust_AcCode.Text = "20000";
                txt_Bill_Handling_Debit_Cust_AcCode_Name.Text = "CURRENT ACCOUNT";
                txt_Bill_Handling_Debit_AdPrint.Text = "Y";
                txt_Bill_Handling_Debit_Details.Text = "BILL HANDLING CHARGES";
                txt_Bill_Handling_Debit_Entity.Text = "010";
                txt_Bill_Handling_Debit_Division.Text = "11";
                txt_Bill_Handling_Credit_Code.Text = "C";
                txt_Bill_Handling_Credit_Cust_AcCode.Text = "67905";
                txt_Bill_Handling_Credit_Cust_AcCode_Name.Text = "COM RCVD IMPORT";
                txt_Bill_Handling_Credit_Details.Text = "BILL HANDLING CHARGES";
                txt_Bill_Handling_Credit_Entity.Text = "010";
                txt_Bill_Handling_Credit_Division.Text = "11";
                switch (FLC_ILC)
                {
                    case "ILC":  //local
                        lblForeign_Local.Text = "Local";
                        txt_Doc_Settlement_for_Cust.Text = "23";
                        switch (Scrutiny)
                        {
                            case "1":  //clean
                                tbDocumentGO_Sundry.Visible = false;
                                tbDocumentGO_Comm_Recieved.Visible = false;
                                switch (Branch)
                                {
                                    case "Mumbai":// Mumbai
                                        tbDocumentGo_Acc_Change.Visible = false;
                                        txt_CR_Code.Text = "01991";
                                        txt_CR_AC_Short_Name.Text = "SUNDRY DEPOSITS";
                                        txt_CR_Cust_abbr.Text = "RBI";
                                        txt_CR_Cust_Acc.Text = "H30000028";
                                        break;
                                    case "Branch":
                                        txt_CR_Code.Text = "81763";
                                        txt_CR_AC_Short_Name.Text = "I/O ACT WITH MB";
                                        txt_CR_Cust_abbr.Text = "MBCB MB";
                                        txt_CR_Cust_Acc.Text = "B79111111";
                                        ////GO_Acc_Change
                                        txt_Acc_Change_SectionNo.Text = "05";
                                        txt_Acc_Change_Remarks.Text = "MHCB I/O INLAND BILL SETT";
                                        txt_Acc_Change_Debit_Code.Text = "D";
                                        txt_Acc_Change_Debit_Curr.Text = "INR";
                                        txt_Acc_Change_Debit_Cust_AcCode.Text = "81323";
                                        txt_Acc_Change_Debit_Cust_AcCode_Name.Text = "I/O ACNT CHN";
                                        txt_Acc_Change_Debit_Cust_AccNo.Text = "B79792111111";
                                        txt_Acc_Change_Debit_Details.Text = "MHCB I/O INLAND BILL SETT";
                                        txt_Acc_Change_Debit_Entity.Text = "010";
                                        txt_Acc_Change_Debit_Division.Text = "21";
                                        txt_Acc_Change_Credit_Code.Text = "C";
                                        txt_Acc_Change_Credit_Curr.Text = "INR";
                                        txt_Acc_Change_Credit_Cust.Text = "RBI";
                                        txt_Acc_Change_Credit_Cust_AcCode.Text = "01991";
                                        txt_Acc_Change_Credit_Cust_AcCode_Name.Text = "RSV DEPO TO RBI";
                                        txt_Acc_Change_Credit_Cust_AccNo.Text = "H60792000028";
                                        txt_Acc_Change_Credit_Details.Text = "MHCB I/O INLAND BILL SETT";
                                        txt_Acc_Change_Credit_Entity.Text = "010";
                                        txt_Acc_Change_Credit_Division.Text = "31";

                                        break;
                                }

                                break;
                            case "2": //discrepancy
                                txt_CR_Code.Text = "22002";
                                txt_CR_AC_Short_Name.Text = "SUNDRY DEPOSITS";
                                txt_CR_Cust_abbr.Text = "900";
                                txt_CR_Cust_Acc.Text = "H30000056";
                                ////GO_Sundry_deposits
                                txt_Sundry_SectionNo.Text = "05";
                                txt_Sundry_Remarks.Text = "RBI SETTLE AND DISC CHGS";
                                txt_Sundry_Debit_Code.Text = "D";
                                txt_Sundry_Debit_Cust.Text = "900";
                                txt_Sundry_Debit_Cust_AcCode.Text = "22002";
                                txt_Sundry_Debit_Cust_AcCode_Name.Text = "SUNDRY DEPOSITS";
                                txt_Sundry_Debit_Details.Text = "RBI SETTLE AND DISC CHGS";
                                txt_Sundry_Debit_Entity.Text = "010";
                                txt_Sundry_Debit_Division.Text = "31";
                                txt_Sundry_Credit_Code.Text = "C";
                                txt_Sundry_Credit_Details.Text = "RBI SETTLE";
                                txt_Sundry_Credit_Entity.Text = "010";
                                ////GO_Comm_Recieved
                                txt_Comm_Recieved_Debit_Code.Text = "C";
                                txt_Comm_Recieved_Debit_Cust.Text = "600MISC";
                                txt_Comm_Recieved_Debit_Cust_Name.Text = "MISCELLANEOUS CUSTOMER";
                                txt_Comm_Recieved_Debit_Cust_AcCode.Text = "67905";
                                txt_Comm_Recieved_Debit_Cust_AcCode_Name.Text = "COM RCVD IMPORT";
                                txt_Comm_Recieved_Debit_Details.Text = "DISC CHGS";
                                txt_Comm_Recieved_Debit_Entity.Text = "010";
                                txt_Comm_Recieved_Debit_Division.Text = "11";
                                switch (Branch)
                                {
                                    case "Mumbai":// mumbai
                                        tbDocumentGo_Acc_Change.Visible = false;
                                        txt_Sundry_Debit_Cust_Name.Text = "MHCB MUM";
                                        txt_Sundry_Debit_Cust_AccNo.Text = "H30792000056";
                                        txt_Sundry_Credit_Cust.Text = "RBI";
                                        txt_Sundry_Credit_Cust_AcCode.Text = "01991";
                                        txt_Sundry_Credit_Cust_AcCode_Name.Text = "RSV DEPO TO RBI";
                                        txt_Sundry_Credit_Cust_AccNo.Text = "H60792000028";
                                        txt_Sundry_Credit_Division.Text = "31";
                                        break;
                                    case "Branch":
                                        txt_Sundry_Debit_Cust_Name.Text = "SUNDRY DEPOSITS";
                                        txt_Sundry_Credit_Cust.Text = "MHCB MB";
                                        txt_Sundry_Credit_Cust_AcCode.Text = "81763";
                                        txt_Sundry_Credit_Cust_AcCode_Name.Text = "I/O ACT WITH MB";
                                        txt_Sundry_Credit_Division.Text = "21";
                                        ////GO_Acc_Change
                                        txt_Acc_Change_SectionNo.Text = "05";
                                        txt_Acc_Change_Remarks.Text = "MHCB I/O INLAND BILL SETT";
                                        txt_Acc_Change_Debit_Code.Text = "D";
                                        txt_Acc_Change_Debit_Curr.Text = "INR";
                                        txt_Acc_Change_Debit_Cust_AcCode.Text = "81323";
                                        txt_Acc_Change_Debit_Cust_AcCode_Name.Text = "I/O ACNT CHN";
                                        txt_Acc_Change_Debit_Cust_AccNo.Text = "B79792111111";
                                        txt_Acc_Change_Debit_Details.Text = "MHCB I/O INLAND BILL SETT";
                                        txt_Acc_Change_Debit_Entity.Text = "010";
                                        txt_Acc_Change_Debit_Division.Text = "21";
                                        txt_Acc_Change_Credit_Code.Text = "C";
                                        txt_Acc_Change_Credit_Curr.Text = "INR";
                                        txt_Acc_Change_Credit_Cust.Text = "RBI";
                                        txt_Acc_Change_Credit_Cust_AcCode.Text = "01991";
                                        txt_Acc_Change_Credit_Cust_AccNo.Text = "H60792000028";
                                        txt_Acc_Change_Credit_Cust_AcCode_Name.Text = "RSV DEPO TO RBI";
                                        txt_Acc_Change_Credit_Details.Text = "MHCB I/O INLAND BILL SETT";
                                        txt_Acc_Change_Credit_Entity.Text = "010";
                                        txt_Acc_Change_Credit_Division.Text = "31";
                                        break;
                                }
                                break;
                        }
                        break;
                    case "FLC": //Foreign
                        lblForeign_Local.Text = "Foreign";
                        txt_Doc_Settlement_for_Cust.Text = "29";
                        tbDocumentGO_Comm_Recieved.Visible = false;
                        switch (Scrutiny)
                        {
                            case "1":  //clean
                                tbDocumentGO_Sundry.Visible = false;
                                switch (Branch)
                                {
                                    case "Mumbai":// mumbai
                                        tbDocumentGo_Acc_Change.Visible = false;
                                        txt_CR_Code.Text = "86006";
                                        txt_CR_AC_Short_Name.Text = "FC IO(ORDINARY)";
                                        txt_CR_Cust_abbr.Text = "MHCB HO";
                                        txt_CR_Cust_Acc.Text = "F79111111";
                                        break;
                                    case "Branch":
                                        txt_CR_Code.Text = "81763";
                                        txt_CR_AC_Short_Name.Text = "I/O ACT WITH MB";
                                        txt_CR_Cust_abbr.Text = "MHCB MB";
                                        txt_CR_Cust_Acc.Text = "B79111111";
                                        ////GO_Acc_Change
                                        txt_Acc_Change_SectionNo.Text = "05";
                                        txt_Acc_Change_Remarks.Text = "MHCB I/O IMPORT BILL SETT";
                                        txt_Acc_Change_Debit_Code.Text = "D";
                                        txt_Acc_Change_Debit_Cust_AcCode.Text = "81323";
                                        txt_Acc_Change_Debit_Cust_AcCode_Name.Text = "I/O ACNT CHN";
                                        txt_Acc_Change_Debit_Cust_AccNo.Text = "B79792111111";
                                        txt_Acc_Change_Debit_Details.Text = "MHCB I/O IMPORT BILL SETT";
                                        txt_Acc_Change_Debit_Entity.Text = "010";
                                        txt_Acc_Change_Debit_Division.Text = "21";
                                        txt_Acc_Change_Credit_Code.Text = "C";
                                        txt_Acc_Change_Credit_Details.Text = "MHCB I/O IMPORT BILL SETT";
                                        txt_Acc_Change_Credit_Entity.Text = "010";
                                        txt_Acc_Change_Credit_Division.Text = "31";
                                        break;
                                }
                                break;
                            case "2":  //discrepancy

                                ////GO_Sundry_deposits
                                txt_Sundry_SectionNo.Text = "05";
                                txt_Sundry_Remarks.Text = "DISCREPANCY CHARGES";
                                txt_Sundry_Debit_Code.Text = "D";
                                txt_Sundry_Debit_Cust.Text = "MHCB MB";
                                txt_Sundry_Debit_Cust_Name.Text = "MIZUHO BANK, LTD.MUMBAI BRANCH";
                                txt_Sundry_Debit_Cust_AcCode.Text = "81763";
                                txt_Sundry_Debit_Cust_AcCode_Name.Text = "I/O ACT WITH MB";
                                txt_Sundry_Debit_Details.Text = "DISCREPANCY CHARGES";
                                txt_Sundry_Debit_Entity.Text = "010";
                                txt_Sundry_Debit_Division.Text = "21";
                                txt_Sundry_Credit_Code.Text = "C";
                                txt_Sundry_Credit_Details.Text = "DISCREPANCY CHARGES";
                                txt_Sundry_Credit_Entity.Text = "010";
                                switch (Branch)
                                {
                                    case "Mumbai":// mumbai
                                        tbDocumentGo_Acc_Change.Visible = false;
                                        txt_CR_Code.Text = "86006";
                                        txt_CR_AC_Short_Name.Text = "FC IO(ORDINARY)";
                                        txt_CR_Cust_abbr.Text = "MHCB HO";
                                        txt_CR_Cust_Acc.Text = "F79111111";
                                        break;
                                    case "Branch":
                                        txt_CR_Code.Text = "81763";
                                        txt_CR_AC_Short_Name.Text = "I/O ACT WITH MB";
                                        txt_CR_Cust_abbr.Text = "MHCB MB";
                                        txt_CR_Cust_Acc.Text = "B79111111";
                                        ////GO_Acc_Change
                                        txt_Acc_Change_SectionNo.Text = "05";
                                        txt_Acc_Change_Remarks.Text = "MHCB I/O IMPORT BILL SETT";
                                        txt_Acc_Change_Debit_Code.Text = "D";
                                        txt_Acc_Change_Debit_Cust_AcCode.Text = "81323";
                                        txt_Acc_Change_Debit_Cust_AcCode_Name.Text = "I/O ACNT CHN";
                                        txt_Acc_Change_Debit_Cust_AccNo.Text = "B79792111111";
                                        txt_Acc_Change_Debit_Details.Text = "MHCB I/O IMPORT BILL SETT";
                                        txt_Acc_Change_Debit_Entity.Text = "010";
                                        txt_Acc_Change_Debit_Division.Text = "21";
                                        txt_Acc_Change_Credit_Code.Text = "C";
                                        txt_Acc_Change_Credit_Details.Text = "MHCB I/O IMPORT BILL SETT";
                                        txt_Acc_Change_Credit_Entity.Text = "010";
                                        txt_Acc_Change_Credit_Division.Text = "31";
                                        break;
                                }
                                break;
                        }
                        break;
                }
                break;
        }
    }
    protected void Fill_Logd_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_BOE_Acceptance_Detalis_For_Settlement", PDocNo);
        if (dt.Rows.Count > 0)
        {
            //////////////// SETTLEMENT(IBD,ACC)//////////////////////////
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt_Doc_Customer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
            txt_Doc_Maturity.Text = dt.Rows[0]["Maturity_Date"].ToString();
            txt_Doc_Interest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();

            txt_Doc_Interest_To.Text = dt.Rows[0]["Maturity_Date"].ToString();
            txt_Doc_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();

            ///////////////////    IMport Accounting     /////////////////////

            txt_DiscAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt_CR_Interest_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Accept_Commission_Curr.Text = "INR";

            txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();

            txt_CR_Others_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();

            txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();

            txt_DR_Code.Text = dt.Rows[0]["AC_Code"].ToString();
            txt_DR_AC_Short_Name.Text = "CURRENT ACCOUNT";
            txt_DR_Cust_abbr.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["CustAcNo"].ToString();
            txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            txt_Bill_Handling_Debit_Curr.Text = "INR";
            txt_Bill_Handling_Debit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            txt_Bill_Handling_Debit_Cust_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txt_Bill_Handling_Debit_Cust_AcCode.Text = dt.Rows[0]["AC_Code"].ToString();
            txt_Bill_Handling_Debit_Cust_AccNo.Text = dt.Rows[0]["CustAcNo"].ToString();

            txt_Bill_Handling_Credit_Curr.Text = "INR";
            txt_Bill_Handling_Credit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            txt_Bill_Handling_Credit_Cust_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();

            txt_Sundry_Debit_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_Sundry_Debit_Amt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            txt_Sundry_Credit_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_Sundry_Credit_Amt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            txt_Comm_Recieved_Debit_Curr.Text = "INR";

            txtTransactionRefNoR42.Text = ""+dt.Rows[0]["Document_No"].ToString();
            txtRelatedReferenceR42.Text = dt.Rows[0]["Their_Ref_No"].ToString();
            txtValueDateR42.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtCureencyR42.Text = "INR";
            txtBeneficiaryInstitutionIFSCR42.Text = dt.Rows[0]["IFSC_Code"].ToString();
            txtCodeWordR42.Text = "TRF";
            txtAdditionalInformationR42.Text ="";
            txtMoreInfo1R42.Text = "//"+dt.Rows[0]["Drawer_NAME"].ToString();
            txtMoreInfo2R42.Text = "//YOUR REF NO:" + dt.Rows[0]["Their_Ref_No"].ToString();
            txtMoreInfo3R42.Text = "//LC REF NO:" + dt.Rows[0]["Doc_LC_No"].ToString();
            txtMoreInfo4R42.Text = "//" + dt.Rows[0]["CUST_NAME"].ToString();
            txtMoreInfo5R42.Text = "//BILL REF NO:" + dt.Rows[0]["Document_No"].ToString();
        }

    }
    protected void Fill_Settlement_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_Settlement_Detalis", PDocNo);
        if (dt.Rows.Count > 0)
        {

            //////////////// SETTLEMENT(IBD,ACC)//////////////////////////
            //txt_Doc_Maturity.Text = dt.Rows[0]["Maturity_Date"].ToString();

            //txtValueDate.Text = dt.Rows[0]["Settlement_Date"].ToString();
            //txt_Doc_Value_Date.Text = dt.Rows[0]["Settlement_Date"].ToString();
            txt_Doc_Interest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();

            txt_Doc_Comment_Code.Text = dt.Rows[0]["Comment_Code"].ToString();
            txt_Doc_Settlement_for_Cust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
            txt_Doc_Settlement_For_Bank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();
            txt_Doc_Discount.Text = dt.Rows[0]["Discount_Flag"].ToString();
            txt_Doc_Interest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            txt_Doc_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();
            txt_Doc_Rate.Text = dt.Rows[0]["Interest_Rate"].ToString();
            txt_Doc_Amount.Text = dt.Rows[0]["Interest_Amount"].ToString();
            txt_Doc_Overdue_Interestrate.Text = dt.Rows[0]["Overdue_Interest_Rate"].ToString();
            txt_Doc_Oveduenoofdays.Text = dt.Rows[0]["Overdue_No_Of_Days"].ToString();
            txt_Doc_Overdueamount.Text = dt.Rows[0]["Overdue_Interest_Amount"].ToString();
            txt_Doc_Attn.Text = dt.Rows[0]["Attn"].ToString();

            ///////////////////    IMport Accounting     ///////////////////////////////////////////////////////////////////

            //txt_DiscAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
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

            txt_CR_Code.Text = dt.Rows[0]["CR_Sundry_Deposit_Code"].ToString();
            txt_CR_AC_Short_Name.Text = dt.Rows[0]["CR_Sundry_Deposit_Short_Name"].ToString();
            txt_CR_Cust_abbr.Text = dt.Rows[0]["CR_Sundry_Deposit_Cust_Abbr"].ToString();
            txt_CR_Cust_Acc.Text = dt.Rows[0]["CR_Sundry_Deposit_Cust_Acc_No"].ToString();
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Sundry_Deposit_Curr"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["CR_Sundry_Deposit_Amt"].ToString();
            txt_CR_Acceptance_payer.Text = dt.Rows[0]["CR_Sundry_Deposit_Payer"].ToString();
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
            txt_DR_Code.Text = dt.Rows[0]["DR_Code"].ToString();
            txt_DR_AC_Short_Name.Text = dt.Rows[0]["DR_Acc_Short_Name"].ToString();
            txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_Abbr"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc_No"].ToString();
            txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["DR_Current_Acc_Amount"].ToString();
            txt_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_Current_Acc_Payer"].ToString();
            txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["DR_Current_Acc_Curr2"].ToString();
            txt_DR_Cur_Acc_amt2.Text = dt.Rows[0]["DR_Current_Acc_Amount2"].ToString();
            txt_DR_Cur_Acc_payer2.Text = dt.Rows[0]["DR_Current_Acc_Payer2"].ToString();
            txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["DR_Current_Acc_Curr3"].ToString();
            txt_DR_Cur_Acc_amt3.Text = dt.Rows[0]["DR_Current_Acc_Amount3"].ToString();
            txt_DR_Cur_Acc_payer3.Text = dt.Rows[0]["DR_Current_Acc_Payer3"].ToString();

            ///////////////////// GENERAL OPRATOIN For Bill Handling /////////////////////////////////////////////////////////////

            if (dt.Rows[0]["GO_Bill_Handling_Flag"].ToString() == "Y")
            {
                cb_GO_Bill_Handling_Flag.Checked = true;
                cb_GO_Bill_Handling_Flag_OnCheckedChanged(null, null);

                txt_Bill_Handling_Comment.Text = dt.Rows[0]["GO_Bill_Handling_Comment"].ToString();
                txt_Bill_Handling_SectionNo.Text = dt.Rows[0]["GO_Bill_Handling_Section"].ToString();
                txt_Bill_Handling_Remarks.Text = dt.Rows[0]["GO_Bill_Handling_Remark"].ToString();
                txt_Bill_Handling_Memo.Text = dt.Rows[0]["GO_Bill_Handling_Memo"].ToString();
                txt_Bill_Handling_Scheme_no.Text = dt.Rows[0]["GO_Bill_Handling_SchemeNo"].ToString();
                txt_Bill_Handling_Debit_Code.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Code"].ToString();
                txt_Bill_Handling_Debit_Curr.Text = dt.Rows[0]["GO_Bill_Handling_Debit_CCY"].ToString();
                txt_Bill_Handling_Debit_Amt.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Amt"].ToString();
                txt_Bill_Handling_Debit_Cust.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_abbr"].ToString();
                txt_Bill_Handling_Debit_Cust_Name.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_Name"].ToString();
                txt_Bill_Handling_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_Bill_Handling_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccCode_Disc"].ToString();
                txt_Bill_Handling_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Cust_AccNo"].ToString();
                txt_Bill_Handling_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Debit_ExchRate"].ToString();
                txt_Bill_Handling_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Bill_Handling_Debit_ExchCCY"].ToString();
                txt_Bill_Handling_Debit_FUND.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Fund"].ToString();
                txt_Bill_Handling_Debit_Check_No.Text = dt.Rows[0]["GO_Bill_Handling_Debit_CheckNo"].ToString();
                txt_Bill_Handling_Debit_Available.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Available"].ToString();
                txt_Bill_Handling_Debit_AdPrint.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Advice_Print"].ToString();
                txt_Bill_Handling_Debit_Details.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Details"].ToString();
                txt_Bill_Handling_Debit_Entity.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Entity"].ToString();
                txt_Bill_Handling_Debit_Division.Text = dt.Rows[0]["GO_Bill_Handling_Debit_Division"].ToString();
                txt_Bill_Handling_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Bill_Handling_Debit_InterAmt"].ToString();
                txt_Bill_Handling_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Debit_InterRate"].ToString();
                txt_Bill_Handling_Credit_Code.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Code"].ToString();
                txt_Bill_Handling_Credit_Curr.Text = dt.Rows[0]["GO_Bill_Handling_Credit_CCY"].ToString();

                txt_Bill_Handling_Credit_Amt.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Amt"].ToString();
                txt_Bill_Handling_Credit_Cust.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_abbr"].ToString();
                txt_Bill_Handling_Credit_Cust_Name.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_Name"].ToString();
                txt_Bill_Handling_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_AccCode"].ToString();
                txt_Bill_Handling_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_AccCode_Disc"].ToString();
                txt_Bill_Handling_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Cust_AccNo"].ToString();
                txt_Bill_Handling_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Credit_ExchRate"].ToString();
                txt_Bill_Handling_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Bill_Handling_Credit_ExchCCY"].ToString();
                txt_Bill_Handling_Credit_FUND.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Fund"].ToString();
                txt_Bill_Handling_Credit_Check_No.Text = dt.Rows[0]["GO_Bill_Handling_Credit_CheckNo"].ToString();
                txt_Bill_Handling_Credit_Available.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Available"].ToString();
                txt_Bill_Handling_Credit_AdPrint.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Advice_Print"].ToString();
                txt_Bill_Handling_Credit_Details.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Details"].ToString();
                txt_Bill_Handling_Credit_Entity.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Entity"].ToString();
                txt_Bill_Handling_Credit_Division.Text = dt.Rows[0]["GO_Bill_Handling_Credit_Division"].ToString();
                txt_Bill_Handling_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Bill_Handling_Credit_InterAmt"].ToString();
                txt_Bill_Handling_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Bill_Handling_Credit_InterRate"].ToString();

            }

            ///////////////////// GENERAL OPRATOIN For Sundry Deposits /////////////////////////////////////////////////////////////

            if (dt.Rows[0]["GO_Sundry_Deposits_Flag"].ToString() == "Y")
            {
                cb_GO_Sundry_Flag.Checked = true;
                cb_GO_Sundry_Flag_OnCheckedChanged(null, null);

                txt_Sundry_Comment.Text = dt.Rows[0]["GO_Sundry_Deposits_Comment"].ToString();
                txt_Sundry_SectionNo.Text = dt.Rows[0]["GO_Sundry_Deposits_Section"].ToString();
                txt_Sundry_Remarks.Text = dt.Rows[0]["GO_Sundry_Deposits_Remark"].ToString();
                txt_Sundry_Memo.Text = dt.Rows[0]["GO_Sundry_Deposits_Memo"].ToString();
                txt_Sundry_Scheme_no.Text = dt.Rows[0]["GO_Sundry_Deposits_SchemeNo"].ToString();
                txt_Sundry_Debit_Code.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Code"].ToString();
                txt_Sundry_Debit_Curr.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_CCY"].ToString();
                txt_Sundry_Debit_Amt.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Amt"].ToString();
                txt_Sundry_Debit_Cust.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Cust_abbr"].ToString();
                txt_Sundry_Debit_Cust_Name.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Cust_Name"].ToString();
                txt_Sundry_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Cust_AccCode"].ToString();
                txt_Sundry_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Cust_AccCode_Disc"].ToString();
                txt_Sundry_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Cust_AccNo"].ToString();
                txt_Sundry_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_ExchRate"].ToString();
                txt_Sundry_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_ExchCCY"].ToString();
                txt_Sundry_Debit_FUND.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Fund"].ToString();
                txt_Sundry_Debit_Check_No.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_CheckNo"].ToString();
                txt_Sundry_Debit_Available.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Available"].ToString();
                txt_Sundry_Debit_AdPrint.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Advice_Print"].ToString();
                txt_Sundry_Debit_Details.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Details"].ToString();
                txt_Sundry_Debit_Entity.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Entity"].ToString();
                txt_Sundry_Debit_Division.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_Division"].ToString();
                txt_Sundry_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_InterAmt"].ToString();
                txt_Sundry_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Sundry_Deposits_Debit_InterRate"].ToString();
                txt_Sundry_Credit_Code.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Code"].ToString();
                txt_Sundry_Credit_Curr.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_CCY"].ToString();
                txt_Sundry_Credit_Amt.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Amt"].ToString();
                txt_Sundry_Credit_Cust.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Cust_abbr"].ToString();
                txt_Sundry_Credit_Cust_Name.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Cust_Name"].ToString();
                txt_Sundry_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Cust_AccCode"].ToString();
                txt_Sundry_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Cust_AccCode_Disc"].ToString();
                txt_Sundry_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Cust_AccNo"].ToString();
                txt_Sundry_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_ExchRate"].ToString();
                txt_Sundry_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_ExchCCY"].ToString();
                txt_Sundry_Credit_FUND.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Fund"].ToString();
                txt_Sundry_Credit_Check_No.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_CheckNo"].ToString();
                txt_Sundry_Credit_Available.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Available"].ToString();
                txt_Sundry_Credit_AdPrint.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Advice_Print"].ToString();
                txt_Sundry_Credit_Details.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Details"].ToString();
                txt_Sundry_Credit_Entity.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Entity"].ToString();
                txt_Sundry_Credit_Division.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_Division"].ToString();
                txt_Sundry_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_InterAmt"].ToString();
                txt_Sundry_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Sundry_Deposits_Credit_InterRate"].ToString();
            }
            ///////////////////// GENERAL OPRATOIN For Comm_Recieved /////////////////////////////////////////////////////////////
            if (dt.Rows[0]["GO_Comm_Received_Flag"].ToString() == "Y")
            {
                cb_GO_Comm_Recieved_Flag.Checked = true;
                cb_GO_Comm_Recieved_Flag_OnCheckedChanged(null, null);

                txt_Comm_Recieved_Comment.Text = dt.Rows[0]["GO_Comm_Received_Comment"].ToString();
                txt_Comm_Recieved_SectionNo.Text = dt.Rows[0]["GO_Comm_Received_Section"].ToString();
                txt_Comm_Recieved_Remarks.Text = dt.Rows[0]["GO_Comm_Received_Remark"].ToString();
                txt_Comm_Recieved_Memo.Text = dt.Rows[0]["GO_Comm_Received_Memo"].ToString();
                txt_Comm_Recieved_Scheme_no.Text = dt.Rows[0]["GO_Comm_Received_SchemeNo"].ToString();
                txt_Comm_Recieved_Debit_Code.Text = dt.Rows[0]["GO_Comm_Received_Debit_Code"].ToString();
                txt_Comm_Recieved_Debit_Curr.Text = dt.Rows[0]["GO_Comm_Received_Debit_CCY"].ToString();
                txt_Comm_Recieved_Debit_Amt.Text = dt.Rows[0]["GO_Comm_Received_Debit_Amt"].ToString();
                txt_Comm_Recieved_Debit_Cust.Text = dt.Rows[0]["GO_Comm_Received_Debit_Cust_abbr"].ToString();
                txt_Comm_Recieved_Debit_Cust_Name.Text = dt.Rows[0]["GO_Comm_Received_Debit_Cust_Name"].ToString();
                txt_Comm_Recieved_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Comm_Received_Debit_Cust_AccCode"].ToString();
                txt_Comm_Recieved_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Comm_Received_Debit_Cust_AccCode_Disc"].ToString();
                txt_Comm_Recieved_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Comm_Received_Debit_Cust_AccNo"].ToString();
                txt_Comm_Recieved_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Comm_Received_Debit_ExchRate"].ToString();
                txt_Comm_Recieved_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Comm_Received_Debit_ExchCCY"].ToString();
                txt_Comm_Recieved_Debit_FUND.Text = dt.Rows[0]["GO_Comm_Received_Debit_Fund"].ToString();
                txt_Comm_Recieved_Debit_Check_No.Text = dt.Rows[0]["GO_Comm_Received_Debit_CheckNo"].ToString();
                txt_Comm_Recieved_Debit_Available.Text = dt.Rows[0]["GO_Comm_Received_Debit_Available"].ToString();
                txt_Comm_Recieved_Debit_AdPrint.Text = dt.Rows[0]["GO_Comm_Received_Debit_Advice_Print"].ToString();
                txt_Comm_Recieved_Debit_Details.Text = dt.Rows[0]["GO_Comm_Received_Debit_Details"].ToString();
                txt_Comm_Recieved_Debit_Entity.Text = dt.Rows[0]["GO_Comm_Received_Debit_Entity"].ToString();
                txt_Comm_Recieved_Debit_Division.Text = dt.Rows[0]["GO_Comm_Received_Debit_Division"].ToString();
                txt_Comm_Recieved_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Comm_Received_Debit_InterAmt"].ToString();
                txt_Comm_Recieved_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Comm_Received_Debit_InterRate"].ToString();
                txt_Comm_Recieved_Credit_Code.Text = dt.Rows[0]["GO_Comm_Received_Credit_Code"].ToString();
                txt_Comm_Recieved_Credit_Curr.Text = dt.Rows[0]["GO_Comm_Received_Credit_CCY"].ToString();
                txt_Comm_Recieved_Credit_Amt.Text = dt.Rows[0]["GO_Comm_Received_Credit_Amt"].ToString();
                txt_Comm_Recieved_Credit_Cust.Text = dt.Rows[0]["GO_Comm_Received_Credit_Cust_abbr"].ToString();
                txt_Comm_Recieved_Credit_Cust_Name.Text = dt.Rows[0]["GO_Comm_Received_Credit_Cust_Name"].ToString();
                txt_Comm_Recieved_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Comm_Received_Credit_Cust_AccCode"].ToString();
                txt_Comm_Recieved_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Comm_Received_Credit_Cust_AccCode_Disc"].ToString();
                txt_Comm_Recieved_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Comm_Received_Credit_Cust_AccNo"].ToString();
                txt_Comm_Recieved_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Comm_Received_Credit_ExchRate"].ToString();
                txt_Comm_Recieved_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Comm_Received_Credit_ExchCCY"].ToString();
                txt_Comm_Recieved_Credit_FUND.Text = dt.Rows[0]["GO_Comm_Received_Credit_Fund"].ToString();
                txt_Comm_Recieved_Credit_Check_No.Text = dt.Rows[0]["GO_Comm_Received_Credit_CheckNo"].ToString();
                txt_Comm_Recieved_Credit_Available.Text = dt.Rows[0]["GO_Comm_Received_Credit_Available"].ToString();
                txt_Comm_Recieved_Credit_AdPrint.Text = dt.Rows[0]["GO_Comm_Received_Credit_Advice_Print"].ToString();
                txt_Comm_Recieved_Credit_Details.Text = dt.Rows[0]["GO_Comm_Received_Credit_Details"].ToString();
                txt_Comm_Recieved_Credit_Entity.Text = dt.Rows[0]["GO_Comm_Received_Credit_Entity"].ToString();
                txt_Comm_Recieved_Credit_Division.Text = dt.Rows[0]["GO_Comm_Received_Credit_Division"].ToString();
                txt_Comm_Recieved_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Comm_Received_Credit_InterAmt"].ToString();
                txt_Comm_Recieved_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Comm_Received_Credit_InterRate"].ToString();
            }
            ///////////////////// GENERAL OPRATOIN For Change branch /////////////////////////////////////////////////////////////

            if (dt.Rows[0]["GO_Acc_Change_Flag"].ToString() == "Y")
            {
                cb_GO_Acc_Change_Flag.Checked = true;
                cb_GO_Acc_Change_Flag_OnCheckedChanged(null, null);

                txt_Acc_Change_Comment.Text = dt.Rows[0]["GO_Acc_Change_Comment"].ToString();
                txt_Acc_Change_SectionNo.Text = dt.Rows[0]["GO_Acc_Change_Section"].ToString();
                txt_Acc_Change_Remarks.Text = dt.Rows[0]["GO_Acc_Change_Remark"].ToString();
                txt_Acc_Change_Memo.Text = dt.Rows[0]["GO_Acc_Change_Memo"].ToString();
                txt_Acc_Change_Scheme_no.Text = dt.Rows[0]["GO_Acc_Change_SchemeNo"].ToString();
                txt_Acc_Change_Debit_Code.Text = dt.Rows[0]["GO_Acc_Change_Debit_Code"].ToString();
                txt_Acc_Change_Debit_Curr.Text = dt.Rows[0]["GO_Acc_Change_Debit_CCY"].ToString();
                txt_Acc_Change_Debit_Amt.Text = dt.Rows[0]["GO_Acc_Change_Debit_Amt"].ToString();
                txt_Acc_Change_Debit_Cust.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_abbr"].ToString();
                txt_Acc_Change_Debit_Cust_Name.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_Name"].ToString();
                txt_Acc_Change_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccCode"].ToString();
                txt_Acc_Change_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccCode_Disc"].ToString();
                txt_Acc_Change_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccNo"].ToString();
                txt_Acc_Change_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Acc_Change_Debit_ExchRate"].ToString();
                txt_Acc_Change_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Acc_Change_Debit_ExchCCY"].ToString();
                txt_Acc_Change_Debit_FUND.Text = dt.Rows[0]["GO_Acc_Change_Debit_Fund"].ToString();
                txt_Acc_Change_Debit_Check_No.Text = dt.Rows[0]["GO_Acc_Change_Debit_CheckNo"].ToString();
                txt_Acc_Change_Debit_Available.Text = dt.Rows[0]["GO_Acc_Change_Debit_Available"].ToString();
                txt_Acc_Change_Debit_AdPrint.Text = dt.Rows[0]["GO_Acc_Change_Debit_Advice_Print"].ToString();
                txt_Acc_Change_Debit_Details.Text = dt.Rows[0]["GO_Acc_Change_Debit_Details"].ToString();
                txt_Acc_Change_Debit_Entity.Text = dt.Rows[0]["GO_Acc_Change_Debit_Entity"].ToString();
                txt_Acc_Change_Debit_Division.Text = dt.Rows[0]["GO_Acc_Change_Debit_Division"].ToString();
                txt_Acc_Change_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Acc_Change_Debit_InterAmt"].ToString();
                txt_Acc_Change_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Acc_Change_Debit_InterRate"].ToString();
                txt_Acc_Change_Credit_Code.Text = dt.Rows[0]["GO_Acc_Change_Credit_Code"].ToString();
                txt_Acc_Change_Credit_Curr.Text = dt.Rows[0]["GO_Acc_Change_Credit_CCY"].ToString();
                txt_Acc_Change_Credit_Amt.Text = dt.Rows[0]["GO_Acc_Change_Credit_Amt"].ToString();
                txt_Acc_Change_Credit_Cust.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_abbr"].ToString();
                txt_Acc_Change_Credit_Cust_Name.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_Name"].ToString();
                txt_Acc_Change_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccCode"].ToString();
                txt_Acc_Change_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccCode_Disc"].ToString();
                txt_Acc_Change_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccNo"].ToString();
                txt_Acc_Change_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Acc_Change_Credit_ExchRate"].ToString();
                txt_Acc_Change_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Acc_Change_Credit_ExchCCY"].ToString();
                txt_Acc_Change_Credit_FUND.Text = dt.Rows[0]["GO_Acc_Change_Credit_Fund"].ToString();
                txt_Acc_Change_Credit_Check_No.Text = dt.Rows[0]["GO_Acc_Change_Credit_CheckNo"].ToString();
                txt_Acc_Change_Credit_Available.Text = dt.Rows[0]["GO_Acc_Change_Credit_Available"].ToString();
                txt_Acc_Change_Credit_AdPrint.Text = dt.Rows[0]["GO_Acc_Change_Credit_Advice_Print"].ToString();
                txt_Acc_Change_Credit_Details.Text = dt.Rows[0]["GO_Acc_Change_Credit_Details"].ToString();
                txt_Acc_Change_Credit_Entity.Text = dt.Rows[0]["GO_Acc_Change_Credit_Entity"].ToString();
                txt_Acc_Change_Credit_Division.Text = dt.Rows[0]["GO_Acc_Change_Credit_Division"].ToString();
                txt_Acc_Change_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Acc_Change_Credit_InterAmt"].ToString();
                txt_Acc_Change_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Acc_Change_Credit_InterRate"].ToString();
            }

            //MT 200            
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

            if (dt.Rows[0]["swift_200"].ToString() == "Y")
            {
                rdb_swift_None.Checked = false;
                rdb_swift_200.Checked = true;
                rdb_swift_R42.Checked = false;
            }
            if (dt.Rows[0]["swift_R42"].ToString() == "Y")
            {
                rdb_swift_R42.Checked = true;
                rdb_swift_None.Checked = false;
                rdb_swift_200.Checked = false;
            }

            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
        }

    }
    protected void Get_Acceptance_Get_Date_Diff(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PInterest_From = new SqlParameter("@Interest_From", txt_Doc_Interest_From.Text.ToString());
        SqlParameter PInterest_To = new SqlParameter("@Interest_To", txt_Doc_Interest_To.Text.ToString());
        DataTable Date_dt = new DataTable();
        Date_dt = obj.getData("TF_IMP_Acceptance_Get_Date_Diff", PInterest_From, PInterest_To);
        if (Date_dt.Rows.Count > 0)
        {
            txt_Doc_No_Of_Days.Text = Date_dt.Rows[0]["NoOfDays"].ToString();
            txt_Doc_Rate.Focus();
        }
    }
    
    [WebMethod]
    public static string AddUpdateSettlement(string hdnUserName, string _BranchName, string _txtDocNo, string _DocType, string _FLC_ILC, string _Doc_Scrutiny, string _Document_Curr,

        //////////////// Document Detalis//////////////////////////
        string _txt_Doc_Customer_ID, string _txtValueDate, string _txt_Doc_Comment_Code, string _txt_Doc_Maturity,
        string _txt_Doc_Settlement_for_Cust, string _txt_Doc_Settlement_For_Bank,
        string _txt_Doc_Interest_From, string _txt_Doc_Discount, string _txt_Doc_Interest_To, string _txt_Doc_No_Of_Days,
        string _txt_Doc_Rate, string _txt_Doc_Amount, string _txt_Doc_Overdue_Interestrate, string _txt_Doc_Oveduenoofdays, string _txt_Doc_Overdueamount,
        string txt_Doc_Attn,

        //////////////// IMPORT ACCOUNTING //////////////////////////
        string _txt_DiscAmt, string _txtPrinc_matu, string _txtPrinc_lump, string _txtprinc_Contract_no, string _txt_Princ_Ex_Curr, string _txtprinc_Ex_rate,
        string _txtprinc_Intnl_Ex_rate, string _txtInterest_matu, string _txtInterest_lump, string _txtInterest_Contract_no, string _txt_interest_Ex_Curr,
        string _txtInterest_Ex_rate, string _txtInterest_Intnl_Ex_rate, string _txtCommission_matu, string _txtCommission_lump, string _txtCommission_Contract_no,
        string _txt_Commission_Ex_Curr, string _txtCommission_Ex_rate, string _txtCommission_Intnl_Ex_rate, string _txtTheir_Commission_matu, string _txtTheir_Commission_lump,
        string _txtTheir_Commission_Contract_no, string _txt_Their_Commission_Ex_Curr, string _txtTheir_Commission_Ex_rate, string _txtTheir_Commission_Intnl_Ex_rate,
        string _txt_CR_Code, string _txt_CR_AC_Short_Name, string _txt_CR_Cust_abbr, string _txt_CR_Cust_Acc, string _txt_CR_Acceptance_Curr, string _txt_CR_Acceptance_amt, string _txt_CR_Acceptance_payer,
        string _txt_CR_Interest_Curr, string _txt_CR_Interest_amt, string _txt_CR_Interest_payer, string _txt_CR_Accept_Commission_Curr, string _txt_CR_Accept_Commission_amt,
        string _txt_CR_Accept_Commission_Payer, string _txt_CR_Pay_Handle_Commission_Curr, string _txt_CR_Pay_Handle_Commission_amt, string _txt_CR_Pay_Handle_Commission_Payer,
        string _txt_CR_Others_Curr, string _txt_CR_Others_amt, string _txt_CR_Others_Payer, string _txt_CR_Their_Commission_Curr, string _txt_CR_Their_Commission_amt,
        string _txt_CR_Their_Commission_Payer, string _txt_DR_Code, string _txt_DR_AC_Short_Name, string _txt_DR_Cust_abbr, string _txt_DR_Cust_Acc, string _txt_DR_Cur_Acc_Curr, string _txt_DR_Cur_Acc_amt,
        string _txt_DR_Cur_Acc_payer, string _txt_DR_Cur_Acc_Curr2, string _txt_DR_Cur_Acc_amt2, string _txt_DR_Cur_Acc_payer2, string _txt_DR_Cur_Acc_Curr3,
         string _txt_DR_Cur_Acc_amt3, string _txt_DR_Cur_Acc_payer3,

        ///////////////////// GENERAL OPRATOIN For Bill_Handling /////////////////////////////////////////////////////////////
        string _GO_Bill_Handling_Flag, string _txt_Bill_Handling_Comment, string _txt_Bill_Handling_SectionNo, string _txt_Bill_Handling_Remarks, string _txt_Bill_Handling_Memo,
        string _txt_Bill_Handling_Scheme_no, string _txt_Bill_Handling_Debit_Code, string _txt_Bill_Handling_Debit_Curr, string _txt_Bill_Handling_Debit_Amt, string _txt_Bill_Handling_Debit_Cust, string _txt_Bill_Handling_Debit_Cust_Name, string _txt_Bill_Handling_Debit_Cust_AcCode,
        string _txt_Bill_Handling_Debit_Cust_AccNo, string _txt_Bill_Handling_Debit_Cust_AcCode_Name, string _txt_Bill_Handling_Debit_Exch_Rate, string _txt_Bill_Handling_Debit_Exch_CCY, string _txt_Bill_Handling_Debit_FUND, string _txt_Bill_Handling_Debit_Check_No,
        string _txt_Bill_Handling_Debit_Available, string _txt_Bill_Handling_Debit_AdPrint, string _txt_Bill_Handling_Debit_Details, string _txt_Bill_Handling_Debit_Entity, string _txt_Bill_Handling_Debit_Division,
        string _txt_Bill_Handling_Debit_Inter_Amount, string _txt_Bill_Handling_Debit_Inter_Rate, string _txt_Bill_Handling_Credit_Code, string _txt_Bill_Handling_Credit_Curr, string _txt_Bill_Handling_Credit_Amt,
        string _txt_Bill_Handling_Credit_Cust, string _txt_Bill_Handling_Credit_Cust_Name, string _txt_Bill_Handling_Credit_Cust_AcCode, string _txt_Bill_Handling_Credit_Cust_AcCode_Name, string _txt_Bill_Handling_Credit_Cust_AccNo, string _txt_Bill_Handling_Credit_Exch_Rate, string _txt_Bill_Handling_Credit_Exch_Curr,
        string _txt_Bill_Handling_Credit_FUND, string _txt_Bill_Handling_Credit_Check_No, string _txt_Bill_Handling_Credit_Available, string _txt_Bill_Handling_Credit_AdPrint, string _txt_Bill_Handling_Credit_Details,
        string _txt_Bill_Handling_Credit_Entity, string _txt_Bill_Handling_Credit_Division, string _txt_Bill_Handling_Credit_Inter_Amount, string _txt_Bill_Handling_Credit_Inter_Rate,

        ///////////////////// GENERAL OPRATOIN For Sundry Deposits /////////////////////////////////////////////////////////////
        string _GO_Sundry_Flag, string _txt_Sundry_Comment, string _txt_Sundry_SectionNo, string _txt_Sundry_Remarks, string _txt_Sundry_Memo,
        string _txt_Sundry_Scheme_no, string _txt_Sundry_Debit_Code, string _txt_Sundry_Debit_Curr, string _txt_Sundry_Debit_Amt, string _txt_Sundry_Debit_Cust, string _txt_Sundry_Debit_Cust_Name, string _txt_Sundry_Debit_Cust_AcCode,
        string _txt_Sundry_Debit_Cust_AccNo, string _txt_Sundry_Debit_Cust_AcCode_Name, string _txt_Sundry_Debit_Exch_Rate, string _txt_Sundry_Debit_Exch_CCY, string _txt_Sundry_Debit_FUND, string _txt_Sundry_Debit_Check_No,
        string _txt_Sundry_Debit_Available, string _txt_Sundry_Debit_AdPrint, string _txt_Sundry_Debit_Details, string _txt_Sundry_Debit_Entity, string _txt_Sundry_Debit_Division,
        string _txt_Sundry_Debit_Inter_Amount, string _txt_Sundry_Debit_Inter_Rate, string _txt_Sundry_Credit_Code, string _txt_Sundry_Credit_Curr, string _txt_Sundry_Credit_Amt,
        string _txt_Sundry_Credit_Cust, string _txt_Sundry_Credit_Cust_Name, string _txt_Sundry_Credit_Cust_AcCode, string _txt_Sundry_Credit_Cust_AcCode_Name, string _txt_Sundry_Credit_Cust_AccNo, string _txt_Sundry_Credit_Exch_Rate, string _txt_Sundry_Credit_Exch_Curr,
        string _txt_Sundry_Credit_FUND, string _txt_Sundry_Credit_Check_No, string _txt_Sundry_Credit_Available, string _txt_Sundry_Credit_AdPrint, string _txt_Sundry_Credit_Details,
        string _txt_Sundry_Credit_Entity, string _txt_Sundry_Credit_Division, string _txt_Sundry_Credit_Inter_Amount, string _txt_Sundry_Credit_Inter_Rate,

        ///////////////////// GENERAL OPRATOIN For Comm_Recieved /////////////////////////////////////////////////////////////
        string _GO_Comm_Recieved_Flag, string _txt_Comm_Recieved_Comment, string _txt_Comm_Recieved_SectionNo, string _txt_Comm_Recieved_Remarks, string _txt_Comm_Recieved_Memo,
        string _txt_Comm_Recieved_Scheme_no, string _txt_Comm_Recieved_Debit_Code, string _txt_Comm_Recieved_Debit_Curr, string _txt_Comm_Recieved_Debit_Amt, string _txt_Comm_Recieved_Debit_Cust, string _txt_Comm_Recieved_Debit_Cust_Name, string _txt_Comm_Recieved_Debit_Cust_AcCode,
        string _txt_Comm_Recieved_Debit_Cust_AccNo, string _txt_Comm_Recieved_Debit_Cust_AcCode_Name, string _txt_Comm_Recieved_Debit_Exch_Rate, string _txt_Comm_Recieved_Debit_Exch_CCY, string _txt_Comm_Recieved_Debit_FUND, string _txt_Comm_Recieved_Debit_Check_No,
        string _txt_Comm_Recieved_Debit_Available, string _txt_Comm_Recieved_Debit_AdPrint, string _txt_Comm_Recieved_Debit_Details, string _txt_Comm_Recieved_Debit_Entity, string _txt_Comm_Recieved_Debit_Division,
        string _txt_Comm_Recieved_Debit_Inter_Amount, string _txt_Comm_Recieved_Debit_Inter_Rate, string _txt_Comm_Recieved_Credit_Code, string _txt_Comm_Recieved_Credit_Curr, string _txt_Comm_Recieved_Credit_Amt,
        string _txt_Comm_Recieved_Credit_Cust, string _txt_Comm_Recieved_Credit_Cust_Name, string _txt_Comm_Recieved_Credit_Cust_AcCode, string _txt_Comm_Recieved_Credit_Cust_AcCode_Name, string _txt_Comm_Recieved_Credit_Cust_AccNo, string _txt_Comm_Recieved_Credit_Exch_Rate, string _txt_Comm_Recieved_Credit_Exch_Curr,
        string _txt_Comm_Recieved_Credit_FUND, string _txt_Comm_Recieved_Credit_Check_No, string _txt_Comm_Recieved_Credit_Available, string _txt_Comm_Recieved_Credit_AdPrint, string _txt_Comm_Recieved_Credit_Details,
        string _txt_Comm_Recieved_Credit_Entity, string _txt_Comm_Recieved_Credit_Division, string _txt_Comm_Recieved_Credit_Inter_Amount, string _txt_Comm_Recieved_Credit_Inter_Rate,

        ///////////////////// GENERAL OPRATOIN For Change Branch /////////////////////////////////////////////////////////////
        string _GO_Acc_Change_Flag, string _txt_Acc_Change_Comment, string _txt_Acc_Change_SectionNo, string _txt_Acc_Change_Remarks, string _txt_Acc_Change_Memo,
        string _txt_Acc_Change_Scheme_no, string _txt_Acc_Change_Debit_Code, string _txt_Acc_Change_Debit_Curr, string _txt_Acc_Change_Debit_Amt, string _txt_Acc_Change_Debit_Cust, string _txt_Acc_Change_Debit_Cust_Name, string _txt_Acc_Change_Debit_Cust_AcCode,
        string _txt_Acc_Change_Debit_Cust_AccNo, string _txt_Acc_Change_Debit_Cust_AcCode_Name, string _txt_Acc_Change_Debit_Exch_Rate, string _txt_Acc_Change_Debit_Exch_CCY, string _txt_Acc_Change_Debit_FUND, string _txt_Acc_Change_Debit_Check_No,
        string _txt_Acc_Change_Debit_Available, string _txt_Acc_Change_Debit_AdPrint, string _txt_Acc_Change_Debit_Details, string _txt_Acc_Change_Debit_Entity, string _txt_Acc_Change_Debit_Division,
        string _txt_Acc_Change_Debit_Inter_Amount, string _txt_Acc_Change_Debit_Inter_Rate, string _txt_Acc_Change_Credit_Code, string _txt_Acc_Change_Credit_Curr, string _txt_Acc_Change_Credit_Amt,
        string _txt_Acc_Change_Credit_Cust, string _txt_Acc_Change_Credit_Cust_Name, string _txt_Acc_Change_Credit_Cust_AcCode, string _txt_Acc_Change_Credit_Cust_AcCode_Name, string _txt_Acc_Change_Credit_Cust_AccNo, string _txt_Acc_Change_Credit_Exch_Rate, string _txt_Acc_Change_Credit_Exch_Curr,
        string _txt_Acc_Change_Credit_FUND, string _txt_Acc_Change_Credit_Check_No, string _txt_Acc_Change_Credit_Available, string _txt_Acc_Change_Credit_AdPrint, string _txt_Acc_Change_Credit_Details,
        string _txt_Acc_Change_Credit_Entity, string _txt_Acc_Change_Credit_Division, string _txt_Acc_Change_Credit_Inter_Amount, string _txt_Acc_Change_Credit_Inter_Rate
        , string _rdb_swift_None, string _rdb_swift_200,
        string _txt200TransactionRefNO, string _txt200Date, string _txt200Currency, string _txt200Amount, string _txt200SenderCorreCode, string _txt200SenderCorreLocation,
        string _ddl200Intermediary, string _txt200IntermediaryAccountNumber, string _txt200IntermediarySwiftCode, string _txt200IntermediaryName,
string _txt200IntermediaryAddress1, string _txt200IntermediaryAddress2, string _txt200IntermediaryAddress3, string _ddl200AccWithInstitution,
string _txt200AccWithInstitutionAccountNumber, string _txt200AccWithInstitutionSwiftCode, string _txt200AccWithInstitutionLocation, string _txt200AccWithInstitutionName,
string _txt200AccWithInstitutionAddress1, string _txt200AccWithInstitutionAddress2, string _txt200AccWithInstitutionAddress3,
string _txt200SendertoReceiverInformation1, string _txt200SendertoReceiverInformation2, string _txt200SendertoReceiverInformation3, string _txt200SendertoReceiverInformation4,
string _txt200SendertoReceiverInformation5, string _txt200SendertoReceiverInformation6,
string _txtTransactionRefNoR42, string _txtRelatedReferenceR42, string _txtValueDateR42, string _txtCureencyR42, string _txtAmountR42, string _txtOrderingInstitutionIFSCR42,
string _txtBeneficiaryInstitutionIFSCR42, string _txtCodeWordR42, string _txtAdditionalInformationR42, string _txtMoreInfo1R42,
string _txtMoreInfo2R42, string _txtMoreInfo3R42, string _txtMoreInfo4R42, string _txtMoreInfo5R42, string _rdb_swift_R42
       )
    {
        TF_DATA obj = new TF_DATA();


        ////document deatails
        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName.ToUpper());
        SqlParameter P_Document_No = new SqlParameter("@Document_No", _txtDocNo.ToUpper());
        SqlParameter P_Settlement_Date = new SqlParameter("@Settlement_Date", _txtValueDate.ToUpper());
        SqlParameter P_Comment_Code = new SqlParameter("@Comment_Code", _txt_Doc_Comment_Code.ToUpper());
        //SqlParameter P_txt_Doc_Maturity = new SqlParameter("", _txt_Doc_Maturity.ToUpper());
        SqlParameter P_txt_Doc_Settlement_for_Cust = new SqlParameter("@Settlement_For_Cust_Code", _txt_Doc_Settlement_for_Cust.ToUpper());
        SqlParameter P_txt_Doc_Settlement_For_Bank = new SqlParameter("@Settlement_For_Bank_Code", _txt_Doc_Settlement_For_Bank.ToUpper());
        SqlParameter P_txt_Doc_Interest_From = new SqlParameter("@Interest_From_Date", _txt_Doc_Interest_From.ToUpper());
        SqlParameter P_txt_Doc_Discount = new SqlParameter("@Discount_Flag", _txt_Doc_Discount.ToUpper());
        SqlParameter P_txt_Doc_Interest_To = new SqlParameter("@Interest_To_Date", _txt_Doc_Interest_To.ToUpper());
        SqlParameter P_txt_Doc_No_Of_Days = new SqlParameter("@No_Of_Days", _txt_Doc_No_Of_Days.ToUpper());
        SqlParameter P_txt_Doc_Rate = new SqlParameter("@Interest_Rate", _txt_Doc_Rate.ToUpper());
        SqlParameter P_txt_Doc_Amount = new SqlParameter("@Interest_Amount", _txt_Doc_Amount.ToUpper());
        SqlParameter P_txt_Doc_Overdue_Interestrate = new SqlParameter("@Overdue_Interest_Rate", _txt_Doc_Overdue_Interestrate.ToUpper());
        SqlParameter P_txt_Doc_Oveduenoofdays = new SqlParameter("@Overdue_No_Of_Days", _txt_Doc_Oveduenoofdays.ToUpper());
        SqlParameter P_txt_Doc_Overdueamount = new SqlParameter("@Overdue_Interest_Amount", _txt_Doc_Overdueamount.ToUpper());
        SqlParameter P_txt_Doc_Attn = new SqlParameter("@Attn", txt_Doc_Attn.ToUpper());

        //////IMPORT ACCOUNTING 
        SqlParameter P_IMP_ACC_ExchRate = new SqlParameter("@IMP_ACC_ExchRate", "");
        SqlParameter P_txtPrinc_matu = new SqlParameter("@Principal_MATU", _txtPrinc_matu.ToUpper());
        SqlParameter P_txtPrinc_lump = new SqlParameter("@Principal_LUMP", _txtPrinc_lump.ToUpper());
        SqlParameter P_txtprinc_Contract_no = new SqlParameter("@Principal_Contract_No", _txtprinc_Contract_no.ToUpper());
        SqlParameter P_txt_Princ_Ex_Curr = new SqlParameter("@Principal_Ex_Curr", _txt_Princ_Ex_Curr.ToUpper());
        SqlParameter P_txtprinc_Ex_rate = new SqlParameter("@Principal_Exch_Rate", _txtprinc_Ex_rate.ToUpper());
        SqlParameter P_txtprinc_Intnl_Ex_rate = new SqlParameter("@Principal_Intnl_Exch_Rate", _txtprinc_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txtInterest_matu = new SqlParameter("@Interest_MATU", _txtInterest_matu.ToUpper());
        SqlParameter P_txtInterest_lump = new SqlParameter("@Interest_LUMP", _txtInterest_lump.ToUpper());
        SqlParameter P_txtInterest_Contract_no = new SqlParameter("@Interest_Contract_No", _txtInterest_Contract_no.ToUpper());
        SqlParameter P_txt_interest_Ex_Curr = new SqlParameter("@Interest_Ex_Curr", _txt_interest_Ex_Curr.ToUpper());
        SqlParameter P_txtInterest_Ex_rate = new SqlParameter("@Interest_Exch_Rate", _txtInterest_Ex_rate.ToUpper());
        SqlParameter P_txtInterest_Intnl_Ex_rate = new SqlParameter("@Interest_Intnl_Exch_Rate", _txtInterest_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txtCommission_matu = new SqlParameter("@Commission_MATU", _txtCommission_matu.ToUpper());
        SqlParameter P_txtCommission_lump = new SqlParameter("@Commission_LUMP", _txtCommission_lump.ToUpper());
        SqlParameter P_txtCommission_Contract_no = new SqlParameter("@Commission_Contract_No", _txtCommission_Contract_no.ToUpper());
        SqlParameter P_txt_Commission_Ex_Curr = new SqlParameter("@Commission_Ex_Curr", _txt_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txtCommission_Ex_rate = new SqlParameter("@Commission_Exch_Rate", _txtCommission_Ex_rate.ToUpper());
        SqlParameter P_txtCommission_Intnl_Ex_rate = new SqlParameter("@Commission_Intnl_Exch_Rate", _txtCommission_Intnl_Ex_rate.ToUpper());
        SqlParameter P_txtTheir_Commission_matu = new SqlParameter("@Their_Commission_MATU", _txtTheir_Commission_matu.ToUpper());
        SqlParameter P_txtTheir_Commission_lump = new SqlParameter("@Their_Commission_LUMP", _txtTheir_Commission_lump.ToUpper());
        SqlParameter P_txtTheir_Commission_Contract_no = new SqlParameter("@Their_Commission_Contract_No", _txtTheir_Commission_Contract_no.ToUpper());
        SqlParameter P_txt_Their_Commission_Ex_Curr = new SqlParameter("@Their_Commission_Ex_Curr", _txt_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_txtTheir_Commission_Ex_rate = new SqlParameter("@Their_Commission_Exch_Rate", _txtTheir_Commission_Ex_rate.ToUpper());
        SqlParameter P_txtTheir_Commission_Intnl_Ex_rate = new SqlParameter("@Their_Commission_Intnl_Exch_Rate", _txtTheir_Commission_Intnl_Ex_rate.ToUpper());

        SqlParameter P_CR_Sundry_Deposit_Code = new SqlParameter("@CR_Sundry_Deposit_Code", _txt_CR_Code.ToUpper());
        SqlParameter P_CR_Sundry_Deposits_Short_Name = new SqlParameter("@CR_Sundry_Deposit_Short_Name", _txt_CR_AC_Short_Name.ToUpper());
        SqlParameter P_CR_Sundry_Deposit_Cust_Abbr = new SqlParameter("@CR_Sundry_Deposit_Cust_Abbr", _txt_CR_Cust_abbr.ToUpper());
        SqlParameter P_CR_Sundry_Deposit_Cust_Acc_No = new SqlParameter("@CR_Sundry_Deposit_Cust_Acc_No", _txt_CR_Cust_Acc.ToUpper());
        SqlParameter P_CR_Sundry_Deposit_Curr = new SqlParameter("@CR_Sundry_Deposit_Curr", _txt_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_CR_Sundry_Deposit_Amt = new SqlParameter("@CR_Sundry_Deposit_Amt", _txt_CR_Acceptance_amt.ToUpper());
        SqlParameter P_txt_CR_Acceptance_payer = new SqlParameter("@CR_Sundry_Deposit_Payer", _txt_CR_Acceptance_payer.ToUpper());
        SqlParameter P_txt_CR_Interest_Curr = new SqlParameter("@CR_Interest_Curr", _txt_CR_Interest_Curr.ToUpper());
        SqlParameter P_txt_CR_Interest_amt = new SqlParameter("@CR_Interest_Amount", _txt_CR_Interest_amt.ToUpper());
        SqlParameter P_txt_CR_Interest_payer = new SqlParameter("@CR_Interest_Payer", _txt_CR_Interest_payer.ToUpper());
        SqlParameter P_txt_CR_Accept_Commission_Curr = new SqlParameter("@CR_Acceptance_Comm_Curr", _txt_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_txt_CR_Accept_Commission_amt = new SqlParameter("@CR_Acceptance_Comm_Amount", _txt_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_txt_CR_Accept_Commission_Payer = new SqlParameter("@CR_Acceptance_Comm_Payer", _txt_CR_Accept_Commission_Payer.ToUpper());
        SqlParameter P_txt_CR_Pay_Handle_Commission_Curr = new SqlParameter("@CR_Pay_Handle_Comm_Curr", _txt_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_txt_CR_Pay_Handle_Commission_amt = new SqlParameter("@CR_Pay_Handle_Comm_Amount", _txt_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_txt_CR_Pay_Handle_Commission_Payer = new SqlParameter("@CR_Pay_Handle_Comm_Payer", _txt_CR_Pay_Handle_Commission_Payer.ToUpper());
        SqlParameter P_txt_CR_Others_Curr = new SqlParameter("@CR_Others_Curr", _txt_CR_Others_Curr.ToUpper());
        SqlParameter P_txt_CR_Others_amt = new SqlParameter("@CR_Others_Amount", _txt_CR_Others_amt.ToUpper());
        SqlParameter P_txt_CR_Others_Payer = new SqlParameter("@CR_Others_Payer", _txt_CR_Others_Payer.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_Curr = new SqlParameter("@CR_Their_Comm_Curr", _txt_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_amt = new SqlParameter("@CR_Their_Comm_Amount", _txt_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_txt_CR_Their_Commission_Payer = new SqlParameter("@CR_Their_Comm_Payer", _txt_CR_Their_Commission_Payer.ToUpper());

        SqlParameter P_DR_Code = new SqlParameter("@DR_Code", _txt_DR_Code.ToUpper());
        SqlParameter P_DR_Acc_Short_Name = new SqlParameter("@DR_Acc_Short_Name", _txt_DR_AC_Short_Name.ToUpper());
        SqlParameter P_DR_Cust_Abbr = new SqlParameter("@DR_Cust_Abbr", _txt_DR_Cust_abbr.ToUpper());
        SqlParameter P_DR_Cust_Acc_No = new SqlParameter("@DR_Cust_Acc_No", _txt_DR_Cust_Acc.ToUpper());

        // string _txt_DR_Code, string _txt_DR_AC_Short_Name, string _txt_DR_Cust_abbr, string _txt_DR_Cust_Acc
        SqlParameter P_txt_DR_Cur_Acc_Curr = new SqlParameter("@DR_Current_Acc_Curr", _txt_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_amt = new SqlParameter("@DR_Current_Acc_Amount", _txt_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_payer = new SqlParameter("@DR_Current_Acc_Payer", _txt_DR_Cur_Acc_payer.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_Curr2 = new SqlParameter("@DR_Current_Acc_Curr2", _txt_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_amt2 = new SqlParameter("@DR_Current_Acc_Amount2", _txt_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_payer2 = new SqlParameter("@DR_Current_Acc_Payer2", _txt_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_Curr3 = new SqlParameter("@DR_Current_Acc_Curr3", _txt_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_amt3 = new SqlParameter("@DR_Current_Acc_Amount3", _txt_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_txt_DR_Cur_Acc_payer3 = new SqlParameter("@DR_Current_Acc_Payer3", _txt_DR_Cur_Acc_payer3.ToUpper());

        /////////GENERAL OPRATION 1 
        SqlParameter P_GO_Bill_Handling_Flag = new SqlParameter("@GO_Bill_Handling_Flag", "Y");
        SqlParameter P_txt_Bill_Handling_Comment = new SqlParameter("@GO_Bill_Handling_Comment", _txt_Bill_Handling_Comment.ToUpper());
        SqlParameter P_txt_Bill_Handling_SectionNo = new SqlParameter("@GO_Bill_Handling_Section", _txt_Bill_Handling_SectionNo.ToUpper());
        SqlParameter P_txt_Bill_Handling_Remarks = new SqlParameter("@GO_Bill_Handling_Remark", _txt_Bill_Handling_Remarks.ToUpper());
        SqlParameter P_txt_Bill_Handling_Memo = new SqlParameter("@GO_Bill_Handling_Memo", _txt_Bill_Handling_Memo.ToUpper());
        SqlParameter P_txt_Bill_Handling_Scheme_no = new SqlParameter("@GO_Bill_Handling_SchemeNo", _txt_Bill_Handling_Scheme_no.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Code = new SqlParameter("@GO_Bill_Handling_Debit_Code", _txt_Bill_Handling_Debit_Code.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Curr = new SqlParameter("@GO_Bill_Handling_Debit_CCY", _txt_Bill_Handling_Debit_Curr.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Amt = new SqlParameter("@GO_Bill_Handling_Debit_Amt", _txt_Bill_Handling_Debit_Amt.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Cust = new SqlParameter("@GO_Bill_Handling_Debit_Cust_abbr", _txt_Bill_Handling_Debit_Cust.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Cust_Name = new SqlParameter("@GO_Bill_Handling_Debit_Cust_Name", _txt_Bill_Handling_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Cust_AcCode = new SqlParameter("@GO_Bill_Handling_Debit_Cust_AccCode", _txt_Bill_Handling_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Bill_Handling_Debit_Cust_AccCode_Disc", _txt_Bill_Handling_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Cust_AccNo = new SqlParameter("@GO_Bill_Handling_Debit_Cust_AccNo", _txt_Bill_Handling_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Exch_Rate = new SqlParameter("@GO_Bill_Handling_Debit_ExchRate", _txt_Bill_Handling_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Exch_CCY = new SqlParameter("@GO_Bill_Handling_Debit_ExchCCY", _txt_Bill_Handling_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_FUND = new SqlParameter("@GO_Bill_Handling_Debit_Fund", _txt_Bill_Handling_Debit_FUND.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Check_No = new SqlParameter("@GO_Bill_Handling_Debit_CheckNo", _txt_Bill_Handling_Debit_Check_No.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Available = new SqlParameter("@GO_Bill_Handling_Debit_Available", _txt_Bill_Handling_Debit_Available.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_AdPrint = new SqlParameter("@GO_Bill_Handling_Debit_Advice_Print", _txt_Bill_Handling_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Details = new SqlParameter("@GO_Bill_Handling_Debit_Details", _txt_Bill_Handling_Debit_Details.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Entity = new SqlParameter("@GO_Bill_Handling_Debit_Entity", _txt_Bill_Handling_Debit_Entity.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Division = new SqlParameter("@GO_Bill_Handling_Debit_Division", _txt_Bill_Handling_Debit_Division.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Inter_Amount = new SqlParameter("@GO_Bill_Handling_Debit_InterAmt", _txt_Bill_Handling_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Bill_Handling_Debit_Inter_Rate = new SqlParameter("@GO_Bill_Handling_Debit_InterRate", _txt_Bill_Handling_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Code = new SqlParameter("@GO_Bill_Handling_Credit_Code", _txt_Bill_Handling_Credit_Code.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Curr = new SqlParameter("@GO_Bill_Handling_Credit_CCY", _txt_Bill_Handling_Credit_Curr.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Amt = new SqlParameter("@GO_Bill_Handling_Credit_Amt", _txt_Bill_Handling_Credit_Amt.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Cust = new SqlParameter("@GO_Bill_Handling_Credit_Cust_abbr", _txt_Bill_Handling_Credit_Cust.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Cust_Name = new SqlParameter("@GO_Bill_Handling_Credit_Cust_Name", _txt_Bill_Handling_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Cust_AcCode = new SqlParameter("@GO_Bill_Handling_Credit_Cust_AccCode", _txt_Bill_Handling_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Bill_Handling_Credit_Cust_AccCode_Disc", _txt_Bill_Handling_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Cust_AccNo = new SqlParameter("@GO_Bill_Handling_Credit_Cust_AccNo", _txt_Bill_Handling_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Exch_Rate = new SqlParameter("@GO_Bill_Handling_Credit_ExchRate", _txt_Bill_Handling_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Exch_Curr = new SqlParameter("@GO_Bill_Handling_Credit_ExchCCY", _txt_Bill_Handling_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_FUND = new SqlParameter("@GO_Bill_Handling_Credit_Fund", _txt_Bill_Handling_Credit_FUND.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Check_No = new SqlParameter("@GO_Bill_Handling_Credit_CheckNo", _txt_Bill_Handling_Credit_Check_No.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Available = new SqlParameter("@GO_Bill_Handling_Credit_Available", _txt_Bill_Handling_Credit_Available.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_AdPrint = new SqlParameter("@GO_Bill_Handling_Credit_Advice_Print", _txt_Bill_Handling_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Details = new SqlParameter("@GO_Bill_Handling_Credit_Details", _txt_Bill_Handling_Credit_Details.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Entity = new SqlParameter("@GO_Bill_Handling_Credit_Entity", _txt_Bill_Handling_Credit_Entity.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Division = new SqlParameter("@GO_Bill_Handling_Credit_Division", _txt_Bill_Handling_Credit_Division.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Inter_Amount = new SqlParameter("@GO_Bill_Handling_Credit_InterAmt", _txt_Bill_Handling_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Bill_Handling_Credit_Inter_Rate = new SqlParameter("@GO_Bill_Handling_Credit_InterRate", _txt_Bill_Handling_Credit_Inter_Rate.ToUpper());

        /////////GENERAL OPRATION For Sundry Deposits
        SqlParameter P_GO_Sundry_Deposits_Flag = new SqlParameter("@GO_Sundry_Deposits_Flag", _GO_Sundry_Flag.ToUpper());
        SqlParameter P_txt_Sundry_Comment = new SqlParameter("@GO_Sundry_Deposits_Comment", _txt_Sundry_Comment.ToUpper());
        SqlParameter P_txt_Sundry_SectionNo = new SqlParameter("@GO_Sundry_Deposits_Section", _txt_Sundry_SectionNo.ToUpper());
        SqlParameter P_txt_Sundry_Remarks = new SqlParameter("@GO_Sundry_Deposits_Remark", _txt_Sundry_Remarks.ToUpper());
        SqlParameter P_txt_Sundry_Memo = new SqlParameter("@GO_Sundry_Deposits_Memo", _txt_Sundry_Memo.ToUpper());
        SqlParameter P_txt_Sundry_Scheme_no = new SqlParameter("@GO_Sundry_Deposits_SchemeNo", _txt_Sundry_Scheme_no.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Code = new SqlParameter("@GO_Sundry_Deposits_Debit_Code", _txt_Sundry_Debit_Code.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Curr = new SqlParameter("@GO_Sundry_Deposits_Debit_CCY", _txt_Sundry_Debit_Curr.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Amt = new SqlParameter("@GO_Sundry_Deposits_Debit_Amt", _txt_Sundry_Debit_Amt.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Cust = new SqlParameter("@GO_Sundry_Deposits_Debit_Cust_abbr", _txt_Sundry_Debit_Cust.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Cust_Name = new SqlParameter("@GO_Sundry_Deposits_Debit_Cust_Name", _txt_Sundry_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Cust_AcCode = new SqlParameter("@GO_Sundry_Deposits_Debit_Cust_AccCode", _txt_Sundry_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Sundry_Deposits_Debit_Cust_AccCode_Disc", _txt_Sundry_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Cust_AccNo = new SqlParameter("@GO_Sundry_Deposits_Debit_Cust_AccNo", _txt_Sundry_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Exch_Rate = new SqlParameter("@GO_Sundry_Deposits_Debit_ExchRate", _txt_Sundry_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Exch_CCY = new SqlParameter("@GO_Sundry_Deposits_Debit_ExchCCY", _txt_Sundry_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_Sundry_Debit_FUND = new SqlParameter("@GO_Sundry_Deposits_Debit_Fund", _txt_Sundry_Debit_FUND.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Check_No = new SqlParameter("@GO_Sundry_Deposits_Debit_CheckNo", _txt_Sundry_Debit_Check_No.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Available = new SqlParameter("@GO_Sundry_Deposits_Debit_Available", _txt_Sundry_Debit_Available.ToUpper());
        SqlParameter P_txt_Sundry_Debit_AdPrint = new SqlParameter("@GO_Sundry_Deposits_Debit_Advice_Print", _txt_Sundry_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Details = new SqlParameter("@GO_Sundry_Deposits_Debit_Details", _txt_Sundry_Debit_Details.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Entity = new SqlParameter("@GO_Sundry_Deposits_Debit_Entity", _txt_Sundry_Debit_Entity.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Division = new SqlParameter("@GO_Sundry_Deposits_Debit_Division", _txt_Sundry_Debit_Division.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Inter_Amount = new SqlParameter("@GO_Sundry_Deposits_Debit_InterAmt", _txt_Sundry_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Sundry_Debit_Inter_Rate = new SqlParameter("@GO_Sundry_Deposits_Debit_InterRate", _txt_Sundry_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Code = new SqlParameter("@GO_Sundry_Deposits_Credit_Code", _txt_Sundry_Credit_Code.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Curr = new SqlParameter("@GO_Sundry_Deposits_Credit_CCY", _txt_Sundry_Credit_Curr.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Amt = new SqlParameter("@GO_Sundry_Deposits_Credit_Amt", _txt_Sundry_Credit_Amt.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Cust = new SqlParameter("@GO_Sundry_Deposits_Credit_Cust_abbr", _txt_Sundry_Credit_Cust.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Cust_Name = new SqlParameter("@GO_Sundry_Deposits_Credit_Cust_Name", _txt_Sundry_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Cust_AcCode = new SqlParameter("@GO_Sundry_Deposits_Credit_Cust_AccCode", _txt_Sundry_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Sundry_Deposits_Credit_Cust_AccCode_Disc", _txt_Sundry_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Cust_AccNo = new SqlParameter("@GO_Sundry_Deposits_Credit_Cust_AccNo", _txt_Sundry_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Exch_Rate = new SqlParameter("@GO_Sundry_Deposits_Credit_ExchRate", _txt_Sundry_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Exch_Curr = new SqlParameter("@GO_Sundry_Deposits_Credit_ExchCCY", _txt_Sundry_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_Sundry_Credit_FUND = new SqlParameter("@GO_Sundry_Deposits_Credit_Fund", _txt_Sundry_Credit_FUND.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Check_No = new SqlParameter("@GO_Sundry_Deposits_Credit_CheckNo", _txt_Sundry_Credit_Check_No.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Available = new SqlParameter("@GO_Sundry_Deposits_Credit_Available", _txt_Sundry_Credit_Available.ToUpper());
        SqlParameter P_txt_Sundry_Credit_AdPrint = new SqlParameter("@GO_Sundry_Deposits_Credit_Advice_Print", _txt_Sundry_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Details = new SqlParameter("@GO_Sundry_Deposits_Credit_Details", _txt_Sundry_Credit_Details.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Entity = new SqlParameter("@GO_Sundry_Deposits_Credit_Entity", _txt_Sundry_Credit_Entity.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Division = new SqlParameter("@GO_Sundry_Deposits_Credit_Division", _txt_Sundry_Credit_Division.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Inter_Amount = new SqlParameter("@GO_Sundry_Deposits_Credit_InterAmt", _txt_Sundry_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Sundry_Credit_Inter_Rate = new SqlParameter("@GO_Sundry_Deposits_Credit_InterRate", _txt_Sundry_Credit_Inter_Rate.ToUpper());

        /////////GENERAL OPRATION 3
        SqlParameter P_GO_Comm_Received_Flag = new SqlParameter("@GO_Comm_Received_Flag", _GO_Comm_Recieved_Flag.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Comment = new SqlParameter("@GO_Comm_Received_Comment", _txt_Comm_Recieved_Comment.ToUpper());
        SqlParameter P_txt_Comm_Recieved_SectionNo = new SqlParameter("@GO_Comm_Received_Section", _txt_Comm_Recieved_SectionNo.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Remarks = new SqlParameter("@GO_Comm_Received_Remark", _txt_Comm_Recieved_Remarks.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Memo = new SqlParameter("@GO_Comm_Received_Memo", _txt_Comm_Recieved_Memo.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Scheme_no = new SqlParameter("@GO_Comm_Received_SchemeNo", _txt_Comm_Recieved_Scheme_no.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Code = new SqlParameter("@GO_Comm_Received_Debit_Code", _txt_Comm_Recieved_Debit_Code.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Curr = new SqlParameter("@GO_Comm_Received_Debit_CCY", _txt_Comm_Recieved_Debit_Curr.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Amt = new SqlParameter("@GO_Comm_Received_Debit_Amt", _txt_Comm_Recieved_Debit_Amt.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Cust = new SqlParameter("@GO_Comm_Received_Debit_Cust_abbr", _txt_Comm_Recieved_Debit_Cust.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Cust_Name = new SqlParameter("@GO_Comm_Received_Debit_Cust_Name", _txt_Comm_Recieved_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Cust_AcCode = new SqlParameter("@GO_Comm_Received_Debit_Cust_AccCode", _txt_Comm_Recieved_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Comm_Received_Debit_Cust_AccCode_Disc", _txt_Comm_Recieved_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Cust_AccNo = new SqlParameter("@GO_Comm_Received_Debit_Cust_AccNo", _txt_Comm_Recieved_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Exch_Rate = new SqlParameter("@GO_Comm_Received_Debit_ExchRate", _txt_Comm_Recieved_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Exch_CCY = new SqlParameter("@GO_Comm_Received_Debit_ExchCCY", _txt_Comm_Recieved_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_FUND = new SqlParameter("@GO_Comm_Received_Debit_Fund", _txt_Comm_Recieved_Debit_FUND.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Check_No = new SqlParameter("@GO_Comm_Received_Debit_CheckNo", _txt_Comm_Recieved_Debit_Check_No.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Available = new SqlParameter("@GO_Comm_Received_Debit_Available", _txt_Comm_Recieved_Debit_Available.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_AdPrint = new SqlParameter("@GO_Comm_Received_Debit_Advice_Print", _txt_Comm_Recieved_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Details = new SqlParameter("@GO_Comm_Received_Debit_Details", _txt_Comm_Recieved_Debit_Details.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Entity = new SqlParameter("@GO_Comm_Received_Debit_Entity", _txt_Comm_Recieved_Debit_Entity.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Division = new SqlParameter("@GO_Comm_Received_Debit_Division", _txt_Comm_Recieved_Debit_Division.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Inter_Amount = new SqlParameter("@GO_Comm_Received_Debit_InterAmt", _txt_Comm_Recieved_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Debit_Inter_Rate = new SqlParameter("@GO_Comm_Received_Debit_InterRate", _txt_Comm_Recieved_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Code = new SqlParameter("@GO_Comm_Received_Credit_Code", _txt_Comm_Recieved_Credit_Code.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Curr = new SqlParameter("@GO_Comm_Received_Credit_CCY", _txt_Comm_Recieved_Credit_Curr.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Amt = new SqlParameter("@GO_Comm_Received_Credit_Amt", _txt_Comm_Recieved_Credit_Amt.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Cust = new SqlParameter("@GO_Comm_Received_Credit_Cust_abbr", _txt_Comm_Recieved_Credit_Cust.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Cust_Name = new SqlParameter("@GO_Comm_Received_Credit_Cust_Name", _txt_Comm_Recieved_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Cust_AcCode = new SqlParameter("@GO_Comm_Received_Credit_Cust_AccCode", _txt_Comm_Recieved_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Comm_Received_Credit_Cust_AccCode_Disc", _txt_Comm_Recieved_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Cust_AccNo = new SqlParameter("@GO_Comm_Received_Credit_Cust_AccNo", _txt_Comm_Recieved_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Exch_Rate = new SqlParameter("@GO_Comm_Received_Credit_ExchRate", _txt_Comm_Recieved_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Exch_Curr = new SqlParameter("@GO_Comm_Received_Credit_ExchCCY", _txt_Comm_Recieved_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_FUND = new SqlParameter("@GO_Comm_Received_Credit_Fund", _txt_Comm_Recieved_Credit_FUND.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Check_No = new SqlParameter("@GO_Comm_Received_Credit_CheckNo", _txt_Comm_Recieved_Credit_Check_No.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Available = new SqlParameter("@GO_Comm_Received_Credit_Available", _txt_Comm_Recieved_Credit_Available.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_AdPrint = new SqlParameter("@GO_Comm_Received_Credit_Advice_Print", _txt_Comm_Recieved_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Details = new SqlParameter("@GO_Comm_Received_Credit_Details", _txt_Comm_Recieved_Credit_Details.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Entity = new SqlParameter("@GO_Comm_Received_Credit_Entity", _txt_Comm_Recieved_Credit_Entity.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Division = new SqlParameter("@GO_Comm_Received_Credit_Division", _txt_Comm_Recieved_Credit_Division.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Inter_Amount = new SqlParameter("@GO_Comm_Received_Credit_InterAmt", _txt_Comm_Recieved_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Comm_Recieved_Credit_Inter_Rate = new SqlParameter("@GO_Comm_Received_Credit_InterRate", _txt_Comm_Recieved_Credit_Inter_Rate.ToUpper());

        /////////GENERAL OPRATION 4
        SqlParameter P_GO_Acc_Change_Flag = new SqlParameter("@GO_Acc_Change_Flag", _GO_Acc_Change_Flag.ToUpper());
        SqlParameter P_txt_Acc_Change_Comment = new SqlParameter("@GO_Acc_Change_Comment", _txt_Acc_Change_Comment.ToUpper());
        SqlParameter P_txt_Acc_Change_SectionNo = new SqlParameter("@GO_Acc_Change_Section", _txt_Acc_Change_SectionNo.ToUpper());
        SqlParameter P_txt_Acc_Change_Remarks = new SqlParameter("@GO_Acc_Change_Remark", _txt_Acc_Change_Remarks.ToUpper());
        SqlParameter P_txt_Acc_Change_Memo = new SqlParameter("@GO_Acc_Change_Memo", _txt_Acc_Change_Memo.ToUpper());
        SqlParameter P_txt_Acc_Change_Scheme_no = new SqlParameter("@GO_Acc_Change_SchemeNo", _txt_Acc_Change_Scheme_no.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Code = new SqlParameter("@GO_Acc_Change_Debit_Code", _txt_Acc_Change_Debit_Code.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Curr = new SqlParameter("@GO_Acc_Change_Debit_CCY", _txt_Acc_Change_Debit_Curr.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Amt = new SqlParameter("@GO_Acc_Change_Debit_Amt", _txt_Acc_Change_Debit_Amt.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Cust = new SqlParameter("@GO_Acc_Change_Debit_Cust_abbr", _txt_Acc_Change_Debit_Cust.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Cust_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_Name", _txt_Acc_Change_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode", _txt_Acc_Change_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode_Disc", _txt_Acc_Change_Debit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccNo", _txt_Acc_Change_Debit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Debit_ExchRate", _txt_Acc_Change_Debit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Exch_CCY = new SqlParameter("@GO_Acc_Change_Debit_ExchCCY", _txt_Acc_Change_Debit_Exch_CCY.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_FUND = new SqlParameter("@GO_Acc_Change_Debit_Fund", _txt_Acc_Change_Debit_FUND.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Check_No = new SqlParameter("@GO_Acc_Change_Debit_CheckNo", _txt_Acc_Change_Debit_Check_No.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Available = new SqlParameter("@GO_Acc_Change_Debit_Available", _txt_Acc_Change_Debit_Available.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_AdPrint = new SqlParameter("@GO_Acc_Change_Debit_Advice_Print", _txt_Acc_Change_Debit_AdPrint.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Details = new SqlParameter("@GO_Acc_Change_Debit_Details", _txt_Acc_Change_Debit_Details.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Entity = new SqlParameter("@GO_Acc_Change_Debit_Entity", _txt_Acc_Change_Debit_Entity.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Division = new SqlParameter("@GO_Acc_Change_Debit_Division", _txt_Acc_Change_Debit_Division.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Debit_InterAmt", _txt_Acc_Change_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Acc_Change_Debit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Debit_InterRate", _txt_Acc_Change_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Code = new SqlParameter("@GO_Acc_Change_Credit_Code", _txt_Acc_Change_Credit_Code.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Curr = new SqlParameter("@GO_Acc_Change_Credit_CCY", _txt_Acc_Change_Credit_Curr.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Amt = new SqlParameter("@GO_Acc_Change_Credit_Amt", _txt_Acc_Change_Credit_Amt.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Cust = new SqlParameter("@GO_Acc_Change_Credit_Cust_abbr", _txt_Acc_Change_Credit_Cust.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Cust_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_Name", _txt_Acc_Change_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode", _txt_Acc_Change_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode_Disc", _txt_Acc_Change_Credit_Cust_AcCode_Name.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccNo", _txt_Acc_Change_Credit_Cust_AccNo.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Credit_ExchRate", _txt_Acc_Change_Credit_Exch_Rate.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Exch_Curr = new SqlParameter("@GO_Acc_Change_Credit_ExchCCY", _txt_Acc_Change_Credit_Exch_Curr.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_FUND = new SqlParameter("@GO_Acc_Change_Credit_Fund", _txt_Acc_Change_Credit_FUND.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Check_No = new SqlParameter("@GO_Acc_Change_Credit_CheckNo", _txt_Acc_Change_Credit_Check_No.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Available = new SqlParameter("@GO_Acc_Change_Credit_Available", _txt_Acc_Change_Credit_Available.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_AdPrint = new SqlParameter("@GO_Acc_Change_Credit_Advice_Print", _txt_Acc_Change_Credit_AdPrint.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Details = new SqlParameter("@GO_Acc_Change_Credit_Details", _txt_Acc_Change_Credit_Details.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Entity = new SqlParameter("@GO_Acc_Change_Credit_Entity", _txt_Acc_Change_Credit_Entity.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Division = new SqlParameter("@GO_Acc_Change_Credit_Division", _txt_Acc_Change_Credit_Division.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Credit_InterAmt", _txt_Acc_Change_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_Acc_Change_Credit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Credit_InterRate", _txt_Acc_Change_Credit_Inter_Rate.ToUpper());

        SqlParameter P_rdb_swift_None = new SqlParameter("@_rdb_swift_None", _rdb_swift_None.ToUpper());
        SqlParameter P_rdb_swift_200 = new SqlParameter("@_rdb_swift_200", _rdb_swift_200.ToUpper());

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

        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdate_Settlement", P_BranchName, P_Document_No, P_Settlement_Date, P_Comment_Code, P_txt_Doc_Settlement_for_Cust, P_txt_Doc_Settlement_For_Bank,
        P_txt_Doc_Interest_From, P_txt_Doc_Interest_To, P_txt_Doc_No_Of_Days, P_txt_Doc_Discount, P_txt_Doc_Amount, P_txt_Doc_Rate, P_txt_Doc_Overdueamount, P_txt_Doc_Overdue_Interestrate,
        P_txt_Doc_Oveduenoofdays, P_txt_Doc_Attn,

        P_IMP_ACC_ExchRate, P_txtPrinc_matu, P_txtPrinc_lump, P_txtprinc_Contract_no, P_txt_Princ_Ex_Curr, P_txtprinc_Ex_rate, P_txtprinc_Intnl_Ex_rate,
        P_txtInterest_matu, P_txtInterest_lump, P_txtInterest_Contract_no, P_txt_interest_Ex_Curr, P_txtInterest_Ex_rate, P_txtInterest_Intnl_Ex_rate,
        P_txtCommission_matu, P_txtCommission_lump, P_txtCommission_Contract_no, P_txt_Commission_Ex_Curr, P_txtCommission_Ex_rate, P_txtCommission_Intnl_Ex_rate,
        P_txtTheir_Commission_matu, P_txtTheir_Commission_lump, P_txtTheir_Commission_Contract_no, P_txt_Their_Commission_Ex_Curr, P_txtTheir_Commission_Ex_rate, P_txtTheir_Commission_Intnl_Ex_rate,
        P_CR_Sundry_Deposit_Code, P_CR_Sundry_Deposits_Short_Name, P_CR_Sundry_Deposit_Cust_Abbr, P_CR_Sundry_Deposit_Cust_Acc_No, P_CR_Sundry_Deposit_Curr, P_CR_Sundry_Deposit_Amt, P_txt_CR_Acceptance_payer,
        P_txt_CR_Interest_amt, P_txt_CR_Interest_Curr, P_txt_CR_Interest_payer,
        P_txt_CR_Accept_Commission_amt, P_txt_CR_Accept_Commission_Curr, P_txt_CR_Accept_Commission_Payer,
        P_txt_CR_Pay_Handle_Commission_amt, P_txt_CR_Pay_Handle_Commission_Curr, P_txt_CR_Pay_Handle_Commission_Payer,
        P_txt_CR_Others_amt, P_txt_CR_Others_Curr, P_txt_CR_Others_Payer,
        P_txt_CR_Their_Commission_amt, P_txt_CR_Their_Commission_Curr, P_txt_CR_Their_Commission_Payer,
        P_DR_Code, P_DR_Acc_Short_Name, P_DR_Cust_Abbr, P_DR_Cust_Acc_No,
        P_txt_DR_Cur_Acc_amt, P_txt_DR_Cur_Acc_Curr, P_txt_DR_Cur_Acc_payer,
        P_txt_DR_Cur_Acc_amt2, P_txt_DR_Cur_Acc_Curr2, P_txt_DR_Cur_Acc_payer2,
        P_txt_DR_Cur_Acc_amt3, P_txt_DR_Cur_Acc_Curr3, P_txt_DR_Cur_Acc_payer3,
        P_GO_Bill_Handling_Flag, P_txt_Bill_Handling_Comment, P_txt_Bill_Handling_SectionNo, P_txt_Bill_Handling_Remarks, P_txt_Bill_Handling_Memo, P_txt_Bill_Handling_Scheme_no,
        P_txt_Bill_Handling_Debit_Code, P_txt_Bill_Handling_Debit_Curr, P_txt_Bill_Handling_Debit_Amt, P_txt_Bill_Handling_Debit_Cust_AcCode, P_txt_Bill_Handling_Debit_Cust_AccNo,
        P_txt_Bill_Handling_Debit_Exch_Rate, P_txt_Bill_Handling_Debit_Exch_CCY, P_txt_Bill_Handling_Debit_FUND, P_txt_Bill_Handling_Debit_Check_No, P_txt_Bill_Handling_Debit_Available,
        P_txt_Bill_Handling_Debit_AdPrint, P_txt_Bill_Handling_Debit_Details, P_txt_Bill_Handling_Debit_Entity, P_txt_Bill_Handling_Debit_Division, P_txt_Bill_Handling_Debit_Inter_Amount,
        P_txt_Bill_Handling_Debit_Inter_Rate, P_txt_Bill_Handling_Credit_Code, P_txt_Bill_Handling_Credit_Curr,
        P_txt_Bill_Handling_Credit_Amt, P_txt_Bill_Handling_Credit_Cust_AcCode, P_txt_Bill_Handling_Credit_Cust_AccNo, P_txt_Bill_Handling_Credit_Exch_Rate, P_txt_Bill_Handling_Credit_Exch_Curr,
        P_txt_Bill_Handling_Credit_FUND, P_txt_Bill_Handling_Credit_Check_No, P_txt_Bill_Handling_Credit_Available, P_txt_Bill_Handling_Credit_AdPrint, P_txt_Bill_Handling_Credit_Details,
        P_txt_Bill_Handling_Credit_Entity, P_txt_Bill_Handling_Credit_Division, P_txt_Bill_Handling_Credit_Inter_Amount, P_txt_Bill_Handling_Credit_Inter_Rate,

        P_GO_Sundry_Deposits_Flag, P_txt_Sundry_Comment, P_txt_Sundry_SectionNo, P_txt_Sundry_Remarks, P_txt_Sundry_Memo, P_txt_Sundry_Scheme_no, P_txt_Sundry_Debit_Code, P_txt_Sundry_Debit_Curr,
        P_txt_Sundry_Debit_Amt, P_txt_Sundry_Debit_Cust_AcCode, P_txt_Sundry_Debit_Cust_AccNo, P_txt_Sundry_Debit_Exch_Rate, P_txt_Sundry_Debit_Exch_CCY, P_txt_Sundry_Debit_FUND, P_txt_Sundry_Debit_Check_No,
        P_txt_Sundry_Debit_Available, P_txt_Sundry_Debit_AdPrint, P_txt_Sundry_Debit_Details, P_txt_Sundry_Debit_Entity, P_txt_Sundry_Debit_Division,
        P_txt_Sundry_Debit_Inter_Amount, P_txt_Sundry_Debit_Inter_Rate, P_txt_Sundry_Credit_Code, P_txt_Sundry_Credit_Curr, P_txt_Sundry_Credit_Amt,
        P_txt_Sundry_Credit_Cust_AcCode, P_txt_Sundry_Credit_Cust_AccNo, P_txt_Sundry_Credit_Exch_Rate, P_txt_Sundry_Credit_Exch_Curr,
        P_txt_Sundry_Credit_FUND, P_txt_Sundry_Credit_Check_No, P_txt_Sundry_Credit_Available, P_txt_Sundry_Credit_AdPrint, P_txt_Sundry_Credit_Details, P_txt_Sundry_Credit_Entity,
        P_txt_Sundry_Credit_Division, P_txt_Sundry_Credit_Inter_Amount, P_txt_Sundry_Credit_Inter_Rate,

        P_GO_Comm_Received_Flag, P_txt_Comm_Recieved_Comment, P_txt_Comm_Recieved_SectionNo, P_txt_Comm_Recieved_Remarks, P_txt_Comm_Recieved_Memo, P_txt_Comm_Recieved_Scheme_no,
        P_txt_Comm_Recieved_Debit_Code, P_txt_Comm_Recieved_Debit_Curr, P_txt_Comm_Recieved_Debit_Amt, P_txt_Comm_Recieved_Debit_Cust_AcCode,
        P_txt_Comm_Recieved_Debit_Cust_AccNo, P_txt_Comm_Recieved_Debit_Exch_Rate, P_txt_Comm_Recieved_Debit_Exch_CCY, P_txt_Comm_Recieved_Debit_FUND, P_txt_Comm_Recieved_Debit_Check_No,
        P_txt_Comm_Recieved_Debit_Available, P_txt_Comm_Recieved_Debit_AdPrint, P_txt_Comm_Recieved_Debit_Details, P_txt_Comm_Recieved_Debit_Entity, P_txt_Comm_Recieved_Debit_Division,
        P_txt_Comm_Recieved_Debit_Inter_Amount, P_txt_Comm_Recieved_Debit_Inter_Rate, P_txt_Comm_Recieved_Credit_Code, P_txt_Comm_Recieved_Credit_Curr, P_txt_Comm_Recieved_Credit_Amt,
        P_txt_Comm_Recieved_Credit_Cust_AcCode, P_txt_Comm_Recieved_Credit_Cust_AccNo, P_txt_Comm_Recieved_Credit_Exch_Rate, P_txt_Comm_Recieved_Credit_Exch_Curr,
        P_txt_Comm_Recieved_Credit_FUND, P_txt_Comm_Recieved_Credit_Check_No, P_txt_Comm_Recieved_Credit_Available, P_txt_Comm_Recieved_Credit_AdPrint, P_txt_Comm_Recieved_Credit_Details,
        P_txt_Comm_Recieved_Credit_Entity, P_txt_Comm_Recieved_Credit_Division, P_txt_Comm_Recieved_Credit_Inter_Amount, P_txt_Comm_Recieved_Credit_Inter_Rate,

        ///////////////////// GENERAL OPRATOIN For Change Branch /////////////////////////////////////////////////////////////
        P_GO_Acc_Change_Flag, P_txt_Acc_Change_Comment, P_txt_Acc_Change_SectionNo, P_txt_Acc_Change_Remarks, P_txt_Acc_Change_Memo,
        P_txt_Acc_Change_Scheme_no, P_txt_Acc_Change_Debit_Code, P_txt_Acc_Change_Debit_Curr, P_txt_Acc_Change_Debit_Amt, P_txt_Acc_Change_Debit_Cust_AcCode,
        P_txt_Acc_Change_Debit_Cust_AccNo, P_txt_Acc_Change_Debit_Exch_Rate, P_txt_Acc_Change_Debit_Exch_CCY, P_txt_Acc_Change_Debit_FUND, P_txt_Acc_Change_Debit_Check_No,
        P_txt_Acc_Change_Debit_Available, P_txt_Acc_Change_Debit_AdPrint, P_txt_Acc_Change_Debit_Details, P_txt_Acc_Change_Debit_Entity, P_txt_Acc_Change_Debit_Division,
        P_txt_Acc_Change_Debit_Inter_Amount, P_txt_Acc_Change_Debit_Inter_Rate, P_txt_Acc_Change_Credit_Code, P_txt_Acc_Change_Credit_Curr, P_txt_Acc_Change_Credit_Amt,
        P_txt_Acc_Change_Credit_Cust_AcCode, P_txt_Acc_Change_Credit_Cust_AccNo, P_txt_Acc_Change_Credit_Exch_Rate, P_txt_Acc_Change_Credit_Exch_Curr,
        P_txt_Acc_Change_Credit_FUND, P_txt_Acc_Change_Credit_Check_No, P_txt_Acc_Change_Credit_Available, P_txt_Acc_Change_Credit_AdPrint, P_txt_Acc_Change_Credit_Details,
        P_txt_Acc_Change_Credit_Entity, P_txt_Acc_Change_Credit_Division, P_txt_Acc_Change_Credit_Inter_Amount, P_txt_Acc_Change_Credit_Inter_Rate,
        P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate,

        P_txt_Bill_Handling_Debit_Cust, P_txt_Bill_Handling_Debit_Cust_Name, P_txt_Bill_Handling_Debit_Cust_AcCode_Name, P_txt_Bill_Handling_Credit_Cust, P_txt_Bill_Handling_Credit_Cust_Name, P_txt_Bill_Handling_Credit_Cust_AcCode_Name,
P_txt_Sundry_Debit_Cust, P_txt_Sundry_Debit_Cust_Name, P_txt_Sundry_Debit_Cust_AcCode_Name, P_txt_Sundry_Credit_Cust, P_txt_Sundry_Credit_Cust_Name, P_txt_Sundry_Credit_Cust_AcCode_Name,
P_txt_Comm_Recieved_Debit_Cust, P_txt_Comm_Recieved_Debit_Cust_Name, P_txt_Comm_Recieved_Debit_Cust_AcCode_Name, P_txt_Comm_Recieved_Credit_Cust, P_txt_Comm_Recieved_Credit_Cust_Name, P_txt_Comm_Recieved_Credit_Cust_AcCode_Name,
P_txt_Acc_Change_Debit_Cust, P_txt_Acc_Change_Debit_Cust_Name, P_txt_Acc_Change_Debit_Cust_AcCode_Name, P_txt_Acc_Change_Credit_Cust, P_txt_Acc_Change_Credit_Cust_Name, P_txt_Acc_Change_Credit_Cust_AcCode_Name

        // MT 200
        , P_rdb_swift_None, P_rdb_swift_200, P_txt200TransactionRefNO, P_txt200Date, P_txt200Currency, P_txt200Amount, P_txt200SenderCorreCode, P_txt200SenderCorreLocation,
P_ddl200Intermediary, P_txt200IntermediaryAccountNumber, P_txt200IntermediarySwiftCode, P_txt200IntermediaryName, P_txt200IntermediaryAddress1,
P_txt200IntermediaryAddress2, P_txt200IntermediaryAddress3, P_ddl200AccWithInstitution, P_txt200AccWithInstitutionAccountNumber, P_txt200AccWithInstitutionSwiftCode,
P_txt200AccWithInstitutionLocation, P_txt200AccWithInstitutionName, P_txt200AccWithInstitutionAddress1, P_txt200AccWithInstitutionAddress2, P_txt200AccWithInstitutionAddress3
, P_txt200SendertoReceiverInformation1, P_txt200SendertoReceiverInformation2, P_txt200SendertoReceiverInformation3,
P_txt200SendertoReceiverInformation4, P_txt200SendertoReceiverInformation5, P_txt200SendertoReceiverInformation6,
P_txtTransactionRefNoR42, P_txtRelatedReferenceR42, P_txtValueDateR42, P_txtCureencyR42, P_txtAmountR42, P_txtOrderingInstitutionIFSCR42, P_txtBeneficiaryInstitutionIFSCR42,
P_txtCodeWordR42, P_txtAdditionalInformationR42, P_txtMoreInfo1R42, P_txtMoreInfo2R42, P_txtMoreInfo3R42, P_txtMoreInfo4R42,
P_txtMoreInfo5R42, P_rdb_swift_R42
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
            string _script = "";
            _script = "window.location='TF_IMP_Settlement_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
    }

    protected void cb_GO_Bill_Handling_Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (cb_GO_Bill_Handling_Flag.Checked == false)
        {
            PanelGO_Bill_Handling.Visible = false;
        }
        else if (cb_GO_Bill_Handling_Flag.Checked == true)
        {
            PanelGO_Bill_Handling.Visible = true;

            txt_Bill_Handling_Debit_Cust_AcCode.Text = txt_DR_Code.Text;
            txt_Bill_Handling_Debit_Cust.Text = txt_DR_Cust_abbr.Text;
            txt_Bill_Handling_Debit_Cust_AccNo.Text = txt_DR_Cust_Acc.Text;
            txt_Bill_Handling_Debit_Cust_AcCode_Name.Text = txt_DR_AC_Short_Name.Text;
        }
    }
    protected void cb_GO_Sundry_Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (cb_GO_Sundry_Flag.Checked == false)
        {
            panalGO_Sundry.Visible = false;
        }
        else if (cb_GO_Sundry_Flag.Checked == true)
        {
            panalGO_Sundry.Visible = true;
        }
    }
    protected void cb_GO_Comm_Recieved_Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (cb_GO_Comm_Recieved_Flag.Checked == false)
        {
            PanelGO_Comm_Recieved.Visible = false;
        }
        else if (cb_GO_Comm_Recieved_Flag.Checked == true)
        {
            PanelGO_Comm_Recieved.Visible = true;
        }
    }
    protected void cb_GO_Acc_Change_Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (cb_GO_Acc_Change_Flag.Checked == false)
        {
            PanelGO_Acc_Change.Visible = false;
        }
        else if (cb_GO_Acc_Change_Flag.Checked == true)
        {
            PanelGO_Acc_Change.Visible = true;
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
}