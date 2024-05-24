using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class EXP_EXP_AddEditFullRealisation_Maker : System.Web.UI.Page
{
    string branchcode = "";
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
                chkManualGR.Attributes.Add("onclick", "return false;");
                if (Request.QueryString["mode"].Trim() != "add")
                {
                    txtDocNo.Enabled = false;
                    txtDocNo.Text = Request.QueryString["DocNo"].ToString();
                    branchcode = Request.QueryString["BranchCode"].ToString();
                    //-------------Anand02-09-2023--------------
                    if (branchcode == "792")
                    {
                        tbDocumentGOAccChange.Enabled = false;
                        btnNGO_Next.Visible = false;
                    }
                    else
                    {
                        tbDocumentGOAccChange.Enabled = true;
                    }
                    //---------------------End-----------------------
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

                    if (DocPrFx == "BCA" || DocPrFx == "BCU")
                    {
                        pnldocTypeInterest.Visible = false;
                    }
                    else
                    {
                        pnldocTypeInterest.Visible = true;
                    }

                    if (hdnBranchName.Value == "6770001")
                    {
                        hdnBranchNameEXPAc.Value = "Mumbai";
                    }
                    if (hdnBranchName.Value == "6770002")
                    {
                        hdnBranchNameEXPAc.Value = "New Delhi";
                    }
                    if (hdnBranchName.Value == "6770003")
                    {
                        hdnBranchNameEXPAc.Value = "Bangalore";
                    }
                    if (hdnBranchName.Value == "6770004")
                    {
                        hdnBranchNameEXPAc.Value = "Chennai";
                    }

                    txtDocPrFx.Value = DocPrFx;
                    hdnmode.Value = Request.QueryString["mode"].ToString();
                    SrNo = Request.QueryString["SrNo"].ToString();
                    txtSrNo.Text = SrNo;
                    fillDetails(txtDocNo.Text, SrNo);
                    Fillshippingbill();
                    btnDocNo.Visible = false;
                    txtDateRealised.Focus();
                    if (Request.QueryString["ForeignOrLocal"].Trim() == "F")
                    {
                        hdnForeignLocal.Value = "F";
                        Check_LEI_ThresholdLimit();
                        lblCreateIRM.Visible = true;
                        chkIRMCreate.Visible = true;
                    }
                    else
                    {
                        hdnForeignLocal.Value = "L";
                        btnLEI.Visible = false;
                        LocalTransField();
                    }
                    txtValueDate.Focus();
                }
                else
                {
                    DocPrFx = Request.QueryString["DocPrFx"].ToString();
                    txtForeignORLocal.Text = Request.QueryString["ForeignOrLocal"].ToString();
                    branchcode = Request.QueryString["BranchCode"].ToString();
                    //-------------Anand02-09-2023--------------
                    if (branchcode == "792")
                    {
                        tbDocumentGOAccChange.Enabled = false;
                        btnNGO_Next.Visible = false;
                    }
                    else
                    {
                        tbDocumentGOAccChange.Enabled = true;
                    }
                    //---------------------End-----------------------
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

                    if (DocPrFx == "BCA" || DocPrFx == "BCU")
                    {
                        pnldocTypeInterest.Visible = false;
                    }
                    else
                    {
                        pnldocTypeInterest.Visible = true;
                    }


                    if (hdnBranchName.Value == "6770001")
                    {
                        hdnBranchNameEXPAc.Value = "Mumbai";
                    }
                    if (hdnBranchName.Value == "6770002")
                    {
                        hdnBranchNameEXPAc.Value = "New Delhi";
                    }
                    if (hdnBranchName.Value == "6770003")
                    {
                        hdnBranchNameEXPAc.Value = "Bangalore";
                    }
                    if (hdnBranchName.Value == "6770004")
                    {
                        hdnBranchNameEXPAc.Value = "Chennai";
                    }

                    if (Request.QueryString["ForeignOrLocal"].Trim() == "F")
                    {
                        lblCreateIRM.Visible = true;
                        chkIRMCreate.Visible = true;
                    }
                    else
                    {

                        LocalTransField();
                    }
                    txtDocPrFx.Value = DocPrFx;
                    hdnBranchCode.Value = branchcode;
                    hdnYear.Value = year;
                    txtDateRealised.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    //Fillshippingbill();
                    //txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //rdbTransType.SelectedValue = "eefc";
                    //tran_type.Value = "EEFC";
                    if (DocPrFx == "B" || DocPrFx == "S")
                    {
                        txtExchangeRate.Text = "1";
                        rdbTransType.SelectedValue = "INR";
                    }
                    btnDocNo.Visible = true;
                    txtDocNo.Focus();
                }
                txtProcessingDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btnDocNo.Attributes.Add("onclick", "return DocHelp();");
                txtDocNo.Attributes.Add("onkeydown", "return OpenDocList(event);");
                //txtDateRealised.Focus();
                //txtDocNo.Focus();
                txtDocNo.Attributes.Add("onblur", "return chkdocno();");
                txtRelCrossCurRate.Attributes.Add("onblur", "return checkExchangeRate();");
                //txtExchangeRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtAmtRealised.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtAmtRealised.Attributes.Add("onchange", "return CalculateBillTab();");
                //txtEEFCAmt.Attributes.Add("onblur", "return calrealisedAmtinINR();");

                //rdbFull.Attributes.Add("onblur", "return calrealisedAmtinINR();");
                //rdbPart.Attributes.Add("onblur", "return calrealisedAmtinINR();");

                //txtCrossCurrRate.Attributes.Add("onblur", "return calcrosscurrTotal();");
                //txtOtherBank.Attributes.Add("onblur", "return calotheramtininr();");
                //txtCommission.Attributes.Add("onblur", "return calTax();");
                //txtCourier.Attributes.Add("onblur", "return calTax();");
                //txtSwift.Attributes.Add("onblur", "return calTax();");
                //txtBankCertificate.Attributes.Add("onblur", "return calTax();");
                ////btnTTNo.Attributes.Add("onclick", "return OpenDocNoList('mouseClick');");
                //btnEEFCCurrency.Attributes.Add("onclick", "return curhelp();");
                btn_recurrhelp.Attributes.Add("onclick", "return curhelp1();");
                //txtEEFCAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtCrossCurrRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtInterest.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtOtherBank.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtCollectionAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtSwift.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtCourier.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtBankCertificate.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtCommission.Attributes.Add("onkeydown", "return validate_Number(event);");
                ////txtInterestRate1.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtInterestRate2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDateRealised.Attributes.Add("onblur", "return checkSysDate();");
                txt_relamount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_relamount.Attributes.Add("onblur", "return chkamt();");
                //txtRelCrossCurRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtRelCrossCurRate.Attributes.Add("onblur", "return calfamtrealised();");
                //txtPartConAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtPartConAmt.Attributes.Add("onblur", "return calrealisedAmtinINR();");
                //btnOtrCrossCur.Attributes.Add("onclick", "return curhelp3();");
                //txtConCurRate.Attributes.Add("onblur", "return calcrosscurrTotal1();");
                //txtConCurRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtprofitamt.Attributes.Add("onblur", "return calTax();");
                //txtprofitamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //btnOverseasBankList.Attributes.Add("onclick", "return OpenOverseasBankList('mouseClick');");
                txtNoofDays2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtRealisedinINR.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtSBcesssamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txt_kkcessamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtsttamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtFxDlsCommission.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txt_fbkcharges.Attributes.Add("onblur", "return fbkcal();");

                //txtBillAmount.Attributes.Add("onblur", "return CallTwoFunctionsOnBillAmt();");
                //txtBENo.Attributes.Add("onblur", "return FillDraft();");
                //ddlCurrency.Attributes.Add("onblur", "return FillGBaseDetailsBYCurrency();");
                //txtCourierChrgs.Attributes.Add("onblur", "return FillGBaseDetailsBYCourierChrgs();");
                //txtMarginAmt.Attributes.Add("onblur", "return Calculate();");
                //txtNegotiatedAmt.Attributes.Add("onblur", "return FillGBaseDetailsBYNegoAmt();");
                //txtInterest.Attributes.Add("onblur", "return FillGBaseDetailsBYInterestAmt();");
                ///////////////////////////////// TT REF No /////////////////////////
                ddlTTRealisedCurr1.Attributes.Add("onblur", "return checkTTCurr(" + ddlTTCurrency1.ClientID + ddlTTRealisedCurr1.ClientID + ");");
                ddlTTRealisedCurr1.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency1.ClientID + "," + ddlTTRealisedCurr1.ClientID + ");");
                ddlTTRealisedCurr2.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency2.ClientID + "," + ddlTTRealisedCurr2.ClientID + ");");
                ddlTTRealisedCurr3.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency3.ClientID + "," + ddlTTRealisedCurr3.ClientID + ");");
                ddlTTRealisedCurr4.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency4.ClientID + "," + ddlTTRealisedCurr4.ClientID + ");");
                ddlTTRealisedCurr5.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency5.ClientID + "," + ddlTTRealisedCurr5.ClientID + ");");
                ddlTTRealisedCurr6.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency6.ClientID + "," + ddlTTRealisedCurr6.ClientID + ");");
                ddlTTRealisedCurr7.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency7.ClientID + "," + ddlTTRealisedCurr7.ClientID + ");");
                ddlTTRealisedCurr8.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency8.ClientID + "," + ddlTTRealisedCurr8.ClientID + ");");
                ddlTTRealisedCurr9.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency9.ClientID + "," + ddlTTRealisedCurr9.ClientID + ");");
                ddlTTRealisedCurr10.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency10.ClientID + "," + ddlTTRealisedCurr10.ClientID + ");");
                ddlTTRealisedCurr11.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency11.ClientID + "," + ddlTTRealisedCurr11.ClientID + ");");
                ddlTTRealisedCurr12.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency12.ClientID + "," + ddlTTRealisedCurr12.ClientID + ");");
                ddlTTRealisedCurr13.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency13.ClientID + "," + ddlTTRealisedCurr13.ClientID + ");");
                ddlTTRealisedCurr14.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency14.ClientID + "," + ddlTTRealisedCurr14.ClientID + ");");
                ddlTTRealisedCurr15.Attributes.Add("onchange", "return checkTTCurr(" + ddlTTCurrency15.ClientID + "," + ddlTTRealisedCurr15.ClientID + ");");

                // btnReimbBank.Attributes.Add("onclick", "return OpenReimbBankList('mouseClick');");              //comment by Anand26-06-2023
                // btnPayingBankList.Attributes.Add("onclick", "return OpenPayingBankList('mouseClick');");       //comment by Anand26-06-2023
                btnSavePrint.Attributes.Add("onclick", "return ValidateSave();");
                btnSave.Attributes.Add("onclick", "return ValidateSave();");

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
    protected void fillDetails(string docno, string SrNo)
    {
        //libor1.Value = libor;
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = docno;
        SqlParameter p2 = new SqlParameter("@srno", SqlDbType.VarChar);
        p2.Value = SrNo;
        string query = "TF_GetRealisedEntryDetailsNew_Maker";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);

        if (dt.Rows.Count > 0)
        {

            txtForeignORLocal.Text = dt.Rows[0]["ForeignORLocal"].ToString().Trim();

            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            fillCustomerIdDescription();

            txtDateReceived.Text = dt.Rows[0]["Date_Received"].ToString();
            txtDueDate.Text = dt.Rows[0]["Due_Date"].ToString();

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

            fillConsigneePartyDescription();
            txtRemitterCountry_TextChanged(null, null);
            txtRemBankCountry_TextChanged(null, null);

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
                txtAmtRealisedinINR.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Amount_In_Rs"].ToString()).ToString("0.00");

            if (dt.Rows[0]["LeiINRAmt"].ToString() != "")
                txtLeiInrAmt.Text = Convert.ToDecimal(dt.Rows[0]["LeiINRAmt"].ToString()).ToString("0.00");
            else
                txtLeiInrAmt.Text = Convert.ToDecimal(dt.Rows[0]["LeiINRAmt"].ToString()).ToString("0.00");

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
            txt_relcur_TextChanged(null, null);
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
                txtGO_ValueDate.Text = dt.Rows[0]["GO_ValueDate"].ToString();

                chk_Generaloperation1.Checked = true;
                chk_Generaloperation1_OnCheckedChanged(null, null);

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
                txtNGO_ValueDate.Text = dt.Rows[0]["NGO_ValueDate"].ToString();
                chk_Generaloperation2.Checked = true;
                chk_Generaloperation2_OnCheckedChanged(null, null);

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
                txtIO_ValueDate.Text = dt.Rows[0]["GO_Acc_Change_ValueDate"].ToString();
                chk_InterOffice.Checked = true;
                chk_InterOffice_OnCheckedChanged(null, null);

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
            string STATUSI = dt.Rows[0]["Checker_Status"].ToString().Trim();
            if (STATUSI == "A" || STATUSI == "C" || STATUSI == "I")
            {
                MakerVisible.Visible = false;
                FillshippingbillChecker();
                CheckerViewOnly();
            }
            else
            {
                MakerVisible.Visible = true;
                Fillshippingbill();
            }


        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewRealisationEntry_Maker.aspx", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewRealisationEntry_Maker.aspx", true);
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
        Fillshippingbill();
        txtValueDate.Focus();
        //string curr = txtCurrency.Text;
        //if (curr != "")
        //{
        //    if (txtDocPrFx.Value != "B" && txtDocPrFx.Value != "S")
        //        getExchangeRate(curr);
        //}
        //}
        //else
        //{
        //    //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Correct Document Number');", true);
        //    clearAll();
        //}

    }
    protected void getDetails(string docno)
    {
        string query = "TF_GetDocNoDetailsforExportRealisationNew_Maker";
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
            //////  as per anu request////
            txt_relcur.Text = dt.Rows[0]["Currency"].ToString();
            txt_relcur_TextChanged(null, null);

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
            //txtInterestRate1.Text = dt.Rows[0]["Interest_Rate_1"].ToString();
            //txtNoofDays1.Text = dt.Rows[0]["For_Days_1"].ToString();

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
            //txtBankCertificate.Text = dt.Rows[0]["Bank_Cert"].ToString();

            ///////////    added by shailesh   

            txtconsigneePartyID.Text = dt.Rows[0]["consigneePartyID"].ToString();
            fillConsigneePartyDescription();

            hdnMerchantTrade.Value = dt.Rows[0]["MerchantTrade"].ToString();
            txtpurposeofRemittance.Text = dt.Rows[0]["Invoice_No"].ToString();

            txtPurposeCode.Text = "P0102";
            txtPurposeCode_TextChanged(null, null);

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

                txtIntFrmDate1.Text = dt.Rows[0]["IntFromDate1"].ToString().Trim();
                txtIntToDate1.Text = dt.Rows[0]["IntToDate1"].ToString().Trim();
                txtForDays1.Text = dt.Rows[0]["For_Days_1"].ToString().Trim();
                txtIntRate1.Text = dt.Rows[0]["Interest_Rate_1"].ToString().Trim();

                txtInterestAmt.Text = dt.Rows[0]["Interest"].ToString();
            }

            getOutstandingAmt(txtDocNo.Text);
            fillOverseasPartyDescription();
            fillIssuingBankDescription();
            //txt_swiftcode_TextChanged(null, null);
            txtRemitterCountry_TextChanged(null, null);
            //txtRemBankCountry_TextChanged(null, null);


            //string interestquery = "TF_GetInterest_Realisation";
            //SqlParameter p2 = new SqlParameter("@docno", SqlDbType.VarChar);
            //p2.Value = docno;
            //TF_DATA Interestdata = new TF_DATA();
            //DataTable dt1 = Interestdata.getData(interestquery, p2);
            //if (dt1.Rows.Count > 0)
            //{
            //    txtInterestRate2.Text = dt1.Rows[0]["Interest_Rate_2"].ToString();
            //    txtNoofDays2.Text = dt1.Rows[0]["For_Days_2"].ToString();
            //}
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
        //SqlParameter p1 = new SqlParameter("@curr", SqlDbType.VarChar);
        //p1.Value = curr;
        //TF_DATA objData = new TF_DATA();
        //string query = "TF_GetReceivingBank";
        //DataTable dt = objData.getData(query, p1);
        //if (dt.Rows.Count > 0)
        //{
        //    ddlAccountType.DataSource = dt.DefaultView;
        //    ddlAccountType.DataTextField = "Abbreviation";
        //    ddlAccountType.DataValueField = "Abbreviation";
        //    ddlAccountType.DataBind();
        //}

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
            hdnCustabbr.Value = dt.Rows[0]["Cust_Abbr"].ToString().Trim();

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
        //SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        //p2.Value = "EXPORT";
        string _query = "TF_GetOverseasPartyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasParty.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            txtOverseasPartyCountry.Text = dt.Rows[0]["Party_Country"].ToString().Trim();
            CountryDescrption("1");
            txtRemitterName.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            txtRemitterCountry.Text = dt.Rows[0]["Party_Country"].ToString().Trim();
            txtRemitterAddress.Text = dt.Rows[0]["Party_Address"].ToString().Trim();

        }
        else
        {
            txtOverseasParty.Text = "";
            lblOverseasParty.Text = "";
            txtOverseasPartyCountry.Text = "";
        }

    }
    ////////////   added by Shailesh start
    protected void getOutstandingAmt(string docno)
    {
        TF_DATA objData = new TF_DATA();
        string query = "TF_GetOutstandingAmtForExpRealisation";
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = docno;
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
            txtOutstandingAmt.Text = dt.Rows[0]["OutstandingAmt"].ToString();
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
            //txtRemitterBankAddress.Focus();
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
    protected void txtConsigneePartyID_TextChanged(object sender, EventArgs e)
    {
        fillConsigneePartyDescription();
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

    public void FillIFSCTemittanceAdCode()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchCode", hdnBranchCode.Value);
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
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CustAcNo", txtCustAcNo.Text);
        DataTable dt = objData.getData("TF_GETPenCardDetails", p1);
        if (dt.Rows.Count > 0)
        {
            txtPanNumber.Text = dt.Rows[0]["PAN_No"].ToString();
            txtBankAccountNumber.Text = dt.Rows[0]["Password_Account_No"].ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Cust ABBR.')", true);
        }
    }
    protected void getICEcode()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@customerACNo", txtCustAcNo.Text);
        SqlParameter p2 = new SqlParameter("@branch", hdnBranchCode.Value);
        DataTable dt = objData.getData("TF_INW_GetCustDetails", p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtIECCode.Text = dt.Rows[0]["CUST_IE_CODE"].ToString();
        }
        else
        {
            txtIECCode.Text = "";
        }
    }

    protected void HeaderChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)GridViewGRPPCustomsDetails.HeaderRow.FindControl("HeaderChkAllow");
        if (chk.Checked)
        {
            for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("RowChkAllow");
                chkrow.Checked = true;
                Label lblAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblAmount");
                TextBox txtsettelementamt = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtsettelementamt");
                TextBox txtFBANK = (TextBox)GridViewGRPPCustomsDetails.Rows[0].FindControl("txtFBANK");
                DropDownList ddlpartfull = (DropDownList)GridViewGRPPCustomsDetails.Rows[i].FindControl("ddlpartfull");
                txtsettelementamt.Text = lblAmount.Text;
                txtFBANK.Text = "0.00";
                ddlpartfull.SelectedValue = "Full";
            }
        }
        else
        {
            for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("RowChkAllow");
                chkrow.Checked = false;
                TextBox txtsettelementamt = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtsettelementamt");
                TextBox txtFBANK = (TextBox)GridViewGRPPCustomsDetails.Rows[0].FindControl("txtFBANK");
                DropDownList ddlpartfull = (DropDownList)GridViewGRPPCustomsDetails.Rows[i].FindControl("ddlpartfull");
                txtsettelementamt.Text = "0.00";
                txtFBANK.Text = "0.00";
                ddlpartfull.SelectedValue = "";
            }
        }
        chk.Focus();
    }
    protected void RowChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;
        if (checkbox.Checked == true)
        {
            checkbox.Focus();
        }
        else
        {
            checkbox.Focus();
        }
        CheckBox chk = (CheckBox)GridViewGRPPCustomsDetails.HeaderRow.FindControl("HeaderChkAllow");
        int isAllChecked = 0;
        for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
                isAllChecked = 1;
            else
            {
                isAllChecked = 0;
                break;
            }
        }
        if (isAllChecked == 1)
            chk.Checked = true;
        else
            chk.Checked = false;
    }
    ////////////   added by Shailesh end
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

            // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "serviceTax", "calTax();", true);

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
                //dt1 = Convert.ToDateTime(txtValueDate.Text).Date;
                //dt2 = Convert.ToDateTime(txtDueDate.Text).Date;

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

        /*string query1 = "TF_EXP_GetInterestRates";
        DataTable dt1 = objData.getData(query1);
        decimal cal = 0;
        if (txtDocPrFx.Value == "E" || (txtDocPrFx.Value == "C" && libor1.Value != ""))
        {
            if (chkinterest == "O")
            {
                intrate3 = dt1.Rows[0]["EBROverdueInterestRate"].ToString();
                if (libor != "")
                    cal = Convert.ToDecimal(intrate1) + Convert.ToDecimal(libor) + Convert.ToDecimal(intrate3);
                txtInterestRate1.Text = Convert.ToString(cal);
                txtNoofDays1.Text = absdays.ToString();
                txtNoofDays2.Text = "0";
            }
            if (chkinterest == "R")
            {
                if (libor != "")
                    cal = Convert.ToDecimal(intrate1) + Convert.ToDecimal(libor);
                if (libor == "")
                {
                    decimal libor2 = 0;
                    cal = Convert.ToDecimal(intrate1) + libor2;
                }
                txtInterestRate1.Text = cal.ToString();
                txtNoofDays1.Text = days.ToString();
            }
            if (days == 0)
            {
                txtNoofDays1.Text = days.ToString();
            }
        }
        else
        {
            if (txtBillType.Text == "Sight Bill")
            {
                if (chkinterest == "O")
                {
                    intrate3 = dt1.Rows[0]["SightBillOverdueInterestRate"].ToString();
                    txtInterestRate1.Text = intrate3;
                    txtNoofDays1.Text = absdays.ToString();
                }
                if (chkinterest == "R")
                {
                    txtInterestRate1.Text = intrate1;
                    txtNoofDays1.Text = days.ToString();
                }
                if (days == 0)
                {
                    txtNoofDays1.Text = days.ToString();
                }
            }
            else
            {
                if (chkinterest == "O")
                {
                    intrate3 = dt1.Rows[0]["UsanceBillOverdueInterestRate"].ToString();
                    txtInterestRate1.Text = intrate3;
                    txtNoofDays1.Text = absdays.ToString();
                }
                if (chkinterest == "R")
                {
                    if (days > ndays2)
                    {
                        txtInterestRate1.Text = intrate1;
                        txtInterestRate2.Text = intrate2;
                        txtNoofDays2.Text = days2;
                        int caldays = days - ndays2;
                        txtNoofDays1.Text = caldays.ToString();
                    }
                    else
                    {
                        txtInterestRate1.Text = intrate2;
                        txtNoofDays1.Text = days.ToString();
                    }
                }
                if (days == 0)
                {
                    txtNoofDays1.Text = days.ToString();
                }
            }
        }
    }
    /*SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
    p1.Value = txtDocNo.Text;
    SqlParameter p2 = new SqlParameter("@ovrDays", SqlDbType.VarChar);
    p2.Value = txtNoofDays1.Text;
    string query = "TF_Export_Realisation_IntCal";
    TF_DATA objData = new TF_DATA();

    DataTable dt = objData.getData(query, p1, p2);
    if (dt.Rows.Count > 0)
    {
        txtInterestRate1.Text = dt.Rows[0]["OvrDueInt"].ToString();
        if (dt.Rows[0]["OvrInt"].ToString() != "")
        {
            txtInterest.Text = Convert.ToDecimal(dt.Rows[0]["OvrInt"].ToString()).ToString("0.00");
        }
        else
            txtInterest.Text = dt.Rows[0]["OvrInt"].ToString();
    }
    //decimal intrate1, intrate2;*/

    }
    protected void txtAmtRealised_TextChanged(object sender, EventArgs e)
    {
        if (txtDocNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Document No.')", true);
            chk_IMPACC1Flag.Checked = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "CalculateBillTab();", true);
            ////////////   added by Shailesh end
            if ((Convert.ToDouble(txtAmtRealised.Text) > Convert.ToDouble(txtBillAmt.Text)) && txtCurrency.Text == txt_relcur.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Realised amount cannot be greater than Bill Amount');", true);
            }
            if (txt_relamount.Text != "" && (Convert.ToDouble(txt_relamount.Text) > Convert.ToDouble(txtBillAmt.Text)) && txtCurrency.Text != txt_relcur.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Realised amount cannot be greater than Bill Amount');", true);
            }
            if ((Convert.ToDouble(txtAmtRealised.Text) > Convert.ToDouble(txtOutstandingAmt.Text)) && txtCurrency.Text == txt_relcur.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message1", "alert('Realised amount cannot be greater than balance amount for realisation');", true);
            }
            if (txt_relamount.Text != "" && (Convert.ToDouble(txt_relamount.Text) > Convert.ToDouble(txtOutstandingAmt.Text)) && txtCurrency.Text != txt_relcur.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message1", "alert('Realised amount cannot be greater than balance amount for realisation');", true);
            }
            rdbTransType.Focus();
            Check_LEI_ThresholdLimit();
        }
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
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "alert('Please enter Relized Amount first !!')", true);
            rdbFull.Checked = false;
        }
        else
        {
            if (txtCurrency.Text == txt_relcur.Text)
            {
                txtInstructedAmt.Text = txtOutstandingAmt.Text;
            }
            if (labelMessageSB.Text == " ")
            {
                CheckBox HeaderChkAllow = (CheckBox)GridViewGRPPCustomsDetails.HeaderRow.FindControl("HeaderChkAllow");
                HeaderChkAllow.Checked = true;
                HeaderChkAllow_CheckedChanged(null, null);
                Singleshippingbill();
            }

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "alert('You have selected Full Payment')", true);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calrealisedAmtinINR();alert('You have selected Full Payment')", true);
            rdbFull.Focus();
        }
    }
    protected void rdbPart_CheckedChanged(object sender, EventArgs e)
    {
        //calrealisedAmtinINR();
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "alert('Please enter Relized Amount first !!')", true);
            rdbPart.Checked = false;
        }
        else
        {
            if (labelMessageSB.Text == " ")
            {
                CheckBox HeaderChkAllow = (CheckBox)GridViewGRPPCustomsDetails.HeaderRow.FindControl("HeaderChkAllow");
                if (HeaderChkAllow.Checked)
                {
                    HeaderChkAllow.Checked = false;
                    HeaderChkAllow_CheckedChanged(null, null);
                }
                Singleshippingbill();
            }

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "alert('You have selected Part Payment')", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('You have selected Part Payment')", true);
            rdbPart.Focus();
        }
    }

    /*protected void getExchangeRate(string curr)
    {
        string purrate = "";
        string salerate = "";
        string asdate = txtDateRealised.Text;
        string asdate1 = DateTime.ParseExact(asdate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
        SqlParameter p1 = new SqlParameter("@curr", SqlDbType.VarChar);
        p1.Value = curr;
        SqlParameter p2 = new SqlParameter("@asondate", SqlDbType.VarChar);
        p2.Value = asdate1;
        string query = "TF_EXPORTGetExchangeRate";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtExchangeRate.Text = dt.Rows[0]["PurchaseRate"].ToString();
            purrate = dt.Rows[0]["PurchaseRate"].ToString();
            salerate = dt.Rows[0]["SaleRate"].ToString();
            purrate1.Value = purrate;
            overrate.Value = salerate;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please update the Currency card rate master');", true);
            txtExchangeRate.Focus();
        }
    }

    /*private string getPCsrNo(string bName, string AcNo, string SubAcNo)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bName", SqlDbType.VarChar);
        p1.Value = bName;

        SqlParameter p2 = new SqlParameter("@pcAcNo", SqlDbType.VarChar);
        p2.Value = AcNo;

        SqlParameter p3 = new SqlParameter("@subAcNo", SqlDbType.VarChar);
        p3.Value = SubAcNo;

        string _query = "TF_INW_GetLastDocNo_Liquidation"; //--------- kept same as imward liquidation
        DataTable dt = objData.getData(_query, p1, p2, p3);
        string srno = "";
        if (dt.Rows.Count > 0)
        {

            srno = dt.Rows[0]["LastDocNo"].ToString();
        }
        return srno;
    }*/
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
        //txtInterestRate1.Text = "";
        //txtNoofDays1.Text = "";
        chkLoanAdvanced.Checked = false;
        //txtNYRefNo.Text = "";
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
        //txtPCAcNo1.Text = "";
        //txtPCAcNo2.Text = "";
        //txtPCAcNo3.Text = "";
        //txtPCAcNo4.Text = "";
        //txtPCAcNo5.Text = "";
        //txtPCAcNo6.Text = "";
        //txtPCAmount1.Text = "";
        //txtPCAmount2.Text = "";
        //txtPCAmount3.Text = "";
        //txtPCAmount4.Text = "";
        //txtPCAmount5.Text = "";
        //txtPCAmount6.Text = "";
        //txtPCsubAcNo1.Text = "";
        //txtPCsubAcNo2.Text = "";
        //txtPCsubAcNo3.Text = "";
        //txtPCsubAcNo4.Text = "";
        //txtPCsubAcNo5.Text = "";
        //txtPCsubAcNo6.Text = "";
        //txtTotalPCLiquidated.Text = "";
        //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "CalculateTotalPC();", true);
    }
    //protected void chkStax_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkStax.Checked == true)
    //    {
    //        fillTaxRates();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "calTax();", true);
    //    }
    //    else
    //    {
    //        ddlServicetax.Items.Clear();
    //    }
    //}
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
            //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "calTax();", true);
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
            chkIRMCreate.Enabled = false;
            btnTTRefNoList.Enabled = false;
        }
        if (chkFirc.Checked == false)
        {
            chkFirc.Text = "No";
            txtFircNo.Text = "";
            txtFircNo.Enabled = false;
            txtFircAdCode.Text = "";
            txtFircAdCode.Enabled = false;
            btnTTRefNoList.Enabled = true;
            if (chkDummySettlement.Checked == true)
            {
                chkIRMCreate.Enabled = false;
            }
            else
            {
                chkIRMCreate.Enabled = true;
            }
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

    //protected Boolean Cmmp_TTamt_RealAmt()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
    //    p1.Value = hdnBranchCode.Value;

    //    SqlParameter p2 = new SqlParameter("@ttrefno1", SqlDbType.VarChar);
    //    p2.Value = btnTTRef1.Text.Trim();

    //    SqlParameter p3 = new SqlParameter("@ttrefno2", SqlDbType.VarChar);
    //    p3.Value = btnTTRef2.Text.Trim();

    //    SqlParameter p4 = new SqlParameter("@ttrefno3", SqlDbType.VarChar);
    //    p4.Value = btnTTRef3.Text.Trim();

    //    SqlParameter p5 = new SqlParameter("@ttrefno4", SqlDbType.VarChar);
    //    p5.Value = btnTTRef4.Text.Trim();

    //    SqlParameter p6 = new SqlParameter("@ttrefno5", SqlDbType.VarChar);
    //    p6.Value = btnTTRef5.Text.Trim();

    //    SqlParameter p7 = new SqlParameter("@realamtinr", SqlDbType.VarChar);
    //    p7.Value = txtAmtRealisedinINR.Text.Trim();

    //    SqlParameter p8 = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
    //    p8.Value = txtCustAcNo.Text.Trim();


    //    string _query = "TF_CpmrTTamt_Realamt";
    //    DataTable dt = objData.getData(_query, p1,p2,p3,p4,p5,p6,p7,p8);
    //    if (dt.Rows.Count > 0)
    //    {
    //        return true;   
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}
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
                        btnSave.Visible = false;
                        //btnSavePrint.Visible = false;
                        LEIAmtCheck.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit. ";
                        LEIverify.Text = "Please Verify LEI.";
                        NotverifyMSG = "This transaction cannot proceed because the LEI Details has not been verified.";
                        //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This transaction cannot proceed because the LEI Details has not been verified.')", true);
                    }
                    else
                    {
                        btnSave.Visible = true;
                        //btnSavePrint.Visible = true;
                        NotverifyMSG = "";
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
        if (txt_relcur.Text != txtCurrency.Text)
        {
            txt_relamount.Enabled = true;
            txtRelCrossCurRate.Enabled = true;
            txt_relamount.Focus();
        }
        else
        {
            txt_relamount.Enabled = false;
            txtRelCrossCurRate.Enabled = false;
            txt_relamount.Text = ""; txtRelCrossCurRate.Text = "";
            txtInstructedAmt.Focus();
        }

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
        DataTable dt = objsave.getData("TF_EXP_filldetailsShippingbill", p1, p2, p3);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewGRPPCustomsDetails.DataSource = dt.DefaultView;
            GridViewGRPPCustomsDetails.DataBind();
            GridViewGRPPCustomsDetails.Visible = true;

            labelMessageSB.Text = " ";
            labelMessageSB.Visible = false;
            lblPeningSB.Visible = true;
            chkSB.Visible = true;
            lblSB.Visible = true;
        }
        else
        {
            GridViewGRPPCustomsDetails.Visible = false;
            labelMessageSB.Text = "No record(s) found.";
            labelMessageSB.Visible = true;
            lblPeningSB.Visible = true;
            chkSB.Visible = true;
            lblSB.Visible = true;

        }
    }
    protected void Calcshippingbill()
    {
        Validateshippingbill();
        hdnShippingbillAmtCheck.Value = "";
        decimal TotalAmtcheck = 0;
        decimal DocAmtcheck = 0;
        decimal SBAmtcheck = 0;
        decimal FBankAmtcheck = 0;
        decimal FBankcheck = 0;
        for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
            {
                Label lblAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblAmount");
                TextBox txtsettelementamt = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtsettelementamt");
                TextBox txtFBANK = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtFBANK");
                SBAmtcheck = decimal.Parse(txtsettelementamt.Text);
                FBankAmtcheck = decimal.Parse(txtFBANK.Text);
                if (GridViewGRPPCustomsDetails.Rows.Count > 1)
                {
                    DocAmtcheck = DocAmtcheck + SBAmtcheck;
                    TotalAmtcheck = TotalAmtcheck + decimal.Parse(lblAmount.Text);
                    FBankcheck = FBankcheck + FBankAmtcheck;
                }
                else
                {
                    DocAmtcheck = SBAmtcheck;
                    TotalAmtcheck = decimal.Parse(lblAmount.Text);
                    FBankcheck = FBankAmtcheck;
                }
                if ((decimal.Parse(lblAmount.Text) < SBAmtcheck) && txtCurrency.Text == txt_relcur.Text)
                {
                    hdnShippingbillAmtCheck.Value = "SBGreater";
                }
            }
        }
        if (chkSB.Checked == false)
        {
            if ((decimal.Parse(txtAmtRealised.Text) != DocAmtcheck) && txtCurrency.Text == txt_relcur.Text)
            {
                hdnShippingbillAmtCheck.Value = "SGreater";
            }
            if ((decimal.Parse(txt_fbkcharges.Text) != FBankcheck) && txtCurrency.Text == txt_relcur.Text)
            {
                hdnShippingbillAmtCheck.Value = "FGreater";
            }
        }
    }
    protected void Validateshippingbill()
    {
        hdnShippingbillValidate.Value = "";
        for (
                    int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("RowChkAllow");

            Label lblAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblAmount");
            TextBox txtsettelementamt = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtsettelementamt");
            TextBox txtFBANK = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtFBANK");
            DropDownList ddlpartfull = (DropDownList)GridViewGRPPCustomsDetails.Rows[i].FindControl("ddlpartfull");

            if (txtsettelementamt.Text != " " && txtsettelementamt.Text != "" && txtsettelementamt.Text != "0.00")
            {
                if (chkrow.Checked == false)
                {
                    hdnShippingbillValidate.Value = "True";
                }
            }
            if (chkrow.Checked == true && (txtsettelementamt.Text == "" || txtsettelementamt.Text == "0.00"))
            {
                hdnShippingbillValidate.Value = "SBAmt";
            }
            if (chkrow.Checked == true && ddlpartfull.Text == "")
            {
                hdnShippingbillValidate.Value = "SBIND";
            }
        }
    }
    protected void Singleshippingbill()
    {
        if (labelMessageSB.Text == " ")
        {
            string doc = txtDocNo.Text.Trim();
            SqlParameter p1 = new SqlParameter("@Document_No", doc);
            SqlParameter p2 = new SqlParameter("@mode", "add");
            SqlParameter p3 = new SqlParameter("@SrNo", txtSrNo.Text);
            TF_DATA objsave = new TF_DATA();
            DataTable dt = objsave.getData("TF_EXP_filldetailsShippingbill", p1, p2, p3);

            if (dt.Rows.Count == 1)
            {
                if (GridViewGRPPCustomsDetails.Rows.Count == 1)
                {
                    CheckBox HeaderChkAllow = (CheckBox)GridViewGRPPCustomsDetails.HeaderRow.FindControl("HeaderChkAllow");
                    HeaderChkAllow.Checked = true;

                    CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[0].FindControl("RowChkAllow");
                    chkrow.Checked = true;
                    Label lblAmount = (Label)GridViewGRPPCustomsDetails.Rows[0].FindControl("lblAmount");
                    TextBox txtsettelementamt = (TextBox)GridViewGRPPCustomsDetails.Rows[0].FindControl("txtsettelementamt");
                    TextBox txtFBANK = (TextBox)GridViewGRPPCustomsDetails.Rows[0].FindControl("txtFBANK");
                    DropDownList ddlpartfull = (DropDownList)GridViewGRPPCustomsDetails.Rows[0].FindControl("ddlpartfull");

                    if (txtAmtRealised.Text == "")
                    {
                        txtsettelementamt.Text = "0.00";
                    }
                    else
                    {
                        txtsettelementamt.Text = txtAmtRealised.Text;
                    }
                    if (txt_fbkcharges.Text == "")
                    {
                        txtFBANK.Text = "0.00";
                    }
                    else
                    {
                        txtFBANK.Text = txt_fbkcharges.Text;
                    }
                    if (rdbPart.Checked == true)
                    {
                        ddlpartfull.SelectedValue = "Part";
                    }
                    else if (rdbFull.Checked == true)
                    {
                        ddlpartfull.SelectedValue = "Full";
                    }
                }
            }
        }
    }

    protected void FillshippingbillChecker()
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
            GridViewGRPPCustomsDetails.Enabled = false;

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
            lblPeningSB.Visible = false;
            chkSB.Visible = false;
            lblSB.Visible = false;
        }
    }

    //private void fillReimbBankDescription()
    //{
    //    lblReimbBankDesc.Text = "";
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
    //    p1.Value = txtReimbBank.Text;
    //    string _query = "TF_GetReimburseBankMasterDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblReimbBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();

    //        if (lblReimbBankDesc.Text.Length > 20)
    //        {
    //            lblReimbBankDesc.ToolTip = lblReimbBankDesc.Text;
    //            lblReimbBankDesc.Text = lblReimbBankDesc.Text.Substring(0, 20) + "...";

    //        }

    //        txtSpecialInstructions1.Text = dt.Rows[0]["Special_Instructions1"].ToString().Trim();
    //        txtSpecialInstructions2.Text = dt.Rows[0]["Special_Instructions2"].ToString().Trim();
    //        txtSpecialInstructions3.Text = dt.Rows[0]["Special_Instructions3"].ToString().Trim();
    //        txtSpecialInstructions4.Text = dt.Rows[0]["Special_Instructions4"].ToString().Trim();
    //        txtSpecialInstructions5.Text = dt.Rows[0]["Special_Instructions5"].ToString().Trim();
    //        txtSpecialInstructions6.Text = dt.Rows[0]["Special_Instructions6"].ToString().Trim();
    //        txtSpecialInstructions7.Text = dt.Rows[0]["Special_Instructions7"].ToString().Trim();
    //        txtSpecialInstructions8.Text = dt.Rows[0]["Special_Instructions8"].ToString().Trim();
    //        txtSpecialInstructions9.Text = dt.Rows[0]["Special_Instructions9"].ToString().Trim();
    //        txtSpecialInstructions10.Text = dt.Rows[0]["Special_Instructions10"].ToString().Trim();
    //    }
    //    else
    //    {
    //        txtReimbBank.Text = "";
    //        lblReimbBankDesc.Text = "";

    //        txtSpecialInstructions1.Text = "";
    //        txtSpecialInstructions2.Text = "";
    //        txtSpecialInstructions3.Text = "";
    //        txtSpecialInstructions4.Text = "";
    //        txtSpecialInstructions5.Text = "";
    //        txtSpecialInstructions6.Text = "";
    //        txtSpecialInstructions7.Text = "";
    //        txtSpecialInstructions8.Text = "";
    //        txtSpecialInstructions9.Text = "";
    //        txtSpecialInstructions10.Text = "";

    //        txtReimbBank.Focus();
    //    }

    //}
    protected void txtReimbBank_TextChanged(object sender, EventArgs e)
    {
        // fillReimbBankDescription();
    }
    //private void fillPayingBankDescription()
    //{
    //    //lblPayingBankDesc.Text = "";
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
    //   // p1.Value = txtPayingBankID.Text;
    //    string _query = "TF_GetOverseasBankMasterDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblPayingBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
    //        if (lblPayingBankDesc.Text.Length > 25)
    //        {
    //            lblPayingBankDesc.ToolTip = lblPayingBankDesc.Text;
    //            lblPayingBankDesc.Text = lblPayingBankDesc.Text.Substring(0, 25) + "...";
    //        }
    //    }
    //    else
    //    {
    //        txtPayingBankID.Text = "";
    //        lblPayingBankDesc.Text = "";
    //    }
    // }
    protected void txtPayingBankID_TextChanged(object sender, EventArgs e)
    {
        //fillPayingBankDescription();
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
    //protected void txtDRCustAbbr1_TextChanged(object sender, EventArgs e)
    //{
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
    //    p1.Value = txtDRCustAbbr1.Text.Trim();
    //    string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {

    //        txtDRGLCode1.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
    //        txtDRCustAcNo11.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
    //        txtDRCustAcNo12.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
    //        txtDRCurr1.Focus();
    //    }
    //    else
    //    {

    //        txtDRCustAbbr1.Focus();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
    //    }
    //}
    //protected void txtDRCustAbbr2_TextChanged(object sender, EventArgs e)
    //{
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
    //    p1.Value = txtDRCustAbbr2.Text.Trim();
    //    string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {

    //        txtDRGLCode2.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
    //        txtDRCustAcNo21.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
    //        txtDRCustAcNo22.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
    //        txtDRCurr2.Focus();
    //    }
    //    else
    //    {

    //        txtDRCustAbbr2.Focus();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
    //    }
    //}
    //protected void txtDRCustAbbr3_TextChanged(object sender, EventArgs e)
    //{
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
    //    p1.Value = txtDRCustAbbr3.Text.Trim();
    //    string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {

    //        txtDRGLCode3.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
    //        txtDRCustAcNo31.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
    //        txtDRCustAcNo32.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
    //        txtDRCurr3.Focus();
    //    }
    //    else
    //    {

    //        txtDRCustAbbr3.Focus();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
    //    }
    //}
    //protected void txtDRCustAbbr4_TextChanged(object sender, EventArgs e)
    //{
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
    //    p1.Value = txtDRCustAbbr4.Text.Trim();
    //    string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {

    //        txtDRGLCode4.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
    //        txtDRCustAcNo41.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
    //        txtDRCustAcNo42.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
    //        txtDRCurr4.Focus();
    //    }
    //    else
    //    {
    //        txtDRCustAbbr4.Focus();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
    //    }
    //}
    //protected void txtDRCustAbbr5_TextChanged(object sender, EventArgs e)
    //{
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
    //    p1.Value = txtDRCustAbbr5.Text.Trim();
    //    string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        txtDRGLCode5.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
    //        txtDRCustAcNo51.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
    //        txtDRCustAcNo52.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
    //        txtDRCurr5.Focus();
    //    }
    //    else
    //    {
    //        txtDRCustAbbr5.Focus();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Boolean flag = true;
        string intrate1 = "";
        string intrate2 = txtInterestRate2.Text;
        //string days1=txtNoofDays1.Text;
        //string days2=txtNoofDays2.Text;
        if (labelMessageSB.Text == " ")
        {
            Calcshippingbill();
            Validateshippingbill();
        }
        if (btnTTRefNoList.Checked == true)
        {
            ValidateTTCurr();
            TTFIRCtotalCaluation();
        }
        else
        {
            hdnTTCurrCheck.Value = "";
            hdnTTFIRCTotalAmtCheck.Value = "";
        }
        if (chk_IMPACC1Flag.Checked == true || chk_IMPACC2Flag.Checked == true || chk_IMPACC3Flag.Checked == true || chk_IMPACC4Flag.Checked == true || chk_IMPACC5Flag.Checked == true)
        {
            AccountAmtCheck();
        }
        int intdays = 0;
        TF_DATA objData = new TF_DATA();
        if (chkLoanAdvanced.Checked == true && txtDateDelinked.Text == "")
        {
            if (txtValueDate.Text != "" && txtDueDate.Text != "")
            {
                string query = "getdatediff";
                SqlParameter p1 = new SqlParameter("@dt2", SqlDbType.VarChar);
                p1.Value = txtValueDate.Text;
                SqlParameter p2 = new SqlParameter("@dt1", SqlDbType.VarChar);
                p2.Value = txtDueDate.Text;
                DataTable dt = objData.getData(query, p1, p2);
                if (dt.Rows.Count > 0)
                    intdays = Convert.ToInt32(dt.Rows[0]["Diff"].ToString());
            }
        }
        if (txtDocNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Document number cannot be blank');", true);
            flag = false;
        }
        if (rdbFull.Checked == false && rdbPart.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please check either Full or Part Payment');", true);
            flag = false;
            rdbFull.Focus();
        }
        if (txtValueDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Value Date cannot be blank');", true);
            flag = false;
            txtValueDate.Focus();
        }
        if (txt_relcur.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Realised Currency Date cannot be blank');", true);
            flag = false;
            txt_relcur.Focus();
        }

        if (chkFirc.Checked == false && txtForeignORLocal.Text == "F" && txtTTRefNo1.Text == "" && txtTTRefNo2.Text == "" && txtTTRefNo3.Text == "" && txtTTRefNo4.Text == "" && txtTTRefNo5.Text == "" && txtOverseasBank.Text == "" && txtSwiftCode.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Overseas Bank If FIRC or ITT Ref. is Not Present');", true);
            flag = false;
            //btnOverseasBankList.Focus();
        }

        if (hdnTTCurrCheck.Value == "TTfalse")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT cross curr rate.');", true);
            flag = false;
        }
        if (hdnTTFIRCTotalAmtCheck.Value == "Greater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('ITT Amount Total should not be More Than Realized Amount!.');", true);
            flag = false;
        }
        if (hdnTTFIRCTotalAmtCheck.Value == "mismatch")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('ITT Amount mismatch with Realized Amount!.');", true);
            flag = false;
        }
        if (chkFirc.Checked == false && txtForeignORLocal.Text == "F" && txtTTRefNo1.Text == "" && txtTTRefNo2.Text == "" && txtTTRefNo3.Text == "" && txtTTRefNo4.Text == "" && txtTTRefNo5.Text == "" && txtOverseasBank.Text != "" && txtSwiftCode.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Swift Code In Overseas Bank Master!');", true);
            flag = false;
            //btnOverseasBankList.Focus();
        }
        if (hdnShippingbillValidate.Value == "SBIND")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please select Full/Part Indicator in shipping Bill.');", true);
            flag = false;
        }
        if (hdnShippingbillValidate.Value == "True")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please select checkbox in shipping Bill.');", true);
            flag = false;
        }
        if (hdnShippingbillValidate.Value == "SBAmt")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('SB Settlement Amount can not be Blank.');", true);
            flag = false;
        }
        if (hdnShippingbillAmtCheck.Value == "SBGreater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Settlement Amt + FBANK Amt should not be More Than SB Amount!');", true);
            flag = false;
        }
        if (hdnShippingbillAmtCheck.Value == "SGreater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('SB Settlement Amt mismatch with Realized Amount!');", true);
            flag = false;
        }
        if (hdnShippingbillAmtCheck.Value == "FGreater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('SB FBANK Amt mismatch with Fbank charges Amount!');", true);
            flag = false;
        }
        if (hdnAccountAmtcheck.Value == "mismatch")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Realized Amount mismatch with Export Accounting Amount!');", true);
            flag = false;
        }
        if (flag == true)
        {
            if (hdnLeiFlag.Value == "Y" && lblLEI_CUST_Remark.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify LEI details.');", true);
            }
            else if ((hdnLeiSpecialFlag.Value == "Y" || hdnLeiSpecialFlag.Value == "R") && lblLEI_CUST_Remark.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify Recurring LEI Customer.');", true);
            }
            else
            {
                string _userName = Session["userName"].ToString().Trim();
                string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                string _mode = Request.QueryString["mode"].Trim();
                DocPrFx = Request.QueryString["DocPrFx"].ToString();
                branchcode = Request.QueryString["BranchCode"].ToString();

                SqlParameter p1 = new SqlParameter("@user", _userName);
                SqlParameter p2 = new SqlParameter("@addedtime", _uploadingDate);
                SqlParameter mode = new SqlParameter("@mode", _mode);
                SqlParameter docno = new SqlParameter("@docno", txtDocNo.Text);
                SqlParameter srno = new SqlParameter("@SrNo", txtSrNo.Text);
                SqlParameter doctype = new SqlParameter("@doctype", DocPrFx);
                SqlParameter branch = new SqlParameter("@branch", branchcode);
                string bill = "";
                SqlParameter billtype = new SqlParameter("@billtype", bill);
                SqlParameter custacno = new SqlParameter("@custacno", txtCustAcNo.Text);
                SqlParameter opc = new SqlParameter("@opc", txtOverseasParty.Text);
                SqlParameter RemiName = new SqlParameter("@remitterName", txtRemitterName.Text.Trim());
                SqlParameter RemiCotry = new SqlParameter("@remitterCountry", txtRemitterCountry.Text.Trim());
                SqlParameter RemiAdd = new SqlParameter("@Remitter_Address", txtRemitterAddress.Text.Trim());
                SqlParameter pconsigneeID = new SqlParameter("@consigneePartyID", txtconsigneePartyID.Text);
                SqlParameter obc = new SqlParameter("@obc", txtOverseasBank.Text);
                SqlParameter PSwiftCode = new SqlParameter("@SwiftCode", txtSwiftCode.Text);
                SqlParameter pRB = new SqlParameter("@Remitter_Bank", txtRemitterBank.Text.Trim());
                SqlParameter pRBA = new SqlParameter("@Remitter_Bank_Address", txtRemitterBankAddress.Text.Trim());
                SqlParameter pRBC = new SqlParameter("@Remitter_Bank_Country", txtRemBankCountry.Text.Trim());
                SqlParameter p20 = new SqlParameter("@Purpose_Of_Remittance", txtpurposeofRemittance.Text.Trim());
                SqlParameter pProcessingDate = new SqlParameter("@ProcessingDate", txtProcessingDate.Text);
                SqlParameter realdate = new SqlParameter("@realdate", txtDateRealised.Text);
                SqlParameter valuedate = new SqlParameter("@valuedate", txtValueDate.Text);
                SqlParameter exrate = new SqlParameter("@exrate", txtExchangeRate.Text);
                SqlParameter realamt = new SqlParameter("@realamt", txtAmtRealised.Text);
                SqlParameter realamtinr = new SqlParameter("@realamtinr", txtAmtRealisedinINR.Text);

                //SqlParameter swift = new SqlParameter("@swift", txtSwift.Text);
                //SqlParameter bankcert = new SqlParameter("@bankcert", txtBankCertificate.Text);
                //SqlParameter courier = new SqlParameter("@courier", txtCourier.Text);

                //SqlParameter days = new SqlParameter("@days", intdays);

                //SqlParameter otherbankcharges = new SqlParameter("@otherbankcharges", SqlDbType.VarChar);

                //if (txtOtherBankinINR.Text == "")
                //{
                //    decimal x = 0;
                //    string x1 = Convert.ToString(x);
                //    otherbankcharges.Value = x1;
                //}
                //else
                //    otherbankcharges.Value = txtOtherBank.Text;

                //SqlParameter otherbankchargesinr = new SqlParameter("@otherbankchargesinr", SqlDbType.VarChar);
                //otherbankchargesinr.Value = txtOtherBankinINR.Text;

                //SqlParameter interest1 = new SqlParameter("@interest1", "");
                //SqlParameter days1 = new SqlParameter("@days1", "");
                //SqlParameter interest2 = new SqlParameter("@interest2", txtInterestRate2.Text);
                //SqlParameter days2 = new SqlParameter("@days2", txtNoofDays2.Text);
                //SqlParameter interestamt = new SqlParameter("@interestamt", txtInterest.Text);
                //SqlParameter interestamtinr = new SqlParameter("@interestamtinr", txtInterestinINR.Text);

                SqlParameter remark = new SqlParameter("@remark", txtRemark.Text);
                SqlParameter nyrefno = new SqlParameter("@nyrefno", "");
                string payind = "";
                if (rdbFull.Checked == true)
                    payind = "F";
                if (rdbPart.Checked == true)
                    payind = "P";
                SqlParameter payind1 = new SqlParameter("@payind", payind);
                SqlParameter colamt = new SqlParameter("@colamt", txtCollectionAmt.Text);
                SqlParameter colamtinr = new SqlParameter("@colamtinr", SqlDbType.VarChar);
                if (txtCollectionAmtinINR.Text == "")
                {
                    decimal x = 0;
                    string x1 = Convert.ToString(x);
                    colamtinr.Value = x1;
                }
                else
                    colamtinr.Value = txtCollectionAmtinINR.Text;

                string loan = "";
                if (chkLoanAdvanced.Checked == true)
                    loan = "Y";
                else
                    loan = "N";
                SqlParameter loan1 = new SqlParameter("@loan", loan);
                string bankline = "";
                if (chkBank.Checked == true)
                    bankline = "Y";
                else
                    bankline = "N";
                SqlParameter bkline = new SqlParameter("@bankline", bankline);
                SqlParameter CrossrealCur = new SqlParameter("@CrossrealCur", txt_relcur.Text);
                SqlParameter RelCrossCurRate = new SqlParameter("@RelCrossCurRate", txtRelCrossCurRate.Text);
                SqlParameter CrossrealAmt = new SqlParameter("@CrossrealAmt", txt_relamount.Text);
                decimal b = 0;
                decimal r = Convert.ToDecimal(txtAmtRealised.Text);
                decimal bi = Convert.ToDecimal(txtBillAmt.Text);
                b = bi - r;
                string bl = Convert.ToString(b);
                SqlParameter bal = new SqlParameter("@balamtforpaytype", bl);

                string payer_st;
                if (rdbShipper.Checked)
                {
                    payer_st = "S";
                }
                else
                {
                    payer_st = "B";
                }
                SqlParameter Payer = new SqlParameter("@Payer", payer_st);

                SqlParameter pIntFrom = new SqlParameter("@IntsFrom", txtIntFrmDate1.Text);
                SqlParameter pIntTo = new SqlParameter("@IntTo", txtIntToDate1.Text);
                SqlParameter pIntDays = new SqlParameter("@IntDays", txtForDays1.Text);
                SqlParameter pIntRate = new SqlParameter("@IntRate", txtIntRate1.Text);

                SqlParameter pInterest = new SqlParameter("@Interest", txtInterestAmt.Text);
                SqlParameter FBKCharge = new SqlParameter("@FBKCharge", txt_fbkcharges.Text);
                SqlParameter pcfcamt = new SqlParameter("@pcfcamt", txtPcfcAmt.Text);
                SqlParameter pEEfcAmt = new SqlParameter("@EEfcAmt", txtEEFCAmt.Text);
                SqlParameter overdueamt = new SqlParameter("@overdueamt", txtOverDue.Text);

                SqlParameter POutstandingAmt = new SqlParameter("@OutstandingAmt", txtOutstandingAmt.Text.Trim());
                SqlParameter PInstructedAmt = new SqlParameter("@InstructedAmt", txtInstructedAmt.Text.Trim());
                SqlParameter PLeiExchRate = new SqlParameter("@LeiExchRate", lbl_Exch_rate.Text.Trim());
                SqlParameter pLeiInrAmt = new SqlParameter("@LeiInrAmt", txtLeiInrAmt.Text);

                string CkIRM = "";
                if (chkIRMCreate.Checked == true)
                    CkIRM = "Y";
                else
                    CkIRM = "N";
                SqlParameter CheckIRM = new SqlParameter("@CheckIRM", CkIRM);

                SqlParameter pIBUTransID = new SqlParameter("@BankUniqueTransactionID", txtBankUniqueTransactionID.Text.Trim());
                SqlParameter pIIFSCCode = new SqlParameter("@IFSCCode", txtIFSCCode.Text.Trim());
                SqlParameter pIRemiADcode = new SqlParameter("@RemittanceADCode", txtRemittanceADCode.Text.Trim());
                SqlParameter pIIECcode = new SqlParameter("@IECCode", txtIECCode.Text.Trim());
                SqlParameter pIPanNo = new SqlParameter("@PanNumber", txtPanNumber.Text.Trim());
                SqlParameter pIModeOfPay = new SqlParameter("@ModeofPayment", ddlModeOfPayment.SelectedValue);
                //SqlParameter pIFactFlag = new SqlParameter("@Factoringflag", ddlFactoringflag.SelectedValue);
                //SqlParameter pIForfFlag = new SqlParameter("@Forfeitingflag", ddlForfeitingflag.SelectedValue);
                SqlParameter pIPurposeCode = new SqlParameter("@purposeCode", txtPurposeCode.Text.Trim());
                SqlParameter pBankReferencenumber = new SqlParameter("@BankReferencenumber", txtBankReferencenumber.Text.Trim());
                SqlParameter pBankAccountNumber = new SqlParameter("@BankAccountNumber", txtBankAccountNumber.Text.Trim());
                SqlParameter pIRMStatus = new SqlParameter("@IRMStatus", SqlDbType.VarChar);
                if (ddlIRMStatus.SelectedValue == "Fresh")
                {
                    pIRMStatus.Value = "F";
                }
                //else if (ddlIRMStatus.SelectedValue == "Amended")
                //{
                //    pIRMStatus.Value = "A";
                //}
                else if (ddlIRMStatus.SelectedValue == "Cancelled")
                {
                    pIRMStatus.Value = "C";
                }

                string CkTT = "";
                if (btnTTRefNoList.Checked == true)
                    CkTT = "Y";
                else
                    CkTT = "N";
                SqlParameter CheckTT = new SqlParameter("@CheckTT", CkTT);

                string CheckDummy = "N";
                if (chkDummySettlement.Checked)
                    CheckDummy = "Y";
                SqlParameter pCCheckDummy = new SqlParameter("@CheckDummySettlement", CheckDummy);

                SqlParameter TTREFNO1 = new SqlParameter("@TTREFNO1", txtTTRefNo1.Text);
                SqlParameter TTAmt1 = new SqlParameter("@TTAmt1", txtTTAmount1.Text);
                SqlParameter TTREFNO2 = new SqlParameter("@TTREFNO2", txtTTRefNo2.Text);
                SqlParameter TTAmt2 = new SqlParameter("@TTAmt2", txtTTAmount2.Text);
                SqlParameter TTREFNO3 = new SqlParameter("@TTREFNO3", txtTTRefNo3.Text);
                SqlParameter TTAmt3 = new SqlParameter("@TTAmt3", txtTTAmount3.Text);
                SqlParameter TTREFNO4 = new SqlParameter("@TTREFNO4", txtTTRefNo4.Text);
                SqlParameter TTAmt4 = new SqlParameter("@TTAmt4", txtTTAmount4.Text);
                SqlParameter TTREFNO5 = new SqlParameter("@TTREFNO5", txtTTRefNo5.Text);
                SqlParameter TTAmt5 = new SqlParameter("@TTAmt5", txtTTAmount5.Text);
                SqlParameter pTTRef6 = new SqlParameter("@TTRef6", txtTTRefNo6.Text.Trim());
                SqlParameter pTTAmt6 = new SqlParameter("@TTAmt6", txtTTAmount6.Text.Trim());
                SqlParameter pTTRef7 = new SqlParameter("@TTRef7", txtTTRefNo7.Text.Trim());
                SqlParameter pTTAmt7 = new SqlParameter("@TTAmt7", txtTTAmount7.Text.Trim());
                SqlParameter pTTRef8 = new SqlParameter("@TTRef8", txtTTRefNo8.Text.Trim());
                SqlParameter pTTAmt8 = new SqlParameter("@TTAmt8", txtTTAmount8.Text.Trim());
                SqlParameter pTTRef9 = new SqlParameter("@TTRef9", txtTTRefNo9.Text.Trim());
                SqlParameter pTTAmt9 = new SqlParameter("@TTAmt9", txtTTAmount9.Text.Trim());
                SqlParameter pTTRef10 = new SqlParameter("@TTRef10", txtTTRefNo10.Text.Trim());
                SqlParameter pTTAmt10 = new SqlParameter("@TTAmt10", txtTTAmount10.Text.Trim());
                SqlParameter pTTRef11 = new SqlParameter("@TTRef11", txtTTRefNo11.Text.Trim());
                SqlParameter pTTAmt11 = new SqlParameter("@TTAmt11", txtTTAmount11.Text.Trim());
                SqlParameter pTTRef12 = new SqlParameter("@TTRef12", txtTTRefNo12.Text.Trim());
                SqlParameter pTTAmt12 = new SqlParameter("@TTAmt12", txtTTAmount12.Text.Trim());
                SqlParameter pTTRef13 = new SqlParameter("@TTRef13", txtTTRefNo13.Text.Trim());
                SqlParameter pTTAmt13 = new SqlParameter("@TTAmt13", txtTTAmount13.Text.Trim());
                SqlParameter pTTRef14 = new SqlParameter("@TTRef14", txtTTRefNo14.Text.Trim());
                SqlParameter pTTAmt14 = new SqlParameter("@TTAmt14", txtTTAmount14.Text.Trim());
                SqlParameter pTTRef15 = new SqlParameter("@TTRef15", txtTTRefNo15.Text.Trim());
                SqlParameter pTTAmt15 = new SqlParameter("@TTAmt15", txtTTAmount15.Text.Trim());

                SqlParameter PTTCurr1 = new SqlParameter("@TTCurr1", ddlTTCurrency1.SelectedValue);
                SqlParameter PTTCurr2 = new SqlParameter("@TTCurr2", ddlTTCurrency2.SelectedValue);
                SqlParameter PTTCurr3 = new SqlParameter("@TTCurr3", ddlTTCurrency3.SelectedValue);
                SqlParameter PTTCurr4 = new SqlParameter("@TTCurr4", ddlTTCurrency4.SelectedValue);
                SqlParameter PTTCurr5 = new SqlParameter("@TTCurr5", ddlTTCurrency5.SelectedValue);
                SqlParameter PTTCurr6 = new SqlParameter("@TTCurr6", ddlTTCurrency6.SelectedValue);
                SqlParameter PTTCurr7 = new SqlParameter("@TTCurr7", ddlTTCurrency7.SelectedValue);
                SqlParameter PTTCurr8 = new SqlParameter("@TTCurr8", ddlTTCurrency8.SelectedValue);
                SqlParameter PTTCurr9 = new SqlParameter("@TTCurr9", ddlTTCurrency9.SelectedValue);
                SqlParameter PTTCurr10 = new SqlParameter("@TTCurr10", ddlTTCurrency10.SelectedValue);
                SqlParameter PTTCurr11 = new SqlParameter("@TTCurr11", ddlTTCurrency11.SelectedValue);
                SqlParameter PTTCurr12 = new SqlParameter("@TTCurr12", ddlTTCurrency12.SelectedValue);
                SqlParameter PTTCurr13 = new SqlParameter("@TTCurr13", ddlTTCurrency13.SelectedValue);
                SqlParameter PTTCurr14 = new SqlParameter("@TTCurr14", ddlTTCurrency14.SelectedValue);
                SqlParameter PTTCurr15 = new SqlParameter("@TTCurr15", ddlTTCurrency15.SelectedValue);

                SqlParameter PTotTTAmt1 = new SqlParameter("@TotTTAmt1", txtTotTTAmt1.Text.Trim());
                SqlParameter PTotTTAmt2 = new SqlParameter("@TotTTAmt2", txtTotTTAmt2.Text.Trim());
                SqlParameter PTotTTAmt3 = new SqlParameter("@TotTTAmt3", txtTotTTAmt3.Text.Trim());
                SqlParameter PTotTTAmt4 = new SqlParameter("@TotTTAmt4", txtTotTTAmt4.Text.Trim());
                SqlParameter PTotTTAmt5 = new SqlParameter("@TotTTAmt5", txtTotTAmt5.Text.Trim());
                SqlParameter PTotTTAmt6 = new SqlParameter("@TotTTAmt6", txtTotTTAmt6.Text.Trim());
                SqlParameter PTotTTAmt7 = new SqlParameter("@TotTTAmt7", txtTotTTAmt7.Text.Trim());
                SqlParameter PTotTTAmt8 = new SqlParameter("@TotTTAmt8", txtTotTTAmt8.Text.Trim());
                SqlParameter PTotTTAmt9 = new SqlParameter("@TotTTAmt9", txtTtTTAmt9.Text.Trim());
                SqlParameter PTotTTAmt10 = new SqlParameter("@TotTTAmt10", txtTotTTAmt10.Text.Trim());
                SqlParameter PTotTTAmt11 = new SqlParameter("@TotTTAmt11", txtTotTTAmt11.Text.Trim());
                SqlParameter PTotTTAmt12 = new SqlParameter("@TotTTAmt12", txtTotTTAmt12.Text.Trim());
                SqlParameter PTotTTAmt13 = new SqlParameter("@TotTTAmt13", txtTotTTAmt13.Text.Trim());
                SqlParameter PTotTTAmt14 = new SqlParameter("@TotTTAmt14", txtTotTTAmt14.Text.Trim());
                SqlParameter PTotTTAmt15 = new SqlParameter("@TotTTAmt15", txtTotTTAmt15.Text.Trim());

                SqlParameter PBalTTAmt1 = new SqlParameter("@BalTTAmt1", txtBalTTAmt1.Text.Trim());
                SqlParameter PBalTTAmt2 = new SqlParameter("@BalTTAmt2", txtBalTTAmt2.Text.Trim());
                SqlParameter PBalTTAmt3 = new SqlParameter("@BalTTAmt3", txtBalTTAmt3.Text.Trim());
                SqlParameter PBalTTAmt4 = new SqlParameter("@BalTTAmt4", txtBalTTAmt4.Text.Trim());
                SqlParameter PBalTTAmt5 = new SqlParameter("@BalTTAmt5", txtBalTTAmt5.Text.Trim());
                SqlParameter PBalTTAmt6 = new SqlParameter("@BalTTAmt6", txtBalTTAmt6.Text.Trim());
                SqlParameter PBalTTAmt7 = new SqlParameter("@BalTTAmt7", txtBalTTAmt7.Text.Trim());
                SqlParameter PBalTTAmt8 = new SqlParameter("@BalTTAmt8", txtBalTAmt8.Text.Trim());
                SqlParameter PBalTTAmt9 = new SqlParameter("@BalTTAmt9", txtBalTTAmt9.Text.Trim());
                SqlParameter PBalTTAmt10 = new SqlParameter("@BalTTAmt10", txtBalTTAmt10.Text.Trim());
                SqlParameter PBalTTAmt11 = new SqlParameter("@BalTTAmt11", txtBalTTAmt11.Text.Trim());
                SqlParameter PBalTTAmt12 = new SqlParameter("@BalTTAmt12", txtBalTTAmt12.Text.Trim());
                SqlParameter PBalTTAmt13 = new SqlParameter("@BalTTAmt13", txtBalTTAmt13.Text.Trim());
                SqlParameter PBalTTAmt14 = new SqlParameter("@BalTTAmt14", txtBalTTAmt14.Text.Trim());
                SqlParameter PBalTTAmt15 = new SqlParameter("@BalTTAmt15", txtBalTTAmt15.Text.Trim());

                SqlParameter PTTRealisedCurr1 = new SqlParameter("@TTRealisedCurr1", ddlTTRealisedCurr1.SelectedValue);
                SqlParameter PTTRealisedCurr2 = new SqlParameter("@TTRealisedCurr2", ddlTTRealisedCurr2.SelectedValue);
                SqlParameter PTTRealisedCurr3 = new SqlParameter("@TTRealisedCurr3", ddlTTRealisedCurr3.SelectedValue);
                SqlParameter PTTRealisedCurr4 = new SqlParameter("@TTRealisedCurr4", ddlTTRealisedCurr4.SelectedValue);
                SqlParameter PTTRealisedCurr5 = new SqlParameter("@TTRealisedCurr5", ddlTTRealisedCurr5.SelectedValue);
                SqlParameter PTTRealisedCurr6 = new SqlParameter("@TTRealisedCurr6", ddlTTRealisedCurr6.SelectedValue);
                SqlParameter PTTRealisedCurr7 = new SqlParameter("@TTRealisedCurr7", ddlTTRealisedCurr7.SelectedValue);
                SqlParameter PTTRealisedCurr8 = new SqlParameter("@TTRealisedCurr8", ddlTTRealisedCurr8.SelectedValue);
                SqlParameter PTTRealisedCurr9 = new SqlParameter("@TTRealisedCurr9", ddlTTRealisedCurr9.SelectedValue);
                SqlParameter PTTRealisedCurr10 = new SqlParameter("@TTRealisedCurr10", ddlTTRealisedCurr10.SelectedValue);
                SqlParameter PTTRealisedCurr11 = new SqlParameter("@TTRealisedCurr11", ddlTTRealisedCurr11.SelectedValue);
                SqlParameter PTTRealisedCurr12 = new SqlParameter("@TTRealisedCurr12", ddlTTRealisedCurr12.SelectedValue);
                SqlParameter PTTRealisedCurr13 = new SqlParameter("@TTRealisedCurr13", ddlTTRealisedCurr13.SelectedValue);
                SqlParameter PTTRealisedCurr14 = new SqlParameter("@TTRealisedCurr14", ddlTTRealisedCurr14.SelectedValue);
                SqlParameter PTTRealisedCurr15 = new SqlParameter("@TTRealisedCurr15", ddlTTRealisedCurr15.SelectedValue);


                SqlParameter PTTCrossCurrRate1 = new SqlParameter("@TTCrossCurrRate1", txtTTCrossCurrRate1.Text.Trim());
                SqlParameter PTTCrossCurrRate2 = new SqlParameter("@TTCrossCurrRate2", txtTTCrossCurrRate2.Text.Trim());
                SqlParameter PTTCrossCurrRate3 = new SqlParameter("@TTCrossCurrRate3", txtTTCrossCurrRate3.Text.Trim());
                SqlParameter PTTCrossCurrRate4 = new SqlParameter("@TTCrossCurrRate4", txtTTCrossCurrRate4.Text.Trim());
                SqlParameter PTTCrossCurrRate5 = new SqlParameter("@TTCrossCurrRate5", txtTTCrossCurrRate5.Text.Trim());
                SqlParameter PTTCrossCurrRate6 = new SqlParameter("@TTCrossCurrRate6", txtTTCrossCurrRate6.Text.Trim());
                SqlParameter PTTCrossCurrRate7 = new SqlParameter("@TTCrossCurrRate7", txtTTCrossCurrRate7.Text.Trim());
                SqlParameter PTTCrossCurrRate8 = new SqlParameter("@TTCrossCurrRate8", txtTTCrossCurrRate8.Text.Trim());
                SqlParameter PTTCrossCurrRate9 = new SqlParameter("@TTCrossCurrRate9", txtTTCrossCurrRate9.Text.Trim());
                SqlParameter PTTCrossCurrRate10 = new SqlParameter("@TTCrossCurrRate10", txtTTCrossCurrRate10.Text.Trim());
                SqlParameter PTTCrossCurrRate11 = new SqlParameter("@TTCrossCurrRate11", txtTTCrossCurrRate11.Text.Trim());
                SqlParameter PTTCrossCurrRate12 = new SqlParameter("@TTCrossCurrRate12", txtTTCrossCurrRate12.Text.Trim());
                SqlParameter PTTCrossCurrRate13 = new SqlParameter("@TTCrossCurrRate13", txtTTCrossCurrRate13.Text.Trim());
                SqlParameter PTTCrossCurrRate14 = new SqlParameter("@TTCrossCurrRate14", txtTTCrossCurrRate14.Text.Trim());
                SqlParameter PTTCrossCurrRate15 = new SqlParameter("@TTCrossCurrRate15", txtTTCrossCurrRate15.Text.Trim());

                SqlParameter PTTAmtRealised1 = new SqlParameter("@TTAmtRealised1", txtTTAmtRealised1.Text.Trim());
                SqlParameter PTTAmtRealised2 = new SqlParameter("@TTAmtRealised2", txtTTAmtRealised2.Text.Trim());
                SqlParameter PTTAmtRealised3 = new SqlParameter("@TTAmtRealised3", txtTTAmtRealised3.Text.Trim());
                SqlParameter PTTAmtRealised4 = new SqlParameter("@TTAmtRealised4", txtTTAmtRealised4.Text.Trim());
                SqlParameter PTTAmtRealised5 = new SqlParameter("@TTAmtRealised5", txtTTAmtRealised5.Text.Trim());
                SqlParameter PTTAmtRealised6 = new SqlParameter("@TTAmtRealised6", txtTTAmtRealised6.Text.Trim());
                SqlParameter PTTAmtRealised7 = new SqlParameter("@TTAmtRealised7", txtTTAmtRealised7.Text.Trim());
                SqlParameter PTTAmtRealised8 = new SqlParameter("@TTAmtRealised8", txtTTAmtRealised8.Text.Trim());
                SqlParameter PTTAmtRealised9 = new SqlParameter("@TTAmtRealised9", txtTTAmtRealised9.Text.Trim());
                SqlParameter PTTAmtRealised10 = new SqlParameter("@TTAmtRealised10", txtTTAmtRealised10.Text.Trim());
                SqlParameter PTTAmtRealised11 = new SqlParameter("@TTAmtRealised11", txtTTAmtRealised11.Text.Trim());
                SqlParameter PTTAmtRealised12 = new SqlParameter("@TTAmtRealised12", txtTTAmtRealised12.Text.Trim());
                SqlParameter PTTAmtRealised13 = new SqlParameter("@TTAmtRealised13", txtTTAmtRealised13.Text.Trim());
                SqlParameter PTTAmtRealised14 = new SqlParameter("@TTAmtRealised14", txtTTAmtRealised14.Text.Trim());
                SqlParameter PTTAmtRealised15 = new SqlParameter("@TTAmtRealised15", txtTTAmtRealised15.Text.Trim());

                SqlParameter FIRC_Status = new SqlParameter("@FIRC_Status", SqlDbType.VarChar);
                if (chkFirc.Checked == true)
                { FIRC_Status.Value = "Y"; }
                if (chkFirc.Checked == false)
                { FIRC_Status.Value = "N"; }
                SqlParameter FIRC_NO = new SqlParameter("@FIRC_NO", txtFircNo.Text);
                SqlParameter FIRC_AD_CODE = new SqlParameter("@FIRC_AD_CODE", txtFircAdCode.Text);
                SqlParameter checkerstatus = new SqlParameter("@checkerstatus", "C");

                string PendingSB = "N";
                if (chkSB.Checked)
                    PendingSB = "Y";
                SqlParameter pCheckPendingSB = new SqlParameter("@PendingSBFlag", PendingSB);


                //-----------------------------------------------------Anand26/06/2023---------------------------------
                /////IMPORT ACCOUNTING 1
                //---------------Anand 11-08-2023----------------------------
                string IMPACC1Flag = "";
                if (chk_IMPACC1Flag.Checked == true)
                    IMPACC1Flag = "Y";
                else
                    IMPACC1Flag = "N";
                //----------------END---------------------------------------
                SqlParameter P_chk_IMPACC1Flag = new SqlParameter("@IMP_ACC1_Flag", IMPACC1Flag);// Added by Anand 11-08-2023
                SqlParameter P_txt_IMPACC1_FCRefNo = new SqlParameter("@IMP_ACC1_FCRefNo", txt_IMPACC1_FCRefNo.Text.Trim());
                SqlParameter P_txt_IMPACC1_DiscAmt = new SqlParameter("@IMP_ACC1_Amount", txt_IMPACC1_DiscAmt.Text.Trim());
                SqlParameter P_txt_IMPACC1_DiscExchRate = new SqlParameter("@IMP_ACC1_ExchRate", "");//_txt_IMPACC1_DiscExchRate.ToUpper());
                SqlParameter P_txt_IMPACC1_Princ_matu = new SqlParameter("@IMP_ACC1_Principal_MATU", txt_IMPACC1_Princ_matu.Text.Trim());
                SqlParameter P_txt_IMPACC1_Princ_lump = new SqlParameter("@IMP_ACC1_Principal_LUMP", txt_IMPACC1_Princ_lump.Text.Trim());
                SqlParameter P_txt_IMPACC1_Princ_Contract_no = new SqlParameter("@IMP_ACC1_Principal_Contract_No", txt_IMPACC1_Princ_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC1_Princ_Ex_Curr = new SqlParameter("@IMP_ACC1_Principal_Ex_Curr", txt_IMPACC1_Princ_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_Princ_Ex_rate = new SqlParameter("@IMP_ACC1_Principal_Exch_Rate", txt_IMPACC1_Princ_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Principal_Intnl_Exch_Rate", txt_IMPACC1_Princ_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_Interest_matu = new SqlParameter("@IMP_ACC1_Interest_MATU", txt_IMPACC1_Interest_matu.Text.Trim());
                SqlParameter P_txt_IMPACC1_Interest_lump = new SqlParameter("@IMP_ACC1_Interest_LUMP", txt_IMPACC1_Interest_lump.Text.Trim());
                SqlParameter P_txt_IMPACC1_Interest_Contract_no = new SqlParameter("@IMP_ACC1_Interest_Contract_No", txt_IMPACC1_Interest_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC1_Interest_Ex_Curr = new SqlParameter("@IMP_ACC1_Interest_Ex_Curr", txt_IMPACC1_Interest_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_Interest_Ex_rate = new SqlParameter("@IMP_ACC1_Interest_Exch_Rate", txt_IMPACC1_Interest_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Interest_Intnl_Exch_Rate", txt_IMPACC1_Interest_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_Commission_matu = new SqlParameter("@IMP_ACC1_Commission_MATU", txt_IMPACC1_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC1_Commission_lump = new SqlParameter("@IMP_ACC1_Commission_LUMP", txt_IMPACC1_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC1_Commission_Contract_no = new SqlParameter("@IMP_ACC1_Commission_Contract_No", txt_IMPACC1_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC1_Commission_Ex_Curr = new SqlParameter("@IMP_ACC1_Commission_Ex_Curr", txt_IMPACC1_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_Commission_Ex_rate = new SqlParameter("@IMP_ACC1_Commission_Exch_Rate", txt_IMPACC1_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Commission_Intnl_Exch_Rate", txt_IMPACC1_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_Their_Commission_matu = new SqlParameter("@IMP_ACC1_Their_Commission_MATU", txt_IMPACC1_Their_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC1_Their_Commission_lump = new SqlParameter("@IMP_ACC1_Their_Commission_LUMP", txt_IMPACC1_Their_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC1_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC1_Their_Commission_Contract_No", txt_IMPACC1_Their_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC1_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC1_Their_Commission_Ex_Curr", txt_IMPACC1_Their_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC1_Their_Commission_Exch_Rate", txt_IMPACC1_Their_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Their_Commission_Intnl_Exch_Rate", txt_IMPACC1_Their_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Code = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Code", txt_IMPACC1_CR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_AC_Short_Name = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Short_Name", txt_IMPACC1_CR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Cust_abbr = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC1_CR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Cust_Acc = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC1_CR_Cust_Acc.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Curr", txt_IMPACC1_CR_Acceptance_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Acceptance_amt = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Amt", txt_IMPACC1_CR_Acceptance_amt.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Acceptance_payer = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Payer", txt_IMPACC1_CR_Acceptance_payer.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Interest_Curr = new SqlParameter("@IMP_ACC1_CR_Interest_Curr", txt_IMPACC1_CR_Interest_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Interest_amt = new SqlParameter("@IMP_ACC1_CR_Interest_Amount", txt_IMPACC1_CR_Interest_amt.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Interest_payer = new SqlParameter("@IMP_ACC1_CR_Interest_Payer", txt_IMPACC1_CR_Interest_payer.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Curr", txt_IMPACC1_CR_Accept_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Amount", txt_IMPACC1_CR_Accept_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Payer", txt_IMPACC1_CR_Accept_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Curr", txt_IMPACC1_CR_Pay_Handle_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Amount", txt_IMPACC1_CR_Pay_Handle_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Payer", txt_IMPACC1_CR_Pay_Handle_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Others_Curr = new SqlParameter("@IMP_ACC1_CR_Others_Curr", txt_IMPACC1_CR_Others_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Others_amt = new SqlParameter("@IMP_ACC1_CR_Others_Amount", txt_IMPACC1_CR_Others_amt.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Others_Payer = new SqlParameter("@IMP_ACC1_CR_Others_Payer", txt_IMPACC1_CR_Others_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Curr", txt_IMPACC1_CR_Their_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Amount", txt_IMPACC1_CR_Their_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC1_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Payer", txt_IMPACC1_CR_Their_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Code = new SqlParameter("@IMP_ACC1_DR_Code", txt_IMPACC1_DR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_AC_Short_Name = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name", txt_IMPACC1_DR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_abbr = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr", txt_IMPACC1_DR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_Acc = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No", txt_IMPACC1_DR_Cust_Acc.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr", txt_IMPACC1_DR_Cur_Acc_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount", txt_IMPACC1_DR_Cur_Acc_amt.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer", txt_IMPACC1_DR_Cur_Acc_payer.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr2", txt_IMPACC1_DR_Cur_Acc_Curr2.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount2", txt_IMPACC1_DR_Cur_Acc_amt2.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer2", txt_IMPACC1_DR_Cur_Acc_payer2.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr3", txt_IMPACC1_DR_Cur_Acc_Curr3.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount3", txt_IMPACC1_DR_Cur_Acc_amt3.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer3", txt_IMPACC1_DR_Cur_Acc_payer3.Text.Trim());

                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr4", txt_IMPACC1_DR_Cur_Acc_Curr4.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount4", txt_IMPACC1_DR_Cur_Acc_amt4.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer4", txt_IMPACC1_DR_Cur_Acc_payer4.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr5", txt_IMPACC1_DR_Cur_Acc_Curr5.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount5", txt_IMPACC1_DR_Cur_Acc_amt5.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer5", txt_IMPACC1_DR_Cur_Acc_payer5.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr6", txt_IMPACC1_DR_Cur_Acc_Curr6.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount6", txt_IMPACC1_DR_Cur_Acc_amt6.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer6", txt_IMPACC1_DR_Cur_Acc_payer6.Text.Trim());

                SqlParameter P_txt_IMPACC1_DR_Code2 = new SqlParameter("@IMP_ACC1_DR_Code2", txt_IMPACC1_DR_Code2.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name2", txt_IMPACC1_DR_AC_Short_Name2.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr2", txt_IMPACC1_DR_Cust_abbr2.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No2", txt_IMPACC1_DR_Cust_Acc2.Text.Trim());

                SqlParameter P_txt_IMPACC1_DR_Code3 = new SqlParameter("@IMP_ACC1_DR_Code3", txt_IMPACC1_DR_Code3.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name3", txt_IMPACC1_DR_AC_Short_Name3.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr3", txt_IMPACC1_DR_Cust_abbr3.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No3", txt_IMPACC1_DR_Cust_Acc3.Text.Trim());

                SqlParameter P_txt_IMPACC1_DR_Code4 = new SqlParameter("@IMP_ACC1_DR_Code4", txt_IMPACC1_DR_Code4.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name4", txt_IMPACC1_DR_AC_Short_Name4.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr4", txt_IMPACC1_DR_Cust_abbr4.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No4", txt_IMPACC1_DR_Cust_Acc4.Text.Trim());

                SqlParameter P_txt_IMPACC1_DR_Code5 = new SqlParameter("@IMP_ACC1_DR_Code5", txt_IMPACC1_DR_Code5.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name5", txt_IMPACC1_DR_AC_Short_Name5.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr5", txt_IMPACC1_DR_Cust_abbr5.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No5", txt_IMPACC1_DR_Cust_Acc5.Text.Trim());

                SqlParameter P_txt_IMPACC1_DR_Code6 = new SqlParameter("@IMP_ACC1_DR_Code6", txt_IMPACC1_DR_Code6.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name6", txt_IMPACC1_DR_AC_Short_Name6.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr6", txt_IMPACC1_DR_Cust_abbr6.Text.Trim());
                SqlParameter P_txt_IMPACC1_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No6", txt_IMPACC1_DR_Cust_Acc6.Text.Trim());


                ////IMPORT ACCOUNTING 2
                //---------------Anand 11-08-2023----------------------------
                string IMPACC2Flag = "";
                if (chk_IMPACC2Flag.Checked == true)
                    IMPACC2Flag = "Y";
                else
                    IMPACC2Flag = "N";
                //----------------END---------------------------------------
                SqlParameter P_chk_IMPACC2Flag = new SqlParameter("@IMP_ACC2_Flag", IMPACC2Flag);//_chk_IMPACC2Flag.ToUpper());
                SqlParameter P_txt_IMPACC2_FCRefNo = new SqlParameter("@IMP_ACC2_FCRefNo", txt_IMPACC2_FCRefNo.Text.Trim());
                SqlParameter P_txt_IMPACC2_DiscAmt = new SqlParameter("@IMP_ACC2_Amount", txt_IMPACC2_DiscAmt.Text.Trim());
                SqlParameter P_txt_IMPACC2_DiscExchRate = new SqlParameter("@IMP_ACC2_ExchRate", "");//_txt_IMPACC2_DiscExchRate.Text.Trim());
                SqlParameter P_txt_IMPACC2_Princ_matu = new SqlParameter("@IMP_ACC2_Principal_MATU", txt_IMPACC2_Princ_matu.Text.Trim());
                SqlParameter P_txt_IMPACC2_Princ_lump = new SqlParameter("@IMP_ACC2_Principal_LUMP", txt_IMPACC2_Princ_lump.Text.Trim());
                SqlParameter P_txt_IMPACC2_Princ_Contract_no = new SqlParameter("@IMP_ACC2_Principal_Contract_No", txt_IMPACC2_Princ_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC2_Princ_Ex_Curr = new SqlParameter("@IMP_ACC2_Principal_Ex_Curr", txt_IMPACC2_Princ_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_Princ_Ex_rate = new SqlParameter("@IMP_ACC2_Principal_Exch_Rate", txt_IMPACC2_Princ_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC2_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Principal_Intnl_Exch_Rate", txt_IMPACC2_Princ_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC2_Interest_matu = new SqlParameter("@IMP_ACC2_Interest_MATU", txt_IMPACC2_Interest_matu.Text.Trim());
                SqlParameter P_txt_IMPACC2_Interest_lump = new SqlParameter("@IMP_ACC2_Interest_LUMP", txt_IMPACC2_Interest_lump.Text.Trim());
                SqlParameter P_txt_IMPACC2_Interest_Contract_no = new SqlParameter("@IMP_ACC2_Interest_Contract_No", txt_IMPACC2_Interest_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC2_Interest_Ex_Curr = new SqlParameter("@IMP_ACC2_Interest_Ex_Curr", txt_IMPACC2_Interest_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_Interest_Ex_rate = new SqlParameter("@IMP_ACC2_Interest_Exch_Rate", txt_IMPACC2_Interest_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC2_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Interest_Intnl_Exch_Rate", txt_IMPACC2_Interest_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC2_Commission_matu = new SqlParameter("@IMP_ACC2_Commission_MATU", txt_IMPACC2_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC2_Commission_lump = new SqlParameter("@IMP_ACC2_Commission_LUMP", txt_IMPACC2_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC2_Commission_Contract_no = new SqlParameter("@IMP_ACC2_Commission_Contract_No", txt_IMPACC2_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC2_Commission_Ex_Curr = new SqlParameter("@IMP_ACC2_Commission_Ex_Curr", txt_IMPACC2_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_Commission_Ex_rate = new SqlParameter("@IMP_ACC2_Commission_Exch_Rate", txt_IMPACC2_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC2_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Commission_Intnl_Exch_Rate", txt_IMPACC2_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC2_Their_Commission_matu = new SqlParameter("@IMP_ACC2_Their_Commission_MATU", txt_IMPACC2_Their_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC2_Their_Commission_lump = new SqlParameter("@IMP_ACC2_Their_Commission_LUMP", txt_IMPACC2_Their_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC2_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC2_Their_Commission_Contract_No", txt_IMPACC2_Their_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC2_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC2_Their_Commission_Ex_Curr", txt_IMPACC2_Their_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC2_Their_Commission_Exch_Rate", txt_IMPACC2_Their_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC2_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Their_Commission_Intnl_Exch_Rate", txt_IMPACC2_Their_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Code = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Code", txt_IMPACC2_CR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_AC_Short_Name = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Short_Name", txt_IMPACC2_CR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Cust_abbr = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC2_CR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Cust_Acc = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC2_CR_Cust_Acc.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Curr", txt_IMPACC2_CR_Acceptance_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Acceptance_amt = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Amt", txt_IMPACC2_CR_Acceptance_amt.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Acceptance_payer = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Payer", txt_IMPACC2_CR_Acceptance_payer.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Interest_Curr = new SqlParameter("@IMP_ACC2_CR_Interest_Curr", txt_IMPACC2_CR_Interest_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Interest_amt = new SqlParameter("@IMP_ACC2_CR_Interest_Amount", txt_IMPACC2_CR_Interest_amt.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Interest_payer = new SqlParameter("@IMP_ACC2_CR_Interest_Payer", txt_IMPACC2_CR_Interest_payer.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Curr", txt_IMPACC2_CR_Accept_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Amount", txt_IMPACC2_CR_Accept_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Payer", txt_IMPACC2_CR_Accept_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Curr", txt_IMPACC2_CR_Pay_Handle_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Amount", txt_IMPACC2_CR_Pay_Handle_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Payer", txt_IMPACC2_CR_Pay_Handle_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Others_Curr = new SqlParameter("@IMP_ACC2_CR_Others_Curr", txt_IMPACC2_CR_Others_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Others_amt = new SqlParameter("@IMP_ACC2_CR_Others_Amount", txt_IMPACC2_CR_Others_amt.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Others_Payer = new SqlParameter("@IMP_ACC2_CR_Others_Payer", txt_IMPACC2_CR_Others_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Curr", txt_IMPACC2_CR_Their_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Amount", txt_IMPACC2_CR_Their_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC2_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Payer", txt_IMPACC2_CR_Their_Commission_Payer.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Code = new SqlParameter("@IMP_ACC2_DR_Code", txt_IMPACC2_DR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_AC_Short_Name = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name", txt_IMPACC2_DR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_abbr = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr", txt_IMPACC2_DR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_Acc = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No", txt_IMPACC2_DR_Cust_Acc.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr", txt_IMPACC2_DR_Cur_Acc_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount", txt_IMPACC2_DR_Cur_Acc_amt.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer", txt_IMPACC2_DR_Cur_Acc_payer.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr2", txt_IMPACC2_DR_Cur_Acc_Curr2.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount2", txt_IMPACC2_DR_Cur_Acc_amt2.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer2", txt_IMPACC2_DR_Cur_Acc_payer2.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr3", txt_IMPACC2_DR_Cur_Acc_Curr3.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount3", txt_IMPACC2_DR_Cur_Acc_amt3.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer3", txt_IMPACC2_DR_Cur_Acc_payer3.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr4", txt_IMPACC2_DR_Cur_Acc_Curr4.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount4", txt_IMPACC2_DR_Cur_Acc_amt4.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer4", txt_IMPACC2_DR_Cur_Acc_payer4.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr5", txt_IMPACC2_DR_Cur_Acc_Curr5.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount5", txt_IMPACC2_DR_Cur_Acc_amt5.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer5", txt_IMPACC2_DR_Cur_Acc_payer5.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr6", txt_IMPACC2_DR_Cur_Acc_Curr6.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount6", txt_IMPACC2_DR_Cur_Acc_amt6.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer6", txt_IMPACC2_DR_Cur_Acc_payer6.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Code2 = new SqlParameter("@IMP_ACC2_DR_Code2", txt_IMPACC2_DR_Code2.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name2", txt_IMPACC2_DR_AC_Short_Name2.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr2", txt_IMPACC2_DR_Cust_abbr2.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No2", txt_IMPACC2_DR_Cust_Acc2.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Code3 = new SqlParameter("@IMP_ACC2_DR_Code3", txt_IMPACC2_DR_Code3.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name3", txt_IMPACC2_DR_AC_Short_Name3.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr3", txt_IMPACC2_DR_Cust_abbr3.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No3", txt_IMPACC2_DR_Cust_Acc3.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Code4 = new SqlParameter("@IMP_ACC2_DR_Code4", txt_IMPACC2_DR_Code4.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name4", txt_IMPACC2_DR_AC_Short_Name4.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr4", txt_IMPACC2_DR_Cust_abbr4.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No4", txt_IMPACC2_DR_Cust_Acc4.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Code5 = new SqlParameter("@IMP_ACC2_DR_Code5", txt_IMPACC2_DR_Code5.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name5", txt_IMPACC2_DR_AC_Short_Name5.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr5", txt_IMPACC2_DR_Cust_abbr5.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No5", txt_IMPACC2_DR_Cust_Acc5.Text.Trim());

                SqlParameter P_txt_IMPACC2_DR_Code6 = new SqlParameter("@IMP_ACC2_DR_Code6", txt_IMPACC2_DR_Code6.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name6", txt_IMPACC2_DR_AC_Short_Name6.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr6", txt_IMPACC2_DR_Cust_abbr6.Text.Trim());
                SqlParameter P_txt_IMPACC2_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No6", txt_IMPACC2_DR_Cust_Acc6.Text.Trim());

                //////IMPORT ACCOUNTING  3
                //---------------Anand 11-08-2023----------------------------
                string IMPACC3Flag = "";
                if (chk_IMPACC3Flag.Checked == true)
                    IMPACC3Flag = "Y";
                else
                    IMPACC3Flag = "N";
                //----------------END---------------------------------------
                SqlParameter P_chk_IMPACC3Flag = new SqlParameter("@IMP_ACC3_Flag", IMPACC3Flag);//_chk_IMPACC3Flag.Text.Trim());
                SqlParameter P_txt_IMPACC3_FCRefNo = new SqlParameter("@IMP_ACC3_FCRefNo", txt_IMPACC3_FCRefNo.Text.Trim());
                SqlParameter P_txt_IMPACC3_DiscAmt = new SqlParameter("@IMP_ACC3_Amount", txt_IMPACC3_DiscAmt.Text.Trim());
                SqlParameter P_txt_IMPACC3_DiscExchRate = new SqlParameter("@IMP_ACC3_ExchRate", "");//txt_IMPACC3_DiscExchRate.Text.Trim());
                SqlParameter P_txt_IMPACC3_Princ_matu = new SqlParameter("@IMP_ACC3_Principal_MATU", txt_IMPACC3_Princ_matu.Text.Trim());
                SqlParameter P_txt_IMPACC3_Princ_lump = new SqlParameter("@IMP_ACC3_Principal_LUMP", txt_IMPACC3_Princ_lump.Text.Trim());
                SqlParameter P_txt_IMPACC3_Princ_Contract_no = new SqlParameter("@IMP_ACC3_Principal_Contract_No", txt_IMPACC3_Princ_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC3_Princ_Ex_Curr = new SqlParameter("@IMP_ACC3_Principal_Ex_Curr", txt_IMPACC3_Princ_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_Princ_Ex_rate = new SqlParameter("@IMP_ACC3_Principal_Exch_Rate", txt_IMPACC3_Princ_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC3_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Principal_Intnl_Exch_Rate", txt_IMPACC3_Princ_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC3_Interest_matu = new SqlParameter("@IMP_ACC3_Interest_MATU", txt_IMPACC3_Interest_matu.Text.Trim());
                SqlParameter P_txt_IMPACC3_Interest_lump = new SqlParameter("@IMP_ACC3_Interest_LUMP", txt_IMPACC3_Interest_lump.Text.Trim());
                SqlParameter P_txt_IMPACC3_Interest_Contract_no = new SqlParameter("@IMP_ACC3_Interest_Contract_No", txt_IMPACC3_Interest_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC3_Interest_Ex_Curr = new SqlParameter("@IMP_ACC3_Interest_Ex_Curr", txt_IMPACC3_Interest_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_Interest_Ex_rate = new SqlParameter("@IMP_ACC3_Interest_Exch_Rate", txt_IMPACC3_Interest_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC3_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Interest_Intnl_Exch_Rate", txt_IMPACC3_Interest_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC3_Commission_matu = new SqlParameter("@IMP_ACC3_Commission_MATU", txt_IMPACC3_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC3_Commission_lump = new SqlParameter("@IMP_ACC3_Commission_LUMP", txt_IMPACC3_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC3_Commission_Contract_no = new SqlParameter("@IMP_ACC3_Commission_Contract_No", txt_IMPACC3_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC3_Commission_Ex_Curr = new SqlParameter("@IMP_ACC3_Commission_Ex_Curr", txt_IMPACC3_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_Commission_Ex_rate = new SqlParameter("@IMP_ACC3_Commission_Exch_Rate", txt_IMPACC3_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC3_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Commission_Intnl_Exch_Rate", txt_IMPACC3_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC3_Their_Commission_matu = new SqlParameter("@IMP_ACC3_Their_Commission_MATU", txt_IMPACC3_Their_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC3_Their_Commission_lump = new SqlParameter("@IMP_ACC3_Their_Commission_LUMP", txt_IMPACC3_Their_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC3_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC3_Their_Commission_Contract_No", txt_IMPACC3_Their_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC3_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC3_Their_Commission_Ex_Curr", txt_IMPACC3_Their_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC3_Their_Commission_Exch_Rate", txt_IMPACC3_Their_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC3_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Their_Commission_Intnl_Exch_Rate", txt_IMPACC3_Their_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Code = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Code", txt_IMPACC3_CR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_AC_Short_Name = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Short_Name", txt_IMPACC3_CR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Cust_abbr = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC3_CR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Cust_Acc = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC3_CR_Cust_Acc.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Curr", txt_IMPACC3_CR_Acceptance_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Acceptance_amt = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Amt", txt_IMPACC3_CR_Acceptance_amt.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Acceptance_payer = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Payer", txt_IMPACC3_CR_Acceptance_payer.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Interest_Curr = new SqlParameter("@IMP_ACC3_CR_Interest_Curr", txt_IMPACC3_CR_Interest_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Interest_amt = new SqlParameter("@IMP_ACC3_CR_Interest_Amount", txt_IMPACC3_CR_Interest_amt.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Interest_payer = new SqlParameter("@IMP_ACC3_CR_Interest_Payer", txt_IMPACC3_CR_Interest_payer.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Curr", txt_IMPACC3_CR_Accept_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Amount", txt_IMPACC3_CR_Accept_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Payer", txt_IMPACC3_CR_Accept_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Curr", txt_IMPACC3_CR_Pay_Handle_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Amount", txt_IMPACC3_CR_Pay_Handle_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Payer", txt_IMPACC3_CR_Pay_Handle_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Others_Curr = new SqlParameter("@IMP_ACC3_CR_Others_Curr", txt_IMPACC3_CR_Others_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Others_amt = new SqlParameter("@IMP_ACC3_CR_Others_Amount", txt_IMPACC3_CR_Others_amt.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Others_Payer = new SqlParameter("@IMP_ACC3_CR_Others_Payer", txt_IMPACC3_CR_Others_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Curr", txt_IMPACC3_CR_Their_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Amount", txt_IMPACC3_CR_Their_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC3_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Payer", txt_IMPACC3_CR_Their_Commission_Payer.Text.Trim());

                SqlParameter P_txt_IMPACC3_DR_Code = new SqlParameter("@IMP_ACC3_DR_Code", txt_IMPACC3_DR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_AC_Short_Name = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name", txt_IMPACC3_DR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_abbr = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr", txt_IMPACC3_DR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_Acc = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No", txt_IMPACC3_DR_Cust_Acc.Text.Trim());

                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr", txt_IMPACC3_DR_Cur_Acc_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount", txt_IMPACC3_DR_Cur_Acc_amt.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer", txt_IMPACC3_DR_Cur_Acc_payer.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr2", txt_IMPACC3_DR_Cur_Acc_Curr2.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount2", txt_IMPACC3_DR_Cur_Acc_amt2.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer2", txt_IMPACC3_DR_Cur_Acc_payer2.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr3", txt_IMPACC3_DR_Cur_Acc_Curr3.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount3", txt_IMPACC3_DR_Cur_Acc_amt3.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer3", txt_IMPACC3_DR_Cur_Acc_payer3.Text.Trim());

                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr4", txt_IMPACC3_DR_Cur_Acc_Curr4.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount4", txt_IMPACC3_DR_Cur_Acc_amt4.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer4", txt_IMPACC3_DR_Cur_Acc_payer4.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr5", txt_IMPACC3_DR_Cur_Acc_Curr5.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount5", txt_IMPACC3_DR_Cur_Acc_amt5.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer5", txt_IMPACC3_DR_Cur_Acc_payer5.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr6", txt_IMPACC3_DR_Cur_Acc_Curr6.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount6", txt_IMPACC3_DR_Cur_Acc_amt6.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer6", txt_IMPACC3_DR_Cur_Acc_payer6.Text.Trim());


                SqlParameter P_txt_IMPACC3_DR_Code2 = new SqlParameter("@IMP_ACC3_DR_Code2", txt_IMPACC3_DR_Code2.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name2", txt_IMPACC3_DR_AC_Short_Name2.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr2", txt_IMPACC3_DR_Cust_abbr2.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No2", txt_IMPACC3_DR_Cust_Acc2.Text.Trim());

                SqlParameter P_txt_IMPACC3_DR_Code3 = new SqlParameter("@IMP_ACC3_DR_Code3", txt_IMPACC3_DR_Code3.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name3", txt_IMPACC3_DR_AC_Short_Name3.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr3", txt_IMPACC3_DR_Cust_abbr3.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No3", txt_IMPACC3_DR_Cust_Acc3.Text.Trim());

                SqlParameter P_txt_IMPACC3_DR_Code4 = new SqlParameter("@IMP_ACC3_DR_Code4", txt_IMPACC3_DR_Code4.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name4", txt_IMPACC3_DR_AC_Short_Name4.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr4", txt_IMPACC3_DR_Cust_abbr4.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No4", txt_IMPACC3_DR_Cust_Acc4.Text.Trim());

                SqlParameter P_txt_IMPACC3_DR_Code5 = new SqlParameter("@IMP_ACC3_DR_Code5", txt_IMPACC3_DR_Code5.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name5", txt_IMPACC3_DR_AC_Short_Name5.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr5", txt_IMPACC3_DR_Cust_abbr5.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No5", txt_IMPACC3_DR_Cust_Acc5.Text.Trim());

                SqlParameter P_txt_IMPACC3_DR_Code6 = new SqlParameter("@IMP_ACC3_DR_Code6", txt_IMPACC3_DR_Code6.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name6", txt_IMPACC3_DR_AC_Short_Name6.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr6", txt_IMPACC3_DR_Cust_abbr6.Text.Trim());
                SqlParameter P_txt_IMPACC3_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No6", txt_IMPACC3_DR_Cust_Acc6.Text.Trim());

                //////IMPORT ACCOUNTING 4
                //---------------Anand 11-08-2023----------------------------
                string IMPACC4Flag = "";
                if (chk_IMPACC4Flag.Checked == true)
                    IMPACC4Flag = "Y";
                else
                    IMPACC4Flag = "N";
                //----------------END---------------------------------------
                SqlParameter P_chk_IMPACC4Flag = new SqlParameter("@IMP_ACC4_Flag", IMPACC4Flag);//_chk_IMPACC4Flag.Text.Trim());
                SqlParameter P_txt_IMPACC4_FCRefNo = new SqlParameter("@IMP_ACC4_FCRefNo", txt_IMPACC4_FCRefNo.Text.Trim());
                SqlParameter P_txt_IMPACC4_DiscAmt = new SqlParameter("@IMP_ACC4_Amount", txt_IMPACC4_DiscAmt.Text.Trim());
                SqlParameter P_txt_IMPACC4_DiscExchRate = new SqlParameter("@IMP_ACC4_ExchRate", "");//_txt_IMPACC4_DiscExchRate.Text.Trim());
                SqlParameter P_txt_IMPACC4_Princ_matu = new SqlParameter("@IMP_ACC4_Principal_MATU", txt_IMPACC4_Princ_matu.Text.Trim());
                SqlParameter P_txt_IMPACC4_Princ_lump = new SqlParameter("@IMP_ACC4_Principal_LUMP", txt_IMPACC4_Princ_lump.Text.Trim());
                SqlParameter P_txt_IMPACC4_Princ_Contract_no = new SqlParameter("@IMP_ACC4_Principal_Contract_No", txt_IMPACC4_Princ_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC4_Princ_Ex_Curr = new SqlParameter("@IMP_ACC4_Principal_Ex_Curr", txt_IMPACC4_Princ_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_Princ_Ex_rate = new SqlParameter("@IMP_ACC4_Principal_Exch_Rate", txt_IMPACC4_Princ_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC4_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Principal_Intnl_Exch_Rate", txt_IMPACC4_Princ_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC4_Interest_matu = new SqlParameter("@IMP_ACC4_Interest_MATU", txt_IMPACC4_Interest_matu.Text.Trim());
                SqlParameter P_txt_IMPACC4_Interest_lump = new SqlParameter("@IMP_ACC4_Interest_LUMP", txt_IMPACC4_Interest_lump.Text.Trim());
                SqlParameter P_txt_IMPACC4_Interest_Contract_no = new SqlParameter("@IMP_ACC4_Interest_Contract_No", txt_IMPACC4_Interest_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC4_Interest_Ex_Curr = new SqlParameter("@IMP_ACC4_Interest_Ex_Curr", txt_IMPACC4_Interest_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_Interest_Ex_rate = new SqlParameter("@IMP_ACC4_Interest_Exch_Rate", txt_IMPACC4_Interest_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC4_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Interest_Intnl_Exch_Rate", txt_IMPACC4_Interest_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC4_Commission_matu = new SqlParameter("@IMP_ACC4_Commission_MATU", txt_IMPACC4_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC4_Commission_lump = new SqlParameter("@IMP_ACC4_Commission_LUMP", txt_IMPACC4_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC4_Commission_Contract_no = new SqlParameter("@IMP_ACC4_Commission_Contract_No", txt_IMPACC4_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC4_Commission_Ex_Curr = new SqlParameter("@IMP_ACC4_Commission_Ex_Curr", txt_IMPACC4_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_Commission_Ex_rate = new SqlParameter("@IMP_ACC4_Commission_Exch_Rate", txt_IMPACC4_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC4_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Commission_Intnl_Exch_Rate", txt_IMPACC4_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC4_Their_Commission_matu = new SqlParameter("@IMP_ACC4_Their_Commission_MATU", txt_IMPACC4_Their_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC4_Their_Commission_lump = new SqlParameter("@IMP_ACC4_Their_Commission_LUMP", txt_IMPACC4_Their_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC4_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC4_Their_Commission_Contract_No", txt_IMPACC4_Their_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC4_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC4_Their_Commission_Ex_Curr", txt_IMPACC4_Their_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC4_Their_Commission_Exch_Rate", txt_IMPACC4_Their_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC4_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Their_Commission_Intnl_Exch_Rate", txt_IMPACC4_Their_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Code = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Code", txt_IMPACC4_CR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_AC_Short_Name = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Short_Name", txt_IMPACC4_CR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Cust_abbr = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC4_CR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Cust_Acc = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC4_CR_Cust_Acc.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Curr", txt_IMPACC4_CR_Acceptance_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Acceptance_amt = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Amt", txt_IMPACC4_CR_Acceptance_amt.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Acceptance_payer = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Payer", txt_IMPACC4_CR_Acceptance_payer.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Interest_Curr = new SqlParameter("@IMP_ACC4_CR_Interest_Curr", txt_IMPACC4_CR_Interest_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Interest_amt = new SqlParameter("@IMP_ACC4_CR_Interest_Amount", txt_IMPACC4_CR_Interest_amt.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Interest_payer = new SqlParameter("@IMP_ACC4_CR_Interest_Payer", txt_IMPACC4_CR_Interest_payer.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Curr", txt_IMPACC4_CR_Accept_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Amount", txt_IMPACC4_CR_Accept_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Payer", txt_IMPACC4_CR_Accept_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Curr", txt_IMPACC4_CR_Pay_Handle_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Amount", txt_IMPACC4_CR_Pay_Handle_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Payer", txt_IMPACC4_CR_Pay_Handle_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Others_Curr = new SqlParameter("@IMP_ACC4_CR_Others_Curr", txt_IMPACC4_CR_Others_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Others_amt = new SqlParameter("@IMP_ACC4_CR_Others_Amount", txt_IMPACC4_CR_Others_amt.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Others_Payer = new SqlParameter("@IMP_ACC4_CR_Others_Payer", txt_IMPACC4_CR_Others_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Curr", txt_IMPACC4_CR_Their_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Amount", txt_IMPACC4_CR_Their_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC4_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Payer", txt_IMPACC4_CR_Their_Commission_Payer.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Code = new SqlParameter("@IMP_ACC4_DR_Code", txt_IMPACC4_DR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_AC_Short_Name = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name", txt_IMPACC4_DR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_abbr = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr", txt_IMPACC4_DR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_Acc = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No", txt_IMPACC4_DR_Cust_Acc.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr", txt_IMPACC4_DR_Cur_Acc_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount", txt_IMPACC4_DR_Cur_Acc_amt.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer", txt_IMPACC4_DR_Cur_Acc_payer.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr2", txt_IMPACC4_DR_Cur_Acc_Curr2.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount2", txt_IMPACC4_DR_Cur_Acc_amt2.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer2", txt_IMPACC4_DR_Cur_Acc_payer2.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr3", txt_IMPACC4_DR_Cur_Acc_Curr3.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount3", txt_IMPACC4_DR_Cur_Acc_amt3.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer3", txt_IMPACC4_DR_Cur_Acc_payer3.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr4", txt_IMPACC4_DR_Cur_Acc_Curr4.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount4", txt_IMPACC4_DR_Cur_Acc_amt4.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer4", txt_IMPACC4_DR_Cur_Acc_payer4.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr5", txt_IMPACC4_DR_Cur_Acc_Curr5.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount5", txt_IMPACC4_DR_Cur_Acc_amt5.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer5", txt_IMPACC4_DR_Cur_Acc_payer5.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr6", txt_IMPACC4_DR_Cur_Acc_Curr6.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount6", txt_IMPACC4_DR_Cur_Acc_amt6.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer6", txt_IMPACC4_DR_Cur_Acc_payer6.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Code2 = new SqlParameter("@IMP_ACC4_DR_Code2", txt_IMPACC4_DR_Code2.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name2", txt_IMPACC4_DR_AC_Short_Name2.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr2", txt_IMPACC4_DR_Cust_abbr2.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No2", txt_IMPACC4_DR_Cust_Acc2.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Code3 = new SqlParameter("@IMP_ACC4_DR_Code3", txt_IMPACC4_DR_Code3.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name3", txt_IMPACC4_DR_AC_Short_Name3.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr3", txt_IMPACC4_DR_Cust_abbr3.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No3", txt_IMPACC4_DR_Cust_Acc3.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Code4 = new SqlParameter("@IMP_ACC4_DR_Code4", txt_IMPACC4_DR_Code4.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name4", txt_IMPACC4_DR_AC_Short_Name4.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr4", txt_IMPACC4_DR_Cust_abbr4.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No4", txt_IMPACC4_DR_Cust_Acc4.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Code5 = new SqlParameter("@IMP_ACC4_DR_Code5", txt_IMPACC4_DR_Code5.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name5", txt_IMPACC4_DR_AC_Short_Name5.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr5", txt_IMPACC4_DR_Cust_abbr5.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No5", txt_IMPACC4_DR_Cust_Acc5.Text.Trim());

                SqlParameter P_txt_IMPACC4_DR_Code6 = new SqlParameter("@IMP_ACC4_DR_Code6", txt_IMPACC4_DR_Code6.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name6", txt_IMPACC4_DR_AC_Short_Name6.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr6", txt_IMPACC4_DR_Cust_abbr6.Text.Trim());
                SqlParameter P_txt_IMPACC4_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No6", txt_IMPACC4_DR_Cust_Acc6.Text.Trim());

                ///////IMPORT ACCOUNTING 5

                //---------------Anand 11-08-2023----------------------------
                string IMPACC5Flag = "";
                if (chk_IMPACC5Flag.Checked == true)
                    IMPACC5Flag = "Y";
                else
                    IMPACC5Flag = "N";
                //----------------END---------------------------------------
                SqlParameter P_chk_IMPACC5Flag = new SqlParameter("@IMP_ACC5_Flag", IMPACC5Flag);//_chk_IMPACC5Flag.ToUpper());
                SqlParameter P_txt_IMPACC5_FCRefNo = new SqlParameter("@IMP_ACC5_FCRefNo", txt_IMPACC5_FCRefNo.Text.Trim());
                SqlParameter P_txt_IMPACC5_DiscAmt = new SqlParameter("@IMP_ACC5_Amount", txt_IMPACC5_DiscAmt.Text.Trim());
                SqlParameter P_txt_IMPACC5_DiscExchRate = new SqlParameter("@IMP_ACC5_ExchRate", "");// _txt_IMPACC5_DiscExchRate.ToUpper());
                SqlParameter P_txt_IMPACC5_Princ_matu = new SqlParameter("@IMP_ACC5_Principal_MATU", txt_IMPACC5_Princ_matu.Text.Trim());
                SqlParameter P_txt_IMPACC5_Princ_lump = new SqlParameter("@IMP_ACC5_Principal_LUMP", txt_IMPACC5_Princ_lump.Text.Trim());
                SqlParameter P_txt_IMPACC5_Princ_Contract_no = new SqlParameter("@IMP_ACC5_Principal_Contract_No", txt_IMPACC5_Princ_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC5_Princ_Ex_Curr = new SqlParameter("@IMP_ACC5_Principal_Ex_Curr", txt_IMPACC5_Princ_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_Princ_Ex_rate = new SqlParameter("@IMP_ACC5_Principal_Exch_Rate", txt_IMPACC5_Princ_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC5_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Principal_Intnl_Exch_Rate", txt_IMPACC5_Princ_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC5_Interest_matu = new SqlParameter("@IMP_ACC5_Interest_MATU", txt_IMPACC5_Interest_matu.Text.Trim());
                SqlParameter P_txt_IMPACC5_Interest_lump = new SqlParameter("@IMP_ACC5_Interest_LUMP", txt_IMPACC5_Interest_lump.Text.Trim());
                SqlParameter P_txt_IMPACC5_Interest_Contract_no = new SqlParameter("@IMP_ACC5_Interest_Contract_No", txt_IMPACC5_Interest_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC5_Interest_Ex_Curr = new SqlParameter("@IMP_ACC5_Interest_Ex_Curr", txt_IMPACC5_Interest_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_Interest_Ex_rate = new SqlParameter("@IMP_ACC5_Interest_Exch_Rate", txt_IMPACC5_Interest_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC5_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Interest_Intnl_Exch_Rate", txt_IMPACC5_Interest_Intnl_Ex_rate.Text.Trim());

                SqlParameter P_txt_IMPACC5_Commission_matu = new SqlParameter("@IMP_ACC5_Commission_MATU", txt_IMPACC5_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC5_Commission_lump = new SqlParameter("@IMP_ACC5_Commission_LUMP", txt_IMPACC5_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC5_Commission_Contract_no = new SqlParameter("@IMP_ACC5_Commission_Contract_No", txt_IMPACC5_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC5_Commission_Ex_Curr = new SqlParameter("@IMP_ACC5_Commission_Ex_Curr", txt_IMPACC5_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_Commission_Ex_rate = new SqlParameter("@IMP_ACC5_Commission_Exch_Rate", txt_IMPACC5_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC5_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Commission_Intnl_Exch_Rate", txt_IMPACC5_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC5_Their_Commission_matu = new SqlParameter("@IMP_ACC5_Their_Commission_MATU", txt_IMPACC5_Their_Commission_matu.Text.Trim());
                SqlParameter P_txt_IMPACC5_Their_Commission_lump = new SqlParameter("@IMP_ACC5_Their_Commission_LUMP", txt_IMPACC5_Their_Commission_lump.Text.Trim());
                SqlParameter P_txt_IMPACC5_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC5_Their_Commission_Contract_No", txt_IMPACC5_Their_Commission_Contract_no.Text.Trim());
                SqlParameter P_txt_IMPACC5_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC5_Their_Commission_Ex_Curr", txt_IMPACC5_Their_Commission_Ex_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC5_Their_Commission_Exch_Rate", txt_IMPACC5_Their_Commission_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC5_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Their_Commission_Intnl_Exch_Rate", txt_IMPACC5_Their_Commission_Intnl_Ex_rate.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Code = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Code", txt_IMPACC5_CR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_AC_Short_Name = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Short_Name", txt_IMPACC5_CR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Cust_abbr = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC5_CR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Cust_Acc = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC5_CR_Cust_Acc.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Curr", txt_IMPACC5_CR_Acceptance_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Acceptance_amt = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Amt", txt_IMPACC5_CR_Acceptance_amt.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Acceptance_payer = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Payer", txt_IMPACC5_CR_Acceptance_payer.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Interest_Curr = new SqlParameter("@IMP_ACC5_CR_Interest_Curr", txt_IMPACC5_CR_Interest_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Interest_amt = new SqlParameter("@IMP_ACC5_CR_Interest_Amount", txt_IMPACC5_CR_Interest_amt.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Interest_payer = new SqlParameter("@IMP_ACC5_CR_Interest_Payer", txt_IMPACC5_CR_Interest_payer.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Curr", txt_IMPACC5_CR_Accept_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Amount", txt_IMPACC5_CR_Accept_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Payer", txt_IMPACC5_CR_Accept_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Curr", txt_IMPACC5_CR_Pay_Handle_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Amount", txt_IMPACC5_CR_Pay_Handle_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Payer", txt_IMPACC5_CR_Pay_Handle_Commission_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Others_Curr = new SqlParameter("@IMP_ACC5_CR_Others_Curr", txt_IMPACC5_CR_Others_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Others_amt = new SqlParameter("@IMP_ACC5_CR_Others_Amount", txt_IMPACC5_CR_Others_amt.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Others_Payer = new SqlParameter("@IMP_ACC5_CR_Others_Payer", txt_IMPACC5_CR_Others_Payer.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Curr", txt_IMPACC5_CR_Their_Commission_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Amount", txt_IMPACC5_CR_Their_Commission_amt.Text.Trim());
                SqlParameter P_txt_IMPACC5_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Payer", txt_IMPACC5_CR_Their_Commission_Payer.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Code = new SqlParameter("@IMP_ACC5_DR_Code", txt_IMPACC5_DR_Code.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_AC_Short_Name = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name", txt_IMPACC5_DR_AC_Short_Name.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_abbr = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr", txt_IMPACC5_DR_Cust_abbr.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_Acc = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No", txt_IMPACC5_DR_Cust_Acc.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr", txt_IMPACC5_DR_Cur_Acc_Curr.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount", txt_IMPACC5_DR_Cur_Acc_amt.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer", txt_IMPACC5_DR_Cur_Acc_payer.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr2", txt_IMPACC5_DR_Cur_Acc_Curr2.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount2", txt_IMPACC5_DR_Cur_Acc_amt2.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer2", txt_IMPACC5_DR_Cur_Acc_payer2.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr3", txt_IMPACC5_DR_Cur_Acc_Curr3.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount3", txt_IMPACC5_DR_Cur_Acc_amt3.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer3", txt_IMPACC5_DR_Cur_Acc_payer3.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr4", txt_IMPACC5_DR_Cur_Acc_Curr4.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount4", txt_IMPACC5_DR_Cur_Acc_amt4.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer4", txt_IMPACC5_DR_Cur_Acc_payer4.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr5", txt_IMPACC5_DR_Cur_Acc_Curr5.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount5", txt_IMPACC5_DR_Cur_Acc_amt5.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer5", txt_IMPACC5_DR_Cur_Acc_payer5.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr6", txt_IMPACC5_DR_Cur_Acc_Curr6.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount6", txt_IMPACC5_DR_Cur_Acc_amt6.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer6", txt_IMPACC5_DR_Cur_Acc_payer6.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Code2 = new SqlParameter("@IMP_ACC5_DR_Code2", txt_IMPACC5_DR_Code2.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name2", txt_IMPACC5_DR_AC_Short_Name2.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr2", txt_IMPACC5_DR_Cust_abbr2.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No2", txt_IMPACC5_DR_Cust_Acc2.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Code3 = new SqlParameter("@IMP_ACC5_DR_Code3", txt_IMPACC5_DR_Code3.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name3", txt_IMPACC5_DR_AC_Short_Name3.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr3", txt_IMPACC5_DR_Cust_abbr3.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No3", txt_IMPACC5_DR_Cust_Acc3.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Code4 = new SqlParameter("@IMP_ACC5_DR_Code4", txt_IMPACC5_DR_Code4.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name4", txt_IMPACC5_DR_AC_Short_Name4.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr4", txt_IMPACC5_DR_Cust_abbr4.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No4", txt_IMPACC5_DR_Cust_Acc4.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Code5 = new SqlParameter("@IMP_ACC5_DR_Code5", txt_IMPACC5_DR_Code5.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name5", txt_IMPACC5_DR_AC_Short_Name5.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr5", txt_IMPACC5_DR_Cust_abbr5.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No5", txt_IMPACC5_DR_Cust_Acc5.Text.Trim());

                SqlParameter P_txt_IMPACC5_DR_Code6 = new SqlParameter("@IMP_ACC5_DR_Code6", txt_IMPACC5_DR_Code6.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name6", txt_IMPACC5_DR_AC_Short_Name6.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr6", txt_IMPACC5_DR_Cust_abbr6.Text.Trim());
                SqlParameter P_txt_IMPACC5_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No6", txt_IMPACC5_DR_Cust_Acc6.Text.Trim());

                //-------------------------------------------------------------END----------------------------------------
                //----------------------------------------------------Anand 28-06-2023------------------------------------------------------
                // --------------------------------------- General Operations ----------------------------------------------------------------

                //---------------Anand 11-08-2023----------------------------
                string Generaloperation1 = "";
                if (chk_Generaloperation1.Checked == true)
                    Generaloperation1 = "Y";
                else
                    Generaloperation1 = "N";
                //----------------END---------------------------------------
                SqlParameter GO_Flag = new SqlParameter("@GO_Flag", Generaloperation1);
                SqlParameter GO_ValueDate = new SqlParameter("@GO_ValueDate", txtGO_ValueDate.Text.Trim());
                // SqlParameter GO_Ref_No = new SqlParameter("@GO_Ref_No", txtGO_Ref_No.Text.Trim());
                SqlParameter GO_Remark = new SqlParameter("@GO_Remark", txtGO_Remark.Text.Trim());
                SqlParameter GO_Section = new SqlParameter("@GO_Section", txtGO_Section.Text.Trim());
                SqlParameter GO_Comment = new SqlParameter("@GO_Comment", txtGO_Comment.Text.Trim());
                SqlParameter GO_Memo = new SqlParameter("@GO_Memo", txtGO_Memo.Text.Trim());
                SqlParameter GO_SchemeNo = new SqlParameter("@GO_SchemeNo", txtGO_SchemeNo.Text.Trim());
                // --------- Debit ----------------
                SqlParameter GO_Debit = new SqlParameter("@GO_Debit", txtGO_Debit.SelectedValue);//Modified by Anand10-08-2023
                SqlParameter GO_Debit_CCY = new SqlParameter("@GO_Debit_CCY", txtGO_Debit_CCY.Text.Trim());
                SqlParameter GO_Debit_Amt = new SqlParameter("@GO_Debit_Amt", txtGO_Debit_Amt.Text.Trim());
                SqlParameter GO_Debit_Cust = new SqlParameter("@GO_Debit_Cust", txtGO_Debit_Cust.Text.Trim());
                SqlParameter GO_Debit_Cust_Name = new SqlParameter("@GO_Debit_Cust_Name", txtGO_Debit_Cust_Name.Text.Trim());//Added by Anand 10-08-2023
                SqlParameter GO_Debit_Cust_AcCode = new SqlParameter("@GO_Debit_Cust_AcCode", txtGO_Debit_Cust_AcCode.Text.Trim());
                SqlParameter GO_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Debit_Cust_AcCode_Name", txtGO_Debit_Cust_AcCode_Name.Text.Trim());//Added by Anand10-08-2023
                SqlParameter GO_Debit_Cust_AccNo = new SqlParameter("@GO_Debit_Cust_AccNo", txtGO_Debit_Cust_AccNo.Text.Trim());
                SqlParameter GO_Debit_ExchRate = new SqlParameter("@GO_Debit_ExchRate", txtGO_Debit_ExchRate.Text.Trim());
                SqlParameter GO_Debit_ExchCCY = new SqlParameter("@GO_Debit_ExchCCY", txtGO_Debit_ExchCCY.Text.Trim());
                SqlParameter GO_Debit_Fund = new SqlParameter("@GO_Debit_Fund", txtGO_Debit_Fund.Text.Trim());
                SqlParameter GO_Debit_CheckNo = new SqlParameter("@GO_Debit_CheckNo", txtGO_Debit_CheckNo.Text.Trim());
                SqlParameter GO_Debit_Available = new SqlParameter("@GO_Debit_Available", txtGO_Debit_Available.Text.Trim());
                SqlParameter GO_Debit_Advice_Print = new SqlParameter("@GO_Debit_Advice_Print", txtGO_Debit_Advice_Print.Text.Trim());
                SqlParameter GO_Debit_Details = new SqlParameter("@GO_Debit_Details", txtGO_Debit_Details.Text.Trim());
                SqlParameter GO_Debit_Entity = new SqlParameter("@GO_Debit_Entity", txtGO_Debit_Entity.Text.Trim());
                SqlParameter GO_Debit_Division = new SqlParameter("@GO_Debit_Division", txtGO_Debit_Division.Text.Trim());
                SqlParameter GO_Debit_InterAmt = new SqlParameter("@GO_Debit_InterAmt", txtGO_Debit_InterAmt.Text.Trim());
                SqlParameter GO_Debit_InterRate = new SqlParameter("@GO_Debit_InterRate", txtGO_Debit_InterRate.Text.Trim());
                // --------- Credit ----------------
                SqlParameter GO_Credit = new SqlParameter("@GO_Credit", txtGO_Credit.SelectedValue);// Modified by Anand 10-08-2023
                SqlParameter GO_Credit_Amt = new SqlParameter("@GO_Credit_Amt", txtGO_Credit_Amt.Text.Trim());
                SqlParameter GO_Credit_CCY = new SqlParameter("@GO_Credit_CCY", txtGO_Credit_CCY.Text.Trim());
                SqlParameter GO_Credit_Cust = new SqlParameter("@GO_Credit_Cust", txtGO_Credit_Cust.Text.Trim());
                SqlParameter GO_Credit_Cust_Name = new SqlParameter("@GO_Credit_Cust_Name", txtGO_Credit_Cust_Name.Text.Trim());// Added by Anand 10-08-2023
                SqlParameter GO_Credit_Cust_AcCode = new SqlParameter("@GO_Credit_Cust_AcCode", txtGO_Credit_Cust_AcCode.Text.Trim());
                SqlParameter GO_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Credit_Cust_AcCode_Name", txtGO_Credit_Cust_AcCode_Name.Text.Trim());// Added by Anand 10-08-2023
                SqlParameter GO_Credit_Cust_AccNo = new SqlParameter("@GO_Credit_Cust_AccNo", txtGO_Credit_Cust_AccNo.Text.Trim());
                SqlParameter GO_Credit_ExchCCY = new SqlParameter("@GO_Credit_ExchCCY", txtGO_Credit_ExchCCY.Text.Trim());
                SqlParameter GO_Credit_ExchRate = new SqlParameter("@GO_Credit_ExchRate", txtGO_Credit_ExchRate.Text.Trim());
                SqlParameter GO_Credit_Fund = new SqlParameter("@GO_Credit_Fund", txtGO_Credit_Fund.Text.Trim());
                SqlParameter GO_Credit_CheckNo = new SqlParameter("@GO_Credit_CheckNo", txtGO_Credit_CheckNo.Text.Trim());
                SqlParameter GO_Credit_Available = new SqlParameter("@GO_Credit_Available", txtGO_Credit_Available.Text.Trim());
                SqlParameter GO_Credit_Advice_Print = new SqlParameter("@GO_Credit_Advice_Print", txtGO_Credit_Advice_Print.Text.Trim());
                SqlParameter GO_Credit_Details = new SqlParameter("@GO_Credit_Details", txtGO_Credit_Details.Text.Trim());
                SqlParameter GO_Credit_Entity = new SqlParameter("@GO_Credit_Entity", txtGO_Credit_Entity.Text.Trim());
                SqlParameter GO_Credit_Division = new SqlParameter("@GO_Credit_Division", txtGO_Credit_Division.Text.Trim());
                SqlParameter GO_Credit_InterAmt = new SqlParameter("@GO_Credit_InterAmt", txtGO_Credit_InterAmt.Text.Trim());
                SqlParameter GO_Credit_InterRate = new SqlParameter("@GO_Credit_InterRate", txtGO_Credit_InterRate.Text.Trim());

                // --------------------------------------- Normal General Operations -----------------------------------------------------------
                //---------------Anand 11-08-2023----------------------------
                string Generaloperation2 = "";
                if (chk_Generaloperation2.Checked == true)
                    Generaloperation2 = "Y";
                else
                    Generaloperation2 = "N";
                //----------------END---------------------------------------
                SqlParameter NGO_Flag = new SqlParameter("@NGO_Flag", Generaloperation2);
                SqlParameter NGO_ValueDate = new SqlParameter("@NGO_ValueDate", txtNGO_ValueDate.Text.Trim());
                // SqlParameter NGO_Ref_No = new SqlParameter("@NGO_Ref_No", txtNGO_Ref_No.Text.Trim());
                SqlParameter NGO_Remark = new SqlParameter("@NGO_Remark", txtNGO_Remark.Text.Trim());
                SqlParameter NGO_Section = new SqlParameter("@NGO_Section", txtNGO_Section.Text.Trim());
                SqlParameter NGO_Comment = new SqlParameter("@NGO_Comment", txtNGO_Comment.Text.Trim());
                SqlParameter NGO_Memo = new SqlParameter("@NGO_Memo", txtNGO_Memo.Text.Trim());
                SqlParameter NGO_SchemeNo = new SqlParameter("@NGO_SchemeNo", txtNGO_SchemeNo.Text.Trim());
                // --------- Debit ----------------
                SqlParameter NGO_Debit = new SqlParameter("@NGO_Debit", txtNGO_Debit.SelectedValue);// Modified by Anand 10-08-2023
                SqlParameter NGO_Debit_CCY = new SqlParameter("@NGO_Debit_CCY", txtNGO_Debit_CCY.Text.Trim());
                SqlParameter NGO_Debit_Amt = new SqlParameter("@NGO_Debit_Amt", txtNGO_Debit_Amt.Text.Trim());
                SqlParameter NGO_Debit_Cust = new SqlParameter("@NGO_Debit_Cust", txtNGO_Debit_Cust.Text.Trim());
                SqlParameter NGO_Debit_Cust_Name = new SqlParameter("@NGO_Debit_Cust_Name", txtNGO_Debit_Cust_Name.Text.Trim());// Added by Anand 10-08-2023
                SqlParameter NGO_Debit_Cust_AcCode = new SqlParameter("@NGO_Debit_Cust_AcCode", txtNGO_Debit_Cust_AcCode.Text.Trim());
                SqlParameter NGO_Debit_Cust_AcCode_Name = new SqlParameter("@NGO_Debit_Cust_AcCode_Name", txtNGO_Debit_Cust_AcCode_Name.Text.Trim());// Added by Anand 10-08-2023
                SqlParameter NGO_Debit_Cust_AccNo = new SqlParameter("@NGO_Debit_Cust_AccNo", txtNGO_Debit_Cust_AccNo.Text.Trim());
                SqlParameter NGO_Debit_ExchRate = new SqlParameter("@NGO_Debit_ExchRate", txtNGO_Debit_ExchRate.Text.Trim());
                SqlParameter NGO_Debit_ExchCCY = new SqlParameter("@NGO_Debit_ExchCCY", txtNGO_Debit_ExchCCY.Text.Trim());
                SqlParameter NGO_Debit_Fund = new SqlParameter("@NGO_Debit_Fund", txtNGO_Debit_Fund.Text.Trim());
                SqlParameter NGO_Debit_CheckNo = new SqlParameter("@NGO_Debit_CheckNo", txtNGO_Debit_CheckNo.Text.Trim());
                SqlParameter NGO_Debit_Available = new SqlParameter("@NGO_Debit_Available", txtNGO_Debit_Available.Text.Trim());
                SqlParameter NGO_Debit_Advice_Print = new SqlParameter("@NGO_Debit_Advice_Print", txtNGO_Debit_Advice_Print.Text.Trim());
                SqlParameter NGO_Debit_Details = new SqlParameter("@NGO_Debit_Details", txtNGO_Debit_Details.Text.Trim());
                SqlParameter NGO_Debit_Entity = new SqlParameter("@NGO_Debit_Entity", txtNGO_Debit_Entity.Text.Trim());
                SqlParameter NGO_Debit_Division = new SqlParameter("@NGO_Debit_Division", txtNGO_Debit_Division.Text.Trim());
                SqlParameter NGO_Debit_InterAmt = new SqlParameter("@NGO_Debit_InterAmt", txtNGO_Debit_InterAmt.Text.Trim());
                SqlParameter NGO_Debit_InterRate = new SqlParameter("@NGO_Debit_InterRate", txtNGO_Debit_InterRate.Text.Trim());
                // --------- Credit ----------------
                SqlParameter NGO_Credit = new SqlParameter("@NGO_Credit", txtNGO_Credit.SelectedValue);// Modified by Anand 10-08-2023
                SqlParameter NGO_Credit_Amt = new SqlParameter("@NGO_Credit_Amt", txtNGO_Credit_Amt.Text.Trim());
                SqlParameter NGO_Credit_CCY = new SqlParameter("@NGO_Credit_CCY", txtNGO_Credit_CCY.Text.Trim());
                SqlParameter NGO_Credit_Cust = new SqlParameter("@NGO_Credit_Cust", txtNGO_Credit_Cust.Text.Trim());
                SqlParameter NGO_Credit_Cust_Name = new SqlParameter("@NGO_Credit_Cust_Name", txtNGO_Credit_Cust_Name.Text.Trim());// Added by Anand 10-08-2023
                SqlParameter NGO_Credit_Cust_AcCode = new SqlParameter("@NGO_Credit_Cust_AcCode", txtNGO_Credit_Cust_AcCode.Text.Trim());
                SqlParameter NGO_Credit_Cust_AcCode_Name = new SqlParameter("@NGO_Credit_Cust_AcCode_Name", txtNGO_Credit_Cust_AcCode_Name.Text.Trim());
                SqlParameter NGO_Credit_Cust_AccNo = new SqlParameter("@NGO_Credit_Cust_AccNo", txtNGO_Credit_Cust_AccNo.Text.Trim());
                SqlParameter NGO_Credit_ExchCCY = new SqlParameter("@NGO_Credit_ExchCCY", txtNGO_Credit_ExchCCY.Text.Trim());
                SqlParameter NGO_Credit_ExchRate = new SqlParameter("@NGO_Credit_ExchRate", txtNGO_Credit_ExchRate.Text.Trim());
                SqlParameter NGO_Credit_Fund = new SqlParameter("@NGO_Credit_Fund", txtNGO_Credit_Fund.Text.Trim());
                SqlParameter NGO_Credit_CheckNo = new SqlParameter("@NGO_Credit_CheckNo", txtNGO_Credit_CheckNo.Text.Trim());
                SqlParameter NGO_Credit_Available = new SqlParameter("@NGO_Credit_Available", txtNGO_Credit_Available.Text.Trim());
                SqlParameter NGO_Credit_Advice_Print = new SqlParameter("@NGO_Credit_Advice_Print", txtNGO_Credit_Advice_Print.Text.Trim());
                SqlParameter NGO_Credit_Details = new SqlParameter("@NGO_Credit_Details", txtNGO_Credit_Details.Text.Trim());
                SqlParameter NGO_Credit_Entity = new SqlParameter("@NGO_Credit_Entity", txtNGO_Credit_Entity.Text.Trim());
                SqlParameter NGO_Credit_Division = new SqlParameter("@NGO_Credit_Division", txtNGO_Credit_Division.Text.Trim());
                SqlParameter NGO_Credit_InterAmt = new SqlParameter("@NGO_Credit_InterAmt", txtNGO_Credit_InterAmt.Text.Trim());
                SqlParameter NGO_Credit_InterRate = new SqlParameter("@NGO_Credit_InterRate", txtNGO_Credit_InterRate.Text.Trim());

                //----------------------------------------------------------END------------------------------------------------------------
                //-----------------------------------------------Anand 04-07-2023 INTER_OFFICE--------------------------------------
                //---------------Anand 11-08-2023----------------------------
                string InterOffice = "";
                if (chk_InterOffice.Checked == true)
                    InterOffice = "Y";
                else
                    InterOffice = "N";
                //----------------END---------------------------------------
                SqlParameter P_txt_GOAccChange_Flag = new SqlParameter("@GO_Acc_Change_Flag", InterOffice);
                SqlParameter P_txt_GOAccChange_ValueDate = new SqlParameter("@GO_Acc_Change_ValueDate", txtIO_ValueDate.Text.Trim());
                SqlParameter P_txt_GOAccChange_Ref_No = new SqlParameter("@GO_Acc_Change_TransRef_No", txt_GOAccChange_Ref_No.Text.Trim());
                SqlParameter P_txt_GOAccChange_Comment = new SqlParameter("@GO_Acc_Change_Comment", txt_GOAccChange_Comment.Text.Trim());
                SqlParameter P_txt_GOAccChange_SectionNo = new SqlParameter("@GO_Acc_Change_Section", txt_GOAccChange_SectionNo.Text.Trim());
                SqlParameter P_txt_GOAccChange_Remarks = new SqlParameter("@GO_Acc_Change_Remark", txt_GOAccChange_Remarks.Text.Trim());
                SqlParameter P_txt_GOAccChange_Memo = new SqlParameter("@GO_Acc_Change_Memo", txt_GOAccChange_Memo.Text.Trim());
                SqlParameter P_txt_GOAccChange_Scheme_no = new SqlParameter("@GO_Acc_Change_SchemeNo", txt_GOAccChange_Scheme_no.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Code = new SqlParameter("@GO_Acc_Change_Debit_Code", txt_GOAccChange_Debit_Code.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Curr = new SqlParameter("@GO_Acc_Change_Debit_CCY", txt_GOAccChange_Debit_Curr.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Amt = new SqlParameter("@GO_Acc_Change_Debit_Amt", txt_GOAccChange_Debit_Amt.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Cust = new SqlParameter("@GO_Acc_Change_Debit_Cust_abbr", txt_GOAccChange_Debit_Cust.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Cust_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_Name", txt_GOAccChange_Debit_Cust_Name.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode", txt_GOAccChange_Debit_Cust_AcCode.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode_Disc", txt_GOAccChange_Debit_Cust_AcCode_Name.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccNo", txt_GOAccChange_Debit_Cust_AccNo.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Debit_ExchRate", txt_GOAccChange_Debit_Exch_Rate.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Exch_CCY = new SqlParameter("@GO_Acc_Change_Debit_ExchCCY", txt_GOAccChange_Debit_Exch_CCY.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_FUND = new SqlParameter("@GO_Acc_Change_Debit_Fund", txt_GOAccChange_Debit_FUND.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Check_No = new SqlParameter("@GO_Acc_Change_Debit_CheckNo", txt_GOAccChange_Debit_Check_No.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Available = new SqlParameter("@GO_Acc_Change_Debit_Available", txt_GOAccChange_Debit_Available.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_AdPrint = new SqlParameter("@GO_Acc_Change_Debit_Advice_Print", txt_GOAccChange_Debit_AdPrint.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Details = new SqlParameter("@GO_Acc_Change_Debit_Details", txt_GOAccChange_Debit_Details.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Entity = new SqlParameter("@GO_Acc_Change_Debit_Entity", txt_GOAccChange_Debit_Entity.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Division = new SqlParameter("@GO_Acc_Change_Debit_Division", txt_GOAccChange_Debit_Division.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Debit_InterAmt", txt_GOAccChange_Debit_Inter_Amount.Text.Trim());
                SqlParameter P_txt_GOAccChange_Debit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Debit_InterRate", txt_GOAccChange_Debit_Inter_Rate.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Code = new SqlParameter("@GO_Acc_Change_Credit_Code", txt_GOAccChange_Credit_Code.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Curr = new SqlParameter("@GO_Acc_Change_Credit_CCY", txt_GOAccChange_Credit_Curr.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Amt = new SqlParameter("@GO_Acc_Change_Credit_Amt", txt_GOAccChange_Credit_Amt.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Cust = new SqlParameter("@GO_Acc_Change_Credit_Cust_abbr", txt_GOAccChange_Credit_Cust.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Cust_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_Name", txt_GOAccChange_Credit_Cust_Name.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode", txt_GOAccChange_Credit_Cust_AcCode.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode_Disc", txt_GOAccChange_Credit_Cust_AcCode_Name.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccNo", txt_GOAccChange_Credit_Cust_AccNo.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Credit_ExchRate", txt_GOAccChange_Credit_Exch_Rate.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Exch_CCY = new SqlParameter("@GO_Acc_Change_Credit_ExchCCY", txt_GOAccChange_Credit_Exch_Curr.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_FUND = new SqlParameter("@GO_Acc_Change_Credit_Fund", txt_GOAccChange_Credit_FUND.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Check_No = new SqlParameter("@GO_Acc_Change_Credit_CheckNo", txt_GOAccChange_Credit_Check_No.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Available = new SqlParameter("@GO_Acc_Change_Credit_Available", txt_GOAccChange_Credit_Available.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_AdPrint = new SqlParameter("@GO_Acc_Change_Credit_Advice_Print", txt_GOAccChange_Credit_AdPrint.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Details = new SqlParameter("@GO_Acc_Change_Credit_Details", txt_GOAccChange_Credit_Details.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Entity = new SqlParameter("@GO_Acc_Change_Credit_Entity", txt_GOAccChange_Credit_Entity.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Division = new SqlParameter("@GO_Acc_Change_Credit_Division", txt_GOAccChange_Credit_Division.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Credit_InterAmt", txt_GOAccChange_Credit_Inter_Amount.Text.Trim());
                SqlParameter P_txt_GOAccChange_Credit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Credit_InterRate", txt_GOAccChange_Credit_Inter_Rate.Text.Trim());

                //---------------------------------------------------End------------------------------------------------

                string query = "TF_UpdateExportRealisationDetails_Maker";
                string queryAccounting = "TF_UpdateExportRealisationDetails_AccountingEntry_Maker";
                string queryGO = "TF_UpdateExportRealisationDetails_GOEntry_Maker";
                string _result = "";
                string _resultAccounting = "";
                string _resultGO = "";

                TF_DATA objSave = new TF_DATA();

                _result = objSave.SaveDeleteData(query, mode, branch, docno, doctype, billtype, custacno, opc, RemiName, RemiCotry, RemiAdd, pconsigneeID, obc, PSwiftCode, pRB, pRBA, pRBC, p20, pProcessingDate, realdate, valuedate, realamt, realamtinr, exrate,
                                                 remark, nyrefno, payind1, srno, colamt, colamtinr, Payer, pIntFrom, pIntTo, pIntDays, pIntRate, pInterest, CheckIRM, CheckTT, pCCheckDummy,
                                                 loan1, bkline, bal, p1, p2, CrossrealCur, RelCrossCurRate, CrossrealAmt,

                                                 TTREFNO1, TTAmt1, TTREFNO2, TTAmt2, TTREFNO3, TTAmt3, TTREFNO4, TTAmt4, TTREFNO5, TTAmt5, pTTRef6, pTTAmt6, pTTRef7, pTTAmt7, pTTRef8, pTTAmt8, pTTRef9, pTTAmt9,
                                                 pTTRef10, pTTAmt10, pTTRef11, pTTAmt11, pTTRef12, pTTAmt12, pTTRef13, pTTAmt13, pTTRef14, pTTAmt14, pTTRef15, pTTAmt15, PTTCurr1, PTTCurr2, PTTCurr3, PTTCurr4, PTTCurr5,
                                                 PTTCurr6, PTTCurr7, PTTCurr8, PTTCurr9, PTTCurr10, PTTCurr11, PTTCurr12, PTTCurr13, PTTCurr14, PTTCurr15, PTotTTAmt1, PTotTTAmt2, PTotTTAmt3, PTotTTAmt4, PTotTTAmt5, PTotTTAmt6,
                                                 PTotTTAmt7, PTotTTAmt8, PTotTTAmt9, PTotTTAmt10, PTotTTAmt11, PTotTTAmt12, PTotTTAmt13, PTotTTAmt14, PTotTTAmt15, PBalTTAmt1, PBalTTAmt2, PBalTTAmt3, PBalTTAmt4, PBalTTAmt5, PBalTTAmt6,
                                                 PBalTTAmt7, PBalTTAmt8, PBalTTAmt9, PBalTTAmt10, PBalTTAmt11, PBalTTAmt12, PBalTTAmt13, PBalTTAmt14, PBalTTAmt15, PTTRealisedCurr1, PTTRealisedCurr2, PTTRealisedCurr3, PTTRealisedCurr4,
                                                 PTTRealisedCurr5, PTTRealisedCurr6, PTTRealisedCurr7, PTTRealisedCurr8, PTTRealisedCurr9, PTTRealisedCurr10, PTTRealisedCurr11, PTTRealisedCurr12, PTTRealisedCurr13, PTTRealisedCurr14,
                                                 PTTRealisedCurr15, PTTCrossCurrRate1, PTTCrossCurrRate2, PTTCrossCurrRate3, PTTCrossCurrRate4, PTTCrossCurrRate5, PTTCrossCurrRate6, PTTCrossCurrRate7, PTTCrossCurrRate8, PTTCrossCurrRate9,
                                                 PTTCrossCurrRate10, PTTCrossCurrRate11, PTTCrossCurrRate12, PTTCrossCurrRate13, PTTCrossCurrRate14, PTTCrossCurrRate15, PTTAmtRealised1, PTTAmtRealised2, PTTAmtRealised3, PTTAmtRealised4,
                                                 PTTAmtRealised5, PTTAmtRealised6, PTTAmtRealised7, PTTAmtRealised8, PTTAmtRealised9, PTTAmtRealised10, PTTAmtRealised11, PTTAmtRealised12, PTTAmtRealised13, PTTAmtRealised14, PTTAmtRealised15,

                                                 FIRC_Status, FIRC_NO, FIRC_AD_CODE,
                                                 FBKCharge, pcfcamt, pEEfcAmt, overdueamt, POutstandingAmt, PInstructedAmt, PLeiExchRate, pLeiInrAmt, pIPurposeCode, pIModeOfPay, checkerstatus, pCheckPendingSB);

                //--------------------------------------Anand26-06-2023--------------------------------------

                if (_result.Substring(0, 5) == "added" || _result == "updated")
                {

                    ////////////Import Accounting 1
                    _resultAccounting = objSave.SaveDeleteData(queryAccounting, mode, branch, docno, srno, p1, p2, P_chk_IMPACC1Flag, P_txt_IMPACC1_FCRefNo,
        P_txt_IMPACC1_DiscAmt, P_txt_IMPACC1_DiscExchRate,
        P_txt_IMPACC1_Princ_matu, P_txt_IMPACC1_Princ_lump, P_txt_IMPACC1_Princ_Contract_no, P_txt_IMPACC1_Princ_Ex_Curr, P_txt_IMPACC1_Princ_Ex_rate, P_txt_IMPACC1_Princ_Intnl_Ex_rate,
        P_txt_IMPACC1_Interest_matu, P_txt_IMPACC1_Interest_lump, P_txt_IMPACC1_Interest_Contract_no, P_txt_IMPACC1_Interest_Ex_Curr, P_txt_IMPACC1_Interest_Ex_rate, P_txt_IMPACC1_Interest_Intnl_Ex_rate,
        P_txt_IMPACC1_Commission_matu, P_txt_IMPACC1_Commission_lump, P_txt_IMPACC1_Commission_Contract_no, P_txt_IMPACC1_Commission_Ex_Curr, P_txt_IMPACC1_Commission_Ex_rate, P_txt_IMPACC1_Commission_Intnl_Ex_rate,
        P_txt_IMPACC1_Their_Commission_matu, P_txt_IMPACC1_Their_Commission_lump, P_txt_IMPACC1_Their_Commission_Contract_no, P_txt_IMPACC1_Their_Commission_Ex_Curr, P_txt_IMPACC1_Their_Commission_Ex_rate, P_txt_IMPACC1_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC1_CR_Code, P_txt_IMPACC1_CR_AC_Short_Name, P_txt_IMPACC1_CR_Cust_abbr, P_txt_IMPACC1_CR_Cust_Acc, P_txt_IMPACC1_CR_Acceptance_Curr, P_txt_IMPACC1_CR_Acceptance_amt, P_txt_IMPACC1_CR_Acceptance_payer,
        P_txt_IMPACC1_CR_Interest_Curr, P_txt_IMPACC1_CR_Interest_amt, P_txt_IMPACC1_CR_Interest_payer,
        P_txt_IMPACC1_CR_Accept_Commission_Curr, P_txt_IMPACC1_CR_Accept_Commission_amt, P_txt_IMPACC1_CR_Accept_Commission_Payer,
        P_txt_IMPACC1_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC1_CR_Pay_Handle_Commission_amt, P_txt_IMPACC1_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC1_CR_Others_Curr, P_txt_IMPACC1_CR_Others_amt, P_txt_IMPACC1_CR_Others_Payer,
        P_txt_IMPACC1_CR_Their_Commission_Curr, P_txt_IMPACC1_CR_Their_Commission_amt, P_txt_IMPACC1_CR_Their_Commission_Payer,
        P_txt_IMPACC1_DR_Code, P_txt_IMPACC1_DR_AC_Short_Name, P_txt_IMPACC1_DR_Cust_abbr, P_txt_IMPACC1_DR_Cust_Acc,
        P_txt_IMPACC1_DR_Cur_Acc_Curr, P_txt_IMPACC1_DR_Cur_Acc_amt, P_txt_IMPACC1_DR_Cur_Acc_payer,
        P_txt_IMPACC1_DR_Cur_Acc_Curr2, P_txt_IMPACC1_DR_Cur_Acc_amt2, P_txt_IMPACC1_DR_Cur_Acc_payer2,
        P_txt_IMPACC1_DR_Cur_Acc_Curr3, P_txt_IMPACC1_DR_Cur_Acc_amt3, P_txt_IMPACC1_DR_Cur_Acc_payer3,
        P_txt_IMPACC1_DR_Cur_Acc_Curr4, P_txt_IMPACC1_DR_Cur_Acc_amt4, P_txt_IMPACC1_DR_Cur_Acc_payer4,
        P_txt_IMPACC1_DR_Cur_Acc_Curr5, P_txt_IMPACC1_DR_Cur_Acc_amt5, P_txt_IMPACC1_DR_Cur_Acc_payer5,
        P_txt_IMPACC1_DR_Cur_Acc_Curr6, P_txt_IMPACC1_DR_Cur_Acc_amt6, P_txt_IMPACC1_DR_Cur_Acc_payer6,

        P_txt_IMPACC1_DR_Code2, P_txt_IMPACC1_DR_AC_Short_Name2, P_txt_IMPACC1_DR_Cust_abbr2, P_txt_IMPACC1_DR_Cust_Acc2,
        P_txt_IMPACC1_DR_Code3, P_txt_IMPACC1_DR_AC_Short_Name3, P_txt_IMPACC1_DR_Cust_abbr3, P_txt_IMPACC1_DR_Cust_Acc3,
        P_txt_IMPACC1_DR_Code4, P_txt_IMPACC1_DR_AC_Short_Name4, P_txt_IMPACC1_DR_Cust_abbr4, P_txt_IMPACC1_DR_Cust_Acc4,
        P_txt_IMPACC1_DR_Code5, P_txt_IMPACC1_DR_AC_Short_Name5, P_txt_IMPACC1_DR_Cust_abbr5, P_txt_IMPACC1_DR_Cust_Acc5,
        P_txt_IMPACC1_DR_Code6, P_txt_IMPACC1_DR_AC_Short_Name6, P_txt_IMPACC1_DR_Cust_abbr6, P_txt_IMPACC1_DR_Cust_Acc6,

        ////////////Import Accounting 2
        P_chk_IMPACC2Flag, P_txt_IMPACC2_FCRefNo,
        P_txt_IMPACC2_DiscAmt, P_txt_IMPACC2_DiscExchRate,
        P_txt_IMPACC2_Princ_matu, P_txt_IMPACC2_Princ_lump, P_txt_IMPACC2_Princ_Contract_no, P_txt_IMPACC2_Princ_Ex_Curr, P_txt_IMPACC2_Princ_Ex_rate, P_txt_IMPACC2_Princ_Intnl_Ex_rate,
        P_txt_IMPACC2_Interest_matu, P_txt_IMPACC2_Interest_lump, P_txt_IMPACC2_Interest_Contract_no, P_txt_IMPACC2_Interest_Ex_Curr, P_txt_IMPACC2_Interest_Ex_rate, P_txt_IMPACC2_Interest_Intnl_Ex_rate,
        P_txt_IMPACC2_Commission_matu, P_txt_IMPACC2_Commission_lump, P_txt_IMPACC2_Commission_Contract_no, P_txt_IMPACC2_Commission_Ex_Curr, P_txt_IMPACC2_Commission_Ex_rate, P_txt_IMPACC2_Commission_Intnl_Ex_rate,
        P_txt_IMPACC2_Their_Commission_matu, P_txt_IMPACC2_Their_Commission_lump, P_txt_IMPACC2_Their_Commission_Contract_no, P_txt_IMPACC2_Their_Commission_Ex_Curr, P_txt_IMPACC2_Their_Commission_Ex_rate, P_txt_IMPACC2_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC2_CR_Code, P_txt_IMPACC2_CR_AC_Short_Name, P_txt_IMPACC2_CR_Cust_abbr, P_txt_IMPACC2_CR_Cust_Acc, P_txt_IMPACC2_CR_Acceptance_Curr, P_txt_IMPACC2_CR_Acceptance_amt, P_txt_IMPACC2_CR_Acceptance_payer,
        P_txt_IMPACC2_CR_Interest_Curr, P_txt_IMPACC2_CR_Interest_amt, P_txt_IMPACC2_CR_Interest_payer,
        P_txt_IMPACC2_CR_Accept_Commission_Curr, P_txt_IMPACC2_CR_Accept_Commission_amt, P_txt_IMPACC2_CR_Accept_Commission_Payer,
        P_txt_IMPACC2_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC2_CR_Pay_Handle_Commission_amt, P_txt_IMPACC2_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC2_CR_Others_Curr, P_txt_IMPACC2_CR_Others_amt, P_txt_IMPACC2_CR_Others_Payer,
        P_txt_IMPACC2_CR_Their_Commission_Curr, P_txt_IMPACC2_CR_Their_Commission_amt, P_txt_IMPACC2_CR_Their_Commission_Payer,
        P_txt_IMPACC2_DR_Code, P_txt_IMPACC2_DR_AC_Short_Name, P_txt_IMPACC2_DR_Cust_abbr, P_txt_IMPACC2_DR_Cust_Acc,
        P_txt_IMPACC2_DR_Cur_Acc_Curr, P_txt_IMPACC2_DR_Cur_Acc_amt, P_txt_IMPACC2_DR_Cur_Acc_payer,
        P_txt_IMPACC2_DR_Cur_Acc_Curr2, P_txt_IMPACC2_DR_Cur_Acc_amt2, P_txt_IMPACC2_DR_Cur_Acc_payer2,
        P_txt_IMPACC2_DR_Cur_Acc_Curr3, P_txt_IMPACC2_DR_Cur_Acc_amt3, P_txt_IMPACC2_DR_Cur_Acc_payer3,
        P_txt_IMPACC2_DR_Cur_Acc_Curr4, P_txt_IMPACC2_DR_Cur_Acc_amt4, P_txt_IMPACC2_DR_Cur_Acc_payer4,
        P_txt_IMPACC2_DR_Cur_Acc_Curr5, P_txt_IMPACC2_DR_Cur_Acc_amt5, P_txt_IMPACC2_DR_Cur_Acc_payer5,
        P_txt_IMPACC2_DR_Cur_Acc_Curr6, P_txt_IMPACC2_DR_Cur_Acc_amt6, P_txt_IMPACC2_DR_Cur_Acc_payer6,

        P_txt_IMPACC2_DR_Code2, P_txt_IMPACC2_DR_AC_Short_Name2, P_txt_IMPACC2_DR_Cust_abbr2, P_txt_IMPACC2_DR_Cust_Acc2,
        P_txt_IMPACC2_DR_Code3, P_txt_IMPACC2_DR_AC_Short_Name3, P_txt_IMPACC2_DR_Cust_abbr3, P_txt_IMPACC2_DR_Cust_Acc3,
        P_txt_IMPACC2_DR_Code4, P_txt_IMPACC2_DR_AC_Short_Name4, P_txt_IMPACC2_DR_Cust_abbr4, P_txt_IMPACC2_DR_Cust_Acc4,
        P_txt_IMPACC2_DR_Code5, P_txt_IMPACC2_DR_AC_Short_Name5, P_txt_IMPACC2_DR_Cust_abbr5, P_txt_IMPACC2_DR_Cust_Acc5,
        P_txt_IMPACC2_DR_Code6, P_txt_IMPACC2_DR_AC_Short_Name6, P_txt_IMPACC2_DR_Cust_abbr6, P_txt_IMPACC2_DR_Cust_Acc6,

        ////////////Import Accounting 3
        P_chk_IMPACC3Flag, P_txt_IMPACC3_FCRefNo,
        P_txt_IMPACC3_DiscAmt, P_txt_IMPACC3_DiscExchRate,
        P_txt_IMPACC3_Princ_matu, P_txt_IMPACC3_Princ_lump, P_txt_IMPACC3_Princ_Contract_no, P_txt_IMPACC3_Princ_Ex_Curr, P_txt_IMPACC3_Princ_Ex_rate, P_txt_IMPACC3_Princ_Intnl_Ex_rate,
        P_txt_IMPACC3_Interest_matu, P_txt_IMPACC3_Interest_lump, P_txt_IMPACC3_Interest_Contract_no, P_txt_IMPACC3_Interest_Ex_Curr, P_txt_IMPACC3_Interest_Ex_rate, P_txt_IMPACC3_Interest_Intnl_Ex_rate,
        P_txt_IMPACC3_Commission_matu, P_txt_IMPACC3_Commission_lump, P_txt_IMPACC3_Commission_Contract_no, P_txt_IMPACC3_Commission_Ex_Curr, P_txt_IMPACC3_Commission_Ex_rate, P_txt_IMPACC3_Commission_Intnl_Ex_rate,
        P_txt_IMPACC3_Their_Commission_matu, P_txt_IMPACC3_Their_Commission_lump, P_txt_IMPACC3_Their_Commission_Contract_no, P_txt_IMPACC3_Their_Commission_Ex_Curr, P_txt_IMPACC3_Their_Commission_Ex_rate, P_txt_IMPACC3_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC3_CR_Code, P_txt_IMPACC3_CR_AC_Short_Name, P_txt_IMPACC3_CR_Cust_abbr, P_txt_IMPACC3_CR_Cust_Acc, P_txt_IMPACC3_CR_Acceptance_Curr, P_txt_IMPACC3_CR_Acceptance_amt, P_txt_IMPACC3_CR_Acceptance_payer,
        P_txt_IMPACC3_CR_Interest_Curr, P_txt_IMPACC3_CR_Interest_amt, P_txt_IMPACC3_CR_Interest_payer,
        P_txt_IMPACC3_CR_Accept_Commission_Curr, P_txt_IMPACC3_CR_Accept_Commission_amt, P_txt_IMPACC3_CR_Accept_Commission_Payer,
        P_txt_IMPACC3_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC3_CR_Pay_Handle_Commission_amt, P_txt_IMPACC3_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC3_CR_Others_Curr, P_txt_IMPACC3_CR_Others_amt, P_txt_IMPACC3_CR_Others_Payer,
        P_txt_IMPACC3_CR_Their_Commission_Curr, P_txt_IMPACC3_CR_Their_Commission_amt, P_txt_IMPACC3_CR_Their_Commission_Payer,
        P_txt_IMPACC3_DR_Code, P_txt_IMPACC3_DR_AC_Short_Name, P_txt_IMPACC3_DR_Cust_abbr, P_txt_IMPACC3_DR_Cust_Acc,
        P_txt_IMPACC3_DR_Cur_Acc_Curr, P_txt_IMPACC3_DR_Cur_Acc_amt, P_txt_IMPACC3_DR_Cur_Acc_payer,
        P_txt_IMPACC3_DR_Cur_Acc_Curr2, P_txt_IMPACC3_DR_Cur_Acc_amt2, P_txt_IMPACC3_DR_Cur_Acc_payer2,
        P_txt_IMPACC3_DR_Cur_Acc_Curr3, P_txt_IMPACC3_DR_Cur_Acc_amt3, P_txt_IMPACC3_DR_Cur_Acc_payer3,
        P_txt_IMPACC3_DR_Cur_Acc_Curr4, P_txt_IMPACC3_DR_Cur_Acc_amt4, P_txt_IMPACC3_DR_Cur_Acc_payer4,
        P_txt_IMPACC3_DR_Cur_Acc_Curr5, P_txt_IMPACC3_DR_Cur_Acc_amt5, P_txt_IMPACC3_DR_Cur_Acc_payer5,
        P_txt_IMPACC3_DR_Cur_Acc_Curr6, P_txt_IMPACC3_DR_Cur_Acc_amt6, P_txt_IMPACC3_DR_Cur_Acc_payer6,

        P_txt_IMPACC3_DR_Code2, P_txt_IMPACC3_DR_AC_Short_Name2, P_txt_IMPACC3_DR_Cust_abbr2, P_txt_IMPACC3_DR_Cust_Acc2,
        P_txt_IMPACC3_DR_Code3, P_txt_IMPACC3_DR_AC_Short_Name3, P_txt_IMPACC3_DR_Cust_abbr3, P_txt_IMPACC3_DR_Cust_Acc3,
        P_txt_IMPACC3_DR_Code4, P_txt_IMPACC3_DR_AC_Short_Name4, P_txt_IMPACC3_DR_Cust_abbr4, P_txt_IMPACC3_DR_Cust_Acc4,
        P_txt_IMPACC3_DR_Code5, P_txt_IMPACC3_DR_AC_Short_Name5, P_txt_IMPACC3_DR_Cust_abbr5, P_txt_IMPACC3_DR_Cust_Acc5,
        P_txt_IMPACC3_DR_Code6, P_txt_IMPACC3_DR_AC_Short_Name6, P_txt_IMPACC3_DR_Cust_abbr6, P_txt_IMPACC3_DR_Cust_Acc6,

        ////////Import accounting 4
        P_chk_IMPACC4Flag, P_txt_IMPACC4_FCRefNo,
        P_txt_IMPACC4_DiscAmt, P_txt_IMPACC4_DiscExchRate,
        P_txt_IMPACC4_Princ_matu, P_txt_IMPACC4_Princ_lump, P_txt_IMPACC4_Princ_Contract_no, P_txt_IMPACC4_Princ_Ex_Curr, P_txt_IMPACC4_Princ_Ex_rate, P_txt_IMPACC4_Princ_Intnl_Ex_rate,
        P_txt_IMPACC4_Interest_matu, P_txt_IMPACC4_Interest_lump, P_txt_IMPACC4_Interest_Contract_no, P_txt_IMPACC4_Interest_Ex_Curr, P_txt_IMPACC4_Interest_Ex_rate, P_txt_IMPACC4_Interest_Intnl_Ex_rate,
        P_txt_IMPACC4_Commission_matu, P_txt_IMPACC4_Commission_lump, P_txt_IMPACC4_Commission_Contract_no, P_txt_IMPACC4_Commission_Ex_Curr, P_txt_IMPACC4_Commission_Ex_rate, P_txt_IMPACC4_Commission_Intnl_Ex_rate,
        P_txt_IMPACC4_Their_Commission_matu, P_txt_IMPACC4_Their_Commission_lump, P_txt_IMPACC4_Their_Commission_Contract_no, P_txt_IMPACC4_Their_Commission_Ex_Curr, P_txt_IMPACC4_Their_Commission_Ex_rate, P_txt_IMPACC4_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC4_CR_Code, P_txt_IMPACC4_CR_AC_Short_Name, P_txt_IMPACC4_CR_Cust_abbr, P_txt_IMPACC4_CR_Cust_Acc, P_txt_IMPACC4_CR_Acceptance_Curr, P_txt_IMPACC4_CR_Acceptance_amt, P_txt_IMPACC4_CR_Acceptance_payer,
        P_txt_IMPACC4_CR_Interest_Curr, P_txt_IMPACC4_CR_Interest_amt, P_txt_IMPACC4_CR_Interest_payer,
        P_txt_IMPACC4_CR_Accept_Commission_Curr, P_txt_IMPACC4_CR_Accept_Commission_amt, P_txt_IMPACC4_CR_Accept_Commission_Payer,
        P_txt_IMPACC4_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC4_CR_Pay_Handle_Commission_amt, P_txt_IMPACC4_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC4_CR_Others_Curr, P_txt_IMPACC4_CR_Others_amt, P_txt_IMPACC4_CR_Others_Payer,
        P_txt_IMPACC4_CR_Their_Commission_Curr, P_txt_IMPACC4_CR_Their_Commission_amt, P_txt_IMPACC4_CR_Their_Commission_Payer,
        P_txt_IMPACC4_DR_Code, P_txt_IMPACC4_DR_AC_Short_Name, P_txt_IMPACC4_DR_Cust_abbr, P_txt_IMPACC4_DR_Cust_Acc,
        P_txt_IMPACC4_DR_Cur_Acc_Curr, P_txt_IMPACC4_DR_Cur_Acc_amt, P_txt_IMPACC4_DR_Cur_Acc_payer,
        P_txt_IMPACC4_DR_Cur_Acc_Curr2, P_txt_IMPACC4_DR_Cur_Acc_amt2, P_txt_IMPACC4_DR_Cur_Acc_payer2,
        P_txt_IMPACC4_DR_Cur_Acc_Curr3, P_txt_IMPACC4_DR_Cur_Acc_amt3, P_txt_IMPACC4_DR_Cur_Acc_payer3,
        P_txt_IMPACC4_DR_Cur_Acc_Curr4, P_txt_IMPACC4_DR_Cur_Acc_amt4, P_txt_IMPACC4_DR_Cur_Acc_payer4,
        P_txt_IMPACC4_DR_Cur_Acc_Curr5, P_txt_IMPACC4_DR_Cur_Acc_amt5, P_txt_IMPACC4_DR_Cur_Acc_payer5,
        P_txt_IMPACC4_DR_Cur_Acc_Curr6, P_txt_IMPACC4_DR_Cur_Acc_amt6, P_txt_IMPACC4_DR_Cur_Acc_payer6,

        P_txt_IMPACC4_DR_Code2, P_txt_IMPACC4_DR_AC_Short_Name2, P_txt_IMPACC4_DR_Cust_abbr2, P_txt_IMPACC4_DR_Cust_Acc2,
        P_txt_IMPACC4_DR_Code3, P_txt_IMPACC4_DR_AC_Short_Name3, P_txt_IMPACC4_DR_Cust_abbr3, P_txt_IMPACC4_DR_Cust_Acc3,
        P_txt_IMPACC4_DR_Code4, P_txt_IMPACC4_DR_AC_Short_Name4, P_txt_IMPACC4_DR_Cust_abbr4, P_txt_IMPACC4_DR_Cust_Acc4,
        P_txt_IMPACC4_DR_Code5, P_txt_IMPACC4_DR_AC_Short_Name5, P_txt_IMPACC4_DR_Cust_abbr5, P_txt_IMPACC4_DR_Cust_Acc5,
        P_txt_IMPACC4_DR_Code6, P_txt_IMPACC4_DR_AC_Short_Name6, P_txt_IMPACC4_DR_Cust_abbr6, P_txt_IMPACC4_DR_Cust_Acc6,

        /////////import accounting 5
        P_chk_IMPACC5Flag, P_txt_IMPACC5_FCRefNo,
        P_txt_IMPACC5_DiscAmt, P_txt_IMPACC5_DiscExchRate,
        P_txt_IMPACC5_Princ_matu, P_txt_IMPACC5_Princ_lump, P_txt_IMPACC5_Princ_Contract_no, P_txt_IMPACC5_Princ_Ex_Curr, P_txt_IMPACC5_Princ_Ex_rate, P_txt_IMPACC5_Princ_Intnl_Ex_rate,
        P_txt_IMPACC5_Interest_matu, P_txt_IMPACC5_Interest_lump, P_txt_IMPACC5_Interest_Contract_no, P_txt_IMPACC5_Interest_Ex_Curr, P_txt_IMPACC5_Interest_Ex_rate, P_txt_IMPACC5_Interest_Intnl_Ex_rate,
        P_txt_IMPACC5_Commission_matu, P_txt_IMPACC5_Commission_lump, P_txt_IMPACC5_Commission_Contract_no, P_txt_IMPACC5_Commission_Ex_Curr, P_txt_IMPACC5_Commission_Ex_rate, P_txt_IMPACC5_Commission_Intnl_Ex_rate,
        P_txt_IMPACC5_Their_Commission_matu, P_txt_IMPACC5_Their_Commission_lump, P_txt_IMPACC5_Their_Commission_Contract_no, P_txt_IMPACC5_Their_Commission_Ex_Curr, P_txt_IMPACC5_Their_Commission_Ex_rate, P_txt_IMPACC5_Their_Commission_Intnl_Ex_rate,
        P_txt_IMPACC5_CR_Code, P_txt_IMPACC5_CR_AC_Short_Name, P_txt_IMPACC5_CR_Cust_abbr, P_txt_IMPACC5_CR_Cust_Acc, P_txt_IMPACC5_CR_Acceptance_Curr, P_txt_IMPACC5_CR_Acceptance_amt, P_txt_IMPACC5_CR_Acceptance_payer,
        P_txt_IMPACC5_CR_Interest_Curr, P_txt_IMPACC5_CR_Interest_amt, P_txt_IMPACC5_CR_Interest_payer,
        P_txt_IMPACC5_CR_Accept_Commission_Curr, P_txt_IMPACC5_CR_Accept_Commission_amt, P_txt_IMPACC5_CR_Accept_Commission_Payer,
        P_txt_IMPACC5_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC5_CR_Pay_Handle_Commission_amt, P_txt_IMPACC5_CR_Pay_Handle_Commission_Payer,
        P_txt_IMPACC5_CR_Others_Curr, P_txt_IMPACC5_CR_Others_amt, P_txt_IMPACC5_CR_Others_Payer,
        P_txt_IMPACC5_CR_Their_Commission_Curr, P_txt_IMPACC5_CR_Their_Commission_amt, P_txt_IMPACC5_CR_Their_Commission_Payer,
        P_txt_IMPACC5_DR_Code, P_txt_IMPACC5_DR_AC_Short_Name, P_txt_IMPACC5_DR_Cust_abbr, P_txt_IMPACC5_DR_Cust_Acc,
        P_txt_IMPACC5_DR_Cur_Acc_Curr, P_txt_IMPACC5_DR_Cur_Acc_amt, P_txt_IMPACC5_DR_Cur_Acc_payer,
        P_txt_IMPACC5_DR_Cur_Acc_Curr2, P_txt_IMPACC5_DR_Cur_Acc_amt2, P_txt_IMPACC5_DR_Cur_Acc_payer2,
        P_txt_IMPACC5_DR_Cur_Acc_Curr3, P_txt_IMPACC5_DR_Cur_Acc_amt3, P_txt_IMPACC5_DR_Cur_Acc_payer3,
        P_txt_IMPACC5_DR_Cur_Acc_Curr4, P_txt_IMPACC5_DR_Cur_Acc_amt4, P_txt_IMPACC5_DR_Cur_Acc_payer4,
        P_txt_IMPACC5_DR_Cur_Acc_Curr5, P_txt_IMPACC5_DR_Cur_Acc_amt5, P_txt_IMPACC5_DR_Cur_Acc_payer5,
        P_txt_IMPACC5_DR_Cur_Acc_Curr6, P_txt_IMPACC5_DR_Cur_Acc_amt6, P_txt_IMPACC5_DR_Cur_Acc_payer6,

        P_txt_IMPACC5_DR_Code2, P_txt_IMPACC5_DR_AC_Short_Name2, P_txt_IMPACC5_DR_Cust_abbr2, P_txt_IMPACC5_DR_Cust_Acc2,
        P_txt_IMPACC5_DR_Code3, P_txt_IMPACC5_DR_AC_Short_Name3, P_txt_IMPACC5_DR_Cust_abbr3, P_txt_IMPACC5_DR_Cust_Acc3,
        P_txt_IMPACC5_DR_Code4, P_txt_IMPACC5_DR_AC_Short_Name4, P_txt_IMPACC5_DR_Cust_abbr4, P_txt_IMPACC5_DR_Cust_Acc4,
        P_txt_IMPACC5_DR_Code5, P_txt_IMPACC5_DR_AC_Short_Name5, P_txt_IMPACC5_DR_Cust_abbr5, P_txt_IMPACC5_DR_Cust_Acc5,
        P_txt_IMPACC5_DR_Code6, P_txt_IMPACC5_DR_AC_Short_Name6, P_txt_IMPACC5_DR_Cust_abbr6, P_txt_IMPACC5_DR_Cust_Acc6);

                    //-------------------------------- General Operations --------------------------------------
                    _resultGO = objSave.SaveDeleteData(queryGO, mode, branch, docno, srno, GO_Flag,
          GO_ValueDate,
                                                 //GO_Ref_No,
                                                 GO_Remark, GO_Section, GO_Comment, GO_Memo, GO_SchemeNo,
                                                 //------------------------- Debit ------------------------------
                                                 GO_Debit, GO_Debit_CCY, GO_Debit_Amt, GO_Debit_Cust, GO_Debit_Cust_Name, GO_Debit_Cust_AcCode, GO_Debit_Cust_AcCode_Name, GO_Debit_Cust_AccNo, GO_Debit_ExchRate, GO_Debit_ExchCCY, GO_Debit_Fund,
                                                 GO_Debit_CheckNo, GO_Debit_Available, GO_Debit_Advice_Print, GO_Debit_Details, GO_Debit_Entity, GO_Debit_Division, GO_Debit_InterAmt, GO_Debit_InterRate,
                                                 //------------------------- Credit ------------------------------
                                                 GO_Credit, GO_Credit_Amt, GO_Credit_CCY, GO_Credit_Cust, GO_Credit_Cust_Name, GO_Credit_Cust_AcCode, GO_Credit_Cust_AcCode_Name, GO_Credit_Cust_AccNo, GO_Credit_ExchCCY, GO_Credit_ExchRate, GO_Credit_Fund,
                                                 GO_Credit_CheckNo, GO_Credit_Available, GO_Credit_Advice_Print, GO_Credit_Details, GO_Credit_Entity, GO_Credit_Division, GO_Credit_InterAmt, GO_Credit_InterRate,
                                                //-------------------------------- Normal General Operations --------------------------------------
                                                NGO_Flag,
                    NGO_ValueDate,
                                                 //NGO_Ref_No, 
                                                 NGO_Remark, NGO_Section, NGO_Comment, NGO_Memo, NGO_SchemeNo,
                                                 //------------------------- Debit ------------------------------
                                                 NGO_Debit, NGO_Debit_CCY, NGO_Debit_Amt, NGO_Debit_Cust, NGO_Debit_Cust_Name, NGO_Debit_Cust_AcCode, NGO_Debit_Cust_AcCode_Name, NGO_Debit_Cust_AccNo, NGO_Debit_ExchRate, NGO_Debit_ExchCCY, NGO_Debit_Fund,
                                                 NGO_Debit_CheckNo, NGO_Debit_Available, NGO_Debit_Advice_Print, NGO_Debit_Details, NGO_Debit_Entity, NGO_Debit_Division, NGO_Debit_InterAmt, NGO_Debit_InterRate,
                                                 //------------------------- Credit ------------------------------
                                                 NGO_Credit, NGO_Credit_Amt, NGO_Credit_CCY, NGO_Credit_Cust, NGO_Credit_Cust_Name, NGO_Credit_Cust_AcCode, NGO_Credit_Cust_AcCode_Name, NGO_Credit_Cust_AccNo, NGO_Credit_ExchCCY, NGO_Credit_ExchRate, NGO_Credit_Fund,
                                                 NGO_Credit_CheckNo, NGO_Credit_Available, NGO_Credit_Advice_Print, NGO_Credit_Details, NGO_Credit_Entity, NGO_Credit_Division, NGO_Credit_InterAmt, NGO_Credit_InterRate,


        //-------------------------------------------END-----------------------------------------------
        //---------------------------------------------------Anand 04-07-2023-----------------------------------
        P_txt_GOAccChange_Flag,
                    P_txt_GOAccChange_ValueDate, P_txt_GOAccChange_Ref_No,
        P_txt_GOAccChange_Comment,
        P_txt_GOAccChange_SectionNo, P_txt_GOAccChange_Remarks, P_txt_GOAccChange_Memo,
        P_txt_GOAccChange_Scheme_no,
        P_txt_GOAccChange_Debit_Code, P_txt_GOAccChange_Debit_Curr, P_txt_GOAccChange_Debit_Amt,
        P_txt_GOAccChange_Debit_Cust, P_txt_GOAccChange_Debit_Cust_Name,
        P_txt_GOAccChange_Debit_Cust_AcCode, P_txt_GOAccChange_Debit_Cust_AcCode_Name, P_txt_GOAccChange_Debit_Cust_AccNo,
        P_txt_GOAccChange_Debit_Exch_Rate, P_txt_GOAccChange_Debit_Exch_CCY,
        P_txt_GOAccChange_Debit_FUND, P_txt_GOAccChange_Debit_Check_No, P_txt_GOAccChange_Debit_Available,
        P_txt_GOAccChange_Debit_AdPrint, P_txt_GOAccChange_Debit_Details, P_txt_GOAccChange_Debit_Entity,
        P_txt_GOAccChange_Debit_Division, P_txt_GOAccChange_Debit_Inter_Amount, P_txt_GOAccChange_Debit_Inter_Rate,
        P_txt_GOAccChange_Credit_Code, P_txt_GOAccChange_Credit_Curr, P_txt_GOAccChange_Credit_Amt,
        P_txt_GOAccChange_Credit_Cust, P_txt_GOAccChange_Credit_Cust_Name,
        P_txt_GOAccChange_Credit_Cust_AcCode, P_txt_GOAccChange_Credit_Cust_AcCode_Name, P_txt_GOAccChange_Credit_Cust_AccNo,
        P_txt_GOAccChange_Credit_Exch_Rate, P_txt_GOAccChange_Credit_Exch_CCY,
        P_txt_GOAccChange_Credit_FUND, P_txt_GOAccChange_Credit_Check_No, P_txt_GOAccChange_Credit_Available,
        P_txt_GOAccChange_Credit_AdPrint, P_txt_GOAccChange_Credit_Details, P_txt_GOAccChange_Credit_Entity,
        P_txt_GOAccChange_Credit_Division, P_txt_GOAccChange_Credit_Inter_Amount, P_txt_GOAccChange_Credit_Inter_Rate, p1, p2
                                                 //-------------------------------------------------------end-----------------------------------------
                                                 );


                    string _resultIRM = "";

                    if (chkIRMCreate.Checked == true)
                    {
                        _resultIRM = objData.SaveDeleteData("TF_EXP_IRM_Realisation_AddUpdate", branch, docno, srno, realdate, custacno, pIPurposeCode, CrossrealCur, realamt, mode, exrate, realamtinr,
         FIRC_NO, FIRC_AD_CODE, opc, RemiName, RemiCotry, valuedate, RemiAdd, pRB, pRBA, p20, pRBC, p1, p2, PSwiftCode,
                            pIBUTransID, pIIFSCCode, pIRemiADcode, pIIECcode, pIPanNo, pIModeOfPay, pBankReferencenumber, pBankAccountNumber, pIRMStatus, CrossrealAmt);
                    }
                    //=========================LEI Changes===========================//
                    string _ForeignOrLocalLei = txtForeignORLocal.Text;

                    if (_ForeignOrLocalLei == "F")
                    {
                        SqlParameter LEIbCode = new SqlParameter("@bCode", Request.QueryString["BranchCode"].Trim());
                        SqlParameter LEIdocNo = new SqlParameter("@docNo", txtDocNo.Text);
                        SqlParameter LEIdocPrfx = new SqlParameter("@docPrfx", Request.QueryString["DocPrFx"].Trim());
                        SqlParameter LEIbillType = new SqlParameter("@billType", billtype.Value);
                        SqlParameter LEICustAcNo = new SqlParameter("@custAcNo", txtCustAcNo.Text.Trim());
                        SqlParameter LEICust_Name = new SqlParameter("@CustName", hdnCustname.Value);
                        SqlParameter LEICust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustabbr.Value);
                        SqlParameter LEICust_LEI = new SqlParameter("@Cust_LEI", hdncustlei.Value);
                        SqlParameter LEICust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdncustleiexpiry.Value);
                        SqlParameter LEIOverseasPartyID = new SqlParameter("@OverseasPartyID", txtOverseasParty.Text.Trim());
                        SqlParameter LEIOverseas_LEI = new SqlParameter("@Overseas_LEI", hdnoverseaslei.Value);
                        SqlParameter LEIOverseas_LEI_Expiry = new SqlParameter("@Overseas_LEI_Expiry", hdnoverseasleiexpiry.Value);
                        SqlParameter LEIAmountFC = new SqlParameter("@ActbillAmt", txtAmtRealised.Text.Trim());
                        SqlParameter LEIAmtInINR = new SqlParameter("@ActbillAmtinRS", hdnbillamtinr.Value);
                        SqlParameter LEICurrency = new SqlParameter("@Curr", txt_relcur.Text);
                        SqlParameter LEIExchRate = new SqlParameter("@exchRtEBR", hdnleiExchRate.Value);
                        SqlParameter LEIdateRcvd = new SqlParameter("@dateRcvd", txtDateRealised.Text.Trim());
                        SqlParameter LEIDueDate = new SqlParameter("@DueDate", txtValueDate.Text.Trim());
                        SqlParameter LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value);
                        SqlParameter LEI_SpecialFlag = new SqlParameter("@LEI_SpecialFlag", hdnLeiSpecialFlag.Value);
                        string Trans_Status1 = "S";
                        SqlParameter LEITrans_Status = new SqlParameter("@Trans_Status", Trans_Status1);
                        SqlParameter LEISrNo = new SqlParameter("@SrNo", txtSrNo.Text);
                        SqlParameter LEIPayIndicator = new SqlParameter("@Realised_PAY_Indicator", payind);
                        SqlParameter LEIuser = new SqlParameter("@user", Session["userName"].ToString());
                        SqlParameter LEIuploadingdate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                        string _resultLEI = "";
                        string _queryLEI = "TF_EXP_Update_LEITransaction";
                        TF_DATA objSaveLEI = new TF_DATA();

                        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                        dateInfo.ShortDatePattern = "dd/MM/yyyy";
                        DateTime LEIEffectDate = Convert.ToDateTime(lblLEIEffectDate.Text.Trim(), dateInfo);
                        DateTime LEIValueDate = Convert.ToDateTime(txtDateRealised.Text.Trim(), dateInfo);

                        if (LEIEffectDate <= LEIValueDate)
                        {
                            _resultLEI = objSaveLEI.SaveDeleteData(_queryLEI, LEIbCode, LEIdocNo, LEIdocPrfx, LEIbillType, LEICustAcNo, LEICust_Name, LEICust_Abbr, LEICust_LEI, LEICust_LEI_Expiry, LEIOverseasPartyID,
                                LEIOverseas_LEI, LEIOverseas_LEI_Expiry, LEIAmountFC, LEIAmtInINR, LEICurrency, LEIExchRate, LEIdateRcvd, LEIDueDate, LEI_Flag, LEI_SpecialFlag, LEITrans_Status, LEISrNo, LEIPayIndicator, LEIuser, LEIuploadingdate);
                        }
                    }
                    string _resultShippingBill = "";
                    string _queryShippingBill = "TF_EXP_IRM_ShippingBill_Realisation_AddUpdate";

                    SqlParameter ShippingbillbCode = new SqlParameter("@branch", Request.QueryString["BranchCode"].Trim());
                    SqlParameter Docsrno = new SqlParameter("@SrNo", txtSrNo.Text);
                    Label lblShipping_Bill_No = new Label();
                    SqlParameter pShipping_Bill_No = new SqlParameter("@Shipping_Bill_No", SqlDbType.VarChar);
                    Label lblInvoiceNo = new Label();
                    SqlParameter pInvoiceNo = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                    Label lblDocument_No = new Label();
                    SqlParameter pDocument_No = new SqlParameter("@docno", SqlDbType.VarChar);
                    Label lblShipCurr = new Label();
                    SqlParameter pShipCurr = new SqlParameter("@ShipCurr", SqlDbType.VarChar);
                    Label lblFOBAmount = new Label();
                    SqlParameter pFOBAmount = new SqlParameter("@FOBAmount", SqlDbType.VarChar);
                    Label lblInsuranceAmount = new Label();
                    SqlParameter pInsuranceAmount = new SqlParameter("@InsuranceAmount", SqlDbType.VarChar);
                    Label lblFreightAmount = new Label();
                    SqlParameter pFreightAmount = new SqlParameter("@FreightAmount", SqlDbType.VarChar);
                    Label lblAmount = new Label();
                    SqlParameter pAmount = new SqlParameter("@Amount", SqlDbType.VarChar);
                    DropDownList ddlpartfull = new DropDownList();
                    SqlParameter ppartfull = new SqlParameter("@Indicator_PartFull", SqlDbType.VarChar);
                    TextBox txtsettelementamt = new TextBox();
                    SqlParameter psettelementamt = new SqlParameter("@SettlementAmt", SqlDbType.VarChar);
                    TextBox txtFBANK = new TextBox();
                    SqlParameter pFBANK = new SqlParameter("@FBANK", SqlDbType.VarChar);

                    for (
                        int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
                    {


                        CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("RowChkAllow");
                        if (chkrow.Checked == true)
                        {


                            lblShipping_Bill_No = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblShipping_Bill_No");
                            lblInvoiceNo = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblInvoiceNo");
                            lblDocument_No = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblDocument_No");
                            lblShipCurr = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblShipCurr");
                            lblFOBAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblFOBAmount");
                            lblInsuranceAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblInsuranceAmount");
                            lblFreightAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblFreightAmount");
                            lblAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblAmount");
                            ddlpartfull = (DropDownList)GridViewGRPPCustomsDetails.Rows[i].FindControl("ddlpartfull");
                            txtsettelementamt = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtsettelementamt");
                            txtFBANK = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtFBANK");

                            pShipping_Bill_No.Value = lblShipping_Bill_No.Text;
                            pInvoiceNo.Value = lblInvoiceNo.Text;
                            pDocument_No.Value = lblDocument_No.Text;
                            pShipCurr.Value = lblShipCurr.Text;
                            pFOBAmount.Value = lblFOBAmount.Text;
                            pInsuranceAmount.Value = lblInsuranceAmount.Text;
                            pFreightAmount.Value = lblFreightAmount.Text;
                            pAmount.Value = lblAmount.Text;
                            ppartfull.Value = ddlpartfull.Text;
                            psettelementamt.Value = txtsettelementamt.Text;
                            pFBANK.Value = txtFBANK.Text;


                            _resultShippingBill = objData.SaveDeleteData(_queryShippingBill, ShippingbillbCode, Docsrno, pShipping_Bill_No, pInvoiceNo, pDocument_No, pFOBAmount, pInsuranceAmount,
                                pFreightAmount, pAmount, ppartfull, psettelementamt, pFBANK, mode, p1, p2, CrossrealCur, RelCrossCurRate, pShipCurr);
                        }

                    }

                    if (_result.Substring(0, 5) == "added")
                    {
                        txtDocNo.Text = "";
                    }
                    //=========================END====================================//
                    string _script = "";

                    var argument = ((Button)sender).CommandArgument;

                    if (_result.Substring(0, 5) == "added")
                    // if (_result == "added")
                    {
                        if (argument.ToString() == "print")
                        {
                            _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                        else
                        {
                            _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=Submit&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }
                    else
                    {
                        if (_result == "updated")
                        {
                            if (argument.ToString() == "print")
                            {
                                _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            }
                            else
                            {
                                _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=Submit&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            }
                        }
                        else
                            labelMessage.Text = _result;
                    }
                    ////////////////////////////////////////////////////////////////////////////////END/////////////////////////////////////////////////////////////////////////////

                }
                else if (_result == "Document fully realised")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Document alredy realised')", true);
                }
            }
        }

    }
    protected void btnSavePrint_Click(object sender, EventArgs e)
    {
        Boolean flag = true;
        string intrate1 = "";
        string intrate2 = txtInterestRate2.Text;
        //string days1=txtNoofDays1.Text;
        //string days2=txtNoofDays2.Text;
        if (labelMessageSB.Text == " ")
        {
            Calcshippingbill();
            Validateshippingbill();

        }
        if (btnTTRefNoList.Checked == true)
        {
            ValidateTTCurr();
            TTFIRCtotalCaluation();
        }
        else
        {
            hdnTTCurrCheck.Value = "";
            hdnTTFIRCTotalAmtCheck.Value = "";
        }

        int intdays = 0;
        TF_DATA objData = new TF_DATA();
        if (chkLoanAdvanced.Checked == true && txtDateDelinked.Text == "")
        {
            if (txtValueDate.Text != "" && txtDueDate.Text != "")
            {
                string query = "getdatediff";
                SqlParameter p1 = new SqlParameter("@dt2", SqlDbType.VarChar);
                p1.Value = txtValueDate.Text;
                SqlParameter p2 = new SqlParameter("@dt1", SqlDbType.VarChar);
                p2.Value = txtDueDate.Text;
                DataTable dt = objData.getData(query, p1, p2);
                if (dt.Rows.Count > 0)
                    intdays = Convert.ToInt32(dt.Rows[0]["Diff"].ToString());
            }
        }
        if (txtDocNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Document number cannot be blank');", true);
            flag = false;
        }
        if (rdbFull.Checked == false && rdbPart.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please check either Full or Part Payment');", true);
            flag = false;
            rdbFull.Focus();
        }
        if (txtValueDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Value Date cannot be blank');", true);
            flag = false;
            txtValueDate.Focus();
        }
        if (txt_relcur.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Realised Currency cannot be blank');", true);
            flag = false;
            txt_relcur.Focus();
        }

        if (hdnTTCurrCheck.Value == "TTfalse")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT cross curr rate.');", true);
            flag = false;
        }
        if (hdnTTFIRCTotalAmtCheck.Value == "Greater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('ITT Amount Total should not be More Than Realized Amount!');", true);
            flag = false;
        }
        if (hdnTTFIRCTotalAmtCheck.Value == "mismatch")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('ITT Amount mismatch with Realized Amount!.');", true);
            flag = false;
        }
        if (chkFirc.Checked == false && txtTTRefNo1.Text == "" && txtTTRefNo2.Text == "" && txtTTRefNo3.Text == "" && txtTTRefNo4.Text == "" && txtTTRefNo5.Text == "" && txtOverseasBank.Text == "" && txtSwiftCode.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Overseas Bank If FIRC or ITT Ref. is Not Present');", true);
            flag = false;
            //btnOverseasBankList.Focus();
        }

        if (chkFirc.Checked == false && txtTTRefNo1.Text == "" && txtTTRefNo2.Text == "" && txtTTRefNo3.Text == "" && txtTTRefNo4.Text == "" && txtTTRefNo5.Text == "" && txtOverseasBank.Text != "" && txtSwiftCode.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Swift Code In Overseas Bank Master!');", true);
            flag = false;
            //btnOverseasBankList.Focus();
        }
        if (hdnShippingbillValidate.Value == "SBIND")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please select Full/Part Indicator in shipping Bill.');", true);
            flag = false;
        }
        if (hdnShippingbillValidate.Value == "True")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please select checkbox in shipping Bill.');", true);
            flag = false;
        }
        if (hdnShippingbillValidate.Value == "SBAmt")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('SB Settlement Amount can not be Blank.');", true);
            flag = false;
        }
        if (hdnShippingbillAmtCheck.Value == "SBGreater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('SB Settlement Amt + FBANK Amt should not be More Than SB Amount!');", true);
            flag = false;
        }
        if (hdnShippingbillAmtCheck.Value == "SGreater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('SB Settlement Amt mismatch with Realized Amount!');", true);
            flag = false;
        }
        if (hdnShippingbillAmtCheck.Value == "FGreater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('SB FBANK Amt mismatch with Fbank charges Amount!');", true);
            flag = false;
        }
        if (flag == true)
        {
            //if (hdnLeiFlag.Value == "Y" && lblLEI_CUST_Remark.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify LEI details.');", true);
            //}
            //else
            //{
            string _userName = Session["userName"].ToString().Trim();
            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string _mode = Request.QueryString["mode"].Trim();
            DocPrFx = Request.QueryString["DocPrFx"].ToString();
            branchcode = Request.QueryString["BranchCode"].ToString();

            SqlParameter p1 = new SqlParameter("@user", _userName);
            SqlParameter p2 = new SqlParameter("@addedtime", _uploadingDate);
            SqlParameter mode = new SqlParameter("@mode", _mode);
            SqlParameter docno = new SqlParameter("@docno", txtDocNo.Text);
            SqlParameter srno = new SqlParameter("@SrNo", txtSrNo.Text);
            SqlParameter doctype = new SqlParameter("@doctype", DocPrFx);
            SqlParameter branch = new SqlParameter("@branch", branchcode);
            string bill = "";
            SqlParameter billtype = new SqlParameter("@billtype", bill);
            SqlParameter custacno = new SqlParameter("@custacno", txtCustAcNo.Text);
            SqlParameter opc = new SqlParameter("@opc", txtOverseasParty.Text);
            SqlParameter RemiName = new SqlParameter("@remitterName", txtRemitterName.Text.Trim());
            SqlParameter RemiCotry = new SqlParameter("@remitterCountry", txtRemitterCountry.Text.Trim());
            SqlParameter RemiAdd = new SqlParameter("@Remitter_Address", txtRemitterAddress.Text.Trim());
            SqlParameter pconsigneeID = new SqlParameter("@consigneePartyID", txtconsigneePartyID.Text);
            SqlParameter obc = new SqlParameter("@obc", txtOverseasBank.Text);
            SqlParameter PSwiftCode = new SqlParameter("@SwiftCode", txtSwiftCode.Text);
            SqlParameter pRB = new SqlParameter("@Remitter_Bank", txtRemitterBank.Text.Trim());
            SqlParameter pRBA = new SqlParameter("@Remitter_Bank_Address", txtRemitterBankAddress.Text.Trim());
            SqlParameter pRBC = new SqlParameter("@Remitter_Bank_Country", txtRemBankCountry.Text.Trim());
            SqlParameter p20 = new SqlParameter("@Purpose_Of_Remittance", txtpurposeofRemittance.Text.Trim());
            SqlParameter pProcessingDate = new SqlParameter("@ProcessingDate", txtProcessingDate.Text);
            SqlParameter realdate = new SqlParameter("@realdate", txtDateRealised.Text);
            SqlParameter valuedate = new SqlParameter("@valuedate", txtValueDate.Text);
            SqlParameter exrate = new SqlParameter("@exrate", txtExchangeRate.Text);
            SqlParameter realamt = new SqlParameter("@realamt", txtAmtRealised.Text);
            SqlParameter realamtinr = new SqlParameter("@realamtinr", txtAmtRealisedinINR.Text);

            //SqlParameter swift = new SqlParameter("@swift", txtSwift.Text);
            //SqlParameter bankcert = new SqlParameter("@bankcert", txtBankCertificate.Text);
            //SqlParameter courier = new SqlParameter("@courier", txtCourier.Text);

            //SqlParameter days = new SqlParameter("@days", intdays);

            //SqlParameter otherbankcharges = new SqlParameter("@otherbankcharges", SqlDbType.VarChar);

            //if (txtOtherBankinINR.Text == "")
            //{
            //    decimal x = 0;
            //    string x1 = Convert.ToString(x);
            //    otherbankcharges.Value = x1;
            //}
            //else
            //    otherbankcharges.Value = txtOtherBank.Text;

            //SqlParameter otherbankchargesinr = new SqlParameter("@otherbankchargesinr", SqlDbType.VarChar);
            //otherbankchargesinr.Value = txtOtherBankinINR.Text;

            //SqlParameter interest1 = new SqlParameter("@interest1", "");
            //SqlParameter days1 = new SqlParameter("@days1", "");
            //SqlParameter interest2 = new SqlParameter("@interest2", txtInterestRate2.Text);
            //SqlParameter days2 = new SqlParameter("@days2", txtNoofDays2.Text);
            //SqlParameter interestamt = new SqlParameter("@interestamt", txtInterest.Text);
            //SqlParameter interestamtinr = new SqlParameter("@interestamtinr", txtInterestinINR.Text);

            SqlParameter remark = new SqlParameter("@remark", txtRemark.Text);
            SqlParameter nyrefno = new SqlParameter("@nyrefno", "");
            string payind = "";
            if (rdbFull.Checked == true)
                payind = "F";
            if (rdbPart.Checked == true)
                payind = "P";
            SqlParameter payind1 = new SqlParameter("@payind", payind);
            SqlParameter colamt = new SqlParameter("@colamt", txtCollectionAmt.Text);
            SqlParameter colamtinr = new SqlParameter("@colamtinr", SqlDbType.VarChar);
            if (txtCollectionAmtinINR.Text == "")
            {
                decimal x = 0;
                string x1 = Convert.ToString(x);
                colamtinr.Value = x1;
            }
            else
                colamtinr.Value = txtCollectionAmtinINR.Text;

            string loan = "";
            if (chkLoanAdvanced.Checked == true)
                loan = "Y";
            else
                loan = "N";
            SqlParameter loan1 = new SqlParameter("@loan", loan);
            string bankline = "";
            if (chkBank.Checked == true)
                bankline = "Y";
            else
                bankline = "N";
            SqlParameter bkline = new SqlParameter("@bankline", bankline);
            SqlParameter CrossrealCur = new SqlParameter("@CrossrealCur", txt_relcur.Text);
            SqlParameter RelCrossCurRate = new SqlParameter("@RelCrossCurRate", txtRelCrossCurRate.Text);
            SqlParameter CrossrealAmt = new SqlParameter("@CrossrealAmt", txt_relamount.Text);
            decimal b = 0;
            decimal r = Convert.ToDecimal(txtAmtRealised.Text);
            decimal bi = Convert.ToDecimal(txtBillAmt.Text);
            b = bi - r;
            string bl = Convert.ToString(b);
            SqlParameter bal = new SqlParameter("@balamtforpaytype", bl);

            string payer_st;
            if (rdbShipper.Checked)
            {
                payer_st = "S";
            }
            else
            {
                payer_st = "B";
            }
            SqlParameter Payer = new SqlParameter("@Payer", payer_st);

            SqlParameter pIntFrom = new SqlParameter("@IntsFrom", txtIntFrmDate1.Text);
            SqlParameter pIntTo = new SqlParameter("@IntTo", txtIntToDate1.Text);
            SqlParameter pIntDays = new SqlParameter("@IntDays", txtForDays1.Text);
            SqlParameter pIntRate = new SqlParameter("@IntRate", txtIntRate1.Text);

            SqlParameter pInterest = new SqlParameter("@Interest", txtInterestAmt.Text);
            SqlParameter FBKCharge = new SqlParameter("@FBKCharge", txt_fbkcharges.Text);
            SqlParameter pcfcamt = new SqlParameter("@pcfcamt", txtPcfcAmt.Text);
            SqlParameter pEEfcAmt = new SqlParameter("@EEfcAmt", txtEEFCAmt.Text);
            SqlParameter overdueamt = new SqlParameter("@overdueamt", txtOverDue.Text);

            SqlParameter POutstandingAmt = new SqlParameter("@OutstandingAmt", txtOutstandingAmt.Text.Trim());
            SqlParameter PInstructedAmt = new SqlParameter("@InstructedAmt", txtInstructedAmt.Text.Trim());
            SqlParameter PLeiExchRate = new SqlParameter("@LeiExchRate", lbl_Exch_rate.Text.Trim());
            SqlParameter pLeiInrAmt = new SqlParameter("@LeiInrAmt", txtLeiInrAmt.Text);

            string CkIRM = "";
            if (chkIRMCreate.Checked == true)
                CkIRM = "Y";
            else
                CkIRM = "N";
            SqlParameter CheckIRM = new SqlParameter("@CheckIRM", CkIRM);

            SqlParameter pIBUTransID = new SqlParameter("@BankUniqueTransactionID", txtBankUniqueTransactionID.Text.Trim());
            SqlParameter pIIFSCCode = new SqlParameter("@IFSCCode", txtIFSCCode.Text.Trim());
            SqlParameter pIRemiADcode = new SqlParameter("@RemittanceADCode", txtRemittanceADCode.Text.Trim());
            SqlParameter pIIECcode = new SqlParameter("@IECCode", txtIECCode.Text.Trim());
            SqlParameter pIPanNo = new SqlParameter("@PanNumber", txtPanNumber.Text.Trim());
            SqlParameter pIModeOfPay = new SqlParameter("@ModeofPayment", ddlModeOfPayment.SelectedValue);
            //SqlParameter pIFactFlag = new SqlParameter("@Factoringflag", ddlFactoringflag.SelectedValue);
            //SqlParameter pIForfFlag = new SqlParameter("@Forfeitingflag", ddlForfeitingflag.SelectedValue);
            SqlParameter pIPurposeCode = new SqlParameter("@purposeCode", txtPurposeCode.Text.Trim());
            SqlParameter pBankReferencenumber = new SqlParameter("@BankReferencenumber", txtBankReferencenumber.Text.Trim());
            SqlParameter pBankAccountNumber = new SqlParameter("@BankAccountNumber", txtBankAccountNumber.Text.Trim());
            SqlParameter pIRMStatus = new SqlParameter("@IRMStatus", SqlDbType.VarChar);
            if (ddlIRMStatus.SelectedValue == "Fresh")
            {
                pIRMStatus.Value = "F";
            }
            //else if (ddlIRMStatus.SelectedValue == "Amended")
            //{
            //    pIRMStatus.Value = "A";
            //}
            else if (ddlIRMStatus.SelectedValue == "Cancelled")
            {
                pIRMStatus.Value = "C";
            }

            string CkTT = "";
            if (btnTTRefNoList.Checked == true)
                CkTT = "Y";
            else
                CkTT = "N";
            SqlParameter CheckTT = new SqlParameter("@CheckTT", CkTT);

            string CheckDummy = "N";
            if (chkDummySettlement.Checked)
                CheckDummy = "Y";
            SqlParameter pCCheckDummy = new SqlParameter("@CheckDummySettlement", CheckDummy);

            SqlParameter TTREFNO1 = new SqlParameter("@TTREFNO1", txtTTRefNo1.Text);
            SqlParameter TTAmt1 = new SqlParameter("@TTAmt1", txtTTAmount1.Text);
            SqlParameter TTREFNO2 = new SqlParameter("@TTREFNO2", txtTTRefNo2.Text);
            SqlParameter TTAmt2 = new SqlParameter("@TTAmt2", txtTTAmount2.Text);
            SqlParameter TTREFNO3 = new SqlParameter("@TTREFNO3", txtTTRefNo3.Text);
            SqlParameter TTAmt3 = new SqlParameter("@TTAmt3", txtTTAmount3.Text);
            SqlParameter TTREFNO4 = new SqlParameter("@TTREFNO4", txtTTRefNo4.Text);
            SqlParameter TTAmt4 = new SqlParameter("@TTAmt4", txtTTAmount4.Text);
            SqlParameter TTREFNO5 = new SqlParameter("@TTREFNO5", txtTTRefNo5.Text);
            SqlParameter TTAmt5 = new SqlParameter("@TTAmt5", txtTTAmount5.Text);
            SqlParameter pTTRef6 = new SqlParameter("@TTRef6", txtTTRefNo6.Text.Trim());
            SqlParameter pTTAmt6 = new SqlParameter("@TTAmt6", txtTTAmount6.Text.Trim());
            SqlParameter pTTRef7 = new SqlParameter("@TTRef7", txtTTRefNo7.Text.Trim());
            SqlParameter pTTAmt7 = new SqlParameter("@TTAmt7", txtTTAmount7.Text.Trim());
            SqlParameter pTTRef8 = new SqlParameter("@TTRef8", txtTTRefNo8.Text.Trim());
            SqlParameter pTTAmt8 = new SqlParameter("@TTAmt8", txtTTAmount8.Text.Trim());
            SqlParameter pTTRef9 = new SqlParameter("@TTRef9", txtTTRefNo9.Text.Trim());
            SqlParameter pTTAmt9 = new SqlParameter("@TTAmt9", txtTTAmount9.Text.Trim());
            SqlParameter pTTRef10 = new SqlParameter("@TTRef10", txtTTRefNo10.Text.Trim());
            SqlParameter pTTAmt10 = new SqlParameter("@TTAmt10", txtTTAmount10.Text.Trim());
            SqlParameter pTTRef11 = new SqlParameter("@TTRef11", txtTTRefNo11.Text.Trim());
            SqlParameter pTTAmt11 = new SqlParameter("@TTAmt11", txtTTAmount11.Text.Trim());
            SqlParameter pTTRef12 = new SqlParameter("@TTRef12", txtTTRefNo12.Text.Trim());
            SqlParameter pTTAmt12 = new SqlParameter("@TTAmt12", txtTTAmount12.Text.Trim());
            SqlParameter pTTRef13 = new SqlParameter("@TTRef13", txtTTRefNo13.Text.Trim());
            SqlParameter pTTAmt13 = new SqlParameter("@TTAmt13", txtTTAmount13.Text.Trim());
            SqlParameter pTTRef14 = new SqlParameter("@TTRef14", txtTTRefNo14.Text.Trim());
            SqlParameter pTTAmt14 = new SqlParameter("@TTAmt14", txtTTAmount14.Text.Trim());
            SqlParameter pTTRef15 = new SqlParameter("@TTRef15", txtTTRefNo15.Text.Trim());
            SqlParameter pTTAmt15 = new SqlParameter("@TTAmt15", txtTTAmount15.Text.Trim());

            SqlParameter PTTCurr1 = new SqlParameter("@TTCurr1", ddlTTCurrency1.SelectedValue);
            SqlParameter PTTCurr2 = new SqlParameter("@TTCurr2", ddlTTCurrency2.SelectedValue);
            SqlParameter PTTCurr3 = new SqlParameter("@TTCurr3", ddlTTCurrency3.SelectedValue);
            SqlParameter PTTCurr4 = new SqlParameter("@TTCurr4", ddlTTCurrency4.SelectedValue);
            SqlParameter PTTCurr5 = new SqlParameter("@TTCurr5", ddlTTCurrency5.SelectedValue);
            SqlParameter PTTCurr6 = new SqlParameter("@TTCurr6", ddlTTCurrency6.SelectedValue);
            SqlParameter PTTCurr7 = new SqlParameter("@TTCurr7", ddlTTCurrency7.SelectedValue);
            SqlParameter PTTCurr8 = new SqlParameter("@TTCurr8", ddlTTCurrency8.SelectedValue);
            SqlParameter PTTCurr9 = new SqlParameter("@TTCurr9", ddlTTCurrency9.SelectedValue);
            SqlParameter PTTCurr10 = new SqlParameter("@TTCurr10", ddlTTCurrency10.SelectedValue);
            SqlParameter PTTCurr11 = new SqlParameter("@TTCurr11", ddlTTCurrency11.SelectedValue);
            SqlParameter PTTCurr12 = new SqlParameter("@TTCurr12", ddlTTCurrency12.SelectedValue);
            SqlParameter PTTCurr13 = new SqlParameter("@TTCurr13", ddlTTCurrency13.SelectedValue);
            SqlParameter PTTCurr14 = new SqlParameter("@TTCurr14", ddlTTCurrency14.SelectedValue);
            SqlParameter PTTCurr15 = new SqlParameter("@TTCurr15", ddlTTCurrency15.SelectedValue);

            SqlParameter PTotTTAmt1 = new SqlParameter("@TotTTAmt1", txtTotTTAmt1.Text.Trim());
            SqlParameter PTotTTAmt2 = new SqlParameter("@TotTTAmt2", txtTotTTAmt2.Text.Trim());
            SqlParameter PTotTTAmt3 = new SqlParameter("@TotTTAmt3", txtTotTTAmt3.Text.Trim());
            SqlParameter PTotTTAmt4 = new SqlParameter("@TotTTAmt4", txtTotTTAmt4.Text.Trim());
            SqlParameter PTotTTAmt5 = new SqlParameter("@TotTTAmt5", txtTotTAmt5.Text.Trim());
            SqlParameter PTotTTAmt6 = new SqlParameter("@TotTTAmt6", txtTotTTAmt6.Text.Trim());
            SqlParameter PTotTTAmt7 = new SqlParameter("@TotTTAmt7", txtTotTTAmt7.Text.Trim());
            SqlParameter PTotTTAmt8 = new SqlParameter("@TotTTAmt8", txtTotTTAmt8.Text.Trim());
            SqlParameter PTotTTAmt9 = new SqlParameter("@TotTTAmt9", txtTtTTAmt9.Text.Trim());
            SqlParameter PTotTTAmt10 = new SqlParameter("@TotTTAmt10", txtTotTTAmt10.Text.Trim());
            SqlParameter PTotTTAmt11 = new SqlParameter("@TotTTAmt11", txtTotTTAmt11.Text.Trim());
            SqlParameter PTotTTAmt12 = new SqlParameter("@TotTTAmt12", txtTotTTAmt12.Text.Trim());
            SqlParameter PTotTTAmt13 = new SqlParameter("@TotTTAmt13", txtTotTTAmt13.Text.Trim());
            SqlParameter PTotTTAmt14 = new SqlParameter("@TotTTAmt14", txtTotTTAmt14.Text.Trim());
            SqlParameter PTotTTAmt15 = new SqlParameter("@TotTTAmt15", txtTotTTAmt15.Text.Trim());

            SqlParameter PBalTTAmt1 = new SqlParameter("@BalTTAmt1", txtBalTTAmt1.Text.Trim());
            SqlParameter PBalTTAmt2 = new SqlParameter("@BalTTAmt2", txtBalTTAmt2.Text.Trim());
            SqlParameter PBalTTAmt3 = new SqlParameter("@BalTTAmt3", txtBalTTAmt3.Text.Trim());
            SqlParameter PBalTTAmt4 = new SqlParameter("@BalTTAmt4", txtBalTTAmt4.Text.Trim());
            SqlParameter PBalTTAmt5 = new SqlParameter("@BalTTAmt5", txtBalTTAmt5.Text.Trim());
            SqlParameter PBalTTAmt6 = new SqlParameter("@BalTTAmt6", txtBalTTAmt6.Text.Trim());
            SqlParameter PBalTTAmt7 = new SqlParameter("@BalTTAmt7", txtBalTTAmt7.Text.Trim());
            SqlParameter PBalTTAmt8 = new SqlParameter("@BalTTAmt8", txtBalTAmt8.Text.Trim());
            SqlParameter PBalTTAmt9 = new SqlParameter("@BalTTAmt9", txtBalTTAmt9.Text.Trim());
            SqlParameter PBalTTAmt10 = new SqlParameter("@BalTTAmt10", txtBalTTAmt10.Text.Trim());
            SqlParameter PBalTTAmt11 = new SqlParameter("@BalTTAmt11", txtBalTTAmt11.Text.Trim());
            SqlParameter PBalTTAmt12 = new SqlParameter("@BalTTAmt12", txtBalTTAmt12.Text.Trim());
            SqlParameter PBalTTAmt13 = new SqlParameter("@BalTTAmt13", txtBalTTAmt13.Text.Trim());
            SqlParameter PBalTTAmt14 = new SqlParameter("@BalTTAmt14", txtBalTTAmt14.Text.Trim());
            SqlParameter PBalTTAmt15 = new SqlParameter("@BalTTAmt15", txtBalTTAmt15.Text.Trim());

            SqlParameter PTTRealisedCurr1 = new SqlParameter("@TTRealisedCurr1", ddlTTRealisedCurr1.SelectedValue);
            SqlParameter PTTRealisedCurr2 = new SqlParameter("@TTRealisedCurr2", ddlTTRealisedCurr2.SelectedValue);
            SqlParameter PTTRealisedCurr3 = new SqlParameter("@TTRealisedCurr3", ddlTTRealisedCurr3.SelectedValue);
            SqlParameter PTTRealisedCurr4 = new SqlParameter("@TTRealisedCurr4", ddlTTRealisedCurr4.SelectedValue);
            SqlParameter PTTRealisedCurr5 = new SqlParameter("@TTRealisedCurr5", ddlTTRealisedCurr5.SelectedValue);
            SqlParameter PTTRealisedCurr6 = new SqlParameter("@TTRealisedCurr6", ddlTTRealisedCurr6.SelectedValue);
            SqlParameter PTTRealisedCurr7 = new SqlParameter("@TTRealisedCurr7", ddlTTRealisedCurr7.SelectedValue);
            SqlParameter PTTRealisedCurr8 = new SqlParameter("@TTRealisedCurr8", ddlTTRealisedCurr8.SelectedValue);
            SqlParameter PTTRealisedCurr9 = new SqlParameter("@TTRealisedCurr9", ddlTTRealisedCurr9.SelectedValue);
            SqlParameter PTTRealisedCurr10 = new SqlParameter("@TTRealisedCurr10", ddlTTRealisedCurr10.SelectedValue);
            SqlParameter PTTRealisedCurr11 = new SqlParameter("@TTRealisedCurr11", ddlTTRealisedCurr11.SelectedValue);
            SqlParameter PTTRealisedCurr12 = new SqlParameter("@TTRealisedCurr12", ddlTTRealisedCurr12.SelectedValue);
            SqlParameter PTTRealisedCurr13 = new SqlParameter("@TTRealisedCurr13", ddlTTRealisedCurr13.SelectedValue);
            SqlParameter PTTRealisedCurr14 = new SqlParameter("@TTRealisedCurr14", ddlTTRealisedCurr14.SelectedValue);
            SqlParameter PTTRealisedCurr15 = new SqlParameter("@TTRealisedCurr15", ddlTTRealisedCurr15.SelectedValue);


            SqlParameter PTTCrossCurrRate1 = new SqlParameter("@TTCrossCurrRate1", txtTTCrossCurrRate1.Text.Trim());
            SqlParameter PTTCrossCurrRate2 = new SqlParameter("@TTCrossCurrRate2", txtTTCrossCurrRate2.Text.Trim());
            SqlParameter PTTCrossCurrRate3 = new SqlParameter("@TTCrossCurrRate3", txtTTCrossCurrRate3.Text.Trim());
            SqlParameter PTTCrossCurrRate4 = new SqlParameter("@TTCrossCurrRate4", txtTTCrossCurrRate4.Text.Trim());
            SqlParameter PTTCrossCurrRate5 = new SqlParameter("@TTCrossCurrRate5", txtTTCrossCurrRate5.Text.Trim());
            SqlParameter PTTCrossCurrRate6 = new SqlParameter("@TTCrossCurrRate6", txtTTCrossCurrRate6.Text.Trim());
            SqlParameter PTTCrossCurrRate7 = new SqlParameter("@TTCrossCurrRate7", txtTTCrossCurrRate7.Text.Trim());
            SqlParameter PTTCrossCurrRate8 = new SqlParameter("@TTCrossCurrRate8", txtTTCrossCurrRate8.Text.Trim());
            SqlParameter PTTCrossCurrRate9 = new SqlParameter("@TTCrossCurrRate9", txtTTCrossCurrRate9.Text.Trim());
            SqlParameter PTTCrossCurrRate10 = new SqlParameter("@TTCrossCurrRate10", txtTTCrossCurrRate10.Text.Trim());
            SqlParameter PTTCrossCurrRate11 = new SqlParameter("@TTCrossCurrRate11", txtTTCrossCurrRate11.Text.Trim());
            SqlParameter PTTCrossCurrRate12 = new SqlParameter("@TTCrossCurrRate12", txtTTCrossCurrRate12.Text.Trim());
            SqlParameter PTTCrossCurrRate13 = new SqlParameter("@TTCrossCurrRate13", txtTTCrossCurrRate13.Text.Trim());
            SqlParameter PTTCrossCurrRate14 = new SqlParameter("@TTCrossCurrRate14", txtTTCrossCurrRate14.Text.Trim());
            SqlParameter PTTCrossCurrRate15 = new SqlParameter("@TTCrossCurrRate15", txtTTCrossCurrRate15.Text.Trim());

            SqlParameter PTTAmtRealised1 = new SqlParameter("@TTAmtRealised1", txtTTAmtRealised1.Text.Trim());
            SqlParameter PTTAmtRealised2 = new SqlParameter("@TTAmtRealised2", txtTTAmtRealised2.Text.Trim());
            SqlParameter PTTAmtRealised3 = new SqlParameter("@TTAmtRealised3", txtTTAmtRealised3.Text.Trim());
            SqlParameter PTTAmtRealised4 = new SqlParameter("@TTAmtRealised4", txtTTAmtRealised4.Text.Trim());
            SqlParameter PTTAmtRealised5 = new SqlParameter("@TTAmtRealised5", txtTTAmtRealised5.Text.Trim());
            SqlParameter PTTAmtRealised6 = new SqlParameter("@TTAmtRealised6", txtTTAmtRealised6.Text.Trim());
            SqlParameter PTTAmtRealised7 = new SqlParameter("@TTAmtRealised7", txtTTAmtRealised7.Text.Trim());
            SqlParameter PTTAmtRealised8 = new SqlParameter("@TTAmtRealised8", txtTTAmtRealised8.Text.Trim());
            SqlParameter PTTAmtRealised9 = new SqlParameter("@TTAmtRealised9", txtTTAmtRealised9.Text.Trim());
            SqlParameter PTTAmtRealised10 = new SqlParameter("@TTAmtRealised10", txtTTAmtRealised10.Text.Trim());
            SqlParameter PTTAmtRealised11 = new SqlParameter("@TTAmtRealised11", txtTTAmtRealised11.Text.Trim());
            SqlParameter PTTAmtRealised12 = new SqlParameter("@TTAmtRealised12", txtTTAmtRealised12.Text.Trim());
            SqlParameter PTTAmtRealised13 = new SqlParameter("@TTAmtRealised13", txtTTAmtRealised13.Text.Trim());
            SqlParameter PTTAmtRealised14 = new SqlParameter("@TTAmtRealised14", txtTTAmtRealised14.Text.Trim());
            SqlParameter PTTAmtRealised15 = new SqlParameter("@TTAmtRealised15", txtTTAmtRealised15.Text.Trim());

            SqlParameter FIRC_Status = new SqlParameter("@FIRC_Status", SqlDbType.VarChar);
            if (chkFirc.Checked == true)
            { FIRC_Status.Value = "Y"; }
            if (chkFirc.Checked == false)
            { FIRC_Status.Value = "N"; }
            SqlParameter FIRC_NO = new SqlParameter("@FIRC_NO", txtFircNo.Text);
            SqlParameter FIRC_AD_CODE = new SqlParameter("@FIRC_AD_CODE", txtFircAdCode.Text);
            SqlParameter checkerstatus = new SqlParameter("@checkerstatus", "M");

            string PendingSB = "N";
            if (chkSB.Checked)
                PendingSB = "Y";
            SqlParameter pCheckPendingSB = new SqlParameter("@PendingSBFlag", PendingSB);

            //-----------------------------------------------------Anand26/06/2023---------------------------------
            /////IMPORT ACCOUNTING 1
            //---------------Anand 11-08-2023----------------------------
            string IMPACC1Flag = "";
            if (chk_IMPACC1Flag.Checked == true)
                IMPACC1Flag = "Y";
            else
                IMPACC1Flag = "N";
            //----------------END---------------------------------------
            SqlParameter P_chk_IMPACC1Flag = new SqlParameter("@IMP_ACC1_Flag", IMPACC1Flag);// Added by Anand 11-08-2023
            SqlParameter P_txt_IMPACC1_FCRefNo = new SqlParameter("@IMP_ACC1_FCRefNo", txt_IMPACC1_FCRefNo.Text.Trim());
            SqlParameter P_txt_IMPACC1_DiscAmt = new SqlParameter("@IMP_ACC1_Amount", txt_IMPACC1_DiscAmt.Text.Trim());
            SqlParameter P_txt_IMPACC1_DiscExchRate = new SqlParameter("@IMP_ACC1_ExchRate", "");//_txt_IMPACC1_DiscExchRate.ToUpper());
            SqlParameter P_txt_IMPACC1_Princ_matu = new SqlParameter("@IMP_ACC1_Principal_MATU", txt_IMPACC1_Princ_matu.Text.Trim());
            SqlParameter P_txt_IMPACC1_Princ_lump = new SqlParameter("@IMP_ACC1_Principal_LUMP", txt_IMPACC1_Princ_lump.Text.Trim());
            SqlParameter P_txt_IMPACC1_Princ_Contract_no = new SqlParameter("@IMP_ACC1_Principal_Contract_No", txt_IMPACC1_Princ_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC1_Princ_Ex_Curr = new SqlParameter("@IMP_ACC1_Principal_Ex_Curr", txt_IMPACC1_Princ_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_Princ_Ex_rate = new SqlParameter("@IMP_ACC1_Principal_Exch_Rate", txt_IMPACC1_Princ_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Principal_Intnl_Exch_Rate", txt_IMPACC1_Princ_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_Interest_matu = new SqlParameter("@IMP_ACC1_Interest_MATU", txt_IMPACC1_Interest_matu.Text.Trim());
            SqlParameter P_txt_IMPACC1_Interest_lump = new SqlParameter("@IMP_ACC1_Interest_LUMP", txt_IMPACC1_Interest_lump.Text.Trim());
            SqlParameter P_txt_IMPACC1_Interest_Contract_no = new SqlParameter("@IMP_ACC1_Interest_Contract_No", txt_IMPACC1_Interest_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC1_Interest_Ex_Curr = new SqlParameter("@IMP_ACC1_Interest_Ex_Curr", txt_IMPACC1_Interest_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_Interest_Ex_rate = new SqlParameter("@IMP_ACC1_Interest_Exch_Rate", txt_IMPACC1_Interest_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Interest_Intnl_Exch_Rate", txt_IMPACC1_Interest_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_Commission_matu = new SqlParameter("@IMP_ACC1_Commission_MATU", txt_IMPACC1_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC1_Commission_lump = new SqlParameter("@IMP_ACC1_Commission_LUMP", txt_IMPACC1_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC1_Commission_Contract_no = new SqlParameter("@IMP_ACC1_Commission_Contract_No", txt_IMPACC1_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC1_Commission_Ex_Curr = new SqlParameter("@IMP_ACC1_Commission_Ex_Curr", txt_IMPACC1_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_Commission_Ex_rate = new SqlParameter("@IMP_ACC1_Commission_Exch_Rate", txt_IMPACC1_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Commission_Intnl_Exch_Rate", txt_IMPACC1_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_Their_Commission_matu = new SqlParameter("@IMP_ACC1_Their_Commission_MATU", txt_IMPACC1_Their_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC1_Their_Commission_lump = new SqlParameter("@IMP_ACC1_Their_Commission_LUMP", txt_IMPACC1_Their_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC1_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC1_Their_Commission_Contract_No", txt_IMPACC1_Their_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC1_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC1_Their_Commission_Ex_Curr", txt_IMPACC1_Their_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC1_Their_Commission_Exch_Rate", txt_IMPACC1_Their_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC1_Their_Commission_Intnl_Exch_Rate", txt_IMPACC1_Their_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Code = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Code", txt_IMPACC1_CR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_AC_Short_Name = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Short_Name", txt_IMPACC1_CR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Cust_abbr = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC1_CR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Cust_Acc = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC1_CR_Cust_Acc.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Curr", txt_IMPACC1_CR_Acceptance_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Acceptance_amt = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Amt", txt_IMPACC1_CR_Acceptance_amt.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Acceptance_payer = new SqlParameter("@IMP_ACC1_CR_Sundry_Deposit_Payer", txt_IMPACC1_CR_Acceptance_payer.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Interest_Curr = new SqlParameter("@IMP_ACC1_CR_Interest_Curr", txt_IMPACC1_CR_Interest_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Interest_amt = new SqlParameter("@IMP_ACC1_CR_Interest_Amount", txt_IMPACC1_CR_Interest_amt.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Interest_payer = new SqlParameter("@IMP_ACC1_CR_Interest_Payer", txt_IMPACC1_CR_Interest_payer.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Curr", txt_IMPACC1_CR_Accept_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Amount", txt_IMPACC1_CR_Accept_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Acceptance_Comm_Payer", txt_IMPACC1_CR_Accept_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Curr", txt_IMPACC1_CR_Pay_Handle_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Amount", txt_IMPACC1_CR_Pay_Handle_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Pay_Handle_Comm_Payer", txt_IMPACC1_CR_Pay_Handle_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Others_Curr = new SqlParameter("@IMP_ACC1_CR_Others_Curr", txt_IMPACC1_CR_Others_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Others_amt = new SqlParameter("@IMP_ACC1_CR_Others_Amount", txt_IMPACC1_CR_Others_amt.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Others_Payer = new SqlParameter("@IMP_ACC1_CR_Others_Payer", txt_IMPACC1_CR_Others_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Curr", txt_IMPACC1_CR_Their_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Amount", txt_IMPACC1_CR_Their_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC1_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC1_CR_Their_Comm_Payer", txt_IMPACC1_CR_Their_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Code = new SqlParameter("@IMP_ACC1_DR_Code", txt_IMPACC1_DR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_AC_Short_Name = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name", txt_IMPACC1_DR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_abbr = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr", txt_IMPACC1_DR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_Acc = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No", txt_IMPACC1_DR_Cust_Acc.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr", txt_IMPACC1_DR_Cur_Acc_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount", txt_IMPACC1_DR_Cur_Acc_amt.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer", txt_IMPACC1_DR_Cur_Acc_payer.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr2", txt_IMPACC1_DR_Cur_Acc_Curr2.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount2", txt_IMPACC1_DR_Cur_Acc_amt2.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer2", txt_IMPACC1_DR_Cur_Acc_payer2.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr3", txt_IMPACC1_DR_Cur_Acc_Curr3.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount3", txt_IMPACC1_DR_Cur_Acc_amt3.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer3", txt_IMPACC1_DR_Cur_Acc_payer3.Text.Trim());

            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr4", txt_IMPACC1_DR_Cur_Acc_Curr4.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount4", txt_IMPACC1_DR_Cur_Acc_amt4.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer4", txt_IMPACC1_DR_Cur_Acc_payer4.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr5", txt_IMPACC1_DR_Cur_Acc_Curr5.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount5", txt_IMPACC1_DR_Cur_Acc_amt5.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer5", txt_IMPACC1_DR_Cur_Acc_payer5.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Curr6", txt_IMPACC1_DR_Cur_Acc_Curr6.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Amount6", txt_IMPACC1_DR_Cur_Acc_amt6.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC1_DR_Current_Acc_Payer6", txt_IMPACC1_DR_Cur_Acc_payer6.Text.Trim());

            SqlParameter P_txt_IMPACC1_DR_Code2 = new SqlParameter("@IMP_ACC1_DR_Code2", txt_IMPACC1_DR_Code2.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name2", txt_IMPACC1_DR_AC_Short_Name2.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr2", txt_IMPACC1_DR_Cust_abbr2.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No2", txt_IMPACC1_DR_Cust_Acc2.Text.Trim());

            SqlParameter P_txt_IMPACC1_DR_Code3 = new SqlParameter("@IMP_ACC1_DR_Code3", txt_IMPACC1_DR_Code3.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name3", txt_IMPACC1_DR_AC_Short_Name3.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr3", txt_IMPACC1_DR_Cust_abbr3.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No3", txt_IMPACC1_DR_Cust_Acc3.Text.Trim());

            SqlParameter P_txt_IMPACC1_DR_Code4 = new SqlParameter("@IMP_ACC1_DR_Code4", txt_IMPACC1_DR_Code4.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name4", txt_IMPACC1_DR_AC_Short_Name4.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr4", txt_IMPACC1_DR_Cust_abbr4.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No4", txt_IMPACC1_DR_Cust_Acc4.Text.Trim());

            SqlParameter P_txt_IMPACC1_DR_Code5 = new SqlParameter("@IMP_ACC1_DR_Code5", txt_IMPACC1_DR_Code5.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name5", txt_IMPACC1_DR_AC_Short_Name5.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr5", txt_IMPACC1_DR_Cust_abbr5.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No5", txt_IMPACC1_DR_Cust_Acc5.Text.Trim());

            SqlParameter P_txt_IMPACC1_DR_Code6 = new SqlParameter("@IMP_ACC1_DR_Code6", txt_IMPACC1_DR_Code6.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC1_DR_Acc_Short_Name6", txt_IMPACC1_DR_AC_Short_Name6.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC1_DR_Cust_Abbr6", txt_IMPACC1_DR_Cust_abbr6.Text.Trim());
            SqlParameter P_txt_IMPACC1_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC1_DR_Cust_Acc_No6", txt_IMPACC1_DR_Cust_Acc6.Text.Trim());


            ////IMPORT ACCOUNTING 2
            //---------------Anand 11-08-2023----------------------------
            string IMPACC2Flag = "";
            if (chk_IMPACC2Flag.Checked == true)
                IMPACC2Flag = "Y";
            else
                IMPACC2Flag = "N";
            //----------------END---------------------------------------
            SqlParameter P_chk_IMPACC2Flag = new SqlParameter("@IMP_ACC2_Flag", IMPACC2Flag);//_chk_IMPACC2Flag.ToUpper());
            SqlParameter P_txt_IMPACC2_FCRefNo = new SqlParameter("@IMP_ACC2_FCRefNo", txt_IMPACC2_FCRefNo.Text.Trim());
            SqlParameter P_txt_IMPACC2_DiscAmt = new SqlParameter("@IMP_ACC2_Amount", txt_IMPACC2_DiscAmt.Text.Trim());
            SqlParameter P_txt_IMPACC2_DiscExchRate = new SqlParameter("@IMP_ACC2_ExchRate", "");//_txt_IMPACC2_DiscExchRate.Text.Trim());
            SqlParameter P_txt_IMPACC2_Princ_matu = new SqlParameter("@IMP_ACC2_Principal_MATU", txt_IMPACC2_Princ_matu.Text.Trim());
            SqlParameter P_txt_IMPACC2_Princ_lump = new SqlParameter("@IMP_ACC2_Principal_LUMP", txt_IMPACC2_Princ_lump.Text.Trim());
            SqlParameter P_txt_IMPACC2_Princ_Contract_no = new SqlParameter("@IMP_ACC2_Principal_Contract_No", txt_IMPACC2_Princ_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC2_Princ_Ex_Curr = new SqlParameter("@IMP_ACC2_Principal_Ex_Curr", txt_IMPACC2_Princ_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_Princ_Ex_rate = new SqlParameter("@IMP_ACC2_Principal_Exch_Rate", txt_IMPACC2_Princ_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC2_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Principal_Intnl_Exch_Rate", txt_IMPACC2_Princ_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC2_Interest_matu = new SqlParameter("@IMP_ACC2_Interest_MATU", txt_IMPACC2_Interest_matu.Text.Trim());
            SqlParameter P_txt_IMPACC2_Interest_lump = new SqlParameter("@IMP_ACC2_Interest_LUMP", txt_IMPACC2_Interest_lump.Text.Trim());
            SqlParameter P_txt_IMPACC2_Interest_Contract_no = new SqlParameter("@IMP_ACC2_Interest_Contract_No", txt_IMPACC2_Interest_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC2_Interest_Ex_Curr = new SqlParameter("@IMP_ACC2_Interest_Ex_Curr", txt_IMPACC2_Interest_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_Interest_Ex_rate = new SqlParameter("@IMP_ACC2_Interest_Exch_Rate", txt_IMPACC2_Interest_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC2_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Interest_Intnl_Exch_Rate", txt_IMPACC2_Interest_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC2_Commission_matu = new SqlParameter("@IMP_ACC2_Commission_MATU", txt_IMPACC2_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC2_Commission_lump = new SqlParameter("@IMP_ACC2_Commission_LUMP", txt_IMPACC2_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC2_Commission_Contract_no = new SqlParameter("@IMP_ACC2_Commission_Contract_No", txt_IMPACC2_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC2_Commission_Ex_Curr = new SqlParameter("@IMP_ACC2_Commission_Ex_Curr", txt_IMPACC2_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_Commission_Ex_rate = new SqlParameter("@IMP_ACC2_Commission_Exch_Rate", txt_IMPACC2_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC2_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Commission_Intnl_Exch_Rate", txt_IMPACC2_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC2_Their_Commission_matu = new SqlParameter("@IMP_ACC2_Their_Commission_MATU", txt_IMPACC2_Their_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC2_Their_Commission_lump = new SqlParameter("@IMP_ACC2_Their_Commission_LUMP", txt_IMPACC2_Their_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC2_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC2_Their_Commission_Contract_No", txt_IMPACC2_Their_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC2_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC2_Their_Commission_Ex_Curr", txt_IMPACC2_Their_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC2_Their_Commission_Exch_Rate", txt_IMPACC2_Their_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC2_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC2_Their_Commission_Intnl_Exch_Rate", txt_IMPACC2_Their_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Code = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Code", txt_IMPACC2_CR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_AC_Short_Name = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Short_Name", txt_IMPACC2_CR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Cust_abbr = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC2_CR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Cust_Acc = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC2_CR_Cust_Acc.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Curr", txt_IMPACC2_CR_Acceptance_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Acceptance_amt = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Amt", txt_IMPACC2_CR_Acceptance_amt.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Acceptance_payer = new SqlParameter("@IMP_ACC2_CR_Sundry_Deposit_Payer", txt_IMPACC2_CR_Acceptance_payer.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Interest_Curr = new SqlParameter("@IMP_ACC2_CR_Interest_Curr", txt_IMPACC2_CR_Interest_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Interest_amt = new SqlParameter("@IMP_ACC2_CR_Interest_Amount", txt_IMPACC2_CR_Interest_amt.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Interest_payer = new SqlParameter("@IMP_ACC2_CR_Interest_Payer", txt_IMPACC2_CR_Interest_payer.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Curr", txt_IMPACC2_CR_Accept_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Amount", txt_IMPACC2_CR_Accept_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Acceptance_Comm_Payer", txt_IMPACC2_CR_Accept_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Curr", txt_IMPACC2_CR_Pay_Handle_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Amount", txt_IMPACC2_CR_Pay_Handle_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Pay_Handle_Comm_Payer", txt_IMPACC2_CR_Pay_Handle_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Others_Curr = new SqlParameter("@IMP_ACC2_CR_Others_Curr", txt_IMPACC2_CR_Others_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Others_amt = new SqlParameter("@IMP_ACC2_CR_Others_Amount", txt_IMPACC2_CR_Others_amt.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Others_Payer = new SqlParameter("@IMP_ACC2_CR_Others_Payer", txt_IMPACC2_CR_Others_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Curr", txt_IMPACC2_CR_Their_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Amount", txt_IMPACC2_CR_Their_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC2_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC2_CR_Their_Comm_Payer", txt_IMPACC2_CR_Their_Commission_Payer.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Code = new SqlParameter("@IMP_ACC2_DR_Code", txt_IMPACC2_DR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_AC_Short_Name = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name", txt_IMPACC2_DR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_abbr = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr", txt_IMPACC2_DR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_Acc = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No", txt_IMPACC2_DR_Cust_Acc.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr", txt_IMPACC2_DR_Cur_Acc_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount", txt_IMPACC2_DR_Cur_Acc_amt.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer", txt_IMPACC2_DR_Cur_Acc_payer.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr2", txt_IMPACC2_DR_Cur_Acc_Curr2.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount2", txt_IMPACC2_DR_Cur_Acc_amt2.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer2", txt_IMPACC2_DR_Cur_Acc_payer2.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr3", txt_IMPACC2_DR_Cur_Acc_Curr3.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount3", txt_IMPACC2_DR_Cur_Acc_amt3.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer3", txt_IMPACC2_DR_Cur_Acc_payer3.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr4", txt_IMPACC2_DR_Cur_Acc_Curr4.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount4", txt_IMPACC2_DR_Cur_Acc_amt4.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer4", txt_IMPACC2_DR_Cur_Acc_payer4.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr5", txt_IMPACC2_DR_Cur_Acc_Curr5.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount5", txt_IMPACC2_DR_Cur_Acc_amt5.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer5", txt_IMPACC2_DR_Cur_Acc_payer5.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Curr6", txt_IMPACC2_DR_Cur_Acc_Curr6.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Amount6", txt_IMPACC2_DR_Cur_Acc_amt6.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC2_DR_Current_Acc_Payer6", txt_IMPACC2_DR_Cur_Acc_payer6.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Code2 = new SqlParameter("@IMP_ACC2_DR_Code2", txt_IMPACC2_DR_Code2.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name2", txt_IMPACC2_DR_AC_Short_Name2.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr2", txt_IMPACC2_DR_Cust_abbr2.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No2", txt_IMPACC2_DR_Cust_Acc2.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Code3 = new SqlParameter("@IMP_ACC2_DR_Code3", txt_IMPACC2_DR_Code3.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name3", txt_IMPACC2_DR_AC_Short_Name3.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr3", txt_IMPACC2_DR_Cust_abbr3.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No3", txt_IMPACC2_DR_Cust_Acc3.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Code4 = new SqlParameter("@IMP_ACC2_DR_Code4", txt_IMPACC2_DR_Code4.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name4", txt_IMPACC2_DR_AC_Short_Name4.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr4", txt_IMPACC2_DR_Cust_abbr4.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No4", txt_IMPACC2_DR_Cust_Acc4.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Code5 = new SqlParameter("@IMP_ACC2_DR_Code5", txt_IMPACC2_DR_Code5.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name5", txt_IMPACC2_DR_AC_Short_Name5.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr5", txt_IMPACC2_DR_Cust_abbr5.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No5", txt_IMPACC2_DR_Cust_Acc5.Text.Trim());

            SqlParameter P_txt_IMPACC2_DR_Code6 = new SqlParameter("@IMP_ACC2_DR_Code6", txt_IMPACC2_DR_Code6.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC2_DR_Acc_Short_Name6", txt_IMPACC2_DR_AC_Short_Name6.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC2_DR_Cust_Abbr6", txt_IMPACC2_DR_Cust_abbr6.Text.Trim());
            SqlParameter P_txt_IMPACC2_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC2_DR_Cust_Acc_No6", txt_IMPACC2_DR_Cust_Acc6.Text.Trim());

            //////IMPORT ACCOUNTING  3
            //---------------Anand 11-08-2023----------------------------
            string IMPACC3Flag = "";
            if (chk_IMPACC3Flag.Checked == true)
                IMPACC3Flag = "Y";
            else
                IMPACC3Flag = "N";
            //----------------END---------------------------------------
            SqlParameter P_chk_IMPACC3Flag = new SqlParameter("@IMP_ACC3_Flag", IMPACC3Flag);//_chk_IMPACC3Flag.Text.Trim());
            SqlParameter P_txt_IMPACC3_FCRefNo = new SqlParameter("@IMP_ACC3_FCRefNo", txt_IMPACC3_FCRefNo.Text.Trim());
            SqlParameter P_txt_IMPACC3_DiscAmt = new SqlParameter("@IMP_ACC3_Amount", txt_IMPACC3_DiscAmt.Text.Trim());
            SqlParameter P_txt_IMPACC3_DiscExchRate = new SqlParameter("@IMP_ACC3_ExchRate", "");//txt_IMPACC3_DiscExchRate.Text.Trim());
            SqlParameter P_txt_IMPACC3_Princ_matu = new SqlParameter("@IMP_ACC3_Principal_MATU", txt_IMPACC3_Princ_matu.Text.Trim());
            SqlParameter P_txt_IMPACC3_Princ_lump = new SqlParameter("@IMP_ACC3_Principal_LUMP", txt_IMPACC3_Princ_lump.Text.Trim());
            SqlParameter P_txt_IMPACC3_Princ_Contract_no = new SqlParameter("@IMP_ACC3_Principal_Contract_No", txt_IMPACC3_Princ_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC3_Princ_Ex_Curr = new SqlParameter("@IMP_ACC3_Principal_Ex_Curr", txt_IMPACC3_Princ_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_Princ_Ex_rate = new SqlParameter("@IMP_ACC3_Principal_Exch_Rate", txt_IMPACC3_Princ_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC3_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Principal_Intnl_Exch_Rate", txt_IMPACC3_Princ_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC3_Interest_matu = new SqlParameter("@IMP_ACC3_Interest_MATU", txt_IMPACC3_Interest_matu.Text.Trim());
            SqlParameter P_txt_IMPACC3_Interest_lump = new SqlParameter("@IMP_ACC3_Interest_LUMP", txt_IMPACC3_Interest_lump.Text.Trim());
            SqlParameter P_txt_IMPACC3_Interest_Contract_no = new SqlParameter("@IMP_ACC3_Interest_Contract_No", txt_IMPACC3_Interest_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC3_Interest_Ex_Curr = new SqlParameter("@IMP_ACC3_Interest_Ex_Curr", txt_IMPACC3_Interest_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_Interest_Ex_rate = new SqlParameter("@IMP_ACC3_Interest_Exch_Rate", txt_IMPACC3_Interest_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC3_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Interest_Intnl_Exch_Rate", txt_IMPACC3_Interest_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC3_Commission_matu = new SqlParameter("@IMP_ACC3_Commission_MATU", txt_IMPACC3_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC3_Commission_lump = new SqlParameter("@IMP_ACC3_Commission_LUMP", txt_IMPACC3_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC3_Commission_Contract_no = new SqlParameter("@IMP_ACC3_Commission_Contract_No", txt_IMPACC3_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC3_Commission_Ex_Curr = new SqlParameter("@IMP_ACC3_Commission_Ex_Curr", txt_IMPACC3_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_Commission_Ex_rate = new SqlParameter("@IMP_ACC3_Commission_Exch_Rate", txt_IMPACC3_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC3_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Commission_Intnl_Exch_Rate", txt_IMPACC3_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC3_Their_Commission_matu = new SqlParameter("@IMP_ACC3_Their_Commission_MATU", txt_IMPACC3_Their_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC3_Their_Commission_lump = new SqlParameter("@IMP_ACC3_Their_Commission_LUMP", txt_IMPACC3_Their_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC3_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC3_Their_Commission_Contract_No", txt_IMPACC3_Their_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC3_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC3_Their_Commission_Ex_Curr", txt_IMPACC3_Their_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC3_Their_Commission_Exch_Rate", txt_IMPACC3_Their_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC3_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC3_Their_Commission_Intnl_Exch_Rate", txt_IMPACC3_Their_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Code = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Code", txt_IMPACC3_CR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_AC_Short_Name = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Short_Name", txt_IMPACC3_CR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Cust_abbr = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC3_CR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Cust_Acc = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC3_CR_Cust_Acc.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Curr", txt_IMPACC3_CR_Acceptance_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Acceptance_amt = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Amt", txt_IMPACC3_CR_Acceptance_amt.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Acceptance_payer = new SqlParameter("@IMP_ACC3_CR_Sundry_Deposit_Payer", txt_IMPACC3_CR_Acceptance_payer.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Interest_Curr = new SqlParameter("@IMP_ACC3_CR_Interest_Curr", txt_IMPACC3_CR_Interest_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Interest_amt = new SqlParameter("@IMP_ACC3_CR_Interest_Amount", txt_IMPACC3_CR_Interest_amt.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Interest_payer = new SqlParameter("@IMP_ACC3_CR_Interest_Payer", txt_IMPACC3_CR_Interest_payer.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Curr", txt_IMPACC3_CR_Accept_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Amount", txt_IMPACC3_CR_Accept_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Acceptance_Comm_Payer", txt_IMPACC3_CR_Accept_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Curr", txt_IMPACC3_CR_Pay_Handle_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Amount", txt_IMPACC3_CR_Pay_Handle_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Pay_Handle_Comm_Payer", txt_IMPACC3_CR_Pay_Handle_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Others_Curr = new SqlParameter("@IMP_ACC3_CR_Others_Curr", txt_IMPACC3_CR_Others_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Others_amt = new SqlParameter("@IMP_ACC3_CR_Others_Amount", txt_IMPACC3_CR_Others_amt.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Others_Payer = new SqlParameter("@IMP_ACC3_CR_Others_Payer", txt_IMPACC3_CR_Others_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Curr", txt_IMPACC3_CR_Their_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Amount", txt_IMPACC3_CR_Their_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC3_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC3_CR_Their_Comm_Payer", txt_IMPACC3_CR_Their_Commission_Payer.Text.Trim());

            SqlParameter P_txt_IMPACC3_DR_Code = new SqlParameter("@IMP_ACC3_DR_Code", txt_IMPACC3_DR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_AC_Short_Name = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name", txt_IMPACC3_DR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_abbr = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr", txt_IMPACC3_DR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_Acc = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No", txt_IMPACC3_DR_Cust_Acc.Text.Trim());

            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr", txt_IMPACC3_DR_Cur_Acc_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount", txt_IMPACC3_DR_Cur_Acc_amt.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer", txt_IMPACC3_DR_Cur_Acc_payer.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr2", txt_IMPACC3_DR_Cur_Acc_Curr2.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount2", txt_IMPACC3_DR_Cur_Acc_amt2.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer2", txt_IMPACC3_DR_Cur_Acc_payer2.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr3", txt_IMPACC3_DR_Cur_Acc_Curr3.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount3", txt_IMPACC3_DR_Cur_Acc_amt3.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer3", txt_IMPACC3_DR_Cur_Acc_payer3.Text.Trim());

            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr4", txt_IMPACC3_DR_Cur_Acc_Curr4.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount4", txt_IMPACC3_DR_Cur_Acc_amt4.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer4", txt_IMPACC3_DR_Cur_Acc_payer4.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr5", txt_IMPACC3_DR_Cur_Acc_Curr5.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount5", txt_IMPACC3_DR_Cur_Acc_amt5.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer5", txt_IMPACC3_DR_Cur_Acc_payer5.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Curr6", txt_IMPACC3_DR_Cur_Acc_Curr6.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Amount6", txt_IMPACC3_DR_Cur_Acc_amt6.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC3_DR_Current_Acc_Payer6", txt_IMPACC3_DR_Cur_Acc_payer6.Text.Trim());


            SqlParameter P_txt_IMPACC3_DR_Code2 = new SqlParameter("@IMP_ACC3_DR_Code2", txt_IMPACC3_DR_Code2.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name2", txt_IMPACC3_DR_AC_Short_Name2.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr2", txt_IMPACC3_DR_Cust_abbr2.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No2", txt_IMPACC3_DR_Cust_Acc2.Text.Trim());

            SqlParameter P_txt_IMPACC3_DR_Code3 = new SqlParameter("@IMP_ACC3_DR_Code3", txt_IMPACC3_DR_Code3.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name3", txt_IMPACC3_DR_AC_Short_Name3.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr3", txt_IMPACC3_DR_Cust_abbr3.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No3", txt_IMPACC3_DR_Cust_Acc3.Text.Trim());

            SqlParameter P_txt_IMPACC3_DR_Code4 = new SqlParameter("@IMP_ACC3_DR_Code4", txt_IMPACC3_DR_Code4.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name4", txt_IMPACC3_DR_AC_Short_Name4.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr4", txt_IMPACC3_DR_Cust_abbr4.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No4", txt_IMPACC3_DR_Cust_Acc4.Text.Trim());

            SqlParameter P_txt_IMPACC3_DR_Code5 = new SqlParameter("@IMP_ACC3_DR_Code5", txt_IMPACC3_DR_Code5.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name5", txt_IMPACC3_DR_AC_Short_Name5.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr5", txt_IMPACC3_DR_Cust_abbr5.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No5", txt_IMPACC3_DR_Cust_Acc5.Text.Trim());

            SqlParameter P_txt_IMPACC3_DR_Code6 = new SqlParameter("@IMP_ACC3_DR_Code6", txt_IMPACC3_DR_Code6.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC3_DR_Acc_Short_Name6", txt_IMPACC3_DR_AC_Short_Name6.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC3_DR_Cust_Abbr6", txt_IMPACC3_DR_Cust_abbr6.Text.Trim());
            SqlParameter P_txt_IMPACC3_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC3_DR_Cust_Acc_No6", txt_IMPACC3_DR_Cust_Acc6.Text.Trim());

            //////IMPORT ACCOUNTING 4
            //---------------Anand 11-08-2023----------------------------
            string IMPACC4Flag = "";
            if (chk_IMPACC4Flag.Checked == true)
                IMPACC4Flag = "Y";
            else
                IMPACC4Flag = "N";
            //----------------END---------------------------------------
            SqlParameter P_chk_IMPACC4Flag = new SqlParameter("@IMP_ACC4_Flag", IMPACC4Flag);//_chk_IMPACC4Flag.Text.Trim());
            SqlParameter P_txt_IMPACC4_FCRefNo = new SqlParameter("@IMP_ACC4_FCRefNo", txt_IMPACC4_FCRefNo.Text.Trim());
            SqlParameter P_txt_IMPACC4_DiscAmt = new SqlParameter("@IMP_ACC4_Amount", txt_IMPACC4_DiscAmt.Text.Trim());
            SqlParameter P_txt_IMPACC4_DiscExchRate = new SqlParameter("@IMP_ACC4_ExchRate", "");//_txt_IMPACC4_DiscExchRate.Text.Trim());
            SqlParameter P_txt_IMPACC4_Princ_matu = new SqlParameter("@IMP_ACC4_Principal_MATU", txt_IMPACC4_Princ_matu.Text.Trim());
            SqlParameter P_txt_IMPACC4_Princ_lump = new SqlParameter("@IMP_ACC4_Principal_LUMP", txt_IMPACC4_Princ_lump.Text.Trim());
            SqlParameter P_txt_IMPACC4_Princ_Contract_no = new SqlParameter("@IMP_ACC4_Principal_Contract_No", txt_IMPACC4_Princ_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC4_Princ_Ex_Curr = new SqlParameter("@IMP_ACC4_Principal_Ex_Curr", txt_IMPACC4_Princ_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_Princ_Ex_rate = new SqlParameter("@IMP_ACC4_Principal_Exch_Rate", txt_IMPACC4_Princ_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC4_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Principal_Intnl_Exch_Rate", txt_IMPACC4_Princ_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC4_Interest_matu = new SqlParameter("@IMP_ACC4_Interest_MATU", txt_IMPACC4_Interest_matu.Text.Trim());
            SqlParameter P_txt_IMPACC4_Interest_lump = new SqlParameter("@IMP_ACC4_Interest_LUMP", txt_IMPACC4_Interest_lump.Text.Trim());
            SqlParameter P_txt_IMPACC4_Interest_Contract_no = new SqlParameter("@IMP_ACC4_Interest_Contract_No", txt_IMPACC4_Interest_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC4_Interest_Ex_Curr = new SqlParameter("@IMP_ACC4_Interest_Ex_Curr", txt_IMPACC4_Interest_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_Interest_Ex_rate = new SqlParameter("@IMP_ACC4_Interest_Exch_Rate", txt_IMPACC4_Interest_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC4_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Interest_Intnl_Exch_Rate", txt_IMPACC4_Interest_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC4_Commission_matu = new SqlParameter("@IMP_ACC4_Commission_MATU", txt_IMPACC4_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC4_Commission_lump = new SqlParameter("@IMP_ACC4_Commission_LUMP", txt_IMPACC4_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC4_Commission_Contract_no = new SqlParameter("@IMP_ACC4_Commission_Contract_No", txt_IMPACC4_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC4_Commission_Ex_Curr = new SqlParameter("@IMP_ACC4_Commission_Ex_Curr", txt_IMPACC4_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_Commission_Ex_rate = new SqlParameter("@IMP_ACC4_Commission_Exch_Rate", txt_IMPACC4_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC4_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Commission_Intnl_Exch_Rate", txt_IMPACC4_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC4_Their_Commission_matu = new SqlParameter("@IMP_ACC4_Their_Commission_MATU", txt_IMPACC4_Their_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC4_Their_Commission_lump = new SqlParameter("@IMP_ACC4_Their_Commission_LUMP", txt_IMPACC4_Their_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC4_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC4_Their_Commission_Contract_No", txt_IMPACC4_Their_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC4_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC4_Their_Commission_Ex_Curr", txt_IMPACC4_Their_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC4_Their_Commission_Exch_Rate", txt_IMPACC4_Their_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC4_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC4_Their_Commission_Intnl_Exch_Rate", txt_IMPACC4_Their_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Code = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Code", txt_IMPACC4_CR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_AC_Short_Name = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Short_Name", txt_IMPACC4_CR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Cust_abbr = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC4_CR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Cust_Acc = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC4_CR_Cust_Acc.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Curr", txt_IMPACC4_CR_Acceptance_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Acceptance_amt = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Amt", txt_IMPACC4_CR_Acceptance_amt.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Acceptance_payer = new SqlParameter("@IMP_ACC4_CR_Sundry_Deposit_Payer", txt_IMPACC4_CR_Acceptance_payer.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Interest_Curr = new SqlParameter("@IMP_ACC4_CR_Interest_Curr", txt_IMPACC4_CR_Interest_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Interest_amt = new SqlParameter("@IMP_ACC4_CR_Interest_Amount", txt_IMPACC4_CR_Interest_amt.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Interest_payer = new SqlParameter("@IMP_ACC4_CR_Interest_Payer", txt_IMPACC4_CR_Interest_payer.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Curr", txt_IMPACC4_CR_Accept_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Amount", txt_IMPACC4_CR_Accept_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Acceptance_Comm_Payer", txt_IMPACC4_CR_Accept_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Curr", txt_IMPACC4_CR_Pay_Handle_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Amount", txt_IMPACC4_CR_Pay_Handle_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Pay_Handle_Comm_Payer", txt_IMPACC4_CR_Pay_Handle_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Others_Curr = new SqlParameter("@IMP_ACC4_CR_Others_Curr", txt_IMPACC4_CR_Others_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Others_amt = new SqlParameter("@IMP_ACC4_CR_Others_Amount", txt_IMPACC4_CR_Others_amt.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Others_Payer = new SqlParameter("@IMP_ACC4_CR_Others_Payer", txt_IMPACC4_CR_Others_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Curr", txt_IMPACC4_CR_Their_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Amount", txt_IMPACC4_CR_Their_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC4_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC4_CR_Their_Comm_Payer", txt_IMPACC4_CR_Their_Commission_Payer.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Code = new SqlParameter("@IMP_ACC4_DR_Code", txt_IMPACC4_DR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_AC_Short_Name = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name", txt_IMPACC4_DR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_abbr = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr", txt_IMPACC4_DR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_Acc = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No", txt_IMPACC4_DR_Cust_Acc.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr", txt_IMPACC4_DR_Cur_Acc_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount", txt_IMPACC4_DR_Cur_Acc_amt.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer", txt_IMPACC4_DR_Cur_Acc_payer.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr2", txt_IMPACC4_DR_Cur_Acc_Curr2.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount2", txt_IMPACC4_DR_Cur_Acc_amt2.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer2", txt_IMPACC4_DR_Cur_Acc_payer2.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr3", txt_IMPACC4_DR_Cur_Acc_Curr3.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount3", txt_IMPACC4_DR_Cur_Acc_amt3.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer3", txt_IMPACC4_DR_Cur_Acc_payer3.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr4", txt_IMPACC4_DR_Cur_Acc_Curr4.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount4", txt_IMPACC4_DR_Cur_Acc_amt4.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer4", txt_IMPACC4_DR_Cur_Acc_payer4.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr5", txt_IMPACC4_DR_Cur_Acc_Curr5.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount5", txt_IMPACC4_DR_Cur_Acc_amt5.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer5", txt_IMPACC4_DR_Cur_Acc_payer5.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Curr6", txt_IMPACC4_DR_Cur_Acc_Curr6.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Amount6", txt_IMPACC4_DR_Cur_Acc_amt6.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC4_DR_Current_Acc_Payer6", txt_IMPACC4_DR_Cur_Acc_payer6.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Code2 = new SqlParameter("@IMP_ACC4_DR_Code2", txt_IMPACC4_DR_Code2.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name2", txt_IMPACC4_DR_AC_Short_Name2.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr2", txt_IMPACC4_DR_Cust_abbr2.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No2", txt_IMPACC4_DR_Cust_Acc2.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Code3 = new SqlParameter("@IMP_ACC4_DR_Code3", txt_IMPACC4_DR_Code3.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name3", txt_IMPACC4_DR_AC_Short_Name3.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr3", txt_IMPACC4_DR_Cust_abbr3.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No3", txt_IMPACC4_DR_Cust_Acc3.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Code4 = new SqlParameter("@IMP_ACC4_DR_Code4", txt_IMPACC4_DR_Code4.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name4", txt_IMPACC4_DR_AC_Short_Name4.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr4", txt_IMPACC4_DR_Cust_abbr4.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No4", txt_IMPACC4_DR_Cust_Acc4.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Code5 = new SqlParameter("@IMP_ACC4_DR_Code5", txt_IMPACC4_DR_Code5.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name5", txt_IMPACC4_DR_AC_Short_Name5.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr5", txt_IMPACC4_DR_Cust_abbr5.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No5", txt_IMPACC4_DR_Cust_Acc5.Text.Trim());

            SqlParameter P_txt_IMPACC4_DR_Code6 = new SqlParameter("@IMP_ACC4_DR_Code6", txt_IMPACC4_DR_Code6.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC4_DR_Acc_Short_Name6", txt_IMPACC4_DR_AC_Short_Name6.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC4_DR_Cust_Abbr6", txt_IMPACC4_DR_Cust_abbr6.Text.Trim());
            SqlParameter P_txt_IMPACC4_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC4_DR_Cust_Acc_No6", txt_IMPACC4_DR_Cust_Acc6.Text.Trim());

            ///////IMPORT ACCOUNTING 5

            //---------------Anand 11-08-2023----------------------------
            string IMPACC5Flag = "";
            if (chk_IMPACC5Flag.Checked == true)
                IMPACC5Flag = "Y";
            else
                IMPACC5Flag = "N";
            //----------------END---------------------------------------
            SqlParameter P_chk_IMPACC5Flag = new SqlParameter("@IMP_ACC5_Flag", IMPACC5Flag);//_chk_IMPACC5Flag.ToUpper());
            SqlParameter P_txt_IMPACC5_FCRefNo = new SqlParameter("@IMP_ACC5_FCRefNo", txt_IMPACC5_FCRefNo.Text.Trim());
            SqlParameter P_txt_IMPACC5_DiscAmt = new SqlParameter("@IMP_ACC5_Amount", txt_IMPACC5_DiscAmt.Text.Trim());
            SqlParameter P_txt_IMPACC5_DiscExchRate = new SqlParameter("@IMP_ACC5_ExchRate", "");// _txt_IMPACC5_DiscExchRate.ToUpper());
            SqlParameter P_txt_IMPACC5_Princ_matu = new SqlParameter("@IMP_ACC5_Principal_MATU", txt_IMPACC5_Princ_matu.Text.Trim());
            SqlParameter P_txt_IMPACC5_Princ_lump = new SqlParameter("@IMP_ACC5_Principal_LUMP", txt_IMPACC5_Princ_lump.Text.Trim());
            SqlParameter P_txt_IMPACC5_Princ_Contract_no = new SqlParameter("@IMP_ACC5_Principal_Contract_No", txt_IMPACC5_Princ_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC5_Princ_Ex_Curr = new SqlParameter("@IMP_ACC5_Principal_Ex_Curr", txt_IMPACC5_Princ_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_Princ_Ex_rate = new SqlParameter("@IMP_ACC5_Principal_Exch_Rate", txt_IMPACC5_Princ_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC5_Princ_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Principal_Intnl_Exch_Rate", txt_IMPACC5_Princ_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC5_Interest_matu = new SqlParameter("@IMP_ACC5_Interest_MATU", txt_IMPACC5_Interest_matu.Text.Trim());
            SqlParameter P_txt_IMPACC5_Interest_lump = new SqlParameter("@IMP_ACC5_Interest_LUMP", txt_IMPACC5_Interest_lump.Text.Trim());
            SqlParameter P_txt_IMPACC5_Interest_Contract_no = new SqlParameter("@IMP_ACC5_Interest_Contract_No", txt_IMPACC5_Interest_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC5_Interest_Ex_Curr = new SqlParameter("@IMP_ACC5_Interest_Ex_Curr", txt_IMPACC5_Interest_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_Interest_Ex_rate = new SqlParameter("@IMP_ACC5_Interest_Exch_Rate", txt_IMPACC5_Interest_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC5_Interest_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Interest_Intnl_Exch_Rate", txt_IMPACC5_Interest_Intnl_Ex_rate.Text.Trim());

            SqlParameter P_txt_IMPACC5_Commission_matu = new SqlParameter("@IMP_ACC5_Commission_MATU", txt_IMPACC5_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC5_Commission_lump = new SqlParameter("@IMP_ACC5_Commission_LUMP", txt_IMPACC5_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC5_Commission_Contract_no = new SqlParameter("@IMP_ACC5_Commission_Contract_No", txt_IMPACC5_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC5_Commission_Ex_Curr = new SqlParameter("@IMP_ACC5_Commission_Ex_Curr", txt_IMPACC5_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_Commission_Ex_rate = new SqlParameter("@IMP_ACC5_Commission_Exch_Rate", txt_IMPACC5_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC5_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Commission_Intnl_Exch_Rate", txt_IMPACC5_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC5_Their_Commission_matu = new SqlParameter("@IMP_ACC5_Their_Commission_MATU", txt_IMPACC5_Their_Commission_matu.Text.Trim());
            SqlParameter P_txt_IMPACC5_Their_Commission_lump = new SqlParameter("@IMP_ACC5_Their_Commission_LUMP", txt_IMPACC5_Their_Commission_lump.Text.Trim());
            SqlParameter P_txt_IMPACC5_Their_Commission_Contract_no = new SqlParameter("@IMP_ACC5_Their_Commission_Contract_No", txt_IMPACC5_Their_Commission_Contract_no.Text.Trim());
            SqlParameter P_txt_IMPACC5_Their_Commission_Ex_Curr = new SqlParameter("@IMP_ACC5_Their_Commission_Ex_Curr", txt_IMPACC5_Their_Commission_Ex_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_Their_Commission_Ex_rate = new SqlParameter("@IMP_ACC5_Their_Commission_Exch_Rate", txt_IMPACC5_Their_Commission_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC5_Their_Commission_Intnl_Ex_rate = new SqlParameter("@IMP_ACC5_Their_Commission_Intnl_Exch_Rate", txt_IMPACC5_Their_Commission_Intnl_Ex_rate.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Code = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Code", txt_IMPACC5_CR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_AC_Short_Name = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Short_Name", txt_IMPACC5_CR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Cust_abbr = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Cust_Abbr", txt_IMPACC5_CR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Cust_Acc = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Cust_Acc_No", txt_IMPACC5_CR_Cust_Acc.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Acceptance_Curr = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Curr", txt_IMPACC5_CR_Acceptance_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Acceptance_amt = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Amt", txt_IMPACC5_CR_Acceptance_amt.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Acceptance_payer = new SqlParameter("@IMP_ACC5_CR_Sundry_Deposit_Payer", txt_IMPACC5_CR_Acceptance_payer.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Interest_Curr = new SqlParameter("@IMP_ACC5_CR_Interest_Curr", txt_IMPACC5_CR_Interest_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Interest_amt = new SqlParameter("@IMP_ACC5_CR_Interest_Amount", txt_IMPACC5_CR_Interest_amt.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Interest_payer = new SqlParameter("@IMP_ACC5_CR_Interest_Payer", txt_IMPACC5_CR_Interest_payer.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Accept_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Curr", txt_IMPACC5_CR_Accept_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Accept_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Amount", txt_IMPACC5_CR_Accept_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Accept_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Acceptance_Comm_Payer", txt_IMPACC5_CR_Accept_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Curr", txt_IMPACC5_CR_Pay_Handle_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Amount", txt_IMPACC5_CR_Pay_Handle_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Pay_Handle_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Pay_Handle_Comm_Payer", txt_IMPACC5_CR_Pay_Handle_Commission_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Others_Curr = new SqlParameter("@IMP_ACC5_CR_Others_Curr", txt_IMPACC5_CR_Others_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Others_amt = new SqlParameter("@IMP_ACC5_CR_Others_Amount", txt_IMPACC5_CR_Others_amt.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Others_Payer = new SqlParameter("@IMP_ACC5_CR_Others_Payer", txt_IMPACC5_CR_Others_Payer.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Their_Commission_Curr = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Curr", txt_IMPACC5_CR_Their_Commission_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Their_Commission_amt = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Amount", txt_IMPACC5_CR_Their_Commission_amt.Text.Trim());
            SqlParameter P_txt_IMPACC5_CR_Their_Commission_Payer = new SqlParameter("@IMP_ACC5_CR_Their_Comm_Payer", txt_IMPACC5_CR_Their_Commission_Payer.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Code = new SqlParameter("@IMP_ACC5_DR_Code", txt_IMPACC5_DR_Code.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_AC_Short_Name = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name", txt_IMPACC5_DR_AC_Short_Name.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_abbr = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr", txt_IMPACC5_DR_Cust_abbr.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_Acc = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No", txt_IMPACC5_DR_Cust_Acc.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr", txt_IMPACC5_DR_Cur_Acc_Curr.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount", txt_IMPACC5_DR_Cur_Acc_amt.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer", txt_IMPACC5_DR_Cur_Acc_payer.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr2", txt_IMPACC5_DR_Cur_Acc_Curr2.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount2", txt_IMPACC5_DR_Cur_Acc_amt2.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer2 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer2", txt_IMPACC5_DR_Cur_Acc_payer2.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr3", txt_IMPACC5_DR_Cur_Acc_Curr3.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount3", txt_IMPACC5_DR_Cur_Acc_amt3.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer3 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer3", txt_IMPACC5_DR_Cur_Acc_payer3.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr4", txt_IMPACC5_DR_Cur_Acc_Curr4.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount4", txt_IMPACC5_DR_Cur_Acc_amt4.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer4 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer4", txt_IMPACC5_DR_Cur_Acc_payer4.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr5", txt_IMPACC5_DR_Cur_Acc_Curr5.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount5", txt_IMPACC5_DR_Cur_Acc_amt5.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer5 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer5", txt_IMPACC5_DR_Cur_Acc_payer5.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_Curr6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Curr6", txt_IMPACC5_DR_Cur_Acc_Curr6.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_amt6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Amount6", txt_IMPACC5_DR_Cur_Acc_amt6.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cur_Acc_payer6 = new SqlParameter("@IMP_ACC5_DR_Current_Acc_Payer6", txt_IMPACC5_DR_Cur_Acc_payer6.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Code2 = new SqlParameter("@IMP_ACC5_DR_Code2", txt_IMPACC5_DR_Code2.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_AC_Short_Name2 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name2", txt_IMPACC5_DR_AC_Short_Name2.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_abbr2 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr2", txt_IMPACC5_DR_Cust_abbr2.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_Acc2 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No2", txt_IMPACC5_DR_Cust_Acc2.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Code3 = new SqlParameter("@IMP_ACC5_DR_Code3", txt_IMPACC5_DR_Code3.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_AC_Short_Name3 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name3", txt_IMPACC5_DR_AC_Short_Name3.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_abbr3 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr3", txt_IMPACC5_DR_Cust_abbr3.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_Acc3 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No3", txt_IMPACC5_DR_Cust_Acc3.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Code4 = new SqlParameter("@IMP_ACC5_DR_Code4", txt_IMPACC5_DR_Code4.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_AC_Short_Name4 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name4", txt_IMPACC5_DR_AC_Short_Name4.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_abbr4 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr4", txt_IMPACC5_DR_Cust_abbr4.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_Acc4 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No4", txt_IMPACC5_DR_Cust_Acc4.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Code5 = new SqlParameter("@IMP_ACC5_DR_Code5", txt_IMPACC5_DR_Code5.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_AC_Short_Name5 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name5", txt_IMPACC5_DR_AC_Short_Name5.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_abbr5 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr5", txt_IMPACC5_DR_Cust_abbr5.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_Acc5 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No5", txt_IMPACC5_DR_Cust_Acc5.Text.Trim());

            SqlParameter P_txt_IMPACC5_DR_Code6 = new SqlParameter("@IMP_ACC5_DR_Code6", txt_IMPACC5_DR_Code6.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_AC_Short_Name6 = new SqlParameter("@IMP_ACC5_DR_Acc_Short_Name6", txt_IMPACC5_DR_AC_Short_Name6.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_abbr6 = new SqlParameter("@IMP_ACC5_DR_Cust_Abbr6", txt_IMPACC5_DR_Cust_abbr6.Text.Trim());
            SqlParameter P_txt_IMPACC5_DR_Cust_Acc6 = new SqlParameter("@IMP_ACC5_DR_Cust_Acc_No6", txt_IMPACC5_DR_Cust_Acc6.Text.Trim());

            //-------------------------------------------------------------END----------------------------------------
            //----------------------------------------------------Anand 28-06-2023------------------------------------------------------
            // --------------------------------------- General Operations ----------------------------------------------------------------

            //---------------Anand 11-08-2023----------------------------
            string Generaloperation1 = "";
            if (chk_Generaloperation1.Checked == true)
                Generaloperation1 = "Y";
            else
                Generaloperation1 = "N";
            //----------------END---------------------------------------
            SqlParameter GO_Flag = new SqlParameter("@GO_Flag", Generaloperation1);
            SqlParameter GO_ValueDate = new SqlParameter("@GO_ValueDate", txtGO_ValueDate.Text.Trim());
            // SqlParameter GO_Ref_No = new SqlParameter("@GO_Ref_No", txtGO_Ref_No.Text.Trim());
            SqlParameter GO_Remark = new SqlParameter("@GO_Remark", txtGO_Remark.Text.Trim());
            SqlParameter GO_Section = new SqlParameter("@GO_Section", txtGO_Section.Text.Trim());
            SqlParameter GO_Comment = new SqlParameter("@GO_Comment", txtGO_Comment.Text.Trim());
            SqlParameter GO_Memo = new SqlParameter("@GO_Memo", txtGO_Memo.Text.Trim());
            SqlParameter GO_SchemeNo = new SqlParameter("@GO_SchemeNo", txtGO_SchemeNo.Text.Trim());
            // --------- Debit ----------------
            SqlParameter GO_Debit = new SqlParameter("@GO_Debit", txtGO_Debit.SelectedValue);//Modified by Anand10-08-2023
            SqlParameter GO_Debit_CCY = new SqlParameter("@GO_Debit_CCY", txtGO_Debit_CCY.Text.Trim());
            SqlParameter GO_Debit_Amt = new SqlParameter("@GO_Debit_Amt", txtGO_Debit_Amt.Text.Trim());
            SqlParameter GO_Debit_Cust = new SqlParameter("@GO_Debit_Cust", txtGO_Debit_Cust.Text.Trim());
            SqlParameter GO_Debit_Cust_Name = new SqlParameter("@GO_Debit_Cust_Name", txtGO_Debit_Cust_Name.Text.Trim());//Added by Anand 10-08-2023
            SqlParameter GO_Debit_Cust_AcCode = new SqlParameter("@GO_Debit_Cust_AcCode", txtGO_Debit_Cust_AcCode.Text.Trim());
            SqlParameter GO_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Debit_Cust_AcCode_Name", txtGO_Debit_Cust_AcCode_Name.Text.Trim());//Added by Anand10-08-2023
            SqlParameter GO_Debit_Cust_AccNo = new SqlParameter("@GO_Debit_Cust_AccNo", txtGO_Debit_Cust_AccNo.Text.Trim());
            SqlParameter GO_Debit_ExchRate = new SqlParameter("@GO_Debit_ExchRate", txtGO_Debit_ExchRate.Text.Trim());
            SqlParameter GO_Debit_ExchCCY = new SqlParameter("@GO_Debit_ExchCCY", txtGO_Debit_ExchCCY.Text.Trim());
            SqlParameter GO_Debit_Fund = new SqlParameter("@GO_Debit_Fund", txtGO_Debit_Fund.Text.Trim());
            SqlParameter GO_Debit_CheckNo = new SqlParameter("@GO_Debit_CheckNo", txtGO_Debit_CheckNo.Text.Trim());
            SqlParameter GO_Debit_Available = new SqlParameter("@GO_Debit_Available", txtGO_Debit_Available.Text.Trim());
            SqlParameter GO_Debit_Advice_Print = new SqlParameter("@GO_Debit_Advice_Print", txtGO_Debit_Advice_Print.Text.Trim());
            SqlParameter GO_Debit_Details = new SqlParameter("@GO_Debit_Details", txtGO_Debit_Details.Text.Trim());
            SqlParameter GO_Debit_Entity = new SqlParameter("@GO_Debit_Entity", txtGO_Debit_Entity.Text.Trim());
            SqlParameter GO_Debit_Division = new SqlParameter("@GO_Debit_Division", txtGO_Debit_Division.Text.Trim());
            SqlParameter GO_Debit_InterAmt = new SqlParameter("@GO_Debit_InterAmt", txtGO_Debit_InterAmt.Text.Trim());
            SqlParameter GO_Debit_InterRate = new SqlParameter("@GO_Debit_InterRate", txtGO_Debit_InterRate.Text.Trim());
            // --------- Credit ----------------
            SqlParameter GO_Credit = new SqlParameter("@GO_Credit", txtGO_Credit.SelectedValue);// Modified by Anand 10-08-2023
            SqlParameter GO_Credit_Amt = new SqlParameter("@GO_Credit_Amt", txtGO_Credit_Amt.Text.Trim());
            SqlParameter GO_Credit_CCY = new SqlParameter("@GO_Credit_CCY", txtGO_Credit_CCY.Text.Trim());
            SqlParameter GO_Credit_Cust = new SqlParameter("@GO_Credit_Cust", txtGO_Credit_Cust.Text.Trim());
            SqlParameter GO_Credit_Cust_Name = new SqlParameter("@GO_Credit_Cust_Name", txtGO_Credit_Cust_Name.Text.Trim());// Added by Anand 10-08-2023
            SqlParameter GO_Credit_Cust_AcCode = new SqlParameter("@GO_Credit_Cust_AcCode", txtGO_Credit_Cust_AcCode.Text.Trim());
            SqlParameter GO_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Credit_Cust_AcCode_Name", txtGO_Credit_Cust_AcCode_Name.Text.Trim());// Added by Anand 10-08-2023
            SqlParameter GO_Credit_Cust_AccNo = new SqlParameter("@GO_Credit_Cust_AccNo", txtGO_Credit_Cust_AccNo.Text.Trim());
            SqlParameter GO_Credit_ExchCCY = new SqlParameter("@GO_Credit_ExchCCY", txtGO_Credit_ExchCCY.Text.Trim());
            SqlParameter GO_Credit_ExchRate = new SqlParameter("@GO_Credit_ExchRate", txtGO_Credit_ExchRate.Text.Trim());
            SqlParameter GO_Credit_Fund = new SqlParameter("@GO_Credit_Fund", txtGO_Credit_Fund.Text.Trim());
            SqlParameter GO_Credit_CheckNo = new SqlParameter("@GO_Credit_CheckNo", txtGO_Credit_CheckNo.Text.Trim());
            SqlParameter GO_Credit_Available = new SqlParameter("@GO_Credit_Available", txtGO_Credit_Available.Text.Trim());
            SqlParameter GO_Credit_Advice_Print = new SqlParameter("@GO_Credit_Advice_Print", txtGO_Credit_Advice_Print.Text.Trim());
            SqlParameter GO_Credit_Details = new SqlParameter("@GO_Credit_Details", txtGO_Credit_Details.Text.Trim());
            SqlParameter GO_Credit_Entity = new SqlParameter("@GO_Credit_Entity", txtGO_Credit_Entity.Text.Trim());
            SqlParameter GO_Credit_Division = new SqlParameter("@GO_Credit_Division", txtGO_Credit_Division.Text.Trim());
            SqlParameter GO_Credit_InterAmt = new SqlParameter("@GO_Credit_InterAmt", txtGO_Credit_InterAmt.Text.Trim());
            SqlParameter GO_Credit_InterRate = new SqlParameter("@GO_Credit_InterRate", txtGO_Credit_InterRate.Text.Trim());

            // --------------------------------------- Normal General Operations -----------------------------------------------------------
            //---------------Anand 11-08-2023----------------------------
            string Generaloperation2 = "";
            if (chk_Generaloperation2.Checked == true)
                Generaloperation2 = "Y";
            else
                Generaloperation2 = "N";
            //----------------END---------------------------------------
            SqlParameter NGO_Flag = new SqlParameter("@NGO_Flag", Generaloperation2);
            SqlParameter NGO_ValueDate = new SqlParameter("@NGO_ValueDate", txtNGO_ValueDate.Text.Trim());
            // SqlParameter NGO_Ref_No = new SqlParameter("@NGO_Ref_No", txtNGO_Ref_No.Text.Trim());
            SqlParameter NGO_Remark = new SqlParameter("@NGO_Remark", txtNGO_Remark.Text.Trim());
            SqlParameter NGO_Section = new SqlParameter("@NGO_Section", txtNGO_Section.Text.Trim());
            SqlParameter NGO_Comment = new SqlParameter("@NGO_Comment", txtNGO_Comment.Text.Trim());
            SqlParameter NGO_Memo = new SqlParameter("@NGO_Memo", txtNGO_Memo.Text.Trim());
            SqlParameter NGO_SchemeNo = new SqlParameter("@NGO_SchemeNo", txtNGO_SchemeNo.Text.Trim());
            // --------- Debit ----------------
            SqlParameter NGO_Debit = new SqlParameter("@NGO_Debit", txtNGO_Debit.SelectedValue);// Modified by Anand 10-08-2023
            SqlParameter NGO_Debit_CCY = new SqlParameter("@NGO_Debit_CCY", txtNGO_Debit_CCY.Text.Trim());
            SqlParameter NGO_Debit_Amt = new SqlParameter("@NGO_Debit_Amt", txtNGO_Debit_Amt.Text.Trim());
            SqlParameter NGO_Debit_Cust = new SqlParameter("@NGO_Debit_Cust", txtNGO_Debit_Cust.Text.Trim());
            SqlParameter NGO_Debit_Cust_Name = new SqlParameter("@NGO_Debit_Cust_Name", txtNGO_Debit_Cust_Name.Text.Trim());// Added by Anand 10-08-2023
            SqlParameter NGO_Debit_Cust_AcCode = new SqlParameter("@NGO_Debit_Cust_AcCode", txtNGO_Debit_Cust_AcCode.Text.Trim());
            SqlParameter NGO_Debit_Cust_AcCode_Name = new SqlParameter("@NGO_Debit_Cust_AcCode_Name", txtNGO_Debit_Cust_AcCode_Name.Text.Trim());// Added by Anand 10-08-2023
            SqlParameter NGO_Debit_Cust_AccNo = new SqlParameter("@NGO_Debit_Cust_AccNo", txtNGO_Debit_Cust_AccNo.Text.Trim());
            SqlParameter NGO_Debit_ExchRate = new SqlParameter("@NGO_Debit_ExchRate", txtNGO_Debit_ExchRate.Text.Trim());
            SqlParameter NGO_Debit_ExchCCY = new SqlParameter("@NGO_Debit_ExchCCY", txtNGO_Debit_ExchCCY.Text.Trim());
            SqlParameter NGO_Debit_Fund = new SqlParameter("@NGO_Debit_Fund", txtNGO_Debit_Fund.Text.Trim());
            SqlParameter NGO_Debit_CheckNo = new SqlParameter("@NGO_Debit_CheckNo", txtNGO_Debit_CheckNo.Text.Trim());
            SqlParameter NGO_Debit_Available = new SqlParameter("@NGO_Debit_Available", txtNGO_Debit_Available.Text.Trim());
            SqlParameter NGO_Debit_Advice_Print = new SqlParameter("@NGO_Debit_Advice_Print", txtNGO_Debit_Advice_Print.Text.Trim());
            SqlParameter NGO_Debit_Details = new SqlParameter("@NGO_Debit_Details", txtNGO_Debit_Details.Text.Trim());
            SqlParameter NGO_Debit_Entity = new SqlParameter("@NGO_Debit_Entity", txtNGO_Debit_Entity.Text.Trim());
            SqlParameter NGO_Debit_Division = new SqlParameter("@NGO_Debit_Division", txtNGO_Debit_Division.Text.Trim());
            SqlParameter NGO_Debit_InterAmt = new SqlParameter("@NGO_Debit_InterAmt", txtNGO_Debit_InterAmt.Text.Trim());
            SqlParameter NGO_Debit_InterRate = new SqlParameter("@NGO_Debit_InterRate", txtNGO_Debit_InterRate.Text.Trim());
            // --------- Credit ----------------
            SqlParameter NGO_Credit = new SqlParameter("@NGO_Credit", txtNGO_Credit.SelectedValue);// Modified by Anand 10-08-2023
            SqlParameter NGO_Credit_Amt = new SqlParameter("@NGO_Credit_Amt", txtNGO_Credit_Amt.Text.Trim());
            SqlParameter NGO_Credit_CCY = new SqlParameter("@NGO_Credit_CCY", txtNGO_Credit_CCY.Text.Trim());
            SqlParameter NGO_Credit_Cust = new SqlParameter("@NGO_Credit_Cust", txtNGO_Credit_Cust.Text.Trim());
            SqlParameter NGO_Credit_Cust_Name = new SqlParameter("@NGO_Credit_Cust_Name", txtNGO_Credit_Cust_Name.Text.Trim());// Added by Anand 10-08-2023
            SqlParameter NGO_Credit_Cust_AcCode = new SqlParameter("@NGO_Credit_Cust_AcCode", txtNGO_Credit_Cust_AcCode.Text.Trim());
            SqlParameter NGO_Credit_Cust_AcCode_Name = new SqlParameter("@NGO_Credit_Cust_AcCode_Name", txtNGO_Credit_Cust_AcCode_Name.Text.Trim());
            SqlParameter NGO_Credit_Cust_AccNo = new SqlParameter("@NGO_Credit_Cust_AccNo", txtNGO_Credit_Cust_AccNo.Text.Trim());
            SqlParameter NGO_Credit_ExchCCY = new SqlParameter("@NGO_Credit_ExchCCY", txtNGO_Credit_ExchCCY.Text.Trim());
            SqlParameter NGO_Credit_ExchRate = new SqlParameter("@NGO_Credit_ExchRate", txtNGO_Credit_ExchRate.Text.Trim());
            SqlParameter NGO_Credit_Fund = new SqlParameter("@NGO_Credit_Fund", txtNGO_Credit_Fund.Text.Trim());
            SqlParameter NGO_Credit_CheckNo = new SqlParameter("@NGO_Credit_CheckNo", txtNGO_Credit_CheckNo.Text.Trim());
            SqlParameter NGO_Credit_Available = new SqlParameter("@NGO_Credit_Available", txtNGO_Credit_Available.Text.Trim());
            SqlParameter NGO_Credit_Advice_Print = new SqlParameter("@NGO_Credit_Advice_Print", txtNGO_Credit_Advice_Print.Text.Trim());
            SqlParameter NGO_Credit_Details = new SqlParameter("@NGO_Credit_Details", txtNGO_Credit_Details.Text.Trim());
            SqlParameter NGO_Credit_Entity = new SqlParameter("@NGO_Credit_Entity", txtNGO_Credit_Entity.Text.Trim());
            SqlParameter NGO_Credit_Division = new SqlParameter("@NGO_Credit_Division", txtNGO_Credit_Division.Text.Trim());
            SqlParameter NGO_Credit_InterAmt = new SqlParameter("@NGO_Credit_InterAmt", txtNGO_Credit_InterAmt.Text.Trim());
            SqlParameter NGO_Credit_InterRate = new SqlParameter("@NGO_Credit_InterRate", txtNGO_Credit_InterRate.Text.Trim());

            //----------------------------------------------------------END------------------------------------------------------------
            //-----------------------------------------------Anand 04-07-2023 INTER_OFFICE--------------------------------------
            //---------------Anand 11-08-2023----------------------------
            string InterOffice = "";
            if (chk_InterOffice.Checked == true)
                InterOffice = "Y";
            else
                InterOffice = "N";
            //----------------END---------------------------------------
            SqlParameter P_txt_GOAccChange_Flag = new SqlParameter("@GO_Acc_Change_Flag", InterOffice);
            SqlParameter P_txt_GOAccChange_ValueDate = new SqlParameter("@GO_Acc_Change_ValueDate", txtIO_ValueDate.Text.Trim());
            SqlParameter P_txt_GOAccChange_Ref_No = new SqlParameter("@GO_Acc_Change_TransRef_No", txt_GOAccChange_Ref_No.Text.Trim());
            SqlParameter P_txt_GOAccChange_Comment = new SqlParameter("@GO_Acc_Change_Comment", txt_GOAccChange_Comment.Text.Trim());
            SqlParameter P_txt_GOAccChange_SectionNo = new SqlParameter("@GO_Acc_Change_Section", txt_GOAccChange_SectionNo.Text.Trim());
            SqlParameter P_txt_GOAccChange_Remarks = new SqlParameter("@GO_Acc_Change_Remark", txt_GOAccChange_Remarks.Text.Trim());
            SqlParameter P_txt_GOAccChange_Memo = new SqlParameter("@GO_Acc_Change_Memo", txt_GOAccChange_Memo.Text.Trim());
            SqlParameter P_txt_GOAccChange_Scheme_no = new SqlParameter("@GO_Acc_Change_SchemeNo", txt_GOAccChange_Scheme_no.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Code = new SqlParameter("@GO_Acc_Change_Debit_Code", txt_GOAccChange_Debit_Code.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Curr = new SqlParameter("@GO_Acc_Change_Debit_CCY", txt_GOAccChange_Debit_Curr.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Amt = new SqlParameter("@GO_Acc_Change_Debit_Amt", txt_GOAccChange_Debit_Amt.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Cust = new SqlParameter("@GO_Acc_Change_Debit_Cust_abbr", txt_GOAccChange_Debit_Cust.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Cust_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_Name", txt_GOAccChange_Debit_Cust_Name.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode", txt_GOAccChange_Debit_Cust_AcCode.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccCode_Disc", txt_GOAccChange_Debit_Cust_AcCode_Name.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Debit_Cust_AccNo", txt_GOAccChange_Debit_Cust_AccNo.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Debit_ExchRate", txt_GOAccChange_Debit_Exch_Rate.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Exch_CCY = new SqlParameter("@GO_Acc_Change_Debit_ExchCCY", txt_GOAccChange_Debit_Exch_CCY.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_FUND = new SqlParameter("@GO_Acc_Change_Debit_Fund", txt_GOAccChange_Debit_FUND.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Check_No = new SqlParameter("@GO_Acc_Change_Debit_CheckNo", txt_GOAccChange_Debit_Check_No.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Available = new SqlParameter("@GO_Acc_Change_Debit_Available", txt_GOAccChange_Debit_Available.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_AdPrint = new SqlParameter("@GO_Acc_Change_Debit_Advice_Print", txt_GOAccChange_Debit_AdPrint.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Details = new SqlParameter("@GO_Acc_Change_Debit_Details", txt_GOAccChange_Debit_Details.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Entity = new SqlParameter("@GO_Acc_Change_Debit_Entity", txt_GOAccChange_Debit_Entity.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Division = new SqlParameter("@GO_Acc_Change_Debit_Division", txt_GOAccChange_Debit_Division.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Debit_InterAmt", txt_GOAccChange_Debit_Inter_Amount.Text.Trim());
            SqlParameter P_txt_GOAccChange_Debit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Debit_InterRate", txt_GOAccChange_Debit_Inter_Rate.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Code = new SqlParameter("@GO_Acc_Change_Credit_Code", txt_GOAccChange_Credit_Code.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Curr = new SqlParameter("@GO_Acc_Change_Credit_CCY", txt_GOAccChange_Credit_Curr.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Amt = new SqlParameter("@GO_Acc_Change_Credit_Amt", txt_GOAccChange_Credit_Amt.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Cust = new SqlParameter("@GO_Acc_Change_Credit_Cust_abbr", txt_GOAccChange_Credit_Cust.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Cust_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_Name", txt_GOAccChange_Credit_Cust_Name.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Cust_AcCode = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode", txt_GOAccChange_Credit_Cust_AcCode.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Cust_AcCode_Name = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccCode_Disc", txt_GOAccChange_Credit_Cust_AcCode_Name.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Cust_AccNo = new SqlParameter("@GO_Acc_Change_Credit_Cust_AccNo", txt_GOAccChange_Credit_Cust_AccNo.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Exch_Rate = new SqlParameter("@GO_Acc_Change_Credit_ExchRate", txt_GOAccChange_Credit_Exch_Rate.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Exch_CCY = new SqlParameter("@GO_Acc_Change_Credit_ExchCCY", txt_GOAccChange_Credit_Exch_Curr.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_FUND = new SqlParameter("@GO_Acc_Change_Credit_Fund", txt_GOAccChange_Credit_FUND.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Check_No = new SqlParameter("@GO_Acc_Change_Credit_CheckNo", txt_GOAccChange_Credit_Check_No.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Available = new SqlParameter("@GO_Acc_Change_Credit_Available", txt_GOAccChange_Credit_Available.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_AdPrint = new SqlParameter("@GO_Acc_Change_Credit_Advice_Print", txt_GOAccChange_Credit_AdPrint.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Details = new SqlParameter("@GO_Acc_Change_Credit_Details", txt_GOAccChange_Credit_Details.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Entity = new SqlParameter("@GO_Acc_Change_Credit_Entity", txt_GOAccChange_Credit_Entity.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Division = new SqlParameter("@GO_Acc_Change_Credit_Division", txt_GOAccChange_Credit_Division.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Inter_Amount = new SqlParameter("@GO_Acc_Change_Credit_InterAmt", txt_GOAccChange_Credit_Inter_Amount.Text.Trim());
            SqlParameter P_txt_GOAccChange_Credit_Inter_Rate = new SqlParameter("@GO_Acc_Change_Credit_InterRate", txt_GOAccChange_Credit_Inter_Rate.Text.Trim());

            //---------------------------------------------------End------------------------------------------------

            string query = "TF_UpdateExportRealisationDetails_Maker";
            string queryAccounting = "TF_UpdateExportRealisationDetails_AccountingEntry_Maker";
            string queryGO = "TF_UpdateExportRealisationDetails_GOEntry_Maker";
            string _result = "";
            string _resultAccounting = "";
            string _resultGO = "";

            TF_DATA objSave = new TF_DATA();

            _result = objSave.SaveDeleteData(query, mode, branch, docno, doctype, billtype, custacno, opc, RemiName, RemiCotry, RemiAdd, pconsigneeID, obc, PSwiftCode, pRB, pRBA, pRBC, p20, pProcessingDate, realdate, valuedate, realamt, realamtinr, exrate,
                                             remark, nyrefno, payind1, srno, colamt, colamtinr, Payer, pIntFrom, pIntTo, pIntDays, pIntRate, pInterest, CheckIRM, CheckTT, pCCheckDummy,
                                             loan1, bkline, bal, p1, p2, CrossrealCur, RelCrossCurRate, CrossrealAmt,

                                             TTREFNO1, TTAmt1, TTREFNO2, TTAmt2, TTREFNO3, TTAmt3, TTREFNO4, TTAmt4, TTREFNO5, TTAmt5, pTTRef6, pTTAmt6, pTTRef7, pTTAmt7, pTTRef8, pTTAmt8, pTTRef9, pTTAmt9,
                                             pTTRef10, pTTAmt10, pTTRef11, pTTAmt11, pTTRef12, pTTAmt12, pTTRef13, pTTAmt13, pTTRef14, pTTAmt14, pTTRef15, pTTAmt15, PTTCurr1, PTTCurr2, PTTCurr3, PTTCurr4, PTTCurr5,
                                             PTTCurr6, PTTCurr7, PTTCurr8, PTTCurr9, PTTCurr10, PTTCurr11, PTTCurr12, PTTCurr13, PTTCurr14, PTTCurr15, PTotTTAmt1, PTotTTAmt2, PTotTTAmt3, PTotTTAmt4, PTotTTAmt5, PTotTTAmt6,
                                             PTotTTAmt7, PTotTTAmt8, PTotTTAmt9, PTotTTAmt10, PTotTTAmt11, PTotTTAmt12, PTotTTAmt13, PTotTTAmt14, PTotTTAmt15, PBalTTAmt1, PBalTTAmt2, PBalTTAmt3, PBalTTAmt4, PBalTTAmt5, PBalTTAmt6,
                                             PBalTTAmt7, PBalTTAmt8, PBalTTAmt9, PBalTTAmt10, PBalTTAmt11, PBalTTAmt12, PBalTTAmt13, PBalTTAmt14, PBalTTAmt15, PTTRealisedCurr1, PTTRealisedCurr2, PTTRealisedCurr3, PTTRealisedCurr4,
                                             PTTRealisedCurr5, PTTRealisedCurr6, PTTRealisedCurr7, PTTRealisedCurr8, PTTRealisedCurr9, PTTRealisedCurr10, PTTRealisedCurr11, PTTRealisedCurr12, PTTRealisedCurr13, PTTRealisedCurr14,
                                             PTTRealisedCurr15, PTTCrossCurrRate1, PTTCrossCurrRate2, PTTCrossCurrRate3, PTTCrossCurrRate4, PTTCrossCurrRate5, PTTCrossCurrRate6, PTTCrossCurrRate7, PTTCrossCurrRate8, PTTCrossCurrRate9,
                                             PTTCrossCurrRate10, PTTCrossCurrRate11, PTTCrossCurrRate12, PTTCrossCurrRate13, PTTCrossCurrRate14, PTTCrossCurrRate15, PTTAmtRealised1, PTTAmtRealised2, PTTAmtRealised3, PTTAmtRealised4,
                                             PTTAmtRealised5, PTTAmtRealised6, PTTAmtRealised7, PTTAmtRealised8, PTTAmtRealised9, PTTAmtRealised10, PTTAmtRealised11, PTTAmtRealised12, PTTAmtRealised13, PTTAmtRealised14, PTTAmtRealised15,

                                             FIRC_Status, FIRC_NO, FIRC_AD_CODE,
                                             FBKCharge, pcfcamt, pEEfcAmt, overdueamt, POutstandingAmt, PInstructedAmt, PLeiExchRate, pLeiInrAmt, pIPurposeCode, pIModeOfPay, checkerstatus, pCheckPendingSB);

            //--------------------------------------Anand26-06-2023--------------------------------------

            if (_result.Substring(0, 5) == "added" || _result == "updated")
            {
                ////////////Import Accounting 1
                _resultAccounting = objSave.SaveDeleteData(queryAccounting, mode, branch, docno, srno, p1, p2, P_chk_IMPACC1Flag, P_txt_IMPACC1_FCRefNo,
    P_txt_IMPACC1_DiscAmt, P_txt_IMPACC1_DiscExchRate,
    P_txt_IMPACC1_Princ_matu, P_txt_IMPACC1_Princ_lump, P_txt_IMPACC1_Princ_Contract_no, P_txt_IMPACC1_Princ_Ex_Curr, P_txt_IMPACC1_Princ_Ex_rate, P_txt_IMPACC1_Princ_Intnl_Ex_rate,
    P_txt_IMPACC1_Interest_matu, P_txt_IMPACC1_Interest_lump, P_txt_IMPACC1_Interest_Contract_no, P_txt_IMPACC1_Interest_Ex_Curr, P_txt_IMPACC1_Interest_Ex_rate, P_txt_IMPACC1_Interest_Intnl_Ex_rate,
    P_txt_IMPACC1_Commission_matu, P_txt_IMPACC1_Commission_lump, P_txt_IMPACC1_Commission_Contract_no, P_txt_IMPACC1_Commission_Ex_Curr, P_txt_IMPACC1_Commission_Ex_rate, P_txt_IMPACC1_Commission_Intnl_Ex_rate,
    P_txt_IMPACC1_Their_Commission_matu, P_txt_IMPACC1_Their_Commission_lump, P_txt_IMPACC1_Their_Commission_Contract_no, P_txt_IMPACC1_Their_Commission_Ex_Curr, P_txt_IMPACC1_Their_Commission_Ex_rate, P_txt_IMPACC1_Their_Commission_Intnl_Ex_rate,
    P_txt_IMPACC1_CR_Code, P_txt_IMPACC1_CR_AC_Short_Name, P_txt_IMPACC1_CR_Cust_abbr, P_txt_IMPACC1_CR_Cust_Acc, P_txt_IMPACC1_CR_Acceptance_Curr, P_txt_IMPACC1_CR_Acceptance_amt, P_txt_IMPACC1_CR_Acceptance_payer,
    P_txt_IMPACC1_CR_Interest_Curr, P_txt_IMPACC1_CR_Interest_amt, P_txt_IMPACC1_CR_Interest_payer,
    P_txt_IMPACC1_CR_Accept_Commission_Curr, P_txt_IMPACC1_CR_Accept_Commission_amt, P_txt_IMPACC1_CR_Accept_Commission_Payer,
    P_txt_IMPACC1_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC1_CR_Pay_Handle_Commission_amt, P_txt_IMPACC1_CR_Pay_Handle_Commission_Payer,
    P_txt_IMPACC1_CR_Others_Curr, P_txt_IMPACC1_CR_Others_amt, P_txt_IMPACC1_CR_Others_Payer,
    P_txt_IMPACC1_CR_Their_Commission_Curr, P_txt_IMPACC1_CR_Their_Commission_amt, P_txt_IMPACC1_CR_Their_Commission_Payer,
    P_txt_IMPACC1_DR_Code, P_txt_IMPACC1_DR_AC_Short_Name, P_txt_IMPACC1_DR_Cust_abbr, P_txt_IMPACC1_DR_Cust_Acc,
    P_txt_IMPACC1_DR_Cur_Acc_Curr, P_txt_IMPACC1_DR_Cur_Acc_amt, P_txt_IMPACC1_DR_Cur_Acc_payer,
    P_txt_IMPACC1_DR_Cur_Acc_Curr2, P_txt_IMPACC1_DR_Cur_Acc_amt2, P_txt_IMPACC1_DR_Cur_Acc_payer2,
    P_txt_IMPACC1_DR_Cur_Acc_Curr3, P_txt_IMPACC1_DR_Cur_Acc_amt3, P_txt_IMPACC1_DR_Cur_Acc_payer3,
    P_txt_IMPACC1_DR_Cur_Acc_Curr4, P_txt_IMPACC1_DR_Cur_Acc_amt4, P_txt_IMPACC1_DR_Cur_Acc_payer4,
    P_txt_IMPACC1_DR_Cur_Acc_Curr5, P_txt_IMPACC1_DR_Cur_Acc_amt5, P_txt_IMPACC1_DR_Cur_Acc_payer5,
    P_txt_IMPACC1_DR_Cur_Acc_Curr6, P_txt_IMPACC1_DR_Cur_Acc_amt6, P_txt_IMPACC1_DR_Cur_Acc_payer6,

    P_txt_IMPACC1_DR_Code2, P_txt_IMPACC1_DR_AC_Short_Name2, P_txt_IMPACC1_DR_Cust_abbr2, P_txt_IMPACC1_DR_Cust_Acc2,
    P_txt_IMPACC1_DR_Code3, P_txt_IMPACC1_DR_AC_Short_Name3, P_txt_IMPACC1_DR_Cust_abbr3, P_txt_IMPACC1_DR_Cust_Acc3,
    P_txt_IMPACC1_DR_Code4, P_txt_IMPACC1_DR_AC_Short_Name4, P_txt_IMPACC1_DR_Cust_abbr4, P_txt_IMPACC1_DR_Cust_Acc4,
    P_txt_IMPACC1_DR_Code5, P_txt_IMPACC1_DR_AC_Short_Name5, P_txt_IMPACC1_DR_Cust_abbr5, P_txt_IMPACC1_DR_Cust_Acc5,
    P_txt_IMPACC1_DR_Code6, P_txt_IMPACC1_DR_AC_Short_Name6, P_txt_IMPACC1_DR_Cust_abbr6, P_txt_IMPACC1_DR_Cust_Acc6,

    ////////////Import Accounting 2
    P_chk_IMPACC2Flag, P_txt_IMPACC2_FCRefNo,
    P_txt_IMPACC2_DiscAmt, P_txt_IMPACC2_DiscExchRate,
    P_txt_IMPACC2_Princ_matu, P_txt_IMPACC2_Princ_lump, P_txt_IMPACC2_Princ_Contract_no, P_txt_IMPACC2_Princ_Ex_Curr, P_txt_IMPACC2_Princ_Ex_rate, P_txt_IMPACC2_Princ_Intnl_Ex_rate,
    P_txt_IMPACC2_Interest_matu, P_txt_IMPACC2_Interest_lump, P_txt_IMPACC2_Interest_Contract_no, P_txt_IMPACC2_Interest_Ex_Curr, P_txt_IMPACC2_Interest_Ex_rate, P_txt_IMPACC2_Interest_Intnl_Ex_rate,
    P_txt_IMPACC2_Commission_matu, P_txt_IMPACC2_Commission_lump, P_txt_IMPACC2_Commission_Contract_no, P_txt_IMPACC2_Commission_Ex_Curr, P_txt_IMPACC2_Commission_Ex_rate, P_txt_IMPACC2_Commission_Intnl_Ex_rate,
    P_txt_IMPACC2_Their_Commission_matu, P_txt_IMPACC2_Their_Commission_lump, P_txt_IMPACC2_Their_Commission_Contract_no, P_txt_IMPACC2_Their_Commission_Ex_Curr, P_txt_IMPACC2_Their_Commission_Ex_rate, P_txt_IMPACC2_Their_Commission_Intnl_Ex_rate,
    P_txt_IMPACC2_CR_Code, P_txt_IMPACC2_CR_AC_Short_Name, P_txt_IMPACC2_CR_Cust_abbr, P_txt_IMPACC2_CR_Cust_Acc, P_txt_IMPACC2_CR_Acceptance_Curr, P_txt_IMPACC2_CR_Acceptance_amt, P_txt_IMPACC2_CR_Acceptance_payer,
    P_txt_IMPACC2_CR_Interest_Curr, P_txt_IMPACC2_CR_Interest_amt, P_txt_IMPACC2_CR_Interest_payer,
    P_txt_IMPACC2_CR_Accept_Commission_Curr, P_txt_IMPACC2_CR_Accept_Commission_amt, P_txt_IMPACC2_CR_Accept_Commission_Payer,
    P_txt_IMPACC2_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC2_CR_Pay_Handle_Commission_amt, P_txt_IMPACC2_CR_Pay_Handle_Commission_Payer,
    P_txt_IMPACC2_CR_Others_Curr, P_txt_IMPACC2_CR_Others_amt, P_txt_IMPACC2_CR_Others_Payer,
    P_txt_IMPACC2_CR_Their_Commission_Curr, P_txt_IMPACC2_CR_Their_Commission_amt, P_txt_IMPACC2_CR_Their_Commission_Payer,
    P_txt_IMPACC2_DR_Code, P_txt_IMPACC2_DR_AC_Short_Name, P_txt_IMPACC2_DR_Cust_abbr, P_txt_IMPACC2_DR_Cust_Acc,
    P_txt_IMPACC2_DR_Cur_Acc_Curr, P_txt_IMPACC2_DR_Cur_Acc_amt, P_txt_IMPACC2_DR_Cur_Acc_payer,
    P_txt_IMPACC2_DR_Cur_Acc_Curr2, P_txt_IMPACC2_DR_Cur_Acc_amt2, P_txt_IMPACC2_DR_Cur_Acc_payer2,
    P_txt_IMPACC2_DR_Cur_Acc_Curr3, P_txt_IMPACC2_DR_Cur_Acc_amt3, P_txt_IMPACC2_DR_Cur_Acc_payer3,
    P_txt_IMPACC2_DR_Cur_Acc_Curr4, P_txt_IMPACC2_DR_Cur_Acc_amt4, P_txt_IMPACC2_DR_Cur_Acc_payer4,
    P_txt_IMPACC2_DR_Cur_Acc_Curr5, P_txt_IMPACC2_DR_Cur_Acc_amt5, P_txt_IMPACC2_DR_Cur_Acc_payer5,
    P_txt_IMPACC2_DR_Cur_Acc_Curr6, P_txt_IMPACC2_DR_Cur_Acc_amt6, P_txt_IMPACC2_DR_Cur_Acc_payer6,

    P_txt_IMPACC2_DR_Code2, P_txt_IMPACC2_DR_AC_Short_Name2, P_txt_IMPACC2_DR_Cust_abbr2, P_txt_IMPACC2_DR_Cust_Acc2,
    P_txt_IMPACC2_DR_Code3, P_txt_IMPACC2_DR_AC_Short_Name3, P_txt_IMPACC2_DR_Cust_abbr3, P_txt_IMPACC2_DR_Cust_Acc3,
    P_txt_IMPACC2_DR_Code4, P_txt_IMPACC2_DR_AC_Short_Name4, P_txt_IMPACC2_DR_Cust_abbr4, P_txt_IMPACC2_DR_Cust_Acc4,
    P_txt_IMPACC2_DR_Code5, P_txt_IMPACC2_DR_AC_Short_Name5, P_txt_IMPACC2_DR_Cust_abbr5, P_txt_IMPACC2_DR_Cust_Acc5,
    P_txt_IMPACC2_DR_Code6, P_txt_IMPACC2_DR_AC_Short_Name6, P_txt_IMPACC2_DR_Cust_abbr6, P_txt_IMPACC2_DR_Cust_Acc6,

    ////////////Import Accounting 3
    P_chk_IMPACC3Flag, P_txt_IMPACC3_FCRefNo,
    P_txt_IMPACC3_DiscAmt, P_txt_IMPACC3_DiscExchRate,
    P_txt_IMPACC3_Princ_matu, P_txt_IMPACC3_Princ_lump, P_txt_IMPACC3_Princ_Contract_no, P_txt_IMPACC3_Princ_Ex_Curr, P_txt_IMPACC3_Princ_Ex_rate, P_txt_IMPACC3_Princ_Intnl_Ex_rate,
    P_txt_IMPACC3_Interest_matu, P_txt_IMPACC3_Interest_lump, P_txt_IMPACC3_Interest_Contract_no, P_txt_IMPACC3_Interest_Ex_Curr, P_txt_IMPACC3_Interest_Ex_rate, P_txt_IMPACC3_Interest_Intnl_Ex_rate,
    P_txt_IMPACC3_Commission_matu, P_txt_IMPACC3_Commission_lump, P_txt_IMPACC3_Commission_Contract_no, P_txt_IMPACC3_Commission_Ex_Curr, P_txt_IMPACC3_Commission_Ex_rate, P_txt_IMPACC3_Commission_Intnl_Ex_rate,
    P_txt_IMPACC3_Their_Commission_matu, P_txt_IMPACC3_Their_Commission_lump, P_txt_IMPACC3_Their_Commission_Contract_no, P_txt_IMPACC3_Their_Commission_Ex_Curr, P_txt_IMPACC3_Their_Commission_Ex_rate, P_txt_IMPACC3_Their_Commission_Intnl_Ex_rate,
    P_txt_IMPACC3_CR_Code, P_txt_IMPACC3_CR_AC_Short_Name, P_txt_IMPACC3_CR_Cust_abbr, P_txt_IMPACC3_CR_Cust_Acc, P_txt_IMPACC3_CR_Acceptance_Curr, P_txt_IMPACC3_CR_Acceptance_amt, P_txt_IMPACC3_CR_Acceptance_payer,
    P_txt_IMPACC3_CR_Interest_Curr, P_txt_IMPACC3_CR_Interest_amt, P_txt_IMPACC3_CR_Interest_payer,
    P_txt_IMPACC3_CR_Accept_Commission_Curr, P_txt_IMPACC3_CR_Accept_Commission_amt, P_txt_IMPACC3_CR_Accept_Commission_Payer,
    P_txt_IMPACC3_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC3_CR_Pay_Handle_Commission_amt, P_txt_IMPACC3_CR_Pay_Handle_Commission_Payer,
    P_txt_IMPACC3_CR_Others_Curr, P_txt_IMPACC3_CR_Others_amt, P_txt_IMPACC3_CR_Others_Payer,
    P_txt_IMPACC3_CR_Their_Commission_Curr, P_txt_IMPACC3_CR_Their_Commission_amt, P_txt_IMPACC3_CR_Their_Commission_Payer,
    P_txt_IMPACC3_DR_Code, P_txt_IMPACC3_DR_AC_Short_Name, P_txt_IMPACC3_DR_Cust_abbr, P_txt_IMPACC3_DR_Cust_Acc,
    P_txt_IMPACC3_DR_Cur_Acc_Curr, P_txt_IMPACC3_DR_Cur_Acc_amt, P_txt_IMPACC3_DR_Cur_Acc_payer,
    P_txt_IMPACC3_DR_Cur_Acc_Curr2, P_txt_IMPACC3_DR_Cur_Acc_amt2, P_txt_IMPACC3_DR_Cur_Acc_payer2,
    P_txt_IMPACC3_DR_Cur_Acc_Curr3, P_txt_IMPACC3_DR_Cur_Acc_amt3, P_txt_IMPACC3_DR_Cur_Acc_payer3,
    P_txt_IMPACC3_DR_Cur_Acc_Curr4, P_txt_IMPACC3_DR_Cur_Acc_amt4, P_txt_IMPACC3_DR_Cur_Acc_payer4,
    P_txt_IMPACC3_DR_Cur_Acc_Curr5, P_txt_IMPACC3_DR_Cur_Acc_amt5, P_txt_IMPACC3_DR_Cur_Acc_payer5,
    P_txt_IMPACC3_DR_Cur_Acc_Curr6, P_txt_IMPACC3_DR_Cur_Acc_amt6, P_txt_IMPACC3_DR_Cur_Acc_payer6,

    P_txt_IMPACC3_DR_Code2, P_txt_IMPACC3_DR_AC_Short_Name2, P_txt_IMPACC3_DR_Cust_abbr2, P_txt_IMPACC3_DR_Cust_Acc2,
    P_txt_IMPACC3_DR_Code3, P_txt_IMPACC3_DR_AC_Short_Name3, P_txt_IMPACC3_DR_Cust_abbr3, P_txt_IMPACC3_DR_Cust_Acc3,
    P_txt_IMPACC3_DR_Code4, P_txt_IMPACC3_DR_AC_Short_Name4, P_txt_IMPACC3_DR_Cust_abbr4, P_txt_IMPACC3_DR_Cust_Acc4,
    P_txt_IMPACC3_DR_Code5, P_txt_IMPACC3_DR_AC_Short_Name5, P_txt_IMPACC3_DR_Cust_abbr5, P_txt_IMPACC3_DR_Cust_Acc5,
    P_txt_IMPACC3_DR_Code6, P_txt_IMPACC3_DR_AC_Short_Name6, P_txt_IMPACC3_DR_Cust_abbr6, P_txt_IMPACC3_DR_Cust_Acc6,

    ////////Import accounting 4
    P_chk_IMPACC4Flag, P_txt_IMPACC4_FCRefNo,
    P_txt_IMPACC4_DiscAmt, P_txt_IMPACC4_DiscExchRate,
    P_txt_IMPACC4_Princ_matu, P_txt_IMPACC4_Princ_lump, P_txt_IMPACC4_Princ_Contract_no, P_txt_IMPACC4_Princ_Ex_Curr, P_txt_IMPACC4_Princ_Ex_rate, P_txt_IMPACC4_Princ_Intnl_Ex_rate,
    P_txt_IMPACC4_Interest_matu, P_txt_IMPACC4_Interest_lump, P_txt_IMPACC4_Interest_Contract_no, P_txt_IMPACC4_Interest_Ex_Curr, P_txt_IMPACC4_Interest_Ex_rate, P_txt_IMPACC4_Interest_Intnl_Ex_rate,
    P_txt_IMPACC4_Commission_matu, P_txt_IMPACC4_Commission_lump, P_txt_IMPACC4_Commission_Contract_no, P_txt_IMPACC4_Commission_Ex_Curr, P_txt_IMPACC4_Commission_Ex_rate, P_txt_IMPACC4_Commission_Intnl_Ex_rate,
    P_txt_IMPACC4_Their_Commission_matu, P_txt_IMPACC4_Their_Commission_lump, P_txt_IMPACC4_Their_Commission_Contract_no, P_txt_IMPACC4_Their_Commission_Ex_Curr, P_txt_IMPACC4_Their_Commission_Ex_rate, P_txt_IMPACC4_Their_Commission_Intnl_Ex_rate,
    P_txt_IMPACC4_CR_Code, P_txt_IMPACC4_CR_AC_Short_Name, P_txt_IMPACC4_CR_Cust_abbr, P_txt_IMPACC4_CR_Cust_Acc, P_txt_IMPACC4_CR_Acceptance_Curr, P_txt_IMPACC4_CR_Acceptance_amt, P_txt_IMPACC4_CR_Acceptance_payer,
    P_txt_IMPACC4_CR_Interest_Curr, P_txt_IMPACC4_CR_Interest_amt, P_txt_IMPACC4_CR_Interest_payer,
    P_txt_IMPACC4_CR_Accept_Commission_Curr, P_txt_IMPACC4_CR_Accept_Commission_amt, P_txt_IMPACC4_CR_Accept_Commission_Payer,
    P_txt_IMPACC4_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC4_CR_Pay_Handle_Commission_amt, P_txt_IMPACC4_CR_Pay_Handle_Commission_Payer,
    P_txt_IMPACC4_CR_Others_Curr, P_txt_IMPACC4_CR_Others_amt, P_txt_IMPACC4_CR_Others_Payer,
    P_txt_IMPACC4_CR_Their_Commission_Curr, P_txt_IMPACC4_CR_Their_Commission_amt, P_txt_IMPACC4_CR_Their_Commission_Payer,
    P_txt_IMPACC4_DR_Code, P_txt_IMPACC4_DR_AC_Short_Name, P_txt_IMPACC4_DR_Cust_abbr, P_txt_IMPACC4_DR_Cust_Acc,
    P_txt_IMPACC4_DR_Cur_Acc_Curr, P_txt_IMPACC4_DR_Cur_Acc_amt, P_txt_IMPACC4_DR_Cur_Acc_payer,
    P_txt_IMPACC4_DR_Cur_Acc_Curr2, P_txt_IMPACC4_DR_Cur_Acc_amt2, P_txt_IMPACC4_DR_Cur_Acc_payer2,
    P_txt_IMPACC4_DR_Cur_Acc_Curr3, P_txt_IMPACC4_DR_Cur_Acc_amt3, P_txt_IMPACC4_DR_Cur_Acc_payer3,
    P_txt_IMPACC4_DR_Cur_Acc_Curr4, P_txt_IMPACC4_DR_Cur_Acc_amt4, P_txt_IMPACC4_DR_Cur_Acc_payer4,
    P_txt_IMPACC4_DR_Cur_Acc_Curr5, P_txt_IMPACC4_DR_Cur_Acc_amt5, P_txt_IMPACC4_DR_Cur_Acc_payer5,
    P_txt_IMPACC4_DR_Cur_Acc_Curr6, P_txt_IMPACC4_DR_Cur_Acc_amt6, P_txt_IMPACC4_DR_Cur_Acc_payer6,

    P_txt_IMPACC4_DR_Code2, P_txt_IMPACC4_DR_AC_Short_Name2, P_txt_IMPACC4_DR_Cust_abbr2, P_txt_IMPACC4_DR_Cust_Acc2,
    P_txt_IMPACC4_DR_Code3, P_txt_IMPACC4_DR_AC_Short_Name3, P_txt_IMPACC4_DR_Cust_abbr3, P_txt_IMPACC4_DR_Cust_Acc3,
    P_txt_IMPACC4_DR_Code4, P_txt_IMPACC4_DR_AC_Short_Name4, P_txt_IMPACC4_DR_Cust_abbr4, P_txt_IMPACC4_DR_Cust_Acc4,
    P_txt_IMPACC4_DR_Code5, P_txt_IMPACC4_DR_AC_Short_Name5, P_txt_IMPACC4_DR_Cust_abbr5, P_txt_IMPACC4_DR_Cust_Acc5,
    P_txt_IMPACC4_DR_Code6, P_txt_IMPACC4_DR_AC_Short_Name6, P_txt_IMPACC4_DR_Cust_abbr6, P_txt_IMPACC4_DR_Cust_Acc6,

    /////////import accounting 5
    P_chk_IMPACC5Flag, P_txt_IMPACC5_FCRefNo,
    P_txt_IMPACC5_DiscAmt, P_txt_IMPACC5_DiscExchRate,
    P_txt_IMPACC5_Princ_matu, P_txt_IMPACC5_Princ_lump, P_txt_IMPACC5_Princ_Contract_no, P_txt_IMPACC5_Princ_Ex_Curr, P_txt_IMPACC5_Princ_Ex_rate, P_txt_IMPACC5_Princ_Intnl_Ex_rate,
    P_txt_IMPACC5_Interest_matu, P_txt_IMPACC5_Interest_lump, P_txt_IMPACC5_Interest_Contract_no, P_txt_IMPACC5_Interest_Ex_Curr, P_txt_IMPACC5_Interest_Ex_rate, P_txt_IMPACC5_Interest_Intnl_Ex_rate,
    P_txt_IMPACC5_Commission_matu, P_txt_IMPACC5_Commission_lump, P_txt_IMPACC5_Commission_Contract_no, P_txt_IMPACC5_Commission_Ex_Curr, P_txt_IMPACC5_Commission_Ex_rate, P_txt_IMPACC5_Commission_Intnl_Ex_rate,
    P_txt_IMPACC5_Their_Commission_matu, P_txt_IMPACC5_Their_Commission_lump, P_txt_IMPACC5_Their_Commission_Contract_no, P_txt_IMPACC5_Their_Commission_Ex_Curr, P_txt_IMPACC5_Their_Commission_Ex_rate, P_txt_IMPACC5_Their_Commission_Intnl_Ex_rate,
    P_txt_IMPACC5_CR_Code, P_txt_IMPACC5_CR_AC_Short_Name, P_txt_IMPACC5_CR_Cust_abbr, P_txt_IMPACC5_CR_Cust_Acc, P_txt_IMPACC5_CR_Acceptance_Curr, P_txt_IMPACC5_CR_Acceptance_amt, P_txt_IMPACC5_CR_Acceptance_payer,
    P_txt_IMPACC5_CR_Interest_Curr, P_txt_IMPACC5_CR_Interest_amt, P_txt_IMPACC5_CR_Interest_payer,
    P_txt_IMPACC5_CR_Accept_Commission_Curr, P_txt_IMPACC5_CR_Accept_Commission_amt, P_txt_IMPACC5_CR_Accept_Commission_Payer,
    P_txt_IMPACC5_CR_Pay_Handle_Commission_Curr, P_txt_IMPACC5_CR_Pay_Handle_Commission_amt, P_txt_IMPACC5_CR_Pay_Handle_Commission_Payer,
    P_txt_IMPACC5_CR_Others_Curr, P_txt_IMPACC5_CR_Others_amt, P_txt_IMPACC5_CR_Others_Payer,
    P_txt_IMPACC5_CR_Their_Commission_Curr, P_txt_IMPACC5_CR_Their_Commission_amt, P_txt_IMPACC5_CR_Their_Commission_Payer,
    P_txt_IMPACC5_DR_Code, P_txt_IMPACC5_DR_AC_Short_Name, P_txt_IMPACC5_DR_Cust_abbr, P_txt_IMPACC5_DR_Cust_Acc,
    P_txt_IMPACC5_DR_Cur_Acc_Curr, P_txt_IMPACC5_DR_Cur_Acc_amt, P_txt_IMPACC5_DR_Cur_Acc_payer,
    P_txt_IMPACC5_DR_Cur_Acc_Curr2, P_txt_IMPACC5_DR_Cur_Acc_amt2, P_txt_IMPACC5_DR_Cur_Acc_payer2,
    P_txt_IMPACC5_DR_Cur_Acc_Curr3, P_txt_IMPACC5_DR_Cur_Acc_amt3, P_txt_IMPACC5_DR_Cur_Acc_payer3,
    P_txt_IMPACC5_DR_Cur_Acc_Curr4, P_txt_IMPACC5_DR_Cur_Acc_amt4, P_txt_IMPACC5_DR_Cur_Acc_payer4,
    P_txt_IMPACC5_DR_Cur_Acc_Curr5, P_txt_IMPACC5_DR_Cur_Acc_amt5, P_txt_IMPACC5_DR_Cur_Acc_payer5,
    P_txt_IMPACC5_DR_Cur_Acc_Curr6, P_txt_IMPACC5_DR_Cur_Acc_amt6, P_txt_IMPACC5_DR_Cur_Acc_payer6,

    P_txt_IMPACC5_DR_Code2, P_txt_IMPACC5_DR_AC_Short_Name2, P_txt_IMPACC5_DR_Cust_abbr2, P_txt_IMPACC5_DR_Cust_Acc2,
    P_txt_IMPACC5_DR_Code3, P_txt_IMPACC5_DR_AC_Short_Name3, P_txt_IMPACC5_DR_Cust_abbr3, P_txt_IMPACC5_DR_Cust_Acc3,
    P_txt_IMPACC5_DR_Code4, P_txt_IMPACC5_DR_AC_Short_Name4, P_txt_IMPACC5_DR_Cust_abbr4, P_txt_IMPACC5_DR_Cust_Acc4,
    P_txt_IMPACC5_DR_Code5, P_txt_IMPACC5_DR_AC_Short_Name5, P_txt_IMPACC5_DR_Cust_abbr5, P_txt_IMPACC5_DR_Cust_Acc5,
    P_txt_IMPACC5_DR_Code6, P_txt_IMPACC5_DR_AC_Short_Name6, P_txt_IMPACC5_DR_Cust_abbr6, P_txt_IMPACC5_DR_Cust_Acc6);

                //-------------------------------- General Operations --------------------------------------
                _resultGO = objSave.SaveDeleteData(queryGO, mode, branch, docno, srno, GO_Flag,
      GO_ValueDate,
                //GO_Ref_No,
                                             GO_Remark, GO_Section, GO_Comment, GO_Memo, GO_SchemeNo,
                //------------------------- Debit ------------------------------
                                             GO_Debit, GO_Debit_CCY, GO_Debit_Amt, GO_Debit_Cust, GO_Debit_Cust_Name, GO_Debit_Cust_AcCode, GO_Debit_Cust_AcCode_Name, GO_Debit_Cust_AccNo, GO_Debit_ExchRate, GO_Debit_ExchCCY, GO_Debit_Fund,
                                             GO_Debit_CheckNo, GO_Debit_Available, GO_Debit_Advice_Print, GO_Debit_Details, GO_Debit_Entity, GO_Debit_Division, GO_Debit_InterAmt, GO_Debit_InterRate,
                //------------------------- Credit ------------------------------
                                             GO_Credit, GO_Credit_Amt, GO_Credit_CCY, GO_Credit_Cust, GO_Credit_Cust_Name, GO_Credit_Cust_AcCode, GO_Credit_Cust_AcCode_Name, GO_Credit_Cust_AccNo, GO_Credit_ExchCCY, GO_Credit_ExchRate, GO_Credit_Fund,
                                             GO_Credit_CheckNo, GO_Credit_Available, GO_Credit_Advice_Print, GO_Credit_Details, GO_Credit_Entity, GO_Credit_Division, GO_Credit_InterAmt, GO_Credit_InterRate,
                //-------------------------------- Normal General Operations --------------------------------------
                                            NGO_Flag,
                NGO_ValueDate,
                //NGO_Ref_No, 
                                             NGO_Remark, NGO_Section, NGO_Comment, NGO_Memo, NGO_SchemeNo,
                //------------------------- Debit ------------------------------
                                             NGO_Debit, NGO_Debit_CCY, NGO_Debit_Amt, NGO_Debit_Cust, NGO_Debit_Cust_Name, NGO_Debit_Cust_AcCode, NGO_Debit_Cust_AcCode_Name, NGO_Debit_Cust_AccNo, NGO_Debit_ExchRate, NGO_Debit_ExchCCY, NGO_Debit_Fund,
                                             NGO_Debit_CheckNo, NGO_Debit_Available, NGO_Debit_Advice_Print, NGO_Debit_Details, NGO_Debit_Entity, NGO_Debit_Division, NGO_Debit_InterAmt, NGO_Debit_InterRate,
                //------------------------- Credit ------------------------------
                                             NGO_Credit, NGO_Credit_Amt, NGO_Credit_CCY, NGO_Credit_Cust, NGO_Credit_Cust_Name, NGO_Credit_Cust_AcCode, NGO_Credit_Cust_AcCode_Name, NGO_Credit_Cust_AccNo, NGO_Credit_ExchCCY, NGO_Credit_ExchRate, NGO_Credit_Fund,
                                             NGO_Credit_CheckNo, NGO_Credit_Available, NGO_Credit_Advice_Print, NGO_Credit_Details, NGO_Credit_Entity, NGO_Credit_Division, NGO_Credit_InterAmt, NGO_Credit_InterRate,


    //-------------------------------------------END-----------------------------------------------
    //---------------------------------------------------Anand 04-07-2023-----------------------------------
    P_txt_GOAccChange_Flag,
                P_txt_GOAccChange_ValueDate, P_txt_GOAccChange_Ref_No,
    P_txt_GOAccChange_Comment,
    P_txt_GOAccChange_SectionNo, P_txt_GOAccChange_Remarks, P_txt_GOAccChange_Memo,
    P_txt_GOAccChange_Scheme_no,
    P_txt_GOAccChange_Debit_Code, P_txt_GOAccChange_Debit_Curr, P_txt_GOAccChange_Debit_Amt,
    P_txt_GOAccChange_Debit_Cust, P_txt_GOAccChange_Debit_Cust_Name,
    P_txt_GOAccChange_Debit_Cust_AcCode, P_txt_GOAccChange_Debit_Cust_AcCode_Name, P_txt_GOAccChange_Debit_Cust_AccNo,
    P_txt_GOAccChange_Debit_Exch_Rate, P_txt_GOAccChange_Debit_Exch_CCY,
    P_txt_GOAccChange_Debit_FUND, P_txt_GOAccChange_Debit_Check_No, P_txt_GOAccChange_Debit_Available,
    P_txt_GOAccChange_Debit_AdPrint, P_txt_GOAccChange_Debit_Details, P_txt_GOAccChange_Debit_Entity,
    P_txt_GOAccChange_Debit_Division, P_txt_GOAccChange_Debit_Inter_Amount, P_txt_GOAccChange_Debit_Inter_Rate,
    P_txt_GOAccChange_Credit_Code, P_txt_GOAccChange_Credit_Curr, P_txt_GOAccChange_Credit_Amt,
    P_txt_GOAccChange_Credit_Cust, P_txt_GOAccChange_Credit_Cust_Name,
    P_txt_GOAccChange_Credit_Cust_AcCode, P_txt_GOAccChange_Credit_Cust_AcCode_Name, P_txt_GOAccChange_Credit_Cust_AccNo,
    P_txt_GOAccChange_Credit_Exch_Rate, P_txt_GOAccChange_Credit_Exch_CCY,
    P_txt_GOAccChange_Credit_FUND, P_txt_GOAccChange_Credit_Check_No, P_txt_GOAccChange_Credit_Available,
    P_txt_GOAccChange_Credit_AdPrint, P_txt_GOAccChange_Credit_Details, P_txt_GOAccChange_Credit_Entity,
    P_txt_GOAccChange_Credit_Division, P_txt_GOAccChange_Credit_Inter_Amount, P_txt_GOAccChange_Credit_Inter_Rate, p1, p2
                //-------------------------------------------------------end-----------------------------------------
                                             );


                string _resultIRM = "";

                if (chkIRMCreate.Checked == true)
                {
                    _resultIRM = objData.SaveDeleteData("TF_EXP_IRM_Realisation_AddUpdate", branch, docno, srno, realdate, custacno, pIPurposeCode, CrossrealCur, realamt, mode, exrate, realamtinr,
     FIRC_NO, FIRC_AD_CODE, opc, RemiName, RemiCotry, valuedate, RemiAdd, pRB, pRBA, p20, pRBC, p1, p2, PSwiftCode,
                        pIBUTransID, pIIFSCCode, pIRemiADcode, pIIECcode, pIPanNo, pIModeOfPay, pBankReferencenumber, pBankAccountNumber, pIRMStatus, CrossrealAmt);
                }


                string _resultShippingBill = "";
                string _queryShippingBill = "TF_EXP_IRM_ShippingBill_Realisation_AddUpdate";

                SqlParameter ShippingbillbCode = new SqlParameter("@branch", Request.QueryString["BranchCode"].Trim());
                SqlParameter Docsrno = new SqlParameter("@SrNo", txtSrNo.Text);
                Label lblShipping_Bill_No = new Label();
                SqlParameter pShipping_Bill_No = new SqlParameter("@Shipping_Bill_No", SqlDbType.VarChar);
                Label lblInvoiceNo = new Label();
                SqlParameter pInvoiceNo = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                Label lblDocument_No = new Label();
                SqlParameter pDocument_No = new SqlParameter("@docno", SqlDbType.VarChar);
                Label lblShipCurr = new Label();
                SqlParameter pShipCurr = new SqlParameter("@ShipCurr", SqlDbType.VarChar);
                Label lblFOBAmount = new Label();
                SqlParameter pFOBAmount = new SqlParameter("@FOBAmount", SqlDbType.VarChar);
                Label lblInsuranceAmount = new Label();
                SqlParameter pInsuranceAmount = new SqlParameter("@InsuranceAmount", SqlDbType.VarChar);
                Label lblFreightAmount = new Label();
                SqlParameter pFreightAmount = new SqlParameter("@FreightAmount", SqlDbType.VarChar);
                Label lblAmount = new Label();
                SqlParameter pAmount = new SqlParameter("@Amount", SqlDbType.VarChar);
                DropDownList ddlpartfull = new DropDownList();
                SqlParameter ppartfull = new SqlParameter("@Indicator_PartFull", SqlDbType.VarChar);
                TextBox txtsettelementamt = new TextBox();
                SqlParameter psettelementamt = new SqlParameter("@SettlementAmt", SqlDbType.VarChar);
                TextBox txtFBANK = new TextBox();
                SqlParameter pFBANK = new SqlParameter("@FBANK", SqlDbType.VarChar);

                for (
                    int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
                {


                    CheckBox chkrow = (CheckBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("RowChkAllow");
                    if (chkrow.Checked == true)
                    {
                        lblShipping_Bill_No = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblShipping_Bill_No");
                        lblInvoiceNo = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblInvoiceNo");
                        lblDocument_No = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblDocument_No");
                        lblShipCurr = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblShipCurr");
                        lblFOBAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblFOBAmount");
                        lblInsuranceAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblInsuranceAmount");
                        lblFreightAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblFreightAmount");
                        lblAmount = (Label)GridViewGRPPCustomsDetails.Rows[i].FindControl("lblAmount");
                        ddlpartfull = (DropDownList)GridViewGRPPCustomsDetails.Rows[i].FindControl("ddlpartfull");
                        txtsettelementamt = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtsettelementamt");
                        txtFBANK = (TextBox)GridViewGRPPCustomsDetails.Rows[i].FindControl("txtFBANK");

                        pShipping_Bill_No.Value = lblShipping_Bill_No.Text;
                        pInvoiceNo.Value = lblInvoiceNo.Text;
                        pDocument_No.Value = lblDocument_No.Text;
                        pShipCurr.Value = lblShipCurr.Text;
                        pFOBAmount.Value = lblFOBAmount.Text;
                        pInsuranceAmount.Value = lblInsuranceAmount.Text;
                        pFreightAmount.Value = lblFreightAmount.Text;
                        pAmount.Value = lblAmount.Text;
                        ppartfull.Value = ddlpartfull.Text;
                        psettelementamt.Value = txtsettelementamt.Text;
                        pFBANK.Value = txtFBANK.Text;


                        _resultShippingBill = objData.SaveDeleteData(_queryShippingBill, ShippingbillbCode, Docsrno, pShipping_Bill_No, pInvoiceNo, pDocument_No, pFOBAmount, pInsuranceAmount,
                            pFreightAmount, pAmount, ppartfull, psettelementamt, pFBANK, mode, p1, p2, CrossrealCur, RelCrossCurRate, pShipCurr);
                    }

                }

                //=========================END====================================//
                if (_result.Substring(0, 5) == "added")
                {
                    txtDocNo.Text = "";
                }
                string _script = "";

                var argument = ((Button)sender).CommandArgument;

                if (_result.Substring(0, 5) == "added")
                // if (_result == "added")
                {
                    if (argument.ToString() == "print")
                    {
                        _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                    else
                    {
                        _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=save&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
                else
                {
                    if (_result == "updated")
                    {
                        if (argument.ToString() == "print")
                        {
                            _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                        else
                        {
                            _script = "window.location='EXP_ViewRealisationEntry_Maker.aspx?result=" + _result + "&saveType=save&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }
                    else
                        labelMessage.Text = _result;
                }
                ////////////////////////////////////////////////////////////////////////////////END/////////////////////////////////////////////////////////////////////////////

            }
            else if (_result == "Document fully realised")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Document alredy realised')", true);
            }
        }
    }

    //----------------------------------------------------Nilesh chngeing END----------------------------------------------


    //------------------------------------------------------Anand 26/06/2023----------------------------------------------

    protected void chk_IMPACC1Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chk_IMPACC1Flag.Checked = false;
        }
        else
        {
            if (chk_IMPACC1Flag.Checked == false)
            {
                ClearExportAcEntry("ACC1");
                PanelIMPACC1.Visible = false;
                txt_IMPACC1_DiscAmt.Text = "";
                txt_IMPACC1_DR_Cur_Acc_Curr.Text = "";
                txt_IMPACC1_DR_Cur_Acc_amt.Text = "0";
            }
            else if (chk_IMPACC1Flag.Checked == true)
            {
                PanelIMPACC1.Visible = true;
                txt_IMPACC1_DiscAmt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_Curr.Text = txt_relcur.Text;
                txt_IMPACC1_DR_Cur_Acc_amt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");
                branchcode = Request.QueryString["BranchCode"].ToString();
                if (branchcode == "793")
                {
                    txt_IMPACC1_DR_Cust_abbr.Text = "MHCB MB";
                    txt_IMPACC1_DR_AC_Short_Name.Text = "INTEROFFICE A/C MUMBAI BR.,";
                    txt_IMPACC1_DR_Code.Text = "81763";
                    txt_IMPACC1_DR_Cust_Acc.Text = "B79-793-111111";
                }
                if (branchcode == "716")
                {
                    txt_IMPACC1_DR_Cust_abbr.Text = "MHCB MB";
                    txt_IMPACC1_DR_AC_Short_Name.Text = "INTEROFFICE A/C MUMBAI BR.,";
                    txt_IMPACC1_DR_Code.Text = "81763";
                    txt_IMPACC1_DR_Cust_Acc.Text = "B79-716-111111";
                }
                if (branchcode == "717")
                {
                    txt_IMPACC1_DR_Cust_abbr.Text = "MHCB MB";
                    txt_IMPACC1_DR_AC_Short_Name.Text = "INTEROFFICE A/C MUMBAI BR.,";
                    txt_IMPACC1_DR_Code.Text = "81763";
                    txt_IMPACC1_DR_Cust_Acc.Text = "B79-717-111111";
                }
                DocPrFx = Request.QueryString["DocPrFx"].ToString();
                if (DocPrFx != "BCU" && DocPrFx != "BCA")
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
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Cust Abbr not available for this A/c.')", true);
                    }

                    string REFNO = txtDocNo.Text.Substring(8, 6);
                    txt_IMPACC1_CR_Cust_abbr.Text = CustAbbrLEI;
                    txt_IMPACC1_CR_AC_Short_Name.Text = "BILLS BOUGHT";
                    txt_IMPACC1_CR_Code.Text = "10005";
                    txt_IMPACC1_CR_Cust_Acc.Text = DocPrFx + "-" + REFNO;
                    txt_IMPACC1_CR_Acceptance_Curr.Text = txt_relcur.Text;
                    txt_IMPACC1_CR_Acceptance_amt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");
                }
            }
        }
    }
    protected void chk_IMPACC2Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chk_IMPACC2Flag.Checked = false;
        }
        else
        {
            if (chk_IMPACC2Flag.Checked == false)
            {
                ClearExportAcEntry("ACC2");
                PanelIMPACC2.Visible = false;
                txt_IMPACC2_DiscAmt.Text = "";
            }
            else if (chk_IMPACC2Flag.Checked == true)
            {
                PanelIMPACC2.Visible = true;
                float Amt1 = 0;
                if (txt_IMPACC1_DiscAmt.Text != "" && txt_IMPACC1_DiscAmt.Text != "0")
                {
                    Amt1 = float.Parse(txt_IMPACC1_DiscAmt.Text);
                }
                txt_IMPACC2_DiscAmt.Text = Convert.ToDecimal((float.Parse(txtAmtRealised.Text) - Amt1)).ToString("0.00");
                if (txt_IMPACC2_DiscAmt.Text.Contains("-"))
                {
                    txt_IMPACC2_DiscAmt.Text = "0.00";
                }
            }
        }
    }
    protected void chk_IMPACC3Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chk_IMPACC3Flag.Checked = false;
        }
        else
        {
            if (chk_IMPACC3Flag.Checked == false)
            {
                ClearExportAcEntry("ACC3");
                PanelIMPACC3.Visible = false;
                txt_IMPACC3_DiscAmt.Text = "";
            }
            else if (chk_IMPACC3Flag.Checked == true)
            {
                PanelIMPACC3.Visible = true;
                float Amt1 = 0; float Amt2 = 0;
                if (txt_IMPACC1_DiscAmt.Text != "" && txt_IMPACC1_DiscAmt.Text != "0")
                {
                    Amt1 = float.Parse(txt_IMPACC1_DiscAmt.Text);
                }
                if (txt_IMPACC2_DiscAmt.Text != "" && txt_IMPACC2_DiscAmt.Text != "0")
                {
                    Amt2 = float.Parse(txt_IMPACC2_DiscAmt.Text);
                }
                txt_IMPACC3_DiscAmt.Text = Convert.ToDecimal((float.Parse(txtAmtRealised.Text) - Amt1 - Amt2)).ToString("0.00");
                if (txt_IMPACC3_DiscAmt.Text.Contains("-"))
                {
                    txt_IMPACC3_DiscAmt.Text = "0.00";
                }
            }
        }
    }
    protected void chk_IMPACC4Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chk_IMPACC4Flag.Checked = false;
        }
        else
        {
            if (chk_IMPACC4Flag.Checked == false)
            {
                ClearExportAcEntry("ACC4");
                PanelIMPACC4.Visible = false;
                txt_IMPACC4_DiscAmt.Text = "";
            }
            else if (chk_IMPACC4Flag.Checked == true)
            {
                PanelIMPACC4.Visible = true;
                float Amt1 = 0; float Amt2 = 0; float Amt3 = 0;
                if (txt_IMPACC1_DiscAmt.Text != "" && txt_IMPACC1_DiscAmt.Text != "0")
                {
                    Amt1 = float.Parse(txt_IMPACC1_DiscAmt.Text);
                }
                if (txt_IMPACC2_DiscAmt.Text != "" && txt_IMPACC2_DiscAmt.Text != "0")
                {
                    Amt2 = float.Parse(txt_IMPACC2_DiscAmt.Text);
                }
                if (txt_IMPACC3_DiscAmt.Text != "" && txt_IMPACC3_DiscAmt.Text != "0")
                {
                    Amt3 = float.Parse(txt_IMPACC3_DiscAmt.Text);
                }
                txt_IMPACC4_DiscAmt.Text = Convert.ToDecimal((float.Parse(txtAmtRealised.Text) - Amt1 - Amt2 - Amt3)).ToString("0.00");
                if (txt_IMPACC4_DiscAmt.Text.Contains("-"))
                {
                    txt_IMPACC4_DiscAmt.Text = "0.00";
                }
            }
        }
    }
    protected void chk_IMPACC5Flag_OnCheckedChanged(object sender, EventArgs e)
    {
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chk_IMPACC5Flag.Checked = false;
        }
        else
        {
            if (chk_IMPACC5Flag.Checked == false)
            {
                ClearExportAcEntry("ACC5");
                PanelIMPACC5.Visible = false;
                txt_IMPACC5_DiscAmt.Text = "";
            }
            else if (chk_IMPACC5Flag.Checked == true)
            {
                PanelIMPACC5.Visible = true;
                float Amt1 = 0; float Amt2 = 0; float Amt3 = 0; float Amt4 = 0;
                if (txt_IMPACC1_DiscAmt.Text != "" && txt_IMPACC1_DiscAmt.Text != "0")
                {
                    Amt1 = float.Parse(txt_IMPACC1_DiscAmt.Text);
                }
                if (txt_IMPACC2_DiscAmt.Text != "" && txt_IMPACC2_DiscAmt.Text != "0")
                {
                    Amt2 = float.Parse(txt_IMPACC2_DiscAmt.Text);
                }
                if (txt_IMPACC3_DiscAmt.Text != "" && txt_IMPACC3_DiscAmt.Text != "0")
                {
                    Amt3 = float.Parse(txt_IMPACC3_DiscAmt.Text);
                }
                if (txt_IMPACC4_DiscAmt.Text != "" && txt_IMPACC4_DiscAmt.Text != "0")
                {
                    Amt4 = float.Parse(txt_IMPACC4_DiscAmt.Text);
                }
                txt_IMPACC5_DiscAmt.Text = Convert.ToDecimal((float.Parse(txtAmtRealised.Text) - Amt1 - Amt2 - Amt3 - Amt4)).ToString("0.00");
                if (txt_IMPACC5_DiscAmt.Text.Contains("-"))
                {
                    txt_IMPACC5_DiscAmt.Text = "0.00";
                }
            }
        }
    }

    protected void txt_IMPACC1_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        /////////  for deal does not use in same part or full settlemnet  disscution//
        //TF_DATA obj = new TF_DATA();
        //DataTable dt = new DataTable();
        //SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC1_FCRefNo.Text.Trim());
        //SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchNameEXPAc.Value.Trim());
        //SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        //SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustabbr.Value.Trim());
        //dt = obj.getData("TF_EXP_FillFXDetails", P1, P2, P4);

        //if (dt.Rows.Count > 0)
        //{
        //    string result = obj.SaveDeleteData("TF_EXP_CheckFX", P1, P3);
        //    string _script = "";
        //    if (result == "Exists")
        //    {
        //        _script = "alert('This FC Ref No(" + txt_IMPACC1_FCRefNo.Text + ") already used.')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC1_FCRefNo.Text = "";
        //        txt_IMPACC1_Princ_Ex_Curr.Text = "";
        //        txt_IMPACC1_Princ_Ex_rate.Text = "";
        //        txt_IMPACC1_Princ_Intnl_Ex_rate.Text = "";
        //        txt_IMPACC1_FCRefNo.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC1_FCRefNo.Focus();
        //    }
        //    txt_IMPACC1_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC1_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC1_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
        //    txt_IMPACC1_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
        //    //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC1_DR_Amt_Calculation();", true);
        //    txt_IMPACC1_FCRefNo.Focus();

        //}
        //else
        //{
        //    txt_IMPACC1_FCRefNo.Text = "";
        //    txt_IMPACC1_Princ_Ex_Curr.Text = "";
        //    txt_IMPACC1_DR_Cur_Acc_Curr.Text = "";
        //    txt_IMPACC1_Princ_Ex_rate.Text = "";
        //    txt_IMPACC1_Princ_Intnl_Ex_rate.Text = "";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
        //    txt_IMPACC1_FCRefNo.Focus();
        //}
    }
    protected void txt_IMPACC2_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA obj = new TF_DATA();
        //DataTable dt = new DataTable();
        //SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC2_FCRefNo.Text.Trim());
        //SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchNameEXPAc.Value.Trim());
        //SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        //SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustabbr.Value.Trim());
        //dt = obj.getData("TF_EXP_FillFXDetails", P1, P2, P4);
        //if (dt.Rows.Count > 0)
        //{
        //    string result = obj.SaveDeleteData("TF_EXP_CheckFX", P1, P3);
        //    string _script = "";
        //    if (result == "Exists")
        //    {
        //        _script = "alert('This FC Ref No(" + txt_IMPACC2_FCRefNo.Text + ") already used.')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC2_FCRefNo.Text = "";
        //        txt_IMPACC2_Princ_Ex_Curr.Text = "";
        //        txt_IMPACC2_Princ_Ex_rate.Text = "";
        //        txt_IMPACC2_Princ_Intnl_Ex_rate.Text = "";
        //        txt_IMPACC2_FCRefNo.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC2_FCRefNo.Focus();
        //    }
        //    txt_IMPACC2_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC2_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC2_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
        //    txt_IMPACC2_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
        //    //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC2_DR_Amt_Calculation();", true);
        //    txt_IMPACC2_FCRefNo.Focus();
        //}
        //else
        //{
        //    txt_IMPACC2_FCRefNo.Text = "";
        //    txt_IMPACC2_Princ_Ex_Curr.Text = "";
        //    txt_IMPACC2_DR_Cur_Acc_Curr.Text = "";
        //    txt_IMPACC2_Princ_Ex_rate.Text = "";
        //    txt_IMPACC2_Princ_Intnl_Ex_rate.Text = "";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
        //    txt_IMPACC2_FCRefNo.Focus();
        //}
    }
    protected void txt_IMPACC3_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA obj = new TF_DATA();
        //DataTable dt = new DataTable();
        //SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC3_FCRefNo.Text.Trim());
        //SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchNameEXPAc.Value.Trim());
        //SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        //SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustabbr.Value.Trim());
        //dt = obj.getData("TF_EXP_FillFXDetails", P1, P2, P4);
        //if (dt.Rows.Count > 0)
        //{
        //    string result = obj.SaveDeleteData("TF_EXP_CheckFX", P1, P3);
        //    string _script = "";
        //    if (result == "Exists")
        //    {
        //        _script = "alert('This FC Ref No(" + txt_IMPACC3_FCRefNo.Text + ") already used.')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC3_FCRefNo.Text = "";
        //        txt_IMPACC3_Princ_Ex_Curr.Text = "";
        //        txt_IMPACC3_Princ_Ex_rate.Text = "";
        //        txt_IMPACC3_Princ_Intnl_Ex_rate.Text = "";
        //        txt_IMPACC3_FCRefNo.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC3_FCRefNo.Focus();
        //    }
        //    txt_IMPACC3_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC3_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC3_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
        //    txt_IMPACC3_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
        //    //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC3_DR_Amt_Calculation();", true);
        //    txt_IMPACC3_FCRefNo.Focus();
        //}
        //else
        //{
        //    txt_IMPACC3_FCRefNo.Text = "";
        //    txt_IMPACC3_Princ_Ex_Curr.Text = "";
        //    txt_IMPACC3_DR_Cur_Acc_Curr.Text = "";
        //    txt_IMPACC3_Princ_Ex_rate.Text = "";
        //    txt_IMPACC3_Princ_Intnl_Ex_rate.Text = "";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
        //    txt_IMPACC3_FCRefNo.Focus();
        //}
    }
    protected void txt_IMPACC4_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA obj = new TF_DATA();
        //DataTable dt = new DataTable();
        //SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC4_FCRefNo.Text.Trim());
        //SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchNameEXPAc.Value.Trim());
        //SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        //SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustabbr.Value.Trim());
        //dt = obj.getData("TF_EXP_FillFXDetails", P1, P2, P4);
        //if (dt.Rows.Count > 0)
        //{
        //    string result = obj.SaveDeleteData("TF_EXP_CheckFX", P1, P3);
        //    string _script = "";
        //    if (result == "Exists")
        //    {
        //        _script = "alert('This FC Ref No(" + txt_IMPACC4_FCRefNo.Text + ") already used.')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC4_FCRefNo.Text = "";
        //        txt_IMPACC4_Princ_Ex_Curr.Text = "";
        //        txt_IMPACC4_Princ_Ex_rate.Text = "";
        //        txt_IMPACC4_Princ_Intnl_Ex_rate.Text = "";
        //        txt_IMPACC4_FCRefNo.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC4_FCRefNo.Focus();
        //    }
        //    txt_IMPACC4_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC4_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC4_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
        //    txt_IMPACC4_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
        //    //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC4_DR_Amt_Calculation();", true);
        //    txt_IMPACC4_FCRefNo.Focus();
        //}
        //else
        //{
        //    txt_IMPACC4_FCRefNo.Text = "";
        //    txt_IMPACC4_Princ_Ex_Curr.Text = "";
        //    txt_IMPACC4_DR_Cur_Acc_Curr.Text = "";
        //    txt_IMPACC4_Princ_Ex_rate.Text = "";
        //    txt_IMPACC4_Princ_Intnl_Ex_rate.Text = "";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
        //    txt_IMPACC4_FCRefNo.Focus();
        //}
    }
    protected void txt_IMPACC5_FCRefNo_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA obj = new TF_DATA();
        //DataTable dt = new DataTable();
        //SqlParameter P1 = new SqlParameter("@REFNO", txt_IMPACC5_FCRefNo.Text.Trim());
        //SqlParameter P2 = new SqlParameter("@BranchName", hdnBranchNameEXPAc.Value.Trim());
        //SqlParameter P3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        //SqlParameter P4 = new SqlParameter("@CustAbbr", hdnCustabbr.Value.Trim());
        //dt = obj.getData("TF_EXP_FillFXDetails", P1, P2, P4);
        //if (dt.Rows.Count > 0)
        //{
        //    string result = obj.SaveDeleteData("TF_EXP_CheckFX", P1, P3);
        //    string _script = "";
        //    if (result == "Exists")
        //    {
        //        _script = "alert('This FC Ref No(" + txt_IMPACC5_FCRefNo.Text + ") already used.')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC5_FCRefNo.Text = "";
        //        txt_IMPACC5_Princ_Ex_Curr.Text = "";
        //        txt_IMPACC5_Princ_Ex_rate.Text = "";
        //        txt_IMPACC5_Princ_Intnl_Ex_rate.Text = "";
        //        txt_IMPACC5_FCRefNo.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        _script = "alert('1. GBase ref no- " + dt.Rows[0]["GBASE_REFERENCE_NUMBER"].ToString() + ". \\n2. Contract Date- " + dt.Rows[0]["CONTRACT_DATE"].ToString() + ".\\n3. Contract Amount- " + dt.Rows[0]["CONTRACT_AMOUNT"].ToString() + ".\\n4. Contract Currency- " + dt.Rows[0]["CONTRACT_CURRENCY"].ToString() + ".\\n5. Equivalent Currency- " + dt.Rows[0]["EQUIVALENT_CURRENCY"].ToString() + ".\\n6. Equivalent Amount- " + dt.Rows[0]["EQUIVALENT_AMOUNT"].ToString() + ".')";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
        //        txt_IMPACC5_FCRefNo.Focus();
        //    }
        //    txt_IMPACC5_Princ_Ex_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC5_DR_Cur_Acc_Curr.Text = dt.Rows[0]["Exchange_Currency"].ToString();
        //    txt_IMPACC5_Princ_Ex_rate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
        //    txt_IMPACC5_Princ_Intnl_Ex_rate.Text = dt.Rows[0]["INTERNAL_RATE"].ToString();
        //    //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "IMPACC5_DR_Amt_Calculation();", true);
        //    txt_IMPACC5_FCRefNo.Focus();
        //}
        //else
        //{
        //    txt_IMPACC5_FCRefNo.Text = "";
        //    txt_IMPACC5_Princ_Ex_Curr.Text = "";
        //    txt_IMPACC5_DR_Cur_Acc_Curr.Text = "";
        //    txt_IMPACC5_Princ_Ex_rate.Text = "";
        //    txt_IMPACC5_Princ_Intnl_Ex_rate.Text = "";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid FC Ref No.')", true);
        //    txt_IMPACC5_FCRefNo.Focus();
        //}
    }
    protected void AccountAmtCheck()
    {
        hdnAccountAmtcheck.Value = "";
        decimal Amt1 = 0, Amt2 = 0, Amt3 = 0, Amt4 = 0, Amt5 = 0, AmtTotal = 0;

        if (txt_IMPACC1_DiscAmt.Text != "" && txt_IMPACC1_DiscAmt.Text != "0")
        {
            Amt1 = decimal.Parse(txt_IMPACC1_DiscAmt.Text);
        }
        if (txt_IMPACC2_DiscAmt.Text != "" && txt_IMPACC2_DiscAmt.Text != "0")
        {
            Amt2 = decimal.Parse(txt_IMPACC2_DiscAmt.Text);
        }
        if (txt_IMPACC3_DiscAmt.Text != "" && txt_IMPACC3_DiscAmt.Text != "0")
        {
            Amt3 = decimal.Parse(txt_IMPACC3_DiscAmt.Text);
        }
        if (txt_IMPACC4_DiscAmt.Text != "" && txt_IMPACC4_DiscAmt.Text != "0")
        {
            Amt4 = decimal.Parse(txt_IMPACC4_DiscAmt.Text);
        }
        if (txt_IMPACC5_DiscAmt.Text != "" && txt_IMPACC5_DiscAmt.Text != "0")
        {
            Amt5 = decimal.Parse(txt_IMPACC5_DiscAmt.Text);
        }

        AmtTotal = Amt1 + Amt2 + Amt3 + Amt4 + Amt5;

        if (decimal.Parse(txtAmtRealised.Text) != AmtTotal)
        {
            hdnAccountAmtcheck.Value = "mismatch";
        }
    }

    protected void chkIRMCreate_CheckedChanged(object sender, EventArgs e)
    {
        if (txtDocNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Document number cannot be blank');", true);
            chkIRMCreate.Checked = false;
        }
        else if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chkIRMCreate.Checked = false;
        }
        else
        {
            if (chkIRMCreate.Checked == true)
            {
                btnTTRefNoList.Enabled = false;
                chkFirc.Enabled = false;
                chkDummySettlement.Enabled = false;
                pnlIRMCreate.Visible = true;
                /////////////  added by shailesh for IRM
                txtBankReferencenumber.Text = txtDocNo.Text;
                FillIFSCTemittanceAdCode();
                FillPanNO();
                //getSrialNo();
                getICEcode();
                txtDocNo.BackColor = txtValueDate.BackColor = txtDateRealised.BackColor = txt_relcur.BackColor = txtAmtRealised.BackColor = txtExchangeRate.BackColor =
                    txtAmtRealisedinINR.BackColor = txtRemitterName.BackColor = txtRemitterAddress.BackColor = txtRemitterCountry.BackColor = txtSwiftCode.BackColor =
                    txtRemitterBank.BackColor = txtRemitterBankAddress.BackColor = txtRemBankCountry.BackColor = txtPurposeCode.BackColor = txtpurposeofRemittance.BackColor =
                    ddlModeOfPayment.BackColor = txtBankUniqueTransactionID.BackColor = txtIFSCCode.BackColor = txtRemittanceADCode.BackColor = txtIECCode.BackColor =
                    txtPanNumber.BackColor = txtBankReferencenumber.BackColor = txtBankAccountNumber.BackColor = ddlIRMStatus.BackColor = System.Drawing.Color.PeachPuff;
            }
            else
            {
                btnTTRefNoList.Enabled = true;
                chkFirc.Enabled = true;
                chkDummySettlement.Enabled = true;
                pnlIRMCreate.Visible = false;

                txtBankUniqueTransactionID.Text = "";
                txtBankReferencenumber.Text = "";
                txtRemittanceADCode.Text = "";
                txtPanNumber.Text = "";
                txtIFSCCode.Text = "";
                txtIECCode.Text = "";
                txtDocNo.BackColor = txtValueDate.BackColor = txtDateRealised.BackColor = txt_relcur.BackColor = txtAmtRealised.BackColor = txtExchangeRate.BackColor =
                    txtAmtRealisedinINR.BackColor = txtRemitterName.BackColor = txtRemitterAddress.BackColor = txtRemitterCountry.BackColor = txtSwiftCode.BackColor =
                    txtRemitterBank.BackColor = txtRemitterBankAddress.BackColor = txtRemBankCountry.BackColor = txtPurposeCode.BackColor = txtpurposeofRemittance.BackColor =
                    ddlModeOfPayment.BackColor = txtBankUniqueTransactionID.BackColor = txtIFSCCode.BackColor = txtRemittanceADCode.BackColor = txtIECCode.BackColor =
                    txtPanNumber.BackColor = txtBankReferencenumber.BackColor = txtBankAccountNumber.BackColor = ddlIRMStatus.BackColor = System.Drawing.Color.White;
            }
        }
    }
    protected void chkTTRefNo_CheckedChanged(object sender, EventArgs e)
    {
        if (txtDocNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Document number cannot be blank');", true);
            btnTTRefNoList.Checked = false;
        }
        else if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            btnTTRefNoList.Checked = false;
        }
        else
        {
            if (btnTTRefNoList.Checked == true)
            {
                chkIRMCreate.Enabled = false;
                chkFirc.Enabled = false;
            }
            else
            {
                if (chkDummySettlement.Checked == true)
                {
                    chkIRMCreate.Enabled = false;
                }
                else
                {
                    chkIRMCreate.Enabled = true;
                }
                chkFirc.Enabled = true;
            }
        }
    }

    protected void chkDummySettlement_CheckedChanged(object sender, EventArgs e)
    {
        if (txtDocNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Document number cannot be blank');", true);
            chkDummySettlement.Checked = false;
        }
        else if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chkDummySettlement.Checked = false;
        }
        else
        {
            if (chkDummySettlement.Checked == true)
            {
                chkIRMCreate.Enabled = false;
                chk_IMPACC1Flag.Checked = true;
                PanelIMPACC1.Visible = true;
                txt_IMPACC1_DiscAmt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");
                txt_IMPACC1_DR_Cur_Acc_Curr.Text = txt_relcur.Text;
                txt_IMPACC1_DR_Cur_Acc_amt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");

                txt_IMPACC1_CR_Cust_abbr.Text = "900";
                txt_IMPACC1_CR_AC_Short_Name.Text = "COMP SUSPENSE";
                txt_IMPACC1_CR_Code.Text = "14017";
                txt_IMPACC1_CR_Cust_Acc.Text = "B79-792-111111";


                txt_IMPACC1_DR_Cust_abbr.Text = "900";
                txt_IMPACC1_DR_AC_Short_Name.Text = "COMP SUSPENSE";
                txt_IMPACC1_DR_Code.Text = "14017";
                txt_IMPACC1_DR_Cust_Acc.Text = "B79-792-111111";

                txt_IMPACC1_CR_Acceptance_Curr.Text = txt_relcur.Text;
                txt_IMPACC1_CR_Acceptance_amt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");

            }
            else
            {
                if (btnTTRefNoList.Checked == true)
                {
                    chkIRMCreate.Enabled = false;
                    chkFirc.Enabled = false;
                }
                else if (chkFirc.Checked == true)
                {
                    chkIRMCreate.Enabled = false;
                    btnTTRefNoList.Enabled = false;
                }
                else
                {
                    chkIRMCreate.Enabled = true;
                }

                chk_IMPACC1Flag.Checked = false;
                PanelIMPACC1.Visible = false;
                ClearExportAcEntry("ACC1");
            }
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
    public void TTFIRCtotalCaluation()
    {
        if (txtTTAmtRealised1.Text == "")
        {
            txtTTAmtRealised1.Text = "0";
        }
        if (txtTTAmtRealised2.Text == "")
        {
            txtTTAmtRealised2.Text = "0";
        }
        if (txtTTAmtRealised3.Text == "")
        {
            txtTTAmtRealised3.Text = "0";
        }
        if (txtTTAmtRealised4.Text == "")
        {
            txtTTAmtRealised4.Text = "0";
        }
        if (txtTTAmtRealised5.Text == "")
        {
            txtTTAmtRealised5.Text = "0";
        }
        if (txtTTAmtRealised6.Text == "")
        {
            txtTTAmtRealised6.Text = "0";
        }
        if (txtTTAmtRealised7.Text == "")
        {
            txtTTAmtRealised7.Text = "0";
        }
        if (txtTTAmtRealised8.Text == "")
        {
            txtTTAmtRealised8.Text = "0";
        }
        if (txtTTAmtRealised9.Text == "")
        {
            txtTTAmtRealised9.Text = "0";
        }
        if (txtTTAmtRealised10.Text == "")
        {
            txtTTAmtRealised10.Text = "0";
        }
        if (txtTTAmtRealised11.Text == "")
        {
            txtTTAmtRealised11.Text = "0";
        }
        if (txtTTAmtRealised12.Text == "")
        {
            txtTTAmtRealised12.Text = "0";
        }
        if (txtTTAmtRealised13.Text == "")
        {
            txtTTAmtRealised13.Text = "0";
        }
        if (txtTTAmtRealised14.Text == "")
        {
            txtTTAmtRealised14.Text = "0";
        }
        if (txtTTAmtRealised15.Text == "")
        {
            txtTTAmtRealised15.Text = "0";
        }

        decimal TotalTTAmt = decimal.Parse(txtTTAmtRealised1.Text) + decimal.Parse(txtTTAmtRealised2.Text) + decimal.Parse(txtTTAmtRealised3.Text) + decimal.Parse(txtTTAmtRealised4.Text) + decimal.Parse(txtTTAmtRealised5.Text) + decimal.Parse(txtTTAmtRealised6.Text) + decimal.Parse(txtTTAmtRealised7.Text) + decimal.Parse(txtTTAmtRealised8.Text) + decimal.Parse(txtTTAmtRealised9.Text) + decimal.Parse(txtTTAmtRealised10.Text) + decimal.Parse(txtTTAmtRealised11.Text) + decimal.Parse(txtTTAmtRealised12.Text) + decimal.Parse(txtTTAmtRealised13.Text) + decimal.Parse(txtTTAmtRealised14.Text) + decimal.Parse(txtTTAmtRealised15.Text);

        //float TotalFIRCAmt = float.Parse(txtFIRCTobeAdjustedinSB1_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB2_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB3_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB4_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB5_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB6_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB7_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB8_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB9_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB10_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB11_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB12_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB13_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB14_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB15_OB.Text);

        //float TotalTTFIRCAmt = TotalTTAmt + TotalFIRCAmt;

        string textValue = TotalTTAmt.ToString();


        string TotalTTAmtstring = textValue;
        int decimalIndex = textValue.IndexOf('.');
        if (decimalIndex != -1 && decimalIndex + 3 < textValue.Length) // check if there are more than 2 decimal places
        {
            TotalTTAmtstring = textValue.Substring(0, decimalIndex + 3); // truncate to 2 decimal places
        }

        decimal roundResultotal = decimal.Parse(TotalTTAmtstring);

        if (roundResultotal > decimal.Parse(txtAmtRealised.Text))
        {
            hdnTTFIRCTotalAmtCheck.Value = "Greater";
        }
        else if (roundResultotal != decimal.Parse(txtAmtRealised.Text))
        {
            hdnTTFIRCTotalAmtCheck.Value = "mismatch";
        }
        else
        {
            hdnTTFIRCTotalAmtCheck.Value = "";
        }

    }

    public void ValidateTTCurr()
    {
        if (ddlTTRealisedCurr1.SelectedValue != "0" && (txtTTCrossCurrRate1.Text == "" || txtTTCrossCurrRate1.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr2.SelectedValue != "0" && (txtTTCrossCurrRate2.Text == "" || txtTTCrossCurrRate2.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr3.SelectedValue != "0" && (txtTTCrossCurrRate3.Text == "" || txtTTCrossCurrRate3.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr4.SelectedValue != "0" && (txtTTCrossCurrRate4.Text == "" || txtTTCrossCurrRate4.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr5.SelectedValue != "0" && (txtTTCrossCurrRate5.Text == "" || txtTTCrossCurrRate5.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr6.SelectedValue != "0" && (txtTTCrossCurrRate6.Text == "" || txtTTCrossCurrRate6.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr7.SelectedValue != "0" && (txtTTCrossCurrRate7.Text == "" || txtTTCrossCurrRate7.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr8.SelectedValue != "0" && (txtTTCrossCurrRate8.Text == "" || txtTTCrossCurrRate8.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr9.SelectedValue != "0" && (txtTTCrossCurrRate9.Text == "" || txtTTCrossCurrRate9.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr10.SelectedValue != "0" && (txtTTCrossCurrRate10.Text == "" || txtTTCrossCurrRate10.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr11.SelectedValue != "0" && (txtTTCrossCurrRate11.Text == "" || txtTTCrossCurrRate11.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr12.SelectedValue != "0" && (txtTTCrossCurrRate12.Text == "" || txtTTCrossCurrRate12.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr13.SelectedValue != "0" && (txtTTCrossCurrRate13.Text == "" || txtTTCrossCurrRate13.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr14.SelectedValue != "0" && (txtTTCrossCurrRate14.Text == "" || txtTTCrossCurrRate14.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }

        if (ddlTTRealisedCurr15.SelectedValue != "0" && (txtTTCrossCurrRate15.Text == "" || txtTTCrossCurrRate15.Text == "0"))
        {
            hdnTTCurrCheck.Value = "TTfalse";
        }


        if (hdnTTCurrCheck.Value == "TTfalse")
        {
            hdnTTCurrCheck.Value = hdnTTCurrCheck.Value;
        }
        else
        {
            hdnTTCurrCheck.Value = "";
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

    //----------------------------------Anand09-08-2023-------------------------------------------

    protected void chk_Generaloperation1_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_Generaloperation1.Checked == false)
        {
            ClearGOEntry("GO1");
            panalGO.Visible = false;
        }
        else if (chk_Generaloperation1.Checked == true)
        {
            panalGO.Visible = true;
            txtGO_ValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtGO_Section.Text = "06";
            txtGO_Debit_Entity.Text = "010";
            txtGO_Credit_Entity.Text = "010";
            txtGO_Debit_Division.Text = "11";
            txtGO_Credit_Division.Text = "11";
        }
    }


    protected void chk_Generaloperation2_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_Generaloperation2.Checked == false)
        {
            ClearGOEntry("GO2");
            PanelNormalGO.Visible = false;
        }
        else if (chk_Generaloperation2.Checked == true)
        {
            PanelNormalGO.Visible = true;
            txtNGO_ValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtNGO_Section.Text = "06";
            txtNGO_Debit_Entity.Text = "010";
            txtNGO_Credit_Entity.Text = "010";
            txtNGO_Debit_Division.Text = "11";
            txtNGO_Credit_Division.Text = "11";
        }
    }

    protected void chk_InterOffice_OnCheckedChanged(object sender, EventArgs e)
    {
        if (txtAmtRealised.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Realized amount!')", true);
            chk_IMPACC1Flag.Checked = false;
        }
        else
        {
            if (chk_InterOffice.Checked == false)
            {
                ClearGOEntry("INTER");
                panal_GOAccChange.Visible = false;
            }
            else if (chk_InterOffice.Checked == true)
            {
                panal_GOAccChange.Visible = true;
                txtIO_ValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txt_GOAccChange_SectionNo.Text = "06";
                txt_GOAccChange_Debit_Entity.Text = "010";
                txt_GOAccChange_Credit_Entity.Text = "010";
                txt_GOAccChange_Debit_Code.Text = "D";
                txt_GOAccChange_Credit_Code.Text = "C";
                txt_GOAccChange_Debit_Division.Text = "31";
                txt_GOAccChange_Credit_Division.Text = "21";
                txt_GOAccChange_Debit_Amt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");
                txt_GOAccChange_Debit_Curr.Text = txt_relcur.Text;
                txt_GOAccChange_Credit_Amt.Text = Convert.ToDecimal(txtAmtRealised.Text).ToString("0.00");
                txt_GOAccChange_Credit_Curr.Text = txt_relcur.Text;
                string REFNO = txtDocNo.Text.Substring(8, 6);
                txt_GOAccChange_Ref_No.Text = "TPN-792-" + REFNO;
                txt_GOAccChange_Remarks.Text = "NOSTRO ENTRY";
                txt_GOAccChange_Debit_Details.Text = "NOSTRO ENTRY";
                txt_GOAccChange_Credit_Details.Text = "NOSTRO ENTRY";
                branchcode = Request.QueryString["BranchCode"].ToString();
                if (branchcode == "793")
                {
                    txt_GOAccChange_Credit_Cust.Text = "MHCB ND";
                    txt_GOAccChange_Credit_Cust_Name.Text = "INTEROFFICE A/C ND BR.,";
                    txt_GOAccChange_Credit_Cust_AcCode.Text = "81014";
                    txt_GOAccChange_Credit_Cust_AccNo.Text = "B79-793-111111";
                }
                if (branchcode == "716")
                {
                    txt_GOAccChange_Credit_Cust.Text = "MHCB BL";
                    txt_GOAccChange_Credit_Cust_Name.Text = "INTEROFFICE A/C BL BR.,";
                    txt_GOAccChange_Credit_Cust_AcCode.Text = "89868";
                    txt_GOAccChange_Credit_Cust_AccNo.Text = "B79-716-111111";
                }
                if (branchcode == "717")
                {
                    txt_GOAccChange_Credit_Cust.Text = "MHCB CI";
                    txt_GOAccChange_Credit_Cust_Name.Text = "INTEROFFICE A/C CI BR.,";
                    txt_GOAccChange_Credit_Cust_AcCode.Text = "81323";
                    txt_GOAccChange_Credit_Cust_AccNo.Text = "B79-717-111111";
                }
            }
        }
    }

    protected void ClearExportAcEntry(string IMP_Accounting)
    {
        switch (IMP_Accounting)
        {
            case "ACC1":
                txt_IMPACC1_FCRefNo.Text = txt_IMPACC1_DiscAmt.Text = txt_IMPACC1_Princ_matu.Text = txt_IMPACC1_Princ_lump.Text = txt_IMPACC1_Princ_Contract_no.Text = ""; txt_IMPACC1_Princ_Ex_Curr.Text = ""; txt_IMPACC1_Princ_Ex_rate.Text = ""; txt_IMPACC1_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC1_Interest_matu.Text = ""; txt_IMPACC1_Interest_lump.Text = ""; txt_IMPACC1_Interest_Contract_no.Text = ""; txt_IMPACC1_Interest_Ex_Curr.Text = ""; txt_IMPACC1_Interest_Ex_rate.Text = ""; txt_IMPACC1_Interest_Intnl_Ex_rate.Text = "";
                txt_IMPACC1_Commission_matu.Text = ""; txt_IMPACC1_Commission_lump.Text = ""; txt_IMPACC1_Commission_Contract_no.Text = ""; txt_IMPACC1_Commission_Ex_Curr.Text = ""; txt_IMPACC1_Commission_Ex_rate.Text = ""; txt_IMPACC1_Commission_Intnl_Ex_rate.Text = "";
                txt_IMPACC1_Their_Commission_matu.Text = ""; txt_IMPACC1_Their_Commission_lump.Text = ""; txt_IMPACC1_Their_Commission_Contract_no.Text = ""; txt_IMPACC1_Their_Commission_Ex_Curr.Text = ""; txt_IMPACC1_Their_Commission_Ex_rate.Text = "";
                txt_IMPACC1_Their_Commission_Intnl_Ex_rate.Text = ""; txt_IMPACC1_CR_Code.Text = ""; txt_IMPACC1_CR_AC_Short_Name.Text = ""; txt_IMPACC1_CR_Cust_abbr.Text = ""; txt_IMPACC1_CR_Cust_Acc.Text = ""; txt_IMPACC1_CR_Acceptance_Curr.Text = "";
                txt_IMPACC1_CR_Acceptance_amt.Text = ""; txt_IMPACC1_CR_Acceptance_payer.Text = ""; txt_IMPACC1_CR_Interest_Curr.Text = ""; txt_IMPACC1_CR_Interest_amt.Text = ""; txt_IMPACC1_CR_Interest_payer.Text = ""; txt_IMPACC1_CR_Accept_Commission_Curr.Text = "";
                txt_IMPACC1_CR_Accept_Commission_amt.Text = ""; txt_IMPACC1_CR_Accept_Commission_Payer.Text = ""; txt_IMPACC1_CR_Pay_Handle_Commission_Curr.Text = ""; txt_IMPACC1_CR_Pay_Handle_Commission_amt.Text = ""; txt_IMPACC1_CR_Pay_Handle_Commission_Payer.Text = "";
                txt_IMPACC1_CR_Others_Curr.Text = ""; txt_IMPACC1_CR_Others_amt.Text = ""; txt_IMPACC1_CR_Others_Payer.Text = ""; txt_IMPACC1_CR_Their_Commission_Curr.Text = ""; txt_IMPACC1_CR_Their_Commission_amt.Text = ""; txt_IMPACC1_CR_Their_Commission_Payer.Text = "";
                txt_IMPACC1_DR_Code.Text = ""; txt_IMPACC1_DR_AC_Short_Name.Text = ""; txt_IMPACC1_DR_Cust_abbr.Text = ""; txt_IMPACC1_DR_Cust_Acc.Text = ""; txt_IMPACC1_DR_Cur_Acc_Curr.Text = ""; txt_IMPACC1_DR_Cur_Acc_amt.Text = ""; txt_IMPACC1_DR_Cur_Acc_payer.Text = "";
                txt_IMPACC1_DR_Code2.Text = ""; txt_IMPACC1_DR_AC_Short_Name2.Text = ""; txt_IMPACC1_DR_Cust_abbr2.Text = ""; txt_IMPACC1_DR_Cust_Acc2.Text = ""; txt_IMPACC1_DR_Cur_Acc_Curr2.Text = ""; txt_IMPACC1_DR_Cur_Acc_amt2.Text = ""; txt_IMPACC1_DR_Cur_Acc_payer2.Text = "";
                txt_IMPACC1_DR_Code3.Text = ""; txt_IMPACC1_DR_AC_Short_Name3.Text = ""; txt_IMPACC1_DR_Cust_abbr3.Text = ""; txt_IMPACC1_DR_Cust_Acc3.Text = ""; txt_IMPACC1_DR_Cur_Acc_Curr3.Text = ""; txt_IMPACC1_DR_Cur_Acc_amt3.Text = ""; txt_IMPACC1_DR_Cur_Acc_payer3.Text = "";
                txt_IMPACC1_DR_Code4.Text = ""; txt_IMPACC1_DR_AC_Short_Name4.Text = ""; txt_IMPACC1_DR_Cust_abbr4.Text = ""; txt_IMPACC1_DR_Cust_Acc4.Text = ""; txt_IMPACC1_DR_Cur_Acc_Curr4.Text = ""; txt_IMPACC1_DR_Cur_Acc_amt4.Text = ""; txt_IMPACC1_DR_Cur_Acc_payer4.Text = "";
                txt_IMPACC1_DR_Code5.Text = ""; txt_IMPACC1_DR_AC_Short_Name5.Text = ""; txt_IMPACC1_DR_Cust_abbr5.Text = ""; txt_IMPACC1_DR_Cust_Acc5.Text = ""; txt_IMPACC1_DR_Cur_Acc_Curr5.Text = ""; txt_IMPACC1_DR_Cur_Acc_amt5.Text = ""; txt_IMPACC1_DR_Cur_Acc_payer5.Text = "";
                txt_IMPACC1_DR_Code6.Text = ""; txt_IMPACC1_DR_AC_Short_Name6.Text = ""; txt_IMPACC1_DR_Cust_abbr6.Text = ""; txt_IMPACC1_DR_Cust_Acc6.Text = ""; txt_IMPACC1_DR_Cur_Acc_Curr6.Text = ""; txt_IMPACC1_DR_Cur_Acc_amt6.Text = ""; txt_IMPACC1_DR_Cur_Acc_payer6.Text = "";
                break;
            case "ACC2":
                txt_IMPACC2_FCRefNo.Text = txt_IMPACC2_DiscAmt.Text = txt_IMPACC2_Princ_matu.Text = txt_IMPACC2_Princ_lump.Text = txt_IMPACC2_Princ_Contract_no.Text = ""; txt_IMPACC2_Princ_Ex_Curr.Text = ""; txt_IMPACC2_Princ_Ex_rate.Text = ""; txt_IMPACC2_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC2_Interest_matu.Text = ""; txt_IMPACC2_Interest_lump.Text = ""; txt_IMPACC2_Interest_Contract_no.Text = ""; txt_IMPACC2_Interest_Ex_Curr.Text = ""; txt_IMPACC2_Interest_Ex_rate.Text = ""; txt_IMPACC2_Interest_Intnl_Ex_rate.Text = "";
                txt_IMPACC2_Commission_matu.Text = ""; txt_IMPACC2_Commission_lump.Text = ""; txt_IMPACC2_Commission_Contract_no.Text = ""; txt_IMPACC2_Commission_Ex_Curr.Text = ""; txt_IMPACC2_Commission_Ex_rate.Text = ""; txt_IMPACC2_Commission_Intnl_Ex_rate.Text = "";
                txt_IMPACC2_Their_Commission_matu.Text = ""; txt_IMPACC2_Their_Commission_lump.Text = ""; txt_IMPACC2_Their_Commission_Contract_no.Text = ""; txt_IMPACC2_Their_Commission_Ex_Curr.Text = ""; txt_IMPACC2_Their_Commission_Ex_rate.Text = "";
                txt_IMPACC2_Their_Commission_Intnl_Ex_rate.Text = ""; txt_IMPACC2_CR_Code.Text = ""; txt_IMPACC2_CR_AC_Short_Name.Text = ""; txt_IMPACC2_CR_Cust_abbr.Text = ""; txt_IMPACC2_CR_Cust_Acc.Text = ""; txt_IMPACC2_CR_Acceptance_Curr.Text = "";
                txt_IMPACC2_CR_Acceptance_amt.Text = ""; txt_IMPACC2_CR_Acceptance_payer.Text = ""; txt_IMPACC2_CR_Interest_Curr.Text = ""; txt_IMPACC2_CR_Interest_amt.Text = ""; txt_IMPACC2_CR_Interest_payer.Text = ""; txt_IMPACC2_CR_Accept_Commission_Curr.Text = "";
                txt_IMPACC2_CR_Accept_Commission_amt.Text = ""; txt_IMPACC2_CR_Accept_Commission_Payer.Text = ""; txt_IMPACC2_CR_Pay_Handle_Commission_Curr.Text = ""; txt_IMPACC2_CR_Pay_Handle_Commission_amt.Text = ""; txt_IMPACC2_CR_Pay_Handle_Commission_Payer.Text = "";
                txt_IMPACC2_CR_Others_Curr.Text = ""; txt_IMPACC2_CR_Others_amt.Text = ""; txt_IMPACC2_CR_Others_Payer.Text = ""; txt_IMPACC2_CR_Their_Commission_Curr.Text = ""; txt_IMPACC2_CR_Their_Commission_amt.Text = ""; txt_IMPACC2_CR_Their_Commission_Payer.Text = "";
                txt_IMPACC2_DR_Code.Text = ""; txt_IMPACC2_DR_AC_Short_Name.Text = ""; txt_IMPACC2_DR_Cust_abbr.Text = ""; txt_IMPACC2_DR_Cust_Acc.Text = ""; txt_IMPACC2_DR_Cur_Acc_Curr.Text = ""; txt_IMPACC2_DR_Cur_Acc_amt.Text = ""; txt_IMPACC2_DR_Cur_Acc_payer.Text = "";
                txt_IMPACC2_DR_Code2.Text = ""; txt_IMPACC2_DR_AC_Short_Name2.Text = ""; txt_IMPACC2_DR_Cust_abbr2.Text = ""; txt_IMPACC2_DR_Cust_Acc2.Text = ""; txt_IMPACC2_DR_Cur_Acc_Curr2.Text = ""; txt_IMPACC2_DR_Cur_Acc_amt2.Text = ""; txt_IMPACC2_DR_Cur_Acc_payer2.Text = "";
                txt_IMPACC2_DR_Code3.Text = ""; txt_IMPACC2_DR_AC_Short_Name3.Text = ""; txt_IMPACC2_DR_Cust_abbr3.Text = ""; txt_IMPACC2_DR_Cust_Acc3.Text = ""; txt_IMPACC2_DR_Cur_Acc_Curr3.Text = ""; txt_IMPACC2_DR_Cur_Acc_amt3.Text = ""; txt_IMPACC2_DR_Cur_Acc_payer3.Text = "";
                txt_IMPACC2_DR_Code4.Text = ""; txt_IMPACC2_DR_AC_Short_Name4.Text = ""; txt_IMPACC2_DR_Cust_abbr4.Text = ""; txt_IMPACC2_DR_Cust_Acc4.Text = ""; txt_IMPACC2_DR_Cur_Acc_Curr4.Text = ""; txt_IMPACC2_DR_Cur_Acc_amt4.Text = ""; txt_IMPACC2_DR_Cur_Acc_payer4.Text = "";
                txt_IMPACC2_DR_Code5.Text = ""; txt_IMPACC2_DR_AC_Short_Name5.Text = ""; txt_IMPACC2_DR_Cust_abbr5.Text = ""; txt_IMPACC2_DR_Cust_Acc5.Text = ""; txt_IMPACC2_DR_Cur_Acc_Curr5.Text = ""; txt_IMPACC2_DR_Cur_Acc_amt5.Text = ""; txt_IMPACC2_DR_Cur_Acc_payer5.Text = "";
                txt_IMPACC2_DR_Code6.Text = ""; txt_IMPACC2_DR_AC_Short_Name6.Text = ""; txt_IMPACC2_DR_Cust_abbr6.Text = ""; txt_IMPACC2_DR_Cust_Acc6.Text = ""; txt_IMPACC2_DR_Cur_Acc_Curr6.Text = ""; txt_IMPACC2_DR_Cur_Acc_amt6.Text = ""; txt_IMPACC2_DR_Cur_Acc_payer6.Text = "";
                break;
            case "ACC3":
                txt_IMPACC3_FCRefNo.Text = txt_IMPACC3_DiscAmt.Text = txt_IMPACC3_Princ_matu.Text = txt_IMPACC3_Princ_lump.Text = txt_IMPACC3_Princ_Contract_no.Text = ""; txt_IMPACC3_Princ_Ex_Curr.Text = ""; txt_IMPACC3_Princ_Ex_rate.Text = ""; txt_IMPACC3_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC3_Interest_matu.Text = ""; txt_IMPACC3_Interest_lump.Text = ""; txt_IMPACC3_Interest_Contract_no.Text = ""; txt_IMPACC3_Interest_Ex_Curr.Text = ""; txt_IMPACC3_Interest_Ex_rate.Text = ""; txt_IMPACC3_Interest_Intnl_Ex_rate.Text = "";
                txt_IMPACC3_Commission_matu.Text = ""; txt_IMPACC3_Commission_lump.Text = ""; txt_IMPACC3_Commission_Contract_no.Text = ""; txt_IMPACC3_Commission_Ex_Curr.Text = ""; txt_IMPACC3_Commission_Ex_rate.Text = ""; txt_IMPACC3_Commission_Intnl_Ex_rate.Text = "";
                txt_IMPACC3_Their_Commission_matu.Text = ""; txt_IMPACC3_Their_Commission_lump.Text = ""; txt_IMPACC3_Their_Commission_Contract_no.Text = ""; txt_IMPACC3_Their_Commission_Ex_Curr.Text = ""; txt_IMPACC3_Their_Commission_Ex_rate.Text = "";
                txt_IMPACC3_Their_Commission_Intnl_Ex_rate.Text = ""; txt_IMPACC3_CR_Code.Text = ""; txt_IMPACC3_CR_AC_Short_Name.Text = ""; txt_IMPACC3_CR_Cust_abbr.Text = ""; txt_IMPACC3_CR_Cust_Acc.Text = ""; txt_IMPACC3_CR_Acceptance_Curr.Text = "";
                txt_IMPACC3_CR_Acceptance_amt.Text = ""; txt_IMPACC3_CR_Acceptance_payer.Text = ""; txt_IMPACC3_CR_Interest_Curr.Text = ""; txt_IMPACC3_CR_Interest_amt.Text = ""; txt_IMPACC3_CR_Interest_payer.Text = ""; txt_IMPACC3_CR_Accept_Commission_Curr.Text = "";
                txt_IMPACC3_CR_Accept_Commission_amt.Text = ""; txt_IMPACC3_CR_Accept_Commission_Payer.Text = ""; txt_IMPACC3_CR_Pay_Handle_Commission_Curr.Text = ""; txt_IMPACC3_CR_Pay_Handle_Commission_amt.Text = ""; txt_IMPACC3_CR_Pay_Handle_Commission_Payer.Text = "";
                txt_IMPACC3_CR_Others_Curr.Text = ""; txt_IMPACC3_CR_Others_amt.Text = ""; txt_IMPACC3_CR_Others_Payer.Text = ""; txt_IMPACC3_CR_Their_Commission_Curr.Text = ""; txt_IMPACC3_CR_Their_Commission_amt.Text = ""; txt_IMPACC3_CR_Their_Commission_Payer.Text = "";
                txt_IMPACC3_DR_Code.Text = ""; txt_IMPACC3_DR_AC_Short_Name.Text = ""; txt_IMPACC3_DR_Cust_abbr.Text = ""; txt_IMPACC3_DR_Cust_Acc.Text = ""; txt_IMPACC3_DR_Cur_Acc_Curr.Text = ""; txt_IMPACC3_DR_Cur_Acc_amt.Text = ""; txt_IMPACC3_DR_Cur_Acc_payer.Text = "";
                txt_IMPACC3_DR_Code2.Text = ""; txt_IMPACC3_DR_AC_Short_Name2.Text = ""; txt_IMPACC3_DR_Cust_abbr2.Text = ""; txt_IMPACC3_DR_Cust_Acc2.Text = ""; txt_IMPACC3_DR_Cur_Acc_Curr2.Text = ""; txt_IMPACC3_DR_Cur_Acc_amt2.Text = ""; txt_IMPACC3_DR_Cur_Acc_payer2.Text = "";
                txt_IMPACC3_DR_Code3.Text = ""; txt_IMPACC3_DR_AC_Short_Name3.Text = ""; txt_IMPACC3_DR_Cust_abbr3.Text = ""; txt_IMPACC3_DR_Cust_Acc3.Text = ""; txt_IMPACC3_DR_Cur_Acc_Curr3.Text = ""; txt_IMPACC3_DR_Cur_Acc_amt3.Text = ""; txt_IMPACC3_DR_Cur_Acc_payer3.Text = "";
                txt_IMPACC3_DR_Code4.Text = ""; txt_IMPACC3_DR_AC_Short_Name4.Text = ""; txt_IMPACC3_DR_Cust_abbr4.Text = ""; txt_IMPACC3_DR_Cust_Acc4.Text = ""; txt_IMPACC3_DR_Cur_Acc_Curr4.Text = ""; txt_IMPACC3_DR_Cur_Acc_amt4.Text = ""; txt_IMPACC3_DR_Cur_Acc_payer4.Text = "";
                txt_IMPACC3_DR_Code5.Text = ""; txt_IMPACC3_DR_AC_Short_Name5.Text = ""; txt_IMPACC3_DR_Cust_abbr5.Text = ""; txt_IMPACC3_DR_Cust_Acc5.Text = ""; txt_IMPACC3_DR_Cur_Acc_Curr5.Text = ""; txt_IMPACC3_DR_Cur_Acc_amt5.Text = ""; txt_IMPACC3_DR_Cur_Acc_payer5.Text = "";
                txt_IMPACC3_DR_Code6.Text = ""; txt_IMPACC3_DR_AC_Short_Name6.Text = ""; txt_IMPACC3_DR_Cust_abbr6.Text = ""; txt_IMPACC3_DR_Cust_Acc6.Text = ""; txt_IMPACC3_DR_Cur_Acc_Curr6.Text = ""; txt_IMPACC3_DR_Cur_Acc_amt6.Text = ""; txt_IMPACC3_DR_Cur_Acc_payer6.Text = "";
                break;
            case "ACC4":
                txt_IMPACC4_FCRefNo.Text = txt_IMPACC4_DiscAmt.Text = txt_IMPACC4_Princ_matu.Text = txt_IMPACC4_Princ_lump.Text = txt_IMPACC4_Princ_Contract_no.Text = ""; txt_IMPACC4_Princ_Ex_Curr.Text = ""; txt_IMPACC4_Princ_Ex_rate.Text = ""; txt_IMPACC4_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC4_Interest_matu.Text = ""; txt_IMPACC4_Interest_lump.Text = ""; txt_IMPACC4_Interest_Contract_no.Text = ""; txt_IMPACC4_Interest_Ex_Curr.Text = ""; txt_IMPACC4_Interest_Ex_rate.Text = ""; txt_IMPACC4_Interest_Intnl_Ex_rate.Text = "";
                txt_IMPACC4_Commission_matu.Text = ""; txt_IMPACC4_Commission_lump.Text = ""; txt_IMPACC4_Commission_Contract_no.Text = ""; txt_IMPACC4_Commission_Ex_Curr.Text = ""; txt_IMPACC4_Commission_Ex_rate.Text = ""; txt_IMPACC4_Commission_Intnl_Ex_rate.Text = "";
                txt_IMPACC4_Their_Commission_matu.Text = ""; txt_IMPACC4_Their_Commission_lump.Text = ""; txt_IMPACC4_Their_Commission_Contract_no.Text = ""; txt_IMPACC4_Their_Commission_Ex_Curr.Text = ""; txt_IMPACC4_Their_Commission_Ex_rate.Text = "";
                txt_IMPACC4_Their_Commission_Intnl_Ex_rate.Text = ""; txt_IMPACC4_CR_Code.Text = ""; txt_IMPACC4_CR_AC_Short_Name.Text = ""; txt_IMPACC4_CR_Cust_abbr.Text = ""; txt_IMPACC4_CR_Cust_Acc.Text = ""; txt_IMPACC4_CR_Acceptance_Curr.Text = "";
                txt_IMPACC4_CR_Acceptance_amt.Text = ""; txt_IMPACC4_CR_Acceptance_payer.Text = ""; txt_IMPACC4_CR_Interest_Curr.Text = ""; txt_IMPACC4_CR_Interest_amt.Text = ""; txt_IMPACC4_CR_Interest_payer.Text = ""; txt_IMPACC4_CR_Accept_Commission_Curr.Text = "";
                txt_IMPACC4_CR_Accept_Commission_amt.Text = ""; txt_IMPACC4_CR_Accept_Commission_Payer.Text = ""; txt_IMPACC4_CR_Pay_Handle_Commission_Curr.Text = ""; txt_IMPACC4_CR_Pay_Handle_Commission_amt.Text = ""; txt_IMPACC4_CR_Pay_Handle_Commission_Payer.Text = "";
                txt_IMPACC4_CR_Others_Curr.Text = ""; txt_IMPACC4_CR_Others_amt.Text = ""; txt_IMPACC4_CR_Others_Payer.Text = ""; txt_IMPACC4_CR_Their_Commission_Curr.Text = ""; txt_IMPACC4_CR_Their_Commission_amt.Text = ""; txt_IMPACC4_CR_Their_Commission_Payer.Text = "";
                txt_IMPACC4_DR_Code.Text = ""; txt_IMPACC4_DR_AC_Short_Name.Text = ""; txt_IMPACC4_DR_Cust_abbr.Text = ""; txt_IMPACC4_DR_Cust_Acc.Text = ""; txt_IMPACC4_DR_Cur_Acc_Curr.Text = ""; txt_IMPACC4_DR_Cur_Acc_amt.Text = ""; txt_IMPACC4_DR_Cur_Acc_payer.Text = "";
                txt_IMPACC4_DR_Code2.Text = ""; txt_IMPACC4_DR_AC_Short_Name2.Text = ""; txt_IMPACC4_DR_Cust_abbr2.Text = ""; txt_IMPACC4_DR_Cust_Acc2.Text = ""; txt_IMPACC4_DR_Cur_Acc_Curr2.Text = ""; txt_IMPACC4_DR_Cur_Acc_amt2.Text = ""; txt_IMPACC4_DR_Cur_Acc_payer2.Text = "";
                txt_IMPACC4_DR_Code3.Text = ""; txt_IMPACC4_DR_AC_Short_Name3.Text = ""; txt_IMPACC4_DR_Cust_abbr3.Text = ""; txt_IMPACC4_DR_Cust_Acc3.Text = ""; txt_IMPACC4_DR_Cur_Acc_Curr3.Text = ""; txt_IMPACC4_DR_Cur_Acc_amt3.Text = ""; txt_IMPACC4_DR_Cur_Acc_payer3.Text = "";
                txt_IMPACC4_DR_Code4.Text = ""; txt_IMPACC4_DR_AC_Short_Name4.Text = ""; txt_IMPACC4_DR_Cust_abbr4.Text = ""; txt_IMPACC4_DR_Cust_Acc4.Text = ""; txt_IMPACC4_DR_Cur_Acc_Curr4.Text = ""; txt_IMPACC4_DR_Cur_Acc_amt4.Text = ""; txt_IMPACC4_DR_Cur_Acc_payer4.Text = "";
                txt_IMPACC4_DR_Code5.Text = ""; txt_IMPACC4_DR_AC_Short_Name5.Text = ""; txt_IMPACC4_DR_Cust_abbr5.Text = ""; txt_IMPACC4_DR_Cust_Acc5.Text = ""; txt_IMPACC4_DR_Cur_Acc_Curr5.Text = ""; txt_IMPACC4_DR_Cur_Acc_amt5.Text = ""; txt_IMPACC4_DR_Cur_Acc_payer5.Text = "";
                txt_IMPACC4_DR_Code6.Text = ""; txt_IMPACC4_DR_AC_Short_Name6.Text = ""; txt_IMPACC4_DR_Cust_abbr6.Text = ""; txt_IMPACC4_DR_Cust_Acc6.Text = ""; txt_IMPACC4_DR_Cur_Acc_Curr6.Text = ""; txt_IMPACC4_DR_Cur_Acc_amt6.Text = ""; txt_IMPACC4_DR_Cur_Acc_payer6.Text = "";
                break;
            case "ACC5":
                txt_IMPACC5_FCRefNo.Text = txt_IMPACC5_DiscAmt.Text = txt_IMPACC5_Princ_matu.Text = txt_IMPACC5_Princ_lump.Text = txt_IMPACC5_Princ_Contract_no.Text = ""; txt_IMPACC5_Princ_Ex_Curr.Text = ""; txt_IMPACC5_Princ_Ex_rate.Text = ""; txt_IMPACC5_Princ_Intnl_Ex_rate.Text = "";
                txt_IMPACC5_Interest_matu.Text = ""; txt_IMPACC5_Interest_lump.Text = ""; txt_IMPACC5_Interest_Contract_no.Text = ""; txt_IMPACC5_Interest_Ex_Curr.Text = ""; txt_IMPACC5_Interest_Ex_rate.Text = ""; txt_IMPACC5_Interest_Intnl_Ex_rate.Text = "";
                txt_IMPACC5_Commission_matu.Text = ""; txt_IMPACC5_Commission_lump.Text = ""; txt_IMPACC5_Commission_Contract_no.Text = ""; txt_IMPACC5_Commission_Ex_Curr.Text = ""; txt_IMPACC5_Commission_Ex_rate.Text = ""; txt_IMPACC5_Commission_Intnl_Ex_rate.Text = "";
                txt_IMPACC5_Their_Commission_matu.Text = ""; txt_IMPACC5_Their_Commission_lump.Text = ""; txt_IMPACC5_Their_Commission_Contract_no.Text = ""; txt_IMPACC5_Their_Commission_Ex_Curr.Text = ""; txt_IMPACC5_Their_Commission_Ex_rate.Text = "";
                txt_IMPACC5_Their_Commission_Intnl_Ex_rate.Text = ""; txt_IMPACC5_CR_Code.Text = ""; txt_IMPACC5_CR_AC_Short_Name.Text = ""; txt_IMPACC5_CR_Cust_abbr.Text = ""; txt_IMPACC5_CR_Cust_Acc.Text = ""; txt_IMPACC5_CR_Acceptance_Curr.Text = "";
                txt_IMPACC5_CR_Acceptance_amt.Text = ""; txt_IMPACC5_CR_Acceptance_payer.Text = ""; txt_IMPACC5_CR_Interest_Curr.Text = ""; txt_IMPACC5_CR_Interest_amt.Text = ""; txt_IMPACC5_CR_Interest_payer.Text = ""; txt_IMPACC5_CR_Accept_Commission_Curr.Text = "";
                txt_IMPACC5_CR_Accept_Commission_amt.Text = ""; txt_IMPACC5_CR_Accept_Commission_Payer.Text = ""; txt_IMPACC5_CR_Pay_Handle_Commission_Curr.Text = ""; txt_IMPACC5_CR_Pay_Handle_Commission_amt.Text = ""; txt_IMPACC5_CR_Pay_Handle_Commission_Payer.Text = "";
                txt_IMPACC5_CR_Others_Curr.Text = ""; txt_IMPACC5_CR_Others_amt.Text = ""; txt_IMPACC5_CR_Others_Payer.Text = ""; txt_IMPACC5_CR_Their_Commission_Curr.Text = ""; txt_IMPACC5_CR_Their_Commission_amt.Text = ""; txt_IMPACC5_CR_Their_Commission_Payer.Text = "";
                txt_IMPACC5_DR_Code.Text = ""; txt_IMPACC5_DR_AC_Short_Name.Text = ""; txt_IMPACC5_DR_Cust_abbr.Text = ""; txt_IMPACC5_DR_Cust_Acc.Text = ""; txt_IMPACC5_DR_Cur_Acc_Curr.Text = ""; txt_IMPACC5_DR_Cur_Acc_amt.Text = ""; txt_IMPACC5_DR_Cur_Acc_payer.Text = "";
                txt_IMPACC5_DR_Code2.Text = ""; txt_IMPACC5_DR_AC_Short_Name2.Text = ""; txt_IMPACC5_DR_Cust_abbr2.Text = ""; txt_IMPACC5_DR_Cust_Acc2.Text = ""; txt_IMPACC5_DR_Cur_Acc_Curr2.Text = ""; txt_IMPACC5_DR_Cur_Acc_amt2.Text = ""; txt_IMPACC5_DR_Cur_Acc_payer2.Text = "";
                txt_IMPACC5_DR_Code3.Text = ""; txt_IMPACC5_DR_AC_Short_Name3.Text = ""; txt_IMPACC5_DR_Cust_abbr3.Text = ""; txt_IMPACC5_DR_Cust_Acc3.Text = ""; txt_IMPACC5_DR_Cur_Acc_Curr3.Text = ""; txt_IMPACC5_DR_Cur_Acc_amt3.Text = ""; txt_IMPACC5_DR_Cur_Acc_payer3.Text = "";
                txt_IMPACC5_DR_Code4.Text = ""; txt_IMPACC5_DR_AC_Short_Name4.Text = ""; txt_IMPACC5_DR_Cust_abbr4.Text = ""; txt_IMPACC5_DR_Cust_Acc4.Text = ""; txt_IMPACC5_DR_Cur_Acc_Curr4.Text = ""; txt_IMPACC5_DR_Cur_Acc_amt4.Text = ""; txt_IMPACC5_DR_Cur_Acc_payer4.Text = "";
                txt_IMPACC5_DR_Code5.Text = ""; txt_IMPACC5_DR_AC_Short_Name5.Text = ""; txt_IMPACC5_DR_Cust_abbr5.Text = ""; txt_IMPACC5_DR_Cust_Acc5.Text = ""; txt_IMPACC5_DR_Cur_Acc_Curr5.Text = ""; txt_IMPACC5_DR_Cur_Acc_amt5.Text = ""; txt_IMPACC5_DR_Cur_Acc_payer5.Text = "";
                txt_IMPACC5_DR_Code6.Text = ""; txt_IMPACC5_DR_AC_Short_Name6.Text = ""; txt_IMPACC5_DR_Cust_abbr6.Text = ""; txt_IMPACC5_DR_Cust_Acc6.Text = ""; txt_IMPACC5_DR_Cur_Acc_Curr6.Text = ""; txt_IMPACC5_DR_Cur_Acc_amt6.Text = ""; txt_IMPACC5_DR_Cur_Acc_payer6.Text = "";
                break;
        }
    }

    protected void ClearGOEntry(string GOENTRY)
    {
        switch (GOENTRY)
        {
            case "GO1":
                txtGO_ValueDate.Text = ""; txtGO_Comment.Text = ""; txtGO_Section.Text = ""; txtGO_Remark.Text = "";
                txtGO_Memo.Text = ""; txtGO_SchemeNo.Text = ""; txtGO_Debit.Text = ""; txtGO_Debit_CCY.Text = ""; txtGO_Debit_Amt.Text = ""; txtGO_Debit_Cust.Text = "";
                txtGO_Debit_Cust_Name.Text = ""; txtGO_Debit_Cust_AcCode.Text = ""; txtGO_Debit_Cust_AcCode_Name.Text = ""; txtGO_Debit_Cust_AccNo.Text = ""; txtGO_Debit_ExchRate.Text = ""; txtGO_Debit_ExchCCY.Text = "";
                txtGO_Debit_Fund.Text = ""; txtGO_Debit_CheckNo.Text = ""; txtGO_Debit_Available.Text = ""; txtGO_Debit_Advice_Print.Text = ""; txtGO_Debit_Details.Text = "";
                txtGO_Debit_Entity.Text = ""; txtGO_Debit_Division.Text = ""; txtGO_Debit_InterAmt.Text = ""; txtGO_Debit_InterRate.Text = ""; txtGO_Credit.Text = ""; txtGO_Credit_CCY.Text = "";
                txtGO_Credit_Amt.Text = ""; txtGO_Credit_Cust.Text = ""; txtGO_Credit_Cust_Name.Text = ""; txtGO_Credit_Cust_AcCode.Text = ""; txtGO_Credit_Cust_AcCode_Name.Text = ""; txtGO_Credit_Cust_AccNo.Text = "";
                txtGO_Credit_ExchRate.Text = ""; txtGO_Credit_ExchCCY.Text = ""; txtGO_Credit_Fund.Text = ""; txtGO_Credit_CheckNo.Text = ""; txtGO_Credit_Available.Text = "";
                txtGO_Credit_Advice_Print.Text = ""; txtGO_Credit_Details.Text = ""; txtGO_Credit_Entity.Text = ""; txtGO_Credit_Division.Text = ""; txtGO_Credit_InterAmt.Text = ""; txtGO_Credit_InterRate.Text = "";
                break;
            case "GO2":
                txtNGO_ValueDate.Text = ""; txtNGO_Comment.Text = ""; txtNGO_Section.Text = ""; txtNGO_Remark.Text = "";
                txtNGO_Memo.Text = ""; txtNGO_SchemeNo.Text = ""; txtNGO_Debit.Text = ""; txtNGO_Debit_CCY.Text = ""; txtNGO_Debit_Amt.Text = ""; txtNGO_Debit_Cust.Text = "";
                txtNGO_Debit_Cust_Name.Text = ""; txtNGO_Debit_Cust_AcCode.Text = ""; txtNGO_Debit_Cust_AcCode_Name.Text = ""; txtNGO_Debit_Cust_AccNo.Text = ""; txtNGO_Debit_ExchRate.Text = ""; txtNGO_Debit_ExchCCY.Text = "";
                txtNGO_Debit_Fund.Text = ""; txtNGO_Debit_CheckNo.Text = ""; txtNGO_Debit_Available.Text = ""; txtNGO_Debit_Advice_Print.Text = ""; txtNGO_Debit_Details.Text = "";
                txtNGO_Debit_Entity.Text = ""; txtNGO_Debit_Division.Text = ""; txtNGO_Debit_InterAmt.Text = ""; txtNGO_Debit_InterRate.Text = ""; txtNGO_Credit.Text = ""; txtNGO_Credit_CCY.Text = "";
                txtNGO_Credit_Amt.Text = ""; txtNGO_Credit_Cust.Text = ""; txtNGO_Credit_Cust_Name.Text = ""; txtNGO_Credit_Cust_AcCode.Text = ""; txtNGO_Credit_Cust_AcCode_Name.Text = ""; txtNGO_Credit_Cust_AccNo.Text = "";
                txtNGO_Credit_ExchRate.Text = ""; txtNGO_Credit_ExchCCY.Text = ""; txtNGO_Credit_Fund.Text = ""; txtNGO_Credit_CheckNo.Text = ""; txtNGO_Credit_Available.Text = "";
                txtNGO_Credit_Advice_Print.Text = ""; txtNGO_Credit_Details.Text = ""; txtNGO_Credit_Entity.Text = ""; txtNGO_Credit_Division.Text = ""; txtNGO_Credit_InterAmt.Text = ""; txtNGO_Credit_InterRate.Text = "";
                break;
            case "INTER":
                txtIO_ValueDate.Text = ""; txt_GOAccChange_Ref_No.Text = ""; txt_GOAccChange_Comment.Text = ""; txt_GOAccChange_SectionNo.Text = ""; txt_GOAccChange_Remarks.Text = "";
                txt_GOAccChange_Memo.Text = ""; txt_GOAccChange_Scheme_no.Text = ""; txt_GOAccChange_Debit_Code.Text = ""; txt_GOAccChange_Debit_Curr.Text = ""; txt_GOAccChange_Debit_Amt.Text = ""; txt_GOAccChange_Debit_Cust.Text = "";
                txt_GOAccChange_Debit_Cust_Name.Text = ""; txt_GOAccChange_Debit_Cust_AcCode.Text = ""; txt_GOAccChange_Debit_Cust_AcCode_Name.Text = ""; txt_GOAccChange_Debit_Cust_AccNo.Text = ""; txt_GOAccChange_Debit_Exch_Rate.Text = ""; txt_GOAccChange_Debit_Exch_CCY.Text = "";
                txt_GOAccChange_Debit_FUND.Text = ""; txt_GOAccChange_Debit_Check_No.Text = ""; txt_GOAccChange_Debit_Available.Text = ""; txt_GOAccChange_Debit_AdPrint.Text = ""; txt_GOAccChange_Debit_Details.Text = "";
                txt_GOAccChange_Debit_Entity.Text = ""; txt_GOAccChange_Debit_Division.Text = ""; txt_GOAccChange_Debit_Inter_Amount.Text = ""; txt_GOAccChange_Debit_Inter_Rate.Text = ""; txt_GOAccChange_Credit_Code.Text = ""; txt_GOAccChange_Credit_Curr.Text = "";
                txt_GOAccChange_Credit_Amt.Text = ""; txt_GOAccChange_Credit_Cust.Text = ""; txt_GOAccChange_Credit_Cust_Name.Text = ""; txt_GOAccChange_Credit_Cust_AcCode.Text = ""; txt_GOAccChange_Credit_Cust_AcCode_Name.Text = ""; txt_GOAccChange_Credit_Cust_AccNo.Text = "";
                txt_GOAccChange_Credit_Exch_Rate.Text = ""; txt_GOAccChange_Credit_Exch_Curr.Text = ""; txt_GOAccChange_Credit_FUND.Text = ""; txt_GOAccChange_Credit_Check_No.Text = ""; txt_GOAccChange_Credit_Available.Text = "";
                txt_GOAccChange_Credit_AdPrint.Text = ""; txt_GOAccChange_Credit_Details.Text = ""; txt_GOAccChange_Credit_Entity.Text = ""; txt_GOAccChange_Credit_Division.Text = ""; txt_GOAccChange_Credit_Inter_Amount.Text = ""; txt_GOAccChange_Credit_Inter_Rate.Text = "";
                break;
        }
    }

    protected void btnTTCancel1_Click(object sender, EventArgs e)
    {
        txtTTRefNo1.Text = "";
        lblTTRname1.Visible = false;
        ddlTTCurrency1.SelectedValue = "0";
        txtTotTTAmt1.Text = "";
        txtBalTTAmt1.Text = "";
        txtTTAmount1.Text = "";
        ddlTTRealisedCurr1.SelectedValue = "0";
        txtTTCrossCurrRate1.Text = "";
        txtTTAmtRealised1.Text = "";
    }
    protected void btnTTCancel2_Click(object sender, EventArgs e)
    {
        txtTTRefNo2.Text = "";
        lblTTRname2.Visible = false;
        ddlTTCurrency2.SelectedValue = "0";
        txtTotTTAmt2.Text = "";
        txtBalTTAmt2.Text = "";
        txtTTAmount2.Text = "";
        ddlTTRealisedCurr2.SelectedValue = "0";
        txtTTCrossCurrRate2.Text = "";
        txtTTAmtRealised2.Text = "";
    }
    protected void btnTTCancel3_Click(object sender, EventArgs e)
    {
        txtTTRefNo3.Text = "";
        lblTTRname3.Visible = false;
        ddlTTCurrency3.SelectedValue = "0";
        txtTotTTAmt3.Text = "";
        txtBalTTAmt3.Text = "";
        txtTTAmount3.Text = "";
        ddlTTRealisedCurr3.SelectedValue = "0";
        txtTTCrossCurrRate3.Text = "";
        txtTTAmtRealised3.Text = "";
    }
    protected void btnTTCancel4_Click(object sender, EventArgs e)
    {
        txtTTRefNo4.Text = "";
        lblTTRname4.Visible = false;
        ddlTTCurrency4.SelectedValue = "0";
        txtTotTTAmt4.Text = "";
        txtBalTTAmt4.Text = "";
        txtTTAmount4.Text = "";
        ddlTTRealisedCurr4.SelectedValue = "0";
        txtTTCrossCurrRate4.Text = "";
        txtTTAmtRealised4.Text = "";
    }
    protected void btnTTCancel5_Click(object sender, EventArgs e)
    {
        txtTTRefNo5.Text = "";
        lblTTRname5.Visible = false;
        ddlTTCurrency5.SelectedValue = "0";
        txtTotTAmt5.Text = "";
        txtBalTTAmt5.Text = "";
        txtTTAmount5.Text = "";
        ddlTTRealisedCurr5.SelectedValue = "0";
        txtTTCrossCurrRate5.Text = "";
        txtTTAmtRealised5.Text = "";
    }
    protected void btnTTCancel6_Click(object sender, EventArgs e)
    {
        txtTTRefNo6.Text = "";
        lblTTRname6.Visible = false;
        ddlTTCurrency6.SelectedValue = "0";
        txtTotTTAmt6.Text = "";
        txtBalTTAmt6.Text = "";
        txtTTAmount6.Text = "";
        ddlTTRealisedCurr6.SelectedValue = "0";
        txtTTCrossCurrRate6.Text = "";
        txtTTAmtRealised6.Text = "";
    }
    protected void btnTTCancel7_Click(object sender, EventArgs e)
    {
        txtTTRefNo7.Text = "";
        lblTTRname7.Visible = false;
        ddlTTCurrency7.SelectedValue = "0";
        txtTotTTAmt7.Text = "";
        txtBalTTAmt7.Text = "";
        txtTTAmount7.Text = "";
        ddlTTRealisedCurr7.SelectedValue = "0";
        txtTTCrossCurrRate7.Text = "";
        txtTTAmtRealised7.Text = "";
    }
    protected void btnTTCancel8_Click(object sender, EventArgs e)
    {
        txtTTRefNo8.Text = "";
        lblTTRname8.Visible = false;
        ddlTTCurrency8.SelectedValue = "0";
        txtTotTTAmt8.Text = "";
        txtBalTAmt8.Text = "";
        txtTTAmount8.Text = "";
        ddlTTRealisedCurr8.SelectedValue = "0";
        txtTTCrossCurrRate8.Text = "";
        txtTTAmtRealised8.Text = "";
    }
    protected void btnTTCancel9_Click(object sender, EventArgs e)
    {
        txtTTRefNo9.Text = "";
        lblTTRname9.Visible = false;
        ddlTTCurrency9.SelectedValue = "0";
        txtTtTTAmt9.Text = "";
        txtBalTTAmt9.Text = "";
        txtTTAmount9.Text = "";
        ddlTTRealisedCurr9.SelectedValue = "0";
        txtTTCrossCurrRate9.Text = "";
        txtTTAmtRealised9.Text = "";
    }
    protected void btnTTCancel10_Click(object sender, EventArgs e)
    {
        txtTTRefNo10.Text = "";
        lblTTRname10.Visible = false;
        ddlTTCurrency10.SelectedValue = "0";
        txtTotTTAmt10.Text = "";
        txtBalTTAmt10.Text = "";
        txtTTAmount10.Text = "";
        ddlTTRealisedCurr10.SelectedValue = "0";
        txtTTCrossCurrRate10.Text = "";
        txtTTAmtRealised10.Text = "";
    }
    protected void btnTTCancel11_Click(object sender, EventArgs e)
    {
        txtTTRefNo11.Text = "";
        lblTTRname11.Visible = false;
        ddlTTCurrency11.SelectedValue = "0";
        txtTotTTAmt11.Text = "";
        txtBalTTAmt11.Text = "";
        txtTTAmount11.Text = "";
        ddlTTRealisedCurr11.SelectedValue = "0";
        txtTTCrossCurrRate11.Text = "";
        txtTTAmtRealised11.Text = "";
    }
    protected void btnTTCancel12_Click(object sender, EventArgs e)
    {
        txtTTRefNo12.Text = "";
        lblTTRname12.Visible = false;
        ddlTTCurrency12.SelectedValue = "0";
        txtTotTTAmt12.Text = "";
        txtBalTTAmt12.Text = "";
        txtTTAmount12.Text = "";
        ddlTTRealisedCurr12.SelectedValue = "0";
        txtTTCrossCurrRate12.Text = "";
        txtTTAmtRealised12.Text = "";
    }
    protected void btnTTCancel13_Click(object sender, EventArgs e)
    {
        txtTTRefNo13.Text = "";
        lblTTRname13.Visible = false;
        ddlTTCurrency13.SelectedValue = "0";
        txtTotTTAmt13.Text = "";
        txtBalTTAmt13.Text = "";
        txtTTAmount13.Text = "";
        ddlTTRealisedCurr13.SelectedValue = "0";
        txtTTCrossCurrRate13.Text = "";
        txtTTAmtRealised13.Text = "";
    }
    protected void btnTTCancel14_Click(object sender, EventArgs e)
    {
        txtTTRefNo14.Text = "";
        lblTTRname14.Visible = false;
        ddlTTCurrency14.SelectedValue = "0";
        txtTotTTAmt14.Text = "";
        txtBalTTAmt14.Text = "";
        txtTTAmount14.Text = "";
        ddlTTRealisedCurr14.SelectedValue = "0";
        txtTTCrossCurrRate14.Text = "";
        txtTTAmtRealised14.Text = "";
    }
    protected void btnTTCancel15_Click(object sender, EventArgs e)
    {
        txtTTRefNo15.Text = "";
        lblTTRname15.Visible = false;
        ddlTTCurrency15.SelectedValue = "0";
        txtTotTTAmt15.Text = "";
        txtBalTTAmt15.Text = "";
        txtTTAmount15.Text = "";
        ddlTTRealisedCurr15.SelectedValue = "0";
        txtTTCrossCurrRate15.Text = "";
        txtTTAmtRealised15.Text = "";
    }

    public void LocalTransField()
    {
        txtExchangeRate.Enabled = txtAmtRealisedinINR.Enabled = txtPcfcAmt.Enabled = txtEEFCAmt.Enabled = txt_relamount.Enabled = txtRelCrossCurRate.Enabled = txtRemitterName.Enabled = txtRemitterAddress.Enabled =
        txtRemitterCountry.Enabled = txtSwiftCode.Enabled = btnHelpSwiftCode.Enabled = txtRemitterBank.Enabled = Button4.Enabled = Button5.Enabled = txtRemitterBankAddress.Enabled = txtRemBankCountry.Enabled = txtPurposeCode.Enabled = btnpurposecode.Enabled = txtpurposeofRemittance.Enabled =
        ddlModeOfPayment.Enabled = chkFirc.Enabled = txtFircNo.Enabled = txtFircAdCode.Enabled = rdbShipper.Enabled = rdbBuyer.Enabled = txtIntFrmDate1.Enabled = txtIntToDate1.Enabled = txtForDays1.Enabled = txtIntRate1.Enabled = txtInterestAmt.Enabled = false;
        lblLinkTTReferenceNo.Visible = btnTTRefNoList.Visible = panelSecondAdd.Visible = false; lblCreateIRM.Visible = false; chkIRMCreate.Visible = false;

    }

    protected void CheckerViewOnly()
    {
        //////////////////////////////// Document Details ////////////////////////
        txtDocNo.Enabled = btnDocNo.Enabled = txtSrNo.Enabled = txtDateReceived.Enabled = txtProcessingDate.Enabled = txtCustAcNo.Enabled = txtOverseasParty.Enabled = txtOverseasPartyCountry.Enabled = txtconsigneePartyID.Enabled = txtconsigneePartyCountry.Enabled = txtOverseasBank.Enabled =
        txtOverseasBankCountry.Enabled = txtCurrency.Enabled = txtBillAmt.Enabled = txtDueDate.Enabled = txtBillAmtinINR.Enabled = txtOutstandingAmt.Enabled = rdbFull.Enabled = rdbPart.Enabled = txtValueDate.Enabled = btncalendar_ValueDate.Enabled = txtDateRealised.Enabled =
        btncalendar_DocDate.Enabled = txt_relcur.Enabled = btn_recurrhelp.Enabled = txtInstructedAmt.Enabled = txtAmtRealised.Enabled = txt_fbkcharges.Enabled = txtExchangeRate.Enabled = txtAmtRealisedinINR.Enabled = txtLeiInrAmt.Enabled =
        txtPcfcAmt.Enabled = txtEEFCAmt.Enabled = txt_relamount.Enabled = txtRelCrossCurRate.Enabled = txtRemitterName.Enabled = txtRemitterAddress.Enabled = txtRemitterCountry.Enabled = Button4.Enabled = txtSwiftCode.Enabled = btnHelpSwiftCode.Enabled =
        txtRemitterBank.Enabled = txtRemitterBankAddress.Enabled = txtRemBankCountry.Enabled = Button5.Enabled = txtPurposeCode.Enabled = btnpurposecode.Enabled = txtpurposeofRemittance.Enabled = ddlModeOfPayment.Enabled = rdbShipper.Enabled =
        rdbBuyer.Enabled = txtIntFrmDate1.Enabled = txtIntToDate1.Enabled = txtForDays1.Enabled = txtIntRate1.Enabled = txtInterestAmt.Enabled = chkFirc.Enabled = txtFircNo.Enabled = txtFircAdCode.Enabled = txtRemark.Enabled =
        chkIRMCreate.Enabled = btnTTRefNoList.Enabled = chkDummySettlement.Enabled = txtBankUniqueTransactionID.Enabled = txtIFSCCode.Enabled = txtRemittanceADCode.Enabled = txtIECCode.Enabled = txtPanNumber.Enabled = txtBankReferencenumber.Enabled =
        txtBankAccountNumber.Enabled = ddlIRMStatus.Enabled = false;

        btnTTRef1.Enabled = txtTTRefNo1.Enabled = ddlTTCurrency1.Enabled = txtTotTTAmt1.Enabled = txtBalTTAmt1.Enabled = txtTTAmount1.Enabled = ddlTTRealisedCurr1.Enabled = txtTTCrossCurrRate1.Enabled = txtTTAmtRealised1.Enabled = false;
        btnTTRef2.Enabled = txtTTRefNo2.Enabled = ddlTTCurrency2.Enabled = txtTotTTAmt2.Enabled = txtBalTTAmt2.Enabled = txtTTAmount2.Enabled = ddlTTRealisedCurr2.Enabled = txtTTCrossCurrRate2.Enabled = txtTTAmtRealised2.Enabled = false;
        btnTTRef3.Enabled = txtTTRefNo3.Enabled = ddlTTCurrency3.Enabled = txtTotTTAmt3.Enabled = txtBalTTAmt3.Enabled = txtTTAmount3.Enabled = ddlTTRealisedCurr3.Enabled = txtTTCrossCurrRate3.Enabled = txtTTAmtRealised3.Enabled = false;
        btnTTRef4.Enabled = txtTTRefNo4.Enabled = ddlTTCurrency4.Enabled = txtTotTTAmt4.Enabled = txtBalTTAmt4.Enabled = txtTTAmount4.Enabled = ddlTTRealisedCurr4.Enabled = txtTTCrossCurrRate4.Enabled = txtTTAmtRealised4.Enabled = false;
        btnTTRef5.Enabled = txtTTRefNo5.Enabled = ddlTTCurrency5.Enabled = txtTotTAmt5.Enabled = txtBalTTAmt5.Enabled = txtTTAmount5.Enabled = ddlTTRealisedCurr5.Enabled = txtTTCrossCurrRate5.Enabled = txtTTAmtRealised5.Enabled = false;
        btnTTRef6.Enabled = txtTTRefNo6.Enabled = ddlTTCurrency6.Enabled = txtTotTTAmt6.Enabled = txtBalTTAmt6.Enabled = txtTTAmount6.Enabled = ddlTTRealisedCurr6.Enabled = txtTTCrossCurrRate6.Enabled = txtTTAmtRealised6.Enabled = false;
        btnTTRef7.Enabled = txtTTRefNo7.Enabled = ddlTTCurrency7.Enabled = txtTotTTAmt7.Enabled = txtBalTTAmt7.Enabled = txtTTAmount7.Enabled = ddlTTRealisedCurr7.Enabled = txtTTCrossCurrRate7.Enabled = txtTTAmtRealised7.Enabled = false;
        btnTTRef8.Enabled = txtTTRefNo8.Enabled = ddlTTCurrency8.Enabled = txtTotTTAmt8.Enabled = txtBalTAmt8.Enabled = txtTTAmount8.Enabled = ddlTTRealisedCurr8.Enabled = txtTTCrossCurrRate8.Enabled = txtTTAmtRealised8.Enabled = false;
        btnTTRef9.Enabled = txtTTRefNo9.Enabled = ddlTTCurrency9.Enabled = txtTtTTAmt9.Enabled = txtBalTTAmt9.Enabled = txtTTAmount9.Enabled = ddlTTRealisedCurr9.Enabled = txtTTCrossCurrRate9.Enabled = txtTTAmtRealised9.Enabled = false;
        btnTTRef10.Enabled = txtTTRefNo10.Enabled = ddlTTCurrency10.Enabled = txtTotTTAmt10.Enabled = txtBalTTAmt10.Enabled = txtTTAmount10.Enabled = ddlTTRealisedCurr10.Enabled = txtTTCrossCurrRate10.Enabled = txtTTAmtRealised10.Enabled = false;
        btnTTRef11.Enabled = txtTTRefNo11.Enabled = ddlTTCurrency11.Enabled = txtTotTTAmt11.Enabled = txtBalTTAmt11.Enabled = txtTTAmount11.Enabled = ddlTTRealisedCurr11.Enabled = txtTTCrossCurrRate11.Enabled = txtTTAmtRealised11.Enabled = false;
        btnTTRef12.Enabled = txtTTRefNo12.Enabled = ddlTTCurrency12.Enabled = txtTotTTAmt12.Enabled = txtBalTTAmt12.Enabled = txtTTAmount12.Enabled = ddlTTRealisedCurr12.Enabled = txtTTCrossCurrRate12.Enabled = txtTTAmtRealised12.Enabled = false;
        btnTTRef13.Enabled = txtTTRefNo13.Enabled = ddlTTCurrency13.Enabled = txtTotTTAmt13.Enabled = txtBalTTAmt13.Enabled = txtTTAmount13.Enabled = ddlTTRealisedCurr13.Enabled = txtTTCrossCurrRate13.Enabled = txtTTAmtRealised13.Enabled = false;
        btnTTRef14.Enabled = txtTTRefNo14.Enabled = ddlTTCurrency14.Enabled = txtTotTTAmt14.Enabled = txtBalTTAmt14.Enabled = txtTTAmount14.Enabled = ddlTTRealisedCurr14.Enabled = txtTTCrossCurrRate14.Enabled = txtTTAmtRealised14.Enabled = false;
        btnTTRef15.Enabled = txtTTRefNo15.Enabled = ddlTTCurrency15.Enabled = txtTotTTAmt15.Enabled = txtBalTTAmt15.Enabled = txtTTAmount15.Enabled = ddlTTRealisedCurr15.Enabled = txtTTCrossCurrRate15.Enabled = txtTTAmtRealised15.Enabled = false;
        btnTTCancel1.Enabled = btnTTCancel2.Enabled = btnTTCancel3.Enabled = btnTTCancel4.Enabled = btnTTCancel5.Enabled = btnTTCancel6.Enabled = btnTTCancel7.Enabled = btnTTCancel8.Enabled = btnTTCancel9.Enabled = btnTTCancel10.Enabled = btnTTCancel11.Enabled = btnTTCancel12.Enabled = btnTTCancel13.Enabled = btnTTCancel14.Enabled = btnTTCancel15.Enabled = false;

        chkSB.Enabled = chk_IMPACC1Flag.Enabled = btn_IMPACC1_FCRefNo_help.Enabled = btn_IMPACC1_CR_Code_help.Enabled = btn_IMPACC1_DR_Code_help.Enabled = false;
        txt_IMPACC1_FCRefNo.Enabled = txt_IMPACC1_DiscAmt.Enabled = txt_IMPACC1_Princ_matu.Enabled = txt_IMPACC1_Princ_lump.Enabled = txt_IMPACC1_Princ_Contract_no.Enabled = txt_IMPACC1_Princ_Ex_Curr.Enabled = txt_IMPACC1_Princ_Ex_rate.Enabled = txt_IMPACC1_Princ_Intnl_Ex_rate.Enabled =
        txt_IMPACC1_Interest_matu.Enabled = txt_IMPACC1_Interest_lump.Enabled = txt_IMPACC1_Interest_Contract_no.Enabled = txt_IMPACC1_Interest_Ex_Curr.Enabled = txt_IMPACC1_Interest_Ex_rate.Enabled = txt_IMPACC1_Interest_Intnl_Ex_rate.Enabled =
        txt_IMPACC1_Commission_matu.Enabled = txt_IMPACC1_Commission_lump.Enabled = txt_IMPACC1_Commission_Contract_no.Enabled = txt_IMPACC1_Commission_Ex_Curr.Enabled = txt_IMPACC1_Commission_Ex_rate.Enabled = txt_IMPACC1_Commission_Intnl_Ex_rate.Enabled =
        txt_IMPACC1_Their_Commission_matu.Enabled = txt_IMPACC1_Their_Commission_lump.Enabled = txt_IMPACC1_Their_Commission_Contract_no.Enabled = txt_IMPACC1_Their_Commission_Ex_Curr.Enabled = txt_IMPACC1_Their_Commission_Ex_rate.Enabled =
        txt_IMPACC1_Their_Commission_Intnl_Ex_rate.Enabled = txt_IMPACC1_CR_Code.Enabled = txt_IMPACC1_CR_AC_Short_Name.Enabled = txt_IMPACC1_CR_Cust_abbr.Enabled = txt_IMPACC1_CR_Cust_Acc.Enabled = txt_IMPACC1_CR_Acceptance_Curr.Enabled =
        txt_IMPACC1_CR_Acceptance_amt.Enabled = txt_IMPACC1_CR_Acceptance_payer.Enabled = txt_IMPACC1_CR_Interest_Curr.Enabled = txt_IMPACC1_CR_Interest_amt.Enabled = txt_IMPACC1_CR_Interest_payer.Enabled = txt_IMPACC1_CR_Accept_Commission_Curr.Enabled =
        txt_IMPACC1_CR_Accept_Commission_amt.Enabled = txt_IMPACC1_CR_Accept_Commission_Payer.Enabled = txt_IMPACC1_CR_Pay_Handle_Commission_Curr.Enabled = txt_IMPACC1_CR_Pay_Handle_Commission_amt.Enabled = txt_IMPACC1_CR_Pay_Handle_Commission_Payer.Enabled =
        txt_IMPACC1_CR_Others_Curr.Enabled = txt_IMPACC1_CR_Others_amt.Enabled = txt_IMPACC1_CR_Others_Payer.Enabled = txt_IMPACC1_CR_Their_Commission_Curr.Enabled = txt_IMPACC1_CR_Their_Commission_amt.Enabled = txt_IMPACC1_CR_Their_Commission_Payer.Enabled =
        txt_IMPACC1_DR_Code.Enabled = txt_IMPACC1_DR_AC_Short_Name.Enabled = txt_IMPACC1_DR_Cust_abbr.Enabled = txt_IMPACC1_DR_Cust_Acc.Enabled = txt_IMPACC1_DR_Cur_Acc_Curr.Enabled = txt_IMPACC1_DR_Cur_Acc_amt.Enabled = txt_IMPACC1_DR_Cur_Acc_payer.Enabled =
        txt_IMPACC1_DR_Code2.Enabled = txt_IMPACC1_DR_AC_Short_Name2.Enabled = txt_IMPACC1_DR_Cust_abbr2.Enabled = txt_IMPACC1_DR_Cust_Acc2.Enabled = txt_IMPACC1_DR_Cur_Acc_Curr2.Enabled = txt_IMPACC1_DR_Cur_Acc_amt2.Enabled = txt_IMPACC1_DR_Cur_Acc_payer2.Enabled =
        txt_IMPACC1_DR_Code3.Enabled = txt_IMPACC1_DR_AC_Short_Name3.Enabled = txt_IMPACC1_DR_Cust_abbr3.Enabled = txt_IMPACC1_DR_Cust_Acc3.Enabled = txt_IMPACC1_DR_Cur_Acc_Curr3.Enabled = txt_IMPACC1_DR_Cur_Acc_amt3.Enabled = txt_IMPACC1_DR_Cur_Acc_payer3.Enabled =
        txt_IMPACC1_DR_Code4.Enabled = txt_IMPACC1_DR_AC_Short_Name4.Enabled = txt_IMPACC1_DR_Cust_abbr4.Enabled = txt_IMPACC1_DR_Cust_Acc4.Enabled = txt_IMPACC1_DR_Cur_Acc_Curr4.Enabled = txt_IMPACC1_DR_Cur_Acc_amt4.Enabled = txt_IMPACC1_DR_Cur_Acc_payer4.Enabled =
        txt_IMPACC1_DR_Code5.Enabled = txt_IMPACC1_DR_AC_Short_Name5.Enabled = txt_IMPACC1_DR_Cust_abbr5.Enabled = txt_IMPACC1_DR_Cust_Acc5.Enabled = txt_IMPACC1_DR_Cur_Acc_Curr5.Enabled = txt_IMPACC1_DR_Cur_Acc_amt5.Enabled = txt_IMPACC1_DR_Cur_Acc_payer5.Enabled =
        txt_IMPACC1_DR_Code6.Enabled = txt_IMPACC1_DR_AC_Short_Name6.Enabled = txt_IMPACC1_DR_Cust_abbr6.Enabled = txt_IMPACC1_DR_Cust_Acc6.Enabled = txt_IMPACC1_DR_Cur_Acc_Curr6.Enabled = txt_IMPACC1_DR_Cur_Acc_amt6.Enabled = txt_IMPACC1_DR_Cur_Acc_payer6.Enabled = false;

        chk_IMPACC2Flag.Enabled = btn_IMPACC2_FCRefNo_help.Enabled = btn_IMPACC2_CR_Code_help.Enabled = btn_IMPACC2_DR_Code_help.Enabled = false;
        txt_IMPACC2_FCRefNo.Enabled = txt_IMPACC2_DiscAmt.Enabled = txt_IMPACC2_Princ_matu.Enabled = txt_IMPACC2_Princ_lump.Enabled = txt_IMPACC2_Princ_Contract_no.Enabled = txt_IMPACC2_Princ_Ex_Curr.Enabled = txt_IMPACC2_Princ_Ex_rate.Enabled = txt_IMPACC2_Princ_Intnl_Ex_rate.Enabled =
        txt_IMPACC2_Interest_matu.Enabled = txt_IMPACC2_Interest_lump.Enabled = txt_IMPACC2_Interest_Contract_no.Enabled = txt_IMPACC2_Interest_Ex_Curr.Enabled = txt_IMPACC2_Interest_Ex_rate.Enabled = txt_IMPACC2_Interest_Intnl_Ex_rate.Enabled =
        txt_IMPACC2_Commission_matu.Enabled = txt_IMPACC2_Commission_lump.Enabled = txt_IMPACC2_Commission_Contract_no.Enabled = txt_IMPACC2_Commission_Ex_Curr.Enabled = txt_IMPACC2_Commission_Ex_rate.Enabled = txt_IMPACC2_Commission_Intnl_Ex_rate.Enabled =
        txt_IMPACC2_Their_Commission_matu.Enabled = txt_IMPACC2_Their_Commission_lump.Enabled = txt_IMPACC2_Their_Commission_Contract_no.Enabled = txt_IMPACC2_Their_Commission_Ex_Curr.Enabled = txt_IMPACC2_Their_Commission_Ex_rate.Enabled =
        txt_IMPACC2_Their_Commission_Intnl_Ex_rate.Enabled = txt_IMPACC2_CR_Code.Enabled = txt_IMPACC2_CR_AC_Short_Name.Enabled = txt_IMPACC2_CR_Cust_abbr.Enabled = txt_IMPACC2_CR_Cust_Acc.Enabled = txt_IMPACC2_CR_Acceptance_Curr.Enabled =
        txt_IMPACC2_CR_Acceptance_amt.Enabled = txt_IMPACC2_CR_Acceptance_payer.Enabled = txt_IMPACC2_CR_Interest_Curr.Enabled = txt_IMPACC2_CR_Interest_amt.Enabled = txt_IMPACC2_CR_Interest_payer.Enabled = txt_IMPACC2_CR_Accept_Commission_Curr.Enabled =
        txt_IMPACC2_CR_Accept_Commission_amt.Enabled = txt_IMPACC2_CR_Accept_Commission_Payer.Enabled = txt_IMPACC2_CR_Pay_Handle_Commission_Curr.Enabled = txt_IMPACC2_CR_Pay_Handle_Commission_amt.Enabled = txt_IMPACC2_CR_Pay_Handle_Commission_Payer.Enabled =
        txt_IMPACC2_CR_Others_Curr.Enabled = txt_IMPACC2_CR_Others_amt.Enabled = txt_IMPACC2_CR_Others_Payer.Enabled = txt_IMPACC2_CR_Their_Commission_Curr.Enabled = txt_IMPACC2_CR_Their_Commission_amt.Enabled = txt_IMPACC2_CR_Their_Commission_Payer.Enabled =
        txt_IMPACC2_DR_Code.Enabled = txt_IMPACC2_DR_AC_Short_Name.Enabled = txt_IMPACC2_DR_Cust_abbr.Enabled = txt_IMPACC2_DR_Cust_Acc.Enabled = txt_IMPACC2_DR_Cur_Acc_Curr.Enabled = txt_IMPACC2_DR_Cur_Acc_amt.Enabled = txt_IMPACC2_DR_Cur_Acc_payer.Enabled =
        txt_IMPACC2_DR_Code2.Enabled = txt_IMPACC2_DR_AC_Short_Name2.Enabled = txt_IMPACC2_DR_Cust_abbr2.Enabled = txt_IMPACC2_DR_Cust_Acc2.Enabled = txt_IMPACC2_DR_Cur_Acc_Curr2.Enabled = txt_IMPACC2_DR_Cur_Acc_amt2.Enabled = txt_IMPACC2_DR_Cur_Acc_payer2.Enabled =
        txt_IMPACC2_DR_Code3.Enabled = txt_IMPACC2_DR_AC_Short_Name3.Enabled = txt_IMPACC2_DR_Cust_abbr3.Enabled = txt_IMPACC2_DR_Cust_Acc3.Enabled = txt_IMPACC2_DR_Cur_Acc_Curr3.Enabled = txt_IMPACC2_DR_Cur_Acc_amt3.Enabled = txt_IMPACC2_DR_Cur_Acc_payer3.Enabled =
        txt_IMPACC2_DR_Code4.Enabled = txt_IMPACC2_DR_AC_Short_Name4.Enabled = txt_IMPACC2_DR_Cust_abbr4.Enabled = txt_IMPACC2_DR_Cust_Acc4.Enabled = txt_IMPACC2_DR_Cur_Acc_Curr4.Enabled = txt_IMPACC2_DR_Cur_Acc_amt4.Enabled = txt_IMPACC2_DR_Cur_Acc_payer4.Enabled =
        txt_IMPACC2_DR_Code5.Enabled = txt_IMPACC2_DR_AC_Short_Name5.Enabled = txt_IMPACC2_DR_Cust_abbr5.Enabled = txt_IMPACC2_DR_Cust_Acc5.Enabled = txt_IMPACC2_DR_Cur_Acc_Curr5.Enabled = txt_IMPACC2_DR_Cur_Acc_amt5.Enabled = txt_IMPACC2_DR_Cur_Acc_payer5.Enabled =
        txt_IMPACC2_DR_Code6.Enabled = txt_IMPACC2_DR_AC_Short_Name6.Enabled = txt_IMPACC2_DR_Cust_abbr6.Enabled = txt_IMPACC2_DR_Cust_Acc6.Enabled = txt_IMPACC2_DR_Cur_Acc_Curr6.Enabled = txt_IMPACC2_DR_Cur_Acc_amt6.Enabled = txt_IMPACC2_DR_Cur_Acc_payer6.Enabled = false;

        chk_IMPACC3Flag.Enabled = btn_IMPACC3_FCRefNo_help.Enabled = btn_IMPACC3_CR_Code_help.Enabled = btn_IMPACC3_DR_Code_help.Enabled = false;
        txt_IMPACC3_FCRefNo.Enabled = txt_IMPACC3_DiscAmt.Enabled = txt_IMPACC3_Princ_matu.Enabled = txt_IMPACC3_Princ_lump.Enabled = txt_IMPACC3_Princ_Contract_no.Enabled = txt_IMPACC3_Princ_Ex_Curr.Enabled = txt_IMPACC3_Princ_Ex_rate.Enabled = txt_IMPACC3_Princ_Intnl_Ex_rate.Enabled =
        txt_IMPACC3_Interest_matu.Enabled = txt_IMPACC3_Interest_lump.Enabled = txt_IMPACC3_Interest_Contract_no.Enabled = txt_IMPACC3_Interest_Ex_Curr.Enabled = txt_IMPACC3_Interest_Ex_rate.Enabled = txt_IMPACC3_Interest_Intnl_Ex_rate.Enabled =
        txt_IMPACC3_Commission_matu.Enabled = txt_IMPACC3_Commission_lump.Enabled = txt_IMPACC3_Commission_Contract_no.Enabled = txt_IMPACC3_Commission_Ex_Curr.Enabled = txt_IMPACC3_Commission_Ex_rate.Enabled = txt_IMPACC3_Commission_Intnl_Ex_rate.Enabled =
        txt_IMPACC3_Their_Commission_matu.Enabled = txt_IMPACC3_Their_Commission_lump.Enabled = txt_IMPACC3_Their_Commission_Contract_no.Enabled = txt_IMPACC3_Their_Commission_Ex_Curr.Enabled = txt_IMPACC3_Their_Commission_Ex_rate.Enabled =
        txt_IMPACC3_Their_Commission_Intnl_Ex_rate.Enabled = txt_IMPACC3_CR_Code.Enabled = txt_IMPACC3_CR_AC_Short_Name.Enabled = txt_IMPACC3_CR_Cust_abbr.Enabled = txt_IMPACC3_CR_Cust_Acc.Enabled = txt_IMPACC3_CR_Acceptance_Curr.Enabled =
        txt_IMPACC3_CR_Acceptance_amt.Enabled = txt_IMPACC3_CR_Acceptance_payer.Enabled = txt_IMPACC3_CR_Interest_Curr.Enabled = txt_IMPACC3_CR_Interest_amt.Enabled = txt_IMPACC3_CR_Interest_payer.Enabled = txt_IMPACC3_CR_Accept_Commission_Curr.Enabled =
        txt_IMPACC3_CR_Accept_Commission_amt.Enabled = txt_IMPACC3_CR_Accept_Commission_Payer.Enabled = txt_IMPACC3_CR_Pay_Handle_Commission_Curr.Enabled = txt_IMPACC3_CR_Pay_Handle_Commission_amt.Enabled = txt_IMPACC3_CR_Pay_Handle_Commission_Payer.Enabled =
        txt_IMPACC3_CR_Others_Curr.Enabled = txt_IMPACC3_CR_Others_amt.Enabled = txt_IMPACC3_CR_Others_Payer.Enabled = txt_IMPACC3_CR_Their_Commission_Curr.Enabled = txt_IMPACC3_CR_Their_Commission_amt.Enabled = txt_IMPACC3_CR_Their_Commission_Payer.Enabled =
        txt_IMPACC3_DR_Code.Enabled = txt_IMPACC3_DR_AC_Short_Name.Enabled = txt_IMPACC3_DR_Cust_abbr.Enabled = txt_IMPACC3_DR_Cust_Acc.Enabled = txt_IMPACC3_DR_Cur_Acc_Curr.Enabled = txt_IMPACC3_DR_Cur_Acc_amt.Enabled = txt_IMPACC3_DR_Cur_Acc_payer.Enabled =
        txt_IMPACC3_DR_Code2.Enabled = txt_IMPACC3_DR_AC_Short_Name2.Enabled = txt_IMPACC3_DR_Cust_abbr2.Enabled = txt_IMPACC3_DR_Cust_Acc2.Enabled = txt_IMPACC3_DR_Cur_Acc_Curr2.Enabled = txt_IMPACC3_DR_Cur_Acc_amt2.Enabled = txt_IMPACC3_DR_Cur_Acc_payer2.Enabled =
        txt_IMPACC3_DR_Code3.Enabled = txt_IMPACC3_DR_AC_Short_Name3.Enabled = txt_IMPACC3_DR_Cust_abbr3.Enabled = txt_IMPACC3_DR_Cust_Acc3.Enabled = txt_IMPACC3_DR_Cur_Acc_Curr3.Enabled = txt_IMPACC3_DR_Cur_Acc_amt3.Enabled = txt_IMPACC3_DR_Cur_Acc_payer3.Enabled =
        txt_IMPACC3_DR_Code4.Enabled = txt_IMPACC3_DR_AC_Short_Name4.Enabled = txt_IMPACC3_DR_Cust_abbr4.Enabled = txt_IMPACC3_DR_Cust_Acc4.Enabled = txt_IMPACC3_DR_Cur_Acc_Curr4.Enabled = txt_IMPACC3_DR_Cur_Acc_amt4.Enabled = txt_IMPACC3_DR_Cur_Acc_payer4.Enabled =
        txt_IMPACC3_DR_Code5.Enabled = txt_IMPACC3_DR_AC_Short_Name5.Enabled = txt_IMPACC3_DR_Cust_abbr5.Enabled = txt_IMPACC3_DR_Cust_Acc5.Enabled = txt_IMPACC3_DR_Cur_Acc_Curr5.Enabled = txt_IMPACC3_DR_Cur_Acc_amt5.Enabled = txt_IMPACC3_DR_Cur_Acc_payer5.Enabled =
        txt_IMPACC3_DR_Code6.Enabled = txt_IMPACC3_DR_AC_Short_Name6.Enabled = txt_IMPACC3_DR_Cust_abbr6.Enabled = txt_IMPACC3_DR_Cust_Acc6.Enabled = txt_IMPACC3_DR_Cur_Acc_Curr6.Enabled = txt_IMPACC3_DR_Cur_Acc_amt6.Enabled = txt_IMPACC3_DR_Cur_Acc_payer6.Enabled = false;

        chk_IMPACC4Flag.Enabled = btn_IMPACC4_FCRefNo_help.Enabled = btn_IMPACC4_CR_Code_help.Enabled = btn_IMPACC4_DR_Code_help.Enabled = false;
        txt_IMPACC4_FCRefNo.Enabled = txt_IMPACC4_DiscAmt.Enabled = txt_IMPACC4_Princ_matu.Enabled = txt_IMPACC4_Princ_lump.Enabled = txt_IMPACC4_Princ_Contract_no.Enabled = txt_IMPACC4_Princ_Ex_Curr.Enabled = txt_IMPACC4_Princ_Ex_rate.Enabled = txt_IMPACC4_Princ_Intnl_Ex_rate.Enabled =
        txt_IMPACC4_Interest_matu.Enabled = txt_IMPACC4_Interest_lump.Enabled = txt_IMPACC4_Interest_Contract_no.Enabled = txt_IMPACC4_Interest_Ex_Curr.Enabled = txt_IMPACC4_Interest_Ex_rate.Enabled = txt_IMPACC4_Interest_Intnl_Ex_rate.Enabled =
        txt_IMPACC4_Commission_matu.Enabled = txt_IMPACC4_Commission_lump.Enabled = txt_IMPACC4_Commission_Contract_no.Enabled = txt_IMPACC4_Commission_Ex_Curr.Enabled = txt_IMPACC4_Commission_Ex_rate.Enabled = txt_IMPACC4_Commission_Intnl_Ex_rate.Enabled =
        txt_IMPACC4_Their_Commission_matu.Enabled = txt_IMPACC4_Their_Commission_lump.Enabled = txt_IMPACC4_Their_Commission_Contract_no.Enabled = txt_IMPACC4_Their_Commission_Ex_Curr.Enabled = txt_IMPACC4_Their_Commission_Ex_rate.Enabled =
        txt_IMPACC4_Their_Commission_Intnl_Ex_rate.Enabled = txt_IMPACC4_CR_Code.Enabled = txt_IMPACC4_CR_AC_Short_Name.Enabled = txt_IMPACC4_CR_Cust_abbr.Enabled = txt_IMPACC4_CR_Cust_Acc.Enabled = txt_IMPACC4_CR_Acceptance_Curr.Enabled =
        txt_IMPACC4_CR_Acceptance_amt.Enabled = txt_IMPACC4_CR_Acceptance_payer.Enabled = txt_IMPACC4_CR_Interest_Curr.Enabled = txt_IMPACC4_CR_Interest_amt.Enabled = txt_IMPACC4_CR_Interest_payer.Enabled = txt_IMPACC4_CR_Accept_Commission_Curr.Enabled =
        txt_IMPACC4_CR_Accept_Commission_amt.Enabled = txt_IMPACC4_CR_Accept_Commission_Payer.Enabled = txt_IMPACC4_CR_Pay_Handle_Commission_Curr.Enabled = txt_IMPACC4_CR_Pay_Handle_Commission_amt.Enabled = txt_IMPACC4_CR_Pay_Handle_Commission_Payer.Enabled =
        txt_IMPACC4_CR_Others_Curr.Enabled = txt_IMPACC4_CR_Others_amt.Enabled = txt_IMPACC4_CR_Others_Payer.Enabled = txt_IMPACC4_CR_Their_Commission_Curr.Enabled = txt_IMPACC4_CR_Their_Commission_amt.Enabled = txt_IMPACC4_CR_Their_Commission_Payer.Enabled =
        txt_IMPACC4_DR_Code.Enabled = txt_IMPACC4_DR_AC_Short_Name.Enabled = txt_IMPACC4_DR_Cust_abbr.Enabled = txt_IMPACC4_DR_Cust_Acc.Enabled = txt_IMPACC4_DR_Cur_Acc_Curr.Enabled = txt_IMPACC4_DR_Cur_Acc_amt.Enabled = txt_IMPACC4_DR_Cur_Acc_payer.Enabled =
        txt_IMPACC4_DR_Code2.Enabled = txt_IMPACC4_DR_AC_Short_Name2.Enabled = txt_IMPACC4_DR_Cust_abbr2.Enabled = txt_IMPACC4_DR_Cust_Acc2.Enabled = txt_IMPACC4_DR_Cur_Acc_Curr2.Enabled = txt_IMPACC4_DR_Cur_Acc_amt2.Enabled = txt_IMPACC4_DR_Cur_Acc_payer2.Enabled =
        txt_IMPACC4_DR_Code3.Enabled = txt_IMPACC4_DR_AC_Short_Name3.Enabled = txt_IMPACC4_DR_Cust_abbr3.Enabled = txt_IMPACC4_DR_Cust_Acc3.Enabled = txt_IMPACC4_DR_Cur_Acc_Curr3.Enabled = txt_IMPACC4_DR_Cur_Acc_amt3.Enabled = txt_IMPACC4_DR_Cur_Acc_payer3.Enabled =
        txt_IMPACC4_DR_Code4.Enabled = txt_IMPACC4_DR_AC_Short_Name4.Enabled = txt_IMPACC4_DR_Cust_abbr4.Enabled = txt_IMPACC4_DR_Cust_Acc4.Enabled = txt_IMPACC4_DR_Cur_Acc_Curr4.Enabled = txt_IMPACC4_DR_Cur_Acc_amt4.Enabled = txt_IMPACC4_DR_Cur_Acc_payer4.Enabled =
        txt_IMPACC4_DR_Code5.Enabled = txt_IMPACC4_DR_AC_Short_Name5.Enabled = txt_IMPACC4_DR_Cust_abbr5.Enabled = txt_IMPACC4_DR_Cust_Acc5.Enabled = txt_IMPACC4_DR_Cur_Acc_Curr5.Enabled = txt_IMPACC4_DR_Cur_Acc_amt5.Enabled = txt_IMPACC4_DR_Cur_Acc_payer5.Enabled =
        txt_IMPACC4_DR_Code6.Enabled = txt_IMPACC4_DR_AC_Short_Name6.Enabled = txt_IMPACC4_DR_Cust_abbr6.Enabled = txt_IMPACC4_DR_Cust_Acc6.Enabled = txt_IMPACC4_DR_Cur_Acc_Curr6.Enabled = txt_IMPACC4_DR_Cur_Acc_amt6.Enabled = txt_IMPACC4_DR_Cur_Acc_payer6.Enabled = false;

        chk_IMPACC5Flag.Enabled = btn_IMPACC5_FCRefNo_help.Enabled = btn_IMPACC5_CR_Code_help.Enabled = btn_IMPACC5_DR_Code_help.Enabled = false;
        txt_IMPACC5_FCRefNo.Enabled = txt_IMPACC5_DiscAmt.Enabled = txt_IMPACC5_Princ_matu.Enabled = txt_IMPACC5_Princ_lump.Enabled = txt_IMPACC5_Princ_Contract_no.Enabled = txt_IMPACC5_Princ_Ex_Curr.Enabled = txt_IMPACC5_Princ_Ex_rate.Enabled = txt_IMPACC5_Princ_Intnl_Ex_rate.Enabled =
        txt_IMPACC5_Interest_matu.Enabled = txt_IMPACC5_Interest_lump.Enabled = txt_IMPACC5_Interest_Contract_no.Enabled = txt_IMPACC5_Interest_Ex_Curr.Enabled = txt_IMPACC5_Interest_Ex_rate.Enabled = txt_IMPACC5_Interest_Intnl_Ex_rate.Enabled =
        txt_IMPACC5_Commission_matu.Enabled = txt_IMPACC5_Commission_lump.Enabled = txt_IMPACC5_Commission_Contract_no.Enabled = txt_IMPACC5_Commission_Ex_Curr.Enabled = txt_IMPACC5_Commission_Ex_rate.Enabled = txt_IMPACC5_Commission_Intnl_Ex_rate.Enabled =
        txt_IMPACC5_Their_Commission_matu.Enabled = txt_IMPACC5_Their_Commission_lump.Enabled = txt_IMPACC5_Their_Commission_Contract_no.Enabled = txt_IMPACC5_Their_Commission_Ex_Curr.Enabled = txt_IMPACC5_Their_Commission_Ex_rate.Enabled =
        txt_IMPACC5_Their_Commission_Intnl_Ex_rate.Enabled = txt_IMPACC5_CR_Code.Enabled = txt_IMPACC5_CR_AC_Short_Name.Enabled = txt_IMPACC5_CR_Cust_abbr.Enabled = txt_IMPACC5_CR_Cust_Acc.Enabled = txt_IMPACC5_CR_Acceptance_Curr.Enabled =
        txt_IMPACC5_CR_Acceptance_amt.Enabled = txt_IMPACC5_CR_Acceptance_payer.Enabled = txt_IMPACC5_CR_Interest_Curr.Enabled = txt_IMPACC5_CR_Interest_amt.Enabled = txt_IMPACC5_CR_Interest_payer.Enabled = txt_IMPACC5_CR_Accept_Commission_Curr.Enabled =
        txt_IMPACC5_CR_Accept_Commission_amt.Enabled = txt_IMPACC5_CR_Accept_Commission_Payer.Enabled = txt_IMPACC5_CR_Pay_Handle_Commission_Curr.Enabled = txt_IMPACC5_CR_Pay_Handle_Commission_amt.Enabled = txt_IMPACC5_CR_Pay_Handle_Commission_Payer.Enabled =
        txt_IMPACC5_CR_Others_Curr.Enabled = txt_IMPACC5_CR_Others_amt.Enabled = txt_IMPACC5_CR_Others_Payer.Enabled = txt_IMPACC5_CR_Their_Commission_Curr.Enabled = txt_IMPACC5_CR_Their_Commission_amt.Enabled = txt_IMPACC5_CR_Their_Commission_Payer.Enabled =
        txt_IMPACC5_DR_Code.Enabled = txt_IMPACC5_DR_AC_Short_Name.Enabled = txt_IMPACC5_DR_Cust_abbr.Enabled = txt_IMPACC5_DR_Cust_Acc.Enabled = txt_IMPACC5_DR_Cur_Acc_Curr.Enabled = txt_IMPACC5_DR_Cur_Acc_amt.Enabled = txt_IMPACC5_DR_Cur_Acc_payer.Enabled =
        txt_IMPACC5_DR_Code2.Enabled = txt_IMPACC5_DR_AC_Short_Name2.Enabled = txt_IMPACC5_DR_Cust_abbr2.Enabled = txt_IMPACC5_DR_Cust_Acc2.Enabled = txt_IMPACC5_DR_Cur_Acc_Curr2.Enabled = txt_IMPACC5_DR_Cur_Acc_amt2.Enabled = txt_IMPACC5_DR_Cur_Acc_payer2.Enabled =
        txt_IMPACC5_DR_Code3.Enabled = txt_IMPACC5_DR_AC_Short_Name3.Enabled = txt_IMPACC5_DR_Cust_abbr3.Enabled = txt_IMPACC5_DR_Cust_Acc3.Enabled = txt_IMPACC5_DR_Cur_Acc_Curr3.Enabled = txt_IMPACC5_DR_Cur_Acc_amt3.Enabled = txt_IMPACC5_DR_Cur_Acc_payer3.Enabled =
        txt_IMPACC5_DR_Code4.Enabled = txt_IMPACC5_DR_AC_Short_Name4.Enabled = txt_IMPACC5_DR_Cust_abbr4.Enabled = txt_IMPACC5_DR_Cust_Acc4.Enabled = txt_IMPACC5_DR_Cur_Acc_Curr4.Enabled = txt_IMPACC5_DR_Cur_Acc_amt4.Enabled = txt_IMPACC5_DR_Cur_Acc_payer4.Enabled =
        txt_IMPACC5_DR_Code5.Enabled = txt_IMPACC5_DR_AC_Short_Name5.Enabled = txt_IMPACC5_DR_Cust_abbr5.Enabled = txt_IMPACC5_DR_Cust_Acc5.Enabled = txt_IMPACC5_DR_Cur_Acc_Curr5.Enabled = txt_IMPACC5_DR_Cur_Acc_amt5.Enabled = txt_IMPACC5_DR_Cur_Acc_payer5.Enabled =
        txt_IMPACC5_DR_Code6.Enabled = txt_IMPACC5_DR_AC_Short_Name6.Enabled = txt_IMPACC5_DR_Cust_abbr6.Enabled = txt_IMPACC5_DR_Cust_Acc6.Enabled = txt_IMPACC5_DR_Cur_Acc_Curr6.Enabled = txt_IMPACC5_DR_Cur_Acc_amt6.Enabled = txt_IMPACC5_DR_Cur_Acc_payer6.Enabled = false;

        chk_Generaloperation1.Enabled = btn_GO_Debit_AccCode_help.Enabled = btn_GO_Debit_GLCode_help.Enabled = btn_GO_Credit_GLCode_Help.Enabled = btn_GO_Credit_AccCode_Help.Enabled = false;
        txtGO_ValueDate.Enabled = txtGO_Comment.Enabled = txtGO_Section.Enabled = txtGO_Remark.Enabled =
        txtGO_Memo.Enabled = txtGO_SchemeNo.Enabled = txtGO_Debit.Enabled = txtGO_Debit_CCY.Enabled = txtGO_Debit_Amt.Enabled = txtGO_Debit_Cust.Enabled =
        txtGO_Debit_Cust_Name.Enabled = txtGO_Debit_Cust_AcCode.Enabled = txtGO_Debit_Cust_AcCode_Name.Enabled = txtGO_Debit_Cust_AccNo.Enabled = txtGO_Debit_ExchRate.Enabled = txtGO_Debit_ExchCCY.Enabled =
        txtGO_Debit_Fund.Enabled = txtGO_Debit_CheckNo.Enabled = txtGO_Debit_Available.Enabled = txtGO_Debit_Advice_Print.Enabled = txtGO_Debit_Details.Enabled =
        txtGO_Debit_Entity.Enabled = txtGO_Debit_Division.Enabled = txtGO_Debit_InterAmt.Enabled = txtGO_Debit_InterRate.Enabled = txtGO_Credit.Enabled = txtGO_Credit_CCY.Enabled =
        txtGO_Credit_Amt.Enabled = txtGO_Credit_Cust.Enabled = txtGO_Credit_Cust_Name.Enabled = txtGO_Credit_Cust_AcCode.Enabled = txtGO_Credit_Cust_AcCode_Name.Enabled = txtGO_Credit_Cust_AccNo.Enabled =
        txtGO_Credit_ExchRate.Enabled = txtGO_Credit_ExchCCY.Enabled = txtGO_Credit_Fund.Enabled = txtGO_Credit_CheckNo.Enabled = txtGO_Credit_Available.Enabled =
        txtGO_Credit_Advice_Print.Enabled = txtGO_Credit_Details.Enabled = txtGO_Credit_Entity.Enabled = txtGO_Credit_Division.Enabled = txtGO_Credit_InterAmt.Enabled = txtGO_Credit_InterRate.Enabled = false;

        chk_Generaloperation2.Enabled = btn_NGO_Debit_AccCode_help.Enabled = btn_NGO_Debit_GLCode_help.Enabled = btn_NGO_Credit_GLCode_Help.Enabled = btn_NGO_Credit_AccCode_Help.Enabled = false;
        txtNGO_ValueDate.Enabled = txtNGO_Comment.Enabled = txtNGO_Section.Enabled = txtNGO_Remark.Enabled =
        txtNGO_Memo.Enabled = txtNGO_SchemeNo.Enabled = txtNGO_Debit.Enabled = txtNGO_Debit_CCY.Enabled = txtNGO_Debit_Amt.Enabled = txtNGO_Debit_Cust.Enabled =
        txtNGO_Debit_Cust_Name.Enabled = txtNGO_Debit_Cust_AcCode.Enabled = txtNGO_Debit_Cust_AcCode_Name.Enabled = txtNGO_Debit_Cust_AccNo.Enabled = txtNGO_Debit_ExchRate.Enabled = txtNGO_Debit_ExchCCY.Enabled =
        txtNGO_Debit_Fund.Enabled = txtNGO_Debit_CheckNo.Enabled = txtNGO_Debit_Available.Enabled = txtNGO_Debit_Advice_Print.Enabled = txtNGO_Debit_Details.Enabled =
        txtNGO_Debit_Entity.Enabled = txtNGO_Debit_Division.Enabled = txtNGO_Debit_InterAmt.Enabled = txtNGO_Debit_InterRate.Enabled = txtNGO_Credit.Enabled = txtNGO_Credit_CCY.Enabled =
        txtNGO_Credit_Amt.Enabled = txtNGO_Credit_Cust.Enabled = txtNGO_Credit_Cust_Name.Enabled = txtNGO_Credit_Cust_AcCode.Enabled = txtNGO_Credit_Cust_AcCode_Name.Enabled = txtNGO_Credit_Cust_AccNo.Enabled =
        txtNGO_Credit_ExchRate.Enabled = txtNGO_Credit_ExchCCY.Enabled = txtNGO_Credit_Fund.Enabled = txtNGO_Credit_CheckNo.Enabled = txtNGO_Credit_Available.Enabled =
        txtNGO_Credit_Advice_Print.Enabled = txtNGO_Credit_Details.Enabled = txtNGO_Credit_Entity.Enabled = txtNGO_Credit_Division.Enabled = txtNGO_Credit_InterAmt.Enabled = txtNGO_Credit_InterRate.Enabled = false;

        chk_InterOffice.Enabled = btn_GOAccChange_Debit_AccCode_help.Enabled = btn_GOAccChange_Debit_GLCode_help.Enabled = btn_GOAccChange_Credit_GLCode_Help.Enabled = btn_GOAccChange_Credit_AccCode_Help.Enabled = false;
        txtIO_ValueDate.Enabled = txt_GOAccChange_Ref_No.Enabled = txt_GOAccChange_Comment.Enabled = txt_GOAccChange_SectionNo.Enabled = txt_GOAccChange_Remarks.Enabled =
        txt_GOAccChange_Memo.Enabled = txt_GOAccChange_Scheme_no.Enabled = txt_GOAccChange_Debit_Code.Enabled = txt_GOAccChange_Debit_Curr.Enabled = txt_GOAccChange_Debit_Amt.Enabled = txt_GOAccChange_Debit_Cust.Enabled =
        txt_GOAccChange_Debit_Cust_Name.Enabled = txt_GOAccChange_Debit_Cust_AcCode.Enabled = txt_GOAccChange_Debit_Cust_AcCode_Name.Enabled = txt_GOAccChange_Debit_Cust_AccNo.Enabled = txt_GOAccChange_Debit_Exch_Rate.Enabled = txt_GOAccChange_Debit_Exch_CCY.Enabled =
        txt_GOAccChange_Debit_FUND.Enabled = txt_GOAccChange_Debit_Check_No.Enabled = txt_GOAccChange_Debit_Available.Enabled = txt_GOAccChange_Debit_AdPrint.Enabled = txt_GOAccChange_Debit_Details.Enabled =
        txt_GOAccChange_Debit_Entity.Enabled = txt_GOAccChange_Debit_Division.Enabled = txt_GOAccChange_Debit_Inter_Amount.Enabled = txt_GOAccChange_Debit_Inter_Rate.Enabled = txt_GOAccChange_Credit_Code.Enabled = txt_GOAccChange_Credit_Curr.Enabled =
        txt_GOAccChange_Credit_Amt.Enabled = txt_GOAccChange_Credit_Cust.Enabled = txt_GOAccChange_Credit_Cust_Name.Enabled = txt_GOAccChange_Credit_Cust_AcCode.Enabled = txt_GOAccChange_Credit_Cust_AcCode_Name.Enabled = txt_GOAccChange_Credit_Cust_AccNo.Enabled =
        txt_GOAccChange_Credit_Exch_Rate.Enabled = txt_GOAccChange_Credit_Exch_Curr.Enabled = txt_GOAccChange_Credit_FUND.Enabled = txt_GOAccChange_Credit_Check_No.Enabled = txt_GOAccChange_Credit_Available.Enabled =
        txt_GOAccChange_Credit_AdPrint.Enabled = txt_GOAccChange_Credit_Details.Enabled = txt_GOAccChange_Credit_Entity.Enabled = txt_GOAccChange_Credit_Division.Enabled = txt_GOAccChange_Credit_Inter_Amount.Enabled = txt_GOAccChange_Credit_Inter_Rate.Enabled = false;



    }
    //--------------------------------------End--------------------------------------------------
}