<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptETXRegister.aspx.cs" Inherits="Reports_XOSReports_rptETXRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
     <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen"> 
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>

    <script type="text/javascript">
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
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

    // This For CustHelp 
    
        function Custhelp() {
            var Branch = document.getElementById('ddlBranch').value;
            var fromDate = document.getElementById('txtFromDate');
            var cust = document.getElementById('txtCustomer').value;
            var toDate = document.getElementById('txtToDate');

            if (Branch == 0) {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }

            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.setfocus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;
            var Branch1 = document.getElementById('ddlBranch').value;
            




            popup = window.open('HelpETXRegister.aspx?Branch=' + Branch1 + '&fromDate=' + from + '&toDate=' + to + '&cust=' + cust, 'helpCustId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCustId"
            return false;

        }
        function CustId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('BtnCustList').click();
            }
        }



        function sss() {
            var s = popup.document.getElementById('txtcell1').value;

            if (common == "helpCustId") {
                document.getElementById('txtCustomer').value = s;
            }

        }

        function selectUser(Uname) {

            document.getElementById('txtCustomer').value = Uname;
            document.getElementById('BtnCustList').click();
            document.getElementById('txtCustomer').focus();

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
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }

            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;


            if (document.getElementById('rdbAllCustomer').checked == true) {
                var Cust = "All";
            }

            else if (document.getElementById('rdbSelectedCustomer').checked == true) {
                var txt = document.getElementById('txtCustomer');
                if (txt.value == '') {
                    alert('Enter Customer A/c No.');
                    txt.focus();
                    return false;
                }

                var Cust = document.getElementById('txtCustomer').value;
            }



            var winname = window.open('viewETXReport.aspx?Branch=' + Branch + '&fromDate=' + from + '&todate=' + to + '&Cust=' + Cust, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1050,height=550');

            winname.focus();
            return false;
        }
        
    </script>
    
    <script language="javascript" type="text/javascript">   
      function fn_DateCompare() {

                var FromDate = document.getElementById('txtFromDate');
                var ToDate = document.getElementById('txtToDate');

                var day = FromDate.value.split("/")[0];
                var month = FromDate.value.split("/")[1];
                var year = FromDate.value.split("/")[2];

                var day1 = ToDate.value.split("/")[0];
                var month1 = ToDate.value.split("/")[1];
                var year1 = ToDate.value.split("/")[2];
         

            var dateOne = new Date(year, month - 1, day); //Year, Month, Date
            var dateTwo = new Date(year1, month1 - 1, day1);
            if (dateOne > dateTwo) {
                alert(' FromDate Cannot be Greater Than To Date');
                FromDate.focus();
                return false;
            
            }
            return true;
        }
    </script>




</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
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
                                <span class="pageLabel">XOS ETX Register</span>
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
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="true" Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
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
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date:
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" 
                                                TabIndex="3" Width="70"></asp:TextBox>
                                            </span>&nbsp;<asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
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
                                </table>
                                <table cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdbAllCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbAllCustomer_CheckedChanged" TabIndex="4"
                                                Text="All Customer" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbSelectedCustomer_CheckedChanged" TabIndex="4"
                                                Text="Selected Customer" />
                                        </td>
                                    </tr>
                                </table>
                                <fieldset id="CustList" runat="server" style="width: 900px" visible="false">
                                    <legend>Select Customer</legend>
                                    <table id="Table1" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                <span style="color: Red">*</span>Customer A/c No. :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="textBox" MaxLength="40" ToolTip="Press F2 For Help"
                                                    TabIndex="5" Visible="false" Width="75px" OnTextChanged="txtCustomer_TextChanged"
                                                    AutoPostBack="True" ></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="BtnCustList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                &nbsp;
                                                <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Visible="false"
                                                    Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate Report" TabIndex="6"  />
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
