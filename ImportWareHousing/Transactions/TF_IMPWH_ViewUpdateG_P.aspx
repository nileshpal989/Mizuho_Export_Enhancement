<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_ViewUpdateG_P.aspx.cs"
    Inherits="ImportWareHousing_Transactions_TF_IMPWH_ViewUpdateG_P" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function Validate() {
            if (document.getElementById('txtFromDate').value == '') {
                alert('Enter Customer Account Numeber.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            if (document.getElementById('txtCustACNo').value == '') {
                alert('Enter Customer Account Numeber.');
                document.getElementById('txtCustACNo').focus();
                return false;
            }
            return true;
        }
        function CustHelp() {
            popup = window.open('../../IDPMS/TF_CustMaster.aspx', 'CustHelp', 'height=500,  width=500,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "CustHelp";
            return false;
        }
        function selectCustomer(Cust, label) {
            document.getElementById('txtCustACNo').value = Cust;
            document.getElementById('lblcustname').innerHTML = label;
            __doPostBack('txtCustACNo', '');
        }
    </script>
    <style type="text/css">
        hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 2px;
        }
    </style>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
        <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
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
            <center>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" valign="bottom" colspan="3" width="100%">
                                    &nbsp;<span class="pageLabel" style="font-weight: bold;">Invoice Status Updation</span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top" colspan="3">
                                    <hr />
                                    <input type="hidden" id="hdnBranchCode" runat="server" />
                                    <input type="hidden" id="hdnUserRole" runat="server" />
                                    <asp:Button ID="btnfillgrid" Style="display: none;" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap style="width: 10%;">
                                    <span class="elementLabel">Branch :</span>
                                </td>
                                <td align="left" nowrap>&nbsp;
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" TabIndex="1">
                                </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 80%;" valign="top">
                                    <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                        CssClass="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                        ToolTip="Search" OnClick="btnSearch_Click" TabIndex="6" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 10%">
                                    <span class="mandatoryField">*</span> <span class="elementLabel">As On Date :</span>
                                </td>
                                <td align="left" style="width: 700px" colspan="2">&nbsp;&nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                    ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
                                    <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                        TabIndex="-1" />
                                    <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                        TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                        ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                        ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                    </ajaxToolkit:MaskedEditValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap style="width: 10%"><span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Customer A/C :</span> </td>
                                <td align="left" nowrap style="width: 50%;">&nbsp;&nbsp;<asp:TextBox ID="txtCustACNo" runat="server" AutoPostBack="true" CssClass="textBox" MaxLength="20" OnTextChanged="txtCustACNo_TextChanged" TabIndex="2" ToolTip="Press F2 for Help" Width="12%"></asp:TextBox>
                                    <asp:Button ID="btnCustHelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                    <asp:Label ID="lblcustname" runat="server" CssClass="elementLabel" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 40%; height: 21px;" valign="top">
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                            <tr id="rowGrid" runat="server">
                                <td align="left" style="width: 94%;" valign="top" colspan="3">
                                    <table cellspacing="0" cellpadding="0" border="0" style="line-height: 100%; width: 100%;">
                                        <tr id="Tr1" runat="server">
                                            <td align="left" style="width: 100%" valign="top" rowspan="1">
                                                <asp:GridView ID="GridViewInvoice" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    HeaderStyle-Height="10px" RowStyle-Height="10px" CssClass="GridView" OnRowDataBound="GridViewInvoice_RowDataBound"
                                                    AllowPaging="True" PageSize="50" OnRowEditing="EditCommision" DataKeyNames="IECode,Bill_Entry_No,Bill_Entry_Date,InvoiceNo,PortCode,Status,SupplierName"
                                                    OnRowUpdating="UpdateCommision" OnRowCancelingEdit="CancelEdit">
                                                    <PagerSettings Visible="false" />
                                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                        CssClass="gridAlternateItem" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BOE No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBOENo" Text='<%# Eval("Bill_Entry_No") %>'
                                                                    runat="server"></asp:Label>
                                                                <asp:Label ID="lblIECode" Text='<%# Eval("IECode") %>' CssClass="elementLabel" runat="server"
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BOE Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBOEDate" runat="server" Text='<%#Eval("Bill_Entry_Date")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNo")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Curr" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceCurr" runat="server" Text='<%#Eval("Invoice_Currency")%>'
                                                                    ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                            HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceAmt" runat="server" Text='<%#Eval("InvoiceAmt")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier Name" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" ItemStyle-Width="25%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("SupplierName")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier Country" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplierCountry" runat="server" Text='<%#Eval("Supplier_Country")%>'
                                                                    ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shipping Date" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShippingDate" runat="server" Text='<%#Eval("ShippingDate")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Port Loading" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPortShippment" runat="server" Text='<%#Eval("Port_Shippment")%>'
                                                                    ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPortCode" runat="server" Text='<%#Eval("PortCode")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="G/D" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="3%" HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>' Visible="false"
                                                                    CssClass="elementLabel"></asp:Label>
                                                                <asp:DropDownList ID="DDLStatus" runat="server">
                                                                    <asp:ListItem Value="G" Text="G"></asp:ListItem>
                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ButtonType="Link" ShowEditButton="True" ItemStyle-HorizontalAlign="Center "
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                                            ControlStyle-Font-Size="Medium" ControlStyle-Font-Underline="true" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="rowPager" runat="server">
                                <td align="center" style="width: 100%" valign="top" colspan="3" class="gridHeader">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0" class="gridHeader">
                                        <tbody>
                                            <tr>
                                                <td align="left" width="25%">&nbsp;Records Per Page :&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                                <td align="center" valign="top" width="50%">
                                                    <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                    <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                    <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                    <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                                </td>
                                                <td align="right" style="width: 25%;">&nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
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
