<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDPMS_BOE_Setlement_Cancel.aspx.cs"
    Inherits="IDPMS_IDPMS_BOE_Setlement_Cancel" %>

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
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
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
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>IDPMS XML (Payment Settlement Cancellation) File Generation</strong></span>
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
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                <tr>
                                    <td width="5%" align="right">
                                        <span class="elementLabel">Branch :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap">
                                        <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" Width="100px"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="5%" align="right">
                                        <span class="elementLabel">Payment Ref. No. :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap">
                                        <asp:TextBox ID="txtPayrefNo" runat="server" CssClass="textBox" Style="width: 150px;"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <span class="mandatoryField">*</span> <span class="elementLabel">Document No : </span>
                                    </td>
                                    <td align="left" style="white-space: nowrap">
                                        <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" Style="width: 150px;"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="right" style="white-space: nowrap">
                                        <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Document Date
                                            :</span>
                                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                            Width="70"></asp:TextBox>
                                        <asp:ImageButton ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
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
                                        <span class="elementLabel"><span class="mandatoryField">* </span>Customer ID :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap">
                                        <asp:TextBox ID="txtPartyID" runat="server" Width="90px" TabIndex="1" CssClass="textBox"
                                            AutoPostBack="true" Enabled="false" OnTextChanged="txtPartyID_TextChanged"></asp:TextBox>
                                        <%--<asp:Button ID="btnPartyID" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"/>--%>
                                        <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="5%" align="right">
                                        <span class="elementLabel"><span class="mandatoryField">* </span>ORM No :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap">
                                        <asp:TextBox ID="txtORMNo" Width="140px" runat="server" CssClass="textBox" Enabled="false"
                                            TabIndex="2" AutoPostBack="true"> </asp:TextBox>
                                        &nbsp;
                                        <asp:Button ID="btnHelp_DocNo" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                        &nbsp;
                                        <asp:Label ID="ORMamt" runat="server" CssClass="elementLabel" Text="ORM Currency:"></asp:Label>
                                        <asp:Label ID="lblcurren" runat="server" CssClass="elementLabel"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblOrmAmount" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox runat="server" ID="ThirdPartyCB" Text="Third Party" CssClass="elementLabel"
                                            OnCheckedChanged="ThirdPartyCB_CheckedChanged" AutoPostBack="true" Enabled="false">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr id="ThirdPartyTR" runat="server" visible="false">
                                    <td width="5%" align="right">
                                        <span class="elementLabel">Party ID :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap">
                                        <asp:TextBox ID="txtThirdPartyID" runat="server" Width="90px" TabIndex="1" CssClass="textBox"
                                            AutoPostBack="true" OnTextChanged="txtThirdPartyID_TextChanged" Enabled="false"></asp:TextBox>
                                        <%--<asp:Button ID="btnThirdPartyID" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"/>--%>
                                        <asp:Label ID="lblThirdPartyName" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap" align="right">
                                        &nbsp;<span class="elementLabel"><span class="mandatoryField">* </span>Bill Of Entry
                                            No.:</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtbillno" runat="server" CssClass="textBox" Width="100px" TabIndex="3"
                                            AutoPostBack="true" Enabled="false"></asp:TextBox>
                                        <%--<asp:Button ID="btnBillEntryNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />--%>
                                        <asp:Label ID="lblboecur" runat="server" CssClass="elementLabel"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblboeamt" runat="server" CssClass="elementLabel"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap" width="3%">
                                        <span class="elementLabel">Bill Of Entry Date :</span>
                                        <asp:TextBox ID="txtbilldate" Enabled="false" runat="server" CssClass="textBox" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70" TabIndex="4"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td style="white-space: nowrap" width="5%">
                                        <span class="elementLabel">Port Code :</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtprtcd" Enabled="false" runat="server" CssClass="textBox" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70" TabIndex="5"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <%--<asp:Button ID="btnAddGRPPCustoms" runat="server" Text="Add" CssClass="buttonDefault"
												TabIndex="6" onclick="btnAddGRPPCustoms_Click"   />--%>
                                    </td>
                                    <td width="40%">
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table width="80%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridViewEDPMS_Bill_Details" runat="server" AutoGenerateColumns="false"
                                            Width="80%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewEDPMS_Bill_Details_RowDataBound"
                                            CssClass="GridView">
                                            <PagerSettings Visible="true" />
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
                                                <asp:TemplateField HeaderText="Invoice Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvcamtic" runat="server" Text='<%# Eval("Invoiceamount","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvcamt" runat="server" Text='<%# Eval("InvoiceAmt","{0:0.00}") %>'
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
                            <table width="100%">
                                <tr>
                                    <td width="55%" align="right">
                                        <%-- <span class="pageLabel">Selected BOE's Total : </span>--%>
                                        <asp:Label ID="lblTot" Style="text-align: right;" runat="server" CssClass="pageLabel" />
                                    </td>
                                    <td width="8%" align="right">
                                        <asp:Label ID="lblTotPayAmt" Style="text-align: right;" runat="server" CssClass="pageLabel" />
                                    </td>
                                    <td width="37%">
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
