﻿function DialogAlert() {
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

    var _ddlApproveReject = $("#TabContainerMain_tbDocumentSwiftFile_ddlApproveReject");
    
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
                    document.getElementById('TabContainerMain_tbDocumentSwiftFile_btnSave').click();
                }
            },
            {
                text: "No", //"✖"
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#TabContainerMain_tbDocumentSwiftFile_ddlApproveReject").val('-Select-')
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
                        document.getElementById('TabContainerMain_tbDocumentSwiftFile_btnSave').click();
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
                    $("#TabContainerMain_tbDocumentSwiftFile_ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    return true;
}
function ViewSwiftMessage() {
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = $("#hdnNegoRemiBankType").val();
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").is(':checked')) {
        _SWIFT_File_Type = 'MT740';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").is(':checked')) {
        _SWIFT_File_Type = 'MT756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
        _SWIFT_File_Type = 'MT999756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
        _SWIFT_File_Type = 'MT799ACC';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").is(':checked')) {
        _SWIFT_File_Type = 'MT747';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    return false;
}
function ViewSFMSMessage() {
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = $("#hdnNegoRemiBankType").val();
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").is(':checked')) {
        _SWIFT_File_Type = 'MT740';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").is(':checked')) {
        _SWIFT_File_Type = 'MT756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
        _SWIFT_File_Type = 'MT999756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
        _SWIFT_File_Type = 'MT799ACC';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").is(':checked')) {
        _SWIFT_File_Type = 'MT747';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    return false;
}