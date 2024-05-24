<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TD_AddReceivingBankMaster.aspx.cs"
    Inherits="TD_AddReceivingBankMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function validateSave() {
            
            var _currencyID = document.getElementById('txtCurrencyID');
            var bankname = document.getElementById('txtBankName');
            var abbr = document.getElementById('txtAbbr');
            
            if (_currencyID.value == '') {
                alert('Enter Currency ID.');
                _currencyID.focus();
                return false;
            }
            
            if (bankname.value == '') {
                alert('Enter Bank Name.');
                _description.focus();
                return false;
            }
            if (abbr.value == '') {
                alert('Enter Abbreviation.');
                _description.focus();
                return false;
            }
            //if(RegExpres.test(trimAll(_description.value)) == false)
            //{
            //alert('Only Alphanumeric value is allowed.');
            //_description.focus();
            //return false;
            //}
            return true;
        }

        function curhelp() {
            popup = window.open('TF_CurrencyLookUp2.aspx', 'helpCurId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCurId";
            return false;

        }

        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpCurId") {
                document.getElementById('txtCurrencyID').value = s;
                document.getElementById('txtBankName').focus();
            }
        }

        function openCountryCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('TF_CountryLookUp1.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectCountry(id, hNo) {
            var txtBankCountry = document.getElementById('txtBankCountry');
            if (hNo == '1') {
                txtBankCountry.value = id;
                __doPostBack('txtBankCountry', '');
                return true;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Receiving Bank Master Entry</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" TabIndex="5" />
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
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">SrNo :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtSrNo" runat="server" CssClass="textBox" TabIndex="-1" MaxLength="5"
                                                Width="20px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency ID :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtCurrencyID" runat="server" CssClass="textBox" TabIndex="-1" Enabled="false"
                                                MaxLength="3" Width="50px"></asp:TextBox>
                                            <asp:Button ID="btnCurrency" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bank Name :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtBankName" runat="server" TabIndex="1" CssClass="textBox" MaxLength="50"
                                                Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Abbreviation :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtAbbr" runat="server" TabIndex="2" CssClass="textBox" MaxLength="15"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>

                                       <tr>
                                        <td align="right" style="width: 200px">
                                            <%--<span class="mandatoryField">*</span>--%>
                                            <span class="elementLabel">Bank Country :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtBankCountry" runat="server" TabIndex="3" CssClass="textBox" MaxLength="2"
                                                Width="40px" AutoPostBack="true" 
                                                ontextchanged="txtBankCountry_TextChanged"></asp:TextBox>
                                                   <asp:Button ID="Button4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                OnClientClick="return openCountryCode('mouseClick','1');" />
                            &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" OnClick="btnSave_Click"
                                                ToolTip="Save" TabIndex="4" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="5" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
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
