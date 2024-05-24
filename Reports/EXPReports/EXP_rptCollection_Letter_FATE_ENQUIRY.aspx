﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_rptCollection_Letter_FATE_ENQUIRY.aspx.cs" Inherits="Reports_EXPReports_EXP_rptCollection_Letter_FATE_ENQUIRY" %>

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
            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == "0") {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }

            var rptType;
            rptType = 'Single';

            //var from = document.getElementById('txtFromDate').value;
            var Doc = '';
            popup = window.open('HelpDocNoLookUp_EXP_Letter_FateEnquiry.aspx?Doc_no=0' + '&Doc=' + Doc + '&rptType=' + rptType + '&Branch=' + Branch, 'helpDocId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


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
            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == "0") {
                alert('Select Branch Name.');
                Branch.focus();
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
            //            var from = document.getElementById('txtFromDate').value;
            var Doc = document.getElementById('txtDocumentNo').value;
            popup = window.open('HelpDocNoLookUp_EXP_Letter_FateEnquiry.aspx?Doc_no=0' + '&Doc=' + Doc + '&rptType=' + rptType + '&Branch=' + Branch, 'helpDocId1', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


            common = "helpDocId1";
            return false;

        }
        function ToDocId(event) {

            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnToDocList').click();
            }
        }
        
        function validateGenerate() {
            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == "0") {
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

            var Branch = document.getElementById('ddlBranch').value;
           
            var rptType;
            var rptFrdocno = "";
            var rptTodocno = "";
            var rptCode;
            var Remark1;
            var Remark2;
            var Remark3;
            var Remark4;
            var Remark5;

            if (document.getElementById('Check1').checked == true) {
                Remark1 = document.getElementById('lblValue1').innerText;
            }
            else {
                Remark1 = "";
            }

            if (document.getElementById('Check2').checked == true) {
                Remark2 = document.getElementById('lblValue2').innerText;
            }
            else {
                Remark2 = "";
            }

            if (document.getElementById('Check3').checked == true) {
                Remark3 = document.getElementById('lblValue3').innerText;
            }
            else {
                Remark3 = "";
            }

            if (document.getElementById('Check4').checked == true) {
                Remark4 = document.getElementById('lblValue4').innerText;
            }
            else {
                Remark4 = "";
            }

            Remark5 = document.getElementById('txtRemark1').value;

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

            var winname = window.open('ViewEXPrptCollection_Letter_FATE_ENQUIRY.aspx?rptFrdocno=' + rptFrdocno + '&rptTodocno=' + rptTodocno + '&rptCode=' + rptCode + '&Remark1=' + Remark1 + '&Remark2=' + Remark2 + '&Remark3=' + Remark3 + '&Remark4=' + Remark4 + '&Remark5=' + Remark5 + '&Branch=' + Branch, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');
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
                <uc1:Menu ID="Menu" runat="server" />
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                  
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <span class="pageLabel">COLLECTION FOLLOW UP LETTER TO CUSTOMER</span>
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
                                                 <td width="10%" align="right" nowrap>
                                           <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                         </td>
                                                 <td align="left" nowrap>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                            Width="120px" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                             </asp:DropDownList>                                                
                                         </td>
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
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Check1" runat="server" ForeColor="#003399" 
                                                TabIndex="6" Font-Names="Times New Roman" Font-Size="10pt" Visible="true" Checked="false"/>
                                                <asp:Label ID="lblValue1" runat="server" CssClass="elementLabel" Width="500px" Visible="true" Text = "We request you to follow up with the overseas buyer as the bill is overdue."></asp:Label>
                                             </td> 
                                        </tr> 
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Check2" runat="server" ForeColor="#003399" 
                                                TabIndex="7" Font-Names="Times New Roman" Font-Size="10pt" Visible="true" Checked="false" />
                                                <asp:Label ID="lblValue2" runat="server" CssClass="elementLabel" Width="500px" Visible="true" Text = "We request you to submit your request for extension (FORM ETX)  for forwarding to Reserve Bank Of India."></asp:Label>
                                            </td> 
                                        </tr> 
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Check3" runat="server" ForeColor="#003399" 
                                                TabIndex="8" Font-Names="Times New Roman" Font-Size="10pt" Visible="true" Checked="false"/>
                                                <asp:Label ID="lblValue3" runat="server" CssClass="elementLabel" Width="500px" Visible="true" Text = "Please note that for overdue export collection bills Rs.250/- per quarter is to be charged as per FEDAI guidelines."></asp:Label>
                                             </td>
                                        </tr>    
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Check4" runat="server" ForeColor="#003399" 
                                                TabIndex="9" Font-Names="Times New Roman" Font-Size="10pt" Visible="true" Checked="false"/>
                                                <asp:Label ID="lblValue4" runat="server" CssClass="elementLabel" Width="500px" Visible="true" Text = "We request you to submit your request for write off of the collection for submission to Reserve Bank Of India"></asp:Label>
                                             </td>
                                        </tr>                                    
                                    </table>
                                        <table>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Remarks :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtRemark1" runat="server" CssClass="textBox" MaxLength="100"
                                                Width="650px" TabIndex="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                                        <table>
                                    <tr valign="bottom"> 
                                        <td align="right" style="width: 120px">
                                            </td>                                       
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate Report"
                                                Width="135px" ToolTip="Generate" TabIndex="11" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                </table>
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