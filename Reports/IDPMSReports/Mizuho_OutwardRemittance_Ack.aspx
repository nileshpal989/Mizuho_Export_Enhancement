<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mizuho_OutwardRemittance_Ack.aspx.cs"
    Inherits="IDPMS_Mizuho_OutwardRemittance_Ack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">

        function generateReport() {

            var brname = document.getElementById('ddlBranch');
            var rdball = document.getElementById('rdball');
            var rdbsucessful = document.getElementById('rdbsuccessful');
            var rdbonlyerror = document.getElementById('rdberror');
            var rdbORM = document.getElementById('rdbORM');

            var ErrorCode = "";
            var modtype = "";

            var type = "";

            if (brname.value == "---Select---") {

                alert('Please select branchname');
                brname.focus();
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



            if (rdball.checked == true) {

                ErrorCode = 'All';

            }

            else if (rdbsucessful.checked == true) {

                ErrorCode = 'SUCCESS';

            }
            else if (rdbonlyerror.checked == true) {
                ErrorCode = 'Error';

            }

            if (rdbORM.checked == true) {
                type = 'ORM';
            }
            else {
                type = 'closure';
            }

            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;

            var winame = window.open('View_outwardRemAck.aspx?branchname=' + brname.value + '&ErrorCode=' + ErrorCode + '&from=' + from + '&to=' + to + '&type=' + type, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            winame.focus();
            return false;
            //return true;
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
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc2:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left">
                                <span class="pageLabel">Outward Remittance (RBI-Acknowledgement)</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">From Ack Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                Width="70" TabIndex="2"></asp:TextBox>
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
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Ack kDate:</span>
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
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:RadioButton ID="rdbORM" runat="server" Text="ORM" CssClass="elementLabel" Checked="true"
                                                GroupName="type" />
                                            <asp:RadioButton ID="rdbORMClosure" runat="server" CssClass="elementLabel" Text="ORM Closure"
                                                GroupName="type" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:RadioButton ID="rdball" runat="server" Text="All" CssClass="elementLabel" Checked="true"
                                                GroupName="ErrorCode" />
                                            <asp:RadioButton ID="rdbsuccessful" runat="server" CssClass="elementLabel" Text="Successful"
                                                GroupName="ErrorCode" />
                                            <asp:RadioButton ID="rdberror" runat="server" CssClass="elementLabel" Text="Only Error"
                                                GroupName="ErrorCode" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr valign="bottom">
                                        <td align="right" style="width: 10%">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="4" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
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
