<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExportCovSchLetter.aspx.cs"
    Inherits="Reports_EXPORTReports_rptExportCovSchLetter" %>

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
  
    <script language="javascript" type="text/javascript">

        function dochelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }

            if (document.getElementById('rdbSelectedCustomer').checked == true) {

                var Cust1 = document.getElementById('txtCustomerID');

                if (Cust1.value == '') {
                    alert("Enter Customer Account Number");
                    Cust1.focus();
                    return false;
                }

                var Cust = document.getElementById('txtCustomerID').value;
            }
            if (document.getElementById('rdbAllCustomer').checked == true) {

                var Cust = 'All';

            }

            var DocNoType = 'Single';

            var from = document.getElementById('txtFromDate').value;
            var FDoc = '';

            var Branch = document.getElementById('ddlBranch').value;
            var pc = 1;

            open_popup('HelpCovLettDocNo.aspx?DocNoType=' + DocNoType + '&FromDate=' + from + '&Branch=' + Branch + '&FDoc=' + FDoc + '&pc=' + pc + '&Cust=' + Cust, 400, 400, "DocFile");

            return false;

        }

        function dochelp1() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }

            var FDoc1 = document.getElementById('txtDocumentNo');
            if (FDoc1.value == '') {
                alert('Select From Document Number.');
                FDoc1.focus();
                return false;
            }

            if (document.getElementById('rdbSelectedCustomer').checked == true) {

                var Cust1 = document.getElementById('txtCustomerID');

                if (Cust1.value == '') {
                    alert("Enter Customer Account Number");
                    Cust1.focus();
                    return false;
                }

                var Cust = document.getElementById('txtCustomerID').value;
            }

            if (document.getElementById('rdbAllCustomer').checked == true) {

                var Cust = 'All';

            }

            var DocNoType = 'All';

            var from = document.getElementById('txtFromDate').value;

            var FDoc = document.getElementById('txtDocumentNo').value;

            var Branch = document.getElementById('ddlBranch').value;

            var pc = 2;

            open_popup('HelpCovLettDocNo.aspx?DocNoType=' + DocNoType + '&FromDate=' + from + '&Branch=' + Branch + '&FDoc=' + FDoc + '&pc=' + pc + '&Cust=' + Cust, 400, 400, "DocFile1");

            return false;

        }

        function Custhelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }

            var from = document.getElementById('txtFromDate').value;

            var Branch = document.getElementById('ddlBranch').value;

            open_popup('HelpCovLettCust.aspx?&FromDate=' + from + '&Branch=' + Branch , 400, 400, "CustFile");

            return false;

        }

        function selectCust(Uname) {
            document.getElementById('txtCustomerID').value = Uname;
            document.getElementById('txtCustomerID').focus(); 
        }

        function selectUser(Uname) {

            document.getElementById('txtDocumentNo').value = Uname;
            document.getElementById('txtToDocumentNo').focus();

        }

        function selectUser1(Uname) {
            var DocNo = document.getElementById('txtDocumentNo');
            if (DocNo.value == '') {
                alert('Enter From Document Number');
                return false;
            }

            document.getElementById('txtToDocumentNo').value = Uname;
            document.getElementById('btnSave').focus();

        }



        function validateGenerate() {

            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }

            if (document.getElementById('rdbAllCustomer').checked == true) {

                var Cust = 'All';

            }
            if (document.getElementById('rdbSelectedCustomer').checked == true) {

                var Cust1 = document.getElementById('txtCustomerID');

                if (Cust1.value == "") {
                    alert("Enter Customer Account Number");
                    Cust1.focus();
                    return false;
                }

                var Cust = document.getElementById('txtCustomerID').value;
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



            var DocNoType;
            var FDoc;
            var TDoc;
            var Branch;
            var NewLine;

                NewLine = 'Subject to Uniform Customs And Practices For Documentary Credit 2007.  Revision ICC Publication No. 600';

            if (document.getElementById('rdbAllDocNo').checked == true) {
                DocNoType = 'All';
                FDoc = '';
                TDoc = '';
               

            }
            if (document.getElementById('rdbSelectedDocNo').checked == true) {

                DocNoType = "Single";
                FDoc = document.getElementById('txtDocumentNo').value;
                
                TDoc = document.getElementById('txtToDocumentNo').value;
                
            }

            Branch = document.getElementById('ddlBranch').value;
            
            var from = document.getElementById('txtFromDate').value;

            var winname = window.open('ViewCovSchLetter.aspx?from=' + from + '&Doc_No_Type=' + DocNoType + '&FDoc=' + FDoc + '&TDoc=' + TDoc + '&NewLine=' + NewLine + '&Branch=' + Branch + '&Cust=' + Cust, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');
            
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
                                <span class="pageLabel">Covering Schedule Letter</span>
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

                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr width="550px">
                                        <td height="40px" width="20%" align="right" nowrap>
                                            <asp:RadioButton ID="rdbAllCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" Text="All Customers" Checked="True" TabIndex="3" oncheckedchanged="rdbAllCustomer_CheckedChanged" 
                                                 />
                                        &nbsp;&nbsp;&nbsp;
                                        <td align="Left" nowrap>
                                            <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Customer" GroupName="Data2" TabIndex="3" oncheckedchanged="rdbSelectedCustomer_CheckedChanged" 
                                                  />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="Left">
                                            <fieldset id="Custlist" runat="server" visible="false">
                                                <legend><span class="pageLabel">Select Customer AC No.</span></legend>
                                                <table id="Table2" runat="server">
                                                    <tr>
                                                        <td align="right" nowrap>
                                                            <span class="pageLabel">Customer AC No. :&nbsp;</span>
                                                        </td>
                                                        <td align="left" nowrap>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="4" Width="80px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Button ID="btnCustList" runat="server" CssClass="btnHelp_enabled" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td height="40px" width="20%" align="right" nowrap>
                                            <asp:RadioButton ID="rdbAllDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Checked = "true" GroupName="Data" Text="All Document No" TabIndex="2" OnCheckedChanged="rdbAllDocNo_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td height="40px" nowrap>
                                            <asp:RadioButton ID="rdbSelectedDocNo" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Document No" GroupName="Data" TabIndex="3" OnCheckedChanged="rdbSelectedDocNo_CheckedChanged" />
                                        </td>
                                    </tr>
                                    
                                    <table id="tblDocNo" runat="server" border="0" visible="false">
                                        <tr>
                                            <td width="150px">
                                            </td>
                                            <td width="120px" align="right">
                                                <span class="elementLabel" id="Doccode" runat="server">From Document No </span>
                                            </td>
                                            <td height="30px" nowrap>
                                                &nbsp;
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                    TabIndex="4" Width="159px"></asp:TextBox>
                                                <asp:Button ID="btnDocList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                <%--<asp:Label ID="lblDocName" runat="server" CssClass="elementLabel" Width="400px" Visible="TRUE"></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="left" width="120px" nowrap>
                                                <span class="elementLabel" id="ToDoccode" runat="server">&nbsp; To Document No </span>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtToDocumentNo" runat="server" CssClass="textBox" MaxLength="40"
                                                    TabIndex="5" Width="159px"></asp:TextBox>
                                                <asp:Button ID="btnToDocList" runat="server" CssClass="btnHelp_enabled" />
                                                <%--<asp:Label ID="lblToDocName" runat="server" CssClass="elementLabel" Width="400px" Visible="TRUE"></asp:Label>--%>
                                            </td>
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
