<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_HouseKeeping.aspx.cs" Inherits="TF_HouseKeeping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validations.js"  language="javascript" type="text/javascript"></script>
    <link href="Style/style_new.css"  rel="Stylesheet" type="text/css" media="screen">
   <script language="javascript" type="text/javascript">
       function validateSave() {
           if (document.getElementById('hdntype').value == "Pwsd") {
               return validatePaswd();
           }
           else {

           //    var RegExpres = /^[a-z0-9 ]+$/i;
               var _userName = document.getElementById('txtUserName');
               var _password = document.getElementById('txtPassword');
               var _retypePassword = document.getElementById('txtReTypePassword');
               var _role = document.getElementById('ddlRole');

               if (_userName.value == '') {
                   alert('Enter User Name.');
                   _userName.focus();
                   return false;
               }
//               if (RegExpres.test(trimAll(_userName.value)) == false) {
//                   alert('Only Alphanumeric value is allowed.');
//                   _userName.focus();
//                   return false;
//               }
               if (_password.value == '') {
                   alert('Enter Password.');
                   _password.focus();
                   return false;
               }
//               if (RegExpres.test(trimAll(_password.value)) == false) {
//                   alert('Only Alphanumeric value is allowed.');
//                   _password.focus();
//                   return false;
//               }
//               else {
                   if (_password.value.length < 8) {
                       alert('Enter minimum 8 characters in Password.');
                       _password.focus();
                       return false;
                   }
//               }

               ////This is to check whether new password and previous password are not same

               //if(document.getElementById('hdnpswd').value!='')
               //{
               //    if(document.getElementById('hdnpswd').value!=_password.value)
               //    {
               //        if(document.getElementById('hdnpswd').value==_password.value)
               //        {
               //            alert('Password cannot be same as previous password.');
               //            _password.focus();
               //            return false;
               //        }
               //    }
               //}

               if (_retypePassword.value == '') {
                   alert('Re-Type Password.');
                   _retypePassword.focus();
                   return false;
               }

//               if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
//                   alert('Only Alphanumeric value is allowed.');
//                   _retypePassword.focus();
//                   return false;
//               }
               if (_password.value != _retypePassword.value) {
                   alert('Password and Re-Typed Password are not matching.');
                   _retypePassword.focus();
                   return false;
               }
               if (_role.selectedIndex == -1 || _role.selectedIndex == 0) {
                   alert('Select Role.');
                   _role.focus();
                   return false;
               }
           }
           return true;
       }

       function validatePaswd() {
   //        var RegExpres = /^[a-z0-9 ]+$/i;
           var _password = document.getElementById('txtPassword');
           var _retypePassword = document.getElementById('txtReTypePassword');
           var _newpassword = document.getElementById('hdnpswd');

           if (_password.value == '') {
               alert('Enter Password.');
               _password.focus();
               return false;
           }
//           if (RegExpres.test(trimAll(_password.value)) == false) {
//               alert('Only Alphanumeric value is allowed.');
//               _password.focus();
//               return false;
//           }
//           else {
               if (_password.value.length < 8) {
                   alert('Enter minimum 8 characters in Password.');
                   _password.focus();
                   return false;
               }
        //   }

           if (_password.value == _newpassword.value) {
               alert('Password cannot be same as previous password.');
               _password.focus();
               return false;
           }
           if (_retypePassword.value == '') {
               alert('Enter Re-Type Password.');
               _retypePassword.focus();
               return false;
           }

//           if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
//               alert('Only Alphanumeric value is allowed.');
//               _retypePassword.focus();
//               return false;
//           }

           if (_password.value != _retypePassword.value) {
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
            var _password = document.getElementById('txtPassword');
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
        .style9
        {
            width: 40%;
        }
        .style10
        {
            width: 10%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 100%" valign="bottom">
                                <span class="pageLabel">User Details</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="450px" style="line-height: 150%;
                                    width: 1267px;">
                                    <tr>
                                        <td align="right" class="style10">
                                            <span class="mandatoryField">*</span><span class="elementLabel">User Name :</span>
                                        </td>
                                        <td align="left" class="style9">
                                            &nbsp;<asp:TextBox ID="txtUserName" runat="server" CssClass="textBox" MaxLength="20"
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
                                        <td align="left" class="style9">
                                            &nbsp;<asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style10">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Re-Type Password :</span>
                                        </td>
                                        <td align="left" class="style9">
                                            &nbsp;<asp:TextBox ID="txtReTypePassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password" ToolTip="Must Contain atleast one number, one uppercase and Lowercase latter,one Special character and atleast 8 or more character."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style10">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Role :</span>
                                        </td>
                                        <td align="left" class="style9">
                                            &nbsp;<asp:DropDownList ID="ddlRole" runat="server" CssClass="dropdownList">
                                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                                <asp:ListItem Value="Supervisor" Text="Supervisor"></asp:ListItem>
                                                <asp:ListItem Value="User" Text="User"></asp:ListItem>
                                                <asp:ListItem Value="OIC" Text="OIC"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style10">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Status :</span>
                                        </td>
                                        <td align="left" class="style9">
                                            &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList">
                                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="In-Active"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style10">
                                        </td>
                                        <td align="left" style="padding-top: 10px; padding-bottom: 10px" class="style9">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" />
                                             <asp:Button ID="btnChangePaswd" runat="server" Text="Change Password" Width="120"
                                                CssClass="buttonDefault" OnClick="btnChangePaswd_Click" ToolTip="Change Password" />
                                            <input type="hidden" id="hdntype" runat="server" />
                                            <input type="hidden" id="hdnpswd" runat="server" />
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
