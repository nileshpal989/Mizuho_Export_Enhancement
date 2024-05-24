<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST1.aspx.cs" Inherits="ImportWareHousing_Transactions_TEST1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Test/jquery-1.8.3.min.js"></script>
    <script language="javascript" type="text/javascript">

        function OpenCustomerCodeList(e, srno) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 13 || e == 'mouseClick') {
                var txtCustAcNo = $get("TabContainerMain_tbDocumentDetails_txtCustAcNo");
                popup = window.open('../HelpForms/TF_IMPW_Help_Customer.aspx?srno=' + 1, 'helpCountryId', 'height=500,  width=500,status= no, resizable= yes, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
                return false;
            }
        }
        function selectCustomer(CustACNo, srno) {
            document.getElementById('txtCustACNo').value = CustACNo;
            __doPostBack('txtCustACNo', '');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <script type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <script type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Supplier Master Data Entry</strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="Button1" runat="server" Text="Back" CssClass="buttonDefault" Font-Bold="true"
                                ToolTip="Back" TabIndex="16" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%" valign="top">
                            <br />
                        </td>
                        <td align="right" style="width: 50%" valign="top">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-top: 2px solid #49A3FF; border-bottom: 2px solid #49A3FF;
                            padding: 5px 5px 5px 5px" valign="middle" colspan="2">
                            <table cellspacing="4px" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td align="right" style="width: 10%">
                                        <span class="elementLabel">Branch :&nbsp;</span>
                                    </td>
                                    <td align="left" style="width: 90%">
                                        <asp:TextBox ID="txtAdCode" runat="server" Enabled="false"  MaxLength="20"
                                            Width="60PX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 10%;">
                                        <span class="mandatoryField">*</span><span class="elementLabel">Cust A/C No. :&nbsp;
                                        </span>
                                    </td>
                                    <td style="width: 90%; text-align: left">
                                        <asp:TextBox ID="txtCustACNo" runat="server" CssClass="textBox" ToolTip="Enter Customer A/C No &#013;Press Enter For HELP"
                                            MaxLength="20" Width="140PX" AutoPostBack="true" TabIndex="1" placeholder="Enter Customer A/C No."
                                            OnTextChanged="txtCustACNo_TextChanged" onkeydown="OpenCustomerCodeList(this,'1');"
                                            required></asp:TextBox>
                                        <asp:Button ID="btnCustomerList" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                            onkeydown="OpenCustomerCodeList(this,'1');" ToolTip="Press Enter for help." />
                                        <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel" Width="300px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
