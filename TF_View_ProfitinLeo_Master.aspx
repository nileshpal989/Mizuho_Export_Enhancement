<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_View_ProfitinLeo_Master.aspx.cs" Inherits="TF_View_ProfitinLeo_Master" %>

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
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>In Lieu Master View</strong></span>
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
                    <tr>
                        <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <asp:GridView ID="GridViewCommission" runat="server" AutoGenerateColumns="False"
                                Width="78%" AllowPaging="True" OnRowCommand="GridViewCommission_RowCommand"
                                OnRowDataBound="GridViewCommission_RowDataBound">
                                <PagerSettings Visible="false" />
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                 <asp:TemplateField HeaderText="Customer A/C No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustac" runat="server" CssClass="elementLabel" Text='<%# Eval("CustAcNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Commn ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" CssClass="elementLabel" Text='<%# Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="6%" />
                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" CssClass="elementLabel" Text='<%# Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Rate %" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" CssClass="elementLabel" Text='<%# Eval("Rate","{0:0.000000}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="8%" />
                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Min Amt." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinAmt" runat="server" CssClass="elementLabel" Text='<%# Eval("MinAmt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="7%" />
                                        <ItemStyle HorizontalAlign="Right" Width="7%" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Max Amt." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmaxamt" runat="server" CssClass="elementLabel" Text='<%# Eval("MaxAmt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="7%" />
                                        <ItemStyle HorizontalAlign="Right" Width="7%" />
                                    </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Flat Amt." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFlat" runat="server" CssClass="elementLabel" Text='<%# Eval("Flat") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="7%" />
                                        <ItemStyle HorizontalAlign="Right" Width="7%" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" HeaderText="Delete"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("CustAcNo")+";"+ Eval("SrNo")%>'
                                                CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="center" style="width: 100%" valign="top" colspan="2" class="gridHeader">
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