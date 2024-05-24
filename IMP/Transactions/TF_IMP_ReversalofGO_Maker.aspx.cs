using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;

public partial class IMP_Transactions_TF_IMP_ReversalofGO_Maker : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_ReversalofGO_Maker_View.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() == "edit")
                    {
                       txtDocNo.Text = Request.QueryString["Delivery_Order_No"].Trim();
                       txtAppl_Name.Text = Request.QueryString["Applicant_Name"].Trim();
                       txtBrodate.Text = Request.QueryString["BRO_Date"].Trim();
                       txtBill_Amt.Text = Request.QueryString["Bill_Amt"].Trim();
                       txtccy.Text = Request.QueryString["Currency"].Trim();
                       fillDetails();
                       MakeReadOnly();
                    }
                }
            }
        }
    }

    public void fillDetails()
    { 
         TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Deliverorderno", Request.QueryString["Delivery_Order_No"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_RevOfGO_ViewDetails", PDocNo);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
            }
            txt_GO1_Comment.Text = dt.Rows[0]["GO_Comment"].ToString();
            txt_GO1_SectionNo.Text = dt.Rows[0]["GO_SectionNo"].ToString();
            txt_GO1_Remarks.Text = "Refund of BRO margin";
            txt_GO1_Memo.Text = dt.Rows[0]["GO_Memo"].ToString();
            txt_GO1_Scheme_no.Text = dt.Rows[0]["GO_SchemeNo"].ToString();

            txt_GO1_Debit_Code.Text = dt.Rows[0]["GO_Debit/Credit_Code1"].ToString();
            txt_GO1_Debit_Curr.Text = dt.Rows[0]["GO_Debit/Credit_CCY2"].ToString();
            txt_GO1_Debit_Amt.Text = dt.Rows[0]["GO_Debit/Credit_Amt2"].ToString();
            txt_GO1_Debit_Cust.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Code2"].ToString();
            txt_GO1_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode2"].ToString();
            txt_GO1_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccNo2"].ToString();
            txt_GO1_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Debit/Credit_ExchRate2"].ToString();
            txt_GO1_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Debit/Credit_ExchCCY2"].ToString();
            txt_GO1_Debit_FUND.Text = dt.Rows[0]["GO_Debit/Credit_Fund2"].ToString();
            txt_GO1_Debit_Check_No.Text = dt.Rows[0]["GO_Debit/Credit_CheckNo2"].ToString();
            txt_GO1_Debit_Available.Text = dt.Rows[0]["GO_Debit/Credit_Available2"].ToString();
            txt_GO1_Debit_AdPrint.Text = dt.Rows[0]["GO_Debit/Credit_Advice_Print2"].ToString();
            txt_GO1_Debit_Details.Text = dt.Rows[0]["GO_Debit/Credit_Details2"].ToString();
            txt_GO1_Debit_Entity.Text = dt.Rows[0]["GO_Debit/Credit_Entity2"].ToString();
            txt_GO1_Debit_Division.Text = dt.Rows[0]["GO_Debit/Credit_Division2"].ToString();
            txt_GO1_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Debit/Credit_InterAmt2"].ToString();
            txt_GO1_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Debit/Credit_InterRate2"].ToString();

            txt_GO1_Credit_Code.Text = dt.Rows[0]["GO_Debit/Credit_Code2"].ToString();
            txt_GO1_Credit_Curr.Text = dt.Rows[0]["GO_Debit/Credit_CCY1"].ToString();
            txt_GO1_Credit_Amt.Text = dt.Rows[0]["GO_Debit/Credit_Amt1"].ToString();
            txt_GO1_Credit_Cust.Text = dt.Rows[0]["GO_Debit/Credit_Cust_Code1"].ToString();
            txt_GO1_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccCode1"].ToString();
            txt_GO1_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Debit/Credit_Cust_AccNo1"].ToString();
            txt_GO1_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Debit/Credit_ExchRate1"].ToString();
            txt_GO1_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Debit/Credit_ExchCCY1"].ToString();
            txt_GO1_Credit_FUND.Text = dt.Rows[0]["GO_Debit/Credit_Fund1"].ToString();
            txt_GO1_Credit_Check_No.Text = dt.Rows[0]["GO_Debit/Credit_CheckNo1"].ToString();
            txt_GO1_Credit_Available.Text = dt.Rows[0]["GO_Debit/Credit_Available1"].ToString();
            txt_GO1_Credit_AdPrint.Text = dt.Rows[0]["GO_Debit/Credit_Advice_Print1"].ToString();
            txt_GO1_Credit_Details.Text = dt.Rows[0]["GO_Debit/Credit_Details1"].ToString();
            txt_GO1_Credit_Entity.Text = dt.Rows[0]["GO_Debit/Credit_Entity1"].ToString();
            txt_GO1_Credit_Division.Text = dt.Rows[0]["GO_Debit/Credit_Division1"].ToString();
            txt_GO1_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Debit/Credit_InterAmt1"].ToString();
            txt_GO1_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Debit/Credit_InterRate1"].ToString();
        }
        else { }
    }

    public void MakeReadOnly()
    {
        txt_GO1_Comment.Enabled = false;
        txt_GO1_SectionNo.Enabled = false;
       // txt_GO1_Remarks.Enabled = false;
        txt_GO1_Memo.Enabled = false;
        txt_GO1_Scheme_no.Enabled = false;

        txt_GO1_Credit_AdPrint.Enabled = false;
        txt_GO1_Credit_Amt.Enabled = false;
        txt_GO1_Credit_Available.Enabled = false;
        txt_GO1_Credit_Check_No.Enabled = false;
        txt_GO1_Credit_Code.Enabled = false;
        txt_GO1_Credit_Curr.Enabled = false;
        txt_GO1_Credit_Cust.Enabled = false;
        txt_GO1_Credit_Cust_AccNo.Enabled = false;
        txt_GO1_Credit_Cust_AcCode.Enabled = false;
       // txt_GO1_Credit_Details.Enabled = false;
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
       // txt_GO1_Debit_Details.Enabled = false;
        txt_GO1_Debit_Division.Enabled = false;
        txt_GO1_Debit_Entity.Enabled = false;
        txt_GO1_Debit_Exch_CCY.Enabled = false;
        txt_GO1_Debit_Exch_Rate.Enabled = false;
        txt_GO1_Debit_FUND.Enabled = false;
        txt_GO1_Debit_Inter_Amount.Enabled = false;
        txt_GO1_Debit_Inter_Rate.Enabled = false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_ReversalofGO_Maker_View.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@Deliveryorder", Request.QueryString["Delivery_Order_No"].ToString());
        string Result = obj.SaveDeleteData("TF_IMP_RevOfGO_SubmitForChecker", P1);
        if (Result == "Submit")
        {
            string _script = "";
            _script = "window.location='TF_IMP_ReversalofGO_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
    }
    [WebMethod]
    public static Fields SubmitToChecker(string DocNo1)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo1 = new SqlParameter("@Deliveryorder", DocNo1);
        
        fields.CheckerStatus = obj.SaveDeleteData("TF_IMP_RevOfGO_SubmitForChecker", P_DocNo1);
        return fields;
    }
    public class Fields
    {
        public string CheckerStatus { get; set; }
    }
}