function OnDocNextClick(index) {
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}

function SaveUpdateData() {
    // Heading Details
    var _hdnUserName = $("#hdnUserName").val();
    var _BranchName = $("#hdnBranchName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _txtIBDDocNo = $("#txtIBDDocNo").val();
    var _txtValueDate = $("#txtValueDate").val();
    var _hdnIBDInterest_MATU = $("#hdnIBDInterest_MATU").val();

    //---------------------Document Details---------------------------
    var _AccDocDetails_Flag = 'N', _txtCustName = '', _txtCommentCode = '',
 _txtMaturityDate = '', _txtsettlCodeForCust = '', _txtSettl_CodeForBank = '',
 _txtInterest_From = '', _txtDiscount = '', _txtInterest_To = '',
 _txt_No_Of_Days = '', _txt_INT_Rate = '', _txtInterestAmt = '',
 _txtOverinterestRate = '', _txtOverNoOfDays = '', _txtOverAmount = '',
 _txtAttn = '';
    if ($("#TabContainerMain_tbDocumentDetails_chk_AccDocDetails").is(':checked')) {
        _AccDocDetails_Flag = 'Y';
        _txtCustName = $("#TabContainerMain_tbDocumentDetails_txtCustName").val();

        _txtCommentCode = $("#TabContainerMain_tbDocumentDetails_txtCommentCode").val();
        _txtMaturityDate = $("#TabContainerMain_tbDocumentDetails_txtMaturityDate").val();

        _txtsettlCodeForCust = $("#TabContainerMain_tbDocumentDetails_txtsettlCodeForCust").val();
        _txtSettl_CodeForBank = $("#TabContainerMain_tbDocumentDetails_txtSettl_CodeForBank").val();
        _txtInterest_From = $("#TabContainerMain_tbDocumentDetails_txtInterest_From").val();
        _txtDiscount = $("#TabContainerMain_tbDocumentDetails_txtDiscount").val();
        _txtInterest_To = $("#TabContainerMain_tbDocumentDetails_txtInterest_To").val();

        _txt_No_Of_Days = $("#TabContainerMain_tbDocumentDetails_txt_No_Of_Days").val();
        _txt_INT_Rate = $("#TabContainerMain_tbDocumentDetails_txt_INT_Rate").val();

        _txtInterestAmt = $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val();
        _txtOverinterestRate = $("#TabContainerMain_tbDocumentDetails_txtOverinterestRate").val();
        _txtOverNoOfDays = $("#TabContainerMain_tbDocumentDetails_txtOverNoOfDays").val();

        _txtOverAmount = $("#TabContainerMain_tbDocumentDetails_txtOverAmount").val();
        _txtAttn = $("#TabContainerMain_tbDocumentDetails_txtAttn").val();
    }
    //--------------Import Accounting acc-----------

    var _AccImpAccounting_Flag = 'N',
_txt_DiscAmt = '', _txt_IMP_ACC_ExchRate = '',
 _txtPrinc_matu = '', _txtPrinc_lump = '', _txtprinc_Contract_no = '', _txt_Princ_Ex_Curr = '', _txtprinc_Ex_rate = '', _txtprinc_Intnl_Ex_rate = '',
 _txtInterest_matu = '', _txtInterest_lump = '', _txtInterest_Contract_no = '', _txt_interest_Ex_Curr = '', _txtInterest_Ex_rate = '', _txtInterest_Intnl_Ex_rate = '',
 _txtCommission_matu = '', _txtCommission_lump = '', _txtCommission_Contract_no = '', _txt_Commission_Ex_Curr = '', _txtCommission_Ex_rate = '', _txtCommission_Intnl_Ex_rate = '',
 _txtTheir_Commission_matu = '', _txtTheir_Commission_lump = '', _txtTheir_Commission_Contract_no = '', _txt_Their_Commission_Ex_Curr = '', _txtTheir_Commission_Ex_rate = '', _txtTheir_Commission_Intnl_Ex_rate = '',

 _txt_CR_Code = '', _txt_CR_AC_ShortName = '', _txt_CR_Cust_abbr = '', _txt_CR_Cust_Acc = '', _txt_CR_Acceptance_Curr = '', _txt_CR_Acceptance_amt = '', _txt_CR_Acceptance_payer = '',
 _txt_CR_Interest_Curr = '', _txt_CR_Interest_amt = '', _txt_CR_Interest_payer = '',
 _txt_CR_Accept_Commission_Curr = '', _txt_CR_Accept_Commission_amt = '', _txt_CR_Accept_Commission_Payer = '',
 _txt_CR_Pay_Handle_Commission_Curr = '', _txt_CR_Pay_Handle_Commission_amt = '', _txt_CR_Pay_Handle_Commission_Payer = '',
 _txt_CR_Others_Curr = '', _txt_CR_Others_amt = '', _txt_CR_Others_Payer = '',
 _txt_CR_Their_Commission_Curr = '', _txt_CR_Their_Commission_amt = '', _txt_CR_Their_Commission_Payer = '',

 _txt_DR_Code = '', _txt_DR_Cust_abbr = '', _txt_DR_Cust_Acc = '', _txt_DR_Cur_Acc_Curr = '', _txt_DR_Cur_Acc_amt = '', _txt_DR_Cur_Acc_payer = '',
 _txt_DR_Cur_Acc_Curr2 = '', _txt_DR_Cur_Acc_amt2 = '', _txt_DR_Cur_Acc_payer2 = '',
 _txt_DR_Cur_Acc_Curr3 = '', _txt_DR_Cur_Acc_amt3 = '', _txt_DR_Cur_Acc_payer3 = '';
    if ($("#TabContainerMain_tbDocumentAccounting_chk_AccImpAccounting").is(':checked')) {
        _AccImpAccounting_Flag = 'Y';
        _txt_DiscAmt = $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val();
        _txt_IMP_ACC_ExchRate = $("#TabContainerMain_tbDocumentAccounting_txt_IMP_ACC_ExchRate").val();
        _txtPrinc_matu = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_matu").val();
        _txtPrinc_lump = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_lump").val();
        _txtprinc_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Contract_no").val();
        _txt_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Princ_Ex_Curr").val();
        _txtprinc_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Ex_rate").val();
        _txtprinc_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Intnl_Ex_rate").val();

        _txtInterest_matu = $("#TabContainerMain_tbDocumentAccounting_txtInterest_matu").val();
        _txtInterest_lump = $("#TabContainerMain_tbDocumentAccounting_txtInterest_lump").val();
        _txtInterest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Contract_no").val();
        _txt_interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_interest_Ex_Curr").val();
        _txtInterest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Ex_rate").val();
        _txtInterest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Intnl_Ex_rate").val();

        _txtCommission_matu = $("#TabContainerMain_tbDocumentAccounting_txtCommission_matu").val();
        _txtCommission_lump = $("#TabContainerMain_tbDocumentAccounting_txtCommission_lump").val();
        _txtCommission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Contract_no").val();
        _txt_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Commission_Ex_Curr").val();
        _txtCommission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Ex_rate").val();
        _txtCommission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Intnl_Ex_rate").val();

        _txtTheir_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_matu").val();
        _txtTheir_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_lump").val();
        _txtTheir_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Contract_no").val();
        _txt_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Their_Commission_Ex_Curr").val();
        _txtTheir_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Ex_rate").val();
        _txtTheir_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Intnl_Ex_rate").val();

        _txt_CR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val();
        _txt_CR_AC_ShortName = $("#TabContainerMain_tbDocumentAccounting_txt_CR_AC_ShortName").val();
        _txt_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val();
        _txt_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val();
        _txt_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_Curr").val();
        _txt_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_amt").val();
        _txt_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_payer").val();

        _txt_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_Curr").val();
        _txt_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_amt").val();
        _txt_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_payer").val();

        _txt_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Curr").val();
        _txt_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_amt").val();
        _txt_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Payer").val();

        _txt_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Curr").val();
        _txt_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_amt").val();
        _txt_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Payer").val();

        _txt_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Curr").val();
        _txt_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_amt").val();
        _txt_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Payer").val();

        _txt_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Curr").val();
        _txt_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_amt").val();
        _txt_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Payer").val();

        _txt_DR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val();
        _txt_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val();
        _txt_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val();
        _txt_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr").val();
        _txt_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val();
        _txt_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer").val();

        _txt_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr2").val();
        _txt_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt2").val();
        _txt_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer2").val();

        _txt_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr3").val();
        _txt_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt3").val();
        _txt_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer3").val();
    }

    //--------------------Genral Operation I----------------------------------------
    var _chk_GO1_Flag = '',
    _txt_GO1_Comment = '', _txt_GO1_SectionNo = '', _txt_GO1_Remarks = '', _txt_GO1_Memo = '',
    _txt_GO1_Scheme_no = '', _txt_GO1_Debit_Code = '', _txt_GO1_Debit_Curr = '', _txt_GO1_Debit_Amt = 0,
     _txt_GO1_Debit_Cust = '', _txt_GO1_Debit_Cust_Name = '',
    _txt_GO1_Debit_Cust_AcCode = '', _txt_GO1_Debit_Cust_AcCode_Name = '', _txt_GO1_Debit_Cust_AccNo = '',
    _txt_GO1_Debit_Exch_Rate = '', _txt_GO1_Debit_Exch_CCY = '',
     _txt_GO1_Debit_FUND = '', _txt_GO1_Debit_Check_No = '', _txt_GO1_Debit_Available = '',
     _txt_GO1_Debit_AdPrint = '', _txt_GO1_Debit_Details = '', _txt_GO1_Debit_Entity = '',
     _txt_GO1_Debit_Division = '', _txt_GO1_Debit_Inter_Amount = '', _txt_GO1_Debit_Inter_Rate = '',
     _txt_GO1_Credit_Code = '', _txt_GO1_Credit_Curr = '', _txt_GO1_Credit_Amt = '',
     _txt_GO1_Credit_Cust = '', _txt_GO1_Credit_Cust_Name = '',
     _txt_GO1_Credit_Cust_AcCode = '', _txt_GO1_Credit_Cust_AcCode_Name = '', _txt_GO1_Credit_Cust_AccNo = '',
     _txt_GO1_Credit_Exch_Rate = '', _txt_GO1_Credit_Exch_Curr = '',
     _txt_GO1_Credit_FUND = '', _txt_GO1_Credit_Check_No = '', _txt_GO1_Credit_Available = '',
     _txt_GO1_Credit_AdPrint = '', _txt_GO1_Credit_Details = '', _txt_GO1_Credit_Entity = '',
     _txt_GO1_Credit_Division = '', _txt_GO1_Credit_Inter_Amount = '', _txt_GO1_Credit_Inter_Rate = '';


    if ($("#TabContainerMain_tbDocumentGO1_chk_GO1_Flag").is(':checked')) {
        _chk_GO1_Flag = 'Y';
        _txt_GO1_Comment = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Comment").val();
        _txt_GO1_SectionNo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_SectionNo").val();
        _txt_GO1_Remarks = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Remarks").val();
        _txt_GO1_Memo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Memo").val();
        _txt_GO1_Scheme_no = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Scheme_no").val();
        _txt_GO1_Debit_Code = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Code").val();
        _txt_GO1_Debit_Curr = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Curr").val();
        _txt_GO1_Debit_Amt = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Amt").val();
        _txt_GO1_Debit_Cust = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust").val();
        _txt_GO1_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_Name").val();
        _txt_GO1_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode").val();
        _txt_GO1_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode_Name").val();
        _txt_GO1_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AccNo").val();
        _txt_GO1_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Exch_Rate").val();
        _txt_GO1_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Exch_CCY").val();
        _txt_GO1_Debit_FUND = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_FUND").val();
        _txt_GO1_Debit_Check_No = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Check_No").val();
        _txt_GO1_Debit_Available = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Available").val();
        _txt_GO1_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_AdPrint").val();
        _txt_GO1_Debit_Details = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Details").val();
        _txt_GO1_Debit_Entity = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Entity").val();
        _txt_GO1_Debit_Division = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Division").val();
        _txt_GO1_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Inter_Amount").val();
        _txt_GO1_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Inter_Rate").val();
        _txt_GO1_Credit_Code = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Code").val();
        _txt_GO1_Credit_Curr = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Curr").val();
        _txt_GO1_Credit_Amt = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Amt").val();
        _txt_GO1_Credit_Cust = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust").val();
        _txt_GO1_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_Name").val();
        _txt_GO1_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode").val();
        _txt_GO1_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode_Name").val();
        _txt_GO1_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AccNo").val();
        _txt_GO1_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Exch_Rate").val();
        _txt_GO1_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Exch_Curr").val();
        _txt_GO1_Credit_FUND = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_FUND").val();
        _txt_GO1_Credit_Check_No = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Check_No").val();
        _txt_GO1_Credit_Available = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Available").val();
        _txt_GO1_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_AdPrint").val();
        _txt_GO1_Credit_Details = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Details").val();
        _txt_GO1_Credit_Entity = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Entity").val();
        _txt_GO1_Credit_Division = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Division").val();
        _txt_GO1_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Inter_Amount").val();
        _txt_GO1_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Inter_Rate").val();
    }

    //--------------------Genral Operation II----------------------------------------
    var _chk_GO2_Flag = '',
    _txt_GO2_Comment = '', _txt_GO2_SectionNo = '', _txt_GO2_Remarks = '', _txt_GO2_Memo = '',
    _txt_GO2_Scheme_no = '', _txt_GO2_Debit_Code = '', _txt_GO2_Debit_Curr = '', _txt_GO2_Debit_Amt = 0,
     _txt_GO2_Debit_Cust = '', _txt_GO2_Debit_Cust_Name = '',
    _txt_GO2_Debit_Cust_AcCode = '', _txt_GO2_Debit_Cust_AcCode_Name = '', _txt_GO2_Debit_Cust_AccNo = '',
    _txt_GO2_Debit_Exch_Rate = '', _txt_GO2_Debit_Exch_CCY = '',
     _txt_GO2_Debit_FUND = '', _txt_GO2_Debit_Check_No = '', _txt_GO2_Debit_Available = '',
     _txt_GO2_Debit_AdPrint = '', _txt_GO2_Debit_Details = '', _txt_GO2_Debit_Entity = '',
     _txt_GO2_Debit_Division = '', _txt_GO2_Debit_Inter_Amount = '', _txt_GO2_Debit_Inter_Rate = '',
     _txt_GO2_Credit_Code = '', _txt_GO2_Credit_Curr = '', _txt_GO2_Credit_Amt = '',
     _txt_GO2_Credit_Cust = '', _txt_GO2_Credit_Cust_Name = '',
     _txt_GO2_Credit_Cust_AcCode = '', _txt_GO2_Credit_Cust_AcCode_Name = '', _txt_GO2_Credit_Cust_AccNo = '',
     _txt_GO2_Credit_Exch_Rate = '', _txt_GO2_Credit_Exch_Curr = '',
     _txt_GO2_Credit_FUND = '', _txt_GO2_Credit_Check_No = '', _txt_GO2_Credit_Available = '',
     _txt_GO2_Credit_AdPrint = '', _txt_GO2_Credit_Details = '', _txt_GO2_Credit_Entity = '',
     _txt_GO2_Credit_Division = '', _txt_GO2_Credit_Inter_Amount = '', _txt_GO2_Credit_Inter_Rate = '';


    if ($("#TabContainerMain_tbDocumentGO2_chk_GO2_Flag").is(':checked')) {
        _chk_GO2_Flag = 'Y';
        _txt_GO2_Comment = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Comment").val();
        _txt_GO2_SectionNo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_SectionNo").val();
        _txt_GO2_Remarks = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Remarks").val();
        _txt_GO2_Memo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Memo").val();
        _txt_GO2_Scheme_no = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Scheme_no").val();
        _txt_GO2_Debit_Code = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Code").val();
        _txt_GO2_Debit_Curr = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Curr").val();
        _txt_GO2_Debit_Amt = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Amt").val();
        _txt_GO2_Debit_Cust = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust").val();
        _txt_GO2_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_Name").val();
        _txt_GO2_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AcCode").val();
        _txt_GO2_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AcCode_Name").val();
        _txt_GO2_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AccNo").val();
        _txt_GO2_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Exch_Rate").val();
        _txt_GO2_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Exch_CCY").val();
        _txt_GO2_Debit_FUND = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_FUND").val();
        _txt_GO2_Debit_Check_No = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Check_No").val();
        _txt_GO2_Debit_Available = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Available").val();
        _txt_GO2_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_AdPrint").val();
        _txt_GO2_Debit_Details = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Details").val();
        _txt_GO2_Debit_Entity = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Entity").val();
        _txt_GO2_Debit_Division = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Division").val();
        _txt_GO2_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Inter_Amount").val();
        _txt_GO2_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Inter_Rate").val();
        _txt_GO2_Credit_Code = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Code").val();
        _txt_GO2_Credit_Curr = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Curr").val();
        _txt_GO2_Credit_Amt = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Amt").val();
        _txt_GO2_Credit_Cust = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust").val();
        _txt_GO2_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_Name").val();
        _txt_GO2_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AcCode").val();
        _txt_GO2_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AcCode_Name").val();
        _txt_GO2_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AccNo").val();
        _txt_GO2_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Exch_Rate").val();
        _txt_GO2_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Exch_Curr").val();
        _txt_GO2_Credit_FUND = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_FUND").val();
        _txt_GO2_Credit_Check_No = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Check_No").val();
        _txt_GO2_Credit_Available = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Available").val();
        _txt_GO2_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_AdPrint").val();
        _txt_GO2_Credit_Details = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Details").val();
        _txt_GO2_Credit_Entity = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Entity").val();
        _txt_GO2_Credit_Division = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Division").val();
        _txt_GO2_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Inter_Amount").val();
        _txt_GO2_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Inter_Rate").val();
    }

    ///////----------IBD Doc Details-------------
    var _txt_IBD_CustName = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_CustName").val();
    var _txt_IBD_CommentCode = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_CommentCode").val();
    var _txt_IBD_MaturityDate = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_MaturityDate").val();

    var _txt_IBD_settlCodeForCust = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_settlCodeForCust").val();
    var _txt_IBD_Settl_CodeForBank = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_Settl_CodeForBank").val();
    var _txt_IBD_Interest_From = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_Interest_From").val();
    var _txt_IBD_Discount = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_Discount").val();
    var _txt_IBD_Interest_To = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_Interest_To").val();

    var _txt_IBD__No_Of_Days = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD__No_Of_Days").val();
    var _txt_IBD__INT_Rate = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD__INT_Rate").val();

    var _txt_IBD_InterestAmt = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_InterestAmt").val();
    var _txt_IBD_OverinterestRate = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_OverinterestRate").val();
    var _txt_IBD_OverNoOfDays = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_OverNoOfDays").val();

    var _txt_IBD_OverAmount = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_OverAmount").val();
    var _txt_IBD_Attn = $("#TabContainerMain_tbIBDDocumentDetails_txt_IBD_Attn").val();

    //--------------IBD Import Accounting acc-----------
    var _txt_IBD_DiscAmt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DiscAmt").val();
    var _txt_IBD_IMP_ACC_ExchRate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IMP_ACC_ExchRate").val();
    var _txt_IBDPrinc_matu = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDPrinc_matu").val();
    var _txt_IBDPrinc_lump = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDPrinc_lump").val();
    var _txt_IBDprinc_Contract_no = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDprinc_Contract_no").val();
    var _txt_IBD_Princ_Ex_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_Princ_Ex_Curr").val();
    var _txt_IBDprinc_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDprinc_Ex_rate").val();
    var _txt_IBDprinc_Intnl_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDprinc_Intnl_Ex_rate").val();

    var _txt_IBDInterest_matu = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDInterest_matu").val();
    var _txt_IBDInterest_lump = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDInterest_lump").val();
    var _txt_IBDInterest_Contract_no = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDInterest_Contract_no").val();
    var _txt_IBD_interest_Ex_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_interest_Ex_Curr").val();
    var _txt_IBDInterest_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDInterest_Ex_rate").val();
    var _txt_IBDInterest_Intnl_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDInterest_Intnl_Ex_rate").val();

    var _txt_IBDCommission_matu = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDCommission_matu").val();
    var _txt_IBDCommission_lump = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDCommission_lump").val();
    var _txt_IBDCommission_Contract_no = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDCommission_Contract_no").val();
    var _txt_IBD_Commission_Ex_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_Commission_Ex_Curr").val();
    var _txt_IBDCommission_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDCommission_Ex_rate").val();
    var _txt_IBDCommission_Intnl_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDCommission_Intnl_Ex_rate").val();

    var _txt_IBDTheir_Commission_matu = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDTheir_Commission_matu").val();
    var _txt_IBDTheir_Commission_lump = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDTheir_Commission_lump").val();
    var _txt_IBDTheir_Commission_Contract_no = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDTheir_Commission_Contract_no").val();
    var _txt_IBD_Their_Commission_Ex_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_Their_Commission_Ex_Curr").val();
    var _txt_IBDTheir_Commission_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDTheir_Commission_Ex_rate").val();
    var _txt_IBDTheir_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBDTheir_Commission_Intnl_Ex_rate").val();

    var _txt_IBD_CR_Code = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Code").val();
    var _txt_IBD_CR_AC_ShortName = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_AC_ShortName").val();
    var _txt_IBD_CR_Cust_abbr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Cust_abbr").val();
    var _txt_IBD_CR_Cust_Acc = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Cust_Acc").val();
    var _txt_IBD_CR_Acceptance_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Acceptance_Curr").val();
    var _txt_IBD_CR_Acceptance_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Acceptance_amt").val();
    var _txt_IBD_CR_Acceptance_payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Acceptance_payer").val();

    var _txt_IBD_CR_Interest_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Interest_Curr").val();
    var _txt_IBD_CR_Interest_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Interest_amt").val();
    var _txt_IBD_CR_Interest_payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Interest_payer").val();

    var _txt_IBD_CR_Accept_Commission_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Accept_Commission_Curr").val();
    var _txt_IBD_CR_Accept_Commission_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Accept_Commission_amt").val();
    var _txt_IBD_CR_Accept_Commission_Payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Accept_Commission_Payer").val();

    var _txt_IBD_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Pay_Handle_Commission_Curr").val();
    var _txt_IBD_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Pay_Handle_Commission_amt").val();
    var _txt_IBD_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Pay_Handle_Commission_Payer").val();

    var _txt_IBD_CR_Others_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Others_Curr").val();
    var _txt_IBD_CR_Others_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Others_amt").val();
    var _txt_IBD_CR_Others_Payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Others_Payer").val();

    var _txt_IBD_CR_Their_Commission_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Their_Commission_Curr").val();
    var _txt_IBD_CR_Their_Commission_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Their_Commission_amt").val();
    var _txt_IBD_CR_Their_Commission_Payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Their_Commission_Payer").val();

    var _txt_IBD_IBD_DR_Code = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Code").val();
    var _txt_IBD_IBD_DR_Cust_abbr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Cust_abbr").val();
    var _txt_IBD_IBD_DR_Cust_Acc = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Cust_Acc").val();
    var _txt_IBD_IBD_DR_Cur_Acc_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Cur_Acc_Curr").val();
    var _txt_IBD_IBD_DR_Cur_Acc_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Cur_Acc_amt").val();
    var _txt_IBD_IBD_DR_Cur_Acc_payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Cur_Acc_payer").val();
    var _txt_IBD_DR_Code = '', _txt_IBD_DR_Cust_abbr = '', _txt_IBD_DR_Cust_Acc = '', _txt_IBD_DR_Cur_Acc_Curr = '', _txt_IBD_DR_Cur_Acc_amt = '', _txt_IBD_DR_Cur_Acc_payer = '';
    if (_hdnIBDInterest_MATU == '2') {
        _txt_IBD_DR_Code = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Code").val();
        _txt_IBD_DR_Cust_abbr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cust_abbr").val();
        _txt_IBD_DR_Cust_Acc = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cust_Acc").val();
        _txt_IBD_DR_Cur_Acc_Curr = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_Curr").val();
        _txt_IBD_DR_Cur_Acc_amt = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_amt").val();
        _txt_IBD_DR_Cur_Acc_payer = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_payer").val();
    }
    var _txt_IBD_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_Curr2").val();
    var _txt_IBD_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_amt2").val();
    var _txt_IBD_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_payer2").val();

    var _txt_IBD_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_Curr3").val();
    var _txt_IBD_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_amt3").val();
    var _txt_IBD_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_DR_Cur_Acc_payer3").val();

    var _IBDExtn_Flag = 'N';
    if ($("#TabContainerMain_tbIBDDocumentAccounting_chk_IBDExtn_Flag").is(':checked')) {
        _IBDExtn_Flag = 'Y';
    }

    $.ajax({
        type: "POST",
        url: "TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Maker.aspx/AddUpdateONLCDiscount",
        data: '{_hdnUserName:"' + _hdnUserName + '", _BranchName:"' + _BranchName + '", _txtDocNo:"' + _txtDocNo + '", _txtIBDDocNo:"' + _txtIBDDocNo + '", _txtValueDate:"' + _txtValueDate +
'", _AccDocDetails_Flag:"' + _AccDocDetails_Flag +
'", _txtCustName:"' + _txtCustName + '", _txtCommentCode:"' + _txtCommentCode +
'", _txtMaturityDate:"' + _txtMaturityDate +
'", _txtsettlCodeForCust:"' + _txtsettlCodeForCust +
'", _txtSettl_CodeForBank:"' + _txtSettl_CodeForBank +
'", _txtInterest_From:"' + _txtInterest_From + '", _txtDiscount:"' + _txtDiscount + '", _txtInterest_To:"' + _txtInterest_To +
'", _txt_No_Of_Days:"' + _txt_No_Of_Days + '", _txt_INT_Rate:"' + _txt_INT_Rate + '", _txtInterestAmt:"' + _txtInterestAmt +
'", _txtOverinterestRate:"' + _txtOverinterestRate + '", _txtOverNoOfDays:"' + _txtOverNoOfDays + '", _txtOverAmount:"' + _txtOverAmount +
'", _txtAttn:"' + _txtAttn +

        ////----------Import Accounting Acc-----
'", _AccImpAccounting_Flag:"' + _AccImpAccounting_Flag +
'", _txt_DiscAmt:"' + _txt_DiscAmt + '", _txt_IMP_ACC_ExchRate:"' + _txt_IMP_ACC_ExchRate +
'", _txtPrinc_matu:"' + _txtPrinc_matu + '", _txtPrinc_lump:"' + _txtPrinc_lump + '", _txtprinc_Contract_no:"' + _txtprinc_Contract_no + '", _txt_Princ_Ex_Curr:"' + _txt_Princ_Ex_Curr + '", _txtprinc_Ex_rate:"' + _txtprinc_Ex_rate + '", _txtprinc_Intnl_Ex_rate:"' + _txtprinc_Intnl_Ex_rate +
'", _txtInterest_matu:"' + _txtInterest_matu + '", _txtInterest_lump:"' + _txtInterest_lump + '", _txtInterest_Contract_no:"' + _txtInterest_Contract_no + '", _txt_interest_Ex_Curr:"' + _txt_interest_Ex_Curr + '", _txtInterest_Ex_rate:"' + _txtInterest_Ex_rate + '", _txtInterest_Intnl_Ex_rate:"' + _txtInterest_Intnl_Ex_rate +
'", _txtCommission_matu:"' + _txtCommission_matu + '", _txtCommission_lump:"' + _txtCommission_lump + '", _txtCommission_Contract_no:"' + _txtCommission_Contract_no + '", _txt_Commission_Ex_Curr:"' + _txt_Commission_Ex_Curr + '", _txtCommission_Ex_rate:"' + _txtCommission_Ex_rate + '", _txtCommission_Intnl_Ex_rate:"' + _txtCommission_Intnl_Ex_rate +
'", _txtTheir_Commission_matu:"' + _txtTheir_Commission_matu + '", _txtTheir_Commission_lump:"' + _txtTheir_Commission_lump + '", _txtTheir_Commission_Contract_no:"' + _txtTheir_Commission_Contract_no + '", _txt_Their_Commission_Ex_Curr:"' + _txt_Their_Commission_Ex_Curr + '", _txtTheir_Commission_Ex_rate:"' + _txtTheir_Commission_Ex_rate + '", _txtTheir_Commission_Intnl_Ex_rate:"' + _txtTheir_Commission_Intnl_Ex_rate +

'", _txt_CR_Code:"' + _txt_CR_Code + '", _txt_CR_AC_ShortName:"' + _txt_CR_AC_ShortName + '", _txt_CR_Cust_abbr:"' + _txt_CR_Cust_abbr + '", _txt_CR_Cust_Acc:"' + _txt_CR_Cust_Acc + '", _txt_CR_Acceptance_Curr:"' + _txt_CR_Acceptance_Curr + '", _txt_CR_Acceptance_amt:"' + _txt_CR_Acceptance_amt + '", _txt_CR_Acceptance_payer:"' + _txt_CR_Acceptance_payer +
'", _txt_CR_Interest_Curr:"' + _txt_CR_Interest_Curr + '", _txt_CR_Interest_amt:"' + _txt_CR_Interest_amt + '", _txt_CR_Interest_payer:"' + _txt_CR_Interest_payer +
'", _txt_CR_Accept_Commission_Curr:"' + _txt_CR_Accept_Commission_Curr + '", _txt_CR_Accept_Commission_amt:"' + _txt_CR_Accept_Commission_amt + '", _txt_CR_Accept_Commission_Payer:"' + _txt_CR_Accept_Commission_Payer +
'", _txt_CR_Pay_Handle_Commission_Curr:"' + _txt_CR_Pay_Handle_Commission_Curr + '", _txt_CR_Pay_Handle_Commission_amt:"' + _txt_CR_Pay_Handle_Commission_amt + '", _txt_CR_Pay_Handle_Commission_Payer:"' + _txt_CR_Pay_Handle_Commission_Payer +
'", _txt_CR_Others_Curr:"' + _txt_CR_Others_Curr + '", _txt_CR_Others_amt:"' + _txt_CR_Others_amt + '", _txt_CR_Others_Payer:"' + _txt_CR_Others_Payer +
'", _txt_CR_Their_Commission_Curr:"' + _txt_CR_Their_Commission_Curr + '", _txt_CR_Their_Commission_amt:"' + _txt_CR_Their_Commission_amt + '", _txt_CR_Their_Commission_Payer:"' + _txt_CR_Their_Commission_Payer +

'", _txt_DR_Code:"' + _txt_DR_Code + '", _txt_DR_Cust_abbr:"' + _txt_DR_Cust_abbr + '", _txt_DR_Cust_Acc:"' + _txt_DR_Cust_Acc + '", _txt_DR_Cur_Acc_Curr:"' + _txt_DR_Cur_Acc_Curr + '", _txt_DR_Cur_Acc_amt:"' + _txt_DR_Cur_Acc_amt + '", _txt_DR_Cur_Acc_payer:"' + _txt_DR_Cur_Acc_payer +
'", _txt_DR_Cur_Acc_Curr2:"' + _txt_DR_Cur_Acc_Curr2 + '", _txt_DR_Cur_Acc_amt2:"' + _txt_DR_Cur_Acc_amt2 + '", _txt_DR_Cur_Acc_payer2:"' + _txt_DR_Cur_Acc_payer2 +
'", _txt_DR_Cur_Acc_Curr3:"' + _txt_DR_Cur_Acc_Curr3 + '", _txt_DR_Cur_Acc_amt3:"' + _txt_DR_Cur_Acc_amt3 + '", _txt_DR_Cur_Acc_payer3:"' + _txt_DR_Cur_Acc_payer3 +

        ///////////////// GENERAL OPRATOIN 1 ////////////////////////////
        '", _chk_GO1_Flag: "' + _chk_GO1_Flag +
        '", _txt_GO1_Comment: "' + _txt_GO1_Comment + '", _txt_GO1_SectionNo: "' + _txt_GO1_SectionNo +
        '", _txt_GO1_Remarks: "' + _txt_GO1_Remarks + '", _txt_GO1_Memo: "' + _txt_GO1_Memo + '", _txt_GO1_Scheme_no: "' + _txt_GO1_Scheme_no + '", _txt_GO1_Debit_Code: "' + _txt_GO1_Debit_Code + '", _txt_GO1_Debit_Curr: "' + _txt_GO1_Debit_Curr +
        '", _txt_GO1_Debit_Amt: "' + _txt_GO1_Debit_Amt + '", _txt_GO1_Debit_Cust: "' + _txt_GO1_Debit_Cust + '", _txt_GO1_Debit_Cust_Name: "' + _txt_GO1_Debit_Cust_Name + '", _txt_GO1_Debit_Cust_AcCode: "' + _txt_GO1_Debit_Cust_AcCode + '", _txt_GO1_Debit_Cust_AccNo: "' + _txt_GO1_Debit_Cust_AccNo +
        '", _txt_GO1_Debit_Cust_AcCode_Name: "' + _txt_GO1_Debit_Cust_AcCode_Name + '", _txt_GO1_Debit_Exch_Rate: "' + _txt_GO1_Debit_Exch_Rate + '", _txt_GO1_Debit_Exch_CCY: "' + _txt_GO1_Debit_Exch_CCY + '", _txt_GO1_Debit_FUND: "' + _txt_GO1_Debit_FUND + '", _txt_GO1_Debit_Check_No: "' + _txt_GO1_Debit_Check_No +
        '",_txt_GO1_Debit_Available:"' + _txt_GO1_Debit_Available + '",_txt_GO1_Debit_AdPrint:"' + _txt_GO1_Debit_AdPrint +
        '", _txt_GO1_Debit_Details: "' + _txt_GO1_Debit_Details + '", _txt_GO1_Debit_Entity: "' + _txt_GO1_Debit_Entity + '", _txt_GO1_Debit_Division: "' + _txt_GO1_Debit_Division + '", _txt_GO1_Debit_Inter_Amount: "' + _txt_GO1_Debit_Inter_Amount + '", _txt_GO1_Debit_Inter_Rate: "' + _txt_GO1_Debit_Inter_Rate +
        '", _txt_GO1_Credit_Code: "' + _txt_GO1_Credit_Code + '", _txt_GO1_Credit_Curr: "' + _txt_GO1_Credit_Curr + '", _txt_GO1_Credit_Amt: "' + _txt_GO1_Credit_Amt + '", _txt_GO1_Credit_Cust: "' + _txt_GO1_Credit_Cust + '", _txt_GO1_Credit_Cust_Name: "' + _txt_GO1_Credit_Cust_Name +
        '", _txt_GO1_Credit_Cust_AcCode: "' + _txt_GO1_Credit_Cust_AcCode + '", _txt_GO1_Credit_Cust_AcCode_Name: "' + _txt_GO1_Credit_Cust_AcCode_Name + '", _txt_GO1_Credit_Cust_AccNo: "' + _txt_GO1_Credit_Cust_AccNo + '", _txt_GO1_Credit_Exch_Rate: "' + _txt_GO1_Credit_Exch_Rate + '", _txt_GO1_Credit_Exch_Curr: "' + _txt_GO1_Credit_Exch_Curr +
        '", _txt_GO1_Credit_FUND: "' + _txt_GO1_Credit_FUND + '", _txt_GO1_Credit_Check_No: "' + _txt_GO1_Credit_Check_No + '", _txt_GO1_Credit_Available: "' + _txt_GO1_Credit_Available + '", _txt_GO1_Credit_AdPrint: "' + _txt_GO1_Credit_AdPrint + '", _txt_GO1_Credit_Details: "' + _txt_GO1_Credit_Details +
        '", _txt_GO1_Credit_Entity: "' + _txt_GO1_Credit_Entity + '", _txt_GO1_Credit_Division: "' + _txt_GO1_Credit_Division + '", _txt_GO1_Credit_Inter_Amount: "' + _txt_GO1_Credit_Inter_Amount + '", _txt_GO1_Credit_Inter_Rate: "' + _txt_GO1_Credit_Inter_Rate +
        ///////////////// GENERAL OPRATOIN 2 ////////////////////////////
        '", _chk_GO2_Flag: "' + _chk_GO2_Flag +
        '", _txt_GO2_Comment: "' + _txt_GO2_Comment + '", _txt_GO2_SectionNo: "' + _txt_GO2_SectionNo +
        '", _txt_GO2_Remarks: "' + _txt_GO2_Remarks + '", _txt_GO2_Memo: "' + _txt_GO2_Memo + '", _txt_GO2_Scheme_no: "' + _txt_GO2_Scheme_no + '", _txt_GO2_Debit_Code: "' + _txt_GO2_Debit_Code + '", _txt_GO2_Debit_Curr: "' + _txt_GO2_Debit_Curr +
        '", _txt_GO2_Debit_Amt: "' + _txt_GO2_Debit_Amt + '", _txt_GO2_Debit_Cust: "' + _txt_GO2_Debit_Cust + '", _txt_GO2_Debit_Cust_Name: "' + _txt_GO2_Debit_Cust_Name + '", _txt_GO2_Debit_Cust_AcCode: "' + _txt_GO2_Debit_Cust_AcCode + '", _txt_GO2_Debit_Cust_AccNo: "' + _txt_GO2_Debit_Cust_AccNo +
        '", _txt_GO2_Debit_Cust_AcCode_Name: "' + _txt_GO2_Debit_Cust_AcCode_Name + '", _txt_GO2_Debit_Exch_Rate: "' + _txt_GO2_Debit_Exch_Rate + '", _txt_GO2_Debit_Exch_CCY: "' + _txt_GO2_Debit_Exch_CCY + '", _txt_GO2_Debit_FUND: "' + _txt_GO2_Debit_FUND + '", _txt_GO2_Debit_Check_No: "' + _txt_GO2_Debit_Check_No +
        '",_txt_GO2_Debit_Available:"' + _txt_GO2_Debit_Available + '",_txt_GO2_Debit_AdPrint:"' + _txt_GO2_Debit_AdPrint +
        '", _txt_GO2_Debit_Details: "' + _txt_GO2_Debit_Details + '", _txt_GO2_Debit_Entity: "' + _txt_GO2_Debit_Entity + '", _txt_GO2_Debit_Division: "' + _txt_GO2_Debit_Division + '", _txt_GO2_Debit_Inter_Amount: "' + _txt_GO2_Debit_Inter_Amount + '", _txt_GO2_Debit_Inter_Rate: "' + _txt_GO2_Debit_Inter_Rate +
        '", _txt_GO2_Credit_Code: "' + _txt_GO2_Credit_Code + '", _txt_GO2_Credit_Curr: "' + _txt_GO2_Credit_Curr + '", _txt_GO2_Credit_Amt: "' + _txt_GO2_Credit_Amt + '", _txt_GO2_Credit_Cust: "' + _txt_GO2_Credit_Cust + '", _txt_GO2_Credit_Cust_Name: "' + _txt_GO2_Credit_Cust_Name +
        '", _txt_GO2_Credit_Cust_AcCode: "' + _txt_GO2_Credit_Cust_AcCode + '", _txt_GO2_Credit_Cust_AcCode_Name: "' + _txt_GO2_Credit_Cust_AcCode_Name + '", _txt_GO2_Credit_Cust_AccNo: "' + _txt_GO2_Credit_Cust_AccNo + '", _txt_GO2_Credit_Exch_Rate: "' + _txt_GO2_Credit_Exch_Rate + '", _txt_GO2_Credit_Exch_Curr: "' + _txt_GO2_Credit_Exch_Curr +
        '", _txt_GO2_Credit_FUND: "' + _txt_GO2_Credit_FUND + '", _txt_GO2_Credit_Check_No: "' + _txt_GO2_Credit_Check_No + '", _txt_GO2_Credit_Available: "' + _txt_GO2_Credit_Available + '", _txt_GO2_Credit_AdPrint: "' + _txt_GO2_Credit_AdPrint + '", _txt_GO2_Credit_Details: "' + _txt_GO2_Credit_Details +
        '", _txt_GO2_Credit_Entity: "' + _txt_GO2_Credit_Entity + '", _txt_GO2_Credit_Division: "' + _txt_GO2_Credit_Division + '", _txt_GO2_Credit_Inter_Amount: "' + _txt_GO2_Credit_Inter_Amount + '", _txt_GO2_Credit_Inter_Rate: "' + _txt_GO2_Credit_Inter_Rate +
        /////////-----IBD Doc  Details---------------
        '", _txt_IBD_CustName:"' + _txt_IBD_CustName + '", _txt_IBD_CommentCode:"' + _txt_IBD_CommentCode +
        '", _txt_IBD_MaturityDate:"' + _txt_IBD_MaturityDate +
        '", _txt_IBD_settlCodeForCust:"' + _txt_IBD_settlCodeForCust +
        '", _txt_IBD_Settl_CodeForBank:"' + _txt_IBD_Settl_CodeForBank +
        '", _txt_IBD_Interest_From:"' + _txt_IBD_Interest_From + '", _txt_IBD_Discount:"' + _txt_IBD_Discount + '", _txt_IBD_Interest_To:"' + _txt_IBD_Interest_To +
        '", _txt_IBD__No_Of_Days:"' + _txt_IBD__No_Of_Days + '", _txt_IBD__INT_Rate:"' + _txt_IBD__INT_Rate + '", _txt_IBD_InterestAmt:"' + _txt_IBD_InterestAmt +
        '", _txt_IBD_OverinterestRate:"' + _txt_IBD_OverinterestRate + '", _txt_IBD_OverNoOfDays:"' + _txt_IBD_OverNoOfDays + '", _txt_IBD_OverAmount:"' + _txt_IBD_OverAmount +
        '", _txt_IBD_Attn:"' + _txt_IBD_Attn +

        //----------Import IBD Accounting Acc-----

        '", _txt_IBD_DiscAmt:"' + _txt_IBD_DiscAmt + '", _txt_IBD_IMP_ACC_ExchRate:"' + _txt_IBD_IMP_ACC_ExchRate +
        '", _txt_IBDPrinc_matu:"' + _txt_IBDPrinc_matu + '", _txt_IBDPrinc_lump:"' + _txt_IBDPrinc_lump + '", _txt_IBDprinc_Contract_no:"' + _txt_IBDprinc_Contract_no + '", _txt_IBD_Princ_Ex_Curr:"' + _txt_IBD_Princ_Ex_Curr + '", _txt_IBDprinc_Ex_rate:"' + _txt_IBDprinc_Ex_rate + '", _txt_IBDprinc_Intnl_Ex_rate:"' + _txt_IBDprinc_Intnl_Ex_rate +
        '", _txt_IBDInterest_matu:"' + _txt_IBDInterest_matu + '", _txt_IBDInterest_lump:"' + _txt_IBDInterest_lump + '", _txt_IBDInterest_Contract_no:"' + _txt_IBDInterest_Contract_no + '", _txt_IBD_interest_Ex_Curr:"' + _txt_IBD_interest_Ex_Curr + '", _txt_IBDInterest_Ex_rate:"' + _txt_IBDInterest_Ex_rate + '", _txt_IBDInterest_Intnl_Ex_rate:"' + _txt_IBDInterest_Intnl_Ex_rate +
        '", _txt_IBDCommission_matu:"' + _txt_IBDCommission_matu + '", _txt_IBDCommission_lump:"' + _txt_IBDCommission_lump + '", _txt_IBDCommission_Contract_no:"' + _txt_IBDCommission_Contract_no + '", _txt_IBD_Commission_Ex_Curr:"' + _txt_IBD_Commission_Ex_Curr + '", _txt_IBDCommission_Ex_rate:"' + _txt_IBDCommission_Ex_rate + '", _txt_IBDCommission_Intnl_Ex_rate:"' + _txt_IBDCommission_Intnl_Ex_rate +
        '", _txt_IBDTheir_Commission_matu:"' + _txt_IBDTheir_Commission_matu + '", _txt_IBDTheir_Commission_lump:"' + _txt_IBDTheir_Commission_lump + '", _txt_IBDTheir_Commission_Contract_no:"' + _txt_IBDTheir_Commission_Contract_no + '", _txt_IBD_Their_Commission_Ex_Curr:"' + _txt_IBD_Their_Commission_Ex_Curr + '",_txt_IBDTheir_Commission_Ex_rate:"' + _txt_IBDTheir_Commission_Ex_rate + '", _txt_IBDTheir_Commission_Intnl_Ex_rate:"' + _txt_IBDTheir_Commission_Intnl_Ex_rate +

        '", _txt_IBD_CR_Code:"' + _txt_IBD_CR_Code + '", _txt_IBD_CR_AC_ShortName:"' + _txt_IBD_CR_AC_ShortName + '", _txt_IBD_CR_Cust_abbr:"' + _txt_IBD_CR_Cust_abbr + '", _txt_IBD_CR_Cust_Acc:"' + _txt_IBD_CR_Cust_Acc + '", _txt_IBD_CR_Acceptance_Curr:"' + _txt_IBD_CR_Acceptance_Curr + '", _txt_IBD_CR_Acceptance_amt:"' + _txt_IBD_CR_Acceptance_amt + '", _txt_IBD_CR_Acceptance_payer:"' + _txt_IBD_CR_Acceptance_payer +
        '", _txt_IBD_CR_Interest_Curr:"' + _txt_IBD_CR_Interest_Curr + '", _txt_IBD_CR_Interest_amt:"' + _txt_IBD_CR_Interest_amt + '", _txt_IBD_CR_Interest_payer:"' + _txt_IBD_CR_Interest_payer +
        '", _txt_IBD_CR_Accept_Commission_Curr:"' + _txt_IBD_CR_Accept_Commission_Curr + '", _txt_IBD_CR_Accept_Commission_amt:"' + _txt_IBD_CR_Accept_Commission_amt + '", _txt_IBD_CR_Accept_Commission_Payer:"' + _txt_IBD_CR_Accept_Commission_Payer +
        '", _txt_IBD_CR_Pay_Handle_Commission_Curr:"' + _txt_IBD_CR_Pay_Handle_Commission_Curr + '", _txt_IBD_CR_Pay_Handle_Commission_amt:"' + _txt_IBD_CR_Pay_Handle_Commission_amt + '", _txt_IBD_CR_Pay_Handle_Commission_Payer:"' + _txt_IBD_CR_Pay_Handle_Commission_Payer +
        '", _txt_IBD_CR_Others_Curr:"' + _txt_IBD_CR_Others_Curr + '", _txt_IBD_CR_Others_amt:"' + _txt_IBD_CR_Others_amt + '", _txt_IBD_CR_Others_Payer:"' + _txt_IBD_CR_Others_Payer +
        '", _txt_IBD_CR_Their_Commission_Curr:"' + _txt_IBD_CR_Their_Commission_Curr + '", _txt_IBD_CR_Their_Commission_amt:"' + _txt_IBD_CR_Their_Commission_amt + '", _txt_IBD_CR_Their_Commission_Payer:"' + _txt_IBD_CR_Their_Commission_Payer +
        '", _txt_IBD_IBD_DR_Code:"' + _txt_IBD_IBD_DR_Code + '", _txt_IBD_IBD_DR_Cust_abbr:"' + _txt_IBD_IBD_DR_Cust_abbr + '", _txt_IBD_IBD_DR_Cust_Acc:"' + _txt_IBD_IBD_DR_Cust_Acc + '", _txt_IBD_IBD_DR_Cur_Acc_Curr:"' + _txt_IBD_IBD_DR_Cur_Acc_Curr + '", _txt_IBD_IBD_DR_Cur_Acc_amt:"' + _txt_IBD_IBD_DR_Cur_Acc_amt + '", _txt_IBD_IBD_DR_Cur_Acc_payer:"' + _txt_IBD_IBD_DR_Cur_Acc_payer +

        '", _txt_IBD_DR_Code:"' + _txt_IBD_DR_Code + '", _txt_IBD_DR_Cust_abbr:"' + _txt_IBD_DR_Cust_abbr + '", _txt_IBD_DR_Cust_Acc:"' + _txt_IBD_DR_Cust_Acc + '", _txt_IBD_DR_Cur_Acc_Curr:"' + _txt_IBD_DR_Cur_Acc_Curr + '", _txt_IBD_DR_Cur_Acc_amt:"' + _txt_IBD_DR_Cur_Acc_amt + '", _txt_IBD_DR_Cur_Acc_payer:"' + _txt_IBD_DR_Cur_Acc_payer +


        '", _txt_IBD_DR_Cur_Acc_Curr2:"' + _txt_IBD_DR_Cur_Acc_Curr2 + '", _txt_IBD_DR_Cur_Acc_amt2:"' + _txt_IBD_DR_Cur_Acc_amt2 + '", _txt_IBD_DR_Cur_Acc_payer2:"' + _txt_IBD_DR_Cur_Acc_payer2 +
        '", _txt_IBD_DR_Cur_Acc_Curr3:"' + _txt_IBD_DR_Cur_Acc_Curr3 + '", _txt_IBD_DR_Cur_Acc_amt3:"' + _txt_IBD_DR_Cur_Acc_amt3 + '", _txt_IBD_DR_Cur_Acc_payer3:"' + _txt_IBD_DR_Cur_Acc_payer3 +
        '", _IBDExtn_Flag:"' + _IBDExtn_Flag +
                '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });
}

function OnSuccess(response) {
}

function CustRem(GO) {
    if (GO == 'GO1') {
        var _valRem = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Remarks").val();
        $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Details").val(_valRem);
        $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Details").val(_valRem);
    }
    if (GO == 'GO2') {
        var _valG1 = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Remarks").val();
        $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Details").val(_valG1);
        $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Details").val(_valG1);
    }
}

function Toggle_GO_Amt(GO) {
    if (GO == 'GO1') {
        var _txt_GO1_Debit_Amt = $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Amt").val(_txt_GO1_Debit_Amt);
    }
    if (GO == 'GO2') {
        var _txt_GO2_Debit_Amt = $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Amt").val(_txt_GO2_Debit_Amt);
    }
}
function TogggleDebitCreditCode(GO_No, DebitCredit_No) {
    if (GO_No == '1') {
        if (DebitCredit_No == '1') {
            if ($("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Code").val() != "") {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Curr").val('INR');
            }
            else {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Curr").val('');
            }
        }
        if (DebitCredit_No == '2') {
            if ($("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Code").val() != "") {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Curr").val('INR');
            }
            else {
                $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Curr").val('');
            }
        }
    }
    if (GO_No == '2') {
        if (DebitCredit_No == '1') {
            if ($("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Code").val() != "") {
                $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Curr").val('INR');
            }
            else {
                $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Curr").val('');
            }
        }
        if (DebitCredit_No == '2') {
            if ($("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Code").val() != "") {
                $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Curr").val('INR');
            }
            else {
                $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Curr").val('');
            }
        }
    }
}
function OpenCR_Code_help(e, IMP_ACC, Debit_Credit) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_SundryaccountHelp.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
        return false;
    }
}
function select_CR_Code1(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val(ACcode);
    //$("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val(Acno);
}
function select_DR_Code1(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val(ACcode);
    //$("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val(Acno);
}

function select_CR_Code2(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Credit_Cust_AccNo").val(Acno);
}
function select_DR_Code2(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO1_txt_GO1_Debit_Cust_AccNo").val(Acno);
}
function select_CR_Code3(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Credit_Cust_AccNo").val(Acno);
}
function select_DR_Code3(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AcCode").val(ACcode);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AcCode_Name").val(ACcodeDiscreption);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust").val(ABB);
    $("#TabContainerMain_tbDocumentGO2_txt_GO2_Debit_Cust_AccNo").val(Acno);
}
function select_CR_Code4(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Code").val(ACcode);
    $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_CR_Cust_Acc").val(Acno);
}
function select_DR_Code4(ACcode, ABB, Acno, ACcodeDiscreption) {

    $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Code").val(ACcode);
    $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Cust_abbr").val(ABB);
    $("#TabContainerMain_tbIBDDocumentAccounting_txt_IBD_IBD_DR_Cust_Acc").val(Acno);
}
function validate_Commission_MATU_Code(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 49 && charCode != 50 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}

function validate_Commission_LUMP_Code(evnt) {

    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 89 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}
function validate_Commission_PAYER_Code(evnt) {

    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 66 && charCode != 83 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}

function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}

function SubmitCheck() {
    SaveUpdateData();
    if (confirm('Are you sure you want to Submit this record to checker?')) {
        return true;
    }
    else
        return false;
}

