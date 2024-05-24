<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_Settlement.aspx.cs"
    Inherits="ImportWareHousing_Transactions_TF_IMPWH_Settlement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-3.3.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .GridView th
        {
            font-size: 8pt;
        }
    </style>
    <script type="text/javascript">
        function Cust_Help(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 13 || e == 'mouseClick') {
                var e = document.getElementById("ddlBranch");
                var branch = e.value;
                var year = document.getElementById('txtYear').value;
                open_popup('../HelpForms/TF_IMPWH_ImportCustomer.aspx?branch=' + branch + '&year=' + year, 500, 500, 'CustList');
                return false;
            }
        }

        function selectCustomer(acno, name) {

            document.getElementById('txtPartyID').value = acno;
            document.getElementById('lblCustName').innerText = name;
            // document.getElementById('txt_custacno').focus();
            //javascript: setTimeout('__doPostBack(\'txtPartyID\',\'\')', 0)
            __doPostBack("txtPartyID", "TextChanged");

        }


        function Cust_Help_New() {
            var e = document.getElementById("ddlBranch");
            var branch = e.value;
            var year = document.getElementById('txtYear').value;
            open_popup('../HelpForms/TF_CustomerLookUpPE.aspx?branch=' + branch + '&year=' + year, 450, 550, 'CustList');
            return false;
        }

        function selectCustomerNew(acno, name) {

            document.getElementById('txtThirdPartyID').value = acno;
            document.getElementById('lblThirdPartyName').innerText = name;
            // document.getElementById('txt_custacno').focus();
            //__doPostBack("txtPartyID", "TextChanged");
            javascript: setTimeout('__doPostBack(\'txtThirdPartyID\',\'\')', 0)

        }
        function OTT_Help() {

            if (document.getElementById('txtPartyID').value == '') {
                alert('Customer ID Can not be blank.');
                document.getElementById('txtPartyID').focus();
                return false;
            }
            if (document.getElementById('txtbillno').value == '') {
                alert('Please Select Bill Of Entry No.');
                document.getElementById('txtbillno').focus();
                return false;
            }
            var e = document.getElementById("ddlBranch");
            var branch = e.options[e.selectedIndex].text;
            //var year = document.getElementById('txtYear').value;
            var iecode = document.getElementById('txtPartyID').value;

            open_popup('../../IDPMS/OTT_Help.aspx?branch=' + branch + '&iecode=' + iecode, 500, 600, 'CustList');
            return false;
        }
        function selectOtt(acno, date, amt, curr) {
            document.getElementById('txtORMNo').value = acno;
            document.getElementById('lblOrmAmount').innerHTML = amt;
            document.getElementById('lblcurren').innerHTML = curr;
            //javascript: setTimeout('__doPostBack(\'txtORMNo\',\'\')', 0)
            __doPostBack('txtORMNo', '');
        }

        function Dump_Help() {

            if (document.getElementById('ThirdPartyCB').checked == true) {
                var iecode = document.getElementById('txtThirdPartyID');

                if (iecode.value == '') {
                    alert('Party ID Can not be blank.');
                    iecode.focus();
                    return false;
                }
            }
            else if (document.getElementById('txtPartyID').value == '') {
                alert('Customer ID Can not be blank.');
                document.getElementById('txtPartyID').focus();
                return false;
            }

            if (document.getElementById('ThirdPartyCB').checked == true) {
                var iecode = document.getElementById('txtThirdPartyID').value;
            }
            else {
                var iecode = document.getElementById('txtPartyID').value;
            }
            var e = document.getElementById("ddlBranch");
            var branch = e.options[e.selectedIndex].text;
            //var year = document.getElementById('txtYear').value;

            open_popup('../Helpforms/TF_IMPWH_PaymentHelp.aspx?branch=' + branch + '&iecode=' + iecode, 450, 600, 'CustList');
            return false;
        }
        function selectDump(acno, date, prtcd, cur, amt) {

            document.getElementById('txtbillno').value = acno;
            document.getElementById('txtbilldate').value = date;
            document.getElementById('txtprtcd').value = prtcd;
            document.getElementById('lblboecur').innerHTML = cur;
            document.getElementById('lblboeamt').innerHTML = amt;
            //javascript: setTimeout('__doPostBack(\'txtbillno\',\'\')', 0)
            __doPostBack('txtbillno', '');
        }

        function calculateDate(d1) {
            var date1 = new Date(d1);
            var date2 = new Date();
            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
            var diffDays = parseInt(timeDiff / (24 * 60 * 60 * 1000), 10);
            return diffDays;
        }

        function Amt() {
            var txtinvcamt = document.getElementById('txtinvcamt');

            if (txtinvcamt.value == '') {
                txtinvcamt.value = 0;
            }
            else {
                txtinvcamt.value = parseFloat(txtinvcamt.value).toFixed(2);
            }
        }

        function ValidateAdd() {
            var InvSrNo = document.getElementById('txtinvocesrNo');
            var InvTerm = document.getElementById('txtinvoiceterm');
            var InvNo = document.getElementById('txtinvcno');
            var InvCur = document.getElementById('txtinvcCur');
            var InvAmt = document.getElementById('txtinvcamt');
            if (InvSrNo.value == '') {
                alert('Invoice Sr No. Can not be blank.');
                InvSrNo.focus();
                return false;
            }
            if (InvTerm.value == '') {
                alert('Invoice Term Can not be blank.');
                InvTerm.focus();
                return false;
            }
            if (InvNo.value == '') {
                alert('Invoice Number Can not be blank.');
                InvNo.focus();
                return false;
            }
            if (InvCur.value == '') {
                alert('Invoice Currency Can not be blank.');
                InvCur.focus();
                return false;
            }
            if (InvAmt.value == '') {
                alert('Invoice Amount Can not be blank.');
                InvAmt.focus();
                return false;
            }
        }


        function ValidateSave() {
            var cust = document.getElementById('txtPartyID');
            var orm = document.getElementById('txtORMNo');
            var bill = document.getElementById('txtbillno');

            if (cust.value == '') {
                alert('Customer ID Can not be blank.');
                cust.focus();
                return false;
            }
            if (orm.value == '') {
                alert('ORM No. Can not be blank.');
                orm.focus();
                return false;
            }
            if (bill.value == '') {
                alert('Bill No. Can not be blank.');
                bill.focus();
                return false;
            }
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }



        function Calculate() {
            var sum = 0;
            $('#GridViewEDPMS_Bill_Details tr').not(':first').each(function () {

                sum += parseFloat($(this).find('td:nth-child(8)').find("[type='text']").val());
            });
            $("#lblTotPayAmt").text(sum);
        }

        function Calculate2() {

            var sum = 0;
            $('#GridViewEDPMS_Bill_Details tr').not(':first').each(function () {

                sum += parseFloat($(this).find('td:nth-child(6)').find("[type='text']").val());
            });
            $("#lblTot").text(sum);
        }


        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <input type="hidden" id="hdnGridValues" runat="server" />
                            <input type="hidden" id="hdnGRtotalAmount" runat="server" />
                            <input type="hidden" id="hdnBillType" runat="server" />
                            <%--<asp:Button ID="btnGridValues" Style="display: none;" runat="server" OnClick="btnGridValues_Click"/>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Payment Settlement ORM Reference</strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Style="visibility: hidden;" Text="Back" CssClass="buttonDefault"
                                ToolTip="Back" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: left; vertical-align: top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="1" cellspacing="2" border="0" width="100%">
                                <tr>
                                    <td width="5%" align="right">
                                        <span class="elementLabel">Branch :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap" colspan="7">
                                        <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" Width="100px"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <span class="mandatoryField">*</span> <span class="elementLabel">Document No : </span>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%; text-align: left">
                                        <asp:TextBox ID="txtDocPrFx" runat="server" CssClass="textBox" Style="width: 30px;"
                                            ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" Style="width: 60px;"
                                            MaxLength="6"></asp:TextBox>
                                        <asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Style="width: 40px;"
                                            MaxLength="4"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; text-align: right; width: 5%">
                                        <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Document Date
                                            :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap" colspan="5">
                                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                            Width="70"></asp:TextBox>
                                        <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                            TabIndex="-1" />
                                        <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                            TargetControlID="txtDocDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                            ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                        </ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Customer A/C No. :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap" colspan="7">
                                        <asp:TextBox ID="txtPartyID" runat="server" Width="110px" TabIndex="1" CssClass="textBox"
                                            AutoPostBack="true" OnTextChanged="txtPartyID_TextChanged"></asp:TextBox>
                                        <asp:Button ID="btnPartyID" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap" align="right">
                                        &nbsp;<span class="elementLabel"><span class="mandatoryField">* </span>Bill Of Entry
                                            No.:</span>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left">
                                        <asp:TextBox ID="txtbillno" runat="server" CssClass="textBox" Width="100px" TabIndex="3"
                                            AutoPostBack="true" OnTextChanged="txtbillno_TextChanged"></asp:TextBox>
                                        <asp:Button ID="btnBillEntryNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        <asp:Label ID="lblboecur" runat="server" CssClass="elementLabel"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblboeamt" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap; text-align: right">
                                        <span class="elementLabel">Bill Of Entry Date :</span>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left; width: 10%">
                                        <asp:TextBox ID="txtbilldate" Enabled="false" runat="server" CssClass="textBox" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70" TabIndex="4"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; text-align: right; width: 5%">
                                        <span class="elementLabel">Port Code :</span>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left; width: 5%">
                                        <asp:TextBox ID="txtprtcd" Enabled="false" runat="server" CssClass="textBox" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70" TabIndex="5"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left" colspan="2">
                                        <a runat="server" id="lblLink" target="_blank" class="elementLink"></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>ORM No :</span>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left" colspan="7">
                                        <asp:TextBox ID="txtORMNo" Width="140px" runat="server" CssClass="textBox" TabIndex="2"
                                            AutoPostBack="true" OnTextChanged="txtORMNo_TextChanged"> </asp:TextBox>
                                        <asp:Button ID="btnHelp_DocNo" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                        &nbsp;
                                        <asp:Label ID="ORMamt" runat="server" CssClass="elementLabel" Text="ORM Amount:"></asp:Label>
                                        <asp:Label ID="lblcurren" runat="server" CssClass="elementLabel"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblOrmAmount" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Exchange Rate :</span>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left">
                                        <asp:TextBox ID="txtExchangeRate" Width="50px" runat="server" CssClass="textBox"
                                            TabIndex="2"> </asp:TextBox>
                                        <asp:Label ID="lblExchangeCurrency" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left; width: 5%" colspan="6">
                                        <asp:Button ID="btnAddGRPPCustoms" runat="server" Text="Add" CssClass="buttonDefault"
                                            TabIndex="6" OnClick="btnAddGRPPCustoms_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left" colspan="7">
                                        <asp:CheckBox runat="server" ID="ThirdPartyCB" Text="Third Party" CssClass="elementLabel"
                                            OnCheckedChanged="ThirdPartyCB_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr id="ThirdPartyTR" runat="server" visible="false">
                                    <td style="text-align: right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Party IE Code :</span>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left" colspan="7">
                                        <asp:TextBox ID="txtThirdPartyID" runat="server" Width="90px" TabIndex="1" CssClass="textBox"></asp:TextBox>
                                        <asp:Button ID="btnThirdPartyID" Style="visibility: hidden;" runat="server" CssClass="btnHelp_enabled"
                                            TabIndex="-1" />
                                        <asp:Label ID="lblThirdPartyName" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table style="width: 80%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridViewEDPMS_Bill_Details" runat="server" AutoGenerateColumns="false"
                                            CssClass="GridView" Width="100%" GridLines="Both" AllowPaging="false" OnRowDataBound="GridViewEDPMS_Bill_Details_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Bill Entry No." HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbillno" runat="server" Text='<%# Eval("Bill_Entry_No") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Serial No." HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvoiesrno" runat="server" Text='<%# Eval("InvoiceSerialNo") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Term" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvcterm" runat="server" Text='<%# Eval("Terms_Invoice") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Number." HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvcno" runat="server" Text='<%# Eval("InvoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Currency" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvcCur" runat="server" Text='<%# Eval("Invoice_Currency") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Amount" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:TextBox AutoPostBack="true" runat="server" onkeypress="return isNumberKey(event,this)"
                                                            OnTextChanged="lblinvcamt_textchange" CssClass="textBox AlgRgh"
                                                            Text='<%# Eval("InvoiceAmt","{0:0.00}") %>' ID="lblinvcamt" Width="90%" MaxLength="20"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="right" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Curr" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentCurr" runat="server" Text='<%# Eval("PaymentCurr") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:TextBox AutoPostBack="true" runat="server" onkeypress="return isNumberKey(event,this)"
                                                            onchange="return Calculate();" OnTextChanged="txtPayAmt_textchange" CssClass="textBox AlgRgh"
                                                            Text='<%# Eval("PaymentAmount","{0:0.00}") %>' ID="txtPayAmt" Width="90%" MaxLength="20" Enabled="false"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="right" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="All" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="4%"
                                                    ItemStyle-Width="4%">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="HeaderChkAllow" AutoPostBack="true" ToolTip="Select All"
                                                            Text="All" OnCheckedChanged="HeaderChkAllow_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="RowChkAllow" AutoPostBack="true" OnCheckedChanged="RowChkAllow_CheckedChanged" />
                                                        <asp:Label ID="lblSel" runat="server" CssClass="elementLabel" ForeColor="RED" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 57%; text-align: right">
                                        <asp:Label ID="lblTot" runat="server" CssClass="pageLabel" />
                                    </td>
                                    <td style="width: 17%; text-align: right">
                                        <asp:Label ID="lblTotPayAmt" runat="server" CssClass="pageLabel" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                <tr>
                                    <td align="right" style="width: 110px">
                                    </td>
                                    <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                        &nbsp;
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                            TabIndex="13" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                            ToolTip="Cancel" TabIndex="14" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                            Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
                                        <%--<asp:Button ID="btnUpdate" runat="server" TabIndex="-1" style="display:none;" OnClick="update" />--%>
                                        <input type="hidden" id="hdnBranch" runat="server" />
                                        <input type="hidden" id="hdnYear" runat="server" />
                                        <input type="hidden" id="hdnInvoiceAmt" runat="server" />
                                    </td>
                                </tr>
                            </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
