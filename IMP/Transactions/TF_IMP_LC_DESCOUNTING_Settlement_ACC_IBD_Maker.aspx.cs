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

public partial class IMP_Transactions_TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Maker : System.Web.UI.Page
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
                    Response.Redirect("TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Maker_View.aspx", true);
                }
                else
                {
                    MakeReadOnly();
                    hdnUserName.Value = Session["userName"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    hdnDocScrutiny.Value = Request.QueryString["DocScrutiny"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    txtIBDDocNo.Text = Request.QueryString["IBD_DocNo"].Trim();

                    txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //txtInterest_From.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    TF_DATA obj = new TF_DATA();
                    SqlParameter PNO = new SqlParameter("@DocNo", txtDocNo.Text);
                    SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
                    string result = "";
                    result = obj.SaveDeleteData("TF_IMP_IBD_Settlement_Check_Document_No", PNO, PIBD_DocNo);
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
                    //Get_LCDescounting_Get_Date_Diff(null, null);
                }
                txt_INT_Rate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterestAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
            }
        }
    }

    [WebMethod]
    public static string AddUpdateONLCDiscount(string _hdnUserName, string _BranchName, string _txtDocNo, string _txtIBDDocNo, string _txtValueDate,
        string _AccDocDetails_Flag,
        string _txtCustName, string _txtCommentCode,
string _txtMaturityDate, string _txtsettlCodeForCust, string _txtSettl_CodeForBank,
string _txtInterest_From, string _txtDiscount, string _txtInterest_To,
string _txt_No_Of_Days, string _txt_INT_Rate, string _txtInterestAmt,
string _txtOverinterestRate, string _txtOverNoOfDays, string _txtOverAmount,
string _txtAttn,

////-----Import Accounting Acc------------
        string _AccImpAccounting_Flag,
string _txt_DiscAmt, string _txt_IMP_ACC_ExchRate,
string _txtPrinc_matu, string _txtPrinc_lump, string _txtprinc_Contract_no, string _txt_Princ_Ex_Curr, string _txtprinc_Ex_rate, string _txtprinc_Intnl_Ex_rate,
string _txtInterest_matu, string _txtInterest_lump, string _txtInterest_Contract_no, string _txt_interest_Ex_Curr, string _txtInterest_Ex_rate, string _txtInterest_Intnl_Ex_rate,
string _txtCommission_matu, string _txtCommission_lump, string _txtCommission_Contract_no, string _txt_Commission_Ex_Curr, string _txtCommission_Ex_rate, string _txtCommission_Intnl_Ex_rate,
string _txtTheir_Commission_matu, string _txtTheir_Commission_lump, string _txtTheir_Commission_Contract_no, string _txt_Their_Commission_Ex_Curr, string _txtTheir_Commission_Ex_rate, string _txtTheir_Commission_Intnl_Ex_rate,

string _txt_CR_Code, string _txt_CR_AC_ShortName, string _txt_CR_Cust_abbr, string _txt_CR_Cust_Acc, string _txt_CR_Acceptance_Curr, string _txt_CR_Acceptance_amt, string _txt_CR_Acceptance_payer,
string _txt_CR_Interest_Curr, string _txt_CR_Interest_amt, string _txt_CR_Interest_payer,
string _txt_CR_Accept_Commission_Curr, string _txt_CR_Accept_Commission_amt, string _txt_CR_Accept_Commission_Payer,
string _txt_CR_Pay_Handle_Commission_Curr, string _txt_CR_Pay_Handle_Commission_amt, string _txt_CR_Pay_Handle_Commission_Payer,
string _txt_CR_Others_Curr, string _txt_CR_Others_amt, string _txt_CR_Others_Payer,
string _txt_CR_Their_Commission_Curr, string _txt_CR_Their_Commission_amt, string _txt_CR_Their_Commission_Payer,

string _txt_DR_Code, string _txt_DR_Cust_abbr, string _txt_DR_Cust_Acc, string _txt_DR_Cur_Acc_Curr, string _txt_DR_Cur_Acc_amt, string _txt_DR_Cur_Acc_payer,
string _txt_DR_Cur_Acc_Curr2, string _txt_DR_Cur_Acc_amt2, string _txt_DR_Cur_Acc_payer2,
string _txt_DR_Cur_Acc_Curr3, string _txt_DR_Cur_Acc_amt3, string _txt_DR_Cur_Acc_payer3,


////----------------------Genral  Operation I-------------------------------------------------------------
string _chk_GO1_Flag,
string _txt_GO1_Comment, string _txt_GO1_SectionNo, string _txt_GO1_Remarks, string _txt_GO1_Memo,
string _txt_GO1_Scheme_no, string _txt_GO1_Debit_Code, string _txt_GO1_Debit_Curr, string _txt_GO1_Debit_Amt, string _txt_GO1_Debit_Cust, string _txt_GO1_Debit_Cust_Name, string _txt_GO1_Debit_Cust_AcCode,
string _txt_GO1_Debit_Cust_AccNo, string _txt_GO1_Debit_Cust_AcCode_Name, string _txt_GO1_Debit_Exch_Rate, string _txt_GO1_Debit_Exch_CCY, string _txt_GO1_Debit_FUND, string _txt_GO1_Debit_Check_No,
string _txt_GO1_Debit_Available, string _txt_GO1_Debit_AdPrint, string _txt_GO1_Debit_Details, string _txt_GO1_Debit_Entity, string _txt_GO1_Debit_Division,
string _txt_GO1_Debit_Inter_Amount, string _txt_GO1_Debit_Inter_Rate, string _txt_GO1_Credit_Code, string _txt_GO1_Credit_Curr, string _txt_GO1_Credit_Amt,
string _txt_GO1_Credit_Cust, string _txt_GO1_Credit_Cust_Name, string _txt_GO1_Credit_Cust_AcCode, string _txt_GO1_Credit_Cust_AcCode_Name, string _txt_GO1_Credit_Cust_AccNo, string _txt_GO1_Credit_Exch_Rate, string _txt_GO1_Credit_Exch_Curr,
string _txt_GO1_Credit_FUND, string _txt_GO1_Credit_Check_No, string _txt_GO1_Credit_Available, string _txt_GO1_Credit_AdPrint, string _txt_GO1_Credit_Details,
string _txt_GO1_Credit_Entity, string _txt_GO1_Credit_Division, string _txt_GO1_Credit_Inter_Amount, string _txt_GO1_Credit_Inter_Rate,

        ////----------------------Genral  Operation II-------------------------------------------------------------
string _chk_GO2_Flag,
string _txt_GO2_Comment, string _txt_GO2_SectionNo, string _txt_GO2_Remarks, string _txt_GO2_Memo,
string _txt_GO2_Scheme_no, string _txt_GO2_Debit_Code, string _txt_GO2_Debit_Curr, string _txt_GO2_Debit_Amt, string _txt_GO2_Debit_Cust, string _txt_GO2_Debit_Cust_Name, string _txt_GO2_Debit_Cust_AcCode,
string _txt_GO2_Debit_Cust_AccNo, string _txt_GO2_Debit_Cust_AcCode_Name, string _txt_GO2_Debit_Exch_Rate, string _txt_GO2_Debit_Exch_CCY, string _txt_GO2_Debit_FUND, string _txt_GO2_Debit_Check_No,
string _txt_GO2_Debit_Available, string _txt_GO2_Debit_AdPrint, string _txt_GO2_Debit_Details, string _txt_GO2_Debit_Entity, string _txt_GO2_Debit_Division,
string _txt_GO2_Debit_Inter_Amount, string _txt_GO2_Debit_Inter_Rate, string _txt_GO2_Credit_Code, string _txt_GO2_Credit_Curr, string _txt_GO2_Credit_Amt,
string _txt_GO2_Credit_Cust, string _txt_GO2_Credit_Cust_Name, string _txt_GO2_Credit_Cust_AcCode, string _txt_GO2_Credit_Cust_AcCode_Name, string _txt_GO2_Credit_Cust_AccNo, string _txt_GO2_Credit_Exch_Rate, string _txt_GO2_Credit_Exch_Curr,
string _txt_GO2_Credit_FUND, string _txt_GO2_Credit_Check_No, string _txt_GO2_Credit_Available, string _txt_GO2_Credit_AdPrint, string _txt_GO2_Credit_Details,
string _txt_GO2_Credit_Entity, string _txt_GO2_Credit_Division, string _txt_GO2_Credit_Inter_Amount, string _txt_GO2_Credit_Inter_Rate,
        ////----------------IBD Doc details-----------------
        string _txt_IBD_CustName, string _txt_IBD_CommentCode,
string _txt_IBD_MaturityDate, string _txt_IBD_settlCodeForCust, string _txt_IBD_Settl_CodeForBank,
string _txt_IBD_Interest_From, string _txt_IBD_Discount, string _txt_IBD_Interest_To,
string _txt_IBD__No_Of_Days, string _txt_IBD__INT_Rate, string _txt_IBD_InterestAmt,
string _txt_IBD_OverinterestRate, string _txt_IBD_OverNoOfDays, string _txt_IBD_OverAmount,
string _txt_IBD_Attn,

////-----IBD Import Accounting Acc------------
string _txt_IBD_DiscAmt, string _txt_IBD_IMP_ACC_ExchRate,
string _txt_IBDPrinc_matu, string _txt_IBDPrinc_lump, string _txt_IBDprinc_Contract_no, string _txt_IBD_Princ_Ex_Curr, string _txt_IBDprinc_Ex_rate, string _txt_IBDprinc_Intnl_Ex_rate,
string _txt_IBDInterest_matu, string _txt_IBDInterest_lump, string _txt_IBDInterest_Contract_no, string _txt_IBD_interest_Ex_Curr, string _txt_IBDInterest_Ex_rate, string _txt_IBDInterest_Intnl_Ex_rate,
string _txt_IBDCommission_matu, string _txt_IBDCommission_lump, string _txt_IBDCommission_Contract_no, string _txt_IBD_Commission_Ex_Curr, string _txt_IBDCommission_Ex_rate, string _txt_IBDCommission_Intnl_Ex_rate,
string _txt_IBDTheir_Commission_matu, string _txt_IBDTheir_Commission_lump, string _txt_IBDTheir_Commission_Contract_no, string _txt_IBD_Their_Commission_Ex_Curr, string _txt_IBDTheir_Commission_Ex_rate, string _txt_IBDTheir_Commission_Intnl_Ex_rate,
string _txt_IBD_CR_Code, string _txt_IBD_CR_AC_ShortName, string _txt_IBD_CR_Cust_abbr, string _txt_IBD_CR_Cust_Acc, string _txt_IBD_CR_Acceptance_Curr, string _txt_IBD_CR_Acceptance_amt, string _txt_IBD_CR_Acceptance_payer,
string _txt_IBD_CR_Interest_Curr, string _txt_IBD_CR_Interest_amt, string _txt_IBD_CR_Interest_payer,
string _txt_IBD_CR_Accept_Commission_Curr, string _txt_IBD_CR_Accept_Commission_amt, string _txt_IBD_CR_Accept_Commission_Payer,
string _txt_IBD_CR_Pay_Handle_Commission_Curr, string _txt_IBD_CR_Pay_Handle_Commission_amt, string _txt_IBD_CR_Pay_Handle_Commission_Payer,
string _txt_IBD_CR_Others_Curr, string _txt_IBD_CR_Others_amt, string _txt_IBD_CR_Others_Payer,
string _txt_IBD_CR_Their_Commission_Curr, string _txt_IBD_CR_Their_Commission_amt, string _txt_IBD_CR_Their_Commission_Payer,
string _txt_IBD_IBD_DR_Code, string _txt_IBD_IBD_DR_Cust_abbr, string _txt_IBD_IBD_DR_Cust_Acc, string _txt_IBD_IBD_DR_Cur_Acc_Curr, string _txt_IBD_IBD_DR_Cur_Acc_amt, string _txt_IBD_IBD_DR_Cur_Acc_payer,

string _txt_IBD_DR_Code, string _txt_IBD_DR_Cust_abbr, string _txt_IBD_DR_Cust_Acc, string _txt_IBD_DR_Cur_Acc_Curr, string _txt_IBD_DR_Cur_Acc_amt, string _txt_IBD_DR_Cur_Acc_payer,
string _txt_IBD_DR_Cur_Acc_Curr2, string _txt_IBD_DR_Cur_Acc_amt2, string _txt_IBD_DR_Cur_Acc_payer2,
string _txt_IBD_DR_Cur_Acc_Curr3, string _txt_IBD_DR_Cur_Acc_amt3, string _txt_IBD_DR_Cur_Acc_payer3,
        string _IBDExtn_Flag
        )
    {
        TF_DATA obj = new TF_DATA();

        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName.ToUpper());
        SqlParameter P_Document_No = new SqlParameter("@Document_No", _txtDocNo.ToUpper());
        SqlParameter P_IBD_Document_No = new SqlParameter("@IBD_Document_No", _txtIBDDocNo.ToUpper());
        SqlParameter P_Settlement_Date = new SqlParameter("@Settlement_Date", _txtValueDate.ToUpper());

        SqlParameter P_AccDocDetails_Flag = new SqlParameter("@AccDocDetails_Flag", _AccDocDetails_Flag);
        SqlParameter P_CustomerName = new SqlParameter("@CustomerName", _txtCustName.ToUpper());
        SqlParameter P_Comment_Code = new SqlParameter("@Comment_Code", _txtCommentCode.ToUpper());
        SqlParameter P_Maturity_Date = new SqlParameter("@Maturity_Date", _txtMaturityDate.ToUpper());
        SqlParameter P_Settlement_For_Cust_Code = new SqlParameter("@Settlement_For_Cust_Code", _txtsettlCodeForCust.ToUpper());
        SqlParameter P_Settlement_For_Bank_Code = new SqlParameter("@Settlement_For_Bank_Code", _txtSettl_CodeForBank.ToUpper());
        SqlParameter P_Interest_From_Date = new SqlParameter("@Interest_From_Date", _txtInterest_From.ToUpper());
        SqlParameter P_Discount_Flag = new SqlParameter("@Discount_Flag", _txtDiscount.ToUpper());
        SqlParameter P_Interest_To_Date = new SqlParameter("@Interest_To_Date", _txtInterest_To.ToUpper());
        SqlParameter P_No_Of_Days = new SqlParameter("@No_Of_Days", _txt_No_Of_Days.ToUpper());
        SqlParameter P_Interest_Rate = new SqlParameter("@Interest_Rate", _txt_INT_Rate.ToUpper());
        SqlParameter P_Interest_Amount = new SqlParameter("@Interest_Amount", _txtInterestAmt.ToUpper());
        SqlParameter P_Overdue_Interest_Rate = new SqlParameter("@Overdue_Interest_Rate", _txtOverinterestRate.ToUpper());
        SqlParameter P_Overdue_No_Of_Days = new SqlParameter("@Overdue_No_Of_Days", _txtOverNoOfDays.ToUpper());
        SqlParameter P_Overdue_Interest_Amount = new SqlParameter("@Overdue_Interest_Amount", _txtOverAmount.ToUpper());
        SqlParameter P_Attn = new SqlParameter("Attn", _txtAttn.ToUpper());

        ////------------Import Accounting Acc------------
        SqlParameter P_AccImpAccounting_Flag = new SqlParameter("@AccImpAccounting_Flag", _AccImpAccounting_Flag);
        SqlParameter P_IMP_ACC_Amount = new SqlParameter("@IMP_ACC_Amount", _txt_DiscAmt.ToUpper());
        SqlParameter P_IMP_ACC_ExchRate = new SqlParameter("@IMP_ACC_ExchRate", _txt_IMP_ACC_ExchRate.ToUpper());
        SqlParameter P_Principal_MATU = new SqlParameter("@Principal_MATU", _txtPrinc_matu.ToUpper());
        SqlParameter P_Principal_LUMP = new SqlParameter("@Principal_LUMP", _txtPrinc_lump.ToUpper());
        SqlParameter P_Principal_Contract_No = new SqlParameter("@Principal_Contract_No", _txtprinc_Contract_no.ToUpper());
        SqlParameter P_Principal_Ex_Curr = new SqlParameter("@Principal_Ex_Curr", _txt_Princ_Ex_Curr.ToUpper());
        SqlParameter P_Principal_Exch_Rate = new SqlParameter("@Principal_Exch_Rate", _txtprinc_Ex_rate.ToUpper());
        SqlParameter P_Principal_Intnl_Exch_Rate = new SqlParameter("@Principal_Intnl_Exch_Rate", _txtprinc_Intnl_Ex_rate.ToUpper());

        SqlParameter P_Interest_MATU = new SqlParameter("@Interest_MATU", _txtInterest_matu.ToUpper());
        SqlParameter P_Interest_LUMP = new SqlParameter("@Interest_LUMP", _txtInterest_lump.ToUpper());
        SqlParameter P_Interest_Contract_No = new SqlParameter("@Interest_Contract_No", _txtInterest_Contract_no.ToUpper());
        SqlParameter P_Interest_Ex_Curr = new SqlParameter("@Interest_Ex_Curr", _txt_interest_Ex_Curr.ToUpper());
        SqlParameter P_Interest_Exch_Rate = new SqlParameter("@Interest_Exch_Rate", _txtInterest_Ex_rate.ToUpper());
        SqlParameter P_Interest_Intnl_Exch_Rate = new SqlParameter("@Interest_Intnl_Exch_Rate", _txtInterest_Intnl_Ex_rate.ToUpper());

        SqlParameter P_Commission_MATU = new SqlParameter("@Commission_MATU", _txtCommission_matu.ToUpper());
        SqlParameter P_Commission_LUMP = new SqlParameter("@Commission_LUMP", _txtCommission_lump.ToUpper());
        SqlParameter P_Commission_Contract_No = new SqlParameter("@Commission_Contract_No", _txtCommission_Contract_no.ToUpper());
        SqlParameter P_Commission_Ex_Curr = new SqlParameter("@Commission_Ex_Curr", _txt_Commission_Ex_Curr.ToUpper());
        SqlParameter P_Commission_Exch_Rate = new SqlParameter("@Commission_Exch_Rate", _txtCommission_Ex_rate.ToUpper());
        SqlParameter P_Commission_Intnl_Exch_Rate = new SqlParameter("@Commission_Intnl_Exch_Rate", _txtCommission_Intnl_Ex_rate.ToUpper());

        SqlParameter P_Their_Commission_MATU = new SqlParameter("@Their_Commission_MATU", _txtTheir_Commission_matu.ToUpper());
        SqlParameter P_Their_Commission_LUMP = new SqlParameter("@Their_Commission_LUMP", _txtTheir_Commission_lump.ToUpper());
        SqlParameter P_Their_Commission_Contract_No = new SqlParameter("@Their_Commission_Contract_No", _txtTheir_Commission_Contract_no.ToUpper());
        SqlParameter P_Their_Commission_Ex_Curr = new SqlParameter("@Their_Commission_Ex_Curr", _txt_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_Their_Commission_Exch_Rate = new SqlParameter("@Their_Commission_Exch_Rate", _txtTheir_Commission_Ex_rate.ToUpper());
        SqlParameter P_Their_Commission_Intnl_Exch_Rate = new SqlParameter("@Their_Commission_Intnl_Exch_Rate", _txtTheir_Commission_Intnl_Ex_rate.ToUpper());

        SqlParameter P_CR_Code = new SqlParameter("@CR_Code", _txt_CR_Code.ToUpper());
        SqlParameter P_CR_Cust_ACC_Name = new SqlParameter("@CR_Cust_ACC_Name", _txt_CR_AC_ShortName.ToUpper());
        SqlParameter P_CR_Cust_Abbr = new SqlParameter("@CR_Cust_Abbr", _txt_CR_Cust_abbr.ToUpper());
        SqlParameter P_CR_Cust_Acc_No = new SqlParameter("@CR_Cust_Acc_No", _txt_CR_Cust_Acc.ToUpper());
        SqlParameter P_CR_Acceptance_Curr = new SqlParameter("@CR_Acceptance_Curr", _txt_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_CR_Acceptance_Amount = new SqlParameter("@CR_Acceptance_Amount", _txt_CR_Acceptance_amt.ToUpper());
        SqlParameter P_CR_Acceptance_Payer = new SqlParameter("@CR_Acceptance_Payer", _txt_CR_Acceptance_payer.ToUpper());

        SqlParameter P_CR_Interest_Curr = new SqlParameter("@CR_Interest_Curr", _txt_CR_Interest_Curr.ToUpper());
        SqlParameter P_CR_Interest_Amount = new SqlParameter("@CR_Interest_Amount", _txt_CR_Interest_amt.ToUpper());
        SqlParameter P_CR_Interest_Payer = new SqlParameter("@CR_Interest_Payer", _txt_CR_Interest_payer.ToString());

        SqlParameter P_CR_Acceptance_Comm_Curr = new SqlParameter("@CR_Acceptance_Comm_Curr", _txt_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_CR_Acceptance_Comm_Amount = new SqlParameter("@CR_Acceptance_Comm_Amount", _txt_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_CR_Acceptance_Comm_Payer = new SqlParameter("@CR_Acceptance_Comm_Payer", _txt_CR_Accept_Commission_Payer.ToString());

        SqlParameter P_CR_Pay_Handle_Comm_Curr = new SqlParameter("@CR_Pay_Handle_Comm_Curr", _txt_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_CR_Pay_Handle_Comm_Amount = new SqlParameter("@CR_Pay_Handle_Comm_Amount", _txt_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_CR_Pay_Handle_Comm_Payer = new SqlParameter("@CR_Pay_Handle_Comm_Payer", _txt_CR_Pay_Handle_Commission_Payer.ToUpper());

        SqlParameter P_CR_Others_Curr = new SqlParameter("@CR_Others_Curr", _txt_CR_Others_Curr.ToUpper());
        SqlParameter P_CR_Others_Amount = new SqlParameter("@CR_Others_Amount", _txt_CR_Others_amt.ToUpper());
        SqlParameter P_CR_Others_Payer = new SqlParameter("@CR_Others_Payer", _txt_CR_Others_Payer.ToUpper());

        SqlParameter P_CR_Their_Comm_Curr = new SqlParameter("@CR_Their_Comm_Curr", _txt_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_CR_Their_Comm_Amount = new SqlParameter("@CR_Their_Comm_Amount", _txt_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_CR_Their_Comm_Payer = new SqlParameter("@CR_Their_Comm_Payer", _txt_CR_Their_Commission_Payer.ToUpper());

        SqlParameter P_DR_CODE = new SqlParameter("@DR_CODE", _txt_DR_Code.ToUpper());
        SqlParameter P_DR_Cust_abbr = new SqlParameter("@DR_Cust_abbr", _txt_DR_Cust_abbr.ToUpper());
        SqlParameter P_DR_Cust_Acc = new SqlParameter("@DR_Cust_Acc", _txt_DR_Cust_Acc.ToUpper());
        SqlParameter P_DR_Current_Acc_Curr = new SqlParameter("@DR_Current_Acc_Curr", _txt_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_DR_Current_Acc_Amount = new SqlParameter("@DR_Current_Acc_Amount", _txt_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_DR_Current_Acc_Payer = new SqlParameter("@DR_Current_Acc_Payer", _txt_DR_Cur_Acc_payer.ToUpper());

        SqlParameter P_DR_Current_Acc_Curr2 = new SqlParameter("@DR_Current_Acc_Curr2", _txt_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_DR_Current_Acc_Amount2 = new SqlParameter("@DR_Current_Acc_Amount2", _txt_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_DR_Current_Acc_Payer2 = new SqlParameter("@DR_Current_Acc_Payer2", _txt_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_DR_Current_Acc_Curr3 = new SqlParameter("@DR_Current_Acc_Curr3", _txt_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_DR_Current_Acc_Amount3 = new SqlParameter("@DR_Current_Acc_Amount3", _txt_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_DR_Current_Acc_Payer3 = new SqlParameter("@DR_Current_Acc_Payer3", _txt_DR_Cur_Acc_payer3.ToUpper());

        /////GENERAL OPRATION 1
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
        SqlParameter P_txt_GO1_Debit_Cust_Name = new SqlParameter("@GO_Bill_Handling_Debit_Cust_Name", _txt_GO1_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO1_Debit_Cust_AcCode = new SqlParameter("@GO_Bill_Handling_Debit_Cust_AccCode", _txt_GO1_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO1_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Bill_Handling_Debit_Cust_AccCode_Disc", _txt_GO1_Debit_Cust_AcCode_Name.ToUpper());
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
        SqlParameter P_txt_GO1_Credit_Cust_Name = new SqlParameter("@GO_Bill_Handling_Credit_Cust_Name", _txt_GO1_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO1_Credit_Cust_AcCode = new SqlParameter("@GO_Bill_Handling_Credit_Cust_AccCode", _txt_GO1_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO1_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Bill_Handling_Credit_Cust_AccCode_Disc", _txt_GO1_Credit_Cust_AcCode_Name.ToUpper());
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

        /////GENERAL OPRATION 2
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
        SqlParameter P_txt_GO2_Debit_Cust_Name = new SqlParameter("@GO2_Bill_Handling_Debit_Cust_Name", _txt_GO2_Debit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO2_Debit_Cust_AcCode = new SqlParameter("@GO2_Bill_Handling_Debit_Cust_AccCode", _txt_GO2_Debit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO2_Debit_Cust_AcCode_Name = new SqlParameter("@GO2_Bill_Handling_Debit_Cust_AccCode_Disc", _txt_GO2_Debit_Cust_AcCode_Name.ToUpper());
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
        SqlParameter P_txt_GO2_Credit_Cust_Name = new SqlParameter("@GO2_Bill_Handling_Credit_Cust_Name", _txt_GO2_Credit_Cust_Name.ToUpper());
        SqlParameter P_txt_GO2_Credit_Cust_AcCode = new SqlParameter("@GO2_Bill_Handling_Credit_Cust_AccCode", _txt_GO2_Credit_Cust_AcCode.ToUpper());
        SqlParameter P_txt_GO2_Credit_Cust_AcCode_Name = new SqlParameter("@GO2_Bill_Handling_Credit_Cust_AccCode_Disc", _txt_GO2_Credit_Cust_AcCode_Name.ToUpper());
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

        ///////--------IBD Doc Details-----------

        SqlParameter P_IBD_CustomerName = new SqlParameter("@IBD_CustomerName", _txt_IBD_CustName.ToUpper());
        SqlParameter P_IBD_Comment_Code = new SqlParameter("@IBD_Comment_Code", _txt_IBD_CommentCode.ToUpper());
        SqlParameter P_IBD_Maturity_Date = new SqlParameter("@IBD_Maturity_Date", _txt_IBD_MaturityDate.ToUpper());
        SqlParameter P_IBD_Settlement_For_Cust_Code = new SqlParameter("@IBD_Settlement_For_Cust_Code", _txt_IBD_settlCodeForCust.ToUpper());
        SqlParameter P_IBD_Settlement_For_Bank_Code = new SqlParameter("@IBD_Settlement_For_Bank_Code", _txt_IBD_Settl_CodeForBank.ToUpper());
        SqlParameter P_IBD_Interest_From_Date = new SqlParameter("@IBD_Interest_From_Date", _txt_IBD_Interest_From.ToUpper());
        SqlParameter P_IBD_Discount_Flag = new SqlParameter("@IBD_Discount_Flag", _txt_IBD_Discount.ToUpper());
        SqlParameter P_IBD_Interest_To_Date = new SqlParameter("@IBD_Interest_To_Date", _txt_IBD_Interest_To.ToUpper());
        SqlParameter P_IBD_No_Of_Days = new SqlParameter("@IBD_No_Of_Days", _txt_IBD__No_Of_Days.ToUpper());
        SqlParameter P_IBD_Interest_Rate = new SqlParameter("@IBD_Interest_Rate", _txt_IBD__INT_Rate.ToUpper());
        SqlParameter P_IBD_Interest_Amount = new SqlParameter("@IBD_Interest_Amount", _txt_IBD_InterestAmt.ToUpper());
        SqlParameter P_IBD_Overdue_Interest_Rate = new SqlParameter("@IBD_Overdue_Interest_Rate", _txt_IBD_OverinterestRate.ToUpper());
        SqlParameter P_IBD_Overdue_No_Of_Days = new SqlParameter("@IBD_Overdue_No_Of_Days", _txt_IBD_OverNoOfDays.ToUpper());
        SqlParameter P_IBD_Overdue_Interest_Amount = new SqlParameter("@IBD_Overdue_Interest_Amount", _txt_IBD_OverAmount.ToUpper());
        SqlParameter P_IBD_Attn = new SqlParameter("@IBD_Attn", _txt_IBD_Attn.ToUpper());

        ////------------IBD Import Accounting Acc------------
        SqlParameter P_IBD_IMP_IBD_ACC_Amount = new SqlParameter("@IBD_IMP_ACC_Amount", _txt_IBD_DiscAmt.ToUpper());
        SqlParameter P_IBD_IMP_IBD_ACC_ExchRate = new SqlParameter("@IBD_IMP_ACC_ExchRate", _txt_IBD_IMP_ACC_ExchRate.ToUpper());
        SqlParameter P_IBD_Principal_MATU = new SqlParameter("@IBD_Principal_MATU", _txt_IBDPrinc_matu.ToUpper());
        SqlParameter P_IBD_Principal_LUMP = new SqlParameter("@IBD_Principal_LUMP", _txt_IBDPrinc_lump.ToUpper());
        SqlParameter P_IBD_Principal_Contract_No = new SqlParameter("@IBD_Principal_Contract_No", _txt_IBDprinc_Contract_no.ToUpper());
        SqlParameter P_IBD_Principal_Ex_Curr = new SqlParameter("@IBD_Principal_Ex_Curr", _txt_IBD_Princ_Ex_Curr.ToUpper());
        SqlParameter P_IBD_Principal_Exch_Rate = new SqlParameter("@IBD_Principal_Exch_Rate", _txt_IBDprinc_Ex_rate.ToUpper());
        SqlParameter P_IBD_Principal_Intnl_Exch_Rate = new SqlParameter("@IBD_Principal_Intnl_Exch_Rate", _txt_IBDprinc_Intnl_Ex_rate.ToUpper());

        SqlParameter P_IBD_Interest_MATU = new SqlParameter("@IBD_Interest_MATU", _txt_IBDInterest_matu.ToUpper());
        SqlParameter P_IBD_Interest_LUMP = new SqlParameter("@IBD_Interest_LUMP", _txt_IBDInterest_lump.ToUpper());
        SqlParameter P_IBD_Interest_Contract_No = new SqlParameter("@IBD_Interest_Contract_No", _txt_IBDInterest_Contract_no.ToUpper());
        SqlParameter P_IBD_Interest_Ex_Curr = new SqlParameter("@IBD_Interest_Ex_Curr", _txt_IBD_interest_Ex_Curr.ToUpper());
        SqlParameter P_IBD_Interest_Exch_Rate = new SqlParameter("@IBD_Interest_Exch_Rate", _txt_IBDInterest_Ex_rate.ToUpper());
        SqlParameter P_IBD_Interest_Intnl_Exch_Rate = new SqlParameter("@IBD_Interest_Intnl_Exch_Rate", _txt_IBDInterest_Intnl_Ex_rate.ToUpper());

        SqlParameter P_IBD_Commission_MATU = new SqlParameter("@IBD_Commission_MATU", _txt_IBDCommission_matu.ToUpper());
        SqlParameter P_IBD_Commission_LUMP = new SqlParameter("@IBD_Commission_LUMP", _txt_IBDCommission_lump.ToUpper());
        SqlParameter P_IBD_Commission_Contract_No = new SqlParameter("@IBD_Commission_Contract_No", _txt_IBDCommission_Contract_no.ToUpper());
        SqlParameter P_IBD_Commission_Ex_Curr = new SqlParameter("@IBD_Commission_Ex_Curr", _txt_IBD_Commission_Ex_Curr.ToUpper());
        SqlParameter P_IBD_Commission_Exch_Rate = new SqlParameter("@IBD_Commission_Exch_Rate", _txt_IBDCommission_Ex_rate.ToUpper());
        SqlParameter P_IBD_Commission_Intnl_Exch_Rate = new SqlParameter("@IBD_Commission_Intnl_Exch_Rate", _txt_IBDCommission_Intnl_Ex_rate.ToUpper());

        SqlParameter P_IBD_Their_Commission_MATU = new SqlParameter("@IBD_Their_Commission_MATU", _txt_IBDTheir_Commission_matu.ToUpper());
        SqlParameter P_IBD_Their_Commission_LUMP = new SqlParameter("@IBD_Their_Commission_LUMP", _txt_IBDTheir_Commission_lump.ToUpper());
        SqlParameter P_IBD_Their_Commission_Contract_No = new SqlParameter("@IBD_Their_Commission_Contract_No", _txt_IBDTheir_Commission_Contract_no.ToUpper());
        SqlParameter P_IBD_Their_Commission_Ex_Curr = new SqlParameter("@IBD_Their_Commission_Ex_Curr", _txt_IBD_Their_Commission_Ex_Curr.ToUpper());
        SqlParameter P_IBD_Their_Commission_Exch_Rate = new SqlParameter("@IBD_Their_Commission_Exch_Rate", _txt_IBDTheir_Commission_Ex_rate.ToUpper());
        SqlParameter P_IBD_Their_Commission_Intnl_Exch_Rate = new SqlParameter("@IBD_Their_Commission_Intnl_Exch_Rate", _txt_IBDTheir_Commission_Intnl_Ex_rate.ToUpper());

        SqlParameter P_IBD_CR_Code = new SqlParameter("@IBD_CR_Code", _txt_IBD_CR_Code.ToUpper());
        SqlParameter P_IBD_CR_Cust_ACC_Name = new SqlParameter("@IBD_CR_Cust_ACC_Name", _txt_IBD_CR_AC_ShortName.ToUpper());
        SqlParameter P_IBD_CR_Cust_Abbr = new SqlParameter("@IBD_CR_Cust_Abbr", _txt_IBD_CR_Cust_abbr.ToUpper());
        SqlParameter P_IBD_CR_Cust_Acc_No = new SqlParameter("@IBD_CR_Cust_Acc_No", _txt_IBD_CR_Cust_Acc.ToUpper());
        SqlParameter P_IBD_CR_Acceptance_Curr = new SqlParameter("@IBD_CR_Acceptance_Curr", _txt_IBD_CR_Acceptance_Curr.ToUpper());
        SqlParameter P_IBD_CR_Acceptance_Amount = new SqlParameter("@IBD_CR_Acceptance_Amount", _txt_IBD_CR_Acceptance_amt.ToUpper());
        SqlParameter P_IBD_CR_Acceptance_Payer = new SqlParameter("@IBD_CR_Acceptance_Payer", _txt_IBD_CR_Acceptance_payer.ToUpper());

        SqlParameter P_IBD_CR_Interest_Curr = new SqlParameter("@IBD_CR_Interest_Curr", _txt_IBD_CR_Interest_Curr.ToUpper());
        SqlParameter P_IBD_CR_Interest_Amount = new SqlParameter("@IBD_CR_Interest_Amount", _txt_IBD_CR_Interest_amt.ToUpper());
        SqlParameter P_IBD_CR_Interest_Payer = new SqlParameter("@IBD_CR_Interest_Payer", _txt_IBD_CR_Interest_payer.ToString());

        SqlParameter P_IBD_CR_Acceptance_Comm_Curr = new SqlParameter("@IBD_CR_Acceptance_Comm_Curr", _txt_IBD_CR_Accept_Commission_Curr.ToUpper());
        SqlParameter P_IBD_CR_Acceptance_Comm_Amount = new SqlParameter("@IBD_CR_Acceptance_Comm_Amount", _txt_IBD_CR_Accept_Commission_amt.ToUpper());
        SqlParameter P_IBD_CR_Acceptance_Comm_Payer = new SqlParameter("@IBD_CR_Acceptance_Comm_Payer", _txt_IBD_CR_Accept_Commission_Payer.ToString());

        SqlParameter P_IBD_CR_Pay_Handle_Comm_Curr = new SqlParameter("@IBD_CR_Pay_Handle_Comm_Curr", _txt_IBD_CR_Pay_Handle_Commission_Curr.ToUpper());
        SqlParameter P_IBD_CR_Pay_Handle_Comm_Amount = new SqlParameter("@IBD_CR_Pay_Handle_Comm_Amount", _txt_IBD_CR_Pay_Handle_Commission_amt.ToUpper());
        SqlParameter P_IBD_CR_Pay_Handle_Comm_Payer = new SqlParameter("@IBD_CR_Pay_Handle_Comm_Payer", _txt_IBD_CR_Pay_Handle_Commission_Payer.ToUpper());

        SqlParameter P_IBD_CR_Others_Curr = new SqlParameter("@IBD_CR_Others_Curr", _txt_IBD_CR_Others_Curr.ToUpper());
        SqlParameter P_IBD_CR_Others_Amount = new SqlParameter("@IBD_CR_Others_Amount", _txt_IBD_CR_Others_amt.ToUpper());
        SqlParameter P_IBD_CR_Others_Payer = new SqlParameter("@IBD_CR_Others_Payer", _txt_IBD_CR_Others_Payer.ToUpper());

        SqlParameter P_IBD_CR_Their_Comm_Curr = new SqlParameter("@IBD_CR_Their_Comm_Curr", _txt_IBD_CR_Their_Commission_Curr.ToUpper());
        SqlParameter P_IBD_CR_Their_Comm_Amount = new SqlParameter("@IBD_CR_Their_Comm_Amount", _txt_IBD_CR_Their_Commission_amt.ToUpper());
        SqlParameter P_IBD_CR_Their_Comm_Payer = new SqlParameter("@IBD_CR_Their_Comm_Payer", _txt_IBD_CR_Their_Commission_Payer.ToUpper());

        SqlParameter P_IBD_DR_IBDCode = new SqlParameter("@IBD_DR_IBDCode", _txt_IBD_IBD_DR_Code.ToUpper());
        SqlParameter P_IBD_DR_IBDCust_abbr = new SqlParameter("@IBD_DR_IBDCust_abbr", _txt_IBD_IBD_DR_Cust_abbr.ToUpper());
        SqlParameter P_IBD_DR_IBDCust_Acc = new SqlParameter("@IBD_DR_IBDCust_Acc", _txt_IBD_IBD_DR_Cust_Acc.ToUpper());
        SqlParameter P_IBD_DR_IBDCur_Acc_Curr = new SqlParameter("@IBD_DR_IBDCur_Acc_Curr", _txt_IBD_IBD_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_IBD_DR_IBDCur_Acc_amt = new SqlParameter("@IBD_DR_IBDCur_Acc_amt", _txt_IBD_IBD_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_IBD_DR_IBDCur_Acc_Payr = new SqlParameter("@IBD_DR_IBDCur_Acc_Payr", _txt_IBD_IBD_DR_Cur_Acc_payer.ToUpper());

        SqlParameter P_IBD_DR_CODE = new SqlParameter("@IBD_DR_CODE", _txt_IBD_DR_Code.ToUpper());
        SqlParameter P_IBD_DR_Cust_abbr = new SqlParameter("@IBD_DR_Cust_abbr", _txt_IBD_DR_Cust_abbr.ToUpper());
        SqlParameter P_IBD_DR_Cust_Acc = new SqlParameter("@IBD_DR_Cust_Acc", _txt_IBD_DR_Cust_Acc.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Curr = new SqlParameter("@IBD_DR_Current_Acc_Curr", _txt_IBD_DR_Cur_Acc_Curr.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Amount = new SqlParameter("@IBD_DR_Current_Acc_Amount", _txt_IBD_DR_Cur_Acc_amt.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Payer = new SqlParameter("@IBD_DR_Current_Acc_Payer", _txt_IBD_DR_Cur_Acc_payer.ToUpper());

        SqlParameter P_IBD_DR_Current_Acc_Curr2 = new SqlParameter("@IBD_DR_Current_Acc_Curr2", _txt_IBD_DR_Cur_Acc_Curr2.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Amount2 = new SqlParameter("@IBD_DR_Current_Acc_Amount2", _txt_IBD_DR_Cur_Acc_amt2.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Payer2 = new SqlParameter("@IBD_DR_Current_Acc_Payer2", _txt_IBD_DR_Cur_Acc_payer2.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Curr3 = new SqlParameter("@IBD_DR_Current_Acc_Curr3", _txt_IBD_DR_Cur_Acc_Curr3.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Amount3 = new SqlParameter("@IBD_DR_Current_Acc_Amount3", _txt_IBD_DR_Cur_Acc_amt3.ToUpper());
        SqlParameter P_IBD_DR_Current_Acc_Payer3 = new SqlParameter("@IBD_DR_Current_Acc_Payer3", _txt_IBD_DR_Cur_Acc_payer3.ToUpper());
        SqlParameter P_IBD_Extn_Flag = new SqlParameter("@IBD_Extn_Flag", _IBDExtn_Flag);

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", _hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", _hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        string _Result = obj.SaveDeleteData("TF_IMP_IBD_Settlement_AddUpdate", P_BranchName, P_Document_No, P_IBD_Document_No, P_Settlement_Date,

            P_AccDocDetails_Flag,
        P_CustomerName,
        P_Comment_Code, P_Maturity_Date,
        P_Settlement_For_Cust_Code, P_Settlement_For_Bank_Code,
        P_Interest_From_Date, P_Discount_Flag, P_Interest_To_Date,
        P_No_Of_Days, P_Interest_Rate, P_Interest_Amount,
        P_Overdue_Interest_Rate, P_Overdue_No_Of_Days, P_Overdue_Interest_Amount,
        P_Attn,

        ////---Import Accounting -----
        P_AccImpAccounting_Flag,
        P_IMP_ACC_Amount, P_IMP_ACC_ExchRate,
        P_Principal_MATU, P_Principal_LUMP, P_Principal_Contract_No, P_Principal_Ex_Curr, P_Principal_Exch_Rate, P_Principal_Intnl_Exch_Rate,
        P_Interest_MATU, P_Interest_LUMP, P_Interest_Contract_No, P_Interest_Ex_Curr, P_Interest_Exch_Rate, P_Interest_Intnl_Exch_Rate,
        P_Commission_MATU, P_Commission_LUMP, P_Commission_Contract_No, P_Commission_Ex_Curr, P_Commission_Exch_Rate, P_Commission_Intnl_Exch_Rate,
        P_Their_Commission_MATU, P_Their_Commission_LUMP, P_Their_Commission_Contract_No, P_Their_Commission_Ex_Curr, P_Their_Commission_Exch_Rate, P_Their_Commission_Intnl_Exch_Rate,

        P_CR_Code, P_CR_Cust_ACC_Name, P_CR_Cust_Abbr, P_CR_Cust_Acc_No, P_CR_Acceptance_Curr, P_CR_Acceptance_Amount, P_CR_Acceptance_Payer,
        P_CR_Interest_Curr, P_CR_Interest_Amount, P_CR_Interest_Payer,
        P_CR_Acceptance_Comm_Curr, P_CR_Acceptance_Comm_Amount, P_CR_Acceptance_Comm_Payer,
        P_CR_Pay_Handle_Comm_Curr, P_CR_Pay_Handle_Comm_Amount, P_CR_Pay_Handle_Comm_Payer,
        P_CR_Others_Curr, P_CR_Others_Amount, P_CR_Others_Payer,
        P_CR_Their_Comm_Curr, P_CR_Their_Comm_Amount, P_CR_Their_Comm_Payer,

        P_DR_CODE, P_DR_Cust_abbr, P_DR_Cust_Acc, P_DR_Current_Acc_Curr, P_DR_Current_Acc_Amount, P_DR_Current_Acc_Payer,
        P_DR_Current_Acc_Curr2, P_DR_Current_Acc_Amount2, P_DR_Current_Acc_Payer2,
        P_DR_Current_Acc_Curr3, P_DR_Current_Acc_Amount3, P_DR_Current_Acc_Payer3,

        //----------------Genral operations I----------------------------------------
                P_chk_GO1_Flag,
                P_txt_GO1_Comment, P_txt_GO1_SectionNo, P_txt_GO1_Remarks, P_txt_GO1_Memo, P_txt_GO1_Scheme_no, P_txt_GO1_Debit_Code, P_txt_GO1_Debit_Curr,
                P_txt_GO1_Debit_Amt, P_txt_GO1_Debit_Cust, P_txt_GO1_Debit_Cust_Name, P_txt_GO1_Debit_Cust_AcCode, P_txt_GO1_Debit_Cust_AcCode_Name,
                P_txt_GO1_Debit_Cust_AccNo, P_txt_GO1_Debit_Exch_Rate, P_txt_GO1_Debit_Exch_CCY, P_txt_GO1_Debit_FUND, P_txt_GO1_Debit_Check_No,
                P_txt_GO1_Debit_Available, P_txt_GO1_Debit_AdPrint, P_txt_GO1_Debit_Details, P_txt_GO1_Debit_Entity, P_txt_GO1_Debit_Division, P_txt_GO1_Debit_Inter_Amount,
                P_txt_GO1_Debit_Inter_Rate, P_txt_GO1_Credit_Code, P_txt_GO1_Credit_Curr, P_txt_GO1_Credit_Amt, P_txt_GO1_Credit_Cust, P_txt_GO1_Credit_Cust_Name,
                P_txt_GO1_Credit_Cust_AcCode, P_txt_GO1_Credit_Cust_AcCode_Name, P_txt_GO1_Credit_Cust_AccNo, P_txt_GO1_Credit_Exch_Rate, P_txt_GO1_Credit_Exch_Curr,
                P_txt_GO1_Credit_FUND, P_txt_GO1_Credit_Check_No, P_txt_GO1_Credit_Available, P_txt_GO1_Credit_AdPrint, P_txt_GO1_Credit_Details, P_txt_GO1_Credit_Entity,
                P_txt_GO1_Credit_Division, P_txt_GO1_Credit_Inter_Amount, P_txt_GO1_Credit_Inter_Rate,

            /////-----IBD Doc Details
                P_IBD_CustomerName,
        P_IBD_Comment_Code, P_IBD_Maturity_Date,
        P_IBD_Settlement_For_Cust_Code, P_IBD_Settlement_For_Bank_Code,
        P_IBD_Interest_From_Date, P_IBD_Discount_Flag, P_IBD_Interest_To_Date,
        P_IBD_No_Of_Days, P_IBD_Interest_Rate, P_IBD_Interest_Amount,
        P_IBD_Overdue_Interest_Rate, P_IBD_Overdue_No_Of_Days, P_IBD_Overdue_Interest_Amount,
        P_IBD_Attn,

         //----------------Genral operations II----------------------------------------
                P_chk_GO2_Flag,
                P_txt_GO2_Comment, P_txt_GO2_SectionNo, P_txt_GO2_Remarks, P_txt_GO2_Memo, P_txt_GO2_Scheme_no, P_txt_GO2_Debit_Code, P_txt_GO2_Debit_Curr,
                P_txt_GO2_Debit_Amt, P_txt_GO2_Debit_Cust, P_txt_GO2_Debit_Cust_Name, P_txt_GO2_Debit_Cust_AcCode, P_txt_GO2_Debit_Cust_AcCode_Name,
                P_txt_GO2_Debit_Cust_AccNo, P_txt_GO2_Debit_Exch_Rate, P_txt_GO2_Debit_Exch_CCY, P_txt_GO2_Debit_FUND, P_txt_GO2_Debit_Check_No,
                P_txt_GO2_Debit_Available, P_txt_GO2_Debit_AdPrint, P_txt_GO2_Debit_Details, P_txt_GO2_Debit_Entity, P_txt_GO2_Debit_Division, P_txt_GO2_Debit_Inter_Amount,
                P_txt_GO2_Debit_Inter_Rate, P_txt_GO2_Credit_Code, P_txt_GO2_Credit_Curr, P_txt_GO2_Credit_Amt, P_txt_GO2_Credit_Cust, P_txt_GO2_Credit_Cust_Name,
                P_txt_GO2_Credit_Cust_AcCode, P_txt_GO2_Credit_Cust_AcCode_Name, P_txt_GO2_Credit_Cust_AccNo, P_txt_GO2_Credit_Exch_Rate, P_txt_GO2_Credit_Exch_Curr,
                P_txt_GO2_Credit_FUND, P_txt_GO2_Credit_Check_No, P_txt_GO2_Credit_Available, P_txt_GO2_Credit_AdPrint, P_txt_GO2_Credit_Details, P_txt_GO2_Credit_Entity,
                P_txt_GO2_Credit_Division, P_txt_GO2_Credit_Inter_Amount, P_txt_GO2_Credit_Inter_Rate,

        ////---IBD Import Accounting -----
            P_IBD_IMP_IBD_ACC_Amount, P_IBD_IMP_IBD_ACC_ExchRate,
            P_IBD_Principal_MATU, P_IBD_Principal_LUMP, P_IBD_Principal_Contract_No, P_IBD_Principal_Ex_Curr, P_IBD_Principal_Exch_Rate, P_IBD_Principal_Intnl_Exch_Rate,
            P_IBD_Interest_MATU, P_IBD_Interest_LUMP, P_IBD_Interest_Contract_No, P_IBD_Interest_Ex_Curr, P_IBD_Interest_Exch_Rate, P_IBD_Interest_Intnl_Exch_Rate,
            P_IBD_Commission_MATU, P_IBD_Commission_LUMP, P_IBD_Commission_Contract_No, P_IBD_Commission_Ex_Curr, P_IBD_Commission_Exch_Rate, P_IBD_Commission_Intnl_Exch_Rate,
            P_IBD_Their_Commission_MATU, P_IBD_Their_Commission_LUMP, P_IBD_Their_Commission_Contract_No, P_IBD_Their_Commission_Ex_Curr, P_IBD_Their_Commission_Exch_Rate, P_IBD_Their_Commission_Intnl_Exch_Rate,

        P_IBD_CR_Code, P_IBD_CR_Cust_ACC_Name, P_IBD_CR_Cust_Abbr, P_IBD_CR_Cust_Acc_No, P_IBD_CR_Acceptance_Curr, P_IBD_CR_Acceptance_Amount, P_IBD_CR_Acceptance_Payer,
        P_IBD_CR_Interest_Curr, P_IBD_CR_Interest_Amount, P_IBD_CR_Interest_Payer,
        P_IBD_CR_Acceptance_Comm_Curr, P_IBD_CR_Acceptance_Comm_Amount, P_IBD_CR_Acceptance_Comm_Payer,
        P_IBD_CR_Pay_Handle_Comm_Curr, P_IBD_CR_Pay_Handle_Comm_Amount, P_IBD_CR_Pay_Handle_Comm_Payer,
        P_IBD_CR_Others_Curr, P_IBD_CR_Others_Amount, P_IBD_CR_Others_Payer,
        P_IBD_CR_Their_Comm_Curr, P_IBD_CR_Their_Comm_Amount, P_IBD_CR_Their_Comm_Payer,
        P_IBD_DR_IBDCode, P_IBD_DR_IBDCust_abbr, P_IBD_DR_IBDCust_Acc, P_IBD_DR_IBDCur_Acc_Curr, P_IBD_DR_IBDCur_Acc_amt, P_IBD_DR_IBDCur_Acc_Payr,
        P_IBD_DR_CODE, P_IBD_DR_Cust_abbr, P_IBD_DR_Cust_Acc, P_IBD_DR_Current_Acc_Curr, P_IBD_DR_Current_Acc_Amount, P_IBD_DR_Current_Acc_Payer,

        P_IBD_DR_Current_Acc_Curr2, P_IBD_DR_Current_Acc_Amount2, P_IBD_DR_Current_Acc_Payer2,
        P_IBD_DR_Current_Acc_Curr3, P_IBD_DR_Current_Acc_Amount3, P_IBD_DR_Current_Acc_Payer3,
        P_IBD_Extn_Flag,

        P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate
                    );
        return _Result;
    }
    protected void Fill_Logd_Accept_Details()
    {
        Get_Acceptance_Details();
        Get_IBD_Lodgment_Details();
    }
    protected void Fill_LCDiscountingDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_IBD_Settlement_Get_Details", PDocNo, PIBD_DocNo);
        if (dt.Rows.Count > 0)
        {

            txtDocNo.Text = dt.Rows[0]["Document_No"].ToString();
            txtIBDDocNo.Text = dt.Rows[0]["IBD_Document_No"].ToString();
            txtValueDate.Text = dt.Rows[0]["Settlement_Date"].ToString();

            if (dt.Rows[0]["AccDocDetails_Flag"].ToString() == "Y")
            {
                chk_AccDocDetails.Checked = true;
                Panel_AccDocDetails.Visible = true;
                txtCustName.Text = dt.Rows[0]["CustomerName"].ToString();
                txtCommentCode.Text = dt.Rows[0]["Comment_Code"].ToString();
                txtMaturityDate.Text = dt.Rows[0]["Maturity_Date"].ToString();
                txtsettlCodeForCust.Text = dt.Rows[0]["Settlement_For_Cust_Code"].ToString();
                txtSettl_CodeForBank.Text = dt.Rows[0]["Settlement_For_Bank_Code"].ToString();
                txtInterest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();
                txtDiscount.Text = dt.Rows[0]["Discount_Flag"].ToString();
                txtInterest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();
                txt_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();
                txt_INT_Rate.Text = dt.Rows[0]["Interest_Rate"].ToString();
                txtInterestAmt.Text = dt.Rows[0]["Interest_Amount"].ToString();
                txtOverinterestRate.Text = dt.Rows[0]["Overdue_Interest_Rate"].ToString();
                txtOverNoOfDays.Text = dt.Rows[0]["Overdue_No_Of_Days"].ToString();
                txtOverAmount.Text = dt.Rows[0]["Overdue_Interest_Amount"].ToString();
                txtAttn.Text = dt.Rows[0]["Attn"].ToString();
            }
            else
            {
                chk_AccDocDetails.Checked = false;
                Panel_AccDocDetails.Visible = false;
            }
            ////-------Import Accounting Acc------------
            if (dt.Rows[0]["AccImpAccounting_Flag"].ToString() == "Y")
            {
                chk_AccImpAccounting.Checked = true;
                Panel_AccImpAccounting.Visible = true;
                txt_DiscAmt.Text = dt.Rows[0]["IMP_ACC_Amount"].ToString();
                txt_IMP_ACC_ExchRate.Text = dt.Rows[0]["IMP_ACC_ExchRate"].ToString();
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

                txt_CR_Code.Text = dt.Rows[0]["CR_Code"].ToString();
                txt_CR_AC_ShortName.Text = dt.Rows[0]["CR_Cust_ACC_Name"].ToString();
                txt_CR_Cust_abbr.Text = dt.Rows[0]["CR_Cust_Abbr"].ToString();
                txt_CR_Cust_Acc.Text = dt.Rows[0]["CR_Cust_Acc_No"].ToString();
                txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Acceptance_Curr"].ToString();
                txt_CR_Acceptance_amt.Text = dt.Rows[0]["CR_Acceptance_Amount"].ToString();
                txt_CR_Acceptance_payer.Text = dt.Rows[0]["CR_Acceptance_Payer"].ToString();

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

                txt_DR_Code.Text = dt.Rows[0]["DR_CODE"].ToString();
                txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_abbr"].ToString();
                txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc"].ToString();
                txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
                txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["DR_Current_Acc_Amount"].ToString();
                txt_DR_Cur_Acc_payer.Text = dt.Rows[0]["DR_Current_Acc_Payer"].ToString();

                txt_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["DR_Current_Acc_Curr2"].ToString();
                txt_DR_Cur_Acc_amt2.Text = dt.Rows[0]["DR_Current_Acc_Amount2"].ToString();
                txt_DR_Cur_Acc_payer2.Text = dt.Rows[0]["DR_Current_Acc_Payer2"].ToString();

                txt_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["DR_Current_Acc_Curr3"].ToString();
                txt_DR_Cur_Acc_amt3.Text = dt.Rows[0]["DR_Current_Acc_Amount3"].ToString();
                txt_DR_Cur_Acc_payer3.Text = dt.Rows[0]["DR_Current_Acc_Payer3"].ToString();
            }
            else
            {
                chk_AccImpAccounting.Checked = false;
                Panel_AccImpAccounting.Visible = false;
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
                txt_GO1_Credit_Code.SelectedValue = dt.Rows[0]["GO_Bill_Handling_Credit_Code"].ToString();
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
                txt_GO2_Debit_Cust_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_Name"].ToString();
                txt_GO2_Debit_Cust_AcCode.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccCode"].ToString();
                txt_GO2_Debit_Cust_AccNo.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccNo"].ToString();
                txt_GO2_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Debit_Cust_AccCode_Disc"].ToString();
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
                txt_GO2_Credit_Cust_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_Name"].ToString();
                txt_GO2_Credit_Cust_AcCode.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_AccCode"].ToString();
                txt_GO2_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO2_Bill_Handling_Credit_Cust_AccCode_Disc"].ToString();
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

            ///////---------IBD Doc Details-----------
            txt_IBD_CustName.Text = dt.Rows[0]["IBD_CustomerName"].ToString();
            txt_IBD_CommentCode.Text = dt.Rows[0]["IBD_Comment_Code"].ToString();
            txt_IBD_MaturityDate.Text = dt.Rows[0]["IBD_Maturity_Date"].ToString();
            txt_IBD_settlCodeForCust.Text = dt.Rows[0]["IBD_Settlement_For_Cust_Code"].ToString();
            txt_IBD_Settl_CodeForBank.Text = dt.Rows[0]["IBD_Settlement_For_Bank_Code"].ToString();
            txt_IBD_Interest_From.Text = dt.Rows[0]["IBD_Interest_From_Date"].ToString();
            txt_IBD_Discount.Text = dt.Rows[0]["IBD_Discount_Flag"].ToString();
            txt_IBD_Interest_To.Text = dt.Rows[0]["IBD_Interest_To_Date"].ToString();
            txt_IBD__No_Of_Days.Text = dt.Rows[0]["IBD_No_Of_Days"].ToString();
            txt_IBD__INT_Rate.Text = dt.Rows[0]["IBD_Interest_Rate"].ToString();
            txt_IBD_InterestAmt.Text = dt.Rows[0]["IBD_Interest_Amount"].ToString();
            txt_IBD_OverinterestRate.Text = dt.Rows[0]["IBD_Overdue_Interest_Rate"].ToString();
            txt_IBD_OverNoOfDays.Text = dt.Rows[0]["IBD_Overdue_No_Of_Days"].ToString();
            txt_IBD_OverAmount.Text = dt.Rows[0]["IBD_Overdue_Interest_Amount"].ToString();
            txt_IBD_Attn.Text = dt.Rows[0]["IBD_Attn"].ToString();

            ////-------IBD Import Accounting Acc------------
            txt_IBD_DiscAmt.Text = dt.Rows[0]["IBD_IMP_ACC_Amount"].ToString();
            txt_IBD_IMP_ACC_ExchRate.Text = dt.Rows[0]["IBD_IMP_ACC_ExchRate"].ToString();
            txt_IBDPrinc_matu.Text = dt.Rows[0]["IBD_Principal_MATU"].ToString();
            txt_IBDPrinc_lump.Text = dt.Rows[0]["IBD_Principal_LUMP"].ToString();
            txt_IBDprinc_Contract_no.Text = dt.Rows[0]["IBD_Principal_Contract_No"].ToString();
            txt_IBD_Princ_Ex_Curr.Text = dt.Rows[0]["IBD_Principal_Ex_Curr"].ToString();
            txt_IBDprinc_Ex_rate.Text = dt.Rows[0]["IBD_Principal_Exch_Rate"].ToString();
            txt_IBDprinc_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Principal_Intnl_Exch_Rate"].ToString();

            txt_IBDInterest_matu.Text = dt.Rows[0]["IBD_Interest_MATU"].ToString();
            txt_IBDInterest_lump.Text = dt.Rows[0]["IBD_Interest_LUMP"].ToString();
            txt_IBDInterest_Contract_no.Text = dt.Rows[0]["IBD_Interest_Contract_No"].ToString();
            txt_IBD_interest_Ex_Curr.Text = dt.Rows[0]["IBD_Interest_Ex_Curr"].ToString();
            txt_IBDInterest_Ex_rate.Text = dt.Rows[0]["IBD_Interest_Exch_Rate"].ToString();
            txt_IBDInterest_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Interest_Intnl_Exch_Rate"].ToString();

            txt_IBDCommission_matu.Text = dt.Rows[0]["IBD_Commission_MATU"].ToString();
            txt_IBDCommission_lump.Text = dt.Rows[0]["IBD_Commission_LUMP"].ToString();
            txt_IBDCommission_Contract_no.Text = dt.Rows[0]["IBD_Commission_Contract_No"].ToString();
            txt_IBD_Commission_Ex_Curr.Text = dt.Rows[0]["IBD_Commission_Ex_Curr"].ToString();
            txt_IBDCommission_Ex_rate.Text = dt.Rows[0]["IBD_Commission_Exch_Rate"].ToString();
            txt_IBDCommission_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Commission_Intnl_Exch_Rate"].ToString();

            txt_IBDTheir_Commission_matu.Text = dt.Rows[0]["IBD_Their_Commission_MATU"].ToString();
            txt_IBDTheir_Commission_lump.Text = dt.Rows[0]["IBD_Their_Commission_LUMP"].ToString();
            txt_IBDTheir_Commission_Contract_no.Text = dt.Rows[0]["IBD_Their_Commission_Contract_No"].ToString();
            txt_IBD_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IBD_Their_Commission_Ex_Curr"].ToString();
            txt_IBDTheir_Commission_Ex_rate.Text = dt.Rows[0]["IBD_Their_Commission_Exch_Rate"].ToString();
            txt_IBDTheir_Commission_Intnl_Ex_rate.Text = dt.Rows[0]["IBD_Their_Commission_Intnl_Exch_Rate"].ToString();

            txt_IBD_CR_Code.Text = dt.Rows[0]["IBD_CR_Code"].ToString();
            txt_IBD_CR_AC_ShortName.Text = "I.B.D";
            txt_IBD_CR_Cust_abbr.Text = dt.Rows[0]["IBD_CR_Cust_Abbr"].ToString();
            txt_IBD_CR_Cust_Acc.Text = dt.Rows[0]["IBD_CR_Cust_Acc_No"].ToString();
            txt_IBD_CR_Acceptance_Curr.Text = dt.Rows[0]["IBD_CR_Acceptance_Curr"].ToString();
            txt_IBD_CR_Acceptance_amt.Text = dt.Rows[0]["IBD_CR_Acceptance_Amount"].ToString();
            txt_IBD_CR_Acceptance_payer.Text = dt.Rows[0]["IBD_CR_Acceptance_Payer"].ToString();

            txt_IBD_CR_Interest_Curr.Text = dt.Rows[0]["IBD_CR_Interest_Curr"].ToString();
            txt_IBD_CR_Interest_amt.Text = dt.Rows[0]["IBD_CR_Interest_Amount"].ToString();
            txt_IBD_CR_Interest_payer.Text = dt.Rows[0]["IBD_CR_Interest_Payer"].ToString();

            txt_IBD_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IBD_CR_Acceptance_Comm_Curr"].ToString();
            txt_IBD_CR_Accept_Commission_amt.Text = dt.Rows[0]["IBD_CR_Acceptance_Comm_Amount"].ToString();
            txt_IBD_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IBD_CR_Acceptance_Comm_Payer"].ToString();

            txt_IBD_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IBD_CR_Pay_Handle_Comm_Curr"].ToString();
            txt_IBD_CR_Pay_Handle_Commission_amt.Text = dt.Rows[0]["IBD_CR_Pay_Handle_Comm_Amount"].ToString();
            txt_IBD_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IBD_CR_Pay_Handle_Comm_Payer"].ToString();

            txt_IBD_CR_Others_Curr.Text = dt.Rows[0]["IBD_CR_Others_Curr"].ToString();
            txt_IBD_CR_Others_amt.Text = dt.Rows[0]["IBD_CR_Others_Amount"].ToString();
            txt_IBD_CR_Others_Payer.Text = dt.Rows[0]["IBD_CR_Others_Payer"].ToString();

            txt_IBD_CR_Their_Commission_Curr.Text = dt.Rows[0]["IBD_CR_Their_Comm_Curr"].ToString();
            txt_IBD_CR_Their_Commission_amt.Text = dt.Rows[0]["IBD_CR_Their_Comm_Amount"].ToString();
            txt_IBD_CR_Their_Commission_Payer.Text = dt.Rows[0]["IBD_CR_Their_Comm_Payer"].ToString();

            txt_IBD_IBD_DR_Code.Text = dt.Rows[0]["IBD_DR_IBDCode"].ToString();
            txt_IBD_IBD_DR_Cust_abbr.Text = dt.Rows[0]["IBD_DR_IBDCust_abbr"].ToString();
            txt_IBD_IBD_DR_Cust_Acc.Text = dt.Rows[0]["IBD_DR_IBDCust_Acc"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IBD_DR_IBDCur_Acc_Curr"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_amt.Text = dt.Rows[0]["IBD_DR_IBDCur_Acc_amt"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_payer.Text = dt.Rows[0]["IBD_DR_IBDCur_Acc_Payr"].ToString();

            if (hdnIBDInterest_MATU.Value == "2")
            {
                panal_IBD_DRdetails.Visible = true;

                txt_IBD_DR_Code.Text = dt.Rows[0]["IBD_DR_CODE"].ToString();
                txt_IBD_DR_Cust_abbr.Text = dt.Rows[0]["IBD_DR_Cust_abbr"].ToString();
                txt_IBD_DR_Cust_Acc.Text = dt.Rows[0]["IBD_DR_Cust_Acc"].ToString();
                txt_IBD_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IBD_DR_Current_Acc_Curr"].ToString();
                txt_IBD_DR_Cur_Acc_amt.Text = dt.Rows[0]["IBD_DR_Current_Acc_Amount"].ToString();
                txt_IBD_DR_Cur_Acc_payer.Text = dt.Rows[0]["IBD_DR_Current_Acc_Payer"].ToString();
            }
            txt_IBD_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IBD_DR_Current_Acc_Curr2"].ToString();
            txt_IBD_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IBD_DR_Current_Acc_Amount2"].ToString();
            txt_IBD_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IBD_DR_Current_Acc_Payer2"].ToString();

            txt_IBD_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IBD_DR_Current_Acc_Curr3"].ToString();
            txt_IBD_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IBD_DR_Current_Acc_Amount3"].ToString();
            txt_IBD_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IBD_DR_Current_Acc_Payer3"].ToString();
            if (dt.Rows[0]["IBD_Extn_Flag"].ToString() == "Y")
            {
                chk_IBDExtn_Flag.Checked = true;
            }
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
        }
    }
    public void Get_Acceptance_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_IBD_Settlement_Get_Acceptance_Details", PDocNo);
        if (dt.Rows.Count > 0)
        {
            ToggleDocType(dt.Rows[0]["Document_Type"].ToString(), dt.Rows[0]["Document_FLC_ILC"].ToString());
            //txtIBDDocNo.Text = Request.QueryString["IBDDocNo"].Trim();

            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            chk_AccDocDetails.Checked = true;
            Panel_AccDocDetails.Visible = true;

            txtCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtMaturityDate.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            txtInterest_From.Text = dt.Rows[0]["Interest_From_Date"].ToString();
            txtInterest_To.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            txt_No_Of_Days.Text = dt.Rows[0]["No_Of_Days"].ToString();


            chk_AccImpAccounting.Checked = true;
            Panel_AccImpAccounting.Visible = true;
            txt_DiscAmt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txt_IMP_ACC_ExchRate.Text = dt.Rows[0]["IMP_ACC_ExchRate"].ToString();

            txt_CR_Code.Text = dt.Rows[0]["CR_Code"].ToString();
            txt_CR_Cust_abbr.Text = dt.Rows[0]["CR_Cust_Abbr"].ToString();
            txt_CR_Cust_Acc.Text = dt.Rows[0]["CR_Cust_Acc_No"].ToString();
            txt_CR_Acceptance_amt.Text = dt.Rows[0]["Draft_Amt"].ToString();
            txt_CR_Acceptance_Curr.Text = dt.Rows[0]["CR_Acceptance_Curr"].ToString();

            txt_CR_Others_amt.Text = dt.Rows[0]["CR_Others_Amount"].ToString();
            txt_CR_Their_Commission_amt.Text = dt.Rows[0]["CR_Their_Comm_Amount"].ToString();

            txt_CR_Interest_Curr.Text = lblDoc_Curr.Text;
            txt_CR_Pay_Handle_Commission_Curr.Text = lblDoc_Curr.Text;
            txt_CR_Accept_Commission_Curr.Text = lblDoc_Curr.Text;
            txt_CR_Others_Curr.Text = lblDoc_Curr.Text;
            txt_CR_Their_Commission_Curr.Text = lblDoc_Curr.Text;

            txt_DR_Code.Text = dt.Rows[0]["DR_Code"].ToString();
            txt_DR_Cust_abbr.Text = dt.Rows[0]["DR_Cust_Abbr"].ToString();
            txt_DR_Cust_Acc.Text = dt.Rows[0]["DR_Cust_Acc_No"].ToString();
            txt_DR_Cur_Acc_Curr.Text = dt.Rows[0]["DR_Current_Acc_Curr"].ToString();
            txt_DR_Cur_Acc_amt.Text = dt.Rows[0]["Draft_Amt"].ToString();
        }
    }
    public void Get_IBD_Lodgment_Details()
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt_IBD = new DataTable();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());
        dt_IBD = obj.getData("TF_IMP_IBD_Settlement_Get_IBD_Lodgment_Details", PDocNo, PIBD_DocNo);
        if (dt_IBD.Rows.Count > 0)
        {
            //////------------------IBD Doc Details----------------
            //txtIBDDocNo.Text = dt_IBD.Rows[0]["IBDDocument_No"].ToString();
            txt_IBD_CustName.Text = dt_IBD.Rows[0]["CustomerName"].ToString();
            txt_IBD_MaturityDate.Text = dt_IBD.Rows[0]["Interest_To_Date"].ToString();
            txt_IBD_Interest_From.Text = dt_IBD.Rows[0]["Interest_From_Date"].ToString();
            txt_IBD_Interest_To.Text = dt_IBD.Rows[0]["Interest_To_Date"].ToString();
            txt_IBD__No_Of_Days.Text = dt_IBD.Rows[0]["No_Of_Days"].ToString();

            txt_IBD_DiscAmt.Text = dt_IBD.Rows[0]["IMP_ACC_Amount"].ToString();
            txt_IBD_IMP_ACC_ExchRate.Text = dt_IBD.Rows[0]["IMP_ACC_ExchRate"].ToString();

            txt_IBD_CR_Code.Text = dt_IBD.Rows[0]["DR_IBDCode"].ToString();
            txt_IBD_CR_AC_ShortName.Text = "I.B.D";
            txt_IBD_CR_Cust_abbr.Text = dt_IBD.Rows[0]["DR_IBDCust_abbr"].ToString();
            txt_IBD_CR_Cust_Acc.Text = dt_IBD.Rows[0]["DR_IBDCust_Acc"].ToString();
            txt_IBD_CR_Acceptance_Curr.Text = dt_IBD.Rows[0]["DR_IBDCur_Acc_Curr"].ToString();
            txt_IBD_CR_Acceptance_amt.Text = dt_IBD.Rows[0]["DR_IBDCur_Acc_amt"].ToString();
            
            txt_IBD_CR_Interest_Curr.Text = lblDoc_Curr.Text;

            txt_IBD_CR_Accept_Commission_amt.Text = dt_IBD.Rows[0]["CR_Acceptance_Comm_Amount"].ToString();
            txt_IBD_CR_Accept_Commission_Curr.Text = dt_IBD.Rows[0]["CR_Acceptance_Comm_Curr"].ToString();

            txt_IBD_CR_Pay_Handle_Commission_amt.Text = dt_IBD.Rows[0]["CR_Pay_Handle_Comm_Amount"].ToString();
            txt_IBD_CR_Pay_Handle_Commission_Curr.Text = dt_IBD.Rows[0]["CR_Pay_Handle_Comm_Curr"].ToString();

            txt_IBD_CR_Others_amt.Text = dt_IBD.Rows[0]["CR_Others_Amount"].ToString();
            txt_IBD_CR_Others_Curr.Text = dt_IBD.Rows[0]["CR_Others_Curr"].ToString();

            txt_IBD_CR_Their_Commission_amt.Text = dt_IBD.Rows[0]["CR_Their_Comm_Amount"].ToString();
            txt_IBD_CR_Their_Commission_Curr.Text = dt_IBD.Rows[0]["CR_Their_Comm_Curr"].ToString();

            txt_IBD_IBD_DR_Code.Text = dt_IBD.Rows[0]["CR_Code"].ToString();
            txt_IBD_IBD_DR_Cust_abbr.Text = dt_IBD.Rows[0]["CR_Cust_Abbr"].ToString();
            txt_IBD_IBD_DR_Cust_Acc.Text = dt_IBD.Rows[0]["CR_Cust_Acc_No"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_Curr.Text = dt_IBD.Rows[0]["CR_Acceptance_Curr"].ToString();
            txt_IBD_IBD_DR_Cur_Acc_amt.Text = dt_IBD.Rows[0]["CR_Acceptance_Amount"].ToString();

            if (dt_IBD.Rows[0]["Interest_MATU"].ToString() == "2")
            {
                hdnIBDInterest_MATU.Value = dt_IBD.Rows[0]["Interest_MATU"].ToString();
                panal_IBD_DRdetails.Visible = true;

                txt_IBD_DR_Code.Text = dt_IBD.Rows[0]["CR_Code"].ToString();
                txt_IBD_DR_Cust_abbr.Text = dt_IBD.Rows[0]["CR_Cust_Abbr"].ToString();
                txt_IBD_DR_Cust_Acc.Text = dt_IBD.Rows[0]["CR_Cust_Acc_No"].ToString();
                txt_IBD_DR_Cur_Acc_Curr.Text = dt_IBD.Rows[0]["CR_Interest_Curr"].ToString();
                txt_IBD_DR_Cur_Acc_amt.Text = dt_IBD.Rows[0]["CR_Interest_Amount"].ToString();
                //txt_IBD_DR_Cur_Acc_payer.Text = dt_IBD.Rows[0]["CR_Acceptance_Payer"].ToString();
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
                    txtsettlCodeForCust.Text = "29";
                    txt_IBD_settlCodeForCust.Text = "29";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "Local";
                    txtsettlCodeForCust.Text = "23";
                    txt_IBD_settlCodeForCust.Text = "23";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblLCDescount_Lodgment_UnderLC.Text = "LCDiscount_Under_LC";
                lblSight_Usance.Text = "Usance";
                txtSettl_CodeForBank.Text = "09";
                txt_IBD_Settl_CodeForBank.Text = "09";

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
    protected void Get_IBD_Date_Diff(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PInterest_From = new SqlParameter("@Interest_From", txt_IBD_Interest_From.Text.ToString());
        SqlParameter PInterest_To = new SqlParameter("@Interest_To", txt_IBD_Interest_To.Text.ToString());
        DataTable Date_dt = new DataTable();
        Date_dt = obj.getData("TF_IMP_ONLCDescounting_Get_Date_Diff", PInterest_From, PInterest_To);
        if (Date_dt.Rows.Count > 0)
        {
            txt_IBD__No_Of_Days.Text = Date_dt.Rows[0]["NoOfDays"].ToString();
            txt_IBD__INT_Rate.Focus();
        }
    }
    protected void MakeReadOnly()
    {
        txtDocNo.Enabled = false;
        txtIBDDocNo.Enabled = false;
        txtValueDate.Enabled = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Maker_View.aspx");
    }
    protected void btnIBD_EXTENSION_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter PIBD_DocNo = new SqlParameter("@IBD_Document_No", txtIBDDocNo.Text.ToString());

        string Result = obj.SaveDeleteData("TF_IMP_IBD_Settlement_SubmitForChecker", P_DocNo, PIBD_DocNo);

        if (chk_IBDExtn_Flag.Checked == true)
        {
            if (hdnDocScrutiny.Value == "Clean")
            {
                Response.Redirect("TF_IMP_LC_DESCOUNTING_ACC_IBD_Maker.aspx?DocNo=" + txtDocNo.Text + "&DocType=" + hdnDocType.Value.Trim() + "&BranchName=" + hdnBranchName.Value.Trim() +
                    "&DocScrutiny=" + hdnDocScrutiny.Value.Trim() + "&IBDDocument_No= &IBDExtnDocNo=" + txtIBDDocNo.Text + "&IBDExtn=Y");
            }
            else
            {
                Response.Redirect("TF_IMP_LC_DESCOUNTING_DISCREPANT_ACC_IBD_Maker.aspx?DocNo=" + txtDocNo.Text + "&DocType=" + hdnDocType.Value.Trim() + "&BranchName=" + hdnBranchName.Value.Trim() +
                    "&DocScrutiny=" + hdnDocScrutiny.Value.Trim() + "&IBDDocument_No= &IBDExtnDocNo=" + txtIBDDocNo.Text + "&IBDExtn=Y");
            }
        }
        else
        {
            string _script = "window.location='TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
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
    protected void chk_AccDocDetails_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_AccDocDetails.Checked == false)
        {
            Panel_AccDocDetails.Visible = false;
        }
        else if (chk_AccDocDetails.Checked == true)
        {
            Panel_AccDocDetails.Visible = true;
            Get_Acceptance_Details();
        }
    }
    protected void chk_AccImpAccounting_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_AccImpAccounting.Checked == false)
        {
            Panel_AccImpAccounting.Visible = false;
        }
        else if (chk_AccImpAccounting.Checked == true)
        {
            Panel_AccImpAccounting.Visible = true;
        }
    }
}