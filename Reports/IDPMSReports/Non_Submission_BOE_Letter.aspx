<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Non_Submission_BOE_Letter.aspx.cs"
    Inherits="Reports_IDPMSReports_Non_Submission_BOE_Letter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function generateReport() {
            var brname = document.getElementById('ddlBranch');

            if (brname.value == "---Select---") {
                alert('Please select branchname');
                brname.focus();
                return false;
            }

            var fromdate = document.getElementById('txtfromDate');
            if (fromdate.value == "") {
                alert('Please Ente From Date');
                fromdate.focus();
                return false;
            }

            var todate = document.getElementById('txtToDate');
            if (todate.value == "") {
                alert('Please Ente From Date');
                todate.focus();
                return false;
            }

            if (document.getElementById('rdbSelectedCust').checked == true) {
                var cust = document.getElementById('txtCust');
                if (cust.value == '') {
                    alert('Select Customer Acc No.');
                    cust.focus();
                    return false;
                }
            }
            if (document.getElementById('rdbSelectedDoc').checked == true) {
                var doc = document.getElementById('txtDoc');
                if (doc.value == '') {
                    alert('Select Document No.');
                    doc.focus();
                    return false;
                }
            }

            if (document.getElementById('rdbOtt').checked == true) {
                var ormtype = 'ott';
            }
            else {
                var ormtype = 'other';
            }

            if (document.getElementById('rdbAllDoc').checked == true) {
                var Doc = 'all';
            }
            else {
                var Doc = document.getElementById('txtDoc').value;
            }

            if (document.getElementById('rdbAllCust').checked == true) {
                var cust = 'all';
            }
            else {
                var cust = document.getElementById('txtCust').value;
            }

            if (document.getElementById('rdbAdvanced').checked == true) {
                var purpose = 'advanced';
            }
            else {
                var purpose = 'direct';
            }
            var winame = window.open('View_Non_Submission_BOE_Letter.aspx?branchname=' + brname.value + '&type=' + cust + '&doc=' + Doc + '&fromdate=' + fromdate.value + '&todate=' + todate.value + '&ormtype=' + ormtype + '&Purpose=' + purpose, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            winame.focus();
            return false;

        }

        function CustHelp() {

            var brname = document.getElementById('ddlBranch');

            if (brname.value == "---Select---") {
                alert('Please select branchname');
                brname.focus();
                return false;

            }

            var e = document.getElementById("ddlBranch");
            var branch = e.options[e.selectedIndex].value;
            open_popup('../../TF_CustomerLookUp2.aspx?adcode=' + branch, 450, 550, 'CustList');
            return false;
        }

        function selectCustomer(acno) {

            document.getElementById('txtCust').value = acno;
            __doPostBack('txtCust', '');
        }

        function REFNOHelp() {

            var brname = document.getElementById('ddlBranch');

            if (brname.value == "---Select---") {
                alert('Please select branchname');
                brname.focus();
                return false;

            }

            if (document.getElementById('rdbSelectedCust').checked == true) {
                var cust = document.getElementById('txtCust');
                if (cust.value == "") {
                    alert('Please Select Customer');
                    cust.focus();
                    return false;
                }
            }

            var fromdate = document.getElementById('txtfromDate');
            if (fromdate.value == "") {
                alert('Please Enter From Date');
                fromdate.focus();
                return false;
            }

            var todate = document.getElementById('txtToDate');
            if (todate.value == "") {
                alert('Please Enter From Date');
                todate.focus();
                return false;
            }

            if (document.getElementById('rdbAllCust').checked == true) {
                var e = document.getElementById("ddlBranch");
                var branch = e.options[e.selectedIndex].value;
                open_popup('../../TF_Ref_No_Help.aspx?adcode=' + branch + '&cust=all' + '&fromdate=' + fromdate.value + '&todate=' + todate.value, 450, 550, 'Ref No. List');
                return false;
            }
            else {
                var e = document.getElementById("ddlBranch");
                var branch = e.options[e.selectedIndex].value;
                var cust = document.getElementById('txtCust').value;
                open_popup('../../TF_Ref_No_Help.aspx?adcode=' + branch + '&cust=' + cust + '&fromdate=' + fromdate.value + '&todate=' + todate.value, 450, 550, 'Ref No. List');
                return false;
            }

        }

        function selectDocument(docno) {

            document.getElementById('txtDoc').value = docno;
            //            __doPostBack('txtDoc', '');
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
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="1" border="0" width="100%">
                    <tr>
                        <td align="left">
                            <span class="pageLabel"><strong>Non Submission of Bill of Entry LETTER</strong>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                            <table cellspacing="2" cellpadding="2" border="0" width="100%" style="line-height: 150%">
                                <tr>
                                    <td width="10%" align="right" nowrap>
                                        <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                    </td>
                                    <td align="left" style="white-space: nowrap">
                                        &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                            Width="115px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px">
                                        <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">From Date :</span>
                                    </td>
                                    <td align="left" style="width: 800px">
                                        &nbsp;<asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10"
                                            ValidationGroup="dtval" Width="70" TabIndex="2"></asp:TextBox>
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
                                        &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>&nbsp;<asp:TextBox
                                            ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70" TabIndex="3"></asp:TextBox>
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
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:RadioButton ID="rdbOtt" runat="server" Checked="True" GroupName="rdbottgroup"
                                                CssClass="elementLabel" Text="OTT" AutoPostBack="True" />
                                            &nbsp;<asp:RadioButton ID="rdbOther" runat="server" GroupName="rdbottgroup" CssClass="elementLabel"
                                                Text="OTHERS (IBA/ICA/ICU/ACC)" AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAllCust" runat="server" Checked="True" GroupName="rdbgroup2"
                                                CssClass="elementLabel" Text="All Customers" AutoPostBack="true" OnCheckedChanged="rdbAllCust_CheckedChanged" />
                                            <asp:RadioButton ID="rdbSelectedCust" runat="server" GroupName="rdbgroup2" CssClass="elementLabel"
                                                Text="Selected Customer" AutoPostBack="true" OnCheckedChanged="rdbSelectedCust_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <table visible="false" id="SelectCust" runat="server" width="100%">
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Customer Name
                                                :</span>
                                        </td>
                                        <td width="7%" nowrap>
                                            <asp:TextBox ID="txtCust" runat="server" Width="100" TabIndex="2" MaxLength="20"
                                                CssClass="textBox" ToolTip="Press F2 for Help" AutoPostBack="true" OnTextChanged="txtCust_TextChanged"></asp:TextBox>
                                        </td>
                                        <td width="2%" nowrap>
                                            <asp:Image ID="btnCustHelp" runat="server" ImageUrl="~/Style/images/help1.png" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblcustname" CssClass="elementLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAllDoc" runat="server" GroupName="rdbgroup1" Checked="true"
                                                CssClass="elementLabel" Text="All Documents" AutoPostBack="true" OnCheckedChanged="rdbAllDoc_CheckedChanged" />
                                            <asp:RadioButton ID="rdbSelectedDoc" runat="server" GroupName="rdbgroup1" CssClass="elementLabel"
                                                Text="Selected Documents" AutoPostBack="true" OnCheckedChanged="rdbSelectedDoc_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <table visible="false" id="SelectDoc" runat="server" width="100%">
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Our Ref No. :</span>
                                        </td>
                                        <td width="7%" nowrap>
                                            <asp:TextBox ID="txtDoc" runat="server" Width="100px" TabIndex="2" MaxLength="20"
                                                CssClass="textBox" ToolTip="Press F2 for Help" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                        <td width="2%" nowrap>
                                            <asp:Image ID="btnDocHelp" runat="server" ImageUrl="~/Style/images/help1.png" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDocName" CssClass="elementLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAdvanced" runat="server" GroupName="rdbgroup3" Checked="true"
                                                CssClass="elementLabel" Text="Advanced" AutoPostBack="true" />
                                            <asp:RadioButton ID="rdbDirect" runat="server" GroupName="rdbgroup3" CssClass="elementLabel"
                                                Text="Direct" AutoPostBack="true" />
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
                        </td>
                    </tr>
                </table>
                </td> </tr> </table> </td> </tr> </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
