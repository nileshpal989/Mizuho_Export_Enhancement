<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_RollBackLodgement.aspx.cs"
    Inherits="IMP_TF_IMP_RollBackLodgement" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OpenDocNoHelp(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../IMP/HelpForms/TF_IMP_BOE_RollBack_Help.aspx', 400, 500, 'DocNoList');
                return false;
            }
        }
        function selectDocNo(DocNo) {
            $("#txtDocumentNo").val(DocNo);
            return true;
        }
        function ValidateSave() {
            if ($("#txtDocumentNo").val() == '') {
                alert('Document No can not be blank.');
                $("#txtDocumentNo").focus();
                return false;
            }
            var RollBack = '';
            if ($("#rdb_Yes").is(':checked')) {
                RollBack = 'Y';
            }
            if ($("#rdb_No").is(':checked')) {
                RollBack = 'N';

                alert('You Dont want to RollBack the Document.');
                $("#txtDocumentNo").val('');
                $("#txtDocumentNo").focus();
                return false;
            }
            if (RollBack == '') {
                alert('Please select Yes / No.');
                return false;
            }
            return true;
        }
    </script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Logdement Rollback</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back to Home page"
                                    TabIndex="11" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                              <hr />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td width="10%" align="right">
                                <span class="elementLabel">Document No:</span>
                            </td>
                            <td width="90%" align="left">
                                <asp:TextBox ID="txtDocumentNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1" Enabled="false"></asp:TextBox>
                                <asp:Button ID="btnDocNoList" runat="server" ToolTip="Help for Doc.list."
                                    CssClass="btnHelp_enabled" OnClientClick="return OpenDocNoHelp('mouseClick');" />
                            </td>
                        </tr>
						<tr>
                            <td align="right">
                            </td>
                            <td align="left">
                            <span class="mandatoryField">Note: Document Help shows only Approved Logdement Bill Which not proceed further procedure.</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Rollback:</span>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rdb_Yes" Text="Yes" CssClass="elementLabel" runat="server"
                                    GroupName="Rollback" TabIndex="2" />
                                <asp:RadioButton ID="rdb_No" Text="No" CssClass="elementLabel" runat="server"
                                    GroupName="Rollback" TabIndex="2" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" TabIndex="2"
                                    ToolTip="Move Bill Loadgement to Maker Queue" OnClick="btnSave_Click" OnClientClick="return ValidateSave();" />
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
