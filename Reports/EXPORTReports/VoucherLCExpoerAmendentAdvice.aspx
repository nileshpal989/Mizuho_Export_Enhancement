<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoucherLCExpoerAmendentAdvice.aspx.cs" Inherits="Reports_EXPORTReports_VoucherLCExpoerAmendentAdvice" %>

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
    <script type="text/javascript">
    
    </script>
    <script language="javascript" type="text/javascript">

        function changeDate() {

            __doPostBack('btnChangeDate', '');
        }
        function dochelp() {

            var Branch = document.getElementById('ddlBranch').value;
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
            popup = window.open('DocumentLookUpExportLCAmendment.aspx?Doc_no=0' + '&frdate=' + from + '&Doc=' + Doc + '&rptType=' + rptType + '&branch=' + Branch, 'helpDocId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


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
            var Branch = document.getElementById('ddlBranch').value;
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
            popup = window.open('DocumentLookUpExportLCAmendment.aspx?Doc_no=0' + '&frdate=' + from + '&Doc=' + Doc + '&rptType=' + rptType + '&branch=' + Branch, 'helpDocId1', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
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
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            var rptType;
            var rptTypeTo;
            var rptCode;



            if (document.getElementById('rdbAllDocNo').checked == true) {
                rptCode = 1;
                rptType = 'All';
                rptTypeTo = 'All'


            }
            else if (document.getElementById('rdbSelectedDocNo').checked == true) {
                var Frdocno = document.getElementById('txtDocumentNo');
                if (Frdocno.value == '') {
                    alert('Enter From Document No..');
                    txtDocumentNo.focus();
                    return false;
                }

                var Todocno = document.getElementById('txtToDocumentNo');
                if (Todocno.value == '') {
                    alert('Enter From Document No..');
                    txtToDocumentNo.focus();
                    return false;
                }
                rptCode = 2;
                rptType = document.getElementById('txtDocumentNo').value;
                rptTypeTo = document.getElementById('txtToDocumentNo').value;
            }

            var winname = window.open('ViewVoucherLCExpoerAmendentAdvice.aspx?frm=' + from + '&rptType=' + rptType + '&rptTypeTo=' + rptTypeTo + '&rptCode=' + rptCode + '&branch=' + Branch, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=950,height=550');
            winname.focus();
            return false;
        }
        
    </script>
    <style type="text/css">
        .style3
        {
            width: 196px;
        }
        .style6
        {
            width: 192px;
        }
        .style7
        {
            width: 138px;
        }
        .style8
        {
            width: 107px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                                <span class="pageLabel">Export L.C. Amendment Advice</span></td>
                            <td align="right" style="width: 50%">
                                &nbsp;
                                <input type="hidden" id="hdnDocId" runat="server" />
                                <asp:Button ID="btnDocId" Style="display: none;" runat="server" OnClick="btnDocId_Click" />
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
                                            <%--<asp:Button ID="btnPurCodeList" runat="server" CssClass="btnHelp_enabled" 
                                                                TabIndex="-1" />--%>
                                            &nbsp;&nbsp;&nbsp;&nbsp; <%--<span class="pageLabel">&nbsp;</span><asp:CheckBox ID="CheckOfficeCopy"
                                                runat="server" ForeColor="#003399" Text=" Office Copy  " TabIndex="2" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">For Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2" 
                                                ontextchanged="txtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
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
                                        </td>
                                    </tr>
                                </table>
                               <table cellspacing="0" border="0" width="550px">
                                   <%-- <tr>
                                  
                                        <td  height="40px" align="right" valign="bottom">
                                        <asp:RadioButton ID="rdbCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                        GroupName="cust" 
                                                Text="Customer &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" 
                                                TabIndex="3" />
                                            &nbsp;
                                        </td>
                                         <td  height="40px" valign="bottom">
                                        <asp:RadioButton ID="rdbCustomerNon" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                        GroupName="cust" Text="Non Customer" TabIndex="4" />
                                        </td>
                                    </tr>--%>
                                    <tr width="550px">
                                        <td class="style8"> 
                                        <td height="40px" align="left" valign="bottom" class="style6">
                                            <asp:RadioButton ID="rdbAllDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data" OnCheckedChanged="rdbAllDocNo_CheckedChanged" Text="All Document No"
                                                TabIndex="3" ValidationGroup="data" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                       
                                        <td height="40px" align="Left" valign="bottom" colspan="2">
                                            <asp:RadioButton ID="rdbSelectedDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Document No" GroupName="Data" OnCheckedChanged="rdbSelectedDocNo_CheckedChanged"
                                                TabIndex="4" ValidationGroup="data" />
                                        </td>
                                        
                                    </tr>
                                      <tr width="550px">  
                                              <td height="40px" align="right" valign="bottom" class="style8"/>
                                               <td height="40px" align="right" valign="bottom" class="style6"/>
                                            <td height="40px" align="left" valign="bottom" class="style7" >
                                                <span class="elementLabel" id="Doccode" runat="server">From Document No </span>
                                            </td>
                                            <td  height="40px" align="Left" valign="bottom" class="style3">
                                                &nbsp;
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                    TabIndex="5" Width="150px" Visible="TRUE" 
                                                    OnTextChanged="txtDocumentNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <asp:Button ID="btnDocList" runat="server" CssClass="btnHelp_enabled" Visible="TRUE" />
                                            </td>
                                        </tr>
                                        <tr width="550px">
                                             <td height="40px" align="left" valign="bottom" class="style8"/>
                                              <td height="40px" align="right" valign="bottom" class="style6"/>
                                            <td  height="40px" align="left" valign="bottom" class="style7" >
                                                <span class="elementLabel" id="ToDoccode" runat="server">To Document No </span>
                                            </td>
                                            <td  height="40px" align="left" valign="bottom" class="style3">
                                                &nbsp;
                                                <asp:TextBox ID="txtToDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                    TabIndex="6" Width="150px" Visible="TRUE" 
                                                    OnTextChanged="txtToDocumentNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <asp:Button ID="btnToDocList" runat="server" CssClass="btnHelp_enabled" Visible="TRUE" />
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
