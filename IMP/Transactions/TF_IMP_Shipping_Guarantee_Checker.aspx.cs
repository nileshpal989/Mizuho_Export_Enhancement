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


public partial class IMP_Transactions_TF_IMP_Shipping_Guarantee_Checker : System.Web.UI.Page
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
                    Response.Redirect("TF_IMP_Shipping_Guarantee_Checker_View.aspx", true);
                }
                else
                {
                    hdnBranchCode.Value = Request.QueryString["BranchCode"].Trim();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    hdnDocNo.Value = Request.QueryString["DocNo"].Trim();
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                    }
                    fillCurrency();
                    fill_GBaseCommodity();
                    fill_Country();

                    //Fill_Logd_Details();
                    Fill_Shipping_Guarantee_Details();
                }
            }
        }
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

            /////////////// shipping guarantee//////////////////////////
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", hdnDocNo.Value.Trim());
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            GBaseFile();
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_Shipping_Guarantee_ChekerApproveReject", P_DocNo, P_Status, P_RejectReason);
        Response.Redirect("TF_IMP_Shipping_Guarantee_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
    public void GBaseFile()
    {
        GBaseFileCreation();
        if (chk_GO1Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation1();
        }
        if (chk_GO2Flag.Checked == true)
        {
            GBaseFileCreationGeneralOperation2();
        }
        
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_Shipping_Guarantee_GBaseFileCreation", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Liability/GBASE/" + MTodayDate + "/");
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
        DataTable dt = objData1.getData("TF_IMP_Shipping_Guarantee_GBaseFileCreation_GO1", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Liability/GBASE/" + MTodayDate + "/");
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
        DataTable dt = objData1.getData("TF_IMP_Shipping_Guarantee_GBaseFileCreation_GO2", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Liability/GBASE/" + MTodayDate + "/");
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
}