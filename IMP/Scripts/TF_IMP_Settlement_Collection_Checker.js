
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

   

    var _ddlApproveReject = $("#ddlApproveReject");
    var lblDoc_Curr=$("#lblDoc_Curr");
    if (_ddlApproveReject.val() == '') {
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

function OnDocNextClick(index) {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}
function ImportAccountingPrevClick() {
    var _DocType = $("#hdnDocType").val();
    var tabContainer = $get('TabContainerMain');
    if (_DocType == 'ACC') {
        tabContainer.control.set_activeTabIndex(1);
    }
    else {
        tabContainer.control.set_activeTabIndex(0);
    }

    return false;
}
function ImportAccountingNextClick(index) {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(2);
    var SubtabContainer = $get('TabContainerMain_tbDocumentAccounting_TabSubContainerACC');
    SubtabContainer.control.set_activeTabIndex(index);
    return false;
}
function GeneralOperationNextClick(index) {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(3);
    var SubtabContainer = $get('TabContainerMain_tbDocumentGO_TabSubContainerGO');
    SubtabContainer.control.set_activeTabIndex(index);
    return false;
}
function GO3_NextClick() {
    var _BranchName = $("#hdnBranchName").val();
    var tabContainer = $get('TabContainerMain');
    var SubtabContainer;

    if (_BranchName != 'Mumbai') {
        tabContainer.control.set_activeTabIndex(3);
        SubtabContainer = $get('TabContainerMain_tbDocumentGO_TabSubContainerGO');
        SubtabContainer.control.set_activeTabIndex(3);
    }
    else {
        tabContainer.control.set_activeTabIndex(4);
        SubtabContainer = $get('TabContainerMain_tbSwift_TabContainerSwift');
        SubtabContainer.control.set_activeTabIndex(0);
    }

    return false;
}
function SwiftPrevClick() {
    
    var _BranchName = $("#hdnBranchName").val();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(3);
    var SubtabContainer = $get('TabContainerMain_tbDocumentGO_TabSubContainerGO');
    if (_BranchName != 'Mumbai') {

        SubtabContainer.control.set_activeTabIndex(3);
    }
    else {
        SubtabContainer.control.set_activeTabIndex(2);
    }

    return false;
}
function SwiftNextClick(index) {
    
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(4);
    var SubtabContainer = $get('TabContainerMain_tbSwift_TabContainerSwift');
    SubtabContainer.control.set_activeTabIndex(index);
    return false;
}
function OnBackClick() {
    
    window.location.href = "TF_IMP_Settlement_Checker_View.aspx";
    return false;
}
function ViewSwiftMessage() {
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = 'FOREIGN';
    if ($("#TabContainerMain_tbSwift_rdb_swift_103").is(':checked')) {
        _SWIFT_File_Type = 'MT103';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_202").is(':checked')) {
        _SWIFT_File_Type = 'MT202';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbSwift_rdb_swift_200").is(':checked')) {
        _SWIFT_File_Type = 'MT200';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    //------------------------------------------------------Nilesh-------------------------------------------------------------
    if ($("#TabContainerMain_tbSwift_rdb_swift_754").is(':checked')) {
        _SWIFT_File_Type = 'MT754';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    //------------------------------------------------------Nilesh END------------------------------------------------------------


    if ($("#TabContainerMain_tbSwift_rdb_swift_R42").is(':checked')) {
        _SWIFT_File_Type = 'R42';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    return false;
}