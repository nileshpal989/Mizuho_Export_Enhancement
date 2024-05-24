<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ChangePassword.aspx.cs" Inherits="TF_ChangePassword" %>

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
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">

    <script language="javascript" type="text/javascript">
        function ShowAlert() {
            //            var _newpassword = document.getElementById('txtNewPassword');
            //            alert('Change your password.');
            //           _newpassword.focus();           
        }

        function validateSave() {
            var RegExpres = /^[a-z0-9 ]+$/i;
            var _userName = document.getElementById('txtUserName');
            var _password = document.getElementById('txtPassword');
            var _newpassword = document.getElementById('txtNewPassword');
            var _retypePassword = document.getElementById('txtReTypePassword');
            var _role = document.getElementById('ddlRole');


            if (_password.value == '') {
                alert('Enter Password.');
                _password.focus();
                return false;
            }
            //            if (RegExpres.test(trimAll(_password.value)) == false) {
            //                alert('Only Alphanumeric value is allowed.');
            //                _password.focus();
            //                return false;
            //            }

            ////This is to check whether new password and previous password are not same


            if (_password.value == _newpassword.value) {
                alert('Password cannot be same as previous password.');
                _newpassword.focus();
                return false;
            }

            if (_password.value < 8 ) {
                alert('Password cannot be less than 8.');
                _newpassword.focus();
                return false;
            }

            if (_retypePassword.value == '') {
                alert('Re-Type Password.');
                _retypePassword.focus();
                return false;
            }

            //            if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
            //                alert('Only Alphanumeric value is allowed.');
            //                _retypePassword.focus();
            //                return false;
            //            }

            if (_newpassword.value != _retypePassword.value) {
                alert('Password and Re-Typed Password are not matching.');
                _retypePassword.focus();
                return false;
            }

            return true;
        }



    </script>
    <script type="text/javascript">
        function policyCheck(key, controlID) {

            var _password = controlID;
            var lblcapital = document.getElementById('lblcapital');
            var lblletter = document.getElementById('lblletter');
            var lblnumber = document.getElementById('lblnumber');
            var lblSpecial = document.getElementById('lblSpecial');
            var lbllenght = document.getElementById('lbllenght');

            // Validate capital letters
            var upperCaseLetters = /[A-Z]/g;
            if (_password.value.match(upperCaseLetters)) {
                lblcapital.style.color = "green";
            } else {
                lblcapital.style.color = "red";
            }

            //Validate lower case letters
            var lowerCaseLetters = /[a-z]/g;
            if (_password.value.match(lowerCaseLetters)) {
                lblletter.style.color = "green";
            } else {
                lblletter.style.color = "red";
            }

            // Validate numbers
            var numbers = /[0-9]/g;
            if (_password.value.match(numbers)) {
                lblnumber.style.color = "green";
            } else {
                lblnumber.style.color = "red";
            }

            // Validate length
            if (_password.value.length >= 8) {
                lbllenght.style.color = "green";
            } else {
                lbllenght.style.color = "red";
            }

            // Validate Special Chracter
            var special = /[!#$%&*+,\-<=>?@^_`|~]/g;
            if (_password.value.match(special)) {
                lblSpecial.style.color = "green";
            } else {
                lblSpecial.style.color = "red";
            }

            return true;

        }
        function checkMatchPass() {
            var _password = document.getElementById('txtNewPassword');
            var txtReTypePassword = document.getElementById('txtReTypePassword');
            var lblMatch = document.getElementById('lblMatch');
            if (_password.value != "" & txtReTypePassword.value != "") {
                if (_password.value == txtReTypePassword.value) {
                    lblMatch.style.color = "green";
                }
                else {
                    lblMatch.style.color = "red";
                }
            }
        }
        function ClearMsg() {
            document.getElementById('lblcapital').style.color = "red";
            document.getElementById('lblletter').style.color = "red";
            document.getElementById('lblnumber').style.color = "red";
            document.getElementById('lblSpecial').style.color = "red";
            document.getElementById('lbllenght').style.color = "red";
            return true;

        }
    </script>
    <style type="text/css">
        .style9 {
            width: 40%;
        }

        .style10 {
            width: 10%;
        }
    </style>

</head>
<body onload="ShowAlert();">
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
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
                                    <span class="pageLabel">Change Password</span>
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
                                <td align="left" style="width: 100%" valign="top">
                                    <table cellspacing="0" cellpadding="0" border="0" width="450px" style="line-height: 150%; width: 1267px;">
                                        <tr>
                                            <td align="right" class="style10">
                                                <span class="mandatoryField">*</span><span class="elementLabel">User Name :</span>
                                            </td>
                                            <td align="left" class="style9">&nbsp;<asp:TextBox ID="txtUserName" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="200px"></asp:TextBox>
                                            </td>
                                            <td align="left" rowspan="6" style="width: 50%; font-size: small; color: blue">
                                                <div id="message" style="visibility: visible">
                                                    <asp:Label ID="lblMsgheader" CssClass='pageLabel' runat="server" Text="Password Policy :"
                                                        Font-Bold="True" Font-Size="Medium"></asp:Label><br />
                                                    <asp:Label ID="lblcapital" ForeColor="Red" runat="server" Text="Minimum 1 uppercase character."
                                                        Font-Bold="True" Font-Size="Small"></asp:Label><br />
                                                    <asp:Label ID="lblletter" ForeColor="Red" runat="server" Text="Minimum 1 lowercase character."
                                                        Font-Bold="True" Font-Size="Small"></asp:Label><br />
                                                    <asp:Label ID="lblnumber" ForeColor="Red" runat="server" Text="Minimum 1 number." Font-Bold="True"
                                                        Font-Size="Small"></asp:Label><br />
                                                    <asp:Label ID="lblSpecial" ForeColor="Red" runat="server" Text="Minimum 1 special character."
                                                        Font-Bold="True" Font-Size="Small"></asp:Label><br />
                                                    <asp:Label ID="lbllenght" ForeColor="Red" runat="server" Text="Minimum 8 characters."
                                                        Font-Bold="True" Font-Size="Small"></asp:Label><br />
                                                    <asp:Label ID="lblMatch" ForeColor="Red" runat="server" Text="Re-type password."
                                                        Font-Bold="True" Font-Size="Small"></asp:Label><br />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style10">
                                                <span class="mandatoryField">*</span><span class="elementLabel">Current Password :</span>
                                            </td>
                                            <td align="left" class="style9">&nbsp;<asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style10">
                                                <span class="mandatoryField">*</span><span class="elementLabel">New Password :</span>
                                            </td>
                                            <td align="left" class="style9">
                                                &nbsp;<asp:TextBox ID="txtNewPassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password" ToolTip="Must Contain atleast one number, one uppercase and Lowercase latter,one Special character and atleast 8 or more character."></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style10">
                                                <span class="mandatoryField">*</span><span class="elementLabel">Re-Type Password :</span>
                                            </td>
                                            <td align="left" class="style9">&nbsp;<asp:TextBox ID="txtReTypePassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password" ToolTip="Must Contain atleast one number, one uppercase and Lowercase latter,one Special character and atleast 8 or more character."></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 140px"></td>
                                            <td align="left" style="width: 310px; padding-top: 10px; padding-bottom: 10px">&nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" />
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

