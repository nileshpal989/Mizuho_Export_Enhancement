<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_SwiftFreeFormat.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_SwiftFreeFormat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Scripts/TF_IMP_SwiftFreeFormat.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        /*
        $(document).ready(function () {
        // Configure to save every 5 seconds
        window.setInterval(SaveUpdateData, 5000); //calling saveDraft function for every 5 seconds
        });
        */
        $(document).ready(function (e) {
            $("input").keypress(function (e) {
                var k;
                document.all ? k = e.keyCode : k = e.which;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 47 || k == 45 || k == 46 || k == 44 || (k >= 48 && k <= 58));
            });
        });

    </script>
    <style type="text/css">
        .textBox
        {
            text-transform: uppercase;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <table id="tbl_Header" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>
                                <asp:Label ID="lbl_Header" runat="server"></asp:Label>
                            </strong></span>
                        </td>
                        <td align="right" style="width: 50%" valign="bottom">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="10px">
                    <asp:Panel ID="Panal_FreeFormatSwift" runat="server">
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">Branch</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" Height="25px" Width="100px" CssClass="dropdownList"
                                    TabIndex="1" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right">
                                <span class="mandatoryField">Swift Types</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSwiftTypes" Height="25px" Width="100px" CssClass="dropdownList"
                                    TabIndex="1" runat="server">
                                    <asp:ListItem Text="----Select----" Value="" />
                                    <asp:ListItem Text="MT199" Value="MT199" />
                                    <asp:ListItem Text="MT299" Value="MT299" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="20%">
                                <span class="mandatoryField">Receiver</span>
                            </td>
                            <td align="left" width="80%">
                                <asp:TextBox ID="txt_Receiver" runat="server" MaxLength="16" CssClass="textBox" TabIndex="1"></asp:TextBox>
                                <asp:Button ID="btnReceiverBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                    OnClientClick="return OpenNego_Remit_BankList('mouseClick');" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">[20] Transaction Reference Number</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_TransRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                    TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">[21] Related Reference</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_RelRef" runat="server" CssClass="textBox" MaxLength="16" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td align="right" valign="top">
                            <span class="mandatoryField">[79] Narrative</span>
                        </td>
                        <td align="left">
                            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                TargetControlID="panel_AddNarrative" CollapsedSize="0" ExpandedSize="200" ScrollContents="True"
                                Enabled="True" />
                            <asp:Panel ID="panel_AddNarrative" runat="server">
                                <table width="100%">
                                    <asp:Panel ID="Panal_FreeFormatNarrative" runat="server">
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">1.</span>
                                                <asp:TextBox ID="Narrative1" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">2.</span>
                                                <asp:TextBox ID="Narrative2" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">3.</span>
                                                <asp:TextBox ID="Narrative3" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">4.</span>
                                                <asp:TextBox ID="Narrative4" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">5.</span>
                                                <asp:TextBox ID="Narrative5" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">6.</span>
                                                <asp:TextBox ID="Narrative6" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">7.</span>
                                                <asp:TextBox ID="Narrative7" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">8.</span>
                                                <asp:TextBox ID="Narrative8" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">9.</span>
                                                <asp:TextBox ID="Narrative9" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">10.</span>
                                                <asp:TextBox ID="Narrative10" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">11.</span>
                                                <asp:TextBox ID="Narrative11" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">12.</span>
                                                <asp:TextBox ID="Narrative12" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">13.</span>
                                                <asp:TextBox ID="Narrative13" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">14.</span>
                                                <asp:TextBox ID="Narrative14" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">15.</span>
                                                <asp:TextBox ID="Narrative15" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">16.</span>
                                                <asp:TextBox ID="Narrative16" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">17.</span>
                                                <asp:TextBox ID="Narrative17" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">18.</span>
                                                <asp:TextBox ID="Narrative18" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">19.</span>
                                                <asp:TextBox ID="Narrative19" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">20.</span>
                                                <asp:TextBox ID="Narrative20" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">21.</span>
                                                <asp:TextBox ID="Narrative21" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">22.</span>
                                                <asp:TextBox ID="Narrative22" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">23.</span>
                                                <asp:TextBox ID="Narrative23" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">24.</span>
                                                <asp:TextBox ID="Narrative24" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">25.</span>
                                                <asp:TextBox ID="Narrative25" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">26.</span>
                                                <asp:TextBox ID="Narrative26" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">27.</span>
                                                <asp:TextBox ID="Narrative27" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">28.</span>
                                                <asp:TextBox ID="Narrative28" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">29.</span>
                                                <asp:TextBox ID="Narrative29" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">30.</span>
                                                <asp:TextBox ID="Narrative30" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">31.</span>
                                                <asp:TextBox ID="Narrative31" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">32.</span>
                                                <asp:TextBox ID="Narrative32" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">33.</span>
                                                <asp:TextBox ID="Narrative33" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">34.</span>
                                                <asp:TextBox ID="Narrative34" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span class="elementLabel">35.</span>
                                                <asp:TextBox ID="Narrative35" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                    TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                        </td>
                        <td align="left" width="80%">
                            <asp:Button ID="btn_Generate_Swift" runat="server" Text="View SWIFT Message" CssClass="buttonDefault"
                                Width="150px" ToolTip="View SWIFT Message" TabIndex="5" OnClientClick="return SubmitCheck();"
                                OnClick="btn_Generate_Swift_Click" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit to Checker" CssClass="buttonDefault"
                                Width="150px" ToolTip="Submit to Checker" TabIndex="5" OnClick="btnSubmit_Click"
                                OnClientClick="return SubmitCheck();" />
                            <span id="Span_ApproveReject" runat="server"><span class="elementLabel">Approve / Reject
                                :</span>
                                <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="5"
                                    OnChange="return DialogAlert();">
                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnApprove" Style="visibility: hidden" runat="server" CssClass="buttonDefault"
                                    ToolTip="Save" OnClick="btnApprove_Click" />
                            </span>
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
