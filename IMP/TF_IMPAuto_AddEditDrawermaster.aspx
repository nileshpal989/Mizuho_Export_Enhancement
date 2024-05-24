<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPAuto_AddEditDrawermaster.aspx.cs"
    Inherits="IMP_TF_IMPAuto_AddEditDrawermaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRDFIN System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function OpenCustomerCodeList(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var ddlBranch = document.getElementById('ddlBranch');
                open_popup('HelpForms/TF_IMP_CustomerHelp.aspx?BranchName=' + ddlBranch.value, 400, 400, 'CustomerCodeList');
                return false;
            }
        }
        function selectCustomer(selectedID) {
            var txtCustomer_ID = document.getElementById('txtCustomer_ID');
            txtCustomer_ID.value = selectedID;
            __doPostBack('txtCustomer_ID', '');
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

        function toUpper_Case() {
            document.getElementById('txtHF').value = (document.getElementById('txtHF').value).toUpperCase();

        }

        function validateSave() {
            var custacno = document.getElementById('txtCustomer_ID');
            var DrawerID = document.getElementById('txtCustid');
            var DrawerName = document.getElementById('txtCustName');

            if (custacno.value == '') {
                //                alert('Enter Cust A/C No.');
                //                custacno.focus();
                VAlert('Enter Cust A/C No.', '#txtCustomer_ID');
                return false;
            }

            if (DrawerName.value == '') {
                //                alert('Enter Cust A/C No.');
                //                custacno.focus();
                VAlert('Enter Drawer Name.', '#txtCustName');
                return false;
            }
            return true;
        }
        
        
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" atomicselection="false">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <%--  alert message--%>
        <div id="dialog" class="AlertJqueryHide">
            <p id="Paragraph">
            </p>
        </div>
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="/Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Drawer Master Details</strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="22" OnClick="btnBack_Click" />
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
                                        <td align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField"></span><span class="elementLabel">Cust A/C No : </span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCustomer_ID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                MaxLength="20" OnTextChanged="txtCustomer_ID_TextChanged" TabIndex="4" Width="90px"></asp:TextBox>
                                            <asp:Button ID="btnCustomerList" runat="server" ToolTip="Press for Customers list."
                                                CssClass="btnHelp_enabled" TabIndex="4" />
                                            <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField"></span><span class="elementLabel">Drawer ID : </span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtCustid" runat="server" CssClass="textBox" MaxLength="20" Width="80px"
                                                Enabled="false"></asp:TextBox>
                                            &nbsp;<span class="elementLabel"></span> &nbsp; <span class="mandatoryField">Drawer
                                                ID Auto Genrated </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Drawer Name :</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtCustName" runat="server" CssClass="textBox" MaxLength="40" Width="300"
                                                TabIndex="4" OnTextChanged="txtCustName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr height="70px">
                                        <td align="right" valign="top">
                                            <span class="elementLabel">Drawer Address :</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtaddress" runat="server" CssClass="textBox" MaxLength="100" Width="300px"
                                                TextMode="MultiLine" Height="65px" TabIndex="9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Country :</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtcountry" runat="server" CssClass="textBox" MaxLength="3" Width="30px"
                                                TabIndex="12" AutoPostBack="True" OnTextChanged="txtcountry_TextChanged" ToolTip="Press F2 For Help"></asp:TextBox>
                                            <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" Width="16px" />
                                            <asp:Label ID="lblCountryName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="right">
                                           </span> <span class="elementLabel">LEI No :</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txt_LEINo" runat="server" CssClass="textBox" MaxLength="40" Width="300"
                                                TabIndex="12"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    <tr>
                                    <td align="right">
                                           </span> <span class="elementLabel">LEI Expiry Date:</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txt_LEIExpiryDate" runat="server" CssClass="textBox" MaxLength="12"
                                                Width="70px" TabIndex="12"></asp:TextBox>&nbsp;
                                                 <ajaxToolkit:MaskedEditExtender ID="mdLEIExpiryDate" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                MaskType="Date" TargetControlID="txt_LEIExpiryDate">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_LEIExpiryDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:CalendarExtender ID="calendarLEIExpiryDate" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" PopupButtonID="btncalendar_LEIExpiryDate" TargetControlID="txt_LEIExpiryDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdLEIExpiryDate"
                                                ControlToValidate="txt_LEIExpiryDate" EmptyValueBlurredText="*" EmptyValueMessage="Enter Date Value"
                                                ErrorMessage="Invalid" InvalidValueBlurredMessage="Date is invalid" ValidationGroup="dtVal"></ajaxToolkit:MaskedEditValidator>
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
