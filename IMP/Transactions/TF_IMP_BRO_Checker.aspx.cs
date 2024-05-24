using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML.Excel;

public partial class IMP_Transactions_TF_IMP_BRO_Checker : System.Web.UI.Page
{
    string valuedate;
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
                TabContainerMain.ActiveTab = tbBRODetails;
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_ViewBRO.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "Add")
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
                            MakeReadOnly();
                            fillBranch();
                        }
                        if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                        {
                            ddlApproveReject.Enabled = false;
                        }
                    }
                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    public void MakeReadOnly()
    {
        txtValueDate.Enabled = false;
        ddlBranch.Enabled = false;
        txtdate.Enabled = false;
        txtshipname.Enabled = false;
        txtshipcoadd.Enabled = false;
        txtshipcoadd1.Enabled = false;
        txtshipcoadd2.Enabled = false;
        txtshipcoadd3.Enabled = false;
        txtApplid.Enabled = false;
        lblApplName.Enabled = false;
        txtApplAdd.Enabled = false;
        txtApplCity.Enabled = false;
        txtApplPincode.Enabled = false;
        txtlcrefno.Enabled = false;
        txtbenefname.Enabled = false;
        txtcountry.Enabled = false;
        txtbill1.Enabled = false;
        txtbill2.Enabled = false;
        txtbilldate.Enabled = false;
        txtbilldate2.Enabled = false;
        txtAircompname.Enabled = false;
        txtflightno1.Enabled = false;
        txtflightno2.Enabled = false;
        txtairlinedate1.Enabled = false;
        txtairlinedate2.Enabled = false;
        txtshipper.Enabled = false;
        txtsupplier.Enabled = false;
        txtconsignee.Enabled = false;
        txtnotifyname.Enabled = false;
        txtdescofgoods.Enabled = false;
        txtquantity.Enabled = false;
        txtshipmarks.Enabled = false;
        txtcurrency.Enabled = false;
        txtbillamt.Enabled = false;
        txtgoodspolicy.Enabled = false;
        txtref.Enabled = false;
        Chk_GenOprtn.Enabled = false;
        txt_GO1_Comment.Enabled = false;
        txt_GO1_SectionNo.Enabled = false;
        txt_GO1_Remarks.Enabled = false;
        txt_GO1_Memo.Enabled = false;
        txt_GO1_Scheme_no.Enabled = false;
        Chk_Ahm.Enabled = false;
        txt_GO1_Credit_AdPrint.Enabled = false;
        txt_GO1_Credit_Amt.Enabled = false;
        txt_GO1_Credit_Available.Enabled = false;
        txt_GO1_Credit_Check_No.Enabled = false;
        txt_GO1_Credit_Code.Enabled = false;
        txt_GO1_Credit_Curr.Enabled = false;
        txt_GO1_Credit_Cust.Enabled = false;
        txt_GO1_Credit_Cust_AccNo.Enabled = false;
        txt_GO1_Credit_Cust_AcCode.Enabled = false;
        txt_GO1_Credit_Details.Enabled = false;
        txt_GO1_Credit_Division.Enabled = false;
        txt_GO1_Credit_Entity.Enabled = false;
        txt_GO1_Credit_Exch_Curr.Enabled = false;
        txt_GO1_Credit_Exch_Rate.Enabled = false;
        txt_GO1_Credit_FUND.Enabled = false;
        txt_GO1_Credit_Inter_Amount.Enabled = false;
        txt_GO1_Credit_Inter_Rate.Enabled = false;
        txt_GO1_Debit_AdPrint.Enabled = false;
        txt_GO1_Debit_Amt.Enabled = false;
        txt_GO1_Debit_Available.Enabled = false;
        txt_GO1_Debit_Check_No.Enabled = false;
        txt_GO1_Debit_Curr.Enabled = false;
        txt_GO1_Debit_Code.Enabled = false;
        txt_GO1_Debit_Cust.Enabled = false;
        txt_GO1_Debit_Cust_AccNo.Enabled = false;
        txt_GO1_Debit_Cust_AcCode.Enabled = false;
        txt_GO1_Debit_Details.Enabled = false;
        txt_GO1_Debit_Division.Enabled = false;
        txt_GO1_Debit_Entity.Enabled = false;
        txt_GO1_Debit_Exch_CCY.Enabled = false;
        txt_GO1_Debit_Exch_Rate.Enabled = false;
        txt_GO1_Debit_FUND.Enabled = false;
        txt_GO1_Debit_Inter_Amount.Enabled = false;
        txt_GO1_Debit_Inter_Rate.Enabled = false;
       
    }

    public void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
       
        SqlParameter PDocNo = new SqlParameter("@Deliverorderno", Request.QueryString["Delivery_Order_No"].ToString());
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            valuedate = DateTime.Now.ToString("dd/MM/yyyy");
           // BROFileCreation();
            if (Chk_GenOprtn.Checked)
            {
                GO1FileCreation();
            }
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        if (AR == "A")
        {
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_ValDate = new SqlParameter("@ValueDate", valuedate);
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveBROData", PDocNo, P_Status, P_ValDate, P_RejectReason);

            Response.Redirect("TF_IMP_BRO_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        }
        else {
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectBROData", PDocNo, P_Status, P_RejectReason);

            Response.Redirect("TF_IMP_BRO_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        
        }
    }
    public void BROFileCreation()
    {
        string bro_no = Request.QueryString["Delivery_Order_No"].ToString();
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", Request.QueryString["Delivery_Order_No"].ToString());
        DataTable dt = objData1.getData("TF_IMP_BRO_ExcelFile_ForAcceptance", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/BRO/BRODETAILS/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + bro_no + ".xls";
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
            }
        }
        else
        {
        }
    }
    public void GO1FileCreation()
    {
        string brono = Request.QueryString["Delivery_Order_No"].ToString();
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", Request.QueryString["Delivery_Order_No"].ToString());
        DataTable dt = objData1.getData("TF_IMP_BRO_GO1_ExcelFile_ForAcceptance", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/BRO/GeneralOperation/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + brono + "_GO1" + ".xlsx";
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
            }
        }
        else
        {
        }
    }

    public void fillDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Deliverorderno", Request.QueryString["Delivery_Order_No"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_BROViewDetails", PDocNo);
        if (dt.Rows.Count > 0)
        {
            ddlBranch.Text=dt.Rows[0]["Branch_Code"].ToString();
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
            txtbillamt.Text = dt.Rows[0]["Bill_Amt"].ToString();
            txtgoodspolicy.Text = dt.Rows[0]["Policy"].ToString();
            txtref.Text = dt.Rows[0]["Our_Ref_No"].ToString();
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
                txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode1"].ToString();
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
                txt_GO1_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode2"].ToString();
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
   
}