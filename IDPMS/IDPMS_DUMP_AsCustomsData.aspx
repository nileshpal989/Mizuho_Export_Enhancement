<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDPMS_DUMP_AsCustomsData.aspx.cs"
    Inherits="IDPMS_IDPMS_DUMP_AsCustomsData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function CustHelp() {

            var brname = document.getElementById('ddlBranch');

            if (brname.value == "---Select---") {
                alert('Please select branchname');
                brname.focus();
                return false;
            }
            popup = window.open('TF_CustMaster.aspx', 'CustHelp', 'height=500,  width=500,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "CustHelp";
        }

        function selectCustomer(Cust, label) {
            document.getElementById('txtCust').value = Cust;
            document.getElementById('lblcustname').innerHTML = label;

        }

        function generateReport() {

            var brname = document.getElementById('ddlBranch');
            var rdball = document.getElementById('rdball');
            var rdbsucessful = document.getElementById('rdbsuccessful');
            var rdbonlyerror = document.getElementById('rdberror');



            var ErrorCode = "";
            var parameter = "";
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


            if (rdball.checked == true) {

                //                ErrorCode = 'All';
                if (document.getElementById('rdballcust').checked == true) {
                    parameter = 'All';
                    var from = document.getElementById('txtFromDate').value;
                    var to = document.getElementById('txtToDate').value;
                    var winame = window.open('VIEW_IDPMS_DUMP_AsCustomsData.aspx?branchname=' + brname.value + '&parameter=' + parameter + '&from=' + from + '&to=' + to + '&type=all', '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
                else {
                    parameter = 'All';
                    var from = document.getElementById('txtFromDate').value;
                    var to = document.getElementById('txtToDate').value;
                    var winame = window.open('VIEW_IDPMS_DUMP_AsCustomsData.aspx?branchname=' + brname.value + '&parameter=' + parameter + '&from=' + from + '&to=' + to + '&type=' + cust.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }



            }

            else if (rdbsucessful.checked == true) {

                //                ErrorCode = 'SUCCESS';
                if (document.getElementById('rdballcust').checked == true) {
                    parameter = 'Match';
                    var from = document.getElementById('txtFromDate').value;
                    var to = document.getElementById('txtToDate').value;
                    var winame = window.open('VIEW_IDPMS_DUMP_AsCustomsData.aspx?branchname=' + brname.value + '&parameter=' + parameter + '&from=' + from + '&to=' + to + '&type=all', '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
                else {
                    parameter = 'Match';
                    var from = document.getElementById('txtFromDate').value;
                    var to = document.getElementById('txtToDate').value;
                    var winame = window.open('VIEW_IDPMS_DUMP_AsCustomsData.aspx?branchname=' + brname.value + '&parameter=' + parameter + '&from=' + from + '&to=' + to + '&type=' + cust.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }

            }
            else if (rdbonlyerror.checked == true) {

                //                ErrorCode = 'Error';
                if (document.getElementById('rdballcust').checked == true) {
                    parameter = 'Unmatch';
                    var from = document.getElementById('txtFromDate').value;
                    var to = document.getElementById('txtToDate').value;
                    var winame = window.open('VIEW_IDPMS_DUMP_AsCustomsData.aspx?branchname=' + brname.value + '&parameter=' + parameter + '&from=' + from + '&to=' + to + '&type=all', '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }
                else {
                    parameter = 'Unmatch';
                    var from = document.getElementById('txtFromDate').value;
                    var to = document.getElementById('txtToDate').value;
                    var winame = window.open('VIEW_IDPMS_DUMP_AsCustomsData.aspx?branchname=' + brname.value + '&parameter=' + parameter + '&from=' + from + '&to=' + to + '&type=' + cust.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
                    winame.focus();
                    return false;
                }


            }

        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManagerMain" runat="server">
    </asp:scriptmanager>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <asp:updateprogress id="updateProgress" runat="server" dynamiclayout="true">
        <progresstemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </progresstemplate>
    </asp:updateprogress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:updatepanel id="UpdatePanelMain" runat="server">
                <contenttemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left">
                                <span class="pageLabel"><b>BOE Data As Per DUMP</b></span>
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
                                                Width="100px" runat="server">
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
                                            <asp:RadioButton ID="rdball" runat="server" Text="All" CssClass="elementLabel" Checked="true"
                                                GroupName="ErrorCode" />
                                            <asp:RadioButton ID="rdbsuccessful" runat="server" CssClass="elementLabel" Text="Matched"
                                                GroupName="ErrorCode" />
                                            <asp:RadioButton ID="rdberror" runat="server" CssClass="elementLabel" Text="Unmatched"
                                                GroupName="ErrorCode" />
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
                                                CssClass="textBox" ToolTip="Press F2 for Help"></asp:TextBox>
                                        </td>
                                        <td width="2%" nowrap>
                                            <asp:Image ID="btnCustHelp" runat="server" ImageUrl="~/Style/images/help1.png" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblcustname" CssClass="elementLabel" runat="server" Text=""></asp:Label>
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
                </contenttemplate>
            </asp:updatepanel>
        </center>
    </div>
    </form>
</body>
</html>
