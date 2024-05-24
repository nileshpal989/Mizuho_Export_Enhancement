<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ForgotPassword.aspx.cs" Inherits="TF_ForgotPassword" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSendRequest">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div>
            <center>
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 100%" valign="bottom" colspan="2">
                                    <span class="pageLabel">Forgot Password</span>
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
                                    <table cellspacing="0" cellpadding="0" border="0" width="550px" style="line-height: 150%">
                                        <tr>
                                            <td align="right" style="width: 140px">
                                                <span class="mandatoryField">*</span><span class="elementLabel">User Name :</span>
                                            </td>
                                            <td align="left" style="width: 310px">&nbsp;<asp:TextBox ID="txtUserName" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="150px" OnTextChanged="txtUserName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:Label ID="lblUserName" runat="server" CssClass="mandatoryField"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 140px">
                                                <span class="mandatoryField">*</span><span class="elementLabel">Email ID :</span>
                                            </td>
                                            <td align="left" style="width: 510px">&nbsp;<asp:TextBox ID="txtEmailID" runat="server" CssClass="textBox" MaxLength="100"
                                                Width="250px" OnTextChanged="txtEmailID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:Label ID="lblEmailID" runat="server" CssClass="mandatoryField"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 140px">
                                                
                                            </td>
                                            <td align="left" style="width: 310px">
                                                &nbsp;<asp:Button ID="btnSendRequest" runat="server" Text="Send Request" OnClick="btnSendRequest_Click" CssClass="buttonDefault" />
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
