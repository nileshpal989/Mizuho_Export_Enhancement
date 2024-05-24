<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDPMS_ManualBOEAddEdit.aspx.cs"
    Inherits="IDPMS_IDPMS_ManualBOEAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function Party_Help() {
            open_popup('../HelpForms/Help_OverseasPartyCode.aspx', 400, 400, 'DocFile');
            return true;
        }

        function SelectOverSeas(Uname) {
            document.getElementById('txtPartyID').value = Uname;
            document.getElementById('txtPartyID').focus();
        }

        function Cust_Help() {
            var txtPartyID = document.getElementById("txtPartyID");
            var txtBranch = document.getElementById("txtBranch");
            open_popup('../Manual_Cust_Help.aspx?CustID=' + txtPartyID.value + "&Branch=" + txtBranch.value, 450, 550, 'CustFile');
            return false;
        }

        function selectCustomer(Uname) {
            document.getElementById('txtPartyID').value = Uname;
            document.getElementById('txtPartyID').focus();
            __doPostBack('txtPartyID', '');
        }


        function PortHelp() {
            open_popup('../HelpForms/Help_GRCustoms_Port_Code.aspx', 400, 400, 'DocFile');
            return false;
        }

        function selectPort(Uname) {
            document.getElementById('txtportchgs').value = Uname;
            document.getElementById('txtportchgs').focus();
        }

        function openCurrency() {
            open_popup('../HelpForms/TF_CurrencyLookUp1.aspx', 400, 400, 'Currency');
            return false;
        }
        function selectCurrency(Uname) {
            document.getElementById('txtinvcCur').value = Uname;
            document.getElementById('txtinvcCur').focus();
        }

        function ValidateSave() {
            var PartyCode = document.getElementById('txtOverseasParty');
            var PartyAdd = document.getElementById('txtadd');
            var PartyCon = document.getElementById('txtPartyCon');
            var txtbilldate = document.getElementById('txtbilldate');
            var txtportchgs = document.getElementById('txtportchgs');
            var txtshipmentport = document.getElementById('txtshipmentport');

            var PartyID = document.getElementById('txtPartyID');
            var BilNo = document.getElementById('txtbillno');

            if (PartyID.value == '') {
                alert('Customer A/C No Can not be blank.');
                PartyID.focus();
                return false;
            }
            if (PartyCode.value == '') {
                alert('Overseas Party Name Can not be blank.');
                PartyCode.focus();
                return false;
            }
            if (PartyAdd.value == '') {
                alert('Overseas Party Address Can not be blank.');
                PartyAdd.focus();
                return false;
            }
            if (PartyCon.value == '') {
                alert('Overseas Party Country Can not be blank.');
                PartyCon.focus();
                return false;
            }
            if (BilNo.value == '') {
                alert('Bill of Entry No Can not be blank.');
                BilNo.focus();
                return false;
            }
            if (txtbilldate.value == '') {
                alert('Bill of Entry Date Can not be blank.');
                txtbilldate.focus();
                return false;
            }
            if (txtportchgs.value == '') {
                alert('Port of Discharge Can not be blank.');
                txtportchgs.focus();
                return false;
            }
            if (txtshipmentport.value == '') {
                alert('Port of Shipment Can not be blank.');
                txtshipmentport.focus();
                return false;
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
            //            if (document.getElementById('txtInvoiceAmt').value == '') {
            //                alert('Invoice Amt. Can not be blank.');
            //                document.getElementById('txtInvoiceAmt').focus();
            //                return false;
            //            }

            //            if (document.getElementById('ddlPortCodeGrid').value == '0') {
            //                alert('Please Select Port Code.');
            //                document.getElementById('ddlPortCodeGrid').focus();
            //                return false;
            //            }

            //return true;
        }


        function HelpShippNo() {

            if (document.getElementById('txtDocumentNo').value == '') {
                alert('Document Number Can not be blank');
                return false;
            }

            var DocNo = document.getElementById('txtDocumentNo').value;
            open_popup('Help_Shipp_No.aspx?DocNo=' + DocNo, 400, 400, 'DocFile');
            return false;
        }

        function selectShippNo(ShippNo, ShipAmt, ShippDate) {
            document.getElementById('txtShippingBillNo').value = ShippNo;
            document.getElementById('txtShippingBillDate').value = ShippDate;
            document.getElementById('txtShipBillAmt').value = ShipAmt;
        }

        function HelpDocNo() {
            var Branch = document.getElementById('hdnBranch').value;
            var Year = document.getElementById('hdnYear').value;

            open_popup('../HelpForms/HelpBOEDocNo.aspx?Branch=' + Branch + '&Year=' + Year, 400, 400, 'DocFile');


            return true;
        }
        //        function selectDocNo(DocNo, BillAmt, Curr) {
        //            document.getElementById('txtDocumentNo').value = DocNo;
        //            document.getElementById('txtBillAmount').value = BillAmt;
        //            document.getElementById('lblCurrency').innerHTML = Curr;

        //            __doPostBack('txtDocumentNo', '');

        //        }
        function selectAccount(DocNo, BillNo, PortCode, BillDate) {
            document.getElementById('txtDocumentNo').value = DocNo;
            document.getElementById('txtbillno').value = BillNo;
            document.getElementById('txtportchgs').value = PortCode;
            document.getElementById('txtbilldate').value = BillDate;
            //document.getElementById('txtBillAmount').value = BillAmt;
            //document.getElementById('lblCurrency').innerHTML = Curr;

            __doPostBack('txtDocumentNo', '');

        }



        function OverseasPartyHelp() {

            var search = document.getElementById('txtOverseasParty').value;
            open_popup('../IDPMS/TF_OverseasPartyLookUp.aspx', 400, 400, 'Help Overseas Party');
            return true;
        }

        function selectOverParty(PartyCode, Address, Country) {
            document.getElementById('txtOverseasParty').value = PartyCode;
            document.getElementById('txtadd').value = Address;
            document.getElementById('txtPartyCon').value = Country;
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

                //              if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        function gridClicked(sr) {
            var id = sr;

            document.getElementById('hdnGridValues').value = id;
            document.getElementById('btnGridValues').click();
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
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <input type="hidden" id="hdnGridValues" runat="server" />
                            <input type="hidden" id="hdnGRtotalAmount" runat="server" />
                            <input type="hidden" id="hdnBillType" runat="server" />
                            <asp:Button ID="btnGridValues" Style="display: none;" runat="server" OnClick="btnGridValues_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Bill Of Entry - Manual Port Data Entry</strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="20" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="1" cellspacing="2" border="0" width="100%">
                                <tr>
                                    <td width="5%" align="right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Branch :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtBranch" Width="70px" runat="server" CssClass="textBox" Enabled="false"
                                            TabIndex="-1" AutoPostBack="true"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="5%" align="right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Doc No :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <%--<asp:TextBox ID="txtDocPrFx" runat="server" CssClass="textBox" Style="width: 30px;"
                                            ReadOnly="True"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtDocumentNo" Enabled="false" runat="server" CssClass="textBox"
                                            Style="width: 105px;"></asp:TextBox>
                                        <%--<asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Style="width: 40px;"
                                            AutoPostBack="true" MaxLength="4"></asp:TextBox>--%>
                                        <asp:Button ID="btnHelp_DocNo" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Document Date :</span>
                                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                            Width="70"></asp:TextBox>
                                        <asp:ImageButton ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                            TabIndex="-1" />
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtDocDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdfdate"
                                            ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                        </ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Customer A/C No. :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtPartyID" runat="server" Width="100px" TabIndex="1" CssClass="textBox"
                                            AutoPostBack="True" OnTextChanged="txtPartyID_TextChanged"></asp:TextBox>
                                        <asp:Button ID="btnPartyID" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        <asp:Label ID="lblcustnm" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;
                                    </td>
                                    <td align="right" nowrap>
                                        <span class="elementLabel"><span class="mandatoryField">* </span>O.Party Name :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtOverseasParty" onfocus="this.select()" runat="server" CssClass="textBox"
                                            TabIndex="2" Height="14px" Width="150px" MaxLength="50" AutoPostBack="true"></asp:TextBox>
                                        <%--<asp:Button ID="btnOverseasParty" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="lblOverPartyDesc" runat="server" CssClass="elementLabel" Width="80px"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr id="orm" runat="server" visible="false">
                                    <td width="5%" align="right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>ORM No :</span>
                                    </td>
                                    <td align="left" nowrap class="style1">
                                        <asp:TextBox ID="txtdocno" Width="140px" runat="server" CssClass="textBox" Enabled="false"
                                            TabIndex="3" AutoPostBack="true"> </asp:TextBox>&nbsp;
                                        <asp:Button ID="Button2" TabIndex="-1" runat="server" CssClass="btnHelp_enabled"
                                            Visible="false" />&nbsp;
                                        <asp:Label ID="ORMamt" runat="server" CssClass="elementLabel" Text="ORM Amount:"></asp:Label>
                                        <asp:Label ID="lblcurren" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;
                                        <asp:Label ID="lblOrmAmount" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="5%" nowrap align="right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Import Agency :</span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddImpAgency" runat="server" CssClass="dropdownList" TabIndex="4"
                                            Width="150px">
                                            <asp:ListItem Value="1">Customs</asp:ListItem>
                                            <asp:ListItem Value="2">SEZ</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" nowrap>
                                        <span class="elementLabel"><span class="mandatoryField">* </span>O.Party Address :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtadd" TabIndex="5" runat="server" CssClass="textBox" Height="14px"
                                            Width="200px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp; <span class="elementLabel"><span class="mandatoryField">*</span>O.Party
                                            Country :</span>
                                        <asp:TextBox ID="txtPartyCon" TabIndex="6" runat="server" CssClass="textBox" Height="14px"
                                            Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Bill Of Entry No.:</span>
                                    </td>
                                    <td width="5%" align="left">
                                        <asp:TextBox ID="txtbillno" runat="server" CssClass="textBox" Width="100px" TabIndex="6"></asp:TextBox>
                                    </td>
                                    <td nowrap width="5%">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Bill Of Entry Date
                                            :</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtbilldate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                            Width="70" TabIndex="8"></asp:TextBox>
                                        <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                            TabIndex="-1" />
                                        <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                            TargetControlID="txtbilldate" InputDirection="RightToLeft" AcceptNegative="Left"
                                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtbilldate" PopupButtonID="btncalendar_FromDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                            ValidationGroup="dtVal" ControlToValidate="txtbilldate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                        </ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Port Of Discharge :</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtportchgs" runat="server" Width="100px" CssClass="textBox" TabIndex="9"></asp:TextBox>
                                        <asp:Button ID="btnportdschrg" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                    </td>
                                    <td nowrap align="right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Shipment Port :</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtshipmentport" runat="server" Width="100px" CssClass="textBox"
                                            TabIndex="10"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <right>
                                <table cellspacing="0" border="1" width="80%">
                                    <tr>
                                        <td colspan="6" align="right">
                                            <span class="mandatoryField">Please ensure to click on <b style="color: #007DFB;">Add</b>
                                                button before you Save the Bill record.</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="70px" nowrap align="center">
                                            <span class="elementLabel"> <span class="mandatoryField">* </span>Invoice Sr No</span>
                                        </td>
                                        <td nowrap align="center">
                                            <span class="elementLabel"><span class="mandatoryField">* </span>Invoice Term</span>
                                        </td>
                                        <td nowrap>
                                            <span class="elementLabel"><span class="mandatoryField">* </span>Invoice Number</span>
                                        </td>
                                        <td nowrap width="5%">
                                            <span class="elementLabel"><span class="mandatoryField">* </span>Invoice Currency</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel"><span class="mandatoryField">* </span>Invoice Amount</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" width="5%">
                                            <asp:TextBox ID="txtinvocesrNo" runat="server" Width="60px" TabIndex="11" MaxLength="30"
                                                CssClass="textBox" ></asp:TextBox>
                                            <%--<asp:Button ID="Help_btnBillNo" runat="server" CssClass="btnHelp_enabled" Visible="false" />--%>
                                        </td>
                                        <td align="center" width="5%">
                                            <asp:TextBox ID="txtinvoiceterm" runat="server" Width="70px" TabIndex="12" CssClass="textBox"
                                                ></asp:TextBox>
                                        </td>
                                        <td align="center" width="5%">
                                            <asp:TextBox ID="txtinvcno" runat="server" Width="80px" TabIndex="13" CssClass="textBox"
                                                Style="text-align: right" ></asp:TextBox>
                                        </td>
                                        <td align="center" style="margin-left: 40px">
                                            <asp:TextBox ID="txtinvcCur" runat="server" Width="80px" TabIndex="14" CssClass="textBox"
                                                Style="text-align: right" ></asp:TextBox>
                                            <asp:Button ID="Button1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                        <td align="center" nowrap style="margin-left: 40px">
                                            <asp:TextBox ID="txtinvcamt" runat="server" Width="90px" TabIndex="15" CssClass="textBox"
                                                Style="text-align: right" ></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnAddGRPPCustoms" runat="server" Text="Add" CssClass="buttonDefault"
                                                TabIndex="16" OnClick="btnAddGRPPCustoms_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="80%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewEDPMS_Bill_Details" runat="server" AutoGenerateColumns="false"
                                                Width="80%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewEDPMS_Bill_Details_RowDataBound">
                                                <PagerSettings Visible="true" />
                                                <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                    CssClass="gridAlternateItem" />
                                                <Columns>
                                                   <%-- <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SrNo") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Invoice Serial No." HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvoiesrno" runat="server" Text='<%# Eval("InvoiceSerialNo") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Term" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvcterm" runat="server" Text='<%# Eval("TermsofInvoice") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Number." HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvcno" runat="server" Text='<%# Eval("InvoiceNo") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Currency" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvcCur" runat="server" Text='<%# Eval("remittanceCurrency") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Amount" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvcamt" runat="server" Text='<%# Eval("invoiceAmt") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                    </asp:TemplateField>                                                    
                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 110px">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="17" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="18" OnClick="btnCancel_Click" />
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
