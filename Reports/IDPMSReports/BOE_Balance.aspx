<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BOE_Balance.aspx.cs" Inherits="Reports_EDPMS_Reports_BOE_Balance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />--%>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function generateReport() {

            var brname = document.getElementById('ddlBranch');
            var rdball = document.getElementById('rdball');
            var rdbutilized = document.getElementById('rdbutilized');
            var rdbos = document.getElementById('rdbos');
            var ErrorCode = "";
            var modtype = "";
            if (brname.value == "---Select---") {

                alert('Please select branchname');
                brname.focus();
                return false;

            }
            var fromDate = document.getElementById('txtFromDate');
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

            if (document.getElementById('rdbselected').checked == true) {
                var cust = document.getElementById('txtCust');
                if (cust.value == '') {
                    alert('Select Customer ID.');
                    cust.focus();
                    return false;
                }
            }


            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;
            //var ormtype = document.getElementById('rdbOtt').value;

            if (document.getElementById('rdbOtt').checked == true) {
                var ormtype = 'ott';
            }
            else {
                var ormtype = 'other';
            }

            if (document.getElementById('rdball').checked == true) {
                if (document.getElementById('rdballcust').checked == true) {
                    var winame = window.open('View_BOE_Balance.aspx?branchname=' + brname.value + '&from=' + from + '&to=' + to + '&mode=all&type=all' + '&ormtype=' + ormtype, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
                else {
                    var cust = document.getElementById('txtCust').value;
                    var winame = window.open('View_BOE_Balance.aspx?branchname=' + brname.value + '&from=' + from + '&to=' + to + '&mode=all&type=' + cust + '&ormtype=' + ormtype, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
            }
            else if (document.getElementById('rdbos').checked == true) {
                if (document.getElementById('rdballcust').checked == true) {
                    var winame = window.open('View_BOE_Balance.aspx?branchname=' + brname.value + '&from=' + from + '&to=' + to + '&mode=os&type=all' + '&ormtype=' + ormtype, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
                else {
                    var cust = document.getElementById('txtCust').value;
                    var winame = window.open('View_BOE_Balance.aspx?branchname=' + brname.value + '&from=' + from + '&to=' + to + '&mode=os&type=' + cust + '&ormtype=' + ormtype, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
            }
            else {
                if (document.getElementById('rdballcust').checked == true) {
                    var winame = window.open('View_BOE_Balance.aspx?branchname=' + brname.value + '&from=' + from + '&to=' + to + '&mode=closed&type=all' + '&ormtype=' + ormtype, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
                else {
                    var cust = document.getElementById('txtCust').value;
                    var winame = window.open('View_BOE_Balance.aspx?branchname=' + brname.value + '&from=' + from + '&to=' + to + '&mode=closed&type=' + cust + '&ormtype=' + ormtype, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
            }
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
            <uc2:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left">
                                <span class="pageLabel"><strong>Bill of Entry Balance Report</strong> </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="115px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                Width="70" TabIndex="2"></asp:TextBox>
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
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date:</span>
                                            &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="3"></asp:TextBox>
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
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:RadioButton ID="rdball" runat="server" Checked="True" GroupName="rdbgroup1"
                                                CssClass="elementLabel" Text="All" AutoPostBack="True" OnCheckedChanged="rdball_CheckedChanged" />
                                            &nbsp;<asp:RadioButton ID="rdbos" runat="server" GroupName="rdbgroup1" CssClass="elementLabel"
                                                Text="Only O/S" AutoPostBack="True" OnCheckedChanged="rdbos_CheckedChanged" />
                                            &nbsp;<asp:RadioButton ID="rdbclosed" runat="server" GroupName="rdbgroup1" CssClass="elementLabel"
                                                Text="Closed BOE" AutoPostBack="True" OnCheckedChanged="rdbclosed_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:RadioButton ID="rdballcust" runat="server" Checked="True" GroupName="rdbgroup"
                                                CssClass="elementLabel" Text="All Customers" AutoPostBack="True" OnCheckedChanged="rdballcust_CheckedChanged" />
                                            &nbsp;<asp:RadioButton ID="rdbselected" runat="server" GroupName="rdbgroup" CssClass="elementLabel"
                                                Text="Selected Customer" AutoPostBack="True" OnCheckedChanged="rdbselected_CheckedChanged" />
                                        </td>
                                    </tr>
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
                                </table>
                                <table id="selectedcust" visible="false" runat="server" cellspacing="0" border="0"
                                    width="100%">
                                    <tr>
                                        <td align="center" style="width: 150px">
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
                                <%--<table cellspacing="0" cellpadding="0" border="0" width="20%" >
                                    <tr id="SelectedCust" runat="server" visible="false">
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Customer ID :</span>
                                        </td>
                                        <td width="8%" align="left">
                                            <asp:TextBox ID="txtCust" runat="server" Width="100" TabIndex="2" MaxLength="20"
                                                CssClass="textBox" ToolTip="Press F2 for Help"></asp:TextBox>
                                        </td>
                                        <td width="2%" align="left">
                                            <asp:Image ID="btnCustHelp" runat="server" ImageUrl="~/Style/images/help1.png" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblcustname" CssClass="elementLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>--%>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
