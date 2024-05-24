<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXPORT_CoveringScheduleLetterExportLC.aspx.cs"
    Inherits="Reports_EXPORTReport_EXPORT_CoveringScheduleLetterExportLC" %>

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
    <script language="javascript" type="text/javascript" src="../../Scripts/Validations.js"></script>
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
            }
        } 
    </script>
    <script language="javascript" type="text/javascript">


        function dochelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var rptType;
            rptType = 'Single';

            var Branch = document.getElementById('ddlBranch').value;
            var from = document.getElementById('txtFromDate').value;
            var Doc = '';

            popup = window.open('EXPORT_DocNoLookup.aspx?Doc_no=0' + '&frdate=' + from + '&Doc=' + Doc + '&rptType=' + rptType + '&branch=' + Branch, 'helpDocId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


            common = "helpDocId";
            return false;

        }

        function DocId(event) {

            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnDocList').click();
            }
        }
        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpDocId") {
                document.getElementById('txtDocumentNo').value = s;
            }
            if (common == "helpDocId1") {
                document.getElementById('txtToDocumentNo').value = s;
            }
        }

        function Todochelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var ToDoc = document.getElementById('txtDocumentNo').value;
            if (ToDoc == '') {
                alert('Select From Document No.');
                txtDocumentNo.focus();
                return false;
            }
            var rptType;
            rptType = 'All';
            var from = document.getElementById('txtFromDate').value;
            var Doc = document.getElementById('txtDocumentNo').value;
            var Branch = document.getElementById('ddlBranch').value;
            popup = window.open('EXPORT_DocNoLookup.aspx?Doc_no=0' + '&frdate=' + from + '&Doc=' + Doc + '&rptType=' + rptType + '&branch=' + Branch, 'helpDocId1', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


            common = "helpDocId1";
            return false;

        }
        function ToDocId(event) {

            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnToDocList').click();
            }
        }


        function validateSave() {

            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == 0) {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }
            if (document.getElementById('rdbSelectedDocNo').checked == true) {
                var Frdocno = document.getElementById('txtDocumentNo');
                if (Frdocno.value == '') {
                    alert('Enter From Document No..');
                    txtDocumentNo.focus();
                    return false;
                }

                var Todocno = document.getElementById('txtToDocumentNo');
                if (Todocno.value == '') {
                    alert('Enter To Document No..');
                    txtToDocumentNo.focus();
                    return false;
                }
            }

            var from = document.getElementById('txtFromDate').value;

            var rptType;
            var rptFrdocno;
            var rptTodocno;
            var rptCode;
            var Docid;
            var txt;


            if (document.getElementById('rdbAllDocNo').checked == true) {
                rptCode = 1;
                rptType = 'All';

            }
            else if (document.getElementById('rdbSelectedDocNo').checked == true) {
                var Frtxt = document.getElementById('txtDocumentNo');
                if (trimAll(Frtxt.value) == '') {
                    try {
                        alert('Enter From Document No.');
                        Frtxt.focus();
                        return false;
                    }
                    catch (err) {
                        alert('Enter From Document No.');
                        return false;
                    }
                }

                var Totxt = document.getElementById('txtToDocumentNo');
                if (trimAll(Totxt.value) == '') {
                    try {
                        alert('Enter To Document No.');
                        Totxt.focus();
                        return false;
                    }
                    catch (err) {
                        alert('Enter To Document No.');
                        return false;
                    }
                }
                rptCode = 2;
                rptType = "Single";
                rptFrdocno = document.getElementById('txtDocumentNo').value;
                rptTodocno = document.getElementById('txtToDocumentNo').value;
            }

            var Print1;
            var print2;
            var Copy;
            if (document.getElementById('CheckCreditRev').checked == true) {
                Print1 = "1";
            }
            else
                Print1 = "0";
            if (document.getElementById('Check1').checked == true) {
                Print2 = "1";
            }
            else
                Print2 = "0";
            if (document.getElementById('CheckOfficeCopy').checked == true) {
                Copy = "1";
            }
            else
                Copy = "0";
            

            var winname = window.open('EXPORT_ViewCoveringScheduleLetterExportLC.aspx?frm=' + from + '&rptType=' + rptType + '&rptCode=' + rptCode +'&rptFrdocno=' + rptFrdocno + '&rptTodocno=' + rptTodocno+ '&branch=' + Branch + '&Print1=' + Print1 + '&Print2=' + Print2 + '&Copy=' + Copy, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1050,height=550');
            winname.focus();
            return false;

        }
       
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Covering Schedule Letter - Export LC</span>
                            </td>
                            <%--<td align="right" style="width: 50%">
                                &nbsp;
                                <input type="hidden" id="hdnDocId" runat="server" />
                                <asp:Button ID="btnDocId" Style="display: none;" runat="server" OnClick="btnDocId_Click" />
                            </td>--%>
                            <%--<td align="right" style="width: 50%">
                                &nbsp;
                                <input type="hidden" id="hdnDocId1" runat="server" />
                                <asp:Button ID="btnDocId1" Style="display: none;" runat="server" OnClick="btnDocId1_Click" />
                            </td>--%>
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
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="true" Width="100px" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="CheckOfficeCopy" runat="server" ForeColor="#003399" Text=" Office Copy  "
                                                TabIndex="-1" />
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="700px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">For Date :</span>
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
                                            </ajaxToolkit:MaskedEditValidator>
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            <%--&nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="70" TabIndex="3"></asp:TextBox>
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
                                        </td>--%>
                                           
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="CheckCreditRev" runat="server" ForeColor="#003399" Text=" Credit Rev 2007 Pub.No.500  "
                                                TabIndex="-1" />
                                    </tr>
                                </table>
                                <table cellspacing="0" border="0" width="500px">
                                    <tr>
                                        <td height="40px" align="left" valign="middle">
                                            <asp:RadioButton ID="rdbAllDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data" Text="All Document No" TabIndex="3" OnCheckedChanged="rdbAllDocNo_CheckedChanged"
                                                Style="forecolor: #000000 font-weight: bold;" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td height="40px" align="left">
                                            <asp:RadioButton ID="rdbSelectedDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Document No" GroupName="Data" TabIndex="4" OnCheckedChanged="rdbSelectedDocNo_CheckedChanged"
                                                Style="forecolor: #000000 font-weight: bold;" />
                                        </td>
                                    </tr>
                                </table>
                                <fieldset id="Doclist" runat="server" style="width: 700px" visible="false">
                                    <legend>Select Document No</legend>
                                    <table>
                                       <tr>
                                        <td align="right">
                                            <span class="elementLabel" id="Doccode" runat="server">From Document No </span>
                                        </td>
                                        <td height="30px" width="10%" nowrap>
                                            &nbsp;
                                            <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                TabIndex="5" Width="159px" Visible="false" 
                                                OnTextChanged="txtDocumentNo_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnDocList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                            <%--<asp:Label ID="lblDocName" runat="server" CssClass="elementLabel" Width="400px" Visible="TRUE"></asp:Label>--%>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel" id="ToDoccode" runat="server">To Document No </span>
                                        </td>
                                        <td height="30px">
                                            &nbsp;
                                            <asp:TextBox ID="txtToDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                TabIndex="6" Width="150px" Visible="false" OnTextChanged="txtToDocumentNo_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnToDocList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                            <%--<asp:Label ID="lblToDocName" runat="server" CssClass="elementLabel" Width="400px" Visible="TRUE"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    </table>
                                </fieldset>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="Check1" runat="server" ForeColor="#003399" Text=" To avoid delay in Beneficiary getting the LC / amendment, we advise the LC / amendment to the Beneficiary <br/> directly even when second advising Bank is mentioned in the LC  "
                                                TabIndex="-1" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate" TabIndex="7" />
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
</html>
