<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_InquiryOfLedgerFile_Maker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_InquiryOfLedgerFile_Maker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Scripts/TF_IMP_InquiryOfLedgerFile_Maker.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        $(document).ready(function () {
            // Configure to save every 5 seconds  
            window.setInterval(SaveUpdateData, 5000); //calling saveDraft function for every 5 seconds  
        });
        $(document).ready(function (e) {
            $("input").keypress(function (e) {
                var k;
                document.all ? k = e.keyCode : k = e.which;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 47 || k == 45 || k == 46 || k == 44 || (k >= 48 && k <= 57));
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
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="tbl_Header" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" valign="bottom" colspan="2">
                            <span class="pageLabel"><strong>Inquiry of Ledger File - Maker</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                        <asp:HiddenField ID="hdnDueDate" runat="server" />
                            <hr />
                        </td>
                    </tr>
                    <tr align="right">
                        <td width="15%" align="left" colspan="2">
                            <span class="elementLabel">Branch :</span>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Rrefence Number :</span>
                            <asp:TextBox ID="txtDocNo" Width="140px" runat="server" CssClass="textBox" TabIndex="1"
                                AutoPostBack="true" OnTextChanged="txtDocNo_OnTextChanged"></asp:TextBox>
                            <asp:Button ID="btnDocNoList" runat="server" ToolTip="Press for Customers list."
                                CssClass="btnHelp_enabled" OnClientClick="return OpenDocNoHelp('mouseClick');" />
                            <asp:Label ID="lblRejectReason" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr align="right">
                        <td width="30%" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="left">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbMoneyMarket" runat="server" HeaderText="Money Market"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="70%">
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Customer Name :</span>
                                                </td>
                                                <td width="30%" align="left" colspan="3">
                                                    <asp:TextBox ID="txtCustomerNameMK" Width="310px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Account Code :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtAccountCodeMK" Width="50px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Currency :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtCurrencyMK" Width="30px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Reference Number :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtReferenceNumberMK" Width="140px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"> </asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">System Code :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtSystemCodeMK" Width="20px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Front No. :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtFrontNoMK" Width="140px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Nos. Of Days :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtNosOfDaysMK" Width="30px" runat="server" CssClass="textBox" TabIndex="1"
                                                        onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestMK" Width="50px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Final Due Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtFinalDueDateMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_FinalDueDateMK" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtFinalDueDateMK" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_FinalDueDateMK" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFinalDueDateMK" PopupButtonID="btncal_FinalDueDateMK" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="ME_FinalDueDateMK"
                                                        ValidationGroup="dtVal" ControlToValidate="txtFinalDueDateMK" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Spread :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtSpreadMK" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Base Rate :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtBaseRateMK" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Funds :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtFundsMK" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">Settlement Method :</span>
                                                    <asp:TextBox ID="txtSettlementMethodMK" Width="30px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Our Dipository :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtOurDipositoryMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Customer Abbr :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtCustomerAbbrMK" Width="120px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Contra Account :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtContraAccountMK" Width="50px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Account No. :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtAccountNoMK" Width="200px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Their Dipository :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtTheirDipository1MK" Width="150px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtTheirDipository2MK" Width="150px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <asp:TextBox ID="txtTheirDipository3MK" Width="150px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtTheirDipository4MK" Width="150px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Their Account :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtTheirAccountMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtATTNMK" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Current Balance :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtCurrentBalanceMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Value Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtValueDateMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtValueDateMK" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Operation Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtOperationDateMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtOperationDateMK" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">(Settlement) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtSettlement1MK" Width="140px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">(Settlement) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtSettlement2MK" Width="140px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Last Modification :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtLastModificationMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Roll Over No. :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtRollOverNoMK" Width="30px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">Last TRNS No. :</span>
                                                    <asp:TextBox ID="txtLastTRNSNoMK" Width="50px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Last Operation :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtLastOperationMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Rem(EUC) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtRemEUCMK" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Status :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtStatusMK" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Over Due Accrue(Y/) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtOverDueAccrueMK" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">Entity :</span>
                                                    <asp:TextBox ID="txtEntityMK" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:Button ID="btnNext" runat="server" Text="Go To Roll Over" Width="150px" CssClass="buttonDefault"
                                                        OnClick="btnNext_Click" ToolTip="Go To Roll Over" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbRollOver" runat="server" HeaderText="Roll Over" Font-Bold="true"
                                    ForeColor="White">
                                    <ContentTemplate>
                                        <table width="70%">
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Customer Name :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtCustomerNameRO" Width="310px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Account Code :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtAccountCodeRO" Width="50px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Currency :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtCurrencyRO" Width="30px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Reference No :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtReferenceNoRO" Width="140px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Division Code :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtDivisionCodeRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Front No. :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtFrontNoRO" Width="140px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Pledged :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtPledgedRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Roll Over No. :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtRollOverNoRO" Width="30px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Operation Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtOperationDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtOperationDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Value Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtValueDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtValueDateRO" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Due Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtDueDateRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_DueDateRO" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtDueDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_DueDateRO" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CE_DueDateRO" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDueDateRO" PopupButtonID="btncal_DueDateRO" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MV_DueDateRO" runat="server" ControlExtender="ME_DueDateRO"
                                                        ValidationGroup="dtVal" ControlToValidate="txtDueDateRO" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    <span class="elementLabel">Days :</span>
                                                    <asp:TextBox ID="txtDaysRO" Width="30px" runat="server" CssClass="textBox" TabIndex="1"
                                                        onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Amount :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtAmountRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">(Prime CCY) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtPrimeCCYRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest Rate(1) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestRate1RO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest Rate(2) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestRate2RO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest Amount :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestAmountRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Days / A Year :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtDaysAYearRO" Width="20px" runat="server" CssClass="textBox" TabIndex="1"
                                                        onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Base Rate :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtBaseRateRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Spread :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtSpreadRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Overdue R/O (Y/) :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtOverdueRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest Payer :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestPayerRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">Non Accrue(Y/) :</span>
                                                    <asp:TextBox ID="txtNonAccrueRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest Operation Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestOperationDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtInterestOperationDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest Value Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestValueDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtInterestValueDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Settlement Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtSettlementDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtSettlementDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Settlement Value Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtSettlementValueDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtSettlementValueDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Last Modification Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtLastModificationDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLastModificationDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Last Operation Date :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtLastOperationDateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLastOperationDateRO" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Transaction No. :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtTransactionNoRO" Width="50px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Realized Interest Total :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtRealizedInterestTotalRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Fund Type :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtFundTypeRO" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Internal Rate :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInternalRateRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Interest Rate Change No :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtInterestRateChangeNoRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" onkeydown="return validate_Number(event);" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td width="15%" align="right">
                                                    <span class="elementLabel">Status Code :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtStatusCodeRO" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="1" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:Button ID="btnPrev" runat="server" Text="Go To Money Market" Width="150px" CssClass="buttonDefault"
                                                        OnClick="btnPrev_Click" ToolTip="Go To Money Market" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="left" valign="top">
                            <hr />
                        </td>
                    </tr>
                </table>
                <table width="70%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonDefault"
                                ToolTip="Submit to Checker" OnClientClick="return SubmitValidation();" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
