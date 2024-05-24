<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Export_Pending_Authorization_Transaction_Report.aspx.cs" Inherits="Reports_EXPORTReports_Export_Pending_Authorization_Transaction_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" />
    <link href="../../Style/style_new.css" rel="stylesheet" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Help_Plugins/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="../../Help_Plugins/jquerynew.min.js" type="text/javascript"></script>
    <link href="../../Help_Plugins/jquery-ui.css" rel="stylesheet" />
    <script src="../../Help_Plugins/jquery-ui.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        function toDate() {

            if (document.getElementById('txtFromDate').value != "__/__/____") {

                var toDate;
                toDate = dd + '/' + mm + '/' + yyyy;
                document.getElementById('txtToDate').value = toDate;
            }
        }
        function validateSave() {
            var Branch = document.getElementById('ddlBranch');
            var Frmdate = document.getElementById('txtFromDate');
            var Todate = document.getElementById('txtToDate');
            if (Branch.value == '0') {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }
            if (Frmdate.value == '') {
                alert('Enter From Date..');
                Frmdate.focus();
                return false;
            }
            if (Todate.value == '') {
                alert('Enter To Date..');
                Todate.focus();
                return false;
            }
            else {
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
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div class="loading" align="center">
            Please wait while the report is generating..<br />
            <br />
            <img src="../../Images/ProgressBar1.gif" alt="" />
        </div>
        <div>
            <center>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnGenerate" />
                        <asp:PostBackTrigger ControlID="btnHidden" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <span class="pageLabel"><strong>Export Pending Authorization Tansaction Report</strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 100%" valign="top">
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                    <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                        <tr>
                                            <td width="12.5%" align="right" nowrap>
                                                <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                            </td>
                                            <td align="left" nowrap>&nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="true" Width="100px" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                        <tr>
                                            <td height="10px" align="left" valign="middle" width="150px"></td>
                                        </tr>
                                    </table>

                                    <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                        <tr>
                                            <td align="right" style="width: 100px">
                                                <span class="mandatoryField">*</span><span class="elementLabel">From DOC Date :</span>
                                            </td>
                                            <td align="left" style="width: 700px">&nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
                                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                    TabIndex="-1" />
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
                                                &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To DOC Date :</span>
                                                &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="3"></asp:TextBox>
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
                                    </table>
                                    <table cellspacing="0" border="0">
                                        <tr>
                                            <td height="40px" align="left" valign="middle" width="150px">
                                                <asp:RadioButton ID="rbdlodgement" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data" Text="Lodgement" Style="forecolor: #000000; font-weight: bold;"
                                                    TabIndex="24" Checked="true" 
                                                    oncheckedchanged="rbdlodgement_CheckedChanged" />
                                            </td>
                                            <td height="20px" valign="middle" width="150px">
                                                <asp:RadioButton ID="rdbsettlement" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data" Style="forecolor: #000000; font-weight: bold;" TabIndex="27"
                                                    Text="Settlement" oncheckedchanged="rdbsettlement_CheckedChanged" />
                                            </td>
                                            <td height="40px" valign="middle" width="170px">
                                                <asp:RadioButton ID="rdbIRM" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data" Style="forecolor: #000000; font-weight: bold;" TabIndex="36"
                                                    Text="IRM" oncheckedchanged="rdbIRM_CheckedChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr valign="top">
                                            <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="top">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp;
                                            <asp:Button ID="btnGenerate" runat="server" CssClass="buttonDefault" Text="Download"
                                                ToolTip="Download" TabIndex="41" OnClick="btnGenerate_Click" />
                                                <asp:Button ID="btnHidden" runat="server" OnClick="btnHidden_Click" Style="display: none" />
                                                <td>&nbsp;
                                                </td>
                                                <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                                <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            </td>
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
<script type="text/javascript">

    var addHandler = function (element, type, handler) {
        if (element.addEventListener) {
            element.addEventListener(type, handler, false);
        } else if (element.attachEvent) {
            element.attachEvent("on" + type, handler);
        } else {
            element["on" + type] = handler;
        }
    };

    var preventDefault = function (event) {
        if (event.preventDefault) {
            event.preventDefault();
        } else {
            event.returnValue = false;
        }
    };

    addHandler(window, "contextmenu", function (event) {
        preventDefault(event);
    });
    document.onkeydown = function (event) {
        if (event.keyCode == 123) {                   // Prevent F12
            return false;
        }
        else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { // Prevent Ctrl+Shift+I        
            return false;
        }
    };


</script>
</html>

