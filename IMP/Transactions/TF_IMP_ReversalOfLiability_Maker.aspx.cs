using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

public partial class
    IMP_Transactions_TF_IMP_ReversalOfLiability_Maker : System.Web.UI.Page
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
            try
            {
                hdnUserName.Value = Session["userName"].ToString();
                if (!IsPostBack)
                {
                    if (Request.QueryString["DocNo"] == null)
                    {
                        Response.Redirect("TF_IMP_BOE_Maker_View.aspx", true);
                    }
                    else
                    {
                        txtRefNo.Text = Request.QueryString["DocNo"].Trim();
                        hdnBranchCode.Value = Request.QueryString["BranchCode"].Trim();
                        hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                        txtAmount.Focus();

                        TF_DATA obj = new TF_DATA();
                        DataTable dt = new DataTable();
                        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtRefNo.Text);
                        dt = obj.getData("TF_IMP_FillRevOfLibilityDetails", P_DocNo);
                        if (dt.Rows.Count > 0)
                        {
                            FillDetails();
                        }
                        else
                        {
                            FillBookingDetails();
                        }
                        txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    [WebMethod]
    public static string AddUpdate(string hdnUserName, string hdnBranchCode, string _txtRefNo, string _txtAmount, string _txtCurrency, string _txtCustomerAcNo, string _txtCCode, string _txtValueDate
        , string _txtExpiryDate, string _ddlAmendmentOption, string _txtAAmount, string _txtACurrency, string _txtRemarks, string _txtApplicationNo, string _txtApplyingBranch,
        string _txtAdvisingBank, string _ddlAttentionCode, string _txtCDCCY1, string _txtCDAmount1, string _txtCDCCY2, string _txtCDAmount2, string _txtCDCCY3,
        string _txtCDAmount3, string _txtDCACShortName1, string _txtDCCustAbbr1, string _txtDCAccountNo1, string _txtDCCCY1, string _txtDCAmount1, string _txtDCACShortName2,
        string _txtDCCustAbbr2, string _txtDCAccountNo2, string _txtDCCCY2, string _txtDCAmount2, string _txtDCACShortName3, string _txtDCCustAbbr3, string _txtDCAccountNo3,
        string _txtDCCCY3, string _txtDCAmount3)
    {
        SqlParameter P_hdnUserName = new SqlParameter("@hdnUserName", hdnUserName.Trim().ToUpper());
        SqlParameter P_hdnBranchCode = new SqlParameter("@BranchCode", hdnBranchCode.Trim().ToUpper());
        SqlParameter P_txtRefNo = new SqlParameter("@_txtRefNo", _txtRefNo.Trim().ToUpper());
        SqlParameter P_txtAmount = new SqlParameter("@_txtAmount", _txtAmount.Trim().ToUpper());
        SqlParameter P_txtCurrency = new SqlParameter("@_txtCurrency", _txtCurrency.Trim().ToUpper());
        SqlParameter P_txtCustomerAcNo = new SqlParameter("@_txtCustomerAcNo", _txtCustomerAcNo.Trim().ToUpper());
        SqlParameter P_txtCCode = new SqlParameter("@_txtCCode", _txtCCode.Trim().ToUpper());
        SqlParameter P_txtValueDate = new SqlParameter("@_txtValueDate", _txtValueDate.Trim().ToUpper());
        SqlParameter P_txtExpiryDate = new SqlParameter("@_txtExpiryDate", _txtExpiryDate.Trim().ToUpper());
        SqlParameter P_ddlAmendmentOption = new SqlParameter("@_ddlAmendmentOption", _ddlAmendmentOption.Trim().ToUpper());
        SqlParameter P_txtAAmount = new SqlParameter("@_txtAAmount", _txtAAmount.Trim().ToUpper());
        SqlParameter P_txtACurrency = new SqlParameter("@_txtACurrency", _txtACurrency.Trim().ToUpper());
        SqlParameter P_txtRemarks = new SqlParameter("@_txtRemarks", _txtRemarks.Trim().ToUpper());
        SqlParameter P_txtApplicationNo = new SqlParameter("@_txtApplicationNo", _txtApplicationNo.Trim().ToUpper());
        SqlParameter P_txtApplyingBranch = new SqlParameter("@_txtApplyingBranch", _txtApplyingBranch.Trim().ToUpper());
        SqlParameter P_txtAdvisingBank = new SqlParameter("@_txtAdvisingBank", _txtAdvisingBank.Trim().ToUpper());
        SqlParameter P_ddlAttentionCode = new SqlParameter("@_ddlAttentionCode", _ddlAttentionCode.Trim().ToUpper());
        SqlParameter P_txtCDCCY1 = new SqlParameter("@_txtCDCCY1", _txtCDCCY1.Trim().ToUpper());
        SqlParameter P_txtCDAmount1 = new SqlParameter("@_txtCDAmount1", _txtCDAmount1.Trim().ToUpper());
        SqlParameter P_txtCDCCY2 = new SqlParameter("@_txtCDCCY2", _txtCDCCY2.Trim().ToUpper());
        SqlParameter P_txtCDAmount2 = new SqlParameter("@_txtCDAmount2", _txtCDAmount2.Trim().ToUpper());
        SqlParameter P_txtCDCCY3 = new SqlParameter("@_txtCDCCY3", _txtCDCCY3.Trim().ToUpper());
        SqlParameter P_txtCDAmount3 = new SqlParameter("@_txtCDAmount3", _txtCDAmount3.Trim().ToUpper());
        SqlParameter P_txtDCACShortName1 = new SqlParameter("@_txtDCACShortName1", _txtDCACShortName1.Trim().ToUpper());
        SqlParameter P_txtDCCustAbbr1 = new SqlParameter("@_txtDCCustAbbr1", _txtDCCustAbbr1.Trim().ToUpper());
        SqlParameter P_txtDCAccountNo1 = new SqlParameter("@_txtDCAccountNo1", _txtDCAccountNo1.Trim().ToUpper());
        SqlParameter P_txtDCCCY1 = new SqlParameter("@_txtDCCCY1", _txtDCCCY1.Trim().ToUpper());
        SqlParameter P_txtDCAmount1 = new SqlParameter("@_txtDCAmount1", _txtDCAmount1.Trim().ToUpper());
        SqlParameter P_txtDCACShortName2 = new SqlParameter("@_txtDCACShortName2", _txtDCACShortName2.Trim().ToUpper());
        SqlParameter P_txtDCCustAbbr2 = new SqlParameter("@_txtDCCustAbbr2", _txtDCCustAbbr2.Trim().ToUpper());
        SqlParameter P_txtDCAccountNo2 = new SqlParameter("@_txtDCAccountNo2", _txtDCAccountNo2.Trim().ToUpper());
        SqlParameter P_txtDCCCY2 = new SqlParameter("@_txtDCCCY2", _txtDCCCY2.Trim().ToUpper());
        SqlParameter P_txtDCAmount2 = new SqlParameter("@_txtDCAmount2", _txtDCAmount2.Trim().ToUpper());
        SqlParameter P_txtDCACShortName3 = new SqlParameter("@_txtDCACShortName3", _txtDCACShortName3.Trim().ToUpper());
        SqlParameter P_txtDCCustAbbr3 = new SqlParameter("@_txtDCCustAbbr3", _txtDCCustAbbr3.Trim().ToUpper());
        SqlParameter P_txtDCAccountNo3 = new SqlParameter("@_txtDCAccountNo3", _txtDCAccountNo3.Trim().ToUpper());
        SqlParameter P_txtDCCCY3 = new SqlParameter("@_txtDCCCY3", _txtDCCCY3.Trim().ToUpper());
        SqlParameter P_txtDCAmount3 = new SqlParameter("@_txtDCAmount3", _txtDCAmount3.Trim().ToUpper());

        TF_DATA obj = new TF_DATA();
        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdateReversalOfLiability", P_hdnBranchCode, P_hdnUserName, P_txtRefNo, P_txtAmount, P_txtCurrency, P_txtCustomerAcNo, P_txtCCode, P_txtValueDate, P_txtExpiryDate, P_ddlAmendmentOption
, P_txtAAmount, P_txtACurrency, P_txtRemarks, P_txtApplicationNo, P_txtApplyingBranch, P_txtAdvisingBank, P_ddlAttentionCode
, P_txtCDCCY1, P_txtCDAmount1, P_txtCDCCY2, P_txtCDAmount2, P_txtCDCCY3, P_txtCDAmount3, P_txtDCACShortName1, P_txtDCCustAbbr1
, P_txtDCAccountNo1, P_txtDCCCY1, P_txtDCAmount1, P_txtDCACShortName2, P_txtDCCustAbbr2, P_txtDCAccountNo2, P_txtDCCCY2
, P_txtDCAmount2, P_txtDCACShortName3, P_txtDCCustAbbr3, P_txtDCAccountNo3, P_txtDCCCY3, P_txtDCAmount3);

        return _Result;
    }

    protected void FillBookingDetails()
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P_DocNo = new SqlParameter("@Document_No", txtRefNo.Text);
        dt = obj.getData("TF_IMP_Shipping_Guarantee_GetDetails", P_DocNo);
        if (dt.Rows.Count > 0)
        {
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            txtAmount.Text = dt.Rows[0]["Bill_Amt"].ToString();
            txtCustomerAcNo.Text = dt.Rows[0]["Cust_AccNo"].ToString();
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtExpiryDate.Text = dt.Rows[0]["Expiry_Date"].ToString();
        }
    }
    protected void FillDetails()
    {
        TF_DATA obj = new TF_DATA();
        DataTable dt = new DataTable();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtRefNo.Text);
        dt = obj.getData("TF_IMP_FillRevOfLibilityDetails", P_DocNo);
        if (dt.Rows.Count > 0)
        {
            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            txtCustomerAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtCCode.Text = dt.Rows[0]["CCode"].ToString();
            txtValueDate.Text = dt.Rows[0]["ValueDate"].ToString();
            txtExpiryDate.Text = dt.Rows[0]["ExpiryDate"].ToString();
            ddlAmendmentOption.Text = dt.Rows[0]["AmendmentOption"].ToString();
            txtAAmount.Text = dt.Rows[0]["AmenAmount"].ToString();
            txtACurrency.Text = dt.Rows[0]["AmenCurrency"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            txtApplicationNo.Text = dt.Rows[0]["ApplicationNo"].ToString();
            txtApplyingBranch.Text = dt.Rows[0]["ApplyingBranch"].ToString();
            txtAdvisingBank.Text = dt.Rows[0]["AdvisingBank"].ToString();
            ddlAttentionCode.Text = dt.Rows[0]["AttentionCode"].ToString();
            txtCDCCY1.Text = dt.Rows[0]["CDCCY1"].ToString();
            txtCDAmount1.Text = dt.Rows[0]["CDAmount1"].ToString();
            txtCDCCY2.Text = dt.Rows[0]["CDCCY2"].ToString();
            txtCDAmount2.Text = dt.Rows[0]["CDAmount2"].ToString();
            txtCDCCY3.Text = dt.Rows[0]["CDCCY3"].ToString();
            txtCDAmount3.Text = dt.Rows[0]["CDAmount3"].ToString();
            txtDCACShortName1.Text = dt.Rows[0]["DCACShortName1"].ToString();
            txtDCCustAbbr1.Text = dt.Rows[0]["DCCustAbbr1"].ToString();
            txtDCAccountNo1.Text = dt.Rows[0]["DCAccountNumber1"].ToString();
            txtDCCCY1.Text = dt.Rows[0]["DCCCY1"].ToString();
            txtDCAmount1.Text = dt.Rows[0]["DCAmount1"].ToString();
            txtDCACShortName2.Text = dt.Rows[0]["DCACShortName2"].ToString();
            txtDCCustAbbr2.Text = dt.Rows[0]["DCCustAbbr2"].ToString();
            txtDCAccountNo2.Text = dt.Rows[0]["DCAccountNumber2"].ToString();
            txtDCCCY2.Text = dt.Rows[0]["DCCCY2"].ToString();
            txtDCAmount2.Text = dt.Rows[0]["DCAmount2"].ToString();
            txtDCACShortName3.Text = dt.Rows[0]["DCACShortName3"].ToString();
            txtDCCustAbbr3.Text = dt.Rows[0]["DCCustAbbr3"].ToString();
            txtDCAccountNo3.Text = dt.Rows[0]["DCAccountNumber3"].ToString();
            txtDCCCY3.Text = dt.Rows[0]["DCCCY3"].ToString();
            txtDCAmount3.Text = dt.Rows[0]["DCAmount3"].ToString();
            if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
            {
                lblRejectReson.Visible = true;
                lblRejectReson.Text = "Reject Reason:- " + dt.Rows[0]["Reject_Remarks"].ToString();
            }
        }
        else
        {
            ClearFields();
        }
    }
    protected void txtRefNo_TextChanged(object sender, EventArgs e)
    {
        FillDetails();
    }
    protected void ClearFields()
    {
        txtAmount.Text = "";
        txtCurrency.Text = "";
        txtCustomerAcNo.Text = "";
        txtCCode.Text = "";
        txtValueDate.Text = "";
        txtExpiryDate.Text = "";
        ddlAmendmentOption.Text = "";
        txtAAmount.Text = "";
        txtACurrency.Text = "";
        txtRemarks.Text = "";
        txtApplicationNo.Text = "";
        txtApplyingBranch.Text = "";
        txtAdvisingBank.Text = "";
        ddlAttentionCode.Text = "";
        txtCDCCY1.Text = "";
        txtCDAmount1.Text = "";
        txtCDCCY2.Text = "";
        txtCDAmount2.Text = "";
        txtCDCCY3.Text = "";
        txtCDAmount3.Text = "";
        txtDCACShortName1.Text = "";
        txtDCCustAbbr1.Text = "";
        txtDCAccountNo1.Text = "";
        txtDCCCY1.Text = "";
        txtDCAmount1.Text = "";
        txtDCACShortName2.Text = "";
        txtDCCustAbbr2.Text = "";
        txtDCAccountNo2.Text = "";
        txtDCCCY2.Text = "";
        txtDCAmount2.Text = "";
        txtDCACShortName3.Text = "";
        txtDCCustAbbr3.Text = "";
        txtDCAccountNo3.Text = "";
        txtDCCCY3.Text = "";
        txtDCAmount3.Text = "";
    }
    [WebMethod]
    public static string SubmitToChecker(string UserName, string DocNo)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", DocNo);
        string Result = obj.SaveDeleteData("TF_IMP_RevOfLiabSubmitChecker", P_DocNo);
        ////IMPClass.CreateUserLogA(UserName, "Sent document(" + DocNo + ") to checker.");
        return Result;
    }
    protected void txtCustomerAcNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter CustACNo = new SqlParameter("@CustAcNo", txtCustomerAcNo.Text.Trim());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_RevOfLibFillCustDetails", CustACNo);
        if (dt.Rows.Count > 0)
        {
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            lblCustomerDesc.Text = "";
        }
    }
}