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
    //var _RejectReason = $("#TabContainerMain_tbDocumentSwiftFile_txtRejectReason");
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
                    document.getElementById('btnSubmit').click();
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
                        document.getElementById('btnSubmit').click();
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