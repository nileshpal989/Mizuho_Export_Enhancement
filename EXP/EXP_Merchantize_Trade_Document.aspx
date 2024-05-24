<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_Merchantize_Trade_Document.aspx.cs"
    Inherits="EXP_EXP_Merchantize_Trade_Document" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
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
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
        <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" colspan="3">
                            <span class="pageLabel"><strong>Import linking to Merchanting Trade</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="10%" nowrap>
                            <span class="elementLabel">Branch : </span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                AutoPostBack="true" TabIndex="1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                Width="70" TabIndex="2" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                            </ajaxToolkit:CalendarExtender>
                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                            &nbsp;
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                TabIndex="3" OnTextChanged="txtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td align="right" valign="top" nowrap>
                            <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                Class="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault" TabIndex="6"
                                ToolTip="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td colspan="3">
                            <asp:GridView ID="GridViewMerchTrade" runat="server" Width="100%" AllowPaging="True"
                                AutoGenerateColumns="false" OnRowCommand="GridViewMerchTrade_RowCommand" OnRowDataBound="GridViewMerchTrade_RowDataBound">
                                <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <PagerSettings Visible="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranch" runat="server" CssClass="elementLabel" Text='<%# Eval("Document_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doc. Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocDate" runat="server" CssClass="elementLabel" Text='<%#Eval("Date_Negotiated") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer A/c No" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustAcNo" runat="server" CssClass="elementLabel" Text='<%#Eval("CustAcNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="left"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Text='<%#Eval("CUST_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="15%"
                                        ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParty_Name" runat="server" CssClass="elementLabel" Text='<%#Eval("Party_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Import Reference No." HeaderStyle-HorizontalAlign="left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblRefNo" runat="server" CssClass="elementLabel" Text='<%#Eval("ImpRefNo") %>'></asp:Label>--%>
                                            <asp:TextBox ID="txtImpRefNo" CssClass="textBox" runat="server" MaxLength="20" Text='<%#Eval("ImpRefNo") %>'></asp:TextBox>
                                            <asp:Button ID="btnAdd" CssClass="deleteButton" runat="server" Text="Add" CommandArgument='<%# "A" + ";" +Eval("Document_No") %>'
                                                CommandName="" />
                                            <asp:Button ID="btnDelete" CssClass="deleteButton" runat="server" Text="Delete" Visible="false"
                                                CommandArgument='<%# "D" + ";" +Eval("Document_No") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="center" valign="top" colspan="3" class="gridHeader">
                            <table cellspacing="0" cellpadding="2" width="100%" border="0" class="gridHeader">
                                <tbody>
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            &nbsp;Records per page:&nbsp;
                                            <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" style="width: 50%" valign="top">
                                            <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                            <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                            <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                            <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                        </td>
                                        <td align="right" width="25%">
                                            &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
