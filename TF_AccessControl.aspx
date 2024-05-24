<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AccessControl.aspx.cs"
    Inherits="TF_AccessControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
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
    <style>
        /* The switch - the box around the slider */
        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 20px;
        }

            /* Hide default HTML checkbox */
            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        /* The slider */
        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 20px;
                left: 4px;
                top: 1px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #77df42;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #77df42;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(20px);
            -ms-transform: translateX(20px);
            transform: translateX(20px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
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
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <span class="pageLabel"><strong>Access Control</strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%;" valign="top">
                                <input type="hidden" id="hdnModuleID" runat="server" />
                                    
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                            <td align="left">
                                &nbsp;&nbsp;
                                <span class="elementLabel">User :</span>
                                     &nbsp;
                                <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                    TabIndex="1" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" Width="200px">
                                </asp:DropDownList>
                                     &nbsp;&nbsp;
                                <span class="elementLabel">Access :</span>
                                     &nbsp
                                <asp:DropDownList ID="ddlAccess" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                    TabIndex="2" Width="200px" 
                                    onselectedindexchanged="ddlAccess_SelectedIndexChanged">
                                               <asp:ListItem>---Select---</asp:ListItem>
                                                <asp:ListItem>Master</asp:ListItem>
                                                <asp:ListItem>Transaction</asp:ListItem>
                                                <asp:ListItem>File Creation</asp:ListItem>
                                                <asp:ListItem>Excel Reports</asp:ListItem>
                                                <asp:ListItem>Reports</asp:ListItem>
                                                <asp:ListItem>File upload</asp:ListItem>
                                                <asp:ListItem>File Format</asp:ListItem>
                                                <asp:ListItem>Audit Trail</asp:ListItem>
                                </asp:DropDownList>
                                    &nbsp&nbsp 
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                        OnClick="btnSave_Click" TabIndex="4" />
                                       &nbsp&nbsp <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="top">
                                    <hr />
                                </td>
                            </tr>
                            </table>
                           <table cellspacing="0" border="0" width="100%">
                            <tr>
                            <td align="left" style="width: 100%" valign="top">
                             <asp:GridView ID="GridViewMenuList" runat="server" AutoGenerateColumns="False"
                                    Width="60%" CssClass="GridView">
                                    <RowStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="10px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <span class="elementLabel"><%# ((GridViewRow)Container).RowIndex + 1%></span> 
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="2%" />
                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Menus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMenu" runat="server" CssClass="elementLabel" Text='<%# Eval("MenuName") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow/Deny" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%"
                                                    ItemStyle-Width="5%">
                                                    <HeaderTemplate>
                                                        <label class="switch">
                                                            <asp:CheckBox runat="server" ID="HeaderChkAllow" AutoPostBack="true"
                                                                OnCheckedChanged="HeaderChkAllow_CheckedChanged" TabIndex="2" />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <label class="switch">
                                                            <asp:CheckBox runat="server" ID="RowChkAllow" OnCheckedChanged="RowChkAllow_CheckedChanged"
                                                                TabIndex="3" />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Module">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModule" runat="server" CssClass="elementLabel" Text='<%# Eval("Module") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" ForeColor="#1A60A6" VerticalAlign="Top" />
                                        <RowStyle CssClass="gridItem" Height="18px" HorizontalAlign="Left" VerticalAlign="Top"
                                            Wrap="False" />
                                    </asp:GridView>
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
