﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_View_Error_Code_Master.aspx.cs"
    Inherits="EDPMS_View_Error_Code_Master" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">
        function validateSearch() {
            var _txtvalue = document.getElementById('txtSearch').value;
            _txtvalue = _txtvalue.replace(/'&lt;'/, "");
            _txtvalue = _txtvalue.replace(/'&gt;'/, "");
            if (_txtvalue.indexOf('<!') != -1 || _txtvalue.indexOf('>!') != -1 || _txtvalue.indexOf('!') != -1 || _txtvalue.indexOf('<') != -1 || _txtvalue.indexOf('>') != -1 || _txtvalue.indexOf('|') != -1) {
                alert('!, |, <, and > are not allowed.');
                document.getElementById('txtSearch').value = _txtvalue;
                return false;
            }
            else
                return true;
        }

        function submitForm(event) {
            if (event.keyCode == '13') {
                if (validateSearch() == true)
                    __doPostBack('btnSearch', '');
                else
                    return false;
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 100%" valign="bottom" nowrap>
                            <span class="pageLabel">Error Code Master View</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            <hr />
                        </td>
                    </tr>
                    <tr align="right">
                        <td align="right" style="width: 100%;" valign="top">
                            <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                ToolTip="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; height: 21px;" valign="top">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td align="left" style="width: 100%" valign="top">
                            <asp:GridView ID="GridViewErrorList" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowPaging="True" OnRowDataBound="GridViewErrorList_RowDataBound">
                                <PagerSettings Visible="false" />
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Error Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblErrorCode" runat="server" CssClass="elementLabel" Text='<%# Eval("ErrorCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldName" runat="server" CssClass="elementLabel" Text='<%# Eval("XMLField") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Error Description" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblError_Desc" runat="server" CssClass="elementLabel" Text='<%# Eval("ErrorDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="70%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="center" style="width: 100%" valign="top" class="gridHeader">
                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
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
                                        <td align="right" style="width: 25%;">
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
