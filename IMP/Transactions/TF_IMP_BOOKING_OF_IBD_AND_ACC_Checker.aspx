<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Scripts/TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker.js" type="text/javascript"></script>
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <table id="tbl_Header" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Import Bill Acceptance - Checker</strong></span>
                        </td>
                        <td align="right" style="width: 50%" valign="bottom">
                        <asp:Label ID="lbl_LEIAmt_Check" runat="server" CssClass="mandatoryField"></asp:Label>
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%" valign="bottom" colspan="2">
                        <asp:Label ID="ReccuringLEI" runat="server" CssClass="mandatoryField" Visible="false"></asp:Label>
                        <asp:Label ID="LeiVerified" runat="server" CssClass="mandatoryField" Visible="false"></asp:Label>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            <%-------------------------  hidden fields  --------------------------------%>
                            <input type="hidden" id="hdnBranchCode" runat="server" />
                            <input type="hidden" id="hdnDocType" runat="server" />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnDocScrutiny" runat="server" />
                            <input type="hidden" id="hdnDocNo" runat="server" />
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnNegoRemiBankType" runat="server" />
                            <input type="hidden" id="hdnUserName" runat="server" />
                            <%-------Added by Bhupen for LEI on 15102022----------------%>
                            <input type="hidden" id="hdnleino" runat="server" />
                            <input type="hidden" id="hdnleiexpiry" runat="server" />
                            <input type="hidden" id="hdnDraweeleino" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry" runat="server" />
                            <input type="hidden" id="hdnLeiFlag" runat="server" />
                            <input type="hidden" id="hdnleiexpiry1" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry1" runat="server" />
                            <input type="hidden" id="hdnDrawer" runat="server" />
                            <input type="hidden" id="hdnDrawerno" runat="server" />
                            <input type="hidden" id="hdnCustAbbr" runat="server" />
                            <input type="hidden" id="hdncustleiflag" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tbl_Acceptance" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="10%">
                            <span class="elementLabel">Trans. Ref. No :</span>
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtDocNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="lblForeign_Local" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblCollection_Lodgment_UnderLC" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSight_Usance" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td width="60%" align="left">
                            <span class="elementLabel">Val.Date :</span>
                            <asp:TextBox ID="txtValueDate" runat="server" TabIndex="2" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="ME_Value_Date" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":">
                            </ajaxToolkit:MaskedEditExtender>
                            <span class="elementLabel">CCY : </span>
                            <asp:Label ID="lblDoc_Curr" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <span class="elementLabel">Bill Amt : </span>
                            <asp:Label ID="lblBillAmt" runat="server" CssClass="elementLabel"></asp:Label>
                         <%---------Added by bhupen for lei on 14112022-----------%>
                            <span class="elementLabel">Exch Rate : </span>
                            <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                        <%----------------------------------END---------------------------------%>
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="3">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbDocumentLedger" runat="server" HeaderText="LEDGER FILE"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" width="10%"> 
                                                    
                                                </td>
                                                <td align="left" colspan="6">
                                                    <asp:CheckBox ID="chk_Ledger_Modify" Text="LEDGER FILE" runat="server" CssClass="elementLabel"
                                                        TabIndex="8" Enable="false"/>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="PanelLedgerFile" runat="server" Visible="false">
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">CUSTOMER NAME :</span>
                                                </td>
                                                <td align="left" colspan="5">
                                                <asp:TextBox ID="txtLedgerCustName" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="5" Width="300px"></asp:TextBox>
                                                        </td>
                                            </tr>
                                            <tr>
                                            <td align="right" width="10%">
                                                    <span class="elementLabel">A/C CODE :</span>
                                                </td>
                                                <td align="left" width="20%">
                                                    <asp:TextBox ID="txtLedgerAccode" runat="server" CssClass="textBox" MaxLength="5"
                                                        TabIndex="22" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">CURRENCY :</span>
                                                </td>
                                                <td align="left" width="20%" >
                                                    <asp:TextBox ID="txtLedgerCURR" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="4" Width="50px"></asp:TextBox>
                                                </td >
                                                <td align="right" width="10%"><span class="elementLabel">REMARKS :</span></td>
                                                <td align="left" width="30%" >
                                                <asp:TextBox ID="txtLedgerRemark" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="5" Width="300px"></asp:TextBox>
                                                        </td>
                                                </tr>
                                                <tr>
                                                <td align="right" >
                                                <span class="elementLabel">REFERENCE NO :</span>
                                                </td>
                                                <td align="left">
                                                <asp:TextBox ID="txtLedgerRefNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1"  MaxLength="14"></asp:TextBox>
                                                </td>
                                                <td align="right" >
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtLedger_Modify_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);" onchange="return Ledger_Amt_validation();"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td align="right" >
                                                    <span class="elementLabel">OPERATION DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerOperationDate" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="7" Width="70px"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerOperationDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgerOperationDate" runat="server" CssClass="btncalendar_enabled" Enabled="false"
                                                        />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerOperationDate" PopupButtonID="btnCal_LedgerOperationDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">BALANCE :</span>
                                                </td>
                                                <td align="left">
                                                        <asp:TextBox ID="txtLedgerBalanceAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td align="right" >
                                                    <span class="elementLabel">ACCEPT DATE :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtLedgerAcceptDate" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="7" Width="70px"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerAcceptDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txtLedgerAcceptDate" runat="server" CssClass="btncalendar_enabled" Enabled="false"
                                                        />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerAcceptDate" PopupButtonID="btnCal_txtLedgerAcceptDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">MATURITY :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtLedgerMaturity" runat="server" CssClass="textBox" MaxLength="10" TabIndex="12"
                                                        Width="70px" ></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerMaturity" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txtLedgerMaturity" runat="server" CssClass="btncalendar_enabled" Enabled="false"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerMaturity" PopupButtonID="btnCal_txtLedgerMaturity" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTLEMENT DATE :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtLedgerSettlememtDate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="12"
                                                        Width="70px" ></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerSettlememtDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgerSettlememtDate" runat="server" CssClass="btncalendar_enabled" Enabled="false"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerSettlememtDate" PopupButtonID="btnCal_LedgerSettlememtDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL. VALUE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerSettlValue" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">LAST MOD. DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerLastModDate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="12"
                                                        Width="70px" ></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerLastModDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgerLastModDate" runat="server" CssClass="btncalendar_enabled" Enabled="false"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerLastModDate" PopupButtonID="btnCal_LedgerLastModDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">REM(EUC) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerREM_EUC" runat="server" CssClass="textBox" MaxLength="20" TabIndex="16"
                                                        Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">LAST OPE. DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerLastOPEDate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="12"
                                                        Width="70px" ></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerLastOPEDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgeRLastOPEDate" runat="server" CssClass="btncalendar_enabled" Enabled="false"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerLastOPEDate" PopupButtonID="btnCal_LedgeRLastOPEDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">TRANS. NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerTransNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1"  MaxLength="14"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CONTRA COUNTRY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerContraCountry" runat="server" CssClass="textBox" TabIndex="20"
                                                        MaxLength="3" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">STATUS CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerStatusCode" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="21" Width="140px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COLLECT. OF COMM :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerCollectOfComm" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">COMMODITY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerCommodity" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="21" Width="140px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">A/C BK :</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMISSION : </span>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">RATE</span>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">CCY</span>
                                                </td>
                                                <td align="left">
                                                     <span class="elementLabel">AMOUNT</span>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">PAYER</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">HANDLING COMM :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerhandlingCommRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerhandlingCommCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerhandlingCommAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerhandlingCommPayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">POSTAGE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerPostageRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerPostageCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerPostageAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerPostagePayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                           <tr>
                                                <td align="right">
                                                    <span class="elementLabel">OTHERS :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerOthersRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerOthersCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerOthersAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerOthersPayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">THEIR COMM. :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerTheirCommRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerTheirCommCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerTheirCommAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerTheirCommPayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COLLECT/NEGO BANK :</span>
                                                </td>
                                                <td align="left" colspan="6">
                                                    <asp:TextBox ID="txtLedgerNegoBank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lblLedgerNegoBank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">REIMBURSING BANK :</span>
                                                </td>
                                                <td align="left" colspan="6">
                                                    <asp:TextBox ID="txtLedgerReimbursingBank" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="39" MaxLength="12"></asp:TextBox>
                                                    <asp:Label ID="lblLedgerReimbursingBank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td  align="right">
                                                    <span class="elementLabel">DRAWER/DRAWEE NAME:</span>
                                                </td>
                                                <td  align="left">
                                                    <asp:TextBox ID="txtLedgerDrawer" runat="server" CssClass="textBox" MaxLength="70" TabIndex="51"
                                                        Width="300px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">TENOR :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtLedgerTenor" Width="45px" runat="server" CssClass="textBox" TabIndex="53"
                                                        MaxLength="3"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtLedgerAttn" Width="350px" runat="server" CssClass="textBox" MaxLength="70"
                                                        TabIndex="41" ></asp:TextBox>
                                                </td>
                                            </tr></asp:Panel>
                                            <tr>
                                                <td align="center" >
                                                </td>
                                                <td >
                                                    <asp:Button ID="btnLedgerNext" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                        TabIndex="50" ToolTip="Go to DOCUMENT DETAILS" OnClick="btnLedgerNext_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="DOCUMENT DETAILS"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CUSTOMER :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtCustomer_ID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                        TabIndex="3" MaxLength="20" Width="100px" OnTextChanged="txtCustomer_ID_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">HO APL :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtHO_Apl" runat="server" CssClass="textBox" MaxLength="9" TabIndex="4"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">IBD/ACC KIND :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txtIBD_ACC_kind" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="5" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                </td>
                                                <td align="left" width="10%">
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">COMMENT CODE :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txtCommentCode" runat="server" CssClass="textBox" MaxLength="2"
                                                        TabIndex="6" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">AUTO STTL :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:TextBox ID="txtAutoSettlement" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="7" Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">DRAFT AMT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDraftAmt" runat="server" CssClass="textBox" MaxLength="16" Width="120px"
                                                        TabIndex="8" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    
                                                    
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">CCY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Doc_Curr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">L/C NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLCNo" runat="server" CssClass="textBox" MaxLength="9" TabIndex="10"
                                                        Width="90px"></asp:TextBox>
                                                    <span class="elementLabel">LC Balance : </span>
                                                    <asp:Label ID="lblLC_Balance" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CONTRACT NO :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtContractNo" runat="server" CssClass="textBox" MaxLength="9" TabIndex="11"
                                                        Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">EXCH RATE :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtExchRate" runat="server" CssClass="textBox" MaxLength="11" TabIndex="12"
                                                        Width="120px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">IBD AMT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtIBDAmt" runat="server" CssClass="textBox" MaxLength="16" Width="120px"
                                                        TabIndex="13" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">COUNTRY RISK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCountryRisk" runat="server" CssClass="textBox" MaxLength="2"
                                                        TabIndex="14" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">RISK CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtRiskCust" runat="server" CssClass="textBox" MaxLength="12" TabIndex="15"
                                                        Width="140px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">GRADE CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGradeCode" runat="server" CssClass="textBox" MaxLength="2" TabIndex="16"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">APPL. NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtApplNo" runat="server" CssClass="textBox" MaxLength="7" TabIndex="17"
                                                        Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">APPL BR :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtApplBR" runat="server" CssClass="textBox" MaxLength="3" TabIndex="18"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">PURPOSE :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtPurpose" runat="server" CssClass="textBox" MaxLength="7" TabIndex="19"
                                                        Width="10px"></asp:TextBox>
                                                    <span class="elementLabel">PURPOSE CODE:</span>
                                                    <asp:DropDownList ID="ddl_PurposeCode" runat="server" CssClass="dropdownList" MaxLength="3"
                                                        TabIndex="23" Width="70px" AutoPostBack="True" OnSelectedIndexChanged="ddl_PurposeCode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lbl_PurposeCodeDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlCodeForCust" runat="server" CssClass="textBox" TabIndex="20"
                                                        MaxLength="7" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ABBR :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlforCust_Abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="21" Width="140px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlforCust_AccCode" runat="server" CssClass="textBox" MaxLength="5"
                                                        TabIndex="22" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlforCust_AccNo" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="23" Width="140px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST FROM :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_From" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="24" ValidationGroup="dtVal" Width="70px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtInterest_From" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST TO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_To" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="25" ValidationGroup="dtVal" Width="70px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtInterest_To" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">NO. OF DAYS :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txt_No_Of_Days" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="26" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INT RATE(%) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_INT_Rate" runat="server" CssClass="textBox" MaxLength="10" Width="70px"
                                                        TabIndex="27" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">BASE RATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtBaseRate" runat="server" CssClass="textBox" MaxLength="2" Width="70px"
                                                        TabIndex="28" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">SPREAD(%) :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtSpread" runat="server" CssClass="textBox" MaxLength="9" Width="70px"
                                                        TabIndex="29" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INT. AMT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterestAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="30" Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">FUND TYPE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtFundType" runat="server" CssClass="textBox" MaxLength="1" Width="20px"
                                                        TabIndex="31"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">INTERNAL RATE :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtInternalRate" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="32" Width="70px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR BANK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_CodeForBank" Width="50px" runat="server" CssClass="textBox"
                                                        TabIndex="33" MaxLength="12"></asp:TextBox>
                                                    <asp:Label ID="lblSettl_ForBank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ABBR :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_ForBank_Abbr" Width="80px" runat="server" CssClass="textBox"
                                                        TabIndex="34" MaxLength="12"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddl_Settl_ForBank_Abbr" runat="server" CssClass="dropdownList"
                                                        TabIndex="34" Width="80px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Settl_ForBank_Abbr_SelectedIndexChanged">
                                                    </asp:DropDownList>--%>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_ForBank_AccCode" runat="server" CssClass="textBox" MaxLength="5"
                                                        TabIndex="35" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_ForBank_AccNo" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="36" Width="140px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">NEGOTIATING BANK TYPE :</span>
                                                </td>
                                                <td align="left" colspan="7">
                                                    <asp:TextBox ID="txtNego_Remit_Bank_Type" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                    &nbsp;&nbsp;<span class="elementLabel">NEGOTIATING BANK :</span>
                                                    <asp:TextBox ID="txtNego_Remit_Bank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lbl_Nego_Remit_Bank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">ACC. WITH BANK :</span>
                                                </td>
                                                <td align="left" colspan="7">
                                                    <asp:TextBox ID="txtAcwithInstitution" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="38" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lblAcWithInstiBankDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">REIMBURSING BANK :</span>
                                                </td>
                                                <td align="left" colspan="7">
                                                    <asp:TextBox ID="txtReimbursingbank" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="39" MaxLength="12"></asp:TextBox>
                                                    <asp:Label ID="lbl_Reimbursingbank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">REM(EUC) :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtREM_EUC" Width="200px" runat="server" CssClass="textBox" TabIndex="40"
                                                        MaxLength="20"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtAttn" Width="350px" runat="server" CssClass="textBox" MaxLength="70"
                                                        TabIndex="41"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                </td>
                                                <td colspan="6">
                                                <asp:Button ID="btnDocPrev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="GoTo LEDGER FILE" TabIndex="256" OnClick="btnDocPrev_Click" />
                                                <asp:Button ID="btnDocNext" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                        TabIndex="50" ToolTip="Go to Instructions" OnClick="btnDocNext_Click" />
                                                <asp:Button ID="btn_Verify" runat="server" Text="LEIVerify" CssClass="buttonDefault"
                                                        ToolTip="Click here to verify LEI Details" Visible="false" TabIndex="50" 
                                                        OnClick="btn_Verify_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentInstructions" runat="server" HeaderText="INSTRUCTIONS"
                                    Font-Bold="true" ForeColor="Lime">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                            <tr>
                                                <td width="10%" align="right">
                                                    <span class="elementLabel">DRAWER :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtDrawer" runat="server" CssClass="textBox" MaxLength="70" TabIndex="51"
                                                        Width="300px"></asp:TextBox>
                                                </td>
                                                <td width="10%" align="right">
                                                </td>
                                                <td width="50%" align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">TENOR :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="ddlTenor" runat="server" CssClass="dropdownList" TabIndex="52">
                                                        <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="SIGHT"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="DEF Pmt"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="MIX pmt"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTenor" Width="45px" runat="server" CssClass="textBox" TabIndex="53"
                                                        MaxLength="3"></asp:TextBox>
                                                    <span class="elementLabel">DAYS FROM</span>&nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlTenor_Days_From" runat="server" CssClass="dropdownList"
                                                        TabIndex="54">
                                                        <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="SHIPMENT DATE" Text="SHIPMENT DATE"></asp:ListItem>
                                                        <asp:ListItem Value="INVOICE DATE" Text="INVOICE DATE"></asp:ListItem>
                                                        <asp:ListItem Value="BOEXCHANGE DATE" Text="BOEXCHANGE DATE"></asp:ListItem>
                                                        <asp:ListItem Value="OTHERS/BLANK" Text="OTHERS/BLANK"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTenor_Description" runat="server" CssClass="textBox" MaxLength="31"
                                                        TabIndex="55" Width="230px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">ARRIVAL DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDateArrival" runat="server" CssClass="textBox" MaxLength="10"
                                                        ValidationGroup="dtVal" Width="70px" TabIndex="56"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_Arrival_date" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtDateArrival" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Arrival_date" runat="server" CssClass="btncalendar_enabled"
                                                        TabIndex="2" />
                                                    <ajaxToolkit:CalendarExtender ID="CE_Arrival_date" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateArrival" PopupButtonID="btncal_Arrival_date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MV_Arrival_date" runat="server" ControlExtender="ME_Arrival_date"
                                                        ValidationGroup="dtVal" ControlToValidate="txtDateArrival" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">THEIR REF.NO :</span>
                                                    <asp:TextBox ID="txt_Their_Ref_no" runat="server" CssClass="textBox" MaxLength="20"
                                                        TabIndex="57" Width="160px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMODITY CODE :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtCommodity" runat="server" CssClass="textBox" TabIndex="58" Width="50px"
                                                        AutoPostBack="True" OnTextChanged="txtCommodity_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lblCommodityDesc" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;
                                                    <asp:TextBox ID="txtCommodityDesc" runat="server" CssClass="textBox" MaxLength="100"
                                                        TabIndex="59" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SHIPPMENT DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingDate" runat="server" CssClass="textBox" MaxLength="10"
                                                        ValidationGroup="dtVal" Width="70px" TabIndex="60"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_Ship_Date" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtShippingDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_Ship_Date" runat="server" CssClass="btncalendar_enabled" TabIndex="27" />
                                                    <ajaxToolkit:CalendarExtender ID="CE_Ship_Date" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtShippingDate" PopupButtonID="btnCal_Ship_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MV_Ship_Date" runat="server" ControlExtender="ME_Ship_Date"
                                                        ValidationGroup="dtVal" ControlToValidate="txtShippingDate" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">VESSEL NAME :</span>
                                                    <asp:TextBox ID="txtVesselName" runat="server" CssClass="textBox" MaxLength="30"
                                                        TabIndex="61" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">FROM :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtFromPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="62"
                                                        Width="100px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;<span class="elementLabel">To :</span>
                                                    <asp:TextBox ID="txtToPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="63"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="100%" colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" width="10%">
                                                                <span class="elementLabel" style="font-weight: bold">DOCUMENTS:</span>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                &nbsp;&nbsp;<span class="elementLabel" style="font-weight: bold">INS</span>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                &nbsp;&nbsp;<span class="elementLabel" style="font-weight: bold">B/L</span>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                &nbsp;&nbsp;<span class="elementLabel" style="font-weight: bold">AWB</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">FIRST :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocFirst1" runat="server" CssClass="textBox" MaxLength="3" TabIndex="64"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                </span>
                                                                <asp:TextBox ID="txtDocFirst2" runat="server" CssClass="textBox" MaxLength="3" TabIndex="65"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocFirst3" runat="server" CssClass="textBox" MaxLength="3" TabIndex="67"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">SECOND :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond1" runat="server" CssClass="textBox" MaxLength="3" TabIndex="68"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond2" runat="server" CssClass="textBox" MaxLength="3" TabIndex="69"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond3" runat="server" CssClass="textBox" MaxLength="3" TabIndex="70"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width ="100%">
                                            <tr>
                                                <td colspan="3"></td>
                                                <td>
                                                    <asp:Button ID="btn_DiscrepancyList" runat="server" Text="Special Instruction" CssClass="buttonDefault"
                                                        Height="20px" Width="150px" Enabled="false"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel"><b>INST CODE :</b></span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_INST_Code" runat="server" MaxLength='2' CssClass="textBox" TabIndex="71"
                                                        Width="50px" onkeydown="return validate_Number(event);"></asp:TextBox>
                                                    <span class="elementLabel">: </span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:Label ID="lbl_Instructions1" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="left" width="50%">
                                                    <asp:RadioButton ID="rdb_SP_Instr_Other" Text="Others" CssClass="elementLabel" runat="server"
                                                        GroupName="SP_Instruction" />
                                                    <asp:RadioButton ID="rdb_SP_Instr_Annexure" Text="As per Annexure" CssClass="elementLabel"
                                                        runat="server" GroupName="SP_Instruction" />
                                                    <asp:RadioButton ID="rdb_SP_Instr_On_Sight" Text="Bene. to be paid on sight" CssClass="elementLabel"
                                                        runat="server" GroupName="SP_Instruction1" />
                                                    <asp:RadioButton ID="rdb_SP_Instr_On_Date" Text="Bene. to be paid on Dated" CssClass="elementLabel"
                                                        runat="server" GroupName="SP_Instruction1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"></td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_Instructions2" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">1.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions1" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="47"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"></td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_Instructions3" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">2.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions2" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="48"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"></td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_Instructions4" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">3.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions3" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="49"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3"></td>
                                                <td align="left">
                                                    <span class="elementLabel">4.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions4" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="50"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2"></td>
                                                <td align="left">
                                                    <asp:Button ID="btn_Instr_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to Document Details" TabIndex="100" OnClick="btn_Instr_Prev_Click"/>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_Instr_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                        ToolTip="Go to Import Accounting" TabIndex="100" OnClick="btn_Instr_Next_Click" />
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">5.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions5" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="51"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentAccounting" runat="server" HeaderText="IMPORT ACCOUNTING"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="80%">
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">1-DISC </span>
                                                    <asp:CheckBox ID="chk_IMP_ACC_Commission" Text="Standard Charges" runat="server" CssClass="elementLabel"
                                                        TabIndex="101" />
                                                </td>
                                                <td align="right" colspan="2">
                                                    <span class="elementLabel">AMOUNT :</span><asp:TextBox ID="txt_DiscAmt" runat="server" CssClass="textBox" TabIndex="101" MaxLength="16"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">EXCH RATE :</span><asp:TextBox ID="txt_IMP_ACC_ExchRate" runat="server" CssClass="textBox" TabIndex="101" MaxLength="9"
                                                        Width="70px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="20%">
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel">2-MATU</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel">LUMP</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel">CONTRACT NO.</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel">EX. CCY</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel">EXCH. RATE</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel">INTNL EX. RATE</span>
                                                </td>
                                                <td align="left" width="20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">PRINCIPAL :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPrinc_matu" runat="server" CssClass="textBox" MaxLength="1" TabIndex="102"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPrinc_lump" runat="server" CssClass="textBox" MaxLength="1" TabIndex="103"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtprinc_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="104" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="105" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtprinc_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="106" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtprinc_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="107" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="108" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="109" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="110" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_interest_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="111" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="112" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="113" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="114" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="115" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="116" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Commission_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="117" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="118" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="119" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="120" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="121" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_Contract_no" runat="server" CssClass="textBox"
                                                        TabIndex="122" MaxLength="9" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                        TabIndex="123" MaxLength="3" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="124" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                        TabIndex="125" MaxLength="9" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%" align="left">
                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span>
                                                            </td>
                                                            <td width="25%" align="left">
                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                            </td>
                                                            <td width="10%" align="center">
                                                                <span class="elementLabel">CUST ABBR</span>
                                                            </td>
                                                            <td width="10%" align="center">
                                                                <span class="elementLabel">A/C NUMBER</span>
                                                            </td>
                                                            <td width="10%" align="center">
                                                                <span class="elementLabel">CCY</span>
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <span class="elementLabel">AMOUNT</span>
                                                            </td>
                                                            <td width="5%" align="left">
                                                                <span class="elementLabel">PAYER</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:TextBox ID="txt_CR_Code" runat="server" CssClass="textBox" MaxLength="9" TabIndex="126"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span class="elementLabel">ACCEPTANCES</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="127" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="128" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_CR_Acceptance_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="129" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Acceptance_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="130" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Acceptance_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="131" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">INTEREST</span>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_CR_Interest_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="132" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="133" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Interest_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="134" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                    TabIndex="135" MaxLength="3" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                    TabIndex="136" MaxLength="9" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                    TabIndex="137" MaxLength="1" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                    TabIndex="138" MaxLength="3" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                    TabIndex="139" MaxLength="9" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                    TabIndex="140" MaxLength="1" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">OTHERS</span>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="141" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="142" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="143" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                    TabIndex="144" MaxLength="3" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Their_Commission_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="145" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                    TabIndex="146" MaxLength="1" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                            </td>
                                                            <td align="center">
                                                                <%-- <asp:TextBox ID="txt_DR_Code_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="147" Width="40px"></asp:TextBox>--%>
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:TextBox ID="txt_DR_Code" runat="server" CssClass="textBox" MaxLength="9" TabIndex="148"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span class="elementLabel">CURRENT ACCOUNT</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="149" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="150" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="151" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="152" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="153" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="154" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="155" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_payer2" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="156" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="157" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="9"
                                                                    TabIndex="157" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_DR_Cur_Acc_payer3" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="158" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:Button ID="btnDocAccPrev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to Document Details" TabIndex="159" OnClick="btnDocAccPrev_Click" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnImpAccNext" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                        ToolTip="Next To GENERAL OPERATION" TabIndex="160" OnClick="btnDocAccNext_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentGO" runat="server" HeaderText="GENERAL OPERATION"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="center" width="50%">
                                                    <asp:CheckBox ID="chkGO_Swift_SFMS" Text="Swift_SFMS" runat="server" CssClass="elementLabel"
                                                        TabIndex="201" OnCheckedChanged="chkGO_Swift_SFMS_OnCheckedChanged" AutoPostBack="true" />
                                                    <asp:CheckBox ID="chkGO_LC_Commitement" Text="LC_COMMITMENT" runat="server" CssClass="elementLabel"
                                                        TabIndex="202" OnCheckedChanged="chkGO_LC_Commitement_OnCheckedChanged" AutoPostBack="true" />
                                                </td>
                                                <td align="center" width="50%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="DivGO_Swift_SFMS" style="width: 50%; float: left; height: 100%">
                                            <table width="100%">
                                                <asp:Panel ID="panalGO_Swift_SFMS" runat="server" Visible="false">
                                                    <tr>
                                                        <td colspan="6">
                                                            <span class="elementLabel"><b>GENERAL OPERATION FOR SWIFT / SFMS CHRG</b></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">REFERENCE NO : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Ref_No" Width="100px" runat="server" TabIndex="203"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Comment" runat="server" CssClass="textBox" TabIndex="204"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" >
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_SectionNo" runat="server" CssClass="textBox" TabIndex="205"
                                                                Width="20px"></asp:TextBox>
                                                        
                                                            <span class="elementLabel">Remarks : </span>
                                                        
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Remarks" runat="server" Width="300px" CssClass="textBox" TabIndex="206"></asp:TextBox>
                                                        </td>
                                                        <td align="right" >
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                TabIndex="207"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
	                                                    <td align="right" >
		                                                    <span class="elementLabel">SCHEME No : </span>
	                                                    </td>
	                                                    <td align="left" colspan="5">
		                                                    <asp:TextBox ID="txt_GO_Swift_SFMS_Scheme_no" runat="server" CssClass="textBox" TabIndex="207"></asp:TextBox>
	                                                    </td>
	                                                    
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td align="left" width="20%">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="208" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td align="left" width="30%">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="209" Width="25px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td align="left" width="15%">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                TabIndex="210" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Cust" runat="server" CssClass="textBox"
                                                                TabIndex="211" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="212" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                TabIndex="213"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Exch_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="214" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="215" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_FUND" runat="server" CssClass="textBox"
                                                                TabIndex="93" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Check_No" runat="server" CssClass="textBox"
                                                                TabIndex="94"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td  align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="216" Width="20px" MaxLength="1"></asp:TextBox>
                                                       
                                                            <span class="elementLabel">DETAILS : </span>
                                                        
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Details" runat="server" CssClass="textBox" Width="300px" 
                                                                TabIndex="217"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Entity" runat="server" CssClass="textBox"
                                                                TabIndex="217" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Division" runat="server" CssClass="textBox"
                                                                TabIndex="99" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="100"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="101" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="218" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="219" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Amt" runat="server" CssClass="textBox"
                                                                TabIndex="220" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Cust" runat="server" CssClass="textBox"
                                                                TabIndex="221" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="222" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                TabIndex="223"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="224" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="225" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_FUND" runat="server" CssClass="textBox"
                                                                TabIndex="110" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Check_No" runat="server" CssClass="textBox"
                                                                TabIndex="111"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="112" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="226" Width="20px" MaxLength="1"></asp:TextBox>
                                                        
                                                            <span class="elementLabel">DETAILS : </span>
                                                        
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Details" runat="server" CssClass="textBox" Width="300px"
                                                                TabIndex="227"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Entity" runat="server" CssClass="textBox"
                                                                TabIndex="228" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Division" runat="server" CssClass="textBox"
                                                                TabIndex="116" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="117"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="118" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="DivGO_LC_Commitment" style="width: 50%; float: right; height: 100%">
                                            <table width="100%">
                                                <asp:Panel ID="panalGO_LC_Commitement" runat="server" Visible="false">
                                                    <tr>
                                                        <td colspan="6">
                                                            <span class="elementLabel"><b>GENERAL OPERATION FOR LC COMMITMENT CHRG</b></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">REFERENCE NO : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Ref_No" runat="server" Width="100px" TabIndex="229"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Comment" runat="server" TabIndex="230" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" >
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_SectionNo" runat="server" CssClass="textBox"
                                                                TabIndex="231" Width="20px"></asp:TextBox>
                                                        
                                                            <span class="elementLabel">Remarks : </span>
                                                       
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Remarks" runat="server" TabIndex="232" width="300px"  CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right" >
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td  align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_MEMO" runat="server" CssClass="textBox" TabIndex="233"
                                                                MaxLength="6" Width="50px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" >
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td align="left" colspan="5">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Scheme_no" runat="server" CssClass="textBox" TabIndex="84"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td align="left" width="20%">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="234" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td align="left" width="30%">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="235" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td align="left" width="15%">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Amt" runat="server" CssClass="textBox"
                                                                TabIndex="236" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Cust" runat="server" CssClass="textBox"
                                                                TabIndex="237" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="238" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C NO : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                MaxLength="20" TabIndex="239"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="3">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Exch_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="240" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="241" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_FUND" runat="server" CssClass="textBox"
                                                                TabIndex="93" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Check_No" runat="server" CssClass="textBox"
                                                                TabIndex="94"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Advice_Print" runat="server" CssClass="textBox"
                                                                TabIndex="242" Width="20px" MaxLength="1"></asp:TextBox>
                                                        
                                                            <span class="elementLabel">DETAILS : </span>
                                                        
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Details" runat="server" TabIndex="243" Width="300px"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Entity" runat="server" CssClass="textBox"
                                                                TabIndex="244" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Division" runat="server" CssClass="textBox"
                                                                TabIndex="99" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="100"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="101" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="245" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="246" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Amt" runat="server" CssClass="textBox"
                                                                TabIndex="247" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Cust" runat="server" CssClass="textBox"
                                                                TabIndex="248" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="249" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C NO : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                MaxLength="20" TabIndex="250"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="3">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="251" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="252" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_FUND" runat="server" CssClass="textBox"
                                                                TabIndex="110" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Check_No" runat="server" CssClass="textBox"
                                                                TabIndex="111"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="112" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Advice_Print" runat="server" CssClass="textBox"
                                                                TabIndex="253" Width="20px" MaxLength="1"></asp:TextBox>
                                                        
                                                            <span class="elementLabel">DETAILS : </span>
                                                       
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Details" runat="server" TabIndex="254" Width="300px"
                                                                CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Entity" runat="server" CssClass="textBox"
                                                                TabIndex="255" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Division" runat="server" CssClass="textBox"
                                                                TabIndex="116" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="117"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_LC_Commitement_Credit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="118" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <table width="100%">
                                            <tr>
                                                <td width="10%">
                                                </td>
                                                <td align="left" width="90%">
                                                    <asp:Button ID="btnGO_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to IMPORT ACCOUNTING" TabIndex="256" OnClick="btnGO_Prev_Click" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnGO_Next" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                        ToolTip="Go to SWIFT FILES" TabIndex="256" OnClick="btnGO_Next_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentSwiftFile" runat="server" HeaderText="SWIFT FILES"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <ajaxToolkit:TabContainer ID="TabContainerSwift" runat="server" ActiveTabIndex="0"
                                            CssClass="ajax__tab_xp-theme">
                                            <ajaxToolkit:TabPanel ID="tbSwift740" runat="server" HeaderText="MT 740"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                    <tr>
                                                    <td align="right">
                                                             <span class="elementLabel">[20] Documentary Credit Number : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_740_documentaryCreditno" runat="server" MaxLength="16" CssClass="textBox" TabIndex="238" Enabled="false"></asp:TextBox>
                                                      </td>
                                                      <td align="right">
                                                            <span class="elementLabel">[25] Account Identification : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_740_AccountIdentification" runat="server" CssClass="textBox" MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td align="right">
                                                            <span class="elementLabel">[31D] Date and Place of Expiry :</span>
                                                      </td>
                                                      <td align="left">
                                                            <asp:TextBox ID="txt_740_Date" onkeydown="return allowBackSpace('DateandplaceofExpiry',event);"
                                                                Width="60px" runat="server" CssClass="textBox" MaxLength="10" Enabled="false" TabIndex="4"></asp:TextBox>
                                                            <asp:Button ID="btncalendar_Date" runat="server" CssClass="btncalendar_enabled"
                                                                TabIndex="-1" />
                                                            <ajaxToolkit:MaskedEditExtender ID="md740date2" Mask="99/99/9999" MaskType="Date" runat="server"
                                                                TargetControlID="txt_740_Date" InputDirection="RightToLeft" AcceptNegative="Right"
                                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Right" PromptCharacter="_">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <ajaxToolkit:CalendarExtender ID="calendar740Date2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_740_Date" PopupButtonID="btncalendar_Date">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:TextBox ID="txt_740_Placeofexpiry" runat="server" CssClass="textBox" MaxLength="29" TabIndex="238" Width="250px" Enabled="false"></asp:TextBox>
                                                       </td>
                                                       <td align="right">
                                                            <span class="elementLabel">[40F] Applicable Rules : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_740_Applicablerules" runat="server" CssClass="textBox" MaxLength="30" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                      </td>
                                                    </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[59] Beneficiary : </span>
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txt_Acceptance_Beneficiary" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Beneficiary2" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%" align="center">
                                                                <span class="elementLabel">[58A] Negotiating Bank </span>
                                                                <asp:DropDownList ID="ddlNegotiatingBankSwift" runat="server" CssClass="dropdownList" 
                                                                Enabled="false" OnSelectedIndexChanged="ddlNegotiatingBankSwift_SelectedIndexChanged">
                                                                    <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C : </span>
                                                            </td>
                                                            <td width="25%" align="left">
                                                                <%--<asp:TextBox ID="txtNegoPartyIdentifier" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="10" MaxLength="1" Enabled="false"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtNegoAccountNumber" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%"></td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txt_Acceptance_Beneficiary3" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Beneficiary4" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%" align="right">
                                                                <asp:Label ID="lblSwiftOrName" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td width="25%" align="left">
                                                                <asp:TextBox ID="txtNegoSwiftCode" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtNegoName" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel"></span>
                                                            </td>
                                                            <td width="45%">
                                                                 <asp:TextBox ID="txt_Acceptance_Beneficiary5" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%" align="right">
                                                                <asp:Label ID="lblNegoAddress1" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td width="25%">
                                                                <asp:TextBox ID="txtNegoAddress1" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td align="right" width="15%">
                                                                <span class="elementLabel">[32B] Credit Amount : </span>
                                                            </td>
                                                            <td width="45%">
                                                                <span class="elementLabel">Currency : </span>
                                                                <asp:TextBox ID="txtCreditCurrency" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">Amount : </span>
                                                                <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="90px" MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%" align="right">
                                                                <asp:Label ID="lblNegoAddress2" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td width="25%">
                                                                <asp:TextBox ID="txtNegoAddress2" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td align="right" width="15%">
                                                                <span class="elementLabel">[39A] Percentage Credit Amount Tolerance : </span>
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txtPercentageCreditAmountTolerance" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="2" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtPercentageCreditAmountTolerance1" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="2" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%" align="right">
                                                                <asp:Label ID="lblNegoAddress3" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td width="25%">
                                                                <asp:TextBox ID="txtNegoAddress3" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[39C] Additional Amounts Covered : </span>
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txt_Acceptance_Additional_Amt_Covered" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Additional_Amt_Covered2" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>

                                                            </td>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[39B] Maximum Credit Amount : </span>
                                                            </td>
                                                            <td width="45%">
                                                               <asp:TextBox ID="txt_Acceptance_Max_Credit_Amt" runat="server" CssClass="textBox"
                                                                    TabIndex="237" Width="130px" MaxLength="13" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%"></td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txt_Acceptance_Additional_Amt_Covered3" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Additional_Amt_Covered4" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%"></td>
                                                            <td width="25%"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[42C] Drafts at : </span>
                                                            </td>
                                                            <td width="85%" colspan="3">
                                                                <asp:TextBox ID="txt_740_Draftsat1" runat="server" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_740_Draftsat2" runat="server" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_740_Draftsat3" runat="server" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[41A] Available with by : </span>
                                                                <asp:DropDownList ID="ddlAvailablewithby_740" runat="server" CssClass="dropdownList"
                                                                    OnSelectedIndexChanged="ddlAvailablewithby_740_TextChanged" AutoPostBack="true" Enabled="false">
                                                                    <asp:ListItem Text="41A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="41D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">Code :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAvailablewithbyCode" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="250px" MaxLength="14" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[42A] Drawee : </span>
                                                                <asp:DropDownList ID="ddlDrawee_740" runat="server" CssClass="dropdownList"
                                                                    OnSelectedIndexChanged="ddlDrawee_740_TextChanged" AutoPostBack="true" Enabled="false">
                                                                    <asp:ListItem Text="42A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="42D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDraweeAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="250px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAvailablewithbySwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAvailablewithbySwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtAvailablewithbyName" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblDraweeSwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtDraweeSwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtDraweeName" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAvailablewithbyAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAvailablewithbyAddress1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblDraweeAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtDraweeAddress1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAvailablewithbyAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAvailablewithbyAddress2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblDraweeAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtDraweeAddress2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" TabIndex="238" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAvailablewithbyAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAvailablewithbyAddress3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblDraweeAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtDraweeAddress3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" TabIndex="238" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[42M] Mixed Payment Details : </span>
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txtMixedPaymentDetails" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtMixedPaymentDetails2" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%"></td>
                                                            <td width="25%"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%"></td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txtMixedPaymentDetails3" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtMixedPaymentDetails4" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%"></td>
                                                            <td width="25%"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[42P] Deferred Payment Details : </span>
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txtDeferredPaymentDetails" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtDeferredPaymentDetails2" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[71A] Reimbursing Bank Charges : </span>
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txt_Acceptance_Reimbur_Bank_Charges" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="30px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%"></td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txtDeferredPaymentDetails3" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtDeferredPaymentDetails4" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15%"></td>
                                                            <td width="25%"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[71D] Other Charges : </span>
                                                            </td>
                                                            <td width="85%" colspan="3">
                                                                <asp:TextBox ID="txt_Acceptance_Other_Charges" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Other_Charges2" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Other_Charges3" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%"></td>
                                                            <td width="85%" colspan="3">
                                                                <asp:TextBox ID="txt_Acceptance_Other_Charges4" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Other_Charges5" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Other_Charges6" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%">
                                                                <span class="elementLabel">[72Z] Sender to Receiver Information : </span>
                                                            </td>
                                                            <td width="85%" colspan="3">
                                                                <asp:TextBox ID="txt_Acceptance_Sender_to_Receiver_Information" runat="server" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" TabIndex="239" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Sender_to_Receiver_Information2" runat="server" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" TabIndex="239" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Sender_to_Receiver_Information3" runat="server" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" TabIndex="239" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="15%"></td>
                                                            <td width="85%" colspan="3">
                                                                <asp:TextBox ID="txt_Acceptance_Sender_to_Receiver_Information4" runat="server" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" TabIndex="239" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Sender_to_Receiver_Information5" Width="300px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="239" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt_Acceptance_Sender_to_Receiver_Information6" Width="300px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="239" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbSwiftMT756" runat="server" HeaderText="MT 756"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="50%" align="right">
                                                                <table width="100%">
                                                                <tr><td align="right"><span class="elementLabel">Discrepancy Charges : </span></td>
                                                                <td align="left">
                                                                            <asp:TextBox runat="server" ID="txt_Discrepancy_Charges_Swift" CssClass="textBox" MaxLength="16" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                        </td>
                                                                </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <span class="elementLabel">[54] Receiver's Correspondent</span>
                                                                            <asp:DropDownList ID="ddlReceiverCorrespondentMT" runat="server" CssClass="dropdownList"
                                                                                OnSelectedIndexChanged="ddlReceiverCorrespondentMT_TextChanged" AutoPostBack="true">
                                                                                <asp:ListItem Text="54A" Value="A"></asp:ListItem>
                                                                                <asp:ListItem Text="54B" Value="B"></asp:ListItem>
                                                                                <asp:ListItem Text="54D" Value="D"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <span class="elementLabel">A / C :</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtReceiverPartyIdentifier" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="10" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            <asp:TextBox runat="server" ID="txtReceiverAccountNumberMT" CssClass="textBox" MaxLength="20" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblReceiverSNLMT" runat="server" CssClass="elementLabel" Text="SwiftCode :"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox runat="server" ID="txtReceiverSwiftCodeMT" CssClass="textBox" MaxLength="11" Width="100px"></asp:TextBox>
                                                                            <asp:TextBox runat="server" ID="txtReceiverNameMT" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                            <asp:TextBox runat="server" ID="txtReceiverLocationMT" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblReceiverAddress1MT" runat="server" CssClass="elementLabel"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox runat="server" ID="txtReceiverAddress1MT" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblReceiverAddress2MT" runat="server" CssClass="elementLabel"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox runat="server" ID="txtReceiverAddress2MT" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblReceiverAddress3MT" runat="server" CssClass="elementLabel"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox runat="server" ID="txtReceiverAddress3MT" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="50%" align="left">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="15%">
                                                                            <span class="elementLabel">[79Z] Narrative : </span>
                                                                        </td>
                                                                        <td width="85%" valign="top">
                                                                            <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Narrative_756" runat="server" TargetControlID="panel_AddNarrative_756"
                                                                                CollapsedSize="0" ExpandedSize="200"
                                                                                ScrollContents="True" Enabled="True" />
                                                                            <asp:Panel ID="panel_AddNarrative_756" runat="server">
                                                                                <table cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">1.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_1" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">2.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_2" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">3.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_3" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">4.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_4" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">5.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_5" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">6.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_6" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">7.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_7" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">8.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_8" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">9.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_9" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">10.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_10" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">11.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_11" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">12.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_12" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">13.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_13" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">14.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_14" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">15.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_15" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">16.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_16" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">17.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_17" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">18.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_18" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">19.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_19" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">20.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_20" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">21.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_21" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">22.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_22" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">23.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_23" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">24.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_24" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">25.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_25" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">26.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_26" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">27.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_27" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">28.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_28" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">29.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_29" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">30.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_30" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">31.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_31" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">32.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_32" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">33.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_33" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">34.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_34" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">35.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_756_35" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbSwiftSFMS756" runat="server" HeaderText="SFMS 756"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="50%">
                                                    <tr><td align="right"><span class="elementLabel">Discrepancy Charges : </span></td>
                                                                <td align="left">
                                                                            <asp:TextBox runat="server" ID="txt_Discrepancy_Charges_SFMS" CssClass="textBox" MaxLength="16" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                        </td>
                                                                </tr>
                                                        <tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[53] Sender's Correspondent</span>
                                                                <asp:DropDownList ID="ddlSenderCorrespondentSFMS" runat="server" CssClass="dropdownList"
                                                                    OnSelectedIndexChanged="ddlSenderCorrespondentSFMS_TextChanged" AutoPostBack="true">
                                                                    <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                 <asp:TextBox ID="txtSenderPartyIdentifier" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="10" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtSenderAccountNumberSFMS" CssClass="textBox" MaxLength="20" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblSenderSNLSFMS" runat="server" CssClass="elementLabel" Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSenderSwiftCodeSFMS" CssClass="textBox" MaxLength="11" Width="100px"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtSenderNameSFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtSenderLocationSFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblSenderAddress1SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSenderAddress1SFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblSenderAddress2SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSenderAddress2SFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblSenderAddress3SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSenderAddress3SFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[54] Receiver's Correspondent</span>
                                                                <asp:DropDownList ID="ddlReceiverCorrespondentSFMS" runat="server" CssClass="dropdownList"
                                                                    OnSelectedIndexChanged="ddlReceiverCorrespondentSFMS_TextChanged" AutoPostBack="true">
                                                                    <asp:ListItem Text="54A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="54B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="54D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtReceiverPartyIdentifierSFMS" runat="server" CssClass="textBox" TabIndex="237"
                                                                    Width="10" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtReceiverAccountNumberSFMS" CssClass="textBox" MaxLength="20" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiverSNLSFMS" runat="server" CssClass="elementLabel" Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiverSwiftCodeSFMS" CssClass="textBox" MaxLength="11" Width="100px"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtReceiverNameSFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtReceiverLocationSFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiverAddress1SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiverAddress1SFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiverAddress2SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiverAddress2SFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiverAddress3SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiverAddress3SFMS" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbSwift799" runat="server" HeaderText="MT/IFN 799"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="50%">
                                                        <tr>
                                                            <td width="15%">
                                                                <span class="elementLabel">[79] Narrative : </span>
                                                            </td>
                                                            <td width="85%" valign="top">
                                                                <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Narrative" runat="server" TargetControlID="panel_AddNarrative"
                                                                    CollapsedSize="0" ExpandedSize="200"
                                                                    ScrollContents="True" Enabled="True" />
                                                                <asp:Panel ID="panel_AddNarrative" runat="server">
                                                                    <table cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">1.</span>
                                                                                <asp:TextBox ID="txt_Narrative_1" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">2.</span>
                                                                                <asp:TextBox ID="txt_Narrative_2" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">3.</span>
                                                                                <asp:TextBox ID="txt_Narrative_3" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">4.</span>
                                                                                <asp:TextBox ID="txt_Narrative_4" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">5.</span>
                                                                                <asp:TextBox ID="txt_Narrative_5" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">6.</span>
                                                                                <asp:TextBox ID="txt_Narrative_6" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">7.</span>
                                                                                <asp:TextBox ID="txt_Narrative_7" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">8.</span>
                                                                                <asp:TextBox ID="txt_Narrative_8" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">9.</span>
                                                                                <asp:TextBox ID="txt_Narrative_9" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">10.</span>
                                                                                <asp:TextBox ID="txt_Narrative_10" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">11.</span>
                                                                                <asp:TextBox ID="txt_Narrative_11" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">12.</span>
                                                                                <asp:TextBox ID="txt_Narrative_12" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">13.</span>
                                                                                <asp:TextBox ID="txt_Narrative_13" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">14.</span>
                                                                                <asp:TextBox ID="txt_Narrative_14" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">15.</span>
                                                                                <asp:TextBox ID="txt_Narrative_15" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">16.</span>
                                                                                <asp:TextBox ID="txt_Narrative_16" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">17.</span>
                                                                                <asp:TextBox ID="txt_Narrative_17" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">18.</span>
                                                                                <asp:TextBox ID="txt_Narrative_18" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">19.</span>
                                                                                <asp:TextBox ID="txt_Narrative_19" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">20.</span>
                                                                                <asp:TextBox ID="txt_Narrative_20" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">21.</span>
                                                                                <asp:TextBox ID="txt_Narrative_21" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">22.</span>
                                                                                <asp:TextBox ID="txt_Narrative_22" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">23.</span>
                                                                                <asp:TextBox ID="txt_Narrative_23" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">24.</span>
                                                                                <asp:TextBox ID="txt_Narrative_24" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">25.</span>
                                                                                <asp:TextBox ID="txt_Narrative_25" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">26.</span>
                                                                                <asp:TextBox ID="txt_Narrative_26" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">27.</span>
                                                                                <asp:TextBox ID="txt_Narrative_27" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">28.</span>
                                                                                <asp:TextBox ID="txt_Narrative_28" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">29.</span>
                                                                                <asp:TextBox ID="txt_Narrative_29" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">30.</span>
                                                                                <asp:TextBox ID="txt_Narrative_30" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">31.</span>
                                                                                <asp:TextBox ID="txt_Narrative_31" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">32.</span>
                                                                                <asp:TextBox ID="txt_Narrative_32" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">33.</span>
                                                                                <asp:TextBox ID="txt_Narrative_33" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">34.</span>
                                                                                <asp:TextBox ID="txt_Narrative_34" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">35.</span>
                                                                                <asp:TextBox ID="txt_Narrative_35" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                             <%-- bhupen  747--%>
                                             <ajaxToolkit:TabPanel ID="tbSwiftMT747" runat="server" HeaderText="MT 747"
										     Font-Bold="true" ForeColor="White">
                                            <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="right">
                                                             <span class="elementLabel" style="color:Red;font-weight:bold;">[20] Documentary Credit Number : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_documentaryCredtno" runat="server" MaxLength="16" CssClass="textBox" TabIndex="1"></asp:TextBox>
                                                      </td>
                                                    <td align="right">
                                                            <span class="elementLabel">[21] Reimbursing Bank's Reference : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_reimbursingbankRef" runat="server" CssClass="textBox" MaxLength="16" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                 </tr>

                                                  <tr>
                                                    <td align="right">
                                                             <span class="elementLabel" style="color:Red;font-weight:bold;">[30] Date of the Original Authorisation to Reimburse : </span>
                                                      </td>
                                                      <td align="left">
                                                              <%--vinay swift changes--%>
                                                             <asp:TextBox ID="txt_747_DateofogauthriztnRmburse" Width="100px" runat="server" MaxLength="10" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                            <%-- -----------------%>
                                                      </td>
                                                      <td align="right">
                                                            <span class="elementLabel">[31E] New Date of Expiry :</span>
                                                      </td>
                                                      <td align="left">
                                                               <%--vinay swift changes--%>
                                                             <asp:TextBox ID="txt_747_NewDateofExpiry" Width="100px" runat="server" CssClass="textBox" MaxLength="10" TabIndex="2"></asp:TextBox>
                                                             <%-------------------%>
                                                      </td>
                                                 </tr>
                                                 
                                                 <tr>
                                                    <td align="right">
                                                             <span class="elementLabel">[32B] Increase of Documentary Credit Amount : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_IncreaseofDocumentaryCreditCurr" Width="50px" runat="server" MaxLength="3" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                             <asp:TextBox ID="txt_747_IncreaseofDocumentaryCreditAmt" runat="server" MaxLength="15" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                      <td align="right">
                                                            <span class="elementLabel">[33B] Decrease of Documentary Credit Amount :</span>
                                                      </td>
                                                      <td align="left">
                                                            <asp:TextBox ID="txt_747_DecreaseofDocumentaryCreditCurr" Width="50px" runat="server" CssClass="textBox" MaxLength="3" TabIndex="2"></asp:TextBox>
                                                             <asp:TextBox ID="txt_747_DecreaseofDocumentaryCreditAmt" runat="server" CssClass="textBox" MaxLength="15" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                 </tr>

                                                <tr>
                                                    <td align="right">
                                                             <span class="elementLabel">[34B]  New Documentary Credit Amount After Amendment : </span>
                                                    </td>
                                                    <td align="left">
                                                             <asp:TextBox ID="txt_747_NewDocumentaryCreditAmtAfterAmendmentCurr" Width="50px" runat="server" MaxLength="3" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                             <asp:TextBox ID="txt_747_NewDocumentaryCreditAmtAfterAmendment"  runat="server" MaxLength="15" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                    <td align="right">
                                                            <span class="elementLabel">[39A] Percentage Credit Amount Tolerance :</span>
                                                    </td>
                                                    <td align="left">
                                                             <asp:TextBox ID="txt_747_PercentageCreditAmtTolerance1" runat="server" Width="50px" CssClass="textBox" MaxLength="2" TabIndex="2"></asp:TextBox>
                                                             <asp:TextBox ID="txt_747_PercentageCreditAmtTolerance2" runat="server" Width="50px" CssClass="textBox" MaxLength="2" TabIndex="2"></asp:TextBox>
                                                    </td>
                                                 </tr>

                                                 <tr>
                                                      <td align="right">
                                                             <span class="elementLabel">[39C] Additional Amounts Covered : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_AdditionalAmtsCovered1" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                      <td align="right">
                                                             <span class="elementLabel">[72Z] Sender to Receiver Information : </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_SentoReceivInfo1" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                   </tr>
                                                   <tr>
                                                       <td align="right"></td>
                                                       <td align="left">
                                                             <asp:TextBox ID="txt_747_AdditionalAmtsCovered2" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                      <td align="right"></td>
                                                       <td align="left">
                                                             <asp:TextBox ID="txt_747_SentoReceivInfo2" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                   </tr>

                                                  <tr>
                                                      <td align="right">
                                                             <span class="elementLabel"> </span>
                                                      </td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_AdditionalAmtsCovered3" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                      <td align="left"></td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_SentoReceivInfo3" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                       <td align="right">
                                                             <span class="elementLabel"> </span>
                                                      </td>
                                                       <td align="left">
                                                             <asp:TextBox ID="txt_747_AdditionalAmtsCovered4" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                       </td>
                                                       <td align="right">
                                                             <span class="elementLabel"> </span>
                                                      </td>
                                                       <td align="left">
                                                             <asp:TextBox ID="txt_747_SentoReceivInfo4" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                  </tr>


                                                 <tr>
                                                      <td align="right">
                                                             <span class="elementLabel"> </span>
                                                      </td>
                                                      <td align="left"></td>
                                                      <td align="right"></td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_SentoReceivInfo5" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                 </tr>
                                                 <tr>
                                                      <td align="right">
                                                             <span class="elementLabel"> </span>
                                                      </td>
                                                      <td align="left" ></td>
                                                      <td align="right"></td>
                                                      <td align="left">
                                                             <asp:TextBox ID="txt_747_SentoReceivInfo6" Width="300px" runat="server" MaxLength="35" CssClass="textBox" TabIndex="2"></asp:TextBox>
                                                      </td>
                                                 </tr>

                                                 <tr>
                                                       <td width="100%" align="left" colspan="4">
                                                                <table width="50%">
                                                                    <tr>
                                                                        <td>
                                                                            <span class="elementLabel">[77] Narrative : </span>
                                                                        </td>
                                                                        <td width="85%" valign="top">
                                                                            <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Narrative_747" runat="server" TargetControlID="panel_AddNarrative_747"
                                                                                CollapsedSize="0" ExpandedSize="200"
                                                                                ScrollContents="True" Enabled="True" />
                                                                            <asp:Panel ID="panel_AddNarrative_747" runat="server">
                                                                                <table cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">1.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_1" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">2.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_2" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">3.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_3" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">4.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_4" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">5.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_5" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">6.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_6" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">7.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_7" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">8.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_8" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">9.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_9" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">10.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_10" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">11.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_11" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">12.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_12" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">13.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_13" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">14.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_14" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">15.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_15" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">16.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_16" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">17.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_17" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <span class="elementLabel">18.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_18" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">19.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_19" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="elementLabel">20.</span>
                                                                                            <asp:TextBox ID="txt_Narrative_747_20" Width="90%" runat="server" CssClass="textBox"
                                                                                                MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                   
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                  
                                                  </tr>
                                                  


                                        </table>
                                        </ContentTemplate>
                                        </ajaxToolkit:TabPanel>

                                        </ajaxToolkit:TabContainer>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%"></td>
                                                <td width="90%">
                                                    <span class="elementLabel">SELECT FILE TYPE :</span>
                                                    <asp:CheckBox ID='rdb_swift_None' Text="None" CssClass="elementLabel" runat="server"
                                                        GroupName="SwiftFile" />
                                                    <asp:CheckBox ID='rdb_swift_740' Text="MT 740" CssClass="elementLabel" runat="server"
                                                        GroupName="SwiftFile" />
                                                    <asp:CheckBox ID='rdb_swift_756' Text="MT / FIN 756" CssClass="elementLabel" runat="server"
                                                        GroupName="SwiftFile" />
                                                    <asp:CheckBox ID='rdb_swift_999' Text="MT 999" CssClass="elementLabel" runat="server"
                                                        GroupName="SwiftFile" />
                                                    <asp:CheckBox ID='rdb_swift_799' Text="MT / FIN 799" CssClass="elementLabel" runat="server"
                                                        GroupName="SwiftFile" />
                                                    <asp:CheckBox ID='rdb_swift_747' Text="MT / FIN 747" CssClass="elementLabel" runat="server"
                                                        GroupName="SwiftFile" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                                        ToolTip="Save" OnClick="btnSave_Click" TabIndex="107" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="btn_SWIFT_File_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back To GENERAL OPERATION" TabIndex="256" OnClick="btn_SWIFT_File_Prev_Click" />
                                                    <asp:Button ID="btn_Generate_Swift" runat="server" Text="View SWIFT Message" CssClass="buttonDefault"
                                                        Width="150px" ToolTip="View SWIFT Message" TabIndex="256" OnClientClick="ViewSwiftMessage();" />
                                                    <asp:Button ID="btn_Generate_SFMS" runat="server" Text="View SFMS Message" CssClass="buttonDefault"
                                                        Width="150px" ToolTip="View SFMS Message" TabIndex="256" OnClientClick="ViewSFMSMessage();" />
                                                    
                                                    <span class="elementLabel">Approve / Reject :</span>
                                                    <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="35">
                                                        <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                            <td align="center" colspan="4">
                                <asp:Label ID="lblLEI_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true" ForeColor="Green" Visible="false"></asp:Label>
                                <asp:Label ID="lblLEIExpiry_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true" ForeColor="Green" Visible="false"></asp:Label>
                            </td>
                       </tr>
                        <tr>
                            <td align="center" colspan="4">
                               <asp:Label ID="lblLEI_Remark_Drawee" runat="server" CssClass="mandatoryField" Font-Bold="true" ForeColor="Green" Visible="false"></asp:Label>
                               <asp:Label ID="lblLEIExpiry_Remark_Drawee" runat="server" CssClass="mandatoryField" Font-Bold="true" ForeColor="Green" Visible="false"></asp:Label>
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
    </div>
    </form>
</body>
</html>
