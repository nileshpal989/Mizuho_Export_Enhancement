<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_ViewEDPMSDataUpdation.aspx.cs"
    Inherits="EDPMS_EDPMS_ViewEDPMSDataUpdation" %>

<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 64 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function Valid_Month() {
            var Y = new Date();
            var _Month = Y.getMonth() + 1;
            var txtMonth = document.getElementById('txtMonth');
            var txtYear = document.getElementById('txtYear');
            if (txtMonth.value == '') {
                alert('Enter Month..');
                txtMonth.focus();
                return false;
            }
            else if (txtMonth.value < 1 || txtMonth.value > 12) {
                alert('Invalid Month..');
                txtMonth.value = _Month;
                txtMonth.focus();
                return false;
            }

        }
        function Valid_Year() {
            var Y = new Date();
            var _Year = Y.getFullYear();
            var txtYear = document.getElementById('txtYear');

            if (txtYear.value == '') {
                alert('Enter Year..');
                txtYear.focus();
                return false;
            }
            else if (txtYear.value > _Year || txtYear.value == 0 || txtYear.value < 1920 || txtYear.value.length != 4) {
                alert('Invalid Year');
                txtYear.value = _Year;
                txtYear.focus();
                return false;
            }

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="bottom" colspan="3">
                                <span class="pageLabel"><strong>Updation Of EDPMS Data</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="3">
                                <hr />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                                <input type="hidden" id="hdnUserRole" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  width="20%">
                                <span class="elementLabel">Branch :</span>
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td align="left"  width="50%" nowrap>
                                <span class="elementLabel">Transaction Type:</span>
                                <asp:DropDownList ID="ddlTransType" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                    OnSelectedIndexChanged="TransType_SelectedIndexChanged" >
                                    <asp:ListItem Value="BLA" Text="Bills Bought with L/C at Sight"></asp:ListItem>
                                    <asp:ListItem Value="BLU" Text="Bills Bought with L/C Usance"></asp:ListItem>
                                    <asp:ListItem Value="BBA" Text="Bills Bought without L/C at Sight"></asp:ListItem>
                                    <asp:ListItem Value="BBU" Text="Bills Bought without L/C Usance"></asp:ListItem>
                                    <asp:ListItem Value="BCA" Text="Bills For Collection at Sight"></asp:ListItem>
                                    <asp:ListItem Value="BCU" Text="Bills For Collection Usance"></asp:ListItem>
                                    <asp:ListItem Value="IBD" Text="Vendor Bills Financing"></asp:ListItem>
                                    <asp:ListItem Value="EB/" Text="Advance"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="30%">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="20" Width="180px" ></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" nowrap>
                                <span class="mandatoryField">*</span><span class="elementLabel"> From Document Date:</span>
                                <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" 
                                    ValidationGroup="dtval" Width="70" TabIndex="2" 
                                    ontextchanged="txtfromDate_TextChanged"></asp:TextBox>
                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate"></ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td align="left" width="8%" nowrap>
                                <span class="mandatoryField">*</span><span class="elementLabel"> To Document Date :</span>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" 
                                    Width="70" TabIndex="3" ontextchanged="txtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate"></ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                    ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <%--<tr align="right">
                            <td align="left" style="width: 10%">
                                <span class="elementLabel">Year Month :</span>
                                
                                <asp:TextBox ID="txtMonth" runat="server" Style="text-align: right" AutoPostBack="true"
                                    CssClass="textBox" MaxLength="2" Width="21px" Height="13px" TabIndex="3" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtYear" runat="server" Style="text-align: right" AutoPostBack="true"
                                    CssClass="textBox" MaxLength="4" Width="40px" Height="13px" TabIndex="4" OnTextChanged="txtYear_TextChanged">
                                </asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdApplicationDate" Mask="99" MaskType="None"
                                    ClearMaskOnLostFocus="false" CultureDateFormat="M" runat="server" TargetControlID="txtMonth"
                                    ErrorTooltipEnabled="True" Enabled="true" PromptCharacter=""></ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="9999" MaskType="None"
                                    ClearMaskOnLostFocus="false" CultureDateFormat="Y" runat="server" TargetControlID="txtYear"
                                    ErrorTooltipEnabled="True" Enabled="true" PromptCharacter=""></ajaxToolkit:MaskedEditExtender>
                            </td>
                        </tr>--%>
                        <%--<tr style="height: 30px;" valign="bottom">
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbla" runat="server" CssClass="elementLabel" Checked="true"
                                    Text="Bills Bought with L/C at Sight" GroupName="TransType" OnCheckedChanged="rbtbla_CheckedChanged"
                                    AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rbtbba" runat="server" CssClass="elementLabel" Text="Bills Bought without L/C at Sight"
                                    GroupName="TransType" OnCheckedChanged="rbtbba_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbca" runat="server" CssClass="elementLabel" Text="Bills For Collection at Sight "
                                    GroupName="TransType" OnCheckedChanged="rbtbca_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Vendor Bills Financing"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtIBD_CheckedChanged" />&nbsp;
                            </td>
                        </tr>--%>
                        <%--<tr>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtblu" runat="server" CssClass="elementLabel" Text="Bills Bought with L/C Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtblu_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rbtbbu" runat="server" CssClass="elementLabel" Text="Bills Bought without L/C Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtbbu_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbcu" runat="server" CssClass="elementLabel" Text="Bills For Collection Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtbcu_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                <asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtBEB_CheckedChanged" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server">
                            <td width="100%" colspan="3" align="left">
                                <asp:GridView ID="GridViewEDPMSUpdation" runat="server" AutoGenerateColumns="false"
                                    Width="90%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewEDPMSUpdation_RowDataBound"
                                    OnRowCommand="GridViewEDPMSUpdation_RowCommand" Style="margin-bottom: 0px">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="AD Bill No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblADBillNo" runat="server" Text='<%# Eval("ADBillNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ship Bill No" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShippingBillNo" runat="server" Text='<%# Eval("ShippingBillNo") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice" runat="server" Text='<%# Eval("InvoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IRM No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIRMNo" runat="server" Text='<%# Eval("IRMNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Curr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Realized Amount" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbilldate" runat="server" Text='<%# Eval("Realized_Amount") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FOB Amount" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_FOBmt" runat="server" CssClass="textBox" Text='<%# Eval("FOB_Amount") %>'
                                                    Style="text-align: right; width: 95%" onfocus="this.select()"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FOB IC Amount" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_FOBICmt" runat="server" CssClass="textBox" Text='<%# Eval("FOB_Amount_IC") %>'
                                                    Style="text-align: right; width: 95%" onfocus="this.select()"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Freight Amt" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_FreigAmt" runat="server" CssClass="textBox" Text='<%# Eval("Freight_Amount") %>'
                                                    Style="text-align: right; width: 95%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Freight IC Amt" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_FreigICAmt" runat="server" CssClass="textBox" Text='<%# Eval("Freight_Amount_IC") %>'
                                                    Style="text-align: right; width: 95%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insurance Amount" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_InsuranceAmt" runat="server" CssClass="textBox" Text='<%# Eval("Insurance_Amount") %>'
                                                    Style="text-align: right; width: 95%" onfocus="this.select()"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insurance IC Amount" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_InsuranceICAmt" runat="server" CssClass="textBox" Text='<%# Eval("Insurance_Amount_IC") %>'
                                                    Style="text-align: right; width: 95%" onfocus="this.select()"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="buttonDefault"
                                                    CommandArgument='<%# Eval("ADBillNo")+"$"+Eval("ShippingBillNo")+"$"+Eval("InvoiceNo")+"$"+Eval("IRMNo") %>'
                                                    ToolTip="Update" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server" visible="false">
                            <td align="left" style="width: 90%" valign="top" colspan="3">
                                <table cellspacing="0" cellpadding="2" width="90%" border="1" frame="void">
                                    <tbody class="gridHeader">
                                        <tr>
                                            <td align="left" width="25%">
                                                &nbsp;Records Per Page :&nbsp;
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
