using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EXP_EXP_AddEditLiquidation : System.Web.UI.Page
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
                Clear();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EXP_ViewLiquidation.aspx", true);
                }
                else
                {

                    //    fillCurrency();
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtDocNo.Text = Request.QueryString["docNo"].Trim();
                        txtBranch.Text = Request.QueryString["Branch"].Trim();


                        fillDetails(Request.QueryString["docNo"].Trim(), Request.QueryString["srNo"].Trim());

                        hdnMode.Value = Request.QueryString["mode"].Trim();
                    }
                    else
                    {

                        //  txtDocNo.Enabled = true;

                        fillEntryNo();
                        txtDocNo.Text = Request.QueryString["docNo"].Trim();
                        fillExportDetails();
                        txtBranch.Text = Request.QueryString["Branch"].Trim();
                        txtLiquidatedDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        hdnMode.Value = Request.QueryString["mode"].Trim();
                    }
                }

                btnSave.Attributes.Add("onclick", "return validateSave();");

                //btnCustAcList.Attributes.Add("onclick", "return OpenCustomerCodeList();");
                //   btncurrList.Attributes.Add("onclick", "return OpenCurrencyList();");

                btnPcACNO.Attributes.Add("onclick", "return OpenSubACList();");

                txtExchRt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtLiquidatedAmt.Attributes.Add("onkeydown", "return validate_Number(event);");

                rbtnIR_INR.Attributes.Add("onclick", "return checkExchRateforINR();");
                rbtnIR_FC.Attributes.Add("onclick", "return checkExchRateforINR();");

                txtPCFC_AcNo.Attributes.Add("onpaste", "return false;");
                txtSubAcNo.Attributes.Add("onpaste", "return false;");

                txtLiquidatedAmt.Attributes.Add("onblur", "return CheckBalance();");

                txtExchRt.Attributes.Add("onblur", "return CalculateINRamt();");
                txtLiquidatedDate.Focus();

                txtBranch.Attributes.Add("onkeydown", "return false;");
                txtDocNo.Attributes.Add("onkeydown", "return false;");
                txtDocType.Attributes.Add("onkeydown", "return false;");
                txtCustAcNo.Attributes.Add("onkeydown", "return false;");
                txtPCFC_AcNo.Attributes.Add("onkeydown", "return false;");
                txtSubAcNo.Attributes.Add("onkeydown", "return false;");

                txtBranch.Attributes.Add("oncut", "return false;");
                txtDocNo.Attributes.Add("oncut", "return false;");
                txtDocType.Attributes.Add("oncut", "return false;");
                txtCustAcNo.Attributes.Add("oncut", "return false;");
                txtPCFC_AcNo.Attributes.Add("oncut", "return false;");
                txtSubAcNo.Attributes.Add("oncut", "return false;");

                txtBranch.Attributes.Add("onpaste", "return false;");
                txtDocNo.Attributes.Add("onpaste", "return false;");
                txtDocType.Attributes.Add("onpaste", "return false;");
                txtCustAcNo.Attributes.Add("onpaste", "return false;");
                txtPCFC_AcNo.Attributes.Add("onpaste", "return false;");
                txtSubAcNo.Attributes.Add("onpaste", "return false;");
            }
        }
    }

    private void fillExportDetails()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_EXP_GetExportBillEntryDetails";

        SqlParameter pDocNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pDocNo.Value = txtDocNo.Text.Trim();

        DataTable dt = objData.getData(_query, pDocNo);

        if (dt.Rows.Count > 0)
        {
          //  txtDocType.Text = dt.Rows[0]["Document_Type"].ToString().Trim();
            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            fillCustomerCodeDescription();
        }

    }

    private void fillDetails(string _docNo, string _srNo)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetExportLiquidatedDetails";

        SqlParameter pDocNo = new SqlParameter("@docNo", SqlDbType.VarChar);
        pDocNo.Value = _docNo;

        SqlParameter pSrNo = new SqlParameter("@srNo", SqlDbType.VarChar);
        pSrNo.Value = _srNo;

        SqlParameter pBranch = new SqlParameter("@branchName", SqlDbType.VarChar);
        pBranch.Value = txtBranch.Text.Trim();


        DataTable dt = objData.getData(_query, pDocNo, pSrNo, pBranch);

        if (dt.Rows.Count > 0)
        {
            txtLiquidatedDate.Text = dt.Rows[0]["Doc_Date"].ToString().Trim();
            txtCustAcNo.Text = dt.Rows[0]["CUST_AcNo"].ToString().Trim();

            //if (dt.Rows[0]["PC_Currency"].ToString().Trim() != "")
            //{
            //    dropDownListCurrency.SelectedValue = dt.Rows[0]["PC_Currency"].ToString().Trim();
            //    SqlParameter p3 = new SqlParameter("@currencyid", SqlDbType.VarChar);
            //    p3.Value = dropDownListCurrency.SelectedValue;
            //    _query = "";
            //    _query = "TF_GetCurrencyMasterDetails";
            //    DataTable dtp = objData.getData(_query, p3);
            //    if (dtp.Rows.Count > 0)
            //    {
            //        txtCurrencyDescription.Text = dtp.Rows[0]["C_Description"].ToString().Trim();
            //    }
            //}
            //else
            //    dropDownListCurrency.SelectedIndex = -1;

            txtCurrencyID.Text = dt.Rows[0]["PC_Currency"].ToString().Trim();
            fillCurrencyDescription();

            txtPCFC_AcNo.Text = dt.Rows[0]["PC_AcNo"].ToString().Trim();
            txtSubAcNo.Text = dt.Rows[0]["SubAcNo"].ToString().Trim();
            txtEntryNo.Text = dt.Rows[0]["SrNo"].ToString().Trim();
            txtDocNo.Text = dt.Rows[0]["Document_No"].ToString().Trim();
            txtLiquidatedAmt.Text = dt.Rows[0]["PC_LiquiAmt"].ToString().Trim();
            txtDocType.Text = dt.Rows[0]["Document_Type"].ToString().Trim();

            txtExchRt.Text = dt.Rows[0]["PC_ExcRate"].ToString().Trim();

            hdnLiquiAmt.Value = txtLiquidatedAmt.Text;
            hdnBalanceAmt.Value = dt.Rows[0]["Balance"].ToString().Trim();
            txtChequeNo.Text = dt.Rows[0]["ChequeNo"].ToString().Trim();
            if (dt.Rows[0]["proofOfExports"].ToString() == "True")
                chkProofofExports.Checked = true;
            else
                chkProofofExports.Checked = false;
            txtContractNo.Text = dt.Rows[0]["ContractNo"].ToString().Trim();


            if (dt.Rows[0]["debitType"].ToString() == "D1")
                rbtnNOSTRO.Checked = true;

            if (dt.Rows[0]["debitType"].ToString() == "D2")
                rbtnEEFC.Checked = true;

            if (dt.Rows[0]["debitType"].ToString() == "D3")
                rbtnINR.Checked = true;

            if (dt.Rows[0]["debitType"].ToString() == "D4")
                rbtnFContract.Checked = true;

            if (dt.Rows[0]["intRec"].ToString() == "IN")
                rbtnIR_INR.Checked = true;

            if (dt.Rows[0]["intRec"].ToString() == "FC")
                rbtnIR_FC.Checked = true;

            if (rbtnIR_INR.Checked == true)
            {
                txtExchRateforIntRec.Enabled = true;
                txtExchRateforIntRec.Text = dt.Rows[0]["ExchRateforIntRec"].ToString();
            }

            if (dt.Rows[0]["StFXDLSamt"].ToString().Trim() != "")
            {
                txtSTFXamt.Text = dt.Rows[0]["StFXDLSamt"].ToString();
                //chkFXDLS.Checked = true;
            }
        }
        else
        {
            txtLiquidatedDate.Focus();

        }
    }

    private void Clear()
    {
        txtLiquidatedDate.Text = "";
        txtDocNo.Text = "";
        txtCustAcNo.Text = "";
        txtCurrencyID.Text = "";
        lblCustDescription.Text = "";
        txtLiquidatedDate.Text = "";
        txtExchRt.Text = "";

        txtPCFC_AcNo.Text = "";
        txtSubAcNo.Text = "";

        hdnBalanceAmt.Value = "";
        hdnCurId.Value = "";
        hdnCustomerCode.Value = "";

    }

    protected void btnCustomerCode_Click(object sender, EventArgs e)
    {
        if (hdnCustomerCode.Value != "")
        {
            txtCustAcNo.Text = hdnCustomerCode.Value;
            fillCustomerCodeDescription();
            txtCustAcNo.Focus();
        }
    }
    protected void btnCurr_Click(object sender, EventArgs e)
    {
        if (hdnCurId.Value != "")
        {
            txtCurrencyID.Text = hdnCurId.Value;
            fillCurrencyDescription();
        }
        if (Request.QueryString["mode"].Trim() == "add")
        {
            fillEntryNo();
        }
        txtLiquidatedAmt.Focus();
    }

    private void fillEntryNo()
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bName", SqlDbType.VarChar);
        p1.Value = txtBranch.Text;

        SqlParameter p2 = new SqlParameter("@pcAcNo", SqlDbType.VarChar);
        p2.Value = txtPCFC_AcNo.Text;

        SqlParameter p3 = new SqlParameter("@subAcNo", SqlDbType.VarChar);
        p3.Value = txtSubAcNo.Text;

        string _query = "TF_INW_GetLastDocNo_Liquidation"; //--------- kept same as imward liquidation
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {

            txtEntryNo.Text = dt.Rows[0]["LastDocNo"].ToString();
        }

    }

    public void fillCurrencyDescription()
    {
        txtCurrencyDescription.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p1.Value = txtCurrencyID.Text;
        string _query = "TF_GetCurrencyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCurrencyDescription.Text = dt.Rows[0]["C_Description"].ToString().Trim();
        }
        //if (txtCustAcNo.Text != "")
        //    fillPCFC_AcNo();
    }

    public void fillCustomerCodeDescription()
    {
        lblCustDescription.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtCustAcNo.Text;
        string _query = "TF_GetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustDescription.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            lblCustDescription.Text = "INVALID CUSTOMER";
            txtCustAcNo.Text = "";

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateLiquidationDetails_EXP";

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = _mode;

        SqlParameter pBranchName = new SqlParameter("@bName", SqlDbType.VarChar);
        pBranchName.Value = txtBranch.Text.Trim();

        SqlParameter pLiquidationDate = new SqlParameter("@LiquiDate", SqlDbType.VarChar);
        pLiquidationDate.Value = txtLiquidatedDate.Text.Trim();

        SqlParameter pCustAcNo = new SqlParameter("@custAc", SqlDbType.VarChar);
        pCustAcNo.Value = txtCustAcNo.Text.Trim();

        SqlParameter pCurr = new SqlParameter("@curr", SqlDbType.VarChar);
        //if (dropDownListCurrency.SelectedIndex > 0)
        //    pCurr.Value = dropDownListCurrency.Text.Trim();
        //else
        pCurr.Value = txtCurrencyID.Text.Trim();

        SqlParameter pPcAcNo = new SqlParameter("@acNo", SqlDbType.VarChar);
        pPcAcNo.Value = txtPCFC_AcNo.Text.Trim();

        SqlParameter pSubAcNo = new SqlParameter("@subAcNo", SqlDbType.VarChar);
        pSubAcNo.Value = txtSubAcNo.Text.Trim();


        SqlParameter pDocNo = new SqlParameter("@docNo", SqlDbType.VarChar);
        pDocNo.Value = txtDocNo.Text.Trim();

        SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
        pDocType.Value = txtDocType.Text.Trim();


        SqlParameter pLiquiAmt = new SqlParameter("@LiquiAmt", SqlDbType.VarChar);
        pLiquiAmt.Value = txtLiquidatedAmt.Text.Trim();

        SqlParameter pExchRate = new SqlParameter("@exchRt", SqlDbType.VarChar);
        pExchRate.Value = txtExchRt.Text.Trim();

        SqlParameter pChequeNo = new SqlParameter("@chequeNo", SqlDbType.VarChar);
        pChequeNo.Value = txtChequeNo.Text.Trim();

        SqlParameter pProofOfExports = new SqlParameter("@proofOfExports", SqlDbType.VarChar);

        if (chkProofofExports.Checked)
            pProofOfExports.Value = true;
        else
            pProofOfExports.Value = false;

        SqlParameter pContractNo = new SqlParameter("@contractNo", SqlDbType.VarChar);
        pContractNo.Value = txtContractNo.Text.Trim();



        SqlParameter pdebitType = new SqlParameter("@debitType", SqlDbType.VarChar);
        if (rbtnNOSTRO.Checked)
            pdebitType.Value = "D1";
        if (rbtnEEFC.Checked)
            pdebitType.Value = "D2";
        if (rbtnINR.Checked)
            pdebitType.Value = "D3";
        if (rbtnFContract.Checked)
            pdebitType.Value = "D4";

        SqlParameter pIntRec = new SqlParameter("@intRec", SqlDbType.VarChar);
        if (rbtnIR_INR.Checked)
            pIntRec.Value = "IN";
        if (rbtnIR_FC.Checked)
            pIntRec.Value = "FC";

        SqlParameter pstFXDLsAmt = new SqlParameter("@stFXDLsAmt", SqlDbType.VarChar);
        pstFXDLsAmt.Value = txtSTFXamt.Text.Trim();


        SqlParameter pExchRateforIntRec = new SqlParameter("@ExchRateforIntRec", SqlDbType.VarChar);
        pExchRateforIntRec.Value = txtExchRateforIntRec.Text.Trim();

        SqlParameter pSrNo = new SqlParameter("@srNo", SqlDbType.VarChar);
        pSrNo.Value = txtEntryNo.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadDate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        string _result = objSave.SaveDeleteData(_query, pMode, pBranchName, pLiquidationDate, pCustAcNo
                        , pCurr, pPcAcNo, pSubAcNo, pDocNo, pDocType, pLiquiAmt, pExchRate
                        , pChequeNo, pProofOfExports, pContractNo, pdebitType, pIntRec, pExchRateforIntRec, pstFXDLsAmt
                        , pSrNo, pUser, pUploadDate);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='EXP_ViewLiquidation.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='EXP_ViewLiquidation.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewLiquidation.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewLiquidation.aspx", true);
    }

    private void fillPCFC_AcNo()
    {
        string _custAc = txtCustAcNo.Text.Trim();
        if (txtCustAcNo.Text.Length > 6)
            _custAc = txtCustAcNo.Text.Substring(txtCustAcNo.Text.Length - 6, 6);
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p1.Value = txtCurrencyID.Text.Trim();
        string _query = "TF_GetCurrencyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtPCFC_AcNo.Text = dt.Rows[0]["C_TEXT1"].ToString() + "1E4" + _custAc;
        }
    }


    protected void txtCustAcNo_TextChanged(object sender, EventArgs e)
    {
        fillCustomerCodeDescription();
        txtPCFC_AcNo.Focus();
    }
    protected void txtCurrencyID_TextChanged(object sender, EventArgs e)
    {
        fillCurrencyDescription();
    }


    protected void txtExchRt_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter pdocDate = new SqlParameter("@DocDt", SqlDbType.VarChar);
        pdocDate.Value = txtLiquidatedDate.Text.Trim();

        SqlParameter pINRamt = new SqlParameter("@InrAmt", SqlDbType.VarChar);
        pINRamt.Value = Convert.ToString(Convert.ToDouble(txtExchRt.Text.Trim()) * Convert.ToDouble(txtLiquidatedAmt.Text.Trim()));

        string _query = "TF_FXSTAX";
        DataTable dt = objData.getData(_query, pINRamt, pdocDate);
        if (dt.Rows.Count > 0)
        {
            txtSTFXamt.Text = dt.Rows[0]["CalFX"].ToString().Trim();
        }
        txtChequeNo.Focus();
    }
}