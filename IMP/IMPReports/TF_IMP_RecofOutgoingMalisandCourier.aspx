<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_RecofOutgoingMalisandCourier.aspx.cs"
    Inherits="IMP_IMPReports_TF_IMP_RecofOutgoingMalisandCourier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        function generateReport() {
            var txtdate = document.getElementById('txtdate');

            if (txtdate.value == "") {
                alert('Please select  Date')
                txtdate.focus();
                return false;
            }

            var winname = window.open('TF_IMP_View_RecofOutgoingMalisandCourier.aspx?txtdate=' + txtdate.value, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=1000,height=500');
            winname.focus();
            return false;

        }
    </script>
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
            <uc1:menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <span class="pageLabel"><strong>Record of Outgoing Registered Mails and Courier Services</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 100px">
                                <span class="mandatoryField">&nbsp; *</span><span class="elementLabel"> Date :</span>
                            </td>
                            <td align="left" style="width: 800px">
                                &nbsp;
                                <asp:TextBox ID="txtdate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                    Width="70" TabIndex="2"></asp:TextBox>
                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="2" />
                                <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtdate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtdate" PopupButtonID="btncalendar_FromDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdfdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtdate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr><td><br /></td></tr>
                        <tr>
                            <td align="right" style="width: 100px">
                            </td>
                            <td align="left" style="width: 800px">
                                <asp:Button ID="Btn_Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Genarate" TabIndex="4" />
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
