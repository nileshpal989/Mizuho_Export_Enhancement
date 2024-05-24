<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditCommodityMaster.aspx.cs"
    Inherits="TF_AddEditCommodityMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="stylesheet" type="text/css" />
      <script src="Help_Plugins/MyJquery1.js" type="text/javascript"></script> <%--snackbar--%>
    <script src="Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="Help_Plugins/jquery-ui.js" type="text/javascript"></script>
        <link href="Help_Plugins/JueryUI.css" rel="stylesheet" type="text/css" />
    <script src="Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
        function validateSave() {

            var txtCommodityID = document.getElementById('txtCommodityID');
            var txtCommodityName = document.getElementById('txtCommodityName');

            if (txtCommodityID.value == '') {
//                alert('Enter Commodity ID.');
                //                txtCommodityID.focus();
                VAlert('Enter Commodity ID.', '#txtCommodityID');
                return false;
            }
            if (txtCommodityName.value == '') {
//                alert('Enter Commodity Name.');
                //                txtCommodityName.focus();
                VAlert('Enter Commodity Name.', '#txtCommodityName');
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
     <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                            <input type="hidden" runat="server" id="hdncommdity" />
                                <span class="pageLabel"><strong> Commodity Master Details</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" TabIndex="6" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" Font-Bold="true" Font-Size="Small" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Commodity ID :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCommodityID" runat="server" CssClass="textBox" MaxLength="3"
                                                Width="60" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Name :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCommodityName" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="300px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="4" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="5" />
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
