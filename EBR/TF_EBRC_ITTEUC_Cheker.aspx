﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ITTEUC_Cheker.aspx.cs" Inherits="EBR_TF_EBRC_ITTEUC_Cheker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen"/>    
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../Help_Plugins/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="../Help_Plugins/jquerynew.min.js" type="text/javascript"></script>
    <link href="../Help_Plugins/jquery-ui.css" rel="stylesheet" />
    <script src="../Help_Plugins/jquery-ui.js" type="text/javascript"></script>    
    <script src="../IMP/Scripts/TF_EBRC_ITTEUC.js" type="text/javascript"></script>  
    <link href="../Style/TF_EBRC_ORM.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>                
        <div class="loading" align="center">
            Please wait while the trasactions are Approving..<br />
            <br />
            <img src="../Images/ProgressBar1.gif" alt="" />
        </div>
        <div>
            <center>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnapprove" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" valign="bottom" colspan="2">
                                    <span class="pageLabel"><strong>EBRC ITT EUC - Checker</strong></span>
                                </td>
                                <td align="right" style="width: 50%">
                                    <asp:Label ID="lblLink" runat="server" CssClass="elementLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top" colspan="3">
                                    <hr />
                                </td>
                            </tr>
                            <tr align="right">
                                <td width="15%" align="left">
                                    <span class="elementLabel">Branch :</span>
                                    <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td width="40%" align="left">
                                    <span class="elementLabel">Uploaded Date :</span>
                                    <asp:TextBox ID="txtUploadedDate" runat="server" CssClass="textBox" MaxLength="10"
                                        ValidationGroup="dtVal" Width="70px" TabIndex="15" AutoPostBack="true" OnTextChanged="txtUploadedDate_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="ME_UploadedDate" Mask="99/99/9999" MaskType="Date"
                                        runat="server" TargetControlID="txtUploadedDate" ErrorTooltipEnabled="True"
                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <asp:Button ID="btnCal_UploadedDate" runat="server" CssClass="btncalendar_enabled" />
                                    <ajaxToolkit:CalendarExtender ID="CE_UploadedDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtUploadedDate" PopupButtonID="btnCal_UploadedDate" Enabled="True">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="MV_UploadedDate" runat="server" ControlExtender="ME_UploadedDate"
                                        ValidationGroup="dtVal" ControlToValidate="txtUploadedDate" EmptyValueMessage="Enter Date Value"
                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                </td>
                                <td width="45%" align="right">
                                    <asp:CheckBox ID="chkapproveall" runat="server" OnCheckedChanged="chkapproveall_CheckedChanged" AutoPostBack="true"
                                        Style="width: 200px; height: 200px;" />
                                    <asp:Button ID="btnapproveall" runat="server" Style="display: none;" CssClass="buttonDefault" OnClick="btnapprove_Click" />
                                    <asp:Button ID="btnapprove" Text="Approve All" CssClass="buttonDefault" runat="server" OnClientClick="return confirmapprove();" />

                                    <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                        CssClass="textBox" MaxLength="40" Width="180px" TabIndex="6"></asp:TextBox>
                                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                        ToolTip="Search" TabIndex="6" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%;" valign="top" colspan="3">
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" border="0" width="100%">
                            <tr id="rowGrid" runat="server">
                                <td align="left" style="width: 100%" valign="top">
                                    <asp:GridView ID="GridViewEBRCEntryList" runat="server" AutoGenerateColumns="False"
                                        Width="100%" AllowPaging="false" OnRowCommand="GridViewEBRCEntryList_RowCommand" OnRowDataBound="GridViewEBRCEntryList_RowDataBound"
                                        CssClass="GridView">
                                        <PagerSettings Visible="false" />
                                        <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                        <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                        <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                            CssClass="gridAlternateItem" />
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Bank Unique Transaction Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankUniqueTransactionId" runat="server" Text='<%# Eval("BankUniqueTransactionId") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank Reference number" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankRefNo" runat="server" Text='<%# Eval("BankRefNo") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Sr No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Eval("srno") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="IRM ISSUE DATE" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIRMIssueDate" runat="server" Text='<%# Eval("DOC_DATE","{0:dd/MM/yyyy}") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IRM Number " HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIRMNo" runat="server" Text='<%# Eval("DOC_NO") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="IRM Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIRMStatus" runat="server" Text='<%# Eval("IRMStatus") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="IFSC Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIFSCCode" runat="server" Text='<%# Eval("IFSC_Code") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Remittance AD Code" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemittanceADCode" runat="server" Text='<%# Eval("RemittanceADCode") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%-- <asp:TemplateField HeaderText="Remittance date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemittancedate" runat="server" Text='<%# Eval("Remittancedate","{0:dd/MM/yyyy}") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Remittance FCC" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemittanceFCC" runat="server" Text='<%# Eval("CURR") %>'
                                                        CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remittance FCC Amount" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemittanceFCCAmount" runat="server" Text='<%# Eval("FCAMOUNT") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="INR Credit Amount" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblINRCreditAmt" runat="server" Text='<%# Eval("INRCreditAmount") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="IEC Code" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIECCode" runat="server" Text='<%# Eval("IE_CODE") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            
                                            <asp:TemplateField HeaderText="Remitter Name" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemitterName" runat="server" Text='<%# Eval("REMITTER_NAME") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remitter country" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemittercountry" runat="server" Text='<%# Eval("COUNTRY_OF_AC_HOLDER") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purpose code" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurpose" runat="server" Text='<%# Eval("PCODE") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Checker Status" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Push Status" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpushstatus" runat="server" Text='<%# Eval("PUSH_STATUS") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Get Status" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgetstatus" runat="server" Text='<%# Eval("GET_STATUS") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve All" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="HeaderChkAllow" runat="server" AutoPostBack="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="RowChkAllow" runat="server" OnCheckedChanged="RowChkAllow_CheckedChanged" AutoPostBack="true" />
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
                                                <td align="left" style="width: 25%">&nbsp;Records per page:&nbsp;
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
