<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Impw_AddEditSupplierMaster.aspx.cs"
    Inherits="ImportWareHousing_Masters_Impw_AddEditSupplierMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function OpenCustomerCodeList(e, srno) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 13 || e == 'mouseClick') {
                var txtCustAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
                popup = window.open('../HelpForms/TF_IMPW_Help_Customer.aspx?srno=' + 1, 'helpCountryId', 'height=500,  width=500,status= no, resizable= yes, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
                return false;
            }
        }
        function selectCustomer(CustACNo, srno) {
            document.getElementById('txtCustACNo').value = CustACNo;
            __doPostBack('txtCustACNo', '');
        }

        function Countryhelp(e, srno) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 13 || e == 'mouseClick') {
                popup = window.open('../HelpForms/TF_IMPW_Help_Country.aspx?srno=' + srno, 'helpCountryId', 'height=500,  width=500,status= no, resizable= yes, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
                return false;
            }
        }

        function selectCountry(CountryID, srno) {
            if (srno == '1') {
                document.getElementById('txtSupplierCountry').value = CountryID;
                __doPostBack('txtSupplierCountry', '');
            }
            else if (srno == '2') {
                document.getElementById('txtBankCountry').value = CountryID;
                __doPostBack('txtBankCountry', '');
            }

        }

        function validateSave() {
            var txtSupplierID = document.getElementById("txtSupplierID");
            var txtSupplierName = document.getElementById("txtSupplierName");
            var txtCustACNo = document.getElementById("txtCustACNo");
            if (txtCustACNo.value.trim() == '') {
                alert('Enter Customer A/C No');
                txtCustACNo.focus();
                return false;
            }
            if (txtSupplierID.value.trim() == '') {
                alert('Enter Supplier ID');
                txtSupplierID.focus();
                return false;
            }
            if (txtSupplierName.value.trim() == '') {
                txtSupplierName.value = '';
                alert('Enter Supplier Name');
                txtSupplierName.focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnClear" />
                </Triggers>
                <ContentTemplate>
                    <table style="margin-top: 5px" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Supplier Master Data Entry</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" Font-Bold="true"
                                    ToolTip="Back" TabIndex="16" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%" valign="top">
                            </td>
                            <td align="right" style="width: 50%" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; border: 1px solid #49A3FF; padding: 5px 5px 5px 5px" valign="middle"
                                colspan="2">
                                <table cellspacing="4px" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Branch :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtAdCode" runat="server" Enabled="false" CssClass="textBox" MaxLength="20"
                                                Width="60PX"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Cust A/C No. :&nbsp;
                                            </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtCustACNo" runat="server" CssClass="textBox" ToolTip="Enter Customer A/C No &#013;Press Enter For HELP"
                                                MaxLength="20" Width="140PX" AutoPostBack="true" onkeydown="OpenCustomerCodeList(this,'1');"
                                                OnTextChanged="txtCustACNo_TextChanged" TabIndex="1"></asp:TextBox>
                                            <asp:Button ID="btnCustomerList" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                                onkeydown="OpenCustomerCodeList(this,'1');" ToolTip="Press Enter for help." />
                                            <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Supplier ID :&nbsp;
                                            </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierID" runat="server" CssClass="textBox" ToolTip="Enter Supplier ID"
                                                MaxLength="20" Width="180PX" AutoPostBack="true" OnTextChanged="txtSupplierID_TextChanged"
                                                TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Supplier Name :&nbsp;
                                            </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" ToolTip="Enter Supplier Name"
                                                MaxLength="50" Width="400PX" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Supplier Address :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierAddress" runat="server" CssClass="textBox" ToolTip="Enter Supplier Address"
                                                MaxLength="200" Width="300px" TextMode="MultiLine" Height="30px" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Supplier Country :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierCountry" runat="server" CssClass="textBox" ToolTip="Enter Supplier Country &#013;Press Enter For HELP"
                                                MaxLength="2" Width="25px" Style="text-transform: uppercase" AutoPostBack="true"
                                                OnTextChanged="txtSupplierCountry_TextChanged" onkeydown="Countryhelp(this,'1');"
                                                TabIndex="5"></asp:TextBox>
                                            <asp:Button ID="btnSupplierCountryList" runat="server" CssClass="btnHelp_enabled"
                                                onkeydown="Countryhelp(this,'1');" Width="16px" />
                                            <asp:Label ID="lblSupplierCountryName" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Bank Swift Code :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtBankSwiftCode" runat="server" CssClass="textBox" ToolTip="Enter Bank Swift Code"
                                                MaxLength="20" Width="100px" TabIndex="6"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Bank Name :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="textBox" ToolTip="Enter Bank Name"
                                                MaxLength="50" Width="200px" TabIndex="7"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Bank Address :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtBankAddress" runat="server" CssClass="textBox" ToolTip="Enter Bank Address"
                                                MaxLength="200" Width="300px" TextMode="MultiLine" Height="30px" TabIndex="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Bank Country :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtBankCountry" runat="server" CssClass="textBox" ToolTip="Enter Bank Country &#013;Press Enter For HELP"
                                                MaxLength="2" Width="25px" Style="text-transform: uppercase" AutoPostBack="true"
                                                OnTextChanged="txtBankCountry_TextChanged" onkeydown="Countryhelp(this,'2');"
                                                TabIndex="9"></asp:TextBox>
                                            <asp:Button ID="btnBankCountryList" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                                onkeydown="Countryhelp(this,'2');" />
                                            <asp:Label ID="lblBankCountryName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Supplier Contact No. :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierContactNo" runat="server" CssClass="textBox" ToolTip="Enter Supplier Contact No."
                                                MaxLength="20" Width="120px" TabIndex="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Supplier Email ID 1 :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierEmailID1" runat="server" CssClass="textBox" ToolTip="Enter Supplier Email ID"
                                                MaxLength="50" Width="200px" TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Supplier Email ID 2 :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierEmailID2" runat="server" CssClass="textBox" ToolTip="Enter Supplier Email ID"
                                                MaxLength="50" Width="200px" TabIndex="12"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Supplier Email ID 3 :&nbsp; </span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <asp:TextBox ID="txtSupplierEmailID3" runat="server" CssClass="textBox" ToolTip="Enter Supplier Email ID"
                                                MaxLength="50" Width="200px" TabIndex="13"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                OnClick="btnSave_Click" TabIndex="14" />&nbsp
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="buttonDefault" OnClick="btnClear_Click"
                                                TabIndex="15" />
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
