<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expswift_CheckerView.aspx.cs"
    Inherits="EXP_Expswift_CheckerView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
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

        function validate() {

            var branchCode = document.getElementById('ddlBranch1');
            if (branchCode.value == '') {
                alert('Enter Branch Code');
                branchCode.focus();
                return false;
            }
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>Export Swift View - Checker <b></span>
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
                            <td width="10%" align="left">
                                <span class="elementLabel">Branch :</span>
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;&nbsp; 
                                <span class="elementLabel">Creation Date :</span>
                                <asp:TextBox ID="txtLodgementDate" runat="server" CssClass="textBox" MaxLength="10"
                                    ValidationGroup="dtVal" Width="70px" TabIndex="15" AutoPostBack="true" OnTextChanged="ddlBranch_SelectedIndexChanged"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_LodgementDate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtLodgementDate" ErrorTooltipEnabled="True"
                                    CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                    CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btnCal_LodgementDate" runat="server" CssClass="btncalendar_enabled" />
                                <ajaxToolkit:CalendarExtender ID="CE_LodgementDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtLodgementDate" PopupButtonID="btnCal_LodgementDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MV_LodgementDate" runat="server" ControlExtender="ME_LodgementDate"
                                    ValidationGroup="dtVal" ControlToValidate="txtLodgementDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAll" runat="server" Text="All" AutoPostBack="true" Checked="true"
                                    GroupName="Status" OnCheckedChanged="rdbAll_CheckedChanged" Font-Bold="true" />
                                <asp:RadioButton ID="rdbApproved" runat="server" Text="Approved" Font-Bold="true"
                                    AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbApproved_CheckedChanged"
                                    ForeColor="GreenYellow" />
                                <asp:RadioButton ID="rdbRejected" runat="server" Text="Rejected" Font-Bold="true"
                                    AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbRejected_CheckedChanged"
                                    ForeColor="Tomato" />
                            </td>
                            <td align="right">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="6"
                                    AutoPostBack="true" OnTextChanged="ddlBranch_SelectedIndexChanged"></asp:TextBox>
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
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:GridView ID="ExportViewChecker" runat="server" AutoGenerateColumns="false" Width="90%"
                                    GridLines="Both" AllowPaging="true" OnRowCommand="ExportViewChecker_RowCommand"
                                    OnRowDataBound="ExportViewChecker_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trans Ref No" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransrefno" runat="server" Text='<%# Eval("Trans_RefNo") %>' CssClass="elementLabel"></asp:Label>
                                                <asp:Label ID="lblApprovedate" runat="server" Visible="false" Text='<%# Eval("ApproveDate") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Swift Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSwifttype" runat="server" Text='<%# Eval("Swift_Type") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Swift Status" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSwift_Status" runat="server" Text='<%# Eval("Swift_Status") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maker ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMakerID" runat="server" Text='<%# Eval("MakerID") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Checker ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheckerID" runat="server" Text='<%# Eval("CheckerID") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Creation Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreationDate" runat="server" Text='<%# Eval("Creation_Date","{0:dd/MM/yyyy hh:mm tt}") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" colspan="2" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" width="25%">
                                                &nbsp;Records Per Page :&nbsp;
                                                <asp:DropDownList ID="ddlrecordpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordpage_SelectedIndexChanged">
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
