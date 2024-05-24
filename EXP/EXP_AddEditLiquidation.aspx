<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditLiquidation.aspx.cs" Inherits="EXP_EXP_AddEditLiquidation" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">



        function CalculateNetRate() {
            var libor = document.getElementById('txtLibor');
            var spread = document.getElementById('txtAuthSpread');
            var src = document.getElementById('txtSRC');
            var pcRate = document.getElementById('txtPCRate');

            if (libor.value == '')
                libor.value = 0;
            if (spread.value == '')
                spread.value = 0;
            if (src.value == '')
                src.value = 0;

            pcRate.value = parseFloat(libor.value) + parseFloat(spread.value) + parseFloat(src.value);
        }

        function OpenCustomerCodeList() {

            open_popup('../TF_CustomerLookUp2.aspx', 450, 460, 'CustomerCodeList');
            return false;
        }

        function selectCustomer(selectedID) {

            var id = selectedID;
            document.getElementById('hdnCustomerCode').value = id;
            document.getElementById('btnCustomerCode').click();
        }


        function OpenCurrencyList() {

            open_popup('../TF_CurrencyLookup1.aspx?pc=1', 450, 320, 'CurrencyList');
            return false;
        }

        function selectCurrency(selectedID) {
            var id = selectedID;
            document.getElementById('hdnCurId').value = id;
            document.getElementById('btnCurr').click();

        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }



        function OpenSubACList() {
            var custAcNo = document.getElementById('txtCustAcNo');

            var branch = document.getElementById('txtBranch');
            if (custAcNo.value == '') {
                alert('Please enter Customer A/C No.');
                return false;
            }
            open_popup('../PC/PC_SubAccountNo.aspx?custAcNo=' + custAcNo.value + '&branch=' + branch.value, 450, 450, 'SubAcList');
            return false;
        }
        function selectSubAc(subAcNo, acNo, curr, bal) {

            var txtSubAcNo = document.getElementById('txtSubAcNo');
            var txtPCFC_AcNo = document.getElementById('txtPCFC_AcNo');

            //var ddlCurr = document.getElementById('dropDownListCurrency');

            txtSubAcNo.value = subAcNo;
            txtPCFC_AcNo.value = acNo;
            document.getElementById('hdnBalanceAmt').value = bal;
            //ddlCurr.value = curr;
            document.getElementById('hdnCurId').value = curr;
            document.getElementById('btnCurr').click();
        }


        function CalculateINRamt() {

            var LiquiAmt = document.getElementById('txtLiquidatedAmt');
            var ExchRt = document.getElementById('txtExchRt');
            var INRamt = document.getElementById('txtAmtinINR');

            if (LiquiAmt.value == '')
                LiquiAmt.value = 0;

            if (ExchRt.value == '')
                ExchRt.value = 0;

            INRamt.value = parseFloat(LiquiAmt.value) * parseFloat(ExchRt.value);
        }

        function CheckBalance() {

            var LiquiBal = document.getElementById('txtLiquidatedAmt');
            var Bal = document.getElementById('hdnBalanceAmt');
            var hdnLiqAmt = document.getElementById('hdnLiquiAmt');
            var mode = document.getElementById('hdnMode');

            if (document.getElementById('txtPCFC_AcNo').value == '')
                return false;

            ////            alert("LiquiBal : " + LiquiBal.value);
            ////            alert("Bal : " + Bal.value);
            ////            alert("hdnLiqAmt : " + hdnLiqAmt.value);


            if (mode == "edit") {
                if (parseFloat(LiquiBal.value) > (parseFloat(Bal.value) - parseFloat(hdnLiqAmt.value))) {
                    alert('Liquidated Amount cannot be greater than Balance Amount.');
                    LiquiBal.value = '';
                    LiquiBal.focus();
                    return false;
                }
            }
            if (parseFloat(LiquiBal.value) > Bal.value) {
                alert('Liquidated Amount cannot be greater than Balance Amount.');
                LiquiBal.value = '';
                LiquiBal.focus();
            }
            return true;
        }

        function validateSave() {
            var custAc = document.getElementById('txtCustAcNo');
            var inr = document.getElementById('rbtnIR_INR');
            var exchRate = document.getElementById('txtExchRateforIntRec');

            var inrDebit = document.getElementById('rbtnINR');
            var fContDebit = document.getElementById('rbtnFContract');
            var prinExchRate = document.getElementById('txtExchRt');

            if (custAc.value == '') {
                alert('Enter Customer A/C No.');
                custAc.focus();
                return false;
            }

            var pcfcCustAc = document.getElementById('txtPCFC_AcNo');
            if (pcfcCustAc.value == '') {
                alert('Enter PCFC A/C No.');
                pcfcCustAc.focus();
                return false;
            }

            if (inrDebit.checked == true) {
                if (prinExchRate.value == '') {
                    alert('Enter Principle Exchange Rate');
                    prinExchRate.focus();
                    return false;
                }
            }

            if (fContDebit.checked == true) {
                if (prinExchRate.value == '') {
                    alert('Enter Principle Exchange Rate');
                    prinExchRate.focus();
                    return false;
                }
            }
            if (inr.checked == true) {
                if (exchRate.value == '' || parseFloat(exchRate.value) == 0) {
                    alert('Enter INR Exchange Rate');
                    exchRate.focus();
                    return false;
                }
            }

            return true;
        }

        function checkExchRateforINR() {
            var inr = document.getElementById('rbtnIR_INR');
            var fc = document.getElementById('rbtnIR_FC');
            var exchRate = document.getElementById('txtExchRateforIntRec');
            if (inr.checked == true) {
                exchRate.disabled = false;
                exchRate.focus();
            }
            else {
                exchRate.value = '';
                exchRate.disabled = true;

            }
        }

       
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
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
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Liquidation</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" />
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
                                <input type="hidden" id="hdnCustomerCode" runat="server" />
                                <asp:Button ID="btnCustomerCode" Style="display: none;" runat="server" OnClick="btnCustomerCode_Click" />
                                <input type="hidden" id="hdnCurId" runat="server" />
                                <input type="hidden" id="hdnMode" runat="server" />
                                <input type="hidden" id="hdnBalanceAmt" runat="server" />
                                <input type="hidden" id="hdnLiquiAmt" runat="server" />
                                <asp:Button ID="btnCurr" Style="display: none;" runat="server" OnClick="btnCurr_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right" width="200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" width="250px">
                                            &nbsp;<asp:TextBox ID="txtBranch" runat="server" CssClass="textBox" Width="100px"
                                                TabIndex="-1"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Document No. :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtDocNo" runat="server" CssClass="textBox" MaxLength="20" Width="150px"
                                                TabIndex="-1"></asp:TextBox>
                                        </td>
                                        <td align="right" nowrap>
                                            <span class="elementLabel"></span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtDocType" Visible="false" runat="server" CssClass="textBox" MaxLength="20" Width="50px"
                                                TabIndex="-1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Date of Liquidation :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtLiquidatedDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70px" TabIndex="1"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtLiquidatedDate" ErrorTooltipEnabled="True"
                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtLiquidatedDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtLiquidatedDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Invalid"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Customer A/c No. :</span>
                                        </td>
                                        <td align="left" nowrap colspan="2">
                                            &nbsp;<asp:TextBox ID="txtCustAcNo" runat="server" CssClass="textBox" MaxLength="6" Width="100px"
                                                TabIndex="-1" AutoPostBack="true" OnTextChanged="txtCustAcNo_TextChanged"></asp:TextBox>
                                            <%--<asp:Button ID="btnCustAcList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />--%>
                                            <asp:Label ID="lblCustDescription" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">PCFC A/c No :</span>
                                        </td>
                                        <td align="left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtPCFC_AcNo" runat="server" CssClass="textBox" Width="100px" TabIndex="-1"></asp:TextBox>
                                            <asp:Button ID="btnPcACNO" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Sub A/c No. :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtSubAcNo" runat="server" CssClass="textBox" MaxLength="2" Width="40px"
                                                TabIndex="-1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Currency :</span>
                                        </td>
                                        <td nowrap>
                                            &nbsp;<asp:TextBox ID="txtCurrencyID" runat="server" CssClass="textBox" Width="40px" TabIndex="-1"
                                                Enabled="false" OnTextChanged="txtCurrencyID_TextChanged"></asp:TextBox>
                                            <asp:Label ID="txtCurrencyDescription" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Entry No. :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtEntryNo" runat="server" CssClass="textBox" MaxLength="4"
                                                Width="40px" TabIndex="6" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Liquidated Amount :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtLiquidatedAmt" runat="server" CssClass="textBox"
                                                MaxLength="10" Width="100px" TabIndex="7"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Exch Rate :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtExchRt" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="60px" TabIndex="8" AutoPostBack="true" 
                                                ontextchanged="txtExchRt_TextChanged" ></asp:TextBox>
                                        </td>
                                        <td nowrap align="right" width="100px">
                                            <span class="elementLabel">INR Amount :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtAmtinINR" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="100px" TabIndex="-1" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                        <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Remarks :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtChequeNo" runat="server" CssClass="textBox" Width="200px"
                                                TabIndex="9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Contract No. :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtContractNo" runat="server" CssClass="textBox" Width="100px"
                                                TabIndex="10"></asp:TextBox>
                                        </td>
                                        <td align="right" width="120px" nowrap>
                                            <span class="elementLabel">Proof of Exports :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:CheckBox ID="chkProofofExports" runat="server" Text="Yes / No" CssClass="elementLabel"
                                                TabIndex="11" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Debit Type :</span>
                                        </td>
                                        <td colspan="3">
                                            <asp:RadioButton ID="rbtnNOSTRO" runat="server" CssClass="elementLabel" GroupName="Data"
                                                TabIndex="12" Text="NOSTRO" Checked="true" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnEEFC" runat="server" CssClass="elementLabel" GroupName="Data"
                                                TabIndex="12" Text="EEFC" />&nbsp;&nbsp;<asp:RadioButton ID="rbtnINR" runat="server"
                                                    CssClass="elementLabel" GroupName="Data" TabIndex="12" Text="INR" />
                                            &nbsp;&nbsp;<asp:RadioButton ID="rbtnFContract" runat="server" CssClass="elementLabel"
                                                GroupName="Data" TabIndex="12" Text="F Contract" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Interest Recovery :</span>
                                        </td>
                                        <td colspan="3">
                                            <asp:RadioButton ID="rbtnIR_INR" runat="server" CssClass="elementLabel" GroupName="IntRec"
                                                TabIndex="12" Text="INR" Checked="true" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnIR_FC" runat="server" CssClass="elementLabel" GroupName="IntRec"
                                                TabIndex="12" Text="Foreign Currency" />&nbsp;&nbsp;&nbsp;&nbsp;<span class="elementLabel">INR
                                                    Exch Rate :</span><asp:TextBox ID="txtExchRateforIntRec" runat="server" CssClass="textBox"
                                                        Width="100px" TabIndex="13"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Service Tax on FXDLS :</span>
                                        </td>
                                        <td align="left" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtSTFXamt" runat="server" CssClass="textBox" Width="100px"
                                                TabIndex="-1" MaxLength="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 100px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="13" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="14" OnClick="btnCancel_Click" />
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
</body>
</html>
