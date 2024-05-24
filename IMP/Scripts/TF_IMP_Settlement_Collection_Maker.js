function allowBackSpace(val, event) { //vinay Swift Changes
    if (val == 'Nego754Date') {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoDate_754").val('');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_btncalendar_PrinAmtPaidAccNegoDate_754").focus();
            return true;
        }
    }
    if (val == 'Total754Date') {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Date").val('');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_btncalendar_754_TotalAmtClmd_Date").focus(); ;
            return true;
        }
    }
    if (val == '200Date') {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Date").val('');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Date").focus();
            return true;
        }
    }
    if (val == '103Date') {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt103Date").val('');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt103Date").focus();
            return true;
        }
    }

}

function Open_NostroBank_help_MT200(e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_IMP_NostroBankHelp.aspx?Swift=MT200', 500, 500, 'NostroBank');
        return false;
    }
}
function select_NostroBank_MT200(CustAbbr, Currency, SwiftCode) {
    $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200BicCode").val(SwiftCode);
}

function Open_NostroBank_help_MT103(e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_IMP_NostroBankHelp.aspx?Swift=MT103', 500, 500, 'NostroBank');
        return false;
    }
}
function select_NostroBank_MT103(CustAbbr, Currency, SwiftCode) {
    $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt_MT103Receiver").val(SwiftCode);
}
function DR_Amt_Calculation(IMP_Accounting, Commission) {
    var CR_Amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_CR_" + Commission + "_amt").val();
    var Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_" + Commission + "_Ex_rate").val();
    var Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_" + Commission + "_Intnl_Ex_rate").val();
    var Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_" + Commission + "_Ex_Curr").val().toUpperCase();
    var DR_Amt;

    if (Ex_rate == '') {
        Ex_rate = 1;
    }
    if (CR_Amt == '') {
        CR_Amt = 0;
    }
    CR_Amt = parseFloat(CR_Amt);
    Ex_rate = parseFloat(Ex_rate);
    if (Ex_rate != 1 && Ex_rate != 0) {
        if ($.isNumeric(CR_Amt) && $.isNumeric(Ex_rate)) {
            DR_Amt = parseFloat(CR_Amt * Ex_rate);
            if (Ex_Curr == 'INR') {
                DR_Amt = parseFloat(round(parseFloat(DR_Amt), 0)).toFixed(2);
                $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_amt6").val(DR_Amt);
            }
            else {
                DR_Amt = parseFloat(DR_Amt).toFixed(2);
                $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_amt6").val(DR_Amt);
            }
        }
    }
    else { $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_amt6").val(CR_Amt); }
}
function DR_Curr_Toggel(IMP_Accounting, Commission, DR_Curr) {

    var Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_" + Commission + "_Ex_Curr").val();
    var Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_CR_" + Commission + "_Curr").val();
    var Document_Curr = $("#lblDoc_Curr").text();
    var Their_Comm_Curr = $("#ACC_Their_Comm_Curr").val();

    if (Ex_Curr == '') {
        if (IMP_Accounting == 'ACC1') {
            $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_" + DR_Curr).val(Their_Comm_Curr);
        }
        else {
            $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_" + DR_Curr).val(Commission_Curr);
        }
    }
    else {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_" + DR_Curr).val(Ex_Curr);
    }
    DR_Amt_Calculation(IMP_Accounting, Commission);
}

// General Opration 1
function GO_Amt_Calculation(GO, LeftOrRight) {
    var Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Amt").val();
    var Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Curr").val();
    var Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Exch_Rate").val();
    var Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Exch_CCY").val();

    if (Debit_Amt != '') {
        if (Debit_Exch_Rate == '') {
            Debit_Exch_Rate = 1;
        }

        Debit_Exch_Rate = parseFloat(Debit_Exch_Rate);

        if (Debit_Exch_Rate != 1 && Debit_Exch_Rate != 0) {
            var roundamount = parseFloat(Debit_Amt * Debit_Exch_Rate);
            if (Debit_Exch_CCY.toUpperCase() == 'INR') {
                roundamount = parseFloat(round(parseFloat(roundamount), 0)).toFixed(0);
            }
            else {
                roundamount = parseFloat(roundamount).toFixed(2);
            }
            $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Amt").val(roundamount);
        }
        else {
            $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Amt").val(Debit_Amt);
        }
    }
}
function GO_Debit_Curr_Change(GO, LeftOrRight) {
    var Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Curr").val();
    if (Debit_Curr.toUpperCase() != 'INR') {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Exch_Curr").val(Debit_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Exch_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Exch_Rate").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Exch_CCY").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Curr").val(Debit_Curr);
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Exch_Rate").val('');
    }
    GO_Amt_Calculation(GO, LeftOrRight);
}
function GO_Debit_Exch_CCY_Change(GO, LeftOrRight) {
    var Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Curr").val();
    var Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Exch_CCY").val();

    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Curr").val(Debit_Exch_CCY);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Exch_Curr").val(Debit_Curr);

    GO_Amt_Calculation(GO, LeftOrRight);
}
function GO_Debit_Exch_Rate_Change(GO, LeftOrRight) {
    var Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Debit_Exch_Rate").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanel" + GO + "_txt_" + GO + "_" + LeftOrRight + "_Credit_Exch_Rate").val(Debit_Exch_Rate);
    GO_Amt_Calculation(GO, LeftOrRight);
}
// General Opration Branch
// LEFT
function txt_GOAccChange_Debit_Curr_Change() {
    var _txt_GOAccChange_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Curr").val();
    if (_txt_GOAccChange_Debit_Curr.toUpperCase() != 'INR') {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Exch_Curr").val(_txt_GOAccChange_Debit_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Exch_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_Rate").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_CCY").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Curr").val(_txt_GOAccChange_Debit_Curr);
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Exch_Rate").val('');
    }
    GOBranch_Amt_Calculation();
}
function txt_GOAccChange_Debit_Exch_CCY_Change() {
    var _txt_GOAccChange_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Curr").val();
    var _txt_GOAccChange_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_CCY").val();

    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Curr").val(_txt_GOAccChange_Debit_Exch_CCY);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Exch_Curr").val(_txt_GOAccChange_Debit_Curr);

    GOBranch_Amt_Calculation();
}
function txt_GOAccChange_Debit_Exch_Rate_Change() {
    var _txt_GOAccChange_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_Rate").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Exch_Rate").val(_txt_GOAccChange_Debit_Exch_Rate);
    GOBranch_Amt_Calculation();
}
function GOBranch_Amt_Calculation() {
    var _txt_GOAccChange_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Amt").val();
    var _txt_GOAccChange_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Curr").val();
    var _txt_GOAccChange_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_Rate").val();
    var _txt_GOAccChange_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_CCY").val();

    if (_txt_GOAccChange_Debit_Amt != '') {

        if (_txt_GOAccChange_Debit_Exch_Rate == '') {
            _txt_GOAccChange_Debit_Exch_Rate = 1;
        }
        if (_txt_GOAccChange_Debit_Exch_Rate != 1 && _txt_GOAccChange_Debit_Exch_Rate != 0) {

            var roundamount = parseFloat(_txt_GOAccChange_Debit_Amt * _txt_GOAccChange_Debit_Exch_Rate);
            if (_txt_GOAccChange_Debit_Exch_CCY.toUpperCase() == 'INR') {
                roundamount = parseFloat(round(parseFloat(roundamount), 0)).toFixed(0);
            }
            else {
                roundamount = parseFloat(roundamount).toFixed(2);
            }
            $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Amt").val(roundamount);
        }
        else {
            $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Amt").val(_txt_GOAccChange_Debit_Amt);
        }
    }
}


function SaveUpdateData() {
    var hdnUserName = document.getElementById('hdnUserName').value;
    var _BranchName = $("#hdnBranchName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _DocType = $("#hdnDocType").val();
    var _FLC_ILC = $("#hdnflcIlcType").val();
    var _Doc_Scrutiny = $("#hdnDoc_Scrutiny").val();
    var _Document_Curr = $("#lblDoc_Curr").text();
    var _txtValueDate = $("#txtValueDate").val();
    ////////////////Document Details//////////////////////////
    var _txt_Doc_Comment_Code = '', _txt_Doc_Maturity = '', _txt_Doc_Settlement_for_Cust = '', _txt_Doc_Settlement_For_Bank = '',
    _txt_Doc_Interest_From = '', _txt_Doc_Interest_To = '', _txt_Doc_No_Of_Days = '', _txt_Doc_Discount = '',
    _txt_Doc_InterestRate = '', _txt_Doc_InterestAmount = '',
    _txt_Doc_Overdue_Interestrate = '', _txt_Doc_Oveduenoofdays = '', _txt_Doc_Overdueamount = '', _txt_Doc_Attn = '';

    if (_DocType == 'ACC') {
        _txt_Doc_Comment_Code = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Comment_Code").val(),
        _txt_Doc_Maturity = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Maturity").val(),
        _txt_Doc_Settlement_for_Cust = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Settlement_for_Cust").val(),
        _txt_Doc_Settlement_For_Bank = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Settlement_For_Bank").val(),
        _txt_Doc_Interest_From = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Interest_From").val(),
        _txt_Doc_Interest_To = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Interest_To").val(),
        _txt_Doc_No_Of_Days = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_No_Of_Days").val(),
        _txt_Doc_Discount = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Discount").val(),
        _txt_Doc_InterestRate = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Rate").val(),
        _txt_Doc_InterestAmount = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Amount").val(),
        _txt_Doc_Overdue_Interestrate = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Overdue_Interestrate").val(),
        _txt_Doc_Oveduenoofdays = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Oveduenoofdays").val(),
        _txt_Doc_Overdueamount = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Overdueamount").val(),
        _txt_Doc_Attn = $("#TabContainerMain_tbDocumentDetailsUnderLC_txt_UnderLC_Attn").val();
    }
    else {
        _txt_Doc_Settlement_for_Cust = $("#TabContainerMain_tbDocumentDetailsCollection_txt_Collection_Settlement_for_Cust").val(),
        _txt_Doc_Settlement_For_Bank = $("#TabContainerMain_tbDocumentDetailsCollection_txt_Collection_Settlement_For_Bank").val(),
        _txt_Doc_Attn = $("#TabContainerMain_tbDocumentDetailsCollection_txt_Collection_Attn").val();
    }

    //////////////// IMPORT ACCOUNTING /////////////////
    var _chk_IMPACC1Flag = 'N', _txt_IMPACC1_FCRefNo = '',
        _txt_IMPACC1_DiscAmt = '', _txt_IMPACC1_DiscExchRate = '',
        _txt_IMPACC1_Princ_matu = '', _txt_IMPACC1_Princ_lump = '', _txt_IMPACC1_Princ_Contract_no = '', _txt_IMPACC1_Princ_Ex_Curr = '', _txt_IMPACC1_Princ_Ex_rate = '', _txt_IMPACC1_Princ_Intnl_Ex_rate = '',
        _txt_IMPACC1_Interest_matu = '', _txt_IMPACC1_Interest_lump = '', _txt_IMPACC1_Interest_Contract_no = '', _txt_IMPACC1_Interest_Ex_Curr = '', _txt_IMPACC1_Interest_Ex_rate = '', _txt_IMPACC1_Interest_Intnl_Ex_rate = '',
        _txt_IMPACC1_Commission_matu = '', _txt_IMPACC1_Commission_lump = '', _txt_IMPACC1_Commission_Contract_no = '', _txt_IMPACC1_Commission_Ex_Curr = '', _txt_IMPACC1_Commission_Ex_rate = '', _txt_IMPACC1_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC1_Their_Commission_matu = '', _txt_IMPACC1_Their_Commission_lump = '', _txt_IMPACC1_Their_Commission_Contract_no = '', _txt_IMPACC1_Their_Commission_Ex_Curr = '', _txt_IMPACC1_Their_Commission_Ex_rate = '', _txt_IMPACC1_Their_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC1_CR_Code = '', _txt_IMPACC1_CR_AC_Short_Name = '', _txt_IMPACC1_CR_Cust_abbr = '', _txt_IMPACC1_CR_Cust_Acc = '', _txt_IMPACC1_CR_Acceptance_Curr = '', _txt_IMPACC1_CR_Acceptance_amt = '', _txt_IMPACC1_CR_Acceptance_payer = '',
        _txt_IMPACC1_CR_Interest_Curr = '', _txt_IMPACC1_CR_Interest_amt = '', _txt_IMPACC1_CR_Interest_payer = '',
        _txt_IMPACC1_CR_Accept_Commission_Curr = '', _txt_IMPACC1_CR_Accept_Commission_amt = '', _txt_IMPACC1_CR_Accept_Commission_Payer = '',
        _txt_IMPACC1_CR_Pay_Handle_Commission_Curr = '', _txt_IMPACC1_CR_Pay_Handle_Commission_amt = '', _txt_IMPACC1_CR_Pay_Handle_Commission_Payer = '',
        _txt_IMPACC1_CR_Others_Curr = '', _txt_IMPACC1_CR_Others_amt = '', _txt_IMPACC1_CR_Others_Payer = '',
        _txt_IMPACC1_CR_Their_Commission_Curr = '', _txt_IMPACC1_CR_Their_Commission_amt = '', _txt_IMPACC1_CR_Their_Commission_Payer = '',
        _txt_IMPACC1_DR_Code = '', _txt_IMPACC1_DR_AC_Short_Name = '', _txt_IMPACC1_DR_Cust_abbr = '', _txt_IMPACC1_DR_Cust_Acc = '',
        _txt_IMPACC1_DR_Cur_Acc_Curr = '', _txt_IMPACC1_DR_Cur_Acc_amt = '', _txt_IMPACC1_DR_Cur_Acc_payer = '',
        _txt_IMPACC1_DR_Cur_Acc_Curr2 = '', _txt_IMPACC1_DR_Cur_Acc_amt2 = '', _txt_IMPACC1_DR_Cur_Acc_payer2 = '',
        _txt_IMPACC1_DR_Cur_Acc_Curr3 = '', _txt_IMPACC1_DR_Cur_Acc_amt3 = '', _txt_IMPACC1_DR_Cur_Acc_payer3 = '',
        _txt_IMPACC1_DR_Cur_Acc_Curr4 = '', _txt_IMPACC1_DR_Cur_Acc_amt4 = '', _txt_IMPACC1_DR_Cur_Acc_payer4 = '',
        _txt_IMPACC1_DR_Cur_Acc_Curr5 = '', _txt_IMPACC1_DR_Cur_Acc_amt5 = '', _txt_IMPACC1_DR_Cur_Acc_payer5 = '',
        _txt_IMPACC1_DR_Cur_Acc_Curr6 = '', _txt_IMPACC1_DR_Cur_Acc_amt6 = '', _txt_IMPACC1_DR_Cur_Acc_payer6 = '',
        _txt_IMPACC1_DR_Code2 = '', _txt_IMPACC1_DR_AC_Short_Name2 = '', _txt_IMPACC1_DR_Cust_abbr2 = '', _txt_IMPACC1_DR_Cust_Acc2 = '',
        _txt_IMPACC1_DR_Code3 = '', _txt_IMPACC1_DR_AC_Short_Name3 = '', _txt_IMPACC1_DR_Cust_abbr3 = '', _txt_IMPACC1_DR_Cust_Acc3 = '',
        _txt_IMPACC1_DR_Code4 = '', _txt_IMPACC1_DR_AC_Short_Name4 = '', _txt_IMPACC1_DR_Cust_abbr4 = '', _txt_IMPACC1_DR_Cust_Acc4 = '',
        _txt_IMPACC1_DR_Code5 = '', _txt_IMPACC1_DR_AC_Short_Name5 = '', _txt_IMPACC1_DR_Cust_abbr5 = '', _txt_IMPACC1_DR_Cust_Acc5 = '',
        _txt_IMPACC1_DR_Code6 = '', _txt_IMPACC1_DR_AC_Short_Name6 = '', _txt_IMPACC1_DR_Cust_abbr6 = '', _txt_IMPACC1_DR_Cust_Acc6 = '';

    if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_chk_IMPACC1Flag").is(':checked')) {
        _chk_IMPACC1Flag = 'Y',
        _txt_IMPACC1_FCRefNo = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_FCRefNo").val(),
        _txt_IMPACC1_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DiscAmt").val(),
    _txt_IMPACC1_Princ_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_matu").val(),
    _txt_IMPACC1_Princ_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_lump").val(),
    _txt_IMPACC1_Princ_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Contract_no").val(),
    _txt_IMPACC1_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_Curr").val(),
    _txt_IMPACC1_Princ_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_rate").val(),
    _txt_IMPACC1_Princ_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Intnl_Ex_rate").val(),

    _txt_IMPACC1_Interest_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Interest_matu").val(),
    _txt_IMPACC1_Interest_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Interest_lump").val(),
    _txt_IMPACC1_Interest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Interest_Contract_no").val(),
    _txt_IMPACC1_Interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Interest_Ex_Curr").val(),
    _txt_IMPACC1_Interest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Interest_Ex_rate").val(),
    _txt_IMPACC1_Interest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Interest_Intnl_Ex_rate").val(),

    _txt_IMPACC1_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Commission_matu").val(),
    _txt_IMPACC1_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Commission_lump").val(),
    _txt_IMPACC1_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Commission_Contract_no").val(),
    _txt_IMPACC1_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Commission_Ex_Curr").val(),
    _txt_IMPACC1_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Commission_Ex_rate").val(),
    _txt_IMPACC1_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC1_Their_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Their_Commission_matu").val(),
    _txt_IMPACC1_Their_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Their_Commission_lump").val(),
    _txt_IMPACC1_Their_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Their_Commission_Contract_no").val(),
    _txt_IMPACC1_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Their_Commission_Ex_Curr").val(),
    _txt_IMPACC1_Their_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Their_Commission_Ex_rate").val(),
    _txt_IMPACC1_Their_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Their_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC1_CR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Code").val(),
    _txt_IMPACC1_CR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_AC_Short_Name").val(),
    _txt_IMPACC1_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Cust_abbr").val(),
    _txt_IMPACC1_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Cust_Acc").val(),
    _txt_IMPACC1_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Acceptance_Curr").val(),
    _txt_IMPACC1_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Acceptance_amt").val(),
    _txt_IMPACC1_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Acceptance_payer").val(),

    _txt_IMPACC1_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Interest_Curr").val(),
    _txt_IMPACC1_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Interest_amt").val(),
    _txt_IMPACC1_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Interest_payer").val(),

    _txt_IMPACC1_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Accept_Commission_Curr").val(),
    _txt_IMPACC1_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Accept_Commission_amt").val(),
    _txt_IMPACC1_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Accept_Commission_Payer").val(),

    _txt_IMPACC1_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Pay_Handle_Commission_Curr").val(),
    _txt_IMPACC1_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Pay_Handle_Commission_amt").val(),
    _txt_IMPACC1_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Pay_Handle_Commission_Payer").val(),

    _txt_IMPACC1_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Others_Curr").val(),
    _txt_IMPACC1_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Others_amt").val(),
    _txt_IMPACC1_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Others_Payer").val(),

    _txt_IMPACC1_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Their_Commission_Curr").val(),
    _txt_IMPACC1_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Their_Commission_amt").val(),
    _txt_IMPACC1_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Their_Commission_Payer").val(),

    _txt_IMPACC1_DR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code").val(),
    _txt_IMPACC1_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name").val(),
    _txt_IMPACC1_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr").val(),
    _txt_IMPACC1_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc").val(),
    _txt_IMPACC1_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr").val(),
    _txt_IMPACC1_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_amt").val(),
    _txt_IMPACC1_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_payer").val(),

    _txt_IMPACC1_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr2").val(),
    _txt_IMPACC1_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_amt2").val(),
    _txt_IMPACC1_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_payer2").val(),

    _txt_IMPACC1_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr3").val(),
    _txt_IMPACC1_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_amt3").val(),
    _txt_IMPACC1_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_payer3").val(),

    _txt_IMPACC1_DR_Cur_Acc_Curr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr4").val(),
    _txt_IMPACC1_DR_Cur_Acc_amt4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_amt4").val(),
    _txt_IMPACC1_DR_Cur_Acc_payer4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_payer4").val(),

	_txt_IMPACC1_DR_Cur_Acc_Curr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr5").val(),
    _txt_IMPACC1_DR_Cur_Acc_amt5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_amt5").val(),
    _txt_IMPACC1_DR_Cur_Acc_payer5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_payer5").val(),

	_txt_IMPACC1_DR_Cur_Acc_Curr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr6").val(),
    _txt_IMPACC1_DR_Cur_Acc_amt6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_amt6").val(),
    _txt_IMPACC1_DR_Cur_Acc_payer6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_payer6").val(),

	_txt_IMPACC1_DR_Code2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code2").val(),
    _txt_IMPACC1_DR_AC_Short_Name2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name2").val(),
    _txt_IMPACC1_DR_Cust_abbr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr2").val(),
    _txt_IMPACC1_DR_Cust_Acc2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc2").val(),

    _txt_IMPACC1_DR_Code3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code3").val(),
    _txt_IMPACC1_DR_AC_Short_Name3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name3").val(),
    _txt_IMPACC1_DR_Cust_abbr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr3").val(),
    _txt_IMPACC1_DR_Cust_Acc3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc3").val(),

    _txt_IMPACC1_DR_Code4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code4").val(),
    _txt_IMPACC1_DR_AC_Short_Name4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name4").val(),
    _txt_IMPACC1_DR_Cust_abbr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr4").val(),
    _txt_IMPACC1_DR_Cust_Acc4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc4").val(),

    _txt_IMPACC1_DR_Code5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code5").val(),
    _txt_IMPACC1_DR_AC_Short_Name5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name5").val(),
    _txt_IMPACC1_DR_Cust_abbr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr5").val(),
    _txt_IMPACC1_DR_Cust_Acc5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc5").val(),

    _txt_IMPACC1_DR_Code6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code6").val(),
    _txt_IMPACC1_DR_AC_Short_Name6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name6").val(),
    _txt_IMPACC1_DR_Cust_abbr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr6").val(),
    _txt_IMPACC1_DR_Cust_Acc6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc6").val();


    }

    var _chk_IMPACC2Flag = 'N', _txt_IMPACC2_FCRefNo = '',
        _txt_IMPACC2_DiscAmt = '', _txt_IMPACC2_DiscExchRate = '',
        _txt_IMPACC2_Princ_matu = '', _txt_IMPACC2_Princ_lump = '', _txt_IMPACC2_Princ_Contract_no = '', _txt_IMPACC2_Princ_Ex_Curr = '', _txt_IMPACC2_Princ_Ex_rate = '', _txt_IMPACC2_Princ_Intnl_Ex_rate = '',
        _txt_IMPACC2_Interest_matu = '', _txt_IMPACC2_Interest_lump = '', _txt_IMPACC2_Interest_Contract_no = '', _txt_IMPACC2_Interest_Ex_Curr = '', _txt_IMPACC2_Interest_Ex_rate = '', _txt_IMPACC2_Interest_Intnl_Ex_rate = '',
        _txt_IMPACC2_Commission_matu = '', _txt_IMPACC2_Commission_lump = '', _txt_IMPACC2_Commission_Contract_no = '', _txt_IMPACC2_Commission_Ex_Curr = '', _txt_IMPACC2_Commission_Ex_rate = '', _txt_IMPACC2_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC2_Their_Commission_matu = '', _txt_IMPACC2_Their_Commission_lump = '', _txt_IMPACC2_Their_Commission_Contract_no = '', _txt_IMPACC2_Their_Commission_Ex_Curr = '', _txt_IMPACC2_Their_Commission_Ex_rate = '', _txt_IMPACC2_Their_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC2_CR_Code = '', _txt_IMPACC2_CR_AC_Short_Name = '', _txt_IMPACC2_CR_Cust_abbr = '', _txt_IMPACC2_CR_Cust_Acc = '', _txt_IMPACC2_CR_Acceptance_Curr = '', _txt_IMPACC2_CR_Acceptance_amt = '', _txt_IMPACC2_CR_Acceptance_payer = '',
        _txt_IMPACC2_CR_Interest_Curr = '', _txt_IMPACC2_CR_Interest_amt = '', _txt_IMPACC2_CR_Interest_payer = '',
        _txt_IMPACC2_CR_Accept_Commission_Curr = '', _txt_IMPACC2_CR_Accept_Commission_amt = '', _txt_IMPACC2_CR_Accept_Commission_Payer = '',
        _txt_IMPACC2_CR_Pay_Handle_Commission_Curr = '', _txt_IMPACC2_CR_Pay_Handle_Commission_amt = '', _txt_IMPACC2_CR_Pay_Handle_Commission_Payer = '',
        _txt_IMPACC2_CR_Others_Curr = '', _txt_IMPACC2_CR_Others_amt = '', _txt_IMPACC2_CR_Others_Payer = '',
        _txt_IMPACC2_CR_Their_Commission_Curr = '', _txt_IMPACC2_CR_Their_Commission_amt = '', _txt_IMPACC2_CR_Their_Commission_Payer = '',
        _txt_IMPACC2_DR_Code = '', _txt_IMPACC2_DR_AC_Short_Name = '', _txt_IMPACC2_DR_Cust_abbr = '', _txt_IMPACC2_DR_Cust_Acc = '',
        _txt_IMPACC2_DR_Cur_Acc_Curr = '', _txt_IMPACC2_DR_Cur_Acc_amt = '', _txt_IMPACC2_DR_Cur_Acc_payer = '',
        _txt_IMPACC2_DR_Cur_Acc_Curr2 = '', _txt_IMPACC2_DR_Cur_Acc_amt2 = '', _txt_IMPACC2_DR_Cur_Acc_payer2 = '',
        _txt_IMPACC2_DR_Cur_Acc_Curr3 = '', _txt_IMPACC2_DR_Cur_Acc_amt3 = '', _txt_IMPACC2_DR_Cur_Acc_payer3 = '',
        _txt_IMPACC2_DR_Cur_Acc_Curr4 = '', _txt_IMPACC2_DR_Cur_Acc_amt4 = '', _txt_IMPACC2_DR_Cur_Acc_payer4 = '',
        _txt_IMPACC2_DR_Cur_Acc_Curr5 = '', _txt_IMPACC2_DR_Cur_Acc_amt5 = '', _txt_IMPACC2_DR_Cur_Acc_payer5 = '',
        _txt_IMPACC2_DR_Cur_Acc_Curr6 = '', _txt_IMPACC2_DR_Cur_Acc_amt6 = '', _txt_IMPACC2_DR_Cur_Acc_payer6 = '',

        _txt_IMPACC2_DR_Code2 = '', _txt_IMPACC2_DR_AC_Short_Name2 = '', _txt_IMPACC2_DR_Cust_abbr2 = '', _txt_IMPACC2_DR_Cust_Acc2 = '',
        _txt_IMPACC2_DR_Code3 = '', _txt_IMPACC2_DR_AC_Short_Name3 = '', _txt_IMPACC2_DR_Cust_abbr3 = '', _txt_IMPACC2_DR_Cust_Acc3 = '',
        _txt_IMPACC2_DR_Code4 = '', _txt_IMPACC2_DR_AC_Short_Name4 = '', _txt_IMPACC2_DR_Cust_abbr4 = '', _txt_IMPACC2_DR_Cust_Acc4 = '',
        _txt_IMPACC2_DR_Code5 = '', _txt_IMPACC2_DR_AC_Short_Name5 = '', _txt_IMPACC2_DR_Cust_abbr5 = '', _txt_IMPACC2_DR_Cust_Acc5 = '',
        _txt_IMPACC2_DR_Code6 = '', _txt_IMPACC2_DR_AC_Short_Name6 = '', _txt_IMPACC2_DR_Cust_abbr6 = '', _txt_IMPACC2_DR_Cust_Acc6 = '';


    if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_chk_IMPACC2Flag").is(':checked')) {
        _chk_IMPACC2Flag = 'Y',
         _txt_IMPACC2_FCRefNo = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_FCRefNo").val(),
    _txt_IMPACC2_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DiscAmt").val(),
    _txt_IMPACC2_Princ_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_matu").val(),
    _txt_IMPACC2_Princ_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_lump").val(),
    _txt_IMPACC2_Princ_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Contract_no").val(),
    _txt_IMPACC2_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_Curr").val(),
    _txt_IMPACC2_Princ_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_rate").val(),
    _txt_IMPACC2_Princ_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Intnl_Ex_rate").val(),

    _txt_IMPACC2_Interest_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Interest_matu").val(),
    _txt_IMPACC2_Interest_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Interest_lump").val(),
    _txt_IMPACC2_Interest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Interest_Contract_no").val(),
    _txt_IMPACC2_Interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Interest_Ex_Curr").val(),
    _txt_IMPACC2_Interest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Interest_Ex_rate").val(),
    _txt_IMPACC2_Interest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Interest_Intnl_Ex_rate").val(),

    _txt_IMPACC2_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Commission_matu").val(),
    _txt_IMPACC2_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Commission_lump").val(),
    _txt_IMPACC2_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Commission_Contract_no").val(),
    _txt_IMPACC2_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Commission_Ex_Curr").val(),
    _txt_IMPACC2_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Commission_Ex_rate").val(),
    _txt_IMPACC2_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC2_Their_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Their_Commission_matu").val(),
    _txt_IMPACC2_Their_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Their_Commission_lump").val(),
    _txt_IMPACC2_Their_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Their_Commission_Contract_no").val(),
    _txt_IMPACC2_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Their_Commission_Ex_Curr").val(),
    _txt_IMPACC2_Their_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Their_Commission_Ex_rate").val(),
    _txt_IMPACC2_Their_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Their_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC2_CR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Code").val(),
    _txt_IMPACC2_CR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_AC_Short_Name").val(),
    _txt_IMPACC2_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Cust_abbr").val(),
    _txt_IMPACC2_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Cust_Acc").val(),
    _txt_IMPACC2_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Acceptance_Curr").val(),
    _txt_IMPACC2_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Acceptance_amt").val(),
    _txt_IMPACC2_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Acceptance_payer").val(),

    _txt_IMPACC2_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Interest_Curr").val(),
    _txt_IMPACC2_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Interest_amt").val(),
    _txt_IMPACC2_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Interest_payer").val(),

    _txt_IMPACC2_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Accept_Commission_Curr").val(),
    _txt_IMPACC2_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Accept_Commission_amt").val(),
    _txt_IMPACC2_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Accept_Commission_Payer").val(),

    _txt_IMPACC2_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Pay_Handle_Commission_Curr").val(),
    _txt_IMPACC2_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Pay_Handle_Commission_amt").val(),
    _txt_IMPACC2_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Pay_Handle_Commission_Payer").val(),

    _txt_IMPACC2_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Others_Curr").val(),
    _txt_IMPACC2_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Others_amt").val(),
    _txt_IMPACC2_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Others_Payer").val(),

    _txt_IMPACC2_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Their_Commission_Curr").val(),
    _txt_IMPACC2_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Their_Commission_amt").val(),
    _txt_IMPACC2_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Their_Commission_Payer").val(),

    _txt_IMPACC2_DR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code").val(),
    _txt_IMPACC2_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name").val(),
    _txt_IMPACC2_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr").val(),
    _txt_IMPACC2_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc").val(),
    _txt_IMPACC2_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr").val(),
    _txt_IMPACC2_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_amt").val(),
    _txt_IMPACC2_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_payer").val(),

    _txt_IMPACC2_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr2").val(),
    _txt_IMPACC2_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_amt2").val(),
    _txt_IMPACC2_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_payer2").val(),

    _txt_IMPACC2_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr3").val(),
    _txt_IMPACC2_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_amt3").val(),
    _txt_IMPACC2_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_payer3").val(),

    _txt_IMPACC2_DR_Cur_Acc_Curr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr4").val(),
    _txt_IMPACC2_DR_Cur_Acc_amt4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_amt4").val(),
    _txt_IMPACC2_DR_Cur_Acc_payer4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_payer4").val(),

	_txt_IMPACC2_DR_Cur_Acc_Curr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr5").val(),
    _txt_IMPACC2_DR_Cur_Acc_amt5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_amt5").val(),
    _txt_IMPACC2_DR_Cur_Acc_payer5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_payer5").val(),

	_txt_IMPACC2_DR_Cur_Acc_Curr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr6").val(),
    _txt_IMPACC2_DR_Cur_Acc_amt6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_amt6").val(),
    _txt_IMPACC2_DR_Cur_Acc_payer6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_payer6").val(),

    _txt_IMPACC2_DR_Code2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code2").val(),
    _txt_IMPACC2_DR_AC_Short_Name2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name2").val(),
    _txt_IMPACC2_DR_Cust_abbr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr2").val(),
    _txt_IMPACC2_DR_Cust_Acc2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc2").val(),

    _txt_IMPACC2_DR_Code3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code3").val(),
    _txt_IMPACC2_DR_AC_Short_Name3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name3").val(),
    _txt_IMPACC2_DR_Cust_abbr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr3").val(),
    _txt_IMPACC2_DR_Cust_Acc3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc3").val(),

    _txt_IMPACC2_DR_Code4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code4").val(),
    _txt_IMPACC2_DR_AC_Short_Name4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name4").val(),
    _txt_IMPACC2_DR_Cust_abbr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr4").val(),
    _txt_IMPACC2_DR_Cust_Acc4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc4").val(),

    _txt_IMPACC2_DR_Code5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code5").val(),
    _txt_IMPACC2_DR_AC_Short_Name5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name5").val(),
    _txt_IMPACC2_DR_Cust_abbr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr5").val(),
    _txt_IMPACC2_DR_Cust_Acc5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc5").val(),

    _txt_IMPACC2_DR_Code6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code6").val(),
    _txt_IMPACC2_DR_AC_Short_Name6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name6").val(),
    _txt_IMPACC2_DR_Cust_abbr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr6").val(),
    _txt_IMPACC2_DR_Cust_Acc6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc6").val();

    }

    var _chk_IMPACC3Flag = 'N', _txt_IMPACC3_FCRefNo = '',
        _txt_IMPACC3_DiscAmt = '', _txt_IMPACC3_DiscExchRate = '',
        _txt_IMPACC3_Princ_matu = '', _txt_IMPACC3_Princ_lump = '', _txt_IMPACC3_Princ_Contract_no = '', _txt_IMPACC3_Princ_Ex_Curr = '', _txt_IMPACC3_Princ_Ex_rate = '', _txt_IMPACC3_Princ_Intnl_Ex_rate = '',
        _txt_IMPACC3_Interest_matu = '', _txt_IMPACC3_Interest_lump = '', _txt_IMPACC3_Interest_Contract_no = '', _txt_IMPACC3_Interest_Ex_Curr = '', _txt_IMPACC3_Interest_Ex_rate = '', _txt_IMPACC3_Interest_Intnl_Ex_rate = '',
        _txt_IMPACC3_Commission_matu = '', _txt_IMPACC3_Commission_lump = '', _txt_IMPACC3_Commission_Contract_no = '', _txt_IMPACC3_Commission_Ex_Curr = '', _txt_IMPACC3_Commission_Ex_rate = '', _txt_IMPACC3_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC3_Their_Commission_matu = '', _txt_IMPACC3_Their_Commission_lump = '', _txt_IMPACC3_Their_Commission_Contract_no = '', _txt_IMPACC3_Their_Commission_Ex_Curr = '', _txt_IMPACC3_Their_Commission_Ex_rate = '', _txt_IMPACC3_Their_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC3_CR_Code = '', _txt_IMPACC3_CR_AC_Short_Name = '', _txt_IMPACC3_CR_Cust_abbr = '', _txt_IMPACC3_CR_Cust_Acc = '', _txt_IMPACC3_CR_Acceptance_Curr = '', _txt_IMPACC3_CR_Acceptance_amt = '', _txt_IMPACC3_CR_Acceptance_payer = '',
        _txt_IMPACC3_CR_Interest_Curr = '', _txt_IMPACC3_CR_Interest_amt = '', _txt_IMPACC3_CR_Interest_payer = '',
        _txt_IMPACC3_CR_Accept_Commission_Curr = '', _txt_IMPACC3_CR_Accept_Commission_amt = '', _txt_IMPACC3_CR_Accept_Commission_Payer = '',
        _txt_IMPACC3_CR_Pay_Handle_Commission_Curr = '', _txt_IMPACC3_CR_Pay_Handle_Commission_amt = '', _txt_IMPACC3_CR_Pay_Handle_Commission_Payer = '',
        _txt_IMPACC3_CR_Others_Curr = '', _txt_IMPACC3_CR_Others_amt = '', _txt_IMPACC3_CR_Others_Payer = '',
        _txt_IMPACC3_CR_Their_Commission_Curr = '', _txt_IMPACC3_CR_Their_Commission_amt = '', _txt_IMPACC3_CR_Their_Commission_Payer = '',
        _txt_IMPACC3_DR_Code = '', _txt_IMPACC3_DR_AC_Short_Name = '', _txt_IMPACC3_DR_Cust_abbr = '', _txt_IMPACC3_DR_Cust_Acc = '',
        _txt_IMPACC3_DR_Cur_Acc_Curr = '', _txt_IMPACC3_DR_Cur_Acc_amt = '', _txt_IMPACC3_DR_Cur_Acc_payer = '',
        _txt_IMPACC3_DR_Cur_Acc_Curr2 = '', _txt_IMPACC3_DR_Cur_Acc_amt2 = '', _txt_IMPACC3_DR_Cur_Acc_payer2 = '',
        _txt_IMPACC3_DR_Cur_Acc_Curr3 = '', _txt_IMPACC3_DR_Cur_Acc_amt3 = '', _txt_IMPACC3_DR_Cur_Acc_payer3 = '',
        _txt_IMPACC3_DR_Cur_Acc_Curr4 = '', _txt_IMPACC3_DR_Cur_Acc_amt4 = '', _txt_IMPACC3_DR_Cur_Acc_payer4 = '',
        _txt_IMPACC3_DR_Cur_Acc_Curr5 = '', _txt_IMPACC3_DR_Cur_Acc_amt5 = '', _txt_IMPACC3_DR_Cur_Acc_payer5 = '',
        _txt_IMPACC3_DR_Cur_Acc_Curr6 = '', _txt_IMPACC3_DR_Cur_Acc_amt6 = '', _txt_IMPACC3_DR_Cur_Acc_payer6 = '',

        _txt_IMPACC3_DR_Code2 = '', _txt_IMPACC3_DR_AC_Short_Name2 = '', _txt_IMPACC3_DR_Cust_abbr2 = '', _txt_IMPACC3_DR_Cust_Acc2 = '',
        _txt_IMPACC3_DR_Code3 = '', _txt_IMPACC3_DR_AC_Short_Name3 = '', _txt_IMPACC3_DR_Cust_abbr3 = '', _txt_IMPACC3_DR_Cust_Acc3 = '',
        _txt_IMPACC3_DR_Code4 = '', _txt_IMPACC3_DR_AC_Short_Name4 = '', _txt_IMPACC3_DR_Cust_abbr4 = '', _txt_IMPACC3_DR_Cust_Acc4 = '',
        _txt_IMPACC3_DR_Code5 = '', _txt_IMPACC3_DR_AC_Short_Name5 = '', _txt_IMPACC3_DR_Cust_abbr5 = '', _txt_IMPACC3_DR_Cust_Acc5 = '',
        _txt_IMPACC3_DR_Code6 = '', _txt_IMPACC3_DR_AC_Short_Name6 = '', _txt_IMPACC3_DR_Cust_abbr6 = '', _txt_IMPACC3_DR_Cust_Acc6 = '';


    if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_chk_IMPACC3Flag").is(':checked')) {
        _chk_IMPACC3Flag = 'Y',
        _txt_IMPACC3_FCRefNo = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_FCRefNo").val(),
        _txt_IMPACC3_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DiscAmt").val(),
    _txt_IMPACC3_Princ_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_matu").val(),
    _txt_IMPACC3_Princ_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_lump").val(),
    _txt_IMPACC3_Princ_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Contract_no").val(),
    _txt_IMPACC3_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_Curr").val(),
    _txt_IMPACC3_Princ_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_rate").val(),
    _txt_IMPACC3_Princ_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Intnl_Ex_rate").val(),

    _txt_IMPACC3_Interest_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Interest_matu").val(),
    _txt_IMPACC3_Interest_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Interest_lump").val(),
    _txt_IMPACC3_Interest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Interest_Contract_no").val(),
    _txt_IMPACC3_Interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Interest_Ex_Curr").val(),
    _txt_IMPACC3_Interest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Interest_Ex_rate").val(),
    _txt_IMPACC3_Interest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Interest_Intnl_Ex_rate").val(),

    _txt_IMPACC3_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Commission_matu").val(),
    _txt_IMPACC3_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Commission_lump").val(),
    _txt_IMPACC3_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Commission_Contract_no").val(),
    _txt_IMPACC3_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Commission_Ex_Curr").val(),
    _txt_IMPACC3_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Commission_Ex_rate").val(),
    _txt_IMPACC3_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC3_Their_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Their_Commission_matu").val(),
    _txt_IMPACC3_Their_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Their_Commission_lump").val(),
    _txt_IMPACC3_Their_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Their_Commission_Contract_no").val(),
    _txt_IMPACC3_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Their_Commission_Ex_Curr").val(),
    _txt_IMPACC3_Their_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Their_Commission_Ex_rate").val(),
    _txt_IMPACC3_Their_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Their_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC3_CR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Code").val(),
    _txt_IMPACC3_CR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_AC_Short_Name").val(),
    _txt_IMPACC3_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Cust_abbr").val(),
    _txt_IMPACC3_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Cust_Acc").val(),
    _txt_IMPACC3_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Acceptance_Curr").val(),
    _txt_IMPACC3_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Acceptance_amt").val(),
    _txt_IMPACC3_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Acceptance_payer").val(),

    _txt_IMPACC3_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Interest_Curr").val(),
    _txt_IMPACC3_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Interest_amt").val(),
    _txt_IMPACC3_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Interest_payer").val(),

    _txt_IMPACC3_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Accept_Commission_Curr").val(),
    _txt_IMPACC3_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Accept_Commission_amt").val(),
    _txt_IMPACC3_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Accept_Commission_Payer").val(),

    _txt_IMPACC3_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Pay_Handle_Commission_Curr").val(),
    _txt_IMPACC3_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Pay_Handle_Commission_amt").val(),
    _txt_IMPACC3_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Pay_Handle_Commission_Payer").val(),

    _txt_IMPACC3_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Others_Curr").val(),
    _txt_IMPACC3_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Others_amt").val(),
    _txt_IMPACC3_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Others_Payer").val(),

    _txt_IMPACC3_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Their_Commission_Curr").val(),
    _txt_IMPACC3_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Their_Commission_amt").val(),
    _txt_IMPACC3_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Their_Commission_Payer").val(),

    _txt_IMPACC3_DR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code").val(),
    _txt_IMPACC3_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name").val(),
    _txt_IMPACC3_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr").val(),
    _txt_IMPACC3_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc").val(),
    _txt_IMPACC3_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr").val(),
    _txt_IMPACC3_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_amt").val(),
    _txt_IMPACC3_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_payer").val(),

    _txt_IMPACC3_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr2").val(),
    _txt_IMPACC3_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_amt2").val(),
    _txt_IMPACC3_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_payer2").val(),

    _txt_IMPACC3_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr3").val(),
    _txt_IMPACC3_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_amt3").val(),
    _txt_IMPACC3_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_payer3").val(),

    _txt_IMPACC3_DR_Cur_Acc_Curr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr4").val(),
    _txt_IMPACC3_DR_Cur_Acc_amt4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_amt4").val(),
    _txt_IMPACC3_DR_Cur_Acc_payer4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_payer4").val(),

	_txt_IMPACC3_DR_Cur_Acc_Curr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr5").val(),
    _txt_IMPACC3_DR_Cur_Acc_amt5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_amt5").val(),
    _txt_IMPACC3_DR_Cur_Acc_payer5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_payer5").val(),

	_txt_IMPACC3_DR_Cur_Acc_Curr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr6").val(),
    _txt_IMPACC3_DR_Cur_Acc_amt6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_amt6").val(),
    _txt_IMPACC3_DR_Cur_Acc_payer6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_payer6").val(),

	_txt_IMPACC3_DR_Code2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code2").val(),
    _txt_IMPACC3_DR_AC_Short_Name2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name2").val(),
    _txt_IMPACC3_DR_Cust_abbr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr2").val(),
    _txt_IMPACC3_DR_Cust_Acc2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc2").val(),

    _txt_IMPACC3_DR_Code3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code3").val(),
    _txt_IMPACC3_DR_AC_Short_Name3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name3").val(),
    _txt_IMPACC3_DR_Cust_abbr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr3").val(),
    _txt_IMPACC3_DR_Cust_Acc3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc3").val(),

    _txt_IMPACC3_DR_Code4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code4").val(),
    _txt_IMPACC3_DR_AC_Short_Name4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name4").val(),
    _txt_IMPACC3_DR_Cust_abbr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr4").val(),
    _txt_IMPACC3_DR_Cust_Acc4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc4").val(),

    _txt_IMPACC3_DR_Code5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code5").val(),
    _txt_IMPACC3_DR_AC_Short_Name5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name5").val(),
    _txt_IMPACC3_DR_Cust_abbr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr5").val(),
    _txt_IMPACC3_DR_Cust_Acc5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc5").val(),

    _txt_IMPACC3_DR_Code6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code6").val(),
    _txt_IMPACC3_DR_AC_Short_Name6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name6").val(),
    _txt_IMPACC3_DR_Cust_abbr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr6").val(),
    _txt_IMPACC3_DR_Cust_Acc6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc6").val();

    }

    var _chk_IMPACC4Flag = 'N', _txt_IMPACC4_FCRefNo = '',
        _txt_IMPACC4_DiscAmt = '', _txt_IMPACC4_DiscExchRate = '',
        _txt_IMPACC4_Princ_matu = '', _txt_IMPACC4_Princ_lump = '', _txt_IMPACC4_Princ_Contract_no = '', _txt_IMPACC4_Princ_Ex_Curr = '', _txt_IMPACC4_Princ_Ex_rate = '', _txt_IMPACC4_Princ_Intnl_Ex_rate = '',
        _txt_IMPACC4_Interest_matu = '', _txt_IMPACC4_Interest_lump = '', _txt_IMPACC4_Interest_Contract_no = '', _txt_IMPACC4_Interest_Ex_Curr = '', _txt_IMPACC4_Interest_Ex_rate = '', _txt_IMPACC4_Interest_Intnl_Ex_rate = '',
        _txt_IMPACC4_Commission_matu = '', _txt_IMPACC4_Commission_lump = '', _txt_IMPACC4_Commission_Contract_no = '', _txt_IMPACC4_Commission_Ex_Curr = '', _txt_IMPACC4_Commission_Ex_rate = '', _txt_IMPACC4_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC4_Their_Commission_matu = '', _txt_IMPACC4_Their_Commission_lump = '', _txt_IMPACC4_Their_Commission_Contract_no = '', _txt_IMPACC4_Their_Commission_Ex_Curr = '', _txt_IMPACC4_Their_Commission_Ex_rate = '', _txt_IMPACC4_Their_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC4_CR_Code = '', _txt_IMPACC4_CR_AC_Short_Name = '', _txt_IMPACC4_CR_Cust_abbr = '', _txt_IMPACC4_CR_Cust_Acc = '', _txt_IMPACC4_CR_Acceptance_Curr = '', _txt_IMPACC4_CR_Acceptance_amt = '', _txt_IMPACC4_CR_Acceptance_payer = '',
        _txt_IMPACC4_CR_Interest_Curr = '', _txt_IMPACC4_CR_Interest_amt = '', _txt_IMPACC4_CR_Interest_payer = '',
        _txt_IMPACC4_CR_Accept_Commission_Curr = '', _txt_IMPACC4_CR_Accept_Commission_amt = '', _txt_IMPACC4_CR_Accept_Commission_Payer = '',
        _txt_IMPACC4_CR_Pay_Handle_Commission_Curr = '', _txt_IMPACC4_CR_Pay_Handle_Commission_amt = '', _txt_IMPACC4_CR_Pay_Handle_Commission_Payer = '',
        _txt_IMPACC4_CR_Others_Curr = '', _txt_IMPACC4_CR_Others_amt = '', _txt_IMPACC4_CR_Others_Payer = '',
        _txt_IMPACC4_CR_Their_Commission_Curr = '', _txt_IMPACC4_CR_Their_Commission_amt = '', _txt_IMPACC4_CR_Their_Commission_Payer = '',
        _txt_IMPACC4_DR_Code = '', _txt_IMPACC4_DR_AC_Short_Name = '', _txt_IMPACC4_DR_Cust_abbr = '', _txt_IMPACC4_DR_Cust_Acc = '',
        _txt_IMPACC4_DR_Cur_Acc_Curr = '', _txt_IMPACC4_DR_Cur_Acc_amt = '', _txt_IMPACC4_DR_Cur_Acc_payer = '',
        _txt_IMPACC4_DR_Cur_Acc_Curr2 = '', _txt_IMPACC4_DR_Cur_Acc_amt2 = '', _txt_IMPACC4_DR_Cur_Acc_payer2 = '',
        _txt_IMPACC4_DR_Cur_Acc_Curr3 = '', _txt_IMPACC4_DR_Cur_Acc_amt3 = '', _txt_IMPACC4_DR_Cur_Acc_payer3 = '',
        _txt_IMPACC4_DR_Cur_Acc_Curr4 = '', _txt_IMPACC4_DR_Cur_Acc_amt4 = '', _txt_IMPACC4_DR_Cur_Acc_payer4 = '',
        _txt_IMPACC4_DR_Cur_Acc_Curr5 = '', _txt_IMPACC4_DR_Cur_Acc_amt5 = '', _txt_IMPACC4_DR_Cur_Acc_payer5 = '',
        _txt_IMPACC4_DR_Cur_Acc_Curr6 = '', _txt_IMPACC4_DR_Cur_Acc_amt6 = '', _txt_IMPACC4_DR_Cur_Acc_payer6 = '',

        _txt_IMPACC4_DR_Code2 = '', _txt_IMPACC4_DR_AC_Short_Name2 = '', _txt_IMPACC4_DR_Cust_abbr2 = '', _txt_IMPACC4_DR_Cust_Acc2 = '',
        _txt_IMPACC4_DR_Code3 = '', _txt_IMPACC4_DR_AC_Short_Name3 = '', _txt_IMPACC4_DR_Cust_abbr3 = '', _txt_IMPACC4_DR_Cust_Acc3 = '',
        _txt_IMPACC4_DR_Code4 = '', _txt_IMPACC4_DR_AC_Short_Name4 = '', _txt_IMPACC4_DR_Cust_abbr4 = '', _txt_IMPACC4_DR_Cust_Acc4 = '',
        _txt_IMPACC4_DR_Code5 = '', _txt_IMPACC4_DR_AC_Short_Name5 = '', _txt_IMPACC4_DR_Cust_abbr5 = '', _txt_IMPACC4_DR_Cust_Acc5 = '',
        _txt_IMPACC4_DR_Code6 = '', _txt_IMPACC4_DR_AC_Short_Name6 = '', _txt_IMPACC4_DR_Cust_abbr6 = '', _txt_IMPACC4_DR_Cust_Acc6 = '';

    if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_chk_IMPACC4Flag").is(':checked')) {
        _chk_IMPACC4Flag = 'Y',
        _txt_IMPACC4_FCRefNo = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_FCRefNo").val(),
        _txt_IMPACC4_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DiscAmt").val(),
    _txt_IMPACC4_Princ_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_matu").val(),
    _txt_IMPACC4_Princ_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_lump").val(),
    _txt_IMPACC4_Princ_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Contract_no").val(),
    _txt_IMPACC4_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_Curr").val(),
    _txt_IMPACC4_Princ_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_rate").val(),
    _txt_IMPACC4_Princ_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Intnl_Ex_rate").val(),

    _txt_IMPACC4_Interest_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Interest_matu").val(),
    _txt_IMPACC4_Interest_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Interest_lump").val(),
    _txt_IMPACC4_Interest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Interest_Contract_no").val(),
    _txt_IMPACC4_Interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Interest_Ex_Curr").val(),
    _txt_IMPACC4_Interest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Interest_Ex_rate").val(),
    _txt_IMPACC4_Interest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Interest_Intnl_Ex_rate").val(),

    _txt_IMPACC4_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Commission_matu").val(),
    _txt_IMPACC4_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Commission_lump").val(),
    _txt_IMPACC4_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Commission_Contract_no").val(),
    _txt_IMPACC4_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Commission_Ex_Curr").val(),
    _txt_IMPACC4_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Commission_Ex_rate").val(),
    _txt_IMPACC4_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC4_Their_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Their_Commission_matu").val(),
    _txt_IMPACC4_Their_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Their_Commission_lump").val(),
    _txt_IMPACC4_Their_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Their_Commission_Contract_no").val(),
    _txt_IMPACC4_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Their_Commission_Ex_Curr").val(),
    _txt_IMPACC4_Their_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Their_Commission_Ex_rate").val(),
    _txt_IMPACC4_Their_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Their_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC4_CR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Code").val(),
    _txt_IMPACC4_CR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_AC_Short_Name").val(),
    _txt_IMPACC4_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Cust_abbr").val(),
    _txt_IMPACC4_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Cust_Acc").val(),
    _txt_IMPACC4_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Acceptance_Curr").val(),
    _txt_IMPACC4_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Acceptance_amt").val(),
    _txt_IMPACC4_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Acceptance_payer").val(),

    _txt_IMPACC4_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Interest_Curr").val(),
    _txt_IMPACC4_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Interest_amt").val(),
    _txt_IMPACC4_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Interest_payer").val(),

    _txt_IMPACC4_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Accept_Commission_Curr").val(),
    _txt_IMPACC4_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Accept_Commission_amt").val(),
    _txt_IMPACC4_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Accept_Commission_Payer").val(),

    _txt_IMPACC4_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Pay_Handle_Commission_Curr").val(),
    _txt_IMPACC4_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Pay_Handle_Commission_amt").val(),
    _txt_IMPACC4_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Pay_Handle_Commission_Payer").val(),

    _txt_IMPACC4_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Others_Curr").val(),
    _txt_IMPACC4_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Others_amt").val(),
    _txt_IMPACC4_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Others_Payer").val(),

    _txt_IMPACC4_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Their_Commission_Curr").val(),
    _txt_IMPACC4_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Their_Commission_amt").val(),
    _txt_IMPACC4_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Their_Commission_Payer").val(),

    _txt_IMPACC4_DR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code").val(),
    _txt_IMPACC4_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name").val(),
    _txt_IMPACC4_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr").val(),
    _txt_IMPACC4_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc").val(),
    _txt_IMPACC4_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr").val(),
    _txt_IMPACC4_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_amt").val(),
    _txt_IMPACC4_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_payer").val(),

    _txt_IMPACC4_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr2").val(),
    _txt_IMPACC4_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_amt2").val(),
    _txt_IMPACC4_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_payer2").val(),

    _txt_IMPACC4_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr3").val(),
    _txt_IMPACC4_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_amt3").val(),
    _txt_IMPACC4_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_payer3").val(),

    _txt_IMPACC4_DR_Cur_Acc_Curr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr4").val(),
    _txt_IMPACC4_DR_Cur_Acc_amt4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_amt4").val(),
    _txt_IMPACC4_DR_Cur_Acc_payer4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_payer4").val(),

	_txt_IMPACC4_DR_Cur_Acc_Curr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr5").val(),
    _txt_IMPACC4_DR_Cur_Acc_amt5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_amt5").val(),
    _txt_IMPACC4_DR_Cur_Acc_payer5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_payer5").val(),

	_txt_IMPACC4_DR_Cur_Acc_Curr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr6").val(),
    _txt_IMPACC4_DR_Cur_Acc_amt6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_amt6").val(),
    _txt_IMPACC4_DR_Cur_Acc_payer6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_payer6").val(),


	_txt_IMPACC4_DR_Code2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code2").val(),
    _txt_IMPACC4_DR_AC_Short_Name2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name2").val(),
    _txt_IMPACC4_DR_Cust_abbr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr2").val(),
    _txt_IMPACC4_DR_Cust_Acc2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc2").val(),

    _txt_IMPACC4_DR_Code3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code3").val(),
    _txt_IMPACC4_DR_AC_Short_Name3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name3").val(),
    _txt_IMPACC4_DR_Cust_abbr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr3").val(),
    _txt_IMPACC4_DR_Cust_Acc3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc3").val(),

    _txt_IMPACC4_DR_Code4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code4").val(),
    _txt_IMPACC4_DR_AC_Short_Name4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name4").val(),
    _txt_IMPACC4_DR_Cust_abbr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr4").val(),
    _txt_IMPACC4_DR_Cust_Acc4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc4").val(),

    _txt_IMPACC4_DR_Code5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code6").val(),
    _txt_IMPACC4_DR_AC_Short_Name5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name6").val(),
    _txt_IMPACC4_DR_Cust_abbr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr6").val(),
    _txt_IMPACC4_DR_Cust_Acc5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc6").val(),

    _txt_IMPACC4_DR_Code6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code6").val(),
    _txt_IMPACC4_DR_AC_Short_Name6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name6").val(),
    _txt_IMPACC4_DR_Cust_abbr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr6").val(),
    _txt_IMPACC4_DR_Cust_Acc6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc6").val();

    }

    var _chk_IMPACC5Flag = 'N', _txt_IMPACC5_FCRefNo = '',
        _txt_IMPACC5_DiscAmt = '', _txt_IMPACC5_DiscExchRate = '',
        _txt_IMPACC5_Princ_matu = '', _txt_IMPACC5_Princ_lump = '', _txt_IMPACC5_Princ_Contract_no = '', _txt_IMPACC5_Princ_Ex_Curr = '', _txt_IMPACC5_Princ_Ex_rate = '', _txt_IMPACC5_Princ_Intnl_Ex_rate = '',
        _txt_IMPACC5_Interest_matu = '', _txt_IMPACC5_Interest_lump = '', _txt_IMPACC5_Interest_Contract_no = '', _txt_IMPACC5_Interest_Ex_Curr = '', _txt_IMPACC5_Interest_Ex_rate = '', _txt_IMPACC5_Interest_Intnl_Ex_rate = '',
        _txt_IMPACC5_Commission_matu = '', _txt_IMPACC5_Commission_lump = '', _txt_IMPACC5_Commission_Contract_no = '', _txt_IMPACC5_Commission_Ex_Curr = '', _txt_IMPACC5_Commission_Ex_rate = '', _txt_IMPACC5_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC5_Their_Commission_matu = '', _txt_IMPACC5_Their_Commission_lump = '', _txt_IMPACC5_Their_Commission_Contract_no = '', _txt_IMPACC5_Their_Commission_Ex_Curr = '', _txt_IMPACC5_Their_Commission_Ex_rate = '', _txt_IMPACC5_Their_Commission_Intnl_Ex_rate = '',
        _txt_IMPACC5_CR_Code = '', _txt_IMPACC5_CR_AC_Short_Name = '', _txt_IMPACC5_CR_Cust_abbr = '', _txt_IMPACC5_CR_Cust_Acc = '', _txt_IMPACC5_CR_Acceptance_Curr = '', _txt_IMPACC5_CR_Acceptance_amt = '', _txt_IMPACC5_CR_Acceptance_payer = '',
        _txt_IMPACC5_CR_Interest_Curr = '', _txt_IMPACC5_CR_Interest_amt = '', _txt_IMPACC5_CR_Interest_payer = '',
        _txt_IMPACC5_CR_Accept_Commission_Curr = '', _txt_IMPACC5_CR_Accept_Commission_amt = '', _txt_IMPACC5_CR_Accept_Commission_Payer = '',
        _txt_IMPACC5_CR_Pay_Handle_Commission_Curr = '', _txt_IMPACC5_CR_Pay_Handle_Commission_amt = '', _txt_IMPACC5_CR_Pay_Handle_Commission_Payer = '',
        _txt_IMPACC5_CR_Others_Curr = '', _txt_IMPACC5_CR_Others_amt = '', _txt_IMPACC5_CR_Others_Payer = '',
        _txt_IMPACC5_CR_Their_Commission_Curr = '', _txt_IMPACC5_CR_Their_Commission_amt = '', _txt_IMPACC5_CR_Their_Commission_Payer = '',
        _txt_IMPACC5_DR_Code = '', _txt_IMPACC5_DR_AC_Short_Name = '', _txt_IMPACC5_DR_Cust_abbr = '', _txt_IMPACC5_DR_Cust_Acc = '',
        _txt_IMPACC5_DR_Cur_Acc_Curr = '', _txt_IMPACC5_DR_Cur_Acc_amt = '', _txt_IMPACC5_DR_Cur_Acc_payer = '',
        _txt_IMPACC5_DR_Cur_Acc_Curr2 = '', _txt_IMPACC5_DR_Cur_Acc_amt2 = '', _txt_IMPACC5_DR_Cur_Acc_payer2 = '',
        _txt_IMPACC5_DR_Cur_Acc_Curr3 = '', _txt_IMPACC5_DR_Cur_Acc_amt3 = '', _txt_IMPACC5_DR_Cur_Acc_payer3 = '',
        _txt_IMPACC5_DR_Cur_Acc_Curr4 = '', _txt_IMPACC5_DR_Cur_Acc_amt4 = '', _txt_IMPACC5_DR_Cur_Acc_payer4 = '',
        _txt_IMPACC5_DR_Cur_Acc_Curr5 = '', _txt_IMPACC5_DR_Cur_Acc_amt5 = '', _txt_IMPACC5_DR_Cur_Acc_payer5 = '',
        _txt_IMPACC5_DR_Cur_Acc_Curr6 = '', _txt_IMPACC5_DR_Cur_Acc_amt6 = '', _txt_IMPACC5_DR_Cur_Acc_payer6 = '',

        _txt_IMPACC5_DR_Code2 = '', _txt_IMPACC5_DR_AC_Short_Name2 = '', _txt_IMPACC5_DR_Cust_abbr2 = '', _txt_IMPACC5_DR_Cust_Acc2 = '',
        _txt_IMPACC5_DR_Code3 = '', _txt_IMPACC5_DR_AC_Short_Name3 = '', _txt_IMPACC5_DR_Cust_abbr3 = '', _txt_IMPACC5_DR_Cust_Acc3 = '',
        _txt_IMPACC5_DR_Code4 = '', _txt_IMPACC5_DR_AC_Short_Name4 = '', _txt_IMPACC5_DR_Cust_abbr4 = '', _txt_IMPACC5_DR_Cust_Acc4 = '',
        _txt_IMPACC5_DR_Code5 = '', _txt_IMPACC5_DR_AC_Short_Name5 = '', _txt_IMPACC5_DR_Cust_abbr5 = '', _txt_IMPACC5_DR_Cust_Acc5 = '',
        _txt_IMPACC5_DR_Code6 = '', _txt_IMPACC5_DR_AC_Short_Name6 = '', _txt_IMPACC5_DR_Cust_abbr6 = '', _txt_IMPACC5_DR_Cust_Acc6 = '';

    if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_chk_IMPACC5Flag").is(':checked')) {
        _chk_IMPACC5Flag = 'Y',
        _txt_IMPACC5_FCRefNo = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_FCRefNo").val(),
    _txt_IMPACC5_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DiscAmt").val(),
    _txt_IMPACC5_Princ_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_matu").val(),
    _txt_IMPACC5_Princ_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_lump").val(),
    _txt_IMPACC5_Princ_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Contract_no").val(),
    _txt_IMPACC5_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_Curr").val(),
    _txt_IMPACC5_Princ_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_rate").val(),
    _txt_IMPACC5_Princ_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Intnl_Ex_rate").val(),

    _txt_IMPACC5_Interest_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Interest_matu").val(),
    _txt_IMPACC5_Interest_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Interest_lump").val(),
    _txt_IMPACC5_Interest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Interest_Contract_no").val(),
    _txt_IMPACC5_Interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Interest_Ex_Curr").val(),
    _txt_IMPACC5_Interest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Interest_Ex_rate").val(),
    _txt_IMPACC5_Interest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Interest_Intnl_Ex_rate").val(),

    _txt_IMPACC5_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Commission_matu").val(),
    _txt_IMPACC5_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Commission_lump").val(),
    _txt_IMPACC5_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Commission_Contract_no").val(),
    _txt_IMPACC5_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Commission_Ex_Curr").val(),
    _txt_IMPACC5_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Commission_Ex_rate").val(),
    _txt_IMPACC5_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC5_Their_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Their_Commission_matu").val(),
    _txt_IMPACC5_Their_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Their_Commission_lump").val(),
    _txt_IMPACC5_Their_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Their_Commission_Contract_no").val(),
    _txt_IMPACC5_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Their_Commission_Ex_Curr").val(),
    _txt_IMPACC5_Their_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Their_Commission_Ex_rate").val(),
    _txt_IMPACC5_Their_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Their_Commission_Intnl_Ex_rate").val(),

    _txt_IMPACC5_CR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Code").val(),
    _txt_IMPACC5_CR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_AC_Short_Name").val(),
    _txt_IMPACC5_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Cust_abbr").val(),
    _txt_IMPACC5_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Cust_Acc").val(),
    _txt_IMPACC5_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Acceptance_Curr").val(),
    _txt_IMPACC5_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Acceptance_amt").val(),
    _txt_IMPACC5_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Acceptance_payer").val(),

    _txt_IMPACC5_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Interest_Curr").val(),
    _txt_IMPACC5_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Interest_amt").val(),
    _txt_IMPACC5_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Interest_payer").val(),

    _txt_IMPACC5_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Accept_Commission_Curr").val(),
    _txt_IMPACC5_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Accept_Commission_amt").val(),
    _txt_IMPACC5_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Accept_Commission_Payer").val(),

    _txt_IMPACC5_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Pay_Handle_Commission_Curr").val(),
    _txt_IMPACC5_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Pay_Handle_Commission_amt").val(),
    _txt_IMPACC5_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Pay_Handle_Commission_Payer").val(),

    _txt_IMPACC5_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Others_Curr").val(),
    _txt_IMPACC5_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Others_amt").val(),
    _txt_IMPACC5_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Others_Payer").val(),

    _txt_IMPACC5_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Their_Commission_Curr").val(),
    _txt_IMPACC5_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Their_Commission_amt").val(),
    _txt_IMPACC5_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Their_Commission_Payer").val(),

    _txt_IMPACC5_DR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code").val(),
    _txt_IMPACC5_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name").val(),
    _txt_IMPACC5_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr").val(),
    _txt_IMPACC5_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc").val(),
    _txt_IMPACC5_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr").val(),
    _txt_IMPACC5_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_amt").val(),
    _txt_IMPACC5_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_payer").val(),

    _txt_IMPACC5_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr2").val(),
    _txt_IMPACC5_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_amt2").val(),
    _txt_IMPACC5_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_payer2").val(),

    _txt_IMPACC5_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr3").val(),
    _txt_IMPACC5_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_amt3").val(),
    _txt_IMPACC5_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_payer3").val(),

    _txt_IMPACC5_DR_Cur_Acc_Curr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr4").val(),
    _txt_IMPACC5_DR_Cur_Acc_amt4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_amt4").val(),
    _txt_IMPACC5_DR_Cur_Acc_payer4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_payer4").val(),

	_txt_IMPACC5_DR_Cur_Acc_Curr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr5").val(),
    _txt_IMPACC5_DR_Cur_Acc_amt5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_amt5").val(),
    _txt_IMPACC5_DR_Cur_Acc_payer5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_payer5").val(),

	_txt_IMPACC5_DR_Cur_Acc_Curr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr6").val(),
    _txt_IMPACC5_DR_Cur_Acc_amt6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_amt6").val(),
    _txt_IMPACC5_DR_Cur_Acc_payer6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_payer6").val(),

	_txt_IMPACC5_DR_Code2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code2").val(),
    _txt_IMPACC5_DR_AC_Short_Name2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name2").val(),
    _txt_IMPACC5_DR_Cust_abbr2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr2").val(),
    _txt_IMPACC5_DR_Cust_Acc2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc2").val(),

    _txt_IMPACC5_DR_Code3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code3").val(),
    _txt_IMPACC5_DR_AC_Short_Name3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name3").val(),
    _txt_IMPACC5_DR_Cust_abbr3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr3").val(),
    _txt_IMPACC5_DR_Cust_Acc3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc3").val(),

    _txt_IMPACC5_DR_Code4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code4").val(),
    _txt_IMPACC5_DR_AC_Short_Name4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name4").val(),
    _txt_IMPACC5_DR_Cust_abbr4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr4").val(),
    _txt_IMPACC5_DR_Cust_Acc4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc4").val(),

    _txt_IMPACC5_DR_Code5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code5").val(),
    _txt_IMPACC5_DR_AC_Short_Name5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name5").val(),
    _txt_IMPACC5_DR_Cust_abbr5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr5").val(),
    _txt_IMPACC5_DR_Cust_Acc5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc5").val(),

    _txt_IMPACC5_DR_Code6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code6").val(),
    _txt_IMPACC5_DR_AC_Short_Name6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name6").val(),
    _txt_IMPACC5_DR_Cust_abbr6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr6").val(),
    _txt_IMPACC5_DR_Cust_Acc6 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc6").val();

    }
    ///////////////// GENERAL OPRATOIN 1 /////////////////
    var _chk_GO1Flag = 'N',

	_txt_GO1_Left_Comment = '',
	_txt_GO1_Left_SectionNo = '', _txt_GO1_Left_Remarks = '', _txt_GO1_Left_Memo = '',
	_txt_GO1_Left_Scheme_no = '',

	_txt_GO1_Left_Debit_Code = '', _txt_GO1_Left_Debit_Curr = '', _txt_GO1_Left_Debit_Amt = '',
	_txt_GO1_Left_Debit_Cust = '', _txt_GO1_Left_Debit_Cust_Name = '',
	_txt_GO1_Left_Debit_Cust_AcCode = '', _txt_GO1_Left_Debit_Cust_AcCode_Name = '', _txt_GO1_Left_Debit_Cust_AccNo = '',
	_txt_GO1_Left_Debit_Exch_Rate = '', _txt_GO1_Left_Debit_Exch_CCY = '',
	_txt_GO1_Left_Debit_FUND = '', _txt_GO1_Left_Debit_Check_No = '', _txt_GO1_Left_Debit_Available = '',
	_txt_GO1_Left_Debit_AdPrint = '', _txt_GO1_Left_Debit_Details = '', _txt_GO1_Left_Debit_Entity = '',
	_txt_GO1_Left_Debit_Division = '', _txt_GO1_Left_Debit_Inter_Amount = '', _txt_GO1_Left_Debit_Inter_Rate = '',

	_txt_GO1_Left_Credit_Code = '', _txt_GO1_Left_Credit_Curr = '', _txt_GO1_Left_Credit_Amt = '',
    _txt_GO1_Left_Credit_Cust = '', _txt_GO1_Left_Credit_Cust_Name = '',
	_txt_GO1_Left_Credit_Cust_AcCode = '', _txt_GO1_Left_Credit_Cust_AcCode_Name = '', _txt_GO1_Left_Credit_Cust_AccNo = '',
    _txt_GO1_Left_Credit_Exch_Rate = '', _txt_GO1_Left_Credit_Exch_Curr = '',
	_txt_GO1_Left_Credit_FUND = '', _txt_GO1_Left_Credit_Check_No = '', _txt_GO1_Left_Credit_Available = '',
    _txt_GO1_Left_Credit_AdPrint = '', _txt_GO1_Left_Credit_Details = '', _txt_GO1_Left_Credit_Entity = '',
	_txt_GO1_Left_Credit_Division = '', _txt_GO1_Left_Credit_Inter_Amount = '', _txt_GO1_Left_Credit_Inter_Rate = '',

    _txt_GO1_Right_Comment = '',
	_txt_GO1_Right_SectionNo = '', _txt_GO1_Right_Remarks = '', _txt_GO1_Right_Memo = '',
	_txt_GO1_Right_Scheme_no = '',

	_txt_GO1_Right_Debit_Code = '', _txt_GO1_Right_Debit_Curr = '', _txt_GO1_Right_Debit_Amt = '',
	_txt_GO1_Right_Debit_Cust = '', _txt_GO1_Right_Debit_Cust_Name = '',
	_txt_GO1_Right_Debit_Cust_AcCode = '', _txt_GO1_Right_Debit_Cust_AcCode_Name = '', _txt_GO1_Right_Debit_Cust_AccNo = '',
	_txt_GO1_Right_Debit_Exch_Rate = '', _txt_GO1_Right_Debit_Exch_CCY = '',
	_txt_GO1_Right_Debit_FUND = '', _txt_GO1_Right_Debit_Check_No = '', _txt_GO1_Right_Debit_Available = '',
	_txt_GO1_Right_Debit_AdPrint = '', _txt_GO1_Right_Debit_Details = '', _txt_GO1_Right_Debit_Entity = '',
	_txt_GO1_Right_Debit_Division = '', _txt_GO1_Right_Debit_Inter_Amount = '', _txt_GO1_Right_Debit_Inter_Rate = '',

	_txt_GO1_Right_Credit_Code = '', _txt_GO1_Right_Credit_Curr = '', _txt_GO1_Right_Credit_Amt = '',
	_txt_GO1_Right_Credit_Cust = '', _txt_GO1_Right_Credit_Cust_Name = '',
	_txt_GO1_Right_Credit_Cust_AcCode = '', _txt_GO1_Right_Credit_Cust_AcCode_Name = '', _txt_GO1_Right_Credit_Cust_AccNo = '',
	_txt_GO1_Right_Credit_Exch_Rate = '', _txt_GO1_Right_Credit_Exch_Curr = '',
	_txt_GO1_Right_Credit_FUND = '', _txt_GO1_Right_Credit_Check_No = '', _txt_GO1_Right_Credit_Available = '',
	_txt_GO1_Right_Credit_AdPrint = '', _txt_GO1_Right_Credit_Details = '', _txt_GO1_Right_Credit_Entity = '',
	_txt_GO1_Right_Credit_Division = '', _txt_GO1_Right_Credit_Inter_Amount = '', _txt_GO1_Right_Credit_Inter_Rate = '';

    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        _chk_GO1Flag = 'Y',

        _txt_GO1_Left_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Comment").val(),
        _txt_GO1_Left_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_SectionNo").val(),
        _txt_GO1_Left_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Remarks").val(),
        _txt_GO1_Left_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Memo").val(),
        _txt_GO1_Left_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Scheme_no").val(),

        _txt_GO1_Left_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Code").val(),
        _txt_GO1_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val(),
        _txt_GO1_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Amt").val(),
        _txt_GO1_Left_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust").val(),
        _txt_GO1_Left_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_Name").val(),
        _txt_GO1_Left_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode").val(),
        _txt_GO1_Left_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AccNo").val(),
        _txt_GO1_Left_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode_Name").val(),
        _txt_GO1_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_Rate").val(),
        _txt_GO1_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_CCY").val(),
        _txt_GO1_Left_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_FUND").val(),
        _txt_GO1_Left_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Check_No").val(),
        _txt_GO1_Left_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Available").val(),
        _txt_GO1_Left_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_AdPrint").val(),
        _txt_GO1_Left_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Details").val(),
        _txt_GO1_Left_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Entity").val(),
        _txt_GO1_Left_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Division").val(),
        _txt_GO1_Left_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Inter_Amount").val(),
        _txt_GO1_Left_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Inter_Rate").val(),
        _txt_GO1_Left_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Code").val(),
        _txt_GO1_Left_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val(),
        _txt_GO1_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Amt").val(),
        _txt_GO1_Left_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust").val(),
        _txt_GO1_Left_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_Name").val(),
        _txt_GO1_Left_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode").val(),
        _txt_GO1_Left_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode_Name").val(),
        _txt_GO1_Left_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AccNo").val(),
        _txt_GO1_Left_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Rate").val(),
        _txt_GO1_Left_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Curr").val(),
        _txt_GO1_Left_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_FUND").val(),
        _txt_GO1_Left_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Check_No").val(),
        _txt_GO1_Left_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Available").val(),
        _txt_GO1_Left_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_AdPrint").val(),
        _txt_GO1_Left_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Details").val(),
        _txt_GO1_Left_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Entity").val(),
        _txt_GO1_Left_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Division").val(),
        _txt_GO1_Left_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Inter_Amount").val(),
        _txt_GO1_Left_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Inter_Rate").val(),

        _txt_GO1_Right_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Comment").val(),
        _txt_GO1_Right_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_SectionNo").val(),
        _txt_GO1_Right_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Remarks").val(),
        _txt_GO1_Right_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Memo").val(),
        _txt_GO1_Right_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Scheme_no").val(),

        _txt_GO1_Right_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Code").val(),
        _txt_GO1_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val(),
        _txt_GO1_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Amt").val(),
        _txt_GO1_Right_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust").val(),
        _txt_GO1_Right_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_Name").val(),
        _txt_GO1_Right_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode").val(),
        _txt_GO1_Right_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AccNo").val(),
        _txt_GO1_Right_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode_Name").val(),
        _txt_GO1_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_Rate").val(),
        _txt_GO1_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_CCY").val(),
        _txt_GO1_Right_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_FUND").val(),
        _txt_GO1_Right_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Check_No").val(),
        _txt_GO1_Right_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Available").val(),
        _txt_GO1_Right_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_AdPrint").val(),
        _txt_GO1_Right_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Details").val(),
        _txt_GO1_Right_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Entity").val(),
        _txt_GO1_Right_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Division").val(),
        _txt_GO1_Right_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Inter_Amount").val(),
        _txt_GO1_Right_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Inter_Rate").val(),
        _txt_GO1_Right_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Code").val(),
        _txt_GO1_Right_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val(),
        _txt_GO1_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Amt").val(),
        _txt_GO1_Right_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust").val(),
        _txt_GO1_Right_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_Name").val(),
        _txt_GO1_Right_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode").val(),
        _txt_GO1_Right_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode_Name").val(),
        _txt_GO1_Right_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AccNo").val(),
        _txt_GO1_Right_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Rate").val(),
        _txt_GO1_Right_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Curr").val(),
        _txt_GO1_Right_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_FUND").val(),
        _txt_GO1_Right_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Check_No").val(),
        _txt_GO1_Right_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Available").val(),
        _txt_GO1_Right_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_AdPrint").val(),
        _txt_GO1_Right_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Details").val(),
        _txt_GO1_Right_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Entity").val(),
        _txt_GO1_Right_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Division").val(),
        _txt_GO1_Right_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Inter_Amount").val(),
        _txt_GO1_Right_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Inter_Rate").val();
    }


    ///////////////// GENERAL OPRATOIN 2 /////////////////////////////////////////////////////////////
    var _chk_GO2Flag = 'N',

	    _txt_GO2_Left_Comment = '',
	    _txt_GO2_Left_SectionNo = '', _txt_GO2_Left_Remarks = '', _txt_GO2_Left_Memo = '',
	    _txt_GO2_Left_Scheme_no = '',

	    _txt_GO2_Left_Debit_Code = '', _txt_GO2_Left_Debit_Curr = '', _txt_GO2_Left_Debit_Amt = '',
	    _txt_GO2_Left_Debit_Cust = '', _txt_GO2_Left_Debit_Cust_Name = '',
	    _txt_GO2_Left_Debit_Cust_AcCode = '', _txt_GO2_Left_Debit_Cust_AcCode_Name = '', _txt_GO2_Left_Debit_Cust_AccNo = '',
	    _txt_GO2_Left_Debit_Exch_Rate = '', _txt_GO2_Left_Debit_Exch_CCY = '',
	    _txt_GO2_Left_Debit_FUND = '', _txt_GO2_Left_Debit_Check_No = '', _txt_GO2_Left_Debit_Available = '',
	    _txt_GO2_Left_Debit_AdPrint = '', _txt_GO2_Left_Debit_Details = '', _txt_GO2_Left_Debit_Entity = '',
	    _txt_GO2_Left_Debit_Division = '', _txt_GO2_Left_Debit_Inter_Amount = '', _txt_GO2_Left_Debit_Inter_Rate = '',

	    _txt_GO2_Left_Credit_Code = '', _txt_GO2_Left_Credit_Curr = '', _txt_GO2_Left_Credit_Amt = '',
        _txt_GO2_Left_Credit_Cust = '', _txt_GO2_Left_Credit_Cust_Name = '',
	    _txt_GO2_Left_Credit_Cust_AcCode = '', _txt_GO2_Left_Credit_Cust_AcCode_Name = '', _txt_GO2_Left_Credit_Cust_AccNo = '',
        _txt_GO2_Left_Credit_Exch_Rate = '', _txt_GO2_Left_Credit_Exch_Curr = '',
	    _txt_GO2_Left_Credit_FUND = '', _txt_GO2_Left_Credit_Check_No = '', _txt_GO2_Left_Credit_Available = '',
        _txt_GO2_Left_Credit_AdPrint = '', _txt_GO2_Left_Credit_Details = '', _txt_GO2_Left_Credit_Entity = '',
	    _txt_GO2_Left_Credit_Division = '', _txt_GO2_Left_Credit_Inter_Amount = '', _txt_GO2_Left_Credit_Inter_Rate = '',

        _txt_GO2_Right_Comment = '',
	    _txt_GO2_Right_SectionNo = '', _txt_GO2_Right_Remarks = '', _txt_GO2_Right_Memo = '',
	    _txt_GO2_Right_Scheme_no = '',

	    _txt_GO2_Right_Debit_Code = '', _txt_GO2_Right_Debit_Curr = '', _txt_GO2_Right_Debit_Amt = '',
	    _txt_GO2_Right_Debit_Cust = '', _txt_GO2_Right_Debit_Cust_Name = '',
	    _txt_GO2_Right_Debit_Cust_AcCode = '', _txt_GO2_Right_Debit_Cust_AcCode_Name = '', _txt_GO2_Right_Debit_Cust_AccNo = '',
	    _txt_GO2_Right_Debit_Exch_Rate = '', _txt_GO2_Right_Debit_Exch_CCY = '',
	    _txt_GO2_Right_Debit_FUND = '', _txt_GO2_Right_Debit_Check_No = '', _txt_GO2_Right_Debit_Available = '',
	    _txt_GO2_Right_Debit_AdPrint = '', _txt_GO2_Right_Debit_Details = '', _txt_GO2_Right_Debit_Entity = '',
	    _txt_GO2_Right_Debit_Division = '', _txt_GO2_Right_Debit_Inter_Amount = '', _txt_GO2_Right_Debit_Inter_Rate = '',

	    _txt_GO2_Right_Credit_Code = '', _txt_GO2_Right_Credit_Curr = '', _txt_GO2_Right_Credit_Amt = '',
	    _txt_GO2_Right_Credit_Cust = '', _txt_GO2_Right_Credit_Cust_Name = '',
	    _txt_GO2_Right_Credit_Cust_AcCode = '', _txt_GO2_Right_Credit_Cust_AcCode_Name = '', _txt_GO2_Right_Credit_Cust_AccNo = '',
	    _txt_GO2_Right_Credit_Exch_Rate = '', _txt_GO2_Right_Credit_Exch_Curr = '',
	    _txt_GO2_Right_Credit_FUND = '', _txt_GO2_Right_Credit_Check_No = '', _txt_GO2_Right_Credit_Available = '',
	    _txt_GO2_Right_Credit_AdPrint = '', _txt_GO2_Right_Credit_Details = '', _txt_GO2_Right_Credit_Entity = '',
	    _txt_GO2_Right_Credit_Division = '', _txt_GO2_Right_Credit_Inter_Amount = '', _txt_GO2_Right_Credit_Inter_Rate = '';

    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_chk_GO2Flag").is(':checked')) {
        _chk_GO2Flag = 'Y',

        _txt_GO2_Left_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Comment").val(),
            _txt_GO2_Left_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_SectionNo").val(),
            _txt_GO2_Left_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Remarks").val(),
            _txt_GO2_Left_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Memo").val(),
            _txt_GO2_Left_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Scheme_no").val(),

            _txt_GO2_Left_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Code").val(),
            _txt_GO2_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val(),
            _txt_GO2_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Amt").val(),
            _txt_GO2_Left_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust").val(),
            _txt_GO2_Left_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_Name").val(),
            _txt_GO2_Left_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode").val(),
            _txt_GO2_Left_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AccNo").val(),
            _txt_GO2_Left_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode_Name").val(),
            _txt_GO2_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_Rate").val(),
            _txt_GO2_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_CCY").val(),
            _txt_GO2_Left_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_FUND").val(),
            _txt_GO2_Left_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Check_No").val(),
            _txt_GO2_Left_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Available").val(),
            _txt_GO2_Left_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_AdPrint").val(),
            _txt_GO2_Left_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Details").val(),
            _txt_GO2_Left_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Entity").val(),
            _txt_GO2_Left_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Division").val(),
            _txt_GO2_Left_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Inter_Amount").val(),
            _txt_GO2_Left_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Inter_Rate").val(),
            _txt_GO2_Left_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Code").val(),
            _txt_GO2_Left_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val(),
            _txt_GO2_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Amt").val(),
            _txt_GO2_Left_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust").val(),
            _txt_GO2_Left_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_Name").val(),
            _txt_GO2_Left_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode").val(),
            _txt_GO2_Left_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode_Name").val(),
            _txt_GO2_Left_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AccNo").val(),
            _txt_GO2_Left_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Rate").val(),
            _txt_GO2_Left_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Curr").val(),
            _txt_GO2_Left_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_FUND").val(),
            _txt_GO2_Left_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Check_No").val(),
            _txt_GO2_Left_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Available").val(),
            _txt_GO2_Left_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_AdPrint").val(),
            _txt_GO2_Left_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Details").val(),
            _txt_GO2_Left_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Entity").val(),
            _txt_GO2_Left_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Division").val(),
            _txt_GO2_Left_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Inter_Amount").val(),
            _txt_GO2_Left_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Inter_Rate").val(),

            _txt_GO2_Right_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Comment").val(),
            _txt_GO2_Right_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_SectionNo").val(),
            _txt_GO2_Right_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Remarks").val(),
            _txt_GO2_Right_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Memo").val(),
            _txt_GO2_Right_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Scheme_no").val(),

            _txt_GO2_Right_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Code").val(),
            _txt_GO2_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val(),
            _txt_GO2_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Amt").val(),
            _txt_GO2_Right_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust").val(),
            _txt_GO2_Right_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_Name").val(),
            _txt_GO2_Right_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode").val(),
            _txt_GO2_Right_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AccNo").val(),
            _txt_GO2_Right_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode_Name").val(),
            _txt_GO2_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_Rate").val(),
            _txt_GO2_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_CCY").val(),
            _txt_GO2_Right_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_FUND").val(),
            _txt_GO2_Right_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Check_No").val(),
            _txt_GO2_Right_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Available").val(),
            _txt_GO2_Right_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_AdPrint").val(),
            _txt_GO2_Right_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Details").val(),
            _txt_GO2_Right_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Entity").val(),
            _txt_GO2_Right_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Division").val(),
            _txt_GO2_Right_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Inter_Amount").val(),
            _txt_GO2_Right_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Inter_Rate").val(),
            _txt_GO2_Right_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Code").val(),
            _txt_GO2_Right_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val(),
            _txt_GO2_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Amt").val(),
            _txt_GO2_Right_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust").val(),
            _txt_GO2_Right_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_Name").val(),
            _txt_GO2_Right_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode").val(),
            _txt_GO2_Right_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode_Name").val(),
            _txt_GO2_Right_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AccNo").val(),
            _txt_GO2_Right_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Rate").val(),
            _txt_GO2_Right_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Curr").val(),
            _txt_GO2_Right_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_FUND").val(),
            _txt_GO2_Right_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Check_No").val(),
            _txt_GO2_Right_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Available").val(),
            _txt_GO2_Right_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_AdPrint").val(),
            _txt_GO2_Right_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Details").val(),
            _txt_GO2_Right_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Entity").val(),
            _txt_GO2_Right_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Division").val(),
            _txt_GO2_Right_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Inter_Amount").val(),
            _txt_GO2_Right_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Inter_Rate").val();
    }

    ///////////////// GENERAL OPRATOIN 3 /////////////////////////////////////////////////////////////
    var _chk_GO3Flag = 'N',

        _txt_GO3_Left_Comment = '',
        _txt_GO3_Left_SectionNo = '', _txt_GO3_Left_Remarks = '', _txt_GO3_Left_Memo = '',
        _txt_GO3_Left_Scheme_no = '',

        _txt_GO3_Left_Debit_Code = '', _txt_GO3_Left_Debit_Curr = '', _txt_GO3_Left_Debit_Amt = '',
        _txt_GO3_Left_Debit_Cust = '', _txt_GO3_Left_Debit_Cust_Name = '',
        _txt_GO3_Left_Debit_Cust_AcCode = '', _txt_GO3_Left_Debit_Cust_AcCode_Name = '', _txt_GO3_Left_Debit_Cust_AccNo = '',
        _txt_GO3_Left_Debit_Exch_Rate = '', _txt_GO3_Left_Debit_Exch_CCY = '',
        _txt_GO3_Left_Debit_FUND = '', _txt_GO3_Left_Debit_Check_No = '', _txt_GO3_Left_Debit_Available = '',
        _txt_GO3_Left_Debit_AdPrint = '', _txt_GO3_Left_Debit_Details = '', _txt_GO3_Left_Debit_Entity = '',
        _txt_GO3_Left_Debit_Division = '', _txt_GO3_Left_Debit_Inter_Amount = '', _txt_GO3_Left_Debit_Inter_Rate = '',

        _txt_GO3_Left_Credit_Code = '', _txt_GO3_Left_Credit_Curr = '', _txt_GO3_Left_Credit_Amt = '',
        _txt_GO3_Left_Credit_Cust = '', _txt_GO3_Left_Credit_Cust_Name = '',
        _txt_GO3_Left_Credit_Cust_AcCode = '', _txt_GO3_Left_Credit_Cust_AcCode_Name = '', _txt_GO3_Left_Credit_Cust_AccNo = '',
        _txt_GO3_Left_Credit_Exch_Rate = '', _txt_GO3_Left_Credit_Exch_Curr = '',
        _txt_GO3_Left_Credit_FUND = '', _txt_GO3_Left_Credit_Check_No = '', _txt_GO3_Left_Credit_Available = '',
        _txt_GO3_Left_Credit_AdPrint = '', _txt_GO3_Left_Credit_Details = '', _txt_GO3_Left_Credit_Entity = '',
        _txt_GO3_Left_Credit_Division = '', _txt_GO3_Left_Credit_Inter_Amount = '', _txt_GO3_Left_Credit_Inter_Rate = '',

        _txt_GO3_Right_Comment = '',
        _txt_GO3_Right_SectionNo = '', _txt_GO3_Right_Remarks = '', _txt_GO3_Right_Memo = '',
        _txt_GO3_Right_Scheme_no = '',

        _txt_GO3_Right_Debit_Code = '', _txt_GO3_Right_Debit_Curr = '', _txt_GO3_Right_Debit_Amt = '',
        _txt_GO3_Right_Debit_Cust = '', _txt_GO3_Right_Debit_Cust_Name = '',
        _txt_GO3_Right_Debit_Cust_AcCode = '', _txt_GO3_Right_Debit_Cust_AcCode_Name = '', _txt_GO3_Right_Debit_Cust_AccNo = '',
        _txt_GO3_Right_Debit_Exch_Rate = '', _txt_GO3_Right_Debit_Exch_CCY = '',
        _txt_GO3_Right_Debit_FUND = '', _txt_GO3_Right_Debit_Check_No = '', _txt_GO3_Right_Debit_Available = '',
        _txt_GO3_Right_Debit_AdPrint = '', _txt_GO3_Right_Debit_Details = '', _txt_GO3_Right_Debit_Entity = '',
        _txt_GO3_Right_Debit_Division = '', _txt_GO3_Right_Debit_Inter_Amount = '', _txt_GO3_Right_Debit_Inter_Rate = '',

        _txt_GO3_Right_Credit_Code = '', _txt_GO3_Right_Credit_Curr = '', _txt_GO3_Right_Credit_Amt = '',
        _txt_GO3_Right_Credit_Cust = '', _txt_GO3_Right_Credit_Cust_Name = '',
        _txt_GO3_Right_Credit_Cust_AcCode = '', _txt_GO3_Right_Credit_Cust_AcCode_Name = '', _txt_GO3_Right_Credit_Cust_AccNo = '',
        _txt_GO3_Right_Credit_Exch_Rate = '', _txt_GO3_Right_Credit_Exch_Curr = '',
        _txt_GO3_Right_Credit_FUND = '', _txt_GO3_Right_Credit_Check_No = '', _txt_GO3_Right_Credit_Available = '',
        _txt_GO3_Right_Credit_AdPrint = '', _txt_GO3_Right_Credit_Details = '', _txt_GO3_Right_Credit_Entity = '',
        _txt_GO3_Right_Credit_Division = '', _txt_GO3_Right_Credit_Inter_Amount = '', _txt_GO3_Right_Credit_Inter_Rate = '';

    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_chk_GO3Flag").is(':checked')) {
        _chk_GO3Flag = 'Y',

        _txt_GO3_Left_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Comment").val(),
        _txt_GO3_Left_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_SectionNo").val(),
        _txt_GO3_Left_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Remarks").val(),
        _txt_GO3_Left_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Memo").val(),
        _txt_GO3_Left_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Scheme_no").val(),

        _txt_GO3_Left_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Code").val(),
        _txt_GO3_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Curr").val(),
        _txt_GO3_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Amt").val(),
        _txt_GO3_Left_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust").val(),
        _txt_GO3_Left_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_Name").val(),
        _txt_GO3_Left_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_AcCode").val(),
        _txt_GO3_Left_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_AccNo").val(),
        _txt_GO3_Left_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_AcCode_Name").val(),
        _txt_GO3_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Exch_Rate").val(),
        _txt_GO3_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Exch_CCY").val(),
        _txt_GO3_Left_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_FUND").val(),
        _txt_GO3_Left_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Check_No").val(),
        _txt_GO3_Left_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Available").val(),
        _txt_GO3_Left_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_AdPrint").val(),
        _txt_GO3_Left_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Details").val(),
        _txt_GO3_Left_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Entity").val(),
        _txt_GO3_Left_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Division").val(),
        _txt_GO3_Left_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Inter_Amount").val(),
        _txt_GO3_Left_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Inter_Rate").val(),
        _txt_GO3_Left_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Code").val(),
        _txt_GO3_Left_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Curr").val(),
        _txt_GO3_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Amt").val(),
        _txt_GO3_Left_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust").val(),
        _txt_GO3_Left_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_Name").val(),
        _txt_GO3_Left_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_AcCode").val(),
        _txt_GO3_Left_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_AcCode_Name").val(),
        _txt_GO3_Left_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_AccNo").val(),
        _txt_GO3_Left_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Exch_Rate").val(),
        _txt_GO3_Left_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Exch_Curr").val(),
        _txt_GO3_Left_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_FUND").val(),
        _txt_GO3_Left_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Check_No").val(),
        _txt_GO3_Left_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Available").val(),
        _txt_GO3_Left_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_AdPrint").val(),
        _txt_GO3_Left_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Details").val(),
        _txt_GO3_Left_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Entity").val(),
        _txt_GO3_Left_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Division").val(),
        _txt_GO3_Left_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Inter_Amount").val(),
        _txt_GO3_Left_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Inter_Rate").val();

        _txt_GO3_Right_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Comment").val(),
        _txt_GO3_Right_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_SectionNo").val(),
        _txt_GO3_Right_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Remarks").val(),
        _txt_GO3_Right_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Memo").val(),
        _txt_GO3_Right_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Scheme_no").val(),

        _txt_GO3_Right_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Code").val(),
        _txt_GO3_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Curr").val(),
        _txt_GO3_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Amt").val(),
        _txt_GO3_Right_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust").val(),
        _txt_GO3_Right_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_Name").val(),
        _txt_GO3_Right_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_AcCode").val(),
        _txt_GO3_Right_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_AccNo").val(),
        _txt_GO3_Right_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_AcCode_Name").val(),
        _txt_GO3_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Exch_Rate").val(),
        _txt_GO3_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Exch_CCY").val(),
        _txt_GO3_Right_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_FUND").val(),
        _txt_GO3_Right_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Check_No").val(),
        _txt_GO3_Right_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Available").val(),
        _txt_GO3_Right_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_AdPrint").val(),
        _txt_GO3_Right_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Details").val(),
        _txt_GO3_Right_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Entity").val(),
        _txt_GO3_Right_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Division").val(),
        _txt_GO3_Right_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Inter_Amount").val(),
        _txt_GO3_Right_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Inter_Rate").val(),
        _txt_GO3_Right_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Code").val(),
        _txt_GO3_Right_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Curr").val(),
        _txt_GO3_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Amt").val(),
        _txt_GO3_Right_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust").val(),
        _txt_GO3_Right_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_Name").val(),
        _txt_GO3_Right_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_AcCode").val(),
        _txt_GO3_Right_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_AcCode_Name").val(),
        _txt_GO3_Right_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_AccNo").val(),
        _txt_GO3_Right_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Exch_Rate").val(),
        _txt_GO3_Right_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Exch_Curr").val(),
        _txt_GO3_Right_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_FUND").val(),
        _txt_GO3_Right_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Check_No").val(),
        _txt_GO3_Right_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Available").val(),
        _txt_GO3_Right_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_AdPrint").val(),
        _txt_GO3_Right_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Details").val(),
        _txt_GO3_Right_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Entity").val(),
        _txt_GO3_Right_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Division").val(),
        _txt_GO3_Right_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Inter_Amount").val(),
        _txt_GO3_Right_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Inter_Rate").val();
    }

    ///////////////////// GENERAL OPRATOIN For Change Branch /////////////////////////////////////////////////////////////
    var _chk_GOAcccChangeFlag = 'N',
    _txt_GOAccChange_Ref_No = '', _txt_GOAccChange_Comment = '',
    _txt_GOAccChange_SectionNo = '', _txt_GOAccChange_Remarks = '', _txt_GOAccChange_Memo = '',
    _txt_GOAccChange_Scheme_no = '',
    _txt_GOAccChange_Debit_Code = '', _txt_GOAccChange_Debit_Curr = '', _txt_GOAccChange_Debit_Amt = '',
    _txt_GOAccChange_Debit_Cust = '', _txt_GOAccChange_Debit_Cust_Name = '',
    _txt_GOAccChange_Debit_Cust_AcCode = '', _txt_GOAccChange_Debit_Cust_AccNo = '', _txt_GOAccChange_Debit_Cust_AcCode_Name = '',
    _txt_GOAccChange_Debit_Exch_Rate = '', _txt_GOAccChange_Debit_Exch_CCY = '',
    _txt_GOAccChange_Debit_FUND = '', _txt_GOAccChange_Debit_Check_No = '', _txt_GOAccChange_Debit_Available = '',
    _txt_GOAccChange_Debit_AdPrint = '', _txt_GOAccChange_Debit_Details = '', _txt_GOAccChange_Debit_Entity = '',
    _txt_GOAccChange_Debit_Division = '', _txt_GOAccChange_Debit_Inter_Amount = '', _txt_GOAccChange_Debit_Inter_Rate = '',
    _txt_GOAccChange_Credit_Code = '', _txt_GOAccChange_Credit_Curr = '', _txt_GOAccChange_Credit_Amt = '',
    _txt_GOAccChange_Credit_Cust = '', _txt_GOAccChange_Credit_Cust_Name = '',
    _txt_GOAccChange_Credit_Cust_AcCode = '', _txt_GOAccChange_Credit_Cust_AcCode_Name = '', _txt_GOAccChange_Credit_Cust_AccNo = '',
    _txt_GOAccChange_Credit_Exch_Rate = '', _txt_GOAccChange_Credit_Exch_Curr = '',
    _txt_GOAccChange_Credit_FUND = '', _txt_GOAccChange_Credit_Check_No = '', _txt_GOAccChange_Credit_Available = '',
    _txt_GOAccChange_Credit_AdPrint = '', _txt_GOAccChange_Credit_Details = '', _txt_GOAccChange_Credit_Entity = '',
    _txt_GOAccChange_Credit_Division = '', _txt_GOAccChange_Credit_Inter_Amount = '', _txt_GOAccChange_Credit_Inter_Rate = '';

    if (_BranchName != 'Mumbai') {

        if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_chk_GOAcccChangeFlag").is(':checked')) {
            _chk_GOAcccChangeFlag = 'Y';
            _txt_GOAccChange_Ref_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Ref_No").val(),
            _txt_GOAccChange_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Comment").val(),
            _txt_GOAccChange_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_SectionNo").val(),
            _txt_GOAccChange_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Remarks").val(),
            _txt_GOAccChange_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Memo").val(),
            _txt_GOAccChange_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Scheme_no").val(),
            _txt_GOAccChange_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Code").val(),
            _txt_GOAccChange_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Curr").val(),
            _txt_GOAccChange_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Amt").val(),
            _txt_GOAccChange_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust").val(),
            _txt_GOAccChange_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_Name").val(),
            _txt_GOAccChange_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AcCode").val(),
            _txt_GOAccChange_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AccNo").val(),
            _txt_GOAccChange_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AcCode_Name").val(),
            _txt_GOAccChange_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_Rate").val(),
            _txt_GOAccChange_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Exch_CCY").val(),
            _txt_GOAccChange_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_FUND").val(),
            _txt_GOAccChange_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Check_No").val(),
            _txt_GOAccChange_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Available").val(),
            _txt_GOAccChange_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_AdPrint").val(),
            _txt_GOAccChange_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Details").val(),
            _txt_GOAccChange_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Entity").val(),
            _txt_GOAccChange_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Division").val(),
            _txt_GOAccChange_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Inter_Amount").val(),
            _txt_GOAccChange_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Inter_Rate").val(),
            _txt_GOAccChange_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Code").val(),
            _txt_GOAccChange_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Curr").val(),
            _txt_GOAccChange_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Amt").val(),
            _txt_GOAccChange_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust").val(),
            _txt_GOAccChange_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_Name").val(),
            _txt_GOAccChange_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AcCode").val(),
            _txt_GOAccChange_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AcCode_Name").val(),
            _txt_GOAccChange_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AccNo").val(),
            _txt_GOAccChange_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Exch_Rate").val(),
            _txt_GOAccChange_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Exch_Curr").val(),
            _txt_GOAccChange_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_FUND").val(),
            _txt_GOAccChange_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Check_No").val(),
            _txt_GOAccChange_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Available").val(),
            _txt_GOAccChange_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_AdPrint").val(),
            _txt_GOAccChange_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Details").val(),
            _txt_GOAccChange_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Entity").val(),
            _txt_GOAccChange_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Division").val(),
            _txt_GOAccChange_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Inter_Amount").val(),
            _txt_GOAccChange_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Inter_Rate").val()
        }
    }


    /////////// Swift File
    _txt_MT103Receiver = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt_MT103Receiver").val();
    _txtInstructionCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtInstructionCode").val();
    _txtTransactionTypeCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtTransactionTypeCode").val();
    var _txtVDate32 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt103Date").val();
    //var _txtCurrency32 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt103Currency").val();
    //var _txtAmount32 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt103Amount").val();
    _txtCurrency = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtCurrency").val();
    _txtInstructedAmount = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtInstructedAmount").val();
    _txtExchangeRate = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtExchangeRate").val();
    _txtSendingInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendingInstitutionAccountNumber").val();
    _txtSendingInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendingInstitutionSwiftCode").val();

    var _ddlOrderingInstitution = '', _txtOrderingInstitutionAccountNumber = '', _txtOrderingInstitutionSwiftCode = '', _txtOrderingInstitutionName = '', _txtOrderingInstitutionAddress1 = '',
    _txtOrderingInstitutionAddress2 = '', _txtOrderingInstitutionAddress3 = '';
    _ddlOrderingInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlOrderingInstitution").val();
    _txtOrderingInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAccountNumber").val();
    if (_ddlOrderingInstitution == 'A') {
        _txtOrderingInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionSwiftCode").val();
    }
    if (_ddlOrderingInstitution == 'D') {
        _txtOrderingInstitutionName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionName").val();
        _txtOrderingInstitutionAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAddress1").val();
        _txtOrderingInstitutionAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAddress2").val();
        _txtOrderingInstitutionAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAddress3").val();
    }

    var _ddlSendersCorrespondent = '', _txtSendersCorrespondentAccountNumber = '', _txtSendersCorrespondentSwiftCode = '', _txtSendersCorrespondentName = '', _txtSendersCorrespondentLocation = '',
    _txtSendersCorrespondentAddress1 = '', _txtSendersCorrespondentAddress2 = '', _txtSendersCorrespondentAddress3 = '';
    _ddlSendersCorrespondent = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlSendersCorrespondent").val();
    _txtSendersCorrespondentAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").val();
    if (_ddlSendersCorrespondent == 'A') {
        _txtSendersCorrespondentSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentSwiftCode").val();
    }
    if (_ddlSendersCorrespondent == 'B') {
        _txtSendersCorrespondentLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentLocation").val();
    }
    if (_ddlSendersCorrespondent == 'D') {
        _txtSendersCorrespondentName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentName").val();
        _txtSendersCorrespondentAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAddress1").val();
        _txtSendersCorrespondentAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAddress2").val();
        _txtSendersCorrespondentAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAddress3").val();
    }

    var _ddlReceiversCorrespondent = '', _txtReceiversCorrespondentAccountNumber = '', _txtReceiversCorrespondentSwiftCode = '', _txtReceiversCorrespondentName = '', _txtReceiversCorrespondentLocation = '',
    _txtReceiversCorrespondentAddress1 = '', _txtReceiversCorrespondentAddress2 = '', _txtReceiversCorrespondentAddress3 = '';
    _ddlReceiversCorrespondent = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlReceiversCorrespondent").val();
    _txtReceiversCorrespondentAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").val();
    if (_ddlReceiversCorrespondent == 'A') {
        _txtReceiversCorrespondentSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentSwiftCode").val();
    }
    if (_ddlReceiversCorrespondent == 'B') {
        _txtReceiversCorrespondentLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentLocation").val();
    }
    if (_ddlReceiversCorrespondent == 'D') {
        _txtReceiversCorrespondentName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentName").val();
        _txtReceiversCorrespondentAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAddress1").val();
        _txtReceiversCorrespondentAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAddress2").val();
        _txtReceiversCorrespondentAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAddress3").val();
    }


    var _ddlThirdReimbursementInstitution = '', _txtThirdReimbursementInstitutionAccountNumber = '', _txtThirdReimbursementInstitutionSwiftCode = '', _txtThirdReimbursementInstitutionName = '', _txtThirdReimbursementInstitutionLocation = '',
    _txtThirdReimbursementInstitutionAddress1 = '', _txtThirdReimbursementInstitutionAddress2 = '', _txtThirdReimbursementInstitutionAddress3 = '';
    _ddlThirdReimbursementInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlThirdReimbursementInstitution").val();
    _txtThirdReimbursementInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAccountNumber").val();
    if (_ddlThirdReimbursementInstitution == 'A') {
        _txtThirdReimbursementInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionSwiftCode").val();
    }
    if (_ddlThirdReimbursementInstitution == 'B') {
        _txtThirdReimbursementInstitutionLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionLocation").val();
    }
    if (_ddlThirdReimbursementInstitution == 'D') {
        _txtThirdReimbursementInstitutionName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionName").val();
        _txtThirdReimbursementInstitutionAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAddress1").val();
        _txtThirdReimbursementInstitutionAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAddress2").val();
        _txtThirdReimbursementInstitutionAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAddress3").val();
    }

    // MT 103 Changes 02112019
    var _ddlIntermediary103 = '', _txtIntermediary103AccountNumber = '', _txtIntermediary103SwiftCode = '', _txtIntermediary103Name = '', _txtIntermediary103Address1 = '',
    _txtIntermediary103Address2 = '', _txtIntermediary103Address3 = '';
    _ddlIntermediary103 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlIntermediary103").val();
    _txtIntermediary103AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103AccountNumber").val();
    if (_ddlIntermediary103 == 'A') {
        _txtIntermediary103SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103SwiftCode").val();
    }
    if (_ddlIntermediary103 == 'D') {
        _txtIntermediary103Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Name").val();
        _txtIntermediary103Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Address1").val();
        _txtIntermediary103Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Address2").val();
        _txtIntermediary103Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Address3").val();
    }

    var _ddlAccountWithInstitution103 = '', _txtAccountWithInstitution103AccountNumber = '', _txtAccountWithInstitution103SwiftCode = '', _txtAccountWithInstitution103Name = '', _txtAccountWithInstitution103Location = '',
    _txtAccountWithInstitution103Address1 = '', _txtAccountWithInstitution103Address2 = '', _txtAccountWithInstitution103Address3 = '';
    _ddlAccountWithInstitution103 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlAccountWithInstitution103").val();
    _txtAccountWithInstitution103AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").val();
    if (_ddlAccountWithInstitution103 == 'A') {
        _txtAccountWithInstitution103SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103SwiftCode").val();
    }
    if (_ddlAccountWithInstitution103 == 'B') {
        _txtAccountWithInstitution103Location = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Location").val();
    }
    if (_ddlAccountWithInstitution103 == 'D') {
        _txtAccountWithInstitution103Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Name").val();
        _txtAccountWithInstitution103Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Address1").val();
        _txtAccountWithInstitution103Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Address2").val();
        _txtAccountWithInstitution103Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Address3").val();
    }
    // MT 103 Changes 02112019

    _txtBeneficiaryCustomerAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAccountNumber").val();
    _txtBeneficiaryCustomerSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerSwiftCode").val();
    _txtDetailsOfCharges = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtDetailsOfCharges").val();
    _txtSenderCharges = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges").val();
    _txtSenderCharges2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges2").val();
    _txtReceiverCharges = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges").val();
    _txtReceiverCharges2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges2").val();
    _txtSendertoReceiverInformation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation").val();
    _txtSendertoReceiverInformation2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation2").val();
    _txtSendertoReceiverInformation3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation3").val();
    _txtSendertoReceiverInformation4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation4").val();
    _txtSendertoReceiverInformation5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation5").val();
    _txtSendertoReceiverInformation6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation6").val();
    _txtRegulatoryReporting = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRegulatoryReporting").val();
    _txtRegulatoryReporting2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRegulatoryReporting2").val();
    _txtRegulatoryReporting3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRegulatoryReporting3").val();

    // MT 103 Changes 11052022

    var _txtTimeIndication = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtTimeIndication').val(),
	_ddlBankOperationCode = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlBankOperationCode').val();

    var _ddlOrderingCustomer = '', _txtOrderingCustomer_Acc = '', _txtOrderingCustomer_SwiftCode = '',
	_txtOrderingCustomer_Name = '', _txtOrderingCustomer_Addr1 = '', _txtOrderingCustomer_Addr2 = '', _txtOrderingCustomer_Addr3 = '';

    _ddlOrderingCustomer = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlOrderingCustomer').val(),
	_txtOrderingCustomer_Acc = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Acc').val();
    if (_ddlOrderingCustomer == 'A') {
        _txtOrderingCustomer_SwiftCode = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_SwiftCode').val();
    }
    if (_ddlOrderingCustomer == 'K' || _ddlOrderingCustomer == 'F') {
        _txtOrderingCustomer_Name = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Name').val();
        _txtOrderingCustomer_Addr1 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr1').val();
        _txtOrderingCustomer_Addr2 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr2').val();
        _txtOrderingCustomer_Addr3 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr3').val();
    }


    var _ddlBeneficiaryCustomer = '', _txtBeneficiaryCustomerAccountNumber = '', _txtBeneficiaryCustomerSwiftCode = '',
	_txtBeneficiaryCustomerName = '', _txtBeneficiaryCustomerAddr1 = '', _txtBeneficiaryCustomerAddr2 = '', _txtBeneficiaryCustomerAddr3 = '';
    _ddlBeneficiaryCustomer = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlBeneficiaryCustomer').val(),
	_txtBeneficiaryCustomerAccountNumber = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAccountNumber').val();

    if (_ddlBeneficiaryCustomer == 'A') {
        _txtBeneficiaryCustomerSwiftCode = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerSwiftCode').val();
    }

    else {
        _txtBeneficiaryCustomerName = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerName').val();
        _txtBeneficiaryCustomerAddr1 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAddr1').val();
        _txtBeneficiaryCustomerAddr2 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAddr2').val();
        _txtBeneficiaryCustomerAddr3 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAddr3').val();
    }

    var _txtRemittanceInformation1 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRemittanceInformation1').val(),
	_txtRemittanceInformation2 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRemittanceInformation2').val(),
	_txtRemittanceInformation3 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRemittanceInformation3').val(),
	_txtRemittanceInformation4 = $('#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRemittanceInformation4').val();

    // MT 103 Changes 11052022 end


    var _txt202Amount = '', _ddlOrderingInstitution202 = '', _txtOrderingInstitution202AccountNumber = '', _txtOrderingInstitution202SwiftCode = '', _txtOrderingInstitution202Name = '', _txtOrderingInstitution202Address1 = '',
    _txtOrderingInstitution202Address2 = '', _txtOrderingInstitution202Address3 = '';

    _txt202Amount = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txt202Amount").val();
    _ddlOrderingInstitution202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlOrderingInstitution202").val();
    _txtOrderingInstitution202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202AccountNumber").val();
    if (_ddlOrderingInstitution202 == 'A') {
        _txtOrderingInstitution202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202SwiftCode").val();
    }
    if (_ddlOrderingInstitution202 == 'D') {
        _txtOrderingInstitution202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202Name").val();
        _txtOrderingInstitution202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202Address1").val();
        _txtOrderingInstitution202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202Address2").val();
        _txtOrderingInstitution202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202Address3").val();
    }

    var _ddlSendersCorrespondent202 = '', _txtSendersCorrespondent202AccountNumber = '', _txtSendersCorrespondent202SwiftCode = '', _txtSendersCorrespondent202Name = '', _txtSendersCorrespondent202Location = '',
    _txtSendersCorrespondent202Address1 = '', _txtSendersCorrespondent202Address2 = '', _txtSendersCorrespondent202Address3 = '';
    _ddlSendersCorrespondent202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlSendersCorrespondent202").val();
    _txtSendersCorrespondent202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202AccountNumber").val();
    if (_ddlSendersCorrespondent202 == 'A') {
        _txtSendersCorrespondent202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202SwiftCode").val();
    }
    if (_ddlSendersCorrespondent202 == 'B') {
        _txtSendersCorrespondent202Location = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202Location").val();
    }
    if (_ddlSendersCorrespondent202 == 'D') {
        _txtSendersCorrespondent202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202Name").val();
        _txtSendersCorrespondent202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202Address1").val();
        _txtSendersCorrespondent202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202Address2").val();
        _txtSendersCorrespondent202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202Address3").val();
    }



    var _ddlReceiversCorrespondent202 = '', _txtReceiversCorrespondent202AccountNumber = '', _txtReceiversCorrespondent202SwiftCode = '', _txtReceiversCorrespondent202Name = '',
    _txtReceiversCorrespondent202Location = '', _txtReceiversCorrespondent202Address1 = '', _txtReceiversCorrespondent202Address2 = '', _txtReceiversCorrespondent202Address3 = '';
    _ddlReceiversCorrespondent202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlReceiversCorrespondent202").val();
    _txtReceiversCorrespondent202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202AccountNumber").val();
    if (_ddlReceiversCorrespondent202 == 'A') {
        _txtReceiversCorrespondent202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202SwiftCode").val();
    }
    if (_ddlReceiversCorrespondent202 == 'B') {
        _txtReceiversCorrespondent202Location = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202Location").val();
    }
    if (_ddlReceiversCorrespondent202 == 'D') {
        _txtReceiversCorrespondent202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202Name").val();
        _txtReceiversCorrespondent202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202Address1").val();
        _txtReceiversCorrespondent202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202Address2").val();
        _txtReceiversCorrespondent202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202Address3").val();
    }

    // MT 202 Changes 02112019
    var _ddlIntermediary202 = '', _txtIntermediary202AccountNumber = '', _txtIntermediary202SwiftCode = '', _txtIntermediary202Name = '', _txtIntermediary202Address1 = '',
    _txtIntermediary202Address2 = '', _txtIntermediary202Address3 = '';
    _ddlIntermediary202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlIntermediary202").val();
    _txtIntermediary202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202AccountNumber").val();
    if (_ddlIntermediary202 == 'A') {
        _txtIntermediary202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202SwiftCode").val();
    }
    if (_ddlIntermediary202 == 'D') {
        _txtIntermediary202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Name").val();
        _txtIntermediary202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Address1").val();
        _txtIntermediary202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Address2").val();
        _txtIntermediary202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Address3").val();
    }

    var _ddlAccountWithInstitution202 = '', _txtAccountWithInstitution202AccountNumber = '', _txtAccountWithInstitution202SwiftCode = '', _txtAccountWithInstitution202Name = '', _txtAccountWithInstitution202Location = '',
    _txtAccountWithInstitution202Address1 = '', _txtAccountWithInstitution202Address2 = '', _txtAccountWithInstitution202Address3 = '';
    _ddlAccountWithInstitution202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlAccountWithInstitution202").val();
    _txtAccountWithInstitution202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").val();
    if (_ddlAccountWithInstitution202 == 'A') {
        _txtAccountWithInstitution202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202SwiftCode").val();
    }
    if (_ddlAccountWithInstitution202 == 'B') {
        _txtAccountWithInstitution202Location = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Location").val();
    }
    if (_ddlAccountWithInstitution202 == 'D') {
        _txtAccountWithInstitution202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Name").val();
        _txtAccountWithInstitution202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Address1").val();
        _txtAccountWithInstitution202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Address2").val();
        _txtAccountWithInstitution202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Address3").val();
    }

    var _ddlBeneficiaryInstitution202 = '', _txtBeneficiaryInstitution202AccountNumber = '', _txtBeneficiaryInstitution202SwiftCode = '', _txtBeneficiaryInstitution202Name = '', _txtBeneficiaryInstitution202Address1 = '',
    _txtBeneficiaryInstitution202Address2 = '', _txtBeneficiaryInstitution202Address3 = '';
    _ddlBeneficiaryInstitution202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlBeneficiaryInstitution202").val();
    _txtBeneficiaryInstitution202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202AccountNumber").val();
    if (_ddlBeneficiaryInstitution202 == 'A') {
        _txtBeneficiaryInstitution202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202SwiftCode").val();
    }
    if (_ddlBeneficiaryInstitution202 == 'D') {
        _txtBeneficiaryInstitution202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Name").val();
        _txtBeneficiaryInstitution202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Address1").val();
        _txtBeneficiaryInstitution202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Address2").val();
        _txtBeneficiaryInstitution202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Address3").val();
    }

    var _txtSenderToReceiverInformation2021 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSenderToReceiverInformation2021").val();
    var _txtSenderToReceiverInformation2022 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSenderToReceiverInformation2022").val();
    var _txtSenderToReceiverInformation2023 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSenderToReceiverInformation2023").val();
    var _txtSenderToReceiverInformation2024 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSenderToReceiverInformation2024").val();
    var _txtSenderToReceiverInformation2025 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSenderToReceiverInformation2025").val();
    var _txtSenderToReceiverInformation2026 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSenderToReceiverInformation2026").val();
    // MT 202 Changes 02112019


    var _rdb_swift_None = '', _rdb_swift_103 = '', _rdb_swift_200 = '', _rdb_swift_202 = '', MT754_Flag = '';
    if ($("#TabContainerMain_tbSwift_rdb_swift_None").is(':checked')) {
        _rdb_swift_None = 'Y';
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_103").is(':checked')) {
        _rdb_swift_103 = 'Y';
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_200").is(':checked')) {
        _rdb_swift_200 = 'Y';
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_202").is(':checked')) {
        _rdb_swift_202 = 'Y';
    }
    //------------------------------------------------------Nilesh-------------------------------------------------------------
    if ($("#TabContainerMain_tbSwift_rdb_swift_754").is(':checked')) {
        MT754_Flag = 'Y';
    }
    //------------------------------------------------------Nilesh END---------------------------------------------------------
    //MT 200
    var _txt200BicCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200BicCode").val();
    var _txt200TransactionRefNO = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200TransactionRefNO").val();
    var _txt200Date = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Date").val();
    var _txt200Currency = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Currency").val();
    var _txt200Amount = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Amount").val();
    var _txt200SenderCorreCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SenderCorreCode").val();
    var _txt200SenderCorreLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SenderCorreLocation").val();

    var _ddl200Intermediary = '', _txt200IntermediaryAccountNumber = '', _txt200IntermediarySwiftCode = '', _txt200IntermediaryName = '',
    _txt200IntermediaryAddress1 = '', _txt200IntermediaryAddress2 = '', _txt200IntermediaryAddress3 = '';
    _ddl200Intermediary = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_ddl200Intermediary").val();
    _txt200IntermediaryAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediaryAccountNumber").val();
    if (_ddl200Intermediary == 'A') {
        _txt200IntermediarySwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediarySwiftCode").val();
    }
    if (_ddl200Intermediary == 'D') {
        _txt200IntermediaryName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediaryName").val();
        _txt200IntermediaryAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediaryAddress1").val();
        _txt200IntermediaryAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediaryAddress2").val();
        _txt200IntermediaryAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediaryAddress3").val();
    }

    var _ddl200AccWithInstitution = '', _txt200AccWithInstitutionAccountNumber = '', _txt200AccWithInstitutionSwiftCode = '', _txt200AccWithInstitutionLocation = '',
    _txt200AccWithInstitutionName = '', _txt200AccWithInstitutionAddress1 = '', _txt200AccWithInstitutionAddress2 = '', _txt200AccWithInstitutionAddress3 = '';
    _ddl200AccWithInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_ddl200AccWithInstitution").val();
    _txt200AccWithInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAccountNumber").val();
    if (_ddl200AccWithInstitution == 'A') {
        _txt200AccWithInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionSwiftCode").val();
    }
    if (_ddl200AccWithInstitution == 'B') {
        _txt200AccWithInstitutionLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionLocation").val();
    }
    if (_ddl200AccWithInstitution == 'D') {
        _txt200AccWithInstitutionName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionName").val();
        _txt200AccWithInstitutionAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAddress1").val();
        _txt200AccWithInstitutionAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAddress2").val();
        _txt200AccWithInstitutionAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAddress3").val();
    }

    var _txt200SendertoReceiverInformation1 = '', _txt200SendertoReceiverInformation2 = '', _txt200SendertoReceiverInformation3 = '',
    _txt200SendertoReceiverInformation4 = '', _txt200SendertoReceiverInformation5 = '', _txt200SendertoReceiverInformation6 = '';
    _txt200SendertoReceiverInformation1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SendertoReceiverInformation1").val();
    _txt200SendertoReceiverInformation2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SendertoReceiverInformation2").val();
    _txt200SendertoReceiverInformation3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SendertoReceiverInformation3").val();
    _txt200SendertoReceiverInformation4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SendertoReceiverInformation4").val();
    _txt200SendertoReceiverInformation5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SendertoReceiverInformation5").val();
    _txt200SendertoReceiverInformation6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SendertoReceiverInformation6").val();

    _rdb_swift_R42 = '';
    if ($("#TabContainerMain_tbSwift_rdb_swift_R42").is(':checked')) {
        _rdb_swift_R42 = 'Y';
    }
    var _txtTransactionRefNoR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtTransactionRefNoR42").val();
    var _txtRelatedReferenceR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtRelatedReferenceR42").val();
    var _txtValueDateR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtValueDateR42").val();
    var _txtCureencyR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtCureencyR42").val();
    var _txtAmountR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtAmountR42").val();
    var _txtOrderingInstitutionIFSCR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtOrderingInstitutionIFSCR42").val();
    var _txtBeneficiaryInstitutionIFSCR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtBeneficiaryInstitutionIFSCR42").val();
    var _txtCodeWordR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtCodeWordR42").val();
    var _txtAdditionalInformationR42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtAdditionalInformationR42").val();
    var _txtMoreInfo1R42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtMoreInfo1R42").val();
    var _txtMoreInfo2R42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtMoreInfo2R42").val();
    var _txtMoreInfo3R42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtMoreInfo3R42").val();
    var _txtMoreInfo4R42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtMoreInfo4R42").val();
    var _txtMoreInfo5R42 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftR42_txtMoreInfo5R42").val();


    //////--------------------------------------------------------Nilesh---------------------------------------------------------------------------------

    //----------------------------------------------------------------- MT 754----------------------------------------------------------------------------
    var _txt_754_SenRef = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRef").val();
    var _txt_754_RelRef = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_RelRef").val();

    var _txt_754_AddAmtClamd_Ccy = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_AddAmtClamd_Ccy").val();
    var _txt_754_AddAmtClamd_Amt = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_AddAmtClamd_Amt").val();

    var _ddlPrinAmtPaidAccNego_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlPrinAmtPaidAccNego_754").val();
    var _txtPrinAmtPaidAccNegoCurr_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoCurr_754").val();
    var _txtPrinAmtPaidAccNegoAmt_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoAmt_754").val();

    var _txtPrinAmtPaidAccNegoDate_754 = '';
    if (_ddlPrinAmtPaidAccNego_754 == 'A') {
        _txtPrinAmtPaidAccNegoDate_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoDate_754").val();
    }

    var _txt_MT754_Charges_Deducted = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesDeduct1").val();
    var _txt_MT754_Charges_Deducted2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesDeduct2").val();
    var _txt_MT754_Charges_Deducted3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesDeduct3").val();
    var _txt_MT754_Charges_Deducted4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesDeduct4").val();
    var _txt_MT754_Charges_Deducted5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesDeduct5").val();
    var _txt_MT754_Charges_Deducted6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesDeduct6").val();

    var _txt_MT754_Charges_Added = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesAdded1").val();
    var _txt_MT754_Charges_Added2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesAdded2").val();
    var _txt_MT754_Charges_Added3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesAdded3").val();
    var _txt_MT754_Charges_Added4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesAdded4").val();
    var _txt_MT754_Charges_Added5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesAdded5").val();
    var _txt_MT754_Charges_Added6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_ChargesAdded6").val();

    var _ddlTotalAmtclamd_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlTotalAmtclamd_754").val();
    var _txt_754_TotalAmtClmd_Ccy = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Ccy").val();
    var _txt_754_TotalAmtClmd_Amt = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Amt").val();

    var _txt_754_TotalAmtClmd_Date = '';
    if (_ddlTotalAmtclamd_754 == 'A') {
        _txt_754_TotalAmtClmd_Date = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Date").val();
    }

    var _ddlReimbursingbank_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlReimbursingbank_754").val();
    var _txtReimbursingBankAccountnumber_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAccountnumber_754").val();
    var _txtReimbursingBankpartyidentifier_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankpartyidentifier_754").val();

    var _txtReimbursingBankIdentifiercode_754 = '', _txtReimbursingBankLocation_754 = '', _txtReimbursingBankName_754 = '', _txtReimbursingBankAddress1_754 = '',
        _txtReimbursingBankAddress2_754 = '', _txtReimbursingBankAddress3_754 = '';
    if (_ddlReimbursingbank_754 == 'A') {
        _txtReimbursingBankIdentifiercode_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankIdentifiercode_754").val();
    }
    if (_ddlReimbursingbank_754 == 'B') {
        _txtReimbursingBankLocation_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankLocation_754").val();
    }
    if (_ddlReimbursingbank_754 == 'D') {
        _txtReimbursingBankName_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankName_754").val();
        _txtReimbursingBankAddress1_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAddress1_754").val();
        _txtReimbursingBankAddress2_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAddress2_754").val();
        _txtReimbursingBankAddress3_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAddress3_754").val();
    }

    var _ddlAccountwithbank_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlAccountwithbank_754").val();
    var _txtAccountwithBankAccountnumber_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAccountnumber_754").val();
    var _txtAccountwithBankpartyidentifier_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankpartyidentifier_754").val();

    var _txtAccountwithBankIdentifiercode_754 = '', _txtAccountwithBankLocation_754 = '', _txtAccountwithBankName_754 = '', _txtAccountwithBankAddress1_754 = '',
        _txtAccountwithBankAddress2_754 = '', _txtAccountwithBankAddress3_754 = '';
    if (_ddlAccountwithbank_754 == 'A') {
        _txtAccountwithBankIdentifiercode_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankIdentifiercode_754").val();
    }
    if (_ddlAccountwithbank_754 == 'B') {
        _txtAccountwithBankLocation_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankLocation_754").val();
    }
    if (_ddlAccountwithbank_754 == 'D') {
        _txtAccountwithBankName_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankName_754").val();
        _txtAccountwithBankAddress1_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAddress1_754").val();
        _txtAccountwithBankAddress2_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAddress2_754").val();
        _txtAccountwithBankAddress3_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAddress3_754").val();
    }

    var _ddlBeneficiarybank_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlBeneficiarybank_754").val();
    var _txtBeneficiaryBankAccountnumber_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiaryBankAccountnumber_754").val();
    var _txtBeneficiarypartyidentifire = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiarypartyidentifire").val();

    var _txtBeneficiaryBankIdentifiercode_754 = '', _txtBeneficiaryBankName_754 = '', _txtBeneficiaryBankAddress1_754 = '',
        _txtBeneficiaryBankAddress2_754 = '', _txtBeneficiaryBankAddress3_754 = '';
    if (_ddlBeneficiarybank_754 == 'A') {
        _txtBeneficiaryBankIdentifiercode_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiaryBankIdentifiercode_754").val();
    }
    if (_ddlBeneficiarybank_754 == 'D') {
        _txtBeneficiaryBankName_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiaryBankName_754").val();
        _txtBeneficiaryBankAddress1_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiaryBankAddress1_754").val();
        _txtBeneficiaryBankAddress2_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiaryBankAddress2_754").val();
        _txtBeneficiaryBankAddress3_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiaryBankAddress3_754").val();
    }
    var _txt_MT754_Sender_to_Receiver_Information = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo1").val();
    var _txt_MT754_Sender_to_Receiver_Information2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo2").val();
    var _txt_MT754_Sender_to_Receiver_Information3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo3").val();
    var _txt_MT754_Sender_to_Receiver_Information4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo4").val();
    var _txt_MT754_Sender_to_Receiver_Information5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo5").val();
    var _txt_MT754_Sender_to_Receiver_Information6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo6").val();

    var _txt_Narrative_754_1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_1").val();
    var _txt_Narrative_754_2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_2").val();
    var _txt_Narrative_754_3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_3").val();
    var _txt_Narrative_754_4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_4").val();
    var _txt_Narrative_754_5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_5").val();
    var _txt_Narrative_754_6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_6").val();
    var _txt_Narrative_754_7 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_7").val();
    var _txt_Narrative_754_8 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_8").val();
    var _txt_Narrative_754_9 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_9").val();
    var _txt_Narrative_754_10 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_10").val();
    var _txt_Narrative_754_11 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_11").val();
    var _txt_Narrative_754_12 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_12").val();
    var _txt_Narrative_754_13 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_13").val();
    var _txt_Narrative_754_14 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_14").val();
    var _txt_Narrative_754_15 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_15").val();
    var _txt_Narrative_754_16 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_16").val();
    var _txt_Narrative_754_17 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_17").val();
    var _txt_Narrative_754_18 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_18").val();
    var _txt_Narrative_754_19 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_19").val();
    var _txt_Narrative_754_20 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_20").val();
    var _txt_Narrative_754_21 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_21").val();
    var _txt_Narrative_754_22 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_22").val();
    var _txt_Narrative_754_23 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_23").val();
    var _txt_Narrative_754_24 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_24").val();
    var _txt_Narrative_754_25 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_25").val();
    var _txt_Narrative_754_26 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_26").val();
    var _txt_Narrative_754_27 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_27").val();
    var _txt_Narrative_754_28 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_28").val();
    var _txt_Narrative_754_29 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_29").val();
    var _txt_Narrative_754_30 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_30").val();
    var _txt_Narrative_754_31 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_31").val();
    var _txt_Narrative_754_32 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_32").val();
    var _txt_Narrative_754_33 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_33").val();
    var _txt_Narrative_754_34 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_34").val();
    var _txt_Narrative_754_35 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_35").val();

    //------------------------------------------------------Nilesh END-----------------------------------------------------------
    if (_txtDocNo != '') {

        $.ajax({
            type: "POST",
            url: "TF_IMP_Settlement_Collection_Maker.aspx/AddUpdateSettlement",
            data: '{ hdnUserName: "' + hdnUserName + '", _BranchName:"' + _BranchName + '", _txtDocNo:"' + _txtDocNo + '", _txt_Doc_Value_Date:"' + _txtValueDate +
            ///////// Doc Details
            '", _txt_Doc_Comment_Code:"' + _txt_Doc_Comment_Code + '", _txt_Doc_Maturity: "' + _txt_Doc_Maturity + '", _txt_Doc_Settlement_for_Cust:"' + _txt_Doc_Settlement_for_Cust + '", _txt_Doc_Settlement_For_Bank:"' + _txt_Doc_Settlement_For_Bank +
            '", _txt_Doc_Interest_From:"' + _txt_Doc_Interest_From + '", _txt_Doc_Interest_To:"' + _txt_Doc_Interest_To + '", _txt_Doc_No_Of_Days:"' + _txt_Doc_No_Of_Days + '", _txt_Doc_Discount: "' + _txt_Doc_Discount +
            '", _txt_Doc_InterestRate:"' + _txt_Doc_InterestRate + '", _txt_Doc_InterestAmount:"' + _txt_Doc_InterestAmount +
            '", _txt_Doc_Overdue_Interestrate:"' + _txt_Doc_Overdue_Interestrate + '", _txt_Doc_Oveduenoofdays:"' + _txt_Doc_Oveduenoofdays + '", _txt_Doc_Overdueamount:"' + _txt_Doc_Overdueamount + '", _txt_Doc_Attn:"' + _txt_Doc_Attn +
            ///////// Accounting 1/////////////
            '", _chk_IMPACC1Flag: "' + _chk_IMPACC1Flag + '", _txt_IMPACC1_FCRefNo:"' + _txt_IMPACC1_FCRefNo +
            '", _txt_IMPACC1_DiscAmt: "' + _txt_IMPACC1_DiscAmt + '", _txt_IMPACC1_DiscExchRate: "' + _txt_IMPACC1_DiscExchRate +
            '", _txt_IMPACC1_Princ_matu: "' + _txt_IMPACC1_Princ_matu + '", _txt_IMPACC1_Princ_lump: "' + _txt_IMPACC1_Princ_lump + '", _txt_IMPACC1_Princ_Contract_no: "' + _txt_IMPACC1_Princ_Contract_no + '", _txt_IMPACC1_Princ_Ex_Curr: "' + _txt_IMPACC1_Princ_Ex_Curr + '", _txt_IMPACC1_Princ_Ex_rate: "' + _txt_IMPACC1_Princ_Ex_rate + '", _txt_IMPACC1_Princ_Intnl_Ex_rate: "' + _txt_IMPACC1_Princ_Intnl_Ex_rate +
            '", _txt_IMPACC1_Interest_matu: "' + _txt_IMPACC1_Interest_matu + '", _txt_IMPACC1_Interest_lump: "' + _txt_IMPACC1_Interest_lump + '", _txt_IMPACC1_Interest_Contract_no:"' + _txt_IMPACC1_Interest_Contract_no + '", _txt_IMPACC1_Interest_Ex_Curr: "' + _txt_IMPACC1_Interest_Ex_Curr + '", _txt_IMPACC1_Interest_Ex_rate:"' + _txt_IMPACC1_Interest_Ex_rate + '", _txt_IMPACC1_Interest_Intnl_Ex_rate:"' + _txt_IMPACC1_Interest_Intnl_Ex_rate +
            '", _txt_IMPACC1_Commission_matu: "' + _txt_IMPACC1_Commission_matu + '", _txt_IMPACC1_Commission_lump:"' + _txt_IMPACC1_Commission_lump + '", _txt_IMPACC1_Commission_Contract_no:"' + _txt_IMPACC1_Commission_Contract_no + '", _txt_IMPACC1_Commission_Ex_Curr:"' + _txt_IMPACC1_Commission_Ex_Curr + '", _txt_IMPACC1_Commission_Ex_rate:"' + _txt_IMPACC1_Commission_Ex_rate + '", _txt_IMPACC1_Commission_Intnl_Ex_rate: "' + _txt_IMPACC1_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC1_Their_Commission_matu:"' + _txt_IMPACC1_Their_Commission_matu + '", _txt_IMPACC1_Their_Commission_lump:"' + _txt_IMPACC1_Their_Commission_lump + '", _txt_IMPACC1_Their_Commission_Contract_no: "' + _txt_IMPACC1_Their_Commission_Contract_no + '", _txt_IMPACC1_Their_Commission_Ex_Curr:"' + _txt_IMPACC1_Their_Commission_Ex_Curr + '", _txt_IMPACC1_Their_Commission_Ex_rate:"' + _txt_IMPACC1_Their_Commission_Ex_rate + '", _txt_IMPACC1_Their_Commission_Intnl_Ex_rate: "' + _txt_IMPACC1_Their_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC1_CR_Code:"' + _txt_IMPACC1_CR_Code + '", _txt_IMPACC1_CR_AC_Short_Name:"' + _txt_IMPACC1_CR_AC_Short_Name + '", _txt_IMPACC1_CR_Cust_abbr: "' + _txt_IMPACC1_CR_Cust_abbr + '", _txt_IMPACC1_CR_Cust_Acc:"' + _txt_IMPACC1_CR_Cust_Acc + '", _txt_IMPACC1_CR_Acceptance_Curr:"' + _txt_IMPACC1_CR_Acceptance_Curr + '", _txt_IMPACC1_CR_Acceptance_amt: "' + _txt_IMPACC1_CR_Acceptance_amt + '", _txt_IMPACC1_CR_Acceptance_payer:"' + _txt_IMPACC1_CR_Acceptance_payer +
            '", _txt_IMPACC1_CR_Interest_Curr :"' + _txt_IMPACC1_CR_Interest_Curr + '", _txt_IMPACC1_CR_Interest_amt: "' + _txt_IMPACC1_CR_Interest_amt + '", _txt_IMPACC1_CR_Interest_payer: "' + _txt_IMPACC1_CR_Interest_payer +
            '", _txt_IMPACC1_CR_Accept_Commission_Curr: "' + _txt_IMPACC1_CR_Accept_Commission_Curr + '", _txt_IMPACC1_CR_Accept_Commission_amt:"' + _txt_IMPACC1_CR_Accept_Commission_amt + '", _txt_IMPACC1_CR_Accept_Commission_Payer: "' + _txt_IMPACC1_CR_Accept_Commission_Payer +
            '", _txt_IMPACC1_CR_Pay_Handle_Commission_Curr: "' + _txt_IMPACC1_CR_Pay_Handle_Commission_Curr + '", _txt_IMPACC1_CR_Pay_Handle_Commission_amt: "' + _txt_IMPACC1_CR_Pay_Handle_Commission_amt + '", _txt_IMPACC1_CR_Pay_Handle_Commission_Payer: "' + _txt_IMPACC1_CR_Pay_Handle_Commission_Payer +
            '", _txt_IMPACC1_CR_Others_Curr: "' + _txt_IMPACC1_CR_Others_Curr + '", _txt_IMPACC1_CR_Others_amt: "' + _txt_IMPACC1_CR_Others_amt + '", _txt_IMPACC1_CR_Others_Payer: "' + _txt_IMPACC1_CR_Others_Payer +
            '", _txt_IMPACC1_CR_Their_Commission_Curr: "' + _txt_IMPACC1_CR_Their_Commission_Curr + '", _txt_IMPACC1_CR_Their_Commission_amt: "' + _txt_IMPACC1_CR_Their_Commission_amt + '", _txt_IMPACC1_CR_Their_Commission_Payer:"' + _txt_IMPACC1_CR_Their_Commission_Payer +

            '", _txt_IMPACC1_DR_Code: "' + _txt_IMPACC1_DR_Code + '", _txt_IMPACC1_DR_AC_Short_Name: "' + _txt_IMPACC1_DR_AC_Short_Name + '", _txt_IMPACC1_DR_Cust_abbr: "' + _txt_IMPACC1_DR_Cust_abbr + '", _txt_IMPACC1_DR_Cust_Acc: "' + _txt_IMPACC1_DR_Cust_Acc +
            '", _txt_IMPACC1_DR_Cur_Acc_Curr: "' + _txt_IMPACC1_DR_Cur_Acc_Curr + '", _txt_IMPACC1_DR_Cur_Acc_amt: "' + _txt_IMPACC1_DR_Cur_Acc_amt + '", _txt_IMPACC1_DR_Cur_Acc_payer : "' + _txt_IMPACC1_DR_Cur_Acc_payer +
            '", _txt_IMPACC1_DR_Cur_Acc_Curr2: "' + _txt_IMPACC1_DR_Cur_Acc_Curr2 + '", _txt_IMPACC1_DR_Cur_Acc_amt2: "' + _txt_IMPACC1_DR_Cur_Acc_amt2 + '", _txt_IMPACC1_DR_Cur_Acc_payer2: "' + _txt_IMPACC1_DR_Cur_Acc_payer2 +
            '", _txt_IMPACC1_DR_Cur_Acc_Curr3: "' + _txt_IMPACC1_DR_Cur_Acc_Curr3 + '", _txt_IMPACC1_DR_Cur_Acc_amt3: "' + _txt_IMPACC1_DR_Cur_Acc_amt3 + '", _txt_IMPACC1_DR_Cur_Acc_payer3: "' + _txt_IMPACC1_DR_Cur_Acc_payer3 +

            '", _txt_IMPACC1_DR_Cur_Acc_Curr4: "' + _txt_IMPACC1_DR_Cur_Acc_Curr4 + '", _txt_IMPACC1_DR_Cur_Acc_amt4: "' + _txt_IMPACC1_DR_Cur_Acc_amt4 + '", _txt_IMPACC1_DR_Cur_Acc_payer4: "' + _txt_IMPACC1_DR_Cur_Acc_payer4 +
            '", _txt_IMPACC1_DR_Cur_Acc_Curr5: "' + _txt_IMPACC1_DR_Cur_Acc_Curr5 + '", _txt_IMPACC1_DR_Cur_Acc_amt5: "' + _txt_IMPACC1_DR_Cur_Acc_amt5 + '", _txt_IMPACC1_DR_Cur_Acc_payer5: "' + _txt_IMPACC1_DR_Cur_Acc_payer5 +
            '", _txt_IMPACC1_DR_Cur_Acc_Curr6: "' + _txt_IMPACC1_DR_Cur_Acc_Curr6 + '", _txt_IMPACC1_DR_Cur_Acc_amt6: "' + _txt_IMPACC1_DR_Cur_Acc_amt6 + '", _txt_IMPACC1_DR_Cur_Acc_payer6: "' + _txt_IMPACC1_DR_Cur_Acc_payer6 +

            '", _txt_IMPACC1_DR_Code2: "' + _txt_IMPACC1_DR_Code2 + '", _txt_IMPACC1_DR_AC_Short_Name2: "' + _txt_IMPACC1_DR_AC_Short_Name2 + '", _txt_IMPACC1_DR_Cust_abbr2: "' + _txt_IMPACC1_DR_Cust_abbr2 + '", _txt_IMPACC1_DR_Cust_Acc2: "' + _txt_IMPACC1_DR_Cust_Acc2 +
            '", _txt_IMPACC1_DR_Code3: "' + _txt_IMPACC1_DR_Code3 + '", _txt_IMPACC1_DR_AC_Short_Name3: "' + _txt_IMPACC1_DR_AC_Short_Name3 + '", _txt_IMPACC1_DR_Cust_abbr3: "' + _txt_IMPACC1_DR_Cust_abbr3 + '", _txt_IMPACC1_DR_Cust_Acc3: "' + _txt_IMPACC1_DR_Cust_Acc3 +
            '", _txt_IMPACC1_DR_Code4: "' + _txt_IMPACC1_DR_Code4 + '", _txt_IMPACC1_DR_AC_Short_Name4: "' + _txt_IMPACC1_DR_AC_Short_Name4 + '", _txt_IMPACC1_DR_Cust_abbr4: "' + _txt_IMPACC1_DR_Cust_abbr4 + '", _txt_IMPACC1_DR_Cust_Acc4: "' + _txt_IMPACC1_DR_Cust_Acc4 +
            '", _txt_IMPACC1_DR_Code5: "' + _txt_IMPACC1_DR_Code5 + '", _txt_IMPACC1_DR_AC_Short_Name5: "' + _txt_IMPACC1_DR_AC_Short_Name5 + '", _txt_IMPACC1_DR_Cust_abbr5: "' + _txt_IMPACC1_DR_Cust_abbr5 + '", _txt_IMPACC1_DR_Cust_Acc5: "' + _txt_IMPACC1_DR_Cust_Acc5 +
            '", _txt_IMPACC1_DR_Code6: "' + _txt_IMPACC1_DR_Code6 + '", _txt_IMPACC1_DR_AC_Short_Name6: "' + _txt_IMPACC1_DR_AC_Short_Name6 + '", _txt_IMPACC1_DR_Cust_abbr6: "' + _txt_IMPACC1_DR_Cust_abbr6 + '", _txt_IMPACC1_DR_Cust_Acc6: "' + _txt_IMPACC1_DR_Cust_Acc6 +

            /////////IMPORT ACCOUNTING 2////////////
            '", _chk_IMPACC2Flag: "' + _chk_IMPACC2Flag + '", _txt_IMPACC2_FCRefNo:"' + _txt_IMPACC2_FCRefNo +
            '", _txt_IMPACC2_DiscAmt: "' + _txt_IMPACC2_DiscAmt + '", _txt_IMPACC2_DiscExchRate: "' + _txt_IMPACC2_DiscExchRate +
            '", _txt_IMPACC2_Princ_matu: "' + _txt_IMPACC2_Princ_matu + '", _txt_IMPACC2_Princ_lump: "' + _txt_IMPACC2_Princ_lump + '", _txt_IMPACC2_Princ_Contract_no: "' + _txt_IMPACC2_Princ_Contract_no + '", _txt_IMPACC2_Princ_Ex_Curr: "' + _txt_IMPACC2_Princ_Ex_Curr + '", _txt_IMPACC2_Princ_Ex_rate: "' + _txt_IMPACC2_Princ_Ex_rate + '", _txt_IMPACC2_Princ_Intnl_Ex_rate: "' + _txt_IMPACC2_Princ_Intnl_Ex_rate +
            '", _txt_IMPACC2_Interest_matu: "' + _txt_IMPACC2_Interest_matu + '", _txt_IMPACC2_Interest_lump: "' + _txt_IMPACC2_Interest_lump + '", _txt_IMPACC2_Interest_Contract_no:"' + _txt_IMPACC2_Interest_Contract_no + '", _txt_IMPACC2_Interest_Ex_Curr: "' + _txt_IMPACC2_Interest_Ex_Curr + '", _txt_IMPACC2_Interest_Ex_rate:"' + _txt_IMPACC2_Interest_Ex_rate + '", _txt_IMPACC2_Interest_Intnl_Ex_rate:"' + _txt_IMPACC2_Interest_Intnl_Ex_rate +
            '", _txt_IMPACC2_Commission_matu: "' + _txt_IMPACC2_Commission_matu + '", _txt_IMPACC2_Commission_lump:"' + _txt_IMPACC2_Commission_lump + '", _txt_IMPACC2_Commission_Contract_no:"' + _txt_IMPACC2_Commission_Contract_no + '", _txt_IMPACC2_Commission_Ex_Curr:"' + _txt_IMPACC2_Commission_Ex_Curr + '", _txt_IMPACC2_Commission_Ex_rate:"' + _txt_IMPACC2_Commission_Ex_rate + '", _txt_IMPACC2_Commission_Intnl_Ex_rate: "' + _txt_IMPACC2_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC2_Their_Commission_matu:"' + _txt_IMPACC2_Their_Commission_matu + '", _txt_IMPACC2_Their_Commission_lump:"' + _txt_IMPACC2_Their_Commission_lump + '", _txt_IMPACC2_Their_Commission_Contract_no: "' + _txt_IMPACC2_Their_Commission_Contract_no + '", _txt_IMPACC2_Their_Commission_Ex_Curr:"' + _txt_IMPACC2_Their_Commission_Ex_Curr + '", _txt_IMPACC2_Their_Commission_Ex_rate:"' + _txt_IMPACC2_Their_Commission_Ex_rate + '", _txt_IMPACC2_Their_Commission_Intnl_Ex_rate: "' + _txt_IMPACC2_Their_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC2_CR_Code:"' + _txt_IMPACC2_CR_Code + '", _txt_IMPACC2_CR_AC_Short_Name:"' + _txt_IMPACC2_CR_AC_Short_Name + '", _txt_IMPACC2_CR_Cust_abbr: "' + _txt_IMPACC2_CR_Cust_abbr + '", _txt_IMPACC2_CR_Cust_Acc:"' + _txt_IMPACC2_CR_Cust_Acc + '", _txt_IMPACC2_CR_Acceptance_Curr:"' + _txt_IMPACC2_CR_Acceptance_Curr + '", _txt_IMPACC2_CR_Acceptance_amt: "' + _txt_IMPACC2_CR_Acceptance_amt + '", _txt_IMPACC2_CR_Acceptance_payer:"' + _txt_IMPACC2_CR_Acceptance_payer +
            '", _txt_IMPACC2_CR_Interest_Curr :"' + _txt_IMPACC2_CR_Interest_Curr + '", _txt_IMPACC2_CR_Interest_amt: "' + _txt_IMPACC2_CR_Interest_amt + '", _txt_IMPACC2_CR_Interest_payer: "' + _txt_IMPACC2_CR_Interest_payer +
            '", _txt_IMPACC2_CR_Accept_Commission_Curr: "' + _txt_IMPACC2_CR_Accept_Commission_Curr + '", _txt_IMPACC2_CR_Accept_Commission_amt:"' + _txt_IMPACC2_CR_Accept_Commission_amt + '", _txt_IMPACC2_CR_Accept_Commission_Payer: "' + _txt_IMPACC2_CR_Accept_Commission_Payer +
            '", _txt_IMPACC2_CR_Pay_Handle_Commission_Curr: "' + _txt_IMPACC2_CR_Pay_Handle_Commission_Curr + '", _txt_IMPACC2_CR_Pay_Handle_Commission_amt: "' + _txt_IMPACC2_CR_Pay_Handle_Commission_amt + '", _txt_IMPACC2_CR_Pay_Handle_Commission_Payer: "' + _txt_IMPACC2_CR_Pay_Handle_Commission_Payer +
            '", _txt_IMPACC2_CR_Others_Curr: "' + _txt_IMPACC2_CR_Others_Curr + '", _txt_IMPACC2_CR_Others_amt: "' + _txt_IMPACC2_CR_Others_amt + '", _txt_IMPACC2_CR_Others_Payer: "' + _txt_IMPACC2_CR_Others_Payer +
            '", _txt_IMPACC2_CR_Their_Commission_Curr: "' + _txt_IMPACC2_CR_Their_Commission_Curr + '", _txt_IMPACC2_CR_Their_Commission_amt: "' + _txt_IMPACC2_CR_Their_Commission_amt + '", _txt_IMPACC2_CR_Their_Commission_Payer:"' + _txt_IMPACC2_CR_Their_Commission_Payer +

            '", _txt_IMPACC2_DR_Code: "' + _txt_IMPACC2_DR_Code + '", _txt_IMPACC2_DR_AC_Short_Name: "' + _txt_IMPACC2_DR_AC_Short_Name + '", _txt_IMPACC2_DR_Cust_abbr: "' + _txt_IMPACC2_DR_Cust_abbr + '", _txt_IMPACC2_DR_Cust_Acc: "' + _txt_IMPACC2_DR_Cust_Acc +
            '", _txt_IMPACC2_DR_Cur_Acc_Curr: "' + _txt_IMPACC2_DR_Cur_Acc_Curr + '", _txt_IMPACC2_DR_Cur_Acc_amt: "' + _txt_IMPACC2_DR_Cur_Acc_amt + '", _txt_IMPACC2_DR_Cur_Acc_payer : "' + _txt_IMPACC2_DR_Cur_Acc_payer +
            '", _txt_IMPACC2_DR_Cur_Acc_Curr2: "' + _txt_IMPACC2_DR_Cur_Acc_Curr2 + '", _txt_IMPACC2_DR_Cur_Acc_amt2: "' + _txt_IMPACC2_DR_Cur_Acc_amt2 + '", _txt_IMPACC2_DR_Cur_Acc_payer2: "' + _txt_IMPACC2_DR_Cur_Acc_payer2 +
            '", _txt_IMPACC2_DR_Cur_Acc_Curr3: "' + _txt_IMPACC2_DR_Cur_Acc_Curr3 + '", _txt_IMPACC2_DR_Cur_Acc_amt3: "' + _txt_IMPACC2_DR_Cur_Acc_amt3 + '", _txt_IMPACC2_DR_Cur_Acc_payer3: "' + _txt_IMPACC2_DR_Cur_Acc_payer3 +

            '", _txt_IMPACC2_DR_Cur_Acc_Curr4: "' + _txt_IMPACC2_DR_Cur_Acc_Curr4 + '", _txt_IMPACC2_DR_Cur_Acc_amt4: "' + _txt_IMPACC2_DR_Cur_Acc_amt4 + '", _txt_IMPACC2_DR_Cur_Acc_payer4: "' + _txt_IMPACC2_DR_Cur_Acc_payer4 +
            '", _txt_IMPACC2_DR_Cur_Acc_Curr5: "' + _txt_IMPACC2_DR_Cur_Acc_Curr5 + '", _txt_IMPACC2_DR_Cur_Acc_amt5: "' + _txt_IMPACC2_DR_Cur_Acc_amt5 + '", _txt_IMPACC2_DR_Cur_Acc_payer5: "' + _txt_IMPACC2_DR_Cur_Acc_payer5 +
            '", _txt_IMPACC2_DR_Cur_Acc_Curr6: "' + _txt_IMPACC2_DR_Cur_Acc_Curr6 + '", _txt_IMPACC2_DR_Cur_Acc_amt6: "' + _txt_IMPACC2_DR_Cur_Acc_amt6 + '", _txt_IMPACC2_DR_Cur_Acc_payer6: "' + _txt_IMPACC2_DR_Cur_Acc_payer6 +

            '", _txt_IMPACC2_DR_Code2: "' + _txt_IMPACC2_DR_Code2 + '", _txt_IMPACC2_DR_AC_Short_Name2: "' + _txt_IMPACC2_DR_AC_Short_Name2 + '", _txt_IMPACC2_DR_Cust_abbr2: "' + _txt_IMPACC2_DR_Cust_abbr2 + '", _txt_IMPACC2_DR_Cust_Acc2: "' + _txt_IMPACC2_DR_Cust_Acc2 +
            '", _txt_IMPACC2_DR_Code3: "' + _txt_IMPACC2_DR_Code3 + '", _txt_IMPACC2_DR_AC_Short_Name3: "' + _txt_IMPACC2_DR_AC_Short_Name3 + '", _txt_IMPACC2_DR_Cust_abbr3: "' + _txt_IMPACC2_DR_Cust_abbr3 + '", _txt_IMPACC2_DR_Cust_Acc3: "' + _txt_IMPACC2_DR_Cust_Acc3 +
            '", _txt_IMPACC2_DR_Code4: "' + _txt_IMPACC2_DR_Code4 + '", _txt_IMPACC2_DR_AC_Short_Name4: "' + _txt_IMPACC2_DR_AC_Short_Name4 + '", _txt_IMPACC2_DR_Cust_abbr4: "' + _txt_IMPACC2_DR_Cust_abbr4 + '", _txt_IMPACC2_DR_Cust_Acc4: "' + _txt_IMPACC2_DR_Cust_Acc4 +
            '", _txt_IMPACC2_DR_Code5: "' + _txt_IMPACC2_DR_Code5 + '", _txt_IMPACC2_DR_AC_Short_Name5: "' + _txt_IMPACC2_DR_AC_Short_Name5 + '", _txt_IMPACC2_DR_Cust_abbr5: "' + _txt_IMPACC2_DR_Cust_abbr5 + '", _txt_IMPACC2_DR_Cust_Acc5: "' + _txt_IMPACC2_DR_Cust_Acc5 +
            '", _txt_IMPACC2_DR_Code6: "' + _txt_IMPACC2_DR_Code6 + '", _txt_IMPACC2_DR_AC_Short_Name6: "' + _txt_IMPACC2_DR_AC_Short_Name6 + '", _txt_IMPACC2_DR_Cust_abbr6: "' + _txt_IMPACC2_DR_Cust_abbr6 + '", _txt_IMPACC2_DR_Cust_Acc6: "' + _txt_IMPACC2_DR_Cust_Acc6 +

            /////////IMPORT ACCOUNTING 3////////////
            '", _chk_IMPACC3Flag: "' + _chk_IMPACC3Flag + '", _txt_IMPACC3_FCRefNo:"' + _txt_IMPACC3_FCRefNo +
            '", _txt_IMPACC3_DiscAmt: "' + _txt_IMPACC3_DiscAmt + '", _txt_IMPACC3_DiscExchRate: "' + _txt_IMPACC3_DiscExchRate +
            '", _txt_IMPACC3_Princ_matu: "' + _txt_IMPACC3_Princ_matu + '", _txt_IMPACC3_Princ_lump: "' + _txt_IMPACC3_Princ_lump + '", _txt_IMPACC3_Princ_Contract_no: "' + _txt_IMPACC3_Princ_Contract_no + '", _txt_IMPACC3_Princ_Ex_Curr: "' + _txt_IMPACC3_Princ_Ex_Curr + '", _txt_IMPACC3_Princ_Ex_rate: "' + _txt_IMPACC3_Princ_Ex_rate + '", _txt_IMPACC3_Princ_Intnl_Ex_rate: "' + _txt_IMPACC3_Princ_Intnl_Ex_rate +
            '", _txt_IMPACC3_Interest_matu: "' + _txt_IMPACC3_Interest_matu + '", _txt_IMPACC3_Interest_lump: "' + _txt_IMPACC3_Interest_lump + '", _txt_IMPACC3_Interest_Contract_no:"' + _txt_IMPACC3_Interest_Contract_no + '", _txt_IMPACC3_Interest_Ex_Curr: "' + _txt_IMPACC3_Interest_Ex_Curr + '", _txt_IMPACC3_Interest_Ex_rate:"' + _txt_IMPACC3_Interest_Ex_rate + '", _txt_IMPACC3_Interest_Intnl_Ex_rate:"' + _txt_IMPACC3_Interest_Intnl_Ex_rate +
            '", _txt_IMPACC3_Commission_matu: "' + _txt_IMPACC3_Commission_matu + '", _txt_IMPACC3_Commission_lump:"' + _txt_IMPACC3_Commission_lump + '", _txt_IMPACC3_Commission_Contract_no:"' + _txt_IMPACC3_Commission_Contract_no + '", _txt_IMPACC3_Commission_Ex_Curr:"' + _txt_IMPACC3_Commission_Ex_Curr + '", _txt_IMPACC3_Commission_Ex_rate:"' + _txt_IMPACC3_Commission_Ex_rate + '", _txt_IMPACC3_Commission_Intnl_Ex_rate: "' + _txt_IMPACC3_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC3_Their_Commission_matu:"' + _txt_IMPACC3_Their_Commission_matu + '", _txt_IMPACC3_Their_Commission_lump:"' + _txt_IMPACC3_Their_Commission_lump + '", _txt_IMPACC3_Their_Commission_Contract_no: "' + _txt_IMPACC3_Their_Commission_Contract_no + '", _txt_IMPACC3_Their_Commission_Ex_Curr:"' + _txt_IMPACC3_Their_Commission_Ex_Curr + '", _txt_IMPACC3_Their_Commission_Ex_rate:"' + _txt_IMPACC3_Their_Commission_Ex_rate + '", _txt_IMPACC3_Their_Commission_Intnl_Ex_rate: "' + _txt_IMPACC3_Their_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC3_CR_Code:"' + _txt_IMPACC3_CR_Code + '", _txt_IMPACC3_CR_AC_Short_Name:"' + _txt_IMPACC3_CR_AC_Short_Name + '", _txt_IMPACC3_CR_Cust_abbr: "' + _txt_IMPACC3_CR_Cust_abbr + '", _txt_IMPACC3_CR_Cust_Acc:"' + _txt_IMPACC3_CR_Cust_Acc + '", _txt_IMPACC3_CR_Acceptance_Curr:"' + _txt_IMPACC3_CR_Acceptance_Curr + '", _txt_IMPACC3_CR_Acceptance_amt: "' + _txt_IMPACC3_CR_Acceptance_amt + '", _txt_IMPACC3_CR_Acceptance_payer:"' + _txt_IMPACC3_CR_Acceptance_payer +
            '", _txt_IMPACC3_CR_Interest_Curr :"' + _txt_IMPACC3_CR_Interest_Curr + '", _txt_IMPACC3_CR_Interest_amt: "' + _txt_IMPACC3_CR_Interest_amt + '", _txt_IMPACC3_CR_Interest_payer: "' + _txt_IMPACC3_CR_Interest_payer +
            '", _txt_IMPACC3_CR_Accept_Commission_Curr: "' + _txt_IMPACC3_CR_Accept_Commission_Curr + '", _txt_IMPACC3_CR_Accept_Commission_amt:"' + _txt_IMPACC3_CR_Accept_Commission_amt + '", _txt_IMPACC3_CR_Accept_Commission_Payer: "' + _txt_IMPACC3_CR_Accept_Commission_Payer +
            '", _txt_IMPACC3_CR_Pay_Handle_Commission_Curr: "' + _txt_IMPACC3_CR_Pay_Handle_Commission_Curr + '", _txt_IMPACC3_CR_Pay_Handle_Commission_amt: "' + _txt_IMPACC3_CR_Pay_Handle_Commission_amt + '", _txt_IMPACC3_CR_Pay_Handle_Commission_Payer: "' + _txt_IMPACC3_CR_Pay_Handle_Commission_Payer +
            '", _txt_IMPACC3_CR_Others_Curr: "' + _txt_IMPACC3_CR_Others_Curr + '", _txt_IMPACC3_CR_Others_amt: "' + _txt_IMPACC3_CR_Others_amt + '", _txt_IMPACC3_CR_Others_Payer: "' + _txt_IMPACC3_CR_Others_Payer +
            '", _txt_IMPACC3_CR_Their_Commission_Curr: "' + _txt_IMPACC3_CR_Their_Commission_Curr + '", _txt_IMPACC3_CR_Their_Commission_amt: "' + _txt_IMPACC3_CR_Their_Commission_amt + '", _txt_IMPACC3_CR_Their_Commission_Payer:"' + _txt_IMPACC3_CR_Their_Commission_Payer +

            '", _txt_IMPACC3_DR_Code: "' + _txt_IMPACC3_DR_Code + '", _txt_IMPACC3_DR_AC_Short_Name: "' + _txt_IMPACC3_DR_AC_Short_Name + '", _txt_IMPACC3_DR_Cust_abbr: "' + _txt_IMPACC3_DR_Cust_abbr + '", _txt_IMPACC3_DR_Cust_Acc: "' + _txt_IMPACC3_DR_Cust_Acc +
            '", _txt_IMPACC3_DR_Cur_Acc_Curr: "' + _txt_IMPACC3_DR_Cur_Acc_Curr + '", _txt_IMPACC3_DR_Cur_Acc_amt: "' + _txt_IMPACC3_DR_Cur_Acc_amt + '", _txt_IMPACC3_DR_Cur_Acc_payer : "' + _txt_IMPACC3_DR_Cur_Acc_payer +
            '", _txt_IMPACC3_DR_Cur_Acc_Curr2: "' + _txt_IMPACC3_DR_Cur_Acc_Curr2 + '", _txt_IMPACC3_DR_Cur_Acc_amt2: "' + _txt_IMPACC3_DR_Cur_Acc_amt2 + '", _txt_IMPACC3_DR_Cur_Acc_payer2: "' + _txt_IMPACC3_DR_Cur_Acc_payer2 +
            '", _txt_IMPACC3_DR_Cur_Acc_Curr3: "' + _txt_IMPACC3_DR_Cur_Acc_Curr3 + '", _txt_IMPACC3_DR_Cur_Acc_amt3: "' + _txt_IMPACC3_DR_Cur_Acc_amt3 + '", _txt_IMPACC3_DR_Cur_Acc_payer3: "' + _txt_IMPACC3_DR_Cur_Acc_payer3 +

            '", _txt_IMPACC3_DR_Cur_Acc_Curr4: "' + _txt_IMPACC3_DR_Cur_Acc_Curr4 + '", _txt_IMPACC3_DR_Cur_Acc_amt4: "' + _txt_IMPACC3_DR_Cur_Acc_amt4 + '", _txt_IMPACC3_DR_Cur_Acc_payer4: "' + _txt_IMPACC3_DR_Cur_Acc_payer4 +
            '", _txt_IMPACC3_DR_Cur_Acc_Curr5: "' + _txt_IMPACC3_DR_Cur_Acc_Curr5 + '", _txt_IMPACC3_DR_Cur_Acc_amt5: "' + _txt_IMPACC3_DR_Cur_Acc_amt5 + '", _txt_IMPACC3_DR_Cur_Acc_payer5: "' + _txt_IMPACC3_DR_Cur_Acc_payer5 +
            '", _txt_IMPACC3_DR_Cur_Acc_Curr6: "' + _txt_IMPACC3_DR_Cur_Acc_Curr6 + '", _txt_IMPACC3_DR_Cur_Acc_amt6: "' + _txt_IMPACC3_DR_Cur_Acc_amt6 + '", _txt_IMPACC3_DR_Cur_Acc_payer6: "' + _txt_IMPACC3_DR_Cur_Acc_payer6 +

            '", _txt_IMPACC3_DR_Code2: "' + _txt_IMPACC3_DR_Code2 + '", _txt_IMPACC3_DR_AC_Short_Name2: "' + _txt_IMPACC3_DR_AC_Short_Name2 + '", _txt_IMPACC3_DR_Cust_abbr2: "' + _txt_IMPACC3_DR_Cust_abbr2 + '", _txt_IMPACC3_DR_Cust_Acc2: "' + _txt_IMPACC3_DR_Cust_Acc2 +
            '", _txt_IMPACC3_DR_Code3: "' + _txt_IMPACC3_DR_Code3 + '", _txt_IMPACC3_DR_AC_Short_Name3: "' + _txt_IMPACC3_DR_AC_Short_Name3 + '", _txt_IMPACC3_DR_Cust_abbr3: "' + _txt_IMPACC3_DR_Cust_abbr3 + '", _txt_IMPACC3_DR_Cust_Acc3: "' + _txt_IMPACC3_DR_Cust_Acc3 +
            '", _txt_IMPACC3_DR_Code4: "' + _txt_IMPACC3_DR_Code4 + '", _txt_IMPACC3_DR_AC_Short_Name4: "' + _txt_IMPACC3_DR_AC_Short_Name4 + '", _txt_IMPACC3_DR_Cust_abbr4: "' + _txt_IMPACC3_DR_Cust_abbr4 + '", _txt_IMPACC3_DR_Cust_Acc4: "' + _txt_IMPACC3_DR_Cust_Acc4 +
            '", _txt_IMPACC3_DR_Code5: "' + _txt_IMPACC3_DR_Code5 + '", _txt_IMPACC3_DR_AC_Short_Name5: "' + _txt_IMPACC3_DR_AC_Short_Name5 + '", _txt_IMPACC3_DR_Cust_abbr5: "' + _txt_IMPACC3_DR_Cust_abbr5 + '", _txt_IMPACC3_DR_Cust_Acc5: "' + _txt_IMPACC3_DR_Cust_Acc5 +
            '", _txt_IMPACC3_DR_Code6: "' + _txt_IMPACC3_DR_Code6 + '", _txt_IMPACC3_DR_AC_Short_Name6: "' + _txt_IMPACC3_DR_AC_Short_Name6 + '", _txt_IMPACC3_DR_Cust_abbr6: "' + _txt_IMPACC3_DR_Cust_abbr6 + '", _txt_IMPACC3_DR_Cust_Acc6: "' + _txt_IMPACC3_DR_Cust_Acc6 +

            /////////IMPORT ACCOUNTING 4////////////
            '", _chk_IMPACC4Flag: "' + _chk_IMPACC4Flag + '", _txt_IMPACC4_FCRefNo:"' + _txt_IMPACC4_FCRefNo +
            '", _txt_IMPACC4_DiscAmt: "' + _txt_IMPACC4_DiscAmt + '", _txt_IMPACC4_DiscExchRate: "' + _txt_IMPACC4_DiscExchRate +
            '", _txt_IMPACC4_Princ_matu: "' + _txt_IMPACC4_Princ_matu + '", _txt_IMPACC4_Princ_lump: "' + _txt_IMPACC4_Princ_lump + '", _txt_IMPACC4_Princ_Contract_no: "' + _txt_IMPACC4_Princ_Contract_no + '", _txt_IMPACC4_Princ_Ex_Curr: "' + _txt_IMPACC4_Princ_Ex_Curr + '", _txt_IMPACC4_Princ_Ex_rate: "' + _txt_IMPACC4_Princ_Ex_rate + '", _txt_IMPACC4_Princ_Intnl_Ex_rate: "' + _txt_IMPACC4_Princ_Intnl_Ex_rate +
            '", _txt_IMPACC4_Interest_matu: "' + _txt_IMPACC4_Interest_matu + '", _txt_IMPACC4_Interest_lump: "' + _txt_IMPACC4_Interest_lump + '", _txt_IMPACC4_Interest_Contract_no:"' + _txt_IMPACC4_Interest_Contract_no + '", _txt_IMPACC4_Interest_Ex_Curr: "' + _txt_IMPACC4_Interest_Ex_Curr + '", _txt_IMPACC4_Interest_Ex_rate:"' + _txt_IMPACC4_Interest_Ex_rate + '", _txt_IMPACC4_Interest_Intnl_Ex_rate:"' + _txt_IMPACC4_Interest_Intnl_Ex_rate +
            '", _txt_IMPACC4_Commission_matu: "' + _txt_IMPACC4_Commission_matu + '", _txt_IMPACC4_Commission_lump:"' + _txt_IMPACC4_Commission_lump + '", _txt_IMPACC4_Commission_Contract_no:"' + _txt_IMPACC4_Commission_Contract_no + '", _txt_IMPACC4_Commission_Ex_Curr:"' + _txt_IMPACC4_Commission_Ex_Curr + '", _txt_IMPACC4_Commission_Ex_rate:"' + _txt_IMPACC4_Commission_Ex_rate + '", _txt_IMPACC4_Commission_Intnl_Ex_rate: "' + _txt_IMPACC4_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC4_Their_Commission_matu:"' + _txt_IMPACC4_Their_Commission_matu + '", _txt_IMPACC4_Their_Commission_lump:"' + _txt_IMPACC4_Their_Commission_lump + '", _txt_IMPACC4_Their_Commission_Contract_no: "' + _txt_IMPACC4_Their_Commission_Contract_no + '", _txt_IMPACC4_Their_Commission_Ex_Curr:"' + _txt_IMPACC4_Their_Commission_Ex_Curr + '", _txt_IMPACC4_Their_Commission_Ex_rate:"' + _txt_IMPACC4_Their_Commission_Ex_rate + '", _txt_IMPACC4_Their_Commission_Intnl_Ex_rate: "' + _txt_IMPACC4_Their_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC4_CR_Code:"' + _txt_IMPACC4_CR_Code + '", _txt_IMPACC4_CR_AC_Short_Name:"' + _txt_IMPACC4_CR_AC_Short_Name + '", _txt_IMPACC4_CR_Cust_abbr: "' + _txt_IMPACC4_CR_Cust_abbr + '", _txt_IMPACC4_CR_Cust_Acc:"' + _txt_IMPACC4_CR_Cust_Acc + '", _txt_IMPACC4_CR_Acceptance_Curr:"' + _txt_IMPACC4_CR_Acceptance_Curr + '", _txt_IMPACC4_CR_Acceptance_amt: "' + _txt_IMPACC4_CR_Acceptance_amt + '", _txt_IMPACC4_CR_Acceptance_payer:"' + _txt_IMPACC4_CR_Acceptance_payer +
            '", _txt_IMPACC4_CR_Interest_Curr :"' + _txt_IMPACC4_CR_Interest_Curr + '", _txt_IMPACC4_CR_Interest_amt: "' + _txt_IMPACC4_CR_Interest_amt + '", _txt_IMPACC4_CR_Interest_payer: "' + _txt_IMPACC4_CR_Interest_payer +
            '", _txt_IMPACC4_CR_Accept_Commission_Curr: "' + _txt_IMPACC4_CR_Accept_Commission_Curr + '", _txt_IMPACC4_CR_Accept_Commission_amt:"' + _txt_IMPACC4_CR_Accept_Commission_amt + '", _txt_IMPACC4_CR_Accept_Commission_Payer: "' + _txt_IMPACC4_CR_Accept_Commission_Payer +
            '", _txt_IMPACC4_CR_Pay_Handle_Commission_Curr: "' + _txt_IMPACC4_CR_Pay_Handle_Commission_Curr + '", _txt_IMPACC4_CR_Pay_Handle_Commission_amt: "' + _txt_IMPACC4_CR_Pay_Handle_Commission_amt + '", _txt_IMPACC4_CR_Pay_Handle_Commission_Payer: "' + _txt_IMPACC4_CR_Pay_Handle_Commission_Payer +
            '", _txt_IMPACC4_CR_Others_Curr: "' + _txt_IMPACC4_CR_Others_Curr + '", _txt_IMPACC4_CR_Others_amt: "' + _txt_IMPACC4_CR_Others_amt + '", _txt_IMPACC4_CR_Others_Payer: "' + _txt_IMPACC4_CR_Others_Payer +
            '", _txt_IMPACC4_CR_Their_Commission_Curr: "' + _txt_IMPACC4_CR_Their_Commission_Curr + '", _txt_IMPACC4_CR_Their_Commission_amt: "' + _txt_IMPACC4_CR_Their_Commission_amt + '", _txt_IMPACC4_CR_Their_Commission_Payer:"' + _txt_IMPACC4_CR_Their_Commission_Payer +

            '", _txt_IMPACC4_DR_Code: "' + _txt_IMPACC4_DR_Code + '", _txt_IMPACC4_DR_AC_Short_Name: "' + _txt_IMPACC4_DR_AC_Short_Name + '", _txt_IMPACC4_DR_Cust_abbr: "' + _txt_IMPACC4_DR_Cust_abbr + '", _txt_IMPACC4_DR_Cust_Acc: "' + _txt_IMPACC4_DR_Cust_Acc +
            '", _txt_IMPACC4_DR_Cur_Acc_Curr: "' + _txt_IMPACC4_DR_Cur_Acc_Curr + '", _txt_IMPACC4_DR_Cur_Acc_amt: "' + _txt_IMPACC4_DR_Cur_Acc_amt + '", _txt_IMPACC4_DR_Cur_Acc_payer : "' + _txt_IMPACC4_DR_Cur_Acc_payer +
            '", _txt_IMPACC4_DR_Cur_Acc_Curr2: "' + _txt_IMPACC4_DR_Cur_Acc_Curr2 + '", _txt_IMPACC4_DR_Cur_Acc_amt2: "' + _txt_IMPACC4_DR_Cur_Acc_amt2 + '", _txt_IMPACC4_DR_Cur_Acc_payer2: "' + _txt_IMPACC4_DR_Cur_Acc_payer2 +
            '", _txt_IMPACC4_DR_Cur_Acc_Curr3: "' + _txt_IMPACC4_DR_Cur_Acc_Curr3 + '", _txt_IMPACC4_DR_Cur_Acc_amt3: "' + _txt_IMPACC4_DR_Cur_Acc_amt3 + '", _txt_IMPACC4_DR_Cur_Acc_payer3: "' + _txt_IMPACC4_DR_Cur_Acc_payer3 +

            '", _txt_IMPACC4_DR_Cur_Acc_Curr4: "' + _txt_IMPACC4_DR_Cur_Acc_Curr4 + '", _txt_IMPACC4_DR_Cur_Acc_amt4: "' + _txt_IMPACC4_DR_Cur_Acc_amt4 + '", _txt_IMPACC4_DR_Cur_Acc_payer4: "' + _txt_IMPACC4_DR_Cur_Acc_payer4 +
            '", _txt_IMPACC4_DR_Cur_Acc_Curr5: "' + _txt_IMPACC4_DR_Cur_Acc_Curr5 + '", _txt_IMPACC4_DR_Cur_Acc_amt5: "' + _txt_IMPACC4_DR_Cur_Acc_amt5 + '", _txt_IMPACC4_DR_Cur_Acc_payer5: "' + _txt_IMPACC4_DR_Cur_Acc_payer5 +
            '", _txt_IMPACC4_DR_Cur_Acc_Curr6: "' + _txt_IMPACC4_DR_Cur_Acc_Curr6 + '", _txt_IMPACC4_DR_Cur_Acc_amt6: "' + _txt_IMPACC4_DR_Cur_Acc_amt6 + '", _txt_IMPACC4_DR_Cur_Acc_payer6: "' + _txt_IMPACC4_DR_Cur_Acc_payer6 +

            '", _txt_IMPACC4_DR_Code2: "' + _txt_IMPACC4_DR_Code2 + '", _txt_IMPACC4_DR_AC_Short_Name2: "' + _txt_IMPACC4_DR_AC_Short_Name2 + '", _txt_IMPACC4_DR_Cust_abbr2: "' + _txt_IMPACC4_DR_Cust_abbr2 + '", _txt_IMPACC4_DR_Cust_Acc2: "' + _txt_IMPACC4_DR_Cust_Acc2 +
            '", _txt_IMPACC4_DR_Code3: "' + _txt_IMPACC4_DR_Code3 + '", _txt_IMPACC4_DR_AC_Short_Name3: "' + _txt_IMPACC4_DR_AC_Short_Name3 + '", _txt_IMPACC4_DR_Cust_abbr3: "' + _txt_IMPACC4_DR_Cust_abbr3 + '", _txt_IMPACC4_DR_Cust_Acc3: "' + _txt_IMPACC4_DR_Cust_Acc3 +
            '", _txt_IMPACC4_DR_Code4: "' + _txt_IMPACC4_DR_Code4 + '", _txt_IMPACC4_DR_AC_Short_Name4: "' + _txt_IMPACC4_DR_AC_Short_Name4 + '", _txt_IMPACC4_DR_Cust_abbr4: "' + _txt_IMPACC4_DR_Cust_abbr4 + '", _txt_IMPACC4_DR_Cust_Acc4: "' + _txt_IMPACC4_DR_Cust_Acc4 +
            '", _txt_IMPACC4_DR_Code5: "' + _txt_IMPACC4_DR_Code5 + '", _txt_IMPACC4_DR_AC_Short_Name5: "' + _txt_IMPACC4_DR_AC_Short_Name5 + '", _txt_IMPACC4_DR_Cust_abbr5: "' + _txt_IMPACC4_DR_Cust_abbr5 + '", _txt_IMPACC4_DR_Cust_Acc5: "' + _txt_IMPACC4_DR_Cust_Acc5 +
            '", _txt_IMPACC4_DR_Code6: "' + _txt_IMPACC4_DR_Code6 + '", _txt_IMPACC4_DR_AC_Short_Name6: "' + _txt_IMPACC4_DR_AC_Short_Name6 + '", _txt_IMPACC4_DR_Cust_abbr6: "' + _txt_IMPACC4_DR_Cust_abbr6 + '", _txt_IMPACC4_DR_Cust_Acc6: "' + _txt_IMPACC4_DR_Cust_Acc6 +


            /////IMPORT ACCOUNTING 5////////////
            '", _chk_IMPACC5Flag: "' + _chk_IMPACC5Flag + '", _txt_IMPACC5_FCRefNo:"' + _txt_IMPACC5_FCRefNo +
            '", _txt_IMPACC5_DiscAmt: "' + _txt_IMPACC5_DiscAmt + '", _txt_IMPACC5_DiscExchRate: "' + _txt_IMPACC5_DiscExchRate +
            '", _txt_IMPACC5_Princ_matu: "' + _txt_IMPACC5_Princ_matu + '", _txt_IMPACC5_Princ_lump: "' + _txt_IMPACC5_Princ_lump + '", _txt_IMPACC5_Princ_Contract_no: "' + _txt_IMPACC5_Princ_Contract_no + '", _txt_IMPACC5_Princ_Ex_Curr: "' + _txt_IMPACC5_Princ_Ex_Curr + '", _txt_IMPACC5_Princ_Ex_rate: "' + _txt_IMPACC5_Princ_Ex_rate + '", _txt_IMPACC5_Princ_Intnl_Ex_rate: "' + _txt_IMPACC5_Princ_Intnl_Ex_rate +
            '", _txt_IMPACC5_Interest_matu: "' + _txt_IMPACC5_Interest_matu + '", _txt_IMPACC5_Interest_lump: "' + _txt_IMPACC5_Interest_lump + '", _txt_IMPACC5_Interest_Contract_no:"' + _txt_IMPACC5_Interest_Contract_no + '", _txt_IMPACC5_Interest_Ex_Curr: "' + _txt_IMPACC5_Interest_Ex_Curr + '", _txt_IMPACC5_Interest_Ex_rate:"' + _txt_IMPACC5_Interest_Ex_rate + '", _txt_IMPACC5_Interest_Intnl_Ex_rate:"' + _txt_IMPACC5_Interest_Intnl_Ex_rate +
            '", _txt_IMPACC5_Commission_matu: "' + _txt_IMPACC5_Commission_matu + '", _txt_IMPACC5_Commission_lump:"' + _txt_IMPACC5_Commission_lump + '", _txt_IMPACC5_Commission_Contract_no:"' + _txt_IMPACC5_Commission_Contract_no + '", _txt_IMPACC5_Commission_Ex_Curr:"' + _txt_IMPACC5_Commission_Ex_Curr + '", _txt_IMPACC5_Commission_Ex_rate:"' + _txt_IMPACC5_Commission_Ex_rate + '", _txt_IMPACC5_Commission_Intnl_Ex_rate: "' + _txt_IMPACC5_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC5_Their_Commission_matu:"' + _txt_IMPACC5_Their_Commission_matu + '", _txt_IMPACC5_Their_Commission_lump:"' + _txt_IMPACC5_Their_Commission_lump + '", _txt_IMPACC5_Their_Commission_Contract_no: "' + _txt_IMPACC5_Their_Commission_Contract_no + '", _txt_IMPACC5_Their_Commission_Ex_Curr:"' + _txt_IMPACC5_Their_Commission_Ex_Curr + '", _txt_IMPACC5_Their_Commission_Ex_rate:"' + _txt_IMPACC5_Their_Commission_Ex_rate + '", _txt_IMPACC5_Their_Commission_Intnl_Ex_rate: "' + _txt_IMPACC5_Their_Commission_Intnl_Ex_rate +
            '", _txt_IMPACC5_CR_Code:"' + _txt_IMPACC5_CR_Code + '", _txt_IMPACC5_CR_AC_Short_Name:"' + _txt_IMPACC5_CR_AC_Short_Name + '", _txt_IMPACC5_CR_Cust_abbr: "' + _txt_IMPACC5_CR_Cust_abbr + '", _txt_IMPACC5_CR_Cust_Acc:"' + _txt_IMPACC5_CR_Cust_Acc + '", _txt_IMPACC5_CR_Acceptance_Curr:"' + _txt_IMPACC5_CR_Acceptance_Curr + '", _txt_IMPACC5_CR_Acceptance_amt: "' + _txt_IMPACC5_CR_Acceptance_amt + '", _txt_IMPACC5_CR_Acceptance_payer:"' + _txt_IMPACC5_CR_Acceptance_payer +
            '", _txt_IMPACC5_CR_Interest_Curr :"' + _txt_IMPACC5_CR_Interest_Curr + '", _txt_IMPACC5_CR_Interest_amt: "' + _txt_IMPACC5_CR_Interest_amt + '", _txt_IMPACC5_CR_Interest_payer: "' + _txt_IMPACC5_CR_Interest_payer +
            '", _txt_IMPACC5_CR_Accept_Commission_Curr: "' + _txt_IMPACC5_CR_Accept_Commission_Curr + '", _txt_IMPACC5_CR_Accept_Commission_amt:"' + _txt_IMPACC5_CR_Accept_Commission_amt + '", _txt_IMPACC5_CR_Accept_Commission_Payer: "' + _txt_IMPACC5_CR_Accept_Commission_Payer +
            '", _txt_IMPACC5_CR_Pay_Handle_Commission_Curr: "' + _txt_IMPACC5_CR_Pay_Handle_Commission_Curr + '", _txt_IMPACC5_CR_Pay_Handle_Commission_amt: "' + _txt_IMPACC5_CR_Pay_Handle_Commission_amt + '", _txt_IMPACC5_CR_Pay_Handle_Commission_Payer: "' + _txt_IMPACC5_CR_Pay_Handle_Commission_Payer +
            '", _txt_IMPACC5_CR_Others_Curr: "' + _txt_IMPACC5_CR_Others_Curr + '", _txt_IMPACC5_CR_Others_amt: "' + _txt_IMPACC5_CR_Others_amt + '", _txt_IMPACC5_CR_Others_Payer: "' + _txt_IMPACC5_CR_Others_Payer +
            '", _txt_IMPACC5_CR_Their_Commission_Curr: "' + _txt_IMPACC5_CR_Their_Commission_Curr + '", _txt_IMPACC5_CR_Their_Commission_amt: "' + _txt_IMPACC5_CR_Their_Commission_amt + '", _txt_IMPACC5_CR_Their_Commission_Payer:"' + _txt_IMPACC5_CR_Their_Commission_Payer +

            '", _txt_IMPACC5_DR_Code: "' + _txt_IMPACC5_DR_Code + '", _txt_IMPACC5_DR_AC_Short_Name: "' + _txt_IMPACC5_DR_AC_Short_Name + '", _txt_IMPACC5_DR_Cust_abbr: "' + _txt_IMPACC5_DR_Cust_abbr + '", _txt_IMPACC5_DR_Cust_Acc: "' + _txt_IMPACC5_DR_Cust_Acc +
            '", _txt_IMPACC5_DR_Cur_Acc_Curr: "' + _txt_IMPACC5_DR_Cur_Acc_Curr + '", _txt_IMPACC5_DR_Cur_Acc_amt: "' + _txt_IMPACC5_DR_Cur_Acc_amt + '", _txt_IMPACC5_DR_Cur_Acc_payer : "' + _txt_IMPACC5_DR_Cur_Acc_payer +
            '", _txt_IMPACC5_DR_Cur_Acc_Curr2: "' + _txt_IMPACC5_DR_Cur_Acc_Curr2 + '", _txt_IMPACC5_DR_Cur_Acc_amt2: "' + _txt_IMPACC5_DR_Cur_Acc_amt2 + '", _txt_IMPACC5_DR_Cur_Acc_payer2: "' + _txt_IMPACC5_DR_Cur_Acc_payer2 +
            '", _txt_IMPACC5_DR_Cur_Acc_Curr3: "' + _txt_IMPACC5_DR_Cur_Acc_Curr3 + '", _txt_IMPACC5_DR_Cur_Acc_amt3: "' + _txt_IMPACC5_DR_Cur_Acc_amt3 + '", _txt_IMPACC5_DR_Cur_Acc_payer3: "' + _txt_IMPACC5_DR_Cur_Acc_payer3 +

            '", _txt_IMPACC5_DR_Cur_Acc_Curr4: "' + _txt_IMPACC5_DR_Cur_Acc_Curr4 + '", _txt_IMPACC5_DR_Cur_Acc_amt4: "' + _txt_IMPACC5_DR_Cur_Acc_amt4 + '", _txt_IMPACC5_DR_Cur_Acc_payer4: "' + _txt_IMPACC5_DR_Cur_Acc_payer4 +
            '", _txt_IMPACC5_DR_Cur_Acc_Curr5: "' + _txt_IMPACC5_DR_Cur_Acc_Curr5 + '", _txt_IMPACC5_DR_Cur_Acc_amt5: "' + _txt_IMPACC5_DR_Cur_Acc_amt5 + '", _txt_IMPACC5_DR_Cur_Acc_payer5: "' + _txt_IMPACC5_DR_Cur_Acc_payer5 +
            '", _txt_IMPACC5_DR_Cur_Acc_Curr6: "' + _txt_IMPACC5_DR_Cur_Acc_Curr6 + '", _txt_IMPACC5_DR_Cur_Acc_amt6: "' + _txt_IMPACC5_DR_Cur_Acc_amt6 + '", _txt_IMPACC5_DR_Cur_Acc_payer6: "' + _txt_IMPACC5_DR_Cur_Acc_payer6 +

            '", _txt_IMPACC5_DR_Code2: "' + _txt_IMPACC5_DR_Code2 + '", _txt_IMPACC5_DR_AC_Short_Name2: "' + _txt_IMPACC5_DR_AC_Short_Name2 + '", _txt_IMPACC5_DR_Cust_abbr2: "' + _txt_IMPACC5_DR_Cust_abbr2 + '", _txt_IMPACC5_DR_Cust_Acc2: "' + _txt_IMPACC5_DR_Cust_Acc2 +
            '", _txt_IMPACC5_DR_Code3: "' + _txt_IMPACC5_DR_Code3 + '", _txt_IMPACC5_DR_AC_Short_Name3: "' + _txt_IMPACC5_DR_AC_Short_Name3 + '", _txt_IMPACC5_DR_Cust_abbr3: "' + _txt_IMPACC5_DR_Cust_abbr3 + '", _txt_IMPACC5_DR_Cust_Acc3: "' + _txt_IMPACC5_DR_Cust_Acc3 +
            '", _txt_IMPACC5_DR_Code4: "' + _txt_IMPACC5_DR_Code4 + '", _txt_IMPACC5_DR_AC_Short_Name4: "' + _txt_IMPACC5_DR_AC_Short_Name4 + '", _txt_IMPACC5_DR_Cust_abbr4: "' + _txt_IMPACC5_DR_Cust_abbr4 + '", _txt_IMPACC5_DR_Cust_Acc4: "' + _txt_IMPACC5_DR_Cust_Acc4 +
            '", _txt_IMPACC5_DR_Code5: "' + _txt_IMPACC5_DR_Code5 + '", _txt_IMPACC5_DR_AC_Short_Name5: "' + _txt_IMPACC5_DR_AC_Short_Name5 + '", _txt_IMPACC5_DR_Cust_abbr5: "' + _txt_IMPACC5_DR_Cust_abbr5 + '", _txt_IMPACC5_DR_Cust_Acc5: "' + _txt_IMPACC5_DR_Cust_Acc5 +
            '", _txt_IMPACC5_DR_Code6: "' + _txt_IMPACC5_DR_Code6 + '", _txt_IMPACC5_DR_AC_Short_Name6: "' + _txt_IMPACC5_DR_AC_Short_Name6 + '", _txt_IMPACC5_DR_Cust_abbr6: "' + _txt_IMPACC5_DR_Cust_abbr6 + '", _txt_IMPACC5_DR_Cust_Acc6: "' + _txt_IMPACC5_DR_Cust_Acc6 +

            ///////// GENERAL OPRATOIN 1 /////////////
            '", _chk_GO1Flag: "' + _chk_GO1Flag +
            '", _txt_GO1_Left_Comment: "' + _txt_GO1_Left_Comment +
            '", _txt_GO1_Left_SectionNo: "' + _txt_GO1_Left_SectionNo + '", _txt_GO1_Left_Remarks: "' + _txt_GO1_Left_Remarks + '", _txt_GO1_Left_Memo: "' + _txt_GO1_Left_Memo +
            '", _txt_GO1_Left_Scheme_no: "' + _txt_GO1_Left_Scheme_no +
            '", _txt_GO1_Left_Debit_Code: "' + _txt_GO1_Left_Debit_Code + '", _txt_GO1_Left_Debit_Curr: "' + _txt_GO1_Left_Debit_Curr + '", _txt_GO1_Left_Debit_Amt: "' + _txt_GO1_Left_Debit_Amt +
            '", _txt_GO1_Left_Debit_Cust: "' + _txt_GO1_Left_Debit_Cust + '", _txt_GO1_Left_Debit_Cust_Name: "' + _txt_GO1_Left_Debit_Cust_Name +
            '", _txt_GO1_Left_Debit_Cust_AcCode: "' + _txt_GO1_Left_Debit_Cust_AcCode + '", _txt_GO1_Left_Debit_Cust_AcCode_Name: "' + _txt_GO1_Left_Debit_Cust_AcCode_Name + '", _txt_GO1_Left_Debit_Cust_AccNo: "' + _txt_GO1_Left_Debit_Cust_AccNo +
            '", _txt_GO1_Left_Debit_Exch_Rate: "' + _txt_GO1_Left_Debit_Exch_Rate + '", _txt_GO1_Left_Debit_Exch_CCY: "' + _txt_GO1_Left_Debit_Exch_CCY +
            '", _txt_GO1_Left_Debit_FUND: "' + _txt_GO1_Left_Debit_FUND + '", _txt_GO1_Left_Debit_Check_No: "' + _txt_GO1_Left_Debit_Check_No + '", _txt_GO1_Left_Debit_Available:"' + _txt_GO1_Left_Debit_Available +
            '", _txt_GO1_Left_Debit_AdPrint:"' + _txt_GO1_Left_Debit_AdPrint + '", _txt_GO1_Left_Debit_Details: "' + _txt_GO1_Left_Debit_Details + '", _txt_GO1_Left_Debit_Entity: "' + _txt_GO1_Left_Debit_Entity +
            '", _txt_GO1_Left_Debit_Division: "' + _txt_GO1_Left_Debit_Division + '", _txt_GO1_Left_Debit_Inter_Amount: "' + _txt_GO1_Left_Debit_Inter_Amount + '", _txt_GO1_Left_Debit_Inter_Rate: "' + _txt_GO1_Left_Debit_Inter_Rate +
            '", _txt_GO1_Left_Credit_Code: "' + _txt_GO1_Left_Credit_Code + '", _txt_GO1_Left_Credit_Curr: "' + _txt_GO1_Left_Credit_Curr + '", _txt_GO1_Left_Credit_Amt: "' + _txt_GO1_Left_Credit_Amt +
            '", _txt_GO1_Left_Credit_Cust: "' + _txt_GO1_Left_Credit_Cust + '", _txt_GO1_Left_Credit_Cust_Name: "' + _txt_GO1_Left_Credit_Cust_Name +
            '", _txt_GO1_Left_Credit_Cust_AcCode: "' + _txt_GO1_Left_Credit_Cust_AcCode + '", _txt_GO1_Left_Credit_Cust_AcCode_Name: "' + _txt_GO1_Left_Credit_Cust_AcCode_Name + '", _txt_GO1_Left_Credit_Cust_AccNo: "' + _txt_GO1_Left_Credit_Cust_AccNo +
            '", _txt_GO1_Left_Credit_Exch_Rate: "' + _txt_GO1_Left_Credit_Exch_Rate + '", _txt_GO1_Left_Credit_Exch_Curr: "' + _txt_GO1_Left_Credit_Exch_Curr +
            '", _txt_GO1_Left_Credit_FUND: "' + _txt_GO1_Left_Credit_FUND + '", _txt_GO1_Left_Credit_Check_No: "' + _txt_GO1_Left_Credit_Check_No + '", _txt_GO1_Left_Credit_Available: "' + _txt_GO1_Left_Credit_Available +
            '", _txt_GO1_Left_Credit_AdPrint: "' + _txt_GO1_Left_Credit_AdPrint + '", _txt_GO1_Left_Credit_Details: "' + _txt_GO1_Left_Credit_Details + '", _txt_GO1_Left_Credit_Entity: "' + _txt_GO1_Left_Credit_Entity +
            '", _txt_GO1_Left_Credit_Division: "' + _txt_GO1_Left_Credit_Division + '", _txt_GO1_Left_Credit_Inter_Amount: "' + _txt_GO1_Left_Credit_Inter_Amount + '", _txt_GO1_Left_Credit_Inter_Rate: "' + _txt_GO1_Left_Credit_Inter_Rate +
            '", _txt_GO1_Right_Comment: "' + _txt_GO1_Right_Comment +
            '", _txt_GO1_Right_SectionNo: "' + _txt_GO1_Right_SectionNo + '", _txt_GO1_Right_Remarks: "' + _txt_GO1_Right_Remarks + '", _txt_GO1_Right_Memo: "' + _txt_GO1_Right_Memo +
            '", _txt_GO1_Right_Scheme_no: "' + _txt_GO1_Right_Scheme_no +
            '", _txt_GO1_Right_Debit_Code: "' + _txt_GO1_Right_Debit_Code + '", _txt_GO1_Right_Debit_Curr: "' + _txt_GO1_Right_Debit_Curr + '", _txt_GO1_Right_Debit_Amt: "' + _txt_GO1_Right_Debit_Amt +
            '", _txt_GO1_Right_Debit_Cust: "' + _txt_GO1_Right_Debit_Cust + '", _txt_GO1_Right_Debit_Cust_Name: "' + _txt_GO1_Right_Debit_Cust_Name +
            '", _txt_GO1_Right_Debit_Cust_AcCode: "' + _txt_GO1_Right_Debit_Cust_AcCode + '", _txt_GO1_Right_Debit_Cust_AcCode_Name: "' + _txt_GO1_Right_Debit_Cust_AcCode_Name + '", _txt_GO1_Right_Debit_Cust_AccNo: "' + _txt_GO1_Right_Debit_Cust_AccNo +
            '", _txt_GO1_Right_Debit_Exch_Rate: "' + _txt_GO1_Right_Debit_Exch_Rate + '", _txt_GO1_Right_Debit_Exch_CCY: "' + _txt_GO1_Right_Debit_Exch_CCY +
            '", _txt_GO1_Right_Debit_FUND: "' + _txt_GO1_Right_Debit_FUND + '", _txt_GO1_Right_Debit_Check_No: "' + _txt_GO1_Right_Debit_Check_No + '", _txt_GO1_Right_Debit_Available:"' + _txt_GO1_Right_Debit_Available +
            '", _txt_GO1_Right_Debit_AdPrint:"' + _txt_GO1_Right_Debit_AdPrint + '", _txt_GO1_Right_Debit_Details: "' + _txt_GO1_Right_Debit_Details + '", _txt_GO1_Right_Debit_Entity: "' + _txt_GO1_Right_Debit_Entity +
            '", _txt_GO1_Right_Debit_Division: "' + _txt_GO1_Right_Debit_Division + '", _txt_GO1_Right_Debit_Inter_Amount: "' + _txt_GO1_Right_Debit_Inter_Amount + '", _txt_GO1_Right_Debit_Inter_Rate: "' + _txt_GO1_Right_Debit_Inter_Rate +
            '", _txt_GO1_Right_Credit_Code: "' + _txt_GO1_Right_Credit_Code + '", _txt_GO1_Right_Credit_Curr: "' + _txt_GO1_Right_Credit_Curr + '", _txt_GO1_Right_Credit_Amt: "' + _txt_GO1_Right_Credit_Amt +
            '", _txt_GO1_Right_Credit_Cust: "' + _txt_GO1_Right_Credit_Cust + '", _txt_GO1_Right_Credit_Cust_Name: "' + _txt_GO1_Right_Credit_Cust_Name +
            '", _txt_GO1_Right_Credit_Cust_AcCode: "' + _txt_GO1_Right_Credit_Cust_AcCode + '", _txt_GO1_Right_Credit_Cust_AcCode_Name: "' + _txt_GO1_Right_Credit_Cust_AcCode_Name + '", _txt_GO1_Right_Credit_Cust_AccNo: "' + _txt_GO1_Right_Credit_Cust_AccNo +
            '", _txt_GO1_Right_Credit_Exch_Rate: "' + _txt_GO1_Right_Credit_Exch_Rate + '", _txt_GO1_Right_Credit_Exch_Curr: "' + _txt_GO1_Right_Credit_Exch_Curr +
            '", _txt_GO1_Right_Credit_FUND: "' + _txt_GO1_Right_Credit_FUND + '", _txt_GO1_Right_Credit_Check_No: "' + _txt_GO1_Right_Credit_Check_No + '", _txt_GO1_Right_Credit_Available: "' + _txt_GO1_Right_Credit_Available +
            '", _txt_GO1_Right_Credit_AdPrint: "' + _txt_GO1_Right_Credit_AdPrint + '", _txt_GO1_Right_Credit_Details: "' + _txt_GO1_Right_Credit_Details + '", _txt_GO1_Right_Credit_Entity: "' + _txt_GO1_Right_Credit_Entity +
            '", _txt_GO1_Right_Credit_Division: "' + _txt_GO1_Right_Credit_Division + '", _txt_GO1_Right_Credit_Inter_Amount: "' + _txt_GO1_Right_Credit_Inter_Amount + '", _txt_GO1_Right_Credit_Inter_Rate: "' + _txt_GO1_Right_Credit_Inter_Rate +

            ///////////////// GENERAL OPRATOIN 2 ////////////////////////////
            '", _chk_GO2Flag: "' + _chk_GO2Flag +
            '", _txt_GO2_Left_Comment: "' + _txt_GO2_Left_Comment +
            '", _txt_GO2_Left_SectionNo: "' + _txt_GO2_Left_SectionNo + '", _txt_GO2_Left_Remarks: "' + _txt_GO2_Left_Remarks + '", _txt_GO2_Left_Memo: "' + _txt_GO2_Left_Memo +
            '", _txt_GO2_Left_Scheme_no: "' + _txt_GO2_Left_Scheme_no +
            '", _txt_GO2_Left_Debit_Code: "' + _txt_GO2_Left_Debit_Code + '", _txt_GO2_Left_Debit_Curr: "' + _txt_GO2_Left_Debit_Curr + '", _txt_GO2_Left_Debit_Amt: "' + _txt_GO2_Left_Debit_Amt +
            '", _txt_GO2_Left_Debit_Cust: "' + _txt_GO2_Left_Debit_Cust + '", _txt_GO2_Left_Debit_Cust_Name: "' + _txt_GO2_Left_Debit_Cust_Name +
            '", _txt_GO2_Left_Debit_Cust_AcCode: "' + _txt_GO2_Left_Debit_Cust_AcCode + '", _txt_GO2_Left_Debit_Cust_AcCode_Name: "' + _txt_GO2_Left_Debit_Cust_AcCode_Name + '", _txt_GO2_Left_Debit_Cust_AccNo: "' + _txt_GO2_Left_Debit_Cust_AccNo +
            '", _txt_GO2_Left_Debit_Exch_Rate: "' + _txt_GO2_Left_Debit_Exch_Rate + '", _txt_GO2_Left_Debit_Exch_CCY: "' + _txt_GO2_Left_Debit_Exch_CCY +
            '", _txt_GO2_Left_Debit_FUND: "' + _txt_GO2_Left_Debit_FUND + '", _txt_GO2_Left_Debit_Check_No: "' + _txt_GO2_Left_Debit_Check_No + '", _txt_GO2_Left_Debit_Available:"' + _txt_GO2_Left_Debit_Available +
            '", _txt_GO2_Left_Debit_AdPrint:"' + _txt_GO2_Left_Debit_AdPrint + '", _txt_GO2_Left_Debit_Details: "' + _txt_GO2_Left_Debit_Details + '", _txt_GO2_Left_Debit_Entity: "' + _txt_GO2_Left_Debit_Entity +
            '", _txt_GO2_Left_Debit_Division: "' + _txt_GO2_Left_Debit_Division + '", _txt_GO2_Left_Debit_Inter_Amount: "' + _txt_GO2_Left_Debit_Inter_Amount + '", _txt_GO2_Left_Debit_Inter_Rate: "' + _txt_GO2_Left_Debit_Inter_Rate +
            '", _txt_GO2_Left_Credit_Code: "' + _txt_GO2_Left_Credit_Code + '", _txt_GO2_Left_Credit_Curr: "' + _txt_GO2_Left_Credit_Curr + '", _txt_GO2_Left_Credit_Amt: "' + _txt_GO2_Left_Credit_Amt +
            '", _txt_GO2_Left_Credit_Cust: "' + _txt_GO2_Left_Credit_Cust + '", _txt_GO2_Left_Credit_Cust_Name: "' + _txt_GO2_Left_Credit_Cust_Name +
            '", _txt_GO2_Left_Credit_Cust_AcCode: "' + _txt_GO2_Left_Credit_Cust_AcCode + '", _txt_GO2_Left_Credit_Cust_AcCode_Name: "' + _txt_GO2_Left_Credit_Cust_AcCode_Name + '", _txt_GO2_Left_Credit_Cust_AccNo: "' + _txt_GO2_Left_Credit_Cust_AccNo +
            '", _txt_GO2_Left_Credit_Exch_Rate: "' + _txt_GO2_Left_Credit_Exch_Rate + '", _txt_GO2_Left_Credit_Exch_Curr: "' + _txt_GO2_Left_Credit_Exch_Curr +
            '", _txt_GO2_Left_Credit_FUND: "' + _txt_GO2_Left_Credit_FUND + '", _txt_GO2_Left_Credit_Check_No: "' + _txt_GO2_Left_Credit_Check_No + '", _txt_GO2_Left_Credit_Available: "' + _txt_GO2_Left_Credit_Available +
            '", _txt_GO2_Left_Credit_AdPrint: "' + _txt_GO2_Left_Credit_AdPrint + '", _txt_GO2_Left_Credit_Details: "' + _txt_GO2_Left_Credit_Details + '", _txt_GO2_Left_Credit_Entity: "' + _txt_GO2_Left_Credit_Entity +
            '", _txt_GO2_Left_Credit_Division: "' + _txt_GO2_Left_Credit_Division + '", _txt_GO2_Left_Credit_Inter_Amount: "' + _txt_GO2_Left_Credit_Inter_Amount + '", _txt_GO2_Left_Credit_Inter_Rate: "' + _txt_GO2_Left_Credit_Inter_Rate +
            '", _txt_GO2_Right_Comment: "' + _txt_GO2_Right_Comment +
            '", _txt_GO2_Right_SectionNo: "' + _txt_GO2_Right_SectionNo + '", _txt_GO2_Right_Remarks: "' + _txt_GO2_Right_Remarks + '", _txt_GO2_Right_Memo: "' + _txt_GO2_Right_Memo +
            '", _txt_GO2_Right_Scheme_no: "' + _txt_GO2_Right_Scheme_no +
            '", _txt_GO2_Right_Debit_Code: "' + _txt_GO2_Right_Debit_Code + '", _txt_GO2_Right_Debit_Curr: "' + _txt_GO2_Right_Debit_Curr + '", _txt_GO2_Right_Debit_Amt: "' + _txt_GO2_Right_Debit_Amt +
            '", _txt_GO2_Right_Debit_Cust: "' + _txt_GO2_Right_Debit_Cust + '", _txt_GO2_Right_Debit_Cust_Name: "' + _txt_GO2_Right_Debit_Cust_Name +
            '", _txt_GO2_Right_Debit_Cust_AcCode: "' + _txt_GO2_Right_Debit_Cust_AcCode + '", _txt_GO2_Right_Debit_Cust_AcCode_Name: "' + _txt_GO2_Right_Debit_Cust_AcCode_Name + '", _txt_GO2_Right_Debit_Cust_AccNo: "' + _txt_GO2_Right_Debit_Cust_AccNo +
            '", _txt_GO2_Right_Debit_Exch_Rate: "' + _txt_GO2_Right_Debit_Exch_Rate + '", _txt_GO2_Right_Debit_Exch_CCY: "' + _txt_GO2_Right_Debit_Exch_CCY +
            '", _txt_GO2_Right_Debit_FUND: "' + _txt_GO2_Right_Debit_FUND + '", _txt_GO2_Right_Debit_Check_No: "' + _txt_GO2_Right_Debit_Check_No + '", _txt_GO2_Right_Debit_Available:"' + _txt_GO2_Right_Debit_Available +
            '", _txt_GO2_Right_Debit_AdPrint:"' + _txt_GO2_Right_Debit_AdPrint + '", _txt_GO2_Right_Debit_Details: "' + _txt_GO2_Right_Debit_Details + '", _txt_GO2_Right_Debit_Entity: "' + _txt_GO2_Right_Debit_Entity +
            '", _txt_GO2_Right_Debit_Division: "' + _txt_GO2_Right_Debit_Division + '", _txt_GO2_Right_Debit_Inter_Amount: "' + _txt_GO2_Right_Debit_Inter_Amount + '", _txt_GO2_Right_Debit_Inter_Rate: "' + _txt_GO2_Right_Debit_Inter_Rate +
            '", _txt_GO2_Right_Credit_Code: "' + _txt_GO2_Right_Credit_Code + '", _txt_GO2_Right_Credit_Curr: "' + _txt_GO2_Right_Credit_Curr + '", _txt_GO2_Right_Credit_Amt: "' + _txt_GO2_Right_Credit_Amt +
            '", _txt_GO2_Right_Credit_Cust: "' + _txt_GO2_Right_Credit_Cust + '", _txt_GO2_Right_Credit_Cust_Name: "' + _txt_GO2_Right_Credit_Cust_Name +
            '", _txt_GO2_Right_Credit_Cust_AcCode: "' + _txt_GO2_Right_Credit_Cust_AcCode + '", _txt_GO2_Right_Credit_Cust_AcCode_Name: "' + _txt_GO2_Right_Credit_Cust_AcCode_Name + '", _txt_GO2_Right_Credit_Cust_AccNo: "' + _txt_GO2_Right_Credit_Cust_AccNo +
            '", _txt_GO2_Right_Credit_Exch_Rate: "' + _txt_GO2_Right_Credit_Exch_Rate + '", _txt_GO2_Right_Credit_Exch_Curr: "' + _txt_GO2_Right_Credit_Exch_Curr +
            '", _txt_GO2_Right_Credit_FUND: "' + _txt_GO2_Right_Credit_FUND + '", _txt_GO2_Right_Credit_Check_No: "' + _txt_GO2_Right_Credit_Check_No + '", _txt_GO2_Right_Credit_Available: "' + _txt_GO2_Right_Credit_Available +
            '", _txt_GO2_Right_Credit_AdPrint: "' + _txt_GO2_Right_Credit_AdPrint + '", _txt_GO2_Right_Credit_Details: "' + _txt_GO2_Right_Credit_Details + '", _txt_GO2_Right_Credit_Entity: "' + _txt_GO2_Right_Credit_Entity +
            '", _txt_GO2_Right_Credit_Division: "' + _txt_GO2_Right_Credit_Division + '", _txt_GO2_Right_Credit_Inter_Amount: "' + _txt_GO2_Right_Credit_Inter_Amount + '", _txt_GO2_Right_Credit_Inter_Rate: "' + _txt_GO2_Right_Credit_Inter_Rate +

            ///////////////// GENERAL OPRATOIN 3 ////////////////////////////
            '", _chk_GO3Flag: "' + _chk_GO3Flag +
            '", _txt_GO3_Left_Comment: "' + _txt_GO3_Left_Comment +
            '", _txt_GO3_Left_SectionNo: "' + _txt_GO3_Left_SectionNo + '", _txt_GO3_Left_Remarks: "' + _txt_GO3_Left_Remarks + '", _txt_GO3_Left_Memo: "' + _txt_GO3_Left_Memo +
            '", _txt_GO3_Left_Scheme_no: "' + _txt_GO3_Left_Scheme_no +
            '", _txt_GO3_Left_Debit_Code: "' + _txt_GO3_Left_Debit_Code + '", _txt_GO3_Left_Debit_Curr: "' + _txt_GO3_Left_Debit_Curr + '", _txt_GO3_Left_Debit_Amt: "' + _txt_GO3_Left_Debit_Amt +
            '", _txt_GO3_Left_Debit_Cust: "' + _txt_GO3_Left_Debit_Cust + '", _txt_GO3_Left_Debit_Cust_Name: "' + _txt_GO3_Left_Debit_Cust_Name +
            '", _txt_GO3_Left_Debit_Cust_AcCode: "' + _txt_GO3_Left_Debit_Cust_AcCode + '", _txt_GO3_Left_Debit_Cust_AcCode_Name: "' + _txt_GO3_Left_Debit_Cust_AcCode_Name + '", _txt_GO3_Left_Debit_Cust_AccNo: "' + _txt_GO3_Left_Debit_Cust_AccNo +
            '", _txt_GO3_Left_Debit_Exch_Rate: "' + _txt_GO3_Left_Debit_Exch_Rate + '", _txt_GO3_Left_Debit_Exch_CCY: "' + _txt_GO3_Left_Debit_Exch_CCY +
            '", _txt_GO3_Left_Debit_FUND: "' + _txt_GO3_Left_Debit_FUND + '", _txt_GO3_Left_Debit_Check_No: "' + _txt_GO3_Left_Debit_Check_No + '", _txt_GO3_Left_Debit_Available:"' + _txt_GO3_Left_Debit_Available +
            '", _txt_GO3_Left_Debit_AdPrint:"' + _txt_GO3_Left_Debit_AdPrint + '", _txt_GO3_Left_Debit_Details: "' + _txt_GO3_Left_Debit_Details + '", _txt_GO3_Left_Debit_Entity: "' + _txt_GO3_Left_Debit_Entity +
            '", _txt_GO3_Left_Debit_Division: "' + _txt_GO3_Left_Debit_Division + '", _txt_GO3_Left_Debit_Inter_Amount: "' + _txt_GO3_Left_Debit_Inter_Amount + '", _txt_GO3_Left_Debit_Inter_Rate: "' + _txt_GO3_Left_Debit_Inter_Rate +
            '", _txt_GO3_Left_Credit_Code: "' + _txt_GO3_Left_Credit_Code + '", _txt_GO3_Left_Credit_Curr: "' + _txt_GO3_Left_Credit_Curr + '", _txt_GO3_Left_Credit_Amt: "' + _txt_GO3_Left_Credit_Amt +
            '", _txt_GO3_Left_Credit_Cust: "' + _txt_GO3_Left_Credit_Cust + '", _txt_GO3_Left_Credit_Cust_Name: "' + _txt_GO3_Left_Credit_Cust_Name +
            '", _txt_GO3_Left_Credit_Cust_AcCode: "' + _txt_GO3_Left_Credit_Cust_AcCode + '", _txt_GO3_Left_Credit_Cust_AcCode_Name: "' + _txt_GO3_Left_Credit_Cust_AcCode_Name + '", _txt_GO3_Left_Credit_Cust_AccNo: "' + _txt_GO3_Left_Credit_Cust_AccNo +
            '", _txt_GO3_Left_Credit_Exch_Rate: "' + _txt_GO3_Left_Credit_Exch_Rate + '", _txt_GO3_Left_Credit_Exch_Curr: "' + _txt_GO3_Left_Credit_Exch_Curr +
            '", _txt_GO3_Left_Credit_FUND: "' + _txt_GO3_Left_Credit_FUND + '", _txt_GO3_Left_Credit_Check_No: "' + _txt_GO3_Left_Credit_Check_No + '", _txt_GO3_Left_Credit_Available: "' + _txt_GO3_Left_Credit_Available +
            '", _txt_GO3_Left_Credit_AdPrint: "' + _txt_GO3_Left_Credit_AdPrint + '", _txt_GO3_Left_Credit_Details: "' + _txt_GO3_Left_Credit_Details + '", _txt_GO3_Left_Credit_Entity: "' + _txt_GO3_Left_Credit_Entity +
            '", _txt_GO3_Left_Credit_Division: "' + _txt_GO3_Left_Credit_Division + '", _txt_GO3_Left_Credit_Inter_Amount: "' + _txt_GO3_Left_Credit_Inter_Amount + '", _txt_GO3_Left_Credit_Inter_Rate: "' + _txt_GO3_Left_Credit_Inter_Rate +
            '", _txt_GO3_Right_Comment: "' + _txt_GO3_Right_Comment +
            '", _txt_GO3_Right_SectionNo: "' + _txt_GO3_Right_SectionNo + '", _txt_GO3_Right_Remarks: "' + _txt_GO3_Right_Remarks + '", _txt_GO3_Right_Memo: "' + _txt_GO3_Right_Memo +
            '", _txt_GO3_Right_Scheme_no: "' + _txt_GO3_Right_Scheme_no +
            '", _txt_GO3_Right_Debit_Code: "' + _txt_GO3_Right_Debit_Code + '", _txt_GO3_Right_Debit_Curr: "' + _txt_GO3_Right_Debit_Curr + '", _txt_GO3_Right_Debit_Amt: "' + _txt_GO3_Right_Debit_Amt +
            '", _txt_GO3_Right_Debit_Cust: "' + _txt_GO3_Right_Debit_Cust + '", _txt_GO3_Right_Debit_Cust_Name: "' + _txt_GO3_Right_Debit_Cust_Name +
            '", _txt_GO3_Right_Debit_Cust_AcCode: "' + _txt_GO3_Right_Debit_Cust_AcCode + '", _txt_GO3_Right_Debit_Cust_AcCode_Name: "' + _txt_GO3_Right_Debit_Cust_AcCode_Name + '", _txt_GO3_Right_Debit_Cust_AccNo: "' + _txt_GO3_Right_Debit_Cust_AccNo +
            '", _txt_GO3_Right_Debit_Exch_Rate: "' + _txt_GO3_Right_Debit_Exch_Rate + '", _txt_GO3_Right_Debit_Exch_CCY: "' + _txt_GO3_Right_Debit_Exch_CCY +
            '", _txt_GO3_Right_Debit_FUND: "' + _txt_GO3_Right_Debit_FUND + '", _txt_GO3_Right_Debit_Check_No: "' + _txt_GO3_Right_Debit_Check_No + '", _txt_GO3_Right_Debit_Available:"' + _txt_GO3_Right_Debit_Available +
            '", _txt_GO3_Right_Debit_AdPrint:"' + _txt_GO3_Right_Debit_AdPrint + '", _txt_GO3_Right_Debit_Details: "' + _txt_GO3_Right_Debit_Details + '", _txt_GO3_Right_Debit_Entity: "' + _txt_GO3_Right_Debit_Entity +
            '", _txt_GO3_Right_Debit_Division: "' + _txt_GO3_Right_Debit_Division + '", _txt_GO3_Right_Debit_Inter_Amount: "' + _txt_GO3_Right_Debit_Inter_Amount + '", _txt_GO3_Right_Debit_Inter_Rate: "' + _txt_GO3_Right_Debit_Inter_Rate +
            '", _txt_GO3_Right_Credit_Code: "' + _txt_GO3_Right_Credit_Code + '", _txt_GO3_Right_Credit_Curr: "' + _txt_GO3_Right_Credit_Curr + '", _txt_GO3_Right_Credit_Amt: "' + _txt_GO3_Right_Credit_Amt +
            '", _txt_GO3_Right_Credit_Cust: "' + _txt_GO3_Right_Credit_Cust + '", _txt_GO3_Right_Credit_Cust_Name: "' + _txt_GO3_Right_Credit_Cust_Name +
            '", _txt_GO3_Right_Credit_Cust_AcCode: "' + _txt_GO3_Right_Credit_Cust_AcCode + '", _txt_GO3_Right_Credit_Cust_AcCode_Name: "' + _txt_GO3_Right_Credit_Cust_AcCode_Name + '", _txt_GO3_Right_Credit_Cust_AccNo: "' + _txt_GO3_Right_Credit_Cust_AccNo +
            '", _txt_GO3_Right_Credit_Exch_Rate: "' + _txt_GO3_Right_Credit_Exch_Rate + '", _txt_GO3_Right_Credit_Exch_Curr: "' + _txt_GO3_Right_Credit_Exch_Curr +
            '", _txt_GO3_Right_Credit_FUND: "' + _txt_GO3_Right_Credit_FUND + '", _txt_GO3_Right_Credit_Check_No: "' + _txt_GO3_Right_Credit_Check_No + '", _txt_GO3_Right_Credit_Available: "' + _txt_GO3_Right_Credit_Available +
            '", _txt_GO3_Right_Credit_AdPrint: "' + _txt_GO3_Right_Credit_AdPrint + '", _txt_GO3_Right_Credit_Details: "' + _txt_GO3_Right_Credit_Details + '", _txt_GO3_Right_Credit_Entity: "' + _txt_GO3_Right_Credit_Entity +
            '", _txt_GO3_Right_Credit_Division: "' + _txt_GO3_Right_Credit_Division + '", _txt_GO3_Right_Credit_Inter_Amount: "' + _txt_GO3_Right_Credit_Inter_Amount + '", _txt_GO3_Right_Credit_Inter_Rate: "' + _txt_GO3_Right_Credit_Inter_Rate +

            ///////////////// GENERAL OPRATOIN Branch Change ////////////////////////////
            '", _chk_GOAcccChangeFlag: "' + _chk_GOAcccChangeFlag +
            '", _txt_GOAccChange_Ref_No: "' + _txt_GOAccChange_Ref_No + '", _txt_GOAccChange_Comment: "' + _txt_GOAccChange_Comment + '", _txt_GOAccChange_SectionNo: "' + _txt_GOAccChange_SectionNo +
            '", _txt_GOAccChange_Remarks: "' + _txt_GOAccChange_Remarks + '", _txt_GOAccChange_Memo: "' + _txt_GOAccChange_Memo + '", _txt_GOAccChange_Scheme_no: "' + _txt_GOAccChange_Scheme_no + '", _txt_GOAccChange_Debit_Code: "' + _txt_GOAccChange_Debit_Code + '", _txt_GOAccChange_Debit_Curr: "' + _txt_GOAccChange_Debit_Curr +
            '", _txt_GOAccChange_Debit_Amt: "' + _txt_GOAccChange_Debit_Amt + '", _txt_GOAccChange_Debit_Cust: "' + _txt_GOAccChange_Debit_Cust + '", _txt_GOAccChange_Debit_Cust_Name: "' + _txt_GOAccChange_Debit_Cust_Name + '", _txt_GOAccChange_Debit_Cust_AcCode: "' + _txt_GOAccChange_Debit_Cust_AcCode + '", _txt_GOAccChange_Debit_Cust_AccNo: "' + _txt_GOAccChange_Debit_Cust_AccNo +
            '", _txt_GOAccChange_Debit_Cust_AcCode_Name: "' + _txt_GOAccChange_Debit_Cust_AcCode_Name + '", _txt_GOAccChange_Debit_Exch_Rate: "' + _txt_GOAccChange_Debit_Exch_Rate + '", _txt_GOAccChange_Debit_Exch_CCY: "' + _txt_GOAccChange_Debit_Exch_CCY + '", _txt_GOAccChange_Debit_FUND: "' + _txt_GOAccChange_Debit_FUND + '", _txt_GOAccChange_Debit_Check_No: "' + _txt_GOAccChange_Debit_Check_No +
            '", _txt_GOAccChange_Debit_Available:"' + _txt_GOAccChange_Debit_Available + '",_txt_GOAccChange_Debit_AdPrint:"' + _txt_GOAccChange_Debit_AdPrint +
            '", _txt_GOAccChange_Debit_Details: "' + _txt_GOAccChange_Debit_Details + '", _txt_GOAccChange_Debit_Entity: "' + _txt_GOAccChange_Debit_Entity + '", _txt_GOAccChange_Debit_Division: "' + _txt_GOAccChange_Debit_Division + '", _txt_GOAccChange_Debit_Inter_Amount: "' + _txt_GOAccChange_Debit_Inter_Amount + '", _txt_GOAccChange_Debit_Inter_Rate: "' + _txt_GOAccChange_Debit_Inter_Rate +
            '", _txt_GOAccChange_Credit_Code: "' + _txt_GOAccChange_Credit_Code + '", _txt_GOAccChange_Credit_Curr: "' + _txt_GOAccChange_Credit_Curr + '", _txt_GOAccChange_Credit_Amt: "' + _txt_GOAccChange_Credit_Amt + '", _txt_GOAccChange_Credit_Cust: "' + _txt_GOAccChange_Credit_Cust + '", _txt_GOAccChange_Credit_Cust_Name: "' + _txt_GOAccChange_Credit_Cust_Name +
            '", _txt_GOAccChange_Credit_Cust_AcCode: "' + _txt_GOAccChange_Credit_Cust_AcCode + '", _txt_GOAccChange_Credit_Cust_AcCode_Name: "' + _txt_GOAccChange_Credit_Cust_AcCode_Name + '", _txt_GOAccChange_Credit_Cust_AccNo: "' + _txt_GOAccChange_Credit_Cust_AccNo + '", _txt_GOAccChange_Credit_Exch_Rate: "' + _txt_GOAccChange_Credit_Exch_Rate + '", _txt_GOAccChange_Credit_Exch_Curr: "' + _txt_GOAccChange_Credit_Exch_Curr +
            '", _txt_GOAccChange_Credit_FUND: "' + _txt_GOAccChange_Credit_FUND + '", _txt_GOAccChange_Credit_Check_No: "' + _txt_GOAccChange_Credit_Check_No + '", _txt_GOAccChange_Credit_Available: "' + _txt_GOAccChange_Credit_Available + '", _txt_GOAccChange_Credit_AdPrint: "' + _txt_GOAccChange_Credit_AdPrint + '", _txt_GOAccChange_Credit_Details: "' + _txt_GOAccChange_Credit_Details +
            '", _txt_GOAccChange_Credit_Entity: "' + _txt_GOAccChange_Credit_Entity + '", _txt_GOAccChange_Credit_Division: "' + _txt_GOAccChange_Credit_Division + '", _txt_GOAccChange_Credit_Inter_Amount: "' + _txt_GOAccChange_Credit_Inter_Amount + '", _txt_GOAccChange_Credit_Inter_Rate: "' + _txt_GOAccChange_Credit_Inter_Rate +

            /////////// Swift File
            '",_txt_MT103Receiver: "' + _txt_MT103Receiver +
            '",_txtInstructionCode: "' + _txtInstructionCode + '",_txtTransactionTypeCode: "' + _txtTransactionTypeCode +
            '",_txtVDate32: "' + _txtVDate32 + 
            //'",_txtCurrency32: "' + _txtCurrency32 + '",_txtAmount32: "' + _txtAmount32 +
            '",_txtCurrency: "' + _txtCurrency + '",_txtInstructedAmount: "' + _txtInstructedAmount +
            '",_txtExchangeRate: "' + _txtExchangeRate + '",_txtSendingInstitutionAccountNumber: "' + _txtSendingInstitutionAccountNumber +
            '",_txtSendingInstitutionSwiftCode: "' + _txtSendingInstitutionSwiftCode + '",_ddlOrderingInstitution: "' + _ddlOrderingInstitution +
            '",_txtOrderingInstitutionAccountNumber: "' + _txtOrderingInstitutionAccountNumber + '",_txtOrderingInstitutionSwiftCode: "' + _txtOrderingInstitutionSwiftCode +
            '",_txtOrderingInstitutionName: "' + _txtOrderingInstitutionName + '",_txtOrderingInstitutionAddress1: "' + _txtOrderingInstitutionAddress1 +
            '",_txtOrderingInstitutionAddress2: "' + _txtOrderingInstitutionAddress2 + '",_txtOrderingInstitutionAddress3: "' + _txtOrderingInstitutionAddress3 +
            '",_ddlSendersCorrespondent: "' + _ddlSendersCorrespondent + '",_txtSendersCorrespondentAccountNumber: "' + _txtSendersCorrespondentAccountNumber +
            '",_txtSendersCorrespondentSwiftCode: "' + _txtSendersCorrespondentSwiftCode + '",_txtSendersCorrespondentName: "' + _txtSendersCorrespondentName +
            '",_txtSendersCorrespondentLocation: "' + _txtSendersCorrespondentLocation + '",_txtSendersCorrespondentAddress1: "' + _txtSendersCorrespondentAddress1 +
            '",_txtSendersCorrespondentAddress2: "' + _txtSendersCorrespondentAddress2 + '",_txtSendersCorrespondentAddress3: "' + _txtSendersCorrespondentAddress3 +
            '",_ddlReceiversCorrespondent: "' + _ddlReceiversCorrespondent + '",_txtReceiversCorrespondentAccountNumber: "' + _txtReceiversCorrespondentAccountNumber +
            '",_txtReceiversCorrespondentSwiftCode: "' + _txtReceiversCorrespondentSwiftCode + '",_txtReceiversCorrespondentName: "' + _txtReceiversCorrespondentName +
            '",_txtReceiversCorrespondentLocation: "' + _txtReceiversCorrespondentLocation + '",_txtReceiversCorrespondentAddress1: "' + _txtReceiversCorrespondentAddress1 +
            '",_txtReceiversCorrespondentAddress2: "' + _txtReceiversCorrespondentAddress2 + '",_txtReceiversCorrespondentAddress3: "' + _txtReceiversCorrespondentAddress3 +
            '",_ddlThirdReimbursementInstitution: "' + _ddlThirdReimbursementInstitution + '",_txtThirdReimbursementInstitutionAccountNumber: "' + _txtThirdReimbursementInstitutionAccountNumber +
            '",_txtThirdReimbursementInstitutionSwiftCode: "' + _txtThirdReimbursementInstitutionSwiftCode + '",_txtThirdReimbursementInstitutionName: "' + _txtThirdReimbursementInstitutionName +
            '",_txtThirdReimbursementInstitutionLocation: "' + _txtThirdReimbursementInstitutionLocation + '",_txtThirdReimbursementInstitutionAddress1: "' + _txtThirdReimbursementInstitutionAddress1 +
            '",_txtThirdReimbursementInstitutionAddress2: "' + _txtThirdReimbursementInstitutionAddress2 + '",_txtThirdReimbursementInstitutionAddress3: "' + _txtThirdReimbursementInstitutionAddress3 +
            '",_txtBeneficiaryCustomerAccountNumber: "' + _txtBeneficiaryCustomerAccountNumber + '",_txtBeneficiaryCustomerSwiftCode: "' + _txtBeneficiaryCustomerSwiftCode +
            '",_txtDetailsOfCharges: "' + _txtDetailsOfCharges + '",_txtSenderCharges: "' + _txtSenderCharges +
            '",_txtSenderCharges2: "' + _txtSenderCharges2 + '",_txtReceiverCharges: "' + _txtReceiverCharges +
            '",_txtReceiverCharges2: "' + _txtReceiverCharges2 + '",_txtSendertoReceiverInformation: "' + _txtSendertoReceiverInformation +
            '",_txtSendertoReceiverInformation2: "' + _txtSendertoReceiverInformation2 + '",_txtSendertoReceiverInformation3: "' + _txtSendertoReceiverInformation3 +
            '",_txtSendertoReceiverInformation4: "' + _txtSendertoReceiverInformation4 + '",_txtSendertoReceiverInformation5: "' + _txtSendertoReceiverInformation5 +
            '",_txtSendertoReceiverInformation6: "' + _txtSendertoReceiverInformation6 + '",_txtRegulatoryReporting: "' + _txtRegulatoryReporting +
            '",_txtRegulatoryReporting2: "' + _txtRegulatoryReporting2 + '",_txtRegulatoryReporting3: "' + _txtRegulatoryReporting3 +

            '",_txtTimeIndication: "' + _txtTimeIndication + '",_ddlBankOperationCode: "' + _ddlBankOperationCode +
            '",_txtOrderingInstitution202AccountNumber: "' + _txtOrderingInstitution202AccountNumber +
            '",_ddlOrderingCustomer: "' + _ddlOrderingCustomer + '",_txtOrderingCustomer_Acc: "' + _txtOrderingCustomer_Acc + '",_txtOrderingCustomer_SwiftCode: "' + _txtOrderingCustomer_SwiftCode +
            '",_txtOrderingCustomer_Name: "' + _txtOrderingCustomer_Name +
            '",_txtOrderingCustomer_Addr1: "' + _txtOrderingCustomer_Addr1 + '",_txtOrderingCustomer_Addr2: "' + _txtOrderingCustomer_Addr2 + '",_txtOrderingCustomer_Addr3: "' + _txtOrderingCustomer_Addr3 +
            '",_ddlBeneficiaryCustomer: "' + _ddlBeneficiaryCustomer +
            '",_txtBeneficiaryCustomerAccountNumber: "' + _txtBeneficiaryCustomerAccountNumber +
            '",_txtBeneficiaryCustomerSwiftCode: "' + _txtBeneficiaryCustomerSwiftCode +
            '",_txtBeneficiaryCustomerName: "' + _txtBeneficiaryCustomerName +
            '",_txtBeneficiaryCustomerAddr1: "' + _txtBeneficiaryCustomerAddr1 + '",_txtBeneficiaryCustomerAddr2: "' + _txtBeneficiaryCustomerAddr2 + '",_txtBeneficiaryCustomerAddr3: "' + _txtBeneficiaryCustomerAddr3 +
            '",_txtRemittanceInformation1: "' + _txtRemittanceInformation1 + '",_txtRemittanceInformation2: "' + _txtRemittanceInformation2 +
            '",_txtRemittanceInformation3: "' + _txtRemittanceInformation3 + '",_txtRemittanceInformation4: "' + _txtRemittanceInformation4 +

            '",_txt202Amount: "' + _txt202Amount + '",_ddlOrderingInstitution202: "' + _ddlOrderingInstitution202 + '",_txtOrderingInstitution202AccountNumber: "' + _txtOrderingInstitution202AccountNumber +
            '",_txtOrderingInstitution202SwiftCode: "' + _txtOrderingInstitution202SwiftCode + '",_txtOrderingInstitution202Name: "' + _txtOrderingInstitution202Name +
            '",_txtOrderingInstitution202Address1: "' + _txtOrderingInstitution202Address1 + '",_txtOrderingInstitution202Address2: "' + _txtOrderingInstitution202Address2 +
            '",_txtOrderingInstitution202Address3: "' + _txtOrderingInstitution202Address3 + '",_ddlSendersCorrespondent202: "' + _ddlSendersCorrespondent202 +
            '",_txtSendersCorrespondent202AccountNumber: "' + _txtSendersCorrespondent202AccountNumber + '",_txtSendersCorrespondent202SwiftCode: "' + _txtSendersCorrespondent202SwiftCode +
            '",_txtSendersCorrespondent202Name: "' + _txtSendersCorrespondent202Name + '",_txtSendersCorrespondent202Location: "' + _txtSendersCorrespondent202Location +
            '",_txtSendersCorrespondent202Address1: "' + _txtSendersCorrespondent202Address1 + '",_txtSendersCorrespondent202Address2: "' + _txtSendersCorrespondent202Address2 +
            '",_txtSendersCorrespondent202Address3: "' + _txtSendersCorrespondent202Address3 + '",_ddlReceiversCorrespondent202: "' + _ddlReceiversCorrespondent202 +
            '",_txtReceiversCorrespondent202AccountNumber: "' + _txtReceiversCorrespondent202AccountNumber + '",_txtReceiversCorrespondent202SwiftCode: "' + _txtReceiversCorrespondent202SwiftCode +
            '",_txtReceiversCorrespondent202Name: "' + _txtReceiversCorrespondent202Name + '",_txtReceiversCorrespondent202Location: "' + _txtReceiversCorrespondent202Location +
            '",_txtReceiversCorrespondent202Address1: "' + _txtReceiversCorrespondent202Address1 + '",_txtReceiversCorrespondent202Address2: "' + _txtReceiversCorrespondent202Address2 +
            '",_txtReceiversCorrespondent202Address3: "' + _txtReceiversCorrespondent202Address3 +

            '",_rdb_swift_None: "' + _rdb_swift_None + '",_rdb_swift_103: "' + _rdb_swift_103 + '",_rdb_swift_202: "' + _rdb_swift_202 + '",_rdb_swift_200: "' + _rdb_swift_200 +

            // MT 200
            '",_txt200BicCode: "' + _txt200BicCode + '",_txt200TransactionRefNO: "' + _txt200TransactionRefNO + '",_txt200Date: "' + _txt200Date +
            '",_txt200Currency: "' + _txt200Currency + '",_txt200Amount: "' + _txt200Amount +
            '",_txt200SenderCorreCode: "' + _txt200SenderCorreCode + '",_txt200SenderCorreLocation: "' + _txt200SenderCorreLocation +
            '",_ddl200Intermediary: "' + _ddl200Intermediary + '",_txt200IntermediaryAccountNumber: "' + _txt200IntermediaryAccountNumber +
            '",_txt200IntermediarySwiftCode: "' + _txt200IntermediarySwiftCode + '",_txt200IntermediaryName: "' + _txt200IntermediaryName +
            '",_txt200IntermediaryAddress1: "' + _txt200IntermediaryAddress1 + '",_txt200IntermediaryAddress2: "' + _txt200IntermediaryAddress2 +
            '",_txt200IntermediaryAddress3: "' + _txt200IntermediaryAddress3 + '",_ddl200AccWithInstitution: "' + _ddl200AccWithInstitution +
            '",_txt200AccWithInstitutionAccountNumber: "' + _txt200AccWithInstitutionAccountNumber + '",_txt200AccWithInstitutionSwiftCode: "' + _txt200AccWithInstitutionSwiftCode +
            '",_txt200AccWithInstitutionLocation: "' + _txt200AccWithInstitutionLocation + '",_txt200AccWithInstitutionName: "' + _txt200AccWithInstitutionName +
            '",_txt200AccWithInstitutionAddress1: "' + _txt200AccWithInstitutionAddress1 + '",_txt200AccWithInstitutionAddress2: "' + _txt200AccWithInstitutionAddress2 +
            '",_txt200AccWithInstitutionAddress3: "' + _txt200AccWithInstitutionAddress3 +
            '",_txt200SendertoReceiverInformation1: "' + _txt200SendertoReceiverInformation1 + '",_txt200SendertoReceiverInformation2: "' + _txt200SendertoReceiverInformation2 +
            '",_txt200SendertoReceiverInformation3: "' + _txt200SendertoReceiverInformation3 + '",_txt200SendertoReceiverInformation4: "' + _txt200SendertoReceiverInformation4 +
            '",_txt200SendertoReceiverInformation5: "' + _txt200SendertoReceiverInformation5 + '",_txt200SendertoReceiverInformation6: "' + _txt200SendertoReceiverInformation6 +

            '",_txtTransactionRefNoR42: "' + _txtTransactionRefNoR42 + '",_txtRelatedReferenceR42: "' + _txtRelatedReferenceR42 +
            '",_txtValueDateR42: "' + _txtValueDateR42 + '",_txtCureencyR42: "' + _txtCureencyR42 +
            '",_txtAmountR42: "' + _txtAmountR42 + '",_txtOrderingInstitutionIFSCR42: "' + _txtOrderingInstitutionIFSCR42 +
            '",_txtBeneficiaryInstitutionIFSCR42: "' + _txtBeneficiaryInstitutionIFSCR42 +
            '",_txtCodeWordR42: "' + _txtCodeWordR42 + '",_txtAdditionalInformationR42: "' + _txtAdditionalInformationR42 +
            '",_txtMoreInfo1R42: "' + _txtMoreInfo1R42 + '",_txtMoreInfo2R42: "' + _txtMoreInfo2R42 +
            '",_txtMoreInfo3R42: "' + _txtMoreInfo3R42 + '",_txtMoreInfo4R42: "' + _txtMoreInfo4R42 +
            '",_txtMoreInfo5R42: "' + _txtMoreInfo5R42 + '",_rdb_swift_R42: "' + _rdb_swift_R42 +
            //---------------------------------------------------------------------------------Nilesh-----------------------------------------------------------------------------------
            //------------------------------------------------------MT754-------------------------------------------------------------
            '",MT754_Flag: "' + MT754_Flag + '", _txt_754_SenRef: "' + _txt_754_SenRef + '", _txt_754_RelRef: "' + _txt_754_RelRef +
            '", _ddlPrinAmtPaidAccNego_754: "' + _ddlPrinAmtPaidAccNego_754 + '", _txtPrinAmtPaidAccNegoDate_754 : "' + _txtPrinAmtPaidAccNegoDate_754 +
            '", _txtPrinAmtPaidAccNegoCurr_754 : "' + _txtPrinAmtPaidAccNegoCurr_754 + '", _txtPrinAmtPaidAccNegoAmt_754 : "' + _txtPrinAmtPaidAccNegoAmt_754 +
            '",_txt_754_AddAmtClamd_Ccy:"' + _txt_754_AddAmtClamd_Ccy + '",_txt_754_AddAmtClamd_Amt :"' + _txt_754_AddAmtClamd_Amt +
            '", _txt_MT754_Charges_Deducted: "' + _txt_MT754_Charges_Deducted + '", _txt_MT754_Charges_Deducted2: "' + _txt_MT754_Charges_Deducted2 + '", _txt_MT754_Charges_Deducted3: "' + _txt_MT754_Charges_Deducted3 + '", _txt_MT754_Charges_Deducted4: "' + _txt_MT754_Charges_Deducted4 +
            '", _txt_MT754_Charges_Deducted5 : "' + _txt_MT754_Charges_Deducted5 + '", _txt_MT754_Charges_Deducted6: "' + _txt_MT754_Charges_Deducted6 +
            '", _txt_MT754_Charges_Added: "' + _txt_MT754_Charges_Added + '", _txt_MT754_Charges_Added2: "' + _txt_MT754_Charges_Added2 +
            '", _txt_MT754_Charges_Added3: "' + _txt_MT754_Charges_Added3 + '", _txt_MT754_Charges_Added4: "' + _txt_MT754_Charges_Added4 + '", _txt_MT754_Charges_Added5 : "' + _txt_MT754_Charges_Added5 + '", _txt_MT754_Charges_Added6: "' + _txt_MT754_Charges_Added6 +
            '", _ddlTotalAmtclamd_754 : "' + _ddlTotalAmtclamd_754 + '", _txt_754_TotalAmtClmd_Date: "' + _txt_754_TotalAmtClmd_Date + '", _txt_754_TotalAmtClmd_Ccy: "' + _txt_754_TotalAmtClmd_Ccy + '", _txt_754_TotalAmtClmd_Amt: "' + _txt_754_TotalAmtClmd_Amt +
            '", _ddlReimbursingbank_754: "' + _ddlReimbursingbank_754 + '", _txtReimbursingBankAccountnumber_754: "' + _txtReimbursingBankAccountnumber_754 + '", _txtReimbursingBankpartyidentifier_754: "' + _txtReimbursingBankpartyidentifier_754 + '", _txtReimbursingBankIdentifiercode_754: "' + _txtReimbursingBankIdentifiercode_754 +
            '", _txtReimbursingBankLocation_754: "' + _txtReimbursingBankLocation_754 + '", _txtReimbursingBankName_754: "' + _txtReimbursingBankName_754 + '", _txtReimbursingBankAddress1_754: "' + _txtReimbursingBankAddress1_754 + '", _txtReimbursingBankAddress2_754: "' + _txtReimbursingBankAddress2_754 +
            '", _txtReimbursingBankAddress3_754: "' + _txtReimbursingBankAddress3_754 +
            '", _ddlAccountwithbank_754: "' + _ddlAccountwithbank_754 + '", _txtAccountwithBankAccountnumber_754: "' + _txtAccountwithBankAccountnumber_754 + '", _txtAccountwithBankpartyidentifier_754: "' + _txtAccountwithBankpartyidentifier_754 + '", _txtAccountwithBankIdentifiercode_754: "' + _txtAccountwithBankIdentifiercode_754 +
            '", _txtAccountwithBankLocation_754: "' + _txtAccountwithBankLocation_754 + '", _txtAccountwithBankName_754: "' + _txtAccountwithBankName_754 + '", _txtAccountwithBankAddress1_754: "' + _txtAccountwithBankAddress1_754 + '", _txtAccountwithBankAddress2_754: "' + _txtAccountwithBankAddress2_754 +
            '", _txtAccountwithBankAddress3_754: "' + _txtAccountwithBankAddress3_754 +
            '", _ddlBeneficiarybank_754: "' + _ddlBeneficiarybank_754 + '", _txtBeneficiaryBankAccountnumber_754: "' + _txtBeneficiaryBankAccountnumber_754 + '", _txtBeneficiarypartyidentifire : "' + _txtBeneficiarypartyidentifire + '", _txtBeneficiaryBankIdentifiercode_754: "' + _txtBeneficiaryBankIdentifiercode_754 +
            '", _txtBeneficiaryBankName_754: "' + _txtBeneficiaryBankName_754 + '", _txtBeneficiaryBankAddress1_754: "' + _txtBeneficiaryBankAddress1_754 + '", _txtBeneficiaryBankAddress2_754: "' + _txtBeneficiaryBankAddress2_754 + '", _txtBeneficiaryBankAddress3_754: "' + _txtBeneficiaryBankAddress3_754 +
            '", _txt_MT754_Sender_to_Receiver_Information: "' + _txt_MT754_Sender_to_Receiver_Information + '", _txt_MT754_Sender_to_Receiver_Information2: "' + _txt_MT754_Sender_to_Receiver_Information2 +
            '", _txt_MT754_Sender_to_Receiver_Information3: "' + _txt_MT754_Sender_to_Receiver_Information3 + '", _txt_MT754_Sender_to_Receiver_Information4: "' + _txt_MT754_Sender_to_Receiver_Information4 +
            '", _txt_MT754_Sender_to_Receiver_Information5: "' + _txt_MT754_Sender_to_Receiver_Information5 + '", _txt_MT754_Sender_to_Receiver_Information6: "' + _txt_MT754_Sender_to_Receiver_Information6 +
            '", _txt_Narrative_754_1: "' + _txt_Narrative_754_1 +
            '", _txt_Narrative_754_2: "' + _txt_Narrative_754_2 + '", _txt_Narrative_754_3: "' + _txt_Narrative_754_3 + '", _txt_Narrative_754_4: "' + _txt_Narrative_754_4 + '", _txt_Narrative_754_5: "' + _txt_Narrative_754_5 +
            '", _txt_Narrative_754_6: "' + _txt_Narrative_754_6 + '", _txt_Narrative_754_7: "' + _txt_Narrative_754_7 + '", _txt_Narrative_754_8: "' + _txt_Narrative_754_8 + '", _txt_Narrative_754_9: "' + _txt_Narrative_754_9 + '", _txt_Narrative_754_10: "' + _txt_Narrative_754_10 +
            '", _txt_Narrative_754_11: "' + _txt_Narrative_754_11 + '", _txt_Narrative_754_12: "' + _txt_Narrative_754_12 + '", _txt_Narrative_754_13: "' + _txt_Narrative_754_13 + '", _txt_Narrative_754_14: "' + _txt_Narrative_754_14 + '", _txt_Narrative_754_15: "' + _txt_Narrative_754_15 +
            '", _txt_Narrative_754_16: "' + _txt_Narrative_754_16 + '", _txt_Narrative_754_17: "' + _txt_Narrative_754_17 + '", _txt_Narrative_754_18: "' + _txt_Narrative_754_18 + '", _txt_Narrative_754_19: "' + _txt_Narrative_754_19 + '", _txt_Narrative_754_20: "' + _txt_Narrative_754_20 +
            '", _txt_Narrative_754_21: "' + _txt_Narrative_754_21 + '", _txt_Narrative_754_22: "' + _txt_Narrative_754_22 + '", _txt_Narrative_754_23: "' + _txt_Narrative_754_23 + '", _txt_Narrative_754_24: "' + _txt_Narrative_754_24 + '", _txt_Narrative_754_25: "' + _txt_Narrative_754_25 +
            '", _txt_Narrative_754_26: "' + _txt_Narrative_754_26 + '", _txt_Narrative_754_27: "' + _txt_Narrative_754_27 + '", _txt_Narrative_754_28: "' + _txt_Narrative_754_28 + '", _txt_Narrative_754_29: "' + _txt_Narrative_754_29 + '", _txt_Narrative_754_30: "' + _txt_Narrative_754_30 +
            '", _txt_Narrative_754_31: "' + _txt_Narrative_754_31 + '", _txt_Narrative_754_32: "' + _txt_Narrative_754_32 + '", _txt_Narrative_754_33: "' + _txt_Narrative_754_33 + '", _txt_Narrative_754_34: "' + _txt_Narrative_754_34 + '", _txt_Narrative_754_35: "' + _txt_Narrative_754_35 +

            //------------------------------------------------------------------------------------Nilesh END---------------------------------------------------------------------------------------

            //MT 202 changes 02122019
            '",_ddlIntermediary202: "' + _ddlIntermediary202 + '",_txtIntermediary202AccountNumber: "' + _txtIntermediary202AccountNumber +
            '",_txtIntermediary202SwiftCode: "' + _txtIntermediary202SwiftCode + '",_txtIntermediary202Name: "' + _txtIntermediary202Name +
            '",_txtIntermediary202Address1: "' + _txtIntermediary202Address1 + '",_txtIntermediary202Address2: "' + _txtIntermediary202Address2 +
            '",_txtIntermediary202Address3: "' + _txtIntermediary202Address3 + '",_ddlAccountWithInstitution202: "' + _ddlAccountWithInstitution202 +
            '",_txtAccountWithInstitution202AccountNumber: "' + _txtAccountWithInstitution202AccountNumber + '",_txtAccountWithInstitution202SwiftCode: "' + _txtAccountWithInstitution202SwiftCode +
            '",_txtAccountWithInstitution202Name: "' + _txtAccountWithInstitution202Name + '",_txtAccountWithInstitution202Location: "' + _txtAccountWithInstitution202Location +
            '",_txtAccountWithInstitution202Address1: "' + _txtAccountWithInstitution202Address1 + '",_txtAccountWithInstitution202Address2: "' + _txtAccountWithInstitution202Address2 +
            '",_txtAccountWithInstitution202Address3: "' + _txtAccountWithInstitution202Address3 + '",_ddlBeneficiaryInstitution202: "' + _ddlBeneficiaryInstitution202 +
            '",_txtBeneficiaryInstitution202AccountNumber: "' + _txtBeneficiaryInstitution202AccountNumber + '",_txtBeneficiaryInstitution202SwiftCode: "' + _txtBeneficiaryInstitution202SwiftCode +
            '",_txtBeneficiaryInstitution202Name: "' + _txtBeneficiaryInstitution202Name + '",_txtBeneficiaryInstitution202Address1: "' + _txtBeneficiaryInstitution202Address1 +
            '",_txtBeneficiaryInstitution202Address2: "' + _txtBeneficiaryInstitution202Address2 + '",_txtBeneficiaryInstitution202Address3: "' + _txtBeneficiaryInstitution202Address3 +
            '",_txtSenderToReceiverInformation2021: "' + _txtSenderToReceiverInformation2021 + '",_txtSenderToReceiverInformation2022: "' + _txtSenderToReceiverInformation2022 +
            '",_txtSenderToReceiverInformation2023: "' + _txtSenderToReceiverInformation2023 + '",_txtSenderToReceiverInformation2024: "' + _txtSenderToReceiverInformation2024 +
            '",_txtSenderToReceiverInformation2025: "' + _txtSenderToReceiverInformation2025 + '",_txtSenderToReceiverInformation2026: "' + _txtSenderToReceiverInformation2026 +
            //MT 202 changes 02122019
            //MT 103 changes 02122019
            '",_ddlIntermediary103: "' + _ddlIntermediary103 + '",_txtIntermediary103AccountNumber: "' + _txtIntermediary103AccountNumber +
            '",_txtIntermediary103SwiftCode: "' + _txtIntermediary103SwiftCode + '",_txtIntermediary103Name: "' + _txtIntermediary103Name +
            '",_txtIntermediary103Address1: "' + _txtIntermediary103Address1 + '",_txtIntermediary103Address2: "' + _txtIntermediary103Address2 +
            '",_txtIntermediary103Address3: "' + _txtIntermediary103Address3 + '",_ddlAccountWithInstitution103: "' + _ddlAccountWithInstitution103 +
            '",_txtAccountWithInstitution103AccountNumber: "' + _txtAccountWithInstitution103AccountNumber + '",_txtAccountWithInstitution103SwiftCode: "' + _txtAccountWithInstitution103SwiftCode +
            '",_txtAccountWithInstitution103Name: "' + _txtAccountWithInstitution103Name + '",_txtAccountWithInstitution103Location: "' + _txtAccountWithInstitution103Location +
            '",_txtAccountWithInstitution103Address1: "' + _txtAccountWithInstitution103Address1 + '",_txtAccountWithInstitution103Address2: "' + _txtAccountWithInstitution103Address2 +
            '",_txtAccountWithInstitution103Address3: "' + _txtAccountWithInstitution103Address3 +
            //MT 103 changes 02122019
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
function OnDocNextClick(index) {
    debugger;
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}
function ImportAccountingPrevClick() {
    SaveUpdateData();
    var _DocType = $("#hdnDocType").val();
    var tabContainer = $get('TabContainerMain');
    if (_DocType == 'ACC') {
        tabContainer.control.set_activeTabIndex(1);
    }
    else {
        tabContainer.control.set_activeTabIndex(0);
    }

    return false;
}
function ImportAccountingNextClick(index) {
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(2);
    var SubtabContainer = $get('TabContainerMain_tbDocumentAccounting_TabSubContainerACC');
    SubtabContainer.control.set_activeTabIndex(index);
    return false;
}
function GeneralOperationNextClick(index) {
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(3);
    var SubtabContainer = $get('TabContainerMain_tbDocumentGO_TabSubContainerGO');
    SubtabContainer.control.set_activeTabIndex(index);
    return false;
}
function GO3_NextClick() {
    SaveUpdateData();
    var _BranchName = $("#hdnBranchName").val();
    var tabContainer = $get('TabContainerMain');
    var SubtabContainer;

    if (_BranchName != 'Mumbai') {
        tabContainer.control.set_activeTabIndex(3);
        SubtabContainer = $get('TabContainerMain_tbDocumentGO_TabSubContainerGO');
        SubtabContainer.control.set_activeTabIndex(3);
    }
    else {
        tabContainer.control.set_activeTabIndex(4);
        SubtabContainer = $get('TabContainerMain_tbSwift_TabContainerSwift');
        SubtabContainer.control.set_activeTabIndex(0);
    }

    return false;
}
function SwiftPrevClick() {
    SaveUpdateData();
    var _BranchName = $("#hdnBranchName").val();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(3);
    var SubtabContainer = $get('TabContainerMain_tbDocumentGO_TabSubContainerGO');
    if (_BranchName != 'Mumbai') {

        SubtabContainer.control.set_activeTabIndex(3);
    }
    else {
        SubtabContainer.control.set_activeTabIndex(2);
    }

    return false;
}
function SwiftNextClick(index) {
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(4);
    var SubtabContainer = $get('TabContainerMain_tbSwift_TabContainerSwift');
    SubtabContainer.control.set_activeTabIndex(index);
    return false;
}
function OnBackClick() {
    SaveUpdateData();
    window.location.href = "TF_IMP_Settlement_Maker_View.aspx";
    return false;
}
function SubmitCheck() {
    {
        var _txt_DiscAmt1 = 0, _txt_DiscAmt2 = 0, _txt_DiscAmt3 = 0, _txt_DiscAmt4 = 0, _txt_DiscAmt5 = 0;
        var _lblBillAmt = $("#lblBillAmt").text();
        if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_chk_IMPACC1Flag").is(':checked')) {
            if ($.isNumeric($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DiscAmt").val())) {
                _txt_DiscAmt1 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DiscAmt").val();
            }
        }
        if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_chk_IMPACC2Flag").is(':checked')) {
            if ($.isNumeric($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DiscAmt").val())) {
                _txt_DiscAmt2 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DiscAmt").val();
            }
        }
        if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_chk_IMPACC3Flag").is(':checked')) {
            if ($.isNumeric($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DiscAmt").val())) {
                _txt_DiscAmt3 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DiscAmt").val();
            }
        }
        if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_chk_IMPACC4Flag").is(':checked')) {
            if ($.isNumeric($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DiscAmt").val())) {
                _txt_DiscAmt4 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DiscAmt").val();
            }
        }
        if ($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_chk_IMPACC5Flag").is(':checked')) {
            if ($.isNumeric($("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DiscAmt").val())) {
                _txt_DiscAmt5 = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DiscAmt").val();
            }
        }

        var total_IMP_ACC_Amt = parseFloat(_txt_DiscAmt1) + parseFloat(_txt_DiscAmt2) + parseFloat(_txt_DiscAmt3) + parseFloat(_txt_DiscAmt4) + parseFloat(_txt_DiscAmt5);
        if ($("#hdnLedgerStatusCode").val() != 'S') {
            //if (round(parseFloat(_lblBillAmt),0) != round(parseFloat(total_IMP_ACC_Amt),0)) {
            if (parseFloat(_lblBillAmt).toFixed(2) != parseFloat(total_IMP_ACC_Amt).toFixed(2)) {
                alert('Total Settlement Amount is not equal to Bill Amount. Please Check Import Accounting');
                return false;
            }
        }
    }
    {
        if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
            var SumDebit1 = 0, sumCredit1 = 0;
            var _txt_GO1_Left_Debit_Amt = 0, _txt_GO1_Left_Credit_Amt = 0, _txt_GO1_Right_Debit_Amt = 0, _txt_GO1_Right_Credit_Amt = 0;
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Amt").val())) {
                _txt_GO1_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Amt").val())) {
                _txt_GO1_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Amt").val())) {
                _txt_GO1_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Amt").val())) {
                _txt_GO1_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Amt").val();
            }

            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Code").val() == "D") {
                SumDebit1 = parseFloat(SumDebit1) + parseFloat(_txt_GO1_Left_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Code").val() == "D") {
                SumDebit1 = parseFloat(SumDebit1) + parseFloat(_txt_GO1_Left_Credit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Code").val() == "D") {
                SumDebit1 = parseFloat(SumDebit1) + parseFloat(_txt_GO1_Right_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Code").val() == "D") {
                SumDebit1 = parseFloat(SumDebit1) + parseFloat(_txt_GO1_Right_Credit_Amt);
            }

            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Code").val() == "C") {
                sumCredit1 = parseFloat(sumCredit1) + parseFloat(_txt_GO1_Left_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Code").val() == "C") {
                sumCredit1 = parseFloat(sumCredit1) + parseFloat(_txt_GO1_Left_Credit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Code").val() == "C") {
                sumCredit1 = parseFloat(sumCredit1) + parseFloat(_txt_GO1_Right_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Code").val() == "C") {
                sumCredit1 = parseFloat(sumCredit1) + parseFloat(_txt_GO1_Right_Credit_Amt);
            }
            //            if (parseFloat(SumDebit1) == 0) {
            //                alert('Total Debit Amount can not be balnk or zero for GENERAL OPERATION I.');
            //                return false;
            //            }
            //            if (parseFloat(sumCredit1) == 0) {
            //                alert('Total Credit Amount can not be balnk or zero for GENERAL OPERATION I.');
            //                return false;
            //            }
            //            if (parseFloat(SumDebit1) != parseFloat(sumCredit1)) {
            //                alert('Total Debit Amount and Credit Amount is not maching for GENERAL OPERATION I.');
            //                return false;
            //            }
        }
    }
    {
        if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_chk_GO2Flag").is(':checked')) {
            var SumDebit2 = 0, sumCredit2 = 0;
            var _txt_GO2_Left_Debit_Amt = 0, _txt_GO2_Left_Credit_Amt = 0, _txt_GO2_Right_Debit_Amt = 0, _txt_GO2_Right_Credit_Amt = 0;
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Amt").val())) {
                _txt_GO2_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Amt").val())) {
                _txt_GO2_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Amt").val())) {
                _txt_GO2_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Amt").val())) {
                _txt_GO2_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Amt").val();
            }

            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Code").val() == "D") {
                SumDebit2 = parseFloat(SumDebit2) + parseFloat(_txt_GO2_Left_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Code").val() == "D") {
                SumDebit2 = parseFloat(SumDebit2) + parseFloat(_txt_GO2_Left_Credit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Code").val() == "D") {
                SumDebit2 = parseFloat(SumDebit2) + parseFloat(_txt_GO2_Right_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Code").val() == "D") {
                SumDebit2 = parseFloat(SumDebit2) + parseFloat(_txt_GO2_Right_Credit_Amt);
            }

            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Code").val() == "C") {
                sumCredit2 = parseFloat(sumCredit2) + parseFloat(_txt_GO2_Left_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Code").val() == "C") {
                sumCredit2 = parseFloat(sumCredit2) + parseFloat(_txt_GO2_Left_Credit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Code").val() == "C") {
                sumCredit2 = parseFloat(sumCredit2) + parseFloat(_txt_GO2_Right_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Code").val() == "C") {
                sumCredit2 = parseFloat(sumCredit2) + parseFloat(_txt_GO2_Right_Credit_Amt);
            }
            //            if (parseFloat(SumDebit2) == 0) {
            //                alert('Total Debit Amount can not be balnk or zero for GENERAL OPERATION II.');
            //                return false;
            //            }
            //            if (parseFloat(sumCredit2) == 0) {
            //                alert('Total Credit Amount can not be balnk or zero for GENERAL OPERATION II.');
            //                return false;
            //            }
            //            if (parseFloat(SumDebit2) != parseFloat(sumCredit2)) {
            //                alert('Total Debit Amount and Credit Amount is not maching for GENERAL OPERATION II.');
            //                return false;
            //            }
        }
    }
    {
        if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_chk_GO3Flag").is(':checked')) {
            var SumDebit3 = 0, sumCredit3 = 0;
            var _txt_GO3_Left_Debit_Amt = 0, _txt_GO3_Left_Credit_Amt = 0, _txt_GO3_Right_Debit_Amt = 0, _txt_GO3_Right_Credit_Amt = 0;
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Amt").val())) {
                _txt_GO3_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Amt").val())) {
                _txt_GO3_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Amt").val())) {
                _txt_GO3_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Amt").val())) {
                _txt_GO3_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Amt").val();
            }

            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Code").val() == "D") {
                SumDebit3 = parseFloat(SumDebit3) + parseFloat(_txt_GO3_Left_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Code").val() == "D") {
                SumDebit3 = parseFloat(SumDebit3) + parseFloat(_txt_GO3_Left_Credit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Code").val() == "D") {
                SumDebit3 = parseFloat(SumDebit3) + parseFloat(_txt_GO3_Right_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Code").val() == "D") {
                SumDebit3 = parseFloat(SumDebit3) + parseFloat(_txt_GO3_Right_Credit_Amt);
            }

            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Code").val() == "C") {
                sumCredit3 = parseFloat(sumCredit3) + parseFloat(_txt_GO3_Left_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Code").val() == "C") {
                sumCredit3 = parseFloat(sumCredit3) + parseFloat(_txt_GO3_Left_Credit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Code").val() == "C") {
                sumCredit3 = parseFloat(sumCredit3) + parseFloat(_txt_GO3_Right_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Code").val() == "C") {
                sumCredit3 = parseFloat(sumCredit3) + parseFloat(_txt_GO3_Right_Credit_Amt);
            }
            //            if (parseFloat(SumDebit3) == 0) {
            //                alert('Total Debit Amount can not be balnk or zero for GENERAL OPERATION III.');
            //                return false;
            //            }
            //            if (parseFloat(sumCredit3) == 0) {
            //                alert('Total Credit Amount can not be balnk or zero for GENERAL OPERATION III.');
            //                return false;
            //            }
            //            if (parseFloat(SumDebit3) != parseFloat(sumCredit3)) {
            //                alert('Total Debit Amount and Credit Amount is not maching for GENERAL OPERATION III.');
            //                return false;
            //            }
        }
    }
    {
        if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_chk_GOAcccChangeFlag").is(':checked')) {
            var SumDebit4 = 0, sumCredit4 = 0, _txt_GOAccChange_Debit_Amt = 0, _txt_GOAccChange_Credit_Amt = 0;
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Amt").val())) {
                _txt_GOAccChange_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Amt").val();
            }
            if ($.isNumeric($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Amt").val())) {
                _txt_GOAccChange_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Amt").val();
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Code").val() == "D") {
                SumDebit4 = parseFloat(SumDebit4) + parseFloat(_txt_GOAccChange_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Code").val() == "D") {
                SumDebit4 = parseFloat(SumDebit4) + parseFloat(_txt_GOAccChange_Credit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Code").val() == "C") {
                sumCredit4 = parseFloat(sumCredit4) + parseFloat(_txt_GOAccChange_Debit_Amt);
            }
            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Code").val() == "C") {
                sumCredit4 = parseFloat(sumCredit4) + parseFloat(_txt_GOAccChange_Credit_Amt);
            }
            //            if (parseFloat(SumDebit4) == 0) {
            //                alert('Total Debit Amount can not be balnk or zero for INTER OFFICE.');
            //                return false;
            //            }
            //            if (parseFloat(sumCredit4) == 0) {
            //                alert('Total Credit Amount can not be balnk or zero for INTER OFFICE.');
            //                return false;
            //            }
            //            if (parseFloat(SumDebit4) != parseFloat(sumCredit4)) {
            //                alert('Total Debit Amount and Credit Amount is not maching for INTER OFFICE.');
            //                return false;
            //            }
        }
    }

    //-----------------------------------------------------Nilesh Start-------------------------------------------------------------------------
    //---------------------------------------------------------Validation for Swift R42-------------------------------------------------------------------------------------
    if ($("#TabContainerMain_tbSwift_rdb_swift_R42").is(':checked')) {

    }
    //---------------------------------------------------------END Validation for Swift R42-------------------------------------------------------------------------------------
    //---------------------------------------------------------Validation for MT103-------------------------------------------------------------------------------------

    var _txt_MT103Receiver = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt_MT103Receiver").val();
    var _lbldocurr = $("#lblDoc_Curr").text();
    var _txtInstructionCode1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtInstructionCode").val();
    var _txtInstructionCode = _txtInstructionCode1.toUpperCase();
    var _txtTransactionTypeCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtTransactionTypeCode").val();
    var _txtCurrency1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtCurrency").val();
    var _txtCurrency = _txtCurrency1.toUpperCase();
    var _txtInstructedAmount = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtInstructedAmount").val();
    var _txtExchangeRate = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtExchangeRate").val();
    var _txtSendingInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendingInstitutionAccountNumber").val();
    var _txtSendingInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendingInstitutionSwiftCode").val();

    var _ddlOrderingInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlOrderingInstitution").val();
    var _txtOrderingInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAccountNumber").val();
    var _txtOrderingInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionSwiftCode").val();
    var _txtOrderingInstitutionName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionName").val();
    var _txtOrderingInstitutionAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAddress1").val();
    var _txtOrderingInstitutionAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAddress2").val();
    var _txtOrderingInstitutionAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAddress3").val();

    var _ddlSendersCorrespondent = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlSendersCorrespondent").val();
    var _txtSendersCorrespondentAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").val();
    var _txtSendersCorrespondentSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentSwiftCode").val();
    var _txtSendersCorrespondentLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentLocation").val();
    var _txtSendersCorrespondentName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentName").val();
    var _txtSendersCorrespondentAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAddress1").val();
    var _txtSendersCorrespondentAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAddress2").val();
    var _txtSendersCorrespondentAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAddress3").val();

    var _ddlReceiversCorrespondent = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlReceiversCorrespondent").val();
    var _txtReceiversCorrespondentAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").val();
    var _txtReceiversCorrespondentSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentSwiftCode").val();
    var _txtReceiversCorrespondentLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentLocation").val();
    var _txtReceiversCorrespondentName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentName").val();
    var _txtReceiversCorrespondentAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAddress1").val();
    var _txtReceiversCorrespondentAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAddress2").val();
    var _txtReceiversCorrespondentAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAddress3").val();

    var _ddlThirdReimbursementInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlThirdReimbursementInstitution").val();
    var _txtThirdReimbursementInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAccountNumber").val();
    var _txtThirdReimbursementInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionSwiftCode").val();
    var _txtThirdReimbursementInstitutionLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionLocation").val();
    var _txtThirdReimbursementInstitutionName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionName").val();
    var _txtThirdReimbursementInstitutionAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAddress1").val();
    var _txtThirdReimbursementInstitutionAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAddress2").val();
    var _txtThirdReimbursementInstitutionAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAddress3").val();

    var _ddlIntermediary103 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlIntermediary103").val();
    var _txtIntermediary103AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103AccountNumber").val();
    var _txtIntermediary103SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103SwiftCode").val();
    var _txtIntermediary103Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Name").val();
    var _txtIntermediary103Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Address1").val();
    var _txtIntermediary103Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Address2").val();
    var _txtIntermediary103Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103Address3").val();

    var _ddlAccountWithInstitution103 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlAccountWithInstitution103").val();
    var _txtAccountWithInstitution103AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").val();
    var _txtAccountWithInstitution103SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103SwiftCode").val();
    var _txtAccountWithInstitution103Location = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Location").val();
    var _txtAccountWithInstitution103Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Name").val();
    var _txtAccountWithInstitution103Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Address1").val();
    var _txtAccountWithInstitution103Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Address2").val();
    var _txtAccountWithInstitution103Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103Address3").val();

    var _txtDetailsOfCharges1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtDetailsOfCharges").val();
    var _txtDetailsOfCharges = _txtDetailsOfCharges1.toUpperCase();
    var _txtSenderCharges = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges").val();
    var _txtSenderCharges2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges2").val();
    var _txtReceiverCharges1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges").val();
    var _txtReceiverCharges = _txtReceiverCharges1.toUpperCase();
    var _txtReceiverCharges2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges2").val();
    var _txtSendertoReceiverInformation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation").val();
    var _txtSendertoReceiverInformation2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation2").val();
    var _txtSendertoReceiverInformation3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation3").val();
    var _txtSendertoReceiverInformation4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation4").val();
    var _txtSendertoReceiverInformation5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation5").val();
    var _txtSendertoReceiverInformation6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendertoReceiverInformation6").val();
    var _txtRegulatoryReporting = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRegulatoryReporting").val();
    var _txtRegulatoryReporting2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRegulatoryReporting2").val();
    var _txtRegulatoryReporting3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtRegulatoryReporting3").val();

    var _txtThirdReimbursementInstitutionAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAccountNumber").val();
    var _txtThirdReimbursementInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionSwiftCode").val();
    var _txtIntermediary103AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103AccountNumber").val();
    var _txtIntermediary103SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103SwiftCode").val();

    //C1
    if ($("#TabContainerMain_tbSwift_rdb_swift_103").is(':checked')) {

        if (_txt_MT103Receiver == "") {
            alert('Please fill Receiver for MT103');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txt_MT103Receiver").focus();
            return false;
        }

        if (_txtCurrency != "") {
            if (_txtCurrency != _lbldocurr) {
                if (_txtExchangeRate == "0" || _txtExchangeRate == "") {
                    alert('Please Fill  Exch Rate.');
                    $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtExchangeRate").focus();
                    return false;
                }
            }
        }

        if (_txtCurrency != "") {
            if (_txtCurrency == _lbldocurr) {
                if (_txtExchangeRate != "") {
                    alert('Exch Rate is not allowed when bill curr and curr are same.');
                    $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtExchangeRate").focus();
                    return false;
                }
            }
        }

        var _ddlOrderingCustomer = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlOrderingCustomer").val();
        var _txtOrderingCustomer_Acc = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Acc").val();

        if (_ddlOrderingCustomer == "A") {
            var _txtOrderingCustomer_SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_SwiftCode").val();
            if (_txtOrderingCustomer_Acc + _txtOrderingCustomer_SwiftCode == "") {
                alert('Please fill Ordering Customer');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Acc").focus();
                return false;
            }
        }
        else if (_ddlOrderingCustomer == "F" || _ddlOrderingCustomer == "K") {
            var _txtOrderingCustomer_Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Name").val();
            var _txtOrderingCustomer_Addr1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr1").val(),
                _txtOrderingCustomer_Addr2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr2").val(),
                _txtOrderingCustomer_Addr3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr3").val();
            if (_txtOrderingCustomer_Acc + _txtOrderingCustomer_Name + _txtOrderingCustomer_Addr1 + _txtOrderingCustomer_Addr2 + _txtOrderingCustomer_Addr3 == "") {
                alert('Please fill Ordering Customer');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Acc").focus();
                return false;
            }
        }
        if (_txtOrderingCustomer_Acc != '' && Check_Party_Identifier(_txtOrderingCustomer_Acc) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Acc").focus();
            return false;
        }

        if (_txtSendingInstitutionAccountNumber != '' && Check_Party_Identifier(_txtSendingInstitutionAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendingInstitutionAccountNumber").focus();
            return false;
        }

        if (_txtOrderingInstitutionAccountNumber != '' && Check_Party_Identifier(_txtOrderingInstitutionAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingInstitutionAccountNumber").focus();
            return false;
        }

        if (_txtReceiversCorrespondentAccountNumber != '' && Check_Party_Identifier(_txtReceiversCorrespondentAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
            return false;
        }
        
        if (_txtSendersCorrespondentAccountNumber != '' && Check_Party_Identifier(_txtSendersCorrespondentAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
            return false;
        }

        if (_txtThirdReimbursementInstitutionAccountNumber != '' && Check_Party_Identifier(_txtThirdReimbursementInstitutionAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtThirdReimbursementInstitutionAccountNumber").focus();
            return false;
        }

        if (_txtIntermediary103AccountNumber != '' && Check_Party_Identifier(_txtIntermediary103AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtIntermediary103AccountNumber").focus();
            return false;
        }

        if (_txtAccountWithInstitution103AccountNumber != '' && Check_Party_Identifier(_txtAccountWithInstitution103AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
            return false;
        }

        var _ddlBeneficiaryCustomer = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_ddlBeneficiaryCustomer").val();
        var _txtBeneficiaryCustomerAccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAccountNumber").val();

        if (_ddlBeneficiaryCustomer == "A") {
            var _txtBeneficiaryCustomerSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerSwiftCode").val();
            if (_txtBeneficiaryCustomerAccountNumber + _txtBeneficiaryCustomerSwiftCode == "") {
                alert('Please fill Beneficiary Customer');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAccountNumber").focus();
                return false;
            }
        }
        else if (_ddlBeneficiaryCustomer == "F" || _ddlBeneficiaryCustomer == "N") {
            var _txtOrderingCustomer_Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Name").val();
            var _txtOrderingCustomer_Addr1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr1").val(),
                _txtOrderingCustomer_Addr2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr2").val(),
                _txtOrderingCustomer_Addr3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtOrderingCustomer_Addr3").val();
            if (_txtBeneficiaryCustomerAccountNumber + _txtOrderingCustomer_Name + _txtOrderingCustomer_Addr1 + _txtOrderingCustomer_Addr2 + _txtOrderingCustomer_Addr3 == "") {
                alert('Please fill Beneficiary Customer');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAccountNumber").focus();
                return false;
            }
        }
        if (_txtBeneficiaryCustomerAccountNumber != '' && Check_Party_Identifier(_txtBeneficiaryCustomerAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtBeneficiaryCustomerAccountNumber").focus();
            return false;
        }

        //C7
        if (_ddlThirdReimbursementInstitution == "A" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionSwiftCode != "") {
            if (_ddlSendersCorrespondent == "A" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentSwiftCode == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "A" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentSwiftCode == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        else if (_ddlThirdReimbursementInstitution == "B" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionLocation != "") {
            if (_ddlSendersCorrespondent == "A" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentSwiftCode == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "A" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentSwiftCode == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        else if (_ddlThirdReimbursementInstitution == "D" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionName + _txtThirdReimbursementInstitutionAddress1
        + _txtThirdReimbursementInstitutionAddress2 + _txtThirdReimbursementInstitutionAddress3 != "") {
            if (_ddlSendersCorrespondent == "A" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentSwiftCode == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "A" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentSwiftCode == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        //---------------------------------------------------IF 55a present and 53b and 54b not present-----------------------------------------------------//
        if (_ddlThirdReimbursementInstitution == "A" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionSwiftCode != "") {
            if (_ddlSendersCorrespondent == "B" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentLocation == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "B" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentLocation == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        else if (_ddlThirdReimbursementInstitution == "B" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionLocation != "") {
            if (_ddlSendersCorrespondent == "B" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentLocation == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "B" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentLocation == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        else if (_ddlThirdReimbursementInstitution == "D" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionName + _txtThirdReimbursementInstitutionAddress1
        + _txtThirdReimbursementInstitutionAddress2 + _txtThirdReimbursementInstitutionAddress3 != "") {
            if (_ddlSendersCorrespondent == "B" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentLocation == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "B" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentLocation == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        //---------------------------------------------------IF 55a present and 53d and 54d not present-----------------------------------------------------//
        if (_ddlThirdReimbursementInstitution == "A" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionSwiftCode != "") {
            if (_ddlSendersCorrespondent == "D" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentName + _txtSendersCorrespondentAddress1
			+ _txtSendersCorrespondentAddress2 + _txtSendersCorrespondentAddress3 == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "D" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentName + _txtReceiversCorrespondentAddress1
			+ _txtReceiversCorrespondentAddress2 + _txtReceiversCorrespondentAddress3 == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        else if (_ddlThirdReimbursementInstitution == "B" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionLocation != "") {
            if (_ddlSendersCorrespondent == "D" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentName + _txtSendersCorrespondentAddress1
			+ _txtSendersCorrespondentAddress2 + _txtSendersCorrespondentAddress3 == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "D" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentName + _txtReceiversCorrespondentAddress1
			+ _txtReceiversCorrespondentAddress2 + _txtReceiversCorrespondentAddress3 == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        else if (_ddlThirdReimbursementInstitution == "D" && _txtThirdReimbursementInstitutionAccountNumber + _txtThirdReimbursementInstitutionName + _txtThirdReimbursementInstitutionAddress1
        + _txtThirdReimbursementInstitutionAddress2 + _txtThirdReimbursementInstitutionAddress3 != "") {
            if (_ddlSendersCorrespondent == "D" && _txtSendersCorrespondentAccountNumber + _txtSendersCorrespondentName + _txtSendersCorrespondentAddress1
			+ _txtSendersCorrespondentAddress2 + _txtSendersCorrespondentAddress3 == "") {
                alert('Please fill 53A .');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSendersCorrespondentAccountNumber").focus();
                return false;
            }
            else if (_ddlReceiversCorrespondent == "D" && _txtReceiversCorrespondentAccountNumber + _txtReceiversCorrespondentName + _txtReceiversCorrespondentAddress1
			+ _txtReceiversCorrespondentAddress2 + _txtReceiversCorrespondentAddress3 == "") {
                alert('Please fill 54A.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiversCorrespondentAccountNumber").focus();
                return false;
            }
        }
        //C9
        if (_ddlIntermediary103 == "A" && _txtIntermediary103AccountNumber + _txtIntermediary103SwiftCode != "") {
            if (_ddlAccountWithInstitution103 == "A" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103SwiftCode == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "B" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103Location == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "C" && _txtAccountWithInstitution103AccountNumber == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "D" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103Name + _txtAccountWithInstitution103Address1
                + _txtAccountWithInstitution103Address2 + _txtAccountWithInstitution103Address3	== "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
        }
        //---------------------------------56C is present and 57a,57b,57c,57d not present-------------------------------------------------------------------------//
        if (_ddlIntermediary103 == "C" && _txtIntermediary103AccountNumber != "") {
            if (_ddlAccountWithInstitution103 == "A" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103SwiftCode == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "B" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103Location == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "C" && _txtAccountWithInstitution103AccountNumber == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "D" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103Name + _txtAccountWithInstitution103Address1
                + _txtAccountWithInstitution103Address2 + _txtAccountWithInstitution103Address3 == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
        }
        //---------------------------------56D is present and 57a,57b,57c,57d not present-------------------------------------------------------------------------//
        if (_ddlIntermediary103 == "D" && _txtIntermediary103AccountNumber + _txtIntermediary103Name + _txtIntermediary103Address1
            + _txtIntermediary103Address2 + _txtIntermediary103Address3 != "") {
            if (_ddlAccountWithInstitution103 == "A" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103SwiftCode == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "B" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103Location == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "C" && _txtAccountWithInstitution103AccountNumber == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution103 == "D" && _txtAccountWithInstitution103AccountNumber + _txtAccountWithInstitution103Name + _txtAccountWithInstitution103Address1
                + _txtAccountWithInstitution103Address2 + _txtAccountWithInstitution103Address3 == "") {
                alert('Please Select 57 Account With Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtAccountWithInstitution103AccountNumber").focus();
                return false;
            }
        }
        //C13
        if (_txtInstructionCode == "CHQB" && _txtBeneficiaryCustomerAccountNumber != "") {
            alert('Beneficiary Customer is not allowed.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_txtInstructionCode").focus();
            return false;
        }

        //C14
        if (_txtDetailsOfCharges == "OUR") {
            if ((_txtSenderCharges != "" && _txtSenderCharges2 != "") || (_txtSenderCharges != "" && _txtSenderCharges2 != "0")) {
                alert('Sender Charges is not allowed for OUR.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges").focus();
                return false;
            }
        }
        else if (_txtDetailsOfCharges == "SHA") {
            if ((_txtReceiverCharges != "" && _txtReceiverCharges2 != "") || (_txtReceiverCharges != "" && _txtReceiverCharges2 != "0")) {
                alert('Receiver Charges is not allowed for SHA.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges").focus();
                return false;
            }
        }
        else if (_txtDetailsOfCharges == "BEN") {
            if ((_txtSenderCharges == "" && _txtSenderCharges2 == "") || (_txtSenderCharges == "" && _txtSenderCharges2 == "0")) {
                alert('Please Fill Sender Charges for BEN.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges").focus();
                return false;
            }
            else if ((_txtReceiverCharges != "" && _txtReceiverCharges2 != "") || (_txtReceiverCharges != "" && _txtReceiverCharges2 != "0")) {
                alert('Receiver Charges is not allowed for BEN.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges").focus();
                return false;
            }
        }
        //C15
        if ((_txtSenderCharges != "" && _txtSenderCharges2 != "") || (_txtSenderCharges != "" && _txtSenderCharges2 != "0")) {
            if ((_txtCurrency == "" && _txtInstructedAmount == "") || (_txtCurrency == "" && _txtInstructedAmount == "0")) {
                alert('Please Fill Currency/Instructed Amount.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtCurrency").focus();
                return false;
            }

        }
        if ((_txtReceiverCharges != "" && _txtReceiverCharges2 != "") || (_txtReceiverCharges != "" && _txtReceiverCharges2 != "0")) {
            if ((_txtCurrency == "" && _txtInstructedAmount == "") || (_txtCurrency == "" && _txtInstructedAmount == "0")) {
                alert('Please Fill Currency/Instructed Amount.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtCurrency").focus();
                return false;
            }
        }

        //C18
        if (_txtReceiverCharges != "") {
            if (_txtReceiverCharges != _lbldocurr) {
                alert('Receiver Charges And Currency Are Not Same.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges").focus();
                return false;
            }
        }
        //Check Currency and Amount
        if ((_txtReceiverCharges != "" && _txtReceiverCharges2 != "") || (_txtReceiverCharges != "" && _txtReceiverCharges2 != "0") || (_txtReceiverCharges == "" && _txtReceiverCharges2 != "0")) {
            if ((_txtReceiverCharges == "" && _txtReceiverCharges2 != "")) {
                alert('Please enter Currency for Receiver charges.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges").focus();
                return false;
            }
            else if ((_txtReceiverCharges != "" && _txtReceiverCharges2 == "") || (_txtReceiverCharges != "" && _txtReceiverCharges2 == "0")) {
                alert('Please enter Amount for Receiver charges.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtReceiverCharges2").focus();
                return false;
            }
        }
        if ((_txtSenderCharges != "" && _txtSenderCharges2 != "") || (_txtSenderCharges != "" && _txtSenderCharges2 != "0") || (_txtSenderCharges == "" && _txtSenderCharges2 != "0")) {
            if ((_txtSenderCharges == "" && _txtSenderCharges2 != "")) {
                alert('Please enter Currency for Senders charges.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges").focus();
                return false;
            }
            else if ((_txtSenderCharges != "" && _txtSenderCharges2 == "") || (_txtSenderCharges != "" && _txtSenderCharges2 == "0")) {
                alert('Please enter Amount for Senders charges.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtSenderCharges2").focus();
                return false;
            }
        }
        if ((_txtCurrency != "" && _txtInstructedAmount != "") || (_txtCurrency != "" && _txtInstructedAmount != "0") || (_txtCurrency == "" && _txtInstructedAmount != "0")) {
            if ((_txtCurrency == "" && _txtInstructedAmount != "")) {
                alert('Please enter Currency');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtCurrency").focus();
                return false;
            }
            else if ((_txtCurrency != "" && _txtInstructedAmount == "") || (_txtCurrency != "" && _txtInstructedAmount == "0")) {
                alert('Please enter Instructed Amount');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtInstructedAmount").focus();
                return false;
            }
        }

        if (_txtDetailsOfCharges == "") {
            alert('Details of Charges can not be blank.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift103_txtDetailsOfCharges").focus();
            return false;
        }
    }
    //---------------------------------------------------------END Validation for MT103----------------------------------------------------------------------------------


    //---------------------------------------------------------Validation for MT200-------------------------------------------------------------------------------------
    var _txt200BicCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200BicCode").val();
    var _txt200TransactionRefNO = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200TransactionRefNO").val();
    var _txt200Date = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Date").val();
    var _txt200Currency = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Currency").val();
    var _txt200Amount = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Amount").val();
    var _ddl200AccWithInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_ddl200AccWithInstitution").val();
    var _txt200AccountWithInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAccountNumber").val();
    var _txt200AccWithInstitutionSwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionSwiftCode").val();
    var _txt200AccWithInstitutionLocation = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionLocation").val();
    var _txt200AccWithInstitutionName = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionName").val();
    var _txt200AccWithInstitutionAddress1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAddress1").val();
    var _txt200AccWithInstitutionAddress2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAddress2").val();
    var _txt200AccWithInstitutionAddress3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAddress3").val();


    if ($("#TabContainerMain_tbSwift_rdb_swift_200").is(':checked')) {
        if (_txt200BicCode == "") {
            alert('Please Fill Receiver BIC Code.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200BicCode").focus();
            return false;
        }
        if (_txt200TransactionRefNO == "") {
            alert('Please Fill Transaction Ref No.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200TransactionRefNO").focus();
            return false;
        }
        else if (_txt200TransactionRefNO.match("^/")) {
            alert("Transaction Ref No must not start with /");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200TransactionRefNO").focus();
            return false;
        }
        else if (_txt200TransactionRefNO.match("/$")) {
            alert("Transaction Ref No must not end with /");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200TransactionRefNO").focus();
            return false;
        }
        else if (_txt200TransactionRefNO.match("//")) {
            alert("Transaction Ref No must not contain two consecutive slashes //");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200TransactionRefNO").focus();
            return false;
        }
        else if (_txt200Date == "") {
            alert('Please enter date.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Date").focus();
            return false;
        }
        else if (_txt200Currency == "") {
            alert('Please enter currency.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Currency").focus();
            return false;
        }
        else if (_txt200Currency.toUpperCase() == "XAU" || _txt200Currency.toUpperCase() == "XAG" || _txt200Currency.toUpperCase() == "XPD" || _txt200Currency.toUpperCase() == "XPT") {
            alert('The codes XAU, XAG, XPD and XPT are not allowed, as these are codes for commodities for which the category 6 commodities messages must be used');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Currency").focus();
            return false;
        }
        else if (_txt200Amount == "" || _txt200Amount == 0) {
            alert('Please enter amount');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200Amount").focus();
            return false;
        }

        if (_ddl200AccWithInstitution == "A") {
            if (_txt200AccountWithInstitution == "" && _txt200AccWithInstitutionSwiftCode == "") {
                alert('Please Fill Account With Institution[57]');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAccountNumber").focus();
                return false;
            }
        }
        else if (_ddl200AccWithInstitution == "B") {
            if (_txt200AccountWithInstitution == "" && _txt200AccWithInstitutionLocation == "") {
                alert('Please Fill Account With Institution[57]');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAccountNumber").focus();
                return false;
            }
        }
        else if (_ddl200AccWithInstitution == "D") {
            if (_txt200AccountWithInstitution == "" && _txt200AccWithInstitutionName == "" && _txt200AccWithInstitutionAddress1 == "" && _txt200AccWithInstitutionAddress2 == "" && _txt200AccWithInstitutionAddress3 == "") {
                alert('Please Fill Account With Institution[57]');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAccountNumber").focus();
                return false;
            }
        }
		
		var _txt200IntermediaryAccountNumber= $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediaryAccountNumber").val();

		if (_txt200IntermediaryAccountNumber != '' && Check_Party_Identifier(_txt200IntermediaryAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200IntermediaryAccountNumber").focus();
            return false;
        }
		
		var _txt200AccWithInstitutionAccountNumber= $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAccountNumber").val();

		if (_txt200AccWithInstitutionAccountNumber != '' && Check_Party_Identifier(_txt200AccWithInstitutionAccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200AccWithInstitutionAccountNumber").focus();
            return false;
        }

        var _txt200SenderCorreCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SenderCorreCode").val();
        if (_txt200SenderCorreCode != '' && Check_Party_Identifier(_txt200SenderCorreCode) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift200_txt200SenderCorreCode").focus();
            return false;
        }
    }
    
    //---------------------------------------------------------END Validation for MT200----------------------------------------------------------------------------------
    //---------------------------------------------------------Validation for MT202-------------------------------------------------------------------------------------
    var _txtBeneficiaryInstitution202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202AccountNumber").val();
    var _BeneficiaryInstitution = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlBeneficiaryInstitution202").val();
    var _ddlIntermediary202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlIntermediary202").val();
    var _txtIntermediary202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202AccountNumber").val();
    var _txtIntermediary202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202SwiftCode").val();
    var _txtIntermediary202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Name").val();
    var _txtIntermediary202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Address1").val();
    var _txtIntermediary202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Address2").val();
    var _txtIntermediary202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202Address3").val();
    var _ddlAccountWithInstitution202 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_ddlAccountWithInstitution202").val();
    var _txtAccountWithInstitution202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").val();
    var _txtAccountWithInstitution202SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202SwiftCode").val();
    var _txtAccountWithInstitution202Location = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Location").val();
    var _txtAccountWithInstitution202Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Name").val();
    var _txtAccountWithInstitution202Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Address1").val();
    var _txtAccountWithInstitution202Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Address2").val();
    var _txtAccountWithInstitution202Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202Address3").val();
    var _txt202Amount = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txt202Amount").val();

    if ($("#TabContainerMain_tbSwift_rdb_swift_202").is(':checked')) {
        // changes done by bhupen on 12012021
        //C1
        if (_ddlIntermediary202 == "A" && _txtIntermediary202AccountNumber + _txtIntermediary202SwiftCode != "") {
            if (_ddlAccountWithInstitution202 == "A" && _txtAccountWithInstitution202AccountNumber + _txtAccountWithInstitution202SwiftCode == "") {
                alert('Please Fill [57] Account With institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution202 == "B" && _txtAccountWithInstitution202AccountNumber + _txtAccountWithInstitution202Location == "") {
                alert('Please Fill [57] Account With institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution202 == "D" && _txtAccountWithInstitution202AccountNumber + _txtAccountWithInstitution202Name + _txtAccountWithInstitution202Address1
                + _txtAccountWithInstitution202Address2 + _txtAccountWithInstitution202Address3 == "") {
                alert('Please Fill [57] Account With institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
                return false;
            }
        }

        if (_ddlIntermediary202 == "D" && _txtIntermediary202AccountNumber + _txtIntermediary202Name + _txtIntermediary202Address1
            + _txtIntermediary202Address2 + _txtIntermediary202Address3 != "") {
            if (_ddlAccountWithInstitution202 == "A" && _txtAccountWithInstitution202AccountNumber + _txtAccountWithInstitution202SwiftCode == "") {
                alert('Please Fill [57] Account With institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution202 == "B" && _txtAccountWithInstitution202AccountNumber + _txtAccountWithInstitution202Location == "") {
                alert('Please Fill [57] Account With institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
                return false;
            }
            else if (_ddlAccountWithInstitution202 == "D" && _txtAccountWithInstitution202AccountNumber + _txtAccountWithInstitution202Name + _txtAccountWithInstitution202Address1
                + _txtAccountWithInstitution202Address2 + _txtAccountWithInstitution202Address3 == "") {
                alert('Please Fill [57] Account With institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
                return false;
            }
        }

//        if (_ddlIntermediary202 == "A") {
//            if ((_txtIntermediary202AccountNumber + _txtIntermediary202SwiftCode != "") && (_txtAccountWithInstitution202AccountNumber + _txtAccountWithInstitution202SwiftCode == "")) {
//                alert('Please Fill [57] Account With institution');
//                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
//                return false;
//            }
//        }
        //--------------------------------------end----------------------------------------------------------------------------

        if (_txt202Amount == "" || _txt202Amount == 0) {
            alert('Please Enter Amount.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txt202Amount").focus();
            return false;
        }

        if (_BeneficiaryInstitution == "A") {
            var SwiftCode = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202SwiftCode").val();

            if (_txtBeneficiaryInstitution202AccountNumber == "" && SwiftCode == "") {
                alert('Please Fill [58] Beneficiary Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202AccountNumber").focus();
                return false;
            }
        }
        else if (_BeneficiaryInstitution == "D") {
            var Name = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Name").val();
            var Address1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Address1").val();
            var Address2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Address2").val();
            var Address3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202Address3").val();

            if (_txtBeneficiaryInstitution202AccountNumber == "" && Name == "" && Address1 == "" && Address2 == "" && Address3 == "") {
                alert('Please Fill [58] Beneficiary Institution.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202AccountNumber").focus();
                return false;
            }
        }

        var _txtOrderingInstitution202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202AccountNumber").val();
        if (_txtOrderingInstitution202AccountNumber != '' && Check_Party_Identifier(_txtOrderingInstitution202AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtOrderingInstitution202AccountNumber").focus();
            return false;
        }

        var _txtSendersCorrespondent202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202AccountNumber").val();

        if (_txtSendersCorrespondent202AccountNumber != '' && Check_Party_Identifier(_txtSendersCorrespondent202AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtSendersCorrespondent202AccountNumber").focus();
            return false;
        }

        var _txtReceiversCorrespondent202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202AccountNumber").val();

        if (_txtReceiversCorrespondent202AccountNumber != '' && Check_Party_Identifier(_txtReceiversCorrespondent202AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtReceiversCorrespondent202AccountNumber").focus();
            return false;
        }

        var _txtIntermediary202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202AccountNumber").val();

        if (_txtIntermediary202AccountNumber != '' && Check_Party_Identifier(_txtIntermediary202AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtIntermediary202AccountNumber").focus();
            return false;
        }

        if (_txtAccountWithInstitution202AccountNumber != '' && Check_Party_Identifier(_txtAccountWithInstitution202AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtAccountWithInstitution202AccountNumber").focus();
            return false;
        }

        var _txtBeneficiaryInstitution202AccountNumber = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202AccountNumber").val();

        if (_txtBeneficiaryInstitution202AccountNumber != '' && Check_Party_Identifier(_txtBeneficiaryInstitution202AccountNumber) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwift202_txtBeneficiaryInstitution202AccountNumber").focus();
            return false;
        }
    }

    //---------------------------------------------------------END Validation for MT202----------------------------------------------------------------------------------
    //--------------------------------------------------------- Validation for MT754-------------------------------------------------------------------------------------

    var _txt_754_SenRef = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRef").val();
    var _txt_754_RelRef = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_RelRef").val();

    var _txt_754_AddAmtClamd_Ccy = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_AddAmtClamd_Ccy").val();
    var _txt_754_AddAmtClamd_Amt = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_AddAmtClamd_Amt").val();

    var _ddlPrinAmtPaidAccNego_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlPrinAmtPaidAccNego_754").val();
    var _txtPrinAmtPaidAccNegoDate_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoDate_754").val();
    var _txtPrinAmtPaidAccNegoCurr_7541 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoCurr_754").val();
    var _txtPrinAmtPaidAccNegoCurr_754 = _txtPrinAmtPaidAccNegoCurr_7541.toUpperCase();
    var _txtPrinAmtPaidAccNegoAmt_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoAmt_754").val();


    var _ddlTotalAmtclamd_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlTotalAmtclamd_754").val();
    var _txt_754_TotalAmtClmd_Date = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Date").val();
    var _txt_754_TotalAmtClmd_Ccy1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Ccy").val();
    var _txt_754_TotalAmtClmd_Ccy = _txt_754_TotalAmtClmd_Ccy1.toUpperCase();
    var _txt_754_TotalAmtClmd_AMT = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Amt").val();

    var _ddlReimbursingbank_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlReimbursingbank_754").val();
    var _txtReimbursingBankAccountnumber_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAccountnumber_754").val();
    var _txtReimbursingBankpartyidentifier_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankpartyidentifier_754").val();
    var _txtReimbursingBankIdentifiercode_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankIdentifiercode_754").val();
    var _txtReimbursingBankLocation_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankLocation_754").val();
    var _txtReimbursingBankName_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankName_754").val();
    var _txtReimbursingBankAddress1_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAddress1_754").val();
    var _txtReimbursingBankAddress2_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAddress2_754").val();
    var _txtReimbursingBankAddress3_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAddress3_754").val();

    var _ddlAccountwithbank_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlAccountwithbank_754").val();
    var _txtAccountwithBankAccountnumber_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAccountnumber_754").val();
    var _txtAccountwithBankpartyidentifier_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankpartyidentifier_754").val();
    var _txtAccountwithBankIdentifiercode_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankIdentifiercode_754").val();
    var _txtAccountwithBankLocation_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankLocation_754").val();
    var _txtAccountwithBankName_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankName_754").val();
    var _txtAccountwithBankAddress1_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAddress1_754").val();
    var _txtAccountwithBankAddress2_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAddress2_754").val();
    var _txtAccountwithBankAddress3_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAddress3_754").val();

    var _txt_MT754_Sender_to_Receiver_Information = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo1").val();
    var _txt_MT754_Sender_to_Receiver_Information2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo2").val();
    var _txt_MT754_Sender_to_Receiver_Information3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo3").val();
    var _txt_MT754_Sender_to_Receiver_Information4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo4").val();
    var _txt_MT754_Sender_to_Receiver_Information5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo5").val();
    var _txt_MT754_Sender_to_Receiver_Information6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo6").val();

    var _txt_Narrative_754_1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_1").val();
    var _txt_Narrative_754_2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_2").val();
    var _txt_Narrative_754_3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_3").val();
    var _txt_Narrative_754_4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_4").val();
    var _txt_Narrative_754_5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_5").val();
    var _txt_Narrative_754_6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_6").val();
    var _txt_Narrative_754_7 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_7").val();
    var _txt_Narrative_754_8 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_8").val();
    var _txt_Narrative_754_9 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_9").val();
    var _txt_Narrative_754_10 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_10").val();
    var _txt_Narrative_754_11 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_11").val();
    var _txt_Narrative_754_12 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_12").val();
    var _txt_Narrative_754_13 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_13").val();
    var _txt_Narrative_754_14 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_14").val();
    var _txt_Narrative_754_15 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_15").val();
    var _txt_Narrative_754_16 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_16").val();
    var _txt_Narrative_754_17 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_17").val();
    var _txt_Narrative_754_18 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_18").val();
    var _txt_Narrative_754_19 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_19").val();
    var _txt_Narrative_754_20 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_20").val();
    var _txt_Narrative_754_21 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_21").val();
    var _txt_Narrative_754_22 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_22").val();
    var _txt_Narrative_754_23 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_23").val();
    var _txt_Narrative_754_24 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_24").val();
    var _txt_Narrative_754_25 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_25").val();
    var _txt_Narrative_754_26 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_26").val();
    var _txt_Narrative_754_27 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_27").val();
    var _txt_Narrative_754_28 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_28").val();
    var _txt_Narrative_754_29 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_29").val();
    var _txt_Narrative_754_30 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_30").val();
    var _txt_Narrative_754_31 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_31").val();
    var _txt_Narrative_754_32 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_32").val();
    var _txt_Narrative_754_33 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_33").val();
    var _txt_Narrative_754_34 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_34").val();
    var _txt_Narrative_754_35 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_35").val();


    if ($("#TabContainerMain_tbSwift_rdb_swift_754").is(':checked')) {
        if (_txt_754_SenRef == "") {
            alert('Please Fill Sender Reference No.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRef").focus();
            return false;
        }
        if (_txt_754_RelRef == "") {
            alert('Please Fill Related Reference No.');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_RelRef").focus();
            return false;
        }
        if (_txt_754_SenRef.match("^/")) {
            alert("Sender Reference No start with /");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRef").focus();
            return false;
        }
        if (_txt_754_SenRef.match("/$")) {
            alert("Sender Reference No ends with /");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRef").focus();
            return false;
        }

        if (_txt_754_SenRef.match("//")) {
            alert("Sender Reference No content //");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRef").focus();
            return false;
        }

        if (_txt_754_RelRef.match("^/")) {
            alert("Related Reference No start with /");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_RelRef").focus();
            return false;
        }
        if (_txt_754_RelRef.match("/$")) {
            alert("Related Reference No. ends with /");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_RelRef").focus();
            return false;
        }

        if (_txt_754_RelRef.match("//")) {
            alert("Related Reference No content //");
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_RelRef").focus();
            return false;
        }
        var _txt_754_SenRecInformation1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo1").val();
        var _txt_754_SenRecInformation2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo2").val();
        var _txt_754_SenRecInformation3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo3").val();
        var _txt_754_SenRecInformation4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo4").val();
        var _txt_754_SenRecInformation5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo5").val();
        var _txt_754_SenRecInformation6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo6").val();


        var _txt_Narratives_754_1 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_1").val();
        var _txt_Narratives_754_2 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_2").val();
        var _txt_Narratives_754_3 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_3").val();
        var _txt_Narratives_754_4 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_4").val();
        var _txt_Narratives_754_5 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_5").val();
        var _txt_Narratives_754_6 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_6").val();
        var _txt_Narratives_754_7 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_7").val();
        var _txt_Narratives_754_8 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_8").val();
        var _txt_Narratives_754_9 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_9").val();
        var _txt_Narratives_754_10 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_10").val();
        var _txt_Narratives_754_11 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_11").val();
        var _txt_Narratives_754_12 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_12").val();
        var _txt_Narratives_754_13 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_13").val();
        var _txt_Narratives_754_14 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_14").val();
        var _txt_Narratives_754_15 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_15").val();
        var _txt_Narratives_754_16 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_16").val();
        var _txt_Narratives_754_17 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_17").val();
        var _txt_Narratives_754_18 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_18").val();
        var _txt_Narratives_754_19 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_19").val();
        var _txt_Narratives_754_20 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_20").val();
        var _txt_Narratives_754_21 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_21").val();
        var _txt_Narratives_754_22 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_22").val();
        var _txt_Narratives_754_23 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_23").val();
        var _txt_Narratives_754_24 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_24").val();
        var _txt_Narratives_754_25 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_25").val();
        var _txt_Narratives_754_26 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_26").val();
        var _txt_Narratives_754_27 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_27").val();
        var _txt_Narratives_754_28 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_28").val();
        var _txt_Narratives_754_29 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_29").val();
        var _txt_Narratives_754_30 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_30").val();
        var _txt_Narratives_754_31 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_31").val();
        var _txt_Narratives_754_32 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_32").val();
        var _txt_Narratives_754_33 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_33").val();
        var _txt_Narratives_754_34 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_34").val();
        var _txt_Narratives_754_35 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_Narrative_754_35").val();

        if (((_txt_754_SenRecInformation1 != '' || _txt_754_SenRecInformation2 != '' || _txt_754_SenRecInformation3 || _txt_754_SenRecInformation4 != '' || _txt_754_SenRecInformation5 != '' || _txt_754_SenRecInformation6 != '')
         && (_txt_Narratives_754_1 != '' || _txt_Narratives_754_2 != '' || _txt_Narratives_754_3 != '' || _txt_Narratives_754_4 != '' || _txt_Narratives_754_5 != ''
         || _txt_Narratives_754_6 != '' || _txt_Narratives_754_7 != '' || _txt_Narratives_754_8 != '' || _txt_Narratives_754_9 != '' || _txt_Narratives_754_10 != ''
         || _txt_Narratives_754_11 != '' || _txt_Narratives_754_12 != '' || _txt_Narratives_754_13 != '' || _txt_Narratives_754_14 != '' || _txt_Narratives_754_15 != ''
         || _txt_Narratives_754_16 != '' || _txt_Narratives_754_17 != '' || _txt_Narratives_754_18 != '' || _txt_Narratives_754_19 != '' || _txt_Narratives_754_20 != ''
         || _txt_Narratives_754_21 != '' || _txt_Narratives_754_22 != '' || _txt_Narratives_754_23 != '' || _txt_Narratives_754_24 != '' || _txt_Narratives_754_25 != ''
         || _txt_Narratives_754_26 != '' || _txt_Narratives_754_27 != '' || _txt_Narratives_754_28 != '' || _txt_Narratives_754_29 != '' || _txt_Narratives_754_30 != ''
         || _txt_Narratives_754_31 != '' || _txt_Narratives_754_32 != '' || _txt_Narratives_754_33 != '' || _txt_Narratives_754_34 != '' || _txt_Narratives_754_35 != '')
         )) {
            alert('Either field 72Z or 77 may be present, but not both ');
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_SenRecInfo1").focus();
            return false;

        }
        var ddlReimbursingbanks_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlReimbursingbank_754").val();
        var txtReimbursingBankAccountnumbers_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankAccountnumber_754").val();
        var txtReimbursingBankpartyidentifiers_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankpartyidentifier_754").val();
        var txtReimbursingBankIdentifiercodes_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankIdentifiercode_754").val();

        var ddlAccountwithbanks_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlAccountwithbank_754").val();
        var txtAccountwithBankAccountnumbers_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankAccountnumber_754").val();
        var txtAccountwithBankpartyidentifiers_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankpartyidentifier_754").val();
        var txtAccountwithBankIdentifiercodes_754 = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankIdentifiercode_754").val();

        if ((ddlReimbursingbanks_754 == 'A' && ddlAccountwithbanks_754 == 'A')) {
            if ((txtReimbursingBankAccountnumbers_754 != '' || txtReimbursingBankpartyidentifiers_754 != '' || txtReimbursingBankIdentifiercodes_754 != '')
            && (txtAccountwithBankAccountnumbers_754 != '' || txtAccountwithBankpartyidentifiers_754 != '' || txtAccountwithBankIdentifiercodes_754 != '')) {
                alert('Either field 53a or 57a may be present, but not both');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_ddlReimbursingbank_754").focus();
                return false;
            }
        }


        //--------------end--------------------------------------------------------------
        if (_ddlPrinAmtPaidAccNego_754 == "A") {
            if (_txtPrinAmtPaidAccNegoDate_754 == "" && _txtPrinAmtPaidAccNegoCurr_754 == "" && _txtPrinAmtPaidAccNegoAmt_754 == "") {
                alert('Please Fill Principal Amount Paid.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoDate_754").focus();
                return false;
            }
            else if (_ddlPrinAmtPaidAccNego_754 == "B") {
                if (_txtPrinAmtPaidAccNegoCurr_754 == "" && _txtPrinAmtPaidAccNegoAmt_754 == "") {
                    alert('Please Fill Principal Amount Paid.');
                    $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoCurr_754").focus();
                    return false;
                }
            }
        }

        //C3


        if (_txt_754_TotalAmtClmd_Date != '') {
            if (_txt_754_TotalAmtClmd_Ccy == '') {
                alert('Please Enter Total Amount Currency.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Ccy").focus();
                return false;
            }
            else if (_txt_754_TotalAmtClmd_AMT == '') {
                alert('Please Enter Total Amount.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Amt").focus();
                return false;
            }
        }
        if (_txt_754_TotalAmtClmd_Ccy != '') {
            if (_txt_754_TotalAmtClmd_Date == '') {
                alert('Please Enter Total Amount Date.');
                // $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Amt").focus();
                return false;
            }
            else if (_txt_754_TotalAmtClmd_AMT == '') {
                alert('Please Enter Total Amount.');
                // $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Amt").focus();
                return false;
            }
        }
        if (_txt_754_TotalAmtClmd_AMT != '') {
            if (_txt_754_TotalAmtClmd_Ccy == '') {
                alert('Please Enter Total Amount Currency.');
                //   $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Amt").focus();
                return false;
            }
            else if (_txt_754_TotalAmtClmd_AMT == '') {
                alert('Please Enter Total Amount.');
                // $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txt_754_TotalAmtClmd_Amt").focus();
                return false;
            }
        }

        if (_txt_754_TotalAmtClmd_Ccy != "") {
            if (_txtPrinAmtPaidAccNegoCurr_754 != _txt_754_TotalAmtClmd_Ccy) {
                alert('Principal Amount Currency And Total Amount Currency Are Not Same.');
                $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtPrinAmtPaidAccNegoCurr_754").focus();
                return false;
            }
        }

        if (txtReimbursingBankpartyidentifiers_754 != '' && Check_Party_Identifier(txtReimbursingBankpartyidentifiers_754) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtReimbursingBankpartyidentifier_754").focus();
            return false;
        }

        if (txtAccountwithBankpartyidentifiers_754 != '' && Check_Party_Identifier(txtAccountwithBankpartyidentifiers_754) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtAccountwithBankpartyidentifier_754").focus();
            return false;
        }

        var txtBeneficiarypartyidentifire = $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiarypartyidentifire").val()

        if (txtBeneficiarypartyidentifire != '' && Check_Party_Identifier(txtBeneficiarypartyidentifire) == false) {
            $("#TabContainerMain_tbSwift_TabContainerSwift_tbSwiftMT754_txtBeneficiarypartyidentifire").focus();
            return false;
        }
    }

    //---------------------------------------------------------END Validation for MT754----------------------------------------------------------------------------------

    //------------------------------------------------------Nilesh END------------------------------------------------------


    if (confirm('Are you sure you want to Submit this record to checker?')) {
        return true;
    }
    else
        return false;

}
function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}

function OpenCR_Code_help(e, IMP_ACC, Debit_Credit) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_SundryaccountHelp.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
        return false;
    }
}
function OpenGO_help(e, IMP_ACC, Debit_Credit) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_SundryaccountHelp1.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
        return false;
    }
}
function select_CR_Code1(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Cust_Acc").val(Acno);
}
function select_DR_Code1(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc").val(Acno);
}
function select_CR_Code2(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Cust_Acc").val(Acno);
}
function select_DR_Code2(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc").val(Acno);
}
function select_CR_Code3(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Cust_Acc").val(Acno);
}
function select_DR_Code3(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc").val(Acno);
}
function select_CR_Code4(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Cust_Acc").val(Acno);
}
function select_DR_Code4(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc").val(Acno);
}
function select_CR_Code5(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Cust_Acc").val(Acno);
}
function select_DR_Code5(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code").val(ACcode);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc").val(Acno);
}
function select_GO1_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Details").val(GLcodeDiscreption);
}
function select_GO1_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Details").val(GLcodeDiscreption);
}
function select_GO1_Debit3(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Details").val(GLcodeDiscreption);
}
function select_GO1_Debit4(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit3(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit4(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Details").val(GLcodeDiscreption);
}
function select_GO3_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Details").val(GLcodeDiscreption);
}
function select_GO3_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Details").val(GLcodeDiscreption);
}
function select_GO3_Debit3(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Details").val(GLcodeDiscreption);
}
function select_GO3_Debit4(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Details").val(GLcodeDiscreption);
}
function select_GO4_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Details").val(GLcodeDiscreption);
}
function select_GO4_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Details").val(GLcodeDiscreption);
}


function select_GO1Left1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AccNo").val(Acno);
}
function select_GO1Left2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AccNo").val(Acno);
}
function select_GO1Right1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AccNo").val(Acno);
}
function select_GO1Right2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AccNo").val(Acno);
}

function select_GO2Left1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AccNo").val(Acno);
}
function select_GO2Left2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AccNo").val(Acno);
}
function select_GO2Right1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AccNo").val(Acno);
}
function select_GO2Right2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AccNo").val(Acno);
}


function select_GO3Left1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Cust_AccNo").val(Acno);
}
function select_GO3Left2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Cust_AccNo").val(Acno);
}
function select_GO3Right1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Cust_AccNo").val(Acno);
}
function select_GO3Right2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Cust_AccNo").val(Acno);
}


function select_GO4_CR_Code(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AccNo").val(Acno);
}
function select_GO4_DR_Code(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AccNo").val(Acno);
}
function Disc_DR_Amt_Calculation(IMP_Accounting) {
    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DiscAmt").val();
    var _txtprinc_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_Princ_Ex_rate").val();
    var _txtprinc_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_Princ_Intnl_Ex_rate").val();
    var _Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_Princ_Ex_Curr").val().toUpperCase();
    var _txt_DR_Cur_Acc_amt;

    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_CR_Acceptance_amt").val(_txt_DiscAmt);
    if (_txtprinc_Ex_rate == '') {
        _txtprinc_Ex_rate = 1;
    }
    if (_txt_DiscAmt == '') {
        _txt_DiscAmt = 0;
    }
    _txt_DiscAmt = parseFloat(_txt_DiscAmt);
    _txtprinc_Ex_rate = parseFloat(_txtprinc_Ex_rate);
    if (_txtprinc_Ex_rate != 1 && _txtprinc_Ex_rate != 0) {
        if ($.isNumeric(_txt_DiscAmt) && $.isNumeric(_txtprinc_Ex_rate)) {
            _txt_DR_Cur_Acc_amt = parseFloat(_txt_DiscAmt * _txtprinc_Ex_rate);
            if (_Princ_Ex_Curr == 'INR') {
                _txt_DR_Cur_Acc_amt = parseFloat(round(parseFloat(_txt_DR_Cur_Acc_amt), 0)).toFixed(2);
                $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_amt").val(_txt_DR_Cur_Acc_amt);
            }
            else {
                _txt_DR_Cur_Acc_amt = parseFloat(_txt_DR_Cur_Acc_amt).toFixed(2);
                $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_amt").val(_txt_DR_Cur_Acc_amt);
            }
        }
    }
    else {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_amt").val(_txt_DiscAmt);
    }
}
function Disc_DR_Curr_Toggel(IMP_Accounting) {
    var _txt_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_Princ_Ex_Curr").val();
    var _Document_Curr = $("#lblDoc_Curr").text();
    if (_txt_Princ_Ex_Curr == '') {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_Curr").val(_Document_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel" + IMP_Accounting + "_txt_IMP" + IMP_Accounting + "_DR_Cur_Acc_Curr").val(_txt_Princ_Ex_Curr);
    }
    Disc_DR_Amt_Calculation(IMP_Accounting);
}
function round(values, decimals) {
    return Number(Math.round(values + 'e' + decimals) + 'e-' + decimals);
}

function Toggel_Swift_Type(Type) {
    if (Type == 'None') {
        if ($("#TabContainerMain_tbSwift_rdb_swift_None").is(':checked')) {
            $("#TabContainerMain_tbSwift_rdb_swift_103").prop('checked', false);
            $("#TabContainerMain_tbSwift_rdb_swift_202").prop('checked', false);
            $("#TabContainerMain_tbSwift_rdb_swift_200").prop('checked', false);
            $("#TabContainerMain_tbSwift_rdb_swift_R42").prop('checked', false);
            $("#TabContainerMain_tbSwift_rdb_swift_754").prop('checked', false);
        }
    }
    else {
        $("#TabContainerMain_tbSwift_rdb_swift_None").prop('checked', false);
    }
}


function ViewSwiftMessage() {
    SaveUpdateData();
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = 'FOREIGN';
    if ($("#TabContainerMain_tbSwift_rdb_swift_103").is(':checked')) {
        _SWIFT_File_Type = 'MT103';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_202").is(':checked')) {
        _SWIFT_File_Type = 'MT202';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_200").is(':checked')) {
        _SWIFT_File_Type = 'MT200';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    //------------------------------------------------------Nilesh-------------------------------------------------------------
    if ($("#TabContainerMain_tbSwift_rdb_swift_754").is(':checked')) {
        _SWIFT_File_Type = 'MT754';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    //------------------------------------------------------Nilesh END------------------------------------------------------------


    if ($("#TabContainerMain_tbSwift_rdb_swift_R42").is(':checked')) {
        _SWIFT_File_Type = 'R42';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    return false;
}
function TogggleDebitCreditCode(GO_No, DebitCredit_No) {
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        if (GO_No == '1') {
            if (DebitCredit_No == '1') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val('');
                }
            }
            if (DebitCredit_No == '3') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '4') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val('');
                }
            }
        }
    }
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_chk_GO2Flag").is(':checked')) {
        if (GO_No == '2') {
            if (DebitCredit_No == '1') {

                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val('');
                }

            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val('');
                }
            }
            if (DebitCredit_No == '3') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '4') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val('');
                }
            }
        }
    }
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_chk_GO3Flag").is(':checked')) {
        if (GO_No == '3') {
            if (DebitCredit_No == '1') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Curr").val('');
                }
            }
            if (DebitCredit_No == '3') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '4') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Curr").val('');
                }
            }
        }
    }
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_chk_GOAcccChangeFlag").is(':checked')) {
        if (GO_No == '4') {
            var _Document_Curr = $("#lblDoc_Curr").text();
            if (DebitCredit_No == '1') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Curr").val(_Document_Curr);
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Curr").val(_Document_Curr);
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Curr").val('');
                }
            }
        }
    }
}

function Toggel_GO1_Left_Remarks() {
    var _txt_GO1_Left_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Remarks").val();
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Details").val(_txt_GO1_Left_Remarks);
    }
}
function ToggleGOAcccChange(Remark_Amt) {
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_chk_GOAcccChangeFlag").is(':checked')) {
        if (Remark_Amt = "Remark") {
            var _txt_GOAccChange_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Remarks").val();

            $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Details").val(_txt_GOAccChange_Remarks);
            $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Details").val(_txt_GOAccChange_Remarks);
        }
        if (Remark_Amt = "Amount") {
            var _txt_GOAccChange_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Amt").val();
            $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Amt").val(_txt_GOAccChange_Debit_Amt);
        }
    }
}

function Toggel_DR_Code() {
    var _txt_DR_Code = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code").val();
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode").val(_txt_DR_Code);
    }
}
function Toggel_DR_Cust_Abbr() {
    var _txt_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr").val();
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust").val(_txt_DR_Cust_abbr);
    }
}
function Toggel_DR_Cust_Acc() {
    var _txt_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc").val();
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AccNo").val(_txt_DR_Cust_Acc);
    }
}
function Toggel_DR_Cust_Name() {
    var _txt_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name").val();
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode_Name").val(_txt_DR_AC_Short_Name);
    }
}
function GO_AccChange_DR_Amt_Calculation() {
    var totalamt = 0, _lblBillAmt = 0, DiscrepancyChrg = 0, hdnTaxChrg = 0, ACC_Their_Comm_Amount = 0;
    var _lblScrutiny = $("#lblScrutiny").text();
    var Foreign_Local = $("#lblForeign_Local").text();

    if ($.isNumeric($("#ACC_Their_Comm_Amount").val())) {
        ACC_Their_Comm_Amount = $("#ACC_Their_Comm_Amount").val();
    }
    if ($.isNumeric($("#lblBillAmt").text())) {
        _lblBillAmt = $("#lblBillAmt").text();
    }
    else {
        alert('Please Check Bill Amount.');
    }
    if (_lblScrutiny == "Discrepant") {
        if ($.isNumeric($("#hdnDiscrepancyChrg").val())) {
            DiscrepancyChrg = $("#hdnDiscrepancyChrg").val();
        }
        else {
            alert('Please Check Discrepancy Charges for Bill Currency.');
        }

        if (Foreign_Local == "Local") {
            if ($.isNumeric($("#hdnTaxChrg").val())) {
                hdnTaxChrg = $("#hdnTaxChrg").val();
            }
            else {
                alert('Please Check Tax amount.');
            }

            var tax = parseFloat(DiscrepancyChrg * (hdnTaxChrg / 100));
            totalamt = parseFloat(_lblBillAmt) - parseFloat(parseFloat(DiscrepancyChrg) + parseFloat(tax)) + parseFloat(ACC_Their_Comm_Amount);
        }
        else if (Foreign_Local == "Foreign") {
            totalamt = parseFloat(_lblBillAmt) - parseFloat(DiscrepancyChrg) + parseFloat(ACC_Their_Comm_Amount);
        }
    }
    else if (_lblScrutiny == "Clean" || _lblScrutiny == "") {
        totalamt = parseFloat(_lblBillAmt) + parseFloat(ACC_Their_Comm_Amount);
    }
    if ($.isNumeric(totalamt)) {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Amt").val(totalamt);
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Amt").val(totalamt);
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


// Changes Ashutosh on 26122019
function OpenFCRefNo_help(e, IMPFCRefNo) {
    debugger;
    var keycode, CustAbbr;
    CustAbbr = $("#hdnCustAbbr").val();
    var BranchName = $("#hdnBranchName").val();
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_IMP_FXHelp.aspx?IMPFCRefNo=' + IMPFCRefNo + '&CustAbbr=' + CustAbbr + '&BranchName=' + BranchName, 500, 700, 'FXRefNoList');
        return false;
    }
}
function selectFX(IMPFCRefNo, GbaseRefNo, ContractDate, InternalRate, ExchangeRate, ContractAmount, ContractCurrency, EquivalentAmount, EquivalentCurrency) {
    //CheckFXREFno(IMPFCRefNo, GbaseRefNo);
    if (IMPFCRefNo == 'IMPFCRefNo1') {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_FCRefNo").val(GbaseRefNo);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_rate").val(ExchangeRate);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Intnl_Ex_rate").val(InternalRate);
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC1_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC1');
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_FCRefNo").focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo2') {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_FCRefNo").val(GbaseRefNo);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_rate").val(ExchangeRate);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Intnl_Ex_rate").val(InternalRate);
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC2_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC2');
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_FCRefNo").focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo3') {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_FCRefNo").val(GbaseRefNo);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_rate").val(ExchangeRate);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Intnl_Ex_rate").val(InternalRate);
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC3_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC3');
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_FCRefNo").focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo4') {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_FCRefNo").val(GbaseRefNo);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_rate").val(ExchangeRate);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Intnl_Ex_rate").val(InternalRate);
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC4_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC4');
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_FCRefNo").focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo5') {
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_FCRefNo").val(GbaseRefNo);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr").val(EquivalentCurrency);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_rate").val(ExchangeRate);
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Intnl_Ex_rate").val(InternalRate);
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC5_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC5');
        $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_FCRefNo").focus();
    }
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
    }
}
//----------------------------------------------------------------------------------------------------------
function CheckFXREFno(IMPFCRefNo, GbaseRefNo) {
    var REFNo = GbaseRefNo;
    var DocNo = $("#txtDocNo").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_Settlement_Collection_Maker.aspx/CheckFXREFno",
        data: '{REFNo:"' + REFNo + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.FXnoStatus == "Exists") {
                alert('This FC Ref No(' + GbaseRefNo + ') already used.')
                if (IMPFCRefNo == 'IMPFCRefNo1') {
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_FCRefNo").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Intnl_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_FCRefNo").focus();

                    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DiscAmt").val();
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cur_Acc_amt").val(_txt_DiscAmt);
                }
                if (IMPFCRefNo == 'IMPFCRefNo2') {
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_FCRefNo").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Intnl_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_FCRefNo").focus();

                    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DiscAmt").val();
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cur_Acc_amt").val(_txt_DiscAmt);
                }
                if (IMPFCRefNo == 'IMPFCRefNo3') {
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_FCRefNo").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Intnl_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_FCRefNo").focus();

                    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DiscAmt").val();
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cur_Acc_amt").val(_txt_DiscAmt);
                }
                if (IMPFCRefNo == 'IMPFCRefNo4') {
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_FCRefNo").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Intnl_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_FCRefNo").focus();

                    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DiscAmt").val();
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cur_Acc_amt").val(_txt_DiscAmt);
                }
                if (IMPFCRefNo == 'IMPFCRefNo5') {
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_FCRefNo").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_Curr").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Intnl_Ex_rate").val('');
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_FCRefNo").focus();

                    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DiscAmt").val();
                    $("#TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cur_Acc_amt").val(_txt_DiscAmt);

                }
                return false;
            }
            else {
                return true;
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}

function Check_Party_Identifier(Party_Identifier) {
    var _Party_Identifier = $("#Party_Identifier").val();
    if (Party_Identifier.match("^/") != '/') {
        alert("Party Identifier must start with /");
        return false;
    }
    else {
        return true;
    }
}