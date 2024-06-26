﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_LCGiventoFirstFlight.aspx.cs"
    Inherits="Reports_EXPORTReports_EXP_LCGiventoFirstFlight" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
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
                document.getElementById('btnSave').focus();
            }
        }

        function changeDate() {

            __doPostBack('btnChangeDate', '');
        }

    </script>
    <script language="javascript" type="text/javascript">

        function validateSave() {

            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Enter From Date.');
                fromDate.focus();
                return false;
            }


            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;

            var branch = document.getElementById('ddlBranch').value;

            if (branch == 0) {

                alert('Please Select Branch');
                document.getElementById('ddlBranch').focus();
                return false;
            }
            if (branch == 01) {

                branch = "All";
            }

            var winname = window.open('ViewExpLCGiventoFirstFlight.aspx?frm=' + from + '&to=' + to + '&branch=' + branch, '', 'scrollbars=yes,left=0,top=50,maximizeButton=yes,menubar=0,width=850,height=550');
            winname.focus();

            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <%-- <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>--%>
                <ContentTemplate>
                    <table border="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">LC's Given to First Flight</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td align="left" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Select Branch :</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBranch" runat="server" Width="125px" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
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
                                            </ajaxToolkit:MaskedEditValidator>&nbsp;
                                            <%-- TO DATE--%>
                                            <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="70" TabIndex="3"></asp:TextBox>
                                            <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
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
                                    <%--<tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                </table>
                                <%--<table id="tblBranch" runat="server" cellspacing="0" cellpadding="0" border="0" width="400px"
                                    style="line-height: 150%">
                                    <tr>
                                        <td height="40px" align="center" valign="bottom">
                                            <asp:RadioButton ID="rdbAllBranch" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data" Text="All Branches" Checked="true" TabIndex="3" OnCheckedChanged="rdbAllBranch_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td height="40px" valign="bottom">
                                            <asp:RadioButton ID="rdbSelectedBranch" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Branch" GroupName="Data" TabIndex="4" OnCheckedChanged="rdbSelectedBranch_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 110px">
                                            <fieldset id="SelectBranch" runat="server" visible="false" style="width: 170px">
                                                <span id="Span1" class="elementLabel" runat="server">Branch :&nbsp; </span>
                                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="5" Width="100px"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>--%>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 80px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate" TabIndex="4" />
                                        </td>
                                    </tr>
                                </table>
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
