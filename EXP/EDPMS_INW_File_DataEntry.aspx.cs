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
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.IO;


public partial class EDPMS_EDPMS_INW_File_DataEntry : System.Web.UI.Page
{
    int Leicount = 5;
    TF_DATA objData = new TF_DATA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"].ToString() == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                txtBranchCode.Text = Request.QueryString["branchcode"].ToString();
                hdnBranchCode.Value = Request.QueryString["branchcode"].Trim();
                FillIFSCTemittanceAdCode();
                fillCurrency();

                //getSrialNo();// added by Anand 29-07-2023

                lblBranchName.Text = Request.QueryString["branchName"].ToString();
                if (Request.QueryString["mode"].ToString() == "add")
                {
                    txtDocPref.Text = "ITT";
                    txtDocBRCode.Text = txtBranchCode.Text;
                    txtDocNo.Enabled = true;
                    txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));
                }
                else
                {
                    txtDocPref.Text = Request.QueryString["DocNo"].ToString().Substring(0, 3).ToUpper();
                    txtDocBRCode.Text = Request.QueryString["DocNo"].ToString().Substring(4, 3);
                    txtDocNo.Text = Request.QueryString["DocNo"].ToString().Substring(8, 6);
                    if ((Request.QueryString["DocNo"].ToString().Length) > 14)
                    {
                        txtDocSrNo.Text = Request.QueryString["DocNo"].ToString().Substring(14, 2);
                    }
                    else
                    {
                        txtDocSrNo.Text = "";
                    }

                    txtYear.Text = Request.QueryString["DocNo"].ToString().Substring(8, 2);
                    txtDocBRCode_TextChanged(null, null);
                    Leicount = 0;
                    Check_LEI_ThresholdLimit();

                    txtDocPref.Enabled = false;
                    txtDocBRCode.Enabled = false;
                    txtDocNo.Enabled = false;
                    txtDocSrNo.Enabled = false;
                    btnDocNoList.Enabled = false;
                    txtYear.Enabled = false;

                }
                txtDocDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtAmount.Attributes.Add("onblur", "return ComputeAmtInINR();");
                txtExchangeRate.Attributes.Add("onblur", "return ComputeAmtInINR();");
                btnOverseasPartyList.Attributes.Add("onclick", "return OpenOverseasPartyList('mouseClick');");
                txtADCode.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtExchangeRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtInINR.Attributes.Add("onkeydown", "return validate_Number(event);");
                btnAdd.Attributes.Add("onClick", "return validateSave();");
                //btnSaveDraft.Attributes.Add("onClick", "return validateSaveDraft();");
                btnPrint.Attributes.Add("onClick", "return validateSave();");
                txtADCode.Attributes.Add("onblur", "return chkFIRCADCode();");
                // txtDocDate.Attributes.Add("onblur", "isValidDate(" + txtDocDate.ClientID + "," + "' Document Date.'" + ");");
                txtFIRCDate.Attributes.Add("onblur", "isValidDate(" + txtFIRCDate.ClientID + "," + "' FIRC Date.'" + ");");
                txtPanNumber.Attributes.Add("onkeydown", "return PanCardNo(event);");
            }
        }
    }
    protected void fillCurrency()
    {
        SqlParameter p1 = new SqlParameter("@search", "");
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlCurrency.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddlCurrency.DataSource = dt.DefaultView;
            ddlCurrency.DataTextField = "C_Code";
            ddlCurrency.DataValueField = "C_Code";
            ddlCurrency.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlCurrency.Items.Insert(0, li);
    }
    protected void fillDetails()
    {
        string docno = Request.QueryString["DocNo"].ToString();
        string irmstatus = Request.QueryString["IRMStatus"].ToString();
        SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
        SqlParameter p2 = new SqlParameter("@docNo", docno);
        SqlParameter p3 = new SqlParameter("@IRMStatus", irmstatus);
        //SqlParameter p3 = new SqlParameter("@mode", "edit");
        //SqlParameter p4 = new SqlParameter("@search", "");
        DataTable dt = objData.getData("TF_EDPMS_INW_File_GetDetails", p1, p2,p3);
        if (dt.Rows.Count > 0)
        {

            txtDocDate.Text = dt.Rows[0]["Document_Date"].ToString();
            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            txtPurposeCode.Text = dt.Rows[0]["Purpose_Code"].ToString().Trim();
            ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString().Trim();
            txtExchangeRate.Text = dt.Rows[0]["Exchange_Rate"].ToString().Trim();
            txtAmtInINR.Text = dt.Rows[0]["Amount_In_INR"].ToString().Trim();
            txtFIRCNo.Text = dt.Rows[0]["FIRC_No"].ToString().Trim();
            txtFIRCDate.Text = dt.Rows[0]["FIRC_Date"].ToString().Trim();
            txtADCode.Text = dt.Rows[0]["FIRC_AD_Code"].ToString().Trim();
            ////////added by shailesh ///////////////////////////////////////
            txtOverseasPartyID.Text = dt.Rows[0]["RemitterID"].ToString().Trim();
            txtRemitterName.Text = dt.Rows[0]["RemitterName"].ToString().Trim();
            txtRemitterCountry.Text = dt.Rows[0]["RemitterCountry"].ToString().Trim();
            txtvalueDate.Text = dt.Rows[0]["Value_Date"].ToString().Trim();
            txtRemitterAddress.Text = dt.Rows[0]["Remitter_Address"].ToString().Trim();
            txtRemitterBank.Text = dt.Rows[0]["Remitter_Bank"].ToString().Trim();
            txtRemitterBankAddress.Text = dt.Rows[0]["Remitter_Bank_Address"].ToString().Trim();
            txtpurposeofRemittance.Text = dt.Rows[0]["Purpose_Of_Remittance"].ToString().Trim();
            txtRemBankCountry.Text = dt.Rows[0]["Remitter_Bank_Country"].ToString().Trim();
            txt_swiftcode.Text = dt.Rows[0]["SwiftCode"].ToString().Trim();
            //if (dt.Rows[0]["Checker_Remark"].ToString() != "" && dt.Rows[0]["Status"].ToString() == "Reject By Checker")
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }

            //-----------------------Anand 15-06-2023------------------
            txtBankUniqueTransactionID.Text = dt.Rows[0]["BankUniqueTransactionID"].ToString().Trim();
            txtIFSCCode.Text = dt.Rows[0]["IFSCCode"].ToString().Trim();
            txtRemittanceADCode.Text = dt.Rows[0]["RemittanceADCode"].ToString().Trim();
            txtIECCode.Text = dt.Rows[0]["IECCode"].ToString().Trim();
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString().Trim();
            ddlModeOfPayment.SelectedValue = dt.Rows[0]["ModeofPayment"].ToString().Trim();// Modified by Anand 11-08-2023
            //  ddlFactoringflag.SelectedValue = dt.Rows[0]["Factoringflag"].ToString().Trim(); // comment by Anand21-07-2023
            //ddlForfeitingflag.SelectedValue = dt.Rows[0]["Forfeitingflag"].ToString().Trim();// comment by Anand21-07-2023
            txtBankReferencenumber.Text = dt.Rows[0]["BankReferencenumber"].ToString().Trim();
            txtBankAccountNumber.Text = dt.Rows[0]["BankAccountNumber"].ToString().Trim();
            if (dt.Rows[0]["IRMStatus"].ToString() == "F")
            {
                ddlIRMStatus.SelectedValue = "Fresh";
            }
            else if (dt.Rows[0]["IRMStatus"].ToString() == "A")
            {
                ddlIRMStatus.SelectedValue = "Amended";
            }
            else if (dt.Rows[0]["IRMStatus"].ToString() == "C")
            {
                ddlIRMStatus.SelectedValue = "Cancelled";
            }

            ddlCurrency_SelectedIndexChanged(null, null);
            txtCustAcNo_TextChanged(null, null);
            txtPurposeCode_TextChanged(null, null);
            txtRemitterCountry_TextChanged(null, null);
            txtRemBankCountry_TextChanged(null, null);
            txtOverseasPartyID_TextChanged(null, null);

            string STATUSI = dt.Rows[0]["Checker_Status"].ToString().Trim();
            if (STATUSI == "A" || STATUSI == "C" || STATUSI == "I")
            {
                MakerVisible.Visible = false;
                CheckerViewOnly();
            }
            else
            {
                MakerVisible.Visible = true;
            }
            //------------------------Audit Trail Anand 09-01-2024-----------
            hdnDocumentDate.Value = dt.Rows[0]["Document_Date"].ToString().Trim();
            hdnValueDate.Value = dt.Rows[0]["Value_Date"].ToString().Trim();
            hdnSwiftCode.Value = dt.Rows[0]["SwiftCode"].ToString().Trim();
            hdnCustomerACNo.Value = dt.Rows[0]["CustAcNo"].ToString().Trim();
            hdnPurposeCode.Value = dt.Rows[0]["Purpose_Code"].ToString().Trim();
            hdnRemitterID.Value = dt.Rows[0]["RemitterID"].ToString().Trim();
            hdnRemitterName.Value = dt.Rows[0]["RemitterName"].ToString().Trim();
            hdnRemitterCountry.Value = dt.Rows[0]["RemitterCountry"].ToString().Trim();
            hdnRemitterAddress.Value = dt.Rows[0]["Remitter_Address"].ToString().Trim();
            hdnRemittingBank.Value = dt.Rows[0]["Remitter_Bank"].ToString().Trim();
            hdnBankCountry.Value = dt.Rows[0]["Remitter_Bank_Country"].ToString().Trim();
            hdnRemittingBankAddress.Value = dt.Rows[0]["Remitter_Bank_Address"].ToString().Trim();
            hdnPurposeOfRemittance.Value = dt.Rows[0]["Purpose_Of_Remittance"].ToString().Trim();
            hdnCurrency.Value = dt.Rows[0]["Currency"].ToString().Trim();
            hdnAmountInFC.Value = dt.Rows[0]["Amount"].ToString().Trim();
            hdnExchangeRate.Value = dt.Rows[0]["Exchange_Rate"].ToString().Trim();
            hdnAmountInINR.Value = dt.Rows[0]["Amount_In_INR"].ToString().Trim();
            hdnFIRCNO.Value = dt.Rows[0]["FIRC_No"].ToString().Trim();
            hdnFIRCDate.Value = dt.Rows[0]["FIRC_Date"].ToString().Trim();
            hdnFIRCADCode.Value = dt.Rows[0]["FIRC_AD_Code"].ToString().Trim();
            hdnBankUniqueTransactionID.Value = dt.Rows[0]["BankUniqueTransactionID"].ToString().Trim();
            hdnIFSCCode.Value = dt.Rows[0]["IFSCCode"].ToString().Trim();
            hdnRemittanceADCode.Value = dt.Rows[0]["RemittanceADCode"].ToString().Trim();
            hdnIECCode.Value = dt.Rows[0]["IECCode"].ToString().Trim();
            hdnPanNumber.Value = dt.Rows[0]["PanNumber"].ToString().Trim();
            hdnModeofPayment.Value = dt.Rows[0]["ModeofPayment"].ToString().Trim();
            hdnBankReferenceNumbe.Value = dt.Rows[0]["BankReferencenumber"].ToString().Trim();
            hdnBankAccountNumber.Value = dt.Rows[0]["BankAccountNumber"].ToString().Trim();
            hdnIRMStatus.Value = dt.Rows[0]["IRMStatus"].ToString();
            hdnCheckarStatus.Value = dt.Rows[0]["Checker_Status"].ToString().Trim();
            //-------------------------------End-------------------------
            }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_INW_File_DataEntryView.aspx");
    }
    protected void txtDocBRCode_TextChanged(object sender, EventArgs e)
    {
        fillDetails();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string PanNumber = txtPanNumber.Text;
            DateTime dt = DateTime.ParseExact(txtDocDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.ParseExact(txtvalueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (txtDocDate.Text != "" && dt > DateTime.Today)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Document Date does not Greater than Today.');", true);
                txtDocDate.Focus();
            }
            else if (txtvalueDate.Text != "" && dt1 > DateTime.Today)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Value Date does not Greater than Today.');", true);
                txtvalueDate.Focus();

            }
            else if (hdnLeiFlag.Value == "Y" && lblLEI_CUST_Remark.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify LEI details.');", true);
            }
            else if (hdnLeiSpecialFlag.Value == "R" && lblLEI_CUST_Remark.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify Recurring LEI Customer.');", true);
            }
            else if (hdnValidateLei.Value == "LeiFalse")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('This transaction cannot proceed because the LEI Details has not been verified.');", true);
            }
            else if (txtOverseasPartyID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Remitter ID.');", true);
                txtOverseasPartyID.Focus();
            }
            //------------------------Anand17-06-2023-------------------------------------------
            //else if (txtBankUniqueTransactionID.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Bank Unique Transaction ID.');", true);
            //    txtBankUniqueTransactionID.Focus();
            //}
            else if (txtIFSCCode.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter IFSC Code.');", true);
                txtIFSCCode.Focus();
            }
            else if (txtRemittanceADCode.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Remittance AD Code');", true);
                txtRemittanceADCode.Focus();
            }
            else if (txtIECCode.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter IEC Code.');", true);
                txtIECCode.Focus();
            }
            //--------------------------------------------End-----------------------------------------------
            else
            {
                string mode = Request.QueryString["mode"].ToString();
                string _result = "", _script = "";
                //string _docno = txtDocBRCode.Text + txtDocNo.Text;
                string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
                SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
                SqlParameter p2 = new SqlParameter("@docNo", _docno);
                SqlParameter p3 = new SqlParameter("@docDate", txtDocDate.Text.Trim());
                SqlParameter p4 = new SqlParameter("@custAcNo", txtCustAcNo.Text.Trim());
                SqlParameter p5 = new SqlParameter("@purposeCode", txtPurposeCode.Text.Trim());
                SqlParameter p6 = new SqlParameter("@currency", ddlCurrency.SelectedValue);
                SqlParameter p7 = new SqlParameter("@amount", txtAmount.Text.Trim());
                SqlParameter p8 = new SqlParameter("@mode", mode);
                SqlParameter p9 = new SqlParameter("@exchangeRate", txtExchangeRate.Text.Trim());
                SqlParameter p10 = new SqlParameter("@amountInINR", txtAmtInINR.Text.Trim());
                SqlParameter p11 = new SqlParameter("@fircNo", txtFIRCNo.Text.Trim());
                SqlParameter p12 = new SqlParameter("@fircDate", txtFIRCDate.Text.Trim());
                SqlParameter p13 = new SqlParameter("@fircAdCode", txtADCode.Text.Trim());
                /////////////////////added by Shailesh help form  remitter ID//////////////////////
                SqlParameter pRemitterID = new SqlParameter("@remitterID", txtOverseasPartyID.Text.Trim());
                SqlParameter p14 = new SqlParameter("@remitterName", txtRemitterName.Text.Trim());
                SqlParameter p15 = new SqlParameter("@remitterCountry", txtRemitterCountry.Text.Trim());
                SqlParameter p16 = new SqlParameter("@valueDate", txtvalueDate.Text.Trim());
                SqlParameter p17 = new SqlParameter("@Remitter_Address", txtRemitterAddress.Text.Trim());
                SqlParameter p18 = new SqlParameter("@Remitter_Bank", txtRemitterBank.Text.Trim());
                SqlParameter p19 = new SqlParameter("@Remitter_Bank_Address", txtRemitterBankAddress.Text.Trim());
                SqlParameter p20 = new SqlParameter("@Purpose_Of_Remittance", txtpurposeofRemittance.Text.Trim());
                SqlParameter pRBC = new SqlParameter("@Remitter_Bank_Country", txtRemBankCountry.Text.Trim());
                SqlParameter p21 = new SqlParameter("@addedBy", Session["userName"].ToString());
                SqlParameter p22 = new SqlParameter("@addedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                SqlParameter p23 = new SqlParameter("@swiftcode", txt_swiftcode.Text);

                SqlParameter p24 = new SqlParameter("@Status", "C"); //------added by Nilesh-----23/02/2023
                //-------------------------------Anand15-06-2023-----------------------------
                SqlParameter p25 = new SqlParameter("@BankUniqueTransactionID", txtBankUniqueTransactionID.Text.Trim());
                SqlParameter p26 = new SqlParameter("@IFSCCode", txtIFSCCode.Text.Trim());
                SqlParameter p27 = new SqlParameter("@RemittanceADCode", txtRemittanceADCode.Text.Trim());
                SqlParameter p28 = new SqlParameter("@IECCode", txtIECCode.Text.Trim());
                SqlParameter p29 = new SqlParameter("@PanNumber", txtPanNumber.Text.Trim());
                SqlParameter p30 = new SqlParameter("@ModeofPayment", ddlModeOfPayment.SelectedValue);// Modified By Anand 11-08-2023
                //  SqlParameter p31 = new SqlParameter("@Factoringflag", ddlFactoringflag.SelectedValue); // comment by Anand21-07-2023
                //SqlParameter p32 = new SqlParameter("@Forfeitingflag", ddlForfeitingflag.SelectedValue); // comment by Anand21-07-2023

                SqlParameter p31 = new SqlParameter("@BankReferencenumber", txtBankReferencenumber.Text.Trim());
                SqlParameter p32 = new SqlParameter("@BankAccountNumber", txtBankAccountNumber.Text.Trim());
                SqlParameter p33 = new SqlParameter("@IRMStatus", SqlDbType.VarChar);
                if (ddlIRMStatus.SelectedValue == "Fresh")
                {
                    p33.Value = "F";
                }
                else if (ddlIRMStatus.SelectedValue == "Amended")
                {
                    p33.Value = "A";
                }
                else if (ddlIRMStatus.SelectedValue == "Cancelled")
                {
                    p33.Value = "C";
                }
                //---------------------------------End------------------------------
                //=========================LEI Changes===========================//

                SqlParameter LEIbCode = new SqlParameter("@bCode", txtBranchCode.Text.Trim());
                SqlParameter LEIdocNo = new SqlParameter("@docNo", _docno);
                SqlParameter LEIdocPrfx = new SqlParameter("@docPrfx", txtDocPref.Text.Trim().ToUpper());
                SqlParameter LEIbillType = new SqlParameter("@billType", "");
                SqlParameter LEICustAcNo = new SqlParameter("@custAcNo", txtCustAcNo.Text.Trim());
                SqlParameter LEICust_Name = new SqlParameter("@CustName", hdnCustname.Value);
                SqlParameter LEICust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustabbr.Value);
                SqlParameter LEICust_LEI = new SqlParameter("@Cust_LEI", hdncustlei.Value);
                SqlParameter LEICust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdncustleiexpiry.Value);
                SqlParameter LEIOverseasPartyID = new SqlParameter("@OverseasPartyID", txtOverseasPartyID.Text.Trim());
                SqlParameter LEIOverseas_LEI = new SqlParameter("@Overseas_LEI", hdnoverseaslei.Value);
                SqlParameter LEIOverseas_LEI_Expiry = new SqlParameter("@Overseas_LEI_Expiry", hdnoverseasleiexpiry.Value);
                SqlParameter LEIAmountFC = new SqlParameter("@ActbillAmt", txtAmount.Text.Trim());
                SqlParameter LEIAmtInINR = new SqlParameter("@ActbillAmtinRS", hdnbillamtinr.Value);
                SqlParameter LEICurrency = new SqlParameter("@Curr", ddlCurrency.SelectedValue);
                SqlParameter LEIExchRate = new SqlParameter("@exchRtEBR", hdnleiExchRate.Value);
                SqlParameter LEIdateRcvd = new SqlParameter("@dateRcvd", txtDocDate.Text.Trim());
                SqlParameter LEIDueDate = new SqlParameter("@DueDate", txtvalueDate.Text.Trim());
                SqlParameter LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value);
                SqlParameter LEI_SpecialFlag = new SqlParameter("@LEI_SpecialFlag", hdnLeiSpecialFlag.Value);
                string Trans_Status1 = "A";
                SqlParameter LEITrans_Status = new SqlParameter("@Trans_Status", Trans_Status1);
                SqlParameter LEISrNo = new SqlParameter("@SrNo", "1");
                SqlParameter LEIPayIndicator = new SqlParameter("@Realised_PAY_Indicator", "");
                SqlParameter LEIpurposeCode = new SqlParameter("@purposeCode", txtPurposeCode.Text);
                SqlParameter LEI_Status = new SqlParameter("@Checker_status", "C");
                SqlParameter LEIuser = new SqlParameter("@user", Session["userName"].ToString());
                SqlParameter LEIuploadingdate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                string _resultLEI = "";
                string _queryLEI = "TF_EXP_Update_LEITransactionIRM";
                TF_DATA objSaveLEI = new TF_DATA();

                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                dateInfo.ShortDatePattern = "dd/MM/yyyy";
                DateTime LEIEffectDate = Convert.ToDateTime(lblLEIEffectDate.Text.Trim(), dateInfo);
                DateTime LEIValueDate = Convert.ToDateTime(txtvalueDate.Text.Trim(), dateInfo);

                if (LEIEffectDate <= LEIValueDate)
                {
                    _resultLEI = objSaveLEI.SaveDeleteData(_queryLEI, LEIbCode, LEIdocNo, LEIdocPrfx, LEIbillType, LEICustAcNo, LEICust_Name, LEICust_Abbr, LEICust_LEI, LEICust_LEI_Expiry, LEIOverseasPartyID,
                        LEIOverseas_LEI, LEIOverseas_LEI_Expiry, LEIAmountFC, LEIAmtInINR, LEICurrency, LEIExchRate, LEIdateRcvd, LEIDueDate, LEI_Flag, LEI_SpecialFlag, LEITrans_Status, LEISrNo, LEIPayIndicator, LEIpurposeCode, LEI_Status, LEIuser, LEIuploadingdate);
                }

                //=========================END====================================//

                _result = objData.SaveDeleteData("TF_EDPMS_INW_File_Insert", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, pRemitterID, p14, p15, p16, p17, p18, p19, p20, pRBC, p21, p22, p23, p24,
                    p25, p26, p27, p28, p29, p30, p31, p32, p33);

                
                string _OldValues = "";// Added by Anand 09-01-2024
                string _NewValues = "";// Added by Anand 09-01-2024
                string _Status = "C";//Added by Anand 09-01-2024
                string _query = "";//Added by Anand 09-01-2024
                string _userName = Session["userName"].ToString();//Added by Anand 09-01-2024

                if (_result == "CancelNotexist" || _result == "AmendNotexist")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Create Fresh Transaction.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "CanelDGFTNotProcess")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Create cancel transaction Processed in DGFT.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "FreshTransactionnotprocessDGFT" || _result == "cancelnotcreated" || _result == "Amendnotcreated")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Create fresh transaction Processed in DGFT.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "FreshTranssactionCreated")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Fresh Transaction Already Created.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "Frestnotfound")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Cancel transaction does not Processed in DGFT.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "CancelCreated")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Cancel transaction Already Created.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "added")
                {
                    AuditIRM(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 09-01-2024
                    txtDocNo.Text = ""; txtCustAcNo.Text = "";
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Document No: " + _docno + " Submit to Checker Successfully.");
                    var script = string.Format("alert({0});window.location ='EDPMS_INW_File_DataEntryView.aspx';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                    //---------------------------End--------------------------
                }
                else
                    if (_result == "updated")
                    {
                        AuditIRM(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 09-01-2024
                        txtDocNo.Text = ""; txtCustAcNo.Text = "";
                        var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Document No: " + _docno + " Submit to Checker Successfully.");
                        var script = string.Format("alert({0});window.location ='EDPMS_INW_File_DataEntryView.aspx';", message);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                        //---------------------------End---------------------------
                    }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.ParseExact(txtDocDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.ParseExact(txtvalueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (txtDocDate.Text != "" && dt > DateTime.Today)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Document Date does not Greater than Today.');", true);
                txtDocDate.Focus();
            }
            else if (txtvalueDate.Text != "" && dt1 > DateTime.Today)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Value Date does not Greater than Today.');", true);
                txtvalueDate.Focus();

            }
            else if (txtOverseasPartyID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Remitter ID.');", true);
                txtOverseasPartyID.Focus();
            }
            else if (txtIFSCCode.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter IFSC Code.');", true);
                txtIFSCCode.Focus();
            }
            else if (txtRemittanceADCode.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Remittance AD Code.');", true);
                txtRemittanceADCode.Focus();
            }
            else if (txtPanNumber.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('please Enter Pan Number.');", true);
                txtPanNumber.Focus();
            }

  //---------------------------------------------------END-------------------------------------    
            else
            {
                string mode = Request.QueryString["mode"].ToString();
                string _result = "", script = "";
                //string _docno = txtDocBRCode.Text + txtDocNo.Text;
                string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
                SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
                SqlParameter p2 = new SqlParameter("@docNo", _docno);
                SqlParameter p3 = new SqlParameter("@docDate", txtDocDate.Text.Trim());
                SqlParameter p4 = new SqlParameter("@custAcNo", txtCustAcNo.Text.Trim());
                SqlParameter p5 = new SqlParameter("@purposeCode", txtPurposeCode.Text.Trim());
                SqlParameter p6 = new SqlParameter("@currency", ddlCurrency.SelectedValue);
                SqlParameter p7 = new SqlParameter("@amount", txtAmount.Text.Trim());
                SqlParameter p8 = new SqlParameter("@mode", mode);
                SqlParameter p9 = new SqlParameter("@exchangeRate", txtExchangeRate.Text.Trim());
                SqlParameter p10 = new SqlParameter("@amountInINR", txtAmtInINR.Text.Trim());
                SqlParameter p11 = new SqlParameter("@fircNo", txtFIRCNo.Text.Trim());
                SqlParameter p12 = new SqlParameter("@fircDate", txtFIRCDate.Text.Trim());
                SqlParameter p13 = new SqlParameter("@fircAdCode", txtADCode.Text.Trim());
                /////////////////////added by Shailesh help form  remitter ID//////////////////////
                SqlParameter pRemitterID = new SqlParameter("@remitterID", txtOverseasPartyID.Text.Trim());
                SqlParameter p14 = new SqlParameter("@remitterName", txtRemitterName.Text.Trim());
                SqlParameter p15 = new SqlParameter("@remitterCountry", txtRemitterCountry.Text.Trim());
                SqlParameter p16 = new SqlParameter("@valueDate", txtvalueDate.Text.Trim());
                SqlParameter p17 = new SqlParameter("@Remitter_Address", txtRemitterAddress.Text.Trim());
                SqlParameter p18 = new SqlParameter("@Remitter_Bank", txtRemitterBank.Text.Trim());
                SqlParameter p19 = new SqlParameter("@Remitter_Bank_Address", txtRemitterBankAddress.Text.Trim());
                SqlParameter p20 = new SqlParameter("@Purpose_Of_Remittance", txtpurposeofRemittance.Text.Trim());
                SqlParameter pRBC = new SqlParameter("@Remitter_Bank_Country", txtRemBankCountry.Text.Trim());
                SqlParameter p21 = new SqlParameter("@addedBy", Session["userName"].ToString());
                SqlParameter p22 = new SqlParameter("@addedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                SqlParameter p23 = new SqlParameter("@swiftcode", txt_swiftcode.Text);
                //////////////     added by shailesh save & draft /////////////////////
                SqlParameter p24 = new SqlParameter("@Status", "M");

                //-------------------------------Anand15-06-2023-----------------------------
                SqlParameter p25 = new SqlParameter("@BankUniqueTransactionID", txtBankUniqueTransactionID.Text.Trim());
                SqlParameter p26 = new SqlParameter("@IFSCCode", txtIFSCCode.Text.Trim());
                SqlParameter p27 = new SqlParameter("@RemittanceADCode", txtRemittanceADCode.Text.Trim());
                SqlParameter p28 = new SqlParameter("@IECCode", txtIECCode.Text.Trim());
                SqlParameter p29 = new SqlParameter("@PanNumber", txtPanNumber.Text.Trim());
                SqlParameter p30 = new SqlParameter("@ModeofPayment", ddlModeOfPayment.SelectedValue);// Modified by Anand 11-08-2023
                // SqlParameter p31 = new SqlParameter("@Factoringflag", ddlFactoringflag.SelectedValue);
                //SqlParameter p32 = new SqlParameter("@Forfeitingflag", ddlForfeitingflag.SelectedValue);
                SqlParameter p31 = new SqlParameter("@BankReferencenumber", txtBankReferencenumber.Text.Trim());
                SqlParameter p32 = new SqlParameter("@BankAccountNumber", txtBankAccountNumber.Text.Trim());
                SqlParameter p33 = new SqlParameter("@IRMStatus", SqlDbType.VarChar);
                if (ddlIRMStatus.SelectedValue == "Fresh")
                {
                    p33.Value = "F";
                }
                else if (ddlIRMStatus.SelectedValue == "Amended")
                {
                    p33.Value = "A";
                }
                else if (ddlIRMStatus.SelectedValue == "Cancelled")
                {
                    p33.Value = "C";
                }

                _result = objData.SaveDeleteData("TF_EDPMS_INW_File_Insert", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, pRemitterID, p14, p15, p16, p17, p18, p19, p20, pRBC, p21, p22, p23, p24,
                     p25, p26, p27, p28, p29, p30, p31, p32, p33);

                string _OldValues = "";// Added by Anand 10-01-2024
                string _NewValues = "";// Added by Anand 10-01-2024
                string _Status = "M";//Added by Anand 10-01-2024
                string _query = "";//Added by Anand 10-01-2024
                string _userName = Session["userName"].ToString(); ;//Added by Anand 10-01-2024
                string _script = "";//Added by Anand 10-01-2024

               
                if (_result == "CancelNotexist" || _result == "AmendNotexist")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Create Fresh Transaction.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "CanelDGFTNotProcess")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Create cancel transaction Processed in DGFT.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "FreshTransactionnotprocessDGFT" || _result == "cancelnotcreated" || _result == "Amendnotcreated")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Create fresh transaction Processed in DGFT.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "FreshTranssactionCreated")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Fresh Transaction Already Created.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "Frestnotfound")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Cancel transaction does not Processed in DGFT.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "CancelCreated")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Cancel transaction Already Created.')", true);
                    txtDocNo.Focus();
                }
                if (_result == "added")
                {
                    string __docNo = _docno;
                    AuditIRM(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 10-01-2024
                    txtDocNo.Text = ""; txtCustAcNo.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added with Document No. : " + __docNo + "');", true);
                    string url = "../Reports/EXPReports/View_INWfileEntry.aspx?DocNo=" + __docNo;
                    script = "window.open('" + url + "','_blank','height=600,width=800,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    var script1 = "window.location ='EDPMS_INW_File_DataEntryView.aspx';"; // Added Anand 05-07-2023
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
                }
                else
                    if (_result == "updated")
                    {
                        string __docNo = _docno;
                        AuditIRM(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 10-01-2024
                        txtDocNo.Text = ""; txtCustAcNo.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        string url = "../Reports/EXPReports/View_INWfileEntry.aspx?DocNo=" + __docNo;
                        script = "window.open('" + url + "','_blank','height=600,width=800,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                        var script1 = "window.location ='EDPMS_INW_File_DataEntryView.aspx';"; // Added Anand 05-07-2023
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
                    }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@currencyid", ddlCurrency.SelectedValue);
        string _query = "TF_GetCurrencyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblcurrencyDesc.Text = dt.Rows[0]["C_Description"].ToString();
            txtAmount.Focus();
        }
        else
        {
            lblcurrencyDesc.Text = "";
        }
        Check_LEI_ThresholdLimit();
    }
    protected void txtDocNo_TextChanged(object sender, EventArgs e)
    {
        // txtBankReferencenumber.Text = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
        if (txtDocPref.Text.Trim().ToUpper() == "ITT")
        {
            int SrLen = txtDocNo.Text.Length;
            if (txtDocNo.Text.Length < 6)
            {
                for (int i = 6; i > SrLen; i--)
                {
                    txtDocNo.Text = "0" + txtDocNo.Text;
                }
            }
            txtBankReferencenumber.Text = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
        }
        else
        {
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@DocSrNo", txtDocSrNo.Text.Trim());
            DataTable dt = objData.getData("TF_EXP_GetINWDocNoDetails", p1, p2);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SRNO"].ToString() == "NOT EXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Document No does not exists.')", true);
                    txtDocNo.Text = "";
                    txtDocSrNo.Text = "";
                }
                else
                {
                    string docsrno = ("000000" + dt.Rows[0]["SRNO"].ToString());
                    txtDocSrNo.Text = docsrno.Substring(docsrno.Length - 2, 2);
                    //txtDocDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    // txtDocDate.Enabled = false;// Added by Anand 07-07-2023
                    // btncalendar_DocDate.Enabled = false; // Added by Anand 07-07-2023
                    //txtvalueDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); // comment by Anand 07-07-2023

                    SqlParameter a1 = new SqlParameter("@DocumentNo", txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim());
                    DataTable dt2 = objData.getData("TF_EXP_GetExportBillEntryDetails", a1);
                    if (dt2.Rows.Count > 0)
                    {
                        txtCustAcNo.Text = dt2.Rows[0]["CustAcNo"].ToString().Trim();
                        txtCustAcNo_TextChanged(this, null);
                    }

                    txtPurposeCode.Text = "P0102";
                    txtPurposeCode_TextChanged(this, null);

                    ddlCurrency.SelectedValue = dt2.Rows[0]["Currency"].ToString().Trim();
                    txtvalueDate.Focus();
                }
            }
            txtBankReferencenumber.Text = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();

        }
        
    }
    protected void txtDocSrNo_TextChanged(object sender, EventArgs e)
    {

        if (txtDocPref.Text.Trim().ToUpper() != "ITT")
        {
            //string docsrno_old = txtDocSrNo.Text.Trim();

            SqlParameter p1 = new SqlParameter("@DocNo", txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@DocSrNo", txtDocSrNo.Text.Trim());
            DataTable dt = objData.getData("TF_EXP_GetINWDocNoDetails", p1, p2);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SRNO"].ToString() == "NOT EXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Document No does not exists.')", true);
                }
                else
                {
                    string docsrno = ("000000" + dt.Rows[0]["SRNO"].ToString());
                    //if (docsrno != (docsrno.Substring(docsrno.Length - 2, 2)))
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Document No already exists')", true);
                    //}
                    txtDocSrNo.Text = docsrno.Substring(docsrno.Length - 2, 2);
                    txtDocDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtvalueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    SqlParameter a1 = new SqlParameter("@DocumentNo", txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim());
                    DataTable dt2 = objData.getData("TF_EXP_GetExportBillEntryDetails", a1);
                    if (dt2.Rows.Count > 0)
                    {
                        txtCustAcNo.Text = dt2.Rows[0]["CustAcNo"].ToString().Trim();
                        txtCustAcNo_TextChanged(this, null);
                    }

                    txtPurposeCode.Text = "P0102";
                    txtPurposeCode_TextChanged(this, null);
                    ddlCurrency.SelectedValue = dt2.Rows[0]["Currency"].ToString();
                    txt_swiftcode.Focus();
                }
            }

        }

    }
    protected void txtCustAcNo_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@customerACNo", txtCustAcNo.Text);
        FillPanNO();
        SqlParameter p2 = new SqlParameter("@branch", txtBranchCode.Text);
        DataTable dt = objData.getData("TF_INW_GetCustDetails", p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtIECCode.Text = dt.Rows[0]["CUST_IE_CODE"].ToString();// Anand 19-06-2023
            //txtPanNumber.Text = dt.Rows[0]["CUST_PAN_NO"].ToString();
            hdnCustname.Value = lblCustName.Text;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Cust A/C No.')", true);
            txtCustAcNo.Text = "";
            txtCustAcNo.Focus();
        }
        Check_LEI_ThresholdLimit();

    }
    protected void txtPurposeCode_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@purposecode", txtPurposeCode.Text);
        DataTable dt = objData.getData("TF_GetPurposeCodeMasterDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblpurposeCode.Text = dt.Rows[0]["description"].ToString();
            txtRemitterName.Focus();

        }
        else
        {
            txtPurposeCode.Text = "";
            lblpurposeCode.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid PurposeCode.')", true);
            txtPurposeCode.Focus();
        }
    }
    protected void txtRemitterCountry_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@cid", txtRemitterCountry.Text.Trim());
        DataTable dt = objData.getData("TF_GetCountryDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
            txtRemitterAddress.Focus();
        }
        else
        {
            lblCountryDesc.Text = "";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Country.')", true);
            txtRemitterCountry.Focus();
        }
    }
    protected void txtRemBankCountry_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@cid", txtRemBankCountry.Text.Trim());
        DataTable dt = objData.getData("TF_GetCountryDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblRemBankCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
            txtRemitterBankAddress.Focus();
        }
        else
        {
            lblRemBankCountryDesc.Text = "";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Country.')", true);
            txtRemBankCountry.Focus();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_INW_File_DataEntryView.aspx");
    }
    protected void txt_swiftcode_TextChanged(object sender, EventArgs e)
    {
        if (txt_swiftcode.Text.Trim() != "")
        {
            SqlParameter p1 = new SqlParameter("@SwiftCode", txt_swiftcode.Text.Trim());
            DataTable dt = objData.getData("TF_GetBankDetailsBySwiftCode", p1);
            if (dt.Rows.Count > 0)
            {
                txtRemitterBank.Text = dt.Rows[0]["BankName"].ToString();
                txtRemitterBankAddress.Text = dt.Rows[0]["BankAddress"].ToString();
                txtRemitterBankAddress.ToolTip = txtRemitterBankAddress.Text;
                txtRemitterBank.ToolTip = txtRemitterBank.Text;
                txtRemBankCountry.Text = dt.Rows[0]["Country"].ToString();
                // txtIFSCCode.Text = dt.Rows[0]["IFSC_Code"].ToString(); // Anand 19-06-2023
                txtRemBankCountry_TextChanged(null, null);
                txtRemitterName.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Swift Code does not exist in Overseas Bank Master.')", true);
                txtRemitterBank.Text = "";
                txtRemitterBankAddress.Text = "";
                txtRemBankCountry.Text = "";
                lblCountryDesc.Text = "";
                txtRemitterName.Focus();
            }
        }

    }
    private void fillOverseasPartyDescription()
    {
        lblOverseasPartyDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = txtOverseasPartyID.Text;

        string _query = "TF_GetOverseasPartyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasPartyDesc.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            ///////added by shailesh along with lie change
            txtRemitterName.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            txtRemitterName.ToolTip = txtRemitterName.Text;
            txtRemitterCountry.Text = dt.Rows[0]["Party_Country"].ToString().Trim();
            txtRemitterAddress.Text = dt.Rows[0]["Party_Address"].ToString().Trim();
            txtRemitterAddress.ToolTip = txtRemitterAddress.Text;

        }
        else
        {
            txtOverseasPartyID.Text = "";
            lblOverseasPartyDesc.Text = "";
        }
        txtRemitterCountry_TextChanged(null, null);
    }
    protected void btnOverseasParty_Click(object sender, EventArgs e)
    {
        if (hdnOverseasPartyId.Value != "")
        {
            txtOverseasPartyID.Text = hdnOverseasPartyId.Value;
            fillOverseasPartyDescription();
            txtOverseasPartyID.Focus();
            Check_LEI_ThresholdLimit();
        }
    }
    protected void txtOverseasPartyID_TextChanged(object sender, EventArgs e)
    {
        fillOverseasPartyDescription();
        Check_LEI_ThresholdLimit();
    }
    protected void btnLEI_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCustAcNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Select Customer for Verifying LEI details.')", true);
                txtCustAcNo.Focus();
            }
            else if (txtOverseasPartyID.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Select Remitter ID for Verifying LEI details.')", true);
                txtOverseasPartyID.Focus();
            }
            else if (ddlCurrency.SelectedItem.Text.Trim() == "Select")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Select Currency for Verifying LEI details.')", true);
                // txtCustAcNo.Focus();
            }
            else if (txtAmount.Text.Trim() == "" || txtAmount.Text.Trim() == "0.00")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Amount for Verifying LEI details.')", true);
                txtAmount.Focus();
            }
            else if (txtvalueDate.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Value date for Verifying LEI details.')", true);
                txtvalueDate.Focus();
            }
            else
            {
                Check_CUST_LEINODetails();
                Check_CUST_LEINO_ExpirydateDetails();
                Check_Overseas_LEINODetails();
                Check_Overseas_LEINO_ExpirydateDetails();

                String CustLEINo = lblLEI_CUST_Remark.Text;
                String CustLEIExpiry = lblLEIExpiry_CUST_Remark.Text;
                String ApplicantLEINo = lblLEI_Overseas_Remark.Text;
                String ApplicantLEIExpiry = lblLEIExpiry_Overseas_Remark.Text;

                if (LEIAmtCheck.Text != "")
                {
                    LEIverify.Text = "";
                    LEIverify.Text = "LEI checked.";
                    LEIverify.ForeColor = System.Drawing.Color.LimeGreen;
                }
                hdnValidateLei.Value = "";
                if (hdncustlei.Value == "" || hdncustleiexpiryRe.Value == "")  //// remove as per user suggesions /   || hdnoverseaslei.Value == "" || hdnoverseasleiexpiryRe.Value == ""
                {
                    hdnValidateLei.Value = "LeiFalse";
                }

                String LEIMSG = @"Customer LEI : " + CustLEINo + "\\n" + "Customer LEI Expiry : " + CustLEIExpiry + "\\n" + "Applicant LEI : " + ApplicantLEINo + "\\n" + "Applicant LEI Expiry : " + ApplicantLEIExpiry;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "messege", "alert('" + LEIMSG + "')", true);
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI Number for this customer is not available.')", true);
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_CUST_LEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            String CustAbbrLEI = "";
            SqlParameter C1 = new SqlParameter("@custid", SqlDbType.VarChar);
            C1.Value = txtCustAcNo.Text;
            string _queryC = "TF_rptGetCustomerMasterDetails";
            DataTable dtC = objData.getData(_queryC, C1);
            if (dtC.Rows.Count > 0)
            {
                CustAbbrLEI = dtC.Rows[0]["Cust_Abbr"].ToString().Trim();
                hdnCustabbr.Value = CustAbbrLEI;
            }
            SqlParameter p1 = new SqlParameter("@CustAbbr", CustAbbrLEI);
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //p1.Value = txtCustomer_ID.Text;
            string _query = "TF_EXP_GetLEIDetails_Customer";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LEI_No1"].ToString() == "")
                {
                    lblLEI_CUST_Remark.Text = " " + "...Not Verified.";
                    lblLEI_CUST_Remark.ForeColor = System.Drawing.Color.Red;
                    hdncustlei.Value = "";
                }
                else
                {
                    lblLEI_CUST_Remark.Text = dt.Rows[0]["LEI_No1"].ToString() + "...Verified.";
                    lblLEI_CUST_Remark.ForeColor = System.Drawing.Color.Green;
                    hdncustlei.Value = dt.Rows[0]["LEI_No1"].ToString();
                }
            }
            else
            {
                lblLEI_CUST_Remark.Text = "Customer abbr not available in C-HUB.";
                lblLEI_CUST_Remark.ForeColor = System.Drawing.Color.Red;
            }
            //lblLEI_CUST_Remark.Visible = true;
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_CUST_LEINO_ExpirydateDetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            String CustAbbrLEIDate = "";
            SqlParameter C1 = new SqlParameter("@custid", SqlDbType.VarChar);
            C1.Value = txtCustAcNo.Text;
            string _queryC = "TF_rptGetCustomerMasterDetails";
            DataTable dtC = objData.getData(_queryC, C1);
            if (dtC.Rows.Count > 0)
            {
                CustAbbrLEIDate = dtC.Rows[0]["Cust_Abbr"].ToString().Trim();
            }
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

            DateTime mDate = new DateTime();


            SqlParameter p1 = new SqlParameter("@CustAbbr", CustAbbrLEIDate);
            SqlParameter p2 = new SqlParameter("@DueDate", txtvalueDate.Text.ToString().Trim());
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //p1.Value = txtCustomer_ID.Text;
            string _query = "TF_EXP_GetLEIDetails_ExpiryDate";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Statuschk"].ToString() == "Greater")
                {
                    lblLEIExpiry_CUST_Remark.Text = dt.Rows[0]["LEI_Expiry_Date"].ToString() + "...Verified.";
                    lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Green;
                    hdncustleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdncustleiexpiryRe.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_CUST_Remark.Text = dt.Rows[0]["LEI_Expiry_Date"].ToString() + "...Not Verified.";
                    lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Red;
                    hdncustleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdncustleiexpiryRe.Value = "";
                }
            }
            else
            {
                lblLEIExpiry_CUST_Remark.Text = " " + "...Not Verified.";
                lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Red;
                hdncustleiexpiry.Value = "";
                hdncustleiexpiryRe.Value = "";
            }
            //lblLEIExpiry_CUST_Remark.Visible = true;
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }

    private void Check_Overseas_LEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();

            SqlParameter p1 = new SqlParameter("@PartyCode", txtOverseasPartyID.Text.Trim());
            //SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //p1.Value = txtCustomer_ID.Text;
            string _query = "TF_EXP_GetLEIDetails_Overseas";
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Party_LEI_No"].ToString() == "")
                {
                    lblLEI_Overseas_Remark.Text = " " + "...Not Verified.";
                    lblLEI_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
                    hdnoverseaslei.Value = "";
                }
                else
                {
                    lblLEI_Overseas_Remark.Text = dt.Rows[0]["Party_LEI_No"].ToString() + "...Verified.";
                    lblLEI_Overseas_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnoverseaslei.Value = dt.Rows[0]["Party_LEI_No"].ToString();
                }
            }
            else
            {
                lblLEI_Overseas_Remark.Text = "Remitter ID is not available."; ;
                lblLEI_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
            }
            //lblLEI_Overseas_Remark.Visible = true;
            //SpanLei3.Visible = true;
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_Overseas_LEINO_ExpirydateDetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();

            SqlParameter p1 = new SqlParameter("@PartyCode", txtOverseasPartyID.Text.Trim());
            SqlParameter p2 = new SqlParameter("@DueDate", txtvalueDate.Text.ToString().Trim());
            //SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //p1.Value = txtCustomer_ID.Text;
            string _query = "TF_EXP_GetLEIDetails_ExpiryDate_Overseas";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Statuschk"].ToString() == "Greater")
                {
                    lblLEIExpiry_Overseas_Remark.Text = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString() + "...Verified.";
                    lblLEIExpiry_Overseas_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnoverseasleiexpiry.Value = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString();
                    hdnoverseasleiexpiryRe.Value = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString();
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Overseas_Remark.Text = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString() + "...Not Verified.";
                    lblLEIExpiry_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
                    hdnoverseasleiexpiry.Value = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString();
                    hdnoverseasleiexpiryRe.Value = "";
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_Overseas_Remark.Text = " " + "...Not Verified.";
                lblLEIExpiry_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
                hdnoverseasleiexpiry.Value = "";
                hdnoverseasleiexpiryRe.Value = "";
            }
            //lblLEIExpiry_Overseas_Remark.Visible = true;
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }

    private void Check_LEI_ThresholdLimit()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CurrCode", SqlDbType.VarChar);
            SqlParameter p2 = new SqlParameter("@Date", txtvalueDate.Text);
            p1.Value = ddlCurrency.SelectedItem.Text.ToString();
            string _query = "TF_EXP_GetLEI_RateCardDetails";
            DataTable dt = objData.getData(_query, p1, p2);
            string Exch_rate = "";
            lbl_Exch_rate.Text = "";
            if (dt.Rows.Count > 0)
            {
                Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
            }
            string txtExch = txtExchangeRate.Text.ToString();
            hdnleiExchRate.Value = "";
            if (txtExch != "0.0000000000" && txtExch != "1.0000000000" && txtExch != "0.00000" && txtExch != "1.00000" && txtExch != "1" && txtExch != "0")
            {
                Exch_rate = txtExchangeRate.Text;
            }
            hdnleiExchRate.Value = Exch_rate;
            LEIAmtCheck.Text = "";
            string result = "";
            string txtbillamt = txtAmount.Text;
            string exchange_Rate = Exch_rate;

            if ((txtbillamt != "0" && txtbillamt != "") && (exchange_Rate != "0" && exchange_Rate != ""))
            {
                if (txtbillamt != "0.00" && (exchange_Rate != "0.0000000000" || exchange_Rate != "0.00000"))
                {
                    SqlParameter billamt = new SqlParameter("@billamt", txtbillamt);
                    SqlParameter exch_Rate = new SqlParameter("@exch_Rate", exchange_Rate);
                    string _queryC = "TF_GetThresholdLimitCheck";
                    DataTable dtLimit = objData.getData(_queryC, billamt, exch_Rate);

                    if (dtLimit.Rows[0]["checkstaus"].ToString() == "Greater")
                    {
                        btnLEI.Visible = true;
                        LEIAmtCheck.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit. ";

                        LEIverify.Text = "";
                        LEIverify.Text = "Please Verify LEI.";
                        LEIverify.ForeColor = System.Drawing.Color.Red;

                        LEIAmtCheck.ForeColor = System.Drawing.Color.Red;
                        LEIAmtCheck.Font.Size = 13;
                        hdnLeiFlag.Value = "Y";
                        hdnLeiSpecialFlag.Value = "Y";
                        hdnbillamtinr.Value = dtLimit.Rows[0]["billamtInr"].ToString().Trim();

                    }
                    else if (dtLimit.Rows[0]["checkstaus"].ToString() == "less")
                    {
                        btnLEI.Visible = false;
                        btnAdd.Visible = true;
                        btnPrint.Visible = true;
                        LEIAmtCheck.Text = "Transaction Bill Amt is less than LEI Thresold Limit. ";
                        LEIverify.Text = "";
                        LEIAmtCheck.ForeColor = System.Drawing.Color.Green;
                        LEIAmtCheck.Font.Size = 13;
                        hdnLeiFlag.Value = "N";
                        hdnLeiSpecialFlag.Value = "";
                        hdnbillamtinr.Value = dtLimit.Rows[0]["billamtInr"].ToString().Trim();
                        hdncustlei.Value = hdncustleiexpiry.Value = hdnoverseaslei.Value = hdnoverseasleiexpiry.Value = "";
                        lblLEI_CUST_Remark.Text = lblLEIExpiry_CUST_Remark.Text = lblLEI_Overseas_Remark.Text = lblLEIExpiry_Overseas_Remark.Text = "";
                        lblLEI_CUST_Remark.Visible = false; lblLEIExpiry_CUST_Remark.Visible = false; lblLEI_Overseas_Remark.Visible = false; lblLEIExpiry_Overseas_Remark.Visible = false;
                        // SpanLei1.Visible = false; SpanLei2.Visible = false; SpanLei3.Visible = false; SpanLei4.Visible = false;
                    }
                    Check_LEI_SpecialFlag();
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }

    private void Check_LEI_SpecialFlag()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            String CustAbbrLEI = "";
            SqlParameter C1 = new SqlParameter("@custid", SqlDbType.VarChar);
            C1.Value = txtCustAcNo.Text;
            string _queryC = "TF_rptGetCustomerMasterDetails";
            DataTable dtC = objData.getData(_queryC, C1);
            if (dtC.Rows.Count > 0)
            {
                CustAbbrLEI = dtC.Rows[0]["Cust_Abbr"].ToString().Trim();
                hdnCustabbr.Value = CustAbbrLEI;
            }
            SqlParameter p1 = new SqlParameter("@CustAbbr", CustAbbrLEI);
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            SqlParameter p3 = new SqlParameter("@Trans_Status", "A");
            string _query = "TF_EXP_GetLEISpecial_Customer";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                hdnLeiSpecialFlag.Value = "R";
                ReccuringLEI.Visible = true;
                ReccuringLEI.Text = "This is Recurring LEI Customer.";
                ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                btnLEI.Visible = true;
            }
            else
            {
                ReccuringLEI.Text = "";
                ReccuringLEI.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    //static SqlDataAdapter da;
    //static DataTable dt;

    //[System.Web.Script.Services.ScriptMethod()]
    //[System.Web.Services.WebMethod]
    //public static List<string> GetSearch(string prefixText)
    //{
    //    //DataTable Result = new DataTable();
    //    //string str = "select nvName from Friend where nvName like '" + prefixText + "%'";
    //    //da = new SqlDataAdapter(str, con);
    //    //dt = new DataTable();


    //    //da.Fill(dt);
    //    //List<string> Output = new List<string>();

    //    //Output.Add("BLA");
    //    //Output.Add("BLU");
    //    //Output.Add("BCA");
    //    ////for (int i = 0; i < dt.Rows.Count; i++)
    //    ////    Output.Add(dt.Rows[i][0].ToString());
    //    //return Output;

    //    List<string> Output = new List<string>();
    //    string[] values = { "BLA", "BLU", "BBA", "BBU", "BCA", "BCU", "ITT", "IBD", "EB" };
    //    Output.AddRange(from tmp in values where tmp.ToLower().StartsWith(prefixText) select tmp);
    //    return Output;
    //}
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        Check_LEI_ThresholdLimit();
    }
    protected void txtExchangeRate_TextChanged(object sender, EventArgs e)
    {
        Check_LEI_ThresholdLimit();
    }


    public void FillIFSCTemittanceAdCode()
    {
        SqlParameter p1 = new SqlParameter("@BranchCode", txtBranchCode.Text);
        DataTable dt = objData.getData("TF_GetIFSCRemittanceDetails", p1);
        if (dt.Rows.Count > 0)
        {
            txtIFSCCode.Text = dt.Rows[0]["RTGSCode"].ToString();
            txtRemittanceADCode.Text = dt.Rows[0]["AuthorizedDealerCode"].ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('No Records Branch.')", true);

        }
    }

    public void FillPanNO()
    {
        SqlParameter p1 = new SqlParameter("@CustAcNo", txtCustAcNo.Text);
        DataTable dt = objData.getData("TF_GETPenCardDetails", p1);
        if (dt.Rows.Count > 0)
        {
            txtPanNumber.Text = dt.Rows[0]["PAN_No"].ToString();
            txtBankAccountNumber.Text = dt.Rows[0]["Password_Account_No"].ToString();// Anand 11-08-2023

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Cust ABBR.')", true);
            txtPanNumber.Text = "";
            txtBankAccountNumber.Text = "";

        }
    }

    protected void CheckerViewOnly()
    {
        txtBranchCode.Enabled = txtYear.Enabled = txtDocPref.Enabled = txtDocBRCode.Enabled = txtDocNo.Enabled = txtDocSrNo.Enabled = btnDocNoList.Enabled = txtDocDate.Enabled = btncalendar_DocDate.Enabled = txtvalueDate.Enabled =
        Button3.Enabled = txt_swiftcode.Enabled = btnHelpSwiftCode.Enabled = txtCustAcNo.Enabled = btnHelpCustAcNo.Enabled = txtPurposeCode.Enabled = Button1.Enabled = txtOverseasPartyID.Enabled = btnOverseasPartyList.Enabled =
        txtRemitterName.Enabled = txtRemitterCountry.Enabled = Button4.Enabled = txtRemitterAddress.Enabled = txtRemitterBank.Enabled = txtRemBankCountry.Enabled = Button5.Enabled = txtRemitterBankAddress.Enabled = txtpurposeofRemittance.Enabled =
        ddlCurrency.Enabled = txtAmount.Enabled = txtExchangeRate.Enabled = txtAmtInINR.Enabled = txtFIRCNo.Enabled = txtFIRCDate.Enabled = Button2.Enabled = txtADCode.Enabled = txtBankUniqueTransactionID.Enabled = txtIFSCCode.Enabled =
        txtRemittanceADCode.Enabled = txtIECCode.Enabled = txtPanNumber.Enabled = ddlModeOfPayment.Enabled = txtBankReferencenumber.Enabled = txtBankAccountNumber.Enabled = ddlIRMStatus.Enabled = false;
    }

    public void AuditIRM(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result, string _Status)
    {
        if (_result == "added")
        {
            string _branchCode = Request.QueryString["BranchCode"].Trim();

            TF_DATA objSave = new TF_DATA();
            _query = "TF_Data_AuditTrailInward";
            _NewValues = "Document Date:" + txtDocDate.Text.Trim() + ";Value Date: " + txtvalueDate.Text.Trim() + ";Swift Code: " + txt_swiftcode.Text.Trim() +
                ";Customer A/C No: " + txtCustAcNo.Text.Trim() + ";Purpose Code: " + txtPurposeCode.Text.Trim() + ";Remitter ID:" + txtOverseasPartyID.Text.Trim()
                   + ";Remitter Name :" + txtRemitterName.Text.Trim() + ";Remitter Country:" + txtRemitterCountry.Text.Trim() + ";Remitter Address:" + txtRemitterAddress.Text.Trim()
                   + ";Remitting Bank :" + txtRemitterBank.Text.Trim() + ";Bank Country:" + txtRemBankCountry.Text.Trim() + ";Remitting Bank Address:" + txtRemitterBankAddress.Text.Trim()
                   + ";Purpose Of Remittance :" + txtpurposeofRemittance.Text.Trim() + ";Currency:" + ddlCurrency.SelectedValue.Trim() + ";Amount In FC:" + txtAmount.Text.Trim()
                   + ";Exchange Rate :" + txtExchangeRate.Text.Trim() + ";Amount In INR:" + txtAmtInINR.Text.Trim() + ";FIRC NO:" + txtFIRCNo.Text.Trim()
                   + ";FIRC Date :" + txtFIRCDate.Text.Trim() + ";FIRC AD Code:" + txtADCode.Text.Trim() + ";Bank Unique Transaction ID:" + txtBankUniqueTransactionID.Text.Trim()
                   + ";IFSC Code :" + txtIFSCCode.Text.Trim() + ";Remittance AD Code:" + txtRemittanceADCode.Text.Trim() + ";IEC Code:" + txtIECCode.Text.Trim()
                   + ";Pan Number :" + txtPanNumber.Text.Trim() + ";Mode of Payment:" + ddlModeOfPayment.SelectedValue.Trim() + ";Bank Reference Number:" + txtBankReferencenumber.Text.Trim()
                   + ";Bank Account Number :" + txtBankAccountNumber.Text.Trim() + ";IRM Status:" + ddlIRMStatus.SelectedValue.Trim()
                   + ";Checkar Status :" + _Status;


            SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            Branch.Value = _branchCode;
            SqlParameter Mod = new SqlParameter("@Module", SqlDbType.VarChar);
            Mod.Value = "EXP";
            SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
            oldvalues.Value = _OldValues;
            SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
            newvalues.Value = _NewValues;
            SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
            Acno.Value = txtCustAcNo.Text;
            string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = _docno;
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDocDate.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Add");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Inward Remittance Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu);
        }

        else
        {

            TF_DATA objSave = new TF_DATA();
            int isneedtolog = 0;
            string NewValues = "";
            _OldValues = "Document Date:" + hdnDocumentDate.Value.ToString() + ";Value Date: " + hdnValueDate.Value.ToString() + ";Swift Code: " + hdnSwiftCode.Value.ToString() +
                ";Customer A/C No: " + hdnCustomerACNo.Value.ToString() + ";Purpose Code: " + hdnPurposeCode.Value.ToString() + ";Remitter ID:" + hdnRemitterID.Value.ToString()
                   + ";Remitter Name :" + hdnRemitterName.Value.ToString() + ";Remitter Country:" + hdnRemitterCountry.Value.ToString() + ";Remitter Address:" + hdnRemitterAddress.Value.ToString()
                   + ";Remitting Bank :" + hdnRemittingBank.Value.ToString() + ";Bank Country:" + hdnBankCountry.Value.ToString() + ";Remitting Bank Address:" + hdnRemittingBankAddress.Value.ToString()
                   + ";Purpose Of Remittance :" + hdnPurposeOfRemittance.Value.ToString() + ";Currency:" + hdnCurrency.Value.ToString() + ";Amount In FC:" + hdnAmountInFC.Value.ToString()
                   + ";Exchange Rate :" + hdnExchangeRate.Value.ToString() + ";Amount In INR:" + hdnAmountInINR.Value.ToString() + ";FIRC NO:" + hdnFIRCNO.Value.ToString()
                   + ";FIRC Date :" + hdnFIRCDate.Value.ToString() + ";FIRC AD Code:" + hdnFIRCADCode.Value.ToString() + ";Bank Unique Transaction ID:" + hdnBankUniqueTransactionID.Value.ToString()
                   + ";IFSC Code :" + hdnIFSCCode.Value.ToString() + ";Remittance AD Code:" + hdnRemittanceADCode.Value.ToString() + ";IEC Code:" + hdnIECCode.Value.ToString()
                   + ";Pan Number :" + hdnPanNumber.Value.ToString() + ";Mode of Payment:" + hdnModeofPayment.Value.ToString() + ";Bank Reference Number:" + hdnBankReferenceNumbe.Value.ToString()
                   + ";Bank Account Number :" + hdnBankAccountNumber.Value.ToString() + ";IRM Status:" + hdnIRMStatus.Value.ToString()
                   + ";Checkar Status :" + hdnCheckarStatus.Value.ToString();

            if (hdnDocumentDate.Value != txtDocDate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Document Date : " + txtDocDate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Document Date : " + txtDocDate.Text.Trim();
                }

            }


            if (hdnValueDate.Value != txtvalueDate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Value Date : " + txtvalueDate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Value Date : " + txtvalueDate.Text.Trim();
                }

            }

            if (hdnSwiftCode.Value != txt_swiftcode.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Swift Code : " + txt_swiftcode.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Swift Code : " + txt_swiftcode.Text.Trim();
                }

            }

            if (hdnCustomerACNo.Value != txtCustAcNo.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Customer A/C No : " + txtCustAcNo.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Customer A/C No : " + txtCustAcNo.Text.Trim();
                }

            }
            if (hdnPurposeCode.Value != txtPurposeCode.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Purpose Code : " + txtPurposeCode.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Purpose Code : " + txtPurposeCode.Text.Trim();
                }

            }

            if (hdnRemitterID.Value != txtOverseasPartyID.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Remitter ID : " + txtOverseasPartyID.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Remitter ID : " + txtOverseasPartyID.Text.Trim();
                }

            }
            if (hdnRemitterName.Value != txtRemitterName.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Remitter Name : " + txtRemitterName.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Remitter Name : " + txtRemitterName.Text.Trim();
                }

            }

            if (hdnRemitterCountry.Value != txtRemitterCountry.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Remitter Country : " + txtRemitterCountry.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Remitter Country : " + txtRemitterCountry.Text.Trim();
                }

            }
            if (hdnRemitterAddress.Value != txtRemitterAddress.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Remitter Address : " + txtRemitterAddress.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Remitter Address : " + txtRemitterAddress.Text.Trim();
                }

            }

            if (hdnRemittingBank.Value != txtRemitterBank.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Remitting Bank : " + txtRemitterBank.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Remitting Bank : " + txtRemitterBank.Text.Trim();
                }

            }
            if (hdnBankCountry.Value != txtRemBankCountry.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + " Bank Country : " + txtRemBankCountry.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + ";  Bank Country : " + txtRemBankCountry.Text.Trim();
                }

            }
            if (hdnRemittingBankAddress.Value != txtRemitterBankAddress.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Remitting Bank Address : " + txtRemitterBankAddress.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Remitting Bank Address : " + txtRemitterBankAddress.Text.Trim();
                }

            }

            if (hdnPurposeOfRemittance.Value != txtpurposeofRemittance.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Purpose Of Remittance : " + txtpurposeofRemittance.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Purpose Of Remittance : " + txtpurposeofRemittance.Text.Trim();
                }

            }


            if (hdnCurrency.Value != ddlCurrency.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Currency : " + ddlCurrency.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Currency : " + ddlCurrency.SelectedValue.Trim();
                }

            }


            if (hdnAmountInFC.Value != txtAmount.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Amount In FC : " + txtAmount.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Amount In FC : " + txtAmount.Text.Trim();
                }

            }


            if (hdnExchangeRate.Value != txtExchangeRate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Exchange Rate : " + txtExchangeRate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Exchange Rate : " + txtExchangeRate.Text.Trim();
                }

            }


            if (hdnAmountInINR.Value != txtAmtInINR.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Amount In INR : " + txtAmtInINR.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Amount In INR : " + txtAmtInINR.Text.Trim();
                }

            }

            if (hdnFIRCNO.Value != txtFIRCNo.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "FIRC NO : " + txtFIRCNo.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; FIRC NO : " + txtFIRCNo.Text.Trim();
                }

            }

            if (hdnFIRCDate.Value != txtFIRCDate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "FIRC Date : " + txtFIRCDate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; FIRC Date : " + txtFIRCDate.Text.Trim();
                }

            }
            if (hdnFIRCADCode.Value != txtADCode.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "FIRC AD Code : " + txtADCode.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; FIRC AD Code : " + txtADCode.Text.Trim();
                }

            }

            if (hdnBankUniqueTransactionID.Value != txtBankUniqueTransactionID.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Bank Unique Transaction ID : " + txtBankUniqueTransactionID.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Bank Unique Transaction ID : " + txtBankUniqueTransactionID.Text.Trim();
                }

            }

            if (hdnIFSCCode.Value != txtIFSCCode.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + " IFSC Code : " + txtIFSCCode.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + ";  IFSC Code : " + txtIFSCCode.Text.Trim();
                }

            }

            if (hdnRemittanceADCode.Value != txtRemittanceADCode.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Remittance AD Code : " + txtRemittanceADCode.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Remittance AD Code : " + txtRemittanceADCode.Text.Trim();
                }

            }

            if (hdnIECCode.Value != txtIECCode.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "IEC Code : " + txtIECCode.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; IEC Code : " + txtIECCode.Text.Trim();
                }

            }

            if (hdnPanNumber.Value != txtPanNumber.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Pan Number : " + txtPanNumber.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Pan Number : " + txtPanNumber.Text.Trim();
                }

            }

            if (hdnModeofPayment.Value != ddlModeOfPayment.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Mode of Payment : " + ddlModeOfPayment.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Mode of Payment : " + ddlModeOfPayment.SelectedValue.Trim();
                }

            }

            if (hdnBankReferenceNumbe.Value != txtBankReferencenumber.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Bank Reference Number : " + txtBankReferencenumber.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Bank Reference Number : " + txtBankReferencenumber.Text.Trim();
                }

            }

            if (hdnBankAccountNumber.Value != txtBankAccountNumber.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Bank Account Number : " + txtBankAccountNumber.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Bank Account Number : " + txtBankAccountNumber.Text.Trim();
                }

            }

            if (hdnIRMStatus.Value != ddlIRMStatus.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "IRM Status : " + ddlIRMStatus.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; IRM Status : " + ddlIRMStatus.SelectedValue.Trim();
                }

            }
            if (hdnCheckarStatus.Value != _Status)
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Checkar Status : " + _Status;
                }
                else
                {
                    _NewValues = _NewValues + "; Checkar Status : " + _Status;
                }
            }
            _query = "TF_Data_AuditTrailInward";
            string _branchCode = Request.QueryString["BranchCode"].Trim();
            SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            Branch.Value = _branchCode;
            SqlParameter Mod = new SqlParameter("@Module", SqlDbType.VarChar);
            Mod.Value = "EXP";
            SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
            oldvalues.Value = _OldValues;
            SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
            newvalues.Value = _NewValues;
            SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
            Acno.Value = txtCustAcNo.Text.Trim();
            string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = _docno;
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDocDate.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Modify");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Inward Remittance Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            if (isneedtolog == 1)
            {
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu);
            }
        }
    }
    }