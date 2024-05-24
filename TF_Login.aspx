<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_Login.aspx.cs" Inherits="TF_Login" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link type="text/css" href="Style/style_new.css" rel="stylesheet" media="screen">
    <link href="Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <title>LMCC Trade Finance System</title>
    <script language="javascript" type="text/javascript">

        function OpenUserList() {

            open_popup('TF_UserLookUp.aspx', 400, 200, 'UserList');
            return false;
        }
        function selectUser(Uname) {

            document.getElementById('txtUserName').value = Uname;
            document.getElementById('txtUserName').focus();
        }



        function validateSave() {
            var _userName = document.getElementById('txtUserName');
            var _password = document.getElementById('txtPassword');
            var _retypePassword = document.getElementById('txtReTypePassword');
            var _role = document.getElementById('ddlRole');

            if (_userName.value == '') {
                alert('Enter User Name.');
                _userName.focus();
                return false;
            }

            if (_password.value == '') {
                alert('Enter Password.');
                _password.focus();
                return false;
            }

            return true;
        }


        function ConfirmCahngePaswd() {
            var msg = 'Your Password will expire in ' + document.getElementById('hdmremaindays').value + 'days.';
            alert(msg);
            var btrue = false;

            var Ok = confirm('Do you want to change it now?');


            if (Ok) {
                Call();
                btrue = true;
            }
            else {
                Call2();
                btrue = false;
            }


            return btrue;
        }

        function Call() {
            document.getElementById('Hidden1').value = '';
            document.getElementById('Hidden1').value = '1';
            document.getElementById('btnalert').click();
        }

        function Call2() {
            document.getElementById('Hidden1').value = '';
            document.getElementById('Hidden1').value = '0';
            document.getElementById('btnalert').click();
        }

        function ConfirmCahngePaswdExpired() {
            var msg = 'Your Password has been expired.';
            alert(msg);
            var btrue = false;

            var Ok = confirm('Do you want to change it now?');


            if (Ok) {
                Call();
                btrue = true;
            }
            else {
                //                Call2();
                //                btrue = false;
            }


            return btrue;
        }
    </script>
</head>
<body class="body2">
    <form id="form1" runat="server" autocomplete="on" defaultbutton="btnLogin">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
    <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--<div class="login_message">--%>
    <asp:Label ID="labelMessage" runat="server" Style="color: Red; font-size: xx-large;"></asp:Label>
    <asp:Button ID="btnalert" runat="server" OnClick="btnalert_Click" />
    <input type="hidden" id="hdmremaindays" runat="server" />
    <input type="hidden" id="Hidden1" runat="server" />
    <%--   </div>--%>
    <div class="login_bg">
        <div class="container3">
            <div class="container5">
                <span class="h3">Login</span>
            </div>
            <div class="container6">
                <div class="container8">
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="input-login" MaxLength="20">
                    </asp:TextBox>
                </div>
                <div class="container7">
                    <asp:Button ID="btnUserList" runat="server" CssClass="help_bt" TabIndex="-1" />
                </div>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="input-login" MaxLength="15"
                    TextMode="Password">
                </asp:TextBox>
                <div class="container4">
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="buttonDefault" ToolTip="Login"
                    OnClick="btnLogin_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="buttonDefault" ToolTip="Reset"
                    OnClick="btnReset_Click" />
                <%--<asp:Button ID="btnForgotPassword" runat="server" Text="Forgot Password" CssClass="buttonDefault" ToolTip="Forgot Password"
                    OnClick="btnForgotPassword_Click" />--%>
            </div>
        </div>
    </div>
    <div class="footer">
        <span class="h2">&copy;&nbsp;2013 Lateral Management Computer Consultants</span></div>
    </form>
</body>
</html>
