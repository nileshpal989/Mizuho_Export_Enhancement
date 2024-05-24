<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditFullRealisation.aspx.cs"
    Inherits="EXP_EXP_AddEditFullRealisation" %>

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
    <script language="javascript" src="../Scripts/rightClick.js" type="text/javascript"></script>
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
            var ForeignORLocal = document.getElementById('txtForeignORLocal').value;

            popup = window.open('ExportDocHelp.aspx?DocPrFx=' + type + '&year=' + year + '&BranchCode=' + branchcode + '&docno=' + docno + '&ForeignORLocal=' + ForeignORLocal, 'HelpDocId', 'height=380,  width=620,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
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

            if (isNaN(comm))
            { comm = 0; }

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
            calTax();
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

            if (isNaN(comm))
            { comm = 0; }

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
            calTax();
            return true;
            //calTax();
        }

        function calTax() {

            var hdnServiceTax;
            var hdnEDU_CESS;
            var hdnSEC_EDU_CESS;
            if (document.getElementById('chkStax').checked == true) {

                hdnServiceTax = document.getElementById('hdnServiceTax').value;
                hdnEDU_CESS = document.getElementById('hdnEDU_CESS').value;
                hdnSEC_EDU_CESS = document.getElementById('hdnSEC_EDU_CESS').value;
            }
            else {

                hdnServiceTax = 0;
                hdnEDU_CESS = 0;
                hdnSEC_EDU_CESS = 0;
            }

            var _serviceTax = 0;
            var _serviceTax_EDUCess = 0;
            var _serviceTax_SecEDUCess = 0;

            var inramt23 = document.getElementById('txtprofitamt');
            if (inramt23.value == '')
                inramt23.value = 0;
            inramt23.value = parseFloat(inramt23.value).toFixed(2);


            var inramt24 = document.getElementById('txtCommission');
            if (inramt24.value == '')
                inramt24.value = 0;
            inramt24.value = parseFloat(inramt24.value).toFixed(2);


            var inramt25 = document.getElementById('txtCourier');
            if (inramt25.value == '')
                inramt25.value = 0;
            inramt25.value = parseFloat(inramt25.value).toFixed(2);


            var inramt26 = document.getElementById('txtSwift');
            if (inramt26.value == '')
                inramt26.value = 0;
            inramt26.value = parseFloat(inramt26.value).toFixed(2);


            var inramt27 = document.getElementById('txtBankCertificate');
            if (inramt27.value == '')
                inramt27.value = 0;
            inramt27.value = parseFloat(inramt27.value).toFixed(2);

            var inramt28 = document.getElementById('txtFxDlsCommission');
            if (inramt28.value == '')
                inramt28.value = 0;
            inramt28.value = parseFloat(inramt28.value).toFixed(2);

            var inramt29 = document.getElementById('txtsbfx');
            if (inramt29.value == '')
                inramt29.value = 0;
            inramt29.value = parseFloat(inramt29.value).toFixed(2);

            var inramt30 = document.getElementById('txt_kkcessonfx');
            if (inramt30.value == '')
                inramt30.value = 0;
            inramt30.value = parseFloat(inramt30.value).toFixed(2);

            var inramt31 = document.getElementById('txttotcessfx');
            if (inramt31.value == '')
                inramt31.value = 0;
            inramt31.value = parseFloat(inramt31.value).toFixed(2);

            var inramt32 = document.getElementById('txtPcfcAmt');
            if (inramt32.value == '')
                inramt32.value = 0;
            inramt32.value = parseFloat(inramt32.value).toFixed(2);

            var inramt33 = document.getElementById('txtOverDue');
            if (inramt33.value == '')
                inramt33.value = 0;
            inramt33.value = parseFloat(inramt33.value).toFixed(2);


            var stax;
            var staxvalue;

            //stax = document.getElementById('ddlServicetax');
            //staxvalue = stax.options[stax.selectedIndex].value;

            var staxamt = document.getElementById('txtServiceTax');
            if (staxamt.value == '')
                staxamt.value = 0;
            staxamt.value = parseFloat(staxamt.value).toFixed(2);

            var iamt23 = 0;
            var iamt24 = 0;
            var iamt25 = 0;
            var iamt26 = 0;
            var iamt27 = 0;
            var iamt32 = 0;
            var iamt33 = 0;

            if (inramt23.value != '') {
                iamt23 = parseFloat(inramt23.value).toFixed(2);
            }

            if (inramt24.value != '') {
                iamt24 = parseFloat(inramt24.value).toFixed(2);
            }

            if (inramt25.value != '') {
                iamt25 = parseFloat(inramt25.value).toFixed(2);
            }
            if (inramt26.value != '') {
                iamt26 = parseFloat(inramt26.value).toFixed(2);
            }
            if (inramt27.value != '') {
                iamt27 = parseFloat(inramt27.value).toFixed(2);
            }
            //                if (inramt32.value != '') {
            //                    iamt32 = parseFloat(inramt32.value).toFixed(2);
            //                }
            if (inramt33.value != '') {
                iamt33 = parseFloat(inramt33.value).toFixed(2);
            }

            _serviceTax = (parseFloat(iamt23) + parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27) + parseFloat(iamt33)) * (parseFloat(hdnServiceTax) / 100);
            _serviceTax = Math.round(_serviceTax);

            _serviceTax_EDUCess = _serviceTax * (parseFloat(hdnEDU_CESS) / 100);

            _serviceTax_SecEDUCess = _serviceTax * (parseFloat(hdnSEC_EDU_CESS) / 100);

            staxamt.value = parseFloat(parseFloat(_serviceTax) + parseFloat(_serviceTax_EDUCess) + parseFloat(_serviceTax_SecEDUCess)).toFixed(2);

            //            staxamt.value = parseFloat((parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27)) * (parseFloat(staxvalue) / 100)).toFixed(2);

            var sbcess = 0;
            var kkcess = 0;

            var sbcessPer = document.getElementById('txtsbcess');
            var kkcessPer = document.getElementById('txt_kkcessper');
            var sbcessamt = document.getElementById('txtSBcesssamt');
            var kkcesssmt = document.getElementById('txt_kkcessamt');
            var tottaxamt = document.getElementById('txtsttamt');


            //document.getElementById('txtServiceTax').value = staxamt.value;
            if (sbcessPer.value == '') {
                sbcessPer.value = 0;
            }
            sbcess = (parseFloat(iamt23) + parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27) + parseFloat(iamt33)) * (parseFloat(sbcessPer.value) / 100);
            sbcessamt.value = (sbcess).toFixed(2);

            if (sbcessamt.value == '') {
                sbcessamt.value = 0;
            }


            if (kkcessPer.value == '') {
                kkcessPer.value = 0;
            }


            kkcess = (parseFloat(iamt23) + parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27) + parseFloat(iamt33)) * (parseFloat(kkcessPer.value) / 100);
            kkcesssmt.value = (kkcess).toFixed(2);

            if (kkcesssmt.value == '') {
                kkcesssmt.value = 0;
            }


            tottaxamt.value = parseFloat(parseFloat(staxamt.value) + parseFloat(sbcess) + parseFloat(kkcess)).toFixed(2);

            calNetAmt();
            return true;

        }

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


        function saveTTRefDetails(TTRefNo, TTAmt, hNo, TTTotAmt) {

            switch (hNo) {
                case "1":
                    document.getElementById('txtTTRefNo1').value = TTRefNo;
                    document.getElementById('txtTotTTAmt1').value = TTTotAmt;
                    document.getElementById('txtBalTTAmt1').value = TTAmt;
                    document.getElementById('txtTTAmount1').value = TTAmt;
                    __doPostBack('txtTTRefNo1', '');
                    break;
                case "2":
                    document.getElementById('txtTTRefNo2').value = TTRefNo;
                    document.getElementById('txtTotTTAmt2').value = TTTotAmt;
                    document.getElementById('txtBalTTAmt2').value = TTAmt;
                    document.getElementById('txtTTAmount2').value = TTAmt;
                    __doPostBack('txtTTRefNo2', '');
                    break;
                case "3":
                    document.getElementById('txtTTRefNo3').value = TTRefNo;
                    document.getElementById('txtTotTTAmt3').value = TTTotAmt;
                    document.getElementById('txtBalTTAmt3').value = TTAmt;
                    document.getElementById('txtTTAmount3').value = TTAmt;
                    __doPostBack('txtTTRefNo3', '');
                    break;
                case "4":
                    document.getElementById('txtTTRefNo4').value = TTRefNo;
                    document.getElementById('txtTotTTAmt4').value = TTTotAmt;
                    document.getElementById('txtBalTTAmt4').value = TTAmt;
                    document.getElementById('txtTTAmount4').value = TTAmt;
                    __doPostBack('txtTTRefNo4', '');
                    break;
                case "5":
                    document.getElementById('txtTTRefNo5').value = TTRefNo;
                    document.getElementById('txtTotTTAmt5').value = TTTotAmt;
                    document.getElementById('txtBalTTAmt5').value = TTAmt;
                    document.getElementById('txtTTAmount5').value = TTAmt;
                    __doPostBack('txtTTRefNo5', '');
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
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
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
                                <span class="pageLabel" style="font-weight: bold">Export Realisation Data Entry</span>
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
                                <asp:Label ID="lblLEIEffectDate" runat="server" Text="01/01/2023" Visible="false"></asp:Label>
                                <%-- <asp:Label ID="lblLEI_CUST_Remark" runat="server"></asp:Label>
                                <asp:Label ID="lblLEIExpiry_CUST_Remark" runat="server"></asp:Label>
                                <asp:Label ID="lblLEI_Overseas_Remark" runat="server"></asp:Label>
                                <asp:Label ID="lblLEIExpiry_Overseas_Remark" runat="server"></asp:Label>--%>
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
                                    Text="Bills Bght with L/C at Sight" Style="font-weight: bold;" GroupName="TransType" />
                                <%--</td>
                            <td width="18%" nowrap align="left">--%>
                                <asp:RadioButton ID="rbtbba" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C at Sight"
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" /><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <asp:RadioButton ID="rbtblu" runat="server" CssClass="elementLabel" Text="Bills Bght with L/C Usance"
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" /><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <asp:RadioButton ID="rbtbbu" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C Usance"
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" /><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <asp:RadioButton ID="rbtbca" runat="server" CssClass="elementLabel" Text="Bills For Coll. at Sight "
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" /><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <asp:RadioButton ID="rbtbcu" runat="server" CssClass="elementLabel" Text="Bills For Coll. Usance"
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" /><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Local Bills Dis."
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" /><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <asp:RadioButton ID="rbtLBC" runat="server" CssClass="elementLabel" Text="Local Bills Coll."
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" />
                                <asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                    GroupName="TransType" Enabled="false" Style="font-weight: bold;" /><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                            </td>
                            <%--<td>
                                &nbsp;
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" width="10%" nowrap class="style1">
                                <span class="elementLabel">Document No. :</span>
                            </td>
                            <td align="left" nowrap class="style1">
                                <asp:TextBox ID="txtDocNo" runat="server" CssClass="textBox" Height="14px" Width="150px"
                                    TabIndex="1" Style="font-weight: bold;" ToolTip="Press F2 for Help" AutoPostBack="true"
                                    OnTextChanged="txtDocNo_TextChanged" Enabled="false"></asp:TextBox>
                                <asp:Button ID="btnDocNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            </td>
                            <td align="left" width="10%" class="style1">
                                <asp:CheckBox ID="chkBank" runat="server" Enabled="false" Style="vertical-align: top;" />
                                <span class="elementLabel" style="vertical-align: top; font-weight: bold; color: Red;">
                                    Bank Line Transfer</span>
                            </td>
                            <td align="right" nowrap width="4%" class="style1">
                                <span class="elementLabel">Date Received :</span>
                            </td>
                            <td align="left" nowrap class="style1">
                                <asp:TextBox ID="txtDateReceived" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                    Enabled="false"></asp:TextBox>
                                <span id="SpanLei5" runat="server" class="mandatoryField" visible="false" style="font-size: medium">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Note :</span>
                                <asp:Label ID="ReccuringLEI" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Sr No :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtSrNo" runat="server" Enabled="false" Width="35px" Height="14px"
                                    CssClass="textBox"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Foreign OR Local :</span>
                                <asp:TextBox ID="txtForeignORLocal" runat="server" Enabled="false" Width="35px" CssClass="textBox"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;<span id="SpanLei1" runat="server" class="elementLabel" visible="false">Customer
                                    LEI :</span>
                                <asp:Label ID="lblLEI_CUST_Remark" runat="server" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Customer A/C No :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtCustAcNo" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Enabled="false" TabIndex="-1"></asp:TextBox>
                                <asp:Label ID="lblCustDesc" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Due Date :</span>
                                <asp:TextBox ID="txtDueDate" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                    Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Date Negotiated :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtDateNegotiated" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                    Enabled="false"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <span id="SpanLei2" runat="server" class="elementLabel" visible="false">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Customer LEI Expiry :</span>
                                <asp:Label ID="lblLEIExpiry_CUST_Remark" runat="server" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Overseas Party ID :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtOverseasParty" runat="server" CssClass="textBox" Height="14px"
                                    Width="70px" Enabled="false" TabIndex="-1"></asp:TextBox>
                                <asp:Label ID="lblOverseasParty" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Date Delink :</span>
                                <asp:TextBox ID="txtDateDelinked" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                    Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Accepted Due Date :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtAcceptedDueDate" runat="server" Width="70px" Height="14px" CssClass="textBox"
                                    Enabled="false"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="SpanLei3" runat="server"
                                    class="elementLabel" visible="false">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Applicant LEI :</span>
                                <asp:Label ID="lblLEI_Overseas_Remark" runat="server" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Overseas Bank ID :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtOverseasBank" MaxLength="7" AutoPostBack="true" runat="server"
                                    CssClass="textBox" Height="14px" Width="70px" TabIndex="-1" OnTextChanged="txtOverseasBank_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnOverseasBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="lblOverseasBank" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                            <%--<td align="right" nowrap>
                                <span class="elementLabel">Bill Type :</span>
                                <asp:TextBox ID="txtBillType" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                    Enabled="false"></asp:TextBox>
                            </td>--%>
                            <td align="right">
                                <span class="elementLabel">Swift Code :</span>
                                <asp:TextBox ID="txtSwiftCode" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                    Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Other Currency :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtOtherCurrency" runat="server" CssClass="textBox" Height="14px"
                                    Width="35px" Enabled="false"></asp:TextBox>
                                &nbsp;<span id="SpanLei4" runat="server" class="elementLabel" visible="false">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Applicant LEI Expiry :</span>
                                <asp:Label ID="lblLEIExpiry_Overseas_Remark" runat="server" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Invoice No :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Bill Amt. :</span>
                                <asp:TextBox ID="txtBillAmt" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                    Enabled="false" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Bill Amt. in र :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtBillAmtinINR" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Enabled="false" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Negotiated Amt. :</span>
                                <asp:TextBox ID="txtNegotiatedAmt" runat="server" CssClass="textBox" Height="14px"
                                    Width="70px" Enabled="false" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Negotiated Amt. in र :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtNegotiatedAmtinINR" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Enabled="false" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">FIRC :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:CheckBox ID="chkFirc" Text="No" runat="server" TabIndex="-1" CssClass="elementLabel"
                                    AutoPostBack="true" OnCheckedChanged="chkFirc_CheckedChanged" />
                                <%--<span class="elementLabel">Yes/No</span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">FIRC No :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtFircNo" MaxLength="50" Enabled="false" runat="server" CssClass="textBox"
                                    Height="14px" Width="150px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap>
                                <span class="elementLabel">FIRC AD Code :</span>
                                <%--</td>
                                 <td align="left" nowrap>--%>
                                <asp:TextBox ID="txtFircAdCode" MaxLength="7" Enabled="false" runat="server" CssClass="textBox"
                                    Height="14px" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Date Realised :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtDateRealised" runat="server" CssClass="textBox" Height="14px"
                                    Width="70px" TabIndex="1" MaxLength="10" onfocus="this.select()"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdRemdate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDateRealised" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDateRealised" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="Mev1" runat="server" ControlExtender="mdRemdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDateRealised" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Value Date :</span>
                                <asp:TextBox ID="txtValueDate" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                    TabIndex="2" MaxLength="10" onfocus="this.select()" OnTextChanged="txtValueDate_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td colspan="2" align="left" nowrap>
                                <ajaxToolkit:MaskedEditExtender ID="mdValueDate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_ValueDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <asp:Label ID="lblRefund" runat="server" CssClass="elementLabel" Style="color: Red;
                                    font-weight: bold;"></asp:Label>
                                <ajaxToolkit:CalendarExtender ID="calenderValueDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtValueDate" PopupButtonID="btncalendar_ValueDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdValueDate"
                                    ValidationGroup="dtVal" ControlToValidate="txtValueDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <%--<td align="right" nowrap>
                                <span class="elementLabel">N.Y. Ref No. :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtNYRefNo" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                    TabIndex="4" MaxLength="6" onfocus="this.select()"></asp:TextBox>
                            </td>--%>
                            <td align="right" nowrap>
                                <span class="elementLabel">Currency :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" Height="14px" Width="35px"
                                    Enabled="false" AutoPostBack="true" OnTextChanged="txtCurrency_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Realised Currency :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txt_relcur" TabIndex="3" runat="server" CssClass="textBox" Height="14px"
                                    Width="35px" Enabled="false" AutoPostBack="true" OnTextChanged="txt_relcur_TextChanged"></asp:TextBox>
                                <asp:Button ID="btn_recurrhelp" runat="server" CssClass="btnHelp_enabled" TabIndex="4" />
                            </td>
                            <td align="left">
                                <span class="elementLabel">Amount :</span>&nbsp;
                                <asp:TextBox runat="server" onfocus="this.select()" TabIndex="4" ID="txt_relamount"
                                    Style="text-align: right;" CssClass="textBox" />
                            </td>
                            <td align="left" colspan="2">
                                <span class="elementLabel">Cross Curr. Rate :</span>
                                <asp:TextBox ID="txtRelCrossCurRate" onfocus="this.select()" runat="server" CssClass="textBox"
                                    TabIndex="5" Width="70px" Height="14px" Style="text-align: right;"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Exchange Rate :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" TabIndex="6" Style="text-align: right" onfocus="this.select()"></asp:TextBox>
                            </td>
                            <%-- <td align="right" colspan="2" nowrap>
                                <span class="elementLabel">Interest Rate :</span>
                                <asp:TextBox ID="txtInterestRate1" runat="server" CssClass="textBox" Height="14px"
                                    Width="35px" TabIndex="4" onfocus="this.select()"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                <span class="elementLabel">For</span>&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtNoofDays1" runat="server" CssClass="textBox" Height="14px" Width="35px"
                                    Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp; <span class="elementLabel">Days</span>
                            </td>--%>
                            <td align="left" colspan="2" nowrap>
                                <span class="elementLabel">Interest Rate :</span>
                                <asp:TextBox ID="txtInterestRate2" runat="server" CssClass="textBox" Height="14px"
                                    Width="35px" TabIndex="7" onfocus="this.select()"></asp:TextBox>&nbsp; <span class="elementLabel">
                                        For</span>&nbsp;
                                <asp:TextBox ID="txtNoofDays2" runat="server" TabIndex="8" CssClass="textBox" Height="14px"
                                    Width="35px"></asp:TextBox>&nbsp; <span class="elementLabel">Days</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:CheckBox ID="chkLoanAdvanced" runat="server" Enabled="false" Style="vertical-align: top;" />
                                <%--<span class="elementLabel">Loan Advanced</span>--%>
                                <asp:Label ID="lblLoan" runat="server" Style="color: Red; font-weight: bold; vertical-align: top;"
                                    Text="Loan Advanced" CssClass="elementLabel"></asp:Label>
                        </tr>
                        <%--<tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Forward Contract No :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" TabIndex="6" MaxLength="12" onfocus="this.select()"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Amount Realised :</span>
                                <%--<table border="1" align="left" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Amount Realised :</span>
                                        </td>
                                        <td align="left" width="10%">
                                            <asp:TextBox ID="txtAmtRealised" runat="server" CssClass="textBox" Height="14px"
                                                Width="100px" TabIndex="5" style="text-align:right" onfocus="this.select()"></asp:TextBox>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Amount Realised in INR :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:TextBox ID="txtAmtRealisedinINR" runat="server" CssClass="textBox" Height="14px"
                                                Width="100px" TabIndex="5" style="text-align:right" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>--%>
                            </td>
                            <td align="left" colspan="2" nowrap>
                                <asp:TextBox ID="txtAmtRealised" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" TabIndex="9" Style="text-align: right" onfocus="this.select()"
                                    OnTextChanged="txtAmtRealised_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:RadioButton ID="rdbFull" runat="server" CssClass="elementLabel" Text="Full Payment"
                                    GroupName="Payment" TabIndex="10" AutoPostBack="true" Style="vertical-align: top;
                                    color: Red; font-weight: bold;" OnCheckedChanged="rdbFull_CheckedChanged" />&nbsp;
                                <asp:RadioButton ID="rdbPart" runat="server" CssClass="elementLabel" Text="Part Payment"
                                    GroupName="Payment" TabIndex="11" AutoPostBack="true" Style="vertical-align: top;
                                    color: Red; font-weight: bold;" OnCheckedChanged="rdbPart_CheckedChanged" />
                                &nbsp;<span class="elementLabel">LEI MINT Rate :</span>
                                <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Amount Realised in र :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtAmtRealisedinINR" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right"></asp:TextBox>&nbsp;&nbsp;&nbsp;<span class="elementLabel"
                                        style="color: Red; font-weight: bold">Manual GR :</span>
                                <asp:CheckBox ID="chkManualGR" runat="server" CssClass="elementLabel" TabIndex="102"
                                    onclick="ChangeManualGRText();" />
                                <asp:Label ID="lblManualGR" runat="server" CssClass="elementLabel" Style="color: Red;"></asp:Label>
                            </td>
                            <%--<td align="left" nowrap>
                                <table border="1" cellspacing="0" width="60%">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <span class="elementLabel">P.C. Liquidated</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%" align="center">
                                            <span class="elementLabel">A/c No.</span>
                                        </td>
                                        <td width="50%" align="center">
                                            <span class="elementLabel">Amount</span>
                                        </td>
                                    </tr>
                                </table>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left" colspan="4">
                                <asp:RadioButtonList ID="rdbTransType" runat="server" TabIndex="12" CssClass="elementLabel"
                                    RepeatDirection="Horizontal" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="rdbTransType_SelectedIndexChanged">
                                    <asp:ListItem Text="Full F.Contract" Value="FC"></asp:ListItem>
                                    <%--<asp:ListItem Text="EEFC" Value="eefc"></asp:ListItem>--%>
                                    <asp:ListItem Text="Full INR" Value="INR"></asp:ListItem>
                                    <asp:ListItem Text="Full Cross Currency" Value="CC"></asp:ListItem>
                                    <asp:ListItem Text="Part Conversion" Value="PC"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Forward Contract No :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtForwardContract" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" TabIndex="13" MaxLength="12" onfocus="this.select()"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="rdbTransType2" runat="server" TabIndex="14" CssClass="elementLabel"
                                    RepeatDirection="Horizontal" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="rdbTransType2_SelectedIndexChanged">
                                    <asp:ListItem Text="Part EEFC" Value="PEEFC"></asp:ListItem>
                                    <asp:ListItem Text="Full EEFC" Value="FEEFC"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">EEFC A/c Amt :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPartConAmt" onfocus="this.select()" runat="server" CssClass="textBox"
                                    Width="100px" TabIndex="15" Style="text-align: right;" Height="14px"></asp:TextBox>
                            </td>
                            <td colspan="3" align="left">
                                <span class="elementLabel">Cross Curr :</span>
                                <asp:TextBox runat="server" Enabled="false" MaxLength="3" ID="txtConCrossCur" onfocus="this.select()"
                                    Width="47px" TabIndex="16" CssClass="textBox" Height="14px" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Button ID="btnOtrCrossCur" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <span class="elementLabel">Cross Curr. Rate :</span>
                                <asp:TextBox ID="txtConCurRate" onfocus="this.select()" runat="server" CssClass="textBox"
                                    TabIndex="17" Width="70px" Height="14px" Style="text-align: right;"> </asp:TextBox>&nbsp;&nbsp;
                                <span class="elementLabel">Total Amt. :</span>
                                <asp:TextBox ID="txtTotConRate" runat="server" CssClass="textBox" Width="100px" Height="14px"
                                    Enabled="false" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Non EEFC A/c Amt :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEEFCAmt" onfocus="this.select()" runat="server" CssClass="textBox"
                                    Width="100px" TabIndex="18" Style="text-align: right;" Height="14px"></asp:TextBox>
                            </td>
                            <td colspan="3" align="left">
                                <span class="elementLabel">Cross Curr :</span>
                                <asp:TextBox runat="server" Enabled="false" MaxLength="3" ID="txtEEFCCurrency" onfocus="this.select()"
                                    Width="47px" TabIndex="19" CssClass="textBox" Height="14px" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Button ID="btnEEFCCurrency" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <span class="elementLabel">Cross Curr. Rate :</span>
                                <asp:TextBox ID="txtCrossCurrRate" onfocus="this.select()" runat="server" CssClass="textBox"
                                    TabIndex="20" Width="70px" Height="14px" Style="text-align: right;"> </asp:TextBox>&nbsp;&nbsp;
                                <span class="elementLabel">Total Amt. :</span>
                                <asp:TextBox ID="txtEEFCAmtTotal" runat="server" CssClass="textBox" Width="100px"
                                    Height="14px" Enabled="false" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Balance Amount :</span>
                            </td>
                            <td align="left" width="10%" nowrap>
                                <asp:TextBox ID="txtBalAmt" runat="server" CssClass="textBox" Width="100px" Height="14px"
                                    Style="text-align: right;" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2" nowrap>
                                <span class="elementLabel">Balance Amount in र :</span>
                                <asp:TextBox ID="txtBalAmtinINR" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
                            </td>
                            <%--<td rowspan="13" nowrap>
                                <table border="1" cellpadding="2">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <span class="elementLabel">PC Liquidation </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <span class="elementLabel">PC Liqui Amt </span>
                                        </td>
                                        <td align="center">
                                            <span class="elementLabel">A/c No. </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPCAmount1" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="25" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPCAcNo1" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="26" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:TextBox ID="txtPCsubAcNo1" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="40px" TabIndex="27" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:Button ID="btnPcAcNo1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPCAmount2" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="28" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPCAcNo2" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="29" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:TextBox ID="txtPCsubAcNo2" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="40px" TabIndex="30" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:Button ID="btnPcAcNo2" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPCAmount3" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="31" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPCAcNo3" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="32" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:TextBox ID="txtPCsubAcNo3" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="40px" TabIndex="33" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:Button ID="btnPcAcNo3" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPCAmount4" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="34" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPCAcNo4" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="35" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:TextBox ID="txtPCsubAcNo4" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="40px" TabIndex="36" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:Button ID="btnPcAcNo4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPCAmount5" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="37" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPCAcNo5" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="38" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:TextBox ID="txtPCsubAcNo5" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="40px" TabIndex="39" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:Button ID="btnPcAcNo5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPCAmount6" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="40" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPCAcNo6" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="41" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:TextBox ID="txtPCsubAcNo6" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="40px" TabIndex="42" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                            <asp:Button ID="btnPcAcNo6" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Total PC Liqui :</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTotalPCLiquidated" runat="server" CssClass="textBox" Style="text-align: right"
                                                Width="80px" TabIndex="-1" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>--%>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Collection Amount :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtCollectionAmt" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right; font-weight: bold;" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2" nowrap>
                                <span class="elementLabel">Collection Amount in र :</span>
                                <asp:TextBox ID="txtCollectionAmtinINR" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Interest Amount :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtInterest" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Style="text-align: right;" TabIndex="21"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2" nowrap>
                                <span class="elementLabel">Interest Amount in र :</span>
                                <asp:TextBox ID="txtInterestinINR" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Other Bank Charges :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtOtherBank" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Style="text-align: right" TabIndex="22" onfocus="this.select()"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2" nowrap>
                                <span class="elementLabel">Other Bank Charges in र :</span>
                                <asp:TextBox ID="txtOtherBankinINR" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">FBK Charges :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_fbkcharges" runat="server" CssClass="textBox" Style="text-align: right"
                                    Width="100px" TabIndex="22" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                            </td>
                            <td colspan="2" align="right">
                                <span class="elementLabel">FBK Charges in र :</span>
                                <asp:TextBox ID="txt_fbkchargesinRS" runat="server" Enabled="false" CssClass="textBox"
                                    Style="text-align: right" Width="100px" onfocus="this.select()" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">STax Applicable</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:CheckBox ID="chkStax" runat="server" TabIndex="23" CssClass="elementLabel" AutoPostBack="true"
                                    OnCheckedChanged="chkStax_CheckedChanged" />
                                <span class="elementLabel">Yes/No</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">STax (%) : </span>
                            </td>
                            <td align="left" nowrap>
                                <asp:DropDownList ID="ddlServicetax" runat="server" CssClass="textBox" TabIndex="24">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtServiceTax" runat="server" CssClass="textBox" Width="100px" Height="14px"
                                    Style="text-align: right; font-weight: bold;" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="LEFT" colspan="3">
                                <span class="elementLabel">SBCess (%) :</span>
                                <asp:TextBox ID="txtsbcess" runat="server" CssClass="textBox" Enabled="false" Width="40px"
                                    Style="text-align: right; font-weight: bold"></asp:TextBox>
                                <%--<span class=elementLabel>SBCess Amt :</span>--%>
                                <asp:TextBox ID="txtSBcesssamt" runat="server" CssClass="textBox" TabIndex="24" Width="80px"
                                    Style="text-align: right"></asp:TextBox>
                                <%--</td>
                                      
                                                  <td align=LEFT colspan=2>--%>
                                <span class="elementLabel">KKCess (%) :</span>
                                <asp:TextBox ID="txt_kkcessper" runat="server" CssClass="textBox" Enabled="false"
                                    Width="40px" Style="text-align: right; font-weight: bold"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txt_kkcessamt" runat="server" CssClass="textBox" TabIndex="25" Width="80px"
                                    Style="text-align: right"></asp:TextBox>
                                <%-- </td>


                                         <td align="LEFT" nowrap>--%>
                                <span class="elementLabel">STax Total Amt :</span>
                                <%--</td>
                                                <td align="left">--%>
                                <asp:TextBox ID="txtsttamt" runat="server" CssClass="textBox" TabIndex="26" Width="100px"
                                    Style="text-align: right; font-weight: bold;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Swift Charges :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtSwift" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Style="text-align: right" TabIndex="27" onfocus="this.select()"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">PCFC Amount :</span>
                                <asp:TextBox ID="txtPcfcAmt" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Style="text-align: right" TabIndex="28" onfocus="this.select()"></asp:TextBox>
                            </td>
                            <%--<td align="right" colspan="2" nowrap>
                                <span class="elementLabel">TT Ref No :</span>
                                <asp:TextBox ID="txtTTRefNo" runat="server" CssClass="textBox" Height="14px" Width="150px"
                                    TabIndex="17" onfocus="this.select()"></asp:TextBox>
                                <asp:Button ID="btnTTNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            </td>--%>
                            <%--<td align="left" nowrap>
                               
                            </td>--%>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Courier Charges :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtCourier" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Style="text-align: right" TabIndex="28" onfocus="this.select()"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Over Due Charges :</span>
                                <asp:TextBox ID="txtOverDue" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Style="text-align: right" TabIndex="28" onfocus="this.select()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">STax on FxDls : </span>
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkFxDls" CssClass="elementLabel" runat="server" TabIndex="29"
                                    Height="10px" Style="vertical-align: top;" AutoPostBack="true" onfocus="this.select()"
                                    OnCheckedChanged="chkFxDls_CheckedChanged" />
                                <span class="elementLabel">Yes/No</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">STax on FxDls : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFxDlsCommission" TabIndex="29" runat="server" CssClass="textBox"
                                    Width="100px" Height="14px" Style="text-align: right;"></asp:TextBox>
                            </td>
                            <%-- </tr>--%>
                            <%--<tr>--%>
                            <td align="left" nowrap>
                                <span class="elementLabel">SBCess on FxDls :</span>
                                <asp:TextBox ID="txtsbfx" runat="server" CssClass="textBox" Width="80px" Style="text-align: right"
                                    TabIndex="29"></asp:TextBox>
                            </td>
                            <td nowrap colspan="2" align="left">
                                <span class="elementLabel">KKCess on FxDls :</span>
                                <asp:TextBox ID="txt_kkcessonfx" runat="server" CssClass="textBox" Width="80px" Style="text-align: right"
                                    TabIndex="29"></asp:TextBox>
                                <span class="elementLabel">Total Sevice Tax On FxDls :</span>
                                <asp:TextBox ID="txttotcessfx" runat="server" CssClass="textBox" Width="100px" Style="text-align: right;
                                    font-weight: bold;" TabIndex="29"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Profit Lieu Applicable</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:CheckBox ID="chkProfitLio" Text="No" runat="server" TabIndex="-1" CssClass="elementLabel"
                                    AutoPostBack="true" OnCheckedChanged="chkProfitLio_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Profit Lieu (%) : </span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtprofitper" Enabled="false" runat="server" CssClass="textBox"
                                    Width="20px" TabIndex="31" onfocus="this.select()" MaxLength="2"></asp:TextBox>
                                <asp:Button ID="btnprofitlist" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="return OpenProfitLieuList();" />
                                <asp:TextBox ID="txtprofitamt" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                    Style="text-align: right; font-weight: bold" TabIndex="31"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Bank Cert/Comm. Lieu :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtBankCertificate" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right" TabIndex="32" onfocus="this.select()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Commission :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtCommissionID" Enabled="false" runat="server" CssClass="textBox"
                                    Width="20px" TabIndex="33" onfocus="this.select()" MaxLength="2"></asp:TextBox>
                                <asp:Button ID="btnCommissionList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="return OpenCommissionList();" />
                                <asp:TextBox ID="txtCommission" runat="server" CssClass="textBox" Height="14px" Width="70px"
                                    Style="text-align: right; font-weight: bold" TabIndex="34"></asp:TextBox>
                            </td>
                            <td align="right" nowrap colspan="2">
                                <span class="elementLabel">Receiving Bank :</span>
                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="textBox" TabIndex="35">
                                    <asp:ListItem Text="" Value="BNSNY"></asp:ListItem>
                                    <asp:ListItem Text="" Value="Citi"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <%--<td align="right" nowrap>
                                <span class="elementLabel">STax on FxDls
                                    <asp:CheckBox ID="chkFxDls" runat="server" TabIndex="39" Height="14px" AutoPostBack="true" />
                                    Yes/No</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtFxDlsCommission" runat="server" CssClass="textBox" Width="100px"
                                    Height="14px" Enabled="false" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                            </td>--%>
                        </tr>
                        <tr>
                            <%--<td align="right" nowrap>
                                <span class="elementLabel">STax (%)
                                    <asp:DropDownList ID="ddlServicetax" runat="server" TabIndex="40">
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtServiceTax" runat="server" CssClass="textBox" Width="100px" Height="14px"
                                    Enabled="false" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                            </td>--%>
                        </tr>
                        <tr>
                            <%--<td align="right" nowrap>
                                <span class="elementLabel">Current A/c in F.C. :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtCurrentAcFC" runat="server" CssClass="textBox" Width="100px"
                                    Height="14px" Enabled="false" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                            </td>--%>
                            <td align="right" nowrap>
                                <span class="elementLabel">EEFC in र :</span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtEEFCinINR" runat="server" CssClass="textBox" Width="100px" Height="14px"
                                    Enabled="false" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2" nowrap>
                                <span class="elementLabel">Net Amount in र :</span>
                                <asp:TextBox ID="txtNetAmt" runat="server" CssClass="textBox" Height="14px" Width="100px"
                                    Style="text-align: right" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right" colspan="2" nowrap>
                                <span class="elementLabel">Current A/C Amount in र :</span>
                                <asp:TextBox ID="txtCurrentAcAmt" runat="server" CssClass="textBox" Height="14px"
                                    Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Remarks :</span>
                            </td>
                            <td align="left" colspan="3" nowrap>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="textBox" Width="504px" Height="14px"
                                    onfocus="this.select()" TabIndex="36" MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Button runat="server" Text="TT Reference No." CssClass="buttonDefault" Height="20px"
                                    TabIndex="54" Width="150px" ID="btnTTRefNoList"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="4" align="left">
                                <table width="50%">
                                    <tr>
                                        <td>
                                            <br />
                                            <ajaxToolkit:CollapsiblePanelExtender ID="cpe2" runat="Server" TargetControlID="panelSecondAdd"
                                                CollapsedSize="0" ExpandedSize="200" Collapsed="True" ExpandControlID="btnTTRefNoList"
                                                CollapseControlID="btnTTRefNoList" AutoCollapse="false" AutoExpand="false" ScrollContents="true"
                                                ExpandDirection="Vertical" />
                                            <asp:Panel ID="panelSecondAdd" runat="server">
                                                <table cellspacing="1" border="1" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <span class="elementLabel">ITT Reference No</span>
                                                                    </td>
                                                                    <td>
                                                                        <span class="elementLabel">Total ITT Amt </span>
                                                                    </td>
                                                                    <td>
                                                                        <span class="elementLabel">Balance ITT Amt </span>
                                                                    </td>
                                                                    <td>
                                                                        <span class="elementLabel">ITT Amt To Adj. </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtTTRefNo1" MaxLength="20" runat="server" TabIndex="-1" CssClass="textBox"
                                                                            Width="150px" AutoPostBack="true" OnTextChanged="txtTTRefNo_TextChanged"></asp:TextBox>
                                                                        <asp:Button ID="btnTTRef1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                            OnClientClick="return OpenTTNoList('1');" />
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
                                                                        <asp:TextBox ID="txtTTAmount1" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"
                                                                            onblur="checkttvalue('txtTTRefNo1','txtTTAmount1')"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtTTRefNo2" MaxLength="20" runat="server" TabIndex="-1" CssClass="textBox"
                                                                            Width="150px" AutoPostBack="true" OnTextChanged="txtTTRefNo_TextChanged"></asp:TextBox>
                                                                        <asp:Button ID="btnTTRef2" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                            OnClientClick="return OpenTTNoList('2');" />
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
                                                                        <asp:TextBox ID="txtTTAmount2" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"
                                                                            onblur="checkttvalue('txtTTRefNo2','txtTTAmount2')"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtTTRefNo3" MaxLength="20" runat="server" TabIndex="-1" CssClass="textBox"
                                                                            Width="150px" AutoPostBack="true" OnTextChanged="txtTTRefNo_TextChanged"></asp:TextBox>
                                                                        <asp:Button ID="btnTTRef3" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                            OnClientClick="return OpenTTNoList('3');" />
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
                                                                        <asp:TextBox ID="txtTTAmount3" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"
                                                                            onblur="checkttvalue('txtTTRefNo3','txtTTAmount3')"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtTTRefNo4" MaxLength="20" runat="server" TabIndex="-1" CssClass="textBox"
                                                                            Width="150px" AutoPostBack="true" OnTextChanged="txtTTRefNo_TextChanged"></asp:TextBox>
                                                                        <asp:Button ID="btnTTRef4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                            OnClientClick="return OpenTTNoList('4');" />
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
                                                                        <asp:TextBox ID="txtTTAmount4" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"
                                                                            onblur="checkttvalue('txtTTRefNo4','txtTTAmount4')"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtTTRefNo5" MaxLength="20" runat="server" TabIndex="-1" CssClass="textBox"
                                                                            Width="150px" AutoPostBack="true" OnTextChanged="txtTTRefNo_TextChanged"></asp:TextBox>
                                                                        <asp:Button ID="btnTTRef5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                                            OnClientClick="return OpenTTNoList('5');" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotTTAmt5" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
                                                                            Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBalTTAmt5" runat="server" Enabled="false" TabIndex="-1" CssClass="textBox"
                                                                            Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTTAmount5" runat="server" TabIndex="-1" CssClass="textBox" Width="80px"
                                                                            onblur="checkttvalue('txtTTRefNo5','txtTTAmount5')"></asp:TextBox>
                                                                    </td>
                                                                </tr>
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
                            <td>
                                &nbsp;
                            </td>
                            <td align="center" colspan="3" nowrap>
                                <asp:Button ID="btnLEI" runat="server" Text="Verify LEI" CssClass="buttonDefault"
                                    ToolTip="Verify LEI" TabIndex="132" OnClick="btnLEI_Click" Visible="true" />
                                &nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="37" CssClass="buttonDefault"
                                    ToolTip="Save" OnClick="btnSave_Click" OnClientClick="return CheckTT();" />
                                <%--//////////////////////////////////////////////Nilesh/////////////////////////////////////////////////--%>
                                &nbsp;
                                <asp:Button ID="btnSavePrint" runat="server" Text="Save & Print" CssClass="buttonDefault"
                                    ToolTip="Save & Print Export Bill Document Realisation Advice" OnClick="btnSave_Click"
                                    TabIndex="133" OnClientClick="return CheckTT();" CommandName="2" CommandArgument="print" />
                                <%-- /////////////////////////////////////////////END////////////////////////////////////////////////////--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="38" CssClass="buttonDefault"
                                    ToolTip="Cancel" OnClick="btnCancel_Click" />
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
