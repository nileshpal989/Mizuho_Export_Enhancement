<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_rpt_AdvanceRemittanceReceived.aspx.cs"
    Inherits="EDPMS_EDPMS_rpt_AdvanceRemittanceReceived" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
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

        function validateDisplay() {
            var rdbAllCust = document.getElementById('rdbAllCust');
            var rdbSelectedCust = document.getElementById('rdbSelectedCust');
            var trCustAcNo = document.getElementById('trCustAcNo');

            if (rdbAllCust.checked == true) {
                document.getElementById('lblCustName').innerHTML = "";
                document.getElementById('txtCustAccountNo').value = "";
                trCustAcNo.style.display = 'none';
            }

            if (rdbSelectedCust.checked == true) {
                trCustAcNo.style.display = 'block';
            }
        }

        function validateSave() {
            var txtFromDate = document.getElementById('txtFromDate');
            var txtToDate = document.getElementById('txtToDate');
            var ddlBranch = document.getElementById('ddlBranch');

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
            var rdbAllCust = document.getElementById('rdbAllCust');
            var rdbSelectedCust = document.getElementById('rdbSelectedCust');
            var cust;
            if (rdbAllCust.checked == true) {
                cust = 'All'
            }
            else {
                if (rdbSelectedCust.checked == true) {
                    if (document.getElementById('txtCustAccountNo').value == "") {
                        alert('Enter Customer Account Number');
                        document.getElementById('txtCustAccountNo').focus();
                        return false;
                    }
                    else
                        cust = document.getElementById('txtCustAccountNo').value;
                }
            }
            var winame = window.open('EDPMS_rpt_AdvanceRemittanceReceived_View.aspx?Branch=' + ddlBranch.value + '&FromDate=' + txtFromDate.value + '&ToDate=' + txtToDate.value + '&Cust=' + cust, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            winame.focus();
            return false;
        }

        function OpenCustmerList(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode > 31 && (keycode < 48 || keycode > 57) && keycode != 8 && keycode != 46 && keycode != 16 && keycode != 37 && keycode != 39 && keycode != 46 && keycode != 144 && (keycode < 96 || keycode > 105))
                return false;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('EDPMS_rpt_AdvanceRemittanceReceived_CustHelp.aspx?', 450, 400, 'Customer List');
                return false;
            }
            return true;
        }

        function selectCustomer(id, name) {
            var txtCustAccountNo = document.getElementById('txtCustAccountNo');
            var lblCustName = document.getElementById('lblCustName');
            txtCustAccountNo.value = id;
            lblCustName.innerHTML = name;
            __doPostBack('txtCustAccountNo', '');
            document.getElementById('trCustAcNo').style.display = 'block';
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:Menu ID="Menu1" runat="server" />
        <asp:ScriptManager ID="scriptmanagermain" runat="server">
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="1" cellpadding="1" border="0" width="100%">
                    <tr>
                        <td colspan="4" align="left" nowrap>
                            <span class="pageLabel"><strong>Export Document against Advance Remittances</strong></span>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="5%">
                            <span class="elementLabel">Branch :</span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" Width="120px"
                                TabIndex="1">
                            </asp:DropDownList>
                            &nbsp&nbsp
                            <asp:Label ID="labelMessage" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">From Realisation Date :</span>
                        </td>
                        <td align="left" nowrap width="5%">
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
                        <td align="right" nowrap width="5%">
                            <span class="elementLabel">To Realisation Date :</span>
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
                            <asp:RadioButton ID="rdbAllCust" runat="server" Text="All Customer" class="elementLabel"
                                Checked="true" GroupName="CustomerAccNo" TabIndex="4" />&nbsp;
                        </td>
                        <td colspan="2" align="left" nowrap>
                            <asp:RadioButton ID="rdbSelectedCust" runat="server" Text="Selected Customer" class="elementLabel"
                                GroupName="CustomerAccNo" TabIndex="5" />
                        </td>
                    </tr>
                    <tr id="trCustAcNo" style="display:none;" runat="server">
                        <td width="10%">
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Customer A/c No.&nbsp; :</span>
                        </td>
                        <td  colspan="2" align="left" nowrap>
                            <asp:TextBox ID="txtCustAccountNo" runat="server" CssClass="textBox" AutoPostBack="true"
                                OnTextChanged="txtCustAccountNo_TextChanged" TabIndex="6"></asp:TextBox>
                            <asp:Button ID="btnCustHelp" CssClass="btnHelp_enabled" runat="server" OnClientClick="return  OpenCustmerList('mouseClick');"
                                TabIndex="-1" />
                            <asp:Label ID="lblCustName" CssClass="elementLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" nowrap style="height: 50px">
                            <asp:Button ID="btnCreate" runat="server" Text="Generate Report" CssClass="buttonDefault"
                                ToolTip="Create File" TabIndex="7" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
