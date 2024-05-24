function confirmmessage() {
    var filename = document.getElementById('FileUpload1').value;
    var ddlBranch = document.getElementById('ddlBranch').value;
    var branchname = $("#ddlBranch option:selected").text();
    if (filename == '') {
        Alert('Please Select File.')
        return false;
    }
    if (branchname == '---Select---') {
        Alert('Select Branch')
        return false;
    }
    else {
        var message = confirm('Are you sure want to upload this file in ' + branchname + ' Branch?')

        if (message == true) {
            ShowProgress();
        }
        else {
            return false;
        }
    }
}
function Validate(Result) {
    var ddlBranch = document.getElementById('ddlBranch').value;
    var branchname = $("#ddlBranch option:selected").text();
    $("#Paragraph").text(Result);
    $("#dialog").dialog({
        title: "Message From LMCC",
        width: 520,
        modal: true,
        closeOnEscape: true,
        dialogClass: "AlertJqueryDisplay",
        hide: { effect: "close", duration: 400 },
        buttons: [
            {
                text: "Ok",
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");

                    popup = window.open('EBRC_Rpt_Data_Validation.aspx?Branch=' + branchname, '', '_blank', 'height=600,  width=900,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100');

                    $("").focus();
                }
            }
        ]
    });
    $('.ui-dialog :button').blur();
    return false;
}
function CallConfirmBox() {

    $("[id$=btnExport]").click();

}
function ShowProgress() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
    }, 200);
}
function ShowProgressValidate() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modalValidate");
        $('body').append(modal);
        var loading = $(".loadingValidate");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
    }, 200);
}
function ShowProgressProcess() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modalProcess");
        $('body').append(modal);
        var loading = $(".loadingProcess");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
    }, 200);
}

function DialogAlert() {

    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = { id: "dialog", };
    var para = { id: "Paragraph" };
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table>", table1);
    var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";

    $("body").append(div1);
    $("#divO").append(dialog);
    $("#dialog").append(Para1);
    $("#dialog").append(tablea);
    $("#tblDialog").append(tr1);

    var _ddlApproveReject = $("#ddlstatus");
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == 'A') {
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
                    //icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog("close");
                        $("[id$=btnAdd]").click();
                    }
                },

                {
                    text: "No", //"✖"
                    //   icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog("close");
                        $("#ddlstatus").val('0')
                        return false;
                    }
                }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    if (_ddlApproveReject.val() == 'R') {
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
                    //icon: "ui-icon-heart",
                    click: function () {
                        if ($("#txtRejectReason").val().trim() != '') {
                            $(this).dialog("close");
                            $("#hdnRejectReason").val($("#txtRejectReason").val());
                            $("#txtRejectReason").val('');
                            document.getElementById('btnAdd').click();

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
                    // icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog("close");
                        $("#txtRejectReason").remove();
                        $("#ddlstatus").val('0')
                        return false;
                    }
                }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    return true;
}


function ORMFIleUPLOADERRORCHECK(_result, result) {

    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = { id: "dialog", };
    var para = { id: "Paragraph" };
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table>", table1);
    var tr1 = "<tr><td width='17%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";


    $("body").append(div1);
    $("#divO").append(dialog);
    $("#dialog").append(Para1);
    $("#dialog").append(tablea);
    $("#tblDialog").append(tr1);

    $("#spnDialog").hide();
    $("#Paragraph").text(_result + "due to error in excel file.Do you want to Process transaction which are uploaded?");
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
                //icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#btnValidate").prop("disabled", false);
                    $("#btnProcess").prop("disabled", false);
                    VAlert(result);
                }
            },

            {
                text: "No", //"✖"
                //   icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#btnProcess").prop("disabled", true);
                    $("#btnValidate").prop("disabled", true);
                    VAlert('Click On ok to see the error records....');
                    return false;
                }
            }
        ]
    });
    $('.ui-dialog :button').blur();
    return true;
}
function VAlert(Result) {
    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = { id: "dialog", };
    var para = { id: "Paragraph" };
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var Para1 = $("<p>", para);
    $("body").append(div1);
    $("#divO").append(dialog);
    $("#dialog").append(Para1);
    $("#Paragraph").text(Result + "due to error in excel file.Correct it and upload again..");
    $("#dialog").dialog({
        title: "Message From LMCC",
        width: 400,
        modal: true,
        closeOnEscape: true,
        dialogClass: "AlertJqueryDisplay",
        hide: { effect: "explode", duration: 400 },
        buttons: [
            {
                text: "Ok",
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#btnProcess").prop("disabled", true);
                    $("#btnValidate").prop("disabled", true);
                    $("").focus();                    
                    $("[id$=btnrecordnotuploaded]").click();
                    
                }
            }
        ]
    });
    $('.ui-dialog :button').blur();
    return false;
}