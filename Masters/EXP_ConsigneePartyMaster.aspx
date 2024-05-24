<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_ConsigneePartyMaster.aspx.cs" Inherits="Masters_EXP_ConsigneePartyMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRDFIN System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function toLower_Case() {
            document.getElementById('txtEmailId').value = (document.getElementById('txtEmailId').value).toLowerCase();
            document.getElementById('txtCPEMailId').value = (document.getElementById('txtCPEMailId').value).toLowerCase();

        }

        function Countryhelp() {
            popup = window.open('../TF_CountryLookup.aspx', 'helpCountryId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
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

        function validate_Number(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 16 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function checkEmail(txtEmailId) {

            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (txtEmailId.value.trim() == "") {
                return false;
            }
            if (!filter.test(txtEmailId.value)) {
                alert('Please provide a valid email address');
                setTimeout(function () {
                    txtEmailId.focus();
                }, 0);
                return false;
            }
        }

        function checkCPEmail(txtCPEMailId) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (txtCPEMailId.value.trim() == "") {
                return false;
            }
            if (!filter.test(txtCPEMailId.value)) {
                alert('Please provide a valid email address');
                setTimeout(function () {
                    txtCPEMailId.focus();
                }, 0);
                return false;
            }
        }

        function validateSave() {
            var PartyId = document.getElementById('txtPartyId');
            if (PartyId.value == '') {
                try {
                    alert('Enter Party ID.');
                    PartyId.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Party ID.');
                    return false;
                }
            }

            var PartyName = document.getElementById('txtPartyName');
            if (PartyName.value == '') {
                try {
                    alert('Enter Party Name.');
                    PartyName.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Party Name.');
                    return false;
                }
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:ScriptManager ID="ScriptManagerMain" runat="server">
            </asp:ScriptManager>
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
                        <input type="hidden" runat="server" id="hdnCode" />
                        <input type="hidden" runat="server" id="hdnName" />
                        <input type="hidden" runat="server" id="hdnAddress" />
                        <input type="hidden" runat="server" id="hdnCity" />
                        <input type="hidden" runat="server" id="hdnpincode" />
                        <input type="hidden" runat="server" id="hdncountry" />
                        <input type="hidden" runat="server" id="hdntelephone" />
                        <input type="hidden" runat="server" id="hdnfaxno" />
                        <input type="hidden" runat="server" id="hdnemail" />
                        <input type="hidden" runat="server" id="hdncontactperson" />
                        <input type="hidden" runat="server" id="hdnPartyCPEmail" />
                        <input type="hidden" runat="server" id="hdnmobile" />
                        <input type="hidden" runat="server" id="hdnCPacntNo" />
                        <input type="hidden" runat="server" id="hdnPartyType" />
                        <input type="hidden" runat="server" id="hdnPartyLEINo" />
                        <input type="hidden" runat="server" id="hdnPartyLEIExpiryDate" />

                        <tr>
                            <td>
                                <input type="hidden" id="hdnIGGrade" runat="server" />
                                <input type="hidden" id="hdnPCFCCatg" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Consignee Party Master Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    TabIndex="17" OnClick="btnBack_Click" />
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
                                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                    <table>
                                        <tr>
                                            <td align="right" width="120px">
                                                <span class="mandatoryField">*</span> <span class="elementLabel">Party ID :</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPartyId" runat="server" CssClass="textBox" MaxLength="7" Width="70"
                                                    TabIndex="1"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td align="right" width="120px">
                                                <span class="mandatoryField">*</span> <span class="elementLabel">Name :</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPartyName" runat="server" CssClass="textBox" MaxLength="100" Width="300"
                                                    TabIndex="2"></asp:TextBox>
                                            </td>

                                        </tr>

                                        <tr height="70px">
                                            <td align="right" valign="top">
                                                <span class="elementLabel">Address :</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtaddress" runat="server" CssClass="textBox" MaxLength="100" Width="300px"
                                                    TextMode="MultiLine" Height="65px" TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td align="right" style="width: 120px">
                                                <span class="elementLabel">City :</span>
                                            </td>
                                            <td align="left" style="width: 100px">
                                                <asp:TextBox ID="txtcity" runat="server" CssClass="textBox" MaxLength="20" Width="150px"
                                                    TabIndex="5"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 70px">
                                                <span class="elementLabel">Pincode : </span>
                                            </td>
                                            <td align="left" style="width: 290px">
                                                <asp:TextBox ID="txtpincode" runat="server" CssClass="textBox" MaxLength="7" Width="70px"
                                                    TabIndex="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td align="right" style="width: 120px">
                                                <span class="elementLabel">Country :</span>
                                            </td>
                                            <td align="left" style="width: 400px">
                                                <asp:TextBox ID="txtcountry" runat="server" CssClass="textBox" MaxLength="2" Width="50px"
                                                    TabIndex="7" Enabled="false" AutoPostBack="True" OnTextChanged="txtcountry_TextChanged"></asp:TextBox>
                                                <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" Width="16px" />
                                                <asp:Label ID="lblCountryName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td align="right" style="width: 120px">
                                                <span class="elementLabel">Telephone No. :</span>
                                            </td>
                                            <td align="left" style="width: 100px">
                                                <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="textBox" MaxLength="20"
                                                    Width="150px" TabIndex="8"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 70px">
                                                <span class="elementLabel">Fax No. :</span>
                                            </td>
                                            <td align="left" style="width: 200px">
                                                <asp:TextBox ID="txtFaxNo" runat="server" CssClass="textBox" MaxLength="20" Width="150px"
                                                    TabIndex="9"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td align="right" style="width: 120px">
                                                <span class="elementLabel">Email ID :</span>
                                            </td>
                                            <td align="left" style="width: 290px">
                                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="textBox" MaxLength="50" Width="350px"
                                                    TabIndex="10"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td align="right" style="width: 120px">
                                                <span class="elementLabel">Contact Person :</span>
                                            </td>
                                            <td align="left" style="width: 200px">
                                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textBox" MaxLength="40"
                                                    Width="250px" TabIndex="11"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 60px">
                                                <span class="elementLabel">Email ID :</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCPEMailId" runat="server" CssClass="textBox" MaxLength="50" Width="250px"
                                                    TabIndex="12"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 70px">
                                                <span class="elementLabel">Mobile No. :</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCPMobileNo" runat="server" CssClass="textBox" MaxLength="20"
                                                    Width="120px" TabIndex="13"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>

                                            <td align="right" style="width: 120px">
                                                <span class="elementLabel">A/C No :</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtACNo" runat="server" CssClass="textBox" MaxLength="30" Width="250px"
                                                    TabIndex="14"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 120px">
                                                <span class="elementLabel">LEI No :</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLEINo" runat="server" CssClass="textBox" MaxLength="20" Width="250px"
                                                    TabIndex="14"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 100px" nowrop>
                                                <span class="elementLabel">LEI Expiry Date :</span>
                                            </td>
                                            <td align="left" style="width: 400px">
                                                <asp:TextBox ID="txtLEIExpiry" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                    Width="70px" TabIndex="1"></asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                    runat="server" TargetControlID="txtLEIExpiry" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                    CultureTimePlaceholder=":" Enabled="True">
                                                </ajaxToolkit:MaskedEditExtender>
                                                <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled" />
                                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtLEIExpiry" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                    ValidationGroup="dtVal" ControlToValidate="txtLEIExpiry" EmptyValueMessage="Enter Date Value"
                                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="MaskedEditValidator3"></ajaxToolkit:MaskedEditValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td width="120px"></td>
                                            <td align="left" style="width: 220px">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                    OnClick="btnSave_Click" TabIndex="15" />&nbsp
                                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="BtnCancel_Click" TabIndex="16" />
                                                <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                                <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            </td>
                                        </tr>
                                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
