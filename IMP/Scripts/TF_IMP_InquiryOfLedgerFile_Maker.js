function OpenDocNoHelp(e) {
    var keycode;
    var BranchName = $('#ddlBranch');
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_IMP_IBDDocNoHelp.aspx?BranchName=' + BranchName.val(), 400, 500, 'DocNoList');
        return false;
    }
}
function selectDocNo(DocNo,DueDate) {
    $("#txtDocNo").val(DocNo);
    $("#hdnDueDate").val(DueDate);
    __doPostBack('txtDocNo', '');
    return true;
}

function SubmitValidation() {
    var _DocNo = $("#txtDocNo");
    var _txtFinalDueDateMK = $("#TabContainerMain_tbMoneyMarket_txtFinalDueDateMK");
    var _txtDueDateRO = $("#TabContainerMain_tbRollOver_txtDueDateRO");
    if (_DocNo.val() == "") {
        alert('Document no can not be blank.');
        _DocNo.focus();
        return false;
    }
    if (_txtFinalDueDateMK.val() == "") {
        alert('Money Market Final Due Date can not be blank.');
        _txtFinalDueDateMK.focus();
        return false;
    }
    if (_txtDueDateRO.val() == "") {
        alert('Roll Over Due Date can not be blank.');
        _txtDueDateRO.focus();
        return false;
    }
    if (confirm('Are you sure you want to Submit this record to checker?')) {
        return true;
    }
    else {
        return false;
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
    // Money Market Fields
    var _txtCustomerNameMK = $("#TabContainerMain_tbMoneyMarket_txtCustomerNameMK");
    var _txtAccountCodeMK = $("#TabContainerMain_tbMoneyMarket_txtAccountCodeMK");
    var _txtCurrencyMK = $("#TabContainerMain_tbMoneyMarket_txtCurrencyMK");
    var _txtReferenceNumberMK = $("#TabContainerMain_tbMoneyMarket_txtReferenceNumberMK");
    var _txtSystemCodeMK = $("#TabContainerMain_tbMoneyMarket_txtSystemCodeMK");
    var _txtFrontNoMK = $("#TabContainerMain_tbMoneyMarket_txtFrontNoMK");
    var _txtNosOfDaysMK = $("#TabContainerMain_tbMoneyMarket_txtNosOfDaysMK");
    var _txtInterestMK = $("#TabContainerMain_tbMoneyMarket_txtInterestMK");
    var _txtFinalDueDateMK = $("#TabContainerMain_tbMoneyMarket_txtFinalDueDateMK");
    var _txtSpreadMK = $("#TabContainerMain_tbMoneyMarket_txtSpreadMK");
    var _txtBaseRateMK = $("#TabContainerMain_tbMoneyMarket_txtBaseRateMK");
    var _txtFundsMK = $("#TabContainerMain_tbMoneyMarket_txtFundsMK");
    var _txtSettlementMethodMK = $("#TabContainerMain_tbMoneyMarket_txtSettlementMethodMK");
    var _txtOurDipositoryMK = $("#TabContainerMain_tbMoneyMarket_txtOurDipositoryMK");
    var _txtCustomerAbbrMK = $("#TabContainerMain_tbMoneyMarket_txtCustomerAbbrMK");
    var _txtContraAccountMK = $("#TabContainerMain_tbMoneyMarket_txtContraAccountMK");
    var _txtAccountNoMK = $("#TabContainerMain_tbMoneyMarket_txtAccountNoMK");
    var _txtTheirDipository1MK = $("#TabContainerMain_tbMoneyMarket_txtTheirDipository1MK");
    var _txtTheirDipository2MK = $("#TabContainerMain_tbMoneyMarket_txtTheirDipository2MK");
    var _txtTheirDipository3MK = $("#TabContainerMain_tbMoneyMarket_txtTheirDipository3MK");
    var _txtTheirDipository4MK = $("#TabContainerMain_tbMoneyMarket_txtTheirDipository4MK");
    var _txtTheirAccountMK = $("#TabContainerMain_tbMoneyMarket_txtTheirAccountMK");
    var _txtATTNMK = $("#TabContainerMain_tbMoneyMarket_txtATTNMK");
    var _txtCurrentBalanceMK = $("#TabContainerMain_tbMoneyMarket_txtCurrentBalanceMK");
    var _txtValueDateMK = $("#TabContainerMain_tbMoneyMarket_txtValueDateMK");
    var _txtOperationDateMK = $("#TabContainerMain_tbMoneyMarket_txtOperationDateMK");
    var _txtSettlement1MK = $("#TabContainerMain_tbMoneyMarket_txtSettlement1MK");
    var _txtSettlement2MK = $("#TabContainerMain_tbMoneyMarket_txtSettlement2MK");
    var _txtLastModificationMK = $("#TabContainerMain_tbMoneyMarket_txtLastModificationMK");
    var _txtRollOverNoMK = $("#TabContainerMain_tbMoneyMarket_txtRollOverNoMK");
    var _txtLastTRNSNoMK = $("#TabContainerMain_tbMoneyMarket_txtLastTRNSNoMK");
    var _txtLastOperationMK = $("#TabContainerMain_tbMoneyMarket_txtLastOperationMK");
    var _txtRemEUCMK = $("#TabContainerMain_tbMoneyMarket_txtRemEUCMK");
    var _txtStatusMK = $("#TabContainerMain_tbMoneyMarket_txtStatusMK");
    var _txtOverDueAccrueMK = $("#TabContainerMain_tbMoneyMarket_txtOverDueAccrueMK");
    var _txtEntityMK = $("#TabContainerMain_tbMoneyMarket_txtEntityMK");

    // Roll Over Fields
    var _txtCustomerNameRO = $("#TabContainerMain_tbRollOver_txtCustomerNameRO");
    var _txtAccountCodeRO = $("#TabContainerMain_tbRollOver_txtAccountCodeRO");
    var _txtCurrencyRO = $("#TabContainerMain_tbRollOver_txtCurrencyRO");
    var _txtReferenceNoRO = $("#TabContainerMain_tbRollOver_txtReferenceNoRO");
    var _txtDivisionCodeRO = $("#TabContainerMain_tbRollOver_txtDivisionCodeRO");
    var _txtFrontNoRO = $("#TabContainerMain_tbRollOver_txtFrontNoRO");
    var _txtPledgedRO = $("#TabContainerMain_tbRollOver_txtPledgedRO");
    var _txtRollOverNoRO = $("#TabContainerMain_tbRollOver_txtRollOverNoRO");
    var _txtOperationDateRO = $("#TabContainerMain_tbRollOver_txtOperationDateRO");
    var _txtValueDateRO = $("#TabContainerMain_tbRollOver_txtValueDateRO");
    var _txtDueDateRO = $("#TabContainerMain_tbRollOver_txtDueDateRO");
    var _txtDaysRO = $("#TabContainerMain_tbRollOver_txtDaysRO");
    var _txtAmountRO = $("#TabContainerMain_tbRollOver_txtAmountRO");
    var _txtPrimeCCYRO = $("#TabContainerMain_tbRollOver_txtPrimeCCYRO");
    var _txtInterestRate1RO = $("#TabContainerMain_tbRollOver_txtInterestRate1RO");
    var _txtInterestRate2RO = $("#TabContainerMain_tbRollOver_txtInterestRate2RO");
    var _txtInterestAmountRO = $("#TabContainerMain_tbRollOver_txtInterestAmountRO");
    var _txtDaysAYearRO = $("#TabContainerMain_tbRollOver_txtDaysAYearRO");
    var _txtBaseRateRO = $("#TabContainerMain_tbRollOver_txtBaseRateRO");
    var _txtSpreadRO = $("#TabContainerMain_tbRollOver_txtSpreadRO");
    var _txtOverdueRO = $("#TabContainerMain_tbRollOver_txtOverdueRO");
    var _txtInterestPayerRO = $("#TabContainerMain_tbRollOver_txtInterestPayerRO");
    var _txtNonAccrueRO = $("#TabContainerMain_tbRollOver_txtNonAccrueRO");
    var _txtInterestOperationDateRO = $("#TabContainerMain_tbRollOver_txtInterestOperationDateRO");
    var _txtInterestValueDateRO = $("#TabContainerMain_tbRollOver_txtInterestValueDateRO");
    var _txtSettlementDateRO = $("#TabContainerMain_tbRollOver_txtSettlementDateRO");
    var _txtSettlementValueDateRO = $("#TabContainerMain_tbRollOver_txtSettlementValueDateRO");
    var _txtLastModificationDateRO = $("#TabContainerMain_tbRollOver_txtLastModificationDateRO");
    var _txtLastOperationDateRO = $("#TabContainerMain_tbRollOver_txtLastOperationDateRO");
    var _txtTransactionNoRO = $("#TabContainerMain_tbRollOver_txtTransactionNoRO");
    var _txtRealizedInterestTotalRO = $("#TabContainerMain_tbRollOver_txtRealizedInterestTotalRO");
    var _txtFundTypeRO = $("#TabContainerMain_tbRollOver_txtFundTypeRO");
    var _txtInternalRateRO = $("#TabContainerMain_tbRollOver_txtInternalRateRO");
    var _txtInterestRateChangeNoRO = $("#TabContainerMain_tbRollOver_txtInterestRateChangeNoRO");
    var _txtStatusCodeRO = $("#TabContainerMain_tbRollOver_txtStatusCodeRO");

    var _DueDate = $('#hdnDueDate');
    var _MoneyMarket = 'Y';
    var _RollOver = 'Y'
    if ($('#txtDocNo').val() != '' && _txtFinalDueDateMK.val() != '' && _txtDueDateRO.val() != '') {
        $.ajax({
            type: 'POST',
            url: 'TF_IMP_InquiryOfLedgerFile_Maker.aspx/SaveUpdateMoneyMarket',
            data: '{ _DueDate:"' + _DueDate.val() + '",_MoneyMarket:"' + _MoneyMarket + '",_txtCustomerNameMK:"' + _txtCustomerNameMK.val() + '",_txtAccountCodeMK:"' + _txtAccountCodeMK.val() + '",_txtCurrencyMK:"' + _txtCurrencyMK.val() +
            '",_txtReferenceNumberMK:"' + _txtReferenceNumberMK.val() + '",_txtSystemCodeMK:"' + _txtSystemCodeMK.val() + '",_txtFrontNoMK:"' + _txtFrontNoMK.val() + '",_txtNosOfDaysMK:"' + _txtNosOfDaysMK.val() +
            '",_txtInterestMK:"' + _txtInterestMK.val() + '",_txtFinalDueDateMK:"' + _txtFinalDueDateMK.val() + '",_txtSpreadMK:"' + _txtSpreadMK.val() + '",_txtBaseRateMK:"' + _txtBaseRateMK.val() +
            '",_txtFundsMK:"' + _txtFundsMK.val() + '",_txtSettlementMethodMK:"' + _txtSettlementMethodMK.val() + '",_txtOurDipositoryMK:"' + _txtOurDipositoryMK.val() + '",_txtCustomerAbbrMK:"' + _txtCustomerAbbrMK.val() +
            '",_txtContraAccountMK:"' + _txtContraAccountMK.val() + '",_txtAccountNoMK:"' + _txtAccountNoMK.val() + '",_txtTheirDipository1MK:"' + _txtTheirDipository1MK.val() + '",_txtTheirDipository2MK:"' + _txtTheirDipository2MK.val() +
            '",_txtTheirDipository3MK:"' + _txtTheirDipository3MK.val() + '",_txtTheirDipository4MK:"' + _txtTheirDipository4MK.val() + '",_txtTheirAccountMK:"' + _txtTheirAccountMK.val() + '",_txtATTNMK:"' + _txtATTNMK.val() +
            '",_txtCurrentBalanceMK:"' + _txtCurrentBalanceMK.val() + '",_txtValueDateMK:"' + _txtValueDateMK.val() + '",_txtOperationDateMK:"' + _txtOperationDateMK.val() + '",_txtSettlement1MK:"' + _txtSettlement1MK.val() +
            '",_txtSettlement2MK:"' + _txtSettlement2MK.val() + '",_txtLastModificationMK:"' + _txtLastModificationMK.val() + '",_txtRollOverNoMK:"' + _txtRollOverNoMK.val() + '",_txtLastTRNSNoMK:"' + _txtLastTRNSNoMK.val() +
            '",_txtLastOperationMK:"' + _txtLastOperationMK.val() + '",_txtRemEUCMK:"' + _txtRemEUCMK.val() + '",_txtStatusMK:"' + _txtStatusMK.val() + '",_txtOverDueAccrueMK:"' + _txtOverDueAccrueMK.val() +
            '",_txtEntityMK:"' + _txtEntityMK.val() +

            '",_RollOver:"' + _RollOver + '",_txtCustomerNameRO:"' + _txtCustomerNameRO.val() + '",_txtAccountCodeRO:"' + _txtAccountCodeRO.val() + '",_txtCurrencyRO:"' + _txtCurrencyRO.val() +
            '",_txtReferenceNoRO:"' + _txtReferenceNoRO.val() + '",_txtDivisionCodeRO:"' + _txtDivisionCodeRO.val() + '",_txtFrontNoRO:"' + _txtFrontNoRO.val() + '",_txtPledgedRO:"' + _txtPledgedRO.val() +
            '",_txtRollOverNoRO:"' + _txtRollOverNoRO.val() + '",_txtOperationDateRO:"' + _txtOperationDateRO.val() + '",_txtValueDateRO:"' + _txtValueDateRO.val() + '",_txtDueDateRO:"' + _txtDueDateRO.val() +
            '",_txtDaysRO:"' + _txtDaysRO.val() + '",_txtAmountRO:"' + _txtAmountRO.val() + '",_txtPrimeCCYRO:"' + _txtPrimeCCYRO.val() + '",_txtInterestRate1RO:"' + _txtInterestRate1RO.val() +
            '",_txtInterestRate2RO:"' + _txtInterestRate2RO.val() + '",_txtInterestAmountRO:"' + _txtInterestAmountRO.val() + '",_txtDaysAYearRO:"' + _txtDaysAYearRO.val() + '",_txtBaseRateRO:"' + _txtBaseRateRO.val() +
            '",_txtSpreadRO:"' + _txtSpreadRO.val() + '",_txtOverdueRO:"' + _txtOverdueRO.val() + '",_txtInterestPayerRO:"' + _txtInterestPayerRO.val() + '",_txtNonAccrueRO:"' + _txtNonAccrueRO.val() +
            '",_txtInterestOperationDateRO:"' + _txtInterestOperationDateRO.val() + '",_txtInterestValueDateRO:"' + _txtInterestValueDateRO.val() + '",_txtSettlementDateRO:"' + _txtSettlementDateRO.val() + '",_txtSettlementValueDateRO:"' + _txtSettlementValueDateRO.val() +
            '",_txtLastModificationDateRO:"' + _txtLastModificationDateRO.val() + '",_txtLastOperationDateRO:"' + _txtLastOperationDateRO.val() + '",_txtTransactionNoRO:"' + _txtTransactionNoRO.val() + '",_txtRealizedInterestTotalRO:"' + _txtRealizedInterestTotalRO.val() +
            '",_txtFundTypeRO:"' + _txtFundTypeRO.val() + '",_txtInternalRateRO:"' + _txtInternalRateRO.val() + '",_txtInterestRateChangeNoRO:"' + _txtInterestRateChangeNoRO.val() + '",_txtStatusCodeRO:"' + _txtStatusCodeRO.val() + '"}',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            failure: '',
            error: ''
        });
    }
}


