using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class XOS_XOS_AddEditExtensionData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("XOS_ViewExtensionData.aspx", true);
                }
                else
                {
                    fillTaxRates();
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBillDocumentNo.Text = Request.QueryString["DocNo"].Trim();
                        txtExtensionDocumentNo.Text = Request.QueryString["ExtensionDocNo"].Trim();
                        fillDetails(Request.QueryString["DocNo"].Trim(), "");
                        txtExtensionDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    }

                }
                txtExtensionDate.Attributes.Add("onblur", "return isValidDate(" + txtExtensionDate.ClientID + "," + "'Extension Date'" + " );");
                txtGrantedUpto.Attributes.Add("onblur", "return isValidDate(" + txtGrantedUpto.ClientID + "," + "'Date'" + " );");

                txtExtensionDocumentNo.Attributes.Add("onkeydown", "return false;");
                txtCustomerAcNo.Attributes.Add("onkeydown", "return false;");
                txtBillDate.Attributes.Add("onkeydown", "return false;");
                txtOverseasParty.Attributes.Add("onkeydown", "return false;");
                txtBillCurrency.Attributes.Add("onkeydown", "return false;");
                txtBillAmount.Attributes.Add("onkeydown", "return false;");
                txtAmountOutstanding.Attributes.Add("onkeydown", "return false;");
                txtBillDueDate.Attributes.Add("onkeydown", "return false;");
                txtBillDocumentNo.Attributes.Add("onkeydown", "return false;");

                txtReasonForExtension.Attributes.Add("onkeyup", "return CharCount(event," + txtReasonForExtension.ClientID + "," + lblCharCountReason.ClientID + ",200 );");
                txtAllowedUnderCriteria.Attributes.Add("onkeyup", "return CharCount(event," + txtAllowedUnderCriteria.ClientID + "," + lblCharCountCriteria.ClientID + ",200 );");
                txtRemarks.Attributes.Add("onkeyup", "return CharCount(event," + txtRemarks.ClientID + "," + lblCharCountRemarks.ClientID + ",100 );");
              
                txtExtensionNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtExtensionCharges.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSTaxAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTotalDebit.Attributes.Add("onkeydown", "return validate_Number(event);");
                
                btnExtensionNoList.Attributes.Add("onclick", "return OpenExtensionNoList();");
                txtExtensionNo.Attributes.Add("onblur", "return onBlurExtensionNo();");

                txtExtensionCharges.Attributes.Add("onblur", "return Calculate();");
                txtSTaxAmount.Attributes.Add("onblur", "return Calculate();");
                txtTotalDebit.Attributes.Add("onblur", "return Calculate();");
                
                btnSave.Attributes.Add("onclick", "return ValidateSave();");

                hdnMode.Value = "add";
                txtExtensionNo.Focus();

            }
        }
    }

    protected void fillTaxRates()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetTaxRates_INW";

        ddlServiceTax.Items.Clear();

        DataTable dt = objData.getData(_query);

        if (dt.Rows.Count > 0)
        {

            ddlServiceTax.DataSource = dt.DefaultView;
            ddlServiceTax.DataTextField = "TOTAL_SERVICE_TAX";
            ddlServiceTax.DataValueField = "TOTAL_SERVICE_TAX";
            ddlServiceTax.DataBind();

        }
    }
    protected void btnExtensionNo_Click(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = txtBillDocumentNo.Text;
        SqlParameter p2 = new SqlParameter("@ExtensionNo", SqlDbType.VarChar);
        p2.Value = txtExtensionNo.Text;
        string _query = "TF_EXP_XOS_GetExtensionEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            hdnMode.Value = "edit";
            txtExtensionDate.Text = dt.Rows[0]["DateOfExtension"].ToString().Trim();

            if (dt.Rows[0]["ExtnGrantingAuthority"].ToString().Trim() != "" || dt.Rows[0]["ExtnGrantingAuthority"].ToString().Trim() != null)
            {
                ddlGrantingAuthority.SelectedValue = dt.Rows[0]["ExtnGrantingAuthority"].ToString().Trim();
            }
            txtGrantedUpto.Text = dt.Rows[0]["ExtnGrantedUpto"].ToString().Trim();
            txtReasonForExtension.Text = dt.Rows[0]["ExtensionReason"].ToString().Trim();
            txtAllowedUnderCriteria.Text = dt.Rows[0]["AllowedUnderCriteria"].ToString().Trim();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString().Trim();

            string _Annex4recvd = dt.Rows[0]["Annex4received"].ToString().Trim();
            if (_Annex4recvd == "True")
                chkAnnex4recvd.Checked = true;
            txtExtensionCharges.Text = dt.Rows[0]["ExtensionCharges"].ToString().Trim();
            if (dt.Rows[0]["STax"].ToString().Trim() != "" || dt.Rows[0]["STax"].ToString().Trim() != null)
            {
                ddlServiceTax.SelectedValue = dt.Rows[0]["STax"].ToString().Trim();
            }
            txtSTaxAmount.Text = dt.Rows[0]["STaxAmt"].ToString().Trim();
            txtTotalDebit.Text = dt.Rows[0]["TotalDebit"].ToString().Trim();
            txtRBIApproval.Text = dt.Rows[0]["RBIApprovalRefNo"].ToString().Trim();
            txtRBIApprovalDate.Text = dt.Rows[0]["RBIApprovalDate"].ToString().Trim();
        }
        else
        {
            clearAll();
            hdnMode.Value = "add";
            //txtUtilisationDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //txtUtilisedAmount.Focus();
        }
        
    }

    protected void fillDetails(string _DocNo, string _UtilNo)
    {
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = _DocNo;
        SqlParameter p2 = new SqlParameter("@ExtensionNo", SqlDbType.VarChar);
        p2.Value = "";
        string _query = "TF_EXP_XOS_GetExtensionEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtBillDate.Text = dt.Rows[0]["BillDate"].ToString().Trim();
            txtCustomerAcNo.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            lblCustomerDescription.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();

            txtOverseasParty.Text = dt.Rows[0]["Overseas_Party_Code"].ToString().Trim();
            lblOverseasPartyDesc.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            if (lblOverseasPartyDesc.Text.Length > 15)
            {
                lblOverseasPartyDesc.ToolTip = lblOverseasPartyDesc.Text;
                lblOverseasPartyDesc.Text = lblOverseasPartyDesc.Text.Substring(0, 15) + "...";

            }

            txtBillCurrency.Text = dt.Rows[0]["Currency"].ToString().Trim();
            lblCurrDesc.Text = dt.Rows[0]["C_DESCRIPTION"].ToString().Trim();
            if (lblCurrDesc.Text.Length > 15)
            {
                lblCurrDesc.ToolTip = lblCurrDesc.Text;
                lblCurrDesc.Text = lblCurrDesc.Text.Substring(0, 15) + "...";

            }
            txtBillAmount.Text = dt.Rows[0]["ActBillAmt"].ToString().Trim();
            txtBillDueDate.Text = dt.Rows[0]["Due_Date"].ToString().Trim();

            txtAmountOutstanding.Text = dt.Rows[0]["BalanceAmount"].ToString().Trim();
            txtExtensionNo.Text = dt.Rows[0]["LastExtensionNo"].ToString().Trim();
            hdnPrevExtensionNo.Value = txtExtensionNo.Text;
            txtAWBDate.Text = dt.Rows[0]["AWB_Date"].ToString();
        }
    }

    private void clearAll()
    {
        txtExtensionDate.Text = "";
        txtGrantedUpto.Text = "";
        txtReasonForExtension.Text = "";
        txtAllowedUnderCriteria.Text = "";
        txtRemarks.Text = "";
        chkAnnex4recvd.Checked = false;
        txtExtensionCharges.Text = "";
        txtSTaxAmount.Text = "";
        txtTotalDebit.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _script = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        TF_DATA objCheck = new TF_DATA();

        SqlParameter bCode = new SqlParameter("@bCode", SqlDbType.VarChar);
        bCode.Value = Request.QueryString["BranchCode"].Trim();

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = hdnMode.Value;

        SqlParameter pBillNo = new SqlParameter("@BillNo", SqlDbType.VarChar);
        pBillNo.Value = txtBillDocumentNo.Text.Trim();

        SqlParameter pExtensionDocNo = new SqlParameter("@ExtensionDocNo", SqlDbType.VarChar);
        pExtensionDocNo.Value = txtExtensionDocumentNo.Text.Trim();

        SqlParameter pExtensionNo = new SqlParameter("@ExtensionNo", SqlDbType.VarChar);
        pExtensionNo.Value = txtExtensionNo.Text.Trim();

        SqlParameter pExtensionDate = new SqlParameter("@ExtensionDate", SqlDbType.VarChar);
        pExtensionDate.Value = txtExtensionDate.Text.Trim();

        SqlParameter pGrantedUpto = new SqlParameter("@GrantedUpto", SqlDbType.VarChar);
        pGrantedUpto.Value = txtGrantedUpto.Text.Trim();

        SqlParameter pGrantingAuthority = new SqlParameter("@GrantingAuthority", SqlDbType.VarChar);
        pGrantingAuthority.Value = ddlGrantingAuthority.SelectedValue.ToString();

        SqlParameter pReasonForExtension = new SqlParameter("@ReasonForExtension", SqlDbType.VarChar);
        pReasonForExtension.Value = txtReasonForExtension.Text.Trim();

        SqlParameter pAllowedUnderCriteria = new SqlParameter("@AllowedUnderCriteria", SqlDbType.VarChar);
        pAllowedUnderCriteria.Value = txtAllowedUnderCriteria.Text.Trim();

        SqlParameter pRemarks = new SqlParameter("@Remarks", SqlDbType.VarChar);
        pRemarks.Value = txtRemarks.Text.Trim();

        SqlParameter pAnnex4recvd = new SqlParameter("@Annex4recvd", SqlDbType.VarChar);
        if (chkAnnex4recvd.Checked == true)
            pAnnex4recvd.Value = "1";
        else
            pAnnex4recvd.Value = "0";

        SqlParameter pRBIAprroval = new SqlParameter("@rbiapproval", SqlDbType.VarChar);
        pRBIAprroval.Value = txtRBIApproval.Text;

        SqlParameter pRBIApprovalDate = new SqlParameter("@rbiapprovaldate", SqlDbType.VarChar);
        pRBIApprovalDate.Value = txtRBIApprovalDate.Text;

        SqlParameter pExtensionCharges = new SqlParameter("@ExtensionCharges", SqlDbType.VarChar);
        pExtensionCharges.Value = txtExtensionCharges.Text.Trim();

        SqlParameter pStax = new SqlParameter("@Stax", SqlDbType.VarChar);
        pStax.Value = ddlServiceTax.Text.Trim();

        SqlParameter pStaxAmount = new SqlParameter("@StaxAmount", SqlDbType.VarChar);
        pStaxAmount.Value = txtSTaxAmount.Text.Trim();

        SqlParameter pTotalDebit = new SqlParameter("@TotalDebit", SqlDbType.VarChar);
        pTotalDebit.Value = txtTotalDebit.Text.Trim();

        SqlParameter pBalance = new SqlParameter("@balance", SqlDbType.VarChar);
        pBalance.Value = txtAmountOutstanding.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        string _query = "TF_EXP_XOS_UpdateExtensionEntryDetails";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query,
        bCode, pMode,pBillNo,pExtensionDocNo,pExtensionNo,pExtensionDate,pGrantedUpto,pGrantingAuthority,pReasonForExtension,pAllowedUnderCriteria,
        pRemarks,pAnnex4recvd,pRBIAprroval,pRBIApprovalDate,pExtensionCharges,pStax,pStaxAmount,pTotalDebit,pBalance,pUser, pUploadDate
        );

        _script = "";
        if (_result.Substring(0, 5) == "added")
        {
            _script = "window.location='XOS_ViewExtensionData.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='XOS_ViewExtensionData.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("XOS_ViewExtensionData.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("XOS_ViewExtensionData.aspx", true);
    }
}