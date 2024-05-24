<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/TF_ModuleSelection.aspx.cs"
    Inherits="TF_ModuleSelection" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <div id="body">
        <div id="container-signout">
            <div id="container2-signout">
                <asp:Button ID="signout" runat="server" CssClass="signout_bt" Text="Logout" OnClick="signout_Click" />
            </div>
        </div>
        <div class="container-header">
            <div class="container2-header">
                <div class="logo">
                </div>
                <div class="header-info">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label><asp:Label ID="lblRole" runat="server"></asp:Label>
                </div>
                <div class="header-Date">
                    <asp:Label ID="lblTime" runat="server" CssClass="elementLabel"></asp:Label>
                </div>
            </div>
        </div>
        <div class="buttonrow">
            <div class="container1-bt">
                <asp:Button ID="btnEXP" runat="server" CssClass="export_bt" TabIndex="1" OnClick="btnEXP_Click" />
            </div>
            <div class="container1-bt">
                <asp:Button ID="btnEbr" runat="server" CssClass="ebrc_bt" TabIndex="2" OnClick="btnEBR_Click" />
            </div>
            <div class="container2-bt">
                <asp:Button ID="btnXOS" runat="server" CssClass="xos_bt" TabIndex="3" OnClick="btnXOS_Click" />
            </div>
        </div>
        <div class="buttonrow2">
            <div class="container1-bt">
                <asp:Button ID="btnEDPMS" runat="server" CssClass="edpms_bt" TabIndex="4" OnClick="btnEDPMS_Click" />
            </div>
            <div class="container1-bt">
                <asp:Button ID="btnIDPMS" runat="server" CssClass="idpms_bt" TabIndex="5" OnClick="btnIDPMS_Click" />
            </div>
            <div class="container2-bt">
                <asp:Button ID="btnRreturn" runat="server" CssClass="ret_bt" TabIndex="6" OnClick="btnRreturn_Click" />
            </div>
        </div>
        <div class="buttonrow3">
            <div class="container1-bt">
                <asp:Button ID="btnImportWarehousing" runat="server" CssClass="importWareHousing_bt"
                    TabIndex="7" OnClick="btnImportWarehousing_Click" />
            </div>
            <div class="container1-bt">
                <asp:Button ID="btnImportAutomation" runat="server"
                    CssClass="importAutomation_bt" TabIndex="5"
                    OnClick="btnImportAutomation_Click" />
            </div>
            <div class="container2-bt">
                <asp:Button ID="btnSwiftSTP" runat="server" CssClass="btnSwiftSTP_bt"
                    TabIndex="6" onclick="btnSwiftSTP_Click"  />
            </div>
        </div>
        <div class="footer">
            <span class="h2">&copy;&nbsp;2018 Lateral Management Computer Consultants</span>
        </div>
    </div>
    </form>
</body>
</html>
