<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EXP_ExportDigiLodgementReport.aspx.cs" Inherits="Reports_EXPORTReports_TF_EXP_ExportDigiLodgementReport" %>

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
    <script type="text/javascript">  
        function CallConfirmBox() {

            $("[id$=btnHidden]").click();

        }
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
    <form id="form2" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div class="loading" align="center">
            Please wait while the report is generating..<br />
            <br />
            <img src="../../Images/ProgressBar1.gif" alt="" />
        </div>
        <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
        <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
        <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <div>
            <center>
                <uc1:Menu ID="Menu1" runat="server" />
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnHidden" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <span class="pageLabel"><strong>EXPORT Digi Lodgement / Settlement</strong></span>
                                </td>
                                <td align="right" style="width: 50%">&nbsp;
                                <input type="hidden" id="hdnCustId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top" colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 100%" valign="top" colspan="2">
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                    <table cellspacing="0" border="0">
                                        <tr>
                                            <td height="40px" align="left" valign="middle" width="150px">
                                                <asp:RadioButton ID="rdbLodgment" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Datafor" Text="Lodgment"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="24" Checked="True" />
                                            </td>
                                            <td height="20px" valign="middle" width="150px">
                                                <asp:RadioButton ID="rdbSettlement" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Settlement" GroupName="Datafor"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="25" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                        <tr>
                                            <td width="10%" align="right" nowrap>
                                                <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                            </td>
                                            <td align="left" nowrap>&nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 100px">
                                                <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
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
                                                &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
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
                                    <table>
                                        <tr valign="bottom">
                                            <td align="right" style="width: 120px"></td>
                                            <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">&nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate" TabIndex="7" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnHidden" runat="server" OnClick="btnHidden_Click" Style="display: none" />
                                                <div>
                                                    <asp:Label ID="lblmsg" runat="server" CssClass="mandatoryField"></asp:Label>
                                                </div>
                                                <asp:Button ID="Button2" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                                <asp:Button ID="Button3" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            </td>
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </tr>
                                    </table>
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
