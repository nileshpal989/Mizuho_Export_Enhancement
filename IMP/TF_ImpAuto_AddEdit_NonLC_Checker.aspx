<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ImpAuto_AddEdit_NonLC_Checker.aspx.cs" Inherits="IMP_TF_ImpAuto_AddEdit_NonLC_Checker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <%--<link href="../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen">

    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>--%>

    <script language="javascript" type="text/javascript">

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

                if (CName == "Maturity Date") {
                    if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                        alert('Invalid ' + CName);
                        controlID.focus();
                        return false;
                    }
                }
                else {
                    if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                        alert('Invalid ' + CName);
                        controlID.focus();
                        return false;
                    }
                }
            }
        }

        function OpenCurrencyList() {

            open_popup('../TF_CurrencyLookup1.aspx?pc=1', 450, 350, 'CurrencyList');
            return false;
        }

        function selectCurrency(selectedID) {
            var id = selectedID;
            document.getElementById('hdnCurId').value = id;
            document.getElementById('btnCurr').click();
            $get("dropDownListCurrency").focus();
        }
        function OpenOverseasPartyList(e) {
            var keycode;
            var txtOverseasBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtOverseasPartyID = document.getElementById('txtOverseasPartyID').value;
                open_popup('../TF_OverseasPartyLookUp.aspx?bankID=' + txtOverseasPartyID, 450, 550, 'OverseasPartyList');
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
            var txtOverseasBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtOverseasBank = document.getElementById('txtOverseasBankID').value;
                open_popup('../TF_OverseasBankLookup.aspx?hNo=1&bankID=' + txtOverseasBank, 450, 650, 'OverseasBankList');
                return false;
            }
        }
        function selectOverseasBank(selectedID) {
            var id = selectedID;
            document.getElementById('hdnOverseasId').value = id;
            document.getElementById('btnOverseasBank').click();
        }

        function OpenIntermediaryBankList(e) {
            var keycode;
            var txtIntermediaryBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtIntermediaryBank = document.getElementById('txtIntermediaryBankID').value;
                open_popup('../TF_OverseasBankLookup.aspx?hNo=2&bankID=' + txtIntermediaryBank, 450, 650, 'IntermediaryBankList');
                return false;
            }
        }
        function selectIntermediaryBank(selectedID) {
            var id = selectedID;
            document.getElementById('hdnIntermediaryBank').value = id;
            document.getElementById('btnIntermediaryBank').click();
        }

        function OpenAcWithInstiList(e) {
            var keycode;
            var txtIntermediaryBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtAcwithInstitution = document.getElementById('txtAcwithInstitution').value;
                open_popup('../TF_OverseasBankLookup.aspx?hNo=3&bankID=' + txtAcwithInstitution, 450, 650, 'AcWithInstiList');
                return false;
            }
        }
        function selectAcWithInsti(selectedID) {
            var id = selectedID;
            document.getElementById('hdnAcwithInsti').value = id;
            document.getElementById('btnAcwithInsti').click();
        }

        function OpenDocsRcvdBankList(e) {
            var keycode;
            var txtIntermediaryBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtDocsRcvdBank = document.getElementById('txtDocsRcvdBank').value;
                open_popup('../TF_OverseasBankLookup.aspx?hNo=4&bankID=' + txtDocsRcvdBank, 450, 650, 'DocsRcvdBankList');
                return false;
            }
        }
        function selectDocsRcvdBank(selectedID) {
            var id = selectedID;
            document.getElementById('hdnDocsRcvdBank').value = id;
            document.getElementById('btnDocsRcvdBank').click();
        }
        function OpenCustomerCodeList(e) {
            var keycode;
            var txtIntermediaryBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtImporterID = document.getElementById('txtImporterID').value;
                open_popup('../TF_CustomerLookUp.aspx?CustID=' + txtImporterID, 450, 450, 'CustomerCodeList');
                return false;
            }
        }
        function selectCustomer(selectedID) {

            var id = selectedID;
            document.getElementById('hdnCustomerCode').value = id;
            document.getElementById('btnCustomerCode').click();
        }
        function OpenPortCodeList(e) {
            var keycode;
            var txtCoveringTo;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtCoveringTo = document.getElementById('txtCoveringTo').value;
                open_popup('../TF_PortCodeLookup.aspx?PortID=' + txtCoveringTo, 450, 450, 'PortCodeList');
                return false;
            }
        }
        function selectPort(selectedID) {

            var id = selectedID;
            document.getElementById('hdnPortCode').value = id;
            document.getElementById('btnPortCode').click();
        }

        function OpenCountryList(hNo) {

            open_popup('../TF_CountryLookUp1.aspx?hNo=' + hNo, 450, 450, 'CountryList');
            return false;
        }

        function selectCountry(selectedID, hNo) {
            var id = selectedID;
            document.getElementById('hdnCountryHelpNo').value = hNo;
            document.getElementById('hdnCountry').value = id;
            document.getElementById('btnCountry').click();

        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function OnChangeCheckbox(checkbox) {
            var Chk = document.getElementById('chkbox');
            if (checkbox.checked) {
                Chk.innerHTML = "Yes";
            }
            else {
                Chk.innerHTML = "No";
            }
        }
       
    </script>
    <script language="javascript" type="text/javascript">
        function ValidateSave() {
            var custAcNo = document.getElementById('txtImporterID');
            var overseasPartyID = document.getElementById('txtOverseasPartyID');
            var overseasBankID = document.getElementById('txtOverseasBankID');
            var billAmt = document.getElementById('txtBillAmount');

            if (custAcNo.value == '') {
                alert('Enter Importer ID.');
                custAcNo.focus();
                return false;
            }
            if (overseasPartyID.value == '') {
                alert('Enter Overseas Party ID.');
                overseasPartyID.focus();
                return false;
            }
            if (overseasBankID.value == '') {
                alert('Enter Overseas Bank ID.');
                overseasBankID.focus();
                return false;
            }
            if (billAmt.value == '') {
                alert('Enter Bill Amount.');
                billAmt.focus();
                return false;
            }
            else {
                var _billAmt = parseFloat(billAmt.value).toFixed(2);
                if (_billAmt == 0) {
                    alert('Enter Bill Amount.');
                    billAmt.focus();
                    return false;
                }
            }

            return true;
        }

        function OpenCopyFromDocNoList(e) {

            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {

                var txtCopyFromDocNo = document.getElementById("txtCopyFromDocNo");
                var custAcNo = document.getElementById("txtImporterID");

                if (custAcNo.value == '') {
                    alert('Enter Customer A/C No.');
                    custAcNo.focus();
                    return false;
                }

                open_popup('IMP_DocumentNoLookUpforCopy.aspx?custAcNo=' + custAcNo.value + '&DocNo=' + txtCopyFromDocNo.value, 450, 350, 'DocNoList');
                return false;
            }
        }

        function selectDocNoCopy(selectedID) {
            var id = selectedID;
            document.getElementById('txtCopyFromDocNo').value = id;
            document.getElementById('btnCopy').focus();
        }
        function ChkEDDNo1() {

            var eddno = document.getElementById('txtEDDNo').value;
            var eddno1 = eddno.substring(10, 12).toString();
            var edd = '';
            if (eddno1 != 'IN') {
                alert('Invalid EDD No');
                document.getElementById('txtEDDNo').value = edd;
                document.getElementById('txtEDDNo').focus;
            }

        }
        function ChkEDDNo(key) {
            var charCode = (key.which) ? key.which : key.keyCode;
            var eddno = document.getElementById('txtEDDNo').value;
            var eddnol = eddno.length;
            var eddno1 = eddno.substring(10, 12).toString();
            var eddno2 = eddno1.length;
            //            var eddno1 = eddno.substring(12, 14).toString();

            if (eddnol < 10) {
                if ((charCode > 47 && charCode < 58) || charCode == 8 || charCode == 9 || charCode == 127 || (charCode > 95 && charCode < 106)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (eddno2 <= 2) {
                if ((charCode > 64 && charCode < 91) || charCode == 8 || charCode == 9 || charCode == 127) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
    </script>

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
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Imports Non LC Bill Data Entry Checker</strong></span>
                        </td>
                        <td align="right" style="width: 50%" nowrap>
                         <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  " 
                                    style="color:red" ></asp:Label>&nbsp;

                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                            <input type="hidden" id="hdnCurId" runat="server" />
                            <asp:Button ID="btnCurr" Style="display: none;" runat="server" OnClick="btnCurr_Click" />
                            <input type="hidden" id="hdnOverseasId" runat="server" />
                            <asp:Button ID="btnOverseasBank" Style="display: none;" runat="server" OnClick="btnOverseasBank_Click" />
                            <input type="hidden" id="hdnIntermediaryBank" runat="server" />
                            <asp:Button ID="btnIntermediaryBank" Style="display: none;" runat="server" OnClick="btnIntermediaryBank_Click" />
                            <input type="hidden" id="hdnAcwithInsti" runat="server" />
                            <asp:Button ID="btnAcwithInsti" Style="display: none;" runat="server" OnClick="btnAcwithInsti_Click" />
                            <input type="hidden" id="hdnDocsRcvdBank" runat="server" />
                            <asp:Button ID="btnDocsRcvdBank" Style="display: none;" runat="server" OnClick="btnDocsRcvdBank_Click" />
                            <input type="hidden" id="hdnOverseasPartyId" runat="server" />
                            <asp:Button ID="btnOverseasParty" Style="display: none;" runat="server" OnClick="btnOverseasParty_Click" />
                            <input type="hidden" id="hdnCustomerCode" runat="server" />
                            <asp:Button ID="btnCustomerCode" Style="display: none;" runat="server" OnClick="btnCustomerCode_Click" />
                            <input type="hidden" id="hdnPortCode" runat="server" />
                            <asp:Button ID="btnPortCode" Style="display: none;" runat="server" OnClick="btnPortCode_Click" />
                            <input type="hidden" id="hdnCountryHelpNo" runat="server" />
                            <input type="hidden" id="hdnCountry" runat="server" />
                            <asp:Button ID="btnCountry" Style="display: none;" runat="server" OnClick="btnCountry_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="15%">
                            <span class="elementLabel">Document No :</span>
                        </td>
                        <td width="30%" nowrap colspan="3" align="left">
                            <asp:TextBox ID="txtDocNo" Width="160px" runat="server" CssClass="textBox" ReadOnly="True"
                                TabIndex="-1"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rdbSight" Text="Sight" CssClass="elementLabel" runat="server"
                                GroupName="SU" Checked="True" TabIndex="1" Visible="false" />
                            <asp:RadioButton ID="rdbUsance" Text="Usance" CssClass="elementLabel" runat="server"
                                GroupName="SU" TabIndex="1" Visible="false" />
                        </td>
                        <td align="right">
                            <span class="elementLabel">Sub Doc No :</span>
                        </td>
                        <td align="left" width="10%">
                            <asp:TextBox ID="txtSubDocNo" Width="20px" Text="1" Style="text-align: center;" runat="server"
                                CssClass="textBox" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                        </td>
                        <td align="left" nowrap colspan="2">
                            <asp:Label ID="lblCopyFrom" runat="server" CssClass="elementLabel" Text="Copy From :"
                                Width="70px" Visible="false"></asp:Label><asp:TextBox ID="txtCopyFromDocNo" Width="150px"
                                    runat="server" CssClass="textBox" TabIndex="115" Visible="false" MaxLength="19"
                                    onkeydown="OpenCopyFromDocNoList(this);" ToolTip="Press F2 for help."></asp:TextBox><asp:Button
                                        ID="btnDocNoListtoCopy" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                        Visible="false" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="buttonCopy" Width="40px"
                                ToolTip="Copy" TabIndex="116" OnClick="btnCopy_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Importer ID :</span>
                        </td>
                        <td nowrap colspan="3" align="left">
                            <asp:TextBox ID="txtImporterID" runat="server" AutoPostBack="True" CssClass="textBox"
                                MaxLength="6" OnTextChanged="txtImporterID_TextChanged" onkeydown="OpenCustomerCodeList(this);"
                                TabIndex="2" Width="70px" ToolTip="Press F2 for help."></asp:TextBox><asp:Button
                                    ID="btnImporterList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                        ID="lblImporterDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                        </td>
                        <td align="right" width="10%">
                            <span class="elementLabel">Date Received:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDateRcvd" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                Width="70px" TabIndex="3"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtDateRcvd" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDateRcvd" PopupButtonID="btncalendar_DocDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                ValidationGroup="dtVal" ControlToValidate="txtDateRcvd" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Overseas Party ID :</span>
                        </td>
                        <td nowrap colspan="3" align="left">
                            <asp:TextBox ID="txtOverseasPartyID" runat="server" AutoPostBack="True" CssClass="textBox"
                                MaxLength="5" OnTextChanged="txtOverseasPartyID_TextChanged" onkeydown="OpenOverseasPartyList(this);"
                                TabIndex="4" Width="70px" ToolTip="Press F2 for help."></asp:TextBox><asp:Button
                                    ID="btnOverseasPartyList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                        ID="lblOverseasPartyDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Overseas Bank ID :</span>
                        </td>
                        <td nowrap colspan="7" align="left">
                            <asp:TextBox ID="txtOverseasBankID" runat="server" AutoPostBack="True" CssClass="textBox"
                                MaxLength="5" OnTextChanged="txtOverseasBankID_TextChanged" onkeydown="OpenOverseasBankList(this);"
                                TabIndex="5" Width="70px" ToolTip="Press F2 for help."></asp:TextBox><asp:Button
                                    ID="btnOverseasBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                        ID="lblOverseasBankDesc" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                            &nbsp;
                            <asp:RadioButton ID="rdbACno_OB" Text="A/C No" CssClass="elementLabel" runat="server"
                                GroupName="OB" Checked="True" TabIndex="5" />
                            <asp:RadioButton ID="rdbSortCode_OB" Text="Sort Code" CssClass="elementLabel" runat="server"
                                GroupName="OB" TabIndex="5" /><asp:RadioButton ID="rdbChipUID_OB" Text="Chip UID"
                                    CssClass="elementLabel" runat="server" GroupName="OB" TabIndex="5" />
                            <asp:RadioButton ID="rdbChipsABAno_OB" Text="Chips ABA No" CssClass="elementLabel"
                                runat="server" GroupName="OB" TabIndex="5" /><asp:RadioButton ID="rdbABAno_OB" Text="ABA No"
                                    CssClass="elementLabel" runat="server" GroupName="OB" TabIndex="5" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Intermediary Bank ID :</span>
                        </td>
                        <td nowrap colspan="7" align="left">
                            <asp:TextBox ID="txtIntermediaryBankID" runat="server" AutoPostBack="True" CssClass="textBox"
                                MaxLength="5" OnTextChanged="txtIntermediaryBankID_TextChanged" onkeydown="OpenIntermediaryBankList(this);"
                                TabIndex="6" Width="70px" ToolTip="Press F2 for help."></asp:TextBox><asp:Button
                                    ID="btnIntermediaryBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                        ID="lblIntermediaryBankDesc" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                            &nbsp;
                            <asp:RadioButton ID="rdbACno_IB" Text="A/C No" CssClass="elementLabel" runat="server"
                                GroupName="IB" Checked="True" TabIndex="6" />
                            <asp:RadioButton ID="rdbSortCode_IB" Text="Sort Code" CssClass="elementLabel" runat="server"
                                GroupName="IB" TabIndex="6" /><asp:RadioButton ID="rdbChipUID_IB" Text="Chip UID"
                                    CssClass="elementLabel" runat="server" GroupName="IB" TabIndex="6" />
                            <asp:RadioButton ID="rdbChipsABAno_IB" Text="Chips ABA No" CssClass="elementLabel"
                                runat="server" GroupName="IB" TabIndex="6" /><asp:RadioButton ID="rdbABAno_IB" Text="ABA No"
                                    CssClass="elementLabel" runat="server" GroupName="IB" TabIndex="6" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">A/C with Institution :</span>
                        </td>
                        <td nowrap colspan="6" align="left">
                            <asp:TextBox ID="txtAcwithInstitution" AutoPostBack="true" runat="server" CssClass="textBox"
                                onkeydown="OpenAcWithInstiList(this);" MaxLength="5" TabIndex="7" Width="70px"
                                ToolTip="Press F2 for help." OnTextChanged="txtAcwithInstitution_TextChanged"></asp:TextBox><asp:Button
                                    ID="btnAcwithInstiList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                        ID="lblAcWithInstiBankDesc" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                            &nbsp;
                            <asp:RadioButton ID="rdbACno_AI" Text="A/C No" CssClass="elementLabel" runat="server"
                                GroupName="AI" Checked="True" TabIndex="7" />
                            <asp:RadioButton ID="rdbSortCode_AI" Text="Sort Code" CssClass="elementLabel" runat="server"
                                GroupName="AI" TabIndex="7" /><asp:RadioButton ID="rdbChipUID_AI" Text="Chip UID"
                                    CssClass="elementLabel" runat="server" GroupName="AI" TabIndex="7" />
                            <asp:RadioButton ID="rdbChipsABAno_AI" Text="Chips ABA No" CssClass="elementLabel"
                                runat="server" GroupName="AI" TabIndex="7" /><asp:RadioButton ID="rdbABAno_AI" Text="ABA No"
                                    CssClass="elementLabel" runat="server" GroupName="AI" TabIndex="7" />
                        </td>
                        <td align="left" width="40%">
                            <span class="elementLabel">No. of Docs</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%">
                            <span class="elementLabel">Draft No. :</span>
                        </td>
                        <td width="3%" align="left">
                            <asp:TextBox ID="txtDraftNo" Width="100px" runat="server" CssClass="textBox" TabIndex="7"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="right" width="5%" nowrap>
                            <span class="elementLabel">Dated :</span>
                        </td>
                        <td width="10%" nowrap align="left">
                            <asp:TextBox ID="txtDraftDocDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="8"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDraftDoc" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtDraftDocDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDTDraftDoc" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calDraftDoc" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDraftDocDate" PopupButtonID="btnDTDraftDoc" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdDraftDoc"
                                ValidationGroup="dtVal" ControlToValidate="txtDraftDocDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                         <td nowrap align="right">
                            <span class="elementLabel">Acceptance Recived :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:CheckBox ID="chkAcceptance" runat="server" CssClass="elementLabel"
                                TabIndex="10"  onclick="OnChangeCheckbox (this)" />
                                <asp:Label ID="chkbox" CssClass="elementLabel" runat="server"></asp:Label>
                                
                        </td>
                         <td></td>
                        <td align="left">
                            <asp:TextBox ID="txtDraftDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="10"
                                MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Invoice No. :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtInvoiceNo" Width="100px" runat="server" CssClass="textBox" TabIndex="11"
                                MaxLength="200"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Dated :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="12"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDTInvoice" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtInvoiceDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDTInvoice" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calInvoiceDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtInvoiceDate" PopupButtonID="btnDTInvoice" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="mdDTInvoice"
                                ValidationGroup="dtVal" ControlToValidate="txtInvoiceDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td nowrap align="right">
                            <span class="elementLabel">Various Invoices :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:CheckBox ID="chkVariousInvoices" runat="server" Text="" CssClass="elementLabel"
                                TabIndex="13" />
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInvoiceDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="14"
                                MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">AWB/BL No/PP :</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAWBno" Width="100px" runat="server" CssClass="textBox" TabIndex="15"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Dated :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAWBDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                Width="70px" TabIndex="16"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdAWBdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtAWBDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDTAWB" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calAWB" runat="server" Format="dd/MM/yyyy" TargetControlID="txtAWBDate"
                                PopupButtonID="btnDTAWB" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="mdAWBdate"
                                ValidationGroup="dtVal" ControlToValidate="txtAWBDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td nowrap align="right">
                            <span class="elementLabel">Issued By :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtAwbIssuedBy" Width="100px" runat="server" CssClass="textBox"
                                TabIndex="17" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAWBDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="18"
                                MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Packing List :</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPackingList" Width="100px" runat="server" CssClass="textBox"
                                TabIndex="19" MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Dated :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPackingListDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="20"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdPackingList" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtPackingListDate" ErrorTooltipEnabled="True"
                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDTPackingList" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calPackingList" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtPackingListDate" PopupButtonID="btnDTPackingList" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="mdPackingList"
                                ValidationGroup="dtVal" ControlToValidate="txtPackingListDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPackingDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="21"
                                MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Cert of Origin :</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCertOfOrigin" Width="100px" runat="server" CssClass="textBox"
                                TabIndex="22" MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                        <td nowrap align="right">
                            <span class="elementLabel">Issued By :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtCertIssuedBy" Width="100px" runat="server" CssClass="textBox"
                                TabIndex="23" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCertOfOriginDoc" Width="50px" runat="server" CssClass="textBox"
                                TabIndex="24" MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Ins. Policy :</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInsPolicy" Width="100px" runat="server" CssClass="textBox" TabIndex="25"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Dated :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInsPolicyDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="26"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdInsPolicy" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtInsPolicyDate" ErrorTooltipEnabled="True"
                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDTInsPolicy" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calInsPolicy" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtInsPolicyDate" PopupButtonID="btnDTInsPolicy" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="mdInsPolicy"
                                ValidationGroup="dtVal" ControlToValidate="txtInsPolicyDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td nowrap align="right">
                            <span class="elementLabel">Issued By :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtInsPolicyIssuedBy" Width="100px" runat="server" CssClass="textBox"
                                TabIndex="27" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInsPolicyDoc" Width="50px" runat="server" CssClass="textBox"
                                TabIndex="28" MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Miscellaneous :</span>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtMiscellaneous" Width="180px" runat="server" CssClass="textBox"
                                TabIndex="29" MaxLength="100"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Country of Origin :</span>
                        </td>
                        <td nowrap align="right">
                            <asp:DropDownList ID="ddlCountryOfOrigin" runat="server" CssClass="dropdownList"
                                TabIndex="30" Width="50px">
                            </asp:DropDownList>
                            <asp:Button ID="btnCountryOriginList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />&nbsp;&nbsp;<span
                                class="elementLabel">Country :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="dropdownList" TabIndex="31"
                                Width="50px">
                            </asp:DropDownList>
                            <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMiscDoc" Width="50px" runat="server" CssClass="textBox" TabIndex="32"
                                MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Covering From :</span>
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="txtCoveringFrom" runat="server" CssClass="textBox" MaxLength="20"
                                TabIndex="33" Width="100px"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">To :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtCoveringTo" runat="server" CssClass="textBox" MaxLength="20"
                                TabIndex="34" Width="90px" onkeydown="OpenPortCodeList(this);" ToolTip="Press F2 for help."></asp:TextBox><asp:Button
                                    ID="btnCoveringTo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Commodity :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtCommodity" runat="server" CssClass="textBox" MaxLength="50" TabIndex="35"
                                Width="100px"></asp:TextBox>
                        </td>
                        <td align="right" nowrap width="5%">
                            <span class="elementLabel">Quantity :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="textBox" MaxLength="25" TabIndex="36"
                                Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">D.O./Gaurantee No. :</span>
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="txtDoGauranteeNo" runat="server" CssClass="textBox" MaxLength="20"
                                TabIndex="37" Width="100px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Dated :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDoGauranteeDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="38"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDoGaurantee" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtDoGauranteeDate" ErrorTooltipEnabled="True"
                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDoGaurantee" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calDoGaurantee" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDoGauranteeDate" PopupButtonID="btnDoGaurantee" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="mdDoGaurantee"
                                ValidationGroup="dtVal" ControlToValidate="txtDoGauranteeDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Shipment :</span>
                        </td>
                        <td colspan="2" align="left">
                            <asp:RadioButton ID="rdbByAir" Text="By Air" CssClass="elementLabel" runat="server"
                                GroupName="Shipment" Checked="True" TabIndex="39" /><asp:RadioButton ID="rdbBySea"
                                    Text="By Sea" CssClass="elementLabel" runat="server" GroupName="Shipment" TabIndex="39" /><asp:RadioButton
                                        ID="rdbByRoad" Text="By Road" CssClass="elementLabel" runat="server" GroupName="Shipment"
                                        TabIndex="39" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Import Licence No. :</span>
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="txtImportLicenceNo" runat="server" AutoPostBack="True" CssClass="textBox"
                                MaxLength="20" TabIndex="40" Width="100px"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Dated :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtImpLiceDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="41"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtImpLiceDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnImpLiceDate" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtImpLiceDate" PopupButtonID="btnImpLiceDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator9" runat="server" ControlExtender="MaskedEditExtender2"
                                ValidationGroup="dtVal" ControlToValidate="txtImpLiceDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">O.G.L :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtOGL" runat="server" CssClass="textBox" MaxLength="20" TabIndex="41"
                                Width="90px" Text="CHAPTER 2 PARA 2.1"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Currency :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:DropDownList ID="dropDownListCurrency" runat="server" CssClass="dropdownList"
                                TabIndex="42">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtcurr" runat="server" AutoPostBack="true" CssClass="textBox" 
                                Width="50px" ontextchanged="txtcurr_TextChanged"></asp:TextBox>--%>
                            &nbsp;&nbsp;<asp:Button ID="btncurrList" runat="server" CssClass="btnHelp_enabled"
                                TabIndex="-1" />
                                <%--<asp:Label ID="lblcurr" runat="server" CssClass="elementLabel"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Bill Amount :</span>
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="txtBillAmount" runat="server" CssClass="textBox" MaxLength="20"
                                TabIndex="44" Width="100px"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Corresp. Chrgs :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtCorrespChrgs" runat="server" CssClass="textBox" MaxLength="20"
                                TabIndex="45" Width="90px"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Interest Amount :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtInterestAmt" runat="server" CssClass="textBox" MaxLength="20"
                                TabIndex="46" Width="90px"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Maturity Date :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtMaturityDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="47"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdMaturityDate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtMaturityDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDTMaturity" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calMaturity" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtMaturityDate" PopupButtonID="btnDTMaturity" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" ControlExtender="mdMaturityDate"
                                ValidationGroup="dtVal" ControlToValidate="txtMaturityDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Overseas Bank Ref. :</span>
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="txtOverseasBankRef" AutoPostBack="true"  runat="server" 
                                CssClass="textBox" MaxLength="50"
                                TabIndex="48" Width="100px" ontextchanged="txtOverseasBankRef_TextChanged"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Terms :</span>
                        </td>
                        <td nowrap align="left">
                            <asp:TextBox ID="txtTerms" runat="server" CssClass="textBox" MaxLength="50" TabIndex="49"
                                Width="90px"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Tenor :</span>
                        </td>
                        <td nowrap colspan="2" align="left">
                            <asp:TextBox ID="txtTenor" runat="server" CssClass="textBox" MaxLength="50" TabIndex="50"
                                Width="170px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Docs Rcvd Bank :</span>
                        </td>
                        <td nowrap colspan="6" align="left">
                            <asp:TextBox ID="txtDocsRcvdBank" runat="server" AutoPostBack="True" CssClass="textBox"
                                MaxLength="5" OnTextChanged="txtDocsRcvdBank_TextChanged" onkeydown="OpenDocsRcvdBankList(this);"
                                TabIndex="51" Width="70px" ToolTip="Press F2 for help."></asp:TextBox><asp:Button
                                    ID="btnDocsRcvdBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                        ID="lblDocsRcvdBankDesc" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                            &nbsp;
                            <asp:RadioButton ID="rdbACno_DB" Text="A/C No" CssClass="elementLabel" runat="server"
                                GroupName="DB" Checked="True" TabIndex="51" />
                            <asp:RadioButton ID="rdbSortCode_DB" Text="Sort Code" CssClass="elementLabel" runat="server"
                                GroupName="DB" TabIndex="51" /><asp:RadioButton ID="rdbChipUID_DB" Text="Chip UID"
                                    CssClass="elementLabel" runat="server" GroupName="DB" TabIndex="51" />
                            <asp:RadioButton ID="rdbChipsABAno_DB" Text="Chips ABA No" CssClass="elementLabel"
                                runat="server" GroupName="DB" TabIndex="51" /><asp:RadioButton ID="rdbABAno_DB" Text="ABA No"
                                    CssClass="elementLabel" runat="server" GroupName="DB" TabIndex="51" />
                        </td>
                    </tr>
                    <tr>
                    <td align="right" nowrap>
                                <span class="elementLabel">EDD Check :</span>
                            </td>
                            <td align="left" nowrap colspan="4">
                            <asp:CheckBox ID="chk_EDDChk" CssClass="elementLabel" runat="server" 
                                    AutoPostBack="true" oncheckedchanged="chk_EDDChk_CheckedChanged" 
                                    />
                                <asp:Label CssClass="mandatoryField" ID="lblEDDChk" runat="server" Text="N"></asp:Label>
                                &nbsp;&nbsp; <span class="elementLabel">EDD No. :</span>
                                <asp:TextBox ID="txtEDDNo" runat="server" onfocus="this.select()" CssClass="textBox"
                                    Enabled="false" MaxLength="12" TabIndex="28" Width="100px" Height="14px" Style="text-align: right"></asp:TextBox>
                                &nbsp;<asp:Label ID="lblEX" Text="Ex :" CssClass="elementLabel" runat="server"></asp:Label>
                                <asp:Label ID="lbleddex" Text="2017052018IN" CssClass="mandatoryField" runat="server"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                    <td align="right">
                        <span class="elementLabel">Remarks :</span>
                    </td>
                    <td align="left" colspan="4" nowrap>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox"  style="width:80%"></asp:TextBox>
                    </td>
                    </tr>
                </table>
                <asp:HyperLink ID="hlIntimationLettrClauses" runat="server" NavigateUrl="#" TabIndex="53">Intimation Letter Clauses
                </asp:HyperLink><br />
                <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="panelText"
                    CollapsedSize="0" ExpandedSize="300" Collapsed="True" ExpandControlID="hlIntimationLettrClauses"
                    CollapseControlID="hlIntimationLettrClauses" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="True" ExpandDirection="Vertical" />
                <asp:Panel ID="panelText" runat="server">
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td width="5%" valign="top">
                                <asp:CheckBox ID="chk1" runat="server" CssClass="elementLabel" Text="" TabIndex="54" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCB1" runat="server" CssClass="elementLabel" Text="Please indicate your confirmation that the described document are acceptable to you by signing and returning the attached copy."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:CheckBox ID="chk2" runat="server" CssClass="elementLabel" Text="" TabIndex="55" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCB2" runat="server" CssClass="elementLabel" Text="Documents will be delivered against acceptance/payment of equivalent bill amount plus all other charges."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:CheckBox ID="chk3" runat="server" CssClass="elementLabel" Text="" TabIndex="56" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCB3" runat="server" CssClass="elementLabel" Text="Please pay interest at ________ from ________ till date of retirement."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:CheckBox ID="chk4" runat="server" CssClass="elementLabel" Text="" TabIndex="57" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCB4" runat="server" CssClass="elementLabel" Text="Please submit relevant import licence and form A1 duly completed at the time of retirement."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:CheckBox ID="chk5" runat="server" CssClass="elementLabel" Text="" TabIndex="58" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCB5" runat="server" CssClass="elementLabel" Text="Please advise whether you have booked any contract against this document."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:CheckBox ID="chk6" runat="server" CssClass="elementLabel" Text="" TabIndex="59" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCB6" runat="server" CssClass="elementLabel" Text="Please return the draft duly accepted by the authorised signatory/ies."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td width="20%" nowrap align="right">
                                <span class="elementLabel">Covering Schedule Remarks :</span>
                            </td>
                            <td align="left" nowrap colspan="3">
                                <asp:TextBox ID="txtRemarkCovSchedule" runat="server" CssClass="textBox" MaxLength="150"
                                    TabIndex="60" Width="60%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" nowrap align="right" valign="top">
                                <span class="elementLabel">Discrepancy :</span>
                            </td>
                            <td align="left" nowrap colspan="3">
                                <asp:TextBox ID="txtDiscrepancy" runat="server" CssClass="textBox" MaxLength="254"
                                    TabIndex="62" Width="60%" TextMode="MultiLine" Height="50px"> </asp:TextBox>
                            </td>
                        </tr>
                                 <tr>
                        <td width="20%" nowrap align="right">
                        <span class="elementLabel">Discrepancy Fee :</span>
                        </td>
                        <td align="left" nowrap width="10%">
                        <asp:TextBox ID="txtDiscrepanyfee" runat="server" CssClass="textBoxRight" MaxLength="15" TabIndex="63" Width="100px"></asp:TextBox>
                        </td>
                        <td width="10%" nowrap align="right">
                       <span class="elementLabel">Discrepancy Date :</span>
                        </td>
                        <td align="left">
                        <asp:TextBox ID="txtDiscrepanydate" runat="server" CssClass="textBox" TabIndex="64" MaxLength="10" Width="70px"   ValidationGroup="dtVal"></asp:TextBox>
                         <ajaxToolkit:MaskedEditExtender ID="mdnDisc" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtDiscrepanydate" ErrorTooltipEnabled="True"
                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDiscrepanydate" runat="server" Visible="true" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDiscrepanydate" PopupButtonID="btnDiscrepanydate" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator10" runat="server" ControlExtender="mdnDisc"
                                ValidationGroup="dtVal" ControlToValidate="txtDiscrepanydate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        </tr>
                        <tr>
                            <td width="20%" nowrap align="right">
                                <span class="elementLabel">Register Remarks :</span>
                            </td>
                            <td align="left" nowrap colspan="3">
                                <asp:TextBox ID="txtRegisterRemarks" runat="server" CssClass="textBox" MaxLength="254"
                                    TabIndex="65" Width="45%"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Voucher Remark :</span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtVoucherRemarks" runat="server" CssClass="textBox" MaxLength="254"
                                    TabIndex="66" Width="60%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" nowrap align="right">
                                <span class="elementLabel">MT103 Remark :</span>
                            </td>
                            <td align="left" nowrap colspan="3">
                                <asp:TextBox ID="txtMT103Remarks" runat="server" CssClass="textBox" MaxLength="254"
                                    TabIndex="67" Width="60%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" nowrap align="right">
                                <span class="elementLabel">MT 202 Remark :</span>
                            </td>
                            <td align="left" nowrap colspan="3">
                                <asp:TextBox ID="txtMT202Remarks" runat="server" CssClass="textBox" MaxLength="254"
                                    TabIndex="68" Width="60%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td width="25%">
                        </td>
                        <td align="left">
                        <asp:Button ID="btnProcess" runat="server" Text="Process" CssClass="buttonDefault" 
                                ToolTip="Process" TabIndex="69" onclick="btnProcess_Click" />&nbsp
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="buttonDefault" 
                                ToolTip="Reject" TabIndex="70" onclick="btnReject_Click" />
                            <%--<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                OnClick="btnSave_Click" TabIndex="69" />--%>&nbsp
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                OnClick="btnCancel_Click" TabIndex="71" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {

            var lcAmt = document.getElementById('txtLCAmt');
            if (lcAmt.value == '')
                lcAmt.value = 0;
            lcAmt.value = parseFloat(lcAmt.value).toFixed(2);

            var bChrgs = document.getElementById('txtBankCharges');
            if (bChrgs.value == '')
                bChrgs.value = 0;
            bChrgs.value = parseFloat(bChrgs.value).toFixed(2);

            var sAmt = document.getElementById('txtSTaxAmount');
            if (sAmt.value == '')
                sAmt.value = 0;
            sAmt.value = parseFloat(sAmt.value).toFixed(2);
        }
    </script>
</body>
</html>

