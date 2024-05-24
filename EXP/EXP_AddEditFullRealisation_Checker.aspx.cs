using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
using System.Web.Services;

public partial class EXP_EXP_AddEditFullRealisation_Checker : System.Web.UI.Page
{
    string branchcode = "";
    string branchcode1 = "";
    string DocPrFx = "";
    string SrNo = "";
    string year = "";
    static string libor = "";
    string doc_type = "";
    string loan = "";
    int Leicount = 5;
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
                hdnUserName.Value = Session["userName"].ToString();
                disable();
                fillddlTTCurrency1();
                fillddlTTCurrency2();
                fillddlTTCurrency3();
                fillddlTTCurrency4();
                fillddlTTCurrency5();
                fillddlTTCurrency6();
                fillddlTTCurrency7();
                fillddlTTCurrency8();
                fillddlTTCurrency9();
                fillddlTTCurrency10();
                fillddlTTCurrency11();
                fillddlTTCurrency12();
                fillddlTTCurrency13();
                fillddlTTCurrency14();
                fillddlTTCurrency15();
                fillddlTTRealisedCurr1();
                fillddlTTRealisedCurr2();
                fillddlTTRealisedCurr3();
                fillddlTTRealisedCurr4();
                fillddlTTRealisedCurr5();
                fillddlTTRealisedCurr6();
                fillddlTTRealisedCurr7();
                fillddlTTRealisedCurr8();
                fillddlTTRealisedCurr9();
                fillddlTTRealisedCurr10();
                fillddlTTRealisedCurr11();
                fillddlTTRealisedCurr12();
                fillddlTTRealisedCurr13();
                fillddlTTRealisedCurr14();
                fillddlTTRealisedCurr15();
                TabContainerMain.ActiveTab = tbDocumentDetails;
                chkManualGR.Attributes.Add("onclick", "return false;");
                if (Request.QueryString["mode"].Trim() != "add")
                {
                    txtDocNo.Text = Request.QueryString["DocNo"].ToString();
                    branchcode = Request.QueryString["BranchCode"].ToString();
                    branchcode1 = Request.QueryString["BranchCode"].ToString();
                    //----------------Anand 02-09-2023--------------
                    if (branchcode == "792")
                    {
                        tbDocumentGOAccChange.Enabled = false;
                        btnNGO_Next.Visible = false;
                    }
                    else
                    {
                        tbDocumentGOAccChange.Enabled = true;
                        PnlApproveIdGO2.Visible = false;
                    }
                    //-----------------END------------------------
                    hdnBranchName.Value = Request.QueryString["BranchName"].ToString();
                    DocPrFx = Request.QueryString["DocPrFx"].ToString();
                    hdnBranchCode.Value = branchcode;
                    if (DocPrFx == "BLA")
                        rbtbla.Checked = rbtbla.Visible = true;
                    if (DocPrFx == "BLU")
                        rbtblu.Checked = rbtblu.Visible = true;
                    if (DocPrFx == "BBA")
                        rbtbba.Checked = rbtbba.Visible = true;
                    if (DocPrFx == "BBU")
                        rbtbbu.Checked = rbtbbu.Visible = true;
                    if (DocPrFx == "BCA")
                        rbtbca.Checked = rbtbca.Visible = true;
                    if (DocPrFx == "BCU")
                        rbtbcu.Checked = rbtbcu.Visible = true;
                    if (DocPrFx == "IBD")
                        rbtIBD.Checked = rbtIBD.Visible = true;
                    if (DocPrFx == "LBC")
                        rbtLBC.Checked = rbtLBC.Visible = true;
                    if (DocPrFx == "BEB")
                        rbtBEB.Checked = rbtBEB.Visible = true;
                    txtDocPrFx.Value = DocPrFx;
                    if (DocPrFx == "BCA" || DocPrFx == "BCU")
                    {
                        pnldocTypeInterest.Visible = false;
                    }
                    else
                    {
                        pnldocTypeInterest.Visible = true;
                    }
                    hdnmode.Value = Request.QueryString["mode"].ToString();
                    SrNo = Request.QueryString["SrNo"].ToString();
                    txtSrNo.Text = SrNo;
                    fillDetails(txtDocNo.Text, SrNo);
                    btnDocNo.Visible = false;
                    txtDateRealised.Focus();
                    Fillshippingbill();
                }
                else
                {
                    DocPrFx = Request.QueryString["DocPrFx"].ToString();
                    branchcode = Request.QueryString["BranchCode"].ToString();
                    //----------------Anand 02-09-2023--------------
                    if (branchcode == "792")
                    {
                        tbDocumentGOAccChange.Enabled = false;
                        btnNGO_Next.Visible = false;
                    }
                    else
                    {
                        tbDocumentGOAccChange.Enabled = true;
                        PnlApproveIdGO2.Visible = false;
                    }
                    //-----------------END------------------------
                    hdnBranchName.Value = Request.QueryString["BranchName"].ToString();
                    year = Request.QueryString["year"].ToString();
                    hdnmode.Value = Request.QueryString["mode"].ToString();
                    if (DocPrFx == "BLA")
                        rbtbla.Checked = rbtbla.Visible = true;
                    if (DocPrFx == "BLU")
                        rbtblu.Checked = rbtblu.Visible = true;
                    if (DocPrFx == "BBA")
                        rbtbba.Checked = rbtbba.Visible = true;
                    if (DocPrFx == "BBU")
                        rbtbbu.Checked = rbtbbu.Visible = true;
                    if (DocPrFx == "BCA")
                        rbtbca.Checked = rbtbca.Visible = true;
                    if (DocPrFx == "BCU")
                        rbtbcu.Checked = rbtbcu.Visible = true;
                    if (DocPrFx == "IBD")
                        rbtIBD.Checked = rbtIBD.Visible = true;
                    if (DocPrFx == "LBC")
                        rbtLBC.Checked = rbtLBC.Visible = true;
                    if (DocPrFx == "BEB")
                        rbtBEB.Checked = rbtBEB.Visible = true;
                    txtDocPrFx.Value = DocPrFx;

                    if (DocPrFx == "BCA" || DocPrFx == "BCU")
                    {
                        pnldocTypeInterest.Visible = false;
                    }
                    else
                    {
                        pnldocTypeInterest.Visible = true;
                    }
                    hdnBranchCode.Value = branchcode;
                    hdnYear.Value = year;
                    txtDateRealised.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    if (DocPrFx == "B" || DocPrFx == "S")
                    {
                        txtExchangeRate.Text = "1";
                        rdbTransType.SelectedValue = "INR";
                    }
                    btnDocNo.Visible = true;
                    txtDateRealised.Focus();
                }
                btnDocNo.Attributes.Add("onclick", "return DocHelp();");
                txtDocNo.Attributes.Add("onkeydown", "return OpenDocList(event);");
                txtDocNo.Attributes.Add("onblur", "return chkdocno();");
                txtExchangeRate.Attributes.Add("onblur", "return checkExchangeRate();");
                txtRelCrossCurRate.Attributes.Add("onblur", "return checkExchangeRate();");
                txtExchangeRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtRealised.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtRealised.Attributes.Add("onblur", "return amtrealised();");
                txtEEFCAmt.Attributes.Add("onblur", "return calrealisedAmtinINR();");
                rdbFull.Attributes.Add("onblur", "return calrealisedAmtinINR();");
                rdbPart.Attributes.Add("onblur", "return calrealisedAmtinINR();");
                txtCrossCurrRate.Attributes.Add("onblur", "return calcrosscurrTotal();");
                txtOtherBank.Attributes.Add("onblur", "return calotheramtininr();");
                //txtCommission.Attributes.Add("onblur", "return calTax();");
                //txtCourier.Attributes.Add("onblur", "return calTax();");
               // txtSwift.Attributes.Add("onblur", "return calTax();");
                //txtBankCertificate.Attributes.Add("onblur", "return calTax();");
                btnEEFCCurrency.Attributes.Add("onclick", "return curhelp();");
                btn_recurrhelp.Attributes.Add("onclick", "return curhelp1();");
                txtEEFCAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCrossCurrRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterest.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtOtherBank.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCollectionAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSwift.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCourier.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtBankCertificate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCommission.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtInterestRate2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDateRealised.Attributes.Add("onblur", "return checkSysDate();");
                txt_relamount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_relamount.Attributes.Add("onblur", "return chkamt();");
                txtRelCrossCurRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRelCrossCurRate.Attributes.Add("onblur", "return calfamtrealised();");
                txtPartConAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtPartConAmt.Attributes.Add("onblur", "return calrealisedAmtinINR();");
                btnOtrCrossCur.Attributes.Add("onclick", "return curhelp3();");
                txtConCurRate.Attributes.Add("onblur", "return calcrosscurrTotal1();");
                txtConCurRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtprofitamt.Attributes.Add("onblur", "return calTax();");
                txtprofitamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //btnOverseasBankList.Attributes.Add("onclick", "return OpenOverseasBankList('mouseClick');");
                //txtNoofDays2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtRealisedinINR.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSBcesssamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_kkcessamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtsttamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFxDlsCommission.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_fbkcharges.Attributes.Add("onblur", "return fbkcal();");
                if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                {
                    if (branchcode == "792")
                    {
                        ddlApproveReject.Enabled = false;
                        lblChecker_Remark.Visible = false;
                    }
                    else
                    {
                        ddlApproveReject1.Enabled = false;
                        lblChecker_Remark.Visible = false;
                    }
                }
                if (Request.QueryString["Status"].ToString() == "Send To Checker")
                {
                    //ddlApproveReject.Enabled = false;
                    TF_DATA obj = new TF_DATA();
                    SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@Checker", hdnUserName.Value);
                    SqlParameter p3 = new SqlParameter("@SrNo", txtSrNo.Text.Trim());
                    string Result = obj.SaveDeleteData("TF_EXP_Realisation_SameTransIdCheckerAlert", p1, p2, p3);
                }
            }
            if (branchcode == "792")
            {
                ddlApproveReject.Attributes.Add("onchange", "return DialogAlertRealisation();");
            }
            else
            {
                ddlApproveReject1.Attributes.Add("onchange", "return DialogAlertRealisation();");
            }
        }
    }
    protected void fillTaxRates()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetTaxRates_INW";

        ddlServicetax.Items.Clear();

        DataTable dt = objData.getData(_query);

        if (dt.Rows.Count > 0)
        {
            ddlServicetax.DataSource = dt.DefaultView;
            ddlServicetax.DataTextField = "SERVICE_TAX";
            ddlServicetax.DataValueField = "SERVICE_TAX";
            ddlServicetax.DataBind();

            hdnServiceTax.Value = dt.Rows[0]["SERVICE_TAX"].ToString();
            hdnEDU_CESS.Value = dt.Rows[0]["EDU_CESS"].ToString();
            hdnSEC_EDU_CESS.Value = dt.Rows[0]["SEC_EDU_CESS"].ToString();


            txtsbcess.Text = dt.Rows[0]["SBCess"].ToString();
            txt_kkcessper.Text = dt.Rows[0]["KKCess"].ToString();

            txtsbcess.Enabled = false;
            txt_kkcessper.Enabled = false;



        }
    }

    [WebMethod]
    public static void ExecuteCSharpCode(string docNo, string checker, string srNo)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@DocNo", docNo);
        SqlParameter p2 = new SqlParameter("@Checker", checker);
        SqlParameter p3 = new SqlParameter("@SrNo", srNo);
        string Result = obj.SaveDeleteData("TF_EXP_Realisation_SameTransIdCheckerAlert", p1, p2, p3);
    }


    protected void fillDetails(string docno, string SrNo)
    {
        //libor1.Value = libor;
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = docno;
        SqlParameter p2 = new SqlParameter("@srno", SqlDbType.VarChar);
        p2.Value = SrNo;
        string query = "TF_GetRealisedEntryDetailsNew_Checker";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);

        if (dt.Rows.Count > 0)
        {
            string STATUSI = dt.Rows[0]["Checker_Status"].ToString().Trim();
            string USERI = dt.Rows[0]["Checkby"].ToString().Trim();
            if (STATUSI == "I" && USERI != hdnUserName.Value)
            {
                string script = @"
                         <script type='text/javascript'>
                             var confirmation = confirm('Selected transaction is in progress! If you proceed any previous changes made may be lost. Do you still want to proceed?');
                             if (confirmation) {
                                executeCSharpCode();
                             } else {
                                 // Redirect to a page
                                 window.location.href = 'EXP_ViewRealisationEntry_Checker.aspx';
                             }
                         </script>
                     ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmationScript", script);
            }
            if (dt.Rows[0]["Checker_Status"].ToString() == "A")
            {
                if (branchcode == "792")
                {
                    ddlApproveReject.SelectedValue = "1";
                }
                else
                {
                    ddlApproveReject1.SelectedValue = "1";
                }
            }
            hdnDocNo.Value = dt.Rows[0]["Document_No"].ToString();
            hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
            txtForeignORLocal.Text = dt.Rows[0]["ForeignORLocal"].ToString().Trim();

            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            fillCustomerIdDescription();

            txtDateReceived.Text = dt.Rows[0]["Date_Received"].ToString();
            txtDueDate.Text = dt.Rows[0]["Due_Date"].ToString();
            txtProcessingDate.Text = dt.Rows[0]["ProcessingDate"].ToString();

            //////////////////////////added by Shailesh  31/07/2023 

            txtOverseasBank.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString();
            txtRemitterName.Text = dt.Rows[0]["remitterName"].ToString();
            txtRemitterCountry.Text = dt.Rows[0]["remitterCountry"].ToString();
            txtRemitterAddress.Text = dt.Rows[0]["Remitter_Address"].ToString();
            fillIssuingBankDescription();
            txtOverseasParty.Text = dt.Rows[0]["Overseas_Party_Code"].ToString();
            txtSwiftCode.Text = dt.Rows[0]["SwiftCode"].ToString();
            txtRemitterBank.Text = dt.Rows[0]["Remitter_Bank"].ToString();
            txtRemitterBankAddress.Text = dt.Rows[0]["Remitter_Bank_Address"].ToString();
            txtRemBankCountry.Text = dt.Rows[0]["Remitter_Bank_Country"].ToString();
            fillOverseasPartyDescription();
            txtconsigneePartyID.Text = dt.Rows[0]["consigneePartyID"].ToString();
            fillConsigneePartyDescription();
            txtRemitterCountry_TextChanged(null, null);
            txtRemBankCountry_TextChanged(null, null);

            txtpurposeofRemittance.Text = dt.Rows[0]["Purpose_Of_Remittance"].ToString();

            if (dt.Rows[0]["OutstandingAmt"].ToString() != "")
                txtOutstandingAmt.Text = Convert.ToDecimal(dt.Rows[0]["OutstandingAmt"].ToString()).ToString("0.00");
            else
                txtOutstandingAmt.Text = dt.Rows[0]["OutstandingAmt"].ToString();

            if (dt.Rows[0]["InstructedAmt"].ToString() != "")
                txtInstructedAmt.Text = Convert.ToDecimal(dt.Rows[0]["InstructedAmt"].ToString()).ToString("0.00");
            else
                txtInstructedAmt.Text = dt.Rows[0]["InstructedAmt"].ToString();

            //txtDateNegotiated.Text = dt.Rows[0]["Date_Negotiated"].ToString();
            //txtDateDelinked.Text = dt.Rows[0]["Delinked_Date"].ToString();
            //txtAcceptedDueDate.Text = dt.Rows[0]["Accepted_Due_Date"].ToString();
            string billtype = dt.Rows[0]["Bill_Type"].ToString();
            //if (billtype == "S")
            //    txtBillType.Text = "Sight Bill";
            //else
            //    txtBillType.Text = "Usance Bill";
            txtOtherCurrency.Text = dt.Rows[0]["Other_Currency_For_INR"].ToString();
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            string curr = txtCurrency.Text;
            txt_relcur.Text = dt.Rows[0]["RealCur"].ToString();

            txtInvoiceNo.Text = dt.Rows[0]["Invoice_No"].ToString();
            if (dt.Rows[0]["Bill_Amount"].ToString() != "")
                txtNegotiatedAmt.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount"].ToString()).ToString("0.00");
            else
                txtNegotiatedAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            if (dt.Rows[0]["Bill_Amount_In_Rs"].ToString() != "")
                txtNegotiatedAmtinINR.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount_In_Rs"].ToString()).ToString("0.00");
            else
                txtNegotiatedAmtinINR.Text = dt.Rows[0]["Bill_Amount_In_Rs"].ToString();
            if (dt.Rows[0]["ActBillAmt"].ToString() != "")
                txtBillAmt.Text = Convert.ToDecimal(dt.Rows[0]["ActBillAmt"].ToString()).ToString("0.00");
            else
                txtBillAmt.Text = dt.Rows[0]["ActBillAmt"].ToString();
            if (dt.Rows[0]["ActBillAmt_InRs"].ToString() != "")
                txtBillAmtinINR.Text = Convert.ToDecimal(dt.Rows[0]["ActBillAmt_InRs"].ToString()).ToString("0.00");
            else
                txtBillAmtinINR.Text = dt.Rows[0]["ActBillAmt_InRs"].ToString();
            if (dt.Rows[0]["CrossRealAmt"].ToString() != "")
                txt_relamount.Text = Convert.ToDecimal(dt.Rows[0]["CrossRealAmt"].ToString()).ToString("0.00");
            else
                txt_relamount.Text = dt.Rows[0]["CrossRealAmt"].ToString();
            if (dt.Rows[0]["RelCrossCurRate"].ToString() != "")
                txtRelCrossCurRate.Text = Convert.ToDecimal(dt.Rows[0]["RelCrossCurRate"].ToString()).ToString("0.00000");
            else
                txtRelCrossCurRate.Text = dt.Rows[0]["RelCrossCurRate"].ToString();


            txtDateRealised.Text = dt.Rows[0]["Realised_Date"].ToString();
            //if (txtDocPrFx.Value != "B" && txtDocPrFx.Value != "S")
            //    getExchangeRate(curr);
            txtValueDate.Text = dt.Rows[0]["Value_Date"].ToString();
            loan = dt.Rows[0]["Loan"].ToString();
            loan1.Value = loan;
            if (loan == "Y")
            {
                chkLoanAdvanced.Checked = true;
                lblLoan.Text = "Loan Advanced";
            }
            else
            {
                chkLoanAdvanced.Checked = false;
                lblLoan.Text = "No Loan Advanced";
            }
            if (chkLoanAdvanced.Checked == true && txtDateDelinked.Text == "")
            {
                if (txtValueDate.Text != "" && txtDueDate.Text != "")
                {
                    string query1 = "getdatediff";
                    SqlParameter p11 = new SqlParameter("@dt2", SqlDbType.VarChar);
                    p11.Value = txtValueDate.Text;
                    SqlParameter p12 = new SqlParameter("@dt1", SqlDbType.VarChar);
                    p12.Value = txtDueDate.Text;
                    DataTable dt1 = objData.getData(query1, p11, p12);
                    //dt1 = Convert.ToDateTime(txtValueDate.Text).Date;
                    //dt2 = Convert.ToDateTime(txtDueDate.Text).Date;
                    int days = 0;
                    if (dt1.Rows.Count > 0)
                        days = Convert.ToInt32(dt1.Rows[0]["Diff"].ToString());
                    if (days < 0)
                    {
                        lblRefund.Text = "Overdue Interest";
                        if (dt.Rows[0]["Realised_Overdue_Interest"].ToString() != "")
                            txtInterest.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Overdue_Interest"].ToString()).ToString("0.00");
                        else
                            txtInterest.Text = dt.Rows[0]["Realised_Overdue_Interest"].ToString();
                        if (dt.Rows[0]["Realised_Overdue_Interest_In_Rs"].ToString() != "")
                            txtInterestinINR.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Overdue_Interest_In_Rs"].ToString()).ToString("0.00");
                        else
                            txtInterestinINR.Text = dt.Rows[0]["Realised_Overdue_Interest_In_Rs"].ToString();
                    }
                    if (days > 0)
                    {
                        lblRefund.Text = "Refund Interest";
                        if (dt.Rows[0]["Realised_Refund_Interest"].ToString() != "")
                            txtInterest.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Refund_Interest"].ToString()).ToString("0.00");
                        else
                            txtInterest.Text = dt.Rows[0]["Realised_Refund_Interest"].ToString();
                        if (dt.Rows[0]["Realised_Refund_Interest_In_Rs"].ToString() != "")
                            txtInterestinINR.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Refund_Interest_In_Rs"].ToString()).ToString("0.00");
                        else
                            txtInterestinINR.Text = dt.Rows[0]["Realised_Refund_Interest_In_Rs"].ToString();

                    }
                    if (days == 0)
                        lblRefund.Text = "";

                    intdays.Value = Convert.ToString(days);
                }
            }



            if (dt.Rows[0]["Realised_Exchange_Rate"].ToString() != "")
                txtExchangeRate.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Exchange_Rate"].ToString()).ToString("0.0000000000");
            else
                txtExchangeRate.Text = dt.Rows[0]["Realised_Exchange_Rate"].ToString();
            if (dt.Rows[0]["Realised_Amount"].ToString() != "")
                txtAmtRealised.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Amount"].ToString()).ToString("0.00");
            else
                txtAmtRealised.Text = dt.Rows[0]["Realised_Amount"].ToString();
            if (dt.Rows[0]["Realised_Amount_In_Rs"].ToString() != "")
                txtAmtRealisedinINR.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Amount_In_Rs"].ToString()).ToString("0.00");
            else
                txtAmtRealisedinINR.Text = dt.Rows[0]["Realised_Amount_In_Rs"].ToString();

            if (dt.Rows[0]["LeiINRAmt"].ToString() != "")
                txtLeiInrAmt.Text = Convert.ToDecimal(dt.Rows[0]["LeiINRAmt"].ToString()).ToString("0.00");
            else
                txtLeiInrAmt.Text = dt.Rows[0]["LeiINRAmt"].ToString();

            if (dt.Rows[0]["Realised_Telex_Charges"].ToString() != "")
                txtSwift.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Telex_Charges"].ToString()).ToString("0.00");
            else
                txtSwift.Text = dt.Rows[0]["Realised_Telex_Charges"].ToString();
            if (dt.Rows[0]["Realised_Bank_Cert"].ToString() != "")
                txtBankCertificate.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Bank_Cert"].ToString()).ToString("0.00");
            else
                txtBankCertificate.Text = dt.Rows[0]["Realised_Bank_Cert"].ToString();
            if (dt.Rows[0]["Realised_Courier_Charges"].ToString() != "")
                txtCourier.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Courier_Charges"].ToString()).ToString("0.00");
            else
                txtCourier.Text = dt.Rows[0]["Realised_Courier_Charges"].ToString();
            if (dt.Rows[0]["Realised_OBC"].ToString() != "")
                txtOtherBank.Text = Convert.ToDecimal(dt.Rows[0]["Realised_OBC"].ToString()).ToString("0.00");
            else
                txtOtherBank.Text = dt.Rows[0]["Realised_OBC"].ToString();
            if (dt.Rows[0]["Realised_OBC_In_Rs"].ToString() != "")
                txtOtherBankinINR.Text = Convert.ToDecimal(dt.Rows[0]["Realised_OBC_In_Rs"].ToString()).ToString("0.00");
            else
                txtOtherBankinINR.Text = dt.Rows[0]["Realised_OBC_In_Rs"].ToString();
            //txtInterestRate1.Text = dt.Rows[0]["Realised_Interest_Rate_1"].ToString();
            //txtNoofDays1.Text = dt.Rows[0]["Realised_For_Days_1"].ToString();
            txtInterestRate2.Text = dt.Rows[0]["Realised_Interest_Rate_2"].ToString();
            txtNoofDays2.Text = dt.Rows[0]["Realised_For_Days_2"].ToString();
            txtCommissionID.Text = dt.Rows[0]["CommissionID"].ToString();
            if (dt.Rows[0]["Realised_Commission"].ToString() != "")
                txtCommission.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Commission"].ToString()).ToString("0.00");
            else
                txtCommission.Text = dt.Rows[0]["Realised_Commission"].ToString();
            if (dt.Rows[0]["RealAmtEEFC_FC"].ToString() != "")
                txtPartConAmt.Text = Convert.ToDecimal(dt.Rows[0]["RealAmtEEFC_FC"].ToString()).ToString("0.00");
            else
                txtPartConAmt.Text = dt.Rows[0]["RealAmtEEFC_FC"].ToString();

            if (dt.Rows[0]["EEFCAmt"].ToString() != "")
                txtEEFCAmt.Text = Convert.ToDecimal(dt.Rows[0]["EEFCAmt"].ToString()).ToString("0.00");
            else
                txtEEFCAmt.Text = dt.Rows[0]["EEFCAmt"].ToString();

            if (dt.Rows[0]["Realised_Current_Account_FC_IN_INR"].ToString() != "")
                txtEEFCinINR.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Current_Account_FC_IN_INR"].ToString()).ToString("0.00");
            else
                txtEEFCinINR.Text = dt.Rows[0]["Realised_Current_Account_FC_IN_INR"].ToString();
            if (dt.Rows[0]["Realised_Current_Account_Amount_In_Rs"].ToString() != "")
                txtNetAmt.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Current_Account_Amount_In_Rs"].ToString()).ToString("0.00");
            else
                txtNetAmt.Text = dt.Rows[0]["Realised_Current_Account_Amount_In_Rs"].ToString();
            txtRemark.Text = dt.Rows[0]["Realised_Remarks"].ToString();
            string payind = dt.Rows[0]["Realised_Payment_Indication"].ToString();
            if (payind == "F")
                rdbFull.Checked = true;
            else
                rdbPart.Checked = true;
            if (dt.Rows[0]["BalAmtRealised"].ToString() != "")
                txtCollectionAmt.Text = Convert.ToDecimal(dt.Rows[0]["BalAmtRealised"].ToString()).ToString("0.00");
            else
                txtCollectionAmt.Text = dt.Rows[0]["BalAmtRealised"].ToString();
            if (dt.Rows[0]["BalAmtRealisedINR"].ToString() != "")
                txtCollectionAmtinINR.Text = Convert.ToDecimal(dt.Rows[0]["BalAmtRealisedINR"].ToString()).ToString("0.00");
            else
                txtCollectionAmtinINR.Text = dt.Rows[0]["BalAmtRealisedINR"].ToString();
            //ddlServicetax.Text = dt.Rows[0]["Stax"].ToString();
            if (dt.Rows[0]["Stax"].ToString() != "0")
            {
                chkStax.Checked = true;
                chkStax_CheckedChanged(this, null);
            }
            if (dt.Rows[0]["StaxAmt"].ToString() != "")
                txtServiceTax.Text = Convert.ToDecimal(dt.Rows[0]["StaxAmt"].ToString()).ToString("0.00");
            else
                txtServiceTax.Text = dt.Rows[0]["StaxAmt"].ToString();

            if (dt.Rows[0]["SBcessamt"].ToString() != "")
                txtSBcesssamt.Text = Convert.ToDecimal(dt.Rows[0]["SBcessamt"].ToString()).ToString("0.00");
            else
                txtSBcesssamt.Text = dt.Rows[0]["SBcessamt"].ToString();

            if (dt.Rows[0]["KKcessamt"].ToString() != "")
                txt_kkcessamt.Text = Convert.ToDecimal(dt.Rows[0]["KKcessamt"].ToString()).ToString("0.00");
            else
                txt_kkcessamt.Text = dt.Rows[0]["KKcessamt"].ToString();

            if (dt.Rows[0]["STaxTotalamt"].ToString() != "")
                txtsttamt.Text = Convert.ToDecimal(dt.Rows[0]["STaxTotalamt"].ToString()).ToString("0.00");
            else
                txtsttamt.Text = dt.Rows[0]["STaxTotalamt"].ToString();
            //txtTTRefNo.Text = dt.Rows[0]["TTREFNO"].ToString();

            txtprofitper.Text = dt.Rows[0]["ProfitLieoId"].ToString();

            if (dt.Rows[0]["ProfitLieoAmt"].ToString() != "")
                txtprofitamt.Text = Convert.ToDecimal(dt.Rows[0]["ProfitLieoAmt"].ToString()).ToString("0.00");
            else
                txtprofitamt.Text = dt.Rows[0]["ProfitLieoAmt"].ToString();

            txtFxDlsCommission.Text = dt.Rows[0]["REALISED_FXDLS"].ToString();

            if (txtFxDlsCommission.Text != "" && txtFxDlsCommission.Text != "0")
                chkFxDls.Checked = true;

            //Fxdls
            if (dt.Rows[0]["SBcessFxDls"].ToString() != "")
            {
                txtsbfx.Text = Convert.ToDecimal(dt.Rows[0]["SBcessFxDls"]).ToString("0.00");
            }
            else
            {
                txtsbfx.Text = dt.Rows[0]["SBcessFxDls"].ToString();
            }

            if (dt.Rows[0]["KKcessFxDls"].ToString() != "")
            {
                txt_kkcessonfx.Text = Convert.ToDecimal(dt.Rows[0]["KKcessFxDls"]).ToString("0.00");
            }

            else
            {
                txt_kkcessonfx.Text = (dt.Rows[0]["KKcessFxDls"]).ToString();
            }

            if (dt.Rows[0]["TotalSBcessFxDls"].ToString() != "")
            {
                txttotcessfx.Text = Convert.ToDecimal(dt.Rows[0]["TotalSBcessFxDls"]).ToString("0.00");
            }
            else
            {
                txttotcessfx.Text = dt.Rows[0]["TotalSBcessFxDls"].ToString();
            }



            string bank = dt.Rows[0]["BankLine"].ToString();
            if (bank == "Y")
            {
                chkBank.Checked = true;
            }
            else
            {
                chkBank.Checked = false;
            }
            string trantype = dt.Rows[0]["Trans_Type"].ToString();
            if (trantype == "FC")
            {
                rdbTransType.SelectedIndex = 0;
                tran_type.Value = trantype;
            }
            if (trantype == "INR")
            {
                rdbTransType.SelectedIndex = 1;
                tran_type.Value = trantype;
            }
            if (trantype == "CC")
            {
                rdbTransType.SelectedIndex = 2;
                tran_type.Value = trantype;
            }
            if (trantype == "PC")
            {
                rdbTransType.SelectedIndex = 3;
                tran_type.Value = trantype;
            }
            string trantype2 = dt.Rows[0]["Trans_Type2"].ToString();
            if (trantype2 == "PEEFC")
            {
                rdbTransType2.SelectedIndex = 0;
                tran_type2.Value = trantype2;
            }
            if (trantype2 == "FEEFC")
            {
                rdbTransType2.SelectedIndex = 1;
                tran_type2.Value = trantype2;
            }
            txtConCrossCur.Text = dt.Rows[0]["RealAmtEEFC_Cur"].ToString();
            txtEEFCCurrency.Text = dt.Rows[0]["EEFC_Currency"].ToString();
            if (dt.Rows[0]["RealAmtEEFC_Rate"].ToString() != "")
                txtConCurRate.Text = Convert.ToDecimal(dt.Rows[0]["RealAmtEEFC_Rate"].ToString()).ToString("0.000000");
            else
                txtConCurRate.Text = dt.Rows[0]["RealAmtEEFC_Rate"].ToString();
            if (dt.Rows[0]["EEFC_Exchange_Rate"].ToString() != "")
                txtCrossCurrRate.Text = Convert.ToDecimal(dt.Rows[0]["EEFC_Exchange_Rate"].ToString()).ToString("0.000000");
            else
                txtCrossCurrRate.Text = dt.Rows[0]["EEFC_Exchange_Rate"].ToString();
            if (dt.Rows[0]["BalanceAmt"].ToString() != "")
                txtBalAmt.Text = Convert.ToDecimal(dt.Rows[0]["BalanceAmt"].ToString()).ToString("0.00");
            else
                txtBalAmt.Text = dt.Rows[0]["BalanceAmt"].ToString();
            if (dt.Rows[0]["BalanceAmtinINR"].ToString() != "")
                txtBalAmtinINR.Text = Convert.ToDecimal(dt.Rows[0]["BalanceAmtinINR"].ToString()).ToString("0.00");
            else
                txtBalAmtinINR.Text = dt.Rows[0]["BalanceAmtinINR"].ToString();
            if (dt.Rows[0]["RealAmtEEFC_INR"].ToString() != "")
                txtTotConRate.Text = Convert.ToDecimal(dt.Rows[0]["RealAmtEEFC_INR"].ToString()).ToString("0.00");
            else
                txtTotConRate.Text = dt.Rows[0]["RealAmtEEFC_INR"].ToString();
            if (dt.Rows[0]["CrossAmt"].ToString() != "")
                txtEEFCAmtTotal.Text = Convert.ToDecimal(dt.Rows[0]["CrossAmt"].ToString()).ToString("0.00");
            else
                txtEEFCAmtTotal.Text = dt.Rows[0]["CrossAmt"].ToString();
            txtForwardContract.Text = dt.Rows[0]["ForwardContractNo"].ToString();
            ddlAccountType.SelectedValue = dt.Rows[0]["AccountType"].ToString();
            //txtNYRefNo.Text = dt.Rows[0]["Realised_NY_Ref_No"].ToString();
            libor1.Value = dt.Rows[0]["LIBOR"].ToString();


            fillReceivingBank(txtCurrency.Text);
            ddlAccountType.DataTextField = dt.Rows[0]["AccountType"].ToString();
            ddlAccountType.DataValueField = dt.Rows[0]["AccountType"].ToString();


            if (dt.Rows[0]["FIRC_Status"].ToString() != "")
            {
                if (dt.Rows[0]["FIRC_Status"].ToString() == "Y")
                { chkFirc.Checked = true; }
                else
                { chkFirc.Checked = false; }
                chkFirc_CheckedChanged(this, null);
            }

            if (dt.Rows[0]["ProfitLieo_Status"].ToString() != "")
            {
                if (dt.Rows[0]["ProfitLieo_Status"].ToString() == "Y")

                { chkProfitLio.Checked = true; }
                else
                { chkProfitLio.Checked = false; }
                chkProfitLio_CheckedChanged(this, null);
            }

            txtFircNo.Text = dt.Rows[0]["FIRC_NO"].ToString();
            txtFircAdCode.Text = dt.Rows[0]["FIRC_AD_CODE"].ToString();

            if (dt.Rows[0]["FBKCharge"].ToString() != "")
                txt_fbkcharges.Text = Convert.ToDecimal(dt.Rows[0]["FBKCharge"].ToString()).ToString("0.00");
            else
                txt_fbkcharges.Text = dt.Rows[0]["FBKCharge"].ToString();

            if (dt.Rows[0]["FBKChargeINR"].ToString() != "")
                txt_fbkchargesinRS.Text = Convert.ToDecimal(dt.Rows[0]["FBKChargeINR"].ToString()).ToString("0.00");
            else
                txt_fbkchargesinRS.Text = dt.Rows[0]["FBKChargeINR"].ToString();

            if (dt.Rows[0]["pcfcamt"].ToString() != "")
                txtPcfcAmt.Text = Convert.ToDecimal(dt.Rows[0]["pcfcamt"].ToString()).ToString("0.00");
            else
                txtPcfcAmt.Text = dt.Rows[0]["pcfcamt"].ToString();

            if (dt.Rows[0]["overdueamt"].ToString() != "")
                txtOverDue.Text = Convert.ToDecimal(dt.Rows[0]["overdueamt"].ToString()).ToString("0.00");
            else
                txtOverDue.Text = dt.Rows[0]["overdueamt"].ToString();

            if (dt.Rows[0]["ForeignORLocal"].ToString() == "F")
            {
                Check_LEI_ThresholdLimit();
            }

           

            if (txtDocPrFx.Value == "BLA" || txtDocPrFx.Value == "BLU" || txtDocPrFx.Value == "BBA" || txtDocPrFx.Value == "BBU")
            {
                if (dt.Rows[0]["Payer"].ToString() == "S")
                {
                    rdbShipper.Checked = true;
                }
                else if (dt.Rows[0]["Payer"].ToString() == "B")
                {
                    rdbBuyer.Checked = true;
                }

                txtIntFrmDate1.Text = dt.Rows[0]["InterestFrom"].ToString().Trim();
                txtIntToDate1.Text = dt.Rows[0]["InterestTo"].ToString().Trim();
                txtForDays1.Text = dt.Rows[0]["InterestDays"].ToString().Trim();
                txtIntRate1.Text = dt.Rows[0]["InterestRates"].ToString().Trim();

                txtInterestAmt.Text = dt.Rows[0]["Interest"].ToString();
            }
            //////////////////////////added by Shailesh  31/07/2023 
            ddlModeOfPayment.SelectedValue = dt.Rows[0]["ModeofPayment"].ToString().Trim();
            txtPurposeCode.Text = dt.Rows[0]["Purpose_Code"].ToString();
            txtPurposeCode_TextChanged(null, null);

            string CheckIRM = dt.Rows[0]["CheckIRM"].ToString();
            if (CheckIRM == "Y")
            {
                chkIRMCreate.Checked = true;
                chkIRMCreate_CheckedChanged(null, null);

                txtBankUniqueTransactionID.Text = dt.Rows[0]["BankUniqueTransactionID"].ToString();
                txtIFSCCode.Text = dt.Rows[0]["IFSCCode"].ToString();
                txtRemittanceADCode.Text = dt.Rows[0]["RemittanceADCode"].ToString();
                txtIECCode.Text = dt.Rows[0]["IECCode"].ToString();
                txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString();

                //ddlFactoringflag.Text = dt.Rows[0]["Factoringflag"].ToString();
                //ddlForfeitingflag.Text = dt.Rows[0]["Forfeitingflag"].ToString();
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
            }
            else
            {
                chkIRMCreate.Checked = false;
            }

            string CheckTT = dt.Rows[0]["CheckTT"].ToString();
            if (CheckTT == "Y")
            {
                btnTTRefNoList.Checked = true;
                chkTTRefNo_CheckedChanged(null, null);
                cpe2.Collapsed = false;
            }
            else
            {
                btnTTRefNoList.Checked = false;
                cpe2.Collapsed = true;
            }

            string CheckDummy = dt.Rows[0]["CheckDummySettlement"].ToString();
            if (CheckDummy == "Y")
            {
                chkDummySettlement.Checked = true;
            }
            else
            {
                chkDummySettlement.Checked = false;
            }

            txtTTRefNo1.Text = dt.Rows[0]["TTREFNO1"].ToString();
            if (dt.Rows[0]["TTAmt1"].ToString() != "")
                txtTTAmount1.Text = Convert.ToDecimal(dt.Rows[0]["TTAmt1"].ToString()).ToString("0.00");
            else
                txtTTAmount1.Text = dt.Rows[0]["TTAmt1"].ToString();

            txtTTRefNo2.Text = dt.Rows[0]["TTREFNO2"].ToString();
            if (dt.Rows[0]["TTAmt2"].ToString() != "")
                txtTTAmount2.Text = Convert.ToDecimal(dt.Rows[0]["TTAmt2"].ToString()).ToString("0.00");
            else
                txtTTAmount2.Text = dt.Rows[0]["TTAmt2"].ToString();

            txtTTRefNo3.Text = dt.Rows[0]["TTREFNO3"].ToString();
            if (dt.Rows[0]["TTAmt3"].ToString() != "")
                txtTTAmount3.Text = Convert.ToDecimal(dt.Rows[0]["TTAmt3"].ToString()).ToString("0.00");
            else
                txtTTAmount3.Text = dt.Rows[0]["TTAmt3"].ToString();

            txtTTRefNo4.Text = dt.Rows[0]["TTREFNO4"].ToString();
            if (dt.Rows[0]["TTAmt4"].ToString() != "")
                txtTTAmount4.Text = Convert.ToDecimal(dt.Rows[0]["TTAmt4"].ToString()).ToString("0.00");
            else
                txtTTAmount4.Text = dt.Rows[0]["TTAmt4"].ToString();

            txtTTRefNo5.Text = dt.Rows[0]["TTREFNO5"].ToString();
            if (dt.Rows[0]["TTAmt5"].ToString() != "")
                txtTTAmount5.Text = Convert.ToDecimal(dt.Rows[0]["TTAmt5"].ToString()).ToString("0.00");
            else
                txtTTAmount5.Text = dt.Rows[0]["TTAmt5"].ToString();

            txtTTAmount6.Text = dt.Rows[0]["TTAmt6"].ToString().Trim();
            txtTTAmount7.Text = dt.Rows[0]["TTAmt7"].ToString().Trim();
            txtTTAmount8.Text = dt.Rows[0]["TTAmt8"].ToString().Trim();
            txtTTAmount9.Text = dt.Rows[0]["TTAmt9"].ToString().Trim();
            txtTTAmount10.Text = dt.Rows[0]["TTAmt10"].ToString().Trim();
            txtTTAmount11.Text = dt.Rows[0]["TTAmt11"].ToString().Trim();
            txtTTAmount12.Text = dt.Rows[0]["TTAmt12"].ToString().Trim();
            txtTTAmount13.Text = dt.Rows[0]["TTAmt13"].ToString().Trim();
            txtTTAmount14.Text = dt.Rows[0]["TTAmt14"].ToString().Trim();
            txtTTAmount15.Text = dt.Rows[0]["TTAmt15"].ToString().Trim();

            txtTTRefNo6.Text = dt.Rows[0]["TTREFNO6"].ToString().Trim();
            txtTTRefNo7.Text = dt.Rows[0]["TTREFNO7"].ToString().Trim();
            txtTTRefNo8.Text = dt.Rows[0]["TTREFNO8"].ToString().Trim();
            txtTTRefNo9.Text = dt.Rows[0]["TTREFNO9"].ToString().Trim();
            txtTTRefNo10.Text = dt.Rows[0]["TTREFNO10"].ToString().Trim();
            txtTTRefNo11.Text = dt.Rows[0]["TTREFNO11"].ToString().Trim();
            txtTTRefNo12.Text = dt.Rows[0]["TTREFNO12"].ToString().Trim();
            txtTTRefNo13.Text = dt.Rows[0]["TTREFNO13"].ToString().Trim();
            txtTTRefNo14.Text = dt.Rows[0]["TTREFNO14"].ToString().Trim();
            txtTTRefNo15.Text = dt.Rows[0]["TTREFNO15"].ToString().Trim();

            ddlTTCurrency1.SelectedValue = dt.Rows[0]["TTCurr1"].ToString().Trim();
            ddlTTCurrency2.SelectedValue = dt.Rows[0]["TTCurr2"].ToString().Trim();
            ddlTTCurrency3.SelectedValue = dt.Rows[0]["TTCurr3"].ToString().Trim();
            ddlTTCurrency4.SelectedValue = dt.Rows[0]["TTCurr4"].ToString().Trim();
            ddlTTCurrency5.SelectedValue = dt.Rows[0]["TTCurr5"].ToString().Trim();
            ddlTTCurrency6.SelectedValue = dt.Rows[0]["TTCurr6"].ToString().Trim();
            ddlTTCurrency7.SelectedValue = dt.Rows[0]["TTCurr7"].ToString().Trim();
            ddlTTCurrency8.SelectedValue = dt.Rows[0]["TTCurr8"].ToString().Trim();
            ddlTTCurrency9.SelectedValue = dt.Rows[0]["TTCurr9"].ToString().Trim();
            ddlTTCurrency10.SelectedValue = dt.Rows[0]["TTCurr10"].ToString().Trim();
            ddlTTCurrency11.SelectedValue = dt.Rows[0]["TTCurr11"].ToString().Trim();
            ddlTTCurrency12.SelectedValue = dt.Rows[0]["TTCurr12"].ToString().Trim();
            ddlTTCurrency13.SelectedValue = dt.Rows[0]["TTCurr13"].ToString().Trim();
            ddlTTCurrency14.SelectedValue = dt.Rows[0]["TTCurr14"].ToString().Trim();
            ddlTTCurrency15.SelectedValue = dt.Rows[0]["TTCurr15"].ToString().Trim();

            txtTotTTAmt1.Text = dt.Rows[0]["TotTTAmt1"].ToString().Trim();
            txtTotTTAmt2.Text = dt.Rows[0]["TotTTAmt2"].ToString().Trim();
            txtTotTTAmt3.Text = dt.Rows[0]["TotTTAmt3"].ToString().Trim();
            txtTotTTAmt4.Text = dt.Rows[0]["TotTTAmt4"].ToString().Trim();
            txtTotTAmt5.Text = dt.Rows[0]["TotTTAmt5"].ToString().Trim();
            txtTotTTAmt6.Text = dt.Rows[0]["TotTTAmt6"].ToString().Trim();
            txtTotTTAmt7.Text = dt.Rows[0]["TotTTAmt7"].ToString().Trim();
            txtTotTTAmt8.Text = dt.Rows[0]["TotTTAmt8"].ToString().Trim();
            txtTtTTAmt9.Text = dt.Rows[0]["TotTTAmt9"].ToString().Trim();
            txtTotTTAmt10.Text = dt.Rows[0]["TotTTAmt10"].ToString().Trim();
            txtTotTTAmt11.Text = dt.Rows[0]["TotTTAmt11"].ToString().Trim();
            txtTotTTAmt12.Text = dt.Rows[0]["TotTTAmt12"].ToString().Trim();
            txtTotTTAmt13.Text = dt.Rows[0]["TotTTAmt13"].ToString().Trim();
            txtTotTTAmt14.Text = dt.Rows[0]["TotTTAmt14"].ToString().Trim();
            txtTotTTAmt15.Text = dt.Rows[0]["TotTTAmt15"].ToString().Trim();

            txtBalTTAmt1.Text = dt.Rows[0]["BalTTAmt1"].ToString().Trim();
            txtBalTTAmt2.Text = dt.Rows[0]["BalTTAmt2"].ToString().Trim();
            txtBalTTAmt3.Text = dt.Rows[0]["BalTTAmt3"].ToString().Trim();
            txtBalTTAmt4.Text = dt.Rows[0]["BalTTAmt4"].ToString().Trim();
            txtBalTTAmt5.Text = dt.Rows[0]["BalTTAmt5"].ToString().Trim();
            txtBalTTAmt6.Text = dt.Rows[0]["BalTTAmt6"].ToString().Trim();
            txtBalTTAmt7.Text = dt.Rows[0]["BalTTAmt7"].ToString().Trim();
            txtBalTAmt8.Text = dt.Rows[0]["BalTTAmt8"].ToString().Trim();
            txtBalTTAmt9.Text = dt.Rows[0]["BalTTAmt9"].ToString().Trim();
            txtBalTTAmt10.Text = dt.Rows[0]["BalTTAmt10"].ToString().Trim();
            txtBalTTAmt11.Text = dt.Rows[0]["BalTTAmt11"].ToString().Trim();
            txtBalTTAmt12.Text = dt.Rows[0]["BalTTAmt12"].ToString().Trim();
            txtBalTTAmt13.Text = dt.Rows[0]["BalTTAmt13"].ToString().Trim();
            txtBalTTAmt14.Text = dt.Rows[0]["BalTTAmt14"].ToString().Trim();
            txtBalTTAmt15.Text = dt.Rows[0]["BalTTAmt15"].ToString().Trim();

            ddlTTRealisedCurr1.SelectedValue = dt.Rows[0]["TTRealisedCurr1"].ToString().Trim();
            ddlTTRealisedCurr2.SelectedValue = dt.Rows[0]["TTRealisedCurr2"].ToString().Trim();
            ddlTTRealisedCurr3.SelectedValue = dt.Rows[0]["TTRealisedCurr3"].ToString().Trim();
            ddlTTRealisedCurr4.SelectedValue = dt.Rows[0]["TTRealisedCurr4"].ToString().Trim();
            ddlTTRealisedCurr5.SelectedValue = dt.Rows[0]["TTRealisedCurr5"].ToString().Trim();
            ddlTTRealisedCurr6.SelectedValue = dt.Rows[0]["TTRealisedCurr6"].ToString().Trim();
            ddlTTRealisedCurr7.SelectedValue = dt.Rows[0]["TTRealisedCurr7"].ToString().Trim();
            ddlTTRealisedCurr8.SelectedValue = dt.Rows[0]["TTRealisedCurr8"].ToString().Trim();
            ddlTTRealisedCurr9.SelectedValue = dt.Rows[0]["TTRealisedCurr9"].ToString().Trim();
            ddlTTRealisedCurr10.SelectedValue = dt.Rows[0]["TTRealisedCurr10"].ToString().Trim();
            ddlTTRealisedCurr11.SelectedValue = dt.Rows[0]["TTRealisedCurr11"].ToString().Trim();
            ddlTTRealisedCurr12.SelectedValue = dt.Rows[0]["TTRealisedCurr12"].ToString().Trim();
            ddlTTRealisedCurr13.SelectedValue = dt.Rows[0]["TTRealisedCurr13"].ToString().Trim();
            ddlTTRealisedCurr14.SelectedValue = dt.Rows[0]["TTRealisedCurr14"].ToString().Trim();
            ddlTTRealisedCurr15.SelectedValue = dt.Rows[0]["TTRealisedCurr15"].ToString().Trim();


            txtTTCrossCurrRate1.Text = dt.Rows[0]["TTCrossCurrRate1"].ToString().Trim();
            txtTTCrossCurrRate2.Text = dt.Rows[0]["TTCrossCurrRate2"].ToString().Trim();
            txtTTCrossCurrRate3.Text = dt.Rows[0]["TTCrossCurrRate3"].ToString().Trim();
            txtTTCrossCurrRate4.Text = dt.Rows[0]["TTCrossCurrRate4"].ToString().Trim();
            txtTTCrossCurrRate5.Text = dt.Rows[0]["TTCrossCurrRate5"].ToString().Trim();
            txtTTCrossCurrRate6.Text = dt.Rows[0]["TTCrossCurrRate6"].ToString().Trim();
            txtTTCrossCurrRate7.Text = dt.Rows[0]["TTCrossCurrRate7"].ToString().Trim();
            txtTTCrossCurrRate8.Text = dt.Rows[0]["TTCrossCurrRate8"].ToString().Trim();
            txtTTCrossCurrRate9.Text = dt.Rows[0]["TTCrossCurrRate9"].ToString().Trim();
            txtTTCrossCurrRate10.Text = dt.Rows[0]["TTCrossCurrRate10"].ToString().Trim();
            txtTTCrossCurrRate11.Text = dt.Rows[0]["TTCrossCurrRate11"].ToString().Trim();
            txtTTCrossCurrRate12.Text = dt.Rows[0]["TTCrossCurrRate12"].ToString().Trim();
            txtTTCrossCurrRate13.Text = dt.Rows[0]["TTCrossCurrRate13"].ToString().Trim();
            txtTTCrossCurrRate14.Text = dt.Rows[0]["TTCrossCurrRate14"].ToString().Trim();
            txtTTCrossCurrRate15.Text = dt.Rows[0]["TTCrossCurrRate15"].ToString().Trim();


            txtTTAmtRealised1.Text = dt.Rows[0]["TTAmtRealised1"].ToString().Trim();
            txtTTAmtRealised2.Text = dt.Rows[0]["TTAmtRealised2"].ToString().Trim();
            txtTTAmtRealised3.Text = dt.Rows[0]["TTAmtRealised3"].ToString().Trim();
            txtTTAmtRealised4.Text = dt.Rows[0]["TTAmtRealised4"].ToString().Trim();
            txtTTAmtRealised5.Text = dt.Rows[0]["TTAmtRealised5"].ToString().Trim();
            txtTTAmtRealised6.Text = dt.Rows[0]["TTAmtRealised6"].ToString().Trim();
            txtTTAmtRealised7.Text = dt.Rows[0]["TTAmtRealised7"].ToString().Trim();
            txtTTAmtRealised8.Text = dt.Rows[0]["TTAmtRealised8"].ToString().Trim();
            txtTTAmtRealised9.Text = dt.Rows[0]["TTAmtRealised9"].ToString().Trim();
            txtTTAmtRealised10.Text = dt.Rows[0]["TTAmtRealised10"].ToString().Trim();
            txtTTAmtRealised11.Text = dt.Rows[0]["TTAmtRealised11"].ToString().Trim();
            txtTTAmtRealised12.Text = dt.Rows[0]["TTAmtRealised12"].ToString().Trim();
            txtTTAmtRealised13.Text = dt.Rows[0]["TTAmtRealised13"].ToString().Trim();
            txtTTAmtRealised14.Text = dt.Rows[0]["TTAmtRealised14"].ToString().Trim();
            txtTTAmtRealised15.Text = dt.Rows[0]["TTAmtRealised15"].ToString().Trim();

            //ddlAccountType.DataBind();

            string CheckPendingSB = dt.Rows[0]["PendingSBFlag"].ToString().Trim();
            if (CheckPendingSB == "Y")
            {
                chkSB.Checked = true;
                lblSB.Text = "Yes";
            }
            else
            {
                chkSB.Checked = false;
                lblSB.Text = "No";
            }

            //-------------------------------Anand27/06/2023-------------------------------------------------
            /////IMPORT ACCOUNTING 1

            string IMPACC1Flag = dt.Rows[0]["IMP_ACC1_Flag"].ToString();
            if (IMPACC1Flag == "Y")
            {
                chk_IMPACC1Flag.Checked = true;
                chk_IMPACC1Flag_OnCheckedChanged(null, null);
                // SqlParameter P_chk_IMPACC1Flag = new SqlParameter("@IMP_ACC1_Flag", "");//_chk_IMPACC1Flag.ToUpper());

                txt_IMPACC1_FCRefNo.Text = dt.Rows[0]["IMP_ACC1_FCRefNo"].ToString();
                txt_IMPACC1_DiscAmt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Amount"].ToString()).ToString("0.00");
                // SqlParameter P_txt_IMPACC1_DiscExchRate = new SqlParameter("@IMP_ACC1_ExchRate", "");//_txt_IMPACC1_DiscExchRate.ToUpper());
                txt_IMPACC1_Princ_matu.Text = dt.Rows[0]["IMP_ACC1_Principal_MATU"].ToString();
                txt_IMPACC1_Princ_lump.Text = dt.Rows[0]["IMP_ACC1_Principal_LUMP"].ToString();
                txt_IMPACC1_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Principal_Contract_No"].ToString();
                txt_IMPACC1_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Principal_Ex_Curr"].ToString();
                txt_IMPACC1_Princ_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Principal_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_Princ_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Principal_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_Interest_matu.Text = dt.Rows[0]["IMP_ACC1_Interest_MATU"].ToString();
                txt_IMPACC1_Interest_lump.Text = dt.Rows[0]["IMP_ACC1_Interest_LUMP"].ToString();
                txt_IMPACC1_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Interest_Contract_No"].ToString();
                txt_IMPACC1_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Interest_Ex_Curr"].ToString();
                txt_IMPACC1_Interest_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Interest_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_Interest_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Interest_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_Commission_matu.Text = dt.Rows[0]["IMP_ACC1_Commission_MATU"].ToString();
                txt_IMPACC1_Commission_lump.Text = dt.Rows[0]["IMP_ACC1_Commission_LUMP"].ToString();
                txt_IMPACC1_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Commission_Contract_No"].ToString();
                txt_IMPACC1_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Commission_Ex_Curr"].ToString();
                txt_IMPACC1_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_MATU"].ToString();
                txt_IMPACC1_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_LUMP"].ToString();
                txt_IMPACC1_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_Contract_No"].ToString();
                txt_IMPACC1_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC1_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC1_Their_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Their_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_Their_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_Their_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC1_CR_Code.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC1_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC1_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC1_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC1_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC1_CR_Acceptance_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Amt"].ToString()).ToString("0.00");
                txt_IMPACC1_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC1_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC1_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Interest_Curr"].ToString();
                txt_IMPACC1_CR_Interest_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_CR_Interest_Amount"].ToString()).ToString("0.00");
                txt_IMPACC1_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC1_CR_Interest_Payer"].ToString();
                txt_IMPACC1_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC1_CR_Accept_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_CR_Acceptance_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC1_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC1_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC1_CR_Pay_Handle_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_CR_Pay_Handle_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC1_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC1_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Others_Curr"].ToString();
                txt_IMPACC1_CR_Others_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_CR_Others_Amount"].ToString()).ToString("0.00");
                txt_IMPACC1_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Others_Payer"].ToString();
                txt_IMPACC1_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC1_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC1_CR_Their_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_CR_Their_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC1_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC1_CR_Their_Comm_Payer"].ToString();
                txt_IMPACC1_DR_Code.Text = dt.Rows[0]["IMP_ACC1_DR_Code"].ToString();
                txt_IMPACC1_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name"].ToString();
                txt_IMPACC1_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr"].ToString();
                txt_IMPACC1_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount"].ToString()).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt2.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount2"].ToString()).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt3.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount3"].ToString()).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC1_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt4.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount4"].ToString()).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt5.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount5"].ToString()).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC1_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC1_DR_Cur_Acc_amt6.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Amount6"].ToString()).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC1_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC1_DR_Code2.Text = dt.Rows[0]["IMP_ACC1_DR_Code2"].ToString();
                txt_IMPACC1_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC1_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr2"].ToString();
                txt_IMPACC1_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC1_DR_Code3.Text = dt.Rows[0]["IMP_ACC1_DR_Code3"].ToString();
                txt_IMPACC1_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC1_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr3"].ToString();
                txt_IMPACC1_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC1_DR_Code4.Text = dt.Rows[0]["IMP_ACC1_DR_Code4"].ToString();
                txt_IMPACC1_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC1_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr4"].ToString();
                txt_IMPACC1_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC1_DR_Code5.Text = dt.Rows[0]["IMP_ACC1_DR_Code5"].ToString();
                txt_IMPACC1_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC1_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr5"].ToString();
                txt_IMPACC1_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC1_DR_Code6.Text = dt.Rows[0]["IMP_ACC1_DR_Code6"].ToString();
                txt_IMPACC1_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC1_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC1_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Abbr6"].ToString();
                txt_IMPACC1_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC1_DR_Cust_Acc_No6"].ToString();
            }
            else
            {
                chk_IMPACC1Flag.Checked = false;
            }

            ////IMPORT ACCOUNTING 2

            string IMPACC2Flag = dt.Rows[0]["IMP_ACC2_Flag"].ToString();
            if (IMPACC2Flag == "Y")
            {
                chk_IMPACC2Flag.Checked = true;
                chk_IMPACC2Flag_OnCheckedChanged(null, null);
                // SqlParameter P_chk_IMPACC2Flag = new SqlParameter("@IMP_ACC2_Flag", "");//_chk_IMPACC2Flag.ToUpper());
                txt_IMPACC2_FCRefNo.Text = dt.Rows[0]["IMP_ACC2_FCRefNo"].ToString();
                txt_IMPACC2_DiscAmt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Amount"].ToString()).ToString("0.00");
                // SqlParameter P_txt_IMPACC2_DiscExchRate = new SqlParameter("@IMP_ACC2_ExchRate", "");//_txt_IMPACC2_DiscExchRate.Text.Trim());
                txt_IMPACC2_Princ_matu.Text = dt.Rows[0]["IMP_ACC2_Principal_MATU"].ToString();
                txt_IMPACC2_Princ_lump.Text = dt.Rows[0]["IMP_ACC2_Principal_LUMP"].ToString();
                txt_IMPACC2_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Principal_Contract_No"].ToString();
                txt_IMPACC2_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Principal_Ex_Curr"].ToString();
                txt_IMPACC2_Princ_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Principal_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC2_Princ_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Principal_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC2_Interest_matu.Text = dt.Rows[0]["IMP_ACC2_Interest_MATU"].ToString();
                txt_IMPACC2_Interest_lump.Text = dt.Rows[0]["IMP_ACC2_Interest_LUMP"].ToString();
                txt_IMPACC2_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Interest_Contract_No"].ToString();
                txt_IMPACC2_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Interest_Ex_Curr"].ToString();
                txt_IMPACC2_Interest_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Interest_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC2_Interest_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Interest_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC2_Commission_matu.Text = dt.Rows[0]["IMP_ACC2_Commission_MATU"].ToString();
                txt_IMPACC2_Commission_lump.Text = dt.Rows[0]["IMP_ACC2_Commission_LUMP"].ToString();
                txt_IMPACC2_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Commission_Contract_No"].ToString();
                txt_IMPACC2_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Commission_Ex_Curr"].ToString();
                txt_IMPACC2_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC2_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC2_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_MATU"].ToString();
                txt_IMPACC2_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_LUMP"].ToString();
                txt_IMPACC2_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_Contract_No"].ToString();
                txt_IMPACC2_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC2_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC2_Their_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Their_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC2_Their_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_Their_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC2_CR_Code.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC2_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC2_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC2_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC2_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC2_CR_Acceptance_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Amt"].ToString()).ToString("0.00");
                txt_IMPACC2_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC2_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC2_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Interest_Curr"].ToString();
                txt_IMPACC2_CR_Interest_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_CR_Interest_Amount"].ToString()).ToString("0.00");
                txt_IMPACC2_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC2_CR_Interest_Payer"].ToString();
                txt_IMPACC2_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC2_CR_Accept_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_CR_Acceptance_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC2_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC2_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC2_CR_Pay_Handle_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_CR_Pay_Handle_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC2_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC2_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Others_Curr"].ToString();
                txt_IMPACC2_CR_Others_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_CR_Others_Amount"].ToString()).ToString("0.00");
                txt_IMPACC2_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Others_Payer"].ToString();
                txt_IMPACC2_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC2_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC2_CR_Their_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC2_CR_Their_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC2_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC2_CR_Their_Comm_Payer"].ToString();

                txt_IMPACC2_DR_Code.Text = dt.Rows[0]["IMP_ACC2_DR_Code"].ToString();
                txt_IMPACC2_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name"].ToString();
                txt_IMPACC2_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr"].ToString();
                txt_IMPACC2_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No"].ToString();

                txt_IMPACC2_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC2_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC2_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC2_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC2_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC2_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC2_DR_Code2.Text = dt.Rows[0]["IMP_ACC2_DR_Code2"].ToString();
                txt_IMPACC2_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC2_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr2"].ToString();
                txt_IMPACC2_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC2_DR_Code3.Text = dt.Rows[0]["IMP_ACC2_DR_Code3"].ToString();
                txt_IMPACC2_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC2_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr3"].ToString();
                txt_IMPACC2_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC2_DR_Code4.Text = dt.Rows[0]["IMP_ACC2_DR_Code4"].ToString();
                txt_IMPACC2_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC2_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr4"].ToString();
                txt_IMPACC2_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC2_DR_Code5.Text = dt.Rows[0]["IMP_ACC2_DR_Code5"].ToString();
                txt_IMPACC2_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC2_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr5"].ToString();
                txt_IMPACC2_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC2_DR_Code6.Text = dt.Rows[0]["IMP_ACC2_DR_Code6"].ToString();
                txt_IMPACC2_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC2_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC2_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Abbr6"].ToString();
                txt_IMPACC2_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC2_DR_Cust_Acc_No6"].ToString();
            }
            else
            {
                chk_IMPACC2Flag.Checked = false;
            }
            //////IMPORT ACCOUNTING  3

            string IMPACC3Flag = dt.Rows[0]["IMP_ACC3_Flag"].ToString();
            if (IMPACC3Flag == "Y")
            {
                chk_IMPACC3Flag.Checked = true;
                chk_IMPACC3Flag_OnCheckedChanged(null, null);
                // SqlParameter P_chk_IMPACC3Flag = new SqlParameter("@IMP_ACC3_Flag", "");//_chk_IMPACC3Flag.Text.Trim());
                txt_IMPACC3_FCRefNo.Text = dt.Rows[0]["IMP_ACC3_FCRefNo"].ToString();
                txt_IMPACC3_DiscAmt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Amount"].ToString()).ToString("0.00");
                // SqlParameter P_txt_IMPACC3_DiscExchRate = new SqlParameter("@IMP_ACC3_ExchRate", "");//txt_IMPACC3_DiscExchRate.Text.Trim());
                txt_IMPACC3_Princ_matu.Text = dt.Rows[0]["IMP_ACC3_Principal_MATU"].ToString();
                txt_IMPACC3_Princ_lump.Text = dt.Rows[0]["IMP_ACC3_Principal_LUMP"].ToString();
                txt_IMPACC3_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Principal_Contract_No"].ToString();
                txt_IMPACC3_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Principal_Ex_Curr"].ToString();
                txt_IMPACC3_Princ_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Principal_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC3_Princ_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Principal_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC3_Interest_matu.Text = dt.Rows[0]["IMP_ACC3_Interest_MATU"].ToString();
                txt_IMPACC3_Interest_lump.Text = dt.Rows[0]["IMP_ACC3_Interest_LUMP"].ToString();
                txt_IMPACC3_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Interest_Contract_No"].ToString();
                txt_IMPACC3_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Interest_Ex_Curr"].ToString();
                txt_IMPACC3_Interest_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Interest_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC3_Interest_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Interest_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC3_Commission_matu.Text = dt.Rows[0]["IMP_ACC3_Commission_MATU"].ToString();
                txt_IMPACC3_Commission_lump.Text = dt.Rows[0]["IMP_ACC3_Commission_LUMP"].ToString();
                txt_IMPACC3_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Commission_Contract_No"].ToString();
                txt_IMPACC3_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Commission_Ex_Curr"].ToString();
                txt_IMPACC3_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC3_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC3_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_MATU"].ToString();
                txt_IMPACC3_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_LUMP"].ToString();
                txt_IMPACC3_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_Contract_No"].ToString();
                txt_IMPACC3_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC3_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC3_Their_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Their_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC3_Their_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_Their_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC3_CR_Code.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC3_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC3_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC3_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC3_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC3_CR_Acceptance_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Amt"].ToString()).ToString("0.00");
                txt_IMPACC3_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC3_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC3_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Interest_Curr"].ToString();
                txt_IMPACC3_CR_Interest_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_CR_Interest_Amount"].ToString()).ToString("0.00");
                txt_IMPACC3_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC3_CR_Interest_Payer"].ToString();
                txt_IMPACC3_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC3_CR_Accept_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_CR_Acceptance_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC3_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC3_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC3_CR_Pay_Handle_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_CR_Pay_Handle_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC3_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC3_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Others_Curr"].ToString();
                txt_IMPACC3_CR_Others_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_CR_Others_Amount"].ToString()).ToString("0.00");
                txt_IMPACC3_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Others_Payer"].ToString();
                txt_IMPACC3_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC3_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC3_CR_Their_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC3_CR_Their_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC3_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC3_CR_Their_Comm_Payer"].ToString();

                txt_IMPACC3_DR_Code.Text = dt.Rows[0]["IMP_ACC3_DR_Code"].ToString();
                txt_IMPACC3_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name"].ToString();
                txt_IMPACC3_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr"].ToString();
                txt_IMPACC3_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No"].ToString();

                txt_IMPACC3_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC3_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC3_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC3_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC3_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC3_DR_Current_Acc_Payer6"].ToString();


                txt_IMPACC3_DR_Code2.Text = dt.Rows[0]["IMP_ACC3_DR_Code2"].ToString();
                txt_IMPACC3_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC3_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr2"].ToString();
                txt_IMPACC3_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC3_DR_Code3.Text = dt.Rows[0]["IMP_ACC3_DR_Code3"].ToString();
                txt_IMPACC3_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC3_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr3"].ToString();
                txt_IMPACC3_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC3_DR_Code4.Text = dt.Rows[0]["IMP_ACC3_DR_Code4"].ToString();
                txt_IMPACC3_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC3_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr4"].ToString();
                txt_IMPACC3_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC3_DR_Code5.Text = dt.Rows[0]["IMP_ACC3_DR_Code5"].ToString();
                txt_IMPACC3_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC3_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr5"].ToString();
                txt_IMPACC3_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC3_DR_Code6.Text = dt.Rows[0]["IMP_ACC3_DR_Code6"].ToString();
                txt_IMPACC3_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC3_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC3_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Abbr6"].ToString();
                txt_IMPACC3_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC3_DR_Cust_Acc_No6"].ToString();
            }
            else
            {
                chk_IMPACC3Flag.Checked = false;
            }
            //////IMPORT ACCOUNTING 4
            string IMPACC4Flag = dt.Rows[0]["IMP_ACC4_Flag"].ToString();
            if (IMPACC4Flag == "Y")
            {
                chk_IMPACC4Flag.Checked = true;
                chk_IMPACC4Flag_OnCheckedChanged(null, null);
                //SqlParameter P_chk_IMPACC4Flag = new SqlParameter("@IMP_ACC4_Flag", "");//_chk_IMPACC4Flag.Text.Trim());
                txt_IMPACC4_FCRefNo.Text = dt.Rows[0]["IMP_ACC4_FCRefNo"].ToString();
                txt_IMPACC4_DiscAmt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Amount"].ToString()).ToString("0.00");
                // SqlParameter P_txt_IMPACC4_DiscExchRate = new SqlParameter("@IMP_ACC4_ExchRate", "");//_txt_IMPACC4_DiscExchRate.Text.Trim());
                txt_IMPACC4_Princ_matu.Text = dt.Rows[0]["IMP_ACC4_Principal_MATU"].ToString();
                txt_IMPACC4_Princ_lump.Text = dt.Rows[0]["IMP_ACC4_Principal_LUMP"].ToString();
                txt_IMPACC4_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Principal_Contract_No"].ToString();
                txt_IMPACC4_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Principal_Ex_Curr"].ToString();
                txt_IMPACC4_Princ_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Principal_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC4_Princ_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Principal_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC4_Interest_matu.Text = dt.Rows[0]["IMP_ACC4_Interest_MATU"].ToString();
                txt_IMPACC4_Interest_lump.Text = dt.Rows[0]["IMP_ACC4_Interest_LUMP"].ToString();
                txt_IMPACC4_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Interest_Contract_No"].ToString();
                txt_IMPACC4_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Interest_Ex_Curr"].ToString();
                txt_IMPACC4_Interest_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Interest_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC4_Interest_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Interest_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC4_Commission_matu.Text = dt.Rows[0]["IMP_ACC4_Commission_MATU"].ToString();
                txt_IMPACC4_Commission_lump.Text = dt.Rows[0]["IMP_ACC4_Commission_LUMP"].ToString();
                txt_IMPACC4_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Commission_Contract_No"].ToString();
                txt_IMPACC4_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Commission_Ex_Curr"].ToString();
                txt_IMPACC4_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC4_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC4_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_MATU"].ToString();
                txt_IMPACC4_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_LUMP"].ToString();
                txt_IMPACC4_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_Contract_No"].ToString();
                txt_IMPACC4_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC4_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC4_Their_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Their_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC4_Their_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_Their_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC4_CR_Code.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC4_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC4_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC4_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC4_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC4_CR_Acceptance_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Amt"].ToString()).ToString("0.00");
                txt_IMPACC4_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC4_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC4_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Interest_Curr"].ToString();
                txt_IMPACC4_CR_Interest_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_CR_Interest_Amount"].ToString()).ToString("0.00");
                txt_IMPACC4_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC4_CR_Interest_Payer"].ToString();
                txt_IMPACC4_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC4_CR_Accept_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_CR_Acceptance_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC4_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC4_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC4_CR_Pay_Handle_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_CR_Pay_Handle_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC4_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC4_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Others_Curr"].ToString();
                txt_IMPACC4_CR_Others_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_CR_Others_Amount"].ToString()).ToString("0.00");
                txt_IMPACC4_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Others_Payer"].ToString();
                txt_IMPACC4_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC4_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC4_CR_Their_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC4_CR_Their_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC4_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC4_CR_Their_Comm_Payer"].ToString();

                txt_IMPACC4_DR_Code.Text = dt.Rows[0]["IMP_ACC4_DR_Code"].ToString();
                txt_IMPACC4_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name"].ToString();
                txt_IMPACC4_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr"].ToString();
                txt_IMPACC4_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No"].ToString();

                txt_IMPACC4_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC4_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC4_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC4_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC4_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC4_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC4_DR_Code2.Text = dt.Rows[0]["IMP_ACC4_DR_Code2"].ToString();
                txt_IMPACC4_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC4_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr2"].ToString();
                txt_IMPACC4_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC4_DR_Code3.Text = dt.Rows[0]["IMP_ACC4_DR_Code3"].ToString();
                txt_IMPACC4_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC4_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr3"].ToString();
                txt_IMPACC4_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC4_DR_Code4.Text = dt.Rows[0]["IMP_ACC4_DR_Code4"].ToString();
                txt_IMPACC4_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC4_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr4"].ToString();
                txt_IMPACC4_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC4_DR_Code5.Text = dt.Rows[0]["IMP_ACC4_DR_Code5"].ToString();
                txt_IMPACC4_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC4_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr5"].ToString();
                txt_IMPACC4_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC4_DR_Code6.Text = dt.Rows[0]["IMP_ACC4_DR_Code6"].ToString();
                txt_IMPACC4_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC4_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC4_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Abbr6"].ToString();
                txt_IMPACC4_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC4_DR_Cust_Acc_No6"].ToString();
            }
            else
            {
                chk_IMPACC4Flag.Checked = false;
            }

            ///////IMPORT ACCOUNTING 5
            string IMPACC5Flag = dt.Rows[0]["IMP_ACC5_Flag"].ToString();
            if (IMPACC5Flag == "Y")
            {
                chk_IMPACC5Flag.Checked = true;
                chk_IMPACC5Flag_OnCheckedChanged(null, null);
                //SqlParameter P_chk_IMPACC5Flag = new SqlParameter("@IMP_ACC5_Flag", "");//_chk_IMPACC5Flag.ToUpper());
                txt_IMPACC5_FCRefNo.Text = dt.Rows[0]["IMP_ACC5_FCRefNo"].ToString();
                txt_IMPACC5_DiscAmt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Amount"].ToString()).ToString("0.00");
                // SqlParameter P_txt_IMPACC5_DiscExchRate = new SqlParameter("@IMP_ACC5_ExchRate", "");// _txt_IMPACC5_DiscExchRate.ToUpper());
                txt_IMPACC5_Princ_matu.Text = dt.Rows[0]["IMP_ACC5_Principal_MATU"].ToString();
                txt_IMPACC5_Princ_lump.Text = dt.Rows[0]["IMP_ACC5_Principal_LUMP"].ToString();
                txt_IMPACC5_Princ_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Principal_Contract_No"].ToString();
                txt_IMPACC5_Princ_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Principal_Ex_Curr"].ToString();
                txt_IMPACC5_Princ_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Principal_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC5_Princ_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Principal_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC5_Interest_matu.Text = dt.Rows[0]["IMP_ACC5_Interest_MATU"].ToString();
                txt_IMPACC5_Interest_lump.Text = dt.Rows[0]["IMP_ACC5_Interest_LUMP"].ToString();
                txt_IMPACC5_Interest_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Interest_Contract_No"].ToString();
                txt_IMPACC5_Interest_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Interest_Ex_Curr"].ToString();
                txt_IMPACC5_Interest_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Interest_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC5_Interest_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Interest_Intnl_Exch_Rate"].ToString()).ToString("0.00000");

                txt_IMPACC5_Commission_matu.Text = dt.Rows[0]["IMP_ACC5_Commission_MATU"].ToString();
                txt_IMPACC5_Commission_lump.Text = dt.Rows[0]["IMP_ACC5_Commission_LUMP"].ToString();
                txt_IMPACC5_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Commission_Contract_No"].ToString();
                txt_IMPACC5_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Commission_Ex_Curr"].ToString();
                txt_IMPACC5_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC5_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC5_Their_Commission_matu.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_MATU"].ToString();
                txt_IMPACC5_Their_Commission_lump.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_LUMP"].ToString();
                txt_IMPACC5_Their_Commission_Contract_no.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_Contract_No"].ToString();
                txt_IMPACC5_Their_Commission_Ex_Curr.Text = dt.Rows[0]["IMP_ACC5_Their_Commission_Ex_Curr"].ToString();
                txt_IMPACC5_Their_Commission_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Their_Commission_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC5_Their_Commission_Intnl_Ex_rate.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_Their_Commission_Intnl_Exch_Rate"].ToString()).ToString("0.00000");
                txt_IMPACC5_CR_Code.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Code"].ToString();
                txt_IMPACC5_CR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Short_Name"].ToString();
                txt_IMPACC5_CR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Cust_Abbr"].ToString();
                txt_IMPACC5_CR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Cust_Acc_No"].ToString();
                txt_IMPACC5_CR_Acceptance_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Curr"].ToString();
                txt_IMPACC5_CR_Acceptance_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Amt"].ToString()).ToString("0.00");
                txt_IMPACC5_CR_Acceptance_payer.Text = dt.Rows[0]["IMP_ACC5_CR_Sundry_Deposit_Payer"].ToString();
                txt_IMPACC5_CR_Interest_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Interest_Curr"].ToString();
                txt_IMPACC5_CR_Interest_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_CR_Interest_Amount"].ToString()).ToString("0.00");
                txt_IMPACC5_CR_Interest_payer.Text = dt.Rows[0]["IMP_ACC5_CR_Interest_Payer"].ToString();
                txt_IMPACC5_CR_Accept_Commission_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Acceptance_Comm_Curr"].ToString();
                txt_IMPACC5_CR_Accept_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_CR_Acceptance_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC5_CR_Accept_Commission_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Acceptance_Comm_Payer"].ToString();
                txt_IMPACC5_CR_Pay_Handle_Commission_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Pay_Handle_Comm_Curr"].ToString();
                txt_IMPACC5_CR_Pay_Handle_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_CR_Pay_Handle_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC5_CR_Pay_Handle_Commission_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Pay_Handle_Comm_Payer"].ToString();
                txt_IMPACC5_CR_Others_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Others_Curr"].ToString();
                txt_IMPACC5_CR_Others_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_CR_Others_Amount"].ToString()).ToString("0.00");
                txt_IMPACC5_CR_Others_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Others_Payer"].ToString();
                txt_IMPACC5_CR_Their_Commission_Curr.Text = dt.Rows[0]["IMP_ACC5_CR_Their_Comm_Curr"].ToString();
                txt_IMPACC5_CR_Their_Commission_amt.Text = Convert.ToDecimal(dt.Rows[0]["IMP_ACC5_CR_Their_Comm_Amount"].ToString()).ToString("0.00");
                txt_IMPACC5_CR_Their_Commission_Payer.Text = dt.Rows[0]["IMP_ACC5_CR_Their_Comm_Payer"].ToString();

                txt_IMPACC5_DR_Code.Text = dt.Rows[0]["IMP_ACC5_DR_Code"].ToString();
                txt_IMPACC5_DR_AC_Short_Name.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name"].ToString();
                txt_IMPACC5_DR_Cust_abbr.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr"].ToString();
                txt_IMPACC5_DR_Cust_Acc.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No"].ToString();

                txt_IMPACC5_DR_Cur_Acc_Curr.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr2.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr2"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt2.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount2"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer2.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer2"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr3.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr3"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt3.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount3"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer3.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer3"].ToString();

                txt_IMPACC5_DR_Cur_Acc_Curr4.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr4"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt4.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount4"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer4.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer4"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr5.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr5"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt5.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount5"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer5.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer5"].ToString();
                txt_IMPACC5_DR_Cur_Acc_Curr6.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Curr6"].ToString();
                txt_IMPACC5_DR_Cur_Acc_amt6.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Amount6"].ToString();
                txt_IMPACC5_DR_Cur_Acc_payer6.Text = dt.Rows[0]["IMP_ACC5_DR_Current_Acc_Payer6"].ToString();

                txt_IMPACC5_DR_Code2.Text = dt.Rows[0]["IMP_ACC5_DR_Code2"].ToString();
                txt_IMPACC5_DR_AC_Short_Name2.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name2"].ToString();
                txt_IMPACC5_DR_Cust_abbr2.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr2"].ToString();
                txt_IMPACC5_DR_Cust_Acc2.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No2"].ToString();

                txt_IMPACC5_DR_Code3.Text = dt.Rows[0]["IMP_ACC5_DR_Code3"].ToString();
                txt_IMPACC5_DR_AC_Short_Name3.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name3"].ToString();
                txt_IMPACC5_DR_Cust_abbr3.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr3"].ToString();
                txt_IMPACC5_DR_Cust_Acc3.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No3"].ToString();

                txt_IMPACC5_DR_Code4.Text = dt.Rows[0]["IMP_ACC5_DR_Code4"].ToString();
                txt_IMPACC5_DR_AC_Short_Name4.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name4"].ToString();
                txt_IMPACC5_DR_Cust_abbr4.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr4"].ToString();
                txt_IMPACC5_DR_Cust_Acc4.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No4"].ToString();

                txt_IMPACC5_DR_Code5.Text = dt.Rows[0]["IMP_ACC5_DR_Code5"].ToString();
                txt_IMPACC5_DR_AC_Short_Name5.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name5"].ToString();
                txt_IMPACC5_DR_Cust_abbr5.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr5"].ToString();
                txt_IMPACC5_DR_Cust_Acc5.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No5"].ToString();

                txt_IMPACC5_DR_Code6.Text = dt.Rows[0]["IMP_ACC5_DR_Code6"].ToString();
                txt_IMPACC5_DR_AC_Short_Name6.Text = dt.Rows[0]["IMP_ACC5_DR_Acc_Short_Name6"].ToString();
                txt_IMPACC5_DR_Cust_abbr6.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Abbr6"].ToString();
                txt_IMPACC5_DR_Cust_Acc6.Text = dt.Rows[0]["IMP_ACC5_DR_Cust_Acc_No6"].ToString();
            }
            else
            {
                chk_IMPACC5Flag.Checked = false;
            }
            //--------------------------------END------------------------------------------------
            // ===================== Supriya START ========================
            // ------------------ General Operations ----------------------

            string Generaloperation1 = dt.Rows[0]["GO_Flag"].ToString();
            if (Generaloperation1 == "Y")
            {
                chk_Generaloperation1.Checked = true;
                chk_Generaloperation1_OnCheckedChanged(null, null);

                txtGO_ValueDate.Text = dt.Rows[0]["GO_ValueDate"].ToString();
                //txtGO_Ref_No.Text = dt.Rows[0]["GO_Ref_No"].ToString();
                txtGO_Remark.Text = dt.Rows[0]["GO_Remark"].ToString();
                txtGO_Section.Text = dt.Rows[0]["GO_Section"].ToString();
                txtGO_Comment.Text = dt.Rows[0]["GO_Comment"].ToString();
                txtGO_Memo.Text = dt.Rows[0]["GO_Memo"].ToString();
                txtGO_SchemeNo.Text = dt.Rows[0]["GO_SchemeNo"].ToString();
                //----------------------- Debit ---------------------------------
                txtGO_Debit.SelectedValue = dt.Rows[0]["GO_Debit"].ToString();//Modified By Anand 10-08-2023
                txtGO_Debit_CCY.Text = dt.Rows[0]["GO_Debit_CCY"].ToString();
                txtGO_Debit_Amt.Text = dt.Rows[0]["GO_Debit_Amt"].ToString();
                txtGO_Debit_Cust.Text = dt.Rows[0]["GO_Debit_Cust"].ToString();
                txtGO_Debit_Cust_Name.Text = dt.Rows[0]["GO_Debit_Cust_Name"].ToString();// Added by Anand 10-08-2023
                txtGO_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Debit_Cust_AcCode"].ToString();
                txtGO_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Debit_Cust_AcCode_Name"].ToString();// Added by Anand 10-08-2023
                txtGO_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Debit_Cust_AccNo"].ToString();
                txtGO_Debit_ExchRate.Text = dt.Rows[0]["GO_Debit_ExchRate"].ToString();
                txtGO_Debit_ExchCCY.Text = dt.Rows[0]["GO_Debit_ExchCCY"].ToString();
                txtGO_Debit_Fund.Text = dt.Rows[0]["GO_Debit_Fund"].ToString();
                txtGO_Debit_CheckNo.Text = dt.Rows[0]["GO_Debit_CheckNo"].ToString();
                txtGO_Debit_Available.Text = dt.Rows[0]["GO_Debit_Available"].ToString();
                txtGO_Debit_Advice_Print.Text = dt.Rows[0]["GO_Debit_Advice_Print"].ToString();
                txtGO_Debit_Details.Text = dt.Rows[0]["GO_Debit_Details"].ToString();
                txtGO_Debit_Entity.Text = dt.Rows[0]["GO_Debit_Entity"].ToString();
                txtGO_Debit_Division.Text = dt.Rows[0]["GO_Debit_Division"].ToString();
                txtGO_Debit_InterAmt.Text = dt.Rows[0]["GO_Debit_InterAmt"].ToString();
                txtGO_Debit_InterRate.Text = dt.Rows[0]["GO_Debit_InterRate"].ToString();
                //----------------------- Credit ---------------------------------
                txtGO_Credit.SelectedValue = dt.Rows[0]["GO_Credit"].ToString();// Modified by Anand 10-08-2023
                txtGO_Credit_CCY.Text = dt.Rows[0]["GO_Credit_CCY"].ToString();
                txtGO_Credit_Amt.Text = dt.Rows[0]["GO_Credit_Amt"].ToString();
                txtGO_Credit_Cust.Text = dt.Rows[0]["GO_Credit_Cust"].ToString();
                txtGO_Credit_Cust_Name.Text = dt.Rows[0]["GO_Credit_Cust_Name"].ToString();// Added by Anand 10-08-2023
                txtGO_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Credit_Cust_AcCode"].ToString();
                txtGO_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Credit_Cust_AcCode_Name"].ToString();// Added by Anand 10-08-2023
                txtGO_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Credit_Cust_AccNo"].ToString();
                txtGO_Credit_ExchRate.Text = dt.Rows[0]["GO_Credit_ExchRate"].ToString();
                txtGO_Credit_ExchCCY.Text = dt.Rows[0]["GO_Credit_ExchCCY"].ToString();
                txtGO_Credit_Fund.Text = dt.Rows[0]["GO_Credit_Fund"].ToString();
                txtGO_Credit_CheckNo.Text = dt.Rows[0]["GO_Credit_CheckNo"].ToString();
                txtGO_Credit_Available.Text = dt.Rows[0]["GO_Credit_Available"].ToString();
                txtGO_Credit_Advice_Print.Text = dt.Rows[0]["GO_Credit_Advice_Print"].ToString();
                txtGO_Credit_Details.Text = dt.Rows[0]["GO_Credit_Details"].ToString();
                txtGO_Credit_Entity.Text = dt.Rows[0]["GO_Credit_Entity"].ToString();
                txtGO_Credit_Division.Text = dt.Rows[0]["GO_Credit_Division"].ToString();
                txtGO_Credit_InterAmt.Text = dt.Rows[0]["GO_Credit_InterAmt"].ToString();
                txtGO_Credit_InterRate.Text = dt.Rows[0]["GO_Credit_InterRate"].ToString();
            }
            else
            {
                chk_Generaloperation1.Checked = false;
            }
            // ------------------ Normal General Operations ----------------------
            string Generaloperation2 = dt.Rows[0]["NGO_Flag"].ToString();
            if (Generaloperation2 == "Y")
            {
                chk_Generaloperation2.Checked = true;
                chk_Generaloperation2_OnCheckedChanged(null, null);
                txtNGO_ValueDate.Text = dt.Rows[0]["NGO_ValueDate"].ToString();
                // txtNGO_Ref_No.Text = dt.Rows[0]["NGO_Ref_No"].ToString();
                txtNGO_Remark.Text = dt.Rows[0]["NGO_Remark"].ToString();
                txtNGO_Section.Text = dt.Rows[0]["NGO_Section"].ToString();
                txtNGO_Comment.Text = dt.Rows[0]["NGO_Comment"].ToString();
                txtNGO_Memo.Text = dt.Rows[0]["NGO_Memo"].ToString();
                txtNGO_SchemeNo.Text = dt.Rows[0]["NGO_SchemeNo"].ToString();
                //----------------------- Debit ---------------------------------
                txtNGO_Debit.SelectedValue = dt.Rows[0]["NGO_Debit"].ToString();// Modified by Anand 10-08-2023
                txtNGO_Debit_CCY.Text = dt.Rows[0]["NGO_Debit_CCY"].ToString();
                txtNGO_Debit_Amt.Text = dt.Rows[0]["NGO_Debit_Amt"].ToString();
                txtNGO_Debit_Cust.Text = dt.Rows[0]["NGO_Debit_Cust"].ToString();
                txtNGO_Debit_Cust_Name.Text = dt.Rows[0]["NGO_Debit_Cust_Name"].ToString();// Added by Anand 10-08-2023
                txtNGO_Debit_Cust_AcCode.Text = dt.Rows[0]["NGO_Debit_Cust_AcCode"].ToString();
                txtNGO_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["NGO_Debit_Cust_AcCode_Name"].ToString();// Added by Anand 10-08-2023
                txtNGO_Debit_Cust_AccNo.Text = dt.Rows[0]["NGO_Debit_Cust_AccNo"].ToString();
                txtNGO_Debit_ExchRate.Text = dt.Rows[0]["NGO_Debit_ExchRate"].ToString();
                txtNGO_Debit_ExchCCY.Text = dt.Rows[0]["NGO_Debit_ExchCCY"].ToString();
                txtNGO_Debit_Fund.Text = dt.Rows[0]["NGO_Debit_Fund"].ToString();
                txtNGO_Debit_CheckNo.Text = dt.Rows[0]["NGO_Debit_CheckNo"].ToString();
                txtNGO_Debit_Available.Text = dt.Rows[0]["NGO_Debit_Available"].ToString();
                txtNGO_Debit_Advice_Print.Text = dt.Rows[0]["NGO_Debit_Advice_Print"].ToString();
                txtNGO_Debit_Details.Text = dt.Rows[0]["NGO_Debit_Details"].ToString();
                txtNGO_Debit_Entity.Text = dt.Rows[0]["NGO_Debit_Entity"].ToString();
                txtNGO_Debit_Division.Text = dt.Rows[0]["NGO_Debit_Division"].ToString();
                txtNGO_Debit_InterAmt.Text = dt.Rows[0]["NGO_Debit_InterAmt"].ToString();
                txtNGO_Debit_InterRate.Text = dt.Rows[0]["NGO_Debit_InterRate"].ToString();
                //----------------------- Credit ---------------------------------
                txtNGO_Credit.SelectedValue = dt.Rows[0]["NGO_Credit"].ToString();// Modified by Anand 10-08-2023
                txtNGO_Credit_CCY.Text = dt.Rows[0]["NGO_Credit_CCY"].ToString();
                txtNGO_Credit_Amt.Text = dt.Rows[0]["NGO_Credit_Amt"].ToString();
                txtNGO_Credit_Cust.Text = dt.Rows[0]["NGO_Credit_Cust"].ToString();
                txtNGO_Credit_Cust_Name.Text = dt.Rows[0]["NGO_Credit_Cust_Name"].ToString();// Added by Anand 10-08-2023
                txtNGO_Credit_Cust_AcCode.Text = dt.Rows[0]["NGO_Credit_Cust_AcCode"].ToString();
                txtNGO_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["NGO_Credit_Cust_AcCode_Name"].ToString();// Added by Anand 10-08-2023
                txtNGO_Credit_Cust_AccNo.Text = dt.Rows[0]["NGO_Credit_Cust_AccNo"].ToString();
                txtNGO_Credit_ExchRate.Text = dt.Rows[0]["NGO_Credit_ExchRate"].ToString();
                txtNGO_Credit_ExchCCY.Text = dt.Rows[0]["NGO_Credit_ExchCCY"].ToString();
                txtNGO_Credit_Fund.Text = dt.Rows[0]["NGO_Credit_Fund"].ToString();
                txtNGO_Credit_CheckNo.Text = dt.Rows[0]["NGO_Credit_CheckNo"].ToString();
                txtNGO_Credit_Available.Text = dt.Rows[0]["NGO_Credit_Available"].ToString();
                txtNGO_Credit_Advice_Print.Text = dt.Rows[0]["NGO_Credit_Advice_Print"].ToString();
                txtNGO_Credit_Details.Text = dt.Rows[0]["NGO_Credit_Details"].ToString();
                txtNGO_Credit_Entity.Text = dt.Rows[0]["NGO_Credit_Entity"].ToString();
                txtNGO_Credit_Division.Text = dt.Rows[0]["NGO_Credit_Division"].ToString();
                txtNGO_Credit_InterAmt.Text = dt.Rows[0]["NGO_Credit_InterAmt"].ToString();
                txtNGO_Credit_InterRate.Text = dt.Rows[0]["NGO_Credit_InterRate"].ToString();
            }
            else
            {
                chk_Generaloperation2.Checked = false;
            }
            // ===================== Supriya End ========================
            //------------------------Anand 04-07-2023---------------------------
            string InterOffice = dt.Rows[0]["GO_Acc_Change_Flag"].ToString();
            if (InterOffice == "Y")
            {
                chk_InterOffice.Checked = true;
                chk_InterOffice_OnCheckedChanged(null, null);

                txtIO_ValueDate.Text = dt.Rows[0]["GO_Acc_Change_ValueDate"].ToString();
                txt_GOAccChange_Ref_No.Text = dt.Rows[0]["GO_Acc_Change_TransRef_No"].ToString();
                txt_GOAccChange_Comment.Text = dt.Rows[0]["GO_Acc_Change_Comment"].ToString();
                txt_GOAccChange_SectionNo.Text = dt.Rows[0]["GO_Acc_Change_Section"].ToString();
                txt_GOAccChange_Remarks.Text = dt.Rows[0]["GO_Acc_Change_Remark"].ToString();
                txt_GOAccChange_Memo.Text = dt.Rows[0]["GO_Acc_Change_Memo"].ToString();
                txt_GOAccChange_Scheme_no.Text = dt.Rows[0]["GO_Acc_Change_SchemeNo"].ToString();
                txt_GOAccChange_Debit_Code.Text = dt.Rows[0]["GO_Acc_Change_Debit_Code"].ToString();
                txt_GOAccChange_Debit_Curr.Text = dt.Rows[0]["GO_Acc_Change_Debit_CCY"].ToString();
                txt_GOAccChange_Debit_Amt.Text = dt.Rows[0]["GO_Acc_Change_Debit_Amt"].ToString();
                txt_GOAccChange_Debit_Cust.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_abbr"].ToString();
                txt_GOAccChange_Debit_Cust_Name.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_Name"].ToString();
                txt_GOAccChange_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccCode"].ToString();
                txt_GOAccChange_Debit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccCode_Disc"].ToString();
                txt_GOAccChange_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_Acc_Change_Debit_Cust_AccNo"].ToString();
                txt_GOAccChange_Debit_Exch_Rate.Text = dt.Rows[0]["GO_Acc_Change_Debit_ExchRate"].ToString();
                txt_GOAccChange_Debit_Exch_CCY.Text = dt.Rows[0]["GO_Acc_Change_Debit_ExchCCY"].ToString();
                txt_GOAccChange_Debit_FUND.Text = dt.Rows[0]["GO_Acc_Change_Debit_Fund"].ToString();
                txt_GOAccChange_Debit_Check_No.Text = dt.Rows[0]["GO_Acc_Change_Debit_CheckNo"].ToString();
                txt_GOAccChange_Debit_Available.Text = dt.Rows[0]["GO_Acc_Change_Debit_Available"].ToString();
                txt_GOAccChange_Debit_AdPrint.Text = dt.Rows[0]["GO_Acc_Change_Debit_Advice_Print"].ToString();
                txt_GOAccChange_Debit_Details.Text = dt.Rows[0]["GO_Acc_Change_Debit_Details"].ToString();
                txt_GOAccChange_Debit_Entity.Text = dt.Rows[0]["GO_Acc_Change_Debit_Entity"].ToString();
                txt_GOAccChange_Debit_Division.Text = dt.Rows[0]["GO_Acc_Change_Debit_Division"].ToString();
                txt_GOAccChange_Debit_Inter_Amount.Text = dt.Rows[0]["GO_Acc_Change_Debit_InterAmt"].ToString();
                txt_GOAccChange_Debit_Inter_Rate.Text = dt.Rows[0]["GO_Acc_Change_Debit_InterRate"].ToString();
                txt_GOAccChange_Credit_Code.Text = dt.Rows[0]["GO_Acc_Change_Credit_Code"].ToString();
                txt_GOAccChange_Credit_Curr.Text = dt.Rows[0]["GO_Acc_Change_Credit_CCY"].ToString();
                txt_GOAccChange_Credit_Amt.Text = dt.Rows[0]["GO_Acc_Change_Credit_Amt"].ToString();
                txt_GOAccChange_Credit_Cust.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_abbr"].ToString();
                txt_GOAccChange_Credit_Cust_Name.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_Name"].ToString();
                txt_GOAccChange_Credit_Cust_AcCode.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccCode"].ToString();
                txt_GOAccChange_Credit_Cust_AcCode_Name.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccCode_Disc"].ToString();
                txt_GOAccChange_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_Acc_Change_Credit_Cust_AccNo"].ToString();
                txt_GOAccChange_Credit_Exch_Rate.Text = dt.Rows[0]["GO_Acc_Change_Credit_ExchRate"].ToString();
                txt_GOAccChange_Credit_Exch_Curr.Text = dt.Rows[0]["GO_Acc_Change_Credit_ExchCCY"].ToString();
                txt_GOAccChange_Credit_FUND.Text = dt.Rows[0]["GO_Acc_Change_Credit_Fund"].ToString();
                txt_GOAccChange_Credit_Check_No.Text = dt.Rows[0]["GO_Acc_Change_Credit_CheckNo"].ToString();
                txt_GOAccChange_Credit_Available.Text = dt.Rows[0]["GO_Acc_Change_Credit_Available"].ToString();
                txt_GOAccChange_Credit_AdPrint.Text = dt.Rows[0]["GO_Acc_Change_Credit_Advice_Print"].ToString();
                txt_GOAccChange_Credit_Details.Text = dt.Rows[0]["GO_Acc_Change_Credit_Details"].ToString();
                txt_GOAccChange_Credit_Entity.Text = dt.Rows[0]["GO_Acc_Change_Credit_Entity"].ToString();
                txt_GOAccChange_Credit_Division.Text = dt.Rows[0]["GO_Acc_Change_Credit_Division"].ToString();
                txt_GOAccChange_Credit_Inter_Amount.Text = dt.Rows[0]["GO_Acc_Change_Credit_InterAmt"].ToString();
                txt_GOAccChange_Credit_Inter_Rate.Text = dt.Rows[0]["GO_Acc_Change_Credit_InterRate"].ToString();
            }
            else
            {
                chk_InterOffice.Checked = false;
            }
            //-----------------------------End---------------------------------

            //Fillshippingbill();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewRealisationEntry_Checker.aspx", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewRealisationEntry_Checker.aspx", true);
    }
    protected void txtDocNo_TextChanged(object sender, EventArgs e)
    {
        string docno = txtDocNo.Text;
        //if (docno.Length == 18)
        //{
        getDetails(docno);
        getLastSrNo(docno);
        fillCustomerIdDescription();
        fillOverseasPartyDescription();
        fillIssuingBankDescription();
        txtValueDate.Focus();
    }
    protected void getDetails(string docno)
    {
        string query = "TF_GetDocNoDetailsforExportRealisationNew_Checker";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = docno;
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtForeignORLocal.Text = dt.Rows[0]["ForeignORLocal"].ToString().Trim();

            string _IsManualGR = dt.Rows[0]["IsManualGR"].ToString().Trim();
            if (_IsManualGR == "True")
            {
                chkManualGR.Checked = true;
                lblManualGR.Text = "Yes";
            }
            else
            {
                chkManualGR.Checked = false;
                lblManualGR.Text = "No";
            }

            string bankline = dt.Rows[0]["BankLine"].ToString();
            if (bankline == "Y")
                chkBank.Checked = true;
            else
                chkBank.Checked = false;
            txtDateReceived.Text = dt.Rows[0]["Date_Received"].ToString();
            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            txtDueDate.Text = dt.Rows[0]["Due_Date"].ToString();
            txtDateNegotiated.Text = dt.Rows[0]["Date_Negotiated"].ToString();
            txtOverseasParty.Text = dt.Rows[0]["Overseas_Party_Code"].ToString();
            txtDateDelinked.Text = dt.Rows[0]["Delinked_Date"].ToString();
            txtAcceptedDueDate.Text = dt.Rows[0]["Accepted_Due_Date"].ToString();
            txtOverseasBank.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString();
            string billtype = dt.Rows[0]["Bill_Type"].ToString();
            txtOtherCurrency.Text = dt.Rows[0]["Other_Currency_For_INR"].ToString();
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            txtInvoiceNo.Text = dt.Rows[0]["Invoice_No"].ToString();
            if (dt.Rows[0]["Bill_Amount"].ToString() != "")
                txtNegotiatedAmt.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount"].ToString()).ToString("0.00");
            else
                txtNegotiatedAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            if (dt.Rows[0]["Bill_Amount_In_Rs"].ToString() != "")
                txtNegotiatedAmtinINR.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount_In_Rs"].ToString()).ToString("0.00");
            else
                txtNegotiatedAmtinINR.Text = dt.Rows[0]["Bill_Amount_In_Rs"].ToString();
            txtForwardContract.Text = dt.Rows[0]["Contract_No"].ToString();
            if (dt.Rows[0]["ActBillAmt"].ToString() != "")
                txtBillAmt.Text = Convert.ToDecimal(dt.Rows[0]["ActBillAmt"].ToString()).ToString("0.00");
            else
                txtBillAmt.Text = dt.Rows[0]["ActBillAmt"].ToString();
            if (dt.Rows[0]["ActBillAmt_InRs"].ToString() != "")
                txtBillAmtinINR.Text = Convert.ToDecimal(dt.Rows[0]["ActBillAmt_InRs"].ToString()).ToString("0.00");
            else
                txtBillAmtinINR.Text = dt.Rows[0]["ActBillAmt_InRs"].ToString();
            loan = dt.Rows[0]["Loan"].ToString();
            loan1.Value = dt.Rows[0]["Loan"].ToString();
            if (loan == "Y")
            {
                chkLoanAdvanced.Checked = true;
                lblLoan.Text = "Loan Advanced";
            }
            else
            {
                chkLoanAdvanced.Checked = false;
                lblLoan.Text = "No Loan Advanced";
            }
            libor = dt.Rows[0]["LIBOR"].ToString();
            libor1.Value = dt.Rows[0]["LIBOR"].ToString();
            exchangerate.Value = dt.Rows[0]["Exchange_Rate"].ToString();
            doc_type = dt.Rows[0]["Document_Type"].ToString();
            string interestquery = "TF_GetInterest_Realisation";
            SqlParameter p2 = new SqlParameter("@docno", SqlDbType.VarChar);
            p2.Value = docno;
            TF_DATA Interestdata = new TF_DATA();
            DataTable dt1 = Interestdata.getData(interestquery, p2);
            if (dt1.Rows.Count > 0)
            {
                txtInterestRate2.Text = dt1.Rows[0]["Interest_Rate_2"].ToString();
                txtNoofDays2.Text = dt1.Rows[0]["For_Days_2"].ToString();
            }
            fillReceivingBank(dt.Rows[0]["Currency"].ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Correct Document Number');", true);
            clearAll();
        }
    }
    protected void getLastSrNo(string docno)
    {
        TF_DATA objData = new TF_DATA();
        string query = "TF_GetLastNoForExportRealisation";
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = docno;
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
            txtSrNo.Text = dt.Rows[0]["LastNumber"].ToString();
    }
    protected void fillReceivingBank(string curr)
    {
        ddlAccountType.Items.Clear();
        ddlAccountType.Items.Add("OTHERS");
        ddlAccountType.SelectedItem.Text = "OTHERS";
    }
    protected void fillCustomerIdDescription()
    {
        lblCustDesc.Text = "";
        string custid = txtCustAcNo.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            hdnCustname.Value = lblCustDesc.Text;

            //if (lblCustDesc.Text.Length > 20)
            //{
            //    lblCustDesc.ToolTip = lblCustDesc.Text;
            //    lblCustDesc.Text = lblCustDesc.Text;
            //    lblCustDesc.Text = lblCustDesc.Text.Substring(0, 16) + "...";
            //}
        }
        else
        {
            txtCustAcNo.Text = "";
            lblCustDesc.Text = "";
        }
    }
    protected void fillIssuingBankDescription()
    {
        lblOverseasBank.Text = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtOverseasBank.Text.Trim();

        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
            if (dt.Rows.Count > 0)
            {
                lblOverseasBank.Text = dt.Rows[0]["BankName"].ToString().Trim();
                txtOverseasBankCountry.Text = dt.Rows[0]["Country"].ToString().Trim();
                CountryDescrption("3");
                //txtSwiftCode.Text = dt.Rows[0]["SwiftCode"].ToString().Trim();
                //txt_swiftcode_TextChanged(null, null);
                //txtRemitterCountry_TextChanged(null, null);
                //txtRemBankCountry_TextChanged(null, null);

            }
            else
            {
                txtOverseasBank.Text = "";
                //txtSwiftCode.Text = "";
                lblOverseasBank.Text = "";
                txtOverseasBankCountry.Text = "";
            }

    }
    protected void fillOverseasPartyDescription()
    {
        lblOverseasParty.Text = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = txtOverseasParty.Text;
        string _query = "TF_GetOverseasPartyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasParty.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            txtOverseasPartyCountry.Text = dt.Rows[0]["Party_Country"].ToString().Trim();
            CountryDescrption("1");
            //if (lblOverseasParty.Text.Length > 20)
            //{
            //    lblOverseasParty.ToolTip = lblOverseasParty.Text;
            //    lblOverseasParty.Text = lblOverseasParty.Text.Substring(0, 16) + "...";
            //}
        }
        else
        {
            txtOverseasParty.Text = "";
            lblOverseasParty.Text = "";
        }

    }
    protected void chkFxDls_CheckedChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        if (chkFxDls.Checked == true)
        {
            SqlParameter pdocDate = new SqlParameter("@DocDt", SqlDbType.VarChar);
            pdocDate.Value = txtDateRealised.Text.Trim();

            decimal bal = 0, eefc = 0, crosscur = 0;
            decimal realamt = Convert.ToDecimal(txtAmtRealised.Text.Trim());
            decimal exrate = Convert.ToDecimal(txtExchangeRate.Text.Trim());

            if (txtBalAmt.Text != "")
            {
                bal = Convert.ToDecimal(txtBalAmt.Text.Trim());
            }
            if (txtEEFCAmt.Text != "")
            {
                eefc = Convert.ToDecimal(txtEEFCAmt.Text.Trim());
            }
            if (txtCrossCurrRate.Text != "")
            {
                crosscur = Convert.ToDecimal(txtCrossCurrRate.Text.Trim());
            }

            SqlParameter pINRamt = new SqlParameter("@InrAmt", SqlDbType.VarChar);

            if (bal != 0 && eefc != 0 && crosscur != 0)
            {
                decimal totalamt = realamt * exrate;
                string t = Convert.ToString(totalamt);
                pINRamt.Value = t;
            }
            if (txtEEFCCurrency.Text != "")
            {
                decimal totalamt = (Convert.ToDecimal(txtAmtRealised.Text.Trim()) * Convert.ToDecimal(txtExchangeRate.Text.Trim()));
                string t = Convert.ToString(totalamt);
                pINRamt.Value = t;
            }
            else
                pINRamt.Value = txtBalAmtinINR.Text.Trim();

            string _query = "TF_FXSTAX";
            DataTable dt = objData.getData(_query, pINRamt, pdocDate);
            if (dt.Rows.Count > 0)
            {
                txtFxDlsCommission.Text = dt.Rows[0]["CalFX"].ToString().Trim();
                txtsbfx.Text = dt.Rows[0]["SBcess"].ToString().Trim();
                txt_kkcessonfx.Text = dt.Rows[0]["KKcess"].ToString().Trim();
                txttotcessfx.Text = dt.Rows[0]["TotFXamt"].ToString().Trim();
            }
        }

        else
        {
            txtFxDlsCommission.Text = "";
            txtsbfx.Text = "";
            txt_kkcessonfx.Text = "";
            txttotcessfx.Text = "";

            //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "serviceTax", "calTax();", true);

            chkFxDls.Focus();
        }
    }
    protected void txtValueDate_TextChanged(object sender, EventArgs e)
    {
        string intrate1 = "";
        string intrate2 = txtInterestRate2.Text;
        string intrate3 = "";
        string days1 = "";
        string days2 = txtNoofDays2.Text;
        int ndays2 = 0;
        if (days2 != "")
            ndays2 = Convert.ToInt32(days2);
        int absdays = 0;
        TF_DATA objData = new TF_DATA();
        if (chkLoanAdvanced.Checked == true && txtDateDelinked.Text == "")
        {
            int days = 0;
            string chkinterest = "";
            if (txtValueDate.Text != "" && txtDueDate.Text != "")
            {
                string query = "getdatediff";
                SqlParameter p1 = new SqlParameter("@dt2", SqlDbType.VarChar);
                p1.Value = txtValueDate.Text;
                SqlParameter p2 = new SqlParameter("@dt1", SqlDbType.VarChar);
                p2.Value = txtDueDate.Text;
                DataTable dt = objData.getData(query, p1, p2);
                if (dt.Rows.Count > 0)
                    days = Convert.ToInt32(dt.Rows[0]["Diff"].ToString());
                if (days < 0)
                {
                    lblRefund.Text = "Overdue Interest";
                    chkinterest = "O";
                    absdays = System.Math.Abs(days);
                }
                if (days > 0)
                {
                    lblRefund.Text = "Refund Interest";
                    chkinterest = "R";
                }
                if (days == 0)
                    lblRefund.Text = "";

                intdays.Value = Convert.ToString(days);
            }
        }
        txtValueDate.Focus();
    }
    protected void txtAmtRealised_TextChanged(object sender, EventArgs e)
    {
        if (amtbalforreal.Value == "")
        {
            amtbalforreal.Value = "0";
        }
        if (txtAmtRealised.Text == "")
        {
            txtAmtRealised.Text = "0";
        }

        if (Convert.ToDouble(txtAmtRealised.Text) > Convert.ToDouble(txtBillAmt.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Realised amount cannot be greater than Bill Amount');", true);
        }
        if (Convert.ToDouble(txtAmtRealised.Text) > Convert.ToDouble(amtbalforreal.Value))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message1", "alert('Realised amount cannot be greater than balance amount for realisation');", true);
        }
        rdbTransType.Focus();
        Check_LEI_ThresholdLimit();
    }
    protected void rdbTransType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbTransType.SelectedValue == "CC")
            tran_type.Value = "CC";
        if (rdbTransType.SelectedValue == "PC")
            tran_type.Value = "PC";
        if (rdbTransType.SelectedValue == "INR")
            tran_type.Value = "INR";
        if (rdbTransType.SelectedValue == "FC")
            tran_type.Value = "FC";
    }

    protected void rdbFull_CheckedChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calrealisedAmtinINR();alert('You have selected Full Payment')", true);
        rdbFull.Focus();
    }
    protected void rdbPart_CheckedChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calrealisedAmtinINR();alert('You have selected Part Payment')", true);
        rdbPart.Focus();
    }
    protected void clearAll()
    {
        txtDocNo.Text = "";
        chkBank.Checked = false;
        txtDateReceived.Text = "";
        txtSrNo.Text = "";
        txtCustAcNo.Text = "";
        lblCustDesc.Text = "";
        txtDueDate.Text = "";
        txtDateNegotiated.Text = "";
        txtOverseasParty.Text = "";
        lblOverseasParty.Text = "";
        txtDateDelinked.Text = "";
        txtAcceptedDueDate.Text = "";
        txtOverseasBank.Text = "";
        lblOverseasBank.Text = "";
        //txtBillType.Text = "";
        txtOtherCurrency.Text = "";
        txtCurrency.Text = "";
        txtInvoiceNo.Text = "";
        txtBillAmt.Text = "";
        txtBillAmtinINR.Text = "";
        txtNegotiatedAmt.Text = "";
        txtNegotiatedAmtinINR.Text = "";
        txtDateRealised.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtValueDate.Text = "";
        lblRefund.Text = "";
        txtExchangeRate.Text = "";
        chkLoanAdvanced.Checked = false;
        txtInterestRate2.Text = "";
        txtNoofDays2.Text = "";
        txtForwardContract.Text = "";
        txtAmtRealised.Text = "";
        rdbFull.Checked = false;
        rdbPart.Checked = false;
        txtAmtRealisedinINR.Text = "";
        rdbTransType.SelectedIndex = 1;
        txtEEFCAmt.Text = "";
        txtEEFCCurrency.Text = "";
        txtCrossCurrRate.Text = "";
        txtEEFCAmtTotal.Text = "";
        txtBalAmt.Text = "";
        txtBalAmtinINR.Text = "";
        txtCollectionAmt.Text = "";
        txtCollectionAmtinINR.Text = "";
        txtInterest.Text = "";
        txtInterestinINR.Text = "";
        txtOtherBank.Text = "";
        txtOtherBankinINR.Text = "";
        txtSwift.Text = "";
        //txtTTRefNo.Text = "";
        txtCourier.Text = "";
        chkFxDls.Checked = false;
        txtFxDlsCommission.Text = "";
        txtBankCertificate.Text = "";
        txtServiceTax.Text = "";
        txtCommission.Text = "";
        ddlAccountType.SelectedIndex = 0;
        txtEEFCinINR.Text = "";
        txtNetAmt.Text = "";
        txtRemark.Text = "";
    }
    protected void chkStax_CheckedChanged(object sender, EventArgs e)
    {
        if (chkStax.Checked == true)
        {
            fillTaxRates();
            txtSwift.Focus();
           // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "calTax();", true);
        }
        else
        {
            ddlServicetax.Items.Clear();
          //  ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "calTax();", true);
            txtServiceTax.Text = "";
            txt_kkcessper.Text = "";
            txtsbcess.Text = "";
            txtSBcesssamt.Text = "";
            txt_kkcessamt.Text = "";
            txtsttamt.Text = "";
        }
    }

    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        fillReceivingBank(txtCurrency.Text);
    }
    protected void rdbTransType2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbTransType2.SelectedValue == "PEEFC")
            tran_type2.Value = "PEEFC";
        if (rdbTransType2.SelectedValue == "FEEFC")
            tran_type2.Value = "FEEFC";
    }
    protected void txtOverseasBank_TextChanged(object sender, EventArgs e)
    {
        fillIssuingBankDescription();
    }
    protected void chkFirc_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFirc.Checked == true)
        {
            chkFirc.Text = "Yes";
            txtFircNo.Enabled = true;
            txtFircAdCode.Enabled = true;
        }
        if (chkFirc.Checked == false)
        {
            chkFirc.Text = "No";
            txtFircNo.Text = "";
            txtFircNo.Enabled = false;
            txtFircAdCode.Text = "";
            txtFircAdCode.Enabled = false;
        }

    }
    protected void chkProfitLio_CheckedChanged(object sender, EventArgs e)
    {
        if (chkProfitLio.Checked == true)
        {
            chkProfitLio.Text = "Yes";
            btnprofitlist.Enabled = true;
            txtprofitamt.Enabled = true;
        }
        if (chkProfitLio.Checked == false)
        {
            chkProfitLio.Text = "No";
            txtprofitamt.Text = "0.00";
            txtprofitper.Text = "";
            btnprofitlist.Enabled = false;
            txtprofitamt.Enabled = false;
        }
    }
    protected void btnttamtCheck_Click(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bCode", SqlDbType.VarChar);
        p1.Value = hdnBranchCode.Value;
        SqlParameter p2 = new SqlParameter("@custAcNo", SqlDbType.VarChar);
        p2.Value = txtCustAcNo.Text;
        SqlParameter p3 = new SqlParameter("@ttno", SqlDbType.VarChar);
        p3.Value = hdnittno.Value;
        SqlParameter p4 = new SqlParameter("@ttamt", SqlDbType.VarChar);
        p4.Value = hdnittamt.Value;
        string _query = "TF_CheckTTAmtAvailable";
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Enter Amount Less Or Equal To Balance Amount Available In ITT Ref. No. " + hdnittno.Value + "');", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "document.getElementById('" + hdnittamtid.Value + "').value = '0.00';", true);
        }
    }
    protected void txtTTRefNo_TextChanged(object sender, EventArgs e)
    {
        if (txtTTRefNo1.Text.Trim() == "")
        {
            txtTotTTAmt1.Text = "";
            txtBalTTAmt1.Text = "";
            txtTTAmount1.Text = "";
        }

        if (txtTTRefNo2.Text.Trim() == "")
        {
            txtTotTTAmt2.Text = "";
            txtBalTTAmt2.Text = "";
            txtTTAmount2.Text = "";
        }

        if (txtTTRefNo3.Text.Trim() == "")
        {
            txtTotTTAmt3.Text = "";
            txtBalTTAmt3.Text = "";
            txtTTAmount3.Text = "";
        }

        if (txtTTRefNo4.Text.Trim() == "")
        {
            txtTotTTAmt4.Text = "";
            txtBalTTAmt4.Text = "";
            txtTTAmount4.Text = "";
        }

        if (txtTTRefNo5.Text.Trim() == "")
        {
            txtTotTAmt5.Text = "";
            txtBalTTAmt5.Text = "";
            txtTTAmount5.Text = "";
        }

        if (txtTTRefNo1.Text.Trim() == "" && txtTTRefNo2.Text.Trim() == "" && txtTTRefNo3.Text.Trim() == "" && txtTTRefNo4.Text.Trim() == "" && txtTTRefNo5.Text.Trim() == "")
        {
            fillReceivingBank(txtCurrency.Text);
        }
        else
        {
            ddlAccountType.SelectedValue = "OTHERS";
        }

        if (txtTTRefNo1.Text.Trim() == "")
        {
            txtTotTTAmt1.Text = "";
            txtBalTTAmt1.Text = "";
            txtTTAmount1.Text = "";
        }
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
            else if (txtOverseasParty.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Overseas Party for Verifying LEI details.')", true);
                txtOverseasParty.Focus();
            }
            else if (txtAmtRealised.Text.Trim() == "" || txtAmtRealised.Text.Trim() == "0.00")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Amount for Verifying LEI details.')", true);
                txtAmtRealised.Focus();
            }
            else if (txt_relcur.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Realised Currency for Verifying LEI details.')", true);
                txt_relcur.Focus();
            }
            else if (txtDateRealised.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Realised date for Verifying LEI details.')", true);
                txtDateRealised.Focus();
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

                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                dateInfo.ShortDatePattern = "dd/MM/yyyy";
                DateTime LEIEffectDate = Convert.ToDateTime(lblLEIEffectDate.Text.Trim(), dateInfo);
                DateTime LEIValueDate = Convert.ToDateTime(txtValueDate.Text.Trim(), dateInfo);
                String NotverifyMSG = "";
                if (LEIEffectDate <= LEIValueDate)
                {
                    if (hdncustlei.Value == "" || hdncustleiexpiryRe.Value == "")  //// remove as per user suggesions /  || hdnoverseaslei.Value == "" || hdnoverseasleiexpiryRe.Value == ""
                    {
                        
                        LEIAmtCheck.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit. ";
                        LEIverify.Text = "Please Verify LEI.";
                        NotverifyMSG = "This transaction cannot proceed because the LEI Details has not been verified.";
                        hdnValidateLei.Value = "LeiFalse";
                        //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This transaction cannot proceed because the LEI Details has not been verified.')", true);
                    }
                    else
                    {
                        NotverifyMSG = "";
                        hdnValidateLei.Value = "";
                    }
                }

                if (LEIAmtCheck.Text != "")
                {
                    LEIverify.Text = "";
                    LEIverify.Text = "LEI checked.";
                    LEIverify.ForeColor = System.Drawing.Color.LimeGreen;
                }

                String LEIMSG = @"Customer LEI : " + CustLEINo + "\\n" + "Customer LEI Expiry : " + CustLEIExpiry + "\\n" + "Applicant LEI : " + ApplicantLEINo + "\\n" + "Applicant LEI Expiry : " + ApplicantLEIExpiry + "\\n" + NotverifyMSG;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "messege", "alert('" + LEIMSG + "')", true);
            }
        }
        catch (Exception ex)
        {

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
                hdncustlei.Value = "";
            }
            lblLEI_CUST_Remark.Visible = false;
            SpanLei1.Visible = false;
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
            SqlParameter p2 = new SqlParameter("@DueDate", txtValueDate.Text.ToString().Trim());
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
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_CUST_Remark.Text = " " + "...Not Verified.";
                lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Red;
                hdncustleiexpiry.Value = "";
                hdncustleiexpiryRe.Value = "";
            }
            lblLEIExpiry_CUST_Remark.Visible = false;
            SpanLei2.Visible = false;
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

            SqlParameter p1 = new SqlParameter("@PartyCode", txtOverseasParty.Text.Trim());
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
            lblLEI_Overseas_Remark.Visible = false;
            SpanLei3.Visible = false;
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

            SqlParameter p1 = new SqlParameter("@PartyCode", txtOverseasParty.Text.Trim());
            SqlParameter p2 = new SqlParameter("@DueDate", txtDateRealised.Text.ToString().Trim());
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
            lblLEIExpiry_Overseas_Remark.Visible = false;
            SpanLei4.Visible = false;
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
            string _ForeignOrLocal = txtForeignORLocal.Text;

            if (_ForeignOrLocal == "F")
            {
                TF_DATA objData = new TF_DATA();
                SqlParameter p1 = new SqlParameter("@CurrCode", SqlDbType.VarChar);
                SqlParameter p2 = new SqlParameter("@Date", txtValueDate.Text);
                p1.Value = txt_relcur.Text.ToString();
                string _query = "TF_EXP_GetLEI_RateCardDetails";
                DataTable dt = objData.getData(_query, p1, p2);
                string Exch_rate = "";
                if (dt.Rows.Count > 0)
                {
                    Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                }
                string txtExch = txtExchangeRate.Text.ToString();
                if (txtExch != "" && txtExch != "1.0000000000" && txtExch != "0.0000000000")
                {
                    Exch_rate = txtExchangeRate.Text;
                }
                hdnleiExchRate.Value = Exch_rate;
                LEIAmtCheck.Text = "";
                string result = "";
                string txtbillamt = txtAmtRealised.Text;
                string exchange_Rate = Exch_rate;

                if ((txtbillamt != "0" && txtbillamt != "") && (exchange_Rate != "0" && exchange_Rate != ""))
                {
                    if (txtbillamt != "0.00" && exchange_Rate != "0.0000000000")
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
                            if (Request.QueryString["mode"].ToString() == "add")
                            {
                                LEIverify.Text = "Please Verify LEI.";
                                LEIverify.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (Request.QueryString["mode"].ToString() == "edit")
                            {
                                if (Leicount == 0)
                                {
                                    LEIverify.Text = "LEI Verified.";
                                    LEIverify.ForeColor = System.Drawing.Color.Green;
                                }
                                else
                                {
                                    LEIverify.Text = "Please Verify LEI.";
                                    LEIverify.ForeColor = System.Drawing.Color.Red;
                                }
                                Leicount = Leicount + 1;
                            }

                            LEIAmtCheck.ForeColor = System.Drawing.Color.Red;
                            LEIAmtCheck.Font.Size = 13;
                            hdnLeiFlag.Value = "Y";
                            hdnbillamtinr.Value = dtLimit.Rows[0]["billamtInr"].ToString().Trim();
                            //Check_CUST_LEINODetails();
                            //Check_CUST_LEINO_ExpirydateDetails();
                            //Check_Overseas_LEINODetails();
                            //Check_Overseas_LEINO_ExpirydateDetails();

                            //SpanLei1.Visible = true; SpanLei2.Visible = true; SpanLei3.Visible = true; SpanLei4.Visible = true;


                        }
                        else if (dtLimit.Rows[0]["checkstaus"].ToString() == "less")
                        {
                            btnLEI.Visible = false;
                            btnSave.Visible = true;
                            btnSavePrint.Visible = true;
                            LEIAmtCheck.Text = "Transaction Bill Amt is less than LEI Thresold Limit. ";
                            LEIAmtCheck.ForeColor = System.Drawing.Color.Green;
                            LEIAmtCheck.Font.Size = 13;
                            hdnLeiFlag.Value = "N";
                            hdnbillamtinr.Value = dtLimit.Rows[0]["billamtInr"].ToString().Trim();
                            hdncustlei.Value = hdncustleiexpiry.Value = hdnoverseaslei.Value = hdnoverseasleiexpiry.Value = "";
                            lblLEI_CUST_Remark.Text = lblLEIExpiry_CUST_Remark.Text = lblLEI_Overseas_Remark.Text = lblLEIExpiry_Overseas_Remark.Text = "";
                            lblLEI_CUST_Remark.Visible = false; lblLEIExpiry_CUST_Remark.Visible = false; lblLEI_Overseas_Remark.Visible = false; lblLEIExpiry_Overseas_Remark.Visible = false;
                            SpanLei1.Visible = false; SpanLei2.Visible = false; SpanLei3.Visible = false; SpanLei4.Visible = false;
                        }
                        Check_LEI_SpecialFlag();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void txt_relcur_TextChanged(object sender, EventArgs e)
    {
        Check_LEI_ThresholdLimit();
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
            SqlParameter p3 = new SqlParameter("@Trans_Status", "S");
            string _query = "TF_EXP_GetLEISpecial_Customer";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                hdnLeiSpecialFlag.Value = "R";

                //Check_CUST_LEINODetails();
                //Check_CUST_LEINO_ExpirydateDetails();
                //Check_Overseas_LEINODetails();
                //Check_Overseas_LEINO_ExpirydateDetails();

                //SpanLei1.Visible = true; SpanLei2.Visible = true; SpanLei3.Visible = true; SpanLei4.Visible = true; SpanLei5.Visible = true;
                ReccuringLEI.Visible = true;
                ReccuringLEI.Text = "This is Recurring LEI Customer.";
                ReccuringLEI.ForeColor = System.Drawing.Color.Red;

                btnLEI.Visible = true;
                if (Request.QueryString["mode"].ToString() == "add")
                {
                    if (hdnLeiFlag.Value == "N")
                    {
                        LEIverify.Text = "";
                        LEIverify.Text = "Please Verify LEI.";
                        LEIverify.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (Request.QueryString["mode"].ToString() == "edit")
                {
                    if (hdnLeiFlag.Value == "N")
                    {
                        if (Leicount == 0)
                        {
                            LEIverify.Text = "";
                            LEIverify.Text = "LEI Verified.";
                            LEIverify.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            LEIverify.Text = "";
                            LEIverify.Text = "Please Verify LEI.";
                            LEIverify.ForeColor = System.Drawing.Color.Red;
                            btnLEI.Visible = true;
                        }
                        Leicount = Leicount + 1;
                    }
                }

                //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                //dateInfo.ShortDatePattern = "dd/MM/yyyy";
                //DateTime LEIEffectDate = Convert.ToDateTime(lblLEIEffectDate.Text.Trim(), dateInfo);
                //DateTime LEIValueDate = Convert.ToDateTime(txtValueDate.Text.Trim(), dateInfo);

                //if (LEIEffectDate <= LEIValueDate)
                //{
                //    if (hdncustlei.Value == "" || hdncustleiexpiryRe.Value == "")  //// remove as per user suggesions / || hdnoverseaslei.Value == "" || hdnoverseasleiexpiryRe.Value == "" 
                //    {
                //        btnLEI.Visible = true;
                //        btnSave.Visible = false;
                //        btnSavePrint.Visible = false;
                //        if (hdnLeiFlag.Value == "N")
                //        {
                //            LEIverify.Text = "";
                //            LEIverify.Text = "Please Verify LEI.";
                //            LEIverify.ForeColor = System.Drawing.Color.Red;
                //        }
                //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This transaction cannot proceed because the LEI Details has not been verified.')", true);
                //    }
                //    else
                //    {
                //        btnSave.Visible = true;
                //        btnSavePrint.Visible = true;
                //    }
                //}
            }
            else
            {
                ReccuringLEI.Text = "";
                ReccuringLEI.Visible = false;
                //SpanLei5.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    //----------------------------------------------------Nilesh chngeing start----------------------------------------------
    protected void Fillshippingbill()
    {
        string doc = txtDocNo.Text.Trim();
        SqlParameter p1 = new SqlParameter("@Document_No", doc);
        SqlParameter p2 = new SqlParameter("@mode", hdnmode.Value);
        SqlParameter p3 = new SqlParameter("@SrNo", txtSrNo.Text);
        TF_DATA objsave = new TF_DATA();
        DataTable dt = objsave.getData("TF_EXP_filldetailsShippingbill_Checker", p1, p2, p3);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewGRPPCustomsDetails.DataSource = dt.DefaultView;
            GridViewGRPPCustomsDetails.DataBind();
            GridViewGRPPCustomsDetails.Visible = true;

            labelMessage.Text = "";
            labelMessage.Visible = false;
            lblPeningSB.Visible = true;
            chkSB.Visible = true;
            lblSB.Visible = true;
        }
        else
        {
            GridViewGRPPCustomsDetails.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
            lblPeningSB.Visible = true;
            chkSB.Visible = true;
            lblSB.Visible = true;
        }
    }
    private void fillReimbBankDescription()
    {
        //lblReimbBankDesc.Text = "";
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        //p1.Value = txtReimbBank.Text;
        //string _query = "TF_GetReimburseBankMasterDetails";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{
        //    lblReimbBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();

        //    if (lblReimbBankDesc.Text.Length > 20)
        //    {
        //        lblReimbBankDesc.ToolTip = lblReimbBankDesc.Text;
        //        lblReimbBankDesc.Text = lblReimbBankDesc.Text.Substring(0, 20) + "...";

        //    }

        //    txtSpecialInstructions1.Text = dt.Rows[0]["Special_Instructions1"].ToString().Trim();
        //    txtSpecialInstructions2.Text = dt.Rows[0]["Special_Instructions2"].ToString().Trim();
        //    txtSpecialInstructions3.Text = dt.Rows[0]["Special_Instructions3"].ToString().Trim();
        //    txtSpecialInstructions4.Text = dt.Rows[0]["Special_Instructions4"].ToString().Trim();
        //    txtSpecialInstructions5.Text = dt.Rows[0]["Special_Instructions5"].ToString().Trim();
        //    txtSpecialInstructions6.Text = dt.Rows[0]["Special_Instructions6"].ToString().Trim();
        //    txtSpecialInstructions7.Text = dt.Rows[0]["Special_Instructions7"].ToString().Trim();
        //    txtSpecialInstructions8.Text = dt.Rows[0]["Special_Instructions8"].ToString().Trim();
        //    txtSpecialInstructions9.Text = dt.Rows[0]["Special_Instructions9"].ToString().Trim();
        //    txtSpecialInstructions10.Text = dt.Rows[0]["Special_Instructions10"].ToString().Trim();
        //}
        //else
        //{
        //    txtReimbBank.Text = "";
        //    lblReimbBankDesc.Text = "";

        //    txtSpecialInstructions1.Text = "";
        //    txtSpecialInstructions2.Text = "";
        //    txtSpecialInstructions3.Text = "";
        //    txtSpecialInstructions4.Text = "";
        //    txtSpecialInstructions5.Text = "";
        //    txtSpecialInstructions6.Text = "";
        //    txtSpecialInstructions7.Text = "";
        //    txtSpecialInstructions8.Text = "";
        //    txtSpecialInstructions9.Text = "";
        //    txtSpecialInstructions10.Text = "";

        //    txtReimbBank.Focus();
        //}

    }
    protected void CountryDescrption(string ID)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", txtOverseasPartyCountry.Text.Trim());
        SqlParameter p2 = new SqlParameter("@cid", txtconsigneePartyCountry.Text.Trim());
        SqlParameter p3 = new SqlParameter("@cid", txtOverseasBankCountry.Text.Trim());

        if (ID == "1")
        {
            DataTable dt = objData.getData("TF_GetCountryDetails", p1);
            if (dt.Rows.Count > 0)
            {
                lblOverseasPartyCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
            }
            else
            {
                lblOverseasPartyCountryDesc.Text = "";
            }
        }
        if (ID == "2")
        {
            DataTable dt = objData.getData("TF_GetCountryDetails", p2);
            if (dt.Rows.Count > 0)
            {
                lblconsigneePartyCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
            }
            else
            {
                lblconsigneePartyCountryDesc.Text = "";
            }
        }
        if (ID == "3")
        {
            DataTable dt = objData.getData("TF_GetCountryDetails", p3);
            if (dt.Rows.Count > 0)
            {
                lblOverseasBankCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
            }
            else
            {
                lblOverseasBankCountryDesc.Text = "";
            }
        }

    }
    private void fillConsigneePartyDescription()
    {
        lblConsigneePartyDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = txtconsigneePartyID.Text;

        string _query = "TF_GetConsigneePartyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblConsigneePartyDesc.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            txtconsigneePartyCountry.Text = dt.Rows[0]["Party_Country"].ToString().Trim();
            CountryDescrption("2");
        }
        else
        {
            txtconsigneePartyID.Text = "";
            lblConsigneePartyDesc.Text = "";
            txtconsigneePartyCountry.Text = "";
        }

    }
    protected void txtReimbBank_TextChanged(object sender, EventArgs e)
    {
        fillReimbBankDescription();
    }
    private void fillPayingBankDescription()
    {
        //lblPayingBankDesc.Text = "";
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        //p1.Value = txtPayingBankID.Text;
        //string _query = "TF_GetOverseasBankMasterDetails";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{
        //    lblPayingBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
        //    if (lblPayingBankDesc.Text.Length > 25)
        //    {
        //        lblPayingBankDesc.ToolTip = lblPayingBankDesc.Text;
        //        lblPayingBankDesc.Text = lblPayingBankDesc.Text.Substring(0, 25) + "...";
        //    }
        //}
        //else
        //{
        //    txtPayingBankID.Text = "";
        //    lblPayingBankDesc.Text = "";
        //}
    }
    protected void txtPayingBankID_TextChanged(object sender, EventArgs e)
    {
        fillPayingBankDescription();
    }
    protected void txtCRCustAbbr_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        //p1.Value = txtCRCustAbbr.Text.Trim();
        //string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{

        //    txtCRGLCode.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
        //    txtCRCustAcNo1.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
        //    txtCRCustAcNo2.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
        //    txtCRCurr.Focus();
        //}
        //else
        //{

        //    txtCRCustAbbr.Focus();
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        //}
    }
    protected void txtDRCustAbbr1_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        //p1.Value = txtDRCustAbbr1.Text.Trim();
        //string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{

        //    txtDRGLCode1.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
        //    txtDRCustAcNo11.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
        //    txtDRCustAcNo12.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
        //    txtDRCurr1.Focus();
        //}
        //else
        //{

        //    txtDRCustAbbr1.Focus();
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        //}
    }
    protected void txtDRCustAbbr2_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        //p1.Value = txtDRCustAbbr2.Text.Trim();
        //string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{

        //    txtDRGLCode2.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
        //    txtDRCustAcNo21.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
        //    txtDRCustAcNo22.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
        //    txtDRCurr2.Focus();
        //}
        //else
        //{

        //    txtDRCustAbbr2.Focus();
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        //}
    }
    protected void txtDRCustAbbr3_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        //p1.Value = txtDRCustAbbr3.Text.Trim();
        //string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{

        //    txtDRGLCode3.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
        //    txtDRCustAcNo31.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
        //    txtDRCustAcNo32.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
        //    txtDRCurr3.Focus();
        //}
        //else
        //{

        //    txtDRCustAbbr3.Focus();
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        //}
    }
    protected void txtDRCustAbbr4_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        //p1.Value = txtDRCustAbbr4.Text.Trim();
        //string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{

        //    txtDRGLCode4.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
        //    txtDRCustAcNo41.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
        //    txtDRCustAcNo42.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
        //    txtDRCurr4.Focus();
        //}
        //else
        //{
        //    txtDRCustAbbr4.Focus();
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        //}
    }
    protected void txtDRCustAbbr5_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        //p1.Value = txtDRCustAbbr5.Text.Trim();
        //string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{
        //    txtDRGLCode5.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
        //    txtDRCustAcNo51.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
        //    txtDRCustAcNo52.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
        //    txtDRCurr5.Focus();
        //}
        //else
        //{
        //    txtDRCustAbbr5.Focus();
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        //}
    }
    private void disable()
    {
        //-------------Document details-------------------
        txtDocNo.Enabled = false;
        btnDocNo.Enabled = false;
        txtDateReceived.Enabled = false;
        txtSrNo.Enabled = false;
        chkBank.Enabled = false;
        txtCustAcNo.Enabled = false;
        txtDueDate.Enabled = false;
        txtDateNegotiated.Enabled = false;
        txtOverseasParty.Enabled = false;
        txtDateDelinked.Enabled = false;
        txtAcceptedDueDate.Enabled = false;
        txtOverseasBank.Enabled = false;
        //btnOverseasBankList.Enabled = false;
        txtSwiftCode.Enabled = false;
        txtOtherCurrency.Enabled = false;
        txtInvoiceNo.Enabled = false;
        txtBillAmt.Enabled = false;
        txtBillAmtinINR.Enabled = false;
        txtNegotiatedAmt.Enabled = false;
        txtNegotiatedAmtinINR.Enabled = false;
        txtForeignORLocal.Enabled = false;
        //-------------Transaction details-------------------
        chkFirc.Enabled = false;
        txtFircNo.Enabled = false;
        txtFircAdCode.Enabled = false;
        txtDateRealised.Enabled = false;
        txtValueDate.Enabled = false;
        txtCurrency.Enabled = false;
        txt_relcur.Enabled = false;
        btn_recurrhelp.Enabled = false;
        txt_relamount.Enabled = false;
        txtRelCrossCurRate.Enabled = false;
        txtExchangeRate.Enabled = false;
        txtInterestRate2.Enabled = false;
        txtNoofDays2.Enabled = false;
        chkLoanAdvanced.Enabled = false;
        txtAmtRealised.Enabled = false;
        rdbFull.Enabled = false;
        rdbPart.Enabled = false;
        txtAmtRealisedinINR.Enabled = false;
        chkManualGR.Enabled = false;
        rdbTransType.Enabled = false;
        txtForwardContract.Enabled = false;
        rdbTransType2.Enabled = false;
        txtPartConAmt.Enabled = false;
        txtConCrossCur.Enabled = false;
        btnOtrCrossCur.Enabled = false;
        txtConCurRate.Enabled = false;
        txtTotConRate.Enabled = false;
        txtEEFCAmt.Enabled = false;
        txtEEFCCurrency.Enabled = false;
        txtCrossCurrRate.Enabled = false;
        txtEEFCAmtTotal.Enabled = false;
        txtBalAmt.Enabled = false;
        txtBalAmtinINR.Enabled = false;
        txtCollectionAmt.Enabled = false;
        txtCollectionAmtinINR.Enabled = false;
        txtInterest.Enabled = false;
        txtInterestinINR.Enabled = false;
        txtOtherBank.Enabled = false;
        txtOtherBankinINR.Enabled = false;
        txt_fbkcharges.Enabled = false;
        txt_fbkchargesinRS.Enabled = false;
        chkStax.Enabled = false;
        ddlServicetax.Enabled = false;
        txtServiceTax.Enabled = false;
        txtsbcess.Enabled = false;
        txtSBcesssamt.Enabled = false;
        txt_kkcessper.Enabled = false;
        txtsttamt.Enabled = false;
        txtSwift.Enabled = false;
        txtPcfcAmt.Enabled = false;
        txtCourier.Enabled = false;
        txtOverDue.Enabled = false;
        chkFxDls.Enabled = false;
        txtFxDlsCommission.Enabled = false;
        txtsbfx.Enabled = false;
        txt_kkcessonfx.Enabled = false;
        txttotcessfx.Enabled = false;
        chkProfitLio.Enabled = false;
        txtprofitper.Enabled = false;
        btnprofitlist.Enabled = false;
        txtprofitamt.Enabled = false;
        txtBankCertificate.Enabled = false;
        txtCommissionID.Enabled = false;
        btnCommissionList.Enabled = false;
        txtCommission.Enabled = false;
        ddlAccountType.Enabled = false;
        txtEEFCinINR.Enabled = false;
        txtNetAmt.Enabled = false;
        txtRemark.Enabled = false;
        txtTTRefNo1.Enabled = false;
        btnTTRef1.Enabled = false;
        txtTotTTAmt1.Enabled = false;
        txtBalTTAmt1.Enabled = false;
        txtTTAmount1.Enabled = false;
        txtTTRefNo2.Enabled = false;
        btnTTRef2.Enabled = false;
        txtTotTTAmt2.Enabled = false;
        txtBalTTAmt2.Enabled = false;
        txtTTAmount2.Enabled = false;
        txtTTRefNo3.Enabled = false;
        btnTTRef3.Enabled = false;
        txtTotTTAmt3.Enabled = false;
        txtBalTTAmt3.Enabled = false;
        txtTTAmount3.Enabled = false;
        txtTTRefNo4.Enabled = false;
        btnTTRef4.Enabled = false;
        txtTotTTAmt4.Enabled = false;
        txtBalTTAmt4.Enabled = false;
        txtTTAmount4.Enabled = false;
        txtTTRefNo5.Enabled = false;
        btnTTRef5.Enabled = false;
        txtTotTAmt5.Enabled = false;
        txtBalTTAmt5.Enabled = false;
        txtTTAmount5.Enabled = false;
        //-------------G-Base details-------------------
        //txtOperationType.Enabled = false;
        //txtSettlementOption.Enabled = false;
        //txtRiskCountry.Enabled = false;
        //rdbShipper.Enabled = false;
        //rdbBuyer.Enabled = false;
        //txtFundType.Enabled = false;
        //txtBaseRate.Enabled = false;
        //txtGradeCode.Enabled = false;
        //txtDirection.Enabled = false;
        //txtCovrInstr.Enabled = false;
        //txtInternalRate.Enabled = false;
        //txtSpread.Enabled = false;
        //txtApplNo.Enabled = false;
        //ddlRemEUC.Enabled = false;
        //txtDraftNo.Enabled = false;
        //txtRiskCustomer.Enabled = false;
        //txtVesselName.Enabled = false;
        //ddlInstructions.Enabled = false;
        //txtReimbBank.Enabled = false;
        //btnReimbBank.Enabled = false;
        //txtGBaseRemarks.Enabled = false;
        //txtMerchandise.Enabled = false;
        //txtPayingBankID.Enabled = false;
        //txtminrefno.Enabled = false;
        //btnPayingBankList.Enabled = false;
        //txtSpecialInstructions1.Enabled = false;
        //txtSpecialInstructions2.Enabled = false;
        //txtSpecialInstructions3.Enabled = false;
        //txtSpecialInstructions4.Enabled = false;
        //txtSpecialInstructions5.Enabled = false;
        //txtSpecialInstructions6.Enabled = false;
        //txtSpecialInstructions7.Enabled = false;
        //txtSpecialInstructions8.Enabled = false;
        //txtSpecialInstructions9.Enabled = false;
        //txtSpecialInstructions10.Enabled = false;
        //txtPrincipalContractNo1.Enabled = false;
        //txtPrincipalContractNo2.Enabled = false;
        //txtPrincipalExchCurr.Enabled = false;
        //txtPrincipalExchRate.Enabled = false;
        //txtPrincipalIntExchRate.Enabled = false;
        //txtInterestLump.Enabled = false;
        //txtInterestContractNo1.Enabled = false;
        //txtInterestContractNo2.Enabled = false;
        //txtInterestExchCurr.Enabled = false;
        //txtInterestExchRate.Enabled = false;
        //txtInterestIntExchRate.Enabled = false;
        //txtCommissionMatu.Enabled = false;
        //txtCommissionContractNo1.Enabled = false;
        //txtCommissionContractNo2.Enabled = false;
        //txtCommissionExchCurr.Enabled = false;
        //txtCommissionExchRate.Enabled = false;
        //txtCommissionIntExchRate.Enabled = false;
        ////-----------------------------1st cr-----------------------------
        //txtCRGLCode.Enabled = false;
        //txtCRCustAbbr.Enabled = false;
        //txtCRCustAcNo1.Enabled = false;
        //txtCRCustAcNo2.Enabled = false;
        //txtCRCurr.Enabled = false;
        //txtCRAmount.Enabled = false;
        //txtCRIntCurr.Enabled = false;
        //txtCRIntAmount.Enabled = false;
        //txtCRIntPayer.Enabled = false;
        //txtCRPaymentCommCurr.Enabled = false;
        //txtCRPaymentCommAmount.Enabled = false;
        //txtCRPaymentCommPayer.Enabled = false;
        //txtCRHandlingCommCurr.Enabled = false;
        //txtCRHandlingCommAmount.Enabled = false;
        //txtCRHandlingCommPayer.Enabled = false;
        //txtCRPostageCurr.Enabled = false;
        //txtCRPostageAmount.Enabled = false;
        //txtCRPostagePayer.Enabled = false;
        //txtCRCurr1.Enabled = false;
        //txtCRAmount1.Enabled = false;
        //txtCRPayer1.Enabled = false;
        ////-----------------------------2nd cr-----------------------------
        //txtCRGLCode21.Enabled = false;
        //txtCRCustAbbr21.Enabled = false;
        //txtCRCustAcNo121.Enabled = false;
        //txtCRCustAcNo221.Enabled = false;
        //txtCRCurr21.Enabled = false;
        //txtCRAmount21.Enabled = false;
        //txtCRIntCurr21.Enabled = false;
        //txtCRIntAmount21.Enabled = false;
        //txtCRIntPayer21.Enabled = false;
        //txtCRPaymentCommCurr21.Enabled = false;
        //txtCRPaymentCommAmount21.Enabled = false;
        //txtCRPaymentCommPayer21.Enabled = false;
        //txtCRHandlingCommCurr21.Enabled = false;
        //txtCRHandlingCommAmount21.Enabled = false;
        //txtCRHandlingCommPayer21.Enabled = false;
        //txtCRPostageCurr21.Enabled = false;
        //txtCRPostageAmount21.Enabled = false;
        //txtCRPostagePayer21.Enabled = false;
        //txtCRCurr22.Enabled = false;
        //txtCRAmount22.Enabled = false;
        //txtCRPayer22.Enabled = false;
        ////-----------------------------3rd cr-----------------------------
        //txtCRGLCode31.Enabled = false;
        //txtCRCustAbbr31.Enabled = false;
        //txtCRCustAcNo131.Enabled = false;
        //txtCRCustAcNo231.Enabled = false;
        //txtCRCurr31.Enabled = false;
        //txtCRAmount31.Enabled = false;
        //txtCRIntCurr31.Enabled = false;
        //txtCRIntAmount31.Enabled = false;
        //txtCRIntPayer31.Enabled = false;
        //txtCRPaymentCommCurr31.Enabled = false;
        //txtCRPaymentCommAmount31.Enabled = false;
        //txtCRPaymentCommPayer31.Enabled = false;
        //txtCRHandlingCommCurr31.Enabled = false;
        //txtCRHandlingCommAmount31.Enabled = false;
        //txtCRHandlingCommPayer31.Enabled = false;
        //txtCRPostageCurr31.Enabled = false;
        //txtCRPostageAmount31.Enabled = false;
        //txtCRPostagePayer31.Enabled = false;
        //txtCRCurr32.Enabled = false;
        //txtCRAmount32.Enabled = false;
        //txtCRPayer32.Enabled = false;
        ////-----------------------------4th cr-----------------------------
        //txtCRGLCode41.Enabled = false;
        //txtCRCustAbbr41.Enabled = false;
        //txtCRCustAcNo141.Enabled = false;
        //txtCRCustAcNo241.Enabled = false;
        //txtCRCurr41.Enabled = false;
        //txtCRAmount41.Enabled = false;
        //txtCRIntCurr41.Enabled = false;
        //txtCRIntAmount41.Enabled = false;
        //txtCRIntPayer41.Enabled = false;
        //txtCRPaymentCommCurr41.Enabled = false;
        //txtCRPaymentCommAmount41.Enabled = false;
        //txtCRPaymentCommPayer41.Enabled = false;
        //txtCRHandlingCommCurr41.Enabled = false;
        //txtCRHandlingCommAmount41.Enabled = false;
        //txtCRHandlingCommPayer41.Enabled = false;
        //txtCRPostageCurr41.Enabled = false;
        //txtCRPostageAmount41.Enabled = false;
        //txtCRPostagePayer41.Enabled = false;
        //txtCRCurr42.Enabled = false;
        //txtCRAmount42.Enabled = false;
        //txtCRPayer42.Enabled = false;
        ////----------------------------- DR start-----------------------------
        //txtDRCurr.Enabled = false;
        //txtDRAmount.Enabled = false;
        //txtDRGLCode1.Enabled = false;
        //txtDRCustAbbr1.Enabled = false;
        //txtDRCustAcNo11.Enabled = false;
        //txtDRCustAcNo12.Enabled = false;
        //txtDRCurr1.Enabled = false;
        //txtDRAmount1.Enabled = false;
        //txtDRGLCode2.Enabled = false;
        //txtDRCustAbbr2.Enabled = false;
        //txtDRCustAcNo21.Enabled = false;
        //txtDRCustAcNo22.Enabled = false;
        //txtDRCurr2.Enabled = false;
        //txtDRAmount2.Enabled = false;
        //txtDRGLCode3.Enabled = false;
        //txtDRCustAbbr3.Enabled = false;
        //txtDRCustAcNo31.Enabled = false;
        //txtDRCustAcNo32.Enabled = false;
        //txtDRCurr3.Enabled = false;
        //txtDRAmount3.Enabled = false;
        //txtDRGLCode4.Enabled = false;
        //txtDRCustAbbr4.Enabled = false;
        //txtDRCustAcNo41.Enabled = false;
        //txtDRCustAcNo42.Enabled = false;
        //txtDRCurr4.Enabled = false;
        //txtDRAmount4.Enabled = false;
        //txtDRGLCode5.Enabled = false;
        //txtDRCustAbbr5.Enabled = false;
        //txtDRCustAcNo51.Enabled = false;
        //txtDRCustAcNo52.Enabled = false;
        //txtDRCurr5.Enabled = false;
        //txtDRAmount5.Enabled = false;
        //======== Supriya Changes Start ===============
        //-------- General Operations ----------------
        txtGO_ValueDate.Enabled = false;
        btncalendar_valdt.Enabled = false;
        //txtGO_Ref_No.Enabled = false;
        txtGO_Remark.Enabled = false;
        txtGO_Section.Enabled = false;
        txtGO_Comment.Enabled = false;
        txtGO_Memo.Enabled = false;
        txtGO_SchemeNo.Enabled = false;
        //-------- Debit -------------
        txtGO_Debit.Enabled = false;
        txtGO_Debit_CCY.Enabled = false;
        txtGO_Debit_Amt.Enabled = false;
        txtGO_Debit_Cust.Enabled = false;
        txtGO_Debit_Cust_Name.Enabled = false; // Anand 10-08-2023
        txtGO_Debit_Cust_AcCode.Enabled = false;
        txtGO_Debit_Cust_AcCode_Name.Enabled = false;// Anand 10-08-2023
        txtGO_Debit_Cust_AccNo.Enabled = false;
        txtGO_Debit_ExchRate.Enabled = false;
        txtGO_Debit_ExchCCY.Enabled = false;
        txtGO_Debit_Fund.Enabled = false;
        txtGO_Debit_CheckNo.Enabled = false;
        txtGO_Debit_Available.Enabled = false;
        txtGO_Debit_Advice_Print.Enabled = false;
        txtGO_Debit_Details.Enabled = false;
        txtGO_Debit_Entity.Enabled = false;
        txtGO_Debit_Division.Enabled = false;
        txtGO_Debit_InterAmt.Enabled = false;
        txtGO_Debit_InterRate.Enabled = false;
        //-------- Credit -------------
        txtGO_Credit.Enabled = false;
        txtGO_Credit_Amt.Enabled = false;
        txtGO_Credit_CCY.Enabled = false;
        txtGO_Credit_Cust.Enabled = false;
        txtGO_Credit_Cust_Name.Enabled = false;// Anand 10-08-2023
        txtGO_Credit_Cust_AcCode.Enabled = false;
        txtGO_Credit_Cust_AcCode_Name.Enabled = false;// Anand 10-08-2023
        txtGO_Credit_Cust_AccNo.Enabled = false;
        txtGO_Credit_ExchCCY.Enabled = false;
        txtGO_Credit_ExchRate.Enabled = false;
        txtGO_Credit_Fund.Enabled = false;
        txtGO_Credit_CheckNo.Enabled = false;
        txtGO_Credit_Available.Enabled = false;
        txtGO_Credit_Advice_Print.Enabled = false;
        txtGO_Credit_Details.Enabled = false;
        txtGO_Credit_Entity.Enabled = false;
        txtGO_Credit_Division.Enabled = false;
        txtGO_Credit_InterAmt.Enabled = false;
        txtGO_Credit_InterRate.Enabled = false;
        //-------- Normal General Operations ----------------
        txtNGO_ValueDate.Enabled = false;
        btncalendar_ngovaldt.Enabled = false;
        // txtNGO_Ref_No.Enabled = false;
        txtNGO_Remark.Enabled = false;
        txtNGO_Section.Enabled = false;
        txtNGO_Comment.Enabled = false;
        txtNGO_Memo.Enabled = false;
        txtNGO_SchemeNo.Enabled = false;
        //-------- Debit -------------
        txtNGO_Debit.Enabled = false;
        txtNGO_Debit_CCY.Enabled = false;
        txtNGO_Debit_Amt.Enabled = false;
        txtNGO_Debit_Cust.Enabled = false;
        txtNGO_Debit_Cust_Name.Enabled = false;//Anand 10-08-2023
        txtNGO_Debit_Cust_AcCode.Enabled = false;
        txtNGO_Debit_Cust_AcCode_Name.Enabled = false;//Anand 10-08-2023
        txtNGO_Debit_Cust_AccNo.Enabled = false;
        txtNGO_Debit_ExchRate.Enabled = false;
        txtNGO_Debit_ExchCCY.Enabled = false;
        txtNGO_Debit_Fund.Enabled = false;
        txtNGO_Debit_CheckNo.Enabled = false;
        txtNGO_Debit_Available.Enabled = false;
        txtNGO_Debit_Advice_Print.Enabled = false;
        txtNGO_Debit_Details.Enabled = false;
        txtNGO_Debit_Entity.Enabled = false;
        txtNGO_Debit_Division.Enabled = false;
        txtNGO_Debit_InterAmt.Enabled = false;
        txtNGO_Debit_InterRate.Enabled = false;
        //-------- Credit -------------
        txtNGO_Credit.Enabled = false;
        txtNGO_Credit_Amt.Enabled = false;
        txtNGO_Credit_CCY.Enabled = false;
        txtNGO_Credit_Cust.Enabled = false;
        txtNGO_Credit_Cust_AcCode.Enabled = false;
        txtNGO_Credit_Cust_Name.Enabled = false;//Anand 10-08-2023
        txtNGO_Credit_Cust_AcCode_Name.Enabled = false;//Anand 10-08-2023
        txtNGO_Credit_Cust_AccNo.Enabled = false;
        txtNGO_Credit_ExchCCY.Enabled = false;
        txtNGO_Credit_ExchRate.Enabled = false;
        txtNGO_Credit_Fund.Enabled = false;
        txtNGO_Credit_CheckNo.Enabled = false;
        txtNGO_Credit_Available.Enabled = false;
        txtNGO_Credit_Advice_Print.Enabled = false;
        txtNGO_Credit_Details.Enabled = false;
        txtNGO_Credit_Entity.Enabled = false;
        txtNGO_Credit_Division.Enabled = false;
        txtNGO_Credit_InterAmt.Enabled = false;
        txtNGO_Credit_InterRate.Enabled = false;
        //======== Supriya Changes End ===============
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", hdnDocNo.Value.Trim());
        SqlParameter P_SR_NO = new SqlParameter("@SR_NO", txtSrNo.Text.Trim());

        SqlParameter p1 = new SqlParameter("@docno", hdnDocNo.Value.Trim());
        SqlParameter p2 = new SqlParameter("@srno", txtSrNo.Text.Trim());
        string query = "TF_GetRealisedEntryDetailsNew_Checker";
        DataTable dt = obj.getData(query, p1, p2);
        string CheckerStatus = "";
        if (dt.Rows.Count > 0)
        {
            CheckerStatus = dt.Rows[0]["Checker_Status"].ToString();
            branchcode1 = dt.Rows[0]["Branch_Code"].ToString();
        }

        if (CheckerStatus == "A" || CheckerStatus == "R")
        {
            string ST = "";
            if (CheckerStatus == "A")
            {
                if (branchcode1 == "792")
                {
                    ST = "Approved";
                    ddlApproveReject.SelectedIndex = 1;
                }
                else
                {
                    ST = "Approved";
                    ddlApproveReject1.SelectedIndex = 1;
                }
            }
            else
            {
                if (branchcode1 == "792")
                {
                    ST = "Rejected";
                    ddlApproveReject.SelectedIndex = 2;
                }
                else
                {
                    ST = "Rejected";
                    ddlApproveReject1.SelectedIndex = 2;
                }
            }
            ddlApproveReject.Enabled = false;
            ddlApproveReject1.Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('This transaction is already " + ST + ".');", true);
        }
        else
        {

            string AR = ""; string chkIRMCreateQ = "";
            if (branchcode1 == "792")
            {
                if (ddlApproveReject.SelectedIndex == 1)
                {
                    AR = "A";
                }
                if (ddlApproveReject.SelectedIndex == 2)
                {
                    AR = "R";
                }
            }
            else
            {
                if (ddlApproveReject1.SelectedIndex == 1)
                {
                    AR = "A";
                }
                if (ddlApproveReject1.SelectedIndex == 2)
                {
                    AR = "R";
                }
            }
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter P_CheckedBy = new SqlParameter("@CheckBy", hdnUserName.Value);
            string Result = obj.SaveDeleteData("TF_EXP_ChekerApproveRejectRealisation", P_DocNo, P_Status, P_RejectReason, P_CheckedBy, P_SR_NO);
            if (AR == "A")
            {
                SqlParameter LEIbCode = new SqlParameter("@bCode", hdnBranchCode.Value);
                SqlParameter LEIdocNo = new SqlParameter("@docNo", hdnDocNo.Value.Trim());
                SqlParameter LEIdocSr = new SqlParameter("@srno", txtSrNo.Text.Trim());
                string Trans_Status1 = "S";
                SqlParameter LEITrans_Status = new SqlParameter("@Trans_Status", Trans_Status1);
                SqlParameter LEI_Status = new SqlParameter("@Checker_status", "A");
                SqlParameter LEIuser = new SqlParameter("@user", Session["userName"].ToString());
                SqlParameter LEIuploadingdate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                string _resultLEI = "";
                string _queryLEI = "TF_EXP_Update_LEITransaction_Realized_Checker";
                TF_DATA objSaveLEI = new TF_DATA();
                _resultLEI = objSaveLEI.SaveDeleteData(_queryLEI, LEIbCode, LEIdocNo, LEIdocSr, LEITrans_Status, LEI_Status, LEIuser, LEIuploadingdate);
                
                if (chkIRMCreate.Checked == true)
                {
                    chkIRMCreateQ = "true";
                    getSrialNo();
                    SqlParameter IRMbCode = new SqlParameter("@branch", branchcode1);
                    SqlParameter P_BankUniqueTransactionID = new SqlParameter("@BankUniqueTransactionID", txtBankUniqueTransactionID.Text);
                    string ResultIRM = obj.SaveDeleteData("TF_EXP_IRM_Realisation_Update_BankUniqueTransactionID", IRMbCode, p1, p2, P_BankUniqueTransactionID);

                    JSON();
                }
                TransferExportToEDPMS_PRN();
            }

            Response.Redirect("EXP_ViewRealisationEntry_Checker.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR + "&chkIRMCreate=" + chkIRMCreateQ, true);
        }
    }

    private void TransferExportToEDPMS_PRN()
    {
        if (chkIRMCreate.Checked == true)
        {
            TF_DATA obj = new TF_DATA();
            SqlParameter P_DocNo = new SqlParameter("@Document_No", hdnDocNo.Value.Trim());
            SqlParameter P_SRNo = new SqlParameter("@SrNo", txtSrNo.Text);
            string Result = obj.SaveDeleteData("TF_EDPMS_DATA_TRANSFER_Approve_PRN_CreateIRM", P_DocNo, P_SRNo);
        }
        else if (btnTTRefNoList.Checked == true)
        {
            TF_DATA obj = new TF_DATA();
            SqlParameter P_DocNo = new SqlParameter("@Document_No", hdnDocNo.Value.Trim());
            SqlParameter P_SRNo = new SqlParameter("@SrNo", txtSrNo.Text);
            string Result = obj.SaveDeleteData("TF_EDPMS_DATA_TRANSFER_Approve_PRN_LnikIRM", P_DocNo, P_SRNo);
        }
        else if (chkFirc.Checked == true)
        {
            TF_DATA obj = new TF_DATA();
            SqlParameter P_DocNo = new SqlParameter("@Document_No", hdnDocNo.Value.Trim());
            SqlParameter P_SRNo = new SqlParameter("@SrNo", txtSrNo.Text);
            string Result = obj.SaveDeleteData("TF_EDPMS_DATA_TRANSFER_Approve_PRN_FIRC", P_DocNo, P_SRNo);
        }

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        string frm = txtDateRealised.Text.Trim();
        string rptdocno = txtDocNo.Text.Trim();
        string _script = "window.open('../Reports/EXPReports/ViewExportBillDocument.aspx?frm=" + frm.Trim() + "&rptType=Single&rptFrdocno=" + rptdocno.Trim() + "&rptTodocno=" + rptdocno.Trim() + "&rptCode=2&Branch=" + Session["userADCode"].ToString() + "&Report=Export Bill Document Realisation', 'popup_window', 'height=520,  width=800,status= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
    }
    //----------------------------------------------------Nilesh chngeing END----------------------------------------------

    //------------------------------------Anand 28-06-2023------------------------------------------
    protected void chk_IMPACC1Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC1Flag.Checked == false)
        {
            PanelIMPACC1.Visible = false;
        }
        else if (chk_IMPACC1Flag.Checked == true)
        {
            PanelIMPACC1.Visible = true;
            //  txt_IMPACC1_CR_Code.Text = Settl_ForBank_AccCode.Value;
            // txt_IMPACC1_CR_Cust_abbr.Text = Settl_For_Bank_Abbr.Value;
            // txt_IMPACC1_CR_Cust_Acc.Text = Settl_ForBank_AccNo.Value;
            // txt_IMPACC1_CR_Their_Commission_amt.Text = ACC_Their_Comm_Amount.Value;
            // txt_IMPACC1_CR_Their_Commission_Curr.Text = ACC_Their_Comm_Curr.Value;
            // txt_IMPACC1_DR_Cur_Acc_amt6.Text = ACC_Their_Comm_Amount.Value;
            // txt_IMPACC1_DR_Cur_Acc_Curr6.Text = ACC_Their_Comm_Curr.Value;


            //txt_IMPACC1_DR_Code.Text = Settlement_For_Cust_AccCode.Value;
            //txt_IMPACC1_DR_AC_Short_Name.Text = "CURRENT ACCOUNT";
            //txt_IMPACC1_DR_Cust_abbr.Text = Settlement_For_Cust_Abbr.Value;
            //txt_IMPACC1_DR_Cust_Acc.Text = Settlement_For_Cust_AccNo.Value;

            //txt_IMPACC1_DiscAmt.Text = lblBillAmt.Text;
            //txt_IMPACC1_CR_Acceptance_amt.Text = lblBillAmt.Text;
            //txt_IMPACC1_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            //txt_IMPACC1_DR_Cur_Acc_amt.Text = lblBillAmt.Text;
            //txt_IMPACC1_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
        }
    }
    protected void chk_IMPACC2Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC2Flag.Checked == false)
        {
            PanelIMPACC2.Visible = false;
        }
        else if (chk_IMPACC2Flag.Checked == true)
        {
            PanelIMPACC2.Visible = true;
            //txt_IMPACC2_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            //txt_IMPACC2_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
        }
    }
    protected void chk_IMPACC3Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC3Flag.Checked == false)
        {
            PanelIMPACC3.Visible = false;
        }
        else if (chk_IMPACC3Flag.Checked == true)
        {
            PanelIMPACC3.Visible = true;
            //txt_IMPACC3_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            //txt_IMPACC3_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
        }
    }
    protected void chk_IMPACC4Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_IMPACC4Flag.Checked == false)
        {
            PanelIMPACC4.Visible = false;
        }
        else if (chk_IMPACC4Flag.Checked == true)
        {
            PanelIMPACC4.Visible = true;
            //txt_IMPACC4_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            //txt_IMPACC4_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
        }
    }
    protected void chk_IMPACC5Flag_OnCheckedChanged(object sender, EventArgs e)
    {

        if (chk_IMPACC5Flag.Checked == false)
        {
            PanelIMPACC5.Visible = false;
        }
        else if (chk_IMPACC5Flag.Checked == true)
        {
            PanelIMPACC5.Visible = true;
            //txt_IMPACC5_CR_Acceptance_Curr.Text = lblDoc_Curr.Text;
            //txt_IMPACC5_DR_Cur_Acc_Curr.Text = lblDoc_Curr.Text;
        }
    }
    //-----------------------------------End------------------------------------------------

    //--------------------------------Anand04-07-2023-----------------------------

    protected void txtRemitterCountry_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", txtRemitterCountry.Text.Trim());
        DataTable dt = objData.getData("TF_GetCountryDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
            //txtRemitterAddress.Focus();
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
        TF_DATA objData = new TF_DATA();
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
    protected void txt_swiftcode_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        if (txtSwiftCode.Text.Trim() != "")
        {
            SqlParameter p1 = new SqlParameter("@SwiftCode", txtSwiftCode.Text.Trim());
            DataTable dt = objData.getData("TF_GetBankDetailsBySwiftCode", p1);
            if (dt.Rows.Count > 0)
            {
                txtRemitterBank.Text = dt.Rows[0]["BankName"].ToString();
                txtRemitterBankAddress.Text = dt.Rows[0]["BankAddress"].ToString();
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
    protected void txtPurposeCode_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@purposecode", txtPurposeCode.Text);
        DataTable dt = objData.getData("TF_GetPurposeCodeMasterDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblpurposeCode.Text = dt.Rows[0]["description"].ToString();

        }
        if (lblpurposeCode.Text == "")
        {
            txtPurposeCode.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid PurposeCode.')", true);
            txtPurposeCode.Focus();
        }
    }

    protected void chkIRMCreate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIRMCreate.Checked == true)
        {
            btnTTRefNoList.Enabled = false;
            pnlIRMCreate.Visible = true;
            /////////////  added by shailesh for IRM
            txtBankReferencenumber.Text = txtDocNo.Text;
            txtDocNo.BackColor = txtValueDate.BackColor = txtDateRealised.BackColor = txt_relcur.BackColor = txtAmtRealised.BackColor = txtExchangeRate.BackColor =
                txtAmtRealisedinINR.BackColor = txtRemitterName.BackColor = txtRemitterAddress.BackColor = txtRemitterCountry.BackColor = txtSwiftCode.BackColor =
                txtRemitterBank.BackColor = txtRemitterBankAddress.BackColor = txtRemBankCountry.BackColor = txtPurposeCode.BackColor = txtpurposeofRemittance.BackColor =
                ddlModeOfPayment.BackColor = txtBankUniqueTransactionID.BackColor = txtIFSCCode.BackColor = txtRemittanceADCode.BackColor = txtIECCode.BackColor =
                txtPanNumber.BackColor = txtBankReferencenumber.BackColor = txtBankAccountNumber.BackColor = ddlIRMStatus.BackColor = System.Drawing.Color.PeachPuff;
        }
        else
        {
            btnTTRefNoList.Enabled = true;
            pnlIRMCreate.Visible = false;

            txtDocNo.BackColor = txtValueDate.BackColor = txtDateRealised.BackColor = txt_relcur.BackColor = txtAmtRealised.BackColor = txtExchangeRate.BackColor =
                txtAmtRealisedinINR.BackColor = txtRemitterName.BackColor = txtRemitterAddress.BackColor = txtRemitterCountry.BackColor = txtSwiftCode.BackColor =
                txtRemitterBank.BackColor = txtRemitterBankAddress.BackColor = txtRemBankCountry.BackColor = txtPurposeCode.BackColor = txtpurposeofRemittance.BackColor =
                ddlModeOfPayment.BackColor = txtBankUniqueTransactionID.BackColor = txtIFSCCode.BackColor = txtRemittanceADCode.BackColor = txtIECCode.BackColor =
                txtPanNumber.BackColor = txtBankReferencenumber.BackColor = txtBankAccountNumber.BackColor = ddlIRMStatus.BackColor = System.Drawing.Color.White;
        }
    }
    protected void chkTTRefNo_CheckedChanged(object sender, EventArgs e)
    {
        if (btnTTRefNoList.Checked == true)
        {
            chkIRMCreate.Enabled = false;
        }
        else
        {
            chkIRMCreate.Enabled = true;
        }
    }

    protected void TTRemitterName(string TTno, string LblRname)
    {

        string _custAcNo = txtCustAcNo.Text.Trim();
        string bcode = Request.QueryString["BranchCode"].Trim();

        SqlParameter p1 = new SqlParameter("@bcode", bcode);
        SqlParameter p2 = new SqlParameter("@custAcNo", _custAcNo);
        SqlParameter p3 = new SqlParameter("@TTrefno ", TTno);

        string _query = "TF_EXP_GetTTRemitterName";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            switch (LblRname)
            {
                case "1":
                    lblTTRname1.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname1.Visible = true;
                    break;

                case "2":
                    lblTTRname2.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname2.Visible = true;
                    break;

                case "3":
                    lblTTRname3.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname3.Visible = true;
                    break;

                case "4":
                    lblTTRname4.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname4.Visible = true;
                    break;
                case "5":
                    lblTTRname5.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname5.Visible = true;
                    break;

                case "6":
                    lblTTRname6.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname6.Visible = true;
                    break;

                case "7":
                    lblTTRname7.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname7.Visible = true;
                    break;

                case "8":
                    lblTTRname8.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname8.Visible = true;
                    break;

                case "9":
                    lblTTRname9.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname9.Visible = true;
                    break;

                case "10":
                    lblTTRname10.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname10.Visible = true;
                    break;

                case "11":
                    lblTTRname11.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname11.Visible = true;
                    break;

                case "12":
                    lblTTRname12.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname12.Visible = true;
                    break;

                case "13":
                    lblTTRname13.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname13.Visible = true;
                    break;

                case "14":
                    lblTTRname14.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname14.Visible = true;
                    break;
                case "15":
                    lblTTRname15.ToolTip = dt.Rows[0]["RemitterName"].ToString().Trim();
                    lblTTRname15.Visible = true;
                    break;

            }

        }
    }
    protected void TTCurrPairCalc(string TTAmtAdj, string TTCurr, string RelizedCurr, string CrossRate, string TTFIRC)
    {
        SqlParameter p1 = new SqlParameter("@IRMCurr", TTCurr);
        SqlParameter p2 = new SqlParameter("@RelizedCurr", RelizedCurr);

        string _query = "TF_EXP_GetTTCurrPair";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            string X = dt.Rows[0]["FXRateRule"].ToString().Trim();
            float val1 = float.Parse(TTAmtAdj);
            float val2 = float.Parse(CrossRate);
            float val3 = 0;
            if (X == "*")
            {
                val3 = val1 * val2;
            }
            else if (X == "/")
            {
                val3 = val1 / val2;
            }
            switch (TTFIRC)
            {
                case "T1":
                    txtTTAmtRealised1.Text = val3.ToString();
                    break;
                case "T2":
                    txtTTAmtRealised2.Text = val3.ToString();
                    break;
                case "T3":
                    txtTTAmtRealised3.Text = val3.ToString();
                    break;
                case "T4":
                    txtTTAmtRealised4.Text = val3.ToString();
                    break;
                case "T5":
                    txtTTAmtRealised5.Text = val3.ToString();
                    break;
                case "T6":
                    txtTTAmtRealised6.Text = val3.ToString();
                    break;
                case "T7":
                    txtTTAmtRealised7.Text = val3.ToString();
                    break;
                case "T8":
                    txtTTAmtRealised8.Text = val3.ToString();
                    break;
                case "T9":
                    txtTTAmtRealised9.Text = val3.ToString();
                    break;
                case "T10":
                    txtTTAmtRealised10.Text = val3.ToString();
                    break;
                case "T11":
                    txtTTAmtRealised11.Text = val3.ToString();
                    break;
                case "T12":
                    txtTTAmtRealised12.Text = val3.ToString();
                    break;
                case "T13":
                    txtTTAmtRealised13.Text = val3.ToString();
                    break;
                case "T14":
                    txtTTAmtRealised14.Text = val3.ToString();
                    break;
                case "T15":
                    txtTTAmtRealised15.Text = val3.ToString();
                    break;
            }
        }
    }
    protected void txtTTRefNo1_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency1.SelectedValue)
        {
            ddlTTRealisedCurr1.Enabled = false;
            txtTTCrossCurrRate1.Enabled = false;
            ddlTTRealisedCurr1.SelectedValue = "0";
            txtTTCrossCurrRate1.Text = "";
            txtTTAmtRealised1.Text = txtTTAmount1.Text;
        }
        else
        {
            ddlTTRealisedCurr1.Enabled = true;
            txtTTCrossCurrRate1.Enabled = true;
            txtTTAmtRealised1.Text = txtTTAmount1.Text;
        }
        TTRemitterName(txtTTRefNo1.Text, "1");

    }
    protected void txtTTAmount1_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency1.SelectedValue)
        {
            txtTTAmtRealised1.Text = txtTTAmount1.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount1.Text, ddlTTCurrency1.SelectedValue, ddlTTRealisedCurr1.SelectedValue, txtTTCrossCurrRate1.Text, "T1");
        }
    }

    protected void txtTTCrossCurrRate1_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr1.SelectedValue == "0" && txtTTCrossCurrRate1.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr1');", true);
            txtTTCrossCurrRate1.Text = "";
            ddlTTRealisedCurr1.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount1.Text, ddlTTCurrency1.SelectedValue, ddlTTRealisedCurr1.SelectedValue, txtTTCrossCurrRate1.Text, "T1");
        }
    }


    protected void txtTTRefNo2_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency2.SelectedValue)
        {
            ddlTTRealisedCurr2.Enabled = false;
            txtTTCrossCurrRate2.Enabled = false;
            ddlTTRealisedCurr2.SelectedValue = "0";
            txtTTCrossCurrRate2.Text = "";
            txtTTAmtRealised2.Text = txtTTAmount2.Text;
        }
        else
        {
            ddlTTRealisedCurr2.Enabled = true;
            txtTTCrossCurrRate2.Enabled = true;
            txtTTAmtRealised2.Text = txtTTAmount2.Text;
        }
        TTRemitterName(txtTTRefNo2.Text, "2");
    }
    protected void txtTTAmount2_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency2.SelectedValue)
        {
            txtTTAmtRealised2.Text = txtTTAmount2.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount2.Text, ddlTTCurrency2.SelectedValue, ddlTTRealisedCurr2.SelectedValue, txtTTCrossCurrRate2.Text, "T2");
        }
    }
    protected void txtTTCrossCurrRate2_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr2.SelectedValue == "0" && txtTTCrossCurrRate2.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr2');", true);
            txtTTCrossCurrRate2.Text = "";
            ddlTTRealisedCurr2.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount2.Text, ddlTTCurrency2.SelectedValue, ddlTTRealisedCurr2.SelectedValue, txtTTCrossCurrRate2.Text, "T2");
        }

    }

    protected void txtTTRefNo3_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency3.SelectedValue)
        {
            ddlTTRealisedCurr3.Enabled = false;
            txtTTCrossCurrRate3.Enabled = false;
            ddlTTRealisedCurr3.SelectedValue = "0";
            txtTTCrossCurrRate3.Text = "";
            txtTTAmtRealised3.Text = txtTTAmount3.Text;
        }
        else
        {
            ddlTTRealisedCurr3.Enabled = true;
            txtTTCrossCurrRate3.Enabled = true;
            txtTTAmtRealised3.Text = txtTTAmount3.Text;
        }
        TTRemitterName(txtTTRefNo3.Text, "3");
    }
    protected void txtTTAmount3_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency3.SelectedValue)
        {
            txtTTAmtRealised3.Text = txtTTAmount3.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount3.Text, ddlTTCurrency3.SelectedValue, ddlTTRealisedCurr3.SelectedValue, txtTTCrossCurrRate3.Text, "T3");
        }
    }
    protected void txtTTCrossCurrRate3_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr3.SelectedValue == "0" && txtTTCrossCurrRate3.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr3');", true);
            txtTTCrossCurrRate3.Text = "";
            ddlTTRealisedCurr3.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount3.Text, ddlTTCurrency3.SelectedValue, ddlTTRealisedCurr3.SelectedValue, txtTTCrossCurrRate3.Text, "T3");
        }

    }


    protected void txtTTRefNo4_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency4.SelectedValue)
        {
            ddlTTRealisedCurr4.Enabled = false;
            txtTTCrossCurrRate4.Enabled = false;
            ddlTTRealisedCurr4.SelectedValue = "0";
            txtTTCrossCurrRate4.Text = "";
            txtTTAmtRealised4.Text = txtTTAmount4.Text;
        }
        else
        {
            ddlTTRealisedCurr4.Enabled = true;
            txtTTCrossCurrRate4.Enabled = true;
            txtTTAmtRealised4.Text = txtTTAmount4.Text;
        }
        TTRemitterName(txtTTRefNo4.Text, "4");
    }
    protected void txtTTAmount4_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency4.SelectedValue)
        {
            txtTTAmtRealised4.Text = txtTTAmount4.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount4.Text, ddlTTCurrency4.SelectedValue, ddlTTRealisedCurr4.SelectedValue, txtTTCrossCurrRate4.Text, "T4");
        }
    }
    protected void txtTTCrossCurrRate4_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr4.SelectedValue == "0" && txtTTCrossCurrRate4.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr4');", true);
            txtTTCrossCurrRate4.Text = "";
            ddlTTRealisedCurr4.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount4.Text, ddlTTCurrency4.SelectedValue, ddlTTRealisedCurr4.SelectedValue, txtTTCrossCurrRate4.Text, "T4");
        }

    }


    protected void txtTTRefNo5_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency5.SelectedValue)
        {
            ddlTTRealisedCurr5.Enabled = false;
            txtTTCrossCurrRate5.Enabled = false;
            ddlTTRealisedCurr5.SelectedValue = "0";
            txtTTCrossCurrRate5.Text = "";
            txtTTAmtRealised5.Text = txtTTAmount5.Text;
        }
        else
        {
            ddlTTRealisedCurr5.Enabled = true;
            txtTTCrossCurrRate5.Enabled = true;
            txtTTAmtRealised5.Text = txtTTAmount5.Text;
        }
        TTRemitterName(txtTTRefNo5.Text, "5");
    }
    protected void txtTTAmount5_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency5.SelectedValue)
        {
            txtTTAmtRealised5.Text = txtTTAmount5.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount5.Text, ddlTTCurrency5.SelectedValue, ddlTTRealisedCurr5.SelectedValue, txtTTCrossCurrRate5.Text, "T5");
        }
    }
    protected void txtTTCrossCurrRate5_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr5.SelectedValue == "0" && txtTTCrossCurrRate5.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr5');", true);
            txtTTCrossCurrRate5.Text = "";
            ddlTTRealisedCurr5.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount5.Text, ddlTTCurrency5.SelectedValue, ddlTTRealisedCurr5.SelectedValue, txtTTCrossCurrRate5.Text, "T5");
        }

    }

    protected void txtTTRefNo6_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency6.SelectedValue)
        {
            ddlTTRealisedCurr6.Enabled = false;
            txtTTCrossCurrRate6.Enabled = false;
            ddlTTRealisedCurr6.SelectedValue = "0";
            txtTTCrossCurrRate6.Text = "";
            txtTTAmtRealised6.Text = txtTTAmount6.Text;
        }
        else
        {
            ddlTTRealisedCurr6.Enabled = true;
            txtTTCrossCurrRate6.Enabled = true;
            txtTTAmtRealised6.Text = txtTTAmount6.Text;
        }
        TTRemitterName(txtTTRefNo6.Text, "6");
    }
    protected void txtTTAmount6_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency6.SelectedValue)
        {
            txtTTAmtRealised6.Text = txtTTAmount6.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount6.Text, ddlTTCurrency6.SelectedValue, ddlTTRealisedCurr6.SelectedValue, txtTTCrossCurrRate6.Text, "T6");
        }
    }
    protected void txtTTCrossCurrRate6_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr6.SelectedValue == "0" && txtTTCrossCurrRate6.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr6');", true);
            txtTTCrossCurrRate6.Text = "";
            ddlTTRealisedCurr6.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount6.Text, ddlTTCurrency6.SelectedValue, ddlTTRealisedCurr6.SelectedValue, txtTTCrossCurrRate6.Text, "T6");
        }

    }

    protected void txtTTRefNo7_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency7.SelectedValue)
        {
            ddlTTRealisedCurr7.Enabled = false;
            txtTTCrossCurrRate7.Enabled = false;
            ddlTTRealisedCurr7.SelectedValue = "0";
            txtTTCrossCurrRate7.Text = "";
            txtTTAmtRealised7.Text = txtTTAmount7.Text;
        }
        else
        {
            ddlTTRealisedCurr7.Enabled = true;
            txtTTCrossCurrRate7.Enabled = true;
            txtTTAmtRealised7.Text = txtTTAmount7.Text;
        }
        TTRemitterName(txtTTRefNo7.Text, "7");
    }
    protected void txtTTAmount7_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency7.SelectedValue)
        {
            txtTTAmtRealised7.Text = txtTTAmount7.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount7.Text, ddlTTCurrency7.SelectedValue, ddlTTRealisedCurr7.SelectedValue, txtTTCrossCurrRate7.Text, "T7");
        }
    }
    protected void txtTTCrossCurrRate7_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr7.SelectedValue == "0" && txtTTCrossCurrRate7.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr7');", true);
            txtTTCrossCurrRate7.Text = "";
            ddlTTRealisedCurr7.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount7.Text, ddlTTCurrency7.SelectedValue, ddlTTRealisedCurr7.SelectedValue, txtTTCrossCurrRate7.Text, "T7");
        }

    }

    protected void txtTTRefNo8_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency8.SelectedValue)
        {
            ddlTTRealisedCurr8.Enabled = false;
            txtTTCrossCurrRate8.Enabled = false;
            ddlTTRealisedCurr8.SelectedValue = "0";
            txtTTCrossCurrRate8.Text = "";
            txtTTAmtRealised8.Text = txtTTAmount8.Text;
        }

        else
        {
            ddlTTRealisedCurr8.Enabled = true;
            txtTTCrossCurrRate8.Enabled = true;
            txtTTAmtRealised8.Text = txtTTAmount8.Text;
        }
        TTRemitterName(txtTTRefNo8.Text, "8");
    }
    protected void txtTTAmount8_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency8.SelectedValue)
        {
            txtTTAmtRealised8.Text = txtTTAmount8.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount8.Text, ddlTTCurrency8.SelectedValue, ddlTTRealisedCurr8.SelectedValue, txtTTCrossCurrRate8.Text, "T8");
        }
    }
    protected void txtTTCrossCurrRate8_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr8.SelectedValue == "0" && txtTTCrossCurrRate8.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr8');", true);
            txtTTCrossCurrRate8.Text = "";
            ddlTTRealisedCurr8.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount8.Text, ddlTTCurrency8.SelectedValue, ddlTTRealisedCurr8.SelectedValue, txtTTCrossCurrRate8.Text, "T8");
        }

    }

    protected void txtTTRefNo9_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency9.SelectedValue)
        {
            ddlTTRealisedCurr9.Enabled = false;
            txtTTCrossCurrRate9.Enabled = false;
            ddlTTRealisedCurr9.SelectedValue = "0";
            txtTTCrossCurrRate9.Text = "";
            txtTTAmtRealised9.Text = txtTTAmount9.Text;
        }
        else
        {
            ddlTTRealisedCurr9.Enabled = true;
            txtTTCrossCurrRate9.Enabled = true;
            txtTTAmtRealised9.Text = txtTTAmount9.Text;
        }
        TTRemitterName(txtTTRefNo9.Text, "9");
    }
    protected void txtTTAmount9_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency9.SelectedValue)
        {
            txtTTAmtRealised9.Text = txtTTAmount9.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount9.Text, ddlTTCurrency9.SelectedValue, ddlTTRealisedCurr9.SelectedValue, txtTTCrossCurrRate9.Text, "T9");
        }
    }
    protected void txtTTCrossCurrRate9_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr9.SelectedValue == "0" && txtTTCrossCurrRate9.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr9');", true);
            txtTTCrossCurrRate9.Text = "";
            ddlTTRealisedCurr9.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount9.Text, ddlTTCurrency9.SelectedValue, ddlTTRealisedCurr9.SelectedValue, txtTTCrossCurrRate9.Text, "T9");
        }

    }

    protected void txtTTRefNo10_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency10.SelectedValue)
        {
            ddlTTRealisedCurr10.Enabled = false;
            txtTTCrossCurrRate10.Enabled = false;
            ddlTTRealisedCurr10.SelectedValue = "0";
            txtTTCrossCurrRate10.Text = "";
            txtTTAmtRealised10.Text = txtTTAmount10.Text;
        }
        else
        {
            ddlTTRealisedCurr10.Enabled = true;
            txtTTCrossCurrRate10.Enabled = true;
            txtTTAmtRealised10.Text = txtTTAmount10.Text;
        }
        TTRemitterName(txtTTRefNo10.Text, "10");
    }
    protected void txtTTAmount10_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency10.SelectedValue)
        {
            txtTTAmtRealised10.Text = txtTTAmount10.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount10.Text, ddlTTCurrency10.SelectedValue, ddlTTRealisedCurr10.SelectedValue, txtTTCrossCurrRate10.Text, "T10");
        }
    }
    protected void txtTTCrossCurrRate10_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr10.SelectedValue == "0" && txtTTCrossCurrRate10.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr10');", true);
            txtTTCrossCurrRate10.Text = "";
            ddlTTRealisedCurr10.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount10.Text, ddlTTCurrency10.SelectedValue, ddlTTRealisedCurr10.SelectedValue, txtTTCrossCurrRate10.Text, "T10");
        }

    }

    protected void txtTTRefNo11_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency11.SelectedValue)
        {
            ddlTTRealisedCurr11.Enabled = false;
            txtTTCrossCurrRate11.Enabled = false;
            ddlTTRealisedCurr11.SelectedValue = "0";
            txtTTCrossCurrRate11.Text = "";
            txtTTAmtRealised11.Text = txtTTAmount11.Text;
        }
        else
        {
            ddlTTRealisedCurr11.Enabled = true;
            txtTTCrossCurrRate11.Enabled = true;
            txtTTAmtRealised11.Text = txtTTAmount11.Text;
        }
        TTRemitterName(txtTTRefNo11.Text, "11");
    }
    protected void txtTTAmount11_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency11.SelectedValue)
        {
            txtTTAmtRealised11.Text = txtTTAmount11.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount11.Text, ddlTTCurrency11.SelectedValue, ddlTTRealisedCurr11.SelectedValue, txtTTCrossCurrRate11.Text, "T11");
        }
    }
    protected void txtTTCrossCurrRate11_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr11.SelectedValue == "0" && txtTTCrossCurrRate11.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr11');", true);
            txtTTCrossCurrRate11.Text = "";
            ddlTTRealisedCurr11.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount11.Text, ddlTTCurrency11.SelectedValue, ddlTTRealisedCurr11.SelectedValue, txtTTCrossCurrRate11.Text, "T11");
        }

    }

    protected void txtTTRefNo12_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency12.SelectedValue)
        {
            ddlTTRealisedCurr12.Enabled = false;
            txtTTCrossCurrRate12.Enabled = false;
            ddlTTRealisedCurr12.SelectedValue = "0";
            txtTTCrossCurrRate12.Text = "";
            txtTTAmtRealised12.Text = txtTTAmount12.Text;
        }
        else
        {
            ddlTTRealisedCurr12.Enabled = true;
            txtTTCrossCurrRate12.Enabled = true;
            txtTTAmtRealised12.Text = txtTTAmount12.Text;
        }
        TTRemitterName(txtTTRefNo12.Text, "12");
    }
    protected void txtTTAmount12_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency12.SelectedValue)
        {
            txtTTAmtRealised12.Text = txtTTAmount12.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount12.Text, ddlTTCurrency12.SelectedValue, ddlTTRealisedCurr12.SelectedValue, txtTTCrossCurrRate12.Text, "T12");
        }
    }
    protected void txtTTCrossCurrRate12_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr12.SelectedValue == "0" && txtTTCrossCurrRate12.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr12');", true);
            txtTTCrossCurrRate12.Text = "";
            ddlTTRealisedCurr12.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount12.Text, ddlTTCurrency12.SelectedValue, ddlTTRealisedCurr12.SelectedValue, txtTTCrossCurrRate12.Text, "T12");
        }

    }

    protected void txtTTRefNo13_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency13.SelectedValue)
        {
            ddlTTRealisedCurr13.Enabled = false;
            txtTTCrossCurrRate13.Enabled = false;
            ddlTTRealisedCurr13.SelectedValue = "0";
            txtTTCrossCurrRate13.Text = "";
            txtTTAmtRealised13.Text = txtTTAmount13.Text;
        }
        else
        {
            ddlTTRealisedCurr13.Enabled = true;
            txtTTCrossCurrRate13.Enabled = true;
            txtTTAmtRealised13.Text = txtTTAmount13.Text;
        }
        TTRemitterName(txtTTRefNo13.Text, "13");
    }
    protected void txtTTAmount13_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency13.SelectedValue)
        {
            txtTTAmtRealised13.Text = txtTTAmount13.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount13.Text, ddlTTCurrency13.SelectedValue, ddlTTRealisedCurr13.SelectedValue, txtTTCrossCurrRate13.Text, "T13");
        }
    }
    protected void txtTTCrossCurrRate13_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr13.SelectedValue == "0" && txtTTCrossCurrRate13.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr13');", true);
            txtTTCrossCurrRate13.Text = "";
            ddlTTRealisedCurr13.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount13.Text, ddlTTCurrency13.SelectedValue, ddlTTRealisedCurr13.SelectedValue, txtTTCrossCurrRate13.Text, "T13");
        }

    }

    protected void txtTTRefNo14_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency14.SelectedValue)
        {
            ddlTTRealisedCurr14.Enabled = false;
            txtTTCrossCurrRate14.Enabled = false;
            ddlTTRealisedCurr14.SelectedValue = "0";
            txtTTCrossCurrRate14.Text = "";
            txtTTAmtRealised14.Text = txtTTAmount14.Text;
        }
        else
        {
            ddlTTRealisedCurr14.Enabled = true;
            txtTTCrossCurrRate14.Enabled = true;
            txtTTAmtRealised14.Text = txtTTAmount14.Text;
        }
        TTRemitterName(txtTTRefNo14.Text, "14");
    }
    protected void txtTTAmount14_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency14.SelectedValue)
        {
            txtTTAmtRealised14.Text = txtTTAmount14.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount14.Text, ddlTTCurrency14.SelectedValue, ddlTTRealisedCurr14.SelectedValue, txtTTCrossCurrRate14.Text, "T14");
        }
    }
    protected void txtTTCrossCurrRate14_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr14.SelectedValue == "0" && txtTTCrossCurrRate14.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr14');", true);
            txtTTCrossCurrRate14.Text = "";
            ddlTTRealisedCurr14.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount14.Text, ddlTTCurrency14.SelectedValue, ddlTTRealisedCurr14.SelectedValue, txtTTCrossCurrRate14.Text, "T14");
        }

    }

    protected void txtTTRefNo15_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency15.SelectedValue)
        {
            ddlTTRealisedCurr15.Enabled = false;
            txtTTCrossCurrRate15.Enabled = false;
            ddlTTRealisedCurr15.SelectedValue = "0";
            txtTTCrossCurrRate15.Text = "";
            txtTTAmtRealised15.Text = txtTTAmount15.Text;
        }
        else
        {
            ddlTTRealisedCurr15.Enabled = true;
            txtTTCrossCurrRate15.Enabled = true;
            txtTTAmtRealised15.Text = txtTTAmount15.Text;
        }
        TTRemitterName(txtTTRefNo15.Text, "15");
    }
    protected void txtTTAmount15_TextChanged(object sender, EventArgs e)
    {
        if (txt_relcur.Text == ddlTTCurrency15.SelectedValue)
        {
            txtTTAmtRealised15.Text = txtTTAmount15.Text;
        }
        else
        {
            TTCurrPairCalc(txtTTAmount15.Text, ddlTTCurrency15.SelectedValue, ddlTTRealisedCurr15.SelectedValue, txtTTCrossCurrRate15.Text, "T15");
        }
    }
    protected void txtTTCrossCurrRate15_TextChanged(object sender, EventArgs e)
    {
        if (ddlTTRealisedCurr15.SelectedValue == "0" && txtTTCrossCurrRate15.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select RealisedCurr15');", true);
            txtTTCrossCurrRate15.Text = "";
            ddlTTRealisedCurr15.Focus();
        }
        else
        {
            TTCurrPairCalc(txtTTAmount15.Text, ddlTTCurrency15.SelectedValue, ddlTTRealisedCurr15.SelectedValue, txtTTCrossCurrRate15.Text, "T15");
        }

    }

    protected void btn_Trans_Add_Click(object sender, EventArgs e)
    {
        TT5Row.Visible = true;
        cpe2.ExpandedSize = 238;
        btn_Trans_Add.Visible = false;
    }
    protected void btn_Trans_Add1_Click(object sender, EventArgs e)
    {
        TT8Row.Visible = true;
        cpe2.ExpandedSize = 330;
        btn_Trans_Add1.Visible = false;
        btn_Trans_Remove1.Visible = false;
    }
    protected void btn_Trans_Remove1_Click(object sender, EventArgs e)
    {
        TT5Row.Visible = false;
        cpe2.ExpandedSize = 160;
        btn_Trans_Add.Visible = true;
    }

    protected void btn_Trans_Add2_Click(object sender, EventArgs e)
    {
        TT11Row.Visible = true;
        cpe2.ExpandedSize = 445;
        btn_Trans_Add2.Visible = false;
        btn_Trans_Remove2.Visible = false;
    }

    protected void btn_Trans_Remove2_Click(object sender, EventArgs e)
    {
        TT8Row.Visible = false;
        cpe2.ExpandedSize = 238;
        btn_Trans_Add1.Visible = true;
        btn_Trans_Remove1.Visible = true;
    }
    protected void btn_Trans_Remove3_Click(object sender, EventArgs e)
    {
        TT11Row.Visible = false;
        cpe2.ExpandedSize = 330;
        btn_Trans_Add2.Visible = true;
        btn_Trans_Remove2.Visible = true;
    }

    protected void fillddlTTCurrency1()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency1.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency1.DataSource = dt.DefaultView;
            ddlTTCurrency1.DataTextField = "C_Code";
            ddlTTCurrency1.DataValueField = "C_Code";
            ddlTTCurrency1.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency1.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency2()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency2.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency2.DataSource = dt.DefaultView;
            ddlTTCurrency2.DataTextField = "C_Code";
            ddlTTCurrency2.DataValueField = "C_Code";
            ddlTTCurrency2.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency2.Items.Insert(0, li);

    }
    protected void fillddlTTCurrency3()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency3.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency3.DataSource = dt.DefaultView;
            ddlTTCurrency3.DataTextField = "C_Code";
            ddlTTCurrency3.DataValueField = "C_Code";
            ddlTTCurrency3.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency3.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency4()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency4.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency4.DataSource = dt.DefaultView;
            ddlTTCurrency4.DataTextField = "C_Code";
            ddlTTCurrency4.DataValueField = "C_Code";
            ddlTTCurrency4.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency4.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency5()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency5.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency5.DataSource = dt.DefaultView;
            ddlTTCurrency5.DataTextField = "C_Code";
            ddlTTCurrency5.DataValueField = "C_Code";
            ddlTTCurrency5.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency5.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency6()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency6.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency6.DataSource = dt.DefaultView;
            ddlTTCurrency6.DataTextField = "C_Code";
            ddlTTCurrency6.DataValueField = "C_Code";
            ddlTTCurrency6.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency6.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency7()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency7.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency7.DataSource = dt.DefaultView;
            ddlTTCurrency7.DataTextField = "C_Code";
            ddlTTCurrency7.DataValueField = "C_Code";
            ddlTTCurrency7.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency7.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency8()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency8.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency8.DataSource = dt.DefaultView;
            ddlTTCurrency8.DataTextField = "C_Code";
            ddlTTCurrency8.DataValueField = "C_Code";
            ddlTTCurrency8.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency8.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency9()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency9.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency9.DataSource = dt.DefaultView;
            ddlTTCurrency9.DataTextField = "C_Code";
            ddlTTCurrency9.DataValueField = "C_Code";
            ddlTTCurrency9.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency9.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency10()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency10.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency10.DataSource = dt.DefaultView;
            ddlTTCurrency10.DataTextField = "C_Code";
            ddlTTCurrency10.DataValueField = "C_Code";
            ddlTTCurrency10.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency10.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency11()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency11.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency11.DataSource = dt.DefaultView;
            ddlTTCurrency11.DataTextField = "C_Code";
            ddlTTCurrency11.DataValueField = "C_Code";
            ddlTTCurrency11.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency11.Items.Insert(0, li);

    }


    protected void fillddlTTCurrency12()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency12.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency12.DataSource = dt.DefaultView;
            ddlTTCurrency12.DataTextField = "C_Code";
            ddlTTCurrency12.DataValueField = "C_Code";
            ddlTTCurrency12.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency12.Items.Insert(0, li);

    }


    protected void fillddlTTCurrency13()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency13.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency13.DataSource = dt.DefaultView;
            ddlTTCurrency13.DataTextField = "C_Code";
            ddlTTCurrency13.DataValueField = "C_Code";
            ddlTTCurrency13.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency13.Items.Insert(0, li);

    }

    protected void fillddlTTCurrency14()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency14.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency14.DataSource = dt.DefaultView;
            ddlTTCurrency14.DataTextField = "C_Code";
            ddlTTCurrency14.DataValueField = "C_Code";
            ddlTTCurrency14.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency14.Items.Insert(0, li);

    }


    protected void fillddlTTCurrency15()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTCurrency15.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTCurrency15.DataSource = dt.DefaultView;
            ddlTTCurrency15.DataTextField = "C_Code";
            ddlTTCurrency15.DataValueField = "C_Code";
            ddlTTCurrency15.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTCurrency15.Items.Insert(0, li);

    }


    protected void fillddlTTRealisedCurr1()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr1.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr1.DataSource = dt.DefaultView;
            ddlTTRealisedCurr1.DataTextField = "C_Code";
            ddlTTRealisedCurr1.DataValueField = "C_Code";
            ddlTTRealisedCurr1.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr1.Items.Insert(0, li);

    }


    protected void fillddlTTRealisedCurr2()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr2.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr2.DataSource = dt.DefaultView;
            ddlTTRealisedCurr2.DataTextField = "C_Code";
            ddlTTRealisedCurr2.DataValueField = "C_Code";
            ddlTTRealisedCurr2.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr2.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr3()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr3.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr3.DataSource = dt.DefaultView;
            ddlTTRealisedCurr3.DataTextField = "C_Code";
            ddlTTRealisedCurr3.DataValueField = "C_Code";
            ddlTTRealisedCurr3.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr3.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr4()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr4.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr4.DataSource = dt.DefaultView;
            ddlTTRealisedCurr4.DataTextField = "C_Code";
            ddlTTRealisedCurr4.DataValueField = "C_Code";
            ddlTTRealisedCurr4.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr4.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr5()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr5.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr5.DataSource = dt.DefaultView;
            ddlTTRealisedCurr5.DataTextField = "C_Code";
            ddlTTRealisedCurr5.DataValueField = "C_Code";
            ddlTTRealisedCurr5.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr5.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr6()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr6.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr6.DataSource = dt.DefaultView;
            ddlTTRealisedCurr6.DataTextField = "C_Code";
            ddlTTRealisedCurr6.DataValueField = "C_Code";
            ddlTTRealisedCurr6.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr6.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr7()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr7.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr7.DataSource = dt.DefaultView;
            ddlTTRealisedCurr7.DataTextField = "C_Code";
            ddlTTRealisedCurr7.DataValueField = "C_Code";
            ddlTTRealisedCurr7.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr7.Items.Insert(0, li);

    }


    protected void fillddlTTRealisedCurr8()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr8.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr8.DataSource = dt.DefaultView;
            ddlTTRealisedCurr8.DataTextField = "C_Code";
            ddlTTRealisedCurr8.DataValueField = "C_Code";
            ddlTTRealisedCurr8.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr8.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr9()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr9.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr9.DataSource = dt.DefaultView;
            ddlTTRealisedCurr9.DataTextField = "C_Code";
            ddlTTRealisedCurr9.DataValueField = "C_Code";
            ddlTTRealisedCurr9.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr9.Items.Insert(0, li);

    }


    protected void fillddlTTRealisedCurr10()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr10.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr10.DataSource = dt.DefaultView;
            ddlTTRealisedCurr10.DataTextField = "C_Code";
            ddlTTRealisedCurr10.DataValueField = "C_Code";
            ddlTTRealisedCurr10.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr10.Items.Insert(0, li);

    }


    protected void fillddlTTRealisedCurr11()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr11.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr11.DataSource = dt.DefaultView;
            ddlTTRealisedCurr11.DataTextField = "C_Code";
            ddlTTRealisedCurr11.DataValueField = "C_Code";
            ddlTTRealisedCurr11.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr11.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr12()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr12.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr12.DataSource = dt.DefaultView;
            ddlTTRealisedCurr12.DataTextField = "C_Code";
            ddlTTRealisedCurr12.DataValueField = "C_Code";
            ddlTTRealisedCurr12.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr12.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr13()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr13.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr13.DataSource = dt.DefaultView;
            ddlTTRealisedCurr13.DataTextField = "C_Code";
            ddlTTRealisedCurr13.DataValueField = "C_Code";
            ddlTTRealisedCurr13.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr13.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr14()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr14.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr14.DataSource = dt.DefaultView;
            ddlTTRealisedCurr14.DataTextField = "C_Code";
            ddlTTRealisedCurr14.DataValueField = "C_Code";
            ddlTTRealisedCurr14.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr14.Items.Insert(0, li);

    }

    protected void fillddlTTRealisedCurr15()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlTTRealisedCurr15.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlTTRealisedCurr15.DataSource = dt.DefaultView;
            ddlTTRealisedCurr15.DataTextField = "C_Code";
            ddlTTRealisedCurr15.DataValueField = "C_Code";
            ddlTTRealisedCurr15.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlTTRealisedCurr15.Items.Insert(0, li);

    }

    protected void chk_Generaloperation1_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_Generaloperation1.Checked == false)
        {
            panalGO.Visible = false;
        }
        else if (chk_Generaloperation1.Checked == true)
        {
            panalGO.Visible = true;
        }
    }


    protected void chk_Generaloperation2_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_Generaloperation2.Checked == false)
        {
            PanelNormalGO.Visible = false;
        }
        else if (chk_Generaloperation2.Checked == true)
        {
            PanelNormalGO.Visible = true;
        }
    }

    protected void chk_InterOffice_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_InterOffice.Checked == false)
        {
            panal_GOAccChange.Visible = false;
        }
        else if (chk_InterOffice.Checked == true)
        {
            panal_GOAccChange.Visible = true;
        }
    }
    //----------created by Anand 02-09-2023-------------
    protected void getSrialNo()
    {
        //string _docno = txtDocPref.Text.Trim().ToUpper() + "/" + txtDocBRCode.Text.Trim() + "/" + txtDocNo.Text.Trim() + txtDocSrNo.Text.Trim();
        TF_DATA objData = new TF_DATA();
        String _query = "TF_Realize_GetBankUniqueTransactionID";

        SqlParameter p1 = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        p1.Value = hdnBranchCode.Value;
        SqlParameter p2 = new SqlParameter("@Doc_date", SqlDbType.VarChar);
        p2.Value = txtDateRealised.Text.Trim().Replace("/", "");

        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            string newsrno = dt.Rows[0]["BankUniqueTransactionID"].ToString().Trim();
            string s2 = newsrno.Replace("/", "").Trim();
            txtBankUniqueTransactionID.Text = "" + s2.ToString();
        }
    }

    public void JSON()
    {
        string date = DateTime.Now.ToString("ddMMyyyy");// create by Anand 12-09-2023
        string _docno = txtDocNo.Text.Trim();
        string _docno1 = txtDocNo.Text.Trim();
        string uniqueTxId = txtBankUniqueTransactionID.Text;
        string[] irmList ={txtBankReferencenumber.Text,txtDateRealised.Text,_docno1,ddlIRMStatus.SelectedValue,txtIFSCCode.Text,txtRemittanceADCode.Text,
                       txtValueDate.Text,txt_relcur.Text,txtAmtRealised.Text,txtAmtRealisedinINR.Text,txtIECCode.Text,txtPanNumber.Text,txtRemitterName.Text,
                       txtRemitterCountry.Text,txtPurposeCode.Text,txtBankAccountNumber.Text};

        _docno = _docno.Replace("/", "_");

        // string filePath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/RealisationIRMJSON/" + _docno);// commemt by Anand 12-09-2023
        string filePath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/IRMJSON/" + date); // create by Anand 12-09-2023
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
            irmList = new List<object>{new{bankRefNumber=txtBankReferencenumber.Text.Trim(),
                        irmIssueDate =txtDateRealised.Text.Trim().Replace("/",""),
                        irmNumber = _docno1,
                        irmStatus = IRMStatus,
                        ifscCode = txtIFSCCode.Text.Trim(),
                        remittanceAdCode =txtRemittanceADCode.Text.Trim(),
                        remittanceDate =txtValueDate.Text.Trim().Replace("/",""),
                        remittanceFCC =txt_relcur.Text.Trim(),
                        remittanceFCAmount=Convert.ToDecimal(txtAmtRealised.Text.Trim()),// Anand 12-09-2023(convert the decimal)
                        inrCreditAmount=Convert.ToDecimal(txtAmtRealisedinINR.Text.Trim()),// Anand 12-09-2023(convert the decimal)
                        iecCode = txtIECCode.Text.Trim(),
                        panNumber = txtPanNumber.Text.Trim(),
                        remitterName =txtRemitterName.Text.Trim(),
                        remitterCountry =txtRemitterCountry.Text.Trim(),
                        purposeOfRemittance =txtPurposeCode.Text.Trim(),
                        bankAccountNo =txtBankAccountNumber.Text.Trim(),
                 }
            }
        };
        string _filePath = filePath + "/" + txtBankUniqueTransactionID.Text + "_" + IRMStatus + ".Json";
        string json = JsonConvert.SerializeObject(IRMData, Formatting.Indented).Trim().Replace("\": \"", "\":\"");
        //string json = JsonConvert.SerializeObject(IRMData, Formatting.Indented).Trim();
        System.IO.File.WriteAllText((_filePath), json);
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('JSON File is Created.');", true);
    }
    //-------------------End---------------------
    //-----------------------------------End----------------------------------------
}