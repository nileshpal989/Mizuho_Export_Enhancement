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
// Changes Shailesh on 26072023

function OpenGO_help(IMP_ACC) {
    var chk_Generaloperation1 = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_chk_Generaloperation1');
    if (chk_Generaloperation1.checked == true) {
        var txtGO_Debit = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit');
        if (txtGO_Debit.value != 'C' && Debit_Credit == 'Debit1' && IMP_ACC == 'GO1') {
            alert('Please select CREDIT.');
            return false;
        }
        var txtGO_Credit = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit');
        if (txtGO_Credit.value != 'C' && Debit_Credit == 'Debit2' && IMP_ACC == 'GO1') {
            alert('Please select CREDIT.');
            return false;
        }
    }
}
function OpenFCRefNo_help(e, IMPFCRefNo) {
    var CustAcno = document.getElementById('TabContainerMain_tbDocumentDetails_txtCustAcNo');
    if (CustAcno.value == '') {
        alert('Enter Customer AcNo.');
        return false;
    }
    else {
        var keycode, CustAbbr;
        CustAbbr = document.getElementById('hdnCustabbr').value;
        var BranchName = document.getElementById('hdnBranchNameEXPAc').value;
        if (window.event) keycode = window.event.keyCode;
        if (keycode == 113 || e == 'mouseClick') {
            open_popup('../EXP/HelpForms/TF_EXP_FXHelp.aspx?IMPFCRefNo=' + IMPFCRefNo + '&CustAbbr=' + CustAbbr + '&BranchName=' + BranchName, 500, 700, 'FXRefNoList');
            return false;

        }
    }
}
function OpenGO_help(e, IMP_ACC, Debit_Credit) {
    var chk_Generaloperation1 = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_chk_Generaloperation1');
    if (chk_Generaloperation1.checked == true) {
        var txtGO_Debit = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit');
        if (txtGO_Debit.value != 'C' && Debit_Credit == 'Debit1' && IMP_ACC == 'GO1') {
            alert('Please select CREDIT.');
            return false;
        }
        var txtGO_Credit = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit');
        if (txtGO_Credit.value != 'C' && Debit_Credit == 'Debit2' && IMP_ACC == 'GO1') {
            alert('Please select CREDIT.');
            return false;
        }
    }

    var chk_Generaloperation2 = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_chk_Generaloperation2');
    if (chk_Generaloperation2.checked == true) {
        var txtNGO_Debit = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit');
        if (txtNGO_Debit.value != 'C' && Debit_Credit == 'Debit1' && IMP_ACC == 'GO2') {
            alert('Please select CREDIT.');
            return false;
        }
        var txtNGO_Credit = document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit');
        if (txtNGO_Credit.value != 'C' && Debit_Credit == 'Debit2' && IMP_ACC == 'GO2') {
            alert('Please select CREDIT.');
            return false;
        }
    }
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../EXP/HelpForms/TF_EXP_SundryaccountHelp1.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
        return false;
    }
}
function selectFX(IMPFCRefNo, GbaseRefNo, ContractDate, InternalRate, ExchangeRate, ContractAmount, ContractCurrency, EquivalentAmount, EquivalentCurrency) {
    //CheckFXREFno(IMPFCRefNo, GbaseRefNo);
    if (IMPFCRefNo == 'IMPFCRefNo1') {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_FCRefNo').value = GbaseRefNo;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_Curr').value = EquivalentCurrency;
        //document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Acceptance_Curr').value = EquivalentCurrency;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Ex_rate').value = ExchangeRate;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_Princ_Intnl_Ex_rate').value = InternalRate;
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC1_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC1');
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_FCRefNo').focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo2') {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_FCRefNo').value = GbaseRefNo;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_Curr').value = EquivalentCurrency;
        //document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Acceptance_Curr').value = EquivalentCurrency;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Ex_rate').value = ExchangeRate;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_Princ_Intnl_Ex_rate').value = InternalRate;
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC2_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC2');
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_FCRefNo').focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo3') {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_FCRefNo').value = GbaseRefNo;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_Curr').value = EquivalentCurrency;
        //document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Acceptance_Curr').value = EquivalentCurrency;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Ex_rate').value = ExchangeRate;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_Princ_Intnl_Ex_rate').value = InternalRate;
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC3_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC3');
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_FCRefNo').focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo4') {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_FCRefNo').value = GbaseRefNo;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_Curr').value = EquivalentCurrency;
        //document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Acceptance_Curr').value = EquivalentCurrency;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Ex_rate').value = ExchangeRate;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_Princ_Intnl_Ex_rate').value = InternalRate;
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC4_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC4');
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_FCRefNo').focus();
    }
    if (IMPFCRefNo == 'IMPFCRefNo5') {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_FCRefNo').value = GbaseRefNo;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_Curr').value = EquivalentCurrency;
        //document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Acceptance_Curr').value = EquivalentCurrency;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Ex_rate').value = ExchangeRate;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_Princ_Intnl_Ex_rate').value = InternalRate;
        alert('1. GBase ref no- ' + GbaseRefNo + '. \n2. Contract Date- ' + ContractDate + '.\n3. Contract Amount- ' + ContractAmount + '.\n4. Contract Currency- ' + ContractCurrency + '.\n5. Equivalent Currency- ' + EquivalentCurrency + '.\n6. Equivalent Amount- ' + EquivalentAmount + '.')
        //IMPACC5_DR_Amt_Calculation();
        Disc_DR_Amt_Calculation('ACC5');
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_FCRefNo').focus();
    }
    return false;
}
///////  added by shailesh
function Disc_DR_Amt_Calculation(IMP_Accounting) {
   
    var _txt_DiscAmt = document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DiscAmt').value;
    var _txtprinc_Ex_rate = document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_Princ_Ex_rate').value;
    var _txtprinc_Intnl_Ex_rate = document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_Princ_Intnl_Ex_rate').value;
    var _Princ_Ex_Curr = document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_CR_Acceptance_Curr').value.toUpperCase();
    var _txt_DR_Cur_Acc_amt;

    //document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DR_Cur_Acc_amt').value = _txt_DiscAmt;
    if (_txtprinc_Ex_rate == '') {
        _txtprinc_Ex_rate = 1;
    }
    if (_txt_DiscAmt == '') {
        _txt_DiscAmt = 0;
    }
    _txt_DiscAmt = parseFloat(_txt_DiscAmt);
    _txtprinc_Ex_rate = parseFloat(_txtprinc_Ex_rate);
    if (_txtprinc_Ex_rate != 1 && _txtprinc_Ex_rate != 0) {
        if ($.isNumeric(_txt_DiscAmt) && $.isNumeric(_txtprinc_Ex_rate)) {
            _txt_DR_Cur_Acc_amt = parseFloat(_txt_DiscAmt * _txtprinc_Ex_rate);
            if (_Princ_Ex_Curr == 'INR') {
                _txt_DR_Cur_Acc_amt = parseFloat(round(parseFloat(_txt_DR_Cur_Acc_amt), 0)).toFixed(2);
                document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_CR_Acceptance_amt').value = _txt_DR_Cur_Acc_amt;
            }
            else {
                _txt_DR_Cur_Acc_amt = parseFloat(_txt_DR_Cur_Acc_amt).toFixed(2);
                document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_CR_Acceptance_amt').value = _txt_DR_Cur_Acc_amt;
            }
        }
        _txt_DiscAmt = parseFloat(_txt_DiscAmt).toFixed(2);
        _txtprinc_Ex_rate = parseFloat(_txtprinc_Ex_rate).toFixed(5);
        if (_txtprinc_Intnl_Ex_rate != '') {
            _txtprinc_Intnl_Ex_rate = parseFloat(_txtprinc_Intnl_Ex_rate).toFixed(5);
            document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_Princ_Intnl_Ex_rate').value = _txtprinc_Intnl_Ex_rate;
        }

        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DiscAmt').value = _txt_DiscAmt;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_Princ_Ex_rate').value = _txtprinc_Ex_rate;
        
    }
    else {
        _txt_DiscAmt = parseFloat(_txt_DiscAmt).toFixed(2);
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DiscAmt').value = _txt_DiscAmt;
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_CR_Acceptance_amt').value = _txt_DiscAmt;
    }
}
function Disc_DR_Curr_Toggel(IMP_Accounting) {
    var _txt_Princ_Ex_Curr = document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_Princ_Ex_Curr').value;
    var _Document_Curr = document.getElementById('TabContainerMain_tbDocumentDetails_txtCurrency').text();
    if (_txt_Princ_Ex_Curr == '') {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DR_Cur_Acc_Curr').value = _Document_Curr;
    }
    else {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DR_Cur_Acc_Curr').value = _txt_Princ_Ex_Curr;
    }
    Disc_DR_Amt_Calculation(IMP_Accounting);
}
function round(values, decimals) {
    return Number(Math.round(values + 'e' + decimals) + 'e-' + decimals);
}

function DR_Curr_Toggel(IMP_Accounting, Commission, DR_Curr) {

    var Ex_Curr = document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_' + Commission + '_Ex_Curr').value;
    var Commission_Curr = document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_CR_' + Commission + '_Curr').value;
    var Document_Curr = document.getElementById('TabContainerMain_tbDocumentDetails_txtCurrency').text();
    var Their_Comm_Curr = document.getElementById('ACC_Their_Comm_Curr').value;

    if (Ex_Curr == '') {
        if (IMP_Accounting == 'ACC1') {
            document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DR_Cur_Acc_' + DR_Curr).val(Their_Comm_Curr);
        }
        else {
            document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DR_Cur_Acc_' + DR_Curr).val(Commission_Curr);
        }
    }
    else {
        document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanel' + IMP_Accounting + '_txt_IMP' + IMP_Accounting + '_DR_Cur_Acc_' + DR_Curr).val(Ex_Curr);
    }
    DR_Amt_Calculation(IMP_Accounting, Commission);
}

////////  added by shailesh  25072023
function OpenCR_Code_help(e, IMP_ACC, Debit_Credit) {
    var keycode;
    var BRCode = document.getElementById('hdnBranchNameEXPAc');
    var ACCode = document.getElementById('TabContainerMain_tbDocumentDetails_txtCustAcNo');
    var relcur = document.getElementById('TabContainerMain_tbDocumentDetails_txt_relcur');
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../EXP/HelpForms/TF_EXP_SundryaccountHelp.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit + '&BRCode=' + BRCode.value + '&ACCode=' + ACCode.value + '&Curr=' + relcur.value, 500, 550, 'SundryCodeList');
        return false;
        //../EXP/HelpForms/TF_EXP_FXHelp.aspx
    }
}

//------------Anand 13-08-2023------------------------------

//function OpenCR_Code_help(e, IMP_ACC, Debit_Credit) {
//    var keycode;
//    if (window.event) keycode = window.event.keyCode;
//    if (keycode == 113 || e == 'mouseClick') {
//        open_popup('../HelpForms/TF_EXP_SundryaccountHelp.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
//        return false;
//    }
//}

function select_CR_Code1(ACcode, ABB, Acno, ACcodeDiscreption, CCY) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Cust_Acc').value = Acno;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_CR_Acceptance_Curr').value = CCY;
    Disc_DR_Amt_Calculation('ACC1');
}

function select_DR_Code1(ACcode, ABB, Acno, ACcodeDiscreption) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC1_txt_IMPACC1_DR_Cust_Acc').value = Acno;
}
function select_CR_Code2(ACcode, ABB, Acno, ACcodeDiscreption, CCY) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Cust_Acc').value = Acno;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_CR_Acceptance_Curr').value = CCY;
    Disc_DR_Amt_Calculation('ACC2');
}
function select_DR_Code2(ACcode, ABB, Acno, ACcodeDiscreption) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC2_txt_IMPACC2_DR_Cust_Acc').value = Acno;
}
function select_CR_Code3(ACcode, ABB, Acno, ACcodeDiscreption, CCY) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Cust_Acc').value = Acno;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_CR_Acceptance_Curr').value = CCY;
    Disc_DR_Amt_Calculation('ACC3');
}
function select_DR_Code3(ACcode, ABB, Acno, ACcodeDiscreption) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC3_txt_IMPACC3_DR_Cust_Acc').value = Acno;
}
function select_CR_Code4(ACcode, ABB, Acno, ACcodeDiscreption, CCY) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Cust_Acc').value = Acno;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_CR_Acceptance_Curr').value = CCY;
    Disc_DR_Amt_Calculation('ACC4');
}
function select_DR_Code4(ACcode, ABB, Acno, ACcodeDiscreption) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC4_txt_IMPACC4_DR_Cust_Acc').value = Acno;
}
function select_CR_Code5(ACcode, ABB, Acno, ACcodeDiscreption, CCY) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Cust_Acc').value = Acno;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_CR_Acceptance_Curr').value = CCY;
    Disc_DR_Amt_Calculation('ACC5');
}
function select_DR_Code5(ACcode, ABB, Acno, ACcodeDiscreption) {

    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Code').value = ACcode;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_AC_Short_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_abbr').value = ABB;
    document.getElementById('TabContainerMain_tbDocumentAccounting_TabSubContainerACC_TabPanelACC5_txt_IMPACC5_DR_Cust_Acc').value = Acno;
}


function select_GO1Right1(ACCCode, CustAbb, Acno, Description, CCY) {
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_Cust_AcCode').value = ACCCode;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_Cust').value = CustAbb;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_Cust_Name').value = Description;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_Cust_AccNo').value = Acno;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_CCY').value = CCY;
}
function select_GO1Right2(ACCCode, CustAbb, Acno, Description, CCY) {
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_Cust_AcCode').value = ACCCode;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit_Cust').value = CustAbb;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit_Cust_Name').value = Description;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit_Cust_AccNo').value = Acno;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit_CCY').value = CCY;
}

function select_GO2Right1(ACCCode, CustAbb, Acno, Description, CCY) {
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit_Cust_AcCode').value = ACCCode;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit_Cust').value = CustAbb;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit_Cust_Name').value = Description;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit_Cust_AccNo').value = Acno;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit_CCY').value = CCY;
}
function select_GO2Right2(ACCCode, CustAbb, Acno, Description, CCY) {
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit_Cust_AcCode').value = ACCCode;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit_Cust').value = CustAbb;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit_Cust_Name').value = Description;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit_Cust_AccNo').value = Acno;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit_CCY').value = CCY;
}

function select_GO4_CR_Code(ACcode, ABB, Acno, ACcodeDiscreption, CCY) {
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AcCode').value = ACcode;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust').value = ABB;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Cust_AccNo').value = Acno;
    //document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Curr').value = CCY;

}
function select_GO4_DR_Code(ACcode, ABB, Acno, ACcodeDiscreption, CCY) {
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AcCode').value = ACcode;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_Name').value = ACcodeDiscreption;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust').value = ABB;
    document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Cust_AccNo').value = Acno;
    //document.getElementById('TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Curr').value = CCY;


}
function select_GO1_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Debit_Cust_AcCode_Name").val(GLcodeDiscreption);
}
function select_GO1_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_pnlGeneralOperation_txtGO_Credit_Cust_AcCode_Name").val(GLcodeDiscreption);
}
function select_GO2_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Debit_Cust_AcCode_Name").val(GLcodeDiscreption);
}
function select_GO2_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbNormalGO_txtNGO_Credit_Cust_AcCode_Name").val(GLcodeDiscreption);
}

function TogggleDebitCreditCode(GO_No, DebitCredit_No) {
    //if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
    //    if (GO_No == '1') {
    //        if (DebitCredit_No == '1') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '2') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '3') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '4') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val('');
    //            }
    //        }
    //    }
    //}
    //if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_chk_GO2Flag").is(':checked')) {
    //    if (GO_No == '2') {
    //        if (DebitCredit_No == '1') {

    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val('');
    //            }

    //        }
    //        if (DebitCredit_No == '2') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '3') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '4') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val('');
    //            }
    //        }
    //    }
    //}
    //if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_chk_GO3Flag").is(':checked')) {
    //    if (GO_No == '3') {
    //        if (DebitCredit_No == '1') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Debit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '2') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Left_Credit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '3') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Debit_Curr").val('');
    //            }
    //        }
    //        if (DebitCredit_No == '4') {
    //            if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Code").val() != "") {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Curr").val('INR');
    //            }
    //            else {
    //                $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO3_txt_GO3_Right_Credit_Curr").val('');
    //            }
    //        }
    //    }
    //}
    if ($("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_chk_InterOffice").is(':checked')) {
        if (GO_No == '4') {
            //var _Document_Curr = $("#lblDoc_Curr").text();
            if (DebitCredit_No == '1') {
                if ($("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Code").val() == "C") {
                    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Division").val('21');
                }
                else {
                    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Debit_Division").val('31');
                }
            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Code").val() == "D") {
                    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Division").val('31');
                }
                else {
                    $("#TabContainerMain_tbGeneralOperation_TabSubContainerGO_tbDocumentGOAccChange_txt_GOAccChange_Credit_Division").val('21');
                }
            }
        }
    }
}
//----------------------------End-------------------
