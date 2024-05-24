function SaveUpdateData() {

    // Document Details
    var hdnUserName = $("#hdnUserName").val();
    var _BranchName = $("#hdnBranchName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _txt_LC_No = $("#txt_LC_No").val();

    var _txtValueDate = $("#txtValueDate").val();
    var _Document_Curr = $("#ddlDoc_Curr").val();
    var _Bill_Amt = $("#txt_Bill_Amt").val();

    var _txtValueDate = $("#txtValueDate").val();

    var Bene_name = $("#TabContainerMain_tbSGRegister_txtbenename").val();
    var Bene_add1 = $("#TabContainerMain_tbSGRegister_txtbeneadd1").val();
    var Bene_add2 = $("#TabContainerMain_tbSGRegister_txtbeneadd2").val();
    var Bene_add3 = $("#TabContainerMain_tbSGRegister_txtbeneadd3").val();
    var Bene_add4 = $("#TabContainerMain_tbSGRegister_txtbeneadd4").val();
    var _Applicantid = $("#TabContainerMain_tbSGRegister_txtApplid").val();
    var _Applicantname = $("#TabContainerMain_tbSGRegister_lblApplName").text(); //label
    var _ApplicantAdd = $("#TabContainerMain_tbSGRegister_txtApplAdd").val();
    var _ApplicantCity = $("#TabContainerMain_tbSGRegister_txtApplCity").val();
    var _ApplicantPincode = $("#TabContainerMain_tbSGRegister_txtApplPincode").val();
    var _LcRefno = $("#TabContainerMain_tbSGRegister_txtlcrefno").val();
    var _Shippingissued = $("#TabContainerMain_tbSGRegister_txtShippingissued").val();
    var _Shippingcompname = $("#TabContainerMain_tbSGRegister_txtShipcompname").val();
    var _SGcountry = $("#TabContainerMain_tbSGRegister_txtcountry").val();
    var _Vesselname1 = $("#TabContainerMain_tbSGRegister_txtVesselname1").val();
    var _Vesselname2 = $("#TabContainerMain_tbSGRegister_txtVesselname2").val();
    var _Vesseldate1 = $("#TabContainerMain_tbSGRegister_txtVesseldate1").val();
    var _Vesseldate2 = $("#TabContainerMain_tbSGRegister_txtVesseldate2").val();

    var _ShipperName = $("#TabContainerMain_tbSGRegister_txtshipper").val();
    var _SupplierName = $("#TabContainerMain_tbSGRegister_txtsupplier").val();
    var _Consignee_Name = $("#TabContainerMain_tbSGRegister_txtconsignee").val();
    var _Notify_Party = $("#TabContainerMain_tbSGRegister_txtnotifyname").val();
    var _Descofgoods = $("#TabContainerMain_tbSGRegister_txtdescofgoods").val();
    var _Quantity = $("#TabContainerMain_tbSGRegister_txtquantity").val();
    var _ShippingMarks = $("#TabContainerMain_tbSGRegister_txtshipmarks").val();
    var _SGCurrency = $("#TabContainerMain_tbSGRegister_txtcurrency").val();
    var _SGBillAmt = $("#TabContainerMain_tbSGRegister_txtbillamt").val();
    var _commercialInvno = $("#TabContainerMain_tbSGRegister_txt_Com_InvNo").val();
    var _Vesselname3 = $("#TabContainerMain_tbSGRegister_txtVesselname3").val();
    var _Billno = $("#TabContainerMain_tbSGRegister_txt_Bill_No").val();
    var _remarks = $("#TabContainerMain_tbSGRegister_txt_Remarks_Reg").val();
    var _SGPolicy = $("#TabContainerMain_tbSGRegister_txtgoodspolicy").val();
    var _SGOurRefNo = $("#TabContainerMain_tbSGRegister_txtOurref").val();
    var _SGAhm = 'N';
    if ($("#TabContainerMain_tbSGRegister_Chk_Ahm").is(':checked')) {
        _Ahm = 'Y';
    }

    var _txt_Doc_Customer_ID = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID").val();
    var _txt_CCode = $("#TabContainerMain_tbDocumentDetails_txt_CCode").val();

    var _txt_Issuing_Date = $("#TabContainerMain_tbDocumentDetails_txt_Issuing_Date").val();
    var _txt_Expiry_Date = $("#TabContainerMain_tbDocumentDetails_txt_Expiry_Date").val();

    var _ddl_revInfoOpt = $("#TabContainerMain_tbDocumentDetails_ddl_revInfoOpt").val();

    var _txt_ApplNoBranch = $("#TabContainerMain_tbDocumentDetails_txt_ApplNoBranch").val();
    var _txt_AdvisingBank = $("#TabContainerMain_tbDocumentDetails_txt_AdvisingBank").val();

    var _ddlCountryCode = $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val();
    var _ddl_Commodity = $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").val();



    var _txt_RiskCountry = $("#TabContainerMain_tbDocumentDetails_txt_RiskCountry").val();
    var _txt_RiskCust = $("#TabContainerMain_tbDocumentDetails_txt_RiskCust").val();

    var _txt_GradeCode = $("#TabContainerMain_tbDocumentDetails_txt_GradeCode").val();
    var _txt_HoApl = $("#TabContainerMain_tbDocumentDetails_txt_HoApl").val();


    var _txt_Remarks = $("#TabContainerMain_tbDocumentDetails_txt_Remarks").val();
    var _txt_RemEUC = $("#TabContainerMain_tbDocumentDetails_txt_RemEUC").val();

    var _txt_Comm_Rate = $("#TabContainerMain_tbDocumentDetails_txt_Comm_Rate").val();
    var _txt_Comm_CalCode = $("#TabContainerMain_tbDocumentDetails_txt_Comm_CalCode").val();
    var _txt_Comm_Interval = $("#TabContainerMain_tbDocumentDetails_txt_Comm_Interval").val();

    var _txt_Comm_Advance = $("#TabContainerMain_tbDocumentDetails_txt_Comm_Advance").val();
    var _txt_Comm_EndInclu = $("#TabContainerMain_tbDocumentDetails_txt_Comm_EndInclu").val();

    var _txt_CreditOpenChrg = $("#TabContainerMain_tbDocumentDetails_txt_CreditOpenChrg").val();
    var _txt_CreditOpenChrg_Curr = $("#TabContainerMain_tbDocumentDetails_txt_CreditOpenChrg_Curr").val();

    var _txt_CreditMailChrgCurr = $("#TabContainerMain_tbDocumentDetails_txt_CreditMailChrgCurr").val();
    var _txt_CreditMailChrg = $("#TabContainerMain_tbDocumentDetails_txt_CreditMailChrg").val();

    var _txt_CreditExchInfCurr = $("#TabContainerMain_tbDocumentDetails_txt_CreditExchInfCurr").val();
    var _txt_CreditExchRate = $("#TabContainerMain_tbDocumentDetails_txt_CreditExchRate").val();
    var _txt_CreditILTExchRate = $("#TabContainerMain_tbDocumentDetails_txt_CreditILTExchRate").val();

    var _txt_DebitAcShortName = $("#TabContainerMain_tbDocumentDetails_txt_DebitAcShortName").val();
    var _txt_DebitCustAbbr = $("#TabContainerMain_tbDocumentDetails_txt_DebitCustAbbr").val();
    var _txt_DebitAcNo = $("#TabContainerMain_tbDocumentDetails_txt_DebitAcNo").val();
    var _txt_DebitCurr = $("#TabContainerMain_tbDocumentDetails_txt_DebitCurr").val();
    var _txt_DebitAmt = $("#TabContainerMain_tbDocumentDetails_txt_DebitAmt").val();

    var _txt_DebitAcShortName2 = $("#TabContainerMain_tbDocumentDetails_txt_DebitAcShortName2").val();
    var _txt_DebitCustAbbr2 = $("#TabContainerMain_tbDocumentDetails_txt_DebitCustAbbr2").val();
    var _txt_DebitAcNo2 = $("#TabContainerMain_tbDocumentDetails_txt_DebitAcNo2").val();
    var _txt_DebitCurr2 = $("#TabContainerMain_tbDocumentDetails_txt_DebitCurr2").val();
    var _txt_DebitAmt2 = $("#TabContainerMain_tbDocumentDetails_txt_DebitAmt2").val();

    ///////////////// GENERAL OPRATOIN 1 /////////////////
    var _chk_GO1Flag = 'N',

	_txt_GO1_Left_Comment = '',
	_txt_GO1_Left_SectionNo = '', _txt_GO1_Left_Remarks = '', _txt_GO1_Left_Memo = '',
	_txt_GO1_Left_Scheme_no = '',

	_txt_GO1_Left_Debit_Code = '', _txt_GO1_Left_Debit_Curr = '', _txt_GO1_Left_Debit_Amt = '',
	_txt_GO1_Left_Debit_Cust = '', _txt_GO1_Left_Debit_Cust_Name = '',
	_txt_GO1_Left_Debit_Cust_AcCode = '', _txt_GO1_Left_Debit_Cust_AcCode_Name = '', _txt_GO1_Left_Debit_Cust_AccNo = '',
	_txt_GO1_Left_Debit_Exch_Rate = '', _txt_GO1_Left_Debit_Exch_CCY = '',
	_txt_GO1_Left_Debit_FUND = '', _txt_GO1_Left_Debit_Check_No = '', _txt_GO1_Left_Debit_Available = '',
	_txt_GO1_Left_Debit_AdPrint = '', _txt_GO1_Left_Debit_Details = '', _txt_GO1_Left_Debit_Entity = '',
	_txt_GO1_Left_Debit_Division = '', _txt_GO1_Left_Debit_Inter_Amount = '', _txt_GO1_Left_Debit_Inter_Rate = '',

	_txt_GO1_Left_Credit_Code = '', _txt_GO1_Left_Credit_Curr = '', _txt_GO1_Left_Credit_Amt = '',
    _txt_GO1_Left_Credit_Cust = '', _txt_GO1_Left_Credit_Cust_Name = '',
	_txt_GO1_Left_Credit_Cust_AcCode = '', _txt_GO1_Left_Credit_Cust_AcCode_Name = '', _txt_GO1_Left_Credit_Cust_AccNo = '',
    _txt_GO1_Left_Credit_Exch_Rate = '', _txt_GO1_Left_Credit_Exch_Curr = '',
	_txt_GO1_Left_Credit_FUND = '', _txt_GO1_Left_Credit_Check_No = '', _txt_GO1_Left_Credit_Available = '',
    _txt_GO1_Left_Credit_AdPrint = '', _txt_GO1_Left_Credit_Details = '', _txt_GO1_Left_Credit_Entity = '',
	_txt_GO1_Left_Credit_Division = '', _txt_GO1_Left_Credit_Inter_Amount = '', _txt_GO1_Left_Credit_Inter_Rate = '',

    _txt_GO1_Right_Comment = '',
	_txt_GO1_Right_SectionNo = '', _txt_GO1_Right_Remarks = '', _txt_GO1_Right_Memo = '',
	_txt_GO1_Right_Scheme_no = '',

	_txt_GO1_Right_Debit_Code = '', _txt_GO1_Right_Debit_Curr = '', _txt_GO1_Right_Debit_Amt = '',
	_txt_GO1_Right_Debit_Cust = '', _txt_GO1_Right_Debit_Cust_Name = '',
	_txt_GO1_Right_Debit_Cust_AcCode = '', _txt_GO1_Right_Debit_Cust_AcCode_Name = '', _txt_GO1_Right_Debit_Cust_AccNo = '',
	_txt_GO1_Right_Debit_Exch_Rate = '', _txt_GO1_Right_Debit_Exch_CCY = '',
	_txt_GO1_Right_Debit_FUND = '', _txt_GO1_Right_Debit_Check_No = '', _txt_GO1_Right_Debit_Available = '',
	_txt_GO1_Right_Debit_AdPrint = '', _txt_GO1_Right_Debit_Details = '', _txt_GO1_Right_Debit_Entity = '',
	_txt_GO1_Right_Debit_Division = '', _txt_GO1_Right_Debit_Inter_Amount = '', _txt_GO1_Right_Debit_Inter_Rate = '',

	_txt_GO1_Right_Credit_Code = '', _txt_GO1_Right_Credit_Curr = '', _txt_GO1_Right_Credit_Amt = '',
	_txt_GO1_Right_Credit_Cust = '', _txt_GO1_Right_Credit_Cust_Name = '',
	_txt_GO1_Right_Credit_Cust_AcCode = '', _txt_GO1_Right_Credit_Cust_AcCode_Name = '', _txt_GO1_Right_Credit_Cust_AccNo = '',
	_txt_GO1_Right_Credit_Exch_Rate = '', _txt_GO1_Right_Credit_Exch_Curr = '',
	_txt_GO1_Right_Credit_FUND = '', _txt_GO1_Right_Credit_Check_No = '', _txt_GO1_Right_Credit_Available = '',
	_txt_GO1_Right_Credit_AdPrint = '', _txt_GO1_Right_Credit_Details = '', _txt_GO1_Right_Credit_Entity = '',
	_txt_GO1_Right_Credit_Division = '', _txt_GO1_Right_Credit_Inter_Amount = '', _txt_GO1_Right_Credit_Inter_Rate = '';

    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        _chk_GO1Flag = 'Y',

        _txt_GO1_Left_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Comment").val(),
        _txt_GO1_Left_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_SectionNo").val(),
        _txt_GO1_Left_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Remarks").val(),
        _txt_GO1_Left_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Memo").val(),
        _txt_GO1_Left_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Scheme_no").val(),

        _txt_GO1_Left_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Code").val(),
        _txt_GO1_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val(),
        _txt_GO1_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Amt").val(),
        _txt_GO1_Left_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust").val(),
        _txt_GO1_Left_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_Name").val(),
        _txt_GO1_Left_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode").val(),
        _txt_GO1_Left_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AccNo").val(),
        _txt_GO1_Left_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode_Name").val(),
        _txt_GO1_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_Rate").val(),
        _txt_GO1_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_CCY").val(),
        _txt_GO1_Left_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_FUND").val(),
        _txt_GO1_Left_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Check_No").val(),
        _txt_GO1_Left_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Available").val(),
        _txt_GO1_Left_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_AdPrint").val(),
        _txt_GO1_Left_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Details").val(),
        _txt_GO1_Left_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Entity").val(),
        _txt_GO1_Left_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Division").val(),
        _txt_GO1_Left_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Inter_Amount").val(),
        _txt_GO1_Left_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Inter_Rate").val(),
        _txt_GO1_Left_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Code").val(),
        _txt_GO1_Left_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val(),
        _txt_GO1_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Amt").val(),
        _txt_GO1_Left_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust").val(),
        _txt_GO1_Left_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_Name").val(),
        _txt_GO1_Left_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode").val(),
        _txt_GO1_Left_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode_Name").val(),
        _txt_GO1_Left_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AccNo").val(),
        _txt_GO1_Left_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Rate").val(),
        _txt_GO1_Left_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Curr").val(),
        _txt_GO1_Left_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_FUND").val(),
        _txt_GO1_Left_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Check_No").val(),
        _txt_GO1_Left_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Available").val(),
        _txt_GO1_Left_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_AdPrint").val(),
        _txt_GO1_Left_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Details").val(),
        _txt_GO1_Left_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Entity").val(),
        _txt_GO1_Left_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Division").val(),
        _txt_GO1_Left_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Inter_Amount").val(),
        _txt_GO1_Left_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Inter_Rate").val(),

        _txt_GO1_Right_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Comment").val(),
        _txt_GO1_Right_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_SectionNo").val(),
        _txt_GO1_Right_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Remarks").val(),
        _txt_GO1_Right_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Memo").val(),
        _txt_GO1_Right_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Scheme_no").val(),

        _txt_GO1_Right_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Code").val(),
        _txt_GO1_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val(),
        _txt_GO1_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Amt").val(),
        _txt_GO1_Right_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust").val(),
        _txt_GO1_Right_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_Name").val(),
        _txt_GO1_Right_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode").val(),
        _txt_GO1_Right_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AccNo").val(),
        _txt_GO1_Right_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode_Name").val(),
        _txt_GO1_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_Rate").val(),
        _txt_GO1_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_CCY").val(),
        _txt_GO1_Right_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_FUND").val(),
        _txt_GO1_Right_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Check_No").val(),
        _txt_GO1_Right_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Available").val(),
        _txt_GO1_Right_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_AdPrint").val(),
        _txt_GO1_Right_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Details").val(),
        _txt_GO1_Right_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Entity").val(),
        _txt_GO1_Right_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Division").val(),
        _txt_GO1_Right_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Inter_Amount").val(),
        _txt_GO1_Right_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Inter_Rate").val(),
        _txt_GO1_Right_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Code").val(),
        _txt_GO1_Right_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val(),
        _txt_GO1_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Amt").val(),
        _txt_GO1_Right_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust").val(),
        _txt_GO1_Right_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_Name").val(),
        _txt_GO1_Right_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode").val(),
        _txt_GO1_Right_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode_Name").val(),
        _txt_GO1_Right_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AccNo").val(),
        _txt_GO1_Right_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Rate").val(),
        _txt_GO1_Right_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Curr").val(),
        _txt_GO1_Right_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_FUND").val(),
        _txt_GO1_Right_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Check_No").val(),
        _txt_GO1_Right_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Available").val(),
        _txt_GO1_Right_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_AdPrint").val(),
        _txt_GO1_Right_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Details").val(),
        _txt_GO1_Right_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Entity").val(),
        _txt_GO1_Right_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Division").val(),
        _txt_GO1_Right_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Inter_Amount").val(),
        _txt_GO1_Right_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Inter_Rate").val();
    }


    ///////////////// GENERAL OPRATOIN 2 /////////////////////////////////////////////////////////////
    var _chk_GO2Flag = 'N',

	    _txt_GO2_Left_Comment = '',
	    _txt_GO2_Left_SectionNo = '', _txt_GO2_Left_Remarks = '', _txt_GO2_Left_Memo = '',
	    _txt_GO2_Left_Scheme_no = '',

	    _txt_GO2_Left_Debit_Code = '', _txt_GO2_Left_Debit_Curr = '', _txt_GO2_Left_Debit_Amt = '',
	    _txt_GO2_Left_Debit_Cust = '', _txt_GO2_Left_Debit_Cust_Name = '',
	    _txt_GO2_Left_Debit_Cust_AcCode = '', _txt_GO2_Left_Debit_Cust_AcCode_Name = '', _txt_GO2_Left_Debit_Cust_AccNo = '',
	    _txt_GO2_Left_Debit_Exch_Rate = '', _txt_GO2_Left_Debit_Exch_CCY = '',
	    _txt_GO2_Left_Debit_FUND = '', _txt_GO2_Left_Debit_Check_No = '', _txt_GO2_Left_Debit_Available = '',
	    _txt_GO2_Left_Debit_AdPrint = '', _txt_GO2_Left_Debit_Details = '', _txt_GO2_Left_Debit_Entity = '',
	    _txt_GO2_Left_Debit_Division = '', _txt_GO2_Left_Debit_Inter_Amount = '', _txt_GO2_Left_Debit_Inter_Rate = '',

	    _txt_GO2_Left_Credit_Code = '', _txt_GO2_Left_Credit_Curr = '', _txt_GO2_Left_Credit_Amt = '',
        _txt_GO2_Left_Credit_Cust = '', _txt_GO2_Left_Credit_Cust_Name = '',
	    _txt_GO2_Left_Credit_Cust_AcCode = '', _txt_GO2_Left_Credit_Cust_AcCode_Name = '', _txt_GO2_Left_Credit_Cust_AccNo = '',
        _txt_GO2_Left_Credit_Exch_Rate = '', _txt_GO2_Left_Credit_Exch_Curr = '',
	    _txt_GO2_Left_Credit_FUND = '', _txt_GO2_Left_Credit_Check_No = '', _txt_GO2_Left_Credit_Available = '',
        _txt_GO2_Left_Credit_AdPrint = '', _txt_GO2_Left_Credit_Details = '', _txt_GO2_Left_Credit_Entity = '',
	    _txt_GO2_Left_Credit_Division = '', _txt_GO2_Left_Credit_Inter_Amount = '', _txt_GO2_Left_Credit_Inter_Rate = '',

        _txt_GO2_Right_Comment = '',
	    _txt_GO2_Right_SectionNo = '', _txt_GO2_Right_Remarks = '', _txt_GO2_Right_Memo = '',
	    _txt_GO2_Right_Scheme_no = '',

	    _txt_GO2_Right_Debit_Code = '', _txt_GO2_Right_Debit_Curr = '', _txt_GO2_Right_Debit_Amt = '',
	    _txt_GO2_Right_Debit_Cust = '', _txt_GO2_Right_Debit_Cust_Name = '',
	    _txt_GO2_Right_Debit_Cust_AcCode = '', _txt_GO2_Right_Debit_Cust_AcCode_Name = '', _txt_GO2_Right_Debit_Cust_AccNo = '',
	    _txt_GO2_Right_Debit_Exch_Rate = '', _txt_GO2_Right_Debit_Exch_CCY = '',
	    _txt_GO2_Right_Debit_FUND = '', _txt_GO2_Right_Debit_Check_No = '', _txt_GO2_Right_Debit_Available = '',
	    _txt_GO2_Right_Debit_AdPrint = '', _txt_GO2_Right_Debit_Details = '', _txt_GO2_Right_Debit_Entity = '',
	    _txt_GO2_Right_Debit_Division = '', _txt_GO2_Right_Debit_Inter_Amount = '', _txt_GO2_Right_Debit_Inter_Rate = '',

	    _txt_GO2_Right_Credit_Code = '', _txt_GO2_Right_Credit_Curr = '', _txt_GO2_Right_Credit_Amt = '',
	    _txt_GO2_Right_Credit_Cust = '', _txt_GO2_Right_Credit_Cust_Name = '',
	    _txt_GO2_Right_Credit_Cust_AcCode = '', _txt_GO2_Right_Credit_Cust_AcCode_Name = '', _txt_GO2_Right_Credit_Cust_AccNo = '',
	    _txt_GO2_Right_Credit_Exch_Rate = '', _txt_GO2_Right_Credit_Exch_Curr = '',
	    _txt_GO2_Right_Credit_FUND = '', _txt_GO2_Right_Credit_Check_No = '', _txt_GO2_Right_Credit_Available = '',
	    _txt_GO2_Right_Credit_AdPrint = '', _txt_GO2_Right_Credit_Details = '', _txt_GO2_Right_Credit_Entity = '',
	    _txt_GO2_Right_Credit_Division = '', _txt_GO2_Right_Credit_Inter_Amount = '', _txt_GO2_Right_Credit_Inter_Rate = '';

    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_chk_GO2Flag").is(':checked')) {
        _chk_GO2Flag = 'Y',

        _txt_GO2_Left_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Comment").val(),
            _txt_GO2_Left_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_SectionNo").val(),
            _txt_GO2_Left_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Remarks").val(),
            _txt_GO2_Left_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Memo").val(),
            _txt_GO2_Left_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Scheme_no").val(),

            _txt_GO2_Left_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Code").val(),
            _txt_GO2_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val(),
            _txt_GO2_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Amt").val(),
            _txt_GO2_Left_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust").val(),
            _txt_GO2_Left_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_Name").val(),
            _txt_GO2_Left_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode").val(),
            _txt_GO2_Left_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AccNo").val(),
            _txt_GO2_Left_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode_Name").val(),
            _txt_GO2_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_Rate").val(),
            _txt_GO2_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_CCY").val(),
            _txt_GO2_Left_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_FUND").val(),
            _txt_GO2_Left_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Check_No").val(),
            _txt_GO2_Left_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Available").val(),
            _txt_GO2_Left_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_AdPrint").val(),
            _txt_GO2_Left_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Details").val(),
            _txt_GO2_Left_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Entity").val(),
            _txt_GO2_Left_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Division").val(),
            _txt_GO2_Left_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Inter_Amount").val(),
            _txt_GO2_Left_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Inter_Rate").val(),
            _txt_GO2_Left_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Code").val(),
            _txt_GO2_Left_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val(),
            _txt_GO2_Left_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Amt").val(),
            _txt_GO2_Left_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust").val(),
            _txt_GO2_Left_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_Name").val(),
            _txt_GO2_Left_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode").val(),
            _txt_GO2_Left_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode_Name").val(),
            _txt_GO2_Left_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AccNo").val(),
            _txt_GO2_Left_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Rate").val(),
            _txt_GO2_Left_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Curr").val(),
            _txt_GO2_Left_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_FUND").val(),
            _txt_GO2_Left_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Check_No").val(),
            _txt_GO2_Left_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Available").val(),
            _txt_GO2_Left_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_AdPrint").val(),
            _txt_GO2_Left_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Details").val(),
            _txt_GO2_Left_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Entity").val(),
            _txt_GO2_Left_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Division").val(),
            _txt_GO2_Left_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Inter_Amount").val(),
            _txt_GO2_Left_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Inter_Rate").val(),

            _txt_GO2_Right_Comment = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Comment").val(),
            _txt_GO2_Right_SectionNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_SectionNo").val(),
            _txt_GO2_Right_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Remarks").val(),
            _txt_GO2_Right_Memo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Memo").val(),
            _txt_GO2_Right_Scheme_no = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Scheme_no").val(),

            _txt_GO2_Right_Debit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Code").val(),
            _txt_GO2_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val(),
            _txt_GO2_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Amt").val(),
            _txt_GO2_Right_Debit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust").val(),
            _txt_GO2_Right_Debit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_Name").val(),
            _txt_GO2_Right_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode").val(),
            _txt_GO2_Right_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AccNo").val(),
            _txt_GO2_Right_Debit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode_Name").val(),
            _txt_GO2_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_Rate").val(),
            _txt_GO2_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_CCY").val(),
            _txt_GO2_Right_Debit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_FUND").val(),
            _txt_GO2_Right_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Check_No").val(),
            _txt_GO2_Right_Debit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Available").val(),
            _txt_GO2_Right_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_AdPrint").val(),
            _txt_GO2_Right_Debit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Details").val(),
            _txt_GO2_Right_Debit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Entity").val(),
            _txt_GO2_Right_Debit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Division").val(),
            _txt_GO2_Right_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Inter_Amount").val(),
            _txt_GO2_Right_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Inter_Rate").val(),
            _txt_GO2_Right_Credit_Code = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Code").val(),
            _txt_GO2_Right_Credit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val(),
            _txt_GO2_Right_Credit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Amt").val(),
            _txt_GO2_Right_Credit_Cust = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust").val(),
            _txt_GO2_Right_Credit_Cust_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_Name").val(),
            _txt_GO2_Right_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode").val(),
            _txt_GO2_Right_Credit_Cust_AcCode_Name = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode_Name").val(),
            _txt_GO2_Right_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AccNo").val(),
            _txt_GO2_Right_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Rate").val(),
            _txt_GO2_Right_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Curr").val(),
            _txt_GO2_Right_Credit_FUND = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_FUND").val(),
            _txt_GO2_Right_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Check_No").val(),
            _txt_GO2_Right_Credit_Available = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Available").val(),
            _txt_GO2_Right_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_AdPrint").val(),
            _txt_GO2_Right_Credit_Details = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Details").val(),
            _txt_GO2_Right_Credit_Entity = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Entity").val(),
            _txt_GO2_Right_Credit_Division = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Division").val(),
            _txt_GO2_Right_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Inter_Amount").val(),
            _txt_GO2_Right_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Inter_Rate").val();
    }
    if (_txt_Doc_Customer_ID != '' && _Document_Curr != '' && _Bill_Amt != '' && _Bill_Amt != 0) {
        $.ajax({
            type: "POST",
            url: "TF_IMP_Shipping_Guarantee_Maker.aspx/AddUpdateShippingBillGuarantee",
            data: '{ hdnUserName: "' + hdnUserName + '", _BranchName:"' + _BranchName + '", _txt_LC_No: "' + _txt_LC_No +
        '", _txtDocNo: "' + _txtDocNo + '", _txtValueDate: "' + _txtValueDate + '", _Document_Curr: "' + _Document_Curr + '", _Bill_Amt: "' + _Bill_Amt +
		'", Bene_name: "' + Bene_name +
        '", Bene_add1: "' + Bene_add1 + '", Bene_add2: "' + Bene_add2 +
        '", Bene_add3: "' + Bene_add3 + '", Bene_add4: "' + Bene_add4 +
        '", _Applicantid: "' + _Applicantid + '", _Applicantname: "' + _Applicantname +
        '", _ApplicantAdd: "' + _ApplicantAdd + '", _ApplicantCity: "' + _ApplicantCity + '", _ApplicantPincode: "' + _ApplicantPincode +
        '", _LcRefno: "' + _LcRefno + '", _Shippingissued: "' + _Shippingissued +
        '", _Shippingcompname: "' + _Shippingcompname + '", _SGcountry: "' + _SGcountry +
        '", _Vesselname1: "' + _Vesselname1 + '", _Vesselname2: "' + _Vesselname2 +
        '", _Vesseldate1: "' + _Vesseldate1 + '", _Vesseldate2: "' + _Vesseldate2 +
        '", _ShipperName:"' + _ShipperName + '", _SupplierName:"' + _SupplierName + '", _Consignee_Name:"' + _Consignee_Name +
        '", _Notify_Party:"' + _Notify_Party + '", _Descofgoods:"' + _Descofgoods + '", _Quantity:"' + _Quantity +
        '", _ShippingMarks: "' + _ShippingMarks + '", _SGCurrency: "' + _SGCurrency + '", _SGBillAmt: "' + _SGBillAmt +
        '", _commercialInvno: "' + _commercialInvno + '", _Vesselname3:"' + _Vesselname3 + '", _Billno: "' + _Billno +
        '", _remarks: "' + _remarks + '", _SGPolicy: "' + _SGPolicy + '", _SGOurRefNo: "' + _SGOurRefNo + '", _SGAhm: "' + _SGAhm + 

        '", _txt_Doc_Customer_ID: "' + _txt_Doc_Customer_ID + '", _txt_CCode: "' + _txt_CCode +
		'", _txt_Issuing_Date: "' + _txt_Issuing_Date + '", _txt_Expiry_Date: "' + _txt_Expiry_Date +
		'", _ddl_revInfoOpt:"' + _ddl_revInfoOpt +
		'", _txt_ApplNoBranch: "' + _txt_ApplNoBranch + '", _txt_AdvisingBank: "' + _txt_AdvisingBank +
		'", _ddlCountryCode: "' + _ddlCountryCode + '", _ddl_Commodity: "' + _ddl_Commodity +
		'", _txt_RiskCountry: "' + _txt_RiskCountry + '", _txt_RiskCust: "' + _txt_RiskCust +
		'", _txt_GradeCode: "' + _txt_GradeCode + '", _txt_HoApl: "' + _txt_HoApl +
		'", _txt_Remarks: "' + _txt_Remarks + '", _txt_RemEUC: "' + _txt_RemEUC +
		'", _txt_Comm_Rate: "' + _txt_Comm_Rate + '", _txt_Comm_CalCode: "' + _txt_Comm_CalCode + '", _txt_Comm_Interval: "' + _txt_Comm_Interval +
		'", _txt_Comm_Advance:"' + _txt_Comm_Advance + '", _txt_Comm_EndInclu:"' + _txt_Comm_EndInclu +
		'", _txt_CreditOpenChrg_Curr:"' + _txt_CreditOpenChrg_Curr + '", _txt_CreditOpenChrg:"' + _txt_CreditOpenChrg +
		'", _txt_CreditMailChrgCurr:"' + _txt_CreditMailChrgCurr + '", _txt_CreditMailChrg:"' + _txt_CreditMailChrg +
		'", _txt_CreditExchInfCurr:"' + _txt_CreditExchInfCurr + '", _txt_CreditExchRate:"' + _txt_CreditExchRate + '", _txt_CreditILTExchRate:"' + _txt_CreditILTExchRate +
		'", _txt_DebitAcShortName:"' + _txt_DebitAcShortName + '", _txt_DebitCustAbbr:"' + _txt_DebitCustAbbr + '", _txt_DebitAcNo:"' + _txt_DebitAcNo + '", _txt_DebitCurr:"' + _txt_DebitCurr + '", _txt_DebitAmt:"' + _txt_DebitAmt +
		'", _txt_DebitAcShortName2:"' + _txt_DebitAcShortName2 + '", _txt_DebitCustAbbr2:"' + _txt_DebitCustAbbr2 + '", _txt_DebitAcNo2:"' + _txt_DebitAcNo2 + '", _txt_DebitCurr2:"' + _txt_DebitCurr2 + '", _txt_DebitAmt2:"' + _txt_DebitAmt2 +
            ///////// GENERAL OPRATOIN 1 /////////////
            '", _chk_GO1Flag: "' + _chk_GO1Flag +
            '", _txt_GO1_Left_Comment: "' + _txt_GO1_Left_Comment +
            '", _txt_GO1_Left_SectionNo: "' + _txt_GO1_Left_SectionNo + '", _txt_GO1_Left_Remarks: "' + _txt_GO1_Left_Remarks + '", _txt_GO1_Left_Memo: "' + _txt_GO1_Left_Memo +
            '", _txt_GO1_Left_Scheme_no: "' + _txt_GO1_Left_Scheme_no +
            '", _txt_GO1_Left_Debit_Code: "' + _txt_GO1_Left_Debit_Code + '", _txt_GO1_Left_Debit_Curr: "' + _txt_GO1_Left_Debit_Curr + '", _txt_GO1_Left_Debit_Amt: "' + _txt_GO1_Left_Debit_Amt +
            '", _txt_GO1_Left_Debit_Cust: "' + _txt_GO1_Left_Debit_Cust + '", _txt_GO1_Left_Debit_Cust_Name: "' + _txt_GO1_Left_Debit_Cust_Name +
            '", _txt_GO1_Left_Debit_Cust_AcCode: "' + _txt_GO1_Left_Debit_Cust_AcCode + '", _txt_GO1_Left_Debit_Cust_AcCode_Name: "' + _txt_GO1_Left_Debit_Cust_AcCode_Name + '", _txt_GO1_Left_Debit_Cust_AccNo: "' + _txt_GO1_Left_Debit_Cust_AccNo +
            '", _txt_GO1_Left_Debit_Exch_Rate: "' + _txt_GO1_Left_Debit_Exch_Rate + '", _txt_GO1_Left_Debit_Exch_CCY: "' + _txt_GO1_Left_Debit_Exch_CCY +
            '", _txt_GO1_Left_Debit_FUND: "' + _txt_GO1_Left_Debit_FUND + '", _txt_GO1_Left_Debit_Check_No: "' + _txt_GO1_Left_Debit_Check_No + '", _txt_GO1_Left_Debit_Available:"' + _txt_GO1_Left_Debit_Available +
            '", _txt_GO1_Left_Debit_AdPrint:"' + _txt_GO1_Left_Debit_AdPrint + '", _txt_GO1_Left_Debit_Details: "' + _txt_GO1_Left_Debit_Details + '", _txt_GO1_Left_Debit_Entity: "' + _txt_GO1_Left_Debit_Entity +
            '", _txt_GO1_Left_Debit_Division: "' + _txt_GO1_Left_Debit_Division + '", _txt_GO1_Left_Debit_Inter_Amount: "' + _txt_GO1_Left_Debit_Inter_Amount + '", _txt_GO1_Left_Debit_Inter_Rate: "' + _txt_GO1_Left_Debit_Inter_Rate +
            '", _txt_GO1_Left_Credit_Code: "' + _txt_GO1_Left_Credit_Code + '", _txt_GO1_Left_Credit_Curr: "' + _txt_GO1_Left_Credit_Curr + '", _txt_GO1_Left_Credit_Amt: "' + _txt_GO1_Left_Credit_Amt +
            '", _txt_GO1_Left_Credit_Cust: "' + _txt_GO1_Left_Credit_Cust + '", _txt_GO1_Left_Credit_Cust_Name: "' + _txt_GO1_Left_Credit_Cust_Name +
            '", _txt_GO1_Left_Credit_Cust_AcCode: "' + _txt_GO1_Left_Credit_Cust_AcCode + '", _txt_GO1_Left_Credit_Cust_AcCode_Name: "' + _txt_GO1_Left_Credit_Cust_AcCode_Name + '", _txt_GO1_Left_Credit_Cust_AccNo: "' + _txt_GO1_Left_Credit_Cust_AccNo +
            '", _txt_GO1_Left_Credit_Exch_Rate: "' + _txt_GO1_Left_Credit_Exch_Rate + '", _txt_GO1_Left_Credit_Exch_Curr: "' + _txt_GO1_Left_Credit_Exch_Curr +
            '", _txt_GO1_Left_Credit_FUND: "' + _txt_GO1_Left_Credit_FUND + '", _txt_GO1_Left_Credit_Check_No: "' + _txt_GO1_Left_Credit_Check_No + '", _txt_GO1_Left_Credit_Available: "' + _txt_GO1_Left_Credit_Available +
            '", _txt_GO1_Left_Credit_AdPrint: "' + _txt_GO1_Left_Credit_AdPrint + '", _txt_GO1_Left_Credit_Details: "' + _txt_GO1_Left_Credit_Details + '", _txt_GO1_Left_Credit_Entity: "' + _txt_GO1_Left_Credit_Entity +
            '", _txt_GO1_Left_Credit_Division: "' + _txt_GO1_Left_Credit_Division + '", _txt_GO1_Left_Credit_Inter_Amount: "' + _txt_GO1_Left_Credit_Inter_Amount + '", _txt_GO1_Left_Credit_Inter_Rate: "' + _txt_GO1_Left_Credit_Inter_Rate +
            '", _txt_GO1_Right_Comment: "' + _txt_GO1_Right_Comment +
            '", _txt_GO1_Right_SectionNo: "' + _txt_GO1_Right_SectionNo + '", _txt_GO1_Right_Remarks: "' + _txt_GO1_Right_Remarks + '", _txt_GO1_Right_Memo: "' + _txt_GO1_Right_Memo +
            '", _txt_GO1_Right_Scheme_no: "' + _txt_GO1_Right_Scheme_no +
            '", _txt_GO1_Right_Debit_Code: "' + _txt_GO1_Right_Debit_Code + '", _txt_GO1_Right_Debit_Curr: "' + _txt_GO1_Right_Debit_Curr + '", _txt_GO1_Right_Debit_Amt: "' + _txt_GO1_Right_Debit_Amt +
            '", _txt_GO1_Right_Debit_Cust: "' + _txt_GO1_Right_Debit_Cust + '", _txt_GO1_Right_Debit_Cust_Name: "' + _txt_GO1_Right_Debit_Cust_Name +
            '", _txt_GO1_Right_Debit_Cust_AcCode: "' + _txt_GO1_Right_Debit_Cust_AcCode + '", _txt_GO1_Right_Debit_Cust_AcCode_Name: "' + _txt_GO1_Right_Debit_Cust_AcCode_Name + '", _txt_GO1_Right_Debit_Cust_AccNo: "' + _txt_GO1_Right_Debit_Cust_AccNo +
            '", _txt_GO1_Right_Debit_Exch_Rate: "' + _txt_GO1_Right_Debit_Exch_Rate + '", _txt_GO1_Right_Debit_Exch_CCY: "' + _txt_GO1_Right_Debit_Exch_CCY +
            '", _txt_GO1_Right_Debit_FUND: "' + _txt_GO1_Right_Debit_FUND + '", _txt_GO1_Right_Debit_Check_No: "' + _txt_GO1_Right_Debit_Check_No + '", _txt_GO1_Right_Debit_Available:"' + _txt_GO1_Right_Debit_Available +
            '", _txt_GO1_Right_Debit_AdPrint:"' + _txt_GO1_Right_Debit_AdPrint + '", _txt_GO1_Right_Debit_Details: "' + _txt_GO1_Right_Debit_Details + '", _txt_GO1_Right_Debit_Entity: "' + _txt_GO1_Right_Debit_Entity +
            '", _txt_GO1_Right_Debit_Division: "' + _txt_GO1_Right_Debit_Division + '", _txt_GO1_Right_Debit_Inter_Amount: "' + _txt_GO1_Right_Debit_Inter_Amount + '", _txt_GO1_Right_Debit_Inter_Rate: "' + _txt_GO1_Right_Debit_Inter_Rate +
            '", _txt_GO1_Right_Credit_Code: "' + _txt_GO1_Right_Credit_Code + '", _txt_GO1_Right_Credit_Curr: "' + _txt_GO1_Right_Credit_Curr + '", _txt_GO1_Right_Credit_Amt: "' + _txt_GO1_Right_Credit_Amt +
            '", _txt_GO1_Right_Credit_Cust: "' + _txt_GO1_Right_Credit_Cust + '", _txt_GO1_Right_Credit_Cust_Name: "' + _txt_GO1_Right_Credit_Cust_Name +
            '", _txt_GO1_Right_Credit_Cust_AcCode: "' + _txt_GO1_Right_Credit_Cust_AcCode + '", _txt_GO1_Right_Credit_Cust_AcCode_Name: "' + _txt_GO1_Right_Credit_Cust_AcCode_Name + '", _txt_GO1_Right_Credit_Cust_AccNo: "' + _txt_GO1_Right_Credit_Cust_AccNo +
            '", _txt_GO1_Right_Credit_Exch_Rate: "' + _txt_GO1_Right_Credit_Exch_Rate + '", _txt_GO1_Right_Credit_Exch_Curr: "' + _txt_GO1_Right_Credit_Exch_Curr +
            '", _txt_GO1_Right_Credit_FUND: "' + _txt_GO1_Right_Credit_FUND + '", _txt_GO1_Right_Credit_Check_No: "' + _txt_GO1_Right_Credit_Check_No + '", _txt_GO1_Right_Credit_Available: "' + _txt_GO1_Right_Credit_Available +
            '", _txt_GO1_Right_Credit_AdPrint: "' + _txt_GO1_Right_Credit_AdPrint + '", _txt_GO1_Right_Credit_Details: "' + _txt_GO1_Right_Credit_Details + '", _txt_GO1_Right_Credit_Entity: "' + _txt_GO1_Right_Credit_Entity +
            '", _txt_GO1_Right_Credit_Division: "' + _txt_GO1_Right_Credit_Division + '", _txt_GO1_Right_Credit_Inter_Amount: "' + _txt_GO1_Right_Credit_Inter_Amount + '", _txt_GO1_Right_Credit_Inter_Rate: "' + _txt_GO1_Right_Credit_Inter_Rate +

            ///////////////// GENERAL OPRATOIN 2 ////////////////////////////
            '", _chk_GO2Flag: "' + _chk_GO2Flag +
            '", _txt_GO2_Left_Comment: "' + _txt_GO2_Left_Comment +
            '", _txt_GO2_Left_SectionNo: "' + _txt_GO2_Left_SectionNo + '", _txt_GO2_Left_Remarks: "' + _txt_GO2_Left_Remarks + '", _txt_GO2_Left_Memo: "' + _txt_GO2_Left_Memo +
            '", _txt_GO2_Left_Scheme_no: "' + _txt_GO2_Left_Scheme_no +
            '", _txt_GO2_Left_Debit_Code: "' + _txt_GO2_Left_Debit_Code + '", _txt_GO2_Left_Debit_Curr: "' + _txt_GO2_Left_Debit_Curr + '", _txt_GO2_Left_Debit_Amt: "' + _txt_GO2_Left_Debit_Amt +
            '", _txt_GO2_Left_Debit_Cust: "' + _txt_GO2_Left_Debit_Cust + '", _txt_GO2_Left_Debit_Cust_Name: "' + _txt_GO2_Left_Debit_Cust_Name +
            '", _txt_GO2_Left_Debit_Cust_AcCode: "' + _txt_GO2_Left_Debit_Cust_AcCode + '", _txt_GO2_Left_Debit_Cust_AcCode_Name: "' + _txt_GO2_Left_Debit_Cust_AcCode_Name + '", _txt_GO2_Left_Debit_Cust_AccNo: "' + _txt_GO2_Left_Debit_Cust_AccNo +
            '", _txt_GO2_Left_Debit_Exch_Rate: "' + _txt_GO2_Left_Debit_Exch_Rate + '", _txt_GO2_Left_Debit_Exch_CCY: "' + _txt_GO2_Left_Debit_Exch_CCY +
            '", _txt_GO2_Left_Debit_FUND: "' + _txt_GO2_Left_Debit_FUND + '", _txt_GO2_Left_Debit_Check_No: "' + _txt_GO2_Left_Debit_Check_No + '", _txt_GO2_Left_Debit_Available:"' + _txt_GO2_Left_Debit_Available +
            '", _txt_GO2_Left_Debit_AdPrint:"' + _txt_GO2_Left_Debit_AdPrint + '", _txt_GO2_Left_Debit_Details: "' + _txt_GO2_Left_Debit_Details + '", _txt_GO2_Left_Debit_Entity: "' + _txt_GO2_Left_Debit_Entity +
            '", _txt_GO2_Left_Debit_Division: "' + _txt_GO2_Left_Debit_Division + '", _txt_GO2_Left_Debit_Inter_Amount: "' + _txt_GO2_Left_Debit_Inter_Amount + '", _txt_GO2_Left_Debit_Inter_Rate: "' + _txt_GO2_Left_Debit_Inter_Rate +
            '", _txt_GO2_Left_Credit_Code: "' + _txt_GO2_Left_Credit_Code + '", _txt_GO2_Left_Credit_Curr: "' + _txt_GO2_Left_Credit_Curr + '", _txt_GO2_Left_Credit_Amt: "' + _txt_GO2_Left_Credit_Amt +
            '", _txt_GO2_Left_Credit_Cust: "' + _txt_GO2_Left_Credit_Cust + '", _txt_GO2_Left_Credit_Cust_Name: "' + _txt_GO2_Left_Credit_Cust_Name +
            '", _txt_GO2_Left_Credit_Cust_AcCode: "' + _txt_GO2_Left_Credit_Cust_AcCode + '", _txt_GO2_Left_Credit_Cust_AcCode_Name: "' + _txt_GO2_Left_Credit_Cust_AcCode_Name + '", _txt_GO2_Left_Credit_Cust_AccNo: "' + _txt_GO2_Left_Credit_Cust_AccNo +
            '", _txt_GO2_Left_Credit_Exch_Rate: "' + _txt_GO2_Left_Credit_Exch_Rate + '", _txt_GO2_Left_Credit_Exch_Curr: "' + _txt_GO2_Left_Credit_Exch_Curr +
            '", _txt_GO2_Left_Credit_FUND: "' + _txt_GO2_Left_Credit_FUND + '", _txt_GO2_Left_Credit_Check_No: "' + _txt_GO2_Left_Credit_Check_No + '", _txt_GO2_Left_Credit_Available: "' + _txt_GO2_Left_Credit_Available +
            '", _txt_GO2_Left_Credit_AdPrint: "' + _txt_GO2_Left_Credit_AdPrint + '", _txt_GO2_Left_Credit_Details: "' + _txt_GO2_Left_Credit_Details + '", _txt_GO2_Left_Credit_Entity: "' + _txt_GO2_Left_Credit_Entity +
            '", _txt_GO2_Left_Credit_Division: "' + _txt_GO2_Left_Credit_Division + '", _txt_GO2_Left_Credit_Inter_Amount: "' + _txt_GO2_Left_Credit_Inter_Amount + '", _txt_GO2_Left_Credit_Inter_Rate: "' + _txt_GO2_Left_Credit_Inter_Rate +
            '", _txt_GO2_Right_Comment: "' + _txt_GO2_Right_Comment +
            '", _txt_GO2_Right_SectionNo: "' + _txt_GO2_Right_SectionNo + '", _txt_GO2_Right_Remarks: "' + _txt_GO2_Right_Remarks + '", _txt_GO2_Right_Memo: "' + _txt_GO2_Right_Memo +
            '", _txt_GO2_Right_Scheme_no: "' + _txt_GO2_Right_Scheme_no +
            '", _txt_GO2_Right_Debit_Code: "' + _txt_GO2_Right_Debit_Code + '", _txt_GO2_Right_Debit_Curr: "' + _txt_GO2_Right_Debit_Curr + '", _txt_GO2_Right_Debit_Amt: "' + _txt_GO2_Right_Debit_Amt +
            '", _txt_GO2_Right_Debit_Cust: "' + _txt_GO2_Right_Debit_Cust + '", _txt_GO2_Right_Debit_Cust_Name: "' + _txt_GO2_Right_Debit_Cust_Name +
            '", _txt_GO2_Right_Debit_Cust_AcCode: "' + _txt_GO2_Right_Debit_Cust_AcCode + '", _txt_GO2_Right_Debit_Cust_AcCode_Name: "' + _txt_GO2_Right_Debit_Cust_AcCode_Name + '", _txt_GO2_Right_Debit_Cust_AccNo: "' + _txt_GO2_Right_Debit_Cust_AccNo +
            '", _txt_GO2_Right_Debit_Exch_Rate: "' + _txt_GO2_Right_Debit_Exch_Rate + '", _txt_GO2_Right_Debit_Exch_CCY: "' + _txt_GO2_Right_Debit_Exch_CCY +
            '", _txt_GO2_Right_Debit_FUND: "' + _txt_GO2_Right_Debit_FUND + '", _txt_GO2_Right_Debit_Check_No: "' + _txt_GO2_Right_Debit_Check_No + '", _txt_GO2_Right_Debit_Available:"' + _txt_GO2_Right_Debit_Available +
            '", _txt_GO2_Right_Debit_AdPrint:"' + _txt_GO2_Right_Debit_AdPrint + '", _txt_GO2_Right_Debit_Details: "' + _txt_GO2_Right_Debit_Details + '", _txt_GO2_Right_Debit_Entity: "' + _txt_GO2_Right_Debit_Entity +
            '", _txt_GO2_Right_Debit_Division: "' + _txt_GO2_Right_Debit_Division + '", _txt_GO2_Right_Debit_Inter_Amount: "' + _txt_GO2_Right_Debit_Inter_Amount + '", _txt_GO2_Right_Debit_Inter_Rate: "' + _txt_GO2_Right_Debit_Inter_Rate +
            '", _txt_GO2_Right_Credit_Code: "' + _txt_GO2_Right_Credit_Code + '", _txt_GO2_Right_Credit_Curr: "' + _txt_GO2_Right_Credit_Curr + '", _txt_GO2_Right_Credit_Amt: "' + _txt_GO2_Right_Credit_Amt +
            '", _txt_GO2_Right_Credit_Cust: "' + _txt_GO2_Right_Credit_Cust + '", _txt_GO2_Right_Credit_Cust_Name: "' + _txt_GO2_Right_Credit_Cust_Name +
            '", _txt_GO2_Right_Credit_Cust_AcCode: "' + _txt_GO2_Right_Credit_Cust_AcCode + '", _txt_GO2_Right_Credit_Cust_AcCode_Name: "' + _txt_GO2_Right_Credit_Cust_AcCode_Name + '", _txt_GO2_Right_Credit_Cust_AccNo: "' + _txt_GO2_Right_Credit_Cust_AccNo +
            '", _txt_GO2_Right_Credit_Exch_Rate: "' + _txt_GO2_Right_Credit_Exch_Rate + '", _txt_GO2_Right_Credit_Exch_Curr: "' + _txt_GO2_Right_Credit_Exch_Curr +
            '", _txt_GO2_Right_Credit_FUND: "' + _txt_GO2_Right_Credit_FUND + '", _txt_GO2_Right_Credit_Check_No: "' + _txt_GO2_Right_Credit_Check_No + '", _txt_GO2_Right_Credit_Available: "' + _txt_GO2_Right_Credit_Available +
            '", _txt_GO2_Right_Credit_AdPrint: "' + _txt_GO2_Right_Credit_AdPrint + '", _txt_GO2_Right_Credit_Details: "' + _txt_GO2_Right_Credit_Details + '", _txt_GO2_Right_Credit_Entity: "' + _txt_GO2_Right_Credit_Entity +
            '", _txt_GO2_Right_Credit_Division: "' + _txt_GO2_Right_Credit_Division + '", _txt_GO2_Right_Credit_Inter_Amount: "' + _txt_GO2_Right_Credit_Inter_Amount + '", _txt_GO2_Right_Credit_Inter_Rate: "' + _txt_GO2_Right_Credit_Inter_Rate +
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

}
function OnSuccess(response) {
}
function OnBackClick() {
    SaveUpdateData();
    window.location.href = "TF_IMP_Shipping_Guarantee_Maker_View.aspx";
    return false;
}

function Countryhelp() {
    popup = window.open('../HelpForms/TF_IMP_CountryHelp.aspx', 'helpCountryId', 'height=320,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
    common = "helpCountryId"
    return false;
}
function selectCountry(CountryID, CountryName) {
    document.getElementById('TabContainerMain_tbSGRegister_txtcountry').value = CountryID;
    __doPostBack("txtcountry", "TextChanged");
}

function CustomerHelp() {
    var hdnBranchName = document.getElementById('hdnBranchName');
    var txtCustomer_ID = $get("TabContainerMain_tbSGRegister_txtApplid");

    popup = window.open('../HelpForms/CustomerhelpSG.aspx?BranchName=' + hdnBranchName.value + '&CustID=' + txtCustomer_ID.value, 'helpCustomerId', 'height=320,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
    common = "helpCustomerId"
    return false;
} 

function selectCustomer1(AcNO) {
    document.getElementById('TabContainerMain_tbSGRegister_txtApplid').value = AcNO;
    __doPostBack("txtApplid", "TextChanged");
}

function CurrencyHelp() {
    popup = window.open('../HelpForms/TF_CurrencyLookUp1.aspx', 'helpCurrencyId', 'height=320,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
    common = "helpCurrencyId"
    return false;
}

function selectCurrency(CurrID) {
    document.getElementById('TabContainerMain_tbSGRegister_txtcurrency').value = CurrID;
    //    __doPostBack("txtcurrency", "TextChanged");
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };
    var txtBillAmount = $("#TabContainerMain_tbSGRegister_txtbillamt");
    if ($('#TabContainerMain_tbSGRegister_txtcurrency').val() == 'JPY') {
        txtBillAmount.val(Math.trunc(txtBillAmount.val()));
    }
}


function OnDocNextClick(index) {
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}
function GeneralOperationNextClick(index) {
    SaveUpdateData();
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(2);
    var SubtabContainer = $get('TabContainerMain_tbDocumentGO_TabSubContainerGO');
    SubtabContainer.control.set_activeTabIndex(index);
    return false;
}

function ccyformat1() {
    SaveUpdateData();
    Toggel_Currency();
    
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };
    var txtBillAmount = $("#txt_Bill_Amt");
    var ddlDoc_Curr = $("#ddlDoc_Curr").val().toUpperCase();
    if (ddlDoc_Curr == 'JPY') {
        txtBillAmount.val(Math.trunc(txtBillAmount.val()));
        Toggel_Amount();
    }
    else {
        Toggel_Amount();
    }
}

function ccyformat() {
    SaveUpdateData();
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };
    var txtBillAmount = $("#TabContainerMain_tbSGRegister_txtbillamt");
    var Curr = $('#TabContainerMain_tbSGRegister_txtcurrency').val().toUpperCase();
    if (Curr == 'JPY') {
        txtBillAmount.val(Math.trunc(txtBillAmount.val()));
    }
}

function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}
function Toggel_GO1_Left_Remarks() {
    var _txt_GO1_Left_Remarks = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Remarks").val();
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Details").val(_txt_GO1_Left_Remarks);
    }
}
function TogggleDebitCreditCode(GO_No, DebitCredit_No) {
    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_chk_GO1Flag").is(':checked')) {
        if (GO_No == '1') {
            if (DebitCredit_No == '1') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val('');
                }
            }
            if (DebitCredit_No == '3') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '4') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val('');
                }
            }
        }
    }

    if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_chk_GO2Flag").is(':checked')) {
        if (GO_No == '2') {
            if (DebitCredit_No == '1') {

                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val('');
                }

            }
            if (DebitCredit_No == '2') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val('');
                }
            }
            if (DebitCredit_No == '3') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val('');
                }
            }
            if (DebitCredit_No == '4') {
                if ($("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Code").val() != "") {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val('INR');
                }
                else {
                    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val('');
                }
            }
        }
    }
}
function OpenGO_help(e, IMP_ACC, Debit_Credit) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_imp_SundryaccountHelp1.aspx?IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
        return false;
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
function OpenSG_Cust_help(e, IMP_ACC, Debit_Credit) {
    var keycode, CustAccNo;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        CustAccNo= $("#TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID").val();
        open_popup('../HelpForms/TF_IMP_SG_CustHelp.aspx?CustAccNo=' + CustAccNo + '&IMP_ACC=' + IMP_ACC + '&Debit_Credit=' + Debit_Credit, 500, 500, 'SundryCodeList');
        return false;
    }
}
function txt_GO1_Left_Debit_Curr_Change() {
    var _txt_GO1_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val();
    if (_txt_GO1_Left_Debit_Curr.toUpperCase() != 'INR') {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Curr").val(_txt_GO1_Left_Debit_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_Rate").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_CCY").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Rate").val('');
    }
    GO1_Amt_Calculation();
}
function txt_GO1_Left_Debit_Exch_CCY_Change() {
    var _txt_GO1_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_CCY").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Curr").val(_txt_GO1_Left_Debit_Exch_CCY);
    GO1_Amt_Calculation();
}
function txt_GO1_Left_Debit_Exch_Rate_Change() {
    var _txt_GO1_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_Rate").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Exch_Rate").val(_txt_GO1_Left_Debit_Exch_Rate);
    GO1_Amt_Calculation();
}
function GO1_Amt_Calculation() {
    var _txt_GO1_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Amt").val();
    var _txt_GO1_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Exch_Rate").val();
    var _txt_GO1_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Curr").val();

    if (_txt_GO1_Left_Debit_Amt != '') {
        if (_txt_GO1_Left_Debit_Exch_Rate == '') {
            _txt_GO1_Left_Debit_Exch_Rate = 1;
        }
        var roundamount = parseFloat(_txt_GO1_Left_Debit_Amt * _txt_GO1_Left_Debit_Exch_Rate);
        if (_txt_GO1_Left_Debit_Curr.toUpperCase() == 'INR') {
            roundamount = _txt_GO1_Left_Debit_Amt;
        }
        else {
            roundamount = parseFloat(round(parseFloat(roundamount), 0)).toFixed(0);
        }
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Amt").val(roundamount);
    }
}
// RIGHT
function txt_GO1_Right_Debit_Curr_Change() {
    var _txt_GO1_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val();
    if (_txt_GO1_Right_Debit_Curr.toUpperCase() != 'INR') {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Curr").val(_txt_GO1_Right_Debit_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_CCY").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_Rate").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Rate").val('');
    }
    GO1_Right_Amt_Calculation();
}
function txt_GO1_Right_Debit_Exch_CCY_Change() {
    var _txt_GO1_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_CCY").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Curr").val(_txt_GO1_Right_Debit_Exch_CCY);
    GO1_Right_Amt_Calculation();
}
function txt_GO1_Right_Debit_Exch_Rate_Change() {
    var _txt_GO1_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_Rate").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Exch_Rate").val(_txt_GO1_Right_Debit_Exch_Rate);
    GO1_Right_Amt_Calculation();
}
function GO1_Right_Amt_Calculation() {
    var _txt_GO1_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Amt").val();
    var _txt_GO1_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_Rate").val();
    var _txt_GO1_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Curr").val();
    var _txt_GO1_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Exch_CCY").val();

    if (_txt_GO1_Right_Debit_Amt != '') {
        if (_txt_GO1_Right_Debit_Exch_Rate == '') {
            _txt_GO1_Right_Debit_Exch_Rate = 1;
        }
        var roundamount = parseFloat(_txt_GO1_Right_Debit_Amt * _txt_GO1_Right_Debit_Exch_Rate);
        if (_txt_GO1_Right_Debit_Curr.toUpperCase() == 'INR') {
            roundamount = _txt_GO1_Right_Debit_Amt;
        }
        else {
            roundamount = parseFloat(round(parseFloat(roundamount), 0)).toFixed(0);
        }
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Amt").val(roundamount);
    }
}
// General Opration 2
// LEFT
function txt_GO2_Left_Debit_Curr_Change() {
    var _txt_GO2_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val();
    if (_txt_GO2_Left_Debit_Curr.toUpperCase() != 'INR') {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Curr").val(_txt_GO2_Left_Debit_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_CCY").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_Rate").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Rate").val('');
    }
    GO2_Amt_Calculation();
}
function txt_GO2_Left_Debit_Exch_CCY_Change() {
    var _txt_GO2_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_CCY").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Curr").val(_txt_GO2_Left_Debit_Exch_CCY);
    GO2_Amt_Calculation();
}
function txt_GO2_Left_Debit_Exch_Rate_Change() {
    var _txt_GO2_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_Rate").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Exch_Rate").val(_txt_GO2_Left_Debit_Exch_Rate);
    GO2_Amt_Calculation();
}
function GO2_Amt_Calculation() {
    var _txt_GO2_Left_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Amt").val();
    var _txt_GO2_Left_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_Rate").val();
    var _txt_GO2_Left_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Curr").val();
    var _txt_GO2_Left_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Exch_CCY").val();

    if (_txt_GO2_Left_Debit_Amt != '') {
        if (_txt_GO2_Left_Debit_Exch_Rate == '') {
            _txt_GO2_Left_Debit_Exch_Rate = 1;
        }
        var roundamount = parseFloat(_txt_GO2_Left_Debit_Amt * _txt_GO2_Left_Debit_Exch_Rate);
        if (_txt_GO2_Left_Debit_Curr.toUpperCase() == 'INR') {
            roundamount = _txt_GO2_Left_Debit_Amt;
        }
        else {
            roundamount = parseFloat(round(parseFloat(roundamount), 0)).toFixed(0);
        }
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Amt").val(roundamount);
    }
}
//RIGHT
function txt_GO2_Right_Debit_Curr_Change() {
    var _txt_GO2_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val();
    if (_txt_GO2_Right_Debit_Curr.toUpperCase() != 'INR') {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Curr").val(_txt_GO2_Right_Debit_Curr);
    }
    else {
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_CCY").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_Rate").val('');
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Rate").val('');
    }
    GO2_Right_Amt_Calculation();
}
function txt_GO2_Right_Debit_Exch_CCY_Change() {
    var _txt_GO2_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_CCY").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Curr").val(_txt_GO2_Right_Debit_Exch_CCY);
    GO2_Right_Amt_Calculation();
}
function txt_GO2_Right_Debit_Exch_Rate_Change() {
    var _txt_GO2_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_Rate").val();
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Exch_Rate").val(_txt_GO2_Right_Debit_Exch_Rate);
    GO2_Right_Amt_Calculation();
}
function GO2_Right_Amt_Calculation() {
    var _txt_GO2_Right_Debit_Amt = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Amt").val();
    var _txt_GO2_Right_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_Rate").val();
    var _txt_GO2_Right_Debit_Curr = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Curr").val();
    var _txt_GO2_Right_Debit_Exch_CCY = $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Exch_CCY").val();

    if (_txt_GO2_Right_Debit_Amt != '') {
        if (_txt_GO2_Right_Debit_Exch_Rate != '') {
            _txt_GO2_Right_Debit_Exch_Rate = 1;
        }
        var roundamount = parseFloat(_txt_GO2_Right_Debit_Amt * _txt_GO2_Right_Debit_Exch_Rate);
        if (_txt_GO2_Right_Debit_Curr.toUpperCase() == 'INR') {
            roundamount = _txt_GO2_Right_Debit_Amt;
        }
        else {
            roundamount = parseFloat(round(parseFloat(roundamount), 0)).toFixed(0);
        }
        $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Amt").val(roundamount);
    }
}
function select_GO1_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Details").val(GLcodeDiscreption);
}
function select_GO1_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Details").val(GLcodeDiscreption);
}
function select_GO1_Debit3(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Details").val(GLcodeDiscreption);
}
function select_GO1_Debit4(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit1(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit2(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit3(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Details").val(GLcodeDiscreption);
}
function select_GO2_Debit4(GLcode, GLcodeDiscreption) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode").val(GLcode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Details").val(GLcodeDiscreption);
}
function select_GO1Left1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Debit_Cust_AccNo").val(Acno);
}
function select_GO1Left2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Left_Credit_Cust_AccNo").val(Acno);
}
function select_GO1Right1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Debit_Cust_AccNo").val(Acno);
}
function select_GO1Right2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO1_txt_GO1_Right_Credit_Cust_AccNo").val(Acno);
}

function select_GO2Left1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Debit_Cust_AccNo").val(Acno);
}
function select_GO2Left2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Left_Credit_Cust_AccNo").val(Acno);
}
function select_GO2Right1(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Debit_Cust_AccNo").val(Acno);
}
function select_GO2Right2(ACCCode, CustAbb, Acno, Description) {
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AcCode").val(ACCCode);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust").val(CustAbb);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_Name").val(Description);
    $("#TabContainerMain_tbDocumentGO_TabSubContainerGO_TabPanelGO2_txt_GO2_Right_Credit_Cust_AccNo").val(Acno);
}

function SubmitCheck() {
    if (confirm('Are you sure you want to Submit this record to checker?')) {
        return true;
    }
    else
        return false;
}
function OpenCustomerCodeList(e) {
    var keycode;
    var hdnBranchName = document.getElementById('hdnBranchName');
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        var txtCustomer_ID = $get("TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID");
        open_popup('../HelpForms/TF_IMP_CustomerHelp.aspx?BranchName=' + hdnBranchName.value + '&CustID=' + txtCustomer_ID.value, 400, 500, 'CustomerCodeList');
        return false;
    }
}
function selectCustomer(selectedID) {
    $("#TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID").val(selectedID);
    FillCustomerDetails();
    return true;
}
function FillCustomerDetails() {
    var txtCustomerID = $("#TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID");
    var brachname = $("#hdnBranchName").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fillCustomerMasterDetails",
        data: '{CustomerID:"' + txtCustomerID.val() + '",BranchName:"' + brachname + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CustStatus == "No") {
                $("#hdnCustAbbr").val(data.d.CustAbbr);                
                $("#TabContainerMain_tbDocumentDetails_txt_RiskCust").val(data.d.CustAbbr);
                $("#TabContainerMain_tbDocumentDetails_lblCustomerDesc").text(data.d.CustName);
            }
            else {
                alert(data.d.CustStatus);
                $("#TabContainerMain_tbDocumentDetails_lblCustomerDesc").text('');
                txtCustomerID.val('');
            }
        },
        failure: function (data) { alert(data.d); txtCustomerID.focus(); },
        error: function (data) { alert(data.d); txtCustomerID.focus(); }
    });
}
function OnChange_CreditOpenChrg_Curr() {
    ccyformat3();
    FillDebetorDetails();
}
function OnChange_CreditOpenChrg() {
    ccyformat3();
    FillDebetorDetails();
}
function FillDebetorDetails() {
   // $("#TabContainerMain_tbDocumentDetails_txt_DebitCustAbbr").val($("#hdnCustAbbr").val());
    $("#TabContainerMain_tbDocumentDetails_txt_DebitAcNo").val($("#TabContainerMain_tbDocumentDetails_txt_Doc_Customer_ID").val());
    $("#TabContainerMain_tbDocumentDetails_txt_DebitCurr").val($("#TabContainerMain_tbDocumentDetails_txt_CreditOpenChrg_Curr").val());
    $("#TabContainerMain_tbDocumentDetails_txt_DebitAmt").val($("#TabContainerMain_tbDocumentDetails_txt_CreditOpenChrg").val());
}

function ccyformat2() {
    SaveUpdateData();
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };
    var txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txt_CreditMailChrg");
    var Curr = $('#TabContainerMain_tbDocumentDetails_txt_CreditMailChrgCurr').val().toUpperCase();
    if (Curr == 'JPY') {
        txtBillAmount.val(Math.trunc(txtBillAmount.val()));
    }
}

function ccyformat3() {
    SaveUpdateData();
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };
    var txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txt_CreditOpenChrg");
    var Curr = $('#TabContainerMain_tbDocumentDetails_txt_CreditOpenChrg_Curr').val().toUpperCase();
    if (Curr == 'JPY') {
        txtBillAmount.val(Math.trunc(txtBillAmount.val()));
    }
}

function ccyformat4() {
    SaveUpdateData();
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };
    var txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txt_DebitAmt");
    var Curr = $('#TabContainerMain_tbDocumentDetails_txt_DebitCurr').val().toUpperCase();
    if (Curr == 'JPY') {
        txtBillAmount.val(Math.trunc(txtBillAmount.val()));
    }
}

function ccyformat5() {
    SaveUpdateData();
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };
    var txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txt_DebitAmt2");
    var Curr = $('#TabContainerMain_tbDocumentDetails_txt_DebitCurr2').val().toUpperCase();
    if (Curr == 'JPY') {
        txtBillAmount.val(Math.trunc(txtBillAmount.val()));
    }
}


function Toggel_LC_RefNo() {
    var _txt_LC_RefNo = $("#txt_LC_No").val();

    $("#TabContainerMain_tbSGRegister_txtlcrefno").val(_txt_LC_RefNo);
}
function Toggel_Amount() {
    var _txt_Amt = $("#txt_Bill_Amt").val();

    $("#TabContainerMain_tbSGRegister_txtbillamt").val(_txt_Amt);
}
function Toggel_Currency() {
    var _ddl_Cur = $("#ddlDoc_Curr").val();

    $("#TabContainerMain_tbSGRegister_txtcurrency").val(_ddl_Cur);
}
