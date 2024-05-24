<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditFullRealisationPRN_Checker.aspx.cs" Inherits="EXP_EXP_AddEditFullRealisationPRN_Checker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <%--<script language="javascript" src="../Scripts/rightClick.js" type="text/javascript"></script>--%>
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/TF_EXP_INW_REMMITENCE_CHECKER.js"></script>
    <script type="text/javascript" src="Scripts/TF_EXP_Settlement_Checker.js"></script>
    <style type="text/css">
    .lblHelp_TT
        {
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
    </style>
    <script language="javascript" type="text/javascript">

        function checkSysDate() {

            //var gDate = document.getElementById('txtRemDate');
            var obj = document.getElementById('txtDateRealised');
            var day = obj.value.split("/")[0];
            var month = obj.value.split("/")[1];
            var year = obj.value.split("/")[2];

            if ((day < 1 || day > 31) || (month < 1 && month > 12) && (year.length != 4)) {

                alert("Invalid Date Format");
                document.getElementById('txtDateRealised').focus();
                return false;
            }

            else {

                var dt = new Date(year, month - 1, day);
                var today = new Date();
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert("Invalid Realised Date");
                    document.getElementById('txtDateRealised').focus();
                    return false;
                }
            }
        }

        function checkSysDate1() {

            //var gDate = document.getElementById('txtRemDate');
            var obj = document.getElementById('txtValueDate');
            var day = obj.value.split("/")[0];
            var month = obj.value.split("/")[1];
            var year = obj.value.split("/")[2];

            if ((day < 1 || day > 31) || (month < 1 && month > 12) && (year.length != 4)) {

                alert("Invalid Date Format");
                document.getElementById('txtValueDate').focus();
                return false;
            }

            else {

                var dt = new Date(year, month - 1, day);
                var today = new Date();
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert("Invalid Value Date");
                    document.getElementById('txtValueDate').focus();
                    return false;
                }
            }
        }

        function DocHelp() {

            var type = document.getElementById('txtDocPrFx').value;
            var branchcode = document.getElementById('hdnBranchCode').value;
            var year = document.getElementById('hdnYear').value;
            var docno = document.getElementById('txtDocNo').value;
            popup = window.open('ExportDocHelp.aspx?DocPrFx=' + type + '&year=' + year + '&BranchCode=' + branchcode + '&docno=' + docno, 'HelpDocId', 'height=380,  width=620,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "HelpDocId";
            return false;
        }

        function OpenDocList(event) {
            var key = event.keyCode;

            if (key == 113 && key != 13) {
                document.getElementById('btnDocNo').click();
                return false;
            }
            return true;
        }

        function OpenDocNoList(e) {

            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {

                var branchCode = document.getElementById('hdnBranchCode');
                var custAcNo = document.getElementById('txtCustAcNo');
                open_popup('EXP_TTdocumentNoLookUp.aspx?branch=' + branchCode.value + '&custAcNo=' + custAcNo.value, 450, 550, 'DocNoList');
                return false;
            }
        }

        function selectDocNo(selectedID) {
            var id = selectedID;
            document.getElementById('txtTTRefNo').value = id;

        }

        function curhelp() {

            popup = window.open('../TF_CurrencyLookUp2.aspx', 'helpCurId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCurId";
            return false;

        }

        function curhelp1() {

            popup = window.open('../TF_CurrencyLookUp2.aspx', 'helpCurId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCurId1";
            return false;

        }

        function curhelp3() {

            popup = window.open('../TF_CurrencyLookUp2.aspx', 'helpCurId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCurId2";
            return false;

        }

        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "HelpDocId") {
                document.getElementById('txtDocNo').value = s;
                var s1 = popup.document.getElementById('txtcell2').value;
                document.getElementById('amtbalforreal').value = s1;
            }

            if (common == "helpCurId") {
                document.getElementById('txtEEFCCurrency').value = s;
            }

            if (common == "helpCurId1") {
                document.getElementById('txt_relcur').value = s;

            }

            if (common == "helpCurId2") {
                document.getElementById('txtConCrossCur').value = s;

            }

        }

        function OpenCommissionList() {
            var custacno = document.getElementById('txtCustAcNo');
            open_popup('EXP_CommisionLookUp.aspx?custacno=' + custacno.value, 450, 650, 'CommisionList');
            return false;
        }

        function selectCommision(srNo, comRate, MinAmt, MaxAmt, FlatAmt) {
            document.getElementById('txtCommissionID').value = srNo;
            document.getElementById('hdnCommRate').value = comRate;
            document.getElementById('hdnCommMinAmt').value = MinAmt;
            document.getElementById('hdnCommMaxAmt').value = MaxAmt;
            document.getElementById('hdnCommFlat').value = FlatAmt;

            calCommission();
            //return true;
            //alert('hi');
        }

        function OpenProfitLieuList() {
            var custacno = document.getElementById('txtCustAcNo');
            open_popup('EXP_ProfitLieoLookUp.aspx?custacno=' + custacno.value, 450, 650, 'ProfitLieoList');
            return false;
        }

        function selectProfitlieo(srNo, comRate, MinAmt, MaxAmt, FlatAmt) {
            document.getElementById('txtprofitper').value = srNo;
            document.getElementById('hdnProfitRate').value = comRate;
            document.getElementById('hdnProfitMinAmt').value = MinAmt;
            document.getElementById('hdnProfitMaxAmt').value = MaxAmt;
            document.getElementById('hdnProfitFlat').value = FlatAmt;

            calProfitLieo();
            //return true;
            //alert('hi');
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function chkdocno() {

            var docno = document.getElementById('txtDocNo');
            var fc;
            if (docno.value != '') {

                fc = document.getElementById('txtDocNo').value.substring(0, 1).toString();
                fc = fc.toUpperCase();
                document.getElementById('txtDocNo').value = fc + document.getElementById('txtDocNo').value.substring(1);
            }
        }

        function checkExchangeRate() {
            var exrate = document.getElementById('txtExchangeRate');

            //            var txtDocPrFx = document.getElementById('txtDocPrFx');
            //            var txtCommission = document.getElementById('txtCommission');
            //            
            //            var txtAmtRealisedinINR = document.getElementById('txtAmtRealisedinINR');


            if (exrate.value == '') {
                exrate.value = 1;
                exrate.value = parseFloat(exrate.value).toFixed(10);
            }
            if (exrate.value != '')
                exrate.value = parseFloat(exrate.value).toFixed(10);

            ////            if (txtDocPrFx.value == "BCA" || txtDocPrFx.value == "BCU") {
            ////                txtCommission.value =  parseFloat( parseFloat(txtAmtRealisedinINR.value) * parseFloat(0.0625));

            ////                if (parseFloat(txtCommission.value) < 500)
            ////                    txtCommission.value = "500.00";
            ////            }

        }

        function chkamt() {
            var relamt = document.getElementById('txt_relamount');

            if (relamt.value == '') {

                alert('Amount cannot be blank.');
                //document.getElementById('txt_relamount').focus();

                return false;
            }
            else {
                relamt.value = parseFloat(relamt.value).toFixed(2);
            }


        }

        function calfamtrealised() {
            var relamt = document.getElementById('txt_relamount');
            var crosscurrate = document.getElementById('txtRelCrossCurRate');
            var amtrealised = document.getElementById('txtAmtRealised');
            if (crosscurrate.value == '') {

                alert('Cross Curr. Rate cannot be blank.');
                //document.getElementById('txtRelCrossCurRate').focus();

                return false;
            }
            if (relamt.value != '' && crosscurrate.value != '') {
                crosscurrate.value = parseFloat(crosscurrate.value).toFixed(6);
                amtrealised.value = parseFloat(relamt.value * crosscurrate.value).toFixed(2);
            }
        }

        function amtrealised() {
            calrealisedAmtinINR();
            __doPostBack('txtAmtRealised', '');

        }

        function calrealisedAmtinINR() {
            //alert('hello');
            var realamt = document.getElementById('txtAmtRealised');
            var exrate = document.getElementById('txtExchangeRate');
            var billamt = document.getElementById('txtBillAmt');
            var negoamt = document.getElementById('txtNegotiatedAmt');
            var loan = document.getElementById('loan1');
            var delinkdate = document.getElementById('txtDateDelinked');
            var cur = document.getElementById('txtCurrency');
            var exrate1 = document.getElementById('exchangerate');
            var balamtforreal = document.getElementById('amtbalforreal');
            var cal;
            var cal1;
            var chkfullpart;
            var bal;
            var bal1;
            var bal2;
            var mode = document.getElementById('hdnmode');
            var noneefc = document.getElementById('txtEEFCAmtTotal');
            var PartConAmt = document.getElementById('txtTotConRate');
            var salerate = document.getElementById('overrate');
            var eefc = document.getElementById('txtTotConRate');

            if (document.getElementById('rdbFull').checked == true)
                chkfullpart = "F";
            if (document.getElementById('rdbPart').checked == true)
                chkfullpart = "P";
            if (realamt.value == '') {

                alert('Realised Amount cannot be blank.');
                //document.getElementById('txtAmtRealised').focus();

                return false;
            }

            if (negoamt.value != '') {

                negoamt.value = parseFloat(negoamt.value).toFixed(2);
            }

            if (realamt.value != '') {

                realamt.value = parseFloat(realamt.value).toFixed(2);
            }

            if (billamt.value != '') {
                billamt.value = parseFloat(billamt.value).toFixed(2);
            }

            //            if (parseFloat(realamt.value) > parseFloat(billamt.value)) {

            //                alert('Realised amount cannot be greater than Bill Amount');
            //                document.getElementById('txtAmtRealised').value = "";
            //                document.getElementById('txtAmtRealised').focus();

            //            }

            //            if (parseFloat(realamt.value) > parseFloat(balamtforreal.value)) {

            //                alert('Realised amount cannot be greater than balance amount for realisation');
            //                document.getElementById('txtAmtRealised').value = "";
            //                document.getElementById('txtAmtRealised').focus();

            //            }

            if (realamt.value != '' && exrate.value != '') {

                cal = Math.round(parseFloat(realamt.value * exrate.value)).toFixed(2);
                document.getElementById('txtAmtRealisedinINR').value = cal;
            }
            if (realamt.value != '' && billamt.value != '') {

                if (loan.value == "Y" && cur.value != "INR" && delinkdate.value == '') {

                    if (parseFloat(balamtforreal.value) != parseFloat(negoamt.value) || parseFloat(balamtforreal.value) != parseFloat(billamt.value)) {

                        cal = parseFloat(negoamt.value - realamt.value).toFixed(2);
                        if (cal < 0)
                            cal = 0;
                        if (chkfullpart == "F")
                            document.getElementById('txtOtherBank').value = cal;
                        else {
                            cal = 0;
                            document.getElementById('txtOtherBank').value = parseFloat(cal).toFixed(2);
                        }
                        //document.getElementById('txtOtherBank').value = cal;
                        //document.getElementById('txtOtherBank').focus();
                        cal1 = parseFloat(cal * exrate.value).toFixed(2);
                        document.getElementById('txtOtherBankinINR').value = cal1;
                    }
                    else {
                        cal = parseFloat(negoamt.value - realamt.value).toFixed(2);
                        if (chkfullpart == "F")
                            document.getElementById('txtOtherBank').value = cal;
                        else {
                            cal = 0;
                            document.getElementById('txtOtherBank').value = parseFloat(cal).toFixed(2);
                        }
                        //document.getElementById('txtOtherBank').value = cal;
                        //document.getElementById('txtOtherBank').focus();
                        cal1 = parseFloat(cal * exrate.value).toFixed(2);
                        //document.getElementById('txtOtherBankinINR').value = cal1;
                    }
                }
                else {
                    if (mode.value == "add") {
                        cal = parseFloat(balamtforreal.value - realamt.value).toFixed(2);
                        if (chkfullpart == "F")
                            document.getElementById('txtOtherBank').value = cal;
                        else {
                            cal = 0;
                            document.getElementById('txtOtherBank').value = parseFloat(cal).toFixed(2);
                        }
                        //document.getElementById('txtOtherBank').value = parseFloat(cal).toFixed(2);
                        //document.getElementById('txtOtherBank').focus();
                        cal1 = parseFloat(cal * exrate.value).toFixed(2);
                        document.getElementById('txtOtherBankinINR').value = cal1;
                    }
                }
                //if(cal
            }

            if (billamt.value != '' && negoamt.value != '') {

                cal = parseFloat(parseFloat(billamt.value) - parseFloat(negoamt.value)).toFixed(2);
                document.getElementById('txtCollectionAmt').value = cal;
                cal1 = parseFloat(cal * exrate1.value).toFixed(2);
                document.getElementById('txtCollectionAmtinINR').value = cal1;
            }
            if (PartConAmt.value == '')
                PartConAmt.value = 0;
            PartConAmt.value = parseFloat(PartConAmt.value).toFixed(2);
            if (noneefc.value == '')
                noneefc.value = 0;
            noneefc.value = parseFloat(noneefc.value).toFixed(2);
            if (PartConAmt.value != '') {
                if (parseFloat(PartConAmt.value) > parseFloat(realamt.value)) {

                    alert('Part Conversion amount cannot be greater than realised amount.');
                    //document.getElementById('txtEEFCAmt').focus();
                }
                if (eefc.value != '') {
                    if (parseFloat(eefc.value) > parseFloat(realamt.value)) {

                        alert('EEFC amount cannot be greater than realised amount.');
                        //document.getElementById('txtEEFCAmt').focus();
                    }
                    if (PartConAmt.value != '' && eefc.value != '') {

                        eefc.value = parseFloat(eefc.value).toFixed(2);
                        realamt.value = parseFloat(realamt.value).toFixed(2);
                        document.getElementById('txtEEFCAmtTotal').value = noneefc.value;
                        bal = parseFloat(realamt.value - (parseFloat(noneefc.value) + parseFloat(eefc.value))).toFixed(2);
                        document.getElementById('txtBalAmt').value = bal;
                        bal1 = Math.round(parseFloat(bal * exrate.value)).toFixed(2);
                        document.getElementById('txtBalAmtinINR').value = bal1;
                        bal2 = parseFloat(eefc.value * exrate.value).toFixed(2);
                        document.getElementById('txtEEFCinINR').value = bal2;
                        //document.getElementById('txtEEFCCurrency').focus();
                    }
                }

                calInterest();
                return true;

            }
        }

        function calcrosscurrTotal() {

            var eefc = document.getElementById('txtEEFCAmt');
            var crossrate = document.getElementById('txtCrossCurrRate');
            var cal;
            if (crossrate.value == '') {

                crossrate.value = 1;
                crossrate.value = parseFloat(crossrate.value).toFixed(10);
            }
            if (crossrate.value != '') {

                crossrate.value = parseFloat(crossrate.value).toFixed(10);
                cal = parseFloat(eefc.value * crossrate.value).toFixed(2);
                document.getElementById('txtEEFCAmtTotal').value = cal;
                document.getElementById('txtInterest').focus();
            }
            calrealisedAmtinINR();
        }

        function calcrosscurrTotal1() {

            var eefc = document.getElementById('txtPartConAmt');
            var crossrate = document.getElementById('txtConCurRate');
            var cal;
            if (crossrate.value == '') {

                crossrate.value = 1;
                crossrate.value = parseFloat(crossrate.value).toFixed(10);
            }
            if (crossrate.value != '') {

                crossrate.value = parseFloat(crossrate.value).toFixed(10);
                cal = parseFloat(eefc.value * crossrate.value).toFixed(2);
                document.getElementById('txtTotConRate').value = cal;

            }

            calrealisedAmtinINR();
        }

        function calInterest() {
            //alert('interest called');
            //var intrate1 = document.getElementById('txtInterestRate1');
            var intrate2 = document.getElementById('txtInterestRate2');
            var noofdays2 = document.getElementById('txtNoofDays2');
            var type = document.getElementById('txtDocPrFx');
            var negoamt = document.getElementById('txtNegotiatedAmt');
            var billtype = document.getElementById('txtBillType');
            var libor = document.getElementById('libor1');
            var loan = document.getElementById('loan1');
            var exrate = document.getElementById('exchangerate');
            var intamt = document.getElementById('txtInterest');
            var intdays = document.getElementById('intdays');
            var cal;
            var cal1;
            var cal2;
            var tot;
            var intlibor1;
            var intlibor2;
            var purrate = document.getElementById('purrate1');
            var salerate = document.getElementById('overrate');
            var exrate = document.getElementById('txtExchangeRate');


            if (intrate2.value == '') {

                intrate2.value = 0;
            }
            if (intdays.value == '') {

                intdays.value = 0;
            }

            if (libor.value == '') {

                libor.value = 0;
            }

            //if (type.value == "E" || (libor.value != '' && (type.value == "BCA" || type.value=="BCU") && loan.value == "Y")) {
            if ((libor.value != '' && (type.value == "BCA" || type.value == "BCU") && loan.value == "Y")) {

                cal1 = parseFloat((intrate2.value / 100) * (noofdays2.value / 360));
                cal = parseFloat(cal1 * negoamt.value).toFixed(2);
                document.getElementById('txtInterest').value = cal;
                //document.getElementById('txtOtherBank').focus();
                //                
            }
            else {
                //if (billtype.value == "Sight Bill") {
                cal1 = parseFloat(parseFloat(intrate2.value) / 100 * parseFloat(noofdays2.value) / 365);
                cal = parseFloat(cal1 * negoamt.value).toFixed(2);
                document.getElementById('txtInterest').value = cal;
                //document.getElementById('txtOtherBank').focus();
                //               
            }
            if (intamt.value != "" && exrate.value != "") {

                cal = parseFloat(intamt.value * exrate.value).toFixed(2);
                document.getElementById('txtInterestinINR').value = cal;
            }
            //document.getElementById('txtOtherBank').focus();
            return true;
        }

        function calProfitLieo() {
            var txtCommission = document.getElementById('hdnProfitRate');
            var commminamt = document.getElementById('hdnProfitMinAmt');
            var commmaxamt = document.getElementById('hdnProfitMaxAmt');
            var commflatamt = document.getElementById('hdnProfitFlat');
            var negoamt = document.getElementById('txtNegotiatedAmt');
            var exrate = document.getElementById('txtExchangeRate');
            var cal;
            var comm;
            if (isNaN(txtCommission.value)) {
                txtCommission.value = 0;
            }
            if (isNaN(commminamt.value)) {
                commminamt.value = 0;
            }
            if (isNaN(commmaxamt.value)) {
                commmaxamt.value = 0;
            }
            if (isNaN(commflatamt.value)) {
                commflatamt.value = 0;
            }
            if (isNaN(negoamt.value)) {
                negoamt.value = 0;
            }
            if (isNaN(exrate.value)) {
                exrate.value = 0;
            }
            if (negoamt.value != "" && exrate.value != "") {

                cal = Math.round(parseFloat(negoamt.value * exrate.value)).toFixed(2);
            }
            comm = Math.round(parseFloat(cal * txtCommission.value) / 100).toFixed(2);

            if (isNaN(comm)) { comm = 0; }

            if (parseFloat(txtCommission.value) != 0 && parseFloat(commminamt.value) == 0 && parseFloat(commmaxamt.value) == 0 && parseFloat(commflatamt.value) == 0) {
                document.getElementById('txtprofitamt').value = comm;
            }
            if (parseFloat(txtCommission.value) != 0 && parseFloat(commminamt.value) != 0 && parseFloat(commmaxamt.value) != 0 && parseFloat(commflatamt.value) == 0) {

                if (parseFloat(comm) < parseFloat(commminamt.value)) {

                    comm = parseFloat(commminamt.value).toFixed(2);
                }
                if (parseFloat(comm) > parseFloat(commminamt.value) && parseFloat(comm) < parseFloat(commmaxamt.value)) {
                    comm = parseFloat(comm);
                }
                if (parseFloat(comm) > parseFloat(commmaxamt.value)) {
                    comm = parseFloat(commmaxamt.value).toFixed(2);
                }
                document.getElementById('txtprofitamt').value = comm;
            }
            if (parseFloat(txtCommission.value) == 0 && parseFloat(commminamt.value) == 0 && parseFloat(commmaxamt.value) == 0 && parseFloat(commflatamt.value) != 0) {
                document.getElementById('txtprofitamt').value = parseFloat(commflatamt.value);
            }


            //alert('hi');

            //                var Peefc = document.getElementById('rdbTransType2_0');
            //                var Feefc = document.getElementById('rdbTransType2_1');
            //                var txtCommission = document.getElementById('hdnProfitRate');
            //                var negoamt = document.getElementById('txtNegotiatedAmt');
            //                var exrate = document.getElementById('txtExchangeRate');
            //                var commminamt = document.getElementById('hdnProfitMinAmt');
            //                //var chkfullpart;
            //                var cal;
            //                var comm;
            ////                if (document.getElementById('rdbFull').checked == true)
            ////                    chkfullpart = "F";
            ////                if (document.getElementById('rdbPart').checked == true)
            ////                    chkfullpart = "P";
            //                if (negoamt.value != "" && exrate.value != "") {

            //                    cal = Math.round(parseFloat(negoamt.value * exrate.value)).toFixed(2);
            //                }

            //                if ((Peefc.value == "PEEFC" || Feefc.value == "FEEFC")) {

            //                    comm = Math.round(parseFloat(cal * txtCommission.value) / 100).toFixed(2);

            //                    if (parseFloat(comm) < parseFloat(commminamt.value)) {

            //                        comm = parseFloat(commminamt.value).toFixed(2);
            //                    }

            //                    document.getElementById('txtprofitamt').value = comm;
            //                }
            //                else {

            //                    comm = 0;
            //                    comm = parseFloat(comm).toFixed(2);
            //                    document.getElementById('txtprofitamt').value = comm;
            //                }
            //calTax();
            return true;
            //calTax();
        }

        function calCommission() {
            var txtCommission = document.getElementById('hdnCommRate');
            var commminamt = document.getElementById('hdnCommMinAmt');
            var commmaxamt = document.getElementById('hdnCommMaxAmt');
            var commflatamt = document.getElementById('hdnCommFlat');
            var negoamt = document.getElementById('txtNegotiatedAmt');
            var exrate = document.getElementById('txtExchangeRate');
            var cal;
            var comm;
            if (isNaN(txtCommission.value)) {
                txtCommission.value = 0;
            }
            if (isNaN(commminamt.value)) {
                commminamt.value = 0;
            }
            if (isNaN(commmaxamt.value)) {
                commmaxamt.value = 0;
            }
            if (isNaN(commflatamt.value)) {
                commflatamt.value = 0;
            }
            if (isNaN(negoamt.value)) {
                negoamt.value = 0;
            }
            if (isNaN(exrate.value)) {
                exrate.value = 0;
            }
            if (negoamt.value != "" && exrate.value != "") {

                cal = Math.round(parseFloat(negoamt.value * exrate.value)).toFixed(2);
            }
            comm = Math.round(parseFloat(cal * txtCommission.value) / 100).toFixed(2);

            if (isNaN(comm)) { comm = 0; }

            if (parseFloat(txtCommission.value) != 0 && parseFloat(commminamt.value) == 0 && parseFloat(commmaxamt.value) == 0 && parseFloat(commflatamt.value) == 0) {
                document.getElementById('txtCommission').value = comm;
            }
            if (parseFloat(txtCommission.value) != 0 && parseFloat(commminamt.value) != 0 && parseFloat(commmaxamt.value) != 0 && parseFloat(commflatamt.value) == 0) {

                if (parseFloat(comm) < parseFloat(commminamt.value)) {

                    comm = parseFloat(commminamt.value).toFixed(2);
                }
                if (parseFloat(comm) > parseFloat(commminamt.value) && parseFloat(comm) < parseFloat(commmaxamt.value)) {

                    comm = parseFloat(comm);
                }
                if (parseFloat(comm) > parseFloat(commmaxamt.value)) {
                    comm = parseFloat(commmaxamt.value).toFixed(2);
                }

                document.getElementById('txtCommission').value = comm;
            }
            if (parseFloat(txtCommission.value) == 0 && parseFloat(commminamt.value) == 0 && parseFloat(commmaxamt.value) == 0 && parseFloat(commflatamt.value) != 0) {
                document.getElementById('txtCommission').value = parseFloat(commflatamt.value);
            }


            //                var txtDocPrFx = document.getElementById('txtDocPrFx');
            //                var txtCommission = document.getElementById('hdnCommRate');
            //                var negoamt = document.getElementById('txtNegotiatedAmt');
            //                var exrate = document.getElementById('txtExchangeRate');
            //                var commminamt = document.getElementById('hdnCommMinAmt');
            //                var chkfullpart;
            //                var cal;
            //                var comm;
            //                if (document.getElementById('rdbFull').checked == true)
            //                    chkfullpart = "F";
            //                if (document.getElementById('rdbPart').checked == true)
            //                    chkfullpart = "P";
            //                if (negoamt.value != "" && exrate.value != "") {

            //                    cal = Math.round(parseFloat(negoamt.value * exrate.value)).toFixed(2);
            //                }

            //                if ((txtDocPrFx.value == "BCA" || txtDocPrFx.value == "BCU") && chkfullpart == "F") {

            //                    comm = Math.round(parseFloat(cal * txtCommission.value) / 100).toFixed(2);

            //                    if (parseFloat(comm) < parseFloat(commminamt.value)) {

            //                        comm = parseFloat(commminamt.value).toFixed(2);
            //                    }

            //                    document.getElementById('txtCommission').value = comm;
            //                }
            //                else {

            //                    comm = 0;
            //                    comm = parseFloat(comm).toFixed(2);
            //                    document.getElementById('txtCommission').value = comm;
            //                }
           // calTax();
            return true;
            //calTax();
        }

//        function calTax() {

//            var hdnServiceTax;
//            var hdnEDU_CESS;
//            var hdnSEC_EDU_CESS;
//            if (document.getElementById('chkStax').checked == true) {

//                hdnServiceTax = document.getElementById('hdnServiceTax').value;
//                hdnEDU_CESS = document.getElementById('hdnEDU_CESS').value;
//                hdnSEC_EDU_CESS = document.getElementById('hdnSEC_EDU_CESS').value;
//            }
//            else {

//                hdnServiceTax = 0;
//                hdnEDU_CESS = 0;
//                hdnSEC_EDU_CESS = 0;
//            }

//            var _serviceTax = 0;
//            var _serviceTax_EDUCess = 0;
//            var _serviceTax_SecEDUCess = 0;

//            var inramt23 = document.getElementById('txtprofitamt');
//            if (inramt23.value == '')
//                inramt23.value = 0;
//            inramt23.value = parseFloat(inramt23.value).toFixed(2);


//            var inramt24 = document.getElementById('txtCommission');
//            if (inramt24.value == '')
//                inramt24.value = 0;
//            inramt24.value = parseFloat(inramt24.value).toFixed(2);


//            var inramt25 = document.getElementById('txtCourier');
//            if (inramt25.value == '')
//                inramt25.value = 0;
//            inramt25.value = parseFloat(inramt25.value).toFixed(2);


//            var inramt26 = document.getElementById('txtSwift');
//            if (inramt26.value == '')
//                inramt26.value = 0;
//            inramt26.value = parseFloat(inramt26.value).toFixed(2);


//            var inramt27 = document.getElementById('txtBankCertificate');
//            if (inramt27.value == '')
//                inramt27.value = 0;
//            inramt27.value = parseFloat(inramt27.value).toFixed(2);

//            var inramt28 = document.getElementById('txtFxDlsCommission');
//            if (inramt28.value == '')
//                inramt28.value = 0;
//            inramt28.value = parseFloat(inramt28.value).toFixed(2);

//            var inramt29 = document.getElementById('txtsbfx');
//            if (inramt29.value == '')
//                inramt29.value = 0;
//            inramt29.value = parseFloat(inramt29.value).toFixed(2);

//            var inramt30 = document.getElementById('txt_kkcessonfx');
//            if (inramt30.value == '')
//                inramt30.value = 0;
//            inramt30.value = parseFloat(inramt30.value).toFixed(2);

//            var inramt31 = document.getElementById('txttotcessfx');
//            if (inramt31.value == '')
//                inramt31.value = 0;
//            inramt31.value = parseFloat(inramt31.value).toFixed(2);

//            var inramt32 = document.getElementById('txtPcfcAmt');
//            if (inramt32.value == '')
//                inramt32.value = 0;
//            inramt32.value = parseFloat(inramt32.value).toFixed(2);

//            var inramt33 = document.getElementById('txtOverDue');
//            if (inramt33.value == '')
//                inramt33.value = 0;
//            inramt33.value = parseFloat(inramt33.value).toFixed(2);


//            var stax;
//            var staxvalue;

//            //stax = document.getElementById('ddlServicetax');
//            //staxvalue = stax.options[stax.selectedIndex].value;

//            var staxamt = document.getElementById('txtServiceTax');
//            if (staxamt.value == '')
//                staxamt.value = 0;
//            staxamt.value = parseFloat(staxamt.value).toFixed(2);

//            var iamt23 = 0;
//            var iamt24 = 0;
//            var iamt25 = 0;
//            var iamt26 = 0;
//            var iamt27 = 0;
//            var iamt32 = 0;
//            var iamt33 = 0;

//            if (inramt23.value != '') {
//                iamt23 = parseFloat(inramt23.value).toFixed(2);
//            }

//            if (inramt24.value != '') {
//                iamt24 = parseFloat(inramt24.value).toFixed(2);
//            }

//            if (inramt25.value != '') {
//                iamt25 = parseFloat(inramt25.value).toFixed(2);
//            }
//            if (inramt26.value != '') {
//                iamt26 = parseFloat(inramt26.value).toFixed(2);
//            }
//            if (inramt27.value != '') {
//                iamt27 = parseFloat(inramt27.value).toFixed(2);
//            }
//            //                if (inramt32.value != '') {
//            //                    iamt32 = parseFloat(inramt32.value).toFixed(2);
//            //                }
//            if (inramt33.value != '') {
//                iamt33 = parseFloat(inramt33.value).toFixed(2);
//            }

//            _serviceTax = (parseFloat(iamt23) + parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27) + parseFloat(iamt33)) * (parseFloat(hdnServiceTax) / 100);
//            _serviceTax = Math.round(_serviceTax);

//            _serviceTax_EDUCess = _serviceTax * (parseFloat(hdnEDU_CESS) / 100);

//            _serviceTax_SecEDUCess = _serviceTax * (parseFloat(hdnSEC_EDU_CESS) / 100);

//            staxamt.value = parseFloat(parseFloat(_serviceTax) + parseFloat(_serviceTax_EDUCess) + parseFloat(_serviceTax_SecEDUCess)).toFixed(2);

//            //            staxamt.value = parseFloat((parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27)) * (parseFloat(staxvalue) / 100)).toFixed(2);

//            var sbcess = 0;
//            var kkcess = 0;

//            var sbcessPer = document.getElementById('txtsbcess');
//            var kkcessPer = document.getElementById('txt_kkcessper');
//            var sbcessamt = document.getElementById('txtSBcesssamt');
//            var kkcesssmt = document.getElementById('txt_kkcessamt');
//            var tottaxamt = document.getElementById('txtsttamt');


//            //document.getElementById('txtServiceTax').value = staxamt.value;
//            if (sbcessPer.value == '') {
//                sbcessPer.value = 0;
//            }
//            sbcess = (parseFloat(iamt23) + parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27) + parseFloat(iamt33)) * (parseFloat(sbcessPer.value) / 100);
//            sbcessamt.value = (sbcess).toFixed(2);

//            if (sbcessamt.value == '') {
//                sbcessamt.value = 0;
//            }


//            if (kkcessPer.value == '') {
//                kkcessPer.value = 0;
//            }


//            kkcess = (parseFloat(iamt23) + parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27) + parseFloat(iamt33)) * (parseFloat(kkcessPer.value) / 100);
//            kkcesssmt.value = (kkcess).toFixed(2);

//            if (kkcesssmt.value == '') {
//                kkcesssmt.value = 0;
//            }


//            tottaxamt.value = parseFloat(parseFloat(staxamt.value) + parseFloat(sbcess) + parseFloat(kkcess)).toFixed(2);

//            calNetAmt();
//            return true;

//        }

        function calNetAmt() {
            var realamtinr = document.getElementById('txtAmtRealisedinINR');
            var profitamt = document.getElementById('txtprofitamt');
            var commission = document.getElementById('txtCommission');
            var swift = document.getElementById('txtSwift');
            var bankCert = document.getElementById('txtBankCertificate');
            var courier = document.getElementById('txtCourier');
            var stax = document.getElementById('txtsttamt');
            var eefcinr = document.getElementById('txtEEFCinINR');
            var loan = document.getElementById('loan1');
            var delinkdate = document.getElementById('txtDateDelinked');
            var cur = document.getElementById('txtCurrency');
            var sfxtax = document.getElementById('txttotcessfx');
            var negoamtinr = document.getElementById('txtNegotiatedAmtinINR');
            var transtype = document.getElementById('tran_type');
            var tran_type2 = document.getElementById('tran_type2');
            var balamtinr = document.getElementById('txtBalAmtinINR');
            var txtOverDue = document.getElementById('txtOverDue');
            //var pcamt = document.getElementById('txtTotalPCLiquidated');
            var bal;
            if (sfxtax.value == '')
                sfxtax.value = 0;
            if (realamtinr.value == '')
                realamtinr.value = 0;
            if (profitamt.value == '')
                profitamt.value = 0;
            if (commission.value == '')
                commission.value = 0;
            if (swift.value == '')
                swift.value = 0;
            if (bankCert.value == '')
                bankCert.value = 0;
            if (courier.value == '')
                courier.value = 0;
            if (stax.value == '')
                stax.value = 0;
            if (eefcinr.value == '')
                eefcinr.value = 0;
            if (negoamtinr.value == '')
                negoamtinr.value = 0;
            //            if (pcamt.value == '')
            //                pcamt.value = 0;

            if (loan.value == "N" || delinkdate.value != '') {

                if (transtype.value == "CC" || transtype.value == "PC" || tran_type2.value == "PEEFC" || tran_type2.value == "FEEFC") {

                    bal = Math.round(parseFloat((parseFloat(balamtinr.value) - parseFloat(profitamt.value) - parseFloat(commission.value) - parseFloat(swift.value) - parseFloat(courier.value) - parseFloat(bankCert.value) - parseFloat(txtOverDue.value) - parseFloat(stax.value) - parseFloat(sfxtax.value)))).toFixed(2);

                    document.getElementById('txtNetAmt').value = bal;
                }
                else {

                    bal = Math.round(parseFloat((parseFloat(realamtinr.value) - parseFloat(profitamt.value) - parseFloat(commission.value) - parseFloat(swift.value) - parseFloat(courier.value) - parseFloat(bankCert.value) - parseFloat(txtOverDue.value) - parseFloat(stax.value) - parseFloat(sfxtax.value)))).toFixed(2);

                    document.getElementById('txtNetAmt').value = bal;
                }
            }
            if (loan.value == "Y" || delinkdate.value != "") {

                if (cur.value == "INR") {

                    bal = Math.round(parseFloat(parseFloat(realamtinr.value) - parseFloat(negoamtinr.value) - parseFloat(txtOverDue.value))).toFixed(2);
                    document.getElementById('txtNetAmt').value = bal;
                }
                else {

                    document.getElementById('txtNetAmt').value = "";
                }
            }
            return true;
        }

        function calotheramtininr() {

            var cal;
            var otherbank = document.getElementById('txtOtherBank');
            var exrate = document.getElementById('txtExchangeRate');
            var cur = document.getElementById('txtCurrency');
            if (otherbank.value == '') {

                otherbank.value = 0;
                otherbank.value = parseFloat(otherbank.value).toFixed(2);
                document.getElementById('txtOtherBank').value = otherbank.value;
            }
            if (otherbank.value != '') {

                otherbank.value = parseFloat(otherbank.value).toFixed(2);
            }

            if (otherbank.value != '') {

                if (cur.value == 'INR') {
                    salerate.value = 1;
                }
                cal = parseFloat(otherbank.value * exrate.value).toFixed(2);
                document.getElementById('txtOtherBankinINR').value = cal;
            }
        }

        function OpenOverseasBankList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtOverseasBankID = document.getElementById('txtOverseasBank');
                open_popup('EXP_OverseasBankLookup.aspx?hNo=1&bankID=' + txtOverseasBankID.value, 450, 650, 'OverseasBankList');
                return false;
            }
        }

        function selectOverseasBank(selectedID) {
            var id = selectedID;
            document.getElementById('txtOverseasBank').value = id;
            __doPostBack('txtOverseasBank', '');
        }

        function OpenTTNoList(hNo) {
            var branchCode = document.getElementById('hdnBranchCode');
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            var IRM = document.getElementById('txtTTRefNo1');
            if (txtCustAcNo.value == "") {
                alert('Enter Customer A/c No.');
                return false;
            }
            open_popup('EXP_TTRefNo2.aspx?custAcNo=' + txtCustAcNo.value + '&hNo=' + hNo + '&IRM1=' + IRM.value + '&bcode=' + branchCode.value, 350, 500, 'TTRefNo');
            return false;

        }

        function saveTTRefDetails(TTRefNo, TTAmt, hNo, TTTotAmt, TTCurr) {
            switch (hNo) {
                case "1":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo1').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt1').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt1').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount1').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency1').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo1', '');
                    break;
                case "2":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo2').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt2').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt2').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount2').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency2').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo2', '');
                    break;
                case "3":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo3').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt3').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt3').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount3').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency3').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo3', '');
                    break;
                case "4":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo4').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt4').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt4').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount4').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency4').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo4', '');
                    break;
                case "5":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo5').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTAmt5').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt5').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount5').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency5').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo5', '');
                    break;
                case "6":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo6').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt6').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt6').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount6').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency6').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo6', '');
                    break;

                case "7":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo7').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt7').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt7').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount7').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency7').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo7', '');
                    break;

                case "8":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo8').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt8').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTAmt8').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount8').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency8').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo8', '');
                    break;

                case "9":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo9').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTtTTAmt9').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt9').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount9').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency9').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo9', '');
                    break;

                case "10":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo10').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt10').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt10').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount10').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency10').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo10', '');
                    break;

                case "11":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo11').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt11').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt11').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount11').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency11').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo11', '');
                    break;

                case "12":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo12').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt12').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt12').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount12').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency12').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo12', '');
                    break;

                case "13":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo13').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt13').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt13').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount13').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency13').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo13', '');
                    break;

                case "14":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo14').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt14').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt14').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount14').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency14').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo14', '');
                    break;

                case "15":
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTRefNo15').value = TTRefNo;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTotTTAmt15').value = TTTotAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtBalTTAmt15').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_txtTTAmount15').value = TTAmt;
                    document.getElementById('TabContainerMain_tbDocumentDetails_ddlTTCurrency15').value = TTCurr;
                    __doPostBack('TabContainerMain_tbDocumentDetails_txtTTRefNo15', '');
                    break;

            }

            //                document.getElementById('btnSaveTTDetails').click();
        }

        function fbkcal() {

            var txtfbkcharges = document.getElementById('txt_fbkcharges');
            var txtfbkchargesRS = document.getElementById('txt_fbkchargesinRS');
            var txtExchangeRate = document.getElementById('txtExchangeRate');


            if (txtfbkcharges.value == '') {

                txtfbkcharges.value = 0;
                txtfbkcharges.value = parseFloat(txtfbkcharges.value).toFixed(2);
                document.getElementById('txt_fbkcharges').value = txtfbkcharges.value;
            }
            else {
                txtfbkcharges.value = parseFloat(txtfbkcharges.value).toFixed(2);
                txtfbkchargesRS.value = parseFloat(parseFloat(txtfbkcharges.value).toFixed(2) * parseFloat(txtExchangeRate.value).toFixed(2)).toFixed(2);

            }




        }

        function CheckTT() {
            var txtCurrency = document.getElementById('txtCurrency');
            var txt_relcur = document.getElementById('txt_relcur');
            var chkFirc = document.getElementById('chkFirc');
            var txtOverseasBank = document.getElementById('txtOverseasBank');
            var txtTTNo1 = document.getElementById('txtTTRefNo1');
            var txtTTNo2 = document.getElementById('txtTTRefNo2');
            var txtTTNo3 = document.getElementById('txtTTRefNo3');
            var txtTTNo4 = document.getElementById('txtTTRefNo4');
            var txtTTNo5 = document.getElementById('txtTTRefNo5');

            var txtForeignORLocal = document.getElementById('txtForeignORLocal');

            if (chkFirc.checked) {
            }
            else {
                if (txtForeignORLocal.value == 'F' || txtForeignORLocal.value == 'f') {
                    if (txtTTNo1.value == "" && txtTTNo2.value == "" && txtTTNo3.value == "" && txtTTNo4.value == "" && txtTTNo5.value == "") {
                        alert('ITT Reference No is not attached! Please attach ITT');
                        return false;
                    }
                }
            }

            //            else if (!confirm('Do You Want to continue with Selected Nostro(Receiving Bank) \n Click "OK" to Continue "Cancel" to Stop!')) {
            //                return false;
            //            }

            if (txt_relcur.value != '' && (txt_relcur.value != txtCurrency.value)) {
            }
            else {
                if (txtTTNo1.value != "" || txtTTNo2.value != "" || txtTTNo3.value != "" || txtTTNo4.value != "" || txtTTNo5.value != "") {
                    var txtAmtRealised = document.getElementById('txtAmtRealised');
                    var txtTTAmount1 = document.getElementById('txtTTAmount1');
                    var txtTTAmount2 = document.getElementById('txtTTAmount2');
                    var txtTTAmount3 = document.getElementById('txtTTAmount3');
                    var txtTTAmount4 = document.getElementById('txtTTAmount4');
                    var txtTTAmount5 = document.getElementById('txtTTAmount5');

                    var AmtReal;
                    var TTamt1, TTamt2, TTamt3, TTamt4, TTamt5;

                    if (txtAmtRealised.value == '')
                        AmtReal = 0;
                    else
                        AmtReal = txtAmtRealised.value;
                    if (txtTTAmount1.value == '')
                        TTamt1 = 0;
                    else
                        TTamt1 = txtTTAmount1.value;
                    if (txtTTAmount2.value == '')
                        TTamt2 = 0;
                    else
                        TTamt2 = txtTTAmount2.value;
                    if (txtTTAmount3.value == '')
                        TTamt3 = 0;
                    else
                        TTamt3 = txtTTAmount3.value;
                    if (txtTTAmount4.value == '')
                        TTamt4 = 0;
                    else
                        TTamt4 = txtTTAmount4.value;
                    if (txtTTAmount5.value == '')
                        TTamt5 = 0;
                    else
                        TTamt5 = txtTTAmount5.value;



                    var RalAmt;
                    var TTAmtSum;

                    RalAmt = parseFloat(AmtReal).toFixed(2);
                    TTAmtSum = parseFloat(parseFloat(TTamt1).toFixed(2) + parseFloat(TTamt2).toFixed(2) + parseFloat(TTamt3).toFixed(2) + parseFloat(TTamt4).toFixed(2) + parseFloat(TTamt5).toFixed(2)).toFixed(2);
                    if (true) {

                    }
                    if (parseFloat(RalAmt) < parseFloat(TTAmtSum)) {
                        alert('ITT Amount Total should not be More Than Realised Amount!');
                        return false;
                    }
                    if (parseFloat(RalAmt) > parseFloat(TTAmtSum)) {
                        if (confirm('ITT Amount Total Less Than Realised Amount! \n Do you want to continue ?'))
                            return true;
                        else
                            return false;
                    }
                }
                else {
                    return true;
                }
            }

        }

        function checkttvalue(ttnoid, ttamtid) {
            var hdnittno = document.getElementById('hdnittno');
            var hdnittamt = document.getElementById('hdnittamt');
            var hdnittamtid = document.getElementById('hdnittamtid');
            hdnittamtid.value = ttamtid;
            var ttno = document.getElementById(ttnoid)
            var ttamt = document.getElementById(ttamtid)
            if (ttno.value != "") {
                hdnittno.value = ttno.value;
                hdnittamt.value = ttamt.value;
                var btnttamtCheck = document.getElementById('btnttamtCheck');
                btnttamtCheck.click();
            }
        }

        //        function checkITT() {

        //            var txtTTRefNo1 = $get("txtTTRefNo1");
        //            var txtTTRefNo2 = $get("txtTTRefNo2");
        //            var txtTTRefNo3 = $get("txtTTRefNo3");
        //            var txtTTRefNo4 = $get("txtTTRefNo4");
        //            var txtTTRefNo5 = $get("txtTTRefNo5");
        //            var ddlAccountType = $get("ddlAccountType");

        //            if (txtTTRefNo1.value == "" && txtTTRefNo2.value == "" && txtTTRefNo3.value == "" && txtTTRefNo4.value == "" && txtTTRefNo5.value == "") {


        //                //                var curr = $get("TabContainerMain_tbTransactionDetails_ddlCurrency");
        //                //                var currValue = curr.options[curr.selectedIndex].value;
        //            }
        //            else {
        //                if (ddlAccountType.value == "") {
        //                    
        //                }
        //            }
        //        }

        function executeCSharpCode() {
            var docNo = document.getElementById('TabContainerMain_tbDocumentDetails_txtDocNo').value;
            var checker = document.getElementById('hdnUserName').value;
            var srNo = document.getElementById('TabContainerMain_tbDocumentDetails_txtSrNo').value;

            $.ajax({
                type: 'POST',
                url: 'EXP_AddEditFullRealisationPRN_Checker.aspx/ExecuteCSharpCode',
                data: JSON.stringify({ docNo: docNo, checker: checker, srNo: srNo }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                },
                error: function (error) {
                }
            });
        }
    </script>
    <%--<script language="javascript" type="text/javascript">

        function CalculateTotalPC() {

            var txtExchRate = document.getElementById('txtExchangeRate');
            if (txtExchRate.value == '')
                txtExchRate.value = 0;
            txtExchRate.value = parseFloat(txtExchRate.value).toFixed(10);

            var txtPCAmount1 = document.getElementById('txtPCAmount1');
            if (txtPCAmount1.value == '')
                txtPCAmount1.value = 0;
            txtPCAmount1.value = parseFloat(txtPCAmount1.value).toFixed(2);

            var txtPCAmount2 = document.getElementById('txtPCAmount2');
            if (txtPCAmount2.value == '')
                txtPCAmount2.value = 0;
            txtPCAmount2.value = parseFloat(txtPCAmount2.value).toFixed(2);

            var txtPCAmount3 = document.getElementById('txtPCAmount3');
            if (txtPCAmount3.value == '')
                txtPCAmount3.value = 0;
            txtPCAmount3.value = parseFloat(txtPCAmount3.value).toFixed(2);

            var txtPCAmount4 = document.getElementById('txtPCAmount4');
            if (txtPCAmount4.value == '')
                txtPCAmount4.value = 0;
            txtPCAmount4.value = parseFloat(txtPCAmount4.value).toFixed(2);

            var txtPCAmount5 = document.getElementById('txtPCAmount5');
            if (txtPCAmount5.value == '')
                txtPCAmount5.value = 0;
            txtPCAmount5.value = parseFloat(txtPCAmount5.value).toFixed(2);

            var txtPCAmount6 = document.getElementById('txtPCAmount6');
            if (txtPCAmount6.value == '')
                txtPCAmount6.value = 0;
            txtPCAmount6.value = parseFloat(txtPCAmount6.value).toFixed(2);

            var txtPCAcNo1 = document.getElementById('txtPCAcNo1');
            var txtPCAcNo2 = document.getElementById('txtPCAcNo2');
            var txtPCAcNo3 = document.getElementById('txtPCAcNo3');
            var txtPCAcNo4 = document.getElementById('txtPCAcNo4');
            var txtPCAcNo5 = document.getElementById('txtPCAcNo5');
            var txtPCAcNo6 = document.getElementById('txtPCAcNo6');

            var txtPCAmtinINR1 = 0;
            var txtPCAmtinINR2 = 0;
            var txtPCAmtinINR3 = 0;
            var txtPCAmtinINR4 = 0;
            var txtPCAmtinINR5 = 0;
            var txtPCAmtinINR6 = 0;

            var txtTotalPCLiquidated = document.getElementById('txtTotalPCLiquidated');
            txtTotalPCLiquidated.value = 0;
            txtTotalPCLiquidated.value = parseFloat(txtTotalPCLiquidated.value).toFixed(2);

            var hdnPCbalance1 = document.getElementById('hdnPCbalance1');
            var hdnPCbalance2 = document.getElementById('hdnPCbalance2');
            var hdnPCbalance3 = document.getElementById('hdnPCbalance3');
            var hdnPCbalance4 = document.getElementById('hdnPCbalance4');
            var hdnPCbalance5 = document.getElementById('hdnPCbalance5');
            var hdnPCbalance6 = document.getElementById('hdnPCbalance6');

            if (txtPCAcNo1.value != '' && hdnPCbalance1.value != '') {
                if (parseFloat(hdnPCbalance1.value) < parseFloat(txtPCAmount1.value)) {
                    alert('PC Amount cannot be greater than balance amount.');
                    txtPCAmount1.value = '';
                    return false;
                }
            }
            if (txtPCAcNo2.value != '' && hdnPCbalance2.value != '') {
                if (parseFloat(hdnPCbalance2.value) < parseFloat(txtPCAmount2.value)) {
                    alert('PC Amount cannot be greater than balance amount.');
                    txtPCAmount2.value = '';
                    return false;
                }
            }
            if (txtPCAcNo3.value != '' && hdnPCbalance3.value != '') {
                if (parseFloat(hdnPCbalance3.value) < parseFloat(txtPCAmount3.value)) {
                    alert('PC Amount cannot be greater than balance amount.');
                    txtPCAmount3.value = '';
                    return false;
                }
            }
            if (txtPCAcNo4.value != '' && hdnPCbalance4.value != '') {
                if (parseFloat(hdnPCbalance4.value) < parseFloat(txtPCAmount4.value)) {
                    alert('PC Amount cannot be greater than balance amount.');
                    txtPCAmount4.value = '';
                    return false;
                }
            }
            if (txtPCAcNo5.value != '' && hdnPCbalance5.value != '') {
                if (parseFloat(hdnPCbalance5.value) < parseFloat(txtPCAmount5.value)) {
                    alert('PC Amount cannot be greater than balance amount.');
                    txtPCAmount5.value = '';
                    return false;
                }
            }
            if (txtPCAcNo6.value != '' && hdnPCbalance6.value != '') {
                if (parseFloat(hdnPCbalance6.value) < parseFloat(txtPCAmount6.value)) {
                    alert('PC Amount cannot be greater than balance amount.');
                    txtPCAmount6.value = '';
                    return false;
                }
            }

            if (txtPCAcNo1.value != '') {
                txtPCAmtinINR1 = parseFloat(txtPCAmount1.value * txtExchRate.value).toFixed(2);
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR1)).toFixed(2);
            }
            else {
                txtPCAmtinINR1.value = 0;
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR1)).toFixed(2);
            }
            if (txtPCAcNo2.value != '') {
                txtPCAmtinINR2 = (parseFloat(txtPCAmount2.value) * parseFloat(txtExchRate.value)).toFixed(2);
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR2)).toFixed(2);
            }
            else {
                txtPCAmtinINR2.value = 0;
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR2)).toFixed(2);
            }
            if (txtPCAcNo3.value != '') {
                txtPCAmtinINR3 = (parseFloat(txtPCAmount3.value) * parseFloat(txtExchRate.value)).toFixed(2);
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR3)).toFixed(2);
            }
            else {
                txtPCAmtinINR3.value = 0;
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR3)).toFixed(2);
            }
            if (txtPCAcNo4.value != '') {
                txtPCAmtinINR4 = (parseFloat(txtPCAmount4.value) * parseFloat(txtExchRate.value)).toFixed(2);
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR4)).toFixed(2);
            }
            else {
                txtPCAmtinINR4.value = 0;
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR4)).toFixed(2);
            }
            if (txtPCAcNo5.value != '') {
                txtPCAmtinINR5 = (parseFloat(txtPCAmount5.value) * parseFloat(txtExchRate.value)).toFixed(2);
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR5)).toFixed(2);
            }
            else {
                txtPCAmtinINR5.value = 0;
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR5)).toFixed(2);
            }
            if (txtPCAcNo6.value != '') {
                txtPCAmtinINR6 = (parseFloat(txtPCAmount6.value) * parseFloat(txtExchRate.value)).toFixed(2);
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR6)).toFixed(2);
            }
            else {
                txtPCAmtinINR6.value = 0;
                txtTotalPCLiquidated.value = parseFloat(parseFloat(txtTotalPCLiquidated.value) + parseFloat(txtPCAmtinINR6)).toFixed(2);
            }
            checkeefc();
            calNetAmt();
            return true;
        }

    </script>
    <script language="javascript" type="text/javascript">
        function OpenSubACList(hNo) {
            var hdnBranchName = document.getElementById('hdnBranchName');
            var custID = document.getElementById('txtCustAcNo');
            open_popup('EXP_SubAccountNo.aspx?custId=' + custID.value + '&hNo=' + hNo + '&branch=' + hdnBranchName.value, 450, 450, 'SubAcList');
            return false;
        }
        function selectSubAc1(sAcNo, AcNo, balance) {
            var txtPCAcNo1 = document.getElementById('txtPCAcNo1');
            var txtPCsubAcNo1 = document.getElementById('txtPCsubAcNo1');
            var hdnPCbalance1 = document.getElementById('hdnPCbalance1');
            txtPCAcNo1.value = AcNo;
            txtPCsubAcNo1.value = sAcNo;
            hdnPCbalance1.value = balance;
        }
        function selectSubAc2(sAcNo, AcNo, balance) {
            var txtPCAcNo2 = document.getElementById('txtPCAcNo2');
            var txtPCsubAcNo2 = document.getElementById('txtPCsubAcNo2');
            var hdnPCbalance2 = document.getElementById('hdnPCbalance2');
            txtPCAcNo2.value = AcNo;
            txtPCsubAcNo2.value = sAcNo;
            hdnPCbalance2.value = balance;
        }
        function selectSubAc3(sAcNo, AcNo, balance) {
            var txtPCAcNo3 = document.getElementById('txtPCAcNo3');
            var txtPCsubAcNo3 = document.getElementById('txtPCsubAcNo3');
            var hdnPCbalance3 = document.getElementById('hdnPCbalance3');
            txtPCAcNo3.value = AcNo;
            txtPCsubAcNo3.value = sAcNo;
            hdnPCbalance3.value = balance;
        }
        function selectSubAc4(sAcNo, AcNo, balance) {
            var txtPCAcNo4 = document.getElementById('txtPCAcNo4');
            var txtPCsubAcNo4 = document.getElementById('txtPCsubAcNo4');
            var hdnPCbalance4 = document.getElementById('hdnPCbalance4');
            txtPCAcNo4.value = AcNo;
            txtPCsubAcNo4.value = sAcNo;
            hdnPCbalance4.value = balance;
        }
        function selectSubAc5(sAcNo, AcNo, balance) {
            var txtPCAcNo5 = document.getElementById('txtPCAcNo5');
            var txtPCsubAcNo5 = document.getElementById('txtPCsubAcNo5');
            var hdnPCbalance5 = document.getElementById('hdnPCbalance5');
            txtPCAcNo5.value = AcNo;
            txtPCsubAcNo5.value = sAcNo;
            hdnPCbalance5.value = balance;
        }
        function selectSubAc6(sAcNo, AcNo, balance) {
            var txtPCAcNo6 = document.getElementById('txtPCAcNo6');
            var txtPCsubAcNo6 = document.getElementById('txtPCsubAcNo6');
            var hdnPCbalance6 = document.getElementById('hdnPCbalance6');
            txtPCAcNo6.value = AcNo;
            txtPCsubAcNo6.value = sAcNo;
            hdnPCbalance6.value = balance;
        }

        function validate_AcNo(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //   alert(charCode);
            if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39)
                return false;
            else
                return true;
        }
    </script>--%>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
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
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <span class="pageLabel" style="font-weight: bold">Approved Realisation For PRN-Checker</span>
                                </td>
                                <td align="right" style="width: 50%">
                                    <asp:Label ID="LEIAmtCheck" runat="server"></asp:Label>
                                    <asp:Label ID="LEIverify" runat="server"></asp:Label>
                                    <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="45" CssClass="buttonDefault"
                                        ToolTip="Back" OnClick="btnBack_Click" />
                                    <input type="hidden" id="txtDocPrFx" runat="server" />
                                    <input type="hidden" id="hdnBranchCode" runat="server" />
                                    <input type="hidden" id="hdnYear" runat="server" />
                                    <input type="hidden" id="libor1" runat="server" />
                                    <input type="hidden" id="loan1" runat="server" />
                                    <input type="hidden" id="exchangerate" runat="server" />
                                    <input type="hidden" id="tran_type" runat="server" />
                                    <input type="hidden" id="tran_type2" runat="server" />
                                    <input type="hidden" id="amtbalforreal" runat="server" />
                                    <input type="hidden" id="purrate1" runat="server" />
                                    <input type="hidden" id="overrate" runat="server" />
                                    <input type="hidden" id="intdays" runat="server" />
                                    <input type="hidden" id="hdnBranchName" runat="server" />
                                    <input type="hidden" id="hdnmode" runat="server" />
                                    <input type="hidden" id="hdnServiceTax" runat="server" />
                                    <input type="hidden" id="hdnEDU_CESS" runat="server" />
                                    <input type="hidden" id="hdnSEC_EDU_CESS" runat="server" />
                                    <input type="hidden" id="hdnCommRate" runat="server" />
                                    <input type="hidden" id="hdnCommMinAmt" runat="server" />
                                    <input type="hidden" id="hdnProfitMinAmt" runat="server" />
                                    <input type="hidden" id="hdnProfitRate" runat="server" />
                                    <input type="hidden" id="hdnCommMaxAmt" runat="server" />
                                    <input type="hidden" id="hdnCommFlat" runat="server" />
                                    <input type="hidden" id="hdnProfitMaxAmt" runat="server" />
                                    <input type="hidden" id="hdnProfitFlat" runat="server" />
                                    <input type="hidden" id="hdnittamt" runat="server" />
                                    <input type="hidden" id="hdnittno" runat="server" />
                                    <input type="hidden" id="hdnittamtid" runat="server" />
                                    <input type="hidden" id="hdnDocNo" runat="server" />
                                    <input type="hidden" id="hdnUserName" runat="server" />
                                    <input type="hidden" id="hdnRejectReason" runat="server" />
                                    <%--<input type="hidden" id="hdnPCsrNo1" runat="server" />
                                <input type="hidden" id="hdnPCsrNo2" runat="server" />
                                <input type="hidden" id="hdnPCsrNo3" runat="server" />
                                <input type="hidden" id="hdnPCsrNo4" runat="server" />
                                <input type="hidden" id="hdnPCsrNo5" runat="server" />
                                <input type="hidden" id="hdnPCsrNo6" runat="server" />
                                <input type="hidden" id="hdnPCbalance1" runat="server" />
                                <input type="hidden" id="hdnPCbalance2" runat="server" />
                                <input type="hidden" id="hdnPCbalance3" runat="server" />
                                <input type="hidden" id="hdnPCbalance4" runat="server" />
                                <input type="hidden" id="hdnPCbalance5" runat="server" />
                                <input type="hidden" id="hdnPCbalance6" runat="server" />--%>
                                    <%--added by Shailesh for LEI--%>
                                    <input type="hidden" id="hdnLeiFlag" runat="server" />
                                    <input type="hidden" id="hdnLeiSpecialFlag" runat="server" />
                                    <input type="hidden" id="hdnCustabbr" runat="server" />
                                    <input type="hidden" id="hdncustlei" runat="server" />
                                    <input type="hidden" id="hdncustleiexpiry" runat="server" />
                                    <input type="hidden" id="hdnoverseaslei" runat="server" />
                                    <input type="hidden" id="hdnoverseasleiexpiry" runat="server" />
                                    <input type="hidden" id="hdncustleiexpiryRe" runat="server" />
                                    <input type="hidden" id="hdnoverseasleiexpiryRe" runat="server" />
                                    <input type="hidden" id="hdnleiExchRate" runat="server" />
                                    <input type="hidden" id="hdnbillamtinr" runat="server" />
                                    <input type="hidden" id="hdnCustname" runat="server" />
                                    <input type="hidden" id="hdnValidateLei" runat="server" />
                                    <asp:Label ID="lblLEIEffectDate" runat="server" Text="01/01/2023" Visible="false"></asp:Label>
                                    <input type="hidden" id="hdnForeignLocal" runat="server" />
                                    <input type="hidden" id="hdnMerchantTrade" runat="server" />

                                    <input type="hidden" id="hdnTTFIRCTotalAmtCheck" runat="server" />
                                    <input type="hidden" id="hdnTTCurrCheck" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top" colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 100%" valign="top" colspan="2">
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rbtbla" runat="server" CssClass="elementLabel" Enabled="false"
                                        Text="Bills Bght with L/C at Sight" Style="font-weight: bold;" GroupName="TransType" Visible="false"/>
                                    <%--</td>
                            <td width="18%" nowrap align="left">--%>
                                    <asp:RadioButton ID="rbtbba" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C at Sight"
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:RadioButton ID="rbtblu" runat="server" CssClass="elementLabel" Text="Bills Bght with L/C Usance"
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:RadioButton ID="rbtbbu" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C Usance"
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:RadioButton ID="rbtbca" runat="server" CssClass="elementLabel" Text="Bills For Coll. at Sight "
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:RadioButton ID="rbtbcu" runat="server" CssClass="elementLabel" Text="Bills For Coll. Usance"
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Local Bills Dis."
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:RadioButton ID="rbtLBC" runat="server" CssClass="elementLabel" Text="Local Bills Coll."
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/>
                                    <asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                        GroupName="TransType" Enabled="false" Style="font-weight: bold;" Visible="false"/>&nbsp;&nbsp;&nbsp;&nbsp;
                                
                                <span class="elementLabel">Foreign OR Local :</span>
                                <asp:TextBox ID="txtForeignORLocal" runat="server" Enabled="false" Width="15px" CssClass="textBox"></asp:TextBox>
                                <span class="elementLabel" style="margin-left:10px;">LEI Mint Rate :</span>
                                <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                                </td>
                                <%--<td>
                                &nbsp;
                            </td>--%>
                            </tr>
                            <tr>
                                <td align="center" >
                                    <asp:Label ID="ReccuringLEI" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                    <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="1"
                                        CssClass="ajax__tab_xp-theme" TabIndex="-1">
                                       <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="Document Details"
                                            Font-Bold="true" ForeColor="White">
                                            <ContentTemplate>
                                                <table cellpadding="0" cellspacing="2" border="0" width="100%">

                                                    <div >
                                                    <td colspan="1" align="left">
														<table width="80%">
                                                    <tr>
                                                        <td align="right" nowrap>
                                                        <span class="elementLabel">Document No. : </span>
                                                        </td>
                                                        <td align="left" nowrap >
                                                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="textBox" Height="14px" Width="150px"
                                                            TabIndex="1" Style="font-weight: bold;" ToolTip="Press F2 for Help" AutoPostBack="true"
                                                            OnTextChanged="txtDocNo_TextChanged" Enabled="false"></asp:TextBox>
                                                        <asp:Button ID="btnDocNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                        </td>
                                                        <td>
                                                        <span  class="elementLabel"  style="padding-left: 120px;">Sr No :</span>
                                                        <asp:TextBox ID="txtSrNo" runat="server" Enabled="false" Width="35px" Height="14px"
                                                            CssClass="textBox"></asp:TextBox>
                                                        <span class="elementLabel" style="display:none;">Date Received :</span>
                                                        <asp:TextBox ID="txtDateReceived" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                                            Enabled="false" Visible="false"></asp:TextBox>
                                                          </td>
                                                          <td align="left" nowrap>
                                                            <span class="elementLabel" style="margin-left:40px;">Processing Date :</span>
                                                        <asp:TextBox ID="txtProcessingDate" runat="server" Width="70px" Height="14px" TabIndex="-1" CssClass="textBox" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                        <span class="elementLabel">Customer A/C No :</span>
                                                        </td>
                                                        <td align="left" nowrap colspan="3">
                                                        <asp:TextBox ID="txtCustAcNo" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                                            Enabled="false" TabIndex="-1"></asp:TextBox>
                                                        <asp:Label ID="lblCustDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="right" nowrap>
                                                        <span class="elementLabel">Overseas Party Name :</span>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox ID="txtOverseasParty" runat="server" CssClass="textBox" Height="14px"
                                                            Width="70px" Enabled="false" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="lblOverseasParty" runat="server" CssClass="textBox" Width="300px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2">
                                                       <span class="elementLabel" style="padding-left: 25px;">Overseas Party Country :</span>
                                                              <asp:TextBox ID="txtOverseasPartyCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                                                   MaxLength="2" Enabled="false"></asp:TextBox>
                                                              &nbsp;<asp:Label ID="lblOverseasPartyCountryDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>

                                                        <span id="SpanLei1" runat="server" class="elementLabel" visible="false">Customer LEI :</span>
                                                        <asp:Label ID="lblLEI_CUST_Remark" runat="server"></asp:Label>
                                                        </td>
                                                      </tr>
                                                    <tr>
                                                   <td align="right" nowrap>
                                                        <span class="elementLabel">Consignee Party Name :</span>
                                                    </td>
                                                    <td nowrap>
                                                        <asp:TextBox ID="txtconsigneePartyID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                            MaxLength="7" Width="70px"  Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="lblConsigneePartyDesc" runat="server" CssClass="textBox" Width="300px" Enabled="false"></asp:TextBox>
                                                    </td> 
                                                   <td colspan="2">
                                                        <span class="elementLabel" style="padding-left: 19px;">Consignee Party Country :</span>
                                                              <asp:TextBox ID="txtconsigneePartyCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                                                   MaxLength="2" Enabled="false"></asp:TextBox>
                                                              &nbsp;<asp:Label ID="lblconsigneePartyCountryDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                                        </td>
                                                   </tr>
                                                    <tr>
                                                   <td style="text-align: right; white-space: nowrap">
                                                        <span class="elementLabel">Overseas Bank Name :</span>
                                                        </td>
                                                        <td nowrap>
                                                        <asp:TextBox ID="txtOverseasBank" MaxLength="7" AutoPostBack="true" runat="server"
                                                            CssClass="textBox" Height="14px" Width="70px" TabIndex="5" OnTextChanged="txtOverseasBank_TextChanged" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="lblOverseasBank" runat="server" CssClass="textBox" Width="300px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2">
                                                        <span class="elementLabel" style="padding-left: 26px;">Overseas Bank Country :</span>
                                                              <asp:TextBox ID="txtOverseasBankCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                                                   MaxLength="2" Enabled="false"></asp:TextBox>
                                                              &nbsp;<asp:Label ID="lblOverseasBankCountryDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>

                                                         <span id="SpanLei2" runat="server" class="elementLabel" visible="false">Customer LEI
                                                             Expiry :</span>
                                                        <asp:Label ID="lblLEIExpiry_CUST_Remark" runat="server"></asp:Label>
                                                        </td>
                                                   </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">Bill Currency :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" Height="14px" Width="35px"
                                                                Enabled="False" AutoPostBack="True" OnTextChanged="txtCurrency_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td align="left" nowrap>
                                                        <span class="elementLabel" style="padding-left: 90px;">Bill Amount :</span>
                                                        <asp:TextBox ID="txtBillAmt" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                                            Enabled="false" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap>
                                                        <span class="elementLabel" style="margin-left:75px;">Due Date :</span>
                                                        <asp:TextBox ID="txtDueDate" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                                            Enabled="false"></asp:TextBox>
                                                        <span class="elementLabel"  style="display:none">Bill Amt. in र :</span>
                                                       
                                                        <asp:TextBox ID="txtBillAmtinINR" runat="server" CssClass="textBox" Height="14px"
                                                            Width="100px" Enabled="false" Style="text-align: right" Visible="false"></asp:TextBox>

                                                           <span id="SpanLei3" runat="server"
                                                            class="elementLabel" visible="false">Applicant LEI :</span>
                                                        <asp:Label ID="lblLEI_Overseas_Remark" runat="server"></asp:Label>
                                                         &nbsp;<span id="SpanLei4" runat="server" class="elementLabel" visible="false">Applicant LEI Expiry :</span>
                                                        <asp:Label ID="lblLEIExpiry_Overseas_Remark" runat="server"></asp:Label>
                                                        </td>
                                                        
                                                   </tr>
                                                    <tr>
                                                   <td align="right" nowrap>
                                                            <span class="elementLabel">Outstanding Amount :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtOutstandingAmt" runat="server" CssClass="textBox" Height="14px"
                                                                Width="100px" TabIndex="9" Style="text-align: right" onfocus="this.select()"
                                                                 AutoPostBack="True" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td align="left" colspan="2" nowrap>
                                                            <span class="elementLabel" style="margin-left:56px;">Full/Part Payment :</span>
                                                            <asp:RadioButton ID="rdbFull" runat="server" CssClass="elementLabel" Text="Full Payment"
                                                                GroupName="Payment" TabIndex="3" AutoPostBack="True" Style="vertical-align: top; color: Red; font-weight: bold;"
                                                                OnCheckedChanged="rdbFull_CheckedChanged" Enabled="false"/>&nbsp;
                                                            <asp:RadioButton ID="rdbPart" runat="server" CssClass="elementLabel" Text="Part Payment"
                                                                GroupName="Payment" TabIndex="11" AutoPostBack="True" Style="vertical-align: top; color: Red; font-weight: bold;"
                                                                OnCheckedChanged="rdbPart_CheckedChanged" Enabled="false"/>
                                                             </td>
                                                   </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel" style="margin-left:38px;">Remittance / MT103 Value Date :</span>
                                                             </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtValueDate" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                                                TabIndex="2" MaxLength="10" onfocus="this.select()" OnTextChanged="txtValueDate_TextChanged"
                                                                AutoPostBack="True" Enabled="false"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdValueDate" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btncalendar_ValueDate" runat="server" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" Enabled="false"/>
                                                            <asp:Label ID="lblRefund" runat="server" CssClass="elementLabel" Style="color: Red; font-weight: bold;"></asp:Label>
                                                            <ajaxToolkit:CalendarExtender ID="calenderValueDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtValueDate" PopupButtonID="btncalendar_ValueDate" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdValueDate"
                                                                ValidationGroup="dtVal" ControlToValidate="txtValueDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <span class="elementLabel" style="margin-left:38px;">IRM / Relization Date :</span>
                                                            <asp:TextBox ID="txtDateRealised" runat="server" CssClass="textBox" Height="14px"
                                                                Width="70px" TabIndex="4" MaxLength="10" onfocus="this.select()" Enabled="false"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdRemdate" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtDateRealised" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" Enabled="false"/>
                                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtDateRealised" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="Mev1" runat="server" ControlExtender="mdRemdate"
                                                                ValidationGroup="dtVal" ControlToValidate="txtDateRealised" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <td align="left" nowrap>
                                                        <span class="elementLabel" style="margin-left:30px;">Realized Currency :</span>
                                                            <asp:TextBox ID="txt_relcur" TabIndex="5" runat="server" CssClass="textBox" Height="14px"
                                                                Width="35px" Enabled="false" AutoPostBack="True" OnTextChanged="txt_relcur_TextChanged"></asp:TextBox>
                                                            <asp:Button ID="btn_recurrhelp" runat="server" CssClass="btnHelp_enabled" TabIndex="4" Enabled="false"/>
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel" style="margin-left:-3px;">Instructed Amount (FC) MT103-33B :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtInstructedAmt" runat="server" CssClass="textBox" Height="14px"
                                                                Width="100px" TabIndex="6" Style="text-align: right" onfocus="this.select()" Enabled="false" onchange=" return CalculateBillTab();"></asp:TextBox>
                                                        </td>
                                                         <td align="left" nowrap>
                                                            <span class="elementLabel" style="margin-left:-25px;">Relized Amount (FC) MT103-32A :</span>
                                                            <asp:TextBox ID="txtAmtRealised" runat="server" CssClass="textBox" Height="14px"
                                                                Width="100px" TabIndex="7" Style="text-align: right" onfocus="this.select()"
                                                                OnTextChanged="txtAmtRealised_TextChanged" AutoPostBack="True" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        <span class="elementLabel" style="margin-left:48px;">Fbank charges :</span>
                                                        <asp:TextBox ID="txt_fbkcharges" runat="server" CssClass="textBox" MaxLength="20" onfocus="this.select()" Style="text-align: right" 
                                                        TabIndex="8" Width="100px" onchange=" return CalculateBillTab();" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel" style="margin-left:17px;">Exchange Rate :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBox" Height="14px"
                                                                TabIndex="9" Style="text-align: right" onfocus="this.select()" Enabled="false" onchange=" return CalculateBillTab();"></asp:TextBox>
                                                        </td>
                                                        <td align="left" nowrap>
                                                        <span class="elementLabel" style="margin-left:28px;">Realized Amount (INR) :</span>
                                                        <asp:TextBox ID="txtAmtRealisedinINR" runat="server" CssClass="textBox" Height="14px"
                                                           Width="100px" Style="text-align: right" TabIndex="10" Enabled="false" onchange=" return CalculateBillTab();"></asp:TextBox>
                                                        </td>
                                                        <td align="left" nowrap>
                                                        <span class="elementLabel" style="margin-left:60px;">LEI INR Amt :</span>
                                                        <asp:TextBox ID="txtLeiInrAmt" runat="server" CssClass="textBox" Height="14px"
                                                           Width="100px" Style="text-align: right" Enabled="false" TabIndex="11" onchange=" return CalculateBillTab();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="right" nowrap>
                                                        <span class="elementLabel">PCFC Amount : </span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                        <asp:TextBox ID="txtPcfcAmt" runat="server" CssClass="textBox" Height="14px" Enabled="false" onfocus="this.select()" Style="text-align: right" TabIndex="12" Width="100px"></asp:TextBox>
                                                            </td>
                                                        <td align="left" colspan="2" nowrap>
                                                        <span class="elementLabel" style="margin-left:79px;">EEFC Amount : </span>
                                                        <asp:TextBox ID="txtEEFCAmt" runat="server" CssClass="textBox" Height="14px" Enabled="false" onfocus="this.select()" Style="text-align: right;" TabIndex="13" Width="100px"></asp:TextBox>      
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel" style="margin-left:12px;">Cross Currency Amount :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox runat="server" onfocus="this.select()" TabIndex="14" ID="txt_relamount"
                                                                Style="text-align: right;" CssClass="textBox" Enabled="false"/>
                                                            
                                                        </td>
                                                        <td align="left" colspan="2" nowrap>
                                                             <span class="elementLabel" style="margin-left:42px;">Cross Currency Rate  :</span>
                                                             <asp:TextBox ID="txtRelCrossCurRate" onfocus="this.select()" runat="server" CssClass="textBox"
                                                                TabIndex="15" Width="70px" Height="14px" Style="text-align: right;" Enabled="false"></asp:TextBox>
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="right" nowrap>
                                                              <span class="elementLabel">Remitter Name :</span>
                                                              </td>
                                                          <td align="left" nowrap>
                                                              <asp:TextBox ID="txtRemitterName" runat="server" Width="295px" CssClass="textBox"
                                                                  MaxLength="50" TabIndex="16" Enabled="false"></asp:TextBox>
                                                          </td>
                                                          <td style="text-align: left; white-space: nowrap">
                                                         <span class="elementLabel">Remitter Address :</span>
                                                         <asp:TextBox ID="txtRemitterAddress" CssClass="textBox" runat="server" MaxLength="100"
                                                             Width="400px" TabIndex="17" Enabled="false"></asp:TextBox>
                                                     </td>
                                                     <td style="text-align: left; white-space: nowrap;">
                                                              <span class="elementLabel" style="padding-left: 35px;">Remitter Country :</span>
                                                              <asp:TextBox ID="txtRemitterCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                                                  AutoPostBack="true" MaxLength="2" TabIndex="18" OnTextChanged="txtRemitterCountry_TextChanged" Enabled="false"></asp:TextBox>
                                                              <asp:Button ID="Button4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                  OnClientClick="return openCountryCode('mouseClick','1');" Enabled="false"/>
                                                              &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                    <td style="text-align: right; white-space: nowrap;">
                                                        <span class="elementLabel">Remitting Bank Swift Code :</span>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox ID="txtSwiftCode" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                                            AutoPostBack="true" OnTextChanged="txt_swiftcode_TextChanged" TabIndex="19" Enabled="false"></asp:TextBox>
                                                            <asp:Button ID="btnHelpSwiftCode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                OnClientClick="openSwiftCodeHelp('mouseClick');" Enabled="false"/>
                                                        </td>
                                                        <td style="text-align: left" nowrap>
                                                             <span class="elementLabel" style="padding-left: 10px;">Remitting Bank :</span>
                                                             <asp:TextBox ID="txtRemitterBank" CssClass="textBox" runat="server" MaxLength="100"
                                                                 Width="280px" TabIndex="20" Enabled="false"></asp:TextBox>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                    <td style="white-space: nowrap; text-align: right">
                                                          <span class="elementLabel">Remitting Bank Address : </span>
                                                      </td>
                                                      <td colspan="2" align="left">
                                                          <asp:TextBox ID="txtRemitterBankAddress" CssClass="textBox" runat="server" MaxLength="100"
                                                              Width="550px" TabIndex="21" Enabled="false"></asp:TextBox>
                                                              </td>
                                                          <td style="text-align: left; white-space: nowrap;">
                                                          <span class="elementLabel" style="padding-left: -10px;">Remitting Bank Country :</span>
                                                          <asp:TextBox ID="txtRemBankCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                                              AutoPostBack="true" MaxLength="2" TabIndex="22" OnTextChanged="txtRemBankCountry_TextChanged" Enabled="false"></asp:TextBox>
                                                          <asp:Button ID="Button5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                              OnClientClick="return openCountryCode('mouseClick','2');" Enabled="false"/>
                                                          &nbsp;<asp:Label ID="lblRemBankCountryDesc" runat="server" CssClass="elementLabel"
                                                              Width="200px"></asp:Label>
                                                              </td>
                                                    </tr>
                                                    <tr>
                                                    <td style="text-align: right;" >
                                                            <span class="elementLabel">Purpose Code : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textBox" Style="width: 50px"
                                                                 AutoPostBack="true" OnTextChanged="txtPurposeCode_TextChanged" MaxLength="6"
                                                                 TabIndex="23" Enabled="false"></asp:TextBox>
                                                             <asp:Button ID="btnpurposecode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                 OnClientClick="return openPurposeCode('mouseClick','1');" Enabled="false"/>
                                                             &nbsp;<asp:Label ID="lblpurposeCode" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    <td style="text-align: left; white-space: nowrap">
                                                         <span class="elementLabel" style="padding-left: 28px;">Purpose Of Remittance :</span>
                                                         <asp:TextBox ID="txtpurposeofRemittance" CssClass="textBox" runat="server" MaxLength="100"
                                                                Width="200px" TabIndex="24" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                        <span class="elementLabel" style="padding-left: 35px;">Mode of Payment : </span>
                                                            <%--<asp:TextBox ID="txtModeofPayment" runat="server" MaxLength="50"  CssClass="textBoxRight" TabIndex="18"
                                                                Style="width: 120px"></asp:TextBox>--%>
                                                             <asp:DropDownList ID="ddlModeOfPayment" runat="server" CssClass="dropdownList"
                                                                  TabIndex="25" Height="19px" Style="width: 130px" Enabled="false" ToolTip="Mode of payment (e.g.SWIFT or any mechanism,NEFT, RTGS etc through which payment is received)">
                                                                 <asp:ListItem Value="">---Select---</asp:ListItem>  
                                                                     <asp:ListItem Value="SWIFT">SWIFT</asp:ListItem> 
                                                                     <asp:ListItem Value="RTGS/NEFT">RTGS/NEFT</asp:ListItem>  
                                                             </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <asp:Panel ID="pnldocTypeInterest" runat="server">
                                                    <tr>
                                                  <td style="text-align: right;" >
                                                        <span id="pnlpayer" runat="server" class="elementLabel">Interest Payer :</span>
                                                        </td>
                                                        <td align="left">
                                                        <asp:RadioButton ID="rdbShipper" runat="server" CssClass="elementLabel" Text="Shipper" TabIndex="26"
															AutoPostBack="true" GroupName="grpPayer" Enabled="false"/>
														<asp:RadioButton ID="rdbBuyer" runat="server" CssClass="elementLabel" Text="Buyer"
															AutoPostBack="true" GroupName="grpPayer" Enabled="false"/>
                                                            <span class="elementLabel" style="padding-left: 20px;">Interest From : </span>
                                                            <asp:TextBox ID="txtIntFrmDate1" runat="server" CssClass="textBox" MaxLength="10"
																		ValidationGroup="dtVal" Width="70px" TabIndex="27" Enabled="false"></asp:TextBox>
																	<ajaxToolkit:MaskedEditExtender ID="mFrmDate1" Mask="99/99/9999" MaskType="Date"
																		runat="server" TargetControlID="txtIntFrmDate1" ErrorTooltipEnabled="True" CultureName="en-GB"
																		CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
																		CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
																		CultureTimePlaceholder=":" Enabled="True">
																	</ajaxToolkit:MaskedEditExtender>
															</td>
															<td align="left">
                                                            <span class="elementLabel" style="padding-left: 25px;">Interest To : </span>
                                                            <asp:TextBox ID="txtIntToDate1" runat="server" CssClass="textBox" MaxLength="10"
																		ValidationGroup="dtVal" Width="70px" TabIndex="28" onblur="CalculateNoOfDays('1','2');" Enabled="false"></asp:TextBox>
																	<ajaxToolkit:MaskedEditExtender ID="mToDate1" Mask="99/99/9999" MaskType="Date" runat="server"
																		TargetControlID="txtIntToDate1" ErrorTooltipEnabled="True" CultureName="en-GB"
																		CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
																		CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
																		CultureTimePlaceholder=":" Enabled="True">
																	</ajaxToolkit:MaskedEditExtender>
                                                            <span class="elementLabel" style="padding-left: 25px;">No Of Days : </span>
                                                            <asp:TextBox ID="txtForDays1" runat="server" CssClass="textBox" Style="text-align: right"
																		Width="30px" TabIndex="29" onfocus="this.select()" MaxLength="4" Enabled="false"></asp:TextBox>
                                                            <span class="elementLabel" style="padding-left: 25px;">Rate : </span>
                                                            <asp:TextBox ID="txtIntRate1" runat="server" CssClass="textBox" Style="text-align: right"
																		Width="60px" TabIndex="30" onfocus="this.select()" MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        <td style="text-align: left;" nowrap>
                                                        <span class="elementLabel" style="padding-left: 40px;">Interest Amount :</span>
                                                        <asp:TextBox ID="txtInterestAmt" CssClass="textBox" runat="server"
                                                                Width="150px" TabIndex="31" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    </asp:Panel>
                                                    <tr width="50%">
                                                        <td align="right" nowrap>
                                                            <span class="elementLabel">FIRC :</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            <asp:CheckBox ID="chkFirc" Text="No" runat="server" TabIndex="-1" CssClass="elementLabel"
                                                                AutoPostBack="True" OnCheckedChanged="chkFirc_CheckedChanged" Enabled="false"/>
                                                        </td>
                                                        <td align="left" colspan="2" nowrap>
                                                            <span class="elementLabel" style="margin-left:50px;">FIRC No :</span>
                                                            <asp:TextBox ID="txtFircNo" MaxLength="50" Enabled="False" runat="server" CssClass="textBox"
                                                                Height="14px" Width="150px" TabIndex="32"></asp:TextBox>
                                                            <span class="elementLabel" style="margin-left:10px;">FIRC AD Code :</span>
                                                            <asp:TextBox ID="txtFircAdCode" MaxLength="7" Enabled="False" runat="server" CssClass="textBox"
                                                                Height="14px" Width="50px" TabIndex="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="right" nowrap>
                                                    <span class="elementLabel">Remarks :</span> 
                                                    </td>
                                                     <td align="left" colspan="2" nowrap>
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="textBox" Enabled="false" Height="14px" MaxLength="100" onfocus="this.select()" TabIndex="34" Width="504px"></asp:TextBox>
                                                            </td>
                                                        <td align="left" nowrap>
                                                            <asp:TextBox ID="txtNegotiatedAmt" runat="server" CssClass="textBox" Height="14px"
                                                            Width="70px" Enabled="false" Visible="false" Style="text-align: right"></asp:TextBox>
                                                        <asp:TextBox ID="txtNegotiatedAmtinINR" runat="server" CssClass="textBox" Height="14px"
                                                            Width="100px" Enabled="false" Visible="false" Style="text-align: right"></asp:TextBox>
                                                        </td>    
                                                    </tr>
                                                    </table>
                                                    </td>
                                                        </div>
                                                   <div style="width:30%">
                                                    <tr>
                                                    <td style="text-align: left; padding-left:145px">
                                                     <span class="elementLabel" style="color: Red; font-weight: bold">Create IRM :</span>
                                                                                    <asp:CheckBox ID="chkIRMCreate" runat="server" CssClass="elementLabel" TabIndex="35"
                                                                                        OnCheckedChanged="chkIRMCreate_CheckedChanged" Enabled="false" AutoPostBack="true" OnClientClick="ValidateIRM();" />
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                  <span class="elementLabel" style="color: Red; font-weight: bold">Link TT Reference No :</span>
                                                   <asp:CheckBox ID="btnTTRefNoList" runat="server" CssClass="elementLabel" Enabled="false" OnCheckedChanged="chkTTRefNo_CheckedChanged" TabIndex="36" AutoPostBack="true"/>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <span class="elementLabel" ID="lblDummySettlement" runat="server" style="color: Red; font-weight: bold">Dummy Settlement :</span>
                                                                                    <asp:CheckBox ID="chkDummySettlement" runat="server" CssClass="elementLabel" TabIndex="35"
                                                                                        Enabled="false"  />
                                                    </td>
                                                   
                                                    </tr>
                                                    <td colspan="1" align="left">
														<table width="45%">
                                                        
                                                    <asp:Panel ID="pnlIRMCreate" runat="server" Visible="false">
                                                    <tr>
                                                    <td style="text-align: right;">
                                                            <span class="mandatoryField" style="margin-left:16px;">*<span class="elementLabel">Bank Unique Transaction ID :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBankUniqueTransactionID" MaxLength="20" runat="server" CssClass="textBoxRight" TabIndex="18"
                                                                Style="width: 180px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                           <span class="mandatoryField">* 
                                                            <span class="elementLabel">IFSC Code : </span>
                                                        </td>
                                                        <td align="left" >
                                                            <asp:TextBox ID="txtIFSCCode" MaxLength="11" runat="server" CssClass="textBoxRight" TabIndex="18"
                                                                width= "120px" Style='text-transform: uppercase' Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                    <td style="text-align: right;">
                                                           <span class="mandatoryField">*<span class="elementLabel">Remittance AD Code : </span>
                                                        </td>
                                                        <td align="left" >
                                                            <asp:TextBox ID="txtRemittanceADCode" runat="server" MaxLength="50" CssClass="textBoxRight" TabIndex="18"
                                                                Style="width: 120px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <span class="mandatoryField">*<span class="elementLabel">IEC Code : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtIECCode" runat="server" CssClass="textBoxRight" MaxLength="10" TabIndex="18"
                                                                Style="width: 120px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                     <tr>
                                                    <td style="text-align: right;">
                                                            <span class="mandatoryField">*<span class="elementLabel">Pan Number : </span>
                                                        </td>
                                                        <td align="left" colspan="1" >
                                                            <asp:TextBox ID="txtPanNumber" runat="server"  CssClass="textBoxRight" 
                                                                MaxLength="10" TabIndex="18"
                                                                 Width="120px" ViewStateMode="Enabled" Style='text-transform: uppercase' Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;" nowrap>
                                                            <span class="mandatoryField">*<span class="elementLabel">Bank Reference Number : </span>
                                                        </td>
                                                        <td align="left" >
                                                           <asp:TextBox ID="txtBankReferencenumber" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="20" TabIndex="33"
                                                              Style="width: 140px" ToolTip="Bank reference number corresponding to IRM message."></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                    <td style="text-align: right;">
                                                            <span class="mandatoryField">*<span class="elementLabel">Bank Account Number : </span>
                                                        </td>
                                                        <td align="left" >
                                                            <asp:TextBox ID="txtBankAccountNumber" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="25" TabIndex="34"
                                                                    ToolTip="Bank account number"  Style="width: 180px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <span class="mandatoryField">*<span class="elementLabel">IRM Status : </span>
                                                        </td>
                                                        <td align="left">
                                                             <asp:DropDownList ID="ddlIRMStatus" runat="server" CssClass="dropdownList"
                                                                   TabIndex="37" Height="19px" Enabled="false" ToolTip="IRM Status(F – Fresh,A -Amended,C – Cancelled)">
                                                                  <asp:ListItem Value="">--Select--</asp:ListItem>  
                                                                      <asp:ListItem Value="Fresh">Fresh</asp:ListItem> 
                                                                      <asp:ListItem Value="Amended">Amended</asp:ListItem>  
                                                                      <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                                              </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    </asp:Panel>
                                                   </table>
                                                   </td>
                                                   
                                                    </div>
                                                    <div style="width:20%;">
                                                    <tr>
                                                    <td>
                                                    </td>
                                                    </tr>
                                                    <tr>
													<td colspan="7" align="left">
														<table width="70%" style="margin-left:80px;">
															<tr>
																<td>
																	<br />
																	<ajaxToolkit:CollapsiblePanelExtender ID="cpe2" runat="Server" TargetControlID="panelSecondAdd"
																		CollapsedSize="0" ExpandedSize="160" Collapsed="true" ExpandControlID="btnTTRefNoList"
																		CollapseControlID="btnTTRefNoList" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
																		ExpandDirection="Vertical"   />
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
																								<asp:TextBox ID="txtTTRefNo1"  runat="server" TabIndex="-1" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo1_TextChanged" Enabled="false"></asp:TextBox>
																								<asp:Button ID="btnTTRef1"  runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('1');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname1" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr1" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                   <asp:DropDownList ID="ddlTTCurrency1" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt1" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt1" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount1" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr1" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr1" runat="server" CssClass="dropdownList" Enabled="false" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate1" AutoPostBack="true" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"  OnTextChanged="txtTTCrossCurrRate1_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised1" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo2" runat="server" TabIndex="-1" CssClass="textBox" Width="150px" OnTextChanged="txtTTRefNo2_TextChanged" Enabled="false"></asp:TextBox>
																								<asp:Button ID="btnTTRef2" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('2');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname2" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr2" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency2" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt2" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt2" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount2" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr2" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr2" runat="server" CssClass="dropdownList" Enabled="false" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate2" runat="server" AutoPostBack="true" TabIndex="-1" Enabled="false" CssClass="textBox" Width="80px" OnTextChanged="txtTTCrossCurrRate2_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised2" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo3" runat="server" TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo3_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef3" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('3');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname3" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr3" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency3" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt3" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt3" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount3" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr3" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr3" runat="server" CssClass="dropdownList" Enabled="false" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate3" runat="server" AutoPostBack="true" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false" OnTextChanged="txtTTCrossCurrRate3_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised3" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo4" runat="server" TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo4_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('4');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname4" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr4" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency4" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt4" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt4" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount4" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr4" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr4" runat="server" CssClass="dropdownList" Enabled="false" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate4" runat="server" AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate4_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised4" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo5" runat="server" TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo5_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('5');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname5" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr5" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency5" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTAmt5" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt5" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount5" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr5" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr5" runat="server" CssClass="dropdownList" Enabled="false" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td> 
																								<asp:TextBox ID="txtTTCrossCurrRate5" runat="server" AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate5_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised5" runat="server" TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
                                                                                  <tr><td align="right" colspan="5">
														<asp:Button ID="btn_Trans_Add" runat="server" Text="Add" CssClass="buttonDefault"
															 TabIndex="106" OnClick="btn_Trans_Add_Click"/>&nbsp;&nbsp;&nbsp;
														
													</td>
                                                                                        </tr>
                                                                                        <asp:Panel ID="TT5Row" runat="server" Visible="false">
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo6" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo6_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef6" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('6');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname6" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr6" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency6" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt6" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt6" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount6" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr6" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr6" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate6" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate6_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised6" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo7" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo7_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef7" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('7');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname7" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr7" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency7" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt7" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt7" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount7" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr7" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr7" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate7" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate7_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised7" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo8" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo8_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef8" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('8');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname8" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr8" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency8" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt8" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTAmt8" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount8" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr8" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr8" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate8" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate8_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised8" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
                                                                                                  <tr><td align="right" colspan="5">
														<asp:Button ID="btn_Trans_Add1" runat="server" Text="Add" CssClass="buttonDefault"
															 TabIndex="106" OnClick="btn_Trans_Add1_Click"/>&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Trans_Remove1" runat="server" Text="Remove" CssClass="buttonDefault"
															 TabIndex="106" OnClick="btn_Trans_Remove1_Click"/>
													</td>
                                                                                        </tr>
                                                                                        </asp:Panel>
                                                                                        <asp:Panel ID="TT8Row" runat="server" Visible="false">
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo9" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo9_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef9" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('9');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname9" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr9" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency9" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTtTTAmt9" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt9" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount9" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr9" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr9" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate9" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate9_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised9" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo10" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo10_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef10" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('10');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname10" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr10" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                    <asp:DropDownList ID="ddlTTCurrency10" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt10" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt10" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount10" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr10" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr10" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate10" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate10_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised10" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo11" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo11_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef11" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('11');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname11" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr11" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                     <asp:DropDownList ID="ddlTTCurrency11" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt11" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt11" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount11" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr11" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr11" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate11" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate11_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised11" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
                                                                                                  <tr><td align="right" colspan="5">
														<asp:Button ID="btn_Trans_Add2" runat="server" Text="Add" CssClass="buttonDefault"
															 TabIndex="106" OnClick="btn_Trans_Add2_Click"/>&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btn_Trans_Remove2" runat="server" Text="Remove" CssClass="buttonDefault"
															 TabIndex="106" OnClick="btn_Trans_Remove2_Click"/>
													</td>
                                                                                        </tr>
                                                                                        </asp:Panel>
                                                                                        <asp:Panel ID="TT11Row" runat="server" Visible="false">
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo12" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo12_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef12" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('12');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname12" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr12" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                     <asp:DropDownList ID="ddlTTCurrency12" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt12" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt12" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount12" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr12" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr12" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate12" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate12_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised12" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo13" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo13_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef13" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('13');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname13" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr13" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                     <asp:DropDownList ID="ddlTTCurrency13" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt13" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt13" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount13" runat="server"  TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr13" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr13" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate13" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" OnTextChanged="txtTTCrossCurrRate13_TextChanged" Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised13" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo14" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo14_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef14" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('14');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname14" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr14" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                     <asp:DropDownList ID="ddlTTCurrency14" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt14" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt14" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount14" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr14" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr14" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate14" runat="server"   AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate14_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised14" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
																						<tr>
																							<td nowrap align="left">
																								<asp:TextBox ID="txtTTRefNo15" runat="server"  TabIndex="-1" CssClass="textBox" Width="150px" Enabled="false" OnTextChanged="txtTTRefNo15_TextChanged"></asp:TextBox>
																								<asp:Button ID="btnTTRef15" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
																									OnClientClick="return OpenTTNoList('15');" Enabled="false"/>
                                                                                                    <asp:Label ID="lblTTRname15" runat="server" Width="16px" CssClass="lblHelp_TT" Visible="false"></asp:Label>
																							</td>
																							<td>
																								<%--<asp:TextBox ID="txtTTCurr15" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>--%>
                                                                                                     <asp:DropDownList ID="ddlTTCurrency15" Enabled="false" runat="server" CssClass="dropdownList" TabIndex="57">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTotTTAmt15" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtBalTTAmt15" runat="server"  Enabled="false" TabIndex="-1" CssClass="textBox"
																									Width="80px"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmount15" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																							 <td>
																								<%--<asp:TextBox ID="txtTTRealisedCurr15" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="ddlTTRealisedCurr15" runat="server" CssClass="dropdownList" TabIndex="57" Enabled="false">																													
	                                                                                                 </asp:DropDownList>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTCrossCurrRate15" runat="server"  AutoPostBack="true" TabIndex="-1" CssClass="textBox" Enabled="false" Width="80px" OnTextChanged="txtTTCrossCurrRate15_TextChanged"></asp:TextBox>
																							</td>
																							<td>
																								<asp:TextBox ID="txtTTAmtRealised15" runat="server"  TabIndex="-1" CssClass="textBox" Width="80px" Enabled="false"></asp:TextBox>
																							</td>
																						</tr>
                                                                  <tr><td align="right" colspan="5">
													
														<asp:Button ID="btn_Trans_Remove3" runat="server" Text="Remove" CssClass="buttonDefault"
															 TabIndex="106" OnClick="btn_Trans_Remove3_Click"/>
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
                                                    </div>
                                                    <div>
                                                    <td align="center">
                                                    <asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="buttonDefault" ToolTip="Go to Transactions Details"
                                                                    OnClientClick="return OnDocNextClick(1);"/>
                                                                    </td>
                                                      </div>
                                        <asp:Panel ID="tbtransactionDetails" runat="server" HeaderText="TRANSACTION Details"
                                            Font-Bold="true" ForeColor="White" Visible="false">
                                            <%--<ContentTemplate>--%>
                                                <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                                    <caption>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <tr>
                                                            <td align="left" colspan="4">
                                                                <asp:RadioButtonList ID="rdbTransType" runat="server" AutoPostBack="True" CssClass="elementLabel" OnSelectedIndexChanged="rdbTransType_SelectedIndexChanged" RepeatDirection="Horizontal" Style="font-weight: bold;" TabIndex="12">
                                                                    <asp:ListItem Text="Full F.Contract" Value="FC"></asp:ListItem>
                                                                    <asp:ListItem Text="Full INR" Value="INR"></asp:ListItem>
                                                                    <asp:ListItem Text="Full Cross Currency" Value="CC"></asp:ListItem>
                                                                    <asp:ListItem Text="Part Conversion" Value="PC"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <%-- added from document tab start--%>
                                                        <tr>
                                                        <td>
                                                         <span class="elementLabel" style="color: Red; font-weight: bold; margin-left:100px;">Manual GR :</span>
                                                            <asp:CheckBox ID="chkManualGR" runat="server" CssClass="elementLabel" TabIndex="102"
                                                                onclick="ChangeManualGRText();" />
                                                            <asp:Label ID="lblManualGR" runat="server" CssClass="elementLabel" Style="color: Red;"></asp:Label>
                                                        <span class="elementLabel">Negotiated Amt. :</span>
                                                        
                                                        <span class="elementLabel" style="padding-left: 108px">Negotiated Amt. in र :</span>
                                                        
                                                            <span class="elementLabel" style="padding-left: 150px;">Other Currency :</span>
                                                        <asp:TextBox ID="txtOtherCurrency" runat="server" CssClass="textBox" Height="14px"
                                                            Width="35px" Enabled="false"></asp:TextBox>
                                                            <span class="elementLabel">Invoice No :</span>
                                                        <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                                            Enabled="false"></asp:TextBox>
                                                        <asp:CheckBox ID="chkBank" runat="server" Enabled="false" Style="vertical-align: top; padding-left: 187px" />
                                                        <span class="elementLabel" style="vertical-align: top; font-weight: bold; color: Red;">Bank Line Transfer</span>
                                                        <span class="elementLabel" style="padding-left: 157px;">Date Delink :</span>
                                                        <asp:TextBox ID="txtDateDelinked" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                                            Enabled="false"></asp:TextBox>
                                                        <span class="elementLabel" style="padding-left: 130px;">Accepted Due Date :</span>
                                                        <asp:TextBox ID="txtAcceptedDueDate" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                                            Enabled="false"></asp:TextBox>
                                                            <span class="elementLabel" style="padding-left: 145px;">Date Negotiated :</span>
                                                        <asp:TextBox ID="txtDateNegotiated" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                                            Enabled="false"></asp:TextBox>
                                                        </td>
                                                        </tr>
                                                        <%-- added from document tab end --%>
                                                        <tr>
                                                        <span class="elementLabel" style="margin-left: 22px;">Interest Rate :</span>
                                                            <asp:TextBox ID="txtInterestRate2" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" TabIndex="7" Width="35px"></asp:TextBox>
                                                            <span class="elementLabel">For</span>&nbsp;
                                                            <asp:TextBox ID="txtNoofDays2" runat="server" CssClass="textBox" Height="14px" TabIndex="8" Width="35px"></asp:TextBox>&nbsp; <span class="elementLabel">Days</span>
                                                            <asp:CheckBox ID="chkLoanAdvanced" runat="server" Enabled="False" Style="vertical-align: top;" />
                                                            <asp:Label ID="lblLoan" runat="server" CssClass="elementLabel" Style="color: Red; font-weight: bold; vertical-align: top;" Text="Loan Advanced"></asp:Label>
                                                        
                                                            <td align="right" nowrap><span class="elementLabel">Forward Contract No :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtForwardContract" runat="server" CssClass="textBox" Height="14px" MaxLength="12" onfocus="this.select()" TabIndex="13" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap>
                                                                <asp:RadioButtonList ID="rdbTransType2" runat="server" AutoPostBack="True" CssClass="elementLabel" OnSelectedIndexChanged="rdbTransType2_SelectedIndexChanged" RepeatDirection="Horizontal" Style="font-weight: bold;" TabIndex="14">
                                                                    <asp:ListItem Text="Part EEFC" Value="PEEFC"></asp:ListItem>
                                                                    <asp:ListItem Text="Full EEFC" Value="FEEFC"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">EEFC A/c Amt :</span> </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPartConAmt" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right;" TabIndex="15" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" colspan="3"><span class="elementLabel">Cross Curr :</span>
                                                                <asp:TextBox ID="txtConCrossCur" runat="server" AutoPostBack="True" CssClass="textBox" Enabled="False" Height="14px" MaxLength="3" onfocus="this.select()" TabIndex="16" Width="47px"></asp:TextBox>
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btnOtrCrossCur" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                                <span class="elementLabel">Cross Curr. Rate :</span>
                                                                <asp:TextBox ID="txtConCurRate" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right;" TabIndex="17" Width="70px"></asp:TextBox>
                                                                &nbsp;&nbsp; <span class="elementLabel">Total Amt. :</span>
                                                                <asp:TextBox ID="txtTotConRate" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right; font-weight: bold;" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Non EEFC A/c Amt :</span> </td>
                                                            <td align="left">
                                                                
                                                            </td>
                                                            <td align="left" colspan="3"><span class="elementLabel">Cross Curr :</span>
                                                                <asp:TextBox ID="txtEEFCCurrency" runat="server" AutoPostBack="True" CssClass="textBox" Enabled="False" Height="14px" MaxLength="3" onfocus="this.select()" TabIndex="19" Width="47px"></asp:TextBox>
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btnEEFCCurrency" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                                <span class="elementLabel">Cross Curr. Rate :</span>
                                                                <asp:TextBox ID="txtCrossCurrRate" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right;" TabIndex="20" Width="70px"></asp:TextBox>
                                                                &nbsp;&nbsp; <span class="elementLabel">Total Amt. :</span>
                                                                <asp:TextBox ID="txtEEFCAmtTotal" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right; font-weight: bold;" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Balance Amount :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtBalAmt" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right;" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap><span class="elementLabel" style="margin-left:20.5px;">Balance Amount in र :</span>
                                                                <asp:TextBox ID="txtBalAmtinINR" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Collection Amount :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtCollectionAmt" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right; font-weight: bold;" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap><span class="elementLabel" style="margin-left: 10px;">Collection Amount in र :</span>
                                                                <asp:TextBox ID="txtCollectionAmtinINR" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Interest Amount :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtInterest" runat="server" CssClass="textBox" Height="14px" Style="text-align: right;" TabIndex="21" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap><span class="elementLabel" style="margin-left:20px;">Interest Amount in र :</span>
                                                                <asp:TextBox ID="txtInterestinINR" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Other Bank Charges :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtOtherBank" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right" TabIndex="22" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap><span class="elementLabel">Other Bank Charges in र :</span>
                                                                <asp:TextBox ID="txtOtherBankinINR" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right"><span class="elementLabel">FBK Charges :</span> </td>
                                                            <td align="left" nowrap>
                                                                
                                                            </td>
                                                            <td align="left"><span class="elementLabel" style="margin-left:40px;">FBK Charges in र :</span>
                                                                <asp:TextBox ID="txt_fbkchargesinRS" runat="server" CssClass="textBox" Enabled="False" MaxLength="20" onfocus="this.select()" Style="text-align: right" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">STax Applicable</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:CheckBox ID="chkStax" runat="server" AutoPostBack="True" CssClass="elementLabel" OnCheckedChanged="chkStax_CheckedChanged" TabIndex="23" />
                                                                <span class="elementLabel">Yes/No</span> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel" style="margin-left:50px;">STax (%) : </span></td>
                                                            <td align="left" nowrap>
                                                                <asp:DropDownList ID="ddlServicetax" runat="server" CssClass="textBox" TabIndex="24">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtServiceTax" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right; font-weight: bold;" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="LEFT" colspan="3"><span class="elementLabel">STax Total Amt :</span>
                                                                <asp:TextBox ID="txtsbcess" runat="server" CssClass="textBox" Enabled="False" Style="text-align: right; font-weight: bold" Width="40px"></asp:TextBox>
                                                                <asp:TextBox ID="txtSBcesssamt" runat="server" CssClass="textBox" Style="text-align: right" TabIndex="24" Width="80px"></asp:TextBox>
                                                                <span class="elementLabel">SBCess (%) :KKCess (%) :</span>
                                                                <asp:TextBox ID="txt_kkcessper" runat="server" CssClass="textBox" Enabled="False" Style="text-align: right; font-weight: bold;" Width="40px"></asp:TextBox>
                                                                &nbsp;<asp:TextBox ID="txt_kkcessamt" runat="server" CssClass="textBox" Style="text-align: right" TabIndex="25" Width="80px"></asp:TextBox>
                                                                <span class="elementLabel"></span>
                                                                <asp:TextBox ID="txtsttamt" runat="server" CssClass="textBox" Style="text-align: right; font-weight: bold" TabIndex="26" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Swift Charges :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtSwift" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right" TabIndex="27" Width="100px"></asp:TextBox>
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Courier Charges :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtCourier" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right" TabIndex="28" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap><span class="elementLabel">Over Due Charges : </span>
                                                                <asp:TextBox ID="txtOverDue" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right" TabIndex="28" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">STax on FxDls : </span></td>
                                                            <td align="left">
                                                                <asp:CheckBox ID="chkFxDls" runat="server" AutoPostBack="True" CssClass="elementLabel" Height="10px" OnCheckedChanged="chkFxDls_CheckedChanged" onfocus="this.select()" Style="vertical-align: top;" TabIndex="29" />
                                                                <span class="elementLabel">Yes/No</span> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">STax on FxDls : </span></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFxDlsCommission" runat="server" CssClass="textBox" Height="14px" Style="text-align: right;" TabIndex="29" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" nowrap><span class="elementLabel">SBCess on FxDls :</span>
                                                                <asp:TextBox ID="txtsbfx" runat="server" CssClass="textBox" Style="text-align: right" TabIndex="29" Width="80px"></asp:TextBox>
                                                                <span class="elementLabel">KKCess on FxDls :</span>
                                                                <asp:TextBox ID="txt_kkcessonfx" runat="server" CssClass="textBox" Style="text-align: right" TabIndex="29" Width="80px"></asp:TextBox>
                                                                <span class="elementLabel">Total Sevice Tax On FxDls :</span>
                                                                <asp:TextBox ID="txttotcessfx" runat="server" CssClass="textBox" Style="text-align: right; font-weight: bold;" TabIndex="29" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Profit Lieu Applicable</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:CheckBox ID="chkProfitLio" runat="server" AutoPostBack="True" CssClass="elementLabel" OnCheckedChanged="chkProfitLio_CheckedChanged" TabIndex="-1" Text="No" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Profit Lieu (%) : </span></td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtprofitper" runat="server" CssClass="textBox" Enabled="False" MaxLength="2" onfocus="this.select()" TabIndex="31" Width="20px"></asp:TextBox>
                                                                <asp:Button ID="btnprofitlist" runat="server" CssClass="btnHelp_enabled" OnClientClick="return OpenProfitLieuList();" TabIndex="-1" />
                                                                <asp:TextBox ID="txtprofitamt" runat="server" CssClass="textBox" Height="14px" Style="text-align: right; font-weight: bold" TabIndex="31" Width="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Bank Cert/Comm. Lieu :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtBankCertificate" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()" Style="text-align: right" TabIndex="32" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">Commission :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtCommissionID" runat="server" CssClass="textBox" Enabled="False" MaxLength="2" onfocus="this.select()" TabIndex="33" Width="20px"></asp:TextBox>
                                                                <asp:Button ID="btnCommissionList" runat="server" CssClass="btnHelp_enabled" OnClientClick="return OpenCommissionList();" TabIndex="-1" />
                                                                <asp:TextBox ID="txtCommission" runat="server" CssClass="textBox" Height="14px" Style="text-align: right; font-weight: bold" TabIndex="34" Width="70px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" colspan="2" nowrap><span class="elementLabel" style="margin-left:6px;">Receiving Bank :</span>
                                                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="textBox" TabIndex="35">
                                                                    <asp:ListItem Value="BNSNY"></asp:ListItem>
                                                                    <asp:ListItem Value="Citi"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" nowrap><span class="elementLabel">EEFC in र :</span> </td>
                                                            <td align="left" nowrap>
                                                                <asp:TextBox ID="txtEEFCinINR" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right; font-weight: bold;" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" colspan="2" nowrap><span class="elementLabel">Net Amount in र :</span>
                                                                <asp:TextBox ID="txtNetAmt" runat="server" CssClass="textBox" Enabled="False" Height="14px" Style="text-align: right" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                        <td></td>
                                                       <td align="right" colspan="5">
                                                        <asp:Button ID="btn_Trans_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back to Document Details" TabIndex="106" OnClientClick="return OnDocNextClick(1);"/>&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btn_Trans_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                            ToolTip="Go to G-Base Details" TabIndex="106" OnClientClick="return OnDocNextClick(1);"/>
                                                    </td>
                                                            
                                                            <td align="left" colspan="4"></td>
                                                        </tr>
                                                    </caption>
                                                </table>
                                            <%--</ContentTemplate>--%>
                                        </asp:Panel>
                                                </table>
                                            </ContentTemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel ID="tbshippingbillDetails" runat="server" HeaderText="Shipping Bills Details"
                                            Font-Bold="true" ForeColor="White">
                                            <ContentTemplate>
                                                    <table cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                        <td colspan="12" style="width: 100%;">
                                                            <asp:GridView ID="GridViewGRPPCustomsDetails" runat="server" AutoGenerateColumns="false"
                                                                Width="95%" GridLines="Both" AllowPaging="true" PageSize="40" >
                                                                <PagerSettings Visible="false" />
                                                                <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                                    CssClass="gridAlternateItem" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="1%" ItemStyle-Width="5%">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="HeaderChkAllow" runat="server" Text="Select All" AutoPostBack="true" Enabled="false"
                                                                                  /> <%--OnCheckedChanged="HeaderChkAllow_CheckedChanged"--%>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="RowChkAllow" runat="server" Enabled="false" /> <%--OnCheckedChanged="RowChkAllow_CheckedChanged"--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Shipping Bill No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblShipping_Bill_No" runat="server" Text='<%#Eval("Shipping_Bill_No") %>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo")%>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Document No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDocument_No" runat="server" Text='<%#Eval("Document_No") %>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ship. Bill Curr" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblShipCurr" runat="server" Text='<%#Eval("GRCurrency") %>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="6%" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FOB Amount." HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFOBAmount" runat="server" Text='<%# Eval("FOBAmount","{0:0.00}")%>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Insur.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInsuranceAmount" runat="server" Text='<%# Eval("InsuranceAmount","{0:0.00}")%>' CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Frgt.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFreightAmount" runat="server" Text='<%# Eval("FreightAmount","{0:0.00}") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:0.00}") %>'
                                                                                CssClass="elementLabel"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Indicator" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="3%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlpartfull" runat="server" Enabled="false" CssClass="dropdownList" Text='<%#Eval("Indicator_PartFull") %>'>
                                                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                                <asp:ListItem Text="Full" Value="Full"></asp:ListItem>
                                                                                <asp:ListItem Text="Part" Value="Part"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Settlement Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsettelementamt" runat="server" Enabled="false" Text='<%# Eval("SettlementAmt","{0:0.00}") %>' onfocus="this.select()" CssClass="textBox" Style="text-align: right;" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="6%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="F.BANK" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="10%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtFBANK" runat="server" Enabled="false" Text='<%# Eval("FBANK","{0:0.00}") %>'  onfocus="this.select()" CssClass="textBox" Style="text-align: right;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                         </td> 
                                                        </tr>
                                                        <tr>
                                                        <td align="center" style="width: 100%" valign="top" colspan="12">
                                                            <asp:Label ID="labelMessageSB" runat="server" CssClass="mandatoryField"></asp:Label>
                                                            <span class="elementLabel" runat="server" id="lblPeningSB" style="color: Black; font-weight: bold">Pending Shipping Bills :</span>
                                                             <asp:CheckBox ID="chkSB" runat="server" CssClass="elementLabel" TabIndex="102"
                                                                onclick="ChangeSBText();" Enabled="false"/>
                                                            <asp:Label ID="lblSB" runat="server" CssClass="elementLabel" Style="color: Red;"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="label3" runat="server" CssClass="mandatoryField"></asp:Label>
                                                            <asp:Button ID="btn_Shipp_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                                    ToolTip="Back to Document Details" TabIndex="106" OnClientClick="return OnShippPrevClick();"/>&nbsp;&nbsp;&nbsp;
                                                                                <asp:Button ID="btn_Shipp_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                                                    ToolTip="Go to Export Accounting" TabIndex="106" OnClientClick="return OnShippNextClick();"/>
                                                        </td>
                                                        </tr>
                                                        </table>   
                                                      </ContentTemplate>
                                        </ajaxToolkit:TabPanel>
                                         <%-- Anand 26-06-2023--%>
                                         <ajaxToolkit:TabPanel ID="tbDocumentAccounting" runat="server" HeaderText="EXPORT ACCOUNTING"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <ajaxToolkit:TabContainer ID="TabSubContainerACC" runat="server" ActiveTabIndex="0"
                                            CssClass="ajax__subtab_xp-theme">
                                            <ajaxToolkit:TabPanel ID="TabPanelACC1" runat="server" HeaderText="EXPORT ACCOUNTING I"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_IMPACC1Flag" Text="EXPORT ACCOUNTING I" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_IMPACC1Flag_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC1" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC1_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                    <asp:Button ID="btn_IMPACC1_FCRefNo_help" runat="server" ToolTip="Press for FC Ref No list."
                                                                        CssClass="btnHelp_enabled" Enabled="false" />
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_DiscAmt" runat="server" 
                                                                        Enabled="false" CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="20%">
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">2-MATU</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">LUMP</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">CONTRACT NO.</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EX. CCY</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EXCH. RATE</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">INTNL EX. RATE</span>
                                                                </td>
                                                                <td align="left" width="20%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">PRINCIPAL :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" 
                                                                        Enabled="false" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" Enabled="false"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="26" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="27" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="30" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="31" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="32" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="33" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="36" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="38"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="39"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" 
                                                                        Enabled="false" TabIndex="42"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="43" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC1_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    Enabled="false" TabIndex="44" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="true"
                                                                                    Style="text-align: right" TabIndex="63" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC1_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                            </td>
                                                                            <td align="center">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="74"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="74" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc1_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to Shipping Details" OnClientClick="return ImportAccountingPrevClick();" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc1_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to EXPORT ACCOUNTING II" OnClientClick="return ImportAccountingNextClick(1);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC2" runat="server" HeaderText="EXPORT ACCOUNTING II"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_IMPACC2Flag" Text="EXPORT ACCOUNTING II" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_IMPACC2Flag_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC2" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC2_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                    <asp:Button ID="btn_IMPACC2_FCRefNo_help" runat="server" ToolTip="Press for FC Ref No list."
                                                                        CssClass="btnHelp_enabled" Enabled="false" />
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_DiscAmt" runat="server" Enabled="false"
                                                                         CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="20%">
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">2-MATU</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">LUMP</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">CONTRACT NO</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EX. CCY</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EXCH. RATE</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">INTNL EX. RATE</span>
                                                                </td>
                                                                <td align="left" width="20%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">PRINCIPAL :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" 
                                                                        Enabled="false" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" Enabled="false"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="26" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="27" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="30" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="31" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="32" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="33" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="36" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" 
                                                                        TabIndex="37" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="38"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="39"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" 
                                                                        Enabled="false"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="43" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC2_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    Enabled="false" TabIndex="44" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false"
                                                                                    Style="text-align: right" TabIndex="63" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC2_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                            </td>
                                                                            <td align="center">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                   Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc2_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to EXPORT ACCOUNTING I" OnClientClick="return ImportAccountingNextClick(0);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc2_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to EXPORT ACCOUNTING III" OnClientClick="return ImportAccountingNextClick(2);"  />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC3" runat="server" HeaderText="EXPORT ACCOUNTING III"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_IMPACC3Flag" Text="EXPORT ACCOUNTING III" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_IMPACC3Flag_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC3" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC3_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" AutoPostBack="true"  Enabled="false"></asp:TextBox>
                                                                    <asp:Button ID="btn_IMPACC3_FCRefNo_help" runat="server" ToolTip="Press for FC Ref No list."
                                                                        CssClass="btnHelp_enabled" Enabled="false" />
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_DiscAmt" runat="server"
                                                                        Enabled="false" CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="20%">
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">2-MATU</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">LUMP</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">CONTRACT NO.</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EX. CCY</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EXCH. RATE</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">INTNL EX. RATE</span>
                                                                </td>
                                                                <td align="left" width="20%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">PRINCIPAL :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" 
                                                                        Enabled="false" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" Enabled="false"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="26" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="27" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="30" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="31" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="32" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                       Enabled="false" TabIndex="33" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="36" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="38"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="39"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                         TabIndex="42"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="43" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC3_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    Enabled="false" TabIndex="44" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                   Enabled="false" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false"
                                                                                    Style="text-align: right" TabIndex="63" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC3_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                            </td>
                                                                            <td align="center">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="71"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="71" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc3_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to EXPORT ACCOUNTING II" OnClientClick="return ImportAccountingNextClick(1);"  />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc3_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to EXPORT ACCOUNTING IV" OnClientClick="return ImportAccountingNextClick(3);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC4" runat="server" HeaderText="EXPORT ACCOUNTING IV"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_IMPACC4Flag" Text="EXPORT ACCOUNTING IV" runat="server" CssClass="elementLabel"
                                                                     OnCheckedChanged="chk_IMPACC4Flag_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC4" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC4_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                    <asp:Button ID="btn_IMPACC4_FCRefNo_help" runat="server" ToolTip="Press for FC Ref No list."
                                                                        CssClass="btnHelp_enabled" Enabled="false" />
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_DiscAmt" runat="server" 
                                                                       Enabled="false" CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="20%">
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">2-MATU</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">LUMP</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">CONTRACT NO.</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EX. CCY</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EXCH. RATE</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">INTNL EX. RATE</span>
                                                                </td>
                                                                <td align="left" width="20%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">PRINCIPAL :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" Enabled="false"
                                                                         Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="26" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                       Enabled="false" TabIndex="27" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="30" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="31" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="32" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="33" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="36" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1"  TabIndex="38" Enabled="false"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" TabIndex="39"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" 
                                                                        Enabled="false"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="43" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC4_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false"/>
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    Enabled="false" TabIndex="44" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Style="text-align: right" TabIndex="63" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC4_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                            </td>
                                                                            <td align="center">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc4_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to EXPORT ACCOUNTING III" OnClientClick="return ImportAccountingNextClick(2);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc4_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to EXPORT ACCOUNTING V" OnClientClick="return ImportAccountingNextClick(4);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC5" runat="server" HeaderText="EXPORT ACCOUNTING V"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_IMPACC5Flag" Text="EXPORT ACCOUNTING V" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_IMPACC5Flag_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC5" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC5_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                    <asp:Button ID="btn_IMPACC5_FCRefNo_help" runat="server" ToolTip="Press for FC Ref No list."
                                                                        CssClass="btnHelp_enabled" Enabled="false"/>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_DiscAmt" runat="server" 
                                                                         CssClass="textBox" TabIndex="20" MaxLength="16" Enabled="false"
                                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="20%">
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">2-MATU</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">LUMP</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">CONTRACT NO.</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EX. CCY</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">EXCH. RATE</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel">INTNL EX. RATE</span>
                                                                </td>
                                                                <td align="left" width="20%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">PRINCIPAL :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" Enabled="false" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" 
                                                                        Enabled="false" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" Enabled="false"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="26" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="27" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="30" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="31" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="32" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        Enabled="false" TabIndex="33" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="36" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="38"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" Enabled="false" TabIndex="39"
                                                                        Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" 
                                                                        Enabled="false" TabIndex="42"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" Enabled="false"
                                                                        TabIndex="43" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC5_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    Enabled="false" TabIndex="44" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Interest_payer" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Enabled="false" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" Style="text-align: right" TabIndex="63" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC5_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false"/>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                            </td>
                                                                            <td align="center">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="true"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Enabled="false" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr4" runat="server" Enabled="false" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc4" runat="server" Enabled="false" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    Style="text-align: right" TabIndex="75" Enabled="false"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer4" runat="server" Enabled="false" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr5" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                     Style="text-align: right" TabIndex="76" Enabled="false"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code6" Enabled="false" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name6" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr6" Enabled="false" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc6" Enabled="false" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr6" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt6" runat="server" Enabled="false" CssClass="textBox" MaxLength="16"
                                                                                     Style="text-align: right" TabIndex="77"
                                                                                    Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer6" runat="server" Enabled="false" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc5_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to EXPORT ACCOUNTING IV" OnClientClick="return ImportAccountingNextClick(3);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc5_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to GENERAL OPERATION 1" OnClientClick="return GeneralOperationNextClick(0);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <%---------------------------------------------------------END------------------------------------------------------------%>
                                        <%---------------------------------------------------------- Supriya Start --------------------------------------%>
                                        <%------------------------------------------------------------GENERAL OPERATION TAB----------------------------------------------------%>
                                        <ajaxToolkit:TabPanel ID="tbGeneralOperation" runat="server" HeaderText="GENERAL OPERATION"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                     <ajaxToolkit:TabContainer ID="TabSubContainerGO" runat="server" ActiveTabIndex="0"
                                            CssClass="ajax__subtab_xp-theme">
                                     <ajaxToolkit:TabPanel ID="pnlGeneralOperation" runat="server" HeaderText="GENERAL OPERATION I"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                     <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_Generaloperation1" Text="GENERAL OPERATION I" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_Generaloperation1_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                        <div id="DivGO" style="width: 75%; float: left; height: 100%">
                                            <table  width="100%">
                                                <asp:Panel ID="panalGO" runat="server" Visible="false">
                                                   
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" >
                                                            <span class="elementLabel" style="margin-left:38px;">VALUE DATE :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtGO_ValueDate" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                                                TabIndex="1" MaxLength="10" onfocus="this.select()" 
                                                                AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdvaldt" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtGO_ValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btncalendar_valdt" runat="server" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <asp:Label ID="Label1" runat="server" CssClass="elementLabel" Style="color: Red; font-weight: bold;"></asp:Label>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtGO_ValueDate" PopupButtonID="btncalendar_valdt" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="mdvaldt"
                                                                ValidationGroup="dtVal" ControlToValidate="txtGO_ValueDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                       <%-- <td align="right">
                                                            <span class="elementLabel">REFERENCE NO : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtGO_Ref_No" Width="100px" runat="server" TabIndex="2"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>--%>
                                                         <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtGO_Comment" runat="server" CssClass="textBox" TabIndex="223"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtGO_Section" runat="server" CssClass="textBox" TabIndex="224"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txtGO_Remark" runat="server" Width="300px" CssClass="textBox"
                                                                                    MaxLength="30" TabIndex="225" onblur="return ToggleGOAcccChange('Remark');"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="226" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
	                                                   <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_SchemeNo" runat="server" CssClass="textBox" TabIndex="227"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="txtGO_Debit" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="228" onchange="return TogggleDebitCreditCode('4','1');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                              <asp:Button ID="btn_GO_Debit_GLCode_help" Enabled="false" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO4','Debit1');" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO_Debit_AccCode_help" Enabled="false" runat="server" ToolTip="Press for AcCode list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Debit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_CCY" runat="server" CssClass="textBox" TabIndex="229"
                                                                                    MaxLength='3' Width="25px" onchange="return txtGO_Debit_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Amt" runat="server" CssClass="textBox" Width="100px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="230" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return GOBranch_Amt_Calculation();"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Cust" runat="server" CssClass="textBox" TabIndex="231"
                                                                                    MaxLength='12' Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtGO_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="231" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="232" MaxLength='5' Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtGO_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="232" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="233" MaxLength='20'></asp:TextBox>
                                                                            </td>
                                                                       
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_ExchRate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="234" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return txtGO_Debit_ExchRate_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_ExchCCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="235" Width="25px" onchange="return txt_GOAccChange_Debit_Exch_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Fund" runat="server" CssClass="textBox" TabIndex="236"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_CheckNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="237" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="238" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Advice_Print" runat="server" CssClass="textBox"
                                                                                    TabIndex="239" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txtGO_Debit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="240" Width="300px" MaxLength="30"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Entity" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="241" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="242" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_InterAmt" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" Width="100px" Style="text-align: right" runat="server" CssClass="textBox"
                                                                                    TabIndex="243"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Debit_InterRate" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" runat="server" CssClass="textBox" TabIndex="244" Width="100px"
                                                                                    Style="text-align: right"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                       <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="txtGO_Credit" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="245" onchange="return TogggleDebitCreditCode('4','2');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO_Credit_GLCode_Help" Enabled="false" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO4','Debit2');" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO_Credit_AccCode_Help" Enabled="false" runat="server" ToolTip="Press for AcCode list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Credit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_CCY" runat="server" CssClass="textBox" TabIndex="246"
                                                                                    Width="25px" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Amt" runat="server" CssClass="textBox" Enabled="true"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="247" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                       <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Cust" runat="server" CssClass="textBox" TabIndex="248"
                                                                                    MaxLength='12' Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtGO_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="248" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            </tr>
                                                                            <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="249" MaxLength='5' Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtGO_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="249" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="250" MaxLength='20'></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_ExchRate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="251" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_ExchCCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="252" Width="25px"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Fund" runat="server" CssClass="textBox" TabIndex="253"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_CheckNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="254" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="255" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Advice_Print" runat="server" CssClass="textBox"
                                                                                    TabIndex="256" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txtGO_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="257" MaxLength="30" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Entity" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="258" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="259" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_InterAmt" runat="server" CssClass="textBox"
                                                                                    TabIndex="260" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGO_Credit_InterRate" runat="server" CssClass="textBox"
                                                                                    TabIndex="261" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                     <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <table width="100%">
                                            <tr>
                                                <td width="10%">
                                                </td>
                                               
                                                    <td align="left" width="90%">
                                                    <asp:Button ID="btnGO_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to IMPORT ACCOUNTING" TabIndex="256" OnClientClick="return ImportAccountingNextClick(4);"
                                        />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnGO_Next" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                        ToolTip="Go to Normal General Operation" TabIndex="256" OnClientClick="return GeneralOperationNextClick(1);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                        <%------------------------------------------------------------GENERAL OPERATION TAB END----------------------------------------------------%>
                                        <%------------------------------------------------------------NORMAL GENERAL OPERATION TAB----------------------------------------------------%>
                                        <ajaxToolkit:TabPanel ID="tbNormalGO" runat="server" HeaderText="GENERAL OPERATION II"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                       <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_Generaloperation2" Text="GENERAL OPERATION II" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_Generaloperation2_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                        <div id="DivNormalGO" style="width: 75%; float: left; height: 100%">
                                            <table  width="100%">
                                                <asp:Panel ID="PanelNormalGO" runat="server" Visible="false">
                                                   
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" >
                                                            <span class="elementLabel" style="margin-left:38px;">VALUE DATE :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtNGO_ValueDate" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                                                TabIndex="1" MaxLength="10" onfocus="this.select()" 
                                                                AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdngovaldt" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtNGO_ValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btncalendar_ngovaldt" runat="server" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <asp:Label ID="Label2" runat="server" CssClass="elementLabel" Style="color: Red; font-weight: bold;"></asp:Label>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtNGO_ValueDate" PopupButtonID="btncalendar_ngovaldt" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdngovaldt"
                                                                ValidationGroup="dtVal" ControlToValidate="txtNGO_ValueDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                        <%--<td align="right">
                                                            <span class="elementLabel">REFERENCE NO : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtNGO_Ref_No" Width="100px" runat="server" TabIndex="2"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>--%>
                                                        <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtNGO_Comment" runat="server" CssClass="textBox" TabIndex="223"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtNGO_Section" runat="server" CssClass="textBox" TabIndex="224"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txtNGO_Remark" runat="server" Width="300px" CssClass="textBox"
                                                                                    MaxLength="30" TabIndex="225" onblur="return ToggleGOAcccChange('Remark');"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="226" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
	                                                   <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_SchemeNo" runat="server" CssClass="textBox" TabIndex="227"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="txtNGO_Debit" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="228" onchange="return TogggleDebitCreditCode('4','1');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                              <asp:Button ID="btn_NGO_Debit_GLCode_help" Enabled="false" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO4','Debit1');" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_NGO_Debit_AccCode_help" Enabled="false" runat="server" ToolTip="Press for AcCode list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Debit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_CCY" runat="server" CssClass="textBox" TabIndex="229"
                                                                                    MaxLength='3' Width="25px" onchange="return txtNGO_Debit_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Amt" runat="server" CssClass="textBox" Width="100px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="230" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                       <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Cust" runat="server" CssClass="textBox" TabIndex="231"
                                                                                    MaxLength='12' Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtNGO_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="231" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            </tr>
                                                                            <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="232" MaxLength='5' Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtNGO_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="232" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="233" MaxLength='20'></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_ExchRate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="234" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return txtNGO_Debit_ExchRate_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_ExchCCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="235" Width="25px" onchange="return txtNGO_Debit_ExchCCY_Change();"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Fund" runat="server" CssClass="textBox" TabIndex="236"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_CheckNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="237" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="238" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Advice_Print" runat="server" CssClass="textBox"
                                                                                    TabIndex="239" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txtNGO_Debit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="240" Width="300px" MaxLength="30"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Entity" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="241" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="242" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_InterAmt" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" Width="100px" Style="text-align: right" runat="server" CssClass="textBox"
                                                                                    TabIndex="243"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Debit_InterRate" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" runat="server" CssClass="textBox" TabIndex="244" Width="100px"
                                                                                    Style="text-align: right"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="txtNGO_Credit" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="245" onchange="return TogggleDebitCreditCode('4','2');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_NGO_Credit_GLCode_Help" Enabled="false" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO4','Debit2');" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_NGO_Credit_AccCode_Help" Enabled="false" runat="server" ToolTip="Press for AcCode list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Credit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_CCY" runat="server" CssClass="textBox" TabIndex="246"
                                                                                    Width="25px" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Amt" runat="server" CssClass="textBox" Enabled="true"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="247" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Cust" runat="server" CssClass="textBox" TabIndex="248"
                                                                                    MaxLength='12' Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtNGO_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="248" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                           </tr>
                                                                            <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="249" MaxLength='5' Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtNGO_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="249" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="250" MaxLength='20'></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_ExchRate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="251" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_ExchCCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="252" Width="25px"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Fund" runat="server" CssClass="textBox" TabIndex="253"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_CheckNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="254" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="255" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Advice_Print" runat="server" CssClass="textBox"
                                                                                    TabIndex="256" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txtNGO_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="257" MaxLength="30" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Entity" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="258" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="259" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_InterAmt" runat="server" CssClass="textBox"
                                                                                    TabIndex="260" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNGO_Credit_InterRate" runat="server" CssClass="textBox"
                                                                                    TabIndex="261" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        
                                       <%-- <div id="DivGO_LC_Commitment" style="width: 50%; float: right; height: 100%">
                                            <table width="100%">
                                                <asp:Panel ID="panalGO_LC_Commitement" runat="server" Visible="false">
                                                    <tr>
                                                        <td colspan="6">
                                                            <span class="elementLabel"><b>GENERAL OPERATION FOR LC COMMITMENT CHRG</b></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">REFERENCE NO : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Ref_No" runat="server" Width="100px" TabIndex="229"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Comment" runat="server" TabIndex="230" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" >
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_SectionNo" runat="server" CssClass="textBox"
                                                                TabIndex="231" Width="20px"></asp:TextBox>

                                                            <span class="elementLabel">Remarks : </span>

                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Remarks" runat="server" TabIndex="232" CssClass="textBox" Width="300px" MaxLength="30" onkeyup="return Toggel_GO_LC_Remark();"></asp:TextBox>
                                                        </td>
                                                        <td align="right" >
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_MEMO" runat="server" CssClass="textBox" TabIndex="233"
                                                                MaxLength="6" Width="50px" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" >
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td align="left" colspan="5">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Scheme_no" runat="server" CssClass="textBox" TabIndex="84"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td align="left" width="20%" >
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="234" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td align="left" width="30%">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="235" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td align="left" width="15%">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Amt" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" onkeyup="return DebitCredit_Amt('LC_Commitement');"
                                                                TabIndex="236" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Cust" runat="server" CssClass="textBox"
                                                                TabIndex="237" Width="90px" MaxLength="12"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="238" Width="90px" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C NO : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                MaxLength="20" TabIndex="239" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="3">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Exch_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="240" Width="25px" MaxLength="3" ></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="241" Width="90px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_FUND" runat="server" CssClass="textBox"
                                                                TabIndex="93" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Check_No" runat="server" CssClass="textBox"
                                                                TabIndex="94"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Advice_Print" runat="server" CssClass="textBox"
                                                                TabIndex="242" Width="20px" MaxLength="1" ></asp:TextBox>

                                                            <span class="elementLabel">DETAILS : </span>

                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Details" runat="server" TabIndex="243" MaxLength="30" Width="300px"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Entity" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="244" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Division" runat="server" CssClass="textBox"
                                                                TabIndex="99" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="100"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="101" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="245" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="246" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Amt" runat="server" CssClass="textBox"
                                                                Enabled="false" onkeydown="return validate_Number(event);" TabIndex="247" Width="90px"
                                                                Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Cust" runat="server" CssClass="textBox" MaxLength="12"
                                                                TabIndex="248" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Cust_AcCode" runat="server" CssClass="textBox"  MaxLength="5"
                                                                TabIndex="249" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C NO : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                MaxLength="20" TabIndex="250" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="3">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="251" Width="25px" MaxLength="3" ></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="252" Width="90px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_FUND" runat="server" CssClass="textBox"
                                                                TabIndex="110" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Check_No" runat="server" CssClass="textBox"
                                                                TabIndex="111"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="112" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Advice_Print" runat="server" CssClass="textBox"
                                                                TabIndex="253" Width="20px" MaxLength="1"></asp:TextBox>

                                                            <span class="elementLabel">DETAILS : </span>

                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Details" runat="server" TabIndex="254" MaxLength="30" Width="300px"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Entity" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="255" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Division" runat="server" CssClass="textBox"
                                                                TabIndex="116" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="117"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="118" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>--%>
                                        <table width="100%">
                                            <tr>
                                                <td width="10%">
                                                </td>
                                               <td align="left" width="90%">
                                                    <asp:Button ID="btnNGO_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to General Operation" TabIndex="256" OnClientClick="return GeneralOperationNextClick(0);"
                                        />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnNGO_Next" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                        ToolTip="Go to Normal General Operation" TabIndex="256" OnClientClick="return GeneralOperationNextClick(2);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentGOAccChange" runat="server" HeaderText="INTER-OFFICE"
                                                Font-Bold="true" ForeColor="White" Enabled="false">
                                                <ContentTemplate>
                                                  <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox ID="chk_InterOffice" Text="INTER-OFFICE" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_InterOffice_OnCheckedChanged" AutoPostBack="true" Enabled="false"/>
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="panal_GOAccChange" runat="server" Visible="false">
                                                                        <tr>
                                                                         <%-- ----------------------------------------------------------------------------Anand09-08-2023------------------------------------------%>
                                                                        <td align="right" >
                                                            <span class="elementLabel" style="margin-left:38px;">VALUE DATE :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtIO_ValueDate" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                                                TabIndex="1" MaxLength="10" Enabled="false" onfocus="this.select()"
                                                                AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mdngovaldtIO" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtIO_ValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":" Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btncalendar_ngovaldtIO" Enabled="false" runat="server" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <asp:Label ID="Label4" runat="server" CssClass="elementLabel" Style="color: Red; font-weight: bold;"></asp:Label>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderIO2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtIO_ValueDate" PopupButtonID="btncalendar_ngovaldtIO" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorIO3" runat="server" ControlExtender="mdngovaldtIO"
                                                                ValidationGroup="dtVal" ControlToValidate="txtIO_ValueDate" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                   <%-- ---------------------------------------------------------------------------END-------------------------------------------------------------%>
                                                                            <td align="right">
                                                                                <span class="elementLabel">REFERENCE NO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Ref_No" Width="100px" Enabled="false" runat="server" TabIndex="222"
                                                                                    CssClass="textBox" MaxLength="14"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Comment" Enabled="false" runat="server" CssClass="textBox" TabIndex="223"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_SectionNo" Enabled="false" runat="server" CssClass="textBox" TabIndex="224"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GOAccChange_Remarks" Enabled="false" runat="server" Width="300px" CssClass="textBox"
                                                                                    MaxLength="30" TabIndex="225" onblur="return ToggleGOAcccChange('Remark');"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Memo" Enabled="false" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="226" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Scheme_no" Enabled="false" runat="server" CssClass="textBox" TabIndex="227"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="txt_GOAccChange_Debit_Code" Enabled="false" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="228" onchange="return TogggleDebitCreditCode('4','1');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                              <asp:Button ID="btn_GOAccChange_Debit_GLCode_help" Enabled="false" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" Visible="false" OnClientClick="return OpenGO_help('mouseClick','GO4','Debit1');" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GOAccChange_Debit_AccCode_help" Enabled="false" runat="server" ToolTip="Press for AcCode list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Debit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Curr" runat="server" Enabled="false" CssClass="textBox" TabIndex="229"
                                                                                    MaxLength='3' Width="25px" onchange="return txt_GOAccChange_Debit_Curr_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Amt" runat="server" Enabled="false" CssClass="textBox" Width="100px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="230" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return GOBranch_Amt_Calculation();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust" Enabled="false" runat="server" CssClass="textBox" TabIndex="231"
                                                                                    MaxLength='12' Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_Name" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="231" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_AcCode" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="232" MaxLength='5' Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_AcCode_Name" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="232" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_AccNo" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="233" MaxLength='20'></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Exch_Rate" Enabled="false" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="234" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return txt_GOAccChange_Debit_Exch_Rate_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Exch_CCY" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="235" Width="25px" onchange="return txt_GOAccChange_Debit_Exch_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_FUND" Enabled="false" runat="server" CssClass="textBox" TabIndex="236"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Check_No" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="237" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Available" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="238" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_AdPrint" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="239" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Details" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="240" Width="300px" MaxLength="30"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Entity" Enabled="false" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="241" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Division" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="242" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Inter_Amount" Enabled="false" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" Width="100px" Style="text-align: right" runat="server" CssClass="textBox"
                                                                                    TabIndex="243"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Inter_Rate" Enabled="false" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" runat="server" CssClass="textBox" TabIndex="244" Width="100px"
                                                                                    Style="text-align: right"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="txt_GOAccChange_Credit_Code" Enabled="false" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="245" onchange="return TogggleDebitCreditCode('4','2');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GOAccChange_Credit_GLCode_Help" Enabled="false" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" Visible="false" OnClientClick="return OpenGO_help('mouseClick','GO4','Debit2');" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GOAccChange_Credit_AccCode_Help" Enabled="false" runat="server" ToolTip="Press for AcCode list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Credit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Curr" Enabled="false" runat="server" CssClass="textBox" TabIndex="246"
                                                                                    Width="25px" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Amt" Enabled="false" runat="server" CssClass="textBox" 
                                                                                    onkeydown="return validate_Number(event);" TabIndex="247" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust" Enabled="false" runat="server" CssClass="textBox" TabIndex="248"
                                                                                    MaxLength='12' Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="248" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_AcCode" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="249" MaxLength='5' Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_AcCode_Name" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="249" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_AccNo" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="250" MaxLength='20'></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Exch_Rate" Enabled="false" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="251" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Exch_Curr" Enabled="false" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="252" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_FUND" Enabled="false" runat="server" CssClass="textBox" TabIndex="253"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Check_No" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="254" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Available" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="255" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_AdPrint" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="256" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Details" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="257" MaxLength="30" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Entity" Enabled="false" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="258" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Division" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="259" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Inter_Amount" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="260" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Inter_Rate" Enabled="false" runat="server" CssClass="textBox"
                                                                                    TabIndex="261" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                             <td align="left" width="90%">
                                                    <asp:Button ID="btnIO_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to General Operation" TabIndex="256" OnClientClick="return GeneralOperationNextClick(1);" />
                                                    
                                                </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                   </ajaxToolkit:TabContainer>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                        <%------------------------------------------------------------NORMAL GENERAL OPERATION TAB END----------------------------------------------------%>    
                                        <%---------------------------------------------------------- Supriya End ----------------------------------------%>
                                    </ajaxToolkit:TabContainer>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                        <tr style="height: 15px">
                                                    
                                                    <td align="center" colspan="3" width="90%">
                                                    <span class="elementLabel">Approve / Reject :</span>
                                                        <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="35">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="4">
                                                        <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td align="center" nowrap>
                                    <asp:Button ID="btnLEI" runat="server" Text="Verify LEI" CssClass="buttonDefault"
                                        ToolTip="Verify LEI" TabIndex="132" OnClick="btnLEI_Click" Visible="true" />
                                      &nbsp;
                                    <asp:Button ID="btnSavePrint" runat="server" Text="View" CssClass="buttonDefault"
                                    ToolTip="Save & Print Export Bill Document Realisation Advice" OnClick="btnView_Click"
                                    TabIndex="133" OnClientClick="return CheckTT();" CommandName="2" CommandArgument="print" />
                                    &nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="37" CssClass="buttonDefault"
                                    ToolTip="Save" OnClick="btnSave_Click" OnClientClick="return CheckTT();" Style="visibility: hidden"/>
                                    <%-------------------------------------- Nilesh --------------------------------------%>
                              
                                
                                    <%------------------------------------END------------------------------------%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="38" CssClass="buttonDefault"
                                        ToolTip="Cancel" OnClick="btnCancel_Click" Style="visibility: hidden"/>
                                    <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                        Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnttamtCheck" runat="server" ClientIDMode="Static" Style="visibility: hidden"
                                        Text="Button" UseSubmitBehavior="False" OnClick="btnttamtCheck_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>
        </div>
    </form>
    <%--<script language="javascript" type="text/javascript">
        window.onload = function () {

            CalculateTotalPC();
        }
    </script>--%>
</body>
</html>
