using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using OfficeOpenXml;
//using Excel = Microsoft.Office.Interop.Excel;
//using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

public partial class IMP_Transactions_TF_IMP_BRO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            hdnUserName.Value = Session["userName"].ToString();
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_ViewBRO.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "edit")
                    {
                        fillBranch();
                        ddlBranch_SelectedIndexChanged1(null, null);
                        getBroNo();
                        txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    else if (Request.QueryString["mode"].Trim() != "Add")
                        {
                            string no = Request.QueryString["Delivery_Order_No"].Trim();
                            string[] value_p;
                            if (no != "")
                            {
                                char[] splitchar = { '-' };
                                value_p = no.Split(splitchar);
                                txtdosrno.Text = value_p[0].ToString();
                                txtdosrno1.Text = value_p[1].ToString();
                                txtdosrno2.Text = value_p[2].ToString();
                                fillDetails();
                                fillBranch1();
                            }
                        }
                    }
            }
            btncountry.Attributes.Add("onclick", "return Countryhelp()");
            btnApplName.Attributes.Add("onclick", "return CustomerHelp()");
            btncurrency.Attributes.Add("onclick", "return CurrencyHelp()");
            txtbillamt.Attributes.Add("onkeydown", "return validate_Number(event);");
        }
    }

    [WebMethod]
    public static string AddUpdateBro(string hdnUserName,string _txtValueDate,string _Branch, string _delorderno, string _delorderno1, string _delorderno2,
        string _brodate, string ship_name, string ship_add,string ship_add1,string ship_add2,string ship_add3, string _Applicantid, string _Applicantname,
       string _ApplicantAdd, string _ApplicantCity, string _ApplicantPincode, string _LcRefno, string _BenName, string _country, string _AirwaysBillNo1, string _AirwaysBillNo2,
        string _AirwaysBillDate, string _AirwaysBillDate2, string _AirlinesCompName, string _flightNo1, string _flightDate1,
        string _flightNo2, string _flightDate2, string _ShipperName, string _SupplierName, string _Consignee_Name,
        string _Notify_Party, string _Descofgoods, string _Quantity, string _ShippingMarks, string _Currency, string _BillAmt,
        string _Policy, string _OurRefNo, string _Gen_opr, string _Ahm,
        string _GO_Comment, string _GO_sectionno, string _GO_Remarks, string _GO_Memo, string _GO_Schemeno,

        string _GO_DebitCredit_Code1, string _GO_Ccy1, string _GO_Amount1, string _GO_Custcode1, string _GO_Accode1,
         string _GO_Accountno1, string _GO_Excrate1, string _GO_ExcCCy1, string _GO_Fund1,
        string _GO_Checkno1, string _GO_Available1, string _GO_Adviceprint1, string _GO_Details1, string _GO_Entity1, string _GO_Division1,
        string _GO_Interamount1, string _Go_InterRate1,

        string _GO_DebitCredit_Code2, string _GO_Ccy2, string _GO_Amount2, string _GO_Custcode2, string _GO_Accode2,
        string _GO_Accountno2, string _GO_Excrate2, string _GO_ExcCCy2, string _GO_Fund2,
        string _GO_Checkno2, string _GO_Available2, string _GO_Adviceprint2, string _GO_Details2, string _GO_Entity2, string _GO_Division2,
        string _GO_Interamount2, string _Go_InterRate2)
         {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_txtValueDate = new SqlParameter("@Valuedate", _txtValueDate.ToUpper());
        SqlParameter P_BranchCode = new SqlParameter("@BranchCode", _Branch.ToUpper());
        SqlParameter P_delorderno = new SqlParameter("@Deliveryorder", _delorderno.ToUpper());
        SqlParameter P_delorderno1 = new SqlParameter("@Deliveryorder1", _delorderno1.ToUpper());
        SqlParameter P_delorderno2 = new SqlParameter("@Deliveryorder2", _delorderno2.ToUpper());
        SqlParameter P_brodate = new SqlParameter("@Brodate", _brodate.ToUpper());
        SqlParameter P_ship_name = new SqlParameter("@ShipName", ship_name.ToUpper());
        SqlParameter P_ship_add = new SqlParameter("@Ship_ADDRESS", ship_add.ToUpper());
        SqlParameter P_ship_add1 = new SqlParameter("@Ship_ADDRESS1", ship_add1.ToUpper());
        SqlParameter P_ship_add2 = new SqlParameter("@Ship_ADDRESS2", ship_add2.ToUpper());
        SqlParameter P_ship_add3 = new SqlParameter("@Ship_ADDRESS3", ship_add3.ToUpper());

        SqlParameter P_Applicantid = new SqlParameter("@Applicant_id", _Applicantid.ToUpper());
        SqlParameter P_Applicantname = new SqlParameter("@Applicant_Name", _Applicantname.ToUpper());
        SqlParameter P_ApplicantAdd = new SqlParameter("@Applicant_Address", _ApplicantAdd.ToUpper());

        SqlParameter P_ApplicantCity = new SqlParameter("@Applicant_City", _ApplicantCity.ToUpper());
        SqlParameter P_ApplicantPincode = new SqlParameter("@Applicant_Pincode", _ApplicantPincode.ToUpper());

        SqlParameter P_LcRefno = new SqlParameter("@LC_Ref_NO", _LcRefno.ToUpper());
        SqlParameter P_BenName = new SqlParameter("@Beneficiary_Name", _BenName.ToUpper());
        SqlParameter P_country = new SqlParameter("@Country_Code", _country.ToUpper());
        SqlParameter P_AirwaysBillNo1 = new SqlParameter("@Airways_Billno1", _AirwaysBillNo1.ToUpper());
        SqlParameter P_AirwaysBillNo2 = new SqlParameter("@Airways_Billno2", _AirwaysBillNo2.ToUpper());
        SqlParameter P_AirwaysBillDate = new SqlParameter("@Airways_date", _AirwaysBillDate.ToUpper());
        SqlParameter P_AirwaysBillDate2 = new SqlParameter("@Airways_date2", _AirwaysBillDate2.ToUpper());
        SqlParameter P_AirlinesCompName = new SqlParameter("@Airlines_Comp_Name", _AirlinesCompName.ToUpper());
        SqlParameter P_flightNo1 = new SqlParameter("@Flight_No1", _flightNo1.ToUpper());
        SqlParameter P_flightDate1 = new SqlParameter("@Flight_Date1", _flightDate1.ToUpper());
        SqlParameter P_flightNo2 = new SqlParameter("@Flight_No2", _flightNo2.ToUpper());
        SqlParameter P_flightDate2 = new SqlParameter("@Flight_Date2", _flightDate2.ToUpper());
        SqlParameter P_ShipperName = new SqlParameter("@Shipper_Name", _ShipperName.ToUpper());
        SqlParameter P_SupplierName = new SqlParameter("@Supplier_Name", _SupplierName.ToUpper());
        SqlParameter P_Consignee_Name = new SqlParameter("@Consignee_Name", _Consignee_Name.ToUpper());
        SqlParameter P_Notify_Party = new SqlParameter("@Notify_Party", _Notify_Party.ToUpper());
        SqlParameter P_Descofgoods = new SqlParameter("@Desc_Of_Goods", _Descofgoods.ToUpper());
        SqlParameter P_Quantity = new SqlParameter("@Quantity", _Quantity.ToUpper());
        SqlParameter P_ShippingMarks = new SqlParameter("@Shipping_marks", _ShippingMarks.ToUpper());
        SqlParameter P_Currency = new SqlParameter("@currency", _Currency.ToUpper());
        SqlParameter P_BillAmt = new SqlParameter("@Bill_Amt", _BillAmt.ToUpper());
        SqlParameter P_Policy = new SqlParameter("@Policy", _Policy.ToUpper());
        SqlParameter P_OurRefNo = new SqlParameter("@Our_Ref_no", _OurRefNo.ToUpper());
        SqlParameter P_Gen_opr = new SqlParameter("@Gen_Opr_Flag", _Gen_opr.ToUpper());
        SqlParameter P_Ahm = new SqlParameter("@AHM_Flag", _Ahm.ToUpper());

        SqlParameter P_GO_Comment = new SqlParameter("@GO_Comment", _GO_Comment.ToUpper());
        SqlParameter P_GO_sectionno = new SqlParameter("@GO_SectionNo", _GO_sectionno.ToUpper());
        SqlParameter P_GO_Remarks = new SqlParameter("@GO_Remark", _GO_Remarks.ToUpper());
        SqlParameter P_GO_Memo = new SqlParameter("@GO_Memo", _GO_Memo.ToUpper());
        SqlParameter P_GO_Schemeno = new SqlParameter("@GO_SchemeNo", _GO_Schemeno.ToUpper());

        SqlParameter P_GO_DebitCredit_Code1 = new SqlParameter("@GO_DebitCredit_Code1", _GO_DebitCredit_Code1.ToUpper());
        SqlParameter P_GO_Ccy1 = new SqlParameter("@GO_DebitCredit_CCY1", _GO_Ccy1.ToUpper());
        SqlParameter P_GO_Amount1 = new SqlParameter("@GO_DebitCredit_Amt1", _GO_Amount1.ToUpper());
        SqlParameter P_GO_Custcode1 = new SqlParameter("@GO_DebitCredit_Cust_Code1", _GO_Custcode1.ToUpper());
        SqlParameter P_GO_Accode1 = new SqlParameter("@GO_DebitCredit_Cust_AccCode1", _GO_Accode1.ToUpper());
        SqlParameter P_GO_Accountno1 = new SqlParameter("@GO_DebitCredit_Cust_AccNo1", _GO_Accountno1.ToUpper());
        SqlParameter P_GO_Excrate1 = new SqlParameter("@GO_DebitCredit_ExchRate1", _GO_Excrate1.ToUpper());
        SqlParameter P_GO_ExcCCy1 = new SqlParameter("@GO_DebitCredit_ExchCCY1", _GO_ExcCCy1.ToUpper());
        SqlParameter P_GO_Fund1 = new SqlParameter("@GO_DebitCredit_Fund1", _GO_Fund1.ToUpper());
        SqlParameter P_GO_Checkno1 = new SqlParameter("@GO_DebitCredit_CheckNo1", _GO_Checkno1.ToUpper());
        SqlParameter P_GO_Available1 = new SqlParameter("@GO_DebitCredit_Available1", _GO_Available1.ToUpper());
        SqlParameter P_GO_Adviceprint1 = new SqlParameter("@GO_DebitCredit_Advice_Print1", _GO_Adviceprint1.ToUpper());
        SqlParameter P_GO_Details1 = new SqlParameter("@GO_DebitCredit_Details1", _GO_Details1.ToUpper());
        SqlParameter P_GO_Entity1 = new SqlParameter("@GO_DebitCredit_Entity1", _GO_Entity1.ToUpper());
        SqlParameter P_GO_Division1 = new SqlParameter("@GO_DebitCredit_Division1", _GO_Division1.ToUpper());
        SqlParameter P_GO_Interamount1 = new SqlParameter("@GO_DebitCredit_InterAmt1", _GO_Interamount1.ToUpper());
        SqlParameter P_Go_InterRate1 = new SqlParameter("@GO_DebitCredit_InterRate1", _Go_InterRate1.ToUpper());

        SqlParameter P_GO_DebitCredit_Code2 = new SqlParameter("@GO_DebitCredit_Code2", _GO_DebitCredit_Code2.ToUpper());
        SqlParameter P_GO_Ccy2 = new SqlParameter("@GO_DebitCredit_CCY2", _GO_Ccy2.ToUpper());
        SqlParameter P_GO_Amount2 = new SqlParameter("@GO_DebitCredit_Amt2", _GO_Amount2.ToUpper());
        SqlParameter P_GO_Custcode2 = new SqlParameter("@GO_DebitCredit_Cust_Code2", _GO_Custcode2.ToUpper());
     
        SqlParameter P_GO_Accode2 = new SqlParameter("@GO_DebitCredit_Cust_AccCode2", _GO_Accode2.ToUpper());
    
        SqlParameter P_GO_Accountno2 = new SqlParameter("@GO_DebitCredit_Cust_AccNo2", _GO_Accountno2.ToUpper());
        SqlParameter P_GO_Excrate2 = new SqlParameter("@GO_DebitCredit_ExchRate2", _GO_Excrate2.ToUpper());
        SqlParameter P_GO_ExcCCy2 = new SqlParameter("@GO_DebitCredit_ExchCCY2", _GO_ExcCCy2.ToUpper());
        SqlParameter P_GO_Fund2 = new SqlParameter("@GO_DebitCredit_Fund2", _GO_Fund2.ToUpper());
        SqlParameter P_GO_Checkno2 = new SqlParameter("@GO_DebitCredit_CheckNo2", _GO_Checkno2.ToUpper());
        SqlParameter P_GO_Available2 = new SqlParameter("@GO_DebitCredit_Available2", _GO_Available2.ToUpper());
        SqlParameter P_GO_Adviceprint2 = new SqlParameter("@GO_DebitCredit_Advice_Print2", _GO_Adviceprint2.ToUpper());
        SqlParameter P_GO_Details2 = new SqlParameter("@GO_DebitCredit_Details2", _GO_Details2.ToUpper());
        SqlParameter P_GO_Entity2 = new SqlParameter("@GO_DebitCredit_Entity2", _GO_Entity2.ToUpper());
        SqlParameter P_GO_Division2 = new SqlParameter("@GO_DebitCredit_Division2", _GO_Division2.ToUpper());
        SqlParameter P_GO_Interamount2 = new SqlParameter("@GO_DebitCredit_InterAmt2", _GO_Interamount2.ToUpper());
        SqlParameter P_Go_InterRate2 = new SqlParameter("@GO_DebitCredit_InterRate2", _Go_InterRate2.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        string _Result = obj.SaveDeleteData("TF_Imp_UpdateBRO", P_txtValueDate, P_BranchCode, P_delorderno, P_delorderno1, P_delorderno2, P_brodate, P_ship_name, P_ship_add, P_ship_add1, P_ship_add2, P_ship_add3,P_Applicantid, P_Applicantname,
            P_ApplicantAdd, P_ApplicantCity, P_ApplicantPincode, P_country, P_BenName,P_LcRefno, P_AirwaysBillNo1, P_AirwaysBillNo2, P_AirwaysBillDate, P_AirwaysBillDate2, P_Gen_opr, P_Ahm, P_AirlinesCompName, P_flightNo1,
            P_flightNo2,P_flightDate1,P_flightDate2,P_ShipperName,P_SupplierName,P_Consignee_Name,P_Notify_Party,P_Descofgoods,P_Quantity,P_ShippingMarks,P_Currency,P_BillAmt,
            P_Policy,P_OurRefNo,P_AddedBy,P_AddedDate,P_UpdatedBy,P_UpdatedDate,
            P_GO_Comment, P_GO_sectionno, P_GO_Remarks, P_GO_Memo, P_GO_Schemeno, P_GO_DebitCredit_Code1, P_GO_Ccy1, P_GO_Amount1, P_GO_Custcode1, P_GO_Accode1,
             P_GO_Accountno1, P_GO_Excrate1, P_GO_ExcCCy1, P_GO_Fund1, P_GO_Checkno1, P_GO_Available1, P_GO_Adviceprint1, P_GO_Details1, P_GO_Entity1, P_GO_Division1,
            P_GO_Interamount1, P_Go_InterRate1,
            P_GO_DebitCredit_Code2, P_GO_Ccy2, P_GO_Amount2, P_GO_Custcode2, P_GO_Accode2,
             P_GO_Accountno2, P_GO_Excrate2, P_GO_ExcCCy2, P_GO_Fund2, P_GO_Checkno2, P_GO_Available2, P_GO_Adviceprint2, P_GO_Details2, P_GO_Entity2, P_GO_Division2,
            P_GO_Interamount2, P_Go_InterRate2 );
        return _Result;
     }
    

    //private void getBroNo()
    //{
    //    string year = System.DateTime.Today.Year.ToString();
    //    string year1 = year.Substring(2, 2);
    //    txtdosrno2.Text = year1;
    //    TF_DATA obj = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@branchcode", SqlDbType.VarChar);
    //    p1.Value = ddlBranch.SelectedValue;

    //    SqlParameter p2 = new SqlParameter("@year", SqlDbType.VarChar);
    //    p2.Value = txtdosrno2.Text;

    //    string _lastBroNo = obj.SaveDeleteData("TF_IMP_BRO_GetMaxNo", p1, p2);
    //    txtdosrno2.Text = _lastBroNo;
    //}

    private void getBroNo()
    {
        string year = System.DateTime.Today.Year.ToString();
        string year1 = year.Substring(2, 2);
        year1 = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));
        txtdosrno2.Text = year1;
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@branchcode", SqlDbType.VarChar);
        p1.Value = ddlBranch.SelectedValue;

        SqlParameter p2 = new SqlParameter("@year", SqlDbType.VarChar);
        p2.Value = txtdosrno2.Text;

        string _lastBroNo = obj.SaveDeleteData("TF_IMP_BRO_GetMaxNo", p1, p2);
        txtdosrno2.Text = _lastBroNo;
    }

    public void fillDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Deliverorderno", Request.QueryString["Delivery_Order_No"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_BROViewDetails", PDocNo);
        if (dt.Rows.Count > 0)
        {
            ddlBranch.Enabled = false;
            txtdate.Enabled = false;
            txtValueDate.Enabled = false;
            txtref.Enabled = false;
            txtconsignee.Enabled = false;
            txtdosrno.Enabled = false;
            txtdosrno1.Enabled = false;
            txtdosrno2.Enabled = false;
            txtValueDate.Text = dt.Rows[0]["Value_Date"].ToString();
            txtdate.Text = dt.Rows[0]["BRO_date"].ToString();
            txtshipname.Text = dt.Rows[0]["Shipping_Co_Name"].ToString();
            txtshipcoadd.Text = dt.Rows[0]["Shipping_Co_Address"].ToString();
            txtshipcoadd1.Text = dt.Rows[0]["Shipping_Co_Address1"].ToString();
            txtshipcoadd2.Text = dt.Rows[0]["Shipping_Co_Address2"].ToString();
            txtshipcoadd3.Text = dt.Rows[0]["Shipping_Co_Address3"].ToString();
            txtApplid.Text = dt.Rows[0]["Applicant_Id"].ToString();
            lblApplName.Text = dt.Rows[0]["Applicant_Name"].ToString();
            txtApplAdd.Text = dt.Rows[0]["Applicant_Address"].ToString();
            txtApplCity.Text = dt.Rows[0]["Applicant_City"].ToString();
            txtApplPincode.Text = dt.Rows[0]["Applicant_Pincode"].ToString();
            txtlcrefno.Text = dt.Rows[0]["LC_Ref_No"].ToString();
            txtbenefname.Text = dt.Rows[0]["Beneficiary_Name"].ToString();
            txtcountry.Text = dt.Rows[0]["Country_Code"].ToString();
            txtbill1.Text = dt.Rows[0]["Airways_BillNo1"].ToString();
            txtbill2.Text = dt.Rows[0]["Airways_BillNo2"].ToString();
            txtbilldate.Text = dt.Rows[0]["Airways_Bill_Date1"].ToString();
            txtbilldate2.Text = dt.Rows[0]["Airways_Bill_Date2"].ToString();
            txtAircompname.Text = dt.Rows[0]["Airlines_Comp_Name"].ToString();
            txtflightno1.Text = dt.Rows[0]["Flight_No1"].ToString();
            txtflightno2.Text = dt.Rows[0]["Flight_No2"].ToString();
            txtairlinedate1.Text = dt.Rows[0]["Flight_Date1"].ToString();
            txtairlinedate2.Text = dt.Rows[0]["Flight_Date2"].ToString();
            txtshipper.Text = dt.Rows[0]["Shipper_Name"].ToString();
            txtsupplier.Text = dt.Rows[0]["Supplier_Name"].ToString();
            txtconsignee.Text = dt.Rows[0]["Consignee_Name"].ToString();
            txtnotifyname.Text = dt.Rows[0]["Notify_Party"].ToString();
            txtdescofgoods.Text = dt.Rows[0]["Desc_Of_Goods"].ToString();
            txtquantity.Text = dt.Rows[0]["Quantity"].ToString();
            txtshipmarks.Text = dt.Rows[0]["Shipping_Marks"].ToString();
            txtcurrency.Text = dt.Rows[0]["Currency"].ToString();
            txtbillamt.Text = dt.Rows[0]["Bill_Amt"].ToString().Replace(",", "");
            txtgoodspolicy.Text = dt.Rows[0]["Policy"].ToString();
            txtref.Text = dt.Rows[0]["Our_Ref_No"].ToString();
            if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
            }
            if (dt.Rows[0]["Gen_Operation_Flag"].ToString() == "Y")
            {
                Chk_GenOprtn.Checked = true;
                PanelGO_Bill_Handling.Visible = true;
                txt_GO1_Comment.Text = dt.Rows[0]["GO_Comment"].ToString();
                txt_GO1_SectionNo.Text = dt.Rows[0]["GO_SectionNo"].ToString();
                txt_GO1_Remarks.Text = dt.Rows[0]["GO_Remark"].ToString();
                txt_GO1_Memo.Text = dt.Rows[0]["GO_Memo"].ToString();
                txt_GO1_Scheme_no.Text = dt.Rows[0]["GO_SchemeNo"].ToString();

                txt_GO1_Debit_Code.Text = dt.Rows[0]["GO_Debit/Credit_Code1"].ToString();
                txt_GO1_Debit_Curr.Text = dt.Rows[0]["GO_Debit/Credit_CCY1"].ToString();
                txt_GO1_Debit_Amt.Text = dt.Rows[0]["GO_Debit/Credit_Amt1"].ToString();
                txt_GO1_Debit_Cust.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Code1"].ToString();
              //  txt_GO1_Debit_Cust_Name.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Name1"].ToString();
                txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode1"].ToString();
              //  txt_GO1_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AcCode1_Name"].ToString();
                txt_GO1_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccNo1"].ToString();
                txt_GO1_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Debit/Credit_ExchRate1"].ToString();
                txt_GO1_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Debit/Credit_ExchCCY1"].ToString();
                txt_GO1_Debit_FUND.Text = dt.Rows[0]["GO_Debit/Credit_Fund1"].ToString();
                txt_GO1_Debit_Check_No.Text = dt.Rows[0]["GO_Debit/Credit_CheckNo1"].ToString();
                txt_GO1_Debit_Available.Text = dt.Rows[0]["GO_Debit/Credit_Available1"].ToString();
                txt_GO1_Debit_AdPrint.Text = dt.Rows[0]["GO_Debit/Credit_Advice_Print1"].ToString();
                txt_GO1_Debit_Details.Text = dt.Rows[0]["GO_Debit/Credit_Details1"].ToString();
                txt_GO1_Debit_Entity.Text = dt.Rows[0]["GO_Debit/Credit_Entity1"].ToString();
                txt_GO1_Debit_Division.Text = dt.Rows[0]["GO_Debit/Credit_Division1"].ToString();
                txt_GO1_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Debit/Credit_InterAmt1"].ToString();
                txt_GO1_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Debit/Credit_InterRate1"].ToString();

                txt_GO1_Credit_Code.Text = dt.Rows[0]["GO_Debit/Credit_Code2"].ToString();
                txt_GO1_Credit_Curr.Text = dt.Rows[0]["GO_Debit/Credit_CCY2"].ToString();
                txt_GO1_Credit_Amt.Text = dt.Rows[0]["GO_Debit/Credit_Amt2"].ToString();
                txt_GO1_Credit_Cust.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Code2"].ToString();
              //  txt_GO1_Credit_Cust_Name.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Name2"].ToString();
                txt_GO1_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode2"].ToString();
              //  txt_GO1_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AcCode2_Name"].ToString();
                txt_GO1_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccNo2"].ToString();
                txt_GO1_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Debit/Credit_ExchRate2"].ToString();
                txt_GO1_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Debit/Credit_ExchCCY2"].ToString();
                txt_GO1_Credit_FUND.Text = dt.Rows[0]["GO_Debit/Credit_Fund2"].ToString();
                txt_GO1_Credit_Check_No.Text = dt.Rows[0]["GO_Debit/Credit_CheckNo2"].ToString();
                txt_GO1_Credit_Available.Text = dt.Rows[0]["GO_Debit/Credit_Available2"].ToString();
                txt_GO1_Credit_AdPrint.Text = dt.Rows[0]["GO_Debit/Credit_Advice_Print2"].ToString();
                txt_GO1_Credit_Details.Text = dt.Rows[0]["GO_Debit/Credit_Details2"].ToString();
                txt_GO1_Credit_Entity.Text = dt.Rows[0]["GO_Debit/Credit_Entity2"].ToString();
                txt_GO1_Credit_Division.Text = dt.Rows[0]["GO_Debit/Credit_Division2"].ToString();
                txt_GO1_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Debit/Credit_InterAmt2"].ToString();
                txt_GO1_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Debit/Credit_InterRate2"].ToString();
            }
            if (dt.Rows[0]["AHM_Flag"].ToString() == "Y")
            {
                Chk_Ahm.Checked = true;
            }
        }
        else { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@Deliveryorder", txtdosrno.Text.Trim());
        SqlParameter P2 = new SqlParameter("@Deliveryorder1", txtdosrno1.Text.Trim());
        SqlParameter P3 = new SqlParameter("@Deliveryorder2", txtdosrno2.Text.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_BroSubmitForChecker", P1, P2, P3);
        if (Result == "Submit")
        {
            string _script = "";
            _script = "window.location='TF_IMP_ViewBRO.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
     }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
    }
    protected void fillBranch1()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = txtdosrno1.Text.Trim();
        string _query = "TF_GetBranch1";
        DataTable dt = objData.getData(_query, p1);
        //ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
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

    protected void txtApplid_TextChanged(object sender, EventArgs e)
    {
        fillCustomerDescription();
    }
    public void fillCustomerDescription()
    {
         TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@App_id", SqlDbType.VarChar);
        p1.Value = txtApplid.Text;
        SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text.ToString());
        string _query = "TF_IMP_BRO_GetCustomerDetails";
        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            lblApplName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtApplAdd.Text = dt.Rows[0]["CUST_ADDRESS"].ToString().Trim();
            txtApplCity.Text = dt.Rows[0]["CUST_CITY"].ToString().Trim();
            txtApplPincode.Text = dt.Rows[0]["CUST_PINCODE"].ToString().Trim();
            txtcountry.Text = dt.Rows[0]["CUST_COUNTRY"].ToString().Trim();
            txtnotifyname.Text=lblApplName.Text;
            txt_GO1_Debit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txt_GO1_Debit_Cust_AccNo.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Trim();
            txt_GO1_Credit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
        }
        else
        {
            lblApplName.Text = "";
            txtApplAdd.Text = "";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_ViewBRO.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_ViewBRO.aspx", true);
    }

    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.SelectedItem.Text.Trim();
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtdosrno1.Text = dt.Rows[0]["BranchCode"].ToString().Trim();
            getBroNo();
        }
        else
        {
            ddlBranch.Text = "";
            txtdosrno2.Text = "";
        }
    }

    protected void Chk_GenOprtn_OnCheckedChanged(object sender, EventArgs e)
    {
        if (Chk_GenOprtn.Checked == false)
        {
            PanelGO_Bill_Handling.Visible = false;
        }
        else if (Chk_GenOprtn.Checked == true)
        {
            PanelGO_Bill_Handling.Visible = true;
        }
    }
    [WebMethod]
    public static Fields SubmitToChecker(string DocNo1, string DocNo2, string DocNo3)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo1 = new SqlParameter("@Deliveryorder", DocNo1);
        SqlParameter P_DocNo2 = new SqlParameter("@Deliveryorder1", DocNo2);
        SqlParameter P_DocNo3 = new SqlParameter("@Deliveryorder2", DocNo3);
        fields.CheckerStatus = obj.SaveDeleteData("TF_IMP_BroSubmitForChecker", P_DocNo1, P_DocNo2, P_DocNo3);
        return fields;
    }
    public class Fields
    {
        public string CheckerStatus { get; set; }
    }
}