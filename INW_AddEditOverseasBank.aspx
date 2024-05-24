<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INW_AddEditOverseasBank.aspx.cs"
    Inherits="INW_AddEditOverseasBank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function toggel_IMP_Spans() {
            if (document.getElementById('Chk_IMP_Auto').checked == true) {
                document.getElementById('Span_Swift').innerHTML = '*';
                document.getElementById('Span_IFSC').innerHTML = '*';
                document.getElementById('Span_Country').innerHTML = '*';
            }
            else {
                document.getElementById('Span_Swift').innerHTML = '';
                document.getElementById('Span_IFSC').innerHTML = '';
                document.getElementById('Span_Country').innerHTML = '';
            }
         }

        function Uppercase(id) {
            var id = document.getElementById(id);
            id.value = id.value.toUpperCase();
        }
        function validateSave() {
            var txtBankID = document.getElementById('txtBankID');
            var txtBankName = document.getElementById('txtBankName');
            var txtSwiftCode = document.getElementById('txtSwiftCode');
            var txtIFSC_Code = document.getElementById('txtIFSC_Code');
            var txtCountry = document.getElementById('txtCountry');

            if (txtBankID.value == '') {
                alert('Enter Bank ID.');
                txtBankID.focus();
                return false;
            }
            
            if (txtBankName.value == '') {
                alert('Enter Bank Name.');
                txtBankName.focus();
                return false;
            }

            if (document.getElementById('Chk_IMP_Auto').checked == true) {
                
                if (txtCountry.value == '') {
                    alert('Enter Country Code.');
                    txtCountry.focus();
                    return false;
                }

//                if (txtSwiftCode.value == '') {
//                    alert('Enter Swift Code.');
//                    txtSwiftCode.focus();
//                    return false;
//                }
//                if (txtIFSC_Code.value == '') {
//                    alert('Enter IFSC Code.');
//                    txtIFSC_Code.focus();
//                    return false;
//                }
                //                else 
//                if (length(txtIFSC_Code.value) != 11) {
//                    alert('IFSC Code Should be 11 Character.');
//                    txtIFSC_Code.focus();
//                    return false;
//                }
            }

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
                document.getElementById('txtCountry').value = s;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Bank Master Details</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" TabIndex="20" />
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
                                <table cellspacing="0" cellpadding="0" border="0" width="700px" style="line-height: 150%">
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bank ID :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtBankID" runat="server" CssClass="textBox" MaxLength="7"
                                                Width="60" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Name :</span>
                                        </td>
                                        <td align="left" style="width: 390px">
                                            &nbsp;<asp:TextBox ID="txtBankName" runat="server" CssClass="textBox" MaxLength="100"
                                                Width="250" TabIndex="2" onkeyup="return Uppercase('txtBankName');"></asp:TextBox>
                                        </td>
                                       <td style="white-space: nowrap; text-align: left">
                                            <asp:CheckBox runat="server" ID="Chk_IMP_Auto" Text="Import Overseas Bank"
                                                CssClass="elementLabel" onchange="return toggel_IMP_Spans();"></asp:CheckBox>
                                       </td>
                                    </tr>
                                    <tr height="70px">
                                        <td align="right" valign="top" style="width: 110px">
                                            <span class="elementLabel">Address :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtAddress" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="250px" TextMode="MultiLine" Height="65px" TabIndex="3" onkeyup="return Uppercase('txtAddress');"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="450px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 212px">
                                            <span class="elementLabel">City :</span>
                                        </td>
                                        <td align="left" style="width: 150px">
                                            &nbsp;<asp:TextBox ID="txtCity" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="70px" TabIndex="4" onkeyup="return Uppercase('txtCity');"></asp:TextBox>
                                        </td>
                                        <td align="right" style="width: 70px">
                                            <span class="elementLabel">Pincode :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtPincode" runat="server" CssClass="textBox" MaxLength="6"
                                                Width="70px" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span id="Span_Country" class="mandatoryField"></span><span class="elementLabel">Country. :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtCountry" runat="server" CssClass="textBox" MaxLength="3" onkeyup="return Uppercase('txtCountry')" 
                                                Width="30px" TabIndex="6" AutoPostBack="true" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" Width="16px" />
                                            <asp:Label ID="lblCountryName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="450px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 127px">
                                            <span class="elementLabel">Telephone No. :</span>
                                        </td>
                                        <td align="left" style="width: 115px">
                                            &nbsp;<asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="80px" TabIndex="7"></asp:TextBox>
                                        </td>
                                        <td align="right" style="width: 70px">
                                            <span class="elementLabel">Fax No. :</span>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtFaxNo" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="80px" TabIndex="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                    <tr>
                                        <td align="right">
                                            <span id="Span_Swift" class="mandatoryField"></span><span class="elementLabel">Swift Code :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtSwiftCode" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" TabIndex="9" onkeyup="return Uppercase('txtSwiftCode');"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span id="Span_IFSC" class="mandatoryField"></span><span class="elementLabel">IFSC Code :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtIFSC_Code" runat="server" CssClass="textBox" MaxLength="11"
                                                Width="90px" TabIndex="9" onkeyup="return Uppercase('txtIFSC_Code');"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="elementLabel">E-Mail Id :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtEmailId" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="200px" TabIndex="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="elementLabel">Contact Person :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtContactPerson" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="200px" TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Vostro A/c No. :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtVostroACNO" runat="server" CssClass="textBox" MaxLength="25"
                                                Width="200px" TabIndex="12"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">BM+ A/c No. :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtBMplusAcNo" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" TabIndex="13"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Sort Code :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtSortCode" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" TabIndex="14"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Chip UID :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtChipUID" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" TabIndex="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Chips ABA No :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtChipsABAno" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" TabIndex="16"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">ABA No :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtABAno" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" TabIndex="17"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" OnClick="btnSave_Click" TabIndex="18" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" OnClick="btnCancel_Click" TabIndex="19" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
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
