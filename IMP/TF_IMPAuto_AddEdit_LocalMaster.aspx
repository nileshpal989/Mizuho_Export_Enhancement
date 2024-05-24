<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPAuto_AddEdit_LocalMaster.aspx.cs"
    Inherits="IMP_TF_IMPAuto_AddEdit_LocalMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <%--snackbar--%>
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="~/Scripts/Validations.js"></script>
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var txtBankID = document.getElementById('txtBankID');
            if (txtBankID.value == '') {
                VAlert('Enter Bank ID.', '#txtBankID');
                txtBankID.focus();
                return false;
            }
            var txtBankName = document.getElementById('txtBankName');
            if (txtBankName.value == '') {
                VAlert('Enter Bank Name.', '#txtBankName');
                txtBankName.focus();
                return false;
            }
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
                document.getElementById('txtCountry').value = s;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
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
                                <input type="hidden" runat="server" id="hdnbankid" />
                                <input type="hidden" runat="server" id="hdnbankname" />
                                <input type="hidden" runat="server" id="hdnaddress" />
                                <input type="hidden" runat="server" id="hdncity" />
                                <input type="hidden" runat="server" id="hdnpincode" />
                                <input type="hidden" runat="server" id="hdnState" />
                                <input type="hidden" runat="server" id="hdncountry" />
                                <input type="hidden" runat="server" id="hdntelino" />
                                <input type="hidden" runat="server" id="hdnfaxno" />
                                <input type="hidden" runat="server" id="hdnemail" />
                                <input type="hidden" runat="server" id="hdncontactp" />
                                <span class="pageLabel"><strong>Local Bank Master Details</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" TabIndex="20" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="500px" style="line-height: 150%">
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bank IFSC Code :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtBankID" runat="server" CssClass="textBox" MaxLength="11"
                                                Width="100" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Name :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtBankName" runat="server" CssClass="textBox" MaxLength="100"
                                                Width="250" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Address :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtAddress" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="250px" TextMode="MultiLine" Height="65px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">City :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCity" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="120px" TabIndex="4"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <span class="elementLabel">Pincode :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtPincode" runat="server" CssClass="textBox" MaxLength="6"
                                                Width="70px" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">State :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtState" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="120px" TabIndex="6"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <span class="elementLabel">Country :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCountry" runat="server" CssClass="textBox"
                                                Width="15px" Text="IN" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="lblCountryName" runat="server" CssClass="elementLabel" Text="INDIA"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Telephone No. :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="100px" TabIndex="7"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <span class="elementLabel">Fax No. :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtFaxNo" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="80px" TabIndex="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField"></span><span class="elementLabel">Swift Code :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtSwiftCode" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="200px" TabIndex="9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">E-Mail Id :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtEmailId" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="200px" TabIndex="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Contact Person :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtContactPerson" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="200px" TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td align="left" colspan="3">
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
