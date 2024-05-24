<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EXP_DIGI_Settlement_Report.aspx.cs" Inherits="Reports_EXPORTReports_TF_EXP_DIGI_Settlement_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .container-1 {
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 100%;
        }

        .left {
            flex: 1;
            text-align: left;
        }

        .right {
            flex: 1;
            text-align: right;
        }

        .container-2 {
            border: 2px solid #ccc;
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 100%;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            flex-direction: column;
            align-items: flex-start;
        }

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
        function ClickAnotherButton() { 
            document.getElementById('btndwnld').click();                       
        }       
        function validateGenerate() {
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
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger  ControlID="btnSave" />
                        <asp:PostBackTrigger  ControlID="btndwnld" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="container-1">
                            <div class="left">
                                <span class="pageLabel"><strong>Export Bill Realisation</strong></span>
                            </div>
                            <div class="right">
                                <asp:Label ID="lblpath" runat="server" CssClass="elementLabel" ForeColor="Red" />
                            </div>
                        </div>
                        <div>
                            <div>
                                <hr />
                            </div>

                            <div align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <div>
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </div>
                                <div class="container-2">
                                    <div>
                                        <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>&nbsp;&nbsp;&nbsp; 
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                Width="100px" runat="server">
                            </asp:DropDownList>
                                    </div>
                                    <br />
                                    <div>
                                        <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                            TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                            TabIndex="-1" />
                                        &nbsp;<ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                            ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                        </ajaxToolkit:MaskedEditValidator>
                                        &nbsp;&nbsp;&nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                        &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="3"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                            ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                        </ajaxToolkit:MaskedEditValidator>
                                    </div>

                                    <div>
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate Report" OnClick="btnSave_Click"
                                ToolTip="Generate Report" TabIndex="41" />
                                        <asp:Button ID="btndwnld" runat="server" OnClick="btnexcel_Click" Style="visibility: hidden" />
                                    </div>
                                    <div>
                                        <asp:Label ID="lblmsg" runat="server" CssClass="mandatoryField"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>
        </div>
    </form>
</body>
</html>
