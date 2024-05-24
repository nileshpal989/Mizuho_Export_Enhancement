function SaveUpdateData() {

    var hdnUserName = document.getElementById('hdnUserName').value;
    var _txtValueDate = $("#txtValueDate").val();
    var _Branch = $("#TabContainerMain_tbBRODetails_ddlBranch").val();
    var _delorderno = $("#TabContainerMain_tbBRODetails_txtdosrno").val();
    var _delorderno1 = $("#TabContainerMain_tbBRODetails_txtdosrno1").val();
    var _delorderno2 = $("#TabContainerMain_tbBRODetails_txtdosrno2").val();
    var _brodate = $("#TabContainerMain_tbBRODetails_txtdate").val();
    var ship_name = $("#TabContainerMain_tbBRODetails_txtshipname").val();
    var ship_add = $("#TabContainerMain_tbBRODetails_txtshipcoadd").val();
    var ship_add1 = $("#TabContainerMain_tbBRODetails_txtshipcoadd1").val();
    var ship_add2 = $("#TabContainerMain_tbBRODetails_txtshipcoadd2").val();
    var ship_add3 = $("#TabContainerMain_tbBRODetails_txtshipcoadd3").val();
    var _Applicantid = $("#TabContainerMain_tbBRODetails_txtApplid").val();
    var _Applicantname = $("#TabContainerMain_tbBRODetails_lblApplName").text(); //label
    var _ApplicantAdd = $("#TabContainerMain_tbBRODetails_txtApplAdd").val();
    var _ApplicantCity = $("#TabContainerMain_tbBRODetails_txtApplCity").val();
    var _ApplicantPincode = $("#TabContainerMain_tbBRODetails_txtApplPincode").val();
    var _LcRefno = $("#TabContainerMain_tbBRODetails_txtlcrefno").val();
    var _BenName = $("#TabContainerMain_tbBRODetails_txtbenefname").val();
    var _country = $("#TabContainerMain_tbBRODetails_txtcountry").val();
    var _AirwaysBillNo1 =$("#TabContainerMain_tbBRODetails_txtbill1").val();
    var _AirwaysBillNo2 = $("#TabContainerMain_tbBRODetails_txtbill2").val();
    var _AirwaysBillDate = $("#TabContainerMain_tbBRODetails_txtbilldate").val();
    var _AirwaysBillDate2 = $("#TabContainerMain_tbBRODetails_txtbilldate2").val();
    var _AirlinesCompName = $("#TabContainerMain_tbBRODetails_txtAircompname").val();
    var _flightNo1 = $("#TabContainerMain_tbBRODetails_txtflightno1").val();
    var _flightDate1 = $("#TabContainerMain_tbBRODetails_txtairlinedate1").val();
    var _flightNo2 = $("#TabContainerMain_tbBRODetails_txtflightno2").val();
    var _flightDate2 = $("#TabContainerMain_tbBRODetails_txtairlinedate2").val();
    var _ShipperName = $("#TabContainerMain_tbBRODetails_txtshipper").val();
    var _SupplierName = $("#TabContainerMain_tbBRODetails_txtsupplier").val();
    var _Consignee_Name = $("#TabContainerMain_tbBRODetails_txtconsignee").val();
    var _Notify_Party =$("#TabContainerMain_tbBRODetails_txtnotifyname").val();
    var _Descofgoods =$("#TabContainerMain_tbBRODetails_txtdescofgoods").val();
    var _Quantity =$("#TabContainerMain_tbBRODetails_txtquantity").val();
    var _ShippingMarks =$("#TabContainerMain_tbBRODetails_txtshipmarks").val();
    var _Currency =$("#TabContainerMain_tbBRODetails_txtcurrency").val();
    var _BillAmt =$("#TabContainerMain_tbBRODetails_txtbillamt").val();
    var _Policy =$("#TabContainerMain_tbBRODetails_txtgoodspolicy").val();
    var _OurRefNo = $("#TabContainerMain_tbBRODetails_txtref").val();
    var _Ahm ='N';
    if ($("#TabContainerMain_tbBRODetails_Chk_Ahm").is(':checked')) {
        _Ahm  = 'Y';
    }
    var _Gen_opr = 'N';
    if ($("#TabContainerMain_tbBROGO1_Chk_GenOprtn").is(':checked')) {
        _Gen_opr = 'Y';
        var _GO_Comment = $("#TabContainerMain_tbBROGO1_txt_GO1_Comment").val();
        var _GO_sectionno = $("#TabContainerMain_tbBROGO1_txt_GO1_SectionNo").val();
        var _GO_Remarks = $("#TabContainerMain_tbBROGO1_txt_GO1_Remarks").val();
        var _GO_Memo = $("#TabContainerMain_tbBROGO1_txt_GO1_Memo").val();
        var _GO_Schemeno = $("#TabContainerMain_tbBROGO1_txt_GO1_Scheme_no").val();

        var _GO_DebitCredit_Code1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Code").val();
        var _GO_Ccy1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Curr").val();
        var _GO_Amount1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Amt").val();
        var _GO_Custcode1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Cust").val();
      //  var _GO_Custname1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Cust_Name").val();
        var _GO_Accode1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Cust_AcCode").val();
       // var _GO_Acccodename1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Cust_AcCode_Name").val();
        var _GO_Accountno1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Cust_AccNo").val();
        var _GO_Excrate1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Exch_Rate").val();
        var _GO_ExcCCy1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Exch_CCY").val();
        var _GO_Fund1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_FUND").val();
        var _GO_Checkno1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Check_No").val();
        var _GO_Available1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Available").val();
        var _GO_Adviceprint1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_AdPrint").val();
        var _GO_Details1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Details").val();
        var _GO_Entity1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Entity").val();
        var _GO_Division1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Division").val();
        var _GO_Interamount1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Inter_Amount").val();
        var _Go_InterRate1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Inter_Rate").val();

        var _GO_DebitCredit_Code2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Code").val();
        var _GO_Ccy2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Curr").val();
        var _GO_Amount2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Amt").val();
        var _GO_Custcode2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Cust").val();
      //  var _GO_Custname2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Cust_Name").val();
        var _GO_Accode2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Cust_AcCode").val();
       // var _GO_Acccodename2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Cust_AcCode_Name").val();
        var _GO_Accountno2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Cust_AccNo").val();
        var _GO_Excrate2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Exch_Rate").val();
        var _GO_ExcCCy2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Exch_Curr").val();
        var _GO_Fund2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_FUND").val();
        var _GO_Checkno2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Check_No").val();
        var _GO_Available2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Available").val();
        var _GO_Adviceprint2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_AdPrint").val();
        var _GO_Details2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Details").val();
        var _GO_Entity2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Entity").val();
        var _GO_Division2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Division").val();
        var _GO_Interamount2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Inter_Amount").val();
        var _Go_InterRate2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Inter_Rate").val();
    }
    else 
    {
        _Gen_opr = 'N';
        _GO_Comment = "";
        _GO_sectionno = "";
        _GO_Remarks = "";
        _GO_Memo = "";
        _GO_Schemeno = "";

        _GO_DebitCredit_Code1 = "";
        _GO_Ccy1 = "";
        _GO_Amount1 = "";
        _GO_Custcode1 = "";
       // _GO_Custname1 = "";
        _GO_Accode1 = "";
       // _GO_Acccodename1 = "";
        _GO_Accountno1 = "";
        _GO_Excrate1 = "";
        _GO_ExcCCy1 = "";
        _GO_Fund1 = "";
        _GO_Checkno1 = "";
        _GO_Available1 = "";
        _GO_Adviceprint1 = "";
        _GO_Details1 = "";
        _GO_Entity1 = "";
        _GO_Division1 = "";
        _GO_Interamount1 = "";
        _Go_InterRate1 = "";

        _GO_DebitCredit_Code2 = "";
        _GO_Ccy2 = "";
        _GO_Amount2 = "";
        _GO_Custcode2 = "";
       // _GO_Custname2 = "";
        _GO_Accode2 = "";
      //  _GO_Acccodename2 = "";
        _GO_Accountno2 = "";
        _GO_Excrate2 = "";
        _GO_ExcCCy2 = "";
        _GO_Fund2 = "";
        _GO_Checkno2 = "";
        _GO_Available2 = "";
        _GO_Adviceprint2 = "";
        _GO_Details2 = "";
        _GO_Entity2 = "";
        _GO_Division2 = "";
        _GO_Interamount2 = "";
        _Go_InterRate2 = "";

    }
    

    if (_delorderno2 != '') {
        $.ajax({
            type: "POST",
            url: "TF_IMP_BRO.aspx/AddUpdateBro",
            data: '{ hdnUserName:"' + hdnUserName + '", _txtValueDate :"' + _txtValueDate + '",_Branch :"' + _Branch + '",_delorderno :"' + _delorderno + '",_delorderno1 :"' + _delorderno1 + '",_delorderno2 :"' + _delorderno2 +
            '",_brodate : "' + _brodate + '", ship_name : "' + ship_name + '", ship_add : "' + ship_add + '", ship_add1 : "' + ship_add1 + '", ship_add2 : "' + ship_add2 + '", ship_add3 : "' + ship_add3 + '", _Applicantid : "' + _Applicantid +
            '", _Applicantname : "' + _Applicantname + '", _ApplicantAdd : "' + _ApplicantAdd + '", _ApplicantCity : "' + _ApplicantCity + '", _ApplicantPincode : "' + _ApplicantPincode + '", _LcRefno : "' + _LcRefno + '", _BenName : "' + _BenName + '", _country : "' + _country +
            '", _AirwaysBillNo1 : "' + _AirwaysBillNo1 + '", _AirwaysBillNo2 : "' + _AirwaysBillNo2 + '",_AirwaysBillDate : "' + _AirwaysBillDate + '", _AirwaysBillDate2 : "' + _AirwaysBillDate2 + '", _AirlinesCompName : "' + _AirlinesCompName
            + '", _flightNo1 : "' + _flightNo1 + '", _flightDate1 : "' + _flightDate1 + '",_flightNo2 : "' + _flightNo2 + '", _flightDate2 : "' + _flightDate2 + '", _ShipperName : "' + _ShipperName +
            '", _SupplierName : "' + _SupplierName + '", _Consignee_Name : "' + _Consignee_Name + '", _Notify_Party : "' + _Notify_Party + '", _Descofgoods : "' + _Descofgoods +
            '", _Quantity : "' + _Quantity + '", _ShippingMarks : "' + _ShippingMarks + '", _Currency : "' + _Currency + '", _BillAmt : "' + _BillAmt + '", _Policy : "' + _Policy +
             '", _OurRefNo : "' + _OurRefNo + '", _Ahm : "' + _Ahm + '", _Gen_opr : "' + _Gen_opr +

             '", _GO_Comment : "' + _GO_Comment + '", _GO_sectionno : "' + _GO_sectionno + '", _GO_Remarks : "' + _GO_Remarks + '", _GO_Memo : "' + _GO_Memo + '", _GO_Schemeno : "' + _GO_Schemeno +

             '", _GO_DebitCredit_Code1 : "' + _GO_DebitCredit_Code1 + '", _GO_Ccy1 : "' + _GO_Ccy1 + '", _GO_Amount1 : "' + _GO_Amount1 + '", _GO_Custcode1 : "' + _GO_Custcode1 +
            '", _GO_Accode1 : "' + _GO_Accode1 + 
             '", _GO_Accountno1 : "' + _GO_Accountno1 +
            '", _GO_Excrate1 : "' + _GO_Excrate1 + '", _GO_ExcCCy1 : "' + _GO_ExcCCy1 + '", _GO_Fund1 : "' + _GO_Fund1 + '", _GO_Checkno1 : "' + _GO_Checkno1 + '", _GO_Available1 : "' + _GO_Available1 +
             '", _GO_Adviceprint1  : "' + _GO_Adviceprint1 + '", _GO_Details1  : "' + _GO_Details1 + '", _GO_Entity1   : "' + _GO_Entity1 + '", _GO_Division1   : "' + _GO_Division1 +
             '", _GO_Interamount1   : "' + _GO_Interamount1 + '", _Go_InterRate1   : "' + _Go_InterRate1 +

             '", _GO_DebitCredit_Code2  : "' + _GO_DebitCredit_Code2 + '", _GO_Ccy2  : "' + _GO_Ccy2 + '", _GO_Amount2  : "' + _GO_Amount2 + '", _GO_Custcode2  : "' + _GO_Custcode2 +
             '", _GO_Accode2  : "' + _GO_Accode2 +
               '", _GO_Accountno2  : "' + _GO_Accountno2 +
            '", _GO_Excrate2  : "' + _GO_Excrate2 + '", _GO_ExcCCy2   : "' + _GO_ExcCCy2 + '", _GO_Fund2   : "' + _GO_Fund2 + '", _GO_Checkno2    : "' + _GO_Checkno2 + '", _GO_Available2    : "' + _GO_Available2 +
              '", _GO_Adviceprint2   : "' + _GO_Adviceprint2 + '", _GO_Details2   : "' + _GO_Details2 + '", _GO_Entity2    : "' + _GO_Entity2 + '", _GO_Division2    : "' + _GO_Division2 +
             '", _GO_Interamount2    : "' + _GO_Interamount2 + '", _Go_InterRate2    : "' + _Go_InterRate2 +  '"}',

             contentType: "application/json; charset=utf-8",
             dataType: "json",
             failure: function (response) {
                 alert(response.d);
             },
             error: function (response) {
                 alert(response.d);
             }
         });
     }
 }
 function SubmitValidation() {
     SaveUpdateData();
     if (confirm('Are you sure you want to Submit this record to checker?')) {
         SubmitToChecker();
     }
     else {
         return false;
     }
 }

 function round(values, decimals) {
     return Number(Math.round(values + 'e' + decimals) + 'e-' + decimals);
 }

 function validate_Number(evnt) {
     var charCode = (evnt.which) ? evnt.which : event.keyCode;
     if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
         return false;
     else
         return true;
 }


 function Toggel_Debit_Amt1() {
     var _txt_GO_Swift_SFMS_Debit_Amt = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Amt").val();
     $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Amt").val(_txt_GO_Swift_SFMS_Debit_Amt);
 }

 function Toggel_GO1_Remarks() {
     var _txt_GO_Remarks = $("#TabContainerMain_tbBROGO1_txt_GO1_Remarks").val();
     $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Details").val(_txt_GO_Remarks);
     $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Details").val(_txt_GO_Remarks);
 }


 function Toggel_Debit_Amt_ontab() {
     if ($("#TabContainerMain_tbBROGO1_Chk_GenOprtn").is(':checked')) {
         var _GO_Amount1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Amt").val();
         $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Amt").val(_GO_Amount1);
     }
 }

 function CustomerHelp() {
     popup = window.open('../HelpForms/TF_CustomerLookUp1.aspx', 'helpCustomerId', 'height=320,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
     common = "helpCustomerId"
     return false;
 }

 function selectCustomer(AcNO) {
     document.getElementById('TabContainerMain_tbBRODetails_txtApplid').value = AcNO;
     __doPostBack("txtApplid", "TextChanged");
 }

 function CurrencyHelp() {
     popup = window.open('../../HelpForms/TF_CurrencyLookUp1.aspx', 'helpCurrencyId', 'height=320,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
     common = "helpCurrencyId"
     return false;
 }

 function ccyformat1() {
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
     var txtBillAmount = $("#TabContainerMain_tbBRODetails_txtbillamt");
     if ($('#TabContainerMain_tbBRODetails_txtcurrency').val() == 'jpy') {
         txtBillAmount.val(Math.trunc(txtBillAmount.val()));
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
     var txtBillAmount = $("#TabContainerMain_tbBRODetails_txtbillamt");
     var Curren = $('#TabContainerMain_tbBRODetails_txtcurrency').val(); 
         if (Curren == 'jpy') {
         txtBillAmount.val(Math.trunc(txtBillAmount.val()));
     }
 }

 function OnGOCurrencyChange() {
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
     
     var txtBillAmount1 = $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Amt");
     var curr = $('#TabContainerMain_tbBROGO1_txt_GO1_Debit_Curr').val();
     if (curr == 'jpy') {
         txtBillAmount1.val(Math.trunc(txtBillAmount1.val()));
         Toggel_Debit_Amt_ontab();
     }
 }

 function OnGOCurrencyChange1() {
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

     var txtBillAmount2 = $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Amt");
     var curr = $('#TabContainerMain_tbBROGO1_txt_GO1_Credit_Curr').val();
     if (curr == 'jpy') {
         txtBillAmount2.val(Math.trunc(txtBillAmount2.val()));
     }
 }


 function selectCurrency(CurrID) {
     document.getElementById('TabContainerMain_tbBRODetails_txtcurrency').value = CurrID;
     Math.trunc = Math.trunc || function (x) {
         if (isNaN(x)) {
             return NaN;
         }
         if (x > 0) {
             return Math.floor(x);
         }
         return Math.ceil(x);
     };
     var txtBillAmount = $("#TabContainerMain_tbBRODetails_txtbillamt");
     if ($('#TabContainerMain_tbBRODetails_txtcurrency').val() == 'JPY') {
         txtBillAmount.val(Math.trunc(txtBillAmount.val()));
     }
     else {
         txtBillAmount.val(Math.trunc(txtBillAmount.val()).toFixed(2));
     }
 }

 function Countryhelp() {
     popup = window.open('../HelpForms/TF_IMP_CountryHelp.aspx', 'helpCountryId', 'height=320,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
     common = "helpCountryId"
     return false;
 }
 function selectCountry(CountryID, CountryName) {
     document.getElementById('TabContainerMain_tbBRODetails_txtcountry').value = CountryID;
     __doPostBack("txtcountry", "TextChanged");
 }
 function OnDocNextClick(index) {
     SaveUpdateData();
     var tabContainer = $get('TabContainerMain');
     tabContainer.control.set_activeTabIndex(index);
     return false;
 }
 function checkSystemDate(controlID) {
     var obj = controlID;
     if (controlID.value != "__/__/____") {
         var day = obj.value.split("/")[0];
         var month = obj.value.split("/")[1];
         var year = obj.value.split("/")[2];
         var dt = new Date(year, month - 1, day);
         var today = new Date();
         if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {
             alert("Invalid Date");
             controlID.focus();
             return false;
         }
     }
 }
 function SubmitToChecker() {
     var DocNo1 = $("#TabContainerMain_tbBRODetails_txtdosrno").val();
     var DocNo2 = $("#TabContainerMain_tbBRODetails_txtdosrno1").val();
     var DocNo3 = $("#TabContainerMain_tbBRODetails_txtdosrno2").val();
     $.ajax({
         type: "POST",
         url: "TF_IMP_BRO.aspx/SubmitToChecker",
         data: '{DocNo1:"' + DocNo1 + '",DocNo2:"' + DocNo2 + '",DocNo3:"' + DocNo3 + '"}',
         datatype: "json",
         contentType: "application/json;charset=utf-8",
         success: function (data) {
             if (data.d.CheckerStatus == "Submit") {
                 window.location.href = "TF_IMP_ViewBRO.aspx?result=Submit";
             }
             else {
                 alert(data.CheckerStatus);
             }
         },
         failure: function (data) { alert(data.d); },
         error: function (data) { alert(data.d); }
     });
 }
 function OnBackClick() {
     SaveUpdateData();
     window.location.href = "TF_IMP_ViewBRO.aspx";
     return false;
 }