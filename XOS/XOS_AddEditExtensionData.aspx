<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XOS_AddEditExtensionData.aspx.cs"
    Inherits="XOS_XOS_AddEditExtensionData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="Shortcut Icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Scripts/Validations.js"></script>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
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

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

    </script>
    <script type="text/javascript" language="javascript">
        function OpenExtensionNoList() {
            var txtExtensionDocumentNo = document.getElementById('txtExtensionDocumentNo').value;

            open_popup('XOS_ExtensionNoList.aspx?DocNo=' + txtExtensionDocumentNo, 300, 450, 'ExtensionNoList');
            return false;
        }
        function selectExtensionNo(selectedID) {
            var txtExtensionNo = document.getElementById('txtExtensionNo');
            txtExtensionNo.value = selectedID;
            document.getElementById('btnExtensionNo').click();
        }

        function onBlurExtensionNo() {
            txtExtensionNo = document.getElementById('txtExtensionNo');
            hdnPrevExtensionNo = document.getElementById('hdnPrevExtensionNo');
            if (trimAll(txtExtensionNo.value) != "") {
                if (txtExtensionNo.value != hdnPrevExtensionNo.value) {
                    hdnPrevExtensionNo.value = txtExtensionNo.value;
                    document.getElementById('btnExtensionNo').click();
                }

            }
            return true;
        }

        function Calculate() {
            var doc = document;
            var txtExtensionCharges = doc.getElementById('txtExtensionCharges');
            if (txtExtensionCharges.value == "")
                txtExtensionCharges.value = 0;
            txtExtensionCharges.value = parseFloat(txtExtensionCharges.value).toFixed(2);

            var txtSTaxAmount = doc.getElementById('txtSTaxAmount');
            if (txtSTaxAmount.value == "")
                txtSTaxAmount.value = 0;
            txtSTaxAmount.value = parseFloat(txtSTaxAmount.value).toFixed(2);

            var txtTotalDebit = doc.getElementById('txtTotalDebit');
            if (txtTotalDebit.value == "")
                txtTotalDebit.value = 0;
            txtTotalDebit.value = parseFloat(txtTotalDebit.value).toFixed(2);

            var sTax = doc.getElementById('ddlServiceTax');
            var sTaxValue = sTax.options[sTax.selectedIndex].value;

            txtSTaxAmount.value = parseFloat(txtExtensionCharges.value) * (parseFloat(sTaxValue) / 100);

            if (txtSTaxAmount.value == '')
                txtSTaxAmount.value = 0;

            txtSTaxAmount.value = parseFloat(txtSTaxAmount.value).toFixed(2);

            txtTotalDebit.value = parseFloat(txtSTaxAmount.value) + parseFloat(txtExtensionCharges.value);

            return true;
        }

        function ValidateSave() {
            var doc = document;
            var txtExtensionNo = doc.getElementById("txtExtensionNo");
            var txtExtensionDate = doc.getElementById("txtExtensionDate");
            var txtGrantedUpto = doc.getElementById("txtGrantedUpto");
            if (trimAll(txtExtensionNo.value) == '') {
                alert('Enter Extension No.');
                txtExtensionNo.focus();
                return false;
            }
            if (txtExtensionDate.value == '') {
                alert('Enter Extension Date.');
                txtExtensionDate.focus();
                return false;
            }
            if (txtGrantedUpto.value == '') {
                alert('Enter Extension Granted Upto.');
                txtGrantedUpto.focus();
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function CharCount(evnt, textBoxID, labelID, maxLength) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            var doc = document;
            var lblObj = labelID;
            var txtObj = textBoxID;
            var charUsed;
            var charLeft = maxLength;
            labelID.style.display = 'block';
            charUsed = parseFloat(textBoxID.value.length);

            if (charUsed > parseFloat(maxLength) && (charCode != 46 && charCode != 37 && charCode != 38 && charCode != 39 && charCode != 40 && charCode != 8 && charCode != 9 && charCode != 27)) {
                textBoxID.value = textBoxID.value.substring(0, parseFloat(maxLength));
                charUsed = parseFloat(textBoxID.value.length);

            }
            charLeft = charLeft - charUsed;

            labelID.innerText = charLeft + " Characters left.";
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="width: 50%" align="left">
                            <span class="pageLabel">Due Date Extension Data Entry</span>
                        </td>
                        <td style="width: 50%" align="right">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="16" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top" colspan="2">
                            <hr />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnPrevExtensionNo" runat="server" />
                            <asp:Button ID="btnExtensionNo" Style="display: none;" runat="server" OnClick="btnExtensionNo_Click" />
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
                        <td width="20%" align="right">
                            <span class="elementLabel">Bill No. :</span>
                        </td>
                        <td width="10%" align="left">
                            <asp:TextBox ID="txtBillDocumentNo" Width="160px" runat="server" CssClass="textBox"
                                ReadOnly="True" TabIndex="-1"></asp:TextBox>
                        </td>
                        <td width="10%" align="right">
                            <span class="elementLabel">Bill Date :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBillDate" CssClass="textBox" runat="server" Width="70px" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Customer :</span>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtCustomerAcNo" runat="server" Width="100px" CssClass="textBox"
                                TabIndex="-1"></asp:TextBox><asp:Label ID="lblCustomerDescription" CssClass="elementLabel"
                                    runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Overseas Party :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOverseasParty" runat="server" Width="50px" CssClass="textBox"
                                TabIndex="-1"></asp:TextBox><asp:Label ID="lblOverseasPartyDesc" CssClass="elementLabel"
                                    runat="server"></asp:Label>
                        </td>
                        <td width="10%" align="right">
                            <span class="elementLabel">AWB/BL Date :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAWBDate" CssClass="textBox" runat="server" Width="70px" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Bill Currency :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBillCurrency" runat="server" Width="40px" CssClass="textBox"
                                TabIndex="-1"></asp:TextBox><asp:Label ID="lblCurrDesc" CssClass="elementLabel" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Due Date :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBillDueDate" CssClass="textBox" runat="server" Width="70px" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Bill Amount :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBillAmount" runat="server" Width="100px" CssClass="textBox" Style="text-align: right"
                                TabIndex="-1"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Amount Outstanding :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAmountOutstanding" CssClass="textBox" Style="text-align: right"
                                runat="server" Width="100px" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Extension Document No :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtExtensionDocumentNo" Width="160px" runat="server" CssClass="textBox"
                                ReadOnly="True" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Extension No :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtExtensionNo" Width="25px" runat="server" CssClass="textBox" TabIndex="1"
                                MaxLength="2"></asp:TextBox><asp:Button ID="btnExtensionNoList" runat="server" CssClass="btnHelp_enabled"
                                    TabIndex="-1" />
                        </td>
                        <td align="right">
                            <span class="elementLabel">Extension Date :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtExtensionDate" CssClass="textBox" runat="server" Width="70px"
                                TabIndex="-1" ValidationGroup="dtVal"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtExtensionDate" ErrorTooltipEnabled="True"
                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtExtensionDate" PopupButtonID="Button1" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                ValidationGroup="dtVal" ControlToValidate="txtExtensionDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Granting Authority :</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlGrantingAuthority" runat="server" CssClass="elementLabel"
                                TabIndex="2" AutoPostBack="false">
                                <asp:ListItem Text="SELF" Value="SELF"></asp:ListItem>
                                <asp:ListItem Text="AD" Value="AD"></asp:ListItem>
                                <asp:ListItem Text="RBI" Value="RBI"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Granted Upto :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGrantedUpto" CssClass="textBox" runat="server" Width="70px" TabIndex="3"
                                ValidationGroup="dtVal"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtGrantedUpto" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtGrantedUpto" PopupButtonID="Button2" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                ValidationGroup="dtVal" ControlToValidate="txtGrantedUpto" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <span class="elementLabel">Reason for Extension :</span><asp:Label ID="lblCharCountReason"
                                runat="server" CssClass="mandatoryField" Style="display: none;"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtReasonForExtension" runat="server" MaxLength="200" TextMode="MultiLine"
                                CssClass="textBox" Columns="60" Rows="4" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <span class="elementLabel">Allowed Under Criteria :</span><asp:Label ID="lblCharCountCriteria"
                                runat="server" CssClass="mandatoryField" Style="display: none;"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtAllowedUnderCriteria" runat="server" TextMode="MultiLine" CssClass="textBox"
                                Columns="60" Rows="4" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <span class="elementLabel">Remarks :</span><asp:Label ID="lblCharCountRemarks" runat="server"
                                CssClass="mandatoryField" Style="display: none;"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Columns="60" Rows="2"
                                CssClass="textBox" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Annex4 received :</span>
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chkAnnex4recvd" runat="server" TabIndex="7" />
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">RBI Approval Ref No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtRBIApproval" runat="server" CssClass="textBox" Width="85px" MaxLength="10"
                                TabIndex="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Extension Charges :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtExtensionCharges" CssClass="textBox" runat="server" Width="70px"
                                Style="text-align: right" onfocus="this.select()" TabIndex="9" MaxLength="20"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">RBI Approval Date :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtRBIApprovalDate" CssClass="textBox" runat="server" Width="70px"
                                MaxLength="10" TabIndex="10"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtRBIApprovalDate" ErrorTooltipEnabled="True"
                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="Button3" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtRBIApprovalDate" PopupButtonID="Button3" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                ValidationGroup="dtVal" ControlToValidate="txtRBIApprovalDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Service Tax(%)</span>
                            <asp:DropDownList ID="ddlServiceTax" runat="server" CssClass="dropdownList" Width="60px"
                                TabIndex="11">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSTaxAmount" runat="server" CssClass="textBox" Style="text-align: right"
                                Width="70px" onfocus="this.select()" MaxLength="20" TabIndex="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Total Debit :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTotalDebit" CssClass="textBox" runat="server" Width="70px" Style="text-align: right"
                                onfocus="this.select()" TabIndex="13" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" colspan="3">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                OnClick="btnSave_Click" TabIndex="14" />&nbsp
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                OnClick="btnCancel_Click" TabIndex="15" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            var doc = document;
            var txtBillAmount = doc.getElementById('txtBillAmount');
            if (txtBillAmount.value == "")
                txtBillAmount.value = 0;
            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);

            var txtAmountOutstanding = doc.getElementById('txtAmountOutstanding');
            if (txtAmountOutstanding.value == "")
                txtAmountOutstanding.value = 0;
            txtAmountOutstanding.value = parseFloat(txtAmountOutstanding.value).toFixed(2);

            var txtExtensionCharges = doc.getElementById('txtExtensionCharges');
            if (txtExtensionCharges.value == "")
                txtExtensionCharges.value = 0;
            txtExtensionCharges.value = parseFloat(txtExtensionCharges.value).toFixed(2);

            var txtSTaxAmount = doc.getElementById('txtSTaxAmount');
            if (txtSTaxAmount.value == "")
                txtSTaxAmount.value = 0;
            txtSTaxAmount.value = parseFloat(txtSTaxAmount.value).toFixed(2);

            var txtTotalDebit = doc.getElementById('txtTotalDebit');
            if (txtTotalDebit.value == "")
                txtTotalDebit.value = 0;
            txtTotalDebit.value = parseFloat(txtTotalDebit.value).toFixed(2);

        }
    </script>
</body>
</html>
