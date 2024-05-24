using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Text;
using System.IO;
using System.Collections;
using System.Web.Services;

public partial class EXP_EDPMS_INW_File_DataEntry_Checker : System.Web.UI.Page
{
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
                hdnUserName.Value = Session["userName"].ToString();
                disabletext();
                txtBranchCode.Text = Request.QueryString["branchcode"].ToString();
                hdnBranchCode.Value = Request.QueryString["branchcode"].Trim();
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
                    fillDetails();
                    Check_LEI_ThresholdLimit();
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {

                       // ddlApproveReject.SelectedValue = "Approve";
                        ddlApproveReject.Enabled = false;
                        lblChecker_Remark.Visible = false;

                    }
                    if (Request.QueryString["Status"].ToString() == "Sent To Checker")
                    {
                        TF_DATA obj = new TF_DATA();
                        SqlParameter p1 = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
                        SqlParameter p2 = new SqlParameter("@Checker", hdnUserName.Value);
                        string Result = obj.SaveDeleteData("TF_EXP_SameTransIdCheckerAlert_IRM", p1, p2);
                    }
                }
                ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
            }
        }
    }

    [WebMethod]
    public static void ExecuteCSharpCodeLodg(string docNo, string checker)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@DocNo", docNo);
        SqlParameter p2 = new SqlParameter("@Checker", checker);
        string Result = obj.SaveDeleteData("TF_EXP_SameTransIdCheckerAlert_IRM", p1, p2);
    }
    protected void fillDetails()
    {
        string docno = Request.QueryString["DocNo"].ToString();
        // SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
        string irmstatus = Request.QueryString["IRMStatus"].ToString();
        SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
        SqlParameter p2 = new SqlParameter("@docNo", docno);
        SqlParameter p3 = new SqlParameter("@IRMStatus", irmstatus);
       
        // SqlParameter p3 = new SqlParameter("@mode", "edit");
        //SqlParameter p4 = new SqlParameter("@search", "");
        DataTable dt = objData.getData("TF_EDPMS_INW_File_GetDetails_checker_new", p1,p2,p3);
        if (dt.Rows.Count > 0)
        {
            hdnDocDate.Value = "";

            //------------------Document Details---------------------//
            string STATUSI = dt.Rows[0]["Checker_Status"].ToString().Trim();
            string USERI = dt.Rows[0]["Checkby"].ToString().Trim();

            if (STATUSI == "I" && USERI != hdnUserName.Value)
            {
                string script = @"
                         <script type='text/javascript'>
                             var confirmation = confirm('Selected transaction is in progress! If you proceed any previous changes made may be lost. Do you still want to proceed?');
                             if (confirmation) {
                                ExecuteCSharpCodeLodg();
                             } else {
                                 // Redirect to a page
                                 window.location.href = 'EDPMS_INW_File_DataEntry_CheckerVIEW.aspx';
                            }
                         </script>
                     ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmationScript", script);
            }

            txtDocDate.Text = dt.Rows[0]["Document_Date"].ToString();
            hdnDocDate.Value = dt.Rows[0]["Document_Date"].ToString();
            hdnDocNo.Value = dt.Rows[0]["Document_No"].ToString();
            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            txtPurposeCode.Text = dt.Rows[0]["Purpose_Code"].ToString().Trim();
            ddlCurrency.Text = dt.Rows[0]["Currency"].ToString().Trim();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString().Trim();
            txtExchangeRate.Text = dt.Rows[0]["Exchange_Rate"].ToString().Trim();
            txtAmtInINR.Text = dt.Rows[0]["Amount_In_INR"].ToString().Trim();
            txtFIRCNo.Text = dt.Rows[0]["FIRC_No"].ToString().Trim();
            txtFIRCDate.Text = dt.Rows[0]["FIRC_Date"].ToString().Trim();
            txtADCode.Text = dt.Rows[0]["FIRC_AD_Code"].ToString().Trim();
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
            hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
            if (dt.Rows[0]["Checker_Status"].ToString() == "A")
            {
                ddlApproveReject.SelectedValue = "1";
                txtDocDate.Text = dt.Rows[0]["Document_Date"].ToString();
            }
            else
            {
                txtDocDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            //-----------------------Anand 15-06-2023------------------
            txtBankUniqueTransactionID.Text = dt.Rows[0]["BankUniqueTransactionID"].ToString().Trim();
            txtIFSCCode.Text = dt.Rows[0]["IFSCCode"].ToString().Trim();
            txtRemittanceADCode.Text = dt.Rows[0]["RemittanceADCode"].ToString().Trim();
            txtIECCode.Text = dt.Rows[0]["IECCode"].ToString().Trim();
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString().Trim();
            ddlModeOfPayment.SelectedValue = dt.Rows[0]["ModeofPayment"].ToString().Trim();// modified by Anand 11-08-2023
            // ddlFactoringflag.SelectedValue = dt.Rows[0]["Factoringflag"].ToString().Trim();
            //ddlForfeitingflag.SelectedValue = dt.Rows[0]["Forfeitingflag"].ToString().Trim();

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

            txtCustAcNo_TextChanged(null, null);
            txtPurposeCode_TextChanged(null, null);
            txtRemitterCountry_TextChanged(null, null);
            txtRemBankCountry_TextChanged(null, null);
            txtOverseasPartyID_TextChanged(null, null);
            //------------------------------End-----------------------
            txtRemitterBankAddress.ToolTip = txtRemitterBankAddress.Text;
            txtRemitterBank.ToolTip = txtRemitterBank.Text;
            txtRemitterAddress.ToolTip = txtRemitterAddress.Text;
            if (hdnDocDate.Value != txtDocDate.Text)
            {
                SqlParameter bCode = new SqlParameter("@bCode", txtBranchCode.Text.Trim());
                SqlParameter docNo = new SqlParameter("@docNo", hdnDocNo.Value);
                SqlParameter docDate = new SqlParameter("@docDate", txtDocDate.Text);
                SqlParameter user = new SqlParameter("@addedBy", Session["userName"].ToString());
                SqlParameter uploadingdate = new SqlParameter("@addedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                string _result = "";
                string _query = "TF_EDPMS_INW_File_DocDate_Update";
                TF_DATA objSaveLEI = new TF_DATA();
                _result = objSaveLEI.SaveDeleteData(_query, bCode, docNo, docDate, user, uploadingdate);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Document Date has been Updated !!')", true);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_INW_File_DataEntry_CheckerVIEW.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string script = "";
            string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
            string __docNo = _docno;
            string url = "../Reports/EXPReports/View_INWfileEntry.aspx?DocNo=" + __docNo;
            script = "window.open('" + url + "','_blank','height=600,width=800,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_INW_File_DataEntry_CheckerVIEW.aspx");
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
            //else if (ddlCurrency.SelectedItem.Text.Trim() == "Select")
            else if (ddlCurrency.Text.Trim() == "Select")
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

                LEIverify.Text = "";
                LEIverify.Text = "LEI checked.";
                LEIverify.ForeColor = System.Drawing.Color.LimeGreen;

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
            hdncustlei.Value = "";
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
                hdncustlei.Value = "";
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
            p1.Value = ddlCurrency.Text.ToString();
            string _query = "TF_EXP_GetLEI_RateCardDetails";
            DataTable dt = objData.getData(_query, p1, p2);
            string Exch_rate = "";
            if (dt.Rows.Count > 0)
            {
                Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
            }
            string txtExch = txtExchangeRate.Text.ToString();
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
                        if (ddlApproveReject.SelectedValue == "1")
                        {
                            LEIverify.Text = "LEI Verified.";
                            LEIverify.ForeColor = System.Drawing.Color.Green;
                            btnLEI.Enabled = false;
                        }
                        else
                        {
                            LEIverify.Text = "Please Verify LEI.";
                            LEIverify.ForeColor = System.Drawing.Color.Red;
                        }

                        LEIAmtCheck.ForeColor = System.Drawing.Color.Red;
                        LEIAmtCheck.Font.Size = 13;
                        hdnLeiFlag.Value = "Y";
                        hdnLeiSpecialFlag.Value = "Y";
                        hdnbillamtinr.Value = dtLimit.Rows[0]["billamtInr"].ToString().Trim();
                        
                    }
                    else if (dtLimit.Rows[0]["checkstaus"].ToString() == "less")
                    {
                        btnLEI.Visible = false;
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
                if (ddlApproveReject.SelectedValue == "1")
                {
                    LEIverify.Text = "LEI Verified.";
                    LEIverify.ForeColor = System.Drawing.Color.Green;
                }

                if (ddlApproveReject.SelectedValue == "1")
                {
                    LEIverify.Text = "";
                    LEIverify.Text = "LEI Verified.";
                    LEIverify.ForeColor = System.Drawing.Color.Green;
                    btnLEI.Enabled = false;
                }
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
    private void disabletext()
    {
        txtDocPref.Enabled = false;
        txtDocBRCode.Enabled = false;
        txtDocNo.Enabled = false;
        txtDocSrNo.Enabled = false;
        btnDocNoList.Enabled = false;
        txtDocDate.Enabled = false;
        btncalendar_DocDate.Enabled = false;
        txtvalueDate.Enabled = false;
        Button3.Enabled = false;
        txt_swiftcode.Enabled = false;
        btnHelpSwiftCode.Enabled = false;
        txtCustAcNo.Enabled = false;
        btnHelpCustAcNo.Enabled = false;
        txtPurposeCode.Enabled = false;
        Button1.Enabled = false;
        txtOverseasPartyID.Enabled = false;
        btnOverseasPartyList.Enabled = false;
        txtRemitterName.Enabled = false;
        txtRemitterCountry.Enabled = false;
        Button4.Enabled = false;
        txtRemitterAddress.Enabled = false;
        txtRemitterBank.Enabled = false;
        txtRemBankCountry.Enabled = false;
        Button5.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtpurposeofRemittance.Enabled = false;
        ddlCurrency.Enabled = false;
        txtAmount.Enabled = false;
        txtExchangeRate.Enabled = false;
        txtAmtInINR.Enabled = false;
        txtFIRCNo.Enabled = false;
        txtFIRCDate.Enabled = false;
        Button2.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtRemitterBankAddress.Enabled = false;
        txtADCode.Enabled = false;
        txtBankUniqueTransactionID.Enabled = false;
        txtIFSCCode.Enabled = false;
        txtRemittanceADCode.Enabled = false;
        txtIECCode.Enabled = false;
        txtPanNumber.Enabled = false;
        ddlModeOfPayment.Enabled = false;// Modified by Anand 11-08-2023
        //ddlFactoringflag.Enabled = false;
        //ddlForfeitingflag.Enabled = false;
        txtBankReferencenumber.Enabled = false;
        txtBankAccountNumber.Enabled = false;
        ddlIRMStatus.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string irmstatus = Request.QueryString["IRMStatus"].ToString();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", hdnDocNo.Value.Trim());
        SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
        SqlParameter p2 = new SqlParameter("@docNo", hdnDocNo.Value.Trim());
        SqlParameter p3 = new SqlParameter("@IRMStatus", irmstatus);
       
        // SqlParameter p3 = new SqlParameter("@mode", "edit");
        //SqlParameter p4 = new SqlParameter("@search", "");
        DataTable dt = objData.getData("TF_EDPMS_INW_File_GetDetails_checker_new", p1,p2,p3);
        string CheckerStatus = "";
        if (dt.Rows.Count > 0)
        {
            CheckerStatus = dt.Rows[0]["Checker_Status"].ToString();
        }

        if (CheckerStatus == "A" || CheckerStatus == "R")
        {
            string ST = "";
            if (CheckerStatus == "A")
            {
                ST = "Approved";
                ddlApproveReject.SelectedIndex = 1;
            }
            else
            {
                ST = "Rejected";
                ddlApproveReject.SelectedIndex = 2;
            }
            ddlApproveReject.Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('This transaction is already " + ST + ".');", true);
        }
        else
        {
            string AR = "";
            if (ddlApproveReject.SelectedIndex == 1)
            {
                AR = "A";
                string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
                SqlParameter LEIbCode = new SqlParameter("@bCode", txtBranchCode.Text.Trim());
                SqlParameter LEIdocNo = new SqlParameter("@docNo", _docno);
                string Trans_Status1 = "A";
                SqlParameter LEITrans_Status = new SqlParameter("@Trans_Status", Trans_Status1);
                SqlParameter LEI_Status = new SqlParameter("@Checker_status", "A");
                SqlParameter LEIuser = new SqlParameter("@user", Session["userName"].ToString());
                SqlParameter LEIuploadingdate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                string _resultLEI = "";
                string _queryLEI = "TF_EXP_Update_LEITransaction_Checker";
                TF_DATA objSaveLEI = new TF_DATA();
                _resultLEI = objSaveLEI.SaveDeleteData(_queryLEI, LEIbCode, LEIdocNo, LEITrans_Status, LEI_Status, LEIuser, LEIuploadingdate);
                getSrialNo();  // comment by Anand 29-07-2023
                JSON();
                // ddlApproveReject.SelectedValue = "Approve";
                //--------Anand 23-01-2024----------------------
                string _query1 = "";
                string _NewValues = "A";
                string _OldValues = "C";
                string _userName = Session["userName"].ToString();
                string _script = "Approve";
                string _result = "";
                string _Status = "";
                AuditChecker(_query1, _NewValues, _OldValues, _userName, _script, _result, _Status);
            }
            if (ddlApproveReject.SelectedIndex == 2)
            {
                AR = "R";
                //--------Anand 23-01-2024----------------------
                string _query1 = "";
                string _NewValues = "R";
                string _OldValues = "C";
                string _userName = Session["userName"].ToString();
                string _script = "Reject";
                string _result = "";
                string _Status = "";
                AuditChecker(_query1, _NewValues, _OldValues, _userName, _script, _result, _Status);
            }
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            string irmstring = "";
            if (ddlIRMStatus.SelectedValue == "Fresh")
            {
                irmstring = "F";
            }
            else if (ddlIRMStatus.SelectedValue == "Amended")
            {
                irmstring = "A";
            }
            else if (ddlIRMStatus.SelectedValue == "Cancelled")
            {
                irmstring = "C";
            }
            SqlParameter P_IRMStatus = new SqlParameter("@IRMStatus", irmstring);
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter P_branchcode = new SqlParameter("@bCode", txtBranchCode.Text.Trim());
            SqlParameter P_CheckedBy = new SqlParameter("@CheckBy", hdnUserName.Value);
            SqlParameter P_BankUniqueTransactionID = new SqlParameter("@BankUniqueTransactionID", txtBankUniqueTransactionID.Text);// comment by Anand 29-07-2023
            string Result = obj.SaveDeleteData("TF_EXP_ChekerApproveRejectINWRemitencedata", P_DocNo, P_Status, P_RejectReason, P_CheckedBy, P_BankUniqueTransactionID, P_branchcode, P_IRMStatus);
            Response.Redirect("EDPMS_INW_File_DataEntry_CheckerVIEW.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        }
    }
    protected void txtCustAcNo_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@customerACNo", txtCustAcNo.Text);
        //FillPanNO();
        SqlParameter p2 = new SqlParameter("@branch", txtBranchCode.Text);
        DataTable dt = objData.getData("TF_INW_GetCustDetails", p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtIECCode.Text = dt.Rows[0]["CUST_IE_CODE"].ToString();// Anand 19-06-2023
            //txtPanNumber.Text = dt.Rows[0]["CUST_PAN_NO"].ToString();
            hdnCustname.Value = lblCustName.Text;
        }
        if (lblCustName.Text == "")
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
        if (lblpurposeCode.Text == "")
        {
            txtPurposeCode.Text = "";
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
            //if (lblOverseasPartyDesc.Text.Length > 20)
            //{
            //    lblOverseasPartyDesc.ToolTip = lblOverseasPartyDesc.Text;
            //    lblOverseasPartyDesc.Text = lblOverseasPartyDesc.Text.Substring(0, 20) + "...";
            //}
        }
        else
        {
            txtOverseasPartyID.Text = "";
            lblOverseasPartyDesc.Text = "";
        }

    }
    protected void txtOverseasPartyID_TextChanged(object sender, EventArgs e)
    {
        fillOverseasPartyDescription();
        
    }

  public  void JSON()
    {
        string date = DateTime.Now.ToString("ddMMyyyy");
        string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
        string _docno1 = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
        string uniqueTxId = txtBankUniqueTransactionID.Text;
        string[] irmList ={txtBankReferencenumber.Text,txtDocDate.Text,_docno1,ddlIRMStatus.SelectedValue,txtIFSCCode.Text,txtRemittanceADCode.Text,
                       txtvalueDate.Text,ddlCurrency.Text,txtAmount.Text,txtAmtInINR.Text,txtIECCode.Text,txtPanNumber.Text,txtRemitterName.Text,
                       txtRemitterCountry.Text,txtPurposeCode.Text,txtBankAccountNumber.Text};

         _docno = _docno.Replace("/", "_");

         string filePath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/IRMJSON/" + date);
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        string IRMStatus = "";
        if (ddlIRMStatus.Text.Trim() == "Fresh")
        {
            IRMStatus = "F";
        }
        else if (ddlIRMStatus.Text.Trim() == "Amended")
        {
            IRMStatus = "A";
        }
        else if (ddlIRMStatus.Text.Trim() == "Cancelled")
        {
            IRMStatus = "C";
        }
     
        var IRMData = new
        {
            uniqueTxId = uniqueTxId,
            irmList=new List<object>{new{bankRefNumber=txtBankReferencenumber.Text.Trim(),
                        irmIssueDate =txtDocDate.Text.Trim().Replace("/",""),
                        irmNumber = _docno1,
                        irmStatus = IRMStatus,
                        ifscCode = txtIFSCCode.Text.Trim(),
                        remittanceAdCode =txtRemittanceADCode.Text.Trim(),
                        remittanceDate =txtvalueDate.Text.Trim().Replace("/",""),
                        remittanceFCC =ddlCurrency.Text.Trim(),
                        remittanceFCAmount=Convert.ToDecimal(txtAmount.Text.Trim()),// Anand 12-09-2023(convert the decimal)
                        inrCreditAmount=Convert.ToDecimal(txtAmtInINR.Text.Trim()),// Anand 12-09-2023(convert the decimal)
                        iecCode = txtIECCode.Text.Trim(),
                        panNumber = txtPanNumber.Text.Trim(),
                        remitterName =txtRemitterName.Text.Trim(),
                        remitterCountry =txtRemitterCountry.Text.Trim(),
                        purposeOfRemittance =txtPurposeCode.Text.Trim(),
                        bankAccountNo =txtBankAccountNumber.Text.Trim()
                 }
            }
        };         
          string _filePath = filePath + "/" + txtBankUniqueTransactionID.Text + "_" + IRMStatus + ".Json";
          string json = JsonConvert.SerializeObject(IRMData, Formatting.Indented).Trim().Replace("\": \"", "\":\"");
          //string json = JsonConvert.SerializeObject(IRMData, Formatting.Indented).Trim();
      
          System.IO.File.WriteAllText((_filePath),json);
          ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('JSON File is Created.');", true);     

    }

  protected void getSrialNo()
  {
      //string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
      TF_DATA objData = new TF_DATA();
      String _query = "TF_IRM_GetBankUniqueTransactionID";

      SqlParameter p1 = new SqlParameter("@BranchCode", SqlDbType.VarChar);
      p1.Value = txtBranchCode.Text.Trim();
      SqlParameter p2 = new SqlParameter("@Doc_date", SqlDbType.VarChar);
      p2.Value = txtDocDate.Text.Trim().Replace("/", "");

      DataTable dt = objData.getData(_query, p1, p2);
      if (dt.Rows.Count > 0)
      {
          string newsrno = dt.Rows[0]["BankUniqueTransactionID"].ToString().Trim();
          string s2 = newsrno.Replace("/", "").Trim();
          txtBankUniqueTransactionID.Text = "" + s2.ToString();
      }
  }

  public void AuditChecker(string _query1, string _NewValues, string _OldValues, string _userName, string _script, string _result, string _Status)
  {
      string _branchCode = Request.QueryString["BranchCode"].Trim();
      string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
      TF_DATA objSave = new TF_DATA();
      _query1 = "TF_Data_AuditTrailInward";
      _OldValues = "Checkar Status :" + _OldValues;
      _NewValues = "Checkar Status :" + _NewValues;


      SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
      Branch.Value = hdnBranchCode.Value;
      SqlParameter Mod = new SqlParameter("@Module", SqlDbType.VarChar);
      Mod.Value = "EXP";
      SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
      oldvalues.Value = _OldValues;
      SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
      newvalues.Value = _NewValues;
      SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
      Acno.Value = txtCustAcNo.Text;

      SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
      DocumentNo.Value = _docno;
      SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
      DocumnetDate.Value = txtDocDate.Text.Trim();
      SqlParameter Mode = new SqlParameter("@Mode", SqlDbType.VarChar);
      Mode.Value = _script;
      SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
      user.Value = _userName;
      string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
      SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
      moddate.Value = _moddate;
      string _menu = "Inward Remittance Data Entry";
      SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
      menu.Value = _menu;
      string at = objSave.SaveDeleteData(_query1, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu);
  }
}




     
