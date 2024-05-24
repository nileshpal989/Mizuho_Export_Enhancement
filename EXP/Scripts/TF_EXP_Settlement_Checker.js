function OnDocNextClick(index) {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}
function OnShippPrevClick() {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(0);
    return false;
}
function OnShippNextClick() {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(2);
    return false;
}
function ImportAccountingPrevClick() {
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(1);

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
    debugger
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(3);
    var SubtabContainer = $get('TabContainerMain_tbGeneralOperation_TabSubContainerGO');
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
