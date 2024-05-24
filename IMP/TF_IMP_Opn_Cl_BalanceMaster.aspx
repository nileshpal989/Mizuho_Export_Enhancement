<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Opn_Cl_BalanceMaster.aspx.cs"
    Inherits="IMP_TF_IMP_Opn_Cl_BalanceMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validateSave() {
            var ddlCurr = document.getElementById('ddlCurr');
            var txtBalanceQty = document.getElementById('txtBalanceQty');
            var txtCloseBalance = document.getElementById('txtCloseBalance');
            if (ddlCurr.value == '') {
                alert('Please select Currency.');
                ddlCurr.focus();
                return false;
            }
            if (txtBalanceQty.value == '') {
                alert('Please Enter Balance Quantity');
                txtBalanceQty.focus();
                return false;
            }
            if (txtCloseBalance.value == '') {
                alert('Please Enter Balance Amount.');
                txtCloseBalance.focus();
                return false;
            }
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left">
                                <span class="pageLabel"><strong>Drafts Closing Balance Master</strong></span>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    AutoPostBack="true" ToolTip="Back" TabIndex="5" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" Font-Bold="true" Font-Size="Small" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency:</span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:DropDownList ID="ddlCurr" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCurr_OnSelectedIndexChanged" Width="70px" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Date:</span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txt_AcceptanceDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70px" TabIndex="8"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="ME_Accept_Date" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txt_AcceptanceDate" ErrorTooltipEnabled="True"
                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncal_Accept_Date" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:CalendarExtender ID="CE_Accept_Date" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txt_AcceptanceDate" PopupButtonID="btncal_Accept_Date" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MV_Accept_Date" runat="server" ControlExtender="ME_Accept_Date"
                                                ValidationGroup="dtVal" ControlToValidate="txt_AcceptanceDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Closing Bal Qty:</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBalanceQty" runat="server" CssClass="textBox" MaxLength="3" Width="60px"
                                                TabIndex="2" onkeyDown="return validate_Number(event);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Closing Bal:</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCloseBalance" runat="server" CssClass="textBox" MaxLength="16"
                                                TabIndex="3" onkeyDown="return validate_Number(event);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" OnClick="btnSave_Click"
                                                ToolTip="Save" TabIndex="4" OnClientClick="return validateSave();" />
                                        </td>
                                    </tr>
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
