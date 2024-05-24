<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportDocHelp.aspx.cs" Inherits="EXP_ExportDocHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export Document No Help</title>
    <link href="~/Style/HelpCss.css" rel="stylesheet" type="text/css" />
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    
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
            //  var s = window.opener.document.getElementById('txtBrand').value;
            //  alert(s);
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
            //  alert('ssss');
            var table = document.getElementById('<%=grdsearch.ClientID %>');

            var Row = table.rows[SelectedRowIndex + 1];
            if (SelectedRowIndex >= 0) {
                var CellValue1 = Row.cells[0].innerText;
                var CellValue2 = Row.cells[4].innerText;
                document.getElementById('txtcell1').value = CellValue1;
                document.getElementById('txtcell2').value = CellValue2;
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
            // alert('sss');
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
    <style type="text/css">
        .style1
        {
            width: 60px;
        }
        .style3
        {
            width: 210px;
        }
        .style5
        {
            width: 193px;
        }
        .style6
        {
            width: 189px;
        }
        .style7
        {
            width: 160px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60px;" align="right">
                            <asp:Label ID="lblCustName" runat="server">Doc No : &nbsp;</asp:Label>
                        </td>
                        <td class="style7" align="left">
                            <asp:TextBox ID="txtsearch" runat="server" Width="184px" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td align="left">
                            &nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Images/search.gif"
                                OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" ondblclick="GetValues()"
                                OnRowCreated="grdsearch_RowCreated" Width="600px" Height="100px" CellPadding="3"
                                BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                                CellSpacing="2">
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem"
                                    BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle ForeColor="White" VerticalAlign="Top" CssClass="gridHeader" BackColor="#A55129"
                                    Font-Bold="True" />
                                <AlternatingRowStyle HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:BoundField DataField="Document_No" HeaderText="Doc No" ItemStyle-Width="80px"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Date_Received" HeaderText="Doc Date" ItemStyle-Width="60px"
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Bill_Amount" HeaderText="Bill Amt" ItemStyle-Width="60px"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" ItemStyle-Width="200px"
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left" Width="230px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BalAmtforRealisation" HeaderText="Bal Amt for Realisation" ItemStyle-Width="200px"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="200px" />
                                    </asp:BoundField>
                                </Columns>
                                <SelectedRowStyle CssClass="grdselectback" BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            &nbsp;
                        </td>
                        <td class="style7">
                            &nbsp;
                        </td>
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
