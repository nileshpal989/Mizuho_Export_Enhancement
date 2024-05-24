<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ViewRelationshipMaster.aspx.cs" Inherits="TF_ViewRelationshipMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
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
            <%-- <Triggers>
                        <asp:PostBackTrigger ControlID="btnAdd" />
                    </Triggers>--%>
            <ContentTemplate>
            <input type="hidden" id="hdnBranchCode" runat="server" />
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Relationship Master View</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr align="right">
                        <td align="right" style="width: 100%;" valign="top" colspan="2">
                            <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                ToolTip="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                 <table cellspacing="0" cellpadding="0" border="0" width="30%">
                    <tr>
                        <td width="10%" align="right" nowrap>
                            <span class="elementLabel">Branch :</span>
                        </td>
                        <td width="10%" align="left" nowrap>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                            </asp:DropDownList>
                        </td>
                        </tr>
                </table>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 100%; height: 21px;" valign="top">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td align="left" style="width: 100%" valign="top">
                            <asp:GridView ID="GridViewRelationShipList" runat="server" AutoGenerateColumns="False"
                                Width="100%" AllowPaging="True" OnRowCommand="GridViewBusinessSourceList_RowCommand"
                                OnRowDataBound="GridViewBusinessSourceList_RowDataBound">
                                <PagerSettings Visible="false" />
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Level 1" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLevel1" runat="server" CssClass="elementLabel" Text='<%# Eval("NAME1") %>' ></asp:Label>
                                            <asp:Label ID="lblLID1" runat="server" CssClass="elementLabel" Text='<%# Eval("L1") %>'  Visible ="false"></asp:Label>
                                            <asp:Label ID="lblLinked" runat="server" Text='<%# Eval("numberOfLinkedRecords") %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Level 2" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLevel2" runat="server" CssClass="elementLabel" Text='<%# Eval("NAME2") %>'></asp:Label>
                                            <asp:Label ID="lblLID2" runat="server" CssClass="elementLabel" Text='<%# Eval("L2") %>'  Visible ="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Level 3" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLevel3" runat="server" CssClass="elementLabel" Text='<%# Eval("NAME3") %>'></asp:Label>
                                           <asp:Label ID="lblLID3" runat="server" CssClass="elementLabel" Text='<%# Eval("L3") %>'  Visible ="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Level 4" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLevel4" runat="server" CssClass="elementLabel" Text='<%# Eval("NAME4") %>'></asp:Label>
                                            <asp:Label ID="lblLID4" runat="server" CssClass="elementLabel" Text='<%# Eval("L4") %>'  Visible ="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Level 5" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLevel5" runat="server" CssClass="elementLabel" Text='<%# Eval("NAME5") %>'></asp:Label>
                                           <asp:Label ID="lblLID5" runat="server" CssClass="elementLabel" Text='<%# Eval("L5") %>'  Visible ="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" HeaderText="Delete"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("L1") %>'
                                                CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
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
