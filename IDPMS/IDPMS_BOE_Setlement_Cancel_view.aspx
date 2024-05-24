<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDPMS_BOE_Setlement_Cancel_view.aspx.cs"
    Inherits="IDPMS_IDPMS_BOE_Setlement_Cancel_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server" autocomplete="off">
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
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" nowrap>
                            <span class="pageLabel"><strong>IDPMS XML (Payment Settlement Cancellation) View</strong></span>
                            <%--<asp:HiddenField ID="btnhdndelete" runat="server" />--%>
                        </td>
                        <td align="right" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <span class="elementLabel">Branch : </span>
                            <asp:DropDownList ID="ddlbranch" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                CssClass="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault" ToolTip="Search"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lblMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td colspan="2" align="left">
                            <asp:GridView ID="GridViewInwData" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True" OnRowDataBound="GridViewInwData_RowDataBound"
                                EditIndex="1" OnRowCommand="GridViewInwData_RowCommand" PageSize="20" CssClass="GridView">
                                <PagerSettings Visible="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Payment Seq. No" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayRefNo" runat="server" Text='<%# Eval("paymentReferenceNumber") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doc No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNo" runat="server" Text='<%# Eval("Doc_No") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOE No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbillno" runat="server" Text='<%# Eval("Bill_No") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOE Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbilldate" runat="server" Text='<%# Eval("Bill_Date") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblport" runat="server" Text='<%# Eval("PortCode") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CUST_NAME") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Outward No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblORMNo" runat="server" Text='<%# Eval("outwardReferenceNumber") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Curr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("remittanceCurrency") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sett Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSetAmt" runat="server" Text='<%# Eval("Tamount","{0:0.00}") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="center" style="width: 100%" valign="top" colspan="2" class="gridHeader">
                            <table cellspacing="0" cellpadding="2" width="100%" border="0" class="gridHeader">
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
