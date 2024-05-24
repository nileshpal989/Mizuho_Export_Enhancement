function R42_Amt_Calculation() {
    var _txtDraftAmt = $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val();
    var _txtInterestAmt = $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val();
    var DiscrepancyChrg = 0, hdnTaxChrg = 0, totalamt = 0;

    if ($.isNumeric($("#hdnDiscrepancyChrg").val())) {
        DiscrepancyChrg = $("#hdnDiscrepancyChrg").val();
    }
    else {
        alert('Please Check Discrepancy Charges for Bill Currency.');
    }
    if ($.isNumeric($("#hdnTaxChrg").val())) {
        hdnTaxChrg = $("#hdnTaxChrg").val();
    }
    else {
        alert('Please Check Tax amount.');
    }

    var tax = parseFloat(DiscrepancyChrg * (hdnTaxChrg / 100));
    if ($("#TabContainerMain_tbDocumentDetails_rdbIDOA").is(':checked')) {
        totalamt = parseFloat(_txtDraftAmt);
    }
    if ($("#TabContainerMain_tbDocumentDetails_rdbIDOB").is(':checked')) {
        totalamt = parseFloat(_txtDraftAmt) - parseFloat(_txtInterestAmt) - parseFloat(parseFloat(DiscrepancyChrg) + parseFloat(tax));
    }
    if ($("#TabContainerMain_tbDocumentDetails_rdbIOBDOA").is(':checked')) {
        totalamt = parseFloat(_txtDraftAmt) - parseFloat(_txtInterestAmt);
    }
    if ($("#TabContainerMain_tbDocumentDetails_rdbIOADOB").is(':checked')) {
        totalamt = parseFloat(_txtDraftAmt) - parseFloat(parseFloat(DiscrepancyChrg) + parseFloat(tax));
    }
    if ($.isNumeric(totalamt)) {
        $("#TabContainerMain_tblR42format_txt_R42_Amt_4488").val(totalamt);
        if ($("#TabContainerMain_tbDocumentGOBranch_cb_GOBranch_Bill_Handling_Flag").is(':checked')) {
            $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Amt").val(totalamt);
            $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Amt").val(totalamt);
        }
    }
}
function TogggleDebitCreditCode(GO_No, DebitCredit_No) {
    if ($("#TabContainerMain_tbDocumentGOBranch_cb_GOBranch_Bill_Handling_Flag").is(':checked')) {
        if (GO_No == 'BR') {
            if (DebitCredit_No == '1') {
                if ($("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Curr").val('');
                }
            }
        }
    }
    if (GO_No == '1') {
        if (DebitCredit_No == '1') {
            if ($("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Code").val() != "") {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Curr").val('INR');
            }
            else {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Curr").val('');
            }
        }
        if (DebitCredit_No == '2') {
            if ($("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Code").val() != "") {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Curr").val('INR');
            }
            else {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Curr").val('');
            }
        }
    }
}
function OpenCR_Code_help(e, IMP_ACC, Debit_Credit) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_SundryaccountHelp.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
        return false;
    }
}
function select_CR_Code3(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val(ACcode);
    //$("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val(Acno);
}
function select_DR_Code3(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val(ACcode);
    //$("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val(Acno);
}
function select_CR_Code1(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AccNo").val(Acno);
}
function select_DR_Code1(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AccNo").val(Acno);
}
function select_CR_Code2(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AcCode").val(ACcode);
    //    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AccNo").val(Acno);
}
function select_DR_Code2(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AcCode").val(ACcode);
    //    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AccNo").val(Acno);
}
function select_GO4_CR_Code(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Cust_AccNo").val(Acno);
}
function select_GO4_DR_Code(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Cust_AccNo").val(Acno);
}
function Toggel_Bill_Amt() {
    var _txtDraftAmt = $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val();
    $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val(_txtDraftAmt);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_amt").val(_txtDraftAmt);
    $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Cur_Acc_amt").val(_txtDraftAmt);

    R42_Amt_Calculation();
}
function ONINTERESTAMT() {
    var txtInterestAmt = $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val();
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_amt").val(txtInterestAmt);
    R42_Amt_Calculation();
}
function OnDocNextClick(index) {
    SaveUpdateData();

    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    Validate();
    return false;
}

function Toggel_IMP_ACC_Coll_Comm() {
    var _txt_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_amt").val();
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val(_txt_CR_Accept_Commission_amt);
}
function Toggel_Amt(id) {
    id = '#' + id;
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };


    if ($(id).val() == '' || $(id).val() == 0) {
        $(id).val(0);
    }

    if ($("#lblDoc_Curr").text() == 'JPY') {
        $(id).val(Math.trunc($(id).val()));
    }
    else {
        $(id).val(parseFloat($(id).val()).toFixed(2));
    }
}


function validate_Commission_MATU_Code(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 49 && charCode != 50 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}

function validate_Commission_LUMP_Code(evnt) {

    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 89 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}
function validate_Commission_PAYER_Code(evnt) {

    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 66 && charCode != 83 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}

function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}

function SubmitCheck() {
    if (confirm('Are you sure you want to Submit this record to checker?')) {
        return true;
    }
    else
        return false;
}

function isValidDate(controlID, CName) {
    var obj = controlID;
    if (controlID.value != "__/__/____") {
        var day = obj.value.split("/")[0];
        var month = obj.value.split("/")[1];
        var year = obj.value.split("/")[2];
        var today = new Date();
        if (day == "__") {
            day = today.getDay();
        }
        if (month == "__") {
            month = today.getMonth() + 1;
        }
        if (year == "____") {
            year = today.getFullYear();
        }
        var dt = new Date(year, month - 1, day);
        if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
            alert('Invalid ' + CName);
            controlID.focus();
            return false;
        }
        else if (CName == 'Received Date') {
            var txtDateReceived = $("#txtDateReceived").val();
            var txtLogdmentDate = $("#txtLogdmentDate").val();
            var _txtDateReceived = new Date(txtDateReceived);
            var _txtLogdmentDate = new Date(txtLogdmentDate);
            if (_txtDateReceived > _txtLogdmentDate) {
                alert("Received Date Should not be greater than Lodgment Date.");
                $("#txtDateReceived").focus();
                return false;
            }
        }
    }
}
function checkSystemDate(controlID) {
    var obj = controlID;
    if (controlID.value != "__/__/____") {
        var day = obj.value.split("/")[0];
        var month = obj.value.split("/")[1];
        var year = obj.value.split("/")[2];

        var dt = new Date(year, month - 1, day);
        var today = new Date();
        if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {
            alert("Invalid Date");
            controlID.focus();
            return false;
        }
    }
}

function SaveUpdateData() {
    // Document Details
    var _BranchName = $("#hdnBranchName").val();
    var _hdnUserName = $("#hdnUserName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _txtIBDDocNo = $("#txtIBDDocNo").val();

    var _hdnIBDExtnFlag = $("#hdnIBDExtnFlag").val();
    var _txtIBDExtnNo = '';
    if (_hdnIBDExtnFlag == 'Y') {
        _txtIBDExtnNo = $("#txtIBDExtnNo").val();
    }

    var _txtValueDate = $("#txtValueDate").val();
    var _Document_Curr = $("#lblDoc_Curr").text();

    //---------------------Document Details---------------------------------------------------------------
    var _txtDraftAmt = $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val();
    var _txtIBDAmt = $("#TabContainerMain_tbDocumentDetails_txtIBDAmt").val();
    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val();

    var _txtCustName = $("#TabContainerMain_tbDocumentDetails_txtCustName").val();
    var _txtHO_Apl = $("#TabContainerMain_tbDocumentDetails_txtHO_Apl").val();
    var _txtIBD_ACC_kind = $("#TabContainerMain_tbDocumentDetails_txtIBD_ACC_kind").val();
    var _txtvalueDate1 = $("#TabContainerMain_tbDocumentDetails_txtvalueDate1").val();
    var _applibeniStatus = '';
    if ($("#TabContainerMain_tbDocumentDetails_rdbIDOA").is(':checked')) {
        _applibeniStatus = 'IDA';
    }
    else if ($("#TabContainerMain_tbDocumentDetails_rdbIDOB").is(':checked')) {

        _applibeniStatus = 'IDB';
    }
    else if ($("#TabContainerMain_tbDocumentDetails_rdbIOBDOA").is(':checked')) {

        _applibeniStatus = 'IBDA';
    }
    else if ($("#TabContainerMain_tbDocumentDetails_rdbIOADOB").is(':checked')) {

        _applibeniStatus = 'IADB';
    }
    var _txtCommentCode = $("#TabContainerMain_tbDocumentDetails_txtCommentCode").val();
    var _txtAutoSettlement = $("#TabContainerMain_tbDocumentDetails_txtAutoSettlement").val();
    var _txtLCNo = $("#TabContainerMain_tbDocumentDetails_txtLCNo").val();
    var _txtContractNo = $("#TabContainerMain_tbDocumentDetails_txtContractNo").val();
    var _txtExchRate = $("#TabContainerMain_tbDocumentDetails_txtExchRate").val();

    //    var _txtIBDAmt = $("#TabContainerMain_tbDocumentDetails_txtExchRate").val();
    var _txtCountryRisk = $("#TabContainerMain_tbDocumentDetails_txtCountryRisk").val();
    var _txtRiskCust = $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val();
    var _txtGradeCode = $("#TabContainerMain_tbDocumentDetails_txtGradeCode").val();

    var _txtApplNo = $("#TabContainerMain_tbDocumentDetails_txtApplNo").val();
    var _txtApplBR = $("#TabContainerMain_tbDocumentDetails_txtApplBR").val();
    var _txtPurpose = $("#TabContainerMain_tbDocumentDetails_txtPurpose").val();
    var _ddl_PurposeCode = $("#TabContainerMain_tbDocumentDetails_ddl_PurposeCode").val();

    var _txtsettlCodeForCust = $("#TabContainerMain_tbDocumentDetails_txtsettlCodeForCust").val();
    var _txtsettlforCust_Abbr = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_Abbr").val();
    var _txtsettlforCust_AccCode = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_AccCode").val();
    var _txtsettlforCust_AccNo = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_AccNo").val();

    var _txtInterest_From = $("#TabContainerMain_tbDocumentDetails_txtInterest_From").val();
    var _txtInterest_To = $("#TabContainerMain_tbDocumentDetails_txtInterest_To").val();
    var _txt_No_Of_Days = $("#TabContainerMain_tbDocumentDetails_txt_No_Of_Days").val();

    var _txt_INT_Rate = $("#TabContainerMain_tbDocumentDetails_txt_INT_Rate").val();
    var _txtBaseRate = $("#TabContainerMain_tbDocumentDetails_txtBaseRate").val();
    var _txtSpread = $("#TabContainerMain_tbDocumentDetails_txtSpread").val();

    var _txtInterestAmt = $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val();
    var _txtFundType = $("#TabContainerMain_tbDocumentDetails_txtFundType").val();
    var _txtInternalRate = $("#TabContainerMain_tbDocumentDetails_txtInternalRate").val();

    var _txtSettl_CodeForBank = $("#TabContainerMain_tbDocumentDetails_txtSettl_CodeForBank").val();
    var _txtSettl_ForBank_Abbr = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_Abbr").val();
    var _txtSettl_ForBank_AccCode = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccCode").val();
    var _txtSettl_ForBank_AccNo = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccNo").val();

    var _txtREM_EUC = $("#TabContainerMain_tbDocumentDetails_txtREM_EUC").val();
    var _txtAttn = $("#TabContainerMain_tbDocumentDetails_txtAttn").val();


    // Instruction
    var _txt_INST_Code = $("#TabContainerMain_tbDocumentInstructions_txt_INST_Code").val();
    var _lbl_Instructions1 = "KINDLY RETURN ATTACHED TRUST RECEIPT OFFICIALLY SIGNED.";

    //    // Import Accounting
    var _txt_IMP_ACC_ExchRate = $("#TabContainerMain_tbDocumentAccounting_txt_IMP_ACC_ExchRate").val();
    var _txtPrinc_matu = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_matu").val();
    var _txtInterest_matu = $("#TabContainerMain_tbDocumentAccounting_txtInterest_matu").val();
    var _txtCommission_matu = $("#TabContainerMain_tbDocumentAccounting_txtCommission_matu").val();
    var _txtTheir_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_matu").val();

    var _txtPrinc_lump = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_lump").val();
    var _txtInterest_lump = $("#TabContainerMain_tbDocumentAccounting_txtInterest_lump").val();
    var _txtCommission_lump = $("#TabContainerMain_tbDocumentAccounting_txtCommission_lump").val();
    var _txtTheir_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_lump").val();

    var _txtprinc_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Contract_no").val();
    var _txtInterest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Contract_no").val();
    var _txtCommission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Contract_no").val();
    var _txtTheir_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Contract_no").val();

    var _txtprinc_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Ex_rate").val();
    var _txtInterest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Ex_rate").val();
    var _txtCommission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Ex_rate").val();
    var _txtTheir_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Ex_rate").val();

    var _txtprinc_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Intnl_Ex_rate").val();
    var _txtInterest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Intnl_Ex_rate").val();
    var _txtCommission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Intnl_Ex_rate").val();
    var _txtTheir_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Intnl_Ex_rate").val();

    var _txt_CR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val();
    var _txt_CR_AC_ShortName = $("#TabContainerMain_tbDocumentAccounting_txt_CR_AC_ShortName").val();
    var _txt_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val();
    var _txt_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val();

    var _txt_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_amt").val();
    var _txt_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_amt").val();
    var _txt_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_amt").val();
    var _txt_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_amt").val();
    var _txt_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_amt").val();
    var _txt_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_amt").val();

    var _txt_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Princ_Ex_Curr").val();
    var _txt_interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_interest_Ex_Curr").val();
    var _txt_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Commission_Ex_Curr").val();
    var _txt_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Their_Commission_Ex_Curr").val();

    var _txt_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_Curr").val();
    var _txt_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_Curr").val();
    var _txt_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Curr").val();
    var _txt_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Curr").val();
    var _txt_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Curr").val();
    var _txt_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Curr").val();
    var _txt_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr2").val();
    var _txt_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr3").val();


    var _txt_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_payer").val();
    var _txt_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_payer").val();
    var _txt_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Payer").val();
    var _txt_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Payer").val();
    var _txt_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Payer").val();
    var _txt_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Payer").val();


    var _txt_IBD_DR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Code").val();
    //    var _txt_IBD_DR_AC_ShortName = $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_AC_ShortName").val();
    var _txt_IBD_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Cust_abbr").val();
    var _txt_IBD_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Cust_Acc").val();
    var _txt_IBD_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Cur_Acc_Curr").val();
    var _txt_IBD_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Cur_Acc_amt").val();
    var _txt_IBD_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Cur_Acc_payer").val();

    var _txt_DR_Code = '', _txt_DR_Cust_abbr = '', _txt_DR_Cust_Acc = '', _txt_DR_Cur_Acc_Curr = '', _txt_DR_Cur_Acc_amt = 0, _txt_DR_Cur_Acc_payer = '';
    if ($("#TabContainerMain_tbDocumentAccounting_txtInterest_matu").val() == 1) {
        _txt_DR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val();
        _txt_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val();
        _txt_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val();
        _txt_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr").val();
        _txt_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val();
        _txt_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer").val();

    }
    var _txt_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt2").val();
    var _txt_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt3").val();
    var _txt_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer2").val();
    var _txt_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer3").val();

    //------------------------------Genral operation Branch-------------------------------------------

    var _cb_GOBranch_Bill_Handling_Flag = '',
    _txt_GOBR_Ref_No='',
    _txt_GOBR_Comment = '', _txt_GOBR_SectionNo = '', _txt_GOBR_Remarks = '', _txt_GOBR_Memo = '',
    _txt_GOBR_Scheme_no = '', _txt_GOBR_Debit_Code = '', _txt_GOBR_Debit_Curr = '', _txt_GOBR_Debit_Amt = 0,
     _txt_GOBR_Debit_Cust = '',
    _txt_GOBR_Debit_Cust_AcCode = '', _txt_GOBR_Debit_Cust_AccNo = '',
    _txt_GOBR_Debit_Exch_Rate = '', _txt_GOBR_Debit_Exch_CCY = '',
     _txt_GOBR_Debit_FUND = '', _txt_GOBR_Debit_Check_No = '', _txt_GOBR_Debit_Available = '',
     _txt_GOBR_Debit_AdPrint = '', _txt_GOBR_Debit_Details = '', _txt_GOBR_Debit_Entity = '',
     _txt_GOBR_Debit_Division = '', _txt_GOBR_Debit_Inter_Amount = '', _txt_GOBR_Debit_Inter_Rate = '',
     _txt_GOBR_Credit_Code = '', _txt_GOBR_Credit_Curr = '', _txt_GOBR_Credit_Amt = '',
     _txt_GOBR_Credit_Cust = '',
     _txt_GOBR_Credit_Cust_AcCode = '', _txt_GOBR_Credit_Cust_AccNo = '',
     _txt_GOBR_Credit_Exch_Rate = '', _txt_GOBR_Credit_Exch_Curr = '',
     _txt_GOBR_Credit_FUND = '', _txt_GOBR_Credit_Check_No = '', _txt_GOBR_Credit_Available = '',
     _txt_GOBR_Credit_AdPrint = '', _txt_GOBR_Credit_Details = '', _txt_GOBR_Credit_Entity = '',
     _txt_GOBR_Credit_Division = '', _txt_GOBR_Credit_Inter_Amount = '', _txt_GOBR_Credit_Inter_Rate = '';
    if (_BranchName != 'Mumbai') {
        if ($("#TabContainerMain_tbDocumentGOBranch_cb_GOBranch_Bill_Handling_Flag").is(':checked')) {
            _cb_GOBranch_Bill_Handling_Flag = 'Y';
            _txt_GOBR_Ref_No = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Ref_No").val();
            _txt_GOBR_Comment = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Comment").val();
            _txt_GOBR_SectionNo = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_SectionNo").val();
            _txt_GOBR_Remarks = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Remarks").val();
            _txt_GOBR_Memo = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Memo").val();
            _txt_GOBR_Scheme_no = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Scheme_no").val();
            _txt_GOBR_Debit_Code = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Code").val();
            _txt_GOBR_Debit_Curr = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Curr").val();
            _txt_GOBR_Debit_Amt = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Amt").val();
            _txt_GOBR_Debit_Cust = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Cust").val();

            _txt_GOBR_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Cust_AcCode").val();

            _txt_GOBR_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Cust_AccNo").val();
            _txt_GOBR_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Exch_Rate").val();
            _txt_GOBR_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Exch_CCY").val();
            _txt_GOBR_Debit_FUND = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_FUND").val();
            _txt_GOBR_Debit_Check_No = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Check_No").val();
            _txt_GOBR_Debit_Available = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Available").val();
            _txt_GOBR_Debit_AdPrint = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_AdPrint").val();
            _txt_GOBR_Debit_Details = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Details").val();
            _txt_GOBR_Debit_Entity = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Entity").val();
            _txt_GOBR_Debit_Division = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Division").val();
            _txt_GOBR_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Inter_Amount").val();
            _txt_GOBR_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Inter_Rate").val();
            _txt_GOBR_Credit_Code = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Code").val();
            _txt_GOBR_Credit_Curr = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Curr").val();
            _txt_GOBR_Credit_Amt = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Amt").val();
            _txt_GOBR_Credit_Cust = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Cust").val();

            _txt_GOBR_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Cust_AcCode").val();

            _txt_GOBR_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Cust_AccNo").val();
            _txt_GOBR_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Exch_Rate").val();
            _txt_GOBR_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Exch_Curr").val();
            _txt_GOBR_Credit_FUND = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_FUND").val();
            _txt_GOBR_Credit_Check_No = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Check_No").val();
            _txt_GOBR_Credit_Available = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Available").val();
            _txt_GOBR_Credit_AdPrint = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_AdPrint").val();
            _txt_GOBR_Credit_Details = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Details").val();
            _txt_GOBR_Credit_Entity = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Entity").val();
            _txt_GOBR_Credit_Division = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Division").val();
            _txt_GOBR_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Inter_Amount").val();
            _txt_GOBR_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Inter_Rate").val();
        }
    }

    //--------------------Genral Operation I----------------------------------------------
    var _chk_GO1_Flag = 'N',
     _txt_GO1_Comment = '', _txt_GO1_SectionNo = '', _txt_GO1_Remarks = '', _txt_GO1_Memo = '',
     _txt_GO1_Scheme_no = '', _txt_GO1_Debit_Code = '', _txt_GO1_Debit_Curr = '', _txt_GO1_Debit_Amt = '', _txt_GO1_Debit_Cust = '', _txt_GO1_Debit_Cust_AcCode = '',
     _txt_GO1_Debit_Cust_AccNo = '', _txt_GO1_Debit_Exch_Rate = '', _txt_GO1_Debit_Exch_CCY = '', _txt_GO1_Debit_FUND = '', _txt_GO1_Debit_Check_No = '',
     _txt_GO1_Debit_Available = '', _txt_GO1_Debit_AdPrint = '', _txt_GO1_Debit_Details = '', _txt_GO1_Debit_Entity = '', _txt_GO1_Debit_Division = '',
     _txt_GO1_Debit_Inter_Amount = '', _txt_GO1_Debit_Inter_Rate = '', _txt_GO1_Credit_Code = '', _txt_GO1_Credit_Curr = '', _txt_GO1_Credit_Amt = '',
     _txt_GO1_Credit_Cust = '', _txt_GO1_Credit_Cust_AcCode = '', _txt_GO1_Credit_Cust_AccNo = '', _txt_GO1_Credit_Exch_Rate = '', _txt_GO1_Credit_Exch_Curr = '',
     _txt_GO1_Credit_FUND = '', _txt_GO1_Credit_Check_No = '', _txt_GO1_Credit_Available = '', _txt_GO1_Credit_AdPrint = '', _txt_GO1_Credit_Details = '',
     _txt_GO1_Credit_Entity = '', _txt_GO1_Credit_Division = '', _txt_GO1_Credit_Inter_Amount = '', _txt_GO1_Credit_Inter_Rate = '';

    if ($("#TabContainerMain_tbDocumentGO1_chk_GO1_Flag").is(':checked')) {
        _chk_GO1_Flag = 'Y';
        _txt_GO1_Comment = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Comment").val();
        _txt_GO1_SectionNo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_SectionNo").val();
        _txt_GO1_Remarks = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Remarks").val();
        _txt_GO1_Memo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Memo").val();
        _txt_GO1_Scheme_no = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Scheme_no").val();
        _txt_GO1_Debit_Code = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Code").val();
        _txt_GO1_Debit_Curr = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Curr").val();
        _txt_GO1_Debit_Amt = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Amt").val();
        _txt_GO1_Debit_Cust = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust").val();

        _txt_GO1_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode").val();

        _txt_GO1_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AccNo").val();
        _txt_GO1_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Exch_Rate").val();
        _txt_GO1_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Exch_CCY").val();
        _txt_GO1_Debit_FUND = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_FUND").val();
        _txt_GO1_Debit_Check_No = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Check_No").val();
        _txt_GO1_Debit_Available = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Available").val();
        _txt_GO1_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_AdPrint").val();
        _txt_GO1_Debit_Details = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Details").val();
        _txt_GO1_Debit_Entity = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Entity").val();
        _txt_GO1_Debit_Division = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Division").val();
        _txt_GO1_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Inter_Amount").val();
        _txt_GO1_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Inter_Rate").val();
        _txt_GO1_Credit_Code = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Code").val();
        _txt_GO1_Credit_Curr = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Curr").val();
        _txt_GO1_Credit_Amt = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Amt").val();
        _txt_GO1_Credit_Cust = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust").val();

        _txt_GO1_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode").val();

        _txt_GO1_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AccNo").val();
        _txt_GO1_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Exch_Rate").val();
        _txt_GO1_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Exch_Curr").val();
        _txt_GO1_Credit_FUND = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_FUND").val();
        _txt_GO1_Credit_Check_No = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Check_No").val();
        _txt_GO1_Credit_Available = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Available").val();
        _txt_GO1_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_AdPrint").val();
        _txt_GO1_Credit_Details = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Details").val();
        _txt_GO1_Credit_Entity = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Entity").val();
        _txt_GO1_Credit_Division = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Division").val();
        _txt_GO1_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Inter_Amount").val();
        _txt_GO1_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Inter_Rate").val();
    }

    //--------------------------------------GO2---------------------------------------------------------
    var _chk_GO2_Flag = 'N',
    _txt_GO2_Comment = '', _txt_GO2_SectionNo = '', _txt_GO2_Remarks = '', _txt_GO2_Memo = '',
    _txt_GO2_Scheme_no = '', _txt_GO2_Debit_Code = '', _txt_GO2_Debit_Curr = '', _txt_GO2_Debit_Amt = 0,
     _txt_GO2_Debit_Cust = '',
    _txt_GO2_Debit_Cust_AcCode = '', _txt_GO2_Debit_Cust_AccNo = '',
    _txt_GO2_Debit_Exch_Rate = '', _txt_GO2_Debit_Exch_CCY = '',
     _txt_GO2_Debit_FUND = '', _txt_GO2_Debit_Check_No = '', _txt_GO2_Debit_Available = '',
     _txt_GO2_Debit_AdPrint = '', _txt_GO2_Debit_Details = '', _txt_GO2_Debit_Entity = '',
     _txt_GO2_Debit_Division = '', _txt_GO2_Debit_Inter_Amount = '', _txt_GO2_Debit_Inter_Rate = '',
     _txt_GO2_Credit_Code = '', _txt_GO2_Credit_Curr = '', _txt_GO2_Credit_Amt = '',
     _txt_GO2_Credit_Cust = '',
     _txt_GO2_Credit_Cust_AcCode = '', _txt_GO2_Credit_Cust_AccNo = '',
     _txt_GO2_Credit_Exch_Rate = '', _txt_GO2_Credit_Exch_Curr = '',
     _txt_GO2_Credit_FUND = '', _txt_GO2_Credit_Check_No = '', _txt_GO2_Credit_Available = '',
     _txt_GO2_Credit_AdPrint = '', _txt_GO2_Credit_Details = '', _txt_GO2_Credit_Entity = '',
     _txt_GO2_Credit_Division = '', _txt_GO2_Credit_Inter_Amount = '', _txt_GO2_Credit_Inter_Rate = '';



    if ($("#TabContainerMain_tbDocumentGO2_chk_GO2_Flag").is(':checked')) {
        _chk_GO2_Flag = 'Y';
        _txt_GO2_Comment = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Comment").val();
        _txt_GO2_SectionNo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_SectionNo").val();
        _txt_GO2_Remarks = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Remarks").val();
        _txt_GO2_Memo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Memo").val();
        _txt_GO2_Scheme_no = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Scheme_no").val();
        _txt_GO2_Debit_Code = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Code").val();
        _txt_GO2_Debit_Curr = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Curr").val();
        _txt_GO2_Debit_Amt = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Amt").val();
        _txt_GO2_Debit_Cust = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust").val();

        _txt_GO2_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AcCode").val();

        _txt_GO2_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AccNo").val();
        _txt_GO2_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Exch_Rate").val();
        _txt_GO2_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Exch_CCY").val();
        _txt_GO2_Debit_FUND = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_FUND").val();
        _txt_GO2_Debit_Check_No = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Check_No").val();
        _txt_GO2_Debit_Available = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Available").val();
        _txt_GO2_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_AdPrint").val();
        _txt_GO2_Debit_Details = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Details").val();
        _txt_GO2_Debit_Entity = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Entity").val();
        _txt_GO2_Debit_Division = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Division").val();
        _txt_GO2_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Inter_Amount").val();
        _txt_GO2_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Inter_Rate").val();
        _txt_GO2_Credit_Code = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Code").val();
        _txt_GO2_Credit_Curr = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Curr").val();
        _txt_GO2_Credit_Amt = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Amt").val();
        _txt_GO2_Credit_Cust = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust").val();

        _txt_GO2_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AcCode").val();

        _txt_GO2_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AccNo").val();
        _txt_GO2_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Exch_Rate").val();
        _txt_GO2_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Exch_Curr").val();
        _txt_GO2_Credit_FUND = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_FUND").val();
        _txt_GO2_Credit_Check_No = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Check_No").val();
        _txt_GO2_Credit_Available = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Available").val();
        _txt_GO2_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_AdPrint").val();
        _txt_GO2_Credit_Details = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Details").val();
        _txt_GO2_Credit_Entity = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Entity").val();
        _txt_GO2_Credit_Division = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Division").val();
        _txt_GO2_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Inter_Amount").val();
        _txt_GO2_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Inter_Rate").val();
    }

    //-------------------------------R42 Format for IBD------------------------------------------------------------------
   var _txt_R42_tansactionRefNO = '', _txt_R42_RelatedRef = '', _txt_R42_ValueDate_4488 = '', _txt_R42_Curr_4488 = '', _txt_R42_Amt_4488 = '',
    _txt_R42_Orderingins_IFSC_5517 = '', _txt_R42_Benificiary_IFSC_6521 = '',
    _txt_R42_CodeWord_7495 = '', _txt_R42_AddInfo_7495 = '', _txt_R42_MoreInfo_7495 = '', _txt_R42_MoreInfo2_7495 = '', _txt_R42_MoreInfo3_7495 = '',
    _txt_R42_MoreInfo4_7495 = '', _txt_R42_MoreInfo5_7495 = '';
   if (_hdnIBDExtnFlag != 'Y') {
       _txt_R42_tansactionRefNO = $("#TabContainerMain_tblR42format_txt_R42_tansactionRefNO").val();
       _txt_R42_RelatedRef = $("#TabContainerMain_tblR42format_txt_R42_RelatedRef").val();
       _txt_R42_ValueDate_4488 = $("#TabContainerMain_tblR42format_txt_R42_ValueDate_4488").val();
       _txt_R42_Curr_4488 = $("#TabContainerMain_tblR42format_txt_R42_Curr_4488").val();
       _txt_R42_Amt_4488 = $("#TabContainerMain_tblR42format_txt_R42_Amt_4488").val();
       _txt_R42_Orderingins_IFSC_5517 = $("#TabContainerMain_tblR42format_txt_R42_Orderingins_IFSC_5517").val();
       _txt_R42_Benificiary_IFSC_6521 = $("#TabContainerMain_tblR42format_txt_R42_Benificiary_IFSC_6521").val();
       _txt_R42_CodeWord_7495 = $("#TabContainerMain_tblR42format_txt_R42_CodeWord_7495").val();
       _txt_R42_AddInfo_7495 = $("#TabContainerMain_tblR42format_txt_R42_AddInfo_7495").val();
       _txt_R42_MoreInfo_7495 = $("#TabContainerMain_tblR42format_txt_R42_MoreInfo_7495").val();
       _txt_R42_MoreInfo2_7495 = $("#TabContainerMain_tblR42format_txt_R42_MoreInfo2_7495").val();
       _txt_R42_MoreInfo3_7495 = $("#TabContainerMain_tblR42format_txt_R42_MoreInfo3_7495").val();
       _txt_R42_MoreInfo4_7495 = $("#TabContainerMain_tblR42format_txt_R42_MoreInfo4_7495").val();
       _txt_R42_MoreInfo5_7495 = $("#TabContainerMain_tblR42format_txt_R42_MoreInfo5_7495").val();
   }
    if (_txtDocNo != '' && _txtIBDDocNo != '') {
        $.ajax({
            type: "POST",
            url: "TF_IMP_LC_DESCOUNTING_DISCREPANT_ACC_IBD_Maker.aspx/AddUpdateONLCDiscount",
            data: '{_hdnUserName:"' + _hdnUserName + '", _BranchName:"' + _BranchName + '", _txtDocNo: "' + _txtDocNo + '", _txtIBDDocNo: "' + _txtIBDDocNo + '", _Document_Curr: "' + _Document_Curr +
        '", _hdnIBDExtnFlag:"' + _hdnIBDExtnFlag + '", _txtIBDExtnNo:"' + _txtIBDExtnNo +
        '", _txtDraftAmt:"' + _txtDraftAmt + '", _txtIBDAmt:"' + _txtIBDAmt + '", _txt_DiscAmt:"' + _txt_DiscAmt +
            //------------------Documnet Details-----------------------------------------------------

                    '", _txtCustName: "' + _txtCustName +
                    '", _txtHO_Apl: "' + _txtHO_Apl + '", _txtIBD_ACC_kind: "' + _txtIBD_ACC_kind + '", _txtvalueDate1: "' + _txtvalueDate1 + '", _txtValueDate: "' + _txtValueDate +
                    '", _applibeniStatus: "' + _applibeniStatus +
                    '", _txtCommentCode: "' + _txtCommentCode + '", _txtAutoSettlement: "' + _txtAutoSettlement +
                    '", _txtContractNo: "' + _txtContractNo + '", _txtExchRate: "' + _txtExchRate +
                    '", _txtCountryRisk: "' + _txtCountryRisk + '", _txtRiskCust: "' + _txtRiskCust +
                    '", _txtGradeCode: "' + _txtGradeCode + '", _txtApplNo: "' + _txtApplNo + '", _txtApplBR: "' + _txtApplBR + '", _txtPurpose: "' + _txtPurpose + '", _ddl_PurposeCode: "' + _ddl_PurposeCode +
                    '", _txtsettlCodeForCust: "' + _txtsettlCodeForCust + '", _txtsettlforCust_Abbr: "' + _txtsettlforCust_Abbr + '", _txtsettlforCust_AccCode: "' + _txtsettlforCust_AccCode + '", _txtsettlforCust_AccNo: "' + _txtsettlforCust_AccNo +
                    '", _txtInterest_From: "' + _txtInterest_From + '", _txtInterest_To: "' + _txtInterest_To + '", _txt_No_Of_Days: "' + _txt_No_Of_Days +
                    '",_txt_INT_Rate: "' + _txt_INT_Rate +
                    '", _txtBaseRate: "' + _txtBaseRate + '", _txtSpread: "' + _txtSpread + '", _txtInterestAmt: "' + _txtInterestAmt + '", _txtFundType: "' + _txtFundType +
                    '", _txtInternalRate: "' + _txtInternalRate + '", _txtSettl_CodeForBank: "' + _txtSettl_CodeForBank + '", _txtSettl_ForBank_Abbr: "' + _txtSettl_ForBank_Abbr +
                    '", _txtSettl_ForBank_AccCode: "' + _txtSettl_ForBank_AccCode + '", _txtSettl_ForBank_AccNo: "' + _txtSettl_ForBank_AccNo +
                    '", _txtAttn: "' + _txtAttn + '", _txtREM_EUC: "' + _txtREM_EUC +


            // Instruction
                    '", _txt_INST_Code:"' + _txt_INST_Code + '", _lbl_Instructions1: "' + _lbl_Instructions1 +

            //        // Import Accounting
                 '", _txt_IMP_ACC_ExchRate:"' + _txt_IMP_ACC_ExchRate + '", _txtPrinc_matu:"' + _txtPrinc_matu + '", _txtInterest_matu:"' + _txtInterest_matu +
                  '", _txtCommission_matu:"' + _txtCommission_matu + '", _txtTheir_Commission_matu:"' + _txtTheir_Commission_matu +
                  '", _txtPrinc_lump:"' + _txtPrinc_lump + '", _txtInterest_lump:"' + _txtInterest_lump + '", _txtCommission_lump:"' + _txtCommission_lump + '", _txtTheir_Commission_lump:"' + _txtTheir_Commission_lump +
                  '", _txtprinc_Contract_no:"' + _txtprinc_Contract_no + '", _txtInterest_Contract_no:"' + _txtInterest_Contract_no + '", _txtCommission_Contract_no:"' + _txtCommission_Contract_no + '", _txtTheir_Commission_Contract_no:"' + _txtTheir_Commission_Contract_no +
                  '", _txtprinc_Ex_rate:"' + _txtprinc_Ex_rate + '", _txtInterest_Ex_rate:"' + _txtInterest_Ex_rate + '", _txtCommission_Ex_rate:"' + _txtCommission_Ex_rate + '", _txtTheir_Commission_Ex_rate:"' + _txtTheir_Commission_Ex_rate +
                  '", _txtprinc_Intnl_Ex_rate:"' + _txtprinc_Intnl_Ex_rate + '", _txtInterest_Intnl_Ex_rate:"' + _txtInterest_Intnl_Ex_rate + '", _txtCommission_Intnl_Ex_rate:"' + _txtCommission_Intnl_Ex_rate + '", _txtTheir_Commission_Intnl_Ex_rate:"' + _txtTheir_Commission_Intnl_Ex_rate +
                   '", _txt_CR_Code:"' + _txt_CR_Code +
                   '", _txt_CR_AC_ShortName:"' + _txt_CR_AC_ShortName +
                    '", _txt_CR_Cust_abbr:"' + _txt_CR_Cust_abbr + '", _txt_CR_Cust_Acc:"' + _txt_CR_Cust_Acc +
                   '", _txt_CR_Acceptance_amt:"' + _txt_CR_Acceptance_amt + '", _txt_CR_Interest_amt:"' + _txt_CR_Interest_amt + '", _txt_CR_Accept_Commission_amt:"' + _txt_CR_Accept_Commission_amt +
                   '", _txt_CR_Pay_Handle_Commission_amt:"' + _txt_CR_Pay_Handle_Commission_amt + '", _txt_CR_Others_amt:"' + _txt_CR_Others_amt + '", _txt_CR_Their_Commission_amt:"' + _txt_CR_Their_Commission_amt +
                   '", _txt_Princ_Ex_Curr:"' + _txt_Princ_Ex_Curr + '", _txt_interest_Ex_Curr:"' + _txt_interest_Ex_Curr + '", _txt_Commission_Ex_Curr:"' + _txt_Commission_Ex_Curr + '", _txt_Their_Commission_Ex_Curr:"' + _txt_Their_Commission_Ex_Curr +
                   '", _txt_CR_Acceptance_Curr:"' + _txt_CR_Acceptance_Curr + '", _txt_CR_Interest_Curr:"' + _txt_CR_Interest_Curr + '", _txt_CR_Accept_Commission_Curr:"' + _txt_CR_Accept_Commission_Curr + '", _txt_CR_Pay_Handle_Commission_Curr:"' + _txt_CR_Pay_Handle_Commission_Curr +
                   '", _txt_CR_Others_Curr:"' + _txt_CR_Others_Curr + '", _txt_CR_Their_Commission_Curr:"' + _txt_CR_Their_Commission_Curr + '", _txt_DR_Cur_Acc_Curr2:"' + _txt_DR_Cur_Acc_Curr2 +
                   '", _txt_DR_Cur_Acc_Curr3:"' + _txt_DR_Cur_Acc_Curr3 +
                   '", _txt_CR_Acceptance_payer:"' + _txt_CR_Acceptance_payer + '", _txt_CR_Interest_payer:"' + _txt_CR_Interest_payer + '", _txt_CR_Accept_Commission_Payer:"' + _txt_CR_Accept_Commission_Payer +
                    '", _txt_CR_Pay_Handle_Commission_Payer:"' + _txt_CR_Pay_Handle_Commission_Payer +
                   '", _txt_CR_Others_Payer:"' + _txt_CR_Others_Payer + '", _txt_CR_Their_Commission_Payer:"' + _txt_CR_Their_Commission_Payer +
                   '", _txt_IBD_DR_Code:"' + _txt_IBD_DR_Code +
            //                   '", _txt_IBD_DR_AC_ShortName:"' + _txt_IBD_DR_AC_ShortName + 
                   '", _txt_IBD_DR_Cust_abbr:"' + _txt_IBD_DR_Cust_abbr + '", _txt_IBD_DR_Cust_Acc:"' + _txt_IBD_DR_Cust_Acc +
                   '", _txt_IBD_DR_Cur_Acc_Curr:"' + _txt_IBD_DR_Cur_Acc_Curr + '", _txt_IBD_DR_Cur_Acc_amt:"' + _txt_IBD_DR_Cur_Acc_amt +
                  '", _txt_IBD_DR_Cur_Acc_payer:"' + _txt_IBD_DR_Cur_Acc_payer +
                  '", _txt_DR_Code:"' + _txt_DR_Code + '", _txt_DR_Cust_abbr:"' + _txt_DR_Cust_abbr + '", _txt_DR_Cust_Acc:"' + _txt_DR_Cust_Acc +
                   '", _txt_DR_Cur_Acc_Curr:"' + _txt_DR_Cur_Acc_Curr + '", _txt_DR_Cur_Acc_amt:"' + _txt_DR_Cur_Acc_amt + '", _txt_DR_Cur_Acc_payer:"' + _txt_DR_Cur_Acc_payer +
                   '", _txt_DR_Cur_Acc_amt2:"' + _txt_DR_Cur_Acc_amt2 + '", _txt_DR_Cur_Acc_amt3:"' + _txt_DR_Cur_Acc_amt3 +
                   '", _txt_DR_Cur_Acc_payer2:"' + _txt_DR_Cur_Acc_payer2 + '", _txt_DR_Cur_Acc_payer3:"' + _txt_DR_Cur_Acc_payer3 +

            //-----------------------------Genral operation Branch------------------------------
            '", _cb_GOBranch_Bill_Handling_Flag: "' + _cb_GOBranch_Bill_Handling_Flag +
            '", _txt_GOBR_Ref_No: "' + _txt_GOBR_Ref_No +
            '", _txt_GOBR_Comment: "' + _txt_GOBR_Comment + '", _txt_GOBR_SectionNo: "' + _txt_GOBR_SectionNo +
            '", _txt_GOBR_Remarks: "' + _txt_GOBR_Remarks + '", _txt_GOBR_Memo: "' + _txt_GOBR_Memo + '", _txt_GOBR_Scheme_no: "' + _txt_GOBR_Scheme_no + '", _txt_GOBR_Debit_Code: "' + _txt_GOBR_Debit_Code + '", _txt_GOBR_Debit_Curr: "' + _txt_GOBR_Debit_Curr +
            '", _txt_GOBR_Debit_Amt: "' + _txt_GOBR_Debit_Amt + '", _txt_GOBR_Debit_Cust: "' + _txt_GOBR_Debit_Cust + '", _txt_GOBR_Debit_Cust_AcCode: "' + _txt_GOBR_Debit_Cust_AcCode + '", _txt_GOBR_Debit_Cust_AccNo: "' + _txt_GOBR_Debit_Cust_AccNo +
            '", _txt_GOBR_Debit_Exch_Rate: "' + _txt_GOBR_Debit_Exch_Rate + '", _txt_GOBR_Debit_Exch_CCY: "' + _txt_GOBR_Debit_Exch_CCY + '", _txt_GOBR_Debit_FUND: "' + _txt_GOBR_Debit_FUND + '", _txt_GOBR_Debit_Check_No: "' + _txt_GOBR_Debit_Check_No +
            '", _txt_GOBR_Debit_Available:"' + _txt_GOBR_Debit_Available + '",_txt_GOBR_Debit_AdPrint:"' + _txt_GOBR_Debit_AdPrint +
            '", _txt_GOBR_Debit_Details: "' + _txt_GOBR_Debit_Details + '", _txt_GOBR_Debit_Entity: "' + _txt_GOBR_Debit_Entity + '", _txt_GOBR_Debit_Division: "' + _txt_GOBR_Debit_Division + '", _txt_GOBR_Debit_Inter_Amount: "' + _txt_GOBR_Debit_Inter_Amount + '", _txt_GOBR_Debit_Inter_Rate: "' + _txt_GOBR_Debit_Inter_Rate +
            '", _txt_GOBR_Credit_Code: "' + _txt_GOBR_Credit_Code + '", _txt_GOBR_Credit_Curr: "' + _txt_GOBR_Credit_Curr + '", _txt_GOBR_Credit_Amt: "' + _txt_GOBR_Credit_Amt + '", _txt_GOBR_Credit_Cust: "' + _txt_GOBR_Credit_Cust + '", _txt_GOBR_Credit_Cust_AcCode: "' + _txt_GOBR_Credit_Cust_AcCode +
            '", _txt_GOBR_Credit_Cust_AccNo: "' + _txt_GOBR_Credit_Cust_AccNo + '", _txt_GOBR_Credit_Exch_Rate: "' + _txt_GOBR_Credit_Exch_Rate + '", _txt_GOBR_Credit_Exch_Curr: "' + _txt_GOBR_Credit_Exch_Curr +
            '", _txt_GOBR_Credit_FUND: "' + _txt_GOBR_Credit_FUND + '", _txt_GOBR_Credit_Check_No: "' + _txt_GOBR_Credit_Check_No + '", _txt_GOBR_Credit_Available: "' + _txt_GOBR_Credit_Available + '", _txt_GOBR_Credit_AdPrint: "' + _txt_GOBR_Credit_AdPrint + '", _txt_GOBR_Credit_Details: "' + _txt_GOBR_Credit_Details +
            '", _txt_GOBR_Credit_Entity: "' + _txt_GOBR_Credit_Entity + '", _txt_GOBR_Credit_Division: "' + _txt_GOBR_Credit_Division + '", _txt_GOBR_Credit_Inter_Amount: "' + _txt_GOBR_Credit_Inter_Amount + '", _txt_GOBR_Credit_Inter_Rate: "' + _txt_GOBR_Credit_Inter_Rate +

            //--------------------Genral operation-------------------------------------
            ///////////////// GENERAL OPRATOIN 1 ////////////////////////////
            '", _chk_GO1_Flag:"' + _chk_GO1_Flag +
            '", _txt_GO1_Comment: "' + _txt_GO1_Comment + '", _txt_GO1_SectionNo: "' + _txt_GO1_SectionNo +
            '", _txt_GO1_Remarks: "' + _txt_GO1_Remarks + '", _txt_GO1_Memo: "' + _txt_GO1_Memo + '", _txt_GO1_Scheme_no: "' + _txt_GO1_Scheme_no + '", _txt_GO1_Debit_Code: "' + _txt_GO1_Debit_Code + '", _txt_GO1_Debit_Curr: "' + _txt_GO1_Debit_Curr +
            '", _txt_GO1_Debit_Amt: "' + _txt_GO1_Debit_Amt + '", _txt_GO1_Debit_Cust: "' + _txt_GO1_Debit_Cust + '", _txt_GO1_Debit_Cust_AcCode: "' + _txt_GO1_Debit_Cust_AcCode + '", _txt_GO1_Debit_Cust_AccNo: "' + _txt_GO1_Debit_Cust_AccNo +
            '", _txt_GO1_Debit_Exch_Rate: "' + _txt_GO1_Debit_Exch_Rate + '", _txt_GO1_Debit_Exch_CCY: "' + _txt_GO1_Debit_Exch_CCY + '", _txt_GO1_Debit_FUND: "' + _txt_GO1_Debit_FUND + '", _txt_GO1_Debit_Check_No: "' + _txt_GO1_Debit_Check_No +
            '",_txt_GO1_Debit_Available:"' + _txt_GO1_Debit_Available + '",_txt_GO1_Debit_AdPrint:"' + _txt_GO1_Debit_AdPrint +
            '", _txt_GO1_Debit_Details: "' + _txt_GO1_Debit_Details + '", _txt_GO1_Debit_Entity: "' + _txt_GO1_Debit_Entity + '", _txt_GO1_Debit_Division: "' + _txt_GO1_Debit_Division + '", _txt_GO1_Debit_Inter_Amount: "' + _txt_GO1_Debit_Inter_Amount + '", _txt_GO1_Debit_Inter_Rate: "' + _txt_GO1_Debit_Inter_Rate +
            '", _txt_GO1_Credit_Code: "' + _txt_GO1_Credit_Code + '", _txt_GO1_Credit_Curr: "' + _txt_GO1_Credit_Curr + '", _txt_GO1_Credit_Amt: "' + _txt_GO1_Credit_Amt + '", _txt_GO1_Credit_Cust: "' + _txt_GO1_Credit_Cust + '", _txt_GO1_Credit_Cust_AcCode: "' + _txt_GO1_Credit_Cust_AcCode +
            '", _txt_GO1_Credit_Cust_AccNo: "' + _txt_GO1_Credit_Cust_AccNo + '", _txt_GO1_Credit_Exch_Rate: "' + _txt_GO1_Credit_Exch_Rate + '", _txt_GO1_Credit_Exch_Curr: "' + _txt_GO1_Credit_Exch_Curr +
            '", _txt_GO1_Credit_FUND: "' + _txt_GO1_Credit_FUND + '", _txt_GO1_Credit_Check_No: "' + _txt_GO1_Credit_Check_No + '", _txt_GO1_Credit_Available: "' + _txt_GO1_Credit_Available + '", _txt_GO1_Credit_AdPrint: "' + _txt_GO1_Credit_AdPrint + '", _txt_GO1_Credit_Details: "' + _txt_GO1_Credit_Details +
            '", _txt_GO1_Credit_Entity: "' + _txt_GO1_Credit_Entity + '", _txt_GO1_Credit_Division: "' + _txt_GO1_Credit_Division + '", _txt_GO1_Credit_Inter_Amount: "' + _txt_GO1_Credit_Inter_Amount + '", _txt_GO1_Credit_Inter_Rate: "' + _txt_GO1_Credit_Inter_Rate +

            ///////////////// GENERAL OPRATOIN 2 ////////////////////////////
            '", _chk_GO2_Flag: "' + _chk_GO2_Flag +
            '", _txt_GO2_Comment: "' + _txt_GO2_Comment + '", _txt_GO2_SectionNo: "' + _txt_GO2_SectionNo +
            '", _txt_GO2_Remarks: "' + _txt_GO2_Remarks + '", _txt_GO2_Memo: "' + _txt_GO2_Memo + '", _txt_GO2_Scheme_no: "' + _txt_GO2_Scheme_no + '", _txt_GO2_Debit_Code: "' + _txt_GO2_Debit_Code + '", _txt_GO2_Debit_Curr: "' + _txt_GO2_Debit_Curr +
            '", _txt_GO2_Debit_Amt: "' + _txt_GO2_Debit_Amt + '", _txt_GO2_Debit_Cust: "' + _txt_GO2_Debit_Cust + '", _txt_GO2_Debit_Cust_AcCode: "' + _txt_GO2_Debit_Cust_AcCode + '", _txt_GO2_Debit_Cust_AccNo: "' + _txt_GO2_Debit_Cust_AccNo +
            '", _txt_GO2_Debit_Exch_Rate: "' + _txt_GO2_Debit_Exch_Rate + '", _txt_GO2_Debit_Exch_CCY: "' + _txt_GO2_Debit_Exch_CCY + '", _txt_GO2_Debit_FUND: "' + _txt_GO2_Debit_FUND + '", _txt_GO2_Debit_Check_No: "' + _txt_GO2_Debit_Check_No +
            '", _txt_GO2_Debit_Available:"' + _txt_GO2_Debit_Available + '",_txt_GO2_Debit_AdPrint:"' + _txt_GO2_Debit_AdPrint +
            '", _txt_GO2_Debit_Details: "' + _txt_GO2_Debit_Details + '", _txt_GO2_Debit_Entity: "' + _txt_GO2_Debit_Entity + '", _txt_GO2_Debit_Division: "' + _txt_GO2_Debit_Division + '", _txt_GO2_Debit_Inter_Amount: "' + _txt_GO2_Debit_Inter_Amount + '", _txt_GO2_Debit_Inter_Rate: "' + _txt_GO2_Debit_Inter_Rate +
            '", _txt_GO2_Credit_Code: "' + _txt_GO2_Credit_Code + '", _txt_GO2_Credit_Curr: "' + _txt_GO2_Credit_Curr + '", _txt_GO2_Credit_Amt: "' + _txt_GO2_Credit_Amt + '", _txt_GO2_Credit_Cust: "' + _txt_GO2_Credit_Cust + '",  _txt_GO2_Credit_Cust_AcCode: "' + _txt_GO2_Credit_Cust_AcCode +
            '", _txt_GO2_Credit_Cust_AccNo: "' + _txt_GO2_Credit_Cust_AccNo + '", _txt_GO2_Credit_Exch_Rate: "' + _txt_GO2_Credit_Exch_Rate + '", _txt_GO2_Credit_Exch_Curr: "' + _txt_GO2_Credit_Exch_Curr +
            '", _txt_GO2_Credit_FUND: "' + _txt_GO2_Credit_FUND + '", _txt_GO2_Credit_Check_No: "' + _txt_GO2_Credit_Check_No + '", _txt_GO2_Credit_Available: "' + _txt_GO2_Credit_Available + '", _txt_GO2_Credit_AdPrint: "' + _txt_GO2_Credit_AdPrint + '", _txt_GO2_Credit_Details: "' + _txt_GO2_Credit_Details +
            '", _txt_GO2_Credit_Entity: "' + _txt_GO2_Credit_Entity + '", _txt_GO2_Credit_Division: "' + _txt_GO2_Credit_Division + '", _txt_GO2_Credit_Inter_Amount: "' + _txt_GO2_Credit_Inter_Amount + '", _txt_GO2_Credit_Inter_Rate: "' + _txt_GO2_Credit_Inter_Rate +


            ////-----------------------------------R42 format for IBD--------------------------------------------------
            '", _txt_R42_tansactionRefNO:"' + _txt_R42_tansactionRefNO + '", _txt_R42_RelatedRef: "' + _txt_R42_RelatedRef + '", _txt_R42_ValueDate_4488: "' + _txt_R42_ValueDate_4488 + '", _txt_R42_Curr_4488: "' + _txt_R42_Curr_4488 + '", _txt_R42_Amt_4488: "' + _txt_R42_Amt_4488 +
            '", _txt_R42_Orderingins_IFSC_5517: "' + _txt_R42_Orderingins_IFSC_5517 + '", _txt_R42_Benificiary_IFSC_6521: "' + _txt_R42_Benificiary_IFSC_6521 +
            '", _txt_R42_CodeWord_7495: "' + _txt_R42_CodeWord_7495 + '", _txt_R42_AddInfo_7495: "' + _txt_R42_AddInfo_7495 + '", _txt_R42_MoreInfo_7495: "' + _txt_R42_MoreInfo_7495 + '", _txt_R42_MoreInfo2_7495: "' + _txt_R42_MoreInfo2_7495 + '", _txt_R42_MoreInfo3_7495: "' + _txt_R42_MoreInfo3_7495 + '", _txt_R42_MoreInfo4_7495: "' + _txt_R42_MoreInfo4_7495 +
            '", _txt_R42_MoreInfo5_7495: "' + _txt_R42_MoreInfo5_7495 +

                    '"}',

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
}

function OnSuccess(response) {
}

function Validate(index) {
    var tabContainer = $get('TabContainerMain');
    if ($("#txtIBDDocNo").val() == '') {
        alert('Please select IBD Ref No.');
        tabContainer.control.set_activeTabIndex(0);
        $("#txtIBDDocNo").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_rdbIDOA").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdbIDOB").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdbIOBDOA").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdbIOADOB").is(':checked')) {
        return true;
    }
    else {
        alert('Please select LC Discounting type.');
        tabContainer.control.set_activeTabIndex(0);
        $("#txtDateReceived").focus();
        return false;
    }
}
function ViewSwiftMessage() {
    SaveUpdateData();
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = 'LOCAL';

    _SWIFT_File_Type = 'IBDR42';

     var _hdnIBDExtnFlag = $("#hdnIBDExtnFlag").val();
     var IBDExtnNo = '';
     if (_hdnIBDExtnFlag == 'Y') {
         IBDExtnNo = $("#txtIBDExtnNo").val();
     }
     var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&IBDExtnNo=' + IBDExtnNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
    winame.focus();
    return false;
    //    }
}
function OpenSettl_CodeForBank_help(e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_IBD_SundryaccountHelp.aspx', 500, 500, 'SundryCodeList');
        return false;
    }
}
function select_CR_Code(ACcode, ABB, Acno) {

    $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccCode").val(ACcode)
    $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_Abbr").val(ABB);
    $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccNo").val(Acno);
    DebitCredit_For_IMP_ACC();
}
function DebitCredit_For_IMP_ACC() {
    var _txtSettl_ForBank_AccCode = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccCode").val();
    var _txtSettl_ForBank_Abbr = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_Abbr").val();
    var _txtSettl_ForBank_AccNo = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccNo").val();
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val(_txtSettl_ForBank_AccCode);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val(_txtSettl_ForBank_Abbr);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val(_txtSettl_ForBank_AccNo);

    if ($("#TabContainerMain_tbDocumentAccounting_txtInterest_matu").val() == '1') {
        var _txtsettlforCust_AccCode = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_AccCode").val();
        var _txtsettlforCust_Abbr = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_Abbr").val();
        var _txtsettlforCust_AccNo = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_AccNo").val();
        var _txtInterestAmt = $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val();
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val(_txtsettlforCust_AccCode);
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val(_txtsettlforCust_Abbr);
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val(_txtsettlforCust_AccNo);
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val(_txtInterestAmt);
    }
}

function CustAbbr() {
    var _valAB = $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val();
    $("#TabContainerMain_tbDocumentAccounting_txt_IBD_DR_Cust_abbr").val(_valAB);
}

function CustRem() {

    var _valRem = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Remarks").val();
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Details").val(_valRem);

    var _valRem1 = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Remarks").val();
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Details").val(_valRem1);


    var _valG1 = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Remarks").val();
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Details").val(_valG1);

    var _valRemG1 = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Remarks").val();
    $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Details").val(_valRemG1);


    var _valG2 = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Remarks").val();
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Details").val(_valG2);

    var _valRemG2 = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Remarks").val();
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Details").val(_valRemG2);
}

function Toggle_GO_Amt(GO) {
    if (GO == 'GO1') {
        var _txt_GO1_Debit_Amt = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Amt").val(_txt_GO1_Debit_Amt);
    }

    if (GO == 'GO2') {
        var _txt_GO2_Debit_Amt = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Amt").val(_txt_GO2_Debit_Amt);
    }

    if (GO == 'BR') {
        var _txt_GOBR_Debit_Amt = $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGOBranch_txt_GOBR_Credit_Amt").val(_txt_GOBR_Debit_Amt);
    }

}
// Changes Ashutosh on 26122019
function OpenIBDDocNo_help(e) {
    var keycode, DocNo;
    DocNo = $("#txtDocNo").val();
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_IMP_MintHelp.aspx?DocNo=' + DocNo, 500, 700, 'MintRefNoList');
        return false;
    }
}

function selectMint(GbaseRefNo,NumberOfDays, CustAbbr, PrincipalAmount, FinalDueDate, InterestRate, TransSpread, InterestAmount, InternalRate) {
    $("#txtIBDDocNo").val(GbaseRefNo);
	$("#TabContainerMain_tblR42format_txt_R42_tansactionRefNO").val(GbaseRefNo);
    $("#TabContainerMain_tbDocumentDetails_txtCustName").val(CustAbbr);
    $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val(PrincipalAmount);
    $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val(CustAbbr);
    $("#TabContainerMain_tbDocumentDetails_txtInterest_To").val(FinalDueDate);
	$("#TabContainerMain_tbDocumentDetails_txt_No_Of_Days").val(NumberOfDays);
    $("#TabContainerMain_tbDocumentDetails_txt_INT_Rate").val(InterestRate);
    $("#TabContainerMain_tbDocumentDetails_txtSpread").val(TransSpread);
    $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val(InterestAmount);
    $("#TabContainerMain_tbDocumentDetails_txtInternalRate").val(InternalRate);
	
	
    CheckIBDREFno();
    Toggel_Bill_Amt();
    ONINTERESTAMT();
    R42_Amt_Calculation();
    $("#txtIBDDocNo").focus();
}
function CheckIBDREFno() {
    var REFNo = $("#txtIBDDocNo").val();
    var DocNo = $("#txtDocNo").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_LC_DESCOUNTING_ACC_IBD_Maker.aspx/CheckIBDREFno",
        data: '{REFNo:"' + REFNo + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.IBDREFnoStatus == "Exists") {
                $("#txtIBDDocNo").val('');
				$("#TabContainerMain_tblR42format_txt_R42_tansactionRefNO").val('');
                $("#TabContainerMain_tbDocumentDetails_txtCustName").val('');
                $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val('');
                $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val('');
                $("#TabContainerMain_tbDocumentDetails_txtInterest_To").val('');
				$("#TabContainerMain_tbDocumentDetails_txt_No_Of_Days").val('');
                $("#TabContainerMain_tbDocumentDetails_txt_INT_Rate").val('');
                $("#TabContainerMain_tbDocumentDetails_txtSpread").val('');
                $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val('');
                $("#TabContainerMain_tbDocumentDetails_txtInternalRate").val('');
                alert('This IBD REF Number(' + REFNo + ') already used.')
                return false;
            }
            else {
                $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_payer").val('B');
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}


