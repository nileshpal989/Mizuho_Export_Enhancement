<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Impw_ViewSupplierMasterGriviewAutoSearchWithPaging.aspx.cs"
    Inherits="ImportWareHousing_Masters_Impw_ViewSupplierMasterGriviewAutoSearchWithPaging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    Search:
    <asp:TextBox ID="txtSearch" runat="server" />
    <hr />
    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderStyle-Width="150px" DataField="ContactName" HeaderText="Contact Name"
                ItemStyle-CssClass="ContactName" />
            <asp:BoundField HeaderStyle-Width="150px" DataField="CustomerID" HeaderText="CustomerID" />
            <asp:BoundField HeaderStyle-Width="150px" DataField="City" HeaderText="City" />
        </Columns>
    </asp:GridView>
    <br />
    <div class="Pager">
    </div>
    </form>
</body>
</html>
