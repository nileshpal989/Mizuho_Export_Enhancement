function SubmitValidation() {
    if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_499").is(':checked')) {
        if (
            $("#TabContainerMain_tbSwiftDetails_Narrative4991").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative4992").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative4993").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative4994").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative4995").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative4996").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative4997").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative4999").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49910").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49911").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49912").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49913").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49914").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49915").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49916").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49917").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49919").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49920").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49921").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49922").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49923").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49924").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49925").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49926").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49927").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49929").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49930").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49931").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49932").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49933").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49934").val() == "" &&
            $("#TabContainerMain_tbSwiftDetails_Narrative49935").val() == ""
        ) {
            alert('Please Enter Narrative.');
            $("#TabContainerMain_tbSwiftDetails_Narrative4991").focus();
            return false;
        }
    }
    return true;
}

function Toggel_GO_SWIFT_Remark() {
    if ($("#TabContainerMain_tbDocumentGO_chkGO_Swift_SFMS").is(':checked')) {
        var _txt_GO_Swift_SFMS_Remarks = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Remarks").val();

        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Details").val(_txt_GO_Swift_SFMS_Remarks);
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Details").val(_txt_GO_Swift_SFMS_Remarks);
    }
}


function ViewSwiftMessage() {
    SaveUpdateData();
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = 'FOREIGN';
    if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_412").is(':checked')) {
        _SWIFT_File_Type = 'MT412';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_499").is(':checked')) {
        _SWIFT_File_Type = 'MT499C';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_999").is(':checked')) {
        _SWIFT_File_Type = 'MT999C';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    return false;
}
function Toggel_Swift_Type(Type) {

    if (Type == 'None') {
        if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_None").is(':checked')) {
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_412").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_999").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_499").prop('checked', false);
        }
    }
    if (Type == 'MT412') {
        if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_412").is(':checked')) {
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_999").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_499").prop('checked', false);
        }
    }
    if (Type == 'MT499') {
        if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_499").is(':checked')) {
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_412").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_999").prop('checked', false);
        }
    }
    if (Type == 'MT999') {
        if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_999").is(':checked')) {
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_412").prop('checked', false);
            $("#TabContainerMain_tbSwiftDetails_rdb_swift_499").prop('checked', false);
        }
    }
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
function DebitCredit_Amt(Type) {
    if (Type == 'Swift_SFMS') {
        var _txt_GO_Swift_SFMS_Debit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Amt").val(_txt_GO_Swift_SFMS_Debit_Amt);
    }
}
function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}
function SaveUpdateData() {

    // Document Details
    var hdnUserName = document.getElementById('hdnUserName').value;
    var _BranchName = $("#hdnBranchName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _txtValueDate = $("#txtValueDate").val();
    var _Document_Curr = $("#lblDoc_Curr").text();

    var _txt_Accepted_Date = $("#TabContainerMain_tbDocumentDetails_txt_Accepted_Date").val();
    var _txt_Maturity_Date = $("#TabContainerMain_tbDocumentDetails_txt_Maturity_Date").val();
    var _txtAttn = $("#TabContainerMain_tbDocumentDetails_txtAttn").val();
    var _txt_Expected_InterestAmt = $("#TabContainerMain_tbDocumentDetails_txt_Expected_InterestAmt").val();
    var _txt_Received_InterestAmt = $("#TabContainerMain_tbDocumentDetails_txt_Received_InterestAmt").val();



    //Swift Details
    //499
    var _txt_Narrative499_1 = $("#TabContainerMain_tbSwiftDetails_Narrative4991").val();
    var _txt_Narrative499_2 = $("#TabContainerMain_tbSwiftDetails_Narrative4992").val();
    var _txt_Narrative499_3 = $("#TabContainerMain_tbSwiftDetails_Narrative4993").val();
    var _txt_Narrative499_4 = $("#TabContainerMain_tbSwiftDetails_Narrative4994").val();
    var _txt_Narrative499_5 = $("#TabContainerMain_tbSwiftDetails_Narrative4995").val();
    var _txt_Narrative499_6 = $("#TabContainerMain_tbSwiftDetails_Narrative4996").val();
    var _txt_Narrative499_7 = $("#TabContainerMain_tbSwiftDetails_Narrative4997").val();
    var _txt_Narrative499_8 = $("#TabContainerMain_tbSwiftDetails_Narrative4998").val();
    var _txt_Narrative499_9 = $("#TabContainerMain_tbSwiftDetails_Narrative4999").val();
    var _txt_Narrative499_10 = $("#TabContainerMain_tbSwiftDetails_Narrative49910").val();
    var _txt_Narrative499_11 = $("#TabContainerMain_tbSwiftDetails_Narrative49911").val();
    var _txt_Narrative499_12 = $("#TabContainerMain_tbSwiftDetails_Narrative49912").val();
    var _txt_Narrative499_13 = $("#TabContainerMain_tbSwiftDetails_Narrative49913").val();
    var _txt_Narrative499_14 = $("#TabContainerMain_tbSwiftDetails_Narrative49914").val();
    var _txt_Narrative499_15 = $("#TabContainerMain_tbSwiftDetails_Narrative49915").val();
    var _txt_Narrative499_16 = $("#TabContainerMain_tbSwiftDetails_Narrative49916").val();
    var _txt_Narrative499_17 = $("#TabContainerMain_tbSwiftDetails_Narrative49917").val();
    var _txt_Narrative499_18 = $("#TabContainerMain_tbSwiftDetails_Narrative49918").val();
    var _txt_Narrative499_19 = $("#TabContainerMain_tbSwiftDetails_Narrative49919").val();
    var _txt_Narrative499_20 = $("#TabContainerMain_tbSwiftDetails_Narrative49920").val();
    var _txt_Narrative499_21 = $("#TabContainerMain_tbSwiftDetails_Narrative49921").val();
    var _txt_Narrative499_22 = $("#TabContainerMain_tbSwiftDetails_Narrative49922").val();
    var _txt_Narrative499_23 = $("#TabContainerMain_tbSwiftDetails_Narrative49923").val();
    var _txt_Narrative499_24 = $("#TabContainerMain_tbSwiftDetails_Narrative49924").val();
    var _txt_Narrative499_25 = $("#TabContainerMain_tbSwiftDetails_Narrative49925").val();
    var _txt_Narrative499_26 = $("#TabContainerMain_tbSwiftDetails_Narrative49926").val();
    var _txt_Narrative499_27 = $("#TabContainerMain_tbSwiftDetails_Narrative49927").val();
    var _txt_Narrative499_28 = $("#TabContainerMain_tbSwiftDetails_Narrative49928").val();
    var _txt_Narrative499_29 = $("#TabContainerMain_tbSwiftDetails_Narrative49929").val();
    var _txt_Narrative499_30 = $("#TabContainerMain_tbSwiftDetails_Narrative49930").val();
    var _txt_Narrative499_31 = $("#TabContainerMain_tbSwiftDetails_Narrative49931").val();
    var _txt_Narrative499_32 = $("#TabContainerMain_tbSwiftDetails_Narrative49932").val();
    var _txt_Narrative499_33 = $("#TabContainerMain_tbSwiftDetails_Narrative49933").val();
    var _txt_Narrative499_34 = $("#TabContainerMain_tbSwiftDetails_Narrative49934").val();
    var _txt_Narrative499_35 = $("#TabContainerMain_tbSwiftDetails_Narrative49935").val();
    //999
    var _txt_Narrative1 = $("#TabContainerMain_tbSwiftDetails_txt_Narrative_1").val();
    var _txt_Narrative2 = $("#TabContainerMain_tbSwiftDetails_txt_Narrative_2").val();
    var _txt_Narrative3 = $("#TabContainerMain_tbSwiftDetails_txt_Narrative_3").val();
    var _txt_Narrative4 = $("#TabContainerMain_tbSwiftDetails_txt_Narrative_4").val();
    var _txt_Narrative5 = $("#TabContainerMain_tbSwiftDetails_txt_Narrative_5").val();
    var _txt_Narrative6 = $("#TabContainerMain_tbSwiftDetails_txt_Narrative_6").val();

    //  General_Operations Swift_SFMS_CHRG
    var _chkGO_Swift_SFMS = 'N', _txt_GO_Swift_SFMS_Comment = '', _txt_GO_Swift_SFMS_Ref_No = '',
        _txt_GO_Swift_SFMS_SectionNo = '', _txt_GO_Swift_SFMS_Remarks = '', _txt_GO_Swift_SFMS_Memo = '',

        _txt_GO_Swift_SFMS_Debit_Code = '', _txt_GO_Swift_SFMS_Debit_Curr = '', _txt_GO_Swift_SFMS_Debit_Amt = 0,
        _txt_GO_Swift_SFMS_Debit_Cust = '', _txt_GO_Swift_SFMS_Debit_Cust_AcCode = '', _txt_GO_Swift_SFMS_Debit_Cust_AccNo = '',
        _txt_GO_Swift_SFMS_Debit_Exch_Curr = '', _txt_GO_Swift_SFMS_Debit_Exch_Rate = 0,
        _txt_GO_Swift_SFMS_Debit_AdPrint = '', _txt_GO_Swift_SFMS_Debit_Details = '', _txt_GO_Swift_SFMS_Debit_Entity = '',

        _txt_GO_Swift_SFMS_Credit_Code = '', _txt_GO_Swift_SFMS_Credit_Curr = '', _txt_GO_Swift_SFMS_Credit_Amt = 0,
        _txt_GO_Swift_SFMS_Credit_Cust = '', _txt_GO_Swift_SFMS_Credit_Cust_AcCode = '', _txt_GO_Swift_SFMS_Credit_Cust_AccNo = '',
        _txt_GO_Swift_SFMS_Credit_Exch_Curr = '', _txt_GO_Swift_SFMS_Credit_Exch_Rate = 0,
        _txt_GO_Swift_SFMS_Credit_AdPrint = '', _txt_GO_Swift_SFMS_Credit_Details = '', _txt_GO_Swift_SFMS_Credit_Entity = '',

        _txt_GO_Swift_SFMS_Scheme_no = '',
        _txt_GO_Swift_SFMS_Debit_FUND = '', _txt_GO_Swift_SFMS_Debit_Check_No = '', _txt_GO_Swift_SFMS_Debit_Available = '',
        _txt_GO_Swift_SFMS_Debit_Division = '', _txt_GO_Swift_SFMS_Debit_Inter_Amount = '', _txt_GO_Swift_SFMS_Debit_Inter_Rate = '',

        _txt_GO_Swift_SFMS_Credit_FUND = '', _txt_GO_Swift_SFMS_Credit_Check_No = '', _txt_GO_Swift_SFMS_Credit_Available = '',
        _txt_GO_Swift_SFMS_Credit_Division = '', _txt_GO_Swift_SFMS_Credit_Inter_Amount = '', _txt_GO_Swift_SFMS_Credit_Inter_Rate = '';

    if ($("#TabContainerMain_tbDocumentGO_chkGO_Swift_SFMS").is(':checked')) {
        _chkGO_Swift_SFMS = 'Y';

        _txt_GO_Swift_SFMS_Comment = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Comment").val();
        _txt_GO_Swift_SFMS_Ref_No = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Ref_No").val();

        _txt_GO_Swift_SFMS_SectionNo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_SectionNo").val();
        _txt_GO_Swift_SFMS_Remarks = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Remarks").val();
        _txt_GO_Swift_SFMS_Memo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Memo").val();

        // Debit

        _txt_GO_Swift_SFMS_Debit_Code = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Code").val();
        _txt_GO_Swift_SFMS_Debit_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Curr").val();
        _txt_GO_Swift_SFMS_Debit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Amt").val();

        _txt_GO_Swift_SFMS_Debit_Cust = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust").val();
        _txt_GO_Swift_SFMS_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust_AcCode").val();
        _txt_GO_Swift_SFMS_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust_AccNo").val();

        _txt_GO_Swift_SFMS_Debit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Exch_Curr").val();
        _txt_GO_Swift_SFMS_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Exch_Rate").val();

        _txt_GO_Swift_SFMS_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_AdPrint").val();
        _txt_GO_Swift_SFMS_Debit_Details = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Details").val();
        _txt_GO_Swift_SFMS_Debit_Entity = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Entity").val();

        // Credit

        _txt_GO_Swift_SFMS_Credit_Code = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Code").val();
        _txt_GO_Swift_SFMS_Credit_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Curr").val();
        _txt_GO_Swift_SFMS_Credit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Amt").val();

        _txt_GO_Swift_SFMS_Credit_Cust = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Cust").val();
        _txt_GO_Swift_SFMS_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Cust_AcCode").val();
        _txt_GO_Swift_SFMS_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Cust_AccNo").val();

        _txt_GO_Swift_SFMS_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Exch_Curr").val();
        _txt_GO_Swift_SFMS_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Exch_Rate").val();

        _txt_GO_Swift_SFMS_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_AdPrint").val();
        _txt_GO_Swift_SFMS_Credit_Details = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Details").val();
        _txt_GO_Swift_SFMS_Credit_Entity = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Entity").val();

        ////////////
        _txt_GO_Swift_SFMS_Scheme_no = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Scheme_no").val();
        _txt_GO_Swift_SFMS_Debit_FUND = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_FUND").val();
        _txt_GO_Swift_SFMS_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Check_No").val();
        _txt_GO_Swift_SFMS_Debit_Available = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Available").val();
        _txt_GO_Swift_SFMS_Debit_Division = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Division").val();
        _txt_GO_Swift_SFMS_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Inter_Amount").val();
        _txt_GO_Swift_SFMS_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Inter_Rate").val();
        _txt_GO_Swift_SFMS_Credit_FUND = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_FUND").val();
        _txt_GO_Swift_SFMS_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Check_No").val();
        _txt_GO_Swift_SFMS_Credit_Available = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Available").val();
        _txt_GO_Swift_SFMS_Credit_Division = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Division").val();
        _txt_GO_Swift_SFMS_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Inter_Amount").val();
        _txt_GO_Swift_SFMS_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Inter_Rate").val();

    }

    var Swift_412 = ""; var Swift_999C = ""; var Swift_499 = "";
    if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_412").is(':checked')) {
        Swift_412 = "Y";
    }
    if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_999").is(':checked')) {
        Swift_999C = "Y";
    }
    if ($("#TabContainerMain_tbSwiftDetails_rdb_swift_499").is(':checked')) {
        Swift_499 = "Y";
    }

    $.ajax({
        type: "POST",
        url: "TF_IMP_Collection_Acceptance_Maker.aspx/AddUpdateAceptance",
        data: '{hdnUserName: "' + hdnUserName + '", _BranchName:"' + _BranchName + '", _txtDocNo: "' + _txtDocNo + '", _Document_Curr: "' + _Document_Curr + '", _txtValueDate: "' + _txtValueDate +
            '", _txt_Accepted_Date: "' + _txt_Accepted_Date + '", _txt_Maturity_Date: "' + _txt_Maturity_Date +
            '", _txtAttn: "' + _txtAttn + '", _txt_Expected_InterestAmt: "' + _txt_Expected_InterestAmt + '", _txt_Received_InterestAmt: "' + _txt_Received_InterestAmt +

        ////  General_Operations Swift_SFMS_CHRG
            '", _chkGO_Swift_SFMS: "' + _chkGO_Swift_SFMS + '", _txt_GO_Swift_SFMS_Comment: "' + _txt_GO_Swift_SFMS_Comment + '", _txt_GO_Swift_SFMS_Ref_No: "' + _txt_GO_Swift_SFMS_Ref_No +
            '", _txt_GO_Swift_SFMS_SectionNo: "' + _txt_GO_Swift_SFMS_SectionNo + '", _txt_GO_Swift_SFMS_Remarks: "' + _txt_GO_Swift_SFMS_Remarks + '", _txt_GO_Swift_SFMS_Memo: "' + _txt_GO_Swift_SFMS_Memo +

            '", _txt_GO_Swift_SFMS_Debit_Code: "' + _txt_GO_Swift_SFMS_Debit_Code + '", _txt_GO_Swift_SFMS_Debit_Curr: "' + _txt_GO_Swift_SFMS_Debit_Curr + '", _txt_GO_Swift_SFMS_Debit_Amt: "' + _txt_GO_Swift_SFMS_Debit_Amt +
            '", _txt_GO_Swift_SFMS_Debit_Cust: "' + _txt_GO_Swift_SFMS_Debit_Cust + '", _txt_GO_Swift_SFMS_Debit_Cust_AcCode: "' + _txt_GO_Swift_SFMS_Debit_Cust_AcCode + '", _txt_GO_Swift_SFMS_Debit_Cust_AccNo: "' + _txt_GO_Swift_SFMS_Debit_Cust_AccNo +
            '", _txt_GO_Swift_SFMS_Debit_Exch_Curr: "' + _txt_GO_Swift_SFMS_Debit_Exch_Curr + '", _txt_GO_Swift_SFMS_Debit_Exch_Rate: "' + _txt_GO_Swift_SFMS_Debit_Exch_Rate +
            '", _txt_GO_Swift_SFMS_Debit_AdPrint: "' + _txt_GO_Swift_SFMS_Debit_AdPrint + '", _txt_GO_Swift_SFMS_Debit_Details: "' + _txt_GO_Swift_SFMS_Debit_Details + '", _txt_GO_Swift_SFMS_Debit_Entity: "' + _txt_GO_Swift_SFMS_Debit_Entity +

            '", _txt_GO_Swift_SFMS_Credit_Code: "' + _txt_GO_Swift_SFMS_Credit_Code + '", _txt_GO_Swift_SFMS_Credit_Curr: "' + _txt_GO_Swift_SFMS_Credit_Curr + '", _txt_GO_Swift_SFMS_Credit_Amt: "' + _txt_GO_Swift_SFMS_Credit_Amt +
            '", _txt_GO_Swift_SFMS_Credit_Cust: "' + _txt_GO_Swift_SFMS_Credit_Cust + '", _txt_GO_Swift_SFMS_Credit_Cust_AcCode: "' + _txt_GO_Swift_SFMS_Credit_Cust_AcCode + '", _txt_GO_Swift_SFMS_Credit_Cust_AccNo: "' + _txt_GO_Swift_SFMS_Credit_Cust_AccNo +
            '", _txt_GO_Swift_SFMS_Credit_Exch_Curr: "' + _txt_GO_Swift_SFMS_Credit_Exch_Curr + '", _txt_GO_Swift_SFMS_Credit_Exch_Rate: "' + _txt_GO_Swift_SFMS_Credit_Exch_Rate +
            '", _txt_GO_Swift_SFMS_Credit_AdPrint: "' + _txt_GO_Swift_SFMS_Credit_AdPrint + '", _txt_GO_Swift_SFMS_Credit_Details: "' + _txt_GO_Swift_SFMS_Credit_Details + '", _txt_GO_Swift_SFMS_Credit_Entity: "' + _txt_GO_Swift_SFMS_Credit_Entity +

            '", _txt_GO_Swift_SFMS_Scheme_no:"' + _txt_GO_Swift_SFMS_Scheme_no +
            '", _txt_GO_Swift_SFMS_Debit_FUND:"' + _txt_GO_Swift_SFMS_Debit_FUND + '", _txt_GO_Swift_SFMS_Debit_Check_No:"' + _txt_GO_Swift_SFMS_Debit_Check_No + '", _txt_GO_Swift_SFMS_Debit_Available:"' + _txt_GO_Swift_SFMS_Debit_Available +
            '", _txt_GO_Swift_SFMS_Debit_Division:"' + _txt_GO_Swift_SFMS_Debit_Division + '", _txt_GO_Swift_SFMS_Debit_Inter_Amount:"' + _txt_GO_Swift_SFMS_Debit_Inter_Amount + '", _txt_GO_Swift_SFMS_Debit_Inter_Rate:"' + _txt_GO_Swift_SFMS_Debit_Inter_Rate +
            '", _txt_GO_Swift_SFMS_Credit_FUND:"' + _txt_GO_Swift_SFMS_Credit_FUND + '", _txt_GO_Swift_SFMS_Credit_Check_No:"' + _txt_GO_Swift_SFMS_Credit_Check_No + '", _txt_GO_Swift_SFMS_Credit_Available:"' + _txt_GO_Swift_SFMS_Credit_Available +
            '", _txt_GO_Swift_SFMS_Credit_Division:"' + _txt_GO_Swift_SFMS_Credit_Division + '", _txt_GO_Swift_SFMS_Credit_Inter_Amount:"' + _txt_GO_Swift_SFMS_Credit_Inter_Amount + '", _txt_GO_Swift_SFMS_Credit_Inter_Rate:"' + _txt_GO_Swift_SFMS_Credit_Inter_Rate +
        //Swift Details
            '", Swift_412: "' + Swift_412 + '", Swift_999C: "' + Swift_999C + '", Swift_499: "' + Swift_499 +
            '", _txt_Narrative1: "' + _txt_Narrative1 + '", _txt_Narrative2: "' + _txt_Narrative2 + '", _txt_Narrative3: "' + _txt_Narrative3 + '", _txt_Narrative4: "' + _txt_Narrative4 + '", _txt_Narrative5: "' + _txt_Narrative5 +
            '", _txt_Narrative6: "' + _txt_Narrative6 +

            '", _txt_Narrative499_1: "' + _txt_Narrative499_1 + '", _txt_Narrative499_2: "' + _txt_Narrative499_2 + '", _txt_Narrative499_3: "' + _txt_Narrative499_3 + '", _txt_Narrative499_4: "' + _txt_Narrative499_4 + '", _txt_Narrative499_5: "' + _txt_Narrative499_5 +
            '", _txt_Narrative499_6: "' + _txt_Narrative499_6 + '", _txt_Narrative499_7: "' + _txt_Narrative499_7 + '", _txt_Narrative499_8: "' + _txt_Narrative499_8 + '", _txt_Narrative499_9: "' + _txt_Narrative499_9 + '", _txt_Narrative499_10: "' + _txt_Narrative499_10 +
            '", _txt_Narrative499_11: "' + _txt_Narrative499_11 + '", _txt_Narrative499_12: "' + _txt_Narrative499_12 + '", _txt_Narrative499_13: "' + _txt_Narrative499_13 + '", _txt_Narrative499_14: "' + _txt_Narrative499_14 + '", _txt_Narrative499_15: "' + _txt_Narrative499_15 +
            '", _txt_Narrative499_16: "' + _txt_Narrative499_16 + '", _txt_Narrative499_17: "' + _txt_Narrative499_17 + '", _txt_Narrative499_18: "' + _txt_Narrative499_18 + '", _txt_Narrative499_19: "' + _txt_Narrative499_19 + '", _txt_Narrative499_20: "' + _txt_Narrative499_20 +
            '", _txt_Narrative499_21: "' + _txt_Narrative499_21 + '", _txt_Narrative499_22: "' + _txt_Narrative499_22 + '", _txt_Narrative499_23: "' + _txt_Narrative499_23 + '", _txt_Narrative499_24: "' + _txt_Narrative499_24 + '", _txt_Narrative499_25: "' + _txt_Narrative499_25 +
            '", _txt_Narrative499_26: "' + _txt_Narrative499_26 + '", _txt_Narrative499_27: "' + _txt_Narrative499_27 + '", _txt_Narrative499_28: "' + _txt_Narrative499_28 + '", _txt_Narrative499_29: "' + _txt_Narrative499_29 + '", _txt_Narrative499_30: "' + _txt_Narrative499_30 +
            '", _txt_Narrative499_31: "' + _txt_Narrative499_31 + '", _txt_Narrative499_32: "' + _txt_Narrative499_32 + '", _txt_Narrative499_33: "' + _txt_Narrative499_33 + '", _txt_Narrative499_34: "' + _txt_Narrative499_34 + '", _txt_Narrative499_35: "' + _txt_Narrative499_35 +


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
function SubmitCheck() {
    if (SubmitValidation() == true) {
        if (confirm('Are you sure you want to Submit this record to checker?')) {
            return true;
        }
        else
            return false;
    }
    else
        return false;
}