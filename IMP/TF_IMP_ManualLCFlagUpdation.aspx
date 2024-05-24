<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_ManualLCFlagUpdation.aspx.cs"
    Inherits="IMP_TF_IMP_Miscellaneous" %>

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
            if ($("#TabContainerMain_tbLCDiscounting_txtFromDate").val() == '') {
                alert('Please Enter From Date.');
                $("#TabContainerMain_tbLCDiscounting_txtFromDate").focus();
                return false;
            }
            else {
                var Fromdate = $("#TabContainerMain_tbLCDiscounting_txtFromDate").val();
            }
            if ($("#TabContainerMain_tbLCDiscounting_txtToDate").val() == '') {
                alert('Please Enter To Date.');
                $("#TabContainerMain_tbLCDiscounting_txtToDate").focus();
                return false;
            }
            else {
                var ToDate = $("#TabContainerMain_tbLCDiscounting_txtToDate").val();
            }
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../IMP/HelpForms/TF_IMP_ManualLCUpdateDocNoHelp.aspx?Fromdate=' + Fromdate + '&ToDate=' + ToDate, 400, 500, 'DocNoList');
                return false;
            }
        }
        function selectDocNo(DocNo, OwnLCDiscType) {
            $("#TabContainerMain_tbLCDiscounting_txtDocumentNo").val(DocNo);
            $("#OWNLCValue").val(OwnLCDiscType);
            __doPostBack('txtDocumentNo', '');
            return true;
        }
        function ValidateUpdate() {
            if ($("#TabContainerMain_tbLCDiscounting_txtFromDate").val() == '') {
                alert('Please Enter From Date.');
                $("#TabContainerMain_tbLCDiscounting_txtFromDate").focus();
                return false;
            }
            if ($("#TabContainerMain_tbLCDiscounting_txtToDate").val() == '') {
                alert('Please Enter To Date.');
                $("#TabContainerMain_tbLCDiscounting_txtToDate").focus();
                return false;
            }

            if ($("#TabContainerMain_tbLCDiscounting_txtDocumentNo").val() == '') {
                alert('Document No can not be blank.');
                $("#TabContainerMain_tbLCDiscounting_txtDocumentNo").focus();
                return false;
            }
            var OWNLC='';
            if ($("#TabContainerMain_tbLCDiscounting_rdb_ownLCDiscount_Yes").is(':checked')) {
                OWNLC = 'Y';
            }
            if ($("#TabContainerMain_tbLCDiscounting_rdb_ownLCDiscount_No").is(':checked')) {
                OWNLC = 'N';
            }
            if ($("#OWNLCValue").val() == OWNLC) {
                alert('Please change LC Discounting.');
                return false;
            }
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnDeleteConfirm" />--%>
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <input type="hidden" runat="server" id="hdnholidaymaster" />
                                <span class="pageLabel"><strong>Manual LC Flag Updation</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <%--<asp:Button ID="btnDeleteConfirm" Style="display: none;" runat="server" OnClick="btnDeleteConfirm_Click" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <input type="hidden" runat="server" id="OWNLCValue" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                    CssClass="ajax__tab_xp-theme">
                                    <ajaxToolkit:TabPanel ID="tbLCDiscounting" runat="server" HeaderText=""
                                        Font-Bold="true" ForeColor="White"><ContentTemplate>
                                            <table width="70%">
                                            <tr>
                                            <td align="right">
                            <span class="elementLabel">From Lodgment Date :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="2"></asp:TextBox>

                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True"></ajaxToolkit:MaskedEditExtender>

                                <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />

                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="Button1" Enabled="True"></ajaxToolkit:CalendarExtender>

                                &nbsp;&nbsp; <span class="elementLabel">To Lodgment Date :</span>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="3"></asp:TextBox>

                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtToDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True"></ajaxToolkit:MaskedEditExtender>

                                <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />

                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="Button2" Enabled="True"></ajaxToolkit:CalendarExtender>

                            </td>
                        </tr>
                                                <tr>
                                                    <td width="15%" align="right">
                                                        <span class="elementLabel">Document No :</span>
                                                    </td>
                                                    <td width="30%" align="left">
                                                        <asp:TextBox ID="txtDocumentNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                         OnTextChanged="txtDocumentNo_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                        <asp:Button ID="btnDocNoList" runat="server" ToolTip="Press for Customers list."
                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenDocNoHelp('mouseClick');" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Change LC Discounting To :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:RadioButton ID="rdb_ownLCDiscount_Yes" Text="Yes" CssClass="elementLabel" runat="server"
                                                            GroupName="ownLCDiscount" TabIndex="54" />

                                                        <asp:RadioButton ID="rdb_ownLCDiscount_No" Text="No" CssClass="elementLabel" runat="server"
                                                            GroupName="ownLCDiscount" TabIndex="54" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="buttonDefault"
                                                            ToolTip="Update" OnClick="btnUpdate_Click" OnClientClick="return ValidateUpdate();"/>

                                                    </td>
                                                </tr>
                                            </table>
                                        
</ContentTemplate>
</ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
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
