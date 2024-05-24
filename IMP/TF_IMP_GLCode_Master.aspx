<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_GLCode_Master.aspx.cs" Inherits="IMP_TF_IMP_GLCode_Master" %>
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var txtGLCode = document.getElementById('txtGLCode');
            var txtGLCodeDiscreption = document.getElementById('txtGLCodeDiscreption');

            if (txtGLCode.value == '') {
                alert('Enter GL Code.');
                txtGLCode.focus();
                return false;
            }
            if (txtGLCodeDiscreption.value == '') {
                alert('Enter GL Code Discreption.');
                txtGLCodeDiscreption.focus();
                return false;
            }
            if (txtGLCurr.value == '') {
                alert('Enter GL Currency.');
                txtGLCurr.focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left">
                                <span class="pageLabel"><strong>GL Code Master</strong></span>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click" AutoPostBack="true"
                                    ToolTip="Back" TabIndex="5" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" Font-Bold="true" Font-Size="Small" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%"  valign="top" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">GL Code:</span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                           <asp:TextBox ID="txtGLCode" runat="server" CssClass="textBox" MaxLength="5"
                                                AutoPostBack="true" Width="60px" TabIndex="1" OnTextChanged="txtGLCode_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" >
                                            <span class="mandatoryField">*</span><span class="elementLabel">Discreption:</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtGLCodeDiscreption" runat="server" CssClass="textBox" MaxLength="40"
                                                Width="300px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" >
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency:</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtGLCurr" runat="server" CssClass="textBox" MaxLength="3"
                                                Width="60px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="4" OnClientClick="return validateSave();"/>
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
