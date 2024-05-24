<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDPMS_BOE_Setlement_view.aspx.cs"
    Inherits="IDPMS_IDPMS_BOE_Setlement_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        //        function CheckBranch() {
        //            var ddlbranch = document.getElementById("ddlbranch");

        //            if (ddlbranch.value != "0") {
        //                return true;
        //            }
        //            else {
        //                alert("Select Branch !");
        //                return false;
        //            }
        //        }
        //        function callModalpopup() {
        //            var popupbtn = document.getElementById("btnShow");
        //            popupbtn.click();

        //        }
        //        function sethdnvalue() {
        //            var btnhdndelete = document.getElementById("btnhdndelete");
        //            btnhdndelete.value = "Yes";
        //        }
        //        function sethdnvalue2() {
        //            var btnhdndelete = document.getElementById("btnhdndelete");
        //            btnhdndelete.value = "No";
        //        }

    </script>
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td style="white-space: nowrap; text-align: left; vertical-align: middle">
                                <span class="pageLabel"><strong>Bill Of Entry - Settlement Deletion Entry</strong></span>
                                <%--<asp:HiddenField ID="btnhdndelete" runat="server" />--%>
                            </td>
                            <td style="white-space: nowrap; text-align: right; vertical-align: middle">
                                <span class="elementLabel">Search :</span>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="textBox" MaxLength="40" Width="180px"
                                    TabIndex="5"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault" ToolTip="Search"
                                    OnClick="btnSearch_Click" />
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
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                            <tr id="rowGrid" runat="server">
                                <td colspan="2" align="left">
                                    <asp:GridView ID="GridViewInwData" runat="server" AutoGenerateColumns="False" Width="100%"
                                        AllowPaging="True" AllowSorting="false" ShowHeaderWhenEmpty="True" OnRowDataBound="GridViewInwData_RowDataBound"
                                        EditIndex="1" OnRowCommand="GridViewInwData_RowCommand" CssClass="GridView" PageSize="20">
                                        <PagerSettings Visible="false" />
                                        <Columns>
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
                                            <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CUST_NAME") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblport" runat="server" Text='<%# Eval("PortCode") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inv Sr No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoicSrNo" runat="server" Text='<%# Eval("invoiceSerialNo") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("invoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Curr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("remittanceCurrency") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("invoiceAmt","{0:0.00}") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Settlement Amt" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSetAmt" runat="server" Text='<%# Eval("OttAmt","{0:0.00}") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("Doc_No")+";"+Eval("Bill_No")+";"+Eval("InvoiceSerialNo") %>'
                                                        CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" Autopostback="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr id="rowPager" runat="server">
                                <td class="gridHeader" style="text-align: center; width: 100%; vertical-align: top"
                                    colspan="2">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0" style="width: 100%">
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
                                                    <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                    </table>
                    <%--
                    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" 
                        onclick="btnShow_Click" />
                    <!-- ModalPopupExtender -->
                    <ajaxToolkit:modalpopupextender id="mp1" runat="server" popupcontrolid="Panel1" targetcontrolid="btnShow"
                        cancelcontrolid="btnClose" backgroundcssclass="modalBackground">
</ajaxToolkit:modalpopupextender>
                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="background-color:Window;display: none">
                        <div style="height: 60px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                   Are you sure you want to delete this record?
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="bntok" OnClientClick="sethdnvalue()" runat="server" TabIndex="2" Text="OK" /> <asp:Button ID="btnClose" OnClientClick="sethdnvalue2()" runat="server" TabIndex="1" Text="Cancel" />
                    </asp:Panel>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
