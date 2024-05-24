<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IDPMS_BOEClosure_Can.aspx.cs"
    Inherits="IDPMS_TF_IDPMS_BOEClosure_Can" %>

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
    <link id="Link2" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link3" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function Cust_Help() {
            //            var e = document.getElementById("ddlBranch");
            //            var branch = e.options[e.selectedIndex].text;
            //            var year = document.getElementById('txtYear').value;
            open_popup('IDPMS_CUST_IECode_help.aspx', 450, 550, 'CustList');
            return false;
        }

        function selectCustomer(acno, name) {

            document.getElementById('txt_iecode').value = acno;
            document.getElementById('lblcust').innerText = name;
            // document.getElementById('txt_custacno').focus();
            //__doPostBack("txtPartyID", "TextChanged");
            javascript: setTimeout('__doPostBack(\'txt_iecode\',\'\')', 0)

        }

        function OpenTTNoList() {

            var custid = document.getElementById('txt_iecode');
            var irmno = document.getElementById('txt_boeno');
            //var year = document.getElementById('hdnyr');


            open_popup('BOENoHelp.aspx?custid=' + custid.value + '&boeno=' + irmno.value, 300, 500, 'TTRefNo');
            return false;

        }

        function saveTTRefDetails(TTRefNo) {

            document.getElementById('txt_boeno').value = TTRefNo;
            //__doPostBack("txt_boeno", "TextChanged");

        }
        function BOECan_Help() {
            //var e = document.getElementById("ddlBranch");
            //var branch = e.options[e.selectedIndex].text;
            //var year = document.getElementById('txtYear').value;
            var IECode = document.getElementById('txt_iecode').value;

            open_popup('../IDPMS/TF_IDPMS_BOEClsrCan_Help.aspx?IECode=' + IECode, 500, 900, 'CustList');
            //open_popup('../IDPMS/Dump_Help.aspx?branch=' + branch + '&iecode=' + iecode, 450, 550, 'CustList');
            return false;
        }
        function selectDump(boeno, boedate, prtcd, InvNo, AdjAmt, AdjDate, AdjRef) {
            document.getElementById('txt_boeno').value = boeno;
            document.getElementById('txtbilldate').value = boedate;
            document.getElementById('txtprtcd').value = prtcd;
            document.getElementById('txt_adjdate').value = AdjDate;
            document.getElementById('txtadjrefno').value = AdjRef;
            javascript: setTimeout('__doPostBack(\'txtadjrefno\',\'\')', 0)
        }
        
    </script>
    <style type="text/css">
        .AlgRgh
        {
            font-family: Verdana, Sans-Serif, Arial;
            font-weight: normal;
            font-size: 8pt;
            border: 1px solid #5970B2;
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" valign="bottom" width="10%" nowrap>
                            <span class="pageLabel"><strong>Bill Of Entry Closure Data Entry</strong> </span>
                        </td>
                        <td width="10%" nowrap>
                            <input type="hidden" id="hdnbranch" runat="server" />
                            <input type="hidden" id="hdnyr" runat="server" />
                        </td>
                        <td width="10%" nowrap>
                            &nbsp;
                        </td>
                        <td width="10%" nowrap>
                            &nbsp;
                        </td>
                        <td width="10%" nowrap>
                            &nbsp;
                        </td>
                        <td align="right" nowrap>
                            <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  "
                                Style="color: red"></asp:Label>
                            &nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="20" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr align="right">
                        <td width="15%" align="right" nowrap>
                            <span class="elementLabel">Branch :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                TabIndex="1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">IE Code:</span>
                        </td>
                        <td align="left" nowrap colspan="1">
                            <asp:TextBox runat="server" CssClass="textBox" ID="txt_iecode" Width="100px" MaxLength="10"
                                OnTextChanged="txt_iecode_TextChanged" Enabled="false" />
                            <asp:Button ID="btncusthelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            <asp:Label ID="lblcust" CssClass="elementLabel" runat="server" Style="text-overflow: ellipsis;
                                overflow: hidden; width: 20px; overflow-y: hidden; height: 37px"></asp:Label>
                        </td>
                        <td align="right">
                            <span class="mandatoryField">*</span><span class="elementLabel">BOE No:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox runat="server" CssClass="textBox" ID="txt_boeno" Width="100px" MaxLength="50"
                                AutoPostBack="true" Enabled="false" />
                            <asp:Button ID="btnboehelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            <asp:Label Text="" ID="lblBalAmt" CssClass="elementLabel" runat="server" />
                            <asp:Label Text="" ID="lblBOECur" CssClass="elementLabel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Port Code:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtprtcd" runat="server" CssClass="textBox" Width="100px" Style="text-align: left"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Bill Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtbilldate" runat="server" CssClass="textBox" Width="100px" Style="text-align: left"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Reason For Closure:</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlreasadj" runat="server" CssClass="dropdownList" Enabled="false"
                                Width="130px">
                                <asp:ListItem Text="Destroy of goods" Value="1" />
                                <asp:ListItem Text="Sort Shipment" Value="2" />
                                <asp:ListItem Text="Quality Issue" Value="3" />
                                <asp:ListItem Text="Re-import" Value="4" />
                                <asp:ListItem Text="Re-export" Value="5" />
                                <asp:ListItem Text="Set-off/Net off" Value="6" />
                                <asp:ListItem Text="Others" Value="7" />
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Closure Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txt_adjdate" runat="server" Enabled="false" CssClass="textBox" Width="80px"
                                MaxLength="10" onfocus="this.select()"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdRemdate1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txt_adjdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate1" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate2" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txt_adjdate" PopupButtonID="btncalendar_DocDate1" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span> <span class="elementLabel">Approved By:</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlapproved" Enabled="false" CssClass="dropdownList"
                                Width="70px">
                                <asp:ListItem Text="RBI" Value="1" />
                                <asp:ListItem Text="AD BANK" Value="2" />
                                <asp:ListItem Text="OTHERS" Value="3" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Letter No:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txt_letterNo" Enabled="false" CssClass="textBox"
                                Width="100px" MaxLength="50" />
                        </td>
                        <td align="right">
                            <span class="elementLabel">Letter Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txt_letterDate" runat="server" Enabled="false" CssClass="textBox"
                                Width="80px" MaxLength="10" onfocus="this.select()"> </asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txt_letterDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate3" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txt_letterDate" PopupButtonID="btncalendar_DocDate3" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Shipping Bill No:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txt_docno" Enabled="false" CssClass="textBox" Width="100px"
                                MaxLength="20" />
                        </td>
                        <td align="right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Shipping Bill Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txt_docdate" runat="server" Enabled="false" CssClass="textBox" Width="80px"
                                MaxLength="10" onfocus="this.select()"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdRemdate2" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txt_docdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate2" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate3" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txt_docdate" PopupButtonID="btncalendar_DocDate2" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Shipping Port:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdocprt" runat="server" Enabled="false" CssClass="textBox" Width="100px"
                                MaxLength="50" />
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Close Of Bill Indicator:</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlboeclose" Enabled="false" CssClass="dropdownList"
                                Width="70px">
                                <asp:ListItem Text="OPEN" Value="1" />
                                <asp:ListItem Text="CLOSE" Selected="true" Value="2" />
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Closure Reference No:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtadjrefno" CssClass="textBox" Width="200px" MaxLength="20"
                                Enabled="false" AutoPostBack="True" OnTextChanged="txtadjrefno_TextChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Remarks:</span>
                        </td>
                        <td colspan="5" align="left">
                            <asp:TextBox runat="server" Enabled="false" CssClass="textBox" ID="txt_remarks" Width="400px"
                                MaxLength="200" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="left">
                            <asp:GridView ID="GridViewInvoice" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="Both" AllowPaging="true" PageSize="20" CssClass="GridView">
                                <PagerSettings Visible="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinsrno" runat="server" Text='<%# Eval("InvoiceSerialNo") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoiceno" runat="server" Text='<%# Eval("InvoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                        <ItemStyle HorizontalAlign="Center" Width="40%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adjusted Inv Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalInvAmt" runat="server" Text='<%# Eval("adjustedValue","{0:0.00}") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="40%" />
                                        <ItemStyle HorizontalAlign="Right" Width="40%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label ID="lblTot" runat="server" CssClass="pageLabel" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="5" style="text-align: left">
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" OnClick="btnSave_Click"
                                Text="Save" ToolTip="Save" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" OnClick="btnCancel_Click"
                                Text="Cancel" ToolTip="Cancel" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
