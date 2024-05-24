<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_Swift_STP_Main.aspx.cs"
    Inherits="TF_Swift_STP_Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                    <tr>
                            <td align="right" valign="bottom" colspan="4">
                                <asp:HyperLink id="hyperlink1" NavigateUrl="~/TF_SwiftSTP_Summary.aspx"
                                Text="Click here for Swift Summary  ." BackColor="#CCCCCC" runat="server" 
                                    BorderColor="Black" ForeColor="Black"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr style="height: 35px">
                            <td align="left">
                                &nbsp;Show
                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                    Height="25px" OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;Records
                            </td>
                            <td align="left">
                                <span >&nbsp; Approve Date</span>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" AutoPostBack="true" ToolTip="Select Checker Approval Date."
                                    Width="70px" TabIndex="3" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_FromDate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btnCal_FromDate" runat="server" CssClass="btncalendar_enabled" />
                                <ajaxToolkit:CalendarExtender ID="CE_FromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="btnCal_FromDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MV_FromDate" runat="server" ControlExtender="ME_FromDate"
                                    ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="False"></ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td align="left">
                                <span> &nbsp; Module</span>
                                <asp:DropDownList ID="ddlModule" runat="server" CssClass="dropdownList" TabIndex="35"
                                    ToolTip="Select Module" AutoPostBack="true" Height="25px" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                    <asp:ListItem Value="IMP" Text="Import"></asp:ListItem>
                                    <asp:ListItem Value="EXP" Text="Export"></asp:ListItem>
                                </asp:DropDownList>
                                <span id="SpanTranstype" visible="false" runat="server">
                                <span>Transaction</span>
                                    <asp:DropDownList ID="ddlTrans" runat="server" CssClass="dropdownList" TabIndex="35"
                                        ToolTip="Select Transaction" AutoPostBack="true" Height="25px" OnSelectedIndexChanged="ddlTrans_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td align="right">
                               <span>&nbsp; Search</span><asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="textBox"
                                    MaxLength="40" Width="180px" TabIndex="6" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" colspan="4">
                                <asp:Label ID="labelMessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewSwiftDash" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True" OnRowDataBound="GridViewSwiftDash_RowDataBound" CssClass="DashBoardGridView">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Transaction Ref No." HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocument_No" runat="server" Text='<%#Eval("Document_No")%>' CssClass="font-weight-normal"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Swift Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSwiftType" runat="server" Text='<%#Eval("SwiftType")%>' CssClass="font-weight-normal"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Date" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocDate" runat="server" Text='<%# Eval("ApproveDate","{0:dd/MM/yyyy}") %>'
                                                    CssClass="font-weight-normal"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gbase Status" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGbaseStatus" runat="server" Text='<%#Eval("GBase_Flag")%>' CssClass="font-weight-bold"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="XML Generated" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblXMLStatus" runat="server" Text='<%#Eval("XMLStatus")%>' ToolTip='<%#Eval("XMLMSG")%>'
                                                    CssClass="font-weight-bold"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Swift ACK/NAK" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInfraStatus" runat="server" Text='<%#Eval("InfrasoftStatus")%>'
                                                    ToolTip='<%#Eval("InfrasoftErrorMSG")%>' CssClass="font-weight-bold"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" class="DashBoardgridFooter">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25%;">
                                                &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 50%" valign="top">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                            </td>
                                            <td align="right" style="width: 25%;">
                                            </td>
                                        </tr>
                                    </tbody>
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
