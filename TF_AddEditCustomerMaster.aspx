<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditCustomerMaster.aspx.cs"
    Inherits="TF_AddEditCustomerMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRDFIN System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="Style/SnackBar.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function Toggel_AC_Code() {
            var _txtGL_Code = document.getElementById('txtGL_Code').value;
            document.getElementById('txtCustcode').value = _txtGL_Code;
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 16 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function Countryhelp() {
            popup = window.open('TF_CountryLookup.aspx', 'helpCountryId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCountryId"
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
            if (common == "helpCountryId") {
                document.getElementById('txtcountry').value = s;
            }
        }

        function toUpper_Case() {
            document.getElementById('txtHF').value = (document.getElementById('txtHF').value).toUpperCase();

        }

        function validateSave() {
            var HF = document.getElementById("txtHF");
            var HF1 = document.getElementById("txtHF").value.substring(0, 1);
            var accno = document.getElementById('txtACNo');
            var ddlStatusHolder = document.getElementById('ddlStatusHolder');
            var custName = document.getElementById('txtCustName');
            var txtIEcode = document.getElementById('txtIEcode');

            if (HF1 == 'H' || HF1 == 'F' || HF1 == 'B') {
            }
            else {
                alert('Enter First Letter has to be H or F or B');
                HF.focus();
                HF.value = '';
                return false;
            }

            var txtaccounttype = document.getElementById("txtHF").value.substring(1, 3);
            if (txtaccounttype == '10' || txtaccounttype == '15' || txtaccounttype == '30' || txtaccounttype == '60' || txtaccounttype == '79' || txtaccounttype == '99' || txtaccounttype == '90') { }
            else {
                alert('Enter Valid Account Type');
                HF.focus();
                HF.value = '';
                return false;
            }

            var lenacc = (accno.value).length;
            if (lenacc < 6) {
                alert('Enter Vaild Customer A/c No.');
                accno.focus();
                return false;
            }

            if (trimAll(custName.value) == '') {
                try {
                    alert('Enter Customer Name.');
                    custName.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Customer Name.');
                    return false;
                }
            }

            if (document.getElementById('Chk_IMP_Auto').checked) {
                if (document.getElementById('txtCust_Abbr').value == '') {
                    alert("Cust abrr can not be blank for Import Automation Customer.");
                    document.getElementById('txtCust_Abbr').focus();
                    return false;
                }
                if (document.getElementById('txtGL_Code').value == "") {
                    alert("Enter GL Code.");
                    return false;
                }
                if (document.getElementById('txtCustcode').value == "") {
                    alert("Enter AC Code.");
                    return false;
                }
            }
            else {

                if (txtIEcode.value == "") {
                    alert("Enter IE Code.");
                    return false;
                }
                else if (txtIEcode.value.length != 10) {
                    alert("IE code should be 10 digits");
                    return false;
                }
                if (ddlStatusHolder.value == "") {
                    alert("Select Export Agency");
                    ddlStatusHolder.focus();
                    return false;
                }

            }
            if (document.getElementById('Chk_Imp_WH').checked) {

                if (document.getElementById('txtCust_Abbr').value == '') {
                    alert("Cust abrr can not be blank for Digital Import Payment Customer.");
                    document.getElementById('txtCust_Abbr').focus();
                    return false;
                }

            }
            if (document.getElementById('Chk_Imp_WH').checked && document.getElementById('Chk_IMP_Auto').checked) {
                if (confirm('This Customer will be added as Digital Import Payment & Import Automation customer. ')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (document.getElementById('Chk_Imp_WH').checked) {
                    if (confirm('This Customer will be added as Digital Import Payment. ')) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    if (document.getElementById('Chk_IMP_Auto').checked) {
                        if (confirm('This Customer will be added as Import Automation customer. ')) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        if (confirm('This Customer will be added as Export customer. ')) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        function IMP_AUTO_Cust_CheckClick() {
            if (document.getElementById('Chk_Imp_WH').checked || document.getElementById('Chk_IMP_Auto').checked) {
                document.getElementById('spnCustAbrr').style.visibility = "visible";
                document.getElementById('spnCustAcCode').style.visibility = "visible";
            }
            else {
                document.getElementById('spnCustAbrr').style.visibility = "hidden";
                document.getElementById('spnCustAcCode').style.visibility = "hidden";
            }
        }

        function datecheck() {
            var Date1 = document.getElementById("ddlDate1");
            var Date2 = document.getElementById("ddlDate2");
            var Date3 = document.getElementById("ddlDate3");
            var Date4 = document.getElementById("ddlDate4");
            var Date5 = document.getElementById("ddlDate5");
            if (document.getElementById('Chk_Imp_WH').checked) {
                document.getElementById('spnCustAbrr').style.visibility = "visible";
                document.getElementById('IMP_WH_date').style.display = "block";
            }
            else {
                Date1.selectedIndex = 0;
                Date2.selectedIndex = 0;
                Date3.selectedIndex = 0;
                Date4.selectedIndex = 0;
                Date5.selectedIndex = 0;
                document.getElementById('spnCustAbrr').style.visibility = "hidden";
                document.getElementById('IMP_WH_date').style.display = "none";
            }
        }



        function Emailvalidate() {

            var emailPat = /^(\".*\"|[A-Za-z]\w*)@(\[\d{1,3}(\.\d{1,3}){3}]|[A-Za-z]\w*(\.[A-Za-z]\w*)+)$/;
            var emailid = document.getElementById('txtEmailID').value;
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert('Your email address incorrect Please try again.', '#txtEmailID');
                //document.getElementById("<%=txtEmailID.ClientID %>",'#<%=txtEmailID.ClientID %>');
                return false;
            }
        }

        function checkEmail() {

            var email = document.getElementById('txtEmailID');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (email.value != "") {

                if (!filter.test(email.value)) {
                    alert('Please provide a valid email address');
                    email.focus();

                    return false;
                }
            }
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 16 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function iecodecheck() {
            var iecode = document.getElementById('txtIEcode');

            if (iecode.value.length < 10) {
                alert('IE code should be 10 digits');
                //               iecode.focus();
                return false;
            }
        }
        function Alert(Result) {
            MyAlert(Result);
        }

        function PanCardNo(key) {
            var charCode = (key.which) ? key.which : key.keyCode;
            var p = document.getElementById('txtPanNo');


            if (p.value.length < 5) {
                if ((charCode > 64 && charCode < 91) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36) {
                    return true;
                }
                else {
                    return false;
                }
            } else if (p.value.length >= 5 && p.value.length <= 8) {
                if ((charCode > 47 && charCode < 58) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36 || (charCode > 95 && charCode < 106)) {
                    return true;
                } else {
                    return false;
                }

            }
            else if (p.value.length > 8) {
                if ((charCode > 64 && charCode < 91) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }



        function PanCardNo(key) {
            var charCode = (key.which) ? key.which : key.keyCode;
            var p = document.getElementById('txtPanNo');


            if (p.value.length < 3) {
                if ((charCode > 64 && charCode < 91) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36) {
                    return true;
                }
                else {
                    return false;
                }
            }
            if (p.value.length >= 3 && p.value.length <= 3) {
                if ((charCode > 47 && charCode < 58) || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 39 || charCode == 37 || charCode == 35 || charCode == 36 || (charCode > 95 && charCode < 106)) {
                    return true;
                } else {
                    return false;
                }

            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" atomicselection="false">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div id="snackbar">
            <div id="snackbarbody" style="padding-top: -500px;">
            </div>
        </div>
        <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Customer Master Details</strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="22" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                            <%-- Audit Trail--%>
                            <input type="hidden" id="hdnCustname" runat="server" />
                            <input type="hidden" id="hdnIE_Code" runat="server" />
                            <input type="hidden" id="hdnGLCode" runat="server" />
                            <input type="hidden" id="hdnCustCountry" runat="server" />
                            <input type="hidden" id="hdnAddress" runat="server" />
                            <input type="hidden" id="hdnexperagency" runat="server" />
                            <input type="hidden" id="hdnAbbr" runat="server" />
                            <input type="hidden" id="hdnCity" runat="server" />
                            <input type="hidden" id="hdnPincode" runat="server" />
                            <input type="hidden" id="hdntelphoneno" runat="server" />
                            <input type="hidden" id="hdnemailid" runat="server" />
                            <input type="hidden" id="hdndate1" runat="server" />
                            <input type="hidden" id="hdndate2" runat="server" />
                            <input type="hidden" id="hdndate3" runat="server" />
                            <input type="hidden" id="hdndate4" runat="server" />
                            <input type="hidden" id="hdndate5" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                <table>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField"></span><span class="elementLabel">Branch&nbsp;</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtBranch" runat="server" CssClass="textBox" MaxLength="20" Width="80px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Customer A/C No&nbsp;</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtHF" runat="server" CssClass="textBox" MaxLength="3" Width="30px"
                                                TabIndex="1" Style="text-align: center"></asp:TextBox>
                                            <asp:TextBox ID="txtbranchcode" runat="server" CssClass="textBox" MaxLength="3" Width="30px"
                                                ReadOnly="True" Style="text-align: center"></asp:TextBox>
                                            <asp:TextBox ID="txtACNo" runat="server" CssClass="textBox" MaxLength="6" Width="50px"
                                                TabIndex="2" AutoPostBack="true" OnTextChanged="txtACNo_TextChanged" Style="text-align: center"></asp:TextBox>
                                            <span class="mandatoryField"></span><span class="elementLabel">[First Letter has
                                            to be H or F. Example-H10 792 102731]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="white-space: nowrap; text-align: left">
                                            <asp:CheckBox runat="server" ID="Chk_Imp_WH" Text="Digital Import Payment" CssClass="elementLabel"
                                                onclick="return datecheck();"></asp:CheckBox>
                                            &nbsp;&nbsp;
                                            <asp:CheckBox runat="server" ID="Chk_IMP_Auto" Text="Import Automation Customer"
                                                CssClass="elementLabel"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Name&nbsp;</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtCustName" runat="server" CssClass="textBox" MaxLength="50" Width="300"
                                                TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField"></span><span class="elementLabel">GL Code&nbsp;</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtGL_Code" runat="server" CssClass="textBox" MaxLength="5" Width="80"
                                                TabIndex="5" onblur="return Toggel_AC_Code();"></asp:TextBox>
                                            &nbsp;&nbsp;<span id="spnCustAbrr" class="mandatoryField" style="visibility: hidden;">*</span><span
                                                class="elementLabel">Cust Abrrevation&nbsp;</span>
                                            <asp:TextBox ID="txtCust_Abbr" runat="server" CssClass="textBox" MaxLength="12" Width="250"
                                                TabIndex="6" OnTextChanged="txtCust_Abbr_TextChanged"></asp:TextBox>
                                            &nbsp; <span id="spnCustAcCode" class="mandatoryField" style="visibility: hidden;">*</span><span
                                                class="elementLabel">A/C Code&nbsp;</span>
                                            <asp:TextBox ID="txtCustcode" runat="server" CssClass="textBox" MaxLength="5" Width="70"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">I/E Code&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtIEcode" runat="server" CssClass="textBox" MaxLength="10" Width="120px"
                                                TabIndex="7"></asp:TextBox>
                                            <span class="elementLabel">&nbsp;&nbsp; Export Agency&nbsp;</span>
                                            <asp:DropDownList ID="ddlStatusHolder" CssClass="dropdownList" TabIndex="8" runat="server">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Customs" Value="Customs"></asp:ListItem>
                                                <asp:ListItem Text="SEZ" Value="SEZ"></asp:ListItem>
                                                <asp:ListItem Text="STPI" Value="STPI"></asp:ListItem>
                                                <asp:ListItem Text="Status holder exporters" Value="Status holder exporters"></asp:ListItem>
                                                <asp:ListItem Text="100% EOU" Value="100% EOU"></asp:ListItem>
                                                <asp:ListItem Text="Warehouse export" Value="Warehouse export"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr height="70px">
                                        <td align="right" valign="top">
                                            <span class="elementLabel">Address&nbsp;</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtaddress" runat="server" CssClass="textBox" MaxLength="100" Width="300px"
                                                TextMode="MultiLine" Height="65px" TabIndex="9"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtaddress"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"
                                                    ErrorMessage="Special characters are not allowed" ID="rfvname"
                                                    ValidationExpression="^[\sa-zA-Z0-9/?:().,'+-]*$">
                                                    <%--"[^0-9a-zA-Z /?:().,'+-]">--%>
                                                </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">City&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtcity" runat="server" CssClass="textBox" MaxLength="20" Width="150px"
                                                TabIndex="10"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Pincode&nbsp;</span>
                                            <asp:TextBox ID="txtpincode" runat="server" CssClass="textBox" MaxLength="7" Width="70px"
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Country&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtcountry" runat="server" CssClass="textBox" MaxLength="3" Width="30px"
                                                TabIndex="12" AutoPostBack="True" OnTextChanged="txtcountry_TextChanged" ToolTip="Press F2 For Help"></asp:TextBox>
                                            <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" Width="16px" />
                                            <asp:Label ID="lblCountryName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Telephone No.&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" TabIndex="13"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Fax No.&nbsp;</span>
                                            <asp:TextBox ID="txtFaxNo" runat="server" CssClass="textBox" MaxLength="20" Width="150px"
                                                TabIndex="14"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">To Email ID </span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="textBox" MaxLength="70" Width="250px"
                                                TabIndex="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Email Type </span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:RadioButton ID="rbtToEmail" runat="server" GroupName="EmailType" Text="To" CssClass="elementLabel" />
                                            <asp:RadioButton ID="rbtCCEmail" runat="server" GroupName="EmailType" Text="CC" CssClass="elementLabel" />
                                            <asp:RadioButton ID="rbtBCCEmail" runat="server" GroupName="EmailType" Text="BCC"
                                                CssClass="elementLabel" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Email ID </span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtEmailIdToCCBCC" runat="server" TextMode="MultiLine" CssClass="textBox"
                                                MaxLength="400" Width="1000px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Email ID For Import </span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtEmailIdExportToCCBCC" runat="server" TextMode="MultiLine" CssClass="textBox"
                                                MaxLength="400" Width="1000px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Contact Person&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textBox" MaxLength="40"
                                                Width="250px" TabIndex="16"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Mobile No. &nbsp;</span>
                                            <asp:TextBox ID="txtCPMobileNo" runat="server" CssClass="textBox" MaxLength="12"
                                                Width="120px" TabIndex="18"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">PAN No.&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtPanNo" runat="server" CssClass="textBox" MaxLength="10" Width="80px"
                                                TabIndex="19"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">TAN No.&nbsp;</span>
                                            <asp:TextBox ID="txtTanNo" runat="server" CssClass="textBox" MaxLength="10" Width="80px"
                                                TabIndex="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">External Rating&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtExtRating" runat="server" CssClass="textBox" MaxLength="4" Width="40px"
                                                TabIndex="21"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <div id="IMP_WH_date" style="display: none;">
                                                <table width="100%">
                                                    <tr>
                                                        <%--   <td align="right" style="width: 120px">
                                    </td>--%>
                                                        <td align="left" colspan="2" valign="bottom">
                                                            <span class="elementLabel">
                                                                <h3>
                                                                    Auto Email Schedular</h3>
                                                            </span>
                                                            <td />
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 120px">
                                                            <span class="elementLabel">&nbsp;&nbsp; Date 1&nbsp;</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap">
                                                            <asp:DropDownList ID="ddlDate1" CssClass="dropdownList" TabIndex="8" runat="server">
                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 120px">
                                                            <span class="elementLabel">&nbsp;&nbsp; Date 2&nbsp;</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap">
                                                            <asp:DropDownList ID="ddlDate2" CssClass="dropdownList" TabIndex="8" runat="server">
                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 120px">
                                                            <span class="elementLabel">&nbsp;&nbsp; Date 3&nbsp;</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap">
                                                            <asp:DropDownList ID="ddlDate3" CssClass="dropdownList" TabIndex="8" runat="server">
                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 120px">
                                                            <span class="elementLabel">&nbsp;&nbsp; Date 4&nbsp;</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap">
                                                            <asp:DropDownList ID="ddlDate4" CssClass="dropdownList" TabIndex="8" runat="server">
                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 120px">
                                                            <span class="elementLabel">&nbsp;&nbsp; Date 5&nbsp;</span>
                                                        </td>
                                                        <td align="left" style="white-space: nowrap">
                                                            <asp:DropDownList ID="ddlDate5" CssClass="dropdownList" TabIndex="8" runat="server">
                                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120px">
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                OnClick="btnSave_Click" TabIndex="22" />&nbsp
                                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="BtnCancel_Click" TabIndex="23" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                </table>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
