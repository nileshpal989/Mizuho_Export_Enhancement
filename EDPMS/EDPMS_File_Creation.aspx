<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_File_Creation.aspx.cs"
    Inherits="EDPMS_EDPMS_File_Creation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function isValidDate(controlID, CName) {
            var obj = controlID;
            if (controlID.value != "__/__/____") {
                var day = obj.value.split("/")[0];
                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];
                var today = new Date();
                if (day == "__") {
                    day = today.getDay();
                }
                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1, day);
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        function validateSave() {
            var txtFromDate = document.getElementById('txtFromDate');
            var txtToDate = document.getElementById('txtToDate');

            if (txtFromDate.value == "") {
                alert('Enter From Date');
                txtFromDate.focus();
                return false;
            }

            if (txtToDate.value == "") {
                alert('Enter To Date');
                txtToDate.focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:Menu ID="Menu1" runat="server" />
      
        <asp:ScriptManager ID="scriptmanagermain" runat="server">
        </asp:ScriptManager>
      <%--  <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">--%>
           
          <%--  <ContentTemplate>--%>
                <table cellspacing="2" cellpadding="2" border="0" width="100%">
                    <tr>
                        <td colspan="2" align="left" nowrap>
                            <span class="pageLabel">EDPMS Data Generation From EXPORT Data</span>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td width="8%" align="right" nowrap>
                            <span class="pageLabel">Branch :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" Width="120px"
                                TabIndex="1">
                            </asp:DropDownList>
                            &nbsp&nbsp
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField" Font-Bold="true"
                                ForeColor="#000066"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="8%" align="right" nowrap>
                            <span class="pageLabel">From Date :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                Width="70px" TabIndex="2" AutoPostBack="true"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Invalid"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="pageLabel">To Date :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal1"
                                Width="70px" TabIndex="3"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtToDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_ToDocDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDocDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                ValidationGroup="dtVal1" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Invalid"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:RadioButton ID="rdbDocBill" Text="Document Bill Lodged" runat="server" CssClass="elementLabel"
                                GroupName="BillType" Checked="true" TabIndex="4" />
                            &nbsp;
                            <asp:RadioButton ID="rdbRealised" Text="Export Bill Realised" runat="server" CssClass="elementLabel"
                                GroupName="BillType" TabIndex="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" nowrap style="height: 50px">
                            <asp:Button ID="btnCreate" runat="server" Text="Create File" CssClass="buttonDefault"
                                ToolTip="Create File" OnClick="btnCreate_Click" TabIndex="6" Style="height: 26px" />
                        </td>
                    </tr>
                </table>
<%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>
