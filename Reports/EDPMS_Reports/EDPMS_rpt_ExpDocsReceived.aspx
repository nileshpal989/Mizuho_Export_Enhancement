<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_rpt_ExpDocsReceived.aspx.cs"
    Inherits="EDPMS_EDPMS_rpt_ExpDocsReceived" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language="javascript">

        function validateSave() {
            var ddlBranch = document.getElementById('ddlBranch');
            if (ddlBranch.value == 0) {
                alert('Select Branch Name.');
                ddlBranch.focus();
                return false;
            }

            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }
            //  return true;
            var winname = window.open('EDPMS_rpt_ExpDocsReceived_View.aspx?branchid=' + ddlBranch.value + '&fromdate=' + fromDate.value + '&todate=' + toDate.value + '&Header=' + lblPageHeader.innerHTML, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=500');
        }


       
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table border="0" width="100%" cellspacing="2">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom" colspan="2">
                             <asp:Label id="lblPageHeader" runat="server" CssClass="pageLabel"></asp:label>
                                <%--<span class="pageLabel">EDPMS Export Documents Received Report</span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" TabIndex="1"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">From Doc. Date :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="2"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="Button1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;&nbsp; <span class="elementLabel">To Doc. Date :</span>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtToDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="Button2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" height="40px">
                                <asp:Button ID="btnCreate" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Generate" TabIndex="6" />
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
