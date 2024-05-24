<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExportReportArbitrageBalStat.aspx.cs" Inherits="Reports_EXPReports_rptExportReportArbitrageBalStat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../../Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
   
    <script src="../../Scripts/Enable_Disable_Opener.js" language="javascript" type="text/javascript"></script>
    <%-- <script type="text/javascript">
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
    </script> --%>
    <script language="javascript" type="text/javascript">


        function validateGenerate() {

            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select For Date.');
                fromDate.focus();
                return false;
            }

            var Branch1 = document.getElementById('ddlBranch');

            if (Branch1.value == '--Select--') {
                alert('Select Branch');
                Branch1.focus();
                return false;


            }



            var Branch;

            Branch = document.getElementById('ddlBranch').value;

            var from = document.getElementById('txtFromDate').value;


            var winname = window.open('ViewExportReportArbitrageBalSta.aspx?from=' + from + '&Branch=' + Branch, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');

            winname.focus();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <%--     <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
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
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Arbitrage Balancing Statement</span>
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
                                        <%-- <td width="10%" align="right" nowrap>
                                                <span class="mandatoryField">* </span><span class="elementLabel">NEFT User Id :</span>
                                            </td>--%>
                                    </tr>
                                    <tr>
                                        <td nowrap width="10px">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="100px"
                                                runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">For Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
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
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                    </tr>
                                </table>
                                    <%--<table>
                                        <tr>
                                            <td width="150px">
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Check1" runat="server" ForeColor="#003399" Text="Credit Rev 2007 Pub. No. 500"
                                                    TabIndex="6" Font-Names="Times New Roman" Font-Size="10pt" />
                                            </td>
                                        </tr>
                                    </table>--%>
                                    <table>
                                        <tr valign="bottom">
                                            <%--<td align="left" style="width: 240px" padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                 &nbsp;
                                                <asp:Button ID="Button2" runat="server" CssClass="buttonDefault" Text="Generate Swift File"
                                                    ToolTip="Generate" TabIndex="6" Width="135px" onclick="btnCreateFile_Click" />
                                            </td>--%>
                                            <td align="left" style="width: 175px; padding-top: 10px; padding-bottom: 10px">
                                            </td>
                                            <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                &nbsp;
                                                <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate Report"
                                                    Width="135px" ToolTip="Generate" TabIndex="7" />
                                            </td>
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </tr>
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

