<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditSanctionedCountry.aspx.cs"
    Inherits="EXP_EXP_AddEditSanctionedCountry" %>

<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen"/>
      <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script> <%--snackbar--%>
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" type="text/javascript"></script>
        <link href="../Help_Plugins/JueryUI.css" rel="stylesheet" type="text/css" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function validateSave() {

            var cid = document.getElementById('txtCountryID').value;
            var cname = document.getElementById('txtCountryName').value;

            if (cid == '') {
//                alert('Enter Country ID');
                //                document.getElementById('txtCountryID').focus();
                VAlert('Enter Country ID', '#txtCountryID');
                return false;
            }

            if (cname == '') {
//                alert('Enter Country Name');
                //                document.getElementById('txtCountryName').focus();
                VAlert('Enter Country Name', '#txtCountryName');
                return false;
            }
        }

        function onlyChars(e) {
            var key = e.keyCode;
            var keychar = String.fromCharCode(key);
            var reg = new RegExp("[a-zA-Z ]");
            if (key != 13) {
                return reg.test(keychar);
            }
        }

        function OpenCountryCodeList(e) {
        var keycode;
       
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {

                open_popup('../TF_CountryLookUp1.aspx?hNo=1', 450, 450, 'CountryList');

                return false;
            }
        }

        function selectCountry(selectedID, hNo) {
            var id = selectedID;
            document.getElementById('hdnCountry').value = id;
            document.getElementById('btnCountry').click();

        }

    </script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
     <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                            <input type="hidden" runat="server" id="hdnsancountryname" />
                                <span class="pageLabel"><strong>Country Master Details</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                                 <input type="hidden" id="hdnCountry" runat="server" />
                            <asp:Button ID="btnCountry" Style="display: none;" runat="server" OnClick="btnCountry_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">* </span><span class="elementLabel">Sanctioned Country
                                                ID :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtCountryID" runat="server" CssClass="textBox" MaxLength="2" Width="30px"
                                                onkeydown="OpenCountryCodeList(this);" TabIndex="1" ToolTip="Press F2 for help."
                                                AutoPostBack="true" OnTextChanged="txtCountryID_TextChanged"></asp:TextBox><asp:Button
                                                    ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Sanctioned Country Name :</span>
                                        </td>
                                        <td align="left" style="width: 400px"><asp:TextBox ID="txtCountryName" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" OnClick="btnCancel_Click" />
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
