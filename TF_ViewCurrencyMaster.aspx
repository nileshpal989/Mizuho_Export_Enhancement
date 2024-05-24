﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ViewCurrencyMaster.aspx.cs"
    Inherits="RBI_ViewCurrencyMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />

    <link href="Style/SnackBar.css" rel="stylesheet" type="text/css" /> <%--snackbar jquery--%>
    <script src="Help_Plugins/MyJquery2.js" type="text/javascript"></script>
     <script src="Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="Help_Plugins/jquery-ui.js" type="text/javascript"></script>
        <link href="Help_Plugins/JueryUI.css" rel="stylesheet" type="text/css" />
    <script src="Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        snackbar
       
        function Alert(Result) {
            MyAlert(Result);
        }

        function confirmDelete() {
            MyConfirm('Do you want to delete this record?', '#btnDeleteConfirm');
        }
    </script>
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
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
             <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
        <%--   snackbar--%>
    <div id="snackbar">
        <div id="snackbarbody" style="padding-top: -500px;">
        </div>
    </div>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                      <asp:PostBackTrigger ControlID="btnDeleteConfirm" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>Currency Master View<b></span>
                            </td>
                            <td align="right" style="width: 50%">
                                     <input type="hidden" id="hdncustid1" runat="server" />
                                     <input type="hidden" id="hdnDescription" runat="server" />
                                     <input type="hidden" id="hdnGBCurrcny" runat="server" />
                                     <input type="hidden" id="hdnStatus" runat="server" />
                            <asp:Button ID="btnDeleteConfirm" Style="display: none;" runat="server" 
                            onclick="btnDeleteConfirm_Click"/>
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" OnClick="btnAdd_Click"
                                    ToolTip="Add New Record" />
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
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:GridView ID="GridViewCurrency" runat="server" AutoGenerateColumns="False" 
                                    Width="50%" AllowPaging="True" OnRowCommand="GridViewCurrency_RowCommand"
                                    OnRowDataBound="GridViewCurrency_RowDataBound"  CssClass="GridView">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridItem" />
                                    <HeaderStyle VerticalAlign="Middle" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Curr ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrencyCode" runat="server" Text='<%#Eval("C_Code")%>' CssClass="elementLabel"></asp:Label>
                                                <asp:Label ID="lblCurrencyID" runat="server" Text='<%#Eval("C_Code")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblLinked" runat="server" Text='<%#Eval("numberOfLinkedRecords")%>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="45%" ItemStyle-Width="45%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("C_Description")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="45%" />
                                            <ItemStyle HorizontalAlign="Left" Width="45%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GBase Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGBaseCode" runat="server" Text='<%#Eval("GBase_Code")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("C_Status")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" ToolTip="Delete Record" CommandName="RemoveRecord"
                                                    CommandArgument='<%#Eval("C_Code")+";"+Eval("C_Description")+";"+Eval("GBase_Code")+";"+Eval("C_Status")%>' Text="Delete"/>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
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
        </center>
    </div>
    </form>
</body>
</html>
