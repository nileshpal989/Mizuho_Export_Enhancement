<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptEXPRealisationBankCharges.aspx.cs"
    Inherits="Reports_EXPReports_rptEXPRealisationBankCharges" %>

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
        //        var today = new Date();
        //        var dd = today.getDate();
        //        var mm = today.getMonth() + 1; //January is 0! 
        //        var yyyy = today.getFullYear();
        //        if (dd < 10) { dd = '0' + dd }
        //        if (mm < 10) { mm = '0' + mm }
        //        function toDate() {

        //            if (document.getElementById('txtFromDate').value != "__/__/____") {

        //                var toDate;
        //                toDate = dd + '/' + mm + '/' + yyyy;
        //                document.getElementById('txtToDate').value = toDate;
        //            }
        //        } 
    </script>
    <script language="javascript" type="text/javascript">

        function changeDate() {

            __doPostBack('btnChangeDate', '');
        }

        function custhelp() {
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

        function curhelp() {
            popup = window.open('../../TF_CurrencyLookUp2.aspx', 'helpCurId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCurId";
            return false;

        }

        function CurId(event) {

            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCurList').click();
            }
        }
        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpCustId") {
                document.getElementById('txtCustomerID').value = s;
            }
            if (common == "helpCurId") {
                document.getElementById('txtCurrency').value = s;
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

            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }

            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;

            // var rptType;
            var Cur;
            var rptCode;
            var custACNo;



            if (document.getElementById('rdbAllcur').checked == true) {

                    if (document.getElementById('rdbAllCustomer').checked == true) {

                        rptCode = 1;
                        rptType = 'All';
                        Cur = 'ALLCUR';
                        custACNo = 'AllCUST';

                    }
                    else if (document.getElementById('rdbSelectedCustomer').checked == true) {
                        var custACNo = document.getElementById('txtCustomerID');


                        if (custACNo == '') {
                            alert('Enter Customer Account No..');
                            txtCustomerID.focus();
                            return false;
                        }

                        rptCode = 2;
                        Cur = 'ALLCUR';
                        custACNo = document.getElementById('txtCustomerID').value;

                    }
            }
            else {

                if (document.getElementById('rdbAllCustomer').checked == true) {
                    Cur = document.getElementById('txtCurrency').value;
                    if (Cur == '') {
                        alert('Enter Currency.');
                        txtCurrency.focus();
                        return false;
                    }
                    rptCode = 3;

                    Cur = document.getElementById('txtCurrency').value;
                    custACNo = 'AllCUST';


                }
                else if (document.getElementById('rdbSelectedCustomer').checked == true) {

                    custACNo = document.getElementById('txtCustomerID').value;

                    if (custACNo == '') {
                        alert('Enter Customer Account No..');
                        txtCustomerID.focus();
                        return false;
                    }

                    rptCode = 4;
                    Cur = document.getElementById('txtCurrency').value;
                    custACNo = document.getElementById('txtCustomerID').value;

                }
            }
            var winname = window.open('View_rptEXPRealisationBankCharges.aspx?frm=' + from + '&to=' + to + '&Cur=' + Cur + '&rptCode=' + rptCode + '&branch=' + Branch + '&custACNo=' + custACNo, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1100,height=550');
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
                                <span class="pageLabel">Export Realisation Bank Charges</span>
                            </td>
                            <td align="right" style="width: 50%">
                                &nbsp;
                                <input type="hidden" id="hdnDocId" runat="server" />
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
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <%--<span class="pageLabel">&nbsp;</span><asp:CheckBox ID="CheckOfficeCopy"
                                                runat="server" ForeColor="#003399" Text=" Office Copy  " TabIndex="2" />--%>
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
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
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
                                </table>
                                <table cellspacing="0" border="0" width="550px">
                                    <tr width="550px">
                                        <td class="style8" />
                                        <td height="40px" align="left" valign="bottom" class="style6">
                                            <asp:RadioButton ID="rdbAllcur" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbAllcur_CheckedChanged" Text="All Currency"
                                                Checked="True" TabIndex="5" />
                                        <td height="40px" align="Left" valign="bottom">
                                            <asp:RadioButton ID="rdbSingleCur" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Currency" GroupName="Data2" OnCheckedChanged="rdbSingleCur_CheckedChanged"
                                                TabIndex="5" />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10px" />
                                        <td height="10px" align="Left" valign="bottom" colspan="2">
                                            <fieldset id="Curlist" runat="server" style="width: 600px" visible="false">
                                                <legend><span class="pageLabel">Select Currency</span></legend>
                                                <table id="Table1" runat="server">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="pageLabel">Currency :&nbsp;</span>
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                                                                Width="55px" Visible="false" AutoPostBack="True" OnTextChanged="txtCurrency_TextChanged"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Button ID="btnCurList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                            &nbsp;
                                                            <asp:Label ID="lblCurrency" runat="server" CssClass="elementLabel" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" border="0" width="550px">
                                    <tr width="550px">
                                        <td class="style8" />
                                        <td height="40px" align="left" valign="bottom" class="style6">
                                            <asp:RadioButton ID="rdbAllCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data1" OnCheckedChanged="rdbAllCustomer_CheckedChanged" Text="All Customers"
                                                Checked="True" TabIndex="5" />
                                        <td height="40px" align="Left" valign="bottom">
                                            <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Selected Customer" GroupName="Data1" OnCheckedChanged="rdbSelectedCustomer_CheckedChanged"
                                                TabIndex="5" />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10px" />
                                        <td height="10px" align="Left" valign="bottom" colspan="2">
                                            <fieldset id="Custlist" runat="server" style="width: 600px" visible="false">
                                                <legend><span class="pageLabel">Select Customer AC No.</span></legend>
                                                <table id="Table2" runat="server">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="pageLabel">Customer AC No. :&nbsp;</span>
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="6" Width="55px" Visible="false" AutoPostBack="True" OnTextChanged="txtCustomerID_TextChanged"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Button ID="btnCustList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                            &nbsp;
                                                            <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
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
                                                ToolTip="Generate" TabIndex="12" />
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
