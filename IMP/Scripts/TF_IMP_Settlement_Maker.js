function Toggel_GO_Sundry_Remarks() {
    if ($("#TabContainerMain_tbDocumentGo_Acc_Change_cb_GO_Acc_Change_Flag").is(':checked')) {
        var _txt_Acc_Change_Remarks = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Remarks").val();

        $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Details").val(_txt_Acc_Change_Remarks);
        $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Details").val(_txt_Acc_Change_Remarks);
    }
}
function Toggel_GO_Sundry_Debit_Amt() {
    if ($("#TabContainerMain_tbDocumentGo_Acc_Change_cb_GO_Acc_Change_Flag").is(':checked')) {
        var _txt_Acc_Change_Debit_Amt = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Amt").val();

        $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Amt").val(_txt_Acc_Change_Debit_Amt);
    }
}

function Toggel_GO_Sundry_Remarks() {
    if ($("#TabContainerMain_tbDocumentGO_Sundry_cb_GO_Sundry_Flag").is(':checked')) {
        var _Sundry_txt_Sundry_Remarks = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Remarks").val();

        $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Details").val(_Sundry_txt_Sundry_Remarks);
        $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Details").val(_Sundry_txt_Sundry_Remarks);
    }
}
function Toggel_GO_Sundry_Debit_Amt() {
    if ($("#TabContainerMain_tbDocumentGO_Sundry_cb_GO_Sundry_Flag").is(':checked')) {
        var _txt_Sundry_Debit_Amt = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Amt").val();

        $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Amt").val(_txt_Sundry_Debit_Amt);
    }
}

function OpenCR_Code_help(e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_SundryaccountHelp.aspx', 500, 500, 'SundryCodeList');
        return false;
    }
}

function OpenDR_Code_help(e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_SundryaccountHelp1.aspx', 500, 500, 'SundryCodeList');
        return false;
    }
}

function select_CR_Code(ACcode, ABB, Acno) {

    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val(ACcode)
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val(Acno);
}

function select_DR_Code(ACcode, ABB, Acno) {

    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val(ACcode)
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val(Acno);

    if ($("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AcCode").val(ACcode);
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust").val(ABB);
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AccNo").val(Acno);
    }
}

////
function Toggel_GO1_Remarks() {
    if ($("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").is(':checked')) {
        var _txt_GO1_Remarks = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Remarks").val();

        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Details").val(_txt_GO1_Remarks);
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Details").val(_txt_GO1_Remarks);
    }
}
function Toggel_Debit_Amt() {
    if ($("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").is(':checked')) {
        var _txt_GO1_Debit_Amt = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Amt").val();

        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Amt").val(_txt_GO1_Debit_Amt);
    }
}
function Toggel_DR_Code() {
    var _txt_DR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val();
    if ($("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AcCode").val(_txt_DR_Code);
    }
}
function Toggel_DR_Cust_Abbr() {
    var _txt_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val();
    if ($("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust").val(_txt_DR_Cust_abbr);
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Cust").val(_txt_DR_Cust_abbr);
    }
}
function Toggel_DR_Cust_Acc() {
    var _txt_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val();
    if ($("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AccNo").val(_txt_DR_Cust_Acc);
    }
}
function Toggel_DR_Cust_Name() {
    var _txt_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_txt_DR_AC_Short_Name").val();
    if ($("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AcCode_Name").val(_txt_DR_AC_Short_Name);
    }
}


////
function DR_Curr_Toggel() {
    var _txt_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Princ_Ex_Curr").val();
    var _Document_Curr = $("#lblDoc_Curr").text();
    if (_txt_Princ_Ex_Curr == '') {
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr").val(_Document_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr").val(_txt_Princ_Ex_Curr);
    }
}
function DR_Amt_Calculation() {
    var _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val();
    var _txtprinc_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Ex_rate").val();
    var _txtprinc_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Intnl_Ex_rate").val();
    var _txt_DR_Cur_Acc_amt;

    _txt_DiscAmt = parseFloat(_txt_DiscAmt);
    _txtprinc_Ex_rate = parseFloat(_txtprinc_Ex_rate);

    _txt_DR_Cur_Acc_amt = parseFloat(_txt_DiscAmt * _txtprinc_Ex_rate);

    _txt_DR_Cur_Acc_amt = parseFloat(round(parseFloat(_txt_DR_Cur_Acc_amt), 0)).toFixed(2);

    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val(_txt_DR_Cur_Acc_amt);
}
function round(values, decimals) {
    return Number(Math.round(values + 'e' + decimals) + 'e-' + decimals);
}
function SaveUpdateData() {

    // Document Details
    var hdnUserName = document.getElementById('hdnUserName').value;
    var _BranchName = $("#hdnBranchName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _DocType = $("#hdnDocType").val();
    var _FLC_ILC = $("#hdnflcIlcType").val();
    var _Doc_Scrutiny = $("#hdnDoc_Scrutiny").val();
    var _Document_Curr = $("#lblDoc_Curr").text();
    var _txtValueDate = $("#txtValueDate").val();

    //TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID
    //////////////// SETTLEMENT(IBD,ACC)//////////////////////////
    var _txt_Doc_Customer_ID = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID").val();
    var _txt_Doc_Comment_Code = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Comment_Code").val(),
        _txt_Doc_Maturity = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Maturity").val(),
        _txt_Doc_Settlement_for_Cust = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Settlement_for_Cust").val(),
        _txt_Doc_Settlement_For_Bank = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Settlement_For_Bank").val(),
        _txt_Doc_Interest_From = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Interest_From").val(),
        _txt_Doc_Discount = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Discount").val(),
        _txt_Doc_Interest_To = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Interest_To").val(),
        _txt_Doc_No_Of_Days = $("#TabContainerMain_tbDocumentDetails_txt_Doc_No_Of_Days").val(),
        _txt_Doc_Rate = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Rate").val(),
        _txt_Doc_Amount = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Amount").val(),
        _txt_Doc_Overdue_Interestrate = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Overdue_Interestrate").val(),
        _txt_Doc_Oveduenoofdays = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Oveduenoofdays").val(),
        _txt_Doc_Overdueamount = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Overdueamount").val(),
        txt_Doc_Attn = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Attn").val(),

    //////////////// IMPORT ACCOUNTING //////////////////////////
        _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val(),
        _txtPrinc_matu = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_matu").val(),
        _txtPrinc_lump = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_lump").val(),
        _txtprinc_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Contract_no").val(),
        _txt_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Princ_Ex_Curr").val(),
        _txtprinc_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Ex_rate").val(),
        _txtprinc_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Intnl_Ex_rate").val(),
        _txtInterest_matu = $("#TabContainerMain_tbDocumentAccounting_txtInterest_matu").val(),
        _txtInterest_lump = $("#TabContainerMain_tbDocumentAccounting_txtInterest_lump").val(),
        _txtInterest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Contract_no").val(),
        _txt_interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_interest_Ex_Curr").val(),
        _txtInterest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Ex_rate").val(),
        _txtInterest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Intnl_Ex_rate").val(),
        _txtCommission_matu = $("#TabContainerMain_tbDocumentAccounting_txtCommission_matu").val(),
        _txtCommission_lump = $("#TabContainerMain_tbDocumentAccounting_txtCommission_lump").val(),
        _txtCommission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Contract_no").val(),
        _txt_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Commission_Ex_Curr").val(),
        _txtCommission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Ex_rate").val(),
        _txtCommission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Intnl_Ex_rate").val(),
        _txtTheir_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_matu").val(),
        _txtTheir_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_lump").val(),
        _txtTheir_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Contract_no").val(),
        _txt_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Their_Commission_Ex_Curr").val(),
        _txtTheir_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Ex_rate").val(),
        _txtTheir_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Intnl_Ex_rate").val(),
        _txt_CR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val(),
        _txt_CR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_txt_CR_AC_Short_Name").val(),
        _txt_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val(),
        _txt_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val(),
        _txt_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_Curr").val(),
        _txt_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_amt").val(),
        _txt_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_payer").val(),
        _txt_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_Curr").val(),
        _txt_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_amt").val(),
        _txt_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_payer").val(),
        _txt_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Curr").val(),
        _txt_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_amt").val(),
        _txt_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Payer").val(),
        _txt_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Curr").val(),
        _txt_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_amt").val(),
        _txt_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Payer").val(),
        _txt_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Curr").val(),
        _txt_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_amt").val(),
        _txt_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Payer").val(),
        _txt_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Curr").val(),
        _txt_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_amt").val(),
        _txt_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Payer").val(),
        _txt_DR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val(),
        _txt_DR_AC_Short_Name = $("#TabContainerMain_tbDocumentAccounting_txt_DR_AC_Short_Name").val(),
        _txt_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val(),
        _txt_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val(),
        _txt_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr").val(),
        _txt_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val(),
        _txt_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer").val(),
        _txt_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr2").val(),
        _txt_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt2").val(),
        _txt_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer2").val(),
        _txt_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr3").val(),
        _txt_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt3").val(),
        _txt_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer3").val(),

    ///////////////////// GENERAL OPRATOIN For Bill_Handling /////////////////////////////////////////////////////////////
        //_GO_Bill_Handling_Flag = $("#TabContainerMain_tbDocumentGO_Bill_Handling_cb_GO_Bill_Handling_Flag").val(),
        _GO_Bill_Handling_Flag='Y',
        _txt_Bill_Handling_Comment = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Comment").val(),
        _txt_Bill_Handling_SectionNo = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_SectionNo").val(),
        _txt_Bill_Handling_Remarks = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Remarks").val(),
        _txt_Bill_Handling_Memo = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Memo").val(),
        _txt_Bill_Handling_Scheme_no = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Scheme_no").val(),
        _txt_Bill_Handling_Debit_Code = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Code").val(),
        _txt_Bill_Handling_Debit_Curr = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Curr").val(),
        _txt_Bill_Handling_Debit_Amt = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Amt").val(),
        _txt_Bill_Handling_Debit_Cust = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust").val(),
        _txt_Bill_Handling_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_Name").val(),
        _txt_Bill_Handling_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AcCode").val(),
        _txt_Bill_Handling_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AccNo").val(),
        _txt_Bill_Handling_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Cust_AcCode_Name").val(),
        _txt_Bill_Handling_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Exch_Rate").val(),
        _txt_Bill_Handling_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Exch_CCY").val(),
        _txt_Bill_Handling_Debit_FUND = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_FUND").val(),
        _txt_Bill_Handling_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Check_No").val(),
        _txt_Bill_Handling_Debit_Available = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Available").val(),
        _txt_Bill_Handling_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_AdPrint").val(),
        _txt_Bill_Handling_Debit_Details = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Details").val(),
        _txt_Bill_Handling_Debit_Entity = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Entity").val(),
        _txt_Bill_Handling_Debit_Division = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Division").val(),
        _txt_Bill_Handling_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Inter_Amount").val(),
        _txt_Bill_Handling_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Debit_Inter_Rate").val(),
        _txt_Bill_Handling_Credit_Code = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Code").val(),
        _txt_Bill_Handling_Credit_Curr = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Curr").val(),
        _txt_Bill_Handling_Credit_Amt = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Amt").val(),
        _txt_Bill_Handling_Credit_Cust = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Cust").val(),
        _txt_Bill_Handling_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Cust_Name").val(),
        _txt_Bill_Handling_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Cust_AcCode").val(),
        _txt_Bill_Handling_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Cust_AcCode_Name").val(),
        _txt_Bill_Handling_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Cust_AccNo").val(),
        _txt_Bill_Handling_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Exch_Rate").val(),
        _txt_Bill_Handling_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Exch_Curr").val(),
        _txt_Bill_Handling_Credit_FUND = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_FUND").val(),
        _txt_Bill_Handling_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Check_No").val(),
        _txt_Bill_Handling_Credit_Available = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Available").val(),
        _txt_Bill_Handling_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_AdPrint").val(),
        _txt_Bill_Handling_Credit_Details = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Details").val(),
        _txt_Bill_Handling_Credit_Entity = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Entity").val(),
        _txt_Bill_Handling_Credit_Division = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Division").val(),
        _txt_Bill_Handling_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Inter_Amount").val(),
        _txt_Bill_Handling_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_Bill_Handling_txt_Bill_Handling_Credit_Inter_Rate").val();

    ///////////////////// GENERAL OPRATOIN For Sundry Deposits /////////////////////////////////////////////////////////////
    var _GO_Sundry_Flag = '', _txt_Sundry_Comment = '', _txt_Sundry_SectionNo = '', _txt_Sundry_Remarks = '', _txt_Sundry_Memo = '',
        _txt_Sundry_Scheme_no = '', _txt_Sundry_Debit_Code = '', _txt_Sundry_Debit_Curr = '', _txt_Sundry_Debit_Amt = '', _txt_Sundry_Debit_Cust = '', _txt_Sundry_Debit_Cust_Name = '', _txt_Sundry_Debit_Cust_AcCode = '',
        _txt_Sundry_Debit_Cust_AccNo = '', _txt_Sundry_Debit_Cust_AcCode_Name = '', _txt_Sundry_Debit_Exch_Rate = '', _txt_Sundry_Debit_Exch_CCY = '', _txt_Sundry_Debit_FUND = '', _txt_Sundry_Debit_Check_No = '',
        _txt_Sundry_Debit_Available = '', _txt_Sundry_Debit_AdPrint = '', _txt_Sundry_Debit_Details = '', _txt_Sundry_Debit_Entity = '', _txt_Sundry_Debit_Division = '',
        _txt_Sundry_Debit_Inter_Amount = '', _txt_Sundry_Debit_Inter_Rate = '', _txt_Sundry_Credit_Code = '', _txt_Sundry_Credit_Curr = '', _txt_Sundry_Credit_Amt = '',
        _txt_Sundry_Credit_Cust = '', _txt_Sundry_Credit_Cust_Name = '', _txt_Sundry_Credit_Cust_AcCode = '', _txt_Sundry_Credit_Cust_AcCode_Name = '', _txt_Sundry_Credit_Cust_AccNo = '', _txt_Sundry_Credit_Exch_Rate = '', _txt_Sundry_Credit_Exch_Curr = '',
        _txt_Sundry_Credit_FUND = '', _txt_Sundry_Credit_Check_No = '', _txt_Sundry_Credit_Available = '', _txt_Sundry_Credit_AdPrint = '', _txt_Sundry_Credit_Details = '',
        _txt_Sundry_Credit_Entity = '', _txt_Sundry_Credit_Division = '', _txt_Sundry_Credit_Inter_Amount = '', _txt_Sundry_Credit_Inter_Rate = '';

    ///////////////////// GENERAL OPRATOIN For Comm_Recieved /////////////////////////////////////////////////////////////
    var _GO_Comm_Recieved_Flag = '', _txt_Comm_Recieved_Ref_No = '', _txt_Comm_Recieved_Comment = '', _txt_Comm_Recieved_SectionNo = '', _txt_Comm_Recieved_Remarks = '', _txt_Comm_Recieved_Memo = '',
        _txt_Comm_Recieved_Scheme_no = '', _txt_Comm_Recieved_Debit_Code = '', _txt_Comm_Recieved_Debit_Curr = '', _txt_Comm_Recieved_Debit_Amt = '', _txt_Comm_Recieved_Debit_Cust = '', _txt_Comm_Recieved_Debit_Cust_Name = '', _txt_Comm_Recieved_Debit_Cust_AcCode = '',
        _txt_Comm_Recieved_Debit_Cust_AccNo = '', _txt_Comm_Recieved_Debit_Cust_AcCode_Name = '', _txt_Comm_Recieved_Debit_Exch_Rate = '', _txt_Comm_Recieved_Debit_Exch_CCY = '', _txt_Comm_Recieved_Debit_FUND = '', _txt_Comm_Recieved_Debit_Check_No = '',
        _txt_Comm_Recieved_Debit_Available = '', _txt_Comm_Recieved_Debit_AdPrint = '', _txt_Comm_Recieved_Debit_Details = '', _txt_Comm_Recieved_Debit_Entity = '', _txt_Comm_Recieved_Debit_Division = '',
        _txt_Comm_Recieved_Debit_Inter_Amount = '', _txt_Comm_Recieved_Debit_Inter_Rate = '', _txt_Comm_Recieved_Credit_Code = '', _txt_Comm_Recieved_Credit_Curr = '', _txt_Comm_Recieved_Credit_Amt = '',
        _txt_Comm_Recieved_Credit_Cust = '', _txt_Comm_Recieved_Credit_Cust_Name = '', _txt_Comm_Recieved_Credit_Cust_AcCode = '', _txt_Comm_Recieved_Credit_Cust_AcCode_Name = '', _txt_Comm_Recieved_Credit_Cust_AccNo = '', _txt_Comm_Recieved_Credit_Exch_Rate = '', _txt_Comm_Recieved_Credit_Exch_Curr = '',
        _txt_Comm_Recieved_Credit_FUND = '', _txt_Comm_Recieved_Credit_Check_No = '', _txt_Comm_Recieved_Credit_Available = '', _txt_Comm_Recieved_Credit_AdPrint = '', _txt_Comm_Recieved_Credit_Details = '',
        _txt_Comm_Recieved_Credit_Entity = '', _txt_Comm_Recieved_Credit_Division = '', _txt_Comm_Recieved_Credit_Inter_Amount = '', _txt_Comm_Recieved_Credit_Inter_Rate = '';

    ///////////////////// GENERAL OPRATOIN For Change Branch /////////////////////////////////////////////////////////////
    var _GO_Acc_Change_Flag = '', _txt_Acc_Change_Ref_No = '', _txt_Acc_Change_Comment = '', _txt_Acc_Change_SectionNo = '', _txt_Acc_Change_Remarks = '', _txt_Acc_Change_Memo = '',
        _txt_Acc_Change_Scheme_no = '', _txt_Acc_Change_Debit_Code = '', _txt_Acc_Change_Debit_Curr = '', _txt_Acc_Change_Debit_Amt = '', _txt_Acc_Change_Debit_Cust = '', _txt_Acc_Change_Debit_Cust_Name = '', _txt_Acc_Change_Debit_Cust_AcCode = '',
        _txt_Acc_Change_Debit_Cust_AccNo = '', _txt_Acc_Change_Debit_Cust_AcCode_Name = '', _txt_Acc_Change_Debit_Exch_Rate = '', _txt_Acc_Change_Debit_Exch_CCY = '', _txt_Acc_Change_Debit_FUND = '', _txt_Acc_Change_Debit_Check_No = '',
        _txt_Acc_Change_Debit_Available = '', _txt_Acc_Change_Debit_AdPrint = '', _txt_Acc_Change_Debit_Details = '', _txt_Acc_Change_Debit_Entity = '', _txt_Acc_Change_Debit_Division = '',
        _txt_Acc_Change_Debit_Inter_Amount = '', _txt_Acc_Change_Debit_Inter_Rate = '', _txt_Acc_Change_Credit_Code = '', _txt_Acc_Change_Credit_Curr = '', _txt_Acc_Change_Credit_Amt = '',
        _txt_Acc_Change_Credit_Cust = '', _txt_Acc_Change_Credit_Cust_Name = '', _txt_Acc_Change_Credit_Cust_AcCode = '', _txt_Acc_Change_Credit_Cust_AcCode_Name = '', _txt_Acc_Change_Credit_Cust_AccNo = '', _txt_Acc_Change_Credit_Exch_Rate = '', _txt_Acc_Change_Credit_Exch_Curr = '',
        _txt_Acc_Change_Credit_FUND = '', _txt_Acc_Change_Credit_Check_No = '', _txt_Acc_Change_Credit_Available = '', _txt_Acc_Change_Credit_AdPrint = '', _txt_Acc_Change_Credit_Details = '',
        _txt_Acc_Change_Credit_Entity = '', _txt_Acc_Change_Credit_Division = '', _txt_Acc_Change_Credit_Inter_Amount = '', _txt_Acc_Change_Credit_Inter_Rate = '';


    if (_Doc_Scrutiny == "2") {
        ///////////////////// GENERAL OPRATOIN For Sundry Deposits ///////////////////////////////////////////////////////////
        //_GO_Sundry_Flag = 'TabContainerMain_tbDocumentGO_Sundry_cb_GO_Sundry_Flag';

        if ($("#TabContainerMain_tbDocumentGO_Sundry_cb_GO_Sundry_Flag").is(':checked')) {
            _GO_Sundry_Flag = 'Y';
            _txt_Sundry_Comment = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Comment").val();
            _txt_Sundry_SectionNo = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_SectionNo").val();
            _txt_Sundry_Remarks = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Remarks").val();
            _txt_Sundry_Memo = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Memo").val();
            _txt_Sundry_Scheme_no = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Scheme_no").val();
            _txt_Sundry_Debit_Code = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Code").val();
            _txt_Sundry_Debit_Curr = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Curr").val();
            _txt_Sundry_Debit_Amt = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Amt").val();
            _txt_Sundry_Debit_Cust = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Cust").val();
            _txt_Sundry_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Cust_Name").val();
            _txt_Sundry_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Cust_AcCode").val();
            _txt_Sundry_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Cust_AccNo").val();
            _txt_Sundry_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Cust_AcCode_Name").val();
            _txt_Sundry_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Exch_Rate").val();
            _txt_Sundry_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Exch_CCY").val();
            _txt_Sundry_Debit_FUND = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_FUND").val();
            _txt_Sundry_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Check_No").val();
            _txt_Sundry_Debit_Available = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Available").val();
            _txt_Sundry_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_AdPrint").val();
            _txt_Sundry_Debit_Details = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Details").val();
            _txt_Sundry_Debit_Entity = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Entity").val();
            _txt_Sundry_Debit_Division = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Division").val();
            _txt_Sundry_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Inter_Amount").val();
            _txt_Sundry_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Debit_Inter_Rate").val();
            _txt_Sundry_Credit_Code = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Code").val();
            _txt_Sundry_Credit_Curr = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Curr").val();
            _txt_Sundry_Credit_Amt = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Amt").val();
            _txt_Sundry_Credit_Cust = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Cust").val();
            _txt_Sundry_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Cust_Name").val();
            _txt_Sundry_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Cust_AcCode").val();
            _txt_Sundry_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Cust_AcCode_Name").val();
            _txt_Sundry_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Cust_AccNo").val();
            _txt_Sundry_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Exch_Rate").val();
            _txt_Sundry_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Exch_Curr").val();
            _txt_Sundry_Credit_FUND = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_FUND").val();
            _txt_Sundry_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Check_No").val();
            _txt_Sundry_Credit_Available = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Available").val();
            _txt_Sundry_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_AdPrint").val();
            _txt_Sundry_Credit_Details = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Details").val();
            _txt_Sundry_Credit_Entity = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Entity").val();
            _txt_Sundry_Credit_Division = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Division").val();
            _txt_Sundry_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Inter_Amount").val();
            _txt_Sundry_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_Sundry_txt_Sundry_Credit_Inter_Rate").val();

        }



        ///////////////////// GENERAL OPRATOIN For Comm_Recieved /////////////////////////////////////////////////////////////
        //_GO_Comm_Recieved_Flag = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_cb_GO_Comm_Recieved_Flag").val();
        if (_FLC_ILC == "ILC") {
            if ($("#TabContainerMain_tbDocumentGO_Comm_Recieved_cb_GO_Comm_Recieved_Flag").is(':checked')) {
                _GO_Comm_Recieved_Flag = 'Y';
                _txt_Comm_Recieved_Comment = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Comment").val();
                _txt_Comm_Recieved_SectionNo = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_SectionNo").val();
                _txt_Comm_Recieved_Remarks = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Remarks").val();
                _txt_Comm_Recieved_Memo = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Memo").val();
                _txt_Comm_Recieved_Scheme_no = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Scheme_no").val();
                _txt_Comm_Recieved_Debit_Code = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Code").val();
                _txt_Comm_Recieved_Debit_Curr = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Curr").val();
                _txt_Comm_Recieved_Debit_Amt = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Amt").val();
                _txt_Comm_Recieved_Debit_Cust = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Cust").val();
                _txt_Comm_Recieved_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Cust_Name").val();
                _txt_Comm_Recieved_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Cust_AcCode").val();
                _txt_Comm_Recieved_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Cust_AccNo").val();
                _txt_Comm_Recieved_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Cust_AcCode_Name").val();
                _txt_Comm_Recieved_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Exch_Rate").val();
                _txt_Comm_Recieved_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Exch_CCY").val();
                _txt_Comm_Recieved_Debit_FUND = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_FUND").val();
                _txt_Comm_Recieved_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Check_No").val();
                _txt_Comm_Recieved_Debit_Available = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Available").val();
                _txt_Comm_Recieved_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_AdPrint").val();
                _txt_Comm_Recieved_Debit_Details = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Details").val();
                _txt_Comm_Recieved_Debit_Entity = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Entity").val();
                _txt_Comm_Recieved_Debit_Division = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Division").val();
                _txt_Comm_Recieved_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Inter_Amount").val();
                _txt_Comm_Recieved_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Debit_Inter_Rate").val();
                _txt_Comm_Recieved_Credit_Code = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Code").val();
                _txt_Comm_Recieved_Credit_Curr = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Curr").val();
                _txt_Comm_Recieved_Credit_Amt = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Amt").val();
                _txt_Comm_Recieved_Credit_Cust = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Cust").val();
                _txt_Comm_Recieved_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Cust_Name").val();
                _txt_Comm_Recieved_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Cust_AcCode").val();
                _txt_Comm_Recieved_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Cust_AcCode_Name").val();
                _txt_Comm_Recieved_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Cust_AccNo").val();
                _txt_Comm_Recieved_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Exch_Rate").val();
                _txt_Comm_Recieved_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Exch_Curr").val();
                _txt_Comm_Recieved_Credit_FUND = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_FUND").val();
                _txt_Comm_Recieved_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Check_No").val();
                _txt_Comm_Recieved_Credit_Available = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Available").val();
                _txt_Comm_Recieved_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_AdPrint").val();
                _txt_Comm_Recieved_Credit_Details = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Details").val();
                _txt_Comm_Recieved_Credit_Entity = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Entity").val();
                _txt_Comm_Recieved_Credit_Division = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Division").val();
                _txt_Comm_Recieved_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Inter_Amount").val();
                _txt_Comm_Recieved_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_Comm_Recieved_txt_Comm_Recieved_Credit_Inter_Rate").val();
            }
        }

        ///////////////////// GENERAL OPRATOIN For Branch change /////////////////////////////////////////////////////////////
        if (_BranchName != 'Mumbai') {
            
            if ($("#TabContainerMain_tbDocumentGo_Acc_Change_cb_GO_Acc_Change_Flag").is(':checked')) {
                _GO_Acc_Change_Flag = 'Y';
                _txt_Acc_Change_Comment = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Comment").val();
                _txt_Acc_Change_SectionNo = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_SectionNo").val();
                _txt_Acc_Change_Remarks = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Remarks").val();
                _txt_Acc_Change_Memo = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Memo").val();
                _txt_Acc_Change_Scheme_no = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Scheme_no").val();
                _txt_Acc_Change_Debit_Code = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Code").val();
                _txt_Acc_Change_Debit_Curr = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Curr").val();
                _txt_Acc_Change_Debit_Amt = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Amt").val();
                _txt_Acc_Change_Debit_Cust = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Cust").val();
                _txt_Acc_Change_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Cust_Name").val();
                _txt_Acc_Change_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Cust_AcCode").val();
                _txt_Acc_Change_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Cust_AccNo").val();
                _txt_Acc_Change_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Cust_AcCode_Name").val();
                _txt_Acc_Change_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Exch_Rate").val();
                _txt_Acc_Change_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Exch_CCY").val();
                _txt_Acc_Change_Debit_FUND = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_FUND").val();
                _txt_Acc_Change_Debit_Check_No = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Check_No").val();
                _txt_Acc_Change_Debit_Available = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Available").val();
                _txt_Acc_Change_Debit_AdPrint = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_AdPrint").val();
                _txt_Acc_Change_Debit_Details = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Details").val();
                _txt_Acc_Change_Debit_Entity = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Entity").val();
                _txt_Acc_Change_Debit_Division = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Division").val();
                _txt_Acc_Change_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Inter_Amount").val();
                _txt_Acc_Change_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Debit_Inter_Rate").val();
                _txt_Acc_Change_Credit_Code = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Code").val();
                _txt_Acc_Change_Credit_Curr = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Curr").val();
                _txt_Acc_Change_Credit_Amt = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Amt").val();
                _txt_Acc_Change_Credit_Cust = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Cust").val();
                _txt_Acc_Change_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Cust_Name").val();
                _txt_Acc_Change_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Cust_AcCode").val();
                _txt_Acc_Change_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Cust_AcCode_Name").val();
                _txt_Acc_Change_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Cust_AccNo").val();
                _txt_Acc_Change_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Exch_Rate").val();
                _txt_Acc_Change_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Exch_Curr").val();
                _txt_Acc_Change_Credit_FUND = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_FUND").val();
                _txt_Acc_Change_Credit_Check_No = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Check_No").val();
                _txt_Acc_Change_Credit_Available = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Available").val();
                _txt_Acc_Change_Credit_AdPrint = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_AdPrint").val();
                _txt_Acc_Change_Credit_Details = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Details").val();
                _txt_Acc_Change_Credit_Entity = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Entity").val();
                _txt_Acc_Change_Credit_Division = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Division").val();
                _txt_Acc_Change_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Inter_Amount").val();
                _txt_Acc_Change_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGo_Acc_Change_txt_Acc_Change_Credit_Inter_Rate").val();
            }
        }
    }

    var _rdb_swift_None = '', _rdb_swift_103 = '', _rdb_swift_200 = '', _rdb_swift_202 = '', _rdb_swift_R42 = '';
    if ($("#TabContainerMain_tbSwift_rdb_swift_None").is(':checked')) {
        _rdb_swift_None = 'Y';
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_200").is(':checked')) {
        _rdb_swift_200 = 'Y';
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_R42").is(':checked')) {
        _rdb_swift_R42 = 'Y';
    }

    //MT 200
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

    $.ajax({
        type: "POST",
        url: "TF_IMP_Settlement_Maker.aspx/AddUpdateSettlement",
        data: '{hdnUserName: "' + hdnUserName + '", _BranchName:"' + _BranchName + '", _txtDocNo: "' + _txtDocNo + '", _DocType: "' + _DocType + '", _FLC_ILC: "' + _FLC_ILC + '", _Doc_Scrutiny: "' + _Doc_Scrutiny + '", _Document_Curr: "' + _Document_Curr +

            '", _txt_Doc_Customer_ID: "' + _txt_Doc_Customer_ID + '", _txtValueDate: "' + _txtValueDate + '", _txt_Doc_Comment_Code: "' + _txt_Doc_Comment_Code + '", _txt_Doc_Maturity: "' + _txt_Doc_Maturity +
            '", _txt_Doc_Settlement_for_Cust: "' + _txt_Doc_Settlement_for_Cust + '", _txt_Doc_Settlement_For_Bank: "' + _txt_Doc_Settlement_For_Bank +
            '", _txt_Doc_Interest_From: "' + _txt_Doc_Interest_From + '", _txt_Doc_Discount: "' + _txt_Doc_Discount +
            '", _txt_Doc_Interest_To: "' + _txt_Doc_Interest_To + '", _txt_Doc_No_Of_Days: "' + _txt_Doc_No_Of_Days +
			'", _txt_Doc_Rate: "' + _txt_Doc_Rate + '", _txt_Doc_Amount: "' + _txt_Doc_Amount + '", _txt_Doc_Overdue_Interestrate: "' + _txt_Doc_Overdue_Interestrate +
            '", _txt_Doc_Oveduenoofdays: "' + _txt_Doc_Oveduenoofdays + '", _txt_Doc_Overdueamount: "' + _txt_Doc_Overdueamount + '", txt_Doc_Attn: "' + txt_Doc_Attn +

        //// Import Accounting
            '", _txt_DiscAmt:"' + _txt_DiscAmt +
            '", _txtPrinc_matu: "' + _txtPrinc_matu + '", _txtPrinc_lump: "' + _txtPrinc_lump + '", _txtprinc_Contract_no: "' + _txtprinc_Contract_no + '", _txt_Princ_Ex_Curr: "' + _txt_Princ_Ex_Curr +
            '", _txtprinc_Ex_rate: "' + _txtprinc_Ex_rate + '", _txtprinc_Intnl_Ex_rate: "' + _txtprinc_Intnl_Ex_rate +
			'", _txtInterest_matu: "' + _txtInterest_matu + '", _txtInterest_lump: "' + _txtInterest_lump +
            '", _txtInterest_Contract_no: "' + _txtInterest_Contract_no + '", _txt_interest_Ex_Curr: "' + _txt_interest_Ex_Curr + '", _txtInterest_Ex_rate: "' + _txtInterest_Ex_rate + '", _txtInterest_Intnl_Ex_rate: "' + _txtInterest_Intnl_Ex_rate +
            '", _txtCommission_matu: "' + _txtCommission_matu + '", _txtCommission_lump: "' + _txtCommission_lump + '", _txtCommission_Contract_no: "' + _txtCommission_Contract_no + '", _txt_Commission_Ex_Curr: "' + _txt_Commission_Ex_Curr +
            '", _txtCommission_Ex_rate: "' + _txtCommission_Ex_rate + '", _txtCommission_Intnl_Ex_rate: "' + _txtCommission_Intnl_Ex_rate +
			'", _txtTheir_Commission_matu: "' + _txtTheir_Commission_matu + '", _txtTheir_Commission_lump: "' + _txtTheir_Commission_lump +
            '", _txtTheir_Commission_Contract_no: "' + _txtTheir_Commission_Contract_no + '", _txt_Their_Commission_Ex_Curr: "' + _txt_Their_Commission_Ex_Curr + '", _txtTheir_Commission_Ex_rate: "' + _txtTheir_Commission_Ex_rate +
            '", _txtTheir_Commission_Intnl_Ex_rate: "' + _txtTheir_Commission_Intnl_Ex_rate +

			'", _txt_CR_Code: "' + _txt_CR_Code + '", _txt_CR_AC_Short_Name: "' + _txt_CR_AC_Short_Name +
            '", _txt_CR_Cust_abbr: "' + _txt_CR_Cust_abbr + '", _txt_CR_Cust_Acc: "' + _txt_CR_Cust_Acc + '", _txt_CR_Acceptance_Curr: "' + _txt_CR_Acceptance_Curr +
            '", _txt_CR_Acceptance_amt: "' + _txt_CR_Acceptance_amt + '", _txt_CR_Acceptance_payer: "' + _txt_CR_Acceptance_payer +
			'", _txt_CR_Interest_Curr: "' + _txt_CR_Interest_Curr +
            '", _txt_CR_Interest_amt: "' + _txt_CR_Interest_amt + '", _txt_CR_Interest_payer: "' + _txt_CR_Interest_payer +
			'", _txt_CR_Accept_Commission_Curr: "' + _txt_CR_Accept_Commission_Curr +
            '", _txt_CR_Accept_Commission_amt: "' + _txt_CR_Accept_Commission_amt + '", _txt_CR_Accept_Commission_Payer: "' + _txt_CR_Accept_Commission_Payer +
			'", _txt_CR_Pay_Handle_Commission_Curr: "' + _txt_CR_Pay_Handle_Commission_Curr +
            '", _txt_CR_Pay_Handle_Commission_amt: "' + _txt_CR_Pay_Handle_Commission_amt + '", _txt_CR_Pay_Handle_Commission_Payer: "' + _txt_CR_Pay_Handle_Commission_Payer +
			'", _txt_CR_Others_Curr: "' + _txt_CR_Others_Curr +
            '", _txt_CR_Others_amt: "' + _txt_CR_Others_amt + '", _txt_CR_Others_Payer: "' + _txt_CR_Others_Payer + '", _txt_CR_Their_Commission_Curr: "' + _txt_CR_Their_Commission_Curr +

            '", _txt_CR_Their_Commission_amt: "' + _txt_CR_Their_Commission_amt + '", _txt_CR_Their_Commission_Payer: "' + _txt_CR_Their_Commission_Payer +
			'", _txt_DR_Code: "' + _txt_DR_Code + '", _txt_DR_AC_Short_Name: "' + _txt_DR_AC_Short_Name +
            '", _txt_DR_Cust_abbr: "' + _txt_DR_Cust_abbr + '", _txt_DR_Cust_Acc: "' + _txt_DR_Cust_Acc + '", _txt_DR_Cur_Acc_Curr: "' + _txt_DR_Cur_Acc_Curr +
            '", _txt_DR_Cur_Acc_amt: "' + _txt_DR_Cur_Acc_amt + '", _txt_DR_Cur_Acc_payer: "' + _txt_DR_Cur_Acc_payer + '", _txt_DR_Cur_Acc_Curr2: "' + _txt_DR_Cur_Acc_Curr2 +
            '", _txt_DR_Cur_Acc_amt2: "' + _txt_DR_Cur_Acc_amt2 + '", _txt_DR_Cur_Acc_payer2: "' + _txt_DR_Cur_Acc_payer2 + '", _txt_DR_Cur_Acc_Curr3: "' + _txt_DR_Cur_Acc_Curr3 +
            '", _txt_DR_Cur_Acc_amt3: "' + _txt_DR_Cur_Acc_amt3 + '", _txt_DR_Cur_Acc_payer3: "' + _txt_DR_Cur_Acc_payer3 +

        //////// GENERAL OPRATOIN For Bill_Handling	

            '", _GO_Bill_Handling_Flag: "' + _GO_Bill_Handling_Flag + '", _txt_Bill_Handling_Comment: "' + _txt_Bill_Handling_Comment + '", _txt_Bill_Handling_SectionNo: "' + _txt_Bill_Handling_SectionNo +
            '", _txt_Bill_Handling_Remarks: "' + _txt_Bill_Handling_Remarks + '", _txt_Bill_Handling_Memo: "' + _txt_Bill_Handling_Memo + '", _txt_Bill_Handling_Scheme_no: "' + _txt_Bill_Handling_Scheme_no +
            '", _txt_Bill_Handling_Debit_Code: "' + _txt_Bill_Handling_Debit_Code +
            '", _txt_Bill_Handling_Debit_Curr: "' + _txt_Bill_Handling_Debit_Curr + '", _txt_Bill_Handling_Debit_Amt: "' + _txt_Bill_Handling_Debit_Amt +
            '", _txt_Bill_Handling_Debit_Cust: "' + _txt_Bill_Handling_Debit_Cust + '", _txt_Bill_Handling_Debit_Cust_Name: "' + _txt_Bill_Handling_Debit_Cust_Name + '", _txt_Bill_Handling_Debit_Cust_AcCode: "' + _txt_Bill_Handling_Debit_Cust_AcCode +

            '", _txt_Bill_Handling_Debit_Cust_AccNo: "' + _txt_Bill_Handling_Debit_Cust_AccNo + '", _txt_Bill_Handling_Debit_Cust_AcCode_Name: "' + _txt_Bill_Handling_Debit_Cust_AcCode_Name + '", _txt_Bill_Handling_Debit_Exch_Rate: "' + _txt_Bill_Handling_Debit_Exch_Rate +
            '", _txt_Bill_Handling_Debit_Exch_CCY: "' + _txt_Bill_Handling_Debit_Exch_CCY + '", _txt_Bill_Handling_Debit_FUND: "' + _txt_Bill_Handling_Debit_FUND +
            '", _txt_Bill_Handling_Debit_Check_No: "' + _txt_Bill_Handling_Debit_Check_No + '", _txt_Bill_Handling_Debit_Available: "' + _txt_Bill_Handling_Debit_Available +
            '", _txt_Bill_Handling_Debit_AdPrint: "' + _txt_Bill_Handling_Debit_AdPrint + '", _txt_Bill_Handling_Debit_Details: "' + _txt_Bill_Handling_Debit_Details + '", _txt_Bill_Handling_Debit_Entity: "' + _txt_Bill_Handling_Debit_Entity +

            '", _txt_Bill_Handling_Debit_Division: "' + _txt_Bill_Handling_Debit_Division + '", _txt_Bill_Handling_Debit_Inter_Amount: "' + _txt_Bill_Handling_Debit_Inter_Amount + '", _txt_Bill_Handling_Debit_Inter_Rate: "' + _txt_Bill_Handling_Debit_Inter_Rate +
            '", _txt_Bill_Handling_Credit_Code: "' + _txt_Bill_Handling_Credit_Code + '", _txt_Bill_Handling_Credit_Curr: "' + _txt_Bill_Handling_Credit_Curr + '", _txt_Bill_Handling_Credit_Amt: "' + _txt_Bill_Handling_Credit_Amt +

            '", _txt_Bill_Handling_Credit_Cust: "' + _txt_Bill_Handling_Credit_Cust + '", _txt_Bill_Handling_Credit_Cust_Name: "' + _txt_Bill_Handling_Credit_Cust_Name + '", _txt_Bill_Handling_Credit_Cust_AcCode: "' + _txt_Bill_Handling_Credit_Cust_AcCode +
            '", _txt_Bill_Handling_Credit_Cust_AcCode_Name: "' + _txt_Bill_Handling_Credit_Cust_AcCode_Name +
            '", _txt_Bill_Handling_Credit_Cust_AccNo: "' + _txt_Bill_Handling_Credit_Cust_AccNo + '", _txt_Bill_Handling_Credit_Exch_Rate: "' + _txt_Bill_Handling_Credit_Exch_Rate +
            '", _txt_Bill_Handling_Credit_Exch_Curr: "' + _txt_Bill_Handling_Credit_Exch_Curr + '", _txt_Bill_Handling_Credit_FUND: "' + _txt_Bill_Handling_Credit_FUND + '", _txt_Bill_Handling_Credit_Check_No: "' + _txt_Bill_Handling_Credit_Check_No +

            '", _txt_Bill_Handling_Credit_Available: "' + _txt_Bill_Handling_Credit_Available + '", _txt_Bill_Handling_Credit_AdPrint: "' + _txt_Bill_Handling_Credit_AdPrint + '", _txt_Bill_Handling_Credit_Details: "' + _txt_Bill_Handling_Credit_Details +
            '", _txt_Bill_Handling_Credit_Entity: "' + _txt_Bill_Handling_Credit_Entity + '", _txt_Bill_Handling_Credit_Division: "' + _txt_Bill_Handling_Credit_Division +
            '", _txt_Bill_Handling_Credit_Inter_Amount: "' + _txt_Bill_Handling_Credit_Inter_Amount + '", _txt_Bill_Handling_Credit_Inter_Rate: "' + _txt_Bill_Handling_Credit_Inter_Rate +


        /////////// GENERAL OPRATOIN For Sundry Deposits

            '", _GO_Sundry_Flag: "' + _GO_Sundry_Flag + '", _txt_Sundry_Comment: "' + _txt_Sundry_Comment + '", _txt_Sundry_SectionNo: "' + _txt_Sundry_SectionNo + '", _txt_Sundry_Remarks: "' + _txt_Sundry_Remarks +
            '", _txt_Sundry_Memo: "' + _txt_Sundry_Memo + '", _txt_Sundry_Scheme_no: "' + _txt_Sundry_Scheme_no + '", _txt_Sundry_Debit_Code: "' + _txt_Sundry_Debit_Code + '", _txt_Sundry_Debit_Curr: "' + _txt_Sundry_Debit_Curr + '", _txt_Sundry_Debit_Amt: "' + _txt_Sundry_Debit_Amt +
            '", _txt_Sundry_Debit_Cust: "' + _txt_Sundry_Debit_Cust + '", _txt_Sundry_Debit_Cust_Name: "' + _txt_Sundry_Debit_Cust_Name + '", _txt_Sundry_Debit_Cust_AcCode: "' + _txt_Sundry_Debit_Cust_AcCode + '", _txt_Sundry_Debit_Cust_AccNo: "' + _txt_Sundry_Debit_Cust_AccNo + '", _txt_Sundry_Debit_Cust_AcCode_Name: "' + _txt_Sundry_Debit_Cust_AcCode_Name +
            '", _txt_Sundry_Debit_Exch_Rate: "' + _txt_Sundry_Debit_Exch_Rate + '", _txt_Sundry_Debit_Exch_CCY: "' + _txt_Sundry_Debit_Exch_CCY + '", _txt_Sundry_Debit_FUND: "' + _txt_Sundry_Debit_FUND + '", _txt_Sundry_Debit_Check_No: "' + _txt_Sundry_Debit_Check_No + '", _txt_Sundry_Debit_Available: "' + _txt_Sundry_Debit_Available +
            '", _txt_Sundry_Debit_AdPrint: "' + _txt_Sundry_Debit_AdPrint + '", _txt_Sundry_Debit_Details: "' + _txt_Sundry_Debit_Details + '", _txt_Sundry_Debit_Entity: "' + _txt_Sundry_Debit_Entity + '", _txt_Sundry_Debit_Division: "' + _txt_Sundry_Debit_Division + '", _txt_Sundry_Debit_Inter_Amount: "' + _txt_Sundry_Debit_Inter_Amount +
            '", _txt_Sundry_Debit_Inter_Rate: "' + _txt_Sundry_Debit_Inter_Rate + '", _txt_Sundry_Credit_Code: "' + _txt_Sundry_Credit_Code + '", _txt_Sundry_Credit_Curr: "' + _txt_Sundry_Credit_Curr + '", _txt_Sundry_Credit_Amt: "' + _txt_Sundry_Credit_Amt + '", _txt_Sundry_Credit_Cust: "' + _txt_Sundry_Credit_Cust +
            '", _txt_Sundry_Credit_Cust_Name: "' + _txt_Sundry_Credit_Cust_Name + '", _txt_Sundry_Credit_Cust_AcCode: "' + _txt_Sundry_Credit_Cust_AcCode + '", _txt_Sundry_Credit_Cust_AcCode_Name: "' + _txt_Sundry_Credit_Cust_AcCode_Name + '", _txt_Sundry_Credit_Cust_AccNo: "' + _txt_Sundry_Credit_Cust_AccNo + '", _txt_Sundry_Credit_Exch_Rate: "' + _txt_Sundry_Credit_Exch_Rate +
            '", _txt_Sundry_Credit_Exch_Curr: "' + _txt_Sundry_Credit_Exch_Curr + '", _txt_Sundry_Credit_FUND: "' + _txt_Sundry_Credit_FUND + '", _txt_Sundry_Credit_Check_No: "' + _txt_Sundry_Credit_Check_No + '", _txt_Sundry_Credit_Available: "' + _txt_Sundry_Credit_Available + '", _txt_Sundry_Credit_AdPrint: "' + _txt_Sundry_Credit_AdPrint +
			'",_txt_Sundry_Credit_Details: "' + _txt_Sundry_Credit_Details +
            '",_txt_Sundry_Credit_Entity: "' + _txt_Sundry_Credit_Entity + '",_txt_Sundry_Credit_Division: "' + _txt_Sundry_Credit_Division + '",_txt_Sundry_Credit_Inter_Amount: "' + _txt_Sundry_Credit_Inter_Amount +
            '",_txt_Sundry_Credit_Inter_Rate: "' + _txt_Sundry_Credit_Inter_Rate +

        /////////// GENERAL OPRATOIN For Comm_Recieved			
			'",_GO_Comm_Recieved_Flag: "' + _GO_Comm_Recieved_Flag +
            '",_txt_Comm_Recieved_Comment: "' + _txt_Comm_Recieved_Comment +
            '",_txt_Comm_Recieved_SectionNo: "' + _txt_Comm_Recieved_SectionNo + '",_txt_Comm_Recieved_Remarks: "' + _txt_Comm_Recieved_Remarks +
            '",_txt_Comm_Recieved_Memo: "' + _txt_Comm_Recieved_Memo + '",_txt_Comm_Recieved_Scheme_no: "' + _txt_Comm_Recieved_Scheme_no + '",_txt_Comm_Recieved_Debit_Code: "' + _txt_Comm_Recieved_Debit_Code +
			'",_txt_Comm_Recieved_Debit_Curr: "' + _txt_Comm_Recieved_Debit_Curr + '",_txt_Comm_Recieved_Debit_Amt: "' + _txt_Comm_Recieved_Debit_Amt +
			'",_txt_Comm_Recieved_Debit_Cust: "' + _txt_Comm_Recieved_Debit_Cust + '",_txt_Comm_Recieved_Debit_Cust_Name: "' + _txt_Comm_Recieved_Debit_Cust_Name +
			'",_txt_Comm_Recieved_Debit_Cust_AcCode: "' + _txt_Comm_Recieved_Debit_Cust_AcCode + '",_txt_Comm_Recieved_Debit_Cust_AccNo: "' + _txt_Comm_Recieved_Debit_Cust_AccNo +
			'",_txt_Comm_Recieved_Debit_Cust_AcCode_Name: "' + _txt_Comm_Recieved_Debit_Cust_AcCode_Name + '",_txt_Comm_Recieved_Debit_Exch_Rate: "' + _txt_Comm_Recieved_Debit_Exch_Rate + '",_txt_Comm_Recieved_Debit_Exch_CCY: "' + _txt_Comm_Recieved_Debit_Exch_CCY +
			'",_txt_Comm_Recieved_Debit_FUND: "' + _txt_Comm_Recieved_Debit_FUND + '",_txt_Comm_Recieved_Debit_Check_No: "' + _txt_Comm_Recieved_Debit_Check_No +
			'",_txt_Comm_Recieved_Debit_Available: "' + _txt_Comm_Recieved_Debit_Available + '",_txt_Comm_Recieved_Debit_AdPrint: "' + _txt_Comm_Recieved_Debit_AdPrint +
			'",_txt_Comm_Recieved_Debit_Details: "' + _txt_Comm_Recieved_Debit_Details + '",_txt_Comm_Recieved_Debit_Entity: "' + _txt_Comm_Recieved_Debit_Entity +
			'",_txt_Comm_Recieved_Debit_Division: "' + _txt_Comm_Recieved_Debit_Division + '",_txt_Comm_Recieved_Debit_Inter_Amount: "' + _txt_Comm_Recieved_Debit_Inter_Amount +
			'",_txt_Comm_Recieved_Debit_Inter_Rate: "' + _txt_Comm_Recieved_Debit_Inter_Rate + '",_txt_Comm_Recieved_Credit_Code: "' + _txt_Comm_Recieved_Credit_Code +
			'",_txt_Comm_Recieved_Credit_Curr: "' + _txt_Comm_Recieved_Credit_Curr + '",_txt_Comm_Recieved_Credit_Amt: "' + _txt_Comm_Recieved_Credit_Amt +
			'",_txt_Comm_Recieved_Credit_Cust: "' + _txt_Comm_Recieved_Credit_Cust + '",_txt_Comm_Recieved_Credit_Cust_Name: "' + _txt_Comm_Recieved_Credit_Cust_Name +
			'",_txt_Comm_Recieved_Credit_Cust_AcCode: "' + _txt_Comm_Recieved_Credit_Cust_AcCode + '",_txt_Comm_Recieved_Credit_Cust_AcCode_Name: "' + _txt_Comm_Recieved_Credit_Cust_AcCode_Name +
			'",_txt_Comm_Recieved_Credit_Cust_AccNo: "' + _txt_Comm_Recieved_Credit_Cust_AccNo + '",_txt_Comm_Recieved_Credit_Exch_Rate: "' + _txt_Comm_Recieved_Credit_Exch_Rate +
			'",_txt_Comm_Recieved_Credit_Exch_Curr: "' + _txt_Comm_Recieved_Credit_Exch_Curr + '",_txt_Comm_Recieved_Credit_FUND: "' + _txt_Comm_Recieved_Credit_FUND +
			'",_txt_Comm_Recieved_Credit_Check_No: "' + _txt_Comm_Recieved_Credit_Check_No + '",_txt_Comm_Recieved_Credit_Available: "' + _txt_Comm_Recieved_Credit_Available +
			'",_txt_Comm_Recieved_Credit_AdPrint: "' + _txt_Comm_Recieved_Credit_AdPrint + '",_txt_Comm_Recieved_Credit_Details: "' + _txt_Comm_Recieved_Credit_Details +
			'",_txt_Comm_Recieved_Credit_Entity: "' + _txt_Comm_Recieved_Credit_Entity + '",_txt_Comm_Recieved_Credit_Division: "' + _txt_Comm_Recieved_Credit_Division +
			'",_txt_Comm_Recieved_Credit_Inter_Amount: "' + _txt_Comm_Recieved_Credit_Inter_Amount + '", _txt_Comm_Recieved_Credit_Inter_Rate: "' + _txt_Comm_Recieved_Credit_Inter_Rate +

        /////////// GENERAL OPRATOIN For Change Branch	
			'", _GO_Acc_Change_Flag: "' + _GO_Acc_Change_Flag + '", _txt_Acc_Change_Comment: "' + _txt_Acc_Change_Comment +
			'", _txt_Acc_Change_SectionNo: "' + _txt_Acc_Change_SectionNo + '", _txt_Acc_Change_Remarks: "' + _txt_Acc_Change_Remarks + '", _txt_Acc_Change_Memo: "' + _txt_Acc_Change_Memo + '", _txt_Acc_Change_Scheme_no: "' + _txt_Acc_Change_Scheme_no + '", _txt_Acc_Change_Debit_Code: "' + _txt_Acc_Change_Debit_Code +
			'", _txt_Acc_Change_Debit_Curr: "' + _txt_Acc_Change_Debit_Curr + '", _txt_Acc_Change_Debit_Amt: "' + _txt_Acc_Change_Debit_Amt + '", _txt_Acc_Change_Debit_Cust: "' + _txt_Acc_Change_Debit_Cust + '", _txt_Acc_Change_Debit_Cust_Name: "' + _txt_Acc_Change_Debit_Cust_Name + '", _txt_Acc_Change_Debit_Cust_AcCode: "' + _txt_Acc_Change_Debit_Cust_AcCode +
			'", _txt_Acc_Change_Debit_Cust_AccNo: "' + _txt_Acc_Change_Debit_Cust_AccNo + '", _txt_Acc_Change_Debit_Cust_AcCode_Name: "' + _txt_Acc_Change_Debit_Cust_AcCode_Name + '", _txt_Acc_Change_Debit_Exch_Rate: "' + _txt_Acc_Change_Debit_Exch_Rate + '", _txt_Acc_Change_Debit_Exch_CCY: "' + _txt_Acc_Change_Debit_Exch_CCY + '", _txt_Acc_Change_Debit_FUND: "' + _txt_Acc_Change_Debit_FUND +
			'", _txt_Acc_Change_Debit_Check_No: "' + _txt_Acc_Change_Debit_Check_No + '", _txt_Acc_Change_Debit_Available: "' + _txt_Acc_Change_Debit_Available + '", _txt_Acc_Change_Debit_AdPrint: "' + _txt_Acc_Change_Debit_AdPrint + '", _txt_Acc_Change_Debit_Details: "' + _txt_Acc_Change_Debit_Details + '", _txt_Acc_Change_Debit_Entity: "' + _txt_Acc_Change_Debit_Entity +
			'", _txt_Acc_Change_Debit_Division: "' + _txt_Acc_Change_Debit_Division + '", _txt_Acc_Change_Debit_Inter_Amount: "' + _txt_Acc_Change_Debit_Inter_Amount + '", _txt_Acc_Change_Debit_Inter_Rate: "' + _txt_Acc_Change_Debit_Inter_Rate + '", _txt_Acc_Change_Credit_Code: "' + _txt_Acc_Change_Credit_Code + '", _txt_Acc_Change_Credit_Curr: "' + _txt_Acc_Change_Credit_Curr +
			'", _txt_Acc_Change_Credit_Amt: "' + _txt_Acc_Change_Credit_Amt + '", _txt_Acc_Change_Credit_Cust: "' + _txt_Acc_Change_Credit_Cust + '", _txt_Acc_Change_Credit_Cust_Name: "' + _txt_Acc_Change_Credit_Cust_Name + '", _txt_Acc_Change_Credit_Cust_AcCode: "' + _txt_Acc_Change_Credit_Cust_AcCode + '", _txt_Acc_Change_Credit_Cust_AcCode_Name: "' + _txt_Acc_Change_Credit_Cust_AcCode_Name +

			'",_txt_Acc_Change_Credit_Cust_AccNo: "' + _txt_Acc_Change_Credit_Cust_AccNo + '",_txt_Acc_Change_Credit_Exch_Rate: "' + _txt_Acc_Change_Credit_Exch_Rate +
			'",_txt_Acc_Change_Credit_Exch_Curr: "' + _txt_Acc_Change_Credit_Exch_Curr + '",_txt_Acc_Change_Credit_FUND: "' + _txt_Acc_Change_Credit_FUND +
			'",_txt_Acc_Change_Credit_Check_No: "' + _txt_Acc_Change_Credit_Check_No + '",_txt_Acc_Change_Credit_Available: "' + _txt_Acc_Change_Credit_Available +
			'",_txt_Acc_Change_Credit_AdPrint: "' + _txt_Acc_Change_Credit_AdPrint + '",_txt_Acc_Change_Credit_Details: "' + _txt_Acc_Change_Credit_Details +

			'",_txt_Acc_Change_Credit_Entity: "' + _txt_Acc_Change_Credit_Entity + '",_txt_Acc_Change_Credit_Division: "' + _txt_Acc_Change_Credit_Division +
			'",_txt_Acc_Change_Credit_Inter_Amount: "' + _txt_Acc_Change_Credit_Inter_Amount + '",_txt_Acc_Change_Credit_Inter_Rate: "' + _txt_Acc_Change_Credit_Inter_Rate +

            '",_rdb_swift_None: "' + _rdb_swift_None + '",_rdb_swift_200: "' + _rdb_swift_200 +
            // MT 200
'",_txt200TransactionRefNO: "' + _txt200TransactionRefNO + '",_txt200Date: "' + _txt200Date +
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
'",_txtMoreInfo5R42: "' + _txtMoreInfo5R42 +  '",_rdb_swift_R42: "' + _rdb_swift_R42 +
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
function OnSuccess(response) {
}
function OnDocNextClick(index) {
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}
function OnBackClick() {
    SaveUpdateData();
    window.location.href = "TF_IMP_Settlement_Maker_View.aspx";
    return false;
}
function SubmitCheck() {
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
function Toggel_Swift_Type(Type) {

    if (Type == 'None') {
        if ($("#TabContainerMain_tbSwift_rdb_swift_None").is(':checked')) {
            $("#TabContainerMain_tbSwift_rdb_swift_200").prop('checked', false);
            $("#TabContainerMain_tbSwift_rdb_swift_R42").prop('checked', false);
        }
    }
    if (Type == 'MT200') {
        if ($("#TabContainerMain_tbSwift_rdb_swift_200").is(':checked')) {
            $("#TabContainerMain_tbSwift_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbSwift_rdb_swift_R42").prop('checked', false);
        }
    }
    if (Type == 'R42') {
        if ($("#TabContainerMain_tbSwift_rdb_swift_R42").is(':checked')) {
            $("#TabContainerMain_tbSwift_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbSwift_rdb_swift_200").prop('checked', false);
        }
    }
}
function ViewSwiftMessage() {
    SaveUpdateData();
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = 'FOREIGN';
    if ($("#TabContainerMain_tbSwift_rdb_swift_200").is(':checked')) {
        _SWIFT_File_Type = 'MT200';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_R42").is(':checked')) {
        _SWIFT_File_Type = 'R42';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    
}