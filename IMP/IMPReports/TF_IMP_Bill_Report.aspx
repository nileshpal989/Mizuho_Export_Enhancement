<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Bill_Report.aspx.cs" Inherits="IMP_IMPReports_TF_IMP_Bill_Report" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery-1.8.3.min.js"type="text/javascript""></script>
    <script src="../../Scripts/jquery.min.js" language="javascript" type="text/javascript"></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/Myjquery2.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        function validateSave() {
            //var btndownload = document.getElementById('btndownload');
            var fromDate = document.getElementById('txtfromDate');
            if (fromDate.value == '') {
                VAlert('Select From Date.', '#fromDate');
                return false;
            }
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                VAlert('Select To Date.', '#toDate');
                return false;
            }
            MyConfirm('Do you want to download this report?', '#btndownload');
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
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
            <uc1:menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
                  <asp:PostBackTrigger ControlID="btndownload" />
                   </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" colspan="2">
                                <span class="pageLabel"><strong>Import Bill Register</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <span class="mandatoryField">*</span><span class="elementLabel">Branch:</span>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" TabIndex="1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="10%">
                            </td>
                            <td align="left" width="90%">
                                <span class="mandatoryField">*</span><span class="elementLabel">From Lodgement Date:</span>
                                <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                    Width="70px" TabIndex="2"></asp:TextBox>
                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="2" />
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
                                &nbsp;&nbsp;&nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To
                                    Lodgement Date:</span>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70px"
                                    TabIndex="3"></asp:TextBox>
                                <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="3" />
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
                                <asp:Button ID="btnCreate" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Generate" TabIndex="7" OnClientClick="return validateSave();" />
                                <asp:Button ID="btndownload" Style="display: none;" runat="server" OnClick="btndownload_Click" />
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
