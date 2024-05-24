<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditBusinessSourceMaster.aspx.cs"
    Inherits="TF_AddEditBusinessSourceMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-Trade Finance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="Scripts/Validations.js"  language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function Myfunction2() {

            var x2 = document.getElementById('txtBusinessSourceID').value;
            
            if (x2.length < 4) {
                alert('Invalid Id');
                return false;
            }
            Myfunction();
            var y;
            var v = document.getElementById('Button2').value;

            if (x2[0] == v[0]) {
                return true;
            }
            else {
                alert('Enter only ' + v[0]);
            }

        }

        function Myfunction() {
            var x = document.getElementById("txtBusinessSourceID").value;
            for (i = 1; i < 11; i++) {
                y = x.charAt(i);
                if (y == " " || isNaN(y)) {
                    alert("Enter Only Numbers");
                }
            }
        }

        function validateSave() {
            var BusSourceId = document.getElementById('txtBusinessSourceID');
            if (trimAll(BusSourceId.value) == '') {

                alert('Enter Business Source ID.');
                BusSourceId.focus();
                return false;

            }

            var BusSourceName = document.getElementById('txtName');
            if (trimAll(BusSourceName.value) == '') {

                alert('Enter Business Source Name.');
                BusSourceName.focus();
                return false;
            }


            return true;
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Account Officer Master Details</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="17" OnClick="btnBack_Click" />
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
                            <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                <table class="elementTable">
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Business Source ID
                                                : </span>
                                        </td>
                                        <td>
                                            <%--  <asp:TextBox ID="txtID" runat="server" CssClass="textBox" 
                    MaxLength="1" Width="20px" onkeydown=""
                    TabIndex="-1" 
                    Enabled="False"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtBusinessSourceID" runat="server" CssClass="textBox" MaxLength="4"
                                                Width="50px" TabIndex="1" OnTextChanged="txtBusinessSourceID_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Name :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="textBox" MaxLength="30" Width="350px"
                                                TabIndex="2" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Designation :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="textBox" MaxLength="30"
                                                Width="350px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table class="elementTable">
                                    <tr>
                                        <td width="60px">
                                        </td>
                                        <td align="left" style="width: 220px">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                OnClick="btnSave_Click" TabIndex="4" />&nbsp
                                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="BtnCancel_Click" />
                                            <asp:Button ID="Button2" runat="server" ClientIDMode="Static" Style="visibility: hidden"
                                                Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
