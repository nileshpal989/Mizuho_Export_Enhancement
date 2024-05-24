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

public partial class IMP_Transactions_TF_IMP_Shipping_Guarantee_Maker : System.Web.UI.Page
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
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_Shipping_Guarantee_Maker_View.aspx", true);
                }
                else
                {
                    hdnBranchCode.Value = Request.QueryString["BranchCode"].Trim();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    fillCurrency();
                    fill_GBaseCommodity();
                    fill_Country();

                    TF_DATA obj = new TF_DATA();
                    SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
                    string result = obj.SaveDeleteData("TF_IMP_Shipping_Guarantee_Check_Ref", PDocNo);
                    if (result == "exists")
                    {  
                        Fill_Shipping_Guarantee_Details();
                    }
                    else
                    {
                        //Fill_Logd_Details();
                        txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    }
                   
                }
            }
            //btncountry.Attributes.Add("onclick", "return Countryhelp()");
            //btnApplName.Attributes.Add("onclick", "return CustomerHelp()");
            //btncurrency.Attributes.Add("onclick", "return CurrencyHelp()");
            //txt_Bill_Amt.Attributes.Add("onchange", "return ccyformat1();");
            //txtbillamt.Attributes.Add("onkeyDown", "return validate_Number(event);");
            //txtApplPincode.Attributes.Add("onkeyDown", "return validate_Number(event);");
        }
    }
    [WebMethod]
    public static string AddUpdateShippingBillGuarantee(string hdnUserName, string _BranchName, string _txt_LC_No,
    string _txtDocNo, string _txtValueDate, string _Document_Curr, string _Bill_Amt,
        ///////////////// SG /////////////////
    string Bene_name,
    string Bene_add1, string Bene_add2, string Bene_add3, string Bene_add4,
    string _Applicantid, string _Applicantname,
    string _ApplicantAdd, string _ApplicantCity, string _ApplicantPincode, string _LcRefno,
    string _Shippingissued, string _Shippingcompname, string _SGcountry,
    string _Vesselname1, string _Vesselname2, string _Vesseldate1, string _Vesseldate2,
    string _ShipperName, string _SupplierName, string _Consignee_Name,
    string _Notify_Party, string _Descofgoods, string _Quantity, string _ShippingMarks,
    string _SGCurrency, string _SGBillAmt, string _commercialInvno,
    string _Vesselname3, string _Billno, string _remarks,
    string _SGPolicy, string _SGOurRefNo, string _SGAhm,

    string _txt_Doc_Customer_ID, string _txt_CCode,
    string _txt_Issuing_Date, string _txt_Expiry_Date,
    string _ddl_revInfoOpt,
    string _txt_ApplNoBranch, string _txt_AdvisingBank,
    string _ddlCountryCode, string _ddl_Commodity,
    string _txt_RiskCountry, string _txt_RiskCust,
    string _txt_GradeCode, string _txt_HoApl,
    string _txt_Remarks, string _txt_RemEUC,
    string _txt_Comm_Rate, string _txt_Comm_CalCode, string _txt_Comm_Interval,
    string _txt_Comm_Advance, string _txt_Comm_EndInclu,
    string _txt_CreditOpenChrg_Curr, string _txt_CreditOpenChrg,
    string _txt_CreditMailChrgCurr, string _txt_CreditMailChrg,
    string _txt_CreditExchInfCurr, string _txt_CreditExchRate, string _txt_CreditILTExchRate,
    string _txt_DebitAcShortName, string _txt_DebitCustAbbr, string _txt_DebitAcNo, string _txt_DebitCurr, string _txt_DebitAmt,
    string _txt_DebitAcShortName2, string _txt_DebitCustAbbr2, string _txt_DebitAcNo2, string _txt_DebitCurr2, string _txt_DebitAmt2,
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
    string _txt_GO2_Right_Credit_Division, string _txt_GO2_Right_Credit_Inter_Amount, string _txt_GO2_Right_Credit_Inter_Rate

)
    {
        TF_DATA obj = new TF_DATA();
        ////document deatails
        SqlParameter P_hdnUserName = new SqlParameter("@AddedBy", hdnUserName.ToUpper());
        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName.ToUpper());
        SqlParameter P_LC_No = new SqlParameter("@LC_No", _txt_LC_No.ToUpper());

        SqlParameter P_Document_No = new SqlParameter("@Document_No", _txtDocNo.ToUpper());
        SqlParameter P_Value_Date = new SqlParameter("@Value_Date", _txtValueDate.ToUpper());
        SqlParameter P_Document_Curr = new SqlParameter("@Currency", _Document_Curr.ToUpper());
        SqlParameter P_Bill_Amt = new SqlParameter("@Bill_Amt", _Bill_Amt.ToUpper());

        //////////////////////SG
        SqlParameter P_Bene_name = new SqlParameter("@Benename", Bene_name.ToUpper());
        SqlParameter P_Bene_add1 = new SqlParameter("@Bene_ADDRESS1", Bene_add1.ToUpper());
        SqlParameter P_Bene_add2 = new SqlParameter("@Bene_ADDRESS2", Bene_add2.ToUpper());
        SqlParameter P_Bene_add3 = new SqlParameter("@Bene_ADDRESS3", Bene_add3.ToUpper());
        SqlParameter P_Bene_add4 = new SqlParameter("@Bene_ADDRESS4", Bene_add4.ToUpper());
        SqlParameter P_Applicantid = new SqlParameter("@Applicant_id", _Applicantid.ToUpper());
        SqlParameter P_Applicantname = new SqlParameter("@Applicant_Name", _Applicantname.ToUpper());
        SqlParameter P_ApplicantAdd = new SqlParameter("@Applicant_Address", _ApplicantAdd.ToUpper());
        SqlParameter P_ApplicantCity = new SqlParameter("@Applicant_City", _ApplicantCity.ToUpper());
        SqlParameter P_ApplicantPincode = new SqlParameter("@Applicant_Pincode", _ApplicantPincode.ToUpper());
        SqlParameter P_LcRefno = new SqlParameter("@SGLC_Ref_NO", _LcRefno.ToUpper());
        SqlParameter P_Shippingissued = new SqlParameter("@Shippingissued", _Shippingissued.ToUpper());
        SqlParameter P_Shippingcompname = new SqlParameter("@Shippingcompname", _Shippingcompname.ToUpper());
        SqlParameter P_SGcountry = new SqlParameter("@SGCountry_Code", _SGcountry.ToUpper());
        SqlParameter P_Vesselname1 = new SqlParameter("@Vesselname1", _Vesselname1.ToUpper());
        SqlParameter P_Vesselname2 = new SqlParameter("@Vesselname2", _Vesselname2.ToUpper());
        SqlParameter P_Vesseldate1 = new SqlParameter("@VesselDate1", _Vesseldate1.ToUpper());
        SqlParameter P_Vesseldate2 = new SqlParameter("@VesselDate2", _Vesseldate2.ToUpper());
        SqlParameter P_ShipperName = new SqlParameter("@Shipper_Name", _ShipperName.ToUpper());
        SqlParameter P_SupplierName = new SqlParameter("@Supplier_Name", _SupplierName.ToUpper());
        SqlParameter P_Consignee_Name = new SqlParameter("@Consignee_Name", _Consignee_Name.ToUpper());
        SqlParameter P_Notify_Party = new SqlParameter("@Notify_Party", _Notify_Party.ToUpper());
        SqlParameter P_Descofgoods = new SqlParameter("@Desc_Of_Goods", _Descofgoods.ToUpper());
        SqlParameter P_Quantity = new SqlParameter("@Quantity", _Quantity.ToUpper());
        SqlParameter P_ShippingMarks = new SqlParameter("@Shipping_marks", _ShippingMarks.ToUpper());
        SqlParameter P_SGCurrency = new SqlParameter("@SGCurrency", _SGCurrency.ToUpper());
        SqlParameter P_SGBillAmt = new SqlParameter("@SGBill_Amt", _SGBillAmt.ToUpper());
        SqlParameter P_commercialInvno = new SqlParameter("@commercialInvno", _commercialInvno.ToUpper());
        SqlParameter P_Vesselname3 = new SqlParameter("@vesselname3", _Vesselname3.ToUpper());
        SqlParameter P_Billno = new SqlParameter("@Billno", _Billno.ToUpper());
        SqlParameter P_remarks = new SqlParameter("@SG_remarks", _remarks.ToUpper());
        SqlParameter P_SGPolicy = new SqlParameter("@SGPolicy", _SGPolicy.ToUpper());
        //SqlParameter P_SGOurRefNo = new SqlParameter("@SGOur_Ref_no", _SGOurRefNo.ToUpper());
        SqlParameter P_SGAhm = new SqlParameter("@SGAHM_Flag", _SGAhm.ToUpper());

        SqlParameter P_Customer_ID = new SqlParameter("@Cust_AccNo", _txt_Doc_Customer_ID.ToUpper());
        SqlParameter P_CCode = new SqlParameter("@C_Code", _txt_CCode.ToUpper());

        SqlParameter P_Issuing_Date = new SqlParameter("@Issuing_Date", _txt_Issuing_Date.ToUpper());
        SqlParameter P_Expiry_Date = new SqlParameter("@Expiry_Date", _txt_Expiry_Date.ToUpper());

        SqlParameter P_ddl_revInfoOpt = new SqlParameter("@Revolving_Info_opt", _ddl_revInfoOpt.ToUpper());

        SqlParameter P_ApplNoBranch = new SqlParameter("@Appl_No_Branch", _txt_ApplNoBranch.ToUpper());
        SqlParameter P_AdvisingBank = new SqlParameter("@Advising_Bank", _txt_AdvisingBank.ToUpper());

        SqlParameter P_CountryCode = new SqlParameter("@Country_Code", _ddlCountryCode.ToUpper());
        SqlParameter P_Commodity = new SqlParameter("@Commodity_Code", _ddl_Commodity.ToUpper());

        SqlParameter P_RiskCountry = new SqlParameter("@Risk_Country", _txt_RiskCountry.ToUpper());
        SqlParameter P_RiskCust = new SqlParameter("@Risk_Cust", _txt_RiskCust.ToUpper());

        SqlParameter P_GradeCode = new SqlParameter("@Grade_Code", _txt_GradeCode.ToUpper());
        SqlParameter P_HoApl = new SqlParameter("@HO_Appl", _txt_HoApl.ToUpper());

        SqlParameter P_Remarks = new SqlParameter("@Remark", _txt_Remarks.ToUpper());
        SqlParameter P_RemEUC = new SqlParameter("@REM_EUC", _txt_RemEUC.ToUpper());

        SqlParameter P_Comm_Rate = new SqlParameter("@Comm_Rate", _txt_Comm_Rate.ToUpper());
        SqlParameter P_Comm_CalCode = new SqlParameter("@Comm_CalCode", _txt_Comm_CalCode.ToUpper());
        SqlParameter P_Comm_Interval = new SqlParameter("@Comm_Interval", _txt_Comm_Interval.ToUpper());

        SqlParameter P_Comm_Advance = new SqlParameter("@Comm_Adv", _txt_Comm_Advance.ToUpper());
        SqlParameter P_Comm_EndInclu = new SqlParameter("@Comm_EndIncl", _txt_Comm_EndInclu.ToUpper());

        SqlParameter P_CreditOpenChrg_Curr = new SqlParameter("@Open_Chrg_Curr", _txt_CreditOpenChrg_Curr.ToUpper());
        SqlParameter P_CreditOpenChrg = new SqlParameter("@Open_Chrg_Amt", _txt_CreditOpenChrg.ToUpper());

        SqlParameter P_CreditMailChrgCurr = new SqlParameter("@CableMail_Chrg_Curr", _txt_CreditMailChrgCurr.ToUpper());
        SqlParameter P_CreditMailChrg = new SqlParameter("@CableMail_Chrg_Amt", _txt_CreditMailChrg.ToUpper());

        SqlParameter P_CreditExchInfCurr = new SqlParameter("@Exch_Inf_Curr", _txt_CreditExchInfCurr.ToUpper());
        SqlParameter P_CreditExchRate = new SqlParameter("@Exch_Inf_Rate", _txt_CreditExchRate.ToUpper());
        SqlParameter P_CreditILTExchRate = new SqlParameter("@ILT_Exch_Rate", _txt_CreditILTExchRate.ToUpper());

        SqlParameter P_DebitAcShortName = new SqlParameter("@Debit1_ShortName", _txt_DebitAcShortName.ToUpper());
        SqlParameter P_DebitCustAbbr = new SqlParameter("@Debit1_CustAbbr", _txt_DebitCustAbbr.ToUpper());
        SqlParameter P_DebitAcNo = new SqlParameter("@Debit1_CustAccNo", _txt_DebitAcNo.ToUpper());
        SqlParameter P_DebitCurr = new SqlParameter("@Debit1_Curr", _txt_DebitCurr.ToUpper());
        SqlParameter P_DebitAmt = new SqlParameter("@Debit1_Amt", _txt_DebitAmt.ToUpper());

        SqlParameter P_DebitAcShortName2 = new SqlParameter("@Debit2_ShortName", _txt_DebitAcShortName2.ToUpper());
        SqlParameter P_DebitCustAbbr2 = new SqlParameter("@Debit2_CustAbbr", _txt_DebitCustAbbr2.ToUpper());
        SqlParameter P_DebitAcNo2 = new SqlParameter("@Debit2_CustAccNo", _txt_DebitAcNo2.ToUpper());
        SqlParameter P_DebitCurr2 = new SqlParameter("@Debit2_Curr", _txt_DebitCurr2.ToUpper());
        SqlParameter P_DebitAmt2 = new SqlParameter("@Debit2_Amt", _txt_DebitAmt2.ToUpper());

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


        string _Result = obj.SaveDeleteData("TF_IMP_Shipping_Guarantee_AddUpdate", P_hdnUserName, P_BranchName, P_LC_No,
        P_Document_No, P_Value_Date, P_Document_Curr, P_Bill_Amt,
        ////// SG
        P_Bene_name,
        P_Bene_add1, P_Bene_add2, P_Bene_add3, P_Bene_add4,
        P_Applicantid, P_Applicantname, P_ApplicantAdd,
        P_ApplicantCity, P_ApplicantPincode, P_LcRefno,
        P_Shippingissued, P_Shippingcompname, P_SGcountry,
        P_Vesselname1,P_Vesselname2,P_Vesseldate1,P_Vesseldate2,
        P_ShipperName, P_SupplierName, P_Consignee_Name, P_Notify_Party,
        P_Descofgoods, P_Quantity, P_ShippingMarks,P_SGCurrency,
		P_SGBillAmt, P_commercialInvno, P_Vesselname3 ,
		P_Billno , P_remarks , P_SGPolicy , 
        //P_SGOurRefNo ,
         P_SGAhm ,

        P_Customer_ID, P_CCode,
        P_Issuing_Date, P_Expiry_Date,
        P_ddl_revInfoOpt,
        P_ApplNoBranch, P_AdvisingBank,
        P_CountryCode, P_Commodity,
        P_RiskCountry, P_RiskCust,
        P_GradeCode, P_HoApl,
        P_Remarks, P_RemEUC,
        P_Comm_Rate, P_Comm_CalCode, P_Comm_Interval,
        P_Comm_Advance, P_Comm_EndInclu,
        P_CreditOpenChrg_Curr, P_CreditOpenChrg,
        P_CreditMailChrgCurr, P_CreditMailChrg,
        P_CreditExchInfCurr, P_CreditExchRate, P_CreditILTExchRate,
        P_DebitAcShortName, P_DebitCustAbbr, P_DebitAcNo, P_DebitCurr, P_DebitAmt,
        P_DebitAcShortName2, P_DebitCustAbbr2, P_DebitAcNo2, P_DebitCurr2, P_DebitAmt2,
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
        P_txt_GO2_Right_Credit_Division, P_txt_GO2_Right_Credit_Inter_Amount, P_txt_GO2_Right_Credit_Inter_Rate
        );
        return _Result;
    }
    protected void fillCurrency()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData("TF_IMP_Currency_List");
            ddlDoc_Curr.Items.Clear();
            ListItem li = new ListItem();
            li.Value = "";
            if (dt.Rows.Count > 0)
            {
                li.Text = "Select";
                ddlDoc_Curr.DataSource = dt.DefaultView;
                ddlDoc_Curr.DataTextField = "C_Code";
                ddlDoc_Curr.DataValueField = "C_Code";
                ddlDoc_Curr.DataBind();
            }
            else
            {
                li.Text = "No record(s) found";
            }
            ddlDoc_Curr.Items.Insert(0, li);
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
    protected void Fill_Logd_Details() { }
    protected void Fill_Shipping_Guarantee_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Document_No", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Shipping_Guarantee_GetDetails", PDocNo);
        if (dt.Rows.Count > 0)
        {
            //////////////// Document details //////////////////////////
            txt_LC_No.Text = dt.Rows[0]["LC_Ref_No"].ToString();
            txtValueDate.Text = dt.Rows[0]["Value_Date"].ToString();
            ddlDoc_Curr.SelectedValue = dt.Rows[0]["Currency"].ToString();
            txt_Bill_Amt.Text = dt.Rows[0]["Bill_Amt"].ToString();

            txtbenename.Text = dt.Rows[0]["Benename"].ToString();
            txtbeneadd1.Text = dt.Rows[0]["Bene_ADDRESS1"].ToString();
            txtbeneadd2.Text = dt.Rows[0]["Bene_ADDRESS2"].ToString();
            txtbeneadd3.Text = dt.Rows[0]["Bene_ADDRESS3"].ToString();
            txtbeneadd4.Text = dt.Rows[0]["Bene_ADDRESS4"].ToString();
            txtApplid.Text = dt.Rows[0]["Applicant_id"].ToString();
            lblApplName.Text = dt.Rows[0]["Applicant_Name"].ToString();
            txtApplAdd.Text = dt.Rows[0]["Applicant_Address"].ToString();
            txtApplCity.Text = dt.Rows[0]["Applicant_City"].ToString();
            txtApplPincode.Text = dt.Rows[0]["Applicant_Pincode"].ToString();
            txtlcrefno.Text = dt.Rows[0]["SGLC_Ref_NO"].ToString();
            txtShippingissued.Text = dt.Rows[0]["Shippingissued"].ToString();
            txtcountry.Text = dt.Rows[0]["SGCountry_Code"].ToString();
            txtShipcompname.Text = dt.Rows[0]["Shippingcompname"].ToString();
            txtVesselname1.Text = dt.Rows[0]["Vesselname1"].ToString();
            txtVesselname2.Text = dt.Rows[0]["Vesselname2"].ToString();
            txtVesseldate1.Text = dt.Rows[0]["VesselDate1"].ToString();
            txtVesseldate2.Text = dt.Rows[0]["VesselDate2"].ToString();
            txtshipper.Text = dt.Rows[0]["Shipper_Name"].ToString();
            txtsupplier.Text = dt.Rows[0]["Supplier_Name"].ToString();
            txtconsignee.Text = dt.Rows[0]["Consignee_Name"].ToString();
            txtnotifyname.Text = dt.Rows[0]["Notify_Party"].ToString();
            txtdescofgoods.Text = dt.Rows[0]["Desc_Of_Goods"].ToString();
            txtquantity.Text = dt.Rows[0]["Quantity"].ToString();
            txtshipmarks.Text = dt.Rows[0]["Shipping_marks"].ToString();
            txtcurrency.Text = dt.Rows[0]["SGCurrency"].ToString();
            txtbillamt.Text = dt.Rows[0]["SGBill_Amt"].ToString().Replace(",", "");
            txt_Com_InvNo.Text = dt.Rows[0]["commercialInvno"].ToString();
            txtVesselname3.Text = dt.Rows[0]["vesselname3"].ToString();
            txt_Bill_No.Text = dt.Rows[0]["Billno"].ToString();
            txt_Remarks_Reg.Text = dt.Rows[0]["remarks"].ToString();
            txtgoodspolicy.Text = dt.Rows[0]["SGPolicy"].ToString();
            txtOurref.Text = dt.Rows[0]["SGOur_Ref_no"].ToString();
            if (dt.Rows[0]["SGAHM_Flag"].ToString() == "Y")
            {
                Chk_Ahm.Checked = true;
            }

            txt_Doc_Customer_ID.Text = dt.Rows[0]["Cust_AccNo"].ToString();
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txt_CCode.Text = dt.Rows[0]["C_Code"].ToString();
            txt_Issuing_Date.Text = dt.Rows[0]["Issuing_Date"].ToString();
            txt_Expiry_Date.Text = dt.Rows[0]["Expiry_Date"].ToString();
            ddl_revInfoOpt.Text = dt.Rows[0]["Revolving_Info_opt"].ToString();
            txt_ApplNoBranch.Text = dt.Rows[0]["Appl_No_Branch"].ToString();

            txt_AdvisingBank.Text = dt.Rows[0]["Advising_Bank"].ToString();
            ddlCountryCode.SelectedValue = dt.Rows[0]["Country_Code"].ToString();

            ddl_Commodity.SelectedValue = dt.Rows[0]["Commodity_Code"].ToString();
            fill_GBaseCommodity_Description();

            txt_RiskCountry.Text = dt.Rows[0]["Risk_Country"].ToString();
            txt_RiskCust.Text = dt.Rows[0]["Risk_Cust"].ToString();
            txt_GradeCode.Text = dt.Rows[0]["Grade_Code"].ToString();
            txt_HoApl.Text = dt.Rows[0]["HO_Appl"].ToString();
            txt_Remarks.Text = dt.Rows[0]["Remark"].ToString();
            txt_RemEUC.Text = dt.Rows[0]["REM_EUC"].ToString();
            txt_Comm_Rate.Text = dt.Rows[0]["Comm_Rate"].ToString();
            txt_Comm_CalCode.Text = dt.Rows[0]["Comm_CalCode"].ToString();
            txt_Comm_Interval.Text = dt.Rows[0]["Comm_Interval"].ToString();
            txt_Comm_Advance.Text = dt.Rows[0]["Comm_Adv"].ToString();
            txt_Comm_EndInclu.Text = dt.Rows[0]["Comm_EndIncl"].ToString();
            txt_CreditOpenChrg.Text = dt.Rows[0]["Open_Chrg_Amt"].ToString();
            txt_CreditOpenChrg_Curr.Text = dt.Rows[0]["Open_Chrg_Curr"].ToString();
            txt_CreditMailChrg.Text = dt.Rows[0]["CableMail_Chrg_Amt"].ToString();
            txt_CreditMailChrgCurr.Text = dt.Rows[0]["CableMail_Chrg_Curr"].ToString();
            txt_CreditExchInfCurr.Text = dt.Rows[0]["Exch_Inf_Curr"].ToString();
            txt_CreditExchRate.Text = dt.Rows[0]["Exch_Inf_Rate"].ToString();
            txt_CreditILTExchRate.Text = dt.Rows[0]["ILT_Exch_Rate"].ToString();

            txt_DebitAcShortName.Text = dt.Rows[0]["Debit1_ShortName"].ToString();
            txt_DebitCustAbbr.Text = dt.Rows[0]["Debit1_CustAbbr"].ToString();
            txt_DebitAcNo.Text = dt.Rows[0]["Debit1_CustAccNo"].ToString();
            txt_DebitCurr.Text = dt.Rows[0]["Debit1_Curr"].ToString();
            txt_DebitAmt.Text = dt.Rows[0]["Debit1_Amt"].ToString();

            txt_DebitAcShortName2.Text = dt.Rows[0]["Debit2_ShortName"].ToString();
            txt_DebitCustAbbr2.Text = dt.Rows[0]["Debit2_CustAbbr"].ToString();
            txt_DebitAcNo2.Text = dt.Rows[0]["Debit2_CustAccNo"].ToString();
            txt_DebitCurr2.Text = dt.Rows[0]["Debit2_Curr"].ToString();
            txt_DebitAmt2.Text = dt.Rows[0]["Debit2_Amt"].ToString();

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

            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
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

            SqlParameter p1 = new SqlParameter("@customerACNo", txt_Doc_Customer_ID.Text);
            SqlParameter p2 = new SqlParameter("@branch", hdnBranchName.Value);
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData("TF_GetCustomerMasterDetails", p1, p2);
            if (dt.Rows.Count > 0)
            {
                txt_GO1_Left_Debit_Cust_AcCode.Text = dt.Rows[0]["AC_Code"].ToString();
                txt_GO1_Left_Debit_Cust_AcCode_Name.Text = "CURRENT ACCOUNT";
                txt_GO1_Left_Debit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString();
                txt_GO1_Left_Debit_Cust_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();
                txt_GO1_Left_Debit_Cust_AccNo.Text = txt_Doc_Customer_ID.Text;
            }
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_Shipping_Guarantee_SubmitToChecker", P_DocNo);
        if (Result == "Submit")
        {
            string _script = "";
            _script = "window.location='TF_IMP_Shipping_Guarantee_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
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
                }
            }
            else
            {
                lblCommodityDesc.Text = "";
                lblCommodityDesc.ToolTip = "";
                ddl_Commodity.ClearSelection();
            }
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
    protected void txtApplid_TextChanged(object sender, EventArgs e)
    {
        fillCustomerDescription();
    }
    public void fillCustomerDescription()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@App_id", SqlDbType.VarChar);
        p1.Value = txtApplid.Text;
        SqlParameter p2 = new SqlParameter("@BranchName", Request.QueryString["BranchName"].Trim());
        string _query = "TF_IMP_BRO_GetCustomerDetails";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblApplName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtApplAdd.Text = dt.Rows[0]["CUST_ADDRESS"].ToString().Trim();
            txtApplCity.Text = dt.Rows[0]["CUST_CITY"].ToString().Trim();
            txtApplPincode.Text = dt.Rows[0]["CUST_PINCODE"].ToString().Trim();
            txtcountry.Text = dt.Rows[0]["CUST_COUNTRY"].ToString().Trim();
            txtnotifyname.Text = lblApplName.Text;
            txt_Doc_Customer_ID.Text = txtApplid.Text;
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txt_RiskCust.Text = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            txt_DebitCustAbbr.Text=dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            txt_DebitAcNo.Text=txtApplid.Text;
            txt_DebitCustAbbr2.Text=dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            txt_DebitAcNo2.Text = txtApplid.Text;

        }
        else
        {
            lblApplName.Text = "";
            txtApplAdd.Text = "";
        }
    }

    protected void txtcountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
    }
    public void fillCountryDescription()
    {
        lblcountryname.Text = "";
        string Countryid = txtcountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblcountryname.Text = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            txtcountry.Text = "";
            lblcountryname.Text = "";
        }
    }
}