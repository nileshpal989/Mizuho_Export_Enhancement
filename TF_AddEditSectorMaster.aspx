<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditSectorMaster.aspx.cs"
    Inherits="TF_AddEditSectorMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="Scripts/Validations.js"  language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function validateSave() {
            var secId = document.getElementById('txtSectorID');
            if (trimAll(secId.value) == '') {
                try {
                    alert('Enter Sector ID.');
                    secId.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Sector ID.');
                    return false;
                }
            }

            var secName = document.getElementById('txtSectorName');
            if (trimAll(secName.value) == '') {
                try {
                    alert('Enter Sector Name.');
                    secName.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Sector Name.');
                    return false;
                }
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
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Sector Master Details</span>
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
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Sector ID : </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSectorID" runat="server" CssClass="textBox" MaxLength="2" Width="30px"
                                                TabIndex="1" OnTextChanged="txtSectorID_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Sector Name :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            <asp:TextBox ID="txtSectorName" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="350px" TabIndex="2" OnTextChanged="txtSectorName_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table class="elementTable">
                                    <tr>
                                        <td width="60px">
                                        </td>
                                        <td align="left" style="width: 220px">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                OnClick="btnSave_Click" />&nbsp
                                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="BtnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
