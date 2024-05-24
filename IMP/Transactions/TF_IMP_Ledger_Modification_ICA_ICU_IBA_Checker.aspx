<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker.aspx.cs" Inherits="IMP_Transactions_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker" %>

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
    <script src="../Scripts/TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
    	$(document).ready(function (e) {
    		$("input").keypress(function (e) {
    			var k;
    			document.all ? k = e.keyCode : k = e.which;
    			return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 46 || k == 44 || (k >= 48 && k <= 57));
    		});
    	});

    </script>
    <style type="text/css">
        .textBox {
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <table id="tbl_Header" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Ledger Modification (ICA,ICU,IBA) - Checker</strong></span>
                        </td>
                        <td align="right" style="width: 50%" valign="bottom">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClick="btnBack_Click" OnClientClick="return SaveUpdateData();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            <%-------------------------  hidden fields  --------------------------------%>
                            <input type="hidden" id="hdnUserName" runat="server" />
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnDocType" runat="server" />
                            <input type="hidden" id="hdn_FLC_ILC" runat="server" />
                            <input type="hidden" id="hdnDocScrutiny" runat="server" />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnNegoRemiBankType" runat="server" />
                            <input type="hidden" id="hdnDocNo" runat="server" />  
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tbl_LegerModification" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="10%">
                            <span class="elementLabel">Trans. Ref. No :</span>
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtDocNo" Enabled="false" Width="120px" runat="server" CssClass="textBox" TabIndex="1"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="lblForeign_Local" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblCollection_Lodgment_UnderLC" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSight_Usance" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td width="60%" align="left" >
                            <span class="elementLabel">Val.Date :</span>
                            <asp:TextBox Enabled="false" ID="txtValueDate" runat="server" TabIndex="2" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="100px"></asp:TextBox>
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
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="3">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbDocumentLedger" runat="server" HeaderText="LEDGER FILE"
                                    Font-Bold="true" ForeColor="White" >
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">CUSTOMER NAME :</span>
                                                </td>
                                                <td align="left" colspan="5">
                                                <asp:TextBox Enabled="false" ID="txtLedgerCustName" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="2" Width="300px"></asp:TextBox>
                                                        </td>
                                            </tr>
                                            <tr>
                                            <td align="right" width="10%">
                                                    <span class="elementLabel">A/C CODE :</span>
                                                </td>
                                                <td align="left" width="20%">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerAccode" runat="server" CssClass="textBox" MaxLength="5"
                                                        TabIndex="3" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%"><span class="elementLabel">CURRENCY :</span> </td>
                                                <td align="left" width="20%" >
                                                    <asp:TextBox Enabled="false" ID="txtLedgerCURR" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="4" Width="50px"></asp:TextBox>
                                                </td >
                                                <td align="right" width="10%"><span class="elementLabel">REMARKS :</span></td>
                                                <td align="left" width="30%" >
                                                <asp:TextBox Enabled="false" ID="txtLedgerRemark" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="5" Width="300px"></asp:TextBox>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td align="right" >
                                                <span class="elementLabel">REFERENCE NO :</span>
                                                </td>
                                                <td align="left">
                                                <asp:TextBox Enabled="false" ID="txtLedgerRefNo" Width="120px" runat="server" CssClass="textBox" TabIndex="6"  MaxLength="14"></asp:TextBox>
                                                </td>
                                                <td align="right" >
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox Enabled="false" ID="txtLedger_Modify_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="7"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td align="right" >
                                                    <span class="elementLabel">OPERATION DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox  ID="txtLedgerOperationDate" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="8" Width="70px" Enabled="False"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerOperationDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgerOperationDate" runat="server" CssClass="btncalendar_enabled"
                                                        />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerOperationDate" PopupButtonID="btnCal_LedgerOperationDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">BALANCE :</span>
                                                </td>
                                                <td align="left">
                                                        <asp:TextBox Enabled="false" ID="txtLedgerBalanceAmt" runat="server" CssClass="textBox" MaxLength="16"
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
                                                        TabIndex="7" Width="70px" Enabled="False"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerAcceptDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txtLedgerAcceptDate" runat="server" CssClass="btncalendar_enabled"
                                                        />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerAcceptDate" PopupButtonID="btnCal_txtLedgerAcceptDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">MATURITY :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox  ID="txtLedgerMaturity" runat="server" CssClass="textBox" 
														MaxLength="10" TabIndex="12"
                                                        Width="90px" Enabled="False"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerMaturity" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txtLedgerMaturity" runat="server" CssClass="btncalendar_enabled"/>
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
                                                    <asp:TextBox ID="txtLedgerSettlememtDate" runat="server" CssClass="textBox" 
														MaxLength="10" TabIndex="12"
                                                        Width="70px" Enabled="False"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerSettlememtDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgerSettlememtDate" runat="server" CssClass="btncalendar_enabled"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerSettlememtDate" PopupButtonID="btnCal_LedgerSettlememtDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL. VALUE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerSettlValue" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">LAST MOD. DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerLastModDate" runat="server" CssClass="textBox" 
														MaxLength="10" TabIndex="12"
                                                        Width="70px" Enabled="False"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerLastModDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgerLastModDate" runat="server" CssClass="btncalendar_enabled"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerLastModDate" PopupButtonID="btnCal_LedgerLastModDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">REM(EUC) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerREM_EUC" runat="server" CssClass="textBox" MaxLength="20" TabIndex="16"
                                                        Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">LAST OPE. DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLedgerLastOPEDate" runat="server" CssClass="textBox" 
														MaxLength="10" TabIndex="12"
                                                        Width="70px" Enabled="False"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtLedgerLastOPEDate" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_LedgeRLastOPEDate" runat="server" CssClass="btncalendar_enabled"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLedgerLastOPEDate" PopupButtonID="btnCal_LedgeRLastOPEDate" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">TRANS. NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerTransNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1"  MaxLength="14"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CONTRA COUNTRY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerContraCountry" runat="server" CssClass="textBox" TabIndex="20"
                                                        MaxLength="3" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">STATUS CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerStatusCode" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="21" Width="140px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COLLECT. OF COMM :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerCollectOfComm" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">COMMODITY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerCommodity" runat="server" CssClass="textBox" MaxLength="12"
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
                                                <td align="left"><span class="elementLabel">PAYER</span> </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">HANDLING COMM :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerhandlingCommRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerhandlingCommCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerhandlingCommAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerhandlingCommPayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">POSTAGE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerPostageRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerPostageCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerPostageAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerPostagePayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">OTHERS :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerOthersRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerOthersCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerOthersAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerOthersPayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">THEIR COMM. :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerTheirCommRate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerTheirCommCurr" runat="server" CssClass="textBox" MaxLength="3" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerTheirCommAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="8"  onkeydown="return validate_Number(event);"
                                                        Width="100px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerTheirCommPayer" runat="server" CssClass="textBox" MaxLength="1" TabIndex="9"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COLLECT/NEGO BANK :</span>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerNegoBank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="37" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lblLedgerNegoBank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">REIMBURSING BANK :</span>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerReimbursingBank" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="39" MaxLength="12"></asp:TextBox>
                                                    <asp:Label ID="lblLedgerReimbursingBank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td  align="right">
                                                    <span class="elementLabel">DRAWER/DRAWEE NAME:</span>
                                                </td>
                                                <td  align="left">
                                                    <asp:TextBox Enabled="false" ID="txtLedgerDrawer" runat="server" CssClass="textBox" MaxLength="50" TabIndex="51"
                                                        Width="300px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">TENOR :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox Enabled="false" ID="txtLedgerTenor" Width="45px" runat="server" CssClass="textBox" TabIndex="53"
                                                        MaxLength="3"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox Enabled="false" ID="txtLedgerAttn" Width="350px" runat="server" CssClass="textBox" MaxLength="70"
                                                        TabIndex="41" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" >
                                                </td>
                                                <td >
                                                    <asp:Button ID="btnLedgerNext" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                        TabIndex="50" ToolTip="Go to DOCUMENT DETAILS" OnClientClick="return OnDocNextClick(1);"
                                                       />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                               <ajaxToolkit:TabPanel ID="tbDocumentSwiftFile" runat="server" HeaderText="Swift Details"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="50%" style="padding-top:20px;padding-left:20px;">                                            
                                            <tr>
                                                <td width="50%">
                                                    <span class="elementLabel">[79] Narrative : </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Discrepancy" runat="server" TargetControlID="panelNarrative499"
                                                                CollapsedSize="0" ExpandedSize="200"
                                                                ScrollContents="True" Enabled="True" />
                                                    <asp:Panel ID="panelNarrative499" runat="server">
                                                        <table cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">1.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative1" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">2.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative2" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">3.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative3" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">4.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">5.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative5" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">6.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative6" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">7.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative7" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">8.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative8" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">9.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative9" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">10.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative10" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">11.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative11" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">12.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative12" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">13.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative13" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">14.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative14" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">15.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative15" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">16.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative16" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">17.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative17" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">18.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative18" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">19.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative19" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">20.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative20" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">21.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative21" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">22.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative22" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">23.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative23" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">24.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative24" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">25.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative25" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">26.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative26" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">27.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative27" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">28.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative28" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">29.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative29" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">30.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative30" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">31.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative31" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">32.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative32" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">33.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative33" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">34.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative34" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">35.</span>
                                                                    <asp:TextBox  ID="Narrative35" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>                                                           
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td width="10%"></td>
                                                <td width="90%">
                                                    <span class="elementLabel">SELECT FILE TYPE :</span>
                                                    <asp:CheckBox Enabled="false" ID='rdb_swift_None' Text="None" CssClass="elementLabel" runat="server"
                                                       />
                                                    <asp:CheckBox Enabled="false"  ID='rdb_swift_499' Text="MT 499" CssClass="elementLabel" runat="server"
                                                     />
                                                    <asp:CheckBox Enabled="false"  ID='rdb_swift_799' Text="MT 799" CssClass="elementLabel" runat="server"
                                                     />
                                                   <asp:CheckBox Enabled="false"  ID='rdb_swift_999' Text="MT 999" CssClass="elementLabel" runat="server"
                                                     />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%"></td>
                                                <td align="left" width="90%">
                                                    <asp:Button ID="btnSwift_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to Ledger File" TabIndex="256"
                                                        OnClientClick="return OnDocNextClick(0);" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_Generate_Swift" runat="server" Text="View SWIFT Message" CssClass="buttonDefault"
                                                        Width="150px" ToolTip="View SWIFT Message" TabIndex="256" OnClientClick="ViewSwiftMessage();" />
                                                       &nbsp;&nbsp;&nbsp; <span class="elementLabel">Approve / Reject :</span>
                                                        <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="45">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                                                        </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                             </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                    <tr>
                    <td align="center" colspan="3">
                          <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                ToolTip="Save" OnClick="btnSave_Click" />
                    </td>
                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
