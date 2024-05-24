﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_CreateSWIFT_MT730_DETAIL.aspx.cs" Inherits="Reports_EXPORTReports_EXP_CreateSWIFT_MT730_DETAIL" %>

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

    function dochelp() {
        var fromDate = document.getElementById('txtFromDate');
        if (fromDate.value == '') {
            alert('Select From Date.');
            fromDate.focus();
            return false;
        }
        var rptType;
        rptType = 'Single';

        var from = document.getElementById('txtFromDate').value;
        var Doc = '';
        //  open_popup('INW_DOCNoLookup1.aspx?Doc_no=0' + '&frdate=' + frdate, 450, 350, 'win1');
        //popup = window.open('TF_CustomerLookUp1.aspx?Doc_no=0' + '&frdate=' + from + '&branch=' + Branch, 'helpDocId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
        popup = window.open('EXPORT_DocNoLookup2.aspx?Doc_no=0' + '&frdate=' + from + '&Doc=' + Doc + '&rptType=' + rptType, 'helpDocId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


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
        //  open_popup('INW_DOCNoLookup1.aspx?Doc_no=0' + '&frdate=' + frdate, 450, 350, 'win1');
        //popup = window.open('TF_CustomerLookUp1.aspx?Doc_no=0' + '&frdate=' + from + '&branch=' + Branch, 'helpDocId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
        popup = window.open('EXPORT_DocNoLookup2.aspx?Doc_no=0' + '&frdate=' + from + '&Doc=' + Doc + '&rptType=' + rptType, 'helpDocId1', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


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

        var fromDate = document.getElementById('txtFromDate');
        if (fromDate.value == '') {
            alert('Select From Date.');
            fromDate.focus();
            return false;
        }

    }
    function validateGenerate() {
        var fromDate = document.getElementById('txtFromDate');
        if (fromDate.value == '') {
            alert('Select From Date.');
            fromDate.focus();
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

        var winname = window.open('ViewEXPrptEXPSWIFT_MT730.aspx?frm=' + from + '&rptType=' + rptType + '&rptFrdocno=' + rptFrdocno + '&rptTodocno=' + rptTodocno + '&rptCode=' + rptCode, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');
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
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnCreateFile" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <span class="pageLabel">LC Acknowledge Message (MT 730) </span>
                                </td>
                                <td align="right" style="width: 50%">
                                    &nbsp;
                                    <input type="hidden" id="hdnDocId" runat="server" />
                                    <asp:Button ID="btnDocId" Style="display: none;" runat="server" OnClick="btnDocId_Click" />
                                </td>
                                <td align="right" style="width: 50%">
                                    &nbsp;
                                    <input type="hidden" id="hdnToDocId" runat="server" />
                                    <asp:Button ID="btnToDocId" Style="display: none;" runat="server" OnClick="btnToDocId_Click" />
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
                                <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                    <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                        <tr>
                                           <%-- <td width="10%" align="right" nowrap>
                                                <span class="mandatoryField">* </span><span class="elementLabel">NEFT User Id :</span>
                                            </td>--%>
                                         
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 100px">
                                                <span class="mandatoryField">*</span><span class="elementLabel">For Date :</span>
                                            </td>
                                            <td align="left" style="width: 700px">
                                                &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                    ValidationGroup="dtVal" Width="70" TabIndex="1" ></asp:TextBox>
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
                                    <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                        <tr>
                                            <td height="40px" width="20%" align="right" valign="bottom" nowrap>
                                                <asp:RadioButton ID="rdbAllDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data" OnCheckedChanged="rdbAllDocNo_CheckedChanged" Text="All Document No"
                                                    TabIndex="2" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td height="40px" valign="bottom" nowrap>
                                                <asp:RadioButton ID="rdbSelectedDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Selected Document No" GroupName="Data" OnCheckedChanged="rdbSelectedDocNo_CheckedChanged"
                                                    TabIndex="3" />
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
                                        <tr>
                                        <td align="right">
                                            <span class="elementLabel" id="Doccode" runat="server">From Document No </span>
                                        </td>
                                        <td height="30px" width="10%" nowrap>
                                            &nbsp;
                                            <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                TabIndex="4" Width="159px" Visible="false" 
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
                                                TabIndex="5" Width="150px" Visible="false" OnTextChanged="txtToDocumentNo_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnToDocList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                            <%--<asp:Label ID="lblToDocName" runat="server" CssClass="elementLabel" Width="400px" Visible="TRUE"></asp:Label>--%>
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
                                            <td align="right" style="width: 210px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                &nbsp;
                                                <asp:Button ID="btnCreateFile" runat="server" CssClass="buttonDefault" Text="Generate Swift File"
                                                    ToolTip="Generate" TabIndex="6" Width="135px" onclick="btnCreateFile_Click" />
                                               
                                            </td>

                                              <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                &nbsp;
                                                 <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate Report" Width = "135px"
                                                    ToolTip="Generate" TabIndex="7" />
                                               
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
