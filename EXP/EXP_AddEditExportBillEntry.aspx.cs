using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Globalization;

public partial class EXP_EXP_AddEditExportBillEntry : System.Web.UI.Page
{

    #region Initialisation
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
                TabContainerMain.ActiveTab = tbDocumentDetails;

                string queryLastShippingBillDate = "TF_GetMaxDate_EDPMS_DUMP";
                TF_DATA ShippingBillDate1 = new TF_DATA();
                DataTable dtShipingBillNo1;
                dtShipingBillNo1 = ShippingBillDate1.getData(queryLastShippingBillDate);

                if (dtShipingBillNo1.Rows.Count > 0)
                {
                    lbl_dumpdate.Text = "Last Shipping Date in EDPMS Dump " + dtShipingBillNo1.Rows[0]["LastDate"].ToString() + ".";
                }
                else
                {
                    lbl_dumpdate.Text = "No Data in EDPMS Dump";

                }

                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EXP_ViewExportBillEntry.aspx", true);
                }
                else
                {
                    hdnRole.Value = Session["UserRole"].ToString();

                    fillCurrency(ddlCurrency, ddlTTCurrency1, ddlTTCurrency2, ddlTTCurrency3, ddlTTCurrency4, ddlTTCurrency5, ddlTTCurrency6, ddlTTCurrency7, ddlTTCurrency8, ddlTTCurrency9,
   ddlTTCurrency10, ddlTTCurrency11, ddlTTCurrency12, ddlTTCurrency13, ddlTTCurrency14, ddlTTCurrency15, ddlTTRealisedCurr1, ddlTTRealisedCurr2, ddlTTRealisedCurr3,
   ddlTTRealisedCurr4, ddlTTRealisedCurr5, ddlTTRealisedCurr6, ddlTTRealisedCurr7, ddlTTRealisedCurr8, ddlTTRealisedCurr9, ddlTTRealisedCurr10, ddlTTRealisedCurr11,
   ddlTTRealisedCurr12, ddlTTRealisedCurr13, ddlTTRealisedCurr14, ddlTTRealisedCurr15, ddlFIRCRealisedCurr1_OB, ddlFIRCRealisedCurr2_OB, ddlFIRCRealisedCurr3_OB,
   ddlFIRCRealisedCurr4_OB, ddlFIRCRealisedCurr5_OB, ddlFIRCRealisedCurr6_OB, ddlFIRCRealisedCurr7_OB, ddlFIRCRealisedCurr8_OB, ddlFIRCRealisedCurr9_OB, ddlFIRCRealisedCurr10_OB,
   ddlFIRCRealisedCurr11_OB, ddlFIRCRealisedCurr12_OB, ddlFIRCRealisedCurr13_OB, ddlFIRCRealisedCurr14_OB, ddlFIRCRealisedCurr15_OB, ddlFIRCCurrency1_OB,
   ddlFIRCCurrency2_OB, ddlFIRCCurrency3_OB, ddlFIRCCurrency4_OB, ddlFIRCCurrency5_OB, ddlFIRCCurrency6_OB, ddlFIRCCurrency7_OB, ddlFIRCCurrency8_OB,
   ddlFIRCCurrency9_OB, ddlFIRCCurrency10_OB, ddlFIRCCurrency11_OB, ddlFIRCCurrency12_OB, ddlFIRCCurrency13_OB, ddlFIRCCurrency14_OB, ddlFIRCCurrency15_OB, ddlOtherCurrency,
   ddlCurrencyGRPP, ddlFreightCurr, ddlInsCurr, ddlDiscCurr, ddlCommCurr, ddlOthDedCurr, ddlPackChgCurr);

                    fillTaxRates();
                    fillPortCodes();

                    String SwichDOCType = "";
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtDocumentNo.Text = Request.QueryString["DocNo"].Trim();
                        string SwichDOC = Request.QueryString["DocNo"].Substring(0, 2);
                        if (SwichDOC != "EB")
                        {
                            SwichDOC = Request.QueryString["DocNo"].Substring(0, 3);
                        }
                        SwichDOCType = SwichDOC;
                        //txtDocType.Text = Request.QueryString["DocPrFx"].Trim();// Comment bt Anand 29-12-2023
                        txtDocType.Text = SwichDOCType;// Added by Anand 29-12-2023
                        fillDetails(Request.QueryString["DocNo"].Trim());
                        if (Request.QueryString["ForeignOrLocal"].Trim() == "F")
                        {
                            hdnForeignLocal.Value = "F";
                            Leicount = 0;
                            Check_LEI_ThresholdLimit();


                        }
                        else
                        {
                            hdnForeignLocal.Value = "L";
                            btnLEI.Visible = false;

                        }
                        rbtnSightBill.Enabled = false;
                        rbtnUsanceBill.Enabled = false;
                        txtDateRcvd.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        lblCopyFrom.Visible = true;
                        txtCopyFromDocNo.Visible = true;
                        rdbOurADCode.Checked = true;
                        btnCopy.Visible = true;
                        btnDocNoListtoCopy.Visible = true;
                        txtDateRcvd.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        txtDateNegotiated.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                        if (Request.QueryString["ForeignOrLocal"].Trim() == "F")
                        {
                            txtOutOfDays.Text = "360";
                            hdnForeignLocal.Value = "F";
                            ddlRemEUC.SelectedValue = "Foreign"; // Added by Anand 01-07-2023
                            //btnLEI.Visible = true;
                            //Check_LEI_ThresholdLimit();
                        }
                        else
                        {
                            txtOutOfDays.Text = "365";
                            hdnForeignLocal.Value = "L";
                            ddlRemEUC.SelectedValue = "Local"; // Added by Anand 01-07-2023
                            btnLEI.Visible = false;
                        }
                        if (txtDateRcvd.Text != "")
                        {
                            TF_DATA objData = new TF_DATA();
                            SqlParameter p1 = new SqlParameter("@dateRcvd", SqlDbType.VarChar);
                            p1.Value = txtDateRcvd.Text;

                            string _query = "getENCdatediff";
                            DataTable dt = objData.getData(_query, p1);
                            if (dt.Rows.Count > 0)
                            {
                                txtENCdate.Text = dt.Rows[0]["ENCdate"].ToString();
                            }
                        }

                        txtDocType.Text = Request.QueryString["DocPrFx"].Trim();
                        string _prFx = Request.QueryString["DocPrFx"].Trim();
                        string _srNo = Request.QueryString["DocSrNo"].Trim();
                        string _branchCode = Request.QueryString["BranchCode"].Trim();

                        string _year = Request.QueryString["DocYear"].Trim();

                        SwichDOCType = _prFx;

                        txtDocumentNo.Text = _prFx + "/" + _branchCode + "/" + _year + _srNo;

                        DDL_Dispatch();
                        DDL_Dispatch2();

                        if (_prFx == "EB")
                        {

                            ddlDispachInd.Text = "Dispatched directly by exporter";
                            DDL_Dispatch2();
                            ddlDispBydefault.Text = "Non-Dispatch";
                            ddlMercTrade.SelectedValue = "No";
                        }
                        if (_prFx == "BCA" || _prFx == "BCU")
                        {
                            txtDirection.Text = "6";   //added by bhupen
                            txtCovrInstr.Text = "L";   //added by bhupen
                        }
                    }
                    rbtnSightBill.Enabled = false;
                    rbtnUsanceBill.Enabled = false;
                    //switch (Request.QueryString["DocPrFx"].Trim())
                    switch (SwichDOCType)
                    {

                        case "BLA":
                            lblDocumentType.Text = "Bills Bought with L/C at Sight";
                            rbtnUsanceBill.Checked = false;
                            rbtnSightBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtNoOfDays.Text = "25";
                            txtNoOfDays_TextChanged(null, null);
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "N" + txtDocumentNo.Text.Substring(8, 6);
                            rbtnSight.Checked = true;
                            txtOtherTenorRemarks.Text = "Days Sight";
                            TTFIRCVisible.Visible = false;
                            break;
                        case "BLU":
                            lblDocumentType.Text = "Bills Bought with L/C Usance";
                            rbtnSightBill.Checked = false;
                            rbtnUsanceBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "N" + txtDocumentNo.Text.Substring(8, 6);

                            TTFIRCVisible.Visible = false;
                            break;
                        case "BBA":
                            lblDocumentType.Text = "Bills Bought without L/C at Sight";
                            rbtnUsanceBill.Checked = false;
                            rbtnSightBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtNoOfDays.Text = "25";
                            txtNoOfDays_TextChanged(null, null);
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "P" + txtDocumentNo.Text.Substring(8, 6);
                            rbtnSight.Checked = true;
                            txtOtherTenorRemarks.Text = "Days Sight";
                            TTFIRCVisible.Visible = false;
                            break;
                        case "BBU":
                            lblDocumentType.Text = "Bills Bought without L/C Usance";
                            rbtnSightBill.Checked = false;
                            rbtnUsanceBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "D" + txtDocumentNo.Text.Substring(8, 6);

                            TTFIRCVisible.Visible = false;
                            break;
                        case "BCA":
                            lblDocumentType.Text = "Bills For Collection at Sight";
                            rbtnUsanceBill.Checked = false;
                            rbtnSightBill.Checked = true;
                            btnTTRefNoList.Enabled = true;
                            txtNoOfDays.Text = "25";
                            txtNoOfDays_TextChanged(null, null);
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "C" + txtDocumentNo.Text.Substring(8, 6);
                            rbtnSight.Checked = true;
                            txtOtherTenorRemarks.Text = "Days Sight";

                            TTFIRCVisible.Visible = false;
                            break;
                        case "BCU":
                            lblDocumentType.Text = "Bills For Collection Usance";
                            rbtnSightBill.Checked = false;
                            rbtnUsanceBill.Checked = true;
                            btnTTRefNoList.Enabled = true;
                            // txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "C" + txtDocumentNo.Text.Substring(8, 6);


                            TTFIRCVisible.Visible = false;
                            break;
                        case "EB":
                            lblDocumentType.Text = "Advance";
                            btnTTRefNoList.Enabled = true;
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "M" + txtDocumentNo.Text.Substring(7, 6);
                            rbtnSightBill.Enabled = true;
                            rbtnUsanceBill.Enabled = true;
                            /////// added for advance suggested by suryaprakash\\\\\
                            TTFIRCVisible.Visible = true;

                            break;
                        case "IBD":
                            lblDocumentType.Text = "Vendor Bill Discounting";
                            ddlCurrency.SelectedValue = "INR";
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtExchRtEBR.Text = "1";
                            chkRBI.Visible = true;
                            rbtnSightBill.Enabled = true;
                            rbtnUsanceBill.Enabled = true;
                            TTFIRCVisible.Visible = false;
                            break;
                    }

                    hdnbranch.Value = Request.QueryString["BranchCode"].Trim();
                    hdnBranchCode.Value = Request.QueryString["BranchCode"].Trim();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                }
                //txtCoveringFrom.Attributes.Add("onblur", "return checkValidPortCode();");

                ddlFormType.Attributes.Add("onkeyup", "return generateSDFcustomsNo();");
                ddlFormType.Attributes.Add("onclick", "return generateSDFcustomsNo();");

                ////txtAmountGRPP.Attributes.Add("onblur", "return calculateAmountinINR_GR();");
                ////txtExchRateGR.Attributes.Add("onblur", "return calculateAmountinINR_GR();");
                ////txtAmountinINRGR.Attributes.Add("onblur", "return calculateAmountinINR_GR();");

                chkRBI.Attributes.Add("onclick", "changeTextColor();");
                btnAddGRPPCustoms.Attributes.Add("onclick", "return chkShippingBillNo();");
                btnCustomerList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
                btnOverseasPartyList.Attributes.Add("onclick", "return OpenOverseasPartyList('mouseClick');");
                btnConsigneePartyList.Attributes.Add("onclick", "return OpenConsigneePartyList('mouseClick');"); //Nilesh 04-08-2023
                btnOverseasBankList.Attributes.Add("onclick", "return OpenOverseasBankList('mouseClick');");

                btnPayingBankList.Attributes.Add("onclick", "return OpenPayingBankList('mouseClick');");


                //GBase Reimb Bank Master
                btnReimbBank.Attributes.Add("onclick", "return OpenReimbBankList('mouseClick');");
                //btnCoveringTo.Attributes.Add("onclick", "return OpenPortCodeList('mouseClick');");
                btnDocNoListtoCopy.Attributes.Add("onclick", "return OpenCopyFromDocNoList('mouseClick');");
                btnCommodityList.Attributes.Add("onclick", "return OpenCommodityList();");
                //GBASE
                btnGBaseCommodityList.Attributes.Add("onclick", "return OpenGBaseCommodityList();");

                btnCountryList.Attributes.Add("onclick", "return OpenCountryList('1');");
                btnSave.Attributes.Add("onclick", "return ValidateSave();");
                btnSaveDraft.Attributes.Add("onclick", "return ValidateSaveLEI();");
                //btnSavePrint.Attributes.Add("onclick", "return ValidateSave();");

                rbtnSightBill.Attributes.Add("onclick", "return Calculate();");
                rbtnUsanceBill.Attributes.Add("onclick", "return Calculate();");

                //Textcahnged Does not work when not commented Gbase by anand and commented by anand 16-01-2024
                //rbtnAfterAWB.Attributes.Add("onclick", "return radioButtonChanged();");
                //rbtnFromAWB.Attributes.Add("onclick", "return radioButtonChanged();");
                //rbtnSight.Attributes.Add("onclick", "return radioButtonChanged();");
                //rbtnDA.Attributes.Add("onclick", "return radioButtonChanged();");
                //rbtnFromInvoice.Attributes.Add("onclick", "return radioButtonChanged();");
                //rbtnOthers.Attributes.Add("onclick", "return radioButtonChanged();");

                rbtnEBR.Attributes.Add("onclick", "return Calculate();");
                rbtnBDR.Attributes.Add("onclick", "return Calculate();");

                chkLoanAdv.Attributes.Add("onclick", "return Calculate();");

                txtExchRate.Attributes.Add("onblur", "return Calculate();");

                txtLibor.Attributes.Add("onblur", "return Calculate();");
                txtIntRate1.Attributes.Add("onblur", "return Calculate();");
                txtForDays1.Attributes.Add("onblur", "return Calculate();");
                txtOutOfDays.Attributes.Add("onblur", "return Calculate();");
                txtIntRate2.Attributes.Add("onblur", "return Calculate();");
                txtForDays2.Attributes.Add("onblur", "return Calculate();");
                txtDueDate.Attributes.Add("onblur", "return Calculate();");

                //txtBillAmount.Attributes.Add("onblur", "return Calculate();");
                //Call Calculate function and then FillGBaseDetailsBYNegoAmt function
                txtBillAmount.Attributes.Add("onblur", "return CallTwoFunctionsOnBillAmt();");

                txtBillAmountinRS.Attributes.Add("onblur", "return Calculate();");

                //txtNegotiatedAmt.Attributes.Add("onblur", "return Calculate();");
                txtNegotiatedAmt.Attributes.Add("onblur", "return FillGBaseDetailsBYNegoAmt();");

                txtNegotiatedAmtinRS.Attributes.Add("onblur", "return Calculate();");
                //txtInterest.Attributes.Add("onblur", "return Calculate();");
                txtInterest.Attributes.Add("onblur", "return FillGBaseDetailsBYInterestAmt();");

                txtInterestinRS.Attributes.Add("onblur", "return Calculate();");
                txtNetAmt.Attributes.Add("onblur", "return Calculate();");
                txtNetAmtinRS.Attributes.Add("onblur", "return Calculate();");
                txtOtherChrgs.Attributes.Add("onblur", "return Calculate();");
                txtBankCert.Attributes.Add("onblur", "return Calculate();");
                txtNegotiationFees.Attributes.Add("onblur", "return Calculate();");

                txtCourierChrgs.Attributes.Add("onblur", "return Calculate();");
                txtCourierChrgs.Attributes.Add("onblur", "return FillGBaseDetailsBYCourierChrgs();");

                txtMarginAmt.Attributes.Add("onblur", "return Calculate();");

                //txtCommission.Attributes.Add("onblur", "return Calculate();");
                txtCommission.Attributes.Add("onblur", "return FillGBaseDetailsBYCommissionAmt();");

                txtSTaxAmount.Attributes.Add("onblur", "return Calculate();");
                txtSTFXDLS.Attributes.Add("onblur", "return Calculate();");
                //txtExchRtEBR.Attributes.Add("onblur", "return Calculate();");

                //txtNoOfDays.Attributes.Add("onblur", "return Calculate();");
                //txtNoOfDays.Attributes.Add("onblur", "return CalculateDueDate();");

                txtLibor.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtNoOfDays.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtIntRate1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtForDays1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtOutOfDays.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtIntRate2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtForDays2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtBillAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtBillAmountinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtNegotiatedAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtNegotiatedAmtinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterest.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterestinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtNetAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtNetAmtinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtOtherChrgs.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtBankCert.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtNegotiationFees.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCourierChrgs.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtMarginAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCommission.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSTaxAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSTFXDLS.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCurrentAcinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmountGRPP.Attributes.Add("onkeydown", "return validate_Number(event);");

                //////txtTTbranchCode.Attributes.Add("onkeydown", "return validate_Number(event);");
                //////txtTTyear.Attributes.Add("onkeydown", "return validate_Number(event);");
                //////txtTTdocNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtReimbAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //New With Gbase
                txtGRExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtPCAmount1.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtPCAmount2.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtPCAmount3.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtPCAmount4.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtPCAmount5.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtPCAmount6.Attributes.Add("onkeydown", "return validate_Number(event);");
                //-----------------Anand 11-09-2023--------------------------------------
                txtTTAmount1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised1.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised2.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount3.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate3.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised3.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount4.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate4.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised4.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount5.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate5.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised5.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount6.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate6.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised6.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount7.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate7.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised7.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount8.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate8.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised8.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount9.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate9.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised9.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount10.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate10.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised10.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount11.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate11.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised11.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount12.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate12.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised12.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount13.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate13.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised13.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount14.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate14.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised14.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtTTAmount15.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTCrossCurrRate15.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtTTAmtRealised15.Attributes.Add("onkeydown", "return validate_Number(event);");

                //--------------EFRC--------------------
                txtFIRCAmount1_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount2_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount3_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount4_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount5_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount6_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount7_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount8_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount9_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount10_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount11_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount12_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount13_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount14_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCAmount15_OB.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtFIRCCrossCurrRate1_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate2_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate3_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate4_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate5_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate6_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate7_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate8_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate9_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate10_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate11_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate12_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate13_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate14_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCCrossCurrRate15_OB.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtFIRCTobeAdjustedinSB1_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB2_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB3_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB4_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB5_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB6_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB7_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB8_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB9_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB10_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB11_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB12_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB13_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB14_OB.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFIRCTobeAdjustedinSB15_OB.Attributes.Add("onkeydown", "return validate_Number(event);");


                //-----------------------End-----------------------------------------
                txtLCNo.Attributes.Add("onblur", "return chk1();");
                txtAccpDate.Attributes.Add("onkeydown", "return chkBankLineDt(event);");

                ddlCurrencyGRPP.Attributes.Add("onblur", "return AlertGRcurrency();");

                txtAWBDate.Attributes.Add("onblur", "return checkAWBDate(" + txtAWBDate.ClientID + ");");

                txtLCNoDate.Attributes.Add("onblur", "return checkSysDate(" + txtLCNoDate.ClientID + ");");
                txtBEDate.Attributes.Add("onblur", "return checkSysDate(" + txtBEDate.ClientID + ");");
                txtInvoiceDate.Attributes.Add("onblur", "return checkSysDate(" + txtInvoiceDate.ClientID + ");");

                txtPackingListDate.Attributes.Add("onblur", "return checkSysDate(" + txtPackingListDate.ClientID + ");");
                txtCustomsDate.Attributes.Add("onblur", "return checkSysDate(" + txtCustomsDate.ClientID + ");");
                txtInsPolicyDate.Attributes.Add("onblur", "return checkSysDate(" + txtInsPolicyDate.ClientID + ");");
                txtGSPDate.Attributes.Add("onblur", "return checkSysDate(" + txtGSPDate.ClientID + ");");
                txtFIRCdate.Attributes.Add("onblur", "return checkSysDate(" + txtFIRCdate.ClientID + ");");
                txtReimbClaimDate.Attributes.Add("onblur", "return checkValidateDate(" + txtReimbClaimDate.ClientID + ");");

                ddlTTRealisedCurr1.Attributes.Add("onblur", "return checkTTCurr(" + ddlTTCurrency1.ClientID + ddlTTRealisedCurr1.ClientID + ");");


                txtDateRcvd.Attributes.Add("onblur", "return isValidDate(" + txtDateRcvd.ClientID + "," + "'Date'" + " );");
                txtENCdate.Attributes.Add("onblur", "return isValidDate(" + txtENCdate.ClientID + "," + "'ENC Date'" + " );");
                txtDateNegotiated.Attributes.Add("onblur", "return isValidDate(" + txtDateNegotiated.ClientID + "," + "'Date'" + " );");
                txtDueDate.Attributes.Add("onblur", "return isValidDate(" + txtDueDate.ClientID + "," + "'Due Date'" + " );");
                txtAcceptedDueDate.Attributes.Add("onblur", "return isValidDate(" + txtAcceptedDueDate.ClientID + "," + "'Accepted Due Date'" + " );");
                txtShippingBillDate.Attributes.Add("onblur", "return isValidDate(" + txtShippingBillDate.ClientID + "," + "'Shipping Bill Date'" + " );");
                // txtReimbClaimDate.Attributes.Add("onblur", "return isValidDate(" + txtReimbClaimDate.ClientID + "," + "'Date'" + " );");
                txtReimbValDate.Attributes.Add("onblur", "return isValidDate(" + txtReimbValDate.ClientID + "," + "'Date'" + " );");

                txtIntToDate1.Attributes.Add("onblur", "GetNoOfDays(" + txtIntFrmDate1.ClientID + "," + txtIntToDate1.ClientID + "," + txtForDays1.ClientID + " );");
                txtIntToDate2.Attributes.Add("onblur", "GetNoOfDays(" + txtIntFrmDate2.ClientID + "," + txtIntToDate2.ClientID + "," + txtForDays2.ClientID + " );");
                txtIntToDate3.Attributes.Add("onblur", "GetNoOfDays(" + txtIntFrmDate3.ClientID + "," + txtIntToDate3.ClientID + "," + txtForDays3.ClientID + " );");
                txtIntToDate4.Attributes.Add("onblur", "GetNoOfDays(" + txtIntFrmDate4.ClientID + "," + txtIntToDate4.ClientID + "," + txtForDays4.ClientID + " );");
                txtIntToDate5.Attributes.Add("onblur", "GetNoOfDays(" + txtIntFrmDate5.ClientID + "," + txtIntToDate5.ClientID + "," + txtForDays5.ClientID + " );");
                txtIntToDate6.Attributes.Add("onblur", "GetNoOfDays(" + txtIntFrmDate6.ClientID + "," + txtIntToDate6.ClientID + "," + txtForDays6.ClientID + " );");

                btn_invsrno.Attributes.Add("onclick", "return openinvsrno();");
                btn_shipbillnohelp.Attributes.Add("onclick", "return shipbillnohelp();");

                txt_fbkcharges.Attributes.Add("onblur", "return fbkcal();");
                ddlServiceTaxfbk.Attributes.Add("onblur", "return fbkcal();");

                ////////////////////////Advance TT validation \\\\\\\\\\\\\\\\\\

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

                ddlFIRCRealisedCurr1_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency1_OB.ClientID + "," + ddlFIRCRealisedCurr1_OB.ClientID + ");");
                ddlFIRCRealisedCurr2_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency2_OB.ClientID + "," + ddlFIRCRealisedCurr2_OB.ClientID + ");");
                ddlFIRCRealisedCurr3_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency3_OB.ClientID + "," + ddlFIRCRealisedCurr3_OB.ClientID + ");");
                ddlFIRCRealisedCurr4_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency4_OB.ClientID + "," + ddlFIRCRealisedCurr4_OB.ClientID + ");");
                ddlFIRCRealisedCurr5_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency5_OB.ClientID + "," + ddlFIRCRealisedCurr5_OB.ClientID + ");");
                ddlFIRCRealisedCurr6_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency6_OB.ClientID + "," + ddlFIRCRealisedCurr6_OB.ClientID + ");");
                ddlFIRCRealisedCurr7_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency7_OB.ClientID + "," + ddlFIRCRealisedCurr7_OB.ClientID + ");");
                ddlFIRCRealisedCurr8_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency8_OB.ClientID + "," + ddlFIRCRealisedCurr8_OB.ClientID + ");");
                ddlFIRCRealisedCurr9_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency9_OB.ClientID + "," + ddlFIRCRealisedCurr9_OB.ClientID + ");");
                ddlFIRCRealisedCurr10_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency10_OB.ClientID + "," + ddlFIRCRealisedCurr10_OB.ClientID + ");");
                ddlFIRCRealisedCurr11_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency11_OB.ClientID + "," + ddlFIRCRealisedCurr11_OB.ClientID + ");");
                ddlFIRCRealisedCurr12_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency12_OB.ClientID + "," + ddlFIRCRealisedCurr12_OB.ClientID + ");");
                ddlFIRCRealisedCurr13_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency13_OB.ClientID + "," + ddlFIRCRealisedCurr13_OB.ClientID + ");");
                ddlFIRCRealisedCurr14_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency14_OB.ClientID + "," + ddlFIRCRealisedCurr14_OB.ClientID + ");");
                ddlFIRCRealisedCurr15_OB.Attributes.Add("onchange", "return checkTTCurr(" + ddlFIRCCurrency15_OB.ClientID + "," + ddlFIRCRealisedCurr15_OB.ClientID + ");");


                // txtAmountGRPP.Attributes.Add("onblur", "return calculateAmountinINR_GR();");
                // txtExchRateGR.Attributes.Add("onblur", "return calculateAmountinINR_GR();");
                //txtAmountinINRGR.Attributes.Add("onblur", "return calculateAmountinINR_GR();");


                //btnPcAcNo1.Attributes.Add("onclick", "return OpenSubACList('1');");
                //btnPcAcNo2.Attributes.Add("onclick", "return OpenSubACList('2');");
                //btnPcAcNo3.Attributes.Add("onclick", "return OpenSubACList('3');");
                //btnPcAcNo4.Attributes.Add("onclick", "return OpenSubACList('4');");
                //btnPcAcNo5.Attributes.Add("onclick", "return OpenSubACList('5');");
                //btnPcAcNo6.Attributes.Add("onclick", "return OpenSubACList('6');");

                //txtPCsubAcNo1.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCsubAcNo2.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCsubAcNo3.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCsubAcNo4.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCsubAcNo5.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCsubAcNo6.Attributes.Add("onkeydown", "return validate_AcNo(event);");

                //txtPCAcNo1.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCAcNo2.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCAcNo3.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCAcNo4.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCAcNo5.Attributes.Add("onkeydown", "return validate_AcNo(event);");
                //txtPCAcNo6.Attributes.Add("onkeydown", "return validate_AcNo(event);");

                //txtPCAmtinINR1.Attributes.Add("onkeydown", "return false;");
                //txtPCAmtinINR2.Attributes.Add("onkeydown", "return false;");
                //txtPCAmtinINR3.Attributes.Add("onkeydown", "return false;");
                //txtPCAmtinINR4.Attributes.Add("onkeydown", "return false;");
                //txtPCAmtinINR5.Attributes.Add("onkeydown", "return false;");
                //txtPCAmtinINR6.Attributes.Add("onkeydown", "return false;");

                txtInternalRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSpread.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtInterestLump.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCommissionMatu.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtPrincipalExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterestExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCommissionExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtPrincipalIntExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInterestIntExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCommissionIntExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCRCustAcNo2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCRAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCRIntAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCRPaymentCommAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCRHandlingCommAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCRPostageAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCRAmount1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRCustAcNo12.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRCustAcNo22.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRCustAcNo32.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRCustAcNo42.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRCustAcNo52.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRAmount1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRAmount2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRAmount3.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRAmount4.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDRAmount5.Attributes.Add("onkeydown", "return validate_Number(event);");

                txtBENo.Attributes.Add("onblur", "return FillDraft();");
                txtInvoiceNo.Attributes.Add("onblur", "return FillDraft();");

                ddlCurrency.Attributes.Add("onblur", "return FillGBaseDetailsBYCurrency();");
                SetInitialRow();

            }

            if (txtDateNegotiated.Text != "" && txtAWBDate.Text != "")
            {
                TF_DATA objData = new TF_DATA();
                SqlParameter p1 = new SqlParameter("@dt1", SqlDbType.VarChar);
                p1.Value = txtDateNegotiated.Text;
                SqlParameter p2 = new SqlParameter("@dt2", SqlDbType.VarChar);
                p2.Value = txtAWBDate.Text;
                string _query = "getdatediff";
                DataTable dt = objData.getData(_query, p1, p2);
                if (dt.Rows.Count > 0)
                {
                    hdnDateDiff.Value = dt.Rows[0]["Diff"].ToString();
                }

            }
            else
                hdnDateDiff.Value = "0";

            if (txtDateNegotiated.Text != "" && txtInvoiceDate.Text != "")
            {
                TF_DATA objData = new TF_DATA();
                SqlParameter p1 = new SqlParameter("@dt1", SqlDbType.VarChar);
                p1.Value = txtDateNegotiated.Text;
                SqlParameter p2 = new SqlParameter("@dt2", SqlDbType.VarChar);
                p2.Value = txtInvoiceDate.Text;
                string _query = "getdatediff";
                DataTable dt = objData.getData(_query, p1, p2);
                if (dt.Rows.Count > 0)
                {
                    hdnDateDiff_InvoiceDate.Value = dt.Rows[0]["Diff"].ToString();
                }

            }
            else
                hdnDateDiff_InvoiceDate.Value = "0";

            ////fillInterestRates();
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "serviceTax", "Calculate();", true);



        }
    }

    protected void fillCurrency(params DropDownList[] dropDowns)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);

        foreach (DropDownList ddlCurrency in dropDowns)// Added by Anand 03/02/2024
        {

            ddlCurrency.Items.Clear();

            ListItem li = new ListItem();
            li.Value = "0";
            if (dt.Rows.Count > 0)
            {
                li.Text = "---Select---";
                ddlCurrency.DataSource = dt.DefaultView;
                ddlCurrency.DataTextField = "C_Code";
                ddlCurrency.DataValueField = "C_Code";
                ddlCurrency.DataBind();

            }
            else
                li.Text = "No record(s) found";

            ddlCurrency.Items.Insert(0, li);

        }
    }

    protected void fillPortCodes()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetPortCodeMasterList";
        DataTable dt = objData.getData(_query, p1);
        ddlPortCode.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlPortCode.DataSource = dt.DefaultView;
            ddlPortCode.DataTextField = "port_Code";
            ddlPortCode.DataValueField = "port_Code";
            ddlPortCode.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlPortCode.Items.Insert(0, li);

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
            ddlServiceTax.DataTextField = "SERVICE_TAX";
            ddlServiceTax.DataValueField = "SERVICE_TAX";
            ddlServiceTax.DataBind();

            ddlServiceTaxfbk.DataSource = dt.DefaultView;
            ddlServiceTaxfbk.DataTextField = "SERVICE_TAX";
            ddlServiceTaxfbk.DataValueField = "SERVICE_TAX";
            ddlServiceTaxfbk.DataBind();

            hdnServiceTax.Value = dt.Rows[0]["SERVICE_TAX"].ToString();
            hdnEDU_CESS.Value = dt.Rows[0]["EDU_CESS"].ToString();
            hdnSEC_EDU_CESS.Value = dt.Rows[0]["SEC_EDU_CESS"].ToString();

            txtsbcess.Text = dt.Rows[0]["SBCess"].ToString();
            txt_kkcessper.Text = dt.Rows[0]["KKCess"].ToString();


            txtsbcessfbk.Text = dt.Rows[0]["SBCess"].ToString();
            txt_kkcessperfbk.Text = dt.Rows[0]["KKCess"].ToString();

            txtsbcess.Enabled = false;
            txt_kkcessper.Enabled = false;

            txtsbcessfbk.Enabled = false;
            txt_kkcessperfbk.Enabled = false;

        }
    }

    //protected void fillCountry()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetCountryList";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlCountry.Items.Clear();

    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "";
    //        ddlCountry.DataSource = dt.DefaultView;
    //        ddlCountry.DataTextField = "CountryID";
    //        ddlCountry.DataValueField = "CountryID";
    //        ddlCountry.DataBind();

    //    }
    //    else
    //        li.Text = "No record(s) found";

    //    ddlCountry.Items.Insert(0, li);

    //}

    ////protected void fillInterestRates()
    ////{
    ////    TF_DATA objData = new TF_DATA();
    ////    string _query = "TF_EXP_GetInterestRates";

    ////    DataTable dt = objData.getData(_query);

    ////    if (dt.Rows.Count > 0)
    ////    {
    ////        //////hdnSightBillMaxDays.Value = dt.Rows[0]["SightBillMaxDays"].ToString();
    ////        //////hdnSightBillOutOfDays.Value = dt.Rows[0]["SightBillOutOfDays"].ToString();
    ////        //////hdnSightBillInterestRate.Value = dt.Rows[0]["SightBillInterestRate"].ToString();
    ////        //////hdnUsanceBillMaxDays.Value = dt.Rows[0]["UsanceBillMaxDays"].ToString();
    ////        //////hdnUsanceBillToDays1.Value = dt.Rows[0]["UsanceBillToDays1"].ToString();
    ////        //////hdnUsanceBillInterestRate1.Value = dt.Rows[0]["UsanceBillInterestRate1"].ToString();
    ////        //////hdnUsanceBillInterestRate2.Value = dt.Rows[0]["UsanceBillInterestRate2"].ToString();
    ////        //////hdnUsanceBillOutOfDays.Value = dt.Rows[0]["UsanceBillOutOfDays"].ToString();
    ////        //////hdnEBROutOfDays.Value = dt.Rows[0]["EBROutOfDays"].ToString();
    ////        //////hdnEBRInterestRate.Value = dt.Rows[0]["EBRInterestRate"].ToString();
    ////        hdnBankCert.Value = dt.Rows[0]["BankCert"].ToString();
    ////        hdnNegoFees.Value = dt.Rows[0]["NegoFees"].ToString();
    ////        hdnCourierCharges.Value = dt.Rows[0]["CourierCharges"].ToString();

    ////    }
    ////}

    #endregion

    #region Help Codes

    protected void btnCustomerCode_Click(object sender, EventArgs e)
    {
        if (hdnCustomerCode.Value != "")
        {
            txtCustAcNo.Text = hdnCustomerCode.Value;
            fillCustomerCodeDescription();
            txtCustAcNo.Focus();
            Check_LEI_ThresholdLimit();

        }
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

    protected void btnOverseasBank_Click(object sender, EventArgs e)
    {
        if (hdnOverseasId.Value != "")
        {
            txtOverseasBankID.Text = hdnOverseasId.Value;
            fillOverseasBankDescription();
            txtOverseasBankID.Focus();
        }
    }

    //protected void btnPortCode_Click(object sender, EventArgs e)
    //{
    //    if (hdnPortCode.Value != "")
    //    {
    //        txtCoveringFrom.Text = hdnPortCode.Value;

    //        txtCoveringFrom.Focus();
    //    }
    //}

    protected void btnCountry_Click(object sender, EventArgs e)
    {
        if (hdnCountry.Value != "")
        {
            txtCountry.Text = hdnCountry.Value;
            txtCountry_TextChanged(null, null);
            txtCountry.Focus();
        }
    }

    //protected void btnTTRefNo_Click(object sender, EventArgs e)
    //{
    //    if (hdnTTRefNo.Value != "")
    //    {
    //        txtTTprFx.Text = hdnTTRefNo.Value.Substring(0, 2);
    //        txtTTdocNo.Text = hdnTTRefNo.Value.Substring(3, 5);
    //        txtTTbranchCode.Text = hdnTTRefNo.Value.Substring(9, 5);
    //        txtTTyear.Text = hdnTTRefNo.Value.Substring(15, 4);
    //    }
    //}

    private void fillCustomerCodeDescription()
    {
        lblCustomerDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtCustAcNo.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            String CustAdd = dt.Rows[0]["CUST_ADDRESS"].ToString().Trim();
            hdnCustname.Value = lblCustomerDesc.Text;
            lblCustomerDesc.ToolTip = "Name: " + lblCustomerDesc.Text + "  Address: " + CustAdd;
            if (lblCustomerDesc.Text.Length > 20)
            {
                lblCustomerDesc.Text = lblCustomerDesc.Text.Substring(0, 20) + "...";
            }

            txtRiskCustomer.Text = dt.Rows[0]["Cust_Abbr"].ToString().Trim();

            if (dt.Rows[0]["Cust_Abbr"].ToString().Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Cust Abbr does not exist in Customer Master!\\nPlease Update Customer Abbr in Customer Master');", true);
            }

            //txtCRGLCode.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            //txtCRCustAbbr.Text = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            //txtCRCustAcNo1.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            //txtCRCustAcNo2.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();

            //txtDRGLCode1.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            //txtDRCustAbbr1.Text = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            //txtDRCustAcNo11.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            //txtDRCustAcNo12.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
        }
        else
        {
            txtCustAcNo.Text = "";
            lblCustomerDesc.Text = "";
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
            String OverAdd = dt.Rows[0]["Party_Address"].ToString().Trim();
            lblOverseasPartyDesc.ToolTip = "Name: " + lblOverseasPartyDesc.Text + "  Address: " + OverAdd;
            if (lblOverseasPartyDesc.Text.Length > 20)
            {
                lblOverseasPartyDesc.Text = lblOverseasPartyDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOverseasPartyID.Text = "";
            lblOverseasPartyDesc.Text = "";
        }

    }

    private void fillOverseasBankDescription()
    {
        lblOverseasBankDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtOverseasBankID.Text;
        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
            String OverBAdd = dt.Rows[0]["BankAddress"].ToString().Trim();
            txtLCNoIssuedBy.Text = lblOverseasBankDesc.Text;
            lblOverseasBankDesc.ToolTip = "Name: " + lblOverseasBankDesc.Text + "  Address: " + OverBAdd;
            if (lblOverseasBankDesc.Text.Length > 20)
            {
                lblOverseasBankDesc.Text = lblOverseasBankDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOverseasBankID.Text = "";
            lblOverseasBankDesc.Text = "";
        }

    }

    private void fillReimbBankDescription()
    {
        lblReimbBankDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtReimbBank.Text;
        string _query = "TF_GetReimburseBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblReimbBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();

            if (lblReimbBankDesc.Text.Length > 20)
            {
                lblReimbBankDesc.ToolTip = lblReimbBankDesc.Text;
                lblReimbBankDesc.Text = lblReimbBankDesc.Text.Substring(0, 20) + "...";

            }
            if (ddlDispachInd.Text.Trim() == "By Bank")  //Added by bhupen 23032023
            {
                txtSpecialInstructions1.Text = dt.Rows[0]["Special_Instructions1"].ToString().Trim();
                txtSpecialInstructions2.Text = dt.Rows[0]["Special_Instructions2"].ToString().Trim();
                txtSpecialInstructions3.Text = dt.Rows[0]["Special_Instructions3"].ToString().Trim();
                txtSpecialInstructions4.Text = dt.Rows[0]["Special_Instructions4"].ToString().Trim();
                txtSpecialInstructions5.Text = dt.Rows[0]["Special_Instructions5"].ToString().Trim();
                // txtSpecialInstructions6.Text = dt.Rows[0]["Special_Instructions6"].ToString().Trim();
                txtSpecialInstructions7.Text = dt.Rows[0]["Special_Instructions7"].ToString().Trim();
                txtSpecialInstructions8.Text = dt.Rows[0]["Special_Instructions8"].ToString().Trim();
                txtSpecialInstructions9.Text = dt.Rows[0]["Special_Instructions9"].ToString().Trim();
                txtSpecialInstructions10.Text = dt.Rows[0]["Special_Instructions10"].ToString().Trim();
            }
            else     //Added by bhupen 23032023
            {
                txtSpecialInstructions1.Text = "";
                txtSpecialInstructions2.Text = "";
                txtSpecialInstructions3.Text = "";
                txtSpecialInstructions4.Text = "";
                txtSpecialInstructions5.Text = "";
                txtSpecialInstructions6.Text = "";
                txtSpecialInstructions7.Text = "";
                txtSpecialInstructions8.Text = "";
                txtSpecialInstructions9.Text = "";
                txtSpecialInstructions10.Text = "";
            }
        }
        else
        {
            txtReimbBank.Text = "";
            lblReimbBankDesc.Text = "";

            txtSpecialInstructions1.Text = "";
            txtSpecialInstructions2.Text = "";
            txtSpecialInstructions3.Text = "";
            txtSpecialInstructions4.Text = "";
            txtSpecialInstructions5.Text = "";
            txtSpecialInstructions6.Text = "";
            txtSpecialInstructions7.Text = "";
            txtSpecialInstructions8.Text = "";
            txtSpecialInstructions9.Text = "";
            txtSpecialInstructions10.Text = "";

            txtReimbBank.Focus();
        }
    }

    private void fillCommodityDescription()
    {
        lblCommodityDescription.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@CommodityID", SqlDbType.VarChar);
        p1.Value = txtCommodityID.Text;
        string _query = "TF_GetCommodityMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCommodityDescription.Text = dt.Rows[0]["CommodityDescription"].ToString().Trim();
            if (lblCommodityDescription.Text.Length > 30)
            {
                lblCommodityDescription.ToolTip = lblCommodityDescription.Text;
                lblCommodityDescription.Text = lblCommodityDescription.Text.Substring(0, 30) + "...";
            }
        }
        else
        {
            txtCommodityID.Text = "";
            lblCommodityDescription.Text = "";
        }
    }

    private void fillGBaseCommodityDescription()
    {
        lblGBaseCommodityDescription.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@CommodityID", SqlDbType.VarChar);
        p1.Value = txtGBaseCommodityID.Text;
        string _query = "TF_GetGBASECommodityMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblGBaseCommodityDescription.Text = dt.Rows[0]["GBase_Commodity_Description"].ToString().Trim();
            if (lblGBaseCommodityDescription.Text.Length > 30)
            {
                lblGBaseCommodityDescription.ToolTip = lblGBaseCommodityDescription.Text;
                lblGBaseCommodityDescription.Text = lblGBaseCommodityDescription.Text.Substring(0, 30) + "...";
            }
        }
        else
        {
            txtGBaseCommodityID.Text = "";
            lblGBaseCommodityDescription.Text = "";
        }
    }

    #endregion

    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        string _cName = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Countrycode", SqlDbType.VarChar);

        p1.Value = txtCountry.Text.Trim();
        string _query = "TF_CheckSanctionedCountry";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString().Trim();
            if (lblCountryDesc.Text.Length > 10)
            {
                lblCountryDesc.ToolTip = lblCountryDesc.Text;
                lblCountryDesc.Text = lblCountryDesc.Text.Substring(0, 10) + "...";
            }
            if (dt.Rows[0]["SanctionedCountryCount"].ToString().Trim() != "0")
            {
                _cName = dt.Rows[0]["CountryName"].ToString().Trim();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('" + _cName + " is a Sanctioned Country.');", true);
            }
        }
        else
        {
            txtCountry.Text = "";
            lblCountryDesc.Text = "";
        }
        txtCountry.Focus();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewExportBillEntry.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime CheckDueDate = new DateTime();
        CheckDueDate = Convert.ToDateTime(txtDueDate.Text, dateInfo);

        string _prFxvalid = Request.QueryString["DocPrFx"].Trim();
        hdnTTCurrCheck.Value = "";
        if (_prFxvalid == "EB")
        {
            ValidateTTCurr();
            TTFIRCtotalCaluation();
        }
        if (ddlDispBydefault.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Dispatch Indicator');", true);
            ddlDispBydefault.Focus();

        }
        else if (ddlDispachInd.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Dispatch Indicator');", true);
            ddlDispachInd.Focus();
        }
        else if (hdnLeiFlag.Value == "Y" && lblLEI_CUST_Remark.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify LEI details.');", true);
        }
        else if (_prFxvalid != "EB" && rbtnAfterAWB.Checked == false && rbtnFromAWB.Checked == false && rbtnSight.Checked == false && rbtnDA.Checked == false && rbtnFromInvoice.Checked == false && rbtnOthers.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select tenor bill.');", true);
        }
        else if ((_prFxvalid != "EB" && rbtnAfterAWB.Checked == true || rbtnFromAWB.Checked == true || rbtnSight.Checked == true || rbtnDA.Checked == true || rbtnFromInvoice.Checked == true || rbtnOthers.Checked == true) && (txtOtherTenorRemarks.Text.Trim() == ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Description of tenor should not be blank.');", true);
        }
        else if (txtBillAmount.Text.Trim() == "" || txtBillAmount.Text.Trim() == "0.00")
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter Bill Amount.')", true);
            txtBillAmount.Focus();
        }
        //--------------------------Anand 21062023------------------------------------------
        else if ((_prFxvalid != "EB" && rbtnAfterAWB.Checked == true || rbtnFromAWB.Checked == true || rbtnSight.Checked == true || rbtnDA.Checked == true || rbtnFromInvoice.Checked == true || rbtnOthers.Checked == true) && (txtOtherTenorRemarks.Text.Trim() == ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Description of tenor should not be blank.');", true);
        }
        else if (_prFxvalid == "EB" && (txtFIRCNo1_OB.Text.Trim() != "" && txtFIRCADCode1_OB.Text.Trim() == "") || (txtFIRCNo2_OB.Text.Trim() != "" && txtFIRCADCode2_OB.Text.Trim() == "") ||
       (txtFIRCNo3_OB.Text.Trim() != "" && txtFIRCADCode3_OB.Text.Trim() == "") || (txtFIRCNo4_OB.Text.Trim() != "" && txtFIRCADCode4_OB.Text.Trim() == "")
       || (txtFIRCNo5_OB.Text.Trim() != "" && txtFIRCADCode5_OB.Text.Trim() == "") || (txtFIRCNo6_OB.Text.Trim() != "" && txtFIRCADCode6_OB.Text.Trim() == "")
       || (txtFIRCNo7_OB.Text.Trim() != "" && txtFIRCADCode7_OB.Text.Trim() == "") || (txtFIRCNo8_OB.Text.Trim() != "" && txtFIRCADCode8_OB.Text.Trim() == "")
       || (txtFIRCNo9_OB.Text.Trim() != "" && txtFIRCADCode9_OB.Text.Trim() == "") || (txtFIRCNo10_OB.Text.Trim() != "" && txtFIRCADCode10_OB.Text.Trim() == "")
       || (txtFIRCNo11_OB.Text.Trim() != "" && txtFIRCADCode11_OB.Text.Trim() == "") || (txtFIRCNo12_OB.Text.Trim() != "" && txtFIRCADCode12_OB.Text.Trim() == "")
       || (txtFIRCNo13_OB.Text.Trim() != "" && txtFIRCADCode13_OB.Text.Trim() == "") || (txtFIRCNo14_OB.Text.Trim() != "" && txtFIRCADCode14_OB.Text.Trim() == "")
       || (txtFIRCNo15_OB.Text.Trim() != "" && txtFIRCADCode15_OB.Text.Trim() == ""))
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter FIRC ADCode.')", true);
            // txtFIRCADCode1_OB.Focus();
        }
        else if (CheckDueDate <= DateTime.Today && _prFxvalid != "EB")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Due Date is Less than Today Day.');", true);
        }
        else if (CheckDueDate < DateTime.Today && _prFxvalid == "EB")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Due Date is Less than Today Day.');", true);
        }
        else if (hdnTTCurrCheck.Value == "TTfalse")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT cross curr rate.');", true);
        }
        else if (hdnTTCurrCheck.Value == "FIRCfalse")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter FIRC cross curr rate.');", true);
        }
        else if (hdnTTFIRCTotalAmtCheck.Value == "Greater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('TT or FIRC Amount is More Than Bill Amount.');", true);
        }
        else if ((txtTTRefNo1.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency1.SelectedValue && ddlTTRealisedCurr1.SelectedValue == "0") || (txtTTRefNo2.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency2.SelectedValue && ddlTTRealisedCurr2.SelectedValue == "0") ||
            (txtTTRefNo3.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency3.SelectedValue && ddlTTRealisedCurr3.SelectedValue == "0") || (txtTTRefNo4.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency4.SelectedValue && ddlTTRealisedCurr4.SelectedValue == "0") ||
            (txtTTRefNo5.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency5.SelectedValue && ddlTTRealisedCurr5.SelectedValue == "0") || (txtTTRefNo6.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency6.SelectedValue && ddlTTRealisedCurr6.SelectedValue == "0") ||
            (txtTTRefNo7.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency7.SelectedValue && ddlTTRealisedCurr7.SelectedValue == "0") || (txtTTRefNo8.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency8.SelectedValue && ddlTTRealisedCurr8.SelectedValue == "0") ||
            (txtTTRefNo9.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency9.SelectedValue && ddlTTRealisedCurr9.SelectedValue == "0") || (txtTTRefNo10.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency10.SelectedValue && ddlTTRealisedCurr10.SelectedValue == "0") ||
            (txtTTRefNo11.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency11.SelectedValue && ddlTTRealisedCurr11.SelectedValue == "0") || (txtTTRefNo12.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency12.SelectedValue && ddlTTRealisedCurr12.SelectedValue == "0") ||
            (txtTTRefNo13.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency13.SelectedValue && ddlTTRealisedCurr13.SelectedValue == "0") || (txtTTRefNo14.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency14.SelectedValue && ddlTTRealisedCurr14.SelectedValue == "0") ||
            (txtTTRefNo15.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency15.SelectedValue && ddlTTRealisedCurr15.SelectedValue == "0"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT Realised Currency.');", true);
        }
        else if ((txtTTRefNo1.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency1.SelectedValue && (txtTTCrossCurrRate1.Text == "0" || txtTTCrossCurrRate1.Text == "")) ||
            (txtTTRefNo2.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency2.SelectedValue && (txtTTCrossCurrRate2.Text == "0" || txtTTCrossCurrRate2.Text == "")) ||
            (txtTTRefNo3.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency3.SelectedValue && (txtTTCrossCurrRate3.Text == "0" || txtTTCrossCurrRate3.Text == "")) ||
            (txtTTRefNo4.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency4.SelectedValue && (txtTTCrossCurrRate4.Text == "0" || txtTTCrossCurrRate4.Text == "")) ||
            (txtTTRefNo5.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency5.SelectedValue && (txtTTCrossCurrRate5.Text == "0" || txtTTCrossCurrRate5.Text == "")) ||
            (txtTTRefNo6.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency6.SelectedValue && (txtTTCrossCurrRate6.Text == "0" || txtTTCrossCurrRate6.Text == "")) ||
            (txtTTRefNo7.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency7.SelectedValue && (txtTTCrossCurrRate7.Text == "0" || txtTTCrossCurrRate7.Text == "")) ||
            (txtTTRefNo8.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency8.SelectedValue && (txtTTCrossCurrRate8.Text == "0" || txtTTCrossCurrRate8.Text == "")) ||
            (txtTTRefNo9.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency9.SelectedValue && (txtTTCrossCurrRate9.Text == "0" || txtTTCrossCurrRate9.Text == "")) ||
            (txtTTRefNo10.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency10.SelectedValue && (txtTTCrossCurrRate10.Text == "0" || txtTTCrossCurrRate10.Text == "")) ||
            (txtTTRefNo11.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency11.SelectedValue && (txtTTCrossCurrRate11.Text == "0" || txtTTCrossCurrRate11.Text == "")) ||
            (txtTTRefNo12.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency12.SelectedValue && (txtTTCrossCurrRate12.Text == "0" || txtTTCrossCurrRate12.Text == "")) ||
            (txtTTRefNo13.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency13.SelectedValue && (txtTTCrossCurrRate13.Text == "0" || txtTTCrossCurrRate13.Text == "")) ||
            (txtTTRefNo14.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency14.SelectedValue && (txtTTCrossCurrRate14.Text == "0" || txtTTCrossCurrRate14.Text == "")) ||
            (txtTTRefNo15.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency15.SelectedValue && (txtTTCrossCurrRate15.Text == "0" || txtTTCrossCurrRate15.Text == "")))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT Cross Curr. Rate.');", true);
        }
        else if ((txtFIRCNo1_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency1_OB.SelectedValue && ddlFIRCRealisedCurr1_OB.SelectedValue == "0") ||
            (txtFIRCNo2_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency2_OB.SelectedValue && ddlFIRCRealisedCurr2_OB.SelectedValue == "0") ||
            (txtFIRCNo3_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency3_OB.SelectedValue && ddlFIRCRealisedCurr3_OB.SelectedValue == "0") ||
            (txtFIRCNo4_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency4_OB.SelectedValue && ddlFIRCRealisedCurr4_OB.SelectedValue == "0") ||
            (txtFIRCNo5_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency5_OB.SelectedValue && ddlFIRCRealisedCurr5_OB.SelectedValue == "0") ||
            (txtFIRCNo6_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency6_OB.SelectedValue && ddlFIRCRealisedCurr6_OB.SelectedValue == "0") ||
            (txtFIRCNo7_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency7_OB.SelectedValue && ddlFIRCRealisedCurr7_OB.SelectedValue == "0") ||
            (txtFIRCNo8_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency8_OB.SelectedValue && ddlFIRCRealisedCurr8_OB.SelectedValue == "0") ||
            (txtFIRCNo9_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency9_OB.SelectedValue && ddlFIRCRealisedCurr9_OB.SelectedValue == "0") ||
            (txtFIRCNo10_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency10_OB.SelectedValue && ddlFIRCRealisedCurr10_OB.SelectedValue == "0") ||
            (txtFIRCNo11_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency11_OB.SelectedValue && ddlFIRCRealisedCurr11_OB.SelectedValue == "0") ||
            (txtFIRCNo12_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency12_OB.SelectedValue && ddlFIRCRealisedCurr12_OB.SelectedValue == "0") ||
            (txtFIRCNo13_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency13_OB.SelectedValue && ddlFIRCRealisedCurr13_OB.SelectedValue == "0") ||
            (txtFIRCNo14_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency14_OB.SelectedValue && ddlFIRCRealisedCurr14_OB.SelectedValue == "0") ||
            (txtFIRCNo15_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency15_OB.SelectedValue && ddlFIRCRealisedCurr15_OB.SelectedValue == "0"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter FIRC Realised Currency.');", true);
        }
        else if ((txtFIRCNo1_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency1_OB.SelectedValue && (txtFIRCCrossCurrRate1_OB.Text == "0" || txtFIRCCrossCurrRate1_OB.Text == "")) ||
            (txtFIRCNo2_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency2_OB.SelectedValue && (txtFIRCCrossCurrRate2_OB.Text == "0" || txtFIRCCrossCurrRate2_OB.Text == "")) ||
            (txtFIRCNo3_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency3_OB.SelectedValue && (txtFIRCCrossCurrRate3_OB.Text == "0" || txtFIRCCrossCurrRate3_OB.Text == "")) ||
            (txtFIRCNo4_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency4_OB.SelectedValue && (txtFIRCCrossCurrRate4_OB.Text == "0" || txtFIRCCrossCurrRate4_OB.Text == "")) ||
            (txtFIRCNo5_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency5_OB.SelectedValue && (txtFIRCCrossCurrRate5_OB.Text == "0" || txtFIRCCrossCurrRate5_OB.Text == "")) ||
            (txtFIRCNo6_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency6_OB.SelectedValue && (txtFIRCCrossCurrRate6_OB.Text == "0" || txtFIRCCrossCurrRate6_OB.Text == "")) ||
            (txtFIRCNo7_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency7_OB.SelectedValue && (txtFIRCCrossCurrRate7_OB.Text == "0" || txtFIRCCrossCurrRate7_OB.Text == "")) ||
            (txtFIRCNo8_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency8_OB.SelectedValue && (txtFIRCCrossCurrRate8_OB.Text == "0" || txtFIRCCrossCurrRate8_OB.Text == "")) ||
            (txtFIRCNo9_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency9_OB.SelectedValue && (txtFIRCCrossCurrRate9_OB.Text == "0" || txtFIRCCrossCurrRate9_OB.Text == "")) ||
            (txtFIRCNo10_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency10_OB.SelectedValue && (txtFIRCCrossCurrRate10_OB.Text == "0" || txtFIRCCrossCurrRate10_OB.Text == "")) ||
            (txtFIRCNo11_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency11_OB.SelectedValue && (txtFIRCCrossCurrRate11_OB.Text == "0" || txtFIRCCrossCurrRate11_OB.Text == "")) ||
            (txtFIRCNo12_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency12_OB.SelectedValue && (txtFIRCCrossCurrRate12_OB.Text == "0" || txtFIRCCrossCurrRate12_OB.Text == "")) ||
            (txtFIRCNo13_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency13_OB.SelectedValue && (txtFIRCCrossCurrRate13_OB.Text == "0" || txtFIRCCrossCurrRate13_OB.Text == "")) ||
            (txtFIRCNo14_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency14_OB.SelectedValue && (txtFIRCCrossCurrRate14_OB.Text == "0" || txtFIRCCrossCurrRate14_OB.Text == "")) ||
            (txtFIRCNo15_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency15_OB.SelectedValue && (txtFIRCCrossCurrRate15_OB.Text == "0" || txtFIRCCrossCurrRate15_OB.Text == "")))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter FIRC Cross Curr. Rate.');", true);
        }
        else if (GridViewGRPPCustomsDetails.Visible == true && GridViewGRPPCustomsDetails.Rows.Count != Convert.ToInt32(txtNoOfSB.Text) && chkSB.Checked==false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No. of Shipping bill Mismatch please check pending shipping bill flag.');", true);
        }
        else if (GridViewGRPPCustomsDetails.Visible == false && GridViewGRPPCustomsDetails.Rows.Count - 1 != Convert.ToInt32(txtNoOfSB.Text) && chkSB.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No. of Shipping bill Mismatch please check pending shipping bill flag.');", true);
        }
        else if (hdnLeiFlag.Value == "Y" && lblLEI_CUST_Remark.Text == "")
        {
           // LEIAmtCheck.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit. ";
            LEIverify.Text = "";
            LEIverify.Text = "Please Verify LEI.";
            LEIverify.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify LEI details.');", true);
        }
        else if (hdnLeiSpecialFlag.Value == "R" && lblLEI_CUST_Remark.Text == "")
        {
            //LEIAmtCheck.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit. ";
            LEIverify.Text = "";
            LEIverify.Text = "Please Verify LEI.";
            LEIverify.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Verify LEI details for Recurring Customer.');", true);
        }
        else
        {
            if (ddlMercTrade.Text.Trim() == "No")
            {
                if (GridViewGRPPCustomsDetails.Visible == true)
                {
                    //int rowIndex = 0;
                    if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Merchant Trade is No, Shipping Bill is mandatory.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Merchant Trade is No, Shipping Bill is mandatory.');", true);
                }
            }
            string _result = "";
            Boolean _proceed = true;
            string _script = "";
            string _userName = Session["userName"].ToString().Trim();
            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string _mode = Request.QueryString["mode"].Trim();
            string _ForeignOrLocal = Request.QueryString["ForeignOrLocal"].ToString().Trim();
            string _prFx = Request.QueryString["DocPrFx"].Trim();

            if (_prFx == "EB")
            {
                if (txtTTRefNo1.Text == "" && txtFIRCNo1_OB.Text == "")
                {

                    _proceed = false;

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('TTRefno cant be blank for advance.');", true);
                }
            }
            Boolean _proceed1 = true;
            //if (_prFx == "EB")
            //{
            //    double TTAmt = 0;
            //    double BillAmt = 0;
            //    if (txtBillAmount.Text != "")
            //    {
            //        BillAmt = Convert.ToDouble(txtBillAmount.Text);
            //    }
            //    else
            //    {

            //        BillAmt = 0;
            //    }
            //    if (hdnTTTotalAmt.Value != "")
            //    {
            //        TTAmt = Convert.ToDouble(hdnTTTotalAmt.Value);

            //    }
            //    else
            //    {
            //        TTAmt = 0;

            //    }
            //    _proceed1 = false;
            //    if (TTAmt > BillAmt)
            //    {
            //        _proceed1 = false;

            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('TT Amount More Than Bill Amount.');", true);
            //    }
            //    else
            //    {
            //        _proceed1 = true;

            //    }
            //  }
            getLastDocNo();
            TF_DATA objCheck = new TF_DATA();
            string _queryCheck = "TF_EXP_CheckShippingBillNo";
            SqlParameter cShippingBillNo = new SqlParameter("@shippingBillNo", SqlDbType.VarChar);
            SqlParameter cDocumentNo = new SqlParameter("@documentNo", SqlDbType.VarChar);
            cDocumentNo.Value = txtDocumentNo.Text;

            SqlParameter cShippingBillDate = new SqlParameter("@ShippingBillDate", SqlDbType.VarChar);
            SqlParameter cPort = new SqlParameter("@PortCode", SqlDbType.VarChar);
            SqlParameter cBranchCode = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            cBranchCode.Value = Request.QueryString["BranchCode"].Trim();

            if (GridViewGRPPCustomsDetails.Rows.Count >= 0)
            {
                for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
                {
                    Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[4].FindControl("lblShippingBillNo");
                    cShippingBillNo.Value = lbl5.Text;

                    Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[5].FindControl("lblShippingBillDate");
                    cShippingBillDate.Value = lbl6.Text;

                    Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[15].FindControl("lblPortCode");
                    cPort.Value = lbl7.Text;

                    if (lbl5.Text != "")
                    {
                        DataTable dtcheck = objCheck.getData(_queryCheck, cShippingBillNo, cDocumentNo, cShippingBillDate, cPort, cBranchCode);
                        if (dtcheck.Rows.Count > 0)
                        {
                            _proceed = false;
                            _script = "alert('Shipping Bill No: " + lbl5.Text + " is assigned to " + dtcheck.Rows[0]["Document_No"].ToString() + "')";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            // txtRefNo.Focus();
                        }
                    }
                }
            }
            //if (_proceed == true)
            //{
            //    if (hdnIsRealised.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already Realised, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}
            //if (_proceed == true)
            //{
            //    if (hdnIsDelinked.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already Delinked, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}
            //if (_proceed == true)
            //{
            //    if (hdnIsWrittenOff.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already WrittenOff, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}
            //if (_proceed == true)
            //{
            //    if (hdnIsExtended.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already Entended, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}

            if (_proceed1 == true)
            {
                if (_proceed == true)
                {
                    SqlParameter bCode = new SqlParameter("@bCode", SqlDbType.VarChar);
                    bCode.Value = Request.QueryString["BranchCode"].Trim();

                    SqlParameter docNumber = new SqlParameter("@docNo", SqlDbType.VarChar);
                    docNumber.Value = txtDocumentNo.Text.Trim();

                    SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
                    p1.Value = _mode;

                    SqlParameter p2 = new SqlParameter("@docPrfx", SqlDbType.VarChar);
                    p2.Value = Request.QueryString["DocPrFx"].Trim();

                    SqlParameter p3 = new SqlParameter("@docSrNo", SqlDbType.VarChar);
                    p3.Value = Request.QueryString["DocSrNo"].Trim();

                    SqlParameter p4 = new SqlParameter("@docNoYear", SqlDbType.VarChar);
                    p4.Value = Request.QueryString["DocYear"].Trim();

                    SqlParameter p5 = new SqlParameter("@billType", SqlDbType.VarChar);
                    string _billType = "";
                    if (rbtnSightBill.Checked)
                        _billType = "S";
                    else if (rbtnUsanceBill.Checked)
                        _billType = "U";
                    p5.Value = _billType;

                    SqlParameter pCustAcNo = new SqlParameter("@custAcNo", SqlDbType.VarChar);
                    pCustAcNo.Value = txtCustAcNo.Text.Trim();

                    SqlParameter pOverseasPartyID = new SqlParameter("@OverseasPartyID", SqlDbType.VarChar);
                    pOverseasPartyID.Value = txtOverseasPartyID.Text.Trim();

                    SqlParameter pOverseasBankID = new SqlParameter("@OverseasBankID", SqlDbType.VarChar);
                    pOverseasBankID.Value = txtOverseasBankID.Text.Trim();

                    SqlParameter pCustCheck = new SqlParameter("@custCheck", "");
                    //string _CustCheck = "B";
                    //if (chkCustCheck.Checked)
                    //    _CustCheck = "C";
                    //pCustCheck.Value = _CustCheck;

                    SqlParameter pCS = new SqlParameter("@cs", "");
                    //string _CS = "N";
                    //if (chkCS.Checked)
                    //    _CS = "Y";
                    //pCS.Value = _CS;

                    SqlParameter pDateRcvd = new SqlParameter("@dateRcvd", SqlDbType.VarChar);
                    pDateRcvd.Value = txtDateRcvd.Text.Trim();

                    SqlParameter pENCdate = new SqlParameter("@encDate", SqlDbType.VarChar);
                    pENCdate.Value = txtENCdate.Text.Trim();

                    SqlParameter pDateNegotiated = new SqlParameter("@dateNegaotiated", SqlDbType.VarChar);
                    pDateNegotiated.Value = txtDateNegotiated.Text.Trim();

                    SqlParameter pLCNo = new SqlParameter("@LCNo", SqlDbType.VarChar);
                    pLCNo.Value = txtLCNo.Text.Trim();

                    SqlParameter pLCDate = new SqlParameter("@LCDate", SqlDbType.VarChar);
                    pLCDate.Value = txtLCNoDate.Text.Trim();

                    SqlParameter pLCissuedBy = new SqlParameter("@LCissuedBy", SqlDbType.VarChar);
                    pLCissuedBy.Value = txtLCNoIssuedBy.Text.Trim();

                    SqlParameter pBENo = new SqlParameter("@BENo", SqlDbType.VarChar);
                    pBENo.Value = txtBENo.Text.Trim();

                    SqlParameter pBEDate = new SqlParameter("@BEDate", SqlDbType.VarChar);
                    pBEDate.Value = txtBEDate.Text.Trim();

                    SqlParameter pBEdoc = new SqlParameter("@BEdoc", SqlDbType.VarChar);
                    pBEdoc.Value = txtBENoDoc.Text.Trim();

                    SqlParameter pbankLine = new SqlParameter("@bankLine", SqlDbType.VarChar);
                    string _bankLine = "N";
                    if (chkBankLineTransfer.Checked)
                        _bankLine = "Y";
                    pbankLine.Value = _bankLine;

                    SqlParameter pAccpDate = new SqlParameter("@AccpDate", SqlDbType.VarChar);
                    pAccpDate.Value = txtAccpDate.Text.Trim();

                    SqlParameter pInvoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                    pInvoiceNo.Value = txtInvoiceNo.Text.Trim();


                    SqlParameter pInvoiceDate = new SqlParameter("@invoiceDate", SqlDbType.VarChar);
                    pInvoiceDate.Value = txtInvoiceDate.Text.Trim();


                    SqlParameter pInvoiceDoc = new SqlParameter("@InvoiceDoc", SqlDbType.VarChar);
                    pInvoiceDoc.Value = txtInvoiceDoc.Text.Trim();

                    SqlParameter pAWBno = new SqlParameter("@AWBno", SqlDbType.VarChar);
                    pAWBno.Value = txtAWBno.Text.Trim();

                    SqlParameter pAWBdate = new SqlParameter("@AWBdate", SqlDbType.VarChar);
                    pAWBdate.Value = txtAWBDate.Text.Trim();

                    SqlParameter pAWBissuedBy = new SqlParameter("@AWBissuedBy", SqlDbType.VarChar);
                    pAWBissuedBy.Value = txtAwbIssuedBy.Text.Trim();

                    SqlParameter pAWBdoc = new SqlParameter("@AWBdoc", SqlDbType.VarChar);
                    pAWBdoc.Value = txtAWBDoc.Text.Trim();

                    SqlParameter pPackingList = new SqlParameter("@PackingList", SqlDbType.VarChar);
                    pPackingList.Value = txtPackingList.Text.Trim();

                    SqlParameter pPackingListDate = new SqlParameter("@PackingListDate", SqlDbType.VarChar);
                    pPackingListDate.Value = txtPackingListDate.Text.Trim();

                    SqlParameter pPackingListDoc = new SqlParameter("@PackingListDoc", SqlDbType.VarChar);
                    pPackingListDoc.Value = txtPackingDoc.Text.Trim();

                    SqlParameter pCertOfOrigin = new SqlParameter("@certOfOrigin", SqlDbType.VarChar);
                    pCertOfOrigin.Value = txtCertOfOrigin.Text.Trim();

                    SqlParameter pCertOfOriginIssuedBy = new SqlParameter("@certOfOriginIssuedBy", SqlDbType.VarChar);
                    pCertOfOriginIssuedBy.Value = txtCertIssuedBy.Text.Trim();

                    SqlParameter pCertOfOriginDoc = new SqlParameter("@certOfOriginDoc", SqlDbType.VarChar);
                    pCertOfOriginDoc.Value = txtCertOfOriginDoc.Text.Trim();

                    SqlParameter pCustomsInvoice = new SqlParameter("@CustomsInvoice", SqlDbType.VarChar);
                    pCustomsInvoice.Value = txtCustomsInvoice.Text.Trim();

                    SqlParameter pCustomsInvoiceDate = new SqlParameter("@CustomsInvoiveDate", SqlDbType.VarChar);
                    pCustomsInvoiceDate.Value = txtCustomsDate.Text.Trim();

                    SqlParameter pCustomsInvoiceDoc = new SqlParameter("@CustomsInvoiveDoc", SqlDbType.VarChar);
                    pCustomsInvoiceDoc.Value = txtCustomsDoc.Text.Trim();

                    SqlParameter pCommodity = new SqlParameter("@Commodity", SqlDbType.VarChar);
                    pCommodity.Value = txtCommodityID.Text.Trim();

                    SqlParameter pInsPolicy = new SqlParameter("@InsPolicy", SqlDbType.VarChar);
                    pInsPolicy.Value = txtInsPolicy.Text.Trim();

                    SqlParameter pInsPolicyDate = new SqlParameter("@InsPolicyDate", SqlDbType.VarChar);
                    pInsPolicyDate.Value = txtInsPolicyDate.Text.Trim();

                    SqlParameter pInsPolicyIssuedBy = new SqlParameter("@InsPolicyIssuedBy", SqlDbType.VarChar);
                    pInsPolicyIssuedBy.Value = txtInsPolicyIssuedBy.Text.Trim();

                    SqlParameter pInsPolicydoc = new SqlParameter("@InsPolicyDoc", SqlDbType.VarChar);
                    pInsPolicydoc.Value = txtInsPolicyDoc.Text.Trim();

                    SqlParameter pGSPdate = new SqlParameter("@GspDate", SqlDbType.VarChar);
                    pGSPdate.Value = txtGSPDate.Text.Trim();

                    SqlParameter pContractNo = new SqlParameter("@ContractNo", SqlDbType.VarChar);
                    pContractNo.Value = txtContractNo.Text.Trim();

                    SqlParameter pContractRate = new SqlParameter("@ContractRate", SqlDbType.VarChar);
                    pContractRate.Value = txtRate.Text.Trim();

                    SqlParameter pGSPdoc = new SqlParameter("@GspDoc", SqlDbType.VarChar);
                    pGSPdoc.Value = txtGSPDoc.Text.Trim();

                    SqlParameter pFIRCno = new SqlParameter("@FIRCno", SqlDbType.VarChar);
                    pFIRCno.Value = txtFIRCno.Text.Trim();

                    SqlParameter pFIRCdate = new SqlParameter("@FIRCDate", SqlDbType.VarChar);
                    pFIRCdate.Value = txtFIRCdate.Text.Trim();

                    SqlParameter pFIRCissuedBy = new SqlParameter("@FIRCIssuedBy", SqlDbType.VarChar);
                    pFIRCissuedBy.Value = txtFIRCnoIssuedBy.Text.Trim();

                    SqlParameter pFIRCdoc = new SqlParameter("@FIRCDoc", SqlDbType.VarChar);
                    pFIRCdoc.Value = txtFIRCdoc.Text.Trim();

                    SqlParameter pMisc = new SqlParameter("@Miscellaneous", SqlDbType.VarChar);
                    pMisc.Value = txtMiscellaneous.Text.Trim();

                    SqlParameter pMiscDoc = new SqlParameter("@MiscellaneousDoc", SqlDbType.VarChar);
                    pMiscDoc.Value = txtMiscDoc.Text.Trim();

                    SqlParameter pShipment = new SqlParameter("@Steamer", SqlDbType.VarChar);
                    string _Shipment = "";
                    if (rdbByAir.Checked)
                        _Shipment = "A";
                    else if (rdbBySea.Checked)
                        _Shipment = "S";
                    else if (rdbByRoad.Checked)
                        _Shipment = "R";
                    pShipment.Value = _Shipment;

                    //SqlParameter pCoveringFrom = new SqlParameter("@CoveringFrom", SqlDbType.VarChar);
                    //pCoveringFrom.Value = txtCoveringFrom.Text.Trim();

                    SqlParameter pCoveringTo = new SqlParameter("@CoveringTo", SqlDbType.VarChar);
                    pCoveringTo.Value = txtCoveringTo.Text.Trim();

                    SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);

                    pCountryCode.Value = txtCountry.Text.Trim();

                    SqlParameter preimbValDate = new SqlParameter("@reimbValDate", SqlDbType.VarChar);
                    preimbValDate.Value = txtReimbValDate.Text.Trim();

                    SqlParameter preimbClaimDate = new SqlParameter("@reimbClaimDate", SqlDbType.VarChar);
                    preimbClaimDate.Value = txtReimbClaimDate.Text.Trim();


                    SqlParameter preimbBankName = new SqlParameter("@reimbBankName", SqlDbType.VarChar);
                    preimbBankName.Value = txtBkName.Text.Trim();

                    SqlParameter preimbBankBICcode = new SqlParameter("@reimbBankBICcode", SqlDbType.VarChar);
                    preimbBankBICcode.Value = txtBIC.Text.Trim();

                    SqlParameter preimbAmount = new SqlParameter("@reimbAmount", SqlDbType.VarChar);
                    preimbAmount.Value = txtReimbAmt.Text.Trim();

                    //SqlParameter pttrefNo = new SqlParameter("@ttrefNo", SqlDbType.VarChar);
                    //if (txtTTdocNo.Text != "")
                    //{
                    //    pttrefNo.Value = txtTTprFx.Text + "/" + txtTTdocNo.Text.Trim() + "/" + txtTTbranchCode.Text.Trim() + "/" + txtTTyear.Text.Trim();
                    //}
                    //else
                    //    pttrefNo.Value = "";

                    SqlParameter pNotes = new SqlParameter("@notes", SqlDbType.VarChar);
                    pNotes.Value = txtNotes.Text.Trim();

                    SqlParameter pCurr = new SqlParameter("@Curr", SqlDbType.VarChar);
                    if (ddlCurrency.SelectedIndex > 0)
                        pCurr.Value = ddlCurrency.SelectedValue.Trim();
                    else
                        pCurr.Value = "";

                    SqlParameter pLoanAdv = new SqlParameter("@loanAdv", SqlDbType.VarChar);
                    string _loanAdv = "N";
                    if (chkLoanAdv.Checked)
                        _loanAdv = "Y";
                    pLoanAdv.Value = _loanAdv;

                    SqlParameter pOthCurr = new SqlParameter("@OthCurr", SqlDbType.VarChar);
                    if (ddlCurrency.Text == "INR")
                    {
                        if (ddlOtherCurrency.SelectedIndex > 0)
                            pOthCurr.Value = ddlOtherCurrency.SelectedValue.Trim();
                        else
                            pOthCurr.Value = "";
                    }
                    else
                        pOthCurr.Value = pCurr.Value;

                    SqlParameter pExchRT = new SqlParameter("@exchRt", SqlDbType.VarChar);
                    pExchRT.Value = txtExchRate.Text.Trim();

                    SqlParameter pNoOfDays = new SqlParameter("@noOfDays", SqlDbType.VarChar);
                    pNoOfDays.Value = txtNoOfDays.Text.Trim();

                    SqlParameter pAfterFrom = new SqlParameter("@afterFrom", SqlDbType.VarChar);
                    string _AfterFrom = "";
                    if (rbtnAfterAWB.Checked == true)
                        _AfterFrom = "A";
                    if (rbtnFromAWB.Checked == true)
                        _AfterFrom = "F";
                    if (rbtnSight.Checked == true)
                        _AfterFrom = "X";
                    if (rbtnDA.Checked == true)
                        _AfterFrom = "Y";
                    if (rbtnFromInvoice.Checked == true)
                        _AfterFrom = "I";
                    if (rbtnOthers.Checked == true)
                        _AfterFrom = "O";
                    pAfterFrom.Value = _AfterFrom;

                    SqlParameter pLibor = new SqlParameter("@libor", SqlDbType.VarChar);
                    pLibor.Value = txtLibor.Text.Trim();

                    SqlParameter pOutOfDays = new SqlParameter("@outOfDays", SqlDbType.VarChar);
                    pOutOfDays.Value = txtOutOfDays.Text.Trim();

                    SqlParameter pDueDate = new SqlParameter("@DueDate", SqlDbType.VarChar);
                    pDueDate.Value = txtDueDate.Text.Trim();

                    SqlParameter pAccpDueDate = new SqlParameter("@AccpDueDate", SqlDbType.VarChar);
                    pAccpDueDate.Value = txtAcceptedDueDate.Text.Trim();

                    SqlParameter pOtherRemarks = new SqlParameter("@otherRemarks", SqlDbType.VarChar);
                    pOtherRemarks.Value = txtOtherTenorRemarks.Text.Trim();

                    SqlParameter pBillAmt = new SqlParameter("@ActbillAmt", SqlDbType.VarChar);
                    pBillAmt.Value = txtBillAmount.Text.Trim();

                    SqlParameter pBillAmtinRS = new SqlParameter("@ActbillAmtinRS", SqlDbType.VarChar);
                    pBillAmtinRS.Value = txtBillAmountinRS.Text.Trim();

                    SqlParameter pNegotiated = new SqlParameter("@NegotiatedAmt", SqlDbType.VarChar);
                    pNegotiated.Value = txtNegotiatedAmt.Text.Trim();

                    SqlParameter pNegotiatedinRS = new SqlParameter("@NegotiatedAmtinRS", SqlDbType.VarChar);
                    pNegotiatedinRS.Value = txtNegotiatedAmtinRS.Text.Trim();

                    SqlParameter pInterest = new SqlParameter("@interest", SqlDbType.VarChar);
                    pInterest.Value = txtInterest.Text.Trim();

                    SqlParameter pInterestinRS = new SqlParameter("@interestinRS", SqlDbType.VarChar);
                    pInterestinRS.Value = txtInterestinRS.Text.Trim();

                    SqlParameter pNetAmt = new SqlParameter("@NetAmt", SqlDbType.VarChar);
                    pNetAmt.Value = txtNetAmt.Text.Trim();

                    SqlParameter pNetAmtinRS = new SqlParameter("@NetAmtinRS", SqlDbType.VarChar);
                    pNetAmtinRS.Value = txtNetAmtinRS.Text.Trim();

                    SqlParameter pExchRtEBR = new SqlParameter("@exchRtEBR", SqlDbType.VarChar);
                    pExchRtEBR.Value = txtExchRtEBR.Text.Trim();

                    SqlParameter pOtherCharges = new SqlParameter("@otherChrgs", SqlDbType.VarChar);
                    pOtherCharges.Value = txtOtherChrgs.Text.Trim();

                    SqlParameter pSTFXDLS = new SqlParameter("@STFXDLS", SqlDbType.VarChar);
                    pSTFXDLS.Value = txtSTFXDLS.Text.Trim();

                    SqlParameter pBankCert = new SqlParameter("@bankCert", SqlDbType.VarChar);
                    pBankCert.Value = txtBankCert.Text.Trim();

                    SqlParameter pNegotiationFees = new SqlParameter("@negotiationFees", SqlDbType.VarChar);
                    pNegotiationFees.Value = txtNegotiationFees.Text.Trim();

                    SqlParameter pCourierCharges = new SqlParameter("@CourierChrgs", SqlDbType.VarChar);
                    pCourierCharges.Value = txtCourierChrgs.Text.Trim();

                    SqlParameter pMarginAmount = new SqlParameter("@MarginAmt", SqlDbType.VarChar);
                    pMarginAmount.Value = txtMarginAmt.Text.Trim();

                    SqlParameter pCommission = new SqlParameter("@Commission", SqlDbType.VarChar);
                    pCommission.Value = txtCommission.Text.Trim();

                    SqlParameter psTax = new SqlParameter("@sTax", SqlDbType.VarChar);
                    psTax.Value = ddlServiceTax.Text.Trim();

                    SqlParameter psAmt = new SqlParameter("@sTaxAmt", SqlDbType.VarChar);
                    psAmt.Value = txtSTaxAmount.Text.Trim();

                    SqlParameter pCurrentAcinRS = new SqlParameter("@CurrentAcinRS", SqlDbType.VarChar);
                    pCurrentAcinRS.Value = txtCurrentAcinRS.Text.Trim();

                    SqlParameter pcb1 = new SqlParameter("@cb1", SqlDbType.VarChar);
                    string bit_chk1 = "0";
                    if (chk1.Checked)
                        bit_chk1 = "1";
                    pcb1.Value = bit_chk1;

                    SqlParameter pcb2 = new SqlParameter("@cb2", SqlDbType.VarChar);
                    string bit_chk2 = "0";
                    if (chk2.Checked)
                        bit_chk2 = "1";
                    pcb2.Value = bit_chk2;

                    SqlParameter pcb3 = new SqlParameter("@cb3", SqlDbType.VarChar);
                    string bit_chk3 = "0";
                    if (chk3.Checked)
                        bit_chk3 = "1";
                    pcb3.Value = bit_chk3;

                    SqlParameter pcb4 = new SqlParameter("@cb4", SqlDbType.VarChar);
                    string bit_chk4 = "0";
                    if (chk4.Checked)
                        bit_chk4 = "1";
                    pcb4.Value = bit_chk4;

                    SqlParameter pcb5 = new SqlParameter("@cb5", SqlDbType.VarChar);
                    string bit_chk5 = "0";
                    if (chk5.Checked)
                        bit_chk5 = "1";
                    pcb5.Value = bit_chk5;

                    SqlParameter pcb6 = new SqlParameter("@cb6", SqlDbType.VarChar);
                    string bit_chk6 = "0";
                    if (chk6.Checked)
                        bit_chk6 = "1";
                    pcb6.Value = bit_chk6;

                    SqlParameter pcb7 = new SqlParameter("@cb7", SqlDbType.VarChar);
                    string bit_chk7 = "0";
                    if (chk7.Checked)
                        bit_chk7 = "1";
                    pcb7.Value = bit_chk7;

                    SqlParameter pcb7A = new SqlParameter("@cb7A", SqlDbType.VarChar);
                    string bit_chk7A = "0";
                    if (chk7A.Checked)
                        bit_chk7A = "1";
                    pcb7A.Value = bit_chk7A;

                    SqlParameter pcb7B = new SqlParameter("@cb7B", SqlDbType.VarChar);
                    string bit_chk7B = "0";
                    if (chk7B.Checked)
                        bit_chk7B = "1";
                    pcb7B.Value = bit_chk7B;

                    SqlParameter pcb8 = new SqlParameter("@cb8", SqlDbType.VarChar);
                    string bit_chk8 = "0";
                    if (chk8.Checked)
                        bit_chk8 = "1";
                    pcb8.Value = bit_chk8;

                    SqlParameter pcb9 = new SqlParameter("@cb9", SqlDbType.VarChar);
                    string bit_chk9 = "0";
                    if (chk9.Checked)
                        bit_chk9 = "1";
                    pcb9.Value = bit_chk9;

                    SqlParameter pcb10 = new SqlParameter("@cb10", SqlDbType.VarChar);
                    string bit_chk10 = "0";
                    if (chk10.Checked)
                        bit_chk10 = "1";
                    pcb10.Value = bit_chk10;

                    SqlParameter pcb11 = new SqlParameter("@cb11", SqlDbType.VarChar);
                    string bit_chk11 = "0";
                    if (chk11.Checked)
                        bit_chk11 = "1";
                    pcb11.Value = bit_chk11;

                    SqlParameter pcb12 = new SqlParameter("@cb12", SqlDbType.VarChar);
                    string bit_chk12 = "0";
                    if (chk12.Checked)
                        bit_chk12 = "1";
                    pcb12.Value = bit_chk12;

                    SqlParameter pcb13 = new SqlParameter("@cb13", SqlDbType.VarChar);
                    string bit_chk13 = "0";
                    if (chk13.Checked)
                        bit_chk13 = "1";
                    pcb13.Value = bit_chk13;

                    SqlParameter pRemarks = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    pRemarks.Value = txtRemark.Text.Trim();

                    SqlParameter pRemarks1 = new SqlParameter("@Remarks1", SqlDbType.VarChar);
                    pRemarks1.Value = txtRemarks1.Text.Trim();

                    SqlParameter pIsManualGR = new SqlParameter("@IsManualGR", SqlDbType.VarChar);
                    string bit_IsManualGR = "0";
                    if (chkManualGR.Checked)
                        bit_IsManualGR = "1";
                    pIsManualGR.Value = bit_IsManualGR;

                    SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
                    pUser.Value = _userName;

                    SqlParameter pUploadDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
                    pUploadDate.Value = _uploadingDate;

                    SqlParameter pchkRBI = new SqlParameter("@chkRBI", SqlDbType.VarChar);
                    string _chkRBI = "False";
                    if (chkRBI.Checked)
                        _chkRBI = "True";
                    pchkRBI.Value = _chkRBI;

                    SqlParameter pTTRef1 = new SqlParameter("@TTRef1", txtTTRefNo1.Text.Trim());
                    SqlParameter pTTAmt1 = new SqlParameter("@TTAmt1", txtTTAmount1.Text.Trim());

                    SqlParameter pTTRef2 = new SqlParameter("@TTRef2", txtTTRefNo2.Text.Trim());
                    SqlParameter pTTAmt2 = new SqlParameter("@TTAmt2", txtTTAmount2.Text.Trim());

                    SqlParameter pTTRef3 = new SqlParameter("@TTRef3", txtTTRefNo3.Text.Trim());
                    SqlParameter pTTAmt3 = new SqlParameter("@TTAmt3", txtTTAmount3.Text.Trim());

                    SqlParameter pTTRef4 = new SqlParameter("@TTRef4", txtTTRefNo4.Text.Trim());
                    SqlParameter pTTAmt4 = new SqlParameter("@TTAmt4", txtTTAmount4.Text.Trim());

                    SqlParameter pTTRef5 = new SqlParameter("@TTRef5", txtTTRefNo5.Text.Trim());
                    SqlParameter pTTAmt5 = new SqlParameter("@TTAmt5", txtTTAmount5.Text.Trim());

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

                    SqlParameter pIntRate1 = new SqlParameter("@interestRate1", SqlDbType.VarChar);
                    pIntRate1.Value = txtIntRate1.Text.Trim();

                    SqlParameter pForDays1 = new SqlParameter("@forDays1", SqlDbType.VarChar);
                    pForDays1.Value = txtForDays1.Text.Trim();

                    SqlParameter pIntRate2 = new SqlParameter("@interestRate2", SqlDbType.VarChar);
                    pIntRate2.Value = txtIntRate2.Text.Trim();

                    SqlParameter pForDays2 = new SqlParameter("@forDays2", SqlDbType.VarChar);
                    pForDays2.Value = txtForDays2.Text.Trim();

                    SqlParameter pIntRate3 = new SqlParameter("@interestRate3", txtIntRate3.Text.Trim());
                    SqlParameter pForDays3 = new SqlParameter("@forDays3", txtForDays3.Text.Trim());

                    SqlParameter pIntRate4 = new SqlParameter("@interestRate4", txtIntRate4.Text.Trim());
                    SqlParameter pForDays4 = new SqlParameter("@forDays4", txtForDays4.Text.Trim());

                    SqlParameter pIntRate5 = new SqlParameter("@interestRate5", txtIntRate5.Text.Trim());
                    SqlParameter pForDays5 = new SqlParameter("@forDays5", txtForDays5.Text.Trim());

                    SqlParameter pIntRate6 = new SqlParameter("@interestRate6", txtIntRate6.Text.Trim());
                    SqlParameter pForDays6 = new SqlParameter("@forDays6", txtForDays6.Text.Trim());

                    SqlParameter pIntFDate1 = new SqlParameter("@intFDate1", txtIntFrmDate1.Text.Trim());
                    SqlParameter pIntTDate1 = new SqlParameter("@intTDate1", txtIntToDate1.Text.Trim());

                    SqlParameter pIntFDate2 = new SqlParameter("@intFDate2", txtIntFrmDate2.Text.Trim());
                    SqlParameter pIntTDate2 = new SqlParameter("@intTDate2", txtIntToDate2.Text.Trim());

                    SqlParameter pIntFDate3 = new SqlParameter("@intFDate3", txtIntFrmDate3.Text.Trim());
                    SqlParameter pIntTDate3 = new SqlParameter("@intTDate3", txtIntToDate3.Text.Trim());

                    SqlParameter pIntFDate4 = new SqlParameter("@intFDate4", txtIntFrmDate4.Text.Trim());
                    SqlParameter pIntTDate4 = new SqlParameter("@intTDate4", txtIntToDate4.Text.Trim());

                    SqlParameter pIntFDate5 = new SqlParameter("@intFDate5", txtIntFrmDate5.Text.Trim());
                    SqlParameter pIntTDate5 = new SqlParameter("@intTDate5", txtIntToDate5.Text.Trim());

                    SqlParameter pIntFDate6 = new SqlParameter("@intFDate6", txtIntFrmDate6.Text.Trim());
                    SqlParameter pIntTDate6 = new SqlParameter("@intTDate6", txtIntToDate6.Text.Trim());

                    SqlParameter pCommissionID = new SqlParameter("@CommissionID", txtCommissionID.Text.Trim());

                    SqlParameter pFIRCNo1_OB = new SqlParameter("@FIRCNo1_OB", txtFIRCNo1_OB.Text.Trim());
                    SqlParameter pFIRCDate1_OB = new SqlParameter("@FIRCDate1_OB", txtFIRCDate1_OB.Text.Trim());
                    SqlParameter pFIRCAmt1_OB = new SqlParameter("@FIRCAmt1_OB", txtFIRCAmount1_OB.Text.Trim());
                    SqlParameter pFIRCADCode1_OB = new SqlParameter("@FIRCADCode1_OB", txtFIRCADCode1_OB.Text.Trim());

                    SqlParameter pFIRCNo2_OB = new SqlParameter("@FIRCNo2_OB", txtFIRCNo2_OB.Text.Trim());
                    SqlParameter pFIRCDate2_OB = new SqlParameter("@FIRCDate2_OB", txtFIRCDate2_OB.Text.Trim());
                    SqlParameter pFIRCAmt2_OB = new SqlParameter("@FIRCAmt2_OB", txtFIRCAmount2_OB.Text.Trim());
                    SqlParameter pFIRCADCode2_OB = new SqlParameter("@FIRCADCode2_OB", txtFIRCADCode2_OB.Text.Trim());

                    SqlParameter pFIRCNo3_OB = new SqlParameter("@FIRCNo3_OB", txtFIRCNo3_OB.Text.Trim());
                    SqlParameter pFIRCDate3_OB = new SqlParameter("@FIRCDate3_OB", txtFIRCDate3_OB.Text.Trim());
                    SqlParameter pFIRCAmt3_OB = new SqlParameter("@FIRCAmt3_OB", txtFIRCAmount3_OB.Text.Trim());
                    SqlParameter pFIRCADCode3_OB = new SqlParameter("@FIRCADCode3_OB", txtFIRCADCode3_OB.Text.Trim());

                    SqlParameter pFIRCNo4_OB = new SqlParameter("@FIRCNo4_OB", txtFIRCNo4_OB.Text.Trim());
                    SqlParameter pFIRCDate4_OB = new SqlParameter("@FIRCDate4_OB", txtFIRCDate4_OB.Text.Trim());
                    SqlParameter pFIRCAmt4_OB = new SqlParameter("@FIRCAmt4_OB", txtFIRCAmount4_OB.Text.Trim());
                    SqlParameter pFIRCADCode4_OB = new SqlParameter("@FIRCADCode4_OB", txtFIRCADCode4_OB.Text.Trim());


                    SqlParameter pFIRCNo5_OB = new SqlParameter("@FIRCNo5_OB", txtFIRCNo5_OB.Text.Trim());
                    SqlParameter pFIRCDate5_OB = new SqlParameter("@FIRCDate5_OB", txtFIRCDate5_OB.Text.Trim());
                    SqlParameter pFIRCAmt5_OB = new SqlParameter("@FIRCAmt5_OB", txtFIRCAmount5_OB.Text.Trim());
                    SqlParameter pFIRCADCode5_OB = new SqlParameter("@FIRCADCode5_OB", txtFIRCADCode5_OB.Text.Trim());


                    SqlParameter pFIRCNo6_OB = new SqlParameter("@FIRCNo6_OB", txtFIRCNo6_OB.Text.Trim());
                    SqlParameter pFIRCDate6_OB = new SqlParameter("@FIRCDate6_OB", txtFIRCDate6_OB.Text.Trim());
                    SqlParameter pFIRCAmt6_OB = new SqlParameter("@FIRCAmt6_OB", txtFIRCAmount6_OB.Text.Trim());
                    SqlParameter pFIRCADCode6_OB = new SqlParameter("@FIRCADCode6_OB", txtFIRCADCode6_OB.Text.Trim());


                    SqlParameter pFIRCNo7_OB = new SqlParameter("@FIRCNo7_OB", txtFIRCNo7_OB.Text.Trim());
                    SqlParameter pFIRCDate7_OB = new SqlParameter("@FIRCDate7_OB", txtFIRCDate7_OB.Text.Trim());
                    SqlParameter pFIRCAmt7_OB = new SqlParameter("@FIRCAmt7_OB", txtFIRCAmount7_OB.Text.Trim());
                    SqlParameter pFIRCADCode7_OB = new SqlParameter("@FIRCADCode7_OB", txtFIRCADCode7_OB.Text.Trim());


                    SqlParameter pFIRCNo8_OB = new SqlParameter("@FIRCNo8_OB", txtFIRCNo8_OB.Text.Trim());
                    SqlParameter pFIRCDate8_OB = new SqlParameter("@FIRCDate8_OB", txtFIRCDate8_OB.Text.Trim());
                    SqlParameter pFIRCAmt8_OB = new SqlParameter("@FIRCAmt8_OB", txtFIRCAmount8_OB.Text.Trim());
                    SqlParameter pFIRCADCode8_OB = new SqlParameter("@FIRCADCode8_OB", txtFIRCADCode8_OB.Text.Trim());

                    SqlParameter pFIRCNo9_OB = new SqlParameter("@FIRCNo9_OB", txtFIRCNo9_OB.Text.Trim());
                    SqlParameter pFIRCDate9_OB = new SqlParameter("@FIRCDate9_OB", txtFIRCDate9_OB.Text.Trim());
                    SqlParameter pFIRCAmt9_OB = new SqlParameter("@FIRCAmt9_OB", txtFIRCAmount9_OB.Text.Trim());
                    SqlParameter pFIRCADCode9_OB = new SqlParameter("@FIRCADCode9_OB", txtFIRCADCode9_OB.Text.Trim());

                    SqlParameter pFIRCNo10_OB = new SqlParameter("@FIRCNo10_OB", txtFIRCNo10_OB.Text.Trim());
                    SqlParameter pFIRCDate10_OB = new SqlParameter("@FIRCDate10_OB", txtFIRCDate10_OB.Text.Trim());
                    SqlParameter pFIRCAmt10_OB = new SqlParameter("@FIRCAmt10_OB", txtFIRCAmount10_OB.Text.Trim());
                    SqlParameter pFIRCADCode10_OB = new SqlParameter("@FIRCADCode10_OB", txtFIRCADCode10_OB.Text.Trim());

                    SqlParameter pFIRCNo11_OB = new SqlParameter("@FIRCNo11_OB", txtFIRCNo11_OB.Text.Trim());
                    SqlParameter pFIRCDate11_OB = new SqlParameter("@FIRCDate11_OB", txtFIRCDate11_OB.Text.Trim());
                    SqlParameter pFIRCAmt11_OB = new SqlParameter("@FIRCAmt11_OB", txtFIRCAmount11_OB.Text.Trim());
                    SqlParameter pFIRCADCode11_OB = new SqlParameter("@FIRCADCode11_OB", txtFIRCADCode11_OB.Text.Trim());

                    SqlParameter pFIRCNo12_OB = new SqlParameter("@FIRCNo12_OB", txtFIRCNo12_OB.Text.Trim());
                    SqlParameter pFIRCDate12_OB = new SqlParameter("@FIRCDate12_OB", txtFIRCDate12_OB.Text.Trim());
                    SqlParameter pFIRCAmt12_OB = new SqlParameter("@FIRCAmt12_OB", txtFIRCAmount12_OB.Text.Trim());
                    SqlParameter pFIRCADCode12_OB = new SqlParameter("@FIRCADCode12_OB", txtFIRCADCode12_OB.Text.Trim());


                    SqlParameter pFIRCNo13_OB = new SqlParameter("@FIRCNo13_OB", txtFIRCNo13_OB.Text.Trim());
                    SqlParameter pFIRCDate13_OB = new SqlParameter("@FIRCDate13_OB", txtFIRCDate13_OB.Text.Trim());
                    SqlParameter pFIRCAmt13_OB = new SqlParameter("@FIRCAmt13_OB", txtFIRCAmount13_OB.Text.Trim());
                    SqlParameter pFIRCADCode13_OB = new SqlParameter("@FIRCADCode13_OB", txtFIRCADCode13_OB.Text.Trim());

                    SqlParameter pFIRCNo14_OB = new SqlParameter("@FIRCNo14_OB", txtFIRCNo14_OB.Text.Trim());
                    SqlParameter pFIRCDate14_OB = new SqlParameter("@FIRCDate14_OB", txtFIRCDate14_OB.Text.Trim());
                    SqlParameter pFIRCAmt14_OB = new SqlParameter("@FIRCAmt14_OB", txtFIRCAmount14_OB.Text.Trim());
                    SqlParameter pFIRCADCode14_OB = new SqlParameter("@FIRCADCode14_OB", txtFIRCADCode14_OB.Text.Trim());

                    SqlParameter pFIRCNo15_OB = new SqlParameter("@FIRCNo15_OB", txtFIRCNo15_OB.Text.Trim());
                    SqlParameter pFIRCDate15_OB = new SqlParameter("@FIRCDate15_OB", txtFIRCDate15_OB.Text.Trim());
                    SqlParameter pFIRCAmt15_OB = new SqlParameter("@FIRCAmt15_OB", txtFIRCAmount15_OB.Text.Trim());
                    SqlParameter pFIRCADCode15_OB = new SqlParameter("@FIRCADCode15_OB", txtFIRCADCode15_OB.Text.Trim());



                    SqlParameter padCode = new SqlParameter("@AdCode", txtADCode.Text.Trim());
                    SqlParameter pForeignLocal = new SqlParameter("@ForeignOrLocal", _ForeignOrLocal);



                    //Swach Bharat
                    SqlParameter SBcess = new SqlParameter("@sbcess", txtsbcess.Text);
                    SqlParameter SBcessamt = new SqlParameter("@sbcessamt", txtSBcesssamt.Text);
                    SqlParameter kkcess = new SqlParameter("@kkcess", txtsbcess.Text);
                    SqlParameter kkcessamt = new SqlParameter("@kkcessamt", txtSBcesssamt.Text);
                    SqlParameter STTamt = new SqlParameter("@totstax", txtsttamt.Text);

                    //Fxdls
                    SqlParameter SBFxDls = new SqlParameter("@sbcessonfxdls", txtsbfx.Text);
                    SqlParameter kkcessfxdls = new SqlParameter("@kkcessonfxdls", txt_kkcessonfx.Text);
                    SqlParameter totalSBFxDls = new SqlParameter("@totsbcessonfxdls", txttotsbcess.Text);

                    //fbk

                    SqlParameter fbkchrg = new SqlParameter("@fbkcharges", txt_fbkcharges.Text);
                    SqlParameter staxfbk = new SqlParameter("@staxfbk", ddlServiceTaxfbk.Text);
                    SqlParameter staxfbkamt = new SqlParameter("@staxfbkamt", txtSTaxAmountfbk.Text);
                    SqlParameter sbcessfbk = new SqlParameter("@sbcessfbk", txtsbcessfbk.Text);
                    SqlParameter sbcessamtfbk = new SqlParameter("@sbcessamtfbk", txtSBcesssamtfbk.Text);
                    SqlParameter kkcessfbk = new SqlParameter("@kkcessfbk", txt_kkcessperfbk.Text);
                    SqlParameter kkcessfbkamt = new SqlParameter("@kkcessfbkamt", txt_kkcessamtfbk.Text);
                    SqlParameter totfbkamt = new SqlParameter("@totalfbkamt", txtsttamtfbk.Text);

                    //GBASE FIELDS

                    SqlParameter DD_Doc1 = new SqlParameter("@DD_Doc1", txtBENoDoc1.Text.Trim());
                    SqlParameter Invoice_Doc1 = new SqlParameter("@Invoice_Doc1", txtInvoiceDoc1.Text.Trim());
                    SqlParameter Customs_Invoice_Doc1 = new SqlParameter("@Customs_Invoice_Doc1", txtCustomsDoc1.Text.Trim());
                    SqlParameter Ins_Policy_Doc1 = new SqlParameter("@Ins_Policy_Doc1", txtInsPolicyDoc1.Text.Trim());
                    SqlParameter Cert_Of_Origin_Doc1 = new SqlParameter("@Cert_Of_Origin_Doc1", txtCertOfOriginDoc1.Text.Trim());
                    SqlParameter Packing_List_Doc1 = new SqlParameter("@Packing_List_Doc1", txtPackingDoc1.Text.Trim());
                    SqlParameter WM_Doc = new SqlParameter("@WM_Doc", txtWM.Text.Trim());
                    SqlParameter WM_Doc1 = new SqlParameter("@WM_Doc1", txtWM1.Text.Trim());
                    SqlParameter INSP_Doc = new SqlParameter("@INSP_Doc", txtINSP.Text.Trim());
                    SqlParameter INSP_Doc1 = new SqlParameter("@INSP_Doc1", txtINSP1.Text.Trim());
                    SqlParameter AWB_Doc1 = new SqlParameter("@AWB_Doc1", txtAWBDoc1.Text.Trim());
                    SqlParameter OTHER_Doc = new SqlParameter("@OTHER_Doc", txtOther.Text.Trim());
                    SqlParameter OTHER_Doc1 = new SqlParameter("@OTHER_Doc1", txtOther1.Text.Trim());
                    SqlParameter GBase_Commodity_ID = new SqlParameter("@GBase_Commodity_ID", txtGBaseCommodityID.Text.Trim());
                    SqlParameter Operation_Type = new SqlParameter("@Operation_Type", txtOperationType.Text.Trim());
                    SqlParameter Settlement_Option = new SqlParameter("@Settlement_Option", txtSettlementOption.Text.Trim());
                    SqlParameter Risk_Country = new SqlParameter("@Risk_Country", txtRiskCountry.Text.Trim());

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
                    SqlParameter Fund_Type = new SqlParameter("@Fund_Type", txtFundType.Text.Trim());
                    SqlParameter Base_Rate_Code = new SqlParameter("@Base_Rate_Code", txtBaseRate.Text.Trim());
                    SqlParameter Grade_Code = new SqlParameter("@Grade_Code", txtGradeCode.Text.Trim());

                    SqlParameter Direction = new SqlParameter("@Direction", txtDirection.Text.Trim());
                    SqlParameter Covr_Instruction = new SqlParameter("@Covr_Instruction", txtCovrInstr.Text.Trim());
                    SqlParameter Internal_Rate = new SqlParameter("@Internal_Rate", txtInternalRate.Text.Trim());
                    SqlParameter Spread_Rate = new SqlParameter("@Spread_Rate", txtSpread.Text.Trim());
                    SqlParameter Application_No = new SqlParameter("@Application_No", txtApplNo.Text.Trim());
                    SqlParameter Remarks_EUC = new SqlParameter("@Remarks_EUC", ddlRemEUC.SelectedValue.Trim());
                    SqlParameter Draft_No = new SqlParameter("@Draft_No", txtDraftNo.Text.Trim());
                    SqlParameter Risk_Customer = new SqlParameter("@Risk_Customer", txtRiskCustomer.Text.Trim());
                    SqlParameter Vessel_Name = new SqlParameter("@Vessel_Name", txtVesselName.Text.Trim());
                    SqlParameter Instructions = new SqlParameter("@Instructions", ddlInstructions.SelectedValue.Trim());
                    SqlParameter Reimbursing_Bank = new SqlParameter("@Reimbursing_Bank", txtReimbBank.Text.Trim());
                    SqlParameter GBaseRemarks = new SqlParameter("@GBaseRemarks", txtGBaseRemarks.Text.Trim());
                    SqlParameter Merchandise = new SqlParameter("@Merchandise", txtMerchandise.Text.Trim());
                    SqlParameter Special_Instructions1 = new SqlParameter("@Special_Instructions1", txtSpecialInstructions1.Text.Trim());
                    SqlParameter Special_Instructions2 = new SqlParameter("@Special_Instructions2", txtSpecialInstructions2.Text.Trim());
                    SqlParameter Special_Instructions3 = new SqlParameter("@Special_Instructions3", txtSpecialInstructions3.Text.Trim());
                    SqlParameter Special_Instructions4 = new SqlParameter("@Special_Instructions4", txtSpecialInstructions4.Text.Trim());
                    SqlParameter Special_Instructions5 = new SqlParameter("@Special_Instructions5", txtSpecialInstructions5.Text.Trim());
                    SqlParameter Special_Instructions6 = new SqlParameter("@Special_Instructions6", txtSpecialInstructions6.Text.Trim());
                    SqlParameter Special_Instructions7 = new SqlParameter("@Special_Instructions7", txtSpecialInstructions7.Text.Trim());
                    SqlParameter Special_Instructions8 = new SqlParameter("@Special_Instructions8", txtSpecialInstructions8.Text.Trim());
                    SqlParameter Special_Instructions9 = new SqlParameter("@Special_Instructions9", txtSpecialInstructions9.Text.Trim());
                    SqlParameter Special_Instructions10 = new SqlParameter("@Special_Instructions10", txtSpecialInstructions10.Text.Trim());
                    SqlParameter PrincipalContractNo1 = new SqlParameter("@PrincipalContractNo1", txtPrincipalContractNo1.Text.Trim());
                    SqlParameter PrincipalContractNo2 = new SqlParameter("@PrincipalContractNo2", txtPrincipalContractNo2.Text.Trim());
                    SqlParameter PrincipalExchCurr = new SqlParameter("@PrincipalExchCurr", txtPrincipalExchCurr.Text.Trim());
                    SqlParameter PrincipalExchRate = new SqlParameter("@PrincipalExchRate", txtPrincipalExchRate.Text.Trim());
                    SqlParameter PrincipalIntExchRate = new SqlParameter("@PrincipalIntExchRate", txtPrincipalIntExchRate.Text.Trim());
                    SqlParameter InterestLump = new SqlParameter("@InterestLump", txtInterestLump.Text.Trim());
                    SqlParameter InterestContractNo1 = new SqlParameter("@InterestContractNo1", txtInterestContractNo1.Text.Trim());
                    SqlParameter InterestContractNo2 = new SqlParameter("@InterestContractNo2", txtInterestContractNo2.Text.Trim());
                    SqlParameter InterestExchCurr = new SqlParameter("@InterestExchCurr", txtInterestExchCurr.Text.Trim());
                    SqlParameter InterestExchRate = new SqlParameter("@InterestExchRate", txtInterestExchRate.Text.Trim());
                    SqlParameter InterestIntExchRate = new SqlParameter("@InterestIntExchRate", txtInterestIntExchRate.Text.Trim());
                    SqlParameter CommissionMatu = new SqlParameter("@CommissionMatu", txtCommissionMatu.Text.Trim());
                    SqlParameter CommissionContractNo1 = new SqlParameter("@CommissionContractNo1", txtCommissionContractNo1.Text.Trim());
                    SqlParameter CommissionContractNo2 = new SqlParameter("@CommissionContractNo2", txtCommissionContractNo2.Text.Trim());
                    SqlParameter CommissionExchCurr = new SqlParameter("@CommissionExchCurr", txtCommissionExchCurr.Text.Trim());
                    SqlParameter CommissionExchRate = new SqlParameter("@CommissionExchRate", txtCommissionExchRate.Text.Trim());
                    SqlParameter CommissionIntExchRate = new SqlParameter("@CommissionIntExchRate", txtCommissionIntExchRate.Text.Trim());
                    SqlParameter CRGLCode = new SqlParameter("@CRGLCode", txtCRGLCode.Text.Trim());
                    SqlParameter CRCustAbbr = new SqlParameter("@CRCustAbbr", txtCRCustAbbr.Text.Trim());
                    SqlParameter CRCustAcNo1 = new SqlParameter("@CRCustAcNo1", txtCRCustAcNo1.Text.Trim());
                    SqlParameter CRCustAcNo2 = new SqlParameter("@CRCustAcNo2", txtCRCustAcNo2.Text.Trim());
                    SqlParameter CRCurr = new SqlParameter("@CRCurr", txtCRCurr.Text.Trim());
                    SqlParameter CRAmount = new SqlParameter("@CRAmount", txtCRAmount.Text.Trim());
                    SqlParameter CRIntCurr = new SqlParameter("@CRIntCurr", txtCRIntCurr.Text.Trim());
                    SqlParameter CRIntAmount = new SqlParameter("@CRIntAmount", txtCRIntAmount.Text.Trim());
                    SqlParameter CRIntPayer = new SqlParameter("@CRIntPayer", txtCRIntPayer.Text.Trim());
                    SqlParameter CRPaymentCommCurr = new SqlParameter("@CRPaymentCommCurr", txtCRPaymentCommCurr.Text.Trim());
                    SqlParameter CRPaymentCommAmount = new SqlParameter("@CRPaymentCommAmount", txtCRPaymentCommAmount.Text.Trim());
                    SqlParameter CRPaymentCommPayer = new SqlParameter("@CRPaymentCommPayer", txtCRPaymentCommPayer.Text.Trim());
                    SqlParameter CRHandlingCommCurr = new SqlParameter("@CRHandlingCommCurr", txtCRHandlingCommCurr.Text.Trim());
                    SqlParameter CRHandlingCommAmount = new SqlParameter("@CRHandlingCommAmount", txtCRHandlingCommAmount.Text.Trim());
                    SqlParameter CRHandlingCommPayer = new SqlParameter("@CRHandlingCommPayer", txtCRHandlingCommPayer.Text.Trim());
                    SqlParameter CRPostageCurr = new SqlParameter("@CRPostageCurr", txtCRPostageCurr.Text.Trim());
                    SqlParameter CRPostageAmount = new SqlParameter("@CRPostageAmount", txtCRPostageAmount.Text.Trim());
                    SqlParameter CRPostagePayer = new SqlParameter("@CRPostagePayer", txtCRPostagePayer.Text.Trim());
                    SqlParameter CRCurr1 = new SqlParameter("@CRCurr1", txtCRCurr1.Text.Trim());
                    SqlParameter CRAmount1 = new SqlParameter("@CRAmount1", txtCRAmount1.Text.Trim());
                    SqlParameter CRPayer1 = new SqlParameter("@CRPayer1", txtCRPayer1.Text.Trim());
                    SqlParameter DRCurr = new SqlParameter("@DRCurr", txtDRCurr.Text.Trim());
                    SqlParameter DRAmount = new SqlParameter("@DRAmount", txtDRAmount.Text.Trim());
                    SqlParameter DRGLCode1 = new SqlParameter("@DRGLCode1", txtDRGLCode1.Text.Trim());
                    SqlParameter DRCustAbbr1 = new SqlParameter("@DRCustAbbr1", txtDRCustAbbr1.Text.Trim());
                    SqlParameter DRCustAcNo11 = new SqlParameter("@DRCustAcNo11", txtDRCustAcNo11.Text.Trim());
                    SqlParameter DRCustAcNo12 = new SqlParameter("@DRCustAcNo12", txtDRCustAcNo12.Text.Trim());
                    SqlParameter DRCurr1 = new SqlParameter("@DRCurr1", txtDRCurr1.Text.Trim());
                    SqlParameter DRAmount1 = new SqlParameter("@DRAmount1", txtDRAmount1.Text.Trim());
                    SqlParameter DRGLCode2 = new SqlParameter("@DRGLCode2", txtDRGLCode2.Text.Trim());
                    SqlParameter DRCustAbbr2 = new SqlParameter("@DRCustAbbr2", txtDRCustAbbr2.Text.Trim());
                    SqlParameter DRCustAcNo21 = new SqlParameter("@DRCustAcNo21", txtDRCustAcNo21.Text.Trim());
                    SqlParameter DRCustAcNo22 = new SqlParameter("@DRCustAcNo22", txtDRCustAcNo22.Text.Trim());
                    SqlParameter DRCurr2 = new SqlParameter("@DRCurr2", txtDRCurr2.Text.Trim());
                    SqlParameter DRAmount2 = new SqlParameter("@DRAmount2", txtDRAmount2.Text.Trim());
                    SqlParameter DRGLCode3 = new SqlParameter("@DRGLCode3", txtDRGLCode3.Text.Trim());
                    SqlParameter DRCustAbbr3 = new SqlParameter("@DRCustAbbr3", txtDRCustAbbr3.Text.Trim());
                    SqlParameter DRCustAcNo31 = new SqlParameter("@DRCustAcNo31", txtDRCustAcNo31.Text.Trim());
                    SqlParameter DRCustAcNo32 = new SqlParameter("@DRCustAcNo32", txtDRCustAcNo32.Text.Trim());
                    SqlParameter DRCurr3 = new SqlParameter("@DRCurr3", txtDRCurr3.Text.Trim());
                    SqlParameter DRAmount3 = new SqlParameter("@DRAmount3", txtDRAmount3.Text.Trim());
                    SqlParameter DRGLCode4 = new SqlParameter("@DRGLCode4", txtDRGLCode4.Text.Trim());
                    SqlParameter DRCustAbbr4 = new SqlParameter("@DRCustAbbr4", txtDRCustAbbr4.Text.Trim());
                    SqlParameter DRCustAcNo41 = new SqlParameter("@DRCustAcNo41", txtDRCustAcNo41.Text.Trim());
                    SqlParameter DRCustAcNo42 = new SqlParameter("@DRCustAcNo42", txtDRCustAcNo42.Text.Trim());
                    SqlParameter DRCurr4 = new SqlParameter("@DRCurr4", txtDRCurr4.Text.Trim());
                    SqlParameter DRAmount4 = new SqlParameter("@DRAmount4", txtDRAmount4.Text.Trim());
                    SqlParameter DRGLCode5 = new SqlParameter("@DRGLCode5", txtDRGLCode5.Text.Trim());
                    SqlParameter DRCustAbbr5 = new SqlParameter("@DRCustAbbr5", txtDRCustAbbr5.Text.Trim());
                    SqlParameter DRCustAcNo51 = new SqlParameter("@DRCustAcNo51", txtDRCustAcNo51.Text.Trim());
                    SqlParameter DRCustAcNo52 = new SqlParameter("@DRCustAcNo52", txtDRCustAcNo52.Text.Trim());
                    SqlParameter DRCurr5 = new SqlParameter("@DRCurr5", txtDRCurr5.Text.Trim());
                    SqlParameter DRAmount5 = new SqlParameter("@DRAmount5", txtDRAmount5.Text.Trim());
                    SqlParameter PayingBank = new SqlParameter("@PayingBank", txtPayingBankID.Text.Trim());
                    string ddldisind = "";

                    if (ddlDispachInd.SelectedValue == "---Select---")
                    {
                        ddldisind = "";
                    }
                    else
                    {
                        ddldisind = ddlDispachInd.SelectedValue.Trim();
                    }
                    SqlParameter DispatchIndc = new SqlParameter("@DispIndc", ddldisind);
                    SqlParameter Merchant_Trade = new SqlParameter("@MerchantTrade", ddlMercTrade.SelectedValue.Trim());
                    //string SaveDraft = "";
                    //if (ddlDispachInd.SelectedValue == "Dispatched directly by exporter" && ddlDispBydefault.SelectedValue == "Digi-Export")
                    //{
                    //    SaveDraft = "A";
                    //}
                    //else
                    //{
                    //    SaveDraft = "C";
                    //}
                    SqlParameter Save_Draft = new SqlParameter("@Save_Draft", "C");
                    //SqlParameter Save_Draft = new SqlParameter("@Save_Draft", SaveDraft);
                    //SqlParameter DispatchIndcDefValue = new SqlParameter("@DispIndByDeafValue", txtDispBydefault.Text.Trim());
                    SqlParameter DispatchIndcDefValue = new SqlParameter("@DispIndByDeafValue", ddlDispBydefault.SelectedValue.Trim());

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

                    SqlParameter PFIRCCurrency1_OB = new SqlParameter("@FIRCCurrency1_OB", ddlFIRCCurrency1_OB.SelectedValue);
                    SqlParameter PFIRCCurrency2_OB = new SqlParameter("@FIRCCurrency2_OB", ddlFIRCCurrency2_OB.SelectedValue);
                    SqlParameter PFIRCCurrency3_OB = new SqlParameter("@FIRCCurrency3_OB", ddlFIRCCurrency3_OB.SelectedValue);
                    SqlParameter PFIRCCurrency4_OB = new SqlParameter("@FIRCCurrency4_OB", ddlFIRCCurrency4_OB.SelectedValue);
                    SqlParameter PFIRCCurrency5_OB = new SqlParameter("@FIRCCurrency5_OB", ddlFIRCCurrency5_OB.SelectedValue);
                    SqlParameter PFIRCCurrency6_OB = new SqlParameter("@FIRCCurrency6_OB", ddlFIRCCurrency6_OB.SelectedValue);
                    SqlParameter PFIRCCurrency7_OB = new SqlParameter("@FIRCCurrency7_OB", ddlFIRCCurrency7_OB.SelectedValue);
                    SqlParameter PFIRCCurrency8_OB = new SqlParameter("@FIRCCurrency8_OB", ddlFIRCCurrency8_OB.SelectedValue);
                    SqlParameter PFIRCCurrency9_OB = new SqlParameter("@FIRCCurrency9_OB", ddlFIRCCurrency9_OB.SelectedValue);
                    SqlParameter PFIRCCurrency10_OB = new SqlParameter("@FIRCCurrency10_OB", ddlFIRCCurrency10_OB.SelectedValue);
                    SqlParameter PFIRCCurrency11_OB = new SqlParameter("@FIRCCurrency11_OB", ddlFIRCCurrency11_OB.SelectedValue);
                    SqlParameter PFIRCCurrency12_OB = new SqlParameter("@FIRCCurrency12_OB", ddlFIRCCurrency12_OB.SelectedValue);
                    SqlParameter PFIRCCurrency13_OB = new SqlParameter("@FIRCCurrency13_OB", ddlFIRCCurrency13_OB.SelectedValue);
                    SqlParameter PFIRCCurrency14_OB = new SqlParameter("@FIRCCurrency14_OB", ddlFIRCCurrency14_OB.SelectedValue);
                    SqlParameter PFIRCCurrency15_OB = new SqlParameter("@FIRCCurrency15_OB", ddlFIRCCurrency15_OB.SelectedValue);


                    SqlParameter PFIRCRealisedCurr1_OB = new SqlParameter("@FIRCRealisedCurr1_OB", ddlFIRCRealisedCurr1_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr2_OB = new SqlParameter("@FIRCRealisedCurr2_OB", ddlFIRCRealisedCurr2_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr3_OB = new SqlParameter("@FIRCRealisedCurr3_OB", ddlFIRCRealisedCurr3_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr4_OB = new SqlParameter("@FIRCRealisedCurr4_OB", ddlFIRCRealisedCurr4_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr5_OB = new SqlParameter("@FIRCRealisedCurr5_OB", ddlFIRCRealisedCurr5_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr6_OB = new SqlParameter("@FIRCRealisedCurr6_OB", ddlFIRCRealisedCurr6_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr7_OB = new SqlParameter("@FIRCRealisedCurr7_OB", ddlFIRCRealisedCurr7_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr8_OB = new SqlParameter("@FIRCRealisedCurr8_OB", ddlFIRCRealisedCurr8_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr9_OB = new SqlParameter("@FIRCRealisedCurr9_OB", ddlFIRCRealisedCurr9_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr10_OB = new SqlParameter("@FIRCRealisedCurr10_OB", ddlFIRCRealisedCurr10_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr11_OB = new SqlParameter("@FIRCRealisedCurr11_OB", ddlFIRCRealisedCurr11_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr12_OB = new SqlParameter("@FIRCRealisedCurr12_OB", ddlFIRCRealisedCurr12_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr13_OB = new SqlParameter("@FIRCRealisedCurr13_OB", ddlFIRCRealisedCurr13_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr14_OB = new SqlParameter("@FIRCRealisedCurr14_OB", ddlFIRCRealisedCurr14_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr15_OB = new SqlParameter("@FIRCRealisedCurr15_OB", ddlFIRCRealisedCurr15_OB.SelectedValue);

                    SqlParameter PFIRCCrossCurrRate1_OB = new SqlParameter("@FIRCCrossCurrRate1_OB", txtFIRCCrossCurrRate1_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate2_OB = new SqlParameter("@FIRCCrossCurrRate2_OB", txtFIRCCrossCurrRate2_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate3_OB = new SqlParameter("@FIRCCrossCurrRate3_OB", txtFIRCCrossCurrRate3_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate4_OB = new SqlParameter("@FIRCCrossCurrRate4_OB", txtFIRCCrossCurrRate4_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate5_OB = new SqlParameter("@FIRCCrossCurrRate5_OB", txtFIRCCrossCurrRate5_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate6_OB = new SqlParameter("@FIRCCrossCurrRate6_OB", txtFIRCCrossCurrRate6_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate7_OB = new SqlParameter("@FIRCCrossCurrRate7_OB", txtFIRCCrossCurrRate7_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate8_OB = new SqlParameter("@FIRCCrossCurrRate8_OB", txtFIRCCrossCurrRate8_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate9_OB = new SqlParameter("@FIRCCrossCurrRate9_OB", txtFIRCCrossCurrRate9_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate10_OB = new SqlParameter("@FIRCCrossCurrRate10_OB", txtFIRCCrossCurrRate10_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate11_OB = new SqlParameter("@FIRCCrossCurrRate11_OB", txtFIRCCrossCurrRate11_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate12_OB = new SqlParameter("@FIRCCrossCurrRate12_OB", txtFIRCCrossCurrRate12_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate13_OB = new SqlParameter("@FIRCCrossCurrRate13_OB", txtFIRCCrossCurrRate13_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate14_OB = new SqlParameter("@FIRCCrossCurrRate14_OB", txtFIRCCrossCurrRate14_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate15_OB = new SqlParameter("@FIRCCrossCurrRate15_OB", txtFIRCCrossCurrRate15_OB.Text.Trim());


                    SqlParameter PFIRCTobeAdjustedinSB1_OB = new SqlParameter("@FIRCTobeAdjustedinSB1_OB", txtFIRCTobeAdjustedinSB1_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB2_OB = new SqlParameter("@FIRCTobeAdjustedinSB2_OB", txtFIRCTobeAdjustedinSB2_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB3_OB = new SqlParameter("@FIRCTobeAdjustedinSB3_OB", txtFIRCTobeAdjustedinSB3_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB4_OB = new SqlParameter("@FIRCTobeAdjustedinSB4_OB", txtFIRCTobeAdjustedinSB4_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB5_OB = new SqlParameter("@FIRCTobeAdjustedinSB5_OB", txtFIRCTobeAdjustedinSB5_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB6_OB = new SqlParameter("@FIRCTobeAdjustedinSB6_OB", txtFIRCTobeAdjustedinSB6_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB7_OB = new SqlParameter("@FIRCTobeAdjustedinSB7_OB", txtFIRCTobeAdjustedinSB7_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB8_OB = new SqlParameter("@FIRCTobeAdjustedinSB8_OB", txtFIRCTobeAdjustedinSB8_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB9_OB = new SqlParameter("@FIRCTobeAdjustedinSB9_OB", txtFIRCTobeAdjustedinSB9_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB10_OB = new SqlParameter("@FIRCTobeAdjustedinSB10_OB", txtFIRCTobeAdjustedinSB10_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB11_OB = new SqlParameter("@FIRCTobeAdjustedinSB11_OB", txtFIRCTobeAdjustedinSB11_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB12_OB = new SqlParameter("@FIRCTobeAdjustedinSB12_OB", txtFIRCTobeAdjustedinSB12_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB13_OB = new SqlParameter("@FIRCTobeAdjustedinSB13_OB", txtFIRCTobeAdjustedinSB13_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB14_OB = new SqlParameter("@FIRCTobeAdjustedinSB14_OB", txtFIRCTobeAdjustedinSB14_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB15_OB = new SqlParameter("@FIRCTobeAdjustedinSB15_OB", txtFIRCTobeAdjustedinSB15_OB.Text.Trim());
                    //---------------------------------------------NILESH 04/08/2023-----------------------------------
                    SqlParameter ConsigneePardyID = new SqlParameter("@consigneePartyID", txtconsigneePartyID.Text.Trim());
                    SqlParameter pNoOfSB = new SqlParameter("@NoOfShippingBills", txtNoOfSB.Text.Trim());
                    string PendingSB = "N";
                    if (chkSB.Checked)
                        PendingSB = "Y";
                    pIsManualGR.Value = bit_IsManualGR;
                    SqlParameter pCheckPendingSB = new SqlParameter("@PendingSBFlag", PendingSB);

                    //---------------------------------------------NILESH END 04/08/2023-----------------------------------


                    string _query = "TF_EXP_UpdateExportEntryDetails";
                    TF_DATA objSave = new TF_DATA();

                    _result = objSave.SaveDeleteData(_query, bCode, docNumber, p1, p2, p3, p4, p5, pCustAcNo, pOverseasPartyID,
                        pOverseasBankID,
                        //pCustCheck, pCS,
                        pDateRcvd, pENCdate, pDateNegotiated, pLCNo, pLCDate, pLCissuedBy, pBENo, pBEDate,
                        pBEdoc, pbankLine, pAccpDate, pInvoiceNo, pInvoiceDate, pInvoiceDoc, pAWBno, pAWBdate, pAWBissuedBy, pAWBdoc, pPackingList,
                        pPackingListDate, pPackingListDoc, pCertOfOrigin, pCertOfOriginIssuedBy, pCertOfOriginDoc, pCustomsInvoice, pCustomsInvoiceDate,
                        pCustomsInvoiceDoc, pCommodity, pInsPolicy, pInsPolicyDate, pInsPolicyIssuedBy, pInsPolicydoc, pGSPdate, pGSPdoc, pContractNo,
                        pContractRate, pFIRCno, pFIRCdate, pFIRCissuedBy, pFIRCdoc, pMisc, pShipment, pMiscDoc,
                        //pCoveringFrom, 
                        pCoveringTo, pCountryCode,
                        preimbValDate, preimbClaimDate, preimbBankName, preimbBankBICcode, preimbAmount, pNotes,
                        pCurr, pLoanAdv, pOthCurr, pExchRT, pNoOfDays, pAfterFrom, pLibor, pOutOfDays,
                        pDueDate, pAccpDueDate, pOtherRemarks, pBillAmt, pBillAmtinRS, pNegotiated, pNegotiatedinRS, pInterest, pInterestinRS,
                        pNetAmt, pNetAmtinRS, pExchRtEBR, pOtherCharges, pSTFXDLS, pBankCert, pNegotiationFees, pCourierCharges, pMarginAmount, pCommission,
                        psTax, psAmt, pCurrentAcinRS, pcb1, pcb2, pcb3, pcb4, pcb5, pcb6, pcb7, pcb7A, pcb7B, pcb8, pcb9, pcb10, pcb11, pcb12, pcb13, pRemarks, pRemarks1, pUser, pUploadDate, pchkRBI
                        , pCustCheck, pCS
                        , pTTRef1, pTTRef2, pTTRef3, pTTRef4, pTTRef5, pTTRef6, pTTRef7, pTTRef8, pTTRef9, pTTRef10, pTTRef11, pTTRef12, pTTRef13, pTTRef14, pTTRef15
                        , pTTAmt1, pTTAmt2, pTTAmt3, pTTAmt4, pTTAmt5, pTTAmt6, pTTAmt7, pTTAmt8, pTTAmt9, pTTAmt10, pTTAmt11, pTTAmt12, pTTAmt13, pTTAmt14, pTTAmt15
                        , pIntRate1, pForDays1, pIntRate2, pForDays2, pIntRate3, pForDays3, pIntRate4, pForDays4, pIntRate5, pForDays5, pIntRate6, pForDays6
                        , pIntFDate1, pIntTDate1, pIntFDate2, pIntTDate2, pIntFDate3, pIntTDate3, pIntFDate4, pIntTDate4, pIntFDate5, pIntTDate5, pIntFDate6, pIntTDate6
                        , pCommissionID
                        , pFIRCNo1_OB, pFIRCDate1_OB, pFIRCAmt1_OB, pFIRCADCode1_OB
                        , pFIRCNo2_OB, pFIRCDate2_OB, pFIRCAmt2_OB, pFIRCADCode2_OB
                        , pFIRCNo3_OB, pFIRCDate3_OB, pFIRCAmt3_OB, pFIRCADCode3_OB
                        , pFIRCNo4_OB, pFIRCDate4_OB, pFIRCAmt4_OB, pFIRCADCode4_OB
                        , pFIRCNo5_OB, pFIRCDate5_OB, pFIRCAmt5_OB, pFIRCADCode5_OB
                        , pFIRCNo6_OB, pFIRCDate6_OB, pFIRCAmt6_OB, pFIRCADCode6_OB
                        , pFIRCNo7_OB, pFIRCDate7_OB, pFIRCAmt7_OB, pFIRCADCode7_OB
                        , pFIRCNo8_OB, pFIRCDate8_OB, pFIRCAmt8_OB, pFIRCADCode8_OB
                        , pFIRCNo9_OB, pFIRCDate9_OB, pFIRCAmt9_OB, pFIRCADCode9_OB
                        , pFIRCNo10_OB, pFIRCDate10_OB, pFIRCAmt10_OB, pFIRCADCode10_OB
                        , pFIRCNo11_OB, pFIRCDate11_OB, pFIRCAmt11_OB, pFIRCADCode11_OB
                        , pFIRCNo12_OB, pFIRCDate12_OB, pFIRCAmt12_OB, pFIRCADCode12_OB
                        , pFIRCNo13_OB, pFIRCDate13_OB, pFIRCAmt13_OB, pFIRCADCode13_OB
                        , pFIRCNo14_OB, pFIRCDate14_OB, pFIRCAmt14_OB, pFIRCADCode14_OB
                        , pFIRCNo15_OB, pFIRCDate15_OB, pFIRCAmt15_OB, pFIRCADCode15_OB
                        , padCode, pForeignLocal, pIsManualGR, SBcess, SBcessamt, kkcess, kkcessamt, SBFxDls, totalSBFxDls, kkcessfxdls, STTamt,
                         fbkchrg, staxfbk, staxfbkamt, sbcessfbk, sbcessamtfbk, kkcessfbk, kkcessfbkamt, totfbkamt,
                        //GBASE FIELDS 
                        DD_Doc1
               , Invoice_Doc1
               , Customs_Invoice_Doc1
               , Ins_Policy_Doc1
               , Cert_Of_Origin_Doc1
               , Packing_List_Doc1
               , WM_Doc
               , WM_Doc1
               , INSP_Doc
               , INSP_Doc1
               , AWB_Doc1
               , OTHER_Doc
               , OTHER_Doc1
               , GBase_Commodity_ID
               , Operation_Type
               , Settlement_Option
               , Risk_Country
               , Payer
               , Fund_Type
               , Base_Rate_Code
               , Grade_Code
               , Direction
               , Covr_Instruction
               , Internal_Rate
               , Spread_Rate
               , Application_No
               , Remarks_EUC
               , Draft_No
               , Risk_Customer
               , Vessel_Name
               , Instructions
               , Reimbursing_Bank
               , GBaseRemarks
               , Merchandise
               , Special_Instructions1
               , Special_Instructions2
               , Special_Instructions3
               , Special_Instructions4
               , Special_Instructions5
               , Special_Instructions6
               , Special_Instructions7
               , Special_Instructions8
               , Special_Instructions9
               , Special_Instructions10
               , PrincipalContractNo1
               , PrincipalContractNo2
               , PrincipalExchCurr
               , PrincipalExchRate
               , PrincipalIntExchRate
               , InterestLump
               , InterestContractNo1
               , InterestContractNo2
               , InterestExchCurr
               , InterestExchRate
               , InterestIntExchRate
               , CommissionMatu
               , CommissionContractNo1
               , CommissionContractNo2
               , CommissionExchCurr
               , CommissionExchRate
               , CommissionIntExchRate
               , CRGLCode
               , CRCustAbbr
               , CRCustAcNo1
               , CRCustAcNo2
               , CRCurr
               , CRAmount
               , CRIntCurr
               , CRIntAmount
               , CRIntPayer
               , CRPaymentCommCurr
               , CRPaymentCommAmount
               , CRPaymentCommPayer
               , CRHandlingCommCurr
               , CRHandlingCommAmount
               , CRHandlingCommPayer
               , CRPostageCurr
               , CRPostageAmount
               , CRPostagePayer
               , CRCurr1
               , CRAmount1
               , CRPayer1
               , DRCurr
               , DRAmount
               , DRGLCode1
               , DRCustAbbr1
               , DRCustAcNo11
               , DRCustAcNo12
               , DRCurr1
               , DRAmount1
               , DRGLCode2
               , DRCustAbbr2
               , DRCustAcNo21
               , DRCustAcNo22
               , DRCurr2
               , DRAmount2
               , DRGLCode3
               , DRCustAbbr3
               , DRCustAcNo31
               , DRCustAcNo32
               , DRCurr3
               , DRAmount3
               , DRGLCode4
               , DRCustAbbr4
               , DRCustAcNo41
               , DRCustAcNo42
               , DRCurr4
               , DRAmount4
               , DRGLCode5
               , DRCustAbbr5
               , DRCustAcNo51
               , DRCustAcNo52
               , DRCurr5
               , DRAmount5
               , PayingBank
               , DispatchIndc
               , DispatchIndcDefValue
               , Merchant_Trade, Save_Draft,
                PTTCurr1,
                PTTCurr2, PTTCurr3, PTTCurr4, PTTCurr5, PTTCurr6, PTTCurr7, PTTCurr8, PTTCurr9, PTTCurr10, PTTCurr11, PTTCurr12, PTTCurr13,
                PTTCurr14, PTTCurr15,
                PTotTTAmt1, PTotTTAmt2, PTotTTAmt3, PTotTTAmt4, PTotTTAmt5, PTotTTAmt6, PTotTTAmt7, PTotTTAmt8, PTotTTAmt9, PTotTTAmt10, PTotTTAmt11, PTotTTAmt12,
                 PTotTTAmt13, PTotTTAmt14, PTotTTAmt15,
                 PBalTTAmt1, PBalTTAmt2, PBalTTAmt3, PBalTTAmt4, PBalTTAmt5, PBalTTAmt6, PBalTTAmt7, PBalTTAmt8, PBalTTAmt9, PBalTTAmt10, PBalTTAmt11, PBalTTAmt12, PBalTTAmt13,
                 PBalTTAmt14, PBalTTAmt15,
                 PTTRealisedCurr1, PTTRealisedCurr2, PTTRealisedCurr3, PTTRealisedCurr4, PTTRealisedCurr5, PTTRealisedCurr6, PTTRealisedCurr7, PTTRealisedCurr8,
                 PTTRealisedCurr9, PTTRealisedCurr10, PTTRealisedCurr11, PTTRealisedCurr12, PTTRealisedCurr13, PTTRealisedCurr14, PTTRealisedCurr15,

                 PTTCrossCurrRate1, PTTCrossCurrRate2, PTTCrossCurrRate3, PTTCrossCurrRate4, PTTCrossCurrRate5, PTTCrossCurrRate6, PTTCrossCurrRate7, PTTCrossCurrRate8,
                 PTTCrossCurrRate9, PTTCrossCurrRate10, PTTCrossCurrRate11, PTTCrossCurrRate12, PTTCrossCurrRate13, PTTCrossCurrRate14, PTTCrossCurrRate15,

                 PTTAmtRealised1, PTTAmtRealised2, PTTAmtRealised3, PTTAmtRealised4, PTTAmtRealised5, PTTAmtRealised6, PTTAmtRealised7, PTTAmtRealised8, PTTAmtRealised9,
                  PTTAmtRealised10, PTTAmtRealised11, PTTAmtRealised12, PTTAmtRealised13, PTTAmtRealised14, PTTAmtRealised15,

                  PFIRCCurrency1_OB, PFIRCCurrency2_OB, PFIRCCurrency3_OB, PFIRCCurrency4_OB, PFIRCCurrency5_OB, PFIRCCurrency6_OB, PFIRCCurrency7_OB, PFIRCCurrency8_OB,
                  PFIRCCurrency9_OB, PFIRCCurrency10_OB, PFIRCCurrency11_OB, PFIRCCurrency12_OB, PFIRCCurrency13_OB, PFIRCCurrency14_OB, PFIRCCurrency15_OB,

                  PFIRCRealisedCurr1_OB, PFIRCRealisedCurr2_OB, PFIRCRealisedCurr3_OB, PFIRCRealisedCurr4_OB, PFIRCRealisedCurr5_OB, PFIRCRealisedCurr6_OB,
                  PFIRCRealisedCurr7_OB, PFIRCRealisedCurr8_OB, PFIRCRealisedCurr9_OB, PFIRCRealisedCurr10_OB, PFIRCRealisedCurr11_OB, PFIRCRealisedCurr12_OB,
                  PFIRCRealisedCurr13_OB, PFIRCRealisedCurr14_OB, PFIRCRealisedCurr15_OB,

                  PFIRCCrossCurrRate1_OB, PFIRCCrossCurrRate2_OB, PFIRCCrossCurrRate3_OB, PFIRCCrossCurrRate4_OB, PFIRCCrossCurrRate5_OB, PFIRCCrossCurrRate6_OB,
                  PFIRCCrossCurrRate7_OB, PFIRCCrossCurrRate8_OB, PFIRCCrossCurrRate9_OB, PFIRCCrossCurrRate10_OB, PFIRCCrossCurrRate11_OB, PFIRCCrossCurrRate12_OB,
                   PFIRCCrossCurrRate13_OB, PFIRCCrossCurrRate14_OB, PFIRCCrossCurrRate15_OB,

                PFIRCTobeAdjustedinSB1_OB, PFIRCTobeAdjustedinSB2_OB, PFIRCTobeAdjustedinSB3_OB, PFIRCTobeAdjustedinSB4_OB, PFIRCTobeAdjustedinSB5_OB, PFIRCTobeAdjustedinSB6_OB,
                PFIRCTobeAdjustedinSB7_OB, PFIRCTobeAdjustedinSB8_OB, PFIRCTobeAdjustedinSB9_OB, PFIRCTobeAdjustedinSB10_OB, PFIRCTobeAdjustedinSB11_OB, PFIRCTobeAdjustedinSB12_OB,
                PFIRCTobeAdjustedinSB13_OB, PFIRCTobeAdjustedinSB14_OB, PFIRCTobeAdjustedinSB15_OB
               //----------------------------------NILESH 04/08/2023--------------------------------
               , ConsigneePardyID, pNoOfSB, pCheckPendingSB
                //---------------------------------------------NILESH END 04/08/2023-----------------------------------
                );
                    if (_result.Substring(0, 5) == "added")
                    {
                        txtDocumentNo.Text = "";
                    }
                    //=========================LEI Changes===========================//
                    if (_result.Substring(0, 5) == "added" || _result == "updated")
                    {
                        string _ForeignOrLocalLei = Request.QueryString["ForeignOrLocal"].ToString().Trim();

                        if (_ForeignOrLocalLei == "F")
                        {
                            string DOCLEI = "";
                            if (_result.Substring(0, 5) == "added")
                                DOCLEI = _result.Substring(5);
                            else
                                DOCLEI = txtDocumentNo.Text.Trim();
                            SqlParameter LEIbCode = new SqlParameter("@bCode", Request.QueryString["BranchCode"].Trim());
                            SqlParameter LEIdocNo = new SqlParameter("@docNo", DOCLEI);
                            SqlParameter LEIdocPrfx = new SqlParameter("@docPrfx", Request.QueryString["DocPrFx"].Trim());
                            SqlParameter LEIbillType = new SqlParameter("@billType", _billType);
                            SqlParameter LEICustAcNo = new SqlParameter("@custAcNo", txtCustAcNo.Text.Trim());
                            SqlParameter LEICust_Name = new SqlParameter("@CustName", hdnCustname.Value);
                            SqlParameter LEICust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustabbr.Value);
                            SqlParameter LEICust_LEI = new SqlParameter("@Cust_LEI", hdncustlei.Value);
                            SqlParameter LEICust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdncustleiexpiry.Value);
                            SqlParameter LEIOverseasPartyID = new SqlParameter("@OverseasPartyID", txtOverseasPartyID.Text.Trim());
                            SqlParameter LEIOverseas_LEI = new SqlParameter("@Overseas_LEI", hdnoverseaslei.Value);
                            SqlParameter LEIOverseas_LEI_Expiry = new SqlParameter("@Overseas_LEI_Expiry", hdnoverseasleiexpiry.Value);
                            SqlParameter LEIAmountFC = new SqlParameter("@ActbillAmt", txtBillAmount.Text.Trim());
                            SqlParameter LEIAmtInINR = new SqlParameter("@ActbillAmtinRS", hdnbillamtinr.Value);
                            SqlParameter LEICurrency = new SqlParameter("@Curr", ddlCurrency.SelectedValue);
                            SqlParameter LEIExchRate = new SqlParameter("@exchRtEBR", lbl_Exch_rate.Text);
                            SqlParameter LEIdateRcvd = new SqlParameter("@dateRcvd", txtDateRcvd.Text.Trim());
                            SqlParameter LEIDueDate = new SqlParameter("@DueDate", txtDueDate.Text.Trim());
                            SqlParameter LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value);
                            SqlParameter LEI_SpecialFlag = new SqlParameter("@LEI_SpecialFlag", hdnLeiSpecialFlag.Value);
                            string Trans_Status1 = "L";
                            SqlParameter LEITrans_Status = new SqlParameter("@Trans_Status", Trans_Status1);
                            SqlParameter LEISrNo = new SqlParameter("@SrNo", "");
                            SqlParameter LEIPayIndicator = new SqlParameter("@Realised_PAY_Indicator", "");
                            SqlParameter LEIuser = new SqlParameter("@user", Session["userName"].ToString());
                            SqlParameter LEIuploadingdate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                            string _resultLEI = "";
                            string _queryLEI = "TF_EXP_Update_LEITransaction";
                            TF_DATA objSaveLEI = new TF_DATA();

                            _resultLEI = objSaveLEI.SaveDeleteData(_queryLEI, LEIbCode, LEIdocNo, LEIdocPrfx, LEIbillType, LEICustAcNo, LEICust_Name, LEICust_Abbr, LEICust_LEI, LEICust_LEI_Expiry, LEIOverseasPartyID,
                                LEIOverseas_LEI, LEIOverseas_LEI_Expiry, LEIAmountFC, LEIAmtInINR, LEICurrency, LEIExchRate, LEIdateRcvd, LEIDueDate, LEI_Flag, LEI_SpecialFlag, LEITrans_Status, LEISrNo, LEIPayIndicator, LEIuser, LEIuploadingdate);
                        }
                        //=========================END====================================//

                        string s = "";

                        //========================= GR/PP/CutomsDetails=====================//
                        if (GridViewGRPPCustomsDetails.Visible == true)
                        {
                            int rowIndex = 0;

                            TF_DATA objSaveGRCustomsDetails = new TF_DATA();

                            SqlParameter pGRbCode = new SqlParameter("@bCode", SqlDbType.VarChar);
                            SqlParameter pGRDocNo = new SqlParameter("@documentNo", SqlDbType.VarChar);

                            SqlParameter pGRno = new SqlParameter("@GR", SqlDbType.VarChar);

                            SqlParameter pGRCurrency = new SqlParameter("@currency", SqlDbType.VarChar);
                            SqlParameter pGRAmount = new SqlParameter("@amount", SqlDbType.VarChar);
                            SqlParameter pGRShippingBillNo = new SqlParameter("@shippingBillNo", SqlDbType.VarChar);
                            SqlParameter pGRShippingBillDate = new SqlParameter("@shippingBillDate", SqlDbType.VarChar);
                            SqlParameter pGRCommission = new SqlParameter("@commission", SqlDbType.VarChar);
                            SqlParameter pGRPortCode = new SqlParameter("@portCode", SqlDbType.VarChar);

                            SqlParameter pGRPFormType = new SqlParameter("@formtype", SqlDbType.VarChar);

                            SqlParameter pGRdispind = new SqlParameter("@dispind", SqlDbType.VarChar);
                            SqlParameter pGRinvoiceno = new SqlParameter("@invoicenum", SqlDbType.VarChar);
                            SqlParameter pGRinvoiceDate = new SqlParameter("@invoicedt", SqlDbType.VarChar);
                            SqlParameter pGRinvoiceAmt = new SqlParameter("@invoiceamt", SqlDbType.VarChar);

                            SqlParameter pGR_frieght = new SqlParameter("@frieghtAmt", SqlDbType.VarChar);
                            SqlParameter pGR_insurance = new SqlParameter("@insuranceAmt", SqlDbType.VarChar);
                            SqlParameter pGR_discount = new SqlParameter("@discountAmt", SqlDbType.VarChar);
                            SqlParameter pGR_otherDeduction = new SqlParameter("@otherDeduction", SqlDbType.VarChar);
                            SqlParameter pGR_packinChrgs = new SqlParameter("@packingCharges", SqlDbType.VarChar);

                            SqlParameter pGR_frieghtCurr = new SqlParameter("@curFreight", SqlDbType.VarChar);
                            SqlParameter pGR_insuranceCurr = new SqlParameter("@curInsurance", SqlDbType.VarChar);
                            SqlParameter pGR_discountCurr = new SqlParameter("@curDiscount", SqlDbType.VarChar);
                            SqlParameter pGR_CommCurr = new SqlParameter("@curComm", SqlDbType.VarChar);
                            SqlParameter pGR_otherDeductionCurr = new SqlParameter("@curOtherDeduction", SqlDbType.VarChar);
                            SqlParameter pGR_packinChrgsCurr = new SqlParameter("@curPackingCharges", SqlDbType.VarChar);
                            SqlParameter pGR_GRExchRate = new SqlParameter("@GRExchRate", SqlDbType.VarChar);
                            SqlParameter invsrno = new SqlParameter("@invsrno", SqlDbType.VarChar);
                            SqlParameter status = new SqlParameter("@status", SqlDbType.VarChar);

                            ////SqlParameter pGRPCommodity = new SqlParameter("@commodity", SqlDbType.VarChar);
                            //SqlParameter pGRPAmtInINR = new SqlParameter("@amount_in_inr", SqlDbType.VarChar);
                            //SqlParameter pGRPExchRate = new SqlParameter("@exchrate", SqlDbType.VarChar);


                            if (ddlDispachInd.SelectedValue == "---Select---")
                            {
                                pGRdispind.Value = "";
                            }
                            else
                            {
                                pGRdispind.Value = ddlDispachInd.SelectedValue;
                            }
                            //SqlParameter pDsipDefltValue = new SqlParameter("@dispDefaval", SqlDbType.VarChar);

                            _query = "TF_EXP_DeleteGRPPCustomsDetails";
                            if (_result.Substring(0, 5) == "added")
                                pGRDocNo.Value = _result.Substring(5);
                            else
                                pGRDocNo.Value = txtDocumentNo.Text;
                            TF_DATA objDel = new TF_DATA();
                            objDel.SaveDeleteData(_query, pGRDocNo);

                            _query = "TF_EXP_UpdateGRPPCustomsDetails";

                            // pcMode.Value = _mode;
                            pGRbCode.Value = hdnBranchCode.Value;

                            rowIndex = 0;
                            for (int i = 1; i <= GridViewGRPPCustomsDetails.Rows.Count; i++)
                            {
                                //extract the TextBox values
                                if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                                {
                                    Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblSrNo");
                                    Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFormType");
                                    Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGR");
                                    Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCurrency");
                                    Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblAmount");
                                    //Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblExchRate");
                                    //Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblAmountInInr");
                                    Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillNo");
                                    Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillDate");
                                    Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommission");
                                    Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPortCode");

                                    //Label lbl14 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDispInd");
                                    Label lbl15 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceNo");
                                    Label lbl16 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceDate");
                                    Label lbl17 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceAmt");

                                    Label lblFreightAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFreightAmt");
                                    Label lblInsuranceAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsuranceAmt");
                                    Label lblDiscountAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDiscountAmt");
                                    Label lblOthDeduction = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOthDeduction");
                                    Label lblPacking = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPacking");


                                    Label lblFreightCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFrieghtCurr");
                                    Label lblInsuranceCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsCurr");
                                    Label lblDiscountCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDisCurr");
                                    Label lblCommCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommCurr");
                                    Label lblOthDeductionCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOtherDedCurr");
                                    Label lblPackingCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPackingChgCurr");

                                    Label lblGRExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGRExchRate");

                                    Label lblinvsrno = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblinvsrno");
                                    Label lblstatus = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblstatus");

                                    invsrno.Value = lblinvsrno.Text;
                                    status.Value = lblstatus.Text;

                                    pGRPFormType.Value = lbl2.Text;
                                    ////pGRPCommodity.Value = lbl3.Text;
                                    pGRno.Value = lbl4.Text;
                                    pGRCurrency.Value = lbl5.Text;
                                    pGRAmount.Value = lbl6.Text;
                                    //pGRPExchRate.Value = lbl7.Text;
                                    //pGRPAmtInINR.Value = lbl8.Text;
                                    pGRShippingBillNo.Value = lbl9.Text;
                                    pGRShippingBillDate.Value = lbl10.Text;
                                    pGRCommission.Value = lbl11.Text;
                                    pGRPortCode.Value = lbl12.Text;

                                    //pGRdispind.Value = lbl14.Text;
                                    pGRinvoiceno.Value = lbl15.Text;
                                    pGRinvoiceDate.Value = lbl16.Text;
                                    pGRinvoiceAmt.Value = lbl17.Text;

                                    pGR_frieght.Value = lblFreightAmt.Text;
                                    pGR_insurance.Value = lblInsuranceAmt.Text;
                                    pGR_discount.Value = lblDiscountAmt.Text;
                                    pGR_otherDeduction.Value = lblOthDeduction.Text;
                                    pGR_packinChrgs.Value = lblPacking.Text;


                                    pGR_frieghtCurr.Value = lblFreightCurr.Text;
                                    pGR_insuranceCurr.Value = lblInsuranceCurr.Text;
                                    pGR_discountCurr.Value = lblDiscountCurr.Text;
                                    pGR_CommCurr.Value = lblCommCurr.Text;
                                    pGR_otherDeductionCurr.Value = lblOthDeductionCurr.Text;
                                    pGR_packinChrgsCurr.Value = lblPackingCurr.Text;

                                    pGR_GRExchRate.Value = lblGRExchRate.Text;

                                }

                                s = objSaveGRCustomsDetails.SaveDeleteData(_query, pGRbCode, pGRDocNo, pGRno, pGRCurrency, pGRAmount,
                                                pGRShippingBillNo, pGRShippingBillDate, pGRCommission, pGRPortCode, pGRPFormType,
                                                pGRdispind, pGRinvoiceno, pGRinvoiceDate, pGRinvoiceAmt,
                                                pGR_frieght, pGR_insurance, pGR_discount, pGR_otherDeduction, pGR_packinChrgs,
                                                pGR_frieghtCurr, pGR_insuranceCurr, pGR_discountCurr, pGR_CommCurr, pGR_otherDeductionCurr, pGR_packinChrgsCurr, invsrno, status, pGR_GRExchRate
                                                );
                                rowIndex++;
                            }
                        }
                    }

                    _script = "";
                    string _OldValues = "";// Added by Anand 04-01-2024
                    string _NewValues = "";// Added by Anand 04-01-2024
                    string _Status = "C";

                    var argument = ((Button)sender).CommandArgument;

                    if (_result.Substring(0, 5) == "added")
                    {
                        AuditDocument(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 04-01-2024
                        AuditTransaction(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                        AuditGBase(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                        AuditGRPP(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 08-01-2024
                        if (argument.ToString() == "print")
                        {
                            _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }

                        else
                        {

                            _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=Submit&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }
                    else
                    {
                        if (_result == "updated")
                        {
                            AuditDocument(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 04-01-2024
                            AuditTransaction(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                            AuditGBase(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                            if (argument.ToString() == "print")
                            {
                                _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            }
                            else
                            {

                                _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=Submit&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            }
                        }
                        else
                            labelMessage.Text = _result;
                    }
                }

            }
        }
    }

    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime CheckDueDate = new DateTime();
        CheckDueDate = Convert.ToDateTime(txtDueDate.Text, dateInfo);
        string _prFxvalid = Request.QueryString["DocPrFx"].Trim();
        hdnTTCurrCheck.Value = "";
        if (_prFxvalid == "EB")
        {
            ValidateTTCurr();
            TTFIRCtotalCaluation();
        }

        if (ddlDispBydefault.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Dispatch Indicator');", true);
            ddlDispBydefault.Focus();

        }
        else if (ddlDispachInd.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Dispatch Indicator');", true);
            ddlDispachInd.Focus();
        }
        else if (_prFxvalid != "EB" && rbtnAfterAWB.Checked == false && rbtnFromAWB.Checked == false && rbtnSight.Checked == false && rbtnDA.Checked == false && rbtnFromInvoice.Checked == false && rbtnOthers.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select tenor bill.');", true);
        }
        else if ((_prFxvalid != "EB" && rbtnAfterAWB.Checked == true || rbtnFromAWB.Checked == true || rbtnSight.Checked == true || rbtnDA.Checked == true || rbtnFromInvoice.Checked == true || rbtnOthers.Checked == true) && (txtOtherTenorRemarks.Text.Trim() == ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Description of tenor should not be blank.');", true);
        }
        //--------------------------Anand 21062023------------------------------------------
        else if ((_prFxvalid != "EB" && rbtnAfterAWB.Checked == true || rbtnFromAWB.Checked == true || rbtnSight.Checked == true || rbtnDA.Checked == true || rbtnFromInvoice.Checked == true || rbtnOthers.Checked == true) && (txtOtherTenorRemarks.Text.Trim() == ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Description of tenor should not be blank.');", true);
        }
        else if (_prFxvalid == "EB" && (txtFIRCNo1_OB.Text.Trim() != "" && txtFIRCADCode1_OB.Text.Trim() == "") || (txtFIRCNo2_OB.Text.Trim() != "" && txtFIRCADCode2_OB.Text.Trim() == "") ||
       (txtFIRCNo3_OB.Text.Trim() != "" && txtFIRCADCode3_OB.Text.Trim() == "") || (txtFIRCNo4_OB.Text.Trim() != "" && txtFIRCADCode4_OB.Text.Trim() == "")
       || (txtFIRCNo5_OB.Text.Trim() != "" && txtFIRCADCode5_OB.Text.Trim() == "") || (txtFIRCNo6_OB.Text.Trim() != "" && txtFIRCADCode6_OB.Text.Trim() == "")
       || (txtFIRCNo7_OB.Text.Trim() != "" && txtFIRCADCode7_OB.Text.Trim() == "") || (txtFIRCNo8_OB.Text.Trim() != "" && txtFIRCADCode8_OB.Text.Trim() == "")
       || (txtFIRCNo9_OB.Text.Trim() != "" && txtFIRCADCode9_OB.Text.Trim() == "") || (txtFIRCNo10_OB.Text.Trim() != "" && txtFIRCADCode10_OB.Text.Trim() == "")
       || (txtFIRCNo11_OB.Text.Trim() != "" && txtFIRCADCode11_OB.Text.Trim() == "") || (txtFIRCNo12_OB.Text.Trim() != "" && txtFIRCADCode12_OB.Text.Trim() == "")
       || (txtFIRCNo13_OB.Text.Trim() != "" && txtFIRCADCode13_OB.Text.Trim() == "") || (txtFIRCNo14_OB.Text.Trim() != "" && txtFIRCADCode14_OB.Text.Trim() == "")
       || (txtFIRCNo15_OB.Text.Trim() != "" && txtFIRCADCode15_OB.Text.Trim() == ""))
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please enter FIRC ADCode.')", true);
            // txtFIRCADCode1_OB.Focus();
        }
        else if (CheckDueDate <= DateTime.Today && _prFxvalid != "EB")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Due Date is Less than Today Day.');", true);
        }
        else if (CheckDueDate < DateTime.Today && _prFxvalid == "EB")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Due Date is Less than Today Day.');", true);
        }
        else if (hdnTTCurrCheck.Value == "TTfalse")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT cross curr rate.');", true);
        }
        else if (hdnTTCurrCheck.Value == "FIRCfalse")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter FIRC cross curr rate.');", true);
        }
        else if (hdnTTFIRCTotalAmtCheck.Value == "Greater")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('TT or FIRC Amount is More Than Bill Amount.');", true);
        }
        else if ((txtTTRefNo1.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency1.SelectedValue && ddlTTRealisedCurr1.SelectedValue == "0") || (txtTTRefNo2.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency2.SelectedValue && ddlTTRealisedCurr2.SelectedValue == "0") ||
            (txtTTRefNo3.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency3.SelectedValue && ddlTTRealisedCurr3.SelectedValue == "0") || (txtTTRefNo4.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency4.SelectedValue && ddlTTRealisedCurr4.SelectedValue == "0") ||
            (txtTTRefNo5.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency5.SelectedValue && ddlTTRealisedCurr5.SelectedValue == "0") || (txtTTRefNo6.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency6.SelectedValue && ddlTTRealisedCurr6.SelectedValue == "0") ||
            (txtTTRefNo7.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency7.SelectedValue && ddlTTRealisedCurr7.SelectedValue == "0") || (txtTTRefNo8.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency8.SelectedValue && ddlTTRealisedCurr8.SelectedValue == "0") ||
            (txtTTRefNo9.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency9.SelectedValue && ddlTTRealisedCurr9.SelectedValue == "0") || (txtTTRefNo10.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency10.SelectedValue && ddlTTRealisedCurr10.SelectedValue == "0") ||
            (txtTTRefNo11.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency11.SelectedValue && ddlTTRealisedCurr11.SelectedValue == "0") || (txtTTRefNo12.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency12.SelectedValue && ddlTTRealisedCurr12.SelectedValue == "0") ||
            (txtTTRefNo13.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency13.SelectedValue && ddlTTRealisedCurr13.SelectedValue == "0") || (txtTTRefNo14.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency14.SelectedValue && ddlTTRealisedCurr14.SelectedValue == "0") ||
            (txtTTRefNo15.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency15.SelectedValue && ddlTTRealisedCurr15.SelectedValue == "0"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT Realised Currency.');", true);
        }
        else if ((txtTTRefNo1.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency1.SelectedValue && (txtTTCrossCurrRate1.Text == "0" || txtTTCrossCurrRate1.Text == "")) ||
            (txtTTRefNo2.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency2.SelectedValue && (txtTTCrossCurrRate2.Text == "0" || txtTTCrossCurrRate2.Text == "")) ||
            (txtTTRefNo3.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency3.SelectedValue && (txtTTCrossCurrRate3.Text == "0" || txtTTCrossCurrRate3.Text == "")) ||
            (txtTTRefNo4.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency4.SelectedValue && (txtTTCrossCurrRate4.Text == "0" || txtTTCrossCurrRate4.Text == "")) ||
            (txtTTRefNo5.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency5.SelectedValue && (txtTTCrossCurrRate5.Text == "0" || txtTTCrossCurrRate5.Text == "")) ||
            (txtTTRefNo6.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency6.SelectedValue && (txtTTCrossCurrRate6.Text == "0" || txtTTCrossCurrRate6.Text == "")) ||
            (txtTTRefNo7.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency7.SelectedValue && (txtTTCrossCurrRate7.Text == "0" || txtTTCrossCurrRate7.Text == "")) ||
            (txtTTRefNo8.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency8.SelectedValue && (txtTTCrossCurrRate8.Text == "0" || txtTTCrossCurrRate8.Text == "")) ||
            (txtTTRefNo9.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency9.SelectedValue && (txtTTCrossCurrRate9.Text == "0" || txtTTCrossCurrRate9.Text == "")) ||
            (txtTTRefNo10.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency10.SelectedValue && (txtTTCrossCurrRate10.Text == "0" || txtTTCrossCurrRate10.Text == "")) ||
            (txtTTRefNo11.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency11.SelectedValue && (txtTTCrossCurrRate11.Text == "0" || txtTTCrossCurrRate11.Text == "")) ||
            (txtTTRefNo12.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency12.SelectedValue && (txtTTCrossCurrRate12.Text == "0" || txtTTCrossCurrRate12.Text == "")) ||
            (txtTTRefNo13.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency13.SelectedValue && (txtTTCrossCurrRate13.Text == "0" || txtTTCrossCurrRate13.Text == "")) ||
            (txtTTRefNo14.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency14.SelectedValue && (txtTTCrossCurrRate14.Text == "0" || txtTTCrossCurrRate14.Text == "")) ||
            (txtTTRefNo15.Text != "" && ddlCurrency.SelectedValue != ddlTTCurrency15.SelectedValue && (txtTTCrossCurrRate15.Text == "0" || txtTTCrossCurrRate15.Text == "")))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter TT Cross Curr. Rate.');", true);
        }
        else if ((txtFIRCNo1_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency1_OB.SelectedValue && ddlFIRCRealisedCurr1_OB.SelectedValue == "0") ||
            (txtFIRCNo2_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency2_OB.SelectedValue && ddlFIRCRealisedCurr2_OB.SelectedValue == "0") ||
            (txtFIRCNo3_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency3_OB.SelectedValue && ddlFIRCRealisedCurr3_OB.SelectedValue == "0") ||
            (txtFIRCNo4_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency4_OB.SelectedValue && ddlFIRCRealisedCurr4_OB.SelectedValue == "0") ||
            (txtFIRCNo5_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency5_OB.SelectedValue && ddlFIRCRealisedCurr5_OB.SelectedValue == "0") ||
            (txtFIRCNo6_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency6_OB.SelectedValue && ddlFIRCRealisedCurr6_OB.SelectedValue == "0") ||
            (txtFIRCNo7_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency7_OB.SelectedValue && ddlFIRCRealisedCurr7_OB.SelectedValue == "0") ||
            (txtFIRCNo8_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency8_OB.SelectedValue && ddlFIRCRealisedCurr8_OB.SelectedValue == "0") ||
            (txtFIRCNo9_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency9_OB.SelectedValue && ddlFIRCRealisedCurr9_OB.SelectedValue == "0") ||
            (txtFIRCNo10_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency10_OB.SelectedValue && ddlFIRCRealisedCurr10_OB.SelectedValue == "0") ||
            (txtFIRCNo11_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency11_OB.SelectedValue && ddlFIRCRealisedCurr11_OB.SelectedValue == "0") ||
            (txtFIRCNo12_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency12_OB.SelectedValue && ddlFIRCRealisedCurr12_OB.SelectedValue == "0") ||
            (txtFIRCNo13_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency13_OB.SelectedValue && ddlFIRCRealisedCurr13_OB.SelectedValue == "0") ||
            (txtFIRCNo14_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency14_OB.SelectedValue && ddlFIRCRealisedCurr14_OB.SelectedValue == "0") ||
            (txtFIRCNo15_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency15_OB.SelectedValue && ddlFIRCRealisedCurr15_OB.SelectedValue == "0"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter FIRC Realised Currency.');", true);
        }
        else if ((txtFIRCNo1_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency1_OB.SelectedValue && (txtFIRCCrossCurrRate1_OB.Text == "0" || txtFIRCCrossCurrRate1_OB.Text == "")) ||
            (txtFIRCNo2_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency2_OB.SelectedValue && (txtFIRCCrossCurrRate2_OB.Text == "0" || txtFIRCCrossCurrRate2_OB.Text == "")) ||
            (txtFIRCNo3_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency3_OB.SelectedValue && (txtFIRCCrossCurrRate3_OB.Text == "0" || txtFIRCCrossCurrRate3_OB.Text == "")) ||
            (txtFIRCNo4_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency4_OB.SelectedValue && (txtFIRCCrossCurrRate4_OB.Text == "0" || txtFIRCCrossCurrRate4_OB.Text == "")) ||
            (txtFIRCNo5_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency5_OB.SelectedValue && (txtFIRCCrossCurrRate5_OB.Text == "0" || txtFIRCCrossCurrRate5_OB.Text == "")) ||
            (txtFIRCNo6_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency6_OB.SelectedValue && (txtFIRCCrossCurrRate6_OB.Text == "0" || txtFIRCCrossCurrRate6_OB.Text == "")) ||
            (txtFIRCNo7_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency7_OB.SelectedValue && (txtFIRCCrossCurrRate7_OB.Text == "0" || txtFIRCCrossCurrRate7_OB.Text == "")) ||
            (txtFIRCNo8_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency8_OB.SelectedValue && (txtFIRCCrossCurrRate8_OB.Text == "0" || txtFIRCCrossCurrRate8_OB.Text == "")) ||
            (txtFIRCNo9_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency9_OB.SelectedValue && (txtFIRCCrossCurrRate9_OB.Text == "0" || txtFIRCCrossCurrRate9_OB.Text == "")) ||
            (txtFIRCNo10_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency10_OB.SelectedValue && (txtFIRCCrossCurrRate10_OB.Text == "0" || txtFIRCCrossCurrRate10_OB.Text == "")) ||
            (txtFIRCNo11_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency11_OB.SelectedValue && (txtFIRCCrossCurrRate11_OB.Text == "0" || txtFIRCCrossCurrRate11_OB.Text == "")) ||
            (txtFIRCNo12_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency12_OB.SelectedValue && (txtFIRCCrossCurrRate12_OB.Text == "0" || txtFIRCCrossCurrRate12_OB.Text == "")) ||
            (txtFIRCNo13_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency13_OB.SelectedValue && (txtFIRCCrossCurrRate13_OB.Text == "0" || txtFIRCCrossCurrRate13_OB.Text == "")) ||
            (txtFIRCNo14_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency14_OB.SelectedValue && (txtFIRCCrossCurrRate14_OB.Text == "0" || txtFIRCCrossCurrRate14_OB.Text == "")) ||
            (txtFIRCNo15_OB.Text != "" && ddlCurrency.SelectedValue != ddlFIRCCurrency15_OB.SelectedValue && (txtFIRCCrossCurrRate15_OB.Text == "0" || txtFIRCCrossCurrRate15_OB.Text == "")))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter FIRC Cross Curr. Rate.');", true);
        }
        else if (GridViewGRPPCustomsDetails.Visible == true && GridViewGRPPCustomsDetails.Rows.Count != Convert.ToInt32(txtNoOfSB.Text) && chkSB.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No. of Shipping bill Mismatch please check pending shipping bill flag.');", true);
        }
        else if (GridViewGRPPCustomsDetails.Visible == false && GridViewGRPPCustomsDetails.Rows.Count - 1 != Convert.ToInt32(txtNoOfSB.Text) && chkSB.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No. of Shipping bill Mismatch please check pending shipping bill flag.');", true);
        }
        else
        {
            if (ddlMercTrade.Text.Trim() == "No")
            {
                if (GridViewGRPPCustomsDetails.Visible == true)
                {
                    //int rowIndex = 0;
                    if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Merchant Trade is No, Shipping Bill is mandatory.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Merchant Trade is No, Shipping Bill is mandatory.');", true);
                }
            }

            string _result = "";
            Boolean _proceed = true;
            string _script = "";
            string _userName = Session["userName"].ToString().Trim();
            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string _mode = Request.QueryString["mode"].Trim();
            string _ForeignOrLocal = Request.QueryString["ForeignOrLocal"].ToString().Trim();
            string _prFx = Request.QueryString["DocPrFx"].Trim();

            if (_prFx == "EB")
            {
                if (txtTTRefNo1.Text == "" && txtFIRCNo1_OB.Text == "")
                {

                    _proceed = false;

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('TTRefno cant be blank for advance.');", true);
                }
            }
            Boolean _proceed1 = true;
            //if (_prFx == "EB")
            //{
            //    double TTAmt = 0;
            //    double BillAmt = 0;
            //    if (txtBillAmount.Text != "")
            //    {
            //        BillAmt = Convert.ToDouble(txtBillAmount.Text);
            //    }
            //    else
            //    {

            //        BillAmt = 0;
            //    }
            //    if (hdnTTTotalAmt.Value != "")
            //    {
            //        TTAmt = Convert.ToDouble(hdnTTTotalAmt.Value);

            //    }
            //    else
            //    {
            //        TTAmt = 0;

            //    }
            //    _proceed1 = false;
            //    if (TTAmt > BillAmt)
            //    {
            //        _proceed1 = false;

            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('TT Amount More Than Bill Amount.');", true);
            //    }
            //    else
            //    {
            //        _proceed1 = true;

            //    }
            //  }
            getLastDocNo();
            TF_DATA objCheck = new TF_DATA();
            string _queryCheck = "TF_EXP_CheckShippingBillNo";
            SqlParameter cShippingBillNo = new SqlParameter("@shippingBillNo", SqlDbType.VarChar);
            SqlParameter cDocumentNo = new SqlParameter("@documentNo", SqlDbType.VarChar);
            cDocumentNo.Value = txtDocumentNo.Text;

            SqlParameter cShippingBillDate = new SqlParameter("@ShippingBillDate", SqlDbType.VarChar);
            SqlParameter cPort = new SqlParameter("@PortCode", SqlDbType.VarChar);
            SqlParameter cBranchCode = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            cBranchCode.Value = Request.QueryString["BranchCode"].Trim();

            if (GridViewGRPPCustomsDetails.Rows.Count >= 0)
            {
                for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
                {
                    Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[4].FindControl("lblShippingBillNo");
                    cShippingBillNo.Value = lbl5.Text;

                    Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[5].FindControl("lblShippingBillDate");
                    cShippingBillDate.Value = lbl6.Text;

                    Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[15].FindControl("lblPortCode");
                    cPort.Value = lbl7.Text;

                    if (lbl5.Text != "")
                    {
                        DataTable dtcheck = objCheck.getData(_queryCheck, cShippingBillNo, cDocumentNo, cShippingBillDate, cPort, cBranchCode);
                        if (dtcheck.Rows.Count > 0)
                        {
                            _proceed = false;
                            _script = "alert('Shipping Bill No: " + lbl5.Text + " is assigned to " + dtcheck.Rows[0]["Document_No"].ToString() + "')";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            // txtRefNo.Focus();
                        }
                    }
                }
            }
            //if (_proceed == true)
            //{
            //    if (hdnIsRealised.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already Realised, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}
            //if (_proceed == true)
            //{
            //    if (hdnIsDelinked.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already Delinked, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}
            //if (_proceed == true)
            //{
            //    if (hdnIsWrittenOff.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already WrittenOff, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}
            //if (_proceed == true)
            //{
            //    if (hdnIsExtended.Value == "YES")
            //    {
            //        _proceed = false;
            //        _script = "alert('This Document is already Entended, You cannot update.')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //    }
            //}

            if (_proceed1 == true)
            {
                if (_proceed == true)
                {
                    SqlParameter bCode = new SqlParameter("@bCode", SqlDbType.VarChar);
                    bCode.Value = Request.QueryString["BranchCode"].Trim();

                    SqlParameter docNumber = new SqlParameter("@docNo", SqlDbType.VarChar);
                    docNumber.Value = txtDocumentNo.Text.Trim();

                    SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
                    p1.Value = _mode;

                    SqlParameter p2 = new SqlParameter("@docPrfx", SqlDbType.VarChar);
                    p2.Value = Request.QueryString["DocPrFx"].Trim();

                    SqlParameter p3 = new SqlParameter("@docSrNo", SqlDbType.VarChar);
                    p3.Value = Request.QueryString["DocSrNo"].Trim();

                    SqlParameter p4 = new SqlParameter("@docNoYear", SqlDbType.VarChar);
                    p4.Value = Request.QueryString["DocYear"].Trim();

                    SqlParameter p5 = new SqlParameter("@billType", SqlDbType.VarChar);
                    string _billType = "";
                    if (rbtnSightBill.Checked)
                        _billType = "S";
                    else if (rbtnUsanceBill.Checked)
                        _billType = "U";
                    p5.Value = _billType;

                    SqlParameter pCustAcNo = new SqlParameter("@custAcNo", SqlDbType.VarChar);
                    pCustAcNo.Value = txtCustAcNo.Text.Trim();

                    SqlParameter pOverseasPartyID = new SqlParameter("@OverseasPartyID", SqlDbType.VarChar);
                    pOverseasPartyID.Value = txtOverseasPartyID.Text.Trim();

                    SqlParameter pOverseasBankID = new SqlParameter("@OverseasBankID", SqlDbType.VarChar);
                    pOverseasBankID.Value = txtOverseasBankID.Text.Trim();

                    SqlParameter pCustCheck = new SqlParameter("@custCheck", "");
                    //string _CustCheck = "B";
                    //if (chkCustCheck.Checked)
                    //    _CustCheck = "C";
                    //pCustCheck.Value = _CustCheck;

                    SqlParameter pCS = new SqlParameter("@cs", "");
                    //string _CS = "N";
                    //if (chkCS.Checked)
                    //    _CS = "Y";
                    //pCS.Value = _CS;

                    SqlParameter pDateRcvd = new SqlParameter("@dateRcvd", SqlDbType.VarChar);
                    pDateRcvd.Value = txtDateRcvd.Text.Trim();

                    SqlParameter pENCdate = new SqlParameter("@encDate", SqlDbType.VarChar);
                    pENCdate.Value = txtENCdate.Text.Trim();

                    SqlParameter pDateNegotiated = new SqlParameter("@dateNegaotiated", SqlDbType.VarChar);
                    pDateNegotiated.Value = txtDateNegotiated.Text.Trim();

                    SqlParameter pLCNo = new SqlParameter("@LCNo", SqlDbType.VarChar);
                    pLCNo.Value = txtLCNo.Text.Trim();

                    SqlParameter pLCDate = new SqlParameter("@LCDate", SqlDbType.VarChar);
                    pLCDate.Value = txtLCNoDate.Text.Trim();

                    SqlParameter pLCissuedBy = new SqlParameter("@LCissuedBy", SqlDbType.VarChar);
                    pLCissuedBy.Value = txtLCNoIssuedBy.Text.Trim();

                    SqlParameter pBENo = new SqlParameter("@BENo", SqlDbType.VarChar);
                    pBENo.Value = txtBENo.Text.Trim();

                    SqlParameter pBEDate = new SqlParameter("@BEDate", SqlDbType.VarChar);
                    pBEDate.Value = txtBEDate.Text.Trim();

                    SqlParameter pBEdoc = new SqlParameter("@BEdoc", SqlDbType.VarChar);
                    pBEdoc.Value = txtBENoDoc.Text.Trim();

                    SqlParameter pbankLine = new SqlParameter("@bankLine", SqlDbType.VarChar);
                    string _bankLine = "N";
                    if (chkBankLineTransfer.Checked)
                        _bankLine = "Y";
                    pbankLine.Value = _bankLine;

                    SqlParameter pAccpDate = new SqlParameter("@AccpDate", SqlDbType.VarChar);
                    pAccpDate.Value = txtAccpDate.Text.Trim();

                    SqlParameter pInvoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
                    pInvoiceNo.Value = txtInvoiceNo.Text.Trim();


                    SqlParameter pInvoiceDate = new SqlParameter("@invoiceDate", SqlDbType.VarChar);
                    pInvoiceDate.Value = txtInvoiceDate.Text.Trim();


                    SqlParameter pInvoiceDoc = new SqlParameter("@InvoiceDoc", SqlDbType.VarChar);
                    pInvoiceDoc.Value = txtInvoiceDoc.Text.Trim();

                    SqlParameter pAWBno = new SqlParameter("@AWBno", SqlDbType.VarChar);
                    pAWBno.Value = txtAWBno.Text.Trim();

                    SqlParameter pAWBdate = new SqlParameter("@AWBdate", SqlDbType.VarChar);
                    pAWBdate.Value = txtAWBDate.Text.Trim();

                    SqlParameter pAWBissuedBy = new SqlParameter("@AWBissuedBy", SqlDbType.VarChar);
                    pAWBissuedBy.Value = txtAwbIssuedBy.Text.Trim();

                    SqlParameter pAWBdoc = new SqlParameter("@AWBdoc", SqlDbType.VarChar);
                    pAWBdoc.Value = txtAWBDoc.Text.Trim();

                    SqlParameter pPackingList = new SqlParameter("@PackingList", SqlDbType.VarChar);
                    pPackingList.Value = txtPackingList.Text.Trim();

                    SqlParameter pPackingListDate = new SqlParameter("@PackingListDate", SqlDbType.VarChar);
                    pPackingListDate.Value = txtPackingListDate.Text.Trim();

                    SqlParameter pPackingListDoc = new SqlParameter("@PackingListDoc", SqlDbType.VarChar);
                    pPackingListDoc.Value = txtPackingDoc.Text.Trim();

                    SqlParameter pCertOfOrigin = new SqlParameter("@certOfOrigin", SqlDbType.VarChar);
                    pCertOfOrigin.Value = txtCertOfOrigin.Text.Trim();

                    SqlParameter pCertOfOriginIssuedBy = new SqlParameter("@certOfOriginIssuedBy", SqlDbType.VarChar);
                    pCertOfOriginIssuedBy.Value = txtCertIssuedBy.Text.Trim();

                    SqlParameter pCertOfOriginDoc = new SqlParameter("@certOfOriginDoc", SqlDbType.VarChar);
                    pCertOfOriginDoc.Value = txtCertOfOriginDoc.Text.Trim();

                    SqlParameter pCustomsInvoice = new SqlParameter("@CustomsInvoice", SqlDbType.VarChar);
                    pCustomsInvoice.Value = txtCustomsInvoice.Text.Trim();

                    SqlParameter pCustomsInvoiceDate = new SqlParameter("@CustomsInvoiveDate", SqlDbType.VarChar);
                    pCustomsInvoiceDate.Value = txtCustomsDate.Text.Trim();

                    SqlParameter pCustomsInvoiceDoc = new SqlParameter("@CustomsInvoiveDoc", SqlDbType.VarChar);
                    pCustomsInvoiceDoc.Value = txtCustomsDoc.Text.Trim();

                    SqlParameter pCommodity = new SqlParameter("@Commodity", SqlDbType.VarChar);
                    pCommodity.Value = txtCommodityID.Text.Trim();

                    SqlParameter pInsPolicy = new SqlParameter("@InsPolicy", SqlDbType.VarChar);
                    pInsPolicy.Value = txtInsPolicy.Text.Trim();

                    SqlParameter pInsPolicyDate = new SqlParameter("@InsPolicyDate", SqlDbType.VarChar);
                    pInsPolicyDate.Value = txtInsPolicyDate.Text.Trim();

                    SqlParameter pInsPolicyIssuedBy = new SqlParameter("@InsPolicyIssuedBy", SqlDbType.VarChar);
                    pInsPolicyIssuedBy.Value = txtInsPolicyIssuedBy.Text.Trim();

                    SqlParameter pInsPolicydoc = new SqlParameter("@InsPolicyDoc", SqlDbType.VarChar);
                    pInsPolicydoc.Value = txtInsPolicyDoc.Text.Trim();

                    SqlParameter pGSPdate = new SqlParameter("@GspDate", SqlDbType.VarChar);
                    pGSPdate.Value = txtGSPDate.Text.Trim();

                    SqlParameter pContractNo = new SqlParameter("@ContractNo", SqlDbType.VarChar);
                    pContractNo.Value = txtContractNo.Text.Trim();

                    SqlParameter pContractRate = new SqlParameter("@ContractRate", SqlDbType.VarChar);
                    pContractRate.Value = txtRate.Text.Trim();

                    SqlParameter pGSPdoc = new SqlParameter("@GspDoc", SqlDbType.VarChar);
                    pGSPdoc.Value = txtGSPDoc.Text.Trim();

                    SqlParameter pFIRCno = new SqlParameter("@FIRCno", SqlDbType.VarChar);
                    pFIRCno.Value = txtFIRCno.Text.Trim();

                    SqlParameter pFIRCdate = new SqlParameter("@FIRCDate", SqlDbType.VarChar);
                    pFIRCdate.Value = txtFIRCdate.Text.Trim();

                    SqlParameter pFIRCissuedBy = new SqlParameter("@FIRCIssuedBy", SqlDbType.VarChar);
                    pFIRCissuedBy.Value = txtFIRCnoIssuedBy.Text.Trim();

                    SqlParameter pFIRCdoc = new SqlParameter("@FIRCDoc", SqlDbType.VarChar);
                    pFIRCdoc.Value = txtFIRCdoc.Text.Trim();

                    SqlParameter pMisc = new SqlParameter("@Miscellaneous", SqlDbType.VarChar);
                    pMisc.Value = txtMiscellaneous.Text.Trim();

                    SqlParameter pMiscDoc = new SqlParameter("@MiscellaneousDoc", SqlDbType.VarChar);
                    pMiscDoc.Value = txtMiscDoc.Text.Trim();

                    SqlParameter pShipment = new SqlParameter("@Steamer", SqlDbType.VarChar);
                    string _Shipment = "";
                    if (rdbByAir.Checked)
                        _Shipment = "A";
                    else if (rdbBySea.Checked)
                        _Shipment = "S";
                    else if (rdbByRoad.Checked)
                        _Shipment = "R";
                    pShipment.Value = _Shipment;

                    //SqlParameter pCoveringFrom = new SqlParameter("@CoveringFrom", SqlDbType.VarChar);
                    //pCoveringFrom.Value = txtCoveringFrom.Text.Trim();

                    SqlParameter pCoveringTo = new SqlParameter("@CoveringTo", SqlDbType.VarChar);
                    pCoveringTo.Value = txtCoveringTo.Text.Trim();

                    SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);

                    pCountryCode.Value = txtCountry.Text.Trim();

                    SqlParameter preimbValDate = new SqlParameter("@reimbValDate", SqlDbType.VarChar);
                    preimbValDate.Value = txtReimbValDate.Text.Trim();

                    SqlParameter preimbClaimDate = new SqlParameter("@reimbClaimDate", SqlDbType.VarChar);
                    preimbClaimDate.Value = txtReimbClaimDate.Text.Trim();


                    SqlParameter preimbBankName = new SqlParameter("@reimbBankName", SqlDbType.VarChar);
                    preimbBankName.Value = txtBkName.Text.Trim();

                    SqlParameter preimbBankBICcode = new SqlParameter("@reimbBankBICcode", SqlDbType.VarChar);
                    preimbBankBICcode.Value = txtBIC.Text.Trim();

                    SqlParameter preimbAmount = new SqlParameter("@reimbAmount", SqlDbType.VarChar);
                    preimbAmount.Value = txtReimbAmt.Text.Trim();

                    //SqlParameter pttrefNo = new SqlParameter("@ttrefNo", SqlDbType.VarChar);
                    //if (txtTTdocNo.Text != "")
                    //{
                    //    pttrefNo.Value = txtTTprFx.Text + "/" + txtTTdocNo.Text.Trim() + "/" + txtTTbranchCode.Text.Trim() + "/" + txtTTyear.Text.Trim();
                    //}
                    //else
                    //    pttrefNo.Value = "";

                    SqlParameter pNotes = new SqlParameter("@notes", SqlDbType.VarChar);
                    pNotes.Value = txtNotes.Text.Trim();

                    SqlParameter pCurr = new SqlParameter("@Curr", SqlDbType.VarChar);
                    if (ddlCurrency.SelectedIndex > 0)
                        pCurr.Value = ddlCurrency.SelectedValue.Trim();
                    else
                        pCurr.Value = "";

                    SqlParameter pLoanAdv = new SqlParameter("@loanAdv", SqlDbType.VarChar);
                    string _loanAdv = "N";
                    if (chkLoanAdv.Checked)
                        _loanAdv = "Y";
                    pLoanAdv.Value = _loanAdv;

                    SqlParameter pOthCurr = new SqlParameter("@OthCurr", SqlDbType.VarChar);
                    if (ddlCurrency.Text == "INR")
                    {
                        if (ddlOtherCurrency.SelectedIndex > 0)
                            pOthCurr.Value = ddlOtherCurrency.SelectedValue.Trim();
                        else
                            pOthCurr.Value = "";
                    }
                    else
                        pOthCurr.Value = pCurr.Value;

                    SqlParameter pExchRT = new SqlParameter("@exchRt", SqlDbType.VarChar);
                    pExchRT.Value = txtExchRate.Text.Trim();

                    SqlParameter pNoOfDays = new SqlParameter("@noOfDays", SqlDbType.VarChar);
                    pNoOfDays.Value = txtNoOfDays.Text.Trim();

                    SqlParameter pAfterFrom = new SqlParameter("@afterFrom", SqlDbType.VarChar);
                    string _AfterFrom = "";
                    if (rbtnAfterAWB.Checked == true)
                        _AfterFrom = "A";
                    if (rbtnFromAWB.Checked == true)
                        _AfterFrom = "F";
                    if (rbtnSight.Checked == true)
                        _AfterFrom = "X";
                    if (rbtnDA.Checked == true)
                        _AfterFrom = "Y";
                    if (rbtnFromInvoice.Checked == true)
                        _AfterFrom = "I";
                    if (rbtnOthers.Checked == true)
                        _AfterFrom = "O";
                    pAfterFrom.Value = _AfterFrom;

                    SqlParameter pLibor = new SqlParameter("@libor", SqlDbType.VarChar);
                    pLibor.Value = txtLibor.Text.Trim();

                    SqlParameter pOutOfDays = new SqlParameter("@outOfDays", SqlDbType.VarChar);
                    pOutOfDays.Value = txtOutOfDays.Text.Trim();

                    SqlParameter pDueDate = new SqlParameter("@DueDate", SqlDbType.VarChar);
                    pDueDate.Value = txtDueDate.Text.Trim();

                    SqlParameter pAccpDueDate = new SqlParameter("@AccpDueDate", SqlDbType.VarChar);
                    pAccpDueDate.Value = txtAcceptedDueDate.Text.Trim();

                    SqlParameter pOtherRemarks = new SqlParameter("@otherRemarks", SqlDbType.VarChar);
                    pOtherRemarks.Value = txtOtherTenorRemarks.Text.Trim();

                    SqlParameter pBillAmt = new SqlParameter("@ActbillAmt", SqlDbType.VarChar);
                    pBillAmt.Value = txtBillAmount.Text.Trim();

                    SqlParameter pBillAmtinRS = new SqlParameter("@ActbillAmtinRS", SqlDbType.VarChar);
                    pBillAmtinRS.Value = txtBillAmountinRS.Text.Trim();

                    SqlParameter pNegotiated = new SqlParameter("@NegotiatedAmt", SqlDbType.VarChar);
                    pNegotiated.Value = txtNegotiatedAmt.Text.Trim();

                    SqlParameter pNegotiatedinRS = new SqlParameter("@NegotiatedAmtinRS", SqlDbType.VarChar);
                    pNegotiatedinRS.Value = txtNegotiatedAmtinRS.Text.Trim();

                    SqlParameter pInterest = new SqlParameter("@interest", SqlDbType.VarChar);
                    pInterest.Value = txtInterest.Text.Trim();

                    SqlParameter pInterestinRS = new SqlParameter("@interestinRS", SqlDbType.VarChar);
                    pInterestinRS.Value = txtInterestinRS.Text.Trim();

                    SqlParameter pNetAmt = new SqlParameter("@NetAmt", SqlDbType.VarChar);
                    pNetAmt.Value = txtNetAmt.Text.Trim();

                    SqlParameter pNetAmtinRS = new SqlParameter("@NetAmtinRS", SqlDbType.VarChar);
                    pNetAmtinRS.Value = txtNetAmtinRS.Text.Trim();

                    SqlParameter pExchRtEBR = new SqlParameter("@exchRtEBR", SqlDbType.VarChar);
                    pExchRtEBR.Value = txtExchRtEBR.Text.Trim();

                    SqlParameter pOtherCharges = new SqlParameter("@otherChrgs", SqlDbType.VarChar);
                    pOtherCharges.Value = txtOtherChrgs.Text.Trim();

                    SqlParameter pSTFXDLS = new SqlParameter("@STFXDLS", SqlDbType.VarChar);
                    pSTFXDLS.Value = txtSTFXDLS.Text.Trim();

                    SqlParameter pBankCert = new SqlParameter("@bankCert", SqlDbType.VarChar);
                    pBankCert.Value = txtBankCert.Text.Trim();

                    SqlParameter pNegotiationFees = new SqlParameter("@negotiationFees", SqlDbType.VarChar);
                    pNegotiationFees.Value = txtNegotiationFees.Text.Trim();

                    SqlParameter pCourierCharges = new SqlParameter("@CourierChrgs", SqlDbType.VarChar);
                    pCourierCharges.Value = txtCourierChrgs.Text.Trim();

                    SqlParameter pMarginAmount = new SqlParameter("@MarginAmt", SqlDbType.VarChar);
                    pMarginAmount.Value = txtMarginAmt.Text.Trim();

                    SqlParameter pCommission = new SqlParameter("@Commission", SqlDbType.VarChar);
                    pCommission.Value = txtCommission.Text.Trim();

                    SqlParameter psTax = new SqlParameter("@sTax", SqlDbType.VarChar);
                    psTax.Value = ddlServiceTax.Text.Trim();

                    SqlParameter psAmt = new SqlParameter("@sTaxAmt", SqlDbType.VarChar);
                    psAmt.Value = txtSTaxAmount.Text.Trim();

                    SqlParameter pCurrentAcinRS = new SqlParameter("@CurrentAcinRS", SqlDbType.VarChar);
                    pCurrentAcinRS.Value = txtCurrentAcinRS.Text.Trim();

                    SqlParameter pcb1 = new SqlParameter("@cb1", SqlDbType.VarChar);
                    string bit_chk1 = "0";
                    if (chk1.Checked)
                        bit_chk1 = "1";
                    pcb1.Value = bit_chk1;

                    SqlParameter pcb2 = new SqlParameter("@cb2", SqlDbType.VarChar);
                    string bit_chk2 = "0";
                    if (chk2.Checked)
                        bit_chk2 = "1";
                    pcb2.Value = bit_chk2;

                    SqlParameter pcb3 = new SqlParameter("@cb3", SqlDbType.VarChar);
                    string bit_chk3 = "0";
                    if (chk3.Checked)
                        bit_chk3 = "1";
                    pcb3.Value = bit_chk3;

                    SqlParameter pcb4 = new SqlParameter("@cb4", SqlDbType.VarChar);
                    string bit_chk4 = "0";
                    if (chk4.Checked)
                        bit_chk4 = "1";
                    pcb4.Value = bit_chk4;

                    SqlParameter pcb5 = new SqlParameter("@cb5", SqlDbType.VarChar);
                    string bit_chk5 = "0";
                    if (chk5.Checked)
                        bit_chk5 = "1";
                    pcb5.Value = bit_chk5;

                    SqlParameter pcb6 = new SqlParameter("@cb6", SqlDbType.VarChar);
                    string bit_chk6 = "0";
                    if (chk6.Checked)
                        bit_chk6 = "1";
                    pcb6.Value = bit_chk6;

                    SqlParameter pcb7 = new SqlParameter("@cb7", SqlDbType.VarChar);
                    string bit_chk7 = "0";
                    if (chk7.Checked)
                        bit_chk7 = "1";
                    pcb7.Value = bit_chk7;

                    SqlParameter pcb7A = new SqlParameter("@cb7A", SqlDbType.VarChar);
                    string bit_chk7A = "0";
                    if (chk7A.Checked)
                        bit_chk7A = "1";
                    pcb7A.Value = bit_chk7A;

                    SqlParameter pcb7B = new SqlParameter("@cb7B", SqlDbType.VarChar);
                    string bit_chk7B = "0";
                    if (chk7B.Checked)
                        bit_chk7B = "1";
                    pcb7B.Value = bit_chk7B;

                    SqlParameter pcb8 = new SqlParameter("@cb8", SqlDbType.VarChar);
                    string bit_chk8 = "0";
                    if (chk8.Checked)
                        bit_chk8 = "1";
                    pcb8.Value = bit_chk8;

                    SqlParameter pcb9 = new SqlParameter("@cb9", SqlDbType.VarChar);
                    string bit_chk9 = "0";
                    if (chk9.Checked)
                        bit_chk9 = "1";
                    pcb9.Value = bit_chk9;

                    SqlParameter pcb10 = new SqlParameter("@cb10", SqlDbType.VarChar);
                    string bit_chk10 = "0";
                    if (chk10.Checked)
                        bit_chk10 = "1";
                    pcb10.Value = bit_chk10;

                    SqlParameter pcb11 = new SqlParameter("@cb11", SqlDbType.VarChar);
                    string bit_chk11 = "0";
                    if (chk11.Checked)
                        bit_chk11 = "1";
                    pcb11.Value = bit_chk11;

                    SqlParameter pcb12 = new SqlParameter("@cb12", SqlDbType.VarChar);
                    string bit_chk12 = "0";
                    if (chk12.Checked)
                        bit_chk12 = "1";
                    pcb12.Value = bit_chk12;

                    SqlParameter pcb13 = new SqlParameter("@cb13", SqlDbType.VarChar);
                    string bit_chk13 = "0";
                    if (chk13.Checked)
                        bit_chk13 = "1";
                    pcb13.Value = bit_chk13;

                    SqlParameter pRemarks = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    pRemarks.Value = txtRemark.Text.Trim();

                    SqlParameter pRemarks1 = new SqlParameter("@Remarks1", SqlDbType.VarChar);
                    pRemarks1.Value = txtRemarks1.Text.Trim();

                    SqlParameter pIsManualGR = new SqlParameter("@IsManualGR", SqlDbType.VarChar);
                    string bit_IsManualGR = "0";
                    if (chkManualGR.Checked)
                        bit_IsManualGR = "1";
                    pIsManualGR.Value = bit_IsManualGR;

                    SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
                    pUser.Value = _userName;

                    SqlParameter pUploadDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
                    pUploadDate.Value = _uploadingDate;

                    SqlParameter pchkRBI = new SqlParameter("@chkRBI", SqlDbType.VarChar);
                    string _chkRBI = "False";
                    if (chkRBI.Checked)
                        _chkRBI = "True";
                    pchkRBI.Value = _chkRBI;

                    SqlParameter pTTRef1 = new SqlParameter("@TTRef1", txtTTRefNo1.Text.Trim());
                    SqlParameter pTTAmt1 = new SqlParameter("@TTAmt1", txtTTAmount1.Text.Trim());

                    SqlParameter pTTRef2 = new SqlParameter("@TTRef2", txtTTRefNo2.Text.Trim());
                    SqlParameter pTTAmt2 = new SqlParameter("@TTAmt2", txtTTAmount2.Text.Trim());

                    SqlParameter pTTRef3 = new SqlParameter("@TTRef3", txtTTRefNo3.Text.Trim());
                    SqlParameter pTTAmt3 = new SqlParameter("@TTAmt3", txtTTAmount3.Text.Trim());

                    SqlParameter pTTRef4 = new SqlParameter("@TTRef4", txtTTRefNo4.Text.Trim());
                    SqlParameter pTTAmt4 = new SqlParameter("@TTAmt4", txtTTAmount4.Text.Trim());

                    SqlParameter pTTRef5 = new SqlParameter("@TTRef5", txtTTRefNo5.Text.Trim());
                    SqlParameter pTTAmt5 = new SqlParameter("@TTAmt5", txtTTAmount5.Text.Trim());

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

                    SqlParameter pIntRate1 = new SqlParameter("@interestRate1", SqlDbType.VarChar);
                    pIntRate1.Value = txtIntRate1.Text.Trim();

                    SqlParameter pForDays1 = new SqlParameter("@forDays1", SqlDbType.VarChar);
                    pForDays1.Value = txtForDays1.Text.Trim();

                    SqlParameter pIntRate2 = new SqlParameter("@interestRate2", SqlDbType.VarChar);
                    pIntRate2.Value = txtIntRate2.Text.Trim();

                    SqlParameter pForDays2 = new SqlParameter("@forDays2", SqlDbType.VarChar);
                    pForDays2.Value = txtForDays2.Text.Trim();

                    SqlParameter pIntRate3 = new SqlParameter("@interestRate3", txtIntRate3.Text.Trim());
                    SqlParameter pForDays3 = new SqlParameter("@forDays3", txtForDays3.Text.Trim());

                    SqlParameter pIntRate4 = new SqlParameter("@interestRate4", txtIntRate4.Text.Trim());
                    SqlParameter pForDays4 = new SqlParameter("@forDays4", txtForDays4.Text.Trim());

                    SqlParameter pIntRate5 = new SqlParameter("@interestRate5", txtIntRate5.Text.Trim());
                    SqlParameter pForDays5 = new SqlParameter("@forDays5", txtForDays5.Text.Trim());

                    SqlParameter pIntRate6 = new SqlParameter("@interestRate6", txtIntRate6.Text.Trim());
                    SqlParameter pForDays6 = new SqlParameter("@forDays6", txtForDays6.Text.Trim());

                    SqlParameter pIntFDate1 = new SqlParameter("@intFDate1", txtIntFrmDate1.Text.Trim());
                    SqlParameter pIntTDate1 = new SqlParameter("@intTDate1", txtIntToDate1.Text.Trim());

                    SqlParameter pIntFDate2 = new SqlParameter("@intFDate2", txtIntFrmDate2.Text.Trim());
                    SqlParameter pIntTDate2 = new SqlParameter("@intTDate2", txtIntToDate2.Text.Trim());

                    SqlParameter pIntFDate3 = new SqlParameter("@intFDate3", txtIntFrmDate3.Text.Trim());
                    SqlParameter pIntTDate3 = new SqlParameter("@intTDate3", txtIntToDate3.Text.Trim());

                    SqlParameter pIntFDate4 = new SqlParameter("@intFDate4", txtIntFrmDate4.Text.Trim());
                    SqlParameter pIntTDate4 = new SqlParameter("@intTDate4", txtIntToDate4.Text.Trim());

                    SqlParameter pIntFDate5 = new SqlParameter("@intFDate5", txtIntFrmDate5.Text.Trim());
                    SqlParameter pIntTDate5 = new SqlParameter("@intTDate5", txtIntToDate5.Text.Trim());

                    SqlParameter pIntFDate6 = new SqlParameter("@intFDate6", txtIntFrmDate6.Text.Trim());
                    SqlParameter pIntTDate6 = new SqlParameter("@intTDate6", txtIntToDate6.Text.Trim());

                    SqlParameter pCommissionID = new SqlParameter("@CommissionID", txtCommissionID.Text.Trim());

                    SqlParameter pFIRCNo1_OB = new SqlParameter("@FIRCNo1_OB", txtFIRCNo1_OB.Text.Trim());
                    SqlParameter pFIRCDate1_OB = new SqlParameter("@FIRCDate1_OB", txtFIRCDate1_OB.Text.Trim());
                    SqlParameter pFIRCAmt1_OB = new SqlParameter("@FIRCAmt1_OB", txtFIRCAmount1_OB.Text.Trim());
                    SqlParameter pFIRCADCode1_OB = new SqlParameter("@FIRCADCode1_OB", txtFIRCADCode1_OB.Text.Trim());

                    SqlParameter pFIRCNo2_OB = new SqlParameter("@FIRCNo2_OB", txtFIRCNo2_OB.Text.Trim());
                    SqlParameter pFIRCDate2_OB = new SqlParameter("@FIRCDate2_OB", txtFIRCDate2_OB.Text.Trim());
                    SqlParameter pFIRCAmt2_OB = new SqlParameter("@FIRCAmt2_OB", txtFIRCAmount2_OB.Text.Trim());
                    SqlParameter pFIRCADCode2_OB = new SqlParameter("@FIRCADCode2_OB", txtFIRCADCode2_OB.Text.Trim());

                    SqlParameter pFIRCNo3_OB = new SqlParameter("@FIRCNo3_OB", txtFIRCNo3_OB.Text.Trim());
                    SqlParameter pFIRCDate3_OB = new SqlParameter("@FIRCDate3_OB", txtFIRCDate3_OB.Text.Trim());
                    SqlParameter pFIRCAmt3_OB = new SqlParameter("@FIRCAmt3_OB", txtFIRCAmount3_OB.Text.Trim());
                    SqlParameter pFIRCADCode3_OB = new SqlParameter("@FIRCADCode3_OB", txtFIRCADCode3_OB.Text.Trim());

                    SqlParameter pFIRCNo4_OB = new SqlParameter("@FIRCNo4_OB", txtFIRCNo4_OB.Text.Trim());
                    SqlParameter pFIRCDate4_OB = new SqlParameter("@FIRCDate4_OB", txtFIRCDate4_OB.Text.Trim());
                    SqlParameter pFIRCAmt4_OB = new SqlParameter("@FIRCAmt4_OB", txtFIRCAmount4_OB.Text.Trim());
                    SqlParameter pFIRCADCode4_OB = new SqlParameter("@FIRCADCode4_OB", txtFIRCADCode4_OB.Text.Trim());


                    SqlParameter pFIRCNo5_OB = new SqlParameter("@FIRCNo5_OB", txtFIRCNo5_OB.Text.Trim());
                    SqlParameter pFIRCDate5_OB = new SqlParameter("@FIRCDate5_OB", txtFIRCDate5_OB.Text.Trim());
                    SqlParameter pFIRCAmt5_OB = new SqlParameter("@FIRCAmt5_OB", txtFIRCAmount5_OB.Text.Trim());
                    SqlParameter pFIRCADCode5_OB = new SqlParameter("@FIRCADCode5_OB", txtFIRCADCode5_OB.Text.Trim());


                    SqlParameter pFIRCNo6_OB = new SqlParameter("@FIRCNo6_OB", txtFIRCNo6_OB.Text.Trim());
                    SqlParameter pFIRCDate6_OB = new SqlParameter("@FIRCDate6_OB", txtFIRCDate6_OB.Text.Trim());
                    SqlParameter pFIRCAmt6_OB = new SqlParameter("@FIRCAmt6_OB", txtFIRCAmount6_OB.Text.Trim());
                    SqlParameter pFIRCADCode6_OB = new SqlParameter("@FIRCADCode6_OB", txtFIRCADCode6_OB.Text.Trim());


                    SqlParameter pFIRCNo7_OB = new SqlParameter("@FIRCNo7_OB", txtFIRCNo7_OB.Text.Trim());
                    SqlParameter pFIRCDate7_OB = new SqlParameter("@FIRCDate7_OB", txtFIRCDate7_OB.Text.Trim());
                    SqlParameter pFIRCAmt7_OB = new SqlParameter("@FIRCAmt7_OB", txtFIRCAmount7_OB.Text.Trim());
                    SqlParameter pFIRCADCode7_OB = new SqlParameter("@FIRCADCode7_OB", txtFIRCADCode7_OB.Text.Trim());


                    SqlParameter pFIRCNo8_OB = new SqlParameter("@FIRCNo8_OB", txtFIRCNo8_OB.Text.Trim());
                    SqlParameter pFIRCDate8_OB = new SqlParameter("@FIRCDate8_OB", txtFIRCDate8_OB.Text.Trim());
                    SqlParameter pFIRCAmt8_OB = new SqlParameter("@FIRCAmt8_OB", txtFIRCAmount8_OB.Text.Trim());
                    SqlParameter pFIRCADCode8_OB = new SqlParameter("@FIRCADCode8_OB", txtFIRCADCode8_OB.Text.Trim());

                    SqlParameter pFIRCNo9_OB = new SqlParameter("@FIRCNo9_OB", txtFIRCNo9_OB.Text.Trim());
                    SqlParameter pFIRCDate9_OB = new SqlParameter("@FIRCDate9_OB", txtFIRCDate9_OB.Text.Trim());
                    SqlParameter pFIRCAmt9_OB = new SqlParameter("@FIRCAmt9_OB", txtFIRCAmount9_OB.Text.Trim());
                    SqlParameter pFIRCADCode9_OB = new SqlParameter("@FIRCADCode9_OB", txtFIRCADCode9_OB.Text.Trim());

                    SqlParameter pFIRCNo10_OB = new SqlParameter("@FIRCNo10_OB", txtFIRCNo10_OB.Text.Trim());
                    SqlParameter pFIRCDate10_OB = new SqlParameter("@FIRCDate10_OB", txtFIRCDate10_OB.Text.Trim());
                    SqlParameter pFIRCAmt10_OB = new SqlParameter("@FIRCAmt10_OB", txtFIRCAmount10_OB.Text.Trim());
                    SqlParameter pFIRCADCode10_OB = new SqlParameter("@FIRCADCode10_OB", txtFIRCADCode10_OB.Text.Trim());

                    SqlParameter pFIRCNo11_OB = new SqlParameter("@FIRCNo11_OB", txtFIRCNo11_OB.Text.Trim());
                    SqlParameter pFIRCDate11_OB = new SqlParameter("@FIRCDate11_OB", txtFIRCDate11_OB.Text.Trim());
                    SqlParameter pFIRCAmt11_OB = new SqlParameter("@FIRCAmt11_OB", txtFIRCAmount11_OB.Text.Trim());
                    SqlParameter pFIRCADCode11_OB = new SqlParameter("@FIRCADCode11_OB", txtFIRCADCode11_OB.Text.Trim());

                    SqlParameter pFIRCNo12_OB = new SqlParameter("@FIRCNo12_OB", txtFIRCNo12_OB.Text.Trim());
                    SqlParameter pFIRCDate12_OB = new SqlParameter("@FIRCDate12_OB", txtFIRCDate12_OB.Text.Trim());
                    SqlParameter pFIRCAmt12_OB = new SqlParameter("@FIRCAmt12_OB", txtFIRCAmount12_OB.Text.Trim());
                    SqlParameter pFIRCADCode12_OB = new SqlParameter("@FIRCADCode12_OB", txtFIRCADCode12_OB.Text.Trim());


                    SqlParameter pFIRCNo13_OB = new SqlParameter("@FIRCNo13_OB", txtFIRCNo13_OB.Text.Trim());
                    SqlParameter pFIRCDate13_OB = new SqlParameter("@FIRCDate13_OB", txtFIRCDate13_OB.Text.Trim());
                    SqlParameter pFIRCAmt13_OB = new SqlParameter("@FIRCAmt13_OB", txtFIRCAmount13_OB.Text.Trim());
                    SqlParameter pFIRCADCode13_OB = new SqlParameter("@FIRCADCode13_OB", txtFIRCADCode13_OB.Text.Trim());

                    SqlParameter pFIRCNo14_OB = new SqlParameter("@FIRCNo14_OB", txtFIRCNo14_OB.Text.Trim());
                    SqlParameter pFIRCDate14_OB = new SqlParameter("@FIRCDate14_OB", txtFIRCDate14_OB.Text.Trim());
                    SqlParameter pFIRCAmt14_OB = new SqlParameter("@FIRCAmt14_OB", txtFIRCAmount14_OB.Text.Trim());
                    SqlParameter pFIRCADCode14_OB = new SqlParameter("@FIRCADCode14_OB", txtFIRCADCode14_OB.Text.Trim());

                    SqlParameter pFIRCNo15_OB = new SqlParameter("@FIRCNo15_OB", txtFIRCNo15_OB.Text.Trim());
                    SqlParameter pFIRCDate15_OB = new SqlParameter("@FIRCDate15_OB", txtFIRCDate15_OB.Text.Trim());
                    SqlParameter pFIRCAmt15_OB = new SqlParameter("@FIRCAmt15_OB", txtFIRCAmount15_OB.Text.Trim());
                    SqlParameter pFIRCADCode15_OB = new SqlParameter("@FIRCADCode15_OB", txtFIRCADCode15_OB.Text.Trim());



                    SqlParameter padCode = new SqlParameter("@AdCode", txtADCode.Text.Trim());
                    SqlParameter pForeignLocal = new SqlParameter("@ForeignOrLocal", _ForeignOrLocal);



                    //Swach Bharat
                    SqlParameter SBcess = new SqlParameter("@sbcess", txtsbcess.Text);
                    SqlParameter SBcessamt = new SqlParameter("@sbcessamt", txtSBcesssamt.Text);
                    SqlParameter kkcess = new SqlParameter("@kkcess", txtsbcess.Text);
                    SqlParameter kkcessamt = new SqlParameter("@kkcessamt", txtSBcesssamt.Text);
                    SqlParameter STTamt = new SqlParameter("@totstax", txtsttamt.Text);

                    //Fxdls
                    SqlParameter SBFxDls = new SqlParameter("@sbcessonfxdls", txtsbfx.Text);
                    SqlParameter kkcessfxdls = new SqlParameter("@kkcessonfxdls", txt_kkcessonfx.Text);
                    SqlParameter totalSBFxDls = new SqlParameter("@totsbcessonfxdls", txttotsbcess.Text);

                    //fbk

                    SqlParameter fbkchrg = new SqlParameter("@fbkcharges", txt_fbkcharges.Text);
                    SqlParameter staxfbk = new SqlParameter("@staxfbk", ddlServiceTaxfbk.Text);
                    SqlParameter staxfbkamt = new SqlParameter("@staxfbkamt", txtSTaxAmountfbk.Text);
                    SqlParameter sbcessfbk = new SqlParameter("@sbcessfbk", txtsbcessfbk.Text);
                    SqlParameter sbcessamtfbk = new SqlParameter("@sbcessamtfbk", txtSBcesssamtfbk.Text);
                    SqlParameter kkcessfbk = new SqlParameter("@kkcessfbk", txt_kkcessperfbk.Text);
                    SqlParameter kkcessfbkamt = new SqlParameter("@kkcessfbkamt", txt_kkcessamtfbk.Text);
                    SqlParameter totfbkamt = new SqlParameter("@totalfbkamt", txtsttamtfbk.Text);

                    //GBASE FIELDS

                    SqlParameter DD_Doc1 = new SqlParameter("@DD_Doc1", txtBENoDoc1.Text.Trim());
                    SqlParameter Invoice_Doc1 = new SqlParameter("@Invoice_Doc1", txtInvoiceDoc1.Text.Trim());
                    SqlParameter Customs_Invoice_Doc1 = new SqlParameter("@Customs_Invoice_Doc1", txtCustomsDoc1.Text.Trim());
                    SqlParameter Ins_Policy_Doc1 = new SqlParameter("@Ins_Policy_Doc1", txtInsPolicyDoc1.Text.Trim());
                    SqlParameter Cert_Of_Origin_Doc1 = new SqlParameter("@Cert_Of_Origin_Doc1", txtCertOfOriginDoc1.Text.Trim());
                    SqlParameter Packing_List_Doc1 = new SqlParameter("@Packing_List_Doc1", txtPackingDoc1.Text.Trim());
                    SqlParameter WM_Doc = new SqlParameter("@WM_Doc", txtWM.Text.Trim());
                    SqlParameter WM_Doc1 = new SqlParameter("@WM_Doc1", txtWM1.Text.Trim());
                    SqlParameter INSP_Doc = new SqlParameter("@INSP_Doc", txtINSP.Text.Trim());
                    SqlParameter INSP_Doc1 = new SqlParameter("@INSP_Doc1", txtINSP1.Text.Trim());
                    SqlParameter AWB_Doc1 = new SqlParameter("@AWB_Doc1", txtAWBDoc1.Text.Trim());
                    SqlParameter OTHER_Doc = new SqlParameter("@OTHER_Doc", txtOther.Text.Trim());
                    SqlParameter OTHER_Doc1 = new SqlParameter("@OTHER_Doc1", txtOther1.Text.Trim());
                    SqlParameter GBase_Commodity_ID = new SqlParameter("@GBase_Commodity_ID", txtGBaseCommodityID.Text.Trim());
                    SqlParameter Operation_Type = new SqlParameter("@Operation_Type", txtOperationType.Text.Trim());
                    SqlParameter Settlement_Option = new SqlParameter("@Settlement_Option", txtSettlementOption.Text.Trim());
                    SqlParameter Risk_Country = new SqlParameter("@Risk_Country", txtRiskCountry.Text.Trim());

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
                    SqlParameter Fund_Type = new SqlParameter("@Fund_Type", txtFundType.Text.Trim());
                    SqlParameter Base_Rate_Code = new SqlParameter("@Base_Rate_Code", txtBaseRate.Text.Trim());
                    SqlParameter Grade_Code = new SqlParameter("@Grade_Code", txtGradeCode.Text.Trim());

                    SqlParameter Direction = new SqlParameter("@Direction", txtDirection.Text.Trim());
                    SqlParameter Covr_Instruction = new SqlParameter("@Covr_Instruction", txtCovrInstr.Text.Trim());
                    SqlParameter Internal_Rate = new SqlParameter("@Internal_Rate", txtInternalRate.Text.Trim());
                    SqlParameter Spread_Rate = new SqlParameter("@Spread_Rate", txtSpread.Text.Trim());
                    SqlParameter Application_No = new SqlParameter("@Application_No", txtApplNo.Text.Trim());
                    SqlParameter Remarks_EUC = new SqlParameter("@Remarks_EUC", ddlRemEUC.SelectedValue.Trim());
                    SqlParameter Draft_No = new SqlParameter("@Draft_No", txtDraftNo.Text.Trim());
                    SqlParameter Risk_Customer = new SqlParameter("@Risk_Customer", txtRiskCustomer.Text.Trim());
                    SqlParameter Vessel_Name = new SqlParameter("@Vessel_Name", txtVesselName.Text.Trim());
                    SqlParameter Instructions = new SqlParameter("@Instructions", ddlInstructions.SelectedValue.Trim());
                    SqlParameter Reimbursing_Bank = new SqlParameter("@Reimbursing_Bank", txtReimbBank.Text.Trim());
                    SqlParameter GBaseRemarks = new SqlParameter("@GBaseRemarks", txtGBaseRemarks.Text.Trim());
                    SqlParameter Merchandise = new SqlParameter("@Merchandise", txtMerchandise.Text.Trim());
                    SqlParameter Special_Instructions1 = new SqlParameter("@Special_Instructions1", txtSpecialInstructions1.Text.Trim());
                    SqlParameter Special_Instructions2 = new SqlParameter("@Special_Instructions2", txtSpecialInstructions2.Text.Trim());
                    SqlParameter Special_Instructions3 = new SqlParameter("@Special_Instructions3", txtSpecialInstructions3.Text.Trim());
                    SqlParameter Special_Instructions4 = new SqlParameter("@Special_Instructions4", txtSpecialInstructions4.Text.Trim());
                    SqlParameter Special_Instructions5 = new SqlParameter("@Special_Instructions5", txtSpecialInstructions5.Text.Trim());
                    SqlParameter Special_Instructions6 = new SqlParameter("@Special_Instructions6", txtSpecialInstructions6.Text.Trim());
                    SqlParameter Special_Instructions7 = new SqlParameter("@Special_Instructions7", txtSpecialInstructions7.Text.Trim());
                    SqlParameter Special_Instructions8 = new SqlParameter("@Special_Instructions8", txtSpecialInstructions8.Text.Trim());
                    SqlParameter Special_Instructions9 = new SqlParameter("@Special_Instructions9", txtSpecialInstructions9.Text.Trim());
                    SqlParameter Special_Instructions10 = new SqlParameter("@Special_Instructions10", txtSpecialInstructions10.Text.Trim());
                    SqlParameter PrincipalContractNo1 = new SqlParameter("@PrincipalContractNo1", txtPrincipalContractNo1.Text.Trim());
                    SqlParameter PrincipalContractNo2 = new SqlParameter("@PrincipalContractNo2", txtPrincipalContractNo2.Text.Trim());
                    SqlParameter PrincipalExchCurr = new SqlParameter("@PrincipalExchCurr", txtPrincipalExchCurr.Text.Trim());
                    SqlParameter PrincipalExchRate = new SqlParameter("@PrincipalExchRate", txtPrincipalExchRate.Text.Trim());
                    SqlParameter PrincipalIntExchRate = new SqlParameter("@PrincipalIntExchRate", txtPrincipalIntExchRate.Text.Trim());
                    SqlParameter InterestLump = new SqlParameter("@InterestLump", txtInterestLump.Text.Trim());
                    SqlParameter InterestContractNo1 = new SqlParameter("@InterestContractNo1", txtInterestContractNo1.Text.Trim());
                    SqlParameter InterestContractNo2 = new SqlParameter("@InterestContractNo2", txtInterestContractNo2.Text.Trim());
                    SqlParameter InterestExchCurr = new SqlParameter("@InterestExchCurr", txtInterestExchCurr.Text.Trim());
                    SqlParameter InterestExchRate = new SqlParameter("@InterestExchRate", txtInterestExchRate.Text.Trim());
                    SqlParameter InterestIntExchRate = new SqlParameter("@InterestIntExchRate", txtInterestIntExchRate.Text.Trim());
                    SqlParameter CommissionMatu = new SqlParameter("@CommissionMatu", txtCommissionMatu.Text.Trim());
                    SqlParameter CommissionContractNo1 = new SqlParameter("@CommissionContractNo1", txtCommissionContractNo1.Text.Trim());
                    SqlParameter CommissionContractNo2 = new SqlParameter("@CommissionContractNo2", txtCommissionContractNo2.Text.Trim());
                    SqlParameter CommissionExchCurr = new SqlParameter("@CommissionExchCurr", txtCommissionExchCurr.Text.Trim());
                    SqlParameter CommissionExchRate = new SqlParameter("@CommissionExchRate", txtCommissionExchRate.Text.Trim());
                    SqlParameter CommissionIntExchRate = new SqlParameter("@CommissionIntExchRate", txtCommissionIntExchRate.Text.Trim());
                    SqlParameter CRGLCode = new SqlParameter("@CRGLCode", txtCRGLCode.Text.Trim());
                    SqlParameter CRCustAbbr = new SqlParameter("@CRCustAbbr", txtCRCustAbbr.Text.Trim());
                    SqlParameter CRCustAcNo1 = new SqlParameter("@CRCustAcNo1", txtCRCustAcNo1.Text.Trim());
                    SqlParameter CRCustAcNo2 = new SqlParameter("@CRCustAcNo2", txtCRCustAcNo2.Text.Trim());
                    SqlParameter CRCurr = new SqlParameter("@CRCurr", txtCRCurr.Text.Trim());
                    SqlParameter CRAmount = new SqlParameter("@CRAmount", txtCRAmount.Text.Trim());
                    SqlParameter CRIntCurr = new SqlParameter("@CRIntCurr", txtCRIntCurr.Text.Trim());
                    SqlParameter CRIntAmount = new SqlParameter("@CRIntAmount", txtCRIntAmount.Text.Trim());
                    SqlParameter CRIntPayer = new SqlParameter("@CRIntPayer", txtCRIntPayer.Text.Trim());
                    SqlParameter CRPaymentCommCurr = new SqlParameter("@CRPaymentCommCurr", txtCRPaymentCommCurr.Text.Trim());
                    SqlParameter CRPaymentCommAmount = new SqlParameter("@CRPaymentCommAmount", txtCRPaymentCommAmount.Text.Trim());
                    SqlParameter CRPaymentCommPayer = new SqlParameter("@CRPaymentCommPayer", txtCRPaymentCommPayer.Text.Trim());
                    SqlParameter CRHandlingCommCurr = new SqlParameter("@CRHandlingCommCurr", txtCRHandlingCommCurr.Text.Trim());
                    SqlParameter CRHandlingCommAmount = new SqlParameter("@CRHandlingCommAmount", txtCRHandlingCommAmount.Text.Trim());
                    SqlParameter CRHandlingCommPayer = new SqlParameter("@CRHandlingCommPayer", txtCRHandlingCommPayer.Text.Trim());
                    SqlParameter CRPostageCurr = new SqlParameter("@CRPostageCurr", txtCRPostageCurr.Text.Trim());
                    SqlParameter CRPostageAmount = new SqlParameter("@CRPostageAmount", txtCRPostageAmount.Text.Trim());
                    SqlParameter CRPostagePayer = new SqlParameter("@CRPostagePayer", txtCRPostagePayer.Text.Trim());
                    SqlParameter CRCurr1 = new SqlParameter("@CRCurr1", txtCRCurr1.Text.Trim());
                    SqlParameter CRAmount1 = new SqlParameter("@CRAmount1", txtCRAmount1.Text.Trim());
                    SqlParameter CRPayer1 = new SqlParameter("@CRPayer1", txtCRPayer1.Text.Trim());
                    SqlParameter DRCurr = new SqlParameter("@DRCurr", txtDRCurr.Text.Trim());
                    SqlParameter DRAmount = new SqlParameter("@DRAmount", txtDRAmount.Text.Trim());
                    SqlParameter DRGLCode1 = new SqlParameter("@DRGLCode1", txtDRGLCode1.Text.Trim());
                    SqlParameter DRCustAbbr1 = new SqlParameter("@DRCustAbbr1", txtDRCustAbbr1.Text.Trim());
                    SqlParameter DRCustAcNo11 = new SqlParameter("@DRCustAcNo11", txtDRCustAcNo11.Text.Trim());
                    SqlParameter DRCustAcNo12 = new SqlParameter("@DRCustAcNo12", txtDRCustAcNo12.Text.Trim());
                    SqlParameter DRCurr1 = new SqlParameter("@DRCurr1", txtDRCurr1.Text.Trim());
                    SqlParameter DRAmount1 = new SqlParameter("@DRAmount1", txtDRAmount1.Text.Trim());
                    SqlParameter DRGLCode2 = new SqlParameter("@DRGLCode2", txtDRGLCode2.Text.Trim());
                    SqlParameter DRCustAbbr2 = new SqlParameter("@DRCustAbbr2", txtDRCustAbbr2.Text.Trim());
                    SqlParameter DRCustAcNo21 = new SqlParameter("@DRCustAcNo21", txtDRCustAcNo21.Text.Trim());
                    SqlParameter DRCustAcNo22 = new SqlParameter("@DRCustAcNo22", txtDRCustAcNo22.Text.Trim());
                    SqlParameter DRCurr2 = new SqlParameter("@DRCurr2", txtDRCurr2.Text.Trim());
                    SqlParameter DRAmount2 = new SqlParameter("@DRAmount2", txtDRAmount2.Text.Trim());
                    SqlParameter DRGLCode3 = new SqlParameter("@DRGLCode3", txtDRGLCode3.Text.Trim());
                    SqlParameter DRCustAbbr3 = new SqlParameter("@DRCustAbbr3", txtDRCustAbbr3.Text.Trim());
                    SqlParameter DRCustAcNo31 = new SqlParameter("@DRCustAcNo31", txtDRCustAcNo31.Text.Trim());
                    SqlParameter DRCustAcNo32 = new SqlParameter("@DRCustAcNo32", txtDRCustAcNo32.Text.Trim());
                    SqlParameter DRCurr3 = new SqlParameter("@DRCurr3", txtDRCurr3.Text.Trim());
                    SqlParameter DRAmount3 = new SqlParameter("@DRAmount3", txtDRAmount3.Text.Trim());
                    SqlParameter DRGLCode4 = new SqlParameter("@DRGLCode4", txtDRGLCode4.Text.Trim());
                    SqlParameter DRCustAbbr4 = new SqlParameter("@DRCustAbbr4", txtDRCustAbbr4.Text.Trim());
                    SqlParameter DRCustAcNo41 = new SqlParameter("@DRCustAcNo41", txtDRCustAcNo41.Text.Trim());
                    SqlParameter DRCustAcNo42 = new SqlParameter("@DRCustAcNo42", txtDRCustAcNo42.Text.Trim());
                    SqlParameter DRCurr4 = new SqlParameter("@DRCurr4", txtDRCurr4.Text.Trim());
                    SqlParameter DRAmount4 = new SqlParameter("@DRAmount4", txtDRAmount4.Text.Trim());
                    SqlParameter DRGLCode5 = new SqlParameter("@DRGLCode5", txtDRGLCode5.Text.Trim());
                    SqlParameter DRCustAbbr5 = new SqlParameter("@DRCustAbbr5", txtDRCustAbbr5.Text.Trim());
                    SqlParameter DRCustAcNo51 = new SqlParameter("@DRCustAcNo51", txtDRCustAcNo51.Text.Trim());
                    SqlParameter DRCustAcNo52 = new SqlParameter("@DRCustAcNo52", txtDRCustAcNo52.Text.Trim());
                    SqlParameter DRCurr5 = new SqlParameter("@DRCurr5", txtDRCurr5.Text.Trim());
                    SqlParameter DRAmount5 = new SqlParameter("@DRAmount5", txtDRAmount5.Text.Trim());
                    SqlParameter PayingBank = new SqlParameter("@PayingBank", txtPayingBankID.Text.Trim());
                    string ddldisind = "";

                    if (ddlDispachInd.SelectedValue == "---Select---")
                    {
                        ddldisind = "";
                    }
                    else
                    {
                        ddldisind = ddlDispachInd.SelectedValue.Trim();
                    }
                    SqlParameter DispatchIndc = new SqlParameter("@DispIndc", ddldisind);
                    SqlParameter Merchant_Trade = new SqlParameter("@MerchantTrade", ddlMercTrade.SelectedValue.Trim());

                    SqlParameter Save_Draft = new SqlParameter("@Save_Draft", "M");
                    //SqlParameter DispatchIndcDefValue = new SqlParameter("@DispIndByDeafValue", txtDispBydefault.Text.Trim());
                    SqlParameter DispatchIndcDefValue = new SqlParameter("@DispIndByDeafValue", ddlDispBydefault.SelectedValue.Trim());

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

                    SqlParameter PFIRCCurrency1_OB = new SqlParameter("@FIRCCurrency1_OB", ddlFIRCCurrency1_OB.SelectedValue);
                    SqlParameter PFIRCCurrency2_OB = new SqlParameter("@FIRCCurrency2_OB", ddlFIRCCurrency2_OB.SelectedValue);
                    SqlParameter PFIRCCurrency3_OB = new SqlParameter("@FIRCCurrency3_OB", ddlFIRCCurrency3_OB.SelectedValue);
                    SqlParameter PFIRCCurrency4_OB = new SqlParameter("@FIRCCurrency4_OB", ddlFIRCCurrency4_OB.SelectedValue);
                    SqlParameter PFIRCCurrency5_OB = new SqlParameter("@FIRCCurrency5_OB", ddlFIRCCurrency5_OB.SelectedValue);
                    SqlParameter PFIRCCurrency6_OB = new SqlParameter("@FIRCCurrency6_OB", ddlFIRCCurrency6_OB.SelectedValue);
                    SqlParameter PFIRCCurrency7_OB = new SqlParameter("@FIRCCurrency7_OB", ddlFIRCCurrency7_OB.SelectedValue);
                    SqlParameter PFIRCCurrency8_OB = new SqlParameter("@FIRCCurrency8_OB", ddlFIRCCurrency8_OB.SelectedValue);
                    SqlParameter PFIRCCurrency9_OB = new SqlParameter("@FIRCCurrency9_OB", ddlFIRCCurrency9_OB.SelectedValue);
                    SqlParameter PFIRCCurrency10_OB = new SqlParameter("@FIRCCurrency10_OB", ddlFIRCCurrency10_OB.SelectedValue);
                    SqlParameter PFIRCCurrency11_OB = new SqlParameter("@FIRCCurrency11_OB", ddlFIRCCurrency11_OB.SelectedValue);
                    SqlParameter PFIRCCurrency12_OB = new SqlParameter("@FIRCCurrency12_OB", ddlFIRCCurrency12_OB.SelectedValue);
                    SqlParameter PFIRCCurrency13_OB = new SqlParameter("@FIRCCurrency13_OB", ddlFIRCCurrency13_OB.SelectedValue);
                    SqlParameter PFIRCCurrency14_OB = new SqlParameter("@FIRCCurrency14_OB", ddlFIRCCurrency14_OB.SelectedValue);
                    SqlParameter PFIRCCurrency15_OB = new SqlParameter("@FIRCCurrency15_OB", ddlFIRCCurrency15_OB.SelectedValue);


                    SqlParameter PFIRCRealisedCurr1_OB = new SqlParameter("@FIRCRealisedCurr1_OB", ddlFIRCRealisedCurr1_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr2_OB = new SqlParameter("@FIRCRealisedCurr2_OB", ddlFIRCRealisedCurr2_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr3_OB = new SqlParameter("@FIRCRealisedCurr3_OB", ddlFIRCRealisedCurr3_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr4_OB = new SqlParameter("@FIRCRealisedCurr4_OB", ddlFIRCRealisedCurr4_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr5_OB = new SqlParameter("@FIRCRealisedCurr5_OB", ddlFIRCRealisedCurr5_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr6_OB = new SqlParameter("@FIRCRealisedCurr6_OB", ddlFIRCRealisedCurr6_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr7_OB = new SqlParameter("@FIRCRealisedCurr7_OB", ddlFIRCRealisedCurr7_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr8_OB = new SqlParameter("@FIRCRealisedCurr8_OB", ddlFIRCRealisedCurr8_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr9_OB = new SqlParameter("@FIRCRealisedCurr9_OB", ddlFIRCRealisedCurr9_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr10_OB = new SqlParameter("@FIRCRealisedCurr10_OB", ddlFIRCRealisedCurr10_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr11_OB = new SqlParameter("@FIRCRealisedCurr11_OB", ddlFIRCRealisedCurr11_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr12_OB = new SqlParameter("@FIRCRealisedCurr12_OB", ddlFIRCRealisedCurr12_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr13_OB = new SqlParameter("@FIRCRealisedCurr13_OB", ddlFIRCRealisedCurr13_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr14_OB = new SqlParameter("@FIRCRealisedCurr14_OB", ddlFIRCRealisedCurr14_OB.SelectedValue);
                    SqlParameter PFIRCRealisedCurr15_OB = new SqlParameter("@FIRCRealisedCurr15_OB", ddlFIRCRealisedCurr15_OB.SelectedValue);

                    SqlParameter PFIRCCrossCurrRate1_OB = new SqlParameter("@FIRCCrossCurrRate1_OB", txtFIRCCrossCurrRate1_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate2_OB = new SqlParameter("@FIRCCrossCurrRate2_OB", txtFIRCCrossCurrRate2_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate3_OB = new SqlParameter("@FIRCCrossCurrRate3_OB", txtFIRCCrossCurrRate3_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate4_OB = new SqlParameter("@FIRCCrossCurrRate4_OB", txtFIRCCrossCurrRate4_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate5_OB = new SqlParameter("@FIRCCrossCurrRate5_OB", txtFIRCCrossCurrRate5_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate6_OB = new SqlParameter("@FIRCCrossCurrRate6_OB", txtFIRCCrossCurrRate6_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate7_OB = new SqlParameter("@FIRCCrossCurrRate7_OB", txtFIRCCrossCurrRate7_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate8_OB = new SqlParameter("@FIRCCrossCurrRate8_OB", txtFIRCCrossCurrRate8_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate9_OB = new SqlParameter("@FIRCCrossCurrRate9_OB", txtFIRCCrossCurrRate9_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate10_OB = new SqlParameter("@FIRCCrossCurrRate10_OB", txtFIRCCrossCurrRate10_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate11_OB = new SqlParameter("@FIRCCrossCurrRate11_OB", txtFIRCCrossCurrRate11_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate12_OB = new SqlParameter("@FIRCCrossCurrRate12_OB", txtFIRCCrossCurrRate12_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate13_OB = new SqlParameter("@FIRCCrossCurrRate13_OB", txtFIRCCrossCurrRate13_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate14_OB = new SqlParameter("@FIRCCrossCurrRate14_OB", txtFIRCCrossCurrRate14_OB.Text.Trim());
                    SqlParameter PFIRCCrossCurrRate15_OB = new SqlParameter("@FIRCCrossCurrRate15_OB", txtFIRCCrossCurrRate15_OB.Text.Trim());


                    SqlParameter PFIRCTobeAdjustedinSB1_OB = new SqlParameter("@FIRCTobeAdjustedinSB1_OB", txtFIRCTobeAdjustedinSB1_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB2_OB = new SqlParameter("@FIRCTobeAdjustedinSB2_OB", txtFIRCTobeAdjustedinSB2_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB3_OB = new SqlParameter("@FIRCTobeAdjustedinSB3_OB", txtFIRCTobeAdjustedinSB3_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB4_OB = new SqlParameter("@FIRCTobeAdjustedinSB4_OB", txtFIRCTobeAdjustedinSB4_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB5_OB = new SqlParameter("@FIRCTobeAdjustedinSB5_OB", txtFIRCTobeAdjustedinSB5_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB6_OB = new SqlParameter("@FIRCTobeAdjustedinSB6_OB", txtFIRCTobeAdjustedinSB6_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB7_OB = new SqlParameter("@FIRCTobeAdjustedinSB7_OB", txtFIRCTobeAdjustedinSB7_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB8_OB = new SqlParameter("@FIRCTobeAdjustedinSB8_OB", txtFIRCTobeAdjustedinSB8_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB9_OB = new SqlParameter("@FIRCTobeAdjustedinSB9_OB", txtFIRCTobeAdjustedinSB9_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB10_OB = new SqlParameter("@FIRCTobeAdjustedinSB10_OB", txtFIRCTobeAdjustedinSB10_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB11_OB = new SqlParameter("@FIRCTobeAdjustedinSB11_OB", txtFIRCTobeAdjustedinSB11_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB12_OB = new SqlParameter("@FIRCTobeAdjustedinSB12_OB", txtFIRCTobeAdjustedinSB12_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB13_OB = new SqlParameter("@FIRCTobeAdjustedinSB13_OB", txtFIRCTobeAdjustedinSB13_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB14_OB = new SqlParameter("@FIRCTobeAdjustedinSB14_OB", txtFIRCTobeAdjustedinSB14_OB.Text.Trim());
                    SqlParameter PFIRCTobeAdjustedinSB15_OB = new SqlParameter("@FIRCTobeAdjustedinSB15_OB", txtFIRCTobeAdjustedinSB15_OB.Text.Trim());
                    //---------------------------------------------NILESH 04/08/2023-----------------------------------
                    SqlParameter ConsigneePardyID = new SqlParameter("@consigneePartyID", txtconsigneePartyID.Text.Trim());
                    SqlParameter pNoOfSB = new SqlParameter("@NoOfShippingBills", txtNoOfSB.Text.Trim());
                    string PendingSB = "N";
                    if (chkSB.Checked)
                        PendingSB = "Y";
                    pIsManualGR.Value = bit_IsManualGR;
                    SqlParameter pCheckPendingSB = new SqlParameter("@PendingSBFlag", PendingSB);

                    //---------------------------------------------NILESH END 04/08/2023-----------------------------------

                    string _query = "TF_EXP_UpdateExportEntryDetails";
                    TF_DATA objSave = new TF_DATA();

                    _result = objSave.SaveDeleteData(_query, bCode, docNumber, p1, p2, p3, p4, p5, pCustAcNo, pOverseasPartyID,
                        pOverseasBankID,
                        //pCustCheck, pCS,
                        pDateRcvd, pENCdate, pDateNegotiated, pLCNo, pLCDate, pLCissuedBy, pBENo, pBEDate,
                        pBEdoc, pbankLine, pAccpDate, pInvoiceNo, pInvoiceDate, pInvoiceDoc, pAWBno, pAWBdate, pAWBissuedBy, pAWBdoc, pPackingList,
                        pPackingListDate, pPackingListDoc, pCertOfOrigin, pCertOfOriginIssuedBy, pCertOfOriginDoc, pCustomsInvoice, pCustomsInvoiceDate,
                        pCustomsInvoiceDoc, pCommodity, pInsPolicy, pInsPolicyDate, pInsPolicyIssuedBy, pInsPolicydoc, pGSPdate, pGSPdoc, pContractNo,
                        pContractRate, pFIRCno, pFIRCdate, pFIRCissuedBy, pFIRCdoc, pMisc, pShipment, pMiscDoc,
                        //pCoveringFrom, 
                        pCoveringTo, pCountryCode,
                        preimbValDate, preimbClaimDate, preimbBankName, preimbBankBICcode, preimbAmount, pNotes,
                        pCurr, pLoanAdv, pOthCurr, pExchRT, pNoOfDays, pAfterFrom, pLibor, pOutOfDays,
                        pDueDate, pAccpDueDate, pOtherRemarks, pBillAmt, pBillAmtinRS, pNegotiated, pNegotiatedinRS, pInterest, pInterestinRS,
                        pNetAmt, pNetAmtinRS, pExchRtEBR, pOtherCharges, pSTFXDLS, pBankCert, pNegotiationFees, pCourierCharges, pMarginAmount, pCommission,
                        psTax, psAmt, pCurrentAcinRS, pcb1, pcb2, pcb3, pcb4, pcb5, pcb6, pcb7, pcb7A, pcb7B, pcb8, pcb9, pcb10, pcb11, pcb12, pcb13, pRemarks, pRemarks1, pUser, pUploadDate, pchkRBI
                        , pCustCheck, pCS
                        , pTTRef1, pTTRef2, pTTRef3, pTTRef4, pTTRef5, pTTRef6, pTTRef7, pTTRef8, pTTRef9, pTTRef10, pTTRef11, pTTRef12, pTTRef13, pTTRef14, pTTRef15
                        , pTTAmt1, pTTAmt2, pTTAmt3, pTTAmt4, pTTAmt5, pTTAmt6, pTTAmt7, pTTAmt8, pTTAmt9, pTTAmt10, pTTAmt11, pTTAmt12, pTTAmt13, pTTAmt14, pTTAmt15
                        , pIntRate1, pForDays1, pIntRate2, pForDays2, pIntRate3, pForDays3, pIntRate4, pForDays4, pIntRate5, pForDays5, pIntRate6, pForDays6
                        , pIntFDate1, pIntTDate1, pIntFDate2, pIntTDate2, pIntFDate3, pIntTDate3, pIntFDate4, pIntTDate4, pIntFDate5, pIntTDate5, pIntFDate6, pIntTDate6
                        , pCommissionID
                        , pFIRCNo1_OB, pFIRCDate1_OB, pFIRCAmt1_OB, pFIRCADCode1_OB
                        , pFIRCNo2_OB, pFIRCDate2_OB, pFIRCAmt2_OB, pFIRCADCode2_OB
                        , pFIRCNo3_OB, pFIRCDate3_OB, pFIRCAmt3_OB, pFIRCADCode3_OB
                        , pFIRCNo4_OB, pFIRCDate4_OB, pFIRCAmt4_OB, pFIRCADCode4_OB
                        , pFIRCNo5_OB, pFIRCDate5_OB, pFIRCAmt5_OB, pFIRCADCode5_OB
                        , pFIRCNo6_OB, pFIRCDate6_OB, pFIRCAmt6_OB, pFIRCADCode6_OB
                        , pFIRCNo7_OB, pFIRCDate7_OB, pFIRCAmt7_OB, pFIRCADCode7_OB
                        , pFIRCNo8_OB, pFIRCDate8_OB, pFIRCAmt8_OB, pFIRCADCode8_OB
                        , pFIRCNo9_OB, pFIRCDate9_OB, pFIRCAmt9_OB, pFIRCADCode9_OB
                        , pFIRCNo10_OB, pFIRCDate10_OB, pFIRCAmt10_OB, pFIRCADCode10_OB
                        , pFIRCNo11_OB, pFIRCDate11_OB, pFIRCAmt11_OB, pFIRCADCode11_OB
                        , pFIRCNo12_OB, pFIRCDate12_OB, pFIRCAmt12_OB, pFIRCADCode12_OB
                        , pFIRCNo13_OB, pFIRCDate13_OB, pFIRCAmt13_OB, pFIRCADCode13_OB
                        , pFIRCNo14_OB, pFIRCDate14_OB, pFIRCAmt14_OB, pFIRCADCode14_OB
                        , pFIRCNo15_OB, pFIRCDate15_OB, pFIRCAmt15_OB, pFIRCADCode15_OB
                        , padCode, pForeignLocal, pIsManualGR, SBcess, SBcessamt, kkcess, kkcessamt, SBFxDls, totalSBFxDls, kkcessfxdls, STTamt,
                         fbkchrg, staxfbk, staxfbkamt, sbcessfbk, sbcessamtfbk, kkcessfbk, kkcessfbkamt, totfbkamt,
                        //GBASE FIELDS 
                        DD_Doc1
               , Invoice_Doc1
               , Customs_Invoice_Doc1
               , Ins_Policy_Doc1
               , Cert_Of_Origin_Doc1
               , Packing_List_Doc1
               , WM_Doc
               , WM_Doc1
               , INSP_Doc
               , INSP_Doc1
               , AWB_Doc1
               , OTHER_Doc
               , OTHER_Doc1
               , GBase_Commodity_ID
               , Operation_Type
               , Settlement_Option
               , Risk_Country
               , Payer
               , Fund_Type
               , Base_Rate_Code
               , Grade_Code
               , Direction
               , Covr_Instruction
               , Internal_Rate
               , Spread_Rate
               , Application_No
               , Remarks_EUC
               , Draft_No
               , Risk_Customer
               , Vessel_Name
               , Instructions
               , Reimbursing_Bank
               , GBaseRemarks
               , Merchandise
               , Special_Instructions1
               , Special_Instructions2
               , Special_Instructions3
               , Special_Instructions4
               , Special_Instructions5
               , Special_Instructions6
               , Special_Instructions7
               , Special_Instructions8
               , Special_Instructions9
               , Special_Instructions10
               , PrincipalContractNo1
               , PrincipalContractNo2
               , PrincipalExchCurr
               , PrincipalExchRate
               , PrincipalIntExchRate
               , InterestLump
               , InterestContractNo1
               , InterestContractNo2
               , InterestExchCurr
               , InterestExchRate
               , InterestIntExchRate
               , CommissionMatu
               , CommissionContractNo1
               , CommissionContractNo2
               , CommissionExchCurr
               , CommissionExchRate
               , CommissionIntExchRate
               , CRGLCode
               , CRCustAbbr
               , CRCustAcNo1
               , CRCustAcNo2
               , CRCurr
               , CRAmount
               , CRIntCurr
               , CRIntAmount
               , CRIntPayer
               , CRPaymentCommCurr
               , CRPaymentCommAmount
               , CRPaymentCommPayer
               , CRHandlingCommCurr
               , CRHandlingCommAmount
               , CRHandlingCommPayer
               , CRPostageCurr
               , CRPostageAmount
               , CRPostagePayer
               , CRCurr1
               , CRAmount1
               , CRPayer1
               , DRCurr
               , DRAmount
               , DRGLCode1
               , DRCustAbbr1
               , DRCustAcNo11
               , DRCustAcNo12
               , DRCurr1
               , DRAmount1
               , DRGLCode2
               , DRCustAbbr2
               , DRCustAcNo21
               , DRCustAcNo22
               , DRCurr2
               , DRAmount2
               , DRGLCode3
               , DRCustAbbr3
               , DRCustAcNo31
               , DRCustAcNo32
               , DRCurr3
               , DRAmount3
               , DRGLCode4
               , DRCustAbbr4
               , DRCustAcNo41
               , DRCustAcNo42
               , DRCurr4
               , DRAmount4
               , DRGLCode5
               , DRCustAbbr5
               , DRCustAcNo51
               , DRCustAcNo52
               , DRCurr5
               , DRAmount5
               , PayingBank
               , DispatchIndc
               , DispatchIndcDefValue
               , Merchant_Trade, Save_Draft,
                PTTCurr1,
                PTTCurr2, PTTCurr3, PTTCurr4, PTTCurr5, PTTCurr6, PTTCurr7, PTTCurr8, PTTCurr9, PTTCurr10, PTTCurr11, PTTCurr12, PTTCurr13,
                PTTCurr14, PTTCurr15,
                PTotTTAmt1, PTotTTAmt2, PTotTTAmt3, PTotTTAmt4, PTotTTAmt5, PTotTTAmt6, PTotTTAmt7, PTotTTAmt8, PTotTTAmt9, PTotTTAmt10, PTotTTAmt11, PTotTTAmt12,
                 PTotTTAmt13, PTotTTAmt14, PTotTTAmt15,
                 PBalTTAmt1, PBalTTAmt2, PBalTTAmt3, PBalTTAmt4, PBalTTAmt5, PBalTTAmt6, PBalTTAmt7, PBalTTAmt8, PBalTTAmt9, PBalTTAmt10, PBalTTAmt11, PBalTTAmt12, PBalTTAmt13,
                 PBalTTAmt14, PBalTTAmt15,
                 PTTRealisedCurr1, PTTRealisedCurr2, PTTRealisedCurr3, PTTRealisedCurr4, PTTRealisedCurr5, PTTRealisedCurr6, PTTRealisedCurr7, PTTRealisedCurr8,
                 PTTRealisedCurr9, PTTRealisedCurr10, PTTRealisedCurr11, PTTRealisedCurr12, PTTRealisedCurr13, PTTRealisedCurr14, PTTRealisedCurr15,

                 PTTCrossCurrRate1, PTTCrossCurrRate2, PTTCrossCurrRate3, PTTCrossCurrRate4, PTTCrossCurrRate5, PTTCrossCurrRate6, PTTCrossCurrRate7, PTTCrossCurrRate8,
                 PTTCrossCurrRate9, PTTCrossCurrRate10, PTTCrossCurrRate11, PTTCrossCurrRate12, PTTCrossCurrRate13, PTTCrossCurrRate14, PTTCrossCurrRate15,

                 PTTAmtRealised1, PTTAmtRealised2, PTTAmtRealised3, PTTAmtRealised4, PTTAmtRealised5, PTTAmtRealised6, PTTAmtRealised7, PTTAmtRealised8, PTTAmtRealised9,
                  PTTAmtRealised10, PTTAmtRealised11, PTTAmtRealised12, PTTAmtRealised13, PTTAmtRealised14, PTTAmtRealised15,

                  PFIRCCurrency1_OB, PFIRCCurrency2_OB, PFIRCCurrency3_OB, PFIRCCurrency4_OB, PFIRCCurrency5_OB, PFIRCCurrency6_OB, PFIRCCurrency7_OB, PFIRCCurrency8_OB,
                  PFIRCCurrency9_OB, PFIRCCurrency10_OB, PFIRCCurrency11_OB, PFIRCCurrency12_OB, PFIRCCurrency13_OB, PFIRCCurrency14_OB, PFIRCCurrency15_OB,

                  PFIRCRealisedCurr1_OB, PFIRCRealisedCurr2_OB, PFIRCRealisedCurr3_OB, PFIRCRealisedCurr4_OB, PFIRCRealisedCurr5_OB, PFIRCRealisedCurr6_OB,
                  PFIRCRealisedCurr7_OB, PFIRCRealisedCurr8_OB, PFIRCRealisedCurr9_OB, PFIRCRealisedCurr10_OB, PFIRCRealisedCurr11_OB, PFIRCRealisedCurr12_OB,
                  PFIRCRealisedCurr13_OB, PFIRCRealisedCurr14_OB, PFIRCRealisedCurr15_OB,

                  PFIRCCrossCurrRate1_OB, PFIRCCrossCurrRate2_OB, PFIRCCrossCurrRate3_OB, PFIRCCrossCurrRate4_OB, PFIRCCrossCurrRate5_OB, PFIRCCrossCurrRate6_OB,
                  PFIRCCrossCurrRate7_OB, PFIRCCrossCurrRate8_OB, PFIRCCrossCurrRate9_OB, PFIRCCrossCurrRate10_OB, PFIRCCrossCurrRate11_OB, PFIRCCrossCurrRate12_OB,
                   PFIRCCrossCurrRate13_OB, PFIRCCrossCurrRate14_OB, PFIRCCrossCurrRate15_OB,

                PFIRCTobeAdjustedinSB1_OB, PFIRCTobeAdjustedinSB2_OB, PFIRCTobeAdjustedinSB3_OB, PFIRCTobeAdjustedinSB4_OB, PFIRCTobeAdjustedinSB5_OB, PFIRCTobeAdjustedinSB6_OB,
                PFIRCTobeAdjustedinSB7_OB, PFIRCTobeAdjustedinSB8_OB, PFIRCTobeAdjustedinSB9_OB, PFIRCTobeAdjustedinSB10_OB, PFIRCTobeAdjustedinSB11_OB, PFIRCTobeAdjustedinSB12_OB,
                PFIRCTobeAdjustedinSB13_OB, PFIRCTobeAdjustedinSB14_OB, PFIRCTobeAdjustedinSB15_OB
                    //----------------------------------NILESH 04/08/2023--------------------------------
               , ConsigneePardyID, pNoOfSB, pCheckPendingSB
               //---------------------------------------------NILESH END 04/08/2023-----------------------------------
               );

                    string s = "";
                    if (_result.Substring(0, 5) == "added")
                    {
                        txtDocumentNo.Text = "";
                    }
                    if (_result.Substring(0, 5) == "added" || _result == "updated")
                    {
                        //========================= GR/PP/CutomsDetails=====================//
                        if (GridViewGRPPCustomsDetails.Visible == true)
                        {
                            int rowIndex = 0;

                            TF_DATA objSaveGRCustomsDetails = new TF_DATA();

                            SqlParameter pGRbCode = new SqlParameter("@bCode", SqlDbType.VarChar);
                            SqlParameter pGRDocNo = new SqlParameter("@documentNo", SqlDbType.VarChar);

                            SqlParameter pGRno = new SqlParameter("@GR", SqlDbType.VarChar);

                            SqlParameter pGRCurrency = new SqlParameter("@currency", SqlDbType.VarChar);
                            SqlParameter pGRAmount = new SqlParameter("@amount", SqlDbType.VarChar);
                            SqlParameter pGRShippingBillNo = new SqlParameter("@shippingBillNo", SqlDbType.VarChar);
                            SqlParameter pGRShippingBillDate = new SqlParameter("@shippingBillDate", SqlDbType.VarChar);
                            SqlParameter pGRCommission = new SqlParameter("@commission", SqlDbType.VarChar);
                            SqlParameter pGRPortCode = new SqlParameter("@portCode", SqlDbType.VarChar);

                            SqlParameter pGRPFormType = new SqlParameter("@formtype", SqlDbType.VarChar);

                            SqlParameter pGRdispind = new SqlParameter("@dispind", SqlDbType.VarChar);
                            SqlParameter pGRinvoiceno = new SqlParameter("@invoicenum", SqlDbType.VarChar);
                            SqlParameter pGRinvoiceDate = new SqlParameter("@invoicedt", SqlDbType.VarChar);
                            SqlParameter pGRinvoiceAmt = new SqlParameter("@invoiceamt", SqlDbType.VarChar);

                            SqlParameter pGR_frieght = new SqlParameter("@frieghtAmt", SqlDbType.VarChar);
                            SqlParameter pGR_insurance = new SqlParameter("@insuranceAmt", SqlDbType.VarChar);
                            SqlParameter pGR_discount = new SqlParameter("@discountAmt", SqlDbType.VarChar);
                            SqlParameter pGR_otherDeduction = new SqlParameter("@otherDeduction", SqlDbType.VarChar);
                            SqlParameter pGR_packinChrgs = new SqlParameter("@packingCharges", SqlDbType.VarChar);

                            SqlParameter pGR_frieghtCurr = new SqlParameter("@curFreight", SqlDbType.VarChar);
                            SqlParameter pGR_insuranceCurr = new SqlParameter("@curInsurance", SqlDbType.VarChar);
                            SqlParameter pGR_discountCurr = new SqlParameter("@curDiscount", SqlDbType.VarChar);
                            SqlParameter pGR_CommCurr = new SqlParameter("@curComm", SqlDbType.VarChar);
                            SqlParameter pGR_otherDeductionCurr = new SqlParameter("@curOtherDeduction", SqlDbType.VarChar);
                            SqlParameter pGR_packinChrgsCurr = new SqlParameter("@curPackingCharges", SqlDbType.VarChar);
                            SqlParameter pGR_GRExchRate = new SqlParameter("@GRExchRate", SqlDbType.VarChar);
                            SqlParameter invsrno = new SqlParameter("@invsrno", SqlDbType.VarChar);
                            SqlParameter status = new SqlParameter("@status", SqlDbType.VarChar);

                            ////SqlParameter pGRPCommodity = new SqlParameter("@commodity", SqlDbType.VarChar);
                            //SqlParameter pGRPAmtInINR = new SqlParameter("@amount_in_inr", SqlDbType.VarChar);
                            //SqlParameter pGRPExchRate = new SqlParameter("@exchrate", SqlDbType.VarChar);


                            if (ddlDispachInd.SelectedValue == "---Select---")
                            {
                                pGRdispind.Value = "";
                            }
                            else
                            {
                                pGRdispind.Value = ddlDispachInd.SelectedValue;
                            }
                            //SqlParameter pDsipDefltValue = new SqlParameter("@dispDefaval", SqlDbType.VarChar);

                            _query = "TF_EXP_DeleteGRPPCustomsDetails";
                            if (_result.Substring(0, 5) == "added")
                                pGRDocNo.Value = _result.Substring(5);
                            else
                                pGRDocNo.Value = txtDocumentNo.Text;
                            TF_DATA objDel = new TF_DATA();
                            objDel.SaveDeleteData(_query, pGRDocNo);

                            _query = "TF_EXP_UpdateGRPPCustomsDetails";

                            // pcMode.Value = _mode;
                            pGRbCode.Value = hdnBranchCode.Value;

                            rowIndex = 0;
                            for (int i = 1; i <= GridViewGRPPCustomsDetails.Rows.Count; i++)
                            {
                                //extract the TextBox values
                                if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                                {
                                    Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblSrNo");
                                    Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFormType");
                                    Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGR");
                                    Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCurrency");
                                    Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblAmount");
                                    //Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblExchRate");
                                    //Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblAmountInInr");
                                    Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillNo");
                                    Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillDate");
                                    Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommission");
                                    Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPortCode");

                                    //Label lbl14 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDispInd");
                                    Label lbl15 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceNo");
                                    Label lbl16 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceDate");
                                    Label lbl17 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceAmt");

                                    Label lblFreightAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFreightAmt");
                                    Label lblInsuranceAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsuranceAmt");
                                    Label lblDiscountAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDiscountAmt");
                                    Label lblOthDeduction = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOthDeduction");
                                    Label lblPacking = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPacking");


                                    Label lblFreightCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFrieghtCurr");
                                    Label lblInsuranceCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsCurr");
                                    Label lblDiscountCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDisCurr");
                                    Label lblCommCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommCurr");
                                    Label lblOthDeductionCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOtherDedCurr");
                                    Label lblPackingCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPackingChgCurr");

                                    Label lblGRExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGRExchRate");

                                    Label lblinvsrno = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblinvsrno");
                                    Label lblstatus = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblstatus");

                                    invsrno.Value = lblinvsrno.Text;
                                    status.Value = lblstatus.Text;

                                    pGRPFormType.Value = lbl2.Text;
                                    ////pGRPCommodity.Value = lbl3.Text;
                                    pGRno.Value = lbl4.Text;
                                    pGRCurrency.Value = lbl5.Text;
                                    pGRAmount.Value = lbl6.Text;
                                    //pGRPExchRate.Value = lbl7.Text;
                                    //pGRPAmtInINR.Value = lbl8.Text;
                                    pGRShippingBillNo.Value = lbl9.Text;
                                    pGRShippingBillDate.Value = lbl10.Text;
                                    pGRCommission.Value = lbl11.Text;
                                    pGRPortCode.Value = lbl12.Text;

                                    //pGRdispind.Value = lbl14.Text;
                                    pGRinvoiceno.Value = lbl15.Text;
                                    pGRinvoiceDate.Value = lbl16.Text;
                                    pGRinvoiceAmt.Value = lbl17.Text;

                                    pGR_frieght.Value = lblFreightAmt.Text;
                                    pGR_insurance.Value = lblInsuranceAmt.Text;
                                    pGR_discount.Value = lblDiscountAmt.Text;
                                    pGR_otherDeduction.Value = lblOthDeduction.Text;
                                    pGR_packinChrgs.Value = lblPacking.Text;


                                    pGR_frieghtCurr.Value = lblFreightCurr.Text;
                                    pGR_insuranceCurr.Value = lblInsuranceCurr.Text;
                                    pGR_discountCurr.Value = lblDiscountCurr.Text;
                                    pGR_CommCurr.Value = lblCommCurr.Text;
                                    pGR_otherDeductionCurr.Value = lblOthDeductionCurr.Text;
                                    pGR_packinChrgsCurr.Value = lblPackingCurr.Text;

                                    pGR_GRExchRate.Value = lblGRExchRate.Text;

                                }

                                s = objSaveGRCustomsDetails.SaveDeleteData(_query, pGRbCode, pGRDocNo, pGRno, pGRCurrency, pGRAmount,
                                                pGRShippingBillNo, pGRShippingBillDate, pGRCommission, pGRPortCode, pGRPFormType,
                                                pGRdispind, pGRinvoiceno, pGRinvoiceDate, pGRinvoiceAmt,
                                                pGR_frieght, pGR_insurance, pGR_discount, pGR_otherDeduction, pGR_packinChrgs,
                                                pGR_frieghtCurr, pGR_insuranceCurr, pGR_discountCurr, pGR_CommCurr, pGR_otherDeductionCurr, pGR_packinChrgsCurr, invsrno, status, pGR_GRExchRate
                                                );
                                rowIndex++;
                            }
                        }
                    }

                    _script = "";
                    string _OldValues = "";// Added by Anand 04-01-2024
                    string _NewValues = "";// Added by Anand 04-01-2024
                    string _Status = "M";

                    var argument = ((Button)sender).CommandArgument;

                    if (_result.Substring(0, 5) == "added")
                    {
                        AuditDocument(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 04-01-2024
                        AuditTransaction(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                        AuditGBase(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                        AuditGRPP(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 08-01-2024
                        if (argument.ToString() == "save")
                        {
                            _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=save&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                        else
                        {
                            _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }
                    else
                    {
                        if (_result == "updated")
                        {
                            AuditDocument(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 04-01-2024
                            AuditTransaction(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                            AuditGBase(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 06-01-2024
                            // AuditGRPP(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);// Added by Anand 08-01-2024
                            if (argument.ToString() == "save")
                            {
                                _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=save&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            }
                            else
                            {
                                _script = "window.location='EXP_ViewExportBillEntry.aspx?result=" + _result + "&saveType=print&frm=" + txtDateRcvd.Text.Trim() + "&rptType=" + "Single" + "&rptFrdocno=" + txtDocumentNo.Text.Trim() + "&rptTodocno=" + txtDocumentNo.Text.Trim() + "&rptCode=" + "2" + "&Branch=" + Session["userADCode"].ToString() + "&Report=" + "Export Bill lodgement'";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                            }
                        }
                        else
                            labelMessage.Text = _result;
                    }
                }
            }
        }
    }

    private string getPCsrNo(string bName, string AcNo, string SubAcNo)
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

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewExportBillEntry.aspx", true);
    }

    protected void fillDetails(string _DocNo)
    {
        DDL_Dispatch();

        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = _DocNo;
        string _query = "TF_EXP_GetExportBillEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //------------------Document Details---------------------//

            if (dt.Rows[0]["IsRealised"].ToString().Trim() == "YES")
            {
                //labelMessage.Font.Bold = true;
                labelMessage.Font.Size = 12;
                labelMessage.Text = "This Document is already Realised, You cannot update.";
                hdnIsRealised.Value = "YES";
            }
            else if (dt.Rows[0]["IsDelinked"].ToString().Trim() == "YES")
            {
                labelMessage.Font.Size = 12;
                labelMessage.Text = "This Document is already Delinked, You cannot update.";
                hdnIsDelinked.Value = "YES";
            }
            else if (dt.Rows[0]["IsWrittenOff"].ToString().Trim() == "YES")
            {
                labelMessage.Font.Size = 12;
                labelMessage.Text = "This Document is already Written Off, You cannot update.";
                hdnIsWrittenOff.Value = "YES";
            }
            else if (dt.Rows[0]["IsExtended"].ToString().Trim() == "YES")
            {
                string _ExtentedUpto = dt.Rows[0]["ExtentedUpto"].ToString().Trim();
                labelMessage.Font.Size = 12;
                labelMessage.Text = "This Document is already Extended upto " + _ExtentedUpto + " , You cannot update.";
                hdnIsExtended.Value = "YES";
            }
            else
            {
                hdnIsDelinked.Value = "NO";
                hdnIsRealised.Value = "NO";
                hdnIsExtended.Value = "NO";
                hdnIsWrittenOff.Value = "NO";
            }

            // txtDocDate.Text = dt.Rows[0]["Remitting_Bank_Ref_No"].ToString().Trim();
            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            fillCustomerCodeDescription();
            txtOverseasPartyID.Text = dt.Rows[0]["Overseas_Party_Code"].ToString().Trim();
            fillOverseasPartyDescription();
            txtOverseasBankID.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString().Trim();
            fillOverseasBankDescription();

            txtDateRcvd.Text = dt.Rows[0]["Date_Received"].ToString().Trim();
            txtENCdate.Text = dt.Rows[0]["ENC_Date"].ToString().Trim();
            txtDateNegotiated.Text = dt.Rows[0]["Date_Negotiated"].ToString().Trim();

            //string _chkCustomer = dt.Rows[0]["CUSTCHECK"].ToString().Trim();
            //if (_chkCustomer == "C")
            //    chkCustCheck.Checked = true;
            //else
            //    chkCustCheck.Checked = false;

            //string _chkCS = dt.Rows[0]["CS"].ToString().Trim();
            //if (_chkCS == "Y")
            //    chkCS.Checked = true;
            //else
            //    chkCS.Checked = false;

            txtLCNo.Text = dt.Rows[0]["LC_NO"].ToString().Trim();
            txtLCNoDate.Text = dt.Rows[0]["LC_Date"].ToString().Trim();
            txtLCNoIssuedBy.Text = dt.Rows[0]["LC_Issued_By"].ToString().Trim();

            txtBENo.Text = dt.Rows[0]["DD_NO"].ToString().Trim();
            txtBEDate.Text = dt.Rows[0]["DD_Date"].ToString().Trim();

            string _bankLine = dt.Rows[0]["BankLine"].ToString().Trim();
            if (_bankLine == "Y")
                chkBankLineTransfer.Checked = true;
            txtAccpDate.Text = dt.Rows[0]["AccpDate"].ToString().Trim();
            txtBENoDoc.Text = dt.Rows[0]["DD_Doc"].ToString().Trim();

            txtInvoiceNo.Text = dt.Rows[0]["Invoice_No"].ToString().Trim();
            txtInvoiceDate.Text = dt.Rows[0]["Invoice_Date"].ToString().Trim();
            txtInvoiceDoc.Text = dt.Rows[0]["Invoice_Doc"].ToString().Trim();

            if (dt.Rows[0]["DispIndc"].ToString().Trim() != "")
            {
                ddlDispachInd.ClearSelection();
                ddlDispachInd.SelectedValue = dt.Rows[0]["DispIndc"].ToString().Trim();
            }
            DDL_Dispatch2();
            //txtDispBydefault.Text = dt.Rows[0]["DispByDefaltuValue"].ToString().Trim();

            //if (ddlDispachInd.SelectedValue == "Dispatched directly by exporter")
            //{
            //    ddlDispBydefault.SelectedValue = "Non-Dispatch";
            //}
            //else
            //{
            ddlDispBydefault.SelectedValue = dt.Rows[0]["DispByDefaltuValue"].ToString().Trim();
            // }
            ddlMercTrade.SelectedValue = dt.Rows[0]["MerchantTrade"].ToString().Trim();

            txtAWBno.Text = dt.Rows[0]["AWB_No"].ToString().Trim();
            txtAWBDate.Text = dt.Rows[0]["AWB_Date"].ToString().Trim();
            txtAwbIssuedBy.Text = dt.Rows[0]["AWB_Issued_By"].ToString().Trim();
            txtAWBDoc.Text = dt.Rows[0]["AWB_Doc"].ToString().Trim();

            txtPackingList.Text = dt.Rows[0]["Packing_List"].ToString().Trim();
            txtPackingListDate.Text = dt.Rows[0]["Packing_List_Date"].ToString().Trim();
            txtPackingDoc.Text = dt.Rows[0]["Packing_List_Doc"].ToString().Trim();

            txtCertOfOrigin.Text = dt.Rows[0]["Cert_Of_Origin"].ToString().Trim();
            txtCertIssuedBy.Text = dt.Rows[0]["Cert_Of_Origin_Issued_By"].ToString().Trim();
            txtCertOfOriginDoc.Text = dt.Rows[0]["Cert_Of_Origin_Doc"].ToString().Trim();

            txtCustomsInvoice.Text = dt.Rows[0]["Customs_Invoice"].ToString().Trim();
            txtCustomsDate.Text = dt.Rows[0]["Customs_Invoice_Date"].ToString().Trim();
            txtCommodityID.Text = dt.Rows[0]["Commodity"].ToString().Trim();
            //  txtPreviousCommodity.Text = dt.Rows[0]["Commodity"].ToString().Trim();
            fillCommodityDescription();
            txtCustomsDoc.Text = dt.Rows[0]["Customs_Invoice_Doc"].ToString().Trim();

            txtInsPolicy.Text = dt.Rows[0]["Ins_Policy"].ToString().Trim();
            txtInsPolicyDate.Text = dt.Rows[0]["Ins_Policy_Date"].ToString().Trim();
            txtInsPolicyIssuedBy.Text = dt.Rows[0]["Ins_Policy_Issued_By"].ToString().Trim();
            txtInsPolicyDoc.Text = dt.Rows[0]["Ins_Policy_Doc"].ToString().Trim();

            txtGSPDate.Text = dt.Rows[0]["GSP_Date"].ToString().Trim();
            txtContractNo.Text = dt.Rows[0]["Contract_No"].ToString().Trim();
            txtRate.Text = dt.Rows[0]["Contract_Rate"].ToString().Trim();
            txtGSPDoc.Text = dt.Rows[0]["GSP_Doc"].ToString().Trim();

            txtFIRCno.Text = dt.Rows[0]["FIRC_NO"].ToString().Trim();
            txtFIRCdate.Text = dt.Rows[0]["FIRC_DATE"].ToString().Trim();
            txtFIRCnoIssuedBy.Text = dt.Rows[0]["FIRC_Issued_By"].ToString().Trim();
            txtFIRCdoc.Text = dt.Rows[0]["FIRC_Doc"].ToString().Trim();

            txtMiscellaneous.Text = dt.Rows[0]["Miscellaneous"].ToString().Trim();
            txtMiscDoc.Text = dt.Rows[0]["Miscellaneous_Doc"].ToString().Trim();

            string _shipment = dt.Rows[0]["Steamer"].ToString().Trim();
            if (_shipment == "A")
                rdbByAir.Checked = true;
            if (_shipment == "S")
                rdbBySea.Checked = true;
            if (_shipment == "R")
                rdbByRoad.Checked = true;


            //txtCoveringFrom.Text = dt.Rows[0]["Covering_From"].ToString().Trim();
            txtCoveringTo.Text = dt.Rows[0]["Covering_To"].ToString().Trim();

            if (dt.Rows[0]["Country_Code"].ToString().Trim() != "")
            {
                txtCountry.Text = dt.Rows[0]["Country_Code"].ToString().Trim();
                txtCountry_TextChanged(null, null);
            }
            else
            {
                txtCountry.Text = "";
            }

            txtReimbValDate.Text = dt.Rows[0]["Reimbursement_ValDate"].ToString().Trim();
            txtReimbClaimDate.Text = dt.Rows[0]["Reimbursement_Claim_Date"].ToString().Trim();
            txtBkName.Text = dt.Rows[0]["Reimbursement_Bank_Name"].ToString().Trim();
            txtBIC.Text = dt.Rows[0]["Reimbursement_BankBICCode"].ToString().Trim();
            txtReimbAmt.Text = dt.Rows[0]["Reimbursement_Amount"].ToString().Trim();

            txtTTRefNo1.Text = dt.Rows[0]["TTREFNO1"].ToString().Trim();
            txtTTRefNo2.Text = dt.Rows[0]["TTREFNO2"].ToString().Trim();
            txtTTRefNo3.Text = dt.Rows[0]["TTREFNO3"].ToString().Trim();
            txtTTRefNo4.Text = dt.Rows[0]["TTREFNO4"].ToString().Trim();
            txtTTRefNo5.Text = dt.Rows[0]["TTREFNO5"].ToString().Trim();
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

            if (txtTTRefNo1.Text != "")
            {
                TTRemitterName(txtTTRefNo1.Text, "1");
            }
            if (txtTTRefNo2.Text != "")
            {
                TTRemitterName(txtTTRefNo2.Text, "2");
            }
            if (txtTTRefNo3.Text != "")
            {
                TTRemitterName(txtTTRefNo3.Text, "3");
            }
            if (txtTTRefNo4.Text != "")
            {
                TTRemitterName(txtTTRefNo4.Text, "4");
            }
            if (txtTTRefNo5.Text != "")
            {
                TTRemitterName(txtTTRefNo5.Text, "5");
            }
            if (txtTTRefNo6.Text != "")
            {
                TTRemitterName(txtTTRefNo6.Text, "6");
            }
            if (txtTTRefNo7.Text != "")
            {
                TTRemitterName(txtTTRefNo7.Text, "7");
            }
            if (txtTTRefNo8.Text != "")
            {
                TTRemitterName(txtTTRefNo8.Text, "8");
            }
            if (txtTTRefNo9.Text != "")
            {
                TTRemitterName(txtTTRefNo9.Text, "9");
            }
            if (txtTTRefNo10.Text != "")
            {
                TTRemitterName(txtTTRefNo10.Text, "10");
            }
            if (txtTTRefNo11.Text != "")
            {
                TTRemitterName(txtTTRefNo11.Text, "11");
            }
            if (txtTTRefNo12.Text != "")
            {
                TTRemitterName(txtTTRefNo12.Text, "12");
            }
            if (txtTTRefNo13.Text != "")
            {
                TTRemitterName(txtTTRefNo13.Text, "13");
            }
            if (txtTTRefNo14.Text != "")
            {
                TTRemitterName(txtTTRefNo14.Text, "14");
            }
            if (txtTTRefNo15.Text != "")
            {
                TTRemitterName(txtTTRefNo15.Text, "15");
            }


            txtTTAmount1.Text = dt.Rows[0]["TTAmt1"].ToString().Trim();
            txtTTAmount2.Text = dt.Rows[0]["TTAmt2"].ToString().Trim();
            txtTTAmount3.Text = dt.Rows[0]["TTAmt3"].ToString().Trim();
            txtTTAmount4.Text = dt.Rows[0]["TTAmt4"].ToString().Trim();
            txtTTAmount5.Text = dt.Rows[0]["TTAmt5"].ToString().Trim();
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

            txtFIRCNo1_OB.Text = dt.Rows[0]["FIRCNo_OtrBank1"].ToString().Trim();
            txtFIRCDate1_OB.Text = dt.Rows[0]["FIRCDate_OtrBank1"].ToString().Trim();
            txtFIRCAmount1_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank1"].ToString().Trim();
            txtFIRCADCode1_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank1"].ToString().Trim();

            txtFIRCNo2_OB.Text = dt.Rows[0]["FIRCNo_OtrBank2"].ToString().Trim();
            txtFIRCDate2_OB.Text = dt.Rows[0]["FIRCDate_OtrBank2"].ToString().Trim();
            txtFIRCAmount2_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank2"].ToString().Trim();
            txtFIRCADCode2_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank2"].ToString().Trim();

            txtFIRCNo3_OB.Text = dt.Rows[0]["FIRCNo_OtrBank3"].ToString().Trim();
            txtFIRCDate3_OB.Text = dt.Rows[0]["FIRCDate_OtrBank3"].ToString().Trim();
            txtFIRCAmount3_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank3"].ToString().Trim();
            txtFIRCADCode3_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank3"].ToString().Trim();

            txtFIRCNo4_OB.Text = dt.Rows[0]["FIRCNo_OtrBank4"].ToString().Trim();
            txtFIRCDate4_OB.Text = dt.Rows[0]["FIRCDate_OtrBank4"].ToString().Trim();
            txtFIRCAmount4_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank4"].ToString().Trim();
            txtFIRCADCode4_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank4"].ToString().Trim();

            txtFIRCNo5_OB.Text = dt.Rows[0]["FIRCNo_OtrBank5"].ToString().Trim();
            txtFIRCDate5_OB.Text = dt.Rows[0]["FIRCDate_OtrBank5"].ToString().Trim();
            txtFIRCAmount5_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank5"].ToString().Trim();
            txtFIRCADCode5_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank5"].ToString().Trim();

            txtFIRCNo6_OB.Text = dt.Rows[0]["FIRCNo_OtrBank6"].ToString().Trim();
            txtFIRCDate6_OB.Text = dt.Rows[0]["FIRCDate_OtrBank6"].ToString().Trim();
            txtFIRCAmount6_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank6"].ToString().Trim();
            txtFIRCADCode6_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank6"].ToString().Trim();

            txtFIRCNo7_OB.Text = dt.Rows[0]["FIRCNo_OtrBank7"].ToString().Trim();
            txtFIRCDate7_OB.Text = dt.Rows[0]["FIRCDate_OtrBank7"].ToString().Trim();
            txtFIRCAmount7_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank7"].ToString().Trim();
            txtFIRCADCode7_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank7"].ToString().Trim();

            txtFIRCNo8_OB.Text = dt.Rows[0]["FIRCNo_OtrBank8"].ToString().Trim();
            txtFIRCDate8_OB.Text = dt.Rows[0]["FIRCDate_OtrBank8"].ToString().Trim();
            txtFIRCAmount8_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank8"].ToString().Trim();
            txtFIRCADCode8_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank8"].ToString().Trim();

            txtFIRCNo9_OB.Text = dt.Rows[0]["FIRCNo_OtrBank9"].ToString().Trim();
            txtFIRCDate9_OB.Text = dt.Rows[0]["FIRCDate_OtrBank9"].ToString().Trim();
            txtFIRCAmount9_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank9"].ToString().Trim();
            txtFIRCADCode9_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank9"].ToString().Trim();

            txtFIRCNo10_OB.Text = dt.Rows[0]["FIRCNo_OtrBank10"].ToString().Trim();
            txtFIRCDate10_OB.Text = dt.Rows[0]["FIRCDate_OtrBank10"].ToString().Trim();
            txtFIRCAmount10_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank10"].ToString().Trim();
            txtFIRCADCode10_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank10"].ToString().Trim();

            txtFIRCNo11_OB.Text = dt.Rows[0]["FIRCNo_OtrBank11"].ToString().Trim();
            txtFIRCDate11_OB.Text = dt.Rows[0]["FIRCDate_OtrBank11"].ToString().Trim();
            txtFIRCAmount11_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank11"].ToString().Trim();
            txtFIRCADCode11_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank11"].ToString().Trim();

            txtFIRCNo12_OB.Text = dt.Rows[0]["FIRCNo_OtrBank12"].ToString().Trim();
            txtFIRCDate12_OB.Text = dt.Rows[0]["FIRCDate_OtrBank12"].ToString().Trim();
            txtFIRCAmount12_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank12"].ToString().Trim();
            txtFIRCADCode12_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank12"].ToString().Trim();

            txtFIRCNo13_OB.Text = dt.Rows[0]["FIRCNo_OtrBank13"].ToString().Trim();
            txtFIRCDate13_OB.Text = dt.Rows[0]["FIRCDate_OtrBank13"].ToString().Trim();
            txtFIRCAmount13_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank13"].ToString().Trim();
            txtFIRCADCode13_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank13"].ToString().Trim();

            txtFIRCNo14_OB.Text = dt.Rows[0]["FIRCNo_OtrBank14"].ToString().Trim();
            txtFIRCDate14_OB.Text = dt.Rows[0]["FIRCDate_OtrBank14"].ToString().Trim();
            txtFIRCAmount14_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank14"].ToString().Trim();
            txtFIRCADCode14_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank14"].ToString().Trim();

            txtFIRCNo15_OB.Text = dt.Rows[0]["FIRCNo_OtrBank15"].ToString().Trim();
            txtFIRCDate15_OB.Text = dt.Rows[0]["FIRCDate_OtrBank15"].ToString().Trim();
            txtFIRCAmount15_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank15"].ToString().Trim();
            txtFIRCADCode15_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank15"].ToString().Trim();


            txtIntFrmDate1.Text = dt.Rows[0]["IntFromDate1"].ToString().Trim();
            txtIntToDate1.Text = dt.Rows[0]["IntToDate1"].ToString().Trim();
            txtIntFrmDate2.Text = dt.Rows[0]["IntFromDate2"].ToString().Trim();
            txtIntToDate2.Text = dt.Rows[0]["IntToDate2"].ToString().Trim();
            txtIntFrmDate3.Text = dt.Rows[0]["IntFromDate3"].ToString().Trim();
            txtIntToDate3.Text = dt.Rows[0]["IntToDate3"].ToString().Trim();
            txtIntFrmDate4.Text = dt.Rows[0]["IntFromDate4"].ToString().Trim();
            txtIntToDate4.Text = dt.Rows[0]["IntToDate4"].ToString().Trim();
            txtIntFrmDate5.Text = dt.Rows[0]["IntFromDate5"].ToString().Trim();
            txtIntToDate5.Text = dt.Rows[0]["IntToDate5"].ToString().Trim();
            txtIntFrmDate6.Text = dt.Rows[0]["IntFromDate6"].ToString().Trim();
            txtIntToDate6.Text = dt.Rows[0]["IntToDate6"].ToString().Trim();

            //////string _ttDocNo = dt.Rows[0]["TTREFNO"].ToString().Trim();
            //////if (_ttDocNo != "")
            //////{
            //////    //txtTTprFx.Text = _ttDocNo.Substring(0, 2);
            //////    //txtTTdocNo.Text = _ttDocNo.Substring(3, 5);
            //////    //txtTTbranchCode.Text = _ttDocNo.Substring(9, 5);
            //////    //txtTTyear.Text = _ttDocNo.Substring(15, 4);
            //////    hdnTTAmount.Value = dt.Rows[0]["TTAmount"].ToString().Trim();
            //////}

            txtNotes.Text = dt.Rows[0]["Document_Remarks"].ToString().Trim();

            string Adcode = dt.Rows[0]["ADCode"].ToString().Trim();
            if (Adcode != "")
            {
                ADCode.Visible = true;
                txtADCode.Text = Adcode;
                rdbotherAdcode.Checked = true;

            }
            else
            {
                ADCode.Visible = false;
                rdbOurADCode.Checked = true;
                txtADCode.Text = "";

            }
            txtconsigneePartyID.Text = dt.Rows[0]["consigneePartyID"].ToString().Trim();//-------------------------------Nilesh 04/08//2023
            fillConsigneePartyDescription();//--------------------------------------------------------------------------Nilesh 04/08//2023
            //------------------End Document Details---------------------//

            //------------------Transaction Details---------------------//

            string _sightUsance = dt.Rows[0]["Bill_Type"].ToString().Trim();
            if (_sightUsance == "S")
                rbtnSightBill.Checked = true;
            if (_sightUsance == "U")
                rbtnUsanceBill.Checked = true;

            if (dt.Rows[0]["Currency"].ToString().Trim() != "")
            {
                ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
            }
            else
                ddlCurrency.SelectedIndex = -1;

            string _loanAdv = dt.Rows[0]["Loan"].ToString().Trim();
            if (_loanAdv == "Y")
                chkLoanAdv.Checked = true;

            if (dt.Rows[0]["Other_Currency_For_INR"].ToString().Trim() != "")
            {
                ddlOtherCurrency.SelectedValue = dt.Rows[0]["Other_Currency_For_INR"].ToString().Trim();
            }
            else
                ddlOtherCurrency.SelectedIndex = -1;

            txtExchRate.Text = dt.Rows[0]["Exchange_Rate"].ToString().Trim();
            txtNoOfDays.Text = dt.Rows[0]["Days"].ToString().Trim();

            string _afterFrom = dt.Rows[0]["After_From"].ToString().Trim();
            if (_afterFrom == "A")
                rbtnAfterAWB.Checked = true;
            if (_afterFrom == "F")
                rbtnFromAWB.Checked = true;
            if (_afterFrom == "X")
                rbtnSight.Checked = true;
            if (_afterFrom == "Y")
                rbtnDA.Checked = true;
            if (_afterFrom == "I")
                rbtnFromInvoice.Checked = true;
            if (_afterFrom == "O")
                rbtnOthers.Checked = true;
            txtOtherTenorRemarks.Text = dt.Rows[0]["Other_Tenor_Remarks"].ToString().Trim();
            txtLibor.Text = dt.Rows[0]["LIBOR"].ToString().Trim();
            if (txtDocType.Text == "C")
            {
                if (txtLibor.Text != "" && txtLibor.Text != (0).ToString())
                    rbtnEBR.Checked = true;
                else
                    rbtnBDR.Checked = true;
            }
            txtIntRate1.Text = dt.Rows[0]["Interest_Rate_1"].ToString().Trim();
            txtForDays1.Text = dt.Rows[0]["For_Days_1"].ToString().Trim();

            txtIntRate2.Text = dt.Rows[0]["Interest_Rate_2"].ToString().Trim();
            txtForDays2.Text = dt.Rows[0]["For_Days_2"].ToString().Trim();

            txtIntRate3.Text = dt.Rows[0]["Interest_Rate_3"].ToString().Trim();
            txtForDays3.Text = dt.Rows[0]["For_Days_3"].ToString().Trim();

            txtIntRate4.Text = dt.Rows[0]["Interest_Rate_4"].ToString().Trim();
            txtForDays4.Text = dt.Rows[0]["For_Days_4"].ToString().Trim();

            txtIntRate5.Text = dt.Rows[0]["Interest_Rate_5"].ToString().Trim();
            txtForDays5.Text = dt.Rows[0]["For_Days_5"].ToString().Trim();

            txtIntRate6.Text = dt.Rows[0]["Interest_Rate_6"].ToString().Trim();
            txtForDays6.Text = dt.Rows[0]["For_Days_6"].ToString().Trim();

            txtOutOfDays.Text = dt.Rows[0]["Out_Of_Days"].ToString().Trim();
            txtDueDate.Text = dt.Rows[0]["Due_Date"].ToString().Trim();

            txtAcceptedDueDate.Text = dt.Rows[0]["Accepted_Due_Date"].ToString().Trim();

            txtBillAmount.Text = dt.Rows[0]["ActBillAmt"].ToString().Trim();
            txtBillAmountinRS.Text = dt.Rows[0]["ActBillAmt_InRs"].ToString().Trim();
            txtNegotiatedAmt.Text = dt.Rows[0]["Bill_Amount"].ToString().Trim();
            txtNegotiatedAmtinRS.Text = dt.Rows[0]["Bill_Amount_In_Rs"].ToString().Trim();
            txtInterest.Text = dt.Rows[0]["Interest"].ToString().Trim();
            txtInterestinRS.Text = dt.Rows[0]["Interest_In_Rs"].ToString().Trim();
            txtNetAmt.Text = dt.Rows[0]["Net_Amount"].ToString().Trim();
            txtNetAmtinRS.Text = dt.Rows[0]["Net_Amount_In_Rs"].ToString().Trim();
            txtExchRtEBR.Text = dt.Rows[0]["Exchange_Rate_EBR"].ToString().Trim();

            txtOtherChrgs.Text = dt.Rows[0]["Other_Charges"].ToString().Trim();

            if (dt.Rows[0]["STax_FxDls"].ToString().Trim() != "" && dt.Rows[0]["STax_FxDls"].ToString().Trim() != "0")
            {
                chkApplicable.Checked = true;
                txtSTFXDLS.Text = dt.Rows[0]["STax_FxDls"].ToString().Trim();
            }

            txtBankCert.Text = dt.Rows[0]["Bank_Cert"].ToString().Trim();
            txtNegotiationFees.Text = dt.Rows[0]["Negotiation_Fees"].ToString().Trim();
            txtCourierChrgs.Text = dt.Rows[0]["Courier_Charges"].ToString().Trim();
            txtMarginAmt.Text = dt.Rows[0]["Margin_Amount"].ToString().Trim();
            txtCommission.Text = dt.Rows[0]["Commission_Amount"].ToString().Trim();

            if (dt.Rows[0]["STax"].ToString().Trim() != "")
            {
                ddlServiceTax.SelectedValue = dt.Rows[0]["STax"].ToString().Trim();
            }
            else
                ddlServiceTax.SelectedIndex = -1;

            txtSTaxAmount.Text = dt.Rows[0]["STaxAmt"].ToString().Trim();

            txtCurrentAcinRS.Text = dt.Rows[0]["Current_Account_Amount_In_Rs"].ToString().Trim();

            string _chkRBI = dt.Rows[0]["chkRBI"].ToString().Trim();
            if (_chkRBI == "True")
                chkRBI.Checked = true;
            else
                chkRBI.Checked = false;


            //swatch

            txtsbcess.Text = dt.Rows[0]["SBCess"].ToString();

            if (dt.Rows[0]["SBCessAmt"].ToString() != "")
            {
                txtSBcesssamt.Text = Convert.ToDecimal(dt.Rows[0]["SBCessAmt"]).ToString("0.00");
            }

            else
            {
                txtSBcesssamt.Text = dt.Rows[0]["SBCessAmt"].ToString();
            }


            txt_kkcessper.Text = dt.Rows[0]["KKcess"].ToString();

            if (dt.Rows[0]["KKcessamt"].ToString() != "")
            {
                txt_kkcessamt.Text = Convert.ToDecimal(dt.Rows[0]["KKcessamt"]).ToString("0.00");
            }
            else
            {
                txt_kkcessamt.Text = dt.Rows[0]["KKcessamt"].ToString();
            }

            if (dt.Rows[0]["STaxTotalamt"].ToString() != "")
            {
                txtsttamt.Text = Convert.ToDecimal(dt.Rows[0]["STaxTotalamt"]).ToString("0.00");
            }
            else
            {
                txtsttamt.Text = dt.Rows[0]["STaxTotalamt"].ToString();
            }

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
                txttotsbcess.Text = Convert.ToDecimal(dt.Rows[0]["TotalSBcessFxDls"]).ToString("0.00");
            }
            else
            {
                txttotsbcess.Text = dt.Rows[0]["TotalSBcessFxDls"].ToString();
            }

            //fbk

            txt_fbkcharges.Text = dt.Rows[0]["Fbkcharges"].ToString();
            ddlServiceTaxfbk.Text = dt.Rows[0]["Staxfbk"].ToString();

            if (dt.Rows[0]["Staxfbkamt"].ToString() != "")
            {
                txtSTaxAmountfbk.Text = Convert.ToDecimal(dt.Rows[0]["Staxfbkamt"]).ToString("0.00");
            }
            else
            {
                txtSTaxAmountfbk.Text = dt.Rows[0]["Staxfbkamt"].ToString();
            }

            txtsbcessfbk.Text = dt.Rows[0]["SBcessfbk"].ToString();

            if (dt.Rows[0]["SBcessfbkamt"].ToString() != "")
            {
                txtSBcesssamtfbk.Text = Convert.ToDecimal(dt.Rows[0]["SBcessfbkamt"]).ToString("0.00");
            }

            else
            {
                txtSBcesssamtfbk.Text = dt.Rows[0]["SBcessfbkamt"].ToString();
            }

            txt_kkcessperfbk.Text = dt.Rows[0]["KKcessfbk"].ToString();

            if (dt.Rows[0]["KKcessfbkamt"].ToString() != "")
            {
                txt_kkcessamtfbk.Text = Convert.ToDecimal(dt.Rows[0]["KKcessfbkamt"]).ToString("0.00");
            }
            else
            {
                txt_kkcessamtfbk.Text = dt.Rows[0]["KKcessfbkamt"].ToString();
            }

            if (dt.Rows[0]["Totfbkamt"].ToString() != "")
            {
                txtsttamtfbk.Text = Convert.ToDecimal(dt.Rows[0]["Totfbkamt"]).ToString("0.00");
            }
            else
            {
                txtsttamtfbk.Text = dt.Rows[0]["Totfbkamt"].ToString();
            }

            //-------------------------Anand10-06-2023--------------------------------

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

            ddlFIRCCurrency1_OB.SelectedValue = dt.Rows[0]["FIRCCurrency1_OB"].ToString().Trim();
            ddlFIRCCurrency2_OB.SelectedValue = dt.Rows[0]["FIRCCurrency2_OB"].ToString().Trim();
            ddlFIRCCurrency3_OB.SelectedValue = dt.Rows[0]["FIRCCurrency3_OB"].ToString().Trim();
            ddlFIRCCurrency4_OB.SelectedValue = dt.Rows[0]["FIRCCurrency4_OB"].ToString().Trim();
            ddlFIRCCurrency5_OB.SelectedValue = dt.Rows[0]["FIRCCurrency5_OB"].ToString().Trim();
            ddlFIRCCurrency6_OB.SelectedValue = dt.Rows[0]["FIRCCurrency6_OB"].ToString().Trim();
            ddlFIRCCurrency7_OB.SelectedValue = dt.Rows[0]["FIRCCurrency7_OB"].ToString().Trim();
            ddlFIRCCurrency8_OB.SelectedValue = dt.Rows[0]["FIRCCurrency8_OB"].ToString().Trim();
            ddlFIRCCurrency9_OB.SelectedValue = dt.Rows[0]["FIRCCurrency9_OB"].ToString().Trim();
            ddlFIRCCurrency10_OB.SelectedValue = dt.Rows[0]["FIRCCurrency10_OB"].ToString().Trim();
            ddlFIRCCurrency11_OB.SelectedValue = dt.Rows[0]["FIRCCurrency11_OB"].ToString().Trim();
            ddlFIRCCurrency12_OB.SelectedValue = dt.Rows[0]["FIRCCurrency12_OB"].ToString().Trim();
            ddlFIRCCurrency13_OB.SelectedValue = dt.Rows[0]["FIRCCurrency13_OB"].ToString().Trim();
            ddlFIRCCurrency14_OB.SelectedValue = dt.Rows[0]["FIRCCurrency14_OB"].ToString().Trim();
            ddlFIRCCurrency15_OB.SelectedValue = dt.Rows[0]["FIRCCurrency15_OB"].ToString().Trim();


            ddlFIRCRealisedCurr1_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr1_OB"].ToString().Trim();
            ddlFIRCRealisedCurr2_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr2_OB"].ToString().Trim();
            ddlFIRCRealisedCurr3_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr3_OB"].ToString().Trim();
            ddlFIRCRealisedCurr4_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr4_OB"].ToString().Trim();
            ddlFIRCRealisedCurr5_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr5_OB"].ToString().Trim();
            ddlFIRCRealisedCurr6_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr6_OB"].ToString().Trim();
            ddlFIRCRealisedCurr7_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr7_OB"].ToString().Trim();
            ddlFIRCRealisedCurr8_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr8_OB"].ToString().Trim();
            ddlFIRCRealisedCurr9_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr9_OB"].ToString().Trim();
            ddlFIRCRealisedCurr10_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr10_OB"].ToString().Trim();
            ddlFIRCRealisedCurr11_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr11_OB"].ToString().Trim();
            ddlFIRCRealisedCurr12_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr12_OB"].ToString().Trim();
            ddlFIRCRealisedCurr13_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr13_OB"].ToString().Trim();
            ddlFIRCRealisedCurr14_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr14_OB"].ToString().Trim();
            ddlFIRCRealisedCurr15_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr15_OB"].ToString().Trim();


            txtFIRCCrossCurrRate1_OB.Text = dt.Rows[0]["FIRCCrossCurrRate1_OB"].ToString().Trim();
            txtFIRCCrossCurrRate2_OB.Text = dt.Rows[0]["FIRCCrossCurrRate2_OB"].ToString().Trim();
            txtFIRCCrossCurrRate3_OB.Text = dt.Rows[0]["FIRCCrossCurrRate3_OB"].ToString().Trim();
            txtFIRCCrossCurrRate4_OB.Text = dt.Rows[0]["FIRCCrossCurrRate4_OB"].ToString().Trim();
            txtFIRCCrossCurrRate5_OB.Text = dt.Rows[0]["FIRCCrossCurrRate5_OB"].ToString().Trim();
            txtFIRCCrossCurrRate6_OB.Text = dt.Rows[0]["FIRCCrossCurrRate6_OB"].ToString().Trim();
            txtFIRCCrossCurrRate7_OB.Text = dt.Rows[0]["FIRCCrossCurrRate7_OB"].ToString().Trim();
            txtFIRCCrossCurrRate8_OB.Text = dt.Rows[0]["FIRCCrossCurrRate8_OB"].ToString().Trim();
            txtFIRCCrossCurrRate9_OB.Text = dt.Rows[0]["FIRCCrossCurrRate9_OB"].ToString().Trim();
            txtFIRCCrossCurrRate10_OB.Text = dt.Rows[0]["FIRCCrossCurrRate10_OB"].ToString().Trim();
            txtFIRCCrossCurrRate11_OB.Text = dt.Rows[0]["FIRCCrossCurrRate11_OB"].ToString().Trim();
            txtFIRCCrossCurrRate12_OB.Text = dt.Rows[0]["FIRCCrossCurrRate12_OB"].ToString().Trim();
            txtFIRCCrossCurrRate13_OB.Text = dt.Rows[0]["FIRCCrossCurrRate13_OB"].ToString().Trim();
            txtFIRCCrossCurrRate14_OB.Text = dt.Rows[0]["FIRCCrossCurrRate14_OB"].ToString().Trim();
            txtFIRCCrossCurrRate15_OB.Text = dt.Rows[0]["FIRCCrossCurrRate15_OB"].ToString().Trim();


            txtFIRCTobeAdjustedinSB1_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB1_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB2_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB2_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB3_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB3_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB4_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB4_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB5_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB5_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB6_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB6_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB7_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB7_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB8_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB8_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB9_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB9_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB10_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB10_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB11_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB11_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB12_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB12_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB13_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB13_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB14_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB14_OB"].ToString().Trim();
            txtFIRCTobeAdjustedinSB15_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB15_OB"].ToString().Trim();

            //----------------------------------End-----------------------------------

            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }

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

            //------------------End Transaction Details---------------------//

            //------------------Covering Schedule Details---------------------//

            string _CB1 = dt.Rows[0]["Covering_Schedule_Text1"].ToString().Trim();
            if (_CB1 == "True")
                chk1.Checked = true;
            else
                chk1.Checked = false;

            string _CB2 = dt.Rows[0]["Covering_Schedule_Text2"].ToString().Trim();
            if (_CB2 == "True")
                chk2.Checked = true;
            else
                chk2.Checked = false;

            string _CB3 = dt.Rows[0]["Covering_Schedule_Text3"].ToString().Trim();
            if (_CB3 == "True")
                chk3.Checked = true;
            else
                chk3.Checked = false;

            string _CB4 = dt.Rows[0]["Covering_Schedule_Text4"].ToString().Trim();
            if (_CB4 == "True")
                chk4.Checked = true;
            else
                chk4.Checked = false;

            string _CB5 = dt.Rows[0]["Covering_Schedule_Text5"].ToString().Trim();
            if (_CB5 == "True")
                chk5.Checked = true;
            else
                chk5.Checked = false;

            string _CB6 = dt.Rows[0]["Covering_Schedule_Text6"].ToString().Trim();
            if (_CB6 == "True")
                chk6.Checked = true;
            else
                chk6.Checked = false;

            string _CB7 = dt.Rows[0]["Covering_Schedule_Text7"].ToString().Trim();
            if (_CB7 == "True")
                chk7.Checked = true;
            else
                chk7.Checked = false;

            string _CB7A = dt.Rows[0]["Covering_Schedule_Text7_A"].ToString().Trim();
            if (_CB7A == "True")
                chk7A.Checked = true;
            else
                chk7A.Checked = false;

            string _CB7B = dt.Rows[0]["Covering_Schedule_Text7_B"].ToString().Trim();
            if (_CB7B == "True")
                chk7B.Checked = true;
            else
                chk7B.Checked = false;

            string _CB8 = dt.Rows[0]["Covering_Schedule_Text8"].ToString().Trim();
            if (_CB8 == "True")
                chk8.Checked = true;
            else
                chk8.Checked = false;

            string _CB9 = dt.Rows[0]["Covering_Schedule_Text9"].ToString().Trim();
            if (_CB9 == "True")
                chk9.Checked = true;
            else
                chk9.Checked = false;

            string _CB10 = dt.Rows[0]["Covering_Schedule_Text10"].ToString().Trim();
            if (_CB10 == "True")
                chk10.Checked = true;
            else
                chk10.Checked = false;

            string _CB11 = dt.Rows[0]["Covering_Schedule_Text11"].ToString().Trim();
            if (_CB11 == "True")
                chk11.Checked = true;
            else
                chk11.Checked = false;

            string _CB12 = dt.Rows[0]["Covering_Schedule_Text12"].ToString().Trim();
            if (_CB12 == "True")
                chk12.Checked = true;
            else
                chk12.Checked = false;

            string _CB13 = dt.Rows[0]["Covering_Schedule_Text13"].ToString().Trim();
            if (_CB13 == "True")
                chk13.Checked = true;
            else
                chk13.Checked = false;

            txtRemark.Text = dt.Rows[0]["Remarks"].ToString().Trim();
            txtRemarks1.Text = dt.Rows[0]["Remarks1"].ToString().Trim();

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

            txtNoOfSB.Text = dt.Rows[0]["NoOfShippingBills"].ToString().Trim();
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

            txtCommissionID.Text = dt.Rows[0]["CommissionID"].ToString();

            SqlParameter _customerAccNo = new SqlParameter("@CustomerNo", txtCustAcNo.Text);
            SqlParameter _DateRec = new SqlParameter("@Date", txtDateRcvd.Text);
            SqlParameter _SrNo = new SqlParameter("@Sr_no", txtCommissionID.Text);

            string _Commission = "TF_Get_CommissionMaster_Details";
            DataTable dt3;
            dt3 = objData.getData(_Commission, _customerAccNo, _DateRec, _SrNo);
            if (dt3.Rows.Count > 0)
            {
                txtCommissionID.Text = dt3.Rows[0]["CommissionID"].ToString();
                hdnCommRate.Value = dt3.Rows[0]["CommissionRate"].ToString();
                hdnCommMinAmt.Value = dt3.Rows[0]["MinimumCommAmount"].ToString();
                hdnCommMaxAmt.Value = dt3.Rows[0]["MaxmumCommAmount"].ToString();
                hdnCommFlat.Value = dt3.Rows[0]["Flat"].ToString();
            }

            #region GBASE Details

            txtBENoDoc1.Text = dt.Rows[0]["DD_Doc1"].ToString().Trim();
            txtInvoiceDoc1.Text = dt.Rows[0]["Invoice_Doc1"].ToString().Trim();
            txtCustomsDoc1.Text = dt.Rows[0]["Customs_Invoice_Doc1"].ToString().Trim();
            txtInsPolicyDoc1.Text = dt.Rows[0]["Ins_Policy_Doc1"].ToString().Trim();
            txtCertOfOriginDoc1.Text = dt.Rows[0]["Cert_Of_Origin_Doc1"].ToString().Trim();
            txtPackingDoc1.Text = dt.Rows[0]["Packing_List_Doc1"].ToString().Trim();
            txtWM.Text = dt.Rows[0]["WM_Doc"].ToString().Trim();
            txtWM1.Text = dt.Rows[0]["WM_Doc1"].ToString().Trim();
            txtINSP.Text = dt.Rows[0]["INSP_Doc"].ToString().Trim();
            txtINSP1.Text = dt.Rows[0]["INSP_Doc1"].ToString().Trim();
            txtAWBDoc1.Text = dt.Rows[0]["AWB_Doc1"].ToString().Trim();
            txtOther.Text = dt.Rows[0]["OTHER_Doc"].ToString().Trim();
            txtOther1.Text = dt.Rows[0]["OTHER_Doc1"].ToString().Trim();
            txtGBaseCommodityID.Text = dt.Rows[0]["GBase_Commodity_ID"].ToString().Trim();
            fillGBaseCommodityDescription();
            txtOperationType.Text = dt.Rows[0]["Operation_Type"].ToString().Trim();
            txtSettlementOption.Text = dt.Rows[0]["Settlement_Option"].ToString().Trim();
            txtRiskCountry.Text = dt.Rows[0]["Risk_Country"].ToString().Trim();

            if (dt.Rows[0]["Payer"].ToString().Trim() == "S")
            {
                rdbBuyer.Checked = false;
                rdbShipper.Checked = true;
            }
            else
            {
                rdbShipper.Checked = false;
                rdbBuyer.Checked = true;
            }

            txtFundType.Text = dt.Rows[0]["Fund_Type"].ToString().Trim();
            txtBaseRate.Text = dt.Rows[0]["Base_Rate_Code"].ToString().Trim();
            txtGradeCode.Text = dt.Rows[0]["Grade_Code"].ToString().Trim();

            txtDirection.Text = dt.Rows[0]["Direction"].ToString().Trim();
            txtCovrInstr.Text = dt.Rows[0]["Covr_Instruction"].ToString().Trim();
            txtInternalRate.Text = dt.Rows[0]["Internal_Rate"].ToString().Trim();
            txtSpread.Text = dt.Rows[0]["Spread_Rate"].ToString().Trim();
            txtApplNo.Text = dt.Rows[0]["Application_No"].ToString().Trim();
            ddlRemEUC.SelectedValue = dt.Rows[0]["Remarks_EUC"].ToString().Trim();
            txtDraftNo.Text = dt.Rows[0]["Draft_No"].ToString().Trim();
            txtRiskCustomer.Text = dt.Rows[0]["Risk_Customer"].ToString().Trim();
            txtVesselName.Text = dt.Rows[0]["Vessel_Name"].ToString().Trim();
            ddlInstructions.SelectedValue = dt.Rows[0]["Instructions"].ToString().Trim();
            txtReimbBank.Text = dt.Rows[0]["Reimbursing_Bank"].ToString().Trim();
            fillReimbBankDescription();
            txtGBaseRemarks.Text = dt.Rows[0]["GBaseRemarks"].ToString().Trim();
            txtMerchandise.Text = dt.Rows[0]["Merchandise"].ToString().Trim();
            txtSpecialInstructions1.Text = dt.Rows[0]["Special_Instructions1"].ToString().Trim();
            txtSpecialInstructions2.Text = dt.Rows[0]["Special_Instructions2"].ToString().Trim();
            txtSpecialInstructions3.Text = dt.Rows[0]["Special_Instructions3"].ToString().Trim();
            txtSpecialInstructions4.Text = dt.Rows[0]["Special_Instructions4"].ToString().Trim();
            txtSpecialInstructions5.Text = dt.Rows[0]["Special_Instructions5"].ToString().Trim();
            txtSpecialInstructions6.Text = dt.Rows[0]["Special_Instructions6"].ToString().Trim();
            txtSpecialInstructions7.Text = dt.Rows[0]["Special_Instructions7"].ToString().Trim();
            txtSpecialInstructions8.Text = dt.Rows[0]["Special_Instructions8"].ToString().Trim();
            txtSpecialInstructions9.Text = dt.Rows[0]["Special_Instructions9"].ToString().Trim();
            txtSpecialInstructions10.Text = dt.Rows[0]["Special_Instructions10"].ToString().Trim();
            txtPrincipalContractNo1.Text = dt.Rows[0]["PrincipalContractNo1"].ToString().Trim();
            txtPrincipalContractNo2.Text = dt.Rows[0]["PrincipalContractNo2"].ToString().Trim();
            txtPrincipalExchCurr.Text = dt.Rows[0]["PrincipalExchCurr"].ToString().Trim();
            txtPrincipalExchRate.Text = dt.Rows[0]["PrincipalExchRate"].ToString().Trim();
            txtPrincipalIntExchRate.Text = dt.Rows[0]["PrincipalIntExchRate"].ToString().Trim();
            txtInterestLump.Text = dt.Rows[0]["InterestLump"].ToString().Trim();
            txtInterestContractNo1.Text = dt.Rows[0]["InterestContractNo1"].ToString().Trim();
            txtInterestContractNo2.Text = dt.Rows[0]["InterestContractNo2"].ToString().Trim();
            txtInterestExchCurr.Text = dt.Rows[0]["InterestExchCurr"].ToString().Trim();
            txtInterestExchRate.Text = dt.Rows[0]["InterestExchRate"].ToString().Trim();
            txtInterestIntExchRate.Text = dt.Rows[0]["InterestIntExchRate"].ToString().Trim();
            txtCommissionMatu.Text = dt.Rows[0]["CommissionMatu"].ToString().Trim();
            txtCommissionContractNo1.Text = dt.Rows[0]["CommissionContractNo1"].ToString().Trim();
            txtCommissionContractNo2.Text = dt.Rows[0]["CommissionContractNo2"].ToString().Trim();
            txtCommissionExchCurr.Text = dt.Rows[0]["CommissionExchCurr"].ToString().Trim();
            txtCommissionExchRate.Text = dt.Rows[0]["CommissionExchRate"].ToString().Trim();
            txtCommissionIntExchRate.Text = dt.Rows[0]["CommissionIntExchRate"].ToString().Trim();
            txtCRGLCode.Text = dt.Rows[0]["CRGLCode"].ToString().Trim();
            txtCRCustAbbr.Text = dt.Rows[0]["CRCustAbbr"].ToString().Trim();
            txtCRCustAcNo1.Text = dt.Rows[0]["CRCustAcNo1"].ToString().Trim();
            txtCRCustAcNo2.Text = dt.Rows[0]["CRCustAcNo2"].ToString().Trim();
            txtCRCurr.Text = dt.Rows[0]["CRCurr"].ToString().Trim();
            txtCRAmount.Text = dt.Rows[0]["CRAmount"].ToString().Trim();
            txtCRIntCurr.Text = dt.Rows[0]["CRIntCurr"].ToString().Trim();
            txtCRIntAmount.Text = dt.Rows[0]["CRIntAmount"].ToString().Trim();
            txtCRIntPayer.Text = dt.Rows[0]["CRIntPayer"].ToString().Trim();
            txtCRPaymentCommCurr.Text = dt.Rows[0]["CRPaymentCommCurr"].ToString().Trim();
            txtCRPaymentCommAmount.Text = dt.Rows[0]["CRPaymentCommAmount"].ToString().Trim();
            txtCRPaymentCommPayer.Text = dt.Rows[0]["CRPaymentCommPayer"].ToString().Trim();
            txtCRHandlingCommCurr.Text = dt.Rows[0]["CRHandlingCommCurr"].ToString().Trim();
            txtCRHandlingCommAmount.Text = dt.Rows[0]["CRHandlingCommAmount"].ToString().Trim();
            txtCRHandlingCommPayer.Text = dt.Rows[0]["CRHandlingCommPayer"].ToString().Trim();
            txtCRPostageCurr.Text = dt.Rows[0]["CRPostageCurr"].ToString().Trim();
            txtCRPostageAmount.Text = dt.Rows[0]["CRPostageAmount"].ToString().Trim();
            txtCRPostagePayer.Text = dt.Rows[0]["CRPostagePayer"].ToString().Trim();
            txtCRCurr1.Text = dt.Rows[0]["CRCurr1"].ToString().Trim();
            txtCRAmount1.Text = dt.Rows[0]["CRAmount1"].ToString().Trim();
            txtCRPayer1.Text = dt.Rows[0]["CRPayer1"].ToString().Trim();
            txtDRCurr.Text = dt.Rows[0]["DRCurr"].ToString().Trim();
            txtDRAmount.Text = dt.Rows[0]["DRAmount"].ToString().Trim();
            txtDRGLCode1.Text = dt.Rows[0]["DRGLCode1"].ToString().Trim();
            txtDRCustAbbr1.Text = dt.Rows[0]["DRCustAbbr1"].ToString().Trim();
            txtDRCustAcNo11.Text = dt.Rows[0]["DRCustAcNo11"].ToString().Trim();
            txtDRCustAcNo12.Text = dt.Rows[0]["DRCustAcNo12"].ToString().Trim();
            txtDRCurr1.Text = dt.Rows[0]["DRCurr1"].ToString().Trim();
            txtDRAmount1.Text = dt.Rows[0]["DRAmount1"].ToString().Trim();
            txtDRGLCode2.Text = dt.Rows[0]["DRGLCode2"].ToString().Trim();
            txtDRCustAbbr2.Text = dt.Rows[0]["DRCustAbbr2"].ToString().Trim();
            txtDRCustAcNo21.Text = dt.Rows[0]["DRCustAcNo21"].ToString().Trim();
            txtDRCustAcNo22.Text = dt.Rows[0]["DRCustAcNo22"].ToString().Trim();
            txtDRCurr2.Text = dt.Rows[0]["DRCurr2"].ToString().Trim();
            txtDRAmount2.Text = dt.Rows[0]["DRAmount2"].ToString().Trim();
            txtDRGLCode3.Text = dt.Rows[0]["DRGLCode3"].ToString().Trim();
            txtDRCustAbbr3.Text = dt.Rows[0]["DRCustAbbr3"].ToString().Trim();
            txtDRCustAcNo31.Text = dt.Rows[0]["DRCustAcNo31"].ToString().Trim();
            txtDRCustAcNo32.Text = dt.Rows[0]["DRCustAcNo32"].ToString().Trim();
            txtDRCurr3.Text = dt.Rows[0]["DRCurr3"].ToString().Trim();
            txtDRAmount3.Text = dt.Rows[0]["DRAmount3"].ToString().Trim();
            txtDRGLCode4.Text = dt.Rows[0]["DRGLCode4"].ToString().Trim();
            txtDRCustAbbr4.Text = dt.Rows[0]["DRCustAbbr4"].ToString().Trim();
            txtDRCustAcNo41.Text = dt.Rows[0]["DRCustAcNo41"].ToString().Trim();
            txtDRCustAcNo42.Text = dt.Rows[0]["DRCustAcNo42"].ToString().Trim();
            txtDRCurr4.Text = dt.Rows[0]["DRCurr4"].ToString().Trim();
            txtDRAmount4.Text = dt.Rows[0]["DRAmount4"].ToString().Trim();
            txtDRGLCode5.Text = dt.Rows[0]["DRGLCode5"].ToString().Trim();
            txtDRCustAbbr5.Text = dt.Rows[0]["DRCustAbbr5"].ToString().Trim();
            txtDRCustAcNo51.Text = dt.Rows[0]["DRCustAcNo51"].ToString().Trim();
            txtDRCustAcNo52.Text = dt.Rows[0]["DRCustAcNo52"].ToString().Trim();
            txtDRCurr5.Text = dt.Rows[0]["DRCurr5"].ToString().Trim();
            txtDRAmount5.Text = dt.Rows[0]["DRAmount5"].ToString().Trim();
            txtPayingBankID.Text = dt.Rows[0]["PayingBank"].ToString().Trim();
            fillPayingBankDescription();
            #endregion
            //--------------------------Anand 04-01-2024 audit trail  -------------------------------
            //------------------------------------Document Tab------------------------
            hdnCustACNO.Value = dt.Rows[0]["CustAcNo"].ToString().Trim();
            hdnOverseasParty.Value = dt.Rows[0]["Overseas_Party_Code"].ToString().Trim();
            hdnConsigneeParty.Value = dt.Rows[0]["consigneePartyID"].ToString().Trim();
            hdnOverseasBankID.Value = dt.Rows[0]["Overseas_Bank_Code"].ToString().Trim();
            hdnDateReceived.Value = dt.Rows[0]["Date_Received"].ToString().Trim();
            hdnDRAFTBENo.Value = dt.Rows[0]["DD_NO"].ToString().Trim();
            hdnAWBBLNoLR.Value = dt.Rows[0]["AWB_No"].ToString().Trim();
            hdnAWBBLNoLRDate.Value = dt.Rows[0]["AWB_Date"].ToString().Trim();
            hdnCheckarStatus.Value = dt.Rows[0]["Checker_Status"].ToString().Trim();

            //--------------------------------------Transaction Tab---------------------------
            hdnCurr.Value = dt.Rows[0]["Currency"].ToString().Trim();
            hdnNoofDays.Value = dt.Rows[0]["Days"].ToString().Trim();
            hdnTenorRemarks.Value = dt.Rows[0]["Other_Tenor_Remarks"].ToString().Trim();
            hdnDueDate.Value = dt.Rows[0]["Due_Date"].ToString().Trim();
            hdnBillAmount.Value = dt.Rows[0]["ActBillAmt"].ToString().Trim();
            hdnIRMReferenceNo1.Value = dt.Rows[0]["TTREFNO1"].ToString().Trim();
            hdnIRMamountUtilized1.Value = dt.Rows[0]["TTAmt1"].ToString().Trim();
            hdnIRMReferenceNo2.Value = dt.Rows[0]["TTREFNO2"].ToString().Trim();
            hdnIRMamountUtilized2.Value = dt.Rows[0]["TTAmt2"].ToString().Trim();
            hdnEFIRCNo1.Value = dt.Rows[0]["FIRCNo_OtrBank1"].ToString().Trim();
            hdnEFIRCDate1.Value = dt.Rows[0]["FIRCDate_OtrBank1"].ToString().Trim();
            hdnEFIRCAmount1.Value = dt.Rows[0]["FIRCAmt_OtrBank1"].ToString().Trim();
            hdnEFIRCNo2.Value = dt.Rows[0]["FIRCNo_OtrBank2"].ToString().Trim();
            hdnEFIRCDate2.Value = dt.Rows[0]["FIRCDate_OtrBank2"].ToString().Trim();
            hdnEFIRCAmount2.Value = dt.Rows[0]["FIRCAmt_OtrBank2"].ToString().Trim();
            //---------------------------------GBase Tab----------------------
            hdnCountryRisk.Value = dt.Rows[0]["Risk_Country"].ToString().Trim();
            hdnReimbursingBank.Value = dt.Rows[0]["Reimbursing_Bank"].ToString().Trim();
        }
        else
        {
            if (txtCopyFromDocNo.Text.Trim() != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid Document No.');", true);
                txtCopyFromDocNo.Focus();
            }
        }
    }

    private void SetInitialRow()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter pc1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pc1.Value = txtDocumentNo.Text.Trim();
        string _query1 = "TF_EXP_GetGRPPCustomsList";

        DataTable dtPC = objData.getData(_query1, pc1);

        if (dtPC.Rows.Count > 0)
        {
            ViewState["CurrentTable"] = dtPC;
            GridViewGRPPCustomsDetails.DataSource = dtPC;
            GridViewGRPPCustomsDetails.DataBind();

            ddlDispachInd.ClearSelection();
            ddlDispachInd.SelectedValue = dtPC.Rows[0]["DispInd"].ToString();
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("SrNo", typeof(string)));
            dt.Columns.Add(new DataColumn("FormType", typeof(string)));
            ////dt.Columns.Add(new DataColumn("ExportAgency", typeof(string)));
            ////dt.Columns.Add(new DataColumn("DispInd", typeof(string)));
            dt.Columns.Add(new DataColumn("GR", typeof(string)));
            dt.Columns.Add(new DataColumn("GRCurrency", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            ////dt.Columns.Add(new DataColumn("ExchRate", typeof(string)));
            ////dt.Columns.Add(new DataColumn("AmtinINR", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipping_Bill_No", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipping_Bill_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceDate", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("FreightAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("InsuranceAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("DiscountAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("Commission", typeof(string)));
            dt.Columns.Add(new DataColumn("OtherDeductionAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("PackingCharges", typeof(string)));
            dt.Columns.Add(new DataColumn("PortCode", typeof(string)));
            dt.Columns.Add(new DataColumn("FreightCurr", typeof(string)));
            dt.Columns.Add(new DataColumn("InsuranceCurr", typeof(string)));
            dt.Columns.Add(new DataColumn("DiscountCurr", typeof(string)));
            dt.Columns.Add(new DataColumn("CommCurr", typeof(string)));
            dt.Columns.Add(new DataColumn("OthDedCurr", typeof(string)));
            dt.Columns.Add(new DataColumn("PackingChrgCurr", typeof(string)));
            dt.Columns.Add(new DataColumn("ExchRate", typeof(string)));
            dt.Columns.Add(new DataColumn("Invoicesrno", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            ////dt.Columns.Add(new DataColumn("CommodityID", typeof(string)));

            dr = dt.NewRow();
            dr["SrNo"] = 0;
            dr["FormType"] = string.Empty;
            //dr["ExportAgency"] = string.Empty;
            //dr["DispInd"] = string.Empty;
            dr["GR"] = string.Empty;
            dr["GRCurrency"] = string.Empty;
            dr["Amount"] = string.Empty;
            //dr["ExchRate"] = string.Empty;
            //dr["AmtinINR"] = string.Empty;
            dr["Shipping_Bill_No"] = string.Empty;
            dr["Shipping_Bill_Date"] = string.Empty;
            dr["InvoiceNo"] = string.Empty;
            dr["InvoiceDate"] = string.Empty;
            dr["InvoiceAmt"] = string.Empty;
            dr["FreightAmount"] = string.Empty;
            dr["InsuranceAmount"] = string.Empty;
            dr["DiscountAmt"] = string.Empty;
            dr["Commission"] = string.Empty;
            dr["OtherDeductionAmt"] = string.Empty;
            dr["PackingCharges"] = string.Empty;
            dr["PortCode"] = string.Empty;
            dr["FreightCurr"] = string.Empty;
            dr["InsuranceCurr"] = string.Empty;
            dr["DiscountCurr"] = string.Empty;
            dr["CommCurr"] = string.Empty;
            dr["OthDedCurr"] = string.Empty;
            dr["PackingChrgCurr"] = string.Empty;
            dr["ExchRate"] = string.Empty;
            dr["Invoicesrno"] = string.Empty;
            dr["Status"] = string.Empty;

            ///dr["CommodityID"] = string.Empty;

            dt.Rows.Add(dr);
            dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
            GridViewGRPPCustomsDetails.Visible = false;
        }
        CalculateGR_Total();
    }
    protected void GridViewGRPPCustomsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerialNumber = new Label();
            Button btnDelete = new Button();
            lblSerialNumber = (Label)e.Row.FindControl("lblSrNo");

            //btnDelete = (Button)e.Row.FindControl("btnDelete");

            //btnDelete.Enabled = true;
            //btnDelete.CssClass = "deleteButton";
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");


            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {

                //string pageurl = "window.location='EBRC_AddEditERSData.aspx?mode=edit&srno=" + lblSerialNumber.Text.Trim() + "'";
                if (i != 17)
                    cell.Attributes.Add("onclick", "return gridClicked(" + lblSerialNumber.Text + ");");
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void btnAddinGrid_Click(object sender, EventArgs e)
    {
        if (ddlCurrency.Text.Trim() != ddlCurrencyGRPP.Text.Trim())
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction currency different from shipping bill currency. Kindly check');", true);
        }
        else
        {
            if (txtGRExchRate.Text.Trim() == "")
            {

                if (ddlFreightCurr.SelectedValue == "0" || ddlFreightCurr.SelectedItem.Text == "---Select---")
                {

                }
                else if (ddlCurrencyGRPP.SelectedItem.Text.Trim() != ddlFreightCurr.SelectedItem.Text.Trim())
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Shipping Bill Currency Not Matching with Freight Currency! Please Enter Exch Rate');", true);
                    txtGRExchRate.Focus();
                    return;
                }

                if (ddlInsCurr.SelectedValue == "0" || ddlInsCurr.SelectedItem.Text == "---Select---")
                {

                }
                else if (ddlCurrencyGRPP.SelectedItem.Text.Trim() != ddlInsCurr.SelectedItem.Text.Trim())
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Shipping Bill Currency Not Matching with Insurence Currency! Please Enter Exch Rate');", true);
                    txtGRExchRate.Focus();
                    return;
                }
            }
            //--------------Added by Anand 28-02-2024-----------------------------------------
            else if (ddlCurrencyGRPP.SelectedValue == ddlFreightCurr.SelectedValue && ddlFreightCurr.SelectedValue == ddlInsCurr.SelectedValue)
            {
                float calcuate = float.Parse(txtInvoiceAmt.Text) + float.Parse(txtFreight.Text) + float.Parse(txtInsurance.Text);
                float roundResultAmt = (float)Math.Round(calcuate, 2);
                float txtAmountGRPPAmt = float.Parse(txtAmountGRPP.Text);
                float roundResultGRPP = (float)Math.Round(txtAmountGRPPAmt, 2);
                if (roundResultAmt != roundResultGRPP)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Shipping Bill Amount MisMatch With Invoice + Freight +Insurance Amount.Please correct the values.');", true);
                    return;
                }
            }
            else if (ddlCurrencyGRPP.SelectedValue != ddlFreightCurr.SelectedValue && ddlCurrencyGRPP.SelectedValue != ddlInsCurr.SelectedValue)
            {
                float FreightAmount = float.Parse(txtFreight.Text) / float.Parse(txtGRExchRate.Text);
                float InsuranceAmount = float.Parse(txtInsurance.Text) / float.Parse(txtGRExchRate.Text);
                float calcuate1 = float.Parse(txtInvoiceAmt.Text) + FreightAmount + InsuranceAmount;
                float roundResultAmt1 = (float)Math.Round(calcuate1, 2);
                float txtAmountGRPP1 = float.Parse(txtAmountGRPP.Text);
                float roundResultGRPP1 = (float)Math.Round(txtAmountGRPP1, 2);
                if (roundResultAmt1 != roundResultGRPP1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Shipping Bill Amount MisMatch With Invoice + Freight +Insurance Amount.Please correct the values.');", true);
                    return;
                }
            }
        

            AddNewRowToGrid();
            txtGRPPCustomsNo.Text = "";
            //txtCurrencyGRPP.Text = "";
            ddlCurrencyGRPP.SelectedIndex = 0;
            txtAmountGRPP.Text = "";
            txtShippingBillDate.Text = "";
            txtShippingBillNo.Text = "";
            txtCommissionGRPP.Text = "";
            ddlPortCode.SelectedIndex = 0;
            ddlFormType.SelectedIndex = 0;
            ////txtExchRateGR.Text = "";
            ////txtAmountinINRGR.Text = "";
            txtInvoiceAmt.Text = "";
            txtInvoiceDt.Text = "";
            txtInvoiceNum.Text = "";
            txtFreight.Text = "";
            txtInsurance.Text = "";
            txtOthDeduction.Text = "";
            txtDiscount.Text = "";
            txtPacking.Text = "";

            ddlFreightCurr.SelectedIndex = 0;
            ddlInsCurr.SelectedIndex = 0;
            ddlDiscCurr.SelectedIndex = 0;
            ddlCommCurr.SelectedIndex = 0;
            ddlOthDedCurr.SelectedIndex = 0;
            ddlPackChgCurr.SelectedIndex = 0;
            txtGRExchRate.Text = "";
            txt_invsrno.Text = "1";
            txt_status.Text = "";
            txtGRExchRate.Text = "";
            ////txtCommodityID.Text = "";


            //txtOperationType.Text = optypeld;
            //txtSettlementOption.Text = settlementoptionld;
            //txtRiskCountry.Text = countryriskld;
            //txtFundType.Text = fundtypeld;
            //txtBaseRate.Text = baserateld;
            //txtGradeCode.Text = graderateld;
            //txtDirection.Text = directionld;
            //txtCovrInstr.Text = covinstructionld;
            //txtInternalRate.Text = internalrateld;
            //txtSpread.Text = spreadld;
            //txtApplNo.Text = applnold;
            //txtDraftNo.Text = draftnold;

            //txtRiskCustomer.Text = riskcustld;
            //txtVesselName.Text = vesselnameld;
            //txtReimbBank.Text = reimbbankld;
            //txtGBaseRemarks.Text = remarksld;
            //txtMerchandise.Text = merchandiseld;
            //txtPayingBankID.Text = payingbankld;
            //txtPrincipalContractNo1.Text = PrincipalContractNo1ld;
            //txtPrincipalContractNo2.Text = PrincipalContractNo2ld;
            //txtPrincipalExchCurr.Text = PrincipalExchCurrld;
            //txtPrincipalExchRate.Text = PrincipalExchRateld;
            //txtPrincipalIntExchRate.Text = PrincipalIntExchRateld;
            //txtInterestLump.Text = InterestLumpld;
            //txtInterestContractNo1.Text = InterestContractNo1ld;
            //txtInterestContractNo2.Text = InterestContractNo2ld;
            //txtInterestExchCurr.Text = InterestExchCurrld;
            //txtInterestExchRate.Text = InterestExchRateld;
            //txtInterestIntExchRate.Text = InterestIntExchRate;
            //txtCommissionMatu.Text = CommissionMatuld;
            //txtCommissionContractNo1.Text = CommissionContractNo1ld;
            //txtCommissionContractNo2.Text = CommissionContractNo2ld;
            //txtCommissionExchCurr.Text = CommissionExchCurrld;
            //txtCommissionExchRate.Text = CommissionExchRateld;
            //txtCommissionIntExchRate.Text = CommissionIntExchRateld;
            //txtCRGLCode.Text = CRGLCodeld;
            //txtCRCustAbbr.Text = CRCustAbbrld;
            //txtCRCustAcNo1.Text = CRCustAcNo1ld;
            //txtCRCustAcNo2.Text = CRCustAcNo2ld;
            //txtCRCurr.Text = CRCurrld;
            //txtCRAmount.Text = CRAmountld;
            //txtCRIntCurr.Text = CRIntCurrld;
            //txtCRIntAmount.Text = CRIntAmountld;
            //txtCRIntPayer.Text = CRIntPayerld;
            //txtCRPaymentCommCurr.Text = CRPaymentCommCurrld;
            //txtCRPaymentCommAmount.Text = CRPaymentCommAmountld;
            //txtCRPaymentCommPayer.Text = CRPaymentCommPayerld;
            //txtCRHandlingCommCurr.Text = CRHandlingCommCurrld;
            //txtCRHandlingCommAmount.Text = CRHandlingCommAmountld;
            //txtCRHandlingCommPayer.Text = CRHandlingCommPayerld;
            //txtCRPostageCurr.Text = CRPostageCurrld;
            //txtCRPostageAmount.Text = CRPostageAmountld;
            //txtCRPostagePayer.Text = CRPostagePayerld;
            //txtCRCurr1.Text = CRCurr1ld;
            //txtCRAmount1.Text = CRAmount1ld;
            //txtCRPayer1.Text = CRPayer1ld;
            //txtDRCurr.Text = DRCurrld;
            //txtDRAmount.Text = DRAmountld;
            //txtDRGLCode1.Text = DRGLCode1ld;
            //txtDRCustAbbr1.Text = DRCustAbbr1ld;
            //txtDRCustAcNo11.Text = DRCustAcNo11ld;
            //txtDRCustAcNo12.Text = DRCustAcNo12ld;
            //txtDRCurr1.Text = DRCurr1ld;
            //txtDRAmount1.Text = DRAmount1ld;
            //txtDRGLCode2.Text = DRGLCode2ld;
            //txtDRCustAbbr2.Text = DRCustAbbr2ld;
            //txtDRCustAcNo21.Text = DRCustAcNo21ld;
            //txtDRCustAcNo22.Text = DRCustAcNo22ld;
            //txtDRCurr2.Text = DRCurr2ld;
            //txtDRAmount2.Text = DRAmount2ld;
            //txtDRGLCode3.Text = DRGLCode3ld;
            //txtDRCustAbbr3.Text = DRCustAbbr3ld;
            //txtDRCustAcNo31.Text = DRCustAcNo31ld;
            //txtDRCustAcNo32.Text = DRCustAcNo32ld;
            //txtDRCurr3.Text = DRCurr3ld;
            //txtDRAmount3.Text = DRAmount3ld;
            //txtDRGLCode4.Text = DRGLCode4ld;
            //txtDRCustAbbr4.Text = DRCustAbbr4ld;
            //txtDRCustAcNo41.Text = DRCustAcNo41ld;
            //txtDRCustAcNo42.Text = DRCustAcNo42ld;
            //txtDRCurr4.Text = DRCurr4ld;
            //txtDRAmount4.Text = DRAmount4ld;
            //txtDRGLCode5.Text = DRGLCode5ld;
            //txtDRCustAbbr5.Text = DRCustAbbr5ld;
            //txtDRCustAcNo51.Text = DRCustAcNo51ld;
            //txtDRCustAcNo52.Text = DRCustAcNo52ld;
            //txtDRCurr5.Text = DRCurr5ld;
            //txtDRAmount5.Text = DRAmount5ld;

        }
    }

    private void AddNewRowToGrid()
    {
        GridViewGRPPCustomsDetails.Visible = true;
        int rowIndex = 0;
        string _GRcurr = "", _GRportID = "", _frieghtCurr = "", _insurCurr = "", _disCurr = "", _CommCurr = "", _OthDed = "", _PackinChrg = "", _GRExchRate = "";

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count >= 0)
            {

                for (int a = 0; a < dtCurrentTable.Rows.Count; a++)
                {
                    if (txtShippingBillNo.Text == dtCurrentTable.Rows[a]["Shipping_Bill_No"].ToString() && txtShippingBillDate.Text == dtCurrentTable.Rows[a]["Shipping_Bill_Date"].ToString() && ddlPortCode.SelectedValue.Trim() == dtCurrentTable.Rows[a]["PortCode"].ToString() && txtInvoiceNum.Text == dtCurrentTable.Rows[a]["InvoiceNo"].ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('BOE No. already Added Please Select Other BOE No.');", true);
                        return;
                    }
                }


                for (int i = 0; i <= dtCurrentTable.Rows.Count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    Label lastSr = new Label();
                    if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                    {
                        lastSr = (Label)GridViewGRPPCustomsDetails.Rows[dtCurrentTable.Rows.Count - 1].Cells[1].FindControl("lblSrNo");
                        if (lastSr.Text == "0")
                            dtCurrentTable.Rows.RemoveAt(0);
                    }
                    else
                        lastSr.Text = 0.ToString();

                    drCurrentRow["SrNo"] = Convert.ToInt32(lastSr.Text) + 1;
                    drCurrentRow["GR"] = txtGRPPCustomsNo.Text;
                    drCurrentRow["FormType"] = ddlFormType.SelectedValue;
                    ////drCurrentRow["ExportAgency"] = ddlExportAgency.SelectedValue;
                    ////drCurrentRow["DispInd"] = ddlDispachInd.SelectedValue;

                    if (ddlCurrencyGRPP.SelectedIndex == 0)
                        _GRcurr = "";
                    else
                        _GRcurr = ddlCurrencyGRPP.SelectedValue;

                    drCurrentRow["GRCurrency"] = _GRcurr;

                    if (txtAmountGRPP.Text == "")
                        txtAmountGRPP.Text = "0";
                    drCurrentRow["Amount"] = txtAmountGRPP.Text;

                    ////if (txtExchRateGR.Text == "")
                    ////    txtExchRateGR.Text = "0";
                    ////drCurrentRow["ExchRate"] = txtExchRateGR.Text;

                    ////if (txtAmountinINRGR.Text == "")
                    ////    txtAmountinINRGR.Text = "0";
                    ////drCurrentRow["AmtinINR"] = txtAmountinINRGR.Text;

                    //hdnGRtotalAmount.Value = (Convert.ToDouble(hdnGRtotalAmount.Value) + Convert.ToDouble(txtAmountGRPP.Text)).ToString();
                    drCurrentRow["Shipping_Bill_No"] = txtShippingBillNo.Text;
                    drCurrentRow["Shipping_Bill_Date"] = txtShippingBillDate.Text;
                    drCurrentRow["InvoiceNo"] = txtInvoiceNum.Text;
                    drCurrentRow["InvoiceDate"] = txtInvoiceDt.Text;
                    drCurrentRow["Invoicesrno"] = txt_invsrno.Text;
                    drCurrentRow["Status"] = txt_status.Text;

                    if (txtInvoiceAmt.Text == "")
                        txtInvoiceAmt.Text = "0";
                    drCurrentRow["InvoiceAmt"] = txtInvoiceAmt.Text;

                    if (txtFreight.Text == "")
                        txtFreight.Text = "0";
                    drCurrentRow["FreightAmount"] = txtFreight.Text;

                    if (txtInsurance.Text == "")
                        txtInsurance.Text = "0";
                    drCurrentRow["InsuranceAmount"] = txtInsurance.Text;

                    if (txtDiscount.Text == "")
                        txtDiscount.Text = "0";
                    drCurrentRow["DiscountAmt"] = txtDiscount.Text;

                    if (txtCommissionGRPP.Text == "")
                        txtCommissionGRPP.Text = "0";
                    drCurrentRow["Commission"] = txtCommissionGRPP.Text;

                    if (txtOthDeduction.Text == "")
                        txtOthDeduction.Text = "0";
                    drCurrentRow["OtherDeductionAmt"] = txtOthDeduction.Text;

                    if (txtPacking.Text == "")
                        txtPacking.Text = "0";
                    drCurrentRow["PackingCharges"] = txtPacking.Text;

                    if (ddlFreightCurr.SelectedIndex == 0)
                        _frieghtCurr = "";
                    else
                        _frieghtCurr = ddlFreightCurr.SelectedValue;
                    drCurrentRow["FreightCurr"] = _frieghtCurr;

                    if (ddlInsCurr.SelectedIndex == 0)
                        _insurCurr = "";
                    else
                        _insurCurr = ddlInsCurr.SelectedValue;
                    drCurrentRow["InsuranceCurr"] = _insurCurr;

                    if (ddlDiscCurr.SelectedIndex == 0)
                        _disCurr = "";
                    else
                        _disCurr = ddlDiscCurr.SelectedValue;
                    drCurrentRow["DiscountCurr"] = _disCurr;

                    if (ddlCommCurr.SelectedIndex == 0)
                        _CommCurr = "";
                    else
                        _CommCurr = ddlCommCurr.SelectedValue;
                    drCurrentRow["CommCurr"] = _CommCurr;

                    if (ddlOthDedCurr.SelectedIndex == 0)
                        _OthDed = "";
                    else
                        _OthDed = ddlOthDedCurr.SelectedValue;
                    drCurrentRow["OthDedCurr"] = _OthDed;

                    if (ddlPackChgCurr.SelectedIndex == 0)
                        _PackinChrg = "";
                    else
                        _PackinChrg = ddlPackChgCurr.SelectedValue;
                    drCurrentRow["PackingChrgCurr"] = _PackinChrg;

                    if (ddlPortCode.SelectedIndex == 0)
                        _GRportID = "";
                    else
                        _GRportID = ddlPortCode.SelectedValue;
                    drCurrentRow["PortCode"] = _GRportID;

                    if (txtGRExchRate.Text.Trim() == "")
                        _GRExchRate = "0";
                    else
                        _GRExchRate = txtGRExchRate.Text;
                    drCurrentRow["ExchRate"] = _GRExchRate;

                    ////drCurrentRow["CommodityID"] = txtCommodityID.Text;
                    rowIndex++;
                }

                dtCurrentTable.Rows.Add(drCurrentRow);
                DataView dv = dtCurrentTable.DefaultView;
                dv.Sort = "SrNo ASC";
                dtCurrentTable = dv.ToTable();
                //txtSrNo.Text = (Convert.ToInt32(dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["SrNo"].ToString()) + 1).ToString();
                ViewState["CurrentTable"] = dtCurrentTable;

                GridViewGRPPCustomsDetails.DataSource = dtCurrentTable;
                GridViewGRPPCustomsDetails.DataBind();
                //--------------------------Anand 09-01-2023-------------------------------
                if (hdShippingBillNo.Value == txtShippingBillNo.Text.Trim())
                {
                    string _query = "TF_Data_AuditTrailGRPP";
                    string _script = "";
                    string _userName = Session["userName"].ToString().Trim();
                    string _OldValues = "";
                    string _NewValues = "";
                    string _result = "updated";
                    string _Status = "";

                    AuditGRPP(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);
                }
                //---------------------------End-----------------------------------------------------
            }
            CalculateGR_Total();
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void CalculateGR_Total()
    {
        hdnGRtotalAmount.Value = "0";
        if (GridViewGRPPCustomsDetails.Rows.Count > 0)
        {
            Label lblSrNo = (Label)GridViewGRPPCustomsDetails.Rows[0].Cells[0].FindControl("lblSrNo");
            if (lblSrNo.Text != "0")
            {
                for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
                {

                    Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[6].FindControl("lblAmount");
                    hdnGRtotalAmount.Value = (Convert.ToDouble(hdnGRtotalAmount.Value) + Convert.ToDouble(lbl6.Text)).ToString();
                }
            }
        }
    }

    protected void LinkButtonClick(object sender, System.EventArgs e)
    {

        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        Label lblShippingBillNo = (Label)gvRow.FindControl("lblShippingBillNo");
        Label lblInvoiceNo = (Label)gvRow.FindControl("lblInvoiceNo");

        // Access the text of the Label
        string shippingBillNo = lblShippingBillNo.Text;
        string InvoiceNo = lblInvoiceNo.Text;

        int rowID = gvRow.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                if (gvRow.RowIndex <= dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                    //-------------Anand 02-04-2024---------------------
                    SqlParameter p1 = new SqlParameter("@documentNo", SqlDbType.VarChar);
                    p1.Value = txtDocumentNo.Text;
                    SqlParameter p2 = new SqlParameter("@ShippingBill", SqlDbType.VarChar);
                    p2.Value = shippingBillNo;
                    SqlParameter p3 = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                    p3.Value = InvoiceNo;
                    string _query1 = "TF_EXP_DeleteGRPPCustoms";
                    TF_DATA objData = new TF_DATA();
                    string result = objData.SaveDeleteData(_query1, p1, p2, p3);

                    //-------------End---------------------
                    string _query = "";
                    string _NewValues = shippingBillNo;
                    string _OldValues = "";
                    string _userName = Session["userName"].ToString().Trim();
                    string _script = "";
                    string _result = "Delete";
                    string _Status = "";
                    AuditGRPPDelete(_query, _NewValues, _OldValues, _userName, _script, _result, _Status);

                }
            }

            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
        }
        CalculateGR_Total();
    }

    protected void btnGridValues_Click(object sender, EventArgs e)
    {
        //Button lb = (Button)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowIndex = 0;
        string srNo = hdnGridValues.Value;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    Label lblSrNo = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblSrNo");
                    int lcolumncount;
                    lcolumncount = GridViewGRPPCustomsDetails.Columns.Count;

                    if (srNo == lblSrNo.Text)
                    {
                        //Remove the Selected Row data
                        //Label lblFormType = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[0].FindControl("lblFormType");
                        //Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblSrNo");
                        //Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[2].FindControl("lblGR");
                        //Label lbl3 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblCurrency");
                        //Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[4].FindControl("lblAmount");
                        //Label lblExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[5].FindControl("lblExchRate");
                        //Label lblAmountinINR = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[6].FindControl("lblAmountinINR");
                        //Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[7].FindControl("lblShippingBillNo");
                        //Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[8].FindControl("lblShippingBillDate");
                        //Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[9].FindControl("lblCommission");
                        //Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[10].FindControl("lblPortCode");
                        //Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[11].FindControl("lblExpAgency");
                        //Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[12].FindControl("lblDispInd");
                        //Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[13].FindControl("lblInvoiceNo");
                        //Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[14].FindControl("lblInvoiceDate");
                        //Label lbl13 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[15].FindControl("lblInvoiceAmt");

                        Label lblFormType = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFormType");
                        Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGR");
                        Label lbl3 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCurrency");
                        Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblAmount");
                        //Label lblExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblExchRate");
                        //Label lblAmountinINR = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblAmountinINR");
                        Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillNo");
                        Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillDate");
                        Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommission");
                        Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPortCode");
                        //Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblExpAgency");
                        ////Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDispInd");
                        Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceNo");
                        Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceDate");
                        Label lbl13 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceAmt");
                        Label lblFreightAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFreightAmt");
                        Label lblInsuranceAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsuranceAmt");
                        Label lblDiscountAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDiscountAmt");
                        Label lblOthDeduction = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOthDeduction");
                        Label lblPacking = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPacking");

                        Label lblFrieghtCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFrieghtCurr");
                        Label lblInsCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsCurr");
                        Label lblDisCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDisCurr");
                        Label lblCommCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommCurr");
                        Label lblOtherDedCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOtherDedCurr");
                        Label lblPackingChgCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPackingChgCurr");

                        Label lblGRExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGRExchRate");

                        Label lblinvsrno = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblinvsrno");
                        Label lblstatus = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblstatus");

                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        if (lblFormType.Text.Trim() != "")
                            ddlFormType.Text = lblFormType.Text;
                        ////if (lbl9.Text != "")
                        ////    ddlExportAgency.Text = lbl9.Text;
                        ////if (lbl10.Text != "")
                        ////    ddlDispachInd.Text = lbl10.Text;
                        txtGRPPCustomsNo.Text = lbl2.Text;
                        if (lbl3.Text != "")
                            ddlCurrencyGRPP.Text = lbl3.Text;
                        txtAmountGRPP.Text = lbl4.Text;
                        ////txtExchRateGR.Text = lblExchRate.Text;
                        ////txtAmountinINRGR.Text = lblAmountinINR.Text;
                        txtShippingBillNo.Text = lbl5.Text;
                        txtShippingBillDate.Text = lbl6.Text;
                        txtInvoiceNum.Text = lbl11.Text;
                        txtInvoiceDt.Text = lbl12.Text;
                        txtInvoiceAmt.Text = lbl13.Text;
                        txtCommissionGRPP.Text = lbl7.Text;
                        txtFreight.Text = lblFreightAmt.Text;
                        txtInsurance.Text = lblInsuranceAmt.Text;
                        txtDiscount.Text = lblDiscountAmt.Text;
                        txtOthDeduction.Text = lblOthDeduction.Text;
                        txtPacking.Text = lblPacking.Text;
                        txt_invsrno.Text = lblinvsrno.Text;
                        txt_status.Text = lblstatus.Text;

                        if (lblFrieghtCurr.Text != "")
                            ddlFreightCurr.Text = lblFrieghtCurr.Text;
                        if (lblInsCurr.Text != "")
                            ddlInsCurr.Text = lblInsCurr.Text;
                        if (lblDisCurr.Text != "")
                            ddlDiscCurr.Text = lblDisCurr.Text;
                        if (lblCommCurr.Text != "")
                            ddlCommCurr.Text = lblCommCurr.Text;
                        if (lblOtherDedCurr.Text != "")
                            ddlOthDedCurr.Text = lblOtherDedCurr.Text;
                        if (lblPackingChgCurr.Text != "")
                            ddlPackChgCurr.Text = lblPackingChgCurr.Text;
                        if (lblGRExchRate.Text != "")
                        {
                            txtGRExchRate.Text = lblGRExchRate.Text;
                        }

                        if (lbl8.Text.Trim() != "")
                        {
                            ddlPortCode.SelectedIndex = ddlPortCode.Items.IndexOf(ddlPortCode.Items.FindByText(lbl8.Text));
                            //ddlPortCode.Text = lbl8.Text;
                        }
                        else
                            ddlPortCode.SelectedIndex = 0;
                        //-----------------Convering GR/PP hidden filed---------------- 
                        hdShippingBillNo.Value = lbl5.Text;
                        hdnShippingBillDate.Value = lbl6.Text;
                        hdnPortcode1.Value = lbl8.Text;
                        hdnCurrency.Value = lbl3.Text;
                        hdnAmount.Value = lbl4.Text;
                        hdnType.Value = lblFormType.Text;
                        hdnGRPPCustNo.Value = lbl2.Text;
                        hdnInvoiceSrNo.Value = lblinvsrno.Text;
                        hdnInvoiceNo.Value = lbl11.Text;
                        hdnInvoiceDate.Value = lbl12.Text;
                        hdnInvoiceAmt.Value = lbl13.Text;
                        hdnFreightAmt.Value = lblFreightAmt.Text;
                        hdnInsuranceAmt.Value = lblInsuranceAmt.Text;
                        hdnDiscountAmt.Value = lblDiscountAmt.Text;
                        hdnCommAmt.Value = lbl7.Text;
                        hdnOthDedChrgs.Value = lblOthDeduction.Text;
                        hdnPackingChrgs.Value = lblPacking.Text;
                        hdnStatus.Value = lblstatus.Text;
                        hdnFreightCurr.Value = lblFrieghtCurr.Text;
                        hdnInsCurr.Value = lblInsCurr.Text;
                        hdnDisCurr.Value = lblDisCurr.Text;
                        hdnCommCurr.Value = lblCommCurr.Text;
                        hdnOthDedCurr.Value = lblOthDeduction.Text;
                        hdnPackChgsCurr.Value = lblPackingChgCurr.Text;
                        hdnExchRate.Value = lblGRExchRate.Text;

                    }
                    rowIndex++;
                }
            }

            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
        }
    }

    protected void txtCustAcNo_TextChanged(object sender, EventArgs e)
    {
        Check_LEI_ThresholdLimit();
        fillCustomerCodeDescription();
    }
    protected void txtOverseasPartyID_TextChanged(object sender, EventArgs e)
    {
        Check_LEI_ThresholdLimit();
        fillOverseasPartyDescription();
    }
    protected void txtOverseasBankID_TextChanged(object sender, EventArgs e)
    {
        fillOverseasBankDescription();
    }
    protected void txtCommodityID_TextChanged(object sender, EventArgs e)
    {
        fillCommodityDescription();
        txtCustomsDoc.Focus();
    }
    protected void chkApplicable_CheckedChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        if (chkApplicable.Checked == true)
        {
            string _AmtInINR = "";
            string _exchRate = "";
            //if ((txtDocType.Text == "E") || (txtDocType.Text == "C" && rbtnEBR.Checked == true))
            //    _exchRate = txtExchRtEBR.Text;
            //else
            _exchRate = txtExchRate.Text;

            //_AmtInINR = ((Convert.ToDecimal(txtNetAmt.Text) - Convert.ToDecimal(txtTotalPCLiquidated.Text)) * Convert.ToDecimal(_exchRate)).ToString();

            _AmtInINR = (Convert.ToDecimal(txtNetAmt.Text) * Convert.ToDecimal(_exchRate)).ToString();
            SqlParameter pdocDate = new SqlParameter("@DocDt", SqlDbType.VarChar);
            pdocDate.Value = txtDateRcvd.Text.Trim();

            SqlParameter pINRamt = new SqlParameter("@InrAmt", SqlDbType.VarChar);
            pINRamt.Value = _AmtInINR;

            string _query = "TF_FXSTAX";
            DataTable dt = objData.getData(_query, pINRamt, pdocDate);
            if (dt.Rows.Count > 0)
            {
                txtSTFXDLS.Text = dt.Rows[0]["CalFX"].ToString().Trim();
                txtsbfx.Text = dt.Rows[0]["SBcess"].ToString().Trim();
                txt_kkcessonfx.Text = dt.Rows[0]["KKcess"].ToString().Trim();
                txttotsbcess.Text = dt.Rows[0]["TotFXamt"].ToString().Trim();
            }
            else
            {
                txtSTFXDLS.Text = "";
                txtsbfx.Text = "";
                txt_kkcessonfx.Text = "";
                txttotsbcess.Text = "";
            }
        }
        else
        {
            txtSTFXDLS.Text = "";
            txtsbfx.Text = "";
            txt_kkcessonfx.Text = "";
            txttotsbcess.Text = "";
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "serviceTax", "Calculate();", true);
        chkApplicable.Focus();
    }

    //protected void btnCopy_Click(object sender, EventArgs e)
    //{
    //    fillDetails_CopyFrom(txtCopyFromDocNo.Text);
    //}

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        string _bCode = Request.QueryString["BranchCode"].Trim();
        string copytext = txtCopyFromDocNo.Text.Trim();
        string copy = copytext.Substring(4, 3);
        string copy1 = copytext.Substring(0, 2); // Anand28-07-2023
        string copy2 = copytext.Substring(3, 3); // Anand28-07-2023       
        if (_bCode == copy)
        {
            fillDetails_CopyFrom(txtCopyFromDocNo.Text);
        }
        //-------------------------------------Anand28-07-2023--------------------------------
        else if (copy1 == "EB")
        {
            if (_bCode == copy2)
            {
                fillDetails_CopyFrom(txtCopyFromDocNo.Text);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid Document No.');", true);
                txtCopyFromDocNo.Focus();
            }
        }
        //-------------End--------------------------------------------------
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid Document No.');", true);
            txtCopyFromDocNo.Focus();
        }
    }

    protected void fillDetails_CopyFrom(string _DocNo)
    {
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = _DocNo;
        string _query = "TF_EXP_GetExportBillEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //------------------Document Details---------------------//

            // txtDocDate.Text = dt.Rows[0]["Remitting_Bank_Ref_No"].ToString().Trim();
            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            fillCustomerCodeDescription();
            txtOverseasPartyID.Text = dt.Rows[0]["Overseas_Party_Code"].ToString().Trim();
            fillOverseasPartyDescription();
            txtOverseasBankID.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString().Trim();
            fillOverseasBankDescription();

            //string _chkCustomer = dt.Rows[0]["CUSTCHECK"].ToString().Trim();
            //if (_chkCustomer == "C")
            //    chkCustCheck.Checked = true;
            //else
            //    chkCustCheck.Checked = false;

            //string _chkCS = dt.Rows[0]["CS"].ToString().Trim();
            //if (_chkCS == "Y")
            //    chkCS.Checked = true;
            //else
            //    chkCS.Checked = false;

            txtLCNo.Text = dt.Rows[0]["LC_NO"].ToString().Trim();
            txtLCNoDate.Text = dt.Rows[0]["LC_Date"].ToString().Trim();
            txtLCNoIssuedBy.Text = dt.Rows[0]["LC_Issued_By"].ToString().Trim();

            txtBENo.Text = dt.Rows[0]["DD_NO"].ToString().Trim();
            txtBEDate.Text = dt.Rows[0]["DD_Date"].ToString().Trim();

            string _bankLine = dt.Rows[0]["BankLine"].ToString().Trim();
            if (_bankLine == "Y")
                chkBankLineTransfer.Checked = true;
            txtAccpDate.Text = dt.Rows[0]["AccpDate"].ToString().Trim();
            txtBENoDoc.Text = dt.Rows[0]["DD_Doc"].ToString().Trim();

            txtInvoiceNo.Text = dt.Rows[0]["Invoice_No"].ToString().Trim();
            txtInvoiceDate.Text = dt.Rows[0]["Invoice_Date"].ToString().Trim();
            txtInvoiceDoc.Text = dt.Rows[0]["Invoice_Doc"].ToString().Trim();

            txtAWBno.Text = dt.Rows[0]["AWB_No"].ToString().Trim();
            txtAWBDate.Text = dt.Rows[0]["AWB_Date"].ToString().Trim();
            txtAwbIssuedBy.Text = dt.Rows[0]["AWB_Issued_By"].ToString().Trim();
            txtAWBDoc.Text = dt.Rows[0]["AWB_Doc"].ToString().Trim();

            txtPackingList.Text = dt.Rows[0]["Packing_List"].ToString().Trim();
            txtPackingListDate.Text = dt.Rows[0]["Packing_List_Date"].ToString().Trim();
            txtPackingDoc.Text = dt.Rows[0]["Packing_List_Doc"].ToString().Trim();

            txtCertOfOrigin.Text = dt.Rows[0]["Cert_Of_Origin"].ToString().Trim();
            txtCertIssuedBy.Text = dt.Rows[0]["Cert_Of_Origin_Issued_By"].ToString().Trim();
            txtCertOfOriginDoc.Text = dt.Rows[0]["Cert_Of_Origin_Doc"].ToString().Trim();

            txtCustomsInvoice.Text = dt.Rows[0]["Customs_Invoice"].ToString().Trim();
            txtCustomsDate.Text = dt.Rows[0]["Customs_Invoice_Date"].ToString().Trim();
            txtCommodityID.Text = dt.Rows[0]["Commodity"].ToString().Trim();
            //  txtPreviousCommodity.Text = dt.Rows[0]["Commodity"].ToString().Trim();
            fillCommodityDescription();
            txtCustomsDoc.Text = dt.Rows[0]["Customs_Invoice_Doc"].ToString().Trim();

            txtInsPolicy.Text = dt.Rows[0]["Ins_Policy"].ToString().Trim();
            txtInsPolicyDate.Text = dt.Rows[0]["Ins_Policy_Date"].ToString().Trim();
            txtInsPolicyIssuedBy.Text = dt.Rows[0]["Ins_Policy_Issued_By"].ToString().Trim();
            txtInsPolicyDoc.Text = dt.Rows[0]["Ins_Policy_Doc"].ToString().Trim();

            txtGSPDate.Text = dt.Rows[0]["GSP_Date"].ToString().Trim();
            txtContractNo.Text = dt.Rows[0]["Contract_No"].ToString().Trim();
            txtRate.Text = dt.Rows[0]["Contract_Rate"].ToString().Trim();
            txtGSPDoc.Text = dt.Rows[0]["GSP_Doc"].ToString().Trim();

            txtFIRCno.Text = dt.Rows[0]["FIRC_NO"].ToString().Trim();
            txtFIRCdate.Text = dt.Rows[0]["FIRC_DATE"].ToString().Trim();
            txtFIRCnoIssuedBy.Text = dt.Rows[0]["FIRC_Issued_By"].ToString().Trim();
            txtFIRCdoc.Text = dt.Rows[0]["FIRC_Doc"].ToString().Trim();

            txtMiscellaneous.Text = dt.Rows[0]["Miscellaneous"].ToString().Trim();
            txtMiscDoc.Text = dt.Rows[0]["Miscellaneous_Doc"].ToString().Trim();

            if (dt.Rows[0]["DispIndc"].ToString().Trim() != "")
            {
                ddlDispachInd.ClearSelection();
                ddlDispachInd.SelectedValue = dt.Rows[0]["DispIndc"].ToString().Trim();
            }
            DDL_Dispatch2();
            ddlDispBydefault.SelectedValue = dt.Rows[0]["DispByDefaltuValue"].ToString().Trim();// Added by Anand 08-09-2023
            //------------------Comment by Anand08-09-2023------------------
            //if (ddlDispachInd.SelectedValue == "Dispatched directly by exporter")
            //{
            //    ddlDispBydefault.SelectedValue = "Non-Dispatch";
            //}
            //else
            //{
            //    ddlDispBydefault.SelectedValue = dt.Rows[0]["DispByDefaltuValue"].ToString().Trim();
            //}
            //--------------------End-----------------------------
            ddlMercTrade.SelectedValue = dt.Rows[0]["MerchantTrade"].ToString().Trim();
            string _shipment = dt.Rows[0]["Steamer"].ToString().Trim();
            if (_shipment == "A")
                rdbByAir.Checked = true;
            if (_shipment == "S")
                rdbBySea.Checked = true;
            if (_shipment == "R")
                rdbByRoad.Checked = true;


            //txtCoveringFrom.Text = dt.Rows[0]["Covering_From"].ToString().Trim();
            txtCoveringTo.Text = dt.Rows[0]["Covering_To"].ToString().Trim();

            if (dt.Rows[0]["Country_Code"].ToString().Trim() != "")
            {
                txtCountry.Text = dt.Rows[0]["Country_Code"].ToString().Trim();
                txtCountry_TextChanged(null, null);
            }

            else
            {
                txtCountry.Text = "";
            }
            txtReimbValDate.Text = dt.Rows[0]["Reimbursement_ValDate"].ToString().Trim();
            txtReimbClaimDate.Text = dt.Rows[0]["Reimbursement_Claim_Date"].ToString().Trim();
            txtBkName.Text = dt.Rows[0]["Reimbursement_Bank_Name"].ToString().Trim();
            txtBIC.Text = dt.Rows[0]["Reimbursement_BankBICCode"].ToString().Trim();
            txtReimbAmt.Text = dt.Rows[0]["Reimbursement_Amount"].ToString().Trim();
            txtconsigneePartyID.Text = dt.Rows[0]["consigneePartyID"].ToString().Trim();//-------------------------------Nilesh 04/08//2023
            fillConsigneePartyDescription();//--------------------------------------------------------------------------Nilesh 04/08//2023
            //txtNotes.Text = dt.Rows[0]["Document_Remarks"].ToString().Trim();
            txtNotes.Text = "";

            string _sightUsance = dt.Rows[0]["Bill_Type"].ToString().Trim();
            if (_sightUsance == "S")
                rbtnSightBill.Checked = true;
            if (_sightUsance == "U")
                rbtnUsanceBill.Checked = true;

            if (dt.Rows[0]["Currency"].ToString().Trim() != "")
            {
                ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
            }
            else
                ddlCurrency.SelectedIndex = -1;

            string _loanAdv = dt.Rows[0]["Loan"].ToString().Trim();
            if (_loanAdv == "Y")
                chkLoanAdv.Checked = true;

            #region GBASE DETAILS

            txtGBaseCommodityID.Text = dt.Rows[0]["GBase_Commodity_ID"].ToString().Trim();
            fillGBaseCommodityDescription();

            txtOperationType.Text = dt.Rows[0]["Operation_Type"].ToString().Trim();
            txtSettlementOption.Text = dt.Rows[0]["Settlement_Option"].ToString().Trim();
            txtRiskCountry.Text = dt.Rows[0]["Risk_Country"].ToString().Trim();

            if (dt.Rows[0]["Payer"].ToString().Trim() == "S")
            {
                rdbBuyer.Checked = false;
                rdbShipper.Checked = true;
            }
            else
            {
                rdbShipper.Checked = false;
                rdbBuyer.Checked = true;
            }

            txtFundType.Text = dt.Rows[0]["Fund_Type"].ToString().Trim();
            txtBaseRate.Text = dt.Rows[0]["Base_Rate_Code"].ToString().Trim();
            txtGradeCode.Text = dt.Rows[0]["Grade_Code"].ToString().Trim();

            txtDirection.Text = dt.Rows[0]["Direction"].ToString().Trim();
            txtCovrInstr.Text = dt.Rows[0]["Covr_Instruction"].ToString().Trim();
            txtInternalRate.Text = dt.Rows[0]["Internal_Rate"].ToString().Trim();
            txtSpread.Text = dt.Rows[0]["Spread_Rate"].ToString().Trim();
            txtApplNo.Text = dt.Rows[0]["Application_No"].ToString().Trim();
            ddlRemEUC.SelectedValue = dt.Rows[0]["Remarks_EUC"].ToString().Trim();
            //txtDraftNo.Text = dt.Rows[0]["Draft_No"].ToString().Trim();
            //txtRiskCustomer.Text = dt.Rows[0]["Risk_Customer"].ToString().Trim();
            txtVesselName.Text = dt.Rows[0]["Vessel_Name"].ToString().Trim();
            ddlInstructions.SelectedValue = dt.Rows[0]["Instructions"].ToString().Trim();
            txtReimbBank.Text = dt.Rows[0]["Reimbursing_Bank"].ToString().Trim();
            fillReimbBankDescription();
            txtGBaseRemarks.Text = dt.Rows[0]["GBaseRemarks"].ToString().Trim();
            txtMerchandise.Text = dt.Rows[0]["Merchandise"].ToString().Trim();
            txtSpecialInstructions1.Text = dt.Rows[0]["Special_Instructions1"].ToString().Trim();
            txtSpecialInstructions2.Text = dt.Rows[0]["Special_Instructions2"].ToString().Trim();
            txtSpecialInstructions3.Text = dt.Rows[0]["Special_Instructions3"].ToString().Trim();
            txtSpecialInstructions4.Text = dt.Rows[0]["Special_Instructions4"].ToString().Trim();
            txtSpecialInstructions5.Text = dt.Rows[0]["Special_Instructions5"].ToString().Trim();
            txtSpecialInstructions6.Text = dt.Rows[0]["Special_Instructions6"].ToString().Trim();
            txtSpecialInstructions7.Text = dt.Rows[0]["Special_Instructions7"].ToString().Trim();
            txtSpecialInstructions8.Text = dt.Rows[0]["Special_Instructions8"].ToString().Trim();
            txtSpecialInstructions9.Text = dt.Rows[0]["Special_Instructions9"].ToString().Trim();
            txtSpecialInstructions10.Text = dt.Rows[0]["Special_Instructions10"].ToString().Trim();

            txtPayingBankID.Text = dt.Rows[0]["PayingBank"].ToString().Trim();
            fillPayingBankDescription();
            #endregion

            FillGbase_Instructions();
            if (txtBENo.Text == "")
            {
                txtDraftNo.Text = txtInvoiceNo.Text;
            }
            else
            {
                txtDraftNo.Text = txtBENo.Text;
            }
        }
        else
        {
            if (txtCopyFromDocNo.Text.Trim() != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid Document No.');", true);
                txtCopyFromDocNo.Focus();
            }
        }
    }

    protected void btnalert_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveTTDetails_Click(object sender, EventArgs e)
    {
        btnTTRefNoList.Focus();
    }
    protected void btnfillDetails_Click(object sender, EventArgs e)
    {

        string Port_Code = hdnPortCode.Value;
        ddlPortCode.SelectedValue = Port_Code;
        TF_DATA objsave = new TF_DATA();

        SqlParameter p = new SqlParameter("@CustAccNo", SqlDbType.VarChar);
        p.Value = txtCustAcNo.Text;
        SqlParameter p2 = new SqlParameter("@ShippingbillNo", SqlDbType.VarChar);
        p2.Value = txtShippingBillNo.Text;
        SqlParameter p3 = new SqlParameter("@ShippingBillDate", SqlDbType.VarChar);
        p3.Value = txtShippingBillDate.Text;
        SqlParameter p4 = new SqlParameter("@PortCode", SqlDbType.VarChar);
        p4.Value = Port_Code;
        string _query = "TF_GetShippingbillDetails";
        DataTable dt;
        dt = objsave.getData(_query, p, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            ddlCurrencyGRPP.SelectedValue = dt.Rows[0]["FOBCurrency"].ToString();
            txtAmountGRPP.Text = dt.Rows[0]["Amt"].ToString();
            ddlFormType.Text = dt.Rows[0]["ExportType"].ToString();
        }


        string _query2 = "TF_Get_Export_Details";
        DataTable dt2;
        dt2 = objsave.getData(_query2, p, p2, p3, p4);
        if (dt2.Rows.Count > 0)
        {
            txt_invsrno.Text = dt2.Rows[0]["InvoiceSrNo"].ToString();
            txtInvoiceNum.Text = dt2.Rows[0]["InvoiceNo"].ToString();
            txtInvoiceDt.Text = dt2.Rows[0]["InvoiceDate"].ToString();
            txtInvoiceAmt.Text = dt2.Rows[0]["FOBAmt"].ToString();
            txtFreight.Text = dt2.Rows[0]["FreightAmt"].ToString();
            txtInsurance.Text = dt2.Rows[0]["InsuranceAmt"].ToString();
            txtGRPPCustomsNo.Text = dt2.Rows[0]["FormNo"].ToString();
            txtDiscount.Text = dt2.Rows[0]["DiscountAmt"].ToString();
            txtCommissionGRPP.Text = dt2.Rows[0]["CommissionAmt"].ToString();
            txtOthDeduction.Text = dt2.Rows[0]["DeductionAmt"].ToString();
            txtPacking.Text = dt2.Rows[0]["PackagingAmt"].ToString();


            if (dt2.Rows[0]["FreightCurrency"].ToString() != "")
            {
                ddlFreightCurr.SelectedValue = dt2.Rows[0]["FreightCurrency"].ToString();
            }
            if (dt2.Rows[0]["InsuranceCurrency"].ToString() != "")
            {
                ddlInsCurr.SelectedValue = dt2.Rows[0]["InsuranceCurrency"].ToString();
            }
            if (dt2.Rows[0]["DiscountCurrency"].ToString() != "")
            {
                ddlDiscCurr.SelectedValue = dt2.Rows[0]["DiscountCurrency"].ToString();
            }
            if (dt2.Rows[0]["CommissionCurrency"].ToString() != "")
            {
                ddlCommCurr.SelectedValue = dt2.Rows[0]["CommissionCurrency"].ToString();
            }
            if (dt2.Rows[0]["DeductionCurrency"].ToString() != "")
            {
                ddlOthDedCurr.SelectedValue = dt2.Rows[0]["DeductionCurrency"].ToString();
            }
            if (dt2.Rows[0]["PackagingCurrency"].ToString() != "")
            {
                ddlPackChgCurr.SelectedValue = dt2.Rows[0]["PackagingCurrency"].ToString();
            }
            txt_status.Text = dt2.Rows[0]["Status"].ToString();







        }




    }
    protected void onotherADCode(object sender, EventArgs e)
    {
        rdbotherAdcode.Checked = true;
        ADCode.Visible = true;
        txtADCode.Text = "";

    }

    protected void onourADCode(object sender, EventArgs e)
    {
        rdbOurADCode.Checked = true;
        ADCode.Visible = false;
        txtADCode.Text = "";

    }


    protected void txt_invsrno_TextChanged(object sender, EventArgs e)
    {
        string query = "FillfromInvsrno";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@shipbillno", txtShippingBillNo.Text);
        SqlParameter p2 = new SqlParameter("@shipbilldate", txtShippingBillDate.Text);
        SqlParameter p3 = new SqlParameter("@portcode", ddlPortCode.SelectedValue);
        SqlParameter p4 = new SqlParameter("@custid", txtCustAcNo.Text);
        SqlParameter p5 = new SqlParameter("@invsrno", txt_invsrno.Text);

        DataTable dt = objdata.getData(query, p1, p2, p3, p4, p5);

        if (dt.Rows.Count > 0)
        {
            txtInvoiceNum.Text = dt.Rows[0]["InvoiceNo"].ToString();
            txtInvoiceDt.Text = dt.Rows[0]["InvoiceDate"].ToString();
            txtInvoiceAmt.Text = dt.Rows[0]["invoiceamt"].ToString();
            txtFreight.Text = dt.Rows[0]["FreightAmt"].ToString();
            txtInsurance.Text = dt.Rows[0]["InsuranceAmt"].ToString();
            txtDiscount.Text = dt.Rows[0]["DiscountAmt"].ToString();
            txtCommission.Text = dt.Rows[0]["CommissionAmt"].ToString();
            txtOthDeduction.Text = dt.Rows[0]["DeductionAmt"].ToString();
            txtPacking.Text = dt.Rows[0]["PackagingAmt"].ToString();
            txt_status.Text = "Matched";

            if (dt.Rows[0]["FreightCurrency"].ToString().Trim() != "")
            {
                ddlFreightCurr.SelectedValue = dt.Rows[0]["FreightCurrency"].ToString();
            }
            else
            {
                ddlFreightCurr.SelectedItem.Text = "---Select---";
            }

            if (dt.Rows[0]["InsuranceCurrency"].ToString().Trim() != "")
            {
                ddlInsCurr.SelectedItem.Text = dt.Rows[0]["InsuranceCurrency"].ToString();
            }
            else
            {
                ddlInsCurr.SelectedItem.Text = "---Select---";
            }

            if (dt.Rows[0]["DiscountCurrency"].ToString() != "")
            {
                ddlDiscCurr.SelectedValue = dt.Rows[0]["DiscountCurrency"].ToString();
            }
            else
            {
                ddlDiscCurr.SelectedItem.Text = "---Select---";
            }

            if (dt.Rows[0]["CommissionCurrency"].ToString() != "")
            {
                ddlCommCurr.SelectedValue = dt.Rows[0]["CommissionCurrency"].ToString();
            }

            else
            {
                ddlCommCurr.SelectedItem.Text = "---Select---";
            }

            if (dt.Rows[0]["DeductionCurrency"].ToString() != "")
            {
                ddlOthDedCurr.SelectedValue = dt.Rows[0]["DeductionCurrency"].ToString();
            }

            else
            {
                ddlOthDedCurr.SelectedItem.Text = "---Select---";
            }

            if (dt.Rows[0]["PackagingCurrency"].ToString() != "")
            {
                ddlPackChgCurr.SelectedValue = dt.Rows[0]["PackagingCurrency"].ToString();
            }

            else
            {
                ddlPackChgCurr.SelectedItem.Text = "---Select---";
            }



            btnAddGRPPCustoms.Focus();

        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('No Records Found');", true);
            txtInvoiceNo.Focus();
        }


    }


    protected void ddlPortCode_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (txtShippingBillNo.Text != "" && txtShippingBillDate.Text != "" && ddlPortCode.SelectedValue != "Select")
        {
            string query = "filldumpdetails";
            TF_DATA objdata = new TF_DATA();
            DataTable dt = new DataTable();
            SqlParameter p1 = new SqlParameter("@shipbillno", txtShippingBillNo.Text);
            SqlParameter p2 = new SqlParameter("@shipbilldate", txtShippingBillDate.Text);
            SqlParameter p3 = new SqlParameter("@portcode", ddlPortCode.SelectedValue);
            SqlParameter p4 = new SqlParameter("@custid", txtCustAcNo.Text);

            dt = objdata.getData(query, p1, p2, p3, p4);

            if (dt.Rows.Count > 0)
            {
                ddlFormType.SelectedValue = dt.Rows[0]["TypeofExport"].ToString();
                // ddlExportAgency.SelectedValue = dt.Rows[0]["Export_Agency"].ToString();
                ddlCurrencyGRPP.SelectedValue = dt.Rows[0]["FOBCurrency"].ToString();
                txt_status.Text = "Matched";
                txt_status.Enabled = false;
                txtGRPPCustomsNo.Focus();
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Please download EDPMS Dumps');", true);
                txt_status.Text = "Unmatched";
                txt_status.Enabled = false;
                txtGRPPCustomsNo.Focus();

            }

        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Shipping Bill No,Shipping Bill Date,Port Code cant be blank.');", true);
            txtShippingBillNo.Focus();
        }

    }

    public void Validate_Amt()
    {
        double TTAmt = 0;
        double BillAmt = Convert.ToDouble(txtBillAmount.Text);
        TTAmt = Convert.ToDouble(txtTTAmount1.Text) + Convert.ToDouble(txtTTAmount2.Text) + Convert.ToDouble(txtTTAmount3.Text) + Convert.ToDouble(txtTTAmount4.Text) + Convert.ToDouble(txtTTAmount5.Text) + Convert.ToDouble(txtTTAmount6.Text) + Convert.ToDouble(txtTTAmount7.Text) + Convert.ToDouble(txtTTAmount8.Text) + Convert.ToDouble(txtTTAmount9.Text) + Convert.ToDouble(txtTTAmount10.Text) + Convert.ToDouble(txtTTAmount11.Text) + Convert.ToDouble(txtTTAmount12.Text) + Convert.ToDouble(txtTTAmount13.Text) + Convert.ToDouble(txtTTAmount14.Text) + Convert.ToDouble(txtTTAmount15.Text);

    }

    protected void txtGBaseCommodityID_TextChanged(object sender, EventArgs e)
    {
        fillGBaseCommodityDescription();
        txtCustomsDoc.Focus();
    }

    protected void txtReimbBank_TextChanged(object sender, EventArgs e)
    {
        fillReimbBankDescription();
    }

    private void fillPayingBankDescription()
    {
        lblPayingBankDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtPayingBankID.Text;
        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblPayingBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblPayingBankDesc.Text.Length > 25)
            {
                lblPayingBankDesc.ToolTip = lblPayingBankDesc.Text;
                lblPayingBankDesc.Text = lblPayingBankDesc.Text.Substring(0, 25) + "...";
            }
        }
        else
        {
            txtPayingBankID.Text = "";
            lblPayingBankDesc.Text = "";
        }
    }

    protected void txtPayingBankID_TextChanged(object sender, EventArgs e)
    {
        fillPayingBankDescription();
    }

    protected void txtCRCustAbbr_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        p1.Value = txtCRCustAbbr.Text.Trim();
        string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            txtCRGLCode.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txtCRCustAcNo1.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            txtCRCustAcNo2.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
            txtCRCurr.Focus();
        }
        else
        {

            txtCRCustAbbr.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        }
    }

    protected void txtDRCustAbbr1_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        p1.Value = txtDRCustAbbr1.Text.Trim();
        string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            txtDRGLCode1.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txtDRCustAcNo11.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            txtDRCustAcNo12.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
            txtDRCurr1.Focus();
        }
        else
        {

            txtDRCustAbbr1.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        }
    }

    protected void txtDRCustAbbr2_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        p1.Value = txtDRCustAbbr2.Text.Trim();
        string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            txtDRGLCode2.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txtDRCustAcNo21.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            txtDRCustAcNo22.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
            txtDRCurr2.Focus();
        }
        else
        {

            txtDRCustAbbr2.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        }
    }

    protected void txtDRCustAbbr3_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        p1.Value = txtDRCustAbbr3.Text.Trim();
        string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            txtDRGLCode3.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txtDRCustAcNo31.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            txtDRCustAcNo32.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
            txtDRCurr3.Focus();
        }
        else
        {

            txtDRCustAbbr3.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        }
    }

    protected void txtDRCustAbbr4_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        p1.Value = txtDRCustAbbr4.Text.Trim();
        string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            txtDRGLCode4.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txtDRCustAcNo41.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            txtDRCustAcNo42.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
            txtDRCurr4.Focus();
        }
        else
        {
            txtDRCustAbbr4.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        }
    }

    protected void txtDRCustAbbr5_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Cust_Abbr", SqlDbType.VarChar);
        p1.Value = txtDRCustAbbr5.Text.Trim();
        string _query = "TF_GetCustomerMasterDetailsByCustAbbr";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtDRGLCode5.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txtDRCustAcNo51.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(0, 3).Trim();
            txtDRCustAcNo52.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString().Substring(6).Trim();
            txtDRCurr5.Focus();
        }
        else
        {
            txtDRCustAbbr5.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('This Customer Does not exist in the Customer Master');", true);
        }
    }
    //protected void txtBENo_TextChanged(object sender, EventArgs e)
    //{
    //    txtDraftNo.Text = txtBENo.Text.Trim();
    //}


    //protected void rdbTenor_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbtnAfterAWB.Checked == true)
    //    {
    //        txtOtherTenorRemarks.Text = "Days After AWB/BL Date";
    //    }
    //    else if (rbtnFromAWB.Checked == true)
    //    {
    //        txtOtherTenorRemarks.Text = "Days From AWB/BL Date";
    //    }
    //    else if (rbtnSight.Checked == true)
    //    {
    //        txtOtherTenorRemarks.Text = "Days Sight";
    //    }
    //    else if (rbtnDA.Checked == true)
    //    {
    //        txtOtherTenorRemarks.Text = "Days D/A";
    //    }
    //    else if (rbtnFromInvoice.Checked == true)
    //    {
    //        txtOtherTenorRemarks.Text = "Days From Invoice Date";
    //    }
    //    else if (rbtnOthers.Checked == true)
    //    {
    //        txtOtherTenorRemarks.Text = "";
    //    }

    //}

    //protected void TabContainerMain_ActiveTabIndexChanged(object sender, EventArgs e)
    //{
    // // Add This in TabContainer :- OnActiveTabChanged="TabContainerMain_ActiveTabIndexChanged" AutoPostBack="true"
    //    if (txtDocType.Text.Trim() == "EB")
    //    {
    //        if (TabContainerMain.ActiveTab.ID.Trim() == "tbGBaseDetails")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('G-Base Details are not mandatory in Advance Bills');", true);
    //        }
    //    }
    //}}
    protected void ddlDispachInd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDispachInd.SelectedItem.Text == "By Bank")
        {
            fillReimbBankDescription();
            FillGbase_Instructions();
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@dispatchid", ddlDispachInd.SelectedItem.Text);

            string _query = "dispatch_thired";
            DataTable dt;
            ListItem li = new ListItem();
            //li.Value = "0";
            //===================Account Type==========================//
            //p1.Value = "Account Type";
            dt = objData.getData(_query, p1);
            ddlDispBydefault.Items.Clear();
            li.Value = "";
            if (dt.Rows.Count > 0)
            {
                li.Text = "--Select--";
                ddlDispBydefault.DataSource = dt.DefaultView;
                ddlDispBydefault.DataTextField = "Dispatch_Name";
                ddlDispBydefault.DataValueField = "Dispatch_Name";
                ddlDispBydefault.DataBind();
            }
            else
                li.Text = "No record(s) found";

            ddlDispBydefault.Items.Insert(0, li);
        }
        else if (ddlDispachInd.SelectedValue == "Dispatched directly by exporter")
        {
            //----------- Modified by Anand05072023
            fillReimbBankDescription();
            FillGbase_Instructions();
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@dispatchid", ddlDispachInd.SelectedValue);

            string _query = "dispatch_thired";
            DataTable dt;

            ////li.Value = "0";
            ////===================Account Type==========================//
            ////p1.Value = "Account Type";
            dt = objData.getData(_query, p1);
            ddlDispBydefault.Items.Clear();
            ListItem li = new ListItem();
            li.Value = "";
            //li.Text = "Non-Dispatch";
            if (dt.Rows.Count > 0)
            {
                li.Text = "--Select--";
                ddlDispBydefault.DataSource = dt.DefaultView;
                ddlDispBydefault.DataTextField = "Dispatch_Name";
                ddlDispBydefault.DataValueField = "Dispatch_Name";
                ddlDispBydefault.DataBind();
            }
            //--------------End---------------------
            //else
            //    li.Text = "No record(s) found";

            ddlDispBydefault.Items.Insert(0, li);
            txtSpecialInstructions1.Text = "";
            txtSpecialInstructions2.Text = "";
            txtSpecialInstructions3.Text = "";
            txtSpecialInstructions4.Text = "";
            txtSpecialInstructions5.Text = "";
            txtSpecialInstructions6.Text = "";
            txtSpecialInstructions7.Text = "";
            txtSpecialInstructions8.Text = "";
            txtSpecialInstructions9.Text = "";
            txtSpecialInstructions10.Text = "";
        }


    }

    protected void DDL_Dispatch()
    {
        TF_DATA objData = new TF_DATA();
        //SqlParameter p1 = new SqlParameter("@EnumerationType", SqlDbType.VarChar);
        string _query = "dispatch_first";
        DataTable dt;
        ListItem li = new ListItem();
        //li.Value = "0";
        //===================Account Type==========================//
        //p1.Value = "Account Type";
        dt = objData.getData(_query);
        ddlDispachInd.Items.Clear();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "--Select--";
            ddlDispachInd.DataSource = dt.DefaultView;
            ddlDispachInd.DataTextField = "DispatchValue";
            ddlDispachInd.DataValueField = "Dispatch_ID";
            ddlDispachInd.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlDispachInd.Items.Insert(0, li);
    }

    protected void DDL_Dispatch2()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Dispatch_Name", ddlDispachInd.SelectedValue);
        string _query = "dispatch_second";
        DataTable dt;
        ListItem li = new ListItem();
        ListItem l2 = new ListItem();
        //li.Value = "0";
        //===================Account Type==========================//
        //p1.Value = "Account Type";
        dt = objData.getData(_query, p1);
        ddlDispBydefault.Items.Clear();
        li.Value = "";
        li.Text = "--Select--";
        if (dt.Rows.Count > 0)
        {

            ddlDispBydefault.DataSource = dt.DefaultView;
            ddlDispBydefault.DataTextField = "Dispatch_Name";
            ddlDispBydefault.DataValueField = "Dispatch_Name";
            ddlDispBydefault.DataBind();
        }
        else
            li.Text = "--Select--";

        ddlDispBydefault.Items.Insert(0, li);
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
            else if (txtBillAmount.Text.Trim() == "" || txtBillAmount.Text.Trim() == "0.00")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Amount for Verifying LEI details.')", true);
                txtBillAmount.Focus();
            }
            else if (txtDueDate.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Fill Due date for Verifying LEI details.')", true);
                txtDueDate.Focus();
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
            lblLEI_CUST_Remark.Visible = false;
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
            SqlParameter p2 = new SqlParameter("@DueDate", txtDueDate.Text.ToString().Trim());
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
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_CUST_Remark.Text = dt.Rows[0]["LEI_Expiry_Date"].ToString() + "...Not Verified.";
                    lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Red;
                    hdncustleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_CUST_Remark.Text = " " + "...Not Verified.";
                lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Red;
                hdncustleiexpiry.Value = "";
            }
            lblLEIExpiry_CUST_Remark.Visible = false;
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
                lblLEI_Overseas_Remark.Text = "Overseas Party ID is not available."; ;
                lblLEI_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
            }
            lblLEI_Overseas_Remark.Visible = false;
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
            SqlParameter p2 = new SqlParameter("@DueDate", txtDueDate.Text.ToString().Trim());
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
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Overseas_Remark.Text = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString() + "...Not Verified.";
                    lblLEIExpiry_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
                    hdnoverseasleiexpiry.Value = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString();
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_Overseas_Remark.Text = " " + "...Not Verified.";
                lblLEIExpiry_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
                hdnoverseasleiexpiry.Value = "";
            }
            lblLEIExpiry_Overseas_Remark.Visible = false;
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
            string _ForeignOrLocal = Request.QueryString["ForeignOrLocal"].ToString().Trim();

            if (_ForeignOrLocal == "F")
            {
                TF_DATA objData = new TF_DATA();
                SqlParameter p1 = new SqlParameter("@CurrCode", SqlDbType.VarChar);
                SqlParameter p2 = new SqlParameter("@Date", txtDateRcvd.Text);
                p1.Value = ddlCurrency.SelectedItem.Text.ToString();
                string _query = "TF_EXP_GetLEI_RateCardDetails";
                DataTable dt = objData.getData(_query, p1, p2);
                string Exch_rate = "";
                if (dt.Rows.Count > 0)
                {
                    Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                }
                else
                {
                    Exch_rate = "0";
                    lbl_Exch_rate.Text = "0.00";
                }
                LEIAmtCheck.Text = "";
                string result = "";
                string txtbillamt = txtBillAmount.Text;
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
                                if (hdnLeisavedraft.Value == "D")
                                {
                                    LEIverify.Text = "Please Verify LEI.";
                                    LEIverify.ForeColor = System.Drawing.Color.Red;
                                }
                                else
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
                                }
                                Leicount = Leicount + 1;
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
                            LEIAmtCheck.ForeColor = System.Drawing.Color.Green;
                            LEIAmtCheck.Font.Size = 13;
                            hdnLeiFlag.Value = "N";
                            hdnLeiSpecialFlag.Value = "";
                            hdnbillamtinr.Value = dtLimit.Rows[0]["billamtInr"].ToString().Trim();
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

    private void Check_LEI_SpecialFlag()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            String CustAbbrLEI = "";
            hdnBranchCode.Value = Request.QueryString["BranchCode"].Trim();
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
            SqlParameter p3 = new SqlParameter("@Trans_Status", "L");
            string _query = "TF_EXP_GetLEISpecial_Customer";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                hdnLeiSpecialFlag.Value = "R";
                btnLEI.Visible = true;
                SpanLei5.Visible = true;
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
                        if (hdnLeisavedraft.Value == "D")
                        {
                            LEIverify.Text = "";
                            LEIverify.Text = "Please Verify LEI.";
                            LEIverify.ForeColor = System.Drawing.Color.Red;
                            btnLEI.Visible = true;
                        }
                        else
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
                        }
                        Leicount = Leicount + 1;
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

    protected void txtExchRtEBR_TextChanged(object sender, EventArgs e)
    {
        Check_LEI_ThresholdLimit();
    }
    protected void txtBillAmount_TextChanged(object sender, EventArgs e)
    {
        Check_LEI_ThresholdLimit();
    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGbase_ReimbursingBank();
        Check_LEI_ThresholdLimit();
        txtNoOfDays_TextChanged(null,null);
    }
    //Added by bhupen 23032023
    protected void ddlDispBydefault_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGbase_Instructions();
    }
    protected void fillGbase_ReimbursingBank()
    {
        SqlParameter p1 = new SqlParameter("@Curr", SqlDbType.VarChar);
        p1.Value = ddlCurrency.Text.Trim();
        string _query = "TF_GetReimbursingBankList_Curr";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtReimbBank.Text = dt.Rows[0]["BankCode"].ToString().Trim();
            fillReimbBankDescription();
        }
        else
        {
            txtReimbBank.Text = "";
            fillReimbBankDescription();
        }
    }
    protected void FillGbase_Instructions()
    {
        if (txtDocType.Text.Trim() == "BLA" || txtDocType.Text.Trim() == "BLU")
        {
        }
        else
        {
            if (ddlDispachInd.Text.Trim() == "By Bank" && ddlDispBydefault.Text.Trim() == "Sent to Bank")
            {
                if (rbtnSightBill.Checked)
                {
                    if (txtLCNo.Text.Trim() == "" && txtLCNoDate.Text.Trim() == "" || txtLCNo.Text.Trim() == "" && txtLCNoDate.Text.Trim() == "__/__/____")
                    {
                        ddlInstructions.SelectedValue = "1,3,8,9";
                    }
                    else
                    {
                        ddlInstructions.SelectedValue = "";
                    }
                }
                else
                {
                    if (txtLCNo.Text.Trim() == "" && txtLCNoDate.Text.Trim() == "" || txtLCNo.Text.Trim() == "" && txtLCNoDate.Text.Trim() == "__/__/____")
                    {
                        ddlInstructions.SelectedValue = "2,4,8,9";
                    }
                    else
                    {
                        ddlInstructions.SelectedValue = "";
                    }
                }
            }
            else
            {
                ddlInstructions.SelectedValue = "";
            }
        }
    }
    protected void txtLCNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FillGbase_Instructions();
            if (txtDocType.Text.Trim() == "BCA" || txtDocType.Text.Trim() == "BCU")
            {
                if (txtLCNo.Text.Trim() == "")
                {
                    txtDirection.Text = "6";
                    txtCovrInstr.Text = "L";
                }
                else
                {
                    txtDirection.Text = "5";
                    txtCovrInstr.Text = "L";
                }
            }
            //-------------------- Added By Anand 05-07-2023-------------------
            if (txtLCNo.Text.Trim() != "" && txtLCNoDate.Text.Trim() != "__/__/____")
            {

                ddlDispachInd.Text = "By Bank";
                DDL_Dispatch2();
                ddlDispBydefault.Text = "Sent to Bank";
                fillReimbBankDescription();
                txtSpecialInstructions6.Text = "DRAWING AMOUNT HAS BEEN ENDORSED ON ORIGINAL CREDIT";

            }

            //------------------End---------------------------------
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }

    protected void txtLCNoDate_TextChanged(object sender, EventArgs e)
    {
        if (txtLCNo.Text.Trim() != "" && txtLCNoDate.Text.Trim() != "__/__/____")
        {

            ddlDispachInd.Text = "By Bank";
            DDL_Dispatch2();
            ddlDispBydefault.Text = "Sent to Bank";
            //fillReimbBankDescription();
            txtSpecialInstructions6.Text = "DRAWING AMOUNT HAS BEEN ENDORSED ON ORIGINAL CREDIT";

        }

    }
    //------------------------- Create By Anand 08-06-2023-------------------

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
                case "F1":
                    txtFIRCTobeAdjustedinSB1_OB.Text = val3.ToString();
                    break;
                case "F2":
                    txtFIRCTobeAdjustedinSB2_OB.Text = val3.ToString();
                    break;
                case "F3":
                    txtFIRCTobeAdjustedinSB3_OB.Text = val3.ToString();
                    break;
                case "F4":
                    txtFIRCTobeAdjustedinSB4_OB.Text = val3.ToString();
                    break;
                case "F5":
                    txtFIRCTobeAdjustedinSB5_OB.Text = val3.ToString();
                    break;
                case "F6":
                    txtFIRCTobeAdjustedinSB6_OB.Text = val3.ToString();
                    break;
                case "F7":
                    txtFIRCTobeAdjustedinSB7_OB.Text = val3.ToString();
                    break;
                case "F8":
                    txtFIRCTobeAdjustedinSB8_OB.Text = val3.ToString();
                    break;
                case "F9":
                    txtFIRCTobeAdjustedinSB9_OB.Text = val3.ToString();
                    break;
                case "F10":
                    txtFIRCTobeAdjustedinSB10_OB.Text = val3.ToString();
                    break;
                case "F11":
                    txtFIRCTobeAdjustedinSB11_OB.Text = val3.ToString();
                    break;
                case "F12":
                    txtFIRCTobeAdjustedinSB12_OB.Text = val3.ToString();
                    break;
                case "F13":
                    txtFIRCTobeAdjustedinSB13_OB.Text = val3.ToString();
                    break;
                case "F14":
                    txtFIRCTobeAdjustedinSB14_OB.Text = val3.ToString();
                    break;
                case "F15":
                    txtFIRCTobeAdjustedinSB15_OB.Text = val3.ToString();
                    break;

            }
        }
    }
    protected void txtTTRefNo1_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlTTCurrency1.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency1.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency2.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency2.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency3.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency3.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency4.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency4.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency5.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency5.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency6.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency6.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency7.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency7.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency8.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency8.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency9.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency9.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency10.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency10.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency11.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency11.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency12.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency12.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency13.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency13.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency14.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency14.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency15.SelectedValue)
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
        if (ddlCurrency.SelectedValue == ddlTTCurrency15.SelectedValue)
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

    // --------------EFRC Reference No.--------------------------------

    protected void txtFIRCAmount1_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency1_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr1_OB.Enabled = false;
            txtFIRCCrossCurrRate1_OB.Enabled = false;
            ddlFIRCRealisedCurr1_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate1_OB.Text = "";
            txtFIRCTobeAdjustedinSB1_OB.Text = txtFIRCAmount1_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr1_OB.Enabled = true;
            txtFIRCCrossCurrRate1_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB1_OB.Text = txtFIRCAmount1_OB.Text;
            TTCurrPairCalc(txtFIRCAmount1_OB.Text, ddlFIRCCurrency1_OB.SelectedValue, ddlFIRCRealisedCurr1_OB.SelectedValue, txtFIRCCrossCurrRate1_OB.Text, "F1");
        }
    }
    protected void txtFIRCCrossCurrRate1_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr1_OB.SelectedValue == "0" && txtFIRCCrossCurrRate1_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr1');", true);
            txtFIRCCrossCurrRate1_OB.Text = "";
            ddlFIRCRealisedCurr1_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount1_OB.Text, ddlFIRCCurrency1_OB.SelectedValue, ddlFIRCRealisedCurr1_OB.SelectedValue, txtFIRCCrossCurrRate1_OB.Text, "F1");
        }
    }

    protected void ddlFIRCCurrency1_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency1_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr1_OB.Enabled = false;
            txtFIRCCrossCurrRate1_OB.Enabled = false;
            ddlFIRCRealisedCurr1_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate1_OB.Text = "";
            txtFIRCTobeAdjustedinSB1_OB.Text = txtFIRCAmount1_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr1_OB.Enabled = true;
            txtFIRCCrossCurrRate1_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount2_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency2_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr2_OB.Enabled = false;
            txtFIRCCrossCurrRate2_OB.Enabled = false;
            ddlFIRCRealisedCurr2_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate2_OB.Text = "";
            txtFIRCTobeAdjustedinSB2_OB.Text = txtFIRCAmount2_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr2_OB.Enabled = true;
            txtFIRCCrossCurrRate2_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB2_OB.Text = txtFIRCAmount2_OB.Text;
            TTCurrPairCalc(txtFIRCAmount2_OB.Text, ddlFIRCCurrency2_OB.SelectedValue, ddlFIRCRealisedCurr2_OB.SelectedValue, txtFIRCCrossCurrRate2_OB.Text, "F2");
        }
    }

    protected void txtFIRCCrossCurrRate2_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr2_OB.SelectedValue == "0" && txtFIRCCrossCurrRate2_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr2');", true);
            txtFIRCCrossCurrRate2_OB.Text = "";
            ddlFIRCRealisedCurr2_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount2_OB.Text, ddlFIRCCurrency2_OB.SelectedValue, ddlFIRCRealisedCurr2_OB.SelectedValue, txtFIRCCrossCurrRate2_OB.Text, "F2");
        }

    }

    protected void ddlFIRCCurrency2_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency2_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr2_OB.Enabled = false;
            txtFIRCCrossCurrRate2_OB.Enabled = false;
            ddlFIRCRealisedCurr2_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate2_OB.Text = "";
            txtFIRCTobeAdjustedinSB2_OB.Text = txtFIRCAmount2_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr2_OB.Enabled = true;
            txtFIRCCrossCurrRate2_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount3_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency3_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr3_OB.Enabled = false;
            txtFIRCCrossCurrRate3_OB.Enabled = false;
            ddlFIRCRealisedCurr3_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate3_OB.Text = "";
            txtFIRCTobeAdjustedinSB3_OB.Text = txtFIRCAmount3_OB.Text;
        }

        else
        {
            ddlFIRCRealisedCurr3_OB.Enabled = true;
            txtFIRCCrossCurrRate3_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB3_OB.Text = txtFIRCAmount3_OB.Text;
            TTCurrPairCalc(txtFIRCAmount3_OB.Text, ddlFIRCCurrency3_OB.SelectedValue, ddlFIRCRealisedCurr3_OB.SelectedValue, txtFIRCCrossCurrRate3_OB.Text, "F3");
        }
    }

    protected void txtFIRCCrossCurrRate3_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr3_OB.SelectedValue == "0" && txtFIRCCrossCurrRate3_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr3');", true);
            txtFIRCCrossCurrRate3_OB.Text = "";
            ddlFIRCRealisedCurr3_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount3_OB.Text, ddlFIRCCurrency3_OB.SelectedValue, ddlFIRCRealisedCurr3_OB.SelectedValue, txtFIRCCrossCurrRate3_OB.Text, "F3");
        }

    }
    protected void ddlFIRCCurrency3_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency3_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr3_OB.Enabled = false;
            txtFIRCCrossCurrRate3_OB.Enabled = false;
            ddlFIRCRealisedCurr3_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate3_OB.Text = "";
            txtFIRCTobeAdjustedinSB3_OB.Text = txtFIRCAmount3_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr3_OB.Enabled = true;
            txtFIRCCrossCurrRate3_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount4_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency4_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr4_OB.Enabled = false;
            txtFIRCCrossCurrRate4_OB.Enabled = false;
            ddlFIRCRealisedCurr4_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate4_OB.Text = "";
            txtFIRCTobeAdjustedinSB4_OB.Text = txtFIRCAmount4_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr4_OB.Enabled = true;
            txtFIRCCrossCurrRate4_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB4_OB.Text = txtFIRCAmount4_OB.Text;
            TTCurrPairCalc(txtFIRCAmount4_OB.Text, ddlFIRCCurrency4_OB.SelectedValue, ddlFIRCRealisedCurr4_OB.SelectedValue, txtFIRCCrossCurrRate4_OB.Text, "F4");
        }
    }

    protected void txtFIRCCrossCurrRate4_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr4_OB.SelectedValue == "0" && txtFIRCCrossCurrRate4_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr4');", true);
            txtFIRCCrossCurrRate4_OB.Text = "";
            ddlFIRCRealisedCurr4_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount4_OB.Text, ddlFIRCCurrency4_OB.SelectedValue, ddlFIRCRealisedCurr4_OB.SelectedValue, txtFIRCCrossCurrRate4_OB.Text, "F4");
        }
    }
    protected void ddlFIRCCurrency4_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency4_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr4_OB.Enabled = false;
            txtFIRCCrossCurrRate4_OB.Enabled = false;
            ddlFIRCRealisedCurr4_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate4_OB.Text = "";
            txtFIRCTobeAdjustedinSB4_OB.Text = txtFIRCAmount4_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr4_OB.Enabled = true;
            txtFIRCCrossCurrRate4_OB.Enabled = true;
        }
    }
    protected void txtFIRCAmount5_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency5_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr5_OB.Enabled = false;
            txtFIRCCrossCurrRate5_OB.Enabled = false;
            ddlFIRCRealisedCurr5_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate5_OB.Text = "";
            txtFIRCTobeAdjustedinSB5_OB.Text = txtFIRCAmount5_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr5_OB.Enabled = true;
            txtFIRCCrossCurrRate5_OB.Enabled = true;
            //txtFIRCTobeAdjustedinSB5_OB.Text = txtFIRCAmount5_OB.Text;
            TTCurrPairCalc(txtFIRCAmount5_OB.Text, ddlFIRCCurrency5_OB.SelectedValue, ddlFIRCRealisedCurr5_OB.SelectedValue, txtFIRCCrossCurrRate5_OB.Text, "F5");
        }
    }

    protected void txtFIRCCrossCurrRate5_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr5_OB.SelectedValue == "0" && txtFIRCCrossCurrRate5_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr5');", true);
            txtFIRCCrossCurrRate5_OB.Text = "";
            ddlFIRCRealisedCurr5_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount5_OB.Text, ddlFIRCCurrency5_OB.SelectedValue, ddlFIRCRealisedCurr5_OB.SelectedValue, txtFIRCCrossCurrRate5_OB.Text, "F5");
        }

    }

    protected void ddlFIRCCurrency5_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency5_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr5_OB.Enabled = false;
            txtFIRCCrossCurrRate5_OB.Enabled = false;
            ddlFIRCRealisedCurr5_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate5_OB.Text = "";
            txtFIRCTobeAdjustedinSB5_OB.Text = txtFIRCAmount5_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr5_OB.Enabled = true;
            txtFIRCCrossCurrRate5_OB.Enabled = true;
        }
    }
    protected void txtFIRCAmount6_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency6_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr6_OB.Enabled = false;
            txtFIRCCrossCurrRate6_OB.Enabled = false;
            ddlFIRCRealisedCurr6_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate6_OB.Text = "";
            txtFIRCTobeAdjustedinSB6_OB.Text = txtFIRCAmount6_OB.Text;
        }

        else
        {
            ddlFIRCRealisedCurr6_OB.Enabled = true;
            txtFIRCCrossCurrRate6_OB.Enabled = true;
            //txtFIRCTobeAdjustedinSB6_OB.Text = txtFIRCAmount6_OB.Text;
            TTCurrPairCalc(txtFIRCAmount6_OB.Text, ddlFIRCCurrency6_OB.SelectedValue, ddlFIRCRealisedCurr6_OB.SelectedValue, txtFIRCCrossCurrRate6_OB.Text, "F6");
        }
    }

    protected void txtFIRCCrossCurrRate6_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr6_OB.SelectedValue == "0" && txtFIRCCrossCurrRate6_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr6');", true);
            txtFIRCCrossCurrRate6_OB.Text = "";
            ddlFIRCRealisedCurr6_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount6_OB.Text, ddlFIRCCurrency6_OB.SelectedValue, ddlFIRCRealisedCurr6_OB.SelectedValue, txtFIRCCrossCurrRate6_OB.Text, "F6");
        }

    }

    protected void ddlFIRCCurrency6_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency6_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr6_OB.Enabled = false;
            txtFIRCCrossCurrRate6_OB.Enabled = false;
            ddlFIRCRealisedCurr6_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate6_OB.Text = "";
            txtFIRCTobeAdjustedinSB6_OB.Text = txtFIRCAmount6_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr6_OB.Enabled = true;
            txtFIRCCrossCurrRate6_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount7_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency7_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr7_OB.Enabled = false;
            txtFIRCCrossCurrRate7_OB.Enabled = false;
            ddlFIRCRealisedCurr7_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate7_OB.Text = "";
            txtFIRCTobeAdjustedinSB7_OB.Text = txtFIRCAmount7_OB.Text;
        }

        else
        {
            ddlFIRCRealisedCurr7_OB.Enabled = true;
            txtFIRCCrossCurrRate7_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB7_OB.Text = txtFIRCAmount7_OB.Text;
            TTCurrPairCalc(txtFIRCAmount7_OB.Text, ddlFIRCCurrency7_OB.SelectedValue, ddlFIRCRealisedCurr7_OB.SelectedValue, txtFIRCCrossCurrRate7_OB.Text, "F7");
        }
    }

    protected void txtFIRCCrossCurrRate7_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr7_OB.SelectedValue == "0" && txtFIRCCrossCurrRate7_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr7');", true);
            txtFIRCCrossCurrRate7_OB.Text = "";
            ddlFIRCRealisedCurr7_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount7_OB.Text, ddlFIRCCurrency7_OB.SelectedValue, ddlFIRCRealisedCurr7_OB.SelectedValue, txtFIRCCrossCurrRate7_OB.Text, "F7");
        }
    }

    protected void ddlFIRCCurrency7_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency7_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr7_OB.Enabled = false;
            txtFIRCCrossCurrRate7_OB.Enabled = false;
            ddlFIRCRealisedCurr7_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate7_OB.Text = "";
            txtFIRCTobeAdjustedinSB7_OB.Text = txtFIRCAmount7_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr7_OB.Enabled = true;
            txtFIRCCrossCurrRate7_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount8_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency8_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr8_OB.Enabled = false;
            txtFIRCCrossCurrRate8_OB.Enabled = false;
            ddlFIRCRealisedCurr8_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate8_OB.Text = "";
            txtFIRCTobeAdjustedinSB8_OB.Text = txtFIRCAmount8_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr8_OB.Enabled = true;
            txtFIRCCrossCurrRate8_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB8_OB.Text = txtFIRCAmount8_OB.Text;
            TTCurrPairCalc(txtFIRCAmount8_OB.Text, ddlFIRCCurrency8_OB.SelectedValue, ddlFIRCRealisedCurr8_OB.SelectedValue, txtFIRCCrossCurrRate8_OB.Text, "F8");
        }
    }

    protected void txtFIRCCrossCurrRate8_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr8_OB.SelectedValue == "0" && txtFIRCCrossCurrRate8_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr8');", true);
            txtFIRCCrossCurrRate8_OB.Text = "";
            ddlFIRCRealisedCurr8_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount8_OB.Text, ddlFIRCCurrency8_OB.SelectedValue, ddlFIRCRealisedCurr8_OB.SelectedValue, txtFIRCCrossCurrRate8_OB.Text, "F8");
        }
    }

    protected void ddlFIRCCurrency8_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency8_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr8_OB.Enabled = false;
            txtFIRCCrossCurrRate8_OB.Enabled = false;
            ddlFIRCRealisedCurr8_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate8_OB.Text = "";
            txtFIRCTobeAdjustedinSB8_OB.Text = txtFIRCAmount8_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr8_OB.Enabled = true;
            txtFIRCCrossCurrRate8_OB.Enabled = true;
        }
    }
    protected void txtFIRCAmount9_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency9_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr9_OB.Enabled = false;
            txtFIRCCrossCurrRate9_OB.Enabled = false;
            ddlFIRCRealisedCurr9_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate9_OB.Text = "";
            txtFIRCTobeAdjustedinSB9_OB.Text = txtFIRCAmount9_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr9_OB.Enabled = true;
            txtFIRCCrossCurrRate9_OB.Enabled = true;
            //  txtFIRCTobeAdjustedinSB9_OB.Text = txtFIRCAmount9_OB.Text;
            TTCurrPairCalc(txtFIRCAmount9_OB.Text, ddlFIRCCurrency9_OB.SelectedValue, ddlFIRCRealisedCurr9_OB.SelectedValue, txtFIRCCrossCurrRate9_OB.Text, "F9");
        }
    }

    protected void txtFIRCCrossCurrRate9_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr9_OB.SelectedValue == "0" && txtFIRCCrossCurrRate9_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr9');", true);
            txtFIRCCrossCurrRate9_OB.Text = "";
            ddlFIRCRealisedCurr9_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount9_OB.Text, ddlFIRCCurrency9_OB.SelectedValue, ddlFIRCRealisedCurr9_OB.SelectedValue, txtFIRCCrossCurrRate9_OB.Text, "F9");
        }
    }

    protected void ddlFIRCCurrency9_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency9_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr9_OB.Enabled = false;
            txtFIRCCrossCurrRate9_OB.Enabled = false;
            ddlFIRCRealisedCurr9_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate9_OB.Text = "";
            txtFIRCTobeAdjustedinSB9_OB.Text = txtFIRCAmount9_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr9_OB.Enabled = true;
            txtFIRCCrossCurrRate9_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount10_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency10_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr10_OB.Enabled = false;
            txtFIRCCrossCurrRate10_OB.Enabled = false;
            ddlFIRCRealisedCurr10_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate10_OB.Text = "";
            txtFIRCTobeAdjustedinSB10_OB.Text = txtFIRCAmount10_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr10_OB.Enabled = true;
            txtFIRCCrossCurrRate10_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB10_OB.Text = txtFIRCAmount10_OB.Text;
            TTCurrPairCalc(txtFIRCAmount10_OB.Text, ddlFIRCCurrency10_OB.SelectedValue, ddlFIRCRealisedCurr10_OB.SelectedValue, txtFIRCCrossCurrRate10_OB.Text, "F10");

        }

    }

    protected void txtFIRCCrossCurrRate10_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr10_OB.SelectedValue == "0" && txtFIRCCrossCurrRate10_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr10');", true);
            txtFIRCCrossCurrRate10_OB.Text = "";
            ddlFIRCRealisedCurr10_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount10_OB.Text, ddlFIRCCurrency10_OB.SelectedValue, ddlFIRCRealisedCurr10_OB.SelectedValue, txtFIRCCrossCurrRate10_OB.Text, "F10");
        }
    }

    protected void ddlFIRCCurrency10_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency10_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr10_OB.Enabled = false;
            txtFIRCCrossCurrRate10_OB.Enabled = false;
            ddlFIRCRealisedCurr10_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate10_OB.Text = "";
            txtFIRCTobeAdjustedinSB10_OB.Text = txtFIRCAmount10_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr10_OB.Enabled = true;
            txtFIRCCrossCurrRate10_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount11_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency11_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr11_OB.Enabled = false;
            txtFIRCCrossCurrRate11_OB.Enabled = false;
            ddlFIRCRealisedCurr11_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate11_OB.Text = "";
            txtFIRCTobeAdjustedinSB11_OB.Text = txtFIRCAmount11_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr11_OB.Enabled = true;
            txtFIRCCrossCurrRate11_OB.Enabled = true;
            //txtFIRCTobeAdjustedinSB11_OB.Text = txtFIRCAmount11_OB.Text;
            TTCurrPairCalc(txtFIRCAmount11_OB.Text, ddlFIRCCurrency11_OB.SelectedValue, ddlFIRCRealisedCurr11_OB.SelectedValue, txtFIRCCrossCurrRate11_OB.Text, "F11");
        }
    }

    protected void txtFIRCCrossCurrRate11_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr11_OB.SelectedValue == "0" && txtFIRCCrossCurrRate11_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr11');", true);
            txtFIRCCrossCurrRate11_OB.Text = "";
            ddlFIRCRealisedCurr11_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount11_OB.Text, ddlFIRCCurrency11_OB.SelectedValue, ddlFIRCRealisedCurr11_OB.SelectedValue, txtFIRCCrossCurrRate11_OB.Text, "F11");
        }
    }

    protected void ddlFIRCCurrency11_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency11_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr11_OB.Enabled = false;
            txtFIRCCrossCurrRate11_OB.Enabled = false;
            ddlFIRCRealisedCurr11_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate11_OB.Text = "";
            txtFIRCTobeAdjustedinSB11_OB.Text = txtFIRCAmount11_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr11_OB.Enabled = true;
            txtFIRCCrossCurrRate11_OB.Enabled = true;
        }
    }
    protected void txtFIRCAmount12_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency12_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr12_OB.Enabled = false;
            txtFIRCCrossCurrRate12_OB.Enabled = false;
            ddlFIRCRealisedCurr12_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate12_OB.Text = "";
            txtFIRCTobeAdjustedinSB12_OB.Text = txtFIRCAmount12_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr12_OB.Enabled = true;
            txtFIRCCrossCurrRate12_OB.Enabled = true;
            //txtFIRCTobeAdjustedinSB12_OB.Text = txtFIRCAmount12_OB.Text;
            TTCurrPairCalc(txtFIRCAmount12_OB.Text, ddlFIRCCurrency12_OB.SelectedValue, ddlFIRCRealisedCurr12_OB.SelectedValue, txtFIRCCrossCurrRate12_OB.Text, "F12");
        }
    }

    protected void txtFIRCCrossCurrRate12_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr12_OB.SelectedValue == "0" && txtFIRCCrossCurrRate12_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr12');", true);
            txtFIRCCrossCurrRate12_OB.Text = "";
            ddlFIRCRealisedCurr12_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount12_OB.Text, ddlFIRCCurrency12_OB.SelectedValue, ddlFIRCRealisedCurr12_OB.SelectedValue, txtFIRCCrossCurrRate12_OB.Text, "F12");
        }
    }

    protected void ddlFIRCCurrency12_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency12_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr12_OB.Enabled = false;
            txtFIRCCrossCurrRate12_OB.Enabled = false;
            ddlFIRCRealisedCurr12_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate12_OB.Text = "";
            txtFIRCTobeAdjustedinSB12_OB.Text = txtFIRCAmount12_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr12_OB.Enabled = true;
            txtFIRCCrossCurrRate12_OB.Enabled = true;
        }
    }
    protected void txtFIRCAmount13_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency13_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr13_OB.Enabled = false;
            txtFIRCCrossCurrRate13_OB.Enabled = false;
            ddlFIRCRealisedCurr13_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate13_OB.Text = "";
            txtFIRCTobeAdjustedinSB13_OB.Text = txtFIRCAmount13_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr13_OB.Enabled = true;
            txtFIRCCrossCurrRate13_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB13_OB.Text = txtFIRCAmount13_OB.Text;
            TTCurrPairCalc(txtFIRCAmount13_OB.Text, ddlFIRCCurrency13_OB.SelectedValue, ddlFIRCRealisedCurr13_OB.SelectedValue, txtFIRCCrossCurrRate13_OB.Text, "F13");
        }
    }


    protected void txtFIRCCrossCurrRate13_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr13_OB.SelectedValue == "0" && txtFIRCCrossCurrRate13_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr13');", true);
            txtFIRCCrossCurrRate13_OB.Text = "";
            ddlFIRCRealisedCurr13_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount13_OB.Text, ddlFIRCCurrency13_OB.SelectedValue, ddlFIRCRealisedCurr13_OB.SelectedValue, txtFIRCCrossCurrRate13_OB.Text, "F13");
        }
    }

    protected void ddlFIRCCurrency13_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency13_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr13_OB.Enabled = false;
            txtFIRCCrossCurrRate13_OB.Enabled = false;
            ddlFIRCRealisedCurr13_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate13_OB.Text = "";
            txtFIRCTobeAdjustedinSB13_OB.Text = txtFIRCAmount13_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr13_OB.Enabled = true;
            txtFIRCCrossCurrRate13_OB.Enabled = true;
        }
    }
    protected void txtFIRCAmount14_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency14_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr14_OB.Enabled = false;
            txtFIRCCrossCurrRate14_OB.Enabled = false;
            ddlFIRCRealisedCurr14_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate14_OB.Text = "";
            txtFIRCTobeAdjustedinSB14_OB.Text = txtFIRCAmount14_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr14_OB.Enabled = true;
            txtFIRCCrossCurrRate14_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB14_OB.Text = txtFIRCAmount14_OB.Text;
            TTCurrPairCalc(txtFIRCAmount14_OB.Text, ddlFIRCCurrency14_OB.SelectedValue, ddlFIRCRealisedCurr14_OB.SelectedValue, txtFIRCCrossCurrRate14_OB.Text, "F14");
        }
    }

    protected void txtFIRCCrossCurrRate14_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr14_OB.SelectedValue == "0" && txtFIRCCrossCurrRate14_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr14');", true);
            txtFIRCCrossCurrRate14_OB.Text = "";
            ddlFIRCRealisedCurr14_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount14_OB.Text, ddlFIRCCurrency14_OB.SelectedValue, ddlFIRCRealisedCurr14_OB.SelectedValue, txtFIRCCrossCurrRate14_OB.Text, "F14");
        }
    }
    protected void ddlFIRCCurrency14_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency14_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr14_OB.Enabled = false;
            txtFIRCCrossCurrRate14_OB.Enabled = false;
            ddlFIRCRealisedCurr14_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate14_OB.Text = "";
            txtFIRCTobeAdjustedinSB14_OB.Text = txtFIRCAmount14_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr14_OB.Enabled = true;
            txtFIRCCrossCurrRate14_OB.Enabled = true;
        }
    }

    protected void txtFIRCAmount15_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency15_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr15_OB.Enabled = false;
            txtFIRCCrossCurrRate15_OB.Enabled = false;
            ddlFIRCRealisedCurr15_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate15_OB.Text = "";
            txtFIRCTobeAdjustedinSB15_OB.Text = txtFIRCAmount15_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr15_OB.Enabled = true;
            txtFIRCCrossCurrRate15_OB.Enabled = true;
            // txtFIRCTobeAdjustedinSB15_OB.Text = txtFIRCAmount15_OB.Text;
            TTCurrPairCalc(txtFIRCAmount15_OB.Text, ddlFIRCCurrency15_OB.SelectedValue, ddlFIRCRealisedCurr15_OB.SelectedValue, txtFIRCCrossCurrRate15_OB.Text, "F15");
        }
    }


    protected void txtFIRCCrossCurrRate15_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlFIRCRealisedCurr15_OB.SelectedValue == "0" && txtFIRCCrossCurrRate15_OB.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select FIRCRealisedCurr15');", true);
            txtFIRCCrossCurrRate15_OB.Text = "";
            ddlFIRCRealisedCurr15_OB.Focus();
        }
        else
        {
            TTCurrPairCalc(txtFIRCAmount15_OB.Text, ddlFIRCCurrency15_OB.SelectedValue, ddlFIRCRealisedCurr15_OB.SelectedValue, txtFIRCCrossCurrRate15_OB.Text, "F15");
        }
    }
    protected void ddlFIRCCurrency15_OB_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == ddlFIRCCurrency15_OB.SelectedValue)
        {
            ddlFIRCRealisedCurr15_OB.Enabled = false;
            txtFIRCCrossCurrRate15_OB.Enabled = false;
            ddlFIRCRealisedCurr15_OB.SelectedValue = "0";
            txtFIRCCrossCurrRate15_OB.Text = "";
            txtFIRCTobeAdjustedinSB15_OB.Text = txtFIRCAmount15_OB.Text;
        }
        else
        {
            ddlFIRCRealisedCurr15_OB.Enabled = true;
            txtFIRCCrossCurrRate15_OB.Enabled = true;
        }
    }
    //---------------------------------Anand03-07-2023--------------------------
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




        if (txtFIRCTobeAdjustedinSB1_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB1_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB2_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB2_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB3_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB3_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB4_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB4_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB5_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB5_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB6_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB6_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB7_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB7_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB8_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB8_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB9_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB9_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB10_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB10_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB11_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB11_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB12_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB12_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB13_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB13_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB14_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB14_OB.Text = "0";
        }
        if (txtFIRCTobeAdjustedinSB15_OB.Text == "")
        {
            txtFIRCTobeAdjustedinSB15_OB.Text = "0";
        }

        //   float TotalTTAmt = float.Parse(txtTTAmtRealised1.Text) + float.Parse(txtTTAmtRealised2.Text) + float.Parse(txtTTAmtRealised3.Text) + float.Parse(txtTTAmtRealised4.Text) + float.Parse(txtTTAmtRealised5.Text) + float.Parse(txtTTAmtRealised6.Text) + float.Parse(txtTTAmtRealised7.Text) + float.Parse(txtTTAmtRealised8.Text) + float.Parse(txtTTAmtRealised9.Text) + float.Parse(txtTTAmtRealised10.Text) + float.Parse(txtTTAmtRealised11.Text) + float.Parse(txtTTAmtRealised12.Text) + float.Parse(txtTTAmtRealised13.Text) + float.Parse(txtTTAmtRealised14.Text) + float.Parse(txtTTAmtRealised15.Text);
        decimal TotalTTAmt = Convert.ToDecimal(txtTTAmtRealised1.Text) + Convert.ToDecimal(txtTTAmtRealised2.Text) + Convert.ToDecimal(txtTTAmtRealised3.Text) + Convert.ToDecimal(txtTTAmtRealised4.Text) + Convert.ToDecimal(txtTTAmtRealised5.Text) + Convert.ToDecimal(txtTTAmtRealised6.Text) + Convert.ToDecimal(txtTTAmtRealised7.Text) + Convert.ToDecimal(txtTTAmtRealised8.Text) + Convert.ToDecimal(txtTTAmtRealised9.Text) + Convert.ToDecimal(txtTTAmtRealised10.Text) + Convert.ToDecimal(txtTTAmtRealised11.Text) + Convert.ToDecimal(txtTTAmtRealised12.Text) + Convert.ToDecimal(txtTTAmtRealised13.Text) + Convert.ToDecimal(txtTTAmtRealised14.Text) + Convert.ToDecimal(txtTTAmtRealised15.Text);
        // float TotalFIRCAmt = float.Parse(txtFIRCTobeAdjustedinSB1_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB2_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB3_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB4_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB5_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB6_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB7_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB8_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB9_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB10_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB11_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB12_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB13_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB14_OB.Text) + float.Parse(txtFIRCTobeAdjustedinSB15_OB.Text);
        decimal TotalFIRCAmt = Convert.ToDecimal(txtFIRCTobeAdjustedinSB1_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB2_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB3_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB4_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB5_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB6_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB7_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB8_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB9_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB10_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB11_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB12_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB13_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB14_OB.Text) + Convert.ToDecimal(txtFIRCTobeAdjustedinSB15_OB.Text);
        decimal TotalTTFIRCAmt = TotalTTAmt + TotalFIRCAmt;
        // float _txtBillAmount = float.Parse(txtBillAmount.Text);

        if (txtDocType.Text == "EB")
        {

            if ((TotalTTFIRCAmt) > Convert.ToDecimal(txtBillAmount.Text))
            {
                hdnTTFIRCTotalAmtCheck.Value = "Greater";
            }
            else
            {
                hdnTTFIRCTotalAmtCheck.Value = "";
            }
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


        ////////////////FIRC       ///////////////////////////////////////

        if (ddlFIRCRealisedCurr1_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate1_OB.Text == "" || txtFIRCCrossCurrRate1_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr2_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate2_OB.Text == "" || txtFIRCCrossCurrRate2_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr3_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate3_OB.Text == "" || txtFIRCCrossCurrRate3_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr4_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate4_OB.Text == "" || txtFIRCCrossCurrRate4_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr5_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate5_OB.Text == "" || txtFIRCCrossCurrRate5_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr6_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate6_OB.Text == "" || txtFIRCCrossCurrRate6_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr7_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate7_OB.Text == "" || txtFIRCCrossCurrRate7_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr8_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate8_OB.Text == "" || txtFIRCCrossCurrRate8_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr9_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate9_OB.Text == "" || txtFIRCCrossCurrRate9_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr10_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate10_OB.Text == "" || txtFIRCCrossCurrRate10_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr11_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate11_OB.Text == "" || txtFIRCCrossCurrRate11_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr12_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate12_OB.Text == "" || txtFIRCCrossCurrRate12_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr13_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate13_OB.Text == "" || txtFIRCCrossCurrRate13_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr14_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate14_OB.Text == "" || txtFIRCCrossCurrRate14_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }

        if (ddlFIRCRealisedCurr15_OB.SelectedValue != "0" && (txtFIRCCrossCurrRate15_OB.Text == "" || txtFIRCCrossCurrRate15_OB.Text == "0"))
        {
            hdnTTCurrCheck.Value = "FIRCfalse";
        }
        if (hdnTTCurrCheck.Value == "TTfalse" || hdnTTCurrCheck.Value == "FIRCfalse")
        {
            hdnTTCurrCheck.Value = hdnTTCurrCheck.Value;
        }
        else
        {
            hdnTTCurrCheck.Value = "";
        }
    }

    protected void btn_Trans_FAdd_Click(object sender, EventArgs e)
    {
        FIRC5Row.Visible = true;
        Cpe3.ExpandedSize = 238;
        btn_Trans_FAdd.Visible = false;
    }

    protected void btn_Trans_FAdd1_Click(object sender, EventArgs e)
    {
        FIRC8Row.Visible = true;
        Cpe3.ExpandedSize = 330;
        btn_Trans_FAdd1.Visible = false;
        btn_Trans_FRemove1.Visible = false;
    }
    protected void btn3_Trans_FRemove1_Click(object sender, EventArgs e)
    {
        FIRC5Row.Visible = false;
        Cpe3.ExpandedSize = 160;
        btn_Trans_FAdd.Visible = true;
    }

    protected void btn_Trans_FAdd2_Click(object sender, EventArgs e)
    {
        FIRC11Row.Visible = true;
        Cpe3.ExpandedSize = 445;
        btn_Trans_FAdd2.Visible = false;
        btn_Trans_FRemove2.Visible = false;
    }

    protected void btn_Trans_FRemove2_Click(object sender, EventArgs e)
    {
        FIRC8Row.Visible = false;
        Cpe3.ExpandedSize = 238;
        btn_Trans_FAdd1.Visible = true;
        btn_Trans_FRemove1.Visible = true;
    }
    protected void btn_Trans_FRemove3_Click(object sender, EventArgs e)
    {
        FIRC11Row.Visible = false;
        Cpe3.ExpandedSize = 330;
        btn_Trans_FAdd2.Visible = true;
        btn_Trans_FRemove2.Visible = true;
    }

    //----------------------------------------------------------------Anand 03-08-2023----------------------------------------
    //protected void fillTTCurrency1()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetCurrencyList";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlTTCurrency1.Items.Clear();

    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "---Select---";
    //        ddlTTCurrency1.DataSource = dt.DefaultView;
    //        ddlTTCurrency1.DataTextField = "C_Code";
    //        ddlTTCurrency1.DataValueField = "C_Code";
    //        ddlTTCurrency1.DataBind();

    //    }
    //    else
    //        li.Text = "No record(s) found";

    //    ddlTTCurrency1.Items.Insert(0, li);

    //}
    //-------------------------------ADDED BY NILESH 04-08-2023------------------------------------
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
            String OverBAdd = dt.Rows[0]["Party_Address"].ToString().Trim();
            lblConsigneePartyDesc.ToolTip = "Name: " + lblConsigneePartyDesc.Text + "  Address: " + OverBAdd;
            if (lblConsigneePartyDesc.Text.Length > 20)
            {
                lblConsigneePartyDesc.Text = lblConsigneePartyDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtconsigneePartyID.Text = "";
            lblConsigneePartyDesc.Text = "";
        }

    }
    protected void btnConsigneesParty_Click(object sender, EventArgs e)
    {
        if (hdnConsigneePartyId.Value != "")
        {
            txtconsigneePartyID.Text = hdnConsigneePartyId.Value;
            fillConsigneePartyDescription();
            txtconsigneePartyID.Focus();
        }
    }

    public void HolidayMaster()
    {
        if (ddlCurrency.SelectedValue == "INR")
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            Boolean flag = false;
            string _DateDetails = "";
            if (txtDueDate.Text.Trim() != "")
            {
                TF_DATA objData = new TF_DATA();
                string _script = "";
                string DueDate = txtDueDate.Text.Trim();
                SqlParameter p1 = new SqlParameter("@MaturityDate", SqlDbType.VarChar);
                p1.Value = txtDueDate.Text.Trim();
                SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchName.Value);
                SqlParameter p3 = new SqlParameter("@curr", ddlCurrency.SelectedValue);

                string _query = "TF_CheckSpecialDates";
                DataTable dt = objData.getData(_query, p1, p2, p3);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["toolTip"].ToString() != "")
                    {
                        flag = true;
                        _DateDetails = dt.Rows[0]["toolTip"].ToString();
                        txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();
                    }
                }

                if (flag == true)
                {
                    _script = "alert('" + DueDate + " is " + _DateDetails + ". (Due Date) ');";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                }
            }
        }
        else
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            Boolean flag = false;
            string _DateDetails = "";
            if (txtDueDate.Text.Trim() != "")
            {
                TF_DATA objData = new TF_DATA();
                string _script = "";
                string DueDate = txtDueDate.Text.Trim();
                SqlParameter p1 = new SqlParameter("@MaturityDate", SqlDbType.VarChar);
                p1.Value = txtDueDate.Text.Trim();
                //SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchName.Value);
                SqlParameter p2 = new SqlParameter("@curr", ddlCurrency.SelectedValue);

                string _query = "TF_CheckSpecialDatesForgin";
                DataTable dt = objData.getData(_query, p1, p2);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["toolTip"].ToString() != "")
                    {
                        flag = true;
                        _DateDetails = dt.Rows[0]["toolTip"].ToString();
                        txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();
                    }
                }

                if (flag == true)
                {
                    _script = "alert('" + DueDate + " is " + _DateDetails + ". (Due Date) ');";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", _script, true);
                }
            }
        }
    }
    protected void txtDueDate_TextChanged(object sender, EventArgs e)
    {

        HolidayMaster();
    }

    protected void txtNoOfDays_TextChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue != "0")
        {
            if (rbtnUsanceBill.Checked == true)
            {
                if (txtAWBDate.Text != "" && rbtnFromAWB.Checked == true)
                {
                    DateTime AWBdate = DateTime.ParseExact(txtAWBDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int noofdays = Convert.ToInt32(txtNoOfDays.Text); // Assuming noofdays represents the number of months
                    DateTime AWBdate1 = AWBdate.AddDays(noofdays);
                    txtDueDate.Text = AWBdate1.ToString("dd/MM/yyyy");
                    HolidayMaster();

                }
                else if (txtAWBDate.Text != "" && rbtnAfterAWB.Checked == true)
                {
                    DateTime AWBdate = DateTime.ParseExact(txtAWBDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int noofdays = Convert.ToInt32(txtNoOfDays.Text) + 1; // Assuming noofdays represents the number of months
                    DateTime AWBdate1 = AWBdate.AddDays(noofdays);
                    txtDueDate.Text = AWBdate1.ToString("dd/MM/yyyy");
                    HolidayMaster();
                }
                else if (txtInvoiceDate.Text != "" && rbtnFromInvoice.Checked == true)
                {
                    DateTime AWBdate = DateTime.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int noofdays = Convert.ToInt32(txtNoOfDays.Text); // Assuming noofdays represents the number of months
                    DateTime AWBdate1 = AWBdate.AddDays(noofdays);
                    txtDueDate.Text = AWBdate1.ToString("dd/MM/yyyy");
                    HolidayMaster();
                }
                else if (txtBEDate.Text != "" && rbtnDA.Checked == true)
                {
                    DateTime AWBdate = DateTime.ParseExact(txtBEDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int noofdays = Convert.ToInt32(txtNoOfDays.Text); // Assuming noofdays represents the number of months
                    DateTime AWBdate1 = AWBdate.AddDays(noofdays);
                    txtDueDate.Text = AWBdate1.ToString("dd/MM/yyyy");
                    HolidayMaster();
                }
                else if (rbtnSight.Checked == true)
                {
                    DateTime Date = System.DateTime.Now;
                    Int64 noofdays = Convert.ToInt64(txtNoOfDays.Text);
                    DateTime date1 = Date.AddDays(noofdays);
                    txtDueDate.Text = date1.ToString("dd/MM/yyyy");
                    HolidayMaster();
                }
                else if (rbtnOthers.Checked == true)
                {
                    DateTime Date = System.DateTime.Now;
                    Int64 noofdays = Convert.ToInt64(txtNoOfDays.Text);
                    DateTime date1 = Date.AddDays(noofdays);
                    txtDueDate.Text = date1.ToString("dd/MM/yyyy");
                    HolidayMaster();
                }
                else
                {
                    txtDueDate.Text = "";
                }
            }
            else
            {
                DateTime Date = System.DateTime.Now;
                Int64 noofdays = Convert.ToInt64(txtNoOfDays.Text);
                DateTime date1 = Date.AddDays(noofdays);
                txtDueDate.Text = date1.ToString("dd/MM/yyyy");
                HolidayMaster();
            }
        }
    }
    //---------------------------------END------------------------------
    //-----------------Anand 12-09-2023-----------------------------
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

    protected void btnEFIRCCancel1_Click(object sender, EventArgs e)
    {
        txtFIRCNo1_OB.Text = "";
        txtFIRCDate1_OB.Text = "";
        ddlFIRCCurrency1_OB.SelectedValue = "0";
        txtFIRCAmount1_OB.Text = "";
        ddlFIRCRealisedCurr1_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate1_OB.Text = "";
        txtFIRCTobeAdjustedinSB1_OB.Text = "";
        txtFIRCADCode1_OB.Text = "";
    }

    protected void btnEFIRCCancel2_Click(object sender, EventArgs e)
    {
        txtFIRCNo2_OB.Text = "";
        txtFIRCDate2_OB.Text = "";
        ddlFIRCCurrency2_OB.SelectedValue = "0";
        txtFIRCAmount2_OB.Text = "";
        ddlFIRCRealisedCurr2_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate2_OB.Text = "";
        txtFIRCTobeAdjustedinSB2_OB.Text = "";
        txtFIRCADCode2_OB.Text = "";
    }
    protected void btnEFIRCCancel3_Click(object sender, EventArgs e)
    {
        txtFIRCNo3_OB.Text = "";
        txtFIRCDate3_OB.Text = "";
        ddlFIRCCurrency3_OB.SelectedValue = "0";
        txtFIRCAmount3_OB.Text = "";
        ddlFIRCRealisedCurr3_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate3_OB.Text = "";
        txtFIRCTobeAdjustedinSB3_OB.Text = "";
        txtFIRCADCode3_OB.Text = "";
    }
    protected void btnEFIRCCancel4_Click(object sender, EventArgs e)
    {
        txtFIRCNo4_OB.Text = "";
        txtFIRCDate4_OB.Text = "";
        ddlFIRCCurrency4_OB.SelectedValue = "0";
        txtFIRCAmount4_OB.Text = "";
        ddlFIRCRealisedCurr4_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate4_OB.Text = "";
        txtFIRCTobeAdjustedinSB4_OB.Text = "";
        txtFIRCADCode4_OB.Text = "";
    }
    protected void btnEFIRCCancel5_Click(object sender, EventArgs e)
    {
        txtFIRCNo5_OB.Text = "";
        txtFIRCDate5_OB.Text = "";
        ddlFIRCCurrency5_OB.SelectedValue = "0";
        txtFIRCAmount5_OB.Text = "";
        ddlFIRCRealisedCurr5_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate5_OB.Text = "";
        txtFIRCTobeAdjustedinSB5_OB.Text = "";
        txtFIRCADCode5_OB.Text = "";
    }
    protected void btnEFIRCCancel6_Click(object sender, EventArgs e)
    {
        txtFIRCNo6_OB.Text = "";
        txtFIRCDate6_OB.Text = "";
        ddlFIRCCurrency6_OB.SelectedValue = "0";
        txtFIRCAmount6_OB.Text = "";
        ddlFIRCRealisedCurr6_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate6_OB.Text = "";
        txtFIRCTobeAdjustedinSB6_OB.Text = "";
        txtFIRCADCode6_OB.Text = "";
    }
    protected void btnEFIRCCancel7_Click(object sender, EventArgs e)
    {
        txtFIRCNo7_OB.Text = "";
        txtFIRCDate7_OB.Text = "";
        ddlFIRCCurrency7_OB.SelectedValue = "0";
        txtFIRCAmount7_OB.Text = "";
        ddlFIRCRealisedCurr7_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate7_OB.Text = "";
        txtFIRCTobeAdjustedinSB7_OB.Text = "";
        txtFIRCADCode7_OB.Text = "";
    }
    protected void btnEFIRCCancel8_Click(object sender, EventArgs e)
    {
        txtFIRCNo8_OB.Text = "";
        txtFIRCDate8_OB.Text = "";
        ddlFIRCCurrency8_OB.SelectedValue = "0";
        txtFIRCAmount8_OB.Text = "";
        ddlFIRCRealisedCurr8_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate8_OB.Text = "";
        txtFIRCTobeAdjustedinSB8_OB.Text = "";
        txtFIRCADCode8_OB.Text = "";
    }
    protected void btnEFIRCCancel9_Click(object sender, EventArgs e)
    {
        txtFIRCNo9_OB.Text = "";
        txtFIRCDate9_OB.Text = "";
        ddlFIRCCurrency9_OB.SelectedValue = "0";
        txtFIRCAmount9_OB.Text = "";
        ddlFIRCRealisedCurr9_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate9_OB.Text = "";
        txtFIRCTobeAdjustedinSB9_OB.Text = "";
        txtFIRCADCode9_OB.Text = "";
    }
    protected void btnEFIRCCancel10_Click(object sender, EventArgs e)
    {
        txtFIRCNo10_OB.Text = "";
        txtFIRCDate10_OB.Text = "";
        ddlFIRCCurrency10_OB.SelectedValue = "0";
        txtFIRCAmount10_OB.Text = "";
        ddlFIRCRealisedCurr10_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate10_OB.Text = "";
        txtFIRCTobeAdjustedinSB10_OB.Text = "";
        txtFIRCADCode10_OB.Text = "";
    }
    protected void btnEFIRCCancel11_Click(object sender, EventArgs e)
    {
        txtFIRCNo11_OB.Text = "";
        txtFIRCDate11_OB.Text = "";
        ddlFIRCCurrency11_OB.SelectedValue = "0";
        txtFIRCAmount11_OB.Text = "";
        ddlFIRCRealisedCurr11_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate11_OB.Text = "";
        txtFIRCTobeAdjustedinSB11_OB.Text = "";
        txtFIRCADCode11_OB.Text = "";
    }
    protected void btnEFIRCCancel12_Click(object sender, EventArgs e)
    {
        txtFIRCNo12_OB.Text = "";
        txtFIRCDate12_OB.Text = "";
        ddlFIRCCurrency12_OB.SelectedValue = "0";
        txtFIRCAmount12_OB.Text = "";
        ddlFIRCRealisedCurr12_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate12_OB.Text = "";
        txtFIRCTobeAdjustedinSB12_OB.Text = "";
        txtFIRCADCode12_OB.Text = "";
    }
    protected void btnEFIRCCancel13_Click(object sender, EventArgs e)
    {
        txtFIRCNo13_OB.Text = "";
        txtFIRCDate13_OB.Text = "";
        ddlFIRCCurrency13_OB.SelectedValue = "0";
        txtFIRCAmount13_OB.Text = "";
        ddlFIRCRealisedCurr13_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate13_OB.Text = "";
        txtFIRCTobeAdjustedinSB13_OB.Text = "";
        txtFIRCADCode13_OB.Text = "";
    }
    protected void btnEFIRCCancel14_Click(object sender, EventArgs e)
    {
        txtFIRCNo14_OB.Text = "";
        txtFIRCDate14_OB.Text = "";
        ddlFIRCCurrency14_OB.SelectedValue = "0";
        txtFIRCAmount14_OB.Text = "";
        ddlFIRCRealisedCurr14_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate14_OB.Text = "";
        txtFIRCTobeAdjustedinSB14_OB.Text = "";
        txtFIRCADCode14_OB.Text = "";
    }
    protected void btnEFIRCCancel15_Click(object sender, EventArgs e)
    {
        txtFIRCNo15_OB.Text = "";
        txtFIRCDate15_OB.Text = "";
        ddlFIRCCurrency15_OB.SelectedValue = "0";
        txtFIRCAmount15_OB.Text = "";
        ddlFIRCRealisedCurr15_OB.SelectedValue = "0";
        txtFIRCCrossCurrRate15_OB.Text = "";
        txtFIRCTobeAdjustedinSB15_OB.Text = "";
        txtFIRCADCode15_OB.Text = "";
    }
    //--------------------------------End----------------------------------- 

    protected void CheckerViewOnly()
    {
        txtCustAcNo.Enabled = btnCustomerList.Enabled = txtOverseasPartyID.Enabled = btnOverseasPartyList.Enabled = txtENCdate.Enabled = Button9.Enabled = txtconsigneePartyID.Enabled = btnConsigneePartyList.Enabled =
        txtDateNegotiated.Enabled = Button10.Enabled = txtOverseasBankID.Enabled = btnOverseasBankList.Enabled = rdbOurADCode.Enabled = rdbotherAdcode.Enabled = txtADCode.Enabled = txtLCNo.Enabled =
        txtLCNoDate.Enabled = btnDTDraftDoc.Enabled = txtLCNoIssuedBy.Enabled = txtBENo.Enabled = txtBEDate.Enabled = Button2.Enabled = chkBankLineTransfer.Enabled = txtAccpDate.Enabled = Button3.Enabled =
        txtBENoDoc1.Enabled = txtInvoiceNo.Enabled = txtInvoiceDate.Enabled = btnDTInvoice.Enabled = ddlDispachInd.Enabled = ddlDispBydefault.Enabled = txtInvoiceDoc.Enabled = txtInvoiceDoc1.Enabled =
        txtAWBno.Enabled = txtAWBDate.Enabled = btnDTAWB.Enabled = txtAwbIssuedBy.Enabled = txtAWBDoc.Enabled = txtAWBDoc1.Enabled = txtPackingList.Enabled = txtPackingListDate.Enabled = btnDTPackingList.Enabled =
        txtPackingDoc.Enabled = txtPackingDoc1.Enabled = txtCertOfOrigin.Enabled = txtCertIssuedBy.Enabled = txtCertOfOriginDoc.Enabled = txtCertOfOriginDoc1.Enabled = txtCustomsInvoice.Enabled = txtCustomsDate.Enabled =
        Button4.Enabled = txtCommodityID.Enabled = btnCommodityList.Enabled = txtCustomsDoc.Enabled = txtCustomsDoc1.Enabled = txtGBaseCommodityID.Enabled = btnGBaseCommodityList.Enabled = txtInsPolicy.Enabled =
        txtInsPolicyDate.Enabled = btnDTInsPolicy.Enabled = txtInsPolicyIssuedBy.Enabled = txtInsPolicyDoc.Enabled = txtInsPolicyDoc1.Enabled = txtGSPDate.Enabled = Button5.Enabled = txtContractNo.Enabled =
        txtRate.Enabled = txtGSPDoc.Enabled = txtFIRCno.Enabled = txtFIRCdate.Enabled = Button6.Enabled = txtFIRCnoIssuedBy.Enabled = txtFIRCdoc.Enabled = txtINSP.Enabled = txtINSP1.Enabled = txtWM.Enabled =
        txtWM1.Enabled = txtOther.Enabled = txtOther1.Enabled = ddlMercTrade.Enabled = txtMiscellaneous.Enabled = rdbByAir.Enabled = rdbBySea.Enabled = rdbByRoad.Enabled = txtMiscDoc.Enabled = txtCoveringTo.Enabled =
        txtCountry.Enabled = btnCountryList.Enabled = txtReimbValDate.Enabled = Button7.Enabled = txtReimbClaimDate.Enabled = Button8.Enabled = txtBkName.Enabled = txtBIC.Enabled = txtReimbAmt.Enabled =
        txtNotes.Enabled = ddlCurrency.Enabled = chkLoanAdv.Enabled = ddlOtherCurrency.Enabled = txtExchRate.Enabled = txtNoOfDays.Enabled = rbtnEBR.Enabled = rbtnAfterAWB.Enabled = rbtnFromAWB.Enabled =
        rbtnSight.Enabled = rbtnDA.Enabled = rbtnFromInvoice.Enabled = rbtnOthers.Enabled = txtOtherTenorRemarks.Enabled = txtLibor.Enabled = txtOutOfDays.Enabled = txtDueDate.Enabled = txtIntRate1.Enabled =
        txtIntFrmDate1.Enabled = txtIntToDate1.Enabled = txtForDays1.Enabled = txtIntRate2.Enabled = txtIntFrmDate2.Enabled = txtIntToDate2.Enabled = txtForDays2.Enabled = txtIntRate3.Enabled = txtIntFrmDate3.Enabled =
        txtIntToDate3.Enabled = txtForDays3.Enabled = txtIntRate4.Enabled = txtIntFrmDate4.Enabled = txtIntToDate4.Enabled = txtForDays4.Enabled = txtIntRate5.Enabled = txtIntFrmDate5.Enabled = txtIntToDate5.Enabled =
        txtForDays5.Enabled = txtIntRate6.Enabled = txtIntFrmDate6.Enabled = txtIntToDate6.Enabled = txtForDays6.Enabled = txtBillAmount.Enabled = txtBillAmountinRS.Enabled = txtNegotiatedAmt.Enabled = txtNegotiatedAmtinRS.Enabled =
        txtInterest.Enabled = txtInterestinRS.Enabled = txtNetAmt.Enabled = txtNetAmtinRS.Enabled = txtExchRtEBR.Enabled = txt_fbkcharges.Enabled = txt_fbkchargesinRS.Enabled = txtOtherChrgs.Enabled = txtBankCert.Enabled =
        txtNegotiationFees.Enabled = txtCourierChrgs.Enabled = txtMarginAmt.Enabled = txtCommissionID.Enabled = txtCommission.Enabled = txtAcceptedDueDate.Enabled = ddlServiceTax.Enabled = txtSTaxAmount.Enabled = txtsbcess.Enabled =
        txtSBcesssamt.Enabled = txt_kkcessper.Enabled = txt_kkcessamt.Enabled = txtsttamt.Enabled = chkApplicable.Enabled = txtSTFXDLS.Enabled = txtsbfx.Enabled = txt_kkcessonfx.Enabled = txttotsbcess.Enabled =
        ddlServiceTaxfbk.Enabled = txtSTaxAmountfbk.Enabled = txtsbcessfbk.Enabled = txtSBcesssamtfbk.Enabled = txt_kkcessperfbk.Enabled = txt_kkcessamtfbk.Enabled = txtsttamtfbk.Enabled = txtCurrentAcinRS.Enabled =
        chkRBI.Enabled = btnCommissionList.Enabled = false;
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

        txtFIRCNo1_OB.Enabled = txtFIRCDate1_OB.Enabled = ddlFIRCCurrency1_OB.Enabled = txtFIRCAmount1_OB.Enabled = ddlFIRCRealisedCurr1_OB.Enabled = txtFIRCCrossCurrRate1_OB.Enabled = txtFIRCTobeAdjustedinSB1_OB.Enabled = txtFIRCADCode1_OB.Enabled = false;
        txtFIRCNo2_OB.Enabled = txtFIRCDate2_OB.Enabled = ddlFIRCCurrency2_OB.Enabled = txtFIRCAmount2_OB.Enabled = ddlFIRCRealisedCurr2_OB.Enabled = txtFIRCCrossCurrRate2_OB.Enabled = txtFIRCTobeAdjustedinSB2_OB.Enabled = txtFIRCADCode2_OB.Enabled = false;
        txtFIRCNo3_OB.Enabled = txtFIRCDate3_OB.Enabled = ddlFIRCCurrency3_OB.Enabled = txtFIRCAmount3_OB.Enabled = ddlFIRCRealisedCurr3_OB.Enabled = txtFIRCCrossCurrRate3_OB.Enabled = txtFIRCTobeAdjustedinSB3_OB.Enabled = txtFIRCADCode3_OB.Enabled = false;
        txtFIRCNo4_OB.Enabled = txtFIRCDate4_OB.Enabled = ddlFIRCCurrency4_OB.Enabled = txtFIRCAmount4_OB.Enabled = ddlFIRCRealisedCurr4_OB.Enabled = txtFIRCCrossCurrRate4_OB.Enabled = txtFIRCTobeAdjustedinSB4_OB.Enabled = txtFIRCADCode4_OB.Enabled = false;
        txtFIRCNo5_OB.Enabled = txtFIRCDate5_OB.Enabled = ddlFIRCCurrency5_OB.Enabled = txtFIRCAmount5_OB.Enabled = ddlFIRCRealisedCurr5_OB.Enabled = txtFIRCCrossCurrRate5_OB.Enabled = txtFIRCTobeAdjustedinSB5_OB.Enabled = txtFIRCADCode5_OB.Enabled = false;
        txtFIRCNo6_OB.Enabled = txtFIRCDate6_OB.Enabled = ddlFIRCCurrency6_OB.Enabled = txtFIRCAmount6_OB.Enabled = ddlFIRCRealisedCurr6_OB.Enabled = txtFIRCCrossCurrRate6_OB.Enabled = txtFIRCTobeAdjustedinSB6_OB.Enabled = txtFIRCADCode6_OB.Enabled = false;
        txtFIRCNo7_OB.Enabled = txtFIRCDate7_OB.Enabled = ddlFIRCCurrency7_OB.Enabled = txtFIRCAmount7_OB.Enabled = ddlFIRCRealisedCurr7_OB.Enabled = txtFIRCCrossCurrRate7_OB.Enabled = txtFIRCTobeAdjustedinSB7_OB.Enabled = txtFIRCADCode7_OB.Enabled = false;
        txtFIRCNo8_OB.Enabled = txtFIRCDate8_OB.Enabled = ddlFIRCCurrency8_OB.Enabled = txtFIRCAmount8_OB.Enabled = ddlFIRCRealisedCurr8_OB.Enabled = txtFIRCCrossCurrRate8_OB.Enabled = txtFIRCTobeAdjustedinSB8_OB.Enabled = txtFIRCADCode8_OB.Enabled = false;
        txtFIRCNo9_OB.Enabled = txtFIRCDate9_OB.Enabled = ddlFIRCCurrency9_OB.Enabled = txtFIRCAmount9_OB.Enabled = ddlFIRCRealisedCurr9_OB.Enabled = txtFIRCCrossCurrRate9_OB.Enabled = txtFIRCTobeAdjustedinSB9_OB.Enabled = txtFIRCADCode9_OB.Enabled = false;
        txtFIRCNo10_OB.Enabled = txtFIRCDate10_OB.Enabled = ddlFIRCCurrency10_OB.Enabled = txtFIRCAmount10_OB.Enabled = ddlFIRCRealisedCurr10_OB.Enabled = txtFIRCCrossCurrRate10_OB.Enabled = txtFIRCTobeAdjustedinSB10_OB.Enabled = txtFIRCADCode10_OB.Enabled = false;
        txtFIRCNo11_OB.Enabled = txtFIRCDate11_OB.Enabled = ddlFIRCCurrency11_OB.Enabled = txtFIRCAmount11_OB.Enabled = ddlFIRCRealisedCurr11_OB.Enabled = txtFIRCCrossCurrRate11_OB.Enabled = txtFIRCTobeAdjustedinSB11_OB.Enabled = txtFIRCADCode11_OB.Enabled = false;
        txtFIRCNo12_OB.Enabled = txtFIRCDate12_OB.Enabled = ddlFIRCCurrency12_OB.Enabled = txtFIRCAmount12_OB.Enabled = ddlFIRCRealisedCurr12_OB.Enabled = txtFIRCCrossCurrRate12_OB.Enabled = txtFIRCTobeAdjustedinSB12_OB.Enabled = txtFIRCADCode12_OB.Enabled = false;
        txtFIRCNo13_OB.Enabled = txtFIRCDate13_OB.Enabled = ddlFIRCCurrency13_OB.Enabled = txtFIRCAmount13_OB.Enabled = ddlFIRCRealisedCurr13_OB.Enabled = txtFIRCCrossCurrRate13_OB.Enabled = txtFIRCTobeAdjustedinSB13_OB.Enabled = txtFIRCADCode13_OB.Enabled = false;
        txtFIRCNo14_OB.Enabled = txtFIRCDate14_OB.Enabled = ddlFIRCCurrency14_OB.Enabled = txtFIRCAmount14_OB.Enabled = ddlFIRCRealisedCurr14_OB.Enabled = txtFIRCCrossCurrRate14_OB.Enabled = txtFIRCTobeAdjustedinSB14_OB.Enabled = txtFIRCADCode14_OB.Enabled = false;
        txtFIRCNo15_OB.Enabled = txtFIRCDate15_OB.Enabled = ddlFIRCCurrency15_OB.Enabled = txtFIRCAmount15_OB.Enabled = ddlFIRCRealisedCurr15_OB.Enabled = txtFIRCCrossCurrRate15_OB.Enabled = txtFIRCTobeAdjustedinSB15_OB.Enabled = txtFIRCADCode15_OB.Enabled = false;
        btnEFIRCCancel1.Enabled = btnEFIRCCancel2.Enabled = btnEFIRCCancel3.Enabled = btnEFIRCCancel4.Enabled = btnEFIRCCancel5.Enabled = btnEFIRCCancel6.Enabled = btnEFIRCCancel7.Enabled = btnEFIRCCancel8.Enabled = btnEFIRCCancel9.Enabled = btnEFIRCCancel10.Enabled = btnEFIRCCancel11.Enabled = btnEFIRCCancel12.Enabled = btnEFIRCCancel13.Enabled = btnEFIRCCancel14.Enabled = btnEFIRCCancel15.Enabled = false;
        //////////////////////////////////  G base //////////////////////////////
        txtOperationType.Enabled = txtSettlementOption.Enabled = txtRiskCountry.Enabled = rdbShipper.Enabled = rdbBuyer.Enabled = txtFundType.Enabled = txtBaseRate.Enabled = txtGradeCode.Enabled = txtDirection.Enabled = txtCovrInstr.Enabled = txtInternalRate.Enabled =
        txtSpread.Enabled = txtApplNo.Enabled = ddlRemEUC.Enabled = txtDraftNo.Enabled = txtRiskCustomer.Enabled = txtVesselName.Enabled = ddlInstructions.Enabled = txtReimbBank.Enabled = btnReimbBank.Enabled = txtGBaseRemarks.Enabled =
        txtMerchandise.Enabled = txtPayingBankID.Enabled = btnPayingBankList.Enabled = txtSpecialInstructions1.Enabled = txtSpecialInstructions2.Enabled = txtSpecialInstructions3.Enabled = txtSpecialInstructions4.Enabled = txtSpecialInstructions5.Enabled =
        txtSpecialInstructions6.Enabled = txtSpecialInstructions7.Enabled = txtSpecialInstructions8.Enabled = txtSpecialInstructions9.Enabled = txtSpecialInstructions10.Enabled = txtPrincipalContractNo1.Enabled = txtPrincipalContractNo2.Enabled = txtPrincipalExchCurr.Enabled =
        txtPrincipalExchRate.Enabled = txtPrincipalIntExchRate.Enabled = txtInterestLump.Enabled = txtInterestContractNo1.Enabled = txtInterestContractNo2.Enabled = txtInterestExchCurr.Enabled = txtInterestExchRate.Enabled = txtInterestIntExchRate.Enabled =
        txtCommissionMatu.Enabled = txtCommissionContractNo1.Enabled = txtCommissionContractNo2.Enabled = txtCommissionExchCurr.Enabled = txtCommissionExchRate.Enabled = txtCommissionIntExchRate.Enabled = txtCRGLCode.Enabled = txtCRCustAbbr.Enabled = txtCRCustAcNo1.Enabled =
        txtCRCustAcNo2.Enabled = txtCRCurr.Enabled = txtCRAmount.Enabled = txtCRIntCurr.Enabled = txtCRIntAmount.Enabled = txtCRIntPayer.Enabled = txtCRPaymentCommCurr.Enabled = txtCRPaymentCommAmount.Enabled = txtCRPaymentCommPayer.Enabled = txtCRHandlingCommCurr.Enabled =
        txtCRHandlingCommAmount.Enabled = txtCRHandlingCommPayer.Enabled = txtCRPostageCurr.Enabled = txtCRPostageAmount.Enabled = txtCRPostagePayer.Enabled = txtCRCurr1.Enabled = txtCRAmount1.Enabled = txtCRPayer1.Enabled = txtDRCurr.Enabled = txtDRAmount.Enabled =
        txtDRGLCode1.Enabled = txtDRCustAbbr1.Enabled = txtDRCustAcNo11.Enabled = txtDRCustAcNo12.Enabled = txtDRCurr1.Enabled = txtDRAmount1.Enabled = txtDRGLCode2.Enabled = txtDRCustAbbr2.Enabled = txtDRCustAcNo21.Enabled = txtDRCustAcNo22.Enabled = txtDRCurr2.Enabled =
        txtDRAmount2.Enabled = txtDRGLCode3.Enabled = txtDRCustAbbr3.Enabled = txtDRCustAcNo31.Enabled = txtDRCustAcNo32.Enabled = txtDRCurr3.Enabled = txtDRAmount3.Enabled = txtDRGLCode4.Enabled = txtDRCustAbbr4.Enabled = txtDRCustAcNo41.Enabled = txtDRCustAcNo42.Enabled =
        txtDRCurr4.Enabled = txtDRAmount4.Enabled = txtDRGLCode5.Enabled = txtDRCustAbbr5.Enabled = txtDRCustAcNo51.Enabled = txtDRCustAcNo52.Enabled = txtDRCurr5.Enabled = txtDRAmount5.Enabled = false;
        chk1.Enabled = chk2.Enabled = chk3.Enabled = chk4.Enabled = chk5.Enabled = chk6.Enabled = chk7.Enabled = chk7A.Enabled = chk7B.Enabled = chk8.Enabled = chk9.Enabled = chk11.Enabled = chk12.Enabled = chk13.Enabled = txtRemark.Enabled = txtRemarks1.Enabled =
        chkManualGR.Enabled = txtNoOfSB.Enabled = chkSB.Enabled = txtShippingBillNo.Enabled = btn_shipbillnohelp.Enabled = txtShippingBillDate.Enabled = ddlPortCode.Enabled = ddlFormType.Enabled = txtGRPPCustomsNo.Enabled = ddlCurrencyGRPP.Enabled = txtAmountGRPP.Enabled =
        txt_invsrno.Enabled = txtInvoiceNum.Enabled = btn_invsrno.Enabled = txtInvoiceDt.Enabled = txtInvoiceAmt.Enabled = txtFreight.Enabled = txtInsurance.Enabled = txtDiscount.Enabled = txtCommissionGRPP.Enabled = txtOthDeduction.Enabled = txtPacking.Enabled = txt_status.Enabled = btnAddGRPPCustoms.Enabled =
        ddlFreightCurr.Enabled = ddlInsCurr.Enabled = ddlDiscCurr.Enabled = ddlCommCurr.Enabled = ddlOthDedCurr.Enabled = ddlPackChgCurr.Enabled = txtGRExchRate.Enabled = GridViewGRPPCustomsDetails.Enabled = false;
        GridViewGRPPCustomsDetails.Enabled = false;
    }

    private void getLastDocNo()
    {
        if (Request.QueryString["mode"].Trim() == "add")
        {
            string _prFx = Request.QueryString["DocPrFx"].Trim();
            string _srNo = Request.QueryString["DocSrNo"].Trim();
            string _branchCode = Request.QueryString["BranchCode"].Trim();
            string _year = Request.QueryString["DocYear"].Trim();

            string _foreignOrLocal = "";
            TF_DATA obj = new TF_DATA();
            SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
            pYear.Value = _year;
            SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
            pDocType.Value = _prFx;
            SqlParameter pBranchCode = new SqlParameter("@branchCode", SqlDbType.VarChar);
            pBranchCode.Value = _branchCode;


            if (Request.QueryString["ForeignOrLocal"].Trim() == "F")
            {
                _foreignOrLocal = "F";
            }
            else if (Request.QueryString["ForeignOrLocal"].Trim() != "F")
            {
                _foreignOrLocal = "L";
            }
            SqlParameter pFL = new SqlParameter("@foreignOrLocal", _foreignOrLocal);

            string _query1 = "TF_GetLastDocNo_EXP";
            string _lastDocNo = obj.SaveDeleteData(_query1, pYear, pDocType, pFL, pBranchCode);
            txtDocumentNo.Text = _prFx + "/" + _branchCode + "/" + _year + _lastDocNo;
        }
    }

    protected void rbtnAfterAWB_CheckedChanged(object sender, EventArgs e)
    {
        txtOtherTenorRemarks.Text = "Days After AWB/BL Date";
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Calculate();", true);
        txtNoOfDays_TextChanged(null, null);

    }
    protected void rbtnFromAWB_CheckedChanged(object sender, EventArgs e)
    {
        txtOtherTenorRemarks.Text = "Days From AWB/BL Date";
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Calculate();", true);
        txtNoOfDays_TextChanged(null, null);
    }
    protected void rbtnSight_CheckedChanged(object sender, EventArgs e)
    {
        txtOtherTenorRemarks.Text = "Days Sight";
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Calculate();", true);
        txtNoOfDays_TextChanged(null, null);
    }
    protected void rbtnDA_CheckedChanged(object sender, EventArgs e)
    {
        txtOtherTenorRemarks.Text = "From Draft/BOE Date";
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Calculate();", true);
        txtNoOfDays_TextChanged(null, null);
    }
    protected void rbtnFromInvoice_CheckedChanged(object sender, EventArgs e)
    {
        txtOtherTenorRemarks.Text = "Days From Invoice Date";
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Calculate();", true);
        txtNoOfDays_TextChanged(null, null);
    }
    protected void rbtnOthers_CheckedChanged(object sender, EventArgs e)
    {
        txtOtherTenorRemarks.Text = "";
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "Calculate();", true);
        txtNoOfDays_TextChanged(null, null);
    }

    //-----------------------------Anand 04-01-2023-----------------------------------------------------

    public void AuditDocument(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result, string _Status)
    {
        if (_result.Substring(0, 5) == "added")
        {
            string _branchCode = Request.QueryString["BranchCode"].Trim();

            TF_DATA objSave = new TF_DATA();
            _query = "TF_Data_AuditTrailDocument";
            _NewValues = "Customer A/C: " + txtCustAcNo.Text.Trim() + ";Overseas Party ID: " + txtOverseasPartyID.Text.Trim() + ";Consignee Party ID:" + txtconsigneePartyID.Text.Trim()
                + ";Overseas Bank ID :" + txtOverseasBankID.Text.Trim() + ";Date Received :" + txtDateRcvd.Text.Trim() + ";(DRAFT) B.E No. :" + txtBENo.Text.Trim() +
               "; AWB/BL No/LR :" + txtAWBno.Text.Trim() + ";AWB/BL No/LR Date :" + txtAWBDate.Text.Trim() +
                ";Checkar Status :" + _Status;


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
            string _prFx = Request.QueryString["DocPrFx"].Trim();
            string _srNo = Request.QueryString["DocSrNo"].Trim();
            //string _branchCode = Request.QueryString["BranchCode"].Trim();

            string _year = Request.QueryString["DocYear"].Trim();

            string Document_No = _result.Substring(5);
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = Document_No;
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Add");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "Document Details";
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
        }

        else
        {

            TF_DATA objSave = new TF_DATA();
            int isneedtolog = 0;
            string NewValues = "";
            _OldValues = "Customer A/C :" + hdnCustACNO.Value.ToString() + ";Overseas Party ID : " + hdnOverseasParty.Value.ToString() + ";Consignee Party ID : " + hdnConsigneeParty.Value.ToString() +
     ";Overseas Bank ID : " + hdnOverseasBankID.Value.ToString() + "; Date Received :" + hdnDateReceived.Value.ToString() + "; (DRAFT) B.E No. :" + hdnDRAFTBENo.Value.ToString()
      + "; AWB/BL No/LR :" + hdnAWBBLNoLR.Value.ToString() + "; AWB/BL No/LR Date :" + hdnAWBBLNoLRDate.Value.ToString()
     + ";Checkar Status:" + hdnCheckarStatus.Value.ToString() + "";

            if (hdnCustACNO.Value != txtCustAcNo.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Customer A/C : " + txtCustAcNo.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Customer A/C : " + txtCustAcNo.Text.Trim();
                }

            }


            if (hdnOverseasParty.Value != txtOverseasPartyID.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Overseas Party ID : " + txtOverseasPartyID.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Overseas Party ID : " + txtOverseasPartyID.Text.Trim();
                }

            }

            if (hdnConsigneeParty.Value != txtconsigneePartyID.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Consignee Party ID : " + txtconsigneePartyID.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Consignee Party ID : " + txtconsigneePartyID.Text.Trim();
                }

            }

            if (hdnOverseasBankID.Value != txtOverseasBankID.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Overseas Bank ID : " + txtOverseasBankID.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Overseas Bank ID : " + txtOverseasBankID.Text.Trim();
                }

            }
            if (hdnDateReceived.Value != txtDateRcvd.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Date Received : " + txtDateRcvd.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Date Received : " + txtDateRcvd.Text.Trim();
                }

            }

            if (hdnDRAFTBENo.Value != txtBENo.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "(DRAFT) B.E No : " + txtBENo.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; (DRAFT) B.E No : " + txtBENo.Text.Trim();
                }

            }

            if (hdnAWBBLNoLR.Value != txtAWBno.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "AWB/BL No/LR : " + txtAWBno.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; AWB/BL No/LR : " + txtAWBno.Text.Trim();
                }

            }

            if (hdnAWBBLNoLRDate.Value != txtAWBDate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "AWB/BL No/LR Date : " + txtAWBDate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; AWB/BL No/LR Date : " + txtAWBDate.Text.Trim();
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
            _query = "TF_Data_AuditTrailDocument";
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
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = txtDocumentNo.Text.Trim();
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Modify");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "Document Details";
            if (isneedtolog == 1)
            {
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
            }
        }
    }

    public void AuditTransaction(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result, string _Status)
    {
        if (_result.Substring(0, 5) == "added")
        {
            string _branchCode = Request.QueryString["BranchCode"].Trim();

            TF_DATA objSave = new TF_DATA();
            _query = "TF_Data_AuditTrailTransaction";
            _NewValues = "Currency : " + ddlCurrency.SelectedValue.Trim() + ";No of Days: " + txtNoOfDays.Text.Trim() + ";TenorRemarks:" + txtOtherTenorRemarks.Text.Trim()
                + ";Due Date :" + txtDueDate.Text.Trim() + ";Bill Amount :" + txtBillAmount.Text.Trim()
                + ";IRM Reference No1 :" + txtTTRefNo1.Text.Trim() + ";IRM amount Utilized1 :" + txtTTAmount1.Text.Trim()
                + ";IRM Reference No2 :" + txtTTRefNo2.Text.Trim() + ";IRM amount Utilized2 :" + txtTTAmount2.Text.Trim()
                + ";EFIRC No1 :" + txtFIRCNo1_OB.Text.Trim() + ";EFIRC Date1 :" + txtFIRCDate1_OB.Text.Trim() + ";EFIRC Amount1 :" + txtFIRCAmount1_OB.Text.Trim()
                + ";EFIRC No2 :" + txtFIRCNo2_OB.Text.Trim() + ";EFIRC Date2 :" + txtFIRCDate2_OB.Text.Trim() + ";EFIRC Amount2 :" + txtFIRCAmount2_OB.Text.Trim()
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
            string _prFx = Request.QueryString["DocPrFx"].Trim();
            string _srNo = Request.QueryString["DocSrNo"].Trim();
            //string _branchCode = Request.QueryString["BranchCode"].Trim();

            string _year = Request.QueryString["DocYear"].Trim();

            string Document_No = _result.Substring(5);
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = Document_No;
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Add");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "Transaction Details";
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
        }

        else
        {

            TF_DATA objSave = new TF_DATA();
            int isneedtolog = 0;
            string NewValues = "";
            _OldValues = "Currency :" + hdnCurr.Value.ToString() + ";No of Days : " + hdnNoofDays.Value.ToString() + ";TenorRemarks : " + hdnTenorRemarks.Value.ToString() +
                         ";Due Date : " + hdnDueDate.Value.ToString() + "; Bill Amount :" + hdnBillAmount.Value.ToString() +
                         ";IRM Reference No1 :" + hdnIRMReferenceNo1.Value.ToString() + ";IRM amount Utilized1 :" + hdnIRMamountUtilized1.Value.ToString() +
                         ";IRM Reference No2 :" + hdnIRMReferenceNo2.Value.ToString() + ";IRM amount Utilized2 :" + hdnIRMamountUtilized2.Value.ToString() +
                         ";EFIRC No1 :" + hdnEFIRCNo1.Value.ToString() + ";EFIRC Date1 :" + hdnEFIRCDate1.Value.ToString() + ";EFIRC Amount1 :" + hdnEFIRCAmount1.Value.ToString() +
                         ";EFIRC No2 :" + hdnEFIRCNo2.Value.ToString() + ";EFIRC Date2 :" + hdnEFIRCDate2.Value.ToString() + ";EFIRC Amount2 :" + hdnEFIRCAmount2.Value.ToString() +
                         ";Checkar Status:" + hdnCheckarStatus.Value.ToString() + "";

            if (hdnCurr.Value != ddlCurrency.SelectedValue.Trim())
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


            if (hdnNoofDays.Value != txtNoOfDays.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "No of Days : " + txtNoOfDays.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; No of Days : " + txtNoOfDays.Text.Trim();
                }

            }

            if (hdnTenorRemarks.Value != txtOtherTenorRemarks.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "TenorRemarks : " + txtOtherTenorRemarks.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; TenorRemarks : " + txtOtherTenorRemarks.Text.Trim();
                }

            }

            if (hdnDueDate.Value != txtDueDate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Due Date : " + txtDueDate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Due Date : " + txtDueDate.Text.Trim();
                }

            }
            if (hdnBillAmount.Value != txtBillAmount.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Bill Amount : " + txtBillAmount.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Bill Amount : " + txtBillAmount.Text.Trim();
                }

            }

            if (hdnIRMReferenceNo1.Value != txtTTRefNo1.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "IRM Reference No1 : " + txtTTRefNo1.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; IRM Reference No1 : " + txtTTRefNo1.Text.Trim();
                }

            }
            if (hdnIRMamountUtilized1.Value != txtTTAmount1.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "IRM amount Utilized1 : " + txtTTAmount1.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; IRM amount Utilized1 : " + txtTTAmount1.Text.Trim();
                }

            }
            if (hdnIRMReferenceNo2.Value != txtTTRefNo2.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "IRM Reference No2 : " + txtTTRefNo2.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; IRM Reference No2 : " + txtTTRefNo2.Text.Trim();
                }

            }
            if (hdnIRMamountUtilized2.Value != txtTTAmount2.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "IRM amount Utilized2 : " + txtTTAmount2.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; IRM amount Utilized2 : " + txtTTAmount2.Text.Trim();
                }

            }
            if (hdnEFIRCNo1.Value != txtFIRCNo1_OB.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "EFIRC No1 : " + txtFIRCNo1_OB.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; EFIRC No1 : " + txtFIRCNo1_OB.Text.Trim();
                }

            }
            if (hdnEFIRCDate1.Value != txtFIRCDate1_OB.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "EFIRC Date1 : " + txtFIRCDate1_OB.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; EFIRC Date1 : " + txtFIRCDate1_OB.Text.Trim();
                }

            }
            if (hdnEFIRCAmount1.Value != txtFIRCAmount1_OB.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "EFIRC Amount1 : " + txtFIRCAmount1_OB.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; EFIRC Amount1 : " + txtFIRCAmount1_OB.Text.Trim();
                }

            }
            if (hdnEFIRCNo2.Value != txtFIRCNo2_OB.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "EFIRC No2 : " + txtFIRCNo2_OB.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; EFIRC No2 : " + txtFIRCNo2_OB.Text.Trim();
                }

            }
            if (hdnEFIRCDate2.Value != txtFIRCDate2_OB.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "EFIRC Date2 : " + txtFIRCDate2_OB.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; EFIRC Date2 : " + txtFIRCDate2_OB.Text.Trim();
                }

            }
            if (hdnEFIRCAmount2.Value != txtFIRCAmount2_OB.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "EFIRC Amount2 : " + txtFIRCAmount2_OB.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; EFIRC Amount2 : " + txtFIRCAmount2_OB.Text.Trim();
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
            _query = "TF_Data_AuditTrailTransaction";
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
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = txtDocumentNo.Text.Trim();
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Modify");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "Transaction Details";
            if (isneedtolog == 1)
            {
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
            }
        }
    }

    public void AuditGBase(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result, string _Status)
    {
        if (_result.Substring(0, 5) == "added")
        {
            string _branchCode = Request.QueryString["BranchCode"].Trim();

            TF_DATA objSave = new TF_DATA();
            _query = "TF_Data_AuditTrailGBase";
            _NewValues = "Country Risk:" + txtRiskCountry.Text.Trim() + ";Reimbursing Bank :" + txtReimbBank.Text.Trim() +
                ";Checkar Status :" + _Status;


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
            string _prFx = Request.QueryString["DocPrFx"].Trim();
            string _srNo = Request.QueryString["DocSrNo"].Trim();
            string _year = Request.QueryString["DocYear"].Trim();

            string Document_No = _result.Substring(5);
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = Document_No;
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Add");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "G-Base Details";
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
        }

        else
        {

            TF_DATA objSave = new TF_DATA();
            int isneedtolog = 0;
            string NewValues = "";
            _OldValues = "Country Risk : " + hdnCountryRisk.Value.ToString() + "; Reimbursing Bank :" + hdnReimbursingBank.Value.ToString() +
     ";Checkar Status:" + hdnCheckarStatus.Value.ToString() + "";

            if (hdnCountryRisk.Value != txtRiskCountry.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Country Risk : " + txtRiskCountry.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Country Risk : " + txtRiskCountry.Text.Trim();
                }

            }
            if (hdnReimbursingBank.Value != txtReimbBank.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Reimbursing Bank : " + txtReimbBank.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Reimbursing Bank : " + txtReimbBank.Text.Trim();
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
            _query = "TF_Data_AuditTrailGBase";
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
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = txtDocumentNo.Text.Trim();
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Modify");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "G-Base Details";
            if (isneedtolog == 1)
            {
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
            }
        }
    }

    public void AuditGRPP(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result, string _Status)
    {
        if (_result.Substring(0, 5) == "added")
        {

            string _branchCode = Request.QueryString["BranchCode"].Trim();
            int rowIndex = 0;
            for (int i = 1; i <= GridViewGRPPCustomsDetails.Rows.Count; i++)
            {

                if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                {
                    Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCurrency");
                    Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblAmount");
                    Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillNo");
                    Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblShippingBillDate");
                    Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPortCode");

                    Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFormType");
                    Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGR");
                    Label lblinvsrno = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblinvsrno");
                    Label lbl15 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceNo");
                    Label lbl16 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceDate");
                    Label lbl17 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInvoiceAmt");
                    Label lblFreightAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFreightAmt");
                    Label lblInsuranceAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsuranceAmt");
                    Label lblDiscountAmt = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDiscountAmt");
                    Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommission");
                    Label lblOthDeduction = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOthDeduction");
                    Label lblPacking = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPacking");
                    Label lblstatus = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblstatus");
                    Label lblFreightCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblFrieghtCurr");
                    Label lblInsuranceCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblInsCurr");
                    Label lblDiscountCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblDisCurr");
                    Label lblCommCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblCommCurr");
                    Label lblOthDeductionCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblOtherDedCurr");
                    Label lblPackingCurr = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblPackingChgCurr");
                    Label lblGRExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].FindControl("lblGRExchRate");

                    TF_DATA objSave = new TF_DATA();
                    _query = "TF_Data_AuditTrailGRPP";
                    _NewValues = "Shipping Bill No : " + lbl9.Text.Trim() + ";Shipping Bill Date: " + lbl10.Text.Trim() + ";Port Code:" + lbl12.Text.Trim()
                        + ";Currency :" + lbl5.Text.Trim() + ";Amount :" + lbl6.Text.Trim() + ";Type :" + lbl2.Text.Trim() + ";GR/PP/Cust.No. :" + lbl4.Text.Trim() +
                        ";Invoice Sr No :" + lblinvsrno.Text.Trim() + ";Invoice No :" + lbl15.Text.Trim() + ";Invoice Date :" + lbl16.Text.Trim() + ";Invoice Amt :" + lbl17.Text.Trim() +
                        ";Freight Amt :" + lblFreightAmt.Text.Trim() + ";Insurance Amt :" + lblInsuranceAmt.Text.Trim() + ";Discount Amt :" + lblDiscountAmt.Text.Trim() +
                        ";Comm. Amt :" + lbl11.Text.Trim() +
                         ";Oth. Ded. Chrgs :" + lblOthDeduction.Text.Trim() + ";Packing Chrgs :" + lblPacking.Text.Trim() + ";Status :" + lblstatus.Text.Trim() +
                         ";Freight Curr :" + lblFreightCurr.Text.Trim() +
                          ";Ins. Curr :" + lblInsuranceCurr.Text.Trim() + ";Dis. Curr :" + lblDiscountCurr.Text.Trim() + ";Comm. Curr :" + lblCommCurr.Text.Trim() +
                          ";Oth. Ded. Curr :" + lblOthDeductionCurr.Text.Trim() +
                           ";Pack. Chgs. Curr :" + lblPackingCurr.Text.Trim() + ";Exch Rate :" + lblGRExchRate.Text.Trim() +

                        ";Checkar Status :" + _Status;


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
                    string Document_No = _result.Substring(5);
                    SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
                    DocumentNo.Value = Document_No;
                    SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
                    DocumnetDate.Value = txtDateRcvd.Text.Trim();
                    SqlParameter Mode = new SqlParameter("@Mode", "Add");
                    SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
                    user.Value = _userName;
                    string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                    moddate.Value = _moddate;
                    string _menu = "Export Bill Data Entry";
                    SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                    menu.Value = _menu;
                    SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
                    TabName.Value = "Covering Schedule/GR/PP Details";
                    string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
                    rowIndex++;
                }

            }
        }

        else
        {

            TF_DATA objSave = new TF_DATA();
            int isneedtolog = 0;
            string NewValues = "";
            _OldValues = "Shipping Bill No :" + hdShippingBillNo.Value.ToString() + ";Shipping Bill Date : " + hdnShippingBillDate.Value.ToString() +
                ";Port Code : " + hdnPortcode1.Value.ToString() + ";Currency : " + hdnCurrency.Value.ToString() + "; Amount :" + hdnAmount.Value.ToString() +
                ";Type : " + hdnType.Value.ToString() + ";GR/PP/Cust.No. : " + hdnGRPPCustNo.Value.ToString() + "; Invoice Sr No :" + hdnInvoiceSrNo.Value.ToString() +
                ";Invoice No : " + hdnInvoiceNo.Value.ToString() + ";Invoice Date : " + hdnInvoiceDate.Value.ToString() + "; Invoice Amt :" + hdnInvoiceAmt.Value.ToString() +
                ";Freight Amt : " + hdnFreightAmt.Value.ToString() + ";Insurance Amt : " + hdnInsuranceAmt.Value.ToString() + "; Discount Amt :" + hdnDiscountAmt.Value.ToString() +
                ";Comm. Amt : " + hdnCommAmt.Value.ToString() + ";Oth. Ded. Chrgs : " + hdnOthDedChrgs.Value.ToString() + "; Packing Chrgs :" + hdnPackingChrgs.Value.ToString() +
                ";Status : " + hdnStatus.Value.ToString() + ";Freight Curr : " + hdnFreightCurr.Value.ToString() + "; Ins. Curr :" + hdnInsCurr.Value.ToString() +
                ";Dis. Curr : " + hdnDisCurr.Value.ToString() + ";Comm. Curr : " + hdnCommCurr.Value.ToString() + "; Oth. Ded. Curr :" + hdnOthDedCurr.Value.ToString() +
                ";Pack. Chgs. Curr : " + hdnPackChgsCurr.Value.ToString() + ";Exch Rate : " + hdnExchRate.Value.ToString() +
                ";Checkar Status:" + hdnCheckarStatus.Value.ToString() + "";

            if (hdShippingBillNo.Value != txtShippingBillNo.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Shipping Bill No : " + txtShippingBillNo.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Shipping Bill No : " + txtShippingBillNo.Text.Trim();
                }

            }


            if (hdnShippingBillDate.Value != txtShippingBillDate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Shipping Bill Date : " + txtShippingBillDate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Shipping Bill Date : " + txtShippingBillDate.Text.Trim();
                }

            }

            if (hdnPortcode1.Value != ddlPortCode.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Port Code : " + ddlPortCode.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Port Code : " + ddlPortCode.SelectedValue.Trim();
                }

            }

            if (hdnCurrency.Value != ddlCurrencyGRPP.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Currency : " + ddlCurrencyGRPP.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Currency : " + ddlCurrencyGRPP.SelectedValue.Trim();
                }

            }
            if (hdnAmount.Value != txtAmountGRPP.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Amount : " + txtAmountGRPP.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Amount : " + txtAmountGRPP.Text.Trim();
                }

            }
            //-----------------------------------------------------------------------------------------------------
            if (hdnType.Value != ddlFormType.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Type : " + ddlFormType.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Type : " + ddlFormType.SelectedValue.Trim();
                }

            }
            if (hdnGRPPCustNo.Value != txtGRPPCustomsNo.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "GR/PP/Cust.No. : " + txtGRPPCustomsNo.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; GR/PP/Cust.No. : " + txtGRPPCustomsNo.Text.Trim();
                }

            }
            if (hdnInvoiceSrNo.Value != txt_invsrno.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Invoice Sr No : " + txt_invsrno.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Invoice Sr No : " + txt_invsrno.Text.Trim();
                }

            }
            if (hdnInvoiceNo.Value != txtInvoiceNum.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Invoice No. : " + txtInvoiceNum.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Invoice No. : " + txtInvoiceNum.Text.Trim();
                }

            }
            if (hdnInvoiceDate.Value != txtInvoiceDt.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Invoice Date : " + txtInvoiceDt.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Invoice Date : " + txtInvoiceDt.Text.Trim();
                }
            }
            if (hdnInvoiceAmt.Value != txtInvoiceAmt.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Invoice Amt : " + txtInvoiceAmt.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Invoice Amt : " + txtInvoiceAmt.Text.Trim();
                }

            }
            if (hdnFreightAmt.Value != txtFreight.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Freight Amt : " + txtFreight.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Freight Amt : " + txtFreight.Text.Trim();
                }

            }
            if (hdnInsuranceAmt.Value != txtInsurance.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Insurance Amt : " + txtInsurance.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Insurance Amt : " + txtInsurance.Text.Trim();
                }

            }
            if (hdnDiscountAmt.Value != txtDiscount.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Discount Amt : " + txtDiscount.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Discount Amt : " + txtDiscount.Text.Trim();
                }

            }
            if (hdnCommAmt.Value != txtCommissionGRPP.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Comm. Amt. : " + txtCommissionGRPP.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Comm. Amt. : " + txtCommissionGRPP.Text.Trim();
                }

            }
            if (hdnOthDedChrgs.Value != txtOthDeduction.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Oth. Ded. Chrgs : " + txtOthDeduction.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Oth. Ded. Chrgs : " + txtOthDeduction.Text.Trim();
                }
            }
            if (hdnPackingChrgs.Value != txtPacking.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Packing Chrgs : " + txtPacking.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Packing Chrgs : " + txtPacking.Text.Trim();
                }

            }
            if (hdnStatus.Value != txt_status.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Status : " + txt_status.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Status : " + txt_status.Text.Trim();
                }

            }
            if (hdnFreightCurr.Value != ddlFreightCurr.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Freight Curr : " + ddlFreightCurr.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Freight Curr : " + ddlFreightCurr.SelectedValue.Trim();
                }

            }
            if (hdnInsCurr.Value != ddlInsCurr.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Ins. Curr : " + ddlInsCurr.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Ins. Curr : " + ddlInsCurr.SelectedValue.Trim();
                }

            }
            if (hdnDisCurr.Value != ddlDiscCurr.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Dis. Curr : " + ddlDiscCurr.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Dis. Curr : " + ddlDiscCurr.SelectedValue.Trim();
                }

            }
            if (hdnCommCurr.Value != ddlCommCurr.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Comm. Curr : " + ddlCommCurr.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Comm. Curr : " + ddlCommCurr.SelectedValue.Trim();
                }

            }
            if (hdnOthDedCurr.Value != ddlOthDedCurr.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Oth. Ded. Curr : " + ddlOthDedCurr.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Oth. Ded. Curr : " + ddlOthDedCurr.SelectedValue.Trim();
                }

            }
            if (hdnPackChgsCurr.Value != ddlPackChgCurr.SelectedValue.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Pack. Chgs. Curr : " + ddlPackChgCurr.SelectedValue.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Pack. Chgs. Curr : " + ddlPackChgCurr.SelectedValue.Trim();
                }

            }
            if (hdnExchRate.Value != txtGRExchRate.Text.Trim())
            {
                isneedtolog = 1;
                if (_NewValues == "")
                {
                    _NewValues = _NewValues + "Exch Rate : " + txtGRExchRate.Text.Trim();
                }
                else
                {
                    _NewValues = _NewValues + "; Exch Rate : " + txtGRExchRate.Text.Trim();
                }

            }

            //if (hdnCheckarStatus.Value != _Status)
            //{
            //    isneedtolog = 1;
            //    if (_NewValues == "")
            //    {
            //        _NewValues = _NewValues + "Checkar Status : " + _Status;
            //    }
            //    else
            //    {
            //        _NewValues = _NewValues + "; Checkar Status : " + _Status;
            //    }
            //}
            _query = "TF_Data_AuditTrailGRPP";
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
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = txtDocumentNo.Text.Trim();
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Modify");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "Covering Schedule/GR/PP Details";
            if (isneedtolog == 1)
            {
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
                //rowIndex++;
            }
        }

    }

    public void AuditGRPPDelete(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result, string _Status)
    {
        if (_result == "Delete")
        {

            string _branchCode = Request.QueryString["BranchCode"].Trim();

            TF_DATA objSave = new TF_DATA();
            _query = "TF_Data_AuditTrailGRPP";
            _NewValues = "Shipping Bill No : " + _NewValues;
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
            
            SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
            DocumentNo.Value = txtDocumentNo.Text;
            SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
            DocumnetDate.Value = txtDateRcvd.Text.Trim();
            SqlParameter Mode = new SqlParameter("@Mode", "Delete");
            SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
            user.Value = _userName;
            string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
            moddate.Value = _moddate;
            string _menu = "Export Bill Data Entry";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            SqlParameter TabName = new SqlParameter("@TabName", SqlDbType.VarChar);
            TabName.Value = "Covering Schedule/GR/PP Details";
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu, TabName);
        }

    }
}