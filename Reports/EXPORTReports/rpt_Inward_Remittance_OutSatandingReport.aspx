<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Inward_Remittance_OutSatandingReport.aspx.cs" Inherits="Reports_EXPORTReports_rpt_Inward_Remittance_OutSatandingReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
<link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen"> 
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

        function sss() {
            var s = popup.document.getElementById('txtcell1').value;

            if (common == "helpCustId") {
                document.getElementById('txtCustomer').value = s;
            }

        }
        function Custhelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select As On Date.');
                fromDate.focus();
                return false;
            }
            popup = window.open('../../TF_CustomerLookUp1.aspx', 'helpCustId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCustId"
            return false;
        }

        function CustId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCustList').click();
            }
        }

        function Custhelp1() {
            var fromdate = document.getElementById('txtfromDate');
            var toDate = document.getElementById('txtToDate');

            if (fromdate.value == '') {
                alert('Select From Date.');
                fromdate.focus();
                return false;
            }
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }

            if (document.getElementById('rdbDocumnetwise').checked == true) {
                rptCode = 1;
            }
            else if (document.getElementById('rdbCustomerwise').checked == true) {
                rptCode = 2;
                if (document.getElementById('rdbAllCustomer').checked == true) {
                    rptType = 'All';
                }
                else if (document.getElementById('rdbSelectedCustomer').checked == true) {
                    var txt = document.getElementById('txtCustomer');
                    if (txt.value == '') {
                        alert('Enter Customer A/c No.');
                        txt.focus();
                        return false;
                    }
                    rptType = document.getElementById('txtCustomer').value;
                }
            }

        }
           

        </script>
</head>
<body>
   <form id="form2" runat="server" autocomplete="off" >
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
     <%--<script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script> --%>
    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" /> 
                                  
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>  --%>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">IRM O/S Report</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
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
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1"
                                          AutoPostBack="true" Width="100px"
                                          runat="server">
                                                </asp:DropDownList>
                                        </td>
                                    </tr>                          
                                    <tr>
                                  
                                        <td align="right" style="width: 20%">                                                                              
                                             <span class="mandatoryField">*</span> <span class="elementLabel">AS On Date:</span>
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
                                            <%--&nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date
                                                :</span> &nbsp;
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
                                </table>
                                
                                <table cellspacing="0" border="0">
                                    <tr>
                                        <td style="width: 30%">
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:RadioButton ID="rdbDocumnetwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbDocumnetwise_CheckedChanged" TabIndex="4"
                                                Text="IRM wise" Checked="true" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbCustomerwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbCustomerwise_CheckedChanged" TabIndex="4"
                                                Text="Customer wise" />
                                              <%--  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbShippingBillwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbShippingBillwise_CheckedChanged" TabIndex="4"
                                                Text="Shipping Bill wise" />--%>
                                        </td>
                                    </tr>
                                </table>
                                <fieldset id="CustList" runat="server" style="width: 900px" visible="false">
                                    <legend>Select Customer</legend>
                                    <table id="Table1" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                <span style="color: Red">*</span>Customer A/C No. :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="textBox" MaxLength="40" TabIndex="5"
                                                    Visible="false" Width="100px" ToolTip="Press F2 for Help" OnTextChanged="txtCustomer_TextChanged"
                                                    AutoPostBack="True"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="BtnCustList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                &nbsp;
                                                <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Visible="false"
                                                    Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                               <%-- <fieldset id="ShippingList" runat="server" style="width: 900px" visible="false">
                                    <legend>Select ShippingBill</legend>
                                    <table id="Table2" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                <span style="color: Red">*</span>Shipping Bill No. :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtShippingBill" runat="server" CssClass="textBox" MaxLength="40" TabIndex="5"
                                                    Visible="false" Width="100px" ToolTip="Press F2 for Help"
                                                    AutoPostBack="True"></asp:TextBox>
                                                &nbsp;
                                               <%-- <asp:Button ID="BtnShippingList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                &nbsp;
                                                <asp:Label ID="lblShippingBill" runat="server" CssClass="elementLabel" Visible="false"
                                                    Width="400px"></asp:Label>--%>
                                         <%--   </td>
                                        </tr>
                                    </table>
                                </fieldset>--%>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                           <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="7" onclick="btnSave_Click" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
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
