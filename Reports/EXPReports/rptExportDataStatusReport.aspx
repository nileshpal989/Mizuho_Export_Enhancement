<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExportDataStatusReport.aspx.cs"
    Inherits="Reports_EXPReports_rptExportDataStatusReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/ajax-loader.gif"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/ajax-loader.gif" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
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

        function validateGenerate() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }

            var txtToDate = document.getElementById('txtToDate');
            if (txtToDate.value == '') {
                alert('Select To Date.');
                txtToDate.focus();
                return false;
            }

            if (document.getElementById('rdbSelectedBranch').checked == true) {
                var Frdocno = document.getElementById('ddlBranch');
                if (Frdocno.value == '0') {
                    alert('Select Branch.');
                    txtDocumentNo.focus();
                    return false;
                }
            }
            var DocumentNo;
            var Branch;
            if (document.getElementById('rdbAllBranch').checked == true) {
                Branch = 'All';
            }
            if (document.getElementById('rdbSelectedBranch').checked == true) {
                Branch = document.getElementById('ddlBranch').value;
            }
            var fromDate = document.getElementById('txtFromDate');
            var txtToDate = document.getElementById('txtToDate');
            var winname = window.open('ViewrptExportDataStatusReport.aspx?fromDate=' + fromDate.value + '&toDate=' + txtToDate.value + '&Branch=' + Branch, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');

            winname.focus();
            return false;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 86px;
        }
        .style2
        {
            width: 22%;
        }
        .style3
        {
            width: 74px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Export Data Status</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td align="left" nowrap width="5%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" width="10%" nowrap>
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="1"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="8" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td align="right" nowrap width="5%">
                                            <span class="elementLabel">&nbsp;To Date :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal1" Width="70px" TabIndex="2"></asp:TextBox>
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
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td height="40px" align="right" nowrap class="style2">
                                            <asp:RadioButton ID="rdbAllBranch" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Checked="true" GroupName="Data" Text="All Branch" TabIndex="3" OnCheckedChanged="rdbAllBranch_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td height="40px" nowrap>
                                            <asp:RadioButton ID="rdbSelectedBranch" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Branch" GroupName="Data" TabIndex="3" OnCheckedChanged="rdbSelectedBranch_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <table id="tblBranch" runat="server" border="0" visible="false">
                                        <tr>
                                            <td width="100px">
                                            </td>
                                            <td align="right" class="style3">
                                                <span class="elementLabel" id="Doccode" runat="server">&nbsp;Branch :</span>
                                            </td>
                                            <td height="30px" nowrap>
                                                &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="4" Width="100px"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr valign="bottom">
                                            <%--<td align="left" style="width: 240px" padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                 &nbsp;
                                                <asp:Button ID="Button2" runat="server" CssClass="buttonDefault" Text="Generate Swift File"
                                                    ToolTip="Generate" TabIndex="6" Width="135px" onclick="btnCreateFile_Click" />
                                            </td>--%>
                                            <td align="left" style="width: 175px; padding-top: 10px; padding-bottom: 10px">
                                            </td>
                                            <td align="left" style="width: 600px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                &nbsp;
                                                <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate Report"
                                                    Width="135px" ToolTip="Generate" TabIndex="7" />
                                            </td>
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </tr>
                        </tr>
                    </table>
                    <input type="hidden" runat="server" id="hdnFromDate" />
                    <input type="hidden" runat="server" id="hdnToDate" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
