<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EXP_Realisation_Excel_Report.aspx.cs" Inherits="Reports_EXPORTReports_TF_EXP_Realisation_Excel_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
     <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <script src="../../Help_Plugins/MyJquery1.js" language="javascript" type="text/javascript"></script>
    <link id="Link1" runat="server" rel="shortcut icon" href="../../Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../../Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css"  />
    <script type="text/javascript">
        function CallConfirmBox() {

            /*$("[id$=btngeneratereport]").click();*/
            document.getElementById("btngeneratereport").click();

        }
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

                //              if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        
    </script>
    <style type="text/css">
        .modal {
    position: fixed;
    top: 0;
    left: 0;
    background-color: transparent;
    z-index: 99;
    -moz-opacity: 0.8;
    min-height: 100%;
    width: 100%;
}

.loading {
    font-family: Arial;
    font-size: 10pt;
    border: 5px solid navy;
    width: 400px;
    height: 40px;
    display: none;
    position: absolute;
    background-color: white;
    z-index: 999;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div class="loading" align="center">
            Please wait while the Report is Generating...<br />
            <br />
            <img src="../../Images/ProgressBar1.gif" alt="" />
        </div>
        
        
        <div>
            <center>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
            </center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <Triggers>                       
                        <asp:PostBackTrigger ControlID="btngenerate" />                        
                        <asp:PostBackTrigger ControlID="btngeneratereport" />
                    </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Export Realisation Report</strong></span>
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
                                        <td nowrap width="5%" align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td colspan="3">&nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="100px"
                                            runat="server">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Realized Date
                                                :</span>
                                        </td>
                                        <td align="left" width="10%" nowrap>&nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
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
                                            <span class="elementLabel">&nbsp;To Realized Date :</span>
                                        </td>
                                        <td align="left" nowrap>&nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal1"
                                            Width="70px" TabIndex="2"></asp:TextBox>
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
                                        <td  align="left"  >

                                        </td>
                                        <td align="left" nowrap>&nbsp;
                                            <asp:Button ID="btngenerate" Text="Generate" runat="server" CssClass="buttonDefault" OnClick="btnGenerate_Click"/>
                                            <asp:Button ID="btngeneratereport" Text="Generate" runat="server" CssClass="buttonDefault" OnClick="btnGeneratereport_Click" Style="visibility:hidden"/>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
