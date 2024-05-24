<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpDocNo_ExportBillDue.aspx.cs"
    Inherits="Reports_EXPReports_HelpDocNo_ExportBillDue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Document No.Help</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
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
                var CellValue2 = Row.cells[1].innerText;
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 50%;">
                    <tr>
                        <td align="left" nowrap>
                            <asp:Label ID="lblCustName" runat="server">Enter Document No. : &nbsp;</asp:Label>
                            <asp:TextBox ID="txtsearch" runat="server" Width="196px" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"
                                MaxLength="20"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Images/search.gif"
                                OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                            <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" ondblclick="GetValues()"
                                OnRowCreated="grdsearch_RowCreated" Width="400px" Height="70px" CellPadding="4"
                                ForeColor="#333333" GridLines="None">
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <AlternatingRowStyle HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Document_No" HeaderText="Doc No." ItemStyle-Width="80px"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Date_Received" HeaderText="Doc Date." ItemStyle-Width="60px"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Bill_Type" HeaderText="Bill Type" />
                                    <asp:BoundField DataField="Currency" HeaderText="Cur" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="left" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Bill_Amount" HeaderText="Bill Amt" ItemStyle-Width="60px"
                                        ItemStyle-HorizontalAlign="right"></asp:BoundField>
                                </Columns>
                                <SelectedRowStyle CssClass="grdselectback" BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                            <asp:Label ID="NoRecords" runat="server" ForeColor="Red" Text="No Record's Found for Date"
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="txtcell1" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
