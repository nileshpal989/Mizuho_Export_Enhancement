﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_OUCodeLookup.aspx.cs" Inherits="TF_OUCodeLookup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <title>LMCC- CTR SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Enable_Disable_Opener.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        window.onload = function () {
            window.document.statusbar.enable = false;
        }

        function validateSearch() {
            var _txtvalue = document.getElementById('txtSearch').value;
            _txtvalue = _txtvalue.replace(/'&lt;'/, "");
            _txtvalue = _txtvalue.replace(/'&gt;'/, "");
            if (_txtvalue.indexOf('<!') != -1 || _txtvalue.indexOf('>!') != -1 || _txtvalue.indexOf('!') != -1 || _txtvalue.indexOf('<') != -1 || _txtvalue.indexOf('>') != -1 || _txtvalue.indexOf('|') != -1) {
                alert('!, |, <, and > are not allowed.');
                document.getElementById('txtSearch').value = _txtvalue;
                return false;
            }
            else
                return true;
        }

        function submitForm(event) {
            if (event.keyCode == '13') {
                if (validateSearch() == true)
                    __doPostBack('btngo', '');
                else
                    return false;
            }
        }
        
    </script>

</head>
<body bgcolor="#CDDAE4" topmargin="0" bottommargin="0" rightmargin="0" leftmargin="0"
    onload="disable_parent();EndRequest();" onunload="enable_parent();" onblur="CheckFocus();"
    onfocus="CheckFocus();" style="width: 100%; height: 100%;">
    <form id="form1" autocomplete="off" runat="server">
        <asp:ScriptManager ID="scriptManagerPopup" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upLookupEmployees" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <table cellspacing="0" cellpadding="2" border="0" width="50%" style="padding-top: 10px">
                        <tr>
                            <td align="right" style="width: 2%;" valign="middle" nowrap>
                                <span class="labelcss">Search :</span>
                            </td>
                            <td align="left" valign="top" style="width: 15%;">
                                &nbsp;<asp:TextBox ID="txtSearch" runat="server" CssClass="txtenabled" MaxLength="40"
                                    onblur="GiveFocus='FALSE';CheckFocus();" onclick="Change(this);" Width="200px"></asp:TextBox>
                                &nbsp;
                                <asp:Button ID="btngo" runat="server" ToolTip="Search" CssClass="btnGO" OnClick="btngo_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="padding-top: 10px" width="100%">
                                    <tr>
                                        <td nowrap>
                                            <asp:RadioButtonList ID="rbtnType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rbtnType_SelectedIndexChanged">
                                                <asp:ListItem Text="OUCode" Value="1" Selected="true"></asp:ListItem>
                                                <asp:ListItem Text="Branch Name" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 15%;" valign="top" colspan="2">
                                &nbsp;<asp:ListBox ID="lstOUCodeList" runat="server" Height="335px" 
                                    Width="320px" CssClass="labelcss"
                                    onblur="GiveFocus='FALSE';CheckFocus();" onmouseover="Change(this);"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="buttonDefault" Text="OK" OnClick="btnSubmit_Click"
                                    ToolTip="OK" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" Text="Cancel"
                                    ToolTip="Cancel" />
                                <input id="hndsessionid" type="hidden" runat=server />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
