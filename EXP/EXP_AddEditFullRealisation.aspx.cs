using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EXP_EXP_AddEditFullRealisation : System.Web.UI.Page
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

                chkManualGR.Attributes.Add("onclick", "return false;");
                if (Request.QueryString["mode"].Trim() != "add")
                {
                    txtDocNo.Text = Request.QueryString["DocNo"].ToString();
                    branchcode = Request.QueryString["BranchCode"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].ToString();
                    DocPrFx = Request.QueryString["DocPrFx"].ToString();
                    hdnBranchCode.Value = branchcode;
                    if (DocPrFx == "BLA")
                        rbtbla.Checked = true;
                    if (DocPrFx == "BLU")
                        rbtblu.Checked = true;
                    if (DocPrFx == "BBA")
                        rbtbba.Checked = true;
                    if (DocPrFx == "BBU")
                        rbtbbu.Checked = true;
                    if (DocPrFx == "BCA")
                        rbtbca.Checked = true;
                    if (DocPrFx == "BCU")
                        rbtbcu.Checked = true;
                    if (DocPrFx == "IBD")
                        rbtIBD.Checked = true;
                    if (DocPrFx == "LBC")
                        rbtLBC.Checked = true;
                    if (DocPrFx == "BEB")
                        rbtBEB.Checked = true;
                    txtDocPrFx.Value = DocPrFx;
                    hdnmode.Value = Request.QueryString["mode"].ToString();
                    SrNo = Request.QueryString["SrNo"].ToString();
                    txtSrNo.Text = SrNo;
                    fillDetails(txtDocNo.Text, SrNo);
                    Leicount = 0;
                    Check_LEI_ThresholdLimit();
                    btnDocNo.Visible = false;
                    txtDateRealised.Focus();
                }
                else
                {
                    DocPrFx = Request.QueryString["DocPrFx"].ToString();
                    txtForeignORLocal.Text = Request.QueryString["ForeignOrLocal"].ToString();
                    branchcode = Request.QueryString["BranchCode"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].ToString();
                    year = Request.QueryString["year"].ToString();
                    hdnmode.Value = Request.QueryString["mode"].ToString();
                    //if (DocPrFx == "N")
                    //    rdbNegotiation.Checked = true;
                    //if (DocPrFx == "P")
                    //    rdbPurchase.Checked = true;
                    //if (DocPrFx == "D")
                    //    rdbDiscount.Checked = true;
                    //if (DocPrFx == "E")
                    //    rdbEBR.Checked = true;
                    //if (DocPrFx == "C")
                    //    rdbCollection.Checked = true;
                    //if (DocPrFx == "M")
                    //    rdbMAdv.Checked = true;
                    //if (DocPrFx == "B")
                    //    rdbLBDbuyers.Checked = true;
                    //if (DocPrFx == "S")
                    //    rdbLBDsellers.Checked = true;
                    if (DocPrFx == "BLA")
                        rbtbla.Checked = true;
                    if (DocPrFx == "BLU")
                        rbtblu.Checked = true;
                    if (DocPrFx == "BBA")
                        rbtbba.Checked = true;
                    if (DocPrFx == "BBU")
                        rbtbbu.Checked = true;
                    if (DocPrFx == "BCA")
                        rbtbca.Checked = true;
                    if (DocPrFx == "BCU")
                        rbtbcu.Checked = true;
                    if (DocPrFx == "IBD")
                        rbtIBD.Checked = true;
                    if (DocPrFx == "LBC")
                        rbtLBC.Checked = true;
                    if (DocPrFx == "BEB")
                        rbtBEB.Checked = true;

                    txtDocPrFx.Value = DocPrFx;
                    hdnBranchCode.Value = branchcode;
                    hdnYear.Value = year;
                    txtDateRealised.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //rdbTransType.SelectedValue = "eefc";
                    //tran_type.Value = "EEFC";
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
                //txtDateRealised.Focus();
                //txtDocNo.Focus();
                txtDocNo.Attributes.Add("onblur", "return chkdocno();");
                txtExchangeRate.Attributes.Add("onblur", "return checkExchangeRate();");
                txtExchangeRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtRealised.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtRealised.Attributes.Add("onblur", "return amtrealised();");
                txtEEFCAmt.Attributes.Add("onblur", "return calrealisedAmtinINR();");

                rdbFull.Attributes.Add("onblur", "return calrealisedAmtinINR();");
                rdbPart.Attributes.Add("onblur", "return calrealisedAmtinINR();");

                txtCrossCurrRate.Attributes.Add("onblur", "return calcrosscurrTotal();");
                txtOtherBank.Attributes.Add("onblur", "return calotheramtininr();");
                txtCommission.Attributes.Add("onblur", "return calTax();");
                txtCourier.Attributes.Add("onblur", "return calTax();");
                txtSwift.Attributes.Add("onblur", "return calTax();");
                txtBankCertificate.Attributes.Add("onblur", "return calTax();");
                //btnTTNo.Attributes.Add("onclick", "return OpenDocNoList('mouseClick');");
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
                //txtInterestRate1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterestRate2.Attributes.Add("onkeydown", "return validate_Number(event);");
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
                txtprofitamt.Attributes.Add("onblur", "return calTax();");
                txtprofitamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                btnOverseasBankList.Attributes.Add("onclick", "return OpenOverseasBankList('mouseClick');");
                txtNoofDays2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmtRealisedinINR.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSBcesssamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_kkcessamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtsttamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFxDlsCommission.Attributes.Add("onkeydown", "return validate_Number(event);");
                txt_fbkcharges.Attributes.Add("onblur", "return fbkcal();");
                /*txtPCAmount1.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAmount2.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAmount3.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAmount4.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAmount5.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAmount6.Attributes.Add("onblur", "return CalculateTotalPC();");

                txtPCAcNo1.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAcNo2.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAcNo3.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAcNo4.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAcNo5.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCAcNo6.Attributes.Add("onblur", "return CalculateTotalPC();");

                txtPCsubAcNo1.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCsubAcNo2.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCsubAcNo3.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCsubAcNo4.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCsubAcNo5.Attributes.Add("onblur", "return CalculateTotalPC();");
                txtPCsubAcNo6.Attributes.Add("onblur", "return CalculateTotalPC();");

                btnPcAcNo1.Attributes.Add("onclick", "return OpenSubACList('1');");
                btnPcAcNo2.Attributes.Add("onclick", "return OpenSubACList('2');");
                btnPcAcNo3.Attributes.Add("onclick", "return OpenSubACList('3');");
                btnPcAcNo4.Attributes.Add("onclick", "return OpenSubACList('4');");
                btnPcAcNo5.Attributes.Add("onclick", "return OpenSubACList('5');");
                btnPcAcNo6.Attributes.Add("onclick", "return OpenSubACList('6');");

                txtPCsubAcNo1.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCsubAcNo2.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCsubAcNo3.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCsubAcNo4.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCsubAcNo5.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCsubAcNo6.Attributes.Add("onkeydown", "return validate_AcNo(event);");

                txtPCAcNo1.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCAcNo2.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCAcNo3.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCAcNo4.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCAcNo5.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                txtPCAcNo6.Attributes.Add("onkeydown", "return validate_AcNo(event);");*/

                //txtValueDate.Attributes.Add("onblur", "return checkSysDate1();");

                //txtTTRefNo1.Attributes.Add("onblur", "return checkITT();");
                //txtTTRefNo2.Attributes.Add("onblur", "return checkITT();");
                //txtTTRefNo3.Attributes.Add("onblur", "return checkITT();");
                //txtTTRefNo4.Attributes.Add("onblur", "return checkITT();");
                //txtTTRefNo5.Attributes.Add("onblur", "return checkITT();");

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
        string query = "TF_GetRealisedEntryDetailsNew";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);

        if (dt.Rows.Count > 0)
        {
            txtForeignORLocal.Text = dt.Rows[0]["ForeignORLocal"].ToString().Trim();

            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            fillCustomerIdDescription();
            txtOverseasBank.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString();
            fillIssuingBankDescription();
            txtOverseasParty.Text = dt.Rows[0]["Overseas_Party_Code"].ToString();
            fillOverseasPartyDescription();
            txtDateReceived.Text = dt.Rows[0]["Date_Received"].ToString();
            txtDueDate.Text = dt.Rows[0]["Due_Date"].ToString();
            txtDateNegotiated.Text = dt.Rows[0]["Date_Negotiated"].ToString();
            txtDateDelinked.Text = dt.Rows[0]["Delinked_Date"].ToString();
            txtAcceptedDueDate.Text = dt.Rows[0]["Accepted_Due_Date"].ToString();
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
                txtRelCrossCurRate.Text = Convert.ToDecimal(dt.Rows[0]["RelCrossCurRate"].ToString()).ToString("0.00");
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
            if (dt.Rows[0]["Realised_Current_Account_FC"].ToString() != "")
                txtEEFCAmt.Text = Convert.ToDecimal(dt.Rows[0]["Realised_Current_Account_FC"].ToString()).ToString("0.00");
            else
                txtEEFCAmt.Text = dt.Rows[0]["Realised_Current_Account_FC"].ToString();
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

            /* SqlParameter pPCdocNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
             pPCdocNo.Value = docno;btnEEFCCurrency
             SqlParameter pPCmodType = new SqlParameter("@modType", SqlDbType.VarChar);
             pPCmodType.Value = "EXPR";
             string _query = "TF_EXP_GetExportBillEntryDetails_Liquidation";
             TF_DATA objDataPC = new TF_DATA();
             DataTable dtPC = objDataPC.getData(_query, pPCdocNo, pPCmodType);
             if (dtPC.Rows.Count > 0)
             {
                 for (int i = 0; i < dtPC.Rows.Count; i++)
                 {
                     switch (i)
                     {
                         case 0:
                             txtPCAmount1.Text = dtPC.Rows[0]["PC_LiquiAmt"].ToString();
                             txtPCAcNo1.Text = dtPC.Rows[0]["PC_ACno"].ToString();
                             txtPCsubAcNo1.Text = dtPC.Rows[0]["SubAcNo"].ToString();
                             hdnPCsrNo1.Value = dtPC.Rows[0]["SrNo"].ToString();
                           //  hdnPCbalance1.Value = dtPC.Rows[0]["BalanceAmount"].ToString();
                             break;
                         case 1:
                             txtPCAmount2.Text = dtPC.Rows[1]["PC_LiquiAmt"].ToString();
                             txtPCAcNo2.Text = dtPC.Rows[1]["PC_ACno"].ToString();
                             txtPCsubAcNo2.Text = dtPC.Rows[1]["SubAcNo"].ToString();
                             hdnPCsrNo2.Value = dtPC.Rows[1]["SrNo"].ToString();
                           //  hdnPCbalance2.Value = dtPC.Rows[1]["BalanceAmount"].ToString();
                             break;
                         case 2:
                             txtPCAmount3.Text = dtPC.Rows[2]["PC_LiquiAmt"].ToString();
                             txtPCAcNo3.Text = dtPC.Rows[2]["PC_ACno"].ToString();
                             txtPCsubAcNo3.Text = dtPC.Rows[2]["SubAcNo"].ToString();
                             hdnPCsrNo3.Value = dtPC.Rows[2]["SrNo"].ToString();
                           //  hdnPCbalance3.Value = dtPC.Rows[2]["BalanceAmount"].ToString();
                             break;
                         case 3:
                             txtPCAmount4.Text = dtPC.Rows[3]["PC_LiquiAmt"].ToString();
                             txtPCAcNo4.Text = dtPC.Rows[3]["PC_ACno"].ToString();
                             txtPCsubAcNo4.Text = dtPC.Rows[3]["SubAcNo"].ToString();
                             hdnPCsrNo4.Value = dtPC.Rows[3]["SrNo"].ToString();
                           //  hdnPCbalance4.Value = dtPC.Rows[3]["BalanceAmount"].ToString();
                             break;
                         case 4:
                             txtPCAmount5.Text = dtPC.Rows[4]["PC_LiquiAmt"].ToString();
                             txtPCAcNo5.Text = dtPC.Rows[4]["PC_ACno"].ToString();
                             txtPCsubAcNo5.Text = dtPC.Rows[4]["SubAcNo"].ToString();
                             hdnPCsrNo5.Value = dtPC.Rows[4]["SrNo"].ToString();
                          //   hdnPCbalance5.Value = dtPC.Rows[4]["BalanceAmount"].ToString();
                             break;
                         case 5:
                             txtPCAmount6.Text = dtPC.Rows[5]["PC_LiquiAmt"].ToString();
                             txtPCAcNo6.Text = dtPC.Rows[5]["PC_ACno"].ToString();
                             txtPCsubAcNo6.Text = dtPC.Rows[5]["SubAcNo"].ToString();
                             hdnPCsrNo6.Value = dtPC.Rows[5]["SrNo"].ToString();
                           //  hdnPCbalance6.Value = dtPC.Rows[5]["BalanceAmount"].ToString();
                             break;
                     }
                 }chkStax

             }*/
            fillReceivingBank(txtCurrency.Text);
            ddlAccountType.DataTextField = dt.Rows[0]["AccountType"].ToString();
            ddlAccountType.DataValueField = dt.Rows[0]["AccountType"].ToString();

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

            //ddlAccountType.DataBind();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewRealisationEntry.aspx", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewRealisationEntry.aspx", true);
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
        string query = "TF_GetDocNoDetailsforExportRealisationNew";
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
            //if (billtype == "S")
            //    txtBillType.Text = "Sight Bill";
            //else
            //    txtBillType.Text = "Usance Bill";
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

            if (lblCustDesc.Text.Length > 20)
            {
                lblCustDesc.ToolTip = lblCustDesc.Text;
                lblCustDesc.Text = lblCustDesc.Text;
                lblCustDesc.Text = lblCustDesc.Text.Substring(0, 16) + "...";
            }
        }
        else
        {
            txtCustAcNo.Text = "";
            lblCustDesc.Text = "INVALID ID";
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
            if (lblOverseasBank.Text.Length > 20)
            {
                lblOverseasBank.ToolTip = lblOverseasBank.Text;
                lblOverseasBank.Text = lblOverseasBank.Text.Substring(0, 16) + "...";
            }
            txtSwiftCode.Text = dt.Rows[0]["SwiftCode"].ToString().Trim();

        }
        else
        {
            txtOverseasBank.Text = "";
            txtSwiftCode.Text = "";
            lblOverseasBank.Text = "INVALID ID";
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
            if (lblOverseasParty.Text.Length > 20)
            {
                lblOverseasParty.ToolTip = lblOverseasParty.Text;
                lblOverseasParty.Text = lblOverseasParty.Text.Substring(0, 16) + "...";
            }
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

            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "serviceTax", "calTax();", true);

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

        Check_LEI_ThresholdLimit();

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
        // string sr = txtSrNo.Text;
        // int srno = 0;
        // if (sr != "")
        //     srno = Convert.ToInt32(sr);
        // //if (srno == 1 || chkLoanAdvanced.Checked == false)
        // DocPrFx = Request.QueryString["DocPrFx"].ToString();
        //// if (srno == 1 && (DocPrFx == "BCA" || DocPrFx == "BCU" ))
        // if ((DocPrFx == "BCA" || DocPrFx == "BCU") && rdbFull.Checked==true)
        // {
        //     TF_DATA objData = new TF_DATA();
        //     string query1 = "TF_Export_CommissionCal_Realisation";
        //     decimal negoamtininr, negoamt = 0, exrate = 0;
        //     if (txtNegotiatedAmt.Text != "")
        //         negoamt = Convert.ToDecimal(txtNegotiatedAmt.Text);
        //     if (txtExchangeRate.Text != "")
        //         exrate = Convert.ToDecimal(txtExchangeRate.Text);
        //     negoamtininr = negoamt * exrate;
        //     SqlParameter p3 = new SqlParameter("@negoamtinr", SqlDbType.VarChar);
        //     p3.Value = negoamtininr;
        //     //SqlParameter p4 = new SqlParameter("@docprfx", SqlDbType.VarChar);
        //     //p4.Value = DocPrFx;
        //     DataTable dt = objData.getData(query1, p3);
        //     if (dt.Rows.Count > 0)
        //     {
        //         txtCommission.Text = dt.Rows[0]["TotCommission"].ToString();
        //     }
        // }
        // else
        //     txtCommission.Text = "0.00";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Boolean flag = true;
        string intrate1 = "";
        string intrate2 = txtInterestRate2.Text;
        //string days1=txtNoofDays1.Text;
        //string days2=txtNoofDays2.Text;
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
        if (txtExchangeRate.Text != "" && (txtExchangeRate.Text == "0.0000000000" || txtExchangeRate.Text == "0"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Exchange rate cannot be zero.');", true);
            flag = false;
            txtExchangeRate.Focus();
        }
        if (txt_relcur.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Realised Currency Date cannot be blank');", true);
            flag = false;
            txt_relcur.Focus();
        }

        if (chkFirc.Checked == false && txtTTRefNo1.Text == "" && txtTTRefNo2.Text == "" && txtTTRefNo3.Text == "" && txtTTRefNo4.Text == "" && txtTTRefNo5.Text == "" && txtOverseasBank.Text == "" && txtSwiftCode.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Overseas Bank If FIRC or ITT Ref. is Not Present');", true);
            flag = false;
            btnOverseasBankList.Focus();
        }

        if (chkFirc.Checked == false && txtTTRefNo1.Text == "" && txtTTRefNo2.Text == "" && txtTTRefNo3.Text == "" && txtTTRefNo4.Text == "" && txtTTRefNo5.Text == "" && txtOverseasBank.Text != "" && txtSwiftCode.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Swift Code In Overseas Bank Master!');", true);
            flag = false;
            btnOverseasBankList.Focus();
        }

        //if (chkFirc.Checked == false && txtOverseasBank.Text == "" && txtSwiftCode.Text == "")
        //{
        //    if (Cmmp_TTamt_RealAmt())
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('TT Amount should Not less Than Realised Amount!');", true);
        //        flag = false;
        //        txtTTRefNo1.Focus();
        //    }
        //}

        if (flag == true)
        {
            if (hdnLeiFlag.Value == "Y" && lblLEI_CUST_Remark.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify LEI details.');", true);
            }
            else
            {
                string _userName = Session["userName"].ToString().Trim();
                string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                string _mode = Request.QueryString["mode"].Trim();
                DocPrFx = Request.QueryString["DocPrFx"].ToString();
                branchcode = Request.QueryString["BranchCode"].ToString();

                SqlParameter p1 = new SqlParameter("@user", SqlDbType.VarChar);
                p1.Value = _userName;

                SqlParameter p2 = new SqlParameter("@addedtime", SqlDbType.VarChar);
                p2.Value = _uploadingDate;

                SqlParameter mode = new SqlParameter("@mode", SqlDbType.VarChar);
                mode.Value = _mode;

                SqlParameter docno = new SqlParameter("@docno", SqlDbType.VarChar);
                docno.Value = txtDocNo.Text;

                SqlParameter srno = new SqlParameter("@SrNo", SqlDbType.VarChar);
                srno.Value = txtSrNo.Text;

                SqlParameter doctype = new SqlParameter("@doctype", SqlDbType.VarChar);
                doctype.Value = DocPrFx;

                SqlParameter branch = new SqlParameter("@branch", SqlDbType.VarChar);
                branch.Value = branchcode;
                string bill = "";
                //if (txtBillType.Text == "Sight Bill")
                //    bill = "S";
                //else
                //    bill = "U";

                SqlParameter billtype = new SqlParameter("@billtype", SqlDbType.VarChar);
                billtype.Value = bill;

                SqlParameter custacno = new SqlParameter("@custacno", SqlDbType.VarChar);
                custacno.Value = txtCustAcNo.Text;

                SqlParameter opc = new SqlParameter("@opc", SqlDbType.VarChar);
                opc.Value = txtOverseasParty.Text;

                SqlParameter obc = new SqlParameter("@obc", SqlDbType.VarChar);
                obc.Value = txtOverseasBank.Text;

                SqlParameter realdate = new SqlParameter("@realdate", SqlDbType.VarChar);
                realdate.Value = txtDateRealised.Text;

                SqlParameter valuedate = new SqlParameter("@valuedate", SqlDbType.VarChar);
                valuedate.Value = txtValueDate.Text;

                SqlParameter exrate = new SqlParameter("@exrate", SqlDbType.VarChar);
                exrate.Value = txtExchangeRate.Text;

                SqlParameter realamt = new SqlParameter("@realamt", SqlDbType.VarChar);
                realamt.Value = txtAmtRealised.Text;

                SqlParameter realamtinr = new SqlParameter("@realamtinr", SqlDbType.VarChar);
                realamtinr.Value = txtAmtRealisedinINR.Text;

                SqlParameter swift = new SqlParameter("@swift", SqlDbType.VarChar);
                swift.Value = txtSwift.Text;

                SqlParameter bankcert = new SqlParameter("@bankcert", SqlDbType.VarChar);
                bankcert.Value = txtBankCertificate.Text;

                SqlParameter courier = new SqlParameter("@courier", SqlDbType.VarChar);
                courier.Value = txtCourier.Text;

                SqlParameter days = new SqlParameter("@days", SqlDbType.VarChar);
                days.Value = intdays;

                SqlParameter otherbankcharges = new SqlParameter("@otherbankcharges", SqlDbType.VarChar);

                if (txtOtherBankinINR.Text == "")
                {
                    decimal x = 0;
                    string x1 = Convert.ToString(x);
                    otherbankcharges.Value = x1;
                }
                else
                    otherbankcharges.Value = txtOtherBank.Text;

                SqlParameter otherbankchargesinr = new SqlParameter("@otherbankchargesinr", SqlDbType.VarChar);
                otherbankchargesinr.Value = txtOtherBankinINR.Text;

                SqlParameter interest1 = new SqlParameter("@interest1", SqlDbType.VarChar);
                interest1.Value = "";

                SqlParameter days1 = new SqlParameter("@days1", SqlDbType.VarChar);
                days1.Value = "";

                SqlParameter interest2 = new SqlParameter("@interest2", SqlDbType.VarChar);
                interest2.Value = txtInterestRate2.Text;

                SqlParameter days2 = new SqlParameter("@days2", SqlDbType.VarChar);
                days2.Value = txtNoofDays2.Text;

                SqlParameter interestamt = new SqlParameter("@interestamt", SqlDbType.VarChar);
                interestamt.Value = txtInterest.Text;

                SqlParameter interestamtinr = new SqlParameter("@interestamtinr", SqlDbType.VarChar);
                interestamtinr.Value = txtInterestinINR.Text;

                SqlParameter commid = new SqlParameter("@commid", SqlDbType.VarChar);
                commid.Value = txtCommissionID.Text;

                SqlParameter commission = new SqlParameter("@commission", SqlDbType.VarChar);
                commission.Value = txtCommission.Text;

                SqlParameter eefc = new SqlParameter("@eefc", SqlDbType.VarChar);
                eefc.Value = txtEEFCAmt.Text;

                SqlParameter eefcamt = new SqlParameter("@eefcamt", SqlDbType.VarChar);
                eefcamt.Value = txtEEFCinINR.Text;

                SqlParameter netamt = new SqlParameter("@netamt", SqlDbType.VarChar);
                if (txtNetAmt.Text == "")
                {
                    decimal x = 0;
                    string x1 = Convert.ToString(x);
                    netamt.Value = x1;
                }
                else
                    netamt.Value = txtNetAmt.Text;

                SqlParameter remark = new SqlParameter("@remark", SqlDbType.VarChar);
                remark.Value = txtRemark.Text;

                SqlParameter nyrefno = new SqlParameter("@nyrefno", SqlDbType.VarChar);
                nyrefno.Value = "";

                string payind = "";
                if (rdbFull.Checked == true)
                    payind = "F";
                if (rdbPart.Checked == true)
                    payind = "P";
                SqlParameter payind1 = new SqlParameter("@payind", SqlDbType.VarChar);
                payind1.Value = payind;

                SqlParameter colamt = new SqlParameter("@colamt", SqlDbType.VarChar);
                colamt.Value = txtCollectionAmt.Text;

                SqlParameter colamtinr = new SqlParameter("@colamtinr", SqlDbType.VarChar);
                if (txtCollectionAmtinINR.Text == "")
                {
                    decimal x = 0;
                    string x1 = Convert.ToString(x);
                    colamtinr.Value = x1;
                }
                else
                    colamtinr.Value = txtCollectionAmtinINR.Text;

                SqlParameter staxvalue = new SqlParameter("@staxvalue", SqlDbType.VarChar);
                staxvalue.Value = ddlServicetax.Text;

                SqlParameter staxamt = new SqlParameter("@staxamt", SqlDbType.VarChar);
                staxamt.Value = txtServiceTax.Text;

                SqlParameter ttrefno = new SqlParameter("@ttrefno", SqlDbType.VarChar);
                ttrefno.Value = "";

                SqlParameter fxdls = new SqlParameter("@fxdls", SqlDbType.VarChar);
                fxdls.Value = txtFxDlsCommission.Text;

                string loan = "";
                if (chkLoanAdvanced.Checked == true)
                    loan = "Y";
                else
                    loan = "N";
                SqlParameter loan1 = new SqlParameter("@loan", SqlDbType.VarChar);
                loan1.Value = loan;
                string bankline = "";
                if (chkBank.Checked == true)
                    bankline = "Y";
                else
                    bankline = "N";
                SqlParameter bkline = new SqlParameter("@bankline", SqlDbType.VarChar);
                bkline.Value = bankline;

                string remtype = "";
                if (rdbTransType.SelectedValue == "FC")
                    remtype = "FC";
                //if (rdbTransType.SelectedValue == "eefc")
                //    remtype = "EEFC";
                if (rdbTransType.SelectedValue == "INR")
                    remtype = "INR";
                if (rdbTransType.SelectedValue == "CC")
                    remtype = "CC";
                if (rdbTransType.SelectedValue == "PC")
                    remtype = "PC";

                string remtype2 = "";
                if (rdbTransType2.SelectedValue == "PEEFC")
                    remtype2 = "PEEFC";
                if (rdbTransType2.SelectedValue == "FEEFC")
                    remtype2 = "FEEFC";

                SqlParameter remamttype = new SqlParameter("@remtype", SqlDbType.VarChar);
                remamttype.Value = remtype;

                SqlParameter remamttype2 = new SqlParameter("@remtype2", SqlDbType.VarChar);
                remamttype2.Value = remtype2;

                SqlParameter eefccur = new SqlParameter("@eefccur", SqlDbType.VarChar);
                eefccur.Value = txtEEFCCurrency.Text;

                SqlParameter eefcexrate = new SqlParameter("@eefcexrate", SqlDbType.VarChar);
                eefcexrate.Value = txtCrossCurrRate.Text;

                SqlParameter crossamt = new SqlParameter("@crossamt", SqlDbType.VarChar);
                crossamt.Value = txtEEFCAmtTotal.Text;

                SqlParameter balrealamt = new SqlParameter("@balrealamt", SqlDbType.VarChar);
                balrealamt.Value = txtBalAmt.Text;

                SqlParameter balrealamtinr = new SqlParameter("@balrealamtinr", SqlDbType.VarChar);
                balrealamtinr.Value = txtBalAmtinINR.Text;

                SqlParameter fcno = new SqlParameter("@fcno", SqlDbType.VarChar);
                fcno.Value = txtForwardContract.Text;

                SqlParameter actype = new SqlParameter("@actype", SqlDbType.VarChar);
                actype.Value = ddlAccountType.Text;

                SqlParameter CrossrealCur = new SqlParameter("@CrossrealCur", SqlDbType.VarChar);
                CrossrealCur.Value = txt_relcur.Text;

                SqlParameter CrossrealAmt = new SqlParameter("@CrossrealAmt", SqlDbType.VarChar);
                CrossrealAmt.Value = txt_relamount.Text;

                SqlParameter CrossRealCurRate = new SqlParameter("@RelCrossCurRate", SqlDbType.VarChar);
                CrossRealCurRate.Value = txtRelCrossCurRate.Text;

                SqlParameter RealAmtEEFC_FC = new SqlParameter("@RealAmtEEFC_FC", SqlDbType.VarChar);
                RealAmtEEFC_FC.Value = txtPartConAmt.Text;

                SqlParameter RealAmtEEFC_Cur = new SqlParameter("@RealAmtEEFC_Cur", SqlDbType.VarChar);
                RealAmtEEFC_Cur.Value = txtConCrossCur.Text;

                SqlParameter RealAmtEEFC_Rate = new SqlParameter("@RealAmtEEFC_Rate", SqlDbType.VarChar);
                RealAmtEEFC_Rate.Value = txtConCurRate.Text;

                SqlParameter RealAmtEEFC_INR = new SqlParameter("@RealAmtEEFC_INR", SqlDbType.VarChar);
                RealAmtEEFC_INR.Value = txtTotConRate.Text;

                SqlParameter SBcess = new SqlParameter("@SBcess", SqlDbType.VarChar);
                SBcess.Value = txtsbcess.Text;

                SqlParameter SBcessamt = new SqlParameter("@SBcessamt", SqlDbType.VarChar);
                SBcessamt.Value = txtSBcesssamt.Text;

                SqlParameter KKcess = new SqlParameter("@KKcess", SqlDbType.VarChar);
                KKcess.Value = txt_kkcessper.Text;

                SqlParameter KKcessamt = new SqlParameter("@KKcessamt", SqlDbType.VarChar);
                KKcessamt.Value = txt_kkcessamt.Text;

                SqlParameter STaxTotalamt = new SqlParameter("@STaxTotalamt", SqlDbType.VarChar);
                STaxTotalamt.Value = txtsttamt.Text;

                SqlParameter ProfitLieoAmt = new SqlParameter("@ProfitLieoAmt", SqlDbType.VarChar);
                ProfitLieoAmt.Value = txtprofitamt.Text;

                SqlParameter ProfitLieoId = new SqlParameter("@ProfitLieoId", SqlDbType.VarChar);
                ProfitLieoId.Value = txtprofitper.Text;

                SqlParameter TTREFNO1 = new SqlParameter("@TTREFNO1", SqlDbType.VarChar);
                TTREFNO1.Value = txtTTRefNo1.Text;

                SqlParameter TTAmt1 = new SqlParameter("@TTAmt1", SqlDbType.VarChar);
                TTAmt1.Value = txtTTAmount1.Text;

                SqlParameter TTREFNO2 = new SqlParameter("@TTREFNO2", SqlDbType.VarChar);
                TTREFNO2.Value = txtTTRefNo2.Text;

                SqlParameter TTAmt2 = new SqlParameter("@TTAmt2", SqlDbType.VarChar);
                TTAmt2.Value = txtTTAmount2.Text;

                SqlParameter TTREFNO3 = new SqlParameter("@TTREFNO3", SqlDbType.VarChar);
                TTREFNO3.Value = txtTTRefNo3.Text;

                SqlParameter TTAmt3 = new SqlParameter("@TTAmt3", SqlDbType.VarChar);
                TTAmt3.Value = txtTTAmount3.Text;

                SqlParameter TTREFNO4 = new SqlParameter("@TTREFNO4", SqlDbType.VarChar);
                TTREFNO4.Value = txtTTRefNo4.Text;

                SqlParameter TTAmt4 = new SqlParameter("@TTAmt4", SqlDbType.VarChar);
                TTAmt4.Value = txtTTAmount4.Text;

                SqlParameter TTREFNO5 = new SqlParameter("@TTREFNO5", SqlDbType.VarChar);
                TTREFNO5.Value = txtTTRefNo5.Text;

                SqlParameter TTAmt5 = new SqlParameter("@TTAmt5", SqlDbType.VarChar);
                TTAmt5.Value = txtTTAmount5.Text;

                SqlParameter FIRC_Status = new SqlParameter("@FIRC_Status", SqlDbType.VarChar);
                if (chkFirc.Checked == true)
                { FIRC_Status.Value = "Y"; }
                if (chkFirc.Checked == false)
                { FIRC_Status.Value = "N"; }

                SqlParameter FIRC_NO = new SqlParameter("@FIRC_NO", SqlDbType.VarChar);
                FIRC_NO.Value = txtFircNo.Text;

                SqlParameter FIRC_AD_CODE = new SqlParameter("@FIRC_AD_CODE", SqlDbType.VarChar);
                FIRC_AD_CODE.Value = txtFircAdCode.Text;

                SqlParameter ProfitLieo_Status = new SqlParameter("@ProfitLieo_Status", SqlDbType.VarChar);
                if (chkProfitLio.Checked == true)
                { ProfitLieo_Status.Value = "Y"; }
                if (chkProfitLio.Checked == false)
                { ProfitLieo_Status.Value = "N"; }

                //Fxdls
                SqlParameter SBFxDls = new SqlParameter("@SBcessFxDls", txtsbfx.Text);
                SqlParameter kkcessfxdls = new SqlParameter("@KKcessFxDls", txt_kkcessonfx.Text);
                SqlParameter totalSBFxDls = new SqlParameter("@TotalSBcessFxDls", txttotcessfx.Text);
                SqlParameter FBKCharge = new SqlParameter("@FBKCharge", txt_fbkcharges.Text);
                SqlParameter FBKChargeINR = new SqlParameter("@FBKChargeINR", txt_fbkchargesinRS.Text);

                SqlParameter pcfcamt = new SqlParameter("@pcfcamt", SqlDbType.VarChar);
                pcfcamt.Value = txtPcfcAmt.Text;

                SqlParameter overdueamt = new SqlParameter("@overdueamt", SqlDbType.VarChar);
                overdueamt.Value = txtOverDue.Text;


                //SqlParameter CrossRealCurRate = new SqlParameter("@RelCrossCurRate", SqlDbType.VarChar);
                //CrossRealCurRate.Value = txtRelCrossCurRate.Text;


                decimal b = 0;
                decimal r = Convert.ToDecimal(txtAmtRealised.Text);
                decimal bi = Convert.ToDecimal(txtBillAmt.Text);

                b = bi - r;

                string bl = Convert.ToString(b);

                SqlParameter bal = new SqlParameter("@balamtforpaytype", SqlDbType.VarChar);
                bal.Value = bl;

                string query = "TF_UpdateExportRealisationDetails";
                string _result = "";

                TF_DATA objSave = new TF_DATA();

//  mode.Value = mode.Value; branch.Value = branch.Value; docno.Value = docno.Value; doctype.Value = doctype.Value; days.Value = days.Value; billtype.Value = billtype.Value; 
//custacno.Value = custacno.Value; opc.Value = opc.Value; obc.Value = obc.Value; realdate.Value = realdate.Value; valuedate.Value = valuedate.Value; realamt.Value = realamt.Value; 
//realamtinr.Value = realamtinr.Value; exrate.Value = exrate.Value; swift.Value = swift.Value; bankcert.Value = bankcert.Value; courier.Value = courier.Value; 
// otherbankcharges.Value = otherbankcharges.Value; otherbankchargesinr.Value = otherbankchargesinr.Value; interest1.Value = interest1.Value;days1.Value =days1.Value; 
// interest2.Value =interest2.Value; days2.Value =days2.Value; interestamt.Value =interestamt.Value; interestamtinr.Value =interestamtinr.Value; commid.Value =commid.Value; 
// commission.Value =commission.Value; eefc.Value =eefc.Value; eefcamt.Value =eefcamt.Value; netamt.Value =netamt.Value; remark.Value =remark.Value; nyrefno.Value =nyrefno.Value; 
// payind1.Value =payind1.Value; srno.Value =srno.Value; colamt.Value =colamt.Value; colamtinr.Value =colamtinr.Value; staxvalue.Value =staxvalue.Value;staxamt.Value =staxamt.Value;
// ttrefno.Value =ttrefno.Value; fxdls.Value =fxdls.Value; loan1.Value =loan1.Value; bkline.Value =bkline.Value; remamttype.Value =remamttype.Value; remamttype2.Value=remamttype2.Value;
//eefccur.Value =eefccur.Value; eefcexrate.Value =eefcexrate.Value; balrealamt.Value =balrealamt.Value; balrealamtinr.Value =balrealamtinr.Value; crossamt.Value =crossamt.Value; 
//fcno.Value =fcno.Value; actype.Value =actype.Value; bal.Value =bal.Value; p1.Value =p1.Value; p2.Value =p2.Value; CrossrealCur.Value =CrossrealCur.Value; CrossrealAmt.Value=CrossrealAmt.Value;
//CrossRealCurRate.Value =CrossRealCurRate.Value; RealAmtEEFC_FC.Value =RealAmtEEFC_FC.Value; RealAmtEEFC_Cur.Value =RealAmtEEFC_Cur.Value; RealAmtEEFC_Rate.Value=RealAmtEEFC_Rate.Value; 
//RealAmtEEFC_INR.Value =RealAmtEEFC_INR.Value; SBcess.Value =SBcess.Value; SBcessamt.Value =SBcessamt.Value; KKcess.Value =KKcess.Value; KKcessamt.Value =KKcessamt.Value; 
//STaxTotalamt.Value =STaxTotalamt.Value; ProfitLieoAmt.Value =ProfitLieoAmt.Value; ProfitLieoId.Value =ProfitLieoId.Value;TTREFNO1.Value =TTREFNO1.Value; TTAmt1.Value =TTAmt1.Value;
//TTREFNO2.Value =TTREFNO2.Value; TTAmt2.Value =TTAmt2.Value; TTREFNO3.Value =TTREFNO3.Value; TTAmt3.Value =TTAmt3.Value; TTREFNO4.Value =TTREFNO4.Value; TTAmt4.Value =TTAmt4.Value;
//TTREFNO5.Value =TTREFNO5.Value; TTAmt5.Value =TTAmt5.Value; FIRC_Status.Value =FIRC_Status.Value; FIRC_NO.Value =FIRC_NO.Value; FIRC_AD_CODE.Value =FIRC_AD_CODE.Value; 
//ProfitLieo_Status.Value =ProfitLieo_Status.Value;SBFxDls.Value =SBFxDls.Value; kkcessfxdls.Value =kkcessfxdls.Value; totalSBFxDls.Value =totalSBFxDls.Value; FBKCharge.Value =FBKCharge.Value; 
//   FBKChargeINR.Value =FBKChargeINR.Value; pcfcamt.Value =pcfcamt.Value; overdueamt.Value =overdueamt.Value;

                _result = objSave.SaveDeleteData(query, mode, branch, docno, doctype, days, billtype, custacno, opc, obc, realdate, valuedate, realamt, realamtinr, exrate, swift, bankcert, courier, otherbankcharges, otherbankchargesinr, interest1,
                                                 days1, interest2, days2, interestamt, interestamtinr, commid, commission, eefc, eefcamt, netamt, remark, nyrefno, payind1, srno, colamt, colamtinr, staxvalue,
                                                 staxamt, ttrefno, fxdls, loan1, bkline, remamttype, remamttype2, eefccur, eefcexrate, balrealamt, balrealamtinr, crossamt, fcno, actype, bal, p1, p2, CrossrealCur, CrossrealAmt,
                                                 CrossRealCurRate, RealAmtEEFC_FC, RealAmtEEFC_Cur, RealAmtEEFC_Rate, RealAmtEEFC_INR, SBcess, SBcessamt, KKcess, KKcessamt, STaxTotalamt, ProfitLieoAmt, ProfitLieoId,
                                                 TTREFNO1, TTAmt1, TTREFNO2, TTAmt2, TTREFNO3, TTAmt3, TTREFNO4, TTAmt4, TTREFNO5, TTAmt5, FIRC_Status, FIRC_NO, FIRC_AD_CODE, ProfitLieo_Status,
                                                 SBFxDls, kkcessfxdls, totalSBFxDls, FBKCharge, FBKChargeINR, pcfcamt, overdueamt);

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
                //=========================END====================================//

                string _script = "";
                //TF_DATA objSavePCLiquiDetails = new TF_DATA();
                //string _query = "TF_EXP_UpdatePCLiquidationDetails";
                //SqlParameter pPCbCode = new SqlParameter("@bCode", SqlDbType.VarChar);
                //pPCbCode.Value = hdnBranchCode.Value;
                //SqlParameter pPCmodType = new SqlParameter("@modType", SqlDbType.VarChar);
                //pPCmodType.Value = "EXPR";
                //SqlParameter pPCLiquiDt = new SqlParameter("@LiquiDate", SqlDbType.VarChar);
                //pPCLiquiDt.Value = txtDateRealised.Text;
                //SqlParameter pPCcustAc = new SqlParameter("@custAc", SqlDbType.VarChar);
                //pPCcustAc.Value = txtCustAcNo.Text;
                //SqlParameter pPCcurr = new SqlParameter("@curr", SqlDbType.VarChar);
                //pPCcurr.Value = txtCurrency.Text;
                //SqlParameter pPCdocNo = new SqlParameter("@docNo", SqlDbType.VarChar);
                //pPCdocNo.Value = txtDocNo.Text;
                //SqlParameter pPCdocType = new SqlParameter("@docType", SqlDbType.VarChar);
                //pPCdocType.Value = txtDocPrFx.Value;
                //SqlParameter pPCexchRt = new SqlParameter("@exchRt", SqlDbType.VarChar);
                //pPCexchRt.Value = txtExchangeRate.Text;
                //SqlParameter pPCuser = new SqlParameter("@user", SqlDbType.VarChar);
                //pPCuser.Value = _userName;
                //SqlParameter pPCuploadingDt = new SqlParameter("@uploadDate", SqlDbType.VarChar);
                //pPCuploadingDt.Value = _uploadingDate;

                //SqlParameter pPCacNo = new SqlParameter("@acNo", SqlDbType.VarChar);
                //SqlParameter pPCsubAcNo = new SqlParameter("@subAcNo", SqlDbType.VarChar);
                //SqlParameter pPCliquiAmt = new SqlParameter("@LiquiAmt", SqlDbType.VarChar);
                //SqlParameter pPCsrNo = new SqlParameter("@srNo", SqlDbType.VarChar);
                //SqlParameter exprsrno = new SqlParameter("@exprsrno", SqlDbType.VarChar);
                //exprsrno.Value = txtSrNo.Text;
                //string s = "";
                //if (txtPCAcNo1.Text != "")
                //{
                //    pPCacNo.Value = txtPCAcNo1.Text;
                //    pPCsubAcNo.Value = txtPCsubAcNo1.Text;
                //    pPCliquiAmt.Value = txtPCAmount1.Text;
                //    if (hdnPCsrNo1.Value != "")
                //        pPCsrNo.Value = hdnPCsrNo1.Value;
                //    else
                //        pPCsrNo.Value = getPCsrNo(hdnBranchName.Value, txtPCAcNo1.Text, txtPCsubAcNo1.Text);

                //    s = objSavePCLiquiDetails.SaveDeleteData(_query, pPCbCode, pPCmodType, pPCLiquiDt, pPCcustAc, pPCcurr,exprsrno,
                //                    pPCdocNo, pPCdocType, pPCexchRt, pPCacNo, pPCsubAcNo, pPCliquiAmt, pPCsrNo, pPCuser, pPCuploadingDt);
                //}
                //if (txtPCAcNo2.Text != "")
                //{
                //    pPCacNo.Value = txtPCAcNo2.Text;
                //    pPCsubAcNo.Value = txtPCsubAcNo2.Text;
                //    pPCliquiAmt.Value = txtPCAmount2.Text;
                //    if (hdnPCsrNo2.Value != "")
                //        pPCsrNo.Value = hdnPCsrNo2.Value;
                //    else
                //        pPCsrNo.Value = getPCsrNo(hdnBranchName.Value, txtPCAcNo2.Text, txtPCsubAcNo2.Text);

                //    s = objSavePCLiquiDetails.SaveDeleteData(_query, pPCbCode, pPCmodType, pPCLiquiDt, pPCcustAc, pPCcurr, exprsrno,
                //                     pPCdocNo, pPCdocType, pPCexchRt, pPCacNo, pPCsubAcNo, pPCliquiAmt, pPCsrNo, pPCuser, pPCuploadingDt);
                //}
                //if (txtPCAcNo3.Text != "")
                //{
                //    pPCacNo.Value = txtPCAcNo3.Text;
                //    pPCsubAcNo.Value = txtPCsubAcNo3.Text;
                //    pPCliquiAmt.Value = txtPCAmount3.Text;
                //    if (hdnPCsrNo3.Value != "")
                //        pPCsrNo.Value = hdnPCsrNo3.Value;
                //    else
                //        pPCsrNo.Value = getPCsrNo(hdnBranchName.Value, txtPCAcNo3.Text, txtPCsubAcNo3.Text);

                //    s = objSavePCLiquiDetails.SaveDeleteData(_query, pPCbCode, pPCmodType, pPCLiquiDt, pPCcustAc, pPCcurr, exprsrno,
                //                    pPCdocNo, pPCdocType, pPCexchRt, pPCacNo, pPCsubAcNo, pPCliquiAmt, pPCsrNo, pPCuser, pPCuploadingDt);
                //}
                //if (txtPCAcNo4.Text != "")
                //{
                //    pPCacNo.Value = txtPCAcNo4.Text;
                //    pPCsubAcNo.Value = txtPCsubAcNo4.Text;
                //    pPCliquiAmt.Value = txtPCAmount4.Text;
                //    if (hdnPCsrNo4.Value != "")
                //        pPCsrNo.Value = hdnPCsrNo4.Value;
                //    else
                //        pPCsrNo.Value = getPCsrNo(hdnBranchName.Value, txtPCAcNo4.Text, txtPCsubAcNo4.Text);

                //    s = objSavePCLiquiDetails.SaveDeleteData(_query, pPCbCode, pPCmodType, pPCLiquiDt, pPCcustAc, pPCcurr, exprsrno,
                //                    pPCdocNo, pPCdocType, pPCexchRt, pPCacNo, pPCsubAcNo, pPCliquiAmt, pPCsrNo, pPCuser, pPCuploadingDt);
                //}
                //if (txtPCAcNo5.Text != "")
                //{
                //    pPCacNo.Value = txtPCAcNo5.Text;
                //    pPCsubAcNo.Value = txtPCsubAcNo5.Text;
                //    pPCliquiAmt.Value = txtPCAmount5.Text;
                //    if (hdnPCsrNo5.Value != "")
                //        pPCsrNo.Value = hdnPCsrNo5.Value;
                //    else
                //        pPCsrNo.Value = getPCsrNo(hdnBranchName.Value, txtPCAcNo5.Text, txtPCsubAcNo5.Text);

                //    s = objSavePCLiquiDetails.SaveDeleteData(_query, pPCbCode, pPCmodType, pPCLiquiDt, pPCcustAc, pPCcurr, exprsrno,
                //                     pPCdocNo, pPCdocType, pPCexchRt, pPCacNo, pPCsubAcNo, pPCliquiAmt, pPCsrNo, pPCuser, pPCuploadingDt);
                //}
                //if (txtPCAcNo6.Text != "")
                //{
                //    pPCacNo.Value = txtPCAcNo6.Text;
                //    pPCsubAcNo.Value = txtPCsubAcNo6.Text;
                //    pPCliquiAmt.Value = txtPCAmount6.Text;
                //    if (hdnPCsrNo6.Value != "")
                //        pPCsrNo.Value = hdnPCsrNo6.Value;
                //    else
                //        pPCsrNo.Value = getPCsrNo(hdnBranchName.Value, txtPCAcNo6.Text, txtPCsubAcNo6.Text);

                //    s = objSavePCLiquiDetails.SaveDeleteData(_query, pPCbCode, pPCmodType, pPCLiquiDt, pPCcustAc, pPCcurr, exprsrno,
                //                    pPCdocNo, pPCdocType, pPCexchRt, pPCacNo, pPCsubAcNo, pPCliquiAmt, pPCsrNo, pPCuser, pPCuploadingDt);
                //}
                ///////////////////////////////////////////////BEFORE////////////////////////////////////////////////////////////////////////
                //if (_result == "added")
                //{
                //    _script = "window.location='EXP_ViewRealisationEntry.aspx?result=" + _result + "'";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);

                //    //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Added');", true);
                //    //clearAll();
                //}
                //else
                //{
                //    if (_result == "updated")
                //    {
                //        _script = "window.location='EXP_ViewRealisationEntry.aspx?result=" + _result + "'";
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                //        //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Added');", true);
                //    }
                //    else
                //        labelMessage.Text = _result;
                //}

                ///////////////////////////////////////////////////////////////////Modify by Nilesh//////////////////////////////////////////////////////////////////////////////////////

                var argument = ((Button)sender).CommandArgument;

                if (_result.Substring(0, 5) == "added")
                // if (_result == "added")
                {
                    if (argument.ToString() == "print")
                    {
                        _script = "window.location='EXP_ViewRealisationEntry.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                    else
                    {
                        _script = "window.location='EXP_ViewRealisationEntry.aspx?result=" + _result + "&saveType=save&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
                else
                {
                    if (_result == "updated")
                    {
                        if (argument.ToString() == "print")
                        {
                            _script = "window.location='EXP_ViewRealisationEntry.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                        else
                        {
                            _script = "window.location='EXP_ViewRealisationEntry.aspx?result=" + _result + "&saveType=save&frm=" + txtDateRealised.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocNo.Text.Trim() + "&rptTodocno=" + txtDocNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill Document Realisation'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }
                    else
                        labelMessage.Text = _result;
                }
                ////////////////////////////////////////////////////////////////////////////////END/////////////////////////////////////////////////////////////////////////////

            }
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
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calrealisedAmtinINR();alert('You have selected Full Payment')", true);
        //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('You have selected Full Payment')", true);
        rdbFull.Focus();
    }

    protected void rdbPart_CheckedChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calrealisedAmtinINR();alert('You have selected Part Payment')", true);
        //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('You have selected Part Payment')", true);
        rdbPart.Focus();
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
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "calTax();", true);
        }
        else
        {
            ddlServicetax.Items.Clear();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "calTax();", true);
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
            txtTotTTAmt5.Text = "";
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

                if (hdncustlei.Value == "" || hdncustleiexpiryRe.Value == "")  //// remove as per user suggesions /   || hdnoverseaslei.Value == "" || hdnoverseasleiexpiryRe.Value == ""
                {
                    btnSave.Visible = false;
                    btnSavePrint.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnSavePrint.Visible = true;
                }

                String LEIMSG = @"Customer LEI : " + CustLEINo + "\\n" + "Customer LEI Expiry : " + CustLEIExpiry + "\\n" + "Applicant LEI : " + ApplicantLEINo + "\\n" + "Applicant LEI Expiry : " + ApplicantLEIExpiry;
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
            lblLEI_CUST_Remark.Visible = true;
            SpanLei1.Visible = true;
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
            lblLEIExpiry_CUST_Remark.Visible = true;
            SpanLei2.Visible = true;
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
            lblLEI_Overseas_Remark.Visible = true;
            SpanLei3.Visible = true;
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
            lblLEIExpiry_Overseas_Remark.Visible = true;
            SpanLei4.Visible = true;
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
                if (txtExch !="" && txtExch != "1.0000000000")
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
                            Check_CUST_LEINODetails();
                            Check_CUST_LEINO_ExpirydateDetails();
                            Check_Overseas_LEINODetails();
                            Check_Overseas_LEINO_ExpirydateDetails();

                            SpanLei1.Visible = true; SpanLei2.Visible = true; SpanLei3.Visible = true; SpanLei4.Visible = true;

                            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                            dateInfo.ShortDatePattern = "dd/MM/yyyy";
                            DateTime LEIEffectDate = Convert.ToDateTime(lblLEIEffectDate.Text.Trim(), dateInfo);
                            DateTime LEIValueDate = Convert.ToDateTime(txtValueDate.Text.Trim(), dateInfo);

                            if (LEIEffectDate <= LEIValueDate)
                            {
                                if (hdncustlei.Value == "" || hdncustleiexpiryRe.Value == "")  //// remove as per user suggesions /  || hdnoverseaslei.Value == "" || hdnoverseasleiexpiryRe.Value == ""
                                {
                                    btnSave.Visible = false;
                                    btnSavePrint.Visible = false;
                                    LEIAmtCheck.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit. ";
                                    LEIverify.Text = "Please Verify LEI.";
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This transaction cannot proceed because the LEI Details has not been verified.')", true);
                                }
                                else
                                {
                                    btnSave.Visible = true;
                                    btnSavePrint.Visible = true;
                                }
                            }

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

                Check_CUST_LEINODetails();
                Check_CUST_LEINO_ExpirydateDetails();
                Check_Overseas_LEINODetails();
                Check_Overseas_LEINO_ExpirydateDetails();

                SpanLei1.Visible = true; SpanLei2.Visible = true; SpanLei3.Visible = true; SpanLei4.Visible = true; SpanLei5.Visible = true;
                ReccuringLEI.Visible = true;
                ReccuringLEI.Text = "This is Recurring LEI Customer.";
                ReccuringLEI.ForeColor = System.Drawing.Color.Red;

                
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

                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                dateInfo.ShortDatePattern = "dd/MM/yyyy";
                DateTime LEIEffectDate = Convert.ToDateTime(lblLEIEffectDate.Text.Trim(), dateInfo);
                DateTime LEIValueDate = Convert.ToDateTime(txtValueDate.Text.Trim(), dateInfo);

                if (LEIEffectDate <= LEIValueDate)
                {
                    if (hdncustlei.Value == "" || hdncustleiexpiryRe.Value == "")  //// remove as per user suggesions / || hdnoverseaslei.Value == "" || hdnoverseasleiexpiryRe.Value == "" 
                    {
                        btnLEI.Visible = true;
                        btnSave.Visible = false;
                        btnSavePrint.Visible = false;
                        if (hdnLeiFlag.Value == "N")
                        {
                            LEIverify.Text = "";
                            LEIverify.Text = "Please Verify LEI.";
                            LEIverify.ForeColor = System.Drawing.Color.Red;
                        }
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('This transaction cannot proceed because the LEI Details has not been verified.')", true);
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnSavePrint.Visible = true;
                    }
                }
            }
            else
            {
                ReccuringLEI.Text = "";
                ReccuringLEI.Visible = false;
                SpanLei5.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
}