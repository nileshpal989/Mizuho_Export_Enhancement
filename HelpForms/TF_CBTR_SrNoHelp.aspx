﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_CBTR_SrNoHelp.aspx.cs"
    Inherits="TF_CBTR_SrNoHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>LMCC- CTR SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        window.onload = function () {
            UpperBound = parseInt('<%= this.grdsearch.Rows.Count %>') - 1;
            LowerBound = 0;
            SelectedRowIndex = -1;
            return false;
        }

        function SelectRow(CurrentRow, RowIndex) {
            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)
                return;

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

        function RefreshParentPage() {

            window.opener.document.getElementById('btnSearchCriteria').click();
            window.close();
            return false;
        }

        function GetValues() {
            var table = document.getElementById('<%=grdsearch.ClientID %>');
            var Row = table.rows[SelectedRowIndex + 1];
            if (SelectedRowIndex >= 0) {
                var CellValue1 = Row.cells[0].innerText;
                document.getElementById('txtcell1').value = CellValue1;
                window.opener.document.getElementById('btnSearchCriteria').click();
                window.close();
            }
            return false;
        }

        function SelectSibling(e) {
            var e = e ? e : window.event;
            var keyCode = e.which ? e.which : e.keyCode;
            if (keyCode == 40)
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            else if (keyCode == 38)
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            if (keyCode == 13) {
                GetValues();
            }
            return false;
        }

        function sss() {
            window.opener.sss();
            window.opener.document.getElementById('btnSearchCriteria').click();
            return false;
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { document.getElementById('ImageButton1').click(); return false; }
        }

        document.onkeydown = stopRKey; 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
        <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td nowrap width="5%">
                            <asp:Label ID="lblCustName" CssClass="elementLabel" runat="server">Doc No : &nbsp;</asp:Label>
                        </td>
                        <td width="10%" nowrap>
                            <asp:TextBox ID="txtsearch" runat="server" Width="184px" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/search.gif"
                                OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" ondblclick="GetValues()"
                                OnRowCreated="grdsearch_RowCreated" Width="600px" Height="100px">
                                <RowStyle HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <HeaderStyle VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr No" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PartyName" HeaderText="Party Name" ItemStyle-Width="200px"
                                        ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                    <asp:BoundField DataField="Debit_Amount" HeaderText="Debit Amt" ItemStyle-Width="80px"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NoofTransDebit" HeaderText="No.of Debit Trans" ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="Credit_Amount" HeaderText="Credit Amt" ItemStyle-Width="80px"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"></asp:BoundField>
                                    <asp:BoundField DataField="NoofTransCredit" HeaderText="No.of Credit Trans" ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="NoofAccount_Invoved" HeaderText="No.of A/Cs. Involved"
                                        ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                </Columns>
                                <SelectedRowStyle CssClass="grdselectback" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="txtcell1" runat="server" />
                            <asp:HiddenField ID="txtcell2" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
