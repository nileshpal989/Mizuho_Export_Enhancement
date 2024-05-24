function DialogAlert() {
    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = { id: "dialog", };
    var para = { id: "Paragraph" };
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table/>", table1);
    var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";

    $("body").append(div1);
    $("#divO").append(dialog);
    $("#dialog").append(Para1);
    $("#dialog").append(tablea);
    $("#tblDialog").append(tr1);

    var _ddlApproveReject = $("#ddlApproveReject");
    
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == '1') {
        $("#txtRejectReason").hide();
        $("#spnDialog").hide();
        $("#Paragraph").text("Do you want to approve transaction?");
        $("#lblReason").hide();
        $("#dialog").dialog({
            title: "Confirm",
            width: 400,
            modal: true,
            closeOnEscape: true,
            dialogClass: "AlertJqueryDisplay",
            hide: { effect: "explode", duration: 400 },
            buttons: [
            {
                text: "Yes", //"✔"
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    document.getElementById('btnApprove').click();
                }
            },
            {
                text: "No", //"✖"
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    if (_ddlApproveReject.val() == '2') {
        $("#txtRejectReason").val($("#hdnRejectReason").val());
        $("#txtRejectReason").show();
        $("#spnDialog").show();
        $("#Paragraph").text("Are you sure you want to reject transaction?");
        $("#lblReason").show();
        $("#dialog").dialog({
            title: "Confirm",
            width: 500,
            modal: true,
            closeOnEscape: true,
            dialogClass: "AlertJqueryDisplay",
            hide: { effect: "explode", duration: 400 },
            buttons: [
            {
                text: "Yes", //"✔"
                icon: "ui-icon-heart",
                click: function () {
                    if ($("#txtRejectReason").val().trim() != '') {
                        $(this).dialog("close");
                        $("#hdnRejectReason").val($("#txtRejectReason").val());
                        $("#txtRejectReason").val('');
                        document.getElementById('btnApprove').click();
                    }
                    else {
                        alert("Reject reason can not be blank.");
                        $("#txtRejectReason").focus();
                        return false;
                    }
                }
            },
            {
                text: "No", //"✖"
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#txtRejectReason").remove();
                    $("#ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    return true;
}

function SubmitCheck() {
    var _Branch = $("#ddlBranch").val();
    if (_Branch == '') {
        alert('Please Select Branch.');
        return false;
    }

    var _SwiftTypes = $("#ddlSwiftTypes").val();
    if (_SwiftTypes == '') {
        alert('Please Select Swift Type.');
        return false;
    }

    var _Receiver = $("#txt_Receiver").val();
    if (_Receiver == '') {
        alert('Please Select/Enter Receiver.');
        return false;
    }
    else if (_Receiver.match("^/")) {
        alert("Receiver start with /");
        return false;
    }
    else if (_Receiver.match("/$")) {
        alert("Receiver ends with /");
        return false;
    }

    else if (_Receiver.match("//")) {
        alert("Receiver content //");
        return false;
    }

    var _TransRefNo = $("#txt_TransRefNo").val();
    if (_TransRefNo == '') {
        alert('Please Enter Transaction Reference Number.');
        return false;
    }
    else if (_TransRefNo.match("^/")) {
        alert("Transaction Reference Number start with /");
        return false;
    }
    else if (_TransRefNo.match("/$")) {
        alert("Transaction Reference Number ends with /");
        return false;
    }
    else if (_TransRefNo.match("//")) {
        alert("Transaction Reference Number content //");
        return false;
    }

    var _Narrative1 = $("#Narrative1").val();
    if (_Narrative1 == '') {
        alert('Please Enter Narrative.');
        return false;
    }
}


function OpenNego_Remit_BankList(e) {
    var keycode;

    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {

        open_popup('../HelpForms/TF_OverseasBankLookUp.aspx?hNo=IMP', 450, 650, 'Nego_Remit_BankList');
        return false;
    }
}
function selectNego_Remit_Bank_IMP(SwiftCode, BankCode, BankName, BankAddress) {
    $("#txt_Receiver").val(SwiftCode);
    return true;
}
