function validatecurr() {
    var lblCurr = $("#lblDoc_Curr").text();
        var amt = $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").val();
        $("#TabContainerMain_tbDocumentLedger_txtLedgerBalanceAmt").val(amt);
}
function Toggel_Swift_Type(Type) {

    if (Type == 'None') {

        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_499").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").prop('checked', false);
        }

    }

    if (Type == 'MT499') {

        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_499").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").prop('checked', false);
        }
    }
    if (Type == 'MT799') {
        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_499").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").prop('checked', false);
        }
    }
    if (Type == 'MT999') {
        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_499").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").prop('checked', false);
        }
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
function Uppercase(id) {
    $('#' + id).val($('#' + id).val().toUpperCase());
}
function round(values, decimals) {
    return Number(Math.round(values + 'e' + decimals) + 'e-' + decimals);
}

function SubmitCheck() {
    if (confirm('Are you sure you want to Submit this record to checker?')) {
        SubmitToChecker();
       // return true;
    }
    else
        return false;
}

function SubmitToChecker() {
    var DocNo = $("#txtDocNo").val();
    var UserName = $("#hdnUserName").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker.aspx/SubmitToChecker",
        data: '{UserName:"' + UserName + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CheckerStatus == "Submit") {
                window.location.href = "TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker_View.aspx?result=Submit";
            }
            else {
                alert(data.d.CheckerStatus);
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}

function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
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
    var hdnUserName = document.getElementById('hdnUserName').value;
    var _BranchName = $("#hdnBranchName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _txtValueDate = $("#txtValueDate").val();
    var _Document_Curr = $("#lblDoc_Curr").text();
    //var  _chk_Ledger_Modify = 'Y';
    var _txtLedgerRemark = $("#TabContainerMain_tbDocumentLedger_txtLedgerRemark").val();
    var _txtLedgerCustName = $("#TabContainerMain_tbDocumentLedger_txtLedgerCustName").val();
    var _txtLedgerAccode = $("#TabContainerMain_tbDocumentLedger_txtLedgerAccode").val();
    var _txtLedgerCURR = $("#TabContainerMain_tbDocumentLedger_txtLedgerCURR").val();
    var _txtLedgerRefNo = $("#TabContainerMain_tbDocumentLedger_txtLedgerRefNo").val();
    var _txtLedger_Modify_amt = $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").val();
    var _txtLedgerOperationDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerOperationDate").val();
    var _txtLedgerBalanceAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerBalanceAmt").val();
    var _txtLedgerAcceptDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerAcceptDate").val();
    var _txtLedgerMaturity = $("#TabContainerMain_tbDocumentLedger_txtLedgerMaturity").val();
    var _txtLedgerSettlememtDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerSettlememtDate").val();
    var _txtLedgerSettlValue = $("#TabContainerMain_tbDocumentLedger_txtLedgerSettlValue").val();
    var _txtLedgerLastModDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerLastModDate").val();
    var _txtLedgerREM_EUC = $("#TabContainerMain_tbDocumentLedger_txtLedgerREM_EUC").val();
    var _txtLedgerLastOPEDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerLastOPEDate").val();
    var _txtLedgerTransNo = $("#TabContainerMain_tbDocumentLedger_txtLedgerTransNo").val();
    var _txtLedgerContraCountry = $("#TabContainerMain_tbDocumentLedger_txtLedgerContraCountry").val();
    var _txtLedgerStatusCode = $("#TabContainerMain_tbDocumentLedger_txtLedgerStatusCode").val();
    var _txtLedgerCollectOfComm = $("#TabContainerMain_tbDocumentLedger_txtLedgerCollectOfComm").val();
    var _txtLedgerCommodity = $("#TabContainerMain_tbDocumentLedger_txtLedgerCommodity").val();
    var _txtLedgerhandlingCommRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommRate").val();
    var _txtLedgerhandlingCommCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommCurr").val();
    var _txtLedgerhandlingCommPayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommPayer").val();
    var _txtLedgerhandlingCommAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommAmt").val();
    var _txtLedgerPostageRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostageRate").val();
    var _txtLedgerPostageCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostageCurr").val();
    var _txtLedgerPostageAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostageAmt").val();
    var _txtLedgerPostagePayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostagePayer").val();
    var _txtLedgerOthersRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersRate").val();
    var _txtLedgerOthersCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersCurr").val();
    var _txtLedgerOthersAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersAmt").val();
    var _txtLedgerOthersPayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersPayer").val();
    var _txtLedgerTheirCommRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommRate").val();
    var _txtLedgerTheirCommCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommCurr").val();
    var _txtLedgerTheirCommAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommAmt").val();
    var _txtLedgerTheirCommPayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommPayer").val();
    var _txtLedgerNegoBank = $("#TabContainerMain_tbDocumentLedger_txtLedgerNegoBank").val();

    var _txtLedgerReimbursingBank = $("#TabContainerMain_tbDocumentLedger_txtLedgerReimbursingBank").val();
    var _txtLedgerDrawer = $("#TabContainerMain_tbDocumentLedger_txtLedgerDrawer").val();
    var _txtLedgerTenor = $("#TabContainerMain_tbDocumentLedger_txtLedgerTenor").val();
    var _txtLedgerAttn = $("#TabContainerMain_tbDocumentLedger_txtLedgerAttn").val();

    // Swift Details
    //// Swift Files
    var None_Flag = '', MT499_Flag = '', MT799_Flag = '', MT999_Flag = '';

    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").is(':checked')) {
        None_Flag = 'Y';
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_499").is(':checked')) {
        MT499_Flag = 'Y';
    }

    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
        MT799_Flag = 'Y';
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
        MT999_Flag = 'Y';
    }

    var _txt_Narrative1 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative1").val();
    var _txt_Narrative2 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative2").val();
    var _txt_Narrative3 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative3").val();
    var _txt_Narrative4 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative4").val();
    var _txt_Narrative5 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative5").val();
    var _txt_Narrative6 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative6").val();
    var _txt_Narrative7 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative7").val();
    var _txt_Narrative8 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative8").val();
    var _txt_Narrative9 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative9").val();
    var _txt_Narrative10 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative10").val();
    var _txt_Narrative11 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative11").val();
    var _txt_Narrative12 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative12").val();
    var _txt_Narrative13 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative13").val();
    var _txt_Narrative14 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative14").val();
    var _txt_Narrative15 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative15").val();
    var _txt_Narrative16 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative16").val();
    var _txt_Narrative17 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative17").val();
    var _txt_Narrative18 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative18").val();
    var _txt_Narrative19 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative19").val();
    var _txt_Narrative20 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative20").val();
    var _txt_Narrative21 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative21").val();
    var _txt_Narrative22 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative22").val();
    var _txt_Narrative23 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative23").val();
    var _txt_Narrative24 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative24").val();
    var _txt_Narrative25 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative25").val();
    var _txt_Narrative26 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative26").val();
    var _txt_Narrative27 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative27").val();
    var _txt_Narrative28 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative28").val();
    var _txt_Narrative29 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative29").val();
    var _txt_Narrative30 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative30").val();
    var _txt_Narrative31 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative31").val();
    var _txt_Narrative32 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative32").val();
    var _txt_Narrative33 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative33").val();
    var _txt_Narrative34 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative34").val();
    var _txt_Narrative35 = $("#TabContainerMain_tbDocumentSwiftFile_Narrative35").val();

    $.ajax({
        type: "POST",
        url: "TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker.aspx/AddUpdateLedgerModification",
        data: '{ hdnUserName: "' + hdnUserName + '", _BranchName:"' + _BranchName + '", _txtDocNo: "' + _txtDocNo + '", _txtValueDate: "' + _txtValueDate +
        //////LEDGER MODIFICATION _txtLedgerRemark
                    '", _txtLedgerRemark:"' + _txtLedgerRemark +
                    '", _txtLedgerCustName:"' + _txtLedgerCustName + '", _txtLedgerAccode:"' + _txtLedgerAccode + '", _txtLedgerCURR:"' + _txtLedgerCURR +
                    '", _txtLedgerRefNo:"' + _txtLedgerRefNo + '", _txtLedger_Modify_amt:"' + _txtLedger_Modify_amt +
                    '", _txtLedgerOperationDate:"' + _txtLedgerOperationDate + '", _txtLedgerBalanceAmt:"' + _txtLedgerBalanceAmt +
                    '", _txtLedgerAcceptDate:"' + _txtLedgerAcceptDate + '", _txtLedgerMaturity:"' + _txtLedgerMaturity +
                    '", _txtLedgerSettlememtDate:"' + _txtLedgerSettlememtDate + '", _txtLedgerSettlValue:"' + _txtLedgerSettlValue +
                    '", _txtLedgerLastModDate:"' + _txtLedgerLastModDate + '", _txtLedgerREM_EUC:"' + _txtLedgerREM_EUC +
                    '", _txtLedgerLastOPEDate:"' + _txtLedgerLastOPEDate + '", _txtLedgerTransNo:"' + _txtLedgerTransNo +
                    '", _txtLedgerContraCountry:"' + _txtLedgerContraCountry + '", _txtLedgerStatusCode:"' + _txtLedgerStatusCode +
                    '", _txtLedgerCollectOfComm:"' + _txtLedgerCollectOfComm + '", _txtLedgerCommodity:"' + _txtLedgerCommodity +
                    '", _txtLedgerhandlingCommRate:"' + _txtLedgerhandlingCommRate + '", _txtLedgerhandlingCommCurr:"' + _txtLedgerhandlingCommCurr +
                    '", _txtLedgerhandlingCommAmt:"' + _txtLedgerhandlingCommAmt + '", _txtLedgerhandlingCommPayer:"' + _txtLedgerhandlingCommPayer +
                    '", _txtLedgerPostageRate:"' + _txtLedgerPostageRate + '", _txtLedgerPostageCurr:"' + _txtLedgerPostageCurr +
                    '", _txtLedgerPostageAmt:"' + _txtLedgerPostageAmt + '", _txtLedgerPostagePayer:"' + _txtLedgerPostagePayer +
                    '", _txtLedgerOthersRate:"' + _txtLedgerOthersRate + '", _txtLedgerOthersCurr:"' + _txtLedgerOthersCurr +
                    '", _txtLedgerOthersAmt:"' + _txtLedgerOthersAmt + '", _txtLedgerOthersPayer:"' + _txtLedgerOthersPayer +
                    '", _txtLedgerTheirCommRate:"' + _txtLedgerTheirCommRate + '", _txtLedgerTheirCommCurr:"' + _txtLedgerTheirCommCurr +
                    '", _txtLedgerTheirCommAmt:"' + _txtLedgerTheirCommAmt + '", _txtLedgerTheirCommPayer:"' + _txtLedgerTheirCommPayer +
                    '", _txtLedgerNegoBank:"' + _txtLedgerNegoBank +
                    '", _txtLedgerReimbursingBank:"' + _txtLedgerReimbursingBank +
                    '", _txtLedgerDrawer:"' + _txtLedgerDrawer + '", _txtLedgerTenor:"' + _txtLedgerTenor + '", _txtLedgerAttn:"' + _txtLedgerAttn +

        //swift File
                    '", None_Flag: "' + None_Flag + '", MT499_Flag: "' + MT499_Flag + '", MT799_Flag: "' + MT799_Flag + '", MT999_Flag: "' + MT999_Flag +
                    '", _txt_Narrative1: "' + _txt_Narrative1 + '", _txt_Narrative2: "' + _txt_Narrative2 + '", _txt_Narrative3: "' + _txt_Narrative3 + '", _txt_Narrative4: "' + _txt_Narrative4 + '", _txt_Narrative5: "' + _txt_Narrative5 +
                    '", _txt_Narrative6: "' + _txt_Narrative6 + '", _txt_Narrative7: "' + _txt_Narrative7 + '", _txt_Narrative8: "' + _txt_Narrative8 + '", _txt_Narrative9: "' + _txt_Narrative9 + '", _txt_Narrative10: "' + _txt_Narrative10 +
                    '", _txt_Narrative11: "' + _txt_Narrative11 + '", _txt_Narrative12: "' + _txt_Narrative12 + '", _txt_Narrative13: "' + _txt_Narrative13 + '", _txt_Narrative14: "' + _txt_Narrative14 + '", _txt_Narrative15: "' + _txt_Narrative15 +
                    '", _txt_Narrative16: "' + _txt_Narrative16 + '", _txt_Narrative17: "' + _txt_Narrative17 + '", _txt_Narrative18: "' + _txt_Narrative18 + '", _txt_Narrative19: "' + _txt_Narrative19 + '", _txt_Narrative20: "' + _txt_Narrative20 +
                    '", _txt_Narrative21: "' + _txt_Narrative21 + '", _txt_Narrative22: "' + _txt_Narrative22 + '", _txt_Narrative23: "' + _txt_Narrative23 + '", _txt_Narrative24: "' + _txt_Narrative24 + '", _txt_Narrative25: "' + _txt_Narrative25 +
                    '", _txt_Narrative26: "' + _txt_Narrative26 + '", _txt_Narrative27: "' + _txt_Narrative27 + '", _txt_Narrative28: "' + _txt_Narrative28 + '", _txt_Narrative29: "' + _txt_Narrative29 + '", _txt_Narrative30: "' + _txt_Narrative30 +
                    '", _txt_Narrative31: "' + _txt_Narrative31 + '", _txt_Narrative32: "' + _txt_Narrative32 + '", _txt_Narrative33: "' + _txt_Narrative33 + '", _txt_Narrative34: "' + _txt_Narrative34 + '", _txt_Narrative35: "' + _txt_Narrative35 +
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

function ViewSwiftMessage() {
    SaveUpdateData();
    var _FLC_ILC = $("#hdn_FLC_ILC").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _lblForeign_Local = "FOREIGN_Ledger";
        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_499").is(':checked')) {
                  _SWIFT_File_Type = 'MT499';
                  var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _lblForeign_Local, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                  winame.focus();
        }
        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
            _SWIFT_File_Type = 'MT799';
            var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _lblForeign_Local, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
            winame.focus();
        }
        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
            _SWIFT_File_Type = 'MT999';
            var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _lblForeign_Local, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
            winame.focus();
        } 
    
    return false;
}
