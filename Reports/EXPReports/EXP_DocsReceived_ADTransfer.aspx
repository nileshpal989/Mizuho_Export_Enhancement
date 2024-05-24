<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_DocsReceived_ADTransfer.aspx.cs"
    Inherits="Reports_EXPReports_EXP_DocsReceived_ADTransfer" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../../Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript" src="../../Scripts/Validations.js"></script>
    <script src="../../Scripts/Enable_Disable_Opener.js" language="javascript" type="text/javascript"></script>
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
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Enter From Date.');
                fromDate.focus();
                return false;
            }
            var toDate = document.getElementById('txtToDate');

            if (toDate.value == '') {
                alert('Enter To Date.');
                txtToDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;
            var ddlBranch = document.getElementById('ddlBranch').value;

            var winname = window.open('EXP_DocsReceived_ADTransfer_View.aspx?branchid=' + ddlBranch + '&fromdate=' + from + '&todate=' + to, '', 'scrollbars=yes,left=0,top=50,maximizeButton=yes,menubar=0,width=1200,height=500');
            winname.focus();
            return false;
        }    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
        <uc1:Menu ID="Menu1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table width="100%" cellspacing="3" cellpadding="3">
                    <tr>
                        <td align="left" colspan="2">
                            <span class="pageLabel"><strong>EXPORT Documents Received Report-For AD Transfer</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap width="10%">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Branch :</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                Width="100px" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                        </td>
                        <td align="left" style="width: 700px">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                Width="70" TabIndex="1"></asp:TextBox>
                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled" />
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
                            </ajaxToolkit:MaskedEditValidator>&nbsp;
                            <%-- TO DATE--%>
                            <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                Width="70" TabIndex="2"></asp:TextBox>
                            <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                            </ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel"></span>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnGenrate" runat="server" CssClass="buttonDefault" Text="Generate"
                                TabIndex="3" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
