<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditReimbursingBank.aspx.cs"
    Inherits="Master_EXP_AddEditReimbursingBank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var txtBankID = document.getElementById('txtBankID');
            if (txtBankID.value == '') {
                alert('Enter Bank ID.');
                txtBankID.focus();
                return false;
            }
            var txtBankName = document.getElementById('txtBankName');
            if (txtBankName.value == '') {
                alert('Enter Bank Name.');
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
    <style type="text/css">
        /* Label Style of Instructions */
        .elementLabelRed
        {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            font-family: Verdana, Sans-Serif, Arial;
            color: Red;
            background-color: Transparent;
            font-weight: bold;
            font-size: 8pt;
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
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
                                <span class="pageLabel"><strong>Reimbursing Bank Master Details</strong></span>
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
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtCurr" runat="server" CssClass="textBox" MaxLength="3" Width="40"
                                                TabIndex="1" AutoPostBack="true" OnTextChanged="txtCurr_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                            <asp:Label ID="lblCurrDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bank ID :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtBankID" runat="server" CssClass="textBox" MaxLength="7"
                                                Width="60" TabIndex="2" AutoPostBack="true" OnTextChanged="txtBankID_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Name :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtBankName" runat="server" CssClass="textBox" MaxLength="100"
                                                Width="250" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch Name :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtBranchName" runat="server" CssClass="textBox" MaxLength="100"
                                                Width="250" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 75px; vertical-align: middle">
                                        <td align="right" valign="top" style="width: 110px">
                                            <span class="elementLabel">Address :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtAddress" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="250px" TextMode="MultiLine" Height="65px" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">City :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCity" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="70px" TabIndex="6"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Pincode :</span> &nbsp;<asp:TextBox
                                                ID="txtPincode" runat="server" CssClass="textBox" MaxLength="6" Width="70px"
                                                TabIndex="7"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Country. :</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            &nbsp;<asp:TextBox ID="txtCountry" runat="server" CssClass="textBox" MaxLength="3"
                                                Width="30px" TabIndex="8" AutoPostBack="true" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" Width="16px" />
                                            <asp:Label ID="lblCountryName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap" align="right">
                                            <span class="elementLabel">Special Instructions :</span>
                                        </td>
                                        <td colspan="3" align="left">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions1" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="9" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                            &nbsp;<span class="elementLabelRed">(Total 10 Lines : 77 Characters per line)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions2" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="10" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions3" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="11" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions4" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="12" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions5" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="13" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions6" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="14" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions7" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="15" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions8" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="16" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions9" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="17" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="3" align="left" style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtSpecialInstructions10" runat="server" CssClass="textBox"
                                                Style="vertical-align: middle;" Width="800px" TabIndex="18" onfocus="this.select()"
                                                MaxLength="77"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-top: 10px; padding-bottom: 10px" colspan="2">
                                &nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                    OnClick="btnSave_Click" TabIndex="19" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                    ToolTip="Cancel" OnClick="btnCancel_Click" TabIndex="20" />
                                <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
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
