<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExportBillRegister.aspx.cs"
    Inherits="Reports_EXPReports_rptExportBillRegister" %>

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
        function Custhelp() {
            var fromDate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');

            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;

            var adcode = document.getElementById('ddlBranch').value;
            popup = window.open('../../TF_CustomerLookUp2.aspx?adcode=' + adcode, 'helpCustId', 'height=520,  width=620,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCustId"
            return false;

        }

        function selectCustomer(selectedID) {
            var id = selectedID;
            document.getElementById('hdnCustId').value = id;
            document.getElementById('txtCustomer').value = selectedID;
            document.getElementById('txtCustomer').focus();
            document.getElementById('BtnCustList').click();
        }

        function CustId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('BtnCustList').click();
            }
        }

        function OVpartyhelp() {
            var fromDate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');

            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;

            popup = window.open('EXP_OverseasPartyLookup.aspx', 'helpOVPartyId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpOVPartyId";
            return false;

        }

        function OVPartyId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnOVPList').click();
            }
        }


        function OVBankhelp() {
            var fromDate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');

            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;

            popup = window.open('EXP_OverseasBankLookup.aspx', 'helpOVBankId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpOVBankId";
            return false;

        }

        function OVBankId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnOVBankList').click();
            }
        }


        function Countryhelp() {
            popup = window.open('../../TF_CountryLookup.aspx', 'helpCountryId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCountryId";
            return false;

        }

        function CountryId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCountryList').click();
            }
        }

        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpOVPartyId") {
                document.getElementById('txtOVPID').value = s;
            }
            if (common == "helpCustId") {
                document.getElementById('txtCustomer').value = s;
            }
            if (common == "helpOVBankId") {
                document.getElementById('txtOVBank').value = s;
            }
            if (common == "helpCountryId") {
                document.getElementById('txtCountry').value = s;
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
            var rptType = "";
            var rptCode;
            var rptDocType;
            var rptValue;
            var rptLoan;
            var rptCS;
            var rptLC;

            if (document.getElementById('rbtbla').checked == true) {
                rptDocType = 'BLA';
            }
            if (document.getElementById('rbtblu').checked == true) {
                rptDocType = 'BLU';
            }
            if (document.getElementById('rbtbba').checked == true) {
                rptDocType = 'BBA';
            }
            if (document.getElementById('rbtbbu').checked == true) {
                rptDocType = 'BBU';
            }
            if (document.getElementById('rbtbca').checked == true) {
                rptDocType = 'BCA';
            }
            if (document.getElementById('rbtbcu').checked == true) {
                rptDocType = 'BCU';
            }
            if (document.getElementById('rbtIBD').checked == true) {
                rptDocType = 'IBD';
            }
//            if (document.getElementById('rbtLBC').checked == true) {
//                rptDocType = 'LBC';
//            }
            if (document.getElementById('rbtBEB').checked == true) {
                rptDocType = 'EB';
            }
             if (document.getElementById('rdbAll').checked == true) {
                rptDocType = 'ALL';
            }


            if (document.getElementById('rdbLoanAdv').checked == true) {
                rptLoan = 'Y';
            }
            else if (document.getElementById('rdbLoanNotAdv').checked == true) {
                rptLoan = 'N';
            }
            else if (document.getElementById('rdbLoanAll').checked == true) {
                rptLoan = 'BOTH';
            }

            if (document.getElementById('rdbLC').checked == true) {
                rptLC = 'LC';
            }
            else if (document.getElementById('rdbNonLC').checked == true) {
                rptLC = 'NLC';
            }
            else if (document.getElementById('rdbLCAll').checked == true) {
                rptLC = 'AllLC';
            }

            if (document.getElementById('ChkCS').checked == true) {
                rptCS = 'Y';
            }
            else {
                rptCS = 'N';
            }


            if (document.getElementById('rdbDocumnetwise').checked == true) {
                rptCode = 1;
            }
            else if (document.getElementById('rdbCustomerwise').checked == true) {
                rptCode = 2;
                if (document.getElementById('rdbAllCustomer').checked == true) {
                    rptType = 'ALLCUST';
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


            if (document.getElementById('rdbOVPartywise').checked == true) {
                rptCode = 3;
                if (document.getElementById('rdbAllOverseasParty').checked == true) {
                    rptType = 'ALLOVP';
                }
                else if (document.getElementById('rdbSelectedOverseasParty').checked == true) {
                    var txt = document.getElementById('txtOVPID');
                    if (txt.value == '') {
                        alert('Enter Overseas Party Id');
                        txt.focus();
                        return false;
                    }

                    rptType = document.getElementById('txtOVPID').value;
                }

            }
            if (document.getElementById('rdbOVBankwise').checked == true) {
                rptCode = 4;
                if (document.getElementById('rdbAllOverseasBank').checked == true) {
                    rptType = 'ALLOVB';
                }
                else if (document.getElementById('rdbSelectedOverseasBank').checked == true) {
                    var txt = document.getElementById('txtOVBank');
                    if (txt.value == '') {
                        alert('Enter Overseas Bank ID');
                        txt.focus();
                        return false;
                    }

                    rptType = document.getElementById('txtOVBank').value;
                }

            }
            if (document.getElementById('rdbCountrywise').checked == true) {
                rptCode = 5;
                if (document.getElementById('rdbAllCountry').checked == true) {
                    rptType = 'ALLC';
                }
                else if (document.getElementById('rdbSelectedCountry').checked == true) {
                    var txt = document.getElementById('txtCountry');
                    if (txt.value == '') {
                        alert('Enter Country ID');
                        txt.focus();
                        return false;
                    }

                    rptType = document.getElementById('txtCountry').value;
                }
            }
             var winname = window.open('View_rptExportBillRegister.aspx?frm=' + from + '&to=' + to + '&rptDocType=' + rptDocType + '&rptLoan=' + rptLoan + '&rptLC=' + rptLC + '&rptCS=' + rptCS + '&rptType=' + encodeURIComponent(rptType) + '&rptCode=' + rptCode + '&branch=' + Branch, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1100,height=550');
            winname.focus();
            return false;
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
                <%--  <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>--%>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Export Bill Register</strong></span>
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
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="true" Width="100px" runat="server">
                                            </asp:DropDownList>
                                            <%--<asp:Button ID="btnPurCodeList" runat="server" CssClass="btnHelp_enabled" 
                                                    TabIndex="-1" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Doc Date :</span>
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
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Doc Date :</span>
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
                                <fieldset id="DocTypeList" runat="server" style="width: 900px" visible="true">
                                    <table cellspacing="0" border="0">
                                        <tr>
                                            <td height="20px" align="left" valign="middle"  nowrap>
                                                <asp:RadioButton ID="rbtbla" runat="server" CssClass="elementLabel" TabIndex="3"
                                                    Text="Bills Bght with L/C at Sight" Style="font-weight: bold;" GroupName="TransType" />
                                            </td>
                                            <td height="20px" align="left" valign="middle"  nowrap>
                                                <asp:RadioButton ID="rbtbba" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C at Sight"
                                                    GroupName="TransType" TabIndex="4" Style="font-weight: bold;" />
                                            </td>
                                            <td height="20px" align="left" valign="middle"  nowrap>
                                                <asp:RadioButton ID="rbtblu" runat="server" CssClass="elementLabel" Text="Bills Bght with L/C Usance"
                                                    GroupName="TransType" TabIndex="5" Style="font-weight: bold;" />
                                            </td>
                                            <td height="20px" align="left" valign="middle" nowrap>
                                                <asp:RadioButton ID="rbtbbu" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C Usance"
                                                    GroupName="TransType" TabIndex="6" Style="font-weight: bold;" />
                                            </td>
                                            <td height="20px" align="left" valign="middle"  nowrap>
                                                <asp:RadioButton ID="rbtbca" runat="server" CssClass="elementLabel" Text="Bills For Coll. at Sight "
                                                    GroupName="TransType" TabIndex="7" Style="font-weight: bold;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="20px" align="left" valign="middle" width="150px" nowrap>
                                                <asp:RadioButton ID="rbtbcu" runat="server" CssClass="elementLabel" Text="Bills For Coll. Usance"
                                                    GroupName="TransType" TabIndex="8" Style="font-weight: bold;" />
                                            </td>
                                            <td height="20px" align="left" valign="middle" width="150px" nowrap>
                                                <asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Vendor Bills Dis."
                                                    GroupName="TransType" TabIndex="9" Style="font-weight: bold;" />
                                            </td>
                                            
                                            <td height="20px" align="left" valign="middle" width="150px" nowrap>
                                                <asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                                    GroupName="TransType" TabIndex="10" Style="font-weight: bold;" />
                                            </td>
                                            <td height="20px" align="left" valign="middle" width="150px" nowrap>
                                                <asp:RadioButton ID="rdbAll" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="TransType" Text="ALL" Style="forecolor: #000000; font-weight: bold;"
                                                    TabIndex="11" Checked="True" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <table cellspacing="0" border="0">
                                    <tr>
                                        <td height="40px" align="left" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbLoanAdv" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Loan" Text="Loan Advanced" Style="forecolor: #000000; font-weight: bold;"
                                                TabIndex="5" />
                                        </td>
                                        <td height="20px" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbLoanNotAdv" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Loan Not Advanced" GroupName="Loan" Style="forecolor: #000000; font-weight: bold;"
                                                TabIndex="5" />
                                        </td>
                                        <td height="40px" valign="middle" width="170px">
                                            <asp:RadioButton ID="rdbLoanAll" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="All" GroupName="Loan" Style="forecolor: #000000; font-weight: bold;" TabIndex="5"
                                                Checked="True" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkCS" runat="server" ForeColor="#003399" Text="CS" TabIndex="-1" />
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" border="0">
                                    <tr>
                                        <td height="40px" align="left" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbLC" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="LC" Text="LC's " Style="forecolor: #000000; font-weight: bold;" TabIndex="5" />
                                        </td>
                                        <td height="20px" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbNonLC" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Non LC's" GroupName="LC" Style="forecolor: #000000; font-weight: bold;"
                                                TabIndex="5" />
                                        </td>
                                        <td height="40px" valign="middle" width="170px">
                                            <asp:RadioButton ID="rdbLCAll" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="All" GroupName="LC" Style="forecolor: #000000; font-weight: bold;" TabIndex="5"
                                                Checked="True" />
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" border="0">
                                    <tr>
                                        <td height="40px" align="left" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbDocumnetwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data" OnCheckedChanged="rdbDocumnetwise_CheckedChanged" Text="Document wise"
                                                Style="forecolor: #000000; font-weight: bold;" TabIndex="6" Checked="True" />
                                        </td>
                                        <td height="20px" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbCustomerwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Customer wise" GroupName="Data" OnCheckedChanged="rdbCustomerwise_CheckedChanged"
                                                Style="forecolor: #000000; font-weight: bold;" TabIndex="6" />
                                        </td>
                                        <td height="40px" valign="middle" width="170px">
                                            <asp:RadioButton ID="rdbOVPartywise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Overseas Party wise" GroupName="Data" OnCheckedChanged="rdbOVPartywise_CheckedChanged"
                                                Style="forecolor: #000000; font-weight: bold;" TabIndex="6" />
                                        </td>
                                        <td height="40px" valign="middle" width="170px">
                                            <asp:RadioButton ID="rdbOVBankwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Overseas Bank wise" GroupName="Data" OnCheckedChanged="rdbOVBankwise_CheckedChanged"
                                                Style="forecolor: #000000; font-weight: bold;" TabIndex="6" />
                                        </td>
                                        <td height="40px" valign="middle" width="170px">
                                            <asp:RadioButton ID="rdbCountrywise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Country wise" GroupName="Data" OnCheckedChanged="rdbCountrywise_CheckedChanged"
                                                Style="forecolor: #000000; font-weight: bold;" TabIndex="6" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAllCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbAllCustomer_CheckedChanged" TabIndex="7"
                                                Text="All Customer" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <br />
                                            <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbSelectedCustomer_CheckedChanged" TabIndex="7"
                                                Text="Selected Customer" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAllOverseasParty" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data1" OnCheckedChanged="rdbAllOverseasParty_CheckedChanged" TabIndex="7"
                                                Text="All Overseas Party" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <br />
                                            <asp:RadioButton ID="rdbSelectedOverseasParty" runat="server" AutoPostBack="true"
                                                CssClass="elementLabel" GroupName="Data1" OnCheckedChanged="rdbSelectedOverseasParty_CheckedChanged"
                                                TabIndex="7" Text="Selected Overseas Party" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAllOverseasBank" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data3" OnCheckedChanged="rdbAllOverseasBank_CheckedChanged" TabIndex="7"
                                                Text="All Overseas Bank" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <br />
                                            <asp:RadioButton ID="rdbSelectedOverseasBank" runat="server" AutoPostBack="true"
                                                CssClass="elementLabel" GroupName="Data3" OnCheckedChanged="rdbSelectedOverseasBank_CheckedChanged"
                                                TabIndex="7" Text="Selected Overseas Bank" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAllCountry" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data3" OnCheckedChanged="rdbAllCountry_CheckedChanged" TabIndex="7"
                                                Text="All Country" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <br />
                                            <asp:RadioButton ID="rdbSelectedCountry" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data3" OnCheckedChanged="rdbSelectedCountry_CheckedChanged" TabIndex="7"
                                                Text="Selected Country" />
                                        </td>
                                    </tr>
                                </table>
                                <fieldset id="CustList" runat="server" style="width: 900px" visible="false">
                                    <legend>Select Customer</legend>
                                    <table id="Table1" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" nowrap>
                                                <span style="color: Red">*</span>Customer A/c No. :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="textBox" MaxLength="40" TabIndex="8"
                                                    Visible="false" Width="100px" OnTextChanged="txtCustomer_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                &nbsp;
                                                <input type="hidden" id="hdnCustId" runat="server" />
                                                <asp:Button ID="BtnCustList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                &nbsp;
                                                <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Visible="false"
                                                    Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset id="OVPartylist" runat="server" style="width: 900px" visible="false">
                                    <legend>Select Overseas Party Name</legend>
                                    <table id="Table2" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" nowrap>
                                                <span style="color: Red">*</span>Oveseas Party Id :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;
                                                <asp:TextBox ID="txtOVPID" runat="server" CssClass="textBox" MaxLength="40" TabIndex="9"
                                                    Visible="false" Width="100px" OnTextChanged="txtOVPID_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="btnOVPList" runat="server" CssClass="btnHelp_enabled" Visible="false"
                                                    Height="16px" />
                                                <asp:Label ID="lblOVPartyName" runat="server" CssClass="elementLabel" Visible="false"
                                                    Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset id="OVBanklist" runat="server" style="width: 900px" visible="false">
                                    <legend>Select Overseas Bank Name</legend>
                                    <table id="Table3" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" nowrap>
                                                <span style="color: Red">*</span>Oveseas Bank Id :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;
                                                <asp:TextBox ID="txtOVBank" runat="server" CssClass="textBox" MaxLength="40" TabIndex="10"
                                                    Visible="false" Width="100px" OnTextChanged="txtOVBank_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="btnOVBankList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                <asp:Label ID="lblOVbankName" runat="server" CssClass="elementLabel" Visible="false"
                                                    Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset id="CountryList" runat="server" style="width: 900px" visible="false">
                                    <legend>Select Country</legend>
                                    <table id="Table4" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" nowrap>
                                                <span style="color: Red">*</span>Country Id. :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;
                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="textBox" MaxLength="40" TabIndex="11"
                                                    Visible="false" Width="100px" AutoPostBack="True" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                &nbsp;
                                                <asp:Label ID="lblCountyName" runat="server" CssClass="elementLabel" Visible="false"
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
                                                ToolTip="Generate" TabIndex="12" />
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
