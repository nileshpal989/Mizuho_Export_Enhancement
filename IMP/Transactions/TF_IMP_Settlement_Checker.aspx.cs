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

public partial class IMP_Transactions_TF_IMP_Settlement_Checker : System.Web.UI.Page
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
                    Response.Redirect("TF_IMP_Settlement_Maker_View.aspx", true);
                }
                else
                {
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    hdnDocNo.Value = Request.QueryString["DocNo"].Trim();
                    hdnflcIlcType.Value = Request.QueryString["FLC_ILC"].Trim();
                    hdnDoc_Scrutiny.Value = Request.QueryString["Doc_Scrutiny"].Trim();

                    Toggel_DocType(hdnDocType.Value, hdnBranchName.Value, hdnflcIlcType.Value, hdnDoc_Scrutiny.Value);
                    Fill_Logd_Details();
                    Fill_Settlement_Details();

                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                    }
                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
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
                                txt_Comm_Recieved_Debit_Cust.Text = "600";
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

            //txt_Doc_Comment_Code.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Settlement_for_Cust.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Settlement_For_Bank.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Discount.Text = dt.Rows[0][""].ToString();
            txt_Doc_Interest_To.Text = dt.Rows[0]["Maturity_Date"].ToString();
            txt_Doc_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();
            //txt_Doc_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Overdue_Interestrate.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Oveduenoofdays.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Overdueamount.Text = dt.Rows[0][""].ToString();
            //txt_Doc_Attn.Text = dt.Rows[0][""].ToString();

            ///////////////////    IMport Accounting     ///////////////////////////////////////////////////////////////////

            txt_DiscAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            //txtPrinc_matu.Text = dt.Rows[0][""].ToString();
            //txtPrinc_lump.Text = dt.Rows[0][""].ToString();
            // txtprinc_Contract_no.Text = dt.Rows[0][""].ToString();
            //txt_Princ_Ex_Curr.Text = dt.Rows[0][""].ToString();
            //txtprinc_Ex_rate.Text = dt.Rows[0][""].ToString();
            //txtprinc_Intnl_Ex_rate.Text = dt.Rows[0][""].ToString();
            //txtInterest_matu.Text = dt.Rows[0][""].ToString();
            //txtInterest_lump.Text = dt.Rows[0][""].ToString();
            //txtInterest_Contract_no.Text = dt.Rows[0][""].ToString();
            //txt_interest_Ex_Curr.Text = dt.Rows[0][""].ToString();
            //txtInterest_Ex_rate.Text = dt.Rows[0][""].ToString();
            //txtInterest_Intnl_Ex_rate.Text = dt.Rows[0][""].ToString();
            //txtCommission_matu.Text = dt.Rows[0][""].ToString();
            //txtCommission_lump.Text = dt.Rows[0][""].ToString();
            //txtCommission_Contract_no.Text = dt.Rows[0][""].ToString();
            //txt_Commission_Ex_Curr.Text = dt.Rows[0][""].ToString();
            //txtCommission_Ex_rate.Text = dt.Rows[0][""].ToString();
            //txtCommission_Intnl_Ex_rate.Text = dt.Rows[0][""].ToString();
            //txtTheir_Commission_matu.Text = dt.Rows[0][""].ToString();
            //txtTheir_Commission_lump.Text = dt.Rows[0][""].ToString();
            //txtTheir_Commission_Contract_no.Text = dt.Rows[0][""].ToString();
            //txt_Their_Commission_Ex_Curr.Text = dt.Rows[0][""].ToString();
            //txtTheir_Commission_Ex_rate.Text = dt.Rows[0][""].ToString();
            //txtTheir_Commission_Intnl_Ex_rate.Text = dt.Rows[0][""].ToString();

            //txt_CR_Code.Text = dt.Rows[0][""].ToString();
            //txt_CR_AC_Short_Name.Text = dt.Rows[0][""].ToString();
            //txt_CR_Cust_abbr.Text = dt.Rows[0][""].ToString();
            //txt_CR_Cust_Acc.Text = dt.Rows[0][""].ToString();
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            //txt_CR_Acceptance_payer.Text = dt.Rows[0][""].ToString();
            txt_CR_Interest_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            //txt_CR_Interest_amt.Text = dt.Rows[0][""].ToString();
            //txt_CR_Interest_payer.Text = dt.Rows[0][""].ToString();
            txt_CR_Accept_Commission_Curr.Text = "INR";
            //txt_CR_Accept_Commission_amt.Text = dt.Rows[0][""].ToString();
            //txt_CR_Accept_Commission_Payer.Text = dt.Rows[0][""].ToString();
            txt_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            //txt_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0][""].ToString();
            //txt_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0][""].ToString();
            txt_CR_Others_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            //txt_CR_Others_amt.Text = dt.Rows[0][""].ToString();
            //txt_CR_Others_Payer.Text = dt.Rows[0][""].ToString();
            txt_CR_Their_Commission_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            //txt_CR_Their_Commission_amt.Text = dt.Rows[0][""].ToString();
            //txt_CR_Their_Commission_Payer.Text = dt.Rows[0][""].ToString();
            txt_DR_Code.Text = dt.Rows[0]["AC_Code"].ToString();
            txt_DR_AC_Short_Name.Text = "CURRENT ACCOUNT";
            txt_DR_Cust_abbr.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["CustAcNo"].ToString();
            txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            //txt_DR_Cur_Acc_payer.Text = dt.Rows[0][""].ToString();
            //txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0][""].ToString();
            //txt_DR_Cur_Acc_amt2.Text = dt.Rows[0][""].ToString();
            //txt_DR_Cur_Acc_payer2.Text = dt.Rows[0][""].ToString();
            //txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0][""].ToString();
            //txt_DR_Cur_Acc_amt3.Text = dt.Rows[0][""].ToString();
            //txt_DR_Cur_Acc_payer3.Text = dt.Rows[0][""].ToString();

            ///////////////////// GENERAL OPRATOIN For Bill Handling /////////////////////////////////////////////////////////////

            //txt_Bill_Handling_Value_Date.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Ref_No.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Comment.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_SectionNo.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Remarks.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Memo.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Scheme_no.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Code.Text = dt.Rows[0][""].ToString();
            txt_Bill_Handling_Debit_Curr.Text = "INR";
            //txt_Bill_Handling_Debit_Amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            txt_Bill_Handling_Debit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            //txt_Bill_Handling_Debit_Cust_Name.Text = dt.Rows[0][""].ToString();
            txt_Bill_Handling_Debit_Cust_AcCode.Text = dt.Rows[0]["AC_Code"].ToString();
            //txt_Bill_Handling_Debit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            txt_Bill_Handling_Debit_Cust_AccNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            //txt_Bill_Handling_Debit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Exch_CCY.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Debit_Inter_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Code.Text = dt.Rows[0][""].ToString();
            txt_Bill_Handling_Credit_Curr.Text = "INR";
            //txt_Bill_Handling_Credit_Amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            //txt_Bill_Handling_Credit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            //txt_Bill_Handling_Credit_Cust_Name.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Cust_AcCode.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Cust_AccNo.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Exch_Curr.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Bill_Handling_Credit_Inter_Rate.Text = dt.Rows[0][""].ToString();

            ///////////////////// GENERAL OPRATOIN For Sundry Deposits /////////////////////////////////////////////////////////////

            //txt_Sundry_Vlaue_Date.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Ref_No.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Comment.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_SectionNo.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Remarks.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Memo.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Scheme_no.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Code.Text = dt.Rows[0][""].ToString();
            txt_Sundry_Debit_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_Sundry_Debit_Amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            //txt_Sundry_Debit_Cust.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Cust_Name.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Cust_AcCode.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Cust_AccNo.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Exch_CCY.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Debit_Inter_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Code.Text = dt.Rows[0][""].ToString();
            txt_Sundry_Credit_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            txt_Sundry_Credit_Amt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            //txt_Sundry_Credit_Cust.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Cust_Name.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Cust_AcCode.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Cust_AccNo.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Exch_Curr.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Sundry_Credit_Inter_Rate.Text = dt.Rows[0][""].ToString();

            ///////////////////// GENERAL OPRATOIN For Comm_Recieved /////////////////////////////////////////////////////////////

            //txt_Comm_Recieved_value_Date.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Ref_No.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Comment.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_SectionNo.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Remarks.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Memo.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Scheme_no.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Code.Text = dt.Rows[0][""].ToString();
            txt_Comm_Recieved_Debit_Curr.Text = "INR";
            //txt_Comm_Recieved_Debit_Amt.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Cust.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Cust_Name.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Cust_AcCode.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Cust_AccNo.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Exch_CCY.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Debit_Inter_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Code.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Curr.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Amt.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Cust.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Cust_Name.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Cust_AcCode.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Cust_AccNo.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Exch_Curr.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Comm_Recieved_Credit_Inter_Rate.Text = dt.Rows[0][""].ToString();

            ///////////////////// GENERAL OPRATOIN For Change branch /////////////////////////////////////////////////////////////

            //txt_Acc_Change_Value_Date.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Ref_No.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Comment.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_SectionNo.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Remarks.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Memo.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Scheme_no.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Code.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Curr.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Amt.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Cust.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Cust_Name.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Cust_AcCode.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Cust_AccNo.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Exch_CCY.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Debit_Inter_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Code.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Curr.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Amt.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Cust.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Cust_Name.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Cust_AcCode.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Cust_AcCode_Name.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Cust_AccNo.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Exch_Rate.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Exch_Curr.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_FUND.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Check_No.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Available.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_AdPrint.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Details.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Entity.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Division.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Inter_Amount.Text = dt.Rows[0][""].ToString();
            //txt_Acc_Change_Credit_Inter_Rate.Text = dt.Rows[0][""].ToString();

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
            hdnRejectReason.Value= dt.Rows[0]["Checker_Remark"].ToString();
            //////////////// SETTLEMENT(IBD,ACC)//////////////////////////
            //txt_Doc_Maturity.Text = dt.Rows[0]["Maturity_Date"].ToString();

            txtValueDate.Text = dt.Rows[0]["Settlement_Date"].ToString();
            txt_Doc_Value_Date.Text = dt.Rows[0]["Settlement_Date"].ToString();
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
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectSettlement", P_DocNo, P_Status, P_RejectReason);
        Response.Redirect("TF_IMP_Settlement_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_Settlement_Checker_View.aspx", true);
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

    public void GBaseFile()
    {
        GBaseFileCreation();
        GBaseFileCreationImportAccounting();
        if (cb_GO_Bill_Handling_Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation1();
        }
        if (cb_GO_Sundry_Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation2();
        }
        if (cb_GO_Comm_Recieved_Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation3();
        }
        if (cb_GO_Acc_Change_Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation4();
        }
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Settlement", PRefNo);
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
    public void GBaseFileCreationImportAccounting()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Settlement_Accounting", PRefNo);
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
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_IAccounting" + ".xlsx";
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

    public void CreateSwiftSFMSFile()
    {
        string FileType = "SWIFT";
        if (rdb_swift_200.Checked)
        {
            CreateSwift200(FileType);
        }
        if(rdb_swift_R42.Checked)
        {
            CreateSwiftR12(FileType);
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
                        ws.Cells["B1"].Value = "Reciver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();

                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B2"].Value = "Transation Reference Number";
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
    public void CreateSwiftR12(string FileType)
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
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_R42.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_R42.xlsx";
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