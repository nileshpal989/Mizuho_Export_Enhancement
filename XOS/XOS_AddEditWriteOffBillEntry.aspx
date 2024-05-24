<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XOS_AddEditWriteOffBillEntry.aspx.cs"
    Inherits="XOS_XOS_AddEditWriteOffBillEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE-8" />
    <title>TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Scripts/rightClick.js" type="text/javascript"></script>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language="javascript">
        function OpenExtensionNoList() {
            var DocumentNo = document.getElementById('txtDocumentNo').value;

            open_popup('XOS_WriteOffNoList.aspx?DocNo=' + DocumentNo, 300, 450, 'WriteOffNoList');
            return false;
        }
        function selectExtensionNo(selectedID) {
            var txtWriteOffNo = document.getElementById('txtWriteOffNo');
            txtWriteOffNo.value = selectedID;
            document.getElementById('btnWriteOffNo').click();
        }

        function onBlurExtensionNo() {
            txtDocumentNo = document.getElementById('txtDocumentNo');
            hdnPrevWriteOffNo = document.getElementById('hdnPrevWriteOffNo');
            if (txtDocumentNo.value != "") {
                if (txtDocumentNo.value != hdnPrevExtensionNo.value) {
                    hdnPrevWriteOffNo.value = txtDocumentNo.value;
                    document.getElementById('btnWriteOffNo').click();
                }

            }
            return true;
        }

        function checkSysDate() {

            //var gDate = document.getElementById('txtRemDate');
            var obj = document.getElementById('txtDocDate');
            var day = obj.value.split("/")[0];
            var month = obj.value.split("/")[1];
            var year = obj.value.split("/")[2];

            if ((day < 1 || day > 31) || (month < 1 && month > 12) && (year.length != 4)) {

                alert("Invalid Date Format");
                document.getElementById('txtDocDate').focus();
                return false;
            }

            else {

                var dt = new Date(year, month - 1, day);
                var today = new Date();
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert("Invalid Write Off Date");
                    document.getElementById('txtDocDate').focus();
                    return false;
                }
            }
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
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
    <script language="javascript" type="text/javascript">

        function chkWriteOffAmt() {

            var woamt = document.getElementById('txtWOAmt');
            var balos = document.getElementById('txtOSAmt');

            if (woamt.value == '') {

                woamt.value = 0;
                woamt.value = parseFloat(woamt.value).toFixed(2);
            }

            if (parseFloat(woamt.value) > parseFloat(balos.value)) {

                alert('Write Off amount cannot be greater than Outstanding Amount');
                woamt.value = '';
                document.getElementById('txtWOAmt').focus();
                return false;
            }

            if (woamt.value != "") {

                woamt.value = parseFloat(woamt.value).toFixed(2);
            }
        }

        function calTax() {

            var inramt24 = document.getElementById('txtWriteOffCharges');
            if (inramt24.value == '')
                inramt24.value = 0;
            inramt24.value = parseFloat(inramt24.value).toFixed(2);


            var inramt25 = document.getElementById('txtCourier');
            if (inramt25.value == '')
                inramt25.value = 0;
            inramt25.value = parseFloat(inramt25.value).toFixed(2);


            var inramt26 = document.getElementById('txtOtherCharges');
            if (inramt26.value == '')
                inramt26.value = 0;
            inramt26.value = parseFloat(inramt26.value).toFixed(2);


            var inramt27 = document.getElementById('txtCommission');
            if (inramt27.value == '')
                inramt27.value = 0;
            inramt27.value = parseFloat(inramt27.value).toFixed(2);


            var stax = document.getElementById('ddlServicetax');
            var staxvalue = stax.options[stax.selectedIndex].value;

            var staxamt = document.getElementById('txtServiceTax');
            if (staxamt.value == '')
                staxamt.value = 0;
            staxamt.value = parseFloat(staxamt.value).toFixed(2);

            var iamt24 = 0;
            var iamt25 = 0;
            var iamt26 = 0;
            var iamt27 = 0;

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

            staxamt.value = parseFloat((parseFloat(iamt24) + parseFloat(iamt25) + parseFloat(iamt26) + parseFloat(iamt27)) * (parseFloat(staxvalue) / 100)).toFixed(2);

            document.getElementById('txtServiceTax').value = staxamt.value;
            calTotal();
            return true;
        }

        function chkExrate() {

            var exrate = document.getElementById('txtExchRate');
            if (exrate.value == '') {

                exrate.value = 0;
                exrate.value = parseFloat(exrate.value).toFixed(10);
                document.getElementById('txtExchRate').value = exrate.value;
            }
            if (exrate.value != "") {

                exrate.value = parseFloat(exrate.value).toFixed(10);
                document.getElementById('txtExchRate').value = exrate.value;
            }
        }

        function calTotal() {

            var wocharges = document.getElementById('txtWriteOffCharges');
            var courier = document.getElementById('txtCourier');
            var other = document.getElementById('txtOtherCharges');
            var comm = document.getElementById('txtCommission');
            var staxamt = document.getElementById('txtServiceTax');
            var cal;
            var tot;
            cal = parseFloat(parseFloat(wocharges.value) + parseFloat(courier.value) + parseFloat(other.value) + parseFloat(comm.value) + parseFloat(staxamt.value)).toFixed(2);
            document.getElementById('txtTotalDebit').value = cal;
        }
    
    </script>
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
        <%-- <center>--%>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table border="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel" style="font-weight: bold;">XOS Write Off Data Entry</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="21" CssClass="buttonDefault"
                                ToolTip="Back" OnClick="btnBack_Click" />
                            <input type="hidden" id="hdnPrevWriteOffNo" runat="server" />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <asp:Button ID="btnWriteOffNo" Style="display: none;" runat="server" OnClick="btnExtensionNo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table border="1" cellspacing="0" width="67%"> 
               <%-- bgcolor="#ffc000"--%>
                    <tr>
                        <td>
                            <table border="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="10%" nowrap align="right">
                                        <span class="elementLabel">BillNo :</span>
                                    </td>
                                    <td width="15%" nowrap align="left">
                                        <asp:TextBox ID="txtBillNo" runat="server" Width="150px" CssClass="textBox" Height="14px"
                                            Style="font-weight: bold;" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td width="10%" nowrap align="right">
                                        <span class="elementLabel">Bill Date :</span>
                                    </td>
                                    <td width="10%" nowrap align="left">
                                        <asp:TextBox ID="txtBillDate" runat="server" Width="70px" CssClass="textBox" Height="14px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td width="55%" nowrap>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <span class="elementLabel">CustAcNo :</span>
                                    </td>
                                    <td align="left" nowrap colspan="3">
                                        <asp:TextBox ID="txtCustAcNo" runat="server" Width="100px" CssClass="textBox" Height="14px"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <span class="elementLabel">Overseas Party ID :</span>
                                    </td>
                                    <td align="left" nowrap colspan="2">
                                        <asp:TextBox ID="txtOverseasParty" runat="server" Width="70px" CssClass="textBox"
                                            Height="14px" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblOverseasPartyName" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                    <td width="10%" align="right">
                                        <span class="elementLabel">AWB/BL Date :</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAWBDate" CssClass="textBox" runat="server" Width="70px" TabIndex="-1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Bill Currency :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtBillCurr" runat="server" Width="35px" CssClass="textBox" Height="14px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Bill Amt :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtBillAmt" runat="server" Width="100px" CssClass="textBox" Height="14px"
                                            Enabled="false" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">O/S Amt :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtOSAmt" runat="server" Width="100px" CssClass="textBox" Height="14px"
                                            Enabled="false" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Due Date :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtDueDate" runat="server" Width="70px" CssClass="textBox" Height="14px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="right" width="10%" nowrap>
                            <span class="elementLabel">Document No :</span>
                        </td>
                        <td align="left" width="15%" nowrap>
                            <asp:TextBox ID="txtDocumentNo" runat="server" Width="150px" CssClass="textBox" Height="14px"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" width="10%" nowrap>
                            <span class="elementLabel">Doc Date :</span>
                        </td>
                        <td align="left" width="10%" nowrap>
                            <asp:TextBox ID="txtDocDate" runat="server" Width="70px" CssClass="textBox" Height="14px"
                                TabIndex="1" MaxLength="10" onfocus="this.select()"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdRemdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtDocDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="Mev1" runat="server" ControlExtender="mdRemdate"
                                ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td width="55%" nowrap rowspan="5">
                            <table border="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" width="10%" nowrap>
                                        <span class="elementLabel">W/O Charges :</span>
                                    </td>
                                    <td align="left" width="10%" nowrap>
                                        <asp:TextBox ID="txtWriteOffCharges" runat="server" Width="70px" CssClass="textBox"
                                            Height="14px" TabIndex="14" Style="text-align: right;" onfocus="this.select()"></asp:TextBox>
                                    </td>
                                    <td width="35%" nowrap>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Courier Charges :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtCourier" runat="server" Width="70px" CssClass="textBox" Height="14px"
                                            TabIndex="15" Style="text-align: right;" onfocus="this.select()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Other Charges :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtOtherCharges" runat="server" Width="70px" CssClass="textBox"
                                            Height="14px" TabIndex="16" Style="text-align: right;" onfocus="this.select()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Commission :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtCommission" runat="server" Width="70px" CssClass="textBox" Height="14px"
                                            TabIndex="17" Style="text-align: right;" onfocus="this.select()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">STax</span>
                                        <asp:DropDownList ID="ddlServicetax" runat="server" CssClass="dropdownList" TabIndex="18">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtServiceTax" runat="server" Width="70px" CssClass="textBox" Height="14px"
                                            Enabled="false" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Total Debit :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtTotalDebit" runat="server" Width="70px" CssClass="textBox" Height="14px"
                                            Enabled="false" Style="text-align: right;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">WriteOff No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtWriteOffNo" runat="server" Width="35px" CssClass="textBox" Height="14px"
                                TabIndex="2" MaxLength="2" onfocus="this.select()"></asp:TextBox>
                            <asp:Button ID="btnDocNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">W/O Granting Auth.</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:DropDownList ID="ddlGrantingAuthority" runat="server" CssClass="dropdownList"
                                TabIndex="3">
                                <asp:ListItem>SELF</asp:ListItem>
                                <asp:ListItem>AD</asp:ListItem>
                                <asp:ListItem>RBI</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">W/O Amt :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtWOAmt" runat="server" Width="100px" CssClass="textBox" Height="14px"
                                TabIndex="4" Style="text-align: right;" onfocus="this.select()"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Exch. Rate :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtExchRate" runat="server" Width="100px" CssClass="textBox" Height="14px"
                                TabIndex="5" Style="text-align: right;" onfocus="this.select()"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Aggregate Percent :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtaggregatepercent" runat="server" Width="70px" CssClass="textBox"
                                Height="14px" TabIndex="6" Style="text-align: right;" onfocus="this.select()"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap style="vertical-align: top">
                            <span class="elementLabel">Reason of W/O :</span><asp:Label ID="lblCharCountReason"
                                runat="server" CssClass="mandatoryField" Style="display: none;"></asp:Label>
                        </td>
                        <td align="left" nowrap colspan="3">
                            <asp:TextBox ID="txtReasonWriteOff" runat="server" MaxLength="200" CssClass="textBox"
                                TabIndex="6" Rows="4" Columns="59" TextMode="MultiLine" onfocus="this.select()"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap style="vertical-align: top">
                            <span class="elementLabel">Allowed under Criteria :</span><asp:Label ID="lblCharCountCriteria"
                                runat="server" CssClass="mandatoryField" Style="display: none;"></asp:Label>
                        </td>
                        <td align="left" nowrap colspan="3">
                            <asp:TextBox ID="txtAllowedCriteria" runat="server" MaxLength="200" class="textBox"
                                TabIndex="7" Rows="4" Columns="59" TextMode="MultiLine" onfocus="this.select()"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap style="vertical-align: top">
                            <span class="elementLabel">Remarks :</span><asp:Label ID="lblCharCountRemarks" runat="server"
                                CssClass="mandatoryField" Style="display: none;"></asp:Label>
                        </td>
                        <td align="left" nowrap colspan="3">
                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="200" class="textBox" TabIndex="8"
                                Rows="2" Columns="59" TextMode="MultiLine" onfocus="this.select()"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" nowrap>
                            <%--<span class="elementLabel">Annex4</span>
                                <asp:CheckBox ID="chkAnnex" runat="server" TabIndex="9" />
                                <span class="elementLabel">Y/N</span>--%>
                            &nbsp;
                        </td>
                        <td align="center" nowrap colspan="2">
                            <span class="elementLabel">Annex4</span>
                            <asp:CheckBox ID="chkAnnex" runat="server" TabIndex="9" />
                            <span class="elementLabel">Y/N</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">
                                Proof of Surrender</span>
                            <asp:CheckBox ID="chkProofSurrender" runat="server" TabIndex="10" />
                            <span class="elementLabel">Y/N</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--</td>
                            <td align="left" nowrap>--%>
                            <span class="elementLabel">CA Cert</span>
                            <asp:CheckBox ID="chkCACert" runat="server" TabIndex="11" />
                            <span class="elementLabel">Y/N</span>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">RBI Approval Ref No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtRBIApproval" runat="server" CssClass="textBox" Width="85px" MaxLength="10"
                                TabIndex="12"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">RBI Approval Date :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtRBIApprovalDate" CssClass="textBox" runat="server" Width="70px"
                                MaxLength="10" TabIndex="13"></asp:TextBox>
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="19" CssClass="buttonDefault"
                                ToolTip="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="20" CssClass="buttonDefault"
                                ToolTip="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- </center>--%>
    </div>
    </form>
</body>
</html>
