 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_INW_File_DataEntry.aspx.cs"
    Inherits="EDPMS_EDPMS_INW_File_DataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function openPurposeCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_PurposeCodeLookUp2.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectPurpose(id, hNo) {
            var txtPurposeCode = document.getElementById('txtPurposeCode');
            if (hNo == '1') {
                txtPurposeCode.value = id;
                __doPostBack('txtPurposeCode', '');
                return true;
            }
        }

        function openCustAcNo(e, hNo) {
            var keycode;
            var txtBranchCode = document.getElementById('txtBranchCode');
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_CustomerLookUp3.aspx?branchCode=' + txtBranchCode.value, 500, 500, 'CustAcNo');
                return false;
            }
            return true;
        }

        function selectCustomer(id) {
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            txtCustAcNo.value = id;
            __doPostBack('txtCustAcNo');
            return true;
        }

        function openSwiftCodeHelp(e) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../HelpForms/Help_SwiftCode.aspx', 500, 600, 'SwiftCode');
                return false;
            }
            return true;
        }

        function selectSwiftCode(code) {
            var txt_swiftcode = document.getElementById('txt_swiftcode');
            txt_swiftcode.value = code;
            __doPostBack('txt_swiftcode');
            return true;
        }

        function openDocNoList(e, hNo) {
            debugger;
            var keycode;
            var currentDate = new Date();
            var currentYear = currentDate.getFullYear() % 100;
            var txtBranchCode = document.getElementById('txtBranchCode');
            var txtDocPref = document.getElementById('txtDocPref');
            var txtYear = currentYear.toString(); //document.getElementById('txtYear');
            var txtDocNo = document.getElementById('txtDocNo');
            if (txtDocPref.value == 'ITT') {

            }
            else {
                if (keycode == 113 || e == 'mouseClick') {
                    open_popup('../HelpForms/TF_EXP_INWDOCLIST.aspx?DocPref=' + txtDocPref.value + '&BranchCode=' + txtBranchCode.value + '&Year=' + txtYear + '&DocNo=' + txtDocNo.value, 500, 600, 'CustAcNo');
                    return false;
                }
            }
            return true;
        }

        function OpenOverseasPartyList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;

            if (keycode == 113 || e == 'mouseClick') {

                var txtOverseasPartyID = $get("txtOverseasPartyID");
                open_popup('TF_OverseasPartyLookUp.aspx?bankID=' + txtOverseasPartyID.value, 450, 500, 'OverseasPartyList');
                return false;
            }
        }
        function selectOverseasParty(selectedID) {

            var id = selectedID;
            document.getElementById('hdnOverseasPartyId').value = id;
            document.getElementById('btnOverseasParty').click();
        }


        function selectDocNo(DocNo) {

            var txtDocPref = document.getElementById('txtDocPref');
            var txtDocNo = document.getElementById('txtDocNo');

            txtDocPref.value = DocNo.substring(0, 3);
            txtDocNo.value = DocNo.substring(8);
            __doPostBack('txtDocNo');
            return true;
        }

        function openCountryCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_CountryLookUp1.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectCountry(id, hNo) {
            var txtRemitterCountry = document.getElementById('txtRemitterCountry');
            var txtRemBankCountry = document.getElementById('txtRemBankCountry');

            if (hNo == '1') {
                txtRemitterCountry.value = id;
                __doPostBack('txtRemitterCountry', '');
                return true;
            }
            if (hNo == '2') {
                txtRemBankCountry.value = id;
                __doPostBack('txtRemBankCountry', '');
                return true;
            }
        }

        function ComputeAmtInINR() {
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');
            if (txtExchangeRate.value == '')
                txtExchangeRate.value = 0;

            document.getElementById('txtExchangeRate').value = parseFloat(txtExchangeRate.value).toFixed(10);
            var inramt = parseFloat((txtAmount.value) * (txtExchangeRate.value)).toFixed(0);
            if (isNaN(inramt) == false)
                txtAmtInINR.value = parseFloat(inramt).toFixed(2);
            else
                txtAmtInINR.value = 0;
        }

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
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function validateSaveDraft() {
            var txtDocNo = document.getElementById('txtDocNo');
            var txtDocDate = document.getElementById('txtDocDate');
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            var PanNumber = document.getElementById('txtPanNumber'); //Add by Anand 19-06-2023
            //-----------------------Anand 19-06-2023-----------------------------------------
            if (PanNumber.value != '') {
                if (PanNumber.value.length < 10) {
                    alert('Pan Number ID No must be 10 digit');
                    document.getElementById('txtPanNumber').focus();
                    return false;
                }
            }
        //--------------------------------------------End-------------------------------------------------
            if (txtDocNo.value == '') {
                alert('Enter Document No.');
                txtDocNo.focus();
                return false;
            }
            if (txtDocDate.value == '') {
                alert('Enter Document Date.');
                txtDocDate.focus();
                return false;
            }
            if (txtCustAcNo.value == '') {
                alert('Enter Customer A/C No.');
                txtCustAcNo.focus();
                return false;
            }

        }
        function validateSave() {
            var txtDocNo = document.getElementById('txtDocNo');
            var txtDocDate = document.getElementById('txtDocDate');
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            var txtPurposeCode = document.getElementById('txtPurposeCode');
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');
            var txtFIRCNo = document.getElementById('txtFIRCNo');
            var txtvalueDate = document.getElementById('txtvalueDate');
            var ddlCurrency = document.getElementById('ddlCurrency');

            var swiftcode = document.getElementById('txt_swiftcode');
            var remname = document.getElementById('txtRemitterName');
            var remadd = document.getElementById('txtRemitterAddress');
            var remcountry = document.getElementById('txtRemitterCountry');
            var rembank = document.getElementById('txtRemitterBank');
            var rembankcountry = document.getElementById('txtRemBankCountry');
            var PanNumber = document.getElementById('txtPanNumber'); // Add by Anand 19-06-2023 

            var txtAmount = document.getElementById('txtAmount');
            var txtBankAccountNumber = document.getElementById('txtBankAccountNumber');
            var txtIECCode = document.getElementById('txtIECCode');
            //var txtBankUniqueTransactionID = document.getElementById('txtBankUniqueTransactionID');
            var txtBankReferencenumber = document.getElementById('txtBankReferencenumber');
            var ddlIRMStatus = document.getElementById('ddlIRMStatus'); 
            if (txtvalueDate.value == '') {
                alert('Enter Value Date.');
                txtvalueDate.focus();
                return false;
            }
            //-----------------------Anand 19-06-2023-----------------------------------
            if (PanNumber.value == '') {
                if (PanNumber.value.length < 10) {
                    alert('Please Enter Pan Number.');
                    document.getElementById('txtPanNumber').focus();
                    return false;
                }
            }

            if (PanNumber.value != '') {
                if (PanNumber.value.length < 10) {
                    alert('Pan Number ID No must be 10 digit');
                    document.getElementById('txtPanNumber').focus();
                    return false;
                }
            }
            //-------------------------End----------------------------------------------------

            if (txtAmount.value == '0.00' || txtAmount.value == '') {
                alert('Enter FC Amount.');
                txtAmount.focus();
                return false;
            }
            if (txtBankAccountNumber.value == '') {
                alert('Enter Bank Account Number.');
                txtBankAccountNumber.focus();
                return false;
            }

            if (ddlIRMStatus.value == '') {
                alert('Enter IRM Status.');
                ddlIRMStatus.focus();
                return false;
            }
            if (txtPurposeCode.value == '') {
                alert('Please Enter Purpose code.');
                txtPurposeCode.focus();
                return false;
            }
            
            if (swiftcode.value == '') {
                alert('Enter swift code.');
                swiftcode.focus();
                return false;
            }

            if (ddlCurrency.value == '0') {
                alert('Select Currency.');
                ddlCurrency.focus();
                return false;
            }
            if (txtDocNo.value == '') {
                alert('Enter Document No.');
                txtDocNo.focus();
                return false;
            }
            if (txtDocDate.value == '') {
                alert('Enter Document Date.');
                txtDocDate.focus();
                return false;
            }
            if (txtCustAcNo.value == '') {
                alert('Enter Customer A/C No.');
                txtCustAcNo.focus();
                return false;
            }
            
            if (remname.value == '') {
                alert('Enter remitter name');
                remname.focus();
                return false;
            }
            if (remadd.value.length < 20) {
                alert('Please enter remitter address and remitter address should be Minimum 20 Character.');
                remadd.focus();
                return false;
            }
            if (remcountry.value == '') {
                alert('Enter remitter country');
                remcountry.focus();
                return false;
            }
            if (rembank.value == '') {
                alert('Enter remitter bank');
                rembank.focus();
                return false;
            }
            if (rembankcountry.value == '') {
                alert('Enter remitter bank country');
                rembankcountry.focus();
                return false;
            }
            if (txtIECCode.value == '') {
                alert('Please Enter IEC Code.');
                txtIECCode.focus();
                return false;
            }
//            if (txtBankUniqueTransactionID.value == '') {
//                alert('Bank Unique Transaction ID can not be Blank.');
//                txtBankUniqueTransactionID.focus();
//                return false;
//            }
            if (txtBankReferencenumber.value == '') {
                alert('Bank Reference number can not be Blank.');
                txtBankReferencenumber.focus();
                return false;
            }
        }
        function chkFIRCADCode() {
            var txtADCode = document.getElementById('txtADCode');

            if (txtADCode.value != '') {
                if (txtADCode.value.length < 7) {
                    alert('FIRC AD Code Should Be 7 Digit.');
                    setTimeout(function () {
                        txtADCode.focus();
                    }, 0);
                    return false;
                }
            }
        }
//----------------------------------------------------Anand 19-06-2023--------------------------------------------------------------
        function PanCardNo(key) {
            var charCode = (key.which) ? key.which : key.keyCode;
            var p = document.getElementById('txtPanNumber');


            if (p.value.length < 5) {
                if ((charCode > 64 && charCode < 91) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36) {
                    return true;
                }
                else {
                    return false;
                }
            } else if (p.value.length >= 5 && p.value.length <= 8) {
                if ((charCode > 47 && charCode < 58) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36 || (charCode > 95 && charCode < 106)) {
                    return true;
                } else {
                    return false;
                }

            }
            else if (p.value.length > 8) {
                if ((charCode > 64 && charCode < 91) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function convertToUppercase(textbox) {
            var text = textbox.value;
            text = text.toUpperCase();
            textbox.value = text;
        }
       
//---------------------------------------------------------------------End----------------------------------------------------------------
       
       
    </script>
    <style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement {
            font-family: Verdana, Sans-Serif, Arial;
            font-weight: normal;
            font-size: 8pt;
            margin: 0px !important;
            background-color: inherit;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            height: auto;
            text-align: left;
            background-position: left;
            list-style-type: none;
        }
        /* AutoComplete highlighted item */

        .autocomplete_highlightedListItem {
            background-color: #C4FFFE;
            color: black;
            padding: 1px;
        }

        /* AutoComplete item */

        .autocomplete_listItem {
            background-color: window;
            color: windowtext;
            padding: 1px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <uc1:Menu ID="Menu1" runat="server" />
            <asp:ScriptManager ID="ScriptManagerMain" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
            <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
            <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
                <ProgressTemplate>
                    <div id="progressBackgroundMain" class="progressBackground">
                        <div id="processMessage" class="progressimageposition">
                            <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <br />
                    <table width="100%" cellpadding="1">
                        <tr>
                            <td colspan="2" align="left">
                                <span class="pageLabel"><strong>Inward Remittances Data Entry-Maker </strong></span>
                            </td>
                            <td colspan="2" align="right">
                                <asp:Label ID="ReccuringLEI" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="LEIAmtCheck" runat="server"></asp:Label>
                                <asp:Label ID="LEIverify" runat="server"></asp:Label>
                                <asp:Button runat="server" ID="btnBack" CssClass="buttonDefault" Text="Back" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <input type="hidden" id="hdnOverseasPartyId" runat="server" />
                                <asp:Button ID="btnOverseasParty" Style="display: none;" runat="server" OnClick="btnOverseasParty_Click" />
                                <input type="hidden" id="hdnLeiFlag" runat="server" />
                                <input type="hidden" id="hdnLeiSpecialFlag" runat="server" />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
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
                                <asp:Label ID="lblLEIEffectDate" runat="server" Text="22/07/2023" Visible="false"></asp:Label>
                                <%--Added by anand 25/01/2024 for audit trail--%>
                                <input type="hidden" id="hdnDocumentDate" runat="server" />
                                <input type="hidden" id="hdnValueDate" runat="server" />
                                <input type="hidden" id="hdnSwiftCode" runat="server" />
                                <input type="hidden" id="hdnCustomerACNo" runat="server" />
                                <input type="hidden" id="hdnPurposeCode" runat="server" />
                                <input type="hidden" id="hdnRemitterID" runat="server" />
                                <input type="hidden" id="hdnRemitterName" runat="server" />
                                <input type="hidden" id="hdnRemitterCountry" runat="server" />
                                <input type="hidden" id="hdnRemitterAddress" runat="server" />
                                <input type="hidden" id="hdnRemittingBank" runat="server" />
                                <input type="hidden" id="hdnBankCountry" runat="server" />
                                <input type="hidden" id="hdnRemittingBankAddress" runat="server" />
                                <input type="hidden" id="hdnPurposeOfRemittance" runat="server" />
                                <input type="hidden" id="hdnCurrency" runat="server" />
                                <input type="hidden" id="hdnAmountInFC" runat="server" />
                                <input type="hidden" id="hdnExchangeRate" runat="server" />
                                <input type="hidden" id="hdnAmountInINR" runat="server" />
                                <input type="hidden" id="hdnFIRCNO" runat="server" />
                                <input type="hidden" id="hdnFIRCDate" runat="server" />
                                <input type="hidden" id="hdnFIRCADCode" runat="server" />
                                <input type="hidden" id="hdnBankUniqueTransactionID" runat="server" />
                                <input type="hidden" id="hdnIFSCCode" runat="server" />
                                <input type="hidden" id="hdnRemittanceADCode" runat="server" />
                                <input type="hidden" id="hdnIECCode" runat="server" />
                                <input type="hidden" id="hdnPanNumber" runat="server" />
                                <input type="hidden" id="hdnModeofPayment" runat="server" />
                                <input type="hidden" id="hdnBankReferenceNumbe" runat="server" />
                                <input type="hidden" id="hdnBankAccountNumber" runat="server" />
                                <input type="hidden" id="hdnIRMStatus" runat="server" />
                                <input type="hidden" id="hdnCheckarStatus" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" width="5%">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Branch ID : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="textBox" Style="width: 40px"
                                    Enabled="false" TabIndex="1"></asp:TextBox>
                                <asp:Label ID="lblBranchName" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 5%; white-space: nowrap;">
                                <%--<span class="mandatoryField">*</span> <span class="elementLabel">Year : </span>--%>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtYear" Enabled="false" runat="server" CssClass="textBox" Style="text-align: center"
                                    Width="20px" TabIndex="2" MaxLength="2" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Document No : </span>
                            </td>
                            <td align="left" width="3%">
                                <asp:TextBox ID="txtDocPref" runat="server" CssClass="textBox" Style="text-transform: uppercase; text-align: center"
                                    MaxLength="3" Width="30px" TabIndex="3" autocomplete="off" />
                                <%-- <ajaxToolkit:AutoCompleteExtender ServiceMethod="GetSearch" MinimumPrefixLength="1"
                                BehaviorID="AutoCompleteEx" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                TargetControlID="txtDocPref" ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </ajaxToolkit:AutoCompleteExtender>--%>
                                <asp:TextBox ID="txtDocBRCode" runat="server" AutoPostBack="true" CssClass="textBox"
                                    Style="text-align: center" TabIndex="4" Width="30px" Enabled="false" OnTextChanged="txtDocBRCode_TextChanged"
                                    MaxLength="3" />
                                <asp:TextBox ID="txtDocNo" runat="server" AutoPostBack="true" CssClass="textBox"
                                    Style="width: 60px; text-transform: uppercase; text-align: center" MaxLength="6"
                                    OnTextChanged="txtDocNo_TextChanged" TabIndex="5"  ToolTip="IRM number" />
                                <asp:TextBox ID="txtDocSrNo" runat="server" TabIndex="6" AutoPostBack="true" CssClass="textBox"
                                    Style="width: 20px;  text-transform: uppercase; text-align: center" MaxLength="2"
                                    OnTextChanged="txtDocSrNo_TextChanged" />
                                <asp:Button ID="btnDocNoList" runat="server"  CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="openDocNoList('mouseClick');" />
                            </td>
                            <td style="text-align: right; width: 5%; white-space: nowrap;">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Document Date :
                                </span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtDocDate" runat="server" CssClass="textBox" Style="width: 70px"
                                     TabIndex="7" ToolTip="Date on which IRM number is generated"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDocDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Value Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtvalueDate" runat="server" TabIndex="8" Width="70px" ToolTip="Remittance Date" CssClass="textBox"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtvalueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button3" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtvalueDate" PopupButtonID="Button3" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                    ValidationGroup="dtVal" ControlToValidate="txtvalueDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Swift Code:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_swiftcode" runat="server" CssClass="textBox" MaxLength="20"
                                    TabIndex="9" AutoPostBack="true" Enabled="false" OnTextChanged="txt_swiftcode_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnHelpSwiftCode" runat="server" CssClass="btnHelp_enabled" TabIndex="9"
                                    OnClientClick="openSwiftCodeHelp('mouseClick');" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Customer A/C No :
                                </span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtCustAcNo" runat="server" CssClass="textBox" Style="width: 100px"
                                    OnTextChanged="txtCustAcNo_TextChanged" Enabled="false" AutoPostBack="true" MaxLength="15" TabIndex="10"></asp:TextBox>
                                <asp:Button ID="btnHelpCustAcNo" runat="server" CssClass="btnHelp_enabled" TabIndex="9"
                                    OnClientClick="openCustAcNo('mouseClick');" />
                                &nbsp;
                            <asp:Label ID="lblCustName" runat="server" Text="" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Purpose Code :
                                </span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtPurposeCode" runat="server"  CssClass="textBox" Style="width: 50px"
                                    AutoPostBack="true"  OnTextChanged="txtPurposeCode_TextChanged"  MaxLength="6"
                                    TabIndex="11" oninput="convertToUppercase(this)" ></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="return openPurposeCode('mouseClick','1');" />
                                &nbsp;<asp:Label ID="lblpurposeCode" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">*</span><span class="elementLabel">Remitter ID :</span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtOverseasPartyID" Enabled="false" runat="server" AutoPostBack="True" CssClass="textBox"
                                    MaxLength="7" onkeydown="OpenOverseasPartyList(this);" TabIndex="12" Width="70px"
                                    ToolTip="Press F2 for help." OnTextChanged="txtOverseasPartyID_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnOverseasPartyList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="lblOverseasPartyDesc" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Name :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemitterName" runat="server" Enabled="false" Width="280px" CssClass="textBox"
                                    MaxLength="250" TabIndex="13" ToolTip="Remitter Name"></asp:TextBox>
                            </td>
                            <td style="text-align: right; white-space: nowrap;">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Country :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemitterCountry" runat="server" Enabled="false" CssClass="textBox" Style="width: 30px"
                                    AutoPostBack="true" MaxLength="2" ToolTip="Remitter Country" TabIndex="14" OnTextChanged="txtRemitterCountry_TextChanged"></asp:TextBox>
                                <asp:Button ID="Button4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" Enabled="false"
                                    OnClientClick="return openCountryCode('mouseClick','1');" />
                                &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                <%--<span id="SpanLei1" runat="server" class="elementLabel" visible="false">Customer LEI :</span>--%>
                                <asp:Label ID="lblLEI_CUST_Remark" runat="server" Visible="false"></asp:Label>
                        </tr>
                        <tr>
                            <td style="text-align: right; white-space: nowrap">
                                <span class="mandatoryField">*</span><span class="elementLabel">Remitter Address :</span>
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtRemitterAddress" CssClass="textBox" runat="server" MaxLength="100"
                                    Width="500px" Enabled="false" TabIndex="15"></asp:TextBox><%--
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <span id="SpanLei2" runat="server" class="elementLabel" visible="false">Customer LEI Expiry :</span>--%>
                                <asp:Label ID="lblLEIExpiry_CUST_Remark" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style1">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitting Bank :</span>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="txtRemitterBank" Enabled="false" CssClass="textBox" runat="server" MaxLength="100"
                                    Width="280px" TabIndex="16"></asp:TextBox>
                            </td>
                            <td style="text-align: right; white-space: nowrap;" class="style1">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Bank Country :</span>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="txtRemBankCountry" runat="server" Enabled="false" CssClass="textBox" Style="width: 30px"
                                    AutoPostBack="true" MaxLength="2" TabIndex="17" OnTextChanged="txtRemBankCountry_TextChanged"></asp:TextBox>
                                <asp:Button ID="Button5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" Enabled="false"
                                    OnClientClick="return openCountryCode('mouseClick','2');" />
                                &nbsp;<asp:Label ID="lblRemBankCountryDesc" runat="server" CssClass="elementLabel"
                                    Width="200px"></asp:Label><%--
                                &nbsp;<span id="SpanLei3" runat="server" class="elementLabel" visible="false">Applicant LEI :</span>--%>
                                <asp:Label ID="lblLEI_Overseas_Remark" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap; text-align: right">
                                <span class="elementLabel">Remitting Bank Address : </span>
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtRemitterBankAddress" Enabled="false" CssClass="textBox" runat="server" MaxLength="100"
                                    Width="500px" TabIndex="18"></asp:TextBox><%--
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <span id="SpanLei4" runat="server" class="elementLabel" visible="false">Applicant LEI Expiry :</span>--%>
                                <asp:Label ID="lblLEIExpiry_Overseas_Remark" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; white-space: nowrap">
                                <span class="elementLabel">Purpose Of Remittance :</span>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtpurposeofRemittance" CssClass="textBox" runat="server" MaxLength="40"
                                    Width="280px" TabIndex="19" ToolTip="Applicable purpose of remittance"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Currency : </span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="dropdownList" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"
                                    AutoPostBack="true" TabIndex="20" Height="19px" ToolTip="Remittance FC Code">
                                </asp:DropDownList>
                                &nbsp;
                            <asp:Label ID="lblcurrencyDesc" runat="server" CssClass="elementLabel" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">Amount In FC : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="textBoxRight" Style="width: 130px"
                                    TabIndex="21" AutoPostBack="true" ToolTip="Remittance FC Amount" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                <span class="elementLabel">LEI MINT Rate :</span>
                                <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                            <%--
                        <td>
                            <span class="elementLabel">LEI Exch Rate :</span>
                            <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>--%>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">Exchange Rate : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBoxRight"  
                                    Style="width: 130px" AutoPostBack="true" TabIndex="22" ontextchanged="txtExchangeRate_TextChanged" ToolTip="Exchange rate for INR conversion"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">Amount In INR : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAmtInINR" runat="server" CssClass="textBoxRight" 
                                    TabIndex="23" Enabled="false" ToolTip="Equivalent INR code" Style="width: 130px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">FIRC NO : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFIRCNo" runat="server" CssClass="textBox" MaxLength="15" TabIndex="24"
                                    Style="width: 120px"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <span class="elementLabel">FIRC Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFIRCDate" runat="server" CssClass="textBox" Style="width: 70px"
                                    TabIndex="25"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="meFircDate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFIRCDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFIRCDate" PopupButtonID="Button2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFircDate"
                                    ValidationGroup="dtVal" ControlToValidate="txtFIRCDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                <span class="elementLabel">FIRC AD Code : </span>
                                <asp:TextBox ID="txtADCode" runat="server" CssClass="textBox" Style="width: 55px"
                                    MaxLength="7" TabIndex="26"></asp:TextBox>
                            </td>
                        </tr>
                       <%-- ------------------- create by Anand15-06-2023---------------------------------------------%>
                        <tr>
                        <td style="text-align: right">
                                <span class="mandatoryField">*<span class="elementLabel">Bank Unique Transaction ID : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankUniqueTransactionID" MaxLength="25" Enabled="false" runat="server" CssClass="textBoxRight" 
                                    Style="width: 170px" TabIndex="27" ToolTip="Technical unique transaction ID for each request."></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                               <span class="mandatoryField">* <span class="elementLabel">IFSC Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIFSCCode" MaxLength="11" runat="server" CssClass="textBoxRight" 
                                    width= "120px" Enabled="false" TabIndex="28" ToolTip="IFSC Code of the bank" Style='text-transform: uppercase'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td style="text-align: right">
                               <span class="mandatoryField">*<span class="elementLabel">Remittance AD Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemittanceADCode" runat="server" MaxLength="50" CssClass="textBoxRight"
                                    ToolTip="Remittance Bank AD Code" Enabled="false"  TabIndex="29" Style="width: 120px"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <span class="mandatoryField">*<span class="elementLabel">IEC Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIECCode" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="10" 
                                   ToolTip="IEC Code" TabIndex="30" Style="width: 120px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                        <td style="text-align: right">
                                <span class="mandatoryField">*<span class="elementLabel">Pan Number : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanNumber" runat="server" Enabled="false"  CssClass="textBoxRight" 
                                    MaxLength="10" TabIndex="31" ToolTip="Pan Number"
                                     Width="120px" ViewStateMode="Enabled" Style='text-transform: uppercase'></asp:TextBox>
                            </td>
                            
                            <td style="text-align: right">
                                <span class="elementLabel">Mode of Payment : </span>
                            </td>
                            <td align="left">
                                <%--<asp:TextBox ID="txtModeofPayment" runat="server" MaxLength="50"  CssClass="textBoxRight" TabIndex="32"
                                    ToolTip="Mode of payment (e.g.SWIFT or any mechanism,NEFT, RTGS etc through which payment is received)" Style="width: 180px"></asp:TextBox>--%>
                                     <asp:DropDownList ID="ddlModeOfPayment" runat="server" CssClass="dropdownList"
                                     TabIndex="32" Height="19px" Style="width: 130px" ToolTip="Mode of payment (e.g.SWIFT or any mechanism,NEFT, RTGS etc through which payment is received)">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>  
                                        <asp:ListItem Value="SWIFT">SWIFT</asp:ListItem> 
                                        <asp:ListItem Value="RTGS">RTGS</asp:ListItem>  
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                       <%--<td style="text-align: right">
                                <span class="mandatoryField">*<span class="elementLabel">Factoring flag : </span>
                            </td>
                            <td align="left">
                              
                                    <asp:DropDownList ID="ddlFactoringflag" runat="server" CssClass="dropdownList"
                                     TabIndex="16" Height="19px">
                                    <asp:ListItem Value=""> </asp:ListItem>  
                                        <asp:ListItem>Y</asp:ListItem>  
                                        <asp:ListItem>N</asp:ListItem>  
            
                                </asp:DropDownList>
                            </td>
                         <td style="text-align: right">
                                <span class="mandatoryField">*<span class="elementLabel">Forfeiting flag : </span>
                            </td>
                            <td align="left">
                             
                                     <asp:DropDownList ID="ddlForfeitingflag" runat="server" CssClass="dropdownList"
                                     TabIndex="16" Height="19px">
                                    <asp:ListItem Value=""> </asp:ListItem>  
                                        <asp:ListItem>Y</asp:ListItem> 
                                        <asp:ListItem>N</asp:ListItem>  
            
                                </asp:DropDownList>
                            </td>--%>
                             <td style="text-align: right">
                                <span class="elementLabel">Bank Reference Number :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankReferencenumber" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="20" TabIndex="33"
                                    Style="width: 140px" ToolTip="Bank reference number corresponding to IRM message."></asp:TextBox>
                            </td>
                              <td style="text-align: right">
                               <span class="mandatoryField">*<span class="elementLabel">Bank Account Number : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankAccountNumber" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="25" TabIndex="34"
                                   ToolTip="Bank account number"  Style="width: 180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td style="text-align: right">
                                <span class="mandatoryField">*<span class="elementLabel">IRM Status : </span>
                            </td>
                            <td align="left">       
                                    <asp:DropDownList ID="ddlIRMStatus" runat="server" CssClass="dropdownList"
                                     TabIndex="35" Height="19px" ToolTip="IRM Status(F – Fresh,A -Amended,C – Cancelled)">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>  
                                        <asp:ListItem Value="Fresh">Fresh</asp:ListItem> 
                                        <%--<asp:ListItem Value="Amended">Amended</asp:ListItem>  --%>
                                        <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                       <%-- ----------------------------End--------------------------------------------------%>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <asp:Panel ID="MakerVisible" runat="server">
                            <td></td>
                            <td align="left" colspan="3">
                                <asp:Button ID="btnLEI" runat="server" Text="Verify LEI" CssClass="buttonDefault"
                                    ToolTip="Verify LEI" TabIndex="36" OnClick="btnLEI_Click" />
                                <%-- &nbsp;
                            <asp:Button ID="btnSaveDraft" runat="server" CssClass="buttonDefault" Text="Save & Draft"
                                OnClick="btnSaveDraft_Click" TabIndex="23" />--%>
                            &nbsp;
                            <asp:Button ID="btnAdd" runat="server" CssClass="buttonDefault" Text="Sent To Checker" OnClick="btnAdd_Click"
                                TabIndex="37" />
                                &nbsp;
                            <asp:Button ID="btnPrint" runat="server" CssClass="buttonDefault" Text="Save & View"
                                OnClick="btnPrint_Click" TabIndex="38" />
                                &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" Text="Cancel"
                                OnClick="btnCancel_Click" TabIndex="39" />
                          
                            </td>
                                </asp:Panel>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" 
                                    Font-Bold="true"></asp:Label>
                            </td>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');

            if (txtAmount.value == '')
                txtAmount.value = 0;
            txtAmount.value = parseFloat(txtAmount.value).toFixed(2);

            if (txtExchangeRate.value == '')
                txtExchangeRate.value = 0;
            txtExchangeRate.value = parseFloat(txtExchangeRate.value).toFixed(5);

            if (txtAmtInINR.value == '')
                txtAmtInINR.value = 0;
            txtAmtInINR.value = parseFloat(txtAmtInINR.value).toFixed(2);
        } //function
    </script>
</body>
</html>
