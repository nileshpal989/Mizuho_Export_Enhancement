using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class EXP_EXP_AddEditApprovedLodgemetForROD_Checker : System.Web.UI.Page
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
                hdnUserName.Value = Session["userName"].ToString();
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
    ddlCurrencyGRPP, ddlFreightCurr, ddlInsCurr, ddlDiscCurr, ddlCommCurr, ddlOthDedCurr, ddlPackChgCurr); // Added by Anand 27-02-2024
                    fillTaxRates();
                    fillPortCodes();

                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtDocumentNo.Text = Request.QueryString["DocNo"].Trim();
                        if (Request.QueryString["DocPrFx"].Trim() == "All")
                        {
                            string Typebill = "";
                            if (txtDocumentNo.Text.Substring(0, 2) == "EB")
                            {
                                Typebill = txtDocumentNo.Text.Substring(0, 2);
                            }
                            else
                            {
                                Typebill = txtDocumentNo.Text.Substring(0, 3);
                            }

                            txtDocType.Text = Typebill;
                        }
                        else
                        {
                            txtDocType.Text = Request.QueryString["DocPrFx"].Trim();
                        }

                        if (txtDocType.Text == "EB")
                        {
                            TTFIRCVisible.Visible = true;
                            hdnBranchCode.Value = txtDocumentNo.Text.Substring(3, 3);
                        }
                        else
                        {
                            TTFIRCVisible.Visible = false;
                            hdnBranchCode.Value = txtDocumentNo.Text.Substring(4, 3);
                        }
                        fillDetails(Request.QueryString["DocNo"].Trim());

                        if (Request.QueryString["ForeignOrLocal"].Trim() == "F")
                        {
                            hdnForeignLocal.Value = "F";
                            Check_LEI_ThresholdLimit();
                        }
                        else
                        {
                            hdnForeignLocal.Value = "L";
                            btnLEI.Visible = false;
                        }
                        rbtnSightBill.Enabled = false;
                        rbtnUsanceBill.Enabled = false;

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

                        txtDocType.Text = Request.QueryString["DocPrFx"].Trim();
                        string _prFx = Request.QueryString["DocPrFx"].Trim();
                        string _srNo = Request.QueryString["DocSrNo"].Trim();
                        string _branchCode = Request.QueryString["BranchCode"].Trim();

                        string _year = Request.QueryString["DocYear"].Trim();

                        txtDocumentNo.Text = _prFx + "/" + _branchCode + "/" + _year + _srNo;

                        DDL_Dispatch();
                        DDL_Dispatch2();
                    }

                    rbtnSightBill.Enabled = false;
                    rbtnUsanceBill.Enabled = false;
                    switch (txtDocType.Text)
                    {

                        case "BLA":
                            lblDocumentType.Text = "Bills Bought with L/C at Sight";
                            rbtnUsanceBill.Checked = false;
                            rbtnSightBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtNoOfDays.Text = "25";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "N" + txtDocumentNo.Text.Substring(8, 6);
                            break;
                        case "BLU":
                            lblDocumentType.Text = "Bills Bought with L/C Usance";
                            rbtnSightBill.Checked = false;
                            rbtnUsanceBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "N" + txtDocumentNo.Text.Substring(8, 6);
                            break;
                        case "BBA":
                            lblDocumentType.Text = "Bills Bought without L/C at Sight";
                            rbtnUsanceBill.Checked = false;
                            rbtnSightBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtNoOfDays.Text = "25";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "P" + txtDocumentNo.Text.Substring(8, 6);
                            break;
                        case "BBU":
                            lblDocumentType.Text = "Bills Bought without L/C Usance";
                            rbtnSightBill.Checked = false;
                            rbtnUsanceBill.Checked = true;
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "D" + txtDocumentNo.Text.Substring(8, 6);
                            break;
                        case "BCA":
                            lblDocumentType.Text = "Bills For Collection at Sight";
                            rbtnUsanceBill.Checked = false;
                            rbtnSightBill.Checked = true;
                            btnTTRefNoList.Enabled = true;
                            txtNoOfDays.Text = "25";
                            //txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "C" + txtDocumentNo.Text.Substring(8, 6);
                            break;
                        case "BCU":
                            lblDocumentType.Text = "Bills For Collection Usance";
                            rbtnSightBill.Checked = false;
                            rbtnUsanceBill.Checked = true;
                            btnTTRefNoList.Enabled = true;
                            // txtOutOfDays.Text = "360";
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "C" + txtDocumentNo.Text.Substring(8, 6);
                            break;
                        case "EB":
                            lblDocumentType.Text = "Advance";
                            btnTTRefNoList.Enabled = true;
                            txtExchRate.Text = "1";
                            txtRBIDocType.Text = "M" + txtDocumentNo.Text.Substring(7, 6);
                            rbtnSightBill.Enabled = true;
                            rbtnUsanceBill.Enabled = true;

                            break;
                        case "IBD":
                            lblDocumentType.Text = "Vendor Bill Discounting";
                            ddlCurrency.SelectedValue = "INR";
                            txtExchRate.Text = "1";
                            txtExchRtEBR.Text = "1";
                            chkRBI.Visible = true;
                            rbtnSightBill.Enabled = true;
                            rbtnUsanceBill.Enabled = true;
                            break;
                    }

                    // hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                }
                //txtCoveringFrom.Attributes.Add("onblur", "return checkValidPortCode();");

                ddlFormType.Attributes.Add("onkeyup", "return generateSDFcustomsNo();");
                ddlFormType.Attributes.Add("onclick", "return generateSDFcustomsNo();");

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
                //btnSave.Attributes.Add("onclick", "return ValidateSave();");
                //btnSavePrint.Attributes.Add("onclick", "return ValidateSave();");

                rbtnSightBill.Attributes.Add("onclick", "return Calculate();");
                rbtnUsanceBill.Attributes.Add("onclick", "return Calculate();");

                //Textcahnged Does not work when not commented Gbase
                rbtnAfterAWB.Attributes.Add("onclick", "return radioButtonChanged();");
                rbtnFromAWB.Attributes.Add("onclick", "return radioButtonChanged();");
                rbtnSight.Attributes.Add("onclick", "return radioButtonChanged();");
                rbtnDA.Attributes.Add("onclick", "return radioButtonChanged();");
                rbtnFromInvoice.Attributes.Add("onclick", "return radioButtonChanged();");
                rbtnOthers.Attributes.Add("onclick", "return radioButtonChanged();");

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
                txtNoOfDays.Attributes.Add("onblur", "return CalculateDueDate();");

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
                txtReimbAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //New With Gbase
                txtGRExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
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
                ddlCurrency.Attributes.Add("onblur", "return FillGBaseDetailsBYCurrency();");
                ddlApproveReject.Attributes.Add("onchange", "return DialogAlertLodgementROD();");
                SetInitialRow();
                if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                {
                    ddlApproveReject.Enabled = false;
                }
                if (Request.QueryString["Status"].ToString() == "Send To Checker")
                {
                    //ddlApproveReject.Enabled = false;
                    TF_DATA obj = new TF_DATA();
                    SqlParameter p1 = new SqlParameter("@DocNo", txtDocumentNo.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@Checker", hdnUserName.Value);
                    string Result = obj.SaveDeleteData("TF_EXP_SameTransIdCheckerAlert_ROD", p1, p2);
                }
            }

        }
    }
    // Added by Anand 27-02-2024
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

    #endregion

    #region Help Codes

    protected void btnCustomerCode_Click(object sender, EventArgs e)
    {
        if (hdnCustomerCode.Value != "")
        {
            txtCustAcNo.Text = hdnCustomerCode.Value;
            fillCustomerCodeDescription();
            txtCustAcNo.Focus();
        }
    }

    protected void btnOverseasParty_Click(object sender, EventArgs e)
    {
        if (hdnOverseasPartyId.Value != "")
        {
            txtOverseasPartyID.Text = hdnOverseasPartyId.Value;
            fillOverseasPartyDescription();
            txtOverseasPartyID.Focus();
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
            hdnCustname.Value = lblCustomerDesc.Text;
            if (lblCustomerDesc.Text.Length > 20)
            {
                lblCustomerDesc.ToolTip = lblCustomerDesc.Text;
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
            if (lblOverseasPartyDesc.Text.Length > 20)
            {
                lblOverseasPartyDesc.ToolTip = lblOverseasPartyDesc.Text;
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
            txtLCNoIssuedBy.Text = lblOverseasBankDesc.Text;
            if (lblOverseasBankDesc.Text.Length > 20)
            {
                lblOverseasBankDesc.ToolTip = lblOverseasBankDesc.Text;
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
        Response.Redirect("EXP_ViewApprovedLodgemetForROD_Checker.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocumentNo.Text.Trim());
        SqlParameter pDocNo = new SqlParameter("@DocumentNo", txtDocumentNo.Text.Trim());
        string _query = "TF_EXP_GetExportBillEntryDetails"; //--------- kept same as imward liquidation
        DataTable dt = obj.getData(_query, pDocNo);
        string CheckerStatus = "";
        if (dt.Rows.Count > 0)
        {

            CheckerStatus = dt.Rows[0]["ApproveROD"].ToString();
        }

        //SqlParameter P_SR_NO = new SqlParameter("@SR_NO", txtSrNo.Text.Trim());
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
            //SqlParameter P_SR_NO = new SqlParameter("@SR_NO", txtSrNo.Text.Trim());
            string AR = "";
            if (ddlApproveReject.SelectedIndex == 1)
            {
                AR = "A";
                TransferExportToEDPMS();
            }
            if (ddlApproveReject.SelectedIndex == 2)
            {
                AR = "R";
            }
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter P_CheckedBy = new SqlParameter("@CheckBy", hdnUserName.Value);
            string Result = obj.SaveDeleteData("TF_EXP_ApproveRODChekerApproveReject_Lodgement", P_DocNo, P_Status, P_RejectReason, P_CheckedBy);
            Response.Redirect("EXP_ViewApprovedLodgemetForROD_Checker.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        }
    }

    private void TransferExportToEDPMS()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@Document_No", txtDocumentNo.Text.Trim());
        string Result = obj.SaveDeleteData("TF_EDPMS_DATA_TRANSFER_DocBill_Approve", P_DocNo);

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
        Response.Redirect("EXP_ViewApprovedLodgemetForROD_Checker.aspx", true);
    }

    [WebMethod]
    public static void ExecuteCSharpCodeLodg(string docNo, string checker)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@DocNo", docNo);
        SqlParameter p2 = new SqlParameter("@Checker", checker);
        string Result = obj.SaveDeleteData("TF_EXP_SameTransIdCheckerAlert_ROD", p1, p2);
    }

    protected void fillDetails(string _DocNo)
    {

        DDL_Dispatch();

        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = _DocNo;
        string _query = "TF_EXP_GetApprovedRODDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //------------------Document Details---------------------//
            string STATUSI = dt.Rows[0]["ApproveROD"].ToString().Trim();
            string USERI = dt.Rows[0]["ApprovedRODCheckBy"].ToString().Trim();

            if (STATUSI == "I" && USERI != hdnUserName.Value)
            {
                string script = @"
                         <script type='text/javascript'>
                             var confirmation = confirm('Selected transaction is in progress! If you proceed any previous changes made may be lost. Do you still want to proceed?');
                             if (confirmation) {
                                ExecuteCSharpCodeLodg();
                             } else {
                                 // Redirect to a page
                                 window.location.href = 'EXP_ViewApprovedLodgemetForROD_Checker.aspx';
                            }
                         </script>
                     ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmationScript", script);
            }

            if (dt.Rows[0]["IsRealised"].ToString().Trim() == "YES")
            {
                //labelMessage.Font.Bold = true;
                labelMessage.Font.Size = 12;
                labelMessage.Text = "This Document is already Realised."; ////  , You cannot update commented for when part realized also.
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

            if (dt.Rows[0]["ApproveROD"].ToString() == "A")
            {
                ddlApproveReject.SelectedValue = "1";
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

            if (ddlDispachInd.SelectedValue == "Dispatched directly by exporter")
            {
                ddlDispBydefault.SelectedValue = "Non-Dispatch";
            }
            else
            {
                ddlDispBydefault.SelectedValue = dt.Rows[0]["DispByDefaltuValue"].ToString().Trim();
            }
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

            //------------------------------------Anand 04-07-2023--------------------------------------------          
            if (dt.Rows[0]["TTREFNO1"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 43;
                txtTTRefNo1.Text = dt.Rows[0]["TTREFNO1"].ToString().Trim();
                txtTTAmount1.Text = dt.Rows[0]["TTAmt1"].ToString().Trim();
                ddlTTCurrency1.SelectedValue = dt.Rows[0]["TTCurr1"].ToString().Trim();
                txtTotTTAmt1.Text = dt.Rows[0]["TotTTAmt1"].ToString().Trim();
                txtBalTTAmt1.Text = dt.Rows[0]["BalTTAmt1"].ToString().Trim();
                ddlTTRealisedCurr1.SelectedValue = dt.Rows[0]["TTRealisedCurr1"].ToString().Trim();
                txtTTCrossCurrRate1.Text = dt.Rows[0]["TTCrossCurrRate1"].ToString().Trim();
                txtTTAmtRealised1.Text = dt.Rows[0]["TTAmtRealised1"].ToString().Trim();
                TTRemitterName(txtTTRefNo1.Text, "1");
            }
            else
            {
                btnTTRef1.Visible = false;
                txtTTRefNo1.Visible = false;
                txtTTAmount1.Visible = false;
                ddlTTCurrency1.Visible = false;
                txtTotTTAmt1.Visible = false;
                txtBalTTAmt1.Visible = false;
                ddlTTRealisedCurr1.Visible = false;
                txtTTCrossCurrRate1.Visible = false;
                txtTTAmtRealised1.Visible = false;
            }

            if (dt.Rows[0]["TTREFNO2"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 65;
                txtTTRefNo2.Text = dt.Rows[0]["TTREFNO2"].ToString().Trim();
                txtTTAmount2.Text = dt.Rows[0]["TTAmt2"].ToString().Trim();
                ddlTTCurrency2.SelectedValue = dt.Rows[0]["TTCurr2"].ToString().Trim();
                txtTotTTAmt2.Text = dt.Rows[0]["TotTTAmt2"].ToString().Trim();
                txtBalTTAmt2.Text = dt.Rows[0]["BalTTAmt2"].ToString().Trim();
                ddlTTRealisedCurr2.SelectedValue = dt.Rows[0]["TTRealisedCurr2"].ToString().Trim();
                txtTTCrossCurrRate2.Text = dt.Rows[0]["TTCrossCurrRate2"].ToString().Trim();
                txtTTAmtRealised2.Text = dt.Rows[0]["TTAmtRealised2"].ToString().Trim();
                TTRemitterName(txtTTRefNo2.Text, "2");
            }
            else
            {
                btnTTRef2.Visible = false;
                txtTTRefNo2.Visible = false;
                txtTTAmount2.Visible = false;
                ddlTTCurrency2.Visible = false;
                txtTotTTAmt2.Visible = false;
                txtBalTTAmt2.Visible = false;
                ddlTTRealisedCurr2.Visible = false;
                txtTTCrossCurrRate2.Visible = false;
                txtTTAmtRealised2.Visible = false;
            }

            if (dt.Rows[0]["TTREFNO3"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 88;
                txtTTRefNo3.Text = dt.Rows[0]["TTREFNO3"].ToString().Trim();
                txtTTAmount3.Text = dt.Rows[0]["TTAmt3"].ToString().Trim();
                ddlTTCurrency3.SelectedValue = dt.Rows[0]["TTCurr3"].ToString().Trim();
                txtTotTTAmt3.Text = dt.Rows[0]["TotTTAmt3"].ToString().Trim();
                txtBalTTAmt3.Text = dt.Rows[0]["BalTTAmt3"].ToString().Trim();
                ddlTTRealisedCurr3.SelectedValue = dt.Rows[0]["TTRealisedCurr3"].ToString().Trim();
                txtTTCrossCurrRate3.Text = dt.Rows[0]["TTCrossCurrRate3"].ToString().Trim();
                txtTTAmtRealised3.Text = dt.Rows[0]["TTAmtRealised3"].ToString().Trim();
                TTRemitterName(txtTTRefNo3.Text, "3");
            }
            else
            {
                btnTTRef3.Visible = false;
                txtTTRefNo3.Visible = false;
                txtTTAmount3.Visible = false;
                ddlTTCurrency3.Visible = false;
                txtTotTTAmt3.Visible = false;
                txtBalTTAmt3.Visible = false;
                ddlTTRealisedCurr3.Visible = false;
                txtTTCrossCurrRate3.Visible = false;
                txtTTAmtRealised3.Visible = false;
            }

            if (dt.Rows[0]["TTREFNO4"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 110;
                txtTTRefNo4.Text = dt.Rows[0]["TTREFNO4"].ToString().Trim();
                txtTTAmount4.Text = dt.Rows[0]["TTAmt4"].ToString().Trim();
                ddlTTCurrency4.SelectedValue = dt.Rows[0]["TTCurr4"].ToString().Trim();
                txtTotTTAmt4.Text = dt.Rows[0]["TotTTAmt4"].ToString().Trim();
                txtBalTTAmt4.Text = dt.Rows[0]["BalTTAmt4"].ToString().Trim();
                ddlTTRealisedCurr4.SelectedValue = dt.Rows[0]["TTRealisedCurr4"].ToString().Trim();
                txtTTCrossCurrRate4.Text = dt.Rows[0]["TTCrossCurrRate4"].ToString().Trim();
                txtTTAmtRealised4.Text = dt.Rows[0]["TTAmtRealised4"].ToString().Trim();
                TTRemitterName(txtTTRefNo4.Text, "4");
            }
            else
            {
                btnTTRef4.Visible = false;
                txtTTRefNo4.Visible = false;
                txtTTAmount4.Visible = false;
                ddlTTCurrency4.Visible = false;
                txtTotTTAmt4.Visible = false;
                txtBalTTAmt4.Visible = false;
                ddlTTRealisedCurr4.Visible = false;
                txtTTCrossCurrRate4.Visible = false;
                txtTTAmtRealised4.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO5"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 132;
                txtTTRefNo5.Text = dt.Rows[0]["TTREFNO5"].ToString().Trim();
                txtTTAmount5.Text = dt.Rows[0]["TTAmt5"].ToString().Trim();
                ddlTTCurrency5.SelectedValue = dt.Rows[0]["TTCurr5"].ToString().Trim();
                txtTotTAmt5.Text = dt.Rows[0]["TotTTAmt5"].ToString().Trim();//check
                txtBalTTAmt5.Text = dt.Rows[0]["BalTTAmt5"].ToString().Trim();
                ddlTTRealisedCurr5.SelectedValue = dt.Rows[0]["TTRealisedCurr5"].ToString().Trim();
                txtTTCrossCurrRate5.Text = dt.Rows[0]["TTCrossCurrRate5"].ToString().Trim();
                txtTTAmtRealised5.Text = dt.Rows[0]["TTAmtRealised5"].ToString().Trim();
                TTRemitterName(txtTTRefNo5.Text, "5");
            }
            else
            {
                btnTTRef5.Visible = false;
                txtTTRefNo5.Visible = false;
                txtTTAmount5.Visible = false;
                ddlTTCurrency5.Visible = false;
                txtTotTAmt5.Visible = false;
                txtBalTTAmt5.Visible = false;
                ddlTTRealisedCurr5.Visible = false;
                txtTTCrossCurrRate5.Visible = false;
                txtTTAmtRealised5.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO6"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 155;
                txtTTRefNo6.Text = dt.Rows[0]["TTREFNO6"].ToString().Trim();
                txtTTAmount6.Text = dt.Rows[0]["TTAmt6"].ToString().Trim();
                ddlTTCurrency6.SelectedValue = dt.Rows[0]["TTCurr6"].ToString().Trim();
                txtTotTTAmt6.Text = dt.Rows[0]["TotTTAmt6"].ToString().Trim();
                txtBalTTAmt6.Text = dt.Rows[0]["BalTTAmt6"].ToString().Trim();
                ddlTTRealisedCurr6.SelectedValue = dt.Rows[0]["TTRealisedCurr6"].ToString().Trim();
                txtTTCrossCurrRate6.Text = dt.Rows[0]["TTCrossCurrRate6"].ToString().Trim();
                txtTTAmtRealised6.Text = dt.Rows[0]["TTAmtRealised6"].ToString().Trim();
                TTRemitterName(txtTTRefNo6.Text, "6");
            }
            else
            {
                btnTTRef6.Visible = false;
                txtTTRefNo6.Visible = false;
                txtTTAmount6.Visible = false;
                ddlTTCurrency6.Visible = false;
                txtTotTTAmt6.Visible = false;
                txtBalTTAmt6.Visible = false;
                ddlTTRealisedCurr6.Visible = false;
                txtTTCrossCurrRate6.Visible = false;
                txtTTAmtRealised6.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO7"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 177;
                txtTTRefNo7.Text = dt.Rows[0]["TTREFNO7"].ToString().Trim();
                txtTTAmount7.Text = dt.Rows[0]["TTAmt7"].ToString().Trim();
                ddlTTCurrency7.SelectedValue = dt.Rows[0]["TTCurr7"].ToString().Trim();
                txtTotTTAmt7.Text = dt.Rows[0]["TotTTAmt7"].ToString().Trim();
                txtBalTTAmt7.Text = dt.Rows[0]["BalTTAmt7"].ToString().Trim();
                ddlTTRealisedCurr7.SelectedValue = dt.Rows[0]["TTRealisedCurr7"].ToString().Trim();
                txtTTCrossCurrRate7.Text = dt.Rows[0]["TTCrossCurrRate7"].ToString().Trim();
                txtTTAmtRealised7.Text = dt.Rows[0]["TTAmtRealised7"].ToString().Trim();
                TTRemitterName(txtTTRefNo7.Text, "7");
            }
            else
            {
                btnTTRef7.Visible = false;
                txtTTRefNo7.Visible = false;
                txtTTAmount7.Visible = false;
                ddlTTCurrency7.Visible = false;
                txtTotTTAmt7.Visible = false;
                txtBalTTAmt7.Visible = false;
                ddlTTRealisedCurr7.Visible = false;
                txtTTCrossCurrRate7.Visible = false;
                txtTTAmtRealised7.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO8"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 202;
                txtTTRefNo8.Text = dt.Rows[0]["TTREFNO8"].ToString().Trim();
                txtTTAmount8.Text = dt.Rows[0]["TTAmt8"].ToString().Trim();
                ddlTTCurrency8.SelectedValue = dt.Rows[0]["TTCurr8"].ToString().Trim();
                txtTotTTAmt8.Text = dt.Rows[0]["TotTTAmt8"].ToString().Trim();
                txtBalTAmt8.Text = dt.Rows[0]["BalTTAmt8"].ToString().Trim();//check
                ddlTTRealisedCurr8.SelectedValue = dt.Rows[0]["TTRealisedCurr8"].ToString().Trim();
                txtTTCrossCurrRate8.Text = dt.Rows[0]["TTCrossCurrRate8"].ToString().Trim();
                txtTTAmtRealised8.Text = dt.Rows[0]["TTAmtRealised8"].ToString().Trim();
                TTRemitterName(txtTTRefNo8.Text, "8");
            }
            else
            {
                btnTTRef8.Visible = false;
                txtTTRefNo8.Visible = false;
                txtTTAmount8.Visible = false;
                ddlTTCurrency8.Visible = false;
                txtTotTTAmt8.Visible = false;
                txtBalTAmt8.Visible = false;
                ddlTTRealisedCurr8.Visible = false;
                txtTTCrossCurrRate8.Visible = false;
                txtTTAmtRealised8.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO9"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 224;
                txtTTRefNo9.Text = dt.Rows[0]["TTREFNO9"].ToString().Trim();
                txtTTAmount9.Text = dt.Rows[0]["TTAmt9"].ToString().Trim();
                ddlTTCurrency9.SelectedValue = dt.Rows[0]["TTCurr9"].ToString().Trim();
                txtTtTTAmt9.Text = dt.Rows[0]["TotTTAmt9"].ToString().Trim();//check
                txtBalTTAmt9.Text = dt.Rows[0]["BalTTAmt9"].ToString().Trim();
                ddlTTRealisedCurr9.SelectedValue = dt.Rows[0]["TTRealisedCurr9"].ToString().Trim();
                txtTTCrossCurrRate9.Text = dt.Rows[0]["TTCrossCurrRate9"].ToString().Trim();
                txtTTAmtRealised9.Text = dt.Rows[0]["TTAmtRealised9"].ToString().Trim();
                TTRemitterName(txtTTRefNo9.Text, "9");
            }
            else
            {
                btnTTRef9.Visible = false;
                txtTTRefNo9.Visible = false;
                txtTTAmount9.Visible = false;
                ddlTTCurrency9.Visible = false;
                txtTtTTAmt9.Visible = false;
                txtBalTTAmt9.Visible = false;
                ddlTTRealisedCurr9.Visible = false;
                txtTTCrossCurrRate9.Visible = false;
                txtTTAmtRealised9.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO10"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 246;
                txtTTRefNo10.Text = dt.Rows[0]["TTREFNO10"].ToString().Trim();
                txtTTAmount10.Text = dt.Rows[0]["TTAmt10"].ToString().Trim();
                ddlTTCurrency10.SelectedValue = dt.Rows[0]["TTCurr10"].ToString().Trim();
                txtTotTTAmt10.Text = dt.Rows[0]["TotTTAmt10"].ToString().Trim();
                txtBalTTAmt10.Text = dt.Rows[0]["BalTTAmt10"].ToString().Trim();
                ddlTTRealisedCurr10.SelectedValue = dt.Rows[0]["TTRealisedCurr10"].ToString().Trim();
                txtTTCrossCurrRate10.Text = dt.Rows[0]["TTCrossCurrRate10"].ToString().Trim();
                txtTTAmtRealised10.Text = dt.Rows[0]["TTAmtRealised10"].ToString().Trim();
                TTRemitterName(txtTTRefNo10.Text, "10");
            }
            else
            {
                btnTTRef10.Visible = false;
                txtTTRefNo10.Visible = false;
                txtTTAmount10.Visible = false;
                ddlTTCurrency10.Visible = false;
                txtTotTTAmt10.Visible = false;
                txtBalTTAmt10.Visible = false;
                ddlTTRealisedCurr10.Visible = false;
                txtTTCrossCurrRate10.Visible = false;
                txtTTAmtRealised10.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO11"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 270;
                txtTTRefNo11.Text = dt.Rows[0]["TTREFNO11"].ToString().Trim();
                txtTTAmount11.Text = dt.Rows[0]["TTAmt11"].ToString().Trim();
                ddlTTCurrency11.SelectedValue = dt.Rows[0]["TTCurr11"].ToString().Trim();
                txtTotTTAmt11.Text = dt.Rows[0]["TotTTAmt11"].ToString().Trim();
                txtBalTTAmt11.Text = dt.Rows[0]["BalTTAmt11"].ToString().Trim();
                ddlTTRealisedCurr11.SelectedValue = dt.Rows[0]["TTRealisedCurr11"].ToString().Trim();
                txtTTCrossCurrRate11.Text = dt.Rows[0]["TTCrossCurrRate11"].ToString().Trim();
                txtTTAmtRealised11.Text = dt.Rows[0]["TTAmtRealised11"].ToString().Trim();
                TTRemitterName(txtTTRefNo11.Text, "11");
            }
            else
            {
                btnTTRef11.Visible = false;
                txtTTRefNo11.Visible = false;
                txtTTAmount11.Visible = false;
                ddlTTCurrency11.Visible = false;
                txtTotTTAmt11.Visible = false;
                txtBalTTAmt11.Visible = false;
                ddlTTRealisedCurr11.Visible = false;
                txtTTCrossCurrRate11.Visible = false;
                txtTTAmtRealised11.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO12"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 292;
                txtTTRefNo12.Text = dt.Rows[0]["TTREFNO12"].ToString().Trim();
                txtTTAmount12.Text = dt.Rows[0]["TTAmt12"].ToString().Trim();
                ddlTTCurrency12.SelectedValue = dt.Rows[0]["TTCurr12"].ToString().Trim();
                txtTotTTAmt12.Text = dt.Rows[0]["TotTTAmt12"].ToString().Trim();
                txtBalTTAmt12.Text = dt.Rows[0]["BalTTAmt12"].ToString().Trim();
                ddlTTRealisedCurr12.SelectedValue = dt.Rows[0]["TTRealisedCurr12"].ToString().Trim();
                txtTTCrossCurrRate12.Text = dt.Rows[0]["TTCrossCurrRate12"].ToString().Trim();
                txtTTAmtRealised12.Text = dt.Rows[0]["TTAmtRealised12"].ToString().Trim();
                TTRemitterName(txtTTRefNo12.Text, "12");
            }
            else
            {
                btnTTRef12.Visible = false;
                txtTTRefNo12.Visible = false;
                txtTTAmount12.Visible = false;
                ddlTTCurrency12.Visible = false;
                txtTotTTAmt12.Visible = false;
                txtBalTTAmt12.Visible = false;
                ddlTTRealisedCurr12.Visible = false;
                txtTTCrossCurrRate12.Visible = false;
                txtTTAmtRealised12.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO13"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 314;
                txtTTRefNo13.Text = dt.Rows[0]["TTREFNO13"].ToString().Trim();
                txtTTAmount13.Text = dt.Rows[0]["TTAmt13"].ToString().Trim();
                ddlTTCurrency13.SelectedValue = dt.Rows[0]["TTCurr13"].ToString().Trim();
                txtTotTTAmt13.Text = dt.Rows[0]["TotTTAmt13"].ToString().Trim();
                txtBalTTAmt13.Text = dt.Rows[0]["BalTTAmt13"].ToString().Trim();
                ddlTTRealisedCurr13.SelectedValue = dt.Rows[0]["TTRealisedCurr13"].ToString().Trim();
                txtTTCrossCurrRate13.Text = dt.Rows[0]["TTCrossCurrRate13"].ToString().Trim();
                txtTTAmtRealised13.Text = dt.Rows[0]["TTAmtRealised13"].ToString().Trim();
                TTRemitterName(txtTTRefNo13.Text, "13");
            }
            else
            {
                btnTTRef13.Visible = false;
                txtTTRefNo13.Visible = false;
                txtTTAmount13.Visible = false;
                ddlTTCurrency13.Visible = false;
                txtTotTTAmt13.Visible = false;
                txtBalTTAmt13.Visible = false;
                ddlTTRealisedCurr13.Visible = false;
                txtTTCrossCurrRate13.Visible = false;
                txtTTAmtRealised13.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO14"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 338;
                txtTTRefNo14.Text = dt.Rows[0]["TTREFNO14"].ToString().Trim();
                txtTTAmount14.Text = dt.Rows[0]["TTAmt14"].ToString().Trim();
                ddlTTCurrency14.SelectedValue = dt.Rows[0]["TTCurr14"].ToString().Trim();
                txtTotTTAmt14.Text = dt.Rows[0]["TotTTAmt14"].ToString().Trim();
                txtBalTTAmt14.Text = dt.Rows[0]["BalTTAmt14"].ToString().Trim();
                ddlTTRealisedCurr14.SelectedValue = dt.Rows[0]["TTRealisedCurr14"].ToString().Trim();
                txtTTCrossCurrRate14.Text = dt.Rows[0]["TTCrossCurrRate14"].ToString().Trim();
                txtTTAmtRealised14.Text = dt.Rows[0]["TTAmtRealised14"].ToString().Trim();
                TTRemitterName(txtTTRefNo14.Text, "14");
            }
            else
            {
                btnTTRef14.Visible = false;
                txtTTRefNo14.Visible = false;
                txtTTAmount14.Visible = false;
                ddlTTCurrency14.Visible = false;
                txtTotTTAmt14.Visible = false;
                txtBalTTAmt14.Visible = false;
                ddlTTRealisedCurr14.Visible = false;
                txtTTCrossCurrRate14.Visible = false;
                txtTTAmtRealised14.Visible = false;
            }
            if (dt.Rows[0]["TTREFNO15"].ToString().Trim() != "")
            {
                cpe2.ExpandedSize = 365;
                txtTTRefNo15.Text = dt.Rows[0]["TTREFNO15"].ToString().Trim();
                txtTTAmount15.Text = dt.Rows[0]["TTAmt15"].ToString().Trim();
                ddlTTCurrency15.SelectedValue = dt.Rows[0]["TTCurr15"].ToString().Trim();
                txtTotTTAmt15.Text = dt.Rows[0]["TotTTAmt15"].ToString().Trim();
                txtBalTTAmt15.Text = dt.Rows[0]["BalTTAmt15"].ToString().Trim();
                ddlTTRealisedCurr15.SelectedValue = dt.Rows[0]["TTRealisedCurr15"].ToString().Trim();
                txtTTCrossCurrRate15.Text = dt.Rows[0]["TTCrossCurrRate15"].ToString().Trim();
                txtTTAmtRealised15.Text = dt.Rows[0]["TTAmtRealised15"].ToString().Trim();
                TTRemitterName(txtTTRefNo15.Text, "15");
            }
            else
            {
                btnTTRef15.Visible = false;
                txtTTRefNo15.Visible = false;
                txtTTAmount15.Visible = false;
                ddlTTCurrency15.Visible = false;
                txtTotTTAmt15.Visible = false;
                txtBalTTAmt15.Visible = false;
                ddlTTRealisedCurr15.Visible = false;
                txtTTCrossCurrRate15.Visible = false;
                txtTTAmtRealised15.Visible = false;
            }

            if (dt.Rows[0]["FIRCNo_OtrBank1"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 43;
                txtFIRCNo1_OB.Text = dt.Rows[0]["FIRCNo_OtrBank1"].ToString().Trim();
                txtFIRCDate1_OB.Text = dt.Rows[0]["FIRCDate_OtrBank1"].ToString().Trim();
                txtFIRCAmount1_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank1"].ToString().Trim();
                txtFIRCADCode1_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank1"].ToString().Trim();
                ddlFIRCCurrency1_OB.SelectedValue = dt.Rows[0]["FIRCCurrency1_OB"].ToString().Trim();
                ddlFIRCRealisedCurr1_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr1_OB"].ToString().Trim();
                txtFIRCCrossCurrRate1_OB.Text = dt.Rows[0]["FIRCCrossCurrRate1_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB1_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB1_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo1_OB.Visible = false;
                txtFIRCDate1_OB.Visible = false;
                txtFIRCAmount1_OB.Visible = false;
                txtFIRCADCode1_OB.Visible = false;
                ddlFIRCCurrency1_OB.Visible = false;
                ddlFIRCRealisedCurr1_OB.Visible = false;
                txtFIRCCrossCurrRate1_OB.Visible = false;
                txtFIRCTobeAdjustedinSB1_OB.Visible = false;

            }

            if (dt.Rows[0]["FIRCNo_OtrBank2"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 65;
                txtFIRCNo2_OB.Text = dt.Rows[0]["FIRCNo_OtrBank2"].ToString().Trim();
                txtFIRCDate2_OB.Text = dt.Rows[0]["FIRCDate_OtrBank2"].ToString().Trim();
                txtFIRCAmount2_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank2"].ToString().Trim();
                txtFIRCADCode2_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank2"].ToString().Trim();
                ddlFIRCCurrency2_OB.SelectedValue = dt.Rows[0]["FIRCCurrency2_OB"].ToString().Trim();
                ddlFIRCRealisedCurr2_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr2_OB"].ToString().Trim();
                txtFIRCCrossCurrRate2_OB.Text = dt.Rows[0]["FIRCCrossCurrRate2_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB2_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB2_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo2_OB.Visible = false;
                txtFIRCDate2_OB.Visible = false;
                txtFIRCAmount2_OB.Visible = false;
                txtFIRCADCode2_OB.Visible = false;
                ddlFIRCCurrency2_OB.Visible = false;
                ddlFIRCRealisedCurr2_OB.Visible = false;
                txtFIRCCrossCurrRate2_OB.Visible = false;
                txtFIRCTobeAdjustedinSB2_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank3"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 88;
                txtFIRCNo3_OB.Text = dt.Rows[0]["FIRCNo_OtrBank3"].ToString().Trim();
                txtFIRCDate3_OB.Text = dt.Rows[0]["FIRCDate_OtrBank3"].ToString().Trim();
                txtFIRCAmount3_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank3"].ToString().Trim();
                txtFIRCADCode3_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank3"].ToString().Trim();
                ddlFIRCCurrency3_OB.SelectedValue = dt.Rows[0]["FIRCCurrency3_OB"].ToString().Trim();
                ddlFIRCRealisedCurr3_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr3_OB"].ToString().Trim();
                txtFIRCCrossCurrRate3_OB.Text = dt.Rows[0]["FIRCCrossCurrRate3_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB3_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB3_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo3_OB.Visible = false;
                txtFIRCDate3_OB.Visible = false;
                txtFIRCAmount3_OB.Visible = false;
                txtFIRCADCode3_OB.Visible = false;
                ddlFIRCCurrency3_OB.Visible = false;
                ddlFIRCRealisedCurr3_OB.Visible = false;
                txtFIRCCrossCurrRate3_OB.Visible = false;
                txtFIRCTobeAdjustedinSB3_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank4"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 110;
                txtFIRCNo4_OB.Text = dt.Rows[0]["FIRCNo_OtrBank4"].ToString().Trim();
                txtFIRCDate4_OB.Text = dt.Rows[0]["FIRCDate_OtrBank4"].ToString().Trim();
                txtFIRCAmount4_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank4"].ToString().Trim();
                txtFIRCADCode4_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank4"].ToString().Trim();
                ddlFIRCCurrency4_OB.SelectedValue = dt.Rows[0]["FIRCCurrency4_OB"].ToString().Trim();
                ddlFIRCRealisedCurr4_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr4_OB"].ToString().Trim();
                txtFIRCCrossCurrRate4_OB.Text = dt.Rows[0]["FIRCCrossCurrRate4_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB4_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB4_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo4_OB.Visible = false;
                txtFIRCDate4_OB.Visible = false;
                txtFIRCAmount4_OB.Visible = false;
                txtFIRCADCode4_OB.Visible = false;
                ddlFIRCCurrency4_OB.Visible = false;
                ddlFIRCRealisedCurr4_OB.Visible = false;
                txtFIRCCrossCurrRate4_OB.Visible = false;
                txtFIRCTobeAdjustedinSB4_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank5"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 132;
                txtFIRCNo5_OB.Text = dt.Rows[0]["FIRCNo_OtrBank5"].ToString().Trim();
                txtFIRCDate5_OB.Text = dt.Rows[0]["FIRCDate_OtrBank5"].ToString().Trim();
                txtFIRCAmount5_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank5"].ToString().Trim();
                txtFIRCADCode5_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank5"].ToString().Trim();
                ddlFIRCCurrency5_OB.SelectedValue = dt.Rows[0]["FIRCCurrency5_OB"].ToString().Trim();
                ddlFIRCRealisedCurr5_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr5_OB"].ToString().Trim();
                txtFIRCCrossCurrRate5_OB.Text = dt.Rows[0]["FIRCCrossCurrRate5_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB5_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB5_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo5_OB.Visible = false;
                txtFIRCDate5_OB.Visible = false;
                txtFIRCAmount5_OB.Visible = false;
                txtFIRCADCode5_OB.Visible = false;
                ddlFIRCCurrency5_OB.Visible = false;
                ddlFIRCRealisedCurr5_OB.Visible = false;
                txtFIRCCrossCurrRate5_OB.Visible = false;
                txtFIRCTobeAdjustedinSB5_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank6"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 155;
                txtFIRCNo6_OB.Text = dt.Rows[0]["FIRCNo_OtrBank6"].ToString().Trim();
                txtFIRCDate6_OB.Text = dt.Rows[0]["FIRCDate_OtrBank6"].ToString().Trim();
                txtFIRCAmount6_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank6"].ToString().Trim();
                txtFIRCADCode6_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank6"].ToString().Trim();
                ddlFIRCCurrency6_OB.SelectedValue = dt.Rows[0]["FIRCCurrency6_OB"].ToString().Trim();
                ddlFIRCRealisedCurr6_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr6_OB"].ToString().Trim();
                txtFIRCCrossCurrRate6_OB.Text = dt.Rows[0]["FIRCCrossCurrRate6_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB6_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB6_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo6_OB.Visible = false;
                txtFIRCDate6_OB.Visible = false;
                txtFIRCAmount6_OB.Visible = false;
                txtFIRCADCode6_OB.Visible = false;
                ddlFIRCCurrency6_OB.Visible = false;
                ddlFIRCRealisedCurr6_OB.Visible = false;
                txtFIRCCrossCurrRate6_OB.Visible = false;
                txtFIRCTobeAdjustedinSB6_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank7"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 177;
                txtFIRCNo7_OB.Text = dt.Rows[0]["FIRCNo_OtrBank7"].ToString().Trim();
                txtFIRCDate7_OB.Text = dt.Rows[0]["FIRCDate_OtrBank7"].ToString().Trim();
                txtFIRCAmount7_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank7"].ToString().Trim();
                txtFIRCADCode7_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank7"].ToString().Trim();
                ddlFIRCCurrency7_OB.SelectedValue = dt.Rows[0]["FIRCCurrency7_OB"].ToString().Trim();
                ddlFIRCRealisedCurr7_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr7_OB"].ToString().Trim();
                txtFIRCCrossCurrRate7_OB.Text = dt.Rows[0]["FIRCCrossCurrRate7_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB7_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB7_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo7_OB.Visible = false;
                txtFIRCDate7_OB.Visible = false;
                txtFIRCAmount7_OB.Visible = false;
                txtFIRCADCode7_OB.Visible = false;
                ddlFIRCCurrency7_OB.Visible = false;
                ddlFIRCRealisedCurr7_OB.Visible = false;
                txtFIRCCrossCurrRate7_OB.Visible = false;
                txtFIRCTobeAdjustedinSB7_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank8"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 202;
                txtFIRCNo8_OB.Text = dt.Rows[0]["FIRCNo_OtrBank8"].ToString().Trim();
                txtFIRCDate8_OB.Text = dt.Rows[0]["FIRCDate_OtrBank8"].ToString().Trim();
                txtFIRCAmount8_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank8"].ToString().Trim();
                txtFIRCADCode8_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank8"].ToString().Trim();
                ddlFIRCCurrency8_OB.SelectedValue = dt.Rows[0]["FIRCCurrency8_OB"].ToString().Trim();
                ddlFIRCRealisedCurr8_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr8_OB"].ToString().Trim();
                txtFIRCCrossCurrRate8_OB.Text = dt.Rows[0]["FIRCCrossCurrRate8_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB8_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB8_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo8_OB.Visible = false;
                txtFIRCDate8_OB.Visible = false;
                txtFIRCAmount8_OB.Visible = false;
                txtFIRCADCode8_OB.Visible = false;
                ddlFIRCCurrency8_OB.Visible = false;
                ddlFIRCRealisedCurr8_OB.Visible = false;
                txtFIRCCrossCurrRate8_OB.Visible = false;
                txtFIRCTobeAdjustedinSB8_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank9"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 224;
                txtFIRCNo9_OB.Text = dt.Rows[0]["FIRCNo_OtrBank9"].ToString().Trim();
                txtFIRCDate9_OB.Text = dt.Rows[0]["FIRCDate_OtrBank9"].ToString().Trim();
                txtFIRCAmount9_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank9"].ToString().Trim();
                txtFIRCADCode9_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank9"].ToString().Trim();
                ddlFIRCCurrency9_OB.SelectedValue = dt.Rows[0]["FIRCCurrency9_OB"].ToString().Trim();
                ddlFIRCRealisedCurr9_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr9_OB"].ToString().Trim();
                txtFIRCCrossCurrRate9_OB.Text = dt.Rows[0]["FIRCCrossCurrRate9_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB9_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB9_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo9_OB.Visible = false;
                txtFIRCDate9_OB.Visible = false;
                txtFIRCAmount9_OB.Visible = false;
                txtFIRCADCode9_OB.Visible = false;
                ddlFIRCCurrency9_OB.Visible = false;
                ddlFIRCRealisedCurr9_OB.Visible = false;
                txtFIRCCrossCurrRate9_OB.Visible = false;
                txtFIRCTobeAdjustedinSB9_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank10"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 246;
                txtFIRCNo10_OB.Text = dt.Rows[0]["FIRCNo_OtrBank10"].ToString().Trim();
                txtFIRCDate10_OB.Text = dt.Rows[0]["FIRCDate_OtrBank10"].ToString().Trim();
                txtFIRCAmount10_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank10"].ToString().Trim();
                txtFIRCADCode10_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank10"].ToString().Trim();
                ddlFIRCCurrency10_OB.SelectedValue = dt.Rows[0]["FIRCCurrency10_OB"].ToString().Trim();
                ddlFIRCRealisedCurr10_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr10_OB"].ToString().Trim();
                txtFIRCCrossCurrRate10_OB.Text = dt.Rows[0]["FIRCCrossCurrRate10_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB10_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB10_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo10_OB.Visible = false;
                txtFIRCDate10_OB.Visible = false;
                txtFIRCAmount10_OB.Visible = false;
                txtFIRCADCode10_OB.Visible = false;
                ddlFIRCCurrency10_OB.Visible = false;
                ddlFIRCRealisedCurr10_OB.Visible = false;
                txtFIRCCrossCurrRate10_OB.Visible = false;
                txtFIRCTobeAdjustedinSB10_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank11"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 270;
                txtFIRCNo11_OB.Text = dt.Rows[0]["FIRCNo_OtrBank11"].ToString().Trim();
                txtFIRCDate11_OB.Text = dt.Rows[0]["FIRCDate_OtrBank11"].ToString().Trim();
                txtFIRCAmount11_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank11"].ToString().Trim();
                txtFIRCADCode11_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank11"].ToString().Trim();
                ddlFIRCCurrency11_OB.SelectedValue = dt.Rows[0]["FIRCCurrency11_OB"].ToString().Trim();
                ddlFIRCRealisedCurr11_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr11_OB"].ToString().Trim();
                txtFIRCCrossCurrRate11_OB.Text = dt.Rows[0]["FIRCCrossCurrRate11_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB11_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB11_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo11_OB.Visible = false;
                txtFIRCDate11_OB.Visible = false;
                txtFIRCAmount11_OB.Visible = false;
                txtFIRCADCode11_OB.Visible = false;
                ddlFIRCCurrency11_OB.Visible = false;
                ddlFIRCRealisedCurr11_OB.Visible = false;
                txtFIRCCrossCurrRate11_OB.Visible = false;
                txtFIRCTobeAdjustedinSB11_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank12"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 292;
                txtFIRCNo12_OB.Text = dt.Rows[0]["FIRCNo_OtrBank12"].ToString().Trim();
                txtFIRCDate12_OB.Text = dt.Rows[0]["FIRCDate_OtrBank12"].ToString().Trim();
                txtFIRCAmount12_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank12"].ToString().Trim();
                txtFIRCADCode12_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank12"].ToString().Trim();
                ddlFIRCCurrency12_OB.SelectedValue = dt.Rows[0]["FIRCCurrency12_OB"].ToString().Trim();
                ddlFIRCRealisedCurr12_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr12_OB"].ToString().Trim();
                txtFIRCCrossCurrRate12_OB.Text = dt.Rows[0]["FIRCCrossCurrRate12_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB12_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB12_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo12_OB.Visible = false;
                txtFIRCDate12_OB.Visible = false;
                txtFIRCAmount12_OB.Visible = false;
                txtFIRCADCode12_OB.Visible = false;
                ddlFIRCCurrency12_OB.Visible = false;
                ddlFIRCRealisedCurr12_OB.Visible = false;
                txtFIRCCrossCurrRate12_OB.Visible = false;
                txtFIRCTobeAdjustedinSB12_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank13"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 314;
                txtFIRCNo13_OB.Text = dt.Rows[0]["FIRCNo_OtrBank13"].ToString().Trim();
                txtFIRCDate13_OB.Text = dt.Rows[0]["FIRCDate_OtrBank13"].ToString().Trim();
                txtFIRCAmount13_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank13"].ToString().Trim();
                txtFIRCADCode13_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank13"].ToString().Trim();
                ddlFIRCCurrency13_OB.SelectedValue = dt.Rows[0]["FIRCCurrency13_OB"].ToString().Trim();
                ddlFIRCRealisedCurr13_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr13_OB"].ToString().Trim();
                txtFIRCCrossCurrRate13_OB.Text = dt.Rows[0]["FIRCCrossCurrRate13_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB13_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB13_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo13_OB.Visible = false;
                txtFIRCDate13_OB.Visible = false;
                txtFIRCAmount13_OB.Visible = false;
                txtFIRCADCode13_OB.Visible = false;
                ddlFIRCCurrency13_OB.Visible = false;
                ddlFIRCRealisedCurr13_OB.Visible = false;
                txtFIRCCrossCurrRate13_OB.Visible = false;
                txtFIRCTobeAdjustedinSB13_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank14"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 338;
                txtFIRCNo14_OB.Text = dt.Rows[0]["FIRCNo_OtrBank14"].ToString().Trim();
                txtFIRCDate14_OB.Text = dt.Rows[0]["FIRCDate_OtrBank14"].ToString().Trim();
                txtFIRCAmount14_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank14"].ToString().Trim();
                txtFIRCADCode14_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank14"].ToString().Trim();
                ddlFIRCCurrency14_OB.SelectedValue = dt.Rows[0]["FIRCCurrency14_OB"].ToString().Trim();
                ddlFIRCRealisedCurr14_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr14_OB"].ToString().Trim();
                txtFIRCCrossCurrRate14_OB.Text = dt.Rows[0]["FIRCCrossCurrRate14_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB14_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB14_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo14_OB.Visible = false;
                txtFIRCDate14_OB.Visible = false;
                txtFIRCAmount14_OB.Visible = false;
                txtFIRCADCode14_OB.Visible = false;
                ddlFIRCCurrency14_OB.Visible = false;
                ddlFIRCRealisedCurr14_OB.Visible = false;
                txtFIRCCrossCurrRate14_OB.Visible = false;
                txtFIRCTobeAdjustedinSB14_OB.Visible = false;

            }
            if (dt.Rows[0]["FIRCNo_OtrBank15"].ToString().Trim() != "")
            {
                Cpe3.ExpandedSize = 365;
                txtFIRCNo15_OB.Text = dt.Rows[0]["FIRCNo_OtrBank15"].ToString().Trim();
                txtFIRCDate15_OB.Text = dt.Rows[0]["FIRCDate_OtrBank15"].ToString().Trim();
                txtFIRCAmount15_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank15"].ToString().Trim();
                txtFIRCADCode15_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank15"].ToString().Trim();
                ddlFIRCCurrency15_OB.SelectedValue = dt.Rows[0]["FIRCCurrency15_OB"].ToString().Trim();
                ddlFIRCRealisedCurr15_OB.SelectedValue = dt.Rows[0]["FIRCRealisedCurr15_OB"].ToString().Trim();
                txtFIRCCrossCurrRate15_OB.Text = dt.Rows[0]["FIRCCrossCurrRate15_OB"].ToString().Trim();
                txtFIRCTobeAdjustedinSB15_OB.Text = dt.Rows[0]["FIRCTobeAdjustedinSB15_OB"].ToString().Trim();
            }
            else
            {
                txtFIRCNo15_OB.Visible = false;
                txtFIRCDate15_OB.Visible = false;
                txtFIRCAmount15_OB.Visible = false;
                txtFIRCADCode15_OB.Visible = false;
                ddlFIRCCurrency15_OB.Visible = false;
                ddlFIRCRealisedCurr15_OB.Visible = false;
                txtFIRCCrossCurrRate15_OB.Visible = false;
                txtFIRCTobeAdjustedinSB15_OB.Visible = false;

            }



            //----------------------------------------------------End-------------------------------------------------------

            //txtTTRefNo1.Text = dt.Rows[0]["TTREFNO1"].ToString().Trim();
            //txtTTRefNo2.Text = dt.Rows[0]["TTREFNO2"].ToString().Trim();
            //txtTTRefNo3.Text = dt.Rows[0]["TTREFNO3"].ToString().Trim();
            //txtTTRefNo4.Text = dt.Rows[0]["TTREFNO4"].ToString().Trim();
            //txtTTRefNo5.Text = dt.Rows[0]["TTREFNO5"].ToString().Trim();
            //txtTTRefNo6.Text = dt.Rows[0]["TTREFNO6"].ToString().Trim();

            //txtTTRefNo7.Text = dt.Rows[0]["TTREFNO7"].ToString().Trim();
            //txtTTRefNo8.Text = dt.Rows[0]["TTREFNO8"].ToString().Trim();
            //txtTTRefNo9.Text = dt.Rows[0]["TTREFNO9"].ToString().Trim();
            //txtTTRefNo10.Text = dt.Rows[0]["TTREFNO10"].ToString().Trim();
            //txtTTRefNo11.Text = dt.Rows[0]["TTREFNO11"].ToString().Trim();
            //txtTTRefNo12.Text = dt.Rows[0]["TTREFNO12"].ToString().Trim();
            //txtTTRefNo13.Text = dt.Rows[0]["TTREFNO13"].ToString().Trim();
            //txtTTRefNo14.Text = dt.Rows[0]["TTREFNO14"].ToString().Trim();
            //txtTTRefNo15.Text = dt.Rows[0]["TTREFNO15"].ToString().Trim();

            //txtTTAmount1.Text = dt.Rows[0]["TTAmt1"].ToString().Trim();
            //txtTTAmount2.Text = dt.Rows[0]["TTAmt2"].ToString().Trim();
            //txtTTAmount3.Text = dt.Rows[0]["TTAmt3"].ToString().Trim();
            //txtTTAmount4.Text = dt.Rows[0]["TTAmt4"].ToString().Trim();
            //txtTTAmount5.Text = dt.Rows[0]["TTAmt5"].ToString().Trim();
            //txtTTAmount6.Text = dt.Rows[0]["TTAmt6"].ToString().Trim();
            //txtTTAmount7.Text = dt.Rows[0]["TTAmt7"].ToString().Trim();
            //txtTTAmount8.Text = dt.Rows[0]["TTAmt8"].ToString().Trim();
            //txtTTAmount9.Text = dt.Rows[0]["TTAmt9"].ToString().Trim();
            //txtTTAmount10.Text = dt.Rows[0]["TTAmt10"].ToString().Trim();
            //txtTTAmount11.Text = dt.Rows[0]["TTAmt11"].ToString().Trim();
            //txtTTAmount12.Text = dt.Rows[0]["TTAmt12"].ToString().Trim();
            //txtTTAmount13.Text = dt.Rows[0]["TTAmt13"].ToString().Trim();
            //txtTTAmount14.Text = dt.Rows[0]["TTAmt14"].ToString().Trim();
            //txtTTAmount15.Text = dt.Rows[0]["TTAmt15"].ToString().Trim();

            //txtFIRCNo1_OB.Text = dt.Rows[0]["FIRCNo_OtrBank1"].ToString().Trim();
            //txtFIRCDate1_OB.Text = dt.Rows[0]["FIRCDate_OtrBank1"].ToString().Trim();
            //txtFIRCAmount1_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank1"].ToString().Trim();
            //txtFIRCADCode1_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank1"].ToString().Trim();

            //txtFIRCNo2_OB.Text = dt.Rows[0]["FIRCNo_OtrBank2"].ToString().Trim();
            //txtFIRCDate2_OB.Text = dt.Rows[0]["FIRCDate_OtrBank2"].ToString().Trim();
            //txtFIRCAmount2_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank2"].ToString().Trim();
            //txtFIRCADCode2_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank2"].ToString().Trim();

            //txtFIRCNo3_OB.Text = dt.Rows[0]["FIRCNo_OtrBank3"].ToString().Trim();
            //txtFIRCDate3_OB.Text = dt.Rows[0]["FIRCDate_OtrBank3"].ToString().Trim();
            //txtFIRCAmount3_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank3"].ToString().Trim();
            //txtFIRCADCode3_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank3"].ToString().Trim();

            //txtFIRCNo4_OB.Text = dt.Rows[0]["FIRCNo_OtrBank4"].ToString().Trim();
            //txtFIRCDate4_OB.Text = dt.Rows[0]["FIRCDate_OtrBank4"].ToString().Trim();
            //txtFIRCAmount4_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank4"].ToString().Trim();
            //txtFIRCADCode4_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank4"].ToString().Trim();

            //txtFIRCNo5_OB.Text = dt.Rows[0]["FIRCNo_OtrBank5"].ToString().Trim();
            //txtFIRCDate5_OB.Text = dt.Rows[0]["FIRCDate_OtrBank5"].ToString().Trim();
            //txtFIRCAmount5_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank5"].ToString().Trim();
            //txtFIRCADCode5_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank5"].ToString().Trim();

            //txtFIRCNo6_OB.Text = dt.Rows[0]["FIRCNo_OtrBank6"].ToString().Trim();
            //txtFIRCDate6_OB.Text = dt.Rows[0]["FIRCDate_OtrBank6"].ToString().Trim();
            //txtFIRCAmount6_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank6"].ToString().Trim();
            //txtFIRCADCode6_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank6"].ToString().Trim();

            //txtFIRCNo7_OB.Text = dt.Rows[0]["FIRCNo_OtrBank7"].ToString().Trim();
            //txtFIRCDate7_OB.Text = dt.Rows[0]["FIRCDate_OtrBank7"].ToString().Trim();
            //txtFIRCAmount7_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank7"].ToString().Trim();
            //txtFIRCADCode7_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank7"].ToString().Trim();

            //txtFIRCNo8_OB.Text = dt.Rows[0]["FIRCNo_OtrBank8"].ToString().Trim();
            //txtFIRCDate8_OB.Text = dt.Rows[0]["FIRCDate_OtrBank8"].ToString().Trim();
            //txtFIRCAmount8_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank8"].ToString().Trim();
            //txtFIRCADCode8_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank8"].ToString().Trim();

            //txtFIRCNo9_OB.Text = dt.Rows[0]["FIRCNo_OtrBank9"].ToString().Trim();
            //txtFIRCDate9_OB.Text = dt.Rows[0]["FIRCDate_OtrBank9"].ToString().Trim();
            //txtFIRCAmount9_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank9"].ToString().Trim();
            //txtFIRCADCode9_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank9"].ToString().Trim();

            //txtFIRCNo10_OB.Text = dt.Rows[0]["FIRCNo_OtrBank10"].ToString().Trim();
            //txtFIRCDate10_OB.Text = dt.Rows[0]["FIRCDate_OtrBank10"].ToString().Trim();
            //txtFIRCAmount10_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank10"].ToString().Trim();
            //txtFIRCADCode10_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank10"].ToString().Trim();

            //txtFIRCNo11_OB.Text = dt.Rows[0]["FIRCNo_OtrBank11"].ToString().Trim();
            //txtFIRCDate11_OB.Text = dt.Rows[0]["FIRCDate_OtrBank11"].ToString().Trim();
            //txtFIRCAmount11_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank11"].ToString().Trim();
            //txtFIRCADCode11_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank11"].ToString().Trim();

            //txtFIRCNo12_OB.Text = dt.Rows[0]["FIRCNo_OtrBank12"].ToString().Trim();
            //txtFIRCDate12_OB.Text = dt.Rows[0]["FIRCDate_OtrBank12"].ToString().Trim();
            //txtFIRCAmount12_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank12"].ToString().Trim();
            //txtFIRCADCode12_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank12"].ToString().Trim();

            //txtFIRCNo13_OB.Text = dt.Rows[0]["FIRCNo_OtrBank13"].ToString().Trim();
            //txtFIRCDate13_OB.Text = dt.Rows[0]["FIRCDate_OtrBank13"].ToString().Trim();
            //txtFIRCAmount13_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank13"].ToString().Trim();
            //txtFIRCADCode13_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank13"].ToString().Trim();

            //txtFIRCNo14_OB.Text = dt.Rows[0]["FIRCNo_OtrBank14"].ToString().Trim();
            //txtFIRCDate14_OB.Text = dt.Rows[0]["FIRCDate_OtrBank14"].ToString().Trim();
            //txtFIRCAmount14_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank14"].ToString().Trim();
            //txtFIRCADCode14_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank14"].ToString().Trim();

            //txtFIRCNo15_OB.Text = dt.Rows[0]["FIRCNo_OtrBank15"].ToString().Trim();
            //txtFIRCDate15_OB.Text = dt.Rows[0]["FIRCDate_OtrBank15"].ToString().Trim();
            //txtFIRCAmount15_OB.Text = dt.Rows[0]["FIRCAmt_OtrBank15"].ToString().Trim();
            //txtFIRCADCode15_OB.Text = dt.Rows[0]["FIRCADCODE_OtrBank15"].ToString().Trim();


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
            //hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
            //if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            //{
            //    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            //}


            //------------------End Covering Schedule Details---------------------//

            ///--------------added by shailesh  save & draft Button  ------///
            //if (dt.Rows[0]["Save_Draft"].ToString() == "")
            //{
            //    btnSaveDraft.Visible = false;
            //}
            //else if (dt.Rows[0]["Save_Draft"].ToString() == "D")
            //{
            //    btnSaveDraft.Visible = true;
            //}
            ////////////////////////////////////////////////////////////
            //------------------GBASE Details---------------------//
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

            // GridViewGRPPCustomsDetails.Cells[0].ReadOnly = true;
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
                    //if (rowID <= dt.Rows.Count - 1)
                    //{
                    //    //  updateSrNo();
                    //    int i = 1;
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        row["SrNo"] = i;
                    //        i = i + 1;
                    //    }
                    //}
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
        fillCustomerCodeDescription();

    }
    protected void txtOverseasPartyID_TextChanged(object sender, EventArgs e)
    {
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

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        fillDetails_CopyFrom(txtCopyFromDocNo.Text);
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
            //txtDispBydefault.Text = dt.Rows[0]["DispByDefaltuValue"].ToString().Trim();

            if (ddlDispachInd.SelectedValue == "Dispatched directly by exporter")
            {
                ddlDispBydefault.SelectedValue = "Non-Dispatch";
            }
            else
            {
                ddlDispBydefault.SelectedValue = dt.Rows[0]["DispByDefaltuValue"].ToString().Trim();
            }

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

            //txtNotes.Text = dt.Rows[0]["Document_Remarks"].ToString().Trim();
            txtNotes.Text = " ";

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
            //TF_DATA objData = new TF_DATA();
            //SqlParameter p1 = new SqlParameter("@dispatchid", ddlDispachInd.SelectedValue);

            //string _query = "dispatch_thired";
            //DataTable dt;

            ////li.Value = "0";
            ////===================Account Type==========================//
            ////p1.Value = "Account Type";
            //dt = objData.getData(_query, p1);
            ddlDispBydefault.Items.Clear();
            ListItem li = new ListItem();
            li.Value = "Non-Dispatch";
            li.Text = "Non-Dispatch";
            //if (dt.Rows.Count > 0)
            //{
            //    li.Text = "--Select--";
            //    ddlDispBydefault.DataSource = dt.DefaultView;
            //    ddlDispBydefault.DataTextField = "Dispatch_Name";
            //    ddlDispBydefault.DataValueField = "Dispatch_Name";
            //    ddlDispBydefault.DataBind();
            //}
            //else
            //    li.Text = "No record(s) found";

            ddlDispBydefault.Items.Insert(0, li);

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
                lblLEI_CUST_Remark.Text = "Customer abbreviation Not Avalable in C-HUB.";
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
                if (dt.Rows[0]["Statuschk"].ToString() == "Grater")
                {
                    lblLEIExpiry_CUST_Remark.Text = dt.Rows[0]["LEI_Expiry_Date"].ToString() + "...Verified.";
                    lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Green;
                    hdncustleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_CUST_Remark.Text = dt.Rows[0]["LEI_Expiry_Date"].ToString() + "...Not Verified.";
                    lblLEIExpiry_CUST_Remark.ForeColor = System.Drawing.Color.Green;
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
                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI Number for this customer is not available.')", true);
                    lblLEI_Overseas_Remark.Text = " " + "...Not Verified.";
                    lblLEI_Overseas_Remark.ForeColor = System.Drawing.Color.Red;
                    hdnoverseaslei.Value = "";
                }
                else
                {
                    lblLEI_Overseas_Remark.Text = dt.Rows[0]["Party_LEI_No"].ToString() + "...Verified.";
                    //lblLEI_Remark.Visible = true;
                    hdnoverseaslei.Value = dt.Rows[0]["Party_LEI_No"].ToString();
                }
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
                if (dt.Rows[0]["Statuschk"].ToString() == "Grater")
                {
                    lblLEIExpiry_Overseas_Remark.Text = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString() + "...Verified.";
                    lblLEIExpiry_Overseas_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnoverseasleiexpiry.Value = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString();
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Overseas_Remark.Text = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString() + "...Not Verified.";
                    lblLEIExpiry_Overseas_Remark.ForeColor = System.Drawing.Color.Green;
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

                            //LEIverify.Text = "";
                            //if (Request.QueryString["mode"].ToString() == "add")
                            //{
                            //    LEIverify.Text = "Please Verify LEI.";
                            //    LEIverify.ForeColor = System.Drawing.Color.Red;
                            //}
                            //else if (Request.QueryString["mode"].ToString() == "edit")
                            //{
                            //    if (Request.QueryString["Status"].ToString() != "Approved By Checker")
                            //    {
                            //        LEIverify.Text = "Please Verify LEI.";
                            //        LEIverify.ForeColor = System.Drawing.Color.Red;
                            //    }
                            //    else
                            //    {
                            //        LEIverify.Text = "LEI Verified.";
                            //        LEIverify.ForeColor = System.Drawing.Color.Green;
                            //    }
                            //    Leicount = Leicount + 1;
                            //}

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
            //hdnBranchCode.Value = Request.QueryString["BranchCode"].Trim();
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

                //if (Request.QueryString["mode"].ToString() == "add")
                //{
                //    if (hdnLeiFlag.Value == "N")
                //    {
                //        LEIverify.Text = "";
                //        LEIverify.Text = "Please Verify LEI.";
                //        LEIverify.ForeColor = System.Drawing.Color.Red;
                //    }
                //}
                //else if (Request.QueryString["mode"].ToString() == "edit")
                //{
                //    if (hdnLeiFlag.Value == "N")
                //    {
                //        if (Request.QueryString["Status"].ToString() != "Approved By Checker")
                //        {
                //            LEIverify.Text = "";
                //            LEIverify.Text = "Please Verify LEI.";
                //            LEIverify.ForeColor = System.Drawing.Color.Red;
                //            btnLEI.Visible = true;
                //        }
                //        else
                //        {
                //            if (Leicount == 0)
                //            {
                //                LEIverify.Text = "";
                //                LEIverify.Text = "LEI Verified.";
                //                LEIverify.ForeColor = System.Drawing.Color.Green;
                //            }
                //            else
                //            {
                //                LEIverify.Text = "";
                //                LEIverify.Text = "Please Verify LEI.";
                //                LEIverify.ForeColor = System.Drawing.Color.Red;
                //                btnLEI.Visible = true;
                //            }
                //        }
                //        Leicount = Leicount + 1;
                //    }
                //}
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

    protected void TTRemitterName(string TTno, string LblRname)
    {

        string _custAcNo = txtCustAcNo.Text.Trim();
        string bcode = hdnBranchCode.Value.ToString();

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
        Check_LEI_ThresholdLimit();
    }

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
            if (lblConsigneePartyDesc.Text.Length > 20)
            {
                lblConsigneePartyDesc.ToolTip = lblConsigneePartyDesc.Text;
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
}