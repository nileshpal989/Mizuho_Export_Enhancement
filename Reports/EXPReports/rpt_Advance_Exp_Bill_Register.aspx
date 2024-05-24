<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Advance_Exp_Bill_Register.aspx.cs" Inherits="Reports_EXPReports_rpt_Advance_Exp_Bill_Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../../Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">   
    <script language="javascript" type="text/javascript">
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
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
        function dochelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var fromDate = document.getElementById('txtFromDate').value;
            var Branch = document.getElementById('ddlBranch').value;
            var txtToDate = document.getElementById('txtToDate').value;

            open_popup('Help_Advance_ExportBill_Register.aspx?fromDate=' + fromDate + '&Branch=' + Branch + '&toDate=' + txtToDate, 400, 600, "DocFile");
            return false;
        }

        function selectUser(docno, Uname) {
            document.getElementById('txtDocumentNo').value = docno;
            document.getElementById('lblCustName').innerHTML = Uname;
        }
        function validateGenerate() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }

            var txtToDate = document.getElementById('txtToDate');
            if (txtToDate.value == '') {
                alert('Select To Date.');
                txtToDate.focus();
                return false;
            }

            if (document.getElementById('rdbSelectedDocNo').checked == true) {
                var Frdocno = document.getElementById('txtDocumentNo');
                if (Frdocno.value == '') {
                    alert('Enter From Document No..');
                    txtDocumentNo.focus();
                    return false;
                }
            }
            var DocumentNo;
            var Branch;
            if (document.getElementById('rdbAllDocNo').checked == true) {
                DocumentNo = 'All';
            }
            if (document.getElementById('rdbSelectedDocNo').checked == true) {
                DocumentNo = document.getElementById('txtDocumentNo').value;
            }
            var fromDate = document.getElementById('txtFromDate');
            var txtToDate = document.getElementById('txtToDate');
            Branch = document.getElementById('ddlBranch').value;
            var winname = window.open('View_rpt_Advance_Exp_Bill_Register.aspx?fromDate=' + fromDate.value + '&toDate=' + txtToDate.value + '&documentNo=' + DocumentNo + '&Branch=' + Branch, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');

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
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Export Bill Covering Letter</strong></span>
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
                                        <%-- <td width="10%" align="right" nowrap>
                                                <span class="mandatoryField">* </span><span class="elementLabel">NEFT User Id :</span>
                                            </td>--%>
                                    </tr>
                                    <tr>
                                        <td nowrap width="5%" align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td colspan="3">
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="100px"
                                                runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date
                                                :</span>
                                        </td>
                                        <td align="left" width="10%" nowrap>
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
                                        </td>
                                        <td align="right" nowrap width="5%">
                                            <span class="elementLabel">&nbsp;To Date :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal1"
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
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td height="40px" width="35%" align="right" nowrap>
                                            <asp:RadioButton ID="rdbAllDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Checked="true" GroupName="Data" Text="All Document No" TabIndex="3" OnCheckedChanged="rdbAllDocNo_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td height="40px" nowrap>
                                            <asp:RadioButton ID="rdbSelectedDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Document No" GroupName="Data" TabIndex="4" OnCheckedChanged="rdbSelectedDocNo_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <%--<tr>
                                            <td align="right" style="width: 200px" >
                                                <span class="elementLabel" id="Doccode" runat="server">Document No </span>
                                            </td>
                                            <td height="40px">
                                                &nbsp;
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                    TabIndex="4" Width="150px" Visible="false" 
                                                    OnTextChanged="txtDocumentNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <asp:Button ID="btnDocList" runat="server" CssClass="btnHelp_enabled" 
                                                    Visible="false" TabIndex="-1" />
                                                <asp:Label ID="lblDocName" runat="server" CssClass="elementLabel" Width="400px" Visible="false"></asp:Label>
                                            </td>
                                        </tr>--%>
                                    <table id="tblDocNo" runat="server" border="0" visible="false">
                                        <tr>
                                            <td width="150px">
                                            </td>
                                            <td width="120px" align="right">
                                                <span class="elementLabel" id="Doccode" runat="server">&nbsp;Document No :</span>
                                            </td>
                                            <td height="30px" nowrap>
                                                &nbsp;
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                    TabIndex="4" Width="159px"></asp:TextBox>
                                                <asp:Button ID="btnDocList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel" Width="400px"
                                                    Visible="TRUE"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr valign="bottom">
                                            <%--<td align="left" style="width: 240px" padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                 &nbsp;
                                                <asp:Button ID="Button2" runat="server" CssClass="buttonDefault" Text="Generate Swift File"
                                                    ToolTip="Generate" TabIndex="6" Width="135px" onclick="btnCreateFile_Click" />
                                            </td>--%>
                                            <td align="left" style="width: 175px; padding-top: 10px; padding-bottom: 10px">
                                            </td>
                                            <td align="left" style="width: 600px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
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
