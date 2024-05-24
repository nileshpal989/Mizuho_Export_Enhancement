function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}
function OnBackClick() {
    SaveUpdateData();
    window.location.href = "TF_IMP_ReversalOfLiability_Maker_View.aspx";
    return false;
}
function SubmitValidation() {
    SaveUpdateData();
    SubmitConfirm();
}
function SubmitConfirm() {
    if (confirm('Are you sure you want to Submit this record to checker?')) {
        $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").val('');
        SubmitToChecker();
    }
    else {
        return false;
    }
}

function SubmitToChecker() {
    var DocNo = $("#txtRefNo").val();
    var UserName = $("#hdnUserName").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_ReversalOfLiability_Maker.aspx/SubmitToChecker",
        data: '{UserName:"' + UserName + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d == "Submit") {
                window.location.href = "TF_IMP_ReversalOfLiability_Maker_View.aspx?result=Submit";
            }
            else {
                alert(data.Result);
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}
function SaveUpdateData() {
    var _hdnUserName = $("#hdnUserName").val();
    var _txtRefNo = $("#txtRefNo").val();
    var _hdnBranchCode = $("#hdnBranchCode").val();
    var _txtAmount = $("#txtAmount").val();
    var _txtCurrency = $("#txtCurrency").val();
    var _txtCustomerAcNo = $("#txtCustomerAcNo").val();
    var _txtCCode = $("#txtCCode").val();
    var _txtValueDate = $("#txtValueDate").val();
    var _txtExpiryDate = $("#txtExpiryDate").val();
    var _ddlAmendmentOption = $("#ddlAmendmentOption").val();
    var _txtAAmount = $("#txtAAmount").val();
    var _txtACurrency = $("#txtACurrency").val();
    var _txtRemarks = $("#txtRemarks").val();
    var _txtApplicationNo = $("#txtApplicationNo").val();
    var _txtApplyingBranch = $("#txtApplyingBranch").val();
    var _txtAdvisingBank = $("#txtAdvisingBank").val();
    var _ddlAttentionCode = $("#ddlAttentionCode").val();
    var _txtCDCCY1 = $("#txtCDCCY1").val();
    var _txtCDAmount1 = $("#txtCDAmount1").val();
    var _txtCDCCY2 = $("#txtCDCCY2").val();
    var _txtCDAmount2 = $("#txtCDAmount2").val();
    var _txtCDCCY3 = $("#txtCDCCY3").val();
    var _txtCDAmount3 = $("#txtCDAmount3").val();
    var _txtDCACShortName1 = $("#txtDCACShortName1").val();
    var _txtDCCustAbbr1 = $("#txtDCCustAbbr1").val();
    var _txtDCAccountNo1 = $("#txtDCAccountNo1").val();
    var _txtDCCCY1 = $("#txtDCCCY1").val();
    var _txtDCAmount1 = $("#txtDCAmount1").val();
    var _txtDCACShortName2 = $("#txtDCACShortName2").val();
    var _txtDCCustAbbr2 = $("#txtDCCustAbbr2").val();
    var _txtDCAccountNo2 = $("#txtDCAccountNo2").val();
    var _txtDCCCY2 = $("#txtDCCCY2").val();
    var _txtDCAmount2 = $("#txtDCAmount2").val();
    var _txtDCACShortName3 = $("#txtDCACShortName3").val();
    var _txtDCCustAbbr3 = $("#txtDCCustAbbr3").val();
    var _txtDCAccountNo3 = $("#txtDCAccountNo3").val();
    var _txtDCCCY3 = $("#txtDCCCY3").val();
    var _txtDCAmount3 = $("#txtDCAmount3").val();
    if (_txtRefNo != '' && _txtAmount != '' && _txtCurrency!='') {
        $.ajax({
            type: "POST",
            url: "TF_IMP_ReversalOfLiability_Maker.aspx/AddUpdate",
            data: '{ hdnUserName:"' + _hdnUserName + '", hdnBranchCode :"' + _hdnBranchCode + '", _txtRefNo :"' + _txtRefNo + '",_txtAmount :"' + _txtAmount + '",_txtCurrency :"' + _txtCurrency + '",_txtCustomerAcNo :"' + _txtCustomerAcNo + '",_txtCCode :"' + _txtCCode +
            '",_txtValueDate : "' + _txtValueDate + '", _txtExpiryDate : "' + _txtExpiryDate + '", _ddlAmendmentOption : "' + _ddlAmendmentOption + '", _txtAAmount : "' + _txtAAmount + '", _txtACurrency : "' + _txtACurrency + '", _txtRemarks : "' + _txtRemarks + '", _txtApplicationNo : "' + _txtApplicationNo +
            '", _txtApplyingBranch : "' + _txtApplyingBranch + '", _txtAdvisingBank : "' + _txtAdvisingBank + '", _ddlAttentionCode : "' + _ddlAttentionCode + '", _txtCDCCY1 : "' + _txtCDCCY1 + '", _txtCDAmount1 : "' + _txtCDAmount1 + '", _txtCDCCY2 : "' + _txtCDCCY2 + '", _txtCDAmount2 : "' + _txtCDAmount2 +
            '", _txtCDCCY3 : "' + _txtCDCCY3 + '", _txtCDAmount3 : "' + _txtCDAmount3 + '",_txtDCACShortName1 : "' + _txtDCACShortName1 + '", _txtDCCustAbbr1 : "' + _txtDCCustAbbr1 + '", _txtDCAccountNo1 : "' + _txtDCAccountNo1
            + '", _txtDCCCY1 : "' + _txtDCCCY1 + '", _txtDCAmount1 : "' + _txtDCAmount1 + '",_txtDCACShortName2 : "' + _txtDCACShortName2 + '", _txtDCCustAbbr2 : "' + _txtDCCustAbbr2 + '", _txtDCAccountNo2 : "' + _txtDCAccountNo2 +
            '", _txtDCCCY2 : "' + _txtDCCCY2 + '", _txtDCAmount2 : "' + _txtDCAmount2 + '", _txtDCACShortName3 : "' + _txtDCACShortName3 + '", _txtDCCustAbbr3 : "' + _txtDCCustAbbr3 +
            '", _txtDCAccountNo3 : "' + _txtDCAccountNo3 + '", _txtDCCCY3 : "' + _txtDCCCY3 + '", _txtDCAmount3 : "' + _txtDCAmount3 + '"}',

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
}
function OpenCustomerCodeList(e) {
    var keycode;
    var hdnBranchName = document.getElementById('hdnBranchName');
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        var txtCustomer_ID = $get("txtCustomerAcNo");
        open_popup('../HelpForms/TF_IMP_CustomerHelp.aspx?BranchName=' + hdnBranchName.value + '&CustID=' + txtCustomer_ID.value, 400, 500, 'CustomerCodeList');
        return false;
    }
}
function selectCustomer(selectedID, CustName) {
    $("#txtCustomerAcNo").val(selectedID);
    $("#lblCustomerDesc").text(CustName);
    return true;
}