<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Cash_equivalent_VouchersBalance.aspx.cs"
    Inherits="IMP_IMPReports_TF_IMP_Cash_equivalent_VouchersBalance" %>

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
        function validateGenerate() {
            var fromDate = document.getElementById('txtfrmdate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var txtToDate = document.getElementById('txttodate');
            if (txtToDate.value == '') {
                alert('Select To Date.');
                txtToDate.focus();
                return false;
            }
            var fromDate = document.getElementById('txtfrmdate');
            var txtToDate = document.getElementById('txttodate');
            var Currency = document.getElementById('ddlcurrency').value;

            var winname = window.open('View_TF_IMP_Cash_equivalent_VouchersBalance.aspx?fromDate=' + fromDate.value + '&toDate=' + txtToDate.value + '&Currency=' + Currency, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=1310,height=550');
            winname.focus();
            return false;
        }
    </script>
    <style type="text/css">
        hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 2px;
        }
        .style2
        {
            width: 100px;
            height: 24px;
        }
        .style3
        {
            height: 24px;
            width: 383px;
        }
        .style6
        {
            width: 168px;
        }
        .style7
        {
            width: 383px;
        }
        .style8
        {
            width: 318px;
        }
        .style9
        {
            width: 100px;
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
            <uc1:menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Notes/Cash-equivalent Vouchers Balance Control Sheet</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td align="center" class="style2">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency :</span>
                                        </td>
                                        <td align="left" class="style3">
                                            <asp:DropDownList ID="ddlcurrency" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="50px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%-- -----------------------------------FROM DATE--------------------------------------------------------%>
                                        <td align="center" class="style9">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel"> From Date :</span>
                                        </td>
                                        <td align="left" class="style7">
                                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                                Width="70" TabIndex="2"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfrmdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtfrmdate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtfrmdate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfrmdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtfrmdate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <%---------------------------------------------------------------------------------------TO DATE---------------------------------------------------------------%>
                                    <tr>
                                        <td align="center" class="style9">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel"> To Date :</span>
                                        </td>
                                        <td align="left" class="style7">
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="2"
                                                ValidationGroup="dtval" Width="70"></asp:TextBox>
                                            <asp:Button ID="btncalendar_Todate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdtodate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txttodate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="Calendartodate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txttodate" PopupButtonID="btncalendar_Todate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdtodate"
                                                ValidationGroup="dtVal" ControlToValidate="txttodate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table id="selectedcust" visible="false" runat="server" cellspacing="0" border="0"
                                    width="100%">
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="4" />
                                        </td>
                                    </tr>
                                </table>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
