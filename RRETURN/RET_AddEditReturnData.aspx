﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RET_AddEditReturnData.aspx.cs"
    Inherits="RRETURN_RET_AddEditReturnData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="Shortcut Icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Scripts/Validations.js"></script>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function DoBlurtextbox(fld) {
            fld.className = 'textBox';
        }
        function DoBlurtextBoxRight(fld) {
            fld.className = 'textBoxRight';
        }
        function DoBlurdropdown(fld) {
            fld.className = 'dropdownList';
        }
        function DoFocus(fld) {
            fld.className = 'focus';
        }
        function DotextBoxRight(fld) {
            fld.className = 'textBoxRightfocus';
        }
        function AddLeadingZero(currentField) {
            //Check if the value length hasn't reach its max length yet
            if (currentField.value.length != currentField.maxLength) {
                //Add leading zero(s) in front of the value
                var numToAdd = currentField.maxLength - currentField.value.length;
                var value = "";
                for (var i = 0; i < numToAdd; i++) {
                    value += "0";
                }
                currentField.value = value + currentField.value;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function validate_dateRange() {
            var str1 = document.getElementById("txtFromDate").value;
            var str2 = document.getElementById("txtDocumentDate").value;
            var str3 = document.getElementById("txtToDate").value;
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10) - 1;
            var yr1 = parseInt(str1.substring(6, 10), 10);
            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10) - 1;
            var yr2 = parseInt(str2.substring(6, 10), 10);
            var dt3 = parseInt(str3.substring(0, 2), 10);
            var mon3 = parseInt(str3.substring(3, 5), 10) - 1;
            var yr3 = parseInt(str3.substring(6, 10), 10);
            var frmDate = new Date(yr1, mon1, dt1);
            var docDate = new Date(yr2, mon2, dt2);
            var toDate = new Date(yr3, mon3, dt3);
            if (document.getElementById("txtDocumentDate").value != '__/__/____') {
                if ((frmDate <= docDate) && (docDate <= toDate)) {
                }
                else {
                    alert('Document Date should be between the From Date and To Date.');
                    document.getElementById("txtDocumentDate").value = '__/__/____';
                    // document.getElementById("txtDocumentDate").focus();
                    return true;
                }
            }
        }
        function formatBillNumber() {
            var billNumber = document.getElementById('txtBillNumber');
            var billPrefixs = billNumber.value[0].toUpperCase();
            if (billPrefixs == 'N' || billPrefixs == 'D' || billPrefixs == 'P' || billPrefixs == 'C' || billPrefixs == 'M' || billPrefixs == 'A') {
                var spaces = '';
                if (trimAll(billNumber.value) != '') {
                    var number = billNumber.value;
                    var len = 6 - (number.length - 1);
                    if (len > 0) {
                        for (var i = 0; i < len; i++) {
                            spaces += '0';
                        }
                    }
                }
                billNumber.value = billPrefixs.toUpperCase() + spaces + billNumber.value.substring(1, number.length);
            }
            else {
                alert('Bill Number Prefix Should be [N/D/P/C/M/A]');
                billNumber.focus();
                return false;
            }
            return true;
        }
        function formatFormNumber() {
            var formnumber = document.getElementById('txtFormNumber');
            var formPrefixs = formnumber.value.substring(0, 2).toUpperCase();
            if (trimAll(formnumber.value) == '') {
                formnumber.value = 'ZZ000000';
                return true;
            }
            if (formPrefixs != '') {
                var spaces = '';
                if (trimAll(formnumber.value) != '') {
                    var number = formnumber.value;
                    var len = 6 - (number.length - 2);
                    if (len > 0) {
                        for (var i = 0; i < len; i++) {
                            spaces += '0';
                        }
                    }
                }
                formnumber.value = formPrefixs.toUpperCase() + spaces + formnumber.value.substring(2, number.length);
            }
            else {
                alert('Form Number Prefix Should be must');
                formnumber.focus();
                return false;
            }
            return true;
        }
        function AddCoomaToTextboxes() {
            var pCode = document.getElementById('txtPurposecode');
            if (pCode.value == 'P0091' || pCode.value == 'P0092' || pCode.value == 'P0093' || pCode.value == 'P0094' || pCode.value == 'P0095' || pCode.value == 'S0091' || pCode.value == 'S0092' || pCode.value == 'S0093' || pCode.value == 'S0094' || pCode.value == 'S0095') {
                document.getElementById('txtBeneficiaryName').focus();
            }
            if (document.getElementById('txtFCAmount').value != '') {
                document.getElementById('txtFCAmount').value = parseFloat(Math.round(document.getElementById('txtFCAmount').value)).toFixed(2);
                calculateINRForFC();
            }
            var txtvaluetxtFCAmount = trimAll(document.getElementById('txtFCAmount').value);
            if (txtvaluetxtFCAmount == '')
                txtvaluetxtFCAmount = 0;
            else
                document.getElementById('txtFCAmount').value = txtvaluetxtFCAmount;
            var txtvaluetxtINRAmount = trimAll(document.getElementById('txtINRAmount').value);
            if (txtvaluetxtINRAmount == '')
                txtvaluetxtINRAmount = 0;
            else
                document.getElementById('txtINRAmount').value = txtvaluetxtINRAmount;
            var txtvaluetxtRealisedAmount = trimAll(document.getElementById('txtRealisedAmount').value);
            if (txtvaluetxtRealisedAmount == '')
                txtvaluetxtRealisedAmount = 0;
            else
                document.getElementById('txtRealisedAmount').value = txtvaluetxtRealisedAmount;
            var _export = document.getElementById('rbtnExport');
            var _import = document.getElementById('rbtnImport');
            var _inward = document.getElementById('rbtnInward');
            var _outwards = document.getElementById('rbtnOutward');
            var _others = document.getElementById('rbtnOthers');
            if (document.getElementById('rbtnExport').checked == true) {
                if (document.getElementById('txtPurposecode').value == 'P0102' || document.getElementById('txtPurposecode').value == 'P0107') {
                    document.getElementById('txtRealisedAmount').focus();
                    return true;
                }
                else {
                    document.getElementById('txtBeneficiaryName').focus();
                    return true;
                }
            }
            else {
                if (pCode.value == 'P0091' || pCode.value == 'P0092' || pCode.value == 'P0093' || pCode.value == 'P0094' || pCode.value == 'P0095' || pCode.value == 'S0091' || pCode.value == 'S0092' || pCode.value == 'S0093' || pCode.value == 'S0094' || pCode.value == 'S0095') {
                    document.getElementById('btnSave').focus();
                    return true;
                }
                else {
                    document.getElementById('txtExchangeRate').focus();
                    return true;
                }
            }
        }
        function validate_date(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 111 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validateSave() {
            var expAlpha = /^[a-zA-Z0-9]+$/i;
            var expNumeric = /^[-+]?[0-9]+$/;
            var adCode = document.getElementById('lblAdcode');
            var documentNumber = document.getElementById('txtDocumentNumber');
            if (trimAll(documentNumber.value) == '') {
                try {
                    alert('Enter Document No.');
                    documentNumber.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Document No.');
                    return false;
                }
            }
            var documentDate = document.getElementById('txtDocumentDate');
            if (trimAll(documentDate.value) == '') {
                try {
                    alert('Enter Document Date.');
                    documentDate.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Document Date.');
                    return false;
                }
            }
            var _export = document.getElementById('rbtnExport');
            var _import = document.getElementById('rbtnImport');
            var _inward = document.getElementById('rbtnInward');
            var _outwards = document.getElementById('rbtnOutward');
            var _others = document.getElementById('rbtnOthers');
            if (_export.checked == false && _import.checked == false && _inward.checked == false && _outwards.checked == false && _others.checked == false) {
                alert('Select an Option.');
                return false;
            }
            var purposeCode = document.getElementById('txtPurposecode');
            var selectedValueOfpurposeCode = purposeCode.value;
            if (selectedValueOfpurposeCode == '') {
                try {
                    alert('Select Purpose Code.');
                    purposeCode.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Purpose Code.');
                    return false;
                }
            }
            var currency = document.getElementById('txtCurrency');
            if (currency.value == "" || currency.value == 0) {
                try {
                    alert('Select Currency.');
                    currency.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Currency.');
                    return false;
                }
            }
            //=========Department Wise ================================//
            //            var rdbscotiamocctia = document.getElementById('rdbscotiamoctia');
            //            var rdbtradefinance = document.getElementById('rdbtradefinance');
            //            var rdbtrearusury = document.getElementById('rdbtrearusury');
            //            var rdbcustomerservice = document.getElementById('rdbcustomerservice');
            //            if (rdbscotiamocctia.checked == false && rdbtradefinance.ckecked == false && rdbtrearusury.checked == false && rdbcustomerservice.checked == false) {
            //                alert('Select Any one Department');
            //                return false;
            //            }
            var fcAmount = document.getElementById('txtFCAmount');
            if (trimAll(fcAmount.value) == "" || fcAmount.value == 0) {
                try {
                    alert('Enter Amount in FC.');
                    fcAmount.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Amount in FC.');
                    return false;
                }
            }
            var pCode = document.getElementById('txtPurposecode');

            if (pCode.value == 'P0091' || pCode.value == 'P0092' || pCode.value == 'P0093' || pCode.value == 'P0094' || pCode.value == 'P0095' || pCode.value == 'S0091' || pCode.value == 'S0092' || pCode.value == 'S0093' || pCode.value == 'S0094' || pCode.value == 'S0095') {
            }
            else {
                var exchangeRate = document.getElementById('txtExchangeRate');

                if (trimAll(exchangeRate.value) == '' || parseFloat(exchangeRate.value) == '0.00') {
                    try {
                        alert('Enter Exchange Rate.');
                        exchangeRate.focus();
                        return false;
                    }
                    catch (err) {
                        alert('Enter Exchange Rate.');
                        return false;
                    }
                }
                var inrAmount = document.getElementById('txtINRAmount');

                if (trimAll(inrAmount.value) == '') {
                    try {
                        alert('Enter Amount in INR.');
                        inrAmount.focus();
                        return false;
                    }
                    catch (err) {
                        alert('Enter Amount in INR.');
                        inrAmount.focus();
                        return false;
                    }
                }
            }
            if (document.getElementById('txtPurposecode').value == 'P0102' || document.getElementById('txtPurposecode').value == 'P0107') {
                if (document.getElementById('txtRealisedAmount').value == '') {
                    alert('Enter Realised Amount.');
                    document.getElementById('txtRealisedAmount').focus();
                    return false;
                }
            }
            if (document.getElementById('txtPurposecode').value == 'P0102' || document.getElementById('txtPurposecode').value == 'P0107') {

                var e = document.getElementById('ddlSchcode');
                if (e.options[e.selectedIndex].value == "") {
                    alert('Select Schedule Code.');
                    document.getElementById('ddlSchcode').focus();
                    return false;
                }
            }
            var nostroVostro = document.getElementById('txtNostroVostro');
            if (trimAll(nostroVostro.value) == '') {
                alert('Enter Nostro(N)/Vostro(V).');
                try {
                    nostroVostro.focus();
                    return false;
                }
                catch (e) { return false; }
            }
            if (trimAll(nostroVostro.value) != 'n' && trimAll(nostroVostro.value) != 'v' && trimAll(nostroVostro.value) != 'N' && trimAll(nostroVostro.value) != 'V') {
                alert('Enter Nostro(N)/Vostro(V).');
                try {
                    nostroVostro.focus();
                    return false;
                }
                catch (e) { return false; }
            }
            //-------------------------------------------------Export/ImportCountry----------txtImpExpCountry---------
            //var pCode = document.getElementById('txtPurposecode');
            var ExpImp = document.getElementById('txtImpExpCountry');
            //var portcodeGroup = pCode.value.substring(1, 3);
            var pCode = document.getElementById('txtPurposecode');
            var portcodeGroup = pCode.value.substring(1, 3);



            //            if ((_inward.checked == true) || (_outwards.checked == true)) {
            if ((_inward.checked == true || _outwards.checked == true) && (portcodeGroup == '02' || portcodeGroup == '03' || portcodeGroup == '05' || portcodeGroup == '06' || portcodeGroup == '07' || portcodeGroup == '08' || portcodeGroup == '09' || portcodeGroup == '10' || portcodeGroup == '11' || portcodeGroup == '15' || portcodeGroup == '16' || portcodeGroup == '17'
            //           portcodeGroup == 'P0211' || pCode.value == 'P0214' || pCode.value == 'P0215' || pCode.value == 'P0216' || pCode.value == 'P0217' || pCode.value == 'P0218' || pCode.value == 'P0219' || pCode.value == 'P0220' || pCode.value == 'P0221' || pCode.value == 'P0222' || pCode.value == 'P0223' || pCode.value == 'P0224' || pCode.value == 'P0225' || pCode.value == 'P0226' ||
            //           pCode.value == 'P0301' || pCode.value == 'P0302' || pCode.value == 'P0304' || pCode.value == 'P0305' || pCode.value == 'P0306' || pCode.value == 'P0308' || pCode.value == 'P0501' || pCode.value == 'P0502' || pCode.value == 'P0601' || pCode.value == 'P0602' || pCode.value == 'P0603' || pCode.value == 'P0605' || pCode.value == 'P0607' || pCode.value == 'P0608' ||
            //           pCode.value == 'P0609' || pCode.value == 'P0610' || pCode.value == 'P0611' || pCode.value == 'P0612' || pCode.value == 'P0701' || pCode.value == 'P0702' || pCode.value == 'P0703' || pCode.value == 'P0801' || pCode.value == 'P0802' || pCode.value == 'P0803' || pCode.value == 'P0804' || pCode.value == 'P0805' || pCode.value == 'P0806' || pCode.value == 'P0807' || pCode.value == 'P0808' || pCode.value == 'P0809' ||
            //           pCode.value == 'P0901' || pCode.value == 'P0902' || pCode.value == 'P1002' || pCode.value == 'P1003' || pCode.value == 'P1004' || pCode.value == 'P1005' || pCode.value == 'P1006' || pCode.value == 'P1007' || pCode.value == 'P1008' || pCode.value == 'P1009' || pCode.value == 'P1010' || pCode.value == 'P1011' || pCode.value == 'P1013' || pCode.value == 'P1014' || pCode.value == 'P1015' || pCode.value == 'P1016' ||
            //           pCode.value == 'P1017' || pCode.value == 'P1018' || pCode.value == 'P1019' || pCode.value == 'P1020' || pCode.value == 'P1021' || pCode.value == 'P1022' || pCode.value == 'P1099'
           )) {
                // if (/["02","03","05","06","07","08","09","10","11","15","16","17"]/.test(portcodeGroup) && ExpImp.value == '') 
                if (ExpImp.value == '') {

                    try {

                        alert('Enter Country of ultimate Importer/Exporter.', '#txtImpExpCountry');
                        ExpImp.focus();
                        return false;
                    }
                    catch (err) {
                        alert('Enter Country of ultimate Importer/Exporter.', '#txtImpExpCountry');
                        return false;
                    }
                }
            }
            return true;
            //--------------------------------------------------------------------OTHERS Validations---------------------------------------------------------------


            //------------------------------------------------------------------------------------------------------
            //            var e = document.getElementById('txtCountryBeneficiary1');
            //            if (document.getElementById('txtCountryBeneficiary1').value == '') {
            //                alert('Select Country of Destination.');
            //                document.getElementById('txtCountryBeneficiary1').focus();
            //                return false
            //            }
            var billNumber = document.getElementById('txtBillNumber');
            //            if (billNumber.value == '') {
            //                alert('Enter Bill No.');
            //                try {
            //                    billNumber.focus();
            //                    return false;
            //                }
            //                catch (err) {
            //                    return false;
            //                }
            //            }
            if (_export.checked == true || _import.checked == true) {
                var shippingBillNumber = document.getElementById('txtShippingBillNumber');
                if (shippingBillNumber.value != '') {
                    if (expAlpha.test(trimAll(shippingBillNumber.value)) == false) {
                        alert('Only Alphanumeric value is allowed.');
                        try {
                            shippingBillNumber.focus();
                            return false;
                        }
                        catch (err) {
                            return false;
                        }
                    }
                }
                var shippingBillDate = document.getElementById('txtShippingBillDate');
                if (shippingBillDate.value != '') {

                }

                //                if (_export.checked == true || _import.checked == true) {
                //                    var PortCode = document.getElementById('txtPort');
                //                    if (PortCode.value == '') {
                //                        alert('Enter Port Code');
                //                        PortCode.focus();
                //                        return false;
                //                    }
                //                }

                var formNumber = document.getElementById('txtFormNumber');
                //                if (_export.checked == true) {
                //                    if (formNumber.value == '') {
                //                        alert('Enter GR No.');
                //                        try {
                //                            formNumber.focus();
                //                            return false;
                //                        }
                //                        catch (err) {
                //                            return false;
                //                        }
                //                    }
                //                }
            }
            return true;
        }
        //To make form number to 0000000 when form prefix is ZZ
        function FormnumberGeneration() {
        }
        function toggleSelection() {
            var _export = document.getElementById('rbtnExport');
            var _import = document.getElementById('rbtnImport');
            var _inward = document.getElementById('rbtnInward');
            var _outwards = document.getElementById('rbtnOutward');
            var _others = document.getElementById('rbtnOthers');
            var billNumber = document.getElementById('txtShippingBillNumber');
            var shippingBillDate = document.getElementById('txtShippingBillDate');
            var btncalendar_ShippingBillDate = document.getElementById('btncalendar_ShippingBillDate');
            var customSerialNumber = document.getElementById('txtCustomSerialNumber');
            var formNumber = document.getElementById('txtFormNumber');
            var lcIndication = document.getElementById('chkLCIndication');
            if (_export.checked == true || _import.checked == true) {
                billNumber.disabled = '';
                shippingBillDate.disabled = '';
                btncalendar_ShippingBillDate.disabled = '';
                customSerialNumber.disabled = '';
                formNumber.disabled = '';
                lcIndication.disabled = '';
            }
            else {
                billNumber.disabled = 'disabled';
                shippingBillDate.disabled = 'disabled';
                btncalendar_ShippingBillDate.disabled = 'disabled';
                customSerialNumber.disabled = 'disabled';
                formNumber.disabled = 'disabled';
                lcIndication.disabled = 'disabled';
                billNumber.value = '';
                shippingBillDate.value = '';
                customSerialNumber.value = '';
                formNumber.value = '';
                lcIndication.checked = false;
            }
        }
        function setSaveFocus() {
            var str1 = document.getElementById('txtDocumentDate').value;
            var str2 = document.getElementById('txtShippingBillDate').value;
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10) - 1;
            var yr1 = parseInt(str1.substring(6, 10), 10);
            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10) - 1;
            var yr2 = parseInt(str2.substring(6, 10), 10);
            var frmDate = new Date(yr1, mon1, dt1);
            var shippingDate = new Date(yr2, mon2, dt2);
            if (document.getElementById("txtShippingBillDate").value != '__/__/____') {
                if ((shippingDate <= frmDate)) {
                }
                else {
                    alert('Shipping Date should be less than or equal Document Date.');
                    document.getElementById("txtShippingBillDate").value = '__/__/____';
                    document.getElementById("txtShippingBillDate").focus();
                    return true;
                }
            }
            else {
                var ans = confirm('Shipping Date is Blank . Do you want to proceed.');
                if (!ans) {
                    document.getElementById("txtShippingBillDate").focus();
                    return false;
                }
            }
            if (document.getElementById('rbtnImport').checked == true) {
                document.getElementById('btnSave').focus();
            }
        }
        function exchRate() {
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtcur = document.getElementById('txtCurrency');
            if (txtcur.value == 'INR') {
                txtExchangeRate.value = 1;
            }

            else if (txtExchangeRate.value == '') {
                txtExchangeRate.value = 0;
            }
            txtExchangeRate.value = parseFloat(txtExchangeRate.value).toFixed(4);
        }
        function setShipDateFocus() {
            if (document.getElementById('rbtnImport').checked == true) {
                document.getElementById('txtShippingBillDate').focus();
            }
            else if ((document.getElementById('rbtnInward').checked == true) || (document.getElementById('rbtnOutward').checked == true) || (document.getElementById('rbtnOthers').checked == true)) {
                document.getElementById('btnSave').focus();
            }
            else if (document.getElementById('rbtnExport').checked == true) {
                document.getElementById('txtPort').focus();
            }
        }
        function setACFocus() {
            var i = document.getElementById('txtCountryBeneficiary1');
            var txtCurrency = document.getElementById('txtCurrency');
            var pCode = document.getElementById('txtPurposecode');
            if (pCode.value == 'P0091' || pCode.value == 'P0092' || pCode.value == 'P0093' || pCode.value == 'P0094' || pCode.value == 'P0095' || pCode.value == 'S0091' || pCode.value == 'S0092' || pCode.value == 'S0093' || pCode.value == 'S0094' || pCode.value == 'S0095') {
                document.getElementById('btnSave').focus();
            }
            if (i.value == "") {
                alert('Select Country of Destination.');
                document.getElementById('txtCountryBeneficiary1').focus();
                return false
            }
            else {
                if (((document.getElementById('rbtnImport').checked == true) || (document.getElementById('rbtnExport').checked == true)) && (txtCurrency == 'INR')) {
                    document.getElementById('txtCountryACHolder1').focus();
                }
                else if (document.getElementById('rbtnImport').checked == true) {
                    document.getElementById('txtShippingBillDate').focus();
                }
                else if (document.getElementById('rbtnExport').checked == true) {
                    document.getElementById('txtPort').focus();
                }
                else if (((document.getElementById('rbtnInward').checked == true) || (document.getElementById('rbtnOutward').checked == true) || (document.getElementById('rbtnOthers').checked == true)) && (txtCurrency == 'INR')) {

                    document.getElementById('txtCountryACHolder1').focus();
                }
                else if ((document.getElementById('rbtnInward').checked == true) || (document.getElementById('rbtnOutward').checked == true) || (document.getElementById('rbtnOthers').checked == true)) {

                    document.getElementById('btnSave').focus();
                }
            }
        }
        function calculateINR() {
            var pCode = document.getElementById('txtPurposecode');
            var txtFCAmount = parseFloat(document.getElementById('txtFCAmount')).toFixed(2);
            if (txtFCAmount.value == '')
                txtFCAmount.value = 0;
            txtFCAmount.value = parseFloat(txtFCAmount.value).toFixed(2);
            if (pCode.value == 'P0091' || pCode.value == 'P0092' || pCode.value == 'P0093' || pCode.value == 'P0094' || pCode.value == 'P0095' || pCode.value == 'S0091' || pCode.value == 'S0092' || pCode.value == 'S0093' || pCode.value == 'S0094' || pCode.value == 'S0095') {
                document.getElementById('btnSave').focus();
            }
            else {
                var fcAmount = document.getElementById('txtFCAmount').value;
                var exchangeRate = document.getElementById('txtExchangeRate').value;
                var INRAmount = '';
                //                if (fcAmount == '' || fcAmount == "0") {
                //                    alert('Enter Amount in FC.');
                //                    try {
                //                        document.getElementById('txtFCAmount').focus();
                //                        return false;
                //                    }
                //                    catch (err) {
                //                        return false;
                //                    }
                //                }
                //                else {
                //                    if (exchangeRate == '' || fcAmount == "0") {
                //                        alert('Enter Exchange Rate.');
                //                        try {
                //                            document.getElementById('txtExchangeRate').focus();
                //                            return false;
                //                        }
                //                        catch (err) {
                //                            return false;
                //                        }
                //                    }

                fcAmount = fcAmount.replace(',', '');
                exchangeRate = exchangeRate.replace(',', '');
                if (fcAmount == "")
                    fcAmount = 0;
                if (exchangeRate == "")
                    exchangeRate = 0;
                document.getElementById('txtINRAmount').value = parseFloat(fcAmount * exchangeRate).toFixed(2);
                //document.getElementById('txtINRAmount').value = parseFloat(Math.round(INRAmount)).toFixed(2);
                //document.getElementById('txtINRAmount').focus();
                //                        if (document.getElementById('dropDownListPurposeCode').value == 'P0102' || document.getElementById('dropDownListPurposeCode').value == 'P0107') {
                //                            document.getElementById('txtRealisedAmount').focus();
                //                        }
                //                        else {
                //                            document.getElementById('txtBeneficiaryName').focus();
                //                        }


            }
        }
        function CalculateExchangeRateFromINR() {
            var INRAmount = document.getElementById('txtINRAmount');
            var fcAmount = document.getElementById('txtFCAmount').value;
            if (fcAmount == '' || fcAmount == "0") {
                alert('Enter Amount in FC.');
                try {
                    document.getElementById('txtFCAmount').focus();
                    return false;
                }
                catch (err) {
                    return false;
                }
            }
            document.getElementById('txtExchangeRate').value = (parseFloat(INRAmount.value) / parseFloat(fcAmount));
            // AddCoomaToTextboxes();
            if (document.getElementById('txtPurposecode').value == 'P0102' || document.getElementById('txtPurposecode').value == 'P0107') {
                document.getElementById('txtRealisedAmount').focus();
            }
            else {
                document.getElementById('txtBeneficiaryName').focus();
            }
            return true;
        }
        function calculateINRForFC() {
            var fcAmount = document.getElementById('txtFCAmount').value;
            var exchangeRate = document.getElementById('txtExchangeRate').value;
            var txtCurrency = document.getElementById('txtCurrency');
            if (txtCurrency == 'INR') {
                document.getElementById('txtExchangeRate').value = 1;
                document.getElementById('txtINRAmount').value = fcAmount;
            }
        }
        function setfocus() {
            document.getElementById('txtDocumentNumber').focus();
        }
        function realisedAmt() {
            if (document.getElementById('txtPurposecode').value == 'P0102' || document.getElementById('txtPurposecode').value == 'P0107') {
                if (document.getElementById('txtRealisedAmount').value == '' || document.getElementById('txtRealisedAmount').value == "0") {
                    alert('Enter Realised Amount.');
                    document.getElementById('txtRealisedAmount').focus();
                    return false
                }
                else if (document.getElementById('txtRealisedAmount').value > document.getElementById('txtFCAmount').value) {
                    alert('Realised Amount Can not be Greater than Amount in FC');
                    document.getElementById('txtRealisedAmount').focus();
                    return false
                }
                else if (document.getElementById('txtRealisedAmount').value <= 0) {
                    alert('Realised Amount Can not 0 or Less than 0');
                    document.getElementById('txtRealisedAmount').focus();
                    return false
                }
                else {
                    var txtRealisedAmount = document.getElementById('txtRealisedAmount');
                    if (txtRealisedAmount.value == '')
                        txtRealisedAmount.value = 0;
                    txtRealisedAmount.value = parseFloat(txtRealisedAmount.value).toFixed(2);
                    return true;
                }
            }
            return true;
        }
        function schCode() {
            realisedAmt();
            if (document.getElementById('txtPurposecode').value == 'P0102' || document.getElementById('txtPurposecode').value == 'P0107') {
                var e = document.getElementById('ddlSchcode');
                if (e.value == "") {
                    alert('Select Schedule Code.');
                    document.getElementById('ddlSchcode').focus();
                    return false
                }
            }
        }

    </script>
    <script language="javascript" type="text/javascript">
        //All helps function
        // help for Country of destinatiom.
        function OpenCountryList(hNo) {
            open_popup('../TF_CountryLookUp1.aspx?PageHeader=Country Lookup&hNo=' + hNo, 450, 400, 'CountryList');
            return false;
        }
        function selectCountry(selectedID, hNo) {
            var id = selectedID;
            if (hNo == '1') {
                document.getElementById('hdnCountryHelpNo').value = hNo;
                document.getElementById('hdnCountry').value = id;
                document.getElementById('hdnCountryName').value = hNo;
                document.getElementById('btnCountry').click();
                document.getElementById('txtCountryBeneficiary1').value = id;
                __doPostBack('txtCountryBeneficiary1', '');
            }
            else {
                document.getElementById('txtImpExpCountry').value = id;
                __doPostBack('txtImpExpCountry', '');
            }
        }
        function changeCountryDesc() {
            var txtCountryBeneficiary1 = document.getElementById('txtCountryBeneficiary1');
            var txtCountryBeneficiary = document.getElementById('txtCountryBeneficiary');
            if (txtCountryBeneficiary1 != '')
                txtCountryBeneficiary = txtCountryBeneficiary1;
            else
                txtCountryBeneficiary = '';
            return true;
        }
        //  Help fuctions for Country of A/c Holder
        function OpenCountryListAc(hNo) {
            open_popup('TF_RET_ACCountryLookUp.aspx?hNo=' + hNo, 450, 400, 'CountryList');
            return false;
        }
        function selectCountryAc(selectedID, selectedName, hNo) {
            var id = selectedID;
            var Name = selectedName;
            document.getElementById('hdnCountryHelpNoAc').value = hNo;
            document.getElementById('hdnCountryAc').value = id;
            document.getElementById('hdnCountryAcName').value = selectedName;
            document.getElementById('btnCountryAc').click();
            //__doPostBack('txtCountryACHolder1', '');    
        }
        function changeCountryDescAc() {
            var txtCountryACHolder1 = document.getElementById('txtCountryACHolder1');
            var txtCountryACHolder = document.getElementById('txtCountryACHolder');
            if (txtCountryACHolder1 != '') {
                txtCountryACHolder = txtCountryACHolder1;
            }
            else {
                txtCountryACHolder.innerHTML = '';
            }
            return true;

        }
        // Help for Currency
        function OpenCurrencyList(hNo) {
            open_popup('../TF_CurrencyLookup1.aspx?pc=' + hNo, 450, 400, 'CurrencyList');
            return false;
        }
        function selectCurrency(selectedID, selectedName, hNo) {
            var id = selectedID;
            var name = selectedName;
            document.getElementById('hdnCurrencyHelpNo').value = hNo;
            document.getElementById('hdnCurId').value = id;
            document.getElementById('hdnCurName').value = name;
            document.getElementById('btnCurr').click();
        }
        function changeCurrDesc() {
            var txtCurrency = document.getElementById('txtCurrency');
            var txtCurrencyDescription = document.getElementById('txtCurrencyDescription');
            if (txtCurrency != '') {
                if (txtCurrencyDescription == '')
                    txtCurrencyDescription.innerHTML = txtCurrency.value;
            }
            else
                txtCurrencyDescription.innerHTML = '';
            var txtNostroVostro = document.getElementById('txtNostroVostro');
            // var strUser = dropDownListCurrency.options[dropDownListCurrency.selectedIndex].innerText;
            var strUser = txtCurrency.value;
            var lblnostrovostro = document.getElementById('lblnostrovostro').innerHTML;
            if (strUser == "ACD" || strUser == "INR") {
                txtNostroVostro.value = "V";
                document.getElementById('lblnostrovostro').innerHTML = "Vostro";
            }
            else {
                txtNostroVostro.value = "N";
                document.getElementById('lblnostrovostro').innerHTML = "Nostro";
            }
            // dropDownListCurrency.focus();
            return true;
        }
        // Help for portcode
        function PortHelp() {
            open_popup('RET_HelpPortCode.aspx', 450, 400, "DocFile");
            return true;
        }
        function selectPort(Uname, Udesc) {
            // document.getElementById('txtPort').value = Uname;
            document.getElementById('hdnPortCode').value = Uname;
            document.getElementById('hdnPortDesc').value = Udesc;
            document.getElementById('txtPort').focus();
            document.getElementById('btnPortCode').click();
            document.getElementById('txtPort').value = Uname;
            __doPostBack('txtPort', '');
        }
        function changePortCodeDesc() {
            var txtPort = document.getElementById('hdnPortDesc');
            var txtPortCode = document.getElementById('txtPortCode');
            if (txtPort.value != '')
                txtPortCode.innerHTML = txtPort.value;
            else
                txtPortCode.innerHTML = '';
            if (txtPort.value == '')
                txtPortCode.innerHTML = '';
            return true;
        }
        //function for fill discription after post bak
        function toogleDisplayHelp() {
            changeCountryDesc();
            changeCountryDescAc();
            changeCurrDesc();
            //changePortCodeDesc();
            // changeBankCodeDesc();
            return true;
        }
        // help function for Porpose Code List
        function OpenPCList() {
            var selectedemp = document.getElementById('txtPurposecode');
            //var selectedValueOfselectedemp = selectedemp.options[selectedemp].value;
            var type = '';
            var _export = document.getElementById('rbtnExport');
            var _import = document.getElementById('rbtnImport');
            var _inward = document.getElementById('rbtnInward');
            var _outward = document.getElementById('rbtnOutward');
            var _others = document.getElementById('rbtnOthers');
            if (_export.checked == true) {
                type = 'EXP';
            }
            else if (_import.checked == true) {
                type = 'IMP';
            }
            else if (_inward.checked == true) {
                type = 'INW';
            }
            else if (_outward.checked == true) {
                type = 'OTW';
            }
            else if (_others.checked == true) {
                type = 'OTH';
            }
            open_popup('RET_helpPurposeCode.aspx?pc_id=' + selectedemp + '&type=' + type, 600, 500, 'win1');
            return false;
        }
        function selectPC(selectedID) {
            var id = selectedID;
            document.getElementById('hdnpcId').value = id;
            document.getElementById('btnPc').click();
        }
        // help Bank Code List 
        function OpenBankCodeList(hNo) {
            var bankcode = document.getElementById('txtCountryACHolder1').value;
            var strUser = document.getElementById('txtBankCode');
            open_popup('RET_helpBankCode.aspx?hNo=' + hNo + '&bankcode=' + bankcode, 450, 400, 'CountryList');
            return false;
        }
        function selectBankCode(selectedID, selectedName, hNo) {
            var id = selectedID;
            var Name = selectedName;
            document.getElementById('hdnBankCodeHelpNo').value = hNo;
            document.getElementById('hdnBankCode').value = id;
            document.getElementById('hdnBankName').value = Name;
            document.getElementById('btnBankCode1').click();
        }
        function changeBankCodeDesc() {
            var txtBankCode = document.getElementById('txtBankCode');
            var lblBankCode = document.getElementById('lblBankCode');
            if (txtBankCode != '')
                lblBankCode.innerHTML = txtBankCode.value;
            else
                lblBankCode.innerHTML = '';
            return true;
        }
        function Confirm() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are You Sure country of detination as a Country of Importer/Expoter")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
           
            return false;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 25%;
        }
        .style5
        {
            width: 4%;
        }
        .style6
        {
            width: 5%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <center>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
            </center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td>
                                <input type="hidden" id="hdnpcId" runat="server" />
                                <asp:Button ID="btnPc" Style="display: none;" runat="server" OnClick="btnPc_Click" />
                                <input type="hidden" id="hdnCntholder" runat="server" />
                                <input type="hidden" id="hdnCntDest" runat="server" />
                                <input type="hidden" id="hdnPortCode" runat="server" />
                                <input type="hidden" id="hdnPortDesc" runat="server" />
                                <asp:Button ID="btnPortCode" Style="display: none;" runat="server" OnClick="btnPortCode_Click" />
                                <input type="hidden" id="hdnCountryHelpNo" runat="server" />
                                <input type="hidden" id="hdnCountry" runat="server" />
                                <input type="hidden" id="hdnCountryName" runat="server" />
                                <asp:Button ID="btnCountry" Style="display: none;" runat="server" OnClick="btnCountry_Click" />
                                <input type="hidden" id="hdnCountryHelpNoAc" runat="server" />
                                <input type="hidden" id="hdnCountryAc" runat="server" />
                                <input type="hidden" id="hdnCountryAcName" runat="server" />
                                <asp:Button ID="btnCountryAc" Style="display: none;" runat="server" OnClick="btnCountryAc_Click" />
                                <input type="hidden" id="hdnCurrencyHelpNo" runat="server" />
                                <input type="hidden" id="hdnCurId" runat="server" />
                                <input type="hidden" id="hdnCurName" runat="server" />
                                <asp:Button ID="btnCurr" Style="display: none;" runat="server" OnClick="btnCurr_Click" />
                                <input type="hidden" id="hdnBankCodeHelpNo" runat="server" />
                                <input type="hidden" id="hdnBankCode" runat="server" />
                                <input type="hidden" id="hdnBankName" runat="server" />
                                <asp:Button ID="btnBankCode1" Style="display: none;" runat="server" OnClick="btnBankCode_Click" />
                                <input type="hidden" id="hdnCountryofDestination" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold;">R-Return Data Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <%-- Audit Trail--%>
                                <input type="hidden" id="hdnSerialno" runat="server" />
                                <input type="hidden" id="hdnDocumentNo" runat="server" />
                                <input type="hidden" id="hdnDocumentDate" runat="server" />
                                <input type="hidden" id="hdnPCode" runat="server" />
                                <input type="hidden" id="hdnCur" runat="server" />
                                <input type="hidden" id="hdnAmtFC" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Branch:</span>
                                        </td>
                                        <td align="left" nowrap="nowrap" valign="middle" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtBranchName" CssClass="textBox" runat="server" MaxLength="10"
                                                Width="100px" TabIndex="1" Enabled="false"></asp:TextBox>&nbsp;&nbsp; <span class="elementLabel">
                                                    AD Code :</span>&nbsp;<asp:Label ID="lblAdcode" CssClass="elementLabel" runat="server"
                                                        Width="50px"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblBankname" runat="server" Style="font-size: small" CssClass="elementLabel"
                                                Width="100px"></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="mandatoryField">* </span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" rowspan="0" class="style5">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" Enabled="false"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp; &nbsp; <span class="mandatoryField" style="text-align: right">* </span><span
                                                class="elementLabel">To Date :</span> &nbsp;<asp:TextBox ID="txtToDate" runat="server"
                                                    CssClass="textBox" MaxLength="10" Width="70" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" nowrap="nowrap" class="style6">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Serial No. :</span>
                                        </td>
                                        <td align="left" class="style5">
                                            &nbsp;<asp:TextBox ID="txtSerialNumber" runat="server" CssClass="textBox" MaxLength="5"
                                                Enabled="false" Width="70"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Document No. :</span>
                                        </td>
                                        <td align="left" class="style5">
                                            &nbsp;<asp:TextBox ID="txtDocumentNumber" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="200" TabIndex="4"></asp:TextBox>
                                        </td>
                                        <td align="left" nowrap="nowrap" class="style1">
                                            <span class="mandatoryField">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; * </span><span class="elementLabel">
                                                Document Date :</span> &nbsp;<asp:TextBox ID="txtDocumentDate" runat="server" CssClass="textBox"
                                                    MaxLength="10" ValidationGroup="dtVal" Width="70" TabIndex="5"></asp:TextBox>
                                            <asp:Button ID="btncalendar_DocumentDate" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtDocumentDate" InputDirection="RightToLeft"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left"
                                                PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarDocumentDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDocumentDate" PopupButtonID="btncalendar_DocumentDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ValidationGroup="dtVal" ControlToValidate="txtDocumentDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style6">
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:RadioButton ID="rbtnExport" runat="server" CssClass="elementLabel" GroupName="Data"
                                                AutoPostBack="true" TabIndex="6" Text="Export" OnCheckedChanged="rbtnExport_CheckedChanged" />&nbsp;
                                            <asp:RadioButton ID="rbtnImport" runat="server" CssClass="elementLabel" GroupName="Data"
                                                AutoPostBack="true" TabIndex="6" Text="Import" OnCheckedChanged="rbtnImport_CheckedChanged" />&nbsp;
                                            <asp:RadioButton ID="rbtnInward" runat="server" CssClass="elementLabel" GroupName="Data"
                                                AutoPostBack="true" TabIndex="6" Text="Inward" OnCheckedChanged="rbtnInward_CheckedChanged" />&nbsp;
                                            <asp:RadioButton ID="rbtnOutward" runat="server" CssClass="elementLabel" GroupName="Data"
                                                AutoPostBack="true" TabIndex="6" Text="Outward" OnCheckedChanged="rbtnOutward_CheckedChanged" />&nbsp;
                                            <asp:RadioButton ID="rbtnOthers" runat="server" CssClass="elementLabel" GroupName="Data"
                                                AutoPostBack="true" TabIndex="6" Text="Others" OnCheckedChanged="rbtnOthers_CheckedChanged"
                                                Checked="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Purpose Code :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap" class="style5">
                                            &nbsp;
                                            <asp:TextBox ID="txtPurposecode" CssClass="textBox" TabIndex="7" AutoPostBack="true"
                                                runat="server" OnTextChanged="txtPurposecode_TextChanged" MaxLength="5" Width="40px"></asp:TextBox>
                                            <asp:Button ID="btnPCList" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="txtPurposeCodeDesc" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 5%;" nowrap="nowrap">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="mandatoryField">
                                                * </span>&nbsp;<span class="elementLabel">Currency :</span> &nbsp;
                                            <asp:TextBox ID="txtCurrency" CssClass="textBox" AutoPostBack="true" runat="server"
                                                MaxLength="3" Width="30" OnTextChanged="txtCurrency_TextChanged" TabIndex="8"></asp:TextBox>
                                            <asp:Button ID="btncurrList" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="txtCurrencyDescription" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Amount in FC :</span>
                                        </td>
                                        <td align="left" class="style5">
                                            &nbsp;<asp:TextBox ID="txtFCAmount" runat="server" CssClass="textBoxRight" MaxLength="15"
                                                TabIndex="9" Width="100"></asp:TextBox>
                                            <span class="mandatoryField">&nbsp;&nbsp; * </span><span class="elementLabel">Exchange
                                                Rate :</span>
                                            <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBoxRight" MaxLength="15"
                                                TabIndex="10" Width="60"></asp:TextBox>&nbsp;&nbsp;
                                            <%--</td>
                                            <td align="right" style="width: 5%;" nowrap>--%>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            <span class="mandatoryField">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span><span
                                                class="elementLabel">Amount in INR :</span>
                                            <%-- </td>
                                            <td align="left">--%>
                                            &nbsp;<asp:TextBox ID="txtINRAmount" runat="server" CssClass="textBoxRight" MaxLength="15"
                                                Width="100" onfocus="this.select()"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <%-- <span class="mandatoryField">*</span>--%>
                                            <span class="elementLabel">Realised Amount :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap" class="style5">
                                            &nbsp;<asp:TextBox ID="txtRealisedAmount" runat="server" CssClass="textBoxRight"
                                                TabIndex="11" MaxLength="20" Width="100"></asp:TextBox>
                                            &nbsp; <span class="mandatoryField">* </span><span class="elementLabel">Schedule Code
                                                :</span>
                                            <asp:DropDownList ID="ddlSchcode" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                                TabIndex="12" OnSelectedIndexChanged="ddlSchcode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 5%;" nowrap="nowrap">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Nostro(N)/Vostro(V)
                                                :</span> &nbsp;<asp:TextBox ID="txtNostroVostro" runat="server" CssClass="textBox"
                                                    MaxLength="1" Width="10" AutoPostBack="true" OnTextChanged="txtNostroVostro_TextChanged1"
                                                    TabIndex="13"></asp:TextBox>
                                            <asp:Label ID="lblnostrovostro" runat="server" CssClass="elementLabel" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="elementLabel">Beneficiary Name :</span>
                                        </td>
                                        <td align="left" class="style5">
                                            &nbsp;<asp:TextBox ID="txtBeneficiaryName" runat="server" CssClass="textBox" MaxLength="40"
                                                TabIndex="14" Width="250"></asp:TextBox>
                                        </td>
                                        <td align="left" style="width: 5%;" nowrap="nowrap">
                                            <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Remitter
                                                Name :</span> &nbsp;<asp:TextBox ID="txtRemitterName" runat="server" CssClass="textBox"
                                                    MaxLength="40" TabIndex="15" Width="250"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="elementLabel">Beneficiary Address :</span>
                                        </td>
                                        <td align="left" class="style5">
                                            &nbsp;<asp:TextBox ID="txtBeneficiaryAdd" runat="server" CssClass="textBox" MaxLength="225"
                                                TabIndex="16" Width="280" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td align="left" style="width: 5%;" nowrap="nowrap">
                                            <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Remitter Address
                                                :</span> &nbsp;<asp:TextBox ID="txtRemitterAdd" runat="server" CssClass="textBox"
                                                    MaxLength="225" TabIndex="17" Width="280" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="elementLabel">Country of Destination :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap" class="style5">
                                            &nbsp;
                                            <asp:TextBox ID="txtCountryBeneficiary1" runat="server" AutoPostBack="true" CssClass="textBox"
                                                MaxLength="2" Width="20px" OnTextChanged="txtCountryBeneficiary1_TextChanged"
                                                TabIndex="18"></asp:TextBox>
                                            <asp:Button ID="btnCountryBeneficiary" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="txtCountryBeneficiary" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 5%;" nowrap="nowrap">
                                            <span class="elementLabel">Country of A/C Holder:</span> &nbsp;
                                            <asp:TextBox ID="txtCountryACHolder1" runat="server" MaxLength="5" CssClass="textBox"
                                                OnTextChanged="txtCountryACHolder1_TextChanged" AutoPostBack="true" Width="20px"
                                                TabIndex="19"></asp:TextBox>
                                            <asp:Button ID="btnCountryACHolder" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="txtCountryACHolder" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="elementLabel">Bank Code :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap" colspan="2">
                                            &nbsp;
                                            <asp:TextBox ID="txtBankCode" CssClass="textBox" runat="server" AutoPostBack="true"
                                                Width="20px" OnTextChanged="txtBankCode_TextChanged" TabIndex="20"></asp:TextBox>
                                            <asp:Button ID="BtnBankCode" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="lblBankCode" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 5%;" nowrap="nowrap">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Country Of Importer/Exporter
                                                :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtImpExpCountry" runat="server" CssClass="textBox" MaxLength="2"
                                                TabIndex="27" Width="20" OnTextChanged="txtImpExpCountry_TextChange" AutoPostBack="true"
                                                Style="text-transform: uppercase"></asp:TextBox>
                                            <asp:Button ID="btnImpExpCountry" runat="server" CssClass="btnHelp_enabled" TabIndex="25" />
                                            <asp:Label ID="lblImpExpCountry" runat="server" CssClass="elementLabel" Width="180px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server" visible="false">
                                        <td align="right" nowrap="nowrap" class="style6">
                                            <span class="elementLabel">Department :</span>
                                        </td>
                                        <td colspan="2">
                                            <asp:RadioButton ID="rdbscotiamoctia" runat="server" CssClass="elementLabel" Text="Scotia Mocatta"
                                                GroupName="return" />
                                            <asp:RadioButton ID="rdbtradefinance" runat="server" CssClass="elementLabel" Text="Trade Finance"
                                                GroupName="return" Checked="true" />
                                            <asp:RadioButton ID="rdbtrearusury" runat="server" CssClass="elementLabel" Text="Treasury"
                                                GroupName="return" />
                                            <asp:RadioButton ID="rdbcustomerservice" runat="server" CssClass="elementLabel" Text="Customer Service"
                                                GroupName="return" />
                                            <asp:RadioButton ID="rdbadmin" runat="server" CssClass="elementLabel" Text="Admin"
                                                GroupName="return" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <span class="elementLabel" style="text-align: center; color: Red;"><b>EXPORT / IMPORT
                                                DETAILS<b> </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <fieldset style="padding: 1px; border: 1px solid gray; width: 62.5%;">
                                                <table cellpadding="0" cellspacing="0" width="100%" bgcolor="#FFCC99">
                                                    <tr>
                                                        <td align="right" style="width: 5%;" nowrap="nowrap">
                                                            <span class="elementLabel">Port Code :</span>
                                                        </td>
                                                        <td align="left" nowrap="nowrap" style="width: 25%;">
                                                            &nbsp;
                                                            <asp:TextBox ID="txtPort" Width="50PX" CssClass="textBox" runat="server" AutoPostBack="true"
                                                                OnTextChanged="txtPort_TextChanged" TabIndex="21"></asp:TextBox>
                                                            <asp:Button ID="btnPortCodeList" runat="server" CssClass="btnHelp_enabled" />
                                                            <asp:Label ID="txtPortCode" runat="server" CssClass="elementLabel" Width="180px"></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 5%;" nowrap="nowrap">
                                                            <span class="elementLabel">I/E Code :</span>
                                                        </td>
                                                        <td align="left" nowrap="nowrap">
                                                            &nbsp;<asp:TextBox ID="txtIECode" runat="server" CssClass="textBox" MaxLength="10"
                                                                TabIndex="22" Width="80"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 5%;" nowrap="nowrap">
                                                            <span class="elementLabel">GR No. :</span>
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;<asp:TextBox ID="txtFormNumber" runat="server" CssClass="textBox" TabIndex="23"
                                                                MaxLength="8" Width="60"></asp:TextBox>
                                                            &nbsp;<span class="elementLabel">[GR/PP/ZZ]</span> &nbsp;
                                                        </td>
                                                        <td align="right" style="width: 5%;" nowrap="nowrap">
                                                            <span class="elementLabel">Bill Prifix :</span>
                                                        </td>
                                                        <td align="left" nowrap="nowrap">
                                                            &nbsp;<asp:TextBox ID="txtBillNumber" runat="server" CssClass="textBox" MaxLength="7"
                                                                TabIndex="24" Width="20"></asp:TextBox>
                                                            &nbsp;<span class="elementLabel">[N/D/P/C/M/A]</span> &nbsp;&nbsp; <span class="elementLabel">
                                                                LC Backed :</span> &nbsp;
                                                            <asp:CheckBox ID="chkLCIndication" runat="server" CssClass="elementLabel" Text="No"
                                                                AutoPostBack="true" TabIndex="25" OnCheckedChanged="chkLCIndication_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top" style="width: 5%; height: 30px" nowrap="nowrap">
                                                            <span class="elementLabel">Custom Serial No. :</span>
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;<asp:TextBox ID="txtCustomSerialNumber" runat="server" CssClass="textBox" MaxLength="8"
                                                                TabIndex="26" Width="80"></asp:TextBox>
                                                            &nbsp; <span class="elementLabel">[YY + SR NO.]</span>
                                                        </td>
                                                        <td align="right" style="width: 5%;" nowrap="nowrap">
                                                            <span class="elementLabel">Shipping Bill No. :</span>
                                                        </td>
                                                        <td align="left" nowrap="nowrap">
                                                            &nbsp;<asp:TextBox ID="txtShippingBillNumber" runat="server" CssClass="textBox" MaxLength="7"
                                                                TabIndex="27" Width="50"></asp:TextBox>
                                                            &nbsp; &nbsp;<span class="elementLabel">Shipping Bill Date :</span> &nbsp;<asp:TextBox
                                                                ID="txtShippingBillDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                TabIndex="28" Width="70"></asp:TextBox>
                                                            <asp:Button ID="btncalendar_ShippingBillDate" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtShippingBillDate" InputDirection="RightToLeft"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left"
                                                                PromptCharacter="_">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <ajaxToolkit:CalendarExtender ID="calendarShippingBillDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtShippingBillDate" PopupButtonID="btncalendar_ShippingBillDate"
                                                                PopupPosition="TopRight">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                                ValidationGroup="dtVal" ControlToValidate="txtShippingBillDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                                            </ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style6">
                                        </td>
                                        <td align="left" style="padding-top: 10px; padding-bottom: 10px" class="style5">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="29" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="30" />
                                            <input type="hidden" runat="server" id="hdnFromDate" />
                                            <input type="hidden" runat="server" id="hdnToDate" /><asp:TextBox ID="txtScheduleNumber"
                                                runat="server" CssClass="textBox" MaxLength="1" Visible="false" TabIndex="31"
                                                Width="10"></asp:TextBox>
                                            <%-- These section to handle log  --%>
                                            <input type="hidden" runat="server" id="hdnType" onfocusout="return hdnType_onfocusout()" />
                                            <input type="hidden" runat="server" id="hdnPurposeCode" />
                                            <input type="hidden" runat="server" id="hdnCurrency" />
                                            <input type="hidden" runat="server" id="hdnFCAmt" />
                                            <input type="hidden" runat="server" id="hdnINRAmt" />
                                            <input type="hidden" runat="server" id="hdnExtRate" />
                                            <input type="hidden" runat="server" id="hdnRealisedAmt" />
                                            <input type="hidden" runat="server" id="hdnDstCode" />
                                            <%-- End of These section to handle log  --%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            toogleDisplayHelp();
            var txtFCAmount = document.getElementById('txtFCAmount');
            if (txtFCAmount.value == '')
                txtFCAmount.value = 0;
            txtFCAmount.value = parseFloat(txtFCAmount.value).toFixed(2);
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtcur = document.getElementById('txtCurrency');
            if (txtExchangeRate.value == '0' && txtcur.value == 'INR') {
                txtExchangeRate.value = 1;
            }

            else if (txtExchangeRate.value == '') {
                txtExchangeRate.value = 0;
            }
            txtExchangeRate.value = parseFloat(txtExchangeRate.value).toFixed(4);
            var txtINRAmount = document.getElementById('txtINRAmount');
            if (txtINRAmount.value == '') {
                document.getElementById('txtINRAmount').value = 0;
            }
            txtINRAmount.value = parseFloat(txtINRAmount.value).toFixed(2);
            var txtRealisedAmount = document.getElementById('txtRealisedAmount');
            if (txtRealisedAmount.value == '')
                txtRealisedAmount.value = 0;
            txtRealisedAmount.value = parseFloat(txtRealisedAmount.value).toFixed(2);
        }
    </script>
</body>
</html>
