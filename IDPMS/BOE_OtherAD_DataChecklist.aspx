﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BOE_OtherAD_DataChecklist.aspx.cs" Inherits="IDPMS_BOE_OtherAD_DataChecklist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    function GetToDate() {

        var fromdate = document.getElementById('txtfromDate');
        var toDate = document.getElementById('txttodate');

        var fromdateyyyy = fromdate.value.split("/")[2];
        var fromdatemm = fromdate.value.split("/")[1];
        var fromdatedd = fromdate.value.split("/")[0];

        if (fromdate.value == '' || fromdate.value == '__/__/____') {
            alert('Select From Date.');
            document.getElementById('txtFromDate').focus();
            return false;
        }
        else {

            var calDt = new Date(parseFloat(fromdateyyyy), parseFloat(fromdatemm), 0);

            toDate.value = calDt.format("dd/MM/yyyy");
        }
    }
         
    </script>
    <script type="text/javascript">

        function generateReport() {
//            var ddlBranch = document.getElementById('ddlBranch');
//            if (ddlBranch.value == 0) {
//                alert('Select Branch Name.');
//                ddlBranch.focus();
//                return false;
//            }

            var fromDate = document.getElementById('txtfromDate');
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

            var winname = window.open('VIEW_BOE_OtherAD_DataChecklist.aspx?frm=' + fromDate.value + '&to=' + toDate.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=500');
        }
        
    </script>
    <%--<script type="text/javascript" language="javascript">

        function generateReport() {

//            var brname = document.getElementById('ddlBranch');
            var frmdate = document.getElementById('txtfromDate');
            var todate = document.getElementById('txtToDate');
//            var modtype = "";
//            if (brname.value == "---Select---") {

//                alert('Please select branchname');
//                brname.focus();
//                return false;

//            }

            if (frmdate.value == "") {

                alert('Please select From Date')
                frmdate.focus();
                return false;
            }

            if (todate.value == "") {

                alert('Please select To Date')
                todate.focus();
                return false;
            }
            var frmdate1 = document.getElementById('txtfromDate').value;
            var todate1 = document.getElementById('txtToDate').value;
            var winname = window.open('VIEW_BOE_OtherAD_DataChecklist.aspx?frm=' + frmdate1 + '&to=' + todate1, '', 'scrollbars=yes,left=0,top=50,maximizeButton=yes,menubar=0,width=820,height=375');
            winname.focus();
             return false;
       </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
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
                                <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span class="pageLabel"><strong>Bill Of Entry-Other AD Data Checklist</strong></span>
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
                                        <td width="10%" align="center" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 150px">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 800px">
                                            <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                                Width="70" TabIndex="2"></asp:TextBox>
                                            <asp:ImageButton ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
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
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                            &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="3"></asp:TextBox>
                                            <asp:ImageButton ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
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
                                    </table>
                                    <%--<br />--%>
                    <table>
                        <tr valign="bottom">
                            <td align="right" style="width: 120px">
                            </td>
                            <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                &nbsp;
                                <asp:Button ID="Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Genarate" TabIndex="4" />
                                <%--<asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />--%>
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
