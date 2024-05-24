<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Courier_Report.aspx.cs" Inherits="IMP_IMPReports_TF_IMP_Courier_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
<%--    <script src="../../Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>--%>
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">


        //        function CustHelp() {

        //            var brname = document.getElementById('ddlBranch');

        //            if (brname.value == "---Select---") {
        //                alert('Please select branchname');
        //                brname.focus();
        //                return false;

        //            }
        //            popup = window.open('../../IDPMS/TF_CustMaster.aspx', 'CustHelp', 'height=500,  width=450,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
        //            common = "CustHelp";

        //        }

        //        function selectCustomer(Cust, label) {
        //            document.getElementById('txtCust').value = Cust;
        //            document.getElementById('lblcustname').innerHTML = label;


        //        }

        function generateReport() {

            //            var brname = document.getElementById('ddlBranch');

//            var doctype = 'ALL';
            var frmdate = document.getElementById('txtfromDate');
            var todate = document.getElementById('txtToDate');
            //            if (brname.value == "---Select---") {

            //                alert('Please select branchname');
            //                brname.focus();
            //                return false;
            //            }
            if (frmdate.value == "") {
                alert('Please select From Date')
                frmdate.focus();
                return false;
            }

//            if (todate.value == "") {
//                alert('Please select To Date')
//                todate.focus();
//                return false;
//            }

            //            if (document.getElementById('rdbOtt').checked == true) {
            //                var ormtype = 'ott';
            //            }
            //            else {
            //                var ormtype = 'other';
            //            }

            //            if (document.getElementById('rdbApproval').checked == true) {
            //                var winame = window.open('View_ImportBillLodgment.aspx?branchname=' + brname.value + '&Documentype=' + doctype + '&from=' + frmdate.value + '&to=' + todate.value + '&Approval=A', '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            //                winame.focus();
            //                return false;
            //            }
            //            else {
            //                (document.getElementById('rdbRejected').checked == true)
            //                var winame = window.open('View_ImportBillLodgment.aspx?branchname=' + brname.value + '&Documentype=' + doctype + '&from=' + frmdate.value + '&to=' + todate.value + '&Approval=R', '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            //                winame.focus();
            //                return false;
            //            }

            //            var winame = window.open('View_ImportBillLodgmentReport.aspx?Documentype=' + doctype + '&from=' + frmdate.value + '&to=' + todate.value , '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            //            winame.focus();
            //            return false;
            var winame = window.open('View_TF_IMP_Courier_Report.aspx?from=' + frmdate.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            winame.focus();
            return false;

        }
    
    </script>
     <style type="text/css">
        hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 2px;
        }
    </style>
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
            <uc1:menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Record of Outgoing Mails and Courier Service Report</strong></span>
                                <%--<asp:Label ID="PageHeader" CssClass="pageLabel" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <%--<td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>--%>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                   <%-- <tr>
                                        <td width="10%" align="center" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 800px">
                                            <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
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
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            <%--&nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
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
                                            </ajaxToolkit:MaskedEditValidator>--%>
                                        </td>
                                    </tr>
                                   
                                   <%-- <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:RadioButton ID="rdbApproval" runat="server" Checked="True" GroupName="rdbgroup"
                                                CssClass="elementLabel" Text="Approval" AutoPostBack="True" OnCheckedChanged="rdbApproval_CheckedChanged" />
                                            &nbsp;<asp:RadioButton ID="rdbRejected" runat="server" GroupName="rdbgroup" CssClass="elementLabel"
                                                Text="Rejected" AutoPostBack="True" OnCheckedChanged="rdbRejected_CheckedChanged" />
                                        </td>
                                    </tr>--%>
                                </table>
                                <table id="selectedcust" visible="false" runat="server" cellspacing="0" border="0"
                                    width="100%">
                                   <%-- <tr>
                                        <td align="center" style="width: 150px">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Customer Name
                                                :</span>
                                        </td>
                                        <td width="7%" nowrap>
                                            <asp:TextBox ID="txtCust" runat="server" Width="100" TabIndex="2" MaxLength="20"
                                                CssClass="textBox" ToolTip="Press F2 for Help"></asp:TextBox>
                                        </td>
                                        <td width="1%" nowrap>
                                            <asp:Image ID="btnCustHelp" runat="server" ImageUrl="~/Style/images/help1.png" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblcustname" CssClass="elementLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>--%>
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="4"  />
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


