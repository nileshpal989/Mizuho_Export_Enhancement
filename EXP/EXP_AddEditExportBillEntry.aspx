<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditExportBillEntry.aspx.cs"
    Inherits="EXP_EXP_AddEditExportBillEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <%--<meta http-equiv="X-UA-Compatible" content="IE=8" />--%>
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        /* TextBox Style of HardCoded Values */
        .textBoxHardCodedValue {
            font-family: Verdana, Sans-Serif, Arial;
            font-weight: normal;
            font-size: 8pt;
            border: 1px solid #5970B2;
            height: 17px;
            background-color: Aqua;
        }
        /* Label Style of Instructions */
        .elementLabelRed {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            font-family: Verdana, Sans-Serif, Arial;
            color: Red;
            background-color: Transparent;
            font-weight: bold;
            font-size: 8pt;
            margin-top: 0px;
        }

        .lblHelp_TT {
            background-image: url(../Images/TTRname.gif);
            background-repeat: no-repeat;
            background-position: center center;
            width: 25px;
            height: 15px;
            font-family: Verdana;
            font-size: 11px;
            font-weight: bolder;
            text-align: center;
            cursor: hand;
            border: 0;
            vertical-align: middle;
            background-color: Transparent;
        }

        .image-button {
            background-image: url(../Images/Close.png); /* Replace with the URL of your image */
            background-repeat: no-repeat;
            background-position: center center;
            text-align: center; /* Center the text horizontally */
            padding: 10px 20px; /* Adjust padding as needed */
            font-size: 11px; /* Adjust font size as needed */
            border: none; /* Remove the default button border */
            cursor: pointer; /* Add a pointer cursor on hover */
        }

            /* Style to remove the default button text */
            .image-button span {
                display: none;
            }
    </style>
    <script language="javascript" type="text/javascript">

        function OnDocNextClick(index) {
            var tabContainer = $get('TabContainerMain');
            tabContainer.control.set_activeTabIndex(index);
            return false;
        }
        function shipbillnohelp(e) {

            var keycode;
            if (window.event) keycode = window.event.keyCode;

            if (keycode == 113 || e == 'mouseClick') {
                //var custid = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
                var shipbillno = $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillNo");
                var custid = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");

                //var shipbilldate = $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillDate");
                //  var portcode = $get("TabContainerMain_tbCoveringScheduleDetails_ddlPortCode");

                open_popup('TF_shipbillnohelp.aspx?shipbillno=' + shipbillno.value + '&custacno=' + custid.value, 450, 550, 'Shipbillno,status=no, resizable=no, scrollbars=yes, toolbar=no');
                //open_popup('../HelpForms/TF_EXP_ShipBillNoHelp.aspx?shipbillno=' + shipbillno.value + '&custacno=' + custid.value, 450, 550, 'Shipbillno,status=no, resizable=no, scrollbars=yes, toolbar=no'); return false;

            }
        }

        function selectshipbillno(shipbillno, shipbilldate, portcode) {

            $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillNo").value = shipbillno;
            $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillDate").value = shipbilldate;
            var hdnPortCode = document.getElementById('hdnPortCode');
            hdnPortCode.value = portcode;
            document.getElementById('btnfillDetails').click();
            // $get("TabContainerMain_tbCoveringScheduleDetails_ddlPortCode").options[$get("TabContainerMain_tbCoveringScheduleDetails_ddlPortCode").selectedIndex].value = portcode;

        }

        function SelectShipBillNoWithInvoice(shipbillno, shipbilldate, portcode, invsrno, invno, invdate) {

            $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillNo").value = shipbillno;
            $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillDate").value = shipbilldate;
            var hdnPortCode = document.getElementById('hdnPortCode');
            hdnPortCode.value = portcode;



            document.getElementById('btnfillDetails').click();
            $get("TabContainerMain_tbCoveringScheduleDetails_txt_invsrno").value = invsrno;
            //            // txt_invsrno_TextChanged(null, null);
            //            __doPostBack("txt_invsrno", "TextChanged");

            // $get("TabContainerMain_tbCoveringScheduleDetails_ddlPortCode").options[$get("TabContainerMain_tbCoveringScheduleDetails_ddlPortCode").selectedIndex].value = portcode;

        }

        function openinvsrno() {

            var custid = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
            var shipbillno = $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillNo");
            var shipbilldate = $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillDate");
            var portcode = $get("TabContainerMain_tbCoveringScheduleDetails_ddlPortCode");

            open_popup('../TF_Invoicesrnohelp.aspx?shipbillno=' + shipbillno.value + '&shipbilldate=' + shipbilldate.value + '&portcode=' + portcode.value + '&custid=' + custid.value, 450, 550, 'Invoicesrno');
            return false;
        }

        function selectinvsrno(invsrno) {

            var invoicesrno = invsrno;
            $get("TabContainerMain_tbCoveringScheduleDetails_txt_invsrno").value = invoicesrno;
            // txt_invsrno_TextChanged(null, null);
            __doPostBack("txt_invsrno", "TextChanged");
        }


        var doc = document;

        function ChangeManualGRText() {

            var chkManualGR = $get("TabContainerMain_tbCoveringScheduleDetails_chkManualGR");
            var lblManualGR = $get("TabContainerMain_tbCoveringScheduleDetails_lblManualGR");

            if (chkManualGR.checked == true) {

                lblManualGR.innerText = "Yes";

            }

            else {

                lblManualGR.innerHTML = "No";
            }

        }

        function ChangeSBText() {

            var chkSB = $get("TabContainerMain_tbCoveringScheduleDetails_chkSB");
            var lblSB = $get("TabContainerMain_tbCoveringScheduleDetails_lblSB");

            if (chkSB.checked == true) {

                lblSB.innerText = "Yes";

            }

            else {

                lblSB.innerHTML = "No";
            }

        }

        function OpenTTNoList(hNo) {

            var txtCustAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
            var branchCode = document.getElementById('hdnbranch');
            if (txtCustAcNo.value == "") {

                alert('Enter Customer A/c No.');
                txtCustAcNo.focus();
                return false;
            }
            open_popup('EXP_TTRefNo.aspx?custAcNo=' + txtCustAcNo.value + '&hNo=' + hNo + '&bcode=' + branchCode.value, 350, 500, 'TTRefNo');
            return false;

        }



        function fbkcal() {

            var txtfbkcharges = $get("TabContainerMain_tbTransactionDetails_txt_fbkcharges");


            // var txtfbkchargesRS = $get("TabContainerMain_tbTransactionDetails_txt_fbkchargesinRS");


            if (txtfbkcharges.value == '') {

                txtfbkcharges.value = 0;
                txtfbkcharges.value = parseFloat(txtfbkcharges.value).toFixed(2);
                document.getElementById('txt_fbkcharges').value = txtfbkcharges.value;
            }

            else {


                var exchrate = $get("TabContainerMain_tbTransactionDetails_txtExchRate");

                var fbkinrs = $get("TabContainerMain_tbTransactionDetails_txt_fbkchargesinRS");

                var fbkchrg = parseFloat(txtfbkcharges.value).toFixed(2);
                var fbkinr = parseFloat(fbkinrs.value).toFixed(2);

                fbkinrs.value = parseFloat(parseFloat(txtfbkcharges.value) * (exchrate.value)).toFixed(2);

            }
        }

        function saveTTRefDetails(TTRefNo, TTAmt, hNo, TTTotAmt, TTCurr) {
            debugger;
            switch (hNo) {
                case "1":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo1").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt1").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt1").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount1").value = TTAmt;
                    // $get("TabContainerMain_tbTransactionDetails_txtTTCurr1").value = TTCurr;// comment by Anand 16-08-2023
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency1").value = TTCurr; // Added by Anand 16-08-2023
                    break;
                case "2":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo2").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt2").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt2").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount2").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency2").value = TTCurr;
                    break;
                case "3":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo3").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt3").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt3").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount3").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency3").value = TTCurr;
                    break;
                case "4":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo4").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt4").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt4").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount4").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency4").value = TTCurr;
                    break;
                case "5":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo5").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTAmt5").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt5").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount5").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency5").value = TTCurr;
                    break;
                case "6":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo6").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt6").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt6").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount6").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency6").value = TTCurr;
                    break;

                case "7":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo7").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt7").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt7").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount7").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency7").value = TTCurr;
                    break;

                case "8":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo8").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt8").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTAmt8").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount8").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency8").value = TTCurr;
                    break;

                case "9":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo9").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTtTTAmt9").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt9").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount9").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency9").value = TTCurr;
                    break;

                case "10":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo10").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt10").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt10").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount10").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency10").value = TTCurr;
                    break;

                case "11":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo11").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt11").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt11").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount11").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency11").value = TTCurr;
                    break;

                case "12":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo12").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt12").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt12").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount12").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency12").value = TTCurr;
                    break;

                case "13":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo13").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt13").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt13").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount13").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency13").value = TTCurr;
                    break;

                case "14":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo14").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt14").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt14").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount14").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency14").value = TTCurr;
                    break;

                case "15":
                    $get("TabContainerMain_tbTransactionDetails_txtTTRefNo15").value = TTRefNo;
                    $get("TabContainerMain_tbTransactionDetails_txtTotTTAmt15").value = TTTotAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtBalTTAmt15").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_txtTTAmount15").value = TTAmt;
                    $get("TabContainerMain_tbTransactionDetails_ddlTTCurrency15").value = TTCurr;
                    break;


            }

            document.getElementById('btnSaveTTDetails').click();
        }


        function CalculateNoOfDays(fDate, tDate) {

            var _day = fDate.split("/")[0];
            var _month = fDate.split("/")[1];
            var _year = fDate.split("/")[2];

            var _day1 = tDate.split("/")[0];
            var _month1 = tDate.split("/")[1];
            var _year1 = tDate.split("/")[2];

            var dt1 = new Date(_year, _month - 1, _day);
            var dt2 = new Date(_year1, _month1 - 1, _day1);

            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

            var diffDays = Math.round(Math.abs((dt1.getTime() - dt2.getTime()) / (oneDay)));

            if (isNaN(diffDays) == true)
                diffDays = 0;

            return diffDays;

        }

        function GetNoOfDays(fDate, tDate, txtNoOfDays) {

            var _day = fDate.value.split("/")[0];
            var _month = fDate.value.split("/")[1];
            var _year = fDate.value.split("/")[2];

            var _day1 = tDate.value.split("/")[0];
            var _month1 = tDate.value.split("/")[1];
            var _year1 = tDate.value.split("/")[2];

            var dt1 = new Date(_year, _month - 1, _day);
            var dt2 = new Date(_year1, _month1 - 1, _day1);

            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

            var diffDays = Math.round(Math.abs((dt1.getTime() - dt2.getTime()) / (oneDay)));

            if (isNaN(diffDays) == false)
                txtNoOfDays.value = diffDays;
            else
                txtNoOfDays.value;
        }



        function generateSDFcustomsNo() {
            var ddlFormType = $get("TabContainerMain_tbCoveringScheduleDetails_ddlFormType");
            var txtGRPPCustomsNo = $get("TabContainerMain_tbCoveringScheduleDetails_txtGRPPCustomsNo");
            if (ddlFormType.value == "SDF")
                txtGRPPCustomsNo.value = "00000";
            return true;
        }

        function changeTextColor() {
            var chkRBI = $get("TabContainerMain_tbTransactionDetails_chkRBI");
            var v = $get("TabContainerMain_tbTransactionDetails_txtCurrentAcinRS");
            if (chkRBI.checked == true)
                v.style.backgroundColor = "#FF4A4F";
            else
                v.style.backgroundColor = "white";
        }

        function chk1() {
            var lcNo = $get("TabContainerMain_tbDocumentDetails_txtLCNo");
            var chk1 = $get("TabContainerMain_tbCoveringScheduleDetails_chk1");
            if (lcNo.value != "")
                chk1.checked = true;
            return true;
        }

        function chkBankLineDt(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            var chkBankLineTransfer = $get("TabContainerMain_tbDocumentDetails_chkBankLineTransfer");

            //alert(charCode);
            if (chkBankLineTransfer.checked != true) {
                if (charCode != 9)
                    return false;
                else
                    return true;
            }
        }

        function AlertGRcurrency() {
            var curr = $get("TabContainerMain_tbTransactionDetails_ddlCurrency");
            var currValue = curr.options[curr.selectedIndex].value;
            var currGR = $get("TabContainerMain_tbCoveringScheduleDetails_ddlCurrencyGRPP");
            var currGRValue = currGR.options[currGR.selectedIndex].value;

            if (currGRValue != currValue) {
                alert("Invalid Currency");
            }
            return true;
        }

        function validate_Number(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validate_AcNo(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //   alert(charCode);
            if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39)
                return false;
            else
                return true;
        }

        function OpenCommodityList() {
            open_popup('../XOS/Help_GRCustoms_Commodity.aspx', 400, 400, "DocFile");
            return false;
        }

        function selectCommodity(id, desc) {

            document.getElementById('TabContainerMain_tbDocumentDetails_txtCommodityID').value = id;
            document.getElementById('TabContainerMain_tbDocumentDetails_lblCommodityDescription').innerHTML = desc;
            document.getElementById('TabContainerMain_tbDocumentDetails_txtCommodityID').focus();
        }

        function OpenGBaseCommodityList() {
            open_popup('../HelpForms/Help_GBase_Commodity.aspx', 400, 400, "DocFile");
            return false;
        }

        function selectGBaseCommodity(id, desc) {

            document.getElementById('TabContainerMain_tbDocumentDetails_txtGBaseCommodityID').value = id;
            document.getElementById('TabContainerMain_tbDocumentDetails_lblGBaseCommodityDescription').innerHTML = desc;
            document.getElementById('TabContainerMain_tbDocumentDetails_txtGBaseCommodityID').focus();

            __doPostBack("TabContainerMain_tbDocumentDetails_txtGBaseCommodityID", "TextChanged");
        }

        function OpenOverseasPartyList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;

            if (keycode == 113 || e == 'mouseClick') {

                var txtOverseasPartyID = $get("TabContainerMain_tbDocumentDetails_txtOverseasPartyID");
                open_popup('TF_OverseasPartyLookUp.aspx?bankID=' + txtOverseasPartyID.value, 450, 500, 'OverseasPartyList');
                return false;
            }
        }
        function selectOverseasParty(selectedID) {

            var id = selectedID;
            document.getElementById('hdnOverseasPartyId').value = id;
            document.getElementById('btnOverseasParty').click();
        }

        function OpenOverseasBankList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtOverseasBankID = $get("TabContainerMain_tbDocumentDetails_txtOverseasBankID");
                open_popup('EXP_OverseasBankLookup.aspx?hNo=1&bankID=' + txtOverseasBankID.value, 450, 650, 'OverseasBankList');
                return false;
            }
        }
        function selectOverseasBank(selectedID) {
            var id = selectedID;
            document.getElementById('hdnOverseasId').value = id;
            document.getElementById('btnOverseasBank').click();
        }

        function OpenPayingBankList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtPayingBankID = $get("TabContainerMain_tbGBaseDetails_txtPayingBankID");
                open_popup('../HelpForms/PayingBankLookUp.aspx?hNo=1&bankID=' + txtPayingBankID.value, 450, 650, 'OverseasBankList');
                return false;
            }
        }
        function selectPayingBank(HNo, BankID) {
            document.getElementById('TabContainerMain_tbGBaseDetails_txtPayingBankID').value = BankID;
            document.getElementById('TabContainerMain_tbGBaseDetails_txtPayingBankID').click();

            __doPostBack("TabContainerMain_tbGBaseDetails_txtPayingBankID", "TextChanged");
        }

        function OpenReimbBankList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtReimbBank = $get("TabContainerMain_tbGBaseDetails_txtReimbBank");
                open_popup('../HelpForms/EXP_ReimbBankLookUp.aspx?bankID=' + txtReimbBank.value, 450, 650, 'ReimbBankList');
                return false;
            }
        }

        function selectReimbBank(selectedID) {
            var id = selectedID;
            document.getElementById('TabContainerMain_tbGBaseDetails_txtReimbBank').value = id;

            __doPostBack("TabContainerMain_tbGBaseDetails_txtReimbBank", "TextChanged");
        }

        function OpenCustomerCodeList(e) {
            var keycode;
            var hdnBranchCode = document.getElementById('hdnBranchCode');
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtCustAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
                open_popup('../TF_CustomerLookUp3.aspx?CustID=' + txtCustAcNo.value + '&branchCode=' + hdnBranchCode.value, 400, 400, 'CustomerCodeList');
                return false;
            }
        }
        function selectCustomer(selectedID) {

            var id = selectedID;
            document.getElementById('hdnCustomerCode').value = id;
            document.getElementById('btnCustomerCode').click();
        }
        //        function OpenPortCodeList(e) {
        //            var keycode;
        //            var txtCoveringTo;
        //            if (window.event) keycode = window.event.keyCode;
        //            if (keycode == 113 || e == 'mouseClick') {
        //                var txtCoveringFrom = $get("TabContainerMain_tbDocumentDetails_txtCoveringFrom");
        //                open_popup('../TF_PortCodeLookup.aspx?PortID=' + txtCoveringFrom.value, 450, 450, 'PortCodeList');
        //                return false;
        //            }
        //        }
        //        function selectPort(selectedID) {

        //            var id = selectedID;
        //            document.getElementById('hdnPortCode').value = id;
        //            document.getElementById('btnPortCode').click();
        //        }

        function OpenCountryList(hNo) {

            open_popup('../TF_CountryLookUp1.aspx?hNo=' + hNo, 450, 450, 'CountryList');
            return false;
        }

        function selectCountry(selectedID, hNo) {
            var id = selectedID;
            //    document.getElementById('hdnCountryHelpNo').value = hNo;
            document.getElementById('hdnCountry').value = id;
            document.getElementById('btnCountry').click();

        }

        function OpenDocNoList(e) {

            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {

                var branchCode = document.getElementById('hdnBranchCode');
                var custAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");

                if (custAcNo.value == '') {

                    alert('Enter Customer A/C No.');
                    custAcNo.focus();
                    return false;

                }

                open_popup('EXP_TTdocumentNoLookUp.aspx?branch=' + branchCode.value + '&custAcNo=' + custAcNo.value, 450, 550, 'DocNoList');
                return false;
            }
        }

        function selectDocNo(selectedID, TTamount) {
            var id = selectedID;
            document.getElementById('hdnTTRefNo').value = id;
            document.getElementById('hdnTTAmount').value = TTamount;
            document.getElementById('btnTTRefNo').click();

        }

        function chkShippingBillNo() {

            var txtShippingBillNo = $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillNo");
            txtShippingBillNo.value = txtShippingBillNo.value.replace(/ /g, "");

            var txtShippingbilldate = $get("TabContainerMain_tbCoveringScheduleDetails_txtShippingBillDate");

            //            var txtExchRateGR = $get("TabContainerMain_tbCoveringScheduleDetails_txtExchRateGR");
            var ddlPortCode = $get("TabContainerMain_tbCoveringScheduleDetails_ddlPortCode");

            var i = 0;
            var Grid = $get("TabContainerMain_tbCoveringScheduleDetails_GridViewGRPPCustomsDetails");
            var txtAmountGRPP = $get("TabContainerMain_tbCoveringScheduleDetails_txtAmountGRPP");

            if (txtAmountGRPP.value == "")
                txtAmountGRPP.value = 0;

            if (txtShippingBillNo.value == "") {

                alert('Please Enter Shipping Bill No.');
                txtShippingBillNo.focus();
                return false;

            }

            if (txtShippingbilldate.value == "") {

                alert('Please Enter Shipping Bill date.');
                txtShippingbilldate.focus();
                return false;

            }

            if (ddlPortCode.value == '---select---') {

                alert('Please select Port Code.');
                ddlPortCode.focus();
                return false;

            }

            if (parseFloat(txtAmountGRPP.value) == 0) {
                alert('Please Enter GR Amount.');
                txtAmountGRPP.focus();
                return false;
            }

            //            if (txtExchRateGR.value == "")
            //                txtExchRateGR.value = 0;

            //            if (parseFloat(txtExchRateGR.value) == 0) {
            //                alert('Please Enter GR Exchange Rate.');
            //                txtExchRateGR.focus();
            //                return false;
            //            }

            if (ddlPortCode.value == "0") {
                alert("Please Select Port Code.");
                ddlPortCode.focus();
                return false;
            }


            if (txtShippingBillNo.value == "") {
                alert('Please Enter Unique Shipping Bill No.');
                txtShippingBillNo.focus();
                return false;
            }
            //            else {
            //                // alert(Grid.rows[1].cells[7].innerText);
            //                //alert(Grid.rows.length);

            //                for (i = 1; i < Grid.rows.length; i++) {
            //                    //alert(Grid.rows[i].cells[7].innerText.toString() + " grid");
            //                    //alert(txtShippingBillNo.value.toString() + " textbox");

            //                    if (txtShippingBillNo.value.toString().trim() == Grid.rows[i].cells[7].innerText.toString().trim()) {

            //                        alert("Shipping Bill no. " + txtShippingBillNo.value + " is already exists in Sr No." + Grid.rows[i].cells[0].innerText.toString().trim());
            //                        txtShippingBillNo.focus();
            //                        return false;
            //                    }
            //                }
            //            }

            return true;
        }

        function gridClicked(sr) {
            var id = sr;
            document.getElementById('hdnGridValues').value = id;
            document.getElementById('btnGridValues').click();
        }
        // ADDED BY NILESH 04-08-2023
        function OpenConsigneePartyList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;

            if (keycode == 113 || e == 'mouseClick') {

                var txtconsigneePartyID = $get("TabContainerMain_tbDocumentDetails_txtconsigneePartyID");
                open_popup('../HelpForms/EXP_ConsigneePartyLookUp.aspx?bankID=' + txtconsigneePartyID.value, 450, 500, 'ConsigneePartyList');
                return false;
            }
        }

        function selectConsigneeParty(selectedID) {

            var id = selectedID;
            document.getElementById('hdnConsigneePartyId').value = id;
            document.getElementById('btnConsigneeParty').click();
        }
    </script>
    <script language="javascript" type="text/javascript">


        function ValidateSaveLEI() {
            var hdnIsExtended = $get("hdnIsExtended");
            var hdnIsRealised = $get("hdnIsRealised");
            var hdnIsDelinked = $get("hdnIsDelinked");
            var hdnIsWrittenOff = $get("hdnIsWrittenOff");
            var hdnRole = $get("hdnRole");

            var chkManualGR = $get("TabContainerMain_tbCoveringScheduleDetails_chkManualGR");

            if (hdnRole.value == "User") {
                if (hdnIsExtended.value == "YES") {
                    alert('This Document is already Entended, You cannot update.');
                    return false;
                }
                if (hdnIsRealised.value == "YES") {
                    alert('This Document is already Realised, You cannot update.');
                    return false;
                }
                if (hdnIsDelinked.value == "YES") {
                    alert('This Document is already Delinked, You cannot update.');
                    return false;
                }
                if (hdnIsWrittenOff.value == "YES") {
                    alert('This Document is already WrittenOff, You cannot update.');
                    return false;
                }
            }
            var answer;
            var custAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
            var overseasPartyID = $get("TabContainerMain_tbDocumentDetails_txtOverseasPartyID");

            if (custAcNo.value == '') {
                alert('Enter Customer A/C No.');
                //custAcNo.focus();
                return false;
            }

            if (overseasPartyID.value == '') {
                alert('Enter Overseas Party ID.');
                //overseasPartyID.focus();
                return false;
            }

        }
        ///Shjaiajs
        function checkTTCurr(controlINWCURR, controlRealisedCurr) {


            if (controlINWCURR.value != "" && controlRealisedCurr.value != "") {


                if (controlINWCURR.value == controlRealisedCurr.value) {

                    alert("Realised Currency can not be Same.");
                    controlRealisedCurr.value = "";
                    controlRealisedCurr.focus();
                    return false;
                }
            }
        }

        function ValidateSave() {

            var hdnIsExtended = $get("hdnIsExtended");
            var hdnIsRealised = $get("hdnIsRealised");
            var hdnIsDelinked = $get("hdnIsDelinked");
            var hdnIsWrittenOff = $get("hdnIsWrittenOff");
            var hdnRole = $get("hdnRole");

            var chkManualGR = $get("TabContainerMain_tbCoveringScheduleDetails_chkManualGR");

            if (hdnRole.value == "User") {
                if (hdnIsExtended.value == "YES") {
                    alert('This Document is already Entended, You cannot update.');
                    return false;
                }
                if (hdnIsRealised.value == "YES") {
                    alert('This Document is already Realised, You cannot update.');
                    return false;
                }
                if (hdnIsDelinked.value == "YES") {
                    alert('This Document is already Delinked, You cannot update.');
                    return false;
                }
                if (hdnIsWrittenOff.value == "YES") {
                    alert('This Document is already WrittenOff, You cannot update.');
                    return false;
                }
            }
            var answer;
            var custAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
            var overseasPartyID = $get("TabContainerMain_tbDocumentDetails_txtOverseasPartyID");

            var commodity = $get("TabContainerMain_tbDocumentDetails_txtCommodityID");
            var curr = $get("TabContainerMain_tbTransactionDetails_ddlCurrency");
            var currValue = curr.options[curr.selectedIndex].value;

            var txtBillAmount = $get("TabContainerMain_tbTransactionDetails_txtBillAmount");
            //            var Dispatch = $("#TabContainerMain_tbTransactionDetails_ddlDispBydefault").val();


            if (txtBillAmount.value == '')
                txtBillAmount.value = 0;
            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);


            var txtAWBDate = $get("TabContainerMain_tbDocumentDetails_txtAWBDate");

            var hdnGRtotalAmount = document.getElementById("hdnGRtotalAmount");
            var hdnTTAmount = document.getElementById("hdnTTAmount");

            var txtDocType = $get("txtDocType");
            var txtAmountGRPP = $get("TabContainerMain_tbCoveringScheduleDetails_txtAmountGRPP");


            var txtDueDate = $get("TabContainerMain_tbTransactionDetails_txtDueDate");

            var txtCoveringTo = $get("TabContainerMain_tbDocumentDetails_txtCoveringTo");

            var txtNotes = $get("TabContainerMain_tbDocumentDetails_txtNotes");

            var txtCountry = $get("TabContainerMain_tbDocumentDetails_txtCountry");

            var Merchant = $get("TabContainerMain_tbDocumentDetails_ddlMercTrade");
            var MerchantValue = Merchant.options[Merchant.selectedIndex].value;


            var otherAdCode = $get("TabContainerMain_tbDocumentDetails_rdbotherAdcode");
            var AdCode = $get("TabContainerMain_tbDocumentDetails_txtADCode");

            var txtDueDate = $get("TabContainerMain_tbTransactionDetails_txtDueDate");
            debugger;
            if (MerchantValue == '0') {
                alert('Select Merchant Trade.');
                // currValue.focus();
                return false;
            }

            if (custAcNo.value == '') {
                alert('Enter Customer A/C No.');
                //custAcNo.focus();
                return false;
            }

            if (overseasPartyID.value == '') {
                alert('Enter Overseas Party ID.');
                //overseasPartyID.focus();
                return false;
            }

            if (otherAdCode.checked == true) {
                if (AdCode.value == '') {
                    alert('Enter Ad Code')
                    return false;
                }
            }

            if (txtAWBDate.value == '') {
                alert('Enter AWB/BL No/LR Date.');

                return false;
            }

            if (commodity.value == '') {
                alert('Enter RBI Commodity ID.');
                //commodity.focus();
                return false;
            }




            if (currValue == '0') {
                alert('Select Currency.');
                // currValue.focus();
                return false;
            }

            ////            if (txtDocType.value != 'M') {
            ////                if (txtDueDate.value == '') {
            ////                    alert('Enter Duedate.');
            ////                    return false;
            ////                }


            if (txtCoveringTo.value == '') {
                alert('Enter Covering To.');
                return false;
            }
            if (txtDocType.value == 'BCA' || txtDocType.value == 'BCU' || txtDocType.value == 'EB') {
                if (txtNotes.value == '') {
                    alert('Please Enter Enate number for notes.');
                    return false;
                }
            }


            if (txtCountry.value == '') {
                alert('Select Country.');
                return false;
            }

            if (txtDueDate.value == "") {
                alert('Please enter Due Date.');
                return false;
            }

            //            var Document_No = document.getElementById("txtDocumentNo").value;
            //            var Document_Type = Document_No.substring(0, 2);
            //            if (Document_Type == 'EB') {
            //                if (parseFloat(SumTTandFIRCTotalAmt) > parseFloat(txtBillAmount.value)) {
            //                    alert('TT or FIRC Amount is More Than Bill Amount');

            //                    return false;
            //                }
            //                //return false;
            //            }




            if (chkManualGR.checked) {
                alert("This is a Manual GR, Please select Part Payment while entering the Realisation Details.");
            }

            if (txtAmountGRPP.value == "")
                txtAmountGRPP.value = "0";


            if (parseFloat(txtAmountGRPP.value) > 0) {
                alert('Click  Add  button to save GR Details and then Click Save button to save the entire record.');
                // currValue.focus();
                return false;
            }

            if (txtDocType.value != "B" && txtDocType.value != "S") {
                if (parseFloat(txtBillAmount.value) != parseFloat(hdnGRtotalAmount.value)) {
                    if (parseFloat(txtBillAmount.value) > parseFloat(hdnGRtotalAmount.value)) {
                        answer = confirm('GR Amount is Less than Bill Amount by ' + (parseFloat(hdnGRtotalAmount.value) - parseFloat(txtBillAmount.value)))
                        if (!answer)

                            return false;
                    }
                    if (parseFloat(txtBillAmount.value) < parseFloat(hdnGRtotalAmount.value)) {
                        answer = confirm('GR Amount is Greater than Bill Amount by ' + (parseFloat(hdnGRtotalAmount.value) - parseFloat(txtBillAmount.value)))
                        if (!answer)

                            return false;
                    }
                }
            }

            //////            if (txtDocType.value == "M" && hdnTTAmount.value != "") {

            //////                if (parseFloat(txtBillAmount.value) > parseFloat(hdnTTAmount.value)) {
            //////                    answer = confirm('MBill Amount is more than Remmitance Amount,\n Do you want to proceed?')
            //////                    if (!answer)

            //////                        return false;
            //////                }

            //////            }

            //New GBASE FIELDS

            var DocType = $get("txtDocType");
            var ForeignLocal = $get("hdnForeignLocal");

            //New GBASE

            var txtGBaseCommodityID = $get("TabContainerMain_tbDocumentDetails_txtGBaseCommodityID");



            var txtOperationType = $get("TabContainerMain_tbGBaseDetails_txtOperationType");
            var txtSettlementOption = $get("TabContainerMain_tbGBaseDetails_txtSettlementOption");
            var txtReimbBank = $get("TabContainerMain_tbGBaseDetails_txtReimbBank");
            var txtFundType = $get("TabContainerMain_tbGBaseDetails_txtFundType");
            var txtInternalRate = $get("TabContainerMain_tbGBaseDetails_txtInternalRate");
            var txtBaseRate = $get("TabContainerMain_tbGBaseDetails_txtBaseRate");
            var txtGradeCode = $get("TabContainerMain_tbGBaseDetails_txtGradeCode");
            var txtSpread = $get("TabContainerMain_tbGBaseDetails_txtSpread");
            var txtApplNo = $get("TabContainerMain_tbGBaseDetails_txtApplNo");
            var txtRiskCountry = $get("TabContainerMain_tbGBaseDetails_txtRiskCountry");
            var txtDirection = $get("TabContainerMain_tbGBaseDetails_txtDirection");
            var txtCovrInstr = $get("TabContainerMain_tbGBaseDetails_txtCovrInstr");
            var txtDraftNo = $get("TabContainerMain_tbGBaseDetails_txtDraftNo");
            var txtMerchandise = $get("TabContainerMain_tbGBaseDetails_txtMerchandise");
            var txtRiskCustomer = $get("TabContainerMain_tbGBaseDetails_txtRiskCustomer");
            var txtGBaseRemarks = $get("TabContainerMain_tbGBaseDetails_txtGBaseRemarks");
            var txtPayingBankID = $get("TabContainerMain_tbGBaseDetails_txtPayingBankID");

            var RemEUC = $get("TabContainerMain_tbGBaseDetails_ddlRemEUC");
            var RemEUCValue = RemEUC.options[RemEUC.selectedIndex].value;

            //            var Merchant = $get("TabContainerMain_tbDocumentDetails_ddlMercTrade");
            //            var MerchantValue = Merchant.options[Merchant.selectedIndex].value;

            if (DocType.value == 'BCA' || DocType.value == 'BCU') {
                if (txtGBaseCommodityID.value == '') {
                    alert('Enter GBase Commodity ID.');
                    return false;
                }
                //                if (txtReimbBank.value == "") {
                //                    alert('Please Enter Reimbursing Bank.');
                //                    return false;
                //                }
                if (txtGradeCode.value == "") {
                    alert('Please Enter Grade Code.');
                    return false;
                }
                if (txtRiskCountry.value == "") {
                    alert('Please Enter Country Risk.');
                    return false;
                }
                if (txtDirection.value == "") {
                    alert('Please Enter Direction.');
                    return false;
                }
                if (txtCovrInstr.value == "") {
                    alert('Please Enter Covr Instruction.');
                    return false;
                }
                if (txtDraftNo.value == "") {
                    alert('Please Enter Draft No.');
                    return false;
                }
                if (txtRiskCustomer.value == "") {
                    alert('Please Enter Risk Customer.');
                    return false;
                }
                if (txtReimbBank.value == "") {
                    alert('Please Enter Reimbursing Bank.');
                    return false;
                }
                if (txtGBaseRemarks.value == "") {
                    alert('Please Enter Remarks.');
                    return false;
                }
                if (txtMerchandise.value == "") {
                    alert('Please Enter Merchandise.');
                    return false;
                }
                if (RemEUCValue == '0') {
                    alert('Select Remarks(EUC).');
                    // currValue.focus();
                    return false;
                }
                //                if (MerchantValue == '0') {
                //                    alert('Select Merchant Trade.');
                //                    // currValue.focus();
                //                    return false;
                //                }
            }
            else if (DocType.value == 'BLA' || DocType.value == 'BLU' || DocType.value == 'BBA' || DocType.value == 'BBU' || DocType.value == 'IBD') {
                if (txtGBaseCommodityID.value == '') {
                    alert('Enter GBase Commodity ID.');
                    return false;
                }
                if (txtOperationType.value == "") {
                    alert('Please Enter Operation Type.');
                    return false;
                }
                if (txtSettlementOption.value == "") {
                    alert('Please Enter Settlement Option.');
                    return false;
                }
                if (txtRiskCountry.value == "") {
                    alert('Please Enter Country Risk.');
                    return false;
                }
                if (txtFundType.value == "") {
                    alert('Please Enter Fund Type.');
                    return false;
                }
                if (txtBaseRate.value == "") {
                    alert('Please Enter Base Rate.');
                    return false;
                }
                if (txtGradeCode.value == "") {
                    alert('Please Enter Grade Code.');
                    return false;
                }
                if (txtDirection.value == "") {
                    alert('Please Enter Direction.');
                    return false;
                }
                if (txtCovrInstr.value == "") {
                    alert('Please Enter Covr Instruction.');
                    return false;
                }
                if (txtInternalRate.value == "") {
                    alert('Please Enter Internal Rate.');
                    return false;
                }
                if (txtSpread.value == "") {
                    alert('Please Enter Spread.');
                    return false;
                }
                if (txtApplNo.value == "") {
                    alert('Please Enter Application No..');
                    return false;
                }
                if (txtDraftNo.value == "") {
                    alert('Please Enter Draft No.');
                    return false;
                }
                if (txtRiskCustomer.value == "") {
                    alert('Please Enter Risk Customer.');
                    return false;
                }
                if (txtReimbBank.value == "") {
                    alert('Please Enter Reimbursing Bank.');
                    return false;
                }
                if (txtGBaseRemarks.value == "") {
                    alert('Please Enter Remarks.');
                    return false;
                }
                if (txtMerchandise.value == "") {
                    alert('Please Enter Merchandise.');
                    return false;
                }
                if (txtPayingBankID.value == "") {
                    alert('Please Enter Paying Bank.');
                    return false;
                }
                if (RemEUCValue == '0') {
                    alert('Select Remarks(EUC).');
                    // currValue.focus();
                    return false;
                }

                //                if (MerchantValue == '0') {
                //                    alert('Select Merchant Trade.');
                //                    // currValue.focus();
                //                    return false;
                //                if (Dispatch == '' || Dispatch == '--Select--' || Dispatch == '0') {
                //                    alert('Select Deipatch.');
                //                    // currValue.focus();
                //                    return false;
                //                } 

            }
            var txtNoOfSB = $get("TabContainerMain_tbCoveringScheduleDetails_txtNoOfSB");
            if (txtNoOfSB.value == '') {
                alert('Enter No of Shipping Biills.');
                txtNoOfSB.focus();
                return false;
            }

            return true;
        }




        function Calculate() {
            //            var hdnIsExtended = $get("hdnIsExtended");
            //            if (hdnIsExtended.value == "YES")
            //                return false;

            var txtDocType = $get("txtDocType");
            //var rbtnSightBill = $get("TabContainerMain_tbTransactionDetails_rbtnSightBill");
            var rbtnSightBill = $get("rbtnSightBill");
            //var rbtnUsanceBill = $get("TabContainerMain_tbTransactionDetails_rbtnUsanceBill");
            var rbtnUsanceBill = $get("rbtnUsanceBill");

            var rbtnAfterAWB = $get("TabContainerMain_tbTransactionDetails_rbtnAfterAWB");
            var rbtnFromAWB = $get("TabContainerMain_tbTransactionDetails_rbtnFromAWB");
            var rbtnSight = $get("TabContainerMain_tbTransactionDetails_rbtnSight");
            var rbtnDA = $get("TabContainerMain_tbTransactionDetails_rbtnDA");
            var rbtnFromInvoice = $get("TabContainerMain_tbTransactionDetails_rbtnFromInvoice");
            var rbtnOthers = $get("TabContainerMain_tbTransactionDetails_rbtnOthers");

            var rbtnEBR = $get("TabContainerMain_tbTransactionDetails_rbtnEBR");
            var rbtnBDR = $get("TabContainerMain_tbTransactionDetails_rbtnBDR");

            var chkLoanAdv = $get("TabContainerMain_tbTransactionDetails_chkLoanAdv");
            var txtNoOfDays = $get("TabContainerMain_tbTransactionDetails_txtNoOfDays");
            if (txtNoOfDays.value == '')
                txtNoOfDays.value = 0;

            var txtOutOfDays = $get("TabContainerMain_tbTransactionDetails_txtOutOfDays");
            if (txtOutOfDays.value == '')
                txtOutOfDays.value = 0;



            var FL = document.getElementById('hdnForeignLocal').value;
            var txtIntRate1 = $get("TabContainerMain_tbTransactionDetails_txtIntRate1");



            if (txtIntRate1.value == '')
                txtIntRate1.value = 0.00;
            //txtIntRate1.value = parseFloat(txtIntRate1.value).toFixed(2);
            txtIntRate1.value = parseFloat(txtIntRate1.value);

            var txtIntRate2 = $get("TabContainerMain_tbTransactionDetails_txtIntRate2");


            if (txtIntRate2.value == '')
                txtIntRate2.value = 0;
            txtIntRate2.value = parseFloat(txtIntRate2.value).toFixed(2);


            var txtIntRate3 = $get("TabContainerMain_tbTransactionDetails_txtIntRate3");

            if (txtIntRate3.value == '')
                txtIntRate3.value = 0;
            txtIntRate3.value = parseFloat(txtIntRate3.value).toFixed(2);

            var txtIntRate4 = $get("TabContainerMain_tbTransactionDetails_txtIntRate4");

            if (txtIntRate4.value == '')
                txtIntRate4.value = 0;
            txtIntRate4.value = parseFloat(txtIntRate4.value).toFixed(2);

            var txtIntRate5 = $get("TabContainerMain_tbTransactionDetails_txtIntRate5");

            if (txtIntRate5.value == '')
                txtIntRate5.value = 0;
            txtIntRate5.value = parseFloat(txtIntRate5.value).toFixed(2);

            var txtIntRate6 = $get("TabContainerMain_tbTransactionDetails_txtIntRate6");

            if (txtIntRate6.value == '')
                txtIntRate6.value = 0;
            txtIntRate6.value = parseFloat(txtIntRate6.value).toFixed(2);


            var txtForDays1 = $get("TabContainerMain_tbTransactionDetails_txtForDays1");
            if (txtForDays1.value == '')
                txtForDays1.value = 0;

            var txtForDays2 = $get("TabContainerMain_tbTransactionDetails_txtForDays2");
            if (txtForDays2.value == '')
                txtForDays2.value = 0;

            var txtForDays3 = $get("TabContainerMain_tbTransactionDetails_txtForDays3");
            if (txtForDays3.value == '')
                txtForDays3.value = 0;

            var txtForDays4 = $get("TabContainerMain_tbTransactionDetails_txtForDays4");
            if (txtForDays4.value == '')
                txtForDays4.value = 0;

            var txtForDays5 = $get("TabContainerMain_tbTransactionDetails_txtForDays5");
            if (txtForDays5.value == '')
                txtForDays5.value = 0;

            var txtForDays6 = $get("TabContainerMain_tbTransactionDetails_txtForDays6");
            if (txtForDays6.value == '')
                txtForDays6.value = 0;

            var txtIntFrmDate1 = $get("TabContainerMain_tbTransactionDetails_txtIntFrmDate1");
            var txtIntToDate1 = $get("TabContainerMain_tbTransactionDetails_txtIntToDate1");
            var txtIntFrmDate2 = $get("TabContainerMain_tbTransactionDetails_txtIntFrmDate2");
            var txtIntToDate2 = $get("TabContainerMain_tbTransactionDetails_txtIntToDate2");
            var txtIntFrmDate3 = $get("TabContainerMain_tbTransactionDetails_txtIntFrmDate3");
            var txtIntToDate3 = $get("TabContainerMain_tbTransactionDetails_txtIntToDate3");
            var txtIntFrmDate4 = $get("TabContainerMain_tbTransactionDetails_txtIntFrmDate4");
            var txtIntToDate4 = $get("TabContainerMain_tbTransactionDetails_txtIntToDate4");
            var txtIntFrmDate5 = $get("TabContainerMain_tbTransactionDetails_txtIntFrmDate5");
            var txtIntToDate5 = $get("TabContainerMain_tbTransactionDetails_txtIntToDate5");
            var txtIntFrmDate6 = $get("TabContainerMain_tbTransactionDetails_txtIntFrmDate6");
            var txtIntToDate6 = $get("TabContainerMain_tbTransactionDetails_txtIntToDate6");

            var InterestRate = 0;
            var NoOfDays = 0;

            if (parseFloat(txtIntRate1.value) > 0) {
                NoOfDays = CalculateNoOfDays(txtIntFrmDate1.value, txtIntToDate1.value);
                InterestRate = txtIntRate1.value;
            }
            if (parseFloat(txtIntRate2.value) > 0) {
                NoOfDays = CalculateNoOfDays(txtIntFrmDate2.value, txtIntToDate2.value);
                InterestRate = txtIntRate2.value;
            }
            if (parseFloat(txtIntRate3.value) > 0) {
                NoOfDays = CalculateNoOfDays(txtIntFrmDate3.value, txtIntToDate3.value);
                InterestRate = txtIntRate3.value;
            }
            if (parseFloat(txtIntRate4.value) > 0) {
                NoOfDays = CalculateNoOfDays(txtIntFrmDate4.value, txtIntToDate4.value);
                InterestRate = txtIntRate4.value;
            }
            if (parseFloat(txtIntRate5.value) > 0) {
                NoOfDays = CalculateNoOfDays(txtIntFrmDate5.value, txtIntToDate5.value);
                InterestRate = txtIntRate5.value;
            }
            if (parseFloat(txtIntRate6.value) > 0) {
                NoOfDays = CalculateNoOfDays(txtIntFrmDate6.value, txtIntToDate6.value);
                InterestRate = txtIntRate6.value;
            }

            var hdnDateDiff = $get("hdnDateDiff");
            var hdnDateDiff_InvoiceDate = $get("hdnDateDiff_InvoiceDate");
            //////            var hdnSightBillMaxDays = $get("hdnSightBillMaxDays");
            //////            var hdnSightBillOutOfDays = $get("hdnSightBillOutOfDays");
            //////            var hdnSightBillInterestRate = $get("hdnSightBillInterestRate");
            //////            var hdnUsanceBillMaxDays = $get("hdnUsanceBillMaxDays");
            //////            var hdnUsanceBillOutOfDays = $get("hdnUsanceBillOutOfDays");
            //////            var hdnUsanceBillInterestRate1 = $get("hdnUsanceBillInterestRate1");
            //////            var hdnUsanceBillInterestRate2 = $get("hdnUsanceBillInterestRate2");
            //////            var hdnEBRInterestRate = $get("hdnEBRInterestRate");
            //////            var hdnEBROutOfDays = $get("hdnEBROutOfDays");
            //////            var hdnUsanceBillToDays1 = $get("hdnUsanceBillToDays1");
            var txtCurrentAcinRS = $get("TabContainerMain_tbTransactionDetails_txtCurrentAcinRS");

            ////            var hdnBankCert = $get("hdnBankCert");
            ////            var hdnNegoFees = $get("hdnNegoFees");
            ////            var hdnCourierCharges = $get("hdnCourierCharges");

            var sTax = $get("TabContainerMain_tbTransactionDetails_ddlServiceTax");
            var sTaxValue = sTax.options[sTax.selectedIndex].value;

            var curr = $get("TabContainerMain_tbTransactionDetails_ddlCurrency");
            var currValue = curr.options[curr.selectedIndex].value;

            var txtExchRate = $get("TabContainerMain_tbTransactionDetails_txtExchRate");
            if (txtExchRate.value == '')
                txtExchRate.value = 0;
            txtExchRate.value = parseFloat(txtExchRate.value).toFixed(10);

            var txtLibor = $get("TabContainerMain_tbTransactionDetails_txtLibor");
            if (txtLibor.value == '')
                txtLibor.value = 0;
            txtLibor.value = parseFloat(txtLibor.value).toFixed(6);

            var txtBillAmount = $get("TabContainerMain_tbTransactionDetails_txtBillAmount");
            if (txtBillAmount.value == '')
                txtBillAmount.value = 0;
            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);

            var txtBillAmountinRS = $get("TabContainerMain_tbTransactionDetails_txtBillAmountinRS");
            if (txtBillAmountinRS.value == '')
                txtBillAmountinRS.value = 0;
            txtBillAmountinRS.value = parseFloat(txtBillAmountinRS.value).toFixed(2);

            var txtNegotiatedAmt = $get("TabContainerMain_tbTransactionDetails_txtNegotiatedAmt");
            if (txtNegotiatedAmt.value == '')
                txtNegotiatedAmt.value = 0;
            txtNegotiatedAmt.value = parseFloat(txtNegotiatedAmt.value).toFixed(2);

            var txtNegotiatedAmtinRS = $get("TabContainerMain_tbTransactionDetails_txtNegotiatedAmtinRS");
            if (txtNegotiatedAmtinRS.value == '')
                txtNegotiatedAmtinRS.value = 0;
            txtNegotiatedAmtinRS.value = parseFloat(txtNegotiatedAmtinRS.value).toFixed(2);

            var txtInterest = $get("TabContainerMain_tbTransactionDetails_txtInterest");
            if (txtInterest.value == '')
                txtInterest.value = 0;

            if (currValue == "INR") {
                txtInterest.value = parseFloat(txtInterest.value).toFixed(0);
            }
            else {
                txtInterest.value = parseFloat(txtInterest.value).toFixed(2);
            }


            var txtInterestinRS = $get("TabContainerMain_tbTransactionDetails_txtInterestinRS");
            if (txtInterestinRS.value == '')
                txtInterestinRS.value = 0;
            //txtInterestinRS.value = parseFloat(txtInterestinRS.value).toFixed(2);

            if (currValue == "INR") {
                txtInterestinRS.value = parseFloat(txtInterestinRS.value).toFixed(0);
            }
            else {
                txtInterestinRS.value = parseFloat(txtInterestinRS.value).toFixed(2);
            }

            var txtNetAmt = $get("TabContainerMain_tbTransactionDetails_txtNetAmt");
            if (txtNetAmt.value == '')
                txtNetAmt.value = 0;
            txtNetAmt.value = parseFloat(txtNetAmt.value).toFixed(2);

            var txtNetAmtinRS = $get("TabContainerMain_tbTransactionDetails_txtNetAmtinRS");
            if (txtNetAmtinRS.value == '')
                txtNetAmtinRS.value = 0;
            txtNetAmtinRS.value = parseFloat(txtNetAmtinRS.value).toFixed(2);

            //////fbk////////

            var txtfbkcharges = $get("TabContainerMain_tbTransactionDetails_txt_fbkcharges");
            if (txtfbkcharges.value == '')
                txtfbkcharges.value = 0;
            txtfbkcharges.value = parseFloat(txtfbkcharges.value).toFixed(2);

            var txtfbkchargesRS = $get("TabContainerMain_tbTransactionDetails_txt_fbkchargesinRS");
            if (txtfbkchargesRS.value == '')
                txtfbkchargesRS.value = 0;
            txtfbkchargesRS.value = parseFloat(txtfbkchargesRS.value).toFixed(2);



            var txtOtherChrgs = $get("TabContainerMain_tbTransactionDetails_txtOtherChrgs");
            if (txtOtherChrgs.value == '')
                txtOtherChrgs.value = 0;
            txtOtherChrgs.value = parseFloat(txtOtherChrgs.value).toFixed(2);

            var txtBankCert = $get("TabContainerMain_tbTransactionDetails_txtBankCert");
            if (txtBankCert.value == '')
                txtBankCert.value = 0;
            txtBankCert.value = parseFloat(txtBankCert.value).toFixed(2);

            var txtNegotiationFees = $get("TabContainerMain_tbTransactionDetails_txtNegotiationFees");
            if (txtNegotiationFees.value == '')
                txtNegotiationFees.value = 0;
            txtNegotiationFees.value = parseFloat(txtNegotiationFees.value).toFixed(2);

            var txtCourierChrgs = $get("TabContainerMain_tbTransactionDetails_txtCourierChrgs");
            if (txtCourierChrgs.value == '')
                txtCourierChrgs.value = 0;
            txtCourierChrgs.value = parseFloat(txtCourierChrgs.value).toFixed(2);

            var txtMarginAmt = $get("TabContainerMain_tbTransactionDetails_txtMarginAmt");
            if (txtMarginAmt.value == '')
                txtMarginAmt.value = 0;
            txtMarginAmt.value = parseFloat(txtMarginAmt.value).toFixed(2);

            var txtCommission = $get("TabContainerMain_tbTransactionDetails_txtCommission");
            if (txtCommission.value == '')
                txtCommission.value = 0;
            txtCommission.value = parseFloat(txtCommission.value).toFixed(2);

            var txtSTaxAmount = $get("TabContainerMain_tbTransactionDetails_txtSTaxAmount");
            if (txtSTaxAmount.value == '')
                txtSTaxAmount.value = 0;
            txtSTaxAmount.value = parseFloat(txtSTaxAmount.value).toFixed(2);

            var txtSTFXDLS = $get("TabContainerMain_tbTransactionDetails_txttotsbcess");
            if (txtSTFXDLS.value == '')
                txtSTFXDLS.value = 0;
            txtSTFXDLS.value = parseFloat(txtSTFXDLS.value).toFixed(2);

            var txtExchRtEBR = $get("TabContainerMain_tbTransactionDetails_txtExchRtEBR");
            if (txtExchRtEBR.value == '')
                txtExchRtEBR.value = 0;
            txtExchRtEBR.value = parseFloat(txtExchRtEBR.value).toFixed(10);

            var txtCommissionID = $get("TabContainerMain_tbTransactionDetails_txtCommissionID");

            //////            var txtTotalPCLiquidated = $get("TabContainerMain_tbTransactionDetails_txtTotalPCLiquidated");
            //////            if (txtTotalPCLiquidated.value == '')
            //////                txtTotalPCLiquidated.value = 0;
            //////            txtTotalPCLiquidated.value = parseFloat(txtTotalPCLiquidated.value).toFixed(2);

            var _sTaxAmt = 0;
            var _sTaxValue = 0;
            var _bankCert = 0;
            var _NegoFees = 0;
            var _CourierChrgs = 0;
            var _Commission = 0;
            var _OtherCharges = 0;

            var hdnCommRate = document.getElementById('hdnCommRate');
            var hdnCommMinAmt = document.getElementById('hdnCommMinAmt');

            var hdnCommMaxAmt = document.getElementById('hdnCommMaxAmt');
            var hdnCommFlat = document.getElementById('hdnCommFlat');

            var hdnServiceTax = document.getElementById('hdnServiceTax');
            var hdnEDU_CESS = document.getElementById('hdnEDU_CESS');
            var hdnSEC_EDU_CESS = document.getElementById('hdnSEC_EDU_CESS');

            if (hdnCommRate.value == '' || isNaN(hdnCommRate.value))
                hdnCommRate.value = 0;

            if (hdnCommMinAmt.value == '' || isNaN(hdnCommMinAmt.value))
                hdnCommMinAmt.value = 0;

            if (txtCommissionID.value == '') {
                hdnCommRate.value = 0;
                hdnCommMinAmt.value = 0;
            }

            var _serviceTax = 0;
            var _serviceTax_EDUCess = 0;
            var _serviceTax_SecEDUCess = 0;

            ////            if (txtDocType.value != "BCA") {
            ////                if (txtDocType.value != "BCU") {
            ////                    rbtnEBR.checked = false;
            ////                    rbtnBDR.checked = false;
            ////                }
            ////            }
            ////            else {
            ////                if (chkLoanAdv.checked == true) {
            ////                    if (rbtnBDR.checked == true)
            ////                        txtLibor.value = 0;
            ////                    else {
            ////                        if (parseFloat(txtExchRate.value) == 0)
            ////                            txtExchRate.value = 1;
            ////                    }
            ////                }
            ////                else {
            ////                    rbtnEBR.checked = false;
            ////                    rbtnBDR.checked = false;
            ////                }
            ////            }


            //////            if (txtDocType.value == "BCA" || txtDocType.value == "BCU") {
            //////                if (chkLoanAdv.checked == true) {
            //////                    if (rbtnBDR.checked == true)
            //////                        txtLibor.value = 0;
            //////                    else {
            //////                        if (parseFloat(txtExchRate.value) == 0)
            //////                            txtExchRate.value = 1;
            //////                    }
            //////                }
            //////                else {
            //////                    rbtnEBR.checked = false;
            //////                    rbtnBDR.checked = false;
            //////                }
            //////            }
            //////            else {
            //////                rbtnEBR.checked = false;
            //////                rbtnBDR.checked = false;
            //////            }

            if (chkLoanAdv.checked == true) {
                if (rbtnBDR.checked == true)
                    txtLibor.value = 0;
                else {
                    if (parseFloat(txtExchRate.value) == 0)
                        txtExchRate.value = 1;
                }
            }
            else {
                rbtnEBR.checked = false;
                rbtnBDR.checked = false;
            }


            if (txtDocType.value == "EB") {

                if (parseFloat(txtNegotiatedAmt.value) == parseFloat(0)) {
                    txtNegotiatedAmt.value = txtBillAmount.value;
                }

                if (txtSTaxAmount.value != '')
                    _sTaxAmt = parseFloat(txtSTaxAmount.value);
                if (sTaxValue != '')
                    _sTaxValue = parseFloat(sTaxValue);

                if (txtBankCert.value != '')
                    _bankCert = parseFloat(txtBankCert.value);
                if (txtNegotiationFees.value != '')
                    _NegoFees = parseFloat(txtNegotiationFees.value);
                if (txtCourierChrgs.value != '')
                    _CourierChrgs = parseFloat(txtCourierChrgs.value);
                if (txtCommission.value != '') {
                    _Commission = parseFloat(txtCommission.value);
                }
                if (txtOtherChrgs.value != '')
                    _OtherCharges = parseFloat(txtOtherChrgs.value);

                //_serviceTax = (_bankCert + _NegoFees + _CourierChrgs + _Commission + _OtherCharges) * (parseFloat(hdnServiceTax.value) / 100);
                //_serviceTax = Math.round(_serviceTax);

                _serviceTax_EDUCess = _serviceTax * (parseFloat(hdnEDU_CESS.value) / 100);

                _serviceTax_SecEDUCess = _serviceTax * (parseFloat(hdnSEC_EDU_CESS.value) / 100);

                //_sTaxAmt = parseFloat(_serviceTax) + parseFloat(_serviceTax_EDUCess) + parseFloat(_serviceTax_SecEDUCess);

                if (_sTaxAmt.value == '')
                    _sTaxAmt.value = 0;

                var SBcess = $get("TabContainerMain_tbTransactionDetails_txtsbcess");
                if (SBcess.value == '')
                    SBcess.value = 0;

                var SBcessamt = $get("TabContainerMain_tbTransactionDetails_txtSBcesssamt");
                if (SBcessamt.value == '')
                    SBcessamt.value = 0;

                var KKCess = $get("TabContainerMain_tbTransactionDetails_txt_kkcessper");
                if (KKCess.value == '')
                    KKCess.value = 0;

                var KKcessamt = $get("TabContainerMain_tbTransactionDetails_txt_kkcessamt");
                if (KKcessamt.value == '')
                    KKcessamt.value = 0;


                var STTamt = $get("TabContainerMain_tbTransactionDetails_txtsttamt");

                _serviceTax = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(hdnServiceTax.value) / 100)
                _serviceTax = Math.round(_serviceTax);
                _serviceTax = parseFloat(_serviceTax).toFixed(2);


                if (SBcess.value != '') {

                    a = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(SBcess.value) / 100)
                    SBcessamt.value = parseFloat(a).toFixed(2);
                    var sbcess = SBcessamt.value;
                    //STTamt.value = parseFloat(a + _serviceTax).toFixed(2);
                }

                else {

                    //STTamt.value = parseFloat(_serviceTax).toFixed(2);

                    if (SBcessamt.value == '')
                        SBcessamt.value = 0;
                }

                if (KKCess.value != '') {

                    a = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(KKCess.value) / 100)
                    KKcessamt.value = parseFloat(a).toFixed(2);
                    var kkcess = KKcessamt.value;
                    //STTamt.value = parseFloat(a + _serviceTax).toFixed(2);
                }

                else {

                    KKcessamt.value = 0;
                }

                _serviceTax_EDUCess = _serviceTax * (parseFloat(hdnEDU_CESS.value) / 100);
                _serviceTax_SecEDUCess = _serviceTax * (parseFloat(hdnSEC_EDU_CESS.value) / 100);

                _sTaxAmt = parseFloat(_serviceTax) + parseFloat(_serviceTax_EDUCess) + parseFloat(_serviceTax_SecEDUCess);

                if (_sTaxAmt.value == '')
                    _sTaxAmt.value = 0;

                txtSTaxAmount.value = _sTaxAmt.toFixed(2);


                ////total stax amount///////

                STTamt.value = parseFloat(parseFloat(_serviceTax) + parseFloat(sbcess) + parseFloat(kkcess)).toFixed(2); var SBcess = $get("TabContainerMain_tbTransactionDetails_txtsbcess");
                if (SBcess.value == '')
                    SBcess.value = 0;

                var SBcessamt = $get("TabContainerMain_tbTransactionDetails_txtSBcesssamt");
                if (SBcessamt.value == '')
                    SBcessamt.value = 0;

                var KKCess = $get("TabContainerMain_tbTransactionDetails_txt_kkcessper");
                if (KKCess.value == '')
                    KKCess.value = 0;

                var KKcessamt = $get("TabContainerMain_tbTransactionDetails_txt_kkcessamt");
                if (KKcessamt.value == '')
                    KKcessamt.value = 0;


                var STTamt = $get("TabContainerMain_tbTransactionDetails_txtsttamt");

                _serviceTax = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(hdnServiceTax.value) / 100)
                _serviceTax = Math.round(_serviceTax);
                _serviceTax = parseFloat(_serviceTax).toFixed(2);


                if (SBcess.value != '') {

                    a = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(SBcess.value) / 100)
                    SBcessamt.value = parseFloat(a).toFixed(2);
                    var sbcess = SBcessamt.value;
                    //STTamt.value = parseFloat(a + _serviceTax).toFixed(2);
                }

                else {

                    //STTamt.value = parseFloat(_serviceTax).toFixed(2);

                    if (SBcessamt.value == '')
                        SBcessamt.value = 0;
                }

                if (KKCess.value != '') {

                    a = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(KKCess.value) / 100)
                    KKcessamt.value = parseFloat(a).toFixed(2);
                    var kkcess = KKcessamt.value;
                    //STTamt.value = parseFloat(a + _serviceTax).toFixed(2);
                }

                else {

                    KKcessamt.value = 0;
                }

                _serviceTax_EDUCess = _serviceTax * (parseFloat(hdnEDU_CESS.value) / 100);
                _serviceTax_SecEDUCess = _serviceTax * (parseFloat(hdnSEC_EDU_CESS.value) / 100);

                _sTaxAmt = parseFloat(_serviceTax) + parseFloat(_serviceTax_EDUCess) + parseFloat(_serviceTax_SecEDUCess);

                if (_sTaxAmt.value == '')
                    _sTaxAmt.value = 0;

                txtSTaxAmount.value = _sTaxAmt.toFixed(2);


                ////total stax amount///////

                STTamt.value = parseFloat(parseFloat(_serviceTax) + parseFloat(sbcess) + parseFloat(kkcess)).toFixed(2);

                txtSTaxAmount.value = _sTaxAmt.toFixed(2);

                rbtnSightBill.checked = false;
                rbtnUsanceBill.checked = false;
                chkLoanAdv.checked = false;
                txtNoOfDays.value = 0;

                rbtnAfterAWB.checked = false;
                rbtnFromAWB.checked = false;
                rbtnSight.checked = false;
                rbtnDA.checked = false;
                rbtnFromInvoice.checked = false;
                rbtnOthers.checked = false;

                return true;

            }


            //==================Calculation of Due Date=======================//


            var txtDateRcvd = $get("TabContainerMain_tbDocumentDetails_txtDateRcvd");
            var txtDateNegotiated = $get("TabContainerMain_tbDocumentDetails_txtDateNegotiated");
            var txtAWBDate = $get("TabContainerMain_tbDocumentDetails_txtAWBDate");
            var txtDueDate = $get("TabContainerMain_tbTransactionDetails_txtDueDate");
            var txtInvoiceDate = $get("TabContainerMain_tbDocumentDetails_txtInvoiceDate");
            var hdnTempDueDate = $get("hdnTempDueDate");

            ////            if (txtDateNegotiated.value == "")
            ////                txtDateNegotiated.value = txtDateRcvd.value;

            ////            var _NegoDate = txtDateNegotiated.value.split("/");

            ////            var nDay = _NegoDate[0];
            ////            var nMonth = _NegoDate[1];
            ////            var nYear = _NegoDate[2];

            ////            var _dueDate = new Date();
            ////            var _AWBDate = txtAWBDate.value.split("/");

            ////            var awbDay = _AWBDate[0];
            ////            var awbMonth = _AWBDate[1];
            ////            var awbYear = _AWBDate[2];

            ////            var _InvDate = txtInvoiceDate.value.split("/");

            ////            var invDay = _InvDate[0];
            ////            var invMonth = _InvDate[1];
            ////            var invYear = _InvDate[2];

            ////            //alert(txtDueDate.value);
            ////            if (rbtnOthers.checked == false) {

            ////                if (rbtnSightBill.checked == true) {
            ////                    _dueDate.setFullYear(nYear, nMonth - 1, nDay);
            ////                    _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));

            ////                }
            ////                else {
            ////                    if (txtAWBDate.value != "") {
            ////                        if (rbtnAfterAWB.checked == true) {
            ////                            _dueDate.setFullYear(awbYear, awbMonth - 1, awbDay);
            ////                            _dueDate.setDate(_dueDate.getDate() + (parseInt(txtNoOfDays.value) + 1));
            ////                        }
            ////                        else if (rbtnFromAWB.checked == true) {
            ////                            _dueDate.setFullYear(awbYear, awbMonth - 1, awbDay);
            ////                            _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));
            ////                        }
            ////                        else if (rbtnSight.checked == true || rbtnDA.checked == true) {
            ////                            _dueDate.setFullYear(nYear, nMonth - 1, nDay);
            ////                            _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));
            ////                        }
            ////                    }
            ////                    if (txtInvoiceDate.value != "") {
            ////                        if (rbtnFromInvoice.checked == true) {
            ////                            _dueDate.setFullYear(invYear, invMonth - 1, invDay);
            ////                            _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));
            ////                        }
            ////                    }
            ////                }

            ////                var _newDueDay = _dueDate.getDate();
            ////                var _newDueMonth = _dueDate.getMonth() + 1;
            ////                var _newDueYear = _dueDate.getFullYear();

            ////                if (_newDueDay < 10) { _newDueDay = '0' + _newDueDay }
            ////                if (_newDueMonth < 10) { _newDueMonth = '0' + _newDueMonth }

            ////                txtDueDate.value = _newDueDay + '/' + _newDueMonth + '/' + _newDueYear;

            ////            }


            //================================================================//

            //==================Calculation of Amount=========================//

            if (parseFloat(txtNegotiatedAmt.value) == parseFloat(0)) {
                txtNegotiatedAmt.value = txtBillAmount.value;
            }

            if (chkLoanAdv.checked == true) {
                if (parseFloat(txtBillAmount.value) != 0 && parseFloat(txtExchRate.value) != 0 && (txtNoOfDays.value != 0 || txtNoOfDays.value != "")) {

                    txtBillAmountinRS.value = (parseFloat(txtBillAmount.value) * parseFloat(txtExchRate.value));
                    txtNegotiatedAmtinRS.value = (parseFloat(txtNegotiatedAmt.value) * parseFloat(txtExchRate.value)).toFixed(2);

                    if (currValue == "INR" || rbtnSightBill.checked == true) {

                        //txtInterestinRS.value = (parseFloat(txtNegotiatedAmtinRS.value) * (parseInt(NoOfDays) / parseInt(txtOutOfDays.value)) * (parseFloat(InterestRate) + parseFloat(txtLibor.value)) / 100).toFixed(2);

                        if (currValue == "INR") {
                            txtInterestinRS.value = (parseFloat(txtNegotiatedAmtinRS.value) * (parseInt(NoOfDays) / parseInt(txtOutOfDays.value)) * (parseFloat(InterestRate) + parseFloat(txtLibor.value)) / 100).toFixed(0);
                        }
                        else {
                            txtInterestinRS.value = (parseFloat(txtNegotiatedAmtinRS.value) * (parseInt(NoOfDays) / parseInt(txtOutOfDays.value)) * (parseFloat(InterestRate) + parseFloat(txtLibor.value)) / 100).toFixed(2);
                        }

                        if (isNaN(txtInterestinRS.value))
                            txtInterestinRS.value = 0;

                        //gbase
                        //txtInterest.value = (parseFloat(txtInterestinRS.value) / parseFloat(txtExchRate.value)).toFixed(2);
                        if (currValue == "INR") {
                            txtInterest.value = (parseFloat(txtInterestinRS.value) / parseFloat(txtExchRate.value)).toFixed(0);
                        }
                        else {
                            txtInterest.value = (parseFloat(txtInterestinRS.value) / parseFloat(txtExchRate.value)).toFixed(2);
                        }

                        txtNetAmtinRS.value = (parseFloat(txtNegotiatedAmtinRS.value) - parseFloat(txtInterestinRS.value)).toFixed(2);

                        txtNetAmt.value = (parseFloat(txtNetAmtinRS.value) / parseFloat(txtExchRate.value)).toFixed(2);

                    }
                    else {

                        txtInterest.value = (parseFloat(txtNegotiatedAmt.value) * (parseInt(NoOfDays) / parseInt(txtOutOfDays.value)) * (parseFloat(InterestRate) + parseFloat(txtLibor.value)) / 100).toFixed(2);

                        txtInterestinRS.value = (parseFloat(txtInterest.value) * parseFloat(txtExchRate.value)).toFixed(2);

                        txtNetAmt.value = (parseFloat(txtNegotiatedAmt.value) - parseFloat(txtInterest.value)).toFixed(2);

                        txtNetAmtinRS.value = (parseFloat(txtNetAmt.value) * parseFloat(txtExchRate.value)).toFixed(2);

                    }
                }
            }

            //================================================================//

            //=====================Calculate Service Tax======================//


            if (txtSTaxAmount.value != '')
                _sTaxAmt = parseFloat(txtSTaxAmount.value);
            if (sTaxValue != '')
                _sTaxValue = parseFloat(sTaxValue);

            if (txtBankCert.value != '')
                _bankCert = parseFloat(txtBankCert.value);
            if (txtNegotiationFees.value != '')
                _NegoFees = parseFloat(txtNegotiationFees.value);
            if (txtCourierChrgs.value != '')
                _CourierChrgs = parseFloat(txtCourierChrgs.value);
            if (txtOtherChrgs.value != '')
                _OtherCharges = parseFloat(txtOtherChrgs.value);

            if (txtCommissionID.value == '') {
                if (txtCommission.value == '' || parseFloat(txtCommission.value) == 0) {

                    _Commission = parseFloat(txtNegotiatedAmtinRS.value) * (parseFloat(hdnCommRate.value) / 100);
                    if (isNaN(_Commission))
                        _Commission = 0;

                    if (parseFloat(_Commission) < parseFloat(hdnCommMinAmt.value))
                        _Commission = hdnCommMinAmt.value;
                    txtCommission.value = parseFloat(_Commission);
                }
            }
            else {
                if (hdnCommRate.value != 0 && parseFloat(hdnCommMinAmt.value) == 0 && parseFloat(hdnCommMaxAmt.value) == 0) {
                    _Commission = parseFloat(txtNegotiatedAmtinRS.value) * (parseFloat(hdnCommRate.value) / 100);
                    if (isNaN(_Commission)) {

                        _Commission = 0;
                    }
                    else {
                        txtCommission.value = parseFloat(_Commission);
                    }
                }
                else if (parseFloat(hdnCommRate.value) != 0 && parseFloat(hdnCommMinAmt.value) != 0 && parseFloat(hdnCommMaxAmt.value) != 0) {
                    _Commission = parseFloat(txtNegotiatedAmtinRS.value) * (parseFloat(hdnCommRate.value) / 100);
                    if (parseFloat(_Commission) < parseFloat(hdnCommMinAmt.value)) {
                        _Commission = hdnCommMinAmt.value;
                        txtCommission.value = parseFloat(_Commission);
                    }
                    else {
                        if (parseFloat(_Commission) > parseFloat(hdnCommMinAmt.value) && parseFloat(_Commission) < parseFloat(hdnCommMaxAmt.value)) {

                            // _Commission = hdnCommMaxAmt.value;
                            txtCommission.value = parseFloat(_Commission);
                        }
                        else {

                            _Commission = hdnCommMaxAmt.value;
                            txtCommission.value = parseFloat(_Commission);
                        }
                    }
                }
                else if (parseFloat(hdnCommRate.value) == 0 && parseFloat(hdnCommMinAmt.value) == 0 && parseFloat(hdnCommMaxAmt.value) == 0 && parseFloat(hdnCommFlat.value) != 0) {
                    _Commission = hdnCommFlat.value;
                    txtCommission.value = parseFloat(_Commission);
                }

            }

            //////            var _serviceTax = 0;
            //////            var _serviceTax_EDUCess = 0;
            //////            var _serviceTax_SecEDUCess = 0;

            ////////////////New Changes////////////////////

            var SBcess = $get("TabContainerMain_tbTransactionDetails_txtsbcess");
            if (SBcess.value == '')
                SBcess.value = 0;

            var SBcessamt = $get("TabContainerMain_tbTransactionDetails_txtSBcesssamt");
            if (SBcessamt.value == '')
                SBcessamt.value = 0;

            var KKCess = $get("TabContainerMain_tbTransactionDetails_txt_kkcessper");
            if (KKCess.value == '')
                KKCess.value = 0;

            var KKcessamt = $get("TabContainerMain_tbTransactionDetails_txt_kkcessamt");
            if (KKcessamt.value == '')
                KKcessamt.value = 0;


            var STTamt = $get("TabContainerMain_tbTransactionDetails_txtsttamt");

            _serviceTax = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(hdnServiceTax.value) / 100)
            _serviceTax = Math.round(_serviceTax);
            _serviceTax = parseFloat(_serviceTax).toFixed(2);


            if (SBcess.value != '') {

                a = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(SBcess.value) / 100)
                SBcessamt.value = parseFloat(a).toFixed(2);
                var sbcess = SBcessamt.value;
                //STTamt.value = parseFloat(a + _serviceTax).toFixed(2);
            }

            else {

                //STTamt.value = parseFloat(_serviceTax).toFixed(2);

                if (SBcessamt.value == '')
                    SBcessamt.value = 0;
            }

            if (KKCess.value != '') {

                a = (parseFloat(_bankCert) + parseFloat(_NegoFees) + parseFloat(_CourierChrgs) + parseFloat(txtCommission.value) + parseFloat(txtOtherChrgs.value)) * (parseFloat(KKCess.value) / 100)
                KKcessamt.value = parseFloat(a).toFixed(2);
                var kkcess = KKcessamt.value;
                //STTamt.value = parseFloat(a + _serviceTax).toFixed(2);
            }

            else {

                KKcessamt.value = 0;
            }

            _serviceTax_EDUCess = _serviceTax * (parseFloat(hdnEDU_CESS.value) / 100);
            _serviceTax_SecEDUCess = _serviceTax * (parseFloat(hdnSEC_EDU_CESS.value) / 100);

            _sTaxAmt = parseFloat(_serviceTax) + parseFloat(_serviceTax_EDUCess) + parseFloat(_serviceTax_SecEDUCess);

            if (_sTaxAmt.value == '')
                _sTaxAmt.value = 0;

            txtSTaxAmount.value = _sTaxAmt.toFixed(2);


            ////total stax amount///////

            STTamt.value = parseFloat(parseFloat(_serviceTax) + parseFloat(sbcess) + parseFloat(kkcess)).toFixed(2);



            if (txtDocType.value == "B")
                txtCurrentAcinRS.value = (parseFloat(txtNegotiatedAmtinRS.value) - (parseFloat(txtOtherChrgs.value) + parseFloat(txtBankCert.value) + parseFloat(txtNegotiationFees.value) + parseFloat(txtCourierChrgs.value) + parseFloat(STTamt.value) + parseFloat(txtMarginAmt.value) + parseFloat(txtSTFXDLS.value) + parseFloat(txtCommission.value))).toFixed(2);
            else if (txtDocType.value == "E" || (txtDocType.value == "C" && rbtnEBR.checked == true))
                txtCurrentAcinRS.value = ((parseFloat(txtNetAmt.value) * parseFloat(txtExchRtEBR.value)) - (parseFloat(txtOtherChrgs.value) + parseFloat(txtBankCert.value) + parseFloat(txtNegotiationFees.value) + parseFloat(txtCourierChrgs.value) + parseFloat(STTamt.value) + parseFloat(txtMarginAmt.value) + parseFloat(txtSTFXDLS.value) + parseFloat(txtCommission.value))).toFixed(2);
            else
                txtCurrentAcinRS.value = ((parseFloat(txtNetAmt.value) * parseFloat(txtExchRate.value)) - (parseFloat(txtOtherChrgs.value) + parseFloat(txtBankCert.value) + parseFloat(txtNegotiationFees.value) + parseFloat(txtCourierChrgs.value) + parseFloat(STTamt.value) + parseFloat(txtMarginAmt.value) + parseFloat(txtSTFXDLS.value) + parseFloat(txtCommission.value))).toFixed(2);

            if (txtCurrentAcinRS.value == '')
                txtCurrentAcinRS.value = 0;

            txtCurrentAcinRS.value = parseFloat(txtCurrentAcinRS.value).toFixed(2);

            //================================================================//

            //=====================Format All Fields==========================//

            if (txtExchRate.value == '')
                txtExchRate.value = 0;
            txtExchRate.value = parseFloat(txtExchRate.value).toFixed(10);

            if (txtLibor.value == '')
                txtLibor.value = 0;
            txtLibor.value = parseFloat(txtLibor.value).toFixed(6);


            if (txtIntRate1.value == '')
                txtIntRate1.value = 0.00;
            //txtIntRate1.value = parseFloat(txtIntRate1.value).toFixed(2);
            txtIntRate1.value = parseFloat(txtIntRate1.value);



            if (txtIntRate2.value == '')
                txtIntRate2.value = 0;
            txtIntRate2.value = parseFloat(txtIntRate2.value).toFixed(2);


            if (txtIntRate3.value == '')
                txtIntRate3.value = 0;
            txtIntRate3.value = parseFloat(txtIntRate3.value).toFixed(2);


            if (txtIntRate4.value == '')
                txtIntRate4.value = 0;
            txtIntRate4.value = parseFloat(txtIntRate4.value).toFixed(2);


            if (txtIntRate5.value == '')
                txtIntRate5.value = 0;
            txtIntRate5.value = parseFloat(txtIntRate5.value).toFixed(2);


            if (txtIntRate6.value == '')
                txtIntRate6.value = 0;
            txtIntRate6.value = parseFloat(txtIntRate6.value).toFixed(2);


            if (txtBillAmount.value == '')
                txtBillAmount.value = 0;
            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);

            if (txtBillAmountinRS.value == '')
                txtBillAmountinRS.value = 0;
            txtBillAmountinRS.value = parseFloat(txtBillAmountinRS.value).toFixed(2);

            if (txtNegotiatedAmt.value == '')
                txtNegotiatedAmt.value = 0;
            txtNegotiatedAmt.value = parseFloat(txtNegotiatedAmt.value).toFixed(2);

            if (txtNegotiatedAmtinRS.value == '')
                txtNegotiatedAmtinRS.value = 0;
            txtNegotiatedAmtinRS.value = parseFloat(txtNegotiatedAmtinRS.value).toFixed(2);

            if (txtInterest.value == '')
                txtInterest.value = 0;

            if (currValue == "INR") {
                txtInterest.value = parseFloat(txtInterest.value).toFixed(0);
            }
            else {
                txtInterest.value = parseFloat(txtInterest.value).toFixed(2);
            }
            //txtInterest.value = parseFloat(txtInterest.value).toFixed(2);

            if (txtInterestinRS.value == '')
                txtInterestinRS.value = 0;

            if (currValue == "INR") {
                txtInterestinRS.value = parseFloat(txtInterestinRS.value).toFixed(0);
            }
            else {
                txtInterestinRS.value = parseFloat(txtInterestinRS.value).toFixed(2);
            }
            //txtInterestinRS.value = parseFloat(txtInterestinRS.value).toFixed(2);

            if (txtNetAmt.value == '')
                txtNetAmt.value = 0;
            txtNetAmt.value = parseFloat(txtNetAmt.value).toFixed(2);




            if (txtOtherChrgs.value == '')
                txtOtherChrgs.value = 0;
            txtOtherChrgs.value = parseFloat(txtOtherChrgs.value).toFixed(2);

            if (txtBankCert.value == '')
                txtBankCert.value = 0;
            txtBankCert.value = parseFloat(txtBankCert.value).toFixed(2);

            if (txtNegotiationFees.value == '')
                txtNegotiationFees.value = 0;
            txtNegotiationFees.value = parseFloat(txtNegotiationFees.value).toFixed(2);

            if (txtCourierChrgs.value == '')
                txtCourierChrgs.value = 0;
            txtCourierChrgs.value = parseFloat(txtCourierChrgs.value).toFixed(2);

            if (txtMarginAmt.value == '')
                txtMarginAmt.value = 0;
            txtMarginAmt.value = parseFloat(txtMarginAmt.value).toFixed(2);

            if (txtCommission.value == '')
                txtCommission.value = 0;
            txtCommission.value = parseFloat(txtCommission.value).toFixed(2);

            if (txtSTaxAmount.value == '')
                txtSTaxAmount.value = 0;
            txtSTaxAmount.value = parseFloat(txtSTaxAmount.value).toFixed(2);

            if (txtSTFXDLS.value == '')
                txtSTFXDLS.value = 0;
            txtSTFXDLS.value = parseFloat(txtSTFXDLS.value).toFixed(2);


            //================================================================//


            return true;
        }

        function CalculateDueDate() {

            var txtDateRcvd = $get("TabContainerMain_tbDocumentDetails_txtDateRcvd");
            var txtDateNegotiated = $get("TabContainerMain_tbDocumentDetails_txtDateNegotiated");
            var txtAWBDate = $get("TabContainerMain_tbDocumentDetails_txtAWBDate");
            var txtDueDate = $get("TabContainerMain_tbTransactionDetails_txtDueDate");
            var _txtBEDate = $get("TabContainerMain_tbDocumentDetails_txtBEDate");
            var txtInvoiceDate = $get("TabContainerMain_tbDocumentDetails_txtInvoiceDate");
            var hdnTempDueDate = $get("hdnTempDueDate");
            var rbtnSightBill = $get("rbtnSightBill");

            var rbtnUsanceBill = $get("rbtnUsanceBill");

            var rbtnAfterAWB = $get("TabContainerMain_tbTransactionDetails_rbtnAfterAWB");
            var rbtnFromAWB = $get("TabContainerMain_tbTransactionDetails_rbtnFromAWB");
            var rbtnSight = $get("TabContainerMain_tbTransactionDetails_rbtnSight");
            var rbtnDA = $get("TabContainerMain_tbTransactionDetails_rbtnDA");
            var rbtnFromInvoice = $get("TabContainerMain_tbTransactionDetails_rbtnFromInvoice");
            var rbtnOthers = $get("TabContainerMain_tbTransactionDetails_rbtnOthers");
            var txtNoOfDays = $get("TabContainerMain_tbTransactionDetails_txtNoOfDays");
            if (txtNoOfDays.value == '')
                txtNoOfDays.value = 0;

            if (txtDateNegotiated.value == "")
                txtDateNegotiated.value = txtDateRcvd.value;

            var _NegoDate = txtDateNegotiated.value.split("/");

            var nDay = _NegoDate[0];
            var nMonth = _NegoDate[1];
            var nYear = _NegoDate[2];

            var _dueDate = new Date();
            var _AWBDate = txtAWBDate.value.split("/");

            var awbDay = _AWBDate[0];
            var awbMonth = _AWBDate[1];
            var awbYear = _AWBDate[2];

            var _BEDate = _txtBEDate.value.split("/");

            var beDay = _BEDate[0];
            var beMonth = _BEDate[1];
            var beYear = _BEDate[2];

            var _InvDate = txtInvoiceDate.value.split("/");

            var invDay = _InvDate[0];
            var invMonth = _InvDate[1];
            var invYear = _InvDate[2];
            debugger;
            //alert(txtDueDate.value);
            if (rbtnOthers.checked == false) {

                if (rbtnSightBill.checked == true) {
                    _dueDate.setFullYear(nYear, nMonth - 1, nDay);
                    _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));

                }
                if (_txtBEDate.value != "") {
                    if (rbtnDA.checked == true) {
                        _dueDate.setFullYear(beYear, beMonth - 1, beDay);
                        _dueDate.setDate(_dueDate.getDate() + (parseInt(txtNoOfDays.value) + 1));
                    }
                }
                else {
                    if (txtAWBDate.value != "") {
                        if (rbtnAfterAWB.checked == true) {
                            _dueDate.setFullYear(awbYear, awbMonth - 1, awbDay);
                            _dueDate.setDate(_dueDate.getDate() + (parseInt(txtNoOfDays.value) + 1));
                        }
                        else if (rbtnFromAWB.checked == true) {
                            _dueDate.setFullYear(awbYear, awbMonth - 1, awbDay);
                            _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));
                        }
                        else if (rbtnSight.checked == true || rbtnDA.checked == true) {
                            _dueDate.setFullYear(nYear, nMonth - 1, nDay);
                            _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));
                        }
                    }
                    if (txtInvoiceDate.value != "") {
                        if (rbtnFromInvoice.checked == true) {
                            _dueDate.setFullYear(invYear, invMonth - 1, invDay);
                            _dueDate.setDate(_dueDate.getDate() + parseInt(txtNoOfDays.value));
                        }
                    }
                }

                var _newDueDay = _dueDate.getDate();
                var _newDueMonth = _dueDate.getMonth() + 1;
                var _newDueYear = _dueDate.getFullYear();

                if (_newDueDay < 10) { _newDueDay = '0' + _newDueDay }
                if (_newDueMonth < 10) { _newDueMonth = '0' + _newDueMonth }

                txtDueDate.value = _newDueDay + '/' + _newDueMonth + '/' + _newDueYear;

            }
            Calculate();
            document.getElementById('btnalert').click();
        }

    </script>
    <script language="javascript" type="text/javascript">

        function OpenCommissionList() {

            var custacno = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
            open_popup('EXP_CommisionLookUp.aspx?custacno=' + custacno.value, 450, 650, 'CommisionList');

            return false;
        }

        function selectCommision(srNo, comRate, MinAmt, MaxAmt, FlatAmt) {
            $get('TabContainerMain_tbTransactionDetails_txtCommissionID').value = srNo;
            document.getElementById('hdnCommRate').value = comRate;
            document.getElementById('hdnCommMinAmt').value = MinAmt;
            document.getElementById('hdnCommMaxAmt').value = MaxAmt;
            document.getElementById('hdnCommFlat').value = FlatAmt;

            Calculate();
        }

        function calculateAmountinINR_GR() {
            var txtAmountGRPP = $get("TabContainerMain_tbCoveringScheduleDetails_txtAmountGRPP");
            var txtExchRateGR = $get("TabContainerMain_tbCoveringScheduleDetails_txtExchRateGR");
            var txtAmountinINRGR = $get("TabContainerMain_tbCoveringScheduleDetails_txtAmountinINRGR");
            if (txtAmountGRPP.value == '')
                txtAmountGRPP.value = 0;
            if (txtExchRateGR.value == '')
                txtExchRateGR.value = 0;
            if (txtAmountinINRGR.value == '')
                txtAmountinINRGR.value = 0;

            txtAmountGRPP.value = parseFloat(txtAmountGRPP.value).toFixed(2);
            txtExchRateGR.value = parseFloat(txtExchRateGR.value).toFixed(10);
            txtAmountinINRGR.value = (parseFloat(txtAmountGRPP.value) * parseFloat(txtExchRateGR.value)).toFixed(2);


            return true;
        }

        //////        function CalculateTotalPC() {

        //////            var txtExchRate = $get("TabContainerMain_tbTransactionDetails_txtExchRate");
        //////            if (txtExchRate.value == '')
        //////                txtExchRate.value = 0;
        //////            txtExchRate.value = parseFloat(txtExchRate.value).toFixed(10);

        //////            var txtPCAmount1 = $get("TabContainerMain_tbTransactionDetails_txtPCAmount1");
        //////            if (txtPCAmount1.value == '')
        //////                txtPCAmount1.value = 0;
        //////            txtPCAmount1.value = parseFloat(txtPCAmount1.value).toFixed(2);

        //////            var txtPCAmount2 = $get("TabContainerMain_tbTransactionDetails_txtPCAmount2");
        //////            if (txtPCAmount2.value == '')
        //////                txtPCAmount2.value = 0;
        //////            txtPCAmount2.value = parseFloat(txtPCAmount2.value).toFixed(2);

        //////            var txtPCAmount3 = $get("TabContainerMain_tbTransactionDetails_txtPCAmount3");
        //////            if (txtPCAmount3.value == '')
        //////                txtPCAmount3.value = 0;
        //////            txtPCAmount3.value = parseFloat(txtPCAmount3.value).toFixed(2);

        //////            var txtPCAmount4 = $get("TabContainerMain_tbTransactionDetails_txtPCAmount4");
        //////            if (txtPCAmount4.value == '')
        //////                txtPCAmount4.value = 0;
        //////            txtPCAmount4.value = parseFloat(txtPCAmount4.value).toFixed(2);

        //////            var txtPCAmount5 = $get("TabContainerMain_tbTransactionDetails_txtPCAmount5");
        //////            if (txtPCAmount5.value == '')
        //////                txtPCAmount5.value = 0;
        //////            txtPCAmount5.value = parseFloat(txtPCAmount5.value).toFixed(2);

        //////            var txtPCAmount6 = $get("TabContainerMain_tbTransactionDetails_txtPCAmount6");
        //////            if (txtPCAmount6.value == '')
        //////                txtPCAmount6.value = 0;
        //////            txtPCAmount6.value = parseFloat(txtPCAmount6.value).toFixed(2);

        //////            var txtPCAcNo1 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo1");
        //////            var txtPCAcNo2 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo2");
        //////            var txtPCAcNo3 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo3");
        //////            var txtPCAcNo4 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo4");
        //////            var txtPCAcNo5 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo5");
        //////            var txtPCAcNo6 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo6");


        //////            var txtPCAmtinINR1 = $get("TabContainerMain_tbTransactionDetails_txtPCAmtinINR1");
        //////            if (txtPCAmtinINR1.value == '')
        //////                txtPCAmtinINR1.value = 0;
        //////            txtPCAmtinINR1.value = parseFloat(txtPCAmtinINR1.value).toFixed(2);

        //////            var txtPCAmtinINR2 = $get("TabContainerMain_tbTransactionDetails_txtPCAmtinINR2");
        //////            if (txtPCAmtinINR2.value == '')
        //////                txtPCAmtinINR2.value = 0;
        //////            txtPCAmtinINR2.value = parseFloat(txtPCAmtinINR2.value).toFixed(2);

        //////            var txtPCAmtinINR3 = $get("TabContainerMain_tbTransactionDetails_txtPCAmtinINR3");
        //////            if (txtPCAmtinINR3.value == '')
        //////                txtPCAmtinINR3.value = 0;
        //////            txtPCAmtinINR3.value = parseFloat(txtPCAmtinINR3.value).toFixed(2);

        //////            var txtPCAmtinINR4 = $get("TabContainerMain_tbTransactionDetails_txtPCAmtinINR4");
        //////            if (txtPCAmtinINR4.value == '')
        //////                txtPCAmtinINR4.value = 0;
        //////            txtPCAmtinINR4.value = parseFloat(txtPCAmtinINR4.value).toFixed(2);

        //////            var txtPCAmtinINR5 = $get("TabContainerMain_tbTransactionDetails_txtPCAmtinINR5");
        //////            if (txtPCAmtinINR5.value == '')
        //////                txtPCAmtinINR5.value = 0;
        //////            txtPCAmtinINR5.value = parseFloat(txtPCAmtinINR5.value).toFixed(2);

        //////            var txtPCAmtinINR6 = $get("TabContainerMain_tbTransactionDetails_txtPCAmtinINR6");
        //////            if (txtPCAmtinINR6.value == '')
        //////                txtPCAmtinINR6.value = 0;
        //////            txtPCAmtinINR6.value = parseFloat(txtPCAmtinINR6.value).toFixed(2);

        //////            var txtTotalPCLiquidated = $get("TabContainerMain_tbTransactionDetails_txtTotalPCLiquidated");
        //////            txtTotalPCLiquidated.value = 0;
        //////            txtTotalPCLiquidated.value = parseFloat(txtTotalPCLiquidated.value).toFixed(2);

        //////            var hdnPCbalance1 = document.getElementById('hdnPCbalance1');
        //////            var hdnPCbalance2 = document.getElementById('hdnPCbalance2');
        //////            var hdnPCbalance3 = document.getElementById('hdnPCbalance3');
        //////            var hdnPCbalance4 = document.getElementById('hdnPCbalance4');
        //////            var hdnPCbalance5 = document.getElementById('hdnPCbalance5');
        //////            var hdnPCbalance6 = document.getElementById('hdnPCbalance6');

        //////            if (txtPCAcNo1.value != '' && hdnPCbalance1.value != '') {
        //////                if (parseFloat(hdnPCbalance1.value) < parseFloat(txtPCAmount1.value)) {
        //////                    alert('PC Amount cannot be greater than balance amount.');
        //////                    //  txtPCAmount1.value = '';
        //////                    return false;
        //////                }
        //////            }
        //////            if (txtPCAcNo2.value != '' && hdnPCbalance2.value != '') {
        //////                if (parseFloat(hdnPCbalance2.value) < parseFloat(txtPCAmount2.value)) {
        //////                    alert('PC Amount cannot be greater than balance amount.');
        //////                    //   txtPCAmount2.value = '';
        //////                    return false;
        //////                }
        //////            }
        //////            if (txtPCAcNo3.value != '' && hdnPCbalance3.value != '') {
        //////                if (parseFloat(hdnPCbalance3.value) < parseFloat(txtPCAmount3.value)) {
        //////                    alert('PC Amount cannot be greater than balance amount.');
        //////                    //   txtPCAmount3.value = '';
        //////                    return false;
        //////                }
        //////            }
        //////            if (txtPCAcNo4.value != '' && hdnPCbalance4.value != '') {
        //////                if (parseFloat(hdnPCbalance4.value) < parseFloat(txtPCAmount4.value)) {
        //////                    alert('PC Amount cannot be greater than balance amount.');
        //////                    //   txtPCAmount4.value = '';
        //////                    return false;
        //////                }
        //////            }
        //////            if (txtPCAcNo5.value != '' && hdnPCbalance5.value != '') {
        //////                if (parseFloat(hdnPCbalance5.value) < parseFloat(txtPCAmount5.value)) {
        //////                    alert('PC Amount cannot be greater than balance amount.');
        //////                    //   txtPCAmount5.value = '';
        //////                    return false;
        //////                }
        //////            }
        //////            if (txtPCAcNo6.value != '' && hdnPCbalance6.value != '') {
        //////                if (parseFloat(hdnPCbalance6.value) < parseFloat(txtPCAmount6.value)) {
        //////                    alert('PC Amount cannot be greater than balance amount.');
        //////                    //   txtPCAmount6.value = '';
        //////                    return false;
        //////                }
        //////            }

        //////            if (txtPCAcNo1.value != '') {
        //////                txtPCAmtinINR1.value = (parseFloat(txtPCAmount1.value) * parseFloat(txtExchRate.value)).toFixed(2);
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount1.value)).toFixed(2);
        //////            }
        //////            else {
        //////                txtPCAmtinINR1.value = 0;
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount1.value)).toFixed(2);
        //////            }
        //////            if (txtPCAcNo2.value != '') {
        //////                txtPCAmtinINR2.value = (parseFloat(txtPCAmount2.value) * parseFloat(txtExchRate.value)).toFixed(2);
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount2.value)).toFixed(2);
        //////            }
        //////            else {
        //////                txtPCAmtinINR2.value = 0;
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount2.value)).toFixed(2);
        //////            }
        //////            if (txtPCAcNo3.value != '') {
        //////                txtPCAmtinINR3.value = (parseFloat(txtPCAmount3.value) * parseFloat(txtExchRate.value)).toFixed(2);
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount3.value)).toFixed(2);
        //////            }
        //////            else {
        //////                txtPCAmtinINR3.value = 0;
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount3.value)).toFixed(2);
        //////            }
        //////            if (txtPCAcNo4.value != '') {
        //////                txtPCAmtinINR4.value = (parseFloat(txtPCAmount4.value) * parseFloat(txtExchRate.value)).toFixed(2);
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount4.value)).toFixed(2);
        //////            }
        //////            else {
        //////                txtPCAmtinINR4.value = 0;
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount4.value)).toFixed(2);
        //////            }
        //////            if (txtPCAcNo5.value != '') {
        //////                txtPCAmtinINR5.value = (parseFloat(txtPCAmount5.value) * parseFloat(txtExchRate.value)).toFixed(2);
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount5.value)).toFixed(2);
        //////            }
        //////            else {
        //////                txtPCAmtinINR5.value = 0;
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount5.value)).toFixed(2);
        //////            }
        //////            if (txtPCAcNo6.value != '') {
        //////                txtPCAmtinINR6.value = (parseFloat(txtPCAmount6.value) * parseFloat(txtExchRate.value)).toFixed(2);
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount6.value)).toFixed(2);
        //////            }
        //////            else {
        //////                txtPCAmtinINR6.value = 0;
        //////                txtTotalPCLiquidated.value = (parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmount6.value)).toFixed(2);
        //////            }

        //////            Calculate();
        //////            return true;
        //////        }
    </script>
    <script language="javascript" type="text/javascript">

        function checkSysDate(controlID) {

            var obj = controlID;

            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];

                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];


                var dt = new Date(year, month - 1, day);
                var today = new Date();

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert("Invalid Date");
                    setTimeout(function () {
                        controlID.focus();
                    }, 0);
                    return false;
                }
            }
        }

        function checkValidateDate(controlID) {

            var obj = controlID;

            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];

                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];


                var dt = new Date(year, month - 1, day);
                var today = new Date();

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert("Invalid Date");
                    setTimeout(function () {
                        controlID.focus();
                    }, 0);
                    return false;
                }
            }
        }

        function checkAWBDate(controlID) {

            var obj = controlID;

            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];

                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];


                var dt = new Date(year, month - 1, day);
                var today = new Date();

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert("Invalid Date");
                    setTimeout(function () {
                        controlID.focus();
                    }, 0);
                    return false;
                }
                else {
                    // Here are the two dates to compare
                    var date1 = year + '-' + (month - 1) + '-' + day;
                    var date2 = today.getFullYear() + '-' + today.getMonth() + '-' + today.getDate();

                    // First we split the values to arrays date1[0] is the year, [1] the month and [2] the day
                    date1 = date1.split('-');
                    date2 = date2.split('-');

                    // Now we convert the array to a Date object, which has several helpful methods
                    date1 = new Date(date1[0], date1[1], date1[2]);
                    date2 = new Date(date2[0], date2[1], date2[2]);

                    // We use the getTime() method and get the unixtime (in milliseconds, but we want seconds, therefore we divide it through 1000)
                    date1_unixtime = parseInt(date1.getTime() / 1000);
                    date2_unixtime = parseInt(date2.getTime() / 1000);

                    // This is the calculated difference in seconds
                    var timeDifference = date2_unixtime - date1_unixtime;

                    // in Hours
                    var timeDifferenceInHours = timeDifference / 60 / 60;

                    // and finaly, in days :)
                    var timeDifferenceInDays = timeDifferenceInHours / 24;

                    if (parseFloat(timeDifferenceInDays) > 21) {
                        alert('Documents Submitted beyond 21 days of shipment date.');
                    }
                    //                    CalculateDueDate();
                }
            }
        }

        //--------------Rem on 07/11/2013 - to check for invalid dates
        function isValidDate(controlID, CName) {
            var obj = controlID;
            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];
                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];
                var today = new Date();

                if (day == "__") {
                    day = today.getDay();
                }
                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1, day);

                //              if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                if (CName == "Due Date" || CName == "Accepted Due Date" || CName == "ENC Date") {
                    if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                        alert('Invalid ' + CName);
                        setTimeout(function () {
                            controlID.focus();
                        }, 0);
                        return false;
                    }
                }
                else {
                    if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                        alert('Invalid ' + CName);
                        setTimeout(function () {
                            controlID.focus();
                        }, 0);
                        return false;
                    }
                }
            }
        }

    </script>
    <script language="javascript" type="text/javascript">
        function OpenSubACList(hNo) {
            var hdnBranchName = document.getElementById('hdnBranchName');
            var custID = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
            open_popup('EXP_SubAccountNo.aspx?custId=' + custID.value + '&hNo=' + hNo + '&branch=' + hdnBranchName.value, 450, 450, 'SubAcList');
            return false;
        }
        function selectSubAc1(sAcNo, AcNo, balance) {
            var txtPCAcNo1 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo1");
            var txtPCsubAcNo1 = $get("TabContainerMain_tbTransactionDetails_txtPCsubAcNo1");
            var hdnPCbalance1 = document.getElementById('hdnPCbalance1');
            txtPCAcNo1.value = AcNo;
            txtPCsubAcNo1.value = sAcNo;
            hdnPCbalance1.value = balance;
        }
        function selectSubAc2(sAcNo, AcNo, balance) {
            var txtPCAcNo2 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo2");
            var txtPCsubAcNo2 = $get("TabContainerMain_tbTransactionDetails_txtPCsubAcNo2");
            var hdnPCbalance2 = document.getElementById('hdnPCbalance2');
            txtPCAcNo2.value = AcNo;
            txtPCsubAcNo2.value = sAcNo;
            hdnPCbalance2.value = balance;
        }
        function selectSubAc3(sAcNo, AcNo, balance) {
            var txtPCAcNo3 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo3");
            var txtPCsubAcNo3 = $get("TabContainerMain_tbTransactionDetails_txtPCsubAcNo3");
            var hdnPCbalance3 = document.getElementById('hdnPCbalance3');
            txtPCAcNo3.value = AcNo;
            txtPCsubAcNo3.value = sAcNo;
            hdnPCbalance3.value = balance;
        }
        function selectSubAc4(sAcNo, AcNo, balance) {
            var txtPCAcNo4 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo4");
            var txtPCsubAcNo4 = $get("TabContainerMain_tbTransactionDetails_txtPCsubAcNo4");
            var hdnPCbalance4 = document.getElementById('hdnPCbalance4');
            txtPCAcNo4.value = AcNo;
            txtPCsubAcNo4.value = sAcNo;
            hdnPCbalance4.value = balance;
        }
        function selectSubAc5(sAcNo, AcNo, balance) {
            var txtPCAcNo5 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo5");
            var txtPCsubAcNo5 = $get("TabContainerMain_tbTransactionDetails_txtPCsubAcNo5");
            var hdnPCbalance5 = document.getElementById('hdnPCbalance5');
            txtPCAcNo5.value = AcNo;
            txtPCsubAcNo5.value = sAcNo;
            hdnPCbalance5.value = balance;
        }
        function selectSubAc6(sAcNo, AcNo, balance) {
            var txtPCAcNo6 = $get("TabContainerMain_tbTransactionDetails_txtPCAcNo6");
            var txtPCsubAcNo6 = $get("TabContainerMain_tbTransactionDetails_txtPCsubAcNo6");
            var hdnPCbalance6 = document.getElementById('hdnPCbalance6');
            txtPCAcNo6.value = AcNo;
            txtPCsubAcNo6.value = sAcNo;
            hdnPCbalance6.value = balance;
        }

        function OpenCopyFromDocNoList(e) {

            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {

                var branchCode = document.getElementById('hdnBranchCode');
                var custAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");

                if (custAcNo.value == '') {
                    alert('Enter Customer A/C No.');
                    custAcNo.focus();
                    return false;
                }

                open_popup('EXP_DocumentNoLookUpforCopy.aspx?custAcNo=' + custAcNo.value + '&bcode=' + branchCode.value, 450, 350, 'DocNoList');
                return false;
            }
        }

        function selectDocNoCopy(selectedID) {
            var id = selectedID;
            document.getElementById('txtCopyFromDocNo').value = id;
            document.getElementById('btnCopy').focus();
        }

//        function radioButtonChanged() {
//            var rbtnAfterAWB = $get("TabContainerMain_tbTransactionDetails_rbtnAfterAWB");
//            var rbtnFromAWB = $get("TabContainerMain_tbTransactionDetails_rbtnFromAWB");
//            var rbtnSight = $get("TabContainerMain_tbTransactionDetails_rbtnSight");
//            var rbtnDA = $get("TabContainerMain_tbTransactionDetails_rbtnDA");
//            var rbtnFromInvoice = $get("TabContainerMain_tbTransactionDetails_rbtnFromInvoice");
//            var rbtnOthers = $get("TabContainerMain_tbTransactionDetails_rbtnOthers");

//            var txtOtherTenorRemarks = $get("TabContainerMain_tbTransactionDetails_txtOtherTenorRemarks");

//            if (rbtnAfterAWB.checked == true) {
//                txtOtherTenorRemarks.value = 'Days After AWB/BL Date';
//            }
//            else if (rbtnFromAWB.checked == true) {
//                txtOtherTenorRemarks.value = 'Days From AWB/BL Date';
//            }
//            else if (rbtnSight.checked == true) {
//                txtOtherTenorRemarks.value = 'Days Sight';
//            }
//            else if (rbtnDA.checked == true) {
//                txtOtherTenorRemarks.value = 'From Draft/BOE Date';
//            }
//            else if (rbtnFromInvoice.checked == true) {
//                txtOtherTenorRemarks.value = 'Days From Invoice Date';
//            }
//            else if (rbtnOthers.checked == true) {
//                txtOtherTenorRemarks.value = '';
//            }

//            Calculate();
//        }

        function FillDraft() {
            var BENo = $get("TabContainerMain_tbDocumentDetails_txtBENo");
            var DraftNo = $get("TabContainerMain_tbGBaseDetails_txtDraftNo");
            var InvoiceNo = $get("TabContainerMain_tbDocumentDetails_txtInvoiceNo");
            // Added by bhupen 23032023
            if (BENo.value != '') {
                DraftNo.value = BENo.value;
            }
            else {
                DraftNo.value = InvoiceNo.value;
            }
        }

        function FillGBaseDetailsBYCurrency() {
            var DocType = $get("txtDocType");
            var ForeignLocal = $get("hdnForeignLocal");

            var Currency = $get("TabContainerMain_tbTransactionDetails_ddlCurrency");
            var CurrencyText = Currency.options[Currency.selectedIndex].text;
            var CurrencyValue = Currency.options[Currency.selectedIndex].value;


            var CRCurr = $get("TabContainerMain_tbGBaseDetails_txtCRCurr");
            var CRIntCurr = $get("TabContainerMain_tbGBaseDetails_txtCRIntCurr");
            var CRHandlingCommCurr = $get("TabContainerMain_tbGBaseDetails_txtCRHandlingCommCurr");
            var CRPostageCurr = $get("TabContainerMain_tbGBaseDetails_txtCRPostageCurr");

            var DRCurr = $get("TabContainerMain_tbGBaseDetails_txtDRCurr");
            var DRCurr1 = $get("TabContainerMain_tbGBaseDetails_txtDRCurr1");
            var DRCurr3 = $get("TabContainerMain_tbGBaseDetails_txtDRCurr3");
            var DRCurr4 = $get("TabContainerMain_tbGBaseDetails_txtDRCurr4");

            if (DocType.value == 'BLA' || DocType.value == 'BLU') {
                if (ForeignLocal.value == 'L') {
                    CRCurr.value = CurrencyText;
                    CRIntCurr.value = CurrencyText;
                    CRHandlingCommCurr.value = CurrencyText;
                    CRPostageCurr.value = CurrencyText;

                    DRCurr.value = CurrencyText;
                    DRCurr1.value = CurrencyText;
                    DRCurr3.value = CurrencyText;
                    DRCurr4.value = CurrencyText;
                }
                else if (ForeignLocal.value == 'F') {
                    CRIntCurr.value = CurrencyText;
                    DRCurr.value = CurrencyText;
                }
            }
        }

        function CallTwoFunctionsOnBillAmt() {
            Calculate();
            FillGBaseDetailsBYNegoAmt();
        }

        function FillGBaseDetailsBYNegoAmt() {
            Calculate();

            var DocType = $get("txtDocType");
            var ForeignLocal = $get("hdnForeignLocal");

            var NegotiatedAmt = $get("TabContainerMain_tbTransactionDetails_txtNegotiatedAmt");

            var CRAmount = $get("TabContainerMain_tbGBaseDetails_txtCRAmount");

            var DRAmount = $get("TabContainerMain_tbGBaseDetails_txtDRAmount");

            if (DocType.value == 'BLA' || DocType.value == 'BLU') {
                if (ForeignLocal.value == 'L') {
                    CRAmount.value = NegotiatedAmt.value;
                    DRAmount.value = NegotiatedAmt.value;
                }
                else if (ForeignLocal.value == 'F') {
                    DRAmount.value = NegotiatedAmt.value;
                }
            }
        }

        function FillGBaseDetailsBYInterestAmt() {
            Calculate();

            var DocType = $get("txtDocType");
            var ForeignLocal = $get("hdnForeignLocal");

            var InterestAmt = $get("TabContainerMain_tbTransactionDetails_txtInterest");

            var CRIntAmount = $get("TabContainerMain_tbGBaseDetails_txtCRIntAmount");

            var DRAmount1 = $get("TabContainerMain_tbGBaseDetails_txtDRAmount1");

            if (DocType.value == 'BLA' || DocType.value == 'BLU') {
                if (ForeignLocal.value == 'L') {
                    CRIntAmount.value = parseFloat(InterestAmt.value).toFixed(0);
                    DRAmount1.value = parseFloat(InterestAmt.value).toFixed(0);
                }
                else if (ForeignLocal.value == 'F') {
                    CRIntAmount.value = InterestAmt.value;
                }
            }
        }

        function FillGBaseDetailsBYCourierChrgs() {
            Calculate();

            var DocType = $get("txtDocType");
            var ForeignLocal = $get("hdnForeignLocal");

            var CourierChrgs = $get("TabContainerMain_tbTransactionDetails_txtCourierChrgs");

            var DRAmount4 = $get("TabContainerMain_tbGBaseDetails_txtDRAmount4");

            var CRPostageAmount = $get("TabContainerMain_tbGBaseDetails_txtCRPostageAmount");

            if (DocType.value == 'BCA' || DocType.value == 'BCU') {
                DRAmount4.value = CourierChrgs.value;
                CRPostageAmount.value = CourierChrgs.value;
            }

            if (DocType.value == 'BLA' || DocType.value == 'BLU') {
                if (ForeignLocal.value == 'L') {
                    DRAmount4.value = CourierChrgs.value;
                    CRPostageAmount.value = CourierChrgs.value;
                }
                else if (ForeignLocal.value == 'F') {
                    DRAmount4.value = CourierChrgs.value;
                    CRPostageAmount.value = CourierChrgs.value;
                }
            }
        }

        function FillGBaseDetailsBYCommissionAmt() {
            Calculate();

            var DocType = $get("txtDocType");
            var ForeignLocal = $get("hdnForeignLocal");

            var Commission = $get("TabContainerMain_tbTransactionDetails_txtCommission");

            var CRHandlingCommAmount = $get("TabContainerMain_tbGBaseDetails_txtCRHandlingCommAmount");

            var DRAmount3 = $get("TabContainerMain_tbGBaseDetails_txtDRAmount3");

            if (DocType.value == 'BLA' || DocType.value == 'BLU') {
                if (ForeignLocal.value == 'L') {
                    CRHandlingCommAmount.value = Commission.value;
                    DRAmount3.value = Commission.value;
                }
                else if (ForeignLocal.value == 'F') {
                    CRHandlingCommAmount.value = Commission.value;
                    DRAmount3.value = Commission.value;
                }
            }
        }

    </script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
        <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="../Images/ajax-loader.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div>
            <center>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 25%;" valign="bottom">
                                    <span class="pageLabel" style="font-weight: bold">Export Bill Entry Details</span>
                                </td>
                                <td align="center" style="width: 50%;" valign="bottom">
                                    <asp:Label ID="lbl_dumpdate" runat="server" CssClass="elementLabelRed"></asp:Label>
                                </td>
                                <td align="right" style="width: 25%;">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                        OnClick="btnBack_Click" TabIndex="134" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top" colspan="3">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 100%" valign="top" colspan="3">
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label><br />
                                    <asp:Label ID="LEIAmtCheck" runat="server"></asp:Label>
                                    <asp:Label ID="LEIverify" runat="server"></asp:Label>
                                    <%-------------------------  hidden fields  --------------------------------%>
                                    <input type="hidden" id="hdnGridValues" runat="server" />
                                    <asp:Button ID="btnGridValues" Style="display: none;" runat="server" OnClick="btnGridValues_Click" />
                                    <input type="hidden" id="hdnOverseasId" runat="server" />
                                    <asp:Button ID="btnOverseasBank" Style="display: none;" runat="server" OnClick="btnOverseasBank_Click" />
                                    <input type="hidden" id="hdnOverseasPartyId" runat="server" />
                                    <asp:Button ID="btnOverseasParty" Style="display: none;" runat="server" OnClick="btnOverseasParty_Click" />
                                    <input type="hidden" id="hdnCustomerCode" runat="server" />
                                    <asp:Button ID="btnCustomerCode" Style="display: none;" runat="server" OnClick="btnCustomerCode_Click" />
                                    <input type="hidden" id="hdnCountry" runat="server" />
                                    <asp:Button ID="btnCountry" Style="display: none;" runat="server" OnClick="btnCountry_Click" />
                                    <input type="hidden" id="hdnDateDiff" runat="server" />
                                    <input type="hidden" id="hdnDateDiff_InvoiceDate" runat="server" />
                                    <input type="hidden" id="hdnBankCert" runat="server" />
                                    <input type="hidden" id="hdnNegoFees" runat="server" />
                                    <input type="hidden" id="hdnCourierCharges" runat="server" />
                                    <input type="hidden" id="hdnBranchCode" runat="server" />
                                    <input type="hidden" id="hdnTTRefNo" runat="server" />
                                    <input type="hidden" id="hdnTTAmount" runat="server" />
                                    <input type="hidden" id="hdnCurrentAcAmtinRS" runat="server" />
                                    <input type="hidden" id="hdnGRtotalAmount" runat="server" />
                                    <input type="hidden" id="hdnBranchName" runat="server" />
                                    <input type="hidden" id="hdnIsRealised" runat="server" />
                                    <input type="hidden" id="hdnIsDelinked" runat="server" />
                                    <input type="hidden" id="hdnIsWrittenOff" runat="server" />
                                    <input type="hidden" id="hdnIsExtended" runat="server" />
                                    <input type="hidden" id="hdnCommSrNo" runat="server" />
                                    <input type="hidden" id="hdnCommRate" runat="server" />
                                    <input type="hidden" id="hdnCommMinAmt" runat="server" />
                                    <input type="hidden" id="hdnCommMaxAmt" runat="server" />
                                    <input type="hidden" id="hdnCommFlat" runat="server" />
                                    <input type="hidden" id="hdnTTTotalAmt" runat="server" />
                                    <asp:Button ID="btnSaveTTDetails" Style="display: none;" runat="server" OnClick="btnSaveTTDetails_Click" />
                                    <asp:Button ID="btnfillDetails" Style="display: none;" runat="server" OnClick="btnfillDetails_Click" />
                                    <input type="hidden" id="hdnPortCode" runat="server" />
                                    <input type="hidden" id="hdnForeignLocal" runat="server" />
                                    <input type="hidden" id="hdnServiceTax" runat="server" />
                                    <input type="hidden" id="hdnEDU_CESS" runat="server" />
                                    <input type="hidden" id="hdnSEC_EDU_CESS" runat="server" />
                                    <asp:Button ID="btnalert" Style="display: none;" runat="server" OnClick="btnalert_Click" />
                                    <input type="hidden" id="hdnTempDueDate" runat="server" />
                                    <input type="hidden" id="hdnRole" runat="server" />
                                    <input type="hidden" id="hdnbranch" runat="server" />
                                    <%--added by Shailesh for LEI--%>
                                    <input type="hidden" id="hdnLeiFlag" runat="server" />
                                    <input type="hidden" id="hdnLeiSpecialFlag" runat="server" />
                                    <input type="hidden" id="hdnCustabbr" runat="server" />
                                    <input type="hidden" id="hdncustlei" runat="server" />
                                    <input type="hidden" id="hdncustleiexpiry" runat="server" />
                                    <input type="hidden" id="hdnoverseaslei" runat="server" />
                                    <input type="hidden" id="hdnoverseasleiexpiry" runat="server" />
                                    <input type="hidden" id="hdnbillamtinr" runat="server" />
                                    <input type="hidden" id="hdnCustname" runat="server" />
                                    <input type="hidden" id="hdnLeisavedraft" runat="server" />
                                    <asp:Label ID="lblLEI_CUST_Remark" runat="server"></asp:Label>
                                    <asp:Label ID="lblLEIExpiry_CUST_Remark" runat="server"></asp:Label>
                                    <asp:Label ID="lblLEI_Overseas_Remark" runat="server"></asp:Label>
                                    <asp:Label ID="lblLEIExpiry_Overseas_Remark" runat="server"></asp:Label>

                                    <input type="hidden" id="hdnTTFIRCTotalAmtCheck" runat="server" />
                                    <input type="hidden" id="hdnTTCurrCheck" runat="server" />

                                    <%------------------ADDDED BY NILESH 04-08-2021--------------------------%>
                                    <input type="hidden" id="hdnConsigneePartyId" runat="server" />
                                    <asp:Button ID="btnConsigneeParty" Style="display: none;" runat="server" OnClick="btnConsigneesParty_Click" />
                                     <%-- -----------Document Tab-------------------------%>
                                    <input type="hidden" id="hdnCustACNO" runat="server" />
                                    <input type="hidden" id="hdnOverseasParty" runat="server" />
                                    <input type="hidden" id="hdnConsigneeParty" runat="server" />
                                    <input type="hidden" id="hdnOverseasBankID" runat="server" />
                                    <input type="hidden" id="hdnDateReceived" runat="server" />
                                    <input type="hidden" id="hdnDRAFTBENo" runat="server" />
                                    <input type="hidden" id="hdnAWBBLNoLR" runat="server" />
                                    <input type="hidden" id="hdnAWBBLNoLRDate" runat="server" />
                                    <input type="hidden" id="hdnCheckarStatus" runat="server" />
                                    <%-------------------------------Transaction Tab-----------------------%>
                                    <input type="hidden" id="hdnCurr" runat="server" />
                                    <input type="hidden" id="hdnNoofDays" runat="server" />
                                    <input type="hidden" id="hdnTenorRemarks" runat="server" />
                                    <input type="hidden" id="hdnDueDate" runat="server" />
                                    <input type="hidden" id="hdnBillAmount" runat="server" />
                                    <input type="hidden" id="hdnIRMReferenceNo1" runat="server" />
                                    <input type="hidden" id="hdnIRMamountUtilized1" runat="server" />
                                    <input type="hidden" id="hdnIRMReferenceNo2" runat="server" />
                                    <input type="hidden" id="hdnIRMamountUtilized2" runat="server" />
                                    <input type="hidden" id="hdnEFIRCNo1" runat="server" />
                                    <input type="hidden" id="hdnEFIRCDate1" runat="server" />
                                    <input type="hidden" id="hdnEFIRCAmount1" runat="server" />
                                    <input type="hidden" id="hdnEFIRCNo2" runat="server" />
                                    <input type="hidden" id="hdnEFIRCDate2" runat="server" />
                                    <input type="hidden" id="hdnEFIRCAmount2" runat="server" />
                                    <%------------------------------------GBase Tab---------------------------%>
                                    <input type="hidden" id="hdnCountryRisk" runat="server" />
                                    <input type="hidden" id="hdnReimbursingBank" runat="server" />

                                    <%-----------------------------------Convering ScheduleGr/pp Tab--------------------%>
                                    <input type="hidden" id="hdShippingBillNo" runat="server" />
                                    <input type="hidden" id="hdnShippingBillDate" runat="server" />
                                    <input type="hidden" id="hdnPortcode1" runat="server" />
                                    <input type="hidden" id="hdnCurrency" runat="server" />
                                    <input type="hidden" id="hdnAmount" runat="server" />
                                    <input type="hidden" id="hdnType" runat="server" />
                                    <input type="hidden" id="hdnGRPPCustNo" runat="server" />
                                    <input type="hidden" id="hdnInvoiceSrNo" runat="server" />
                                    <input type="hidden" id="hdnInvoiceNo" runat="server" />
                                    <input type="hidden" id="hdnInvoiceDate" runat="server" />
                                    <input type="hidden" id="hdnInvoiceAmt" runat="server" />
                                    <input type="hidden" id="hdnFreightAmt" runat="server" />
                                    <input type="hidden" id="hdnInsuranceAmt" runat="server" />
                                    <input type="hidden" id="hdnDiscountAmt" runat="server" />
                                    <input type="hidden" id="hdnCommAmt" runat="server" />
                                    <input type="hidden" id="hdnOthDedChrgs" runat="server" />
                                    <input type="hidden" id="hdnPackingChrgs" runat="server" />
                                    <input type="hidden" id="hdnStatus" runat="server" />
                                    <input type="hidden" id="hdnFreightCurr" runat="server" />
                                    <input type="hidden" id="hdnInsCurr" runat="server" />
                                    <input type="hidden" id="hdnDisCurr" runat="server" />
                                    <input type="hidden" id="hdnCommCurr" runat="server" />
                                    <input type="hidden" id="hdnOthDedCurr" runat="server" />
                                    <input type="hidden" id="hdnPackChgsCurr" runat="server" />
                                    <input type="hidden" id="hdnExchRate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="white-space: nowrap">
                                    <span class="elementLabel">Doc Type :</span><asp:TextBox ID="txtDocType" Width="25px"
                                        runat="server" CssClass="textBox" ReadOnly="True" TabIndex="-1"></asp:TextBox>&nbsp;<asp:Label
                                            ID="lblDocumentType" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                ID="rbtnSightBill" runat="server" CssClass="elementLabel" GroupName="SU" TabIndex="56"
                                                Text="Sight Bill" Checked="True" />
                                    <asp:RadioButton ID="rbtnUsanceBill" runat="server" CssClass="elementLabel" GroupName="SU"
                                        TabIndex="56" Text="Usance Bill" />&nbsp;&nbsp;
                                </td>
                                <td align="right" style="white-space: nowrap">
                                    <asp:Label ID="lblCopyFrom" runat="server" CssClass="elementLabel" Text="Copy From : "
                                        Width="70px" Visible="false"></asp:Label><asp:TextBox ID="txtCopyFromDocNo" Width="150px"
                                            runat="server" CssClass="textBox" Visible="false" MaxLength="18"></asp:TextBox><asp:Button
                                                ID="btnDocNoListtoCopy" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                Visible="false" />&nbsp;
								<asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="buttonCopy" Width="40px"
                                    ToolTip="Copy" OnClick="btnCopy_Click" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <span class="elementLabel">RBI Doc Type : </span>
                                    <asp:TextBox ID="txtRBIDocType" Width="70px" runat="server" CssClass="textBox" ReadOnly="True"
                                        TabIndex="-1"></asp:TextBox>&nbsp;<asp:Label ID="lblRBIDocType" runat="server" CssClass="elementLabel"
                                            Text="[N/P/D/C/M]"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<span class="elementLabel">Doc No : </span>
                                    <asp:TextBox ID="txtDocumentNo" Width="140px" runat="server" CssClass="textBox" ReadOnly="True"
                                        TabIndex="-1"></asp:TextBox>&nbsp; <span id="SpanLei5" runat="server" class="mandatoryField"
                                            style="font-size: medium" visible="false">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										Note :</span>
                                    <asp:Label ID="ReccuringLEI" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="3">
                                    <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                        CssClass="ajax__tab_xp-theme" TabIndex="-1">
                                        <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="Document Details"
                                            Font-Bold="true" ForeColor="White">
                                            <ContentTemplate>
                                                <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                                    <tr>
                                                        <td align="right" style="white-space: nowrap">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Customer A/c No. :</span>
                                                        </td>
                                                        <td colspan="5" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtCustAcNo" runat="server" AutoPostBack="true" CssClass="textBox"
                                                                MaxLength="14" OnTextChanged="txtCustAcNo_TextChanged" onkeydown="OpenCustomerCodeList(this);"
                                                                TabIndex="1" Width="100px" ToolTip="Press F2 for help."></asp:TextBox>
                                                            <asp:Button ID="btnCustomerList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">Date Received:</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateRcvd" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="4" Enabled="false"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtDateRcvd" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button1" runat="server" Enabled="false" CssClass="btncalendar_enabled" TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtDateRcvd" PopupButtonID="Button1" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                ValidationGroup="dtVal" ControlToValidate="txtDateRcvd" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Overseas Party ID :</span>
                                                        </td>
                                                        <td nowrap colspan="5">
                                                            <asp:TextBox ID="txtOverseasPartyID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                                MaxLength="7" onkeydown="OpenOverseasPartyList(this);" TabIndex="2" Width="70px"
                                                                ToolTip="Press F2 for help." OnTextChanged="txtOverseasPartyID_TextChanged"></asp:TextBox>
                                                            <asp:Button ID="btnOverseasPartyList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            <asp:Label ID="lblOverseasPartyDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">E.N.C Date :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtENCdate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="5"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtENCdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button9" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtENCdate" PopupButtonID="Button9" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator14" runat="server" ControlExtender="MaskedEditExtender9"
                                                                ValidationGroup="dtVal" ControlToValidate="txtENCdate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--ADDED BY NILESH    04/08/2023--%>
                                                        <td align="right" nowrap>
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Consignee Party ID :</span>
                                                        </td>
                                                        <td nowrap colspan="4">
                                                            <asp:TextBox ID="txtconsigneePartyID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                                MaxLength="7" onkeydown="OpenConsigneePartyList(this);" TabIndex="2" Width="70px"
                                                                ToolTip="Press F2 for help." OnTextChanged="txtConsigneePartyID_TextChanged"></asp:TextBox>
                                                            <asp:Button ID="btnConsigneePartyList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            <asp:Label ID="lblConsigneePartyDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                                        </td>
                                                        <%--ADDED BY NILESH    END 04/08/2023--%>
                                                        <td>
                                                            <%-- <asp:CheckBox ID="chkCS" runat="server" CssClass="elementLabel" Text="CS" TabIndex="3" />--%>
                                                        </td>
                                                        <td align="right" width="10%" nowrap>
                                                            <span class="elementLabel">Date Negotiated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateNegotiated" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="70px" TabIndex="6"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtDateNegotiated" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button10" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtDateNegotiated" PopupButtonID="Button10" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator15" runat="server" ControlExtender="MaskedEditExtender10"
                                                                ValidationGroup="dtVal" ControlToValidate="txtDateNegotiated" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <%--ADDED BY NILESH    04/08/2023--%>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Overseas Bank ID :</span>
                                                        </td>
                                                        <td nowrap colspan="4">
                                                            <asp:TextBox ID="txtOverseasBankID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                                MaxLength="7" onkeydown="OpenOverseasBankList(this);" TabIndex="3" Width="70px"
                                                                ToolTip="Press F2 for help." OnTextChanged="txtOverseasBankID_TextChanged"></asp:TextBox>
                                                            <asp:Button ID="btnOverseasBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            <asp:Label ID="lblOverseasBankDesc" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%--ADDED BY NILESH   END 04/08/2023--%>

                                                    <tr>
                                                        <td align="right" width="15%">&nbsp;
                                                        </td>
                                                        <td width="10%" colspan="2" nowrap>
                                                            <asp:RadioButton ID="rdbOurADCode" CssClass="elementLabel" GroupName="ADCODE" runat="server"
                                                                AutoPostBack="true" OnCheckedChanged="onourADCode" />
                                                            <span class="elementLabel">Our AdCode</span>
                                                            <asp:RadioButton ID="rdbotherAdcode" CssClass="elementLabel" GroupName="ADCODE" runat="server"
                                                                AutoPostBack="true" OnCheckedChanged="onotherADCode" />
                                                            <span class="elementLabel">Other AdCode</span>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" visible="false" id="ADCode">
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">AdCode</span>
                                                        </td>
                                                        <td width="10%" nowrap>
                                                            <asp:TextBox ID="txtADCode" runat="server" CssClass="textBox" Width="90px" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">L.C No. :</span>
                                                        </td>
                                                        <td width="5%">
                                                            <asp:TextBox ID="txtLCNo" Width="100px" runat="server" CssClass="textBox" TabIndex="7"
                                                                MaxLength="50" OnTextChanged="txtLCNo_TextChanged"
                                                                AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="3%" nowrap>
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td width="5%" nowrap>
                                                            <asp:TextBox ID="txtLCNoDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="8" OnTextChanged="txtLCNoDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdLCNo" Mask="99/99/9999" MaskType="Date" runat="server"
                                                                TargetControlID="txtLCNoDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnDTDraftDoc" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="calDraftDoc" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtLCNoDate" PopupButtonID="btnDTDraftDoc" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="mdLCNo"
                                                                ValidationGroup="dtVal" ControlToValidate="txtLCNoDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td nowrap align="right" width="5%">
                                                            <span class="elementLabel">Issued By :</span>
                                                        </td>
                                                        <td nowrap colspan="2">
                                                            <asp:TextBox ID="txtLCNoIssuedBy" Width="180px" runat="server" CssClass="textBox"
                                                                TabIndex="9" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">1ST MAIL &nbsp;&nbsp;&nbsp; 2nd MAIL</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">(DRAFT) B.E No. :</span>
                                                        </td>
                                                        <td width="5%">
                                                            <asp:TextBox ID="txtBENo" Width="100px" runat="server" CssClass="textBox" TabIndex="10"
                                                                MaxLength="100"></asp:TextBox>
                                                            <%--AutoPostBack="true" OnTextChanged="txtBENo_TextChanged"></asp:TextBox>--%>
                                                        </td>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td nowrap>
                                                            <asp:TextBox ID="txtBEDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="11"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdBEdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                                TargetControlID="txtBEDate" ErrorTooltipEnabled="True" CultureName="en-GB" CultureAMPMPlaceholder="AM;PM"
                                                                CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                                CultureDecimalPlaceholder="." CultureThousandsPlaceholder="," CultureTimePlaceholder=":"
                                                                Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button2" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtBEDate" PopupButtonID="btnDTDraftDoc" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator9" runat="server" ControlExtender="mdBEdate"
                                                                ValidationGroup="dtVal" ControlToValidate="txtBEDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td nowrap colspan="2" align="right">
                                                            <asp:CheckBox ID="chkBankLineTransfer" runat="server" Text="Bank Line Transfer (Accp Date)"
                                                                CssClass="elementLabel" TabIndex="12" />
                                                            <%--<span class="elementLabel">(Accp Date) :</span>--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccpDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="13"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtAccpDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button3" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtAccpDate" PopupButtonID="btnDTDraftDoc" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator10" runat="server" ControlExtender="MaskedEditExtender3"
                                                                ValidationGroup="dtVal" ControlToValidate="txtAccpDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBENoDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="14"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtBENoDoc1" Width="50px" runat="server" CssClass="textBox" TabIndex="14"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Invoice No. :</span>
                                                        </td>
                                                        <td nowrap>
                                                            <asp:TextBox ID="txtInvoiceNo" Width="100px" runat="server" CssClass="textBox" TabIndex="14"
                                                                MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="70px" TabIndex="15"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdDTInvoice" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtInvoiceDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnDTInvoice" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="calInvoiceDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtInvoiceDate" PopupButtonID="btnDTInvoice" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="mdValidatorInvoice" runat="server" ControlExtender="mdDTInvoice"
                                                                ValidationGroup="dtVal" ControlToValidate="txtInvoiceDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Dispatch Ind. :</span>
                                                        </td>
                                                        <td colspan="2" align="left">
                                                            <asp:DropDownList ID="ddlDispachInd" runat="server" CssClass="dropdownList" TabIndex="15"
                                                                Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlDispachInd_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <%--<asp:DropDownList ID="ddlDispachInd" runat="server" CssClass="dropdownList" TabIndex="15" 
															Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlDispachInd_SelectedIndexChanged">
															<asp:ListItem Value="">---Select---</asp:ListItem>
															<asp:ListItem Value="Dispatched directly by exporter">By Exporter</asp:ListItem>
															<asp:ListItem Value="By Bank">By Bank</asp:ListItem>
														</asp:DropDownList>--%>
                                                            <%-- <asp:TextBox ID="txtDispBydefault" Width="100px" runat="server" CssClass="textBox" TabIndex="16"
															MaxLength="35"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlDispBydefault" runat="server" CssClass="dropdownList" TabIndex="15" 
															Width="100px" >
															<asp:ListItem Value="Non-Dispatch">Non-Dispatch</asp:ListItem>
															<asp:ListItem Value="Sent to Bank">Sent to Bank</asp:ListItem>
															<asp:ListItem Value="Sent to drawee">Sent to drawee</asp:ListItem>
														</asp:DropDownList>--%>
                                                            <asp:DropDownList ID="ddlDispBydefault" runat="server" CssClass="dropdownList" TabIndex="15"
                                                                Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlDispBydefault_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInvoiceDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="16"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtInvoiceDoc1" Width="50px" runat="server" CssClass="textBox" TabIndex="16"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">AWB/BL No/LR :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAWBno" Width="100px" runat="server" CssClass="textBox" TabIndex="17"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAWBDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="18"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdAWBdate" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtAWBDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnDTAWB" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="calAWB" runat="server" Format="dd/MM/yyyy" TargetControlID="txtAWBDate"
                                                                PopupButtonID="btnDTAWB" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="mdAWBdate"
                                                                ValidationGroup="dtVal" ControlToValidate="txtAWBDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td nowrap align="right">
                                                            <span class="elementLabel">Issued By :</span>
                                                        </td>
                                                        <td nowrap colspan="2">
                                                            <asp:TextBox ID="txtAwbIssuedBy" Width="180px" runat="server" CssClass="textBox"
                                                                TabIndex="19" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAWBDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="20"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtAWBDoc1" Width="50px" runat="server" CssClass="textBox" TabIndex="20"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Packing List :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPackingList" Width="100px" runat="server" CssClass="textBox"
                                                                TabIndex="21" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPackingListDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="70px" TabIndex="22"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdPackingList" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtPackingListDate" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnDTPackingList" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="calPackingList" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtPackingListDate" PopupButtonID="btnDTPackingList" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="mdPackingList"
                                                                ValidationGroup="dtVal" ControlToValidate="txtPackingListDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td colspan="3"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtPackingDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="23"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtPackingDoc1" Width="50px" runat="server" CssClass="textBox" TabIndex="23"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Cert of Origin :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCertOfOrigin" Width="100px" runat="server" CssClass="textBox"
                                                                TabIndex="24" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2"></td>
                                                        <td nowrap align="right">
                                                            <span class="elementLabel">Issued By :</span>
                                                        </td>
                                                        <td nowrap colspan="2">
                                                            <asp:TextBox ID="txtCertIssuedBy" Width="180px" runat="server" CssClass="textBox"
                                                                TabIndex="25" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCertOfOriginDoc" Width="50px" runat="server" CssClass="textBox"
                                                                TabIndex="26" MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtCertOfOriginDoc1" Width="50px" runat="server" CssClass="textBox"
                                                            TabIndex="26" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Customs Invoice :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCustomsInvoice" Width="100px" runat="server" CssClass="textBox"
                                                                TabIndex="27" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCustomsDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="70px" TabIndex="28"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtCustomsDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button4" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtCustomsDate" PopupButtonID="btnDTPackingList" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator11" runat="server" ControlExtender="MaskedEditExtender4"
                                                                ValidationGroup="dtVal" ControlToValidate="txtCustomsDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td align="right" style="white-space: nowrap">
                                                            <span class="mandatoryField">* </span>
                                                            <span class="elementLabel">RBI Commodity ID :</span>
                                                        </td>
                                                        <td colspan="2" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtCommodityID" Width="25px" runat="server" CssClass="textBox" TabIndex="29"
                                                                MaxLength="3" AutoPostBack="True" OnTextChanged="txtCommodityID_TextChanged" />
                                                            <asp:Button ID="btnCommodityList" TabIndex="-1" CssClass="btnHelp_enabled" runat="server" />
                                                            <asp:Label ID="lblCommodityDescription" Width="150px" runat="server" CssClass="elementLabel" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCustomsDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="30"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtCustomsDoc1" Width="50px" runat="server" CssClass="textBox" TabIndex="30"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="white-space: nowrap" colspan="5">
                                                            <span class="mandatoryField">* </span>
                                                            <span class="elementLabel">GBase Commodity ID
															:</span>
                                                        </td>
                                                        <td colspan="2" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtGBaseCommodityID" Width="25px" runat="server" CssClass="textBox"
                                                                TabIndex="30" MaxLength="3" AutoPostBack="True" OnTextChanged="txtGBaseCommodityID_TextChanged" />
                                                            <asp:Button ID="btnGBaseCommodityList" TabIndex="-1" CssClass="btnHelp_enabled" runat="server" />
                                                            <asp:Label ID="lblGBaseCommodityDescription" Width="150px" runat="server" CssClass="elementLabel" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Ins. Policy :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInsPolicy" Width="100px" runat="server" CssClass="textBox" TabIndex="31"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInsPolicyDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="70px" TabIndex="32"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdInsPolicy" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtInsPolicyDate" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnDTInsPolicy" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="calInsPolicy" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtInsPolicyDate" PopupButtonID="btnDTInsPolicy" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="mdInsPolicy"
                                                                ValidationGroup="dtVal" ControlToValidate="txtInsPolicyDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td nowrap align="right">
                                                            <span class="elementLabel">Issued By :</span>
                                                        </td>
                                                        <td nowrap colspan="2">
                                                            <asp:TextBox ID="txtInsPolicyIssuedBy" Width="180px" runat="server" CssClass="textBox"
                                                                TabIndex="33" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInsPolicyDoc" Width="50px" runat="server" CssClass="textBox"
                                                                TabIndex="34" MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtInsPolicyDoc1" Width="50px" runat="server" CssClass="textBox"
                                                            TabIndex="34" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">GSP/Form A :</span>
                                                        </td>
                                                        <td></td>
                                                        <td align="right">
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGSPDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="35"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtGSPDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button5" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtGSPDate" PopupButtonID="btnDTPackingList" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator12" runat="server" ControlExtender="MaskedEditExtender5"
                                                                ValidationGroup="dtVal" ControlToValidate="txtGSPDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td nowrap align="right">
                                                            <span class="elementLabel">Contract No. :</span>
                                                        </td>
                                                        <td nowrap width="10%">
                                                            <asp:TextBox ID="txtContractNo" Width="90px" runat="server" CssClass="textBox" TabIndex="36"
                                                                MaxLength="50"></asp:TextBox>
                                                            &nbsp;&nbsp;<span class="elementLabel">Rate :</span>
                                                        </td>
                                                        <td nowrap align="left">
                                                            <asp:TextBox ID="txtRate" Width="45px" runat="server" CssClass="textBox" TabIndex="37"
                                                                MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGSPDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="38"
                                                                MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FIRC No. :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFIRCno" Width="100px" runat="server" CssClass="textBox" TabIndex="39"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Dated :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFIRCdate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" TabIndex="40"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtFIRCdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button6" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFIRCdate" PopupButtonID="btnDTInsPolicy" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator13" runat="server" ControlExtender="MaskedEditExtender6"
                                                                ValidationGroup="dtVal" ControlToValidate="txtFIRCdate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td nowrap align="right">
                                                            <span class="elementLabel">Issued By :</span>
                                                        </td>
                                                        <td nowrap colspan="2">
                                                            <asp:TextBox ID="txtFIRCnoIssuedBy" Width="180px" runat="server" CssClass="textBox"
                                                                TabIndex="41" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFIRCdoc" Width="50px" runat="server" CssClass="textBox" TabIndex="42"
                                                                MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <%--New Fields--%>
                                                    <tr>
                                                        <%--   <td align="right">
														<span class="mandatoryField">*</span><span class="elementLabel">Merchant Trade :</span>
													</td>
													<td align="left" colspan="3">
														<asp:DropDownList ID="ddlMercTrade" runat="server" TabIndex="91" CssClass="dropdownList">
															<asp:ListItem Value="0">Select</asp:ListItem>
															<asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
															<asp:ListItem Text="No" Value="No"></asp:ListItem>
														</asp:DropDownList>
													</td>--%>
                                                        <td colspan="6"></td>
                                                        <td align="right">
                                                            <span class="elementLabel">INSP :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtINSP" Width="50px" runat="server" CssClass="textBox" TabIndex="42"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtINSP1" Width="50px" runat="server" CssClass="textBox" TabIndex="42"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6"></td>
                                                        <td align="right">
                                                            <span class="elementLabel">W&M :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWM" Width="50px" runat="server" CssClass="textBox" TabIndex="42"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtWM1" Width="50px" runat="server" CssClass="textBox" TabIndex="42"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6"></td>
                                                        <td align="right">
                                                            <span class="elementLabel">OTHER :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOther" Width="50px" runat="server" CssClass="textBox" TabIndex="42"
                                                                MaxLength="5"></asp:TextBox>
                                                            &nbsp;&nbsp;
														<asp:TextBox ID="txtOther1" Width="50px" runat="server" CssClass="textBox" TabIndex="42"
                                                            MaxLength="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Merchant Trade :</span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:DropDownList ID="ddlMercTrade" runat="server" TabIndex="91" CssClass="dropdownList">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Miscellaneous :</span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtMiscellaneous" Width="250px" runat="server" CssClass="textBox"
                                                                TabIndex="43" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Shipment :</span>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RadioButton ID="rdbByAir" Text="By Air" CssClass="elementLabel" runat="server"
                                                                GroupName="Shipment" Checked="True" TabIndex="44" />
                                                            <asp:RadioButton ID="rdbBySea" Text="By Sea" CssClass="elementLabel" runat="server"
                                                                GroupName="Shipment" TabIndex="44" />
                                                            <asp:RadioButton ID="rdbByRoad" Text="By Road" CssClass="elementLabel" runat="server"
                                                                GroupName="Shipment" TabIndex="44" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMiscDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="45"
                                                                MaxLength="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <%-- <span class="elementLabel">Covering From(PORT ID) :</span>--%>
                                                        </td>
                                                        <td nowrap>
                                                            <%-- <asp:TextBox ID="txtCoveringFrom" runat="server" CssClass="textBox" MaxLength="20"
																			TabIndex="46" Width="100px" onkeydown="OpenPortCodeList(this);" ToolTip="Press F2 for help."></asp:TextBox>
																			<asp:Button ID="btnCoveringTo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />--%>
                                                        </td>
                                                        <td align="right" nowrap>
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Covering To :</span>
                                                        </td>
                                                        <td nowrap>
                                                            <asp:TextBox ID="txtCoveringTo" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="47" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap>
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Country :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textBox" MaxLength="2" TabIndex="48"
                                                                Width="30px" AutoPostBack="True" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                                                            <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            <asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel" Width="30px"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Reimb. Val Dt :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReimbValDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="70px" TabIndex="49"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtReimbValDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button7" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtReimbValDate" PopupButtonID="btnDTInsPolicy" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender7"
                                                                ValidationGroup="dtVal" ControlToValidate="txtReimbValDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Reimb. Claim Dt :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReimbClaimDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="70px" TabIndex="50"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtReimbClaimDate" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Button8" runat="server" Visible="False" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtReimbClaimDate" PopupButtonID="btnDTInsPolicy" Enabled="False">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" ControlExtender="MaskedEditExtender8"
                                                                ValidationGroup="dtVal" ControlToValidate="txtReimbClaimDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Bk Name :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBkName" Width="100px" runat="server" CssClass="textBox" TabIndex="51"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">BIC :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBIC" Width="120px" runat="server" CssClass="textBox" TabIndex="52"
                                                                MaxLength="13"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Reimb. Amt :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReimbAmt" Width="70px" runat="server" CssClass="textBox" TabIndex="53"
                                                                MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Notes :</span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtNotes" Width="300px" runat="server" CssClass="textBox" TabIndex="55"
                                                                MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="buttonDefault" ToolTip="Go to Transactions"
                                                                OnClientClick="return OnDocNextClick(1);" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel ID="tbTransactionDetails" runat="server" HeaderText="Transaction Details"
                                            Font-Bold="true" ForeColor="Lime">
                                            <ContentTemplate>
                                                <table cellspacing="1" cellpadding="0" border="0" width="100%">
                                                    <tr>
                                                        <td width="10%" align="right" nowrap></td>
                                                        <td align="right" nowrap>
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Curr :</span>
                                                        </td>
                                                        <td width="5%" nowrap>
                                                            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="dropdownList" TabIndex="57"
                                                                AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td nowrap width="10%">
                                                            <asp:CheckBox ID="chkLoanAdv" runat="server" CssClass="elementLabel" Text="Loan Advanced"
                                                                TabIndex="58" />
                                                        </td>
                                                        <td align="right" nowrap width="5%">
                                                            <span class="elementLabel">Oth Curr :</span>
                                                        </td>
                                                        <td nowrap width="5%">
                                                            <asp:DropDownList ID="ddlOtherCurrency" runat="server" CssClass="dropdownList" TabIndex="59">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" nowrap width="5%">
                                                            <span class="elementLabel">Exch Rt :</span><asp:TextBox ID="txtExchRate" runat="server"
                                                                CssClass="textBox" Style="text-align: right" Width="80px" TabIndex="60" onfocus="this.select()"
                                                                MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td nowrap align="left" width="5%">
                                                            <span class="elementLabel">No of Days :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtNoOfDays" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="25px" Text="0" TabIndex="61" AutoPostBack="true" OnTextChanged="txtNoOfDays_TextChanged" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3"></td>
                                                        <td colspan="2">
                                                            <asp:RadioButton ID="rbtnEBR" runat="server" CssClass="elementLabel" GroupName="EbrBdr"
                                                                TabIndex="61" Text="EBR" />&nbsp;<asp:RadioButton ID="rbtnBDR" runat="server" CssClass="elementLabel"
                                                                    GroupName="EbrBdr" TabIndex="62" Text="BDR" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" style="white-space: nowrap">
                                                            <asp:RadioButton ID="rbtnAfterAWB" runat="server" CssClass="elementLabel" GroupName="InterestDays"
                                                                TabIndex="63" OnCheckedChanged="rbtnAfterAWB_CheckedChanged" AutoPostBack="true" Text="After AWB/BL Date" />
                                                            &nbsp;<asp:RadioButton ID="rbtnFromAWB" runat="server" CssClass="elementLabel" GroupName="InterestDays"
                                                                TabIndex="64" Text="From AWB/BL Date" OnCheckedChanged="rbtnFromAWB_CheckedChanged" AutoPostBack="true" />
                                                            &nbsp;<asp:RadioButton ID="rbtnSight" runat="server" CssClass="elementLabel" GroupName="InterestDays"
                                                                TabIndex="65" Text="xx Days Sight" OnCheckedChanged="rbtnSight_CheckedChanged" AutoPostBack="true" />
                                                            &nbsp;<asp:RadioButton ID="rbtnDA" runat="server" CssClass="elementLabel" GroupName="InterestDays"
                                                                TabIndex="66" Text="from draft/BOE date" OnCheckedChanged="rbtnDA_CheckedChanged" AutoPostBack="true" />
                                                            &nbsp;<asp:RadioButton ID="rbtnFromInvoice" runat="server" CssClass="elementLabel"
                                                                GroupName="InterestDays" TabIndex="67" Text="From Invoice Date" OnCheckedChanged="rbtnFromInvoice_CheckedChanged" AutoPostBack="true" />
                                                            &nbsp;<asp:RadioButton ID="rbtnOthers" runat="server" CssClass="elementLabel" GroupName="InterestDays"
                                                                TabIndex="68" Text="Others" OnCheckedChanged="rbtnOthers_CheckedChanged" AutoPostBack="true" />
                                                            &nbsp;&nbsp;&nbsp;
														<asp:TextBox ID="txtOtherTenorRemarks" runat="server" CssClass="textBox" Width="200px"
                                                            TabIndex="69" onfocus="this.select()" MaxLength="31"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">LIBOR :</span>
                                                        </td>
                                                        <td width="5%">
                                                            <asp:TextBox ID="txtLibor" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="60px" TabIndex="70" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap width="5%">
                                                            <span class="elementLabel">Out of Days :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtOutOfDays" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="30px" TabIndex="71" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <span class="elementLabel">Due Date </span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                                Width="70px" OnTextChanged="txtDueDate_TextChanged" AutoPostBack="true" TabIndex="72"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdDoGaurantee" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtDueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"></td>
                                                        <td colspan="4" rowspan="9">
                                                            <table width="100%" border="1" cellpadding="1">
                                                                <tr>
                                                                    <td width="25%">
                                                                        <span class="elementLabel">Interest Rate</span>
                                                                    </td>
                                                                    <td width="20%">
                                                                        <span class="elementLabel">From Date</span>
                                                                    </td>
                                                                    <td width="20%">
                                                                        <span class="elementLabel">To Date</span>
                                                                    </td>
                                                                    <td>
                                                                        <span class="elementLabel">Days</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntRate1" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="60px" TabIndex="73" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntFrmDate1" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="74"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="mFrmDate1" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntFrmDate1" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntToDate1" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="75" onblur="CalculateNoOfDays('1','2');"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="mToDate1" Mask="99/99/9999" MaskType="Date" runat="server"
                                                                            TargetControlID="txtIntToDate1" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtForDays1" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="30px" TabIndex="76" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntRate2" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="60px" TabIndex="77" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntFrmDate2" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="78"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="mFrmDate2" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntFrmDate2" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntToDate2" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="79"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntToDate2" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtForDays2" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="30px" TabIndex="80" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntRate3" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="60px" TabIndex="81" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntFrmDate3" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="82"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender12" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntFrmDate3" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntToDate3" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="83"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender13" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntToDate3" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtForDays3" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="30px" TabIndex="84" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntRate4" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="60px" TabIndex="85" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntFrmDate4" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="86"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender14" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntFrmDate4" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntToDate4" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="87"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender15" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntToDate4" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtForDays4" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="30px" TabIndex="88" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntRate5" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="60px" TabIndex="89" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntFrmDate5" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="90"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender16" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntFrmDate5" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntToDate5" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="91"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender17" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntToDate5" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtForDays5" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="30px" TabIndex="92" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntRate6" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="60px" TabIndex="93" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntFrmDate6" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="94"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender18" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntFrmDate6" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIntToDate6" runat="server" CssClass="textBox" MaxLength="10"
                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="95"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender19" Mask="99/99/9999" MaskType="Date"
                                                                            runat="server" TargetControlID="txtIntToDate6" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtForDays6" runat="server" CssClass="textBox" Style="text-align: right"
                                                                            Width="30px" TabIndex="96" onfocus="this.select()" MaxLength="4"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="5%"></td>
                                                        <td width="5%">
                                                            <span class="elementLabel">Amount in</span>
                                                        </td>
                                                        <td width="5%" nowrap>
                                                            <span class="elementLabel">Amount in INR</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Bill Amount :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBillAmount" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="97" onfocus="this.select()" MaxLength="20" OnTextChanged="txtBillAmount_TextChanged"
                                                                AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBillAmountinRS" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="98" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">LEI MINT Rate :</span>
                                                            <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Negotiated Amount :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNegotiatedAmt" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="100" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNegotiatedAmtinRS" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="101" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Interest :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInterest" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="102" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInterestinRS" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="103" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <span class="elementLabel">Exch Rate (EBR)</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Net Amount :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNetAmt" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="104" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNetAmtinRS" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="105" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtExchRtEBR" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="106" onfocus="this.select()" MaxLength="15" OnTextChanged="txtExchRtEBR_TextChanged"
                                                                AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FBK Charges :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_fbkcharges" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="107" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_fbkchargesinRS" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="108" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Other Charges :</span>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOtherChrgs" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="109" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Bank Cert :</span>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtBankCert" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="110" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td style="border: 2;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Negotiation Fees :</span>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtNegotiationFees" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="111" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Courier Charges :</span>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCourierChrgs" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="112" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Margin Amount :</span>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtMarginAmt" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="113" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Commission :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommissionID" runat="server" CssClass="textBox" Width="20px"
                                                                TabIndex="114" onfocus="this.select()" MaxLength="2"></asp:TextBox><asp:Button ID="btnCommissionList"
                                                                    runat="server" CssClass="btnHelp_enabled" TabIndex="-1" OnClientClick="return OpenCommissionList();" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommission" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="115" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap colspan="2">
                                                            <span class="elementLabel">Accepted Due Date :</span>
                                                            <asp:TextBox ID="txtAcceptedDueDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                ValidationGroup="dtVal" Width="80px" TabIndex="116"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtAcceptedDueDate" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">GST(%)</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlServiceTax" runat="server" CssClass="dropdownList" Width="60px"
                                                                TabIndex="117">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSTaxAmount" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="118" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td align="right" colspan="2">
                                                            <span class="elementLabel">SBCess (%) :</span>
                                                            <asp:TextBox ID="txtsbcess" runat="server" CssClass="textBox" TabIndex="119" Width="40px"
                                                                Style="text-align: right; font-weight: bold"></asp:TextBox>
                                                            <asp:TextBox ID="txtSBcesssamt" runat="server" CssClass="textBox" TabIndex="120" Width="80px"
                                                                Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                        <td align="center" colspan="2">
                                                            <span class="elementLabel">KKCess (%) :</span>
                                                            <asp:TextBox ID="txt_kkcessper" runat="server" CssClass="textBox" TabIndex="121" Width="40px"
                                                                Style="text-align: right; font-weight: bold"></asp:TextBox>&nbsp;
														<asp:TextBox ID="txt_kkcessamt" runat="server" CssClass="textBox" TabIndex="122" Width="80px"
                                                            Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">STax Total Amt :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtsttamt" runat="server" CssClass="textBox" TabIndex="123" Width="100px"
                                                                Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">GST on FXDLS :</span>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkApplicable" runat="server" CssClass="elementLabel" OnCheckedChanged="chkApplicable_CheckedChanged"
                                                                Text="Yes/No" AutoPostBack="True" TabIndex="124" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSTFXDLS" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="125" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">SBCess on FxDls :</span>
                                                        </td>
                                                        <td align="left" width="10%">
                                                            <asp:TextBox ID="txtsbfx" runat="server" CssClass="textBox" Width="80px" Style="text-align: right"
                                                                TabIndex="126"></asp:TextBox>
                                                        </td>
                                                        <td nowrap align="center">
                                                            <span class="elementLabel">KKCess on FxDls :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_kkcessonfx" runat="server" CssClass="textBox" Width="80px" Style="text-align: right"
                                                                TabIndex="127"></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Total GST On FxDls :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txttotsbcess" runat="server" CssClass="textBox" Width="100px" Style="text-align: right"
                                                                TabIndex="128"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr runat="server" visible="False" b>
                                                        <td align="right" nowrap="nowrap" runat="server">
                                                            <span class="elementLabel">Stax FBK (%)</span>
                                                        </td>
                                                        <td runat="server">
                                                            <asp:DropDownList ID="ddlServiceTaxfbk" runat="server" CssClass="dropdownList" Width="60px"
                                                                TabIndex="129">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td runat="server">
                                                            <asp:TextBox ID="txtSTaxAmountfbk" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="130" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td align="right" colspan="2" runat="server">
                                                            <span class="elementLabel">SBCess on FBK (%) :</span>
                                                            <asp:TextBox ID="txtsbcessfbk" runat="server" CssClass="textBox" TabIndex="131" Width="40px"
                                                                Style="text-align: right; font-weight: bold"></asp:TextBox>
                                                            <asp:TextBox ID="txtSBcesssamtfbk" runat="server" CssClass="textBox" TabIndex="132"
                                                                Width="80px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                        <td align="left" colspan="2" nowrap="nowrap" runat="server">
                                                            <span class="elementLabel">KKCess on FBK(%) :</span>
                                                            <asp:TextBox ID="txt_kkcessperfbk" runat="server" CssClass="textBox" TabIndex="133"
                                                                Width="40px" Style="text-align: right; font-weight: bold"></asp:TextBox>&nbsp;
														<asp:TextBox ID="txt_kkcessamtfbk" runat="server" CssClass="textBox" TabIndex="134"
                                                            Width="80px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap="nowrap" runat="server">
                                                            <span class="elementLabel">STax Total on FBK :</span>
                                                        </td>
                                                        <td align="left" runat="server">
                                                            <asp:TextBox ID="txtsttamtfbk" runat="server" CssClass="textBox" TabIndex="135" Width="100px"
                                                                Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                        <td runat="server"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2">
                                                            <span class="elementLabel">Credit to Current A/C :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCurrentAcinRS" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="80px" TabIndex="136" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;
														<asp:CheckBox ID="chkRBI" runat="server" CssClass="elementLabel" Text="RBI A/C" TextAlign="Left"
                                                            TabIndex="137" Visible="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <%-- create Anand 07/06/2023     </table>--%>
                                                            <asp:Panel ID="TTFIRCVisible" runat="server">
                                                                <table cellspacing="1" cellpadding="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td colspan="6" align="right" style="width: 850px">
                                                                            <asp:Button runat="server" Text="TT Reference No." CssClass="buttonDefault" Height="20px"
                                                                                Width="150px" TabIndex="138" ID="btnTTRefNoList"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button runat="server" Text="E-Firc Reference No." CssClass="buttonDefault" Height="20px"
                                                                Width="150px" TabIndex="139" ID="btnFircRefNoList"></asp:Button>
                                                                        </td>
                                                                        <tr>

                                                                            <td colspan="7" align="right">
                                                                                <table width="130%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <br />
                                                                                            <ajaxToolkit:CollapsiblePanelExtender ID="cpe2" runat="Server" TargetControlID="panelSecondAdd"
                                                                                                CollapsedSize="0" ExpandedSize="160" Collapsed="true" ExpandControlID="btnTTRefNoList"
                                                                                                CollapseControlID="btnTTRefNoList" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
                                                                                                ExpandDirection="Vertical" />
                                                                                            <asp:Panel ID="panelSecondAdd" runat="server">
                                                                                                <table cellspacing="1" border="1" width="95%">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table cellspacing="0" border="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <span class="elementLabel">TT Reference No </span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Inward currency </span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Total ITT Amt </span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Balance ITT Amt </span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">ITT Amt To Adj.</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Realised Currency</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Cross Curr. Rate</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Amount Realised</span>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtTTRefNo1" runat="server" TabIndex="200" CssClass="textBox" Enabled="false" Width="150px" OnTextChanged="txtTTRefNo1_TextChanged"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTRef1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                            OnClientClick="return OpenTTNoList('1');" />
                                                                                                                        <asp:Label ID="lblTTRname1" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTCurr1" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTCurrency1" Enabled="false" runat="server" TabIndex="201" CssClass="dropdownList">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTotTTAmt1" runat="server" Enabled="false" TabIndex="202" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtBalTTAmt1" runat="server" Enabled="false" TabIndex="203" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmount1" runat="server" TabIndex="204" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount1_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTRealisedCurr1" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTRealisedCurr1" runat="server" CssClass="dropdownList" TabIndex="205">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTCrossCurrRate1" AutoPostBack="true" runat="server" TabIndex="206" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate1_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmtRealised1" runat="server" TabIndex="207" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTCancel1" runat="server" OnClick="btnTTCancel1_Click" CssClass="image-button" />
                                                                                                                    </td>

                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtTTRefNo2" runat="server" TabIndex="208" CssClass="textBox" Enabled="false" Width="150px" OnTextChanged="txtTTRefNo2_TextChanged"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTRef2" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                            OnClientClick="return OpenTTNoList('2');" />
                                                                                                                        <asp:Label ID="lblTTRname2" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTCurr2" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTCurrency2" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="209">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTotTTAmt2" runat="server" Enabled="false" CssClass="textBox" TabIndex="210"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtBalTTAmt2" runat="server" Enabled="false" TabIndex="211" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmount2" runat="server" TabIndex="212" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount2_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTRealisedCurr2" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTRealisedCurr2" runat="server" CssClass="dropdownList" TabIndex="213">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTCrossCurrRate2" runat="server" AutoPostBack="true" TabIndex="214" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate2_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmtRealised2" runat="server" TabIndex="215" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTCancel2" runat="server" OnClick="btnTTCancel2_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtTTRefNo3" runat="server" Enabled="false" TabIndex="216" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo3_TextChanged"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTRef3" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                            OnClientClick="return OpenTTNoList('3');" />
                                                                                                                        <asp:Label ID="lblTTRname3" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTCurr3" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTCurrency3" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="217">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTotTTAmt3" runat="server" Enabled="false" TabIndex="218" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtBalTTAmt3" runat="server" Enabled="false" TabIndex="219" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmount3" runat="server" TabIndex="220" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount3_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTRealisedCurr3" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTRealisedCurr3" runat="server" CssClass="dropdownList" TabIndex="221">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTCrossCurrRate3" runat="server" AutoPostBack="true" TabIndex="222" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate3_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmtRealised3" runat="server" TabIndex="223" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTCancel3" runat="server" OnClick="btnTTCancel3_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtTTRefNo4" runat="server" Enabled="false" TabIndex="224" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo4_TextChanged"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTRef4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                            OnClientClick="return OpenTTNoList('4');" />
                                                                                                                        <asp:Label ID="lblTTRname4" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTCurr4" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTCurrency4" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="225">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTotTTAmt4" runat="server" Enabled="false" TabIndex="226" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtBalTTAmt4" runat="server" Enabled="false" TabIndex="227" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmount4" runat="server" TabIndex="228" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount4_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTRealisedCurr4" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTRealisedCurr4" runat="server" CssClass="dropdownList" TabIndex="229">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTCrossCurrRate4" runat="server" AutoPostBack="true" TabIndex="230" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate4_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmtRealised4" runat="server" TabIndex="231" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTCancel4" runat="server" OnClick="btnTTCancel4_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtTTRefNo5" runat="server" Enabled="false" TabIndex="232" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo5_TextChanged"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTRef5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                            OnClientClick="return OpenTTNoList('5');" />
                                                                                                                        <asp:Label ID="lblTTRname5" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTCurr5" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTCurrency5" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="233">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTotTAmt5" runat="server" Enabled="false" TabIndex="234" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtBalTTAmt5" runat="server" Enabled="false" TabIndex="235" CssClass="textBox"
                                                                                                                            Width="80px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmount5" runat="server" TabIndex="236" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount5_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtTTRealisedCurr5" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlTTRealisedCurr5" runat="server" CssClass="dropdownList" TabIndex="237">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTCrossCurrRate5" runat="server" AutoPostBack="true" TabIndex="238" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate5_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtTTAmtRealised5" runat="server" TabIndex="239" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTTCancel5" runat="server" OnClick="btnTTCancel5_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="right" colspan="5">
                                                                                                                        <asp:Button ID="btn_Trans_Add" runat="server" Text="Add" CssClass="buttonDefault"
                                                                                                                            TabIndex="240" OnClick="btn_Trans_Add_Click" />&nbsp;&nbsp;&nbsp;
														
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <asp:Panel ID="TT5Row" runat="server" Visible="false">
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo6" runat="server" Enabled="false" TabIndex="241" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo6_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef6" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('6');" />
                                                                                                                            <asp:Label ID="lblTTRname6" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr6" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency6" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="242">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt6" runat="server" Enabled="false" TabIndex="243" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt6" runat="server" Enabled="false" TabIndex="244" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount6" runat="server" TabIndex="245" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount6_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr6" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr6" runat="server" CssClass="dropdownList" TabIndex="246">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate6" runat="server" AutoPostBack="true" TabIndex="247" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate6_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised6" runat="server" TabIndex="248" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel6" runat="server" OnClick="btnTTCancel6_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo7" runat="server" Enabled="false" TabIndex="249" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo7_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef7" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('7');" />
                                                                                                                            <asp:Label ID="lblTTRname7" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr7" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency7" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="250">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt7" runat="server" Enabled="false" TabIndex="251" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt7" runat="server" Enabled="false" TabIndex="252" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount7" runat="server" TabIndex="253" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount7_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr7" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr7" runat="server" CssClass="dropdownList" TabIndex="254">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate7" runat="server" AutoPostBack="true" TabIndex="255" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate7_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised7" runat="server" TabIndex="256" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel7" runat="server" OnClick="btnTTCancel7_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo8" runat="server" Enabled="false" TabIndex="257" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo8_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef8" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('8');" />
                                                                                                                            <asp:Label ID="lblTTRname8" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr8" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency8" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="258">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt8" runat="server" Enabled="false" TabIndex="259" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTAmt8" runat="server" Enabled="false" TabIndex="260" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount8" runat="server" TabIndex="261" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount8_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr8" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr8" runat="server" CssClass="dropdownList" TabIndex="262">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate8" runat="server" AutoPostBack="true" TabIndex="263" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate8_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised8" runat="server" TabIndex="264" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel8" runat="server" OnClick="btnTTCancel8_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="right" colspan="5">
                                                                                                                            <asp:Button ID="btn_Trans_Add1" runat="server" Text="Add" CssClass="buttonDefault"
                                                                                                                                TabIndex="265" OnClick="btn_Trans_Add1_Click" />&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Trans_Remove1" runat="server" Text="Remove" CssClass="buttonDefault"
                                                            TabIndex="266" OnClick="btn_Trans_Remove1_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                                <asp:Panel ID="TT8Row" runat="server" Visible="false">
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo9" runat="server" Enabled="false" TabIndex="267" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo9_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef9" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('9');" />
                                                                                                                            <asp:Label ID="lblTTRname9" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr9" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency9" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="268">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTtTTAmt9" runat="server" Enabled="false" TabIndex="269" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt9" runat="server" Enabled="false" TabIndex="270" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount9" runat="server" TabIndex="271" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount9_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr9" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr9" runat="server" CssClass="dropdownList" TabIndex="272">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate9" runat="server" AutoPostBack="true" TabIndex="273" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate9_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised9" runat="server" TabIndex="274" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel9" runat="server" OnClick="btnTTCancel9_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo10" runat="server" Enabled="false" TabIndex="275" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo10_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef10" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('10');" />
                                                                                                                            <asp:Label ID="lblTTRname10" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr10" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency10" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="276">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt10" runat="server" Enabled="false" TabIndex="277" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt10" runat="server" Enabled="false" TabIndex="278" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount10" runat="server" TabIndex="279" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount10_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr10" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr10" runat="server" CssClass="dropdownList" TabIndex="280">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate10" runat="server" AutoPostBack="true" TabIndex="281" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate10_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised10" runat="server" TabIndex="282" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel10" runat="server" OnClick="btnTTCancel10_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo11" runat="server" Enabled="false" TabIndex="283" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo11_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef11" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('11');" />
                                                                                                                            <asp:Label ID="lblTTRname11" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr11" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency11" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="284">
                                                                                                                            </asp:DropDownList>

                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt11" runat="server" Enabled="false" TabIndex="285" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt11" runat="server" Enabled="false" TabIndex="286" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount11" runat="server" TabIndex="287" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount11_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr11" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr11" runat="server" CssClass="dropdownList" TabIndex="288">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate11" runat="server" AutoPostBack="true" TabIndex="289" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate11_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised11" runat="server" TabIndex="290" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel11" runat="server" OnClick="btnTTCancel11_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="right" colspan="5">
                                                                                                                            <asp:Button ID="btn_Trans_Add2" runat="server" Text="Add" CssClass="buttonDefault"
                                                                                                                                TabIndex="291" OnClick="btn_Trans_Add2_Click" />&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Trans_Remove2" runat="server" Text="Remove" CssClass="buttonDefault"
                                                            TabIndex="292" OnClick="btn_Trans_Remove2_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                                <asp:Panel ID="TT11Row" runat="server" Visible="false">
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo12" runat="server" Enabled="false" TabIndex="293" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo12_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef12" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('12');" />
                                                                                                                            <asp:Label ID="lblTTRname12" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr12" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency12" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="294">
                                                                                                                            </asp:DropDownList>

                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt12" runat="server" Enabled="false" TabIndex="295" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt12" runat="server" Enabled="false" TabIndex="296" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount12" runat="server" TabIndex="297" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount12_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr12" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr12" runat="server" CssClass="dropdownList" TabIndex="298">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate12" runat="server" AutoPostBack="true" TabIndex="299" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate12_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised12" runat="server" TabIndex="300" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel12" runat="server" OnClick="btnTTCancel12_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo13" runat="server" Enabled="false" TabIndex="301" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo13_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef13" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('13');" />
                                                                                                                            <asp:Label ID="lblTTRname13" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr13" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency13" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="302">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt13" runat="server" Enabled="false" TabIndex="303" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt13" runat="server" Enabled="false" TabIndex="304" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount13" runat="server" TabIndex="305" CssClass="textBox" Width="80px" AutoPostBack="true" OnTextChanged="txtTTAmount13_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr13" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr13" runat="server" CssClass="dropdownList" TabIndex="306">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate13" runat="server" AutoPostBack="true" TabIndex="307" CssClass="textBox" OnTextChanged="txtTTCrossCurrRate13_TextChanged" Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised13" runat="server" TabIndex="308" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel13" runat="server" OnClick="btnTTCancel13_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo14" runat="server" Enabled="false" TabIndex="309" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo14_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef14" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('14');" />
                                                                                                                            <asp:Label ID="lblTTRname14" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr14" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency14" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="310">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt14" runat="server" Enabled="false" TabIndex="311" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt14" runat="server" Enabled="false" TabIndex="312" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount14" runat="server" TabIndex="313" CssClass="textBox" AutoPostBack="true" Width="80px" OnTextChanged="txtTTAmount14_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr14" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr14" runat="server" CssClass="dropdownList" TabIndex="314">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate14" runat="server" AutoPostBack="true" TabIndex="315" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate14_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised14" runat="server" TabIndex="316" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel14" runat="server" OnClick="btnTTCancel14_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtTTRefNo15" runat="server" Enabled="false" TabIndex="317" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo15_TextChanged"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTRef15" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                                                                                OnClientClick="return OpenTTNoList('15');" />
                                                                                                                            <asp:Label ID="lblTTRname15" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTCurr15" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTCurrency15" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="318">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTotTTAmt15" runat="server" Enabled="false" TabIndex="319" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtBalTTAmt15" runat="server" Enabled="false" TabIndex="320" CssClass="textBox"
                                                                                                                                Width="80px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmount15" runat="server" TabIndex="321" CssClass="textBox" AutoPostBack="true" Width="80px" OnTextChanged="txtTTAmount15_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtTTRealisedCurr15" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlTTRealisedCurr15" runat="server" CssClass="dropdownList" TabIndex="322">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTCrossCurrRate15" runat="server" AutoPostBack="true" TabIndex="323" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate15_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtTTAmtRealised15" runat="server" TabIndex="324" CssClass="textBox" Width="80px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnTTCancel15" runat="server" OnClick="btnTTCancel15_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="right" colspan="5">

                                                                                                                            <asp:Button ID="btn_Trans_Remove3" runat="server" Text="Remove" CssClass="buttonDefault"
                                                                                                                                TabIndex="325" OnClick="btn_Trans_Remove3_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="7" align="right">
                                                                                <table width="130%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <br />
                                                                                            <ajaxToolkit:CollapsiblePanelExtender ID="Cpe3" runat="Server" TargetControlID="panelSecondAdd1"
                                                                                                CollapsedSize="0" ExpandedSize="160" Collapsed="true" ExpandControlID="btnFircRefNoList"
                                                                                                CollapseControlID="btnFircRefNoList" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
                                                                                                ExpandDirection="Vertical" />
                                                                                            <asp:Panel ID="panelSecondAdd1" runat="server">
                                                                                                <table cellspacing="1" border="1" width="95%">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table cellspacing="0" border="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <span class="elementLabel">FIRC No </span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">FIRC Date</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Efirc Currency </span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Efirc Amt To Adj</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Relised Curr</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">Cross Curr. Rate</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">To be Adjusted in SB</span>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <span class="elementLabel">FIRC AD Code</span>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtFIRCNo1_OB" runat="server" TabIndex="326" CssClass="textBox" Width="150px"
                                                                                                                            MaxLength="35"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCDate1_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="327"></asp:TextBox>
                                                                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender21" Mask="99/99/9999" MaskType="Date"
                                                                                                                            runat="server" TargetControlID="txtFIRCDate1_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCCurrency1_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCCurrency1_OB" runat="server" CssClass="dropdownList" TabIndex="328" OnTextChanged="ddlFIRCCurrency1_OB_TextChanged" AutoPostBack="true">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCAmount1_OB" AutoPostBack="true" runat="server" TabIndex="329" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount1_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCRealisedCurr1_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCRealisedCurr1_OB" runat="server" CssClass="dropdownList" TabIndex="330">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCCrossCurrRate1_OB" runat="server" AutoPostBack="true" TabIndex="331" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate1_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCTobeAdjustedinSB1_OB" runat="server" TabIndex="332" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCADCode1_OB" runat="server" TabIndex="333" CssClass="textBox"
                                                                                                                            MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnEFIRCCancel1" runat="server" OnClick="btnEFIRCCancel1_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtFIRCNo2_OB" runat="server" TabIndex="334" CssClass="textBox" Width="150px"
                                                                                                                            MaxLength="35"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCDate2_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="335"></asp:TextBox>
                                                                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender22" Mask="99/99/9999" MaskType="Date"
                                                                                                                            runat="server" TargetControlID="txtFIRCDate2_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCCurrency2_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCCurrency2_OB" runat="server" CssClass="dropdownList" TabIndex="336" OnTextChanged="ddlFIRCCurrency2_OB_TextChanged" AutoPostBack="true">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCAmount2_OB" AutoPostBack="true" runat="server" TabIndex="337" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount2_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCRealisedCurr2_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCRealisedCurr2_OB" runat="server" CssClass="dropdownList" TabIndex="338">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCCrossCurrRate2_OB" runat="server" AutoPostBack="true" TabIndex="339" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate2_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCTobeAdjustedinSB2_OB" runat="server" TabIndex="340" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCADCode2_OB" runat="server" TabIndex="341" CssClass="textBox"
                                                                                                                            MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnEFIRCCancel2" runat="server" OnClick="btnEFIRCCancel2_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtFIRCNo3_OB" runat="server" TabIndex="342" CssClass="textBox" Width="150px"
                                                                                                                            MaxLength="35"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCDate3_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="343"></asp:TextBox>
                                                                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender23" Mask="99/99/9999" MaskType="Date"
                                                                                                                            runat="server" TargetControlID="txtFIRCDate3_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCCurrency3_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCCurrency3_OB" runat="server" CssClass="dropdownList" TabIndex="344" OnTextChanged="ddlFIRCCurrency3_OB_TextChanged" AutoPostBack="true">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCAmount3_OB" AutoPostBack="true" runat="server" TabIndex="345" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount3_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCRealisedCurr3_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCRealisedCurr3_OB" runat="server" CssClass="dropdownList" TabIndex="346">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCCrossCurrRate3_OB" runat="server" AutoPostBack="true" TabIndex="347" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate3_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCTobeAdjustedinSB3_OB" runat="server" TabIndex="348" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCADCode3_OB" runat="server" TabIndex="349" CssClass="textBox"
                                                                                                                            MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnEFIRCCancel3" runat="server" OnClick="btnEFIRCCancel3_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtFIRCNo4_OB" runat="server" TabIndex="350" CssClass="textBox" Width="150px"
                                                                                                                            MaxLength="35"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCDate4_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="351"></asp:TextBox>
                                                                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender24" Mask="99/99/9999" MaskType="Date"
                                                                                                                            runat="server" TargetControlID="txtFIRCDate4_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                                                                        </ajaxToolkit:MaskedEditExtender>

                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCCurrency4_OB"  runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCCurrency4_OB" runat="server" CssClass="dropdownList" TabIndex="352" OnTextChanged="ddlFIRCCurrency4_OB_TextChanged" AutoPostBack="true">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCAmount4_OB" AutoPostBack="true" runat="server" TabIndex="353" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount4_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCRealisedCurr4_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCRealisedCurr4_OB" runat="server" CssClass="dropdownList" TabIndex="354">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCCrossCurrRate4_OB" AutoPostBack="true" runat="server" TabIndex="355" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate4_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCTobeAdjustedinSB4_OB" runat="server" TabIndex="356" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                    </td>

                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCADCode4_OB" runat="server" TabIndex="357" CssClass="textBox"
                                                                                                                            MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnEFIRCCancel4" runat="server" OnClick="btnEFIRCCancel4_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td nowrap align="left">
                                                                                                                        <asp:TextBox ID="txtFIRCNo5_OB" runat="server" TabIndex="358" CssClass="textBox" Width="150px"
                                                                                                                            MaxLength="35"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCDate5_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                            ValidationGroup="dtVal" Width="70px" TabIndex="359"></asp:TextBox>
                                                                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender25" Mask="99/99/9999" MaskType="Date"
                                                                                                                            runat="server" TargetControlID="txtFIRCDate5_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                            CultureTimePlaceholder=":" Enabled="True">
                                                                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCCurrency5_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCCurrency5_OB" runat="server" CssClass="dropdownList" TabIndex="360" OnTextChanged="ddlFIRCCurrency5_OB_TextChanged" AutoPostBack="true">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCAmount5_OB" AutoPostBack="true" runat="server" TabIndex="361" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount5_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%--<asp:TextBox ID="txtFIRCRealisedCurr5_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                        <asp:DropDownList ID="ddlFIRCRealisedCurr5_OB" runat="server" CssClass="dropdownList" TabIndex="362">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCCrossCurrRate5_OB" runat="server" AutoPostBack="true" TabIndex="363" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate5_OB_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCTobeAdjustedinSB5_OB" runat="server" TabIndex="364" CssClass="textBox"
                                                                                                                            MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                    </td>

                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtFIRCADCode5_OB" runat="server" TabIndex="365" CssClass="textBox"
                                                                                                                            MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnEFIRCCancel5" runat="server" OnClick="btnEFIRCCancel5_Click" CssClass="image-button" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="right" colspan="5">
                                                                                                                        <asp:Button ID="btn_Trans_FAdd" runat="server" Text="Add" CssClass="buttonDefault"
                                                                                                                            TabIndex="366" OnClick="btn_Trans_FAdd_Click" />&nbsp;&nbsp;&nbsp;
														
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <asp:Panel ID="FIRC5Row" runat="server" Visible="false">
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo6_OB" runat="server" TabIndex="367" CssClass="textBox" Width="150px"
                                                                                                                                MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate6_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="368"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender26" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate6_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                                CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency6_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency6_OB" runat="server" CssClass="dropdownList" TabIndex="369" OnTextChanged="ddlFIRCCurrency6_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount6_OB" AutoPostBack="true" runat="server" TabIndex="370" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount6_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr6_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr6_OB" runat="server" CssClass="dropdownList" TabIndex="371">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate6_OB" runat="server" AutoPostBack="true" TabIndex="372" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate6_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB6_OB" runat="server" TabIndex="373" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode6_OB" runat="server" TabIndex="374" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel6" runat="server" OnClick="btnEFIRCCancel6_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo7_OB" runat="server" TabIndex="375" CssClass="textBox" Width="150px"
                                                                                                                                MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate7_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="376"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender27" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate7_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                                CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency7_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency7_OB" runat="server" CssClass="dropdownList" TabIndex="377" OnTextChanged="ddlFIRCCurrency7_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount7_OB" AutoPostBack="true" runat="server" TabIndex="378" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount7_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr7_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr7_OB" runat="server" CssClass="dropdownList" TabIndex="379">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate7_OB" runat="server" AutoPostBack="true" TabIndex="380" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate7_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB7_OB" runat="server" TabIndex="381" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode7_OB" runat="server" TabIndex="382" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel7" runat="server" OnClick="btnEFIRCCancel7_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo8_OB" runat="server" TabIndex="383" CssClass="textBox" Width="150px"
                                                                                                                                MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate8_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="384"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender28" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate8_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                                CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency8_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency8_OB" runat="server" CssClass="dropdownList" TabIndex="385" OnTextChanged="ddlFIRCCurrency8_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount8_OB" AutoPostBack="true" runat="server" TabIndex="386" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount8_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr8_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr8_OB" runat="server" CssClass="dropdownList" TabIndex="387">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate8_OB" runat="server" AutoPostBack="true" TabIndex="388" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate8_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB8_OB" runat="server" TabIndex="389" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode8_OB" runat="server" TabIndex="390" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel8" runat="server" OnClick="btnEFIRCCancel8_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="right" colspan="5">
                                                                                                                            <asp:Button ID="btn_Trans_FAdd1" runat="server" Text="Add" CssClass="buttonDefault"
                                                                                                                                TabIndex="391" OnClick="btn_Trans_FAdd1_Click" />&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Trans_FRemove1" runat="server" Text="Remove" CssClass="buttonDefault"
                                                            TabIndex="392" OnClick="btn3_Trans_FRemove1_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                                <asp:Panel ID="FIRC8Row" runat="server" Visible="false">
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo9_OB" runat="server" TabIndex="393" CssClass="textBox" Width="150px"
                                                                                                                                MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate9_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="394"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender29" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate9_OB" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                                                                                CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency9_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency9_OB" runat="server" CssClass="dropdownList" TabIndex="395" OnTextChanged="ddlFIRCCurrency9_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount9_OB" AutoPostBack="true" runat="server" TabIndex="396" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount9_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr9_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr9_OB" runat="server" CssClass="dropdownList" TabIndex="397">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate9_OB" runat="server" AutoPostBack="true" TabIndex="398" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate9_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB9_OB" runat="server" TabIndex="399" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode9_OB" runat="server" TabIndex="400" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel9" runat="server" OnClick="btnEFIRCCancel9_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo10_OB" runat="server" TabIndex="401" CssClass="textBox"
                                                                                                                                Width="150px" MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate10_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="402"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender30" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate10_OB" ErrorTooltipEnabled="True"
                                                                                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency10_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency10_OB" runat="server" CssClass="dropdownList" TabIndex="403" OnTextChanged="ddlFIRCCurrency10_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount10_OB" AutoPostBack="true" runat="server" TabIndex="404" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount10_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr10_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr10_OB" runat="server" CssClass="dropdownList" TabIndex="405">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate10_OB" runat="server" AutoPostBack="true" TabIndex="406" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate10_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB10_OB" runat="server" TabIndex="407" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode10_OB" runat="server" TabIndex="408" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel10" runat="server" OnClick="btnEFIRCCancel10_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo11_OB" runat="server" TabIndex="409" CssClass="textBox"
                                                                                                                                Width="150px" MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate11_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="410"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender31" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate11_OB" ErrorTooltipEnabled="True"
                                                                                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency11_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency11_OB" runat="server" CssClass="dropdownList" TabIndex="411" OnTextChanged="ddlFIRCCurrency11_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount11_OB" AutoPostBack="true" runat="server" TabIndex="412" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount11_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr11_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr11_OB" runat="server" CssClass="dropdownList" TabIndex="413">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate11_OB" runat="server" AutoPostBack="true" TabIndex="414" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate11_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB11_OB" runat="server" TabIndex="415" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode11_OB" runat="server" TabIndex="416" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel11" runat="server" OnClick="btnEFIRCCancel11_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="right" colspan="5">
                                                                                                                            <asp:Button ID="btn_Trans_FAdd2" runat="server" Text="Add" CssClass="buttonDefault"
                                                                                                                                TabIndex="417" OnClick="btn_Trans_FAdd2_Click" />&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Trans_FRemove2" runat="server" Text="Remove" CssClass="buttonDefault"
                                                            TabIndex="418" OnClick="btn_Trans_FRemove2_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                                <asp:Panel ID="FIRC11Row" runat="server" Visible="false">
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo12_OB" runat="server" TabIndex="419" CssClass="textBox"
                                                                                                                                Width="150px" MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate12_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="420"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender32" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate12_OB" ErrorTooltipEnabled="True"
                                                                                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency12_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency12_OB" runat="server" CssClass="dropdownList" TabIndex="421" OnTextChanged="ddlFIRCCurrency12_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount12_OB" AutoPostBack="true" runat="server" TabIndex="422" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount12_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr12_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>

                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr12_OB" runat="server" CssClass="dropdownList" TabIndex="423">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate12_OB" runat="server" AutoPostBack="true" TabIndex="424" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate12_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB12_OB" runat="server" TabIndex="425" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode12_OB" runat="server" TabIndex="426" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel12" runat="server" OnClick="btnEFIRCCancel12_Click" CssClass="image-button" />

                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo13_OB" runat="server" TabIndex="427" CssClass="textBox"
                                                                                                                                Width="150px" MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate13_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="428"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender33" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate13_OB" ErrorTooltipEnabled="True"
                                                                                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency13_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency13_OB" runat="server" CssClass="dropdownList" TabIndex="429" OnTextChanged="ddlFIRCCurrency13_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount13_OB" AutoPostBack="true" runat="server" TabIndex="430" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount13_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr13_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr13_OB" runat="server" CssClass="dropdownList" TabIndex="431">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate13_OB" runat="server" AutoPostBack="true" TabIndex="432" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate13_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB13_OB" runat="server" TabIndex="433" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode13_OB" runat="server" TabIndex="434" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel13" runat="server" OnClick="btnEFIRCCancel13_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo14_OB" runat="server" TabIndex="435" CssClass="textBox"
                                                                                                                                Width="150px" MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate14_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="436"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender34" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate14_OB" ErrorTooltipEnabled="True"
                                                                                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency14_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency14_OB" runat="server" CssClass="dropdownList" TabIndex="437" OnTextChanged="ddlFIRCCurrency14_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount14_OB" AutoPostBack="true" runat="server" TabIndex="438" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount14_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr14_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr14_OB" runat="server" CssClass="dropdownList" TabIndex="439">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate14_OB" runat="server" AutoPostBack="true" TabIndex="440" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate14_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB14_OB" runat="server" TabIndex="441" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode14_OB" runat="server" TabIndex="442" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel14" runat="server" OnClick="btnEFIRCCancel14_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td nowrap align="left">
                                                                                                                            <asp:TextBox ID="txtFIRCNo15_OB" runat="server" TabIndex="443" CssClass="textBox"
                                                                                                                                Width="150px" MaxLength="35"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCDate15_OB" runat="server" CssClass="textBox" MaxLength="10"
                                                                                                                                ValidationGroup="dtVal" Width="70px" TabIndex="444"></asp:TextBox>
                                                                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender35" Mask="99/99/9999" MaskType="Date"
                                                                                                                                runat="server" TargetControlID="txtFIRCDate15_OB" ErrorTooltipEnabled="True"
                                                                                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCCurrency15_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="3" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCCurrency15_OB" runat="server" CssClass="dropdownList" TabIndex="445" OnTextChanged="ddlFIRCCurrency15_OB_TextChanged" AutoPostBack="true">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCAmount15_OB" AutoPostBack="true" runat="server" TabIndex="446" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCAmount15_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtFIRCRealisedCurr15_OB" runat="server" TabIndex="55" CssClass="textBox"
																									MaxLength="20" Width="100px"></asp:TextBox>--%>
                                                                                                                            <asp:DropDownList ID="ddlFIRCRealisedCurr15_OB" runat="server" CssClass="dropdownList" TabIndex="447">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCCrossCurrRate15_OB" runat="server" AutoPostBack="true" TabIndex="448" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px" OnTextChanged="txtFIRCCrossCurrRate15_OB_TextChanged"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCTobeAdjustedinSB15_OB" runat="server" TabIndex="449" CssClass="textBox"
                                                                                                                                MaxLength="20" Width="100px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFIRCADCode15_OB" runat="server" TabIndex="450" CssClass="textBox"
                                                                                                                                MaxLength="7" Width="150px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnEFIRCCancel15" runat="server" OnClick="btnEFIRCCancel15_Click" CssClass="image-button" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="right" colspan="5">

                                                                                                                            <asp:Button ID="btn_Trans_FRemove3" runat="server" Text="Remove" CssClass="buttonDefault"
                                                                                                                                TabIndex="451" OnClick="btn_Trans_FRemove3_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td align="right" colspan="5">
                                                            <asp:Button ID="btn_Trans_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                ToolTip="Back to Document Details" OnClientClick="return OnDocNextClick(0);" />&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Trans_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                            ToolTip="Go to G-Base Details" OnClientClick="return OnDocNextClick(2);" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--End--%>
                                            </ContentTemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel ID="tbGBaseDetails" runat="server" HeaderText="G-Base Details"
                                            Font-Bold="true" ForeColor="Lime">
                                            <ContentTemplate>
                                                <table cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="text-align: right; white-space: nowrap">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Operation Type :</span>
                                                        </td>
                                                        <td style="text-align: left; white-space: nowrap">
                                                            <asp:TextBox ID="txtOperationType" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center"
                                                                Width="20px" TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; white-space: nowrap">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Settlement Option :</span>
                                                        </td>
                                                        <td style="text-align: left; white-space: nowrap">
                                                            <asp:TextBox ID="txtSettlementOption" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center"
                                                                Width="20px" TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Country Risk :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRiskCountry" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center"
                                                                Width="20px" TabIndex="91" onfocus="this.select()" MaxLength="2"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; white-space: nowrap">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Payer :</span>
                                                        </td>
                                                        <td style="text-align: left; white-space: nowrap">
                                                            <asp:RadioButton ID="rdbShipper" runat="server" CssClass="elementLabel" Text="Shipper"
                                                                AutoPostBack="true" GroupName="grpPayer" Checked="true" />
                                                            <asp:RadioButton ID="rdbBuyer" runat="server" CssClass="elementLabel" Text="Buyer"
                                                                AutoPostBack="true" GroupName="grpPayer" />
                                                        </td>
                                                        <td style="text-align: right; white-space: nowrap">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Fund Type :</span>
                                                        </td>
                                                        <td style="text-align: left; white-space: nowrap">
                                                            <asp:TextBox ID="txtFundType" runat="server" Width="20px" TabIndex="91" CssClass="textBox"
                                                                Style="vertical-align: middle; text-align: center" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; white-space: nowrap">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Base Rate :</span>
                                                        </td>
                                                        <td style="text-align: left; white-space: nowrap">
                                                            <asp:TextBox ID="txtBaseRate" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center"
                                                                Width="20px" TabIndex="91" onfocus="this.select()" MaxLength="2"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Grade Code :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtGradeCode" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center"
                                                                Width="20px" TabIndex="91" onfocus="this.select()" MaxLength="2"
                                                                Text="99"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Direction :</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtDirection" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center"
                                                                Width="20px" TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Covr Instruction :</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtCovrInstr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center"
                                                                Width="20px" TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; white-space: nowrap">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Internal Rate :</span>
                                                        </td>
                                                        <td style="text-align: left; white-space: nowrap">
                                                            <asp:TextBox ID="txtInternalRate" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="50px" TabIndex="91" onfocus="this.select()" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Spread :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtSpread" runat="server" CssClass="textBox" Style="text-align: right"
                                                                Width="50px" TabIndex="91" onfocus="this.select()" MaxLength="10" ToolTip="Interest Rate - Internal Rate"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Application No. :</span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtApplNo" runat="server" CssClass="textBox" Width="100px" TabIndex="91"
                                                                onfocus="this.select()" MaxLength="7" />
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Remarks(EUC) :</span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:DropDownList ID="ddlRemEUC" runat="server" TabIndex="91" CssClass="dropdownList">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Text="Foreign" Value="Foreign"></asp:ListItem>
                                                                <asp:ListItem Text="Local" Value="Local"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Draft No :</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap" colspan="5">
                                                            <asp:TextBox ID="txtDraftNo" runat="server" CssClass="textBox" Style="width: 150px; vertical-align: middle"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Risk Customer :</span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtRiskCustomer" runat="server" CssClass="textBox" Width="150px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="12" />
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Vessel Name :</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap" colspan="3">
                                                            <asp:TextBox ID="txtVesselName" runat="server" CssClass="textBox" Style="vertical-align: middle"
                                                                Width="200px" TabIndex="91" onfocus="this.select()" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Instructions :</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap" colspan="3">
                                                            <asp:DropDownList ID="ddlInstructions" runat="server" CssClass="dropdownList" TabIndex="91">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="2,4,8,9">2,4,8,9</asp:ListItem>
                                                                <asp:ListItem Value="1,3,8,9">1,3,8,9</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: right; white-space: nowrap">
                                                            <span class="elementLabel">Reimbursing Bank :</span>
                                                        </td>
                                                        <td style="text-align: left; white-space: nowrap" colspan="5">
                                                            <asp:TextBox ID="txtReimbBank" runat="server" AutoPostBack="True" CssClass="textBox"
                                                                Width="60px" TabIndex="91" onkeydown="OpenReimbBankList(this);" ToolTip="Press F2 for help."
                                                                OnTextChanged="txtReimbBank_TextChanged" onfocus="this.select()" MaxLength="7"></asp:TextBox>
                                                            <asp:Button ID="btnReimbBank" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            <asp:Label ID="lblReimbBankDesc" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Remarks :</span>
                                                        </td>
                                                        <td align="left" colspan="4">
                                                            <asp:TextBox ID="txtGBaseRemarks" runat="server" CssClass="textBox" Width="300px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="30" />
                                                        </td>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Merchandise :</span>
                                                        </td>
                                                        <td align="left" colspan="7">
                                                            <asp:TextBox ID="txtMerchandise" runat="server" CssClass="textBox" Style="vertical-align: middle"
                                                                Width="500px" TabIndex="91" onfocus="this.select()" MaxLength="60"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="mandatoryField">*</span><span class="elementLabel">Paying Bank :</span>
                                                        </td>
                                                        <td align="left" colspan="17">
                                                            <asp:TextBox ID="txtPayingBankID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                                onkeydown="OpenPayingBankList(this);" MaxLength="7" TabIndex="91" Width="70px"
                                                                ToolTip="Press F2 for help." OnTextChanged="txtPayingBankID_TextChanged" />
                                                            <asp:Button ID="btnPayingBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            <asp:Label ID="lblPayingBankDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Special Instructions :</span>
                                                        </td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions1" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                            &nbsp;<span class="elementLabelRed">(Total 10 Lines : 77 Characters per line)</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions2" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions3" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions4" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions5" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions6" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions7" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions8" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions9" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td colspan="17" align="left" style="white-space: nowrap">
                                                            <asp:TextBox ID="txtSpecialInstructions10" runat="server" CssClass="textBox" Style="vertical-align: middle;"
                                                                Width="800px" TabIndex="91" onfocus="this.select()" MaxLength="77"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr />
                                                <table width="100%" cellspacing="1" cellpadding="2" border="1">
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">2 - MATU</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">LUMP</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <span class="elementLabel">CONTRACT NO.</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">EX. CCY</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <span class="elementLabel">EXCH. RATE</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <span class="elementLabel">INTL EX. RATE</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">PRINCIPAL</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">X</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">X</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtPrincipalContractNo1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtPrincipalContractNo2" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtPrincipalExchCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtPrincipalExchRate" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtPrincipalIntExchRate" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">INTERTST</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">X</span>
                                                            <td style="text-align: center; white-space: nowrap; width: 10%;">
                                                                <asp:TextBox ID="txtInterestLump" runat="server" CssClass="textBox"
                                                                    MaxLength="1" onfocus="this.select()" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                    TabIndex="91"></asp:TextBox>
                                                            </td>
                                                            <td
                                                                style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                                <asp:TextBox ID="txtInterestContractNo1" runat="server" CssClass="textBox"
                                                                    MaxLength="3" onfocus="this.select()" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                    TabIndex="91"></asp:TextBox>
                                                                &nbsp;
														<asp:TextBox ID="txtInterestContractNo2" runat="server" CssClass="textBox"
                                                            MaxLength="6" onfocus="this.select()" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91"></asp:TextBox>
                                                            </td>
                                                            <td
                                                                style="white-space: nowrap; width: 10%; text-align: center; vertical-align: middle">
                                                                <asp:TextBox ID="txtInterestExchCurr" runat="server" CssClass="textBox"
                                                                    MaxLength="3" onfocus="this.select()" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                    TabIndex="91"></asp:TextBox>
                                                            </td>
                                                            <td
                                                                style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                                <asp:TextBox ID="txtInterestExchRate" runat="server" CssClass="textBox"
                                                                    onfocus="this.select()" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                    TabIndex="91"></asp:TextBox>
                                                            </td>
                                                            <td
                                                                style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                                <asp:TextBox ID="txtInterestIntExchRate" runat="server" CssClass="textBox"
                                                                    onfocus="this.select()" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                    TabIndex="91"></asp:TextBox>
                                                            </td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">COMMISSION</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCommissionMatu" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">X</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtCommissionContractNo1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtCommissionContractNo2" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtCommissionExchCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtCommissionExchRate" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtCommissionIntExchRate" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr />
                                                <table width="100%" cellspacing="1" cellpadding="2" border="1">
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; vertical-align: text-top" rowspan="2"
                                                            align="left">
                                                            <span class="elementLabel">/ CR./</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">CODE</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <span class="elementLabel">CUST. ABBR.</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <span class="elementLabel">A/C NUMBER</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <span class="elementLabel">CCY</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <span class="elementLabel">AMOUNT</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center">
                                                            <span class="elementLabel">PAYER</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCRGLCode" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 50px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtCRCustAbbr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 120px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="12"
                                                                AutoPostBack="true" OnTextChanged="txtCRCustAbbr_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <asp:TextBox ID="txtCRCustAcNo1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtCRCustAcNo2" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCRCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtCRAmount" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center">
                                                            <span class="elementLabel">X</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 30%; text-align: center" colspan="2"></td>
                                                        <td style="white-space: nowrap; text-align: left" colspan="2">
                                                            <span class="elementLabel">INTEREST</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCRIntCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtCRIntAmount" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center">
                                                            <asp:TextBox ID="txtCRIntPayer" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 30%; text-align: center" colspan="2"></td>
                                                        <td style="white-space: nowrap; text-align: left" colspan="2">
                                                            <span class="elementLabel">PAYMENT COMM.</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCRPaymentCommCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtCRPaymentCommAmount" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center">
                                                            <asp:TextBox ID="txtCRPaymentCommPayer" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 30%; text-align: center" colspan="2"></td>
                                                        <td style="white-space: nowrap; text-align: left" colspan="2">
                                                            <span class="elementLabel">HANDLING COMM./COLLECTING CHARGE</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCRHandlingCommCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtCRHandlingCommAmount" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center">
                                                            <asp:TextBox ID="txtCRHandlingCommPayer" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 30%; text-align: center" colspan="2"></td>
                                                        <td style="white-space: nowrap; text-align: left" colspan="2">
                                                            <span class="elementLabel">POSTAGE</span>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCRPostageCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtCRPostageAmount" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center">
                                                            <asp:TextBox ID="txtCRPostagePayer" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 30%; text-align: center" colspan="2"></td>
                                                        <td style="white-space: nowrap; text-align: left" colspan="2"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtCRCurr1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtCRAmount1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center">
                                                            <asp:TextBox ID="txtCRPayer1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 100%; vertical-align: text-top" colspan="7"
                                                            align="left">
                                                            <span class="elementLabel">/ DR./</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 30%; text-align: center" colspan="2"></td>
                                                        <td style="white-space: nowrap; text-align: left" colspan="2"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRCurr" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtDRAmount" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRGLCode1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 50px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtDRCustAbbr1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 120px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="12"
                                                                AutoPostBack="true" OnTextChanged="txtDRCustAbbr1_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <asp:TextBox ID="txtDRCustAcNo11" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtDRCustAcNo12" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRCurr1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtDRAmount1" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRGLCode2" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 50px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtDRCustAbbr2" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 120px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="12"
                                                                AutoPostBack="true" OnTextChanged="txtDRCustAbbr2_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <asp:TextBox ID="txtDRCustAcNo21" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtDRCustAcNo22" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRCurr2" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtDRAmount2" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRGLCode3" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 50px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtDRCustAbbr3" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 120px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="12"
                                                                AutoPostBack="true" OnTextChanged="txtDRCustAbbr3_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <asp:TextBox ID="txtDRCustAcNo31" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtDRCustAcNo32" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRCurr3" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtDRAmount3" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRGLCode4" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 50px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtDRCustAbbr4" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 120px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="12"
                                                                AutoPostBack="true" OnTextChanged="txtDRCustAbbr4_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <asp:TextBox ID="txtDRCustAcNo41" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtDRCustAcNo42" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRCurr4" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtDRAmount4" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center"></td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRGLCode5" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 50px"
                                                                TabIndex="91" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center; vertical-align: middle">
                                                            <asp:TextBox ID="txtDRCustAbbr5" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: left; width: 120px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="12"
                                                                AutoPostBack="true" OnTextChanged="txtDRCustAbbr5_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 20%; text-align: center">
                                                            <asp:TextBox ID="txtDRCustAcNo51" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                            &nbsp;
														<asp:TextBox ID="txtDRCustAcNo52" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 80px"
                                                            TabIndex="91" onfocus="this.select()" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 10%; text-align: center">
                                                            <asp:TextBox ID="txtDRCurr5" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: center; width: 40px"
                                                                TabIndex="91" onfocus="this.select()" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtDRAmount5" runat="server" CssClass="textBox" Style="vertical-align: middle; text-align: right; width: 120px"
                                                                TabIndex="91" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td style="white-space: nowrap; width: 5%; text-align: center"></td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" width="100%" cellpadding="5">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btn_Gbase_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                ToolTip="Back to Transactions" TabIndex="106" OnClientClick="return OnDocNextClick(1);" />&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Gbase_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                            ToolTip="Go to Covering Schedule/GR/PP Details" TabIndex="106" OnClientClick="return OnDocNextClick(3);" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel ID="tbCoveringScheduleDetails" runat="server" HeaderText="Covering Schedule/GR/PP Details"
                                            Font-Bold="true" ForeColor="Lime">
                                            <ContentTemplate>
                                                <table cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td width="2%" valign="top">
                                                            <asp:CheckBox ID="chk1" runat="server" CssClass="elementLabel" TabIndex="89" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB1" runat="server" CssClass="elementLabel" Text="The
				above bill (s) is / are drawn under Letter of Credit No. _______________dated __________"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk2" runat="server" CssClass="elementLabel" TabIndex="90" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB2" runat="server" CssClass="elementLabel" Text="We certify that all terms and conditions of the above credit have been complied
				with."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk3" runat="server" CssClass="elementLabel" TabIndex="91" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB3" runat="server" CssClass="elementLabel" Text="We are holding drawer's Letter of indemnity covering
				the below noted discrepancy(ies)."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk4" runat="server" CssClass="elementLabel" TabIndex="92" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB4" runat="server" CssClass="elementLabel" Text="Please
				deliver documents against payment."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk5" runat="server" CssClass="elementLabel" TabIndex="93" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB5" runat="server" CssClass="elementLabel" Text="Please
				deliver documents against acceptance, and advice us by telex / swift the due date
				of the bill."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk6" runat="server" CssClass="elementLabel" TabIndex="94" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB6" runat="server" CssClass="elementLabel" Text="In Reimbursement , at Maturity / When
				bill paid , please remit proceeds to us by telegraphic transfer to the credit of
				our IBA Account under advice to us held with , "></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk7" runat="server" CssClass="elementLabel" TabIndex="95" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB7" runat="server" CssClass="elementLabel" Text="As
				per terms of credit we have claimed reimbursement on _________________"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk7A" runat="server" CssClass="elementLabel" TabIndex="96" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB7A" runat="server" CssClass="elementLabel" Text="As per terms of credit we shall claim reimbursement on _________________ at
				maturity."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk7B" runat="server" CssClass="elementLabel" TabIndex="97" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB7B" runat="server" CssClass="elementLabel" Text="As per terms of credit kindly authorise
				us to claim reimbursement on ____________ from _______."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk8" runat="server" CssClass="elementLabel" TabIndex="98" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB8" runat="server" CssClass="elementLabel" Text="All banking charges are for account of drawee."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk9" runat="server" CssClass="elementLabel" TabIndex="99" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB9" runat="server" CssClass="elementLabel" Text="Please deliver documents free of payment."></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox
                                                                ID="chk10" runat="server" CssClass="elementLabel" Text="PLEASE DO NOT WAIVE YOUR
				CHARGES."
                                                                TabIndex="100" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="5%" valign="top">
                                                            <asp:CheckBox ID="chk11" runat="server" CssClass="elementLabel" TabIndex="54" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB11" runat="server" CssClass="elementLabel" Text="Please indicate your confirmation
				that the described document are acceptable to you by signing and returning the attached
				copy."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk12" runat="server" CssClass="elementLabel" TabIndex="55" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB12" runat="server" CssClass="elementLabel" Text="Documents will be delivered against
				acceptance/payment of equivalent bill amount plus all other charges."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chk13" runat="server" CssClass="elementLabel" TabIndex="59" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCB13" runat="server" CssClass="elementLabel" Text="Please return the draft duly accepted by the authorised signatory/ies."></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td width="10%" nowrap align="right">
                                                            <span class="elementLabel">Remarks :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="textBox" MaxLength="250" TabIndex="101"
                                                                Width="60%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtRemarks1" runat="server" CssClass="textBox" MaxLength="250" TabIndex="102"
                                                                Width="60%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1"></td>
                                                        <td style="text-align: left" colspan="1">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold">Manual GR :</span>
                                                            <asp:CheckBox ID="chkManualGR" runat="server" CssClass="elementLabel" TabIndex="102"
                                                                onclick="ChangeManualGRText();" />
                                                            <asp:Label ID="lblManualGR" runat="server" CssClass="elementLabel" Style="color: Red;"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;" colspan="1">
                                                            <span class="elementLabel" style="color: Black; font-weight: bold">No of Shipping Bills :</span>
                                                            <asp:TextBox ID="txtNoOfSB" runat="server" CssClass="textBox" TabIndex="102" Width="30px" Text="0" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left;" colspan="1">
                                                            <span class="elementLabel" style="color: Black; font-weight: bold">Pending Shipping Bills :</span>
                                                            <asp:CheckBox ID="chkSB" runat="server" CssClass="elementLabel" TabIndex="102"
                                                                onclick="ChangeSBText();" />
                                                            <asp:Label ID="lblSB" runat="server" CssClass="elementLabel" Style="color: Red;"></asp:Label>
                                                        </td>
                                                        <%-- <td style="text-align: left;" colspan="1">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold">No of Shipping Bills :</span>
                                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textBox" TabIndex="102" Width="30px" MaxLength="3"></asp:TextBox>
                                                        </td>--%>
                                                        <%--<td>--%>
                                                        <%-- <asp:CheckBox
				ID="chkManualGR" runat="server" CssClass="elementLabel" TabIndex="102" onclick="ChangeManualGRText();"/>
				<asp:Label ID="lblManualGR" runat="server" CssClass="elementLabel" style="color:Red;"
				></asp:Label> <span class="elementLabel" style="color:Red;font-weight:bold" >Manual
				GR :</span></td>--%>
                                                    </tr>
                                                </table>
                                                <br />
                                                <center>
                                                    <table cellspacing="0" border="1" width="100%">
                                                        <tr>
                                                            <td colspan="6">
                                                                <span class="elementLabel">GR/PP/Customs Details</span>
                                                            </td>
                                                            <td colspan="5" align="right" nowrap>
                                                                <span class="mandatoryField">Please ensure to click on 
															<b style="color: #007DFB;">Add</b>
                                                                    button before you Save the Bill record.</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" nowrap align="left">
                                                                <span class="elementLabel">Ship. Bill No</span>
                                                            </td>
                                                            <td width="10%" nowrap >
                                                                <span class="elementLabel">Ship. Date</span>
                                                            </td>
                                                            <td width="10%">
                                                                <span class="elementLabel">Port Code</span>
                                                            </td>
                                                            <td width="10%" nowrap>
                                                                <span class="elementLabel">Type</span>
                                                            </td>
                                                            <%-- <td width="10%"
				nowrap> <span class="elementLabel">Export Agency</span> </td>--%>
                                                            <%-- <td width="10%"
				nowrap> <span class="elementLabel">Dispatch Ind.</span> </td>--%>
                                                            <td width="10%" nowrap>
                                                                <span class="elementLabel">GR/PP/Cust.No.</span>
                                                            </td>
                                                            <td width="5%" nowrap>
                                                                <span class="elementLabel">Currency</span>
                                                            </td>
                                                            <td width="10%" nowrap>
                                                                <span class="elementLabel">Amount</span>
                                                            </td>
                                                            <%-- <td width="12%" nowrap> <span class="elementLabel">Exch Rate</span> </td>
				<td width="10%" nowrap> <span class="elementLabel">Amt in INR</span> </td>--%>
                                                            <td>
                                                                <span class="elementLabel">Invoice Sr No.</span>
                                                            </td>
                                                            <td width="10%" nowrap colspan="2">
                                                                <span class="elementLabel">Invoice No.</span>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtShippingBillNo" runat="server" Width="70px" TabIndex="103" MaxLength="30"
                                                                    CssClass="textBox" onkeydown="return shipbillnohelp('113');"></asp:TextBox>
                                                                <asp:Button ID="btn_shipbillnohelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                    OnClientClick="return shipbillnohelp('mouseClick');" />
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtShippingBillDate" runat="server" Width="70px" TabIndex="104"
                                                                    CssClass="textBox"></asp:TextBox>
                                                                <ajaxToolkit:MaskedEditExtender ID="mdShippingDate" Mask="99/99/9999" MaskType="Date"
                                                                    runat="server" TargetControlID="txtShippingBillDate" ErrorTooltipEnabled="True"
                                                                    CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                    CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                    CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                                                </ajaxToolkit:MaskedEditExtender>
                                                            </td>
                                                            <td align="left" id="portcode">
                                                                <asp:DropDownList ID="ddlPortCode" runat="server" CssClass="dropdownList" TabIndex="105"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPortCode_SelectedIndexChanged"
                                                                    Width="90px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlFormType" runat="server" CssClass="dropdownList" TabIndex="106"
                                                                    Width="80px">
                                                                    <asp:ListItem Text="GOODS" Value="GOODS"></asp:ListItem>
                                                                    <asp:ListItem Text="SOFTEX" Value="SOFTEX"></asp:ListItem>
                                                                    <asp:ListItem Text="ROYALTY" Value="ROYALTY"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <%-- <td align="left"> <asp:DropDownList ID="ddlExportAgency" runat="server"
				CssClass="dropdownList" TabIndex="107" Width="84px"> <asp:ListItem Value="Customs">Customs</asp:ListItem>
				<asp:ListItem Value="SEZ">SEZ</asp:ListItem> <asp:ListItem Value="STPI">STPI</asp:ListItem>
				<asp:ListItem Value="Status holder exporters">Status holder exporters</asp:ListItem>
				<asp:ListItem Value="100% EOU">100% EOU</asp:ListItem> <asp:ListItem Value="Warehouse
				export">Warehouse export</asp:ListItem> </asp:DropDownList> </td>--%>
                                                            <%-- <td align="left"
				style="margin-left: 40px"> <asp:DropDownList ID="DropDownList1" runat="server" CssClass="dropdownList"
				TabIndex="108" Width="100px"> <asp:ListItem Value="Dispatched directly by exporter">By
				Exporter</asp:ListItem> <asp:ListItem Value="By Bank">By Bank</asp:ListItem> </asp:DropDownList>
				</td>--%>
                                                            <td>
                                                                <asp:TextBox ID="txtGRPPCustomsNo" runat="server" Width="70px" TabIndex="109" MaxLength="30"
                                                                    CssClass="textBox"></asp:TextBox>
                                                            </td>
                                                            <td id="Td1" align="left" runat="server">
                                                                <asp:DropDownList ID="ddlCurrencyGRPP" runat="server" CssClass="dropdownList" TabIndex="110">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAmountGRPP" runat="server" Width="85px" TabIndex="111" MaxLength="20"
                                                                    onfocus="this.select()" CssClass="textBox"></asp:TextBox>
                                                            </td>
                                                            <%-- <td align="left"> <asp:TextBox ID="txtExchRateGR"
				runat="server" Width="90px" TabIndex="112" MaxLength="20" onfocus="this.select()"
				CssClass="textBox"></asp:TextBox> </td>--%>
                                                            <%-- <td align="left"> <asp:TextBox
				ID="txtAmountinINRGR" runat="server" Width="90px" TabIndex="113" MaxLength="20"
				onfocus="this.select()" CssClass="textBox"></asp:TextBox> </td>--%>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox runat="server" ID="txt_invsrno" CssClass="textBox" Width="70px" MaxLength="6"
                                                                    TabIndex="114" OnTextChanged="txt_invsrno_TextChanged" AutoPostBack="true" />
                                                                <asp:Button ID="btn_invsrno" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtInvoiceNum" runat="server" Width="140px" TabIndex="115" MaxLength="30"
                                                                    CssClass="textBox"></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="11%" nowrap>
                                                                <span class="elementLabel">Invoice Date</span>
                                                            </td>
                                                            <td width="11%" nowrap>
                                                                <span class="elementLabel">Invoice Amt</span>
                                                            </td>
                                                            <td width="11%" nowrap>
                                                                <span class="elementLabel">Freight Amt</span>
                                                            </td>
                                                            <td width="11%" nowrap>
                                                                <span class="elementLabel">Insurance Amt</span>
                                                            </td>
                                                            <td width="11%">
                                                                <span class="elementLabel">Discount Amt</span>
                                                            </td>
                                                            <td width="11%">
                                                                <span class="elementLabel">Comm. Amt.</span>
                                                            </td>
                                                            <td width="11%" nowrap>
                                                                <span class="elementLabel">Oth. Ded. Chrgs</span>
                                                            </td>
                                                            <td width="11%">
                                                                <span class="elementLabel">Packing Chrgs</span>
                                                            </td>
                                                            <td width="12%">
                                                                <span class="elementLabel">Status</span>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtInvoiceDt" runat="server" Width="70px" TabIndex="116" MaxLength="10"
                                                                    CssClass="textBox"></asp:TextBox>
                                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender20" Mask="99/99/9999" MaskType="Date"
                                                                    runat="server" TargetControlID="txtInvoiceDt" InputDirection="RightToLeft" AcceptNegative="Left"
                                                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                                                </ajaxToolkit:MaskedEditExtender>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtInvoiceAmt" runat="server" Width="85px" TabIndex="117" MaxLength="20"
                                                                    onfocus="this.select()" CssClass="textBox" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFreight" runat="server" CssClass="textBox" Width="80px" TabIndex="118"
                                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtInsurance" runat="server" CssClass="textBox" Width="80px" TabIndex="119"
                                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="textBox" Width="80px" TabIndex="120"
                                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCommissionGRPP" runat="server" Width="80px" TabIndex="121" MaxLength="20"
                                                                    CssClass="textBox" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtOthDeduction" runat="server" CssClass="textBox" Width="80px"
                                                                    TabIndex="122" onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPacking" runat="server" CssClass="textBox" Width="80px" TabIndex="123"
                                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" CssClass="textBox" ID="txt_status" TabIndex="123" Width="85px"
                                                                    Style="font-weight: bold" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnAddGRPPCustoms" runat="server" Text="Add" CssClass="buttonDefault"
                                                                    TabIndex="124" OnClick="btnAddinGrid_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td>
                                                                <span class="elementLabel">Freight Curr</span>
                                                            </td>
                                                            <td>
                                                                <span class="elementLabel">Ins. Curr</span>
                                                            </td>
                                                            <td>
                                                                <span class="elementLabel">Dis. Curr</span>
                                                            </td>
                                                            <td>
                                                                <span class="elementLabel">Comm. Curr</span>
                                                            </td>
                                                            <td>
                                                                <span class="elementLabel">Oth. Ded. Curr</span>
                                                            </td>
                                                            <td nowrap>
                                                                <span class="elementLabel">Pack. Chgs. Curr</span>
                                                            </td>
                                                            <td style="white-space: nowrap">
                                                                <span class="elementLabel">Exch Rate</span>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlFreightCurr" runat="server" CssClass="dropdownList" TabIndex="125">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlInsCurr" runat="server" CssClass="dropdownList" TabIndex="126">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDiscCurr" runat="server" CssClass="dropdownList" TabIndex="127">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCommCurr" runat="server" CssClass="dropdownList" TabIndex="128">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlOthDedCurr" runat="server" CssClass="dropdownList" TabIndex="129">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPackChgCurr" runat="server" CssClass="dropdownList" TabIndex="130">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGRExchRate" runat="server" CssClass="textBox" Width="80px" TabIndex="130"
                                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="10">
                                                            <asp:GridView ID="GridViewGRPPCustomsDetails" runat="server" AutoGenerateColumns="false"
                                                                Width="100%" GridLines="Both" AllowPaging="true" PageSize="40" OnRowDataBound="GridViewGRPPCustomsDetails_RowDataBound">
                                                                <PagerSettings Visible="false" />
                                                                <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                                    CssClass="gridAlternateItem" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%#
				Eval("SrNo") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFormType" runat="server" Text='<%# Eval("FormType")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:TemplateField HeaderText="Exp. Agency" HeaderStyle-HorizontalAlign="Left"
				ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
				<ItemTemplate> <asp:Label ID="lblExpAgency" runat="server" Text='<%# Eval("ExportAgency")
				%>' CssClass="elementLabel"></asp:Label> </ItemTemplate> <HeaderStyle HorizontalAlign="Left"
				Width="10%" /> <ItemStyle HorizontalAlign="Left" Width="10%" /> </asp:TemplateField>--%>
                                                                    <%-- <asp:TemplateField HeaderText="Disp. Ind." HeaderStyle-HorizontalAlign="Left"
				ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%" ItemStyle-Width="8%"> <ItemTemplate>
				<asp:Label ID="lblDispInd" runat="server" Text='<%# Eval("DispInd") %>' CssClass="elementLabel"></asp:Label>
				</ItemTemplate> <HeaderStyle HorizontalAlign="Left" Width="8%" /> <ItemStyle HorizontalAlign="Left"
				Width="8%" /> </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Cust. No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGR" runat="server" Text='<%#
				Eval("GR") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Curr." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("GRCurrency") %>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ship.Amt." HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:0.00}")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:TemplateField HeaderText="Exch Rate" HeaderStyle-HorizontalAlign="Right"
				ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
				<ItemTemplate> <asp:Label ID="lblExchRate" runat="server" Text='<%# Eval("ExchRate","{0:0.00}")
				%>' CssClass="elementLabel"></asp:Label> </ItemTemplate> <HeaderStyle HorizontalAlign="Left"
				Width="10%" /> <ItemStyle HorizontalAlign="Right" Width="10%" /> </asp:TemplateField>--%>
                                                                    <%-- <asp:TemplateField HeaderText="Amt in INR" HeaderStyle-HorizontalAlign="Right"
				ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
				<ItemTemplate> <asp:Label ID="lblAmountinINR" runat="server" Text='<%# Eval("AmtinINR","{0:0.00}")
				%>' CssClass="elementLabel"></asp:Label> </ItemTemplate> <HeaderStyle HorizontalAlign="Left"
				Width="10%" /> <ItemStyle HorizontalAlign="Right" Width="10%" /> </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="InvSrNo." HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblinvsrno" runat="server" Text='<%# Eval("Invoicesrno") %>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Shipp. Bill No." HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblShippingBillNo" runat="server" Text='<%# Eval("Shipping_Bill_No") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ship. Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblShippingBillDate" runat="server" Text='<%# Eval("Shipping_Bill_Date","{0:dd/MM/yyyy}")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Invoice No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0:dd/MM/yyyy}") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Inv.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoiceAmt" runat="server" Text='<%# Eval("InvoiceAmt","{0:0.00}")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Frgt.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFreightAmt" runat="server" Text='<%# Eval("FreightAmount","{0:0.00}") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Insur.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInsuranceAmt" runat="server" Text='<%# Eval("InsuranceAmount","{0:0.00}")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dis.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDiscountAmt" runat="server" Text='<%# Eval("DiscountAmt","{0:0.00}") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Comm." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCommission" runat="server" Text='<%# Eval("Commission","{0:0.00}")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Oth Ded Charges" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOthDeduction" runat="server" Text='<%# Eval("OtherDeductionAmt","{0:0.00}")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pack. Chrgs" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPacking" runat="server" Text='<%# Eval("PackingCharges","{0:0.00}") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")
				%>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPortCode" runat="server" Text='<%# Eval("PortCode") %>' CssClass="elementLabel"></asp:Label>
                                                                            <asp:Label ID="lblFrieghtCurr" runat="server" Text='<%# Eval("FreightCurr") %>' Style="display: none;"></asp:Label>
                                                                            <asp:Label ID="lblInsCurr" runat="server" Text='<%#
				Eval("InsuranceCurr") %>'
                                                                                Style="display: none;"></asp:Label>
                                                                            <asp:Label ID="lblDisCurr" runat="server" Text='<%# Eval("DiscountCurr") %>' Style="display: none;"></asp:Label>
                                                                            <asp:Label ID="lblCommCurr" runat="server" Text='<%# Eval("CommCurr") %>' Style="display: none;"></asp:Label>
                                                                            <asp:Label ID="lblOtherDedCurr" runat="server" Text='<%# Eval("OthDedCurr")
				%>'
                                                                                Style="display: none;"></asp:Label>
                                                                            <asp:Label ID="lblPackingChgCurr" runat="server" Text='<%# Eval("PackingChrgCurr") %>'
                                                                                Style="display: none;"></asp:Label>
                                                                            <asp:Label ID="lblGRExchRate" runat="server" Text='<%# Eval("ExchRate") %>' Style="display: none;"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%#
				Eval("SrNo") %>'
                                                                                CommandName="RemoveRecord" CssClass="deleteButton" OnClick="LinkButtonClick" Text="Delete" ToolTip="Delete Record" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                                    <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="MakerVisible" runat="server">
                                                                <td align="center" style="padding-top: 10px; padding-bottom: 10px; white-space: nowrap">
                                                                    <asp:Button ID="btn_GRPP_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                        ToolTip="Back to G-Base Details" TabIndex="106" OnClientClick="return OnDocNextClick(2);" />
                                                                    &nbsp;<asp:Button ID="btnLEI" runat="server" Text="Verify LEI" CssClass="buttonDefault"
                                                                        ToolTip="Verify LEI" TabIndex="132" OnClick="btnLEI_Click" Visible="true" />
                                                                    &nbsp;<asp:Button ID="btnSaveDraft" runat="server" Text="Save & Draft" CssClass="buttonDefault"
                                                                        ToolTip="Save & Draft" OnClick="btnSaveDraft_Click" TabIndex="133" CommandName="1"
                                                                        CommandArgument="save" />
                                                                    &nbsp;<asp:Button ID="btnSave" runat="server" Text="Submit To Checker" CssClass="buttonDefault" Width="125px"
                                                                        ToolTip="Send To Checker" OnClick="btnSave_Click" TabIndex="132" CommandName="1" CommandArgument="Submit" />
                                                                    <%--&nbsp;<asp:Button ID="btnSavePrint" runat="server" Text="Save & Print" CssClass="buttonDefault"
									ToolTip="Save & Print Export Bill lodgement Advice" OnClick="btnSavePrint_Click" TabIndex="134"
									CommandName="2" CommandArgument="print" Visible="false" />--%>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                                        ToolTip="Cancel" OnClick="btnCancel_Click" TabIndex="135" Style="visibility: hidden;" />
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                    </table>

                                                </center>

                                            </ContentTemplate>
                                        </ajaxToolkit:TabPanel>
                                    </ajaxToolkit:TabContainer>
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

            var txtExchRate = $get("TabContainerMain_tbTransactionDetails_txtExchRate");
            if (txtExchRate.value == '')
                txtExchRate.value = 0;
            txtExchRate.value = parseFloat(txtExchRate.value).toFixed(10);

            var txtLibor = $get("TabContainerMain_tbTransactionDetails_txtLibor");
            if (txtLibor.value == '')
                txtLibor.value = 0;
            txtLibor.value = parseFloat(txtLibor.value).toFixed(6);

            var txtIntRate1 = $get("TabContainerMain_tbTransactionDetails_txtIntRate1");
            if (txtIntRate1.value == '')
                txtIntRate1.value = 0.00;
            //txtIntRate1.value = parseFloat(txtIntRate1.value).toFixed(2);
            txtIntRate1.value = parseFloat(txtIntRate1.value);

            var txtIntRate2 = $get("TabContainerMain_tbTransactionDetails_txtIntRate2");
            if (txtIntRate2.value == '')
                txtIntRate2.value = 0;
            txtIntRate2.value = parseFloat(txtIntRate2.value).toFixed(2);

            var txtIntRate3 = $get("TabContainerMain_tbTransactionDetails_txtIntRate3");
            if (txtIntRate3.value == '')
                txtIntRate3.value = 0;
            txtIntRate3.value = parseFloat(txtIntRate3.value).toFixed(2);

            var txtIntRate4 = $get("TabContainerMain_tbTransactionDetails_txtIntRate4");
            if (txtIntRate4.value == '')
                txtIntRate4.value = 0;
            txtIntRate4.value = parseFloat(txtIntRate4.value).toFixed(2);

            var txtIntRate5 = $get("TabContainerMain_tbTransactionDetails_txtIntRate5");
            if (txtIntRate5.value == '')
                txtIntRate5.value = 0;
            txtIntRate5.value = parseFloat(txtIntRate5.value).toFixed(2);

            var txtIntRate6 = $get("TabContainerMain_tbTransactionDetails_txtIntRate6");
            if (txtIntRate6.value == '')
                txtIntRate6.value = 0;
            txtIntRate6.value = parseFloat(txtIntRate6.value).toFixed(2);

            var txtBillAmount = $get("TabContainerMain_tbTransactionDetails_txtBillAmount");
            if (txtBillAmount.value == '')
                txtBillAmount.value = 0;
            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);

            var txtBillAmountinRS = $get("TabContainerMain_tbTransactionDetails_txtBillAmountinRS");
            if (txtBillAmountinRS.value == '')
                txtBillAmountinRS.value = 0;
            txtBillAmountinRS.value = parseFloat(txtBillAmountinRS.value).toFixed(2);

            var txtNegotiatedAmt = $get("TabContainerMain_tbTransactionDetails_txtNegotiatedAmt");
            if (txtNegotiatedAmt.value == '')
                txtNegotiatedAmt.value = 0;
            txtNegotiatedAmt.value = parseFloat(txtNegotiatedAmt.value).toFixed(2);

            var txtNegotiatedAmtinRS = $get("TabContainerMain_tbTransactionDetails_txtNegotiatedAmtinRS");
            if (txtNegotiatedAmtinRS.value == '')
                txtNegotiatedAmtinRS.value = 0;
            txtNegotiatedAmtinRS.value = parseFloat(txtNegotiatedAmtinRS.value).toFixed(2);

            var txtInterest = $get("TabContainerMain_tbTransactionDetails_txtInterest");
            if (txtInterest.value == '')
                txtInterest.value = 0;
            txtInterest.value = parseFloat(txtInterest.value).toFixed(2);


            var txtInterestinRS = $get("TabContainerMain_tbTransactionDetails_txtInterestinRS");
            if (txtInterestinRS.value == '')
                txtInterestinRS.value = 0;
            txtInterestinRS.value = parseFloat(txtInterestinRS.value).toFixed(2);

            var txtNetAmt = $get("TabContainerMain_tbTransactionDetails_txtNetAmt");
            if (txtNetAmt.value == '')
                txtNetAmt.value = 0;
            txtNetAmt.value = parseFloat(txtNetAmt.value).toFixed(2);


            var txtfbkchrg = $get("TabContainerMain_tbTransactionDetails_txt_fbkcharges");
            if (txtfbkchrg.value == '')
                txtfbkchrg.value = 0;
            txtfbkchrg.value = parseFloat(txtfbkchrg.value).toFixed(2);

            var txtfbkinRS = $get("TabContainerMain_tbTransactionDetails_txt_fbkchargesinRS");
            if (txtfbkinRS.value == '')
                txtfbkinRS.value = 0;
            txtfbkinRS.value = parseFloat(txtfbkinRS.value).toFixed(2);

            var txtNetAmtinRS = $get("TabContainerMain_tbTransactionDetails_txtNetAmtinRS");
            if (txtNetAmtinRS.value == '')
                txtNetAmtinRS.value = 0;
            txtNetAmtinRS.value = parseFloat(txtNetAmtinRS.value).toFixed(2);



            var txtOtherChrgs = $get("TabContainerMain_tbTransactionDetails_txtOtherChrgs");
            if (txtOtherChrgs.value == '')
                txtOtherChrgs.value = 0;
            txtOtherChrgs.value = parseFloat(txtOtherChrgs.value).toFixed(2);

            var txtBankCert = $get("TabContainerMain_tbTransactionDetails_txtBankCert");
            if (txtBankCert.value == '')
                txtBankCert.value = 0;
            txtBankCert.value = parseFloat(txtBankCert.value).toFixed(2);

            var txtNegotiationFees = $get("TabContainerMain_tbTransactionDetails_txtNegotiationFees");
            if (txtNegotiationFees.value == '')
                txtNegotiationFees.value = 0;
            txtNegotiationFees.value = parseFloat(txtNegotiationFees.value).toFixed(2);

            var txtCourierChrgs = $get("TabContainerMain_tbTransactionDetails_txtCourierChrgs");
            if (txtCourierChrgs.value == '')
                txtCourierChrgs.value = 0;
            txtCourierChrgs.value = parseFloat(txtCourierChrgs.value).toFixed(2);

            var txtMarginAmt = $get("TabContainerMain_tbTransactionDetails_txtMarginAmt");
            if (txtMarginAmt.value == '')
                txtMarginAmt.value = 0;
            txtMarginAmt.value = parseFloat(txtMarginAmt.value).toFixed(2);

            var txtCommission = $get("TabContainerMain_tbTransactionDetails_txtCommission");
            if (txtCommission.value == '')
                txtCommission.value = 0;
            txtCommission.value = parseFloat(txtCommission.value).toFixed(2);

            var txtSTaxAmount = $get("TabContainerMain_tbTransactionDetails_txtSTaxAmount");
            if (txtSTaxAmount.value == '')
                txtSTaxAmount.value = 0;
            txtSTaxAmount.value = parseFloat(txtSTaxAmount.value).toFixed(2);

            var txtSTFXDLS = $get("TabContainerMain_tbTransactionDetails_txtSTFXDLS");
            if (txtSTFXDLS.value == '')
                txtSTFXDLS.value = 0;
            txtSTFXDLS.value = parseFloat(txtSTFXDLS.value).toFixed(2);

            var txtCurrentAcinRS = $get("TabContainerMain_tbTransactionDetails_txtCurrentAcinRS");
            if (txtCurrentAcinRS.value == '')
                txtCurrentAcinRS.value = 0;
            txtCurrentAcinRS.value = parseFloat(txtCurrentAcinRS.value).toFixed(2);

            var txtExchRtEBR = $get("TabContainerMain_tbTransactionDetails_txtExchRtEBR");
            if (txtExchRtEBR.value == '')
                txtExchRtEBR.value = 0;
            txtExchRtEBR.value = parseFloat(txtExchRtEBR.value).toFixed(10);

            var txtAmountGRPP = $get("TabContainerMain_tbCoveringScheduleDetails_txtAmountGRPP");
            if (txtAmountGRPP.value == '')
                txtAmountGRPP.value = 0;
            txtAmountGRPP.value = parseFloat(txtAmountGRPP.value).toFixed(2);

            ChangeManualGRText();



        }
    </script>
</body>
</html>
