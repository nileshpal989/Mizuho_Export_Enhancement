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

    var _ddlApproveReject = $("#ddlApproveReject");
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == '1') {

        $("body").append(div1);
        $("#divO").append(dialog);
        $("#dialog").append(Para1);
        $("#dialog").append(tablea);
        $("#tblDialog").append(tr1);

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
                        document.getElementById('btnSave').click();
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
        $("body").append(div1);
        $("#divO").append(dialog);
        $("#dialog").append(Para1);
        $("#dialog").append(tablea);
        $("#tblDialog").append(tr1);

        if ($("#hdnRejectReason").val() != '') {
            $("#txtRejectReason").val($("#hdnRejectReason").val());
        }
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
                            document.getElementById('btnSave').click();
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

function DialogAlertLodgement() {

    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = { id: "dialog", };
    var para = { id: "Paragraph" };
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table>", table1);
    var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";

    var _ddlApproveReject = $("#TabContainerMain_tbCoveringScheduleDetails_ddlApproveReject");
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == '1') {

        $("body").append(div1);
        $("#divO").append(dialog);
        $("#dialog").append(Para1);
        $("#dialog").append(tablea);
        $("#tblDialog").append(tr1);

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
                        document.getElementById('TabContainerMain_tbCoveringScheduleDetails_btnSave').click();
                    }
                },
                {
                    text: "No", //"✖"
                    icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog("close");
                        $("#TabContainerMain_tbCoveringScheduleDetails_ddlApproveReject").val('-Select-')
                        return false;
                    }
                }
            ]
        });
        $('.ui-dialog :button').blur();

    }
    if (_ddlApproveReject.val() == '2') {
        $("body").append(div1);
        $("#divO").append(dialog);
        $("#dialog").append(Para1);
        $("#dialog").append(tablea);
        $("#tblDialog").append(tr1);

        if ($("#hdnRejectReason").val() != '') {
            $("#txtRejectReason").val($("#hdnRejectReason").val());
        }
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
                            document.getElementById('TabContainerMain_tbCoveringScheduleDetails_btnSave').click();
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

function DialogAlertRealisation() {
    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = { id: "dialog", };
    var para = { id: "Paragraph" };
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table>", table1);
    var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";

   // var _ddlApproveReject = $("#TabContainerMain_tbNormalGO_ddlApproveReject");
   var _ddlApproveReject = $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_ddlApproveReject");
   
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == '1') {

        $("body").append(div1);
        $("#divO").append(dialog);
        $("#dialog").append(Para1);
        $("#dialog").append(tablea);
        $("#tblDialog").append(tr1);

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
                        document.getElementById('btnSave').click();
                    }
                },
                {
                    text: "No", //"✖"
                    icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog("close");                        
                      //  $("#TabContainerMain_tbNormalGO_ddlApproveReject").val('-Select-')
                        $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_ddlApproveReject").val('-Select-')
                        return false;
                    }
                }
            ]
        });
        $('.ui-dialog :button').blur();

    }
    if (_ddlApproveReject.val() == '2') {
        $("body").append(div1);
        $("#divO").append(dialog);
        $("#dialog").append(Para1);
        $("#dialog").append(tablea);
        $("#tblDialog").append(tr1);

        if ($("#hdnRejectReason").val() != '') {
            //$("#TabContainerMain_tbNormalGO_txtRejectReason").val($("#hdnRejectReason").val());
            $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtRejectReason").val($("#hdnRejectReason").val());
        }
        $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtRejectReason").show();        
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
                        var rejectreson = $("#txtRejectReason").val();
                        if ($("#txtRejectReason").val() != '') {
                            $(this).dialog("close");
                            $("#hdnRejectReason").val($("#txtRejectReason").val());
                            $("#txtRejectReason").val('');                            
                            document.getElementById('btnSave').click();
                        }
                        else {
                            alert("Reject reason can not be blank.");
                            $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtRejectReason").focus();
                            return false;
                        }
                    }
                },
                {
                    text: "No", //"✖"
                    icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog("close");
                        $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtRejectReason").remove();
                        $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_ddlApproveReject").val('-Select-')
                        return false;
                    }
                }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    return true;
}