function OnDocNextClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(1);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(1);
    }
    return false;
}
function OnSwiftPrevClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(0);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(0);
    }
    return false;
}
function OnSwiftNextClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(2);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(2);
    }
    return false;
}
function OnInstPrevClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(0);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(1);
    }
    return false;
}
function OnInstNextClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(2);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(3);
    }
    return false;
}
function OnAMLPrevClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(1);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(2);
    }
    return false;
}
function OnDocNextClick(index) {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}
function PageLoadFunctions()
{
    OnTheirRefNoChange();
    OnInvoiceNoChange();
}
function OnTheirRefNoChange() {
    var DocNo = $("#txtDocNo");
    var TheirRefNo = $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no");
    if (TheirRefNo.val() != '') {
        $.ajax({
            type: "POST",
            url: "TF_IMP_BOE_Maker.aspx/TheirRefNoChange",
            data: '{TheirRefNo:"' + TheirRefNo.val() + '",DocNo:"' + DocNo.val() + '"}',
            dataType: "JSON",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.d.TheirRefNoStatus == 'Exist') {
                    alert('This Bank Ref No [' + TheirRefNo.val() + '] already exists.');
                    TheirRefNo.focus();
                    //TheirRefNo.val('');
                    //return false;
                }
                else {

                }
            },
            failure: function () { },
            error: function () { }
        });
   }    
}
function OnInvoiceNoChange() {
    var DocNo = $("#txtDocNo");
    var InvoiceNo = $("#TabContainerMain_tbDocumentDetails_txt_Inv_No");
    var InvoiceNoArray = InvoiceNo.val().split(",");
    if (InvoiceNo.val()!=''){
    $.each(InvoiceNoArray, function (index, value) {
        $.ajax({
            type: "POST",
            url: "TF_IMP_BOE_Maker.aspx/InvoiceNoChange",
            data: "{InvoiceNo:'" + value + "',DocNo:'" + DocNo.val() + "'}",
            dataType: "JSON",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.d.InvoiceNoStatus == 'Exist') {
                    alert('This Invoice No [' + value + '] already exists.');
                    //InvoiceNo.focus();
                    //InvoiceNo.val('');
                    //return false;
                }
                else {

                }
            },
            failure: function () { },
            error: function () { }
        });
    });
    }
}
function Block_Backpsace(evnt) {

    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    //  alert(charCode);
    if (charCode == 8)
        return false;
    else
        return true;
}
function ValidateReason() {
    var _ddlApproveReject = $("#TabContainerMain_tbDocumentInstructions_ddlApproveReject");
    var _RejectReason = $("#TabContainerMain_tbDocumentInstructions_txtRejectReason");
    if (_ddlApproveReject.val() == '') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    else if (_ddlApproveReject.val() == '2' && _RejectReason.val() == '') {

        alert('Reject reason can not be blank.');
        _ddlApproveReject.val();
        _RejectReason.focus();
        return false;
    }
    else {
        document.getElementById('TabContainerMain_tbDocumentInstructions_btnSave').click();
    }
    return true;
}
function ViewSwiftMessage() {
    var ddl = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank");
    var Lodg = $("#lblCollection_Lodgment_UnderLC").text();
    if (ddl.val() != '') {
        if (ddl.val() == 'Local') {
            alert("Swift File can not create for Local bank type.");
            return false;
        }
        else {
            if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {

                var _txtDocNo = $("#txtDocNo").val();
                var _SWIFT_File_Type = '';

                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                    _SWIFT_File_Type = 'MT499';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _SWIFT_File_Type = 'MT999';
                }
                if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                    _SWIFT_File_Type = 'Prot499';
                }
                if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _SWIFT_File_Type = 'Prot999';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
                    _SWIFT_File_Type = 'MT734';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked')) {
                    _SWIFT_File_Type = 'MT799';
                }
                if (Lodg == "LodgmentUnderLC") {
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') && $("#hdnDocumentScrutiny").val() == '2') {
                        _SWIFT_File_Type = 'MT999LC';
                    }
                }

                var _ddl_Nego_Remit_Bank_Type = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val();
                if (_ddl_Nego_Remit_Bank_Type == '0') {
                    _ddl_Nego_Remit_Bank_Type = '';
                }

                if (Lodg == "LodgmentUnderLC") {
                    var ddlscrutiny = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val();
                    if (ddlscrutiny == 2) {
                        var _txt_Discrepancy1 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val();
                        if (_txt_Discrepancy1 == "") {
                            alert('Please Enter Discrepancy.');
                            $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").focus();
                            return false;
                        }
                        else {
                            var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                            winame.focus();
                            return false;
                        }
                    }
                    else {
                        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                        winame.focus();
                        return false;
                    }
                }
                else {
                    var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                    winame.focus();
                    return false;
                }
            }
            else {
                alert("Please Select File Type.");
                return false;
            }
        }
    }
    else {
        alert("Please select bank type.");
        return false;
    }

}
function ViewSFMSMessage() {
    var ddl = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank");
    var Lodg = $("#lblCollection_Lodgment_UnderLC").text();
    if (ddl.val() != '') {
        if (ddl.val() == 'Foreign') {
            alert("SFMS File can not create for Foreign bank type.");
            return false;
        }
        else {
            if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {

                var _txtDocNo = $("#txtDocNo").val();
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                    _SWIFT_File_Type = 'MT499';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _SWIFT_File_Type = 'MT999';
                }
                if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                    _SWIFT_File_Type = 'Prot499';
                }
                if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _SWIFT_File_Type = 'Prot999';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
                    _SWIFT_File_Type = 'MT734';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked')) {
                    _SWIFT_File_Type = 'MT799';
                }
                if (Lodg == "LodgmentUnderLC") {
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') && $("#hdnDocumentScrutiny").val() == '2') {
                        _SWIFT_File_Type = 'MT999LC';
                    }
                }

                var _ddl_Nego_Remit_Bank_Type = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val();
                if (_ddl_Nego_Remit_Bank_Type == '0') {
                    _ddl_Nego_Remit_Bank_Type = '';
                }
                if (Lodg == "LodgmentUnderLC") {
                    var ddlscrutiny = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val();
                    if (ddlscrutiny == 2) {
                        var _txt_Discrepancy1 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val();
                        if (_txt_Discrepancy1 == "") {
                            alert('Please Enter Discrepancy.');
                            $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").focus();
                            return false;
                        }
                        else {
                            var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                            winame.focus();
                            return false;
                        }
                    }
                    else {
                        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                        winame.focus();
                        return false;
                    }
                }
                else {
                    var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                    winame.focus();
                    return false;
                }
            }
            else {
                alert("Please Select File Type.");
                return false;
            }
        }
    }
    else {
        alert("Please select bank type.");
        return false;
    }
}
function ViewAMLReport() {
    var _SWIFT_File_Type = "AML";
    var _ddl_Nego_Remit_Bank_Type = "AML";
    var _txtDocNo = $("#txtDocNo").val();
    var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
    winame.focus();
    return false;
}
function DialogAlert() {
    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = {id: "dialog",};
    var para = {id: "Paragraph"};
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table>", table1);
    var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";



    var _ddlApproveReject = $("#TabContainerMain_tbAmlDetails_ddlApproveReject");
    var _ddl_Doc_Currency =$("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == '1') 
    {

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
                document.getElementById('TabContainerMain_tbDocumentInstructions_btnSave').click();
            }
        },
        {
            text: "No", //"✖"
            icon: "ui-icon-heart",
            click: function () {
                $(this).dialog("close");
                $("#TabContainerMain_tbAmlDetails_ddlApproveReject").val('-Select-')
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

        if ($("#hdnRejectReason").val() != '')
        {
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
                        document.getElementById('TabContainerMain_tbDocumentInstructions_btnSave').click();
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
                    $("#TabContainerMain_tbAmlDetails_ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    return true;
}
function ViewAMLReport() {
    var _SWIFT_File_Type = "AML";
    var _ddl_Nego_Remit_Bank_Type = "AML";
    var _txtDocNo = $("#txtDocNo").val();
    var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
    winame.focus();
    return false;
}