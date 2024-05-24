<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EXP_BILL_Realisation_Report.aspx.cs" Inherits="Reports_EXPORTReports_TF_EXP_BILL_Realisation_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" />
    <link href="../../Style/style_new.css" rel="stylesheet" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Help_Plugins/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="../../Help_Plugins/jquerynew.min.js" type="text/javascript"></script>
    <link href="../../Help_Plugins/jquery-ui.css" rel="stylesheet" />
    <script src="../../Help_Plugins/jquery-ui.js" type="text/javascript"></script>


    <script type="text/javascript">
        function Custhelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
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

        function OVpartyhelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select As On Date.');
                fromDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            popup = window.open('../../Reports/EXPReports/EXP_OverseasPartyLookup.aspx', 'helpOVPartyId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpOVPartyId"
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

            if (fromDate.value == '') {
                alert('Select As on Date.');
                fromDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            popup = window.open('../../Reports/EXPReports/EXP_OverseasBankLookup.aspx', 'helpOVBankId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpOVBankId"
            return false;

        }

        function OVBankId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnOVBankList').click();
            }
        }

        function Countryhelp() {
            popup = window.open('../../TF_CurrencyLookUp2.aspx', 'helpCountryId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCountryId"
            return false;

        }

        function CountryId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCountryList').click();
            }
        }

        function Consigneehelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select As On Date.');
                fromDate.focus();
                return false;
            }
            popup = window.open('../../HelpForms/TF_EXP_ConsigneePARTY_Helpform_RPT.aspx?bankID=' + "", 'helpConsigneeId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpConsigneeId"
            return false;
        }

        function ConsigneeId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnConsigneeList').click();
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
            if (common == "helpConsigneeId") {
                document.getElementById('txtConsignee').value = s;
            }
        }
    </script>

    <script type="text/javascript">  
        function validateSave() {
            var Branch = document.getElementById('ddlBranch');
            var fromDate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');
            if (Branch.value == 0) {
                alert('Select Branch First.');
                Branch.focus();
                return false;
            }
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            if (toDate.value == '') {
                alert('Select To Date.');
                fromDate.focus();
                return false;
            }
            else {
                var progressbar = setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);

                if (document.getElementById('rdbCustomerwise').checked == true) {
                    if (document.getElementById('rdbSelectedCustomer').checked == true) {
                        var txt = document.getElementById('txtCustomer');
                        if (txt.value == '') {
                            alert('Enter Customer A/c No.');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbOVPartywise').checked == true) {
                    if (document.getElementById('rdbSelectedOverseasParty').checked == true) {
                        var txt = document.getElementById('txtOVPID');
                        if (txt.value == '') {
                            alert('Enter Overseas Party Id');
                            clearTimeout(progressbar);
                            progressbar = null;
                            txt.focus();
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbOVBankwise').checked == true) {
                    if (document.getElementById('rdbSelectedOverseasBank').checked == true) {
                        var txt = document.getElementById('txtOVBank');
                        if (txt.value == '') {
                            alert('Enter Overseas Bank ID');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbCountrywise').checked == true) {
                    if (document.getElementById('rdbSelectedCountry').checked == true) {
                        var txt = document.getElementById('txtCountry');
                        if (txt.value == '') {
                            alert('Enter Currency');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbConsignee').checked == true) {
                    if (document.getElementById('rdbSelectedConsignee').checked == true) {
                        var txt = document.getElementById('txtConsignee');
                        if (txt.value == '') {
                            alert('Enter Consignee Id');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
                document.getElementById('hdnrptCode').value = rptCode;
                document.getElementById('hdnrptType').value = rptType;
            }
        }

        function ShowProgress() {
            var fromdate = document.getElementById("txtFromDate").value;
            var todate = document.getElementById("txtToDate").value;
            var branch = document.getElementById("ddlBranch").value;
            if (branch == "0" || branch == "---Select---") {
                alert("Please Select Branch First.");
                return false;
            }
            else if (fromdate == "") {
                alert("Please Select From Realised date First.");
                return false;
            }
            else if (todate == "") {
                alert("Please Select To Realised Date First.");
                return false;
            }
            else {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
                var progressbar = setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);

                if (document.getElementById('rdbCustomerwise').checked == true) {
                    if (document.getElementById('rdbSelectedCustomer').checked == true) {
                        var txt = document.getElementById('txtCustomer');
                        if (txt.value == '') {
                            alert('Enter Customer A/c No.');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbOVPartywise').checked == true) {
                    if (document.getElementById('rdbSelectedOverseasParty').checked == true) {
                        var txt = document.getElementById('txtOVPID');
                        if (txt.value == '') {
                            alert('Enter Overseas Party Id');
                            clearTimeout(progressbar);
                            progressbar = null;
                            txt.focus();
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbOVBankwise').checked == true) {
                    if (document.getElementById('rdbSelectedOverseasBank').checked == true) {
                        var txt = document.getElementById('txtOVBank');
                        if (txt.value == '') {
                            alert('Enter Overseas Bank ID');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbCountrywise').checked == true) {
                    if (document.getElementById('rdbSelectedCountry').checked == true) {
                        var txt = document.getElementById('txtCountry');
                        if (txt.value == '') {
                            alert('Enter Currency');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
                if (document.getElementById('rdbConsignee').checked == true) {
                    if (document.getElementById('rdbSelectedConsignee').checked == true) {
                        var txt = document.getElementById('txtConsignee');
                        if (txt.value == '') {
                            alert('Enter Consignee Id');
                            txt.focus();
                            clearTimeout(progressbar);
                            progressbar = null;
                            return false;
                        }
                    }
                }
            }
        }

        function ClickAnotherButton() {
            document.getElementById('btndwnld').click();

        }
    </script>

    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: transparent;
            z-index: 99;
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid navy;
            width: 400px;
            height: 40px;
            display: none;
            position: absolute;
            background-color: white;
            z-index: 999;
        }
    </style>

</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div class="loading" align="center">
            Please wait while the report is generating..<br />
            <br />
            <img src="../../Images/ProgressBar1.gif" alt="" />
        </div>

        <div>
            <center>
                <uc1:Menu ID="Menu1" runat="server" />

                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btndwnld" />
                    </Triggers>
                    <ContentTemplate>

                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td align="left" style="width: 50%" valign="bottom">
                                                <span class="pageLabel"><strong>Export Bill Realisation</strong></span>
                                            </td>
                                            <td align="right" style="width: 50%" valign="bottom">
                                                <asp:Label ID="lblpath" runat="server" CssClass="elementLabel" ForeColor="Red" />
                                            </td>
                                        </tr>
                                    </table>
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
                                    <table cellspacing="0" border="0">
                                        <tr>
                                            <td height="40px" align="left" valign="middle" width="150px">
                                                <asp:RadioButton ID="rdbforeign" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Datafor" Text="Foreign"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="24" Checked="True" />
                                            </td>
                                            <td height="20px" valign="middle" width="150px">
                                                <asp:RadioButton ID="rdblocal" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Local" GroupName="Datafor"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="25" />
                                            </td>
                                            <td height="40px" valign="middle" width="170px">
                                                <asp:RadioButton ID="rdbboth" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Both" GroupName="Datafor"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="28" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                        <tr>
                                            <td width="10%" align="right" nowrap>
                                                <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                            </td>
                                            <td align="left" nowrap>&nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1"
                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" Width="100px" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 100px">
                                                <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                            </td>
                                            <td align="left" style="width: 700px">&nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
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

                                    <table cellspacing="0" border="0">
                                        <tr>
                                            <td height="40px" align="left" valign="middle" width="150px">
                                                <asp:RadioButton ID="rdbDocumnetwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data" OnCheckedChanged="rdbDocumnetwise_CheckedChanged" Text="Document wise"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="24" Checked="True" />
                                            </td>
                                            <td height="20px" valign="middle" width="150px">
                                                <asp:RadioButton ID="rdbCustomerwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Customer wise" GroupName="Data" OnCheckedChanged="rdbCustomerwise_CheckedChanged"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="25" />
                                            </td>
                                            <td height="40px" valign="middle" width="170px">
                                                <asp:RadioButton ID="rdbOVPartywise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Overseas Party wise" GroupName="Data" OnCheckedChanged="rdbOVPartywise_CheckedChanged"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="28" />
                                            </td>
                                            <td height="40px" valign="middle" width="170px">
                                                <asp:RadioButton ID="rdbOVBankwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Overseas Bank wise" GroupName="Data" OnCheckedChanged="rdbOVBankwise_CheckedChanged"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="31" />
                                            </td>
                                            <td height="40px" valign="middle" width="170px">
                                                <asp:RadioButton ID="rdbCountrywise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Currency wise" GroupName="Data" OnCheckedChanged="rdbCountrywise_CheckedChanged"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="34" />
                                            </td>
                                            <td height="40px" valign="middle" width="170px">
                                                <asp:RadioButton ID="rdbConsignee" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    Text="Consignee Wise" GroupName="Data" OnCheckedChanged="rdbConsignee_CheckedChanged"
                                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="31" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:RadioButton ID="rdbAllCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data2" OnCheckedChanged="rdbAllCustomer_CheckedChanged" TabIndex="26"
                                                    Text="All Customer" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                                <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data2" OnCheckedChanged="rdbSelectedCustomer_CheckedChanged" TabIndex="27"
                                                    Text="Selected Customer" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdbAllOverseasParty" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data1" OnCheckedChanged="rdbAllOverseasParty_CheckedChanged" TabIndex="29"
                                                    Text="All Overseas Party" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                                <asp:RadioButton ID="rdbSelectedOverseasParty" runat="server" AutoPostBack="true"
                                                    CssClass="elementLabel" GroupName="Data1" OnCheckedChanged="rdbSelectedOverseasParty_CheckedChanged"
                                                    TabIndex="30" Text="Selected Overseas Party" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdbAllOverseasBank" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data3" OnCheckedChanged="rdbAllOverseasBank_CheckedChanged" TabIndex="32"
                                                    Text="All Overseas Bank" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                                <asp:RadioButton ID="rdbSelectedOverseasBank" runat="server" AutoPostBack="true"
                                                    CssClass="elementLabel" GroupName="Data3" OnCheckedChanged="rdbSelectedOverseasBank_CheckedChanged"
                                                    TabIndex="33" Text="Selected Overseas Bank" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdbAllCountry" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data3" OnCheckedChanged="rdbAllCountry_CheckedChanged" TabIndex="35"
                                                    Text="All Currency" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                                <asp:RadioButton ID="rdbSelectedCountry" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data3" OnCheckedChanged="rdbSelectedCountry_CheckedChanged" TabIndex="36"
                                                    Text="Currency" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdbAllConsignee" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data3" OnCheckedChanged="rdbAllConsignee_CheckedChanged" TabIndex="26"
                                                    Text="All Consignee" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                                <asp:RadioButton ID="rdbSelectedConsignee" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                    GroupName="Data3" OnCheckedChanged="rdbSelectedConsignee_CheckedChanged" TabIndex="27"
                                                    Text="Selected Consignee" />
                                            </td>
                                        </tr>
                                    </table>
                                    <fieldset id="CustList" runat="server" style="width: 900px" visible="false">
                                        <legend><span class="elementLabel">Select Customer</span></legend>
                                        <table id="Table1" runat="server">
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                    <span style="color: Red">*</span><span class="elementLabel">Customer A/c No. :</span>&nbsp;
                                                </td>
                                                <td align="left" width="0px" style="font-weight: bold; color: #000000;">&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="textBox" MaxLength="40" TabIndex="37"
                                        Visible="false" Width="100px" OnTextChanged="txtCustomer_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    &nbsp;
                                    <asp:Button ID="BtnCustList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                    &nbsp;
                                    <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Visible="false"
                                        Width="400px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset id="OVPartylist" runat="server" style="width: 900px" visible="false">
                                        <legend><span class="elementLabel">Select Overseas Party Name</span></legend>
                                        <table id="Table2" runat="server">
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="150px">
                                                    <span style="color: Red">*</span><span class="elementLabel">Oveseas Party Id:</span>&nbsp;
                                                </td>
                                                <td align="left" width="0px" style="font-weight: bold; color: #000000;">&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtOVPID" runat="server" CssClass="textBox" MaxLength="40" TabIndex="38"
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
                                        <legend><span class="elementLabel">Select Overseas Bank Name</span></legend>
                                        <table id="Table3" runat="server">
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="150px">
                                                    <span style="color: Red">*</span><span class="elementLabel">Oveseas Bank Id:&nbsp;</span>
                                                </td>
                                                <td align="left" width="0px" style="font-weight: bold; color: #000000;">&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtOVBank" runat="server" CssClass="textBox" MaxLength="40" TabIndex="39"
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
                                        <legend><span class="elementLabel">Select Currency</span></legend>
                                        <table id="Table4" runat="server">
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                    <span style="color: Red">*</span><span class="elementLabel">Currency Id. :&nbsp;</span>
                                                </td>
                                                <td align="left" width="0px" style="font-weight: bold; color: #000000;">&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textBox" MaxLength="40" TabIndex="40"
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
                                    <fieldset id="consigneeparty" runat="server" style="width: 900px" visible="false">
                                        <legend><span class="elementLabel">Select Consignee</span></legend>
                                        <table id="Table5" runat="server">
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                    <span style="color: Red">*</span><span class="elementLabel">Consignee :</span>&nbsp;
                                                </td>
                                                <td align="left" width="0px" style="font-weight: bold; color: #000000;">&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtConsignee" runat="server" CssClass="textBox" MaxLength="40" TabIndex="37"
                                        Visible="false" Width="100px" OnTextChanged="txtConsignee_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    &nbsp;
                                    <asp:Button ID="btnConsigneeList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                    &nbsp;
                                    <asp:Label ID="lblConsigneePartyName" runat="server" CssClass="elementLabel" Visible="false"
                                        Width="400px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>

                                    <table>
                                        <tr valign="bottom">
                                            <td align="right" style="width: 120px"></td>
                                            <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">&nbsp;
                                <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate Report" OnClick="btnSave_Click"
                                    ToolTip="Generate Report" TabIndex="41" />
                                                <asp:Button ID="btndwnld" runat="server" CssClass="buttonDefault" Text="Download"
                                                    OnClick="btnexcel_Click" ToolTip="Generate Report" TabIndex="41" Style="visibility: hidden" />
                                            </td>
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <td>
                                                <asp:Label ID="lblmsg" runat="server" CssClass="mandatoryField"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>
        </div>
    </form>
</body>
</html>
