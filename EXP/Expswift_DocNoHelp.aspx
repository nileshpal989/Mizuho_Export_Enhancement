<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expswift_DocNoHelp.aspx.cs"
    Inherits="EXP_Expswift_DocNoHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;

        window.onload = function () {

            UpperBound = parseInt('<%= this.GridViewLogdeDocList.Rows.Count %>') - 1;
            LowerBound = 0;
            SelectedRowIndex = -1;

            return false;

        }

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

        function SelectRow(CurrentRow, RowIndex) {

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#DCFC5C';
                CurrentRow.style.color = 'Black';
            }

            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }

        function DisSelectRow(CurrentRow, RowIndex) {

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = CurrentRow.originalBackgroundColor;
                CurrentRow.style.color = 'Black';
            }

        }
        function CloseMe() {
            enable_parent();
            window.close();
        }
    </script>
</head>
<body bgcolor="#CDDAE4" topmargin="0" bottommargin="0" rightmargin="0" leftmargin="0"
    onload="disable_parent();EndRequest();" onunload="enable_parent();" onblur="CheckFocus();"
    onfocus="CheckFocus();" style="width: 100%; height: 100%;">
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
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table align="left" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span class="elementLabel">Finacial Year:</span>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" TabIndex="2" 
                                AutoPostBack="true" OnSelectedIndexChanged="btngo_Click">
                                </asp:DropDownList>
                                &nbsp;
                                <span class="elementLabel">DOC Types:</span>
                                <asp:DropDownList ID="ddlDocTypes" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="btngo_Click">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="BLA" Value="BLA" />
                                    <asp:ListItem Text="BLU" Value="BLU" />
                                    <asp:ListItem Text="BBA" Value="BBA" />
                                    <asp:ListItem Text="BBU" Value="BBU" />
                                    <asp:ListItem Text="BCA" Value="BCA" />
                                    <asp:ListItem Text="BCU" Value="BCU" />
                                    <asp:ListItem Text="IBD" Value="IBD" />
                                    <asp:ListItem Text="EB" Value="EB" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 2%; white-space: nowrap; vertical-align: middle">
                                <span class="elementLabel" style="vertical-align: middle">Search :</span> &nbsp;
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txtenabled" MaxLength="40" Width="200px"
                                    AutoPostBack="true" OnTextChanged="btngo_Click"></asp:TextBox>
                                <asp:Button ID="btnCloseMe" CssClass="btnCloseMe" runat="server" ToolTip="Close Me!"
                                    OnClientClick="CloseMe();" />
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server" style="white-space: nowrap">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewLogdeDocList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="true" OnRowCommand="GridViewLogdeDocList_RowCommand"
                                    OnRowDataBound="GridViewLogdeDocList_RowDataBound" OnRowCreated="GridViewLogdeDocList_RowCreated"
                                    PageSize="20" onblur="GiveFocus='FALSE';CheckFocus();">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocument_No" runat="server" Text='<%# Eval("Document_No") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust AccNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustAcNo" runat="server" Text='<%# Eval("CustAcNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="50%" ItemStyle-Width="50%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCUST_NAME" runat="server" Text='<%# Eval("ShortName") %>' ToolTip='<%# Eval("CUST_NAME") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBill_Amount" runat="server" Text='<%# Eval("Bill_Amount","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
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
                                            <td align="left" style="width: 25%; vertical-align: middle">
                                                &nbsp;Records per page:&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 50%; vertical-align: middle" valign="top">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                            </td>
                                            <td align="right" style="width: 25%; vertical-align: middle">
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
        </center>
    </div>
    </form>
</body>
</html>
