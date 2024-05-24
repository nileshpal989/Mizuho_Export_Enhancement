<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Impw_ViewSupplierMaster_CP.aspx.cs"
    Inherits="ImportWareHousing_Masters_Supplier_Master" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
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
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom" colspan="2">
                            <span class="pageLabel"><strong>Supplier Master View Paging </strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <input type="hidden" id="hdnUserRole" runat="server" />
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="buttonDefault"
                                ToolTip="Upload Supplier Master File" OnClick="btnUpload_Click" />
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Supplier"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 50%" align="left">
                            <span class="elementLabel">Branch :</span>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                TabIndex="1">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td align="right" style="width: 100%;" valign="top">
                            <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                ToolTip="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="3">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td align="left" style="width: 50%" valign="top" colspan="3">
                            <asp:GridView ID="GridViewCustomerList" runat="server" AutoGenerateColumns="false">
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Supplier ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierID" runat="server" CssClass="elementLabel" ToolTip='<%# Eval("Supplier_ID") %>'
                                                Text='<%# Eval("Supplier_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" CssClass="elementLabel" ToolTip='<%# Eval("Supplier_Name") %>'
                                                Text='<%# Eval("Supplier_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierAddress" runat="server" CssClass="elementLabel" ToolTip='<%# Eval("Supplier_Address") %>'
                                                Text='<%# (Eval("Supplier_Address").ToString().Length > 50) ? (Eval("Supplier_Address").ToString().Substring(0, 50) + "...") : Eval("Supplier_Address")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierCountry" runat="server" CssClass="elementLabel" ToolTip='<%# Eval("SupplierCountryName") %>'
                                                Text='<%# Eval("SupplierCountryName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankName" runat="server" CssClass="elementLabel" ToolTip='<%# Eval("Bank_Name") %>'
                                                Text='<%# Eval("Bank_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("Supplier_ID") %>'
                                                CommandName="RemoveRecord" CssClass="deleteButton" Text="Delete" ToolTip="Delete Suuplier ID" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="center" style="width: 50%" valign="top" colspan="3" class="gridHeader">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td align="left" style="width: 20%; vertical-align: middle">
                                            Records per page:
                                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="20" Value="20" Selected="True" />
                                                <asp:ListItem Text="30" Value="30" />
                                                <asp:ListItem Text="40" Value="40" />
                                                <asp:ListItem Text="50" Value="50" />
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" style="width: 60%" valign="top">
                                            <asp:Repeater ID="rptPager" runat="server">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed" CssClass="pagingButton"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <br />
                                            <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click"
                                                CssClass="pagingButton" />
                                            <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click"
                                                CssClass="pagingButton" />
                                            <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click"
                                                CssClass="pagingButton" />
                                            <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click"
                                                CssClass="pagingButton" />
                                        </td>
                                        <td align="right" style="width: 20%;">
                                            <asp:Label ID="lblpageno" runat="server"></asp:Label>
                                            <br />
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
