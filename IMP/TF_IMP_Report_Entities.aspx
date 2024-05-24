<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Report_Entities.aspx.cs" Inherits="IMP_TF_IMP_Report_Entities" %>
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var txt_Ctrl_Dept = document.getElementById('txt_Ctrl_Dept');
            var txt_Ctrl_Person = document.getElementById('txt_Ctrl_Person');
            var txt_BalChecker = document.getElementById('txt_BalChecker');

            if (txt_Ctrl_Dept.value == '') {
                alert('Enter Control Dept.');
                txt_Ctrl_Dept.focus();
                return false;
            }
            if (txt_Ctrl_Person.value == '') {
                alert('Enter Control Person.');
                txt_Ctrl_Person.focus();
                return false;
            }
            if (txt_BalChecker.value == '') {
                alert('Enter Balance Checker.');
                txt_BalChecker.focus();
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
                                <span class="pageLabel"><strong>I-6 Report Entities</strong></span>
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
                                            <span class="mandatoryField">*</span><span class="elementLabel">Control Dept:</span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                           <asp:TextBox ID="txt_Ctrl_Dept" runat="server" CssClass="textBox" MaxLength="10"
                                                AutoPostBack="true" Width="60px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" >
                                            <span class="mandatoryField">*</span><span class="elementLabel">Control Person:</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txt_Ctrl_Person" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="300px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" >
                                            <span class="mandatoryField">*</span><span class="elementLabel">Balance Checker:</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txt_BalChecker" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="300px" TabIndex="3"></asp:TextBox>
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
